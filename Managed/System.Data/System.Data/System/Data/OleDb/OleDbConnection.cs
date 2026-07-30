using System;
using System.Data.Common;
using System.EnterpriseServices;
using System.Transactions;

namespace System.Data.OleDb
{
	/// <summary>Represents an open connection to a data source.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000124 RID: 292
	[MonoTODO("OleDb is not implemented.")]
	public sealed class OleDbConnection : DbConnection, IDbConnection, IDisposable, ICloneable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.OleDb.OleDbConnection" /> class.</summary>
		// Token: 0x06000F01 RID: 3841 RVA: 0x00050D64 File Offset: 0x0004EF64
		public OleDbConnection()
		{
			throw ADP.OleDb();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.OleDb.OleDbConnection" /> class with the specified connection string.</summary>
		/// <param name="connectionString">The connection used to open the database. </param>
		// Token: 0x06000F02 RID: 3842 RVA: 0x00050D64 File Offset: 0x0004EF64
		public OleDbConnection(string connectionString)
		{
			throw ADP.OleDb();
		}

		/// <summary>Gets or sets the string used to open a database.</summary>
		/// <returns>The OLE DB provider connection string that includes the data source name, and other parameters needed to establish the initial connection. The default value is an empty string.</returns>
		/// <exception cref="T:System.ArgumentException">An invalid connection string argument has been supplied or a required connection string argument has not been supplied. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700027E RID: 638
		// (get) Token: 0x06000F03 RID: 3843 RVA: 0x00050D50 File Offset: 0x0004EF50
		// (set) Token: 0x06000F04 RID: 3844 RVA: 0x00005E03 File Offset: 0x00004003
		public override string ConnectionString
		{
			get
			{
				throw ADP.OleDb();
			}
			set
			{
			}
		}

		/// <summary>Gets the time to wait while trying to establish a connection before terminating the attempt and generating an error.</summary>
		/// <returns>The time in seconds to wait for a connection to open. The default value is 15 seconds.</returns>
		/// <exception cref="T:System.ArgumentException">The value set is less than 0. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x1700027F RID: 639
		// (get) Token: 0x06000F05 RID: 3845 RVA: 0x00050D50 File Offset: 0x0004EF50
		public override int ConnectionTimeout
		{
			get
			{
				throw ADP.OleDb();
			}
		}

		/// <summary>Gets the name of the current database or the database to be used after a connection is opened.</summary>
		/// <returns>The name of the current database or the name of the database to be used after a connection is opened. The default value is an empty string.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x17000280 RID: 640
		// (get) Token: 0x06000F06 RID: 3846 RVA: 0x00050D50 File Offset: 0x0004EF50
		public override string Database
		{
			get
			{
				throw ADP.OleDb();
			}
		}

		/// <summary>Gets the server name or file name of the data source.</summary>
		/// <returns>The server name or file name of the data source. The default value is an empty string.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x17000281 RID: 641
		// (get) Token: 0x06000F07 RID: 3847 RVA: 0x00050D50 File Offset: 0x0004EF50
		public override string DataSource
		{
			get
			{
				throw ADP.OleDb();
			}
		}

		/// <summary>Gets the name of the OLE DB provider specified in the "Provider= " clause of the connection string.</summary>
		/// <returns>The name of the provider as specified in the "Provider= " clause of the connection string. The default value is an empty string.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000282 RID: 642
		// (get) Token: 0x06000F08 RID: 3848 RVA: 0x00050D50 File Offset: 0x0004EF50
		public string Provider
		{
			get
			{
				throw ADP.OleDb();
			}
		}

		/// <summary>Gets a string that contains the version of the server to which the client is connected.</summary>
		/// <returns>The version of the connected server.</returns>
		/// <exception cref="T:System.InvalidOperationException">The connection is closed. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000283 RID: 643
		// (get) Token: 0x06000F09 RID: 3849 RVA: 0x00050D50 File Offset: 0x0004EF50
		public override string ServerVersion
		{
			get
			{
				throw ADP.OleDb();
			}
		}

		/// <summary>Gets the current state of the connection.</summary>
		/// <returns>A bitwise combination of the <see cref="T:System.Data.ConnectionState" /> values. The default is Closed.</returns>
		// Token: 0x17000284 RID: 644
		// (get) Token: 0x06000F0A RID: 3850 RVA: 0x00050D50 File Offset: 0x0004EF50
		public override ConnectionState State
		{
			get
			{
				throw ADP.OleDb();
			}
		}

		// Token: 0x06000F0B RID: 3851 RVA: 0x00050D50 File Offset: 0x0004EF50
		protected override DbTransaction BeginDbTransaction(IsolationLevel isolationLevel)
		{
			throw ADP.OleDb();
		}

		/// <summary>Starts a database transaction with the current <see cref="T:System.Data.IsolationLevel" /> value.</summary>
		/// <returns>An object representing the new transaction.</returns>
		/// <exception cref="T:System.InvalidOperationException">Parallel transactions are not supported. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000F0C RID: 3852 RVA: 0x00050D50 File Offset: 0x0004EF50
		public new OleDbTransaction BeginTransaction()
		{
			throw ADP.OleDb();
		}

		/// <summary>Starts a database transaction with the specified isolation level.</summary>
		/// <returns>An object representing the new transaction.</returns>
		/// <param name="isolationLevel">The isolation level under which the transaction should run.</param>
		/// <exception cref="T:System.InvalidOperationException">Parallel transactions are not supported. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000F0D RID: 3853 RVA: 0x00050D50 File Offset: 0x0004EF50
		public new OleDbTransaction BeginTransaction(IsolationLevel isolationLevel)
		{
			throw ADP.OleDb();
		}

		/// <summary>Changes the current database for an open <see cref="T:System.Data.OleDb.OleDbConnection" />.</summary>
		/// <param name="value">The database name. </param>
		/// <exception cref="T:System.ArgumentException">The database name is not valid. </exception>
		/// <exception cref="T:System.InvalidOperationException">The connection is not open. </exception>
		/// <exception cref="T:System.Data.OleDb.OleDbException">Cannot change the database. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000F0E RID: 3854 RVA: 0x00050D50 File Offset: 0x0004EF50
		public override void ChangeDatabase(string value)
		{
			throw ADP.OleDb();
		}

		/// <summary>Closes the connection to the data source.</summary>
		// Token: 0x06000F0F RID: 3855 RVA: 0x00050D50 File Offset: 0x0004EF50
		public override void Close()
		{
			throw ADP.OleDb();
		}

		/// <summary>Creates and returns an <see cref="T:System.Data.OleDb.OleDbCommand" /> object associated with the <see cref="T:System.Data.OleDb.OleDbConnection" />.</summary>
		/// <returns>An <see cref="T:System.Data.OleDb.OleDbCommand" /> object.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000F10 RID: 3856 RVA: 0x00050D50 File Offset: 0x0004EF50
		public new OleDbCommand CreateCommand()
		{
			throw ADP.OleDb();
		}

		// Token: 0x06000F11 RID: 3857 RVA: 0x00050D50 File Offset: 0x0004EF50
		protected override DbCommand CreateDbCommand()
		{
			throw ADP.OleDb();
		}

		// Token: 0x06000F12 RID: 3858 RVA: 0x00050D50 File Offset: 0x0004EF50
		protected override void Dispose(bool disposing)
		{
			throw ADP.OleDb();
		}

		/// <summary>Enlists in the specified transaction as a distributed transaction.</summary>
		/// <param name="transaction">A reference to an existing <see cref="T:System.EnterpriseServices.ITransaction" /> in which to enlist.</param>
		// Token: 0x06000F13 RID: 3859 RVA: 0x00050D50 File Offset: 0x0004EF50
		public void EnlistDistributedTransaction(ITransaction transaction)
		{
			throw ADP.OleDb();
		}

		/// <summary>Enlists in the specified transaction as a distributed transaction.</summary>
		/// <param name="transaction">A reference to an existing <see cref="T:System.Transactions.Transaction" /> in which to enlist.</param>
		// Token: 0x06000F14 RID: 3860 RVA: 0x00050D50 File Offset: 0x0004EF50
		public override void EnlistTransaction(Transaction transaction)
		{
			throw ADP.OleDb();
		}

		/// <summary>Returns schema information from a data source as indicated by a GUID, and after it applies the specified restrictions.</summary>
		/// <returns>A <see cref="T:System.Data.DataTable" /> that contains the requested schema information.</returns>
		/// <param name="schema">One of the <see cref="T:System.Data.OleDb.OleDbSchemaGuid" /> values that specifies the schema table to return. </param>
		/// <param name="restrictions">An <see cref="T:System.Object" /> array of restriction values. These are applied in the order of the restriction columns. That is, the first restriction value applies to the first restriction column, the second restriction value applies to the second restriction column, and so on. </param>
		/// <exception cref="T:System.Data.OleDb.OleDbException">The specified set of restrictions is invalid. </exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Data.OleDb.OleDbConnection" /> is closed. </exception>
		/// <exception cref="T:System.ArgumentException">The specified schema rowset is not supported by the OLE DB provider.-or- The <paramref name="schema" /> parameter contains a value of <see cref="F:System.Data.OleDb.OleDbSchemaGuid.DbInfoLiterals" /> and the <paramref name="restrictions" /> parameter contains one or more restrictions. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000F15 RID: 3861 RVA: 0x00050D50 File Offset: 0x0004EF50
		public DataTable GetOleDbSchemaTable(Guid schema, object[] restrictions)
		{
			throw ADP.OleDb();
		}

		/// <summary>Returns schema information for the data source of this <see cref="T:System.Data.OleDb.OleDbConnection" />.</summary>
		/// <returns>A <see cref="T:System.Data.DataTable" /> that contains schema information.</returns>
		// Token: 0x06000F16 RID: 3862 RVA: 0x00050D50 File Offset: 0x0004EF50
		public override DataTable GetSchema()
		{
			throw ADP.OleDb();
		}

		/// <summary>Returns schema information for the data source of this <see cref="T:System.Data.OleDb.OleDbConnection" /> using the specified string for the schema name.</summary>
		/// <returns>A <see cref="T:System.Data.DataTable" /> that contains schema information.</returns>
		/// <param name="collectionName">Specifies the name of the schema to return. </param>
		// Token: 0x06000F17 RID: 3863 RVA: 0x00050D50 File Offset: 0x0004EF50
		public override DataTable GetSchema(string collectionName)
		{
			throw ADP.OleDb();
		}

		/// <summary>Returns schema information for the data source of this <see cref="T:System.Data.OleDb.OleDbConnection" /> using the specified string for the schema name and the specified string array for the restriction values.</summary>
		/// <returns>A <see cref="T:System.Data.DataTable" /> that contains schema information.</returns>
		/// <param name="collectionName">Specifies the name of the schema to return.</param>
		/// <param name="restrictionValues">Specifies a set of restriction values for the requested schema.</param>
		// Token: 0x06000F18 RID: 3864 RVA: 0x00050D50 File Offset: 0x0004EF50
		public override DataTable GetSchema(string collectionName, string[] restrictionValues)
		{
			throw ADP.OleDb();
		}

		/// <summary>Opens a database connection with the property settings specified by the <see cref="P:System.Data.OleDb.OleDbConnection.ConnectionString" />.</summary>
		/// <exception cref="T:System.InvalidOperationException">The connection is already open.</exception>
		/// <exception cref="T:System.Data.OleDb.OleDbException">A connection-level error occurred while opening the connection.</exception>
		// Token: 0x06000F19 RID: 3865 RVA: 0x00050D50 File Offset: 0x0004EF50
		public override void Open()
		{
			throw ADP.OleDb();
		}

		/// <summary>Indicates that the <see cref="T:System.Data.OleDb.OleDbConnection" /> object pool can be released when the last underlying connection is released.</summary>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000F1A RID: 3866 RVA: 0x00050D50 File Offset: 0x0004EF50
		public static void ReleaseObjectPool()
		{
			throw ADP.OleDb();
		}

		/// <summary>Updates the <see cref="P:System.Data.OleDb.OleDbConnection.State" /> property of the <see cref="T:System.Data.OleDb.OleDbConnection" /> object.</summary>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000F1B RID: 3867 RVA: 0x00050D50 File Offset: 0x0004EF50
		public void ResetState()
		{
			throw ADP.OleDb();
		}

		/// <summary>Occurs when the provider sends a warning or an informational message.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x14000020 RID: 32
		// (add) Token: 0x06000F1C RID: 3868 RVA: 0x00050D74 File Offset: 0x0004EF74
		// (remove) Token: 0x06000F1D RID: 3869 RVA: 0x00050DAC File Offset: 0x0004EFAC
		public event OleDbInfoMessageEventHandler InfoMessage;

		/// <summary>For a description of this member, see <see cref="M:System.ICloneable.Clone" />.</summary>
		/// <returns>A new <see cref="T:System.Object" /> that is a copy of this instance.</returns>
		// Token: 0x06000F1E RID: 3870 RVA: 0x00050D50 File Offset: 0x0004EF50
		object ICloneable.Clone()
		{
			throw ADP.OleDb();
		}
	}
}
