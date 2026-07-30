using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;

namespace System.Data.ProviderBase
{
	// Token: 0x02000307 RID: 775
	internal abstract class DbConnectionFactory
	{
		// Token: 0x0600224C RID: 8780 RVA: 0x000A0242 File Offset: 0x0009E442
		protected DbConnectionFactory()
		{
			this._connectionPoolGroups = new Dictionary<DbConnectionPoolKey, DbConnectionPoolGroup>();
			this._poolsToRelease = new List<DbConnectionPool>();
			this._poolGroupsToRelease = new List<DbConnectionPoolGroup>();
			this._pruningTimer = this.CreatePruningTimer();
		}

		// Token: 0x170005E1 RID: 1505
		// (get) Token: 0x0600224D RID: 8781
		public abstract DbProviderFactory ProviderFactory { get; }

		// Token: 0x0600224E RID: 8782 RVA: 0x000A0278 File Offset: 0x0009E478
		public void ClearAllPools()
		{
			foreach (KeyValuePair<DbConnectionPoolKey, DbConnectionPoolGroup> keyValuePair in this._connectionPoolGroups)
			{
				DbConnectionPoolGroup value = keyValuePair.Value;
				if (value != null)
				{
					value.Clear();
				}
			}
		}

		// Token: 0x0600224F RID: 8783 RVA: 0x000A02D8 File Offset: 0x0009E4D8
		public void ClearPool(DbConnection connection)
		{
			ADP.CheckArgumentNull(connection, "connection");
			DbConnectionPoolGroup connectionPoolGroup = this.GetConnectionPoolGroup(connection);
			if (connectionPoolGroup != null)
			{
				connectionPoolGroup.Clear();
			}
		}

		// Token: 0x06002250 RID: 8784 RVA: 0x000A0304 File Offset: 0x0009E504
		public void ClearPool(DbConnectionPoolKey key)
		{
			ADP.CheckArgumentNull(key.ConnectionString, "key.ConnectionString");
			DbConnectionPoolGroup dbConnectionPoolGroup;
			if (this._connectionPoolGroups.TryGetValue(key, out dbConnectionPoolGroup))
			{
				dbConnectionPoolGroup.Clear();
			}
		}

		// Token: 0x06002251 RID: 8785 RVA: 0x00004526 File Offset: 0x00002726
		internal virtual DbConnectionPoolProviderInfo CreateConnectionPoolProviderInfo(DbConnectionOptions connectionOptions)
		{
			return null;
		}

		// Token: 0x06002252 RID: 8786 RVA: 0x000A0338 File Offset: 0x0009E538
		internal DbConnectionInternal CreateNonPooledConnection(DbConnection owningConnection, DbConnectionPoolGroup poolGroup, DbConnectionOptions userOptions)
		{
			DbConnectionOptions connectionOptions = poolGroup.ConnectionOptions;
			DbConnectionPoolGroupProviderInfo providerInfo = poolGroup.ProviderInfo;
			DbConnectionPoolKey poolKey = poolGroup.PoolKey;
			DbConnectionInternal dbConnectionInternal = this.CreateConnection(connectionOptions, poolKey, providerInfo, null, owningConnection, userOptions);
			if (dbConnectionInternal != null)
			{
				dbConnectionInternal.MakeNonPooledObject(owningConnection);
			}
			return dbConnectionInternal;
		}

		// Token: 0x06002253 RID: 8787 RVA: 0x000A0374 File Offset: 0x0009E574
		internal DbConnectionInternal CreatePooledConnection(DbConnectionPool pool, DbConnection owningObject, DbConnectionOptions options, DbConnectionPoolKey poolKey, DbConnectionOptions userOptions)
		{
			DbConnectionPoolGroupProviderInfo providerInfo = pool.PoolGroup.ProviderInfo;
			DbConnectionInternal dbConnectionInternal = this.CreateConnection(options, poolKey, providerInfo, pool, owningObject, userOptions);
			if (dbConnectionInternal != null)
			{
				dbConnectionInternal.MakePooledConnection(pool);
			}
			return dbConnectionInternal;
		}

		// Token: 0x06002254 RID: 8788 RVA: 0x00004526 File Offset: 0x00002726
		internal virtual DbConnectionPoolGroupProviderInfo CreateConnectionPoolGroupProviderInfo(DbConnectionOptions connectionOptions)
		{
			return null;
		}

		// Token: 0x06002255 RID: 8789 RVA: 0x000A03A7 File Offset: 0x0009E5A7
		private Timer CreatePruningTimer()
		{
			return new Timer(new TimerCallback(this.PruneConnectionPoolGroups), null, 240000, 30000);
		}

		// Token: 0x06002256 RID: 8790 RVA: 0x000A03C8 File Offset: 0x0009E5C8
		protected DbConnectionOptions FindConnectionOptions(DbConnectionPoolKey key)
		{
			DbConnectionPoolGroup dbConnectionPoolGroup;
			if (!string.IsNullOrEmpty(key.ConnectionString) && this._connectionPoolGroups.TryGetValue(key, out dbConnectionPoolGroup))
			{
				return dbConnectionPoolGroup.ConnectionOptions;
			}
			return null;
		}

		// Token: 0x06002257 RID: 8791 RVA: 0x000A03FA File Offset: 0x0009E5FA
		private static Task<DbConnectionInternal> GetCompletedTask()
		{
			Task<DbConnectionInternal> task;
			if ((task = DbConnectionFactory.s_completedTask) == null)
			{
				task = (DbConnectionFactory.s_completedTask = Task.FromResult<DbConnectionInternal>(null));
			}
			return task;
		}

		// Token: 0x06002258 RID: 8792 RVA: 0x000A0414 File Offset: 0x0009E614
		internal bool TryGetConnection(DbConnection owningConnection, TaskCompletionSource<DbConnectionInternal> retry, DbConnectionOptions userOptions, DbConnectionInternal oldConnection, out DbConnectionInternal connection)
		{
			DbConnectionFactory.<>c__DisplayClass22_0 CS$<>8__locals1 = new DbConnectionFactory.<>c__DisplayClass22_0();
			CS$<>8__locals1.retry = retry;
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.owningConnection = owningConnection;
			CS$<>8__locals1.userOptions = userOptions;
			CS$<>8__locals1.oldConnection = oldConnection;
			connection = null;
			int num = 10;
			int num2 = 1;
			for (;;)
			{
				CS$<>8__locals1.poolGroup = this.GetConnectionPoolGroup(CS$<>8__locals1.owningConnection);
				DbConnectionPool connectionPool = this.GetConnectionPool(CS$<>8__locals1.owningConnection, CS$<>8__locals1.poolGroup);
				if (connectionPool == null)
				{
					CS$<>8__locals1.poolGroup = this.GetConnectionPoolGroup(CS$<>8__locals1.owningConnection);
					if (CS$<>8__locals1.retry != null)
					{
						break;
					}
					connection = this.CreateNonPooledConnection(CS$<>8__locals1.owningConnection, CS$<>8__locals1.poolGroup, CS$<>8__locals1.userOptions);
				}
				else
				{
					if (((SqlConnection)CS$<>8__locals1.owningConnection).ForceNewConnection)
					{
						connection = connectionPool.ReplaceConnection(CS$<>8__locals1.owningConnection, CS$<>8__locals1.userOptions, CS$<>8__locals1.oldConnection);
					}
					else if (!connectionPool.TryGetConnection(CS$<>8__locals1.owningConnection, CS$<>8__locals1.retry, CS$<>8__locals1.userOptions, out connection))
					{
						return false;
					}
					if (connection == null)
					{
						if (connectionPool.IsRunning)
						{
							goto Block_8;
						}
						Thread.Sleep(num2);
						num2 *= 2;
					}
				}
				if (connection != null || num-- <= 0)
				{
					goto IL_0268;
				}
			}
			CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
			Task<DbConnectionInternal>[] array = DbConnectionFactory.s_pendingOpenNonPooled;
			Task<DbConnectionInternal> task3;
			lock (array)
			{
				int i;
				for (i = 0; i < DbConnectionFactory.s_pendingOpenNonPooled.Length; i++)
				{
					Task task4 = DbConnectionFactory.s_pendingOpenNonPooled[i];
					if (task4 == null)
					{
						DbConnectionFactory.s_pendingOpenNonPooled[i] = DbConnectionFactory.GetCompletedTask();
						break;
					}
					if (task4.IsCompleted)
					{
						break;
					}
				}
				if (i == DbConnectionFactory.s_pendingOpenNonPooled.Length)
				{
					i = (int)((ulong)DbConnectionFactory.s_pendingOpenNonPooledNext % (ulong)((long)DbConnectionFactory.s_pendingOpenNonPooled.Length));
					DbConnectionFactory.s_pendingOpenNonPooledNext += 1U;
				}
				Task<DbConnectionInternal> task2 = DbConnectionFactory.s_pendingOpenNonPooled[i];
				Func<Task<DbConnectionInternal>, DbConnectionInternal> func;
				if ((func = CS$<>8__locals1.<>9__1) == null)
				{
					func = (CS$<>8__locals1.<>9__1 = delegate(Task<DbConnectionInternal> _)
					{
						Transaction currentTransaction = ADP.GetCurrentTransaction();
						DbConnectionInternal dbConnectionInternal2;
						try
						{
							ADP.SetCurrentTransaction(CS$<>8__locals1.retry.Task.AsyncState as Transaction);
							DbConnectionInternal dbConnectionInternal = CS$<>8__locals1.<>4__this.CreateNonPooledConnection(CS$<>8__locals1.owningConnection, CS$<>8__locals1.poolGroup, CS$<>8__locals1.userOptions);
							if (CS$<>8__locals1.oldConnection != null && CS$<>8__locals1.oldConnection.State == ConnectionState.Open)
							{
								CS$<>8__locals1.oldConnection.PrepareForReplaceConnection();
								CS$<>8__locals1.oldConnection.Dispose();
							}
							dbConnectionInternal2 = dbConnectionInternal;
						}
						finally
						{
							ADP.SetCurrentTransaction(currentTransaction);
						}
						return dbConnectionInternal2;
					});
				}
				task3 = task2.ContinueWith<DbConnectionInternal>(func, cancellationTokenSource.Token, TaskContinuationOptions.LongRunning, TaskScheduler.Default);
				DbConnectionFactory.s_pendingOpenNonPooled[i] = task3;
			}
			if (CS$<>8__locals1.owningConnection.ConnectionTimeout > 0)
			{
				int num3 = CS$<>8__locals1.owningConnection.ConnectionTimeout * 1000;
				cancellationTokenSource.CancelAfter(num3);
			}
			task3.ContinueWith(delegate(Task<DbConnectionInternal> task)
			{
				cancellationTokenSource.Dispose();
				if (task.IsCanceled)
				{
					CS$<>8__locals1.retry.TrySetException(ADP.ExceptionWithStackTrace(ADP.NonPooledOpenTimeout()));
					return;
				}
				if (task.IsFaulted)
				{
					CS$<>8__locals1.retry.TrySetException(task.Exception.InnerException);
					return;
				}
				if (!CS$<>8__locals1.retry.TrySetResult(task.Result))
				{
					task.Result.DoomThisConnection();
					task.Result.Dispose();
				}
			}, TaskScheduler.Default);
			return false;
			Block_8:
			throw ADP.PooledOpenTimeout();
			IL_0268:
			if (connection == null)
			{
				throw ADP.PooledOpenTimeout();
			}
			return true;
		}

		// Token: 0x06002259 RID: 8793 RVA: 0x000A06A8 File Offset: 0x0009E8A8
		private DbConnectionPool GetConnectionPool(DbConnection owningObject, DbConnectionPoolGroup connectionPoolGroup)
		{
			if (connectionPoolGroup.IsDisabled && connectionPoolGroup.PoolGroupOptions != null)
			{
				DbConnectionPoolGroupOptions poolGroupOptions = connectionPoolGroup.PoolGroupOptions;
				DbConnectionOptions connectionOptions = connectionPoolGroup.ConnectionOptions;
				connectionPoolGroup = this.GetConnectionPoolGroup(connectionPoolGroup.PoolKey, poolGroupOptions, ref connectionOptions);
				this.SetConnectionPoolGroup(owningObject, connectionPoolGroup);
			}
			return connectionPoolGroup.GetConnectionPool(this);
		}

		// Token: 0x0600225A RID: 8794 RVA: 0x000A06F4 File Offset: 0x0009E8F4
		internal DbConnectionPoolGroup GetConnectionPoolGroup(DbConnectionPoolKey key, DbConnectionPoolGroupOptions poolOptions, ref DbConnectionOptions userConnectionOptions)
		{
			if (string.IsNullOrEmpty(key.ConnectionString))
			{
				return null;
			}
			Dictionary<DbConnectionPoolKey, DbConnectionPoolGroup> dictionary = this._connectionPoolGroups;
			DbConnectionPoolGroup dbConnectionPoolGroup;
			if (!dictionary.TryGetValue(key, out dbConnectionPoolGroup) || (dbConnectionPoolGroup.IsDisabled && dbConnectionPoolGroup.PoolGroupOptions != null))
			{
				DbConnectionOptions dbConnectionOptions = this.CreateConnectionOptions(key.ConnectionString, userConnectionOptions);
				if (dbConnectionOptions == null)
				{
					throw ADP.InternalConnectionError(ADP.ConnectionError.ConnectionOptionsMissing);
				}
				if (userConnectionOptions == null)
				{
					userConnectionOptions = dbConnectionOptions;
				}
				if (poolOptions == null)
				{
					if (dbConnectionPoolGroup != null)
					{
						poolOptions = dbConnectionPoolGroup.PoolGroupOptions;
					}
					else
					{
						poolOptions = this.CreateConnectionPoolGroupOptions(dbConnectionOptions);
					}
				}
				lock (this)
				{
					dictionary = this._connectionPoolGroups;
					if (!dictionary.TryGetValue(key, out dbConnectionPoolGroup))
					{
						DbConnectionPoolGroup dbConnectionPoolGroup2 = new DbConnectionPoolGroup(dbConnectionOptions, key, poolOptions);
						dbConnectionPoolGroup2.ProviderInfo = this.CreateConnectionPoolGroupProviderInfo(dbConnectionOptions);
						Dictionary<DbConnectionPoolKey, DbConnectionPoolGroup> dictionary2 = new Dictionary<DbConnectionPoolKey, DbConnectionPoolGroup>(1 + dictionary.Count);
						foreach (KeyValuePair<DbConnectionPoolKey, DbConnectionPoolGroup> keyValuePair in dictionary)
						{
							dictionary2.Add(keyValuePair.Key, keyValuePair.Value);
						}
						dictionary2.Add(key, dbConnectionPoolGroup2);
						dbConnectionPoolGroup = dbConnectionPoolGroup2;
						this._connectionPoolGroups = dictionary2;
					}
					return dbConnectionPoolGroup;
				}
			}
			if (userConnectionOptions == null)
			{
				userConnectionOptions = dbConnectionPoolGroup.ConnectionOptions;
			}
			return dbConnectionPoolGroup;
		}

		// Token: 0x0600225B RID: 8795 RVA: 0x000A0844 File Offset: 0x0009EA44
		private void PruneConnectionPoolGroups(object state)
		{
			List<DbConnectionPool> poolsToRelease = this._poolsToRelease;
			lock (poolsToRelease)
			{
				if (this._poolsToRelease.Count != 0)
				{
					foreach (DbConnectionPool dbConnectionPool in this._poolsToRelease.ToArray())
					{
						if (dbConnectionPool != null)
						{
							dbConnectionPool.Clear();
							if (dbConnectionPool.Count == 0)
							{
								this._poolsToRelease.Remove(dbConnectionPool);
							}
						}
					}
				}
			}
			List<DbConnectionPoolGroup> poolGroupsToRelease = this._poolGroupsToRelease;
			lock (poolGroupsToRelease)
			{
				if (this._poolGroupsToRelease.Count != 0)
				{
					foreach (DbConnectionPoolGroup dbConnectionPoolGroup in this._poolGroupsToRelease.ToArray())
					{
						if (dbConnectionPoolGroup != null && dbConnectionPoolGroup.Clear() == 0)
						{
							this._poolGroupsToRelease.Remove(dbConnectionPoolGroup);
						}
					}
				}
			}
			lock (this)
			{
				Dictionary<DbConnectionPoolKey, DbConnectionPoolGroup> connectionPoolGroups = this._connectionPoolGroups;
				Dictionary<DbConnectionPoolKey, DbConnectionPoolGroup> dictionary = new Dictionary<DbConnectionPoolKey, DbConnectionPoolGroup>(connectionPoolGroups.Count);
				foreach (KeyValuePair<DbConnectionPoolKey, DbConnectionPoolGroup> keyValuePair in connectionPoolGroups)
				{
					if (keyValuePair.Value != null)
					{
						if (keyValuePair.Value.Prune())
						{
							this.QueuePoolGroupForRelease(keyValuePair.Value);
						}
						else
						{
							dictionary.Add(keyValuePair.Key, keyValuePair.Value);
						}
					}
				}
				this._connectionPoolGroups = dictionary;
			}
		}

		// Token: 0x0600225C RID: 8796 RVA: 0x000A09FC File Offset: 0x0009EBFC
		internal void QueuePoolForRelease(DbConnectionPool pool, bool clearing)
		{
			pool.Shutdown();
			List<DbConnectionPool> poolsToRelease = this._poolsToRelease;
			lock (poolsToRelease)
			{
				if (clearing)
				{
					pool.Clear();
				}
				this._poolsToRelease.Add(pool);
			}
		}

		// Token: 0x0600225D RID: 8797 RVA: 0x000A0A54 File Offset: 0x0009EC54
		internal void QueuePoolGroupForRelease(DbConnectionPoolGroup poolGroup)
		{
			List<DbConnectionPoolGroup> poolGroupsToRelease = this._poolGroupsToRelease;
			lock (poolGroupsToRelease)
			{
				this._poolGroupsToRelease.Add(poolGroup);
			}
		}

		// Token: 0x0600225E RID: 8798 RVA: 0x000A0A9C File Offset: 0x0009EC9C
		protected virtual DbConnectionInternal CreateConnection(DbConnectionOptions options, DbConnectionPoolKey poolKey, object poolGroupProviderInfo, DbConnectionPool pool, DbConnection owningConnection, DbConnectionOptions userOptions)
		{
			return this.CreateConnection(options, poolKey, poolGroupProviderInfo, pool, owningConnection);
		}

		// Token: 0x0600225F RID: 8799 RVA: 0x000A0AAC File Offset: 0x0009ECAC
		internal DbMetaDataFactory GetMetaDataFactory(DbConnectionPoolGroup connectionPoolGroup, DbConnectionInternal internalConnection)
		{
			DbMetaDataFactory dbMetaDataFactory = connectionPoolGroup.MetaDataFactory;
			if (dbMetaDataFactory == null)
			{
				bool flag = false;
				dbMetaDataFactory = this.CreateMetaDataFactory(internalConnection, out flag);
				if (flag)
				{
					connectionPoolGroup.MetaDataFactory = dbMetaDataFactory;
				}
			}
			return dbMetaDataFactory;
		}

		// Token: 0x06002260 RID: 8800 RVA: 0x000A0ADA File Offset: 0x0009ECDA
		protected virtual DbMetaDataFactory CreateMetaDataFactory(DbConnectionInternal internalConnection, out bool cacheMetaDataFactory)
		{
			cacheMetaDataFactory = false;
			throw ADP.NotSupported();
		}

		// Token: 0x06002261 RID: 8801
		protected abstract DbConnectionInternal CreateConnection(DbConnectionOptions options, DbConnectionPoolKey poolKey, object poolGroupProviderInfo, DbConnectionPool pool, DbConnection owningConnection);

		// Token: 0x06002262 RID: 8802
		protected abstract DbConnectionOptions CreateConnectionOptions(string connectionString, DbConnectionOptions previous);

		// Token: 0x06002263 RID: 8803
		protected abstract DbConnectionPoolGroupOptions CreateConnectionPoolGroupOptions(DbConnectionOptions options);

		// Token: 0x06002264 RID: 8804
		internal abstract DbConnectionPoolGroup GetConnectionPoolGroup(DbConnection connection);

		// Token: 0x06002265 RID: 8805
		internal abstract DbConnectionInternal GetInnerConnection(DbConnection connection);

		// Token: 0x06002266 RID: 8806
		internal abstract void PermissionDemand(DbConnection outerConnection);

		// Token: 0x06002267 RID: 8807
		internal abstract void SetConnectionPoolGroup(DbConnection outerConnection, DbConnectionPoolGroup poolGroup);

		// Token: 0x06002268 RID: 8808
		internal abstract void SetInnerConnectionEvent(DbConnection owningObject, DbConnectionInternal to);

		// Token: 0x06002269 RID: 8809
		internal abstract bool SetInnerConnectionFrom(DbConnection owningObject, DbConnectionInternal to, DbConnectionInternal from);

		// Token: 0x0600226A RID: 8810
		internal abstract void SetInnerConnectionTo(DbConnection owningObject, DbConnectionInternal to);

		// Token: 0x040016D2 RID: 5842
		private Dictionary<DbConnectionPoolKey, DbConnectionPoolGroup> _connectionPoolGroups;

		// Token: 0x040016D3 RID: 5843
		private readonly List<DbConnectionPool> _poolsToRelease;

		// Token: 0x040016D4 RID: 5844
		private readonly List<DbConnectionPoolGroup> _poolGroupsToRelease;

		// Token: 0x040016D5 RID: 5845
		private readonly Timer _pruningTimer;

		// Token: 0x040016D6 RID: 5846
		private const int PruningDueTime = 240000;

		// Token: 0x040016D7 RID: 5847
		private const int PruningPeriod = 30000;

		// Token: 0x040016D8 RID: 5848
		private static uint s_pendingOpenNonPooledNext = 0U;

		// Token: 0x040016D9 RID: 5849
		private static Task<DbConnectionInternal>[] s_pendingOpenNonPooled = new Task<DbConnectionInternal>[Environment.ProcessorCount];

		// Token: 0x040016DA RID: 5850
		private static Task<DbConnectionInternal> s_completedTask;
	}
}
