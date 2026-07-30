using System;
using System.Security.Cryptography.X509Certificates;

namespace Mono.Security.Protocol.Tls
{
	// Token: 0x02000046 RID: 70
	// (Invoke) Token: 0x060002CA RID: 714
	public delegate bool CertificateValidationCallback(X509Certificate certificate, int[] certificateErrors);
}
