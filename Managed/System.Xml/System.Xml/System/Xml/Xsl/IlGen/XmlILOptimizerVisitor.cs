using System;
using System.Collections.Generic;
using System.Xml.Schema;
using System.Xml.Xsl.Qil;
using System.Xml.Xsl.Runtime;

namespace System.Xml.Xsl.IlGen
{
	// Token: 0x0200066F RID: 1647
	internal class XmlILOptimizerVisitor : QilPatternVisitor
	{
		// Token: 0x06004235 RID: 16949 RVA: 0x001615FC File Offset: 0x0015F7FC
		static XmlILOptimizerVisitor()
		{
			XmlILOptimizerVisitor.PatternsNoOpt.Add(104);
			XmlILOptimizerVisitor.PatternsNoOpt.Add(88);
			XmlILOptimizerVisitor.PatternsNoOpt.Add(97);
			XmlILOptimizerVisitor.PatternsNoOpt.Add(71);
			XmlILOptimizerVisitor.PatternsNoOpt.Add(70);
			XmlILOptimizerVisitor.PatternsNoOpt.Add(58);
			XmlILOptimizerVisitor.PatternsNoOpt.Add(96);
			XmlILOptimizerVisitor.PatternsNoOpt.Add(79);
			XmlILOptimizerVisitor.PatternsNoOpt.Add(78);
			XmlILOptimizerVisitor.PatternsNoOpt.Add(91);
			XmlILOptimizerVisitor.PatternsNoOpt.Add(93);
			XmlILOptimizerVisitor.PatternsNoOpt.Add(134);
			XmlILOptimizerVisitor.PatternsNoOpt.Add(118);
			XmlILOptimizerVisitor.PatternsNoOpt.Add(112);
			XmlILOptimizerVisitor.PatternsNoOpt.Add(41);
			XmlILOptimizerVisitor.PatternsNoOpt.Add(48);
			XmlILOptimizerVisitor.PatternsNoOpt.Add(15);
			XmlILOptimizerVisitor.PatternsNoOpt.Add(8);
			XmlILOptimizerVisitor.PatternsNoOpt.Add(23);
			XmlILOptimizerVisitor.PatternsNoOpt.Add(24);
			XmlILOptimizerVisitor.PatternsNoOpt.Add(7);
			XmlILOptimizerVisitor.PatternsNoOpt.Add(18);
		}

		// Token: 0x06004236 RID: 16950 RVA: 0x00161734 File Offset: 0x0015F934
		public XmlILOptimizerVisitor(QilExpression qil, bool optimize)
			: base(optimize ? XmlILOptimizerVisitor.PatternsOpt : XmlILOptimizerVisitor.PatternsNoOpt, qil.Factory)
		{
			this.qil = qil;
			this.elemAnalyzer = new XmlILElementAnalyzer(qil.Factory);
			this.contentAnalyzer = new XmlILStateAnalyzer(qil.Factory);
			this.nmspAnalyzer = new XmlILNamespaceAnalyzer();
		}

		// Token: 0x06004237 RID: 16951 RVA: 0x001617A8 File Offset: 0x0015F9A8
		public QilExpression Optimize()
		{
			QilExpression qilExpression = (QilExpression)this.Visit(this.qil);
			if (this[XmlILOptimization.TailCall])
			{
				TailCallAnalyzer.Analyze(qilExpression);
			}
			return qilExpression;
		}

		// Token: 0x06004238 RID: 16952 RVA: 0x001617D8 File Offset: 0x0015F9D8
		protected override QilNode Visit(QilNode nd)
		{
			if (nd != null && this[XmlILOptimization.EliminateNamespaceDecl])
			{
				QilNodeType nodeType = nd.NodeType;
				if (nodeType != QilNodeType.QilExpression)
				{
					if (nodeType != QilNodeType.ElementCtor)
					{
						if (nodeType == QilNodeType.DocumentCtor)
						{
							this.nmspAnalyzer.Analyze(nd, true);
						}
					}
					else if (!XmlILConstructInfo.Read(nd).IsNamespaceInScope)
					{
						this.nmspAnalyzer.Analyze(nd, false);
					}
				}
				else
				{
					this.nmspAnalyzer.Analyze(((QilExpression)nd).Root, true);
				}
			}
			return base.Visit(nd);
		}

		// Token: 0x06004239 RID: 16953 RVA: 0x00161854 File Offset: 0x0015FA54
		protected override QilNode VisitReference(QilNode oldNode)
		{
			QilNode qilNode = this.subs.FindReplacement(oldNode);
			if (qilNode == null)
			{
				qilNode = oldNode;
			}
			if (this[XmlILOptimization.EliminateLiteralVariables] && qilNode != null && (qilNode.NodeType == QilNodeType.Let || qilNode.NodeType == QilNodeType.For))
			{
				QilNode binding = ((QilIterator)oldNode).Binding;
				if (this.IsLiteral(binding))
				{
					return this.Replace(XmlILOptimization.EliminateLiteralVariables, qilNode, binding.ShallowClone(this.f));
				}
			}
			if (this[XmlILOptimization.EliminateUnusedGlobals] && this.IsGlobalValue(qilNode))
			{
				OptimizerPatterns.Write(qilNode).AddPattern(OptimizerPatternName.IsReferenced);
			}
			return base.VisitReference(qilNode);
		}

		// Token: 0x0600423A RID: 16954 RVA: 0x001618E4 File Offset: 0x0015FAE4
		protected bool AllowReplace(XmlILOptimization pattern, QilNode original)
		{
			return base.AllowReplace((int)pattern, original);
		}

		// Token: 0x0600423B RID: 16955 RVA: 0x001618EE File Offset: 0x0015FAEE
		protected QilNode Replace(XmlILOptimization pattern, QilNode original, QilNode replacement)
		{
			return base.Replace((int)pattern, original, replacement);
		}

		// Token: 0x0600423C RID: 16956 RVA: 0x001618FC File Offset: 0x0015FAFC
		protected override QilNode NoReplace(QilNode node)
		{
			if (node != null)
			{
				QilNodeType nodeType = node.NodeType;
				if (nodeType <= QilNodeType.Invoke)
				{
					if (nodeType - QilNodeType.Error > 1)
					{
						if (nodeType != QilNodeType.Invoke)
						{
							goto IL_0066;
						}
						if (!((QilInvoke)node).Function.MaybeSideEffects)
						{
							goto IL_0066;
						}
					}
				}
				else if (nodeType != QilNodeType.XsltInvokeLateBound)
				{
					if (nodeType != QilNodeType.XsltInvokeEarlyBound)
					{
						goto IL_0066;
					}
					if (((QilInvokeEarlyBound)node).Name.NamespaceUri.Length == 0)
					{
						goto IL_0066;
					}
				}
				IL_002C:
				OptimizerPatterns.Write(node).AddPattern(OptimizerPatternName.MaybeSideEffects);
				return node;
				IL_0066:
				for (int i = 0; i < node.Count; i++)
				{
					if (node[i] != null && OptimizerPatterns.Read(node[i]).MatchesPattern(OptimizerPatternName.MaybeSideEffects))
					{
						goto IL_002C;
					}
				}
			}
			return node;
		}

		// Token: 0x0600423D RID: 16957 RVA: 0x0016199F File Offset: 0x0015FB9F
		protected override void RecalculateType(QilNode node, XmlQueryType oldType)
		{
			if (node.NodeType != QilNodeType.Let || !this.qil.GlobalVariableList.Contains(node))
			{
				base.RecalculateType(node, oldType);
			}
		}

		// Token: 0x0600423E RID: 16958 RVA: 0x001619C8 File Offset: 0x0015FBC8
		protected override QilNode VisitQilExpression(QilExpression local0)
		{
			QilNode qilNode = local0[0];
			if (this[XmlILOptimization.EliminateUnusedGlobals] && this.AllowReplace(XmlILOptimization.EliminateUnusedGlobals, local0))
			{
				XmlILOptimizerVisitor.EliminateUnusedGlobals(local0.GlobalVariableList);
				XmlILOptimizerVisitor.EliminateUnusedGlobals(local0.GlobalParameterList);
				XmlILOptimizerVisitor.EliminateUnusedGlobals(local0.FunctionList);
			}
			if (this[XmlILOptimization.AnnotateConstruction] && this.AllowReplace(XmlILOptimization.AnnotateConstruction, local0))
			{
				foreach (QilNode qilNode2 in local0.FunctionList)
				{
					QilFunction qilFunction = (QilFunction)qilNode2;
					if (this.IsConstructedExpression(qilFunction.Definition))
					{
						qilFunction.Definition = this.contentAnalyzer.Analyze(qilFunction, qilFunction.Definition);
					}
				}
				local0.Root = this.contentAnalyzer.Analyze(null, local0.Root);
				XmlILConstructInfo.Write(local0.Root).PushToWriterLast = true;
			}
			return this.NoReplace(local0);
		}

		// Token: 0x0600423F RID: 16959 RVA: 0x00161AC0 File Offset: 0x0015FCC0
		protected override QilNode VisitOptimizeBarrier(QilUnary local0)
		{
			QilNode qilNode = local0[0];
			if (this[XmlILOptimization.AnnotateBarrier] && this.AllowReplace(XmlILOptimization.AnnotateBarrier, local0))
			{
				OptimizerPatterns.Inherit(qilNode, local0, OptimizerPatternName.IsDocOrderDistinct);
				OptimizerPatterns.Inherit(qilNode, local0, OptimizerPatternName.SameDepth);
			}
			return this.NoReplace(local0);
		}

		// Token: 0x06004240 RID: 16960 RVA: 0x00161B00 File Offset: 0x0015FD00
		protected override QilNode VisitDataSource(QilDataSource local0)
		{
			QilNode qilNode = local0[0];
			QilNode qilNode2 = local0[1];
			if (this[XmlILOptimization.FoldNone] && qilNode.XmlType == XmlQueryTypeFactory.None && this.AllowReplace(XmlILOptimization.FoldNone, local0))
			{
				return this.Replace(XmlILOptimization.FoldNone, local0, this.VisitNop(this.f.Nop(qilNode)));
			}
			if (this[XmlILOptimization.FoldNone] && qilNode2.XmlType == XmlQueryTypeFactory.None && this.AllowReplace(XmlILOptimization.FoldNone, local0))
			{
				return this.Replace(XmlILOptimization.FoldNone, local0, this.VisitNop(this.f.Nop(qilNode2)));
			}
			return this.NoReplace(local0);
		}

		// Token: 0x06004241 RID: 16961 RVA: 0x00161BA0 File Offset: 0x0015FDA0
		protected override QilNode VisitNop(QilUnary local0)
		{
			QilNode qilNode = local0[0];
			if (this[XmlILOptimization.EliminateNop] && this.AllowReplace(XmlILOptimization.EliminateNop, local0))
			{
				return this.Replace(XmlILOptimization.EliminateNop, local0, qilNode);
			}
			return this.NoReplace(local0);
		}

		// Token: 0x06004242 RID: 16962 RVA: 0x00161BDC File Offset: 0x0015FDDC
		protected override QilNode VisitError(QilUnary local0)
		{
			QilNode qilNode = local0[0];
			if (this[XmlILOptimization.FoldNone] && qilNode.XmlType == XmlQueryTypeFactory.None && this.AllowReplace(XmlILOptimization.FoldNone, local0))
			{
				return this.Replace(XmlILOptimization.FoldNone, local0, this.VisitNop(this.f.Nop(qilNode)));
			}
			return this.NoReplace(local0);
		}

		// Token: 0x06004243 RID: 16963 RVA: 0x00161C38 File Offset: 0x0015FE38
		protected override QilNode VisitWarning(QilUnary local0)
		{
			QilNode qilNode = local0[0];
			if (this[XmlILOptimization.FoldNone] && qilNode.XmlType == XmlQueryTypeFactory.None && this.AllowReplace(XmlILOptimization.FoldNone, local0))
			{
				return this.Replace(XmlILOptimization.FoldNone, local0, this.VisitNop(this.f.Nop(qilNode)));
			}
			return this.NoReplace(local0);
		}

		// Token: 0x06004244 RID: 16964 RVA: 0x00161C94 File Offset: 0x0015FE94
		protected override QilNode VisitLet(QilIterator local0)
		{
			QilNode qilNode = local0[0];
			if (local0.XmlType.IsSingleton && !this.IsGlobalVariable(local0) && this[XmlILOptimization.NormalizeSingletonLet] && this.AllowReplace(XmlILOptimization.NormalizeSingletonLet, local0))
			{
				local0.NodeType = QilNodeType.For;
				this.VisitFor(local0);
			}
			if (this[XmlILOptimization.AnnotateLet] && this.AllowReplace(XmlILOptimization.AnnotateLet, local0))
			{
				OptimizerPatterns.Inherit(qilNode, local0, OptimizerPatternName.Step);
				OptimizerPatterns.Inherit(qilNode, local0, OptimizerPatternName.IsDocOrderDistinct);
				OptimizerPatterns.Inherit(qilNode, local0, OptimizerPatternName.SameDepth);
			}
			return this.NoReplace(local0);
		}

		// Token: 0x06004245 RID: 16965 RVA: 0x00161D20 File Offset: 0x0015FF20
		protected override QilNode VisitPositionOf(QilUnary local0)
		{
			QilNode qilNode = local0[0];
			if (this[XmlILOptimization.EliminatePositionOf] && qilNode.NodeType != QilNodeType.For && this.AllowReplace(XmlILOptimization.EliminatePositionOf, local0))
			{
				return this.Replace(XmlILOptimization.EliminatePositionOf, local0, this.VisitLiteralInt32(this.f.LiteralInt32(1)));
			}
			if (this[XmlILOptimization.EliminatePositionOf] && qilNode.NodeType == QilNodeType.For && qilNode[0].XmlType.IsSingleton && this.AllowReplace(XmlILOptimization.EliminatePositionOf, local0))
			{
				return this.Replace(XmlILOptimization.EliminatePositionOf, local0, this.VisitLiteralInt32(this.f.LiteralInt32(1)));
			}
			if (this[XmlILOptimization.AnnotatePositionalIterator] && this.AllowReplace(XmlILOptimization.AnnotatePositionalIterator, local0))
			{
				OptimizerPatterns.Write(qilNode).AddPattern(OptimizerPatternName.IsPositional);
			}
			return this.NoReplace(local0);
		}

		// Token: 0x06004246 RID: 16966 RVA: 0x00161DE8 File Offset: 0x0015FFE8
		protected override QilNode VisitAnd(QilBinary local0)
		{
			QilNode qilNode = local0[0];
			QilNode qilNode2 = local0[1];
			if (this[XmlILOptimization.FoldNone] && qilNode.XmlType == XmlQueryTypeFactory.None && this.AllowReplace(XmlILOptimization.FoldNone, local0))
			{
				return this.Replace(XmlILOptimization.FoldNone, local0, this.VisitNop(this.f.Nop(qilNode)));
			}
			if (this[XmlILOptimization.FoldNone] && qilNode2.XmlType == XmlQueryTypeFactory.None && this.AllowReplace(XmlILOptimization.FoldNone, local0))
			{
				return this.Replace(XmlILOptimization.FoldNone, local0, this.VisitNop(this.f.Nop(qilNode2)));
			}
			if (this[XmlILOptimization.EliminateAnd] && qilNode.NodeType == QilNodeType.True && this.AllowReplace(XmlILOptimization.EliminateAnd, local0))
			{
				return this.Replace(XmlILOptimization.EliminateAnd, local0, qilNode2);
			}
			if (this[XmlILOptimization.EliminateAnd] && qilNode.NodeType == QilNodeType.False && this.AllowReplace(XmlILOptimization.EliminateAnd, local0))
			{
				return this.Replace(XmlILOptimization.EliminateAnd, local0, qilNode);
			}
			if (this[XmlILOptimization.EliminateAnd] && qilNode2.NodeType == QilNodeType.True && this.AllowReplace(XmlILOptimization.EliminateAnd, local0))
			{
				return this.Replace(XmlILOptimization.EliminateAnd, local0, qilNode);
			}
			if (this[XmlILOptimization.EliminateAnd] && qilNode2.NodeType == QilNodeType.False && this.AllowReplace(XmlILOptimization.EliminateAnd, local0))
			{
				return this.Replace(XmlILOptimization.EliminateAnd, local0, qilNode2);
			}
			return this.NoReplace(local0);
		}

		// Token: 0x06004247 RID: 16967 RVA: 0x00161F30 File Offset: 0x00160130
		protected override QilNode VisitOr(QilBinary local0)
		{
			QilNode qilNode = local0[0];
			QilNode qilNode2 = local0[1];
			if (this[XmlILOptimization.FoldNone] && qilNode.XmlType == XmlQueryTypeFactory.None && this.AllowReplace(XmlILOptimization.FoldNone, local0))
			{
				return this.Replace(XmlILOptimization.FoldNone, local0, this.VisitNop(this.f.Nop(qilNode)));
			}
			if (this[XmlILOptimization.FoldNone] && qilNode2.XmlType == XmlQueryTypeFactory.None && this.AllowReplace(XmlILOptimization.FoldNone, local0))
			{
				return this.Replace(XmlILOptimization.FoldNone, local0, this.VisitNop(this.f.Nop(qilNode2)));
			}
			if (this[XmlILOptimization.EliminateOr] && qilNode.NodeType == QilNodeType.True && this.AllowReplace(XmlILOptimization.EliminateOr, local0))
			{
				return this.Replace(XmlILOptimization.EliminateOr, local0, qilNode);
			}
			if (this[XmlILOptimization.EliminateOr] && qilNode.NodeType == QilNodeType.False && this.AllowReplace(XmlILOptimization.EliminateOr, local0))
			{
				return this.Replace(XmlILOptimization.EliminateOr, local0, qilNode2);
			}
			if (this[XmlILOptimization.EliminateOr] && qilNode2.NodeType == QilNodeType.True && this.AllowReplace(XmlILOptimization.EliminateOr, local0))
			{
				return this.Replace(XmlILOptimization.EliminateOr, local0, qilNode2);
			}
			if (this[XmlILOptimization.EliminateOr] && qilNode2.NodeType == QilNodeType.False && this.AllowReplace(XmlILOptimization.EliminateOr, local0))
			{
				return this.Replace(XmlILOptimization.EliminateOr, local0, qilNode);
			}
			return this.NoReplace(local0);
		}

		// Token: 0x06004248 RID: 16968 RVA: 0x00162078 File Offset: 0x00160278
		protected override QilNode VisitNot(QilUnary local0)
		{
			QilNode qilNode = local0[0];
			if (this[XmlILOptimization.FoldNone] && qilNode.XmlType == XmlQueryTypeFactory.None && this.AllowReplace(XmlILOptimization.FoldNone, local0))
			{
				return this.Replace(XmlILOptimization.FoldNone, local0, this.VisitNop(this.f.Nop(qilNode)));
			}
			if (this[XmlILOptimization.EliminateNot] && qilNode.NodeType == QilNodeType.True && this.AllowReplace(XmlILOptimization.EliminateNot, local0))
			{
				return this.Replace(XmlILOptimization.EliminateNot, local0, this.VisitFalse(this.f.False()));
			}
			if (this[XmlILOptimization.EliminateNot] && qilNode.NodeType == QilNodeType.False && this.AllowReplace(XmlILOptimization.EliminateNot, local0))
			{
				return this.Replace(XmlILOptimization.EliminateNot, local0, this.VisitTrue(this.f.True()));
			}
			return this.NoReplace(local0);
		}

		// Token: 0x06004249 RID: 16969 RVA: 0x00162148 File Offset: 0x00160348
		protected override QilNode VisitConditional(QilTernary local0)
		{
			QilNode qilNode = local0[0];
			QilNode qilNode2 = local0[1];
			QilNode qilNode3 = local0[2];
			if (this[XmlILOptimization.FoldNone] && qilNode.XmlType == XmlQueryTypeFactory.None && this.AllowReplace(XmlILOptimization.FoldNone, local0))
			{
				return this.Replace(XmlILOptimization.FoldNone, local0, this.VisitNop(this.f.Nop(qilNode)));
			}
			if (this[XmlILOptimization.EliminateConditional] && qilNode.NodeType == QilNodeType.True && this.AllowReplace(XmlILOptimization.EliminateConditional, local0))
			{
				return this.Replace(XmlILOptimization.EliminateConditional, local0, qilNode2);
			}
			if (this[XmlILOptimization.EliminateConditional] && qilNode.NodeType == QilNodeType.False && this.AllowReplace(XmlILOptimization.EliminateConditional, local0))
			{
				return this.Replace(XmlILOptimization.EliminateConditional, local0, qilNode3);
			}
			if (this[XmlILOptimization.EliminateConditional] && qilNode2.NodeType == QilNodeType.True && qilNode3.NodeType == QilNodeType.False && this.AllowReplace(XmlILOptimization.EliminateConditional, local0))
			{
				return this.Replace(XmlILOptimization.EliminateConditional, local0, qilNode);
			}
			if (this[XmlILOptimization.EliminateConditional] && qilNode2.NodeType == QilNodeType.False && qilNode3.NodeType == QilNodeType.True && this.AllowReplace(XmlILOptimization.EliminateConditional, local0))
			{
				return this.Replace(XmlILOptimization.EliminateConditional, local0, this.VisitNot(this.f.Not(qilNode)));
			}
			if (this[XmlILOptimization.FoldConditionalNot] && qilNode.NodeType == QilNodeType.Not)
			{
				QilNode qilNode4 = qilNode[0];
				if (this.AllowReplace(XmlILOptimization.FoldConditionalNot, local0))
				{
					return this.Replace(XmlILOptimization.FoldConditionalNot, local0, this.VisitConditional(this.f.Conditional(qilNode4, qilNode3, qilNode2)));
				}
			}
			if (this[XmlILOptimization.NormalizeConditionalText] && qilNode2.NodeType == QilNodeType.TextCtor)
			{
				QilNode qilNode5 = qilNode2[0];
				if (qilNode3.NodeType == QilNodeType.TextCtor)
				{
					QilNode qilNode6 = qilNode3[0];
					if (this.AllowReplace(XmlILOptimization.NormalizeConditionalText, local0))
					{
						return this.Replace(XmlILOptimization.NormalizeConditionalText, local0, this.VisitTextCtor(this.f.TextCtor(this.VisitConditional(this.f.Conditional(qilNode, qilNode5, qilNode6)))));
					}
				}
			}
			return this.NoReplace(local0);
		}

		// Token: 0x0600424A RID: 16970 RVA: 0x00162330 File Offset: 0x00160530
		protected override QilNode VisitChoice(QilChoice local0)
		{
			QilNode qilNode = local0[0];
			QilNode qilNode2 = local0[1];
			if (this[XmlILOptimization.AnnotateConstruction] && this.AllowReplace(XmlILOptimization.AnnotateConstruction, local0))
			{
				this.contentAnalyzer.Analyze(local0, null);
			}
			return this.NoReplace(local0);
		}

		// Token: 0x0600424B RID: 16971 RVA: 0x0016236C File Offset: 0x0016056C
		protected override QilNode VisitLength(QilUnary local0)
		{
			QilNode qilNode = local0[0];
			if (this[XmlILOptimization.FoldNone] && qilNode.XmlType == XmlQueryTypeFactory.None && this.AllowReplace(XmlILOptimization.FoldNone, local0))
			{
				return this.Replace(XmlILOptimization.FoldNone, local0, this.VisitNop(this.f.Nop(qilNode)));
			}
			if (this[XmlILOptimization.EliminateLength] && qilNode.NodeType == QilNodeType.Sequence && qilNode.Count == 0 && this.AllowReplace(XmlILOptimization.EliminateLength, local0))
			{
				return this.Replace(XmlILOptimization.EliminateLength, local0, this.VisitLiteralInt32(this.f.LiteralInt32(0)));
			}
			if (this[XmlILOptimization.EliminateLength] && qilNode.XmlType.IsSingleton && !OptimizerPatterns.Read(qilNode).MatchesPattern(OptimizerPatternName.MaybeSideEffects) && this.AllowReplace(XmlILOptimization.EliminateLength, local0))
			{
				return this.Replace(XmlILOptimization.EliminateLength, local0, this.VisitLiteralInt32(this.f.LiteralInt32(1)));
			}
			if (this[XmlILOptimization.IntroducePrecedingDod] && !this.IsDocOrderDistinct(qilNode) && (this.IsStepPattern(qilNode, QilNodeType.XPathPreceding) || this.IsStepPattern(qilNode, QilNodeType.PrecedingSibling)) && this.AllowReplace(XmlILOptimization.IntroducePrecedingDod, local0))
			{
				return this.Replace(XmlILOptimization.IntroducePrecedingDod, local0, this.VisitLength(this.f.Length(this.VisitDocOrderDistinct(this.f.DocOrderDistinct(qilNode)))));
			}
			return this.NoReplace(local0);
		}

		// Token: 0x0600424C RID: 16972 RVA: 0x001624B8 File Offset: 0x001606B8
		protected override QilNode VisitSequence(QilList local0)
		{
			if (local0.Count == 1 && this[XmlILOptimization.EliminateSequence] && this.AllowReplace(XmlILOptimization.EliminateSequence, local0))
			{
				return this.Replace(XmlILOptimization.EliminateSequence, local0, local0[0]);
			}
			if (this.HasNestedSequence(local0) && this[XmlILOptimization.NormalizeNestedSequences] && this.AllowReplace(XmlILOptimization.NormalizeNestedSequences, local0))
			{
				QilNode qilNode = this.VisitSequence(this.f.Sequence());
				foreach (QilNode qilNode2 in local0)
				{
					if (qilNode2.NodeType == QilNodeType.Sequence)
					{
						qilNode.Add(qilNode2);
					}
					else
					{
						qilNode.Add(qilNode2);
					}
				}
				qilNode = this.VisitSequence((QilList)qilNode);
				return this.Replace(XmlILOptimization.NormalizeNestedSequences, local0, qilNode);
			}
			return this.NoReplace(local0);
		}

		// Token: 0x0600424D RID: 16973 RVA: 0x001625A0 File Offset: 0x001607A0
		protected override QilNode VisitUnion(QilBinary local0)
		{
			QilNode qilNode = local0[0];
			QilNode qilNode2 = local0[1];
			if (this[XmlILOptimization.FoldNone] && qilNode.XmlType == XmlQueryTypeFactory.None && this.AllowReplace(XmlILOptimization.FoldNone, local0))
			{
				return this.Replace(XmlILOptimization.FoldNone, local0, this.VisitNop(this.f.Nop(qilNode)));
			}
			if (this[XmlILOptimization.FoldNone] && qilNode2.XmlType == XmlQueryTypeFactory.None && this.AllowReplace(XmlILOptimization.FoldNone, local0))
			{
				return this.Replace(XmlILOptimization.FoldNone, local0, this.VisitNop(this.f.Nop(qilNode2)));
			}
			if (this[XmlILOptimization.EliminateUnion] && qilNode2 == qilNode && this.AllowReplace(XmlILOptimization.EliminateUnion, local0))
			{
				return this.Replace(XmlILOptimization.EliminateUnion, local0, this.VisitDocOrderDistinct(this.f.DocOrderDistinct(qilNode)));
			}
			if (this[XmlILOptimization.EliminateUnion] && qilNode.NodeType == QilNodeType.Sequence && qilNode.Count == 0 && this.AllowReplace(XmlILOptimization.EliminateUnion, local0))
			{
				return this.Replace(XmlILOptimization.EliminateUnion, local0, this.VisitDocOrderDistinct(this.f.DocOrderDistinct(qilNode2)));
			}
			if (this[XmlILOptimization.EliminateUnion] && qilNode2.NodeType == QilNodeType.Sequence && qilNode2.Count == 0 && this.AllowReplace(XmlILOptimization.EliminateUnion, local0))
			{
				return this.Replace(XmlILOptimization.EliminateUnion, local0, this.VisitDocOrderDistinct(this.f.DocOrderDistinct(qilNode)));
			}
			if (this[XmlILOptimization.EliminateUnion] && qilNode.NodeType == QilNodeType.XmlContext && qilNode2.NodeType == QilNodeType.XmlContext && this.AllowReplace(XmlILOptimization.EliminateUnion, local0))
			{
				return this.Replace(XmlILOptimization.EliminateUnion, local0, qilNode);
			}
			if (this[XmlILOptimization.NormalizeUnion] && (!this.IsDocOrderDistinct(qilNode) || !this.IsDocOrderDistinct(qilNode2)) && this.AllowReplace(XmlILOptimization.NormalizeUnion, local0))
			{
				return this.Replace(XmlILOptimization.NormalizeUnion, local0, this.VisitUnion(this.f.Union(this.VisitDocOrderDistinct(this.f.DocOrderDistinct(qilNode)), this.VisitDocOrderDistinct(this.f.DocOrderDistinct(qilNode2)))));
			}
			if (this[XmlILOptimization.AnnotateUnion] && this.AllowReplace(XmlILOptimization.AnnotateUnion, local0))
			{
				OptimizerPatterns.Write(local0).AddPattern(OptimizerPatternName.IsDocOrderDistinct);
			}
			if (this[XmlILOptimization.AnnotateUnionContent] && (this.IsStepPattern(qilNode, QilNodeType.Content) || this.IsStepPattern(qilNode, QilNodeType.Union)) && (this.IsStepPattern(qilNode2, QilNodeType.Content) || this.IsStepPattern(qilNode2, QilNodeType.Union)) && OptimizerPatterns.Read(qilNode).GetArgument(OptimizerPatternArgument.StepInput) == OptimizerPatterns.Read(qilNode2).GetArgument(OptimizerPatternArgument.StepInput) && this.AllowReplace(XmlILOptimization.AnnotateUnionContent, local0))
			{
				this.AddStepPattern(local0, (QilNode)OptimizerPatterns.Read(qilNode).GetArgument(OptimizerPatternArgument.StepInput));
				OptimizerPatterns.Write(local0).AddPattern(OptimizerPatternName.SameDepth);
			}
			return this.NoReplace(local0);
		}

		// Token: 0x0600424E RID: 16974 RVA: 0x00162840 File Offset: 0x00160A40
		protected override QilNode VisitIntersection(QilBinary local0)
		{
			QilNode qilNode = local0[0];
			QilNode qilNode2 = local0[1];
			if (this[XmlILOptimization.FoldNone] && qilNode.XmlType == XmlQueryTypeFactory.None && this.AllowReplace(XmlILOptimization.FoldNone, local0))
			{
				return this.Replace(XmlILOptimization.FoldNone, local0, this.VisitNop(this.f.Nop(qilNode)));
			}
			if (this[XmlILOptimization.FoldNone] && qilNode2.XmlType == XmlQueryTypeFactory.None && this.AllowReplace(XmlILOptimization.FoldNone, local0))
			{
				return this.Replace(XmlILOptimization.FoldNone, local0, this.VisitNop(this.f.Nop(qilNode2)));
			}
			if (this[XmlILOptimization.EliminateIntersection] && qilNode2 == qilNode && this.AllowReplace(XmlILOptimization.EliminateIntersection, local0))
			{
				return this.Replace(XmlILOptimization.EliminateIntersection, local0, this.VisitDocOrderDistinct(this.f.DocOrderDistinct(qilNode)));
			}
			if (this[XmlILOptimization.EliminateIntersection] && qilNode.NodeType == QilNodeType.Sequence && qilNode.Count == 0 && this.AllowReplace(XmlILOptimization.EliminateIntersection, local0))
			{
				return this.Replace(XmlILOptimization.EliminateIntersection, local0, qilNode);
			}
			if (this[XmlILOptimization.EliminateIntersection] && qilNode2.NodeType == QilNodeType.Sequence && qilNode2.Count == 0 && this.AllowReplace(XmlILOptimization.EliminateIntersection, local0))
			{
				return this.Replace(XmlILOptimization.EliminateIntersection, local0, qilNode2);
			}
			if (this[XmlILOptimization.EliminateIntersection] && qilNode.NodeType == QilNodeType.XmlContext && qilNode2.NodeType == QilNodeType.XmlContext && this.AllowReplace(XmlILOptimization.EliminateIntersection, local0))
			{
				return this.Replace(XmlILOptimization.EliminateIntersection, local0, qilNode);
			}
			if (this[XmlILOptimization.NormalizeIntersect] && (!this.IsDocOrderDistinct(qilNode) || !this.IsDocOrderDistinct(qilNode2)) && this.AllowReplace(XmlILOptimization.NormalizeIntersect, local0))
			{
				return this.Replace(XmlILOptimization.NormalizeIntersect, local0, this.VisitIntersection(this.f.Intersection(this.VisitDocOrderDistinct(this.f.DocOrderDistinct(qilNode)), this.VisitDocOrderDistinct(this.f.DocOrderDistinct(qilNode2)))));
			}
			if (this[XmlILOptimization.AnnotateIntersect] && this.AllowReplace(XmlILOptimization.AnnotateIntersect, local0))
			{
				OptimizerPatterns.Write(local0).AddPattern(OptimizerPatternName.IsDocOrderDistinct);
			}
			return this.NoReplace(local0);
		}

		// Token: 0x0600424F RID: 16975 RVA: 0x00162A34 File Offset: 0x00160C34
		protected override QilNode VisitDifference(QilBinary local0)
		{
			QilNode qilNode = local0[0];
			QilNode qilNode2 = local0[1];
			if (this[XmlILOptimization.FoldNone] && qilNode.XmlType == XmlQueryTypeFactory.None && this.AllowReplace(XmlILOptimization.FoldNone, local0))
			{
				return this.Replace(XmlILOptimization.FoldNone, local0, this.VisitNop(this.f.Nop(qilNode)));
			}
			if (this[XmlILOptimization.FoldNone] && qilNode2.XmlType == XmlQueryTypeFactory.None && this.AllowReplace(XmlILOptimization.FoldNone, local0))
			{
				return this.Replace(XmlILOptimization.FoldNone, local0, this.VisitNop(this.f.Nop(qilNode2)));
			}
			if (this[XmlILOptimization.EliminateDifference] && qilNode.NodeType == QilNodeType.Sequence && qilNode.Count == 0 && this.AllowReplace(XmlILOptimization.EliminateDifference, local0))
			{
				return this.Replace(XmlILOptimization.EliminateDifference, local0, qilNode);
			}
			if (this[XmlILOptimization.EliminateDifference] && qilNode2.NodeType == QilNodeType.Sequence && qilNode2.Count == 0 && this.AllowReplace(XmlILOptimization.EliminateDifference, local0))
			{
				return this.Replace(XmlILOptimization.EliminateDifference, local0, this.VisitDocOrderDistinct(this.f.DocOrderDistinct(qilNode)));
			}
			if (this[XmlILOptimization.EliminateDifference] && qilNode2 == qilNode && this.AllowReplace(XmlILOptimization.EliminateDifference, local0))
			{
				return this.Replace(XmlILOptimization.EliminateDifference, local0, this.VisitSequence(this.f.Sequence()));
			}
			if (this[XmlILOptimization.EliminateDifference] && qilNode.NodeType == QilNodeType.XmlContext && qilNode2.NodeType == QilNodeType.XmlContext && this.AllowReplace(XmlILOptimization.EliminateDifference, local0))
			{
				return this.Replace(XmlILOptimization.EliminateDifference, local0, this.VisitSequence(this.f.Sequence()));
			}
			if (this[XmlILOptimization.NormalizeDifference] && (!this.IsDocOrderDistinct(qilNode) || !this.IsDocOrderDistinct(qilNode2)) && this.AllowReplace(XmlILOptimization.NormalizeDifference, local0))
			{
				return this.Replace(XmlILOptimization.NormalizeDifference, local0, this.VisitDifference(this.f.Difference(this.VisitDocOrderDistinct(this.f.DocOrderDistinct(qilNode)), this.VisitDocOrderDistinct(this.f.DocOrderDistinct(qilNode2)))));
			}
			if (this[XmlILOptimization.AnnotateDifference] && this.AllowReplace(XmlILOptimization.AnnotateDifference, local0))
			{
				OptimizerPatterns.Write(local0).AddPattern(OptimizerPatternName.IsDocOrderDistinct);
			}
			return this.NoReplace(local0);
		}

		// Token: 0x06004250 RID: 16976 RVA: 0x00162C48 File Offset: 0x00160E48
		protected override QilNode VisitAverage(QilUnary local0)
		{
			QilNode qilNode = local0[0];
			if (this[XmlILOptimization.FoldNone] && qilNode.XmlType == XmlQueryTypeFactory.None && this.AllowReplace(XmlILOptimization.FoldNone, local0))
			{
				return this.Replace(XmlILOptimization.FoldNone, local0, this.VisitNop(this.f.Nop(qilNode)));
			}
			if (this[XmlILOptimization.EliminateAverage] && qilNode.XmlType.Cardinality == XmlQueryCardinality.Zero && this.AllowReplace(XmlILOptimization.EliminateAverage, local0))
			{
				return this.Replace(XmlILOptimization.EliminateAverage, local0, this.VisitNop(this.f.Nop(qilNode)));
			}
			return this.NoReplace(local0);
		}

		// Token: 0x06004251 RID: 16977 RVA: 0x00162CEC File Offset: 0x00160EEC
		protected override QilNode VisitSum(QilUnary local0)
		{
			QilNode qilNode = local0[0];
			if (this[XmlILOptimization.FoldNone] && qilNode.XmlType == XmlQueryTypeFactory.None && this.AllowReplace(XmlILOptimization.FoldNone, local0))
			{
				return this.Replace(XmlILOptimization.FoldNone, local0, this.VisitNop(this.f.Nop(qilNode)));
			}
			if (this[XmlILOptimization.EliminateSum] && qilNode.XmlType.Cardinality == XmlQueryCardinality.Zero && this.AllowReplace(XmlILOptimization.EliminateSum, local0))
			{
				return this.Replace(XmlILOptimization.EliminateSum, local0, this.VisitNop(this.f.Nop(qilNode)));
			}
			return this.NoReplace(local0);
		}

		// Token: 0x06004252 RID: 16978 RVA: 0x00162D90 File Offset: 0x00160F90
		protected override QilNode VisitMinimum(QilUnary local0)
		{
			QilNode qilNode = local0[0];
			if (this[XmlILOptimization.FoldNone] && qilNode.XmlType == XmlQueryTypeFactory.None && this.AllowReplace(XmlILOptimization.FoldNone, local0))
			{
				return this.Replace(XmlILOptimization.FoldNone, local0, this.VisitNop(this.f.Nop(qilNode)));
			}
			if (this[XmlILOptimization.EliminateMinimum] && qilNode.XmlType.Cardinality == XmlQueryCardinality.Zero && this.AllowReplace(XmlILOptimization.EliminateMinimum, local0))
			{
				return this.Replace(XmlILOptimization.EliminateMinimum, local0, this.VisitNop(this.f.Nop(qilNode)));
			}
			return this.NoReplace(local0);
		}

		// Token: 0x06004253 RID: 16979 RVA: 0x00162E34 File Offset: 0x00161034
		protected override QilNode VisitMaximum(QilUnary local0)
		{
			QilNode qilNode = local0[0];
			if (this[XmlILOptimization.FoldNone] && qilNode.XmlType == XmlQueryTypeFactory.None && this.AllowReplace(XmlILOptimization.FoldNone, local0))
			{
				return this.Replace(XmlILOptimization.FoldNone, local0, this.VisitNop(this.f.Nop(qilNode)));
			}
			if (this[XmlILOptimization.EliminateMaximum] && qilNode.XmlType.Cardinality == XmlQueryCardinality.Zero && this.AllowReplace(XmlILOptimization.EliminateMaximum, local0))
			{
				return this.Replace(XmlILOptimization.EliminateMaximum, local0, this.VisitNop(this.f.Nop(qilNode)));
			}
			return this.NoReplace(local0);
		}

		// Token: 0x06004254 RID: 16980 RVA: 0x00162ED8 File Offset: 0x001610D8
		protected override QilNode VisitNegate(QilUnary local0)
		{
			QilNode qilNode = local0[0];
			if (this[XmlILOptimization.FoldNone] && qilNode.XmlType == XmlQueryTypeFactory.None && this.AllowReplace(XmlILOptimization.FoldNone, local0))
			{
				return this.Replace(XmlILOptimization.FoldNone, local0, this.VisitNop(this.f.Nop(qilNode)));
			}
			if (this[XmlILOptimization.EliminateNegate] && qilNode.NodeType == QilNodeType.LiteralDecimal)
			{
				decimal num = (decimal)((QilLiteral)qilNode).Value;
				if (this.AllowReplace(XmlILOptimization.EliminateNegate, local0))
				{
					return this.Replace(XmlILOptimization.EliminateNegate, local0, this.VisitLiteralDecimal(this.f.LiteralDecimal(-num)));
				}
			}
			if (this[XmlILOptimization.EliminateNegate] && qilNode.NodeType == QilNodeType.LiteralDouble)
			{
				double num2 = (double)((QilLiteral)qilNode).Value;
				if (this.AllowReplace(XmlILOptimization.EliminateNegate, local0))
				{
					return this.Replace(XmlILOptimization.EliminateNegate, local0, this.VisitLiteralDouble(this.f.LiteralDouble(-num2)));
				}
			}
			if (this[XmlILOptimization.EliminateNegate] && qilNode.NodeType == QilNodeType.LiteralInt32)
			{
				int num3 = (int)((QilLiteral)qilNode).Value;
				if (this.AllowReplace(XmlILOptimization.EliminateNegate, local0))
				{
					return this.Replace(XmlILOptimization.EliminateNegate, local0, this.VisitLiteralInt32(this.f.LiteralInt32(-num3)));
				}
			}
			if (this[XmlILOptimization.EliminateNegate] && qilNode.NodeType == QilNodeType.LiteralInt64)
			{
				long num4 = (long)((QilLiteral)qilNode).Value;
				if (this.AllowReplace(XmlILOptimization.EliminateNegate, local0))
				{
					return this.Replace(XmlILOptimization.EliminateNegate, local0, this.VisitLiteralInt64(this.f.LiteralInt64(-num4)));
				}
			}
			return this.NoReplace(local0);
		}

		// Token: 0x06004255 RID: 16981 RVA: 0x0016306C File Offset: 0x0016126C
		protected override QilNode VisitAdd(QilBinary local0)
		{
			QilNode qilNode = local0[0];
			QilNode qilNode2 = local0[1];
			if (this[XmlILOptimization.FoldNone] && qilNode.XmlType == XmlQueryTypeFactory.None && this.AllowReplace(XmlILOptimization.FoldNone, local0))
			{
				return this.Replace(XmlILOptimization.FoldNone, local0, this.VisitNop(this.f.Nop(qilNode)));
			}
			if (this[XmlILOptimization.FoldNone] && qilNode2.XmlType == XmlQueryTypeFactory.None && this.AllowReplace(XmlILOptimization.FoldNone, local0))
			{
				return this.Replace(XmlILOptimization.FoldNone, local0, this.VisitNop(this.f.Nop(qilNode2)));
			}
			if (this[XmlILOptimization.EliminateAdd] && this.IsLiteral(qilNode) && this.IsLiteral(qilNode2) && this.CanFoldArithmetic(QilNodeType.Add, (QilLiteral)qilNode, (QilLiteral)qilNode2) && this.AllowReplace(XmlILOptimization.EliminateAdd, local0))
			{
				return this.Replace(XmlILOptimization.EliminateAdd, local0, this.FoldArithmetic(QilNodeType.Add, (QilLiteral)qilNode, (QilLiteral)qilNode2));
			}
			if (this[XmlILOptimization.NormalizeAddLiteral] && this.IsLiteral(qilNode) && !this.IsLiteral(qilNode2) && this.AllowReplace(XmlILOptimization.NormalizeAddLiteral, local0))
			{
				return this.Replace(XmlILOptimization.NormalizeAddLiteral, local0, this.VisitAdd(this.f.Add(qilNode2, qilNode)));
			}
			return this.NoReplace(local0);
		}

		// Token: 0x06004256 RID: 16982 RVA: 0x001631AC File Offset: 0x001613AC
		protected override QilNode VisitSubtract(QilBinary local0)
		{
			QilNode qilNode = local0[0];
			QilNode qilNode2 = local0[1];
			if (this[XmlILOptimization.FoldNone] && qilNode.XmlType == XmlQueryTypeFactory.None && this.AllowReplace(XmlILOptimization.FoldNone, local0))
			{
				return this.Replace(XmlILOptimization.FoldNone, local0, this.VisitNop(this.f.Nop(qilNode)));
			}
			if (this[XmlILOptimization.FoldNone] && qilNode2.XmlType == XmlQueryTypeFactory.None && this.AllowReplace(XmlILOptimization.FoldNone, local0))
			{
				return this.Replace(XmlILOptimization.FoldNone, local0, this.VisitNop(this.f.Nop(qilNode2)));
			}
			if (this[XmlILOptimization.EliminateSubtract] && this.IsLiteral(qilNode) && this.IsLiteral(qilNode2) && this.CanFoldArithmetic(QilNodeType.Subtract, (QilLiteral)qilNode, (QilLiteral)qilNode2) && this.AllowReplace(XmlILOptimization.EliminateSubtract, local0))
			{
				return this.Replace(XmlILOptimization.EliminateSubtract, local0, this.FoldArithmetic(QilNodeType.Subtract, (QilLiteral)qilNode, (QilLiteral)qilNode2));
			}
			return this.NoReplace(local0);
		}

		// Token: 0x06004257 RID: 16983 RVA: 0x001632A8 File Offset: 0x001614A8
		protected override QilNode VisitMultiply(QilBinary local0)
		{
			QilNode qilNode = local0[0];
			QilNode qilNode2 = local0[1];
			if (this[XmlILOptimization.FoldNone] && qilNode.XmlType == XmlQueryTypeFactory.None && this.AllowReplace(XmlILOptimization.FoldNone, local0))
			{
				return this.Replace(XmlILOptimization.FoldNone, local0, this.VisitNop(this.f.Nop(qilNode)));
			}
			if (this[XmlILOptimization.FoldNone] && qilNode2.XmlType == XmlQueryTypeFactory.None && this.AllowReplace(XmlILOptimization.FoldNone, local0))
			{
				return this.Replace(XmlILOptimization.FoldNone, local0, this.VisitNop(this.f.Nop(qilNode2)));
			}
			if (this[XmlILOptimization.EliminateMultiply] && this.IsLiteral(qilNode) && this.IsLiteral(qilNode2) && this.CanFoldArithmetic(QilNodeType.Multiply, (QilLiteral)qilNode, (QilLiteral)qilNode2) && this.AllowReplace(XmlILOptimization.EliminateMultiply, local0))
			{
				return this.Replace(XmlILOptimization.EliminateMultiply, local0, this.FoldArithmetic(QilNodeType.Multiply, (QilLiteral)qilNode, (QilLiteral)qilNode2));
			}
			if (this[XmlILOptimization.NormalizeMultiplyLiteral] && this.IsLiteral(qilNode) && !this.IsLiteral(qilNode2) && this.AllowReplace(XmlILOptimization.NormalizeMultiplyLiteral, local0))
			{
				return this.Replace(XmlILOptimization.NormalizeMultiplyLiteral, local0, this.VisitMultiply(this.f.Multiply(qilNode2, qilNode)));
			}
			return this.NoReplace(local0);
		}

		// Token: 0x06004258 RID: 16984 RVA: 0x001633F0 File Offset: 0x001615F0
		protected override QilNode VisitDivide(QilBinary local0)
		{
			QilNode qilNode = local0[0];
			QilNode qilNode2 = local0[1];
			if (this[XmlILOptimization.FoldNone] && qilNode.XmlType == XmlQueryTypeFactory.None && this.AllowReplace(XmlILOptimization.FoldNone, local0))
			{
				return this.Replace(XmlILOptimization.FoldNone, local0, this.VisitNop(this.f.Nop(qilNode)));
			}
			if (this[XmlILOptimization.FoldNone] && qilNode2.XmlType == XmlQueryTypeFactory.None && this.AllowReplace(XmlILOptimization.FoldNone, local0))
			{
				return this.Replace(XmlILOptimization.FoldNone, local0, this.VisitNop(this.f.Nop(qilNode2)));
			}
			if (this[XmlILOptimization.EliminateDivide] && this.IsLiteral(qilNode) && this.IsLiteral(qilNode2) && this.CanFoldArithmetic(QilNodeType.Divide, (QilLiteral)qilNode, (QilLiteral)qilNode2) && this.AllowReplace(XmlILOptimization.EliminateDivide, local0))
			{
				return this.Replace(XmlILOptimization.EliminateDivide, local0, this.FoldArithmetic(QilNodeType.Divide, (QilLiteral)qilNode, (QilLiteral)qilNode2));
			}
			return this.NoReplace(local0);
		}

		// Token: 0x06004259 RID: 16985 RVA: 0x001634EC File Offset: 0x001616EC
		protected override QilNode VisitModulo(QilBinary local0)
		{
			QilNode qilNode = local0[0];
			QilNode qilNode2 = local0[1];
			if (this[XmlILOptimization.FoldNone] && qilNode.XmlType == XmlQueryTypeFactory.None && this.AllowReplace(XmlILOptimization.FoldNone, local0))
			{
				return this.Replace(XmlILOptimization.FoldNone, local0, this.VisitNop(this.f.Nop(qilNode)));
			}
			if (this[XmlILOptimization.FoldNone] && qilNode2.XmlType == XmlQueryTypeFactory.None && this.AllowReplace(XmlILOptimization.FoldNone, local0))
			{
				return this.Replace(XmlILOptimization.FoldNone, local0, this.VisitNop(this.f.Nop(qilNode2)));
			}
			if (this[XmlILOptimization.EliminateModulo] && this.IsLiteral(qilNode) && this.IsLiteral(qilNode2) && this.CanFoldArithmetic(QilNodeType.Modulo, (QilLiteral)qilNode, (QilLiteral)qilNode2) && this.AllowReplace(XmlILOptimization.EliminateModulo, local0))
			{
				return this.Replace(XmlILOptimization.EliminateModulo, local0, this.FoldArithmetic(QilNodeType.Modulo, (QilLiteral)qilNode, (QilLiteral)qilNode2));
			}
			return this.NoReplace(local0);
		}

		// Token: 0x0600425A RID: 16986 RVA: 0x001635E8 File Offset: 0x001617E8
		protected override QilNode VisitStrLength(QilUnary local0)
		{
			QilNode qilNode = local0[0];
			if (this[XmlILOptimization.FoldNone] && qilNode.XmlType == XmlQueryTypeFactory.None && this.AllowReplace(XmlILOptimization.FoldNone, local0))
			{
				return this.Replace(XmlILOptimization.FoldNone, local0, this.VisitNop(this.f.Nop(qilNode)));
			}
			if (this[XmlILOptimization.EliminateStrLength] && qilNode.NodeType == QilNodeType.LiteralString)
			{
				string text = (string)((QilLiteral)qilNode).Value;
				if (this.AllowReplace(XmlILOptimization.EliminateStrLength, local0))
				{
					return this.Replace(XmlILOptimization.EliminateStrLength, local0, this.VisitLiteralInt32(this.f.LiteralInt32(text.Length)));
				}
			}
			return this.NoReplace(local0);
		}

		// Token: 0x0600425B RID: 16987 RVA: 0x00163694 File Offset: 0x00161894
		protected override QilNode VisitStrConcat(QilStrConcat local0)
		{
			QilNode qilNode = local0[0];
			QilNode qilNode2 = local0[1];
			if (this[XmlILOptimization.FoldNone] && qilNode.XmlType == XmlQueryTypeFactory.None && this.AllowReplace(XmlILOptimization.FoldNone, local0))
			{
				return this.Replace(XmlILOptimization.FoldNone, local0, this.VisitNop(this.f.Nop(qilNode)));
			}
			if (this[XmlILOptimization.FoldNone] && qilNode2.XmlType == XmlQueryTypeFactory.None && this.AllowReplace(XmlILOptimization.FoldNone, local0))
			{
				return this.Replace(XmlILOptimization.FoldNone, local0, this.VisitNop(this.f.Nop(qilNode2)));
			}
			if (qilNode2.XmlType.IsSingleton && this[XmlILOptimization.EliminateStrConcatSingle] && this.AllowReplace(XmlILOptimization.EliminateStrConcatSingle, local0))
			{
				return this.Replace(XmlILOptimization.EliminateStrConcatSingle, local0, this.VisitNop(this.f.Nop(qilNode2)));
			}
			if (this[XmlILOptimization.EliminateStrConcat] && qilNode.NodeType == QilNodeType.LiteralString)
			{
				string text = (string)((QilLiteral)qilNode).Value;
				if (qilNode2.NodeType == QilNodeType.Sequence && this.AreLiteralArgs(qilNode2) && this.AllowReplace(XmlILOptimization.EliminateStrConcat, local0))
				{
					StringConcat stringConcat = default(StringConcat);
					stringConcat.Delimiter = text;
					foreach (QilNode qilNode3 in qilNode2)
					{
						QilLiteral qilLiteral = (QilLiteral)qilNode3;
						stringConcat.Concat(qilLiteral);
					}
					return this.Replace(XmlILOptimization.EliminateStrConcat, local0, this.VisitLiteralString(this.f.LiteralString(stringConcat.GetResult())));
				}
			}
			return this.NoReplace(local0);
		}

		// Token: 0x0600425C RID: 16988 RVA: 0x00163840 File Offset: 0x00161A40
		protected override QilNode VisitStrParseQName(QilBinary local0)
		{
			QilNode qilNode = local0[0];
			QilNode qilNode2 = local0[1];
			if (this[XmlILOptimization.FoldNone] && qilNode.XmlType == XmlQueryTypeFactory.None && this.AllowReplace(XmlILOptimization.FoldNone, local0))
			{
				return this.Replace(XmlILOptimization.FoldNone, local0, this.VisitNop(this.f.Nop(qilNode)));
			}
			if (this[XmlILOptimization.FoldNone] && qilNode2.XmlType == XmlQueryTypeFactory.None && this.AllowReplace(XmlILOptimization.FoldNone, local0))
			{
				return this.Replace(XmlILOptimization.FoldNone, local0, this.VisitNop(this.f.Nop(qilNode2)));
			}
			return this.NoReplace(local0);
		}

		// Token: 0x0600425D RID: 16989 RVA: 0x001638E0 File Offset: 0x00161AE0
		protected override QilNode VisitNe(QilBinary local0)
		{
			QilNode qilNode = local0[0];
			QilNode qilNode2 = local0[1];
			if (this[XmlILOptimization.FoldNone] && qilNode.XmlType == XmlQueryTypeFactory.None && this.AllowReplace(XmlILOptimization.FoldNone, local0))
			{
				return this.Replace(XmlILOptimization.FoldNone, local0, this.VisitNop(this.f.Nop(qilNode)));
			}
			if (this[XmlILOptimization.FoldNone] && qilNode2.XmlType == XmlQueryTypeFactory.None && this.AllowReplace(XmlILOptimization.FoldNone, local0))
			{
				return this.Replace(XmlILOptimization.FoldNone, local0, this.VisitNop(this.f.Nop(qilNode2)));
			}
			if (this[XmlILOptimization.EliminateNe] && this.IsLiteral(qilNode) && this.IsLiteral(qilNode2) && this.AllowReplace(XmlILOptimization.EliminateNe, local0))
			{
				return this.Replace(XmlILOptimization.EliminateNe, local0, this.FoldComparison(QilNodeType.Ne, qilNode, qilNode2));
			}
			if (this[XmlILOptimization.NormalizeNeLiteral] && this.IsLiteral(qilNode) && !this.IsLiteral(qilNode2) && this.AllowReplace(XmlILOptimization.NormalizeNeLiteral, local0))
			{
				return this.Replace(XmlILOptimization.NormalizeNeLiteral, local0, this.VisitNe(this.f.Ne(qilNode2, qilNode)));
			}
			if (this[XmlILOptimization.NormalizeXsltConvertNe] && qilNode.NodeType == QilNodeType.XsltConvert)
			{
				QilNode qilNode3 = qilNode[0];
				QilNode qilNode4 = qilNode[1];
				if (qilNode4.NodeType == QilNodeType.LiteralType)
				{
					XmlQueryType xmlQueryType = (XmlQueryType)((QilLiteral)qilNode4).Value;
					if (this.IsPrimitiveNumeric(qilNode3.XmlType) && this.IsPrimitiveNumeric(xmlQueryType) && this.IsLiteral(qilNode2) && this.CanFoldXsltConvertNonLossy(qilNode2, qilNode3.XmlType) && this.AllowReplace(XmlILOptimization.NormalizeXsltConvertNe, local0))
					{
						return this.Replace(XmlILOptimization.NormalizeXsltConvertNe, local0, this.VisitNe(this.f.Ne(qilNode3, this.FoldXsltConvert(qilNode2, qilNode3.XmlType))));
					}
				}
			}
			if (this[XmlILOptimization.NormalizeIdNe] && qilNode.NodeType == QilNodeType.XsltGenerateId)
			{
				QilNode qilNode5 = qilNode[0];
				if (qilNode5.XmlType.IsSingleton && qilNode2.NodeType == QilNodeType.XsltGenerateId)
				{
					QilNode qilNode6 = qilNode2[0];
					if (qilNode6.XmlType.IsSingleton && this.AllowReplace(XmlILOptimization.NormalizeIdNe, local0))
					{
						return this.Replace(XmlILOptimization.NormalizeIdNe, local0, this.VisitNot(this.f.Not(this.VisitIs(this.f.Is(qilNode5, qilNode6)))));
					}
				}
			}
			if (this[XmlILOptimization.NormalizeLengthNe] && qilNode.NodeType == QilNodeType.Length)
			{
				QilNode qilNode7 = qilNode[0];
				if (qilNode2.NodeType == QilNodeType.LiteralInt32 && (int)((QilLiteral)qilNode2).Value == 0 && this.AllowReplace(XmlILOptimization.NormalizeLengthNe, local0))
				{
					return this.Replace(XmlILOptimization.NormalizeLengthNe, local0, this.VisitNot(this.f.Not(this.VisitIsEmpty(this.f.IsEmpty(qilNode7)))));
				}
			}
			if (this[XmlILOptimization.AnnotateMaxLengthNe] && qilNode.NodeType == QilNodeType.Length && qilNode2.NodeType == QilNodeType.LiteralInt32)
			{
				int num = (int)((QilLiteral)qilNode2).Value;
				if (this.AllowReplace(XmlILOptimization.AnnotateMaxLengthNe, local0))
				{
					OptimizerPatterns.Write(qilNode).AddPattern(OptimizerPatternName.MaxPosition);
					OptimizerPatterns.Write(qilNode).AddArgument(OptimizerPatternArgument.ElementQName, num);
				}
			}
			return this.NoReplace(local0);
		}

		// Token: 0x0600425E RID: 16990 RVA: 0x00163C10 File Offset: 0x00161E10
		protected override QilNode VisitEq(QilBinary local0)
		{
			QilNode qilNode = local0[0];
			QilNode qilNode2 = local0[1];
			if (this[XmlILOptimization.FoldNone] && qilNode.XmlType == XmlQueryTypeFactory.None && this.AllowReplace(XmlILOptimization.FoldNone, local0))
			{
				return this.Replace(XmlILOptimization.FoldNone, local0, this.VisitNop(this.f.Nop(qilNode)));
			}
			if (this[XmlILOptimization.FoldNone] && qilNode2.XmlType == XmlQueryTypeFactory.None && this.AllowReplace(XmlILOptimization.FoldNone, local0))
			{
				return this.Replace(XmlILOptimization.FoldNone, local0, this.VisitNop(this.f.Nop(qilNode2)));
			}
			if (this[XmlILOptimization.EliminateEq] && this.IsLiteral(qilNode) && this.IsLiteral(qilNode2) && this.AllowReplace(XmlILOptimization.EliminateEq, local0))
			{
				return this.Replace(XmlILOptimization.EliminateEq, local0, this.FoldComparison(QilNodeType.Eq, qilNode, qilNode2));
			}
			if (this[XmlILOptimization.NormalizeEqLiteral] && this.IsLiteral(qilNode) && !this.IsLiteral(qilNode2) && this.AllowReplace(XmlILOptimization.NormalizeEqLiteral, local0))
			{
				return this.Replace(XmlILOptimization.NormalizeEqLiteral, local0, this.VisitEq(this.f.Eq(qilNode2, qilNode)));
			}
			if (this[XmlILOptimization.NormalizeXsltConvertEq] && qilNode.NodeType == QilNodeType.XsltConvert)
			{
				QilNode qilNode3 = qilNode[0];
				QilNode qilNode4 = qilNode[1];
				if (qilNode4.NodeType == QilNodeType.LiteralType)
				{
					XmlQueryType xmlQueryType = (XmlQueryType)((QilLiteral)qilNode4).Value;
					if (this.IsPrimitiveNumeric(qilNode3.XmlType) && this.IsPrimitiveNumeric(xmlQueryType) && this.IsLiteral(qilNode2) && this.CanFoldXsltConvertNonLossy(qilNode2, qilNode3.XmlType) && this.AllowReplace(XmlILOptimization.NormalizeXsltConvertEq, local0))
					{
						return this.Replace(XmlILOptimization.NormalizeXsltConvertEq, local0, this.VisitEq(this.f.Eq(qilNode3, this.FoldXsltConvert(qilNode2, qilNode3.XmlType))));
					}
				}
			}
			if (this[XmlILOptimization.NormalizeAddEq] && qilNode.NodeType == QilNodeType.Add)
			{
				QilNode qilNode5 = qilNode[0];
				QilNode qilNode6 = qilNode[1];
				if (this.IsLiteral(qilNode6) && this.IsLiteral(qilNode2) && this.CanFoldArithmetic(QilNodeType.Subtract, (QilLiteral)qilNode2, (QilLiteral)qilNode6) && this.AllowReplace(XmlILOptimization.NormalizeAddEq, local0))
				{
					return this.Replace(XmlILOptimization.NormalizeAddEq, local0, this.VisitEq(this.f.Eq(qilNode5, this.FoldArithmetic(QilNodeType.Subtract, (QilLiteral)qilNode2, (QilLiteral)qilNode6))));
				}
			}
			if (this[XmlILOptimization.NormalizeIdEq] && qilNode.NodeType == QilNodeType.XsltGenerateId)
			{
				QilNode qilNode7 = qilNode[0];
				if (qilNode7.XmlType.IsSingleton && qilNode2.NodeType == QilNodeType.XsltGenerateId)
				{
					QilNode qilNode8 = qilNode2[0];
					if (qilNode8.XmlType.IsSingleton && this.AllowReplace(XmlILOptimization.NormalizeIdEq, local0))
					{
						return this.Replace(XmlILOptimization.NormalizeIdEq, local0, this.VisitIs(this.f.Is(qilNode7, qilNode8)));
					}
				}
			}
			if (this[XmlILOptimization.NormalizeIdEq] && qilNode.NodeType == QilNodeType.XsltGenerateId)
			{
				QilNode qilNode9 = qilNode[0];
				if (qilNode9.XmlType.IsSingleton && qilNode2.NodeType == QilNodeType.StrConcat)
				{
					QilNode qilNode10 = qilNode2[1];
					if (qilNode10.NodeType == QilNodeType.Loop)
					{
						QilNode qilNode11 = qilNode10[0];
						QilNode qilNode12 = qilNode10[1];
						if (qilNode11.NodeType == QilNodeType.For)
						{
							QilNode qilNode13 = qilNode11[0];
							if (!qilNode13.XmlType.MaybeMany && qilNode12.NodeType == QilNodeType.XsltGenerateId && qilNode12[0] == qilNode11 && this.AllowReplace(XmlILOptimization.NormalizeIdEq, local0))
							{
								QilNode qilNode14 = this.VisitFor(this.f.For(qilNode13));
								return this.Replace(XmlILOptimization.NormalizeIdEq, local0, this.VisitNot(this.f.Not(this.VisitIsEmpty(this.f.IsEmpty(this.VisitFilter(this.f.Filter(qilNode14, this.VisitIs(this.f.Is(qilNode9, qilNode14)))))))));
							}
						}
					}
				}
			}
			if (this[XmlILOptimization.NormalizeIdEq] && qilNode.NodeType == QilNodeType.StrConcat)
			{
				QilNode qilNode15 = qilNode[1];
				if (qilNode15.NodeType == QilNodeType.Loop)
				{
					QilNode qilNode16 = qilNode15[0];
					QilNode qilNode17 = qilNode15[1];
					if (qilNode16.NodeType == QilNodeType.For)
					{
						QilNode qilNode18 = qilNode16[0];
						if (!qilNode18.XmlType.MaybeMany && qilNode17.NodeType == QilNodeType.XsltGenerateId && qilNode17[0] == qilNode16 && qilNode2.NodeType == QilNodeType.XsltGenerateId)
						{
							QilNode qilNode19 = qilNode2[0];
							if (qilNode19.XmlType.IsSingleton && this.AllowReplace(XmlILOptimization.NormalizeIdEq, local0))
							{
								QilNode qilNode20 = this.VisitFor(this.f.For(qilNode18));
								return this.Replace(XmlILOptimization.NormalizeIdEq, local0, this.VisitNot(this.f.Not(this.VisitIsEmpty(this.f.IsEmpty(this.VisitFilter(this.f.Filter(qilNode20, this.VisitIs(this.f.Is(qilNode19, qilNode20)))))))));
							}
						}
					}
				}
			}
			if (this[XmlILOptimization.NormalizeMuenchian] && qilNode.NodeType == QilNodeType.Length)
			{
				QilNode qilNode21 = qilNode[0];
				if (qilNode21.NodeType == QilNodeType.Union)
				{
					QilNode qilNode22 = qilNode21[0];
					QilNode qilNode23 = qilNode21[1];
					if (qilNode22.XmlType.IsSingleton && !qilNode23.XmlType.MaybeMany && qilNode2.NodeType == QilNodeType.LiteralInt32 && (int)((QilLiteral)qilNode2).Value == 1 && this.AllowReplace(XmlILOptimization.NormalizeMuenchian, local0))
					{
						QilNode qilNode24 = this.VisitFor(this.f.For(qilNode23));
						return this.Replace(XmlILOptimization.NormalizeMuenchian, local0, this.VisitIsEmpty(this.f.IsEmpty(this.VisitFilter(this.f.Filter(qilNode24, this.VisitNot(this.f.Not(this.VisitIs(this.f.Is(qilNode22, qilNode24)))))))));
					}
				}
			}
			if (this[XmlILOptimization.NormalizeMuenchian] && qilNode.NodeType == QilNodeType.Length)
			{
				QilNode qilNode25 = qilNode[0];
				if (qilNode25.NodeType == QilNodeType.Union)
				{
					QilNode qilNode26 = qilNode25[0];
					QilNode qilNode27 = qilNode25[1];
					if (!qilNode26.XmlType.MaybeMany && qilNode27.XmlType.IsSingleton && qilNode2.NodeType == QilNodeType.LiteralInt32 && (int)((QilLiteral)qilNode2).Value == 1 && this.AllowReplace(XmlILOptimization.NormalizeMuenchian, local0))
					{
						QilNode qilNode28 = this.VisitFor(this.f.For(qilNode26));
						return this.Replace(XmlILOptimization.NormalizeMuenchian, local0, this.VisitIsEmpty(this.f.IsEmpty(this.VisitFilter(this.f.Filter(qilNode28, this.VisitNot(this.f.Not(this.VisitIs(this.f.Is(qilNode28, qilNode27)))))))));
					}
				}
			}
			if (this[XmlILOptimization.AnnotateMaxLengthEq] && qilNode.NodeType == QilNodeType.Length && qilNode2.NodeType == QilNodeType.LiteralInt32)
			{
				int num = (int)((QilLiteral)qilNode2).Value;
				if (this.AllowReplace(XmlILOptimization.AnnotateMaxLengthEq, local0))
				{
					OptimizerPatterns.Write(qilNode).AddPattern(OptimizerPatternName.MaxPosition);
					OptimizerPatterns.Write(qilNode).AddArgument(OptimizerPatternArgument.ElementQName, num);
				}
			}
			return this.NoReplace(local0);
		}

		// Token: 0x0600425F RID: 16991 RVA: 0x00164394 File Offset: 0x00162594
		protected override QilNode VisitGt(QilBinary local0)
		{
			QilNode qilNode = local0[0];
			QilNode qilNode2 = local0[1];
			if (this[XmlILOptimization.FoldNone] && qilNode.XmlType == XmlQueryTypeFactory.None && this.AllowReplace(XmlILOptimization.FoldNone, local0))
			{
				return this.Replace(XmlILOptimization.FoldNone, local0, this.VisitNop(this.f.Nop(qilNode)));
			}
			if (this[XmlILOptimization.FoldNone] && qilNode2.XmlType == XmlQueryTypeFactory.None && this.AllowReplace(XmlILOptimization.FoldNone, local0))
			{
				return this.Replace(XmlILOptimization.FoldNone, local0, this.VisitNop(this.f.Nop(qilNode2)));
			}
			if (this[XmlILOptimization.EliminateGt] && this.IsLiteral(qilNode) && this.IsLiteral(qilNode2) && this.AllowReplace(XmlILOptimization.EliminateGt, local0))
			{
				return this.Replace(XmlILOptimization.EliminateGt, local0, this.FoldComparison(QilNodeType.Gt, qilNode, qilNode2));
			}
			if (this[XmlILOptimization.NormalizeGtLiteral] && this.IsLiteral(qilNode) && !this.IsLiteral(qilNode2) && this.AllowReplace(XmlILOptimization.NormalizeGtLiteral, local0))
			{
				return this.Replace(XmlILOptimization.NormalizeGtLiteral, local0, this.VisitLt(this.f.Lt(qilNode2, qilNode)));
			}
			if (this[XmlILOptimization.NormalizeXsltConvertGt] && qilNode.NodeType == QilNodeType.XsltConvert)
			{
				QilNode qilNode3 = qilNode[0];
				QilNode qilNode4 = qilNode[1];
				if (qilNode4.NodeType == QilNodeType.LiteralType)
				{
					XmlQueryType xmlQueryType = (XmlQueryType)((QilLiteral)qilNode4).Value;
					if (this.IsPrimitiveNumeric(qilNode3.XmlType) && this.IsPrimitiveNumeric(xmlQueryType) && this.IsLiteral(qilNode2) && this.CanFoldXsltConvertNonLossy(qilNode2, qilNode3.XmlType) && this.AllowReplace(XmlILOptimization.NormalizeXsltConvertGt, local0))
					{
						return this.Replace(XmlILOptimization.NormalizeXsltConvertGt, local0, this.VisitGt(this.f.Gt(qilNode3, this.FoldXsltConvert(qilNode2, qilNode3.XmlType))));
					}
				}
			}
			if (this[XmlILOptimization.NormalizeLengthGt] && qilNode.NodeType == QilNodeType.Length)
			{
				QilNode qilNode5 = qilNode[0];
				if (qilNode2.NodeType == QilNodeType.LiteralInt32 && (int)((QilLiteral)qilNode2).Value == 0 && this.AllowReplace(XmlILOptimization.NormalizeLengthGt, local0))
				{
					return this.Replace(XmlILOptimization.NormalizeLengthGt, local0, this.VisitNot(this.f.Not(this.VisitIsEmpty(this.f.IsEmpty(qilNode5)))));
				}
			}
			if (this[XmlILOptimization.AnnotateMaxLengthGt] && qilNode.NodeType == QilNodeType.Length && qilNode2.NodeType == QilNodeType.LiteralInt32)
			{
				int num = (int)((QilLiteral)qilNode2).Value;
				if (this.AllowReplace(XmlILOptimization.AnnotateMaxLengthGt, local0))
				{
					OptimizerPatterns.Write(qilNode).AddPattern(OptimizerPatternName.MaxPosition);
					OptimizerPatterns.Write(qilNode).AddArgument(OptimizerPatternArgument.ElementQName, num);
				}
			}
			return this.NoReplace(local0);
		}

		// Token: 0x06004260 RID: 16992 RVA: 0x00164634 File Offset: 0x00162834
		protected override QilNode VisitGe(QilBinary local0)
		{
			QilNode qilNode = local0[0];
			QilNode qilNode2 = local0[1];
			if (this[XmlILOptimization.FoldNone] && qilNode.XmlType == XmlQueryTypeFactory.None && this.AllowReplace(XmlILOptimization.FoldNone, local0))
			{
				return this.Replace(XmlILOptimization.FoldNone, local0, this.VisitNop(this.f.Nop(qilNode)));
			}
			if (this[XmlILOptimization.FoldNone] && qilNode2.XmlType == XmlQueryTypeFactory.None && this.AllowReplace(XmlILOptimization.FoldNone, local0))
			{
				return this.Replace(XmlILOptimization.FoldNone, local0, this.VisitNop(this.f.Nop(qilNode2)));
			}
			if (this[XmlILOptimization.EliminateGe] && this.IsLiteral(qilNode) && this.IsLiteral(qilNode2) && this.AllowReplace(XmlILOptimization.EliminateGe, local0))
			{
				return this.Replace(XmlILOptimization.EliminateGe, local0, this.FoldComparison(QilNodeType.Ge, qilNode, qilNode2));
			}
			if (this[XmlILOptimization.NormalizeGeLiteral] && this.IsLiteral(qilNode) && !this.IsLiteral(qilNode2) && this.AllowReplace(XmlILOptimization.NormalizeGeLiteral, local0))
			{
				return this.Replace(XmlILOptimization.NormalizeGeLiteral, local0, this.VisitLe(this.f.Le(qilNode2, qilNode)));
			}
			if (this[XmlILOptimization.NormalizeXsltConvertGe] && qilNode.NodeType == QilNodeType.XsltConvert)
			{
				QilNode qilNode3 = qilNode[0];
				QilNode qilNode4 = qilNode[1];
				if (qilNode4.NodeType == QilNodeType.LiteralType)
				{
					XmlQueryType xmlQueryType = (XmlQueryType)((QilLiteral)qilNode4).Value;
					if (this.IsPrimitiveNumeric(qilNode3.XmlType) && this.IsPrimitiveNumeric(xmlQueryType) && this.IsLiteral(qilNode2) && this.CanFoldXsltConvertNonLossy(qilNode2, qilNode3.XmlType) && this.AllowReplace(XmlILOptimization.NormalizeXsltConvertGe, local0))
					{
						return this.Replace(XmlILOptimization.NormalizeXsltConvertGe, local0, this.VisitGe(this.f.Ge(qilNode3, this.FoldXsltConvert(qilNode2, qilNode3.XmlType))));
					}
				}
			}
			if (this[XmlILOptimization.AnnotateMaxLengthGe] && qilNode.NodeType == QilNodeType.Length && qilNode2.NodeType == QilNodeType.LiteralInt32)
			{
				int num = (int)((QilLiteral)qilNode2).Value;
				if (this.AllowReplace(XmlILOptimization.AnnotateMaxLengthGe, local0))
				{
					OptimizerPatterns.Write(qilNode).AddPattern(OptimizerPatternName.MaxPosition);
					OptimizerPatterns.Write(qilNode).AddArgument(OptimizerPatternArgument.ElementQName, num);
				}
			}
			return this.NoReplace(local0);
		}

		// Token: 0x06004261 RID: 16993 RVA: 0x00164864 File Offset: 0x00162A64
		protected override QilNode VisitLt(QilBinary local0)
		{
			QilNode qilNode = local0[0];
			QilNode qilNode2 = local0[1];
			if (this[XmlILOptimization.FoldNone] && qilNode.XmlType == XmlQueryTypeFactory.None && this.AllowReplace(XmlILOptimization.FoldNone, local0))
			{
				return this.Replace(XmlILOptimization.FoldNone, local0, this.VisitNop(this.f.Nop(qilNode)));
			}
			if (this[XmlILOptimization.FoldNone] && qilNode2.XmlType == XmlQueryTypeFactory.None && this.AllowReplace(XmlILOptimization.FoldNone, local0))
			{
				return this.Replace(XmlILOptimization.FoldNone, local0, this.VisitNop(this.f.Nop(qilNode2)));
			}
			if (this[XmlILOptimization.EliminateLt] && this.IsLiteral(qilNode) && this.IsLiteral(qilNode2) && this.AllowReplace(XmlILOptimization.EliminateLt, local0))
			{
				return this.Replace(XmlILOptimization.EliminateLt, local0, this.FoldComparison(QilNodeType.Lt, qilNode, qilNode2));
			}
			if (this[XmlILOptimization.NormalizeLtLiteral] && this.IsLiteral(qilNode) && !this.IsLiteral(qilNode2) && this.AllowReplace(XmlILOptimization.NormalizeLtLiteral, local0))
			{
				return this.Replace(XmlILOptimization.NormalizeLtLiteral, local0, this.VisitGt(this.f.Gt(qilNode2, qilNode)));
			}
			if (this[XmlILOptimization.NormalizeXsltConvertLt] && qilNode.NodeType == QilNodeType.XsltConvert)
			{
				QilNode qilNode3 = qilNode[0];
				QilNode qilNode4 = qilNode[1];
				if (qilNode4.NodeType == QilNodeType.LiteralType)
				{
					XmlQueryType xmlQueryType = (XmlQueryType)((QilLiteral)qilNode4).Value;
					if (this.IsPrimitiveNumeric(qilNode3.XmlType) && this.IsPrimitiveNumeric(xmlQueryType) && this.IsLiteral(qilNode2) && this.CanFoldXsltConvertNonLossy(qilNode2, qilNode3.XmlType) && this.AllowReplace(XmlILOptimization.NormalizeXsltConvertLt, local0))
					{
						return this.Replace(XmlILOptimization.NormalizeXsltConvertLt, local0, this.VisitLt(this.f.Lt(qilNode3, this.FoldXsltConvert(qilNode2, qilNode3.XmlType))));
					}
				}
			}
			if (this[XmlILOptimization.AnnotateMaxLengthLt] && qilNode.NodeType == QilNodeType.Length && qilNode2.NodeType == QilNodeType.LiteralInt32)
			{
				int num = (int)((QilLiteral)qilNode2).Value;
				if (this.AllowReplace(XmlILOptimization.AnnotateMaxLengthLt, local0))
				{
					OptimizerPatterns.Write(qilNode).AddPattern(OptimizerPatternName.MaxPosition);
					OptimizerPatterns.Write(qilNode).AddArgument(OptimizerPatternArgument.ElementQName, num);
				}
			}
			return this.NoReplace(local0);
		}

		// Token: 0x06004262 RID: 16994 RVA: 0x00164A94 File Offset: 0x00162C94
		protected override QilNode VisitLe(QilBinary local0)
		{
			QilNode qilNode = local0[0];
			QilNode qilNode2 = local0[1];
			if (this[XmlILOptimization.FoldNone] && qilNode.XmlType == XmlQueryTypeFactory.None && this.AllowReplace(XmlILOptimization.FoldNone, local0))
			{
				return this.Replace(XmlILOptimization.FoldNone, local0, this.VisitNop(this.f.Nop(qilNode)));
			}
			if (this[XmlILOptimization.FoldNone] && qilNode2.XmlType == XmlQueryTypeFactory.None && this.AllowReplace(XmlILOptimization.FoldNone, local0))
			{
				return this.Replace(XmlILOptimization.FoldNone, local0, this.VisitNop(this.f.Nop(qilNode2)));
			}
			if (this[XmlILOptimization.EliminateLe] && this.IsLiteral(qilNode) && this.IsLiteral(qilNode2) && this.AllowReplace(XmlILOptimization.EliminateLe, local0))
			{
				return this.Replace(XmlILOptimization.EliminateLe, local0, this.FoldComparison(QilNodeType.Le, qilNode, qilNode2));
			}
			if (this[XmlILOptimization.NormalizeLeLiteral] && this.IsLiteral(qilNode) && !this.IsLiteral(qilNode2) && this.AllowReplace(XmlILOptimization.NormalizeLeLiteral, local0))
			{
				return this.Replace(XmlILOptimization.NormalizeLeLiteral, local0, this.VisitGe(this.f.Ge(qilNode2, qilNode)));
			}
			if (this[XmlILOptimization.NormalizeXsltConvertLe] && qilNode.NodeType == QilNodeType.XsltConvert)
			{
				QilNode qilNode3 = qilNode[0];
				QilNode qilNode4 = qilNode[1];
				if (qilNode4.NodeType == QilNodeType.LiteralType)
				{
					XmlQueryType xmlQueryType = (XmlQueryType)((QilLiteral)qilNode4).Value;
					if (this.IsPrimitiveNumeric(qilNode3.XmlType) && this.IsPrimitiveNumeric(xmlQueryType) && this.IsLiteral(qilNode2) && this.CanFoldXsltConvertNonLossy(qilNode2, qilNode3.XmlType) && this.AllowReplace(XmlILOptimization.NormalizeXsltConvertLe, local0))
					{
						return this.Replace(XmlILOptimization.NormalizeXsltConvertLe, local0, this.VisitLe(this.f.Le(qilNode3, this.FoldXsltConvert(qilNode2, qilNode3.XmlType))));
					}
				}
			}
			if (this[XmlILOptimization.AnnotateMaxLengthLe] && qilNode.NodeType == QilNodeType.Length && qilNode2.NodeType == QilNodeType.LiteralInt32)
			{
				int num = (int)((QilLiteral)qilNode2).Value;
				if (this.AllowReplace(XmlILOptimization.AnnotateMaxLengthLe, local0))
				{
					OptimizerPatterns.Write(qilNode).AddPattern(OptimizerPatternName.MaxPosition);
					OptimizerPatterns.Write(qilNode).AddArgument(OptimizerPatternArgument.ElementQName, num);
				}
			}
			return this.NoReplace(local0);
		}

		// Token: 0x06004263 RID: 16995 RVA: 0x00164CC4 File Offset: 0x00162EC4
		protected override QilNode VisitIs(QilBinary local0)
		{
			QilNode qilNode = local0[0];
			QilNode qilNode2 = local0[1];
			if (this[XmlILOptimization.FoldNone] && qilNode.XmlType == XmlQueryTypeFactory.None && this.AllowReplace(XmlILOptimization.FoldNone, local0))
			{
				return this.Replace(XmlILOptimization.FoldNone, local0, this.VisitNop(this.f.Nop(qilNode)));
			}
			if (this[XmlILOptimization.FoldNone] && qilNode2.XmlType == XmlQueryTypeFactory.None && this.AllowReplace(XmlILOptimization.FoldNone, local0))
			{
				return this.Replace(XmlILOptimization.FoldNone, local0, this.VisitNop(this.f.Nop(qilNode2)));
			}
			if (this[XmlILOptimization.EliminateIs] && qilNode2 == qilNode && this.AllowReplace(XmlILOptimization.EliminateIs, local0))
			{
				return this.Replace(XmlILOptimization.EliminateIs, local0, this.VisitTrue(this.f.True()));
			}
			return this.NoReplace(local0);
		}

		// Token: 0x06004264 RID: 16996 RVA: 0x00164D98 File Offset: 0x00162F98
		protected override QilNode VisitAfter(QilBinary local0)
		{
			QilNode qilNode = local0[0];
			QilNode qilNode2 = local0[1];
			if (this[XmlILOptimization.FoldNone] && qilNode.XmlType == XmlQueryTypeFactory.None && this.AllowReplace(XmlILOptimization.FoldNone, local0))
			{
				return this.Replace(XmlILOptimization.FoldNone, local0, this.VisitNop(this.f.Nop(qilNode)));
			}
			if (this[XmlILOptimization.FoldNone] && qilNode2.XmlType == XmlQueryTypeFactory.None && this.AllowReplace(XmlILOptimization.FoldNone, local0))
			{
				return this.Replace(XmlILOptimization.FoldNone, local0, this.VisitNop(this.f.Nop(qilNode2)));
			}
			if (this[XmlILOptimization.EliminateAfter] && qilNode2 == qilNode && this.AllowReplace(XmlILOptimization.EliminateAfter, local0))
			{
				return this.Replace(XmlILOptimization.EliminateAfter, local0, this.VisitFalse(this.f.False()));
			}
			return this.NoReplace(local0);
		}

		// Token: 0x06004265 RID: 16997 RVA: 0x00164E6C File Offset: 0x0016306C
		protected override QilNode VisitBefore(QilBinary local0)
		{
			QilNode qilNode = local0[0];
			QilNode qilNode2 = local0[1];
			if (this[XmlILOptimization.FoldNone] && qilNode.XmlType == XmlQueryTypeFactory.None && this.AllowReplace(XmlILOptimization.FoldNone, local0))
			{
				return this.Replace(XmlILOptimization.FoldNone, local0, this.VisitNop(this.f.Nop(qilNode)));
			}
			if (this[XmlILOptimization.FoldNone] && qilNode2.XmlType == XmlQueryTypeFactory.None && this.AllowReplace(XmlILOptimization.FoldNone, local0))
			{
				return this.Replace(XmlILOptimization.FoldNone, local0, this.VisitNop(this.f.Nop(qilNode2)));
			}
			if (this[XmlILOptimization.EliminateBefore] && qilNode2 == qilNode && this.AllowReplace(XmlILOptimization.EliminateBefore, local0))
			{
				return this.Replace(XmlILOptimization.EliminateBefore, local0, this.VisitFalse(this.f.False()));
			}
			return this.NoReplace(local0);
		}

		// Token: 0x06004266 RID: 16998 RVA: 0x00164F40 File Offset: 0x00163140
		protected override QilNode VisitLoop(QilLoop local0)
		{
			QilNode qilNode = local0[0];
			QilNode qilNode2 = local0[1];
			if (this[XmlILOptimization.FoldNone] && qilNode.XmlType == XmlQueryTypeFactory.None && this.AllowReplace(XmlILOptimization.FoldNone, local0))
			{
				return this.Replace(XmlILOptimization.FoldNone, local0, this.VisitNop(this.f.Nop(qilNode[0])));
			}
			if (this[XmlILOptimization.EliminateIterator] && qilNode.NodeType == QilNodeType.For)
			{
				QilNode qilNode3 = qilNode[0];
				if (qilNode3.NodeType == QilNodeType.For && !OptimizerPatterns.Read(qilNode).MatchesPattern(OptimizerPatternName.IsPositional) && this.AllowReplace(XmlILOptimization.EliminateIterator, local0))
				{
					return this.Replace(XmlILOptimization.EliminateIterator, local0, this.Subs(qilNode2, qilNode, qilNode3));
				}
			}
			if (this[XmlILOptimization.EliminateLoop] && qilNode.NodeType == QilNodeType.For)
			{
				QilNode qilNode4 = qilNode[0];
				if (qilNode4.NodeType == QilNodeType.Sequence && qilNode4.Count == 0 && this.AllowReplace(XmlILOptimization.EliminateLoop, local0))
				{
					return this.Replace(XmlILOptimization.EliminateLoop, local0, this.VisitSequence(this.f.Sequence()));
				}
			}
			if (this[XmlILOptimization.EliminateLoop] && !OptimizerPatterns.Read(qilNode).MatchesPattern(OptimizerPatternName.MaybeSideEffects) && qilNode2.NodeType == QilNodeType.Sequence && qilNode2.Count == 0 && this.AllowReplace(XmlILOptimization.EliminateLoop, local0))
			{
				return this.Replace(XmlILOptimization.EliminateLoop, local0, this.VisitSequence(this.f.Sequence()));
			}
			if (this[XmlILOptimization.EliminateLoop] && qilNode2 == qilNode && this.AllowReplace(XmlILOptimization.EliminateLoop, local0))
			{
				return this.Replace(XmlILOptimization.EliminateLoop, local0, qilNode[0]);
			}
			if (this[XmlILOptimization.NormalizeLoopText] && qilNode.NodeType == QilNodeType.For && qilNode[0].XmlType.IsSingleton && qilNode2.NodeType == QilNodeType.TextCtor)
			{
				QilNode qilNode5 = qilNode2[0];
				if (this.AllowReplace(XmlILOptimization.NormalizeLoopText, local0))
				{
					return this.Replace(XmlILOptimization.NormalizeLoopText, local0, this.VisitTextCtor(this.f.TextCtor(this.VisitLoop(this.f.Loop(qilNode, qilNode5)))));
				}
			}
			if (this[XmlILOptimization.EliminateIteratorUsedAtMostOnce] && (qilNode.NodeType == QilNodeType.Let || qilNode[0].XmlType.IsSingleton) && !OptimizerPatterns.Read(qilNode).MatchesPattern(OptimizerPatternName.MaybeSideEffects) && this.nodeCounter.Count(qilNode2, qilNode) <= 1 && this.AllowReplace(XmlILOptimization.EliminateIteratorUsedAtMostOnce, local0))
			{
				return this.Replace(XmlILOptimization.EliminateIteratorUsedAtMostOnce, local0, this.Subs(qilNode2, qilNode, qilNode[0]));
			}
			if (this[XmlILOptimization.NormalizeLoopConditional] && qilNode2.NodeType == QilNodeType.Conditional)
			{
				QilNode qilNode6 = qilNode2[0];
				QilNode qilNode7 = qilNode2[1];
				QilNode qilNode8 = qilNode2[2];
				if (qilNode7.NodeType == QilNodeType.Sequence && qilNode7.Count == 0 && qilNode8 == qilNode && this.AllowReplace(XmlILOptimization.NormalizeLoopConditional, local0))
				{
					return this.Replace(XmlILOptimization.NormalizeLoopConditional, local0, this.VisitFilter(this.f.Filter(qilNode, this.VisitNot(this.f.Not(qilNode6)))));
				}
			}
			if (this[XmlILOptimization.NormalizeLoopConditional] && qilNode2.NodeType == QilNodeType.Conditional)
			{
				QilNode qilNode9 = qilNode2[0];
				QilNode qilNode10 = qilNode2[1];
				QilNode qilNode11 = qilNode2[2];
				if (qilNode10 == qilNode && qilNode11.NodeType == QilNodeType.Sequence && qilNode11.Count == 0 && this.AllowReplace(XmlILOptimization.NormalizeLoopConditional, local0))
				{
					return this.Replace(XmlILOptimization.NormalizeLoopConditional, local0, this.VisitFilter(this.f.Filter(qilNode, qilNode9)));
				}
			}
			if (this[XmlILOptimization.NormalizeLoopConditional] && qilNode.NodeType == QilNodeType.For && qilNode2.NodeType == QilNodeType.Conditional)
			{
				QilNode qilNode12 = qilNode2[0];
				QilNode qilNode13 = qilNode2[1];
				QilNode qilNode14 = qilNode2[2];
				if (qilNode13.NodeType == QilNodeType.Sequence && qilNode13.Count == 0 && this.NonPositional(qilNode14, qilNode) && this.AllowReplace(XmlILOptimization.NormalizeLoopConditional, local0))
				{
					QilNode qilNode15 = this.VisitFor(this.f.For(this.VisitFilter(this.f.Filter(qilNode, this.VisitNot(this.f.Not(qilNode12))))));
					return this.Replace(XmlILOptimization.NormalizeLoopConditional, local0, this.VisitLoop(this.f.Loop(qilNode15, this.Subs(qilNode14, qilNode, qilNode15))));
				}
			}
			if (this[XmlILOptimization.NormalizeLoopConditional] && qilNode.NodeType == QilNodeType.For && qilNode2.NodeType == QilNodeType.Conditional)
			{
				QilNode qilNode16 = qilNode2[0];
				QilNode qilNode17 = qilNode2[1];
				QilNode qilNode18 = qilNode2[2];
				if (this.NonPositional(qilNode17, qilNode) && qilNode18.NodeType == QilNodeType.Sequence && qilNode18.Count == 0 && this.AllowReplace(XmlILOptimization.NormalizeLoopConditional, local0))
				{
					QilNode qilNode19 = this.VisitFor(this.f.For(this.VisitFilter(this.f.Filter(qilNode, qilNode16))));
					return this.Replace(XmlILOptimization.NormalizeLoopConditional, local0, this.VisitLoop(this.f.Loop(qilNode19, this.Subs(qilNode17, qilNode, qilNode19))));
				}
			}
			if (this[XmlILOptimization.NormalizeLoopLoop] && qilNode2.NodeType == QilNodeType.Loop)
			{
				QilNode qilNode20 = qilNode2[0];
				QilNode qilNode21 = qilNode2[1];
				if (qilNode20.NodeType == QilNodeType.For)
				{
					QilNode qilNode22 = qilNode20[0];
					if (!this.DependsOn(qilNode21, qilNode) && this.NonPositional(qilNode21, qilNode20) && this.AllowReplace(XmlILOptimization.NormalizeLoopLoop, local0))
					{
						QilNode qilNode23 = this.VisitFor(this.f.For(this.VisitLoop(this.f.Loop(qilNode, qilNode22))));
						return this.Replace(XmlILOptimization.NormalizeLoopLoop, local0, this.VisitLoop(this.f.Loop(qilNode23, this.Subs(qilNode21, qilNode20, qilNode23))));
					}
				}
			}
			if (this[XmlILOptimization.AnnotateSingletonLoop] && qilNode.NodeType == QilNodeType.For && !qilNode[0].XmlType.MaybeMany && this.AllowReplace(XmlILOptimization.AnnotateSingletonLoop, local0))
			{
				OptimizerPatterns.Inherit(qilNode2, local0, OptimizerPatternName.IsDocOrderDistinct);
				OptimizerPatterns.Inherit(qilNode2, local0, OptimizerPatternName.SameDepth);
			}
			if (this[XmlILOptimization.AnnotateRootLoop] && this.IsStepPattern(qilNode2, QilNodeType.Root) && this.AllowReplace(XmlILOptimization.AnnotateRootLoop, local0))
			{
				OptimizerPatterns.Write(local0).AddPattern(OptimizerPatternName.SameDepth);
			}
			if (this[XmlILOptimization.AnnotateContentLoop] && qilNode.NodeType == QilNodeType.For)
			{
				QilNode qilNode24 = qilNode[0];
				if (OptimizerPatterns.Read(qilNode24).MatchesPattern(OptimizerPatternName.SameDepth) && (this.IsStepPattern(qilNode2, QilNodeType.Content) || this.IsStepPattern(qilNode2, QilNodeType.Union)) && qilNode == OptimizerPatterns.Read(qilNode2).GetArgument(OptimizerPatternArgument.StepInput) && this.AllowReplace(XmlILOptimization.AnnotateContentLoop, local0))
				{
					OptimizerPatterns.Write(local0).AddPattern(OptimizerPatternName.SameDepth);
					OptimizerPatterns.Inherit(qilNode24, local0, OptimizerPatternName.IsDocOrderDistinct);
				}
			}
			if (this[XmlILOptimization.AnnotateAttrNmspLoop] && qilNode.NodeType == QilNodeType.For)
			{
				QilNode qilNode25 = qilNode[0];
				if ((this.IsStepPattern(qilNode2, QilNodeType.Attribute) || this.IsStepPattern(qilNode2, QilNodeType.XPathNamespace) || OptimizerPatterns.Read(qilNode2).MatchesPattern(OptimizerPatternName.FilterAttributeKind)) && qilNode == OptimizerPatterns.Read(qilNode2).GetArgument(OptimizerPatternArgument.StepInput) && this.AllowReplace(XmlILOptimization.AnnotateAttrNmspLoop, local0))
				{
					OptimizerPatterns.Inherit(qilNode25, local0, OptimizerPatternName.SameDepth);
					OptimizerPatterns.Inherit(qilNode25, local0, OptimizerPatternName.IsDocOrderDistinct);
				}
			}
			if (this[XmlILOptimization.AnnotateDescendantLoop] && qilNode.NodeType == QilNodeType.For)
			{
				QilNode qilNode26 = qilNode[0];
				if (OptimizerPatterns.Read(qilNode26).MatchesPattern(OptimizerPatternName.SameDepth) && (this.IsStepPattern(qilNode2, QilNodeType.Descendant) || this.IsStepPattern(qilNode2, QilNodeType.DescendantOrSelf)) && qilNode == OptimizerPatterns.Read(qilNode2).GetArgument(OptimizerPatternArgument.StepInput) && this.AllowReplace(XmlILOptimization.AnnotateDescendantLoop, local0))
				{
					OptimizerPatterns.Inherit(qilNode26, local0, OptimizerPatternName.IsDocOrderDistinct);
				}
			}
			return this.NoReplace(local0);
		}

		// Token: 0x06004267 RID: 16999 RVA: 0x00165688 File Offset: 0x00163888
		protected override QilNode VisitFilter(QilLoop local0)
		{
			QilNode qilNode = local0[0];
			QilNode qilNode2 = local0[1];
			if (this[XmlILOptimization.FoldNone] && qilNode.XmlType == XmlQueryTypeFactory.None && this.AllowReplace(XmlILOptimization.FoldNone, local0))
			{
				return this.Replace(XmlILOptimization.FoldNone, local0, this.VisitNop(this.f.Nop(qilNode[0])));
			}
			if (this[XmlILOptimization.FoldNone] && qilNode2.XmlType == XmlQueryTypeFactory.None && this.AllowReplace(XmlILOptimization.FoldNone, local0))
			{
				return this.Replace(XmlILOptimization.FoldNone, local0, this.VisitLoop(this.f.Loop(qilNode, qilNode2)));
			}
			if (this[XmlILOptimization.EliminateFilter] && !OptimizerPatterns.Read(qilNode).MatchesPattern(OptimizerPatternName.MaybeSideEffects) && qilNode2.NodeType == QilNodeType.False && this.AllowReplace(XmlILOptimization.EliminateFilter, local0))
			{
				return this.Replace(XmlILOptimization.EliminateFilter, local0, this.VisitSequence(this.f.Sequence()));
			}
			if (this[XmlILOptimization.EliminateFilter] && qilNode2.NodeType == QilNodeType.True && this.AllowReplace(XmlILOptimization.EliminateFilter, local0))
			{
				return this.Replace(XmlILOptimization.EliminateFilter, local0, qilNode[0]);
			}
			if (this[XmlILOptimization.NormalizeAttribute] && qilNode.NodeType == QilNodeType.For)
			{
				QilNode qilNode3 = qilNode[0];
				if (qilNode3.NodeType == QilNodeType.Content)
				{
					QilNode qilNode4 = qilNode3[0];
					if (qilNode2.NodeType == QilNodeType.And)
					{
						QilNode qilNode5 = qilNode2[0];
						QilNode qilNode6 = qilNode2[1];
						if (qilNode5.NodeType == QilNodeType.IsType)
						{
							QilNode qilNode7 = qilNode5[0];
							QilNode qilNode8 = qilNode5[1];
							if (qilNode7 == qilNode && qilNode8.NodeType == QilNodeType.LiteralType && (XmlQueryType)((QilLiteral)qilNode8).Value == XmlQueryTypeFactory.Attribute && qilNode6.NodeType == QilNodeType.Eq)
							{
								QilNode qilNode9 = qilNode6[0];
								QilNode qilNode10 = qilNode6[1];
								if (qilNode9.NodeType == QilNodeType.NameOf && qilNode9[0] == qilNode && qilNode10.NodeType == QilNodeType.LiteralQName && this.AllowReplace(XmlILOptimization.NormalizeAttribute, local0))
								{
									return this.Replace(XmlILOptimization.NormalizeAttribute, local0, this.VisitAttribute(this.f.Attribute(qilNode4, qilNode10)));
								}
							}
						}
					}
				}
			}
			if (this[XmlILOptimization.CommuteFilterLoop] && qilNode.NodeType == QilNodeType.For)
			{
				QilNode qilNode11 = qilNode[0];
				if (qilNode11.NodeType == QilNodeType.Loop)
				{
					QilNode qilNode12 = qilNode11[0];
					QilNode qilNode13 = qilNode11[1];
					if (this.NonPositional(qilNode2, qilNode) && !this.IsDocOrderDistinct(qilNode11) && this.AllowReplace(XmlILOptimization.CommuteFilterLoop, local0))
					{
						QilNode qilNode14 = this.VisitFor(this.f.For(qilNode13));
						return this.Replace(XmlILOptimization.CommuteFilterLoop, local0, this.VisitLoop(this.f.Loop(qilNode12, this.VisitFilter(this.f.Filter(qilNode14, this.Subs(qilNode2, qilNode, qilNode14))))));
					}
				}
			}
			if (this[XmlILOptimization.NormalizeLoopInvariant] && !OptimizerPatterns.Read(qilNode).MatchesPattern(OptimizerPatternName.MaybeSideEffects) && qilNode[0].NodeType != QilNodeType.OptimizeBarrier && !this.DependsOn(qilNode2, qilNode) && !OptimizerPatterns.Read(qilNode2).MatchesPattern(OptimizerPatternName.MaybeSideEffects) && this.AllowReplace(XmlILOptimization.NormalizeLoopInvariant, local0))
			{
				return this.Replace(XmlILOptimization.NormalizeLoopInvariant, local0, this.VisitConditional(this.f.Conditional(qilNode2, qilNode[0], this.VisitSequence(this.f.Sequence()))));
			}
			if (this[XmlILOptimization.AnnotateMaxPositionEq] && qilNode2.NodeType == QilNodeType.Eq)
			{
				QilNode qilNode15 = qilNode2[0];
				QilNode qilNode16 = qilNode2[1];
				if (qilNode15.NodeType == QilNodeType.PositionOf && qilNode15[0] == qilNode && qilNode16.NodeType == QilNodeType.LiteralInt32)
				{
					int num = (int)((QilLiteral)qilNode16).Value;
					if (this.AllowReplace(XmlILOptimization.AnnotateMaxPositionEq, local0))
					{
						OptimizerPatterns.Write(qilNode).AddPattern(OptimizerPatternName.MaxPosition);
						OptimizerPatterns.Write(qilNode).AddArgument(OptimizerPatternArgument.ElementQName, num);
					}
				}
			}
			if (this[XmlILOptimization.AnnotateMaxPositionLe] && qilNode2.NodeType == QilNodeType.Le)
			{
				QilNode qilNode17 = qilNode2[0];
				QilNode qilNode18 = qilNode2[1];
				if (qilNode17.NodeType == QilNodeType.PositionOf && qilNode17[0] == qilNode && qilNode18.NodeType == QilNodeType.LiteralInt32)
				{
					int num2 = (int)((QilLiteral)qilNode18).Value;
					if (this.AllowReplace(XmlILOptimization.AnnotateMaxPositionLe, local0))
					{
						OptimizerPatterns.Write(qilNode).AddPattern(OptimizerPatternName.MaxPosition);
						OptimizerPatterns.Write(qilNode).AddArgument(OptimizerPatternArgument.ElementQName, num2);
					}
				}
			}
			if (this[XmlILOptimization.AnnotateMaxPositionLt] && qilNode2.NodeType == QilNodeType.Lt)
			{
				QilNode qilNode19 = qilNode2[0];
				QilNode qilNode20 = qilNode2[1];
				if (qilNode19.NodeType == QilNodeType.PositionOf && qilNode19[0] == qilNode && qilNode20.NodeType == QilNodeType.LiteralInt32)
				{
					int num3 = (int)((QilLiteral)qilNode20).Value;
					if (this.AllowReplace(XmlILOptimization.AnnotateMaxPositionLt, local0))
					{
						OptimizerPatterns.Write(qilNode).AddPattern(OptimizerPatternName.MaxPosition);
						OptimizerPatterns.Write(qilNode).AddArgument(OptimizerPatternArgument.ElementQName, num3 - 1);
					}
				}
			}
			if (this[XmlILOptimization.AnnotateFilter] && qilNode.NodeType == QilNodeType.For)
			{
				QilNode qilNode21 = qilNode[0];
				if (this.AllowReplace(XmlILOptimization.AnnotateFilter, local0))
				{
					OptimizerPatterns.Inherit(qilNode21, local0, OptimizerPatternName.Step);
					OptimizerPatterns.Inherit(qilNode21, local0, OptimizerPatternName.IsDocOrderDistinct);
					OptimizerPatterns.Inherit(qilNode21, local0, OptimizerPatternName.SameDepth);
				}
			}
			if (this[XmlILOptimization.AnnotateFilterElements] && qilNode.NodeType == QilNodeType.For && OptimizerPatterns.Read(qilNode[0]).MatchesPattern(OptimizerPatternName.Axis) && qilNode2.NodeType == QilNodeType.And)
			{
				QilNode qilNode22 = qilNode2[0];
				QilNode qilNode23 = qilNode2[1];
				if (qilNode22.NodeType == QilNodeType.IsType)
				{
					QilNode qilNode24 = qilNode22[0];
					QilNode qilNode25 = qilNode22[1];
					if (qilNode24 == qilNode && qilNode25.NodeType == QilNodeType.LiteralType && (XmlQueryType)((QilLiteral)qilNode25).Value == XmlQueryTypeFactory.Element && qilNode23.NodeType == QilNodeType.Eq)
					{
						QilNode qilNode26 = qilNode23[0];
						QilNode qilNode27 = qilNode23[1];
						if (qilNode26.NodeType == QilNodeType.NameOf && qilNode26[0] == qilNode && qilNode27.NodeType == QilNodeType.LiteralQName && this.AllowReplace(XmlILOptimization.AnnotateFilterElements, local0))
						{
							OptimizerPatterns.Write(local0).AddPattern(OptimizerPatternName.FilterElements);
							OptimizerPatterns.Write(local0).AddArgument(OptimizerPatternArgument.ElementQName, qilNode27);
						}
					}
				}
			}
			if (this[XmlILOptimization.AnnotateFilterContentKind] && qilNode.NodeType == QilNodeType.For && OptimizerPatterns.Read(qilNode[0]).MatchesPattern(OptimizerPatternName.Axis) && qilNode2.NodeType == QilNodeType.IsType)
			{
				QilNode qilNode28 = qilNode2[0];
				QilNode qilNode29 = qilNode2[1];
				if (qilNode28 == qilNode && qilNode29.NodeType == QilNodeType.LiteralType)
				{
					XmlQueryType xmlQueryType = (XmlQueryType)((QilLiteral)qilNode29).Value;
					if (this.MatchesContentTest(xmlQueryType) && this.AllowReplace(XmlILOptimization.AnnotateFilterContentKind, local0))
					{
						OptimizerPatterns.Write(local0).AddPattern(OptimizerPatternName.FilterContentKind);
						OptimizerPatterns.Write(local0).AddArgument(OptimizerPatternArgument.ElementQName, xmlQueryType);
					}
				}
			}
			if (this[XmlILOptimization.AnnotateFilterAttributeKind] && qilNode.NodeType == QilNodeType.For && qilNode[0].NodeType == QilNodeType.Content && qilNode2.NodeType == QilNodeType.IsType)
			{
				QilNode qilNode30 = qilNode2[0];
				QilNode qilNode31 = qilNode2[1];
				if (qilNode30 == qilNode && qilNode31.NodeType == QilNodeType.LiteralType && (XmlQueryType)((QilLiteral)qilNode31).Value == XmlQueryTypeFactory.Attribute && this.AllowReplace(XmlILOptimization.AnnotateFilterAttributeKind, local0))
				{
					OptimizerPatterns.Write(local0).AddPattern(OptimizerPatternName.FilterAttributeKind);
				}
			}
			return this.NoReplace(local0);
		}

		// Token: 0x06004268 RID: 17000 RVA: 0x00165DE8 File Offset: 0x00163FE8
		protected override QilNode VisitSort(QilLoop local0)
		{
			QilNode qilNode = local0[0];
			QilNode qilNode2 = local0[1];
			if (this[XmlILOptimization.FoldNone] && qilNode.XmlType == XmlQueryTypeFactory.None && this.AllowReplace(XmlILOptimization.FoldNone, local0))
			{
				return this.Replace(XmlILOptimization.FoldNone, local0, this.VisitNop(this.f.Nop(qilNode[0])));
			}
			if (this[XmlILOptimization.EliminateSort] && qilNode.NodeType == QilNodeType.For)
			{
				QilNode qilNode3 = qilNode[0];
				if (qilNode3.XmlType.IsSingleton && this.AllowReplace(XmlILOptimization.EliminateSort, local0))
				{
					return this.Replace(XmlILOptimization.EliminateSort, local0, this.VisitNop(this.f.Nop(qilNode3)));
				}
			}
			return this.NoReplace(local0);
		}

		// Token: 0x06004269 RID: 17001 RVA: 0x00165EA0 File Offset: 0x001640A0
		protected override QilNode VisitSortKey(QilSortKey local0)
		{
			QilNode qilNode = local0[0];
			QilNode qilNode2 = local0[1];
			if (this[XmlILOptimization.NormalizeSortXsltConvert] && qilNode.NodeType == QilNodeType.XsltConvert)
			{
				QilNode qilNode3 = qilNode[0];
				QilNode qilNode4 = qilNode[1];
				if (qilNode4.NodeType == QilNodeType.LiteralType)
				{
					XmlQueryType xmlQueryType = (XmlQueryType)((QilLiteral)qilNode4).Value;
					if (qilNode3.XmlType == XmlQueryTypeFactory.IntX && xmlQueryType == XmlQueryTypeFactory.DoubleX && this.AllowReplace(XmlILOptimization.NormalizeSortXsltConvert, local0))
					{
						return this.Replace(XmlILOptimization.NormalizeSortXsltConvert, local0, this.VisitSortKey(this.f.SortKey(qilNode3, qilNode2)));
					}
				}
			}
			return this.NoReplace(local0);
		}

		// Token: 0x0600426A RID: 17002 RVA: 0x00165F58 File Offset: 0x00164158
		protected override QilNode VisitDocOrderDistinct(QilUnary local0)
		{
			QilNode qilNode = local0[0];
			if (this[XmlILOptimization.FoldNone] && qilNode.XmlType == XmlQueryTypeFactory.None && this.AllowReplace(XmlILOptimization.FoldNone, local0))
			{
				return this.Replace(XmlILOptimization.FoldNone, local0, this.VisitNop(this.f.Nop(qilNode)));
			}
			if (this[XmlILOptimization.EliminateDod] && this.IsDocOrderDistinct(qilNode) && this.AllowReplace(XmlILOptimization.EliminateDod, local0))
			{
				return this.Replace(XmlILOptimization.EliminateDod, local0, qilNode);
			}
			if (this[XmlILOptimization.FoldNamedDescendants] && qilNode.NodeType == QilNodeType.Loop)
			{
				QilNode qilNode2 = qilNode[0];
				QilNode qilNode3 = qilNode[1];
				if (qilNode2.NodeType == QilNodeType.For)
				{
					QilNode qilNode4 = qilNode2[0];
					if (qilNode4.NodeType == QilNodeType.Loop)
					{
						QilNode qilNode5 = qilNode4[0];
						QilNode qilNode6 = qilNode4[1];
						if (qilNode6.NodeType == QilNodeType.DescendantOrSelf)
						{
							QilNode qilNode7 = qilNode6[0];
							if (qilNode3.NodeType == QilNodeType.Filter)
							{
								QilNode qilNode8 = qilNode3[0];
								QilNode qilNode9 = qilNode3[1];
								if ((OptimizerPatterns.Read(qilNode3).MatchesPattern(OptimizerPatternName.FilterElements) || OptimizerPatterns.Read(qilNode3).MatchesPattern(OptimizerPatternName.FilterContentKind)) && this.IsStepPattern(qilNode3, QilNodeType.Content) && this.AllowReplace(XmlILOptimization.FoldNamedDescendants, local0))
								{
									QilNode qilNode10 = this.VisitFor(this.f.For(this.VisitDescendant(this.f.Descendant(qilNode7))));
									return this.Replace(XmlILOptimization.FoldNamedDescendants, local0, this.VisitDocOrderDistinct(this.f.DocOrderDistinct(this.VisitLoop(this.f.Loop(qilNode5, this.VisitFilter(this.f.Filter(qilNode10, this.Subs(qilNode9, qilNode8, qilNode10))))))));
								}
							}
						}
					}
				}
			}
			if (this[XmlILOptimization.FoldNamedDescendants] && qilNode.NodeType == QilNodeType.Loop)
			{
				QilNode qilNode11 = qilNode[0];
				QilNode qilNode12 = qilNode[1];
				if (qilNode11.NodeType == QilNodeType.For)
				{
					QilNode qilNode13 = qilNode11[0];
					if (qilNode13.NodeType == QilNodeType.DescendantOrSelf)
					{
						QilNode qilNode14 = qilNode13[0];
						if (qilNode12.NodeType == QilNodeType.Filter)
						{
							QilNode qilNode15 = qilNode12[0];
							QilNode qilNode16 = qilNode12[1];
							if ((OptimizerPatterns.Read(qilNode12).MatchesPattern(OptimizerPatternName.FilterElements) || OptimizerPatterns.Read(qilNode12).MatchesPattern(OptimizerPatternName.FilterContentKind)) && this.IsStepPattern(qilNode12, QilNodeType.Content) && this.AllowReplace(XmlILOptimization.FoldNamedDescendants, local0))
							{
								QilNode qilNode17 = this.VisitFor(this.f.For(this.VisitDescendant(this.f.Descendant(qilNode14))));
								return this.Replace(XmlILOptimization.FoldNamedDescendants, local0, this.VisitFilter(this.f.Filter(qilNode17, this.Subs(qilNode16, qilNode15, qilNode17))));
							}
						}
					}
				}
			}
			if (this[XmlILOptimization.CommuteDodFilter] && qilNode.NodeType == QilNodeType.Filter)
			{
				QilNode qilNode18 = qilNode[0];
				QilNode qilNode19 = qilNode[1];
				if (qilNode18.NodeType == QilNodeType.For)
				{
					QilNode qilNode20 = qilNode18[0];
					if (!OptimizerPatterns.Read(qilNode18).MatchesPattern(OptimizerPatternName.IsPositional) && !OptimizerPatterns.Read(qilNode).MatchesPattern(OptimizerPatternName.FilterElements) && !OptimizerPatterns.Read(qilNode).MatchesPattern(OptimizerPatternName.FilterContentKind) && !OptimizerPatterns.Read(qilNode).MatchesPattern(OptimizerPatternName.FilterAttributeKind) && this.AllowReplace(XmlILOptimization.CommuteDodFilter, local0))
					{
						QilNode qilNode21 = this.VisitFor(this.f.For(this.VisitDocOrderDistinct(this.f.DocOrderDistinct(qilNode20))));
						return this.Replace(XmlILOptimization.CommuteDodFilter, local0, this.VisitFilter(this.f.Filter(qilNode21, this.Subs(qilNode19, qilNode18, qilNode21))));
					}
				}
			}
			if (this[XmlILOptimization.CommuteDodFilter] && qilNode.NodeType == QilNodeType.Loop)
			{
				QilNode qilNode22 = qilNode[0];
				QilNode qilNode23 = qilNode[1];
				if (qilNode23.NodeType == QilNodeType.Filter)
				{
					QilNode qilNode24 = qilNode23[0];
					QilNode qilNode25 = qilNode23[1];
					if (qilNode24.NodeType == QilNodeType.For)
					{
						QilNode qilNode26 = qilNode24[0];
						if (!OptimizerPatterns.Read(qilNode24).MatchesPattern(OptimizerPatternName.IsPositional) && !this.DependsOn(qilNode25, qilNode22) && !OptimizerPatterns.Read(qilNode23).MatchesPattern(OptimizerPatternName.FilterElements) && !OptimizerPatterns.Read(qilNode23).MatchesPattern(OptimizerPatternName.FilterContentKind) && !OptimizerPatterns.Read(qilNode23).MatchesPattern(OptimizerPatternName.FilterAttributeKind) && this.AllowReplace(XmlILOptimization.CommuteDodFilter, local0))
						{
							QilNode qilNode27 = this.VisitFor(this.f.For(this.VisitDocOrderDistinct(this.f.DocOrderDistinct(this.VisitLoop(this.f.Loop(qilNode22, qilNode26))))));
							return this.Replace(XmlILOptimization.CommuteDodFilter, local0, this.VisitFilter(this.f.Filter(qilNode27, this.Subs(qilNode25, qilNode24, qilNode27))));
						}
					}
				}
			}
			if (this[XmlILOptimization.IntroduceDod] && qilNode.NodeType == QilNodeType.Loop)
			{
				QilNode qilNode28 = qilNode[0];
				QilNode qilNode29 = qilNode[1];
				if (qilNode28.NodeType == QilNodeType.For)
				{
					QilNode qilNode30 = qilNode28[0];
					if (!this.IsDocOrderDistinct(qilNode30) && !OptimizerPatterns.Read(qilNode28).MatchesPattern(OptimizerPatternName.IsPositional) && qilNode30.XmlType.IsSubtypeOf(XmlQueryTypeFactory.NodeNotRtfS) && !OptimizerPatterns.Read(qilNode).MatchesPattern(OptimizerPatternName.FilterElements) && !OptimizerPatterns.Read(qilNode).MatchesPattern(OptimizerPatternName.FilterContentKind) && !OptimizerPatterns.Read(qilNode).MatchesPattern(OptimizerPatternName.FilterAttributeKind) && this.AllowReplace(XmlILOptimization.IntroduceDod, local0))
					{
						QilNode qilNode31 = this.VisitFor(this.f.For(this.VisitDocOrderDistinct(this.f.DocOrderDistinct(qilNode30))));
						return this.Replace(XmlILOptimization.IntroduceDod, local0, this.VisitDocOrderDistinct(this.f.DocOrderDistinct(this.VisitLoop(this.f.Loop(qilNode31, this.Subs(qilNode29, qilNode28, qilNode31))))));
					}
				}
			}
			if (this[XmlILOptimization.IntroducePrecedingDod] && qilNode.NodeType == QilNodeType.Loop)
			{
				QilNode qilNode32 = qilNode[0];
				QilNode qilNode33 = qilNode[1];
				if (!this.IsDocOrderDistinct(qilNode33) && this.IsStepPattern(qilNode33, QilNodeType.PrecedingSibling) && this.AllowReplace(XmlILOptimization.IntroducePrecedingDod, local0))
				{
					return this.Replace(XmlILOptimization.IntroducePrecedingDod, local0, this.VisitDocOrderDistinct(this.f.DocOrderDistinct(this.VisitLoop(this.f.Loop(qilNode32, this.VisitDocOrderDistinct(this.f.DocOrderDistinct(qilNode33)))))));
				}
			}
			if (this[XmlILOptimization.EliminateReturnDod] && qilNode.NodeType == QilNodeType.Loop)
			{
				QilNode qilNode34 = qilNode[0];
				QilNode qilNode35 = qilNode[1];
				if (qilNode35.NodeType == QilNodeType.DocOrderDistinct)
				{
					QilNode qilNode36 = qilNode35[0];
					if (!this.IsStepPattern(qilNode36, QilNodeType.PrecedingSibling) && this.AllowReplace(XmlILOptimization.EliminateReturnDod, local0))
					{
						return this.Replace(XmlILOptimization.EliminateReturnDod, local0, this.VisitDocOrderDistinct(this.f.DocOrderDistinct(this.VisitLoop(this.f.Loop(qilNode34, qilNode36)))));
					}
				}
			}
			if (this[XmlILOptimization.AnnotateDod] && this.AllowReplace(XmlILOptimization.AnnotateDod, local0))
			{
				OptimizerPatterns.Write(local0).AddPattern(OptimizerPatternName.IsDocOrderDistinct);
				OptimizerPatterns.Inherit(qilNode, local0, OptimizerPatternName.SameDepth);
			}
			if (this[XmlILOptimization.AnnotateDodReverse] && this.AllowDodReverse(qilNode) && this.AllowReplace(XmlILOptimization.AnnotateDodReverse, local0))
			{
				OptimizerPatterns.Write(local0).AddPattern(OptimizerPatternName.DodReverse);
				OptimizerPatterns.Write(local0).AddArgument(OptimizerPatternArgument.ElementQName, qilNode);
			}
			if (this[XmlILOptimization.AnnotateJoinAndDod] && qilNode.NodeType == QilNodeType.Loop)
			{
				QilNode qilNode37 = qilNode[0];
				QilNode qilNode38 = qilNode[1];
				if (qilNode37.NodeType == QilNodeType.For)
				{
					QilNode qilNode39 = qilNode37[0];
					if (this.IsDocOrderDistinct(qilNode39) && this.AllowJoinAndDod(qilNode38) && qilNode37 == OptimizerPatterns.Read(qilNode38).GetArgument(OptimizerPatternArgument.StepInput) && this.AllowReplace(XmlILOptimization.AnnotateJoinAndDod, local0))
					{
						OptimizerPatterns.Write(local0).AddPattern(OptimizerPatternName.JoinAndDod);
						OptimizerPatterns.Write(local0).AddArgument(OptimizerPatternArgument.ElementQName, qilNode38);
					}
				}
			}
			if (this[XmlILOptimization.AnnotateDodMerge] && qilNode.NodeType == QilNodeType.Loop)
			{
				QilNode qilNode40 = qilNode[1];
				if (qilNode40.NodeType == QilNodeType.Invoke && this.IsDocOrderDistinct(qilNode40) && this.AllowReplace(XmlILOptimization.AnnotateDodMerge, local0))
				{
					OptimizerPatterns.Write(local0).AddPattern(OptimizerPatternName.DodMerge);
				}
			}
			return this.NoReplace(local0);
		}

		// Token: 0x0600426B RID: 17003 RVA: 0x00166764 File Offset: 0x00164964
		protected override QilNode VisitFunction(QilFunction local0)
		{
			QilNode qilNode = local0[0];
			QilNode qilNode2 = local0[1];
			QilNode qilNode3 = local0[2];
			XmlQueryType xmlType = local0.XmlType;
			if (local0.XmlType.IsSubtypeOf(XmlQueryTypeFactory.NodeS) && this[XmlILOptimization.AnnotateIndex1] && qilNode.Count == 2 && qilNode[0].XmlType.IsSubtypeOf(XmlQueryTypeFactory.Node) && qilNode[1].XmlType == XmlQueryTypeFactory.StringX && qilNode2.NodeType == QilNodeType.Filter)
			{
				QilNode qilNode4 = qilNode2[0];
				QilNode qilNode5 = qilNode2[1];
				if (qilNode4.NodeType == QilNodeType.For)
				{
					QilNode qilNode6 = qilNode4[0];
					if (qilNode5.NodeType == QilNodeType.Not)
					{
						QilNode qilNode7 = qilNode5[0];
						if (qilNode7.NodeType == QilNodeType.IsEmpty)
						{
							QilNode qilNode8 = qilNode7[0];
							if (qilNode8.NodeType == QilNodeType.Filter)
							{
								QilNode qilNode9 = qilNode8[0];
								QilNode qilNode10 = qilNode8[1];
								if (qilNode9.NodeType == QilNodeType.For)
								{
									QilNode qilNode11 = qilNode9[0];
									if (qilNode10.NodeType == QilNodeType.Eq)
									{
										QilNode qilNode12 = qilNode10[0];
										QilNode qilNode13 = qilNode10[1];
										if (qilNode12 == qilNode9 && qilNode13.NodeType == QilNodeType.Parameter && qilNode13 == qilNode[1] && this.IsDocOrderDistinct(qilNode2) && this.AllowReplace(XmlILOptimization.AnnotateIndex1, local0))
										{
											XmlILOptimizerVisitor.EqualityIndexVisitor equalityIndexVisitor = new XmlILOptimizerVisitor.EqualityIndexVisitor();
											if (equalityIndexVisitor.Scan(qilNode6, qilNode[0], qilNode13) && equalityIndexVisitor.Scan(qilNode11, qilNode[0], qilNode13))
											{
												OptimizerPatterns optimizerPatterns = OptimizerPatterns.Write(qilNode2);
												optimizerPatterns.AddPattern(OptimizerPatternName.EqualityIndex);
												optimizerPatterns.AddArgument(OptimizerPatternArgument.StepNode, qilNode4);
												optimizerPatterns.AddArgument(OptimizerPatternArgument.StepInput, qilNode11);
											}
										}
									}
								}
							}
						}
					}
				}
			}
			if (local0.XmlType.IsSubtypeOf(XmlQueryTypeFactory.NodeS) && this[XmlILOptimization.AnnotateIndex2] && qilNode.Count == 2 && qilNode[0].XmlType == XmlQueryTypeFactory.Node && qilNode[1].XmlType == XmlQueryTypeFactory.StringX && qilNode2.NodeType == QilNodeType.Filter)
			{
				QilNode qilNode14 = qilNode2[0];
				QilNode qilNode15 = qilNode2[1];
				if (qilNode14.NodeType == QilNodeType.For)
				{
					QilNode qilNode16 = qilNode14[0];
					if (qilNode15.NodeType == QilNodeType.Eq)
					{
						QilNode qilNode17 = qilNode15[0];
						QilNode qilNode18 = qilNode15[1];
						if (qilNode18.NodeType == QilNodeType.Parameter && qilNode18 == qilNode[1] && this.IsDocOrderDistinct(qilNode2) && this.AllowReplace(XmlILOptimization.AnnotateIndex2, local0))
						{
							XmlILOptimizerVisitor.EqualityIndexVisitor equalityIndexVisitor2 = new XmlILOptimizerVisitor.EqualityIndexVisitor();
							if (equalityIndexVisitor2.Scan(qilNode16, qilNode[0], qilNode18) && equalityIndexVisitor2.Scan(qilNode17, qilNode[0], qilNode18))
							{
								OptimizerPatterns optimizerPatterns2 = OptimizerPatterns.Write(qilNode2);
								optimizerPatterns2.AddPattern(OptimizerPatternName.EqualityIndex);
								optimizerPatterns2.AddArgument(OptimizerPatternArgument.StepNode, qilNode14);
								optimizerPatterns2.AddArgument(OptimizerPatternArgument.StepInput, qilNode17);
							}
						}
					}
				}
			}
			return this.NoReplace(local0);
		}

		// Token: 0x0600426C RID: 17004 RVA: 0x00166A68 File Offset: 0x00164C68
		protected override QilNode VisitInvoke(QilInvoke local0)
		{
			QilNode qilNode = local0[0];
			QilNode qilNode2 = local0[1];
			if (this[XmlILOptimization.NormalizeInvokeEmpty] && qilNode.NodeType == QilNodeType.Function)
			{
				QilNode qilNode3 = qilNode[1];
				if (qilNode3.NodeType == QilNodeType.Sequence && qilNode3.Count == 0 && this.AllowReplace(XmlILOptimization.NormalizeInvokeEmpty, local0))
				{
					return this.Replace(XmlILOptimization.NormalizeInvokeEmpty, local0, this.VisitSequence(this.f.Sequence()));
				}
			}
			if (this[XmlILOptimization.AnnotateTrackCallers] && this.AllowReplace(XmlILOptimization.AnnotateTrackCallers, local0))
			{
				XmlILConstructInfo.Write(qilNode).CallersInfo.Add(XmlILConstructInfo.Write(local0));
			}
			if (this[XmlILOptimization.AnnotateInvoke] && qilNode.NodeType == QilNodeType.Function)
			{
				QilNode qilNode4 = qilNode[1];
				if (this.AllowReplace(XmlILOptimization.AnnotateInvoke, local0))
				{
					OptimizerPatterns.Inherit(qilNode4, local0, OptimizerPatternName.IsDocOrderDistinct);
					OptimizerPatterns.Inherit(qilNode4, local0, OptimizerPatternName.SameDepth);
				}
			}
			return this.NoReplace(local0);
		}

		// Token: 0x0600426D RID: 17005 RVA: 0x00166B44 File Offset: 0x00164D44
		protected override QilNode VisitContent(QilUnary local0)
		{
			QilNode qilNode = local0[0];
			if (this[XmlILOptimization.FoldNone] && qilNode.XmlType == XmlQueryTypeFactory.None && this.AllowReplace(XmlILOptimization.FoldNone, local0))
			{
				return this.Replace(XmlILOptimization.FoldNone, local0, this.VisitNop(this.f.Nop(qilNode)));
			}
			if (this[XmlILOptimization.AnnotateContent] && this.AllowReplace(XmlILOptimization.AnnotateContent, local0))
			{
				this.AddStepPattern(local0, qilNode);
				OptimizerPatterns.Write(local0).AddPattern(OptimizerPatternName.Axis);
				OptimizerPatterns.Write(local0).AddPattern(OptimizerPatternName.IsDocOrderDistinct);
				OptimizerPatterns.Write(local0).AddPattern(OptimizerPatternName.SameDepth);
			}
			return this.NoReplace(local0);
		}

		// Token: 0x0600426E RID: 17006 RVA: 0x00166BE4 File Offset: 0x00164DE4
		protected override QilNode VisitAttribute(QilBinary local0)
		{
			QilNode qilNode = local0[0];
			QilNode qilNode2 = local0[1];
			if (this[XmlILOptimization.FoldNone] && qilNode.XmlType == XmlQueryTypeFactory.None && this.AllowReplace(XmlILOptimization.FoldNone, local0))
			{
				return this.Replace(XmlILOptimization.FoldNone, local0, this.VisitNop(this.f.Nop(qilNode)));
			}
			if (this[XmlILOptimization.FoldNone] && qilNode2.XmlType == XmlQueryTypeFactory.None && this.AllowReplace(XmlILOptimization.FoldNone, local0))
			{
				return this.Replace(XmlILOptimization.FoldNone, local0, this.VisitNop(this.f.Nop(qilNode2)));
			}
			if (this[XmlILOptimization.AnnotateAttribute] && this.AllowReplace(XmlILOptimization.AnnotateAttribute, local0))
			{
				OptimizerPatterns.Write(local0).AddPattern(OptimizerPatternName.Axis);
				this.AddStepPattern(local0, qilNode);
				OptimizerPatterns.Write(local0).AddPattern(OptimizerPatternName.IsDocOrderDistinct);
				OptimizerPatterns.Write(local0).AddPattern(OptimizerPatternName.SameDepth);
			}
			return this.NoReplace(local0);
		}

		// Token: 0x0600426F RID: 17007 RVA: 0x00166CC8 File Offset: 0x00164EC8
		protected override QilNode VisitParent(QilUnary local0)
		{
			QilNode qilNode = local0[0];
			if (this[XmlILOptimization.FoldNone] && qilNode.XmlType == XmlQueryTypeFactory.None && this.AllowReplace(XmlILOptimization.FoldNone, local0))
			{
				return this.Replace(XmlILOptimization.FoldNone, local0, this.VisitNop(this.f.Nop(qilNode)));
			}
			if (this[XmlILOptimization.AnnotateParent] && this.AllowReplace(XmlILOptimization.AnnotateParent, local0))
			{
				OptimizerPatterns.Write(local0).AddPattern(OptimizerPatternName.Axis);
				this.AddStepPattern(local0, qilNode);
				OptimizerPatterns.Write(local0).AddPattern(OptimizerPatternName.IsDocOrderDistinct);
				OptimizerPatterns.Write(local0).AddPattern(OptimizerPatternName.SameDepth);
			}
			return this.NoReplace(local0);
		}

		// Token: 0x06004270 RID: 17008 RVA: 0x00166D68 File Offset: 0x00164F68
		protected override QilNode VisitRoot(QilUnary local0)
		{
			QilNode qilNode = local0[0];
			if (this[XmlILOptimization.FoldNone] && qilNode.XmlType == XmlQueryTypeFactory.None && this.AllowReplace(XmlILOptimization.FoldNone, local0))
			{
				return this.Replace(XmlILOptimization.FoldNone, local0, this.VisitNop(this.f.Nop(qilNode)));
			}
			if (this[XmlILOptimization.AnnotateRoot] && this.AllowReplace(XmlILOptimization.AnnotateRoot, local0))
			{
				OptimizerPatterns.Write(local0).AddPattern(OptimizerPatternName.Axis);
				this.AddStepPattern(local0, qilNode);
				OptimizerPatterns.Write(local0).AddPattern(OptimizerPatternName.IsDocOrderDistinct);
				OptimizerPatterns.Write(local0).AddPattern(OptimizerPatternName.SameDepth);
			}
			return this.NoReplace(local0);
		}

		// Token: 0x06004271 RID: 17009 RVA: 0x00166E08 File Offset: 0x00165008
		protected override QilNode VisitDescendant(QilUnary local0)
		{
			QilNode qilNode = local0[0];
			if (this[XmlILOptimization.FoldNone] && qilNode.XmlType == XmlQueryTypeFactory.None && this.AllowReplace(XmlILOptimization.FoldNone, local0))
			{
				return this.Replace(XmlILOptimization.FoldNone, local0, this.VisitNop(this.f.Nop(qilNode)));
			}
			if (this[XmlILOptimization.AnnotateDescendant] && this.AllowReplace(XmlILOptimization.AnnotateDescendant, local0))
			{
				OptimizerPatterns.Write(local0).AddPattern(OptimizerPatternName.Axis);
				this.AddStepPattern(local0, qilNode);
				OptimizerPatterns.Write(local0).AddPattern(OptimizerPatternName.IsDocOrderDistinct);
			}
			return this.NoReplace(local0);
		}

		// Token: 0x06004272 RID: 17010 RVA: 0x00166E98 File Offset: 0x00165098
		protected override QilNode VisitDescendantOrSelf(QilUnary local0)
		{
			QilNode qilNode = local0[0];
			if (this[XmlILOptimization.FoldNone] && qilNode.XmlType == XmlQueryTypeFactory.None && this.AllowReplace(XmlILOptimization.FoldNone, local0))
			{
				return this.Replace(XmlILOptimization.FoldNone, local0, this.VisitNop(this.f.Nop(qilNode)));
			}
			if (this[XmlILOptimization.AnnotateDescendantSelf] && this.AllowReplace(XmlILOptimization.AnnotateDescendantSelf, local0))
			{
				OptimizerPatterns.Write(local0).AddPattern(OptimizerPatternName.Axis);
				this.AddStepPattern(local0, qilNode);
				OptimizerPatterns.Write(local0).AddPattern(OptimizerPatternName.IsDocOrderDistinct);
			}
			return this.NoReplace(local0);
		}

		// Token: 0x06004273 RID: 17011 RVA: 0x00166F28 File Offset: 0x00165128
		protected override QilNode VisitAncestor(QilUnary local0)
		{
			QilNode qilNode = local0[0];
			if (this[XmlILOptimization.FoldNone] && qilNode.XmlType == XmlQueryTypeFactory.None && this.AllowReplace(XmlILOptimization.FoldNone, local0))
			{
				return this.Replace(XmlILOptimization.FoldNone, local0, this.VisitNop(this.f.Nop(qilNode)));
			}
			if (this[XmlILOptimization.AnnotateAncestor] && this.AllowReplace(XmlILOptimization.AnnotateAncestor, local0))
			{
				OptimizerPatterns.Write(local0).AddPattern(OptimizerPatternName.Axis);
				this.AddStepPattern(local0, qilNode);
			}
			return this.NoReplace(local0);
		}

		// Token: 0x06004274 RID: 17012 RVA: 0x00166FAC File Offset: 0x001651AC
		protected override QilNode VisitAncestorOrSelf(QilUnary local0)
		{
			QilNode qilNode = local0[0];
			if (this[XmlILOptimization.FoldNone] && qilNode.XmlType == XmlQueryTypeFactory.None && this.AllowReplace(XmlILOptimization.FoldNone, local0))
			{
				return this.Replace(XmlILOptimization.FoldNone, local0, this.VisitNop(this.f.Nop(qilNode)));
			}
			if (this[XmlILOptimization.AnnotateAncestorSelf] && this.AllowReplace(XmlILOptimization.AnnotateAncestorSelf, local0))
			{
				OptimizerPatterns.Write(local0).AddPattern(OptimizerPatternName.Axis);
				this.AddStepPattern(local0, qilNode);
			}
			return this.NoReplace(local0);
		}

		// Token: 0x06004275 RID: 17013 RVA: 0x00167030 File Offset: 0x00165230
		protected override QilNode VisitPreceding(QilUnary local0)
		{
			QilNode qilNode = local0[0];
			if (this[XmlILOptimization.FoldNone] && qilNode.XmlType == XmlQueryTypeFactory.None && this.AllowReplace(XmlILOptimization.FoldNone, local0))
			{
				return this.Replace(XmlILOptimization.FoldNone, local0, this.VisitNop(this.f.Nop(qilNode)));
			}
			if (this[XmlILOptimization.AnnotatePreceding] && this.AllowReplace(XmlILOptimization.AnnotatePreceding, local0))
			{
				OptimizerPatterns.Write(local0).AddPattern(OptimizerPatternName.Axis);
				this.AddStepPattern(local0, qilNode);
			}
			return this.NoReplace(local0);
		}

		// Token: 0x06004276 RID: 17014 RVA: 0x001670B4 File Offset: 0x001652B4
		protected override QilNode VisitFollowingSibling(QilUnary local0)
		{
			QilNode qilNode = local0[0];
			if (this[XmlILOptimization.FoldNone] && qilNode.XmlType == XmlQueryTypeFactory.None && this.AllowReplace(XmlILOptimization.FoldNone, local0))
			{
				return this.Replace(XmlILOptimization.FoldNone, local0, this.VisitNop(this.f.Nop(qilNode)));
			}
			if (this[XmlILOptimization.AnnotateFollowingSibling] && this.AllowReplace(XmlILOptimization.AnnotateFollowingSibling, local0))
			{
				OptimizerPatterns.Write(local0).AddPattern(OptimizerPatternName.Axis);
				this.AddStepPattern(local0, qilNode);
				OptimizerPatterns.Write(local0).AddPattern(OptimizerPatternName.IsDocOrderDistinct);
				OptimizerPatterns.Write(local0).AddPattern(OptimizerPatternName.SameDepth);
			}
			return this.NoReplace(local0);
		}

		// Token: 0x06004277 RID: 17015 RVA: 0x00167154 File Offset: 0x00165354
		protected override QilNode VisitPrecedingSibling(QilUnary local0)
		{
			QilNode qilNode = local0[0];
			if (this[XmlILOptimization.FoldNone] && qilNode.XmlType == XmlQueryTypeFactory.None && this.AllowReplace(XmlILOptimization.FoldNone, local0))
			{
				return this.Replace(XmlILOptimization.FoldNone, local0, this.VisitNop(this.f.Nop(qilNode)));
			}
			if (this[XmlILOptimization.AnnotatePrecedingSibling] && this.AllowReplace(XmlILOptimization.AnnotatePrecedingSibling, local0))
			{
				OptimizerPatterns.Write(local0).AddPattern(OptimizerPatternName.Axis);
				this.AddStepPattern(local0, qilNode);
				OptimizerPatterns.Write(local0).AddPattern(OptimizerPatternName.SameDepth);
			}
			return this.NoReplace(local0);
		}

		// Token: 0x06004278 RID: 17016 RVA: 0x001671E8 File Offset: 0x001653E8
		protected override QilNode VisitNodeRange(QilBinary local0)
		{
			QilNode qilNode = local0[0];
			QilNode qilNode2 = local0[1];
			if (this[XmlILOptimization.FoldNone] && qilNode.XmlType == XmlQueryTypeFactory.None && this.AllowReplace(XmlILOptimization.FoldNone, local0))
			{
				return this.Replace(XmlILOptimization.FoldNone, local0, this.VisitNop(this.f.Nop(qilNode)));
			}
			if (this[XmlILOptimization.FoldNone] && qilNode2.XmlType == XmlQueryTypeFactory.None && this.AllowReplace(XmlILOptimization.FoldNone, local0))
			{
				return this.Replace(XmlILOptimization.FoldNone, local0, this.VisitNop(this.f.Nop(qilNode2)));
			}
			if (this[XmlILOptimization.AnnotateNodeRange] && this.AllowReplace(XmlILOptimization.AnnotateNodeRange, local0))
			{
				OptimizerPatterns.Write(local0).AddPattern(OptimizerPatternName.Axis);
				this.AddStepPattern(local0, qilNode);
				OptimizerPatterns.Write(local0).AddPattern(OptimizerPatternName.IsDocOrderDistinct);
			}
			return this.NoReplace(local0);
		}

		// Token: 0x06004279 RID: 17017 RVA: 0x001672C0 File Offset: 0x001654C0
		protected override QilNode VisitDeref(QilBinary local0)
		{
			QilNode qilNode = local0[0];
			QilNode qilNode2 = local0[1];
			if (this[XmlILOptimization.FoldNone] && qilNode.XmlType == XmlQueryTypeFactory.None && this.AllowReplace(XmlILOptimization.FoldNone, local0))
			{
				return this.Replace(XmlILOptimization.FoldNone, local0, this.VisitNop(this.f.Nop(qilNode)));
			}
			if (this[XmlILOptimization.FoldNone] && qilNode2.XmlType == XmlQueryTypeFactory.None && this.AllowReplace(XmlILOptimization.FoldNone, local0))
			{
				return this.Replace(XmlILOptimization.FoldNone, local0, this.VisitNop(this.f.Nop(qilNode2)));
			}
			return this.NoReplace(local0);
		}

		// Token: 0x0600427A RID: 17018 RVA: 0x00167360 File Offset: 0x00165560
		protected override QilNode VisitElementCtor(QilBinary local0)
		{
			QilNode qilNode = local0[0];
			QilNode qilNode2 = local0[1];
			if (this[XmlILOptimization.FoldNone] && qilNode.XmlType == XmlQueryTypeFactory.None && this.AllowReplace(XmlILOptimization.FoldNone, local0))
			{
				return this.Replace(XmlILOptimization.FoldNone, local0, this.VisitNop(this.f.Nop(qilNode)));
			}
			if (this[XmlILOptimization.FoldNone] && qilNode2.XmlType == XmlQueryTypeFactory.None && this.AllowReplace(XmlILOptimization.FoldNone, local0))
			{
				return this.Replace(XmlILOptimization.FoldNone, local0, this.VisitNop(this.f.Nop(qilNode2)));
			}
			if (this[XmlILOptimization.AnnotateConstruction] && this.AllowReplace(XmlILOptimization.AnnotateConstruction, local0))
			{
				local0.Right = this.elemAnalyzer.Analyze(local0, qilNode2);
			}
			return this.NoReplace(local0);
		}

		// Token: 0x0600427B RID: 17019 RVA: 0x00167428 File Offset: 0x00165628
		protected override QilNode VisitAttributeCtor(QilBinary local0)
		{
			QilNode qilNode = local0[0];
			QilNode qilNode2 = local0[1];
			if (this[XmlILOptimization.FoldNone] && qilNode.XmlType == XmlQueryTypeFactory.None && this.AllowReplace(XmlILOptimization.FoldNone, local0))
			{
				return this.Replace(XmlILOptimization.FoldNone, local0, this.VisitNop(this.f.Nop(qilNode)));
			}
			if (this[XmlILOptimization.FoldNone] && qilNode2.XmlType == XmlQueryTypeFactory.None && this.AllowReplace(XmlILOptimization.FoldNone, local0))
			{
				return this.Replace(XmlILOptimization.FoldNone, local0, this.VisitNop(this.f.Nop(qilNode2)));
			}
			if (this[XmlILOptimization.AnnotateConstruction] && this.AllowReplace(XmlILOptimization.AnnotateConstruction, local0))
			{
				local0.Right = this.contentAnalyzer.Analyze(local0, qilNode2);
			}
			return this.NoReplace(local0);
		}

		// Token: 0x0600427C RID: 17020 RVA: 0x001674F0 File Offset: 0x001656F0
		protected override QilNode VisitCommentCtor(QilUnary local0)
		{
			QilNode qilNode = local0[0];
			if (this[XmlILOptimization.FoldNone] && qilNode.XmlType == XmlQueryTypeFactory.None && this.AllowReplace(XmlILOptimization.FoldNone, local0))
			{
				return this.Replace(XmlILOptimization.FoldNone, local0, this.VisitNop(this.f.Nop(qilNode)));
			}
			if (this[XmlILOptimization.AnnotateConstruction] && this.AllowReplace(XmlILOptimization.AnnotateConstruction, local0))
			{
				local0.Child = this.contentAnalyzer.Analyze(local0, qilNode);
			}
			return this.NoReplace(local0);
		}

		// Token: 0x0600427D RID: 17021 RVA: 0x00167570 File Offset: 0x00165770
		protected override QilNode VisitPICtor(QilBinary local0)
		{
			QilNode qilNode = local0[0];
			QilNode qilNode2 = local0[1];
			if (this[XmlILOptimization.FoldNone] && qilNode.XmlType == XmlQueryTypeFactory.None && this.AllowReplace(XmlILOptimization.FoldNone, local0))
			{
				return this.Replace(XmlILOptimization.FoldNone, local0, this.VisitNop(this.f.Nop(qilNode)));
			}
			if (this[XmlILOptimization.FoldNone] && qilNode2.XmlType == XmlQueryTypeFactory.None && this.AllowReplace(XmlILOptimization.FoldNone, local0))
			{
				return this.Replace(XmlILOptimization.FoldNone, local0, this.VisitNop(this.f.Nop(qilNode2)));
			}
			if (this[XmlILOptimization.AnnotateConstruction] && this.AllowReplace(XmlILOptimization.AnnotateConstruction, local0))
			{
				local0.Right = this.contentAnalyzer.Analyze(local0, qilNode2);
			}
			return this.NoReplace(local0);
		}

		// Token: 0x0600427E RID: 17022 RVA: 0x00167638 File Offset: 0x00165838
		protected override QilNode VisitTextCtor(QilUnary local0)
		{
			QilNode qilNode = local0[0];
			if (this[XmlILOptimization.FoldNone] && qilNode.XmlType == XmlQueryTypeFactory.None && this.AllowReplace(XmlILOptimization.FoldNone, local0))
			{
				return this.Replace(XmlILOptimization.FoldNone, local0, this.VisitNop(this.f.Nop(qilNode)));
			}
			if (this[XmlILOptimization.AnnotateConstruction] && this.AllowReplace(XmlILOptimization.AnnotateConstruction, local0))
			{
				this.contentAnalyzer.Analyze(local0, null);
			}
			return this.NoReplace(local0);
		}

		// Token: 0x0600427F RID: 17023 RVA: 0x001676B4 File Offset: 0x001658B4
		protected override QilNode VisitRawTextCtor(QilUnary local0)
		{
			QilNode qilNode = local0[0];
			if (this[XmlILOptimization.FoldNone] && qilNode.XmlType == XmlQueryTypeFactory.None && this.AllowReplace(XmlILOptimization.FoldNone, local0))
			{
				return this.Replace(XmlILOptimization.FoldNone, local0, this.VisitNop(this.f.Nop(qilNode)));
			}
			if (this[XmlILOptimization.AnnotateConstruction] && this.AllowReplace(XmlILOptimization.AnnotateConstruction, local0))
			{
				this.contentAnalyzer.Analyze(local0, null);
			}
			return this.NoReplace(local0);
		}

		// Token: 0x06004280 RID: 17024 RVA: 0x00167730 File Offset: 0x00165930
		protected override QilNode VisitDocumentCtor(QilUnary local0)
		{
			QilNode qilNode = local0[0];
			if (this[XmlILOptimization.FoldNone] && qilNode.XmlType == XmlQueryTypeFactory.None && this.AllowReplace(XmlILOptimization.FoldNone, local0))
			{
				return this.Replace(XmlILOptimization.FoldNone, local0, this.VisitNop(this.f.Nop(qilNode)));
			}
			if (this[XmlILOptimization.AnnotateConstruction] && this.AllowReplace(XmlILOptimization.AnnotateConstruction, local0))
			{
				local0.Child = this.contentAnalyzer.Analyze(local0, qilNode);
			}
			return this.NoReplace(local0);
		}

		// Token: 0x06004281 RID: 17025 RVA: 0x001677B0 File Offset: 0x001659B0
		protected override QilNode VisitNamespaceDecl(QilBinary local0)
		{
			QilNode qilNode = local0[0];
			QilNode qilNode2 = local0[1];
			if (this[XmlILOptimization.FoldNone] && qilNode.XmlType == XmlQueryTypeFactory.None && this.AllowReplace(XmlILOptimization.FoldNone, local0))
			{
				return this.Replace(XmlILOptimization.FoldNone, local0, this.VisitNop(this.f.Nop(qilNode)));
			}
			if (this[XmlILOptimization.FoldNone] && qilNode2.XmlType == XmlQueryTypeFactory.None && this.AllowReplace(XmlILOptimization.FoldNone, local0))
			{
				return this.Replace(XmlILOptimization.FoldNone, local0, this.VisitNop(this.f.Nop(qilNode2)));
			}
			if (XmlILConstructInfo.Read(local0).IsNamespaceInScope && this[XmlILOptimization.EliminateNamespaceDecl] && this.AllowReplace(XmlILOptimization.EliminateNamespaceDecl, local0))
			{
				return this.Replace(XmlILOptimization.EliminateNamespaceDecl, local0, this.VisitSequence(this.f.Sequence()));
			}
			if (this[XmlILOptimization.AnnotateConstruction] && this.AllowReplace(XmlILOptimization.AnnotateConstruction, local0))
			{
				this.contentAnalyzer.Analyze(local0, null);
			}
			return this.NoReplace(local0);
		}

		// Token: 0x06004282 RID: 17026 RVA: 0x001678B0 File Offset: 0x00165AB0
		protected override QilNode VisitRtfCtor(QilBinary local0)
		{
			QilNode qilNode = local0[0];
			QilNode qilNode2 = local0[1];
			if (this[XmlILOptimization.FoldNone] && qilNode.XmlType == XmlQueryTypeFactory.None && this.AllowReplace(XmlILOptimization.FoldNone, local0))
			{
				return this.Replace(XmlILOptimization.FoldNone, local0, this.VisitNop(this.f.Nop(qilNode)));
			}
			if (this[XmlILOptimization.AnnotateConstruction] && this.AllowReplace(XmlILOptimization.AnnotateConstruction, local0))
			{
				local0.Left = this.contentAnalyzer.Analyze(local0, qilNode);
			}
			if (this[XmlILOptimization.AnnotateSingleTextRtf] && qilNode.NodeType == QilNodeType.TextCtor)
			{
				QilNode qilNode3 = qilNode[0];
				if (this.AllowReplace(XmlILOptimization.AnnotateSingleTextRtf, local0))
				{
					OptimizerPatterns.Write(local0).AddPattern(OptimizerPatternName.SingleTextRtf);
					OptimizerPatterns.Write(local0).AddArgument(OptimizerPatternArgument.ElementQName, qilNode3);
					XmlILConstructInfo.Write(local0).PullFromIteratorFirst = true;
				}
			}
			return this.NoReplace(local0);
		}

		// Token: 0x06004283 RID: 17027 RVA: 0x00167988 File Offset: 0x00165B88
		protected override QilNode VisitNameOf(QilUnary local0)
		{
			QilNode qilNode = local0[0];
			if (this[XmlILOptimization.FoldNone] && qilNode.XmlType == XmlQueryTypeFactory.None && this.AllowReplace(XmlILOptimization.FoldNone, local0))
			{
				return this.Replace(XmlILOptimization.FoldNone, local0, this.VisitNop(this.f.Nop(qilNode)));
			}
			return this.NoReplace(local0);
		}

		// Token: 0x06004284 RID: 17028 RVA: 0x001679E4 File Offset: 0x00165BE4
		protected override QilNode VisitLocalNameOf(QilUnary local0)
		{
			QilNode qilNode = local0[0];
			if (this[XmlILOptimization.FoldNone] && qilNode.XmlType == XmlQueryTypeFactory.None && this.AllowReplace(XmlILOptimization.FoldNone, local0))
			{
				return this.Replace(XmlILOptimization.FoldNone, local0, this.VisitNop(this.f.Nop(qilNode)));
			}
			return this.NoReplace(local0);
		}

		// Token: 0x06004285 RID: 17029 RVA: 0x00167A40 File Offset: 0x00165C40
		protected override QilNode VisitNamespaceUriOf(QilUnary local0)
		{
			QilNode qilNode = local0[0];
			if (this[XmlILOptimization.FoldNone] && qilNode.XmlType == XmlQueryTypeFactory.None && this.AllowReplace(XmlILOptimization.FoldNone, local0))
			{
				return this.Replace(XmlILOptimization.FoldNone, local0, this.VisitNop(this.f.Nop(qilNode)));
			}
			return this.NoReplace(local0);
		}

		// Token: 0x06004286 RID: 17030 RVA: 0x00167A9C File Offset: 0x00165C9C
		protected override QilNode VisitPrefixOf(QilUnary local0)
		{
			QilNode qilNode = local0[0];
			if (this[XmlILOptimization.FoldNone] && qilNode.XmlType == XmlQueryTypeFactory.None && this.AllowReplace(XmlILOptimization.FoldNone, local0))
			{
				return this.Replace(XmlILOptimization.FoldNone, local0, this.VisitNop(this.f.Nop(qilNode)));
			}
			return this.NoReplace(local0);
		}

		// Token: 0x06004287 RID: 17031 RVA: 0x00167AF8 File Offset: 0x00165CF8
		protected override QilNode VisitTypeAssert(QilTargetType local0)
		{
			QilNode qilNode = local0[0];
			QilNode qilNode2 = local0[1];
			if (this[XmlILOptimization.FoldNone] && qilNode.XmlType == XmlQueryTypeFactory.None && this.AllowReplace(XmlILOptimization.FoldNone, local0))
			{
				return this.Replace(XmlILOptimization.FoldNone, local0, this.VisitNop(this.f.Nop(qilNode)));
			}
			if (this[XmlILOptimization.EliminateTypeAssert] && qilNode2.NodeType == QilNodeType.LiteralType)
			{
				XmlQueryType xmlQueryType = (XmlQueryType)((QilLiteral)qilNode2).Value;
				if (qilNode.XmlType.NeverSubtypeOf(xmlQueryType) && this.AllowReplace(XmlILOptimization.EliminateTypeAssert, local0))
				{
					return this.Replace(XmlILOptimization.EliminateTypeAssert, local0, this.VisitError(this.f.Error(this.VisitLiteralString(this.f.LiteralString(string.Empty)))));
				}
			}
			if (this[XmlILOptimization.EliminateTypeAssert] && qilNode2.NodeType == QilNodeType.LiteralType)
			{
				XmlQueryType xmlQueryType2 = (XmlQueryType)((QilLiteral)qilNode2).Value;
				if (qilNode.XmlType.Prime.NeverSubtypeOf(xmlQueryType2.Prime) && this.AllowReplace(XmlILOptimization.EliminateTypeAssert, local0))
				{
					return this.Replace(XmlILOptimization.EliminateTypeAssert, local0, this.VisitConditional(this.f.Conditional(this.VisitIsEmpty(this.f.IsEmpty(qilNode)), this.VisitSequence(this.f.Sequence()), this.VisitError(this.f.Error(this.VisitLiteralString(this.f.LiteralString(string.Empty)))))));
				}
			}
			if (this[XmlILOptimization.EliminateTypeAssertOptional] && qilNode2.NodeType == QilNodeType.LiteralType)
			{
				XmlQueryType xmlQueryType3 = (XmlQueryType)((QilLiteral)qilNode2).Value;
				if (qilNode.XmlType.IsSubtypeOf(xmlQueryType3) && this.AllowReplace(XmlILOptimization.EliminateTypeAssertOptional, local0))
				{
					return this.Replace(XmlILOptimization.EliminateTypeAssertOptional, local0, qilNode);
				}
			}
			return this.NoReplace(local0);
		}

		// Token: 0x06004288 RID: 17032 RVA: 0x00167CC8 File Offset: 0x00165EC8
		protected override QilNode VisitIsType(QilTargetType local0)
		{
			QilNode qilNode = local0[0];
			QilNode qilNode2 = local0[1];
			if (this[XmlILOptimization.FoldNone] && qilNode.XmlType == XmlQueryTypeFactory.None && this.AllowReplace(XmlILOptimization.FoldNone, local0))
			{
				return this.Replace(XmlILOptimization.FoldNone, local0, this.VisitNop(this.f.Nop(qilNode)));
			}
			if (this[XmlILOptimization.EliminateIsType] && !OptimizerPatterns.Read(qilNode).MatchesPattern(OptimizerPatternName.MaybeSideEffects) && qilNode2.NodeType == QilNodeType.LiteralType)
			{
				XmlQueryType xmlQueryType = (XmlQueryType)((QilLiteral)qilNode2).Value;
				if (qilNode.XmlType.IsSubtypeOf(xmlQueryType) && this.AllowReplace(XmlILOptimization.EliminateIsType, local0))
				{
					return this.Replace(XmlILOptimization.EliminateIsType, local0, this.VisitTrue(this.f.True()));
				}
			}
			if (this[XmlILOptimization.EliminateIsType] && !OptimizerPatterns.Read(qilNode).MatchesPattern(OptimizerPatternName.MaybeSideEffects) && qilNode2.NodeType == QilNodeType.LiteralType)
			{
				XmlQueryType xmlQueryType2 = (XmlQueryType)((QilLiteral)qilNode2).Value;
				if (qilNode.XmlType.NeverSubtypeOf(xmlQueryType2) && this.AllowReplace(XmlILOptimization.EliminateIsType, local0))
				{
					return this.Replace(XmlILOptimization.EliminateIsType, local0, this.VisitFalse(this.f.False()));
				}
			}
			if (this[XmlILOptimization.EliminateIsType] && qilNode2.NodeType == QilNodeType.LiteralType)
			{
				XmlQueryType xmlQueryType3 = (XmlQueryType)((QilLiteral)qilNode2).Value;
				if (qilNode.XmlType.Prime.NeverSubtypeOf(xmlQueryType3.Prime) && this.AllowReplace(XmlILOptimization.EliminateIsType, local0))
				{
					return this.Replace(XmlILOptimization.EliminateIsType, local0, this.VisitIsEmpty(this.f.IsEmpty(qilNode)));
				}
			}
			if (this[XmlILOptimization.EliminateIsType] && OptimizerPatterns.Read(qilNode).MatchesPattern(OptimizerPatternName.MaybeSideEffects) && qilNode2.NodeType == QilNodeType.LiteralType)
			{
				XmlQueryType xmlQueryType4 = (XmlQueryType)((QilLiteral)qilNode2).Value;
				if (qilNode.XmlType.IsSubtypeOf(xmlQueryType4) && this.AllowReplace(XmlILOptimization.EliminateIsType, local0))
				{
					return this.Replace(XmlILOptimization.EliminateIsType, local0, this.VisitLoop(this.f.Loop(this.VisitLet(this.f.Let(qilNode)), this.VisitTrue(this.f.True()))));
				}
			}
			if (this[XmlILOptimization.EliminateIsType] && OptimizerPatterns.Read(qilNode).MatchesPattern(OptimizerPatternName.MaybeSideEffects) && qilNode2.NodeType == QilNodeType.LiteralType)
			{
				XmlQueryType xmlQueryType5 = (XmlQueryType)((QilLiteral)qilNode2).Value;
				if (qilNode.XmlType.NeverSubtypeOf(xmlQueryType5) && this.AllowReplace(XmlILOptimization.EliminateIsType, local0))
				{
					return this.Replace(XmlILOptimization.EliminateIsType, local0, this.VisitLoop(this.f.Loop(this.VisitLet(this.f.Let(qilNode)), this.VisitFalse(this.f.False()))));
				}
			}
			return this.NoReplace(local0);
		}

		// Token: 0x06004289 RID: 17033 RVA: 0x00167F80 File Offset: 0x00166180
		protected override QilNode VisitIsEmpty(QilUnary local0)
		{
			QilNode qilNode = local0[0];
			if (this[XmlILOptimization.FoldNone] && qilNode.XmlType == XmlQueryTypeFactory.None && this.AllowReplace(XmlILOptimization.FoldNone, local0))
			{
				return this.Replace(XmlILOptimization.FoldNone, local0, this.VisitNop(this.f.Nop(qilNode)));
			}
			if (this[XmlILOptimization.EliminateIsEmpty] && qilNode.NodeType == QilNodeType.Sequence && qilNode.Count == 0 && this.AllowReplace(XmlILOptimization.EliminateIsEmpty, local0))
			{
				return this.Replace(XmlILOptimization.EliminateIsEmpty, local0, this.VisitTrue(this.f.True()));
			}
			if (this[XmlILOptimization.EliminateIsEmpty] && !qilNode.XmlType.MaybeEmpty && !OptimizerPatterns.Read(qilNode).MatchesPattern(OptimizerPatternName.MaybeSideEffects) && this.AllowReplace(XmlILOptimization.EliminateIsEmpty, local0))
			{
				return this.Replace(XmlILOptimization.EliminateIsEmpty, local0, this.VisitFalse(this.f.False()));
			}
			if (this[XmlILOptimization.EliminateIsEmpty] && !qilNode.XmlType.MaybeEmpty && this.AllowReplace(XmlILOptimization.EliminateIsEmpty, local0))
			{
				return this.Replace(XmlILOptimization.EliminateIsEmpty, local0, this.VisitLoop(this.f.Loop(this.VisitLet(this.f.Let(qilNode)), this.VisitFalse(this.f.False()))));
			}
			return this.NoReplace(local0);
		}

		// Token: 0x0600428A RID: 17034 RVA: 0x001680C8 File Offset: 0x001662C8
		protected override QilNode VisitXPathNodeValue(QilUnary local0)
		{
			QilNode qilNode = local0[0];
			if (this[XmlILOptimization.FoldNone] && qilNode.XmlType == XmlQueryTypeFactory.None && this.AllowReplace(XmlILOptimization.FoldNone, local0))
			{
				return this.Replace(XmlILOptimization.FoldNone, local0, this.VisitNop(this.f.Nop(qilNode)));
			}
			return this.NoReplace(local0);
		}

		// Token: 0x0600428B RID: 17035 RVA: 0x00168124 File Offset: 0x00166324
		protected override QilNode VisitXPathFollowing(QilUnary local0)
		{
			QilNode qilNode = local0[0];
			if (this[XmlILOptimization.FoldNone] && qilNode.XmlType == XmlQueryTypeFactory.None && this.AllowReplace(XmlILOptimization.FoldNone, local0))
			{
				return this.Replace(XmlILOptimization.FoldNone, local0, this.VisitNop(this.f.Nop(qilNode)));
			}
			if (this[XmlILOptimization.AnnotateXPathFollowing] && this.AllowReplace(XmlILOptimization.AnnotateXPathFollowing, local0))
			{
				OptimizerPatterns.Write(local0).AddPattern(OptimizerPatternName.Axis);
				this.AddStepPattern(local0, qilNode);
				OptimizerPatterns.Write(local0).AddPattern(OptimizerPatternName.IsDocOrderDistinct);
			}
			return this.NoReplace(local0);
		}

		// Token: 0x0600428C RID: 17036 RVA: 0x001681B4 File Offset: 0x001663B4
		protected override QilNode VisitXPathPreceding(QilUnary local0)
		{
			QilNode qilNode = local0[0];
			if (this[XmlILOptimization.FoldNone] && qilNode.XmlType == XmlQueryTypeFactory.None && this.AllowReplace(XmlILOptimization.FoldNone, local0))
			{
				return this.Replace(XmlILOptimization.FoldNone, local0, this.VisitNop(this.f.Nop(qilNode)));
			}
			if (this[XmlILOptimization.AnnotateXPathPreceding] && this.AllowReplace(XmlILOptimization.AnnotateXPathPreceding, local0))
			{
				OptimizerPatterns.Write(local0).AddPattern(OptimizerPatternName.Axis);
				this.AddStepPattern(local0, qilNode);
			}
			return this.NoReplace(local0);
		}

		// Token: 0x0600428D RID: 17037 RVA: 0x00168238 File Offset: 0x00166438
		protected override QilNode VisitXPathNamespace(QilUnary local0)
		{
			QilNode qilNode = local0[0];
			if (this[XmlILOptimization.FoldNone] && qilNode.XmlType == XmlQueryTypeFactory.None && this.AllowReplace(XmlILOptimization.FoldNone, local0))
			{
				return this.Replace(XmlILOptimization.FoldNone, local0, this.VisitNop(this.f.Nop(qilNode)));
			}
			if (this[XmlILOptimization.AnnotateNamespace] && this.AllowReplace(XmlILOptimization.AnnotateNamespace, local0))
			{
				OptimizerPatterns.Write(local0).AddPattern(OptimizerPatternName.Axis);
				this.AddStepPattern(local0, qilNode);
				OptimizerPatterns.Write(local0).AddPattern(OptimizerPatternName.IsDocOrderDistinct);
				OptimizerPatterns.Write(local0).AddPattern(OptimizerPatternName.SameDepth);
			}
			return this.NoReplace(local0);
		}

		// Token: 0x0600428E RID: 17038 RVA: 0x001682D8 File Offset: 0x001664D8
		protected override QilNode VisitXsltGenerateId(QilUnary local0)
		{
			QilNode qilNode = local0[0];
			if (this[XmlILOptimization.FoldNone] && qilNode.XmlType == XmlQueryTypeFactory.None && this.AllowReplace(XmlILOptimization.FoldNone, local0))
			{
				return this.Replace(XmlILOptimization.FoldNone, local0, this.VisitNop(this.f.Nop(qilNode)));
			}
			return this.NoReplace(local0);
		}

		// Token: 0x0600428F RID: 17039 RVA: 0x00168334 File Offset: 0x00166534
		protected override QilNode VisitXsltCopy(QilBinary local0)
		{
			QilNode qilNode = local0[0];
			QilNode qilNode2 = local0[1];
			if (this[XmlILOptimization.FoldNone] && qilNode.XmlType == XmlQueryTypeFactory.None && this.AllowReplace(XmlILOptimization.FoldNone, local0))
			{
				return this.Replace(XmlILOptimization.FoldNone, local0, this.VisitNop(this.f.Nop(qilNode)));
			}
			if (this[XmlILOptimization.FoldNone] && qilNode2.XmlType == XmlQueryTypeFactory.None && this.AllowReplace(XmlILOptimization.FoldNone, local0))
			{
				return this.Replace(XmlILOptimization.FoldNone, local0, this.VisitNop(this.f.Nop(qilNode2)));
			}
			if (this[XmlILOptimization.AnnotateConstruction] && this.AllowReplace(XmlILOptimization.AnnotateConstruction, local0))
			{
				local0.Right = this.contentAnalyzer.Analyze(local0, qilNode2);
			}
			return this.NoReplace(local0);
		}

		// Token: 0x06004290 RID: 17040 RVA: 0x001683FC File Offset: 0x001665FC
		protected override QilNode VisitXsltCopyOf(QilUnary local0)
		{
			QilNode qilNode = local0[0];
			if (this[XmlILOptimization.FoldNone] && qilNode.XmlType == XmlQueryTypeFactory.None && this.AllowReplace(XmlILOptimization.FoldNone, local0))
			{
				return this.Replace(XmlILOptimization.FoldNone, local0, this.VisitNop(this.f.Nop(qilNode)));
			}
			if (this[XmlILOptimization.AnnotateConstruction] && this.AllowReplace(XmlILOptimization.AnnotateConstruction, local0))
			{
				this.contentAnalyzer.Analyze(local0, null);
			}
			return this.NoReplace(local0);
		}

		// Token: 0x06004291 RID: 17041 RVA: 0x00168478 File Offset: 0x00166678
		protected override QilNode VisitXsltConvert(QilTargetType local0)
		{
			QilNode qilNode = local0[0];
			QilNode qilNode2 = local0[1];
			if (this[XmlILOptimization.FoldNone] && qilNode.XmlType == XmlQueryTypeFactory.None && this.AllowReplace(XmlILOptimization.FoldNone, local0))
			{
				return this.Replace(XmlILOptimization.FoldNone, local0, this.VisitNop(this.f.Nop(qilNode)));
			}
			if (this[XmlILOptimization.FoldXsltConvertLiteral] && this.IsLiteral(qilNode) && qilNode2.NodeType == QilNodeType.LiteralType)
			{
				XmlQueryType xmlQueryType = (XmlQueryType)((QilLiteral)qilNode2).Value;
				if (this.CanFoldXsltConvert(qilNode, xmlQueryType) && this.AllowReplace(XmlILOptimization.FoldXsltConvertLiteral, local0))
				{
					return this.Replace(XmlILOptimization.FoldXsltConvertLiteral, local0, this.FoldXsltConvert(qilNode, xmlQueryType));
				}
			}
			if (this[XmlILOptimization.EliminateXsltConvert] && qilNode2.NodeType == QilNodeType.LiteralType)
			{
				XmlQueryType xmlQueryType2 = (XmlQueryType)((QilLiteral)qilNode2).Value;
				if (qilNode.XmlType == xmlQueryType2 && this.AllowReplace(XmlILOptimization.EliminateXsltConvert, local0))
				{
					return this.Replace(XmlILOptimization.EliminateXsltConvert, local0, qilNode);
				}
			}
			return this.NoReplace(local0);
		}

		// Token: 0x17000CF0 RID: 3312
		private bool this[XmlILOptimization ann]
		{
			get
			{
				return base.Patterns.IsSet((int)ann);
			}
		}

		// Token: 0x06004293 RID: 17043 RVA: 0x00168586 File Offset: 0x00166786
		private bool DependsOn(QilNode expr, QilNode target)
		{
			return new XmlILOptimizerVisitor.NodeFinder().Find(expr, target);
		}

		// Token: 0x06004294 RID: 17044 RVA: 0x00168594 File Offset: 0x00166794
		protected bool NonPositional(QilNode expr, QilNode iter)
		{
			return !new XmlILOptimizerVisitor.PositionOfFinder().Find(expr, iter);
		}

		// Token: 0x06004295 RID: 17045 RVA: 0x001685A8 File Offset: 0x001667A8
		private QilNode Subs(QilNode expr, QilNode refOld, QilNode refNew)
		{
			this.subs.AddSubstitutionPair(refOld, refNew);
			QilNode qilNode;
			if (expr is QilReference)
			{
				qilNode = this.VisitReference(expr);
			}
			else
			{
				qilNode = this.Visit(expr);
			}
			this.subs.RemoveLastSubstitutionPair();
			return qilNode;
		}

		// Token: 0x06004296 RID: 17046 RVA: 0x001685E8 File Offset: 0x001667E8
		private bool IsGlobalVariable(QilIterator iter)
		{
			return this.qil.GlobalVariableList.Contains(iter);
		}

		// Token: 0x06004297 RID: 17047 RVA: 0x001685FB File Offset: 0x001667FB
		private bool IsGlobalValue(QilNode nd)
		{
			if (nd.NodeType == QilNodeType.Let)
			{
				return this.qil.GlobalVariableList.Contains(nd);
			}
			return nd.NodeType == QilNodeType.Parameter && this.qil.GlobalParameterList.Contains(nd);
		}

		// Token: 0x06004298 RID: 17048 RVA: 0x00168638 File Offset: 0x00166838
		private bool IsPrimitiveNumeric(XmlQueryType typ)
		{
			return typ == XmlQueryTypeFactory.IntX || typ == XmlQueryTypeFactory.IntegerX || typ == XmlQueryTypeFactory.DecimalX || typ == XmlQueryTypeFactory.FloatX || typ == XmlQueryTypeFactory.DoubleX;
		}

		// Token: 0x06004299 RID: 17049 RVA: 0x00168694 File Offset: 0x00166894
		private bool MatchesContentTest(XmlQueryType typ)
		{
			return typ == XmlQueryTypeFactory.Element || typ == XmlQueryTypeFactory.Text || typ == XmlQueryTypeFactory.Comment || typ == XmlQueryTypeFactory.PI || typ == XmlQueryTypeFactory.Content;
		}

		// Token: 0x0600429A RID: 17050 RVA: 0x001686F0 File Offset: 0x001668F0
		private bool IsConstructedExpression(QilNode nd)
		{
			if (this.qil.IsDebug)
			{
				return true;
			}
			if (nd.XmlType.IsNode)
			{
				QilNodeType nodeType = nd.NodeType;
				if (nodeType <= QilNodeType.Loop)
				{
					switch (nodeType)
					{
					case QilNodeType.Conditional:
						break;
					case QilNodeType.Choice:
						return true;
					case QilNodeType.Length:
						return false;
					case QilNodeType.Sequence:
					{
						if (nd.Count == 0)
						{
							return true;
						}
						using (IEnumerator<QilNode> enumerator = nd.GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								QilNode qilNode = enumerator.Current;
								if (this.IsConstructedExpression(qilNode))
								{
									return true;
								}
							}
							return false;
						}
						break;
					}
					default:
						if (nodeType != QilNodeType.Loop)
						{
							return false;
						}
						return this.IsConstructedExpression(((QilLoop)nd).Body);
					}
					QilTernary qilTernary = (QilTernary)nd;
					return this.IsConstructedExpression(qilTernary.Center) || this.IsConstructedExpression(qilTernary.Right);
				}
				if (nodeType == QilNodeType.Invoke)
				{
					return !((QilInvoke)nd).Function.XmlType.IsAtomicValue;
				}
				if (nodeType - QilNodeType.ElementCtor > 7 && nodeType - QilNodeType.XsltCopy > 1)
				{
					return false;
				}
				return true;
			}
			return false;
		}

		// Token: 0x0600429B RID: 17051 RVA: 0x00168808 File Offset: 0x00166A08
		private bool IsLiteral(QilNode nd)
		{
			QilNodeType nodeType = nd.NodeType;
			return nodeType - QilNodeType.True <= 7;
		}

		// Token: 0x0600429C RID: 17052 RVA: 0x00168828 File Offset: 0x00166A28
		private bool AreLiteralArgs(QilNode nd)
		{
			foreach (QilNode qilNode in nd)
			{
				if (!this.IsLiteral(qilNode))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600429D RID: 17053 RVA: 0x0016887C File Offset: 0x00166A7C
		private object ExtractLiteralValue(QilNode nd)
		{
			if (nd.NodeType == QilNodeType.True)
			{
				return true;
			}
			if (nd.NodeType == QilNodeType.False)
			{
				return false;
			}
			if (nd.NodeType == QilNodeType.LiteralQName)
			{
				return nd;
			}
			return ((QilLiteral)nd).Value;
		}

		// Token: 0x0600429E RID: 17054 RVA: 0x001688B8 File Offset: 0x00166AB8
		private bool HasNestedSequence(QilNode nd)
		{
			using (IEnumerator<QilNode> enumerator = nd.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.NodeType == QilNodeType.Sequence)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x0600429F RID: 17055 RVA: 0x00168908 File Offset: 0x00166B08
		private bool AllowJoinAndDod(QilNode nd)
		{
			OptimizerPatterns optimizerPatterns = OptimizerPatterns.Read(nd);
			return (optimizerPatterns.MatchesPattern(OptimizerPatternName.FilterElements) || optimizerPatterns.MatchesPattern(OptimizerPatternName.FilterContentKind)) && (this.IsStepPattern(optimizerPatterns, QilNodeType.DescendantOrSelf) || this.IsStepPattern(optimizerPatterns, QilNodeType.Descendant) || this.IsStepPattern(optimizerPatterns, QilNodeType.Content) || this.IsStepPattern(optimizerPatterns, QilNodeType.XPathPreceding) || this.IsStepPattern(optimizerPatterns, QilNodeType.XPathFollowing) || this.IsStepPattern(optimizerPatterns, QilNodeType.FollowingSibling));
		}

		// Token: 0x060042A0 RID: 17056 RVA: 0x00168974 File Offset: 0x00166B74
		private bool AllowDodReverse(QilNode nd)
		{
			OptimizerPatterns optimizerPatterns = OptimizerPatterns.Read(nd);
			return (optimizerPatterns.MatchesPattern(OptimizerPatternName.Axis) || optimizerPatterns.MatchesPattern(OptimizerPatternName.FilterElements) || optimizerPatterns.MatchesPattern(OptimizerPatternName.FilterContentKind)) && (this.IsStepPattern(optimizerPatterns, QilNodeType.Ancestor) || this.IsStepPattern(optimizerPatterns, QilNodeType.AncestorOrSelf) || this.IsStepPattern(optimizerPatterns, QilNodeType.XPathPreceding) || this.IsStepPattern(optimizerPatterns, QilNodeType.PrecedingSibling));
		}

		// Token: 0x060042A1 RID: 17057 RVA: 0x001689D3 File Offset: 0x00166BD3
		private bool CanFoldXsltConvert(QilNode ndLiteral, XmlQueryType typTarget)
		{
			return this.FoldXsltConvert(ndLiteral, typTarget).NodeType != QilNodeType.XsltConvert;
		}

		// Token: 0x060042A2 RID: 17058 RVA: 0x001689EC File Offset: 0x00166BEC
		private bool CanFoldXsltConvertNonLossy(QilNode ndLiteral, XmlQueryType typTarget)
		{
			QilNode qilNode = this.FoldXsltConvert(ndLiteral, typTarget);
			if (qilNode.NodeType == QilNodeType.XsltConvert)
			{
				return false;
			}
			qilNode = this.FoldXsltConvert(qilNode, ndLiteral.XmlType);
			return qilNode.NodeType != QilNodeType.XsltConvert && this.ExtractLiteralValue(ndLiteral).Equals(this.ExtractLiteralValue(qilNode));
		}

		// Token: 0x060042A3 RID: 17059 RVA: 0x00168A3C File Offset: 0x00166C3C
		private QilNode FoldXsltConvert(QilNode ndLiteral, XmlQueryType typTarget)
		{
			try
			{
				if (typTarget.IsAtomicValue)
				{
					XmlAtomicValue xmlAtomicValue = new XmlAtomicValue(ndLiteral.XmlType.SchemaType, this.ExtractLiteralValue(ndLiteral));
					xmlAtomicValue = XsltConvert.ConvertToType(xmlAtomicValue, typTarget);
					if (typTarget == XmlQueryTypeFactory.StringX)
					{
						return this.f.LiteralString(xmlAtomicValue.Value);
					}
					if (typTarget == XmlQueryTypeFactory.IntX)
					{
						return this.f.LiteralInt32(xmlAtomicValue.ValueAsInt);
					}
					if (typTarget == XmlQueryTypeFactory.IntegerX)
					{
						return this.f.LiteralInt64(xmlAtomicValue.ValueAsLong);
					}
					if (typTarget == XmlQueryTypeFactory.DecimalX)
					{
						return this.f.LiteralDecimal((decimal)xmlAtomicValue.ValueAs(XsltConvert.DecimalType));
					}
					if (typTarget == XmlQueryTypeFactory.DoubleX)
					{
						return this.f.LiteralDouble(xmlAtomicValue.ValueAsDouble);
					}
					if (typTarget == XmlQueryTypeFactory.BooleanX)
					{
						return xmlAtomicValue.ValueAsBoolean ? this.f.True() : this.f.False();
					}
				}
			}
			catch (OverflowException)
			{
			}
			catch (FormatException)
			{
			}
			return this.f.XsltConvert(ndLiteral, typTarget);
		}

		// Token: 0x060042A4 RID: 17060 RVA: 0x00168BA8 File Offset: 0x00166DA8
		private QilNode FoldComparison(QilNodeType opType, QilNode left, QilNode right)
		{
			object obj = this.ExtractLiteralValue(left);
			object obj2 = this.ExtractLiteralValue(right);
			if (left.NodeType == QilNodeType.LiteralDouble && (double.IsNaN((double)obj) || double.IsNaN((double)obj2)))
			{
				if (opType != QilNodeType.Ne)
				{
					return this.f.False();
				}
				return this.f.True();
			}
			else if (opType == QilNodeType.Eq)
			{
				if (!obj.Equals(obj2))
				{
					return this.f.False();
				}
				return this.f.True();
			}
			else if (opType == QilNodeType.Ne)
			{
				if (!obj.Equals(obj2))
				{
					return this.f.True();
				}
				return this.f.False();
			}
			else
			{
				int num;
				if (left.NodeType == QilNodeType.LiteralString)
				{
					num = string.CompareOrdinal((string)obj, (string)obj2);
				}
				else
				{
					num = ((IComparable)obj).CompareTo(obj2);
				}
				switch (opType)
				{
				case QilNodeType.Gt:
					if (num <= 0)
					{
						return this.f.False();
					}
					return this.f.True();
				case QilNodeType.Ge:
					if (num < 0)
					{
						return this.f.False();
					}
					return this.f.True();
				case QilNodeType.Lt:
					if (num >= 0)
					{
						return this.f.False();
					}
					return this.f.True();
				case QilNodeType.Le:
					if (num > 0)
					{
						return this.f.False();
					}
					return this.f.True();
				default:
					return null;
				}
			}
		}

		// Token: 0x060042A5 RID: 17061 RVA: 0x00168D09 File Offset: 0x00166F09
		private bool CanFoldArithmetic(QilNodeType opType, QilLiteral left, QilLiteral right)
		{
			return this.FoldArithmetic(opType, left, right) is QilLiteral;
		}

		// Token: 0x060042A6 RID: 17062 RVA: 0x00168D1C File Offset: 0x00166F1C
		private QilNode FoldArithmetic(QilNodeType opType, QilLiteral left, QilLiteral right)
		{
			checked
			{
				try
				{
					switch (left.NodeType)
					{
					case QilNodeType.LiteralInt32:
					{
						int num = left;
						int num2 = right;
						switch (opType)
						{
						case QilNodeType.Add:
							return this.f.LiteralInt32(num + num2);
						case QilNodeType.Subtract:
							return this.f.LiteralInt32(num - num2);
						case QilNodeType.Multiply:
							return this.f.LiteralInt32(num * num2);
						case QilNodeType.Divide:
							return this.f.LiteralInt32(num / num2);
						case QilNodeType.Modulo:
							return this.f.LiteralInt32(num % num2);
						}
						break;
					}
					case QilNodeType.LiteralInt64:
					{
						long num3 = left;
						long num4 = right;
						switch (opType)
						{
						case QilNodeType.Add:
							return this.f.LiteralInt64(num3 + num4);
						case QilNodeType.Subtract:
							return this.f.LiteralInt64(num3 - num4);
						case QilNodeType.Multiply:
							return this.f.LiteralInt64(num3 * num4);
						case QilNodeType.Divide:
							return this.f.LiteralInt64(num3 / num4);
						case QilNodeType.Modulo:
							return this.f.LiteralInt64(num3 % num4);
						}
						break;
					}
					case QilNodeType.LiteralDouble:
					{
						double num5 = left;
						double num6 = right;
						unchecked
						{
							switch (opType)
							{
							case QilNodeType.Add:
								return this.f.LiteralDouble(num5 + num6);
							case QilNodeType.Subtract:
								return this.f.LiteralDouble(num5 - num6);
							case QilNodeType.Multiply:
								return this.f.LiteralDouble(num5 * num6);
							case QilNodeType.Divide:
								return this.f.LiteralDouble(num5 / num6);
							case QilNodeType.Modulo:
								return this.f.LiteralDouble(num5 % num6);
							}
							break;
						}
					}
					case QilNodeType.LiteralDecimal:
					{
						decimal num7 = left;
						decimal num8 = right;
						switch (opType)
						{
						case QilNodeType.Add:
							return this.f.LiteralDecimal(num7 + num8);
						case QilNodeType.Subtract:
							return this.f.LiteralDecimal(num7 - num8);
						case QilNodeType.Multiply:
							return this.f.LiteralDecimal(num7 * num8);
						case QilNodeType.Divide:
							return this.f.LiteralDecimal(num7 / num8);
						case QilNodeType.Modulo:
							return this.f.LiteralDecimal(num7 % num8);
						}
						break;
					}
					}
				}
				catch (OverflowException)
				{
				}
				catch (DivideByZeroException)
				{
				}
				switch (opType)
				{
				case QilNodeType.Add:
					return this.f.Add(left, right);
				case QilNodeType.Subtract:
					return this.f.Subtract(left, right);
				case QilNodeType.Multiply:
					return this.f.Multiply(left, right);
				case QilNodeType.Divide:
					return this.f.Divide(left, right);
				case QilNodeType.Modulo:
					return this.f.Modulo(left, right);
				default:
					return null;
				}
				QilNode qilNode;
				return qilNode;
			}
		}

		// Token: 0x060042A7 RID: 17063 RVA: 0x00169074 File Offset: 0x00167274
		private void AddStepPattern(QilNode nd, QilNode input)
		{
			OptimizerPatterns optimizerPatterns = OptimizerPatterns.Write(nd);
			optimizerPatterns.AddPattern(OptimizerPatternName.Step);
			optimizerPatterns.AddArgument(OptimizerPatternArgument.StepNode, nd);
			optimizerPatterns.AddArgument(OptimizerPatternArgument.StepInput, input);
		}

		// Token: 0x060042A8 RID: 17064 RVA: 0x00169093 File Offset: 0x00167293
		private bool IsDocOrderDistinct(QilNode nd)
		{
			return OptimizerPatterns.Read(nd).MatchesPattern(OptimizerPatternName.IsDocOrderDistinct);
		}

		// Token: 0x060042A9 RID: 17065 RVA: 0x001690A1 File Offset: 0x001672A1
		private bool IsStepPattern(QilNode nd, QilNodeType stepType)
		{
			return this.IsStepPattern(OptimizerPatterns.Read(nd), stepType);
		}

		// Token: 0x060042AA RID: 17066 RVA: 0x001690B0 File Offset: 0x001672B0
		private bool IsStepPattern(OptimizerPatterns patt, QilNodeType stepType)
		{
			return patt.MatchesPattern(OptimizerPatternName.Step) && ((QilNode)patt.GetArgument(OptimizerPatternArgument.StepNode)).NodeType == stepType;
		}

		// Token: 0x060042AB RID: 17067 RVA: 0x001690D4 File Offset: 0x001672D4
		private static void EliminateUnusedGlobals(IList<QilNode> globals)
		{
			int num = 0;
			for (int i = 0; i < globals.Count; i++)
			{
				QilNode qilNode = globals[i];
				bool flag;
				if (qilNode.NodeType == QilNodeType.Function)
				{
					flag = XmlILConstructInfo.Read(qilNode).CallersInfo.Count != 0;
				}
				else
				{
					OptimizerPatterns optimizerPatterns = OptimizerPatterns.Read(qilNode);
					flag = optimizerPatterns.MatchesPattern(OptimizerPatternName.IsReferenced) || optimizerPatterns.MatchesPattern(OptimizerPatternName.MaybeSideEffects);
				}
				if (flag)
				{
					if (num < i)
					{
						globals[num] = globals[i];
					}
					num++;
				}
			}
			for (int j = globals.Count - 1; j >= num; j--)
			{
				globals.RemoveAt(j);
			}
		}

		// Token: 0x04002B0B RID: 11019
		private static readonly QilPatternVisitor.QilPatterns PatternsNoOpt = new QilPatternVisitor.QilPatterns(141, false);

		// Token: 0x04002B0C RID: 11020
		private static readonly QilPatternVisitor.QilPatterns PatternsOpt = new QilPatternVisitor.QilPatterns(141, true);

		// Token: 0x04002B0D RID: 11021
		private QilExpression qil;

		// Token: 0x04002B0E RID: 11022
		private XmlILElementAnalyzer elemAnalyzer;

		// Token: 0x04002B0F RID: 11023
		private XmlILStateAnalyzer contentAnalyzer;

		// Token: 0x04002B10 RID: 11024
		private XmlILNamespaceAnalyzer nmspAnalyzer;

		// Token: 0x04002B11 RID: 11025
		private XmlILOptimizerVisitor.NodeCounter nodeCounter = new XmlILOptimizerVisitor.NodeCounter();

		// Token: 0x04002B12 RID: 11026
		private SubstitutionList subs = new SubstitutionList();

		// Token: 0x02000670 RID: 1648
		private class NodeCounter : QilVisitor
		{
			// Token: 0x060042AC RID: 17068 RVA: 0x00169174 File Offset: 0x00167374
			public int Count(QilNode expr, QilNode target)
			{
				this.cnt = 0;
				this.target = target;
				this.Visit(expr);
				return this.cnt;
			}

			// Token: 0x060042AD RID: 17069 RVA: 0x00169192 File Offset: 0x00167392
			protected override QilNode Visit(QilNode n)
			{
				if (n == null)
				{
					return null;
				}
				if (n == this.target)
				{
					this.cnt++;
				}
				return this.VisitChildren(n);
			}

			// Token: 0x060042AE RID: 17070 RVA: 0x001691B7 File Offset: 0x001673B7
			protected override QilNode VisitReference(QilNode n)
			{
				if (n == this.target)
				{
					this.cnt++;
				}
				return n;
			}

			// Token: 0x04002B13 RID: 11027
			protected QilNode target;

			// Token: 0x04002B14 RID: 11028
			protected int cnt;
		}

		// Token: 0x02000671 RID: 1649
		private class NodeFinder : QilVisitor
		{
			// Token: 0x060042B0 RID: 17072 RVA: 0x001691D1 File Offset: 0x001673D1
			public bool Find(QilNode expr, QilNode target)
			{
				this.result = false;
				this.target = target;
				this.parent = null;
				this.VisitAssumeReference(expr);
				return this.result;
			}

			// Token: 0x060042B1 RID: 17073 RVA: 0x001691F8 File Offset: 0x001673F8
			protected override QilNode Visit(QilNode expr)
			{
				if (!this.result)
				{
					if (expr == this.target)
					{
						this.result = this.OnFound(expr);
					}
					if (!this.result)
					{
						QilNode qilNode = this.parent;
						this.parent = expr;
						this.VisitChildren(expr);
						this.parent = qilNode;
					}
				}
				return expr;
			}

			// Token: 0x060042B2 RID: 17074 RVA: 0x00169249 File Offset: 0x00167449
			protected override QilNode VisitReference(QilNode expr)
			{
				if (expr == this.target)
				{
					this.result = this.OnFound(expr);
				}
				return expr;
			}

			// Token: 0x060042B3 RID: 17075 RVA: 0x00003242 File Offset: 0x00001442
			protected virtual bool OnFound(QilNode expr)
			{
				return true;
			}

			// Token: 0x04002B15 RID: 11029
			protected bool result;

			// Token: 0x04002B16 RID: 11030
			protected QilNode target;

			// Token: 0x04002B17 RID: 11031
			protected QilNode parent;
		}

		// Token: 0x02000672 RID: 1650
		private class PositionOfFinder : XmlILOptimizerVisitor.NodeFinder
		{
			// Token: 0x060042B5 RID: 17077 RVA: 0x00169262 File Offset: 0x00167462
			protected override bool OnFound(QilNode expr)
			{
				return this.parent != null && this.parent.NodeType == QilNodeType.PositionOf;
			}
		}

		// Token: 0x02000673 RID: 1651
		private class EqualityIndexVisitor : QilVisitor
		{
			// Token: 0x060042B7 RID: 17079 RVA: 0x00169285 File Offset: 0x00167485
			public bool Scan(QilNode expr, QilNode ctxt, QilNode key)
			{
				this.result = true;
				this.ctxt = ctxt;
				this.key = key;
				this.Visit(expr);
				return this.result;
			}

			// Token: 0x060042B8 RID: 17080 RVA: 0x001692AA File Offset: 0x001674AA
			protected override QilNode VisitReference(QilNode expr)
			{
				if (this.result && (expr == this.key || expr == this.ctxt))
				{
					this.result = false;
					return expr;
				}
				return expr;
			}

			// Token: 0x060042B9 RID: 17081 RVA: 0x001692D0 File Offset: 0x001674D0
			protected override QilNode VisitRoot(QilUnary root)
			{
				if (root.Child == this.ctxt)
				{
					return root;
				}
				return this.VisitChildren(root);
			}

			// Token: 0x04002B18 RID: 11032
			protected bool result;

			// Token: 0x04002B19 RID: 11033
			protected QilNode ctxt;

			// Token: 0x04002B1A RID: 11034
			protected QilNode key;
		}
	}
}
