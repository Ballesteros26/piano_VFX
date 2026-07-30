using System;

namespace System.CodeDom
{
	/// <summary>Provides a base class for a member of a type. Type members include fields, methods, properties, constructors and nested types.</summary>
	// Token: 0x02000797 RID: 1943
	[Serializable]
	public class CodeTypeMember : CodeObject
	{
		/// <summary>Gets or sets the name of the member.</summary>
		/// <returns>The name of the member.</returns>
		// Token: 0x17000EF2 RID: 3826
		// (get) Token: 0x06003D84 RID: 15748 RVA: 0x000DA7E7 File Offset: 0x000D89E7
		// (set) Token: 0x06003D85 RID: 15749 RVA: 0x000DA7F8 File Offset: 0x000D89F8
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

		/// <summary>Gets or sets the attributes of the member.</summary>
		/// <returns>A bitwise combination of the <see cref="T:System.CodeDom.MemberAttributes" /> values used to indicate the attributes of the member. The default value is <see cref="F:System.CodeDom.MemberAttributes.Private" /> | <see cref="F:System.CodeDom.MemberAttributes.Final" />. </returns>
		// Token: 0x17000EF3 RID: 3827
		// (get) Token: 0x06003D86 RID: 15750 RVA: 0x000DA801 File Offset: 0x000D8A01
		// (set) Token: 0x06003D87 RID: 15751 RVA: 0x000DA809 File Offset: 0x000D8A09
		public MemberAttributes Attributes { get; set; } = (MemberAttributes)20482;

		/// <summary>Gets or sets the custom attributes of the member.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeAttributeDeclarationCollection" /> that indicates the custom attributes of the member.</returns>
		// Token: 0x17000EF4 RID: 3828
		// (get) Token: 0x06003D88 RID: 15752 RVA: 0x000DA814 File Offset: 0x000D8A14
		// (set) Token: 0x06003D89 RID: 15753 RVA: 0x000DA839 File Offset: 0x000D8A39
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
			set
			{
				this._customAttributes = value;
			}
		}

		/// <summary>Gets or sets the line on which the type member statement occurs.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeLinePragma" /> object that indicates the location of the type member declaration.</returns>
		// Token: 0x17000EF5 RID: 3829
		// (get) Token: 0x06003D8A RID: 15754 RVA: 0x000DA842 File Offset: 0x000D8A42
		// (set) Token: 0x06003D8B RID: 15755 RVA: 0x000DA84A File Offset: 0x000D8A4A
		public CodeLinePragma LinePragma { get; set; }

		/// <summary>Gets the collection of comments for the type member.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeCommentStatementCollection" /> that indicates the comments for the member.</returns>
		// Token: 0x17000EF6 RID: 3830
		// (get) Token: 0x06003D8C RID: 15756 RVA: 0x000DA853 File Offset: 0x000D8A53
		public CodeCommentStatementCollection Comments { get; } = new CodeCommentStatementCollection();

		/// <summary>Gets the start directives for the member.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeDirectiveCollection" /> object containing start directives.</returns>
		// Token: 0x17000EF7 RID: 3831
		// (get) Token: 0x06003D8D RID: 15757 RVA: 0x000DA85C File Offset: 0x000D8A5C
		public CodeDirectiveCollection StartDirectives
		{
			get
			{
				CodeDirectiveCollection codeDirectiveCollection;
				if ((codeDirectiveCollection = this._startDirectives) == null)
				{
					codeDirectiveCollection = (this._startDirectives = new CodeDirectiveCollection());
				}
				return codeDirectiveCollection;
			}
		}

		/// <summary>Gets the end directives for the member.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeDirectiveCollection" /> object containing end directives.</returns>
		// Token: 0x17000EF8 RID: 3832
		// (get) Token: 0x06003D8E RID: 15758 RVA: 0x000DA884 File Offset: 0x000D8A84
		public CodeDirectiveCollection EndDirectives
		{
			get
			{
				CodeDirectiveCollection codeDirectiveCollection;
				if ((codeDirectiveCollection = this._endDirectives) == null)
				{
					codeDirectiveCollection = (this._endDirectives = new CodeDirectiveCollection());
				}
				return codeDirectiveCollection;
			}
		}

		// Token: 0x04002DF9 RID: 11769
		private string _name;

		// Token: 0x04002DFA RID: 11770
		private CodeAttributeDeclarationCollection _customAttributes;

		// Token: 0x04002DFB RID: 11771
		private CodeDirectiveCollection _startDirectives;

		// Token: 0x04002DFC RID: 11772
		private CodeDirectiveCollection _endDirectives;
	}
}
