using System;

namespace System.Security.Cryptography.X509Certificates
{
	/// <summary>Defines how the certificate key can be used. If this value is not defined, the key can be used for any purpose.</summary>
	// Token: 0x0200039D RID: 925
	[Flags]
	public enum X509KeyUsageFlags
	{
		/// <summary>No key usage parameters.</summary>
		// Token: 0x04001949 RID: 6473
		None = 0,
		/// <summary>The key can be used for encryption only.</summary>
		// Token: 0x0400194A RID: 6474
		EncipherOnly = 1,
		/// <summary>The key can be used to sign a certificate revocation list (CRL).</summary>
		// Token: 0x0400194B RID: 6475
		CrlSign = 2,
		/// <summary>The key can be used to sign certificates.</summary>
		// Token: 0x0400194C RID: 6476
		KeyCertSign = 4,
		/// <summary>The key can be used to determine key agreement, such as a key created using the Diffie-Hellman key agreement algorithm.</summary>
		// Token: 0x0400194D RID: 6477
		KeyAgreement = 8,
		/// <summary>The key can be used for data encryption.</summary>
		// Token: 0x0400194E RID: 6478
		DataEncipherment = 16,
		/// <summary>The key can be used for key encryption.</summary>
		// Token: 0x0400194F RID: 6479
		KeyEncipherment = 32,
		/// <summary>The key can be used for authentication.</summary>
		// Token: 0x04001950 RID: 6480
		NonRepudiation = 64,
		/// <summary>The key can be used as a digital signature.</summary>
		// Token: 0x04001951 RID: 6481
		DigitalSignature = 128,
		/// <summary>The key can be used for decryption only.</summary>
		// Token: 0x04001952 RID: 6482
		DecipherOnly = 32768
	}
}
