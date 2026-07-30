using System;
using System.Security.Cryptography.X509Certificates;

namespace Mono.Security.Interface
{
	// Token: 0x02000084 RID: 132
	// (Invoke) Token: 0x060004BE RID: 1214
	public delegate bool MonoRemoteCertificateValidationCallback(string targetHost, X509Certificate certificate, X509Chain chain, MonoSslPolicyErrors sslPolicyErrors);
}
