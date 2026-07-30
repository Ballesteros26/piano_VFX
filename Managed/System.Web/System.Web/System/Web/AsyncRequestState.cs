using System;
using System.Threading;

namespace System.Web
{
	// Token: 0x0200007B RID: 123
	internal class AsyncRequestState : IAsyncResult
	{
		// Token: 0x06000554 RID: 1364 RVA: 0x0000C9DD File Offset: 0x0000ABDD
		internal AsyncRequestState(ManualResetEvent complete_event, AsyncCallback cb, object cb_data)
		{
			this.cb = cb;
			this.cb_data = cb_data;
			this.complete_event = complete_event;
		}

		// Token: 0x06000555 RID: 1365 RVA: 0x0000C9FC File Offset: 0x0000ABFC
		internal void Complete()
		{
			this.completed = true;
			try
			{
				if (this.cb != null)
				{
					this.cb(this);
				}
			}
			catch
			{
			}
			this.complete_event.Set();
		}

		// Token: 0x1700020F RID: 527
		// (get) Token: 0x06000556 RID: 1366 RVA: 0x0000CA48 File Offset: 0x0000AC48
		public object AsyncState
		{
			get
			{
				return this.cb_data;
			}
		}

		// Token: 0x17000210 RID: 528
		// (get) Token: 0x06000557 RID: 1367 RVA: 0x00008A69 File Offset: 0x00006C69
		public bool CompletedSynchronously
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000211 RID: 529
		// (get) Token: 0x06000558 RID: 1368 RVA: 0x0000CA50 File Offset: 0x0000AC50
		public bool IsCompleted
		{
			get
			{
				return this.completed;
			}
		}

		// Token: 0x17000212 RID: 530
		// (get) Token: 0x06000559 RID: 1369 RVA: 0x0000CA58 File Offset: 0x0000AC58
		public WaitHandle AsyncWaitHandle
		{
			get
			{
				return this.complete_event;
			}
		}

		// Token: 0x04000EDA RID: 3802
		private AsyncCallback cb;

		// Token: 0x04000EDB RID: 3803
		private object cb_data;

		// Token: 0x04000EDC RID: 3804
		private bool completed;

		// Token: 0x04000EDD RID: 3805
		private ManualResetEvent complete_event;
	}
}
