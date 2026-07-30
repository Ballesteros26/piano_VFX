using System;
using System.Runtime.CompilerServices;

namespace System.Linq.Expressions
{
	// Token: 0x02000264 RID: 612
	internal sealed class TypedDynamicExpression1 : DynamicExpression1
	{
		// Token: 0x0600111D RID: 4381 RVA: 0x00037D48 File Offset: 0x00035F48
		internal TypedDynamicExpression1(Type retType, Type delegateType, CallSiteBinder binder, Expression arg0)
			: base(delegateType, binder, arg0)
		{
			this.Type = retType;
		}

		// Token: 0x170002F3 RID: 755
		// (get) Token: 0x0600111E RID: 4382 RVA: 0x00037D5B File Offset: 0x00035F5B
		public sealed override Type Type { get; }
	}
}
