using System;
using System.Security.Cryptography.X509Certificates;

namespace Mono.Security.Interface
{
	// Token: 0x02000085 RID: 133
	// (Invoke) Token: 0x060004C2 RID: 1218
	public delegate X509Certificate MonoLocalCertificateSelectionCallback(string targetHost, X509CertificateCollection localCertificates, X509Certificate remoteCertificate, string[] acceptableIssuers);
}
