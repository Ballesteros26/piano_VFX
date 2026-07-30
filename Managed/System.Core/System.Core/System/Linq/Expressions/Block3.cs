using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic.Utils;

namespace System.Linq.Expressions
{
	// Token: 0x02000247 RID: 583
	internal sealed class Block3 : BlockExpression
	{
		// Token: 0x0600101E RID: 4126 RVA: 0x0003545A File Offset: 0x0003365A
		internal Block3(Expression arg0, Expression arg1, Expression arg2)
		{
			this._arg0 = arg0;
			this._arg1 = arg1;
			this._arg2 = arg2;
		}

		// Token: 0x0600101F RID: 4127 RVA: 0x00035478 File Offset: 0x00033678
		internal override bool SameExpressions(ICollection<Expression> expressions)
		{
			if (expressions.Count == 3)
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
							return enumerator.Current == this._arg2;
						}
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x06001020 RID: 4128 RVA: 0x00035510 File Offset: 0x00033710
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
			default:
				throw Error.ArgumentOutOfRange("index");
			}
		}

		// Token: 0x170002B6 RID: 694
		// (get) Token: 0x06001021 RID: 4129 RVA: 0x0003554A File Offset: 0x0003374A
		internal override int ExpressionCount
		{
			get
			{
				return 3;
			}
		}

		// Token: 0x06001022 RID: 4130 RVA: 0x0003554D File Offset: 0x0003374D
		internal override ReadOnlyCollection<Expression> GetOrMakeExpressions()
		{
			return BlockExpression.ReturnReadOnlyExpressions(this, ref this._arg0);
		}

		// Token: 0x06001023 RID: 4131 RVA: 0x0003555B File Offset: 0x0003375B
		internal override BlockExpression Rewrite(ReadOnlyCollection<ParameterExpression> variables, Expression[] args)
		{
			return new Block3(args[0], args[1], args[2]);
		}

		// Token: 0x040008B1 RID: 2225
		private object _arg0;

		// Token: 0x040008B2 RID: 2226
		private readonly Expression _arg1;

		// Token: 0x040008B3 RID: 2227
		private readonly Expression _arg2;
	}
}
