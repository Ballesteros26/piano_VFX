using System;

namespace System.Data.SqlClient.SNI
{
	// Token: 0x02000243 RID: 579
	[Flags]
	internal enum SNISMUXFlags
	{
		// Token: 0x04001272 RID: 4722
		SMUX_SYN = 1,
		// Token: 0x04001273 RID: 4723
		SMUX_ACK = 2,
		// Token: 0x04001274 RID: 4724
		SMUX_FIN = 4,
		// Token: 0x04001275 RID: 4725
		SMUX_DATA = 8
	}
}
