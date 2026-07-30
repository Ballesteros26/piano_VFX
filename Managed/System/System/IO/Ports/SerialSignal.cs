using System;

namespace System.IO.Ports
{
	// Token: 0x020003FD RID: 1021
	internal enum SerialSignal
	{
		// Token: 0x04001B29 RID: 6953
		None,
		// Token: 0x04001B2A RID: 6954
		Cd,
		// Token: 0x04001B2B RID: 6955
		Cts,
		// Token: 0x04001B2C RID: 6956
		Dsr = 4,
		// Token: 0x04001B2D RID: 6957
		Dtr = 8,
		// Token: 0x04001B2E RID: 6958
		Rts = 16
	}
}
