using System;

namespace Mono.Unix.Native
{
	// Token: 0x0200004E RID: 78
	[Flags]
	[Map]
	[CLSCompliant(false)]
	public enum MessageFlags
	{
		// Token: 0x040003DF RID: 991
		MSG_OOB = 1,
		// Token: 0x040003E0 RID: 992
		MSG_PEEK = 2,
		// Token: 0x040003E1 RID: 993
		MSG_DONTROUTE = 4,
		// Token: 0x040003E2 RID: 994
		MSG_CTRUNC = 8,
		// Token: 0x040003E3 RID: 995
		MSG_PROXY = 16,
		// Token: 0x040003E4 RID: 996
		MSG_TRUNC = 32,
		// Token: 0x040003E5 RID: 997
		MSG_DONTWAIT = 64,
		// Token: 0x040003E6 RID: 998
		MSG_EOR = 128,
		// Token: 0x040003E7 RID: 999
		MSG_WAITALL = 256,
		// Token: 0x040003E8 RID: 1000
		MSG_FIN = 512,
		// Token: 0x040003E9 RID: 1001
		MSG_SYN = 1024,
		// Token: 0x040003EA RID: 1002
		MSG_CONFIRM = 2048,
		// Token: 0x040003EB RID: 1003
		MSG_RST = 4096,
		// Token: 0x040003EC RID: 1004
		MSG_ERRQUEUE = 8192,
		// Token: 0x040003ED RID: 1005
		MSG_NOSIGNAL = 16384,
		// Token: 0x040003EE RID: 1006
		MSG_MORE = 32768,
		// Token: 0x040003EF RID: 1007
		MSG_WAITFORONE = 65536,
		// Token: 0x040003F0 RID: 1008
		MSG_FASTOPEN = 536870912,
		// Token: 0x040003F1 RID: 1009
		MSG_CMSG_CLOEXEC = 1073741824
	}
}
