using System;

namespace System.CodeDom
{
	/// <summary>Represents a literal code fragment that can be compiled.</summary>
	// Token: 0x0200078A RID: 1930
	[Serializable]
	public class CodeSnippetCompileUnit : CodeCompileUnit
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeSnippetCompileUnit" /> class. </summary>
		// Token: 0x06003D2C RID: 15660 RVA: 0x000DA0CF File Offset: 0x000D82CF
		public CodeSnippetCompileUnit()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeSnippetCompileUnit" /> class.</summary>
		/// <param name="value">The literal code fragment to represent. </param>
		// Token: 0x06003D2D RID: 15661 RVA: 0x000DA0D7 File Offset: 0x000D82D7
		public CodeSnippetCompileUnit(string value)
		{
			this.Value = value;
		}

		/// <summary>Gets or sets the literal code fragment to represent.</summary>
		/// <returns>The literal code fragment.</returns>
		// Token: 0x17000ED9 RID: 3801
		// (get) Token: 0x06003D2E RID: 15662 RVA: 0x000DA0E6 File Offset: 0x000D82E6
		// (set) Token: 0x06003D2F RID: 15663 RVA: 0x000DA0F7 File Offset: 0x000D82F7
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

		/// <summary>Gets or sets the line and file information about where the code is located in a source code document.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeLinePragma" /> that indicates the position of the code fragment.</returns>
		// Token: 0x17000EDA RID: 3802
		// (get) Token: 0x06003D30 RID: 15664 RVA: 0x000DA100 File Offset: 0x000D8300
		// (set) Token: 0x06003D31 RID: 15665 RVA: 0x000DA108 File Offset: 0x000D8308
		public CodeLinePragma LinePragma { get; set; }

		// Token: 0x04002DDF RID: 11743
		private string _value;
	}
}
