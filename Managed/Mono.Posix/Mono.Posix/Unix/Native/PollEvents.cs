using System;

namespace Mono.Unix.Native
{
	// Token: 0x02000041 RID: 65
	[Map]
	[Flags]
	public enum PollEvents : short
	{
		// Token: 0x04000325 RID: 805
		POLLIN = 1,
		// Token: 0x04000326 RID: 806
		POLLPRI = 2,
		// Token: 0x04000327 RID: 807
		POLLOUT = 4,
		// Token: 0x04000328 RID: 808
		POLLERR = 8,
		// Token: 0x04000329 RID: 809
		POLLHUP = 16,
		// Token: 0x0400032A RID: 810
		POLLNVAL = 32,
		// Token: 0x0400032B RID: 811
		POLLRDNORM = 64,
		// Token: 0x0400032C RID: 812
		POLLRDBAND = 128,
		// Token: 0x0400032D RID: 813
		POLLWRNORM = 256,
		// Token: 0x0400032E RID: 814
		POLLWRBAND = 512
	}
}
