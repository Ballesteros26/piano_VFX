using System;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x020006AC RID: 1708
	internal interface INativeCertificateHelper
	{
		// Token: 0x060048C6 RID: 18630
		X509CertificateImpl Import(byte[] data, string password, X509KeyStorageFlags flags);

		// Token: 0x060048C7 RID: 18631
		X509CertificateImpl Import(X509Certificate cert);
	}
}
