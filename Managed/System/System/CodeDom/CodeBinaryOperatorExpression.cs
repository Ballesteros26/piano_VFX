using System;

namespace System.CodeDom
{
	/// <summary>Represents an expression that consists of a binary operation between two expressions.</summary>
	// Token: 0x0200075A RID: 1882
	[Serializable]
	public class CodeBinaryOperatorExpression : CodeExpression
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeBinaryOperatorExpression" /> class.</summary>
		// Token: 0x06003BCA RID: 15306 RVA: 0x000D82AC File Offset: 0x000D64AC
		public CodeBinaryOperatorExpression()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeBinaryOperatorExpression" /> class using the specified parameters.</summary>
		/// <param name="left">The <see cref="T:System.CodeDom.CodeExpression" /> on the left of the operator. </param>
		/// <param name="op">A <see cref="T:System.CodeDom.CodeBinaryOperatorType" /> indicating the type of operator. </param>
		/// <param name="right">The <see cref="T:System.CodeDom.CodeExpression" /> on the right of the operator. </param>
		// Token: 0x06003BCB RID: 15307 RVA: 0x000D8804 File Offset: 0x000D6A04
		public CodeBinaryOperatorExpression(CodeExpression left, CodeBinaryOperatorType op, CodeExpression right)
		{
			this.Right = right;
			this.Operator = op;
			this.Left = left;
		}

		/// <summary>Gets or sets the code expression on the right of the operator.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the right operand.</returns>
		// Token: 0x17000E71 RID: 3697
		// (get) Token: 0x06003BCC RID: 15308 RVA: 0x000D8821 File Offset: 0x000D6A21
		// (set) Token: 0x06003BCD RID: 15309 RVA: 0x000D8829 File Offset: 0x000D6A29
		public CodeExpression Right { get; set; }

		/// <summary>Gets or sets the code expression on the left of the operator.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the left operand.</returns>
		// Token: 0x17000E72 RID: 3698
		// (get) Token: 0x06003BCE RID: 15310 RVA: 0x000D8832 File Offset: 0x000D6A32
		// (set) Token: 0x06003BCF RID: 15311 RVA: 0x000D883A File Offset: 0x000D6A3A
		public CodeExpression Left { get; set; }

		/// <summary>Gets or sets the operator in the binary operator expression.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeBinaryOperatorType" /> that indicates the type of operator in the expression.</returns>
		// Token: 0x17000E73 RID: 3699
		// (get) Token: 0x06003BD0 RID: 15312 RVA: 0x000D8843 File Offset: 0x000D6A43
		// (set) Token: 0x06003BD1 RID: 15313 RVA: 0x000D884B File Offset: 0x000D6A4B
		public CodeBinaryOperatorType Operator { get; set; }
	}
}
