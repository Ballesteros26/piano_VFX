using System;
using System.ComponentModel;
using System.Data.Common;
using System.Runtime.CompilerServices;
using System.Threading;

namespace System.Data.Odbc
{
	/// <summary>Represents an SQL statement or stored procedure to execute against a data source. This class cannot be inherited.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x0200028B RID: 651
	public sealed class OdbcCommand : DbCommand, ICloneable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.Odbc.OdbcCommand" /> class.</summary>
		// Token: 0x06001AF1 RID: 6897 RVA: 0x000878CD File Offset: 0x00085ACD
		public OdbcCommand()
		{
			GC.SuppressFinalize(this);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.Odbc.OdbcCommand" /> class with the text of the query.</summary>
		/// <param name="cmdText">The text of the query. </param>
		// Token: 0x06001AF2 RID: 6898 RVA: 0x000878FA File Offset: 0x00085AFA
		public OdbcCommand(string cmdText)
			: this()
		{
			this.CommandText = cmdText;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.Odbc.OdbcCommand" /> class with the text of the query and an <see cref="T:System.Data.Odbc.OdbcConnection" /> object.</summary>
		/// <param name="cmdText">The text of the query. </param>
		/// <param name="connection">An <see cref="T:System.Data.Odbc.OdbcConnection" /> object that represents the connection to a data source. </param>
		// Token: 0x06001AF3 RID: 6899 RVA: 0x00087909 File Offset: 0x00085B09
		public OdbcCommand(string cmdText, OdbcConnection connection)
			: this()
		{
			this.CommandText = cmdText;
			this.Connection = connection;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.Odbc.OdbcCommand" /> class with the text of the query, an <see cref="T:System.Data.Odbc.OdbcConnection" /> object, and the <see cref="P:System.Data.Odbc.OdbcCommand.Transaction" />.</summary>
		/// <param name="cmdText">The text of the query. </param>
		/// <param name="connection">An <see cref="T:System.Data.Odbc.OdbcConnection" /> object that represents the connection to a data source. </param>
		/// <param name="transaction">The transaction in which the <see cref="T:System.Data.Odbc.OdbcCommand" /> executes. </param>
		// Token: 0x06001AF4 RID: 6900 RVA: 0x0008791F File Offset: 0x00085B1F
		public OdbcCommand(string cmdText, OdbcConnection connection, OdbcTransaction transaction)
			: this()
		{
			this.CommandText = cmdText;
			this.Connection = connection;
			this.Transaction = transaction;
		}

		// Token: 0x06001AF5 RID: 6901 RVA: 0x0008793C File Offset: 0x00085B3C
		private void DisposeDeadDataReader()
		{
			if (ConnectionState.Fetching == this._cmdState && this._weakDataReaderReference != null && !this._weakDataReaderReference.IsAlive)
			{
				if (this._cmdWrapper != null)
				{
					this._cmdWrapper.FreeKeyInfoStatementHandle(ODBC32.STMT.CLOSE);
					this._cmdWrapper.FreeStatementHandle(ODBC32.STMT.CLOSE);
				}
				this.CloseFromDataReader();
			}
		}

		// Token: 0x06001AF6 RID: 6902 RVA: 0x00087990 File Offset: 0x00085B90
		private void DisposeDataReader()
		{
			if (this._weakDataReaderReference != null)
			{
				IDisposable disposable = (IDisposable)this._weakDataReaderReference.Target;
				if (disposable != null && this._weakDataReaderReference.IsAlive)
				{
					disposable.Dispose();
				}
				this.CloseFromDataReader();
			}
		}

		// Token: 0x06001AF7 RID: 6903 RVA: 0x000879D4 File Offset: 0x00085BD4
		internal void DisconnectFromDataReaderAndConnection()
		{
			OdbcDataReader odbcDataReader = null;
			if (this._weakDataReaderReference != null)
			{
				OdbcDataReader odbcDataReader2 = (OdbcDataReader)this._weakDataReaderReference.Target;
				if (this._weakDataReaderReference.IsAlive)
				{
					odbcDataReader = odbcDataReader2;
				}
			}
			if (odbcDataReader != null)
			{
				odbcDataReader.Command = null;
			}
			this._transaction = null;
			if (this._connection != null)
			{
				this._connection.RemoveWeakReference(this);
				this._connection = null;
			}
			if (odbcDataReader == null)
			{
				this.CloseCommandWrapper();
			}
			this._cmdWrapper = null;
		}

		// Token: 0x06001AF8 RID: 6904 RVA: 0x00087A47 File Offset: 0x00085C47
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.DisconnectFromDataReaderAndConnection();
				this._parameterCollection = null;
				this.CommandText = null;
			}
			this._cmdWrapper = null;
			this._isPrepared = false;
			base.Dispose(disposing);
		}

		// Token: 0x170004E5 RID: 1253
		// (get) Token: 0x06001AF9 RID: 6905 RVA: 0x00087A75 File Offset: 0x00085C75
		internal bool Canceling
		{
			get
			{
				return this._cmdWrapper.Canceling;
			}
		}

		/// <summary>Gets or sets the SQL statement or stored procedure to execute against the data source.</summary>
		/// <returns>The SQL statement or stored procedure to execute. The default value is an empty string ("").</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170004E6 RID: 1254
		// (get) Token: 0x06001AFA RID: 6906 RVA: 0x00087A84 File Offset: 0x00085C84
		// (set) Token: 0x06001AFB RID: 6907 RVA: 0x00087AA2 File Offset: 0x00085CA2
		public override string CommandText
		{
			get
			{
				string commandText = this._commandText;
				if (commandText == null)
				{
					return ADP.StrEmpty;
				}
				return commandText;
			}
			set
			{
				if (this._commandText != value)
				{
					this.PropertyChanging();
					this._commandText = value;
				}
			}
		}

		/// <summary>Gets or sets the wait time before terminating an attempt to execute a command and generating an error.</summary>
		/// <returns>The time in seconds to wait for the command to execute. The default is 30 seconds.</returns>
		// Token: 0x170004E7 RID: 1255
		// (get) Token: 0x06001AFC RID: 6908 RVA: 0x00087ABF File Offset: 0x00085CBF
		// (set) Token: 0x06001AFD RID: 6909 RVA: 0x00087AC7 File Offset: 0x00085CC7
		public override int CommandTimeout
		{
			get
			{
				return this._commandTimeout;
			}
			set
			{
				if (value < 0)
				{
					throw ADP.InvalidCommandTimeout(value, "CommandTimeout");
				}
				if (value != this._commandTimeout)
				{
					this.PropertyChanging();
					this._commandTimeout = value;
				}
			}
		}

		/// <summary>Resets the <see cref="P:System.Data.Odbc.OdbcCommand.CommandTimeout" /> property to the default value.</summary>
		// Token: 0x06001AFE RID: 6910 RVA: 0x00087AEF File Offset: 0x00085CEF
		public void ResetCommandTimeout()
		{
			if (30 != this._commandTimeout)
			{
				this.PropertyChanging();
				this._commandTimeout = 30;
			}
		}

		// Token: 0x06001AFF RID: 6911 RVA: 0x00087B09 File Offset: 0x00085D09
		private bool ShouldSerializeCommandTimeout()
		{
			return 30 != this._commandTimeout;
		}

		/// <summary>Gets or sets a value that indicates how the <see cref="P:System.Data.Odbc.OdbcCommand.CommandText" /> property is interpreted.</summary>
		/// <returns>One of the <see cref="T:System.Data.CommandType" /> values. The default is Text.</returns>
		/// <exception cref="T:System.ArgumentException">The value was not a valid <see cref="T:System.Data.CommandType" />. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x170004E8 RID: 1256
		// (get) Token: 0x06001B00 RID: 6912 RVA: 0x00087B18 File Offset: 0x00085D18
		// (set) Token: 0x06001B01 RID: 6913 RVA: 0x00087B32 File Offset: 0x00085D32
		[DefaultValue(CommandType.Text)]
		public override CommandType CommandType
		{
			get
			{
				CommandType commandType = this._commandType;
				if (commandType == (CommandType)0)
				{
					return CommandType.Text;
				}
				return commandType;
			}
			set
			{
				if (value == CommandType.Text || value == CommandType.StoredProcedure)
				{
					this.PropertyChanging();
					this._commandType = value;
					return;
				}
				if (value != CommandType.TableDirect)
				{
					throw ADP.InvalidCommandType(value);
				}
				throw ODBC.NotSupportedCommandType(value);
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Data.Odbc.OdbcConnection" /> used by this instance of the <see cref="T:System.Data.Odbc.OdbcCommand" />.</summary>
		/// <returns>The connection to a data source. The default is a null value.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Data.Odbc.OdbcCommand.Connection" /> property was changed while a transaction was in progress. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170004E9 RID: 1257
		// (get) Token: 0x06001B02 RID: 6914 RVA: 0x00087B61 File Offset: 0x00085D61
		// (set) Token: 0x06001B03 RID: 6915 RVA: 0x00087B69 File Offset: 0x00085D69
		public new OdbcConnection Connection
		{
			get
			{
				return this._connection;
			}
			set
			{
				if (value != this._connection)
				{
					this.PropertyChanging();
					this.DisconnectFromDataReaderAndConnection();
					this._connection = value;
				}
			}
		}

		// Token: 0x170004EA RID: 1258
		// (get) Token: 0x06001B04 RID: 6916 RVA: 0x00087B87 File Offset: 0x00085D87
		// (set) Token: 0x06001B05 RID: 6917 RVA: 0x00087B8F File Offset: 0x00085D8F
		protected override DbConnection DbConnection
		{
			get
			{
				return this.Connection;
			}
			set
			{
				this.Connection = (OdbcConnection)value;
			}
		}

		// Token: 0x170004EB RID: 1259
		// (get) Token: 0x06001B06 RID: 6918 RVA: 0x00087B9D File Offset: 0x00085D9D
		protected override DbParameterCollection DbParameterCollection
		{
			get
			{
				return this.Parameters;
			}
		}

		// Token: 0x170004EC RID: 1260
		// (get) Token: 0x06001B07 RID: 6919 RVA: 0x00087BA5 File Offset: 0x00085DA5
		// (set) Token: 0x06001B08 RID: 6920 RVA: 0x00087BAD File Offset: 0x00085DAD
		protected override DbTransaction DbTransaction
		{
			get
			{
				return this.Transaction;
			}
			set
			{
				this.Transaction = (OdbcTransaction)value;
			}
		}

		/// <summary>Gets or sets a value that indicates whether the command object should be visible in a customized interface control.</summary>
		/// <returns>true, if the command object should be visible in a control; otherwise false. The default is true.</returns>
		// Token: 0x170004ED RID: 1261
		// (get) Token: 0x06001B09 RID: 6921 RVA: 0x00087BBB File Offset: 0x00085DBB
		// (set) Token: 0x06001B0A RID: 6922 RVA: 0x00087BC6 File Offset: 0x00085DC6
		[Browsable(false)]
		[DefaultValue(true)]
		[DesignOnly(true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool DesignTimeVisible
		{
			get
			{
				return !this._designTimeInvisible;
			}
			set
			{
				this._designTimeInvisible = !value;
				TypeDescriptor.Refresh(this);
			}
		}

		// Token: 0x170004EE RID: 1262
		// (get) Token: 0x06001B0B RID: 6923 RVA: 0x00087BD8 File Offset: 0x00085DD8
		internal bool HasParameters
		{
			get
			{
				return this._parameterCollection != null;
			}
		}

		/// <summary>Gets the <see cref="T:System.Data.Odbc.OdbcParameterCollection" />.</summary>
		/// <returns>The parameters of the SQL statement or stored procedure. The default is an empty collection.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170004EF RID: 1263
		// (get) Token: 0x06001B0C RID: 6924 RVA: 0x00087BE3 File Offset: 0x00085DE3
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public new OdbcParameterCollection Parameters
		{
			get
			{
				if (this._parameterCollection == null)
				{
					this._parameterCollection = new OdbcParameterCollection();
				}
				return this._parameterCollection;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Data.Odbc.OdbcTransaction" /> within which the <see cref="T:System.Data.Odbc.OdbcCommand" /> executes.</summary>
		/// <returns>An <see cref="T:System.Data.Odbc.OdbcTransaction" />. The default is a null value.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170004F0 RID: 1264
		// (get) Token: 0x06001B0D RID: 6925 RVA: 0x00087BFE File Offset: 0x00085DFE
		// (set) Token: 0x06001B0E RID: 6926 RVA: 0x00087C22 File Offset: 0x00085E22
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new OdbcTransaction Transaction
		{
			get
			{
				if (this._transaction != null && this._transaction.Connection == null)
				{
					this._transaction = null;
				}
				return this._transaction;
			}
			set
			{
				if (this._transaction != value)
				{
					this.PropertyChanging();
					this._transaction = value;
				}
			}
		}

		/// <summary>Gets or sets a value that specifies how the Update method should apply command results to the DataRow.</summary>
		/// <returns>One of the <see cref="T:System.Data.UpdateRowSource" /> values.</returns>
		// Token: 0x170004F1 RID: 1265
		// (get) Token: 0x06001B0F RID: 6927 RVA: 0x00087C3A File Offset: 0x00085E3A
		// (set) Token: 0x06001B10 RID: 6928 RVA: 0x00087C42 File Offset: 0x00085E42
		[DefaultValue(UpdateRowSource.Both)]
		public override UpdateRowSource UpdatedRowSource
		{
			get
			{
				return this._updatedRowSource;
			}
			set
			{
				if (value <= UpdateRowSource.Both)
				{
					this._updatedRowSource = value;
					return;
				}
				throw ADP.InvalidUpdateRowSource(value);
			}
		}

		// Token: 0x06001B11 RID: 6929 RVA: 0x00087C56 File Offset: 0x00085E56
		internal OdbcDescriptorHandle GetDescriptorHandle(ODBC32.SQL_ATTR attribute)
		{
			return this._cmdWrapper.GetDescriptorHandle(attribute);
		}

		// Token: 0x06001B12 RID: 6930 RVA: 0x00087C64 File Offset: 0x00085E64
		internal CMDWrapper GetStatementHandle()
		{
			if (this._cmdWrapper == null)
			{
				this._cmdWrapper = new CMDWrapper(this._connection);
				this._connection.AddWeakReference(this, 1);
			}
			if (this._cmdWrapper._dataReaderBuf == null)
			{
				this._cmdWrapper._dataReaderBuf = new CNativeBuffer(4096);
			}
			if (this._cmdWrapper.StatementHandle == null)
			{
				this._isPrepared = false;
				this._cmdWrapper.CreateStatementHandle();
			}
			else if (this._parameterCollection != null && this._parameterCollection.RebindCollection)
			{
				this._cmdWrapper.FreeStatementHandle(ODBC32.STMT.RESET_PARAMS);
			}
			return this._cmdWrapper;
		}

		/// <summary>Tries to cancel the execution of an <see cref="T:System.Data.Odbc.OdbcCommand" />.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001B13 RID: 6931 RVA: 0x00087D04 File Offset: 0x00085F04
		public override void Cancel()
		{
			CMDWrapper cmdWrapper = this._cmdWrapper;
			if (cmdWrapper != null)
			{
				cmdWrapper.Canceling = true;
				OdbcStatementHandle statementHandle = cmdWrapper.StatementHandle;
				if (statementHandle != null)
				{
					OdbcStatementHandle odbcStatementHandle = statementHandle;
					lock (odbcStatementHandle)
					{
						ODBC32.RetCode retCode = statementHandle.Cancel();
						if (retCode > ODBC32.RetCode.SUCCESS_WITH_INFO)
						{
							throw cmdWrapper.Connection.HandleErrorNoThrow(statementHandle, retCode);
						}
					}
				}
			}
		}

		/// <summary>For a description of this member, see <see cref="M:System.ICloneable.Clone" />.</summary>
		/// <returns>A new <see cref="T;System.Object" /> that is a copy of this instance.</returns>
		// Token: 0x06001B14 RID: 6932 RVA: 0x00087D70 File Offset: 0x00085F70
		object ICloneable.Clone()
		{
			OdbcCommand odbcCommand = new OdbcCommand();
			odbcCommand.CommandText = this.CommandText;
			odbcCommand.CommandTimeout = this.CommandTimeout;
			odbcCommand.CommandType = this.CommandType;
			odbcCommand.Connection = this.Connection;
			odbcCommand.Transaction = this.Transaction;
			odbcCommand.UpdatedRowSource = this.UpdatedRowSource;
			if (this._parameterCollection != null && 0 < this.Parameters.Count)
			{
				OdbcParameterCollection parameters = odbcCommand.Parameters;
				foreach (object obj in this.Parameters)
				{
					ICloneable cloneable = (ICloneable)obj;
					parameters.Add(cloneable.Clone());
				}
			}
			return odbcCommand;
		}

		// Token: 0x06001B15 RID: 6933 RVA: 0x00087E40 File Offset: 0x00086040
		internal bool RecoverFromConnection()
		{
			this.DisposeDeadDataReader();
			return this._cmdState == ConnectionState.Closed;
		}

		// Token: 0x06001B16 RID: 6934 RVA: 0x00087E54 File Offset: 0x00086054
		private void CloseCommandWrapper()
		{
			CMDWrapper cmdWrapper = this._cmdWrapper;
			if (cmdWrapper != null)
			{
				try
				{
					cmdWrapper.Dispose();
					if (this._connection != null)
					{
						this._connection.RemoveWeakReference(this);
					}
				}
				finally
				{
					this._cmdWrapper = null;
				}
			}
		}

		// Token: 0x06001B17 RID: 6935 RVA: 0x00087EA0 File Offset: 0x000860A0
		internal void CloseFromConnection()
		{
			if (this._parameterCollection != null)
			{
				this._parameterCollection.RebindCollection = true;
			}
			this.DisposeDataReader();
			this.CloseCommandWrapper();
			this._isPrepared = false;
			this._transaction = null;
		}

		// Token: 0x06001B18 RID: 6936 RVA: 0x00087ED0 File Offset: 0x000860D0
		internal void CloseFromDataReader()
		{
			this._weakDataReaderReference = null;
			this._cmdState = ConnectionState.Closed;
		}

		/// <summary>Creates a new instance of an <see cref="T:System.Data.Odbc.OdbcParameter" /> object.</summary>
		/// <returns>An <see cref="T:System.Data.Odbc.OdbcParameter" /> object.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001B19 RID: 6937 RVA: 0x00087EE0 File Offset: 0x000860E0
		public new OdbcParameter CreateParameter()
		{
			return new OdbcParameter();
		}

		// Token: 0x06001B1A RID: 6938 RVA: 0x00087EE7 File Offset: 0x000860E7
		protected override DbParameter CreateDbParameter()
		{
			return this.CreateParameter();
		}

		// Token: 0x06001B1B RID: 6939 RVA: 0x00087EEF File Offset: 0x000860EF
		protected override DbDataReader ExecuteDbDataReader(CommandBehavior behavior)
		{
			return this.ExecuteReader(behavior);
		}

		/// <summary>Executes an SQL statement against the <see cref="P:System.Data.Odbc.OdbcCommand.Connection" /> and returns the number of rows affected.</summary>
		/// <returns>For UPDATE, INSERT, and DELETE statements, the return value is the number of rows affected by the command. For all other types of statements, the return value is -1.</returns>
		/// <exception cref="T:System.InvalidOperationException">The connection does not exist.-or- The connection is not open. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001B1C RID: 6940 RVA: 0x00087EF8 File Offset: 0x000860F8
		public override int ExecuteNonQuery()
		{
			int recordsAffected;
			using (OdbcDataReader odbcDataReader = this.ExecuteReaderObject(CommandBehavior.Default, "ExecuteNonQuery", false))
			{
				odbcDataReader.Close();
				recordsAffected = odbcDataReader.RecordsAffected;
			}
			return recordsAffected;
		}

		/// <summary>Sends the <see cref="P:System.Data.Odbc.OdbcCommand.CommandText" /> to the <see cref="P:System.Data.Odbc.OdbcCommand.Connection" /> and builds an <see cref="T:System.Data.Odbc.OdbcDataReader" />.</summary>
		/// <returns>An <see cref="T:System.Data.Odbc.OdbcDataReader" /> object.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001B1D RID: 6941 RVA: 0x00087F40 File Offset: 0x00086140
		public new OdbcDataReader ExecuteReader()
		{
			return this.ExecuteReader(CommandBehavior.Default);
		}

		/// <summary>Sends the <see cref="P:System.Data.Odbc.OdbcCommand.CommandText" /> to the <see cref="P:System.Data.Odbc.OdbcCommand.Connection" />, and builds an <see cref="T:System.Data.Odbc.OdbcDataReader" /> using one of the CommandBehavior values.</summary>
		/// <returns>An <see cref="T:System.Data.Odbc.OdbcDataReader" /> object.</returns>
		/// <param name="behavior">One of the System.Data.CommandBehavior values. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001B1E RID: 6942 RVA: 0x00087F49 File Offset: 0x00086149
		public new OdbcDataReader ExecuteReader(CommandBehavior behavior)
		{
			return this.ExecuteReaderObject(behavior, "ExecuteReader", true);
		}

		// Token: 0x06001B1F RID: 6943 RVA: 0x00087F58 File Offset: 0x00086158
		internal OdbcDataReader ExecuteReaderFromSQLMethod(object[] methodArguments, ODBC32.SQL_API method)
		{
			return this.ExecuteReaderObject(CommandBehavior.Default, method.ToString(), true, methodArguments, method);
		}

		// Token: 0x06001B20 RID: 6944 RVA: 0x00087F71 File Offset: 0x00086171
		private OdbcDataReader ExecuteReaderObject(CommandBehavior behavior, string method, bool needReader)
		{
			if (this.CommandText == null || this.CommandText.Length == 0)
			{
				throw ADP.CommandTextRequired(method);
			}
			return this.ExecuteReaderObject(behavior, method, needReader, null, ODBC32.SQL_API.SQLEXECDIRECT);
		}

		// Token: 0x06001B21 RID: 6945 RVA: 0x00087F9C File Offset: 0x0008619C
		private OdbcDataReader ExecuteReaderObject(CommandBehavior behavior, string method, bool needReader, object[] methodArguments, ODBC32.SQL_API odbcApiMethod)
		{
			OdbcDataReader odbcDataReader = null;
			try
			{
				this.DisposeDeadDataReader();
				this.ValidateConnectionAndTransaction(method);
				if ((CommandBehavior.SingleRow & behavior) != CommandBehavior.Default)
				{
					behavior |= CommandBehavior.SingleResult;
				}
				OdbcStatementHandle statementHandle = this.GetStatementHandle().StatementHandle;
				this._cmdWrapper.Canceling = false;
				if (this._weakDataReaderReference != null && this._weakDataReaderReference.IsAlive)
				{
					object target = this._weakDataReaderReference.Target;
					if (target != null && this._weakDataReaderReference.IsAlive && !((OdbcDataReader)target).IsClosed)
					{
						throw ADP.OpenReaderExists();
					}
				}
				odbcDataReader = new OdbcDataReader(this, this._cmdWrapper, behavior);
				if (!this.Connection.ProviderInfo.NoQueryTimeout)
				{
					this.TrySetStatementAttribute(statementHandle, ODBC32.SQL_ATTR.QUERY_TIMEOUT, (IntPtr)this.CommandTimeout);
				}
				if (needReader && this.Connection.IsV3Driver && !this.Connection.ProviderInfo.NoSqlSoptSSNoBrowseTable && !this.Connection.ProviderInfo.NoSqlSoptSSHiddenColumns)
				{
					if (odbcDataReader.IsBehavior(CommandBehavior.KeyInfo))
					{
						if (!this._cmdWrapper._ssKeyInfoModeOn)
						{
							this.TrySetStatementAttribute(statementHandle, (ODBC32.SQL_ATTR)1228, (IntPtr)1);
							this.TrySetStatementAttribute(statementHandle, ODBC32.SQL_ATTR.SQL_COPT_SS_TXN_ISOLATION, (IntPtr)1);
							this._cmdWrapper._ssKeyInfoModeOff = false;
							this._cmdWrapper._ssKeyInfoModeOn = true;
						}
					}
					else if (!this._cmdWrapper._ssKeyInfoModeOff)
					{
						this.TrySetStatementAttribute(statementHandle, (ODBC32.SQL_ATTR)1228, (IntPtr)0);
						this.TrySetStatementAttribute(statementHandle, ODBC32.SQL_ATTR.SQL_COPT_SS_TXN_ISOLATION, (IntPtr)0);
						this._cmdWrapper._ssKeyInfoModeOff = true;
						this._cmdWrapper._ssKeyInfoModeOn = false;
					}
				}
				if (odbcDataReader.IsBehavior(CommandBehavior.KeyInfo) || odbcDataReader.IsBehavior(CommandBehavior.SchemaOnly))
				{
					ODBC32.RetCode retCode = statementHandle.Prepare(this.CommandText);
					if (retCode != ODBC32.RetCode.SUCCESS)
					{
						this._connection.HandleError(statementHandle, retCode);
					}
				}
				bool flag = false;
				CNativeBuffer cnativeBuffer = this._cmdWrapper._nativeParameterBuffer;
				RuntimeHelpers.PrepareConstrainedRegions();
				try
				{
					if (this._parameterCollection != null && 0 < this._parameterCollection.Count)
					{
						int num = this._parameterCollection.CalcParameterBufferSize(this);
						if (cnativeBuffer == null || cnativeBuffer.Length < num)
						{
							if (cnativeBuffer != null)
							{
								cnativeBuffer.Dispose();
							}
							cnativeBuffer = new CNativeBuffer(num);
							this._cmdWrapper._nativeParameterBuffer = cnativeBuffer;
						}
						else
						{
							cnativeBuffer.ZeroMemory();
						}
						cnativeBuffer.DangerousAddRef(ref flag);
						this._parameterCollection.Bind(this, this._cmdWrapper, cnativeBuffer);
					}
					if (!odbcDataReader.IsBehavior(CommandBehavior.SchemaOnly))
					{
						ODBC32.RetCode retCode;
						if ((odbcDataReader.IsBehavior(CommandBehavior.KeyInfo) || odbcDataReader.IsBehavior(CommandBehavior.SchemaOnly)) && this.CommandType != CommandType.StoredProcedure)
						{
							short num2;
							retCode = statementHandle.NumberOfResultColumns(out num2);
							if (retCode == ODBC32.RetCode.SUCCESS || retCode == ODBC32.RetCode.SUCCESS_WITH_INFO)
							{
								if (num2 > 0)
								{
									odbcDataReader.GetSchemaTable();
								}
							}
							else if (retCode != ODBC32.RetCode.NO_DATA)
							{
								this._connection.HandleError(statementHandle, retCode);
							}
						}
						if (odbcApiMethod <= ODBC32.SQL_API.SQLGETTYPEINFO)
						{
							if (odbcApiMethod != ODBC32.SQL_API.SQLEXECDIRECT)
							{
								if (odbcApiMethod == ODBC32.SQL_API.SQLCOLUMNS)
								{
									retCode = statementHandle.Columns((string)methodArguments[0], (string)methodArguments[1], (string)methodArguments[2], (string)methodArguments[3]);
									goto IL_042A;
								}
								if (odbcApiMethod == ODBC32.SQL_API.SQLGETTYPEINFO)
								{
									retCode = statementHandle.GetTypeInfo((short)methodArguments[0]);
									goto IL_042A;
								}
							}
							else
							{
								if (odbcDataReader.IsBehavior(CommandBehavior.KeyInfo) || this._isPrepared)
								{
									retCode = statementHandle.Execute();
									goto IL_042A;
								}
								retCode = statementHandle.ExecuteDirect(this.CommandText);
								goto IL_042A;
							}
						}
						else if (odbcApiMethod <= ODBC32.SQL_API.SQLTABLES)
						{
							if (odbcApiMethod == ODBC32.SQL_API.SQLSTATISTICS)
							{
								retCode = statementHandle.Statistics((string)methodArguments[0], (string)methodArguments[1], (string)methodArguments[2], (short)methodArguments[3], (short)methodArguments[4]);
								goto IL_042A;
							}
							if (odbcApiMethod == ODBC32.SQL_API.SQLTABLES)
							{
								retCode = statementHandle.Tables((string)methodArguments[0], (string)methodArguments[1], (string)methodArguments[2], (string)methodArguments[3]);
								goto IL_042A;
							}
						}
						else
						{
							if (odbcApiMethod == ODBC32.SQL_API.SQLPROCEDURECOLUMNS)
							{
								retCode = statementHandle.ProcedureColumns((string)methodArguments[0], (string)methodArguments[1], (string)methodArguments[2], (string)methodArguments[3]);
								goto IL_042A;
							}
							if (odbcApiMethod == ODBC32.SQL_API.SQLPROCEDURES)
							{
								retCode = statementHandle.Procedures((string)methodArguments[0], (string)methodArguments[1], (string)methodArguments[2]);
								goto IL_042A;
							}
						}
						throw ADP.InvalidOperation(method.ToString());
						IL_042A:
						if (retCode != ODBC32.RetCode.SUCCESS && ODBC32.RetCode.NO_DATA != retCode)
						{
							this._connection.HandleError(statementHandle, retCode);
						}
					}
				}
				finally
				{
					if (flag)
					{
						cnativeBuffer.DangerousRelease();
					}
				}
				this._weakDataReaderReference = new WeakReference(odbcDataReader);
				if (!odbcDataReader.IsBehavior(CommandBehavior.SchemaOnly))
				{
					odbcDataReader.FirstResult();
				}
				this._cmdState = ConnectionState.Fetching;
			}
			finally
			{
				if (ConnectionState.Fetching != this._cmdState)
				{
					if (odbcDataReader != null)
					{
						if (this._parameterCollection != null)
						{
							this._parameterCollection.ClearBindings();
						}
						((IDisposable)odbcDataReader).Dispose();
					}
					if (this._cmdState != ConnectionState.Closed)
					{
						this._cmdState = ConnectionState.Closed;
					}
				}
			}
			return odbcDataReader;
		}

		/// <summary>Executes the query, and returns the first column of the first row in the result set returned by the query. Additional columns or rows are ignored.</summary>
		/// <returns>The first column of the first row in the result set, or a null reference if the result set is empty.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001B22 RID: 6946 RVA: 0x00088484 File Offset: 0x00086684
		public override object ExecuteScalar()
		{
			object obj = null;
			using (IDataReader dataReader = this.ExecuteReaderObject(CommandBehavior.Default, "ExecuteScalar", false))
			{
				if (dataReader.Read() && 0 < dataReader.FieldCount)
				{
					obj = dataReader.GetValue(0);
				}
				dataReader.Close();
			}
			return obj;
		}

		// Token: 0x06001B23 RID: 6947 RVA: 0x000884E0 File Offset: 0x000866E0
		internal string GetDiagSqlState()
		{
			return this._cmdWrapper.GetDiagSqlState();
		}

		// Token: 0x06001B24 RID: 6948 RVA: 0x000884ED File Offset: 0x000866ED
		private void PropertyChanging()
		{
			this._isPrepared = false;
		}

		/// <summary>Creates a prepared or compiled version of the command at the data source.</summary>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Data.Odbc.OdbcCommand.Connection" /> is not set.-or- The <see cref="P:System.Data.Odbc.OdbcCommand.Connection" /> is not <see cref="!:System.Data.Odbc.OdbcConnection.Open" />. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001B25 RID: 6949 RVA: 0x000884F8 File Offset: 0x000866F8
		public override void Prepare()
		{
			this.ValidateOpenConnection("Prepare");
			if ((ConnectionState.Fetching & this._connection.InternalState) != ConnectionState.Closed)
			{
				throw ADP.OpenReaderExists();
			}
			if (this.CommandType == CommandType.TableDirect)
			{
				return;
			}
			this.DisposeDeadDataReader();
			this.GetStatementHandle();
			OdbcStatementHandle statementHandle = this._cmdWrapper.StatementHandle;
			ODBC32.RetCode retCode = statementHandle.Prepare(this.CommandText);
			if (retCode != ODBC32.RetCode.SUCCESS)
			{
				this._connection.HandleError(statementHandle, retCode);
			}
			this._isPrepared = true;
		}

		// Token: 0x06001B26 RID: 6950 RVA: 0x00088570 File Offset: 0x00086770
		private void TrySetStatementAttribute(OdbcStatementHandle stmt, ODBC32.SQL_ATTR stmtAttribute, IntPtr value)
		{
			if (stmt.SetStatementAttribute(stmtAttribute, value, ODBC32.SQL_IS.UINTEGER) == ODBC32.RetCode.ERROR)
			{
				string text;
				stmt.GetDiagnosticField(out text);
				if (text == "HYC00" || text == "HY092")
				{
					this.Connection.FlagUnsupportedStmtAttr(stmtAttribute);
				}
			}
		}

		// Token: 0x06001B27 RID: 6951 RVA: 0x000885BC File Offset: 0x000867BC
		private void ValidateOpenConnection(string methodName)
		{
			OdbcConnection connection = this.Connection;
			if (connection == null)
			{
				throw ADP.ConnectionRequired(methodName);
			}
			ConnectionState state = connection.State;
			if (ConnectionState.Open != state)
			{
				throw ADP.OpenConnectionRequired(methodName, state);
			}
		}

		// Token: 0x06001B28 RID: 6952 RVA: 0x000885ED File Offset: 0x000867ED
		private void ValidateConnectionAndTransaction(string method)
		{
			if (this._connection == null)
			{
				throw ADP.ConnectionRequired(method);
			}
			this._transaction = this._connection.SetStateExecuting(method, this.Transaction);
			this._cmdState = ConnectionState.Executing;
		}

		// Token: 0x040014C2 RID: 5314
		private static int s_objectTypeCount;

		// Token: 0x040014C3 RID: 5315
		internal readonly int ObjectID = Interlocked.Increment(ref OdbcCommand.s_objectTypeCount);

		// Token: 0x040014C4 RID: 5316
		private string _commandText;

		// Token: 0x040014C5 RID: 5317
		private CommandType _commandType;

		// Token: 0x040014C6 RID: 5318
		private int _commandTimeout = 30;

		// Token: 0x040014C7 RID: 5319
		private UpdateRowSource _updatedRowSource = UpdateRowSource.Both;

		// Token: 0x040014C8 RID: 5320
		private bool _designTimeInvisible;

		// Token: 0x040014C9 RID: 5321
		private bool _isPrepared;

		// Token: 0x040014CA RID: 5322
		private OdbcConnection _connection;

		// Token: 0x040014CB RID: 5323
		private OdbcTransaction _transaction;

		// Token: 0x040014CC RID: 5324
		private WeakReference _weakDataReaderReference;

		// Token: 0x040014CD RID: 5325
		private CMDWrapper _cmdWrapper;

		// Token: 0x040014CE RID: 5326
		private OdbcParameterCollection _parameterCollection;

		// Token: 0x040014CF RID: 5327
		private ConnectionState _cmdState;
	}
}
