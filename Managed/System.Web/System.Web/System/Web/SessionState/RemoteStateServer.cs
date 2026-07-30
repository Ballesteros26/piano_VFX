using System;
using System.Web.Caching;

namespace System.Web.SessionState
{
	// Token: 0x0200049A RID: 1178
	internal class RemoteStateServer : MarshalByRefObject
	{
		// Token: 0x0600357D RID: 13693 RVA: 0x0008B936 File Offset: 0x00089B36
		internal RemoteStateServer()
		{
			this.cache = new Cache();
		}

		// Token: 0x0600357E RID: 13694 RVA: 0x0008B949 File Offset: 0x00089B49
		private void Insert(string id, LockableStateServerItem item)
		{
			this.cache.Insert(id, item, null, Cache.NoAbsoluteExpiration, new TimeSpan(0, item.item.Timeout, 0));
		}

		// Token: 0x0600357F RID: 13695 RVA: 0x0008B970 File Offset: 0x00089B70
		private LockableStateServerItem Retrieve(string id)
		{
			return this.cache[id] as LockableStateServerItem;
		}

		// Token: 0x06003580 RID: 13696 RVA: 0x0008B984 File Offset: 0x00089B84
		internal void CreateUninitializedItem(string id, int timeout)
		{
			LockableStateServerItem lockableStateServerItem = new LockableStateServerItem(new StateServerItem(timeout)
			{
				Action = SessionStateActions.InitializeItem
			});
			this.Insert(id, lockableStateServerItem);
		}

		// Token: 0x06003581 RID: 13697 RVA: 0x0008B9AC File Offset: 0x00089BAC
		internal StateServerItem GetItem(string id, out bool locked, out TimeSpan lockAge, out object lockId, out SessionStateActions actions, bool exclusive)
		{
			locked = false;
			lockAge = TimeSpan.MinValue;
			lockId = int.MinValue;
			actions = SessionStateActions.None;
			LockableStateServerItem lockableStateServerItem = this.Retrieve(id);
			if (lockableStateServerItem == null || lockableStateServerItem.item.IsAbandoned())
			{
				return null;
			}
			try
			{
				lockableStateServerItem.rwlock.AcquireReaderLock(30000);
				if (lockableStateServerItem.item.Locked)
				{
					locked = true;
					lockAge = DateTime.UtcNow.Subtract(lockableStateServerItem.item.LockedTime);
					lockId = lockableStateServerItem.item.LockId;
					return null;
				}
				lockableStateServerItem.rwlock.ReleaseReaderLock();
				if (exclusive)
				{
					lockableStateServerItem.rwlock.AcquireWriterLock(30000);
					lockableStateServerItem.item.Locked = true;
					lockableStateServerItem.item.LockedTime = DateTime.UtcNow;
					lockableStateServerItem.item.LockId++;
					lockId = lockableStateServerItem.item.LockId;
				}
			}
			catch
			{
				throw;
			}
			finally
			{
				if (lockableStateServerItem.rwlock.IsReaderLockHeld)
				{
					lockableStateServerItem.rwlock.ReleaseReaderLock();
				}
				if (lockableStateServerItem.rwlock.IsWriterLockHeld)
				{
					lockableStateServerItem.rwlock.ReleaseWriterLock();
				}
			}
			actions = lockableStateServerItem.item.Action;
			return lockableStateServerItem.item;
		}

		// Token: 0x06003582 RID: 13698 RVA: 0x0008BB14 File Offset: 0x00089D14
		internal void Remove(string id, object lockid)
		{
			this.cache.Remove(id);
		}

		// Token: 0x06003583 RID: 13699 RVA: 0x0008BB24 File Offset: 0x00089D24
		internal void ResetItemTimeout(string id)
		{
			LockableStateServerItem lockableStateServerItem = this.Retrieve(id);
			if (lockableStateServerItem == null)
			{
				return;
			}
			lockableStateServerItem.item.Touch();
		}

		// Token: 0x06003584 RID: 13700 RVA: 0x0008BB48 File Offset: 0x00089D48
		internal void ReleaseItemExclusive(string id, object lockId)
		{
			LockableStateServerItem lockableStateServerItem = this.Retrieve(id);
			if (lockableStateServerItem == null || lockableStateServerItem.item.LockId != (int)lockId)
			{
				return;
			}
			try
			{
				lockableStateServerItem.rwlock.AcquireWriterLock(30000);
				lockableStateServerItem.item.Locked = false;
			}
			catch
			{
				throw;
			}
			finally
			{
				if (lockableStateServerItem.rwlock.IsWriterLockHeld)
				{
					lockableStateServerItem.rwlock.ReleaseWriterLock();
				}
			}
		}

		// Token: 0x06003585 RID: 13701 RVA: 0x0008BBCC File Offset: 0x00089DCC
		internal void SetAndReleaseItemExclusive(string id, byte[] collection_data, byte[] sobjs_data, object lockId, int timeout, bool newItem)
		{
			LockableStateServerItem lockableStateServerItem = this.Retrieve(id);
			bool flag = false;
			if (newItem || lockableStateServerItem == null)
			{
				lockableStateServerItem = new LockableStateServerItem(new StateServerItem(collection_data, sobjs_data, timeout));
				lockableStateServerItem.item.LockId = (int)lockId;
				flag = true;
			}
			else
			{
				if (lockableStateServerItem.item.LockId != (int)lockId)
				{
					return;
				}
				this.Remove(id, lockId);
			}
			try
			{
				lockableStateServerItem.rwlock.AcquireWriterLock(30000);
				lockableStateServerItem.item.Locked = false;
				if (!flag)
				{
					lockableStateServerItem.item.CollectionData = collection_data;
					lockableStateServerItem.item.StaticObjectsData = sobjs_data;
				}
				this.Insert(id, lockableStateServerItem);
			}
			catch
			{
				throw;
			}
			finally
			{
				if (lockableStateServerItem.rwlock.IsWriterLockHeld)
				{
					lockableStateServerItem.rwlock.ReleaseWriterLock();
				}
			}
		}

		// Token: 0x06003586 RID: 13702 RVA: 0x00003BEA File Offset: 0x00001DEA
		public override object InitializeLifetimeService()
		{
			return null;
		}

		// Token: 0x04001D58 RID: 7512
		private const int lockAcquireTimeout = 30000;

		// Token: 0x04001D59 RID: 7513
		private Cache cache;
	}
}
