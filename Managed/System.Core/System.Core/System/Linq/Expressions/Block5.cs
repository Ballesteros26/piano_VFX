using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic.Utils;

namespace System.Linq.Expressions
{
	// Token: 0x02000249 RID: 585
	internal sealed class Block5 : BlockExpression
	{
		// Token: 0x0600102A RID: 4138 RVA: 0x000356B4 File Offset: 0x000338B4
		internal Block5(Expression arg0, Expression arg1, Expression arg2, Expression arg3, Expression arg4)
		{
			this._arg0 = arg0;
			this._arg1 = arg1;
			this._arg2 = arg2;
			this._arg3 = arg3;
			this._arg4 = arg4;
		}

		// Token: 0x0600102B RID: 4139 RVA: 0x000356E4 File Offset: 0x000338E4
		internal override Expression GetExpression(int index)
		{
			switch (index)
			{
			case 0:
				return ExpressionUtils.ReturnObject<Expression>(this._arg0);
			case 1:
				return this._arg1;
			case 2:
				return this._arg2;
			case 3:
				return this._arg3;
			case 4:
				return this._arg4;
			default:
				throw Error.ArgumentOutOfRange("index");
			}
		}

		// Token: 0x0600102C RID: 4140 RVA: 0x00035740 File Offset: 0x00033940
		internal override bool SameExpressions(ICollection<Expression> expressions)
		{
			if (expressions.Count == 5)
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
						if (enumerator.Current == this._arg1)
						{
							enumerator.MoveNext();
							if (enumerator.Current == this._arg2)
							{
								enumerator.MoveNext();
								if (enumerator.Current == this._arg3)
								{
									enumerator.MoveNext();
									return enumerator.Current == this._arg4;
								}
							}
						}
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x170002B8 RID: 696
		// (get) Token: 0x0600102D RID: 4141 RVA: 0x00035804 File Offset: 0x00033A04
		internal override int ExpressionCount
		{
			get
			{
				return 5;
			}
		}

		// Token: 0x0600102E RID: 4142 RVA: 0x00035807 File Offset: 0x00033A07
		internal override ReadOnlyCollection<Expression> GetOrMakeExpressions()
		{
			return BlockExpression.ReturnReadOnlyExpressions(this, ref this._arg0);
		}

		// Token: 0x0600102F RID: 4143 RVA: 0x00035815 File Offset: 0x00033A15
		internal override BlockExpression Rewrite(ReadOnlyCollection<ParameterExpression> variables, Expression[] args)
		{
			return new Block5(args[0], args[1], args[2], args[3], args[4]);
		}

		// Token: 0x040008B8 RID: 2232
		private object _arg0;

		// Token: 0x040008B9 RID: 2233
		private readonly Expression _arg1;

		// Token: 0x040008BA RID: 2234
		private readonly Expression _arg2;

		// Token: 0x040008BB RID: 2235
		private readonly Expression _arg3;

		// Token: 0x040008BC RID: 2236
		private readonly Expression _arg4;
	}
}
