using System;
using System.Data.Common;
using Unity;

namespace System.Data.Odbc
{
	/// <summary>Represents an SQL transaction to be made at a data source. This class cannot be inherited.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020002B2 RID: 690
	public sealed class OdbcTransaction : DbTransaction
	{
		// Token: 0x06001D84 RID: 7556 RVA: 0x0009176F File Offset: 0x0008F96F
		internal OdbcTransaction(OdbcConnection connection, IsolationLevel isolevel, OdbcConnectionHandle handle)
		{
			this._isolevel = IsolationLevel.Unspecified;
			base..ctor();
			this._connection = connection;
			this._isolevel = isolevel;
			this._handle = handle;
		}

		/// <summary>Gets the <see cref="T:System.Data.Odbc.OdbcConnection" /> object associated with the transaction, or null if the transaction is no longer valid.</summary>
		/// <returns>The <see cref="T:System.Data.Odbc.OdbcConnection" /> object associated with the transaction.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000567 RID: 1383
		// (get) Token: 0x06001D85 RID: 7557 RVA: 0x00091793 File Offset: 0x0008F993
		public new OdbcConnection Connection
		{
			get
			{
				return this._connection;
			}
		}

		// Token: 0x17000568 RID: 1384
		// (get) Token: 0x06001D86 RID: 7558 RVA: 0x0009179B File Offset: 0x0008F99B
		protected override DbConnection DbConnection
		{
			get
			{
				return this.Connection;
			}
		}

		/// <summary>Specifies the <see cref="T:System.Data.IsolationLevel" /> for this transaction.</summary>
		/// <returns>The <see cref="T:System.Data.IsolationLevel" /> for this transaction. The default depends on the underlying ODBC driver.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x17000569 RID: 1385
		// (get) Token: 0x06001D87 RID: 7559 RVA: 0x000917A4 File Offset: 0x0008F9A4
		public override IsolationLevel IsolationLevel
		{
			get
			{
				OdbcConnection connection = this._connection;
				if (connection == null)
				{
					throw ADP.TransactionZombied(this);
				}
				if (IsolationLevel.Unspecified == this._isolevel)
				{
					int connectAttr = connection.GetConnectAttr(ODBC32.SQL_ATTR.TXN_ISOLATION, ODBC32.HANDLER.THROW);
					ODBC32.SQL_TRANSACTION sql_TRANSACTION = (ODBC32.SQL_TRANSACTION)connectAttr;
					switch (sql_TRANSACTION)
					{
					case ODBC32.SQL_TRANSACTION.READ_UNCOMMITTED:
						this._isolevel = IsolationLevel.ReadUncommitted;
						goto IL_0091;
					case ODBC32.SQL_TRANSACTION.READ_COMMITTED:
						this._isolevel = IsolationLevel.ReadCommitted;
						goto IL_0091;
					case (ODBC32.SQL_TRANSACTION)3:
						break;
					case ODBC32.SQL_TRANSACTION.REPEATABLE_READ:
						this._isolevel = IsolationLevel.RepeatableRead;
						goto IL_0091;
					default:
						if (sql_TRANSACTION == ODBC32.SQL_TRANSACTION.SERIALIZABLE)
						{
							this._isolevel = IsolationLevel.Serializable;
							goto IL_0091;
						}
						if (sql_TRANSACTION == ODBC32.SQL_TRANSACTION.SNAPSHOT)
						{
							this._isolevel = IsolationLevel.Snapshot;
							goto IL_0091;
						}
						break;
					}
					throw ODBC.NoMappingForSqlTransactionLevel(connectAttr);
				}
				IL_0091:
				return this._isolevel;
			}
		}

		/// <summary>Commits the database transaction.</summary>
		/// <exception cref="T:System.Exception">An error occurred while trying to commit the transaction. </exception>
		/// <exception cref="T:System.InvalidOperationException">The transaction has already been committed or rolled back.-or- The connection is broken. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06001D88 RID: 7560 RVA: 0x00091848 File Offset: 0x0008FA48
		public override void Commit()
		{
			OdbcConnection connection = this._connection;
			if (connection == null)
			{
				throw ADP.TransactionZombied(this);
			}
			connection.CheckState("CommitTransaction");
			if (this._handle == null)
			{
				throw ODBC.NotInTransaction();
			}
			ODBC32.RetCode retCode = this._handle.CompleteTransaction(0);
			if (retCode == ODBC32.RetCode.ERROR)
			{
				connection.HandleError(this._handle, retCode);
			}
			connection.LocalTransaction = null;
			this._connection = null;
			this._handle = null;
		}

		// Token: 0x06001D89 RID: 7561 RVA: 0x000918B4 File Offset: 0x0008FAB4
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				OdbcConnectionHandle handle = this._handle;
				this._handle = null;
				if (handle != null)
				{
					try
					{
						ODBC32.RetCode retCode = handle.CompleteTransaction(1);
						if (retCode == ODBC32.RetCode.ERROR && this._connection != null)
						{
							ADP.TraceExceptionWithoutRethrow(this._connection.HandleErrorNoThrow(handle, retCode));
						}
					}
					catch (Exception ex)
					{
						if (!ADP.IsCatchableExceptionType(ex))
						{
							throw;
						}
					}
				}
				if (this._connection != null && this._connection.IsOpen)
				{
					this._connection.LocalTransaction = null;
				}
				this._connection = null;
				this._isolevel = IsolationLevel.Unspecified;
			}
			base.Dispose(disposing);
		}

		/// <summary>Rolls back a transaction from a pending state.</summary>
		/// <exception cref="T:System.Exception">An error occurred while trying to commit the transaction. </exception>
		/// <exception cref="T:System.InvalidOperationException">The transaction has already been committed or rolled back.-or- The connection is broken.</exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06001D8A RID: 7562 RVA: 0x00091950 File Offset: 0x0008FB50
		public override void Rollback()
		{
			OdbcConnection connection = this._connection;
			if (connection == null)
			{
				throw ADP.TransactionZombied(this);
			}
			connection.CheckState("RollbackTransaction");
			if (this._handle == null)
			{
				throw ODBC.NotInTransaction();
			}
			ODBC32.RetCode retCode = this._handle.CompleteTransaction(1);
			if (retCode == ODBC32.RetCode.ERROR)
			{
				connection.HandleError(this._handle, retCode);
			}
			connection.LocalTransaction = null;
			this._connection = null;
			this._handle = null;
		}

		// Token: 0x06001D8B RID: 7563 RVA: 0x00010468 File Offset: 0x0000E668
		internal OdbcTransaction()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04001586 RID: 5510
		private OdbcConnection _connection;

		// Token: 0x04001587 RID: 5511
		private IsolationLevel _isolevel;

		// Token: 0x04001588 RID: 5512
		private OdbcConnectionHandle _handle;
	}
}
