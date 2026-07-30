using System;

namespace System.Net.Security
{
	/// <summary>Enumerates Secure Socket Layer (SSL) policy errors.</summary>
	// Token: 0x020005F0 RID: 1520
	[Flags]
	public enum SslPolicyErrors
	{
		/// <summary>No SSL policy errors.</summary>
		// Token: 0x04002799 RID: 10137
		None = 0,
		/// <summary>Certificate not available.</summary>
		// Token: 0x0400279A RID: 10138
		RemoteCertificateNotAvailable = 1,
		/// <summary>Certificate name mismatch.</summary>
		// Token: 0x0400279B RID: 10139
		RemoteCertificateNameMismatch = 2,
		/// <summary>
		///   <see cref="P:System.Security.Cryptography.X509Certificates.X509Chain.ChainStatus" /> has returned a non empty array.</summary>
		// Token: 0x0400279C RID: 10140
		RemoteCertificateChainErrors = 4
	}
}
