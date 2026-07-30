using System;

namespace System.CodeDom
{
	/// <summary>Represents a declaration for an event of a type.</summary>
	// Token: 0x02000777 RID: 1911
	[Serializable]
	public class CodeMemberEvent : CodeTypeMember
	{
		/// <summary>Gets or sets the data type of the delegate type that handles the event.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeTypeReference" /> that indicates the delegate type that handles the event.</returns>
		// Token: 0x17000EA5 RID: 3749
		// (get) Token: 0x06003C87 RID: 15495 RVA: 0x000D9280 File Offset: 0x000D7480
		// (set) Token: 0x06003C88 RID: 15496 RVA: 0x000D92AA File Offset: 0x000D74AA
		public CodeTypeReference Type
		{
			get
			{
				CodeTypeReference codeTypeReference;
				if ((codeTypeReference = this._type) == null)
				{
					codeTypeReference = (this._type = new CodeTypeReference(""));
				}
				return codeTypeReference;
			}
			set
			{
				this._type = value;
			}
		}

		/// <summary>Gets or sets the privately implemented data type, if any.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeTypeReference" /> that indicates the data type that the event privately implements.</returns>
		// Token: 0x17000EA6 RID: 3750
		// (get) Token: 0x06003C89 RID: 15497 RVA: 0x000D92B3 File Offset: 0x000D74B3
		// (set) Token: 0x06003C8A RID: 15498 RVA: 0x000D92BB File Offset: 0x000D74BB
		public CodeTypeReference PrivateImplementationType { get; set; }

		/// <summary>Gets or sets the data type that the member event implements.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeTypeReferenceCollection" /> that indicates the data type or types that the member event implements.</returns>
		// Token: 0x17000EA7 RID: 3751
		// (get) Token: 0x06003C8B RID: 15499 RVA: 0x000D92C4 File Offset: 0x000D74C4
		public CodeTypeReferenceCollection ImplementationTypes
		{
			get
			{
				CodeTypeReferenceCollection codeTypeReferenceCollection;
				if ((codeTypeReferenceCollection = this._implementationTypes) == null)
				{
					codeTypeReferenceCollection = (this._implementationTypes = new CodeTypeReferenceCollection());
				}
				return codeTypeReferenceCollection;
			}
		}

		// Token: 0x04002DA1 RID: 11681
		private CodeTypeReference _type;

		// Token: 0x04002DA2 RID: 11682
		private CodeTypeReferenceCollection _implementationTypes;
	}
}
