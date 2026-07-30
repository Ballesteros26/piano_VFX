using System;

namespace System.Security.Cryptography
{
	/// <summary>Specifies Cryptography Next Generation (CNG) key property options.</summary>
	// Token: 0x02000074 RID: 116
	[Flags]
	public enum CngPropertyOptions
	{
		/// <summary>The referenced property has no options.</summary>
		// Token: 0x040002D8 RID: 728
		None = 0,
		/// <summary>The property is not specified by CNG. Use this option to avoid future name conflicts with CNG properties.</summary>
		// Token: 0x040002D9 RID: 729
		CustomProperty = 1073741824,
		/// <summary>The property should be persisted.</summary>
		// Token: 0x040002DA RID: 730
		Persist = -2147483648
	}
}
