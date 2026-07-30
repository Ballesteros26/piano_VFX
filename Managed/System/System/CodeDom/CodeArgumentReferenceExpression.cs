using System;

namespace System.CodeDom
{
	/// <summary>Represents a reference to the value of an argument passed to a method.</summary>
	// Token: 0x02000750 RID: 1872
	[Serializable]
	public class CodeArgumentReferenceExpression : CodeExpression
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeArgumentReferenceExpression" /> class.</summary>
		// Token: 0x06003B78 RID: 15224 RVA: 0x000D82AC File Offset: 0x000D64AC
		public CodeArgumentReferenceExpression()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeArgumentReferenceExpression" /> class using the specified parameter name.</summary>
		/// <param name="parameterName">The name of the parameter to reference. </param>
		// Token: 0x06003B79 RID: 15225 RVA: 0x000D82B4 File Offset: 0x000D64B4
		public CodeArgumentReferenceExpression(string parameterName)
		{
			this._parameterName = parameterName;
		}

		/// <summary>Gets or sets the name of the parameter this expression references.</summary>
		/// <returns>The name of the parameter to reference.</returns>
		// Token: 0x17000E5F RID: 3679
		// (get) Token: 0x06003B7A RID: 15226 RVA: 0x000D82C3 File Offset: 0x000D64C3
		// (set) Token: 0x06003B7B RID: 15227 RVA: 0x000D82D4 File Offset: 0x000D64D4
		public string ParameterName
		{
			get
			{
				return this._parameterName ?? string.Empty;
			}
			set
			{
				this._parameterName = value;
			}
		}

		// Token: 0x04002D4F RID: 11599
		private string _parameterName;
	}
}
