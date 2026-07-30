using System;

namespace Mono.Security.X509
{
	// Token: 0x02000017 RID: 23
	[Flags]
	[Serializable]
	public enum X509ChainStatusFlags
	{
		// Token: 0x040000B5 RID: 181
		InvalidBasicConstraints = 1024,
		// Token: 0x040000B6 RID: 182
		NoError = 0,
		// Token: 0x040000B7 RID: 183
		NotSignatureValid = 8,
		// Token: 0x040000B8 RID: 184
		NotTimeNested = 2,
		// Token: 0x040000B9 RID: 185
		NotTimeValid = 1,
		// Token: 0x040000BA RID: 186
		PartialChain = 65536,
		// Token: 0x040000BB RID: 187
		UntrustedRoot = 32
	}
}
