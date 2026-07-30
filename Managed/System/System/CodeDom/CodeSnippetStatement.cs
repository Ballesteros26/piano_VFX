using System;

namespace System.CodeDom
{
	/// <summary>Represents a statement using a literal code fragment.</summary>
	// Token: 0x0200078C RID: 1932
	[Serializable]
	public class CodeSnippetStatement : CodeStatement
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeSnippetStatement" /> class.</summary>
		// Token: 0x06003D36 RID: 15670 RVA: 0x000D84F9 File Offset: 0x000D66F9
		public CodeSnippetStatement()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeSnippetStatement" /> class using the specified code fragment.</summary>
		/// <param name="value">The literal code fragment of the statement to represent. </param>
		// Token: 0x06003D37 RID: 15671 RVA: 0x000DA13A File Offset: 0x000D833A
		public CodeSnippetStatement(string value)
		{
			this.Value = value;
		}

		/// <summary>Gets or sets the literal code fragment statement.</summary>
		/// <returns>The literal code fragment statement.</returns>
		// Token: 0x17000EDC RID: 3804
		// (get) Token: 0x06003D38 RID: 15672 RVA: 0x000DA149 File Offset: 0x000D8349
		// (set) Token: 0x06003D39 RID: 15673 RVA: 0x000DA15A File Offset: 0x000D835A
		public string Value
		{
			get
			{
				return this._value ?? string.Empty;
			}
			set
			{
				this._value = value;
			}
		}

		// Token: 0x04002DE2 RID: 11746
		private string _value;
	}
}
