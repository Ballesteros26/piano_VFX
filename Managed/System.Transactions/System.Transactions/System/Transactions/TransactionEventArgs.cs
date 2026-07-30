using System;

namespace System.Transactions
{
	/// <summary>Provides data for the following transaction events: <see cref="E:System.Transactions.TransactionManager.DistributedTransactionStarted" />, <see cref="E:System.Transactions.Transaction.TransactionCompleted" />.</summary>
	// Token: 0x02000020 RID: 32
	public class TransactionEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Transactions.TransactionEventArgs" /> class. </summary>
		// Token: 0x0600008B RID: 139 RVA: 0x00002B1E File Offset: 0x00000D1E
		public TransactionEventArgs()
		{
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00002B26 File Offset: 0x00000D26
		internal TransactionEventArgs(Transaction transaction)
			: this()
		{
			this.transaction = transaction;
		}

		/// <summary>Gets the transaction for which event status is provided.</summary>
		/// <returns>A <see cref="T:System.Transactions.Transaction" /> for which event status is provided.</returns>
		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600008D RID: 141 RVA: 0x00002B35 File Offset: 0x00000D35
		public Transaction Transaction
		{
			get
			{
				return this.transaction;
			}
		}

		// Token: 0x04000059 RID: 89
		private Transaction transaction;
	}
}
