using System;

namespace System.Security.Authentication
{
	/// <summary>Defines the possible cipher algorithms for the <see cref="T:System.Net.Security.SslStream" /> class.</summary>
	// Token: 0x0200037B RID: 891
	public enum CipherAlgorithmType
	{
		/// <summary>No encryption algorithm is used.</summary>
		// Token: 0x040018A5 RID: 6309
		None,
		/// <summary>No encryption is used with a Null cipher algorithm. </summary>
		// Token: 0x040018A6 RID: 6310
		Null = 24576,
		/// <summary>The Advanced Encryption Standard (AES) algorithm.</summary>
		// Token: 0x040018A7 RID: 6311
		Aes = 26129,
		/// <summary>The Advanced Encryption Standard (AES) algorithm with a 128 bit key.</summary>
		// Token: 0x040018A8 RID: 6312
		Aes128 = 26126,
		/// <summary>The Advanced Encryption Standard (AES) algorithm with a 192 bit key.</summary>
		// Token: 0x040018A9 RID: 6313
		Aes192,
		/// <summary>The Advanced Encryption Standard (AES) algorithm with a 256 bit key.</summary>
		// Token: 0x040018AA RID: 6314
		Aes256,
		/// <summary>The Data Encryption Standard (DES) algorithm.</summary>
		// Token: 0x040018AB RID: 6315
		Des = 26113,
		/// <summary>Rivest's Code 2 (RC2) algorithm.</summary>
		// Token: 0x040018AC RID: 6316
		Rc2,
		/// <summary>Rivest's Code 4 (RC4) algorithm.</summary>
		// Token: 0x040018AD RID: 6317
		Rc4 = 26625,
		/// <summary>The Triple Data Encryption Standard (3DES) algorithm.</summary>
		// Token: 0x040018AE RID: 6318
		TripleDes = 26115
	}
}
