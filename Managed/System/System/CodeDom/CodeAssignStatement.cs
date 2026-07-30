using System;

namespace System.CodeDom
{
	/// <summary>Represents a simple assignment statement.</summary>
	// Token: 0x02000753 RID: 1875
	[Serializable]
	public class CodeAssignStatement : CodeStatement
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeAssignStatement" /> class.</summary>
		// Token: 0x06003B92 RID: 15250 RVA: 0x000D84F9 File Offset: 0x000D66F9
		public CodeAssignStatement()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeAssignStatement" /> class using the specified expressions.</summary>
		/// <param name="left">The variable to assign to. </param>
		/// <param name="right">The value to assign. </param>
		// Token: 0x06003B93 RID: 15251 RVA: 0x000D8501 File Offset: 0x000D6701
		public CodeAssignStatement(CodeExpression left, CodeExpression right)
		{
			this.Left = left;
			this.Right = right;
		}

		/// <summary>Gets or sets the expression representing the object or reference to assign to.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the object or reference to assign to.</returns>
		// Token: 0x17000E66 RID: 3686
		// (get) Token: 0x06003B94 RID: 15252 RVA: 0x000D8517 File Offset: 0x000D6717
		// (set) Token: 0x06003B95 RID: 15253 RVA: 0x000D851F File Offset: 0x000D671F
		public CodeExpression Left { get; set; }

		/// <summary>Gets or sets the expression representing the object or reference to assign.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the object or reference to assign.</returns>
		// Token: 0x17000E67 RID: 3687
		// (get) Token: 0x06003B96 RID: 15254 RVA: 0x000D8528 File Offset: 0x000D6728
		// (set) Token: 0x06003B97 RID: 15255 RVA: 0x000D8530 File Offset: 0x000D6730
		public CodeExpression Right { get; set; }
	}
}
