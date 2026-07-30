using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic.Utils;

namespace System.Linq.Expressions
{
	// Token: 0x0200024B RID: 587
	internal class ScopeExpression : BlockExpression
	{
		// Token: 0x06001036 RID: 4150 RVA: 0x00035878 File Offset: 0x00033A78
		internal ScopeExpression(IReadOnlyList<ParameterExpression> variables)
		{
			this._variables = variables;
		}

		// Token: 0x06001037 RID: 4151 RVA: 0x00035887 File Offset: 0x00033A87
		internal override bool SameVariables(ICollection<ParameterExpression> variables)
		{
			return ExpressionUtils.SameElements<ParameterExpression>(variables, this._variables);
		}

		// Token: 0x06001038 RID: 4152 RVA: 0x00035895 File Offset: 0x00033A95
		internal override ReadOnlyCollection<ParameterExpression> GetOrMakeVariables()
		{
			return ExpressionUtils.ReturnReadOnly<ParameterExpression>(ref this._variables);
		}

		// Token: 0x170002BA RID: 698
		// (get) Token: 0x06001039 RID: 4153 RVA: 0x000358A2 File Offset: 0x00033AA2
		protected IReadOnlyList<ParameterExpression> VariablesList
		{
			get
			{
				return this._variables;
			}
		}

		// Token: 0x0600103A RID: 4154 RVA: 0x000358AA File Offset: 0x00033AAA
		internal IReadOnlyList<ParameterExpression> ReuseOrValidateVariables(ReadOnlyCollection<ParameterExpression> variables)
		{
			if (variables != null && variables != this.VariablesList)
			{
				Expression.ValidateVariables(variables, "variables");
				return variables;
			}
			return this.VariablesList;
		}

		// Token: 0x040008BE RID: 2238
		private IReadOnlyList<ParameterExpression> _variables;
	}
}
