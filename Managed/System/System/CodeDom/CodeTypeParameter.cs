using System;

namespace System.CodeDom
{
	/// <summary>Represents a type parameter of a generic type or method.</summary>
	// Token: 0x0200079A RID: 1946
	[Serializable]
	public class CodeTypeParameter : CodeObject
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeTypeParameter" /> class. </summary>
		// Token: 0x06003DA3 RID: 15779 RVA: 0x000D8AA9 File Offset: 0x000D6CA9
		public CodeTypeParameter()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeTypeParameter" /> class with the specified type parameter name. </summary>
		/// <param name="name">The name of the type parameter.</param>
		// Token: 0x06003DA4 RID: 15780 RVA: 0x000DA9D3 File Offset: 0x000D8BD3
		public CodeTypeParameter(string name)
		{
			this._name = name;
		}

		/// <summary>Gets or sets the name of the type parameter.</summary>
		/// <returns>The name of the type parameter. The default is an empty string ("").</returns>
		// Token: 0x17000EFB RID: 3835
		// (get) Token: 0x06003DA5 RID: 15781 RVA: 0x000DA9E2 File Offset: 0x000D8BE2
		// (set) Token: 0x06003DA6 RID: 15782 RVA: 0x000DA9F3 File Offset: 0x000D8BF3
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

		/// <summary>Gets the constraints for the type parameter.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeTypeReferenceCollection" /> object that contains the constraints for the type parameter.</returns>
		// Token: 0x17000EFC RID: 3836
		// (get) Token: 0x06003DA7 RID: 15783 RVA: 0x000DA9FC File Offset: 0x000D8BFC
		public CodeTypeReferenceCollection Constraints
		{
			get
			{
				CodeTypeReferenceCollection codeTypeReferenceCollection;
				if ((codeTypeReferenceCollection = this._constraints) == null)
				{
					codeTypeReferenceCollection = (this._constraints = new CodeTypeReferenceCollection());
				}
				return codeTypeReferenceCollection;
			}
		}

		/// <summary>Gets the custom attributes of the type parameter.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeAttributeDeclarationCollection" /> that indicates the custom attributes of the type parameter. The default is null.</returns>
		// Token: 0x17000EFD RID: 3837
		// (get) Token: 0x06003DA8 RID: 15784 RVA: 0x000DAA24 File Offset: 0x000D8C24
		public CodeAttributeDeclarationCollection CustomAttributes
		{
			get
			{
				CodeAttributeDeclarationCollection codeAttributeDeclarationCollection;
				if ((codeAttributeDeclarationCollection = this._customAttributes) == null)
				{
					codeAttributeDeclarationCollection = (this._customAttributes = new CodeAttributeDeclarationCollection());
				}
				return codeAttributeDeclarationCollection;
			}
		}

		/// <summary>Gets or sets a value indicating whether the type parameter has a constructor constraint.</summary>
		/// <returns>true if the type parameter has a constructor constraint; otherwise, false. The default is false.</returns>
		// Token: 0x17000EFE RID: 3838
		// (get) Token: 0x06003DA9 RID: 15785 RVA: 0x000DAA49 File Offset: 0x000D8C49
		// (set) Token: 0x06003DAA RID: 15786 RVA: 0x000DAA51 File Offset: 0x000D8C51
		public bool HasConstructorConstraint { get; set; }

		// Token: 0x04002E01 RID: 11777
		private string _name;

		// Token: 0x04002E02 RID: 11778
		private CodeAttributeDeclarationCollection _customAttributes;

		// Token: 0x04002E03 RID: 11779
		private CodeTypeReferenceCollection _constraints;
	}
}
