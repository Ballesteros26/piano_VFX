using System;
using System.ComponentModel;
using System.IO;
using System.IO.Pipes;
using System.Net.Security;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace System.Data.SqlClient.SNI
{
	// Token: 0x0200024C RID: 588
	internal class SNINpHandle : SNIHandle
	{
		// Token: 0x06001A0A RID: 6666 RVA: 0x00083E38 File Offset: 0x00082038
		public SNINpHandle(string serverName, string pipeName, long timerExpire, object callbackObject)
		{
			this._targetServer = serverName;
			this._callbackObject = callbackObject;
			this._writeScheduler = new ConcurrentExclusiveSchedulerPair().ExclusiveScheduler;
			this._writeTaskFactory = new TaskFactory(this._writeScheduler);
			try
			{
				this._pipeStream = new NamedPipeClientStream(serverName, pipeName, PipeDirection.InOut, PipeOptions.WriteThrough | PipeOptions.Asynchronous);
				if (9223372036854775807L == timerExpire)
				{
					this._pipeStream.Connect(-1);
				}
				else
				{
					TimeSpan timeSpan = DateTime.FromFileTime(timerExpire) - DateTime.Now;
					timeSpan = ((timeSpan.Ticks < 0L) ? TimeSpan.FromTicks(0L) : timeSpan);
					this._pipeStream.Connect((int)timeSpan.TotalMilliseconds);
				}
			}
			catch (TimeoutException ex)
			{
				SNICommon.ReportSNIError(SNIProviders.NP_PROV, 40U, ex);
				this._status = 1U;
				return;
			}
			catch (IOException ex2)
			{
				SNICommon.ReportSNIError(SNIProviders.NP_PROV, 40U, ex2);
				this._status = 1U;
				return;
			}
			if (!this._pipeStream.IsConnected || !this._pipeStream.CanWrite || !this._pipeStream.CanRead)
			{
				SNICommon.ReportSNIError(SNIProviders.NP_PROV, 0U, 40U, string.Empty);
				this._status = 1U;
				return;
			}
			this._sslOverTdsStream = new SslOverTdsStream(this._pipeStream);
			this._sslStream = new SslStream(this._sslOverTdsStream, true, new RemoteCertificateValidationCallback(this.ValidateServerCertificate), null);
			this._stream = this._pipeStream;
			this._status = 0U;
		}

		// Token: 0x170004C7 RID: 1223
		// (get) Token: 0x06001A0B RID: 6667 RVA: 0x00083FD8 File Offset: 0x000821D8
		public override Guid ConnectionId
		{
			get
			{
				return this._connectionId;
			}
		}

		// Token: 0x170004C8 RID: 1224
		// (get) Token: 0x06001A0C RID: 6668 RVA: 0x00083FE0 File Offset: 0x000821E0
		public override uint Status
		{
			get
			{
				return this._status;
			}
		}

		// Token: 0x06001A0D RID: 6669 RVA: 0x00083FE8 File Offset: 0x000821E8
		public override uint CheckConnection()
		{
			if (!this._stream.CanWrite || !this._stream.CanRead)
			{
				return 1U;
			}
			return 0U;
		}

		// Token: 0x06001A0E RID: 6670 RVA: 0x00084008 File Offset: 0x00082208
		public override void Dispose()
		{
			lock (this)
			{
				if (this._sslOverTdsStream != null)
				{
					this._sslOverTdsStream.Dispose();
					this._sslOverTdsStream = null;
				}
				if (this._sslStream != null)
				{
					this._sslStream.Dispose();
					this._sslStream = null;
				}
				if (this._pipeStream != null)
				{
					this._pipeStream.Dispose();
					this._pipeStream = null;
				}
				this._stream = null;
			}
		}

		// Token: 0x06001A0F RID: 6671 RVA: 0x00084094 File Offset: 0x00082294
		public override uint Receive(out SNIPacket packet, int timeout)
		{
			uint num;
			lock (this)
			{
				packet = null;
				try
				{
					packet = new SNIPacket(null);
					packet.Allocate(this._bufferSize);
					packet.ReadFromStream(this._stream);
					if (packet.Length == 0)
					{
						Win32Exception ex = new Win32Exception();
						return this.ReportErrorAndReleasePacket(packet, (uint)ex.NativeErrorCode, 0U, ex.Message);
					}
				}
				catch (ObjectDisposedException ex2)
				{
					return this.ReportErrorAndReleasePacket(packet, ex2);
				}
				catch (IOException ex3)
				{
					return this.ReportErrorAndReleasePacket(packet, ex3);
				}
				num = 0U;
			}
			return num;
		}

		// Token: 0x06001A10 RID: 6672 RVA: 0x00084150 File Offset: 0x00082350
		public override uint ReceiveAsync(ref SNIPacket packet)
		{
			uint num;
			lock (this)
			{
				packet = new SNIPacket(null);
				packet.Allocate(this._bufferSize);
				try
				{
					packet.ReadFromStreamAsync(this._stream, this._receiveCallback);
					num = 997U;
				}
				catch (ObjectDisposedException ex)
				{
					num = this.ReportErrorAndReleasePacket(packet, ex);
				}
				catch (IOException ex2)
				{
					num = this.ReportErrorAndReleasePacket(packet, ex2);
				}
			}
			return num;
		}

		// Token: 0x06001A11 RID: 6673 RVA: 0x000841E8 File Offset: 0x000823E8
		public override uint Send(SNIPacket packet)
		{
			uint num;
			lock (this)
			{
				try
				{
					packet.WriteToStream(this._stream);
					num = 0U;
				}
				catch (ObjectDisposedException ex)
				{
					num = this.ReportErrorAndReleasePacket(packet, ex);
				}
				catch (IOException ex2)
				{
					num = this.ReportErrorAndReleasePacket(packet, ex2);
				}
			}
			return num;
		}

		// Token: 0x06001A12 RID: 6674 RVA: 0x00084260 File Offset: 0x00082460
		public override uint SendAsync(SNIPacket packet, SNIAsyncCallback callback = null)
		{
			SNIPacket packet2 = packet;
			this._writeTaskFactory.StartNew(delegate
			{
				try
				{
					SNINpHandle <>4__this = this;
					lock (<>4__this)
					{
						packet.WriteToStream(this._stream);
					}
				}
				catch (Exception ex)
				{
					SNICommon.ReportSNIError(SNIProviders.NP_PROV, 35U, ex);
					if (callback != null)
					{
						callback(packet, 1U);
					}
					else
					{
						this._sendCallback(packet, 1U);
					}
					return;
				}
				if (callback != null)
				{
					callback(packet, 0U);
					return;
				}
				this._sendCallback(packet, 0U);
			});
			return 997U;
		}

		// Token: 0x06001A13 RID: 6675 RVA: 0x000842AC File Offset: 0x000824AC
		public override void SetAsyncCallbacks(SNIAsyncCallback receiveCallback, SNIAsyncCallback sendCallback)
		{
			this._receiveCallback = receiveCallback;
			this._sendCallback = sendCallback;
		}

		// Token: 0x06001A14 RID: 6676 RVA: 0x000842BC File Offset: 0x000824BC
		public override uint EnableSsl(uint options)
		{
			this._validateCert = (options & 1U) > 0U;
			try
			{
				this._sslStream.AuthenticateAsClientAsync(this._targetServer).GetAwaiter().GetResult();
				this._sslOverTdsStream.FinishHandshake();
			}
			catch (AuthenticationException ex)
			{
				return SNICommon.ReportSNIError(SNIProviders.NP_PROV, 35U, ex);
			}
			catch (InvalidOperationException ex2)
			{
				return SNICommon.ReportSNIError(SNIProviders.NP_PROV, 35U, ex2);
			}
			this._stream = this._sslStream;
			return 0U;
		}

		// Token: 0x06001A15 RID: 6677 RVA: 0x00084348 File Offset: 0x00082548
		public override void DisableSsl()
		{
			this._sslStream.Dispose();
			this._sslStream = null;
			this._sslOverTdsStream.Dispose();
			this._sslOverTdsStream = null;
			this._stream = this._pipeStream;
		}

		// Token: 0x06001A16 RID: 6678 RVA: 0x0008437A File Offset: 0x0008257A
		private bool ValidateServerCertificate(object sender, X509Certificate cert, X509Chain chain, SslPolicyErrors policyErrors)
		{
			return !this._validateCert || SNICommon.ValidateSslServerCertificate(this._targetServer, sender, cert, chain, policyErrors);
		}

		// Token: 0x06001A17 RID: 6679 RVA: 0x00084396 File Offset: 0x00082596
		public override void SetBufferSize(int bufferSize)
		{
			this._bufferSize = bufferSize;
		}

		// Token: 0x06001A18 RID: 6680 RVA: 0x0008439F File Offset: 0x0008259F
		private uint ReportErrorAndReleasePacket(SNIPacket packet, Exception sniException)
		{
			if (packet != null)
			{
				packet.Release();
			}
			return SNICommon.ReportSNIError(SNIProviders.NP_PROV, 35U, sniException);
		}

		// Token: 0x06001A19 RID: 6681 RVA: 0x000843B3 File Offset: 0x000825B3
		private uint ReportErrorAndReleasePacket(SNIPacket packet, uint nativeError, uint sniError, string errorMessage)
		{
			if (packet != null)
			{
				packet.Release();
			}
			return SNICommon.ReportSNIError(SNIProviders.NP_PROV, nativeError, sniError, errorMessage);
		}

		// Token: 0x040012B5 RID: 4789
		internal const string DefaultPipePath = "sql\\query";

		// Token: 0x040012B6 RID: 4790
		private const int MAX_PIPE_INSTANCES = 255;

		// Token: 0x040012B7 RID: 4791
		private readonly string _targetServer;

		// Token: 0x040012B8 RID: 4792
		private readonly object _callbackObject;

		// Token: 0x040012B9 RID: 4793
		private readonly TaskScheduler _writeScheduler;

		// Token: 0x040012BA RID: 4794
		private readonly TaskFactory _writeTaskFactory;

		// Token: 0x040012BB RID: 4795
		private Stream _stream;

		// Token: 0x040012BC RID: 4796
		private NamedPipeClientStream _pipeStream;

		// Token: 0x040012BD RID: 4797
		private SslOverTdsStream _sslOverTdsStream;

		// Token: 0x040012BE RID: 4798
		private SslStream _sslStream;

		// Token: 0x040012BF RID: 4799
		private SNIAsyncCallback _receiveCallback;

		// Token: 0x040012C0 RID: 4800
		private SNIAsyncCallback _sendCallback;

		// Token: 0x040012C1 RID: 4801
		private bool _validateCert = true;

		// Token: 0x040012C2 RID: 4802
		private readonly uint _status = uint.MaxValue;

		// Token: 0x040012C3 RID: 4803
		private int _bufferSize = 4096;

		// Token: 0x040012C4 RID: 4804
		private readonly Guid _connectionId = Guid.NewGuid();
	}
}
