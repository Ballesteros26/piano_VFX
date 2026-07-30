using System;
using System.Collections.ObjectModel;
using System.Dynamic.Utils;

namespace System.Linq.Expressions
{
	// Token: 0x0200027F RID: 639
	internal sealed class InvocationExpression5 : InvocationExpression
	{
		// Token: 0x060012B6 RID: 4790 RVA: 0x0003B333 File Offset: 0x00039533
		public InvocationExpression5(Expression lambda, Type returnType, Expression arg0, Expression arg1, Expression arg2, Expression arg3, Expression arg4)
			: base(lambda, returnType)
		{
			this._arg0 = arg0;
			this._arg1 = arg1;
			this._arg2 = arg2;
			this._arg3 = arg3;
			this._arg4 = arg4;
		}

		// Token: 0x060012B7 RID: 4791 RVA: 0x0003B364 File Offset: 0x00039564
		internal override ReadOnlyCollection<Expression> GetOrMakeArguments()
		{
			return ExpressionUtils.ReturnReadOnly(this, ref this._arg0);
		}

		// Token: 0x060012B8 RID: 4792 RVA: 0x0003B374 File Offset: 0x00039574
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

		// Token: 0x17000317 RID: 791
		// (get) Token: 0x060012B9 RID: 4793 RVA: 0x00035804 File Offset: 0x00033A04
		public override int ArgumentCount
		{
			get
			{
				return 5;
			}
		}

		// Token: 0x060012BA RID: 4794 RVA: 0x0003B3D0 File Offset: 0x000395D0
		internal override InvocationExpression Rewrite(Expression lambda, Expression[] arguments)
		{
			if (arguments != null)
			{
				return Expression.Invoke(lambda, arguments[0], arguments[1], arguments[2], arguments[3], arguments[4]);
			}
			return Expression.Invoke(lambda, ExpressionUtils.ReturnObject<Expression>(this._arg0), this._arg1, this._arg2, this._arg3, this._arg4);
		}

		// Token: 0x04000972 RID: 2418
		private object _arg0;

		// Token: 0x04000973 RID: 2419
		private readonly Expression _arg1;

		// Token: 0x04000974 RID: 2420
		private readonly Expression _arg2;

		// Token: 0x04000975 RID: 2421
		private readonly Expression _arg3;

		// Token: 0x04000976 RID: 2422
		private readonly Expression _arg4;
	}
}
