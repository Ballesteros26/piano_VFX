using System;
using System.Data.Common;

namespace System.Data.Odbc
{
	/// <summary>Represents a set of data commands and a connection to a data source that are used to fill the <see cref="T:System.Data.DataSet" /> and update the data source. This class cannot be inherited.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000296 RID: 662
	public sealed class OdbcDataAdapter : DbDataAdapter, IDbDataAdapter, IDataAdapter, ICloneable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.Odbc.OdbcDataAdapter" /> class.</summary>
		// Token: 0x06001BF6 RID: 7158 RVA: 0x0008A6C6 File Offset: 0x000888C6
		public OdbcDataAdapter()
		{
			GC.SuppressFinalize(this);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.Odbc.OdbcDataAdapter" /> class with the specified SQL SELECT statement.</summary>
		/// <param name="selectCommand">An <see cref="T:System.Data.Odbc.OdbcCommand" /> that is an SQL SELECT statement or stored procedure, and is set as the <see cref="P:System.Data.Odbc.OdbcDataAdapter.SelectCommand" /> property of the <see cref="T:System.Data.Odbc.OdbcDataAdapter" />. </param>
		// Token: 0x06001BF7 RID: 7159 RVA: 0x0008A6D4 File Offset: 0x000888D4
		public OdbcDataAdapter(OdbcCommand selectCommand)
			: this()
		{
			this.SelectCommand = selectCommand;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.Odbc.OdbcDataAdapter" /> class with an SQL SELECT statement and an <see cref="T:System.Data.Odbc.OdbcConnection" />.</summary>
		/// <param name="selectCommandText">A string that is a SQL SELECT statement or stored procedure to be used by the <see cref="P:System.Data.Odbc.OdbcDataAdapter.SelectCommand" /> property of the <see cref="T:System.Data.Odbc.OdbcDataAdapter" />. </param>
		/// <param name="selectConnection">An <see cref="T:System.Data.Odbc.OdbcConnection" /> that represents the connection. </param>
		// Token: 0x06001BF8 RID: 7160 RVA: 0x0008A6E3 File Offset: 0x000888E3
		public OdbcDataAdapter(string selectCommandText, OdbcConnection selectConnection)
			: this()
		{
			this.SelectCommand = new OdbcCommand(selectCommandText, selectConnection);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.Odbc.OdbcDataAdapter" /> class with an SQL SELECT statement and a connection string.</summary>
		/// <param name="selectCommandText">A string that is a SQL SELECT statement or stored procedure to be used by the <see cref="P:System.Data.Odbc.OdbcDataAdapter.SelectCommand" /> property of the <see cref="T:System.Data.Odbc.OdbcDataAdapter" />. </param>
		/// <param name="selectConnectionString">The connection string. </param>
		// Token: 0x06001BF9 RID: 7161 RVA: 0x0008A6F8 File Offset: 0x000888F8
		public OdbcDataAdapter(string selectCommandText, string selectConnectionString)
			: this()
		{
			OdbcConnection odbcConnection = new OdbcConnection(selectConnectionString);
			this.SelectCommand = new OdbcCommand(selectCommandText, odbcConnection);
		}

		// Token: 0x06001BFA RID: 7162 RVA: 0x0008A71F File Offset: 0x0008891F
		private OdbcDataAdapter(OdbcDataAdapter from)
			: base(from)
		{
			GC.SuppressFinalize(this);
		}

		/// <summary>Gets or sets an SQL statement or stored procedure used to delete records in the data source.</summary>
		/// <returns>An <see cref="T:System.Data.Odbc.OdbcCommand" /> used during an update operation to delete records in the data source that correspond to deleted rows in the <see cref="T:System.Data.DataSet" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000521 RID: 1313
		// (get) Token: 0x06001BFB RID: 7163 RVA: 0x0008A72E File Offset: 0x0008892E
		// (set) Token: 0x06001BFC RID: 7164 RVA: 0x0008A736 File Offset: 0x00088936
		public new OdbcCommand DeleteCommand
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

		/// <summary>For a description of this member, see <see cref="M:System.Data.IDbDataAdapter.DeleteCommand" />.</summary>
		/// <returns>An <see cref="T:System.Data.IDbCommand" /> used during an update to delete records in the data source for deleted rows in the data set.</returns>
		// Token: 0x17000522 RID: 1314
		// (get) Token: 0x06001BFD RID: 7165 RVA: 0x0008A72E File Offset: 0x0008892E
		// (set) Token: 0x06001BFE RID: 7166 RVA: 0x0008A73F File Offset: 0x0008893F
		IDbCommand IDbDataAdapter.DeleteCommand
		{
			get
			{
				return this._deleteCommand;
			}
			set
			{
				this._deleteCommand = (OdbcCommand)value;
			}
		}

		/// <summary>Gets or sets an SQL statement or stored procedure used to insert new records into the data source.</summary>
		/// <returns>An <see cref="T:System.Data.Odbc.OdbcCommand" /> used during an update operation to insert records in the data source that correspond to new rows in the <see cref="T:System.Data.DataSet" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000523 RID: 1315
		// (get) Token: 0x06001BFF RID: 7167 RVA: 0x0008A74D File Offset: 0x0008894D
		// (set) Token: 0x06001C00 RID: 7168 RVA: 0x0008A755 File Offset: 0x00088955
		public new OdbcCommand InsertCommand
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

		/// <summary>For a description of this member, see <see cref="M:System.Data.IDbDataAdapter.InsertCommand" />.</summary>
		/// <returns>An <see cref="T:System.Data.IDbCommand" /> that is used during an update to insert records from a data source for placement in the data set.</returns>
		// Token: 0x17000524 RID: 1316
		// (get) Token: 0x06001C01 RID: 7169 RVA: 0x0008A74D File Offset: 0x0008894D
		// (set) Token: 0x06001C02 RID: 7170 RVA: 0x0008A75E File Offset: 0x0008895E
		IDbCommand IDbDataAdapter.InsertCommand
		{
			get
			{
				return this._insertCommand;
			}
			set
			{
				this._insertCommand = (OdbcCommand)value;
			}
		}

		/// <summary>Gets or sets an SQL statement or stored procedure used to select records in the data source.</summary>
		/// <returns>An <see cref="T:System.Data.Odbc.OdbcCommand" /> that is used during a fill operation to select records from data source for placement in the <see cref="T:System.Data.DataSet" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000525 RID: 1317
		// (get) Token: 0x06001C03 RID: 7171 RVA: 0x0008A76C File Offset: 0x0008896C
		// (set) Token: 0x06001C04 RID: 7172 RVA: 0x0008A774 File Offset: 0x00088974
		public new OdbcCommand SelectCommand
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

		/// <summary>For a description of this member, see <see cref="M:System.Data.IDbDataAdapter.SelectCommand" />.</summary>
		/// <returns>An <see cref="T:System.Data.IDbCommand" /> that is used during an update to select records from a data source for placement in the data set.</returns>
		// Token: 0x17000526 RID: 1318
		// (get) Token: 0x06001C05 RID: 7173 RVA: 0x0008A76C File Offset: 0x0008896C
		// (set) Token: 0x06001C06 RID: 7174 RVA: 0x0008A77D File Offset: 0x0008897D
		IDbCommand IDbDataAdapter.SelectCommand
		{
			get
			{
				return this._selectCommand;
			}
			set
			{
				this._selectCommand = (OdbcCommand)value;
			}
		}

		/// <summary>Gets or sets an SQL statement or stored procedure used to update records in the data source.</summary>
		/// <returns>An <see cref="T:System.Data.Odbc.OdbcCommand" /> used during an update operation to update records in the data source that correspond to modified rows in the <see cref="T:System.Data.DataSet" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000527 RID: 1319
		// (get) Token: 0x06001C07 RID: 7175 RVA: 0x0008A78B File Offset: 0x0008898B
		// (set) Token: 0x06001C08 RID: 7176 RVA: 0x0008A793 File Offset: 0x00088993
		public new OdbcCommand UpdateCommand
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

		/// <summary>For a description of this member, see <see cref="M:System.Data.IDbDataAdapter.UpdateCommand" />.</summary>
		/// <returns>An <see cref="T:System.Data.IDbCommand" /> used during an update to update records in the data source for modified rows in the data set.</returns>
		// Token: 0x17000528 RID: 1320
		// (get) Token: 0x06001C09 RID: 7177 RVA: 0x0008A78B File Offset: 0x0008898B
		// (set) Token: 0x06001C0A RID: 7178 RVA: 0x0008A79C File Offset: 0x0008899C
		IDbCommand IDbDataAdapter.UpdateCommand
		{
			get
			{
				return this._updateCommand;
			}
			set
			{
				this._updateCommand = (OdbcCommand)value;
			}
		}

		/// <summary>Occurs during an update operation after a command is executed against the data source.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1400002A RID: 42
		// (add) Token: 0x06001C0B RID: 7179 RVA: 0x0008A7AA File Offset: 0x000889AA
		// (remove) Token: 0x06001C0C RID: 7180 RVA: 0x0008A7BD File Offset: 0x000889BD
		public event OdbcRowUpdatedEventHandler RowUpdated
		{
			add
			{
				base.Events.AddHandler(OdbcDataAdapter.s_eventRowUpdated, value);
			}
			remove
			{
				base.Events.RemoveHandler(OdbcDataAdapter.s_eventRowUpdated, value);
			}
		}

		/// <summary>Occurs during <see cref="M:System.Data.Common.DbDataAdapter.Update(System.Data.DataSet)" /> before a command is executed against the data source.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1400002B RID: 43
		// (add) Token: 0x06001C0D RID: 7181 RVA: 0x0008A7D0 File Offset: 0x000889D0
		// (remove) Token: 0x06001C0E RID: 7182 RVA: 0x0008A834 File Offset: 0x00088A34
		public event OdbcRowUpdatingEventHandler RowUpdating
		{
			add
			{
				OdbcRowUpdatingEventHandler odbcRowUpdatingEventHandler = (OdbcRowUpdatingEventHandler)base.Events[OdbcDataAdapter.s_eventRowUpdating];
				if (odbcRowUpdatingEventHandler != null && value.Target is OdbcCommandBuilder)
				{
					OdbcRowUpdatingEventHandler odbcRowUpdatingEventHandler2 = (OdbcRowUpdatingEventHandler)ADP.FindBuilder(odbcRowUpdatingEventHandler);
					if (odbcRowUpdatingEventHandler2 != null)
					{
						base.Events.RemoveHandler(OdbcDataAdapter.s_eventRowUpdating, odbcRowUpdatingEventHandler2);
					}
				}
				base.Events.AddHandler(OdbcDataAdapter.s_eventRowUpdating, value);
			}
			remove
			{
				base.Events.RemoveHandler(OdbcDataAdapter.s_eventRowUpdating, value);
			}
		}

		/// <summary>For a description of this member, see <see cref="M:System.ICloneable.Clone" />.</summary>
		/// <returns>A new <see cref="T:System.Object" /> that is a copy of this instance.</returns>
		// Token: 0x06001C0F RID: 7183 RVA: 0x0008A847 File Offset: 0x00088A47
		object ICloneable.Clone()
		{
			return new OdbcDataAdapter(this);
		}

		// Token: 0x06001C10 RID: 7184 RVA: 0x0008A84F File Offset: 0x00088A4F
		protected override RowUpdatedEventArgs CreateRowUpdatedEvent(DataRow dataRow, IDbCommand command, StatementType statementType, DataTableMapping tableMapping)
		{
			return new OdbcRowUpdatedEventArgs(dataRow, command, statementType, tableMapping);
		}

		// Token: 0x06001C11 RID: 7185 RVA: 0x0008A85B File Offset: 0x00088A5B
		protected override RowUpdatingEventArgs CreateRowUpdatingEvent(DataRow dataRow, IDbCommand command, StatementType statementType, DataTableMapping tableMapping)
		{
			return new OdbcRowUpdatingEventArgs(dataRow, command, statementType, tableMapping);
		}

		// Token: 0x06001C12 RID: 7186 RVA: 0x0008A868 File Offset: 0x00088A68
		protected override void OnRowUpdated(RowUpdatedEventArgs value)
		{
			OdbcRowUpdatedEventHandler odbcRowUpdatedEventHandler = (OdbcRowUpdatedEventHandler)base.Events[OdbcDataAdapter.s_eventRowUpdated];
			if (odbcRowUpdatedEventHandler != null && value is OdbcRowUpdatedEventArgs)
			{
				odbcRowUpdatedEventHandler(this, (OdbcRowUpdatedEventArgs)value);
			}
			base.OnRowUpdated(value);
		}

		// Token: 0x06001C13 RID: 7187 RVA: 0x0008A8AC File Offset: 0x00088AAC
		protected override void OnRowUpdating(RowUpdatingEventArgs value)
		{
			OdbcRowUpdatingEventHandler odbcRowUpdatingEventHandler = (OdbcRowUpdatingEventHandler)base.Events[OdbcDataAdapter.s_eventRowUpdating];
			if (odbcRowUpdatingEventHandler != null && value is OdbcRowUpdatingEventArgs)
			{
				odbcRowUpdatingEventHandler(this, (OdbcRowUpdatingEventArgs)value);
			}
			base.OnRowUpdating(value);
		}

		// Token: 0x04001505 RID: 5381
		private static readonly object s_eventRowUpdated = new object();

		// Token: 0x04001506 RID: 5382
		private static readonly object s_eventRowUpdating = new object();

		// Token: 0x04001507 RID: 5383
		private OdbcCommand _deleteCommand;

		// Token: 0x04001508 RID: 5384
		private OdbcCommand _insertCommand;

		// Token: 0x04001509 RID: 5385
		private OdbcCommand _selectCommand;

		// Token: 0x0400150A RID: 5386
		private OdbcCommand _updateCommand;
	}
}
