using System;
using System.Reflection;

namespace System.CodeDom
{
	/// <summary>Represents a type declaration for a class, structure, interface, or enumeration.</summary>
	// Token: 0x02000794 RID: 1940
	[Serializable]
	public class CodeTypeDeclaration : CodeTypeMember
	{
		/// <summary>Occurs when the <see cref="P:System.CodeDom.CodeTypeDeclaration.BaseTypes" /> collection is accessed for the first time.</summary>
		// Token: 0x14000071 RID: 113
		// (add) Token: 0x06003D5D RID: 15709 RVA: 0x000DA3B8 File Offset: 0x000D85B8
		// (remove) Token: 0x06003D5E RID: 15710 RVA: 0x000DA3F0 File Offset: 0x000D85F0
		public event EventHandler PopulateBaseTypes;

		/// <summary>Occurs when the <see cref="P:System.CodeDom.CodeTypeDeclaration.Members" /> collection is accessed for the first time.</summary>
		// Token: 0x14000072 RID: 114
		// (add) Token: 0x06003D5F RID: 15711 RVA: 0x000DA428 File Offset: 0x000D8628
		// (remove) Token: 0x06003D60 RID: 15712 RVA: 0x000DA460 File Offset: 0x000D8660
		public event EventHandler PopulateMembers;

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeTypeDeclaration" /> class.</summary>
		// Token: 0x06003D61 RID: 15713 RVA: 0x000DA495 File Offset: 0x000D8695
		public CodeTypeDeclaration()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeTypeDeclaration" /> class with the specified name.</summary>
		/// <param name="name">The name for the new type. </param>
		// Token: 0x06003D62 RID: 15714 RVA: 0x000DA4BA File Offset: 0x000D86BA
		public CodeTypeDeclaration(string name)
		{
			base.Name = name;
		}

		/// <summary>Gets or sets the attributes of the type.</summary>
		/// <returns>A <see cref="T:System.Reflection.TypeAttributes" /> object that indicates the attributes of the type.</returns>
		// Token: 0x17000EE6 RID: 3814
		// (get) Token: 0x06003D63 RID: 15715 RVA: 0x000DA4E6 File Offset: 0x000D86E6
		// (set) Token: 0x06003D64 RID: 15716 RVA: 0x000DA4EE File Offset: 0x000D86EE
		public TypeAttributes TypeAttributes { get; set; } = TypeAttributes.Public;

		/// <summary>Gets the base types of the type.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeTypeReferenceCollection" /> object that indicates the base types of the type.</returns>
		// Token: 0x17000EE7 RID: 3815
		// (get) Token: 0x06003D65 RID: 15717 RVA: 0x000DA4F7 File Offset: 0x000D86F7
		public CodeTypeReferenceCollection BaseTypes
		{
			get
			{
				if ((this._populated & 1) == 0)
				{
					this._populated |= 1;
					EventHandler populateBaseTypes = this.PopulateBaseTypes;
					if (populateBaseTypes != null)
					{
						populateBaseTypes(this, EventArgs.Empty);
					}
				}
				return this._baseTypes;
			}
		}

		/// <summary>Gets or sets a value indicating whether the type is a class or reference type.</summary>
		/// <returns>true if the type is a class or reference type; otherwise, false.</returns>
		// Token: 0x17000EE8 RID: 3816
		// (get) Token: 0x06003D66 RID: 15718 RVA: 0x000DA52E File Offset: 0x000D872E
		// (set) Token: 0x06003D67 RID: 15719 RVA: 0x000DA54E File Offset: 0x000D874E
		public bool IsClass
		{
			get
			{
				return (this.TypeAttributes & TypeAttributes.ClassSemanticsMask) == TypeAttributes.NotPublic && !this._isEnum && !this._isStruct;
			}
			set
			{
				if (value)
				{
					this.TypeAttributes &= ~TypeAttributes.ClassSemanticsMask;
					this.TypeAttributes |= TypeAttributes.NotPublic;
					this._isStruct = false;
					this._isEnum = false;
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether the type is a value type (struct).</summary>
		/// <returns>true if the type is a value type; otherwise, false.</returns>
		// Token: 0x17000EE9 RID: 3817
		// (get) Token: 0x06003D68 RID: 15720 RVA: 0x000DA57E File Offset: 0x000D877E
		// (set) Token: 0x06003D69 RID: 15721 RVA: 0x000DA586 File Offset: 0x000D8786
		public bool IsStruct
		{
			get
			{
				return this._isStruct;
			}
			set
			{
				if (value)
				{
					this.TypeAttributes &= ~TypeAttributes.ClassSemanticsMask;
					this._isEnum = false;
				}
				this._isStruct = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the type is an enumeration.</summary>
		/// <returns>true if the type is an enumeration; otherwise, false.</returns>
		// Token: 0x17000EEA RID: 3818
		// (get) Token: 0x06003D6A RID: 15722 RVA: 0x000DA5A8 File Offset: 0x000D87A8
		// (set) Token: 0x06003D6B RID: 15723 RVA: 0x000DA5B0 File Offset: 0x000D87B0
		public bool IsEnum
		{
			get
			{
				return this._isEnum;
			}
			set
			{
				if (value)
				{
					this.TypeAttributes &= ~TypeAttributes.ClassSemanticsMask;
					this._isStruct = false;
				}
				this._isEnum = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the type is an interface.</summary>
		/// <returns>true if the type is an interface; otherwise, false.</returns>
		// Token: 0x17000EEB RID: 3819
		// (get) Token: 0x06003D6C RID: 15724 RVA: 0x000DA5D2 File Offset: 0x000D87D2
		// (set) Token: 0x06003D6D RID: 15725 RVA: 0x000DA5E4 File Offset: 0x000D87E4
		public bool IsInterface
		{
			get
			{
				return (this.TypeAttributes & TypeAttributes.ClassSemanticsMask) == TypeAttributes.ClassSemanticsMask;
			}
			set
			{
				if (value)
				{
					this.TypeAttributes &= ~TypeAttributes.ClassSemanticsMask;
					this.TypeAttributes |= TypeAttributes.ClassSemanticsMask;
					this._isStruct = false;
					this._isEnum = false;
					return;
				}
				this.TypeAttributes &= ~TypeAttributes.ClassSemanticsMask;
			}
		}

		/// <summary>Gets or sets a value indicating whether the type declaration is complete or partial.</summary>
		/// <returns>true if the class or structure declaration is a partial representation of the implementation; false if the declaration is a complete implementation of the class or structure. The default is false.</returns>
		// Token: 0x17000EEC RID: 3820
		// (get) Token: 0x06003D6E RID: 15726 RVA: 0x000DA630 File Offset: 0x000D8830
		// (set) Token: 0x06003D6F RID: 15727 RVA: 0x000DA638 File Offset: 0x000D8838
		public bool IsPartial { get; set; }

		/// <summary>Gets the collection of class members for the represented type.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeTypeMemberCollection" /> object that indicates the class members.</returns>
		// Token: 0x17000EED RID: 3821
		// (get) Token: 0x06003D70 RID: 15728 RVA: 0x000DA641 File Offset: 0x000D8841
		public CodeTypeMemberCollection Members
		{
			get
			{
				if ((this._populated & 2) == 0)
				{
					this._populated |= 2;
					EventHandler populateMembers = this.PopulateMembers;
					if (populateMembers != null)
					{
						populateMembers(this, EventArgs.Empty);
					}
				}
				return this._members;
			}
		}

		/// <summary>Gets the type parameters for the type declaration.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeTypeParameterCollection" /> that contains the type parameters for the type declaration.</returns>
		// Token: 0x17000EEE RID: 3822
		// (get) Token: 0x06003D71 RID: 15729 RVA: 0x000DA678 File Offset: 0x000D8878
		public CodeTypeParameterCollection TypeParameters
		{
			get
			{
				CodeTypeParameterCollection codeTypeParameterCollection;
				if ((codeTypeParameterCollection = this._typeParameters) == null)
				{
					codeTypeParameterCollection = (this._typeParameters = new CodeTypeParameterCollection());
				}
				return codeTypeParameterCollection;
			}
		}

		// Token: 0x04002DEB RID: 11755
		private readonly CodeTypeReferenceCollection _baseTypes = new CodeTypeReferenceCollection();

		// Token: 0x04002DEC RID: 11756
		private readonly CodeTypeMemberCollection _members = new CodeTypeMemberCollection();

		// Token: 0x04002DED RID: 11757
		private bool _isEnum;

		// Token: 0x04002DEE RID: 11758
		private bool _isStruct;

		// Token: 0x04002DEF RID: 11759
		private int _populated;

		// Token: 0x04002DF0 RID: 11760
		private const int BaseTypesCollection = 1;

		// Token: 0x04002DF1 RID: 11761
		private const int MembersCollection = 2;

		// Token: 0x04002DF2 RID: 11762
		private CodeTypeParameterCollection _typeParameters;
	}
}
