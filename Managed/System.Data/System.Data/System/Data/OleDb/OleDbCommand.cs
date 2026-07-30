using System;
using System.Data.Common;

namespace System.Data.OleDb
{
	/// <summary>Represents an SQL statement or stored procedure to execute against a data source.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000122 RID: 290
	[MonoTODO("OleDb is not implemented.")]
	public sealed class OleDbCommand : DbCommand, IDbCommand, IDisposable, ICloneable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.OleDb.OleDbCommand" /> class.</summary>
		// Token: 0x06000EC6 RID: 3782 RVA: 0x00050D3B File Offset: 0x0004EF3B
		public OleDbCommand()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.OleDb.OleDbCommand" /> class with the text of the query.</summary>
		/// <param name="cmdText">The text of the query. </param>
		// Token: 0x06000EC7 RID: 3783 RVA: 0x00050D43 File Offset: 0x0004EF43
		public OleDbCommand(string cmdText)
		{
			throw ADP.OleDb();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.OleDb.OleDbCommand" /> class with the text of the query and an <see cref="T:System.Data.OleDb.OleDbConnection" />.</summary>
		/// <param name="cmdText">The text of the query. </param>
		/// <param name="connection">An <see cref="T:System.Data.OleDb.OleDbConnection" /> that represents the connection to a data source. </param>
		// Token: 0x06000EC8 RID: 3784 RVA: 0x00050D43 File Offset: 0x0004EF43
		public OleDbCommand(string cmdText, OleDbConnection connection)
		{
			throw ADP.OleDb();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.OleDb.OleDbCommand" /> class with the text of the query, an <see cref="T:System.Data.OleDb.OleDbConnection" />, and the <see cref="P:System.Data.OleDb.OleDbCommand.Transaction" />.</summary>
		/// <param name="cmdText">The text of the query. </param>
		/// <param name="connection">An <see cref="T:System.Data.OleDb.OleDbConnection" /> that represents the connection to a data source. </param>
		/// <param name="transaction">The transaction in which the <see cref="T:System.Data.OleDb.OleDbCommand" /> executes. </param>
		// Token: 0x06000EC9 RID: 3785 RVA: 0x00050D43 File Offset: 0x0004EF43
		public OleDbCommand(string cmdText, OleDbConnection connection, OleDbTransaction transaction)
		{
			throw ADP.OleDb();
		}

		/// <summary>Gets or sets the SQL statement or stored procedure to execute at the data source.</summary>
		/// <returns>The SQL statement or stored procedure to execute. The default value is an empty string.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000272 RID: 626
		// (get) Token: 0x06000ECA RID: 3786 RVA: 0x00050D50 File Offset: 0x0004EF50
		// (set) Token: 0x06000ECB RID: 3787 RVA: 0x00005E03 File Offset: 0x00004003
		public override string CommandText
		{
			get
			{
				throw ADP.OleDb();
			}
			set
			{
			}
		}

		/// <summary>Gets or sets the wait time before terminating an attempt to execute a command and generating an error.</summary>
		/// <returns>The time (in seconds) to wait for the command to execute. The default is 30 seconds.</returns>
		// Token: 0x17000273 RID: 627
		// (get) Token: 0x06000ECC RID: 3788 RVA: 0x00050D50 File Offset: 0x0004EF50
		// (set) Token: 0x06000ECD RID: 3789 RVA: 0x00005E03 File Offset: 0x00004003
		public override int CommandTimeout
		{
			get
			{
				throw ADP.OleDb();
			}
			set
			{
			}
		}

		/// <summary>Gets or sets a value that indicates how the <see cref="P:System.Data.OleDb.OleDbCommand.CommandText" /> property is interpreted.</summary>
		/// <returns>One of the <see cref="P:System.Data.OleDb.OleDbCommand.CommandType" /> values. The default is Text.</returns>
		/// <exception cref="T:System.ArgumentException">The value was not a valid <see cref="P:System.Data.OleDb.OleDbCommand.CommandType" />.</exception>
		// Token: 0x17000274 RID: 628
		// (get) Token: 0x06000ECE RID: 3790 RVA: 0x00050D50 File Offset: 0x0004EF50
		// (set) Token: 0x06000ECF RID: 3791 RVA: 0x00005E03 File Offset: 0x00004003
		public override CommandType CommandType
		{
			get
			{
				throw ADP.OleDb();
			}
			set
			{
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Data.OleDb.OleDbConnection" /> used by this instance of the <see cref="T:System.Data.OleDb.OleDbCommand" />.</summary>
		/// <returns>The connection to a data source. The default value is null.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Data.OleDb.OleDbCommand.Connection" /> property was changed while a transaction was in progress. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000275 RID: 629
		// (get) Token: 0x06000ED0 RID: 3792 RVA: 0x00050D50 File Offset: 0x0004EF50
		// (set) Token: 0x06000ED1 RID: 3793 RVA: 0x00005E03 File Offset: 0x00004003
		public new OleDbConnection Connection
		{
			get
			{
				throw ADP.OleDb();
			}
			set
			{
			}
		}

		// Token: 0x17000276 RID: 630
		// (get) Token: 0x06000ED2 RID: 3794 RVA: 0x00050D50 File Offset: 0x0004EF50
		// (set) Token: 0x06000ED3 RID: 3795 RVA: 0x00005E03 File Offset: 0x00004003
		protected override DbConnection DbConnection
		{
			get
			{
				throw ADP.OleDb();
			}
			set
			{
			}
		}

		// Token: 0x17000277 RID: 631
		// (get) Token: 0x06000ED4 RID: 3796 RVA: 0x00050D50 File Offset: 0x0004EF50
		protected override DbParameterCollection DbParameterCollection
		{
			get
			{
				throw ADP.OleDb();
			}
		}

		// Token: 0x17000278 RID: 632
		// (get) Token: 0x06000ED5 RID: 3797 RVA: 0x00050D50 File Offset: 0x0004EF50
		// (set) Token: 0x06000ED6 RID: 3798 RVA: 0x00005E03 File Offset: 0x00004003
		protected override DbTransaction DbTransaction
		{
			get
			{
				throw ADP.OleDb();
			}
			set
			{
			}
		}

		/// <summary>Gets or sets a value that indicates whether the command object should be visible in a customized Windows Forms Designer control.</summary>
		/// <returns>A value that indicates whether the command object should be visible in a control. The default is true.</returns>
		// Token: 0x17000279 RID: 633
		// (get) Token: 0x06000ED7 RID: 3799 RVA: 0x00050D50 File Offset: 0x0004EF50
		// (set) Token: 0x06000ED8 RID: 3800 RVA: 0x00005E03 File Offset: 0x00004003
		public override bool DesignTimeVisible
		{
			get
			{
				throw ADP.OleDb();
			}
			set
			{
			}
		}

		/// <summary>Gets the <see cref="T:System.Data.OleDb.OleDbParameterCollection" />.</summary>
		/// <returns>The parameters of the SQL statement or stored procedure. The default is an empty collection.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700027A RID: 634
		// (get) Token: 0x06000ED9 RID: 3801 RVA: 0x00050D50 File Offset: 0x0004EF50
		public new OleDbParameterCollection Parameters
		{
			get
			{
				throw ADP.OleDb();
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Data.OleDb.OleDbTransaction" /> within which the <see cref="T:System.Data.OleDb.OleDbCommand" /> executes.</summary>
		/// <returns>The <see cref="T:System.Data.OleDb.OleDbTransaction" />. The default value is null.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700027B RID: 635
		// (get) Token: 0x06000EDA RID: 3802 RVA: 0x00050D50 File Offset: 0x0004EF50
		// (set) Token: 0x06000EDB RID: 3803 RVA: 0x00005E03 File Offset: 0x00004003
		public new OleDbTransaction Transaction
		{
			get
			{
				throw ADP.OleDb();
			}
			set
			{
			}
		}

		/// <summary>Gets or sets how command results are applied to the <see cref="T:System.Data.DataRow" /> when used by the Update method of the <see cref="T:System.Data.OleDb.OleDbDataAdapter" />.</summary>
		/// <returns>One of the <see cref="T:System.Data.UpdateRowSource" /> values.</returns>
		/// <exception cref="T:System.ArgumentException">The value entered was not one of the <see cref="T:System.Data.UpdateRowSource" /> values.</exception>
		// Token: 0x1700027C RID: 636
		// (get) Token: 0x06000EDC RID: 3804 RVA: 0x00050D50 File Offset: 0x0004EF50
		// (set) Token: 0x06000EDD RID: 3805 RVA: 0x00005E03 File Offset: 0x00004003
		public override UpdateRowSource UpdatedRowSource
		{
			get
			{
				throw ADP.OleDb();
			}
			set
			{
			}
		}

		/// <summary>Tries to cancel the execution of an <see cref="T:System.Data.OleDb.OleDbCommand" />.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000EDE RID: 3806 RVA: 0x00005E03 File Offset: 0x00004003
		public override void Cancel()
		{
		}

		/// <summary>Creates a new <see cref="T:System.Data.OleDb.OleDbCommand" /> object that is a copy of the current instance.</summary>
		/// <returns>A new <see cref="T:System.Data.OleDb.OleDbCommand" /> object that is a copy of this instance.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000EDF RID: 3807 RVA: 0x00050D50 File Offset: 0x0004EF50
		public OleDbCommand Clone()
		{
			throw ADP.OleDb();
		}

		// Token: 0x06000EE0 RID: 3808 RVA: 0x00050D50 File Offset: 0x0004EF50
		protected override DbParameter CreateDbParameter()
		{
			throw ADP.OleDb();
		}

		/// <summary>Creates a new instance of an <see cref="T:System.Data.OleDb.OleDbParameter" /> object.</summary>
		/// <returns>An <see cref="T:System.Data.OleDb.OleDbParameter" /> object.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000EE1 RID: 3809 RVA: 0x00050D50 File Offset: 0x0004EF50
		public new OleDbParameter CreateParameter()
		{
			throw ADP.OleDb();
		}

		// Token: 0x06000EE2 RID: 3810 RVA: 0x00050D50 File Offset: 0x0004EF50
		protected override void Dispose(bool disposing)
		{
			throw ADP.OleDb();
		}

		// Token: 0x06000EE3 RID: 3811 RVA: 0x00050D50 File Offset: 0x0004EF50
		protected override DbDataReader ExecuteDbDataReader(CommandBehavior behavior)
		{
			throw ADP.OleDb();
		}

		/// <summary>Executes an SQL statement against the <see cref="P:System.Data.OleDb.OleDbCommand.Connection" /> and returns the number of rows affected.</summary>
		/// <returns>The number of rows affected.</returns>
		/// <exception cref="T:System.InvalidOperationException">The connection does not exist.-or- The connection is not open.-or- Cannot execute a command within a transaction context that differs from the context in which the connection was originally enlisted. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence, ControlPolicy" />
		///   <IPermission class="System.Security.Permissions.UIPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Window="SafeSubWindows" />
		///   <IPermission class="System.Data.OleDb.OleDbPermission, System.Data, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000EE4 RID: 3812 RVA: 0x00050D50 File Offset: 0x0004EF50
		public override int ExecuteNonQuery()
		{
			throw ADP.OleDb();
		}

		/// <summary>Sends the <see cref="P:System.Data.OleDb.OleDbCommand.CommandText" /> to the <see cref="P:System.Data.OleDb.OleDbCommand.Connection" /> and builds an <see cref="T:System.Data.OleDb.OleDbDataReader" />.</summary>
		/// <returns>An <see cref="T:System.Data.OleDb.OleDbDataReader" /> object.</returns>
		/// <exception cref="T:System.InvalidOperationException">Cannot execute a command within a transaction context that differs from the context in which the connection was originally enlisted. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence, ControlPolicy" />
		///   <IPermission class="System.Security.Permissions.UIPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Window="SafeSubWindows" />
		///   <IPermission class="System.Data.OleDb.OleDbPermission, System.Data, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000EE5 RID: 3813 RVA: 0x00050D50 File Offset: 0x0004EF50
		public new OleDbDataReader ExecuteReader()
		{
			throw ADP.OleDb();
		}

		/// <summary>Sends the <see cref="P:System.Data.OleDb.OleDbCommand.CommandText" /> to the <see cref="P:System.Data.OleDb.OleDbCommand.Connection" />, and builds an <see cref="T:System.Data.OleDb.OleDbDataReader" /> using one of the <see cref="T:System.Data.CommandBehavior" /> values.</summary>
		/// <returns>An <see cref="T:System.Data.OleDb.OleDbDataReader" /> object.</returns>
		/// <param name="behavior">One of the <see cref="T:System.Data.CommandBehavior" /> values. </param>
		/// <exception cref="T:System.InvalidOperationException">Cannot execute a command within a transaction context that differs from the context in which the connection was originally enlisted. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence, ControlPolicy" />
		///   <IPermission class="System.Security.Permissions.UIPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Window="SafeSubWindows" />
		///   <IPermission class="System.Data.OleDb.OleDbPermission, System.Data, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000EE6 RID: 3814 RVA: 0x00050D50 File Offset: 0x0004EF50
		public new OleDbDataReader ExecuteReader(CommandBehavior behavior)
		{
			throw ADP.OleDb();
		}

		/// <summary>Executes the query, and returns the first column of the first row in the result set returned by the query. Additional columns or rows are ignored.</summary>
		/// <returns>The first column of the first row in the result set, or a null reference if the result set is empty.</returns>
		/// <exception cref="T:System.InvalidOperationException">Cannot execute a command within a transaction context that differs from the context in which the connection was originally enlisted. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence, ControlPolicy" />
		///   <IPermission class="System.Security.Permissions.UIPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Window="SafeSubWindows" />
		///   <IPermission class="System.Data.OleDb.OleDbPermission, System.Data, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000EE7 RID: 3815 RVA: 0x00050D50 File Offset: 0x0004EF50
		public override object ExecuteScalar()
		{
			throw ADP.OleDb();
		}

		/// <summary>Creates a prepared (or compiled) version of the command on the data source.</summary>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Data.OleDb.OleDbCommand.Connection" /> is not set.-or- The <see cref="P:System.Data.OleDb.OleDbCommand.Connection" /> is not open. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000EE8 RID: 3816 RVA: 0x00050D50 File Offset: 0x0004EF50
		public override void Prepare()
		{
			throw ADP.OleDb();
		}

		/// <summary>Resets the <see cref="P:System.Data.OleDb.OleDbCommand.CommandTimeout" /> property to the default value.</summary>
		// Token: 0x06000EE9 RID: 3817 RVA: 0x00050D50 File Offset: 0x0004EF50
		public void ResetCommandTimeout()
		{
			throw ADP.OleDb();
		}

		/// <summary>For a description of this member, see <see cref="M:System.Data.IDbCommand.ExecuteReader" />.</summary>
		/// <returns>An <see cref="T:System.Data.IDataReader" /> object.</returns>
		// Token: 0x06000EEA RID: 3818 RVA: 0x00050D50 File Offset: 0x0004EF50
		IDataReader IDbCommand.ExecuteReader()
		{
			throw ADP.OleDb();
		}

		/// <summary>Executes the <see cref="P:System.Data.IDbCommand.CommandText" /> against the <see cref="P:System.Data.IDbCommand.Connection" />, and builds an <see cref="T:System.Data.IDataReader" /> using one of the <see cref="T:System.Data.CommandBehavior" /> values.</summary>
		/// <returns>An <see cref="T:System.Data.IDataReader" /> built using one of the <see cref="T:System.Data.CommandBehavior" /> values.</returns>
		/// <param name="behavior">One of the <see cref="T:System.Data.CommandBehavior" /> values.</param>
		// Token: 0x06000EEB RID: 3819 RVA: 0x00050D50 File Offset: 0x0004EF50
		IDataReader IDbCommand.ExecuteReader(CommandBehavior behavior)
		{
			throw ADP.OleDb();
		}

		/// <summary>For a description of this member, see <see cref="M:System.ICloneable.Clone" />.</summary>
		/// <returns>A new <see cref="T:System.Object" /> that is a copy of this instance.</returns>
		// Token: 0x06000EEC RID: 3820 RVA: 0x00050D50 File Offset: 0x0004EF50
		object ICloneable.Clone()
		{
			throw ADP.OleDb();
		}
	}
}
