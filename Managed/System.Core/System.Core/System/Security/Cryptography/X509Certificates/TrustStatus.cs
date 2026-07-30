using System;

namespace System.Security.Cryptography.X509Certificates
{
	/// <summary>Identifies the level of trustworthiness that is assigned to the signature for a manifest.</summary>
	// Token: 0x02000088 RID: 136
	public enum TrustStatus
	{
		/// <summary>The signature was created by an explicitly distrusted publisher.</summary>
		// Token: 0x04000315 RID: 789
		Untrusted,
		/// <summary>The identity is not known and the signature is invalid. Because there is no verified signature, an identity cannot be determined.</summary>
		// Token: 0x04000316 RID: 790
		UnknownIdentity,
		/// <summary>The identity is known and the signature is valid. A valid Authenticode signature provides an identity.</summary>
		// Token: 0x04000317 RID: 791
		KnownIdentity,
		/// <summary>The signature is valid and was created by an explicitly trusted publisher.</summary>
		// Token: 0x04000318 RID: 792
		Trusted
	}
}
