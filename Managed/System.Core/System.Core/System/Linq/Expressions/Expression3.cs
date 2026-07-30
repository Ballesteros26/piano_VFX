using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic.Utils;

namespace System.Linq.Expressions
{
	// Token: 0x02000287 RID: 647
	internal sealed class Expression3<TDelegate> : Expression<TDelegate>
	{
		// Token: 0x060012FF RID: 4863 RVA: 0x0003B87A File Offset: 0x00039A7A
		public Expression3(Expression body, ParameterExpression par0, ParameterExpression par1, ParameterExpression par2)
			: base(body)
		{
			this._par0 = par0;
			this._par1 = par1;
			this._par2 = par2;
		}

		// Token: 0x17000330 RID: 816
		// (get) Token: 0x06001300 RID: 4864 RVA: 0x0003554A File Offset: 0x0003374A
		internal override int ParameterCount
		{
			get
			{
				return 3;
			}
		}

		// Token: 0x06001301 RID: 4865 RVA: 0x0003B899 File Offset: 0x00039A99
		internal override ParameterExpression GetParameter(int index)
		{
			switch (index)
			{
			case 0:
				return ExpressionUtils.ReturnObject<ParameterExpression>(this._par0);
			case 1:
				return this._par1;
			case 2:
				return this._par2;
			default:
				throw Error.ArgumentOutOfRange("index");
			}
		}

		// Token: 0x06001302 RID: 4866 RVA: 0x0003B8D4 File Offset: 0x00039AD4
		internal override bool SameParameters(ICollection<ParameterExpression> parameters)
		{
			if (parameters != null && parameters.Count == 3)
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
						if (enumerator.Current == this._par1)
						{
							enumerator.MoveNext();
							return enumerator.Current == this._par2;
						}
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x06001303 RID: 4867 RVA: 0x0003B96C File Offset: 0x00039B6C
		internal override ReadOnlyCollection<ParameterExpression> GetOrMakeParameters()
		{
			return ExpressionUtils.ReturnReadOnly(this, ref this._par0);
		}

		// Token: 0x06001304 RID: 4868 RVA: 0x0003B97A File Offset: 0x00039B7A
		internal override Expression<TDelegate> Rewrite(Expression body, ParameterExpression[] parameters)
		{
			if (parameters != null)
			{
				return Expression.Lambda<TDelegate>(body, parameters);
			}
			return Expression.Lambda<TDelegate>(body, new ParameterExpression[]
			{
				ExpressionUtils.ReturnObject<ParameterExpression>(this._par0),
				this._par1,
				this._par2
			});
		}

		// Token: 0x0400097F RID: 2431
		private object _par0;

		// Token: 0x04000980 RID: 2432
		private readonly ParameterExpression _par1;

		// Token: 0x04000981 RID: 2433
		private readonly ParameterExpression _par2;
	}
}
