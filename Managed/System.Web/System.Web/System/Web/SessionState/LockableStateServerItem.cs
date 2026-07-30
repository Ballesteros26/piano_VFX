using System;
using System.Threading;

namespace System.Web.SessionState
{
	// Token: 0x02000499 RID: 1177
	internal class LockableStateServerItem
	{
		// Token: 0x0600357C RID: 13692 RVA: 0x0008B91C File Offset: 0x00089B1C
		public LockableStateServerItem(StateServerItem item)
		{
			this.item = item;
			this.rwlock = new ReaderWriterLock();
		}

		// Token: 0x04001D56 RID: 7510
		public StateServerItem item;

		// Token: 0x04001D57 RID: 7511
		public ReaderWriterLock rwlock;
	}
}
