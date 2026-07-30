using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Runtime.ExceptionServices;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;
using Mono.Security.Interface;

namespace Mono.Net.Security
{
	// Token: 0x02000072 RID: 114
	internal abstract class MobileAuthenticatedStream : AuthenticatedStream, IMonoSslStream, IDisposable
	{
		// Token: 0x0600021D RID: 541 RVA: 0x000068F0 File Offset: 0x00004AF0
		public MobileAuthenticatedStream(Stream innerStream, bool leaveInnerStreamOpen, SslStream owner, MonoTlsSettings settings, MonoTlsProvider provider)
			: base(innerStream, leaveInnerStreamOpen)
		{
			this.SslStream = owner;
			this.Settings = settings;
			this.Provider = provider;
			this.readBuffer = new BufferOffsetSize2(16834);
			this.writeBuffer = new BufferOffsetSize2(16384);
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x0600021E RID: 542 RVA: 0x0000695A File Offset: 0x00004B5A
		public SslStream SslStream { get; }

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x0600021F RID: 543 RVA: 0x00006962 File Offset: 0x00004B62
		public MonoTlsSettings Settings { get; }

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x06000220 RID: 544 RVA: 0x0000696A File Offset: 0x00004B6A
		public MonoTlsProvider Provider { get; }

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x06000221 RID: 545 RVA: 0x00006972 File Offset: 0x00004B72
		internal bool HasContext
		{
			get
			{
				return this.xobileTlsContext != null;
			}
		}

		// Token: 0x06000222 RID: 546 RVA: 0x00006980 File Offset: 0x00004B80
		internal void CheckThrow(bool authSuccessCheck, bool shutdownCheck = false)
		{
			if (this.lastException != null)
			{
				this.lastException.Throw();
			}
			if (authSuccessCheck && !this.IsAuthenticated)
			{
				throw new InvalidOperationException("This operation is only allowed using a successfully authenticated context.");
			}
			if (shutdownCheck && this.shutdown)
			{
				throw new InvalidOperationException("Write operations are not allowed after the channel was shutdown.");
			}
		}

		// Token: 0x06000223 RID: 547 RVA: 0x000069CC File Offset: 0x00004BCC
		internal static Exception GetSSPIException(Exception e)
		{
			if (e is OperationCanceledException || e is IOException || e is ObjectDisposedException || e is AuthenticationException)
			{
				return e;
			}
			return new AuthenticationException("A call to SSPI failed, see inner exception.", e);
		}

		// Token: 0x06000224 RID: 548 RVA: 0x000069FB File Offset: 0x00004BFB
		internal static Exception GetIOException(Exception e, string message)
		{
			if (e is OperationCanceledException || e is IOException || e is ObjectDisposedException || e is AuthenticationException)
			{
				return e;
			}
			return new IOException(message, e);
		}

		// Token: 0x06000225 RID: 549 RVA: 0x00006A28 File Offset: 0x00004C28
		internal ExceptionDispatchInfo SetException(Exception e)
		{
			ExceptionDispatchInfo exceptionDispatchInfo = ExceptionDispatchInfo.Capture(e);
			return Interlocked.CompareExchange<ExceptionDispatchInfo>(ref this.lastException, exceptionDispatchInfo, null) ?? exceptionDispatchInfo;
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000226 RID: 550 RVA: 0x00003DEC File Offset: 0x00001FEC
		private SslProtocols DefaultProtocols
		{
			get
			{
				return SslProtocols.Tls | SslProtocols.Tls11 | SslProtocols.Tls12;
			}
		}

		// Token: 0x06000227 RID: 551 RVA: 0x00006A4E File Offset: 0x00004C4E
		public void AuthenticateAsClient(string targetHost)
		{
			this.AuthenticateAsClient(targetHost, new X509CertificateCollection(), this.DefaultProtocols, false);
		}

		// Token: 0x06000228 RID: 552 RVA: 0x00006A63 File Offset: 0x00004C63
		public void AuthenticateAsClient(string targetHost, X509CertificateCollection clientCertificates, SslProtocols enabledSslProtocols, bool checkCertificateRevocation)
		{
			this.ProcessAuthentication(true, false, targetHost, enabledSslProtocols, null, clientCertificates, false).Wait();
		}

		// Token: 0x06000229 RID: 553 RVA: 0x00006A77 File Offset: 0x00004C77
		public IAsyncResult BeginAuthenticateAsClient(string targetHost, AsyncCallback asyncCallback, object asyncState)
		{
			return this.BeginAuthenticateAsClient(targetHost, new X509CertificateCollection(), this.DefaultProtocols, false, asyncCallback, asyncState);
		}

		// Token: 0x0600022A RID: 554 RVA: 0x00006A8E File Offset: 0x00004C8E
		public IAsyncResult BeginAuthenticateAsClient(string targetHost, X509CertificateCollection clientCertificates, SslProtocols enabledSslProtocols, bool checkCertificateRevocation, AsyncCallback asyncCallback, object asyncState)
		{
			return TaskToApm.Begin(this.ProcessAuthentication(false, false, targetHost, enabledSslProtocols, null, clientCertificates, false), asyncCallback, asyncState);
		}

		// Token: 0x0600022B RID: 555 RVA: 0x00006AA6 File Offset: 0x00004CA6
		public void EndAuthenticateAsClient(IAsyncResult asyncResult)
		{
			TaskToApm.End(asyncResult);
		}

		// Token: 0x0600022C RID: 556 RVA: 0x00006AAE File Offset: 0x00004CAE
		public void AuthenticateAsServer(X509Certificate serverCertificate)
		{
			this.AuthenticateAsServer(serverCertificate, false, this.DefaultProtocols, false);
		}

		// Token: 0x0600022D RID: 557 RVA: 0x00006ABF File Offset: 0x00004CBF
		public void AuthenticateAsServer(X509Certificate serverCertificate, bool clientCertificateRequired, SslProtocols enabledSslProtocols, bool checkCertificateRevocation)
		{
			this.ProcessAuthentication(true, true, string.Empty, enabledSslProtocols, serverCertificate, null, clientCertificateRequired).Wait();
		}

		// Token: 0x0600022E RID: 558 RVA: 0x00006AD7 File Offset: 0x00004CD7
		public IAsyncResult BeginAuthenticateAsServer(X509Certificate serverCertificate, AsyncCallback asyncCallback, object asyncState)
		{
			return this.BeginAuthenticateAsServer(serverCertificate, false, this.DefaultProtocols, false, asyncCallback, asyncState);
		}

		// Token: 0x0600022F RID: 559 RVA: 0x00006AEA File Offset: 0x00004CEA
		public IAsyncResult BeginAuthenticateAsServer(X509Certificate serverCertificate, bool clientCertificateRequired, SslProtocols enabledSslProtocols, bool checkCertificateRevocation, AsyncCallback asyncCallback, object asyncState)
		{
			return TaskToApm.Begin(this.ProcessAuthentication(false, true, string.Empty, enabledSslProtocols, serverCertificate, null, clientCertificateRequired), asyncCallback, asyncState);
		}

		// Token: 0x06000230 RID: 560 RVA: 0x00006AA6 File Offset: 0x00004CA6
		public void EndAuthenticateAsServer(IAsyncResult asyncResult)
		{
			TaskToApm.End(asyncResult);
		}

		// Token: 0x06000231 RID: 561 RVA: 0x00006B06 File Offset: 0x00004D06
		public Task AuthenticateAsClientAsync(string targetHost)
		{
			return this.ProcessAuthentication(false, false, targetHost, this.DefaultProtocols, null, null, false);
		}

		// Token: 0x06000232 RID: 562 RVA: 0x00006B1A File Offset: 0x00004D1A
		public Task AuthenticateAsClientAsync(string targetHost, X509CertificateCollection clientCertificates, SslProtocols enabledSslProtocols, bool checkCertificateRevocation)
		{
			return this.ProcessAuthentication(false, false, targetHost, enabledSslProtocols, null, clientCertificates, false);
		}

		// Token: 0x06000233 RID: 563 RVA: 0x00006B29 File Offset: 0x00004D29
		public Task AuthenticateAsServerAsync(X509Certificate serverCertificate)
		{
			return this.AuthenticateAsServerAsync(serverCertificate, false, this.DefaultProtocols, false);
		}

		// Token: 0x06000234 RID: 564 RVA: 0x00006B3A File Offset: 0x00004D3A
		public Task AuthenticateAsServerAsync(X509Certificate serverCertificate, bool clientCertificateRequired, SslProtocols enabledSslProtocols, bool checkCertificateRevocation)
		{
			return this.ProcessAuthentication(false, true, string.Empty, enabledSslProtocols, serverCertificate, null, clientCertificateRequired);
		}

		// Token: 0x06000235 RID: 565 RVA: 0x00006B50 File Offset: 0x00004D50
		public Task ShutdownAsync()
		{
			AsyncShutdownRequest asyncShutdownRequest = new AsyncShutdownRequest(this);
			return this.StartOperation(MobileAuthenticatedStream.OperationType.Shutdown, asyncShutdownRequest, CancellationToken.None);
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000236 RID: 566 RVA: 0x00002068 File Offset: 0x00000268
		public AuthenticatedStream AuthenticatedStream
		{
			get
			{
				return this;
			}
		}

		// Token: 0x06000237 RID: 567 RVA: 0x00006B74 File Offset: 0x00004D74
		private async Task ProcessAuthentication(bool runSynchronously, bool serverMode, string targetHost, SslProtocols enabledProtocols, X509Certificate serverCertificate, X509CertificateCollection clientCertificates, bool clientCertRequired)
		{
			if (serverMode)
			{
				if (serverCertificate == null)
				{
					throw new ArgumentException("serverCertificate");
				}
			}
			else
			{
				if (targetHost == null)
				{
					throw new ArgumentException("targetHost");
				}
				if (targetHost.Length == 0)
				{
					targetHost = "?" + Interlocked.Increment(ref MobileAuthenticatedStream.uniqueNameInteger).ToString(NumberFormatInfo.InvariantInfo);
				}
			}
			if (this.lastException != null)
			{
				this.lastException.Throw();
			}
			AsyncHandshakeRequest asyncHandshakeRequest = new AsyncHandshakeRequest(this, runSynchronously);
			if (Interlocked.CompareExchange<AsyncProtocolRequest>(ref this.asyncHandshakeRequest, asyncHandshakeRequest, null) != null)
			{
				throw new InvalidOperationException("Invalid nested call.");
			}
			if (Interlocked.CompareExchange<AsyncProtocolRequest>(ref this.asyncReadRequest, asyncHandshakeRequest, null) != null)
			{
				throw new InvalidOperationException("Invalid nested call.");
			}
			if (Interlocked.CompareExchange<AsyncProtocolRequest>(ref this.asyncWriteRequest, asyncHandshakeRequest, null) != null)
			{
				throw new InvalidOperationException("Invalid nested call.");
			}
			AsyncProtocolResult asyncProtocolResult;
			try
			{
				object obj = this.ioLock;
				lock (obj)
				{
					if (this.xobileTlsContext != null)
					{
						throw new InvalidOperationException();
					}
					this.readBuffer.Reset();
					this.writeBuffer.Reset();
					this.xobileTlsContext = this.CreateContext(serverMode, targetHost, enabledProtocols, serverCertificate, clientCertificates, clientCertRequired);
				}
				try
				{
					asyncProtocolResult = await asyncHandshakeRequest.StartOperation(CancellationToken.None).ConfigureAwait(false);
				}
				catch (Exception ex)
				{
					asyncProtocolResult = new AsyncProtocolResult(this.SetException(MobileAuthenticatedStream.GetSSPIException(ex)));
				}
			}
			finally
			{
				object obj = this.ioLock;
				bool flag = false;
				try
				{
					Monitor.Enter(obj, ref flag);
					this.readBuffer.Reset();
					this.writeBuffer.Reset();
					this.asyncWriteRequest = null;
					this.asyncReadRequest = null;
					this.asyncHandshakeRequest = null;
				}
				finally
				{
					int num;
					if (num < 0 && flag)
					{
						Monitor.Exit(obj);
					}
				}
			}
			if (asyncProtocolResult.Error != null)
			{
				asyncProtocolResult.Error.Throw();
			}
		}

		// Token: 0x06000238 RID: 568
		protected abstract MobileTlsContext CreateContext(bool serverMode, string targetHost, SslProtocols enabledProtocols, X509Certificate serverCertificate, X509CertificateCollection clientCertificates, bool askForClientCert);

		// Token: 0x06000239 RID: 569 RVA: 0x00006BF8 File Offset: 0x00004DF8
		public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback asyncCallback, object asyncState)
		{
			AsyncReadRequest asyncReadRequest = new AsyncReadRequest(this, false, buffer, offset, count);
			return TaskToApm.Begin(this.StartOperation(MobileAuthenticatedStream.OperationType.Read, asyncReadRequest, CancellationToken.None), asyncCallback, asyncState);
		}

		// Token: 0x0600023A RID: 570 RVA: 0x00006C26 File Offset: 0x00004E26
		public override int EndRead(IAsyncResult asyncResult)
		{
			return TaskToApm.End<int>(asyncResult);
		}

		// Token: 0x0600023B RID: 571 RVA: 0x00006C30 File Offset: 0x00004E30
		public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback asyncCallback, object asyncState)
		{
			AsyncWriteRequest asyncWriteRequest = new AsyncWriteRequest(this, false, buffer, offset, count);
			return TaskToApm.Begin(this.StartOperation(MobileAuthenticatedStream.OperationType.Write, asyncWriteRequest, CancellationToken.None), asyncCallback, asyncState);
		}

		// Token: 0x0600023C RID: 572 RVA: 0x00006AA6 File Offset: 0x00004CA6
		public override void EndWrite(IAsyncResult asyncResult)
		{
			TaskToApm.End(asyncResult);
		}

		// Token: 0x0600023D RID: 573 RVA: 0x00006C60 File Offset: 0x00004E60
		public override int Read(byte[] buffer, int offset, int count)
		{
			AsyncReadRequest asyncReadRequest = new AsyncReadRequest(this, true, buffer, offset, count);
			return this.StartOperation(MobileAuthenticatedStream.OperationType.Read, asyncReadRequest, CancellationToken.None).Result;
		}

		// Token: 0x0600023E RID: 574 RVA: 0x00006C8A File Offset: 0x00004E8A
		public void Write(byte[] buffer)
		{
			this.Write(buffer, 0, buffer.Length);
		}

		// Token: 0x0600023F RID: 575 RVA: 0x00006C98 File Offset: 0x00004E98
		public override void Write(byte[] buffer, int offset, int count)
		{
			AsyncWriteRequest asyncWriteRequest = new AsyncWriteRequest(this, true, buffer, offset, count);
			this.StartOperation(MobileAuthenticatedStream.OperationType.Write, asyncWriteRequest, CancellationToken.None).Wait();
		}

		// Token: 0x06000240 RID: 576 RVA: 0x00006CC4 File Offset: 0x00004EC4
		public override Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			AsyncReadRequest asyncReadRequest = new AsyncReadRequest(this, false, buffer, offset, count);
			return this.StartOperation(MobileAuthenticatedStream.OperationType.Read, asyncReadRequest, cancellationToken);
		}

		// Token: 0x06000241 RID: 577 RVA: 0x00006CE8 File Offset: 0x00004EE8
		public override Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			AsyncWriteRequest asyncWriteRequest = new AsyncWriteRequest(this, false, buffer, offset, count);
			return this.StartOperation(MobileAuthenticatedStream.OperationType.Write, asyncWriteRequest, cancellationToken);
		}

		// Token: 0x06000242 RID: 578 RVA: 0x00006D0C File Offset: 0x00004F0C
		private async Task<int> StartOperation(MobileAuthenticatedStream.OperationType type, AsyncProtocolRequest asyncRequest, CancellationToken cancellationToken)
		{
			this.CheckThrow(true, type > MobileAuthenticatedStream.OperationType.Read);
			if (type == MobileAuthenticatedStream.OperationType.Read)
			{
				if (Interlocked.CompareExchange<AsyncProtocolRequest>(ref this.asyncReadRequest, asyncRequest, null) != null)
				{
					throw new InvalidOperationException("Invalid nested call.");
				}
			}
			else if (Interlocked.CompareExchange<AsyncProtocolRequest>(ref this.asyncWriteRequest, asyncRequest, null) != null)
			{
				throw new InvalidOperationException("Invalid nested call.");
			}
			AsyncProtocolResult asyncProtocolResult;
			try
			{
				object obj = this.ioLock;
				lock (obj)
				{
					if (type == MobileAuthenticatedStream.OperationType.Read)
					{
						this.readBuffer.Reset();
					}
					else
					{
						this.writeBuffer.Reset();
					}
				}
				asyncProtocolResult = await asyncRequest.StartOperation(cancellationToken).ConfigureAwait(false);
			}
			catch (Exception ex)
			{
				asyncProtocolResult = new AsyncProtocolResult(this.SetException(MobileAuthenticatedStream.GetIOException(ex, asyncRequest.Name + " failed")));
			}
			finally
			{
				object obj = this.ioLock;
				bool flag = false;
				try
				{
					Monitor.Enter(obj, ref flag);
					if (type == MobileAuthenticatedStream.OperationType.Read)
					{
						this.readBuffer.Reset();
						this.asyncReadRequest = null;
					}
					else
					{
						this.writeBuffer.Reset();
						this.asyncWriteRequest = null;
					}
				}
				finally
				{
					int num;
					if (num < 0 && flag)
					{
						Monitor.Exit(obj);
					}
				}
			}
			if (asyncProtocolResult.Error != null)
			{
				asyncProtocolResult.Error.Throw();
			}
			return asyncProtocolResult.UserResult;
		}

		// Token: 0x06000243 RID: 579 RVA: 0x000027E8 File Offset: 0x000009E8
		[Conditional("MONO_TLS_DEBUG")]
		protected internal void Debug(string message, params object[] args)
		{
		}

		// Token: 0x06000244 RID: 580 RVA: 0x00006D6C File Offset: 0x00004F6C
		internal int InternalRead(byte[] buffer, int offset, int size, out bool outWantMore)
		{
			int num;
			try
			{
				AsyncProtocolRequest asyncProtocolRequest = this.asyncHandshakeRequest ?? this.asyncReadRequest;
				ValueTuple<int, bool> valueTuple = this.InternalRead(asyncProtocolRequest, this.readBuffer, buffer, offset, size);
				int item = valueTuple.Item1;
				bool item2 = valueTuple.Item2;
				outWantMore = item2;
				num = item;
			}
			catch (Exception ex)
			{
				this.SetException(MobileAuthenticatedStream.GetIOException(ex, "InternalRead() failed"));
				outWantMore = false;
				num = -1;
			}
			return num;
		}

		// Token: 0x06000245 RID: 581 RVA: 0x00006DE0 File Offset: 0x00004FE0
		private ValueTuple<int, bool> InternalRead(AsyncProtocolRequest asyncRequest, BufferOffsetSize internalBuffer, byte[] buffer, int offset, int size)
		{
			if (asyncRequest == null)
			{
				throw new InvalidOperationException();
			}
			if (internalBuffer.Size == 0 && !internalBuffer.Complete)
			{
				internalBuffer.Offset = (internalBuffer.Size = 0);
				asyncRequest.RequestRead(size);
				return new ValueTuple<int, bool>(0, true);
			}
			int num = Math.Min(internalBuffer.Size, size);
			Buffer.BlockCopy(internalBuffer.Buffer, internalBuffer.Offset, buffer, offset, num);
			internalBuffer.Offset += num;
			internalBuffer.Size -= num;
			return new ValueTuple<int, bool>(num, !internalBuffer.Complete && num < size);
		}

		// Token: 0x06000246 RID: 582 RVA: 0x00006E7C File Offset: 0x0000507C
		internal bool InternalWrite(byte[] buffer, int offset, int size)
		{
			bool flag;
			try
			{
				AsyncProtocolRequest asyncProtocolRequest = this.asyncHandshakeRequest ?? this.asyncWriteRequest;
				flag = this.InternalWrite(asyncProtocolRequest, this.writeBuffer, buffer, offset, size);
			}
			catch (Exception ex)
			{
				this.SetException(MobileAuthenticatedStream.GetIOException(ex, "InternalWrite() failed"));
				flag = false;
			}
			return flag;
		}

		// Token: 0x06000247 RID: 583 RVA: 0x00006ED8 File Offset: 0x000050D8
		private bool InternalWrite(AsyncProtocolRequest asyncRequest, BufferOffsetSize2 internalBuffer, byte[] buffer, int offset, int size)
		{
			if (asyncRequest == null)
			{
				if (this.lastException != null)
				{
					return false;
				}
				if (Interlocked.Exchange(ref this.closeRequested, 1) == 0)
				{
					internalBuffer.Reset();
				}
				else if (internalBuffer.Remaining == 0)
				{
					throw new InvalidOperationException();
				}
			}
			internalBuffer.AppendData(buffer, offset, size);
			if (asyncRequest != null)
			{
				asyncRequest.RequestWrite();
			}
			return true;
		}

		// Token: 0x06000248 RID: 584 RVA: 0x00006F2C File Offset: 0x0000512C
		internal async Task<int> InnerRead(bool sync, int requestedSize, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			int len = Math.Min(this.readBuffer.Remaining, requestedSize);
			if (len == 0)
			{
				throw new InvalidOperationException();
			}
			Task<int> task;
			if (sync)
			{
				task = Task.Run<int>(() => this.InnerStream.Read(this.readBuffer.Buffer, this.readBuffer.EndOffset, len));
			}
			else
			{
				task = base.InnerStream.ReadAsync(this.readBuffer.Buffer, this.readBuffer.EndOffset, len, cancellationToken);
			}
			int num = await task.ConfigureAwait(false);
			if (num >= 0)
			{
				this.readBuffer.Size += num;
				this.readBuffer.TotalBytes += num;
			}
			if (num == 0)
			{
				this.readBuffer.Complete = true;
				if (this.readBuffer.TotalBytes > 0)
				{
					num = -1;
				}
			}
			return num;
		}

		// Token: 0x06000249 RID: 585 RVA: 0x00006F8C File Offset: 0x0000518C
		internal async Task InnerWrite(bool sync, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			if (this.writeBuffer.Size != 0)
			{
				Task task;
				if (sync)
				{
					task = Task.Run(delegate
					{
						base.InnerStream.Write(this.writeBuffer.Buffer, this.writeBuffer.Offset, this.writeBuffer.Size);
					});
				}
				else
				{
					task = base.InnerStream.WriteAsync(this.writeBuffer.Buffer, this.writeBuffer.Offset, this.writeBuffer.Size);
				}
				await task.ConfigureAwait(false);
				this.writeBuffer.TotalBytes += this.writeBuffer.Size;
				BufferOffsetSize bufferOffsetSize = this.writeBuffer;
				BufferOffsetSize bufferOffsetSize2 = this.writeBuffer;
				int num = 0;
				bufferOffsetSize2.Size = num;
				bufferOffsetSize.Offset = num;
			}
		}

		// Token: 0x0600024A RID: 586 RVA: 0x00006FE4 File Offset: 0x000051E4
		internal AsyncOperationStatus ProcessHandshake(AsyncOperationStatus status)
		{
			object obj = this.ioLock;
			AsyncOperationStatus asyncOperationStatus;
			lock (obj)
			{
				if (status == AsyncOperationStatus.Initialize)
				{
					this.xobileTlsContext.StartHandshake();
					asyncOperationStatus = AsyncOperationStatus.Continue;
				}
				else
				{
					if (status == AsyncOperationStatus.ReadDone)
					{
						throw new IOException("Authentication failed because the remote party has closed the transport stream.");
					}
					if (status != AsyncOperationStatus.Continue)
					{
						throw new InvalidOperationException();
					}
					AsyncOperationStatus asyncOperationStatus2 = AsyncOperationStatus.Continue;
					if (this.xobileTlsContext.ProcessHandshake())
					{
						this.xobileTlsContext.FinishHandshake();
						asyncOperationStatus2 = AsyncOperationStatus.Complete;
					}
					if (this.lastException != null)
					{
						this.lastException.Throw();
					}
					asyncOperationStatus = asyncOperationStatus2;
				}
			}
			return asyncOperationStatus;
		}

		// Token: 0x0600024B RID: 587 RVA: 0x0000707C File Offset: 0x0000527C
		internal ValueTuple<int, bool> ProcessRead(BufferOffsetSize userBuffer)
		{
			object obj = this.ioLock;
			ValueTuple<int, bool> valueTuple2;
			lock (obj)
			{
				ValueTuple<int, bool> valueTuple = this.xobileTlsContext.Read(userBuffer.Buffer, userBuffer.Offset, userBuffer.Size);
				if (this.lastException != null)
				{
					this.lastException.Throw();
				}
				valueTuple2 = valueTuple;
			}
			return valueTuple2;
		}

		// Token: 0x0600024C RID: 588 RVA: 0x000070E8 File Offset: 0x000052E8
		internal ValueTuple<int, bool> ProcessWrite(BufferOffsetSize userBuffer)
		{
			object obj = this.ioLock;
			ValueTuple<int, bool> valueTuple2;
			lock (obj)
			{
				ValueTuple<int, bool> valueTuple = this.xobileTlsContext.Write(userBuffer.Buffer, userBuffer.Offset, userBuffer.Size);
				if (this.lastException != null)
				{
					this.lastException.Throw();
				}
				valueTuple2 = valueTuple;
			}
			return valueTuple2;
		}

		// Token: 0x0600024D RID: 589 RVA: 0x00007154 File Offset: 0x00005354
		internal AsyncOperationStatus ProcessShutdown(AsyncOperationStatus status)
		{
			object obj = this.ioLock;
			AsyncOperationStatus asyncOperationStatus;
			lock (obj)
			{
				this.xobileTlsContext.Shutdown();
				this.shutdown = true;
				asyncOperationStatus = AsyncOperationStatus.Complete;
			}
			return asyncOperationStatus;
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x0600024E RID: 590 RVA: 0x000071A4 File Offset: 0x000053A4
		public override bool IsServer
		{
			get
			{
				this.CheckThrow(false, false);
				return this.xobileTlsContext != null && this.xobileTlsContext.IsServer;
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x0600024F RID: 591 RVA: 0x000071C4 File Offset: 0x000053C4
		public override bool IsAuthenticated
		{
			get
			{
				object obj = this.ioLock;
				bool flag2;
				lock (obj)
				{
					flag2 = this.xobileTlsContext != null && this.lastException == null && this.xobileTlsContext.IsAuthenticated;
				}
				return flag2;
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000250 RID: 592 RVA: 0x00007220 File Offset: 0x00005420
		public override bool IsMutuallyAuthenticated
		{
			get
			{
				object obj = this.ioLock;
				bool flag2;
				lock (obj)
				{
					if (!this.IsAuthenticated)
					{
						flag2 = false;
					}
					else if ((this.xobileTlsContext.IsServer ? this.xobileTlsContext.LocalServerCertificate : this.xobileTlsContext.LocalClientCertificate) == null)
					{
						flag2 = false;
					}
					else
					{
						flag2 = this.xobileTlsContext.IsRemoteCertificateAvailable;
					}
				}
				return flag2;
			}
		}

		// Token: 0x06000251 RID: 593 RVA: 0x000072A0 File Offset: 0x000054A0
		protected override void Dispose(bool disposing)
		{
			try
			{
				object obj = this.ioLock;
				lock (obj)
				{
					this.lastException = ExceptionDispatchInfo.Capture(new ObjectDisposedException("MobileAuthenticatedStream"));
					if (this.xobileTlsContext != null)
					{
						this.xobileTlsContext.Dispose();
						this.xobileTlsContext = null;
					}
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x06000252 RID: 594 RVA: 0x00007320 File Offset: 0x00005520
		public override void Flush()
		{
			base.InnerStream.Flush();
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000253 RID: 595 RVA: 0x00007330 File Offset: 0x00005530
		public SslProtocols SslProtocol
		{
			get
			{
				object obj = this.ioLock;
				SslProtocols negotiatedProtocol;
				lock (obj)
				{
					this.CheckThrow(true, false);
					negotiatedProtocol = (SslProtocols)this.xobileTlsContext.NegotiatedProtocol;
				}
				return negotiatedProtocol;
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000254 RID: 596 RVA: 0x00007380 File Offset: 0x00005580
		public X509Certificate RemoteCertificate
		{
			get
			{
				object obj = this.ioLock;
				X509Certificate remoteCertificate;
				lock (obj)
				{
					this.CheckThrow(true, false);
					remoteCertificate = this.xobileTlsContext.RemoteCertificate;
				}
				return remoteCertificate;
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000255 RID: 597 RVA: 0x000073D0 File Offset: 0x000055D0
		public X509Certificate LocalCertificate
		{
			get
			{
				object obj = this.ioLock;
				X509Certificate internalLocalCertificate;
				lock (obj)
				{
					this.CheckThrow(true, false);
					internalLocalCertificate = this.InternalLocalCertificate;
				}
				return internalLocalCertificate;
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000256 RID: 598 RVA: 0x0000741C File Offset: 0x0000561C
		public X509Certificate InternalLocalCertificate
		{
			get
			{
				object obj = this.ioLock;
				X509Certificate x509Certificate;
				lock (obj)
				{
					this.CheckThrow(false, false);
					if (this.xobileTlsContext == null)
					{
						x509Certificate = null;
					}
					else
					{
						x509Certificate = (this.xobileTlsContext.IsServer ? this.xobileTlsContext.LocalServerCertificate : this.xobileTlsContext.LocalClientCertificate);
					}
				}
				return x509Certificate;
			}
		}

		// Token: 0x06000257 RID: 599 RVA: 0x00007494 File Offset: 0x00005694
		public MonoTlsConnectionInfo GetConnectionInfo()
		{
			object obj = this.ioLock;
			MonoTlsConnectionInfo connectionInfo;
			lock (obj)
			{
				this.CheckThrow(true, false);
				connectionInfo = this.xobileTlsContext.ConnectionInfo;
			}
			return connectionInfo;
		}

		// Token: 0x06000258 RID: 600 RVA: 0x000074E4 File Offset: 0x000056E4
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000259 RID: 601 RVA: 0x000074EB File Offset: 0x000056EB
		public override void SetLength(long value)
		{
			base.InnerStream.SetLength(value);
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x0600025A RID: 602 RVA: 0x000074E4 File Offset: 0x000056E4
		public TransportContext TransportContext
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x0600025B RID: 603 RVA: 0x000074F9 File Offset: 0x000056F9
		public override bool CanRead
		{
			get
			{
				return this.IsAuthenticated && base.InnerStream.CanRead;
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x0600025C RID: 604 RVA: 0x00007510 File Offset: 0x00005710
		public override bool CanTimeout
		{
			get
			{
				return base.InnerStream.CanTimeout;
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x0600025D RID: 605 RVA: 0x0000751D File Offset: 0x0000571D
		public override bool CanWrite
		{
			get
			{
				return (this.IsAuthenticated & base.InnerStream.CanWrite) && !this.shutdown;
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x0600025E RID: 606 RVA: 0x00004240 File Offset: 0x00002440
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x0600025F RID: 607 RVA: 0x0000753E File Offset: 0x0000573E
		public override long Length
		{
			get
			{
				return base.InnerStream.Length;
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000260 RID: 608 RVA: 0x0000754B File Offset: 0x0000574B
		// (set) Token: 0x06000261 RID: 609 RVA: 0x000074E4 File Offset: 0x000056E4
		public override long Position
		{
			get
			{
				return base.InnerStream.Position;
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x06000262 RID: 610 RVA: 0x00007558 File Offset: 0x00005758
		public override bool IsEncrypted
		{
			get
			{
				return this.IsAuthenticated;
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x06000263 RID: 611 RVA: 0x00007558 File Offset: 0x00005758
		public override bool IsSigned
		{
			get
			{
				return this.IsAuthenticated;
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x06000264 RID: 612 RVA: 0x00007560 File Offset: 0x00005760
		// (set) Token: 0x06000265 RID: 613 RVA: 0x0000756D File Offset: 0x0000576D
		public override int ReadTimeout
		{
			get
			{
				return base.InnerStream.ReadTimeout;
			}
			set
			{
				base.InnerStream.ReadTimeout = value;
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x06000266 RID: 614 RVA: 0x0000757B File Offset: 0x0000577B
		// (set) Token: 0x06000267 RID: 615 RVA: 0x00007588 File Offset: 0x00005788
		public override int WriteTimeout
		{
			get
			{
				return base.InnerStream.WriteTimeout;
			}
			set
			{
				base.InnerStream.WriteTimeout = value;
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x06000268 RID: 616 RVA: 0x00007598 File Offset: 0x00005798
		public global::System.Security.Authentication.CipherAlgorithmType CipherAlgorithm
		{
			get
			{
				this.CheckThrow(true, false);
				MonoTlsConnectionInfo connectionInfo = this.GetConnectionInfo();
				if (connectionInfo == null)
				{
					return global::System.Security.Authentication.CipherAlgorithmType.None;
				}
				switch (connectionInfo.CipherAlgorithmType)
				{
				case Mono.Security.Interface.CipherAlgorithmType.Aes128:
				case Mono.Security.Interface.CipherAlgorithmType.AesGcm128:
					return global::System.Security.Authentication.CipherAlgorithmType.Aes128;
				case Mono.Security.Interface.CipherAlgorithmType.Aes256:
				case Mono.Security.Interface.CipherAlgorithmType.AesGcm256:
					return global::System.Security.Authentication.CipherAlgorithmType.Aes256;
				default:
					return global::System.Security.Authentication.CipherAlgorithmType.None;
				}
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x06000269 RID: 617 RVA: 0x000075E8 File Offset: 0x000057E8
		public global::System.Security.Authentication.HashAlgorithmType HashAlgorithm
		{
			get
			{
				this.CheckThrow(true, false);
				MonoTlsConnectionInfo connectionInfo = this.GetConnectionInfo();
				if (connectionInfo == null)
				{
					return global::System.Security.Authentication.HashAlgorithmType.None;
				}
				Mono.Security.Interface.HashAlgorithmType hashAlgorithmType = connectionInfo.HashAlgorithmType;
				if (hashAlgorithmType != Mono.Security.Interface.HashAlgorithmType.Md5)
				{
					if (hashAlgorithmType - Mono.Security.Interface.HashAlgorithmType.Sha1 <= 4)
					{
						return global::System.Security.Authentication.HashAlgorithmType.Sha1;
					}
					if (hashAlgorithmType != Mono.Security.Interface.HashAlgorithmType.Md5Sha1)
					{
						return global::System.Security.Authentication.HashAlgorithmType.None;
					}
				}
				return global::System.Security.Authentication.HashAlgorithmType.Md5;
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x0600026A RID: 618 RVA: 0x00007630 File Offset: 0x00005830
		public global::System.Security.Authentication.ExchangeAlgorithmType KeyExchangeAlgorithm
		{
			get
			{
				this.CheckThrow(true, false);
				MonoTlsConnectionInfo connectionInfo = this.GetConnectionInfo();
				if (connectionInfo == null)
				{
					return global::System.Security.Authentication.ExchangeAlgorithmType.None;
				}
				switch (connectionInfo.ExchangeAlgorithmType)
				{
				case Mono.Security.Interface.ExchangeAlgorithmType.Dhe:
				case Mono.Security.Interface.ExchangeAlgorithmType.EcDhe:
					return global::System.Security.Authentication.ExchangeAlgorithmType.DiffieHellman;
				case Mono.Security.Interface.ExchangeAlgorithmType.Rsa:
					return global::System.Security.Authentication.ExchangeAlgorithmType.RsaSign;
				default:
					return global::System.Security.Authentication.ExchangeAlgorithmType.None;
				}
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x0600026B RID: 619 RVA: 0x00004239 File Offset: 0x00002439
		public int CipherStrength
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x0600026C RID: 620 RVA: 0x00004239 File Offset: 0x00002439
		public int HashStrength
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x0600026D RID: 621 RVA: 0x00004239 File Offset: 0x00002439
		public int KeyExchangeStrength
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x0600026E RID: 622 RVA: 0x00004239 File Offset: 0x00002439
		public bool CheckCertRevocationStatus
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x040007B1 RID: 1969
		private MobileTlsContext xobileTlsContext;

		// Token: 0x040007B2 RID: 1970
		private ExceptionDispatchInfo lastException;

		// Token: 0x040007B3 RID: 1971
		private AsyncProtocolRequest asyncHandshakeRequest;

		// Token: 0x040007B4 RID: 1972
		private AsyncProtocolRequest asyncReadRequest;

		// Token: 0x040007B5 RID: 1973
		private AsyncProtocolRequest asyncWriteRequest;

		// Token: 0x040007B6 RID: 1974
		private BufferOffsetSize2 readBuffer;

		// Token: 0x040007B7 RID: 1975
		private BufferOffsetSize2 writeBuffer;

		// Token: 0x040007B8 RID: 1976
		private object ioLock = new object();

		// Token: 0x040007B9 RID: 1977
		private int closeRequested;

		// Token: 0x040007BA RID: 1978
		private bool shutdown;

		// Token: 0x040007BB RID: 1979
		private static int uniqueNameInteger = 123;

		// Token: 0x040007BF RID: 1983
		private static int nextId;

		// Token: 0x040007C0 RID: 1984
		internal readonly int ID = ++MobileAuthenticatedStream.nextId;

		// Token: 0x02000073 RID: 115
		private enum OperationType
		{
			// Token: 0x040007C2 RID: 1986
			Read,
			// Token: 0x040007C3 RID: 1987
			Write,
			// Token: 0x040007C4 RID: 1988
			Shutdown
		}
	}
}
