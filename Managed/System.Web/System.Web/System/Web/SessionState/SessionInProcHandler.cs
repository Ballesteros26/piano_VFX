using System;
using System.Collections.Specialized;
using System.Threading;
using System.Web.Caching;

namespace System.Web.SessionState
{
	// Token: 0x0200049E RID: 1182
	internal class SessionInProcHandler : SessionStateStoreProviderBase
	{
		// Token: 0x06003598 RID: 13720 RVA: 0x0008BF90 File Offset: 0x0008A190
		public override SessionStateStoreData CreateNewStoreData(HttpContext context, int timeout)
		{
			return new SessionStateStoreData(new SessionStateItemCollection(), this.staticObjects, timeout);
		}

		// Token: 0x06003599 RID: 13721 RVA: 0x0008BFA3 File Offset: 0x0008A1A3
		private void InsertSessionItem(InProcSessionItem item, int timeout, string id)
		{
			if (item == null || string.IsNullOrEmpty(id))
			{
				return;
			}
			HttpRuntime.InternalCache.Insert(id, item, null, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes((double)timeout), CacheItemPriority.AboveNormal, this.removedCB);
		}

		// Token: 0x0600359A RID: 13722 RVA: 0x0008BFD1 File Offset: 0x0008A1D1
		private void UpdateSessionItemTimeout(int timeout, string id)
		{
			if (string.IsNullOrEmpty(id))
			{
				return;
			}
			HttpRuntime.InternalCache.SetItemTimeout(id, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes((double)timeout), true);
		}

		// Token: 0x0600359B RID: 13723 RVA: 0x0008BFF4 File Offset: 0x0008A1F4
		public override void CreateUninitializedItem(HttpContext context, string id, int timeout)
		{
			this.EnsureGoodId(id, true);
			this.InsertSessionItem(new InProcSessionItem
			{
				expiresAt = DateTime.UtcNow.AddMinutes((double)timeout),
				timeout = timeout
			}, timeout, "@@@InProc@" + id);
		}

		// Token: 0x0600359C RID: 13724 RVA: 0x0000393A File Offset: 0x00001B3A
		public override void Dispose()
		{
		}

		// Token: 0x0600359D RID: 13725 RVA: 0x0008C03E File Offset: 0x0008A23E
		public override void EndRequest(HttpContext context)
		{
			if (this.staticObjects != null)
			{
				this.staticObjects.GetObjects().Clear();
				this.staticObjects = null;
			}
		}

		// Token: 0x0600359E RID: 13726 RVA: 0x0008C060 File Offset: 0x0008A260
		private SessionStateStoreData GetItemInternal(HttpContext context, string id, out bool locked, out TimeSpan lockAge, out object lockId, out SessionStateActions actions, bool exclusive)
		{
			locked = false;
			lockAge = TimeSpan.MinValue;
			lockId = int.MinValue;
			actions = SessionStateActions.None;
			if (id == null)
			{
				return null;
			}
			Cache internalCache = HttpRuntime.InternalCache;
			string text = "@@@InProc@" + id;
			InProcSessionItem inProcSessionItem = internalCache[text] as InProcSessionItem;
			if (inProcSessionItem == null)
			{
				return null;
			}
			bool flag = false;
			bool flag2 = false;
			SessionStateStoreData sessionStateStoreData;
			try
			{
				if (!inProcSessionItem.rwlock.TryEnterUpgradeableReadLock(30000))
				{
					throw new ApplicationException("Failed to acquire lock");
				}
				flag = true;
				if (inProcSessionItem.locked)
				{
					locked = true;
					lockAge = DateTime.UtcNow.Subtract(inProcSessionItem.lockedTime);
					lockId = inProcSessionItem.lockId;
					sessionStateStoreData = null;
				}
				else
				{
					if (exclusive)
					{
						if (!inProcSessionItem.rwlock.TryEnterWriteLock(30000))
						{
							throw new ApplicationException("Failed to acquire lock");
						}
						flag2 = true;
						inProcSessionItem.locked = true;
						inProcSessionItem.lockedTime = DateTime.UtcNow;
						inProcSessionItem.lockId++;
						lockId = inProcSessionItem.lockId;
					}
					if (inProcSessionItem.items == null)
					{
						actions = SessionStateActions.InitializeItem;
						inProcSessionItem.items = new SessionStateItemCollection();
					}
					if (inProcSessionItem.staticItems == null)
					{
						inProcSessionItem.staticItems = this.staticObjects;
					}
					sessionStateStoreData = new SessionStateStoreData(inProcSessionItem.items, inProcSessionItem.staticItems, inProcSessionItem.timeout);
				}
			}
			catch
			{
				throw;
			}
			finally
			{
				if (flag2)
				{
					inProcSessionItem.rwlock.ExitWriteLock();
				}
				if (flag)
				{
					inProcSessionItem.rwlock.ExitUpgradeableReadLock();
				}
			}
			return sessionStateStoreData;
		}

		// Token: 0x0600359F RID: 13727 RVA: 0x0008C1EC File Offset: 0x0008A3EC
		public override SessionStateStoreData GetItem(HttpContext context, string id, out bool locked, out TimeSpan lockAge, out object lockId, out SessionStateActions actions)
		{
			this.EnsureGoodId(id, false);
			return this.GetItemInternal(context, id, out locked, out lockAge, out lockId, out actions, false);
		}

		// Token: 0x060035A0 RID: 13728 RVA: 0x0008C206 File Offset: 0x0008A406
		public override SessionStateStoreData GetItemExclusive(HttpContext context, string id, out bool locked, out TimeSpan lockAge, out object lockId, out SessionStateActions actions)
		{
			this.EnsureGoodId(id, false);
			return this.GetItemInternal(context, id, out locked, out lockAge, out lockId, out actions, true);
		}

		// Token: 0x060035A1 RID: 13729 RVA: 0x0008C220 File Offset: 0x0008A420
		public override void Initialize(string name, NameValueCollection config)
		{
			if (string.IsNullOrEmpty(name))
			{
				name = "Session InProc handler";
			}
			this.removedCB = new CacheItemRemovedCallback(this.OnSessionRemoved);
			base.Initialize(name, config);
		}

		// Token: 0x060035A2 RID: 13730 RVA: 0x0008C24B File Offset: 0x0008A44B
		public override void InitializeRequest(HttpContext context)
		{
			this.staticObjects = HttpApplicationFactory.ApplicationState.SessionObjects.Clone();
		}

		// Token: 0x060035A3 RID: 13731 RVA: 0x0008C264 File Offset: 0x0008A464
		public override void ReleaseItemExclusive(HttpContext context, string id, object lockId)
		{
			this.EnsureGoodId(id, true);
			string text = "@@@InProc@" + id;
			InProcSessionItem inProcSessionItem = HttpRuntime.InternalCache[text] as InProcSessionItem;
			if (inProcSessionItem == null || lockId == null || lockId.GetType() != typeof(int) || inProcSessionItem.lockId != (int)lockId)
			{
				return;
			}
			bool flag = false;
			ReaderWriterLockSlim readerWriterLockSlim = null;
			try
			{
				readerWriterLockSlim = inProcSessionItem.rwlock;
				if (readerWriterLockSlim == null || !readerWriterLockSlim.TryEnterWriteLock(30000))
				{
					throw new ApplicationException("Failed to acquire lock");
				}
				flag = true;
				inProcSessionItem.locked = false;
			}
			catch
			{
				throw;
			}
			finally
			{
				if (flag && readerWriterLockSlim != null)
				{
					readerWriterLockSlim.ExitWriteLock();
				}
			}
		}

		// Token: 0x060035A4 RID: 13732 RVA: 0x0008C324 File Offset: 0x0008A524
		public override void RemoveItem(HttpContext context, string id, object lockId, SessionStateStoreData item)
		{
			this.EnsureGoodId(id, true);
			string text = "@@@InProc@" + id;
			Cache internalCache = HttpRuntime.InternalCache;
			InProcSessionItem inProcSessionItem = internalCache[text] as InProcSessionItem;
			if (inProcSessionItem == null || lockId == null || lockId.GetType() != typeof(int) || inProcSessionItem.lockId != (int)lockId)
			{
				return;
			}
			bool flag = false;
			ReaderWriterLockSlim readerWriterLockSlim = null;
			try
			{
				readerWriterLockSlim = inProcSessionItem.rwlock;
				if (readerWriterLockSlim == null || !readerWriterLockSlim.TryEnterWriteLock(30000))
				{
					throw new ApplicationException("Failed to acquire lock after");
				}
				flag = true;
				internalCache.Remove(text);
			}
			catch
			{
				throw;
			}
			finally
			{
				if (flag)
				{
					readerWriterLockSlim.ExitWriteLock();
				}
			}
		}

		// Token: 0x060035A5 RID: 13733 RVA: 0x0008C3E8 File Offset: 0x0008A5E8
		public override void ResetItemTimeout(HttpContext context, string id)
		{
			this.EnsureGoodId(id, true);
			string text = "@@@InProc@" + id;
			InProcSessionItem inProcSessionItem = HttpRuntime.InternalCache[text] as InProcSessionItem;
			if (inProcSessionItem == null)
			{
				return;
			}
			bool flag = false;
			ReaderWriterLockSlim readerWriterLockSlim = null;
			try
			{
				readerWriterLockSlim = inProcSessionItem.rwlock;
				if (readerWriterLockSlim == null || !readerWriterLockSlim.TryEnterWriteLock(30000))
				{
					throw new ApplicationException("Failed to acquire lock after");
				}
				flag = true;
				inProcSessionItem.resettingTimeout = true;
				this.UpdateSessionItemTimeout(inProcSessionItem.timeout, text);
			}
			catch
			{
				throw;
			}
			finally
			{
				if (flag && readerWriterLockSlim != null)
				{
					readerWriterLockSlim.ExitWriteLock();
				}
			}
		}

		// Token: 0x060035A6 RID: 13734 RVA: 0x0008C48C File Offset: 0x0008A68C
		public override void SetAndReleaseItemExclusive(HttpContext context, string id, SessionStateStoreData item, object lockId, bool newItem)
		{
			this.EnsureGoodId(id, true);
			string text = "@@@InProc@" + id;
			Cache internalCache = HttpRuntime.InternalCache;
			InProcSessionItem inProcSessionItem = internalCache[text] as InProcSessionItem;
			ISessionStateItemCollection sessionStateItemCollection = null;
			int num = 20;
			HttpStaticObjectsCollection httpStaticObjectsCollection = null;
			if (item != null)
			{
				sessionStateItemCollection = item.Items;
				num = item.Timeout;
				httpStaticObjectsCollection = item.StaticObjects;
			}
			if (newItem || inProcSessionItem == null)
			{
				inProcSessionItem = new InProcSessionItem();
				inProcSessionItem.timeout = num;
				inProcSessionItem.expiresAt = DateTime.UtcNow.AddMinutes((double)num);
				if (lockId.GetType() == typeof(int))
				{
					inProcSessionItem.lockId = (int)lockId;
				}
			}
			else
			{
				if (lockId == null || lockId.GetType() != typeof(int) || inProcSessionItem.lockId != (int)lockId)
				{
					return;
				}
				inProcSessionItem.resettingTimeout = true;
				internalCache.Remove(text);
			}
			bool flag = false;
			ReaderWriterLockSlim readerWriterLockSlim = null;
			try
			{
				readerWriterLockSlim = inProcSessionItem.rwlock;
				if (readerWriterLockSlim != null && readerWriterLockSlim.TryEnterWriteLock(30000))
				{
					flag = true;
				}
				else if (readerWriterLockSlim != null)
				{
					throw new ApplicationException("Failed to acquire lock");
				}
				if (inProcSessionItem.resettingTimeout)
				{
					this.UpdateSessionItemTimeout(num, text);
				}
				else
				{
					inProcSessionItem.locked = false;
					inProcSessionItem.items = sessionStateItemCollection;
					inProcSessionItem.staticItems = httpStaticObjectsCollection;
					this.InsertSessionItem(inProcSessionItem, num, text);
				}
			}
			catch
			{
				throw;
			}
			finally
			{
				if (flag && readerWriterLockSlim != null)
				{
					readerWriterLockSlim.ExitWriteLock();
				}
			}
		}

		// Token: 0x060035A7 RID: 13735 RVA: 0x0008C60C File Offset: 0x0008A80C
		public override bool SetItemExpireCallback(SessionStateItemExpireCallback expireCallback)
		{
			this.expireCallback = expireCallback;
			return true;
		}

		// Token: 0x060035A8 RID: 13736 RVA: 0x0008C616 File Offset: 0x0008A816
		private void EnsureGoodId(string id, bool throwOnNull)
		{
			if (id == null)
			{
				if (throwOnNull)
				{
					throw new HttpException("Session ID is invalid");
				}
				return;
			}
			else
			{
				if (id.Length > SessionIDManager.SessionIDMaxLength)
				{
					throw new HttpException("Session ID too long");
				}
				return;
			}
		}

		// Token: 0x060035A9 RID: 13737 RVA: 0x0008C644 File Offset: 0x0008A844
		private void OnSessionRemoved(string key, object value, CacheItemRemovedReason reason)
		{
			if (this.expireCallback == null)
			{
				if (value is InProcSessionItem)
				{
					InProcSessionItem inProcSessionItem = (InProcSessionItem)value;
					if (inProcSessionItem.resettingTimeout)
					{
						inProcSessionItem.resettingTimeout = false;
						return;
					}
					inProcSessionItem.Dispose();
				}
				return;
			}
			if (key.StartsWith("@@@InProc@", StringComparison.OrdinalIgnoreCase))
			{
				key = key.Substring(10);
			}
			if (value is SessionStateStoreData)
			{
				this.expireCallback(key, (SessionStateStoreData)value);
				return;
			}
			if (!(value is InProcSessionItem))
			{
				this.expireCallback(key, null);
				return;
			}
			InProcSessionItem inProcSessionItem2 = (InProcSessionItem)value;
			if (inProcSessionItem2.resettingTimeout)
			{
				inProcSessionItem2.resettingTimeout = false;
				return;
			}
			this.expireCallback(key, new SessionStateStoreData(inProcSessionItem2.items, inProcSessionItem2.staticItems, inProcSessionItem2.timeout));
			inProcSessionItem2.Dispose();
		}

		// Token: 0x04001D68 RID: 7528
		private const string CachePrefix = "@@@InProc@";

		// Token: 0x04001D69 RID: 7529
		private const int CachePrefixLength = 10;

		// Token: 0x04001D6A RID: 7530
		private const int lockAcquireTimeout = 30000;

		// Token: 0x04001D6B RID: 7531
		private CacheItemRemovedCallback removedCB;

		// Token: 0x04001D6C RID: 7532
		private SessionStateItemExpireCallback expireCallback;

		// Token: 0x04001D6D RID: 7533
		private HttpStaticObjectsCollection staticObjects;
	}
}
