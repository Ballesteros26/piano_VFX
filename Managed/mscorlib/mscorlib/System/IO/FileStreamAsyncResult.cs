using System;
using System.Threading;

namespace System.IO
{
	// Token: 0x020003DA RID: 986
	internal class FileStreamAsyncResult : IAsyncResult
	{
		// Token: 0x06002E67 RID: 11879 RVA: 0x000A5DD7 File Offset: 0x000A3FD7
		public FileStreamAsyncResult(AsyncCallback cb, object state)
		{
			this.state = state;
			this.realcb = cb;
			if (this.realcb != null)
			{
				this.cb = new AsyncCallback(FileStreamAsyncResult.CBWrapper);
			}
			this.wh = new ManualResetEvent(false);
		}

		// Token: 0x06002E68 RID: 11880 RVA: 0x000A5E13 File Offset: 0x000A4013
		private static void CBWrapper(IAsyncResult ares)
		{
			((FileStreamAsyncResult)ares).realcb.BeginInvoke(ares, null, null);
		}

		// Token: 0x06002E69 RID: 11881 RVA: 0x000A5E29 File Offset: 0x000A4029
		public void SetComplete(Exception e)
		{
			this.exc = e;
			this.completed = true;
			this.wh.Set();
			if (this.cb != null)
			{
				this.cb(this);
			}
		}

		// Token: 0x06002E6A RID: 11882 RVA: 0x000A5E59 File Offset: 0x000A4059
		public void SetComplete(Exception e, int nbytes)
		{
			this.BytesRead = nbytes;
			this.SetComplete(e);
		}

		// Token: 0x06002E6B RID: 11883 RVA: 0x000A5E69 File Offset: 0x000A4069
		public void SetComplete(Exception e, int nbytes, bool synch)
		{
			this.completedSynch = synch;
			this.SetComplete(e, nbytes);
		}

		// Token: 0x1700074B RID: 1867
		// (get) Token: 0x06002E6C RID: 11884 RVA: 0x000A5E7A File Offset: 0x000A407A
		public object AsyncState
		{
			get
			{
				return this.state;
			}
		}

		// Token: 0x1700074C RID: 1868
		// (get) Token: 0x06002E6D RID: 11885 RVA: 0x000A5E82 File Offset: 0x000A4082
		public bool CompletedSynchronously
		{
			get
			{
				return this.completedSynch;
			}
		}

		// Token: 0x1700074D RID: 1869
		// (get) Token: 0x06002E6E RID: 11886 RVA: 0x000A5E8A File Offset: 0x000A408A
		public WaitHandle AsyncWaitHandle
		{
			get
			{
				return this.wh;
			}
		}

		// Token: 0x1700074E RID: 1870
		// (get) Token: 0x06002E6F RID: 11887 RVA: 0x000A5E92 File Offset: 0x000A4092
		public bool IsCompleted
		{
			get
			{
				return this.completed;
			}
		}

		// Token: 0x1700074F RID: 1871
		// (get) Token: 0x06002E70 RID: 11888 RVA: 0x000A5E9A File Offset: 0x000A409A
		public Exception Exception
		{
			get
			{
				return this.exc;
			}
		}

		// Token: 0x17000750 RID: 1872
		// (get) Token: 0x06002E71 RID: 11889 RVA: 0x000A5EA2 File Offset: 0x000A40A2
		// (set) Token: 0x06002E72 RID: 11890 RVA: 0x000A5EAA File Offset: 0x000A40AA
		public bool Done
		{
			get
			{
				return this.done;
			}
			set
			{
				this.done = value;
			}
		}

		// Token: 0x04001805 RID: 6149
		private object state;

		// Token: 0x04001806 RID: 6150
		private bool completed;

		// Token: 0x04001807 RID: 6151
		private bool done;

		// Token: 0x04001808 RID: 6152
		private Exception exc;

		// Token: 0x04001809 RID: 6153
		private ManualResetEvent wh;

		// Token: 0x0400180A RID: 6154
		private AsyncCallback cb;

		// Token: 0x0400180B RID: 6155
		private bool completedSynch;

		// Token: 0x0400180C RID: 6156
		public byte[] Buffer;

		// Token: 0x0400180D RID: 6157
		public int Offset;

		// Token: 0x0400180E RID: 6158
		public int Count;

		// Token: 0x0400180F RID: 6159
		public int OriginalCount;

		// Token: 0x04001810 RID: 6160
		public int BytesRead;

		// Token: 0x04001811 RID: 6161
		private AsyncCallback realcb;
	}
}
