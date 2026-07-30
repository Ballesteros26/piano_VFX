using System;
using System.Xml.Xsl.Qil;
using System.Xml.Xsl.XPath;

namespace System.Xml.Xsl.Xslt
{
	// Token: 0x02000579 RID: 1401
	internal class KeyMatchBuilder : XPathBuilder, XPathPatternParser.IPatternBuilder, IXPathBuilder<QilNode>
	{
		// Token: 0x060037A4 RID: 14244 RVA: 0x0013595A File Offset: 0x00133B5A
		public KeyMatchBuilder(IXPathEnvironment env)
			: base(env)
		{
			this.convertor = new KeyMatchBuilder.PathConvertor(env.Factory);
		}

		// Token: 0x060037A5 RID: 14245 RVA: 0x00135974 File Offset: 0x00133B74
		public override void StartBuild()
		{
			if (this.depth == 0)
			{
				base.StartBuild();
			}
			this.depth++;
		}

		// Token: 0x060037A6 RID: 14246 RVA: 0x00135994 File Offset: 0x00133B94
		public override QilNode EndBuild(QilNode result)
		{
			this.depth--;
			if (result == null)
			{
				return base.EndBuild(result);
			}
			if (this.depth == 0)
			{
				result = this.convertor.ConvertReletive2Absolute(result, this.fixupCurrent);
				result = base.EndBuild(result);
			}
			return result;
		}

		// Token: 0x060037A7 RID: 14247 RVA: 0x00002068 File Offset: 0x00000268
		public virtual IXPathBuilder<QilNode> GetPredicateBuilder(QilNode ctx)
		{
			return this;
		}

		// Token: 0x040023BA RID: 9146
		private int depth;

		// Token: 0x040023BB RID: 9147
		private KeyMatchBuilder.PathConvertor convertor;

		// Token: 0x0200057A RID: 1402
		internal class PathConvertor : QilReplaceVisitor
		{
			// Token: 0x060037A8 RID: 14248 RVA: 0x001359E0 File Offset: 0x00133BE0
			public PathConvertor(XPathQilFactory f)
				: base(f.BaseFactory)
			{
				this.f = f;
			}

			// Token: 0x060037A9 RID: 14249 RVA: 0x001359F5 File Offset: 0x00133BF5
			public QilNode ConvertReletive2Absolute(QilNode node, QilNode fixup)
			{
				QilDepthChecker.Check(node);
				this.fixup = fixup;
				return this.Visit(node);
			}

			// Token: 0x060037AA RID: 14250 RVA: 0x00135A0B File Offset: 0x00133C0B
			protected override QilNode Visit(QilNode n)
			{
				if (n.NodeType == QilNodeType.Union || n.NodeType == QilNodeType.DocOrderDistinct || n.NodeType == QilNodeType.Filter || n.NodeType == QilNodeType.Loop)
				{
					return base.Visit(n);
				}
				return n;
			}

			// Token: 0x060037AB RID: 14251 RVA: 0x00135A40 File Offset: 0x00133C40
			protected override QilNode VisitLoop(QilLoop n)
			{
				if (n.Variable.Binding.NodeType == QilNodeType.Root || n.Variable.Binding.NodeType == QilNodeType.Deref)
				{
					return n;
				}
				if (n.Variable.Binding.NodeType == QilNodeType.Content)
				{
					QilUnary qilUnary = (QilUnary)n.Variable.Binding;
					QilIterator qilIterator = this.f.For(this.f.DescendantOrSelf(this.f.Root(this.fixup)));
					qilUnary.Child = qilIterator;
					n.Variable.Binding = this.f.Loop(qilIterator, qilUnary);
					return n;
				}
				n.Variable.Binding = this.Visit(n.Variable.Binding);
				return n;
			}

			// Token: 0x060037AC RID: 14252 RVA: 0x00135B02 File Offset: 0x00133D02
			protected override QilNode VisitFilter(QilLoop n)
			{
				return this.VisitLoop(n);
			}

			// Token: 0x040023BC RID: 9148
			private new XPathQilFactory f;

			// Token: 0x040023BD RID: 9149
			private QilNode fixup;
		}
	}
}
