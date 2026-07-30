using System;

namespace System.Security.Cryptography
{
	/// <summary>Specifies the cryptographic operations that a Cryptography Next Generation (CNG) key may be used with. </summary>
	// Token: 0x02000073 RID: 115
	[Flags]
	public enum CngKeyUsages
	{
		/// <summary>No usage values are assigned to the key.</summary>
		// Token: 0x040002D2 RID: 722
		None = 0,
		/// <summary>The key can be used for encryption and decryption.</summary>
		// Token: 0x040002D3 RID: 723
		Decryption = 1,
		/// <summary>The key can be used for signing and verification.</summary>
		// Token: 0x040002D4 RID: 724
		Signing = 2,
		/// <summary>The key can be used for secret agreement generation and key exchange.</summary>
		// Token: 0x040002D5 RID: 725
		KeyAgreement = 4,
		/// <summary>The key can be used for all purposes.</summary>
		// Token: 0x040002D6 RID: 726
		AllUsages = 16777215
	}
}
