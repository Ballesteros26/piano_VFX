using System;
using System.Collections.ObjectModel;
using System.Dynamic.Utils;

namespace System.Linq.Expressions
{
	// Token: 0x0200027C RID: 636
	internal sealed class InvocationExpression2 : InvocationExpression
	{
		// Token: 0x060012A7 RID: 4775 RVA: 0x0003B15B File Offset: 0x0003935B
		public InvocationExpression2(Expression lambda, Type returnType, Expression arg0, Expression arg1)
			: base(lambda, returnType)
		{
			this._arg0 = arg0;
			this._arg1 = arg1;
		}

		// Token: 0x060012A8 RID: 4776 RVA: 0x0003B174 File Offset: 0x00039374
		internal override ReadOnlyCollection<Expression> GetOrMakeArguments()
		{
			return ExpressionUtils.ReturnReadOnly(this, ref this._arg0);
		}

		// Token: 0x060012A9 RID: 4777 RVA: 0x0003B182 File Offset: 0x00039382
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

		// Token: 0x17000314 RID: 788
		// (get) Token: 0x060012AA RID: 4778 RVA: 0x0003543C File Offset: 0x0003363C
		public override int ArgumentCount
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x060012AB RID: 4779 RVA: 0x0003B1AA File Offset: 0x000393AA
		internal override InvocationExpression Rewrite(Expression lambda, Expression[] arguments)
		{
			if (arguments != null)
			{
				return Expression.Invoke(lambda, arguments[0], arguments[1]);
			}
			return Expression.Invoke(lambda, ExpressionUtils.ReturnObject<Expression>(this._arg0), this._arg1);
		}

		// Token: 0x04000969 RID: 2409
		private object _arg0;

		// Token: 0x0400096A RID: 2410
		private readonly Expression _arg1;
	}
}
