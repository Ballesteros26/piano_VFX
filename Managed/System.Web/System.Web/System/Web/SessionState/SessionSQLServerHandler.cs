using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration.Provider;
using System.Data;
using System.Data.Common;
using System.IO;
using System.IO.Compression;
using System.Web.Configuration;
using System.Web.Hosting;

namespace System.Web.SessionState
{
	// Token: 0x0200049F RID: 1183
	internal sealed class SessionSQLServerHandler : SessionStateStoreProviderBase
	{
		// Token: 0x170010F2 RID: 4338
		// (get) Token: 0x060035AB RID: 13739 RVA: 0x0008C714 File Offset: 0x0008A914
		private DbProviderFactory ProviderFactory
		{
			get
			{
				if (this.providerFactory == null)
				{
					try
					{
						this.providerFactory = Activator.CreateInstance(this.providerFactoryType) as DbProviderFactory;
					}
					catch (Exception ex)
					{
						throw new ProviderException("Failure to create database factory instance.", ex);
					}
				}
				return this.providerFactory;
			}
		}

		// Token: 0x170010F3 RID: 4339
		// (get) Token: 0x060035AC RID: 13740 RVA: 0x0008C764 File Offset: 0x0008A964
		// (set) Token: 0x060035AD RID: 13741 RVA: 0x0008C76C File Offset: 0x0008A96C
		public string ApplicationName { get; private set; }

		// Token: 0x060035AE RID: 13742 RVA: 0x0008C778 File Offset: 0x0008A978
		public override void Initialize(string name, NameValueCollection config)
		{
			if (config == null)
			{
				throw new ArgumentNullException("config");
			}
			if (string.IsNullOrEmpty(name))
			{
				name = "SessionSQLServerHandler";
			}
			if (string.IsNullOrEmpty(config["description"]))
			{
				config.Remove("description");
				config.Add("description", "Mono SQL Session Store Provider");
			}
			this.ApplicationName = HostingEnvironment.ApplicationVirtualPath;
			base.Initialize(name, config);
			this.sessionConfig = WebConfigurationManager.GetWebApplicationSection("system.web/sessionState") as SessionStateSection;
			this.connectionString = this.sessionConfig.SqlConnectionString;
			string text;
			if (string.IsNullOrEmpty(this.connectionString) || string.Compare(this.connectionString, SessionStateSection.DefaultSqlConnectionString, StringComparison.Ordinal) == 0)
			{
				this.connectionString = "Data Source=|DataDirectory|/ASPState.sqlite;Version=3";
				text = SessionSQLServerHandler.defaultDbFactoryTypeName;
			}
			else
			{
				string[] array = this.connectionString.Split(new char[] { ';' });
				List<string> list = new List<string>();
				text = null;
				bool allowCustomSqlDatabase = this.sessionConfig.AllowCustomSqlDatabase;
				foreach (string text2 in array)
				{
					if (text2.Trim().Length != 0)
					{
						if (text2.StartsWith("DbProviderName", StringComparison.OrdinalIgnoreCase))
						{
							int num = text2.IndexOf('=');
							if (num < 0)
							{
								throw new ProviderException("Invalid format for the 'DbProviderName' connection string parameter. Expected 'DbProviderName = value'.");
							}
							text = text2.Substring(num + 1);
						}
						else
						{
							if (!allowCustomSqlDatabase)
							{
								string text3 = text2.Trim();
								if (text3.StartsWith("database", StringComparison.OrdinalIgnoreCase) || text3.StartsWith("initial catalog", StringComparison.OrdinalIgnoreCase))
								{
									throw new ProviderException("Specifying a custom database is not allowed. Set the allowCustomSqlDatabase attribute of the <system.web/sessionState> section to 'true' in order to use a custom database name.");
								}
							}
							list.Add(text2);
						}
					}
				}
				this.connectionString = string.Join(";", list.ToArray());
				if (string.IsNullOrEmpty(text))
				{
					text = SessionSQLServerHandler.defaultDbFactoryTypeName;
				}
			}
			Exception ex = null;
			try
			{
				this.providerFactoryType = Type.GetType(text, true);
			}
			catch (Exception ex)
			{
				this.providerFactoryType = null;
			}
			if (this.providerFactoryType == null)
			{
				throw new ProviderException("Unable to find database provider factory type.", ex);
			}
			this.sqlCommandTimeout = (int)this.sessionConfig.SqlCommandTimeout.TotalSeconds;
		}

		// Token: 0x060035AF RID: 13743 RVA: 0x0000393A File Offset: 0x00001B3A
		public override void Dispose()
		{
		}

		// Token: 0x060035B0 RID: 13744 RVA: 0x00008A69 File Offset: 0x00006C69
		public override bool SetItemExpireCallback(SessionStateItemExpireCallback expireCallback)
		{
			return false;
		}

		// Token: 0x060035B1 RID: 13745 RVA: 0x0008C994 File Offset: 0x0008AB94
		public override void SetAndReleaseItemExclusive(HttpContext context, string id, SessionStateStoreData item, object lockId, bool newItem)
		{
			DbCommand dbCommand = null;
			string text = this.Serialize((SessionStateItemCollection)item.Items);
			DbProviderFactory dbProviderFactory = this.ProviderFactory;
			string applicationName = this.ApplicationName;
			DbConnection dbConnection = this.CreateConnection(dbProviderFactory);
			DateTime now = DateTime.Now;
			DbCommand dbCommand2;
			if (newItem)
			{
				dbCommand = this.CreateCommand(dbProviderFactory, dbConnection, "DELETE FROM Sessions WHERE SessionId = @SessionId AND ApplicationName = @ApplicationName AND Expires < @Expires");
				DbParameterCollection parameters = dbCommand.Parameters;
				parameters.Add(this.CreateParameter<string>(dbProviderFactory, "@SessionId", id, 80));
				parameters.Add(this.CreateParameter<string>(dbProviderFactory, "@ApplicationName", applicationName, 255));
				parameters.Add(this.CreateParameter<DateTime>(dbProviderFactory, "@Expires", now));
				dbCommand2 = this.CreateCommand(dbProviderFactory, dbConnection, "INSERT INTO Sessions (SessionId, ApplicationName, Created, Expires, LockDate, LockId, Timeout, Locked, SessionItems, Flags) Values (@SessionId, @ApplicationName, @Created, @Expires, @LockDate, @LockId , @Timeout, @Locked, @SessionItems, @Flags)");
				DbParameterCollection parameters2 = dbCommand2.Parameters;
				parameters2.Add(this.CreateParameter<string>(dbProviderFactory, "@SessionId", id, 80));
				parameters2.Add(this.CreateParameter<string>(dbProviderFactory, "@ApplicationName", applicationName, 255));
				parameters2.Add(this.CreateParameter<DateTime>(dbProviderFactory, "@Created", now));
				parameters2.Add(this.CreateParameter<DateTime>(dbProviderFactory, "@Expires", now.AddMinutes((double)item.Timeout)));
				parameters2.Add(this.CreateParameter<DateTime>(dbProviderFactory, "@LockDate", now));
				parameters2.Add(this.CreateParameter<int>(dbProviderFactory, "@LockId", 0));
				parameters2.Add(this.CreateParameter<int>(dbProviderFactory, "@Timeout", item.Timeout));
				parameters2.Add(this.CreateParameter<bool>(dbProviderFactory, "@Locked", false));
				parameters2.Add(this.CreateParameter<string>(dbProviderFactory, "@SessionItems", text));
				parameters2.Add(this.CreateParameter<int>(dbProviderFactory, "@Flags", 0));
			}
			else
			{
				dbCommand2 = this.CreateCommand(dbProviderFactory, dbConnection, "UPDATE Sessions SET Expires = @Expires, SessionItems = @SessionItems, Locked = @Locked WHERE SessionId = @SessionId AND ApplicationName = @ApplicationName AND LockId = @LockId");
				DbParameterCollection parameters3 = dbCommand2.Parameters;
				parameters3.Add(this.CreateParameter<DateTime>(dbProviderFactory, "@Expires", now.AddMinutes((double)item.Timeout)));
				parameters3.Add(this.CreateParameter<string>(dbProviderFactory, "@SessionItems", text));
				parameters3.Add(this.CreateParameter<bool>(dbProviderFactory, "@Locked", false));
				parameters3.Add(this.CreateParameter<string>(dbProviderFactory, "@SessionId", id, 80));
				parameters3.Add(this.CreateParameter<string>(dbProviderFactory, "@ApplicationName", applicationName, 255));
				parameters3.Add(this.CreateParameter<int>(dbProviderFactory, "@Lockid", (int)lockId));
			}
			try
			{
				dbConnection.Open();
				if (dbCommand != null)
				{
					dbCommand.ExecuteNonQuery();
				}
				dbCommand2.ExecuteNonQuery();
			}
			catch (Exception ex)
			{
				throw new ProviderException("Failure storing session item in database.", ex);
			}
			finally
			{
				dbConnection.Close();
			}
		}

		// Token: 0x060035B2 RID: 13746 RVA: 0x0008CC28 File Offset: 0x0008AE28
		public override SessionStateStoreData GetItem(HttpContext context, string id, out bool locked, out TimeSpan lockAge, out object lockId, out SessionStateActions actionFlags)
		{
			return this.GetSessionStoreItem(false, context, id, out locked, out lockAge, out lockId, out actionFlags);
		}

		// Token: 0x060035B3 RID: 13747 RVA: 0x0008CC3A File Offset: 0x0008AE3A
		public override SessionStateStoreData GetItemExclusive(HttpContext context, string id, out bool locked, out TimeSpan lockAge, out object lockId, out SessionStateActions actionFlags)
		{
			return this.GetSessionStoreItem(true, context, id, out locked, out lockAge, out lockId, out actionFlags);
		}

		// Token: 0x060035B4 RID: 13748 RVA: 0x0008CC4C File Offset: 0x0008AE4C
		private SessionStateStoreData GetSessionStoreItem(bool lockRecord, HttpContext context, string id, out bool locked, out TimeSpan lockAge, out object lockId, out SessionStateActions actionFlags)
		{
			SessionStateStoreData sessionStateStoreData = null;
			lockAge = TimeSpan.Zero;
			lockId = null;
			locked = false;
			actionFlags = SessionStateActions.None;
			DbProviderFactory dbProviderFactory = this.ProviderFactory;
			DbConnection dbConnection = this.CreateConnection(dbProviderFactory);
			string applicationName = this.ApplicationName;
			DbDataReader dbDataReader = null;
			string text = string.Empty;
			bool flag = false;
			bool flag2 = false;
			int num = 0;
			DateTime now = DateTime.Now;
			try
			{
				dbConnection.Open();
				if (lockRecord)
				{
					DbCommand dbCommand = this.CreateCommand(dbProviderFactory, dbConnection, "UPDATE Sessions SET Locked = @Locked, LockDate = @LockDate WHERE SessionId = @SessionId AND ApplicationName = @ApplicationName AND Expires > @Expires");
					DbParameterCollection parameters = dbCommand.Parameters;
					parameters.Add(this.CreateParameter<bool>(dbProviderFactory, "@Locked", true));
					parameters.Add(this.CreateParameter<DateTime>(dbProviderFactory, "@LockDate", now));
					parameters.Add(this.CreateParameter<string>(dbProviderFactory, "@SessionId", id, 80));
					parameters.Add(this.CreateParameter<string>(dbProviderFactory, "@ApplicationName", applicationName, 255));
					parameters.Add(this.CreateParameter<DateTime>(dbProviderFactory, "@Expires", now));
					if (dbCommand.ExecuteNonQuery() == 0)
					{
						locked = true;
					}
					else
					{
						locked = false;
					}
				}
				DbCommand dbCommand2 = this.CreateCommand(dbProviderFactory, dbConnection, "SELECT Expires, SessionItems, LockId, LockDate, Flags, Timeout FROM Sessions WHERE SessionId = @SessionId AND ApplicationName = @ApplicationName");
				DbParameterCollection parameters2 = dbCommand2.Parameters;
				parameters2.Add(this.CreateParameter<string>(dbProviderFactory, "@SessionId", id, 80));
				parameters2.Add(this.CreateParameter<string>(dbProviderFactory, "@ApplicationName", applicationName, 255));
				dbDataReader = dbCommand2.ExecuteReader(CommandBehavior.SingleRow);
				while (dbDataReader.Read())
				{
					if (dbDataReader.GetDateTime(dbDataReader.GetOrdinal("Expires")) < now)
					{
						locked = false;
						flag2 = true;
					}
					else
					{
						flag = true;
					}
					text = dbDataReader.GetString(dbDataReader.GetOrdinal("SessionItems"));
					lockId = dbDataReader.GetInt32(dbDataReader.GetOrdinal("LockId"));
					lockAge = now.Subtract(dbDataReader.GetDateTime(dbDataReader.GetOrdinal("LockDate")));
					actionFlags = (SessionStateActions)dbDataReader.GetInt32(dbDataReader.GetOrdinal("Flags"));
					num = dbDataReader.GetInt32(dbDataReader.GetOrdinal("Timeout"));
				}
				dbDataReader.Close();
				if (flag2)
				{
					DbCommand dbCommand3 = this.CreateCommand(dbProviderFactory, dbConnection, "DELETE FROM Sessions WHERE SessionId = @SessionId AND ApplicationName = @ApplicationName");
					DbParameterCollection parameters3 = dbCommand3.Parameters;
					parameters3.Add(this.CreateParameter<string>(dbProviderFactory, "@SessionId", id, 80));
					parameters3.Add(this.CreateParameter<string>(dbProviderFactory, "@ApplicationName", applicationName, 255));
					dbCommand3.ExecuteNonQuery();
				}
				if (!flag)
				{
					locked = false;
				}
				if (flag && !locked)
				{
					lockId = (int)lockId + 1;
					DbCommand dbCommand4 = this.CreateCommand(dbProviderFactory, dbConnection, "UPDATE Sessions SET LockId = @LockId, Flags = 0 WHERE SessionId = @SessionId AND ApplicationName = @ApplicationName");
					DbParameterCollection parameters4 = dbCommand4.Parameters;
					parameters4.Add(this.CreateParameter<int>(dbProviderFactory, "@LockId", (int)lockId));
					parameters4.Add(this.CreateParameter<string>(dbProviderFactory, "@SessionId", id, 80));
					parameters4.Add(this.CreateParameter<string>(dbProviderFactory, "@ApplicationName", applicationName, 255));
					dbCommand4.ExecuteNonQuery();
					if (actionFlags == SessionStateActions.InitializeItem)
					{
						sessionStateStoreData = this.CreateNewStoreData(context, (int)this.sessionConfig.Timeout.TotalMinutes);
					}
					else
					{
						sessionStateStoreData = this.Deserialize(context, text, num);
					}
				}
			}
			catch (Exception ex)
			{
				throw new ProviderException("Unable to retrieve session item from database.", ex);
			}
			finally
			{
				if (dbDataReader != null)
				{
					dbDataReader.Close();
				}
				dbConnection.Close();
			}
			return sessionStateStoreData;
		}

		// Token: 0x060035B5 RID: 13749 RVA: 0x0008CFA4 File Offset: 0x0008B1A4
		private string Serialize(SessionStateItemCollection items)
		{
			GZipStream gzipStream = null;
			MemoryStream memoryStream = null;
			BinaryWriter binaryWriter = null;
			string text;
			try
			{
				memoryStream = new MemoryStream();
				Stream stream;
				if (this.sessionConfig.CompressionEnabled)
				{
					gzipStream = (stream = new GZipStream(memoryStream, CompressionMode.Compress, true));
				}
				else
				{
					stream = memoryStream;
				}
				binaryWriter = new BinaryWriter(stream);
				if (items != null)
				{
					items.Serialize(binaryWriter);
				}
				if (gzipStream != null)
				{
					gzipStream.Close();
				}
				binaryWriter.Close();
				text = Convert.ToBase64String(memoryStream.ToArray());
			}
			finally
			{
				if (binaryWriter != null)
				{
					binaryWriter.Dispose();
				}
				if (gzipStream != null)
				{
					gzipStream.Dispose();
				}
				if (memoryStream != null)
				{
					memoryStream.Dispose();
				}
			}
			return text;
		}

		// Token: 0x060035B6 RID: 13750 RVA: 0x0008D038 File Offset: 0x0008B238
		private SessionStateStoreData Deserialize(HttpContext context, string serializedItems, int timeout)
		{
			MemoryStream memoryStream = null;
			BinaryReader binaryReader = null;
			GZipStream gzipStream = null;
			SessionStateStoreData sessionStateStoreData;
			try
			{
				memoryStream = new MemoryStream(Convert.FromBase64String(serializedItems));
				SessionStateItemCollection sessionStateItemCollection = new SessionStateItemCollection();
				if (memoryStream.Length > 0L)
				{
					Stream stream;
					if (this.sessionConfig.CompressionEnabled)
					{
						gzipStream = (stream = new GZipStream(memoryStream, CompressionMode.Decompress, true));
					}
					else
					{
						stream = memoryStream;
					}
					binaryReader = new BinaryReader(stream);
					sessionStateItemCollection = SessionStateItemCollection.Deserialize(binaryReader);
					if (gzipStream != null)
					{
						gzipStream.Close();
					}
					binaryReader.Close();
				}
				sessionStateStoreData = new SessionStateStoreData(sessionStateItemCollection, SessionStateUtility.GetSessionStaticObjects(context), timeout);
			}
			finally
			{
				if (binaryReader != null)
				{
					binaryReader.Dispose();
				}
				if (gzipStream != null)
				{
					gzipStream.Dispose();
				}
				if (memoryStream != null)
				{
					memoryStream.Dispose();
				}
			}
			return sessionStateStoreData;
		}

		// Token: 0x060035B7 RID: 13751 RVA: 0x0008D0E4 File Offset: 0x0008B2E4
		public override void ReleaseItemExclusive(HttpContext context, string id, object lockId)
		{
			DbProviderFactory dbProviderFactory = this.ProviderFactory;
			DbConnection dbConnection = this.CreateConnection(dbProviderFactory);
			DbCommand dbCommand = this.CreateCommand(dbProviderFactory, dbConnection, "UPDATE Sessions SET Locked = 0, Expires = @Expires WHERE SessionId = @SessionId AND ApplicationName = @ApplicationName AND LockId = @LockId");
			DbParameterCollection parameters = dbCommand.Parameters;
			parameters.Add(this.CreateParameter<DateTime>(dbProviderFactory, "@Expires", DateTime.Now.AddMinutes(this.sessionConfig.Timeout.TotalMinutes)));
			parameters.Add(this.CreateParameter<string>(dbProviderFactory, "@SessionId", id, 80));
			parameters.Add(this.CreateParameter<string>(dbProviderFactory, "@ApplicationName", this.ApplicationName, 255));
			parameters.Add(this.CreateParameter<int>(dbProviderFactory, "@LockId", (int)lockId));
			try
			{
				dbConnection.Open();
				dbCommand.ExecuteNonQuery();
			}
			catch (Exception ex)
			{
				throw new ProviderException("Error releasing item in database.", ex);
			}
			finally
			{
				dbConnection.Close();
			}
		}

		// Token: 0x060035B8 RID: 13752 RVA: 0x0008D1D8 File Offset: 0x0008B3D8
		public override void RemoveItem(HttpContext context, string id, object lockId, SessionStateStoreData item)
		{
			DbProviderFactory dbProviderFactory = this.ProviderFactory;
			DbConnection dbConnection = this.CreateConnection(dbProviderFactory);
			DbCommand dbCommand = this.CreateCommand(dbProviderFactory, dbConnection, "DELETE FROM Sessions WHERE SessionId = @SessionId AND ApplicationName = @ApplicationName AND LockId = @LockId");
			DbParameterCollection parameters = dbCommand.Parameters;
			parameters.Add(this.CreateParameter<string>(dbProviderFactory, "@SessionId", id, 80));
			parameters.Add(this.CreateParameter<string>(dbProviderFactory, "@ApplicationName", this.ApplicationName, 255));
			parameters.Add(this.CreateParameter<int>(dbProviderFactory, "@LockId", (int)lockId));
			try
			{
				dbConnection.Open();
				dbCommand.ExecuteNonQuery();
			}
			catch (Exception ex)
			{
				throw new ProviderException("Error removing item from database.", ex);
			}
			finally
			{
				dbConnection.Close();
			}
		}

		// Token: 0x060035B9 RID: 13753 RVA: 0x0008D294 File Offset: 0x0008B494
		public override void CreateUninitializedItem(HttpContext context, string id, int timeout)
		{
			DbProviderFactory dbProviderFactory = this.ProviderFactory;
			DbConnection dbConnection = this.CreateConnection(dbProviderFactory);
			DbCommand dbCommand = this.CreateCommand(dbProviderFactory, dbConnection, "INSERT INTO Sessions (SessionId, ApplicationName, Created, Expires, LockDate, LockId, Timeout, Locked, SessionItems, Flags) Values (@SessionId, @ApplicationName, @Created, @Expires, @LockDate, @LockId , @Timeout, @Locked, @SessionItems, @Flags)");
			DateTime now = DateTime.Now;
			DbParameterCollection parameters = dbCommand.Parameters;
			parameters.Add(this.CreateParameter<string>(dbProviderFactory, "@SessionId", id, 80));
			parameters.Add(this.CreateParameter<string>(dbProviderFactory, "@ApplicationName", this.ApplicationName, 255));
			parameters.Add(this.CreateParameter<DateTime>(dbProviderFactory, "@Created", now));
			parameters.Add(this.CreateParameter<DateTime>(dbProviderFactory, "@Expires", now.AddMinutes((double)timeout)));
			parameters.Add(this.CreateParameter<DateTime>(dbProviderFactory, "@LockDate", now));
			parameters.Add(this.CreateParameter<int>(dbProviderFactory, "@LockId", 0));
			parameters.Add(this.CreateParameter<int>(dbProviderFactory, "@Timeout", timeout));
			parameters.Add(this.CreateParameter<bool>(dbProviderFactory, "@Locked", false));
			parameters.Add(this.CreateParameter<string>(dbProviderFactory, "@SessionItems", string.Empty));
			parameters.Add(this.CreateParameter<int>(dbProviderFactory, "@Flags", 1));
			try
			{
				dbConnection.Open();
				dbCommand.ExecuteNonQuery();
			}
			catch (Exception ex)
			{
				throw new ProviderException("Error creating uninitialized session item in the database.", ex);
			}
			finally
			{
				dbConnection.Close();
			}
		}

		// Token: 0x060035BA RID: 13754 RVA: 0x0008D3EC File Offset: 0x0008B5EC
		public override SessionStateStoreData CreateNewStoreData(HttpContext context, int timeout)
		{
			return new SessionStateStoreData(new SessionStateItemCollection(), SessionStateUtility.GetSessionStaticObjects(context), timeout);
		}

		// Token: 0x060035BB RID: 13755 RVA: 0x0008D400 File Offset: 0x0008B600
		public override void ResetItemTimeout(HttpContext context, string id)
		{
			DbProviderFactory dbProviderFactory = this.ProviderFactory;
			DbConnection dbConnection = this.CreateConnection(dbProviderFactory);
			DbCommand dbCommand = this.CreateCommand(dbProviderFactory, dbConnection, "UPDATE Sessions SET Expires = @Expires WHERE SessionId = @SessionId AND ApplicationName = @ApplicationName");
			DbParameterCollection parameters = dbCommand.Parameters;
			parameters.Add(this.CreateParameter<DateTime>(dbProviderFactory, "@Expires", DateTime.Now.AddMinutes(this.sessionConfig.Timeout.TotalMinutes)));
			parameters.Add(this.CreateParameter<string>(dbProviderFactory, "@SessionId", id, 80));
			parameters.Add(this.CreateParameter<string>(dbProviderFactory, "@ApplicationName", this.ApplicationName, 255));
			try
			{
				dbConnection.Open();
				dbCommand.ExecuteNonQuery();
			}
			catch (Exception ex)
			{
				throw new ProviderException("Error resetting session item timeout in the database.", ex);
			}
			finally
			{
				dbConnection.Close();
			}
		}

		// Token: 0x060035BC RID: 13756 RVA: 0x0000393A File Offset: 0x00001B3A
		public override void InitializeRequest(HttpContext context)
		{
		}

		// Token: 0x060035BD RID: 13757 RVA: 0x0000393A File Offset: 0x00001B3A
		public override void EndRequest(HttpContext context)
		{
		}

		// Token: 0x060035BE RID: 13758 RVA: 0x0008D4D8 File Offset: 0x0008B6D8
		private DbConnection CreateConnection(DbProviderFactory factory)
		{
			DbConnection dbConnection = factory.CreateConnection();
			dbConnection.ConnectionString = this.connectionString;
			return dbConnection;
		}

		// Token: 0x060035BF RID: 13759 RVA: 0x0008D4EC File Offset: 0x0008B6EC
		private DbCommand CreateCommand(DbProviderFactory factory, DbConnection conn, string commandText)
		{
			DbCommand dbCommand = factory.CreateCommand();
			dbCommand.CommandTimeout = this.sqlCommandTimeout;
			dbCommand.Connection = conn;
			dbCommand.CommandText = commandText;
			return dbCommand;
		}

		// Token: 0x060035C0 RID: 13760 RVA: 0x0008D50E File Offset: 0x0008B70E
		private DbParameter CreateParameter<ValueType>(DbProviderFactory factory, string name, ValueType value)
		{
			return this.CreateParameter<ValueType>(factory, name, value, -1);
		}

		// Token: 0x060035C1 RID: 13761 RVA: 0x0008D51C File Offset: 0x0008B71C
		private DbParameter CreateParameter<ValueType>(DbProviderFactory factory, string name, ValueType value, int size)
		{
			DbParameter dbParameter = factory.CreateParameter();
			dbParameter.ParameterName = name;
			Type typeFromHandle = typeof(ValueType);
			if (typeFromHandle == typeof(string))
			{
				dbParameter.DbType = DbType.String;
			}
			else if (typeFromHandle == typeof(int))
			{
				dbParameter.DbType = DbType.Int32;
			}
			else if (typeFromHandle == typeof(bool))
			{
				dbParameter.DbType = DbType.Boolean;
			}
			else if (typeFromHandle == typeof(DateTime))
			{
				dbParameter.DbType = DbType.DateTime;
			}
			if (size > -1)
			{
				dbParameter.Size = size;
			}
			dbParameter.Value = value;
			return dbParameter;
		}

		// Token: 0x04001D6E RID: 7534
		private static readonly string defaultDbFactoryTypeName = "Mono.Data.Sqlite.SqliteFactory, Mono.Data.Sqlite, Version=2.0.0.0, Culture=neutral, PublicKeyToken=0738eb9f132ed756";

		// Token: 0x04001D6F RID: 7535
		private SessionStateSection sessionConfig;

		// Token: 0x04001D70 RID: 7536
		private string connectionString;

		// Token: 0x04001D71 RID: 7537
		private Type providerFactoryType;

		// Token: 0x04001D72 RID: 7538
		private DbProviderFactory providerFactory;

		// Token: 0x04001D73 RID: 7539
		private int sqlCommandTimeout;
	}
}
