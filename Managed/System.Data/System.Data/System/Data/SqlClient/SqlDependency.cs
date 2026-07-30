using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.ProviderBase;
using System.Data.Sql;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Xml;

namespace System.Data.SqlClient
{
	/// <summary>The <see cref="T:System.Data.SqlClient.SqlDependency" /> object represents a query notification dependency between an application and an instance of SQL Server. An application can create a <see cref="T:System.Data.SqlClient.SqlDependency" /> object and register to receive notifications via the <see cref="T:System.Data.SqlClient.OnChangeEventHandler" /> event handler.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001B5 RID: 437
	public sealed class SqlDependency
	{
		/// <summary>Creates a new instance of the <see cref="T:System.Data.SqlClient.SqlDependency" /> class with the default settings.</summary>
		// Token: 0x06001461 RID: 5217 RVA: 0x00066435 File Offset: 0x00064635
		public SqlDependency()
			: this(null, null, 0)
		{
		}

		/// <summary>Creates a new instance of the <see cref="T:System.Data.SqlClient.SqlDependency" /> class and associates it with the <see cref="T:System.Data.SqlClient.SqlCommand" /> parameter.</summary>
		/// <param name="command">The <see cref="T:System.Data.SqlClient.SqlCommand" /> object to associate with this <see cref="T:System.Data.SqlClient.SqlDependency" /> object. The constructor will set up a <see cref="T:System.Data.Sql.SqlNotificationRequest" /> object and bind it to the command. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="command" /> parameter is NULL. </exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Data.SqlClient.SqlCommand" /> object already has a <see cref="T:System.Data.Sql.SqlNotificationRequest" /> object assigned to its <see cref="P:System.Data.SqlClient.SqlCommand.Notification" /> property, and that <see cref="T:System.Data.Sql.SqlNotificationRequest" /> is not associated with this dependency. </exception>
		// Token: 0x06001462 RID: 5218 RVA: 0x00066440 File Offset: 0x00064640
		public SqlDependency(SqlCommand command)
			: this(command, null, 0)
		{
		}

		/// <summary>Creates a new instance of the <see cref="T:System.Data.SqlClient.SqlDependency" /> class, associates it with the <see cref="T:System.Data.SqlClient.SqlCommand" /> parameter, and specifies notification options and a time-out value.</summary>
		/// <param name="command">The <see cref="T:System.Data.SqlClient.SqlCommand" /> object to associate with this <see cref="T:System.Data.SqlClient.SqlDependency" /> object. The constructor sets up a <see cref="T:System.Data.Sql.SqlNotificationRequest" /> object and bind it to the command.</param>
		/// <param name="options">The notification request options to be used by this dependency.  <paramref name="null" /> to use the default service. </param>
		/// <param name="timeout">The time-out for this notification in seconds. The default is 0, indicating that the server's time-out should be used.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="command" /> parameter is NULL. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The time-out value is less than zero.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Data.SqlClient.SqlCommand" /> object already has a <see cref="T:System.Data.Sql.SqlNotificationRequest" /> object assigned to its <see cref="P:System.Data.SqlClient.SqlCommand.Notification" /> property and that <see cref="T:System.Data.Sql.SqlNotificationRequest" /> is not associated with this dependency.An attempt was made to create a SqlDependency instance from within SQLCLR.</exception>
		// Token: 0x06001463 RID: 5219 RVA: 0x0006644C File Offset: 0x0006464C
		public SqlDependency(SqlCommand command, string options, int timeout)
		{
			if (timeout < 0)
			{
				throw SQL.InvalidSqlDependencyTimeout("timeout");
			}
			this._timeout = timeout;
			if (options != null)
			{
				this._options = options;
			}
			this.AddCommandInternal(command);
			SqlDependencyPerAppDomainDispatcher.SingletonInstance.AddDependencyEntry(this);
		}

		/// <summary>Gets a value that indicates whether one of the result sets associated with the dependency has changed.</summary>
		/// <returns>A Boolean value indicating whether one of the result sets has changed.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170003C2 RID: 962
		// (get) Token: 0x06001464 RID: 5220 RVA: 0x000664E5 File Offset: 0x000646E5
		public bool HasChanges
		{
			get
			{
				return this._dependencyFired;
			}
		}

		/// <summary>Gets a value that uniquely identifies this instance of the <see cref="T:System.Data.SqlClient.SqlDependency" /> class.</summary>
		/// <returns>A string representation of a GUID that is generated for each instance of the <see cref="T:System.Data.SqlClient.SqlDependency" /> class.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170003C3 RID: 963
		// (get) Token: 0x06001465 RID: 5221 RVA: 0x000664ED File Offset: 0x000646ED
		public string Id
		{
			get
			{
				return this._id;
			}
		}

		// Token: 0x170003C4 RID: 964
		// (get) Token: 0x06001466 RID: 5222 RVA: 0x000664F5 File Offset: 0x000646F5
		internal static string AppDomainKey
		{
			get
			{
				return SqlDependency.s_appDomainKey;
			}
		}

		// Token: 0x170003C5 RID: 965
		// (get) Token: 0x06001467 RID: 5223 RVA: 0x000664FC File Offset: 0x000646FC
		internal DateTime ExpirationTime
		{
			get
			{
				return this._expirationTime;
			}
		}

		// Token: 0x170003C6 RID: 966
		// (get) Token: 0x06001468 RID: 5224 RVA: 0x00066504 File Offset: 0x00064704
		internal string Options
		{
			get
			{
				return this._options;
			}
		}

		// Token: 0x170003C7 RID: 967
		// (get) Token: 0x06001469 RID: 5225 RVA: 0x0006650C File Offset: 0x0006470C
		internal static SqlDependencyProcessDispatcher ProcessDispatcher
		{
			get
			{
				return SqlDependency.s_processDispatcher;
			}
		}

		// Token: 0x170003C8 RID: 968
		// (get) Token: 0x0600146A RID: 5226 RVA: 0x00066513 File Offset: 0x00064713
		internal int Timeout
		{
			get
			{
				return this._timeout;
			}
		}

		/// <summary>Occurs when a notification is received for any of the commands associated with this <see cref="T:System.Data.SqlClient.SqlDependency" /> object.</summary>
		// Token: 0x14000028 RID: 40
		// (add) Token: 0x0600146B RID: 5227 RVA: 0x0006651C File Offset: 0x0006471C
		// (remove) Token: 0x0600146C RID: 5228 RVA: 0x000665A4 File Offset: 0x000647A4
		public event OnChangeEventHandler OnChange
		{
			add
			{
				if (value != null)
				{
					SqlNotificationEventArgs sqlNotificationEventArgs = null;
					object eventHandlerLock = this._eventHandlerLock;
					lock (eventHandlerLock)
					{
						if (this._dependencyFired)
						{
							sqlNotificationEventArgs = new SqlNotificationEventArgs(SqlNotificationType.Subscribe, SqlNotificationInfo.AlreadyChanged, SqlNotificationSource.Client);
						}
						else
						{
							SqlDependency.EventContextPair eventContextPair = new SqlDependency.EventContextPair(value, this);
							if (this._eventList.Contains(eventContextPair))
							{
								throw SQL.SqlDependencyEventNoDuplicate();
							}
							this._eventList.Add(eventContextPair);
						}
					}
					if (sqlNotificationEventArgs != null)
					{
						value(this, sqlNotificationEventArgs);
					}
				}
			}
			remove
			{
				if (value != null)
				{
					SqlDependency.EventContextPair eventContextPair = new SqlDependency.EventContextPair(value, this);
					object eventHandlerLock = this._eventHandlerLock;
					lock (eventHandlerLock)
					{
						int num = this._eventList.IndexOf(eventContextPair);
						if (0 <= num)
						{
							this._eventList.RemoveAt(num);
						}
					}
				}
			}
		}

		/// <summary>Associates a <see cref="T:System.Data.SqlClient.SqlCommand" /> object with this <see cref="T:System.Data.SqlClient.SqlDependency" /> instance.</summary>
		/// <param name="command">A <see cref="T:System.Data.SqlClient.SqlCommand" /> object containing a statement that is valid for notifications. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="command" /> parameter is null. </exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Data.SqlClient.SqlCommand" /> object already has a <see cref="T:System.Data.Sql.SqlNotificationRequest" /> object assigned to its <see cref="P:System.Data.SqlClient.SqlCommand.Notification" /> property, and that <see cref="T:System.Data.Sql.SqlNotificationRequest" /> is not associated with this dependency. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x0600146D RID: 5229 RVA: 0x00066608 File Offset: 0x00064808
		public void AddCommandDependency(SqlCommand command)
		{
			if (command == null)
			{
				throw ADP.ArgumentNull("command");
			}
			this.AddCommandInternal(command);
		}

		/// <summary>Starts the listener for receiving dependency change notifications from the instance of SQL Server specified by the connection string.</summary>
		/// <returns>true if the listener initialized successfully; false if a compatible listener already exists.</returns>
		/// <param name="connectionString">The connection string for the instance of SQL Server from which to obtain change notifications.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="connectionString" /> parameter is NULL.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <paramref name="connectionString" /> parameter is the same as a previous call to this method, but the parameters are different.The method was called from within the CLR.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required <see cref="T:System.Data.SqlClient.SqlClientPermission" /> code access security (CAS) permission.</exception>
		/// <exception cref="T:System.Data.SqlClient.SqlException">A subsequent call to the method has been made with an equivalent <paramref name="connectionString" /> parameter with a different user, or a user that does not default to the same schema.Also, any underlying SqlClient exceptions.</exception>
		// Token: 0x0600146E RID: 5230 RVA: 0x0006661F File Offset: 0x0006481F
		public static bool Start(string connectionString)
		{
			return SqlDependency.Start(connectionString, null, true);
		}

		/// <summary>Starts the listener for receiving dependency change notifications from the instance of SQL Server specified by the connection string using the specified SQL Server Service Broker queue.</summary>
		/// <returns>true if the listener initialized successfully; false if a compatible listener already exists.</returns>
		/// <param name="connectionString">The connection string for the instance of SQL Server from which to obtain change notifications.</param>
		/// <param name="queue">An existing SQL Server Service Broker queue to be used. If null, the default queue is used.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="connectionString" /> parameter is NULL.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <paramref name="connectionString" /> parameter is the same as a previous call to this method, but the parameters are different.The method was called from within the CLR.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required <see cref="T:System.Data.SqlClient.SqlClientPermission" /> code access security (CAS) permission.</exception>
		/// <exception cref="T:System.Data.SqlClient.SqlException">A subsequent call to the method has been made with an equivalent <paramref name="connectionString" /> parameter but a different user, or a user that does not default to the same schema.Also, any underlying SqlClient exceptions.</exception>
		// Token: 0x0600146F RID: 5231 RVA: 0x00066629 File Offset: 0x00064829
		public static bool Start(string connectionString, string queue)
		{
			return SqlDependency.Start(connectionString, queue, false);
		}

		// Token: 0x06001470 RID: 5232 RVA: 0x00066634 File Offset: 0x00064834
		internal static bool Start(string connectionString, string queue, bool useDefaults)
		{
			if (!string.IsNullOrEmpty(connectionString))
			{
				if (!useDefaults && string.IsNullOrEmpty(queue))
				{
					useDefaults = true;
					queue = null;
				}
				bool flag = false;
				bool flag2 = false;
				object obj = SqlDependency.s_startStopLock;
				lock (obj)
				{
					try
					{
						if (SqlDependency.s_processDispatcher == null)
						{
							SqlDependency.s_processDispatcher = SqlDependencyProcessDispatcher.SingletonProcessDispatcher;
						}
						if (useDefaults)
						{
							string text = null;
							DbConnectionPoolIdentity dbConnectionPoolIdentity = null;
							string text2 = null;
							string text3 = null;
							string text4 = null;
							bool flag4 = false;
							RuntimeHelpers.PrepareConstrainedRegions();
							try
							{
								flag2 = SqlDependency.s_processDispatcher.StartWithDefault(connectionString, out text, out dbConnectionPoolIdentity, out text2, out text3, ref text4, SqlDependency.s_appDomainKey, SqlDependencyPerAppDomainDispatcher.SingletonInstance, out flag, out flag4);
								goto IL_00FF;
							}
							finally
							{
								if (flag4 && !flag)
								{
									SqlDependency.IdentityUserNamePair identityUserNamePair = new SqlDependency.IdentityUserNamePair(dbConnectionPoolIdentity, text2);
									SqlDependency.DatabaseServicePair databaseServicePair = new SqlDependency.DatabaseServicePair(text3, text4);
									if (!SqlDependency.AddToServerUserHash(text, identityUserNamePair, databaseServicePair))
									{
										try
										{
											SqlDependency.Stop(connectionString, queue, useDefaults, true);
										}
										catch (Exception ex)
										{
											if (!ADP.IsCatchableExceptionType(ex))
											{
												throw;
											}
											ADP.TraceExceptionWithoutRethrow(ex);
										}
										throw SQL.SqlDependencyDuplicateStart();
									}
								}
							}
						}
						flag2 = SqlDependency.s_processDispatcher.Start(connectionString, queue, SqlDependency.s_appDomainKey, SqlDependencyPerAppDomainDispatcher.SingletonInstance);
						IL_00FF:;
					}
					catch (Exception ex2)
					{
						if (!ADP.IsCatchableExceptionType(ex2))
						{
							throw;
						}
						ADP.TraceExceptionWithoutRethrow(ex2);
						throw;
					}
				}
				return flag2;
			}
			if (connectionString == null)
			{
				throw ADP.ArgumentNull("connectionString");
			}
			throw ADP.Argument("connectionString");
		}

		/// <summary>Stops a listener for a connection specified in a previous <see cref="Overload:System.Data.SqlClient.SqlDependency.Start" /> call.</summary>
		/// <returns>true if the listener was completely stopped; false if the <see cref="T:System.AppDomain" /> was unbound from the listener, but there are is at least one other <see cref="T:System.AppDomain" /> using the same listener.</returns>
		/// <param name="connectionString">Connection string for the instance of SQL Server that was used in a previous <see cref="M:System.Data.SqlClient.SqlDependency.Start(System.String)" /> call.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="connectionString" /> parameter is NULL. </exception>
		/// <exception cref="T:System.InvalidOperationException">The method was called from within SQLCLR.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required <see cref="T:System.Data.SqlClient.SqlClientPermission" /> code access security (CAS) permission.</exception>
		/// <exception cref="T:System.Data.SqlClient.SqlException">An underlying SqlClient exception occurred.</exception>
		// Token: 0x06001471 RID: 5233 RVA: 0x00066794 File Offset: 0x00064994
		public static bool Stop(string connectionString)
		{
			return SqlDependency.Stop(connectionString, null, true, false);
		}

		/// <summary>Stops a listener for a connection specified in a previous <see cref="Overload:System.Data.SqlClient.SqlDependency.Start" /> call.</summary>
		/// <returns>true if the listener was completely stopped; false if the <see cref="T:System.AppDomain" /> was unbound from the listener, but there is at least one other <see cref="T:System.AppDomain" /> using the same listener.</returns>
		/// <param name="connectionString">Connection string for the instance of SQL Server that was used in a previous <see cref="M:System.Data.SqlClient.SqlDependency.Start(System.String,System.String)" /> call.</param>
		/// <param name="queue">The SQL Server Service Broker queue that was used in a previous <see cref="M:System.Data.SqlClient.SqlDependency.Start(System.String,System.String)" /> call.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="connectionString" /> parameter is NULL. </exception>
		/// <exception cref="T:System.InvalidOperationException">The method was called from within SQLCLR.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required <see cref="T:System.Data.SqlClient.SqlClientPermission" /> code access security (CAS) permission.</exception>
		/// <exception cref="T:System.Data.SqlClient.SqlException">And underlying SqlClient exception occurred.</exception>
		// Token: 0x06001472 RID: 5234 RVA: 0x0006679F File Offset: 0x0006499F
		public static bool Stop(string connectionString, string queue)
		{
			return SqlDependency.Stop(connectionString, queue, false, false);
		}

		// Token: 0x06001473 RID: 5235 RVA: 0x000667AC File Offset: 0x000649AC
		internal static bool Stop(string connectionString, string queue, bool useDefaults, bool startFailed)
		{
			if (!string.IsNullOrEmpty(connectionString))
			{
				if (!useDefaults && string.IsNullOrEmpty(queue))
				{
					useDefaults = true;
					queue = null;
				}
				bool flag = false;
				object obj = SqlDependency.s_startStopLock;
				lock (obj)
				{
					if (SqlDependency.s_processDispatcher != null)
					{
						try
						{
							string text = null;
							DbConnectionPoolIdentity dbConnectionPoolIdentity = null;
							string text2 = null;
							string text3 = null;
							string text4 = null;
							if (useDefaults)
							{
								bool flag3 = false;
								RuntimeHelpers.PrepareConstrainedRegions();
								try
								{
									flag = SqlDependency.s_processDispatcher.Stop(connectionString, out text, out dbConnectionPoolIdentity, out text2, out text3, ref text4, SqlDependency.s_appDomainKey, out flag3);
									goto IL_00CB;
								}
								finally
								{
									if (flag3 && !startFailed)
									{
										SqlDependency.IdentityUserNamePair identityUserNamePair = new SqlDependency.IdentityUserNamePair(dbConnectionPoolIdentity, text2);
										SqlDependency.DatabaseServicePair databaseServicePair = new SqlDependency.DatabaseServicePair(text3, text4);
										SqlDependency.RemoveFromServerUserHash(text, identityUserNamePair, databaseServicePair);
									}
								}
							}
							bool flag4;
							flag = SqlDependency.s_processDispatcher.Stop(connectionString, out text, out dbConnectionPoolIdentity, out text2, out text3, ref queue, SqlDependency.s_appDomainKey, out flag4);
							IL_00CB:;
						}
						catch (Exception ex)
						{
							if (!ADP.IsCatchableExceptionType(ex))
							{
								throw;
							}
							ADP.TraceExceptionWithoutRethrow(ex);
						}
					}
				}
				return flag;
			}
			if (connectionString == null)
			{
				throw ADP.ArgumentNull("connectionString");
			}
			throw ADP.Argument("connectionString");
		}

		// Token: 0x06001474 RID: 5236 RVA: 0x000668CC File Offset: 0x00064ACC
		private static bool AddToServerUserHash(string server, SqlDependency.IdentityUserNamePair identityUser, SqlDependency.DatabaseServicePair databaseService)
		{
			bool flag = false;
			Dictionary<string, Dictionary<SqlDependency.IdentityUserNamePair, List<SqlDependency.DatabaseServicePair>>> dictionary = SqlDependency.s_serverUserHash;
			lock (dictionary)
			{
				Dictionary<SqlDependency.IdentityUserNamePair, List<SqlDependency.DatabaseServicePair>> dictionary2;
				if (!SqlDependency.s_serverUserHash.ContainsKey(server))
				{
					dictionary2 = new Dictionary<SqlDependency.IdentityUserNamePair, List<SqlDependency.DatabaseServicePair>>();
					SqlDependency.s_serverUserHash.Add(server, dictionary2);
				}
				else
				{
					dictionary2 = SqlDependency.s_serverUserHash[server];
				}
				List<SqlDependency.DatabaseServicePair> list;
				if (!dictionary2.ContainsKey(identityUser))
				{
					list = new List<SqlDependency.DatabaseServicePair>();
					dictionary2.Add(identityUser, list);
				}
				else
				{
					list = dictionary2[identityUser];
				}
				if (!list.Contains(databaseService))
				{
					list.Add(databaseService);
					flag = true;
				}
			}
			return flag;
		}

		// Token: 0x06001475 RID: 5237 RVA: 0x00066970 File Offset: 0x00064B70
		private static void RemoveFromServerUserHash(string server, SqlDependency.IdentityUserNamePair identityUser, SqlDependency.DatabaseServicePair databaseService)
		{
			Dictionary<string, Dictionary<SqlDependency.IdentityUserNamePair, List<SqlDependency.DatabaseServicePair>>> dictionary = SqlDependency.s_serverUserHash;
			lock (dictionary)
			{
				if (SqlDependency.s_serverUserHash.ContainsKey(server))
				{
					Dictionary<SqlDependency.IdentityUserNamePair, List<SqlDependency.DatabaseServicePair>> dictionary2 = SqlDependency.s_serverUserHash[server];
					if (dictionary2.ContainsKey(identityUser))
					{
						List<SqlDependency.DatabaseServicePair> list = dictionary2[identityUser];
						int num = list.IndexOf(databaseService);
						if (num >= 0)
						{
							list.RemoveAt(num);
							if (list.Count == 0)
							{
								dictionary2.Remove(identityUser);
								if (dictionary2.Count == 0)
								{
									SqlDependency.s_serverUserHash.Remove(server);
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x06001476 RID: 5238 RVA: 0x00066A10 File Offset: 0x00064C10
		internal static string GetDefaultComposedOptions(string server, string failoverServer, SqlDependency.IdentityUserNamePair identityUser, string database)
		{
			Dictionary<string, Dictionary<SqlDependency.IdentityUserNamePair, List<SqlDependency.DatabaseServicePair>>> dictionary = SqlDependency.s_serverUserHash;
			string text2;
			lock (dictionary)
			{
				if (!SqlDependency.s_serverUserHash.ContainsKey(server))
				{
					if (SqlDependency.s_serverUserHash.Count == 0)
					{
						throw SQL.SqlDepDefaultOptionsButNoStart();
					}
					if (string.IsNullOrEmpty(failoverServer) || !SqlDependency.s_serverUserHash.ContainsKey(failoverServer))
					{
						throw SQL.SqlDependencyNoMatchingServerStart();
					}
					server = failoverServer;
				}
				Dictionary<SqlDependency.IdentityUserNamePair, List<SqlDependency.DatabaseServicePair>> dictionary2 = SqlDependency.s_serverUserHash[server];
				List<SqlDependency.DatabaseServicePair> list = null;
				if (!dictionary2.ContainsKey(identityUser))
				{
					if (dictionary2.Count > 1)
					{
						throw SQL.SqlDependencyNoMatchingServerStart();
					}
					using (Dictionary<SqlDependency.IdentityUserNamePair, List<SqlDependency.DatabaseServicePair>>.Enumerator enumerator = dictionary2.GetEnumerator())
					{
						if (!enumerator.MoveNext())
						{
							goto IL_00B6;
						}
						KeyValuePair<SqlDependency.IdentityUserNamePair, List<SqlDependency.DatabaseServicePair>> keyValuePair = enumerator.Current;
						list = keyValuePair.Value;
						goto IL_00B6;
					}
				}
				list = dictionary2[identityUser];
				IL_00B6:
				SqlDependency.DatabaseServicePair databaseServicePair = new SqlDependency.DatabaseServicePair(database, null);
				SqlDependency.DatabaseServicePair databaseServicePair2 = null;
				int num = list.IndexOf(databaseServicePair);
				if (num != -1)
				{
					databaseServicePair2 = list[num];
				}
				if (databaseServicePair2 != null)
				{
					database = SqlDependency.FixupServiceOrDatabaseName(databaseServicePair2.Database);
					string text = SqlDependency.FixupServiceOrDatabaseName(databaseServicePair2.Service);
					text2 = "Service=" + text + ";Local Database=" + database;
				}
				else
				{
					if (list.Count != 1)
					{
						throw SQL.SqlDependencyNoMatchingServerDatabaseStart();
					}
					databaseServicePair2 = (SqlDependency.DatabaseServicePair)list.ToArray()[0];
					string text3 = SqlDependency.FixupServiceOrDatabaseName(databaseServicePair2.Database);
					string text4 = SqlDependency.FixupServiceOrDatabaseName(databaseServicePair2.Service);
					text2 = "Service=" + text4 + ";Local Database=" + text3;
				}
			}
			return text2;
		}

		// Token: 0x06001477 RID: 5239 RVA: 0x00066BC0 File Offset: 0x00064DC0
		internal void AddToServerList(string server)
		{
			List<string> serverList = this._serverList;
			lock (serverList)
			{
				int num = this._serverList.BinarySearch(server, StringComparer.OrdinalIgnoreCase);
				if (0 > num)
				{
					num = ~num;
					this._serverList.Insert(num, server);
				}
			}
		}

		// Token: 0x06001478 RID: 5240 RVA: 0x00066C20 File Offset: 0x00064E20
		internal bool ContainsServer(string server)
		{
			List<string> serverList = this._serverList;
			bool flag2;
			lock (serverList)
			{
				flag2 = this._serverList.Contains(server);
			}
			return flag2;
		}

		// Token: 0x06001479 RID: 5241 RVA: 0x00066C68 File Offset: 0x00064E68
		internal string ComputeHashAndAddToDispatcher(SqlCommand command)
		{
			string text = this.ComputeCommandHash(command.Connection.ConnectionString, command);
			return SqlDependencyPerAppDomainDispatcher.SingletonInstance.AddCommandEntry(text, this);
		}

		// Token: 0x0600147A RID: 5242 RVA: 0x00066C94 File Offset: 0x00064E94
		internal void Invalidate(SqlNotificationType type, SqlNotificationInfo info, SqlNotificationSource source)
		{
			List<SqlDependency.EventContextPair> list = null;
			object eventHandlerLock = this._eventHandlerLock;
			lock (eventHandlerLock)
			{
				if (this._dependencyFired && SqlNotificationInfo.AlreadyChanged != info && SqlNotificationSource.Client != source)
				{
					if (this.ExpirationTime >= DateTime.UtcNow)
					{
					}
				}
				else
				{
					this._dependencyFired = true;
					list = this._eventList;
					this._eventList = new List<SqlDependency.EventContextPair>();
				}
			}
			if (list != null)
			{
				foreach (SqlDependency.EventContextPair eventContextPair in list)
				{
					eventContextPair.Invoke(new SqlNotificationEventArgs(type, info, source));
				}
			}
		}

		// Token: 0x0600147B RID: 5243 RVA: 0x00066D54 File Offset: 0x00064F54
		internal void StartTimer(SqlNotificationRequest notificationRequest)
		{
			if (this._expirationTime == DateTime.MaxValue)
			{
				int num = 432000;
				if (this._timeout != 0)
				{
					num = this._timeout;
				}
				if (notificationRequest != null && notificationRequest.Timeout < num && notificationRequest.Timeout != 0)
				{
					num = notificationRequest.Timeout;
				}
				this._expirationTime = DateTime.UtcNow.AddSeconds((double)num);
				SqlDependencyPerAppDomainDispatcher.SingletonInstance.StartTimer(this);
			}
		}

		// Token: 0x0600147C RID: 5244 RVA: 0x00066DC4 File Offset: 0x00064FC4
		private void AddCommandInternal(SqlCommand cmd)
		{
			if (cmd != null)
			{
				SqlConnection connection = cmd.Connection;
				if (cmd.Notification != null)
				{
					if (cmd._sqlDep == null || cmd._sqlDep != this)
					{
						throw SQL.SqlCommandHasExistingSqlNotificationRequest();
					}
				}
				else
				{
					bool flag = false;
					object eventHandlerLock = this._eventHandlerLock;
					lock (eventHandlerLock)
					{
						if (!this._dependencyFired)
						{
							cmd.Notification = new SqlNotificationRequest
							{
								Timeout = this._timeout
							};
							if (this._options != null)
							{
								cmd.Notification.Options = this._options;
							}
							cmd._sqlDep = this;
						}
						else if (this._eventList.Count == 0)
						{
							flag = true;
						}
					}
					if (flag)
					{
						this.Invalidate(SqlNotificationType.Subscribe, SqlNotificationInfo.AlreadyChanged, SqlNotificationSource.Client);
					}
				}
			}
		}

		// Token: 0x0600147D RID: 5245 RVA: 0x00066E90 File Offset: 0x00065090
		private string ComputeCommandHash(string connectionString, SqlCommand command)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("{0};{1}", connectionString, command.CommandText);
			for (int i = 0; i < command.Parameters.Count; i++)
			{
				object value = command.Parameters[i].Value;
				if (value == null || value == DBNull.Value)
				{
					stringBuilder.Append("; NULL");
				}
				else
				{
					Type type = value.GetType();
					if (type == typeof(byte[]))
					{
						stringBuilder.Append(";");
						byte[] array = (byte[])value;
						for (int j = 0; j < array.Length; j++)
						{
							stringBuilder.Append(array[j].ToString("x2", CultureInfo.InvariantCulture));
						}
					}
					else if (type == typeof(char[]))
					{
						stringBuilder.Append((char[])value);
					}
					else if (type == typeof(XmlReader))
					{
						stringBuilder.Append(";");
						stringBuilder.Append(Guid.NewGuid().ToString());
					}
					else
					{
						stringBuilder.Append(";");
						stringBuilder.Append(value.ToString());
					}
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600147E RID: 5246 RVA: 0x00066FDC File Offset: 0x000651DC
		internal static string FixupServiceOrDatabaseName(string name)
		{
			if (!string.IsNullOrEmpty(name))
			{
				return "\"" + name.Replace("\"", "\"\"") + "\"";
			}
			return name;
		}

		// Token: 0x04000D96 RID: 3478
		private readonly string _id = Guid.NewGuid().ToString() + ";" + SqlDependency.s_appDomainKey;

		// Token: 0x04000D97 RID: 3479
		private string _options;

		// Token: 0x04000D98 RID: 3480
		private int _timeout;

		// Token: 0x04000D99 RID: 3481
		private bool _dependencyFired;

		// Token: 0x04000D9A RID: 3482
		private List<SqlDependency.EventContextPair> _eventList = new List<SqlDependency.EventContextPair>();

		// Token: 0x04000D9B RID: 3483
		private object _eventHandlerLock = new object();

		// Token: 0x04000D9C RID: 3484
		private DateTime _expirationTime = DateTime.MaxValue;

		// Token: 0x04000D9D RID: 3485
		private List<string> _serverList = new List<string>();

		// Token: 0x04000D9E RID: 3486
		private static object s_startStopLock = new object();

		// Token: 0x04000D9F RID: 3487
		private static readonly string s_appDomainKey = Guid.NewGuid().ToString();

		// Token: 0x04000DA0 RID: 3488
		private static Dictionary<string, Dictionary<SqlDependency.IdentityUserNamePair, List<SqlDependency.DatabaseServicePair>>> s_serverUserHash = new Dictionary<string, Dictionary<SqlDependency.IdentityUserNamePair, List<SqlDependency.DatabaseServicePair>>>(StringComparer.OrdinalIgnoreCase);

		// Token: 0x04000DA1 RID: 3489
		private static SqlDependencyProcessDispatcher s_processDispatcher = null;

		// Token: 0x04000DA2 RID: 3490
		private static readonly string s_assemblyName = typeof(SqlDependencyProcessDispatcher).Assembly.FullName;

		// Token: 0x04000DA3 RID: 3491
		private static readonly string s_typeName = typeof(SqlDependencyProcessDispatcher).FullName;

		// Token: 0x020001B6 RID: 438
		internal class IdentityUserNamePair
		{
			// Token: 0x06001480 RID: 5248 RVA: 0x00067079 File Offset: 0x00065279
			internal IdentityUserNamePair(DbConnectionPoolIdentity identity, string userName)
			{
				this._identity = identity;
				this._userName = userName;
			}

			// Token: 0x170003C9 RID: 969
			// (get) Token: 0x06001481 RID: 5249 RVA: 0x0006708F File Offset: 0x0006528F
			internal DbConnectionPoolIdentity Identity
			{
				get
				{
					return this._identity;
				}
			}

			// Token: 0x170003CA RID: 970
			// (get) Token: 0x06001482 RID: 5250 RVA: 0x00067097 File Offset: 0x00065297
			internal string UserName
			{
				get
				{
					return this._userName;
				}
			}

			// Token: 0x06001483 RID: 5251 RVA: 0x000670A0 File Offset: 0x000652A0
			public override bool Equals(object value)
			{
				SqlDependency.IdentityUserNamePair identityUserNamePair = (SqlDependency.IdentityUserNamePair)value;
				bool flag = false;
				if (identityUserNamePair == null)
				{
					flag = false;
				}
				else if (this == identityUserNamePair)
				{
					flag = true;
				}
				else if (this._identity != null)
				{
					if (this._identity.Equals(identityUserNamePair._identity))
					{
						flag = true;
					}
				}
				else if (this._userName == identityUserNamePair._userName)
				{
					flag = true;
				}
				return flag;
			}

			// Token: 0x06001484 RID: 5252 RVA: 0x000670FC File Offset: 0x000652FC
			public override int GetHashCode()
			{
				int num;
				if (this._identity != null)
				{
					num = this._identity.GetHashCode();
				}
				else
				{
					num = this._userName.GetHashCode();
				}
				return num;
			}

			// Token: 0x04000DA4 RID: 3492
			private DbConnectionPoolIdentity _identity;

			// Token: 0x04000DA5 RID: 3493
			private string _userName;
		}

		// Token: 0x020001B7 RID: 439
		private class DatabaseServicePair
		{
			// Token: 0x06001485 RID: 5253 RVA: 0x0006712E File Offset: 0x0006532E
			internal DatabaseServicePair(string database, string service)
			{
				this._database = database;
				this._service = service;
			}

			// Token: 0x170003CB RID: 971
			// (get) Token: 0x06001486 RID: 5254 RVA: 0x00067144 File Offset: 0x00065344
			internal string Database
			{
				get
				{
					return this._database;
				}
			}

			// Token: 0x170003CC RID: 972
			// (get) Token: 0x06001487 RID: 5255 RVA: 0x0006714C File Offset: 0x0006534C
			internal string Service
			{
				get
				{
					return this._service;
				}
			}

			// Token: 0x06001488 RID: 5256 RVA: 0x00067154 File Offset: 0x00065354
			public override bool Equals(object value)
			{
				SqlDependency.DatabaseServicePair databaseServicePair = (SqlDependency.DatabaseServicePair)value;
				bool flag = false;
				if (databaseServicePair == null)
				{
					flag = false;
				}
				else if (this == databaseServicePair)
				{
					flag = true;
				}
				else if (this._database == databaseServicePair._database)
				{
					flag = true;
				}
				return flag;
			}

			// Token: 0x06001489 RID: 5257 RVA: 0x0006718F File Offset: 0x0006538F
			public override int GetHashCode()
			{
				return this._database.GetHashCode();
			}

			// Token: 0x04000DA6 RID: 3494
			private string _database;

			// Token: 0x04000DA7 RID: 3495
			private string _service;
		}

		// Token: 0x020001B8 RID: 440
		internal class EventContextPair
		{
			// Token: 0x0600148A RID: 5258 RVA: 0x0006719C File Offset: 0x0006539C
			internal EventContextPair(OnChangeEventHandler eventHandler, SqlDependency dependency)
			{
				this._eventHandler = eventHandler;
				this._context = ExecutionContext.Capture();
				this._dependency = dependency;
			}

			// Token: 0x0600148B RID: 5259 RVA: 0x000671C0 File Offset: 0x000653C0
			public override bool Equals(object value)
			{
				SqlDependency.EventContextPair eventContextPair = (SqlDependency.EventContextPair)value;
				bool flag = false;
				if (eventContextPair == null)
				{
					flag = false;
				}
				else if (this == eventContextPair)
				{
					flag = true;
				}
				else if (this._eventHandler == eventContextPair._eventHandler)
				{
					flag = true;
				}
				return flag;
			}

			// Token: 0x0600148C RID: 5260 RVA: 0x000671FB File Offset: 0x000653FB
			public override int GetHashCode()
			{
				return this._eventHandler.GetHashCode();
			}

			// Token: 0x0600148D RID: 5261 RVA: 0x00067208 File Offset: 0x00065408
			internal void Invoke(SqlNotificationEventArgs args)
			{
				this._args = args;
				ExecutionContext.Run(this._context, SqlDependency.EventContextPair.s_contextCallback, this);
			}

			// Token: 0x0600148E RID: 5262 RVA: 0x00067224 File Offset: 0x00065424
			private static void InvokeCallback(object eventContextPair)
			{
				SqlDependency.EventContextPair eventContextPair2 = (SqlDependency.EventContextPair)eventContextPair;
				eventContextPair2._eventHandler(eventContextPair2._dependency, eventContextPair2._args);
			}

			// Token: 0x04000DA8 RID: 3496
			private OnChangeEventHandler _eventHandler;

			// Token: 0x04000DA9 RID: 3497
			private ExecutionContext _context;

			// Token: 0x04000DAA RID: 3498
			private SqlDependency _dependency;

			// Token: 0x04000DAB RID: 3499
			private SqlNotificationEventArgs _args;

			// Token: 0x04000DAC RID: 3500
			private static ContextCallback s_contextCallback = new ContextCallback(SqlDependency.EventContextPair.InvokeCallback);
		}
	}
}
