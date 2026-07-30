using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic.Utils;
using System.Runtime.CompilerServices;

namespace System.Linq.Expressions
{
	// Token: 0x02000265 RID: 613
	internal class DynamicExpression2 : DynamicExpression, IArgumentProvider
	{
		// Token: 0x0600111F RID: 4383 RVA: 0x00037D63 File Offset: 0x00035F63
		internal DynamicExpression2(Type delegateType, CallSiteBinder binder, Expression arg0, Expression arg1)
			: base(delegateType, binder)
		{
			this._arg0 = arg0;
			this._arg1 = arg1;
		}

		// Token: 0x06001120 RID: 4384 RVA: 0x00037D7C File Offset: 0x00035F7C
		Expression IArgumentProvider.GetArgument(int index)
		{
			if (index == 0)
			{
				return ExpressionUtils.ReturnObject<Expression>(this._arg0);
			}
			if (index != 1)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			return this._arg1;
		}

		// Token: 0x170002F4 RID: 756
		// (get) Token: 0x06001121 RID: 4385 RVA: 0x0003543C File Offset: 0x0003363C
		int IArgumentProvider.ArgumentCount
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x06001122 RID: 4386 RVA: 0x00037DA4 File Offset: 0x00035FA4
		internal override bool SameArguments(ICollection<Expression> arguments)
		{
			if (arguments != null && arguments.Count == 2)
			{
				ReadOnlyCollection<Expression> readOnlyCollection = this._arg0 as ReadOnlyCollection<Expression>;
				if (readOnlyCollection != null)
				{
					return ExpressionUtils.SameElements<Expression>(arguments, readOnlyCollection);
				}
				using (IEnumerator<Expression> enumerator = arguments.GetEnumerator())
				{
					enumerator.MoveNext();
					if (enumerator.Current == this._arg0)
					{
						enumerator.MoveNext();
						return enumerator.Current == this._arg1;
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x06001123 RID: 4387 RVA: 0x00037E28 File Offset: 0x00036028
		internal override ReadOnlyCollection<Expression> GetOrMakeArguments()
		{
			return ExpressionUtils.ReturnReadOnly(this, ref this._arg0);
		}

		// Token: 0x06001124 RID: 4388 RVA: 0x00037E36 File Offset: 0x00036036
		internal override DynamicExpression Rewrite(Expression[] args)
		{
			return ExpressionExtension.MakeDynamic(base.DelegateType, base.Binder, args[0], args[1]);
		}

		// Token: 0x040008F3 RID: 2291
		private object _arg0;

		// Token: 0x040008F4 RID: 2292
		private readonly Expression _arg1;
	}
}
