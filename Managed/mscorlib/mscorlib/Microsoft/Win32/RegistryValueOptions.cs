using System;

namespace Microsoft.Win32
{
	/// <summary>Specifies optional behavior when retrieving name/value pairs from a registry key.</summary>
	// Token: 0x020000B3 RID: 179
	[Flags]
	public enum RegistryValueOptions
	{
		/// <summary>No optional behavior is specified.</summary>
		// Token: 0x04000635 RID: 1589
		None = 0,
		/// <summary>A value of type <see cref="F:Microsoft.Win32.RegistryValueKind.ExpandString" /> is retrieved without expanding its embedded environment variables. </summary>
		// Token: 0x04000636 RID: 1590
		DoNotExpandEnvironmentNames = 1
	}
}
