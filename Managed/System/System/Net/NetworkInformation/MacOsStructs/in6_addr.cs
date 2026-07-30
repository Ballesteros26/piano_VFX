using System;
using System.Runtime.InteropServices;

namespace System.Net.NetworkInformation.MacOsStructs
{
	// Token: 0x0200068A RID: 1674
	internal struct in6_addr
	{
		// Token: 0x04002A2C RID: 10796
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
		public byte[] u6_addr8;
	}
}
