using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic.Utils;

namespace System.Linq.Expressions
{
	// Token: 0x02000279 RID: 633
	internal sealed class InvocationExpressionN : InvocationExpression
	{
		// Token: 0x06001298 RID: 4760 RVA: 0x0003B081 File Offset: 0x00039281
		public InvocationExpressionN(Expression lambda, IReadOnlyList<Expression> arguments, Type returnType)
			: base(lambda, returnType)
		{
			this._arguments = arguments;
		}

		// Token: 0x06001299 RID: 4761 RVA: 0x0003B092 File Offset: 0x00039292
		internal override ReadOnlyCollection<Expression> GetOrMakeArguments()
		{
			return ExpressionUtils.ReturnReadOnly<Expression>(ref this._arguments);
		}

		// Token: 0x0600129A RID: 4762 RVA: 0x0003B09F File Offset: 0x0003929F
		public override Expression GetArgument(int index)
		{
			return this._arguments[index];
		}

		// Token: 0x17000311 RID: 785
		// (get) Token: 0x0600129B RID: 4763 RVA: 0x0003B0AD File Offset: 0x000392AD
		public override int ArgumentCount
		{
			get
			{
				return this._arguments.Count;
			}
		}

		// Token: 0x0600129C RID: 4764 RVA: 0x0003B0BC File Offset: 0x000392BC
		internal override InvocationExpression Rewrite(Expression lambda, Expression[] arguments)
		{
			return Expression.Invoke(lambda, arguments ?? this._arguments);
		}

		// Token: 0x04000967 RID: 2407
		private IReadOnlyList<Expression> _arguments;
	}
}
