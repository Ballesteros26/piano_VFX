using System;
using System.Security.Cryptography.X509Certificates;

namespace System.Net.Security
{
	// Token: 0x020005F1 RID: 1521
	// (Invoke) Token: 0x06003060 RID: 12384
	internal delegate bool RemoteCertValidationCallback(string host, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors);
}
