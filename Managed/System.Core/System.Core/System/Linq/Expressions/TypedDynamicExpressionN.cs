using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Linq.Expressions
{
	// Token: 0x02000262 RID: 610
	internal class TypedDynamicExpressionN : DynamicExpressionN
	{
		// Token: 0x06001115 RID: 4373 RVA: 0x00037C7E File Offset: 0x00035E7E
		internal TypedDynamicExpressionN(Type returnType, Type delegateType, CallSiteBinder binder, IReadOnlyList<Expression> arguments)
			: base(delegateType, binder, arguments)
		{
			this.Type = returnType;
		}

		// Token: 0x170002F1 RID: 753
		// (get) Token: 0x06001116 RID: 4374 RVA: 0x00037C91 File Offset: 0x00035E91
		public sealed override Type Type { get; }
	}
}
