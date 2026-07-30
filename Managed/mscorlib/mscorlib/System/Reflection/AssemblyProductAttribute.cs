using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	/// <summary>Defines a product name custom attribute for an assembly manifest.</summary>
	// Token: 0x020002C3 RID: 707
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
	[ComVisible(true)]
	public sealed class AssemblyProductAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.AssemblyProductAttribute" /> class.</summary>
		/// <param name="product">The product name information. </param>
		// Token: 0x06002023 RID: 8227 RVA: 0x0007DE10 File Offset: 0x0007C010
		public AssemblyProductAttribute(string product)
		{
			this.m_product = product;
		}

		/// <summary>Gets product name information.</summary>
		/// <returns>A string containing the product name.</returns>
		// Token: 0x1700046E RID: 1134
		// (get) Token: 0x06002024 RID: 8228 RVA: 0x0007DE1F File Offset: 0x0007C01F
		public string Product
		{
			get
			{
				return this.m_product;
			}
		}

		// Token: 0x04001163 RID: 4451
		private string m_product;
	}
}
