using System;

namespace System.CodeDom
{
	/// <summary>Represents a statement consisting of a single comment.</summary>
	// Token: 0x02000761 RID: 1889
	[Serializable]
	public class CodeCommentStatement : CodeStatement
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeCommentStatement" /> class.</summary>
		// Token: 0x06003BFF RID: 15359 RVA: 0x000D84F9 File Offset: 0x000D66F9
		public CodeCommentStatement()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeCommentStatement" /> class using the specified comment.</summary>
		/// <param name="comment">A <see cref="T:System.CodeDom.CodeComment" /> that indicates the comment. </param>
		// Token: 0x06003C00 RID: 15360 RVA: 0x000D8B01 File Offset: 0x000D6D01
		public CodeCommentStatement(CodeComment comment)
		{
			this.Comment = comment;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeCommentStatement" /> class using the specified text as contents.</summary>
		/// <param name="text">The contents of the comment. </param>
		// Token: 0x06003C01 RID: 15361 RVA: 0x000D8B10 File Offset: 0x000D6D10
		public CodeCommentStatement(string text)
		{
			this.Comment = new CodeComment(text);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeCommentStatement" /> class using the specified text and documentation comment flag.</summary>
		/// <param name="text">The contents of the comment. </param>
		/// <param name="docComment">true if the comment is a documentation comment; otherwise, false. </param>
		// Token: 0x06003C02 RID: 15362 RVA: 0x000D8B24 File Offset: 0x000D6D24
		public CodeCommentStatement(string text, bool docComment)
		{
			this.Comment = new CodeComment(text, docComment);
		}

		/// <summary>Gets or sets the contents of the comment.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeComment" /> that indicates the comment.</returns>
		// Token: 0x17000E7F RID: 3711
		// (get) Token: 0x06003C03 RID: 15363 RVA: 0x000D8B39 File Offset: 0x000D6D39
		// (set) Token: 0x06003C04 RID: 15364 RVA: 0x000D8B41 File Offset: 0x000D6D41
		public CodeComment Comment { get; set; }
	}
}
