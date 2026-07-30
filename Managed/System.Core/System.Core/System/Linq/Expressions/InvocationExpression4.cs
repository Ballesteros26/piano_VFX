using System;
using System.Collections.ObjectModel;
using System.Dynamic.Utils;

namespace System.Linq.Expressions
{
	// Token: 0x0200027E RID: 638
	internal sealed class InvocationExpression4 : InvocationExpression
	{
		// Token: 0x060012B1 RID: 4785 RVA: 0x0003B26E File Offset: 0x0003946E
		public InvocationExpression4(Expression lambda, Type returnType, Expression arg0, Expression arg1, Expression arg2, Expression arg3)
			: base(lambda, returnType)
		{
			this._arg0 = arg0;
			this._arg1 = arg1;
			this._arg2 = arg2;
			this._arg3 = arg3;
		}

		// Token: 0x060012B2 RID: 4786 RVA: 0x0003B297 File Offset: 0x00039497
		internal override ReadOnlyCollection<Expression> GetOrMakeArguments()
		{
			return ExpressionUtils.ReturnReadOnly(this, ref this._arg0);
		}

		// Token: 0x060012B3 RID: 4787 RVA: 0x0003B2A8 File Offset: 0x000394A8
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

		// Token: 0x17000316 RID: 790
		// (get) Token: 0x060012B4 RID: 4788 RVA: 0x00035690 File Offset: 0x00033890
		public override int ArgumentCount
		{
			get
			{
				return 4;
			}
		}

		// Token: 0x060012B5 RID: 4789 RVA: 0x0003B2F8 File Offset: 0x000394F8
		internal override InvocationExpression Rewrite(Expression lambda, Expression[] arguments)
		{
			if (arguments != null)
			{
				return Expression.Invoke(lambda, arguments[0], arguments[1], arguments[2], arguments[3]);
			}
			return Expression.Invoke(lambda, ExpressionUtils.ReturnObject<Expression>(this._arg0), this._arg1, this._arg2, this._arg3);
		}

		// Token: 0x0400096E RID: 2414
		private object _arg0;

		// Token: 0x0400096F RID: 2415
		private readonly Expression _arg1;

		// Token: 0x04000970 RID: 2416
		private readonly Expression _arg2;

		// Token: 0x04000971 RID: 2417
		private readonly Expression _arg3;
	}
}
