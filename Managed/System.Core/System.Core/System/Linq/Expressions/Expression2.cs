using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic.Utils;

namespace System.Linq.Expressions
{
	// Token: 0x02000286 RID: 646
	internal sealed class Expression2<TDelegate> : Expression<TDelegate>
	{
		// Token: 0x060012F9 RID: 4857 RVA: 0x0003B779 File Offset: 0x00039979
		public Expression2(Expression body, ParameterExpression par0, ParameterExpression par1)
			: base(body)
		{
			this._par0 = par0;
			this._par1 = par1;
		}

		// Token: 0x1700032F RID: 815
		// (get) Token: 0x060012FA RID: 4858 RVA: 0x0003543C File Offset: 0x0003363C
		internal override int ParameterCount
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x060012FB RID: 4859 RVA: 0x0003B790 File Offset: 0x00039990
		internal override ParameterExpression GetParameter(int index)
		{
			if (index == 0)
			{
				return ExpressionUtils.ReturnObject<ParameterExpression>(this._par0);
			}
			if (index != 1)
			{
				throw Error.ArgumentOutOfRange("index");
			}
			return this._par1;
		}

		// Token: 0x060012FC RID: 4860 RVA: 0x0003B7B8 File Offset: 0x000399B8
		internal override bool SameParameters(ICollection<ParameterExpression> parameters)
		{
			if (parameters != null && parameters.Count == 2)
			{
				ReadOnlyCollection<ParameterExpression> readOnlyCollection = this._par0 as ReadOnlyCollection<ParameterExpression>;
				if (readOnlyCollection != null)
				{
					return ExpressionUtils.SameElements<ParameterExpression>(parameters, readOnlyCollection);
				}
				using (IEnumerator<ParameterExpression> enumerator = parameters.GetEnumerator())
				{
					enumerator.MoveNext();
					if (enumerator.Current == this._par0)
					{
						enumerator.MoveNext();
						return enumerator.Current == this._par1;
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x060012FD RID: 4861 RVA: 0x0003B83C File Offset: 0x00039A3C
		internal override ReadOnlyCollection<ParameterExpression> GetOrMakeParameters()
		{
			return ExpressionUtils.ReturnReadOnly(this, ref this._par0);
		}

		// Token: 0x060012FE RID: 4862 RVA: 0x0003B84A File Offset: 0x00039A4A
		internal override Expression<TDelegate> Rewrite(Expression body, ParameterExpression[] parameters)
		{
			if (parameters != null)
			{
				return Expression.Lambda<TDelegate>(body, parameters);
			}
			return Expression.Lambda<TDelegate>(body, new ParameterExpression[]
			{
				ExpressionUtils.ReturnObject<ParameterExpression>(this._par0),
				this._par1
			});
		}

		// Token: 0x0400097D RID: 2429
		private object _par0;

		// Token: 0x0400097E RID: 2430
		private readonly ParameterExpression _par1;
	}
}
