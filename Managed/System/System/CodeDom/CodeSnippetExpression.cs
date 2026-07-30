using System;

namespace System.CodeDom
{
	/// <summary>Represents a literal expression.</summary>
	// Token: 0x0200078B RID: 1931
	[Serializable]
	public class CodeSnippetExpression : CodeExpression
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeSnippetExpression" /> class.</summary>
		// Token: 0x06003D32 RID: 15666 RVA: 0x000D82AC File Offset: 0x000D64AC
		public CodeSnippetExpression()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeSnippetExpression" /> class using the specified literal expression.</summary>
		/// <param name="value">The literal expression to represent. </param>
		// Token: 0x06003D33 RID: 15667 RVA: 0x000DA111 File Offset: 0x000D8311
		public CodeSnippetExpression(string value)
		{
			this.Value = value;
		}

		/// <summary>Gets or sets the literal string of code.</summary>
		/// <returns>The literal string.</returns>
		// Token: 0x17000EDB RID: 3803
		// (get) Token: 0x06003D34 RID: 15668 RVA: 0x000DA120 File Offset: 0x000D8320
		// (set) Token: 0x06003D35 RID: 15669 RVA: 0x000DA131 File Offset: 0x000D8331
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

		// Token: 0x04002DE1 RID: 11745
		private string _value;
	}
}
