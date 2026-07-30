using System;

namespace System.CodeDom
{
	/// <summary>Represents a declaration for a property of a type.</summary>
	// Token: 0x0200077A RID: 1914
	[Serializable]
	public class CodeMemberProperty : CodeTypeMember
	{
		/// <summary>Gets or sets the data type of the interface, if any, this property, if private, implements.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeTypeReference" /> that indicates the data type of the interface, if any, the property, if private, implements.</returns>
		// Token: 0x17000EB1 RID: 3761
		// (get) Token: 0x06003CA4 RID: 15524 RVA: 0x000D964B File Offset: 0x000D784B
		// (set) Token: 0x06003CA5 RID: 15525 RVA: 0x000D9653 File Offset: 0x000D7853
		public CodeTypeReference PrivateImplementationType { get; set; }

		/// <summary>Gets the data types of any interfaces that the property implements.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeTypeReferenceCollection" /> that indicates the data types the property implements.</returns>
		// Token: 0x17000EB2 RID: 3762
		// (get) Token: 0x06003CA6 RID: 15526 RVA: 0x000D965C File Offset: 0x000D785C
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

		/// <summary>Gets or sets the data type of the property.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeTypeReference" /> that indicates the data type of the property.</returns>
		// Token: 0x17000EB3 RID: 3763
		// (get) Token: 0x06003CA7 RID: 15527 RVA: 0x000D9684 File Offset: 0x000D7884
		// (set) Token: 0x06003CA8 RID: 15528 RVA: 0x000D96AE File Offset: 0x000D78AE
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

		/// <summary>Gets or sets a value indicating whether the property has a get method accessor.</summary>
		/// <returns>true if the Count property of the <see cref="P:System.CodeDom.CodeMemberProperty.GetStatements" /> collection is non-zero, or if the value of this property has been set to true; otherwise, false.</returns>
		// Token: 0x17000EB4 RID: 3764
		// (get) Token: 0x06003CA9 RID: 15529 RVA: 0x000D96B7 File Offset: 0x000D78B7
		// (set) Token: 0x06003CAA RID: 15530 RVA: 0x000D96D1 File Offset: 0x000D78D1
		public bool HasGet
		{
			get
			{
				return this._hasGet || this.GetStatements.Count > 0;
			}
			set
			{
				this._hasGet = value;
				if (!value)
				{
					this.GetStatements.Clear();
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether the property has a set method accessor.</summary>
		/// <returns>true if the <see cref="P:System.Collections.CollectionBase.Count" /> property of the <see cref="P:System.CodeDom.CodeMemberProperty.SetStatements" /> collection is non-zero; otherwise, false.</returns>
		// Token: 0x17000EB5 RID: 3765
		// (get) Token: 0x06003CAB RID: 15531 RVA: 0x000D96E8 File Offset: 0x000D78E8
		// (set) Token: 0x06003CAC RID: 15532 RVA: 0x000D9702 File Offset: 0x000D7902
		public bool HasSet
		{
			get
			{
				return this._hasSet || this.SetStatements.Count > 0;
			}
			set
			{
				this._hasSet = value;
				if (!value)
				{
					this.SetStatements.Clear();
				}
			}
		}

		/// <summary>Gets the collection of get statements for the property.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeStatementCollection" /> that contains the get statements for the member property.</returns>
		// Token: 0x17000EB6 RID: 3766
		// (get) Token: 0x06003CAD RID: 15533 RVA: 0x000D9719 File Offset: 0x000D7919
		public CodeStatementCollection GetStatements { get; } = new CodeStatementCollection();

		/// <summary>Gets the collection of set statements for the property.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeStatementCollection" /> that contains the set statements for the member property.</returns>
		// Token: 0x17000EB7 RID: 3767
		// (get) Token: 0x06003CAE RID: 15534 RVA: 0x000D9721 File Offset: 0x000D7921
		public CodeStatementCollection SetStatements { get; } = new CodeStatementCollection();

		/// <summary>Gets the collection of declaration expressions for the property.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeParameterDeclarationExpressionCollection" /> that indicates the declaration expressions for the property.</returns>
		// Token: 0x17000EB8 RID: 3768
		// (get) Token: 0x06003CAF RID: 15535 RVA: 0x000D9729 File Offset: 0x000D7929
		public CodeParameterDeclarationExpressionCollection Parameters { get; } = new CodeParameterDeclarationExpressionCollection();

		// Token: 0x04002DB4 RID: 11700
		private CodeTypeReference _type;

		// Token: 0x04002DB5 RID: 11701
		private bool _hasGet;

		// Token: 0x04002DB6 RID: 11702
		private bool _hasSet;

		// Token: 0x04002DB7 RID: 11703
		private CodeTypeReferenceCollection _implementationTypes;
	}
}
