using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic.Utils;
using System.Reflection;

namespace System.Linq.Expressions
{
	// Token: 0x02000297 RID: 663
	internal sealed class MethodCallExpressionN : MethodCallExpression, IArgumentProvider
	{
		// Token: 0x06001364 RID: 4964 RVA: 0x0003BF9A File Offset: 0x0003A19A
		public MethodCallExpressionN(MethodInfo method, IReadOnlyList<Expression> args)
			: base(method)
		{
			this._arguments = args;
		}

		// Token: 0x06001365 RID: 4965 RVA: 0x0003BFAA File Offset: 0x0003A1AA
		public override Expression GetArgument(int index)
		{
			return this._arguments[index];
		}

		// Token: 0x17000353 RID: 851
		// (get) Token: 0x06001366 RID: 4966 RVA: 0x0003BFB8 File Offset: 0x0003A1B8
		public override int ArgumentCount
		{
			get
			{
				return this._arguments.Count;
			}
		}

		// Token: 0x06001367 RID: 4967 RVA: 0x0003BFC5 File Offset: 0x0003A1C5
		internal override ReadOnlyCollection<Expression> GetOrMakeArguments()
		{
			return ExpressionUtils.ReturnReadOnly<Expression>(ref this._arguments);
		}

		// Token: 0x06001368 RID: 4968 RVA: 0x0003BFD2 File Offset: 0x0003A1D2
		internal override bool SameArguments(ICollection<Expression> arguments)
		{
			return ExpressionUtils.SameElements<Expression>(arguments, this._arguments);
		}

		// Token: 0x06001369 RID: 4969 RVA: 0x0003BFE0 File Offset: 0x0003A1E0
		internal override MethodCallExpression Rewrite(Expression instance, IReadOnlyList<Expression> args)
		{
			return Expression.Call(base.Method, args ?? this._arguments);
		}

		// Token: 0x0400099A RID: 2458
		private IReadOnlyList<Expression> _arguments;
	}
}
