using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic.Utils;
using System.Reflection;

namespace System.Linq.Expressions
{
	// Token: 0x020002A2 RID: 674
	internal sealed class InstanceMethodCallExpression3 : InstanceMethodCallExpression, IArgumentProvider
	{
		// Token: 0x060013A6 RID: 5030 RVA: 0x0003C907 File Offset: 0x0003AB07
		public InstanceMethodCallExpression3(MethodInfo method, Expression instance, Expression arg0, Expression arg1, Expression arg2)
			: base(method, instance)
		{
			this._arg0 = arg0;
			this._arg1 = arg1;
			this._arg2 = arg2;
		}

		// Token: 0x060013A7 RID: 5031 RVA: 0x0003C928 File Offset: 0x0003AB28
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

		// Token: 0x1700035E RID: 862
		// (get) Token: 0x060013A8 RID: 5032 RVA: 0x0003554A File Offset: 0x0003374A
		public override int ArgumentCount
		{
			get
			{
				return 3;
			}
		}

		// Token: 0x060013A9 RID: 5033 RVA: 0x0003C964 File Offset: 0x0003AB64
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

		// Token: 0x060013AA RID: 5034 RVA: 0x0003C9FC File Offset: 0x0003ABFC
		internal override ReadOnlyCollection<Expression> GetOrMakeArguments()
		{
			return ExpressionUtils.ReturnReadOnly(this, ref this._arg0);
		}

		// Token: 0x060013AB RID: 5035 RVA: 0x0003CA0C File Offset: 0x0003AC0C
		internal override MethodCallExpression Rewrite(Expression instance, IReadOnlyList<Expression> args)
		{
			if (args != null)
			{
				return Expression.Call(instance, base.Method, args[0], args[1], args[2]);
			}
			return Expression.Call(instance, base.Method, ExpressionUtils.ReturnObject<Expression>(this._arg0), this._arg1, this._arg2);
		}

		// Token: 0x040009AE RID: 2478
		private object _arg0;

		// Token: 0x040009AF RID: 2479
		private readonly Expression _arg1;

		// Token: 0x040009B0 RID: 2480
		private readonly Expression _arg2;
	}
}
