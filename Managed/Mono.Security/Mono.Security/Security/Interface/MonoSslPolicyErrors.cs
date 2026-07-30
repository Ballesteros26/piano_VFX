using System;

namespace Mono.Security.Interface
{
	// Token: 0x02000082 RID: 130
	[Flags]
	public enum MonoSslPolicyErrors
	{
		// Token: 0x04000369 RID: 873
		None = 0,
		// Token: 0x0400036A RID: 874
		RemoteCertificateNotAvailable = 1,
		// Token: 0x0400036B RID: 875
		RemoteCertificateNameMismatch = 2,
		// Token: 0x0400036C RID: 876
		RemoteCertificateChainErrors = 4
	}
}
