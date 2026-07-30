using System;
using System.Data.Common;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Transactions;

namespace System.Data.SqlClient
{
	// Token: 0x020001B4 RID: 436
	internal sealed class SqlDelegatedTransaction : IPromotableSinglePhaseNotification, ITransactionPromoter
	{
		// Token: 0x170003BF RID: 959
		// (get) Token: 0x06001455 RID: 5205 RVA: 0x00065DE6 File Offset: 0x00063FE6
		internal int ObjectID
		{
			get
			{
				return this._objectID;
			}
		}

		// Token: 0x06001456 RID: 5206 RVA: 0x00065DF0 File Offset: 0x00063FF0
		internal SqlDelegatedTransaction(SqlInternalConnection connection, Transaction tx)
		{
			this._connection = connection;
			this._atomicTransaction = tx;
			this._active = false;
			IsolationLevel isolationLevel = tx.IsolationLevel;
			switch (isolationLevel)
			{
			case IsolationLevel.Serializable:
				this._isolationLevel = IsolationLevel.Serializable;
				return;
			case IsolationLevel.RepeatableRead:
				this._isolationLevel = IsolationLevel.RepeatableRead;
				return;
			case IsolationLevel.ReadCommitted:
				this._isolationLevel = IsolationLevel.ReadCommitted;
				return;
			case IsolationLevel.ReadUncommitted:
				this._isolationLevel = IsolationLevel.ReadUncommitted;
				return;
			case IsolationLevel.Snapshot:
				this._isolationLevel = IsolationLevel.Snapshot;
				return;
			default:
				throw SQL.UnknownSysTxIsolationLevel(isolationLevel);
			}
		}

		// Token: 0x170003C0 RID: 960
		// (get) Token: 0x06001457 RID: 5207 RVA: 0x00065E8D File Offset: 0x0006408D
		internal Transaction Transaction
		{
			get
			{
				return this._atomicTransaction;
			}
		}

		// Token: 0x06001458 RID: 5208 RVA: 0x00065E98 File Offset: 0x00064098
		public void Initialize()
		{
			SqlInternalConnection connection = this._connection;
			SqlConnection connection2 = connection.Connection;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				if (connection.IsEnlistedInTransaction)
				{
					connection.EnlistNull();
				}
				this._internalTransaction = new SqlInternalTransaction(connection, TransactionType.Delegated, null);
				connection.ExecuteTransaction(SqlInternalConnection.TransactionRequest.Begin, null, this._isolationLevel, this._internalTransaction, true);
				if (connection.CurrentTransaction == null)
				{
					connection.DoomThisConnection();
					throw ADP.InternalError(ADP.InternalErrorCode.UnknownTransactionFailure);
				}
				this._active = true;
			}
			catch (OutOfMemoryException ex)
			{
				connection2.Abort(ex);
				throw;
			}
			catch (StackOverflowException ex2)
			{
				connection2.Abort(ex2);
				throw;
			}
			catch (ThreadAbortException ex3)
			{
				connection2.Abort(ex3);
				throw;
			}
		}

		// Token: 0x170003C1 RID: 961
		// (get) Token: 0x06001459 RID: 5209 RVA: 0x00065F50 File Offset: 0x00064150
		internal bool IsActive
		{
			get
			{
				return this._active;
			}
		}

		// Token: 0x0600145A RID: 5210 RVA: 0x00065F58 File Offset: 0x00064158
		public byte[] Promote()
		{
			SqlInternalConnection validConnection = this.GetValidConnection();
			byte[] array = null;
			SqlConnection connection = validConnection.Connection;
			RuntimeHelpers.PrepareConstrainedRegions();
			Exception ex;
			try
			{
				SqlInternalConnection sqlInternalConnection = validConnection;
				lock (sqlInternalConnection)
				{
					try
					{
						this.ValidateActiveOnConnection(validConnection);
						validConnection.ExecuteTransaction(SqlInternalConnection.TransactionRequest.Promote, null, IsolationLevel.Unspecified, this._internalTransaction, true);
						array = this._connection.PromotedDTCToken;
						if (this._connection.IsGlobalTransaction)
						{
							if (SysTxForGlobalTransactions.SetDistributedTransactionIdentifier == null)
							{
								throw SQL.UnsupportedSysTxForGlobalTransactions();
							}
							if (!this._connection.IsGlobalTransactionsEnabledForServer)
							{
								throw SQL.GlobalTransactionsNotEnabled();
							}
							SysTxForGlobalTransactions.SetDistributedTransactionIdentifier.Invoke(this._atomicTransaction, new object[]
							{
								this,
								this.GetGlobalTxnIdentifierFromToken()
							});
						}
						ex = null;
					}
					catch (SqlException ex)
					{
						validConnection.DoomThisConnection();
					}
					catch (InvalidOperationException ex)
					{
						validConnection.DoomThisConnection();
					}
				}
			}
			catch (OutOfMemoryException ex2)
			{
				connection.Abort(ex2);
				throw;
			}
			catch (StackOverflowException ex3)
			{
				connection.Abort(ex3);
				throw;
			}
			catch (ThreadAbortException ex4)
			{
				connection.Abort(ex4);
				throw;
			}
			if (ex != null)
			{
				throw SQL.PromotionFailed(ex);
			}
			return array;
		}

		// Token: 0x0600145B RID: 5211 RVA: 0x000660AC File Offset: 0x000642AC
		public void Rollback(SinglePhaseEnlistment enlistment)
		{
			SqlInternalConnection validConnection = this.GetValidConnection();
			SqlConnection connection = validConnection.Connection;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				SqlInternalConnection sqlInternalConnection = validConnection;
				lock (sqlInternalConnection)
				{
					try
					{
						this.ValidateActiveOnConnection(validConnection);
						this._active = false;
						this._connection = null;
						if (!this._internalTransaction.IsAborted)
						{
							validConnection.ExecuteTransaction(SqlInternalConnection.TransactionRequest.Rollback, null, IsolationLevel.Unspecified, this._internalTransaction, true);
						}
					}
					catch (SqlException)
					{
						validConnection.DoomThisConnection();
					}
					catch (InvalidOperationException)
					{
						validConnection.DoomThisConnection();
					}
				}
				validConnection.CleanupConnectionOnTransactionCompletion(this._atomicTransaction);
				enlistment.Aborted();
			}
			catch (OutOfMemoryException ex)
			{
				connection.Abort(ex);
				throw;
			}
			catch (StackOverflowException ex2)
			{
				connection.Abort(ex2);
				throw;
			}
			catch (ThreadAbortException ex3)
			{
				connection.Abort(ex3);
				throw;
			}
		}

		// Token: 0x0600145C RID: 5212 RVA: 0x000661B0 File Offset: 0x000643B0
		public void SinglePhaseCommit(SinglePhaseEnlistment enlistment)
		{
			SqlInternalConnection validConnection = this.GetValidConnection();
			SqlConnection connection = validConnection.Connection;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				if (validConnection.IsConnectionDoomed)
				{
					SqlInternalConnection sqlInternalConnection = validConnection;
					lock (sqlInternalConnection)
					{
						this._active = false;
						this._connection = null;
					}
					enlistment.Aborted(SQL.ConnectionDoomed());
				}
				else
				{
					SqlInternalConnection sqlInternalConnection = validConnection;
					Exception ex;
					lock (sqlInternalConnection)
					{
						try
						{
							this.ValidateActiveOnConnection(validConnection);
							this._active = false;
							this._connection = null;
							validConnection.ExecuteTransaction(SqlInternalConnection.TransactionRequest.Commit, null, IsolationLevel.Unspecified, this._internalTransaction, true);
							ex = null;
						}
						catch (SqlException ex)
						{
							validConnection.DoomThisConnection();
						}
						catch (InvalidOperationException ex)
						{
							validConnection.DoomThisConnection();
						}
					}
					if (ex != null)
					{
						if (this._internalTransaction.IsCommitted)
						{
							enlistment.Committed();
						}
						else if (this._internalTransaction.IsAborted)
						{
							enlistment.Aborted(ex);
						}
						else
						{
							enlistment.InDoubt(ex);
						}
					}
					validConnection.CleanupConnectionOnTransactionCompletion(this._atomicTransaction);
					if (ex == null)
					{
						enlistment.Committed();
					}
				}
			}
			catch (OutOfMemoryException ex2)
			{
				connection.Abort(ex2);
				throw;
			}
			catch (StackOverflowException ex3)
			{
				connection.Abort(ex3);
				throw;
			}
			catch (ThreadAbortException ex4)
			{
				connection.Abort(ex4);
				throw;
			}
		}

		// Token: 0x0600145D RID: 5213 RVA: 0x00066330 File Offset: 0x00064530
		internal void TransactionEnded(Transaction transaction)
		{
			SqlInternalConnection connection = this._connection;
			if (connection != null)
			{
				SqlInternalConnection sqlInternalConnection = connection;
				lock (sqlInternalConnection)
				{
					if (this._atomicTransaction.Equals(transaction))
					{
						this._active = false;
						this._connection = null;
					}
				}
			}
		}

		// Token: 0x0600145E RID: 5214 RVA: 0x0006638C File Offset: 0x0006458C
		private SqlInternalConnection GetValidConnection()
		{
			SqlInternalConnection connection = this._connection;
			if (connection == null)
			{
				throw ADP.ObjectDisposed(this);
			}
			return connection;
		}

		// Token: 0x0600145F RID: 5215 RVA: 0x000663AC File Offset: 0x000645AC
		private void ValidateActiveOnConnection(SqlInternalConnection connection)
		{
			if (!this._active || connection != this._connection || connection.DelegatedTransaction != this)
			{
				if (connection != null)
				{
					connection.DoomThisConnection();
				}
				if (connection != this._connection && this._connection != null)
				{
					this._connection.DoomThisConnection();
				}
				throw ADP.InternalError(ADP.InternalErrorCode.UnpooledObjectHasWrongOwner);
			}
		}

		// Token: 0x06001460 RID: 5216 RVA: 0x00066404 File Offset: 0x00064604
		private Guid GetGlobalTxnIdentifierFromToken()
		{
			byte[] array = new byte[16];
			Array.Copy(this._connection.PromotedDTCToken, 4, array, 0, array.Length);
			return new Guid(array);
		}

		// Token: 0x04000D8E RID: 3470
		private static int _objectTypeCount;

		// Token: 0x04000D8F RID: 3471
		private readonly int _objectID = Interlocked.Increment(ref SqlDelegatedTransaction._objectTypeCount);

		// Token: 0x04000D90 RID: 3472
		private const int _globalTransactionsTokenVersionSizeInBytes = 4;

		// Token: 0x04000D91 RID: 3473
		private SqlInternalConnection _connection;

		// Token: 0x04000D92 RID: 3474
		private IsolationLevel _isolationLevel;

		// Token: 0x04000D93 RID: 3475
		private SqlInternalTransaction _internalTransaction;

		// Token: 0x04000D94 RID: 3476
		private Transaction _atomicTransaction;

		// Token: 0x04000D95 RID: 3477
		private bool _active;
	}
}
