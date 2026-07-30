using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Globalization;
using System.IO;
using System.Text;
using System.Transactions;

namespace Mono.Data.Sqlite
{
	// Token: 0x02000003 RID: 3
	public sealed class SqliteConnection : DbConnection, ICloneable
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		protected override DbProviderFactory DbProviderFactory
		{
			get
			{
				return SqliteFactory.Instance;
			}
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000002 RID: 2 RVA: 0x00002058 File Offset: 0x00000258
		// (remove) Token: 0x06000003 RID: 3 RVA: 0x00002090 File Offset: 0x00000290
		private event SQLiteUpdateEventHandler _updateHandler;

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000004 RID: 4 RVA: 0x000020C8 File Offset: 0x000002C8
		// (remove) Token: 0x06000005 RID: 5 RVA: 0x00002100 File Offset: 0x00000300
		private event SQLiteCommitHandler _commitHandler;

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x06000006 RID: 6 RVA: 0x00002138 File Offset: 0x00000338
		// (remove) Token: 0x06000007 RID: 7 RVA: 0x00002170 File Offset: 0x00000370
		private event EventHandler _rollbackHandler;

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x06000008 RID: 8 RVA: 0x000021A8 File Offset: 0x000003A8
		// (remove) Token: 0x06000009 RID: 9 RVA: 0x000021E0 File Offset: 0x000003E0
		public override event StateChangeEventHandler StateChange;

		// Token: 0x0600000A RID: 10 RVA: 0x00002215 File Offset: 0x00000415
		public SqliteConnection()
			: this("")
		{
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002224 File Offset: 0x00000424
		public SqliteConnection(string connectionString)
		{
			this._sql = null;
			this._connectionState = ConnectionState.Closed;
			this._connectionString = "";
			this._transactionLevel = 0;
			this._version = 0L;
			if (connectionString != null)
			{
				this.ConnectionString = connectionString;
			}
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002274 File Offset: 0x00000474
		public SqliteConnection(SqliteConnection connection)
			: this(connection.ConnectionString)
		{
			if (connection.State == ConnectionState.Open)
			{
				this.Open();
				using (DataTable schema = connection.GetSchema("Catalogs"))
				{
					foreach (object obj in schema.Rows)
					{
						DataRow dataRow = (DataRow)obj;
						string text = dataRow[0].ToString();
						if (string.Compare(text, "main", true, CultureInfo.InvariantCulture) != 0 && string.Compare(text, "temp", true, CultureInfo.InvariantCulture) != 0)
						{
							using (SqliteCommand sqliteCommand = this.CreateCommand())
							{
								sqliteCommand.CommandText = string.Format(CultureInfo.InvariantCulture, "ATTACH DATABASE '{0}' AS [{1}]", dataRow[1], dataRow[0]);
								sqliteCommand.ExecuteNonQuery();
							}
						}
					}
				}
			}
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002390 File Offset: 0x00000590
		public object Clone()
		{
			return new SqliteConnection(this);
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002398 File Offset: 0x00000598
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
			if (this._sql != null)
			{
				this._sql.Dispose();
			}
			if (disposing)
			{
				this.Close();
			}
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000023BD File Offset: 0x000005BD
		public static void CreateFile(string databaseFileName)
		{
			File.Create(databaseFileName).Close();
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000023CC File Offset: 0x000005CC
		internal void OnStateChange(ConnectionState newState)
		{
			ConnectionState connectionState = this._connectionState;
			this._connectionState = newState;
			if (this.StateChange != null && connectionState != newState)
			{
				StateChangeEventArgs stateChangeEventArgs = new StateChangeEventArgs(connectionState, newState);
				this.StateChange(this, stateChangeEventArgs);
			}
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002408 File Offset: 0x00000608
		[Obsolete("Use one of the standard BeginTransaction methods, this one will be removed soon")]
		public SqliteTransaction BeginTransaction(global::System.Data.IsolationLevel isolationLevel, bool deferredLock)
		{
			return (SqliteTransaction)this.BeginDbTransaction((!deferredLock) ? global::System.Data.IsolationLevel.Serializable : global::System.Data.IsolationLevel.ReadCommitted);
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002424 File Offset: 0x00000624
		[Obsolete("Use one of the standard BeginTransaction methods, this one will be removed soon")]
		public SqliteTransaction BeginTransaction(bool deferredLock)
		{
			return (SqliteTransaction)this.BeginDbTransaction((!deferredLock) ? global::System.Data.IsolationLevel.Serializable : global::System.Data.IsolationLevel.ReadCommitted);
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002440 File Offset: 0x00000640
		public new SqliteTransaction BeginTransaction(global::System.Data.IsolationLevel isolationLevel)
		{
			return (SqliteTransaction)this.BeginDbTransaction(isolationLevel);
		}

		// Token: 0x06000014 RID: 20 RVA: 0x0000244E File Offset: 0x0000064E
		public new SqliteTransaction BeginTransaction()
		{
			return (SqliteTransaction)this.BeginDbTransaction(this._defaultIsolation);
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002464 File Offset: 0x00000664
		protected override DbTransaction BeginDbTransaction(global::System.Data.IsolationLevel isolationLevel)
		{
			if (this._connectionState != ConnectionState.Open)
			{
				throw new InvalidOperationException();
			}
			if (isolationLevel == global::System.Data.IsolationLevel.Unspecified)
			{
				isolationLevel = this._defaultIsolation;
			}
			if (isolationLevel != global::System.Data.IsolationLevel.Serializable && isolationLevel != global::System.Data.IsolationLevel.ReadCommitted)
			{
				throw new ArgumentException("isolationLevel");
			}
			return new SqliteTransaction(this, isolationLevel != global::System.Data.IsolationLevel.Serializable);
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000024B8 File Offset: 0x000006B8
		public override void ChangeDatabase(string databaseName)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000024C0 File Offset: 0x000006C0
		public override void Close()
		{
			if (this._sql != null)
			{
				if (this._enlistment != null)
				{
					SqliteConnection sqliteConnection = new SqliteConnection();
					sqliteConnection._sql = this._sql;
					sqliteConnection._transactionLevel = this._transactionLevel;
					sqliteConnection._enlistment = this._enlistment;
					sqliteConnection._connectionState = this._connectionState;
					sqliteConnection._version = this._version;
					sqliteConnection._enlistment._transaction._cnn = sqliteConnection;
					sqliteConnection._enlistment._disposeConnection = true;
					this._sql = null;
					this._enlistment = null;
				}
				if (this._sql != null)
				{
					this._sql.Close();
				}
				this._sql = null;
				this._transactionLevel = 0;
			}
			this.OnStateChange(ConnectionState.Closed);
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002575 File Offset: 0x00000775
		public static void ClearPool(SqliteConnection connection)
		{
			if (connection._sql == null)
			{
				return;
			}
			connection._sql.ClearPool();
		}

		// Token: 0x06000019 RID: 25 RVA: 0x0000258B File Offset: 0x0000078B
		public static void ClearAllPools()
		{
			SqliteConnectionPool.ClearAllPools();
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600001A RID: 26 RVA: 0x00002592 File Offset: 0x00000792
		// (set) Token: 0x0600001B RID: 27 RVA: 0x0000259A File Offset: 0x0000079A
		[RefreshProperties(RefreshProperties.All)]
		[DefaultValue("")]
		[Editor("SQLite.Designer.SqliteConnectionStringEditor, SQLite.Designer, Version=1.0.36.0, Culture=neutral, PublicKeyToken=db937bc2d44ff139", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public override string ConnectionString
		{
			get
			{
				return this._connectionString;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException();
				}
				if (this._connectionState != ConnectionState.Closed)
				{
					throw new InvalidOperationException();
				}
				this._connectionString = value;
			}
		}

		// Token: 0x0600001C RID: 28 RVA: 0x000025BA File Offset: 0x000007BA
		public new SqliteCommand CreateCommand()
		{
			return new SqliteCommand(this);
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000025C2 File Offset: 0x000007C2
		protected override DbCommand CreateDbCommand()
		{
			return this.CreateCommand();
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600001E RID: 30 RVA: 0x000025CA File Offset: 0x000007CA
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override string DataSource
		{
			get
			{
				return this._dataSource;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600001F RID: 31 RVA: 0x000025D2 File Offset: 0x000007D2
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override string Database
		{
			get
			{
				return "main";
			}
		}

		// Token: 0x06000020 RID: 32 RVA: 0x000025DC File Offset: 0x000007DC
		internal static void MapMonoKeyword(string[] arPiece, SortedList<string, string> ls)
		{
			string text = arPiece[0].ToLower(CultureInfo.InvariantCulture);
			string text2;
			string text3;
			if (text == "uri")
			{
				text2 = "Data Source";
				text3 = SqliteConnection.MapMonoUriPath(arPiece[1]);
			}
			else
			{
				text2 = arPiece[0];
				text3 = arPiece[1];
			}
			ls.Add(text2, text3);
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002628 File Offset: 0x00000828
		internal static string MapMonoUriPath(string path)
		{
			if (path.StartsWith("file://"))
			{
				return path.Substring(7);
			}
			if (path.StartsWith("file:"))
			{
				return path.Substring(5);
			}
			if (path.StartsWith("/"))
			{
				return path;
			}
			throw new InvalidOperationException("Invalid connection string: invalid URI");
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002678 File Offset: 0x00000878
		internal static string MapUriPath(string path)
		{
			if (path.StartsWith("file://"))
			{
				return path.Substring(7);
			}
			if (path.StartsWith("file:"))
			{
				return path.Substring(5);
			}
			if (path.StartsWith("/"))
			{
				return path;
			}
			throw new InvalidOperationException("Invalid connection string: invalid URI");
		}

		// Token: 0x06000023 RID: 35 RVA: 0x000026C8 File Offset: 0x000008C8
		internal static SortedList<string, string> ParseConnectionString(string connectionString)
		{
			string text = connectionString.Replace(',', ';');
			SortedList<string, string> sortedList = new SortedList<string, string>(StringComparer.OrdinalIgnoreCase);
			string[] array = SqliteConvert.Split(text, ';');
			int num = array.Length;
			for (int i = 0; i < num; i++)
			{
				string[] array2 = SqliteConvert.Split(array[i], '=');
				if (array2.Length != 2)
				{
					throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, "Invalid ConnectionString format for parameter \"{0}\"", (array2.Length != 0) ? array2[0] : "null"));
				}
				SqliteConnection.MapMonoKeyword(array2, sortedList);
			}
			return sortedList;
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002744 File Offset: 0x00000944
		public override void EnlistTransaction(Transaction transaction)
		{
			if (this._transactionLevel > 0 && transaction != null)
			{
				throw new ArgumentException("Unable to enlist in transaction, a local transaction already exists");
			}
			if (this._enlistment != null && transaction != this._enlistment._scope)
			{
				throw new ArgumentException("Already enlisted in a transaction");
			}
			this._enlistment = new SQLiteEnlistment(this, transaction);
		}

		// Token: 0x06000025 RID: 37 RVA: 0x000027A4 File Offset: 0x000009A4
		internal static string FindKey(SortedList<string, string> items, string key, string defValue)
		{
			string text;
			if (items.TryGetValue(key, out text))
			{
				return text;
			}
			return defValue;
		}

		// Token: 0x06000026 RID: 38 RVA: 0x000027C0 File Offset: 0x000009C0
		public override void Open()
		{
			if (this._connectionState != ConnectionState.Closed)
			{
				throw new InvalidOperationException();
			}
			this.Close();
			SortedList<string, string> sortedList = SqliteConnection.ParseConnectionString(this._connectionString);
			if (Convert.ToInt32(SqliteConnection.FindKey(sortedList, "Version", "3"), CultureInfo.InvariantCulture) != 3)
			{
				throw new NotSupportedException("Only SQLite Version 3 is supported at this time");
			}
			string text = SqliteConnection.FindKey(sortedList, "Data Source", "");
			if (string.IsNullOrEmpty(text))
			{
				text = SqliteConnection.FindKey(sortedList, "Uri", "");
				if (string.IsNullOrEmpty(text))
				{
					throw new ArgumentException("Data Source cannot be empty.  Use :memory: to open an in-memory database");
				}
				text = SqliteConnection.MapUriPath(text);
			}
			if (string.Compare(text, ":MEMORY:", true, CultureInfo.InvariantCulture) == 0)
			{
				text = ":memory:";
			}
			else
			{
				text = this.ExpandFileName(text);
			}
			try
			{
				bool flag = SqliteConvert.ToBoolean(SqliteConnection.FindKey(sortedList, "Pooling", bool.FalseString));
				bool flag2 = SqliteConvert.ToBoolean(SqliteConnection.FindKey(sortedList, "UseUTF16Encoding", bool.FalseString));
				int num = Convert.ToInt32(SqliteConnection.FindKey(sortedList, "Max Pool Size", "100"));
				this._defaultTimeout = Convert.ToInt32(SqliteConnection.FindKey(sortedList, "Default Timeout", "30"), CultureInfo.CurrentCulture);
				this._defaultIsolation = (global::System.Data.IsolationLevel)Enum.Parse(typeof(global::System.Data.IsolationLevel), SqliteConnection.FindKey(sortedList, "Default IsolationLevel", "Serializable"), true);
				if (this._defaultIsolation != global::System.Data.IsolationLevel.Serializable && this._defaultIsolation != global::System.Data.IsolationLevel.ReadCommitted)
				{
					throw new NotSupportedException("Invalid Default IsolationLevel specified");
				}
				SQLiteDateFormats sqliteDateFormats = (SQLiteDateFormats)Enum.Parse(typeof(SQLiteDateFormats), SqliteConnection.FindKey(sortedList, "DateTimeFormat", "ISO8601"), true);
				if (flag2)
				{
					this._sql = new SQLite3_UTF16(sqliteDateFormats);
				}
				else
				{
					this._sql = new SQLite3(sqliteDateFormats);
				}
				SQLiteOpenFlagsEnum sqliteOpenFlagsEnum = SQLiteOpenFlagsEnum.None;
				if (SqliteConvert.ToBoolean(SqliteConnection.FindKey(sortedList, "Read Only", bool.FalseString)))
				{
					sqliteOpenFlagsEnum |= SQLiteOpenFlagsEnum.ReadOnly;
				}
				else
				{
					sqliteOpenFlagsEnum |= SQLiteOpenFlagsEnum.ReadWrite;
					if (!SqliteConvert.ToBoolean(SqliteConnection.FindKey(sortedList, "FailIfMissing", bool.FalseString)))
					{
						sqliteOpenFlagsEnum |= SQLiteOpenFlagsEnum.Create;
					}
				}
				if (SqliteConvert.ToBoolean(SqliteConnection.FindKey(sortedList, "FileProtectionComplete", bool.FalseString)))
				{
					sqliteOpenFlagsEnum |= SQLiteOpenFlagsEnum.FileProtectionComplete;
				}
				if (SqliteConvert.ToBoolean(SqliteConnection.FindKey(sortedList, "FileProtectionCompleteUnlessOpen", bool.FalseString)))
				{
					sqliteOpenFlagsEnum |= SQLiteOpenFlagsEnum.FileProtectionCompleteUnlessOpen;
				}
				if (SqliteConvert.ToBoolean(SqliteConnection.FindKey(sortedList, "FileProtectionCompleteUntilFirstUserAuthentication", bool.FalseString)))
				{
					sqliteOpenFlagsEnum |= SQLiteOpenFlagsEnum.FileProtectionCompleteUntilFirstUserAuthentication;
				}
				if (SqliteConvert.ToBoolean(SqliteConnection.FindKey(sortedList, "FileProtectionNone", bool.FalseString)))
				{
					sqliteOpenFlagsEnum |= SQLiteOpenFlagsEnum.FileProtectionNone;
				}
				this._sql.Open(text, sqliteOpenFlagsEnum, num, flag);
				this._binaryGuid = SqliteConvert.ToBoolean(SqliteConnection.FindKey(sortedList, "BinaryGUID", bool.TrueString));
				string text2 = SqliteConnection.FindKey(sortedList, "Password", null);
				if (!string.IsNullOrEmpty(text2))
				{
					this._sql.SetPassword(Encoding.UTF8.GetBytes(text2));
				}
				else if (this._password != null)
				{
					this._sql.SetPassword(this._password);
				}
				this._password = null;
				this._dataSource = Path.GetFileNameWithoutExtension(text);
				this.OnStateChange(ConnectionState.Open);
				this._version += 1L;
				using (SqliteCommand sqliteCommand = this.CreateCommand())
				{
					string text3;
					if (text != ":memory:")
					{
						text3 = SqliteConnection.FindKey(sortedList, "Page Size", "1024");
						if (Convert.ToInt32(text3, CultureInfo.InvariantCulture) != 1024)
						{
							sqliteCommand.CommandText = string.Format(CultureInfo.InvariantCulture, "PRAGMA page_size={0}", text3);
							sqliteCommand.ExecuteNonQuery();
						}
					}
					text3 = SqliteConnection.FindKey(sortedList, "Max Page Count", "0");
					if (Convert.ToInt32(text3, CultureInfo.InvariantCulture) != 0)
					{
						sqliteCommand.CommandText = string.Format(CultureInfo.InvariantCulture, "PRAGMA max_page_count={0}", text3);
						sqliteCommand.ExecuteNonQuery();
					}
					text3 = SqliteConnection.FindKey(sortedList, "Legacy Format", bool.FalseString);
					sqliteCommand.CommandText = string.Format(CultureInfo.InvariantCulture, "PRAGMA legacy_file_format={0}", SqliteConvert.ToBoolean(text3) ? "ON" : "OFF");
					sqliteCommand.ExecuteNonQuery();
					text3 = SqliteConnection.FindKey(sortedList, "Synchronous", "Normal");
					if (string.Compare(text3, "Full", StringComparison.OrdinalIgnoreCase) != 0)
					{
						sqliteCommand.CommandText = string.Format(CultureInfo.InvariantCulture, "PRAGMA synchronous={0}", text3);
						sqliteCommand.ExecuteNonQuery();
					}
					text3 = SqliteConnection.FindKey(sortedList, "Cache Size", "2000");
					if (Convert.ToInt32(text3, CultureInfo.InvariantCulture) != 2000)
					{
						sqliteCommand.CommandText = string.Format(CultureInfo.InvariantCulture, "PRAGMA cache_size={0}", text3);
						sqliteCommand.ExecuteNonQuery();
					}
					text3 = SqliteConnection.FindKey(sortedList, "Journal Mode", "Delete");
					if (string.Compare(text3, "Default", StringComparison.OrdinalIgnoreCase) != 0)
					{
						sqliteCommand.CommandText = string.Format(CultureInfo.InvariantCulture, "PRAGMA journal_mode={0}", text3);
						sqliteCommand.ExecuteNonQuery();
					}
				}
				if (this._commitHandler != null)
				{
					this._sql.SetCommitHook(this._commitCallback);
				}
				if (this._updateHandler != null)
				{
					this._sql.SetUpdateHook(this._updateCallback);
				}
				if (this._rollbackHandler != null)
				{
					this._sql.SetRollbackHook(this._rollbackCallback);
				}
				if (Transaction.Current != null && SqliteConvert.ToBoolean(SqliteConnection.FindKey(sortedList, "Enlist", bool.TrueString)))
				{
					this.EnlistTransaction(Transaction.Current);
				}
			}
			catch (SqliteException)
			{
				this.Close();
				throw;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000027 RID: 39 RVA: 0x00002D48 File Offset: 0x00000F48
		// (set) Token: 0x06000028 RID: 40 RVA: 0x00002D50 File Offset: 0x00000F50
		public int DefaultTimeout
		{
			get
			{
				return this._defaultTimeout;
			}
			set
			{
				this._defaultTimeout = value;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000029 RID: 41 RVA: 0x00002D59 File Offset: 0x00000F59
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override string ServerVersion
		{
			get
			{
				if (this._connectionState != ConnectionState.Open)
				{
					throw new InvalidOperationException();
				}
				return this._sql.Version;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600002A RID: 42 RVA: 0x00002D75 File Offset: 0x00000F75
		public static string SQLiteVersion
		{
			get
			{
				return SQLite3.SQLiteVersion;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600002B RID: 43 RVA: 0x00002D7C File Offset: 0x00000F7C
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override ConnectionState State
		{
			get
			{
				return this._connectionState;
			}
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002D84 File Offset: 0x00000F84
		public void ChangePassword(string newPassword)
		{
			this.ChangePassword(string.IsNullOrEmpty(newPassword) ? null : Encoding.UTF8.GetBytes(newPassword));
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002DA2 File Offset: 0x00000FA2
		public void ChangePassword(byte[] newPassword)
		{
			if (this._connectionState != ConnectionState.Open)
			{
				throw new InvalidOperationException("Database must be opened before changing the password.");
			}
			this._sql.ChangePassword(newPassword);
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002DC4 File Offset: 0x00000FC4
		public void SetPassword(string databasePassword)
		{
			this.SetPassword(string.IsNullOrEmpty(databasePassword) ? null : Encoding.UTF8.GetBytes(databasePassword));
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002DE2 File Offset: 0x00000FE2
		public void SetPassword(byte[] databasePassword)
		{
			if (this._connectionState != ConnectionState.Closed)
			{
				throw new InvalidOperationException("Password can only be set before the database is opened.");
			}
			if (databasePassword != null && databasePassword.Length == 0)
			{
				databasePassword = null;
			}
			this._password = databasePassword;
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002E08 File Offset: 0x00001008
		private string ExpandFileName(string sourceFile)
		{
			if (string.IsNullOrEmpty(sourceFile))
			{
				return sourceFile;
			}
			if (sourceFile.StartsWith("|DataDirectory|", StringComparison.OrdinalIgnoreCase))
			{
				string text = AppDomain.CurrentDomain.GetData("DataDirectory") as string;
				if (string.IsNullOrEmpty(text))
				{
					text = AppDomain.CurrentDomain.BaseDirectory;
				}
				if (sourceFile.Length > "|DataDirectory|".Length && (sourceFile["|DataDirectory|".Length] == Path.DirectorySeparatorChar || sourceFile["|DataDirectory|".Length] == Path.AltDirectorySeparatorChar))
				{
					sourceFile = sourceFile.Remove("|DataDirectory|".Length, 1);
				}
				sourceFile = Path.Combine(text, sourceFile.Substring("|DataDirectory|".Length));
			}
			sourceFile = Path.GetFullPath(sourceFile);
			return sourceFile;
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002ECC File Offset: 0x000010CC
		public override DataTable GetSchema()
		{
			return this.GetSchema("MetaDataCollections", null);
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002EDA File Offset: 0x000010DA
		public override DataTable GetSchema(string collectionName)
		{
			return this.GetSchema(collectionName, new string[0]);
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002EEC File Offset: 0x000010EC
		public override DataTable GetSchema(string collectionName, string[] restrictionValues)
		{
			if (this._connectionState != ConnectionState.Open)
			{
				throw new InvalidOperationException();
			}
			string[] array = new string[5];
			if (restrictionValues == null)
			{
				restrictionValues = new string[0];
			}
			restrictionValues.CopyTo(array, 0);
			string text = collectionName.ToUpper(CultureInfo.InvariantCulture);
			uint num = <PrivateImplementationDetails>.ComputeStringHash(text);
			if (num <= 1969105565U)
			{
				if (num <= 775012827U)
				{
					if (num != 441939398U)
					{
						if (num != 746237978U)
						{
							if (num != 775012827U)
							{
								goto IL_02DD;
							}
							if (!(text == "INDEXES"))
							{
								goto IL_02DD;
							}
							return this.Schema_Indexes(array[0], array[2], array[3]);
						}
						else if (!(text == "TABLECOLUMNS"))
						{
							goto IL_02DD;
						}
					}
					else
					{
						if (!(text == "INDEXCOLUMNS"))
						{
							goto IL_02DD;
						}
						return this.Schema_IndexColumns(array[0], array[2], array[3], array[4]);
					}
				}
				else if (num <= 855730417U)
				{
					if (num != 853125532U)
					{
						if (num != 855730417U)
						{
							goto IL_02DD;
						}
						if (!(text == "VIEWS"))
						{
							goto IL_02DD;
						}
						return this.Schema_Views(array[0], array[2]);
					}
					else if (!(text == "COLUMNS"))
					{
						goto IL_02DD;
					}
				}
				else if (num != 1725202083U)
				{
					if (num != 1969105565U)
					{
						goto IL_02DD;
					}
					if (!(text == "METADATACOLLECTIONS"))
					{
						goto IL_02DD;
					}
					return SqliteConnection.Schema_MetaDataCollections();
				}
				else
				{
					if (!(text == "VIEWCOLUMNS"))
					{
						goto IL_02DD;
					}
					return this.Schema_ViewColumns(array[0], array[2], array[3]);
				}
				return this.Schema_Columns(array[0], array[2], array[3]);
			}
			if (num <= 3042169616U)
			{
				if (num != 1982860548U)
				{
					if (num != 3019482018U)
					{
						if (num == 3042169616U)
						{
							if (text == "TRIGGERS")
							{
								return this.Schema_Triggers(array[0], array[2], array[3]);
							}
						}
					}
					else if (text == "RESERVEDWORDS")
					{
						return SqliteConnection.Schema_ReservedWords();
					}
				}
				else if (text == "TABLES")
				{
					return this.Schema_Tables(array[0], array[2], array[3]);
				}
			}
			else if (num <= 3314625073U)
			{
				if (num != 3109683963U)
				{
					if (num == 3314625073U)
					{
						if (text == "CATALOGS")
						{
							return this.Schema_Catalogs(array[0]);
						}
					}
				}
				else if (text == "FOREIGNKEYS")
				{
					return this.Schema_ForeignKeys(array[0], array[2], array[3]);
				}
			}
			else if (num != 3858812934U)
			{
				if (num == 3986803546U)
				{
					if (text == "DATATYPES")
					{
						return this.Schema_DataTypes();
					}
				}
			}
			else if (text == "DATASOURCEINFORMATION")
			{
				return this.Schema_DataSourceInformation();
			}
			IL_02DD:
			throw new NotSupportedException();
		}

		// Token: 0x06000034 RID: 52 RVA: 0x000031DC File Offset: 0x000013DC
		private static DataTable Schema_ReservedWords()
		{
			DataTable dataTable = new DataTable("MetaDataCollections");
			dataTable.Locale = CultureInfo.InvariantCulture;
			dataTable.Columns.Add("ReservedWord", typeof(string));
			dataTable.Columns.Add("MaximumVersion", typeof(string));
			dataTable.Columns.Add("MinimumVersion", typeof(string));
			dataTable.BeginLoadData();
			foreach (string text in SR.Keywords.Split(new char[] { ',' }))
			{
				DataRow dataRow = dataTable.NewRow();
				dataRow[0] = text;
				dataTable.Rows.Add(dataRow);
			}
			dataTable.AcceptChanges();
			dataTable.EndLoadData();
			return dataTable;
		}

		// Token: 0x06000035 RID: 53 RVA: 0x000032A8 File Offset: 0x000014A8
		private static DataTable Schema_MetaDataCollections()
		{
			DataTable dataTable = new DataTable("MetaDataCollections");
			dataTable.Locale = CultureInfo.InvariantCulture;
			dataTable.Columns.Add("CollectionName", typeof(string));
			dataTable.Columns.Add("NumberOfRestrictions", typeof(int));
			dataTable.Columns.Add("NumberOfIdentifierParts", typeof(int));
			dataTable.BeginLoadData();
			StringReader stringReader = new StringReader(SR.MetaDataCollections);
			dataTable.ReadXml(stringReader);
			stringReader.Close();
			dataTable.AcceptChanges();
			dataTable.EndLoadData();
			return dataTable;
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00003348 File Offset: 0x00001548
		private DataTable Schema_DataSourceInformation()
		{
			DataTable dataTable = new DataTable("DataSourceInformation");
			dataTable.Locale = CultureInfo.InvariantCulture;
			dataTable.Columns.Add(DbMetaDataColumnNames.CompositeIdentifierSeparatorPattern, typeof(string));
			dataTable.Columns.Add(DbMetaDataColumnNames.DataSourceProductName, typeof(string));
			dataTable.Columns.Add(DbMetaDataColumnNames.DataSourceProductVersion, typeof(string));
			dataTable.Columns.Add(DbMetaDataColumnNames.DataSourceProductVersionNormalized, typeof(string));
			dataTable.Columns.Add(DbMetaDataColumnNames.GroupByBehavior, typeof(int));
			dataTable.Columns.Add(DbMetaDataColumnNames.IdentifierPattern, typeof(string));
			dataTable.Columns.Add(DbMetaDataColumnNames.IdentifierCase, typeof(int));
			dataTable.Columns.Add(DbMetaDataColumnNames.OrderByColumnsInSelect, typeof(bool));
			dataTable.Columns.Add(DbMetaDataColumnNames.ParameterMarkerFormat, typeof(string));
			dataTable.Columns.Add(DbMetaDataColumnNames.ParameterMarkerPattern, typeof(string));
			dataTable.Columns.Add(DbMetaDataColumnNames.ParameterNameMaxLength, typeof(int));
			dataTable.Columns.Add(DbMetaDataColumnNames.ParameterNamePattern, typeof(string));
			dataTable.Columns.Add(DbMetaDataColumnNames.QuotedIdentifierPattern, typeof(string));
			dataTable.Columns.Add(DbMetaDataColumnNames.QuotedIdentifierCase, typeof(int));
			dataTable.Columns.Add(DbMetaDataColumnNames.StatementSeparatorPattern, typeof(string));
			dataTable.Columns.Add(DbMetaDataColumnNames.StringLiteralPattern, typeof(string));
			dataTable.Columns.Add(DbMetaDataColumnNames.SupportedJoinOperators, typeof(int));
			dataTable.BeginLoadData();
			DataRow dataRow = dataTable.NewRow();
			dataRow.ItemArray = new object[]
			{
				null,
				"SQLite",
				this._sql.Version,
				this._sql.Version,
				3,
				"(^\\[\\p{Lo}\\p{Lu}\\p{Ll}_@#][\\p{Lo}\\p{Lu}\\p{Ll}\\p{Nd}@$#_]*$)|(^\\[[^\\]\\0]|\\]\\]+\\]$)|(^\\\"[^\\\"\\0]|\\\"\\\"+\\\"$)",
				1,
				false,
				"{0}",
				"@[\\p{Lo}\\p{Lu}\\p{Ll}\\p{Lm}_@#][\\p{Lo}\\p{Lu}\\p{Ll}\\p{Lm}\\p{Nd}\\uff3f_@#\\$]*(?=\\s+|$)",
				255,
				"^[\\p{Lo}\\p{Lu}\\p{Ll}\\p{Lm}_@#][\\p{Lo}\\p{Lu}\\p{Ll}\\p{Lm}\\p{Nd}\\uff3f_@#\\$]*(?=\\s+|$)",
				"(([^\\[]|\\]\\])*)",
				1,
				";",
				"'(([^']|'')*)'",
				15
			};
			dataTable.Rows.Add(dataRow);
			dataTable.AcceptChanges();
			dataTable.EndLoadData();
			return dataTable;
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00003608 File Offset: 0x00001808
		private DataTable Schema_Columns(string strCatalog, string strTable, string strColumn)
		{
			DataTable dataTable = new DataTable("Columns");
			dataTable.Locale = CultureInfo.InvariantCulture;
			dataTable.Columns.Add("TABLE_CATALOG", typeof(string));
			dataTable.Columns.Add("TABLE_SCHEMA", typeof(string));
			dataTable.Columns.Add("TABLE_NAME", typeof(string));
			dataTable.Columns.Add("COLUMN_NAME", typeof(string));
			dataTable.Columns.Add("COLUMN_GUID", typeof(Guid));
			dataTable.Columns.Add("COLUMN_PROPID", typeof(long));
			dataTable.Columns.Add("ORDINAL_POSITION", typeof(int));
			dataTable.Columns.Add("COLUMN_HASDEFAULT", typeof(bool));
			dataTable.Columns.Add("COLUMN_DEFAULT", typeof(string));
			dataTable.Columns.Add("COLUMN_FLAGS", typeof(long));
			dataTable.Columns.Add("IS_NULLABLE", typeof(bool));
			dataTable.Columns.Add("DATA_TYPE", typeof(string));
			dataTable.Columns.Add("TYPE_GUID", typeof(Guid));
			dataTable.Columns.Add("CHARACTER_MAXIMUM_LENGTH", typeof(int));
			dataTable.Columns.Add("CHARACTER_OCTET_LENGTH", typeof(int));
			dataTable.Columns.Add("NUMERIC_PRECISION", typeof(int));
			dataTable.Columns.Add("NUMERIC_SCALE", typeof(int));
			dataTable.Columns.Add("DATETIME_PRECISION", typeof(long));
			dataTable.Columns.Add("CHARACTER_SET_CATALOG", typeof(string));
			dataTable.Columns.Add("CHARACTER_SET_SCHEMA", typeof(string));
			dataTable.Columns.Add("CHARACTER_SET_NAME", typeof(string));
			dataTable.Columns.Add("COLLATION_CATALOG", typeof(string));
			dataTable.Columns.Add("COLLATION_SCHEMA", typeof(string));
			dataTable.Columns.Add("COLLATION_NAME", typeof(string));
			dataTable.Columns.Add("DOMAIN_CATALOG", typeof(string));
			dataTable.Columns.Add("DOMAIN_NAME", typeof(string));
			dataTable.Columns.Add("DESCRIPTION", typeof(string));
			dataTable.Columns.Add("PRIMARY_KEY", typeof(bool));
			dataTable.Columns.Add("EDM_TYPE", typeof(string));
			dataTable.Columns.Add("AUTOINCREMENT", typeof(bool));
			dataTable.Columns.Add("UNIQUE", typeof(bool));
			dataTable.BeginLoadData();
			if (string.IsNullOrEmpty(strCatalog))
			{
				strCatalog = "main";
			}
			string text = ((string.Compare(strCatalog, "temp", true, CultureInfo.InvariantCulture) == 0) ? "sqlite_temp_master" : "sqlite_master");
			using (SqliteCommand sqliteCommand = new SqliteCommand(string.Format(CultureInfo.InvariantCulture, "SELECT * FROM [{0}].[{1}] WHERE [type] LIKE 'table' OR [type] LIKE 'view'", strCatalog, text), this))
			{
				using (SqliteDataReader sqliteDataReader = sqliteCommand.ExecuteReader())
				{
					while (sqliteDataReader.Read())
					{
						if (string.IsNullOrEmpty(strTable) || string.Compare(strTable, sqliteDataReader.GetString(2), true, CultureInfo.InvariantCulture) == 0)
						{
							try
							{
								using (SqliteCommand sqliteCommand2 = new SqliteCommand(string.Format(CultureInfo.InvariantCulture, "SELECT * FROM [{0}].[{1}]", strCatalog, sqliteDataReader.GetString(2)), this))
								{
									using (SqliteDataReader sqliteDataReader2 = sqliteCommand2.ExecuteReader(CommandBehavior.SchemaOnly))
									{
										using (DataTable schemaTable = sqliteDataReader2.GetSchemaTable(true, true))
										{
											foreach (object obj in schemaTable.Rows)
											{
												DataRow dataRow = (DataRow)obj;
												if (string.Compare(dataRow[SchemaTableColumn.ColumnName].ToString(), strColumn, true, CultureInfo.InvariantCulture) == 0 || strColumn == null)
												{
													DataRow dataRow2 = dataTable.NewRow();
													dataRow2["NUMERIC_PRECISION"] = dataRow[SchemaTableColumn.NumericPrecision];
													dataRow2["NUMERIC_SCALE"] = dataRow[SchemaTableColumn.NumericScale];
													dataRow2["TABLE_NAME"] = sqliteDataReader.GetString(2);
													dataRow2["COLUMN_NAME"] = dataRow[SchemaTableColumn.ColumnName];
													dataRow2["TABLE_CATALOG"] = strCatalog;
													dataRow2["ORDINAL_POSITION"] = dataRow[SchemaTableColumn.ColumnOrdinal];
													dataRow2["COLUMN_HASDEFAULT"] = dataRow[SchemaTableOptionalColumn.DefaultValue] != DBNull.Value;
													dataRow2["COLUMN_DEFAULT"] = dataRow[SchemaTableOptionalColumn.DefaultValue];
													dataRow2["IS_NULLABLE"] = dataRow[SchemaTableColumn.AllowDBNull];
													dataRow2["DATA_TYPE"] = dataRow["DataTypeName"].ToString().ToLower(CultureInfo.InvariantCulture);
													dataRow2["EDM_TYPE"] = SqliteConvert.DbTypeToTypeName((DbType)dataRow[SchemaTableColumn.ProviderType]).ToString().ToLower(CultureInfo.InvariantCulture);
													dataRow2["CHARACTER_MAXIMUM_LENGTH"] = dataRow[SchemaTableColumn.ColumnSize];
													dataRow2["TABLE_SCHEMA"] = dataRow[SchemaTableColumn.BaseSchemaName];
													dataRow2["PRIMARY_KEY"] = dataRow[SchemaTableColumn.IsKey];
													dataRow2["AUTOINCREMENT"] = dataRow[SchemaTableOptionalColumn.IsAutoIncrement];
													dataRow2["COLLATION_NAME"] = dataRow["CollationType"];
													dataRow2["UNIQUE"] = dataRow[SchemaTableColumn.IsUnique];
													dataTable.Rows.Add(dataRow2);
												}
											}
										}
									}
								}
							}
							catch (SqliteException)
							{
							}
						}
					}
				}
			}
			dataTable.AcceptChanges();
			dataTable.EndLoadData();
			return dataTable;
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00003D58 File Offset: 0x00001F58
		private DataTable Schema_Indexes(string strCatalog, string strTable, string strIndex)
		{
			DataTable dataTable = new DataTable("Indexes");
			List<int> list = new List<int>();
			dataTable.Locale = CultureInfo.InvariantCulture;
			dataTable.Columns.Add("TABLE_CATALOG", typeof(string));
			dataTable.Columns.Add("TABLE_SCHEMA", typeof(string));
			dataTable.Columns.Add("TABLE_NAME", typeof(string));
			dataTable.Columns.Add("INDEX_CATALOG", typeof(string));
			dataTable.Columns.Add("INDEX_SCHEMA", typeof(string));
			dataTable.Columns.Add("INDEX_NAME", typeof(string));
			dataTable.Columns.Add("PRIMARY_KEY", typeof(bool));
			dataTable.Columns.Add("UNIQUE", typeof(bool));
			dataTable.Columns.Add("CLUSTERED", typeof(bool));
			dataTable.Columns.Add("TYPE", typeof(int));
			dataTable.Columns.Add("FILL_FACTOR", typeof(int));
			dataTable.Columns.Add("INITIAL_SIZE", typeof(int));
			dataTable.Columns.Add("NULLS", typeof(int));
			dataTable.Columns.Add("SORT_BOOKMARKS", typeof(bool));
			dataTable.Columns.Add("AUTO_UPDATE", typeof(bool));
			dataTable.Columns.Add("NULL_COLLATION", typeof(int));
			dataTable.Columns.Add("ORDINAL_POSITION", typeof(int));
			dataTable.Columns.Add("COLUMN_NAME", typeof(string));
			dataTable.Columns.Add("COLUMN_GUID", typeof(Guid));
			dataTable.Columns.Add("COLUMN_PROPID", typeof(long));
			dataTable.Columns.Add("COLLATION", typeof(short));
			dataTable.Columns.Add("CARDINALITY", typeof(decimal));
			dataTable.Columns.Add("PAGES", typeof(int));
			dataTable.Columns.Add("FILTER_CONDITION", typeof(string));
			dataTable.Columns.Add("INTEGRATED", typeof(bool));
			dataTable.Columns.Add("INDEX_DEFINITION", typeof(string));
			dataTable.BeginLoadData();
			if (string.IsNullOrEmpty(strCatalog))
			{
				strCatalog = "main";
			}
			string text = ((string.Compare(strCatalog, "temp", true, CultureInfo.InvariantCulture) == 0) ? "sqlite_temp_master" : "sqlite_master");
			using (SqliteCommand sqliteCommand = new SqliteCommand(string.Format(CultureInfo.InvariantCulture, "SELECT * FROM [{0}].[{1}] WHERE [type] LIKE 'table'", strCatalog, text), this))
			{
				using (SqliteDataReader sqliteDataReader = sqliteCommand.ExecuteReader())
				{
					while (sqliteDataReader.Read())
					{
						bool flag = false;
						list.Clear();
						if (string.IsNullOrEmpty(strTable) || string.Compare(sqliteDataReader.GetString(2), strTable, true, CultureInfo.InvariantCulture) == 0)
						{
							try
							{
								using (SqliteCommand sqliteCommand2 = new SqliteCommand(string.Format(CultureInfo.InvariantCulture, "PRAGMA [{0}].table_info([{1}])", strCatalog, sqliteDataReader.GetString(2)), this))
								{
									using (SqliteDataReader sqliteDataReader2 = sqliteCommand2.ExecuteReader())
									{
										while (sqliteDataReader2.Read())
										{
											if (sqliteDataReader2.GetInt32(5) == 1)
											{
												list.Add(sqliteDataReader2.GetInt32(0));
												if (string.Compare(sqliteDataReader2.GetString(2), "INTEGER", true, CultureInfo.InvariantCulture) == 0)
												{
													flag = true;
												}
											}
										}
									}
								}
							}
							catch (SqliteException)
							{
							}
							if (list.Count == 1 && flag)
							{
								DataRow dataRow = dataTable.NewRow();
								dataRow["TABLE_CATALOG"] = strCatalog;
								dataRow["TABLE_NAME"] = sqliteDataReader.GetString(2);
								dataRow["INDEX_CATALOG"] = strCatalog;
								dataRow["PRIMARY_KEY"] = true;
								dataRow["INDEX_NAME"] = string.Format(CultureInfo.InvariantCulture, "{1}_PK_{0}", sqliteDataReader.GetString(2), text);
								dataRow["UNIQUE"] = true;
								if (string.Compare((string)dataRow["INDEX_NAME"], strIndex, true, CultureInfo.InvariantCulture) == 0 || strIndex == null)
								{
									dataTable.Rows.Add(dataRow);
								}
								list.Clear();
							}
							try
							{
								using (SqliteCommand sqliteCommand3 = new SqliteCommand(string.Format(CultureInfo.InvariantCulture, "PRAGMA [{0}].index_list([{1}])", strCatalog, sqliteDataReader.GetString(2)), this))
								{
									using (SqliteDataReader sqliteDataReader3 = sqliteCommand3.ExecuteReader())
									{
										while (sqliteDataReader3.Read())
										{
											if (string.Compare(sqliteDataReader3.GetString(1), strIndex, true, CultureInfo.InvariantCulture) == 0 || strIndex == null)
											{
												DataRow dataRow = dataTable.NewRow();
												dataRow["TABLE_CATALOG"] = strCatalog;
												dataRow["TABLE_NAME"] = sqliteDataReader.GetString(2);
												dataRow["INDEX_CATALOG"] = strCatalog;
												dataRow["INDEX_NAME"] = sqliteDataReader3.GetString(1);
												dataRow["UNIQUE"] = sqliteDataReader3.GetBoolean(2);
												dataRow["PRIMARY_KEY"] = false;
												using (SqliteCommand sqliteCommand4 = new SqliteCommand(string.Format(CultureInfo.InvariantCulture, "SELECT * FROM [{0}].[{2}] WHERE [type] LIKE 'index' AND [name] LIKE '{1}'", strCatalog, sqliteDataReader3.GetString(1).Replace("'", "''"), text), this))
												{
													using (SqliteDataReader sqliteDataReader4 = sqliteCommand4.ExecuteReader())
													{
														while (sqliteDataReader4.Read())
														{
															if (!sqliteDataReader4.IsDBNull(4))
															{
																dataRow["INDEX_DEFINITION"] = sqliteDataReader4.GetString(4);
																break;
															}
														}
													}
												}
												if (list.Count > 0 && sqliteDataReader3.GetString(1).StartsWith("sqlite_autoindex_" + sqliteDataReader.GetString(2), StringComparison.InvariantCultureIgnoreCase))
												{
													using (SqliteCommand sqliteCommand5 = new SqliteCommand(string.Format(CultureInfo.InvariantCulture, "PRAGMA [{0}].index_info([{1}])", strCatalog, sqliteDataReader3.GetString(1)), this))
													{
														using (SqliteDataReader sqliteDataReader5 = sqliteCommand5.ExecuteReader())
														{
															int num = 0;
															while (sqliteDataReader5.Read())
															{
																if (!list.Contains(sqliteDataReader5.GetInt32(1)))
																{
																	num = 0;
																	break;
																}
																num++;
															}
															if (num == list.Count)
															{
																dataRow["PRIMARY_KEY"] = true;
																list.Clear();
															}
														}
													}
												}
												dataTable.Rows.Add(dataRow);
											}
										}
									}
								}
							}
							catch (SqliteException)
							{
							}
						}
					}
				}
			}
			dataTable.AcceptChanges();
			dataTable.EndLoadData();
			return dataTable;
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00004598 File Offset: 0x00002798
		private DataTable Schema_Triggers(string catalog, string table, string triggerName)
		{
			DataTable dataTable = new DataTable("Triggers");
			dataTable.Locale = CultureInfo.InvariantCulture;
			dataTable.Columns.Add("TABLE_CATALOG", typeof(string));
			dataTable.Columns.Add("TABLE_SCHEMA", typeof(string));
			dataTable.Columns.Add("TABLE_NAME", typeof(string));
			dataTable.Columns.Add("TRIGGER_NAME", typeof(string));
			dataTable.Columns.Add("TRIGGER_DEFINITION", typeof(string));
			dataTable.BeginLoadData();
			if (string.IsNullOrEmpty(table))
			{
				table = null;
			}
			if (string.IsNullOrEmpty(catalog))
			{
				catalog = "main";
			}
			string text = ((string.Compare(catalog, "temp", true, CultureInfo.InvariantCulture) == 0) ? "sqlite_temp_master" : "sqlite_master");
			using (SqliteCommand sqliteCommand = new SqliteCommand(string.Format(CultureInfo.InvariantCulture, "SELECT [type], [name], [tbl_name], [rootpage], [sql], [rowid] FROM [{0}].[{1}] WHERE [type] LIKE 'trigger'", catalog, text), this))
			{
				using (SqliteDataReader sqliteDataReader = sqliteCommand.ExecuteReader())
				{
					while (sqliteDataReader.Read())
					{
						if ((string.Compare(sqliteDataReader.GetString(1), triggerName, true, CultureInfo.InvariantCulture) == 0 || triggerName == null) && (table == null || string.Compare(table, sqliteDataReader.GetString(2), true, CultureInfo.InvariantCulture) == 0))
						{
							DataRow dataRow = dataTable.NewRow();
							dataRow["TABLE_CATALOG"] = catalog;
							dataRow["TABLE_NAME"] = sqliteDataReader.GetString(2);
							dataRow["TRIGGER_NAME"] = sqliteDataReader.GetString(1);
							dataRow["TRIGGER_DEFINITION"] = sqliteDataReader.GetString(4);
							dataTable.Rows.Add(dataRow);
						}
					}
				}
			}
			dataTable.AcceptChanges();
			dataTable.EndLoadData();
			return dataTable;
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00004780 File Offset: 0x00002980
		private DataTable Schema_Tables(string strCatalog, string strTable, string strType)
		{
			DataTable dataTable = new DataTable("Tables");
			dataTable.Locale = CultureInfo.InvariantCulture;
			dataTable.Columns.Add("TABLE_CATALOG", typeof(string));
			dataTable.Columns.Add("TABLE_SCHEMA", typeof(string));
			dataTable.Columns.Add("TABLE_NAME", typeof(string));
			dataTable.Columns.Add("TABLE_TYPE", typeof(string));
			dataTable.Columns.Add("TABLE_ID", typeof(long));
			dataTable.Columns.Add("TABLE_ROOTPAGE", typeof(int));
			dataTable.Columns.Add("TABLE_DEFINITION", typeof(string));
			dataTable.BeginLoadData();
			if (string.IsNullOrEmpty(strCatalog))
			{
				strCatalog = "main";
			}
			string text = ((string.Compare(strCatalog, "temp", true, CultureInfo.InvariantCulture) == 0) ? "sqlite_temp_master" : "sqlite_master");
			using (SqliteCommand sqliteCommand = new SqliteCommand(string.Format(CultureInfo.InvariantCulture, "SELECT [type], [name], [tbl_name], [rootpage], [sql], [rowid] FROM [{0}].[{1}] WHERE [type] LIKE 'table'", strCatalog, text), this))
			{
				using (SqliteDataReader sqliteDataReader = sqliteCommand.ExecuteReader())
				{
					while (sqliteDataReader.Read())
					{
						string text2 = sqliteDataReader.GetString(0);
						if (string.Compare(sqliteDataReader.GetString(2), 0, "SQLITE_", 0, 7, true, CultureInfo.InvariantCulture) == 0)
						{
							text2 = "SYSTEM_TABLE";
						}
						if ((string.Compare(strType, text2, true, CultureInfo.InvariantCulture) == 0 || strType == null) && (string.Compare(sqliteDataReader.GetString(2), strTable, true, CultureInfo.InvariantCulture) == 0 || strTable == null))
						{
							DataRow dataRow = dataTable.NewRow();
							dataRow["TABLE_CATALOG"] = strCatalog;
							dataRow["TABLE_NAME"] = sqliteDataReader.GetString(2);
							dataRow["TABLE_TYPE"] = text2;
							dataRow["TABLE_ID"] = sqliteDataReader.GetInt64(5);
							dataRow["TABLE_ROOTPAGE"] = sqliteDataReader.GetInt32(3);
							dataRow["TABLE_DEFINITION"] = sqliteDataReader.GetString(4);
							dataTable.Rows.Add(dataRow);
						}
					}
				}
			}
			dataTable.AcceptChanges();
			dataTable.EndLoadData();
			return dataTable;
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00004A04 File Offset: 0x00002C04
		private DataTable Schema_Views(string strCatalog, string strView)
		{
			DataTable dataTable = new DataTable("Views");
			dataTable.Locale = CultureInfo.InvariantCulture;
			dataTable.Columns.Add("TABLE_CATALOG", typeof(string));
			dataTable.Columns.Add("TABLE_SCHEMA", typeof(string));
			dataTable.Columns.Add("TABLE_NAME", typeof(string));
			dataTable.Columns.Add("VIEW_DEFINITION", typeof(string));
			dataTable.Columns.Add("CHECK_OPTION", typeof(bool));
			dataTable.Columns.Add("IS_UPDATABLE", typeof(bool));
			dataTable.Columns.Add("DESCRIPTION", typeof(string));
			dataTable.Columns.Add("DATE_CREATED", typeof(DateTime));
			dataTable.Columns.Add("DATE_MODIFIED", typeof(DateTime));
			dataTable.BeginLoadData();
			if (string.IsNullOrEmpty(strCatalog))
			{
				strCatalog = "main";
			}
			string text = ((string.Compare(strCatalog, "temp", true, CultureInfo.InvariantCulture) == 0) ? "sqlite_temp_master" : "sqlite_master");
			using (SqliteCommand sqliteCommand = new SqliteCommand(string.Format(CultureInfo.InvariantCulture, "SELECT * FROM [{0}].[{1}] WHERE [type] LIKE 'view'", strCatalog, text), this))
			{
				using (SqliteDataReader sqliteDataReader = sqliteCommand.ExecuteReader())
				{
					while (sqliteDataReader.Read())
					{
						if (string.Compare(sqliteDataReader.GetString(1), strView, true, CultureInfo.InvariantCulture) == 0 || string.IsNullOrEmpty(strView))
						{
							string text2 = sqliteDataReader.GetString(4).Replace('\r', ' ').Replace('\n', ' ')
								.Replace('\t', ' ');
							int num = CultureInfo.InvariantCulture.CompareInfo.IndexOf(text2, " AS ", CompareOptions.IgnoreCase);
							if (num > -1)
							{
								text2 = text2.Substring(num + 4).Trim();
								DataRow dataRow = dataTable.NewRow();
								dataRow["TABLE_CATALOG"] = strCatalog;
								dataRow["TABLE_NAME"] = sqliteDataReader.GetString(2);
								dataRow["IS_UPDATABLE"] = false;
								dataRow["VIEW_DEFINITION"] = text2;
								dataTable.Rows.Add(dataRow);
							}
						}
					}
				}
			}
			dataTable.AcceptChanges();
			dataTable.EndLoadData();
			return dataTable;
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00004C88 File Offset: 0x00002E88
		private DataTable Schema_Catalogs(string strCatalog)
		{
			DataTable dataTable = new DataTable("Catalogs");
			dataTable.Locale = CultureInfo.InvariantCulture;
			dataTable.Columns.Add("CATALOG_NAME", typeof(string));
			dataTable.Columns.Add("DESCRIPTION", typeof(string));
			dataTable.Columns.Add("ID", typeof(long));
			dataTable.BeginLoadData();
			using (SqliteCommand sqliteCommand = new SqliteCommand("PRAGMA database_list", this))
			{
				using (SqliteDataReader sqliteDataReader = sqliteCommand.ExecuteReader())
				{
					while (sqliteDataReader.Read())
					{
						if (string.Compare(sqliteDataReader.GetString(1), strCatalog, true, CultureInfo.InvariantCulture) == 0 || strCatalog == null)
						{
							DataRow dataRow = dataTable.NewRow();
							dataRow["CATALOG_NAME"] = sqliteDataReader.GetString(1);
							dataRow["DESCRIPTION"] = sqliteDataReader.GetString(2);
							dataRow["ID"] = sqliteDataReader.GetInt64(0);
							dataTable.Rows.Add(dataRow);
						}
					}
				}
			}
			dataTable.AcceptChanges();
			dataTable.EndLoadData();
			return dataTable;
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00004DC4 File Offset: 0x00002FC4
		private DataTable Schema_DataTypes()
		{
			DataTable dataTable = new DataTable("DataTypes");
			dataTable.Locale = CultureInfo.InvariantCulture;
			dataTable.Columns.Add("TypeName", typeof(string));
			dataTable.Columns.Add("ProviderDbType", typeof(int));
			dataTable.Columns.Add("ColumnSize", typeof(long));
			dataTable.Columns.Add("CreateFormat", typeof(string));
			dataTable.Columns.Add("CreateParameters", typeof(string));
			dataTable.Columns.Add("DataType", typeof(string));
			dataTable.Columns.Add("IsAutoIncrementable", typeof(bool));
			dataTable.Columns.Add("IsBestMatch", typeof(bool));
			dataTable.Columns.Add("IsCaseSensitive", typeof(bool));
			dataTable.Columns.Add("IsFixedLength", typeof(bool));
			dataTable.Columns.Add("IsFixedPrecisionScale", typeof(bool));
			dataTable.Columns.Add("IsLong", typeof(bool));
			dataTable.Columns.Add("IsNullable", typeof(bool));
			dataTable.Columns.Add("IsSearchable", typeof(bool));
			dataTable.Columns.Add("IsSearchableWithLike", typeof(bool));
			dataTable.Columns.Add("IsLiteralSupported", typeof(bool));
			dataTable.Columns.Add("LiteralPrefix", typeof(string));
			dataTable.Columns.Add("LiteralSuffix", typeof(string));
			dataTable.Columns.Add("IsUnsigned", typeof(bool));
			dataTable.Columns.Add("MaximumScale", typeof(short));
			dataTable.Columns.Add("MinimumScale", typeof(short));
			dataTable.Columns.Add("IsConcurrencyType", typeof(bool));
			dataTable.BeginLoadData();
			StringReader stringReader = new StringReader(SR.DataTypes);
			dataTable.ReadXml(stringReader);
			stringReader.Close();
			dataTable.AcceptChanges();
			dataTable.EndLoadData();
			return dataTable;
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00005064 File Offset: 0x00003264
		private DataTable Schema_IndexColumns(string strCatalog, string strTable, string strIndex, string strColumn)
		{
			DataTable dataTable = new DataTable("IndexColumns");
			List<KeyValuePair<int, string>> list = new List<KeyValuePair<int, string>>();
			dataTable.Locale = CultureInfo.InvariantCulture;
			dataTable.Columns.Add("CONSTRAINT_CATALOG", typeof(string));
			dataTable.Columns.Add("CONSTRAINT_SCHEMA", typeof(string));
			dataTable.Columns.Add("CONSTRAINT_NAME", typeof(string));
			dataTable.Columns.Add("TABLE_CATALOG", typeof(string));
			dataTable.Columns.Add("TABLE_SCHEMA", typeof(string));
			dataTable.Columns.Add("TABLE_NAME", typeof(string));
			dataTable.Columns.Add("COLUMN_NAME", typeof(string));
			dataTable.Columns.Add("ORDINAL_POSITION", typeof(int));
			dataTable.Columns.Add("INDEX_NAME", typeof(string));
			dataTable.Columns.Add("COLLATION_NAME", typeof(string));
			dataTable.Columns.Add("SORT_MODE", typeof(string));
			dataTable.Columns.Add("CONFLICT_OPTION", typeof(int));
			if (string.IsNullOrEmpty(strCatalog))
			{
				strCatalog = "main";
			}
			string text = ((string.Compare(strCatalog, "temp", true, CultureInfo.InvariantCulture) == 0) ? "sqlite_temp_master" : "sqlite_master");
			dataTable.BeginLoadData();
			using (SqliteCommand sqliteCommand = new SqliteCommand(string.Format(CultureInfo.InvariantCulture, "SELECT * FROM [{0}].[{1}] WHERE [type] LIKE 'table'", strCatalog, text), this))
			{
				using (SqliteDataReader sqliteDataReader = sqliteCommand.ExecuteReader())
				{
					while (sqliteDataReader.Read())
					{
						bool flag = false;
						list.Clear();
						if (string.IsNullOrEmpty(strTable) || string.Compare(sqliteDataReader.GetString(2), strTable, true, CultureInfo.InvariantCulture) == 0)
						{
							try
							{
								using (SqliteCommand sqliteCommand2 = new SqliteCommand(string.Format(CultureInfo.InvariantCulture, "PRAGMA [{0}].table_info([{1}])", strCatalog, sqliteDataReader.GetString(2)), this))
								{
									using (SqliteDataReader sqliteDataReader2 = sqliteCommand2.ExecuteReader())
									{
										while (sqliteDataReader2.Read())
										{
											if (sqliteDataReader2.GetInt32(5) == 1)
											{
												list.Add(new KeyValuePair<int, string>(sqliteDataReader2.GetInt32(0), sqliteDataReader2.GetString(1)));
												if (string.Compare(sqliteDataReader2.GetString(2), "INTEGER", true, CultureInfo.InvariantCulture) == 0)
												{
													flag = true;
												}
											}
										}
									}
								}
							}
							catch (SqliteException)
							{
							}
							if (list.Count == 1 && flag)
							{
								DataRow dataRow = dataTable.NewRow();
								dataRow["CONSTRAINT_CATALOG"] = strCatalog;
								dataRow["CONSTRAINT_NAME"] = string.Format(CultureInfo.InvariantCulture, "{1}_PK_{0}", sqliteDataReader.GetString(2), text);
								dataRow["TABLE_CATALOG"] = strCatalog;
								dataRow["TABLE_NAME"] = sqliteDataReader.GetString(2);
								dataRow["COLUMN_NAME"] = list[0].Value;
								dataRow["INDEX_NAME"] = dataRow["CONSTRAINT_NAME"];
								dataRow["ORDINAL_POSITION"] = 0;
								dataRow["COLLATION_NAME"] = "BINARY";
								dataRow["SORT_MODE"] = "ASC";
								dataRow["CONFLICT_OPTION"] = 2;
								if (string.IsNullOrEmpty(strIndex) || string.Compare(strIndex, (string)dataRow["INDEX_NAME"], true, CultureInfo.InvariantCulture) == 0)
								{
									dataTable.Rows.Add(dataRow);
								}
							}
							using (SqliteCommand sqliteCommand3 = new SqliteCommand(string.Format(CultureInfo.InvariantCulture, "SELECT * FROM [{0}].[{2}] WHERE [type] LIKE 'index' AND [tbl_name] LIKE '{1}'", strCatalog, sqliteDataReader.GetString(2).Replace("'", "''"), text), this))
							{
								using (SqliteDataReader sqliteDataReader3 = sqliteCommand3.ExecuteReader())
								{
									while (sqliteDataReader3.Read())
									{
										int num = 0;
										if (string.IsNullOrEmpty(strIndex) || string.Compare(strIndex, sqliteDataReader3.GetString(1), true, CultureInfo.InvariantCulture) == 0)
										{
											try
											{
												using (SqliteCommand sqliteCommand4 = new SqliteCommand(string.Format(CultureInfo.InvariantCulture, "PRAGMA [{0}].index_info([{1}])", strCatalog, sqliteDataReader3.GetString(1)), this))
												{
													using (SqliteDataReader sqliteDataReader4 = sqliteCommand4.ExecuteReader())
													{
														while (sqliteDataReader4.Read())
														{
															DataRow dataRow = dataTable.NewRow();
															dataRow["CONSTRAINT_CATALOG"] = strCatalog;
															dataRow["CONSTRAINT_NAME"] = sqliteDataReader3.GetString(1);
															dataRow["TABLE_CATALOG"] = strCatalog;
															dataRow["TABLE_NAME"] = sqliteDataReader3.GetString(2);
															dataRow["COLUMN_NAME"] = sqliteDataReader4.GetString(2);
															dataRow["INDEX_NAME"] = sqliteDataReader3.GetString(1);
															dataRow["ORDINAL_POSITION"] = num;
															int num2;
															int num3;
															string text2;
															this._sql.GetIndexColumnExtendedInfo(strCatalog, sqliteDataReader3.GetString(1), sqliteDataReader4.GetString(2), out num2, out num3, out text2);
															if (!string.IsNullOrEmpty(text2))
															{
																dataRow["COLLATION_NAME"] = text2;
															}
															dataRow["SORT_MODE"] = ((num2 == 0) ? "ASC" : "DESC");
															dataRow["CONFLICT_OPTION"] = num3;
															num++;
															if (string.IsNullOrEmpty(strColumn) || string.Compare(strColumn, dataRow["COLUMN_NAME"].ToString(), true, CultureInfo.InvariantCulture) == 0)
															{
																dataTable.Rows.Add(dataRow);
															}
														}
													}
												}
											}
											catch (SqliteException)
											{
											}
										}
									}
								}
							}
						}
					}
				}
			}
			dataTable.EndLoadData();
			dataTable.AcceptChanges();
			return dataTable;
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00005724 File Offset: 0x00003924
		private DataTable Schema_ViewColumns(string strCatalog, string strView, string strColumn)
		{
			DataTable dataTable = new DataTable("ViewColumns");
			dataTable.Locale = CultureInfo.InvariantCulture;
			dataTable.Columns.Add("VIEW_CATALOG", typeof(string));
			dataTable.Columns.Add("VIEW_SCHEMA", typeof(string));
			dataTable.Columns.Add("VIEW_NAME", typeof(string));
			dataTable.Columns.Add("VIEW_COLUMN_NAME", typeof(string));
			dataTable.Columns.Add("TABLE_CATALOG", typeof(string));
			dataTable.Columns.Add("TABLE_SCHEMA", typeof(string));
			dataTable.Columns.Add("TABLE_NAME", typeof(string));
			dataTable.Columns.Add("COLUMN_NAME", typeof(string));
			dataTable.Columns.Add("ORDINAL_POSITION", typeof(int));
			dataTable.Columns.Add("COLUMN_HASDEFAULT", typeof(bool));
			dataTable.Columns.Add("COLUMN_DEFAULT", typeof(string));
			dataTable.Columns.Add("COLUMN_FLAGS", typeof(long));
			dataTable.Columns.Add("IS_NULLABLE", typeof(bool));
			dataTable.Columns.Add("DATA_TYPE", typeof(string));
			dataTable.Columns.Add("CHARACTER_MAXIMUM_LENGTH", typeof(int));
			dataTable.Columns.Add("NUMERIC_PRECISION", typeof(int));
			dataTable.Columns.Add("NUMERIC_SCALE", typeof(int));
			dataTable.Columns.Add("DATETIME_PRECISION", typeof(long));
			dataTable.Columns.Add("CHARACTER_SET_CATALOG", typeof(string));
			dataTable.Columns.Add("CHARACTER_SET_SCHEMA", typeof(string));
			dataTable.Columns.Add("CHARACTER_SET_NAME", typeof(string));
			dataTable.Columns.Add("COLLATION_CATALOG", typeof(string));
			dataTable.Columns.Add("COLLATION_SCHEMA", typeof(string));
			dataTable.Columns.Add("COLLATION_NAME", typeof(string));
			dataTable.Columns.Add("PRIMARY_KEY", typeof(bool));
			dataTable.Columns.Add("EDM_TYPE", typeof(string));
			dataTable.Columns.Add("AUTOINCREMENT", typeof(bool));
			dataTable.Columns.Add("UNIQUE", typeof(bool));
			if (string.IsNullOrEmpty(strCatalog))
			{
				strCatalog = "main";
			}
			string text = ((string.Compare(strCatalog, "temp", true, CultureInfo.InvariantCulture) == 0) ? "sqlite_temp_master" : "sqlite_master");
			dataTable.BeginLoadData();
			using (SqliteCommand sqliteCommand = new SqliteCommand(string.Format(CultureInfo.InvariantCulture, "SELECT * FROM [{0}].[{1}] WHERE [type] LIKE 'view'", strCatalog, text), this))
			{
				using (SqliteDataReader sqliteDataReader = sqliteCommand.ExecuteReader())
				{
					while (sqliteDataReader.Read())
					{
						if (string.IsNullOrEmpty(strView) || string.Compare(strView, sqliteDataReader.GetString(2), true, CultureInfo.InvariantCulture) == 0)
						{
							using (SqliteCommand sqliteCommand2 = new SqliteCommand(string.Format(CultureInfo.InvariantCulture, "SELECT * FROM [{0}].[{1}]", strCatalog, sqliteDataReader.GetString(2)), this))
							{
								string text2 = sqliteDataReader.GetString(4).Replace('\r', ' ').Replace('\n', ' ')
									.Replace('\t', ' ');
								int i = CultureInfo.InvariantCulture.CompareInfo.IndexOf(text2, " AS ", CompareOptions.IgnoreCase);
								if (i >= 0)
								{
									text2 = text2.Substring(i + 4);
									using (SqliteCommand sqliteCommand3 = new SqliteCommand(text2, this))
									{
										using (SqliteDataReader sqliteDataReader2 = sqliteCommand2.ExecuteReader(CommandBehavior.SchemaOnly))
										{
											using (SqliteDataReader sqliteDataReader3 = sqliteCommand3.ExecuteReader(CommandBehavior.SchemaOnly))
											{
												using (DataTable schemaTable = sqliteDataReader2.GetSchemaTable(false, false))
												{
													using (DataTable schemaTable2 = sqliteDataReader3.GetSchemaTable(false, false))
													{
														for (i = 0; i < schemaTable2.Rows.Count; i++)
														{
															DataRow dataRow = schemaTable.Rows[i];
															DataRow dataRow2 = schemaTable2.Rows[i];
															if (string.Compare(dataRow[SchemaTableColumn.ColumnName].ToString(), strColumn, true, CultureInfo.InvariantCulture) == 0 || strColumn == null)
															{
																DataRow dataRow3 = dataTable.NewRow();
																dataRow3["VIEW_CATALOG"] = strCatalog;
																dataRow3["VIEW_NAME"] = sqliteDataReader.GetString(2);
																dataRow3["TABLE_CATALOG"] = strCatalog;
																dataRow3["TABLE_SCHEMA"] = dataRow2[SchemaTableColumn.BaseSchemaName];
																dataRow3["TABLE_NAME"] = dataRow2[SchemaTableColumn.BaseTableName];
																dataRow3["COLUMN_NAME"] = dataRow2[SchemaTableColumn.BaseColumnName];
																dataRow3["VIEW_COLUMN_NAME"] = dataRow[SchemaTableColumn.ColumnName];
																dataRow3["COLUMN_HASDEFAULT"] = dataRow[SchemaTableOptionalColumn.DefaultValue] != DBNull.Value;
																dataRow3["COLUMN_DEFAULT"] = dataRow[SchemaTableOptionalColumn.DefaultValue];
																dataRow3["ORDINAL_POSITION"] = dataRow[SchemaTableColumn.ColumnOrdinal];
																dataRow3["IS_NULLABLE"] = dataRow[SchemaTableColumn.AllowDBNull];
																dataRow3["DATA_TYPE"] = dataRow["DataTypeName"];
																dataRow3["EDM_TYPE"] = SqliteConvert.DbTypeToTypeName((DbType)dataRow[SchemaTableColumn.ProviderType]).ToString().ToLower(CultureInfo.InvariantCulture);
																dataRow3["CHARACTER_MAXIMUM_LENGTH"] = dataRow[SchemaTableColumn.ColumnSize];
																dataRow3["TABLE_SCHEMA"] = dataRow[SchemaTableColumn.BaseSchemaName];
																dataRow3["PRIMARY_KEY"] = dataRow[SchemaTableColumn.IsKey];
																dataRow3["AUTOINCREMENT"] = dataRow[SchemaTableOptionalColumn.IsAutoIncrement];
																dataRow3["COLLATION_NAME"] = dataRow["CollationType"];
																dataRow3["UNIQUE"] = dataRow[SchemaTableColumn.IsUnique];
																dataTable.Rows.Add(dataRow3);
															}
														}
													}
												}
											}
										}
									}
								}
							}
						}
					}
				}
			}
			dataTable.EndLoadData();
			dataTable.AcceptChanges();
			return dataTable;
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00005ED4 File Offset: 0x000040D4
		private DataTable Schema_ForeignKeys(string strCatalog, string strTable, string strKeyName)
		{
			DataTable dataTable = new DataTable("ForeignKeys");
			dataTable.Locale = CultureInfo.InvariantCulture;
			dataTable.Columns.Add("CONSTRAINT_CATALOG", typeof(string));
			dataTable.Columns.Add("CONSTRAINT_SCHEMA", typeof(string));
			dataTable.Columns.Add("CONSTRAINT_NAME", typeof(string));
			dataTable.Columns.Add("TABLE_CATALOG", typeof(string));
			dataTable.Columns.Add("TABLE_SCHEMA", typeof(string));
			dataTable.Columns.Add("TABLE_NAME", typeof(string));
			dataTable.Columns.Add("CONSTRAINT_TYPE", typeof(string));
			dataTable.Columns.Add("IS_DEFERRABLE", typeof(bool));
			dataTable.Columns.Add("INITIALLY_DEFERRED", typeof(bool));
			dataTable.Columns.Add("FKEY_FROM_COLUMN", typeof(string));
			dataTable.Columns.Add("FKEY_FROM_ORDINAL_POSITION", typeof(int));
			dataTable.Columns.Add("FKEY_TO_CATALOG", typeof(string));
			dataTable.Columns.Add("FKEY_TO_SCHEMA", typeof(string));
			dataTable.Columns.Add("FKEY_TO_TABLE", typeof(string));
			dataTable.Columns.Add("FKEY_TO_COLUMN", typeof(string));
			if (string.IsNullOrEmpty(strCatalog))
			{
				strCatalog = "main";
			}
			string text = ((string.Compare(strCatalog, "temp", true, CultureInfo.InvariantCulture) == 0) ? "sqlite_temp_master" : "sqlite_master");
			dataTable.BeginLoadData();
			using (SqliteCommand sqliteCommand = new SqliteCommand(string.Format(CultureInfo.InvariantCulture, "SELECT * FROM [{0}].[{1}] WHERE [type] LIKE 'table'", strCatalog, text), this))
			{
				using (SqliteDataReader sqliteDataReader = sqliteCommand.ExecuteReader())
				{
					while (sqliteDataReader.Read())
					{
						if (string.IsNullOrEmpty(strTable) || string.Compare(strTable, sqliteDataReader.GetString(2), true, CultureInfo.InvariantCulture) == 0)
						{
							try
							{
								using (SqliteCommandBuilder sqliteCommandBuilder = new SqliteCommandBuilder())
								{
									using (SqliteCommand sqliteCommand2 = new SqliteCommand(string.Format(CultureInfo.InvariantCulture, "PRAGMA [{0}].foreign_key_list([{1}])", strCatalog, sqliteDataReader.GetString(2)), this))
									{
										using (SqliteDataReader sqliteDataReader2 = sqliteCommand2.ExecuteReader())
										{
											while (sqliteDataReader2.Read())
											{
												DataRow dataRow = dataTable.NewRow();
												dataRow["CONSTRAINT_CATALOG"] = strCatalog;
												dataRow["CONSTRAINT_NAME"] = string.Format(CultureInfo.InvariantCulture, "FK_{0}_{1}", sqliteDataReader[2], sqliteDataReader2.GetInt32(0));
												dataRow["TABLE_CATALOG"] = strCatalog;
												dataRow["TABLE_NAME"] = sqliteCommandBuilder.UnquoteIdentifier(sqliteDataReader.GetString(2));
												dataRow["CONSTRAINT_TYPE"] = "FOREIGN KEY";
												dataRow["IS_DEFERRABLE"] = false;
												dataRow["INITIALLY_DEFERRED"] = false;
												dataRow["FKEY_FROM_COLUMN"] = sqliteCommandBuilder.UnquoteIdentifier(sqliteDataReader2[3].ToString());
												dataRow["FKEY_TO_CATALOG"] = strCatalog;
												dataRow["FKEY_TO_TABLE"] = sqliteCommandBuilder.UnquoteIdentifier(sqliteDataReader2[2].ToString());
												dataRow["FKEY_TO_COLUMN"] = sqliteCommandBuilder.UnquoteIdentifier(sqliteDataReader2[4].ToString());
												dataRow["FKEY_FROM_ORDINAL_POSITION"] = sqliteDataReader2[1];
												if (string.IsNullOrEmpty(strKeyName) || string.Compare(strKeyName, dataRow["CONSTRAINT_NAME"].ToString(), true, CultureInfo.InvariantCulture) == 0)
												{
													dataTable.Rows.Add(dataRow);
												}
											}
										}
									}
								}
							}
							catch (SqliteException)
							{
							}
						}
					}
				}
			}
			dataTable.EndLoadData();
			dataTable.AcceptChanges();
			return dataTable;
		}

		// Token: 0x14000005 RID: 5
		// (add) Token: 0x06000041 RID: 65 RVA: 0x00006388 File Offset: 0x00004588
		// (remove) Token: 0x06000042 RID: 66 RVA: 0x000063C4 File Offset: 0x000045C4
		public event SQLiteUpdateEventHandler Update
		{
			add
			{
				if (this._updateHandler == null)
				{
					this._updateCallback = new SQLiteUpdateCallback(this.UpdateCallback);
					if (this._sql != null)
					{
						this._sql.SetUpdateHook(this._updateCallback);
					}
				}
				this._updateHandler += value;
			}
			remove
			{
				this._updateHandler -= value;
				if (this._updateHandler == null)
				{
					if (this._sql != null)
					{
						this._sql.SetUpdateHook(null);
					}
					this._updateCallback = null;
				}
			}
		}

		// Token: 0x06000043 RID: 67 RVA: 0x000063F0 File Offset: 0x000045F0
		private void UpdateCallback(IntPtr puser, int type, IntPtr database, IntPtr table, long rowid)
		{
			this._updateHandler(this, new UpdateEventArgs(SqliteConvert.UTF8ToString(database, -1), SqliteConvert.UTF8ToString(table, -1), (UpdateEventType)type, rowid));
		}

		// Token: 0x14000006 RID: 6
		// (add) Token: 0x06000044 RID: 68 RVA: 0x00006415 File Offset: 0x00004615
		// (remove) Token: 0x06000045 RID: 69 RVA: 0x00006451 File Offset: 0x00004651
		public event SQLiteCommitHandler Commit
		{
			add
			{
				if (this._commitHandler == null)
				{
					this._commitCallback = new SQLiteCommitCallback(this.CommitCallback);
					if (this._sql != null)
					{
						this._sql.SetCommitHook(this._commitCallback);
					}
				}
				this._commitHandler += value;
			}
			remove
			{
				this._commitHandler -= value;
				if (this._commitHandler == null)
				{
					if (this._sql != null)
					{
						this._sql.SetCommitHook(null);
					}
					this._commitCallback = null;
				}
			}
		}

		// Token: 0x14000007 RID: 7
		// (add) Token: 0x06000046 RID: 70 RVA: 0x0000647D File Offset: 0x0000467D
		// (remove) Token: 0x06000047 RID: 71 RVA: 0x000064B9 File Offset: 0x000046B9
		public event EventHandler RollBack
		{
			add
			{
				if (this._rollbackHandler == null)
				{
					this._rollbackCallback = new SQLiteRollbackCallback(this.RollbackCallback);
					if (this._sql != null)
					{
						this._sql.SetRollbackHook(this._rollbackCallback);
					}
				}
				this._rollbackHandler += value;
			}
			remove
			{
				this._rollbackHandler -= value;
				if (this._rollbackHandler == null)
				{
					if (this._sql != null)
					{
						this._sql.SetRollbackHook(null);
					}
					this._rollbackCallback = null;
				}
			}
		}

		// Token: 0x06000048 RID: 72 RVA: 0x000064E8 File Offset: 0x000046E8
		private int CommitCallback(IntPtr parg)
		{
			CommitEventArgs commitEventArgs = new CommitEventArgs();
			this._commitHandler(this, commitEventArgs);
			if (!commitEventArgs.AbortTransaction)
			{
				return 0;
			}
			return 1;
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00006513 File Offset: 0x00004713
		private void RollbackCallback(IntPtr parg)
		{
			this._rollbackHandler(this, EventArgs.Empty);
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00006528 File Offset: 0x00004728
		public static void SetConfig(SQLiteConfig config)
		{
			int num = UnsafeNativeMethods.sqlite3_config(config);
			if (num > 0)
			{
				throw new SqliteException(num, null);
			}
		}

		// Token: 0x0400002A RID: 42
		private const string _dataDirectory = "|DataDirectory|";

		// Token: 0x0400002B RID: 43
		private const string _masterdb = "sqlite_master";

		// Token: 0x0400002C RID: 44
		private const string _tempmasterdb = "sqlite_temp_master";

		// Token: 0x0400002D RID: 45
		private ConnectionState _connectionState;

		// Token: 0x0400002E RID: 46
		private string _connectionString;

		// Token: 0x0400002F RID: 47
		internal int _transactionLevel;

		// Token: 0x04000030 RID: 48
		private global::System.Data.IsolationLevel _defaultIsolation;

		// Token: 0x04000031 RID: 49
		internal SQLiteEnlistment _enlistment;

		// Token: 0x04000032 RID: 50
		internal SQLiteBase _sql;

		// Token: 0x04000033 RID: 51
		private string _dataSource;

		// Token: 0x04000034 RID: 52
		private byte[] _password;

		// Token: 0x04000035 RID: 53
		private int _defaultTimeout = 30;

		// Token: 0x04000036 RID: 54
		internal bool _binaryGuid;

		// Token: 0x04000037 RID: 55
		internal long _version;

		// Token: 0x0400003B RID: 59
		private SQLiteUpdateCallback _updateCallback;

		// Token: 0x0400003C RID: 60
		private SQLiteCommitCallback _commitCallback;

		// Token: 0x0400003D RID: 61
		private SQLiteRollbackCallback _rollbackCallback;
	}
}
