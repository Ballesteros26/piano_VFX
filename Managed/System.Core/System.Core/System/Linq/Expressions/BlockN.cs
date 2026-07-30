using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic.Utils;

namespace System.Linq.Expressions
{
	// Token: 0x0200024A RID: 586
	internal class BlockN : BlockExpression
	{
		// Token: 0x06001030 RID: 4144 RVA: 0x0003582B File Offset: 0x00033A2B
		internal BlockN(IReadOnlyList<Expression> expressions)
		{
			this._expressions = expressions;
		}

		// Token: 0x06001031 RID: 4145 RVA: 0x0003583A File Offset: 0x00033A3A
		internal override bool SameExpressions(ICollection<Expression> expressions)
		{
			return ExpressionUtils.SameElements<Expression>(expressions, this._expressions);
		}

		// Token: 0x06001032 RID: 4146 RVA: 0x00035848 File Offset: 0x00033A48
		internal override Expression GetExpression(int index)
		{
			return this._expressions[index];
		}

		// Token: 0x170002B9 RID: 697
		// (get) Token: 0x06001033 RID: 4147 RVA: 0x00035856 File Offset: 0x00033A56
		internal override int ExpressionCount
		{
			get
			{
				return this._expressions.Count;
			}
		}

		// Token: 0x06001034 RID: 4148 RVA: 0x00035863 File Offset: 0x00033A63
		internal override ReadOnlyCollection<Expression> GetOrMakeExpressions()
		{
			return ExpressionUtils.ReturnReadOnly<Expression>(ref this._expressions);
		}

		// Token: 0x06001035 RID: 4149 RVA: 0x00035870 File Offset: 0x00033A70
		internal override BlockExpression Rewrite(ReadOnlyCollection<ParameterExpression> variables, Expression[] args)
		{
			return new BlockN(args);
		}

		// Token: 0x040008BD RID: 2237
		private IReadOnlyList<Expression> _expressions;
	}
}
