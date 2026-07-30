using System;
using System.Transactions;

namespace Mono.Data.Sqlite
{
	// Token: 0x02000020 RID: 32
	internal class SQLiteEnlistment : IEnlistmentNotification
	{
		// Token: 0x060001DF RID: 479 RVA: 0x0000B659 File Offset: 0x00009859
		internal SQLiteEnlistment(SqliteConnection cnn, Transaction scope)
		{
			this._transaction = cnn.BeginTransaction();
			this._scope = scope;
			this._disposeConnection = false;
			this._scope.EnlistVolatile(this, EnlistmentOptions.None);
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x0000B689 File Offset: 0x00009889
		private void Cleanup(SqliteConnection cnn)
		{
			if (this._disposeConnection)
			{
				cnn.Dispose();
			}
			this._transaction = null;
			this._scope = null;
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x0000B6A8 File Offset: 0x000098A8
		public void Commit(Enlistment enlistment)
		{
			SqliteConnection connection = this._transaction.Connection;
			connection._enlistment = null;
			try
			{
				this._transaction.IsValid(true);
				this._transaction.Connection._transactionLevel = 1;
				this._transaction.Commit();
				enlistment.Done();
			}
			finally
			{
				this.Cleanup(connection);
			}
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x0000B714 File Offset: 0x00009914
		public void InDoubt(Enlistment enlistment)
		{
			enlistment.Done();
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x0000B71C File Offset: 0x0000991C
		public void Prepare(PreparingEnlistment preparingEnlistment)
		{
			if (!this._transaction.IsValid(false))
			{
				preparingEnlistment.ForceRollback();
				return;
			}
			preparingEnlistment.Prepared();
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x0000B73C File Offset: 0x0000993C
		public void Rollback(Enlistment enlistment)
		{
			SqliteConnection connection = this._transaction.Connection;
			connection._enlistment = null;
			try
			{
				this._transaction.Rollback();
				enlistment.Done();
			}
			finally
			{
				this.Cleanup(connection);
			}
		}

		// Token: 0x040000A3 RID: 163
		internal SqliteTransaction _transaction;

		// Token: 0x040000A4 RID: 164
		internal Transaction _scope;

		// Token: 0x040000A5 RID: 165
		internal bool _disposeConnection;
	}
}
