using System;
using System.Collections.ObjectModel;
using System.Dynamic.Utils;

namespace System.Linq.Expressions
{
	// Token: 0x0200027B RID: 635
	internal sealed class InvocationExpression1 : InvocationExpression
	{
		// Token: 0x060012A2 RID: 4770 RVA: 0x0003B101 File Offset: 0x00039301
		public InvocationExpression1(Expression lambda, Type returnType, Expression arg0)
			: base(lambda, returnType)
		{
			this._arg0 = arg0;
		}

		// Token: 0x060012A3 RID: 4771 RVA: 0x0003B112 File Offset: 0x00039312
		internal override ReadOnlyCollection<Expression> GetOrMakeArguments()
		{
			return ExpressionUtils.ReturnReadOnly(this, ref this._arg0);
		}

		// Token: 0x060012A4 RID: 4772 RVA: 0x0003B120 File Offset: 0x00039320
		public override Expression GetArgument(int index)
		{
			if (index == 0)
			{
				return ExpressionUtils.ReturnObject<Expression>(this._arg0);
			}
			throw new ArgumentOutOfRangeException("index");
		}

		// Token: 0x17000313 RID: 787
		// (get) Token: 0x060012A5 RID: 4773 RVA: 0x0000AA13 File Offset: 0x00008C13
		public override int ArgumentCount
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x060012A6 RID: 4774 RVA: 0x0003B13B File Offset: 0x0003933B
		internal override InvocationExpression Rewrite(Expression lambda, Expression[] arguments)
		{
			if (arguments != null)
			{
				return Expression.Invoke(lambda, arguments[0]);
			}
			return Expression.Invoke(lambda, ExpressionUtils.ReturnObject<Expression>(this._arg0));
		}

		// Token: 0x04000968 RID: 2408
		private object _arg0;
	}
}
