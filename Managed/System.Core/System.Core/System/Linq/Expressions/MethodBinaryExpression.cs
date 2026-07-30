using System;
using System.Reflection;

namespace System.Linq.Expressions
{
	// Token: 0x02000226 RID: 550
	internal class MethodBinaryExpression : SimpleBinaryExpression
	{
		// Token: 0x06000DAC RID: 3500 RVA: 0x0002D81B File Offset: 0x0002BA1B
		internal MethodBinaryExpression(ExpressionType nodeType, Expression left, Expression right, Type type, MethodInfo method)
			: base(nodeType, left, right, type)
		{
			this._method = method;
		}

		// Token: 0x06000DAD RID: 3501 RVA: 0x0002D830 File Offset: 0x0002BA30
		internal override MethodInfo GetMethod()
		{
			return this._method;
		}

		// Token: 0x04000888 RID: 2184
		private readonly MethodInfo _method;
	}
}
