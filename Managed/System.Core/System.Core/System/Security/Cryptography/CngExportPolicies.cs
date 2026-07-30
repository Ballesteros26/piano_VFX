using System;

namespace System.Security.Cryptography
{
	/// <summary>Specifies the key export policies for a key. </summary>
	// Token: 0x0200006F RID: 111
	[Flags]
	public enum CngExportPolicies
	{
		/// <summary>No export policies are established. Key export is allowed without restriction.</summary>
		// Token: 0x040002C0 RID: 704
		None = 0,
		/// <summary>The private key can be exported multiple times.</summary>
		// Token: 0x040002C1 RID: 705
		AllowExport = 1,
		/// <summary>The private key can be exported multiple times as plaintext.</summary>
		// Token: 0x040002C2 RID: 706
		AllowPlaintextExport = 2,
		/// <summary>The private key can be exported one time for archiving purposes.</summary>
		// Token: 0x040002C3 RID: 707
		AllowArchiving = 4,
		/// <summary>The private key can be exported one time as plaintext.</summary>
		// Token: 0x040002C4 RID: 708
		AllowPlaintextArchiving = 8
	}
}
