using System;

namespace Microsoft.Win32
{
	/// <summary>Specifies options to use when creating a registry key.</summary>
	// Token: 0x020000B1 RID: 177
	[Flags]
	[Serializable]
	public enum RegistryOptions
	{
		/// <summary>A non-volatile key. This is the default.</summary>
		// Token: 0x04000629 RID: 1577
		None = 0,
		/// <summary>A volatile key. The information is stored in memory and is not preserved when the corresponding registry hive is unloaded.</summary>
		// Token: 0x0400062A RID: 1578
		Volatile = 1
	}
}
