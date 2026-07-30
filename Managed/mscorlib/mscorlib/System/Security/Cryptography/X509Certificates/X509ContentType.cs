using System;

namespace System.Security.Cryptography.X509Certificates
{
	/// <summary>Specifies the format of an X.509 certificate. </summary>
	// Token: 0x020006A7 RID: 1703
	public enum X509ContentType
	{
		/// <summary>An unknown X.509 certificate.  </summary>
		// Token: 0x04002616 RID: 9750
		Unknown,
		/// <summary>A single X.509 certificate.</summary>
		// Token: 0x04002617 RID: 9751
		Cert,
		/// <summary>A single serialized X.509 certificate. </summary>
		// Token: 0x04002618 RID: 9752
		SerializedCert,
		/// <summary>A PFX-formatted certificate. The Pfx value is identical to the Pkcs12 value.</summary>
		// Token: 0x04002619 RID: 9753
		Pfx,
		/// <summary>A PKCS #12–formatted certificate. The Pkcs12 value is identical to the Pfx value.</summary>
		// Token: 0x0400261A RID: 9754
		Pkcs12 = 3,
		/// <summary>A serialized store.</summary>
		// Token: 0x0400261B RID: 9755
		SerializedStore,
		/// <summary>A PKCS #7–formatted certificate.</summary>
		// Token: 0x0400261C RID: 9756
		Pkcs7,
		/// <summary>An Authenticode X.509 certificate. </summary>
		// Token: 0x0400261D RID: 9757
		Authenticode
	}
}
