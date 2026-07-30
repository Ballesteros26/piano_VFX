using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic.Utils;
using System.Reflection;

namespace System.Linq.Expressions
{
	// Token: 0x0200029E RID: 670
	internal sealed class MethodCallExpression5 : MethodCallExpression, IArgumentProvider
	{
		// Token: 0x0600138E RID: 5006 RVA: 0x0003C544 File Offset: 0x0003A744
		public MethodCallExpression5(MethodInfo method, Expression arg0, Expression arg1, Expression arg2, Expression arg3, Expression arg4)
			: base(method)
		{
			this._arg0 = arg0;
			this._arg1 = arg1;
			this._arg2 = arg2;
			this._arg3 = arg3;
			this._arg4 = arg4;
		}

		// Token: 0x0600138F RID: 5007 RVA: 0x0003C574 File Offset: 0x0003A774
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
			case 4:
				return this._arg4;
			default:
				throw new ArgumentOutOfRangeException("index");
			}
		}

		// Token: 0x1700035A RID: 858
		// (get) Token: 0x06001390 RID: 5008 RVA: 0x00035804 File Offset: 0x00033A04
		public override int ArgumentCount
		{
			get
			{
				return 5;
			}
		}

		// Token: 0x06001391 RID: 5009 RVA: 0x0003C5D0 File Offset: 0x0003A7D0
		internal override bool SameArguments(ICollection<Expression> arguments)
		{
			if (arguments != null && arguments.Count == 5)
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
								if (enumerator.Current == this._arg3)
								{
									enumerator.MoveNext();
									return enumerator.Current == this._arg4;
								}
							}
						}
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x06001392 RID: 5010 RVA: 0x0003C698 File Offset: 0x0003A898
		internal override ReadOnlyCollection<Expression> GetOrMakeArguments()
		{
			return ExpressionUtils.ReturnReadOnly(this, ref this._arg0);
		}

		// Token: 0x06001393 RID: 5011 RVA: 0x0003C6A8 File Offset: 0x0003A8A8
		internal override MethodCallExpression Rewrite(Expression instance, IReadOnlyList<Expression> args)
		{
			if (args != null)
			{
				return Expression.Call(base.Method, args[0], args[1], args[2], args[3], args[4]);
			}
			return Expression.Call(base.Method, ExpressionUtils.ReturnObject<Expression>(this._arg0), this._arg1, this._arg2, this._arg3, this._arg4);
		}

		// Token: 0x040009A6 RID: 2470
		private object _arg0;

		// Token: 0x040009A7 RID: 2471
		private readonly Expression _arg1;

		// Token: 0x040009A8 RID: 2472
		private readonly Expression _arg2;

		// Token: 0x040009A9 RID: 2473
		private readonly Expression _arg3;

		// Token: 0x040009AA RID: 2474
		private readonly Expression _arg4;
	}
}
