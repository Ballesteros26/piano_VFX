using System;

namespace System.CodeDom
{
	/// <summary>Represents a member of a type using a literal code fragment.</summary>
	// Token: 0x0200078D RID: 1933
	[Serializable]
	public class CodeSnippetTypeMember : CodeTypeMember
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeSnippetTypeMember" /> class.</summary>
		// Token: 0x06003D3A RID: 15674 RVA: 0x000D9275 File Offset: 0x000D7475
		public CodeSnippetTypeMember()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeSnippetTypeMember" /> class using the specified text.</summary>
		/// <param name="text">The literal code fragment for the type member. </param>
		// Token: 0x06003D3B RID: 15675 RVA: 0x000DA163 File Offset: 0x000D8363
		public CodeSnippetTypeMember(string text)
		{
			this.Text = text;
		}

		/// <summary>Gets or sets the literal code fragment for the type member.</summary>
		/// <returns>The literal code fragment for the type member.</returns>
		// Token: 0x17000EDD RID: 3805
		// (get) Token: 0x06003D3C RID: 15676 RVA: 0x000DA172 File Offset: 0x000D8372
		// (set) Token: 0x06003D3D RID: 15677 RVA: 0x000DA183 File Offset: 0x000D8383
		public string Text
		{
			get
			{
				return this._text ?? string.Empty;
			}
			set
			{
				this._text = value;
			}
		}

		// Token: 0x04002DE3 RID: 11747
		private string _text;
	}
}
