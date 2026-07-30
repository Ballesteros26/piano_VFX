using System;

namespace System.CodeDom
{
	/// <summary>Represents a declaration for a method of a type.</summary>
	// Token: 0x02000779 RID: 1913
	[Serializable]
	public class CodeMemberMethod : CodeTypeMember
	{
		/// <summary>An event that will be raised the first time the <see cref="P:System.CodeDom.CodeMemberMethod.Parameters" /> collection is accessed.</summary>
		// Token: 0x1400006B RID: 107
		// (add) Token: 0x06003C94 RID: 15508 RVA: 0x000D937C File Offset: 0x000D757C
		// (remove) Token: 0x06003C95 RID: 15509 RVA: 0x000D93B4 File Offset: 0x000D75B4
		public event EventHandler PopulateParameters;

		/// <summary>An event that will be raised the first time the <see cref="P:System.CodeDom.CodeMemberMethod.Statements" /> collection is accessed.</summary>
		// Token: 0x1400006C RID: 108
		// (add) Token: 0x06003C96 RID: 15510 RVA: 0x000D93EC File Offset: 0x000D75EC
		// (remove) Token: 0x06003C97 RID: 15511 RVA: 0x000D9424 File Offset: 0x000D7624
		public event EventHandler PopulateStatements;

		/// <summary>An event that will be raised the first time the <see cref="P:System.CodeDom.CodeMemberMethod.ImplementationTypes" /> collection is accessed.</summary>
		// Token: 0x1400006D RID: 109
		// (add) Token: 0x06003C98 RID: 15512 RVA: 0x000D945C File Offset: 0x000D765C
		// (remove) Token: 0x06003C99 RID: 15513 RVA: 0x000D9494 File Offset: 0x000D7694
		public event EventHandler PopulateImplementationTypes;

		/// <summary>Gets or sets the data type of the return value of the method.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeTypeReference" /> that indicates the data type of the value returned by the method.</returns>
		// Token: 0x17000EAA RID: 3754
		// (get) Token: 0x06003C9A RID: 15514 RVA: 0x000D94CC File Offset: 0x000D76CC
		// (set) Token: 0x06003C9B RID: 15515 RVA: 0x000D9500 File Offset: 0x000D7700
		public CodeTypeReference ReturnType
		{
			get
			{
				CodeTypeReference codeTypeReference;
				if ((codeTypeReference = this._returnType) == null)
				{
					codeTypeReference = (this._returnType = new CodeTypeReference(typeof(void).FullName));
				}
				return codeTypeReference;
			}
			set
			{
				this._returnType = value;
			}
		}

		/// <summary>Gets the statements within the method.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeStatementCollection" /> that indicates the statements within the method.</returns>
		// Token: 0x17000EAB RID: 3755
		// (get) Token: 0x06003C9C RID: 15516 RVA: 0x000D9509 File Offset: 0x000D7709
		public CodeStatementCollection Statements
		{
			get
			{
				if ((this._populated & 2) == 0)
				{
					this._populated |= 2;
					EventHandler populateStatements = this.PopulateStatements;
					if (populateStatements != null)
					{
						populateStatements(this, EventArgs.Empty);
					}
				}
				return this._statements;
			}
		}

		/// <summary>Gets the parameter declarations for the method.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeParameterDeclarationExpressionCollection" /> that indicates the method parameters.</returns>
		// Token: 0x17000EAC RID: 3756
		// (get) Token: 0x06003C9D RID: 15517 RVA: 0x000D9540 File Offset: 0x000D7740
		public CodeParameterDeclarationExpressionCollection Parameters
		{
			get
			{
				if ((this._populated & 1) == 0)
				{
					this._populated |= 1;
					EventHandler populateParameters = this.PopulateParameters;
					if (populateParameters != null)
					{
						populateParameters(this, EventArgs.Empty);
					}
				}
				return this._parameters;
			}
		}

		/// <summary>Gets or sets the data type of the interface this method, if private, implements a method of, if any.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeTypeReference" /> that indicates the data type of the interface with the method that the private method whose declaration is represented by this <see cref="T:System.CodeDom.CodeMemberMethod" /> implements.</returns>
		// Token: 0x17000EAD RID: 3757
		// (get) Token: 0x06003C9E RID: 15518 RVA: 0x000D9577 File Offset: 0x000D7777
		// (set) Token: 0x06003C9F RID: 15519 RVA: 0x000D957F File Offset: 0x000D777F
		public CodeTypeReference PrivateImplementationType { get; set; }

		/// <summary>Gets the data types of the interfaces implemented by this method, unless it is a private method implementation, which is indicated by the <see cref="P:System.CodeDom.CodeMemberMethod.PrivateImplementationType" /> property.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeTypeReferenceCollection" /> that indicates the interfaces implemented by this method.</returns>
		// Token: 0x17000EAE RID: 3758
		// (get) Token: 0x06003CA0 RID: 15520 RVA: 0x000D9588 File Offset: 0x000D7788
		public CodeTypeReferenceCollection ImplementationTypes
		{
			get
			{
				if (this._implementationTypes == null)
				{
					this._implementationTypes = new CodeTypeReferenceCollection();
				}
				if ((this._populated & 4) == 0)
				{
					this._populated |= 4;
					EventHandler populateImplementationTypes = this.PopulateImplementationTypes;
					if (populateImplementationTypes != null)
					{
						populateImplementationTypes(this, EventArgs.Empty);
					}
				}
				return this._implementationTypes;
			}
		}

		/// <summary>Gets the custom attributes of the return type of the method.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeAttributeDeclarationCollection" /> that indicates the custom attributes.</returns>
		// Token: 0x17000EAF RID: 3759
		// (get) Token: 0x06003CA1 RID: 15521 RVA: 0x000D95E0 File Offset: 0x000D77E0
		public CodeAttributeDeclarationCollection ReturnTypeCustomAttributes
		{
			get
			{
				CodeAttributeDeclarationCollection codeAttributeDeclarationCollection;
				if ((codeAttributeDeclarationCollection = this._returnAttributes) == null)
				{
					codeAttributeDeclarationCollection = (this._returnAttributes = new CodeAttributeDeclarationCollection());
				}
				return codeAttributeDeclarationCollection;
			}
		}

		/// <summary>Gets the type parameters for the current generic method.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeTypeParameterCollection" /> that contains the type parameters for the generic method.</returns>
		// Token: 0x17000EB0 RID: 3760
		// (get) Token: 0x06003CA2 RID: 15522 RVA: 0x000D9608 File Offset: 0x000D7808
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

		// Token: 0x04002DA6 RID: 11686
		private readonly CodeParameterDeclarationExpressionCollection _parameters = new CodeParameterDeclarationExpressionCollection();

		// Token: 0x04002DA7 RID: 11687
		private readonly CodeStatementCollection _statements = new CodeStatementCollection();

		// Token: 0x04002DA8 RID: 11688
		private CodeTypeReference _returnType;

		// Token: 0x04002DA9 RID: 11689
		private CodeTypeReferenceCollection _implementationTypes;

		// Token: 0x04002DAA RID: 11690
		private CodeAttributeDeclarationCollection _returnAttributes;

		// Token: 0x04002DAB RID: 11691
		private CodeTypeParameterCollection _typeParameters;

		// Token: 0x04002DAC RID: 11692
		private int _populated;

		// Token: 0x04002DAD RID: 11693
		private const int ParametersCollection = 1;

		// Token: 0x04002DAE RID: 11694
		private const int StatementsCollection = 2;

		// Token: 0x04002DAF RID: 11695
		private const int ImplTypesCollection = 4;
	}
}
