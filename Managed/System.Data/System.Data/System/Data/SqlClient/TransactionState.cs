using System;

namespace System.Data.SqlClient
{
	// Token: 0x02000233 RID: 563
	internal enum TransactionState
	{
		// Token: 0x04001234 RID: 4660
		Pending,
		// Token: 0x04001235 RID: 4661
		Active,
		// Token: 0x04001236 RID: 4662
		Aborted,
		// Token: 0x04001237 RID: 4663
		Committed,
		// Token: 0x04001238 RID: 4664
		Unknown
	}
}
