using System;
using System.IO;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace System.Net
{
	// Token: 0x0200055F RID: 1375
	internal class WebRequestStream : WebConnectionStream
	{
		// Token: 0x06002AE8 RID: 10984 RVA: 0x000A5F5C File Offset: 0x000A415C
		public WebRequestStream(WebConnection connection, WebOperation operation, Stream stream, WebConnectionTunnel tunnel)
			: base(connection, operation, stream)
		{
			this.allowBuffering = operation.Request.InternalAllowBuffering;
			this.sendChunked = operation.Request.SendChunked && operation.WriteBuffer == null;
			if (!this.sendChunked && this.allowBuffering && operation.WriteBuffer == null)
			{
				this.writeBuffer = new MemoryStream();
			}
			this.KeepAlive = base.Request.KeepAlive;
			if (((tunnel != null) ? tunnel.ProxyVersion : null) != null && ((tunnel != null) ? tunnel.ProxyVersion : null) != HttpVersion.Version11)
			{
				this.KeepAlive = false;
			}
		}

		// Token: 0x17000928 RID: 2344
		// (get) Token: 0x06002AE9 RID: 10985 RVA: 0x000A600D File Offset: 0x000A420D
		public bool KeepAlive { get; }

		// Token: 0x17000929 RID: 2345
		// (get) Token: 0x06002AEA RID: 10986 RVA: 0x000074E4 File Offset: 0x000056E4
		public override long Length
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x1700092A RID: 2346
		// (get) Token: 0x06002AEB RID: 10987 RVA: 0x00004240 File Offset: 0x00002440
		public override bool CanRead
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700092B RID: 2347
		// (get) Token: 0x06002AEC RID: 10988 RVA: 0x000027E2 File Offset: 0x000009E2
		public override bool CanWrite
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700092C RID: 2348
		// (get) Token: 0x06002AED RID: 10989 RVA: 0x000A6015 File Offset: 0x000A4215
		// (set) Token: 0x06002AEE RID: 10990 RVA: 0x000A601D File Offset: 0x000A421D
		internal bool SendChunked
		{
			get
			{
				return this.sendChunked;
			}
			set
			{
				this.sendChunked = value;
			}
		}

		// Token: 0x1700092D RID: 2349
		// (get) Token: 0x06002AEF RID: 10991 RVA: 0x000A6026 File Offset: 0x000A4226
		internal bool HasWriteBuffer
		{
			get
			{
				return base.Operation.WriteBuffer != null || this.writeBuffer != null;
			}
		}

		// Token: 0x1700092E RID: 2350
		// (get) Token: 0x06002AF0 RID: 10992 RVA: 0x000A6040 File Offset: 0x000A4240
		internal int WriteBufferLength
		{
			get
			{
				if (base.Operation.WriteBuffer != null)
				{
					return base.Operation.WriteBuffer.Size;
				}
				if (this.writeBuffer != null)
				{
					return (int)this.writeBuffer.Length;
				}
				return -1;
			}
		}

		// Token: 0x06002AF1 RID: 10993 RVA: 0x000A6078 File Offset: 0x000A4278
		internal BufferOffsetSize GetWriteBuffer()
		{
			if (base.Operation.WriteBuffer != null)
			{
				return base.Operation.WriteBuffer;
			}
			if (this.writeBuffer == null || this.writeBuffer.Length == 0L)
			{
				return null;
			}
			return new BufferOffsetSize(this.writeBuffer.GetBuffer(), 0, (int)this.writeBuffer.Length, false);
		}

		// Token: 0x06002AF2 RID: 10994 RVA: 0x000A60D4 File Offset: 0x000A42D4
		private async Task FinishWriting(CancellationToken cancellationToken)
		{
			if (Interlocked.CompareExchange(ref this.completeRequestWritten, 1, 0) == 0)
			{
				try
				{
					base.Operation.ThrowIfClosedOrDisposed(cancellationToken);
					if (this.sendChunked)
					{
						await this.WriteChunkTrailer_inner(cancellationToken).ConfigureAwait(false);
					}
				}
				catch (Exception ex)
				{
					base.Operation.CompleteRequestWritten(this, ex);
					throw;
				}
				finally
				{
				}
				base.Operation.CompleteRequestWritten(this, null);
			}
		}

		// Token: 0x06002AF3 RID: 10995 RVA: 0x000A6124 File Offset: 0x000A4324
		public override async Task WriteAsync(byte[] buffer, int offset, int size, CancellationToken cancellationToken)
		{
			base.Operation.ThrowIfClosedOrDisposed(cancellationToken);
			if (base.Operation.WriteBuffer != null)
			{
				throw new InvalidOperationException();
			}
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			int num = buffer.Length;
			if (offset < 0 || num < offset)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (size < 0 || num - offset < size)
			{
				throw new ArgumentOutOfRangeException("size");
			}
			WebCompletionSource completion = new WebCompletionSource();
			if (Interlocked.CompareExchange<WebCompletionSource>(ref this.pendingWrite, completion, null) != null)
			{
				throw new InvalidOperationException(global::SR.GetString("Cannot re-call BeginGetRequestStream/BeginGetResponse while a previous call is still in progress."));
			}
			try
			{
				await this.ProcessWrite(buffer, offset, size, cancellationToken).ConfigureAwait(false);
				if (base.Request.ContentLength > 0L && this.totalWritten == base.Request.ContentLength)
				{
					await this.FinishWriting(cancellationToken);
				}
				this.pendingWrite = null;
				completion.TrySetCompleted();
			}
			catch (Exception ex)
			{
				this.KillBuffer();
				this.closed = true;
				if (ex is SocketException)
				{
					ex = new IOException("Error writing request", ex);
				}
				base.Operation.CompleteRequestWritten(this, ex);
				this.pendingWrite = null;
				completion.TrySetException(ex);
				throw;
			}
		}

		// Token: 0x06002AF4 RID: 10996 RVA: 0x000A618C File Offset: 0x000A438C
		private async Task ProcessWrite(byte[] buffer, int offset, int size, CancellationToken cancellationToken)
		{
			base.Operation.ThrowIfClosedOrDisposed(cancellationToken);
			if (this.sendChunked)
			{
				this.requestWritten = true;
				string text = string.Format("{0:X}\r\n", size);
				byte[] bytes = Encoding.ASCII.GetBytes(text);
				int num = 2 + size + bytes.Length;
				byte[] array = new byte[num];
				Buffer.BlockCopy(bytes, 0, array, 0, bytes.Length);
				Buffer.BlockCopy(buffer, offset, array, bytes.Length, size);
				Buffer.BlockCopy(WebRequestStream.crlf, 0, array, bytes.Length + size, WebRequestStream.crlf.Length);
				if (this.allowBuffering)
				{
					if (this.writeBuffer == null)
					{
						this.writeBuffer = new MemoryStream();
					}
					this.writeBuffer.Write(buffer, offset, size);
				}
				this.totalWritten += (long)size;
				buffer = array;
				offset = 0;
				size = num;
			}
			else
			{
				this.CheckWriteOverflow(base.Request.ContentLength, this.totalWritten, (long)size);
				if (this.allowBuffering)
				{
					if (this.writeBuffer == null)
					{
						this.writeBuffer = new MemoryStream();
					}
					this.writeBuffer.Write(buffer, offset, size);
					this.totalWritten += (long)size;
					if (base.Request.ContentLength <= 0L || this.totalWritten < base.Request.ContentLength)
					{
						return;
					}
					this.requestWritten = true;
					buffer = this.writeBuffer.GetBuffer();
					offset = 0;
					size = (int)this.totalWritten;
				}
				else
				{
					this.totalWritten += (long)size;
				}
			}
			try
			{
				await base.InnerStream.WriteAsync(buffer, offset, size, cancellationToken).ConfigureAwait(false);
			}
			catch
			{
				if (!this.IgnoreIOErrors)
				{
					throw;
				}
			}
		}

		// Token: 0x06002AF5 RID: 10997 RVA: 0x000A61F4 File Offset: 0x000A43F4
		private void CheckWriteOverflow(long contentLength, long totalWritten, long size)
		{
			if (contentLength == -1L)
			{
				return;
			}
			long num = contentLength - totalWritten;
			if (size > num)
			{
				this.KillBuffer();
				this.closed = true;
				ProtocolViolationException ex = new ProtocolViolationException("The number of bytes to be written is greater than the specified ContentLength.");
				base.Operation.CompleteRequestWritten(this, ex);
				throw ex;
			}
		}

		// Token: 0x06002AF6 RID: 10998 RVA: 0x000A6238 File Offset: 0x000A4438
		internal async Task Initialize(CancellationToken cancellationToken)
		{
			base.Operation.ThrowIfClosedOrDisposed(cancellationToken);
			if (base.Operation.WriteBuffer != null)
			{
				if (base.Operation.IsNtlmChallenge)
				{
					base.Request.InternalContentLength = 0L;
				}
				else
				{
					base.Request.InternalContentLength = (long)base.Operation.WriteBuffer.Size;
				}
			}
			await this.SetHeadersAsync(false, cancellationToken).ConfigureAwait(false);
			base.Operation.ThrowIfClosedOrDisposed(cancellationToken);
			if (base.Operation.WriteBuffer != null && !base.Operation.IsNtlmChallenge)
			{
				await this.WriteRequestAsync(cancellationToken);
				this.Close();
			}
		}

		// Token: 0x06002AF7 RID: 10999 RVA: 0x000A6288 File Offset: 0x000A4488
		private async Task SetHeadersAsync(bool setInternalLength, CancellationToken cancellationToken)
		{
			base.Operation.ThrowIfClosedOrDisposed(cancellationToken);
			if (!this.headersSent)
			{
				string method = base.Request.Method;
				bool flag = method == "GET" || method == "CONNECT" || method == "HEAD" || method == "TRACE";
				bool flag2 = method == "PROPFIND" || method == "PROPPATCH" || method == "MKCOL" || method == "COPY" || method == "MOVE" || method == "LOCK" || method == "UNLOCK";
				if (base.Operation.IsNtlmChallenge)
				{
					flag = true;
				}
				if (setInternalLength && !flag && this.HasWriteBuffer)
				{
					base.Request.InternalContentLength = (long)this.WriteBufferLength;
				}
				bool flag3 = !flag && (!this.HasWriteBuffer || base.Request.ContentLength > -1L);
				if (this.sendChunked || flag3 || flag || flag2)
				{
					this.headersSent = true;
					this.headers = base.Request.GetRequestHeaders();
					try
					{
						await base.InnerStream.WriteAsync(this.headers, 0, this.headers.Length, cancellationToken).ConfigureAwait(false);
						long contentLength = base.Request.ContentLength;
						if (!this.sendChunked && contentLength == 0L)
						{
							this.requestWritten = true;
						}
					}
					catch (Exception ex)
					{
						if (ex is WebException || ex is OperationCanceledException)
						{
							throw;
						}
						throw new WebException("Error writing headers", WebExceptionStatus.SendFailure, WebExceptionInternalStatus.RequestFatal, ex);
					}
				}
			}
		}

		// Token: 0x06002AF8 RID: 11000 RVA: 0x000A62E0 File Offset: 0x000A44E0
		internal async Task WriteRequestAsync(CancellationToken cancellationToken)
		{
			base.Operation.ThrowIfClosedOrDisposed(cancellationToken);
			if (!this.requestWritten)
			{
				this.requestWritten = true;
				if (!this.sendChunked && this.HasWriteBuffer)
				{
					BufferOffsetSize buffer = this.GetWriteBuffer();
					if (buffer != null && !base.Operation.IsNtlmChallenge && base.Request.ContentLength != -1L && base.Request.ContentLength < (long)buffer.Size)
					{
						this.closed = true;
						WebException ex = new WebException("Specified Content-Length is less than the number of bytes to write", null, WebExceptionStatus.ServerProtocolViolation, null);
						base.Operation.CompleteRequestWritten(this, ex);
						throw ex;
					}
					await this.SetHeadersAsync(true, cancellationToken).ConfigureAwait(false);
					base.Operation.ThrowIfClosedOrDisposed(cancellationToken);
					if (buffer != null && buffer.Size > 0)
					{
						await base.InnerStream.WriteAsync(buffer.Buffer, 0, buffer.Size, cancellationToken);
					}
					await this.FinishWriting(cancellationToken);
				}
			}
		}

		// Token: 0x06002AF9 RID: 11001 RVA: 0x000A6330 File Offset: 0x000A4530
		private async Task WriteChunkTrailer_inner(CancellationToken cancellationToken)
		{
			if (Interlocked.CompareExchange(ref this.chunkTrailerWritten, 1, 0) == 0)
			{
				base.Operation.ThrowIfClosedOrDisposed(cancellationToken);
				byte[] bytes = Encoding.ASCII.GetBytes("0\r\n\r\n");
				await base.InnerStream.WriteAsync(bytes, 0, bytes.Length, cancellationToken).ConfigureAwait(false);
			}
		}

		// Token: 0x06002AFA RID: 11002 RVA: 0x000A6380 File Offset: 0x000A4580
		private async Task WriteChunkTrailer()
		{
			using (CancellationTokenSource cts = new CancellationTokenSource())
			{
				cts.CancelAfter(this.WriteTimeout);
				Task timeoutTask = Task.Delay(this.WriteTimeout);
				ConfiguredTaskAwaitable<Task>.ConfiguredTaskAwaiter configuredTaskAwaiter;
				do
				{
					WebCompletionSource webCompletionSource = new WebCompletionSource();
					WebCompletionSource webCompletionSource2 = Interlocked.CompareExchange<WebCompletionSource>(ref this.pendingWrite, webCompletionSource, null);
					if (webCompletionSource2 == null)
					{
						goto IL_0103;
					}
					Task<bool> task = webCompletionSource2.WaitForCompletion(true);
					configuredTaskAwaiter = Task.WhenAny(new Task[] { timeoutTask, task }).ConfigureAwait(false).GetAwaiter();
					if (!configuredTaskAwaiter.IsCompleted)
					{
						await configuredTaskAwaiter;
						ConfiguredTaskAwaitable<Task>.ConfiguredTaskAwaiter configuredTaskAwaiter2;
						configuredTaskAwaiter = configuredTaskAwaiter2;
						configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<Task>.ConfiguredTaskAwaiter);
					}
				}
				while (configuredTaskAwaiter.GetResult() != timeoutTask);
				throw new WebException("The operation has timed out.", WebExceptionStatus.Timeout);
				IL_0103:
				try
				{
					await this.WriteChunkTrailer_inner(cts.Token).ConfigureAwait(false);
				}
				catch
				{
				}
				finally
				{
					this.pendingWrite = null;
				}
				timeoutTask = null;
			}
			CancellationTokenSource cts = null;
		}

		// Token: 0x06002AFB RID: 11003 RVA: 0x000A63C5 File Offset: 0x000A45C5
		internal void KillBuffer()
		{
			this.writeBuffer = null;
		}

		// Token: 0x06002AFC RID: 11004 RVA: 0x000A63CE File Offset: 0x000A45CE
		public override Task<int> ReadAsync(byte[] buffer, int offset, int size, CancellationToken cancellationToken)
		{
			return Task.FromException<int>(new NotSupportedException("The stream does not support reading."));
		}

		// Token: 0x06002AFD RID: 11005 RVA: 0x000A63E0 File Offset: 0x000A45E0
		protected override void Close_internal(ref bool disposed)
		{
			if (disposed)
			{
				return;
			}
			disposed = true;
			if (this.sendChunked)
			{
				this.WriteChunkTrailer().Wait();
				return;
			}
			if (!this.allowBuffering || this.requestWritten)
			{
				base.Operation.CompleteRequestWritten(this, null);
				return;
			}
			long contentLength = base.Request.ContentLength;
			if (!this.sendChunked && !base.Operation.IsNtlmChallenge && contentLength != -1L && this.totalWritten != contentLength)
			{
				IOException ex = new IOException("Cannot close the stream until all bytes are written");
				this.closed = true;
				disposed = true;
				WebException ex2 = new WebException("Request was cancelled.", WebExceptionStatus.RequestCanceled, WebExceptionInternalStatus.RequestFatal, ex);
				base.Operation.CompleteRequestWritten(this, ex2);
				throw ex2;
			}
			disposed = true;
			base.Operation.CompleteRequestWritten(this, null);
		}

		// Token: 0x04002386 RID: 9094
		private static byte[] crlf = new byte[] { 13, 10 };

		// Token: 0x04002387 RID: 9095
		private MemoryStream writeBuffer;

		// Token: 0x04002388 RID: 9096
		private bool requestWritten;

		// Token: 0x04002389 RID: 9097
		private bool allowBuffering;

		// Token: 0x0400238A RID: 9098
		private bool sendChunked;

		// Token: 0x0400238B RID: 9099
		private WebCompletionSource pendingWrite;

		// Token: 0x0400238C RID: 9100
		private long totalWritten;

		// Token: 0x0400238D RID: 9101
		private byte[] headers;

		// Token: 0x0400238E RID: 9102
		private bool headersSent;

		// Token: 0x0400238F RID: 9103
		private int completeRequestWritten;

		// Token: 0x04002390 RID: 9104
		private int chunkTrailerWritten;

		// Token: 0x04002391 RID: 9105
		internal readonly string ME;
	}
}
