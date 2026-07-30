using System;
using System.Runtime.InteropServices;

namespace System.Net
{
	// Token: 0x0200044C RID: 1100
	[StructLayout(LayoutKind.Sequential)]
	internal class SecChannelBindings
	{
		// Token: 0x04001D6E RID: 7534
		internal int dwInitiatorAddrType;

		// Token: 0x04001D6F RID: 7535
		internal int cbInitiatorLength;

		// Token: 0x04001D70 RID: 7536
		internal int dwInitiatorOffset;

		// Token: 0x04001D71 RID: 7537
		internal int dwAcceptorAddrType;

		// Token: 0x04001D72 RID: 7538
		internal int cbAcceptorLength;

		// Token: 0x04001D73 RID: 7539
		internal int dwAcceptorOffset;

		// Token: 0x04001D74 RID: 7540
		internal int cbApplicationDataLength;

		// Token: 0x04001D75 RID: 7541
		internal int dwApplicationDataOffset;
	}
}
