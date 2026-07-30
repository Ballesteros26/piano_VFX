using System;

namespace Mono.Unix.Native
{
	// Token: 0x0200005C RID: 92
	[Flags]
	[Map]
	[CLSCompliant(false)]
	public enum EpollEvents : uint
	{
		// Token: 0x04000433 RID: 1075
		EPOLLIN = 1U,
		// Token: 0x04000434 RID: 1076
		EPOLLPRI = 2U,
		// Token: 0x04000435 RID: 1077
		EPOLLOUT = 4U,
		// Token: 0x04000436 RID: 1078
		EPOLLRDNORM = 64U,
		// Token: 0x04000437 RID: 1079
		EPOLLRDBAND = 128U,
		// Token: 0x04000438 RID: 1080
		EPOLLWRNORM = 256U,
		// Token: 0x04000439 RID: 1081
		EPOLLWRBAND = 512U,
		// Token: 0x0400043A RID: 1082
		EPOLLMSG = 1024U,
		// Token: 0x0400043B RID: 1083
		EPOLLERR = 8U,
		// Token: 0x0400043C RID: 1084
		EPOLLHUP = 16U,
		// Token: 0x0400043D RID: 1085
		EPOLLRDHUP = 8192U,
		// Token: 0x0400043E RID: 1086
		EPOLLONESHOT = 1073741824U,
		// Token: 0x0400043F RID: 1087
		EPOLLET = 2147483648U
	}
}
