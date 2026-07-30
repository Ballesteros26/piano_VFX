using System;

namespace System.Net.NetworkInformation.MacOsStructs
{
	// Token: 0x02000687 RID: 1671
	internal struct ifaddrs
	{
		// Token: 0x04002A1F RID: 10783
		public IntPtr ifa_next;

		// Token: 0x04002A20 RID: 10784
		public string ifa_name;

		// Token: 0x04002A21 RID: 10785
		public uint ifa_flags;

		// Token: 0x04002A22 RID: 10786
		public IntPtr ifa_addr;

		// Token: 0x04002A23 RID: 10787
		public IntPtr ifa_netmask;

		// Token: 0x04002A24 RID: 10788
		public IntPtr ifa_dstaddr;

		// Token: 0x04002A25 RID: 10789
		public IntPtr ifa_data;
	}
}
