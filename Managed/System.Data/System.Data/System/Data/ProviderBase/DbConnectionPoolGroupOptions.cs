using System;

namespace System.Data.ProviderBase
{
	// Token: 0x02000314 RID: 788
	internal sealed class DbConnectionPoolGroupOptions
	{
		// Token: 0x06002305 RID: 8965 RVA: 0x000A2C5C File Offset: 0x000A0E5C
		public DbConnectionPoolGroupOptions(bool poolByIdentity, int minPoolSize, int maxPoolSize, int creationTimeout, int loadBalanceTimeout, bool hasTransactionAffinity)
		{
			this._poolByIdentity = poolByIdentity;
			this._minPoolSize = minPoolSize;
			this._maxPoolSize = maxPoolSize;
			this._creationTimeout = creationTimeout;
			if (loadBalanceTimeout != 0)
			{
				this._loadBalanceTimeout = new TimeSpan(0, 0, loadBalanceTimeout);
				this._useLoadBalancing = true;
			}
			this._hasTransactionAffinity = hasTransactionAffinity;
		}

		// Token: 0x17000616 RID: 1558
		// (get) Token: 0x06002306 RID: 8966 RVA: 0x000A2CAE File Offset: 0x000A0EAE
		public int CreationTimeout
		{
			get
			{
				return this._creationTimeout;
			}
		}

		// Token: 0x17000617 RID: 1559
		// (get) Token: 0x06002307 RID: 8967 RVA: 0x000A2CB6 File Offset: 0x000A0EB6
		public bool HasTransactionAffinity
		{
			get
			{
				return this._hasTransactionAffinity;
			}
		}

		// Token: 0x17000618 RID: 1560
		// (get) Token: 0x06002308 RID: 8968 RVA: 0x000A2CBE File Offset: 0x000A0EBE
		public TimeSpan LoadBalanceTimeout
		{
			get
			{
				return this._loadBalanceTimeout;
			}
		}

		// Token: 0x17000619 RID: 1561
		// (get) Token: 0x06002309 RID: 8969 RVA: 0x000A2CC6 File Offset: 0x000A0EC6
		public int MaxPoolSize
		{
			get
			{
				return this._maxPoolSize;
			}
		}

		// Token: 0x1700061A RID: 1562
		// (get) Token: 0x0600230A RID: 8970 RVA: 0x000A2CCE File Offset: 0x000A0ECE
		public int MinPoolSize
		{
			get
			{
				return this._minPoolSize;
			}
		}

		// Token: 0x1700061B RID: 1563
		// (get) Token: 0x0600230B RID: 8971 RVA: 0x000A2CD6 File Offset: 0x000A0ED6
		public bool PoolByIdentity
		{
			get
			{
				return this._poolByIdentity;
			}
		}

		// Token: 0x1700061C RID: 1564
		// (get) Token: 0x0600230C RID: 8972 RVA: 0x000A2CDE File Offset: 0x000A0EDE
		public bool UseLoadBalancing
		{
			get
			{
				return this._useLoadBalancing;
			}
		}

		// Token: 0x04001732 RID: 5938
		private readonly bool _poolByIdentity;

		// Token: 0x04001733 RID: 5939
		private readonly int _minPoolSize;

		// Token: 0x04001734 RID: 5940
		private readonly int _maxPoolSize;

		// Token: 0x04001735 RID: 5941
		private readonly int _creationTimeout;

		// Token: 0x04001736 RID: 5942
		private readonly TimeSpan _loadBalanceTimeout;

		// Token: 0x04001737 RID: 5943
		private readonly bool _hasTransactionAffinity;

		// Token: 0x04001738 RID: 5944
		private readonly bool _useLoadBalancing;
	}
}
