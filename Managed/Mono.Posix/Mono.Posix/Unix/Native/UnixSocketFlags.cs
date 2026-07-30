using System;

namespace Mono.Unix.Native
{
	// Token: 0x0200004A RID: 74
	[Map]
	[Flags]
	[CLSCompliant(false)]
	public enum UnixSocketFlags
	{
		// Token: 0x04000369 RID: 873
		SOCK_CLOEXEC = 524288,
		// Token: 0x0400036A RID: 874
		SOCK_NONBLOCK = 2048
	}
}
