using System;
using System.Security.Cryptography.X509Certificates;
using Mono.Security.Cryptography;
using Mono.Security.X509;

namespace Mono.Security.Protocol.Tls
{
	// Token: 0x0200004F RID: 79
	internal sealed class TlsClientSettings
	{
		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x0600035D RID: 861 RVA: 0x00012C96 File Offset: 0x00010E96
		// (set) Token: 0x0600035E RID: 862 RVA: 0x00012C9E File Offset: 0x00010E9E
		public string TargetHost
		{
			get
			{
				return this.targetHost;
			}
			set
			{
				this.targetHost = value;
			}
		}

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x0600035F RID: 863 RVA: 0x00012CA7 File Offset: 0x00010EA7
		// (set) Token: 0x06000360 RID: 864 RVA: 0x00012CAF File Offset: 0x00010EAF
		public global::System.Security.Cryptography.X509Certificates.X509CertificateCollection Certificates
		{
			get
			{
				return this.certificates;
			}
			set
			{
				this.certificates = value;
			}
		}

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x06000361 RID: 865 RVA: 0x00012CB8 File Offset: 0x00010EB8
		// (set) Token: 0x06000362 RID: 866 RVA: 0x00012CC0 File Offset: 0x00010EC0
		public global::System.Security.Cryptography.X509Certificates.X509Certificate ClientCertificate
		{
			get
			{
				return this.clientCertificate;
			}
			set
			{
				this.clientCertificate = value;
				this.UpdateCertificateRSA();
			}
		}

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x06000363 RID: 867 RVA: 0x00012CCF File Offset: 0x00010ECF
		public RSAManaged CertificateRSA
		{
			get
			{
				return this.certificateRSA;
			}
		}

		// Token: 0x06000364 RID: 868 RVA: 0x00012CD7 File Offset: 0x00010ED7
		public TlsClientSettings()
		{
			this.certificates = new global::System.Security.Cryptography.X509Certificates.X509CertificateCollection();
			this.targetHost = string.Empty;
		}

		// Token: 0x06000365 RID: 869 RVA: 0x00012CF8 File Offset: 0x00010EF8
		public void UpdateCertificateRSA()
		{
			if (this.clientCertificate == null)
			{
				this.certificateRSA = null;
				return;
			}
			Mono.Security.X509.X509Certificate x509Certificate = new Mono.Security.X509.X509Certificate(this.clientCertificate.GetRawCertData());
			this.certificateRSA = new RSAManaged(x509Certificate.RSA.KeySize);
			this.certificateRSA.ImportParameters(x509Certificate.RSA.ExportParameters(false));
		}

		// Token: 0x040001B3 RID: 435
		private string targetHost;

		// Token: 0x040001B4 RID: 436
		private global::System.Security.Cryptography.X509Certificates.X509CertificateCollection certificates;

		// Token: 0x040001B5 RID: 437
		private global::System.Security.Cryptography.X509Certificates.X509Certificate clientCertificate;

		// Token: 0x040001B6 RID: 438
		private RSAManaged certificateRSA;
	}
}
