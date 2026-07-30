using System;
using System.Runtime.InteropServices;

namespace System.Net.NetworkInformation
{
	// Token: 0x0200064E RID: 1614
	internal struct in6_addr
	{
		// Token: 0x040028DC RID: 10460
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
		public byte[] u6_addr8;
	}
}
