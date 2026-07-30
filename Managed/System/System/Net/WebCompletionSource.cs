using System;
using System.Runtime.ExceptionServices;
using System.Threading.Tasks;

namespace System.Net
{
	// Token: 0x0200054D RID: 1357
	internal class WebCompletionSource
	{
		// Token: 0x06002A5D RID: 10845 RVA: 0x000A35E0 File Offset: 0x000A17E0
		public WebCompletionSource()
		{
			this.completion = new TaskCompletionSource<WebCompletionSource.Result>();
		}

		// Token: 0x06002A5E RID: 10846 RVA: 0x000A35F3 File Offset: 0x000A17F3
		public bool TrySetCompleted()
		{
			return this.completion.TrySetResult(new WebCompletionSource.Result(WebCompletionSource.State.Completed, null));
		}

		// Token: 0x06002A5F RID: 10847 RVA: 0x000A3608 File Offset: 0x000A1808
		public bool TrySetCanceled()
		{
			OperationCanceledException ex = new OperationCanceledException();
			WebCompletionSource.Result result = new WebCompletionSource.Result(WebCompletionSource.State.Canceled, ExceptionDispatchInfo.Capture(ex));
			return this.completion.TrySetResult(result);
		}

		// Token: 0x06002A60 RID: 10848 RVA: 0x000A3634 File Offset: 0x000A1834
		public bool TrySetException(Exception error)
		{
			WebCompletionSource.Result result = new WebCompletionSource.Result(WebCompletionSource.State.Faulted, ExceptionDispatchInfo.Capture(error));
			return this.completion.TrySetResult(result);
		}

		// Token: 0x17000902 RID: 2306
		// (get) Token: 0x06002A61 RID: 10849 RVA: 0x000A365A File Offset: 0x000A185A
		public bool IsCompleted
		{
			get
			{
				return this.completion.Task.IsCompleted;
			}
		}

		// Token: 0x06002A62 RID: 10850 RVA: 0x000A366C File Offset: 0x000A186C
		public void ThrowOnError()
		{
			if (!this.completion.Task.IsCompleted)
			{
				return;
			}
			ExceptionDispatchInfo error = this.completion.Task.Result.Error;
			if (error == null)
			{
				return;
			}
			error.Throw();
		}

		// Token: 0x06002A63 RID: 10851 RVA: 0x000A36A0 File Offset: 0x000A18A0
		public async Task<bool> WaitForCompletion(bool throwOnError)
		{
			WebCompletionSource.Result result = await this.completion.Task.ConfigureAwait(false);
			bool flag;
			if (result.State == WebCompletionSource.State.Completed)
			{
				flag = true;
			}
			else
			{
				if (throwOnError)
				{
					result.Error.Throw();
				}
				flag = false;
			}
			return flag;
		}

		// Token: 0x040022FC RID: 8956
		private TaskCompletionSource<WebCompletionSource.Result> completion;

		// Token: 0x0200054E RID: 1358
		private enum State
		{
			// Token: 0x040022FE RID: 8958
			Running,
			// Token: 0x040022FF RID: 8959
			Completed,
			// Token: 0x04002300 RID: 8960
			Canceled,
			// Token: 0x04002301 RID: 8961
			Faulted
		}

		// Token: 0x0200054F RID: 1359
		private class Result
		{
			// Token: 0x17000903 RID: 2307
			// (get) Token: 0x06002A64 RID: 10852 RVA: 0x000A36ED File Offset: 0x000A18ED
			public WebCompletionSource.State State { get; }

			// Token: 0x17000904 RID: 2308
			// (get) Token: 0x06002A65 RID: 10853 RVA: 0x000A36F5 File Offset: 0x000A18F5
			public ExceptionDispatchInfo Error { get; }

			// Token: 0x06002A66 RID: 10854 RVA: 0x000A36FD File Offset: 0x000A18FD
			public Result(WebCompletionSource.State state, ExceptionDispatchInfo error)
			{
				this.State = state;
				this.Error = error;
			}
		}
	}
}
