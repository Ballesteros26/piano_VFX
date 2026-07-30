using System;

namespace System.CodeDom
{
	/// <summary>Represents an attribute declaration.</summary>
	// Token: 0x02000757 RID: 1879
	[Serializable]
	public class CodeAttributeDeclaration
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeAttributeDeclaration" /> class.</summary>
		// Token: 0x06003BB3 RID: 15283 RVA: 0x000D8690 File Offset: 0x000D6890
		public CodeAttributeDeclaration()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeAttributeDeclaration" /> class using the specified name.</summary>
		/// <param name="name">The name of the attribute. </param>
		// Token: 0x06003BB4 RID: 15284 RVA: 0x000D86A3 File Offset: 0x000D68A3
		public CodeAttributeDeclaration(string name)
		{
			this.Name = name;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeAttributeDeclaration" /> class using the specified name and arguments.</summary>
		/// <param name="name">The name of the attribute. </param>
		/// <param name="arguments">An array of type <see cref="T:System.CodeDom.CodeAttributeArgument" />  that contains the arguments for the attribute. </param>
		// Token: 0x06003BB5 RID: 15285 RVA: 0x000D86BD File Offset: 0x000D68BD
		public CodeAttributeDeclaration(string name, params CodeAttributeArgument[] arguments)
		{
			this.Name = name;
			this.Arguments.AddRange(arguments);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeAttributeDeclaration" /> class using the specified code type reference.</summary>
		/// <param name="attributeType">The <see cref="T:System.CodeDom.CodeTypeReference" /> that identifies the attribute.</param>
		// Token: 0x06003BB6 RID: 15286 RVA: 0x000D86E3 File Offset: 0x000D68E3
		public CodeAttributeDeclaration(CodeTypeReference attributeType)
			: this(attributeType, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeAttributeDeclaration" /> class using the specified code type reference and arguments.</summary>
		/// <param name="attributeType">The <see cref="T:System.CodeDom.CodeTypeReference" /> that identifies the attribute.</param>
		/// <param name="arguments">An array of type <see cref="T:System.CodeDom.CodeAttributeArgument" /> that contains the arguments for the attribute.</param>
		// Token: 0x06003BB7 RID: 15287 RVA: 0x000D86ED File Offset: 0x000D68ED
		public CodeAttributeDeclaration(CodeTypeReference attributeType, params CodeAttributeArgument[] arguments)
		{
			this._attributeType = attributeType;
			if (attributeType != null)
			{
				this._name = attributeType.BaseType;
			}
			if (arguments != null)
			{
				this.Arguments.AddRange(arguments);
			}
		}

		/// <summary>Gets or sets the name of the attribute being declared.</summary>
		/// <returns>The name of the attribute.</returns>
		// Token: 0x17000E6D RID: 3693
		// (get) Token: 0x06003BB8 RID: 15288 RVA: 0x000D8725 File Offset: 0x000D6925
		// (set) Token: 0x06003BB9 RID: 15289 RVA: 0x000D8736 File Offset: 0x000D6936
		public string Name
		{
			get
			{
				return this._name ?? string.Empty;
			}
			set
			{
				this._name = value;
				this._attributeType = new CodeTypeReference(this._name);
			}
		}

		/// <summary>Gets the arguments for the attribute.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeAttributeArgumentCollection" /> that contains the arguments for the attribute.</returns>
		// Token: 0x17000E6E RID: 3694
		// (get) Token: 0x06003BBA RID: 15290 RVA: 0x000D8750 File Offset: 0x000D6950
		public CodeAttributeArgumentCollection Arguments
		{
			get
			{
				return this._arguments;
			}
		}

		/// <summary>Gets the code type reference for the code attribute declaration.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeTypeReference" /> that identifies the <see cref="T:System.CodeDom.CodeAttributeDeclaration" />.</returns>
		// Token: 0x17000E6F RID: 3695
		// (get) Token: 0x06003BBB RID: 15291 RVA: 0x000D8758 File Offset: 0x000D6958
		public CodeTypeReference AttributeType
		{
			get
			{
				return this._attributeType;
			}
		}

		// Token: 0x04002D5C RID: 11612
		private string _name;

		// Token: 0x04002D5D RID: 11613
		private readonly CodeAttributeArgumentCollection _arguments = new CodeAttributeArgumentCollection();

		// Token: 0x04002D5E RID: 11614
		private CodeTypeReference _attributeType;
	}
}
