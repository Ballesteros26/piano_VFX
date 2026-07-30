using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x0200064B RID: 1611
	internal struct ifaddrs
	{
		// Token: 0x040028CD RID: 10445
		public IntPtr ifa_next;

		// Token: 0x040028CE RID: 10446
		public string ifa_name;

		// Token: 0x040028CF RID: 10447
		public uint ifa_flags;

		// Token: 0x040028D0 RID: 10448
		public IntPtr ifa_addr;

		// Token: 0x040028D1 RID: 10449
		public IntPtr ifa_netmask;

		// Token: 0x040028D2 RID: 10450
		public ifa_ifu ifa_ifu;

		// Token: 0x040028D3 RID: 10451
		public IntPtr ifa_data;
	}
}
