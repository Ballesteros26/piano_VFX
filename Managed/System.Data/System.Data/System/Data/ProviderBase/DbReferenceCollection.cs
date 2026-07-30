using System;
using System.Threading;

namespace System.Data.ProviderBase
{
	// Token: 0x02000317 RID: 791
	internal abstract class DbReferenceCollection
	{
		// Token: 0x0600231F RID: 8991 RVA: 0x000A3719 File Offset: 0x000A1919
		protected DbReferenceCollection()
		{
			this._items = new DbReferenceCollection.CollectionEntry[20];
			this._itemLock = new object();
			this._optimisticCount = 0;
			this._lastItemIndex = 0;
		}

		// Token: 0x06002320 RID: 8992
		public abstract void Add(object value, int tag);

		// Token: 0x06002321 RID: 8993 RVA: 0x000A3748 File Offset: 0x000A1948
		protected void AddItem(object value, int tag)
		{
			bool flag = false;
			object itemLock = this._itemLock;
			lock (itemLock)
			{
				for (int i = 0; i <= this._lastItemIndex; i++)
				{
					if (this._items[i].Tag == 0)
					{
						this._items[i].NewTarget(tag, value);
						flag = true;
						break;
					}
				}
				if (!flag && this._lastItemIndex + 1 < this._items.Length)
				{
					this._lastItemIndex++;
					this._items[this._lastItemIndex].NewTarget(tag, value);
					flag = true;
				}
				if (!flag)
				{
					for (int j = 0; j <= this._lastItemIndex; j++)
					{
						if (!this._items[j].HasTarget)
						{
							this._items[j].NewTarget(tag, value);
							flag = true;
							break;
						}
					}
				}
				if (!flag)
				{
					Array.Resize<DbReferenceCollection.CollectionEntry>(ref this._items, this._items.Length * 2);
					this._lastItemIndex++;
					this._items[this._lastItemIndex].NewTarget(tag, value);
				}
				this._optimisticCount++;
			}
		}

		// Token: 0x06002322 RID: 8994 RVA: 0x000A3898 File Offset: 0x000A1A98
		internal T FindItem<T>(int tag, Func<T, bool> filterMethod) where T : class
		{
			bool flag = false;
			try
			{
				this.TryEnterItemLock(ref flag);
				if (flag && this._optimisticCount > 0)
				{
					for (int i = 0; i <= this._lastItemIndex; i++)
					{
						if (this._items[i].Tag == tag)
						{
							object target = this._items[i].Target;
							if (target != null)
							{
								T t = target as T;
								if (t != null && filterMethod(t))
								{
									return t;
								}
							}
						}
					}
				}
			}
			finally
			{
				this.ExitItemLockIfNeeded(flag);
			}
			return default(T);
		}

		// Token: 0x06002323 RID: 8995 RVA: 0x000A3940 File Offset: 0x000A1B40
		public void Notify(int message)
		{
			bool flag = false;
			try
			{
				this.TryEnterItemLock(ref flag);
				if (flag)
				{
					try
					{
						this._isNotifying = true;
						if (this._optimisticCount > 0)
						{
							for (int i = 0; i <= this._lastItemIndex; i++)
							{
								object target = this._items[i].Target;
								if (target != null)
								{
									this.NotifyItem(message, this._items[i].Tag, target);
									this._items[i].RemoveTarget();
								}
							}
							this._optimisticCount = 0;
						}
						if (this._items.Length > 100)
						{
							this._lastItemIndex = 0;
							this._items = new DbReferenceCollection.CollectionEntry[20];
						}
					}
					finally
					{
						this._isNotifying = false;
					}
				}
			}
			finally
			{
				this.ExitItemLockIfNeeded(flag);
			}
		}

		// Token: 0x06002324 RID: 8996
		protected abstract void NotifyItem(int message, int tag, object value);

		// Token: 0x06002325 RID: 8997
		public abstract void Remove(object value);

		// Token: 0x06002326 RID: 8998 RVA: 0x000A3A18 File Offset: 0x000A1C18
		protected void RemoveItem(object value)
		{
			bool flag = false;
			try
			{
				this.TryEnterItemLock(ref flag);
				if (flag && this._optimisticCount > 0)
				{
					for (int i = 0; i <= this._lastItemIndex; i++)
					{
						if (value == this._items[i].Target)
						{
							this._items[i].RemoveTarget();
							this._optimisticCount--;
							break;
						}
					}
				}
			}
			finally
			{
				this.ExitItemLockIfNeeded(flag);
			}
		}

		// Token: 0x06002327 RID: 8999 RVA: 0x000A3A9C File Offset: 0x000A1C9C
		private void TryEnterItemLock(ref bool lockObtained)
		{
			lockObtained = false;
			while (!this._isNotifying && !lockObtained)
			{
				Monitor.TryEnter(this._itemLock, 100, ref lockObtained);
			}
		}

		// Token: 0x06002328 RID: 9000 RVA: 0x000A3ABF File Offset: 0x000A1CBF
		private void ExitItemLockIfNeeded(bool lockObtained)
		{
			if (lockObtained)
			{
				Monitor.Exit(this._itemLock);
			}
		}

		// Token: 0x0400174B RID: 5963
		private const int LockPollTime = 100;

		// Token: 0x0400174C RID: 5964
		private const int DefaultCollectionSize = 20;

		// Token: 0x0400174D RID: 5965
		private DbReferenceCollection.CollectionEntry[] _items;

		// Token: 0x0400174E RID: 5966
		private readonly object _itemLock;

		// Token: 0x0400174F RID: 5967
		private int _optimisticCount;

		// Token: 0x04001750 RID: 5968
		private int _lastItemIndex;

		// Token: 0x04001751 RID: 5969
		private volatile bool _isNotifying;

		// Token: 0x02000318 RID: 792
		private struct CollectionEntry
		{
			// Token: 0x06002329 RID: 9001 RVA: 0x000A3ACF File Offset: 0x000A1CCF
			public void NewTarget(int tag, object target)
			{
				if (this._weak == null)
				{
					this._weak = new WeakReference(target, false);
				}
				else
				{
					this._weak.Target = target;
				}
				this._tag = tag;
			}

			// Token: 0x0600232A RID: 9002 RVA: 0x000A3AFB File Offset: 0x000A1CFB
			public void RemoveTarget()
			{
				this._tag = 0;
			}

			// Token: 0x17000620 RID: 1568
			// (get) Token: 0x0600232B RID: 9003 RVA: 0x000A3B04 File Offset: 0x000A1D04
			public bool HasTarget
			{
				get
				{
					return this._tag != 0 && this._weak.IsAlive;
				}
			}

			// Token: 0x17000621 RID: 1569
			// (get) Token: 0x0600232C RID: 9004 RVA: 0x000A3B1B File Offset: 0x000A1D1B
			public int Tag
			{
				get
				{
					return this._tag;
				}
			}

			// Token: 0x17000622 RID: 1570
			// (get) Token: 0x0600232D RID: 9005 RVA: 0x000A3B23 File Offset: 0x000A1D23
			public object Target
			{
				get
				{
					if (this._tag != 0)
					{
						return this._weak.Target;
					}
					return null;
				}
			}

			// Token: 0x04001752 RID: 5970
			private int _tag;

			// Token: 0x04001753 RID: 5971
			private WeakReference _weak;
		}
	}
}
