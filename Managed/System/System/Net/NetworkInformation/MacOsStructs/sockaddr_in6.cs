using System;

namespace System.Net.NetworkInformation.MacOsStructs
{
	// Token: 0x0200068B RID: 1675
	internal struct sockaddr_in6
	{
		// Token: 0x04002A2D RID: 10797
		public byte sin6_len;

		// Token: 0x04002A2E RID: 10798
		public byte sin6_family;

		// Token: 0x04002A2F RID: 10799
		public ushort sin6_port;

		// Token: 0x04002A30 RID: 10800
		public uint sin6_flowinfo;

		// Token: 0x04002A31 RID: 10801
		public in6_addr sin6_addr;

		// Token: 0x04002A32 RID: 10802
		public uint sin6_scope_id;
	}
}
