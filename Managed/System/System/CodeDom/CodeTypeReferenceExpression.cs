using System;

namespace System.CodeDom
{
	/// <summary>Represents a reference to a data type.</summary>
	// Token: 0x0200079C RID: 1948
	[Serializable]
	public class CodeTypeReferenceExpression : CodeExpression
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeTypeReferenceExpression" /> class.</summary>
		// Token: 0x06003DB9 RID: 15801 RVA: 0x000D82AC File Offset: 0x000D64AC
		public CodeTypeReferenceExpression()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeTypeReferenceExpression" /> class using the specified type.</summary>
		/// <param name="type">A <see cref="T:System.CodeDom.CodeTypeReference" /> that indicates the data type to reference. </param>
		// Token: 0x06003DBA RID: 15802 RVA: 0x000DAB0C File Offset: 0x000D8D0C
		public CodeTypeReferenceExpression(CodeTypeReference type)
		{
			this.Type = type;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeTypeReferenceExpression" /> class using the specified data type name.</summary>
		/// <param name="type">The name of the data type to reference. </param>
		// Token: 0x06003DBB RID: 15803 RVA: 0x000DAB1B File Offset: 0x000D8D1B
		public CodeTypeReferenceExpression(string type)
		{
			this.Type = new CodeTypeReference(type);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeTypeReferenceExpression" /> class using the specified data type.</summary>
		/// <param name="type">An instance of the data type to reference. </param>
		// Token: 0x06003DBC RID: 15804 RVA: 0x000DAB2F File Offset: 0x000D8D2F
		public CodeTypeReferenceExpression(Type type)
		{
			this.Type = new CodeTypeReference(type);
		}

		/// <summary>Gets or sets the data type to reference.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeTypeReference" /> that indicates the data type to reference.</returns>
		// Token: 0x17000F00 RID: 3840
		// (get) Token: 0x06003DBD RID: 15805 RVA: 0x000DAB44 File Offset: 0x000D8D44
		// (set) Token: 0x06003DBE RID: 15806 RVA: 0x000DAB6E File Offset: 0x000D8D6E
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

		// Token: 0x04002E05 RID: 11781
		private CodeTypeReference _type;
	}
}
