using System;

namespace Mono.Unix.Native
{
	// Token: 0x0200004B RID: 75
	[Map]
	[CLSCompliant(false)]
	public enum UnixSocketProtocol
	{
		// Token: 0x0400036C RID: 876
		IPPROTO_ICMP = 1,
		// Token: 0x0400036D RID: 877
		IPPROTO_IGMP,
		// Token: 0x0400036E RID: 878
		IPPROTO_IPIP = 4,
		// Token: 0x0400036F RID: 879
		IPPROTO_TCP = 6,
		// Token: 0x04000370 RID: 880
		IPPROTO_EGP = 8,
		// Token: 0x04000371 RID: 881
		IPPROTO_PUP = 12,
		// Token: 0x04000372 RID: 882
		IPPROTO_UDP = 17,
		// Token: 0x04000373 RID: 883
		IPPROTO_IDP = 22,
		// Token: 0x04000374 RID: 884
		IPPROTO_TP = 29,
		// Token: 0x04000375 RID: 885
		IPPROTO_DCCP = 33,
		// Token: 0x04000376 RID: 886
		IPPROTO_IPV6 = 41,
		// Token: 0x04000377 RID: 887
		IPPROTO_RSVP = 46,
		// Token: 0x04000378 RID: 888
		IPPROTO_GRE,
		// Token: 0x04000379 RID: 889
		IPPROTO_ESP = 50,
		// Token: 0x0400037A RID: 890
		IPPROTO_AH,
		// Token: 0x0400037B RID: 891
		IPPROTO_MTP = 92,
		// Token: 0x0400037C RID: 892
		IPPROTO_BEETPH = 94,
		// Token: 0x0400037D RID: 893
		IPPROTO_ENCAP = 98,
		// Token: 0x0400037E RID: 894
		IPPROTO_PIM = 103,
		// Token: 0x0400037F RID: 895
		IPPROTO_COMP = 108,
		// Token: 0x04000380 RID: 896
		IPPROTO_SCTP = 132,
		// Token: 0x04000381 RID: 897
		IPPROTO_UDPLITE = 136,
		// Token: 0x04000382 RID: 898
		IPPROTO_RAW = 255,
		// Token: 0x04000383 RID: 899
		IPPROTO_IP = 1024,
		// Token: 0x04000384 RID: 900
		SOL_SOCKET = 2048
	}
}
