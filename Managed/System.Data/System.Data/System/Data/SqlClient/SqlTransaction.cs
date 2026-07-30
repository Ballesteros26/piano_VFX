using System;
using System.Data.Common;
using Unity;

namespace System.Data.SqlClient
{
	/// <summary>Represents a Transact-SQL transaction to be made in a SQL Server database. This class cannot be inherited. </summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x020001ED RID: 493
	public sealed class SqlTransaction : DbTransaction
	{
		// Token: 0x060016BA RID: 5818 RVA: 0x000709C4 File Offset: 0x0006EBC4
		internal SqlTransaction(SqlInternalConnection internalConnection, SqlConnection con, IsolationLevel iso, SqlInternalTransaction internalTransaction)
		{
			this._isolationLevel = IsolationLevel.ReadCommitted;
			base..ctor();
			this._isolationLevel = iso;
			this._connection = con;
			if (internalTransaction == null)
			{
				this._internalTransaction = new SqlInternalTransaction(internalConnection, TransactionType.LocalFromAPI, this);
				return;
			}
			this._internalTransaction = internalTransaction;
			this._internalTransaction.InitParent(this);
		}

		/// <summary>Gets the <see cref="T:System.Data.SqlClient.SqlConnection" /> object associated with the transaction, or null if the transaction is no longer valid.</summary>
		/// <returns>The <see cref="T:System.Data.SqlClient.SqlConnection" /> object associated with the transaction.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000468 RID: 1128
		// (get) Token: 0x060016BB RID: 5819 RVA: 0x00070A17 File Offset: 0x0006EC17
		public new SqlConnection Connection
		{
			get
			{
				if (this.IsZombied)
				{
					return null;
				}
				return this._connection;
			}
		}

		// Token: 0x17000469 RID: 1129
		// (get) Token: 0x060016BC RID: 5820 RVA: 0x00070A29 File Offset: 0x0006EC29
		protected override DbConnection DbConnection
		{
			get
			{
				return this.Connection;
			}
		}

		// Token: 0x1700046A RID: 1130
		// (get) Token: 0x060016BD RID: 5821 RVA: 0x00070A31 File Offset: 0x0006EC31
		internal SqlInternalTransaction InternalTransaction
		{
			get
			{
				return this._internalTransaction;
			}
		}

		/// <summary>Specifies the <see cref="T:System.Data.IsolationLevel" /> for this transaction.</summary>
		/// <returns>The <see cref="T:System.Data.IsolationLevel" /> for this transaction. The default is ReadCommitted.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x1700046B RID: 1131
		// (get) Token: 0x060016BE RID: 5822 RVA: 0x00070A39 File Offset: 0x0006EC39
		public override IsolationLevel IsolationLevel
		{
			get
			{
				this.ZombieCheck();
				return this._isolationLevel;
			}
		}

		// Token: 0x1700046C RID: 1132
		// (get) Token: 0x060016BF RID: 5823 RVA: 0x00070A47 File Offset: 0x0006EC47
		private bool IsYukonPartialZombie
		{
			get
			{
				return this._internalTransaction != null && this._internalTransaction.IsCompleted;
			}
		}

		// Token: 0x1700046D RID: 1133
		// (get) Token: 0x060016C0 RID: 5824 RVA: 0x00070A5E File Offset: 0x0006EC5E
		internal bool IsZombied
		{
			get
			{
				return this._internalTransaction == null || this._internalTransaction.IsCompleted;
			}
		}

		// Token: 0x1700046E RID: 1134
		// (get) Token: 0x060016C1 RID: 5825 RVA: 0x00070A75 File Offset: 0x0006EC75
		internal SqlStatistics Statistics
		{
			get
			{
				if (this._connection != null && this._connection.StatisticsEnabled)
				{
					return this._connection.Statistics;
				}
				return null;
			}
		}

		/// <summary>Commits the database transaction.</summary>
		/// <exception cref="T:System.Exception">An error occurred while trying to commit the transaction. </exception>
		/// <exception cref="T:System.InvalidOperationException">The transaction has already been committed or rolled back.-or- The connection is broken. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		///   <IPermission class="System.Security.Permissions.RegistryPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence, ControlPolicy, ControlAppDomain" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Data.SqlClient.SqlClientPermission, System.Data, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060016C2 RID: 5826 RVA: 0x00070A9C File Offset: 0x0006EC9C
		public override void Commit()
		{
			Exception ex = null;
			Guid guid = SqlTransaction.s_diagnosticListener.WriteTransactionCommitBefore(this._isolationLevel, this._connection, "Commit");
			this.ZombieCheck();
			SqlStatistics sqlStatistics = null;
			try
			{
				sqlStatistics = SqlStatistics.StartTimer(this.Statistics);
				this._isFromAPI = true;
				this._internalTransaction.Commit();
			}
			catch (Exception ex)
			{
				throw;
			}
			finally
			{
				if (ex != null)
				{
					SqlTransaction.s_diagnosticListener.WriteTransactionCommitError(guid, this._isolationLevel, this._connection, ex, "Commit");
				}
				else
				{
					SqlTransaction.s_diagnosticListener.WriteTransactionCommitAfter(guid, this._isolationLevel, this._connection, "Commit");
				}
				this._isFromAPI = false;
				SqlStatistics.StopTimer(sqlStatistics);
			}
		}

		// Token: 0x060016C3 RID: 5827 RVA: 0x00070B5C File Offset: 0x0006ED5C
		protected override void Dispose(bool disposing)
		{
			if (disposing && !this.IsZombied && !this.IsYukonPartialZombie)
			{
				this._internalTransaction.Dispose();
			}
			base.Dispose(disposing);
		}

		/// <summary>Rolls back a transaction from a pending state.</summary>
		/// <exception cref="T:System.Exception">An error occurred while trying to commit the transaction. </exception>
		/// <exception cref="T:System.InvalidOperationException">The transaction has already been committed or rolled back.-or- The connection is broken. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		///   <IPermission class="System.Security.Permissions.RegistryPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence, ControlPolicy, ControlAppDomain" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Data.SqlClient.SqlClientPermission, System.Data, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060016C4 RID: 5828 RVA: 0x00070B84 File Offset: 0x0006ED84
		public override void Rollback()
		{
			Exception ex = null;
			Guid guid = SqlTransaction.s_diagnosticListener.WriteTransactionRollbackBefore(this._isolationLevel, this._connection, null, "Rollback");
			if (this.IsYukonPartialZombie)
			{
				this._internalTransaction = null;
				return;
			}
			this.ZombieCheck();
			SqlStatistics sqlStatistics = null;
			try
			{
				sqlStatistics = SqlStatistics.StartTimer(this.Statistics);
				this._isFromAPI = true;
				this._internalTransaction.Rollback();
			}
			catch (Exception ex)
			{
				throw;
			}
			finally
			{
				if (ex != null)
				{
					SqlTransaction.s_diagnosticListener.WriteTransactionRollbackError(guid, this._isolationLevel, this._connection, null, ex, "Rollback");
				}
				else
				{
					SqlTransaction.s_diagnosticListener.WriteTransactionRollbackAfter(guid, this._isolationLevel, this._connection, null, "Rollback");
				}
				this._isFromAPI = false;
				SqlStatistics.StopTimer(sqlStatistics);
			}
		}

		/// <summary>Rolls back a transaction from a pending state, and specifies the transaction or savepoint name.</summary>
		/// <param name="transactionName">The name of the transaction to roll back, or the savepoint to which to roll back. </param>
		/// <exception cref="T:System.ArgumentException">No transaction name was specified. </exception>
		/// <exception cref="T:System.InvalidOperationException">The transaction has already been committed or rolled back.-or- The connection is broken. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060016C5 RID: 5829 RVA: 0x00070C58 File Offset: 0x0006EE58
		public void Rollback(string transactionName)
		{
			Exception ex = null;
			Guid guid = SqlTransaction.s_diagnosticListener.WriteTransactionRollbackBefore(this._isolationLevel, this._connection, transactionName, "Rollback");
			this.ZombieCheck();
			SqlStatistics sqlStatistics = null;
			try
			{
				sqlStatistics = SqlStatistics.StartTimer(this.Statistics);
				this._isFromAPI = true;
				this._internalTransaction.Rollback(transactionName);
			}
			catch (Exception ex)
			{
				throw;
			}
			finally
			{
				if (ex != null)
				{
					SqlTransaction.s_diagnosticListener.WriteTransactionRollbackError(guid, this._isolationLevel, this._connection, transactionName, ex, "Rollback");
				}
				else
				{
					SqlTransaction.s_diagnosticListener.WriteTransactionRollbackAfter(guid, this._isolationLevel, this._connection, transactionName, "Rollback");
				}
				this._isFromAPI = false;
				SqlStatistics.StopTimer(sqlStatistics);
			}
		}

		/// <summary>Creates a savepoint in the transaction that can be used to roll back a part of the transaction, and specifies the savepoint name.</summary>
		/// <param name="savePointName">The name of the savepoint. </param>
		/// <exception cref="T:System.Exception">An error occurred while trying to commit the transaction. </exception>
		/// <exception cref="T:System.InvalidOperationException">The transaction has already been committed or rolled back.-or- The connection is broken. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060016C6 RID: 5830 RVA: 0x00070D1C File Offset: 0x0006EF1C
		public void Save(string savePointName)
		{
			this.ZombieCheck();
			SqlStatistics sqlStatistics = null;
			try
			{
				sqlStatistics = SqlStatistics.StartTimer(this.Statistics);
				this._internalTransaction.Save(savePointName);
			}
			finally
			{
				SqlStatistics.StopTimer(sqlStatistics);
			}
		}

		// Token: 0x060016C7 RID: 5831 RVA: 0x00070D64 File Offset: 0x0006EF64
		internal void Zombie()
		{
			if (!(this._connection.InnerConnection is SqlInternalConnection) || this._isFromAPI)
			{
				this._internalTransaction = null;
			}
		}

		// Token: 0x060016C8 RID: 5832 RVA: 0x00070D87 File Offset: 0x0006EF87
		private void ZombieCheck()
		{
			if (this.IsZombied)
			{
				if (this.IsYukonPartialZombie)
				{
					this._internalTransaction = null;
				}
				throw ADP.TransactionZombied(this);
			}
		}

		// Token: 0x060016CA RID: 5834 RVA: 0x00010468 File Offset: 0x0000E668
		internal SqlTransaction()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04000F0F RID: 3855
		private static readonly DiagnosticListener s_diagnosticListener = new DiagnosticListener("SqlClientDiagnosticListener");

		// Token: 0x04000F10 RID: 3856
		internal readonly IsolationLevel _isolationLevel;

		// Token: 0x04000F11 RID: 3857
		private SqlInternalTransaction _internalTransaction;

		// Token: 0x04000F12 RID: 3858
		private SqlConnection _connection;

		// Token: 0x04000F13 RID: 3859
		private bool _isFromAPI;
	}
}
