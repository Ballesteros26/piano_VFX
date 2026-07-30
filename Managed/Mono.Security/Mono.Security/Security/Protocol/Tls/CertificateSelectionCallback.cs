using System;
using System.Security.Cryptography.X509Certificates;

namespace Mono.Security.Protocol.Tls
{
	// Token: 0x02000048 RID: 72
	// (Invoke) Token: 0x060002D2 RID: 722
	public delegate X509Certificate CertificateSelectionCallback(X509CertificateCollection clientCertificates, X509Certificate serverCertificate, string targetHost, X509CertificateCollection serverRequestedCertificates);
}
