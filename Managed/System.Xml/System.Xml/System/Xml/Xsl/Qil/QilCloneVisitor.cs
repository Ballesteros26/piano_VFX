using System;

namespace System.Xml.Xsl.Qil
{
	// Token: 0x0200062C RID: 1580
	internal class QilCloneVisitor : QilScopedVisitor
	{
		// Token: 0x06003DE1 RID: 15841 RVA: 0x00156021 File Offset: 0x00154221
		public QilCloneVisitor(QilFactory fac)
			: this(fac, new SubstitutionList())
		{
		}

		// Token: 0x06003DE2 RID: 15842 RVA: 0x0015602F File Offset: 0x0015422F
		public QilCloneVisitor(QilFactory fac, SubstitutionList subs)
		{
			this.fac = fac;
			this.subs = subs;
		}

		// Token: 0x06003DE3 RID: 15843 RVA: 0x00156045 File Offset: 0x00154245
		public QilNode Clone(QilNode node)
		{
			QilDepthChecker.Check(node);
			return this.VisitAssumeReference(node);
		}

		// Token: 0x06003DE4 RID: 15844 RVA: 0x00156054 File Offset: 0x00154254
		protected override QilNode Visit(QilNode oldNode)
		{
			QilNode qilNode = null;
			if (oldNode == null)
			{
				return null;
			}
			if (oldNode is QilReference)
			{
				qilNode = this.FindClonedReference(oldNode);
			}
			if (qilNode == null)
			{
				qilNode = oldNode.ShallowClone(this.fac);
			}
			return base.Visit(qilNode);
		}

		// Token: 0x06003DE5 RID: 15845 RVA: 0x00156090 File Offset: 0x00154290
		protected override QilNode VisitChildren(QilNode parent)
		{
			for (int i = 0; i < parent.Count; i++)
			{
				QilNode qilNode = parent[i];
				if (this.IsReference(parent, i))
				{
					parent[i] = this.VisitReference(qilNode);
					if (parent[i] == null)
					{
						parent[i] = qilNode;
					}
				}
				else
				{
					parent[i] = this.Visit(qilNode);
				}
			}
			return parent;
		}

		// Token: 0x06003DE6 RID: 15846 RVA: 0x001560F0 File Offset: 0x001542F0
		protected override QilNode VisitReference(QilNode oldNode)
		{
			QilNode qilNode = this.FindClonedReference(oldNode);
			return base.VisitReference((qilNode == null) ? oldNode : qilNode);
		}

		// Token: 0x06003DE7 RID: 15847 RVA: 0x00156112 File Offset: 0x00154312
		protected override void BeginScope(QilNode node)
		{
			this.subs.AddSubstitutionPair(node, node.ShallowClone(this.fac));
		}

		// Token: 0x06003DE8 RID: 15848 RVA: 0x0015612C File Offset: 0x0015432C
		protected override void EndScope(QilNode node)
		{
			this.subs.RemoveLastSubstitutionPair();
		}

		// Token: 0x06003DE9 RID: 15849 RVA: 0x00156139 File Offset: 0x00154339
		protected QilNode FindClonedReference(QilNode node)
		{
			return this.subs.FindReplacement(node);
		}

		// Token: 0x04002837 RID: 10295
		private QilFactory fac;

		// Token: 0x04002838 RID: 10296
		private SubstitutionList subs;
	}
}
