using System;

namespace System.CodeDom
{
	/// <summary>Represents an argument used in a metadata attribute declaration.</summary>
	// Token: 0x02000755 RID: 1877
	[Serializable]
	public class CodeAttributeArgument
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeAttributeArgument" /> class.</summary>
		// Token: 0x06003B9F RID: 15263 RVA: 0x000020EB File Offset: 0x000002EB
		public CodeAttributeArgument()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeAttributeArgument" /> class using the specified value.</summary>
		/// <param name="value">A <see cref="T:System.CodeDom.CodeExpression" /> that represents the value of the argument. </param>
		// Token: 0x06003BA0 RID: 15264 RVA: 0x000D859F File Offset: 0x000D679F
		public CodeAttributeArgument(CodeExpression value)
		{
			this.Value = value;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeAttributeArgument" /> class using the specified name and value.</summary>
		/// <param name="name">The name of the attribute property the argument applies to. </param>
		/// <param name="value">A <see cref="T:System.CodeDom.CodeExpression" /> that represents the value of the argument. </param>
		// Token: 0x06003BA1 RID: 15265 RVA: 0x000D85AE File Offset: 0x000D67AE
		public CodeAttributeArgument(string name, CodeExpression value)
		{
			this.Name = name;
			this.Value = value;
		}

		/// <summary>Gets or sets the name of the attribute.</summary>
		/// <returns>The name of the attribute property the argument is for.</returns>
		// Token: 0x17000E6A RID: 3690
		// (get) Token: 0x06003BA2 RID: 15266 RVA: 0x000D85C4 File Offset: 0x000D67C4
		// (set) Token: 0x06003BA3 RID: 15267 RVA: 0x000D85D5 File Offset: 0x000D67D5
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

		/// <summary>Gets or sets the value for the attribute argument.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the value for the attribute argument.</returns>
		// Token: 0x17000E6B RID: 3691
		// (get) Token: 0x06003BA4 RID: 15268 RVA: 0x000D85DE File Offset: 0x000D67DE
		// (set) Token: 0x06003BA5 RID: 15269 RVA: 0x000D85E6 File Offset: 0x000D67E6
		public CodeExpression Value { get; set; }

		// Token: 0x04002D5A RID: 11610
		private string _name;
	}
}
