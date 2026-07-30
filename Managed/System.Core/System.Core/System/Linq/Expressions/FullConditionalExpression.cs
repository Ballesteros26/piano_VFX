using System;

namespace System.Linq.Expressions
{
	// Token: 0x02000256 RID: 598
	internal class FullConditionalExpression : ConditionalExpression
	{
		// Token: 0x06001078 RID: 4216 RVA: 0x00035E7F File Offset: 0x0003407F
		internal FullConditionalExpression(Expression test, Expression ifTrue, Expression ifFalse)
			: base(test, ifTrue)
		{
			this._false = ifFalse;
		}

		// Token: 0x06001079 RID: 4217 RVA: 0x00035E90 File Offset: 0x00034090
		internal override Expression GetFalse()
		{
			return this._false;
		}

		// Token: 0x040008D3 RID: 2259
		private readonly Expression _false;
	}
}
