using System;
using System.Data.Common;
using System.Data.ProviderBase;
using System.Threading;
using System.Transactions;

namespace System.Data.SqlClient
{
	// Token: 0x020001C4 RID: 452
	internal abstract class SqlInternalConnection : DbConnectionInternal
	{
		// Token: 0x170003ED RID: 1005
		// (get) Token: 0x060014F6 RID: 5366 RVA: 0x0006A019 File Offset: 0x00068219
		// (set) Token: 0x060014F7 RID: 5367 RVA: 0x0006A021 File Offset: 0x00068221
		internal string CurrentDatabase { get; set; }

		// Token: 0x170003EE RID: 1006
		// (get) Token: 0x060014F8 RID: 5368 RVA: 0x0006A02A File Offset: 0x0006822A
		// (set) Token: 0x060014F9 RID: 5369 RVA: 0x0006A032 File Offset: 0x00068232
		internal string CurrentDataSource { get; set; }

		// Token: 0x170003EF RID: 1007
		// (get) Token: 0x060014FA RID: 5370 RVA: 0x0006A03B File Offset: 0x0006823B
		// (set) Token: 0x060014FB RID: 5371 RVA: 0x0006A043 File Offset: 0x00068243
		internal SqlDelegatedTransaction DelegatedTransaction { get; set; }

		// Token: 0x060014FC RID: 5372 RVA: 0x0006A04C File Offset: 0x0006824C
		internal SqlInternalConnection(SqlConnectionString connectionOptions)
		{
			this._connectionOptions = connectionOptions;
		}

		// Token: 0x170003F0 RID: 1008
		// (get) Token: 0x060014FD RID: 5373 RVA: 0x0006A05B File Offset: 0x0006825B
		internal SqlConnection Connection
		{
			get
			{
				return (SqlConnection)base.Owner;
			}
		}

		// Token: 0x170003F1 RID: 1009
		// (get) Token: 0x060014FE RID: 5374 RVA: 0x0006A068 File Offset: 0x00068268
		internal SqlConnectionString ConnectionOptions
		{
			get
			{
				return this._connectionOptions;
			}
		}

		// Token: 0x170003F2 RID: 1010
		// (get) Token: 0x060014FF RID: 5375
		internal abstract SqlInternalTransaction CurrentTransaction { get; }

		// Token: 0x170003F3 RID: 1011
		// (get) Token: 0x06001500 RID: 5376 RVA: 0x0006A070 File Offset: 0x00068270
		internal virtual SqlInternalTransaction AvailableInternalTransaction
		{
			get
			{
				return this.CurrentTransaction;
			}
		}

		// Token: 0x170003F4 RID: 1012
		// (get) Token: 0x06001501 RID: 5377
		internal abstract SqlInternalTransaction PendingTransaction { get; }

		// Token: 0x170003F5 RID: 1013
		// (get) Token: 0x06001502 RID: 5378 RVA: 0x0006A078 File Offset: 0x00068278
		protected internal override bool IsNonPoolableTransactionRoot
		{
			get
			{
				return this.IsTransactionRoot;
			}
		}

		// Token: 0x170003F6 RID: 1014
		// (get) Token: 0x06001503 RID: 5379 RVA: 0x0006A080 File Offset: 0x00068280
		internal override bool IsTransactionRoot
		{
			get
			{
				SqlDelegatedTransaction delegatedTransaction = this.DelegatedTransaction;
				return delegatedTransaction != null && delegatedTransaction.IsActive;
			}
		}

		// Token: 0x170003F7 RID: 1015
		// (get) Token: 0x06001504 RID: 5380 RVA: 0x0006A0A0 File Offset: 0x000682A0
		internal bool HasLocalTransaction
		{
			get
			{
				SqlInternalTransaction currentTransaction = this.CurrentTransaction;
				return currentTransaction != null && currentTransaction.IsLocal;
			}
		}

		// Token: 0x170003F8 RID: 1016
		// (get) Token: 0x06001505 RID: 5381 RVA: 0x0006A0C0 File Offset: 0x000682C0
		internal bool HasLocalTransactionFromAPI
		{
			get
			{
				SqlInternalTransaction currentTransaction = this.CurrentTransaction;
				return currentTransaction != null && currentTransaction.HasParentTransaction;
			}
		}

		// Token: 0x170003F9 RID: 1017
		// (get) Token: 0x06001506 RID: 5382 RVA: 0x0006A0DF File Offset: 0x000682DF
		internal bool IsEnlistedInTransaction
		{
			get
			{
				return this._isEnlistedInTransaction;
			}
		}

		// Token: 0x170003FA RID: 1018
		// (get) Token: 0x06001507 RID: 5383
		internal abstract bool IsLockedForBulkCopy { get; }

		// Token: 0x170003FB RID: 1019
		// (get) Token: 0x06001508 RID: 5384
		internal abstract bool IsKatmaiOrNewer { get; }

		// Token: 0x170003FC RID: 1020
		// (get) Token: 0x06001509 RID: 5385 RVA: 0x0006A0E7 File Offset: 0x000682E7
		// (set) Token: 0x0600150A RID: 5386 RVA: 0x0006A0EF File Offset: 0x000682EF
		internal byte[] PromotedDTCToken
		{
			get
			{
				return this._promotedDTCToken;
			}
			set
			{
				this._promotedDTCToken = value;
			}
		}

		// Token: 0x170003FD RID: 1021
		// (get) Token: 0x0600150B RID: 5387 RVA: 0x0006A0F8 File Offset: 0x000682F8
		// (set) Token: 0x0600150C RID: 5388 RVA: 0x0006A100 File Offset: 0x00068300
		internal bool IsGlobalTransaction
		{
			get
			{
				return this._isGlobalTransaction;
			}
			set
			{
				this._isGlobalTransaction = value;
			}
		}

		// Token: 0x170003FE RID: 1022
		// (get) Token: 0x0600150D RID: 5389 RVA: 0x0006A109 File Offset: 0x00068309
		// (set) Token: 0x0600150E RID: 5390 RVA: 0x0006A111 File Offset: 0x00068311
		internal bool IsGlobalTransactionsEnabledForServer
		{
			get
			{
				return this._isGlobalTransactionEnabledForServer;
			}
			set
			{
				this._isGlobalTransactionEnabledForServer = value;
			}
		}

		// Token: 0x0600150F RID: 5391 RVA: 0x0006A11A File Offset: 0x0006831A
		public override DbTransaction BeginTransaction(IsolationLevel iso)
		{
			return this.BeginSqlTransaction(iso, null, false);
		}

		// Token: 0x06001510 RID: 5392 RVA: 0x0006A128 File Offset: 0x00068328
		internal virtual SqlTransaction BeginSqlTransaction(IsolationLevel iso, string transactionName, bool shouldReconnect)
		{
			SqlStatistics sqlStatistics = null;
			SqlTransaction sqlTransaction2;
			try
			{
				sqlStatistics = SqlStatistics.StartTimer(this.Connection.Statistics);
				this.ValidateConnectionForExecute(null);
				if (this.HasLocalTransactionFromAPI)
				{
					throw ADP.ParallelTransactionsNotSupported(this.Connection);
				}
				if (iso == IsolationLevel.Unspecified)
				{
					iso = IsolationLevel.ReadCommitted;
				}
				SqlTransaction sqlTransaction = new SqlTransaction(this, this.Connection, iso, this.AvailableInternalTransaction);
				sqlTransaction.InternalTransaction.RestoreBrokenConnection = shouldReconnect;
				this.ExecuteTransaction(SqlInternalConnection.TransactionRequest.Begin, transactionName, iso, sqlTransaction.InternalTransaction, false);
				sqlTransaction.InternalTransaction.RestoreBrokenConnection = false;
				sqlTransaction2 = sqlTransaction;
			}
			finally
			{
				SqlStatistics.StopTimer(sqlStatistics);
			}
			return sqlTransaction2;
		}

		// Token: 0x06001511 RID: 5393 RVA: 0x0006A1C8 File Offset: 0x000683C8
		public override void ChangeDatabase(string database)
		{
			if (string.IsNullOrEmpty(database))
			{
				throw ADP.EmptyDatabaseName();
			}
			this.ValidateConnectionForExecute(null);
			this.ChangeDatabaseInternal(database);
		}

		// Token: 0x06001512 RID: 5394
		protected abstract void ChangeDatabaseInternal(string database);

		// Token: 0x06001513 RID: 5395 RVA: 0x0006A1E8 File Offset: 0x000683E8
		protected override void CleanupTransactionOnCompletion(Transaction transaction)
		{
			SqlDelegatedTransaction delegatedTransaction = this.DelegatedTransaction;
			if (delegatedTransaction != null)
			{
				delegatedTransaction.TransactionEnded(transaction);
			}
		}

		// Token: 0x06001514 RID: 5396 RVA: 0x0006A206 File Offset: 0x00068406
		protected override DbReferenceCollection CreateReferenceCollection()
		{
			return new SqlReferenceCollection();
		}

		// Token: 0x06001515 RID: 5397 RVA: 0x0006A210 File Offset: 0x00068410
		protected override void Deactivate()
		{
			try
			{
				SqlReferenceCollection sqlReferenceCollection = (SqlReferenceCollection)base.ReferenceCollection;
				if (sqlReferenceCollection != null)
				{
					sqlReferenceCollection.Deactivate();
				}
				this.InternalDeactivate();
			}
			catch (Exception ex)
			{
				if (!ADP.IsCatchableExceptionType(ex))
				{
					throw;
				}
				base.DoomThisConnection();
			}
		}

		// Token: 0x06001516 RID: 5398
		internal abstract void DisconnectTransaction(SqlInternalTransaction internalTransaction);

		// Token: 0x06001517 RID: 5399 RVA: 0x0006A25C File Offset: 0x0006845C
		public override void Dispose()
		{
			this._whereAbouts = null;
			base.Dispose();
		}

		// Token: 0x06001518 RID: 5400 RVA: 0x0006A26C File Offset: 0x0006846C
		protected void Enlist(Transaction tx)
		{
			if (null == tx)
			{
				if (this.IsEnlistedInTransaction)
				{
					this.EnlistNull();
					return;
				}
				Transaction enlistedTransaction = base.EnlistedTransaction;
				if (enlistedTransaction != null && enlistedTransaction.TransactionInformation.Status != TransactionStatus.Active)
				{
					this.EnlistNull();
					return;
				}
			}
			else if (!tx.Equals(base.EnlistedTransaction))
			{
				this.EnlistNonNull(tx);
			}
		}

		// Token: 0x06001519 RID: 5401 RVA: 0x0006A2CC File Offset: 0x000684CC
		private void EnlistNonNull(Transaction tx)
		{
			bool flag = false;
			SqlDelegatedTransaction sqlDelegatedTransaction = new SqlDelegatedTransaction(this, tx);
			try
			{
				if (this._isGlobalTransaction)
				{
					if (SysTxForGlobalTransactions.EnlistPromotableSinglePhase == null)
					{
						flag = tx.EnlistPromotableSinglePhase(sqlDelegatedTransaction);
					}
					else
					{
						flag = (bool)SysTxForGlobalTransactions.EnlistPromotableSinglePhase.Invoke(tx, new object[]
						{
							sqlDelegatedTransaction,
							SqlInternalConnection._globalTransactionTMID
						});
					}
				}
				else
				{
					flag = tx.EnlistPromotableSinglePhase(sqlDelegatedTransaction);
				}
				if (flag)
				{
					this.DelegatedTransaction = sqlDelegatedTransaction;
				}
			}
			catch (SqlException ex)
			{
				if (ex.Class >= 20)
				{
					throw;
				}
				SqlInternalConnectionTds sqlInternalConnectionTds = this as SqlInternalConnectionTds;
				if (sqlInternalConnectionTds != null)
				{
					TdsParser parser = sqlInternalConnectionTds.Parser;
					if (parser == null || parser.State != TdsParserState.OpenLoggedIn)
					{
						throw;
					}
				}
			}
			if (!flag)
			{
				byte[] array;
				if (this._isGlobalTransaction)
				{
					if (SysTxForGlobalTransactions.GetPromotedToken == null)
					{
						throw SQL.UnsupportedSysTxForGlobalTransactions();
					}
					array = (byte[])SysTxForGlobalTransactions.GetPromotedToken.Invoke(tx, null);
				}
				else
				{
					if (this._whereAbouts == null)
					{
						byte[] dtcaddress = this.GetDTCAddress();
						if (dtcaddress == null)
						{
							throw SQL.CannotGetDTCAddress();
						}
						this._whereAbouts = dtcaddress;
					}
					array = SqlInternalConnection.GetTransactionCookie(tx, this._whereAbouts);
				}
				this.PropagateTransactionCookie(array);
				this._isEnlistedInTransaction = true;
			}
			base.EnlistedTransaction = tx;
		}

		// Token: 0x0600151A RID: 5402 RVA: 0x0006A3F8 File Offset: 0x000685F8
		internal void EnlistNull()
		{
			this.PropagateTransactionCookie(null);
			this._isEnlistedInTransaction = false;
			base.EnlistedTransaction = null;
		}

		// Token: 0x0600151B RID: 5403 RVA: 0x0006A410 File Offset: 0x00068610
		public override void EnlistTransaction(Transaction transaction)
		{
			this.ValidateConnectionForExecute(null);
			if (this.HasLocalTransaction)
			{
				throw ADP.LocalTransactionPresent();
			}
			if (null != transaction && transaction.Equals(base.EnlistedTransaction))
			{
				return;
			}
			try
			{
				this.Enlist(transaction);
			}
			catch (OutOfMemoryException ex)
			{
				this.Connection.Abort(ex);
				throw;
			}
			catch (StackOverflowException ex2)
			{
				this.Connection.Abort(ex2);
				throw;
			}
			catch (ThreadAbortException ex3)
			{
				this.Connection.Abort(ex3);
				throw;
			}
		}

		// Token: 0x0600151C RID: 5404
		internal abstract void ExecuteTransaction(SqlInternalConnection.TransactionRequest transactionRequest, string name, IsolationLevel iso, SqlInternalTransaction internalTransaction, bool isDelegateControlRequest);

		// Token: 0x0600151D RID: 5405 RVA: 0x0006A4AC File Offset: 0x000686AC
		internal SqlDataReader FindLiveReader(SqlCommand command)
		{
			SqlDataReader sqlDataReader = null;
			SqlReferenceCollection sqlReferenceCollection = (SqlReferenceCollection)base.ReferenceCollection;
			if (sqlReferenceCollection != null)
			{
				sqlDataReader = sqlReferenceCollection.FindLiveReader(command);
			}
			return sqlDataReader;
		}

		// Token: 0x0600151E RID: 5406 RVA: 0x0006A4D4 File Offset: 0x000686D4
		internal SqlCommand FindLiveCommand(TdsParserStateObject stateObj)
		{
			SqlCommand sqlCommand = null;
			SqlReferenceCollection sqlReferenceCollection = (SqlReferenceCollection)base.ReferenceCollection;
			if (sqlReferenceCollection != null)
			{
				sqlCommand = sqlReferenceCollection.FindLiveCommand(stateObj);
			}
			return sqlCommand;
		}

		// Token: 0x0600151F RID: 5407
		protected abstract byte[] GetDTCAddress();

		// Token: 0x06001520 RID: 5408 RVA: 0x0006A4FC File Offset: 0x000686FC
		private static byte[] GetTransactionCookie(Transaction transaction, byte[] whereAbouts)
		{
			byte[] array = null;
			if (null != transaction)
			{
				array = TransactionInterop.GetExportCookie(transaction, whereAbouts);
			}
			return array;
		}

		// Token: 0x06001521 RID: 5409 RVA: 0x00005E03 File Offset: 0x00004003
		protected virtual void InternalDeactivate()
		{
		}

		// Token: 0x06001522 RID: 5410 RVA: 0x0006A520 File Offset: 0x00068720
		internal void OnError(SqlException exception, bool breakConnection, Action<Action> wrapCloseInAction = null)
		{
			if (breakConnection)
			{
				base.DoomThisConnection();
			}
			SqlConnection connection = this.Connection;
			if (connection != null)
			{
				connection.OnError(exception, breakConnection, wrapCloseInAction);
				return;
			}
			if (exception.Class >= 11)
			{
				throw exception;
			}
		}

		// Token: 0x06001523 RID: 5411
		protected abstract void PropagateTransactionCookie(byte[] transactionCookie);

		// Token: 0x06001524 RID: 5412
		internal abstract void ValidateConnectionForExecute(SqlCommand command);

		// Token: 0x04000E2C RID: 3628
		private readonly SqlConnectionString _connectionOptions;

		// Token: 0x04000E2D RID: 3629
		private bool _isEnlistedInTransaction;

		// Token: 0x04000E2E RID: 3630
		private byte[] _promotedDTCToken;

		// Token: 0x04000E2F RID: 3631
		private byte[] _whereAbouts;

		// Token: 0x04000E30 RID: 3632
		private bool _isGlobalTransaction;

		// Token: 0x04000E31 RID: 3633
		private bool _isGlobalTransactionEnabledForServer;

		// Token: 0x04000E32 RID: 3634
		private static readonly Guid _globalTransactionTMID = new Guid("1c742caf-6680-40ea-9c26-6b6846079764");

		// Token: 0x020001C5 RID: 453
		internal enum TransactionRequest
		{
			// Token: 0x04000E37 RID: 3639
			Begin,
			// Token: 0x04000E38 RID: 3640
			Promote,
			// Token: 0x04000E39 RID: 3641
			Commit,
			// Token: 0x04000E3A RID: 3642
			Rollback,
			// Token: 0x04000E3B RID: 3643
			IfRollback,
			// Token: 0x04000E3C RID: 3644
			Save
		}
	}
}
