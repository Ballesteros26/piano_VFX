using System;

namespace System.Reflection
{
	/// <summary>Defines a key/value metadata pair for the decorated assembly.</summary>
	// Token: 0x020002D1 RID: 721
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true, Inherited = false)]
	public sealed class AssemblyMetadataAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.AssemblyMetadataAttribute" /> class by using the specified metadata key and value.</summary>
		/// <param name="key">The metadata key.</param>
		/// <param name="value">The metadata value.</param>
		// Token: 0x06002043 RID: 8259 RVA: 0x0007DF60 File Offset: 0x0007C160
		public AssemblyMetadataAttribute(string key, string value)
		{
			this.m_key = key;
			this.m_value = value;
		}

		/// <summary>Gets the metadata key.</summary>
		/// <returns>The metadata key.</returns>
		// Token: 0x1700047D RID: 1149
		// (get) Token: 0x06002044 RID: 8260 RVA: 0x0007DF76 File Offset: 0x0007C176
		public string Key
		{
			get
			{
				return this.m_key;
			}
		}

		/// <summary>Gets the metadata value.</summary>
		/// <returns>The metadata value.</returns>
		// Token: 0x1700047E RID: 1150
		// (get) Token: 0x06002045 RID: 8261 RVA: 0x0007DF7E File Offset: 0x0007C17E
		public string Value
		{
			get
			{
				return this.m_value;
			}
		}

		// Token: 0x04001171 RID: 4465
		private string m_key;

		// Token: 0x04001172 RID: 4466
		private string m_value;
	}
}
