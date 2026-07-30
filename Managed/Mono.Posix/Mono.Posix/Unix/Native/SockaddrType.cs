using System;

namespace Mono.Unix.Native
{
	// Token: 0x02000050 RID: 80
	[Map]
	internal enum SockaddrType
	{
		// Token: 0x040003F7 RID: 1015
		Invalid,
		// Token: 0x040003F8 RID: 1016
		SockaddrStorage,
		// Token: 0x040003F9 RID: 1017
		SockaddrUn,
		// Token: 0x040003FA RID: 1018
		Sockaddr,
		// Token: 0x040003FB RID: 1019
		SockaddrIn,
		// Token: 0x040003FC RID: 1020
		SockaddrIn6,
		// Token: 0x040003FD RID: 1021
		MustBeWrapped = 32768
	}
}
