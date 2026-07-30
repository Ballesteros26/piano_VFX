using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x02000680 RID: 1664
	internal struct Win32_IP_ADAPTER_DNS_SERVER_ADDRESS
	{
		// Token: 0x04002A04 RID: 10756
		public Win32LengthFlagsUnion LengthFlags;

		// Token: 0x04002A05 RID: 10757
		public IntPtr Next;

		// Token: 0x04002A06 RID: 10758
		public Win32_SOCKET_ADDRESS Address;
	}
}
