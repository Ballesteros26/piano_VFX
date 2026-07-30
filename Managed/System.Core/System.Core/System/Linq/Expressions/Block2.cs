using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic.Utils;

namespace System.Linq.Expressions
{
	// Token: 0x02000246 RID: 582
	internal sealed class Block2 : BlockExpression
	{
		// Token: 0x06001018 RID: 4120 RVA: 0x0003537B File Offset: 0x0003357B
		internal Block2(Expression arg0, Expression arg1)
		{
			this._arg0 = arg0;
			this._arg1 = arg1;
		}

		// Token: 0x06001019 RID: 4121 RVA: 0x00035391 File Offset: 0x00033591
		internal override Expression GetExpression(int index)
		{
			if (index == 0)
			{
				return ExpressionUtils.ReturnObject<Expression>(this._arg0);
			}
			if (index != 1)
			{
				throw Error.ArgumentOutOfRange("index");
			}
			return this._arg1;
		}

		// Token: 0x0600101A RID: 4122 RVA: 0x000353BC File Offset: 0x000335BC
		internal override bool SameExpressions(ICollection<Expression> expressions)
		{
			if (expressions.Count == 2)
			{
				ReadOnlyCollection<Expression> readOnlyCollection = this._arg0 as ReadOnlyCollection<Expression>;
				if (readOnlyCollection != null)
				{
					return ExpressionUtils.SameElements<Expression>(expressions, readOnlyCollection);
				}
				using (IEnumerator<Expression> enumerator = expressions.GetEnumerator())
				{
					enumerator.MoveNext();
					if (enumerator.Current == this._arg0)
					{
						enumerator.MoveNext();
						return enumerator.Current == this._arg1;
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x170002B5 RID: 693
		// (get) Token: 0x0600101B RID: 4123 RVA: 0x0003543C File Offset: 0x0003363C
		internal override int ExpressionCount
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x0600101C RID: 4124 RVA: 0x0003543F File Offset: 0x0003363F
		internal override ReadOnlyCollection<Expression> GetOrMakeExpressions()
		{
			return BlockExpression.ReturnReadOnlyExpressions(this, ref this._arg0);
		}

		// Token: 0x0600101D RID: 4125 RVA: 0x0003544D File Offset: 0x0003364D
		internal override BlockExpression Rewrite(ReadOnlyCollection<ParameterExpression> variables, Expression[] args)
		{
			return new Block2(args[0], args[1]);
		}

		// Token: 0x040008AF RID: 2223
		private object _arg0;

		// Token: 0x040008B0 RID: 2224
		private readonly Expression _arg1;
	}
}
