using System;

namespace System.Net.Sockets
{
	// Token: 0x020005E3 RID: 1507
	internal enum SocketOperation
	{
		// Token: 0x04002756 RID: 10070
		Accept,
		// Token: 0x04002757 RID: 10071
		Connect,
		// Token: 0x04002758 RID: 10072
		Receive,
		// Token: 0x04002759 RID: 10073
		ReceiveFrom,
		// Token: 0x0400275A RID: 10074
		Send,
		// Token: 0x0400275B RID: 10075
		SendTo,
		// Token: 0x0400275C RID: 10076
		RecvJustCallback,
		// Token: 0x0400275D RID: 10077
		SendJustCallback,
		// Token: 0x0400275E RID: 10078
		Disconnect,
		// Token: 0x0400275F RID: 10079
		AcceptReceive,
		// Token: 0x04002760 RID: 10080
		ReceiveGeneric,
		// Token: 0x04002761 RID: 10081
		SendGeneric
	}
}
