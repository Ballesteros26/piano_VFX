using System;
using System.Security.Cryptography.X509Certificates;

namespace System.Net.Security
{
	// Token: 0x020005F2 RID: 1522
	// (Invoke) Token: 0x06003064 RID: 12388
	internal delegate X509Certificate LocalCertSelectionCallback(string targetHost, X509CertificateCollection localCertificates, X509Certificate remoteCertificate, string[] acceptableIssuers);
}
