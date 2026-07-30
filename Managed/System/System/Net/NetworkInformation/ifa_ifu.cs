using System;
using System.Runtime.InteropServices;

namespace System.Net.NetworkInformation
{
	// Token: 0x0200064A RID: 1610
	[StructLayout(LayoutKind.Explicit)]
	internal struct ifa_ifu
	{
		// Token: 0x040028CB RID: 10443
		[FieldOffset(0)]
		public IntPtr ifu_broadaddr;

		// Token: 0x040028CC RID: 10444
		[FieldOffset(0)]
		public IntPtr ifu_dstaddr;
	}
}
