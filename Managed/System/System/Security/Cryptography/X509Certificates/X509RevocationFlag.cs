using System;

namespace System.Security.Cryptography.X509Certificates
{
	/// <summary>Specifies which X509 certificates in the chain should be checked for revocation.</summary>
	// Token: 0x0200039F RID: 927
	public enum X509RevocationFlag
	{
		/// <summary>Only the end certificate is checked for revocation.</summary>
		// Token: 0x0400195B RID: 6491
		EndCertificateOnly,
		/// <summary>The entire chain of certificates is checked for revocation.</summary>
		// Token: 0x0400195C RID: 6492
		EntireChain,
		/// <summary>The entire chain, except the root certificate, is checked for revocation.</summary>
		// Token: 0x0400195D RID: 6493
		ExcludeRoot
	}
}
