using System;
using System.Collections.Generic;
using System.Xml.Xsl.Qil;
using System.Xml.Xsl.XPath;

namespace System.Xml.Xsl.Xslt
{
	// Token: 0x02000580 RID: 1408
	internal class MatcherBuilder
	{
		// Token: 0x060037BC RID: 14268 RVA: 0x001365E4 File Offset: 0x001347E4
		public MatcherBuilder(XPathQilFactory f, ReferenceReplacer refReplacer, InvokeGenerator invkGen)
		{
			this.f = f;
			this.refReplacer = refReplacer;
			this.invkGen = invkGen;
		}

		// Token: 0x060037BD RID: 14269 RVA: 0x0013666C File Offset: 0x0013486C
		private void Clear()
		{
			this.priority = -1;
			this.elementPatterns.Clear();
			this.attributePatterns.Clear();
			this.textPatterns.Clear();
			this.documentPatterns.Clear();
			this.commentPatterns.Clear();
			this.piPatterns.Clear();
			this.heterogenousPatterns.Clear();
			this.allMatches.Clear();
		}

		// Token: 0x060037BE RID: 14270 RVA: 0x001366D8 File Offset: 0x001348D8
		private void AddPatterns(List<TemplateMatch> matches)
		{
			foreach (TemplateMatch templateMatch in matches)
			{
				TemplateMatch templateMatch2 = templateMatch;
				int num = this.priority + 1;
				this.priority = num;
				Pattern pattern = new Pattern(templateMatch2, num);
				XmlNodeKindFlags nodeKind = templateMatch.NodeKind;
				if (nodeKind <= XmlNodeKindFlags.Text)
				{
					switch (nodeKind)
					{
					case XmlNodeKindFlags.Document:
						this.documentPatterns.Add(pattern);
						continue;
					case XmlNodeKindFlags.Element:
						this.elementPatterns.Add(pattern);
						continue;
					case XmlNodeKindFlags.Document | XmlNodeKindFlags.Element:
						break;
					case XmlNodeKindFlags.Attribute:
						this.attributePatterns.Add(pattern);
						continue;
					default:
						if (nodeKind == XmlNodeKindFlags.Text)
						{
							this.textPatterns.Add(pattern);
							continue;
						}
						break;
					}
				}
				else
				{
					if (nodeKind == XmlNodeKindFlags.Comment)
					{
						this.commentPatterns.Add(pattern);
						continue;
					}
					if (nodeKind == XmlNodeKindFlags.PI)
					{
						this.piPatterns.Add(pattern);
						continue;
					}
				}
				this.heterogenousPatterns.Add(pattern);
			}
		}

		// Token: 0x060037BF RID: 14271 RVA: 0x001367DC File Offset: 0x001349DC
		private void CollectPatternsInternal(Stylesheet sheet, QilName mode)
		{
			foreach (Stylesheet stylesheet in sheet.Imports)
			{
				this.CollectPatternsInternal(stylesheet, mode);
			}
			List<TemplateMatch> list;
			if (sheet.TemplateMatches.TryGetValue(mode, out list))
			{
				this.AddPatterns(list);
				this.allMatches.Add(list);
			}
		}

		// Token: 0x060037C0 RID: 14272 RVA: 0x00136830 File Offset: 0x00134A30
		public void CollectPatterns(StylesheetLevel sheet, QilName mode)
		{
			this.Clear();
			foreach (Stylesheet stylesheet in sheet.Imports)
			{
				this.CollectPatternsInternal(stylesheet, mode);
			}
		}

		// Token: 0x060037C1 RID: 14273 RVA: 0x00136864 File Offset: 0x00134A64
		private QilNode MatchPattern(QilIterator it, TemplateMatch match)
		{
			QilNode qilNode = match.Condition;
			if (qilNode == null)
			{
				return this.f.True();
			}
			qilNode = qilNode.DeepClone(this.f.BaseFactory);
			return this.refReplacer.Replace(qilNode, match.Iterator, it);
		}

		// Token: 0x060037C2 RID: 14274 RVA: 0x001368AC File Offset: 0x00134AAC
		private QilNode MatchPatterns(QilIterator it, List<Pattern> patternList)
		{
			QilNode qilNode = this.f.Int32(-1);
			foreach (Pattern pattern in patternList)
			{
				qilNode = this.f.Conditional(this.MatchPattern(it, pattern.Match), this.f.Int32(pattern.Priority), qilNode);
			}
			return qilNode;
		}

		// Token: 0x060037C3 RID: 14275 RVA: 0x0013692C File Offset: 0x00134B2C
		private QilNode MatchPatterns(QilIterator it, XmlQueryType xt, List<Pattern> patternList, QilNode otherwise)
		{
			if (patternList.Count == 0)
			{
				return otherwise;
			}
			return this.f.Conditional(this.f.IsType(it, xt), this.MatchPatterns(it, patternList), otherwise);
		}

		// Token: 0x060037C4 RID: 14276 RVA: 0x0013695B File Offset: 0x00134B5B
		private bool IsNoMatch(QilNode matcher)
		{
			return matcher.NodeType == QilNodeType.LiteralInt32;
		}

		// Token: 0x060037C5 RID: 14277 RVA: 0x0013696C File Offset: 0x00134B6C
		private QilNode MatchPatternsWhosePriorityGreater(QilIterator it, List<Pattern> patternList, QilNode matcher)
		{
			if (patternList.Count == 0)
			{
				return matcher;
			}
			if (this.IsNoMatch(matcher))
			{
				return this.MatchPatterns(it, patternList);
			}
			QilIterator qilIterator = this.f.Let(matcher);
			QilNode qilNode = this.f.Int32(-1);
			int num = -1;
			foreach (Pattern pattern in patternList)
			{
				if (pattern.Priority > num + 1)
				{
					qilNode = this.f.Conditional(this.f.Gt(qilIterator, this.f.Int32(num)), qilIterator, qilNode);
				}
				qilNode = this.f.Conditional(this.MatchPattern(it, pattern.Match), this.f.Int32(pattern.Priority), qilNode);
				num = pattern.Priority;
			}
			if (num != this.priority)
			{
				qilNode = this.f.Conditional(this.f.Gt(qilIterator, this.f.Int32(num)), qilIterator, qilNode);
			}
			return this.f.Loop(qilIterator, qilNode);
		}

		// Token: 0x060037C6 RID: 14278 RVA: 0x00136A90 File Offset: 0x00134C90
		private QilNode MatchPatterns(QilIterator it, XmlQueryType xt, PatternBag patternBag, QilNode otherwise)
		{
			if (patternBag.FixedNamePatternsNames.Count == 0)
			{
				return this.MatchPatterns(it, xt, patternBag.NonFixedNamePatterns, otherwise);
			}
			QilNode qilNode = this.f.Int32(-1);
			foreach (QilName qilName in patternBag.FixedNamePatternsNames)
			{
				qilNode = this.f.Conditional(this.f.Eq(this.f.NameOf(it), qilName.ShallowClone(this.f.BaseFactory)), this.MatchPatterns(it, patternBag.FixedNamePatterns[qilName]), qilNode);
			}
			qilNode = this.MatchPatternsWhosePriorityGreater(it, patternBag.NonFixedNamePatterns, qilNode);
			return this.f.Conditional(this.f.IsType(it, xt), qilNode, otherwise);
		}

		// Token: 0x060037C7 RID: 14279 RVA: 0x00136B7C File Offset: 0x00134D7C
		public QilNode BuildMatcher(QilIterator it, IList<XslNode> actualArgs, QilNode otherwise)
		{
			QilNode qilNode = this.f.Int32(-1);
			qilNode = this.MatchPatterns(it, XmlQueryTypeFactory.PI, this.piPatterns, qilNode);
			qilNode = this.MatchPatterns(it, XmlQueryTypeFactory.Comment, this.commentPatterns, qilNode);
			qilNode = this.MatchPatterns(it, XmlQueryTypeFactory.Document, this.documentPatterns, qilNode);
			qilNode = this.MatchPatterns(it, XmlQueryTypeFactory.Text, this.textPatterns, qilNode);
			qilNode = this.MatchPatterns(it, XmlQueryTypeFactory.Attribute, this.attributePatterns, qilNode);
			qilNode = this.MatchPatterns(it, XmlQueryTypeFactory.Element, this.elementPatterns, qilNode);
			qilNode = this.MatchPatternsWhosePriorityGreater(it, this.heterogenousPatterns, qilNode);
			if (this.IsNoMatch(qilNode))
			{
				return otherwise;
			}
			QilNode[] array = new QilNode[this.priority + 2];
			int num = -1;
			foreach (List<TemplateMatch> list in this.allMatches)
			{
				foreach (TemplateMatch templateMatch in list)
				{
					array[++num] = this.invkGen.GenerateInvoke(templateMatch.TemplateFunction, actualArgs);
				}
			}
			array[++num] = otherwise;
			return this.f.Choice(qilNode, this.f.BranchList(array));
		}

		// Token: 0x04002440 RID: 9280
		private XPathQilFactory f;

		// Token: 0x04002441 RID: 9281
		private ReferenceReplacer refReplacer;

		// Token: 0x04002442 RID: 9282
		private InvokeGenerator invkGen;

		// Token: 0x04002443 RID: 9283
		private const int NoMatch = -1;

		// Token: 0x04002444 RID: 9284
		private int priority = -1;

		// Token: 0x04002445 RID: 9285
		private PatternBag elementPatterns = new PatternBag();

		// Token: 0x04002446 RID: 9286
		private PatternBag attributePatterns = new PatternBag();

		// Token: 0x04002447 RID: 9287
		private List<Pattern> textPatterns = new List<Pattern>();

		// Token: 0x04002448 RID: 9288
		private List<Pattern> documentPatterns = new List<Pattern>();

		// Token: 0x04002449 RID: 9289
		private List<Pattern> commentPatterns = new List<Pattern>();

		// Token: 0x0400244A RID: 9290
		private PatternBag piPatterns = new PatternBag();

		// Token: 0x0400244B RID: 9291
		private List<Pattern> heterogenousPatterns = new List<Pattern>();

		// Token: 0x0400244C RID: 9292
		private List<List<TemplateMatch>> allMatches = new List<List<TemplateMatch>>();
	}
}
