using System;
using System.Runtime.CompilerServices;

namespace System.Linq.Expressions
{
	// Token: 0x02000266 RID: 614
	internal sealed class TypedDynamicExpression2 : DynamicExpression2
	{
		// Token: 0x06001125 RID: 4389 RVA: 0x00037E4F File Offset: 0x0003604F
		internal TypedDynamicExpression2(Type retType, Type delegateType, CallSiteBinder binder, Expression arg0, Expression arg1)
			: base(delegateType, binder, arg0, arg1)
		{
			this.Type = retType;
		}

		// Token: 0x170002F5 RID: 757
		// (get) Token: 0x06001126 RID: 4390 RVA: 0x00037E64 File Offset: 0x00036064
		public sealed override Type Type { get; }
	}
}
