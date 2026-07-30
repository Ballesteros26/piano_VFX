using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x0200064D RID: 1613
	internal struct sockaddr_in6
	{
		// Token: 0x040028D7 RID: 10455
		public ushort sin6_family;

		// Token: 0x040028D8 RID: 10456
		public ushort sin6_port;

		// Token: 0x040028D9 RID: 10457
		public uint sin6_flowinfo;

		// Token: 0x040028DA RID: 10458
		public in6_addr sin6_addr;

		// Token: 0x040028DB RID: 10459
		public uint sin6_scope_id;
	}
}
