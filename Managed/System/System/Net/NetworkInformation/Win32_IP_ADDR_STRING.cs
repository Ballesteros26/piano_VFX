using System;
using System.Runtime.InteropServices;

namespace System.Net.NetworkInformation
{
	// Token: 0x0200067D RID: 1661
	internal struct Win32_IP_ADDR_STRING
	{
		// Token: 0x040029F9 RID: 10745
		public IntPtr Next;

		// Token: 0x040029FA RID: 10746
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
		public string IpAddress;

		// Token: 0x040029FB RID: 10747
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
		public string IpMask;

		// Token: 0x040029FC RID: 10748
		public uint Context;
	}
}
