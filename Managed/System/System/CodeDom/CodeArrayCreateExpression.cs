using System;

namespace System.CodeDom
{
	/// <summary>Represents an expression that creates an array.</summary>
	// Token: 0x02000751 RID: 1873
	[Serializable]
	public class CodeArrayCreateExpression : CodeExpression
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeArrayCreateExpression" /> class.</summary>
		// Token: 0x06003B7C RID: 15228 RVA: 0x000D82DD File Offset: 0x000D64DD
		public CodeArrayCreateExpression()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeArrayCreateExpression" /> class using the specified array data type and initialization expressions.</summary>
		/// <param name="createType">A <see cref="T:System.CodeDom.CodeTypeReference" /> that indicates the data type of the array to create. </param>
		/// <param name="initializers">An array of expressions to use to initialize the array. </param>
		// Token: 0x06003B7D RID: 15229 RVA: 0x000D82F0 File Offset: 0x000D64F0
		public CodeArrayCreateExpression(CodeTypeReference createType, params CodeExpression[] initializers)
		{
			this._createType = createType;
			this._initializers.AddRange(initializers);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeArrayCreateExpression" /> class using the specified array data type name and initializers.</summary>
		/// <param name="createType">The name of the data type of the array to create. </param>
		/// <param name="initializers">An array of expressions to use to initialize the array. </param>
		// Token: 0x06003B7E RID: 15230 RVA: 0x000D8316 File Offset: 0x000D6516
		public CodeArrayCreateExpression(string createType, params CodeExpression[] initializers)
		{
			this._createType = new CodeTypeReference(createType);
			this._initializers.AddRange(initializers);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeArrayCreateExpression" /> class using the specified array data type and initializers.</summary>
		/// <param name="createType">The data type of the array to create. </param>
		/// <param name="initializers">An array of expressions to use to initialize the array. </param>
		// Token: 0x06003B7F RID: 15231 RVA: 0x000D8341 File Offset: 0x000D6541
		public CodeArrayCreateExpression(Type createType, params CodeExpression[] initializers)
		{
			this._createType = new CodeTypeReference(createType);
			this._initializers.AddRange(initializers);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeArrayCreateExpression" /> class using the specified array data type and number of indexes for the array.</summary>
		/// <param name="createType">A <see cref="T:System.CodeDom.CodeTypeReference" /> indicating the data type of the array to create. </param>
		/// <param name="size">The number of indexes of the array to create. </param>
		// Token: 0x06003B80 RID: 15232 RVA: 0x000D836C File Offset: 0x000D656C
		public CodeArrayCreateExpression(CodeTypeReference createType, int size)
		{
			this._createType = createType;
			this.Size = size;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeArrayCreateExpression" /> class using the specified array data type name and number of indexes for the array.</summary>
		/// <param name="createType">The name of the data type of the array to create. </param>
		/// <param name="size">The number of indexes of the array to create. </param>
		// Token: 0x06003B81 RID: 15233 RVA: 0x000D838D File Offset: 0x000D658D
		public CodeArrayCreateExpression(string createType, int size)
		{
			this._createType = new CodeTypeReference(createType);
			this.Size = size;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeArrayCreateExpression" /> class using the specified array data type and number of indexes for the array.</summary>
		/// <param name="createType">The data type of the array to create. </param>
		/// <param name="size">The number of indexes of the array to create. </param>
		// Token: 0x06003B82 RID: 15234 RVA: 0x000D83B3 File Offset: 0x000D65B3
		public CodeArrayCreateExpression(Type createType, int size)
		{
			this._createType = new CodeTypeReference(createType);
			this.Size = size;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeArrayCreateExpression" /> class using the specified array data type and code expression indicating the number of indexes for the array.</summary>
		/// <param name="createType">A <see cref="T:System.CodeDom.CodeTypeReference" /> indicating the data type of the array to create. </param>
		/// <param name="size">An expression that indicates the number of indexes of the array to create. </param>
		// Token: 0x06003B83 RID: 15235 RVA: 0x000D83D9 File Offset: 0x000D65D9
		public CodeArrayCreateExpression(CodeTypeReference createType, CodeExpression size)
		{
			this._createType = createType;
			this.SizeExpression = size;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeArrayCreateExpression" /> class using the specified array data type name and code expression indicating the number of indexes for the array.</summary>
		/// <param name="createType">The name of the data type of the array to create. </param>
		/// <param name="size">An expression that indicates the number of indexes of the array to create. </param>
		// Token: 0x06003B84 RID: 15236 RVA: 0x000D83FA File Offset: 0x000D65FA
		public CodeArrayCreateExpression(string createType, CodeExpression size)
		{
			this._createType = new CodeTypeReference(createType);
			this.SizeExpression = size;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeArrayCreateExpression" /> class using the specified array data type and code expression indicating the number of indexes for the array.</summary>
		/// <param name="createType">The data type of the array to create. </param>
		/// <param name="size">An expression that indicates the number of indexes of the array to create. </param>
		// Token: 0x06003B85 RID: 15237 RVA: 0x000D8420 File Offset: 0x000D6620
		public CodeArrayCreateExpression(Type createType, CodeExpression size)
		{
			this._createType = new CodeTypeReference(createType);
			this.SizeExpression = size;
		}

		/// <summary>Gets or sets the type of array to create.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeTypeReference" /> that indicates the type of the array.</returns>
		// Token: 0x17000E60 RID: 3680
		// (get) Token: 0x06003B86 RID: 15238 RVA: 0x000D8448 File Offset: 0x000D6648
		// (set) Token: 0x06003B87 RID: 15239 RVA: 0x000D8472 File Offset: 0x000D6672
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

		/// <summary>Gets the initializers with which to initialize the array.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeExpressionCollection" /> that indicates the initialization values.</returns>
		// Token: 0x17000E61 RID: 3681
		// (get) Token: 0x06003B88 RID: 15240 RVA: 0x000D847B File Offset: 0x000D667B
		public CodeExpressionCollection Initializers
		{
			get
			{
				return this._initializers;
			}
		}

		/// <summary>Gets or sets the number of indexes in the array.</summary>
		/// <returns>The number of indexes in the array.</returns>
		// Token: 0x17000E62 RID: 3682
		// (get) Token: 0x06003B89 RID: 15241 RVA: 0x000D8483 File Offset: 0x000D6683
		// (set) Token: 0x06003B8A RID: 15242 RVA: 0x000D848B File Offset: 0x000D668B
		public int Size { get; set; }

		/// <summary>Gets or sets the expression that indicates the size of the array.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the size of the array.</returns>
		// Token: 0x17000E63 RID: 3683
		// (get) Token: 0x06003B8B RID: 15243 RVA: 0x000D8494 File Offset: 0x000D6694
		// (set) Token: 0x06003B8C RID: 15244 RVA: 0x000D849C File Offset: 0x000D669C
		public CodeExpression SizeExpression { get; set; }

		// Token: 0x04002D50 RID: 11600
		private readonly CodeExpressionCollection _initializers = new CodeExpressionCollection();

		// Token: 0x04002D51 RID: 11601
		private CodeTypeReference _createType;
	}
}
