using System;
using System.Runtime.InteropServices;

namespace System.Net.NetworkInformation
{
	// Token: 0x0200063C RID: 1596
	[StructLayout(LayoutKind.Sequential)]
	internal class Win32_IP_PER_ADAPTER_INFO
	{
		// Token: 0x0400289B RID: 10395
		public uint AutoconfigEnabled;

		// Token: 0x0400289C RID: 10396
		public uint AutoconfigActive;

		// Token: 0x0400289D RID: 10397
		public IntPtr CurrentDnsServer;

		// Token: 0x0400289E RID: 10398
		public Win32_IP_ADDR_STRING DnsServerList;
	}
}
