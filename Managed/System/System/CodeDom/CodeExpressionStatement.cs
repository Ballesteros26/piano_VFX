using System;

namespace System.CodeDom
{
	/// <summary>Represents a statement that consists of a single expression.</summary>
	// Token: 0x02000770 RID: 1904
	[Serializable]
	public class CodeExpressionStatement : CodeStatement
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeExpressionStatement" /> class.</summary>
		// Token: 0x06003C5D RID: 15453 RVA: 0x000D84F9 File Offset: 0x000D66F9
		public CodeExpressionStatement()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeExpressionStatement" /> class by using the specified expression.</summary>
		/// <param name="expression">A <see cref="T:System.CodeDom.CodeExpression" /> for the statement. </param>
		// Token: 0x06003C5E RID: 15454 RVA: 0x000D907C File Offset: 0x000D727C
		public CodeExpressionStatement(CodeExpression expression)
		{
			this.Expression = expression;
		}

		/// <summary>Gets or sets the expression for the statement.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the expression for the statement.</returns>
		// Token: 0x17000E97 RID: 3735
		// (get) Token: 0x06003C5F RID: 15455 RVA: 0x000D908B File Offset: 0x000D728B
		// (set) Token: 0x06003C60 RID: 15456 RVA: 0x000D9093 File Offset: 0x000D7293
		public CodeExpression Expression { get; set; }
	}
}
