using System;

namespace System.CodeDom
{
	/// <summary>Represents a reference to a local variable.</summary>
	// Token: 0x0200079E RID: 1950
	[Serializable]
	public class CodeVariableReferenceExpression : CodeExpression
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeVariableReferenceExpression" /> class.</summary>
		// Token: 0x06003DCC RID: 15820 RVA: 0x000D82AC File Offset: 0x000D64AC
		public CodeVariableReferenceExpression()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeVariableReferenceExpression" /> class using the specified local variable name.</summary>
		/// <param name="variableName">The name of the local variable to reference. </param>
		// Token: 0x06003DCD RID: 15821 RVA: 0x000DAC83 File Offset: 0x000D8E83
		public CodeVariableReferenceExpression(string variableName)
		{
			this._variableName = variableName;
		}

		/// <summary>Gets or sets the name of the local variable to reference.</summary>
		/// <returns>The name of the local variable to reference.</returns>
		// Token: 0x17000F04 RID: 3844
		// (get) Token: 0x06003DCE RID: 15822 RVA: 0x000DAC92 File Offset: 0x000D8E92
		// (set) Token: 0x06003DCF RID: 15823 RVA: 0x000DACA3 File Offset: 0x000D8EA3
		public string VariableName
		{
			get
			{
				return this._variableName ?? string.Empty;
			}
			set
			{
				this._variableName = value;
			}
		}

		// Token: 0x04002E09 RID: 11785
		private string _variableName;
	}
}
