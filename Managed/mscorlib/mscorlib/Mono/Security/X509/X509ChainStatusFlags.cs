using System;

namespace Mono.Security.X509
{
	// Token: 0x02000060 RID: 96
	[Flags]
	[Serializable]
	internal enum X509ChainStatusFlags
	{
		// Token: 0x04000512 RID: 1298
		InvalidBasicConstraints = 1024,
		// Token: 0x04000513 RID: 1299
		NoError = 0,
		// Token: 0x04000514 RID: 1300
		NotSignatureValid = 8,
		// Token: 0x04000515 RID: 1301
		NotTimeNested = 2,
		// Token: 0x04000516 RID: 1302
		NotTimeValid = 1,
		// Token: 0x04000517 RID: 1303
		PartialChain = 65536,
		// Token: 0x04000518 RID: 1304
		UntrustedRoot = 32
	}
}
