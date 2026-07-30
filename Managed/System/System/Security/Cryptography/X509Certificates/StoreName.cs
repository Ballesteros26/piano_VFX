using System;

namespace System.Security.Cryptography.X509Certificates
{
	/// <summary>Specifies the name of the X.509 certificate store to open.</summary>
	// Token: 0x02000398 RID: 920
	public enum StoreName
	{
		/// <summary>The X.509 certificate store for other users.</summary>
		// Token: 0x04001905 RID: 6405
		AddressBook = 1,
		/// <summary>The X.509 certificate store for third-party certificate authorities (CAs).</summary>
		// Token: 0x04001906 RID: 6406
		AuthRoot,
		/// <summary>The X.509 certificate store for intermediate certificate authorities (CAs). </summary>
		// Token: 0x04001907 RID: 6407
		CertificateAuthority,
		/// <summary>The X.509 certificate store for revoked certificates.</summary>
		// Token: 0x04001908 RID: 6408
		Disallowed,
		/// <summary>The X.509 certificate store for personal certificates.</summary>
		// Token: 0x04001909 RID: 6409
		My,
		/// <summary>The X.509 certificate store for trusted root certificate authorities (CAs).</summary>
		// Token: 0x0400190A RID: 6410
		Root,
		/// <summary>The X.509 certificate store for directly trusted people and resources.</summary>
		// Token: 0x0400190B RID: 6411
		TrustedPeople,
		/// <summary>The X.509 certificate store for directly trusted publishers.</summary>
		// Token: 0x0400190C RID: 6412
		TrustedPublisher
	}
}
