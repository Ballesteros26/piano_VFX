using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic.Utils;
using System.Runtime.CompilerServices;

namespace System.Linq.Expressions
{
	// Token: 0x02000263 RID: 611
	internal class DynamicExpression1 : DynamicExpression, IArgumentProvider
	{
		// Token: 0x06001117 RID: 4375 RVA: 0x00037C99 File Offset: 0x00035E99
		internal DynamicExpression1(Type delegateType, CallSiteBinder binder, Expression arg0)
			: base(delegateType, binder)
		{
			this._arg0 = arg0;
		}

		// Token: 0x06001118 RID: 4376 RVA: 0x00037CAA File Offset: 0x00035EAA
		Expression IArgumentProvider.GetArgument(int index)
		{
			if (index == 0)
			{
				return ExpressionUtils.ReturnObject<Expression>(this._arg0);
			}
			throw new ArgumentOutOfRangeException("index");
		}

		// Token: 0x170002F2 RID: 754
		// (get) Token: 0x06001119 RID: 4377 RVA: 0x0000AA13 File Offset: 0x00008C13
		int IArgumentProvider.ArgumentCount
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x0600111A RID: 4378 RVA: 0x00037CC8 File Offset: 0x00035EC8
		internal override bool SameArguments(ICollection<Expression> arguments)
		{
			if (arguments != null && arguments.Count == 1)
			{
				using (IEnumerator<Expression> enumerator = arguments.GetEnumerator())
				{
					enumerator.MoveNext();
					return enumerator.Current == ExpressionUtils.ReturnObject<Expression>(this._arg0);
				}
				return false;
			}
			return false;
		}

		// Token: 0x0600111B RID: 4379 RVA: 0x00037D24 File Offset: 0x00035F24
		internal override ReadOnlyCollection<Expression> GetOrMakeArguments()
		{
			return ExpressionUtils.ReturnReadOnly(this, ref this._arg0);
		}

		// Token: 0x0600111C RID: 4380 RVA: 0x00037D32 File Offset: 0x00035F32
		internal override DynamicExpression Rewrite(Expression[] args)
		{
			return ExpressionExtension.MakeDynamic(base.DelegateType, base.Binder, args[0]);
		}

		// Token: 0x040008F1 RID: 2289
		private object _arg0;
	}
}
