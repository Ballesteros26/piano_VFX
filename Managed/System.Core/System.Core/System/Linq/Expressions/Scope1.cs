using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic.Utils;

namespace System.Linq.Expressions
{
	// Token: 0x0200024C RID: 588
	internal sealed class Scope1 : ScopeExpression
	{
		// Token: 0x0600103B RID: 4155 RVA: 0x000358CB File Offset: 0x00033ACB
		internal Scope1(IReadOnlyList<ParameterExpression> variables, Expression body)
			: this(variables, body)
		{
		}

		// Token: 0x0600103C RID: 4156 RVA: 0x000358D5 File Offset: 0x00033AD5
		private Scope1(IReadOnlyList<ParameterExpression> variables, object body)
			: base(variables)
		{
			this._body = body;
		}

		// Token: 0x0600103D RID: 4157 RVA: 0x000358E8 File Offset: 0x00033AE8
		internal override bool SameExpressions(ICollection<Expression> expressions)
		{
			if (expressions.Count == 1)
			{
				ReadOnlyCollection<Expression> readOnlyCollection = this._body as ReadOnlyCollection<Expression>;
				if (readOnlyCollection != null)
				{
					return ExpressionUtils.SameElements<Expression>(expressions, readOnlyCollection);
				}
				using (IEnumerator<Expression> enumerator = expressions.GetEnumerator())
				{
					enumerator.MoveNext();
					return ExpressionUtils.ReturnObject<Expression>(this._body) == enumerator.Current;
				}
				return false;
			}
			return false;
		}

		// Token: 0x0600103E RID: 4158 RVA: 0x00035958 File Offset: 0x00033B58
		internal override Expression GetExpression(int index)
		{
			if (index == 0)
			{
				return ExpressionUtils.ReturnObject<Expression>(this._body);
			}
			throw Error.ArgumentOutOfRange("index");
		}

		// Token: 0x170002BB RID: 699
		// (get) Token: 0x0600103F RID: 4159 RVA: 0x0000AA13 File Offset: 0x00008C13
		internal override int ExpressionCount
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x06001040 RID: 4160 RVA: 0x00035973 File Offset: 0x00033B73
		internal override ReadOnlyCollection<Expression> GetOrMakeExpressions()
		{
			return BlockExpression.ReturnReadOnlyExpressions(this, ref this._body);
		}

		// Token: 0x06001041 RID: 4161 RVA: 0x00035981 File Offset: 0x00033B81
		internal override BlockExpression Rewrite(ReadOnlyCollection<ParameterExpression> variables, Expression[] args)
		{
			if (args == null)
			{
				Expression.ValidateVariables(variables, "variables");
				return new Scope1(variables, this._body);
			}
			return new Scope1(base.ReuseOrValidateVariables(variables), args[0]);
		}

		// Token: 0x040008BF RID: 2239
		private object _body;
	}
}
