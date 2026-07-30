using System;
using System.Security.Cryptography.X509Certificates;
using Mono.Security.Interface;
using Mono.Security.Protocol.Tls.Handshake;
using Mono.Security.X509;

namespace Mono.Security.Protocol.Tls
{
	// Token: 0x02000043 RID: 67
	internal class ServerContext : Context
	{
		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x060002BA RID: 698 RVA: 0x0000FCD6 File Offset: 0x0000DED6
		public SslServerStream SslStream
		{
			get
			{
				return this.sslStream;
			}
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x060002BB RID: 699 RVA: 0x0000FCDE File Offset: 0x0000DEDE
		public bool ClientCertificateRequired
		{
			get
			{
				return this.clientCertificateRequired;
			}
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x060002BC RID: 700 RVA: 0x0000FCE6 File Offset: 0x0000DEE6
		public bool RequestClientCertificate
		{
			get
			{
				return this.request_client_certificate;
			}
		}

		// Token: 0x060002BD RID: 701 RVA: 0x0000FCF0 File Offset: 0x0000DEF0
		public ServerContext(SslServerStream stream, SecurityProtocolType securityProtocolType, global::System.Security.Cryptography.X509Certificates.X509Certificate serverCertificate, bool clientCertificateRequired, bool requestClientCertificate)
			: base(securityProtocolType)
		{
			this.sslStream = stream;
			this.clientCertificateRequired = clientCertificateRequired;
			this.request_client_certificate = requestClientCertificate;
			Mono.Security.X509.X509Certificate x509Certificate = new Mono.Security.X509.X509Certificate(serverCertificate.GetRawCertData());
			base.ServerSettings.Certificates = new Mono.Security.X509.X509CertificateCollection();
			base.ServerSettings.Certificates.Add(x509Certificate);
			base.ServerSettings.UpdateCertificateRSA();
			if (CertificateValidationHelper.SupportsX509Chain)
			{
				Mono.Security.X509.X509Chain x509Chain = new Mono.Security.X509.X509Chain(X509StoreManager.IntermediateCACertificates);
				if (x509Chain.Build(x509Certificate))
				{
					for (int i = x509Chain.Chain.Count - 1; i > 0; i--)
					{
						base.ServerSettings.Certificates.Add(x509Chain.Chain[i]);
					}
				}
			}
			base.ServerSettings.CertificateTypes = new ClientCertificateType[base.ServerSettings.Certificates.Count];
			for (int j = 0; j < base.ServerSettings.CertificateTypes.Length; j++)
			{
				base.ServerSettings.CertificateTypes[j] = ClientCertificateType.RSA;
			}
			if (CertificateValidationHelper.SupportsX509Chain)
			{
				Mono.Security.X509.X509CertificateCollection trustedRootCertificates = X509StoreManager.TrustedRootCertificates;
				string[] array = new string[trustedRootCertificates.Count];
				int num = 0;
				foreach (Mono.Security.X509.X509Certificate x509Certificate2 in trustedRootCertificates)
				{
					array[num++] = x509Certificate2.IssuerName;
				}
				base.ServerSettings.DistinguisedNames = array;
			}
		}

		// Token: 0x0400018B RID: 395
		private SslServerStream sslStream;

		// Token: 0x0400018C RID: 396
		private bool request_client_certificate;

		// Token: 0x0400018D RID: 397
		private bool clientCertificateRequired;
	}
}
