using System;

namespace System.Data.SqlClient
{
	// Token: 0x02000234 RID: 564
	internal enum TransactionType
	{
		// Token: 0x0400123A RID: 4666
		LocalFromTSQL = 1,
		// Token: 0x0400123B RID: 4667
		LocalFromAPI,
		// Token: 0x0400123C RID: 4668
		Delegated,
		// Token: 0x0400123D RID: 4669
		Distributed,
		// Token: 0x0400123E RID: 4670
		Context
	}
}
