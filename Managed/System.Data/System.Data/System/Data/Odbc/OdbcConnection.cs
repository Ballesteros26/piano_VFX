using System;
using System.ComponentModel;
using System.Data.Common;
using System.Data.ProviderBase;
using System.EnterpriseServices;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Transactions;
using Unity;

namespace System.Data.Odbc
{
	/// <summary>Represents an open connection to a data source. </summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x0200028E RID: 654
	public sealed class OdbcConnection : DbConnection, ICloneable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.Odbc.OdbcConnection" /> class with the specified connection string.</summary>
		/// <param name="connectionString">The connection used to open the data source. </param>
		// Token: 0x06001B52 RID: 6994 RVA: 0x00088D8E File Offset: 0x00086F8E
		public OdbcConnection(string connectionString)
			: this()
		{
			this.ConnectionString = connectionString;
		}

		// Token: 0x06001B53 RID: 6995 RVA: 0x00088D9D File Offset: 0x00086F9D
		private OdbcConnection(OdbcConnection connection)
			: this()
		{
			this.CopyFrom(connection);
			this._connectionTimeout = connection._connectionTimeout;
		}

		// Token: 0x170004F8 RID: 1272
		// (get) Token: 0x06001B54 RID: 6996 RVA: 0x00088DB8 File Offset: 0x00086FB8
		// (set) Token: 0x06001B55 RID: 6997 RVA: 0x00088DC0 File Offset: 0x00086FC0
		internal OdbcConnectionHandle ConnectionHandle
		{
			get
			{
				return this._connectionHandle;
			}
			set
			{
				this._connectionHandle = value;
			}
		}

		/// <summary>Gets or sets the string used to open a data source.</summary>
		/// <returns>The ODBC driver connection string that includes settings, such as the data source name, needed to establish the initial connection. The default value is an empty string (""). The maximum length is 1024 characters.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170004F9 RID: 1273
		// (get) Token: 0x06001B56 RID: 6998 RVA: 0x00088DC9 File Offset: 0x00086FC9
		// (set) Token: 0x06001B57 RID: 6999 RVA: 0x00088DD1 File Offset: 0x00086FD1
		public override string ConnectionString
		{
			get
			{
				return this.ConnectionString_Get();
			}
			set
			{
				this.ConnectionString_Set(value);
			}
		}

		/// <summary>Gets or sets the time to wait while trying to establish a connection before terminating the attempt and generating an error.</summary>
		/// <returns>The time in seconds to wait for a connection to open. The default value is 15 seconds.</returns>
		/// <exception cref="T:System.ArgumentException">The value set is less than 0. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x170004FA RID: 1274
		// (get) Token: 0x06001B58 RID: 7000 RVA: 0x00088DDA File Offset: 0x00086FDA
		// (set) Token: 0x06001B59 RID: 7001 RVA: 0x00088DE2 File Offset: 0x00086FE2
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[DefaultValue(15)]
		public new int ConnectionTimeout
		{
			get
			{
				return this._connectionTimeout;
			}
			set
			{
				if (value < 0)
				{
					throw ODBC.NegativeArgument();
				}
				if (this.IsOpen)
				{
					throw ODBC.CantSetPropertyOnOpenConnection();
				}
				this._connectionTimeout = value;
			}
		}

		/// <summary>Gets the name of the current database or the database to be used after a connection is opened.</summary>
		/// <returns>The name of the current database. The default value is an empty string ("") until the connection is opened.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x170004FB RID: 1275
		// (get) Token: 0x06001B5A RID: 7002 RVA: 0x00088E03 File Offset: 0x00087003
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override string Database
		{
			get
			{
				if (this.IsOpen && !this.ProviderInfo.NoCurrentCatalog)
				{
					return this.GetConnectAttrString(ODBC32.SQL_ATTR.CURRENT_CATALOG);
				}
				return string.Empty;
			}
		}

		/// <summary>Gets the server name or file name of the data source.</summary>
		/// <returns>The server name or file name of the data source. The default value is an empty string ("") until the connection is opened.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170004FC RID: 1276
		// (get) Token: 0x06001B5B RID: 7003 RVA: 0x00088E28 File Offset: 0x00087028
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override string DataSource
		{
			get
			{
				if (this.IsOpen)
				{
					return this.GetInfoStringUnhandled(ODBC32.SQL_INFO.SERVER_NAME, true);
				}
				return string.Empty;
			}
		}

		/// <summary>Gets a string that contains the version of the server to which the client is connected.</summary>
		/// <returns>The version of the connected server.</returns>
		/// <exception cref="T:System.InvalidOperationException">The connection is closed. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170004FD RID: 1277
		// (get) Token: 0x06001B5C RID: 7004 RVA: 0x00088E41 File Offset: 0x00087041
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public override string ServerVersion
		{
			get
			{
				return this.InnerConnection.ServerVersion;
			}
		}

		/// <summary>Gets the current state of the connection.</summary>
		/// <returns>A bitwise combination of the <see cref="T:System.Data.ConnectionState" /> values. The default is Closed.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170004FE RID: 1278
		// (get) Token: 0x06001B5D RID: 7005 RVA: 0x00088E4E File Offset: 0x0008704E
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override ConnectionState State
		{
			get
			{
				return this.InnerConnection.State;
			}
		}

		// Token: 0x170004FF RID: 1279
		// (get) Token: 0x06001B5E RID: 7006 RVA: 0x00088E5B File Offset: 0x0008705B
		internal OdbcConnectionPoolGroupProviderInfo ProviderInfo
		{
			get
			{
				return (OdbcConnectionPoolGroupProviderInfo)this.PoolGroup.ProviderInfo;
			}
		}

		// Token: 0x17000500 RID: 1280
		// (get) Token: 0x06001B5F RID: 7007 RVA: 0x00088E6D File Offset: 0x0008706D
		internal ConnectionState InternalState
		{
			get
			{
				return this.State | this._extraState;
			}
		}

		// Token: 0x17000501 RID: 1281
		// (get) Token: 0x06001B60 RID: 7008 RVA: 0x00088E7C File Offset: 0x0008707C
		internal bool IsOpen
		{
			get
			{
				return this.InnerConnection is OdbcConnectionOpen;
			}
		}

		// Token: 0x17000502 RID: 1282
		// (get) Token: 0x06001B61 RID: 7009 RVA: 0x00088E8C File Offset: 0x0008708C
		// (set) Token: 0x06001B62 RID: 7010 RVA: 0x00088EB5 File Offset: 0x000870B5
		internal OdbcTransaction LocalTransaction
		{
			get
			{
				OdbcTransaction odbcTransaction = null;
				if (this._weakTransaction != null)
				{
					odbcTransaction = (OdbcTransaction)this._weakTransaction.Target;
				}
				return odbcTransaction;
			}
			set
			{
				this._weakTransaction = null;
				if (value != null)
				{
					this._weakTransaction = new WeakReference(value);
				}
			}
		}

		/// <summary>Gets the name of the ODBC driver specified for the current connection.</summary>
		/// <returns>The name of the ODBC driver. This typically is the DLL name (for example, Sqlsrv32.dll). The default value is an empty string ("") until the connection is opened.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000503 RID: 1283
		// (get) Token: 0x06001B63 RID: 7011 RVA: 0x00088ECD File Offset: 0x000870CD
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public string Driver
		{
			get
			{
				if (this.IsOpen)
				{
					if (this.ProviderInfo.DriverName == null)
					{
						this.ProviderInfo.DriverName = this.GetInfoStringUnhandled(ODBC32.SQL_INFO.DRIVER_NAME);
					}
					return this.ProviderInfo.DriverName;
				}
				return ADP.StrEmpty;
			}
		}

		// Token: 0x17000504 RID: 1284
		// (get) Token: 0x06001B64 RID: 7012 RVA: 0x00088F08 File Offset: 0x00087108
		internal bool IsV3Driver
		{
			get
			{
				if (this.ProviderInfo.DriverVersion == null)
				{
					this.ProviderInfo.DriverVersion = this.GetInfoStringUnhandled(ODBC32.SQL_INFO.DRIVER_ODBC_VER);
					if (this.ProviderInfo.DriverVersion != null && this.ProviderInfo.DriverVersion.Length >= 2)
					{
						try
						{
							this.ProviderInfo.IsV3Driver = int.Parse(this.ProviderInfo.DriverVersion.Substring(0, 2), CultureInfo.InvariantCulture) >= 3;
							goto IL_0095;
						}
						catch (FormatException ex)
						{
							this.ProviderInfo.IsV3Driver = false;
							ADP.TraceExceptionWithoutRethrow(ex);
							goto IL_0095;
						}
					}
					this.ProviderInfo.DriverVersion = "";
				}
				IL_0095:
				return this.ProviderInfo.IsV3Driver;
			}
		}

		/// <summary>Occurs when the ODBC driver sends a warning or an informational message.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x14000029 RID: 41
		// (add) Token: 0x06001B65 RID: 7013 RVA: 0x00088FC8 File Offset: 0x000871C8
		// (remove) Token: 0x06001B66 RID: 7014 RVA: 0x00088FE1 File Offset: 0x000871E1
		public event OdbcInfoMessageEventHandler InfoMessage
		{
			add
			{
				this._infoMessageEventHandler = (OdbcInfoMessageEventHandler)Delegate.Combine(this._infoMessageEventHandler, value);
			}
			remove
			{
				this._infoMessageEventHandler = (OdbcInfoMessageEventHandler)Delegate.Remove(this._infoMessageEventHandler, value);
			}
		}

		// Token: 0x06001B67 RID: 7015 RVA: 0x00088FFC File Offset: 0x000871FC
		internal char EscapeChar(string method)
		{
			this.CheckState(method);
			if (!this.ProviderInfo.HasEscapeChar)
			{
				string infoStringUnhandled = this.GetInfoStringUnhandled(ODBC32.SQL_INFO.SEARCH_PATTERN_ESCAPE);
				this.ProviderInfo.EscapeChar = ((infoStringUnhandled.Length == 1) ? infoStringUnhandled[0] : this.QuoteChar(method)[0]);
			}
			return this.ProviderInfo.EscapeChar;
		}

		// Token: 0x06001B68 RID: 7016 RVA: 0x0008905C File Offset: 0x0008725C
		internal string QuoteChar(string method)
		{
			this.CheckState(method);
			if (!this.ProviderInfo.HasQuoteChar)
			{
				string infoStringUnhandled = this.GetInfoStringUnhandled(ODBC32.SQL_INFO.IDENTIFIER_QUOTE_CHAR);
				this.ProviderInfo.QuoteChar = ((1 == infoStringUnhandled.Length) ? infoStringUnhandled : "\0");
			}
			return this.ProviderInfo.QuoteChar;
		}

		/// <summary>Starts a transaction at the data source.</summary>
		/// <returns>An object representing the new transaction.</returns>
		/// <exception cref="T:System.InvalidOperationException">A transaction is currently active. Parallel transactions are not supported. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001B69 RID: 7017 RVA: 0x000890AD File Offset: 0x000872AD
		public new OdbcTransaction BeginTransaction()
		{
			return this.BeginTransaction(IsolationLevel.Unspecified);
		}

		/// <summary>Starts a transaction at the data source with the specified <see cref="T:System.Data.IsolationLevel" /> value.</summary>
		/// <returns>An object representing the new transaction.</returns>
		/// <param name="isolevel">The transaction isolation level for this connection. If you do not specify an isolation level, the default isolation level for the driver is used. </param>
		/// <exception cref="T:System.InvalidOperationException">A transaction is currently active. Parallel transactions are not supported. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001B6A RID: 7018 RVA: 0x000890B6 File Offset: 0x000872B6
		public new OdbcTransaction BeginTransaction(IsolationLevel isolevel)
		{
			return (OdbcTransaction)this.InnerConnection.BeginTransaction(isolevel);
		}

		// Token: 0x06001B6B RID: 7019 RVA: 0x000890CC File Offset: 0x000872CC
		private void RollbackDeadTransaction()
		{
			WeakReference weakTransaction = this._weakTransaction;
			if (weakTransaction != null && !weakTransaction.IsAlive)
			{
				this._weakTransaction = null;
				this.ConnectionHandle.CompleteTransaction(1);
			}
		}

		/// <summary>Changes the current database associated with an open <see cref="T:System.Data.Odbc.OdbcConnection" />.</summary>
		/// <param name="value">The database name. </param>
		/// <exception cref="T:System.ArgumentException">The database name is not valid. </exception>
		/// <exception cref="T:System.InvalidOperationException">The connection is not open. </exception>
		/// <exception cref="T:System.Data.Odbc.OdbcException">Cannot change the database. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06001B6C RID: 7020 RVA: 0x000890FF File Offset: 0x000872FF
		public override void ChangeDatabase(string value)
		{
			this.InnerConnection.ChangeDatabase(value);
		}

		// Token: 0x06001B6D RID: 7021 RVA: 0x00089110 File Offset: 0x00087310
		internal void CheckState(string method)
		{
			ConnectionState internalState = this.InternalState;
			if (ConnectionState.Open != internalState)
			{
				throw ADP.OpenConnectionRequired(method, internalState);
			}
		}

		/// <summary>For a description of this member, see <see cref="M:System.ICloneable.Clone" />.</summary>
		/// <returns>A new <see cref="T:System.Object" /> that is a copy of this instance.</returns>
		// Token: 0x06001B6E RID: 7022 RVA: 0x00089130 File Offset: 0x00087330
		object ICloneable.Clone()
		{
			return new OdbcConnection(this);
		}

		// Token: 0x06001B6F RID: 7023 RVA: 0x00089138 File Offset: 0x00087338
		internal bool ConnectionIsAlive(Exception innerException)
		{
			if (this.IsOpen)
			{
				if (!this.ProviderInfo.NoConnectionDead)
				{
					int connectAttr = this.GetConnectAttr(ODBC32.SQL_ATTR.CONNECTION_DEAD, ODBC32.HANDLER.IGNORE);
					if (1 == connectAttr)
					{
						this.Close();
						throw ADP.ConnectionIsDisabled(innerException);
					}
				}
				return true;
			}
			return false;
		}

		/// <summary>Creates and returns an <see cref="T:System.Data.Odbc.OdbcCommand" /> object associated with the <see cref="T:System.Data.Odbc.OdbcConnection" />.</summary>
		/// <returns>An <see cref="T:System.Data.Odbc.OdbcCommand" /> object.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001B70 RID: 7024 RVA: 0x0008917B File Offset: 0x0008737B
		public new OdbcCommand CreateCommand()
		{
			return new OdbcCommand(string.Empty, this);
		}

		// Token: 0x06001B71 RID: 7025 RVA: 0x00089188 File Offset: 0x00087388
		internal OdbcStatementHandle CreateStatementHandle()
		{
			return new OdbcStatementHandle(this.ConnectionHandle);
		}

		/// <summary>Closes the connection to the data source. </summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001B72 RID: 7026 RVA: 0x00089198 File Offset: 0x00087398
		public override void Close()
		{
			this.InnerConnection.CloseConnection(this, this.ConnectionFactory);
			OdbcConnectionHandle connectionHandle = this._connectionHandle;
			if (connectionHandle != null)
			{
				this._connectionHandle = null;
				WeakReference weakTransaction = this._weakTransaction;
				if (weakTransaction != null)
				{
					this._weakTransaction = null;
					IDisposable disposable = weakTransaction.Target as OdbcTransaction;
					if (disposable != null && weakTransaction.IsAlive)
					{
						disposable.Dispose();
					}
				}
				connectionHandle.Dispose();
			}
		}

		// Token: 0x06001B73 RID: 7027 RVA: 0x00005E03 File Offset: 0x00004003
		private void DisposeMe(bool disposing)
		{
		}

		// Token: 0x06001B74 RID: 7028 RVA: 0x000891FC File Offset: 0x000873FC
		internal string GetConnectAttrString(ODBC32.SQL_ATTR attribute)
		{
			string text = "";
			int num = 0;
			byte[] array = new byte[100];
			OdbcConnectionHandle connectionHandle = this.ConnectionHandle;
			if (connectionHandle != null)
			{
				ODBC32.RetCode retCode = connectionHandle.GetConnectionAttribute(attribute, array, out num);
				if (array.Length + 2 <= num)
				{
					array = new byte[num + 2];
					retCode = connectionHandle.GetConnectionAttribute(attribute, array, out num);
				}
				if (retCode == ODBC32.RetCode.SUCCESS || ODBC32.RetCode.SUCCESS_WITH_INFO == retCode)
				{
					text = Encoding.Unicode.GetString(array, 0, Math.Min(num, array.Length));
				}
				else if (retCode == ODBC32.RetCode.ERROR)
				{
					string diagSqlState = this.GetDiagSqlState();
					if ("HYC00" == diagSqlState || "HY092" == diagSqlState || "IM001" == diagSqlState)
					{
						this.FlagUnsupportedConnectAttr(attribute);
					}
				}
			}
			return text;
		}

		// Token: 0x06001B75 RID: 7029 RVA: 0x000892B0 File Offset: 0x000874B0
		internal int GetConnectAttr(ODBC32.SQL_ATTR attribute, ODBC32.HANDLER handler)
		{
			int num = -1;
			int num2 = 0;
			byte[] array = new byte[4];
			OdbcConnectionHandle connectionHandle = this.ConnectionHandle;
			if (connectionHandle != null)
			{
				ODBC32.RetCode connectionAttribute = connectionHandle.GetConnectionAttribute(attribute, array, out num2);
				if (connectionAttribute == ODBC32.RetCode.SUCCESS || ODBC32.RetCode.SUCCESS_WITH_INFO == connectionAttribute)
				{
					num = BitConverter.ToInt32(array, 0);
				}
				else
				{
					if (connectionAttribute == ODBC32.RetCode.ERROR)
					{
						string diagSqlState = this.GetDiagSqlState();
						if ("HYC00" == diagSqlState || "HY092" == diagSqlState || "IM001" == diagSqlState)
						{
							this.FlagUnsupportedConnectAttr(attribute);
						}
					}
					if (handler == ODBC32.HANDLER.THROW)
					{
						this.HandleError(connectionHandle, connectionAttribute);
					}
				}
			}
			return num;
		}

		// Token: 0x06001B76 RID: 7030 RVA: 0x00089340 File Offset: 0x00087540
		private string GetDiagSqlState()
		{
			string text;
			this.ConnectionHandle.GetDiagnosticField(out text);
			return text;
		}

		// Token: 0x06001B77 RID: 7031 RVA: 0x0008935C File Offset: 0x0008755C
		internal ODBC32.RetCode GetInfoInt16Unhandled(ODBC32.SQL_INFO info, out short resultValue)
		{
			byte[] array = new byte[2];
			ODBC32.RetCode info2 = this.ConnectionHandle.GetInfo1(info, array);
			resultValue = BitConverter.ToInt16(array, 0);
			return info2;
		}

		// Token: 0x06001B78 RID: 7032 RVA: 0x00089388 File Offset: 0x00087588
		internal ODBC32.RetCode GetInfoInt32Unhandled(ODBC32.SQL_INFO info, out int resultValue)
		{
			byte[] array = new byte[4];
			ODBC32.RetCode info2 = this.ConnectionHandle.GetInfo1(info, array);
			resultValue = BitConverter.ToInt32(array, 0);
			return info2;
		}

		// Token: 0x06001B79 RID: 7033 RVA: 0x000893B4 File Offset: 0x000875B4
		private int GetInfoInt32Unhandled(ODBC32.SQL_INFO infotype)
		{
			byte[] array = new byte[4];
			this.ConnectionHandle.GetInfo1(infotype, array);
			return BitConverter.ToInt32(array, 0);
		}

		// Token: 0x06001B7A RID: 7034 RVA: 0x000893DD File Offset: 0x000875DD
		internal string GetInfoStringUnhandled(ODBC32.SQL_INFO info)
		{
			return this.GetInfoStringUnhandled(info, false);
		}

		// Token: 0x06001B7B RID: 7035 RVA: 0x000893E8 File Offset: 0x000875E8
		private string GetInfoStringUnhandled(ODBC32.SQL_INFO info, bool handleError)
		{
			string text = null;
			short num = 0;
			byte[] array = new byte[100];
			OdbcConnectionHandle connectionHandle = this.ConnectionHandle;
			if (connectionHandle != null)
			{
				ODBC32.RetCode retCode = connectionHandle.GetInfo2(info, array, out num);
				if (array.Length < (int)(num - 2))
				{
					array = new byte[(int)(num + 2)];
					retCode = connectionHandle.GetInfo2(info, array, out num);
				}
				if (retCode == ODBC32.RetCode.SUCCESS || retCode == ODBC32.RetCode.SUCCESS_WITH_INFO)
				{
					text = Encoding.Unicode.GetString(array, 0, Math.Min((int)num, array.Length));
				}
				else if (handleError)
				{
					this.HandleError(this.ConnectionHandle, retCode);
				}
			}
			else if (handleError)
			{
				text = "";
			}
			return text;
		}

		// Token: 0x06001B7C RID: 7036 RVA: 0x00089474 File Offset: 0x00087674
		internal Exception HandleErrorNoThrow(OdbcHandle hrHandle, ODBC32.RetCode retcode)
		{
			if (retcode != ODBC32.RetCode.SUCCESS)
			{
				if (retcode != ODBC32.RetCode.SUCCESS_WITH_INFO)
				{
					OdbcException ex = OdbcException.CreateException(ODBC32.GetDiagErrors(null, hrHandle, retcode), retcode);
					if (ex != null)
					{
						ex.Errors.SetSource(this.Driver);
					}
					this.ConnectionIsAlive(ex);
					return ex;
				}
				if (this._infoMessageEventHandler != null)
				{
					OdbcErrorCollection diagErrors = ODBC32.GetDiagErrors(null, hrHandle, retcode);
					diagErrors.SetSource(this.Driver);
					this.OnInfoMessage(new OdbcInfoMessageEventArgs(diagErrors));
				}
			}
			return null;
		}

		// Token: 0x06001B7D RID: 7037 RVA: 0x000894E4 File Offset: 0x000876E4
		internal void HandleError(OdbcHandle hrHandle, ODBC32.RetCode retcode)
		{
			Exception ex = this.HandleErrorNoThrow(hrHandle, retcode);
			if (retcode > ODBC32.RetCode.SUCCESS_WITH_INFO)
			{
				throw ex;
			}
		}

		/// <summary>Opens a connection to a data source with the property settings specified by the <see cref="P:System.Data.Odbc.OdbcConnection.ConnectionString" />.</summary>
		/// <exception cref="T:System.NotSupportedException">The functionality of this method is unsupported in the base class and must be implemented in a derived class instead. </exception>
		// Token: 0x06001B7E RID: 7038 RVA: 0x00089500 File Offset: 0x00087700
		public override void Open()
		{
			try
			{
				this.InnerConnection.OpenConnection(this, this.ConnectionFactory);
			}
			catch (DllNotFoundException ex) when (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
			{
				throw new DllNotFoundException("Dependency unixODBC with minimum version 2.3.1 is required." + Environment.NewLine + ex.Message);
			}
			if (ADP.NeedManualEnlistment())
			{
				this.EnlistTransaction(Transaction.Current);
			}
		}

		// Token: 0x06001B7F RID: 7039 RVA: 0x00089580 File Offset: 0x00087780
		private void OnInfoMessage(OdbcInfoMessageEventArgs args)
		{
			if (this._infoMessageEventHandler != null)
			{
				try
				{
					this._infoMessageEventHandler(this, args);
				}
				catch (Exception ex)
				{
					if (!ADP.IsCatchableOrSecurityExceptionType(ex))
					{
						throw;
					}
					ADP.TraceExceptionWithoutRethrow(ex);
				}
			}
		}

		/// <summary>Indicates that the ODBC Driver Manager environment handle can be released when the last underlying connection is released.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001B80 RID: 7040 RVA: 0x000895C8 File Offset: 0x000877C8
		public static void ReleaseObjectPool()
		{
			OdbcEnvironment.ReleaseObjectPool();
		}

		// Token: 0x06001B81 RID: 7041 RVA: 0x000895D0 File Offset: 0x000877D0
		internal OdbcTransaction SetStateExecuting(string method, OdbcTransaction transaction)
		{
			if (this._weakTransaction != null)
			{
				OdbcTransaction odbcTransaction = this._weakTransaction.Target as OdbcTransaction;
				if (transaction != odbcTransaction)
				{
					if (transaction == null)
					{
						throw ADP.TransactionRequired(method);
					}
					if (this != transaction.Connection)
					{
						throw ADP.TransactionConnectionMismatch();
					}
					transaction = null;
				}
			}
			else if (transaction != null)
			{
				if (transaction.Connection != null)
				{
					throw ADP.TransactionConnectionMismatch();
				}
				transaction = null;
			}
			ConnectionState connectionState = this.InternalState;
			if (ConnectionState.Open != connectionState)
			{
				this.NotifyWeakReference(1);
				connectionState = this.InternalState;
				if (ConnectionState.Open != connectionState)
				{
					if ((ConnectionState.Fetching & connectionState) != ConnectionState.Closed)
					{
						throw ADP.OpenReaderExists();
					}
					throw ADP.OpenConnectionRequired(method, connectionState);
				}
			}
			return transaction;
		}

		// Token: 0x06001B82 RID: 7042 RVA: 0x00089660 File Offset: 0x00087860
		internal void SetSupportedType(ODBC32.SQL_TYPE sqltype)
		{
			ODBC32.SQL_CVT sql_CVT;
			switch (sqltype)
			{
			case ODBC32.SQL_TYPE.WLONGVARCHAR:
				sql_CVT = ODBC32.SQL_CVT.WLONGVARCHAR;
				break;
			case ODBC32.SQL_TYPE.WVARCHAR:
				sql_CVT = ODBC32.SQL_CVT.WVARCHAR;
				break;
			case ODBC32.SQL_TYPE.WCHAR:
				sql_CVT = ODBC32.SQL_CVT.WCHAR;
				break;
			default:
				if (sqltype != ODBC32.SQL_TYPE.NUMERIC)
				{
					return;
				}
				sql_CVT = ODBC32.SQL_CVT.NUMERIC;
				break;
			}
			this.ProviderInfo.TestedSQLTypes |= (int)sql_CVT;
			this.ProviderInfo.SupportedSQLTypes |= (int)sql_CVT;
		}

		// Token: 0x06001B83 RID: 7043 RVA: 0x000896CC File Offset: 0x000878CC
		internal void FlagRestrictedSqlBindType(ODBC32.SQL_TYPE sqltype)
		{
			ODBC32.SQL_CVT sql_CVT;
			if (sqltype != ODBC32.SQL_TYPE.NUMERIC)
			{
				if (sqltype != ODBC32.SQL_TYPE.DECIMAL)
				{
					return;
				}
				sql_CVT = ODBC32.SQL_CVT.DECIMAL;
			}
			else
			{
				sql_CVT = ODBC32.SQL_CVT.NUMERIC;
			}
			this.ProviderInfo.RestrictedSQLBindTypes |= (int)sql_CVT;
		}

		// Token: 0x06001B84 RID: 7044 RVA: 0x000896FB File Offset: 0x000878FB
		internal void FlagUnsupportedConnectAttr(ODBC32.SQL_ATTR Attribute)
		{
			if (Attribute == ODBC32.SQL_ATTR.CURRENT_CATALOG)
			{
				this.ProviderInfo.NoCurrentCatalog = true;
				return;
			}
			if (Attribute != ODBC32.SQL_ATTR.CONNECTION_DEAD)
			{
				return;
			}
			this.ProviderInfo.NoConnectionDead = true;
		}

		// Token: 0x06001B85 RID: 7045 RVA: 0x00089724 File Offset: 0x00087924
		internal void FlagUnsupportedStmtAttr(ODBC32.SQL_ATTR Attribute)
		{
			if (Attribute == ODBC32.SQL_ATTR.QUERY_TIMEOUT)
			{
				this.ProviderInfo.NoQueryTimeout = true;
				return;
			}
			if (Attribute == ODBC32.SQL_ATTR.SQL_COPT_SS_TXN_ISOLATION)
			{
				this.ProviderInfo.NoSqlSoptSSHiddenColumns = true;
				return;
			}
			if (Attribute != (ODBC32.SQL_ATTR)1228)
			{
				return;
			}
			this.ProviderInfo.NoSqlSoptSSNoBrowseTable = true;
		}

		// Token: 0x06001B86 RID: 7046 RVA: 0x00089760 File Offset: 0x00087960
		internal void FlagUnsupportedColAttr(ODBC32.SQL_DESC v3FieldId, ODBC32.SQL_COLUMN v2FieldId)
		{
			if (this.IsV3Driver && v3FieldId == (ODBC32.SQL_DESC)1212)
			{
				this.ProviderInfo.NoSqlCASSColumnKey = true;
			}
		}

		// Token: 0x06001B87 RID: 7047 RVA: 0x00089780 File Offset: 0x00087980
		internal bool SQLGetFunctions(ODBC32.SQL_API odbcFunction)
		{
			OdbcConnectionHandle connectionHandle = this.ConnectionHandle;
			if (connectionHandle != null)
			{
				short num;
				ODBC32.RetCode functions = connectionHandle.GetFunctions(odbcFunction, out num);
				if (functions != ODBC32.RetCode.SUCCESS)
				{
					this.HandleError(connectionHandle, functions);
				}
				return num != 0;
			}
			throw ODBC.ConnectionClosed();
		}

		// Token: 0x06001B88 RID: 7048 RVA: 0x000897BC File Offset: 0x000879BC
		internal bool TestTypeSupport(ODBC32.SQL_TYPE sqltype)
		{
			ODBC32.SQL_CONVERT sql_CONVERT;
			ODBC32.SQL_CVT sql_CVT;
			switch (sqltype)
			{
			case ODBC32.SQL_TYPE.WLONGVARCHAR:
				sql_CONVERT = ODBC32.SQL_CONVERT.LONGVARCHAR;
				sql_CVT = ODBC32.SQL_CVT.WLONGVARCHAR;
				break;
			case ODBC32.SQL_TYPE.WVARCHAR:
				sql_CONVERT = ODBC32.SQL_CONVERT.VARCHAR;
				sql_CVT = ODBC32.SQL_CVT.WVARCHAR;
				break;
			case ODBC32.SQL_TYPE.WCHAR:
				sql_CONVERT = ODBC32.SQL_CONVERT.CHAR;
				sql_CVT = ODBC32.SQL_CVT.WCHAR;
				break;
			default:
				if (sqltype != ODBC32.SQL_TYPE.NUMERIC)
				{
					return false;
				}
				sql_CONVERT = ODBC32.SQL_CONVERT.NUMERIC;
				sql_CVT = ODBC32.SQL_CVT.NUMERIC;
				break;
			}
			if ((this.ProviderInfo.TestedSQLTypes & (int)sql_CVT) == 0)
			{
				int num = this.GetInfoInt32Unhandled((ODBC32.SQL_INFO)sql_CONVERT);
				num &= (int)sql_CVT;
				this.ProviderInfo.TestedSQLTypes |= (int)sql_CVT;
				this.ProviderInfo.SupportedSQLTypes |= num;
			}
			return (this.ProviderInfo.SupportedSQLTypes & (int)sql_CVT) != 0;
		}

		// Token: 0x06001B89 RID: 7049 RVA: 0x00089860 File Offset: 0x00087A60
		internal bool TestRestrictedSqlBindType(ODBC32.SQL_TYPE sqltype)
		{
			ODBC32.SQL_CVT sql_CVT;
			if (sqltype != ODBC32.SQL_TYPE.NUMERIC)
			{
				if (sqltype != ODBC32.SQL_TYPE.DECIMAL)
				{
					return false;
				}
				sql_CVT = ODBC32.SQL_CVT.DECIMAL;
			}
			else
			{
				sql_CVT = ODBC32.SQL_CVT.NUMERIC;
			}
			return (this.ProviderInfo.RestrictedSQLBindTypes & (int)sql_CVT) != 0;
		}

		// Token: 0x06001B8A RID: 7050 RVA: 0x00089891 File Offset: 0x00087A91
		protected override DbTransaction BeginDbTransaction(IsolationLevel isolationLevel)
		{
			DbTransaction dbTransaction = this.InnerConnection.BeginTransaction(isolationLevel);
			GC.KeepAlive(this);
			return dbTransaction;
		}

		// Token: 0x06001B8B RID: 7051 RVA: 0x000898A8 File Offset: 0x00087AA8
		internal OdbcTransaction Open_BeginTransaction(IsolationLevel isolevel)
		{
			this.CheckState("BeginTransaction");
			this.RollbackDeadTransaction();
			if (this._weakTransaction != null && this._weakTransaction.IsAlive)
			{
				throw ADP.ParallelTransactionsNotSupported(this);
			}
			if (isolevel <= IsolationLevel.ReadUncommitted)
			{
				if (isolevel == IsolationLevel.Unspecified)
				{
					goto IL_0082;
				}
				if (isolevel == IsolationLevel.Chaos)
				{
					throw ODBC.NotSupportedIsolationLevel(isolevel);
				}
				if (isolevel == IsolationLevel.ReadUncommitted)
				{
					goto IL_0082;
				}
			}
			else if (isolevel <= IsolationLevel.RepeatableRead)
			{
				if (isolevel == IsolationLevel.ReadCommitted || isolevel == IsolationLevel.RepeatableRead)
				{
					goto IL_0082;
				}
			}
			else if (isolevel == IsolationLevel.Serializable || isolevel == IsolationLevel.Snapshot)
			{
				goto IL_0082;
			}
			throw ADP.InvalidIsolationLevel(isolevel);
			IL_0082:
			OdbcConnectionHandle connectionHandle = this.ConnectionHandle;
			ODBC32.RetCode retCode = connectionHandle.BeginTransaction(ref isolevel);
			if (retCode == ODBC32.RetCode.ERROR)
			{
				this.HandleError(connectionHandle, retCode);
			}
			OdbcTransaction odbcTransaction = new OdbcTransaction(this, isolevel, connectionHandle);
			this._weakTransaction = new WeakReference(odbcTransaction);
			return odbcTransaction;
		}

		// Token: 0x06001B8C RID: 7052 RVA: 0x0008996C File Offset: 0x00087B6C
		internal void Open_ChangeDatabase(string value)
		{
			this.CheckState("ChangeDatabase");
			if (value == null || value.Trim().Length == 0)
			{
				throw ADP.EmptyDatabaseName();
			}
			if (1024 < value.Length * 2 + 2)
			{
				throw ADP.DatabaseNameTooLong();
			}
			this.RollbackDeadTransaction();
			OdbcConnectionHandle connectionHandle = this.ConnectionHandle;
			ODBC32.RetCode retCode = connectionHandle.SetConnectionAttribute3(ODBC32.SQL_ATTR.CURRENT_CATALOG, value, checked(value.Length * 2));
			if (retCode != ODBC32.RetCode.SUCCESS)
			{
				this.HandleError(connectionHandle, retCode);
			}
		}

		// Token: 0x06001B8D RID: 7053 RVA: 0x000899DB File Offset: 0x00087BDB
		internal string Open_GetServerVersion()
		{
			return this.GetInfoStringUnhandled(ODBC32.SQL_INFO.DBMS_VER, true);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.Odbc.OdbcConnection" /> class.</summary>
		// Token: 0x06001B8E RID: 7054 RVA: 0x000899E6 File Offset: 0x00087BE6
		public OdbcConnection()
		{
			GC.SuppressFinalize(this);
			this._innerConnection = DbConnectionClosedNeverOpened.SingletonInstance;
		}

		// Token: 0x06001B8F RID: 7055 RVA: 0x00089A08 File Offset: 0x00087C08
		private void CopyFrom(OdbcConnection connection)
		{
			ADP.CheckArgumentNull(connection, "connection");
			this._userConnectionOptions = connection.UserConnectionOptions;
			this._poolGroup = connection.PoolGroup;
			if (DbConnectionClosedNeverOpened.SingletonInstance == connection._innerConnection)
			{
				this._innerConnection = DbConnectionClosedNeverOpened.SingletonInstance;
				return;
			}
			this._innerConnection = DbConnectionClosedPreviouslyOpened.SingletonInstance;
		}

		// Token: 0x17000505 RID: 1285
		// (get) Token: 0x06001B90 RID: 7056 RVA: 0x00089A5C File Offset: 0x00087C5C
		internal int CloseCount
		{
			get
			{
				return this._closeCount;
			}
		}

		// Token: 0x17000506 RID: 1286
		// (get) Token: 0x06001B91 RID: 7057 RVA: 0x00089A64 File Offset: 0x00087C64
		internal DbConnectionFactory ConnectionFactory
		{
			get
			{
				return OdbcConnection.s_connectionFactory;
			}
		}

		// Token: 0x17000507 RID: 1287
		// (get) Token: 0x06001B92 RID: 7058 RVA: 0x00089A6C File Offset: 0x00087C6C
		internal DbConnectionOptions ConnectionOptions
		{
			get
			{
				DbConnectionPoolGroup poolGroup = this.PoolGroup;
				if (poolGroup == null)
				{
					return null;
				}
				return poolGroup.ConnectionOptions;
			}
		}

		// Token: 0x06001B93 RID: 7059 RVA: 0x00089A8C File Offset: 0x00087C8C
		private string ConnectionString_Get()
		{
			bool shouldHidePassword = this.InnerConnection.ShouldHidePassword;
			DbConnectionOptions userConnectionOptions = this.UserConnectionOptions;
			if (userConnectionOptions == null)
			{
				return "";
			}
			return userConnectionOptions.UsersConnectionString(shouldHidePassword);
		}

		// Token: 0x06001B94 RID: 7060 RVA: 0x00089ABC File Offset: 0x00087CBC
		private void ConnectionString_Set(string value)
		{
			DbConnectionPoolKey dbConnectionPoolKey = new DbConnectionPoolKey(value);
			this.ConnectionString_Set(dbConnectionPoolKey);
		}

		// Token: 0x06001B95 RID: 7061 RVA: 0x00089AD8 File Offset: 0x00087CD8
		private void ConnectionString_Set(DbConnectionPoolKey key)
		{
			DbConnectionOptions dbConnectionOptions = null;
			DbConnectionPoolGroup connectionPoolGroup = this.ConnectionFactory.GetConnectionPoolGroup(key, null, ref dbConnectionOptions);
			DbConnectionInternal innerConnection = this.InnerConnection;
			bool flag = innerConnection.AllowSetConnectionString;
			if (flag)
			{
				flag = this.SetInnerConnectionFrom(DbConnectionClosedBusy.SingletonInstance, innerConnection);
				if (flag)
				{
					this._userConnectionOptions = dbConnectionOptions;
					this._poolGroup = connectionPoolGroup;
					this._innerConnection = DbConnectionClosedNeverOpened.SingletonInstance;
				}
			}
			if (!flag)
			{
				throw ADP.OpenConnectionPropertySet("ConnectionString", innerConnection.State);
			}
		}

		// Token: 0x17000508 RID: 1288
		// (get) Token: 0x06001B96 RID: 7062 RVA: 0x00089B45 File Offset: 0x00087D45
		internal DbConnectionInternal InnerConnection
		{
			get
			{
				return this._innerConnection;
			}
		}

		// Token: 0x17000509 RID: 1289
		// (get) Token: 0x06001B97 RID: 7063 RVA: 0x00089B4D File Offset: 0x00087D4D
		// (set) Token: 0x06001B98 RID: 7064 RVA: 0x00089B55 File Offset: 0x00087D55
		internal DbConnectionPoolGroup PoolGroup
		{
			get
			{
				return this._poolGroup;
			}
			set
			{
				this._poolGroup = value;
			}
		}

		// Token: 0x1700050A RID: 1290
		// (get) Token: 0x06001B99 RID: 7065 RVA: 0x00089B5E File Offset: 0x00087D5E
		internal DbConnectionOptions UserConnectionOptions
		{
			get
			{
				return this._userConnectionOptions;
			}
		}

		// Token: 0x06001B9A RID: 7066 RVA: 0x00089B68 File Offset: 0x00087D68
		internal void Abort(Exception e)
		{
			DbConnectionInternal innerConnection = this._innerConnection;
			if (ConnectionState.Open == innerConnection.State)
			{
				Interlocked.CompareExchange<DbConnectionInternal>(ref this._innerConnection, DbConnectionClosedPreviouslyOpened.SingletonInstance, innerConnection);
				innerConnection.DoomThisConnection();
			}
		}

		// Token: 0x06001B9B RID: 7067 RVA: 0x00089B9D File Offset: 0x00087D9D
		internal void AddWeakReference(object value, int tag)
		{
			this.InnerConnection.AddWeakReference(value, tag);
		}

		// Token: 0x06001B9C RID: 7068 RVA: 0x00089BAC File Offset: 0x00087DAC
		protected override DbCommand CreateDbCommand()
		{
			DbCommand dbCommand = this.ConnectionFactory.ProviderFactory.CreateCommand();
			dbCommand.Connection = this;
			return dbCommand;
		}

		// Token: 0x06001B9D RID: 7069 RVA: 0x00089BC5 File Offset: 0x00087DC5
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this._userConnectionOptions = null;
				this._poolGroup = null;
				this.Close();
			}
			this.DisposeMe(disposing);
			base.Dispose(disposing);
		}

		// Token: 0x06001B9E RID: 7070 RVA: 0x00089BEC File Offset: 0x00087DEC
		internal void NotifyWeakReference(int message)
		{
			this.InnerConnection.NotifyWeakReference(message);
		}

		// Token: 0x06001B9F RID: 7071 RVA: 0x00089BFC File Offset: 0x00087DFC
		internal void PermissionDemand()
		{
			DbConnectionPoolGroup poolGroup = this.PoolGroup;
			DbConnectionOptions dbConnectionOptions = ((poolGroup != null) ? poolGroup.ConnectionOptions : null);
			if (dbConnectionOptions == null || dbConnectionOptions.IsEmpty)
			{
				throw ADP.NoConnectionString();
			}
			DbConnectionOptions userConnectionOptions = this.UserConnectionOptions;
		}

		// Token: 0x06001BA0 RID: 7072 RVA: 0x00089C35 File Offset: 0x00087E35
		internal void RemoveWeakReference(object value)
		{
			this.InnerConnection.RemoveWeakReference(value);
		}

		// Token: 0x06001BA1 RID: 7073 RVA: 0x00089C44 File Offset: 0x00087E44
		internal void SetInnerConnectionEvent(DbConnectionInternal to)
		{
			ConnectionState connectionState = this._innerConnection.State & ConnectionState.Open;
			ConnectionState connectionState2 = to.State & ConnectionState.Open;
			if (connectionState != connectionState2 && connectionState2 == ConnectionState.Closed)
			{
				this._closeCount++;
			}
			this._innerConnection = to;
			if (connectionState == ConnectionState.Closed && ConnectionState.Open == connectionState2)
			{
				this.OnStateChange(DbConnectionInternal.StateChangeOpen);
				return;
			}
			if (ConnectionState.Open == connectionState && connectionState2 == ConnectionState.Closed)
			{
				this.OnStateChange(DbConnectionInternal.StateChangeClosed);
				return;
			}
			if (connectionState != connectionState2)
			{
				this.OnStateChange(new StateChangeEventArgs(connectionState, connectionState2));
			}
		}

		// Token: 0x06001BA2 RID: 7074 RVA: 0x00089CBB File Offset: 0x00087EBB
		internal bool SetInnerConnectionFrom(DbConnectionInternal to, DbConnectionInternal from)
		{
			return from == Interlocked.CompareExchange<DbConnectionInternal>(ref this._innerConnection, to, from);
		}

		// Token: 0x06001BA3 RID: 7075 RVA: 0x00089CCD File Offset: 0x00087ECD
		internal void SetInnerConnectionTo(DbConnectionInternal to)
		{
			this._innerConnection = to;
		}

		/// <summary>Enlists in the specified transaction as a distributed transaction.</summary>
		/// <param name="transaction">A reference to an existing <see cref="T:System.EnterpriseServices.ITransaction" /> in which to enlist.</param>
		// Token: 0x06001BA5 RID: 7077 RVA: 0x00010468 File Offset: 0x0000E668
		public void EnlistDistributedTransaction(ITransaction transaction)
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x040014DA RID: 5338
		private int _connectionTimeout = 15;

		// Token: 0x040014DB RID: 5339
		private OdbcInfoMessageEventHandler _infoMessageEventHandler;

		// Token: 0x040014DC RID: 5340
		private WeakReference _weakTransaction;

		// Token: 0x040014DD RID: 5341
		private OdbcConnectionHandle _connectionHandle;

		// Token: 0x040014DE RID: 5342
		private ConnectionState _extraState;

		// Token: 0x040014DF RID: 5343
		private static readonly DbConnectionFactory s_connectionFactory = OdbcConnectionFactory.SingletonInstance;

		// Token: 0x040014E0 RID: 5344
		private DbConnectionOptions _userConnectionOptions;

		// Token: 0x040014E1 RID: 5345
		private DbConnectionPoolGroup _poolGroup;

		// Token: 0x040014E2 RID: 5346
		private DbConnectionInternal _innerConnection;

		// Token: 0x040014E3 RID: 5347
		private int _closeCount;
	}
}
