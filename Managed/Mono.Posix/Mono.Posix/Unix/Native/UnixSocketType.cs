using System;

namespace Mono.Unix.Native
{
	// Token: 0x02000049 RID: 73
	[Map]
	[CLSCompliant(false)]
	public enum UnixSocketType
	{
		// Token: 0x04000361 RID: 865
		SOCK_STREAM = 1,
		// Token: 0x04000362 RID: 866
		SOCK_DGRAM,
		// Token: 0x04000363 RID: 867
		SOCK_RAW,
		// Token: 0x04000364 RID: 868
		SOCK_RDM,
		// Token: 0x04000365 RID: 869
		SOCK_SEQPACKET,
		// Token: 0x04000366 RID: 870
		SOCK_DCCP,
		// Token: 0x04000367 RID: 871
		SOCK_PACKET = 10
	}
}
