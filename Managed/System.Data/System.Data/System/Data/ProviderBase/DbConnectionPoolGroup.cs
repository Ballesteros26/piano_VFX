using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data.Common;

namespace System.Data.ProviderBase
{
	// Token: 0x02000311 RID: 785
	internal sealed class DbConnectionPoolGroup
	{
		// Token: 0x060022EE RID: 8942 RVA: 0x000A27F4 File Offset: 0x000A09F4
		internal DbConnectionPoolGroup(DbConnectionOptions connectionOptions, DbConnectionPoolKey key, DbConnectionPoolGroupOptions poolGroupOptions)
		{
			this._connectionOptions = connectionOptions;
			this._poolKey = key;
			this._poolGroupOptions = poolGroupOptions;
			this._poolCollection = new ConcurrentDictionary<DbConnectionPoolIdentity, DbConnectionPool>();
			this._state = 1;
		}

		// Token: 0x1700060E RID: 1550
		// (get) Token: 0x060022EF RID: 8943 RVA: 0x000A2823 File Offset: 0x000A0A23
		internal DbConnectionOptions ConnectionOptions
		{
			get
			{
				return this._connectionOptions;
			}
		}

		// Token: 0x1700060F RID: 1551
		// (get) Token: 0x060022F0 RID: 8944 RVA: 0x000A282B File Offset: 0x000A0A2B
		internal DbConnectionPoolKey PoolKey
		{
			get
			{
				return this._poolKey;
			}
		}

		// Token: 0x17000610 RID: 1552
		// (get) Token: 0x060022F1 RID: 8945 RVA: 0x000A2833 File Offset: 0x000A0A33
		// (set) Token: 0x060022F2 RID: 8946 RVA: 0x000A283B File Offset: 0x000A0A3B
		internal DbConnectionPoolGroupProviderInfo ProviderInfo
		{
			get
			{
				return this._providerInfo;
			}
			set
			{
				this._providerInfo = value;
				if (value != null)
				{
					this._providerInfo.PoolGroup = this;
				}
			}
		}

		// Token: 0x17000611 RID: 1553
		// (get) Token: 0x060022F3 RID: 8947 RVA: 0x000A2853 File Offset: 0x000A0A53
		internal bool IsDisabled
		{
			get
			{
				return 4 == this._state;
			}
		}

		// Token: 0x17000612 RID: 1554
		// (get) Token: 0x060022F4 RID: 8948 RVA: 0x000A285E File Offset: 0x000A0A5E
		internal DbConnectionPoolGroupOptions PoolGroupOptions
		{
			get
			{
				return this._poolGroupOptions;
			}
		}

		// Token: 0x17000613 RID: 1555
		// (get) Token: 0x060022F5 RID: 8949 RVA: 0x000A2866 File Offset: 0x000A0A66
		// (set) Token: 0x060022F6 RID: 8950 RVA: 0x000A286E File Offset: 0x000A0A6E
		internal DbMetaDataFactory MetaDataFactory
		{
			get
			{
				return this._metaDataFactory;
			}
			set
			{
				this._metaDataFactory = value;
			}
		}

		// Token: 0x060022F7 RID: 8951 RVA: 0x000A2878 File Offset: 0x000A0A78
		internal int Clear()
		{
			ConcurrentDictionary<DbConnectionPoolIdentity, DbConnectionPool> concurrentDictionary = null;
			lock (this)
			{
				if (this._poolCollection.Count > 0)
				{
					concurrentDictionary = this._poolCollection;
					this._poolCollection = new ConcurrentDictionary<DbConnectionPoolIdentity, DbConnectionPool>();
				}
			}
			if (concurrentDictionary != null)
			{
				foreach (KeyValuePair<DbConnectionPoolIdentity, DbConnectionPool> keyValuePair in concurrentDictionary)
				{
					DbConnectionPool value = keyValuePair.Value;
					if (value != null)
					{
						value.ConnectionFactory.QueuePoolForRelease(value, true);
					}
				}
			}
			return this._poolCollection.Count;
		}

		// Token: 0x060022F8 RID: 8952 RVA: 0x000A292C File Offset: 0x000A0B2C
		internal DbConnectionPool GetConnectionPool(DbConnectionFactory connectionFactory)
		{
			DbConnectionPool dbConnectionPool = null;
			if (this._poolGroupOptions != null)
			{
				DbConnectionPoolIdentity dbConnectionPoolIdentity = DbConnectionPoolIdentity.NoIdentity;
				if (this._poolGroupOptions.PoolByIdentity)
				{
					dbConnectionPoolIdentity = DbConnectionPoolIdentity.GetCurrent();
					if (dbConnectionPoolIdentity.IsRestricted)
					{
						dbConnectionPoolIdentity = null;
					}
				}
				if (dbConnectionPoolIdentity != null && !this._poolCollection.TryGetValue(dbConnectionPoolIdentity, out dbConnectionPool))
				{
					DbConnectionPoolGroup dbConnectionPoolGroup = this;
					lock (dbConnectionPoolGroup)
					{
						if (!this._poolCollection.TryGetValue(dbConnectionPoolIdentity, out dbConnectionPool))
						{
							DbConnectionPoolProviderInfo dbConnectionPoolProviderInfo = connectionFactory.CreateConnectionPoolProviderInfo(this.ConnectionOptions);
							DbConnectionPool dbConnectionPool2 = new DbConnectionPool(connectionFactory, this, dbConnectionPoolIdentity, dbConnectionPoolProviderInfo);
							if (this.MarkPoolGroupAsActive())
							{
								dbConnectionPool2.Startup();
								this._poolCollection.TryAdd(dbConnectionPoolIdentity, dbConnectionPool2);
								dbConnectionPool = dbConnectionPool2;
							}
							else
							{
								dbConnectionPool2.Shutdown();
							}
						}
					}
				}
			}
			if (dbConnectionPool == null)
			{
				DbConnectionPoolGroup dbConnectionPoolGroup = this;
				lock (dbConnectionPoolGroup)
				{
					this.MarkPoolGroupAsActive();
				}
			}
			return dbConnectionPool;
		}

		// Token: 0x060022F9 RID: 8953 RVA: 0x000A2A28 File Offset: 0x000A0C28
		private bool MarkPoolGroupAsActive()
		{
			if (2 == this._state)
			{
				this._state = 1;
			}
			return 1 == this._state;
		}

		// Token: 0x060022FA RID: 8954 RVA: 0x000A2A44 File Offset: 0x000A0C44
		internal bool Prune()
		{
			bool flag2;
			lock (this)
			{
				if (this._poolCollection.Count > 0)
				{
					ConcurrentDictionary<DbConnectionPoolIdentity, DbConnectionPool> concurrentDictionary = new ConcurrentDictionary<DbConnectionPoolIdentity, DbConnectionPool>();
					foreach (KeyValuePair<DbConnectionPoolIdentity, DbConnectionPool> keyValuePair in this._poolCollection)
					{
						DbConnectionPool value = keyValuePair.Value;
						if (value != null)
						{
							if (!value.ErrorOccurred && value.Count == 0)
							{
								value.ConnectionFactory.QueuePoolForRelease(value, false);
							}
							else
							{
								concurrentDictionary.TryAdd(keyValuePair.Key, keyValuePair.Value);
							}
						}
					}
					this._poolCollection = concurrentDictionary;
				}
				if (this._poolCollection.Count == 0)
				{
					if (1 == this._state)
					{
						this._state = 2;
					}
					else if (2 == this._state)
					{
						this._state = 4;
					}
				}
				flag2 = 4 == this._state;
			}
			return flag2;
		}

		// Token: 0x04001722 RID: 5922
		private readonly DbConnectionOptions _connectionOptions;

		// Token: 0x04001723 RID: 5923
		private readonly DbConnectionPoolKey _poolKey;

		// Token: 0x04001724 RID: 5924
		private readonly DbConnectionPoolGroupOptions _poolGroupOptions;

		// Token: 0x04001725 RID: 5925
		private ConcurrentDictionary<DbConnectionPoolIdentity, DbConnectionPool> _poolCollection;

		// Token: 0x04001726 RID: 5926
		private int _state;

		// Token: 0x04001727 RID: 5927
		private DbConnectionPoolGroupProviderInfo _providerInfo;

		// Token: 0x04001728 RID: 5928
		private DbMetaDataFactory _metaDataFactory;

		// Token: 0x04001729 RID: 5929
		private const int PoolGroupStateActive = 1;

		// Token: 0x0400172A RID: 5930
		private const int PoolGroupStateIdle = 2;

		// Token: 0x0400172B RID: 5931
		private const int PoolGroupStateDisabled = 4;
	}
}
