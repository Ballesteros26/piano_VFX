using System;
using System.Threading;

namespace System.Web.SessionState
{
	// Token: 0x0200049D RID: 1181
	internal sealed class InProcSessionItem
	{
		// Token: 0x06003595 RID: 13717 RVA: 0x0008BEBC File Offset: 0x0008A0BC
		internal InProcSessionItem()
		{
			this.locked = false;
			this.cookieless = false;
			this.items = null;
			this.staticItems = null;
			this.lockedTime = DateTime.MinValue;
			this.expiresAt = DateTime.MinValue;
			this.rwlock = new ReaderWriterLockSlim();
			this.lockId = int.MinValue;
			this.timeout = 0;
			this.resettingTimeout = false;
		}

		// Token: 0x06003596 RID: 13718 RVA: 0x0008BF25 File Offset: 0x0008A125
		public void Dispose()
		{
			if (this.rwlock != null)
			{
				this.rwlock.Dispose();
				this.rwlock = null;
			}
			this.staticItems = null;
			if (this.items != null)
			{
				this.items.Clear();
			}
			this.items = null;
		}

		// Token: 0x06003597 RID: 13719 RVA: 0x0008BF64 File Offset: 0x0008A164
		~InProcSessionItem()
		{
			this.Dispose();
		}

		// Token: 0x04001D5E RID: 7518
		public bool locked;

		// Token: 0x04001D5F RID: 7519
		public bool cookieless;

		// Token: 0x04001D60 RID: 7520
		public ISessionStateItemCollection items;

		// Token: 0x04001D61 RID: 7521
		public DateTime lockedTime;

		// Token: 0x04001D62 RID: 7522
		public DateTime expiresAt;

		// Token: 0x04001D63 RID: 7523
		public ReaderWriterLockSlim rwlock;

		// Token: 0x04001D64 RID: 7524
		public int lockId;

		// Token: 0x04001D65 RID: 7525
		public int timeout;

		// Token: 0x04001D66 RID: 7526
		public bool resettingTimeout;

		// Token: 0x04001D67 RID: 7527
		public HttpStaticObjectsCollection staticItems;
	}
}
