using System;
using System.Collections;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Web.Configuration;

namespace System.Web
{
	// Token: 0x020000D0 RID: 208
	internal sealed class QueueManager
	{
		// Token: 0x06000B19 RID: 2841 RVA: 0x0001D0D4 File Offset: 0x0001B2D4
		public QueueManager()
		{
			Exception ex = null;
			try
			{
				HttpRuntimeSection section = HttpRuntime.Section;
				if (section != null)
				{
					this.minFree = section.MinFreeThreads;
					this.minLocalFree = section.MinLocalRequestFreeThreads;
					this.queueLimit = section.AppRequestQueueLimit;
				}
			}
			catch (Exception ex)
			{
			}
			try
			{
				this.queue = new Queue(this.queueLimit);
			}
			catch (Exception ex2)
			{
				if (ex == null)
				{
					this.initialException = ex2;
				}
				else
				{
					StringBuilder stringBuilder = new StringBuilder("Several exceptions occurred:\n");
					stringBuilder.AppendFormat("--- Exception Q1:\n{0}\n", ex.ToString());
					stringBuilder.AppendFormat("--- Exception Q2:\n{0}\n", ex2.ToString());
					this.initialException = new Exception(stringBuilder.ToString());
				}
			}
			if (this.initialException == null && ex != null)
			{
				this.initialException = ex;
			}
			this.requestsQueuedCounter = new PerformanceCounter("ASP.NET", "Requests Queued");
			this.requestsQueuedCounter.RawValue = 0L;
		}

		// Token: 0x170003E8 RID: 1000
		// (get) Token: 0x06000B1A RID: 2842 RVA: 0x0001D1E8 File Offset: 0x0001B3E8
		public bool HasException
		{
			get
			{
				return this.initialException != null;
			}
		}

		// Token: 0x170003E9 RID: 1001
		// (get) Token: 0x06000B1B RID: 2843 RVA: 0x0001D1F3 File Offset: 0x0001B3F3
		public Exception InitialException
		{
			get
			{
				return this.initialException;
			}
		}

		// Token: 0x06000B1C RID: 2844 RVA: 0x0001D1FC File Offset: 0x0001B3FC
		private bool CanExecuteRequest(HttpWorkerRequest req)
		{
			if (this.disposing)
			{
				return false;
			}
			int num;
			int num2;
			ThreadPool.GetAvailableThreads(out num, out num2);
			bool flag = req != null && req.GetLocalAddress() == "127.0.0.1";
			return num > this.minFree || (flag && num > this.minLocalFree);
		}

		// Token: 0x06000B1D RID: 2845 RVA: 0x0001D24C File Offset: 0x0001B44C
		public HttpWorkerRequest GetNextRequest(HttpWorkerRequest req)
		{
			Queue queue;
			if (!this.CanExecuteRequest(req))
			{
				if (!this.disposing && req != null)
				{
					queue = this.queue;
					lock (queue)
					{
						this.Queue(req);
					}
				}
				return null;
			}
			queue = this.queue;
			HttpWorkerRequest httpWorkerRequest;
			lock (queue)
			{
				httpWorkerRequest = this.Dequeue();
				if (httpWorkerRequest != null)
				{
					if (req != null)
					{
						this.Queue(req);
					}
				}
				else
				{
					httpWorkerRequest = req;
				}
			}
			return httpWorkerRequest;
		}

		// Token: 0x06000B1E RID: 2846 RVA: 0x0001D2E8 File Offset: 0x0001B4E8
		private void Queue(HttpWorkerRequest wr)
		{
			if (this.queue.Count < this.queueLimit)
			{
				this.queue.Enqueue(wr);
				this.requestsQueuedCounter.Increment();
				return;
			}
			HttpRuntime.FinishUnavailable(wr);
		}

		// Token: 0x06000B1F RID: 2847 RVA: 0x0001D31C File Offset: 0x0001B51C
		private HttpWorkerRequest Dequeue()
		{
			if (this.queue.Count > 0)
			{
				HttpWorkerRequest httpWorkerRequest = (HttpWorkerRequest)this.queue.Dequeue();
				this.requestsQueuedCounter.Decrement();
				return httpWorkerRequest;
			}
			return null;
		}

		// Token: 0x06000B20 RID: 2848 RVA: 0x0001D34C File Offset: 0x0001B54C
		public void Dispose()
		{
			if (this.disposing)
			{
				return;
			}
			this.disposing = true;
			HttpWorkerRequest nextRequest;
			while ((nextRequest = this.GetNextRequest(null)) != null)
			{
				HttpRuntime.FinishUnavailable(nextRequest);
			}
			this.queue = null;
		}

		// Token: 0x0400108B RID: 4235
		private int minFree = 8;

		// Token: 0x0400108C RID: 4236
		private int minLocalFree = 4;

		// Token: 0x0400108D RID: 4237
		private int queueLimit = 5000;

		// Token: 0x0400108E RID: 4238
		private Queue queue;

		// Token: 0x0400108F RID: 4239
		private bool disposing;

		// Token: 0x04001090 RID: 4240
		private Exception initialException;

		// Token: 0x04001091 RID: 4241
		private PerformanceCounter requestsQueuedCounter;
	}
}
