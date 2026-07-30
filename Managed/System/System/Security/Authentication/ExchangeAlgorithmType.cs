using System;

namespace System.Security.Authentication
{
	/// <summary>Specifies the algorithm used to create keys shared by the client and server.</summary>
	// Token: 0x0200037C RID: 892
	public enum ExchangeAlgorithmType
	{
		/// <summary>No key exchange algorithm is used.</summary>
		// Token: 0x040018B0 RID: 6320
		None,
		/// <summary>The Diffie Hellman ephemeral key exchange algorithm.</summary>
		// Token: 0x040018B1 RID: 6321
		DiffieHellman = 43522,
		/// <summary>The RSA public-key exchange algorithm.</summary>
		// Token: 0x040018B2 RID: 6322
		RsaKeyX = 41984,
		/// <summary>The RSA public-key signature algorithm.</summary>
		// Token: 0x040018B3 RID: 6323
		RsaSign = 9216
	}
}
