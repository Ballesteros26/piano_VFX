using System;
using System.Runtime.InteropServices;

namespace System.Net.NetworkInformation
{
	// Token: 0x0200064F RID: 1615
	internal struct sockaddr_ll
	{
		// Token: 0x040028DD RID: 10461
		public ushort sll_family;

		// Token: 0x040028DE RID: 10462
		public ushort sll_protocol;

		// Token: 0x040028DF RID: 10463
		public int sll_ifindex;

		// Token: 0x040028E0 RID: 10464
		public ushort sll_hatype;

		// Token: 0x040028E1 RID: 10465
		public byte sll_pkttype;

		// Token: 0x040028E2 RID: 10466
		public byte sll_halen;

		// Token: 0x040028E3 RID: 10467
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
		public byte[] sll_addr;
	}
}
