using System;
using System.Data.Common;

namespace System.Data.SqlClient
{
	/// <summary>Represents a set of data commands and a database connection that are used to fill the <see cref="T:System.Data.DataSet" /> and update a SQL Server database. This class cannot be inherited.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x020001A1 RID: 417
	public sealed class SqlDataAdapter : DbDataAdapter, IDbDataAdapter, IDataAdapter, ICloneable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlClient.SqlDataAdapter" /> class.</summary>
		// Token: 0x06001373 RID: 4979 RVA: 0x0005FD6D File Offset: 0x0005DF6D
		public SqlDataAdapter()
		{
			GC.SuppressFinalize(this);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlClient.SqlDataAdapter" /> class with the specified <see cref="T:System.Data.SqlClient.SqlCommand" /> as the <see cref="P:System.Data.SqlClient.SqlDataAdapter.SelectCommand" /> property.</summary>
		/// <param name="selectCommand">A <see cref="T:System.Data.SqlClient.SqlCommand" /> that is a Transact-SQL SELECT statement or stored procedure and is set as the <see cref="P:System.Data.SqlClient.SqlDataAdapter.SelectCommand" /> property of the <see cref="T:System.Data.SqlClient.SqlDataAdapter" />. </param>
		// Token: 0x06001374 RID: 4980 RVA: 0x0005FD82 File Offset: 0x0005DF82
		public SqlDataAdapter(SqlCommand selectCommand)
			: this()
		{
			this.SelectCommand = selectCommand;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlClient.SqlDataAdapter" /> class with a <see cref="P:System.Data.SqlClient.SqlDataAdapter.SelectCommand" /> and a connection string.</summary>
		/// <param name="selectCommandText">A <see cref="T:System.String" /> that is a Transact-SQL SELECT statement or stored procedure to be used by the <see cref="P:System.Data.SqlClient.SqlDataAdapter.SelectCommand" /> property of the <see cref="T:System.Data.SqlClient.SqlDataAdapter" />. </param>
		/// <param name="selectConnectionString">The connection string. If your connection string does not use Integrated Security = true, you can use <see cref="M:System.Data.SqlClient.SqlDataAdapter.#ctor(System.String,System.Data.SqlClient.SqlConnection)" /> and <see cref="T:System.Data.SqlClient.SqlCredential" /> to pass the user ID and password more securely than by specifying the user ID and password as text in the connection string.</param>
		// Token: 0x06001375 RID: 4981 RVA: 0x0005FD94 File Offset: 0x0005DF94
		public SqlDataAdapter(string selectCommandText, string selectConnectionString)
			: this()
		{
			SqlConnection sqlConnection = new SqlConnection(selectConnectionString);
			this.SelectCommand = new SqlCommand(selectCommandText, sqlConnection);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlClient.SqlDataAdapter" /> class with a <see cref="P:System.Data.SqlClient.SqlDataAdapter.SelectCommand" /> and a <see cref="T:System.Data.SqlClient.SqlConnection" /> object.</summary>
		/// <param name="selectCommandText">A <see cref="T:System.String" /> that is a Transact-SQL SELECT statement or stored procedure to be used by the <see cref="P:System.Data.SqlClient.SqlDataAdapter.SelectCommand" /> property of the <see cref="T:System.Data.SqlClient.SqlDataAdapter" />. </param>
		/// <param name="selectConnection">A <see cref="T:System.Data.SqlClient.SqlConnection" /> that represents the connection. If your connection string does not use Integrated Security = true, you can use <see cref="T:System.Data.SqlClient.SqlCredential" /> to pass the user ID and password more securely than by specifying the user ID and password as text in the connection string.</param>
		// Token: 0x06001376 RID: 4982 RVA: 0x0005FDBB File Offset: 0x0005DFBB
		public SqlDataAdapter(string selectCommandText, SqlConnection selectConnection)
			: this()
		{
			this.SelectCommand = new SqlCommand(selectCommandText, selectConnection);
		}

		// Token: 0x06001377 RID: 4983 RVA: 0x0005FDD0 File Offset: 0x0005DFD0
		private SqlDataAdapter(SqlDataAdapter from)
			: base(from)
		{
			GC.SuppressFinalize(this);
		}

		/// <summary>Gets or sets a Transact-SQL statement or stored procedure to delete records from the data set.</summary>
		/// <returns>A <see cref="T:System.Data.SqlClient.SqlCommand" /> used during <see cref="M:System.Data.Common.DbDataAdapter.Update(System.Data.DataSet)" /> to delete records in the database that correspond to deleted rows in the <see cref="T:System.Data.DataSet" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700039F RID: 927
		// (get) Token: 0x06001378 RID: 4984 RVA: 0x0005FDE6 File Offset: 0x0005DFE6
		// (set) Token: 0x06001379 RID: 4985 RVA: 0x0005FDEE File Offset: 0x0005DFEE
		public new SqlCommand DeleteCommand
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

		/// <summary>For a description of this member, see <see cref="P:System.Data.IDbDataAdapter.DeleteCommand" />.</summary>
		/// <returns>An <see cref="T:System.Data.IdbCommandthatis" /> used during <see cref="M:System.Data.Common.DbDataAdapter.Update(System.Data.DataSet)" /> to delete records in the data source for deleted rows in the data set.</returns>
		// Token: 0x170003A0 RID: 928
		// (get) Token: 0x0600137A RID: 4986 RVA: 0x0005FDE6 File Offset: 0x0005DFE6
		// (set) Token: 0x0600137B RID: 4987 RVA: 0x0005FDF7 File Offset: 0x0005DFF7
		IDbCommand IDbDataAdapter.DeleteCommand
		{
			get
			{
				return this._deleteCommand;
			}
			set
			{
				this._deleteCommand = (SqlCommand)value;
			}
		}

		/// <summary>Gets or sets a Transact-SQL statement or stored procedure to insert new records into the data source.</summary>
		/// <returns>A <see cref="T:System.Data.SqlClient.SqlCommand" /> used during <see cref="M:System.Data.Common.DbDataAdapter.Update(System.Data.DataSet)" /> to insert records into the database that correspond to new rows in the <see cref="T:System.Data.DataSet" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170003A1 RID: 929
		// (get) Token: 0x0600137C RID: 4988 RVA: 0x0005FE05 File Offset: 0x0005E005
		// (set) Token: 0x0600137D RID: 4989 RVA: 0x0005FE0D File Offset: 0x0005E00D
		public new SqlCommand InsertCommand
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

		/// <summary>For a description of this member, see <see cref="P:System.Data.IDbDataAdapter.InsertCommand" />.</summary>
		/// <returns>An <see cref="T:System.Data.IDbCommand" /> that is used during <see cref="M:System.Data.Common.DbDataAdapter.Update(System.Data.DataSet)" /> to insert records in the data source for new rows in the data set.</returns>
		// Token: 0x170003A2 RID: 930
		// (get) Token: 0x0600137E RID: 4990 RVA: 0x0005FE05 File Offset: 0x0005E005
		// (set) Token: 0x0600137F RID: 4991 RVA: 0x0005FE16 File Offset: 0x0005E016
		IDbCommand IDbDataAdapter.InsertCommand
		{
			get
			{
				return this._insertCommand;
			}
			set
			{
				this._insertCommand = (SqlCommand)value;
			}
		}

		/// <summary>Gets or sets a Transact-SQL statement or stored procedure used to select records in the data source.</summary>
		/// <returns>A <see cref="T:System.Data.SqlClient.SqlCommand" /> used during <see cref="M:System.Data.Common.DbDataAdapter.Fill(System.Data.DataSet)" /> to select records from the database for placement in the <see cref="T:System.Data.DataSet" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170003A3 RID: 931
		// (get) Token: 0x06001380 RID: 4992 RVA: 0x0005FE24 File Offset: 0x0005E024
		// (set) Token: 0x06001381 RID: 4993 RVA: 0x0005FE2C File Offset: 0x0005E02C
		public new SqlCommand SelectCommand
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

		/// <summary>For a description of this member, see <see cref="P:System.Data.IDbDataAdapter.SelectCommand" />.</summary>
		/// <returns>An <see cref="T:System.Data.IDbCommand" /> that is used during <see cref="M:System.Data.Common.DbDataAdapter.Update(System.Data.DataSet)" /> to select records from data source for placement in the data set.</returns>
		// Token: 0x170003A4 RID: 932
		// (get) Token: 0x06001382 RID: 4994 RVA: 0x0005FE24 File Offset: 0x0005E024
		// (set) Token: 0x06001383 RID: 4995 RVA: 0x0005FE35 File Offset: 0x0005E035
		IDbCommand IDbDataAdapter.SelectCommand
		{
			get
			{
				return this._selectCommand;
			}
			set
			{
				this._selectCommand = (SqlCommand)value;
			}
		}

		/// <summary>Gets or sets a Transact-SQL statement or stored procedure used to update records in the data source.</summary>
		/// <returns>A <see cref="T:System.Data.SqlClient.SqlCommand" /> used during <see cref="M:System.Data.Common.DbDataAdapter.Update(System.Data.DataSet)" /> to update records in the database that correspond to modified rows in the <see cref="T:System.Data.DataSet" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170003A5 RID: 933
		// (get) Token: 0x06001384 RID: 4996 RVA: 0x0005FE43 File Offset: 0x0005E043
		// (set) Token: 0x06001385 RID: 4997 RVA: 0x0005FE4B File Offset: 0x0005E04B
		public new SqlCommand UpdateCommand
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

		/// <summary>For a description of this member, see <see cref="P:System.Data.IDbDataAdapter.UpdateCommand" />.</summary>
		/// <returns>An <see cref="T:System.Data.IdbCommand" /> that is used during <see cref="M:System.Data.Common.DbDataAdapter.Update(System.Data.DataSet)" /> to update records in the data source for modified rows in the data set.</returns>
		// Token: 0x170003A6 RID: 934
		// (get) Token: 0x06001386 RID: 4998 RVA: 0x0005FE43 File Offset: 0x0005E043
		// (set) Token: 0x06001387 RID: 4999 RVA: 0x0005FE54 File Offset: 0x0005E054
		IDbCommand IDbDataAdapter.UpdateCommand
		{
			get
			{
				return this._updateCommand;
			}
			set
			{
				this._updateCommand = (SqlCommand)value;
			}
		}

		/// <summary>Gets or sets the number of rows that are processed in each round-trip to the server.</summary>
		/// <returns>The number of rows to process per-batch. Value isEffect0There is no limit on the batch size..1Disables batch updating.&gt;1Changes are sent using batches of <see cref="P:System.Data.SqlClient.SqlDataAdapter.UpdateBatchSize" /> operations at a time.When setting this to a value other than 1, all the commands associated with the <see cref="T:System.Data.SqlClient.SqlDataAdapter" /> have to have their UpdatedRowSource property set to None or OutputParameters. An exception is thrown otherwise.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170003A7 RID: 935
		// (get) Token: 0x06001388 RID: 5000 RVA: 0x0005FE62 File Offset: 0x0005E062
		// (set) Token: 0x06001389 RID: 5001 RVA: 0x0005FE6A File Offset: 0x0005E06A
		public override int UpdateBatchSize
		{
			get
			{
				return this._updateBatchSize;
			}
			set
			{
				if (0 > value)
				{
					throw ADP.ArgumentOutOfRange("UpdateBatchSize");
				}
				this._updateBatchSize = value;
			}
		}

		// Token: 0x0600138A RID: 5002 RVA: 0x0005FE82 File Offset: 0x0005E082
		protected override int AddToBatch(IDbCommand command)
		{
			int commandCount = this._commandSet.CommandCount;
			this._commandSet.Append((SqlCommand)command);
			return commandCount;
		}

		// Token: 0x0600138B RID: 5003 RVA: 0x0005FEA0 File Offset: 0x0005E0A0
		protected override void ClearBatch()
		{
			this._commandSet.Clear();
		}

		// Token: 0x0600138C RID: 5004 RVA: 0x0005FEAD File Offset: 0x0005E0AD
		protected override int ExecuteBatch()
		{
			return this._commandSet.ExecuteNonQuery();
		}

		// Token: 0x0600138D RID: 5005 RVA: 0x0005FEBA File Offset: 0x0005E0BA
		protected override IDataParameter GetBatchedParameter(int commandIdentifier, int parameterIndex)
		{
			return this._commandSet.GetParameter(commandIdentifier, parameterIndex);
		}

		// Token: 0x0600138E RID: 5006 RVA: 0x0005FEC9 File Offset: 0x0005E0C9
		protected override bool GetBatchedRecordsAffected(int commandIdentifier, out int recordsAffected, out Exception error)
		{
			return this._commandSet.GetBatchedAffected(commandIdentifier, out recordsAffected, out error);
		}

		// Token: 0x0600138F RID: 5007 RVA: 0x0005FEDC File Offset: 0x0005E0DC
		protected override void InitializeBatching()
		{
			this._commandSet = new SqlCommandSet();
			SqlCommand sqlCommand = this.SelectCommand;
			if (sqlCommand == null)
			{
				sqlCommand = this.InsertCommand;
				if (sqlCommand == null)
				{
					sqlCommand = this.UpdateCommand;
					if (sqlCommand == null)
					{
						sqlCommand = this.DeleteCommand;
					}
				}
			}
			if (sqlCommand != null)
			{
				this._commandSet.Connection = sqlCommand.Connection;
				this._commandSet.Transaction = sqlCommand.Transaction;
				this._commandSet.CommandTimeout = sqlCommand.CommandTimeout;
			}
		}

		// Token: 0x06001390 RID: 5008 RVA: 0x0005FF4F File Offset: 0x0005E14F
		protected override void TerminateBatching()
		{
			if (this._commandSet != null)
			{
				this._commandSet.Dispose();
				this._commandSet = null;
			}
		}

		/// <summary>For a description of this member, see <see cref="M:System.ICloneable.Clone" />.</summary>
		/// <returns>A new object that is a copy of the current instance.</returns>
		// Token: 0x06001391 RID: 5009 RVA: 0x0005FF6B File Offset: 0x0005E16B
		object ICloneable.Clone()
		{
			return new SqlDataAdapter(this);
		}

		// Token: 0x06001392 RID: 5010 RVA: 0x0005FF73 File Offset: 0x0005E173
		protected override RowUpdatedEventArgs CreateRowUpdatedEvent(DataRow dataRow, IDbCommand command, StatementType statementType, DataTableMapping tableMapping)
		{
			return new SqlRowUpdatedEventArgs(dataRow, command, statementType, tableMapping);
		}

		// Token: 0x06001393 RID: 5011 RVA: 0x0005FF7F File Offset: 0x0005E17F
		protected override RowUpdatingEventArgs CreateRowUpdatingEvent(DataRow dataRow, IDbCommand command, StatementType statementType, DataTableMapping tableMapping)
		{
			return new SqlRowUpdatingEventArgs(dataRow, command, statementType, tableMapping);
		}

		/// <summary>Occurs during <see cref="M:System.Data.Common.DbDataAdapter.Update(System.Data.DataSet)" /> after a command is executed against the data source. The attempt to update is made, so the event fires.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x14000026 RID: 38
		// (add) Token: 0x06001394 RID: 5012 RVA: 0x0005FF8B File Offset: 0x0005E18B
		// (remove) Token: 0x06001395 RID: 5013 RVA: 0x0005FF9E File Offset: 0x0005E19E
		public event SqlRowUpdatedEventHandler RowUpdated
		{
			add
			{
				base.Events.AddHandler(SqlDataAdapter.EventRowUpdated, value);
			}
			remove
			{
				base.Events.RemoveHandler(SqlDataAdapter.EventRowUpdated, value);
			}
		}

		/// <summary>Occurs during <see cref="M:System.Data.Common.DbDataAdapter.Update(System.Data.DataSet)" /> before a command is executed against the data source. The attempt to update is made, so the event fires.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x14000027 RID: 39
		// (add) Token: 0x06001396 RID: 5014 RVA: 0x0005FFB4 File Offset: 0x0005E1B4
		// (remove) Token: 0x06001397 RID: 5015 RVA: 0x00060018 File Offset: 0x0005E218
		public event SqlRowUpdatingEventHandler RowUpdating
		{
			add
			{
				SqlRowUpdatingEventHandler sqlRowUpdatingEventHandler = (SqlRowUpdatingEventHandler)base.Events[SqlDataAdapter.EventRowUpdating];
				if (sqlRowUpdatingEventHandler != null && value.Target is DbCommandBuilder)
				{
					SqlRowUpdatingEventHandler sqlRowUpdatingEventHandler2 = (SqlRowUpdatingEventHandler)ADP.FindBuilder(sqlRowUpdatingEventHandler);
					if (sqlRowUpdatingEventHandler2 != null)
					{
						base.Events.RemoveHandler(SqlDataAdapter.EventRowUpdating, sqlRowUpdatingEventHandler2);
					}
				}
				base.Events.AddHandler(SqlDataAdapter.EventRowUpdating, value);
			}
			remove
			{
				base.Events.RemoveHandler(SqlDataAdapter.EventRowUpdating, value);
			}
		}

		// Token: 0x06001398 RID: 5016 RVA: 0x0006002C File Offset: 0x0005E22C
		protected override void OnRowUpdated(RowUpdatedEventArgs value)
		{
			SqlRowUpdatedEventHandler sqlRowUpdatedEventHandler = (SqlRowUpdatedEventHandler)base.Events[SqlDataAdapter.EventRowUpdated];
			if (sqlRowUpdatedEventHandler != null && value is SqlRowUpdatedEventArgs)
			{
				sqlRowUpdatedEventHandler(this, (SqlRowUpdatedEventArgs)value);
			}
			base.OnRowUpdated(value);
		}

		// Token: 0x06001399 RID: 5017 RVA: 0x00060070 File Offset: 0x0005E270
		protected override void OnRowUpdating(RowUpdatingEventArgs value)
		{
			SqlRowUpdatingEventHandler sqlRowUpdatingEventHandler = (SqlRowUpdatingEventHandler)base.Events[SqlDataAdapter.EventRowUpdating];
			if (sqlRowUpdatingEventHandler != null && value is SqlRowUpdatingEventArgs)
			{
				sqlRowUpdatingEventHandler(this, (SqlRowUpdatingEventArgs)value);
			}
			base.OnRowUpdating(value);
		}

		// Token: 0x04000D17 RID: 3351
		private static readonly object EventRowUpdated = new object();

		// Token: 0x04000D18 RID: 3352
		private static readonly object EventRowUpdating = new object();

		// Token: 0x04000D19 RID: 3353
		private SqlCommand _deleteCommand;

		// Token: 0x04000D1A RID: 3354
		private SqlCommand _insertCommand;

		// Token: 0x04000D1B RID: 3355
		private SqlCommand _selectCommand;

		// Token: 0x04000D1C RID: 3356
		private SqlCommand _updateCommand;

		// Token: 0x04000D1D RID: 3357
		private SqlCommandSet _commandSet;

		// Token: 0x04000D1E RID: 3358
		private int _updateBatchSize = 1;
	}
}
