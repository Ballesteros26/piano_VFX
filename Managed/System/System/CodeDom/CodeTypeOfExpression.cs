using System;

namespace System.CodeDom
{
	/// <summary>Represents a typeof expression, an expression that returns a <see cref="T:System.Type" /> for a specified type name.</summary>
	// Token: 0x02000799 RID: 1945
	[Serializable]
	public class CodeTypeOfExpression : CodeExpression
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeTypeOfExpression" /> class.</summary>
		// Token: 0x06003D9D RID: 15773 RVA: 0x000D82AC File Offset: 0x000D64AC
		public CodeTypeOfExpression()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeTypeOfExpression" /> class.</summary>
		/// <param name="type">A <see cref="T:System.CodeDom.CodeTypeReference" /> that indicates the data type for the typeof expression. </param>
		// Token: 0x06003D9E RID: 15774 RVA: 0x000DA968 File Offset: 0x000D8B68
		public CodeTypeOfExpression(CodeTypeReference type)
		{
			this.Type = type;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeTypeOfExpression" /> class using the specified type.</summary>
		/// <param name="type">The name of the data type for the typeof expression. </param>
		// Token: 0x06003D9F RID: 15775 RVA: 0x000DA977 File Offset: 0x000D8B77
		public CodeTypeOfExpression(string type)
		{
			this.Type = new CodeTypeReference(type);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeTypeOfExpression" /> class using the specified type.</summary>
		/// <param name="type">The data type of the data type of the typeof expression. </param>
		// Token: 0x06003DA0 RID: 15776 RVA: 0x000DA98B File Offset: 0x000D8B8B
		public CodeTypeOfExpression(Type type)
		{
			this.Type = new CodeTypeReference(type);
		}

		/// <summary>Gets or sets the data type referenced by the typeof expression.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeTypeReference" /> that indicates the data type referenced by the typeof expression. This property will never return null, and defaults to the <see cref="T:System.Void" /> type.</returns>
		// Token: 0x17000EFA RID: 3834
		// (get) Token: 0x06003DA1 RID: 15777 RVA: 0x000DA9A0 File Offset: 0x000D8BA0
		// (set) Token: 0x06003DA2 RID: 15778 RVA: 0x000DA9CA File Offset: 0x000D8BCA
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

		// Token: 0x04002E00 RID: 11776
		private CodeTypeReference _type;
	}
}
