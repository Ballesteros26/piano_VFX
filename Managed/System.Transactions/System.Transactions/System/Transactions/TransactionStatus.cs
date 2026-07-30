using System;

namespace System.Transactions
{
	/// <summary>Describes the current status of a distributed transaction.</summary>
	// Token: 0x0200002C RID: 44
	public enum TransactionStatus
	{
		/// <summary>The status of the transaction is unknown, because some participants must still be polled.</summary>
		// Token: 0x04000078 RID: 120
		Active,
		/// <summary>The transaction has been committed.</summary>
		// Token: 0x04000079 RID: 121
		Committed,
		/// <summary>The transaction has been rolled back.</summary>
		// Token: 0x0400007A RID: 122
		Aborted,
		/// <summary>The status of the transaction is unknown.</summary>
		// Token: 0x0400007B RID: 123
		InDoubt
	}
}
