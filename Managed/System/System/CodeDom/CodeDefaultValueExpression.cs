using System;

namespace System.CodeDom
{
	/// <summary>Represents a reference to a default value.</summary>
	// Token: 0x02000766 RID: 1894
	[Serializable]
	public class CodeDefaultValueExpression : CodeExpression
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeDefaultValueExpression" /> class. </summary>
		// Token: 0x06003C22 RID: 15394 RVA: 0x000D82AC File Offset: 0x000D64AC
		public CodeDefaultValueExpression()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeDefaultValueExpression" /> class using the specified code type reference.</summary>
		/// <param name="type">A <see cref="T:System.CodeDom.CodeTypeReference" /> that specifies the reference to a value type.</param>
		// Token: 0x06003C23 RID: 15395 RVA: 0x000D8D8B File Offset: 0x000D6F8B
		public CodeDefaultValueExpression(CodeTypeReference type)
		{
			this._type = type;
		}

		/// <summary>Gets or sets the data type reference for a default value.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeTypeReference" /> object representing a data type that has a default value.</returns>
		// Token: 0x17000E8B RID: 3723
		// (get) Token: 0x06003C24 RID: 15396 RVA: 0x000D8D9C File Offset: 0x000D6F9C
		// (set) Token: 0x06003C25 RID: 15397 RVA: 0x000D8DC6 File Offset: 0x000D6FC6
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

		// Token: 0x04002D89 RID: 11657
		private CodeTypeReference _type;
	}
}
