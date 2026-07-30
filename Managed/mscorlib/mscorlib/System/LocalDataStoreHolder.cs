using System;

namespace System
{
	// Token: 0x02000105 RID: 261
	internal sealed class LocalDataStoreHolder
	{
		// Token: 0x06000997 RID: 2455 RVA: 0x00031961 File Offset: 0x0002FB61
		public LocalDataStoreHolder(LocalDataStore store)
		{
			this.m_Store = store;
		}

		// Token: 0x06000998 RID: 2456 RVA: 0x00031970 File Offset: 0x0002FB70
		protected override void Finalize()
		{
			try
			{
				LocalDataStore store = this.m_Store;
				if (store != null)
				{
					store.Dispose();
				}
			}
			finally
			{
				base.Finalize();
			}
		}

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x06000999 RID: 2457 RVA: 0x000319A8 File Offset: 0x0002FBA8
		public LocalDataStore Store
		{
			get
			{
				return this.m_Store;
			}
		}

		// Token: 0x04000779 RID: 1913
		private LocalDataStore m_Store;
	}
}
