using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Dynamic.Utils;

namespace System.Linq.Expressions.Compiler
{
	// Token: 0x020002E7 RID: 743
	internal sealed class SpilledExpressionBlock : BlockN
	{
		// Token: 0x060016B9 RID: 5817 RVA: 0x0004A862 File Offset: 0x00048A62
		internal SpilledExpressionBlock(IReadOnlyList<Expression> expressions)
			: base(expressions)
		{
		}

		// Token: 0x060016BA RID: 5818 RVA: 0x00035338 File Offset: 0x00033538
		[ExcludeFromCodeCoverage]
		internal override BlockExpression Rewrite(ReadOnlyCollection<ParameterExpression> variables, Expression[] args)
		{
			throw ContractUtils.Unreachable;
		}
	}
}
