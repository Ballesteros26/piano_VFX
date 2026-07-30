using System;

namespace System.Linq.Expressions
{
	/// <summary>Specifies what kind of jump this <see cref="T:System.Linq.Expressions.GotoExpression" /> represents.</summary>
	// Token: 0x02000272 RID: 626
	public enum GotoExpressionKind
	{
		/// <summary>A <see cref="T:System.Linq.Expressions.GotoExpression" /> that represents a jump to some location.</summary>
		// Token: 0x0400095A RID: 2394
		Goto,
		/// <summary>A <see cref="T:System.Linq.Expressions.GotoExpression" /> that represents a return statement.</summary>
		// Token: 0x0400095B RID: 2395
		Return,
		/// <summary>A <see cref="T:System.Linq.Expressions.GotoExpression" /> that represents a break statement.</summary>
		// Token: 0x0400095C RID: 2396
		Break,
		/// <summary>A <see cref="T:System.Linq.Expressions.GotoExpression" /> that represents a continue statement.</summary>
		// Token: 0x0400095D RID: 2397
		Continue
	}
}
