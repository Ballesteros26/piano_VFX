using System;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Util;

namespace System.Web.Hosting
{
	// Token: 0x02000534 RID: 1332
	internal sealed class BackgroundWorkScheduler : IRegisteredObject
	{
		// Token: 0x06003A5D RID: 14941 RVA: 0x0009D752 File Offset: 0x0009B952
		internal BackgroundWorkScheduler(Action<BackgroundWorkScheduler> unregisterCallback, Action<AppDomain, Exception> logCallback, Action workItemCompleteCallback = null)
		{
			this._unregisterCallback = unregisterCallback;
			this._logCallback = logCallback;
			this._workItemCompleteCallback = workItemCompleteCallback;
		}

		// Token: 0x06003A5E RID: 14942 RVA: 0x0009D77B File Offset: 0x0009B97B
		private void FinalShutdown()
		{
			this._unregisterCallback(this);
		}

		// Token: 0x06003A5F RID: 14943 RVA: 0x0009D78C File Offset: 0x0009B98C
		private async void RunWorkItemImpl(Func<CancellationToken, Task> workItem)
		{
			Task returnedTask = null;
			try
			{
				returnedTask = workItem(this._cancellationTokenHelper.Token);
				await returnedTask.ConfigureAwait(false);
			}
			catch (Exception ex)
			{
				if (returnedTask == null || !returnedTask.IsCanceled)
				{
					OperationCanceledException ex2 = ex as OperationCanceledException;
					if (ex2 == null || !(ex2.CancellationToken == this._cancellationTokenHelper.Token))
					{
						this._logCallback(AppDomain.CurrentDomain, ex);
					}
				}
			}
			finally
			{
				this.WorkItemComplete();
			}
		}

		// Token: 0x06003A60 RID: 14944 RVA: 0x0009D7CD File Offset: 0x0009B9CD
		public void ScheduleWorkItem(Func<CancellationToken, Task> workItem)
		{
			if (this._cancellationTokenHelper.IsCancellationRequested)
			{
				return;
			}
			ThreadPool.UnsafeQueueUserWorkItem(delegate(object state)
			{
				lock (this)
				{
					if (this._cancellationTokenHelper.IsCancellationRequested)
					{
						return;
					}
					this._numExecutingWorkItems++;
				}
				this.RunWorkItemImpl((Func<CancellationToken, Task>)state);
			}, workItem);
		}

		// Token: 0x06003A61 RID: 14945 RVA: 0x0009D7F0 File Offset: 0x0009B9F0
		public void Stop(bool immediate)
		{
			int numExecutingWorkItems;
			lock (this)
			{
				this._cancellationTokenHelper.Cancel();
				numExecutingWorkItems = this._numExecutingWorkItems;
			}
			if (numExecutingWorkItems == 0)
			{
				this.FinalShutdown();
			}
		}

		// Token: 0x06003A62 RID: 14946 RVA: 0x0009D840 File Offset: 0x0009BA40
		private void WorkItemComplete()
		{
			int num2;
			bool isCancellationRequested;
			lock (this)
			{
				int num = this._numExecutingWorkItems - 1;
				this._numExecutingWorkItems = num;
				num2 = num;
				isCancellationRequested = this._cancellationTokenHelper.IsCancellationRequested;
			}
			if (this._workItemCompleteCallback != null)
			{
				this._workItemCompleteCallback();
			}
			if (num2 == 0 && isCancellationRequested)
			{
				this.FinalShutdown();
			}
		}

		// Token: 0x04001FBE RID: 8126
		private readonly CancellationTokenHelper _cancellationTokenHelper = new CancellationTokenHelper(false);

		// Token: 0x04001FBF RID: 8127
		private int _numExecutingWorkItems;

		// Token: 0x04001FC0 RID: 8128
		private readonly Action<BackgroundWorkScheduler> _unregisterCallback;

		// Token: 0x04001FC1 RID: 8129
		private readonly Action<AppDomain, Exception> _logCallback;

		// Token: 0x04001FC2 RID: 8130
		private readonly Action _workItemCompleteCallback;
	}
}
