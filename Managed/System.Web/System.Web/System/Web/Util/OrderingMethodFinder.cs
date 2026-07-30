using System;
using System.Linq.Expressions;

namespace System.Web.Util
{
	// Token: 0x02000129 RID: 297
	internal sealed class OrderingMethodFinder : ExpressionVisitor
	{
		// Token: 0x170004E0 RID: 1248
		// (get) Token: 0x06000E36 RID: 3638 RVA: 0x000265FD File Offset: 0x000247FD
		// (set) Token: 0x06000E37 RID: 3639 RVA: 0x00026605 File Offset: 0x00024805
		private bool OrderingMethodFound { get; set; }

		// Token: 0x06000E38 RID: 3640 RVA: 0x0002660E File Offset: 0x0002480E
		protected override Expression VisitMethodCall(MethodCallExpression node)
		{
			if (this.isTopLevelMethodCall && QueryableUtility.IsOrderingMethod(node))
			{
				this.OrderingMethodFound = true;
			}
			this.isTopLevelMethodCall = false;
			Expression expression = base.VisitMethodCall(node);
			this.isTopLevelMethodCall = true;
			return expression;
		}

		// Token: 0x06000E39 RID: 3641 RVA: 0x0002663C File Offset: 0x0002483C
		internal static bool OrderMethodExists(Expression expression)
		{
			OrderingMethodFinder orderingMethodFinder = new OrderingMethodFinder();
			orderingMethodFinder.OrderingMethodFound = false;
			orderingMethodFinder.Visit(expression);
			return orderingMethodFinder.OrderingMethodFound;
		}

		// Token: 0x040011BD RID: 4541
		private bool isTopLevelMethodCall = true;
	}
}
