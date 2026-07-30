using System;

namespace System.Security.Cryptography.Pkcs
{
	/// <summary>The <see cref="T:System.Security.Cryptography.Pkcs.SubjectIdentifierOrKeyType" /> enumeration defines how a subject is identified.</summary>
	// Token: 0x02000037 RID: 55
	public enum SubjectIdentifierOrKeyType
	{
		/// <summary>The type is unknown.</summary>
		// Token: 0x040000FD RID: 253
		Unknown,
		/// <summary>The subject is identified by the certificate issuer and serial number.</summary>
		// Token: 0x040000FE RID: 254
		IssuerAndSerialNumber,
		/// <summary>The subject is identified by the hash of the subject key.</summary>
		// Token: 0x040000FF RID: 255
		SubjectKeyIdentifier,
		/// <summary>The subject is identified by the public key.</summary>
		// Token: 0x04000100 RID: 256
		PublicKeyInfo
	}
}
