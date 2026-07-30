using System;

namespace System.Net.NetworkInformation.MacOsStructs
{
	// Token: 0x02000689 RID: 1673
	internal struct sockaddr_in
	{
		// Token: 0x04002A28 RID: 10792
		public byte sin_len;

		// Token: 0x04002A29 RID: 10793
		public byte sin_family;

		// Token: 0x04002A2A RID: 10794
		public ushort sin_port;

		// Token: 0x04002A2B RID: 10795
		public uint sin_addr;
	}
}
