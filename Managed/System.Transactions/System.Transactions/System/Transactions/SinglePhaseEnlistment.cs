using System;

namespace System.Transactions
{
	/// <summary>Provides a set of callbacks that facilitate communication between a participant enlisted for Single Phase Commit and the transaction manager when the <see cref="M:System.Transactions.ISinglePhaseNotification.SinglePhaseCommit(System.Transactions.SinglePhaseEnlistment)" /> notification is received.</summary>
	// Token: 0x0200001B RID: 27
	public class SinglePhaseEnlistment : Enlistment
	{
		// Token: 0x06000045 RID: 69 RVA: 0x0000227D File Offset: 0x0000047D
		internal SinglePhaseEnlistment()
		{
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002285 File Offset: 0x00000485
		internal SinglePhaseEnlistment(Transaction tx, object abortingEnlisted)
		{
			this.tx = tx;
			this.abortingEnlisted = abortingEnlisted;
		}

		/// <summary>Represents a callback that is used to indicate to the transaction manager that the transaction should be rolled back.</summary>
		// Token: 0x06000047 RID: 71 RVA: 0x0000229B File Offset: 0x0000049B
		public void Aborted()
		{
			this.Aborted(null);
		}

		/// <summary>Represents a callback that is used to indicate to the transaction manager that the transaction should be rolled back, and provides an explanation.</summary>
		/// <param name="e">An explanation of why a rollback is initiated.</param>
		// Token: 0x06000048 RID: 72 RVA: 0x000022A4 File Offset: 0x000004A4
		public void Aborted(Exception e)
		{
			if (this.tx != null)
			{
				this.tx.Rollback(e, this.abortingEnlisted);
			}
		}

		/// <summary>Represents a callback that is used to indicate to the transaction manager that the SinglePhaseCommit was successful.</summary>
		// Token: 0x06000049 RID: 73 RVA: 0x000021E0 File Offset: 0x000003E0
		[MonoTODO]
		public void Committed()
		{
		}

		/// <summary>Represents a callback that is used to indicate to the transaction manager that the status of the transaction is in doubt.</summary>
		// Token: 0x0600004A RID: 74 RVA: 0x00002162 File Offset: 0x00000362
		[MonoTODO("Not implemented")]
		public void InDoubt()
		{
			throw new NotImplementedException();
		}

		/// <summary>Represents a callback that is used to indicate to the transaction manager that the status of the transaction is in doubt, and provides an explanation.</summary>
		/// <param name="e">An explanation of why the transaction is in doubt.</param>
		// Token: 0x0600004B RID: 75 RVA: 0x00002162 File Offset: 0x00000362
		[MonoTODO("Not implemented")]
		public void InDoubt(Exception e)
		{
			throw new NotImplementedException();
		}

		// Token: 0x04000048 RID: 72
		private Transaction tx;

		// Token: 0x04000049 RID: 73
		private object abortingEnlisted;
	}
}
