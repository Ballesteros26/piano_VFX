using System;
using System.Runtime.InteropServices;

namespace System.Net.NetworkInformation
{
	// Token: 0x02000685 RID: 1669
	internal struct Win32_SOCKADDR
	{
		// Token: 0x04002A1A RID: 10778
		public ushort AddressFamily;

		// Token: 0x04002A1B RID: 10779
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 28)]
		public byte[] AddressData;
	}
}
