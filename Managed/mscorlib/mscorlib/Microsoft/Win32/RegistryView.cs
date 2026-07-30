using System;

namespace Microsoft.Win32
{
	/// <summary>Specifies which registry view to target on a 64-bit operating system.</summary>
	// Token: 0x020000B4 RID: 180
	[Serializable]
	public enum RegistryView
	{
		/// <summary>The default view.</summary>
		// Token: 0x04000638 RID: 1592
		Default,
		/// <summary>The 64-bit view.</summary>
		// Token: 0x04000639 RID: 1593
		Registry64 = 256,
		/// <summary>The 32-bit view.</summary>
		// Token: 0x0400063A RID: 1594
		Registry32 = 512
	}
}
