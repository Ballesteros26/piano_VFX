using System;

namespace System.CodeDom
{
	/// <summary>Represents a specific location within a specific file.</summary>
	// Token: 0x02000776 RID: 1910
	[Serializable]
	public class CodeLinePragma
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeLinePragma" /> class. </summary>
		// Token: 0x06003C80 RID: 15488 RVA: 0x000020EB File Offset: 0x000002EB
		public CodeLinePragma()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeLinePragma" /> class.</summary>
		/// <param name="fileName">The file name of the associated file. </param>
		/// <param name="lineNumber">The line number to store a reference to. </param>
		// Token: 0x06003C81 RID: 15489 RVA: 0x000D9234 File Offset: 0x000D7434
		public CodeLinePragma(string fileName, int lineNumber)
		{
			this.FileName = fileName;
			this.LineNumber = lineNumber;
		}

		/// <summary>Gets or sets the name of the associated file.</summary>
		/// <returns>The file name of the associated file.</returns>
		// Token: 0x17000EA3 RID: 3747
		// (get) Token: 0x06003C82 RID: 15490 RVA: 0x000D924A File Offset: 0x000D744A
		// (set) Token: 0x06003C83 RID: 15491 RVA: 0x000D925B File Offset: 0x000D745B
		public string FileName
		{
			get
			{
				return this._fileName ?? string.Empty;
			}
			set
			{
				this._fileName = value;
			}
		}

		/// <summary>Gets or sets the line number of the associated reference.</summary>
		/// <returns>The line number.</returns>
		// Token: 0x17000EA4 RID: 3748
		// (get) Token: 0x06003C84 RID: 15492 RVA: 0x000D9264 File Offset: 0x000D7464
		// (set) Token: 0x06003C85 RID: 15493 RVA: 0x000D926C File Offset: 0x000D746C
		public int LineNumber { get; set; }

		// Token: 0x04002D9F RID: 11679
		private string _fileName;
	}
}
