using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic.Utils;
using System.Reflection;

namespace System.Linq.Expressions
{
	// Token: 0x020002A1 RID: 673
	internal sealed class InstanceMethodCallExpression2 : InstanceMethodCallExpression, IArgumentProvider
	{
		// Token: 0x060013A0 RID: 5024 RVA: 0x0003C7F6 File Offset: 0x0003A9F6
		public InstanceMethodCallExpression2(MethodInfo method, Expression instance, Expression arg0, Expression arg1)
			: base(method, instance)
		{
			this._arg0 = arg0;
			this._arg1 = arg1;
		}

		// Token: 0x060013A1 RID: 5025 RVA: 0x0003C80F File Offset: 0x0003AA0F
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

		// Token: 0x1700035D RID: 861
		// (get) Token: 0x060013A2 RID: 5026 RVA: 0x0003543C File Offset: 0x0003363C
		public override int ArgumentCount
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x060013A3 RID: 5027 RVA: 0x0003C838 File Offset: 0x0003AA38
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

		// Token: 0x060013A4 RID: 5028 RVA: 0x0003C8BC File Offset: 0x0003AABC
		internal override ReadOnlyCollection<Expression> GetOrMakeArguments()
		{
			return ExpressionUtils.ReturnReadOnly(this, ref this._arg0);
		}

		// Token: 0x060013A5 RID: 5029 RVA: 0x0003C8CA File Offset: 0x0003AACA
		internal override MethodCallExpression Rewrite(Expression instance, IReadOnlyList<Expression> args)
		{
			if (args != null)
			{
				return Expression.Call(instance, base.Method, args[0], args[1]);
			}
			return Expression.Call(instance, base.Method, ExpressionUtils.ReturnObject<Expression>(this._arg0), this._arg1);
		}

		// Token: 0x040009AC RID: 2476
		private object _arg0;

		// Token: 0x040009AD RID: 2477
		private readonly Expression _arg1;
	}
}
