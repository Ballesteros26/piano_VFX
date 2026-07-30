using System;
using System.Collections.ObjectModel;
using System.Dynamic.Utils;

namespace System.Linq.Expressions
{
	// Token: 0x0200027A RID: 634
	internal sealed class InvocationExpression0 : InvocationExpression
	{
		// Token: 0x0600129D RID: 4765 RVA: 0x0003B0DC File Offset: 0x000392DC
		public InvocationExpression0(Expression lambda, Type returnType)
			: base(lambda, returnType)
		{
		}

		// Token: 0x0600129E RID: 4766 RVA: 0x0003B0E6 File Offset: 0x000392E6
		internal override ReadOnlyCollection<Expression> GetOrMakeArguments()
		{
			return EmptyReadOnlyCollection<Expression>.Instance;
		}

		// Token: 0x0600129F RID: 4767 RVA: 0x0003B0ED File Offset: 0x000392ED
		public override Expression GetArgument(int index)
		{
			throw new ArgumentOutOfRangeException("index");
		}

		// Token: 0x17000312 RID: 786
		// (get) Token: 0x060012A0 RID: 4768 RVA: 0x00002285 File Offset: 0x00000485
		public override int ArgumentCount
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x060012A1 RID: 4769 RVA: 0x0003B0F9 File Offset: 0x000392F9
		internal override InvocationExpression Rewrite(Expression lambda, Expression[] arguments)
		{
			return Expression.Invoke(lambda);
		}
	}
}
