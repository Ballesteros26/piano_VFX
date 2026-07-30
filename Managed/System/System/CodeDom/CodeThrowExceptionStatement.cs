using System;

namespace System.CodeDom
{
	/// <summary>Represents a statement that throws an exception.</summary>
	// Token: 0x02000791 RID: 1937
	[Serializable]
	public class CodeThrowExceptionStatement : CodeStatement
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeThrowExceptionStatement" /> class.</summary>
		// Token: 0x06003D52 RID: 15698 RVA: 0x000D84F9 File Offset: 0x000D66F9
		public CodeThrowExceptionStatement()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeThrowExceptionStatement" /> class with the specified exception type instance.</summary>
		/// <param name="toThrow">A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the exception to throw. </param>
		// Token: 0x06003D53 RID: 15699 RVA: 0x000DA29C File Offset: 0x000D849C
		public CodeThrowExceptionStatement(CodeExpression toThrow)
		{
			this.ToThrow = toThrow;
		}

		/// <summary>Gets or sets the exception to throw.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeExpression" /> representing an instance of the exception to throw.</returns>
		// Token: 0x17000EE2 RID: 3810
		// (get) Token: 0x06003D54 RID: 15700 RVA: 0x000DA2AB File Offset: 0x000D84AB
		// (set) Token: 0x06003D55 RID: 15701 RVA: 0x000DA2B3 File Offset: 0x000D84B3
		public CodeExpression ToThrow { get; set; }
	}
}
