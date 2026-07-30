using System;
using System.Reflection;

namespace System.Linq.Expressions
{
	// Token: 0x02000224 RID: 548
	internal sealed class OpAssignMethodConversionBinaryExpression : MethodBinaryExpression
	{
		// Token: 0x06000DA7 RID: 3495 RVA: 0x0002D7D3 File Offset: 0x0002B9D3
		internal OpAssignMethodConversionBinaryExpression(ExpressionType nodeType, Expression left, Expression right, Type type, MethodInfo method, LambdaExpression conversion)
			: base(nodeType, left, right, type, method)
		{
			this._conversion = conversion;
		}

		// Token: 0x06000DA8 RID: 3496 RVA: 0x0002D7EA File Offset: 0x0002B9EA
		internal override LambdaExpression GetConversion()
		{
			return this._conversion;
		}

		// Token: 0x04000885 RID: 2181
		private readonly LambdaExpression _conversion;
	}
}
