using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic.Utils;

namespace System.Linq.Expressions
{
	// Token: 0x02000248 RID: 584
	internal sealed class Block4 : BlockExpression
	{
		// Token: 0x06001024 RID: 4132 RVA: 0x0003556B File Offset: 0x0003376B
		internal Block4(Expression arg0, Expression arg1, Expression arg2, Expression arg3)
		{
			this._arg0 = arg0;
			this._arg1 = arg1;
			this._arg2 = arg2;
			this._arg3 = arg3;
		}

		// Token: 0x06001025 RID: 4133 RVA: 0x00035590 File Offset: 0x00033790
		internal override bool SameExpressions(ICollection<Expression> expressions)
		{
			if (expressions.Count == 4)
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
								return enumerator.Current == this._arg3;
							}
						}
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x06001026 RID: 4134 RVA: 0x00035640 File Offset: 0x00033840
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
			default:
				throw Error.ArgumentOutOfRange("index");
			}
		}

		// Token: 0x170002B7 RID: 695
		// (get) Token: 0x06001027 RID: 4135 RVA: 0x00035690 File Offset: 0x00033890
		internal override int ExpressionCount
		{
			get
			{
				return 4;
			}
		}

		// Token: 0x06001028 RID: 4136 RVA: 0x00035693 File Offset: 0x00033893
		internal override ReadOnlyCollection<Expression> GetOrMakeExpressions()
		{
			return BlockExpression.ReturnReadOnlyExpressions(this, ref this._arg0);
		}

		// Token: 0x06001029 RID: 4137 RVA: 0x000356A1 File Offset: 0x000338A1
		internal override BlockExpression Rewrite(ReadOnlyCollection<ParameterExpression> variables, Expression[] args)
		{
			return new Block4(args[0], args[1], args[2], args[3]);
		}

		// Token: 0x040008B4 RID: 2228
		private object _arg0;

		// Token: 0x040008B5 RID: 2229
		private readonly Expression _arg1;

		// Token: 0x040008B6 RID: 2230
		private readonly Expression _arg2;

		// Token: 0x040008B7 RID: 2231
		private readonly Expression _arg3;
	}
}
