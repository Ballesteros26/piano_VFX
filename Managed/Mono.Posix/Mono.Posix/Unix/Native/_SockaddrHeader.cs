using System;

namespace Mono.Unix.Native
{
	// Token: 0x02000068 RID: 104
	[Map]
	internal struct _SockaddrHeader
	{
		// Token: 0x0400046F RID: 1135
		internal SockaddrType type;

		// Token: 0x04000470 RID: 1136
		internal UnixAddressFamily sa_family;
	}
}
