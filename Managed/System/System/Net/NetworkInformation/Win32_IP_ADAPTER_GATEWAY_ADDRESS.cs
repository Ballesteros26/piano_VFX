using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x02000682 RID: 1666
	internal struct Win32_IP_ADAPTER_GATEWAY_ADDRESS
	{
		// Token: 0x04002A0A RID: 10762
		public Win32LengthFlagsUnion LengthFlags;

		// Token: 0x04002A0B RID: 10763
		public IntPtr Next;

		// Token: 0x04002A0C RID: 10764
		public Win32_SOCKET_ADDRESS Address;
	}
}
