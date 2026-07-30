using System;
using System.Data.Common;

namespace System.Data.OleDb
{
	/// <summary>Represents a set of data commands and a database connection that are used to fill the <see cref="T:System.Data.DataSet" /> and update the data source.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000126 RID: 294
	[MonoTODO("OleDb is not implemented.")]
	public sealed class OleDbDataAdapter : DbDataAdapter, IDataAdapter, IDbDataAdapter, ICloneable
	{
		/// <summary>Gets or sets an SQL statement or stored procedure for deleting records from the data set.</summary>
		/// <returns>An <see cref="T:System.Data.OleDb.OleDbCommand" /> used during <see cref="M:System.Data.Common.DbDataAdapter.Update(System.Data.DataSet)" /> to delete records in the data source that correspond to deleted rows in the <see cref="T:System.Data.DataSet" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700028C RID: 652
		// (get) Token: 0x06000F34 RID: 3892 RVA: 0x00050D50 File Offset: 0x0004EF50
		// (set) Token: 0x06000F35 RID: 3893 RVA: 0x00005E03 File Offset: 0x00004003
		public new OleDbCommand DeleteCommand
		{
			get
			{
				throw ADP.OleDb();
			}
			set
			{
			}
		}

		/// <summary>Gets or sets an SQL statement or stored procedure used to insert new records into the data source.</summary>
		/// <returns>An <see cref="T:System.Data.OleDb.OleDbCommand" /> used during <see cref="M:System.Data.Common.DbDataAdapter.Update(System.Data.DataSet)" /> to insert records in the data source that correspond to new rows in the <see cref="T:System.Data.DataSet" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700028D RID: 653
		// (get) Token: 0x06000F36 RID: 3894 RVA: 0x00050D50 File Offset: 0x0004EF50
		// (set) Token: 0x06000F37 RID: 3895 RVA: 0x00005E03 File Offset: 0x00004003
		public new OleDbCommand InsertCommand
		{
			get
			{
				throw ADP.OleDb();
			}
			set
			{
			}
		}

		/// <summary>Gets or sets an SQL statement or stored procedure used to select records in the data source.</summary>
		/// <returns>An <see cref="T:System.Data.OleDb.OleDbCommand" /> that is used during <see cref="M:System.Data.Common.DbDataAdapter.Fill(System.Data.DataSet)" /> to select records from data source for placement in the <see cref="T:System.Data.DataSet" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700028E RID: 654
		// (get) Token: 0x06000F38 RID: 3896 RVA: 0x00050D50 File Offset: 0x0004EF50
		// (set) Token: 0x06000F39 RID: 3897 RVA: 0x00005E03 File Offset: 0x00004003
		public new OleDbCommand SelectCommand
		{
			get
			{
				throw ADP.OleDb();
			}
			set
			{
			}
		}

		/// <summary>For a description of this member, see <see cref="M:System.Data.IDbDataAdapter.DeleteCommand" />.</summary>
		/// <returns>An <see cref="T:System.Data.IDbCommand" /> used during an update to delete records in the data source for deleted rows in the data set.</returns>
		// Token: 0x1700028F RID: 655
		// (get) Token: 0x06000F3A RID: 3898 RVA: 0x00050D50 File Offset: 0x0004EF50
		// (set) Token: 0x06000F3B RID: 3899 RVA: 0x00005E03 File Offset: 0x00004003
		IDbCommand IDbDataAdapter.DeleteCommand
		{
			get
			{
				throw ADP.OleDb();
			}
			set
			{
			}
		}

		/// <summary>For a description of this member, see <see cref="M:System.Data.IDbDataAdapter.InsertCommand" />.</summary>
		/// <returns>An <see cref="T:System.Data.IDbCommand" /> that is used during an update to insert records from a data source for placement in the data set.</returns>
		// Token: 0x17000290 RID: 656
		// (get) Token: 0x06000F3C RID: 3900 RVA: 0x00050D50 File Offset: 0x0004EF50
		// (set) Token: 0x06000F3D RID: 3901 RVA: 0x00005E03 File Offset: 0x00004003
		IDbCommand IDbDataAdapter.InsertCommand
		{
			get
			{
				throw ADP.OleDb();
			}
			set
			{
			}
		}

		/// <summary>For a description of this member, see <see cref="M:System.Data.IDbDataAdapter.SelectCommand" />.</summary>
		/// <returns>An <see cref="T:System.Data.IDbCommand" /> that is used during an update to select records from a data source for placement in the data set.</returns>
		// Token: 0x17000291 RID: 657
		// (get) Token: 0x06000F3E RID: 3902 RVA: 0x00050D50 File Offset: 0x0004EF50
		// (set) Token: 0x06000F3F RID: 3903 RVA: 0x00005E03 File Offset: 0x00004003
		IDbCommand IDbDataAdapter.SelectCommand
		{
			get
			{
				throw ADP.OleDb();
			}
			set
			{
			}
		}

		/// <summary>For a description of this member, see <see cref="M:System.Data.IDbDataAdapter.UpdateCommand" />.</summary>
		/// <returns>An <see cref="T:System.Data.IDbCommand" /> used during an update to update records in the data source for modified rows in the data set.</returns>
		// Token: 0x17000292 RID: 658
		// (get) Token: 0x06000F40 RID: 3904 RVA: 0x00050D50 File Offset: 0x0004EF50
		// (set) Token: 0x06000F41 RID: 3905 RVA: 0x00005E03 File Offset: 0x00004003
		IDbCommand IDbDataAdapter.UpdateCommand
		{
			get
			{
				throw ADP.OleDb();
			}
			set
			{
			}
		}

		/// <summary>Gets or sets an SQL statement or stored procedure used to update records in the data source.</summary>
		/// <returns>An <see cref="T:System.Data.OleDb.OleDbCommand" /> used during <see cref="M:System.Data.Common.DbDataAdapter.Update(System.Data.DataSet)" /> to update records in the data source that correspond to modified rows in the <see cref="T:System.Data.DataSet" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000293 RID: 659
		// (get) Token: 0x06000F42 RID: 3906 RVA: 0x00050D50 File Offset: 0x0004EF50
		// (set) Token: 0x06000F43 RID: 3907 RVA: 0x00005E03 File Offset: 0x00004003
		public new OleDbCommand UpdateCommand
		{
			get
			{
				throw ADP.OleDb();
			}
			set
			{
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.OleDb.OleDbDataAdapter" /> class.</summary>
		// Token: 0x06000F44 RID: 3908 RVA: 0x00050DEE File Offset: 0x0004EFEE
		public OleDbDataAdapter()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.OleDb.OleDbDataAdapter" /> class with the specified <see cref="T:System.Data.OleDb.OleDbCommand" /> as the <see cref="P:System.Data.OleDb.OleDbDataAdapter.SelectCommand" /> property.</summary>
		/// <param name="selectCommand">An <see cref="T:System.Data.OleDb.OleDbCommand" /> that is a SELECT statement or stored procedure, and is set as the <see cref="P:System.Data.OleDb.OleDbDataAdapter.SelectCommand" /> property of the <see cref="T:System.Data.OleDb.OleDbDataAdapter" />.</param>
		// Token: 0x06000F45 RID: 3909 RVA: 0x00050DF6 File Offset: 0x0004EFF6
		public OleDbDataAdapter(OleDbCommand selectCommand)
		{
			throw ADP.OleDb();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.OleDb.OleDbDataAdapter" /> class with a <see cref="P:System.Data.OleDb.OleDbDataAdapter.SelectCommand" />.</summary>
		/// <param name="selectCommandText">A string that is an SQL SELECT statement or stored procedure to be used by the <see cref="P:System.Data.OleDb.OleDbDataAdapter.SelectCommand" /> property of the <see cref="T:System.Data.OleDb.OleDbDataAdapter" />. </param>
		/// <param name="selectConnection">An <see cref="T:System.Data.OleDb.OleDbConnection" /> that represents the connection. </param>
		// Token: 0x06000F46 RID: 3910 RVA: 0x00050DF6 File Offset: 0x0004EFF6
		public OleDbDataAdapter(string selectCommandText, OleDbConnection selectConnection)
		{
			throw ADP.OleDb();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.OleDb.OleDbDataAdapter" /> class with a <see cref="P:System.Data.OleDb.OleDbDataAdapter.SelectCommand" />.</summary>
		/// <param name="selectCommandText">A string that is an SQL SELECT statement or stored procedure to be used by the <see cref="P:System.Data.OleDb.OleDbDataAdapter.SelectCommand" /> property of the <see cref="T:System.Data.OleDb.OleDbDataAdapter" />. </param>
		/// <param name="selectConnectionString">The connection string. </param>
		// Token: 0x06000F47 RID: 3911 RVA: 0x00050DF6 File Offset: 0x0004EFF6
		public OleDbDataAdapter(string selectCommandText, string selectConnectionString)
		{
			throw ADP.OleDb();
		}

		// Token: 0x06000F48 RID: 3912 RVA: 0x00050D50 File Offset: 0x0004EF50
		protected override RowUpdatedEventArgs CreateRowUpdatedEvent(DataRow dataRow, IDbCommand command, StatementType statementType, DataTableMapping tableMapping)
		{
			throw ADP.OleDb();
		}

		// Token: 0x06000F49 RID: 3913 RVA: 0x00050D50 File Offset: 0x0004EF50
		protected override RowUpdatingEventArgs CreateRowUpdatingEvent(DataRow dataRow, IDbCommand command, StatementType statementType, DataTableMapping tableMapping)
		{
			throw ADP.OleDb();
		}

		/// <summary>Adds or refreshes rows in the <see cref="T:System.Data.DataSet" /> to match those in an ADO Recordset or Record object using the specified <see cref="T:System.Data.DataSet" />, ADO object, and source table name.</summary>
		/// <returns>The number of rows successfully added to or refreshed in the <see cref="T:System.Data.DataSet" />. This does not include rows affected by statements that do not return rows.</returns>
		/// <param name="dataSet">A <see cref="T:System.Data.DataSet" /> to fill with records and, if it is required, schema. </param>
		/// <param name="ADODBRecordSet">An ADO Recordset or Record object. </param>
		/// <param name="srcTable">The source table used for the table mappings. </param>
		/// <exception cref="T:System.SystemException">The source table is invalid. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000F4A RID: 3914 RVA: 0x00050D50 File Offset: 0x0004EF50
		public int Fill(DataSet dataSet, object ADODBRecordSet, string srcTable)
		{
			throw ADP.OleDb();
		}

		/// <summary>Adds or refreshes rows in a <see cref="T:System.Data.DataTable" /> to match those in an ADO Recordset or Record object using the specified <see cref="T:System.Data.DataTable" /> and ADO objects.</summary>
		/// <returns>The number of rows successfully refreshed to the <see cref="T:System.Data.DataTable" />. This does not include rows affected by statements that do not return rows.</returns>
		/// <param name="dataTable">A <see cref="T:System.Data.DataTable" /> to fill with records and, if it is required, schema. </param>
		/// <param name="ADODBRecordSet">An ADO Recordset or Record object. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000F4B RID: 3915 RVA: 0x00050D50 File Offset: 0x0004EF50
		public int Fill(DataTable dataTable, object ADODBRecordSet)
		{
			throw ADP.OleDb();
		}

		// Token: 0x06000F4C RID: 3916 RVA: 0x00050D50 File Offset: 0x0004EF50
		protected override void OnRowUpdated(RowUpdatedEventArgs value)
		{
			throw ADP.OleDb();
		}

		// Token: 0x06000F4D RID: 3917 RVA: 0x00050D50 File Offset: 0x0004EF50
		protected override void OnRowUpdating(RowUpdatingEventArgs value)
		{
			throw ADP.OleDb();
		}

		/// <summary>Occurs during <see cref="M:System.Data.Common.DbDataAdapter.Update(System.Data.DataSet)" /> after a command is executed against the data source. The attempt to update is made. Therefore, the event occurs.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x14000021 RID: 33
		// (add) Token: 0x06000F4E RID: 3918 RVA: 0x00050E04 File Offset: 0x0004F004
		// (remove) Token: 0x06000F4F RID: 3919 RVA: 0x00050E3C File Offset: 0x0004F03C
		public event OleDbRowUpdatedEventHandler RowUpdated;

		/// <summary>Occurs during <see cref="M:System.Data.Common.DbDataAdapter.Update(System.Data.DataSet)" /> before a command is executed against the data source. The attempt to update is made. Therefore, the event occurs.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x14000022 RID: 34
		// (add) Token: 0x06000F50 RID: 3920 RVA: 0x00050E74 File Offset: 0x0004F074
		// (remove) Token: 0x06000F51 RID: 3921 RVA: 0x00050EAC File Offset: 0x0004F0AC
		public event OleDbRowUpdatingEventHandler RowUpdating;

		/// <summary>For a description of this member, see <see cref="M:System.ICloneable.Clone" />.</summary>
		/// <returns>A new <see cref="T:System.Object" /> that is a copy of this instance.</returns>
		// Token: 0x06000F52 RID: 3922 RVA: 0x00050D50 File Offset: 0x0004EF50
		object ICloneable.Clone()
		{
			throw ADP.OleDb();
		}
	}
}
