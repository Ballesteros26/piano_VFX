using System;

namespace System.Security.Cryptography
{
	/// <summary>Specifies options for opening key handles.</summary>
	// Token: 0x02000062 RID: 98
	[Flags]
	public enum CngKeyHandleOpenOptions
	{
		/// <summary>The key handle being opened does not specify an ephemeral key.</summary>
		// Token: 0x0400029D RID: 669
		None = 0,
		/// <summary>The key handle being opened specifies an ephemeral key.</summary>
		// Token: 0x0400029E RID: 670
		EphemeralKey = 1
	}
}
