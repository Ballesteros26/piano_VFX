using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x0200064C RID: 1612
	internal struct sockaddr_in
	{
		// Token: 0x040028D4 RID: 10452
		public ushort sin_family;

		// Token: 0x040028D5 RID: 10453
		public ushort sin_port;

		// Token: 0x040028D6 RID: 10454
		public uint sin_addr;
	}
}
