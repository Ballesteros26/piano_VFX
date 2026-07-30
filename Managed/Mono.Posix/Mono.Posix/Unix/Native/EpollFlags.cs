using System;

namespace Mono.Unix.Native
{
	// Token: 0x0200005B RID: 91
	[Flags]
	[Map]
	public enum EpollFlags
	{
		// Token: 0x04000430 RID: 1072
		EPOLL_CLOEXEC = 2000000,
		// Token: 0x04000431 RID: 1073
		EPOLL_NONBLOCK = 4000
	}
}
