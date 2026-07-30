using System;

namespace System.CodeDom
{
	/// <summary>Represents a parameter declaration for a method, property, or constructor.</summary>
	// Token: 0x02000782 RID: 1922
	[Serializable]
	public class CodeParameterDeclarationExpression : CodeExpression
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeParameterDeclarationExpression" /> class.</summary>
		// Token: 0x06003CFB RID: 15611 RVA: 0x000D82AC File Offset: 0x000D64AC
		public CodeParameterDeclarationExpression()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeParameterDeclarationExpression" /> class using the specified parameter type and name.</summary>
		/// <param name="type">An object that indicates the type of the parameter to declare. </param>
		/// <param name="name">The name of the parameter to declare. </param>
		// Token: 0x06003CFC RID: 15612 RVA: 0x000D9E3F File Offset: 0x000D803F
		public CodeParameterDeclarationExpression(CodeTypeReference type, string name)
		{
			this.Type = type;
			this.Name = name;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeParameterDeclarationExpression" /> class using the specified parameter type and name.</summary>
		/// <param name="type">The type of the parameter to declare. </param>
		/// <param name="name">The name of the parameter to declare. </param>
		// Token: 0x06003CFD RID: 15613 RVA: 0x000D9E55 File Offset: 0x000D8055
		public CodeParameterDeclarationExpression(string type, string name)
		{
			this.Type = new CodeTypeReference(type);
			this.Name = name;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeParameterDeclarationExpression" /> class using the specified parameter type and name.</summary>
		/// <param name="type">The type of the parameter to declare. </param>
		/// <param name="name">The name of the parameter to declare. </param>
		// Token: 0x06003CFE RID: 15614 RVA: 0x000D9E70 File Offset: 0x000D8070
		public CodeParameterDeclarationExpression(Type type, string name)
		{
			this.Type = new CodeTypeReference(type);
			this.Name = name;
		}

		/// <summary>Gets or sets the custom attributes for the parameter declaration.</summary>
		/// <returns>An object that indicates the custom attributes.</returns>
		// Token: 0x17000ECD RID: 3789
		// (get) Token: 0x06003CFF RID: 15615 RVA: 0x000D9E8C File Offset: 0x000D808C
		// (set) Token: 0x06003D00 RID: 15616 RVA: 0x000D9EB1 File Offset: 0x000D80B1
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

		/// <summary>Gets or sets the direction of the field.</summary>
		/// <returns>An object that indicates the direction of the field.</returns>
		// Token: 0x17000ECE RID: 3790
		// (get) Token: 0x06003D01 RID: 15617 RVA: 0x000D9EBA File Offset: 0x000D80BA
		// (set) Token: 0x06003D02 RID: 15618 RVA: 0x000D9EC2 File Offset: 0x000D80C2
		public FieldDirection Direction { get; set; }

		/// <summary>Gets or sets the type of the parameter.</summary>
		/// <returns>The type of the parameter.</returns>
		// Token: 0x17000ECF RID: 3791
		// (get) Token: 0x06003D03 RID: 15619 RVA: 0x000D9ECC File Offset: 0x000D80CC
		// (set) Token: 0x06003D04 RID: 15620 RVA: 0x000D9EF6 File Offset: 0x000D80F6
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

		/// <summary>Gets or sets the name of the parameter.</summary>
		/// <returns>The name of the parameter.</returns>
		// Token: 0x17000ED0 RID: 3792
		// (get) Token: 0x06003D05 RID: 15621 RVA: 0x000D9EFF File Offset: 0x000D80FF
		// (set) Token: 0x06003D06 RID: 15622 RVA: 0x000D9F10 File Offset: 0x000D8110
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

		// Token: 0x04002DD0 RID: 11728
		private CodeTypeReference _type;

		// Token: 0x04002DD1 RID: 11729
		private string _name;

		// Token: 0x04002DD2 RID: 11730
		private CodeAttributeDeclarationCollection _customAttributes;
	}
}
