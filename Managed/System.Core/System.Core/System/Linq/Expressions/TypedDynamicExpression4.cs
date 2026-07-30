using System;
using System.Runtime.CompilerServices;

namespace System.Linq.Expressions
{
	// Token: 0x0200026A RID: 618
	internal sealed class TypedDynamicExpression4 : DynamicExpression4
	{
		// Token: 0x06001135 RID: 4405 RVA: 0x00038105 File Offset: 0x00036305
		internal TypedDynamicExpression4(Type retType, Type delegateType, CallSiteBinder binder, Expression arg0, Expression arg1, Expression arg2, Expression arg3)
			: base(delegateType, binder, arg0, arg1, arg2, arg3)
		{
			this.Type = retType;
		}

		// Token: 0x170002F9 RID: 761
		// (get) Token: 0x06001136 RID: 4406 RVA: 0x0003811E File Offset: 0x0003631E
		public sealed override Type Type { get; }
	}
}
