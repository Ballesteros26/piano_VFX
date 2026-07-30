using System;
using System.Runtime.InteropServices;

namespace System.Net.NetworkInformation
{
	// Token: 0x02000649 RID: 1609
	internal struct Win32_MIBICMPSTATS_EX
	{
		// Token: 0x040028C8 RID: 10440
		public uint Msgs;

		// Token: 0x040028C9 RID: 10441
		public uint Errors;

		// Token: 0x040028CA RID: 10442
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
		public uint[] Counts;
	}
}
