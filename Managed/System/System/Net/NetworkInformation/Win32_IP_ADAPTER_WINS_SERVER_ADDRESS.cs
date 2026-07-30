using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x02000683 RID: 1667
	internal struct Win32_IP_ADAPTER_WINS_SERVER_ADDRESS
	{
		// Token: 0x04002A0D RID: 10765
		public Win32LengthFlagsUnion LengthFlags;

		// Token: 0x04002A0E RID: 10766
		public IntPtr Next;

		// Token: 0x04002A0F RID: 10767
		public Win32_SOCKET_ADDRESS Address;
	}
}
