using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.ProviderBase;

namespace System.Data.Common
{
	/// <summary>Aids implementation of the <see cref="T:System.Data.IDbDataAdapter" /> interface. Inheritors of <see cref="T:System.Data.Common.DbDataAdapter" /> implement a set of functions to provide strong typing, but inherit most of the functionality needed to fully implement a DataAdapter. </summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000343 RID: 835
	public abstract class DbDataAdapter : DataAdapter, IDbDataAdapter, IDataAdapter, ICloneable
	{
		/// <summary>Initializes a new instance of a DataAdapter class.</summary>
		// Token: 0x06002740 RID: 10048 RVA: 0x000AAFEA File Offset: 0x000A91EA
		protected DbDataAdapter()
		{
		}

		/// <summary>Initializes a new instance of a DataAdapter class from an existing object of the same type.</summary>
		/// <param name="adapter">A DataAdapter object used to create the new DataAdapter. </param>
		// Token: 0x06002741 RID: 10049 RVA: 0x000AEED9 File Offset: 0x000AD0D9
		protected DbDataAdapter(DbDataAdapter adapter)
			: base(adapter)
		{
			this.CloneFrom(adapter);
		}

		// Token: 0x170006D2 RID: 1746
		// (get) Token: 0x06002742 RID: 10050 RVA: 0x00005D82 File Offset: 0x00003F82
		private IDbDataAdapter _IDbDataAdapter
		{
			get
			{
				return this;
			}
		}

		/// <summary>Gets or sets a command for deleting records from the data set.</summary>
		/// <returns>An <see cref="T:System.Data.IDbCommand" /> used during <see cref="M:System.Data.IDataAdapter.Update(System.Data.DataSet)" /> to delete records in the data source for deleted rows in the data set.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170006D3 RID: 1747
		// (get) Token: 0x06002743 RID: 10051 RVA: 0x000AEEE9 File Offset: 0x000AD0E9
		// (set) Token: 0x06002744 RID: 10052 RVA: 0x000AEEFB File Offset: 0x000AD0FB
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public DbCommand DeleteCommand
		{
			get
			{
				return (DbCommand)this._IDbDataAdapter.DeleteCommand;
			}
			set
			{
				this._IDbDataAdapter.DeleteCommand = value;
			}
		}

		/// <summary>Gets or sets an SQL statement for deleting records from the data set.</summary>
		/// <returns>An <see cref="T:System.Data.IDbCommand" /> used during <see cref="M:System.Data.Common.DbDataAdapter.Update(System.Data.DataSet)" /> to delete records in the data source for deleted rows in the data set.</returns>
		// Token: 0x170006D4 RID: 1748
		// (get) Token: 0x06002745 RID: 10053 RVA: 0x000AEF09 File Offset: 0x000AD109
		// (set) Token: 0x06002746 RID: 10054 RVA: 0x000AEF11 File Offset: 0x000AD111
		IDbCommand IDbDataAdapter.DeleteCommand
		{
			get
			{
				return this._deleteCommand;
			}
			set
			{
				this._deleteCommand = value;
			}
		}

		/// <summary>Gets or sets the behavior of the command used to fill the data adapter.</summary>
		/// <returns>The <see cref="T:System.Data.CommandBehavior" /> of the command used to fill the data adapter.</returns>
		// Token: 0x170006D5 RID: 1749
		// (get) Token: 0x06002747 RID: 10055 RVA: 0x000AEF1A File Offset: 0x000AD11A
		// (set) Token: 0x06002748 RID: 10056 RVA: 0x000AEF25 File Offset: 0x000AD125
		protected internal CommandBehavior FillCommandBehavior
		{
			get
			{
				return this._fillCommandBehavior | CommandBehavior.SequentialAccess;
			}
			set
			{
				this._fillCommandBehavior = value | CommandBehavior.SequentialAccess;
			}
		}

		/// <summary>Gets or sets a command used to insert new records into the data source.</summary>
		/// <returns>A <see cref="T:System.Data.IDbCommand" /> used during <see cref="M:System.Data.IDataAdapter.Update(System.Data.DataSet)" /> to insert records in the data source for new rows in the data set.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170006D6 RID: 1750
		// (get) Token: 0x06002749 RID: 10057 RVA: 0x000AEF31 File Offset: 0x000AD131
		// (set) Token: 0x0600274A RID: 10058 RVA: 0x000AEF43 File Offset: 0x000AD143
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public DbCommand InsertCommand
		{
			get
			{
				return (DbCommand)this._IDbDataAdapter.InsertCommand;
			}
			set
			{
				this._IDbDataAdapter.InsertCommand = value;
			}
		}

		/// <summary>Gets or sets an SQL statement used to insert new records into the data source.</summary>
		/// <returns>An <see cref="T:System.Data.IDbCommand" /> used during <see cref="M:System.Data.Common.DbDataAdapter.Update(System.Data.DataSet)" /> to insert records in the data source for new rows in the data set.</returns>
		// Token: 0x170006D7 RID: 1751
		// (get) Token: 0x0600274B RID: 10059 RVA: 0x000AEF51 File Offset: 0x000AD151
		// (set) Token: 0x0600274C RID: 10060 RVA: 0x000AEF59 File Offset: 0x000AD159
		IDbCommand IDbDataAdapter.InsertCommand
		{
			get
			{
				return this._insertCommand;
			}
			set
			{
				this._insertCommand = value;
			}
		}

		/// <summary>Gets or sets a command used to select records in the data source.</summary>
		/// <returns>A <see cref="T:System.Data.IDbCommand" /> that is used during <see cref="M:System.Data.IDataAdapter.Update(System.Data.DataSet)" /> to select records from data source for placement in the data set.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170006D8 RID: 1752
		// (get) Token: 0x0600274D RID: 10061 RVA: 0x000AEF62 File Offset: 0x000AD162
		// (set) Token: 0x0600274E RID: 10062 RVA: 0x000AEF74 File Offset: 0x000AD174
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public DbCommand SelectCommand
		{
			get
			{
				return (DbCommand)this._IDbDataAdapter.SelectCommand;
			}
			set
			{
				this._IDbDataAdapter.SelectCommand = value;
			}
		}

		/// <summary>Gets or sets an SQL statement used to select records in the data source.</summary>
		/// <returns>An <see cref="T:System.Data.IDbCommand" /> that is used during <see cref="M:System.Data.Common.DbDataAdapter.Update(System.Data.DataSet)" /> to select records from data source for placement in the data set.</returns>
		// Token: 0x170006D9 RID: 1753
		// (get) Token: 0x0600274F RID: 10063 RVA: 0x000AEF82 File Offset: 0x000AD182
		// (set) Token: 0x06002750 RID: 10064 RVA: 0x000AEF8A File Offset: 0x000AD18A
		IDbCommand IDbDataAdapter.SelectCommand
		{
			get
			{
				return this._selectCommand;
			}
			set
			{
				this._selectCommand = value;
			}
		}

		/// <summary>Gets or sets a value that enables or disables batch processing support, and specifies the number of commands that can be executed in a batch. </summary>
		/// <returns>The number of rows to process per batch. Value isEffect0There is no limit on the batch size.1Disables batch updating.&gt; 1Changes are sent using batches of <see cref="P:System.Data.Common.DbDataAdapter.UpdateBatchSize" /> operations at a time.When setting this to a value other than 1 ,all the commands associated with the <see cref="T:System.Data.Common.DbDataAdapter" /> must have their <see cref="P:System.Data.IDbCommand.UpdatedRowSource" /> property set to None or OutputParameters. An exception will be thrown otherwise. </returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x170006DA RID: 1754
		// (get) Token: 0x06002751 RID: 10065 RVA: 0x0000EF2B File Offset: 0x0000D12B
		// (set) Token: 0x06002752 RID: 10066 RVA: 0x000AEF93 File Offset: 0x000AD193
		[DefaultValue(1)]
		public virtual int UpdateBatchSize
		{
			get
			{
				return 1;
			}
			set
			{
				if (1 != value)
				{
					throw ADP.NotSupported();
				}
			}
		}

		/// <summary>Gets or sets a command used to update records in the data source.</summary>
		/// <returns>A <see cref="T:System.Data.IDbCommand" /> used during <see cref="M:System.Data.IDataAdapter.Update(System.Data.DataSet)" /> to update records in the data source for modified rows in the data set.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170006DB RID: 1755
		// (get) Token: 0x06002753 RID: 10067 RVA: 0x000AEF9F File Offset: 0x000AD19F
		// (set) Token: 0x06002754 RID: 10068 RVA: 0x000AEFB1 File Offset: 0x000AD1B1
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public DbCommand UpdateCommand
		{
			get
			{
				return (DbCommand)this._IDbDataAdapter.UpdateCommand;
			}
			set
			{
				this._IDbDataAdapter.UpdateCommand = value;
			}
		}

		/// <summary>Gets or sets an SQL statement used to update records in the data source.</summary>
		/// <returns>An <see cref="T:System.Data.IDbCommand" /> used during <see cref="M:System.Data.Common.DbDataAdapter.Update(System.Data.DataSet)" /> to update records in the data source for modified rows in the data set.</returns>
		// Token: 0x170006DC RID: 1756
		// (get) Token: 0x06002755 RID: 10069 RVA: 0x000AEFBF File Offset: 0x000AD1BF
		// (set) Token: 0x06002756 RID: 10070 RVA: 0x000AEFC7 File Offset: 0x000AD1C7
		IDbCommand IDbDataAdapter.UpdateCommand
		{
			get
			{
				return this._updateCommand;
			}
			set
			{
				this._updateCommand = value;
			}
		}

		// Token: 0x170006DD RID: 1757
		// (get) Token: 0x06002757 RID: 10071 RVA: 0x000AEFD0 File Offset: 0x000AD1D0
		private MissingMappingAction UpdateMappingAction
		{
			get
			{
				if (MissingMappingAction.Passthrough == base.MissingMappingAction)
				{
					return MissingMappingAction.Passthrough;
				}
				return MissingMappingAction.Error;
			}
		}

		// Token: 0x170006DE RID: 1758
		// (get) Token: 0x06002758 RID: 10072 RVA: 0x000AEFE0 File Offset: 0x000AD1E0
		private MissingSchemaAction UpdateSchemaAction
		{
			get
			{
				MissingSchemaAction missingSchemaAction = base.MissingSchemaAction;
				if (MissingSchemaAction.Add == missingSchemaAction || MissingSchemaAction.AddWithKey == missingSchemaAction)
				{
					return MissingSchemaAction.Ignore;
				}
				return MissingSchemaAction.Error;
			}
		}

		/// <summary>Adds a <see cref="T:System.Data.IDbCommand" /> to the current batch.</summary>
		/// <returns>The number of commands in the batch before adding the <see cref="T:System.Data.IDbCommand" />.</returns>
		/// <param name="command">The <see cref="T:System.Data.IDbCommand" /> to add to the batch.</param>
		/// <exception cref="T:System.NotSupportedException">The adapter does not support batches. </exception>
		// Token: 0x06002759 RID: 10073 RVA: 0x000621D6 File Offset: 0x000603D6
		protected virtual int AddToBatch(IDbCommand command)
		{
			throw ADP.NotSupported();
		}

		/// <summary>Removes all <see cref="T:System.Data.IDbCommand" /> objects from the batch.</summary>
		/// <exception cref="T:System.NotSupportedException">The adapter does not support batches. </exception>
		// Token: 0x0600275A RID: 10074 RVA: 0x000621D6 File Offset: 0x000603D6
		protected virtual void ClearBatch()
		{
			throw ADP.NotSupported();
		}

		/// <summary>Creates a new object that is a copy of the current instance.</summary>
		/// <returns>A new object that is a copy of this instance.</returns>
		// Token: 0x0600275B RID: 10075 RVA: 0x000AEFFF File Offset: 0x000AD1FF
		object ICloneable.Clone()
		{
			DbDataAdapter dbDataAdapter = (DbDataAdapter)this.CloneInternals();
			dbDataAdapter.CloneFrom(this);
			return dbDataAdapter;
		}

		// Token: 0x0600275C RID: 10076 RVA: 0x000AF014 File Offset: 0x000AD214
		private void CloneFrom(DbDataAdapter from)
		{
			IDbDataAdapter idbDataAdapter = from._IDbDataAdapter;
			this._IDbDataAdapter.SelectCommand = this.CloneCommand(idbDataAdapter.SelectCommand);
			this._IDbDataAdapter.InsertCommand = this.CloneCommand(idbDataAdapter.InsertCommand);
			this._IDbDataAdapter.UpdateCommand = this.CloneCommand(idbDataAdapter.UpdateCommand);
			this._IDbDataAdapter.DeleteCommand = this.CloneCommand(idbDataAdapter.DeleteCommand);
		}

		// Token: 0x0600275D RID: 10077 RVA: 0x000AF084 File Offset: 0x000AD284
		private IDbCommand CloneCommand(IDbCommand command)
		{
			return (IDbCommand)((command is ICloneable) ? ((ICloneable)command).Clone() : null);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.Common.RowUpdatedEventArgs" /> class.</summary>
		/// <returns>A new instance of the <see cref="T:System.Data.Common.RowUpdatedEventArgs" /> class.</returns>
		/// <param name="dataRow">The <see cref="T:System.Data.DataRow" /> used to update the data source. </param>
		/// <param name="command">The <see cref="T:System.Data.IDbCommand" /> executed during the <see cref="M:System.Data.IDataAdapter.Update(System.Data.DataSet)" />. </param>
		/// <param name="statementType">Whether the command is an UPDATE, INSERT, DELETE, or SELECT statement. </param>
		/// <param name="tableMapping">A <see cref="T:System.Data.Common.DataTableMapping" /> object. </param>
		// Token: 0x0600275E RID: 10078 RVA: 0x000AF0A1 File Offset: 0x000AD2A1
		protected virtual RowUpdatedEventArgs CreateRowUpdatedEvent(DataRow dataRow, IDbCommand command, StatementType statementType, DataTableMapping tableMapping)
		{
			return new RowUpdatedEventArgs(dataRow, command, statementType, tableMapping);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.Common.RowUpdatingEventArgs" /> class.</summary>
		/// <returns>A new instance of the <see cref="T:System.Data.Common.RowUpdatingEventArgs" /> class.</returns>
		/// <param name="dataRow">The <see cref="T:System.Data.DataRow" /> that updates the data source. </param>
		/// <param name="command">The <see cref="T:System.Data.IDbCommand" /> to execute during the <see cref="M:System.Data.IDataAdapter.Update(System.Data.DataSet)" />. </param>
		/// <param name="statementType">Whether the command is an UPDATE, INSERT, DELETE, or SELECT statement. </param>
		/// <param name="tableMapping">A <see cref="T:System.Data.Common.DataTableMapping" /> object. </param>
		// Token: 0x0600275F RID: 10079 RVA: 0x000AF0AD File Offset: 0x000AD2AD
		protected virtual RowUpdatingEventArgs CreateRowUpdatingEvent(DataRow dataRow, IDbCommand command, StatementType statementType, DataTableMapping tableMapping)
		{
			return new RowUpdatingEventArgs(dataRow, command, statementType, tableMapping);
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Data.Common.DbDataAdapter" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
		// Token: 0x06002760 RID: 10080 RVA: 0x000AF0B9 File Offset: 0x000AD2B9
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				((IDbDataAdapter)this).SelectCommand = null;
				((IDbDataAdapter)this).InsertCommand = null;
				((IDbDataAdapter)this).UpdateCommand = null;
				((IDbDataAdapter)this).DeleteCommand = null;
			}
			base.Dispose(disposing);
		}

		/// <summary>Executes the current batch.</summary>
		/// <returns>The return value from the last command in the batch.</returns>
		// Token: 0x06002761 RID: 10081 RVA: 0x000621D6 File Offset: 0x000603D6
		protected virtual int ExecuteBatch()
		{
			throw ADP.NotSupported();
		}

		/// <summary>Configures the schema of the specified <see cref="T:System.Data.DataTable" /> based on the specified <see cref="T:System.Data.SchemaType" />.</summary>
		/// <returns>A <see cref="T:System.Data.DataTable" /> that contains schema information returned from the data source.</returns>
		/// <param name="dataTable">The <see cref="T:System.Data.DataTable" /> to be filled with the schema from the data source. </param>
		/// <param name="schemaType">One of the <see cref="T:System.Data.SchemaType" /> values. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06002762 RID: 10082 RVA: 0x000AF0E4 File Offset: 0x000AD2E4
		public DataTable FillSchema(DataTable dataTable, SchemaType schemaType)
		{
			long num = DataCommonEventSource.Log.EnterScope<int, SchemaType>("<comm.DbDataAdapter.FillSchema|API> {0}, dataTable, schemaType={1}", base.ObjectID, schemaType);
			DataTable dataTable2;
			try
			{
				IDbCommand selectCommand = this._IDbDataAdapter.SelectCommand;
				CommandBehavior fillCommandBehavior = this.FillCommandBehavior;
				dataTable2 = this.FillSchema(dataTable, schemaType, selectCommand, fillCommandBehavior);
			}
			finally
			{
				DataCommonEventSource.Log.ExitScope(num);
			}
			return dataTable2;
		}

		/// <summary>Adds a <see cref="T:System.Data.DataTable" /> named "Table" to the specified <see cref="T:System.Data.DataSet" /> and configures the schema to match that in the data source based on the specified <see cref="T:System.Data.SchemaType" />.</summary>
		/// <returns>A reference to a collection of <see cref="T:System.Data.DataTable" /> objects that were added to the <see cref="T:System.Data.DataSet" />.</returns>
		/// <param name="dataSet">A <see cref="T:System.Data.DataSet" /> to insert the schema in. </param>
		/// <param name="schemaType">One of the <see cref="T:System.Data.SchemaType" /> values that specify how to insert the schema. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06002763 RID: 10083 RVA: 0x000AF148 File Offset: 0x000AD348
		public override DataTable[] FillSchema(DataSet dataSet, SchemaType schemaType)
		{
			long num = DataCommonEventSource.Log.EnterScope<int, SchemaType>("<comm.DbDataAdapter.FillSchema|API> {0}, dataSet, schemaType={1}", base.ObjectID, schemaType);
			DataTable[] array;
			try
			{
				IDbCommand selectCommand = this._IDbDataAdapter.SelectCommand;
				if (base.DesignMode && (selectCommand == null || selectCommand.Connection == null || string.IsNullOrEmpty(selectCommand.CommandText)))
				{
					array = Array.Empty<DataTable>();
				}
				else
				{
					CommandBehavior fillCommandBehavior = this.FillCommandBehavior;
					array = this.FillSchema(dataSet, schemaType, selectCommand, "Table", fillCommandBehavior);
				}
			}
			finally
			{
				DataCommonEventSource.Log.ExitScope(num);
			}
			return array;
		}

		/// <summary>Adds a <see cref="T:System.Data.DataTable" /> to the specified <see cref="T:System.Data.DataSet" /> and configures the schema to match that in the data source based upon the specified <see cref="T:System.Data.SchemaType" /> and <see cref="T:System.Data.DataTable" />.</summary>
		/// <returns>A reference to a collection of <see cref="T:System.Data.DataTable" /> objects that were added to the <see cref="T:System.Data.DataSet" />.</returns>
		/// <param name="dataSet">A <see cref="T:System.Data.DataSet" /> to insert the schema in. </param>
		/// <param name="schemaType">One of the <see cref="T:System.Data.SchemaType" /> values that specify how to insert the schema. </param>
		/// <param name="srcTable">The name of the source table to use for table mapping. </param>
		/// <exception cref="T:System.ArgumentException">A source table from which to get the schema could not be found. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06002764 RID: 10084 RVA: 0x000AF1D8 File Offset: 0x000AD3D8
		public DataTable[] FillSchema(DataSet dataSet, SchemaType schemaType, string srcTable)
		{
			long num = DataCommonEventSource.Log.EnterScope<int, int, string>("<comm.DbDataAdapter.FillSchema|API> {0}, dataSet, schemaType={1}, srcTable={2}", base.ObjectID, (int)schemaType, srcTable);
			DataTable[] array;
			try
			{
				IDbCommand selectCommand = this._IDbDataAdapter.SelectCommand;
				CommandBehavior fillCommandBehavior = this.FillCommandBehavior;
				array = this.FillSchema(dataSet, schemaType, selectCommand, srcTable, fillCommandBehavior);
			}
			finally
			{
				DataCommonEventSource.Log.ExitScope(num);
			}
			return array;
		}

		/// <summary>Adds a <see cref="T:System.Data.DataTable" /> to the specified <see cref="T:System.Data.DataSet" /> and configures the schema to match that in the data source based on the specified <see cref="T:System.Data.SchemaType" />.</summary>
		/// <returns>An array of <see cref="T:System.Data.DataTable" /> objects that contain schema information returned from the data source.</returns>
		/// <param name="dataSet">The <see cref="T:System.Data.DataSet" /> to be filled with the schema from the data source. </param>
		/// <param name="schemaType">One of the <see cref="T:System.Data.SchemaType" /> values. </param>
		/// <param name="command">The SQL SELECT statement used to retrieve rows from the data source. </param>
		/// <param name="srcTable">The name of the source table to use for table mapping. </param>
		/// <param name="behavior">One of the <see cref="T:System.Data.CommandBehavior" /> values. </param>
		// Token: 0x06002765 RID: 10085 RVA: 0x000AF23C File Offset: 0x000AD43C
		protected virtual DataTable[] FillSchema(DataSet dataSet, SchemaType schemaType, IDbCommand command, string srcTable, CommandBehavior behavior)
		{
			long num = DataCommonEventSource.Log.EnterScope<int, CommandBehavior>("<comm.DbDataAdapter.FillSchema|API> {0}, dataSet, schemaType, command, srcTable, behavior={1}", base.ObjectID, behavior);
			DataTable[] array;
			try
			{
				if (dataSet == null)
				{
					throw ADP.ArgumentNull("dataSet");
				}
				if (SchemaType.Source != schemaType && SchemaType.Mapped != schemaType)
				{
					throw ADP.InvalidSchemaType(schemaType);
				}
				if (string.IsNullOrEmpty(srcTable))
				{
					throw ADP.FillSchemaRequiresSourceTableName("srcTable");
				}
				if (command == null)
				{
					throw ADP.MissingSelectCommand("FillSchema");
				}
				array = (DataTable[])this.FillSchemaInternal(dataSet, null, schemaType, command, srcTable, behavior);
			}
			finally
			{
				DataCommonEventSource.Log.ExitScope(num);
			}
			return array;
		}

		/// <summary>Configures the schema of the specified <see cref="T:System.Data.DataTable" /> based on the specified <see cref="T:System.Data.SchemaType" />, command string, and <see cref="T:System.Data.CommandBehavior" /> values.</summary>
		/// <returns>A of <see cref="T:System.Data.DataTable" /> object that contains schema information returned from the data source.</returns>
		/// <param name="dataTable">The <see cref="T:System.Data.DataTable" /> to be filled with the schema from the data source. </param>
		/// <param name="schemaType">One of the <see cref="T:System.Data.SchemaType" /> values. </param>
		/// <param name="command">The SQL SELECT statement used to retrieve rows from the data source. </param>
		/// <param name="behavior">One of the <see cref="T:System.Data.CommandBehavior" /> values. </param>
		// Token: 0x06002766 RID: 10086 RVA: 0x000AF2D4 File Offset: 0x000AD4D4
		protected virtual DataTable FillSchema(DataTable dataTable, SchemaType schemaType, IDbCommand command, CommandBehavior behavior)
		{
			long num = DataCommonEventSource.Log.EnterScope<int, CommandBehavior>("<comm.DbDataAdapter.FillSchema|API> {0}, dataTable, schemaType, command, behavior={1}", base.ObjectID, behavior);
			DataTable dataTable2;
			try
			{
				if (dataTable == null)
				{
					throw ADP.ArgumentNull("dataTable");
				}
				if (SchemaType.Source != schemaType && SchemaType.Mapped != schemaType)
				{
					throw ADP.InvalidSchemaType(schemaType);
				}
				if (command == null)
				{
					throw ADP.MissingSelectCommand("FillSchema");
				}
				string text = dataTable.TableName;
				int num2 = base.IndexOfDataSetTable(text);
				if (-1 != num2)
				{
					text = base.TableMappings[num2].SourceTable;
				}
				dataTable2 = (DataTable)this.FillSchemaInternal(null, dataTable, schemaType, command, text, behavior | CommandBehavior.SingleResult);
			}
			finally
			{
				DataCommonEventSource.Log.ExitScope(num);
			}
			return dataTable2;
		}

		// Token: 0x06002767 RID: 10087 RVA: 0x000AF380 File Offset: 0x000AD580
		private object FillSchemaInternal(DataSet dataset, DataTable datatable, SchemaType schemaType, IDbCommand command, string srcTable, CommandBehavior behavior)
		{
			object obj = null;
			bool flag = command.Connection == null;
			try
			{
				IDbConnection connection = DbDataAdapter.GetConnection3(this, command, "FillSchema");
				ConnectionState connectionState = ConnectionState.Open;
				try
				{
					DbDataAdapter.QuietOpen(connection, out connectionState);
					using (IDataReader dataReader = command.ExecuteReader(behavior | CommandBehavior.SchemaOnly | CommandBehavior.KeyInfo))
					{
						if (datatable != null)
						{
							obj = this.FillSchema(datatable, schemaType, dataReader);
						}
						else
						{
							obj = this.FillSchema(dataset, schemaType, srcTable, dataReader);
						}
					}
				}
				finally
				{
					DbDataAdapter.QuietClose(connection, connectionState);
				}
			}
			finally
			{
				if (flag)
				{
					command.Transaction = null;
					command.Connection = null;
				}
			}
			return obj;
		}

		/// <summary>Adds or refreshes rows in the <see cref="T:System.Data.DataSet" />.</summary>
		/// <returns>The number of rows successfully added to or refreshed in the <see cref="T:System.Data.DataSet" />. This does not include rows affected by statements that do not return rows.</returns>
		/// <param name="dataSet">A <see cref="T:System.Data.DataSet" /> to fill with records and, if necessary, schema. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06002768 RID: 10088 RVA: 0x000AF434 File Offset: 0x000AD634
		public override int Fill(DataSet dataSet)
		{
			long num = DataCommonEventSource.Log.EnterScope<int>("<comm.DbDataAdapter.Fill|API> {0}, dataSet", base.ObjectID);
			int num2;
			try
			{
				IDbCommand selectCommand = this._IDbDataAdapter.SelectCommand;
				CommandBehavior fillCommandBehavior = this.FillCommandBehavior;
				num2 = this.Fill(dataSet, 0, 0, "Table", selectCommand, fillCommandBehavior);
			}
			finally
			{
				DataCommonEventSource.Log.ExitScope(num);
			}
			return num2;
		}

		/// <summary>Adds or refreshes rows in the <see cref="T:System.Data.DataSet" /> to match those in the data source using the <see cref="T:System.Data.DataSet" /> and <see cref="T:System.Data.DataTable" /> names.</summary>
		/// <returns>The number of rows successfully added to or refreshed in the <see cref="T:System.Data.DataSet" />. This does not include rows affected by statements that do not return rows.</returns>
		/// <param name="dataSet">A <see cref="T:System.Data.DataSet" /> to fill with records and, if necessary, schema. </param>
		/// <param name="srcTable">The name of the source table to use for table mapping. </param>
		/// <exception cref="T:System.SystemException">The source table is invalid. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06002769 RID: 10089 RVA: 0x000AF49C File Offset: 0x000AD69C
		public int Fill(DataSet dataSet, string srcTable)
		{
			long num = DataCommonEventSource.Log.EnterScope<int, string>("<comm.DbDataAdapter.Fill|API> {0}, dataSet, srcTable='{1}'", base.ObjectID, srcTable);
			int num2;
			try
			{
				IDbCommand selectCommand = this._IDbDataAdapter.SelectCommand;
				CommandBehavior fillCommandBehavior = this.FillCommandBehavior;
				num2 = this.Fill(dataSet, 0, 0, srcTable, selectCommand, fillCommandBehavior);
			}
			finally
			{
				DataCommonEventSource.Log.ExitScope(num);
			}
			return num2;
		}

		/// <summary>Adds or refreshes rows in a specified range in the <see cref="T:System.Data.DataSet" /> to match those in the data source using the <see cref="T:System.Data.DataSet" /> and <see cref="T:System.Data.DataTable" /> names.</summary>
		/// <returns>The number of rows successfully added to or refreshed in the <see cref="T:System.Data.DataSet" />. This does not include rows affected by statements that do not return rows.</returns>
		/// <param name="dataSet">A <see cref="T:System.Data.DataSet" /> to fill with records and, if necessary, schema. </param>
		/// <param name="startRecord">The zero-based record number to start with. </param>
		/// <param name="maxRecords">The maximum number of records to retrieve. </param>
		/// <param name="srcTable">The name of the source table to use for table mapping. </param>
		/// <exception cref="T:System.SystemException">The <see cref="T:System.Data.DataSet" /> is invalid. </exception>
		/// <exception cref="T:System.InvalidOperationException">The source table is invalid.-or- The connection is invalid. </exception>
		/// <exception cref="T:System.InvalidCastException">The connection could not be found. </exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="startRecord" /> parameter is less than 0.-or- The <paramref name="maxRecords" /> parameter is less than 0. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x0600276A RID: 10090 RVA: 0x000AF500 File Offset: 0x000AD700
		public int Fill(DataSet dataSet, int startRecord, int maxRecords, string srcTable)
		{
			long num = DataCommonEventSource.Log.EnterScope<int, int, int, string>("<comm.DbDataAdapter.Fill|API> {0}, dataSet, startRecord={1}, maxRecords={2}, srcTable='{3}'", base.ObjectID, startRecord, maxRecords, srcTable);
			int num2;
			try
			{
				IDbCommand selectCommand = this._IDbDataAdapter.SelectCommand;
				CommandBehavior fillCommandBehavior = this.FillCommandBehavior;
				num2 = this.Fill(dataSet, startRecord, maxRecords, srcTable, selectCommand, fillCommandBehavior);
			}
			finally
			{
				DataCommonEventSource.Log.ExitScope(num);
			}
			return num2;
		}

		/// <summary>Adds or refreshes rows in a specified range in the <see cref="T:System.Data.DataSet" /> to match those in the data source using the <see cref="T:System.Data.DataSet" /> and source table names, command string, and command behavior.</summary>
		/// <returns>The number of rows successfully added to or refreshed in the <see cref="T:System.Data.DataSet" />. This does not include rows affected by statements that do not return rows.</returns>
		/// <param name="dataSet">A <see cref="T:System.Data.DataSet" /> to fill with records and, if necessary, schema. </param>
		/// <param name="startRecord">The zero-based record number to start with. </param>
		/// <param name="maxRecords">The maximum number of records to retrieve. </param>
		/// <param name="srcTable">The name of the source table to use for table mapping. </param>
		/// <param name="command">The SQL SELECT statement used to retrieve rows from the data source. </param>
		/// <param name="behavior">One of the <see cref="T:System.Data.CommandBehavior" /> values. </param>
		/// <exception cref="T:System.InvalidOperationException">The source table is invalid. </exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="startRecord" /> parameter is less than 0.-or- The <paramref name="maxRecords" /> parameter is less than 0. </exception>
		// Token: 0x0600276B RID: 10091 RVA: 0x000AF568 File Offset: 0x000AD768
		protected virtual int Fill(DataSet dataSet, int startRecord, int maxRecords, string srcTable, IDbCommand command, CommandBehavior behavior)
		{
			long num = DataCommonEventSource.Log.EnterScope<int, CommandBehavior>("<comm.DbDataAdapter.Fill|API> {0}, dataSet, startRecord, maxRecords, srcTable, command, behavior={1}", base.ObjectID, behavior);
			int num2;
			try
			{
				if (dataSet == null)
				{
					throw ADP.FillRequires("dataSet");
				}
				if (startRecord < 0)
				{
					throw ADP.InvalidStartRecord("startRecord", startRecord);
				}
				if (maxRecords < 0)
				{
					throw ADP.InvalidMaxRecords("maxRecords", maxRecords);
				}
				if (string.IsNullOrEmpty(srcTable))
				{
					throw ADP.FillRequiresSourceTableName("srcTable");
				}
				if (command == null)
				{
					throw ADP.MissingSelectCommand("Fill");
				}
				num2 = this.FillInternal(dataSet, null, startRecord, maxRecords, srcTable, command, behavior);
			}
			finally
			{
				DataCommonEventSource.Log.ExitScope(num);
			}
			return num2;
		}

		/// <summary>Adds or refreshes rows in a specified range in the <see cref="T:System.Data.DataSet" /> to match those in the data source using the <see cref="T:System.Data.DataTable" /> name.</summary>
		/// <returns>The number of rows successfully added to or refreshed in the <see cref="T:System.Data.DataSet" />. This does not include rows affected by statements that do not return rows.</returns>
		/// <param name="dataTable">The name of the <see cref="T:System.Data.DataTable" /> to use for table mapping. </param>
		/// <exception cref="T:System.InvalidOperationException">The source table is invalid. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x0600276C RID: 10092 RVA: 0x000AF610 File Offset: 0x000AD810
		public int Fill(DataTable dataTable)
		{
			long num = DataCommonEventSource.Log.EnterScope<int>("<comm.DbDataAdapter.Fill|API> {0}, dataTable", base.ObjectID);
			int num2;
			try
			{
				DataTable[] array = new DataTable[] { dataTable };
				IDbCommand selectCommand = this._IDbDataAdapter.SelectCommand;
				CommandBehavior fillCommandBehavior = this.FillCommandBehavior;
				num2 = this.Fill(array, 0, 0, selectCommand, fillCommandBehavior);
			}
			finally
			{
				DataCommonEventSource.Log.ExitScope(num);
			}
			return num2;
		}

		/// <summary>Adds or refreshes rows in a <see cref="T:System.Data.DataTable" /> to match those in the data source starting at the specified record and retrieving up to the specified maximum number of records.</summary>
		/// <returns>The number of rows successfully added to or refreshed in the <see cref="T:System.Data.DataTable" />. This value does not include rows affected by statements that do not return rows.</returns>
		/// <param name="startRecord">The zero-based record number to start with. </param>
		/// <param name="maxRecords">The maximum number of records to retrieve. </param>
		/// <param name="dataTables">The <see cref="T:System.Data.DataTable" /> objects to fill from the data source.</param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x0600276D RID: 10093 RVA: 0x000AF680 File Offset: 0x000AD880
		public int Fill(int startRecord, int maxRecords, params DataTable[] dataTables)
		{
			long num = DataCommonEventSource.Log.EnterScope<int, int, int>("<comm.DbDataAdapter.Fill|API> {0}, startRecord={1}, maxRecords={2}, dataTable[]", base.ObjectID, startRecord, maxRecords);
			int num2;
			try
			{
				IDbCommand selectCommand = this._IDbDataAdapter.SelectCommand;
				CommandBehavior fillCommandBehavior = this.FillCommandBehavior;
				num2 = this.Fill(dataTables, startRecord, maxRecords, selectCommand, fillCommandBehavior);
			}
			finally
			{
				DataCommonEventSource.Log.ExitScope(num);
			}
			return num2;
		}

		/// <summary>Adds or refreshes rows in a <see cref="T:System.Data.DataTable" /> to match those in the data source using the specified <see cref="T:System.Data.DataTable" />, <see cref="T:System.Data.IDbCommand" /> and <see cref="T:System.Data.CommandBehavior" />.</summary>
		/// <returns>The number of rows successfully added to or refreshed in the <see cref="T:System.Data.DataTable" />. This does not include rows affected by statements that do not return rows.</returns>
		/// <param name="dataTable">A <see cref="T:System.Data.DataTable" /> to fill with records and, if necessary, schema. </param>
		/// <param name="command">The SQL SELECT statement used to retrieve rows from the data source. </param>
		/// <param name="behavior">One of the <see cref="T:System.Data.CommandBehavior" /> values.</param>
		// Token: 0x0600276E RID: 10094 RVA: 0x000AF6E4 File Offset: 0x000AD8E4
		protected virtual int Fill(DataTable dataTable, IDbCommand command, CommandBehavior behavior)
		{
			long num = DataCommonEventSource.Log.EnterScope<int, CommandBehavior>("<comm.DbDataAdapter.Fill|API> {0}, dataTable, command, behavior={1}", base.ObjectID, behavior);
			int num2;
			try
			{
				DataTable[] array = new DataTable[] { dataTable };
				num2 = this.Fill(array, 0, 0, command, behavior);
			}
			finally
			{
				DataCommonEventSource.Log.ExitScope(num);
			}
			return num2;
		}

		/// <summary>Adds or refreshes rows in a specified range in the <see cref="T:System.Data.DataSet" /> to match those in the data source using the <see cref="T:System.Data.DataSet" /> and <see cref="T:System.Data.DataTable" /> names.</summary>
		/// <returns>The number of rows added to or refreshed in the data tables.</returns>
		/// <param name="dataTables">The <see cref="T:System.Data.DataTable" /> objects to fill from the data source.</param>
		/// <param name="startRecord">The zero-based record number to start with.</param>
		/// <param name="maxRecords">The maximum number of records to retrieve.</param>
		/// <param name="command">The <see cref="T:System.Data.IDbCommand" /> executed to fill the <see cref="T:System.Data.DataTable" /> objects.</param>
		/// <param name="behavior">One of the <see cref="T:System.Data.CommandBehavior" /> values.</param>
		/// <exception cref="T:System.SystemException">The <see cref="T:System.Data.DataSet" /> is invalid. </exception>
		/// <exception cref="T:System.InvalidOperationException">The source table is invalid.-or- The connection is invalid. </exception>
		/// <exception cref="T:System.InvalidCastException">The connection could not be found. </exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="startRecord" /> parameter is less than 0.-or- The <paramref name="maxRecords" /> parameter is less than 0. </exception>
		// Token: 0x0600276F RID: 10095 RVA: 0x000AF740 File Offset: 0x000AD940
		protected virtual int Fill(DataTable[] dataTables, int startRecord, int maxRecords, IDbCommand command, CommandBehavior behavior)
		{
			long num = DataCommonEventSource.Log.EnterScope<int, CommandBehavior>("<comm.DbDataAdapter.Fill|API> {0}, dataTables[], startRecord, maxRecords, command, behavior={1}", base.ObjectID, behavior);
			int num2;
			try
			{
				if (dataTables == null || dataTables.Length == 0 || dataTables[0] == null)
				{
					throw ADP.FillRequires("dataTable");
				}
				if (startRecord < 0)
				{
					throw ADP.InvalidStartRecord("startRecord", startRecord);
				}
				if (maxRecords < 0)
				{
					throw ADP.InvalidMaxRecords("maxRecords", maxRecords);
				}
				if (1 < dataTables.Length && (startRecord != 0 || maxRecords != 0))
				{
					throw ADP.OnlyOneTableForStartRecordOrMaxRecords();
				}
				if (command == null)
				{
					throw ADP.MissingSelectCommand("Fill");
				}
				if (1 == dataTables.Length)
				{
					behavior |= CommandBehavior.SingleResult;
				}
				num2 = this.FillInternal(null, dataTables, startRecord, maxRecords, null, command, behavior);
			}
			finally
			{
				DataCommonEventSource.Log.ExitScope(num);
			}
			return num2;
		}

		// Token: 0x06002770 RID: 10096 RVA: 0x000AF7F8 File Offset: 0x000AD9F8
		private int FillInternal(DataSet dataset, DataTable[] datatables, int startRecord, int maxRecords, string srcTable, IDbCommand command, CommandBehavior behavior)
		{
			int num = 0;
			bool flag = command.Connection == null;
			try
			{
				IDbConnection connection = DbDataAdapter.GetConnection3(this, command, "Fill");
				ConnectionState connectionState = ConnectionState.Open;
				if (MissingSchemaAction.AddWithKey == base.MissingSchemaAction)
				{
					behavior |= CommandBehavior.KeyInfo;
				}
				try
				{
					DbDataAdapter.QuietOpen(connection, out connectionState);
					behavior |= CommandBehavior.SequentialAccess;
					IDataReader dataReader = null;
					try
					{
						dataReader = command.ExecuteReader(behavior);
						if (datatables != null)
						{
							num = this.Fill(datatables, dataReader, startRecord, maxRecords);
						}
						else
						{
							num = this.Fill(dataset, srcTable, dataReader, startRecord, maxRecords);
						}
					}
					finally
					{
						if (dataReader != null)
						{
							dataReader.Dispose();
						}
					}
				}
				finally
				{
					DbDataAdapter.QuietClose(connection, connectionState);
				}
			}
			finally
			{
				if (flag)
				{
					command.Transaction = null;
					command.Connection = null;
				}
			}
			return num;
		}

		/// <summary>Returns a <see cref="T:System.Data.IDataParameter" /> from one of the commands in the current batch.</summary>
		/// <returns>The <see cref="T:System.Data.IDataParameter" /> specified.</returns>
		/// <param name="commandIdentifier">The index of the command to retrieve the parameter from.</param>
		/// <param name="parameterIndex">The index of the parameter within the command.</param>
		/// <exception cref="T:System.NotSupportedException">The adapter does not support batches. </exception>
		// Token: 0x06002771 RID: 10097 RVA: 0x000621D6 File Offset: 0x000603D6
		protected virtual IDataParameter GetBatchedParameter(int commandIdentifier, int parameterIndex)
		{
			throw ADP.NotSupported();
		}

		/// <summary>Returns information about an individual update attempt within a larger batched update.</summary>
		/// <returns>Information about an individual update attempt within a larger batched update.</returns>
		/// <param name="commandIdentifier">The zero-based column ordinal of the individual command within the batch.</param>
		/// <param name="recordsAffected">The number of rows affected in the data store by the specified command within the batch.</param>
		/// <param name="error">An <see cref="T:System.Exception" /> thrown during execution of the specified command. Returns null (Nothing in Visual Basic) if no exception is thrown.</param>
		// Token: 0x06002772 RID: 10098 RVA: 0x000AF8C4 File Offset: 0x000ADAC4
		protected virtual bool GetBatchedRecordsAffected(int commandIdentifier, out int recordsAffected, out Exception error)
		{
			recordsAffected = 1;
			error = null;
			return true;
		}

		/// <summary>Gets the parameters set by the user when executing an SQL SELECT statement.</summary>
		/// <returns>An array of <see cref="T:System.Data.IDataParameter" /> objects that contains the parameters set by the user.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06002773 RID: 10099 RVA: 0x000AF8D0 File Offset: 0x000ADAD0
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public override IDataParameter[] GetFillParameters()
		{
			IDataParameter[] array = null;
			IDbCommand selectCommand = this._IDbDataAdapter.SelectCommand;
			if (selectCommand != null)
			{
				IDataParameterCollection parameters = selectCommand.Parameters;
				if (parameters != null)
				{
					array = new IDataParameter[parameters.Count];
					parameters.CopyTo(array, 0);
				}
			}
			if (array == null)
			{
				array = Array.Empty<IDataParameter>();
			}
			return array;
		}

		// Token: 0x06002774 RID: 10100 RVA: 0x000AF918 File Offset: 0x000ADB18
		internal DataTableMapping GetTableMapping(DataTable dataTable)
		{
			DataTableMapping dataTableMapping = null;
			int num = base.IndexOfDataSetTable(dataTable.TableName);
			if (-1 != num)
			{
				dataTableMapping = base.TableMappings[num];
			}
			if (dataTableMapping == null)
			{
				if (MissingMappingAction.Error == base.MissingMappingAction)
				{
					throw ADP.MissingTableMappingDestination(dataTable.TableName);
				}
				dataTableMapping = new DataTableMapping(dataTable.TableName, dataTable.TableName);
			}
			return dataTableMapping;
		}

		/// <summary>Initializes batching for the <see cref="T:System.Data.Common.DbDataAdapter" />.</summary>
		/// <exception cref="T:System.NotSupportedException">The adapter does not support batches. </exception>
		// Token: 0x06002775 RID: 10101 RVA: 0x000621D6 File Offset: 0x000603D6
		protected virtual void InitializeBatching()
		{
			throw ADP.NotSupported();
		}

		/// <summary>Raises the RowUpdated event of a .NET Framework data provider.</summary>
		/// <param name="value">A <see cref="T:System.Data.Common.RowUpdatedEventArgs" /> that contains the event data. </param>
		// Token: 0x06002776 RID: 10102 RVA: 0x00005E03 File Offset: 0x00004003
		protected virtual void OnRowUpdated(RowUpdatedEventArgs value)
		{
		}

		/// <summary>Raises the RowUpdating event of a .NET Framework data provider.</summary>
		/// <param name="value">An <see cref="T:System.Data.Common.RowUpdatingEventArgs" />  that contains the event data. </param>
		// Token: 0x06002777 RID: 10103 RVA: 0x00005E03 File Offset: 0x00004003
		protected virtual void OnRowUpdating(RowUpdatingEventArgs value)
		{
		}

		// Token: 0x06002778 RID: 10104 RVA: 0x000AF970 File Offset: 0x000ADB70
		private void ParameterInput(IDataParameterCollection parameters, StatementType typeIndex, DataRow row, DataTableMapping mappings)
		{
			MissingMappingAction updateMappingAction = this.UpdateMappingAction;
			MissingSchemaAction updateSchemaAction = this.UpdateSchemaAction;
			foreach (object obj in parameters)
			{
				IDataParameter dataParameter = (IDataParameter)obj;
				if (dataParameter != null && (ParameterDirection.Input & dataParameter.Direction) != (ParameterDirection)0)
				{
					string sourceColumn = dataParameter.SourceColumn;
					if (!string.IsNullOrEmpty(sourceColumn))
					{
						DataColumn dataColumn = mappings.GetDataColumn(sourceColumn, null, row.Table, updateMappingAction, updateSchemaAction);
						if (dataColumn != null)
						{
							DataRowVersion parameterSourceVersion = DbDataAdapter.GetParameterSourceVersion(typeIndex, dataParameter);
							dataParameter.Value = row[dataColumn, parameterSourceVersion];
						}
						else
						{
							dataParameter.Value = null;
						}
						DbParameter dbParameter = dataParameter as DbParameter;
						if (dbParameter != null && dbParameter.SourceColumnNullMapping)
						{
							dataParameter.Value = (ADP.IsNull(dataParameter.Value) ? DbDataAdapter.s_parameterValueNullValue : DbDataAdapter.s_parameterValueNonNullValue);
						}
					}
				}
			}
		}

		// Token: 0x06002779 RID: 10105 RVA: 0x000AFA64 File Offset: 0x000ADC64
		private void ParameterOutput(IDataParameter parameter, DataRow row, DataTableMapping mappings, MissingMappingAction missingMapping, MissingSchemaAction missingSchema)
		{
			if ((ParameterDirection.Output & parameter.Direction) != (ParameterDirection)0)
			{
				object value = parameter.Value;
				if (value != null)
				{
					string sourceColumn = parameter.SourceColumn;
					if (!string.IsNullOrEmpty(sourceColumn))
					{
						DataColumn dataColumn = mappings.GetDataColumn(sourceColumn, null, row.Table, missingMapping, missingSchema);
						if (dataColumn != null)
						{
							if (dataColumn.ReadOnly)
							{
								try
								{
									dataColumn.ReadOnly = false;
									row[dataColumn] = value;
									return;
								}
								finally
								{
									dataColumn.ReadOnly = true;
								}
							}
							row[dataColumn] = value;
						}
					}
				}
			}
		}

		// Token: 0x0600277A RID: 10106 RVA: 0x000AFAE4 File Offset: 0x000ADCE4
		private void ParameterOutput(IDataParameterCollection parameters, DataRow row, DataTableMapping mappings)
		{
			MissingMappingAction updateMappingAction = this.UpdateMappingAction;
			MissingSchemaAction updateSchemaAction = this.UpdateSchemaAction;
			foreach (object obj in parameters)
			{
				IDataParameter dataParameter = (IDataParameter)obj;
				if (dataParameter != null)
				{
					this.ParameterOutput(dataParameter, row, mappings, updateMappingAction, updateSchemaAction);
				}
			}
		}

		/// <summary>Ends batching for the <see cref="T:System.Data.Common.DbDataAdapter" />.</summary>
		/// <exception cref="T:System.NotSupportedException">The adapter does not support batches. </exception>
		// Token: 0x0600277B RID: 10107 RVA: 0x000621D6 File Offset: 0x000603D6
		protected virtual void TerminateBatching()
		{
			throw ADP.NotSupported();
		}

		/// <summary>Updates the values in the database by executing the respective INSERT, UPDATE, or DELETE statements for each inserted, updated, or deleted row in the specified <see cref="T:System.Data.DataSet" />.</summary>
		/// <returns>The number of rows successfully updated from the <see cref="T:System.Data.DataSet" />.</returns>
		/// <param name="dataSet">The <see cref="T:System.Data.DataSet" /> used to update the data source. </param>
		/// <exception cref="T:System.InvalidOperationException">The source table is invalid. </exception>
		/// <exception cref="T:System.Data.DBConcurrencyException">An attempt to execute an INSERT, UPDATE, or DELETE statement resulted in zero records affected. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x0600277C RID: 10108 RVA: 0x000AFB50 File Offset: 0x000ADD50
		public override int Update(DataSet dataSet)
		{
			return this.Update(dataSet, "Table");
		}

		/// <summary>Updates the values in the database by executing the respective INSERT, UPDATE, or DELETE statements for each inserted, updated, or deleted row in the specified array in the <see cref="T:System.Data.DataSet" />.</summary>
		/// <returns>The number of rows successfully updated from the <see cref="T:System.Data.DataSet" />.</returns>
		/// <param name="dataRows">An array of <see cref="T:System.Data.DataRow" /> objects used to update the data source. </param>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="T:System.Data.DataSet" /> is invalid. </exception>
		/// <exception cref="T:System.InvalidOperationException">The source table is invalid. </exception>
		/// <exception cref="T:System.SystemException">No <see cref="T:System.Data.DataRow" /> exists to update.-or- No <see cref="T:System.Data.DataTable" /> exists to update.-or- No <see cref="T:System.Data.DataSet" /> exists to use as a source. </exception>
		/// <exception cref="T:System.Data.DBConcurrencyException">An attempt to execute an INSERT, UPDATE, or DELETE statement resulted in zero records affected. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x0600277D RID: 10109 RVA: 0x000AFB60 File Offset: 0x000ADD60
		public int Update(DataRow[] dataRows)
		{
			long num = DataCommonEventSource.Log.EnterScope<int>("<comm.DbDataAdapter.Update|API> {0}, dataRows[]", base.ObjectID);
			int num3;
			try
			{
				int num2 = 0;
				if (dataRows == null)
				{
					throw ADP.ArgumentNull("dataRows");
				}
				if (dataRows.Length != 0)
				{
					DataTable dataTable = null;
					for (int i = 0; i < dataRows.Length; i++)
					{
						if (dataRows[i] != null && dataTable != dataRows[i].Table)
						{
							if (dataTable != null)
							{
								throw ADP.UpdateMismatchRowTable(i);
							}
							dataTable = dataRows[i].Table;
						}
					}
					if (dataTable != null)
					{
						DataTableMapping tableMapping = this.GetTableMapping(dataTable);
						num2 = this.Update(dataRows, tableMapping);
					}
				}
				num3 = num2;
			}
			finally
			{
				DataCommonEventSource.Log.ExitScope(num);
			}
			return num3;
		}

		/// <summary>Updates the values in the database by executing the respective INSERT, UPDATE, or DELETE statements for each inserted, updated, or deleted row in the specified <see cref="T:System.Data.DataTable" />.</summary>
		/// <returns>The number of rows successfully updated from the <see cref="T:System.Data.DataTable" />.</returns>
		/// <param name="dataTable">The <see cref="T:System.Data.DataTable" /> used to update the data source. </param>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="T:System.Data.DataSet" /> is invalid. </exception>
		/// <exception cref="T:System.InvalidOperationException">The source table is invalid. </exception>
		/// <exception cref="T:System.SystemException">No <see cref="T:System.Data.DataRow" /> exists to update.-or- No <see cref="T:System.Data.DataTable" /> exists to update.-or- No <see cref="T:System.Data.DataSet" /> exists to use as a source. </exception>
		/// <exception cref="T:System.Data.DBConcurrencyException">An attempt to execute an INSERT, UPDATE, or DELETE statement resulted in zero records affected. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x0600277E RID: 10110 RVA: 0x000AFC04 File Offset: 0x000ADE04
		public int Update(DataTable dataTable)
		{
			long num = DataCommonEventSource.Log.EnterScope<int>("<comm.DbDataAdapter.Update|API> {0}, dataTable", base.ObjectID);
			int num3;
			try
			{
				if (dataTable == null)
				{
					throw ADP.UpdateRequiresDataTable("dataTable");
				}
				DataTableMapping dataTableMapping = null;
				int num2 = base.IndexOfDataSetTable(dataTable.TableName);
				if (-1 != num2)
				{
					dataTableMapping = base.TableMappings[num2];
				}
				if (dataTableMapping == null)
				{
					if (MissingMappingAction.Error == base.MissingMappingAction)
					{
						throw ADP.MissingTableMappingDestination(dataTable.TableName);
					}
					dataTableMapping = new DataTableMapping("Table", dataTable.TableName);
				}
				num3 = this.UpdateFromDataTable(dataTable, dataTableMapping);
			}
			finally
			{
				DataCommonEventSource.Log.ExitScope(num);
			}
			return num3;
		}

		/// <summary>Updates the values in the database by executing the respective INSERT, UPDATE, or DELETE statements for each inserted, updated, or deleted row in the <see cref="T:System.Data.DataSet" />  with the specified <see cref="T:System.Data.DataTable" /> name.</summary>
		/// <returns>The number of rows successfully updated from the <see cref="T:System.Data.DataSet" />.</returns>
		/// <param name="dataSet">The <see cref="T:System.Data.DataSet" /> to use to update the data source. </param>
		/// <param name="srcTable">The name of the source table to use for table mapping. </param>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="T:System.Data.DataSet" /> is invalid. </exception>
		/// <exception cref="T:System.InvalidOperationException">The source table is invalid. </exception>
		/// <exception cref="T:System.Data.DBConcurrencyException">An attempt to execute an INSERT, UPDATE, or DELETE statement resulted in zero records affected. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x0600277F RID: 10111 RVA: 0x000AFCA8 File Offset: 0x000ADEA8
		public int Update(DataSet dataSet, string srcTable)
		{
			long num = DataCommonEventSource.Log.EnterScope<int, string>("<comm.DbDataAdapter.Update|API> {0}, dataSet, srcTable='{1}'", base.ObjectID, srcTable);
			int num3;
			try
			{
				if (dataSet == null)
				{
					throw ADP.UpdateRequiresNonNullDataSet("dataSet");
				}
				if (string.IsNullOrEmpty(srcTable))
				{
					throw ADP.UpdateRequiresSourceTableName("srcTable");
				}
				int num2 = 0;
				MissingMappingAction updateMappingAction = this.UpdateMappingAction;
				DataTableMapping tableMappingBySchemaAction = base.GetTableMappingBySchemaAction(srcTable, srcTable, this.UpdateMappingAction);
				MissingSchemaAction updateSchemaAction = this.UpdateSchemaAction;
				DataTable dataTableBySchemaAction = tableMappingBySchemaAction.GetDataTableBySchemaAction(dataSet, updateSchemaAction);
				if (dataTableBySchemaAction != null)
				{
					num2 = this.UpdateFromDataTable(dataTableBySchemaAction, tableMappingBySchemaAction);
				}
				else if (!base.HasTableMappings() || -1 == base.TableMappings.IndexOf(tableMappingBySchemaAction))
				{
					throw ADP.UpdateRequiresSourceTable(srcTable);
				}
				num3 = num2;
			}
			finally
			{
				DataCommonEventSource.Log.ExitScope(num);
			}
			return num3;
		}

		/// <summary>Updates the values in the database by executing the respective INSERT, UPDATE, or DELETE statements for each inserted, updated, or deleted row in the specified array of <see cref="T:System.Data.DataSet" /> objects.</summary>
		/// <returns>The number of rows successfully updated from the <see cref="T:System.Data.DataSet" />.</returns>
		/// <param name="dataRows">An array of <see cref="T:System.Data.DataRow" /> objects used to update the data source. </param>
		/// <param name="tableMapping">The <see cref="P:System.Data.IDataAdapter.TableMappings" /> collection to use. </param>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="T:System.Data.DataSet" /> is invalid. </exception>
		/// <exception cref="T:System.InvalidOperationException">The source table is invalid. </exception>
		/// <exception cref="T:System.SystemException">No <see cref="T:System.Data.DataRow" /> exists to update.-or- No <see cref="T:System.Data.DataTable" /> exists to update.-or- No <see cref="T:System.Data.DataSet" /> exists to use as a source. </exception>
		/// <exception cref="T:System.Data.DBConcurrencyException">An attempt to execute an INSERT, UPDATE, or DELETE statement resulted in zero records affected. </exception>
		// Token: 0x06002780 RID: 10112 RVA: 0x000AFD68 File Offset: 0x000ADF68
		protected virtual int Update(DataRow[] dataRows, DataTableMapping tableMapping)
		{
			long num = DataCommonEventSource.Log.EnterScope<int>("<comm.DbDataAdapter.Update|API> {0}, dataRows[], tableMapping", base.ObjectID);
			int i;
			try
			{
				int num2 = 0;
				IDbConnection[] array = new IDbConnection[5];
				ConnectionState[] array2 = new ConnectionState[5];
				bool flag = false;
				IDbCommand selectCommand = this._IDbDataAdapter.SelectCommand;
				if (selectCommand != null)
				{
					array[0] = selectCommand.Connection;
					if (array[0] != null)
					{
						array2[0] = array[0].State;
						flag = true;
					}
				}
				int num3 = Math.Min(this.UpdateBatchSize, dataRows.Length);
				if (num3 < 1)
				{
					num3 = dataRows.Length;
				}
				DbDataAdapter.BatchCommandInfo[] array3 = new DbDataAdapter.BatchCommandInfo[num3];
				DataRow[] array4 = new DataRow[num3];
				int num4 = 0;
				try
				{
					try
					{
						if (1 != num3)
						{
							this.InitializeBatching();
						}
						StatementType statementType = StatementType.Select;
						IDbCommand dbCommand = null;
						foreach (DataRow dataRow in dataRows)
						{
							if (dataRow != null)
							{
								bool flag2 = false;
								DataRowState rowState = dataRow.RowState;
								if (rowState <= DataRowState.Added)
								{
									if (rowState - DataRowState.Detached <= 1)
									{
										goto IL_059B;
									}
									if (rowState != DataRowState.Added)
									{
										goto IL_0115;
									}
									statementType = StatementType.Insert;
									dbCommand = this._IDbDataAdapter.InsertCommand;
								}
								else if (rowState != DataRowState.Deleted)
								{
									if (rowState != DataRowState.Modified)
									{
										goto IL_0115;
									}
									statementType = StatementType.Update;
									dbCommand = this._IDbDataAdapter.UpdateCommand;
								}
								else
								{
									statementType = StatementType.Delete;
									dbCommand = this._IDbDataAdapter.DeleteCommand;
								}
								RowUpdatingEventArgs rowUpdatingEventArgs = this.CreateRowUpdatingEvent(dataRow, dbCommand, statementType, tableMapping);
								try
								{
									dataRow.RowError = null;
									if (dbCommand != null)
									{
										this.ParameterInput(dbCommand.Parameters, statementType, dataRow, tableMapping);
									}
								}
								catch (Exception ex) when (ADP.IsCatchableExceptionType(ex))
								{
									ADP.TraceExceptionForCapture(ex);
									rowUpdatingEventArgs.Errors = ex;
									rowUpdatingEventArgs.Status = UpdateStatus.ErrorsOccurred;
								}
								this.OnRowUpdating(rowUpdatingEventArgs);
								IDbCommand command = rowUpdatingEventArgs.Command;
								flag2 = dbCommand != command;
								dbCommand = command;
								UpdateStatus status = rowUpdatingEventArgs.Status;
								if (status != UpdateStatus.Continue)
								{
									if (UpdateStatus.ErrorsOccurred == status)
									{
										this.UpdatingRowStatusErrors(rowUpdatingEventArgs, dataRow);
										goto IL_059B;
									}
									if (UpdateStatus.SkipCurrentRow == status)
									{
										if (DataRowState.Unchanged == dataRow.RowState)
										{
											num2++;
											goto IL_059B;
										}
										goto IL_059B;
									}
									else
									{
										if (UpdateStatus.SkipAllRemainingRows != status)
										{
											throw ADP.InvalidUpdateStatus(status);
										}
										if (DataRowState.Unchanged == dataRow.RowState)
										{
											num2++;
											break;
										}
										break;
									}
								}
								else
								{
									rowUpdatingEventArgs = null;
									RowUpdatedEventArgs rowUpdatedEventArgs = null;
									if (1 == num3)
									{
										if (dbCommand != null)
										{
											array3[0]._commandIdentifier = 0;
											array3[0]._parameterCount = dbCommand.Parameters.Count;
											array3[0]._statementType = statementType;
											array3[0]._updatedRowSource = dbCommand.UpdatedRowSource;
										}
										array3[0]._row = dataRow;
										array4[0] = dataRow;
										num4 = 1;
									}
									else
									{
										Exception ex2 = null;
										try
										{
											if (dbCommand != null)
											{
												if ((UpdateRowSource.FirstReturnedRecord & dbCommand.UpdatedRowSource) == UpdateRowSource.None)
												{
													array3[num4]._commandIdentifier = this.AddToBatch(dbCommand);
													array3[num4]._parameterCount = dbCommand.Parameters.Count;
													array3[num4]._row = dataRow;
													array3[num4]._statementType = statementType;
													array3[num4]._updatedRowSource = dbCommand.UpdatedRowSource;
													array4[num4] = dataRow;
													num4++;
													if (num4 < num3)
													{
														goto IL_059B;
													}
												}
												else
												{
													ex2 = ADP.ResultsNotAllowedDuringBatch();
												}
											}
											else
											{
												ex2 = ADP.UpdateRequiresCommand(statementType, flag2);
											}
										}
										catch (Exception ex3) when (ADP.IsCatchableExceptionType(ex3))
										{
											ADP.TraceExceptionForCapture(ex3);
											ex2 = ex3;
										}
										if (ex2 != null)
										{
											rowUpdatedEventArgs = this.CreateRowUpdatedEvent(dataRow, dbCommand, StatementType.Batch, tableMapping);
											rowUpdatedEventArgs.Errors = ex2;
											rowUpdatedEventArgs.Status = UpdateStatus.ErrorsOccurred;
											this.OnRowUpdated(rowUpdatedEventArgs);
											if (ex2 != rowUpdatedEventArgs.Errors)
											{
												for (int j = 0; j < array3.Length; j++)
												{
													array3[j]._errors = null;
												}
											}
											num2 += this.UpdatedRowStatus(rowUpdatedEventArgs, array3, num4);
											if (UpdateStatus.SkipAllRemainingRows == rowUpdatedEventArgs.Status)
											{
												break;
											}
											goto IL_059B;
										}
									}
									rowUpdatedEventArgs = this.CreateRowUpdatedEvent(dataRow, dbCommand, statementType, tableMapping);
									try
									{
										if (1 != num3)
										{
											IDbConnection connection = DbDataAdapter.GetConnection1(this);
											ConnectionState connectionState = this.UpdateConnectionOpen(connection, StatementType.Batch, array, array2, flag);
											rowUpdatedEventArgs.AdapterInit(array4);
											if (ConnectionState.Open == connectionState)
											{
												this.UpdateBatchExecute(array3, num4, rowUpdatedEventArgs);
											}
											else
											{
												rowUpdatedEventArgs.Errors = ADP.UpdateOpenConnectionRequired(StatementType.Batch, false, connectionState);
												rowUpdatedEventArgs.Status = UpdateStatus.ErrorsOccurred;
											}
										}
										else if (dbCommand != null)
										{
											IDbConnection connection2 = DbDataAdapter.GetConnection4(this, dbCommand, statementType, flag2);
											ConnectionState connectionState2 = this.UpdateConnectionOpen(connection2, statementType, array, array2, flag);
											if (ConnectionState.Open == connectionState2)
											{
												this.UpdateRowExecute(rowUpdatedEventArgs, dbCommand, statementType);
												array3[0]._recordsAffected = new int?(rowUpdatedEventArgs.RecordsAffected);
												array3[0]._errors = null;
											}
											else
											{
												rowUpdatedEventArgs.Errors = ADP.UpdateOpenConnectionRequired(statementType, flag2, connectionState2);
												rowUpdatedEventArgs.Status = UpdateStatus.ErrorsOccurred;
											}
										}
										else
										{
											rowUpdatedEventArgs.Errors = ADP.UpdateRequiresCommand(statementType, flag2);
											rowUpdatedEventArgs.Status = UpdateStatus.ErrorsOccurred;
										}
									}
									catch (Exception ex4) when (ADP.IsCatchableExceptionType(ex4))
									{
										ADP.TraceExceptionForCapture(ex4);
										rowUpdatedEventArgs.Errors = ex4;
										rowUpdatedEventArgs.Status = UpdateStatus.ErrorsOccurred;
									}
									bool flag3 = UpdateStatus.ErrorsOccurred == rowUpdatedEventArgs.Status;
									Exception errors = rowUpdatedEventArgs.Errors;
									this.OnRowUpdated(rowUpdatedEventArgs);
									if (errors != rowUpdatedEventArgs.Errors)
									{
										for (int k = 0; k < array3.Length; k++)
										{
											array3[k]._errors = null;
										}
									}
									num2 += this.UpdatedRowStatus(rowUpdatedEventArgs, array3, num4);
									if (UpdateStatus.SkipAllRemainingRows != rowUpdatedEventArgs.Status)
									{
										if (1 != num3)
										{
											this.ClearBatch();
											num4 = 0;
										}
										for (int l = 0; l < array3.Length; l++)
										{
											array3[l] = default(DbDataAdapter.BatchCommandInfo);
										}
										num4 = 0;
										goto IL_059B;
									}
									if (flag3 && 1 != num3)
									{
										this.ClearBatch();
										num4 = 0;
										break;
									}
									break;
								}
								IL_0115:
								throw ADP.InvalidDataRowState(dataRow.RowState);
							}
							IL_059B:;
						}
						if (1 != num3 && 0 < num4)
						{
							RowUpdatedEventArgs rowUpdatedEventArgs2 = this.CreateRowUpdatedEvent(null, dbCommand, statementType, tableMapping);
							try
							{
								IDbConnection connection3 = DbDataAdapter.GetConnection1(this);
								ConnectionState connectionState3 = this.UpdateConnectionOpen(connection3, StatementType.Batch, array, array2, flag);
								DataRow[] array5 = array4;
								if (num4 < array4.Length)
								{
									array5 = new DataRow[num4];
									Array.Copy(array4, 0, array5, 0, num4);
								}
								rowUpdatedEventArgs2.AdapterInit(array5);
								if (ConnectionState.Open == connectionState3)
								{
									this.UpdateBatchExecute(array3, num4, rowUpdatedEventArgs2);
								}
								else
								{
									rowUpdatedEventArgs2.Errors = ADP.UpdateOpenConnectionRequired(StatementType.Batch, false, connectionState3);
									rowUpdatedEventArgs2.Status = UpdateStatus.ErrorsOccurred;
								}
							}
							catch (Exception ex5) when (ADP.IsCatchableExceptionType(ex5))
							{
								ADP.TraceExceptionForCapture(ex5);
								rowUpdatedEventArgs2.Errors = ex5;
								rowUpdatedEventArgs2.Status = UpdateStatus.ErrorsOccurred;
							}
							Exception errors2 = rowUpdatedEventArgs2.Errors;
							this.OnRowUpdated(rowUpdatedEventArgs2);
							if (errors2 != rowUpdatedEventArgs2.Errors)
							{
								for (int m = 0; m < array3.Length; m++)
								{
									array3[m]._errors = null;
								}
							}
							num2 += this.UpdatedRowStatus(rowUpdatedEventArgs2, array3, num4);
						}
					}
					finally
					{
						if (1 != num3)
						{
							this.TerminateBatching();
						}
					}
				}
				finally
				{
					for (int n = 0; n < array.Length; n++)
					{
						DbDataAdapter.QuietClose(array[n], array2[n]);
					}
				}
				i = num2;
			}
			finally
			{
				DataCommonEventSource.Log.ExitScope(num);
			}
			return i;
		}

		// Token: 0x06002781 RID: 10113 RVA: 0x000B051C File Offset: 0x000AE71C
		private void UpdateBatchExecute(DbDataAdapter.BatchCommandInfo[] batchCommands, int commandCount, RowUpdatedEventArgs rowUpdatedEvent)
		{
			try
			{
				int num = this.ExecuteBatch();
				rowUpdatedEvent.AdapterInit(num);
			}
			catch (DbException ex)
			{
				ADP.TraceExceptionForCapture(ex);
				rowUpdatedEvent.Errors = ex;
				rowUpdatedEvent.Status = UpdateStatus.ErrorsOccurred;
			}
			MissingMappingAction updateMappingAction = this.UpdateMappingAction;
			MissingSchemaAction updateSchemaAction = this.UpdateSchemaAction;
			int num2 = 0;
			bool flag = false;
			List<DataRow> list = null;
			for (int i = 0; i < commandCount; i++)
			{
				DbDataAdapter.BatchCommandInfo batchCommandInfo = batchCommands[i];
				StatementType statementType = batchCommandInfo._statementType;
				int num3;
				if (this.GetBatchedRecordsAffected(batchCommandInfo._commandIdentifier, out num3, out batchCommands[i]._errors))
				{
					batchCommands[i]._recordsAffected = new int?(num3);
				}
				if (batchCommands[i]._errors == null && batchCommands[i]._recordsAffected != null)
				{
					if (StatementType.Update == statementType || StatementType.Delete == statementType)
					{
						num2++;
						if (num3 == 0)
						{
							if (list == null)
							{
								list = new List<DataRow>();
							}
							batchCommands[i]._errors = ADP.UpdateConcurrencyViolation(batchCommands[i]._statementType, 0, 1, new DataRow[] { rowUpdatedEvent.Rows[i] });
							flag = true;
							list.Add(rowUpdatedEvent.Rows[i]);
						}
					}
					if ((StatementType.Insert == statementType || StatementType.Update == statementType) && (UpdateRowSource.OutputParameters & batchCommandInfo._updatedRowSource) != UpdateRowSource.None && num3 != 0)
					{
						if (StatementType.Insert == statementType)
						{
							rowUpdatedEvent.Rows[i].AcceptChanges();
						}
						for (int j = 0; j < batchCommandInfo._parameterCount; j++)
						{
							IDataParameter batchedParameter = this.GetBatchedParameter(batchCommandInfo._commandIdentifier, j);
							this.ParameterOutput(batchedParameter, batchCommandInfo._row, rowUpdatedEvent.TableMapping, updateMappingAction, updateSchemaAction);
						}
					}
				}
			}
			if (rowUpdatedEvent.Errors == null && rowUpdatedEvent.Status == UpdateStatus.Continue && 0 < num2 && (rowUpdatedEvent.RecordsAffected == 0 || flag))
			{
				DataRow[] array = ((list != null) ? list.ToArray() : rowUpdatedEvent.Rows);
				rowUpdatedEvent.Errors = ADP.UpdateConcurrencyViolation(StatementType.Batch, commandCount - array.Length, commandCount, array);
				rowUpdatedEvent.Status = UpdateStatus.ErrorsOccurred;
			}
		}

		// Token: 0x06002782 RID: 10114 RVA: 0x000B071C File Offset: 0x000AE91C
		private ConnectionState UpdateConnectionOpen(IDbConnection connection, StatementType statementType, IDbConnection[] connections, ConnectionState[] connectionStates, bool useSelectConnectionState)
		{
			if (connection != connections[(int)statementType])
			{
				DbDataAdapter.QuietClose(connections[(int)statementType], connectionStates[(int)statementType]);
				connections[(int)statementType] = connection;
				connectionStates[(int)statementType] = ConnectionState.Closed;
				DbDataAdapter.QuietOpen(connection, out connectionStates[(int)statementType]);
				if (useSelectConnectionState && connections[0] == connection)
				{
					connectionStates[(int)statementType] = connections[0].State;
				}
			}
			return connection.State;
		}

		// Token: 0x06002783 RID: 10115 RVA: 0x000B0770 File Offset: 0x000AE970
		private int UpdateFromDataTable(DataTable dataTable, DataTableMapping tableMapping)
		{
			int num = 0;
			DataRow[] array = ADP.SelectAdapterRows(dataTable, false);
			if (array != null && array.Length != 0)
			{
				num = this.Update(array, tableMapping);
			}
			return num;
		}

		// Token: 0x06002784 RID: 10116 RVA: 0x000B0798 File Offset: 0x000AE998
		private void UpdateRowExecute(RowUpdatedEventArgs rowUpdatedEvent, IDbCommand dataCommand, StatementType cmdIndex)
		{
			bool flag = true;
			UpdateRowSource updatedRowSource = dataCommand.UpdatedRowSource;
			if (StatementType.Delete == cmdIndex || (UpdateRowSource.FirstReturnedRecord & updatedRowSource) == UpdateRowSource.None)
			{
				int num = dataCommand.ExecuteNonQuery();
				rowUpdatedEvent.AdapterInit(num);
			}
			else if (StatementType.Insert == cmdIndex || StatementType.Update == cmdIndex)
			{
				using (IDataReader dataReader = dataCommand.ExecuteReader(CommandBehavior.SequentialAccess))
				{
					DataReaderContainer dataReaderContainer = DataReaderContainer.Create(dataReader, this.ReturnProviderSpecificTypes);
					try
					{
						bool flag2 = false;
						while (0 >= dataReaderContainer.FieldCount)
						{
							if (!dataReader.NextResult())
							{
								IL_0061:
								if (flag2 && dataReader.RecordsAffected != 0)
								{
									SchemaMapping schemaMapping = new SchemaMapping(this, null, rowUpdatedEvent.Row.Table, dataReaderContainer, false, SchemaType.Mapped, rowUpdatedEvent.TableMapping.SourceTable, true, null, null);
									if (schemaMapping.DataTable != null && schemaMapping.DataValues != null && dataReader.Read())
									{
										if (StatementType.Insert == cmdIndex && flag)
										{
											rowUpdatedEvent.Row.AcceptChanges();
											flag = false;
										}
										schemaMapping.ApplyToDataRow(rowUpdatedEvent.Row);
									}
								}
								goto IL_00F2;
							}
						}
						flag2 = true;
						goto IL_0061;
					}
					finally
					{
						dataReader.Close();
						int recordsAffected = dataReader.RecordsAffected;
						rowUpdatedEvent.AdapterInit(recordsAffected);
					}
				}
			}
			IL_00F2:
			if ((StatementType.Insert == cmdIndex || StatementType.Update == cmdIndex) && (UpdateRowSource.OutputParameters & updatedRowSource) != UpdateRowSource.None && rowUpdatedEvent.RecordsAffected != 0)
			{
				if (StatementType.Insert == cmdIndex && flag)
				{
					rowUpdatedEvent.Row.AcceptChanges();
				}
				this.ParameterOutput(dataCommand.Parameters, rowUpdatedEvent.Row, rowUpdatedEvent.TableMapping);
			}
			if (rowUpdatedEvent.Status == UpdateStatus.Continue && cmdIndex - StatementType.Update <= 1 && rowUpdatedEvent.RecordsAffected == 0)
			{
				rowUpdatedEvent.Errors = ADP.UpdateConcurrencyViolation(cmdIndex, rowUpdatedEvent.RecordsAffected, 1, new DataRow[] { rowUpdatedEvent.Row });
				rowUpdatedEvent.Status = UpdateStatus.ErrorsOccurred;
			}
		}

		// Token: 0x06002785 RID: 10117 RVA: 0x000B0938 File Offset: 0x000AEB38
		private int UpdatedRowStatus(RowUpdatedEventArgs rowUpdatedEvent, DbDataAdapter.BatchCommandInfo[] batchCommands, int commandCount)
		{
			int num;
			switch (rowUpdatedEvent.Status)
			{
			case UpdateStatus.Continue:
				num = this.UpdatedRowStatusContinue(rowUpdatedEvent, batchCommands, commandCount);
				break;
			case UpdateStatus.ErrorsOccurred:
				num = this.UpdatedRowStatusErrors(rowUpdatedEvent, batchCommands, commandCount);
				break;
			case UpdateStatus.SkipCurrentRow:
			case UpdateStatus.SkipAllRemainingRows:
				num = this.UpdatedRowStatusSkip(batchCommands, commandCount);
				break;
			default:
				throw ADP.InvalidUpdateStatus(rowUpdatedEvent.Status);
			}
			return num;
		}

		// Token: 0x06002786 RID: 10118 RVA: 0x000B0998 File Offset: 0x000AEB98
		private int UpdatedRowStatusContinue(RowUpdatedEventArgs rowUpdatedEvent, DbDataAdapter.BatchCommandInfo[] batchCommands, int commandCount)
		{
			int num = 0;
			bool acceptChangesDuringUpdate = base.AcceptChangesDuringUpdate;
			for (int i = 0; i < commandCount; i++)
			{
				DataRow row = batchCommands[i]._row;
				if (batchCommands[i]._errors == null && batchCommands[i]._recordsAffected != null && batchCommands[i]._recordsAffected.Value != 0)
				{
					if (acceptChangesDuringUpdate && ((DataRowState.Added | DataRowState.Deleted | DataRowState.Modified) & row.RowState) != (DataRowState)0)
					{
						row.AcceptChanges();
					}
					num++;
				}
			}
			return num;
		}

		// Token: 0x06002787 RID: 10119 RVA: 0x000B0A14 File Offset: 0x000AEC14
		private int UpdatedRowStatusErrors(RowUpdatedEventArgs rowUpdatedEvent, DbDataAdapter.BatchCommandInfo[] batchCommands, int commandCount)
		{
			Exception ex = rowUpdatedEvent.Errors;
			if (ex == null)
			{
				ex = ADP.RowUpdatedErrors();
				rowUpdatedEvent.Errors = ex;
			}
			int num = 0;
			bool flag = false;
			string message = ex.Message;
			for (int i = 0; i < commandCount; i++)
			{
				DataRow row = batchCommands[i]._row;
				if (batchCommands[i]._errors != null)
				{
					string text = batchCommands[i]._errors.Message;
					if (string.IsNullOrEmpty(text))
					{
						text = message;
					}
					DataRow dataRow = row;
					dataRow.RowError += text;
					flag = true;
				}
			}
			if (!flag)
			{
				for (int j = 0; j < commandCount; j++)
				{
					DataRow row2 = batchCommands[j]._row;
					row2.RowError += message;
				}
			}
			else
			{
				num = this.UpdatedRowStatusContinue(rowUpdatedEvent, batchCommands, commandCount);
			}
			if (!base.ContinueUpdateOnError)
			{
				throw ex;
			}
			return num;
		}

		// Token: 0x06002788 RID: 10120 RVA: 0x000B0AF0 File Offset: 0x000AECF0
		private int UpdatedRowStatusSkip(DbDataAdapter.BatchCommandInfo[] batchCommands, int commandCount)
		{
			int num = 0;
			for (int i = 0; i < commandCount; i++)
			{
				DataRow row = batchCommands[i]._row;
				if (((DataRowState.Detached | DataRowState.Unchanged) & row.RowState) != (DataRowState)0)
				{
					num++;
				}
			}
			return num;
		}

		// Token: 0x06002789 RID: 10121 RVA: 0x000B0B28 File Offset: 0x000AED28
		private void UpdatingRowStatusErrors(RowUpdatingEventArgs rowUpdatedEvent, DataRow dataRow)
		{
			Exception ex = rowUpdatedEvent.Errors;
			if (ex == null)
			{
				ex = ADP.RowUpdatingErrors();
				rowUpdatedEvent.Errors = ex;
			}
			string message = ex.Message;
			dataRow.RowError += message;
			if (!base.ContinueUpdateOnError)
			{
				throw ex;
			}
		}

		// Token: 0x0600278A RID: 10122 RVA: 0x000B0B70 File Offset: 0x000AED70
		private static IDbConnection GetConnection1(DbDataAdapter adapter)
		{
			IDbCommand dbCommand = adapter._IDbDataAdapter.SelectCommand;
			if (dbCommand == null)
			{
				dbCommand = adapter._IDbDataAdapter.InsertCommand;
				if (dbCommand == null)
				{
					dbCommand = adapter._IDbDataAdapter.UpdateCommand;
					if (dbCommand == null)
					{
						dbCommand = adapter._IDbDataAdapter.DeleteCommand;
					}
				}
			}
			IDbConnection dbConnection = null;
			if (dbCommand != null)
			{
				dbConnection = dbCommand.Connection;
			}
			if (dbConnection == null)
			{
				throw ADP.UpdateConnectionRequired(StatementType.Batch, false);
			}
			return dbConnection;
		}

		// Token: 0x0600278B RID: 10123 RVA: 0x000B0BD0 File Offset: 0x000AEDD0
		private static IDbConnection GetConnection3(DbDataAdapter adapter, IDbCommand command, string method)
		{
			IDbConnection connection = command.Connection;
			if (connection == null)
			{
				throw ADP.ConnectionRequired_Res(method);
			}
			return connection;
		}

		// Token: 0x0600278C RID: 10124 RVA: 0x000B0BF0 File Offset: 0x000AEDF0
		private static IDbConnection GetConnection4(DbDataAdapter adapter, IDbCommand command, StatementType statementType, bool isCommandFromRowUpdating)
		{
			IDbConnection connection = command.Connection;
			if (connection == null)
			{
				throw ADP.UpdateConnectionRequired(statementType, isCommandFromRowUpdating);
			}
			return connection;
		}

		// Token: 0x0600278D RID: 10125 RVA: 0x000B0C10 File Offset: 0x000AEE10
		private static DataRowVersion GetParameterSourceVersion(StatementType statementType, IDataParameter parameter)
		{
			switch (statementType)
			{
			case StatementType.Select:
			case StatementType.Batch:
				throw ADP.UnwantedStatementType(statementType);
			case StatementType.Insert:
				return DataRowVersion.Current;
			case StatementType.Update:
				return parameter.SourceVersion;
			case StatementType.Delete:
				return DataRowVersion.Original;
			default:
				throw ADP.InvalidStatementType(statementType);
			}
		}

		// Token: 0x0600278E RID: 10126 RVA: 0x000B0C4E File Offset: 0x000AEE4E
		private static void QuietClose(IDbConnection connection, ConnectionState originalState)
		{
			if (connection != null && originalState == ConnectionState.Closed)
			{
				connection.Close();
			}
		}

		// Token: 0x0600278F RID: 10127 RVA: 0x000B0C5C File Offset: 0x000AEE5C
		private static void QuietOpen(IDbConnection connection, out ConnectionState originalState)
		{
			originalState = connection.State;
			if (originalState == ConnectionState.Closed)
			{
				connection.Open();
			}
		}

		/// <summary>The default name used by the <see cref="T:System.Data.Common.DataAdapter" /> object for table mappings.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x040018BC RID: 6332
		public const string DefaultSourceTableName = "Table";

		// Token: 0x040018BD RID: 6333
		internal static readonly object s_parameterValueNonNullValue = 0;

		// Token: 0x040018BE RID: 6334
		internal static readonly object s_parameterValueNullValue = 1;

		// Token: 0x040018BF RID: 6335
		private IDbCommand _deleteCommand;

		// Token: 0x040018C0 RID: 6336
		private IDbCommand _insertCommand;

		// Token: 0x040018C1 RID: 6337
		private IDbCommand _selectCommand;

		// Token: 0x040018C2 RID: 6338
		private IDbCommand _updateCommand;

		// Token: 0x040018C3 RID: 6339
		private CommandBehavior _fillCommandBehavior;

		// Token: 0x02000344 RID: 836
		private struct BatchCommandInfo
		{
			// Token: 0x040018C4 RID: 6340
			internal int _commandIdentifier;

			// Token: 0x040018C5 RID: 6341
			internal int _parameterCount;

			// Token: 0x040018C6 RID: 6342
			internal DataRow _row;

			// Token: 0x040018C7 RID: 6343
			internal StatementType _statementType;

			// Token: 0x040018C8 RID: 6344
			internal UpdateRowSource _updatedRowSource;

			// Token: 0x040018C9 RID: 6345
			internal int? _recordsAffected;

			// Token: 0x040018CA RID: 6346
			internal Exception _errors;
		}
	}
}
