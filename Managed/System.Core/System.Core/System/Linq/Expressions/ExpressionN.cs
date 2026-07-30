using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic.Utils;

namespace System.Linq.Expressions
{
	// Token: 0x02000288 RID: 648
	internal class ExpressionN<TDelegate> : Expression<TDelegate>
	{
		// Token: 0x06001305 RID: 4869 RVA: 0x0003B9B3 File Offset: 0x00039BB3
		public ExpressionN(Expression body, IReadOnlyList<ParameterExpression> parameters)
			: base(body)
		{
			this._parameters = parameters;
		}

		// Token: 0x17000331 RID: 817
		// (get) Token: 0x06001306 RID: 4870 RVA: 0x0003B9C3 File Offset: 0x00039BC3
		internal override int ParameterCount
		{
			get
			{
				return this._parameters.Count;
			}
		}

		// Token: 0x06001307 RID: 4871 RVA: 0x0003B9D0 File Offset: 0x00039BD0
		internal override ParameterExpression GetParameter(int index)
		{
			return this._parameters[index];
		}

		// Token: 0x06001308 RID: 4872 RVA: 0x0003B9DE File Offset: 0x00039BDE
		internal override bool SameParameters(ICollection<ParameterExpression> parameters)
		{
			return ExpressionUtils.SameElements<ParameterExpression>(parameters, this._parameters);
		}

		// Token: 0x06001309 RID: 4873 RVA: 0x0003B9EC File Offset: 0x00039BEC
		internal override ReadOnlyCollection<ParameterExpression> GetOrMakeParameters()
		{
			return ExpressionUtils.ReturnReadOnly<ParameterExpression>(ref this._parameters);
		}

		// Token: 0x0600130A RID: 4874 RVA: 0x0003B9FC File Offset: 0x00039BFC
		internal override Expression<TDelegate> Rewrite(Expression body, ParameterExpression[] parameters)
		{
			return Expression.Lambda<TDelegate>(body, base.Name, base.TailCall, parameters ?? this._parameters);
		}

		// Token: 0x04000982 RID: 2434
		private IReadOnlyList<ParameterExpression> _parameters;
	}
}
