using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic.Utils;
using System.Runtime.CompilerServices;

namespace System.Linq.Expressions
{
	// Token: 0x02000269 RID: 617
	internal class DynamicExpression4 : DynamicExpression, IArgumentProvider
	{
		// Token: 0x0600112F RID: 4399 RVA: 0x00037FA9 File Offset: 0x000361A9
		internal DynamicExpression4(Type delegateType, CallSiteBinder binder, Expression arg0, Expression arg1, Expression arg2, Expression arg3)
			: base(delegateType, binder)
		{
			this._arg0 = arg0;
			this._arg1 = arg1;
			this._arg2 = arg2;
			this._arg3 = arg3;
		}

		// Token: 0x06001130 RID: 4400 RVA: 0x00037FD4 File Offset: 0x000361D4
		Expression IArgumentProvider.GetArgument(int index)
		{
			switch (index)
			{
			case 0:
				return ExpressionUtils.ReturnObject<Expression>(this._arg0);
			case 1:
				return this._arg1;
			case 2:
				return this._arg2;
			case 3:
				return this._arg3;
			default:
				throw new ArgumentOutOfRangeException("index");
			}
		}

		// Token: 0x170002F8 RID: 760
		// (get) Token: 0x06001131 RID: 4401 RVA: 0x00035690 File Offset: 0x00033890
		int IArgumentProvider.ArgumentCount
		{
			get
			{
				return 4;
			}
		}

		// Token: 0x06001132 RID: 4402 RVA: 0x00038024 File Offset: 0x00036224
		internal override bool SameArguments(ICollection<Expression> arguments)
		{
			if (arguments != null && arguments.Count == 4)
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
						if (enumerator.Current == this._arg1)
						{
							enumerator.MoveNext();
							if (enumerator.Current == this._arg2)
							{
								enumerator.MoveNext();
								return enumerator.Current == this._arg3;
							}
						}
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x06001133 RID: 4403 RVA: 0x000380D8 File Offset: 0x000362D8
		internal override ReadOnlyCollection<Expression> GetOrMakeArguments()
		{
			return ExpressionUtils.ReturnReadOnly(this, ref this._arg0);
		}

		// Token: 0x06001134 RID: 4404 RVA: 0x000380E6 File Offset: 0x000362E6
		internal override DynamicExpression Rewrite(Expression[] args)
		{
			return ExpressionExtension.MakeDynamic(base.DelegateType, base.Binder, args[0], args[1], args[2], args[3]);
		}

		// Token: 0x040008FA RID: 2298
		private object _arg0;

		// Token: 0x040008FB RID: 2299
		private readonly Expression _arg1;

		// Token: 0x040008FC RID: 2300
		private readonly Expression _arg2;

		// Token: 0x040008FD RID: 2301
		private readonly Expression _arg3;
	}
}
