using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic.Utils;

namespace System.Linq.Expressions
{
	// Token: 0x0200024D RID: 589
	internal class ScopeN : ScopeExpression
	{
		// Token: 0x06001042 RID: 4162 RVA: 0x000359AD File Offset: 0x00033BAD
		internal ScopeN(IReadOnlyList<ParameterExpression> variables, IReadOnlyList<Expression> body)
			: base(variables)
		{
			this._body = body;
		}

		// Token: 0x06001043 RID: 4163 RVA: 0x000359BD File Offset: 0x00033BBD
		internal override bool SameExpressions(ICollection<Expression> expressions)
		{
			return ExpressionUtils.SameElements<Expression>(expressions, this._body);
		}

		// Token: 0x170002BC RID: 700
		// (get) Token: 0x06001044 RID: 4164 RVA: 0x000359CB File Offset: 0x00033BCB
		protected IReadOnlyList<Expression> Body
		{
			get
			{
				return this._body;
			}
		}

		// Token: 0x06001045 RID: 4165 RVA: 0x000359D3 File Offset: 0x00033BD3
		internal override Expression GetExpression(int index)
		{
			return this._body[index];
		}

		// Token: 0x170002BD RID: 701
		// (get) Token: 0x06001046 RID: 4166 RVA: 0x000359E1 File Offset: 0x00033BE1
		internal override int ExpressionCount
		{
			get
			{
				return this._body.Count;
			}
		}

		// Token: 0x06001047 RID: 4167 RVA: 0x000359EE File Offset: 0x00033BEE
		internal override ReadOnlyCollection<Expression> GetOrMakeExpressions()
		{
			return ExpressionUtils.ReturnReadOnly<Expression>(ref this._body);
		}

		// Token: 0x06001048 RID: 4168 RVA: 0x000359FB File Offset: 0x00033BFB
		internal override BlockExpression Rewrite(ReadOnlyCollection<ParameterExpression> variables, Expression[] args)
		{
			if (args == null)
			{
				Expression.ValidateVariables(variables, "variables");
				return new ScopeN(variables, this._body);
			}
			return new ScopeN(base.ReuseOrValidateVariables(variables), args);
		}

		// Token: 0x040008C0 RID: 2240
		private IReadOnlyList<Expression> _body;
	}
}
