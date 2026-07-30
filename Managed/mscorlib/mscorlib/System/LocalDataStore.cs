using System;
using System.Runtime.CompilerServices;
using System.Security;
using System.Threading;

namespace System
{
	// Token: 0x02000107 RID: 263
	internal sealed class LocalDataStore
	{
		// Token: 0x0600099E RID: 2462 RVA: 0x000319D8 File Offset: 0x0002FBD8
		public LocalDataStore(LocalDataStoreMgr mgr, int InitialCapacity)
		{
			this.m_Manager = mgr;
			this.m_DataTable = new LocalDataStoreElement[InitialCapacity];
		}

		// Token: 0x0600099F RID: 2463 RVA: 0x000319F3 File Offset: 0x0002FBF3
		internal void Dispose()
		{
			this.m_Manager.DeleteLocalDataStore(this);
		}

		// Token: 0x060009A0 RID: 2464 RVA: 0x00031A04 File Offset: 0x0002FC04
		public object GetData(LocalDataStoreSlot slot)
		{
			this.m_Manager.ValidateSlot(slot);
			int slot2 = slot.Slot;
			if (slot2 >= 0)
			{
				if (slot2 >= this.m_DataTable.Length)
				{
					return null;
				}
				LocalDataStoreElement localDataStoreElement = this.m_DataTable[slot2];
				if (localDataStoreElement == null)
				{
					return null;
				}
				if (localDataStoreElement.Cookie == slot.Cookie)
				{
					return localDataStoreElement.Value;
				}
			}
			throw new InvalidOperationException(Environment.GetResourceString("LocalDataStoreSlot storage has been freed."));
		}

		// Token: 0x060009A1 RID: 2465 RVA: 0x00031A68 File Offset: 0x0002FC68
		public void SetData(LocalDataStoreSlot slot, object data)
		{
			this.m_Manager.ValidateSlot(slot);
			int slot2 = slot.Slot;
			if (slot2 >= 0)
			{
				LocalDataStoreElement localDataStoreElement = ((slot2 < this.m_DataTable.Length) ? this.m_DataTable[slot2] : null);
				if (localDataStoreElement == null)
				{
					localDataStoreElement = this.PopulateElement(slot);
				}
				if (localDataStoreElement.Cookie == slot.Cookie)
				{
					localDataStoreElement.Value = data;
					return;
				}
			}
			throw new InvalidOperationException(Environment.GetResourceString("LocalDataStoreSlot storage has been freed."));
		}

		// Token: 0x060009A2 RID: 2466 RVA: 0x00031AD4 File Offset: 0x0002FCD4
		internal void FreeData(int slot, long cookie)
		{
			if (slot >= this.m_DataTable.Length)
			{
				return;
			}
			LocalDataStoreElement localDataStoreElement = this.m_DataTable[slot];
			if (localDataStoreElement != null && localDataStoreElement.Cookie == cookie)
			{
				this.m_DataTable[slot] = null;
			}
		}

		// Token: 0x060009A3 RID: 2467 RVA: 0x00031B0C File Offset: 0x0002FD0C
		[SecuritySafeCritical]
		private LocalDataStoreElement PopulateElement(LocalDataStoreSlot slot)
		{
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			LocalDataStoreElement localDataStoreElement;
			try
			{
				Monitor.Enter(this.m_Manager, ref flag);
				int slot2 = slot.Slot;
				if (slot2 < 0)
				{
					throw new InvalidOperationException(Environment.GetResourceString("LocalDataStoreSlot storage has been freed."));
				}
				if (slot2 >= this.m_DataTable.Length)
				{
					LocalDataStoreElement[] array = new LocalDataStoreElement[this.m_Manager.GetSlotTableLength()];
					Array.Copy(this.m_DataTable, array, this.m_DataTable.Length);
					this.m_DataTable = array;
				}
				if (this.m_DataTable[slot2] == null)
				{
					this.m_DataTable[slot2] = new LocalDataStoreElement(slot.Cookie);
				}
				localDataStoreElement = this.m_DataTable[slot2];
			}
			finally
			{
				if (flag)
				{
					Monitor.Exit(this.m_Manager);
				}
			}
			return localDataStoreElement;
		}

		// Token: 0x0400077C RID: 1916
		private LocalDataStoreElement[] m_DataTable;

		// Token: 0x0400077D RID: 1917
		private LocalDataStoreMgr m_Manager;
	}
}
