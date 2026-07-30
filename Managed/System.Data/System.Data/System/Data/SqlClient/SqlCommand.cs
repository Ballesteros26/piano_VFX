using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Sql;
using System.Data.SqlTypes;
using System.Runtime.CompilerServices;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using Microsoft.SqlServer.Server;
using Unity;

namespace System.Data.SqlClient
{
	/// <summary>Represents a Transact-SQL statement or stored procedure to execute against a SQL Server database. This class cannot be inherited.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x0200016B RID: 363
	public sealed class SqlCommand : DbCommand, ICloneable, IDbCommand, IDisposable
	{
		// Token: 0x17000304 RID: 772
		// (get) Token: 0x0600113B RID: 4411 RVA: 0x00056C0B File Offset: 0x00054E0B
		internal bool InPrepare
		{
			get
			{
				return this._inPrepare;
			}
		}

		// Token: 0x17000305 RID: 773
		// (get) Token: 0x0600113C RID: 4412 RVA: 0x00056C13 File Offset: 0x00054E13
		private SqlCommand.CachedAsyncState cachedAsyncState
		{
			get
			{
				if (this._cachedAsyncState == null)
				{
					this._cachedAsyncState = new SqlCommand.CachedAsyncState();
				}
				return this._cachedAsyncState;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlClient.SqlCommand" /> class.</summary>
		// Token: 0x0600113D RID: 4413 RVA: 0x00056C2E File Offset: 0x00054E2E
		public SqlCommand()
		{
			this._commandTimeout = 30;
			this._updatedRowSource = UpdateRowSource.Both;
			this._prepareHandle = -1;
			this._preparedConnectionCloseCount = -1;
			this._preparedConnectionReconnectCount = -1;
			this._rowsAffected = -1;
			base..ctor();
			GC.SuppressFinalize(this);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlClient.SqlCommand" /> class with the text of the query.</summary>
		/// <param name="cmdText">The text of the query. </param>
		// Token: 0x0600113E RID: 4414 RVA: 0x00056C67 File Offset: 0x00054E67
		public SqlCommand(string cmdText)
			: this()
		{
			this.CommandText = cmdText;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlClient.SqlCommand" /> class with the text of the query and a <see cref="T:System.Data.SqlClient.SqlConnection" />.</summary>
		/// <param name="cmdText">The text of the query. </param>
		/// <param name="connection">A <see cref="T:System.Data.SqlClient.SqlConnection" /> that represents the connection to an instance of SQL Server. </param>
		// Token: 0x0600113F RID: 4415 RVA: 0x00056C76 File Offset: 0x00054E76
		public SqlCommand(string cmdText, SqlConnection connection)
			: this()
		{
			this.CommandText = cmdText;
			this.Connection = connection;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlClient.SqlCommand" /> class with the text of the query, a <see cref="T:System.Data.SqlClient.SqlConnection" />, and the <see cref="T:System.Data.SqlClient.SqlTransaction" />.</summary>
		/// <param name="cmdText">The text of the query. </param>
		/// <param name="connection">A <see cref="T:System.Data.SqlClient.SqlConnection" /> that represents the connection to an instance of SQL Server. </param>
		/// <param name="transaction">The <see cref="T:System.Data.SqlClient.SqlTransaction" /> in which the <see cref="T:System.Data.SqlClient.SqlCommand" /> executes. </param>
		// Token: 0x06001140 RID: 4416 RVA: 0x00056C8C File Offset: 0x00054E8C
		public SqlCommand(string cmdText, SqlConnection connection, SqlTransaction transaction)
			: this()
		{
			this.CommandText = cmdText;
			this.Connection = connection;
			this.Transaction = transaction;
		}

		// Token: 0x06001141 RID: 4417 RVA: 0x00056CAC File Offset: 0x00054EAC
		private SqlCommand(SqlCommand from)
			: this()
		{
			this.CommandText = from.CommandText;
			this.CommandTimeout = from.CommandTimeout;
			this.CommandType = from.CommandType;
			this.Connection = from.Connection;
			this.DesignTimeVisible = from.DesignTimeVisible;
			this.Transaction = from.Transaction;
			this.UpdatedRowSource = from.UpdatedRowSource;
			SqlParameterCollection parameters = this.Parameters;
			foreach (object obj in from.Parameters)
			{
				parameters.Add((obj is ICloneable) ? (obj as ICloneable).Clone() : obj);
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Data.SqlClient.SqlConnection" /> used by this instance of the <see cref="T:System.Data.SqlClient.SqlCommand" />.</summary>
		/// <returns>The connection to a data source. The default value is null.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Data.SqlClient.SqlCommand.Connection" /> property was changed while the command was enlisted in a transaction.. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000306 RID: 774
		// (get) Token: 0x06001142 RID: 4418 RVA: 0x00056D78 File Offset: 0x00054F78
		// (set) Token: 0x06001143 RID: 4419 RVA: 0x00056D80 File Offset: 0x00054F80
		public new SqlConnection Connection
		{
			get
			{
				return this._activeConnection;
			}
			set
			{
				if (this._activeConnection != value && this._activeConnection != null && this.cachedAsyncState.PendingAsyncOperation)
				{
					throw SQL.CannotModifyPropertyAsyncOperationInProgress("Connection");
				}
				if (this._transaction != null && this._transaction.Connection == null)
				{
					this._transaction = null;
				}
				if (this.IsPrepared && this._activeConnection != value && this._activeConnection != null)
				{
					try
					{
						this.Unprepare();
					}
					catch (Exception)
					{
					}
					finally
					{
						this._prepareHandle = -1;
						this._execType = SqlCommand.EXECTYPE.UNPREPARED;
					}
				}
				this._activeConnection = value;
			}
		}

		// Token: 0x17000307 RID: 775
		// (get) Token: 0x06001144 RID: 4420 RVA: 0x00056E28 File Offset: 0x00055028
		// (set) Token: 0x06001145 RID: 4421 RVA: 0x00056E30 File Offset: 0x00055030
		protected override DbConnection DbConnection
		{
			get
			{
				return this.Connection;
			}
			set
			{
				this.Connection = (SqlConnection)value;
			}
		}

		// Token: 0x17000308 RID: 776
		// (get) Token: 0x06001146 RID: 4422 RVA: 0x00056E3E File Offset: 0x0005503E
		private SqlInternalConnectionTds InternalTdsConnection
		{
			get
			{
				return (SqlInternalConnectionTds)this._activeConnection.InnerConnection;
			}
		}

		/// <summary>Gets or sets a value that specifies the <see cref="T:System.Data.Sql.SqlNotificationRequest" /> object bound to this command.</summary>
		/// <returns>When set to null (default), no notification should be requested.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000309 RID: 777
		// (get) Token: 0x06001147 RID: 4423 RVA: 0x00056E50 File Offset: 0x00055050
		// (set) Token: 0x06001148 RID: 4424 RVA: 0x00056E58 File Offset: 0x00055058
		public SqlNotificationRequest Notification
		{
			get
			{
				return this._notification;
			}
			set
			{
				this._sqlDep = null;
				this._notification = value;
			}
		}

		// Token: 0x1700030A RID: 778
		// (get) Token: 0x06001149 RID: 4425 RVA: 0x00056E68 File Offset: 0x00055068
		internal SqlStatistics Statistics
		{
			get
			{
				if (this._activeConnection != null && (this._activeConnection.StatisticsEnabled || SqlCommand._diagnosticListener.IsEnabled("System.Data.SqlClient.WriteCommandAfter")))
				{
					return this._activeConnection.Statistics;
				}
				return null;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Data.SqlClient.SqlTransaction" /> within which the <see cref="T:System.Data.SqlClient.SqlCommand" /> executes.</summary>
		/// <returns>The <see cref="T:System.Data.SqlClient.SqlTransaction" />. The default value is null.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x1700030B RID: 779
		// (get) Token: 0x0600114A RID: 4426 RVA: 0x00056E9D File Offset: 0x0005509D
		// (set) Token: 0x0600114B RID: 4427 RVA: 0x00056EC1 File Offset: 0x000550C1
		public new SqlTransaction Transaction
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
				if (this._transaction != value && this._activeConnection != null && this.cachedAsyncState.PendingAsyncOperation)
				{
					throw SQL.CannotModifyPropertyAsyncOperationInProgress("Transaction");
				}
				this._transaction = value;
			}
		}

		// Token: 0x1700030C RID: 780
		// (get) Token: 0x0600114C RID: 4428 RVA: 0x00056EF3 File Offset: 0x000550F3
		// (set) Token: 0x0600114D RID: 4429 RVA: 0x00056EFB File Offset: 0x000550FB
		protected override DbTransaction DbTransaction
		{
			get
			{
				return this.Transaction;
			}
			set
			{
				this.Transaction = (SqlTransaction)value;
			}
		}

		/// <summary>Gets or sets the Transact-SQL statement, table name or stored procedure to execute at the data source.</summary>
		/// <returns>The Transact-SQL statement or stored procedure to execute. The default is an empty string.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700030D RID: 781
		// (get) Token: 0x0600114E RID: 4430 RVA: 0x00056F0C File Offset: 0x0005510C
		// (set) Token: 0x0600114F RID: 4431 RVA: 0x00056F2A File Offset: 0x0005512A
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

		/// <summary>Gets or sets the wait time before terminating the attempt to execute a command and generating an error.</summary>
		/// <returns>The time in seconds to wait for the command to execute. The default is 30 seconds.</returns>
		// Token: 0x1700030E RID: 782
		// (get) Token: 0x06001150 RID: 4432 RVA: 0x00056F47 File Offset: 0x00055147
		// (set) Token: 0x06001151 RID: 4433 RVA: 0x00056F4F File Offset: 0x0005514F
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

		/// <summary>Resets the <see cref="P:System.Data.SqlClient.SqlCommand.CommandTimeout" /> property to its default value.</summary>
		// Token: 0x06001152 RID: 4434 RVA: 0x00056F77 File Offset: 0x00055177
		public void ResetCommandTimeout()
		{
			if (30 != this._commandTimeout)
			{
				this.PropertyChanging();
				this._commandTimeout = 30;
			}
		}

		/// <summary>Gets or sets a value indicating how the <see cref="P:System.Data.SqlClient.SqlCommand.CommandText" /> property is to be interpreted.</summary>
		/// <returns>One of the <see cref="T:System.Data.CommandType" /> values. The default is Text.</returns>
		/// <exception cref="T:System.ArgumentException">The value was not a valid <see cref="T:System.Data.CommandType" />. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x1700030F RID: 783
		// (get) Token: 0x06001153 RID: 4435 RVA: 0x00056F94 File Offset: 0x00055194
		// (set) Token: 0x06001154 RID: 4436 RVA: 0x00056FAE File Offset: 0x000551AE
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
				if (this._commandType == value)
				{
					return;
				}
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
				throw SQL.NotSupportedCommandType(value);
			}
		}

		/// <summary>Gets or sets a value indicating whether the command object should be visible in a Windows Form Designer control.</summary>
		/// <returns>A value indicating whether the command object should be visible in a control. The default is true.</returns>
		// Token: 0x17000310 RID: 784
		// (get) Token: 0x06001155 RID: 4437 RVA: 0x00056FE7 File Offset: 0x000551E7
		// (set) Token: 0x06001156 RID: 4438 RVA: 0x00056FF2 File Offset: 0x000551F2
		public override bool DesignTimeVisible
		{
			get
			{
				return !this._designTimeInvisible;
			}
			set
			{
				this._designTimeInvisible = !value;
			}
		}

		/// <summary>Gets the <see cref="T:System.Data.SqlClient.SqlParameterCollection" />.</summary>
		/// <returns>The parameters of the Transact-SQL statement or stored procedure. The default is an empty collection.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000311 RID: 785
		// (get) Token: 0x06001157 RID: 4439 RVA: 0x00056FFE File Offset: 0x000551FE
		public new SqlParameterCollection Parameters
		{
			get
			{
				if (this._parameters == null)
				{
					this._parameters = new SqlParameterCollection();
				}
				return this._parameters;
			}
		}

		// Token: 0x17000312 RID: 786
		// (get) Token: 0x06001158 RID: 4440 RVA: 0x00057019 File Offset: 0x00055219
		protected override DbParameterCollection DbParameterCollection
		{
			get
			{
				return this.Parameters;
			}
		}

		/// <summary>Gets or sets how command results are applied to the <see cref="T:System.Data.DataRow" /> when used by the Update method of the <see cref="T:System.Data.Common.DbDataAdapter" />.</summary>
		/// <returns>One of the <see cref="T:System.Data.UpdateRowSource" /> values.</returns>
		// Token: 0x17000313 RID: 787
		// (get) Token: 0x06001159 RID: 4441 RVA: 0x00057021 File Offset: 0x00055221
		// (set) Token: 0x0600115A RID: 4442 RVA: 0x00057029 File Offset: 0x00055229
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

		/// <summary>Occurs when the execution of a Transact-SQL statement completes.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x14000024 RID: 36
		// (add) Token: 0x0600115B RID: 4443 RVA: 0x0005703D File Offset: 0x0005523D
		// (remove) Token: 0x0600115C RID: 4444 RVA: 0x00057056 File Offset: 0x00055256
		public event StatementCompletedEventHandler StatementCompleted
		{
			add
			{
				this._statementCompletedEventHandler = (StatementCompletedEventHandler)Delegate.Combine(this._statementCompletedEventHandler, value);
			}
			remove
			{
				this._statementCompletedEventHandler = (StatementCompletedEventHandler)Delegate.Remove(this._statementCompletedEventHandler, value);
			}
		}

		// Token: 0x0600115D RID: 4445 RVA: 0x00057070 File Offset: 0x00055270
		internal void OnStatementCompleted(int recordCount)
		{
			if (0 <= recordCount)
			{
				StatementCompletedEventHandler statementCompletedEventHandler = this._statementCompletedEventHandler;
				if (statementCompletedEventHandler != null)
				{
					try
					{
						statementCompletedEventHandler(this, new StatementCompletedEventArgs(recordCount));
					}
					catch (Exception ex)
					{
						if (!ADP.IsCatchableOrSecurityExceptionType(ex))
						{
							throw;
						}
					}
				}
			}
		}

		// Token: 0x0600115E RID: 4446 RVA: 0x000570B8 File Offset: 0x000552B8
		private void PropertyChanging()
		{
			this.IsDirty = true;
		}

		/// <summary>Creates a prepared version of the command on an instance of SQL Server.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		///   <IPermission class="System.Security.Permissions.RegistryPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence, ControlPolicy, ControlAppDomain" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Data.SqlClient.SqlClientPermission, System.Data, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600115F RID: 4447 RVA: 0x000570C4 File Offset: 0x000552C4
		public override void Prepare()
		{
			this._pendingCancel = false;
			SqlStatistics sqlStatistics = null;
			sqlStatistics = SqlStatistics.StartTimer(this.Statistics);
			if ((this.IsPrepared && !this.IsDirty) || this.CommandType == CommandType.StoredProcedure || (CommandType.Text == this.CommandType && this.GetParameterCount(this._parameters) == 0))
			{
				if (this.Statistics != null)
				{
					this.Statistics.SafeIncrement(ref this.Statistics._prepares);
				}
				this._hiddenPrepare = false;
			}
			else
			{
				this.ValidateCommand(false, "Prepare");
				bool flag = true;
				try
				{
					this.GetStateObject(null);
					if (this._parameters != null)
					{
						int count = this._parameters.Count;
						for (int i = 0; i < count; i++)
						{
							this._parameters[i].Prepare(this);
						}
					}
					this.InternalPrepare();
				}
				catch (Exception ex)
				{
					flag = ADP.IsCatchableExceptionType(ex);
					throw;
				}
				finally
				{
					if (flag)
					{
						this._hiddenPrepare = false;
						this.ReliablePutStateObject();
					}
				}
			}
			SqlStatistics.StopTimer(sqlStatistics);
		}

		// Token: 0x06001160 RID: 4448 RVA: 0x000571CC File Offset: 0x000553CC
		private void InternalPrepare()
		{
			if (this.IsDirty)
			{
				this.Unprepare();
				this.IsDirty = false;
			}
			this._execType = SqlCommand.EXECTYPE.PREPAREPENDING;
			this._preparedConnectionCloseCount = this._activeConnection.CloseCount;
			this._preparedConnectionReconnectCount = this._activeConnection.ReconnectCount;
			if (this.Statistics != null)
			{
				this.Statistics.SafeIncrement(ref this.Statistics._prepares);
			}
		}

		// Token: 0x06001161 RID: 4449 RVA: 0x00057236 File Offset: 0x00055436
		internal void Unprepare()
		{
			this._execType = SqlCommand.EXECTYPE.PREPAREPENDING;
			if (this._activeConnection.CloseCount != this._preparedConnectionCloseCount || this._activeConnection.ReconnectCount != this._preparedConnectionReconnectCount)
			{
				this._prepareHandle = -1;
			}
			this._cachedMetaData = null;
		}

		/// <summary>Tries to cancel the execution of a <see cref="T:System.Data.SqlClient.SqlCommand" />.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001162 RID: 4450 RVA: 0x00057274 File Offset: 0x00055474
		public override void Cancel()
		{
			SqlStatistics sqlStatistics = null;
			try
			{
				sqlStatistics = SqlStatistics.StartTimer(this.Statistics);
				TaskCompletionSource<object> reconnectionCompletionSource = this._reconnectionCompletionSource;
				if (reconnectionCompletionSource == null || !reconnectionCompletionSource.TrySetCanceled())
				{
					if (this._activeConnection != null)
					{
						SqlInternalConnectionTds sqlInternalConnectionTds = this._activeConnection.InnerConnection as SqlInternalConnectionTds;
						if (sqlInternalConnectionTds != null)
						{
							SqlInternalConnectionTds sqlInternalConnectionTds2 = sqlInternalConnectionTds;
							lock (sqlInternalConnectionTds2)
							{
								if (sqlInternalConnectionTds == this._activeConnection.InnerConnection as SqlInternalConnectionTds)
								{
									if (sqlInternalConnectionTds.Parser != null)
									{
										if (!this._pendingCancel)
										{
											this._pendingCancel = true;
											TdsParserStateObject stateObj = this._stateObj;
											if (stateObj != null)
											{
												stateObj.Cancel(this);
											}
											else
											{
												SqlDataReader sqlDataReader = sqlInternalConnectionTds.FindLiveReader(this);
												if (sqlDataReader != null)
												{
													sqlDataReader.Cancel(this);
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
			finally
			{
				SqlStatistics.StopTimer(sqlStatistics);
			}
		}

		/// <summary>Creates a new instance of a <see cref="T:System.Data.SqlClient.SqlParameter" /> object.</summary>
		/// <returns>A <see cref="T:System.Data.SqlClient.SqlParameter" /> object.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001163 RID: 4451 RVA: 0x00056B4F File Offset: 0x00054D4F
		public new SqlParameter CreateParameter()
		{
			return new SqlParameter();
		}

		// Token: 0x06001164 RID: 4452 RVA: 0x0005736C File Offset: 0x0005556C
		protected override DbParameter CreateDbParameter()
		{
			return this.CreateParameter();
		}

		// Token: 0x06001165 RID: 4453 RVA: 0x00057374 File Offset: 0x00055574
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this._cachedMetaData = null;
			}
			base.Dispose(disposing);
		}

		/// <summary>Executes the query, and returns the first column of the first row in the result set returned by the query. Additional columns or rows are ignored.</summary>
		/// <returns>The first column of the first row in the result set, or a null reference (Nothing in Visual Basic) if the result set is empty. Returns a maximum of 2033 characters.</returns>
		/// <exception cref="T:System.InvalidCastException">A <see cref="P:System.Data.SqlClient.SqlParameter.SqlDbType" /> other than Binary or VarBinary was used when <see cref="P:System.Data.SqlClient.SqlParameter.Value" /> was set to <see cref="T:System.IO.Stream" />. For more information about streaming, see SqlClient Streaming Support.A <see cref="P:System.Data.SqlClient.SqlParameter.SqlDbType" /> other than Char, NChar, NVarChar, VarChar, or  Xml was used when <see cref="P:System.Data.SqlClient.SqlParameter.Value" /> was set to <see cref="T:System.IO.TextReader" />.A <see cref="P:System.Data.SqlClient.SqlParameter.SqlDbType" /> other than Xml was used when <see cref="P:System.Data.SqlClient.SqlParameter.Value" /> was set to <see cref="T:System.Xml.XmlReader" />.</exception>
		/// <exception cref="T:System.Data.SqlClient.SqlException">An exception occurred while executing the command against a locked row. This exception is not generated when you are using Microsoft .NET Framework version 1.0.A timeout occurred during a streaming operation. For more information about streaming, see SqlClient Streaming Support.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Data.SqlClient.SqlConnection" /> closed or dropped during a streaming operation. For more information about streaming, see SqlClient Streaming Support.</exception>
		/// <exception cref="T:System.IO.IOException">An error occurred in a <see cref="T:System.IO.Stream" />, <see cref="T:System.Xml.XmlReader" /> or <see cref="T:System.IO.TextReader" /> object during a streaming operation.  For more information about streaming, see SqlClient Streaming Support.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.Stream" />, <see cref="T:System.Xml.XmlReader" /> or <see cref="T:System.IO.TextReader" /> object was closed during a streaming operation.  For more information about streaming, see SqlClient Streaming Support.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		///   <IPermission class="System.Security.Permissions.RegistryPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence, ControlPolicy, ControlAppDomain" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Data.SqlClient.SqlClientPermission, System.Data, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001166 RID: 4454 RVA: 0x00057388 File Offset: 0x00055588
		public override object ExecuteScalar()
		{
			this._pendingCancel = false;
			Guid guid = SqlCommand._diagnosticListener.WriteCommandBefore(this, "ExecuteScalar");
			SqlStatistics sqlStatistics = null;
			Exception ex = null;
			object obj;
			try
			{
				sqlStatistics = SqlStatistics.StartTimer(this.Statistics);
				SqlDataReader sqlDataReader = this.RunExecuteReader(CommandBehavior.Default, RunBehavior.ReturnImmediately, true, "ExecuteScalar");
				obj = this.CompleteExecuteScalar(sqlDataReader, false);
			}
			catch (Exception ex)
			{
				throw;
			}
			finally
			{
				SqlStatistics.StopTimer(sqlStatistics);
				if (ex != null)
				{
					SqlCommand._diagnosticListener.WriteCommandError(guid, this, ex, "ExecuteScalar");
				}
				else
				{
					SqlCommand._diagnosticListener.WriteCommandAfter(guid, this, "ExecuteScalar");
				}
			}
			return obj;
		}

		// Token: 0x06001167 RID: 4455 RVA: 0x0005742C File Offset: 0x0005562C
		private object CompleteExecuteScalar(SqlDataReader ds, bool returnSqlValue)
		{
			object obj = null;
			try
			{
				if (ds.Read() && ds.FieldCount > 0)
				{
					if (returnSqlValue)
					{
						obj = ds.GetSqlValue(0);
					}
					else
					{
						obj = ds.GetValue(0);
					}
				}
			}
			finally
			{
				ds.Close();
			}
			return obj;
		}

		/// <summary>Executes a Transact-SQL statement against the connection and returns the number of rows affected.</summary>
		/// <returns>The number of rows affected.</returns>
		/// <exception cref="T:System.InvalidCastException">A <see cref="P:System.Data.SqlClient.SqlParameter.SqlDbType" /> other than Binary or VarBinary was used when <see cref="P:System.Data.SqlClient.SqlParameter.Value" /> was set to <see cref="T:System.IO.Stream" />. For more information about streaming, see SqlClient Streaming Support.A <see cref="P:System.Data.SqlClient.SqlParameter.SqlDbType" /> other than Char, NChar, NVarChar, VarChar, or  Xml was used when <see cref="P:System.Data.SqlClient.SqlParameter.Value" /> was set to <see cref="T:System.IO.TextReader" />.A <see cref="P:System.Data.SqlClient.SqlParameter.SqlDbType" /> other than Xml was used when <see cref="P:System.Data.SqlClient.SqlParameter.Value" /> was set to <see cref="T:System.Xml.XmlReader" />.</exception>
		/// <exception cref="T:System.Data.SqlClient.SqlException">An exception occurred while executing the command against a locked row. This exception is not generated when you are using Microsoft .NET Framework version 1.0.A timeout occurred during a streaming operation. For more information about streaming, see SqlClient Streaming Support.</exception>
		/// <exception cref="T:System.IO.IOException">An error occurred in a <see cref="T:System.IO.Stream" />, <see cref="T:System.Xml.XmlReader" /> or <see cref="T:System.IO.TextReader" /> object during a streaming operation.  For more information about streaming, see SqlClient Streaming Support.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Data.SqlClient.SqlConnection" /> closed or dropped during a streaming operation. For more information about streaming, see SqlClient Streaming Support.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.Stream" />, <see cref="T:System.Xml.XmlReader" /> or <see cref="T:System.IO.TextReader" /> object was closed during a streaming operation.  For more information about streaming, see SqlClient Streaming Support.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Data.SqlClient.SqlClientPermission, System.Data, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001168 RID: 4456 RVA: 0x0005747C File Offset: 0x0005567C
		public override int ExecuteNonQuery()
		{
			this._pendingCancel = false;
			Guid guid = SqlCommand._diagnosticListener.WriteCommandBefore(this, "ExecuteNonQuery");
			SqlStatistics sqlStatistics = null;
			Exception ex = null;
			int rowsAffected;
			try
			{
				sqlStatistics = SqlStatistics.StartTimer(this.Statistics);
				this.InternalExecuteNonQuery(null, false, this.CommandTimeout, false, "ExecuteNonQuery");
				rowsAffected = this._rowsAffected;
			}
			catch (Exception ex)
			{
				throw;
			}
			finally
			{
				SqlStatistics.StopTimer(sqlStatistics);
				if (ex != null)
				{
					SqlCommand._diagnosticListener.WriteCommandError(guid, this, ex, "ExecuteNonQuery");
				}
				else
				{
					SqlCommand._diagnosticListener.WriteCommandAfter(guid, this, "ExecuteNonQuery");
				}
			}
			return rowsAffected;
		}

		/// <summary>Initiates the asynchronous execution of the Transact-SQL statement or stored procedure that is described by this <see cref="T:System.Data.SqlClient.SqlCommand" />, given a callback procedure and state information.</summary>
		/// <returns>An <see cref="T:System.IAsyncResult" /> that can be used to poll or wait for results, or both; this value is also needed when invoking <see cref="M:System.Data.SqlClient.SqlCommand.EndExecuteNonQuery(System.IAsyncResult)" />, which returns the number of affected rows.</returns>
		/// <param name="callback">An <see cref="T:System.AsyncCallback" /> delegate that is invoked when the command's execution has completed. Pass null (Nothing in Microsoft Visual Basic) to indicate that no callback is required.</param>
		/// <param name="stateObject">A user-defined state object that is passed to the callback procedure. Retrieve this object from within the callback procedure using the <see cref="P:System.IAsyncResult.AsyncState" /> property.</param>
		/// <exception cref="T:System.InvalidCastException">A <see cref="P:System.Data.SqlClient.SqlParameter.SqlDbType" /> other than Binary or VarBinary was used when <see cref="P:System.Data.SqlClient.SqlParameter.Value" /> was set to <see cref="T:System.IO.Stream" />. For more information about streaming, see SqlClient Streaming Support.A <see cref="P:System.Data.SqlClient.SqlParameter.SqlDbType" /> other than Char, NChar, NVarChar, VarChar, or  Xml was used when <see cref="P:System.Data.SqlClient.SqlParameter.Value" /> was set to <see cref="T:System.IO.TextReader" />.A <see cref="P:System.Data.SqlClient.SqlParameter.SqlDbType" /> other than Xml was used when <see cref="P:System.Data.SqlClient.SqlParameter.Value" /> was set to <see cref="T:System.Xml.XmlReader" />.</exception>
		/// <exception cref="T:System.Data.SqlClient.SqlException">Any error that occurred while executing the command text.A timeout occurred during a streaming operation. For more information about streaming, see SqlClient Streaming Support.</exception>
		/// <exception cref="T:System.InvalidOperationException">The name/value pair "Asynchronous Processing=true" was not included within the connection string defining the connection for this <see cref="T:System.Data.SqlClient.SqlCommand" />.The <see cref="T:System.Data.SqlClient.SqlConnection" /> closed or dropped during a streaming operation. For more information about streaming, see SqlClient Streaming Support.</exception>
		/// <exception cref="T:System.IO.IOException">An error occurred in a <see cref="T:System.IO.Stream" />, <see cref="T:System.Xml.XmlReader" /> or <see cref="T:System.IO.TextReader" /> object during a streaming operation.  For more information about streaming, see SqlClient Streaming Support.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.Stream" />, <see cref="T:System.Xml.XmlReader" /> or <see cref="T:System.IO.TextReader" /> object was closed during a streaming operation.  For more information about streaming, see SqlClient Streaming Support.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001169 RID: 4457 RVA: 0x00057520 File Offset: 0x00055720
		private IAsyncResult BeginExecuteNonQuery(AsyncCallback callback, object stateObject)
		{
			this._pendingCancel = false;
			this.ValidateAsyncCommand();
			SqlStatistics sqlStatistics = null;
			IAsyncResult task2;
			try
			{
				sqlStatistics = SqlStatistics.StartTimer(this.Statistics);
				TaskCompletionSource<object> completion = new TaskCompletionSource<object>(stateObject);
				try
				{
					Task task = this.InternalExecuteNonQuery(completion, false, this.CommandTimeout, true, "BeginExecuteNonQuery");
					this.cachedAsyncState.SetActiveConnectionAndResult(completion, "EndExecuteNonQuery", this._activeConnection);
					if (task != null)
					{
						AsyncHelper.ContinueTask(task, completion, delegate
						{
							this.BeginExecuteNonQueryInternalReadStage(completion);
						}, null, null, null, null, null);
					}
					else
					{
						this.BeginExecuteNonQueryInternalReadStage(completion);
					}
				}
				catch (Exception ex)
				{
					if (!ADP.IsCatchableOrSecurityExceptionType(ex))
					{
						throw;
					}
					this.ReliablePutStateObject();
					throw;
				}
				if (callback != null)
				{
					completion.Task.ContinueWith(delegate(Task<object> t)
					{
						callback(t);
					}, TaskScheduler.Default);
				}
				task2 = completion.Task;
			}
			finally
			{
				SqlStatistics.StopTimer(sqlStatistics);
			}
			return task2;
		}

		// Token: 0x0600116A RID: 4458 RVA: 0x00057658 File Offset: 0x00055858
		private void BeginExecuteNonQueryInternalReadStage(TaskCompletionSource<object> completion)
		{
			try
			{
				this._stateObj.ReadSni(completion);
			}
			catch (Exception)
			{
				if (this._cachedAsyncState != null)
				{
					this._cachedAsyncState.ResetAsyncState();
				}
				this.ReliablePutStateObject();
				throw;
			}
		}

		// Token: 0x0600116B RID: 4459 RVA: 0x000576A0 File Offset: 0x000558A0
		private void VerifyEndExecuteState(Task completionTask, string endMethod)
		{
			if (completionTask.IsCanceled)
			{
				if (this._stateObj == null)
				{
					throw SQL.CR_ReconnectionCancelled();
				}
				this._stateObj.Parser.State = TdsParserState.Broken;
				this._stateObj.Parser.Connection.BreakConnection();
				this._stateObj.Parser.ThrowExceptionAndWarning(this._stateObj, false, false);
			}
			else if (completionTask.IsFaulted)
			{
				throw completionTask.Exception.InnerException;
			}
			if (this.cachedAsyncState.EndMethodName == null)
			{
				throw ADP.MethodCalledTwice(endMethod);
			}
			if (endMethod != this.cachedAsyncState.EndMethodName)
			{
				throw ADP.MismatchedAsyncResult(this.cachedAsyncState.EndMethodName, endMethod);
			}
			if (this._activeConnection.State != ConnectionState.Open || !this.cachedAsyncState.IsActiveConnectionValid(this._activeConnection))
			{
				throw ADP.ClosedConnectionError();
			}
		}

		// Token: 0x0600116C RID: 4460 RVA: 0x00057777 File Offset: 0x00055977
		private void WaitForAsyncResults(IAsyncResult asyncResult)
		{
			Task task = (Task)asyncResult;
			if (!asyncResult.IsCompleted)
			{
				asyncResult.AsyncWaitHandle.WaitOne();
			}
			this._stateObj._networkPacketTaskSource = null;
			this._activeConnection.GetOpenTdsConnection().DecrementAsyncCount();
		}

		// Token: 0x0600116D RID: 4461 RVA: 0x000577B0 File Offset: 0x000559B0
		private void ThrowIfReconnectionHasBeenCanceled()
		{
			if (this._stateObj == null)
			{
				TaskCompletionSource<object> reconnectionCompletionSource = this._reconnectionCompletionSource;
				if (reconnectionCompletionSource != null && reconnectionCompletionSource.Task.IsCanceled)
				{
					throw SQL.CR_ReconnectionCancelled();
				}
			}
		}

		/// <summary>Finishes asynchronous execution of a Transact-SQL statement.</summary>
		/// <returns>The number of rows affected (the same behavior as <see cref="M:System.Data.SqlClient.SqlCommand.ExecuteNonQuery" />).</returns>
		/// <param name="asyncResult">The <see cref="T:System.IAsyncResult" /> returned by the call to <see cref="M:System.Data.SqlClient.SqlCommand.BeginExecuteNonQuery" />.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="asyncResult" /> parameter is null (Nothing in Microsoft Visual Basic)</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="M:System.Data.SqlClient.SqlCommand.EndExecuteNonQuery(System.IAsyncResult)" /> was called more than once for a single command execution, or the method was mismatched against its execution method (for example, the code called <see cref="M:System.Data.SqlClient.SqlCommand.EndExecuteNonQuery(System.IAsyncResult)" /> to complete execution of a call to <see cref="M:System.Data.SqlClient.SqlCommand.BeginExecuteXmlReader" />.</exception>
		/// <exception cref="T:System.Data.SqlClient.SqlException">The amount of time specified in <see cref="P:System.Data.SqlClient.SqlCommand.CommandTimeout" /> elapsed and the asynchronous operation specified with <see cref="Overload:System.Data.SqlClient.SqlCommand.BeginExecuteNonQuery" /> is not complete.In some situations, <see cref="T:System.IAsyncResult" /> can be set to IsCompleted incorrectly. If this occurs and <see cref="M:System.Data.SqlClient.SqlCommand.EndExecuteNonQuery(System.IAsyncResult)" /> is called, EndExecuteNonQuery could raise a SqlException error if the amount of time specified in <see cref="P:System.Data.SqlClient.SqlCommand.CommandTimeout" /> elapsed and the asynchronous operation specified with <see cref="Overload:System.Data.SqlClient.SqlCommand.BeginExecuteNonQuery" /> is not complete. To correct this situation, you should either increase the value of CommandTimeout or reduce the work being done by the asynchronous operation.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600116E RID: 4462 RVA: 0x000577E4 File Offset: 0x000559E4
		private int EndExecuteNonQuery(IAsyncResult asyncResult)
		{
			Exception exception = ((Task)asyncResult).Exception;
			if (exception != null)
			{
				if (this.cachedAsyncState != null)
				{
					this.cachedAsyncState.ResetAsyncState();
				}
				this.ReliablePutStateObject();
				throw exception.InnerException;
			}
			this.ThrowIfReconnectionHasBeenCanceled();
			TdsParserStateObject stateObj = this._stateObj;
			int num;
			lock (stateObj)
			{
				num = this.EndExecuteNonQueryInternal(asyncResult);
			}
			return num;
		}

		// Token: 0x0600116F RID: 4463 RVA: 0x0005785C File Offset: 0x00055A5C
		private int EndExecuteNonQueryInternal(IAsyncResult asyncResult)
		{
			SqlStatistics sqlStatistics = null;
			int rowsAffected;
			try
			{
				sqlStatistics = SqlStatistics.StartTimer(this.Statistics);
				this.VerifyEndExecuteState((Task)asyncResult, "EndExecuteNonQuery");
				this.WaitForAsyncResults(asyncResult);
				bool flag = true;
				try
				{
					this.CheckThrowSNIException();
					if (CommandType.Text == this.CommandType && this.GetParameterCount(this._parameters) == 0)
					{
						try
						{
							bool flag2;
							if (!this._stateObj.Parser.TryRun(RunBehavior.UntilDone, this, null, null, this._stateObj, out flag2))
							{
								throw SQL.SynchronousCallMayNotPend();
							}
							goto IL_0087;
						}
						finally
						{
							this.cachedAsyncState.ResetAsyncState();
						}
					}
					SqlDataReader sqlDataReader = this.CompleteAsyncExecuteReader();
					if (sqlDataReader != null)
					{
						sqlDataReader.Close();
					}
					IL_0087:;
				}
				catch (Exception ex)
				{
					flag = ADP.IsCatchableExceptionType(ex);
					throw;
				}
				finally
				{
					if (flag)
					{
						this.PutStateObject();
					}
				}
				rowsAffected = this._rowsAffected;
			}
			catch (Exception ex2)
			{
				if (this.cachedAsyncState != null)
				{
					this.cachedAsyncState.ResetAsyncState();
				}
				if (ADP.IsCatchableExceptionType(ex2))
				{
					this.ReliablePutStateObject();
				}
				throw;
			}
			finally
			{
				SqlStatistics.StopTimer(sqlStatistics);
			}
			return rowsAffected;
		}

		// Token: 0x06001170 RID: 4464 RVA: 0x0005797C File Offset: 0x00055B7C
		private Task InternalExecuteNonQuery(TaskCompletionSource<object> completion, bool sendToPipe, int timeout, bool asyncWrite = false, [CallerMemberName] string methodName = "")
		{
			bool flag = completion != null;
			SqlStatistics statistics = this.Statistics;
			this._rowsAffected = -1;
			this.ValidateCommand(flag, methodName);
			this.CheckNotificationStateAndAutoEnlist();
			Task task = null;
			if (CommandType.Text == this.CommandType && this.GetParameterCount(this._parameters) == 0)
			{
				if (statistics != null)
				{
					if (!this.IsDirty && this.IsPrepared)
					{
						statistics.SafeIncrement(ref statistics._preparedExecs);
					}
					else
					{
						statistics.SafeIncrement(ref statistics._unpreparedExecs);
					}
				}
				task = this.RunExecuteNonQueryTds(methodName, flag, timeout, asyncWrite);
			}
			else
			{
				SqlDataReader reader = this.RunExecuteReader(CommandBehavior.Default, RunBehavior.UntilDone, false, completion, timeout, out task, asyncWrite, methodName);
				if (reader != null)
				{
					if (task != null)
					{
						task = AsyncHelper.CreateContinuationTask(task, delegate
						{
							reader.Close();
						}, null, null);
					}
					else
					{
						reader.Close();
					}
				}
			}
			return task;
		}

		/// <summary>Sends the <see cref="P:System.Data.SqlClient.SqlCommand.CommandText" /> to the <see cref="P:System.Data.SqlClient.SqlCommand.Connection" /> and builds an <see cref="T:System.Xml.XmlReader" /> object.</summary>
		/// <returns>An <see cref="T:System.Xml.XmlReader" /> object.</returns>
		/// <exception cref="T:System.InvalidCastException">A <see cref="P:System.Data.SqlClient.SqlParameter.SqlDbType" /> other than Binary or VarBinary was used when <see cref="P:System.Data.SqlClient.SqlParameter.Value" /> was set to <see cref="T:System.IO.Stream" />. For more information about streaming, see SqlClient Streaming Support.A <see cref="P:System.Data.SqlClient.SqlParameter.SqlDbType" /> other than Char, NChar, NVarChar, VarChar, or  Xml was used when <see cref="P:System.Data.SqlClient.SqlParameter.Value" /> was set to <see cref="T:System.IO.TextReader" />.A <see cref="P:System.Data.SqlClient.SqlParameter.SqlDbType" /> other than Xml was used when <see cref="P:System.Data.SqlClient.SqlParameter.Value" /> was set to <see cref="T:System.Xml.XmlReader" />.</exception>
		/// <exception cref="T:System.Data.SqlClient.SqlException">An exception occurred while executing the command against a locked row. This exception is not generated when you are using Microsoft .NET Framework version 1.0.A timeout occurred during a streaming operation. For more information about streaming, see SqlClient Streaming Support.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Data.SqlClient.SqlConnection" /> closed or dropped during a streaming operation. For more information about streaming, see SqlClient Streaming Support.</exception>
		/// <exception cref="T:System.IO.IOException">An error occurred in a <see cref="T:System.IO.Stream" />, <see cref="T:System.Xml.XmlReader" /> or <see cref="T:System.IO.TextReader" /> object during a streaming operation.  For more information about streaming, see SqlClient Streaming Support.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.Stream" />, <see cref="T:System.Xml.XmlReader" /> or <see cref="T:System.IO.TextReader" /> object was closed during a streaming operation.  For more information about streaming, see SqlClient Streaming Support.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		///   <IPermission class="System.Security.Permissions.RegistryPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence, ControlPolicy, ControlAppDomain" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Data.SqlClient.SqlClientPermission, System.Data, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001171 RID: 4465 RVA: 0x00057A50 File Offset: 0x00055C50
		public XmlReader ExecuteXmlReader()
		{
			this._pendingCancel = false;
			Guid guid = SqlCommand._diagnosticListener.WriteCommandBefore(this, "ExecuteXmlReader");
			SqlStatistics sqlStatistics = null;
			Exception ex = null;
			XmlReader xmlReader;
			try
			{
				sqlStatistics = SqlStatistics.StartTimer(this.Statistics);
				SqlDataReader sqlDataReader = this.RunExecuteReader(CommandBehavior.SequentialAccess, RunBehavior.ReturnImmediately, true, "ExecuteXmlReader");
				xmlReader = this.CompleteXmlReader(sqlDataReader, false);
			}
			catch (Exception ex)
			{
				throw;
			}
			finally
			{
				SqlStatistics.StopTimer(sqlStatistics);
				if (ex != null)
				{
					SqlCommand._diagnosticListener.WriteCommandError(guid, this, ex, "ExecuteXmlReader");
				}
				else
				{
					SqlCommand._diagnosticListener.WriteCommandAfter(guid, this, "ExecuteXmlReader");
				}
			}
			return xmlReader;
		}

		/// <summary>Initiates the asynchronous execution of the Transact-SQL statement or stored procedure that is described by this <see cref="T:System.Data.SqlClient.SqlCommand" /> and returns results as an <see cref="T:System.Xml.XmlReader" /> object, using a callback procedure.</summary>
		/// <returns>An <see cref="T:System.IAsyncResult" /> that can be used to poll, wait for results, or both; this value is also needed when the <see cref="M:System.Data.SqlClient.SqlCommand.EndExecuteXmlReader(System.IAsyncResult)" /> is called, which returns the results of the command as XML.</returns>
		/// <param name="callback">An <see cref="T:System.AsyncCallback" /> delegate that is invoked when the command's execution has completed. Pass null (Nothing in Microsoft Visual Basic) to indicate that no callback is required.</param>
		/// <param name="stateObject">A user-defined state object that is passed to the callback procedure. Retrieve this object from within the callback procedure using the <see cref="P:System.IAsyncResult.AsyncState" /> property.</param>
		/// <exception cref="T:System.InvalidCastException">A <see cref="P:System.Data.SqlClient.SqlParameter.SqlDbType" /> other than Binary or VarBinary was used when <see cref="P:System.Data.SqlClient.SqlParameter.Value" /> was set to <see cref="T:System.IO.Stream" />. For more information about streaming, see SqlClient Streaming Support.A <see cref="P:System.Data.SqlClient.SqlParameter.SqlDbType" /> other than Char, NChar, NVarChar, VarChar, or  Xml was used when <see cref="P:System.Data.SqlClient.SqlParameter.Value" /> was set to <see cref="T:System.IO.TextReader" />.A <see cref="P:System.Data.SqlClient.SqlParameter.SqlDbType" /> other than Xml was used when <see cref="P:System.Data.SqlClient.SqlParameter.Value" /> was set to <see cref="T:System.Xml.XmlReader" />.</exception>
		/// <exception cref="T:System.Data.SqlClient.SqlException">Any error that occurred while executing the command text.A timeout occurred during a streaming operation. For more information about streaming, see SqlClient Streaming Support.</exception>
		/// <exception cref="T:System.InvalidOperationException">The name/value pair "Asynchronous Processing=true" was not included within the connection string defining the connection for this <see cref="T:System.Data.SqlClient.SqlCommand" />.The <see cref="T:System.Data.SqlClient.SqlConnection" /> closed or dropped during a streaming operation. For more information about streaming, see SqlClient Streaming Support.</exception>
		/// <exception cref="T:System.IO.IOException">An error occurred in a <see cref="T:System.IO.Stream" />, <see cref="T:System.Xml.XmlReader" /> or <see cref="T:System.IO.TextReader" /> object during a streaming operation.  For more information about streaming, see SqlClient Streaming Support.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.Stream" />, <see cref="T:System.Xml.XmlReader" /> or <see cref="T:System.IO.TextReader" /> object was closed during a streaming operation.  For more information about streaming, see SqlClient Streaming Support.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001172 RID: 4466 RVA: 0x00057AF4 File Offset: 0x00055CF4
		private IAsyncResult BeginExecuteXmlReader(AsyncCallback callback, object stateObject)
		{
			this._pendingCancel = false;
			this.ValidateAsyncCommand();
			SqlStatistics sqlStatistics = null;
			IAsyncResult task2;
			try
			{
				sqlStatistics = SqlStatistics.StartTimer(this.Statistics);
				TaskCompletionSource<object> completion = new TaskCompletionSource<object>(stateObject);
				Task task;
				try
				{
					this.RunExecuteReader(CommandBehavior.SequentialAccess, RunBehavior.ReturnImmediately, true, completion, this.CommandTimeout, out task, true, "BeginExecuteXmlReader");
				}
				catch (Exception ex)
				{
					if (!ADP.IsCatchableOrSecurityExceptionType(ex))
					{
						throw;
					}
					this.ReliablePutStateObject();
					throw;
				}
				this.cachedAsyncState.SetActiveConnectionAndResult(completion, "EndExecuteXmlReader", this._activeConnection);
				if (task != null)
				{
					AsyncHelper.ContinueTask(task, completion, delegate
					{
						this.BeginExecuteXmlReaderInternalReadStage(completion);
					}, null, null, null, null, null);
				}
				else
				{
					this.BeginExecuteXmlReaderInternalReadStage(completion);
				}
				if (callback != null)
				{
					completion.Task.ContinueWith(delegate(Task<object> t)
					{
						callback(t);
					}, TaskScheduler.Default);
				}
				task2 = completion.Task;
			}
			finally
			{
				SqlStatistics.StopTimer(sqlStatistics);
			}
			return task2;
		}

		// Token: 0x06001173 RID: 4467 RVA: 0x00057C30 File Offset: 0x00055E30
		private void BeginExecuteXmlReaderInternalReadStage(TaskCompletionSource<object> completion)
		{
			try
			{
				this._stateObj.ReadSni(completion);
			}
			catch (Exception ex)
			{
				if (this._cachedAsyncState != null)
				{
					this._cachedAsyncState.ResetAsyncState();
				}
				this.ReliablePutStateObject();
				completion.TrySetException(ex);
			}
		}

		/// <summary>Finishes asynchronous execution of a Transact-SQL statement, returning the requested data as XML.</summary>
		/// <returns>An <see cref="T:System.Xml.XmlReader" /> object that can be used to fetch the resulting XML data.</returns>
		/// <param name="asyncResult">The <see cref="T:System.IAsyncResult" /> returned by the call to <see cref="M:System.Data.SqlClient.SqlCommand.BeginExecuteXmlReader" />.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="asyncResult" /> parameter is null (Nothing in Microsoft Visual Basic)</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="M:System.Data.SqlClient.SqlCommand.EndExecuteXmlReader(System.IAsyncResult)" /> was called more than once for a single command execution, or the method was mismatched against its execution method (for example, the code called <see cref="M:System.Data.SqlClient.SqlCommand.EndExecuteXmlReader(System.IAsyncResult)" /> to complete execution of a call to <see cref="M:System.Data.SqlClient.SqlCommand.BeginExecuteNonQuery" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001174 RID: 4468 RVA: 0x00057C80 File Offset: 0x00055E80
		private XmlReader EndExecuteXmlReader(IAsyncResult asyncResult)
		{
			Exception exception = ((Task)asyncResult).Exception;
			if (exception != null)
			{
				if (this.cachedAsyncState != null)
				{
					this.cachedAsyncState.ResetAsyncState();
				}
				this.ReliablePutStateObject();
				throw exception.InnerException;
			}
			this.ThrowIfReconnectionHasBeenCanceled();
			TdsParserStateObject stateObj = this._stateObj;
			XmlReader xmlReader;
			lock (stateObj)
			{
				xmlReader = this.EndExecuteXmlReaderInternal(asyncResult);
			}
			return xmlReader;
		}

		// Token: 0x06001175 RID: 4469 RVA: 0x00057CF8 File Offset: 0x00055EF8
		private XmlReader EndExecuteXmlReaderInternal(IAsyncResult asyncResult)
		{
			XmlReader xmlReader;
			try
			{
				xmlReader = this.CompleteXmlReader(this.InternalEndExecuteReader(asyncResult, "EndExecuteXmlReader"), true);
			}
			catch (Exception ex)
			{
				if (this.cachedAsyncState != null)
				{
					this.cachedAsyncState.ResetAsyncState();
				}
				if (ADP.IsCatchableExceptionType(ex))
				{
					this.ReliablePutStateObject();
				}
				throw;
			}
			return xmlReader;
		}

		// Token: 0x06001176 RID: 4470 RVA: 0x00057D50 File Offset: 0x00055F50
		private XmlReader CompleteXmlReader(SqlDataReader ds, bool async = false)
		{
			XmlReader xmlReader = null;
			SmiExtendedMetaData[] internalSmiMetaData = ds.GetInternalSmiMetaData();
			if (internalSmiMetaData != null && internalSmiMetaData.Length == 1 && (internalSmiMetaData[0].SqlDbType == SqlDbType.NText || internalSmiMetaData[0].SqlDbType == SqlDbType.NVarChar || internalSmiMetaData[0].SqlDbType == SqlDbType.Xml))
			{
				try
				{
					xmlReader = new SqlStream(ds, true, internalSmiMetaData[0].SqlDbType != SqlDbType.Xml).ToXmlReader(async);
				}
				catch (Exception ex)
				{
					if (ADP.IsCatchableExceptionType(ex))
					{
						ds.Close();
					}
					throw;
				}
			}
			if (xmlReader == null)
			{
				ds.Close();
				throw SQL.NonXmlResult();
			}
			return xmlReader;
		}

		// Token: 0x06001177 RID: 4471 RVA: 0x00057DEC File Offset: 0x00055FEC
		protected override DbDataReader ExecuteDbDataReader(CommandBehavior behavior)
		{
			return this.ExecuteReader(behavior);
		}

		/// <summary>Sends the <see cref="P:System.Data.SqlClient.SqlCommand.CommandText" /> to the <see cref="P:System.Data.SqlClient.SqlCommand.Connection" /> and builds a <see cref="T:System.Data.SqlClient.SqlDataReader" />.</summary>
		/// <returns>A <see cref="T:System.Data.SqlClient.SqlDataReader" /> object.</returns>
		/// <exception cref="T:System.InvalidCastException">A <see cref="P:System.Data.SqlClient.SqlParameter.SqlDbType" /> other than Binary or VarBinary was used when <see cref="P:System.Data.SqlClient.SqlParameter.Value" /> was set to <see cref="T:System.IO.Stream" />. For more information about streaming, see SqlClient Streaming Support.A <see cref="P:System.Data.SqlClient.SqlParameter.SqlDbType" /> other than Char, NChar, NVarChar, VarChar, or  Xml was used when <see cref="P:System.Data.SqlClient.SqlParameter.Value" /> was set to <see cref="T:System.IO.TextReader" />.A <see cref="P:System.Data.SqlClient.SqlParameter.SqlDbType" /> other than Xml was used when <see cref="P:System.Data.SqlClient.SqlParameter.Value" /> was set to <see cref="T:System.Xml.XmlReader" />.</exception>
		/// <exception cref="T:System.Data.SqlClient.SqlException">An exception occurred while executing the command against a locked row. This exception is not generated when you are using Microsoft .NET Framework version 1.0.A timeout occurred during a streaming operation. For more information about streaming, see SqlClient Streaming Support.</exception>
		/// <exception cref="T:System.InvalidOperationException">The current state of the connection is closed. <see cref="M:System.Data.SqlClient.SqlCommand.ExecuteReader" /> requires an open <see cref="T:System.Data.SqlClient.SqlConnection" />.The <see cref="T:System.Data.SqlClient.SqlConnection" /> closed or dropped during a streaming operation. For more information about streaming, see SqlClient Streaming Support.</exception>
		/// <exception cref="T:System.IO.IOException">An error occurred in a <see cref="T:System.IO.Stream" />, <see cref="T:System.Xml.XmlReader" /> or <see cref="T:System.IO.TextReader" /> object during a streaming operation.  For more information about streaming, see SqlClient Streaming Support.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.Stream" />, <see cref="T:System.Xml.XmlReader" /> or <see cref="T:System.IO.TextReader" /> object was closed during a streaming operation.  For more information about streaming, see SqlClient Streaming Support.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001178 RID: 4472 RVA: 0x00057DF8 File Offset: 0x00055FF8
		public new SqlDataReader ExecuteReader()
		{
			SqlStatistics sqlStatistics = null;
			SqlDataReader sqlDataReader;
			try
			{
				sqlStatistics = SqlStatistics.StartTimer(this.Statistics);
				sqlDataReader = this.ExecuteReader(CommandBehavior.Default);
			}
			finally
			{
				SqlStatistics.StopTimer(sqlStatistics);
			}
			return sqlDataReader;
		}

		/// <summary>Sends the <see cref="P:System.Data.SqlClient.SqlCommand.CommandText" /> to the <see cref="P:System.Data.SqlClient.SqlCommand.Connection" />, and builds a <see cref="T:System.Data.SqlClient.SqlDataReader" /> using one of the <see cref="T:System.Data.CommandBehavior" /> values.</summary>
		/// <returns>A <see cref="T:System.Data.SqlClient.SqlDataReader" /> object.</returns>
		/// <param name="behavior">One of the <see cref="T:System.Data.CommandBehavior" /> values.</param>
		/// <exception cref="T:System.InvalidCastException">A <see cref="P:System.Data.SqlClient.SqlParameter.SqlDbType" /> other than Binary or VarBinary was used when <see cref="P:System.Data.SqlClient.SqlParameter.Value" /> was set to <see cref="T:System.IO.Stream" />. For more information about streaming, see SqlClient Streaming Support.A <see cref="P:System.Data.SqlClient.SqlParameter.SqlDbType" /> other than Char, NChar, NVarChar, VarChar, or  Xml was used when <see cref="P:System.Data.SqlClient.SqlParameter.Value" /> was set to <see cref="T:System.IO.TextReader" />.A <see cref="P:System.Data.SqlClient.SqlParameter.SqlDbType" /> other than Xml was used when <see cref="P:System.Data.SqlClient.SqlParameter.Value" /> was set to <see cref="T:System.Xml.XmlReader" />.</exception>
		/// <exception cref="T:System.Data.SqlClient.SqlException">A timeout occurred during a streaming operation. For more information about streaming, see SqlClient Streaming Support.</exception>
		/// <exception cref="T:System.IO.IOException">An error occurred in a <see cref="T:System.IO.Stream" />, <see cref="T:System.Xml.XmlReader" /> or <see cref="T:System.IO.TextReader" /> object during a streaming operation.  For more information about streaming, see SqlClient Streaming Support.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Data.SqlClient.SqlConnection" /> closed or dropped during a streaming operation. For more information about streaming, see SqlClient Streaming Support.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.Stream" />, <see cref="T:System.Xml.XmlReader" /> or <see cref="T:System.IO.TextReader" /> object was closed during a streaming operation.  For more information about streaming, see SqlClient Streaming Support.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		///   <IPermission class="System.Security.Permissions.RegistryPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence, ControlPolicy, ControlAppDomain" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Data.SqlClient.SqlClientPermission, System.Data, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001179 RID: 4473 RVA: 0x00057E38 File Offset: 0x00056038
		public new SqlDataReader ExecuteReader(CommandBehavior behavior)
		{
			this._pendingCancel = false;
			Guid guid = SqlCommand._diagnosticListener.WriteCommandBefore(this, "ExecuteReader");
			SqlStatistics sqlStatistics = null;
			Exception ex = null;
			SqlDataReader sqlDataReader;
			try
			{
				sqlStatistics = SqlStatistics.StartTimer(this.Statistics);
				sqlDataReader = this.RunExecuteReader(behavior, RunBehavior.ReturnImmediately, true, "ExecuteReader");
			}
			catch (Exception ex)
			{
				throw;
			}
			finally
			{
				SqlStatistics.StopTimer(sqlStatistics);
				if (ex != null)
				{
					SqlCommand._diagnosticListener.WriteCommandError(guid, this, ex, "ExecuteReader");
				}
				else
				{
					SqlCommand._diagnosticListener.WriteCommandAfter(guid, this, "ExecuteReader");
				}
			}
			return sqlDataReader;
		}

		/// <summary>Finishes asynchronous execution of a Transact-SQL statement, returning the requested <see cref="T:System.Data.SqlClient.SqlDataReader" />.</summary>
		/// <returns>A <see cref="T:System.Data.SqlClient.SqlDataReader" /> object that can be used to retrieve the requested rows.</returns>
		/// <param name="asyncResult">The <see cref="T:System.IAsyncResult" /> returned by the call to <see cref="M:System.Data.SqlClient.SqlCommand.BeginExecuteReader" />.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="asyncResult" /> parameter is null (Nothing in Microsoft Visual Basic)</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="M:System.Data.SqlClient.SqlCommand.EndExecuteReader(System.IAsyncResult)" /> was called more than once for a single command execution, or the method was mismatched against its execution method (for example, the code called <see cref="M:System.Data.SqlClient.SqlCommand.EndExecuteReader(System.IAsyncResult)" /> to complete execution of a call to <see cref="M:System.Data.SqlClient.SqlCommand.BeginExecuteXmlReader" />.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600117A RID: 4474 RVA: 0x00057ED0 File Offset: 0x000560D0
		internal SqlDataReader EndExecuteReader(IAsyncResult asyncResult)
		{
			Exception exception = ((Task)asyncResult).Exception;
			if (exception != null)
			{
				if (this.cachedAsyncState != null)
				{
					this.cachedAsyncState.ResetAsyncState();
				}
				this.ReliablePutStateObject();
				throw exception.InnerException;
			}
			this.ThrowIfReconnectionHasBeenCanceled();
			TdsParserStateObject stateObj = this._stateObj;
			SqlDataReader sqlDataReader;
			lock (stateObj)
			{
				sqlDataReader = this.EndExecuteReaderInternal(asyncResult);
			}
			return sqlDataReader;
		}

		// Token: 0x0600117B RID: 4475 RVA: 0x00057F48 File Offset: 0x00056148
		private SqlDataReader EndExecuteReaderInternal(IAsyncResult asyncResult)
		{
			SqlStatistics sqlStatistics = null;
			SqlDataReader sqlDataReader;
			try
			{
				sqlStatistics = SqlStatistics.StartTimer(this.Statistics);
				sqlDataReader = this.InternalEndExecuteReader(asyncResult, "EndExecuteReader");
			}
			catch (Exception ex)
			{
				if (this.cachedAsyncState != null)
				{
					this.cachedAsyncState.ResetAsyncState();
				}
				if (ADP.IsCatchableExceptionType(ex))
				{
					this.ReliablePutStateObject();
				}
				throw;
			}
			finally
			{
				SqlStatistics.StopTimer(sqlStatistics);
			}
			return sqlDataReader;
		}

		// Token: 0x0600117C RID: 4476 RVA: 0x00057FB8 File Offset: 0x000561B8
		internal IAsyncResult BeginExecuteReader(CommandBehavior behavior, AsyncCallback callback, object stateObject)
		{
			this._pendingCancel = false;
			SqlStatistics sqlStatistics = null;
			IAsyncResult task2;
			try
			{
				sqlStatistics = SqlStatistics.StartTimer(this.Statistics);
				TaskCompletionSource<object> completion = new TaskCompletionSource<object>(stateObject);
				this.ValidateAsyncCommand();
				Task task = null;
				try
				{
					this.RunExecuteReader(behavior, RunBehavior.ReturnImmediately, true, completion, this.CommandTimeout, out task, true, "BeginExecuteReader");
				}
				catch (Exception ex)
				{
					if (!ADP.IsCatchableOrSecurityExceptionType(ex))
					{
						throw;
					}
					this.ReliablePutStateObject();
					throw;
				}
				this.cachedAsyncState.SetActiveConnectionAndResult(completion, "EndExecuteReader", this._activeConnection);
				if (task != null)
				{
					AsyncHelper.ContinueTask(task, completion, delegate
					{
						this.BeginExecuteReaderInternalReadStage(completion);
					}, null, null, null, null, null);
				}
				else
				{
					this.BeginExecuteReaderInternalReadStage(completion);
				}
				if (callback != null)
				{
					completion.Task.ContinueWith(delegate(Task<object> t)
					{
						callback(t);
					}, TaskScheduler.Default);
				}
				task2 = completion.Task;
			}
			finally
			{
				SqlStatistics.StopTimer(sqlStatistics);
			}
			return task2;
		}

		// Token: 0x0600117D RID: 4477 RVA: 0x000580F4 File Offset: 0x000562F4
		private void BeginExecuteReaderInternalReadStage(TaskCompletionSource<object> completion)
		{
			try
			{
				this._stateObj.ReadSni(completion);
			}
			catch (Exception ex)
			{
				if (this._cachedAsyncState != null)
				{
					this._cachedAsyncState.ResetAsyncState();
				}
				this.ReliablePutStateObject();
				completion.TrySetException(ex);
			}
		}

		// Token: 0x0600117E RID: 4478 RVA: 0x00058144 File Offset: 0x00056344
		private SqlDataReader InternalEndExecuteReader(IAsyncResult asyncResult, string endMethod)
		{
			this.VerifyEndExecuteState((Task)asyncResult, endMethod);
			this.WaitForAsyncResults(asyncResult);
			this.CheckThrowSNIException();
			return this.CompleteAsyncExecuteReader();
		}

		/// <summary>An asynchronous version of <see cref="M:System.Data.SqlClient.SqlCommand.ExecuteNonQuery" />, which executes a Transact-SQL statement against the connection and returns the number of rows affected. The cancellation token can be used to request that the operation be abandoned before the command timeout elapses.  Exceptions will be reported via the returned Task object.</summary>
		/// <returns>A task representing the asynchronous operation.</returns>
		/// <param name="cancellationToken">The cancellation instruction.</param>
		/// <exception cref="T:System.InvalidCastException">A <see cref="P:System.Data.SqlClient.SqlParameter.SqlDbType" /> other than Binary or VarBinary was used when <see cref="P:System.Data.SqlClient.SqlParameter.Value" /> was set to <see cref="T:System.IO.Stream" />. For more information about streaming, see SqlClient Streaming Support.A <see cref="P:System.Data.SqlClient.SqlParameter.SqlDbType" /> other than Char, NChar, NVarChar, VarChar, or  Xml was used when <see cref="P:System.Data.SqlClient.SqlParameter.Value" /> was set to <see cref="T:System.IO.TextReader" />.A <see cref="P:System.Data.SqlClient.SqlParameter.SqlDbType" /> other than Xml was used when <see cref="P:System.Data.SqlClient.SqlParameter.Value" /> was set to <see cref="T:System.Xml.XmlReader" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">Calling <see cref="M:System.Data.SqlClient.SqlCommand.ExecuteNonQueryAsync(System.Threading.CancellationToken)" /> more than once for the same instance before task completion.The <see cref="T:System.Data.SqlClient.SqlConnection" /> closed or dropped during a streaming operation. For more information about streaming, see SqlClient Streaming Support.Context Connection=true is specified in the connection string.</exception>
		/// <exception cref="T:System.Data.SqlClient.SqlException">SQL Server returned an error while executing the command text.A timeout occurred during a streaming operation. For more information about streaming, see SqlClient Streaming Support.</exception>
		/// <exception cref="T:System.IO.IOException">An error occurred in a <see cref="T:System.IO.Stream" />, <see cref="T:System.Xml.XmlReader" /> or <see cref="T:System.IO.TextReader" /> object during a streaming operation.  For more information about streaming, see SqlClient Streaming Support.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.Stream" />, <see cref="T:System.Xml.XmlReader" /> or <see cref="T:System.IO.TextReader" /> object was closed during a streaming operation.  For more information about streaming, see SqlClient Streaming Support.</exception>
		// Token: 0x0600117F RID: 4479 RVA: 0x00058168 File Offset: 0x00056368
		public override Task<int> ExecuteNonQueryAsync(CancellationToken cancellationToken)
		{
			Guid operationId = SqlCommand._diagnosticListener.WriteCommandBefore(this, "ExecuteNonQueryAsync");
			TaskCompletionSource<int> source = new TaskCompletionSource<int>();
			CancellationTokenRegistration registration = default(CancellationTokenRegistration);
			if (cancellationToken.CanBeCanceled)
			{
				if (cancellationToken.IsCancellationRequested)
				{
					source.SetCanceled();
					return source.Task;
				}
				registration = cancellationToken.Register(delegate(object s)
				{
					((SqlCommand)s).CancelIgnoreFailure();
				}, this);
			}
			Task<int> task = source.Task;
			try
			{
				this.RegisterForConnectionCloseNotification<int>(ref task);
				Task<int>.Factory.FromAsync(new Func<AsyncCallback, object, IAsyncResult>(this.BeginExecuteNonQuery), new Func<IAsyncResult, int>(this.EndExecuteNonQuery), null).ContinueWith(delegate(Task<int> t)
				{
					registration.Dispose();
					if (t.IsFaulted)
					{
						Exception innerException = t.Exception.InnerException;
						SqlCommand._diagnosticListener.WriteCommandError(operationId, this, innerException, "ExecuteNonQueryAsync");
						source.SetException(innerException);
						return;
					}
					if (t.IsCanceled)
					{
						source.SetCanceled();
					}
					else
					{
						source.SetResult(t.Result);
					}
					SqlCommand._diagnosticListener.WriteCommandAfter(operationId, this, "ExecuteNonQueryAsync");
				}, TaskScheduler.Default);
			}
			catch (Exception ex)
			{
				SqlCommand._diagnosticListener.WriteCommandError(operationId, this, ex, "ExecuteNonQueryAsync");
				source.SetException(ex);
			}
			return task;
		}

		// Token: 0x06001180 RID: 4480 RVA: 0x0005828C File Offset: 0x0005648C
		protected override Task<DbDataReader> ExecuteDbDataReaderAsync(CommandBehavior behavior, CancellationToken cancellationToken)
		{
			return this.ExecuteReaderAsync(behavior, cancellationToken).ContinueWith<DbDataReader>(delegate(Task<SqlDataReader> result)
			{
				if (result.IsFaulted)
				{
					throw result.Exception.InnerException;
				}
				return result.Result;
			}, CancellationToken.None, TaskContinuationOptions.NotOnCanceled | TaskContinuationOptions.ExecuteSynchronously, TaskScheduler.Default);
		}

		/// <summary>An asynchronous version of <see cref="M:System.Data.SqlClient.SqlCommand.ExecuteReader" />, which sends the <see cref="P:System.Data.SqlClient.SqlCommand.CommandText" /> to the <see cref="P:System.Data.SqlClient.SqlCommand.Connection" /> and builds a <see cref="T:System.Data.SqlClient.SqlDataReader" />. Exceptions will be reported via the returned Task object.</summary>
		/// <returns>A task representing the asynchronous operation.</returns>
		/// <exception cref="T:System.InvalidCastException">A <see cref="P:System.Data.SqlClient.SqlParameter.SqlDbType" /> other than Binary or VarBinary was used when <see cref="P:System.Data.SqlClient.SqlParameter.Value" /> was set to <see cref="T:System.IO.Stream" />. For more information about streaming, see SqlClient Streaming Support.A <see cref="P:System.Data.SqlClient.SqlParameter.SqlDbType" /> other than Char, NChar, NVarChar, VarChar, or  Xml was used when <see cref="P:System.Data.SqlClient.SqlParameter.Value" /> was set to <see cref="T:System.IO.TextReader" />.A <see cref="P:System.Data.SqlClient.SqlParameter.SqlDbType" /> other than Xml was used when <see cref="P:System.Data.SqlClient.SqlParameter.Value" /> was set to <see cref="T:System.Xml.XmlReader" />.</exception>
		/// <exception cref="T:System.ArgumentException">An invalid <see cref="T:System.Data.CommandBehavior" /> value.</exception>
		/// <exception cref="T:System.InvalidOperationException">Calling <see cref="M:System.Data.SqlClient.SqlCommand.ExecuteReaderAsync" /> more than once for the same instance before task completion.The <see cref="T:System.Data.SqlClient.SqlConnection" /> closed or dropped during a streaming operation. For more information about streaming, see SqlClient Streaming Support.Context Connection=true is specified in the connection string.</exception>
		/// <exception cref="T:System.Data.SqlClient.SqlException">SQL Server returned an error while executing the command text.A timeout occurred during a streaming operation. For more information about streaming, see SqlClient Streaming Support.</exception>
		/// <exception cref="T:System.IO.IOException">An error occurred in a <see cref="T:System.IO.Stream" />, <see cref="T:System.Xml.XmlReader" /> or <see cref="T:System.IO.TextReader" /> object during a streaming operation.  For more information about streaming, see SqlClient Streaming Support.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.Stream" />, <see cref="T:System.Xml.XmlReader" /> or <see cref="T:System.IO.TextReader" /> object was closed during a streaming operation.  For more information about streaming, see SqlClient Streaming Support.</exception>
		// Token: 0x06001181 RID: 4481 RVA: 0x000582C9 File Offset: 0x000564C9
		public new Task<SqlDataReader> ExecuteReaderAsync()
		{
			return this.ExecuteReaderAsync(CommandBehavior.Default, CancellationToken.None);
		}

		/// <summary>An asynchronous version of <see cref="M:System.Data.SqlClient.SqlCommand.ExecuteReader(System.Data.CommandBehavior)" />, which sends the <see cref="P:System.Data.SqlClient.SqlCommand.CommandText" /> to the <see cref="P:System.Data.SqlClient.SqlCommand.Connection" />, and builds a <see cref="T:System.Data.SqlClient.SqlDataReader" />. Exceptions will be reported via the returned Task object.</summary>
		/// <returns>A task representing the asynchronous operation.</returns>
		/// <param name="behavior">Options for statement execution and data retrieval.  When is set to Default, <see cref="M:System.Data.SqlClient.SqlDataReader.ReadAsync(System.Threading.CancellationToken)" /> reads the entire row before returning a complete Task.</param>
		/// <exception cref="T:System.InvalidCastException">A <see cref="P:System.Data.SqlClient.SqlParameter.SqlDbType" /> other than Binary or VarBinary was used when <see cref="P:System.Data.SqlClient.SqlParameter.Value" /> was set to <see cref="T:System.IO.Stream" />. For more information about streaming, see SqlClient Streaming Support.A <see cref="P:System.Data.SqlClient.SqlParameter.SqlDbType" /> other than Char, NChar, NVarChar, VarChar, or  Xml was used when <see cref="P:System.Data.SqlClient.SqlParameter.Value" /> was set to <see cref="T:System.IO.TextReader" />.A <see cref="P:System.Data.SqlClient.SqlParameter.SqlDbType" /> other than Xml was used when <see cref="P:System.Data.SqlClient.SqlParameter.Value" /> was set to <see cref="T:System.Xml.XmlReader" />.</exception>
		/// <exception cref="T:System.ArgumentException">An invalid <see cref="T:System.Data.CommandBehavior" /> value.</exception>
		/// <exception cref="T:System.InvalidOperationException">Calling <see cref="M:System.Data.SqlClient.SqlCommand.ExecuteReaderAsync(System.Data.CommandBehavior)" /> more than once for the same instance before task completion.The <see cref="T:System.Data.SqlClient.SqlConnection" /> closed or dropped during a streaming operation. For more information about streaming, see SqlClient Streaming Support.Context Connection=true is specified in the connection string.</exception>
		/// <exception cref="T:System.Data.SqlClient.SqlException">SQL Server returned an error while executing the command text.A timeout occurred during a streaming operation. For more information about streaming, see SqlClient Streaming Support.</exception>
		/// <exception cref="T:System.IO.IOException">An error occurred in a <see cref="T:System.IO.Stream" />, <see cref="T:System.Xml.XmlReader" /> or <see cref="T:System.IO.TextReader" /> object during a streaming operation.  For more information about streaming, see SqlClient Streaming Support.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.Stream" />, <see cref="T:System.Xml.XmlReader" /> or <see cref="T:System.IO.TextReader" /> object was closed during a streaming operation.  For more information about streaming, see SqlClient Streaming Support.</exception>
		// Token: 0x06001182 RID: 4482 RVA: 0x000582D7 File Offset: 0x000564D7
		public new Task<SqlDataReader> ExecuteReaderAsync(CommandBehavior behavior)
		{
			return this.ExecuteReaderAsync(behavior, CancellationToken.None);
		}

		/// <summary>An asynchronous version of <see cref="M:System.Data.SqlClient.SqlCommand.ExecuteReader" />, which sends the <see cref="P:System.Data.SqlClient.SqlCommand.CommandText" /> to the <see cref="P:System.Data.SqlClient.SqlCommand.Connection" /> and builds a <see cref="T:System.Data.SqlClient.SqlDataReader" />.The cancellation token can be used to request that the operation be abandoned before the command timeout elapses.  Exceptions will be reported via the returned Task object.</summary>
		/// <returns>A task representing the asynchronous operation.</returns>
		/// <param name="cancellationToken">The cancellation instruction.</param>
		/// <exception cref="T:System.InvalidCastException">A <see cref="P:System.Data.SqlClient.SqlParameter.SqlDbType" /> other than Binary or VarBinary was used when <see cref="P:System.Data.SqlClient.SqlParameter.Value" /> was set to <see cref="T:System.IO.Stream" />. For more information about streaming, see SqlClient Streaming Support.A <see cref="P:System.Data.SqlClient.SqlParameter.SqlDbType" /> other than Char, NChar, NVarChar, VarChar, or  Xml was used when <see cref="P:System.Data.SqlClient.SqlParameter.Value" /> was set to <see cref="T:System.IO.TextReader" />.A <see cref="P:System.Data.SqlClient.SqlParameter.SqlDbType" /> other than Xml was used when <see cref="P:System.Data.SqlClient.SqlParameter.Value" /> was set to <see cref="T:System.Xml.XmlReader" />.</exception>
		/// <exception cref="T:System.ArgumentException">An invalid <see cref="T:System.Data.CommandBehavior" /> value.</exception>
		/// <exception cref="T:System.InvalidOperationException">Calling <see cref="M:System.Data.SqlClient.SqlCommand.ExecuteReaderAsync(System.Data.CommandBehavior,System.Threading.CancellationToken)" /> more than once for the same instance before task completion.The <see cref="T:System.Data.SqlClient.SqlConnection" /> closed or dropped during a streaming operation. For more information about streaming, see SqlClient Streaming Support.Context Connection=true is specified in the connection string.</exception>
		/// <exception cref="T:System.Data.SqlClient.SqlException">SQL Server returned an error while executing the command text.A timeout occurred during a streaming operation. For more information about streaming, see SqlClient Streaming Support.</exception>
		/// <exception cref="T:System.IO.IOException">An error occurred in a <see cref="T:System.IO.Stream" />, <see cref="T:System.Xml.XmlReader" /> or <see cref="T:System.IO.TextReader" /> object during a streaming operation.  For more information about streaming, see SqlClient Streaming Support.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.Stream" />, <see cref="T:System.Xml.XmlReader" /> or <see cref="T:System.IO.TextReader" /> object was closed during a streaming operation.  For more information about streaming, see SqlClient Streaming Support.</exception>
		// Token: 0x06001183 RID: 4483 RVA: 0x000582E5 File Offset: 0x000564E5
		public new Task<SqlDataReader> ExecuteReaderAsync(CancellationToken cancellationToken)
		{
			return this.ExecuteReaderAsync(CommandBehavior.Default, cancellationToken);
		}

		/// <summary>An asynchronous version of <see cref="M:System.Data.SqlClient.SqlCommand.ExecuteReader(System.Data.CommandBehavior)" />, which sends the <see cref="P:System.Data.SqlClient.SqlCommand.CommandText" /> to the <see cref="P:System.Data.SqlClient.SqlCommand.Connection" />, and builds a <see cref="T:System.Data.SqlClient.SqlDataReader" />The cancellation token can be used to request that the operation be abandoned before the command timeout elapses.  Exceptions will be reported via the returned Task object.</summary>
		/// <returns>A task representing the asynchronous operation.</returns>
		/// <param name="behavior">Options for statement execution and data retrieval.  When is set to Default, <see cref="M:System.Data.SqlClient.SqlDataReader.ReadAsync(System.Threading.CancellationToken)" /> reads the entire row before returning a complete Task.</param>
		/// <param name="cancellationToken">The cancellation instruction.</param>
		/// <exception cref="T:System.InvalidCastException">A <see cref="P:System.Data.SqlClient.SqlParameter.SqlDbType" /> other than Binary or VarBinary was used when <see cref="P:System.Data.SqlClient.SqlParameter.Value" /> was set to <see cref="T:System.IO.Stream" />. For more information about streaming, see SqlClient Streaming Support.A <see cref="P:System.Data.SqlClient.SqlParameter.SqlDbType" /> other than Char, NChar, NVarChar, VarChar, or  Xml was used when <see cref="P:System.Data.SqlClient.SqlParameter.Value" /> was set to <see cref="T:System.IO.TextReader" />.A <see cref="P:System.Data.SqlClient.SqlParameter.SqlDbType" /> other than Xml was used when <see cref="P:System.Data.SqlClient.SqlParameter.Value" /> was set to <see cref="T:System.Xml.XmlReader" />.</exception>
		/// <exception cref="T:System.ArgumentException">An invalid <see cref="T:System.Data.CommandBehavior" /> value.</exception>
		/// <exception cref="T:System.InvalidOperationException">Calling <see cref="M:System.Data.SqlClient.SqlCommand.ExecuteReaderAsync(System.Data.CommandBehavior,System.Threading.CancellationToken)" /> more than once for the same instance before task completion.The <see cref="T:System.Data.SqlClient.SqlConnection" /> closed or dropped during a streaming operation. For more information about streaming, see SqlClient Streaming Support.Context Connection=true is specified in the connection string.</exception>
		/// <exception cref="T:System.Data.SqlClient.SqlException">SQL Server returned an error while executing the command text.A timeout occurred during a streaming operation. For more information about streaming, see SqlClient Streaming Support.</exception>
		/// <exception cref="T:System.IO.IOException">An error occurred in a <see cref="T:System.IO.Stream" />, <see cref="T:System.Xml.XmlReader" /> or <see cref="T:System.IO.TextReader" /> object during a streaming operation.  For more information about streaming, see SqlClient Streaming Support.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.Stream" />, <see cref="T:System.Xml.XmlReader" /> or <see cref="T:System.IO.TextReader" /> object was closed during a streaming operation.  For more information about streaming, see SqlClient Streaming Support.</exception>
		// Token: 0x06001184 RID: 4484 RVA: 0x000582F0 File Offset: 0x000564F0
		public new Task<SqlDataReader> ExecuteReaderAsync(CommandBehavior behavior, CancellationToken cancellationToken)
		{
			Guid operationId = default(Guid);
			if (!this._parentOperationStarted)
			{
				operationId = SqlCommand._diagnosticListener.WriteCommandBefore(this, "ExecuteReaderAsync");
			}
			TaskCompletionSource<SqlDataReader> source = new TaskCompletionSource<SqlDataReader>();
			CancellationTokenRegistration registration = default(CancellationTokenRegistration);
			if (cancellationToken.CanBeCanceled)
			{
				if (cancellationToken.IsCancellationRequested)
				{
					source.SetCanceled();
					return source.Task;
				}
				registration = cancellationToken.Register(delegate(object s)
				{
					((SqlCommand)s).CancelIgnoreFailure();
				}, this);
			}
			Task<SqlDataReader> task = source.Task;
			try
			{
				this.RegisterForConnectionCloseNotification<SqlDataReader>(ref task);
				Task<SqlDataReader>.Factory.FromAsync<CommandBehavior>(new Func<CommandBehavior, AsyncCallback, object, IAsyncResult>(this.BeginExecuteReader), new Func<IAsyncResult, SqlDataReader>(this.EndExecuteReader), behavior, null).ContinueWith(delegate(Task<SqlDataReader> t)
				{
					registration.Dispose();
					if (t.IsFaulted)
					{
						Exception innerException = t.Exception.InnerException;
						if (!this._parentOperationStarted)
						{
							SqlCommand._diagnosticListener.WriteCommandError(operationId, this, innerException, "ExecuteReaderAsync");
						}
						source.SetException(innerException);
						return;
					}
					if (t.IsCanceled)
					{
						source.SetCanceled();
					}
					else
					{
						source.SetResult(t.Result);
					}
					if (!this._parentOperationStarted)
					{
						SqlCommand._diagnosticListener.WriteCommandAfter(operationId, this, "ExecuteReaderAsync");
					}
				}, TaskScheduler.Default);
			}
			catch (Exception ex)
			{
				if (!this._parentOperationStarted)
				{
					SqlCommand._diagnosticListener.WriteCommandError(operationId, this, ex, "ExecuteReaderAsync");
				}
				source.SetException(ex);
			}
			return task;
		}

		/// <summary>An asynchronous version of <see cref="M:System.Data.SqlClient.SqlCommand.ExecuteScalar" />, which executes the query asynchronously and returns the first column of the first row in the result set returned by the query. Additional columns or rows are ignored.The cancellation token can be used to request that the operation be abandoned before the command timeout elapses. Exceptions will be reported via the returned Task object.</summary>
		/// <returns>A task representing the asynchronous operation.</returns>
		/// <param name="cancellationToken">The cancellation instruction.</param>
		/// <exception cref="T:System.InvalidCastException">A <see cref="P:System.Data.SqlClient.SqlParameter.SqlDbType" /> other than Binary or VarBinary was used when <see cref="P:System.Data.SqlClient.SqlParameter.Value" /> was set to <see cref="T:System.IO.Stream" />. For more information about streaming, see SqlClient Streaming Support.A <see cref="P:System.Data.SqlClient.SqlParameter.SqlDbType" /> other than Char, NChar, NVarChar, VarChar, or  Xml was used when <see cref="P:System.Data.SqlClient.SqlParameter.Value" /> was set to <see cref="T:System.IO.TextReader" />.A <see cref="P:System.Data.SqlClient.SqlParameter.SqlDbType" /> other than Xml was used when <see cref="P:System.Data.SqlClient.SqlParameter.Value" /> was set to <see cref="T:System.Xml.XmlReader" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">Calling <see cref="M:System.Data.SqlClient.SqlCommand.ExecuteScalarAsync(System.Threading.CancellationToken)" /> more than once for the same instance before task completion.The <see cref="T:System.Data.SqlClient.SqlConnection" /> closed or dropped during a streaming operation. For more information about streaming, see SqlClient Streaming Support.Context Connection=true is specified in the connection string.</exception>
		/// <exception cref="T:System.Data.SqlClient.SqlException">SQL Server returned an error while executing the command text.A timeout occurred during a streaming operation. For more information about streaming, see SqlClient Streaming Support.</exception>
		/// <exception cref="T:System.IO.IOException">An error occurred in a <see cref="T:System.IO.Stream" />, <see cref="T:System.Xml.XmlReader" /> or <see cref="T:System.IO.TextReader" /> object during a streaming operation.  For more information about streaming, see SqlClient Streaming Support.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.Stream" />, <see cref="T:System.Xml.XmlReader" /> or <see cref="T:System.IO.TextReader" /> object was closed during a streaming operation.  For more information about streaming, see SqlClient Streaming Support.</exception>
		// Token: 0x06001185 RID: 4485 RVA: 0x00058434 File Offset: 0x00056634
		public override Task<object> ExecuteScalarAsync(CancellationToken cancellationToken)
		{
			this._parentOperationStarted = true;
			Guid operationId = SqlCommand._diagnosticListener.WriteCommandBefore(this, "ExecuteScalarAsync");
			return this.ExecuteReaderAsync(cancellationToken).ContinueWith<Task<object>>(delegate(Task<SqlDataReader> executeTask)
			{
				TaskCompletionSource<object> source = new TaskCompletionSource<object>();
				if (executeTask.IsCanceled)
				{
					source.SetCanceled();
				}
				else if (executeTask.IsFaulted)
				{
					SqlCommand._diagnosticListener.WriteCommandError(operationId, this, executeTask.Exception.InnerException, "ExecuteScalarAsync");
					source.SetException(executeTask.Exception.InnerException);
				}
				else
				{
					SqlDataReader reader = executeTask.Result;
					reader.ReadAsync(cancellationToken).ContinueWith(delegate(Task<bool> readTask)
					{
						try
						{
							if (readTask.IsCanceled)
							{
								reader.Dispose();
								source.SetCanceled();
							}
							else if (readTask.IsFaulted)
							{
								reader.Dispose();
								SqlCommand._diagnosticListener.WriteCommandError(operationId, this, readTask.Exception.InnerException, "ExecuteScalarAsync");
								source.SetException(readTask.Exception.InnerException);
							}
							else
							{
								Exception ex = null;
								object obj = null;
								try
								{
									if (readTask.Result && reader.FieldCount > 0)
									{
										try
										{
											obj = reader.GetValue(0);
										}
										catch (Exception ex)
										{
										}
									}
								}
								finally
								{
									reader.Dispose();
								}
								if (ex != null)
								{
									SqlCommand._diagnosticListener.WriteCommandError(operationId, this, ex, "ExecuteScalarAsync");
									source.SetException(ex);
								}
								else
								{
									SqlCommand._diagnosticListener.WriteCommandAfter(operationId, this, "ExecuteScalarAsync");
									source.SetResult(obj);
								}
							}
						}
						catch (Exception ex2)
						{
							source.SetException(ex2);
						}
					}, TaskScheduler.Default);
				}
				this._parentOperationStarted = false;
				return source.Task;
			}, TaskScheduler.Default).Unwrap<object>();
		}

		/// <summary>An asynchronous version of <see cref="M:System.Data.SqlClient.SqlCommand.ExecuteXmlReader" />, which sends the <see cref="P:System.Data.SqlClient.SqlCommand.CommandText" /> to the <see cref="P:System.Data.SqlClient.SqlCommand.Connection" /> and builds an <see cref="T:System.Xml.XmlReader" /> object.Exceptions will be reported via the returned Task object.</summary>
		/// <returns>A task representing the asynchronous operation.</returns>
		/// <exception cref="T:System.InvalidCastException">A <see cref="P:System.Data.SqlClient.SqlParameter.SqlDbType" /> other than Binary or VarBinary was used when <see cref="P:System.Data.SqlClient.SqlParameter.Value" /> was set to <see cref="T:System.IO.Stream" />. For more information about streaming, see SqlClient Streaming Support.A <see cref="P:System.Data.SqlClient.SqlParameter.SqlDbType" /> other than Char, NChar, NVarChar, VarChar, or  Xml was used when <see cref="P:System.Data.SqlClient.SqlParameter.Value" /> was set to <see cref="T:System.IO.TextReader" />.A <see cref="P:System.Data.SqlClient.SqlParameter.SqlDbType" /> other than Xml was used when <see cref="P:System.Data.SqlClient.SqlParameter.Value" /> was set to <see cref="T:System.Xml.XmlReader" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">Calling <see cref="M:System.Data.SqlClient.SqlCommand.ExecuteScalarAsync(System.Threading.CancellationToken)" /> more than once for the same instance before task completion.The <see cref="T:System.Data.SqlClient.SqlConnection" /> closed or dropped during a streaming operation. For more information about streaming, see SqlClient Streaming Support.Context Connection=true is specified in the connection string.</exception>
		/// <exception cref="T:System.Data.SqlClient.SqlException">SQL Server returned an error while executing the command text.A timeout occurred during a streaming operation. For more information about streaming, see SqlClient Streaming Support.</exception>
		/// <exception cref="T:System.IO.IOException">An error occurred in a <see cref="T:System.IO.Stream" />, <see cref="T:System.Xml.XmlReader" /> or <see cref="T:System.IO.TextReader" /> object during a streaming operation.  For more information about streaming, see SqlClient Streaming Support.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.Stream" />, <see cref="T:System.Xml.XmlReader" /> or <see cref="T:System.IO.TextReader" /> object was closed during a streaming operation.  For more information about streaming, see SqlClient Streaming Support.</exception>
		// Token: 0x06001186 RID: 4486 RVA: 0x00058499 File Offset: 0x00056699
		public Task<XmlReader> ExecuteXmlReaderAsync()
		{
			return this.ExecuteXmlReaderAsync(CancellationToken.None);
		}

		/// <summary>An asynchronous version of <see cref="M:System.Data.SqlClient.SqlCommand.ExecuteXmlReader" />, which sends the <see cref="P:System.Data.SqlClient.SqlCommand.CommandText" /> to the <see cref="P:System.Data.SqlClient.SqlCommand.Connection" /> and builds an <see cref="T:System.Xml.XmlReader" /> object.The cancellation token can be used to request that the operation be abandoned before the command timeout elapses.  Exceptions will be reported via the returned Task object.</summary>
		/// <returns>A task representing the asynchronous operation.</returns>
		/// <param name="cancellationToken">The cancellation instruction.</param>
		/// <exception cref="T:System.InvalidCastException">A <see cref="P:System.Data.SqlClient.SqlParameter.SqlDbType" /> other than Binary or VarBinary was used when <see cref="P:System.Data.SqlClient.SqlParameter.Value" /> was set to <see cref="T:System.IO.Stream" />. For more information about streaming, see SqlClient Streaming Support.A <see cref="P:System.Data.SqlClient.SqlParameter.SqlDbType" /> other than Char, NChar, NVarChar, VarChar, or  Xml was used when <see cref="P:System.Data.SqlClient.SqlParameter.Value" /> was set to <see cref="T:System.IO.TextReader" />.A <see cref="P:System.Data.SqlClient.SqlParameter.SqlDbType" /> other than Xml was used when <see cref="P:System.Data.SqlClient.SqlParameter.Value" /> was set to <see cref="T:System.Xml.XmlReader" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">Calling <see cref="M:System.Data.SqlClient.SqlCommand.ExecuteScalarAsync(System.Threading.CancellationToken)" /> more than once for the same instance before task completion.The <see cref="T:System.Data.SqlClient.SqlConnection" /> closed or dropped during a streaming operation. For more information about streaming, see SqlClient Streaming Support.Context Connection=true is specified in the connection string.</exception>
		/// <exception cref="T:System.Data.SqlClient.SqlException">SQL Server returned an error while executing the command text.A timeout occurred during a streaming operation. For more information about streaming, see SqlClient Streaming Support.</exception>
		/// <exception cref="T:System.IO.IOException">An error occurred in a <see cref="T:System.IO.Stream" />, <see cref="T:System.Xml.XmlReader" /> or <see cref="T:System.IO.TextReader" /> object during a streaming operation.  For more information about streaming, see SqlClient Streaming Support.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.Stream" />, <see cref="T:System.Xml.XmlReader" /> or <see cref="T:System.IO.TextReader" /> object was closed during a streaming operation.  For more information about streaming, see SqlClient Streaming Support.</exception>
		// Token: 0x06001187 RID: 4487 RVA: 0x000584A8 File Offset: 0x000566A8
		public Task<XmlReader> ExecuteXmlReaderAsync(CancellationToken cancellationToken)
		{
			Guid operationId = SqlCommand._diagnosticListener.WriteCommandBefore(this, "ExecuteXmlReaderAsync");
			TaskCompletionSource<XmlReader> source = new TaskCompletionSource<XmlReader>();
			CancellationTokenRegistration registration = default(CancellationTokenRegistration);
			if (cancellationToken.CanBeCanceled)
			{
				if (cancellationToken.IsCancellationRequested)
				{
					source.SetCanceled();
					return source.Task;
				}
				registration = cancellationToken.Register(delegate(object s)
				{
					((SqlCommand)s).CancelIgnoreFailure();
				}, this);
			}
			Task<XmlReader> task = source.Task;
			try
			{
				this.RegisterForConnectionCloseNotification<XmlReader>(ref task);
				Task<XmlReader>.Factory.FromAsync(new Func<AsyncCallback, object, IAsyncResult>(this.BeginExecuteXmlReader), new Func<IAsyncResult, XmlReader>(this.EndExecuteXmlReader), null).ContinueWith(delegate(Task<XmlReader> t)
				{
					registration.Dispose();
					if (t.IsFaulted)
					{
						Exception innerException = t.Exception.InnerException;
						SqlCommand._diagnosticListener.WriteCommandError(operationId, this, innerException, "ExecuteXmlReaderAsync");
						source.SetException(innerException);
						return;
					}
					if (t.IsCanceled)
					{
						source.SetCanceled();
					}
					else
					{
						source.SetResult(t.Result);
					}
					SqlCommand._diagnosticListener.WriteCommandAfter(operationId, this, "ExecuteXmlReaderAsync");
				}, TaskScheduler.Default);
			}
			catch (Exception ex)
			{
				SqlCommand._diagnosticListener.WriteCommandError(operationId, this, ex, "ExecuteXmlReaderAsync");
				source.SetException(ex);
			}
			return task;
		}

		// Token: 0x06001188 RID: 4488 RVA: 0x000585CC File Offset: 0x000567CC
		private static string UnquoteProcedurePart(string part)
		{
			if (part != null && 2 <= part.Length && '[' == part[0] && ']' == part[part.Length - 1])
			{
				part = part.Substring(1, part.Length - 2);
				part = part.Replace("]]", "]");
			}
			return part;
		}

		// Token: 0x06001189 RID: 4489 RVA: 0x00058628 File Offset: 0x00056828
		private static string UnquoteProcedureName(string name, out object groupNumber)
		{
			groupNumber = null;
			string text = name;
			if (text != null)
			{
				if (char.IsDigit(text[text.Length - 1]))
				{
					int num = text.LastIndexOf(';');
					if (num != -1)
					{
						string text2 = text.Substring(num + 1);
						int num2 = 0;
						if (int.TryParse(text2, out num2))
						{
							groupNumber = num2;
							text = text.Substring(0, num);
						}
					}
				}
				text = SqlCommand.UnquoteProcedurePart(text);
			}
			return text;
		}

		// Token: 0x0600118A RID: 4490 RVA: 0x0005868C File Offset: 0x0005688C
		internal void DeriveParameters()
		{
			CommandType commandType = this.CommandType;
			if (commandType == CommandType.Text)
			{
				throw ADP.DeriveParametersNotSupported(this);
			}
			if (commandType != CommandType.StoredProcedure)
			{
				if (commandType != CommandType.TableDirect)
				{
					throw ADP.InvalidCommandType(this.CommandType);
				}
				throw ADP.DeriveParametersNotSupported(this);
			}
			else
			{
				this.ValidateCommand(false, "DeriveParameters");
				string[] array = MultipartIdentifier.ParseMultipartIdentifier(this.CommandText, "[\"", "]\"", "SqlCommand.DeriveParameters failed because the SqlCommand.CommandText property value is an invalid multipart name", false);
				if (array[3] == null || string.IsNullOrEmpty(array[3]))
				{
					throw ADP.NoStoredProcedureExists(this.CommandText);
				}
				SqlCommand sqlCommand = null;
				StringBuilder stringBuilder = new StringBuilder();
				if (!string.IsNullOrEmpty(array[0]))
				{
					SqlCommandSet.BuildStoredProcedureName(stringBuilder, array[0]);
					stringBuilder.Append(".");
				}
				if (string.IsNullOrEmpty(array[1]))
				{
					array[1] = this.Connection.Database;
				}
				SqlCommandSet.BuildStoredProcedureName(stringBuilder, array[1]);
				stringBuilder.Append(".");
				string[] array2;
				bool flag;
				if (this.Connection.IsKatmaiOrNewer)
				{
					stringBuilder.Append("[sys].[").Append("sp_procedure_params_100_managed").Append("]");
					array2 = SqlCommand.KatmaiProcParamsNames;
					flag = true;
				}
				else
				{
					stringBuilder.Append("[sys].[").Append("sp_procedure_params_managed").Append("]");
					array2 = SqlCommand.PreKatmaiProcParamsNames;
					flag = false;
				}
				sqlCommand = new SqlCommand(stringBuilder.ToString(), this.Connection, this.Transaction)
				{
					CommandType = CommandType.StoredProcedure
				};
				sqlCommand.Parameters.Add(new SqlParameter("@procedure_name", SqlDbType.NVarChar, 255));
				object obj;
				sqlCommand.Parameters[0].Value = SqlCommand.UnquoteProcedureName(array[3], out obj);
				if (obj != null)
				{
					sqlCommand.Parameters.Add(new SqlParameter("@group_number", SqlDbType.Int)).Value = obj;
				}
				if (!string.IsNullOrEmpty(array[2]))
				{
					sqlCommand.Parameters.Add(new SqlParameter("@procedure_schema", SqlDbType.NVarChar, 255)).Value = SqlCommand.UnquoteProcedurePart(array[2]);
				}
				SqlDataReader sqlDataReader = null;
				List<SqlParameter> list = new List<SqlParameter>();
				bool flag2 = true;
				try
				{
					sqlDataReader = sqlCommand.ExecuteReader();
					while (sqlDataReader.Read())
					{
						SqlParameter sqlParameter = new SqlParameter
						{
							ParameterName = (string)sqlDataReader[array2[0]]
						};
						if (flag)
						{
							sqlParameter.SqlDbType = (SqlDbType)((short)sqlDataReader[array2[3]]);
							SqlDbType sqlDbType = sqlParameter.SqlDbType;
							if (sqlDbType <= SqlDbType.NText)
							{
								if (sqlDbType != SqlDbType.Image)
								{
									if (sqlDbType != SqlDbType.NText)
									{
										goto IL_02BA;
									}
									sqlParameter.SqlDbType = SqlDbType.NVarChar;
									goto IL_02BA;
								}
							}
							else
							{
								if (sqlDbType == SqlDbType.Text)
								{
									sqlParameter.SqlDbType = SqlDbType.VarChar;
									goto IL_02BA;
								}
								if (sqlDbType != SqlDbType.Timestamp)
								{
									goto IL_02BA;
								}
							}
							sqlParameter.SqlDbType = SqlDbType.VarBinary;
						}
						else
						{
							sqlParameter.SqlDbType = MetaType.GetSqlDbTypeFromOleDbType((short)sqlDataReader[array2[2]], ADP.IsNull(sqlDataReader[array2[9]]) ? ADP.StrEmpty : ((string)sqlDataReader[array2[9]]));
						}
						IL_02BA:
						object obj2 = sqlDataReader[array2[4]];
						if (obj2 is int)
						{
							int num = (int)obj2;
							if (num == 0 && (sqlParameter.SqlDbType == SqlDbType.NVarChar || sqlParameter.SqlDbType == SqlDbType.VarBinary || sqlParameter.SqlDbType == SqlDbType.VarChar))
							{
								num = -1;
							}
							sqlParameter.Size = num;
						}
						sqlParameter.Direction = this.ParameterDirectionFromOleDbDirection((short)sqlDataReader[array2[1]]);
						if (sqlParameter.SqlDbType == SqlDbType.Decimal)
						{
							sqlParameter.ScaleInternal = (byte)((short)sqlDataReader[array2[6]] & 255);
							sqlParameter.PrecisionInternal = (byte)((short)sqlDataReader[array2[5]] & 255);
						}
						if (SqlDbType.Structured == sqlParameter.SqlDbType)
						{
							sqlParameter.TypeName = string.Concat(new object[]
							{
								sqlDataReader[array2[7]],
								".",
								sqlDataReader[array2[8]],
								".",
								sqlDataReader[array2[9]]
							});
						}
						if (SqlDbType.Xml == sqlParameter.SqlDbType)
						{
							object obj3 = sqlDataReader[array2[10]];
							sqlParameter.XmlSchemaCollectionDatabase = (ADP.IsNull(obj3) ? string.Empty : ((string)obj3));
							obj3 = sqlDataReader[array2[11]];
							sqlParameter.XmlSchemaCollectionOwningSchema = (ADP.IsNull(obj3) ? string.Empty : ((string)obj3));
							obj3 = sqlDataReader[array2[12]];
							sqlParameter.XmlSchemaCollectionName = (ADP.IsNull(obj3) ? string.Empty : ((string)obj3));
						}
						if (MetaType._IsVarTime(sqlParameter.SqlDbType))
						{
							object obj4 = sqlDataReader[array2[14]];
							if (obj4 is int)
							{
								sqlParameter.ScaleInternal = (byte)((int)obj4 & 255);
							}
						}
						list.Add(sqlParameter);
					}
				}
				catch (Exception ex)
				{
					flag2 = ADP.IsCatchableExceptionType(ex);
					throw;
				}
				finally
				{
					if (flag2)
					{
						if (sqlDataReader != null)
						{
							sqlDataReader.Close();
						}
						sqlCommand.Connection = null;
					}
				}
				if (list.Count == 0)
				{
					throw ADP.NoStoredProcedureExists(this.CommandText);
				}
				this.Parameters.Clear();
				foreach (SqlParameter sqlParameter2 in list)
				{
					this._parameters.Add(sqlParameter2);
				}
				return;
			}
		}

		// Token: 0x0600118B RID: 4491 RVA: 0x00058C00 File Offset: 0x00056E00
		private ParameterDirection ParameterDirectionFromOleDbDirection(short oledbDirection)
		{
			switch (oledbDirection)
			{
			case 2:
				return ParameterDirection.InputOutput;
			case 3:
				return ParameterDirection.Output;
			case 4:
				return ParameterDirection.ReturnValue;
			default:
				return ParameterDirection.Input;
			}
		}

		// Token: 0x17000314 RID: 788
		// (get) Token: 0x0600118C RID: 4492 RVA: 0x00058C1F File Offset: 0x00056E1F
		internal _SqlMetaDataSet MetaData
		{
			get
			{
				return this._cachedMetaData;
			}
		}

		// Token: 0x0600118D RID: 4493 RVA: 0x00058C28 File Offset: 0x00056E28
		private void CheckNotificationStateAndAutoEnlist()
		{
			if (this.Notification != null && this._sqlDep != null)
			{
				if (this._sqlDep.Options == null)
				{
					SqlInternalConnectionTds sqlInternalConnectionTds = this._activeConnection.InnerConnection as SqlInternalConnectionTds;
					SqlDependency.IdentityUserNamePair identityUserNamePair;
					if (sqlInternalConnectionTds.Identity != null)
					{
						identityUserNamePair = new SqlDependency.IdentityUserNamePair(sqlInternalConnectionTds.Identity, null);
					}
					else
					{
						identityUserNamePair = new SqlDependency.IdentityUserNamePair(null, sqlInternalConnectionTds.ConnectionOptions.UserID);
					}
					this.Notification.Options = SqlDependency.GetDefaultComposedOptions(this._activeConnection.DataSource, this.InternalTdsConnection.ServerProvidedFailOverPartner, identityUserNamePair, this._activeConnection.Database);
				}
				this.Notification.UserData = this._sqlDep.ComputeHashAndAddToDispatcher(this);
				this._sqlDep.AddToServerList(this._activeConnection.DataSource);
			}
		}

		// Token: 0x0600118E RID: 4494 RVA: 0x00058CF4 File Offset: 0x00056EF4
		private Task RunExecuteNonQueryTds(string methodName, bool async, int timeout, bool asyncWrite)
		{
			bool flag = true;
			try
			{
				Task task = this._activeConnection.ValidateAndReconnect(null, timeout);
				if (task != null)
				{
					long reconnectionStart = ADP.TimerCurrent();
					if (async)
					{
						TaskCompletionSource<object> completion = new TaskCompletionSource<object>();
						this._activeConnection.RegisterWaitingForReconnect(completion.Task);
						this._reconnectionCompletionSource = completion;
						CancellationTokenSource timeoutCTS = new CancellationTokenSource();
						AsyncHelper.SetTimeoutException(completion, timeout, new Func<Exception>(SQL.CR_ReconnectTimeout), timeoutCTS.Token);
						Action <>9__2;
						AsyncHelper.ContinueTask(task, completion, delegate
						{
							if (completion.Task.IsCompleted)
							{
								return;
							}
							Interlocked.CompareExchange<TaskCompletionSource<object>>(ref this._reconnectionCompletionSource, null, completion);
							timeoutCTS.Cancel();
							Task task2 = this.RunExecuteNonQueryTds(methodName, async, TdsParserStaticMethods.GetRemainingTimeout(timeout, reconnectionStart), asyncWrite);
							if (task2 == null)
							{
								completion.SetResult(null);
								return;
							}
							Task task3 = task2;
							TaskCompletionSource<object> completion2 = completion;
							Action action;
							if ((action = <>9__2) == null)
							{
								action = (<>9__2 = delegate
								{
									completion.SetResult(null);
								});
							}
							AsyncHelper.ContinueTask(task3, completion2, action, null, null, null, null, null);
						}, null, null, null, null, this._activeConnection);
						return completion.Task;
					}
					AsyncHelper.WaitForCompletion(task, timeout, delegate
					{
						throw SQL.CR_ReconnectTimeout();
					}, true);
					timeout = TdsParserStaticMethods.GetRemainingTimeout(timeout, reconnectionStart);
				}
				if (asyncWrite)
				{
					this._activeConnection.AddWeakReference(this, 2);
				}
				this.GetStateObject(null);
				this._stateObj.Parser.TdsExecuteSQLBatch(this.CommandText, timeout, this.Notification, this._stateObj, true, false);
				this.NotifyDependency();
				bool flag2;
				if (async)
				{
					this._activeConnection.GetOpenTdsConnection(methodName).IncrementAsyncCount();
				}
				else if (!this._stateObj.Parser.TryRun(RunBehavior.UntilDone, this, null, null, this._stateObj, out flag2))
				{
					throw SQL.SynchronousCallMayNotPend();
				}
			}
			catch (Exception ex)
			{
				flag = ADP.IsCatchableExceptionType(ex);
				throw;
			}
			finally
			{
				if (flag && !async)
				{
					this.PutStateObject();
				}
			}
			return null;
		}

		// Token: 0x0600118F RID: 4495 RVA: 0x00058F5C File Offset: 0x0005715C
		internal SqlDataReader RunExecuteReader(CommandBehavior cmdBehavior, RunBehavior runBehavior, bool returnStream, [CallerMemberName] string method = "")
		{
			Task task;
			return this.RunExecuteReader(cmdBehavior, runBehavior, returnStream, null, this.CommandTimeout, out task, false, method);
		}

		// Token: 0x06001190 RID: 4496 RVA: 0x00058F80 File Offset: 0x00057180
		internal SqlDataReader RunExecuteReader(CommandBehavior cmdBehavior, RunBehavior runBehavior, bool returnStream, TaskCompletionSource<object> completion, int timeout, out Task task, bool asyncWrite = false, [CallerMemberName] string method = "")
		{
			bool flag = completion != null;
			task = null;
			this._rowsAffected = -1;
			if ((CommandBehavior.SingleRow & cmdBehavior) != CommandBehavior.Default)
			{
				cmdBehavior |= CommandBehavior.SingleResult;
			}
			this.ValidateCommand(flag, method);
			this.CheckNotificationStateAndAutoEnlist();
			SqlStatistics statistics = this.Statistics;
			if (statistics != null)
			{
				if ((!this.IsDirty && this.IsPrepared && !this._hiddenPrepare) || (this.IsPrepared && this._execType == SqlCommand.EXECTYPE.PREPAREPENDING))
				{
					statistics.SafeIncrement(ref statistics._preparedExecs);
				}
				else
				{
					statistics.SafeIncrement(ref statistics._unpreparedExecs);
				}
			}
			return this.RunExecuteReaderTds(cmdBehavior, runBehavior, returnStream, flag, timeout, out task, asyncWrite && flag, null);
		}

		// Token: 0x06001191 RID: 4497 RVA: 0x0005901C File Offset: 0x0005721C
		private SqlDataReader RunExecuteReaderTds(CommandBehavior cmdBehavior, RunBehavior runBehavior, bool returnStream, bool async, int timeout, out Task task, bool asyncWrite, SqlDataReader ds = null)
		{
			if ((ds == null) & returnStream)
			{
				ds = new SqlDataReader(this, cmdBehavior);
			}
			Task task2 = this._activeConnection.ValidateAndReconnect(null, timeout);
			if (task2 != null)
			{
				long reconnectionStart = ADP.TimerCurrent();
				if (async)
				{
					TaskCompletionSource<object> completion = new TaskCompletionSource<object>();
					this._activeConnection.RegisterWaitingForReconnect(completion.Task);
					this._reconnectionCompletionSource = completion;
					CancellationTokenSource timeoutCTS = new CancellationTokenSource();
					AsyncHelper.SetTimeoutException(completion, timeout, new Func<Exception>(SQL.CR_ReconnectTimeout), timeoutCTS.Token);
					Action <>9__2;
					AsyncHelper.ContinueTask(task2, completion, delegate
					{
						if (completion.Task.IsCompleted)
						{
							return;
						}
						Interlocked.CompareExchange<TaskCompletionSource<object>>(ref this._reconnectionCompletionSource, null, completion);
						timeoutCTS.Cancel();
						Task task4;
						this.RunExecuteReaderTds(cmdBehavior, runBehavior, returnStream, async, TdsParserStaticMethods.GetRemainingTimeout(timeout, reconnectionStart), out task4, asyncWrite, ds);
						if (task4 == null)
						{
							completion.SetResult(null);
							return;
						}
						Task task5 = task4;
						TaskCompletionSource<object> completion2 = completion;
						Action action;
						if ((action = <>9__2) == null)
						{
							action = (<>9__2 = delegate
							{
								completion.SetResult(null);
							});
						}
						AsyncHelper.ContinueTask(task5, completion2, action, null, null, null, null, null);
					}, null, null, null, null, this._activeConnection);
					task = completion.Task;
					return ds;
				}
				AsyncHelper.WaitForCompletion(task2, timeout, delegate
				{
					throw SQL.CR_ReconnectTimeout();
				}, true);
				timeout = TdsParserStaticMethods.GetRemainingTimeout(timeout, reconnectionStart);
			}
			bool flag = (cmdBehavior & CommandBehavior.SchemaOnly) > CommandBehavior.Default;
			_SqlRPC sqlRPC = null;
			task = null;
			string optionSettings = null;
			bool flag2 = true;
			bool flag3 = false;
			if (async)
			{
				this._activeConnection.GetOpenTdsConnection().IncrementAsyncCount();
				flag3 = true;
			}
			try
			{
				if (asyncWrite)
				{
					this._activeConnection.AddWeakReference(this, 2);
				}
				this.GetStateObject(null);
				Task task3;
				if (CommandType.Text == this.CommandType && this.GetParameterCount(this._parameters) == 0)
				{
					string text = this.GetCommandText(cmdBehavior) + this.GetResetOptionsString(cmdBehavior);
					task3 = this._stateObj.Parser.TdsExecuteSQLBatch(text, timeout, this.Notification, this._stateObj, !asyncWrite, false);
				}
				else if (CommandType.Text == this.CommandType)
				{
					if (this.IsDirty)
					{
						if (this._execType == SqlCommand.EXECTYPE.PREPARED)
						{
							this._hiddenPrepare = true;
						}
						this.Unprepare();
						this.IsDirty = false;
					}
					if (this._execType == SqlCommand.EXECTYPE.PREPARED)
					{
						sqlRPC = this.BuildExecute(flag);
					}
					else if (this._execType == SqlCommand.EXECTYPE.PREPAREPENDING)
					{
						sqlRPC = this.BuildPrepExec(cmdBehavior);
						this._execType = SqlCommand.EXECTYPE.PREPARED;
						this._preparedConnectionCloseCount = this._activeConnection.CloseCount;
						this._preparedConnectionReconnectCount = this._activeConnection.ReconnectCount;
						this._inPrepare = true;
					}
					else
					{
						this.BuildExecuteSql(cmdBehavior, null, this._parameters, ref sqlRPC);
					}
					sqlRPC.options = 2;
					task3 = this._stateObj.Parser.TdsExecuteRPC(this._rpcArrayOf1, timeout, flag, this.Notification, this._stateObj, CommandType.StoredProcedure == this.CommandType, !asyncWrite, null, 0, 0);
				}
				else
				{
					this.BuildRPC(flag, this._parameters, ref sqlRPC);
					optionSettings = this.GetSetOptionsString(cmdBehavior);
					if (optionSettings != null)
					{
						this._stateObj.Parser.TdsExecuteSQLBatch(optionSettings, timeout, this.Notification, this._stateObj, true, false);
						bool flag4;
						if (!this._stateObj.Parser.TryRun(RunBehavior.UntilDone, this, null, null, this._stateObj, out flag4))
						{
							throw SQL.SynchronousCallMayNotPend();
						}
						optionSettings = this.GetResetOptionsString(cmdBehavior);
					}
					task3 = this._stateObj.Parser.TdsExecuteRPC(this._rpcArrayOf1, timeout, flag, this.Notification, this._stateObj, CommandType.StoredProcedure == this.CommandType, !asyncWrite, null, 0, 0);
				}
				if (async)
				{
					flag3 = false;
					if (task3 != null)
					{
						task = AsyncHelper.CreateContinuationTask(task3, delegate
						{
							this._activeConnection.GetOpenTdsConnection();
							this.cachedAsyncState.SetAsyncReaderState(ds, runBehavior, optionSettings);
						}, null, delegate(Exception exc)
						{
							this._activeConnection.GetOpenTdsConnection().DecrementAsyncCount();
						});
					}
					else
					{
						this.cachedAsyncState.SetAsyncReaderState(ds, runBehavior, optionSettings);
					}
				}
				else
				{
					this.FinishExecuteReader(ds, runBehavior, optionSettings);
				}
			}
			catch (Exception ex)
			{
				flag2 = ADP.IsCatchableExceptionType(ex);
				if (flag3)
				{
					SqlInternalConnectionTds sqlInternalConnectionTds = this._activeConnection.InnerConnection as SqlInternalConnectionTds;
					if (sqlInternalConnectionTds != null)
					{
						sqlInternalConnectionTds.DecrementAsyncCount();
					}
				}
				throw;
			}
			finally
			{
				if (flag2 && !async)
				{
					this.PutStateObject();
				}
			}
			return ds;
		}

		// Token: 0x06001192 RID: 4498 RVA: 0x00059550 File Offset: 0x00057750
		private SqlDataReader CompleteAsyncExecuteReader()
		{
			SqlDataReader cachedAsyncReader = this.cachedAsyncState.CachedAsyncReader;
			bool flag = true;
			try
			{
				this.FinishExecuteReader(cachedAsyncReader, this.cachedAsyncState.CachedRunBehavior, this.cachedAsyncState.CachedSetOptions);
			}
			catch (Exception ex)
			{
				flag = ADP.IsCatchableExceptionType(ex);
				throw;
			}
			finally
			{
				if (flag)
				{
					this.cachedAsyncState.ResetAsyncState();
					this.PutStateObject();
				}
			}
			return cachedAsyncReader;
		}

		// Token: 0x06001193 RID: 4499 RVA: 0x000595C4 File Offset: 0x000577C4
		private void FinishExecuteReader(SqlDataReader ds, RunBehavior runBehavior, string resetOptionsString)
		{
			this.NotifyDependency();
			if (runBehavior == RunBehavior.UntilDone)
			{
				try
				{
					bool flag;
					if (!this._stateObj.Parser.TryRun(RunBehavior.UntilDone, this, ds, null, this._stateObj, out flag))
					{
						throw SQL.SynchronousCallMayNotPend();
					}
				}
				catch (Exception ex)
				{
					if (ADP.IsCatchableExceptionType(ex))
					{
						if (this._inPrepare)
						{
							this._inPrepare = false;
							this.IsDirty = true;
							this._execType = SqlCommand.EXECTYPE.PREPAREPENDING;
						}
						if (ds != null)
						{
							try
							{
								ds.Close();
							}
							catch (Exception)
							{
							}
						}
					}
					throw;
				}
			}
			if (ds != null)
			{
				ds.Bind(this._stateObj);
				this._stateObj = null;
				ds.ResetOptionsString = resetOptionsString;
				this._activeConnection.AddWeakReference(ds, 1);
				try
				{
					this._cachedMetaData = ds.MetaData;
					ds.IsInitialized = true;
				}
				catch (Exception ex2)
				{
					if (ADP.IsCatchableExceptionType(ex2))
					{
						if (this._inPrepare)
						{
							this._inPrepare = false;
							this.IsDirty = true;
							this._execType = SqlCommand.EXECTYPE.PREPAREPENDING;
						}
						try
						{
							ds.Close();
						}
						catch (Exception)
						{
						}
					}
					throw;
				}
			}
		}

		// Token: 0x06001194 RID: 4500 RVA: 0x000596DC File Offset: 0x000578DC
		private void RegisterForConnectionCloseNotification<T>(ref Task<T> outerTask)
		{
			SqlConnection activeConnection = this._activeConnection;
			if (activeConnection == null)
			{
				throw ADP.ClosedConnectionError();
			}
			activeConnection.RegisterForConnectionCloseNotification<T>(ref outerTask, this, 2);
		}

		// Token: 0x06001195 RID: 4501 RVA: 0x000596F8 File Offset: 0x000578F8
		private void ValidateCommand(bool async, [CallerMemberName] string method = "")
		{
			if (this._activeConnection == null)
			{
				throw ADP.ConnectionRequired(method);
			}
			SqlInternalConnectionTds sqlInternalConnectionTds = this._activeConnection.InnerConnection as SqlInternalConnectionTds;
			if (sqlInternalConnectionTds != null)
			{
				TdsParser parser = sqlInternalConnectionTds.Parser;
				if (parser == null || parser.State == TdsParserState.Closed)
				{
					throw ADP.OpenConnectionRequired(method, ConnectionState.Closed);
				}
				if (parser.State != TdsParserState.OpenLoggedIn)
				{
					throw ADP.OpenConnectionRequired(method, ConnectionState.Broken);
				}
			}
			else
			{
				if (this._activeConnection.State == ConnectionState.Closed)
				{
					throw ADP.OpenConnectionRequired(method, ConnectionState.Closed);
				}
				if (this._activeConnection.State == ConnectionState.Broken)
				{
					throw ADP.OpenConnectionRequired(method, ConnectionState.Broken);
				}
			}
			this.ValidateAsyncCommand();
			this._activeConnection.ValidateConnectionForExecute(method, this);
			if (this._transaction != null && this._transaction.Connection == null)
			{
				this._transaction = null;
			}
			if (this._activeConnection.HasLocalTransactionFromAPI && this._transaction == null)
			{
				throw ADP.TransactionRequired(method);
			}
			if (this._transaction != null && this._activeConnection != this._transaction.Connection)
			{
				throw ADP.TransactionConnectionMismatch();
			}
			if (string.IsNullOrEmpty(this.CommandText))
			{
				throw ADP.CommandTextRequired(method);
			}
		}

		// Token: 0x06001196 RID: 4502 RVA: 0x00059801 File Offset: 0x00057A01
		private void ValidateAsyncCommand()
		{
			if (this.cachedAsyncState.PendingAsyncOperation)
			{
				if (this.cachedAsyncState.IsActiveConnectionValid(this._activeConnection))
				{
					throw SQL.PendingBeginXXXExists();
				}
				this._stateObj = null;
				this.cachedAsyncState.ResetAsyncState();
			}
		}

		// Token: 0x06001197 RID: 4503 RVA: 0x0005983C File Offset: 0x00057A3C
		private void GetStateObject(TdsParser parser = null)
		{
			if (this._pendingCancel)
			{
				this._pendingCancel = false;
				throw SQL.OperationCancelled();
			}
			if (parser == null)
			{
				parser = this._activeConnection.Parser;
				if (parser == null || parser.State == TdsParserState.Broken || parser.State == TdsParserState.Closed)
				{
					throw ADP.ClosedConnectionError();
				}
			}
			TdsParserStateObject session = parser.GetSession(this);
			session.StartSession(this);
			this._stateObj = session;
			if (this._pendingCancel)
			{
				this._pendingCancel = false;
				throw SQL.OperationCancelled();
			}
		}

		// Token: 0x06001198 RID: 4504 RVA: 0x000598BB File Offset: 0x00057ABB
		private void ReliablePutStateObject()
		{
			this.PutStateObject();
		}

		// Token: 0x06001199 RID: 4505 RVA: 0x000598C4 File Offset: 0x00057AC4
		private void PutStateObject()
		{
			TdsParserStateObject stateObj = this._stateObj;
			this._stateObj = null;
			if (stateObj != null)
			{
				stateObj.CloseSession();
			}
		}

		// Token: 0x0600119A RID: 4506 RVA: 0x000598E8 File Offset: 0x00057AE8
		internal void OnDoneProc()
		{
			if (this.BatchRPCMode)
			{
				this._SqlRPCBatchArray[this._currentlyExecutingBatch].cumulativeRecordsAffected = this._rowsAffected;
				this._SqlRPCBatchArray[this._currentlyExecutingBatch].recordsAffected = new int?((0 < this._currentlyExecutingBatch && 0 <= this._rowsAffected) ? (this._rowsAffected - Math.Max(this._SqlRPCBatchArray[this._currentlyExecutingBatch - 1].cumulativeRecordsAffected, 0)) : this._rowsAffected);
				this._SqlRPCBatchArray[this._currentlyExecutingBatch].errorsIndexStart = ((0 < this._currentlyExecutingBatch) ? this._SqlRPCBatchArray[this._currentlyExecutingBatch - 1].errorsIndexEnd : 0);
				this._SqlRPCBatchArray[this._currentlyExecutingBatch].errorsIndexEnd = this._stateObj.ErrorCount;
				this._SqlRPCBatchArray[this._currentlyExecutingBatch].errors = this._stateObj._errors;
				this._SqlRPCBatchArray[this._currentlyExecutingBatch].warningsIndexStart = ((0 < this._currentlyExecutingBatch) ? this._SqlRPCBatchArray[this._currentlyExecutingBatch - 1].warningsIndexEnd : 0);
				this._SqlRPCBatchArray[this._currentlyExecutingBatch].warningsIndexEnd = this._stateObj.WarningCount;
				this._SqlRPCBatchArray[this._currentlyExecutingBatch].warnings = this._stateObj._warnings;
				this._currentlyExecutingBatch++;
			}
		}

		// Token: 0x0600119B RID: 4507 RVA: 0x00059A50 File Offset: 0x00057C50
		internal void OnReturnStatus(int status)
		{
			if (this._inPrepare)
			{
				return;
			}
			SqlParameterCollection sqlParameterCollection = this._parameters;
			if (this.BatchRPCMode)
			{
				if (this._parameterCollectionList.Count > this._currentlyExecutingBatch)
				{
					sqlParameterCollection = this._parameterCollectionList[this._currentlyExecutingBatch];
				}
				else
				{
					sqlParameterCollection = null;
				}
			}
			int parameterCount = this.GetParameterCount(sqlParameterCollection);
			int i = 0;
			while (i < parameterCount)
			{
				SqlParameter sqlParameter = sqlParameterCollection[i];
				if (sqlParameter.Direction == ParameterDirection.ReturnValue)
				{
					object value = sqlParameter.Value;
					if (value != null && value.GetType() == typeof(SqlInt32))
					{
						sqlParameter.Value = new SqlInt32(status);
						return;
					}
					sqlParameter.Value = status;
					return;
				}
				else
				{
					i++;
				}
			}
		}

		// Token: 0x0600119C RID: 4508 RVA: 0x00059B08 File Offset: 0x00057D08
		internal void OnReturnValue(SqlReturnValue rec)
		{
			if (this._inPrepare)
			{
				if (!rec.value.IsNull)
				{
					this._prepareHandle = rec.value.Int32;
				}
				this._inPrepare = false;
				return;
			}
			SqlParameterCollection currentParameterCollection = this.GetCurrentParameterCollection();
			int parameterCount = this.GetParameterCount(currentParameterCollection);
			SqlParameter parameterForOutputValueExtraction = this.GetParameterForOutputValueExtraction(currentParameterCollection, rec.parameter, parameterCount);
			if (parameterForOutputValueExtraction != null)
			{
				object value = parameterForOutputValueExtraction.Value;
				parameterForOutputValueExtraction.SetSqlBuffer(rec.value);
				MetaType metaTypeFromSqlDbType = MetaType.GetMetaTypeFromSqlDbType(rec.type, false);
				if (rec.type == SqlDbType.Decimal)
				{
					parameterForOutputValueExtraction.ScaleInternal = rec.scale;
					parameterForOutputValueExtraction.PrecisionInternal = rec.precision;
				}
				else if (metaTypeFromSqlDbType.IsVarTime)
				{
					parameterForOutputValueExtraction.ScaleInternal = rec.scale;
				}
				else if (rec.type == SqlDbType.Xml)
				{
					SqlCachedBuffer sqlCachedBuffer = parameterForOutputValueExtraction.Value as SqlCachedBuffer;
					if (sqlCachedBuffer != null)
					{
						parameterForOutputValueExtraction.Value = sqlCachedBuffer.ToString();
					}
				}
				if (rec.collation != null)
				{
					parameterForOutputValueExtraction.Collation = rec.collation;
				}
			}
		}

		// Token: 0x0600119D RID: 4509 RVA: 0x00059BFC File Offset: 0x00057DFC
		private SqlParameterCollection GetCurrentParameterCollection()
		{
			if (!this.BatchRPCMode)
			{
				return this._parameters;
			}
			if (this._parameterCollectionList.Count > this._currentlyExecutingBatch)
			{
				return this._parameterCollectionList[this._currentlyExecutingBatch];
			}
			return null;
		}

		// Token: 0x0600119E RID: 4510 RVA: 0x00059C34 File Offset: 0x00057E34
		private SqlParameter GetParameterForOutputValueExtraction(SqlParameterCollection parameters, string paramName, int paramCount)
		{
			SqlParameter sqlParameter = null;
			bool flag = false;
			if (paramName == null)
			{
				for (int i = 0; i < paramCount; i++)
				{
					sqlParameter = parameters[i];
					if (sqlParameter.Direction == ParameterDirection.ReturnValue)
					{
						flag = true;
						break;
					}
				}
			}
			else
			{
				for (int j = 0; j < paramCount; j++)
				{
					sqlParameter = parameters[j];
					if (sqlParameter.Direction != ParameterDirection.Input && sqlParameter.Direction != ParameterDirection.ReturnValue && paramName == sqlParameter.ParameterNameFixed)
					{
						flag = true;
						break;
					}
				}
			}
			if (flag)
			{
				return sqlParameter;
			}
			return null;
		}

		// Token: 0x0600119F RID: 4511 RVA: 0x00059CAC File Offset: 0x00057EAC
		private void GetRPCObject(int paramCount, ref _SqlRPC rpc)
		{
			if (rpc == null)
			{
				if (this._rpcArrayOf1 == null)
				{
					this._rpcArrayOf1 = new _SqlRPC[1];
					this._rpcArrayOf1[0] = new _SqlRPC();
				}
				rpc = this._rpcArrayOf1[0];
			}
			rpc.ProcID = 0;
			rpc.rpcName = null;
			rpc.options = 0;
			if (rpc.parameters == null || rpc.parameters.Length < paramCount)
			{
				rpc.parameters = new SqlParameter[paramCount];
			}
			else if (rpc.parameters.Length > paramCount)
			{
				rpc.parameters[paramCount] = null;
			}
			if (rpc.paramoptions == null || rpc.paramoptions.Length < paramCount)
			{
				rpc.paramoptions = new byte[paramCount];
				return;
			}
			for (int i = 0; i < paramCount; i++)
			{
				rpc.paramoptions[i] = 0;
			}
		}

		// Token: 0x060011A0 RID: 4512 RVA: 0x00059D74 File Offset: 0x00057F74
		private void SetUpRPCParameters(_SqlRPC rpc, int startCount, bool inSchema, SqlParameterCollection parameters)
		{
			int parameterCount = this.GetParameterCount(parameters);
			int num = startCount;
			TdsParser parser = this._activeConnection.Parser;
			for (int i = 0; i < parameterCount; i++)
			{
				SqlParameter sqlParameter = parameters[i];
				sqlParameter.Validate(i, CommandType.StoredProcedure == this.CommandType);
				if (!sqlParameter.ValidateTypeLengths().IsPlp && sqlParameter.Direction != ParameterDirection.Output)
				{
					sqlParameter.FixStreamDataForNonPLP();
				}
				if (SqlCommand.ShouldSendParameter(sqlParameter))
				{
					rpc.parameters[num] = sqlParameter;
					if (sqlParameter.Direction == ParameterDirection.InputOutput || sqlParameter.Direction == ParameterDirection.Output)
					{
						rpc.paramoptions[num] = 1;
					}
					if (sqlParameter.Direction != ParameterDirection.Output && sqlParameter.Value == null && (!inSchema || SqlDbType.Structured == sqlParameter.SqlDbType))
					{
						byte[] paramoptions = rpc.paramoptions;
						int num2 = num;
						paramoptions[num2] |= 2;
					}
					num++;
				}
			}
		}

		// Token: 0x060011A1 RID: 4513 RVA: 0x00059E40 File Offset: 0x00058040
		private _SqlRPC BuildPrepExec(CommandBehavior behavior)
		{
			int num = 3;
			int num2 = this.CountSendableParameters(this._parameters);
			_SqlRPC sqlRPC = null;
			this.GetRPCObject(num2 + num, ref sqlRPC);
			sqlRPC.ProcID = 13;
			sqlRPC.rpcName = "sp_prepexec";
			SqlParameter sqlParameter = new SqlParameter(null, SqlDbType.Int);
			sqlParameter.Direction = ParameterDirection.InputOutput;
			sqlParameter.Value = this._prepareHandle;
			sqlRPC.parameters[0] = sqlParameter;
			sqlRPC.paramoptions[0] = 1;
			string text = this.BuildParamList(this._stateObj.Parser, this._parameters);
			sqlParameter = new SqlParameter(null, (text.Length << 1 <= 8000) ? SqlDbType.NVarChar : SqlDbType.NText, text.Length);
			sqlParameter.Value = text;
			sqlRPC.parameters[1] = sqlParameter;
			string commandText = this.GetCommandText(behavior);
			sqlParameter = new SqlParameter(null, (commandText.Length << 1 <= 8000) ? SqlDbType.NVarChar : SqlDbType.NText, commandText.Length);
			sqlParameter.Value = commandText;
			sqlRPC.parameters[2] = sqlParameter;
			this.SetUpRPCParameters(sqlRPC, num, false, this._parameters);
			return sqlRPC;
		}

		// Token: 0x060011A2 RID: 4514 RVA: 0x00059F4C File Offset: 0x0005814C
		private static bool ShouldSendParameter(SqlParameter p)
		{
			ParameterDirection direction = p.Direction;
			return direction - ParameterDirection.Input <= 2;
		}

		// Token: 0x060011A3 RID: 4515 RVA: 0x00059F70 File Offset: 0x00058170
		private int CountSendableParameters(SqlParameterCollection parameters)
		{
			int num = 0;
			if (parameters != null)
			{
				int count = parameters.Count;
				for (int i = 0; i < count; i++)
				{
					if (SqlCommand.ShouldSendParameter(parameters[i]))
					{
						num++;
					}
				}
			}
			return num;
		}

		// Token: 0x060011A4 RID: 4516 RVA: 0x00059FA8 File Offset: 0x000581A8
		private int GetParameterCount(SqlParameterCollection parameters)
		{
			if (parameters == null)
			{
				return 0;
			}
			return parameters.Count;
		}

		// Token: 0x060011A5 RID: 4517 RVA: 0x00059FB8 File Offset: 0x000581B8
		private void BuildRPC(bool inSchema, SqlParameterCollection parameters, ref _SqlRPC rpc)
		{
			int num = this.CountSendableParameters(parameters);
			this.GetRPCObject(num, ref rpc);
			rpc.rpcName = this.CommandText;
			this.SetUpRPCParameters(rpc, 0, inSchema, parameters);
		}

		// Token: 0x060011A6 RID: 4518 RVA: 0x00059FF0 File Offset: 0x000581F0
		private _SqlRPC BuildExecute(bool inSchema)
		{
			int num = 1;
			int num2 = this.CountSendableParameters(this._parameters);
			_SqlRPC sqlRPC = null;
			this.GetRPCObject(num2 + num, ref sqlRPC);
			sqlRPC.ProcID = 12;
			sqlRPC.rpcName = "sp_execute";
			SqlParameter sqlParameter = new SqlParameter(null, SqlDbType.Int);
			sqlParameter.Value = this._prepareHandle;
			sqlRPC.parameters[0] = sqlParameter;
			this.SetUpRPCParameters(sqlRPC, num, inSchema, this._parameters);
			return sqlRPC;
		}

		// Token: 0x060011A7 RID: 4519 RVA: 0x0005A060 File Offset: 0x00058260
		private void BuildExecuteSql(CommandBehavior behavior, string commandText, SqlParameterCollection parameters, ref _SqlRPC rpc)
		{
			int num = this.CountSendableParameters(parameters);
			int num2;
			if (num > 0)
			{
				num2 = 2;
			}
			else
			{
				num2 = 1;
			}
			this.GetRPCObject(num + num2, ref rpc);
			rpc.ProcID = 10;
			rpc.rpcName = "sp_executesql";
			if (commandText == null)
			{
				commandText = this.GetCommandText(behavior);
			}
			SqlParameter sqlParameter = new SqlParameter(null, (commandText.Length << 1 <= 8000) ? SqlDbType.NVarChar : SqlDbType.NText, commandText.Length);
			sqlParameter.Value = commandText;
			rpc.parameters[0] = sqlParameter;
			if (num > 0)
			{
				string text = this.BuildParamList(this._stateObj.Parser, this.BatchRPCMode ? parameters : this._parameters);
				sqlParameter = new SqlParameter(null, (text.Length << 1 <= 8000) ? SqlDbType.NVarChar : SqlDbType.NText, text.Length);
				sqlParameter.Value = text;
				rpc.parameters[1] = sqlParameter;
				bool flag = (behavior & CommandBehavior.SchemaOnly) > CommandBehavior.Default;
				this.SetUpRPCParameters(rpc, num2, flag, parameters);
			}
		}

		// Token: 0x060011A8 RID: 4520 RVA: 0x0005A154 File Offset: 0x00058354
		internal string BuildParamList(TdsParser parser, SqlParameterCollection parameters)
		{
			StringBuilder stringBuilder = new StringBuilder();
			bool flag = false;
			int count = parameters.Count;
			for (int i = 0; i < count; i++)
			{
				SqlParameter sqlParameter = parameters[i];
				sqlParameter.Validate(i, CommandType.StoredProcedure == this.CommandType);
				if (SqlCommand.ShouldSendParameter(sqlParameter))
				{
					if (flag)
					{
						stringBuilder.Append(',');
					}
					stringBuilder.Append(sqlParameter.ParameterNameFixed);
					MetaType metaType = sqlParameter.InternalMetaType;
					stringBuilder.Append(" ");
					if (metaType.SqlDbType == SqlDbType.Udt)
					{
						throw ADP.DbTypeNotSupported(SqlDbType.Udt.ToString());
					}
					if (metaType.SqlDbType == SqlDbType.Structured)
					{
						string typeName = sqlParameter.TypeName;
						if (string.IsNullOrEmpty(typeName))
						{
							throw SQL.MustSetTypeNameForParam(metaType.TypeName, sqlParameter.ParameterNameFixed);
						}
						stringBuilder.Append(this.ParseAndQuoteIdentifier(typeName));
						stringBuilder.Append(" READONLY");
					}
					else
					{
						metaType = sqlParameter.ValidateTypeLengths();
						if (!metaType.IsPlp && sqlParameter.Direction != ParameterDirection.Output)
						{
							sqlParameter.FixStreamDataForNonPLP();
						}
						stringBuilder.Append(metaType.TypeName);
					}
					flag = true;
					if (metaType.SqlDbType == SqlDbType.Decimal)
					{
						byte b = sqlParameter.GetActualPrecision();
						byte actualScale = sqlParameter.GetActualScale();
						stringBuilder.Append('(');
						if (b == 0)
						{
							b = 29;
						}
						stringBuilder.Append(b);
						stringBuilder.Append(',');
						stringBuilder.Append(actualScale);
						stringBuilder.Append(')');
					}
					else if (metaType.IsVarTime)
					{
						byte actualScale2 = sqlParameter.GetActualScale();
						stringBuilder.Append('(');
						stringBuilder.Append(actualScale2);
						stringBuilder.Append(')');
					}
					else if (!metaType.IsFixed && !metaType.IsLong && metaType.SqlDbType != SqlDbType.Timestamp && metaType.SqlDbType != SqlDbType.Udt && SqlDbType.Structured != metaType.SqlDbType)
					{
						int num = sqlParameter.Size;
						stringBuilder.Append('(');
						if (metaType.IsAnsiType)
						{
							object coercedValue = sqlParameter.GetCoercedValue();
							string text = null;
							if (coercedValue != null && DBNull.Value != coercedValue)
							{
								text = coercedValue as string;
								if (text == null)
								{
									SqlString sqlString = ((coercedValue is SqlString) ? ((SqlString)coercedValue) : SqlString.Null);
									if (!sqlString.IsNull)
									{
										text = sqlString.Value;
									}
								}
							}
							if (text != null)
							{
								int encodingCharLength = parser.GetEncodingCharLength(text, sqlParameter.GetActualSize(), sqlParameter.Offset, null);
								if (encodingCharLength > num)
								{
									num = encodingCharLength;
								}
							}
						}
						if (num == 0)
						{
							num = (metaType.IsSizeInCharacters ? 4000 : 8000);
						}
						stringBuilder.Append(num);
						stringBuilder.Append(')');
					}
					else if (metaType.IsPlp && metaType.SqlDbType != SqlDbType.Xml && metaType.SqlDbType != SqlDbType.Udt)
					{
						stringBuilder.Append("(max) ");
					}
					if (sqlParameter.Direction != ParameterDirection.Input)
					{
						stringBuilder.Append(" output");
					}
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060011A9 RID: 4521 RVA: 0x0005A450 File Offset: 0x00058650
		private string ParseAndQuoteIdentifier(string identifier)
		{
			string[] array = SqlParameter.ParseTypeName(identifier);
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < array.Length; i++)
			{
				if (0 < stringBuilder.Length)
				{
					stringBuilder.Append('.');
				}
				if (array[i] != null && array[i].Length != 0)
				{
					stringBuilder.Append(ADP.BuildQuotedString("[", "]", array[i]));
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060011AA RID: 4522 RVA: 0x0005A4B8 File Offset: 0x000586B8
		private string GetSetOptionsString(CommandBehavior behavior)
		{
			string text = null;
			if (CommandBehavior.SchemaOnly == (behavior & CommandBehavior.SchemaOnly) || CommandBehavior.KeyInfo == (behavior & CommandBehavior.KeyInfo))
			{
				text = " SET FMTONLY OFF;";
				if (CommandBehavior.KeyInfo == (behavior & CommandBehavior.KeyInfo))
				{
					text += " SET NO_BROWSETABLE ON;";
				}
				if (CommandBehavior.SchemaOnly == (behavior & CommandBehavior.SchemaOnly))
				{
					text += " SET FMTONLY ON;";
				}
			}
			return text;
		}

		// Token: 0x060011AB RID: 4523 RVA: 0x0005A500 File Offset: 0x00058700
		private string GetResetOptionsString(CommandBehavior behavior)
		{
			string text = null;
			if (CommandBehavior.SchemaOnly == (behavior & CommandBehavior.SchemaOnly))
			{
				text += " SET FMTONLY OFF;";
			}
			if (CommandBehavior.KeyInfo == (behavior & CommandBehavior.KeyInfo))
			{
				text += " SET NO_BROWSETABLE OFF;";
			}
			return text;
		}

		// Token: 0x060011AC RID: 4524 RVA: 0x0005A534 File Offset: 0x00058734
		private string GetCommandText(CommandBehavior behavior)
		{
			return this.GetSetOptionsString(behavior) + this.CommandText;
		}

		// Token: 0x060011AD RID: 4525 RVA: 0x0005A548 File Offset: 0x00058748
		internal void CheckThrowSNIException()
		{
			TdsParserStateObject stateObj = this._stateObj;
			if (stateObj != null)
			{
				stateObj.CheckThrowSNIException();
			}
		}

		// Token: 0x060011AE RID: 4526 RVA: 0x0005A568 File Offset: 0x00058768
		internal void OnConnectionClosed()
		{
			TdsParserStateObject stateObj = this._stateObj;
			if (stateObj != null)
			{
				stateObj.OnConnectionClosed();
			}
		}

		// Token: 0x17000315 RID: 789
		// (get) Token: 0x060011AF RID: 4527 RVA: 0x0005A585 File Offset: 0x00058785
		internal TdsParserStateObject StateObject
		{
			get
			{
				return this._stateObj;
			}
		}

		// Token: 0x17000316 RID: 790
		// (get) Token: 0x060011B0 RID: 4528 RVA: 0x0005A58D File Offset: 0x0005878D
		private bool IsPrepared
		{
			get
			{
				return this._execType > SqlCommand.EXECTYPE.UNPREPARED;
			}
		}

		// Token: 0x17000317 RID: 791
		// (get) Token: 0x060011B1 RID: 4529 RVA: 0x0005A598 File Offset: 0x00058798
		private bool IsUserPrepared
		{
			get
			{
				return this.IsPrepared && !this._hiddenPrepare && !this.IsDirty;
			}
		}

		// Token: 0x17000318 RID: 792
		// (get) Token: 0x060011B2 RID: 4530 RVA: 0x0005A5B8 File Offset: 0x000587B8
		// (set) Token: 0x060011B3 RID: 4531 RVA: 0x0005A61B File Offset: 0x0005881B
		internal bool IsDirty
		{
			get
			{
				SqlConnection activeConnection = this._activeConnection;
				return this.IsPrepared && (this._dirty || (this._parameters != null && this._parameters.IsDirty) || (activeConnection != null && (activeConnection.CloseCount != this._preparedConnectionCloseCount || activeConnection.ReconnectCount != this._preparedConnectionReconnectCount)));
			}
			set
			{
				this._dirty = value && this.IsPrepared;
				if (this._parameters != null)
				{
					this._parameters.IsDirty = this._dirty;
				}
				this._cachedMetaData = null;
			}
		}

		// Token: 0x17000319 RID: 793
		// (get) Token: 0x060011B4 RID: 4532 RVA: 0x0005A64F File Offset: 0x0005884F
		// (set) Token: 0x060011B5 RID: 4533 RVA: 0x0005A657 File Offset: 0x00058857
		internal int InternalRecordsAffected
		{
			get
			{
				return this._rowsAffected;
			}
			set
			{
				if (-1 == this._rowsAffected)
				{
					this._rowsAffected = value;
					return;
				}
				if (0 < value)
				{
					this._rowsAffected += value;
				}
			}
		}

		// Token: 0x060011B6 RID: 4534 RVA: 0x0005A67C File Offset: 0x0005887C
		internal void ClearBatchCommand()
		{
			List<_SqlRPC> rpclist = this._RPCList;
			if (rpclist != null)
			{
				rpclist.Clear();
			}
			if (this._parameterCollectionList != null)
			{
				this._parameterCollectionList.Clear();
			}
			this._SqlRPCBatchArray = null;
			this._currentlyExecutingBatch = 0;
		}

		// Token: 0x1700031A RID: 794
		// (get) Token: 0x060011B7 RID: 4535 RVA: 0x0005A6BA File Offset: 0x000588BA
		// (set) Token: 0x060011B8 RID: 4536 RVA: 0x0005A6C2 File Offset: 0x000588C2
		internal bool BatchRPCMode
		{
			get
			{
				return this._batchRPCMode;
			}
			set
			{
				this._batchRPCMode = value;
				if (!this._batchRPCMode)
				{
					this.ClearBatchCommand();
					return;
				}
				if (this._RPCList == null)
				{
					this._RPCList = new List<_SqlRPC>();
				}
				if (this._parameterCollectionList == null)
				{
					this._parameterCollectionList = new List<SqlParameterCollection>();
				}
			}
		}

		// Token: 0x060011B9 RID: 4537 RVA: 0x0005A700 File Offset: 0x00058900
		internal void AddBatchCommand(string commandText, SqlParameterCollection parameters, CommandType cmdType)
		{
			_SqlRPC sqlRPC = new _SqlRPC();
			this.CommandText = commandText;
			this.CommandType = cmdType;
			this.GetStateObject(null);
			if (cmdType == CommandType.StoredProcedure)
			{
				this.BuildRPC(false, parameters, ref sqlRPC);
			}
			else
			{
				this.BuildExecuteSql(CommandBehavior.Default, commandText, parameters, ref sqlRPC);
			}
			this._RPCList.Add(sqlRPC);
			this._parameterCollectionList.Add(parameters);
			this.ReliablePutStateObject();
		}

		// Token: 0x060011BA RID: 4538 RVA: 0x0005A761 File Offset: 0x00058961
		internal int ExecuteBatchRPCCommand()
		{
			this._SqlRPCBatchArray = this._RPCList.ToArray();
			this._currentlyExecutingBatch = 0;
			return this.ExecuteNonQuery();
		}

		// Token: 0x060011BB RID: 4539 RVA: 0x0005A781 File Offset: 0x00058981
		internal int? GetRecordsAffected(int commandIndex)
		{
			return this._SqlRPCBatchArray[commandIndex].recordsAffected;
		}

		// Token: 0x060011BC RID: 4540 RVA: 0x0005A790 File Offset: 0x00058990
		internal SqlException GetErrors(int commandIndex)
		{
			SqlException ex = null;
			int num = this._SqlRPCBatchArray[commandIndex].errorsIndexEnd - this._SqlRPCBatchArray[commandIndex].errorsIndexStart;
			if (0 < num)
			{
				SqlErrorCollection sqlErrorCollection = new SqlErrorCollection();
				for (int i = this._SqlRPCBatchArray[commandIndex].errorsIndexStart; i < this._SqlRPCBatchArray[commandIndex].errorsIndexEnd; i++)
				{
					sqlErrorCollection.Add(this._SqlRPCBatchArray[commandIndex].errors[i]);
				}
				for (int j = this._SqlRPCBatchArray[commandIndex].warningsIndexStart; j < this._SqlRPCBatchArray[commandIndex].warningsIndexEnd; j++)
				{
					sqlErrorCollection.Add(this._SqlRPCBatchArray[commandIndex].warnings[j]);
				}
				ex = SqlException.CreateException(sqlErrorCollection, this.Connection.ServerVersion, this.Connection.ClientConnectionId, null);
			}
			return ex;
		}

		// Token: 0x060011BD RID: 4541 RVA: 0x0005A868 File Offset: 0x00058A68
		internal new void CancelIgnoreFailure()
		{
			try
			{
				this.Cancel();
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x060011BE RID: 4542 RVA: 0x0005A890 File Offset: 0x00058A90
		private void NotifyDependency()
		{
			if (this._sqlDep != null)
			{
				this._sqlDep.StartTimer(this.Notification);
			}
		}

		/// <summary>Creates a new <see cref="T:System.Data.SqlClient.SqlCommand" /> object that is a copy of the current instance.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlClient.SqlCommand" /> object that is a copy of this instance.</returns>
		// Token: 0x060011BF RID: 4543 RVA: 0x0005A8AB File Offset: 0x00058AAB
		object ICloneable.Clone()
		{
			return this.Clone();
		}

		/// <summary>Creates a new <see cref="T:System.Data.SqlClient.SqlCommand" /> object that is a copy of the current instance.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlClient.SqlCommand" /> object that is a copy of this instance.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060011C0 RID: 4544 RVA: 0x0005A8B3 File Offset: 0x00058AB3
		public SqlCommand Clone()
		{
			return new SqlCommand(this);
		}

		/// <summary>Gets or sets a value indicating whether the application should automatically receive query notifications from a common <see cref="T:System.Data.SqlClient.SqlDependency" /> object.</summary>
		/// <returns>true if the application should automatically receive query notifications; otherwise false. The default value is true.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700031B RID: 795
		// (get) Token: 0x060011C1 RID: 4545 RVA: 0x0005A8BB File Offset: 0x00058ABB
		// (set) Token: 0x060011C2 RID: 4546 RVA: 0x0005A8C6 File Offset: 0x00058AC6
		[MonoTODO]
		public bool NotificationAutoEnlist
		{
			get
			{
				return this.Notification != null;
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Initiates the asynchronous execution of the Transact-SQL statement or stored procedure that is described by this <see cref="T:System.Data.SqlClient.SqlCommand" />.</summary>
		/// <returns>An <see cref="T:System.IAsyncResult" /> that can be used to poll or wait for results, or both; this value is also needed when invoking <see cref="M:System.Data.SqlClient.SqlCommand.EndExecuteNonQuery(System.IAsyncResult)" />, which returns the number of affected rows.</returns>
		/// <exception cref="T:System.InvalidCastException">A <see cref="P:System.Data.SqlClient.SqlParameter.SqlDbType" /> other than Binary or VarBinary was used when <see cref="P:System.Data.SqlClient.SqlParameter.Value" /> was set to <see cref="T:System.IO.Stream" />. For more information about streaming, see SqlClient Streaming Support.A <see cref="P:System.Data.SqlClient.SqlParameter.SqlDbType" /> other than Char, NChar, NVarChar, VarChar, or  Xml was used when <see cref="P:System.Data.SqlClient.SqlParameter.Value" /> was set to <see cref="T:System.IO.TextReader" />.A <see cref="P:System.Data.SqlClient.SqlParameter.SqlDbType" /> other than Xml was used when <see cref="P:System.Data.SqlClient.SqlParameter.Value" /> was set to <see cref="T:System.Xml.XmlReader" />.</exception>
		/// <exception cref="T:System.Data.SqlClient.SqlException">Any error that occurred while executing the command text.A timeout occurred during a streaming operation. For more information about streaming, see SqlClient Streaming Support.</exception>
		/// <exception cref="T:System.InvalidOperationException">The name/value pair "Asynchronous Processing=true" was not included within the connection string defining the connection for this <see cref="T:System.Data.SqlClient.SqlCommand" />.The <see cref="T:System.Data.SqlClient.SqlConnection" /> closed or dropped during a streaming operation. For more information about streaming, see SqlClient Streaming Support.</exception>
		/// <exception cref="T:System.IO.IOException">An error occurred in a <see cref="T:System.IO.Stream" />, <see cref="T:System.Xml.XmlReader" /> or <see cref="T:System.IO.TextReader" /> object during a streaming operation.  For more information about streaming, see SqlClient Streaming Support.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.Stream" />, <see cref="T:System.Xml.XmlReader" /> or <see cref="T:System.IO.TextReader" /> object was closed during a streaming operation.  For more information about streaming, see SqlClient Streaming Support.</exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Data.SqlClient.SqlClientPermission, System.Data, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060011C3 RID: 4547 RVA: 0x0005A8CD File Offset: 0x00058ACD
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public IAsyncResult BeginExecuteNonQuery()
		{
			return this.BeginExecuteNonQuery(null, null);
		}

		/// <summary>Initiates the asynchronous execution of the Transact-SQL statement or stored procedure that is described by this <see cref="T:System.Data.SqlClient.SqlCommand" /> and returns results as an <see cref="T:System.Xml.XmlReader" /> object.</summary>
		/// <returns>An <see cref="T:System.IAsyncResult" /> that can be used to poll or wait for results, or both; this value is also needed when invoking EndExecuteXmlReader, which returns a single XML value.</returns>
		/// <exception cref="T:System.InvalidCastException">A <see cref="P:System.Data.SqlClient.SqlParameter.SqlDbType" /> other than Binary or VarBinary was used when <see cref="P:System.Data.SqlClient.SqlParameter.Value" /> was set to <see cref="T:System.IO.Stream" />. For more information about streaming, see SqlClient Streaming Support.A <see cref="P:System.Data.SqlClient.SqlParameter.SqlDbType" /> other than Char, NChar, NVarChar, VarChar, or  Xml was used when <see cref="P:System.Data.SqlClient.SqlParameter.Value" /> was set to <see cref="T:System.IO.TextReader" />.A <see cref="P:System.Data.SqlClient.SqlParameter.SqlDbType" /> other than Xml was used when <see cref="P:System.Data.SqlClient.SqlParameter.Value" /> was set to <see cref="T:System.Xml.XmlReader" />.</exception>
		/// <exception cref="T:System.Data.SqlClient.SqlException">Any error that occurred while executing the command text.A timeout occurred during a streaming operation. For more information about streaming, see SqlClient Streaming Support.</exception>
		/// <exception cref="T:System.InvalidOperationException">The name/value pair "Asynchronous Processing=true" was not included within the connection string defining the connection for this <see cref="T:System.Data.SqlClient.SqlCommand" />.The <see cref="T:System.Data.SqlClient.SqlConnection" /> closed or dropped during a streaming operation. For more information about streaming, see SqlClient Streaming Support.</exception>
		/// <exception cref="T:System.IO.IOException">An error occurred in a <see cref="T:System.IO.Stream" />, <see cref="T:System.Xml.XmlReader" /> or <see cref="T:System.IO.TextReader" /> object during a streaming operation.  For more information about streaming, see SqlClient Streaming Support.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.Stream" />, <see cref="T:System.Xml.XmlReader" /> or <see cref="T:System.IO.TextReader" /> object was closed during a streaming operation.  For more information about streaming, see SqlClient Streaming Support.</exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		///   <IPermission class="System.Security.Permissions.RegistryPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence, ControlPolicy, ControlAppDomain" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Data.SqlClient.SqlClientPermission, System.Data, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060011C4 RID: 4548 RVA: 0x0005A8D7 File Offset: 0x00058AD7
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public IAsyncResult BeginExecuteXmlReader()
		{
			return this.BeginExecuteXmlReader(null, null);
		}

		/// <summary>Initiates the asynchronous execution of the Transact-SQL statement or stored procedure that is described by this <see cref="T:System.Data.SqlClient.SqlCommand" />, and retrieves one or more result sets from the server.</summary>
		/// <returns>An <see cref="T:System.IAsyncResult" /> that can be used to poll or wait for results, or both; this value is also needed when invoking <see cref="M:System.Data.SqlClient.SqlCommand.EndExecuteReader(System.IAsyncResult)" />, which returns a <see cref="T:System.Data.SqlClient.SqlDataReader" /> instance that can be used to retrieve the returned rows.</returns>
		/// <exception cref="T:System.InvalidCastException">A <see cref="P:System.Data.SqlClient.SqlParameter.SqlDbType" /> other than Binary or VarBinary was used when <see cref="P:System.Data.SqlClient.SqlParameter.Value" /> was set to <see cref="T:System.IO.Stream" />. For more information about streaming, see SqlClient Streaming Support.A <see cref="P:System.Data.SqlClient.SqlParameter.SqlDbType" /> other than Char, NChar, NVarChar, VarChar, or  Xml was used when <see cref="P:System.Data.SqlClient.SqlParameter.Value" /> was set to <see cref="T:System.IO.TextReader" />.A <see cref="P:System.Data.SqlClient.SqlParameter.SqlDbType" /> other than Xml was used when <see cref="P:System.Data.SqlClient.SqlParameter.Value" /> was set to <see cref="T:System.Xml.XmlReader" />.</exception>
		/// <exception cref="T:System.Data.SqlClient.SqlException">Any error that occurred while executing the command text.A timeout occurred during a streaming operation. For more information about streaming, see SqlClient Streaming Support.</exception>
		/// <exception cref="T:System.InvalidOperationException">The name/value pair "Asynchronous Processing=true" was not included within the connection string defining the connection for this <see cref="T:System.Data.SqlClient.SqlCommand" />.The <see cref="T:System.Data.SqlClient.SqlConnection" /> closed or dropped during a streaming operation. For more information about streaming, see SqlClient Streaming Support.</exception>
		/// <exception cref="T:System.IO.IOException">An error occurred in a <see cref="T:System.IO.Stream" />, <see cref="T:System.Xml.XmlReader" /> or <see cref="T:System.IO.TextReader" /> object during a streaming operation.  For more information about streaming, see SqlClient Streaming Support.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.Stream" />, <see cref="T:System.Xml.XmlReader" /> or <see cref="T:System.IO.TextReader" /> object was closed during a streaming operation.  For more information about streaming, see SqlClient Streaming Support.</exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		///   <IPermission class="System.Security.Permissions.RegistryPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence, ControlPolicy, ControlAppDomain" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Data.SqlClient.SqlClientPermission, System.Data, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060011C5 RID: 4549 RVA: 0x0005A8E1 File Offset: 0x00058AE1
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public IAsyncResult BeginExecuteReader()
		{
			return this.BeginExecuteReader(CommandBehavior.Default, null, null);
		}

		/// <summary>Initiates the asynchronous execution of the Transact-SQL statement or stored procedure that is described by this <see cref="T:System.Data.SqlClient.SqlCommand" /> and retrieves one or more result sets from the server, given a callback procedure and state information.</summary>
		/// <returns>An <see cref="T:System.IAsyncResult" /> that can be used to poll, wait for results, or both; this value is also needed when invoking <see cref="M:System.Data.SqlClient.SqlCommand.EndExecuteReader(System.IAsyncResult)" />, which returns a <see cref="T:System.Data.SqlClient.SqlDataReader" /> instance which can be used to retrieve the returned rows.</returns>
		/// <param name="callback">An <see cref="T:System.AsyncCallback" /> delegate that is invoked when the command's execution has completed. Pass null (Nothing in Microsoft Visual Basic) to indicate that no callback is required.</param>
		/// <param name="stateObject">A user-defined state object that is passed to the callback procedure. Retrieve this object from within the callback procedure using the <see cref="P:System.IAsyncResult.AsyncState" /> property.</param>
		/// <exception cref="T:System.InvalidCastException">A <see cref="P:System.Data.SqlClient.SqlParameter.SqlDbType" /> other than Binary or VarBinary was used when <see cref="P:System.Data.SqlClient.SqlParameter.Value" /> was set to <see cref="T:System.IO.Stream" />. For more information about streaming, see SqlClient Streaming Support.A <see cref="P:System.Data.SqlClient.SqlParameter.SqlDbType" /> other than Char, NChar, NVarChar, VarChar, or  Xml was used when <see cref="P:System.Data.SqlClient.SqlParameter.Value" /> was set to <see cref="T:System.IO.TextReader" />.A <see cref="P:System.Data.SqlClient.SqlParameter.SqlDbType" /> other than Xml was used when <see cref="P:System.Data.SqlClient.SqlParameter.Value" /> was set to <see cref="T:System.Xml.XmlReader" />.</exception>
		/// <exception cref="T:System.Data.SqlClient.SqlException">Any error that occurred while executing the command text.A timeout occurred during a streaming operation. For more information about streaming, see SqlClient Streaming Support.</exception>
		/// <exception cref="T:System.InvalidOperationException">The name/value pair "Asynchronous Processing=true" was not included within the connection string defining the connection for this <see cref="T:System.Data.SqlClient.SqlCommand" />.The <see cref="T:System.Data.SqlClient.SqlConnection" /> closed or dropped during a streaming operation. For more information about streaming, see SqlClient Streaming Support.</exception>
		/// <exception cref="T:System.IO.IOException">An error occurred in a <see cref="T:System.IO.Stream" />, <see cref="T:System.Xml.XmlReader" /> or <see cref="T:System.IO.TextReader" /> object during a streaming operation.  For more information about streaming, see SqlClient Streaming Support.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.Stream" />, <see cref="T:System.Xml.XmlReader" /> or <see cref="T:System.IO.TextReader" /> object was closed during a streaming operation.  For more information about streaming, see SqlClient Streaming Support.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060011C6 RID: 4550 RVA: 0x0005A8EC File Offset: 0x00058AEC
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public IAsyncResult BeginExecuteReader(AsyncCallback callback, object stateObject)
		{
			return this.BeginExecuteReader(CommandBehavior.Default, callback, stateObject);
		}

		/// <summary>Initiates the asynchronous execution of the Transact-SQL statement or stored procedure that is described by this <see cref="T:System.Data.SqlClient.SqlCommand" />, using one of the CommandBehavior values, and retrieving one or more result sets from the server, given a callback procedure and state information. </summary>
		/// <returns>An <see cref="T:System.IAsyncResult" /> that can be used to poll or wait for results, or both; this value is also needed when invoking <see cref="M:System.Data.SqlClient.SqlCommand.EndExecuteReader(System.IAsyncResult)" />, which returns a <see cref="T:System.Data.SqlClient.SqlDataReader" /> instance which can be used to retrieve the returned rows.</returns>
		/// <param name="callback">An <see cref="T:System.AsyncCallback" /> delegate that is invoked when the command's execution has completed. Pass null (Nothing in Microsoft Visual Basic) to indicate that no callback is required.</param>
		/// <param name="stateObject">A user-defined state object that is passed to the callback procedure. Retrieve this object from within the callback procedure using the <see cref="P:System.IAsyncResult.AsyncState" /> property.</param>
		/// <param name="behavior">One of the <see cref="T:System.Data.CommandBehavior" /> values, indicating options for statement execution and data retrieval.</param>
		/// <exception cref="T:System.InvalidCastException">A <see cref="P:System.Data.SqlClient.SqlParameter.SqlDbType" /> other than Binary or VarBinary was used when <see cref="P:System.Data.SqlClient.SqlParameter.Value" /> was set to <see cref="T:System.IO.Stream" />. For more information about streaming, see SqlClient Streaming Support.A <see cref="P:System.Data.SqlClient.SqlParameter.SqlDbType" /> other than Char, NChar, NVarChar, VarChar, or  Xml was used when <see cref="P:System.Data.SqlClient.SqlParameter.Value" /> was set to <see cref="T:System.IO.TextReader" />.A <see cref="P:System.Data.SqlClient.SqlParameter.SqlDbType" /> other than Xml was used when <see cref="P:System.Data.SqlClient.SqlParameter.Value" /> was set to <see cref="T:System.Xml.XmlReader" />.</exception>
		/// <exception cref="T:System.Data.SqlClient.SqlException">Any error that occurred while executing the command text.A timeout occurred during a streaming operation. For more information about streaming, see SqlClient Streaming Support.</exception>
		/// <exception cref="T:System.InvalidOperationException">The name/value pair "Asynchronous Processing=true" was not included within the connection string defining the connection for this <see cref="T:System.Data.SqlClient.SqlCommand" />.The <see cref="T:System.Data.SqlClient.SqlConnection" /> closed or dropped during a streaming operation. For more information about streaming, see SqlClient Streaming Support.</exception>
		/// <exception cref="T:System.IO.IOException">An error occurred in a <see cref="T:System.IO.Stream" />, <see cref="T:System.Xml.XmlReader" /> or <see cref="T:System.IO.TextReader" /> object during a streaming operation.  For more information about streaming, see SqlClient Streaming Support.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.Stream" />, <see cref="T:System.Xml.XmlReader" /> or <see cref="T:System.IO.TextReader" /> object was closed during a streaming operation.  For more information about streaming, see SqlClient Streaming Support.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060011C7 RID: 4551 RVA: 0x0005A8F7 File Offset: 0x00058AF7
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public IAsyncResult BeginExecuteReader(AsyncCallback callback, object stateObject, CommandBehavior behavior)
		{
			return this.BeginExecuteReader(behavior, callback, stateObject);
		}

		/// <summary>Initiates the asynchronous execution of the Transact-SQL statement or stored procedure that is described by this <see cref="T:System.Data.SqlClient.SqlCommand" /> using one of the <see cref="T:System.Data.CommandBehavior" /> values.</summary>
		/// <returns>An <see cref="T:System.IAsyncResult" /> that can be used to poll, wait for results, or both; this value is also needed when invoking <see cref="M:System.Data.SqlClient.SqlCommand.EndExecuteReader(System.IAsyncResult)" />, which returns a <see cref="T:System.Data.SqlClient.SqlDataReader" /> instance that can be used to retrieve the returned rows.</returns>
		/// <param name="behavior">One of the <see cref="T:System.Data.CommandBehavior" /> values, indicating options for statement execution and data retrieval.</param>
		/// <exception cref="T:System.InvalidCastException">A <see cref="P:System.Data.SqlClient.SqlParameter.SqlDbType" /> other than Binary or VarBinary was used when <see cref="P:System.Data.SqlClient.SqlParameter.Value" /> was set to <see cref="T:System.IO.Stream" />. For more information about streaming, see SqlClient Streaming Support.A <see cref="P:System.Data.SqlClient.SqlParameter.SqlDbType" /> other than Char, NChar, NVarChar, VarChar, or  Xml was used when <see cref="P:System.Data.SqlClient.SqlParameter.Value" /> was set to <see cref="T:System.IO.TextReader" />.A <see cref="P:System.Data.SqlClient.SqlParameter.SqlDbType" /> other than Xml was used when <see cref="P:System.Data.SqlClient.SqlParameter.Value" /> was set to <see cref="T:System.Xml.XmlReader" />.</exception>
		/// <exception cref="T:System.Data.SqlClient.SqlException">Any error that occurred while executing the command text.A timeout occurred during a streaming operation. For more information about streaming, see SqlClient Streaming Support.</exception>
		/// <exception cref="T:System.InvalidOperationException">The name/value pair "Asynchronous Processing=true" was not included within the connection string defining the connection for this <see cref="T:System.Data.SqlClient.SqlCommand" />.The <see cref="T:System.Data.SqlClient.SqlConnection" /> closed or dropped during a streaming operation. For more information about streaming, see SqlClient Streaming Support.</exception>
		/// <exception cref="T:System.IO.IOException">An error occurred in a <see cref="T:System.IO.Stream" />, <see cref="T:System.Xml.XmlReader" /> or <see cref="T:System.IO.TextReader" /> object during a streaming operation.  For more information about streaming, see SqlClient Streaming Support.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.Stream" />, <see cref="T:System.Xml.XmlReader" /> or <see cref="T:System.IO.TextReader" /> object was closed during a streaming operation.  For more information about streaming, see SqlClient Streaming Support.</exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		///   <IPermission class="System.Security.Permissions.RegistryPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence, ControlPolicy, ControlAppDomain" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Data.SqlClient.SqlClientPermission, System.Data, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060011C8 RID: 4552 RVA: 0x0005A902 File Offset: 0x00058B02
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public IAsyncResult BeginExecuteReader(CommandBehavior behavior)
		{
			return this.BeginExecuteReader(behavior, null, null);
		}

		// Token: 0x060011C9 RID: 4553 RVA: 0x0005A910 File Offset: 0x00058B10
		// Note: this type is marked as 'beforefieldinit'.
		static SqlCommand()
		{
			string[] array = new string[15];
			array[0] = "PARAMETER_NAME";
			array[1] = "PARAMETER_TYPE";
			array[2] = "DATA_TYPE";
			array[4] = "CHARACTER_MAXIMUM_LENGTH";
			array[5] = "NUMERIC_PRECISION";
			array[6] = "NUMERIC_SCALE";
			array[7] = "UDT_CATALOG";
			array[8] = "UDT_SCHEMA";
			array[9] = "TYPE_NAME";
			array[10] = "XML_CATALOGNAME";
			array[11] = "XML_SCHEMANAME";
			array[12] = "XML_SCHEMACOLLECTIONNAME";
			array[13] = "UDT_NAME";
			SqlCommand.PreKatmaiProcParamsNames = array;
			SqlCommand.KatmaiProcParamsNames = new string[]
			{
				"PARAMETER_NAME", "PARAMETER_TYPE", null, "MANAGED_DATA_TYPE", "CHARACTER_MAXIMUM_LENGTH", "NUMERIC_PRECISION", "NUMERIC_SCALE", "TYPE_CATALOG_NAME", "TYPE_SCHEMA_NAME", "TYPE_NAME",
				"XML_CATALOGNAME", "XML_SCHEMANAME", "XML_SCHEMACOLLECTIONNAME", null, "SS_DATETIME_PRECISION"
			};
		}

		// Token: 0x060011CA RID: 4554 RVA: 0x00010468 File Offset: 0x0000E668
		public SqlCommand(string cmdText, SqlConnection connection, SqlTransaction transaction, SqlCommandColumnEncryptionSetting columnEncryptionSetting)
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x1700031C RID: 796
		// (get) Token: 0x060011CB RID: 4555 RVA: 0x0005AA20 File Offset: 0x00058C20
		public SqlCommandColumnEncryptionSetting ColumnEncryptionSetting
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return SqlCommandColumnEncryptionSetting.UseConnectionSetting;
			}
		}

		// Token: 0x04000B80 RID: 2944
		private string _commandText;

		// Token: 0x04000B81 RID: 2945
		private CommandType _commandType;

		// Token: 0x04000B82 RID: 2946
		private int _commandTimeout;

		// Token: 0x04000B83 RID: 2947
		private UpdateRowSource _updatedRowSource;

		// Token: 0x04000B84 RID: 2948
		private bool _designTimeInvisible;

		// Token: 0x04000B85 RID: 2949
		internal SqlDependency _sqlDep;

		// Token: 0x04000B86 RID: 2950
		private static readonly DiagnosticListener _diagnosticListener = new DiagnosticListener("SqlClientDiagnosticListener");

		// Token: 0x04000B87 RID: 2951
		private bool _parentOperationStarted;

		// Token: 0x04000B88 RID: 2952
		private bool _inPrepare;

		// Token: 0x04000B89 RID: 2953
		private int _prepareHandle;

		// Token: 0x04000B8A RID: 2954
		private bool _hiddenPrepare;

		// Token: 0x04000B8B RID: 2955
		private int _preparedConnectionCloseCount;

		// Token: 0x04000B8C RID: 2956
		private int _preparedConnectionReconnectCount;

		// Token: 0x04000B8D RID: 2957
		private SqlParameterCollection _parameters;

		// Token: 0x04000B8E RID: 2958
		private SqlConnection _activeConnection;

		// Token: 0x04000B8F RID: 2959
		private bool _dirty;

		// Token: 0x04000B90 RID: 2960
		private SqlCommand.EXECTYPE _execType;

		// Token: 0x04000B91 RID: 2961
		private _SqlRPC[] _rpcArrayOf1;

		// Token: 0x04000B92 RID: 2962
		private _SqlMetaDataSet _cachedMetaData;

		// Token: 0x04000B93 RID: 2963
		private TaskCompletionSource<object> _reconnectionCompletionSource;

		// Token: 0x04000B94 RID: 2964
		private SqlCommand.CachedAsyncState _cachedAsyncState;

		// Token: 0x04000B95 RID: 2965
		internal int _rowsAffected;

		// Token: 0x04000B96 RID: 2966
		private SqlNotificationRequest _notification;

		// Token: 0x04000B97 RID: 2967
		private SqlTransaction _transaction;

		// Token: 0x04000B98 RID: 2968
		private StatementCompletedEventHandler _statementCompletedEventHandler;

		// Token: 0x04000B99 RID: 2969
		private TdsParserStateObject _stateObj;

		// Token: 0x04000B9A RID: 2970
		private volatile bool _pendingCancel;

		// Token: 0x04000B9B RID: 2971
		private bool _batchRPCMode;

		// Token: 0x04000B9C RID: 2972
		private List<_SqlRPC> _RPCList;

		// Token: 0x04000B9D RID: 2973
		private _SqlRPC[] _SqlRPCBatchArray;

		// Token: 0x04000B9E RID: 2974
		private List<SqlParameterCollection> _parameterCollectionList;

		// Token: 0x04000B9F RID: 2975
		private int _currentlyExecutingBatch;

		// Token: 0x04000BA0 RID: 2976
		internal static readonly string[] PreKatmaiProcParamsNames;

		// Token: 0x04000BA1 RID: 2977
		internal static readonly string[] KatmaiProcParamsNames;

		// Token: 0x0200016C RID: 364
		private enum EXECTYPE
		{
			// Token: 0x04000BA3 RID: 2979
			UNPREPARED,
			// Token: 0x04000BA4 RID: 2980
			PREPAREPENDING,
			// Token: 0x04000BA5 RID: 2981
			PREPARED
		}

		// Token: 0x0200016D RID: 365
		private class CachedAsyncState
		{
			// Token: 0x060011CC RID: 4556 RVA: 0x0005AA3B File Offset: 0x00058C3B
			internal CachedAsyncState()
			{
			}

			// Token: 0x1700031D RID: 797
			// (get) Token: 0x060011CD RID: 4557 RVA: 0x0005AA51 File Offset: 0x00058C51
			internal SqlDataReader CachedAsyncReader
			{
				get
				{
					return this._cachedAsyncReader;
				}
			}

			// Token: 0x1700031E RID: 798
			// (get) Token: 0x060011CE RID: 4558 RVA: 0x0005AA59 File Offset: 0x00058C59
			internal RunBehavior CachedRunBehavior
			{
				get
				{
					return this._cachedRunBehavior;
				}
			}

			// Token: 0x1700031F RID: 799
			// (get) Token: 0x060011CF RID: 4559 RVA: 0x0005AA61 File Offset: 0x00058C61
			internal string CachedSetOptions
			{
				get
				{
					return this._cachedSetOptions;
				}
			}

			// Token: 0x17000320 RID: 800
			// (get) Token: 0x060011D0 RID: 4560 RVA: 0x0005AA69 File Offset: 0x00058C69
			internal bool PendingAsyncOperation
			{
				get
				{
					return this._cachedAsyncResult != null;
				}
			}

			// Token: 0x17000321 RID: 801
			// (get) Token: 0x060011D1 RID: 4561 RVA: 0x0005AA74 File Offset: 0x00058C74
			internal string EndMethodName
			{
				get
				{
					return this._cachedEndMethod;
				}
			}

			// Token: 0x060011D2 RID: 4562 RVA: 0x0005AA7C File Offset: 0x00058C7C
			internal bool IsActiveConnectionValid(SqlConnection activeConnection)
			{
				return this._cachedAsyncConnection == activeConnection && this._cachedAsyncCloseCount == activeConnection.CloseCount;
			}

			// Token: 0x060011D3 RID: 4563 RVA: 0x0005AA98 File Offset: 0x00058C98
			internal void ResetAsyncState()
			{
				this._cachedAsyncCloseCount = -1;
				this._cachedAsyncResult = null;
				if (this._cachedAsyncConnection != null)
				{
					this._cachedAsyncConnection.AsyncCommandInProgress = false;
					this._cachedAsyncConnection = null;
				}
				this._cachedAsyncReader = null;
				this._cachedRunBehavior = RunBehavior.ReturnImmediately;
				this._cachedSetOptions = null;
				this._cachedEndMethod = null;
			}

			// Token: 0x060011D4 RID: 4564 RVA: 0x0005AAEC File Offset: 0x00058CEC
			internal void SetActiveConnectionAndResult(TaskCompletionSource<object> completion, string endMethod, SqlConnection activeConnection)
			{
				TdsParser tdsParser = ((activeConnection != null) ? activeConnection.Parser : null);
				if (tdsParser == null || tdsParser.State == TdsParserState.Closed || tdsParser.State == TdsParserState.Broken)
				{
					throw ADP.ClosedConnectionError();
				}
				this._cachedAsyncCloseCount = activeConnection.CloseCount;
				this._cachedAsyncResult = completion;
				if (!tdsParser.MARSOn && activeConnection.AsyncCommandInProgress)
				{
					throw SQL.MARSUnspportedOnConnection();
				}
				this._cachedAsyncConnection = activeConnection;
				this._cachedAsyncConnection.AsyncCommandInProgress = true;
				this._cachedEndMethod = endMethod;
			}

			// Token: 0x060011D5 RID: 4565 RVA: 0x0005AB63 File Offset: 0x00058D63
			internal void SetAsyncReaderState(SqlDataReader ds, RunBehavior runBehavior, string optionSettings)
			{
				this._cachedAsyncReader = ds;
				this._cachedRunBehavior = runBehavior;
				this._cachedSetOptions = optionSettings;
			}

			// Token: 0x04000BA6 RID: 2982
			private int _cachedAsyncCloseCount = -1;

			// Token: 0x04000BA7 RID: 2983
			private TaskCompletionSource<object> _cachedAsyncResult;

			// Token: 0x04000BA8 RID: 2984
			private SqlConnection _cachedAsyncConnection;

			// Token: 0x04000BA9 RID: 2985
			private SqlDataReader _cachedAsyncReader;

			// Token: 0x04000BAA RID: 2986
			private RunBehavior _cachedRunBehavior = RunBehavior.ReturnImmediately;

			// Token: 0x04000BAB RID: 2987
			private string _cachedSetOptions;

			// Token: 0x04000BAC RID: 2988
			private string _cachedEndMethod;
		}

		// Token: 0x0200016E RID: 366
		private enum ProcParamsColIndex
		{
			// Token: 0x04000BAE RID: 2990
			ParameterName,
			// Token: 0x04000BAF RID: 2991
			ParameterType,
			// Token: 0x04000BB0 RID: 2992
			DataType,
			// Token: 0x04000BB1 RID: 2993
			ManagedDataType,
			// Token: 0x04000BB2 RID: 2994
			CharacterMaximumLength,
			// Token: 0x04000BB3 RID: 2995
			NumericPrecision,
			// Token: 0x04000BB4 RID: 2996
			NumericScale,
			// Token: 0x04000BB5 RID: 2997
			TypeCatalogName,
			// Token: 0x04000BB6 RID: 2998
			TypeSchemaName,
			// Token: 0x04000BB7 RID: 2999
			TypeName,
			// Token: 0x04000BB8 RID: 3000
			XmlSchemaCollectionCatalogName,
			// Token: 0x04000BB9 RID: 3001
			XmlSchemaCollectionSchemaName,
			// Token: 0x04000BBA RID: 3002
			XmlSchemaCollectionName,
			// Token: 0x04000BBB RID: 3003
			UdtTypeName,
			// Token: 0x04000BBC RID: 3004
			DateTimeScale
		}
	}
}
