using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace System.Linq.Expressions
{
	// Token: 0x0200024E RID: 590
	internal sealed class ScopeWithType : ScopeN
	{
		// Token: 0x06001049 RID: 4169 RVA: 0x00035A25 File Offset: 0x00033C25
		internal ScopeWithType(IReadOnlyList<ParameterExpression> variables, IReadOnlyList<Expression> expressions, Type type)
			: base(variables, expressions)
		{
			this.Type = type;
		}

		// Token: 0x170002BE RID: 702
		// (get) Token: 0x0600104A RID: 4170 RVA: 0x00035A36 File Offset: 0x00033C36
		public sealed override Type Type { get; }

		// Token: 0x0600104B RID: 4171 RVA: 0x00035A3E File Offset: 0x00033C3E
		internal override BlockExpression Rewrite(ReadOnlyCollection<ParameterExpression> variables, Expression[] args)
		{
			if (args == null)
			{
				Expression.ValidateVariables(variables, "variables");
				return new ScopeWithType(variables, base.Body, this.Type);
			}
			return new ScopeWithType(base.ReuseOrValidateVariables(variables), args, this.Type);
		}
	}
}
