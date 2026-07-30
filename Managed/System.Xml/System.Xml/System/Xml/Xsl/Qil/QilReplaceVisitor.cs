using System;

namespace System.Xml.Xsl.Qil
{
	// Token: 0x02000640 RID: 1600
	internal abstract class QilReplaceVisitor : QilVisitor
	{
		// Token: 0x06003FE5 RID: 16357 RVA: 0x00158A86 File Offset: 0x00156C86
		public QilReplaceVisitor(QilFactory f)
		{
			this.f = f;
		}

		// Token: 0x06003FE6 RID: 16358 RVA: 0x00158A98 File Offset: 0x00156C98
		protected override QilNode VisitChildren(QilNode parent)
		{
			XmlQueryType xmlType = parent.XmlType;
			bool flag = false;
			for (int i = 0; i < parent.Count; i++)
			{
				QilNode qilNode = parent[i];
				XmlQueryType xmlQueryType = ((qilNode != null) ? qilNode.XmlType : null);
				QilNode qilNode2;
				if (this.IsReference(parent, i))
				{
					qilNode2 = this.VisitReference(qilNode);
				}
				else
				{
					qilNode2 = this.Visit(qilNode);
				}
				if (qilNode != qilNode2 || (qilNode2 != null && xmlQueryType != qilNode2.XmlType))
				{
					flag = true;
					parent[i] = qilNode2;
				}
			}
			if (flag)
			{
				this.RecalculateType(parent, xmlType);
			}
			return parent;
		}

		// Token: 0x06003FE7 RID: 16359 RVA: 0x00158B20 File Offset: 0x00156D20
		protected virtual void RecalculateType(QilNode node, XmlQueryType oldType)
		{
			XmlQueryType xmlQueryType = this.f.TypeChecker.Check(node);
			node.XmlType = xmlQueryType;
		}

		// Token: 0x040028C7 RID: 10439
		protected QilFactory f;
	}
}
