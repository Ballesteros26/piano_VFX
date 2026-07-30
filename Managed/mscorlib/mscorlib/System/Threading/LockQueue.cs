using System;

namespace System.Threading
{
	// Token: 0x020004A6 RID: 1190
	internal class LockQueue
	{
		// Token: 0x060037D6 RID: 14294 RVA: 0x000CADCA File Offset: 0x000C8FCA
		public LockQueue(ReaderWriterLock rwlock)
		{
			this.rwlock = rwlock;
		}

		// Token: 0x060037D7 RID: 14295 RVA: 0x000CADDC File Offset: 0x000C8FDC
		public bool Wait(int timeout)
		{
			bool flag = false;
			bool flag3;
			try
			{
				lock (this)
				{
					this.lockCount++;
					Monitor.Exit(this.rwlock);
					flag = true;
					flag3 = Monitor.Wait(this, timeout);
				}
			}
			finally
			{
				if (flag)
				{
					Monitor.Enter(this.rwlock);
					this.lockCount--;
				}
			}
			return flag3;
		}

		// Token: 0x17000931 RID: 2353
		// (get) Token: 0x060037D8 RID: 14296 RVA: 0x000CAE60 File Offset: 0x000C9060
		public bool IsEmpty
		{
			get
			{
				bool flag2;
				lock (this)
				{
					flag2 = this.lockCount == 0;
				}
				return flag2;
			}
		}

		// Token: 0x060037D9 RID: 14297 RVA: 0x000CAEA0 File Offset: 0x000C90A0
		public void Pulse()
		{
			lock (this)
			{
				Monitor.Pulse(this);
			}
		}

		// Token: 0x04001D41 RID: 7489
		private ReaderWriterLock rwlock;

		// Token: 0x04001D42 RID: 7490
		private int lockCount;
	}
}
