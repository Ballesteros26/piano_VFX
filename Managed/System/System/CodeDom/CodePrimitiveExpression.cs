using System;

namespace System.CodeDom
{
	/// <summary>Represents a primitive data type value.</summary>
	// Token: 0x02000784 RID: 1924
	[Serializable]
	public class CodePrimitiveExpression : CodeExpression
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodePrimitiveExpression" /> class.</summary>
		// Token: 0x06003D14 RID: 15636 RVA: 0x000D82AC File Offset: 0x000D64AC
		public CodePrimitiveExpression()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodePrimitiveExpression" /> class using the specified object.</summary>
		/// <param name="value">The object to represent. </param>
		// Token: 0x06003D15 RID: 15637 RVA: 0x000D9FBC File Offset: 0x000D81BC
		public CodePrimitiveExpression(object value)
		{
			this.Value = value;
		}

		/// <summary>Gets or sets the primitive data type to represent.</summary>
		/// <returns>The primitive data type instance to represent the value of.</returns>
		// Token: 0x17000ED2 RID: 3794
		// (get) Token: 0x06003D16 RID: 15638 RVA: 0x000D9FCB File Offset: 0x000D81CB
		// (set) Token: 0x06003D17 RID: 15639 RVA: 0x000D9FD3 File Offset: 0x000D81D3
		public object Value { get; set; }
	}
}
