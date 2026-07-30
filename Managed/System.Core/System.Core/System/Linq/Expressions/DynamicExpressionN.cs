using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic.Utils;
using System.Runtime.CompilerServices;

namespace System.Linq.Expressions
{
	// Token: 0x02000261 RID: 609
	internal class DynamicExpressionN : DynamicExpression, IArgumentProvider
	{
		// Token: 0x0600110F RID: 4367 RVA: 0x00037C23 File Offset: 0x00035E23
		internal DynamicExpressionN(Type delegateType, CallSiteBinder binder, IReadOnlyList<Expression> arguments)
			: base(delegateType, binder)
		{
			this._arguments = arguments;
		}

		// Token: 0x06001110 RID: 4368 RVA: 0x00037C34 File Offset: 0x00035E34
		Expression IArgumentProvider.GetArgument(int index)
		{
			return this._arguments[index];
		}

		// Token: 0x06001111 RID: 4369 RVA: 0x00037C42 File Offset: 0x00035E42
		internal override bool SameArguments(ICollection<Expression> arguments)
		{
			return ExpressionUtils.SameElements<Expression>(arguments, this._arguments);
		}

		// Token: 0x170002F0 RID: 752
		// (get) Token: 0x06001112 RID: 4370 RVA: 0x00037C50 File Offset: 0x00035E50
		int IArgumentProvider.ArgumentCount
		{
			get
			{
				return this._arguments.Count;
			}
		}

		// Token: 0x06001113 RID: 4371 RVA: 0x00037C5D File Offset: 0x00035E5D
		internal override ReadOnlyCollection<Expression> GetOrMakeArguments()
		{
			return ExpressionUtils.ReturnReadOnly<Expression>(ref this._arguments);
		}

		// Token: 0x06001114 RID: 4372 RVA: 0x00037C6A File Offset: 0x00035E6A
		internal override DynamicExpression Rewrite(Expression[] args)
		{
			return ExpressionExtension.MakeDynamic(base.DelegateType, base.Binder, args);
		}

		// Token: 0x040008EF RID: 2287
		private IReadOnlyList<Expression> _arguments;
	}
}
