using System;

namespace System.CodeDom
{
	/// <summary>Represents a namespace import directive that indicates a namespace to use.</summary>
	// Token: 0x0200077F RID: 1919
	[Serializable]
	public class CodeNamespaceImport : CodeObject
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeNamespaceImport" /> class.</summary>
		// Token: 0x06003CD5 RID: 15573 RVA: 0x000D8AA9 File Offset: 0x000D6CA9
		public CodeNamespaceImport()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeNamespaceImport" /> class using the specified namespace to import.</summary>
		/// <param name="nameSpace">The name of the namespace to import. </param>
		// Token: 0x06003CD6 RID: 15574 RVA: 0x000D9B20 File Offset: 0x000D7D20
		public CodeNamespaceImport(string nameSpace)
		{
			this.Namespace = nameSpace;
		}

		/// <summary>Gets or sets the line and file the statement occurs on.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeLinePragma" /> that indicates the context of the statement.</returns>
		// Token: 0x17000EC1 RID: 3777
		// (get) Token: 0x06003CD7 RID: 15575 RVA: 0x000D9B2F File Offset: 0x000D7D2F
		// (set) Token: 0x06003CD8 RID: 15576 RVA: 0x000D9B37 File Offset: 0x000D7D37
		public CodeLinePragma LinePragma { get; set; }

		/// <summary>Gets or sets the namespace to import.</summary>
		/// <returns>The name of the namespace to import.</returns>
		// Token: 0x17000EC2 RID: 3778
		// (get) Token: 0x06003CD9 RID: 15577 RVA: 0x000D9B40 File Offset: 0x000D7D40
		// (set) Token: 0x06003CDA RID: 15578 RVA: 0x000D9B51 File Offset: 0x000D7D51
		public string Namespace
		{
			get
			{
				return this._nameSpace ?? string.Empty;
			}
			set
			{
				this._nameSpace = value;
			}
		}

		// Token: 0x04002DCA RID: 11722
		private string _nameSpace;
	}
}
