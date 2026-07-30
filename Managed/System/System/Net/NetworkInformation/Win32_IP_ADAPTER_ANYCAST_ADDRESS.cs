using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x0200067F RID: 1663
	internal struct Win32_IP_ADAPTER_ANYCAST_ADDRESS
	{
		// Token: 0x04002A01 RID: 10753
		public Win32LengthFlagsUnion LengthFlags;

		// Token: 0x04002A02 RID: 10754
		public IntPtr Next;

		// Token: 0x04002A03 RID: 10755
		public Win32_SOCKET_ADDRESS Address;
	}
}
