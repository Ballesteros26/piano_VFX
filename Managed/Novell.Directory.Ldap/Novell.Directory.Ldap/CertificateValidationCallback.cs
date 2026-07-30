using System;
using System.Security.Cryptography.X509Certificates;

namespace Novell.Directory.Ldap
{
	// Token: 0x02000007 RID: 7
	// (Invoke) Token: 0x06000022 RID: 34
	public delegate bool CertificateValidationCallback(X509Certificate certificate, int[] certificateErrors);
}
