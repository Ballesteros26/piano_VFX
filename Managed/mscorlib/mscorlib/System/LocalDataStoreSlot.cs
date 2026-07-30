using System;
using System.Runtime.InteropServices;
using Unity;

namespace System
{
	/// <summary>Encapsulates a memory slot to store local data. This class cannot be inherited.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000108 RID: 264
	[ComVisible(true)]
	public sealed class LocalDataStoreSlot
	{
		// Token: 0x060009A4 RID: 2468 RVA: 0x00031BC8 File Offset: 0x0002FDC8
		internal LocalDataStoreSlot(LocalDataStoreMgr mgr, int slot, long cookie)
		{
			this.m_mgr = mgr;
			this.m_slot = slot;
			this.m_cookie = cookie;
		}

		// Token: 0x1700019C RID: 412
		// (get) Token: 0x060009A5 RID: 2469 RVA: 0x00031BE5 File Offset: 0x0002FDE5
		internal LocalDataStoreMgr Manager
		{
			get
			{
				return this.m_mgr;
			}
		}

		// Token: 0x1700019D RID: 413
		// (get) Token: 0x060009A6 RID: 2470 RVA: 0x00031BED File Offset: 0x0002FDED
		internal int Slot
		{
			get
			{
				return this.m_slot;
			}
		}

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x060009A7 RID: 2471 RVA: 0x00031BF5 File Offset: 0x0002FDF5
		internal long Cookie
		{
			get
			{
				return this.m_cookie;
			}
		}

		// Token: 0x060009A8 RID: 2472 RVA: 0x00031C00 File Offset: 0x0002FE00
		protected override void Finalize()
		{
			try
			{
				LocalDataStoreMgr mgr = this.m_mgr;
				if (mgr != null)
				{
					int slot = this.m_slot;
					this.m_slot = -1;
					mgr.FreeDataSlot(slot, this.m_cookie);
				}
			}
			finally
			{
				base.Finalize();
			}
		}

		// Token: 0x060009A9 RID: 2473 RVA: 0x0001FB35 File Offset: 0x0001DD35
		internal LocalDataStoreSlot()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x0400077E RID: 1918
		private LocalDataStoreMgr m_mgr;

		// Token: 0x0400077F RID: 1919
		private int m_slot;

		// Token: 0x04000780 RID: 1920
		private long m_cookie;
	}
}
