using System;

namespace System
{
	// Token: 0x02000106 RID: 262
	internal sealed class LocalDataStoreElement
	{
		// Token: 0x0600099A RID: 2458 RVA: 0x000319B0 File Offset: 0x0002FBB0
		public LocalDataStoreElement(long cookie)
		{
			this.m_cookie = cookie;
		}

		// Token: 0x1700019A RID: 410
		// (get) Token: 0x0600099B RID: 2459 RVA: 0x000319BF File Offset: 0x0002FBBF
		// (set) Token: 0x0600099C RID: 2460 RVA: 0x000319C7 File Offset: 0x0002FBC7
		public object Value
		{
			get
			{
				return this.m_value;
			}
			set
			{
				this.m_value = value;
			}
		}

		// Token: 0x1700019B RID: 411
		// (get) Token: 0x0600099D RID: 2461 RVA: 0x000319D0 File Offset: 0x0002FBD0
		public long Cookie
		{
			get
			{
				return this.m_cookie;
			}
		}

		// Token: 0x0400077A RID: 1914
		private object m_value;

		// Token: 0x0400077B RID: 1915
		private long m_cookie;
	}
}
