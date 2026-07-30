using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x02000681 RID: 1665
	internal struct Win32_IP_ADAPTER_MULTICAST_ADDRESS
	{
		// Token: 0x04002A07 RID: 10759
		public Win32LengthFlagsUnion LengthFlags;

		// Token: 0x04002A08 RID: 10760
		public IntPtr Next;

		// Token: 0x04002A09 RID: 10761
		public Win32_SOCKET_ADDRESS Address;
	}
}
