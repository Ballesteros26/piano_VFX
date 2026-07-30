using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic.Utils;
using System.Reflection;

namespace System.Linq.Expressions
{
	// Token: 0x0200029D RID: 669
	internal sealed class MethodCallExpression4 : MethodCallExpression, IArgumentProvider
	{
		// Token: 0x06001388 RID: 5000 RVA: 0x0003C3A7 File Offset: 0x0003A5A7
		public MethodCallExpression4(MethodInfo method, Expression arg0, Expression arg1, Expression arg2, Expression arg3)
			: base(method)
		{
			this._arg0 = arg0;
			this._arg1 = arg1;
			this._arg2 = arg2;
			this._arg3 = arg3;
		}

		// Token: 0x06001389 RID: 5001 RVA: 0x0003C3D0 File Offset: 0x0003A5D0
		public override Expression GetArgument(int index)
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

		// Token: 0x17000359 RID: 857
		// (get) Token: 0x0600138A RID: 5002 RVA: 0x00035690 File Offset: 0x00033890
		public override int ArgumentCount
		{
			get
			{
				return 4;
			}
		}

		// Token: 0x0600138B RID: 5003 RVA: 0x0003C420 File Offset: 0x0003A620
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

		// Token: 0x0600138C RID: 5004 RVA: 0x0003C4D4 File Offset: 0x0003A6D4
		internal override ReadOnlyCollection<Expression> GetOrMakeArguments()
		{
			return ExpressionUtils.ReturnReadOnly(this, ref this._arg0);
		}

		// Token: 0x0600138D RID: 5005 RVA: 0x0003C4E4 File Offset: 0x0003A6E4
		internal override MethodCallExpression Rewrite(Expression instance, IReadOnlyList<Expression> args)
		{
			if (args != null)
			{
				return Expression.Call(base.Method, args[0], args[1], args[2], args[3]);
			}
			return Expression.Call(base.Method, ExpressionUtils.ReturnObject<Expression>(this._arg0), this._arg1, this._arg2, this._arg3);
		}

		// Token: 0x040009A2 RID: 2466
		private object _arg0;

		// Token: 0x040009A3 RID: 2467
		private readonly Expression _arg1;

		// Token: 0x040009A4 RID: 2468
		private readonly Expression _arg2;

		// Token: 0x040009A5 RID: 2469
		private readonly Expression _arg3;
	}
}
