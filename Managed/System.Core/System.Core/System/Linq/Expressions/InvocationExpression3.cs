using System;
using System.Collections.ObjectModel;
using System.Dynamic.Utils;

namespace System.Linq.Expressions
{
	// Token: 0x0200027D RID: 637
	internal sealed class InvocationExpression3 : InvocationExpression
	{
		// Token: 0x060012AC RID: 4780 RVA: 0x0003B1D3 File Offset: 0x000393D3
		public InvocationExpression3(Expression lambda, Type returnType, Expression arg0, Expression arg1, Expression arg2)
			: base(lambda, returnType)
		{
			this._arg0 = arg0;
			this._arg1 = arg1;
			this._arg2 = arg2;
		}

		// Token: 0x060012AD RID: 4781 RVA: 0x0003B1F4 File Offset: 0x000393F4
		internal override ReadOnlyCollection<Expression> GetOrMakeArguments()
		{
			return ExpressionUtils.ReturnReadOnly(this, ref this._arg0);
		}

		// Token: 0x060012AE RID: 4782 RVA: 0x0003B202 File Offset: 0x00039402
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

		// Token: 0x17000315 RID: 789
		// (get) Token: 0x060012AF RID: 4783 RVA: 0x0003554A File Offset: 0x0003374A
		public override int ArgumentCount
		{
			get
			{
				return 3;
			}
		}

		// Token: 0x060012B0 RID: 4784 RVA: 0x0003B23C File Offset: 0x0003943C
		internal override InvocationExpression Rewrite(Expression lambda, Expression[] arguments)
		{
			if (arguments != null)
			{
				return Expression.Invoke(lambda, arguments[0], arguments[1], arguments[2]);
			}
			return Expression.Invoke(lambda, ExpressionUtils.ReturnObject<Expression>(this._arg0), this._arg1, this._arg2);
		}

		// Token: 0x0400096B RID: 2411
		private object _arg0;

		// Token: 0x0400096C RID: 2412
		private readonly Expression _arg1;

		// Token: 0x0400096D RID: 2413
		private readonly Expression _arg2;
	}
}
