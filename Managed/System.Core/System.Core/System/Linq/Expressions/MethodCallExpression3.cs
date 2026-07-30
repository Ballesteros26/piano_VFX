using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic.Utils;
using System.Reflection;

namespace System.Linq.Expressions
{
	// Token: 0x0200029C RID: 668
	internal sealed class MethodCallExpression3 : MethodCallExpression, IArgumentProvider
	{
		// Token: 0x06001382 RID: 4994 RVA: 0x0003C251 File Offset: 0x0003A451
		public MethodCallExpression3(MethodInfo method, Expression arg0, Expression arg1, Expression arg2)
			: base(method)
		{
			this._arg0 = arg0;
			this._arg1 = arg1;
			this._arg2 = arg2;
		}

		// Token: 0x06001383 RID: 4995 RVA: 0x0003C270 File Offset: 0x0003A470
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
			default:
				throw new ArgumentOutOfRangeException("index");
			}
		}

		// Token: 0x17000358 RID: 856
		// (get) Token: 0x06001384 RID: 4996 RVA: 0x0003554A File Offset: 0x0003374A
		public override int ArgumentCount
		{
			get
			{
				return 3;
			}
		}

		// Token: 0x06001385 RID: 4997 RVA: 0x0003C2AC File Offset: 0x0003A4AC
		internal override bool SameArguments(ICollection<Expression> arguments)
		{
			if (arguments != null && arguments.Count == 3)
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
							return enumerator.Current == this._arg2;
						}
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x06001386 RID: 4998 RVA: 0x0003C344 File Offset: 0x0003A544
		internal override ReadOnlyCollection<Expression> GetOrMakeArguments()
		{
			return ExpressionUtils.ReturnReadOnly(this, ref this._arg0);
		}

		// Token: 0x06001387 RID: 4999 RVA: 0x0003C354 File Offset: 0x0003A554
		internal override MethodCallExpression Rewrite(Expression instance, IReadOnlyList<Expression> args)
		{
			if (args != null)
			{
				return Expression.Call(base.Method, args[0], args[1], args[2]);
			}
			return Expression.Call(base.Method, ExpressionUtils.ReturnObject<Expression>(this._arg0), this._arg1, this._arg2);
		}

		// Token: 0x0400099F RID: 2463
		private object _arg0;

		// Token: 0x040009A0 RID: 2464
		private readonly Expression _arg1;

		// Token: 0x040009A1 RID: 2465
		private readonly Expression _arg2;
	}
}
