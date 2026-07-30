using System;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x02000039 RID: 57
	internal static class RSACertificateExtensions
	{
		// Token: 0x0600013B RID: 315 RVA: 0x00004B32 File Offset: 0x00002D32
		public static RSA GetRSAPrivateKey(this X509Certificate2 certificate)
		{
			if (certificate == null)
			{
				throw new ArgumentNullException("certificate");
			}
			return certificate.PrivateKey as RSA;
		}

		// Token: 0x0600013C RID: 316 RVA: 0x00004B4D File Offset: 0x00002D4D
		public static RSA GetRSAPublicKey(this X509Certificate2 certificate)
		{
			if (certificate == null)
			{
				throw new ArgumentNullException("certificate");
			}
			return certificate.PublicKey.Key as RSA;
		}
	}
}
