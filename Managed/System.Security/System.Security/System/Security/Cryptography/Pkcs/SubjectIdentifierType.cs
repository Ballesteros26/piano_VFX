using System;

namespace System.Security.Cryptography.Pkcs
{
	/// <summary>The <see cref="T:System.Security.Cryptography.Pkcs.SubjectIdentifierType" /> enumeration defines the type of subject identifier.</summary>
	// Token: 0x02000038 RID: 56
	public enum SubjectIdentifierType
	{
		/// <summary>The type of subject identifier is unknown.</summary>
		// Token: 0x04000102 RID: 258
		Unknown,
		/// <summary>The subject is identified by the certificate issuer and serial number.</summary>
		// Token: 0x04000103 RID: 259
		IssuerAndSerialNumber,
		/// <summary>The subject is identified by the hash of the subject's public key. The hash algorithm used is determined by the signature algorithm suite in the subject's certificate.</summary>
		// Token: 0x04000104 RID: 260
		SubjectKeyIdentifier,
		/// <summary>The subject is identified as taking part in an integrity check operation that uses only a hashing algorithm.</summary>
		// Token: 0x04000105 RID: 261
		NoSignature
	}
}
