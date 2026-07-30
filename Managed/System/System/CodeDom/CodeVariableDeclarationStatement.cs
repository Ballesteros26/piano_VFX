using System;

namespace System.CodeDom
{
	/// <summary>Represents a variable declaration.</summary>
	// Token: 0x0200079D RID: 1949
	[Serializable]
	public class CodeVariableDeclarationStatement : CodeStatement
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeVariableDeclarationStatement" /> class.</summary>
		// Token: 0x06003DBF RID: 15807 RVA: 0x000D84F9 File Offset: 0x000D66F9
		public CodeVariableDeclarationStatement()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeVariableDeclarationStatement" /> class using the specified type and name.</summary>
		/// <param name="type">A <see cref="T:System.CodeDom.CodeTypeReference" /> that indicates the data type of the variable. </param>
		/// <param name="name">The name of the variable. </param>
		// Token: 0x06003DC0 RID: 15808 RVA: 0x000DAB77 File Offset: 0x000D8D77
		public CodeVariableDeclarationStatement(CodeTypeReference type, string name)
		{
			this.Type = type;
			this.Name = name;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeVariableDeclarationStatement" /> class using the specified data type name and variable name.</summary>
		/// <param name="type">The name of the data type of the variable. </param>
		/// <param name="name">The name of the variable. </param>
		// Token: 0x06003DC1 RID: 15809 RVA: 0x000DAB8D File Offset: 0x000D8D8D
		public CodeVariableDeclarationStatement(string type, string name)
		{
			this.Type = new CodeTypeReference(type);
			this.Name = name;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeVariableDeclarationStatement" /> class using the specified data type and variable name.</summary>
		/// <param name="type">The data type for the variable. </param>
		/// <param name="name">The name of the variable. </param>
		// Token: 0x06003DC2 RID: 15810 RVA: 0x000DABA8 File Offset: 0x000D8DA8
		public CodeVariableDeclarationStatement(Type type, string name)
		{
			this.Type = new CodeTypeReference(type);
			this.Name = name;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeVariableDeclarationStatement" /> class using the specified data type, variable name, and initialization expression.</summary>
		/// <param name="type">A <see cref="T:System.CodeDom.CodeTypeReference" /> that indicates the type of the variable. </param>
		/// <param name="name">The name of the variable. </param>
		/// <param name="initExpression">A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the initialization expression for the variable. </param>
		// Token: 0x06003DC3 RID: 15811 RVA: 0x000DABC3 File Offset: 0x000D8DC3
		public CodeVariableDeclarationStatement(CodeTypeReference type, string name, CodeExpression initExpression)
		{
			this.Type = type;
			this.Name = name;
			this.InitExpression = initExpression;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeVariableDeclarationStatement" /> class using the specified data type, variable name, and initialization expression.</summary>
		/// <param name="type">The name of the data type of the variable. </param>
		/// <param name="name">The name of the variable. </param>
		/// <param name="initExpression">A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the initialization expression for the variable. </param>
		// Token: 0x06003DC4 RID: 15812 RVA: 0x000DABE0 File Offset: 0x000D8DE0
		public CodeVariableDeclarationStatement(string type, string name, CodeExpression initExpression)
		{
			this.Type = new CodeTypeReference(type);
			this.Name = name;
			this.InitExpression = initExpression;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeVariableDeclarationStatement" /> class using the specified data type, variable name, and initialization expression.</summary>
		/// <param name="type">The data type of the variable. </param>
		/// <param name="name">The name of the variable. </param>
		/// <param name="initExpression">A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the initialization expression for the variable. </param>
		// Token: 0x06003DC5 RID: 15813 RVA: 0x000DAC02 File Offset: 0x000D8E02
		public CodeVariableDeclarationStatement(Type type, string name, CodeExpression initExpression)
		{
			this.Type = new CodeTypeReference(type);
			this.Name = name;
			this.InitExpression = initExpression;
		}

		/// <summary>Gets or sets the initialization expression for the variable.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the initialization expression for the variable.</returns>
		// Token: 0x17000F01 RID: 3841
		// (get) Token: 0x06003DC6 RID: 15814 RVA: 0x000DAC24 File Offset: 0x000D8E24
		// (set) Token: 0x06003DC7 RID: 15815 RVA: 0x000DAC2C File Offset: 0x000D8E2C
		public CodeExpression InitExpression { get; set; }

		/// <summary>Gets or sets the name of the variable.</summary>
		/// <returns>The name of the variable.</returns>
		// Token: 0x17000F02 RID: 3842
		// (get) Token: 0x06003DC8 RID: 15816 RVA: 0x000DAC35 File Offset: 0x000D8E35
		// (set) Token: 0x06003DC9 RID: 15817 RVA: 0x000DAC46 File Offset: 0x000D8E46
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

		/// <summary>Gets or sets the data type of the variable.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeTypeReference" /> that indicates the data type of the variable.</returns>
		// Token: 0x17000F03 RID: 3843
		// (get) Token: 0x06003DCA RID: 15818 RVA: 0x000DAC50 File Offset: 0x000D8E50
		// (set) Token: 0x06003DCB RID: 15819 RVA: 0x000DAC7A File Offset: 0x000D8E7A
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

		// Token: 0x04002E06 RID: 11782
		private CodeTypeReference _type;

		// Token: 0x04002E07 RID: 11783
		private string _name;
	}
}
