using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;

namespace Mono.Data.Sqlite
{
	// Token: 0x0200000B RID: 11
	[Designer("SQLite.Designer.SqliteCommandDesigner, SQLite.Designer, Version=1.0.36.0, Culture=neutral, PublicKeyToken=db937bc2d44ff139")]
	[ToolboxItem(true)]
	public sealed class SqliteCommand : DbCommand, ICloneable
	{
		// Token: 0x060000F6 RID: 246 RVA: 0x000077BC File Offset: 0x000059BC
		public SqliteCommand()
			: this(null, null)
		{
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x000077C6 File Offset: 0x000059C6
		public SqliteCommand(string commandText)
			: this(commandText, null, null)
		{
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x000077D1 File Offset: 0x000059D1
		public SqliteCommand(string commandText, SqliteConnection connection)
			: this(commandText, connection, null)
		{
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x000077DC File Offset: 0x000059DC
		public SqliteCommand(SqliteConnection connection)
			: this(null, connection, null)
		{
		}

		// Token: 0x060000FA RID: 250 RVA: 0x000077E8 File Offset: 0x000059E8
		private SqliteCommand(SqliteCommand source)
			: this(source.CommandText, source.Connection, source.Transaction)
		{
			this.CommandTimeout = source.CommandTimeout;
			this.DesignTimeVisible = source.DesignTimeVisible;
			this.UpdatedRowSource = source.UpdatedRowSource;
			foreach (object obj in source._parameterCollection)
			{
				SqliteParameter sqliteParameter = (SqliteParameter)obj;
				this.Parameters.Add(sqliteParameter.Clone());
			}
		}

		// Token: 0x060000FB RID: 251 RVA: 0x00007888 File Offset: 0x00005A88
		public SqliteCommand(string commandText, SqliteConnection connection, SqliteTransaction transaction)
		{
			this._statementList = null;
			this._activeReader = null;
			this._commandTimeout = 30;
			this._parameterCollection = new SqliteParameterCollection(this);
			this._designTimeVisible = true;
			this._updateRowSource = UpdateRowSource.None;
			this._transaction = null;
			if (commandText != null)
			{
				this.CommandText = commandText;
			}
			if (connection != null)
			{
				this.DbConnection = connection;
				this._commandTimeout = connection.DefaultTimeout;
			}
			if (transaction != null)
			{
				this.Transaction = transaction;
			}
		}

		// Token: 0x060000FC RID: 252 RVA: 0x000078FC File Offset: 0x00005AFC
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
			if (disposing)
			{
				SqliteDataReader sqliteDataReader = null;
				if (this._activeReader != null)
				{
					try
					{
						sqliteDataReader = this._activeReader.Target as SqliteDataReader;
					}
					catch
					{
					}
				}
				if (sqliteDataReader != null)
				{
					sqliteDataReader._disposeCommand = true;
					this._activeReader = null;
					return;
				}
				this.Connection = null;
				this._parameterCollection.Clear();
				this._commandText = null;
			}
		}

		// Token: 0x060000FD RID: 253 RVA: 0x00007970 File Offset: 0x00005B70
		internal void ClearCommands()
		{
			if (this._activeReader != null)
			{
				SqliteDataReader sqliteDataReader = null;
				try
				{
					sqliteDataReader = this._activeReader.Target as SqliteDataReader;
				}
				catch
				{
				}
				if (sqliteDataReader != null)
				{
					sqliteDataReader.Close();
				}
				this._activeReader = null;
			}
			if (this._statementList == null)
			{
				return;
			}
			int count = this._statementList.Count;
			for (int i = 0; i < count; i++)
			{
				this._statementList[i].Dispose();
			}
			this._statementList = null;
			this._parameterCollection.Unbind();
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00007A04 File Offset: 0x00005C04
		internal SqliteStatement BuildNextCommand()
		{
			SqliteStatement sqliteStatement = null;
			SqliteStatement sqliteStatement2;
			try
			{
				if (this._statementList == null)
				{
					this._remainingText = this._commandText;
				}
				sqliteStatement = this._cnn._sql.Prepare(this._cnn, this._remainingText, (this._statementList == null) ? null : this._statementList[this._statementList.Count - 1], (uint)(this._commandTimeout * 1000), out this._remainingText);
				if (sqliteStatement != null)
				{
					sqliteStatement._command = this;
					if (this._statementList == null)
					{
						this._statementList = new List<SqliteStatement>();
					}
					this._statementList.Add(sqliteStatement);
					this._parameterCollection.MapParameters(sqliteStatement);
					sqliteStatement.BindParameters();
				}
				sqliteStatement2 = sqliteStatement;
			}
			catch (Exception)
			{
				if (sqliteStatement != null)
				{
					if (this._statementList.Contains(sqliteStatement))
					{
						this._statementList.Remove(sqliteStatement);
					}
					sqliteStatement.Dispose();
				}
				this._remainingText = null;
				throw;
			}
			return sqliteStatement2;
		}

		// Token: 0x060000FF RID: 255 RVA: 0x00007AF8 File Offset: 0x00005CF8
		internal SqliteStatement GetStatement(int index)
		{
			if (this._statementList == null)
			{
				return this.BuildNextCommand();
			}
			if (index != this._statementList.Count)
			{
				SqliteStatement sqliteStatement = this._statementList[index];
				sqliteStatement.BindParameters();
				return sqliteStatement;
			}
			if (!string.IsNullOrEmpty(this._remainingText))
			{
				return this.BuildNextCommand();
			}
			return null;
		}

		// Token: 0x06000100 RID: 256 RVA: 0x00007B4C File Offset: 0x00005D4C
		public override void Cancel()
		{
			if (this._activeReader != null)
			{
				SqliteDataReader sqliteDataReader = this._activeReader.Target as SqliteDataReader;
				if (sqliteDataReader != null)
				{
					sqliteDataReader.Cancel();
				}
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000101 RID: 257 RVA: 0x00007B7B File Offset: 0x00005D7B
		// (set) Token: 0x06000102 RID: 258 RVA: 0x00007B84 File Offset: 0x00005D84
		[DefaultValue("")]
		[RefreshProperties(RefreshProperties.All)]
		[Editor("Microsoft.VSDesigner.Data.SQL.Design.SqlCommandTextEditor, Microsoft.VSDesigner, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public override string CommandText
		{
			get
			{
				return this._commandText;
			}
			set
			{
				if (this._commandText == value)
				{
					return;
				}
				if (this._activeReader != null && this._activeReader.IsAlive)
				{
					throw new InvalidOperationException("Cannot set CommandText while a DataReader is active");
				}
				this.ClearCommands();
				this._commandText = value;
				SqliteConnection cnn = this._cnn;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000103 RID: 259 RVA: 0x00007BD4 File Offset: 0x00005DD4
		// (set) Token: 0x06000104 RID: 260 RVA: 0x00007BDC File Offset: 0x00005DDC
		[DefaultValue(30)]
		public override int CommandTimeout
		{
			get
			{
				return this._commandTimeout;
			}
			set
			{
				this._commandTimeout = value;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000105 RID: 261 RVA: 0x00007BE5 File Offset: 0x00005DE5
		// (set) Token: 0x06000106 RID: 262 RVA: 0x00007BE8 File Offset: 0x00005DE8
		[RefreshProperties(RefreshProperties.All)]
		[DefaultValue(CommandType.Text)]
		public override CommandType CommandType
		{
			get
			{
				return CommandType.Text;
			}
			set
			{
				if (value != CommandType.Text)
				{
					throw new NotSupportedException();
				}
			}
		}

		// Token: 0x06000107 RID: 263 RVA: 0x00007BF4 File Offset: 0x00005DF4
		protected override DbParameter CreateDbParameter()
		{
			return this.CreateParameter();
		}

		// Token: 0x06000108 RID: 264 RVA: 0x00007BFC File Offset: 0x00005DFC
		public new SqliteParameter CreateParameter()
		{
			return new SqliteParameter();
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000109 RID: 265 RVA: 0x00007C03 File Offset: 0x00005E03
		// (set) Token: 0x0600010A RID: 266 RVA: 0x00007C0C File Offset: 0x00005E0C
		[DefaultValue(null)]
		[Editor("Microsoft.VSDesigner.Data.Design.DbConnectionEditor, Microsoft.VSDesigner, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public new SqliteConnection Connection
		{
			get
			{
				return this._cnn;
			}
			set
			{
				if (this._activeReader != null && this._activeReader.IsAlive)
				{
					throw new InvalidOperationException("Cannot set Connection while a DataReader is active");
				}
				if (this._cnn != null)
				{
					this.ClearCommands();
				}
				this._cnn = value;
				if (this._cnn != null)
				{
					this._version = this._cnn._version;
				}
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600010B RID: 267 RVA: 0x00007C67 File Offset: 0x00005E67
		// (set) Token: 0x0600010C RID: 268 RVA: 0x00007C6F File Offset: 0x00005E6F
		protected override DbConnection DbConnection
		{
			get
			{
				return this.Connection;
			}
			set
			{
				this.Connection = (SqliteConnection)value;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600010D RID: 269 RVA: 0x00007C7D File Offset: 0x00005E7D
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public new SqliteParameterCollection Parameters
		{
			get
			{
				return this._parameterCollection;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600010E RID: 270 RVA: 0x00007C85 File Offset: 0x00005E85
		protected override DbParameterCollection DbParameterCollection
		{
			get
			{
				return this.Parameters;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600010F RID: 271 RVA: 0x00007C8D File Offset: 0x00005E8D
		// (set) Token: 0x06000110 RID: 272 RVA: 0x00007C98 File Offset: 0x00005E98
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new SqliteTransaction Transaction
		{
			get
			{
				return this._transaction;
			}
			set
			{
				if (this._cnn == null)
				{
					this.Connection = value.Connection;
					this._transaction = value;
					return;
				}
				if (this._activeReader != null && this._activeReader.IsAlive)
				{
					throw new InvalidOperationException("Cannot set Transaction while a DataReader is active");
				}
				if (value != null && value._cnn != this._cnn)
				{
					throw new ArgumentException("Transaction is not associated with the command's connection");
				}
				this._transaction = value;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000111 RID: 273 RVA: 0x00007D04 File Offset: 0x00005F04
		// (set) Token: 0x06000112 RID: 274 RVA: 0x00007D0C File Offset: 0x00005F0C
		protected override DbTransaction DbTransaction
		{
			get
			{
				return this.Transaction;
			}
			set
			{
				this.Transaction = (SqliteTransaction)value;
			}
		}

		// Token: 0x06000113 RID: 275 RVA: 0x00007D1C File Offset: 0x00005F1C
		private void InitializeForReader()
		{
			if (this._activeReader != null && this._activeReader.IsAlive)
			{
				throw new InvalidOperationException("DataReader already active on this command");
			}
			if (this._cnn == null)
			{
				throw new InvalidOperationException("No connection associated with this command");
			}
			if (this._cnn.State != ConnectionState.Open)
			{
				throw new InvalidOperationException("Database is not open");
			}
			if (this._cnn._version != this._version)
			{
				this._version = this._cnn._version;
				this.ClearCommands();
			}
			this._parameterCollection.MapParameters(null);
		}

		// Token: 0x06000114 RID: 276 RVA: 0x00007DAB File Offset: 0x00005FAB
		protected override DbDataReader ExecuteDbDataReader(CommandBehavior behavior)
		{
			return this.ExecuteReader(behavior);
		}

		// Token: 0x06000115 RID: 277 RVA: 0x00007DB4 File Offset: 0x00005FB4
		public new SqliteDataReader ExecuteReader(CommandBehavior behavior)
		{
			this.InitializeForReader();
			SqliteDataReader sqliteDataReader = new SqliteDataReader(this, behavior);
			this._activeReader = new WeakReference(sqliteDataReader, false);
			return sqliteDataReader;
		}

		// Token: 0x06000116 RID: 278 RVA: 0x00007DDD File Offset: 0x00005FDD
		public new SqliteDataReader ExecuteReader()
		{
			return this.ExecuteReader(CommandBehavior.Default);
		}

		// Token: 0x06000117 RID: 279 RVA: 0x00007DE6 File Offset: 0x00005FE6
		internal void ClearDataReader()
		{
			this._activeReader = null;
		}

		// Token: 0x06000118 RID: 280 RVA: 0x00007DF0 File Offset: 0x00005FF0
		public override int ExecuteNonQuery()
		{
			int recordsAffected;
			using (SqliteDataReader sqliteDataReader = this.ExecuteReader(CommandBehavior.SingleResult | CommandBehavior.SingleRow))
			{
				while (sqliteDataReader.NextResult())
				{
				}
				recordsAffected = sqliteDataReader.RecordsAffected;
			}
			return recordsAffected;
		}

		// Token: 0x06000119 RID: 281 RVA: 0x00007E34 File Offset: 0x00006034
		public override object ExecuteScalar()
		{
			using (SqliteDataReader sqliteDataReader = this.ExecuteReader(CommandBehavior.SingleResult | CommandBehavior.SingleRow))
			{
				if (sqliteDataReader.Read())
				{
					return sqliteDataReader[0];
				}
			}
			return null;
		}

		// Token: 0x0600011A RID: 282 RVA: 0x00007E7C File Offset: 0x0000607C
		public override void Prepare()
		{
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600011B RID: 283 RVA: 0x00007E7E File Offset: 0x0000607E
		// (set) Token: 0x0600011C RID: 284 RVA: 0x00007E86 File Offset: 0x00006086
		[DefaultValue(UpdateRowSource.None)]
		public override UpdateRowSource UpdatedRowSource
		{
			get
			{
				return this._updateRowSource;
			}
			set
			{
				this._updateRowSource = value;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600011D RID: 285 RVA: 0x00007E8F File Offset: 0x0000608F
		// (set) Token: 0x0600011E RID: 286 RVA: 0x00007E97 File Offset: 0x00006097
		[DesignOnly(true)]
		[Browsable(false)]
		[DefaultValue(true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool DesignTimeVisible
		{
			get
			{
				return this._designTimeVisible;
			}
			set
			{
				this._designTimeVisible = value;
				TypeDescriptor.Refresh(this);
			}
		}

		// Token: 0x0600011F RID: 287 RVA: 0x00007EA6 File Offset: 0x000060A6
		public object Clone()
		{
			return new SqliteCommand(this);
		}

		// Token: 0x04000057 RID: 87
		private string _commandText;

		// Token: 0x04000058 RID: 88
		private SqliteConnection _cnn;

		// Token: 0x04000059 RID: 89
		private long _version;

		// Token: 0x0400005A RID: 90
		private WeakReference _activeReader;

		// Token: 0x0400005B RID: 91
		internal int _commandTimeout;

		// Token: 0x0400005C RID: 92
		private bool _designTimeVisible;

		// Token: 0x0400005D RID: 93
		private UpdateRowSource _updateRowSource;

		// Token: 0x0400005E RID: 94
		private SqliteParameterCollection _parameterCollection;

		// Token: 0x0400005F RID: 95
		internal List<SqliteStatement> _statementList;

		// Token: 0x04000060 RID: 96
		internal string _remainingText;

		// Token: 0x04000061 RID: 97
		private SqliteTransaction _transaction;
	}
}
