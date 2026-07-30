using System;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace Mono.Security.Protocol.Tls
{
	// Token: 0x02000049 RID: 73
	// (Invoke) Token: 0x060002D6 RID: 726
	public delegate AsymmetricAlgorithm PrivateKeySelectionCallback(X509Certificate certificate, string targetHost);
}
