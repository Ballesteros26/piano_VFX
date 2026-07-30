using System;

namespace System.CodeDom
{
	/// <summary>Defines identifiers for supported binary operators.</summary>
	// Token: 0x0200075B RID: 1883
	public enum CodeBinaryOperatorType
	{
		/// <summary>Addition operator.</summary>
		// Token: 0x04002D63 RID: 11619
		Add,
		/// <summary>Subtraction operator.</summary>
		// Token: 0x04002D64 RID: 11620
		Subtract,
		/// <summary>Multiplication operator.</summary>
		// Token: 0x04002D65 RID: 11621
		Multiply,
		/// <summary>Division operator.</summary>
		// Token: 0x04002D66 RID: 11622
		Divide,
		/// <summary>Modulus operator.</summary>
		// Token: 0x04002D67 RID: 11623
		Modulus,
		/// <summary>Assignment operator.</summary>
		// Token: 0x04002D68 RID: 11624
		Assign,
		/// <summary>Identity not equal operator.</summary>
		// Token: 0x04002D69 RID: 11625
		IdentityInequality,
		/// <summary>Identity equal operator.</summary>
		// Token: 0x04002D6A RID: 11626
		IdentityEquality,
		/// <summary>Value equal operator.</summary>
		// Token: 0x04002D6B RID: 11627
		ValueEquality,
		/// <summary>Bitwise or operator.</summary>
		// Token: 0x04002D6C RID: 11628
		BitwiseOr,
		/// <summary>Bitwise and operator.</summary>
		// Token: 0x04002D6D RID: 11629
		BitwiseAnd,
		/// <summary>Boolean or operator. This represents a short circuiting operator. A short circuiting operator will evaluate only as many expressions as necessary before returning a correct value.</summary>
		// Token: 0x04002D6E RID: 11630
		BooleanOr,
		/// <summary>Boolean and operator. This represents a short circuiting operator. A short circuiting operator will evaluate only as many expressions as necessary before returning a correct value.</summary>
		// Token: 0x04002D6F RID: 11631
		BooleanAnd,
		/// <summary>Less than operator.</summary>
		// Token: 0x04002D70 RID: 11632
		LessThan,
		/// <summary>Less than or equal operator.</summary>
		// Token: 0x04002D71 RID: 11633
		LessThanOrEqual,
		/// <summary>Greater than operator.</summary>
		// Token: 0x04002D72 RID: 11634
		GreaterThan,
		/// <summary>Greater than or equal operator.</summary>
		// Token: 0x04002D73 RID: 11635
		GreaterThanOrEqual
	}
}
