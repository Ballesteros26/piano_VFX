using System;
using System.Collections.Generic;

namespace System.Xml.Xsl.Qil
{
	// Token: 0x02000641 RID: 1601
	internal class QilScopedVisitor : QilVisitor
	{
		// Token: 0x06003FE8 RID: 16360 RVA: 0x00002F50 File Offset: 0x00001150
		protected virtual void BeginScope(QilNode node)
		{
		}

		// Token: 0x06003FE9 RID: 16361 RVA: 0x00002F50 File Offset: 0x00001150
		protected virtual void EndScope(QilNode node)
		{
		}

		// Token: 0x06003FEA RID: 16362 RVA: 0x00158B48 File Offset: 0x00156D48
		protected virtual void BeforeVisit(QilNode node)
		{
			QilNodeType nodeType = node.NodeType;
			if (nodeType != QilNodeType.QilExpression)
			{
				if (nodeType - QilNodeType.Loop <= 2)
				{
					goto IL_00EF;
				}
				if (nodeType != QilNodeType.Function)
				{
					return;
				}
			}
			else
			{
				QilExpression qilExpression = (QilExpression)node;
				foreach (QilNode qilNode in qilExpression.GlobalParameterList)
				{
					this.BeginScope(qilNode);
				}
				foreach (QilNode qilNode2 in qilExpression.GlobalVariableList)
				{
					this.BeginScope(qilNode2);
				}
				using (IEnumerator<QilNode> enumerator = qilExpression.FunctionList.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						QilNode qilNode3 = enumerator.Current;
						this.BeginScope(qilNode3);
					}
					return;
				}
			}
			using (IEnumerator<QilNode> enumerator = ((QilFunction)node).Arguments.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					QilNode qilNode4 = enumerator.Current;
					this.BeginScope(qilNode4);
				}
				return;
			}
			IL_00EF:
			this.BeginScope(((QilLoop)node).Variable);
		}

		// Token: 0x06003FEB RID: 16363 RVA: 0x00158C8C File Offset: 0x00156E8C
		protected virtual void AfterVisit(QilNode node)
		{
			QilNodeType nodeType = node.NodeType;
			if (nodeType != QilNodeType.QilExpression)
			{
				if (nodeType - QilNodeType.Loop <= 2)
				{
					goto IL_00EF;
				}
				if (nodeType != QilNodeType.Function)
				{
					return;
				}
			}
			else
			{
				QilExpression qilExpression = (QilExpression)node;
				foreach (QilNode qilNode in qilExpression.FunctionList)
				{
					this.EndScope(qilNode);
				}
				foreach (QilNode qilNode2 in qilExpression.GlobalVariableList)
				{
					this.EndScope(qilNode2);
				}
				using (IEnumerator<QilNode> enumerator = qilExpression.GlobalParameterList.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						QilNode qilNode3 = enumerator.Current;
						this.EndScope(qilNode3);
					}
					return;
				}
			}
			using (IEnumerator<QilNode> enumerator = ((QilFunction)node).Arguments.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					QilNode qilNode4 = enumerator.Current;
					this.EndScope(qilNode4);
				}
				return;
			}
			IL_00EF:
			this.EndScope(((QilLoop)node).Variable);
		}

		// Token: 0x06003FEC RID: 16364 RVA: 0x00158DD0 File Offset: 0x00156FD0
		protected override QilNode Visit(QilNode n)
		{
			this.BeforeVisit(n);
			QilNode qilNode = base.Visit(n);
			this.AfterVisit(n);
			return qilNode;
		}
	}
}
