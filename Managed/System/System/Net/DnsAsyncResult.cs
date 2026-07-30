using System;
using System.Threading;

namespace System.Net
{
	// Token: 0x0200050D RID: 1293
	internal class DnsAsyncResult : IAsyncResult
	{
		// Token: 0x060026B3 RID: 9907 RVA: 0x0009545A File Offset: 0x0009365A
		public DnsAsyncResult(AsyncCallback cb, object state)
		{
			this.callback = cb;
			this.state = state;
		}

		// Token: 0x060026B4 RID: 9908 RVA: 0x00095470 File Offset: 0x00093670
		public void SetCompleted(bool synch, IPHostEntry entry, Exception e)
		{
			this.synch = synch;
			this.entry = entry;
			this.exc = e;
			lock (this)
			{
				if (this.is_completed)
				{
					return;
				}
				this.is_completed = true;
				if (this.handle != null)
				{
					this.handle.Set();
				}
			}
			if (this.callback != null)
			{
				ThreadPool.QueueUserWorkItem(DnsAsyncResult.internal_cb, this);
			}
		}

		// Token: 0x060026B5 RID: 9909 RVA: 0x000954F4 File Offset: 0x000936F4
		public void SetCompleted(bool synch, Exception e)
		{
			this.SetCompleted(synch, null, e);
		}

		// Token: 0x060026B6 RID: 9910 RVA: 0x000954FF File Offset: 0x000936FF
		public void SetCompleted(bool synch, IPHostEntry entry)
		{
			this.SetCompleted(synch, entry, null);
		}

		// Token: 0x060026B7 RID: 9911 RVA: 0x0009550C File Offset: 0x0009370C
		private static void CB(object _this)
		{
			DnsAsyncResult dnsAsyncResult = (DnsAsyncResult)_this;
			dnsAsyncResult.callback(dnsAsyncResult);
		}

		// Token: 0x170007D4 RID: 2004
		// (get) Token: 0x060026B8 RID: 9912 RVA: 0x0009552C File Offset: 0x0009372C
		public object AsyncState
		{
			get
			{
				return this.state;
			}
		}

		// Token: 0x170007D5 RID: 2005
		// (get) Token: 0x060026B9 RID: 9913 RVA: 0x00095534 File Offset: 0x00093734
		public WaitHandle AsyncWaitHandle
		{
			get
			{
				lock (this)
				{
					if (this.handle == null)
					{
						this.handle = new ManualResetEvent(this.is_completed);
					}
				}
				return this.handle;
			}
		}

		// Token: 0x170007D6 RID: 2006
		// (get) Token: 0x060026BA RID: 9914 RVA: 0x00095588 File Offset: 0x00093788
		public Exception Exception
		{
			get
			{
				return this.exc;
			}
		}

		// Token: 0x170007D7 RID: 2007
		// (get) Token: 0x060026BB RID: 9915 RVA: 0x00095590 File Offset: 0x00093790
		public IPHostEntry HostEntry
		{
			get
			{
				return this.entry;
			}
		}

		// Token: 0x170007D8 RID: 2008
		// (get) Token: 0x060026BC RID: 9916 RVA: 0x00095598 File Offset: 0x00093798
		public bool CompletedSynchronously
		{
			get
			{
				return this.synch;
			}
		}

		// Token: 0x170007D9 RID: 2009
		// (get) Token: 0x060026BD RID: 9917 RVA: 0x000955A0 File Offset: 0x000937A0
		public bool IsCompleted
		{
			get
			{
				bool flag2;
				lock (this)
				{
					flag2 = this.is_completed;
				}
				return flag2;
			}
		}

		// Token: 0x04002114 RID: 8468
		private static WaitCallback internal_cb = new WaitCallback(DnsAsyncResult.CB);

		// Token: 0x04002115 RID: 8469
		private ManualResetEvent handle;

		// Token: 0x04002116 RID: 8470
		private bool synch;

		// Token: 0x04002117 RID: 8471
		private bool is_completed;

		// Token: 0x04002118 RID: 8472
		private AsyncCallback callback;

		// Token: 0x04002119 RID: 8473
		private object state;

		// Token: 0x0400211A RID: 8474
		private IPHostEntry entry;

		// Token: 0x0400211B RID: 8475
		private Exception exc;
	}
}
