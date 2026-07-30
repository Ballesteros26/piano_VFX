using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic.Utils;

namespace System.Linq.Expressions
{
	// Token: 0x02000284 RID: 644
	internal sealed class Expression0<TDelegate> : Expression<TDelegate>
	{
		// Token: 0x060012ED RID: 4845 RVA: 0x0003B69D File Offset: 0x0003989D
		public Expression0(Expression body)
			: base(body)
		{
		}

		// Token: 0x1700032D RID: 813
		// (get) Token: 0x060012EE RID: 4846 RVA: 0x00002285 File Offset: 0x00000485
		internal override int ParameterCount
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x060012EF RID: 4847 RVA: 0x00035328 File Offset: 0x00033528
		internal override bool SameParameters(ICollection<ParameterExpression> parameters)
		{
			return parameters == null || parameters.Count == 0;
		}

		// Token: 0x060012F0 RID: 4848 RVA: 0x0003B6A6 File Offset: 0x000398A6
		internal override ParameterExpression GetParameter(int index)
		{
			throw Error.ArgumentOutOfRange("index");
		}

		// Token: 0x060012F1 RID: 4849 RVA: 0x0003533F File Offset: 0x0003353F
		internal override ReadOnlyCollection<ParameterExpression> GetOrMakeParameters()
		{
			return EmptyReadOnlyCollection<ParameterExpression>.Instance;
		}

		// Token: 0x060012F2 RID: 4850 RVA: 0x0003B6B2 File Offset: 0x000398B2
		internal override Expression<TDelegate> Rewrite(Expression body, ParameterExpression[] parameters)
		{
			return Expression.Lambda<TDelegate>(body, parameters);
		}
	}
}
