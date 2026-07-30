using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security;
using System.Threading;

namespace System
{
	// Token: 0x02000109 RID: 265
	internal sealed class LocalDataStoreMgr
	{
		// Token: 0x060009AA RID: 2474 RVA: 0x00031C50 File Offset: 0x0002FE50
		[SecuritySafeCritical]
		public LocalDataStoreHolder CreateLocalDataStore()
		{
			LocalDataStore localDataStore = new LocalDataStore(this, this.m_SlotInfoTable.Length);
			LocalDataStoreHolder localDataStoreHolder = new LocalDataStoreHolder(localDataStore);
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				Monitor.Enter(this, ref flag);
				this.m_ManagedLocalDataStores.Add(localDataStore);
			}
			finally
			{
				if (flag)
				{
					Monitor.Exit(this);
				}
			}
			return localDataStoreHolder;
		}

		// Token: 0x060009AB RID: 2475 RVA: 0x00031CAC File Offset: 0x0002FEAC
		[SecuritySafeCritical]
		public void DeleteLocalDataStore(LocalDataStore store)
		{
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				Monitor.Enter(this, ref flag);
				this.m_ManagedLocalDataStores.Remove(store);
			}
			finally
			{
				if (flag)
				{
					Monitor.Exit(this);
				}
			}
		}

		// Token: 0x060009AC RID: 2476 RVA: 0x00031CF4 File Offset: 0x0002FEF4
		[SecuritySafeCritical]
		public LocalDataStoreSlot AllocateDataSlot()
		{
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			LocalDataStoreSlot localDataStoreSlot2;
			try
			{
				Monitor.Enter(this, ref flag);
				int num = this.m_SlotInfoTable.Length;
				int num2 = this.m_FirstAvailableSlot;
				while (num2 < num && this.m_SlotInfoTable[num2])
				{
					num2++;
				}
				if (num2 >= num)
				{
					int num3;
					if (num < 512)
					{
						num3 = num * 2;
					}
					else
					{
						num3 = num + 128;
					}
					bool[] array = new bool[num3];
					Array.Copy(this.m_SlotInfoTable, array, num);
					this.m_SlotInfoTable = array;
				}
				this.m_SlotInfoTable[num2] = true;
				int num4 = num2;
				long cookieGenerator = this.m_CookieGenerator;
				this.m_CookieGenerator = checked(cookieGenerator + 1L);
				LocalDataStoreSlot localDataStoreSlot = new LocalDataStoreSlot(this, num4, cookieGenerator);
				this.m_FirstAvailableSlot = num2 + 1;
				localDataStoreSlot2 = localDataStoreSlot;
			}
			finally
			{
				if (flag)
				{
					Monitor.Exit(this);
				}
			}
			return localDataStoreSlot2;
		}

		// Token: 0x060009AD RID: 2477 RVA: 0x00031DBC File Offset: 0x0002FFBC
		[SecuritySafeCritical]
		public LocalDataStoreSlot AllocateNamedDataSlot(string name)
		{
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			LocalDataStoreSlot localDataStoreSlot2;
			try
			{
				Monitor.Enter(this, ref flag);
				LocalDataStoreSlot localDataStoreSlot = this.AllocateDataSlot();
				this.m_KeyToSlotMap.Add(name, localDataStoreSlot);
				localDataStoreSlot2 = localDataStoreSlot;
			}
			finally
			{
				if (flag)
				{
					Monitor.Exit(this);
				}
			}
			return localDataStoreSlot2;
		}

		// Token: 0x060009AE RID: 2478 RVA: 0x00031E0C File Offset: 0x0003000C
		[SecuritySafeCritical]
		public LocalDataStoreSlot GetNamedDataSlot(string name)
		{
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			LocalDataStoreSlot localDataStoreSlot;
			try
			{
				Monitor.Enter(this, ref flag);
				LocalDataStoreSlot valueOrDefault = this.m_KeyToSlotMap.GetValueOrDefault(name);
				if (valueOrDefault == null)
				{
					localDataStoreSlot = this.AllocateNamedDataSlot(name);
				}
				else
				{
					localDataStoreSlot = valueOrDefault;
				}
			}
			finally
			{
				if (flag)
				{
					Monitor.Exit(this);
				}
			}
			return localDataStoreSlot;
		}

		// Token: 0x060009AF RID: 2479 RVA: 0x00031E64 File Offset: 0x00030064
		[SecuritySafeCritical]
		public void FreeNamedDataSlot(string name)
		{
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				Monitor.Enter(this, ref flag);
				this.m_KeyToSlotMap.Remove(name);
			}
			finally
			{
				if (flag)
				{
					Monitor.Exit(this);
				}
			}
		}

		// Token: 0x060009B0 RID: 2480 RVA: 0x00031EAC File Offset: 0x000300AC
		[SecuritySafeCritical]
		internal void FreeDataSlot(int slot, long cookie)
		{
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				Monitor.Enter(this, ref flag);
				for (int i = 0; i < this.m_ManagedLocalDataStores.Count; i++)
				{
					this.m_ManagedLocalDataStores[i].FreeData(slot, cookie);
				}
				this.m_SlotInfoTable[slot] = false;
				if (slot < this.m_FirstAvailableSlot)
				{
					this.m_FirstAvailableSlot = slot;
				}
			}
			finally
			{
				if (flag)
				{
					Monitor.Exit(this);
				}
			}
		}

		// Token: 0x060009B1 RID: 2481 RVA: 0x00031F28 File Offset: 0x00030128
		public void ValidateSlot(LocalDataStoreSlot slot)
		{
			if (slot == null || slot.Manager != this)
			{
				throw new ArgumentException(Environment.GetResourceString("Specified slot number was invalid."));
			}
		}

		// Token: 0x060009B2 RID: 2482 RVA: 0x00031F46 File Offset: 0x00030146
		internal int GetSlotTableLength()
		{
			return this.m_SlotInfoTable.Length;
		}

		// Token: 0x04000781 RID: 1921
		private const int InitialSlotTableSize = 64;

		// Token: 0x04000782 RID: 1922
		private const int SlotTableDoubleThreshold = 512;

		// Token: 0x04000783 RID: 1923
		private const int LargeSlotTableSizeIncrease = 128;

		// Token: 0x04000784 RID: 1924
		private bool[] m_SlotInfoTable = new bool[64];

		// Token: 0x04000785 RID: 1925
		private int m_FirstAvailableSlot;

		// Token: 0x04000786 RID: 1926
		private List<LocalDataStore> m_ManagedLocalDataStores = new List<LocalDataStore>();

		// Token: 0x04000787 RID: 1927
		private Dictionary<string, LocalDataStoreSlot> m_KeyToSlotMap = new Dictionary<string, LocalDataStoreSlot>();

		// Token: 0x04000788 RID: 1928
		private long m_CookieGenerator;
	}
}
