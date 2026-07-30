using System;

namespace Mono.Security.Protocol.Tls
{
	// Token: 0x02000042 RID: 66
	[Flags]
	[Serializable]
	public enum SecurityProtocolType
	{
		// Token: 0x04000185 RID: 389
		Default = -1073741824,
		// Token: 0x04000186 RID: 390
		Ssl2 = 12,
		// Token: 0x04000187 RID: 391
		Ssl3 = 48,
		// Token: 0x04000188 RID: 392
		Tls = 192,
		// Token: 0x04000189 RID: 393
		Tls11 = 768,
		// Token: 0x0400018A RID: 394
		Tls12 = 3072
	}
}
