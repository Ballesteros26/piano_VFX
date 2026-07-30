using System;
using System.Security.Cryptography.X509Certificates;

namespace Mono.Security.Interface
{
	// Token: 0x02000079 RID: 121
	public interface ICertificateValidator
	{
		// Token: 0x17000119 RID: 281
		// (get) Token: 0x0600046F RID: 1135
		MonoTlsSettings Settings { get; }

		// Token: 0x06000470 RID: 1136
		bool SelectClientCertificate(string targetHost, X509CertificateCollection localCertificates, X509Certificate remoteCertificate, string[] acceptableIssuers, out X509Certificate clientCertificate);

		// Token: 0x06000471 RID: 1137
		ValidationResult ValidateCertificate(string targetHost, bool serverMode, X509CertificateCollection certificates);
	}
}
