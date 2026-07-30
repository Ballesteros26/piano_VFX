using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace System.Web.Util
{
	// Token: 0x0200012C RID: 300
	internal static class QueryableUtility
	{
		// Token: 0x06000E42 RID: 3650 RVA: 0x00026B20 File Offset: 0x00024D20
		private static MethodInfo GetQueryableMethod(Expression expression)
		{
			if (expression.NodeType == ExpressionType.Call)
			{
				MethodCallExpression methodCallExpression = (MethodCallExpression)expression;
				if (methodCallExpression.Method.IsStatic && methodCallExpression.Method.DeclaringType == typeof(Queryable))
				{
					return methodCallExpression.Method.GetGenericMethodDefinition();
				}
			}
			return null;
		}

		// Token: 0x06000E43 RID: 3651 RVA: 0x00026B74 File Offset: 0x00024D74
		public static bool IsQueryableMethod(Expression expression, string method)
		{
			return QueryableUtility._methods.Where((MethodInfo m) => m.Name == method).Contains(QueryableUtility.GetQueryableMethod(expression));
		}

		// Token: 0x06000E44 RID: 3652 RVA: 0x00026BB0 File Offset: 0x00024DB0
		public static bool IsOrderingMethod(Expression expression)
		{
			return QueryableUtility._orderMethods.Any((string method) => QueryableUtility.IsQueryableMethod(expression, method));
		}

		// Token: 0x040011C4 RID: 4548
		private static readonly string[] _orderMethods = new string[] { "OrderBy", "ThenBy", "OrderByDescending", "ThenByDescending" };

		// Token: 0x040011C5 RID: 4549
		private static readonly MethodInfo[] _methods = typeof(Queryable).GetMethods(BindingFlags.Static | BindingFlags.Public);
	}
}
