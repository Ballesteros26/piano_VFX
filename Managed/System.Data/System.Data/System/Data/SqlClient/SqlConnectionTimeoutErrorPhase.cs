using System;

namespace System.Data.SqlClient
{
	// Token: 0x0200019D RID: 413
	internal enum SqlConnectionTimeoutErrorPhase
	{
		// Token: 0x04000D03 RID: 3331
		Undefined,
		// Token: 0x04000D04 RID: 3332
		PreLoginBegin,
		// Token: 0x04000D05 RID: 3333
		InitializeConnection,
		// Token: 0x04000D06 RID: 3334
		SendPreLoginHandshake,
		// Token: 0x04000D07 RID: 3335
		ConsumePreLoginHandshake,
		// Token: 0x04000D08 RID: 3336
		LoginBegin,
		// Token: 0x04000D09 RID: 3337
		ProcessConnectionAuth,
		// Token: 0x04000D0A RID: 3338
		PostLogin,
		// Token: 0x04000D0B RID: 3339
		Complete,
		// Token: 0x04000D0C RID: 3340
		Count
	}
}
