using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic.Utils;

namespace System.Linq.Expressions
{
	// Token: 0x02000285 RID: 645
	internal sealed class Expression1<TDelegate> : Expression<TDelegate>
	{
		// Token: 0x060012F3 RID: 4851 RVA: 0x0003B6BB File Offset: 0x000398BB
		public Expression1(Expression body, ParameterExpression par0)
			: base(body)
		{
			this._par0 = par0;
		}

		// Token: 0x1700032E RID: 814
		// (get) Token: 0x060012F4 RID: 4852 RVA: 0x0000AA13 File Offset: 0x00008C13
		internal override int ParameterCount
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x060012F5 RID: 4853 RVA: 0x0003B6CB File Offset: 0x000398CB
		internal override ParameterExpression GetParameter(int index)
		{
			if (index == 0)
			{
				return ExpressionUtils.ReturnObject<ParameterExpression>(this._par0);
			}
			throw Error.ArgumentOutOfRange("index");
		}

		// Token: 0x060012F6 RID: 4854 RVA: 0x0003B6E8 File Offset: 0x000398E8
		internal override bool SameParameters(ICollection<ParameterExpression> parameters)
		{
			if (parameters != null && parameters.Count == 1)
			{
				using (IEnumerator<ParameterExpression> enumerator = parameters.GetEnumerator())
				{
					enumerator.MoveNext();
					return enumerator.Current == ExpressionUtils.ReturnObject<ParameterExpression>(this._par0);
				}
				return false;
			}
			return false;
		}

		// Token: 0x060012F7 RID: 4855 RVA: 0x0003B744 File Offset: 0x00039944
		internal override ReadOnlyCollection<ParameterExpression> GetOrMakeParameters()
		{
			return ExpressionUtils.ReturnReadOnly(this, ref this._par0);
		}

		// Token: 0x060012F8 RID: 4856 RVA: 0x0003B752 File Offset: 0x00039952
		internal override Expression<TDelegate> Rewrite(Expression body, ParameterExpression[] parameters)
		{
			if (parameters != null)
			{
				return Expression.Lambda<TDelegate>(body, parameters);
			}
			return Expression.Lambda<TDelegate>(body, new ParameterExpression[] { ExpressionUtils.ReturnObject<ParameterExpression>(this._par0) });
		}

		// Token: 0x0400097C RID: 2428
		private object _par0;
	}
}
