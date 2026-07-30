using System;

namespace System.Linq.Expressions
{
	// Token: 0x020002AB RID: 683
	internal sealed class PrimitiveParameterExpression<T> : ParameterExpression
	{
		// Token: 0x060013D1 RID: 5073 RVA: 0x0003CD5D File Offset: 0x0003AF5D
		internal PrimitiveParameterExpression(string name)
			: base(name)
		{
		}

		// Token: 0x1700036F RID: 879
		// (get) Token: 0x060013D2 RID: 5074 RVA: 0x0000C4DE File Offset: 0x0000A6DE
		public sealed override Type Type
		{
			get
			{
				return typeof(T);
			}
		}
	}
}
