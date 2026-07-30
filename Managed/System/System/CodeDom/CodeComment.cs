using System;

namespace System.CodeDom
{
	/// <summary>Represents a comment.</summary>
	// Token: 0x02000760 RID: 1888
	[Serializable]
	public class CodeComment : CodeObject
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeComment" /> class.</summary>
		// Token: 0x06003BF8 RID: 15352 RVA: 0x000D8AA9 File Offset: 0x000D6CA9
		public CodeComment()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeComment" /> class with the specified text as contents.</summary>
		/// <param name="text">The contents of the comment. </param>
		// Token: 0x06003BF9 RID: 15353 RVA: 0x000D8AB1 File Offset: 0x000D6CB1
		public CodeComment(string text)
		{
			this.Text = text;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeComment" /> class using the specified text and documentation comment flag.</summary>
		/// <param name="text">The contents of the comment. </param>
		/// <param name="docComment">true if the comment is a documentation comment; otherwise, false. </param>
		// Token: 0x06003BFA RID: 15354 RVA: 0x000D8AC0 File Offset: 0x000D6CC0
		public CodeComment(string text, bool docComment)
		{
			this.Text = text;
			this.DocComment = docComment;
		}

		/// <summary>Gets or sets a value that indicates whether the comment is a documentation comment.</summary>
		/// <returns>true if the comment is a documentation comment; otherwise, false.</returns>
		// Token: 0x17000E7D RID: 3709
		// (get) Token: 0x06003BFB RID: 15355 RVA: 0x000D8AD6 File Offset: 0x000D6CD6
		// (set) Token: 0x06003BFC RID: 15356 RVA: 0x000D8ADE File Offset: 0x000D6CDE
		public bool DocComment { get; set; }

		/// <summary>Gets or sets the text of the comment.</summary>
		/// <returns>A string containing the comment text.</returns>
		// Token: 0x17000E7E RID: 3710
		// (get) Token: 0x06003BFD RID: 15357 RVA: 0x000D8AE7 File Offset: 0x000D6CE7
		// (set) Token: 0x06003BFE RID: 15358 RVA: 0x000D8AF8 File Offset: 0x000D6CF8
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

		// Token: 0x04002D7C RID: 11644
		private string _text;
	}
}
