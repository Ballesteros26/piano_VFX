using System;
using System.IO;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using Mono.Security.Interface;
using Mono.Security.Protocol.Tls.Handshake;
using Mono.Security.X509;

namespace Mono.Security.Protocol.Tls
{
	// Token: 0x0200004A RID: 74
	public class SslClientStream : SslStreamBase
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x060002D9 RID: 729 RVA: 0x00010674 File Offset: 0x0000E874
		// (remove) Token: 0x060002DA RID: 730 RVA: 0x000106AC File Offset: 0x0000E8AC
		internal event CertificateValidationCallback ServerCertValidation;

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x060002DB RID: 731 RVA: 0x000106E4 File Offset: 0x0000E8E4
		// (remove) Token: 0x060002DC RID: 732 RVA: 0x0001071C File Offset: 0x0000E91C
		internal event CertificateSelectionCallback ClientCertSelection;

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x060002DD RID: 733 RVA: 0x00010754 File Offset: 0x0000E954
		// (remove) Token: 0x060002DE RID: 734 RVA: 0x0001078C File Offset: 0x0000E98C
		internal event PrivateKeySelectionCallback PrivateKeySelection;

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x060002DF RID: 735 RVA: 0x000107C1 File Offset: 0x0000E9C1
		internal Stream InputBuffer
		{
			get
			{
				return this.inputBuffer;
			}
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x060002E0 RID: 736 RVA: 0x000107C9 File Offset: 0x0000E9C9
		public global::System.Security.Cryptography.X509Certificates.X509CertificateCollection ClientCertificates
		{
			get
			{
				return this.context.ClientSettings.Certificates;
			}
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x060002E1 RID: 737 RVA: 0x000107DB File Offset: 0x0000E9DB
		public global::System.Security.Cryptography.X509Certificates.X509Certificate SelectedClientCertificate
		{
			get
			{
				return this.context.ClientSettings.ClientCertificate;
			}
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x060002E2 RID: 738 RVA: 0x000107ED File Offset: 0x0000E9ED
		// (set) Token: 0x060002E3 RID: 739 RVA: 0x000107F5 File Offset: 0x0000E9F5
		public CertificateValidationCallback ServerCertValidationDelegate
		{
			get
			{
				return this.ServerCertValidation;
			}
			set
			{
				this.ServerCertValidation = value;
			}
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x060002E4 RID: 740 RVA: 0x000107FE File Offset: 0x0000E9FE
		// (set) Token: 0x060002E5 RID: 741 RVA: 0x00010806 File Offset: 0x0000EA06
		public CertificateSelectionCallback ClientCertSelectionDelegate
		{
			get
			{
				return this.ClientCertSelection;
			}
			set
			{
				this.ClientCertSelection = value;
			}
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x060002E6 RID: 742 RVA: 0x0001080F File Offset: 0x0000EA0F
		// (set) Token: 0x060002E7 RID: 743 RVA: 0x00010817 File Offset: 0x0000EA17
		public PrivateKeySelectionCallback PrivateKeyCertSelectionDelegate
		{
			get
			{
				return this.PrivateKeySelection;
			}
			set
			{
				this.PrivateKeySelection = value;
			}
		}

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x060002E8 RID: 744 RVA: 0x00010820 File Offset: 0x0000EA20
		// (remove) Token: 0x060002E9 RID: 745 RVA: 0x00010858 File Offset: 0x0000EA58
		public event CertificateValidationCallback2 ServerCertValidation2;

		// Token: 0x060002EA RID: 746 RVA: 0x0001088D File Offset: 0x0000EA8D
		public SslClientStream(Stream stream, string targetHost, bool ownsStream)
			: this(stream, targetHost, ownsStream, SecurityProtocolType.Default, null)
		{
		}

		// Token: 0x060002EB RID: 747 RVA: 0x000108A0 File Offset: 0x0000EAA0
		public SslClientStream(Stream stream, string targetHost, global::System.Security.Cryptography.X509Certificates.X509Certificate clientCertificate)
			: this(stream, targetHost, false, SecurityProtocolType.Default, new global::System.Security.Cryptography.X509Certificates.X509CertificateCollection(new global::System.Security.Cryptography.X509Certificates.X509Certificate[] { clientCertificate }))
		{
		}

		// Token: 0x060002EC RID: 748 RVA: 0x000108CA File Offset: 0x0000EACA
		public SslClientStream(Stream stream, string targetHost, global::System.Security.Cryptography.X509Certificates.X509CertificateCollection clientCertificates)
			: this(stream, targetHost, false, SecurityProtocolType.Default, clientCertificates)
		{
		}

		// Token: 0x060002ED RID: 749 RVA: 0x000108DB File Offset: 0x0000EADB
		public SslClientStream(Stream stream, string targetHost, bool ownsStream, SecurityProtocolType securityProtocolType)
			: this(stream, targetHost, ownsStream, securityProtocolType, new global::System.Security.Cryptography.X509Certificates.X509CertificateCollection())
		{
		}

		// Token: 0x060002EE RID: 750 RVA: 0x000108F0 File Offset: 0x0000EAF0
		public SslClientStream(Stream stream, string targetHost, bool ownsStream, SecurityProtocolType securityProtocolType, global::System.Security.Cryptography.X509Certificates.X509CertificateCollection clientCertificates)
			: base(stream, ownsStream)
		{
			if (targetHost == null || targetHost.Length == 0)
			{
				throw new ArgumentNullException("targetHost is null or an empty string.");
			}
			this.context = new ClientContext(this, securityProtocolType, targetHost, clientCertificates);
			this.protocol = new ClientRecordProtocol(this.innerStream, (ClientContext)this.context);
		}

		// Token: 0x060002EF RID: 751 RVA: 0x00010948 File Offset: 0x0000EB48
		~SslClientStream()
		{
			base.Dispose(false);
		}

		// Token: 0x060002F0 RID: 752 RVA: 0x00010978 File Offset: 0x0000EB78
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
			if (disposing)
			{
				this.ServerCertValidation = null;
				this.ClientCertSelection = null;
				this.PrivateKeySelection = null;
				this.ServerCertValidation2 = null;
			}
		}

		// Token: 0x060002F1 RID: 753 RVA: 0x000109A0 File Offset: 0x0000EBA0
		private void SafeEndReceiveRecord(IAsyncResult ar, bool ignoreEmpty = false)
		{
			byte[] array = this.protocol.EndReceiveRecord(ar);
			if (!ignoreEmpty && (array == null || array.Length == 0))
			{
				throw new TlsException(AlertDescription.HandshakeFailiure, "The server stopped the handshake.");
			}
		}

		// Token: 0x060002F2 RID: 754 RVA: 0x000109D4 File Offset: 0x0000EBD4
		internal override IAsyncResult BeginNegotiateHandshake(AsyncCallback callback, object state)
		{
			if (this.context.HandshakeState != HandshakeState.None)
			{
				this.context.Clear();
			}
			this.context.SupportedCiphers = CipherSuiteFactory.GetSupportedCiphers(false, this.context.SecurityProtocol);
			this.context.HandshakeState = HandshakeState.Started;
			SslClientStream.NegotiateAsyncResult negotiateAsyncResult = new SslClientStream.NegotiateAsyncResult(callback, state, SslClientStream.NegotiateState.SentClientHello);
			this.protocol.BeginSendRecord(HandshakeType.ClientHello, new AsyncCallback(this.NegotiateAsyncWorker), negotiateAsyncResult);
			return negotiateAsyncResult;
		}

		// Token: 0x060002F3 RID: 755 RVA: 0x00010A48 File Offset: 0x0000EC48
		internal override void EndNegotiateHandshake(IAsyncResult result)
		{
			SslClientStream.NegotiateAsyncResult negotiateAsyncResult = result as SslClientStream.NegotiateAsyncResult;
			if (negotiateAsyncResult == null)
			{
				throw new ArgumentNullException();
			}
			if (!negotiateAsyncResult.IsCompleted)
			{
				negotiateAsyncResult.AsyncWaitHandle.WaitOne();
			}
			if (negotiateAsyncResult.CompletedWithError)
			{
				throw negotiateAsyncResult.AsyncException;
			}
		}

		// Token: 0x060002F4 RID: 756 RVA: 0x00010A88 File Offset: 0x0000EC88
		private void NegotiateAsyncWorker(IAsyncResult result)
		{
			SslClientStream.NegotiateAsyncResult negotiateAsyncResult = result.AsyncState as SslClientStream.NegotiateAsyncResult;
			try
			{
				switch (negotiateAsyncResult.State)
				{
				case SslClientStream.NegotiateState.SentClientHello:
					this.protocol.EndSendRecord(result);
					negotiateAsyncResult.State = SslClientStream.NegotiateState.ReceiveClientHelloResponse;
					this.protocol.BeginReceiveRecord(this.innerStream, new AsyncCallback(this.NegotiateAsyncWorker), negotiateAsyncResult);
					goto IL_03C2;
				case SslClientStream.NegotiateState.ReceiveClientHelloResponse:
				{
					this.SafeEndReceiveRecord(result, true);
					if (this.context.LastHandshakeMsg != HandshakeType.ServerHelloDone && (!this.context.AbbreviatedHandshake || this.context.LastHandshakeMsg != HandshakeType.ServerHello))
					{
						this.protocol.BeginReceiveRecord(this.innerStream, new AsyncCallback(this.NegotiateAsyncWorker), negotiateAsyncResult);
						goto IL_03C2;
					}
					if (this.context.AbbreviatedHandshake)
					{
						ClientSessionCache.SetContextFromCache(this.context);
						this.context.Negotiating.Cipher.ComputeKeys();
						this.context.Negotiating.Cipher.InitializeCipher();
						negotiateAsyncResult.State = SslClientStream.NegotiateState.SentCipherSpec;
						this.protocol.BeginSendChangeCipherSpec(new AsyncCallback(this.NegotiateAsyncWorker), negotiateAsyncResult);
						goto IL_03C2;
					}
					bool flag = this.context.ServerSettings.CertificateRequest;
					using (MemoryStream memoryStream = new MemoryStream())
					{
						if (this.context.SecurityProtocol == SecurityProtocolType.Ssl3)
						{
							flag = this.context.ClientSettings.Certificates != null && this.context.ClientSettings.Certificates.Count > 0;
						}
						byte[] array;
						if (flag)
						{
							array = this.protocol.EncodeHandshakeRecord(HandshakeType.Certificate);
							memoryStream.Write(array, 0, array.Length);
						}
						array = this.protocol.EncodeHandshakeRecord(HandshakeType.ClientKeyExchange);
						memoryStream.Write(array, 0, array.Length);
						this.context.Negotiating.Cipher.InitializeCipher();
						if (flag && this.context.ClientSettings.ClientCertificate != null)
						{
							array = this.protocol.EncodeHandshakeRecord(HandshakeType.CertificateVerify);
							memoryStream.Write(array, 0, array.Length);
						}
						this.protocol.SendChangeCipherSpec(memoryStream);
						array = this.protocol.EncodeHandshakeRecord(HandshakeType.Finished);
						memoryStream.Write(array, 0, array.Length);
						negotiateAsyncResult.State = SslClientStream.NegotiateState.SentKeyExchange;
						this.innerStream.BeginWrite(memoryStream.GetBuffer(), 0, (int)memoryStream.Length, new AsyncCallback(this.NegotiateAsyncWorker), negotiateAsyncResult);
						goto IL_03C2;
					}
					break;
				}
				case SslClientStream.NegotiateState.SentCipherSpec:
					this.protocol.EndSendChangeCipherSpec(result);
					negotiateAsyncResult.State = SslClientStream.NegotiateState.ReceiveCipherSpecResponse;
					this.protocol.BeginReceiveRecord(this.innerStream, new AsyncCallback(this.NegotiateAsyncWorker), negotiateAsyncResult);
					goto IL_03C2;
				case SslClientStream.NegotiateState.ReceiveCipherSpecResponse:
					this.SafeEndReceiveRecord(result, true);
					if (this.context.HandshakeState != HandshakeState.Finished)
					{
						this.protocol.BeginReceiveRecord(this.innerStream, new AsyncCallback(this.NegotiateAsyncWorker), negotiateAsyncResult);
						goto IL_03C2;
					}
					negotiateAsyncResult.State = SslClientStream.NegotiateState.SentFinished;
					this.protocol.BeginSendRecord(HandshakeType.Finished, new AsyncCallback(this.NegotiateAsyncWorker), negotiateAsyncResult);
					goto IL_03C2;
				case SslClientStream.NegotiateState.SentKeyExchange:
					break;
				case SslClientStream.NegotiateState.ReceiveFinishResponse:
					this.SafeEndReceiveRecord(result, false);
					if (this.context.HandshakeState != HandshakeState.Finished)
					{
						this.protocol.BeginReceiveRecord(this.innerStream, new AsyncCallback(this.NegotiateAsyncWorker), negotiateAsyncResult);
						goto IL_03C2;
					}
					this.context.HandshakeMessages.Reset();
					this.context.ClearKeyInfo();
					negotiateAsyncResult.SetComplete();
					goto IL_03C2;
				case SslClientStream.NegotiateState.SentFinished:
					this.protocol.EndSendRecord(result);
					this.context.HandshakeMessages.Reset();
					this.context.ClearKeyInfo();
					negotiateAsyncResult.SetComplete();
					goto IL_03C2;
				default:
					goto IL_03C2;
				}
				this.innerStream.EndWrite(result);
				negotiateAsyncResult.State = SslClientStream.NegotiateState.ReceiveFinishResponse;
				this.protocol.BeginReceiveRecord(this.innerStream, new AsyncCallback(this.NegotiateAsyncWorker), negotiateAsyncResult);
				IL_03C2:;
			}
			catch (TlsException ex)
			{
				try
				{
					Exception ex2 = ex;
					this.protocol.SendAlert(ref ex2);
				}
				catch
				{
				}
				negotiateAsyncResult.SetComplete(new IOException("The authentication or decryption has failed.", ex));
			}
			catch (Exception ex3)
			{
				try
				{
					this.protocol.SendAlert(AlertDescription.InternalError);
				}
				catch
				{
				}
				negotiateAsyncResult.SetComplete(new IOException("The authentication or decryption has failed.", ex3));
			}
		}

		// Token: 0x060002F5 RID: 757 RVA: 0x00010F2C File Offset: 0x0000F12C
		internal override global::System.Security.Cryptography.X509Certificates.X509Certificate OnLocalCertificateSelection(global::System.Security.Cryptography.X509Certificates.X509CertificateCollection clientCertificates, global::System.Security.Cryptography.X509Certificates.X509Certificate serverCertificate, string targetHost, global::System.Security.Cryptography.X509Certificates.X509CertificateCollection serverRequestedCertificates)
		{
			if (this.ClientCertSelection != null)
			{
				return this.ClientCertSelection(clientCertificates, serverCertificate, targetHost, serverRequestedCertificates);
			}
			return null;
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x060002F6 RID: 758 RVA: 0x00010F48 File Offset: 0x0000F148
		internal override bool HaveRemoteValidation2Callback
		{
			get
			{
				return this.ServerCertValidation2 != null;
			}
		}

		// Token: 0x060002F7 RID: 759 RVA: 0x00010F54 File Offset: 0x0000F154
		internal override ValidationResult OnRemoteCertificateValidation2(Mono.Security.X509.X509CertificateCollection collection)
		{
			CertificateValidationCallback2 serverCertValidation = this.ServerCertValidation2;
			if (serverCertValidation != null)
			{
				return serverCertValidation(collection);
			}
			return null;
		}

		// Token: 0x060002F8 RID: 760 RVA: 0x00010F74 File Offset: 0x0000F174
		internal override bool OnRemoteCertificateValidation(global::System.Security.Cryptography.X509Certificates.X509Certificate certificate, int[] errors)
		{
			if (this.ServerCertValidation != null)
			{
				return this.ServerCertValidation(certificate, errors);
			}
			return errors != null && errors.Length == 0;
		}

		// Token: 0x060002F9 RID: 761 RVA: 0x00010F96 File Offset: 0x0000F196
		internal virtual bool RaiseServerCertificateValidation(global::System.Security.Cryptography.X509Certificates.X509Certificate certificate, int[] certificateErrors)
		{
			return base.RaiseRemoteCertificateValidation(certificate, certificateErrors);
		}

		// Token: 0x060002FA RID: 762 RVA: 0x00010FA0 File Offset: 0x0000F1A0
		internal virtual ValidationResult RaiseServerCertificateValidation2(Mono.Security.X509.X509CertificateCollection collection)
		{
			return base.RaiseRemoteCertificateValidation2(collection);
		}

		// Token: 0x060002FB RID: 763 RVA: 0x00010FA9 File Offset: 0x0000F1A9
		internal global::System.Security.Cryptography.X509Certificates.X509Certificate RaiseClientCertificateSelection(global::System.Security.Cryptography.X509Certificates.X509CertificateCollection clientCertificates, global::System.Security.Cryptography.X509Certificates.X509Certificate serverCertificate, string targetHost, global::System.Security.Cryptography.X509Certificates.X509CertificateCollection serverRequestedCertificates)
		{
			return base.RaiseLocalCertificateSelection(clientCertificates, serverCertificate, targetHost, serverRequestedCertificates);
		}

		// Token: 0x060002FC RID: 764 RVA: 0x00010FB6 File Offset: 0x0000F1B6
		internal override AsymmetricAlgorithm OnLocalPrivateKeySelection(global::System.Security.Cryptography.X509Certificates.X509Certificate certificate, string targetHost)
		{
			if (this.PrivateKeySelection != null)
			{
				return this.PrivateKeySelection(certificate, targetHost);
			}
			return null;
		}

		// Token: 0x060002FD RID: 765 RVA: 0x00010FCF File Offset: 0x0000F1CF
		internal AsymmetricAlgorithm RaisePrivateKeySelection(global::System.Security.Cryptography.X509Certificates.X509Certificate certificate, string targetHost)
		{
			return base.RaiseLocalPrivateKeySelection(certificate, targetHost);
		}

		// Token: 0x020000E0 RID: 224
		private enum NegotiateState
		{
			// Token: 0x040004FB RID: 1275
			SentClientHello,
			// Token: 0x040004FC RID: 1276
			ReceiveClientHelloResponse,
			// Token: 0x040004FD RID: 1277
			SentCipherSpec,
			// Token: 0x040004FE RID: 1278
			ReceiveCipherSpecResponse,
			// Token: 0x040004FF RID: 1279
			SentKeyExchange,
			// Token: 0x04000500 RID: 1280
			ReceiveFinishResponse,
			// Token: 0x04000501 RID: 1281
			SentFinished
		}

		// Token: 0x020000E1 RID: 225
		private class NegotiateAsyncResult : IAsyncResult
		{
			// Token: 0x06000796 RID: 1942 RVA: 0x00021FD5 File Offset: 0x000201D5
			public NegotiateAsyncResult(AsyncCallback userCallback, object userState, SslClientStream.NegotiateState state)
			{
				this._userCallback = userCallback;
				this._userState = userState;
				this._state = state;
			}

			// Token: 0x170001E8 RID: 488
			// (get) Token: 0x06000797 RID: 1943 RVA: 0x00021FFD File Offset: 0x000201FD
			// (set) Token: 0x06000798 RID: 1944 RVA: 0x00022005 File Offset: 0x00020205
			public SslClientStream.NegotiateState State
			{
				get
				{
					return this._state;
				}
				set
				{
					this._state = value;
				}
			}

			// Token: 0x170001E9 RID: 489
			// (get) Token: 0x06000799 RID: 1945 RVA: 0x0002200E File Offset: 0x0002020E
			public object AsyncState
			{
				get
				{
					return this._userState;
				}
			}

			// Token: 0x170001EA RID: 490
			// (get) Token: 0x0600079A RID: 1946 RVA: 0x00022016 File Offset: 0x00020216
			public Exception AsyncException
			{
				get
				{
					return this._asyncException;
				}
			}

			// Token: 0x170001EB RID: 491
			// (get) Token: 0x0600079B RID: 1947 RVA: 0x0002201E File Offset: 0x0002021E
			public bool CompletedWithError
			{
				get
				{
					return this.IsCompleted && this._asyncException != null;
				}
			}

			// Token: 0x170001EC RID: 492
			// (get) Token: 0x0600079C RID: 1948 RVA: 0x00022034 File Offset: 0x00020234
			public WaitHandle AsyncWaitHandle
			{
				get
				{
					object obj = this.locker;
					lock (obj)
					{
						if (this.handle == null)
						{
							this.handle = new ManualResetEvent(this.completed);
						}
					}
					return this.handle;
				}
			}

			// Token: 0x170001ED RID: 493
			// (get) Token: 0x0600079D RID: 1949 RVA: 0x00022090 File Offset: 0x00020290
			public bool CompletedSynchronously
			{
				get
				{
					return false;
				}
			}

			// Token: 0x170001EE RID: 494
			// (get) Token: 0x0600079E RID: 1950 RVA: 0x00022094 File Offset: 0x00020294
			public bool IsCompleted
			{
				get
				{
					object obj = this.locker;
					bool flag2;
					lock (obj)
					{
						flag2 = this.completed;
					}
					return flag2;
				}
			}

			// Token: 0x0600079F RID: 1951 RVA: 0x000220D8 File Offset: 0x000202D8
			public void SetComplete(Exception ex)
			{
				object obj = this.locker;
				lock (obj)
				{
					if (!this.completed)
					{
						this.completed = true;
						if (this.handle != null)
						{
							this.handle.Set();
						}
						if (this._userCallback != null)
						{
							this._userCallback.BeginInvoke(this, null, null);
						}
						this._asyncException = ex;
					}
				}
			}

			// Token: 0x060007A0 RID: 1952 RVA: 0x00022158 File Offset: 0x00020358
			public void SetComplete()
			{
				this.SetComplete(null);
			}

			// Token: 0x04000502 RID: 1282
			private object locker = new object();

			// Token: 0x04000503 RID: 1283
			private AsyncCallback _userCallback;

			// Token: 0x04000504 RID: 1284
			private object _userState;

			// Token: 0x04000505 RID: 1285
			private Exception _asyncException;

			// Token: 0x04000506 RID: 1286
			private ManualResetEvent handle;

			// Token: 0x04000507 RID: 1287
			private SslClientStream.NegotiateState _state;

			// Token: 0x04000508 RID: 1288
			private bool completed;
		}
	}
}
