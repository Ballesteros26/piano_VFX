using System;
using System.Data.Common;

namespace System.Data.OleDb
{
	/// <summary>Represents an SQL transaction to be made at a data source. This class cannot be inherited. </summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000139 RID: 313
	[MonoTODO("OleDb is not implemented.")]
	public sealed class OleDbTransaction : DbTransaction
	{
		// Token: 0x06000FFD RID: 4093 RVA: 0x00050F96 File Offset: 0x0004F196
		internal OleDbTransaction()
		{
		}

		/// <summary>Gets the <see cref="T:System.Data.OleDb.OleDbConnection" /> object associated with the transaction, or null if the transaction is no longer valid.</summary>
		/// <returns>The <see cref="T:System.Data.OleDb.OleDbConnection" /> object associated with the transaction.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170002C1 RID: 705
		// (get) Token: 0x06000FFE RID: 4094 RVA: 0x00050D50 File Offset: 0x0004EF50
		public new OleDbConnection Connection
		{
			get
			{
				throw ADP.OleDb();
			}
		}

		// Token: 0x170002C2 RID: 706
		// (get) Token: 0x06000FFF RID: 4095 RVA: 0x00050D50 File Offset: 0x0004EF50
		protected override DbConnection DbConnection
		{
			get
			{
				throw ADP.OleDb();
			}
		}

		/// <summary>Specifies the <see cref="T:System.Data.IsolationLevel" /> for this transaction.</summary>
		/// <returns>The <see cref="T:System.Data.IsolationLevel" /> for this transaction. The default is ReadCommitted.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x170002C3 RID: 707
		// (get) Token: 0x06001000 RID: 4096 RVA: 0x00050D50 File Offset: 0x0004EF50
		public override IsolationLevel IsolationLevel
		{
			get
			{
				throw ADP.OleDb();
			}
		}

		/// <summary>Initiates a nested database transaction.</summary>
		/// <returns>A nested database transaction.</returns>
		/// <exception cref="T:System.InvalidOperationException">Nested transactions are not supported. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001001 RID: 4097 RVA: 0x00050D50 File Offset: 0x0004EF50
		public OleDbTransaction Begin()
		{
			throw ADP.OleDb();
		}

		/// <summary>Initiates a nested database transaction and specifies the isolation level to use for the new transaction.</summary>
		/// <returns>A nested database transaction.</returns>
		/// <param name="isolevel">The isolation level to use for the transaction. </param>
		/// <exception cref="T:System.InvalidOperationException">Nested transactions are not supported. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001002 RID: 4098 RVA: 0x00050D50 File Offset: 0x0004EF50
		public OleDbTransaction Begin(IsolationLevel isolevel)
		{
			throw ADP.OleDb();
		}

		/// <summary>Commits the database transaction.</summary>
		/// <exception cref="T:System.Exception">An error occurred while trying to commit the transaction. </exception>
		/// <exception cref="T:System.InvalidOperationException">The transaction has already been committed or rolled back.-or- The connection is broken. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001003 RID: 4099 RVA: 0x00050D50 File Offset: 0x0004EF50
		public override void Commit()
		{
			throw ADP.OleDb();
		}

		// Token: 0x06001004 RID: 4100 RVA: 0x00050D50 File Offset: 0x0004EF50
		protected override void Dispose(bool disposing)
		{
			throw ADP.OleDb();
		}

		/// <summary>Rolls back a transaction from a pending state.</summary>
		/// <exception cref="T:System.Exception">An error occurred while trying to commit the transaction. </exception>
		/// <exception cref="T:System.InvalidOperationException">The transaction has already been committed or rolled back.-or- The connection is broken.</exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001005 RID: 4101 RVA: 0x00050D50 File Offset: 0x0004EF50
		public override void Rollback()
		{
			throw ADP.OleDb();
		}
	}
}
