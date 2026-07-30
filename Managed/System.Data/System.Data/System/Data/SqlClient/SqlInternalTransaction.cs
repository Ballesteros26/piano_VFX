using System;
using System.Data.Common;
using System.Threading;

namespace System.Data.SqlClient
{
	// Token: 0x02000235 RID: 565
	internal sealed class SqlInternalTransaction
	{
		// Token: 0x170004AC RID: 1196
		// (get) Token: 0x06001986 RID: 6534 RVA: 0x000821CA File Offset: 0x000803CA
		// (set) Token: 0x06001987 RID: 6535 RVA: 0x000821D2 File Offset: 0x000803D2
		internal bool RestoreBrokenConnection { get; set; }

		// Token: 0x170004AD RID: 1197
		// (get) Token: 0x06001988 RID: 6536 RVA: 0x000821DB File Offset: 0x000803DB
		// (set) Token: 0x06001989 RID: 6537 RVA: 0x000821E3 File Offset: 0x000803E3
		internal bool ConnectionHasBeenRestored { get; set; }

		// Token: 0x0600198A RID: 6538 RVA: 0x000821EC File Offset: 0x000803EC
		internal SqlInternalTransaction(SqlInternalConnection innerConnection, TransactionType type, SqlTransaction outerTransaction)
			: this(innerConnection, type, outerTransaction, 0L)
		{
		}

		// Token: 0x0600198B RID: 6539 RVA: 0x000821F9 File Offset: 0x000803F9
		internal SqlInternalTransaction(SqlInternalConnection innerConnection, TransactionType type, SqlTransaction outerTransaction, long transactionId)
		{
			this._innerConnection = innerConnection;
			this._transactionType = type;
			if (outerTransaction != null)
			{
				this._parent = new WeakReference(outerTransaction);
			}
			this._transactionId = transactionId;
			this.RestoreBrokenConnection = false;
			this.ConnectionHasBeenRestored = false;
		}

		// Token: 0x170004AE RID: 1198
		// (get) Token: 0x0600198C RID: 6540 RVA: 0x00082234 File Offset: 0x00080434
		internal bool HasParentTransaction
		{
			get
			{
				return TransactionType.LocalFromAPI == this._transactionType || (TransactionType.LocalFromTSQL == this._transactionType && this._parent != null);
			}
		}

		// Token: 0x170004AF RID: 1199
		// (get) Token: 0x0600198D RID: 6541 RVA: 0x00082255 File Offset: 0x00080455
		internal bool IsAborted
		{
			get
			{
				return TransactionState.Aborted == this._transactionState;
			}
		}

		// Token: 0x170004B0 RID: 1200
		// (get) Token: 0x0600198E RID: 6542 RVA: 0x00082260 File Offset: 0x00080460
		internal bool IsActive
		{
			get
			{
				return TransactionState.Active == this._transactionState;
			}
		}

		// Token: 0x170004B1 RID: 1201
		// (get) Token: 0x0600198F RID: 6543 RVA: 0x0008226B File Offset: 0x0008046B
		internal bool IsCommitted
		{
			get
			{
				return TransactionState.Committed == this._transactionState;
			}
		}

		// Token: 0x170004B2 RID: 1202
		// (get) Token: 0x06001990 RID: 6544 RVA: 0x00082276 File Offset: 0x00080476
		internal bool IsCompleted
		{
			get
			{
				return TransactionState.Aborted == this._transactionState || TransactionState.Committed == this._transactionState || TransactionState.Unknown == this._transactionState;
			}
		}

		// Token: 0x170004B3 RID: 1203
		// (get) Token: 0x06001991 RID: 6545 RVA: 0x00082295 File Offset: 0x00080495
		internal bool IsDelegated
		{
			get
			{
				return TransactionType.Delegated == this._transactionType;
			}
		}

		// Token: 0x170004B4 RID: 1204
		// (get) Token: 0x06001992 RID: 6546 RVA: 0x000822A0 File Offset: 0x000804A0
		internal bool IsDistributed
		{
			get
			{
				return TransactionType.Distributed == this._transactionType;
			}
		}

		// Token: 0x170004B5 RID: 1205
		// (get) Token: 0x06001993 RID: 6547 RVA: 0x000822AB File Offset: 0x000804AB
		internal bool IsLocal
		{
			get
			{
				return TransactionType.LocalFromTSQL == this._transactionType || TransactionType.LocalFromAPI == this._transactionType;
			}
		}

		// Token: 0x170004B6 RID: 1206
		// (get) Token: 0x06001994 RID: 6548 RVA: 0x000822C4 File Offset: 0x000804C4
		internal bool IsOrphaned
		{
			get
			{
				return this._parent != null && this._parent.Target == null;
			}
		}

		// Token: 0x170004B7 RID: 1207
		// (get) Token: 0x06001995 RID: 6549 RVA: 0x000822F1 File Offset: 0x000804F1
		internal bool IsZombied
		{
			get
			{
				return this._innerConnection == null;
			}
		}

		// Token: 0x170004B8 RID: 1208
		// (get) Token: 0x06001996 RID: 6550 RVA: 0x000822FC File Offset: 0x000804FC
		internal int OpenResultsCount
		{
			get
			{
				return this._openResultCount;
			}
		}

		// Token: 0x170004B9 RID: 1209
		// (get) Token: 0x06001997 RID: 6551 RVA: 0x00082304 File Offset: 0x00080504
		internal SqlTransaction Parent
		{
			get
			{
				SqlTransaction sqlTransaction = null;
				if (this._parent != null)
				{
					sqlTransaction = (SqlTransaction)this._parent.Target;
				}
				return sqlTransaction;
			}
		}

		// Token: 0x170004BA RID: 1210
		// (get) Token: 0x06001998 RID: 6552 RVA: 0x0008232D File Offset: 0x0008052D
		// (set) Token: 0x06001999 RID: 6553 RVA: 0x00082335 File Offset: 0x00080535
		internal long TransactionId
		{
			get
			{
				return this._transactionId;
			}
			set
			{
				this._transactionId = value;
			}
		}

		// Token: 0x0600199A RID: 6554 RVA: 0x0008233E File Offset: 0x0008053E
		internal void Activate()
		{
			this._transactionState = TransactionState.Active;
		}

		// Token: 0x0600199B RID: 6555 RVA: 0x00082348 File Offset: 0x00080548
		private void CheckTransactionLevelAndZombie()
		{
			try
			{
				if (!this.IsZombied && this.GetServerTransactionLevel() == 0)
				{
					this.Zombie();
				}
			}
			catch (Exception ex)
			{
				if (!ADP.IsCatchableExceptionType(ex))
				{
					throw;
				}
				this.Zombie();
			}
		}

		// Token: 0x0600199C RID: 6556 RVA: 0x00082390 File Offset: 0x00080590
		internal void CloseFromConnection()
		{
			SqlInternalConnection innerConnection = this._innerConnection;
			bool flag = true;
			try
			{
				innerConnection.ExecuteTransaction(SqlInternalConnection.TransactionRequest.IfRollback, null, IsolationLevel.Unspecified, null, false);
			}
			catch (Exception ex)
			{
				flag = ADP.IsCatchableExceptionType(ex);
				throw;
			}
			finally
			{
				if (flag)
				{
					this.Zombie();
				}
			}
		}

		// Token: 0x0600199D RID: 6557 RVA: 0x000823E4 File Offset: 0x000805E4
		internal void Commit()
		{
			if (this._innerConnection.IsLockedForBulkCopy)
			{
				throw SQL.ConnectionLockedForBcpEvent();
			}
			this._innerConnection.ValidateConnectionForExecute(null);
			try
			{
				this._innerConnection.ExecuteTransaction(SqlInternalConnection.TransactionRequest.Commit, null, IsolationLevel.Unspecified, null, false);
				this.ZombieParent();
			}
			catch (Exception ex)
			{
				if (ADP.IsCatchableExceptionType(ex))
				{
					this.CheckTransactionLevelAndZombie();
				}
				throw;
			}
		}

		// Token: 0x0600199E RID: 6558 RVA: 0x00082448 File Offset: 0x00080648
		internal void Completed(TransactionState transactionState)
		{
			this._transactionState = transactionState;
			this.Zombie();
		}

		// Token: 0x0600199F RID: 6559 RVA: 0x00082457 File Offset: 0x00080657
		internal int DecrementAndObtainOpenResultCount()
		{
			int num = Interlocked.Decrement(ref this._openResultCount);
			if (num < 0)
			{
				throw SQL.OpenResultCountExceeded();
			}
			return num;
		}

		// Token: 0x060019A0 RID: 6560 RVA: 0x0008246E File Offset: 0x0008066E
		internal void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060019A1 RID: 6561 RVA: 0x0008247D File Offset: 0x0008067D
		private void Dispose(bool disposing)
		{
			if (disposing && this._innerConnection != null)
			{
				this._disposing = true;
				this.Rollback();
			}
		}

		// Token: 0x060019A2 RID: 6562 RVA: 0x00082498 File Offset: 0x00080698
		private int GetServerTransactionLevel()
		{
			int num;
			using (SqlCommand sqlCommand = new SqlCommand("set @out = @@trancount", (SqlConnection)this._innerConnection.Owner))
			{
				sqlCommand.Transaction = this.Parent;
				SqlParameter sqlParameter = new SqlParameter("@out", SqlDbType.Int);
				sqlParameter.Direction = ParameterDirection.Output;
				sqlCommand.Parameters.Add(sqlParameter);
				sqlCommand.RunExecuteReader(CommandBehavior.Default, RunBehavior.UntilDone, false, "GetServerTransactionLevel");
				num = (int)sqlParameter.Value;
			}
			return num;
		}

		// Token: 0x060019A3 RID: 6563 RVA: 0x00082524 File Offset: 0x00080724
		internal int IncrementAndObtainOpenResultCount()
		{
			int num = Interlocked.Increment(ref this._openResultCount);
			if (num < 0)
			{
				throw SQL.OpenResultCountExceeded();
			}
			return num;
		}

		// Token: 0x060019A4 RID: 6564 RVA: 0x0008253B File Offset: 0x0008073B
		internal void InitParent(SqlTransaction transaction)
		{
			this._parent = new WeakReference(transaction);
		}

		// Token: 0x060019A5 RID: 6565 RVA: 0x0008254C File Offset: 0x0008074C
		internal void Rollback()
		{
			if (this._innerConnection.IsLockedForBulkCopy)
			{
				throw SQL.ConnectionLockedForBcpEvent();
			}
			this._innerConnection.ValidateConnectionForExecute(null);
			try
			{
				this._innerConnection.ExecuteTransaction(SqlInternalConnection.TransactionRequest.IfRollback, null, IsolationLevel.Unspecified, null, false);
				this.Zombie();
			}
			catch (Exception ex)
			{
				if (!ADP.IsCatchableExceptionType(ex))
				{
					throw;
				}
				this.CheckTransactionLevelAndZombie();
				if (!this._disposing)
				{
					throw;
				}
			}
		}

		// Token: 0x060019A6 RID: 6566 RVA: 0x000825BC File Offset: 0x000807BC
		internal void Rollback(string transactionName)
		{
			if (this._innerConnection.IsLockedForBulkCopy)
			{
				throw SQL.ConnectionLockedForBcpEvent();
			}
			this._innerConnection.ValidateConnectionForExecute(null);
			if (string.IsNullOrEmpty(transactionName))
			{
				throw SQL.NullEmptyTransactionName();
			}
			try
			{
				this._innerConnection.ExecuteTransaction(SqlInternalConnection.TransactionRequest.Rollback, transactionName, IsolationLevel.Unspecified, null, false);
			}
			catch (Exception ex)
			{
				if (ADP.IsCatchableExceptionType(ex))
				{
					this.CheckTransactionLevelAndZombie();
				}
				throw;
			}
		}

		// Token: 0x060019A7 RID: 6567 RVA: 0x00082628 File Offset: 0x00080828
		internal void Save(string savePointName)
		{
			this._innerConnection.ValidateConnectionForExecute(null);
			if (string.IsNullOrEmpty(savePointName))
			{
				throw SQL.NullEmptyTransactionName();
			}
			try
			{
				this._innerConnection.ExecuteTransaction(SqlInternalConnection.TransactionRequest.Save, savePointName, IsolationLevel.Unspecified, null, false);
			}
			catch (Exception ex)
			{
				if (ADP.IsCatchableExceptionType(ex))
				{
					this.CheckTransactionLevelAndZombie();
				}
				throw;
			}
		}

		// Token: 0x060019A8 RID: 6568 RVA: 0x00082684 File Offset: 0x00080884
		internal void Zombie()
		{
			this.ZombieParent();
			SqlInternalConnection innerConnection = this._innerConnection;
			this._innerConnection = null;
			if (innerConnection != null)
			{
				innerConnection.DisconnectTransaction(this);
			}
		}

		// Token: 0x060019A9 RID: 6569 RVA: 0x000826B0 File Offset: 0x000808B0
		private void ZombieParent()
		{
			if (this._parent != null)
			{
				SqlTransaction sqlTransaction = (SqlTransaction)this._parent.Target;
				if (sqlTransaction != null)
				{
					sqlTransaction.Zombie();
				}
				this._parent = null;
			}
		}

		// Token: 0x0400123F RID: 4671
		internal const long NullTransactionId = 0L;

		// Token: 0x04001240 RID: 4672
		private TransactionState _transactionState;

		// Token: 0x04001241 RID: 4673
		private TransactionType _transactionType;

		// Token: 0x04001242 RID: 4674
		private long _transactionId;

		// Token: 0x04001243 RID: 4675
		private int _openResultCount;

		// Token: 0x04001244 RID: 4676
		private SqlInternalConnection _innerConnection;

		// Token: 0x04001245 RID: 4677
		private bool _disposing;

		// Token: 0x04001246 RID: 4678
		private WeakReference _parent;
	}
}
