using System;
using System.Runtime.CompilerServices;

namespace System.Linq.Expressions
{
	// Token: 0x02000268 RID: 616
	internal sealed class TypedDynamicExpression3 : DynamicExpression3
	{
		// Token: 0x0600112D RID: 4397 RVA: 0x00037F8A File Offset: 0x0003618A
		internal TypedDynamicExpression3(Type retType, Type delegateType, CallSiteBinder binder, Expression arg0, Expression arg1, Expression arg2)
			: base(delegateType, binder, arg0, arg1, arg2)
		{
			this.Type = retType;
		}

		// Token: 0x170002F7 RID: 759
		// (get) Token: 0x0600112E RID: 4398 RVA: 0x00037FA1 File Offset: 0x000361A1
		public sealed override Type Type { get; }
	}
}
