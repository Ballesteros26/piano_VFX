using System;

namespace System.CodeDom
{
	/// <summary>Represents an expression that creates a new instance of a type.</summary>
	// Token: 0x02000781 RID: 1921
	[Serializable]
	public class CodeObjectCreateExpression : CodeExpression
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeObjectCreateExpression" /> class.</summary>
		// Token: 0x06003CF4 RID: 15604 RVA: 0x000D9D74 File Offset: 0x000D7F74
		public CodeObjectCreateExpression()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeObjectCreateExpression" /> class using the specified type and parameters.</summary>
		/// <param name="createType">A <see cref="T:System.CodeDom.CodeTypeReference" /> that indicates the data type of the object to create. </param>
		/// <param name="parameters">An array of <see cref="T:System.CodeDom.CodeExpression" /> objects that indicates the parameters to use to create the object. </param>
		// Token: 0x06003CF5 RID: 15605 RVA: 0x000D9D87 File Offset: 0x000D7F87
		public CodeObjectCreateExpression(CodeTypeReference createType, params CodeExpression[] parameters)
		{
			this.CreateType = createType;
			this.Parameters.AddRange(parameters);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeObjectCreateExpression" /> class using the specified type and parameters.</summary>
		/// <param name="createType">The name of the data type of object to create. </param>
		/// <param name="parameters">An array of <see cref="T:System.CodeDom.CodeExpression" /> objects that indicates the parameters to use to create the object. </param>
		// Token: 0x06003CF6 RID: 15606 RVA: 0x000D9DAD File Offset: 0x000D7FAD
		public CodeObjectCreateExpression(string createType, params CodeExpression[] parameters)
		{
			this.CreateType = new CodeTypeReference(createType);
			this.Parameters.AddRange(parameters);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeObjectCreateExpression" /> class using the specified type and parameters.</summary>
		/// <param name="createType">The data type of the object to create. </param>
		/// <param name="parameters">An array of <see cref="T:System.CodeDom.CodeExpression" /> objects that indicates the parameters to use to create the object. </param>
		// Token: 0x06003CF7 RID: 15607 RVA: 0x000D9DD8 File Offset: 0x000D7FD8
		public CodeObjectCreateExpression(Type createType, params CodeExpression[] parameters)
		{
			this.CreateType = new CodeTypeReference(createType);
			this.Parameters.AddRange(parameters);
		}

		/// <summary>Gets or sets the data type of the object to create.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeTypeReference" /> to the data type of the object to create.</returns>
		// Token: 0x17000ECB RID: 3787
		// (get) Token: 0x06003CF8 RID: 15608 RVA: 0x000D9E04 File Offset: 0x000D8004
		// (set) Token: 0x06003CF9 RID: 15609 RVA: 0x000D9E2E File Offset: 0x000D802E
		public CodeTypeReference CreateType
		{
			get
			{
				CodeTypeReference codeTypeReference;
				if ((codeTypeReference = this._createType) == null)
				{
					codeTypeReference = (this._createType = new CodeTypeReference(""));
				}
				return codeTypeReference;
			}
			set
			{
				this._createType = value;
			}
		}

		/// <summary>Gets or sets the parameters to use in creating the object.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeExpressionCollection" /> that indicates the parameters to use when creating the object.</returns>
		// Token: 0x17000ECC RID: 3788
		// (get) Token: 0x06003CFA RID: 15610 RVA: 0x000D9E37 File Offset: 0x000D8037
		public CodeExpressionCollection Parameters { get; } = new CodeExpressionCollection();

		// Token: 0x04002DCE RID: 11726
		private CodeTypeReference _createType;
	}
}
