using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Globalization;
using System.Threading;

namespace System.Data.SqlClient
{
	// Token: 0x020001B9 RID: 441
	internal class SqlDependencyPerAppDomainDispatcher : MarshalByRefObject
	{
		// Token: 0x06001490 RID: 5264 RVA: 0x00067264 File Offset: 0x00065464
		private SqlDependencyPerAppDomainDispatcher()
		{
			this._dependencyIdToDependencyHash = new Dictionary<string, SqlDependency>();
			this._notificationIdToDependenciesHash = new Dictionary<string, SqlDependencyPerAppDomainDispatcher.DependencyList>();
			this._commandHashToNotificationId = new Dictionary<string, string>();
			this._timeoutTimer = new Timer(new TimerCallback(SqlDependencyPerAppDomainDispatcher.TimeoutTimerCallback), null, -1, -1);
			this.SubscribeToAppDomainUnload();
		}

		// Token: 0x06001491 RID: 5265 RVA: 0x00004526 File Offset: 0x00002726
		public override object InitializeLifetimeService()
		{
			return null;
		}

		// Token: 0x06001492 RID: 5266 RVA: 0x000672C4 File Offset: 0x000654C4
		internal void AddDependencyEntry(SqlDependency dep)
		{
			object instanceLock = this._instanceLock;
			lock (instanceLock)
			{
				this._dependencyIdToDependencyHash.Add(dep.Id, dep);
			}
		}

		// Token: 0x06001493 RID: 5267 RVA: 0x00067310 File Offset: 0x00065510
		internal string AddCommandEntry(string commandHash, SqlDependency dep)
		{
			string text = string.Empty;
			object instanceLock = this._instanceLock;
			lock (instanceLock)
			{
				if (this._dependencyIdToDependencyHash.ContainsKey(dep.Id))
				{
					if (this._commandHashToNotificationId.TryGetValue(commandHash, out text))
					{
						SqlDependencyPerAppDomainDispatcher.DependencyList dependencyList = null;
						if (!this._notificationIdToDependenciesHash.TryGetValue(text, out dependencyList))
						{
							throw ADP.InternalError(ADP.InternalErrorCode.SqlDependencyCommandHashIsNotAssociatedWithNotification);
						}
						if (!dependencyList.Contains(dep))
						{
							dependencyList.Add(dep);
						}
					}
					else
					{
						text = string.Format(CultureInfo.InvariantCulture, "{0};{1}", SqlDependency.AppDomainKey, Guid.NewGuid().ToString("D", CultureInfo.InvariantCulture));
						SqlDependencyPerAppDomainDispatcher.DependencyList dependencyList2 = new SqlDependencyPerAppDomainDispatcher.DependencyList(commandHash);
						dependencyList2.Add(dep);
						this._commandHashToNotificationId.Add(commandHash, text);
						this._notificationIdToDependenciesHash.Add(text, dependencyList2);
					}
				}
			}
			return text;
		}

		// Token: 0x06001494 RID: 5268 RVA: 0x000673FC File Offset: 0x000655FC
		internal void InvalidateCommandID(SqlNotification sqlNotification)
		{
			List<SqlDependency> list = null;
			object instanceLock = this._instanceLock;
			lock (instanceLock)
			{
				list = this.LookupCommandEntryWithRemove(sqlNotification.Key);
				if (list != null)
				{
					foreach (SqlDependency sqlDependency in list)
					{
						this.LookupDependencyEntryWithRemove(sqlDependency.Id);
						this.RemoveDependencyFromCommandToDependenciesHash(sqlDependency);
					}
				}
			}
			if (list != null)
			{
				foreach (SqlDependency sqlDependency2 in list)
				{
					try
					{
						sqlDependency2.Invalidate(sqlNotification.Type, sqlNotification.Info, sqlNotification.Source);
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
		}

		// Token: 0x06001495 RID: 5269 RVA: 0x00067508 File Offset: 0x00065708
		internal void InvalidateServer(string server, SqlNotification sqlNotification)
		{
			List<SqlDependency> list = new List<SqlDependency>();
			object instanceLock = this._instanceLock;
			lock (instanceLock)
			{
				foreach (KeyValuePair<string, SqlDependency> keyValuePair in this._dependencyIdToDependencyHash)
				{
					SqlDependency value = keyValuePair.Value;
					if (value.ContainsServer(server))
					{
						list.Add(value);
					}
				}
				foreach (SqlDependency sqlDependency in list)
				{
					this.LookupDependencyEntryWithRemove(sqlDependency.Id);
					this.RemoveDependencyFromCommandToDependenciesHash(sqlDependency);
				}
			}
			foreach (SqlDependency sqlDependency2 in list)
			{
				try
				{
					sqlDependency2.Invalidate(sqlNotification.Type, sqlNotification.Info, sqlNotification.Source);
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

		// Token: 0x06001496 RID: 5270 RVA: 0x0006765C File Offset: 0x0006585C
		internal SqlDependency LookupDependencyEntry(string id)
		{
			if (id == null)
			{
				throw ADP.ArgumentNull("id");
			}
			if (string.IsNullOrEmpty(id))
			{
				throw SQL.SqlDependencyIdMismatch();
			}
			SqlDependency sqlDependency = null;
			object instanceLock = this._instanceLock;
			lock (instanceLock)
			{
				if (this._dependencyIdToDependencyHash.ContainsKey(id))
				{
					sqlDependency = this._dependencyIdToDependencyHash[id];
				}
			}
			return sqlDependency;
		}

		// Token: 0x06001497 RID: 5271 RVA: 0x000676D0 File Offset: 0x000658D0
		private void LookupDependencyEntryWithRemove(string id)
		{
			object instanceLock = this._instanceLock;
			lock (instanceLock)
			{
				if (this._dependencyIdToDependencyHash.ContainsKey(id))
				{
					this._dependencyIdToDependencyHash.Remove(id);
					if (this._dependencyIdToDependencyHash.Count == 0)
					{
						this._timeoutTimer.Change(-1, -1);
						this._sqlDependencyTimeOutTimerStarted = false;
					}
				}
			}
		}

		// Token: 0x06001498 RID: 5272 RVA: 0x00067748 File Offset: 0x00065948
		private List<SqlDependency> LookupCommandEntryWithRemove(string notificationId)
		{
			SqlDependencyPerAppDomainDispatcher.DependencyList dependencyList = null;
			object instanceLock = this._instanceLock;
			lock (instanceLock)
			{
				if (this._notificationIdToDependenciesHash.TryGetValue(notificationId, out dependencyList))
				{
					this._notificationIdToDependenciesHash.Remove(notificationId);
					this._commandHashToNotificationId.Remove(dependencyList.CommandHash);
				}
			}
			return dependencyList;
		}

		// Token: 0x06001499 RID: 5273 RVA: 0x000677B4 File Offset: 0x000659B4
		private void RemoveDependencyFromCommandToDependenciesHash(SqlDependency dependency)
		{
			object instanceLock = this._instanceLock;
			lock (instanceLock)
			{
				List<string> list = new List<string>();
				List<string> list2 = new List<string>();
				foreach (KeyValuePair<string, SqlDependencyPerAppDomainDispatcher.DependencyList> keyValuePair in this._notificationIdToDependenciesHash)
				{
					SqlDependencyPerAppDomainDispatcher.DependencyList value = keyValuePair.Value;
					if (value.Remove(dependency) && value.Count == 0)
					{
						list.Add(keyValuePair.Key);
						list2.Add(keyValuePair.Value.CommandHash);
					}
				}
				for (int i = 0; i < list.Count; i++)
				{
					this._notificationIdToDependenciesHash.Remove(list[i]);
					this._commandHashToNotificationId.Remove(list2[i]);
				}
			}
		}

		// Token: 0x0600149A RID: 5274 RVA: 0x000678B0 File Offset: 0x00065AB0
		internal void StartTimer(SqlDependency dep)
		{
			object instanceLock = this._instanceLock;
			lock (instanceLock)
			{
				if (!this._sqlDependencyTimeOutTimerStarted)
				{
					this._timeoutTimer.Change(15000, 15000);
					this._nextTimeout = dep.ExpirationTime;
					this._sqlDependencyTimeOutTimerStarted = true;
				}
				else if (this._nextTimeout > dep.ExpirationTime)
				{
					this._nextTimeout = dep.ExpirationTime;
				}
			}
		}

		// Token: 0x0600149B RID: 5275 RVA: 0x0006793C File Offset: 0x00065B3C
		private static void TimeoutTimerCallback(object state)
		{
			object obj = SqlDependencyPerAppDomainDispatcher.SingletonInstance._instanceLock;
			SqlDependency[] array;
			lock (obj)
			{
				if (SqlDependencyPerAppDomainDispatcher.SingletonInstance._dependencyIdToDependencyHash.Count == 0)
				{
					return;
				}
				if (SqlDependencyPerAppDomainDispatcher.SingletonInstance._nextTimeout > DateTime.UtcNow)
				{
					return;
				}
				array = new SqlDependency[SqlDependencyPerAppDomainDispatcher.SingletonInstance._dependencyIdToDependencyHash.Count];
				SqlDependencyPerAppDomainDispatcher.SingletonInstance._dependencyIdToDependencyHash.Values.CopyTo(array, 0);
			}
			DateTime utcNow = DateTime.UtcNow;
			DateTime dateTime = DateTime.MaxValue;
			int i = 0;
			while (i < array.Length)
			{
				if (array[i].ExpirationTime <= utcNow)
				{
					try
					{
						array[i].Invalidate(SqlNotificationType.Change, SqlNotificationInfo.Error, SqlNotificationSource.Timeout);
						goto IL_00E0;
					}
					catch (Exception ex)
					{
						if (!ADP.IsCatchableExceptionType(ex))
						{
							throw;
						}
						ADP.TraceExceptionWithoutRethrow(ex);
						goto IL_00E0;
					}
					goto IL_00C0;
				}
				goto IL_00C0;
				IL_00E0:
				i++;
				continue;
				IL_00C0:
				if (array[i].ExpirationTime < dateTime)
				{
					dateTime = array[i].ExpirationTime;
				}
				array[i] = null;
				goto IL_00E0;
			}
			obj = SqlDependencyPerAppDomainDispatcher.SingletonInstance._instanceLock;
			lock (obj)
			{
				for (int j = 0; j < array.Length; j++)
				{
					if (array[j] != null)
					{
						SqlDependencyPerAppDomainDispatcher.SingletonInstance._dependencyIdToDependencyHash.Remove(array[j].Id);
					}
				}
				if (dateTime < SqlDependencyPerAppDomainDispatcher.SingletonInstance._nextTimeout)
				{
					SqlDependencyPerAppDomainDispatcher.SingletonInstance._nextTimeout = dateTime;
				}
			}
		}

		// Token: 0x0600149C RID: 5276 RVA: 0x00005E03 File Offset: 0x00004003
		private void SubscribeToAppDomainUnload()
		{
		}

		// Token: 0x04000DAD RID: 3501
		internal static readonly SqlDependencyPerAppDomainDispatcher SingletonInstance = new SqlDependencyPerAppDomainDispatcher();

		// Token: 0x04000DAE RID: 3502
		internal object _instanceLock = new object();

		// Token: 0x04000DAF RID: 3503
		private Dictionary<string, SqlDependency> _dependencyIdToDependencyHash;

		// Token: 0x04000DB0 RID: 3504
		private Dictionary<string, SqlDependencyPerAppDomainDispatcher.DependencyList> _notificationIdToDependenciesHash;

		// Token: 0x04000DB1 RID: 3505
		private Dictionary<string, string> _commandHashToNotificationId;

		// Token: 0x04000DB2 RID: 3506
		private bool _sqlDependencyTimeOutTimerStarted;

		// Token: 0x04000DB3 RID: 3507
		private DateTime _nextTimeout;

		// Token: 0x04000DB4 RID: 3508
		private Timer _timeoutTimer;

		// Token: 0x020001BA RID: 442
		private sealed class DependencyList : List<SqlDependency>
		{
			// Token: 0x0600149E RID: 5278 RVA: 0x00067ADC File Offset: 0x00065CDC
			internal DependencyList(string commandHash)
			{
				this.CommandHash = commandHash;
			}

			// Token: 0x04000DB5 RID: 3509
			public readonly string CommandHash;
		}
	}
}
