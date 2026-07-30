using System;

namespace System.Security.Cryptography.X509Certificates
{
	/// <summary>Specifies the mode used to check for X509 certificate revocation.</summary>
	// Token: 0x020003A0 RID: 928
	public enum X509RevocationMode
	{
		/// <summary>No revocation check is performed on the certificate.</summary>
		// Token: 0x0400195F RID: 6495
		NoCheck,
		/// <summary>A revocation check is made using an online certificate revocation list (CRL).</summary>
		// Token: 0x04001960 RID: 6496
		Online,
		/// <summary>A revocation check is made using a cached certificate revocation list (CRL).</summary>
		// Token: 0x04001961 RID: 6497
		Offline
	}
}
