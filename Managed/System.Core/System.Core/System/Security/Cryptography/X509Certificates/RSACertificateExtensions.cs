using System;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x0200008A RID: 138
	public static class RSACertificateExtensions
	{
		// Token: 0x0600033E RID: 830 RVA: 0x000084D1 File Offset: 0x000066D1
		public static RSA GetRSAPrivateKey(this X509Certificate2 certificate)
		{
			if (certificate == null)
			{
				throw new ArgumentNullException("certificate");
			}
			return certificate.PrivateKey as RSA;
		}

		// Token: 0x0600033F RID: 831 RVA: 0x000084EC File Offset: 0x000066EC
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
