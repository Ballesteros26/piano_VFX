using System;
using System.Runtime.ExceptionServices;
using System.Threading;
using System.Threading.Tasks;

namespace System.Net
{
	// Token: 0x0200055B RID: 1371
	internal class WebOperation
	{
		// Token: 0x17000920 RID: 2336
		// (get) Token: 0x06002AC2 RID: 10946 RVA: 0x000A548E File Offset: 0x000A368E
		public HttpWebRequest Request { get; }

		// Token: 0x17000921 RID: 2337
		// (get) Token: 0x06002AC3 RID: 10947 RVA: 0x000A5496 File Offset: 0x000A3696
		// (set) Token: 0x06002AC4 RID: 10948 RVA: 0x000A549E File Offset: 0x000A369E
		public WebConnection Connection { get; private set; }

		// Token: 0x17000922 RID: 2338
		// (get) Token: 0x06002AC5 RID: 10949 RVA: 0x000A54A7 File Offset: 0x000A36A7
		// (set) Token: 0x06002AC6 RID: 10950 RVA: 0x000A54AF File Offset: 0x000A36AF
		public ServicePoint ServicePoint { get; private set; }

		// Token: 0x17000923 RID: 2339
		// (get) Token: 0x06002AC7 RID: 10951 RVA: 0x000A54B8 File Offset: 0x000A36B8
		public BufferOffsetSize WriteBuffer { get; }

		// Token: 0x17000924 RID: 2340
		// (get) Token: 0x06002AC8 RID: 10952 RVA: 0x000A54C0 File Offset: 0x000A36C0
		public bool IsNtlmChallenge { get; }

		// Token: 0x06002AC9 RID: 10953 RVA: 0x000A54C8 File Offset: 0x000A36C8
		public WebOperation(HttpWebRequest request, BufferOffsetSize writeBuffer, bool isNtlmChallenge, CancellationToken cancellationToken)
		{
			this.Request = request;
			this.WriteBuffer = writeBuffer;
			this.IsNtlmChallenge = isNtlmChallenge;
			this.cts = CancellationTokenSource.CreateLinkedTokenSource(new CancellationToken[] { cancellationToken });
			this.requestTask = new TaskCompletionSource<WebRequestStream>();
			this.requestWrittenTask = new TaskCompletionSource<WebRequestStream>();
			this.completeResponseReadTask = new TaskCompletionSource<bool>();
			this.responseTask = new TaskCompletionSource<WebResponseStream>();
			this.finishedTask = new TaskCompletionSource<ValueTuple<bool, WebOperation>>();
		}

		// Token: 0x17000925 RID: 2341
		// (get) Token: 0x06002ACA RID: 10954 RVA: 0x000A5541 File Offset: 0x000A3741
		public bool Aborted
		{
			get
			{
				return this.disposedInfo != null || this.Request.Aborted || (this.cts != null && this.cts.IsCancellationRequested);
			}
		}

		// Token: 0x17000926 RID: 2342
		// (get) Token: 0x06002ACB RID: 10955 RVA: 0x000A5572 File Offset: 0x000A3772
		public bool Closed
		{
			get
			{
				return this.Aborted || this.closedInfo != null;
			}
		}

		// Token: 0x06002ACC RID: 10956 RVA: 0x000A5587 File Offset: 0x000A3787
		public void Abort()
		{
			if (!this.SetDisposed(ref this.disposedInfo).Item2)
			{
				return;
			}
			CancellationTokenSource cancellationTokenSource = this.cts;
			if (cancellationTokenSource != null)
			{
				cancellationTokenSource.Cancel();
			}
			this.SetCanceled();
			this.Close();
		}

		// Token: 0x06002ACD RID: 10957 RVA: 0x000A55BC File Offset: 0x000A37BC
		public void Close()
		{
			if (!this.SetDisposed(ref this.closedInfo).Item2)
			{
				return;
			}
			WebRequestStream webRequestStream = Interlocked.Exchange<WebRequestStream>(ref this.writeStream, null);
			if (webRequestStream != null)
			{
				try
				{
					webRequestStream.Close();
				}
				catch
				{
				}
			}
		}

		// Token: 0x06002ACE RID: 10958 RVA: 0x000A5608 File Offset: 0x000A3808
		private void SetCanceled()
		{
			this.requestTask.TrySetCanceled();
			this.requestWrittenTask.TrySetCanceled();
			this.responseTask.TrySetCanceled();
			this.completeResponseReadTask.TrySetCanceled();
		}

		// Token: 0x06002ACF RID: 10959 RVA: 0x000A563A File Offset: 0x000A383A
		private void SetError(Exception error)
		{
			this.requestTask.TrySetException(error);
			this.requestWrittenTask.TrySetException(error);
			this.responseTask.TrySetException(error);
			this.completeResponseReadTask.TrySetException(error);
		}

		// Token: 0x06002AD0 RID: 10960 RVA: 0x000A5670 File Offset: 0x000A3870
		private ValueTuple<ExceptionDispatchInfo, bool> SetDisposed(ref ExceptionDispatchInfo field)
		{
			ExceptionDispatchInfo exceptionDispatchInfo = ExceptionDispatchInfo.Capture(new WebException(global::SR.GetString("The request was canceled"), WebExceptionStatus.RequestCanceled));
			ExceptionDispatchInfo exceptionDispatchInfo2 = Interlocked.CompareExchange<ExceptionDispatchInfo>(ref field, exceptionDispatchInfo, null);
			return new ValueTuple<ExceptionDispatchInfo, bool>(exceptionDispatchInfo2 ?? exceptionDispatchInfo, exceptionDispatchInfo2 == null);
		}

		// Token: 0x06002AD1 RID: 10961 RVA: 0x000A56AB File Offset: 0x000A38AB
		internal void ThrowIfDisposed()
		{
			this.ThrowIfDisposed(CancellationToken.None);
		}

		// Token: 0x06002AD2 RID: 10962 RVA: 0x000A56B8 File Offset: 0x000A38B8
		internal void ThrowIfDisposed(CancellationToken cancellationToken)
		{
			if (this.Aborted || cancellationToken.IsCancellationRequested)
			{
				this.ThrowDisposed(ref this.disposedInfo);
			}
		}

		// Token: 0x06002AD3 RID: 10963 RVA: 0x000A56D7 File Offset: 0x000A38D7
		internal void ThrowIfClosedOrDisposed()
		{
			this.ThrowIfClosedOrDisposed(CancellationToken.None);
		}

		// Token: 0x06002AD4 RID: 10964 RVA: 0x000A56E4 File Offset: 0x000A38E4
		internal void ThrowIfClosedOrDisposed(CancellationToken cancellationToken)
		{
			if (this.Closed || cancellationToken.IsCancellationRequested)
			{
				this.ThrowDisposed(ref this.closedInfo);
			}
		}

		// Token: 0x06002AD5 RID: 10965 RVA: 0x000A5704 File Offset: 0x000A3904
		private void ThrowDisposed(ref ExceptionDispatchInfo field)
		{
			ValueTuple<ExceptionDispatchInfo, bool> valueTuple = this.SetDisposed(ref field);
			ExceptionDispatchInfo item = valueTuple.Item1;
			if (valueTuple.Item2)
			{
				CancellationTokenSource cancellationTokenSource = this.cts;
				if (cancellationTokenSource != null)
				{
					cancellationTokenSource.Cancel();
				}
			}
			item.Throw();
		}

		// Token: 0x06002AD6 RID: 10966 RVA: 0x000A5740 File Offset: 0x000A3940
		internal void RegisterRequest(ServicePoint servicePoint, WebConnection connection)
		{
			if (servicePoint == null)
			{
				throw new ArgumentNullException("servicePoint");
			}
			if (connection == null)
			{
				throw new ArgumentNullException("connection");
			}
			lock (this)
			{
				if (Interlocked.CompareExchange(ref this.requestSent, 1, 0) != 0)
				{
					throw new InvalidOperationException("Invalid nested call.");
				}
				this.ServicePoint = servicePoint;
				this.Connection = connection;
			}
			this.cts.Token.Register(delegate
			{
				this.Request.FinishedReading = true;
				this.SetDisposed(ref this.disposedInfo);
			});
		}

		// Token: 0x06002AD7 RID: 10967 RVA: 0x000A57DC File Offset: 0x000A39DC
		public void SetPriorityRequest(WebOperation operation)
		{
			lock (this)
			{
				if (this.requestSent != 1 || this.ServicePoint == null || this.finishedReading)
				{
					throw new InvalidOperationException("Should never happen.");
				}
				if (Interlocked.CompareExchange<WebOperation>(ref this.priorityRequest, operation, null) != null)
				{
					throw new InvalidOperationException("Invalid nested request.");
				}
			}
		}

		// Token: 0x06002AD8 RID: 10968 RVA: 0x000A5854 File Offset: 0x000A3A54
		public Task<WebRequestStream> GetRequestStream()
		{
			return this.requestTask.Task;
		}

		// Token: 0x06002AD9 RID: 10969 RVA: 0x000A5861 File Offset: 0x000A3A61
		public Task WaitUntilRequestWritten()
		{
			return this.requestWrittenTask.Task;
		}

		// Token: 0x17000927 RID: 2343
		// (get) Token: 0x06002ADA RID: 10970 RVA: 0x000A586E File Offset: 0x000A3A6E
		public WebRequestStream WriteStream
		{
			get
			{
				this.ThrowIfDisposed();
				return this.writeStream;
			}
		}

		// Token: 0x06002ADB RID: 10971 RVA: 0x000A587C File Offset: 0x000A3A7C
		public Task<WebResponseStream> GetResponseStream()
		{
			return this.responseTask.Task;
		}

		// Token: 0x06002ADC RID: 10972 RVA: 0x000A588C File Offset: 0x000A3A8C
		internal async Task<ValueTuple<bool, WebOperation>> WaitForCompletion(bool ignoreErrors)
		{
			ValueTuple<bool, WebOperation> valueTuple;
			try
			{
				valueTuple = await this.finishedTask.Task.ConfigureAwait(false);
			}
			catch
			{
				if (!ignoreErrors)
				{
					throw;
				}
				valueTuple = new ValueTuple<bool, WebOperation>(false, null);
			}
			return valueTuple;
		}

		// Token: 0x06002ADD RID: 10973 RVA: 0x000A58DC File Offset: 0x000A3ADC
		internal async void Run()
		{
			try
			{
				this.FinishReading();
				this.ThrowIfClosedOrDisposed();
				WebRequestStream webRequestStream = await this.Connection.InitConnection(this, this.cts.Token).ConfigureAwait(false);
				WebRequestStream requestStream = webRequestStream;
				this.ThrowIfClosedOrDisposed();
				this.writeStream = requestStream;
				await requestStream.Initialize(this.cts.Token).ConfigureAwait(false);
				this.ThrowIfClosedOrDisposed();
				this.requestTask.TrySetResult(requestStream);
				WebResponseStream stream = new WebResponseStream(requestStream);
				this.responseStream = stream;
				await stream.InitReadAsync(this.cts.Token).ConfigureAwait(false);
				this.responseTask.TrySetResult(stream);
				requestStream = null;
				stream = null;
			}
			catch (OperationCanceledException)
			{
				this.SetCanceled();
			}
			catch (Exception ex)
			{
				this.SetError(ex);
			}
		}

		// Token: 0x06002ADE RID: 10974 RVA: 0x000A5918 File Offset: 0x000A3B18
		private async void FinishReading()
		{
			bool ok = false;
			Exception error = null;
			try
			{
				bool flag = await this.completeResponseReadTask.Task.ConfigureAwait(false);
				ok = flag;
			}
			catch (Exception error)
			{
			}
			WebResponseStream webResponseStream;
			WebOperation webOperation;
			lock (this)
			{
				this.finishedReading = true;
				webResponseStream = Interlocked.Exchange<WebResponseStream>(ref this.responseStream, null);
				webOperation = Interlocked.Exchange<WebOperation>(ref this.priorityRequest, null);
				this.Request.FinishedReading = true;
			}
			if (error != null)
			{
				if (webOperation != null)
				{
					webOperation.SetError(error);
				}
				this.finishedTask.TrySetException(error);
			}
			else
			{
				bool flag2 = !this.Aborted && ok && webResponseStream != null && webResponseStream.KeepAlive;
				if (webOperation != null && webOperation.Aborted)
				{
					webOperation = null;
					flag2 = false;
				}
				this.finishedTask.TrySetResult(new ValueTuple<bool, WebOperation>(flag2, webOperation));
			}
		}

		// Token: 0x06002ADF RID: 10975 RVA: 0x000A5951 File Offset: 0x000A3B51
		internal void CompleteRequestWritten(WebRequestStream stream, Exception error = null)
		{
			if (error != null)
			{
				this.SetError(error);
				return;
			}
			this.requestWrittenTask.TrySetResult(stream);
		}

		// Token: 0x06002AE0 RID: 10976 RVA: 0x000A596B File Offset: 0x000A3B6B
		internal void CompleteResponseRead(bool ok, Exception error = null)
		{
			if (error != null)
			{
				this.completeResponseReadTask.TrySetException(error);
				return;
			}
			this.completeResponseReadTask.TrySetResult(ok);
		}

		// Token: 0x04002366 RID: 9062
		internal readonly int ID;

		// Token: 0x04002367 RID: 9063
		private CancellationTokenSource cts;

		// Token: 0x04002368 RID: 9064
		private TaskCompletionSource<WebRequestStream> requestTask;

		// Token: 0x04002369 RID: 9065
		private TaskCompletionSource<WebRequestStream> requestWrittenTask;

		// Token: 0x0400236A RID: 9066
		private TaskCompletionSource<WebResponseStream> responseTask;

		// Token: 0x0400236B RID: 9067
		private TaskCompletionSource<bool> completeResponseReadTask;

		// Token: 0x0400236C RID: 9068
		private TaskCompletionSource<ValueTuple<bool, WebOperation>> finishedTask;

		// Token: 0x0400236D RID: 9069
		private WebRequestStream writeStream;

		// Token: 0x0400236E RID: 9070
		private WebResponseStream responseStream;

		// Token: 0x0400236F RID: 9071
		private ExceptionDispatchInfo disposedInfo;

		// Token: 0x04002370 RID: 9072
		private ExceptionDispatchInfo closedInfo;

		// Token: 0x04002371 RID: 9073
		private WebOperation priorityRequest;

		// Token: 0x04002372 RID: 9074
		private volatile bool finishedReading;

		// Token: 0x04002373 RID: 9075
		private int requestSent;
	}
}
