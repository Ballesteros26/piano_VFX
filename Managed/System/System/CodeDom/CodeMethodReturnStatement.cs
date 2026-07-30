using System;

namespace System.CodeDom
{
	/// <summary>Represents a return value statement.</summary>
	// Token: 0x0200077C RID: 1916
	[Serializable]
	public class CodeMethodReturnStatement : CodeStatement
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeMethodReturnStatement" /> class.</summary>
		// Token: 0x06003CB7 RID: 15543 RVA: 0x000D84F9 File Offset: 0x000D66F9
		public CodeMethodReturnStatement()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeMethodReturnStatement" /> class using the specified expression.</summary>
		/// <param name="expression">A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the return value. </param>
		// Token: 0x06003CB8 RID: 15544 RVA: 0x000D97F6 File Offset: 0x000D79F6
		public CodeMethodReturnStatement(CodeExpression expression)
		{
			this.Expression = expression;
		}

		/// <summary>Gets or sets the return value.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the value to return for the return statement, or null if the statement is part of a subroutine.</returns>
		// Token: 0x17000EBB RID: 3771
		// (get) Token: 0x06003CB9 RID: 15545 RVA: 0x000D9805 File Offset: 0x000D7A05
		// (set) Token: 0x06003CBA RID: 15546 RVA: 0x000D980D File Offset: 0x000D7A0D
		public CodeExpression Expression { get; set; }
	}
}
