using System;

namespace System.CodeDom
{
	/// <summary>Represents a namespace declaration.</summary>
	// Token: 0x0200077D RID: 1917
	[Serializable]
	public class CodeNamespace : CodeObject
	{
		/// <summary>An event that will be raised the first time the <see cref="P:System.CodeDom.CodeNamespace.Comments" /> collection is accessed.</summary>
		// Token: 0x1400006E RID: 110
		// (add) Token: 0x06003CBB RID: 15547 RVA: 0x000D9818 File Offset: 0x000D7A18
		// (remove) Token: 0x06003CBC RID: 15548 RVA: 0x000D9850 File Offset: 0x000D7A50
		public event EventHandler PopulateComments;

		/// <summary>An event that will be raised the first time the <see cref="P:System.CodeDom.CodeNamespace.Imports" /> collection is accessed.</summary>
		// Token: 0x1400006F RID: 111
		// (add) Token: 0x06003CBD RID: 15549 RVA: 0x000D9888 File Offset: 0x000D7A88
		// (remove) Token: 0x06003CBE RID: 15550 RVA: 0x000D98C0 File Offset: 0x000D7AC0
		public event EventHandler PopulateImports;

		/// <summary>An event that will be raised the first time the <see cref="P:System.CodeDom.CodeNamespace.Types" /> collection is accessed.</summary>
		// Token: 0x14000070 RID: 112
		// (add) Token: 0x06003CBF RID: 15551 RVA: 0x000D98F8 File Offset: 0x000D7AF8
		// (remove) Token: 0x06003CC0 RID: 15552 RVA: 0x000D9930 File Offset: 0x000D7B30
		public event EventHandler PopulateTypes;

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeNamespace" /> class.</summary>
		// Token: 0x06003CC1 RID: 15553 RVA: 0x000D9965 File Offset: 0x000D7B65
		public CodeNamespace()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeNamespace" /> class using the specified name.</summary>
		/// <param name="name">The name of the namespace being declared. </param>
		// Token: 0x06003CC2 RID: 15554 RVA: 0x000D998E File Offset: 0x000D7B8E
		public CodeNamespace(string name)
		{
			this.Name = name;
		}

		/// <summary>Gets the collection of types that the namespace contains.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeTypeDeclarationCollection" /> that indicates the types contained in the namespace.</returns>
		// Token: 0x17000EBC RID: 3772
		// (get) Token: 0x06003CC3 RID: 15555 RVA: 0x000D99BE File Offset: 0x000D7BBE
		public CodeTypeDeclarationCollection Types
		{
			get
			{
				if ((this._populated & 4) == 0)
				{
					this._populated |= 4;
					EventHandler populateTypes = this.PopulateTypes;
					if (populateTypes != null)
					{
						populateTypes(this, EventArgs.Empty);
					}
				}
				return this._classes;
			}
		}

		/// <summary>Gets the collection of namespace import directives used by the namespace.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeNamespaceImportCollection" /> that indicates the namespace import directives used by the namespace.</returns>
		// Token: 0x17000EBD RID: 3773
		// (get) Token: 0x06003CC4 RID: 15556 RVA: 0x000D99F5 File Offset: 0x000D7BF5
		public CodeNamespaceImportCollection Imports
		{
			get
			{
				if ((this._populated & 1) == 0)
				{
					this._populated |= 1;
					EventHandler populateImports = this.PopulateImports;
					if (populateImports != null)
					{
						populateImports(this, EventArgs.Empty);
					}
				}
				return this._imports;
			}
		}

		/// <summary>Gets or sets the name of the namespace.</summary>
		/// <returns>The name of the namespace.</returns>
		// Token: 0x17000EBE RID: 3774
		// (get) Token: 0x06003CC5 RID: 15557 RVA: 0x000D9A2C File Offset: 0x000D7C2C
		// (set) Token: 0x06003CC6 RID: 15558 RVA: 0x000D9A3D File Offset: 0x000D7C3D
		public string Name
		{
			get
			{
				return this._name ?? string.Empty;
			}
			set
			{
				this._name = value;
			}
		}

		/// <summary>Gets the comments for the namespace.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeCommentStatementCollection" /> that indicates the comments for the namespace.</returns>
		// Token: 0x17000EBF RID: 3775
		// (get) Token: 0x06003CC7 RID: 15559 RVA: 0x000D9A46 File Offset: 0x000D7C46
		public CodeCommentStatementCollection Comments
		{
			get
			{
				if ((this._populated & 2) == 0)
				{
					this._populated |= 2;
					EventHandler populateComments = this.PopulateComments;
					if (populateComments != null)
					{
						populateComments(this, EventArgs.Empty);
					}
				}
				return this._comments;
			}
		}

		// Token: 0x04002DBF RID: 11711
		private string _name;

		// Token: 0x04002DC0 RID: 11712
		private readonly CodeNamespaceImportCollection _imports = new CodeNamespaceImportCollection();

		// Token: 0x04002DC1 RID: 11713
		private readonly CodeCommentStatementCollection _comments = new CodeCommentStatementCollection();

		// Token: 0x04002DC2 RID: 11714
		private readonly CodeTypeDeclarationCollection _classes = new CodeTypeDeclarationCollection();

		// Token: 0x04002DC3 RID: 11715
		private int _populated;

		// Token: 0x04002DC4 RID: 11716
		private const int ImportsCollection = 1;

		// Token: 0x04002DC5 RID: 11717
		private const int CommentsCollection = 2;

		// Token: 0x04002DC6 RID: 11718
		private const int TypesCollection = 4;
	}
}
