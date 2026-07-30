using System;
using System.Runtime.InteropServices;

namespace System.Net.NetworkInformation
{
	// Token: 0x02000678 RID: 1656
	internal struct Win32_FIXED_INFO
	{
		// Token: 0x0400298E RID: 10638
		private const int MAX_HOSTNAME_LEN = 128;

		// Token: 0x0400298F RID: 10639
		private const int MAX_DOMAIN_NAME_LEN = 128;

		// Token: 0x04002990 RID: 10640
		private const int MAX_SCOPE_ID_LEN = 256;

		// Token: 0x04002991 RID: 10641
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 132)]
		public string HostName;

		// Token: 0x04002992 RID: 10642
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 132)]
		public string DomainName;

		// Token: 0x04002993 RID: 10643
		public IntPtr CurrentDnsServer;

		// Token: 0x04002994 RID: 10644
		public Win32_IP_ADDR_STRING DnsServerList;

		// Token: 0x04002995 RID: 10645
		public NetBiosNodeType NodeType;

		// Token: 0x04002996 RID: 10646
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
		public string ScopeId;

		// Token: 0x04002997 RID: 10647
		public uint EnableRouting;

		// Token: 0x04002998 RID: 10648
		public uint EnableProxy;

		// Token: 0x04002999 RID: 10649
		public uint EnableDns;
	}
}
