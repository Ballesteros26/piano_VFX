using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic.Utils;
using System.Reflection;

namespace System.Linq.Expressions
{
	// Token: 0x0200029B RID: 667
	internal sealed class MethodCallExpression2 : MethodCallExpression, IArgumentProvider
	{
		// Token: 0x0600137C RID: 4988 RVA: 0x0003C142 File Offset: 0x0003A342
		public MethodCallExpression2(MethodInfo method, Expression arg0, Expression arg1)
			: base(method)
		{
			this._arg0 = arg0;
			this._arg1 = arg1;
		}

		// Token: 0x0600137D RID: 4989 RVA: 0x0003C159 File Offset: 0x0003A359
		public override Expression GetArgument(int index)
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

		// Token: 0x17000357 RID: 855
		// (get) Token: 0x0600137E RID: 4990 RVA: 0x0003543C File Offset: 0x0003363C
		public override int ArgumentCount
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x0600137F RID: 4991 RVA: 0x0003C184 File Offset: 0x0003A384
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

		// Token: 0x06001380 RID: 4992 RVA: 0x0003C208 File Offset: 0x0003A408
		internal override ReadOnlyCollection<Expression> GetOrMakeArguments()
		{
			return ExpressionUtils.ReturnReadOnly(this, ref this._arg0);
		}

		// Token: 0x06001381 RID: 4993 RVA: 0x0003C216 File Offset: 0x0003A416
		internal override MethodCallExpression Rewrite(Expression instance, IReadOnlyList<Expression> args)
		{
			if (args != null)
			{
				return Expression.Call(base.Method, args[0], args[1]);
			}
			return Expression.Call(base.Method, ExpressionUtils.ReturnObject<Expression>(this._arg0), this._arg1);
		}

		// Token: 0x0400099D RID: 2461
		private object _arg0;

		// Token: 0x0400099E RID: 2462
		private readonly Expression _arg1;
	}
}
