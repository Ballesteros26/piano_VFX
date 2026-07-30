using System;
using System.IO;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using Mono.Security.Interface;
using Mono.Security.X509;

namespace Mono.Security.Protocol.Tls
{
	// Token: 0x0200004D RID: 77
	public abstract class SslStreamBase : Stream, IDisposable
	{
		// Token: 0x06000320 RID: 800 RVA: 0x00011764 File Offset: 0x0000F964
		protected SslStreamBase(Stream stream, bool ownsStream)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream is null.");
			}
			if (!stream.CanRead || !stream.CanWrite)
			{
				throw new ArgumentNullException("stream is not both readable and writable.");
			}
			this.inputBuffer = new MemoryStream();
			this.innerStream = stream;
			this.ownsStream = ownsStream;
			this.negotiate = new object();
			this.read = new object();
			this.write = new object();
			this.negotiationComplete = new ManualResetEvent(false);
		}

		// Token: 0x06000321 RID: 801 RVA: 0x00011804 File Offset: 0x0000FA04
		private void AsyncHandshakeCallback(IAsyncResult asyncResult)
		{
			SslStreamBase.InternalAsyncResult internalAsyncResult = asyncResult.AsyncState as SslStreamBase.InternalAsyncResult;
			try
			{
				try
				{
					this.EndNegotiateHandshake(asyncResult);
				}
				catch (Exception ex)
				{
					this.protocol.SendAlert(ref ex);
					throw new IOException("The authentication or decryption has failed.", ex);
				}
				if (internalAsyncResult.ProceedAfterHandshake)
				{
					if (internalAsyncResult.FromWrite)
					{
						this.InternalBeginWrite(internalAsyncResult);
					}
					else
					{
						this.InternalBeginRead(internalAsyncResult);
					}
					this.negotiationComplete.Set();
				}
				else
				{
					this.negotiationComplete.Set();
					internalAsyncResult.SetComplete();
				}
			}
			catch (Exception ex2)
			{
				this.negotiationComplete.Set();
				internalAsyncResult.SetComplete(ex2);
			}
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x06000322 RID: 802 RVA: 0x000118B4 File Offset: 0x0000FAB4
		internal bool MightNeedHandshake
		{
			get
			{
				if (this.context.HandshakeState == HandshakeState.Finished)
				{
					return false;
				}
				object obj = this.negotiate;
				bool flag2;
				lock (obj)
				{
					flag2 = this.context.HandshakeState != HandshakeState.Finished;
				}
				return flag2;
			}
		}

		// Token: 0x06000323 RID: 803 RVA: 0x00011914 File Offset: 0x0000FB14
		internal void NegotiateHandshake()
		{
			if (this.MightNeedHandshake)
			{
				SslStreamBase.InternalAsyncResult internalAsyncResult = new SslStreamBase.InternalAsyncResult(null, null, null, 0, 0, false, false);
				if (!this.BeginNegotiateHandshake(internalAsyncResult))
				{
					this.negotiationComplete.WaitOne();
					return;
				}
				this.EndNegotiateHandshake(internalAsyncResult);
			}
		}

		// Token: 0x06000324 RID: 804
		internal abstract IAsyncResult BeginNegotiateHandshake(AsyncCallback callback, object state);

		// Token: 0x06000325 RID: 805
		internal abstract void EndNegotiateHandshake(IAsyncResult result);

		// Token: 0x06000326 RID: 806
		internal abstract global::System.Security.Cryptography.X509Certificates.X509Certificate OnLocalCertificateSelection(global::System.Security.Cryptography.X509Certificates.X509CertificateCollection clientCertificates, global::System.Security.Cryptography.X509Certificates.X509Certificate serverCertificate, string targetHost, global::System.Security.Cryptography.X509Certificates.X509CertificateCollection serverRequestedCertificates);

		// Token: 0x06000327 RID: 807
		internal abstract bool OnRemoteCertificateValidation(global::System.Security.Cryptography.X509Certificates.X509Certificate certificate, int[] errors);

		// Token: 0x06000328 RID: 808
		internal abstract ValidationResult OnRemoteCertificateValidation2(Mono.Security.X509.X509CertificateCollection collection);

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x06000329 RID: 809
		internal abstract bool HaveRemoteValidation2Callback { get; }

		// Token: 0x0600032A RID: 810
		internal abstract AsymmetricAlgorithm OnLocalPrivateKeySelection(global::System.Security.Cryptography.X509Certificates.X509Certificate certificate, string targetHost);

		// Token: 0x0600032B RID: 811 RVA: 0x00011953 File Offset: 0x0000FB53
		internal global::System.Security.Cryptography.X509Certificates.X509Certificate RaiseLocalCertificateSelection(global::System.Security.Cryptography.X509Certificates.X509CertificateCollection certificates, global::System.Security.Cryptography.X509Certificates.X509Certificate remoteCertificate, string targetHost, global::System.Security.Cryptography.X509Certificates.X509CertificateCollection requestedCertificates)
		{
			return this.OnLocalCertificateSelection(certificates, remoteCertificate, targetHost, requestedCertificates);
		}

		// Token: 0x0600032C RID: 812 RVA: 0x00011960 File Offset: 0x0000FB60
		internal bool RaiseRemoteCertificateValidation(global::System.Security.Cryptography.X509Certificates.X509Certificate certificate, int[] errors)
		{
			return this.OnRemoteCertificateValidation(certificate, errors);
		}

		// Token: 0x0600032D RID: 813 RVA: 0x0001196A File Offset: 0x0000FB6A
		internal ValidationResult RaiseRemoteCertificateValidation2(Mono.Security.X509.X509CertificateCollection collection)
		{
			return this.OnRemoteCertificateValidation2(collection);
		}

		// Token: 0x0600032E RID: 814 RVA: 0x00011973 File Offset: 0x0000FB73
		internal AsymmetricAlgorithm RaiseLocalPrivateKeySelection(global::System.Security.Cryptography.X509Certificates.X509Certificate certificate, string targetHost)
		{
			return this.OnLocalPrivateKeySelection(certificate, targetHost);
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x0600032F RID: 815 RVA: 0x0001197D File Offset: 0x0000FB7D
		// (set) Token: 0x06000330 RID: 816 RVA: 0x00011985 File Offset: 0x0000FB85
		public bool CheckCertRevocationStatus
		{
			get
			{
				return this.checkCertRevocationStatus;
			}
			set
			{
				this.checkCertRevocationStatus = value;
			}
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x06000331 RID: 817 RVA: 0x0001198E File Offset: 0x0000FB8E
		public CipherAlgorithmType CipherAlgorithm
		{
			get
			{
				if (this.context.HandshakeState == HandshakeState.Finished)
				{
					return this.context.Current.Cipher.CipherAlgorithmType;
				}
				return CipherAlgorithmType.None;
			}
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x06000332 RID: 818 RVA: 0x000119B5 File Offset: 0x0000FBB5
		public int CipherStrength
		{
			get
			{
				if (this.context.HandshakeState == HandshakeState.Finished)
				{
					return (int)this.context.Current.Cipher.EffectiveKeyBits;
				}
				return 0;
			}
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x06000333 RID: 819 RVA: 0x000119DC File Offset: 0x0000FBDC
		public HashAlgorithmType HashAlgorithm
		{
			get
			{
				if (this.context.HandshakeState == HandshakeState.Finished)
				{
					return this.context.Current.Cipher.HashAlgorithmType;
				}
				return HashAlgorithmType.None;
			}
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x06000334 RID: 820 RVA: 0x00011A03 File Offset: 0x0000FC03
		public int HashStrength
		{
			get
			{
				if (this.context.HandshakeState == HandshakeState.Finished)
				{
					return this.context.Current.Cipher.HashSize * 8;
				}
				return 0;
			}
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x06000335 RID: 821 RVA: 0x00011A2C File Offset: 0x0000FC2C
		public int KeyExchangeStrength
		{
			get
			{
				if (this.context.HandshakeState == HandshakeState.Finished)
				{
					return this.context.ServerSettings.Certificates[0].RSA.KeySize;
				}
				return 0;
			}
		}

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x06000336 RID: 822 RVA: 0x00011A5E File Offset: 0x0000FC5E
		public ExchangeAlgorithmType KeyExchangeAlgorithm
		{
			get
			{
				if (this.context.HandshakeState == HandshakeState.Finished)
				{
					return this.context.Current.Cipher.ExchangeAlgorithmType;
				}
				return ExchangeAlgorithmType.None;
			}
		}

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x06000337 RID: 823 RVA: 0x00011A85 File Offset: 0x0000FC85
		public SecurityProtocolType SecurityProtocol
		{
			get
			{
				if (this.context.HandshakeState == HandshakeState.Finished)
				{
					return this.context.SecurityProtocol;
				}
				return (SecurityProtocolType)0;
			}
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x06000338 RID: 824 RVA: 0x00011AA4 File Offset: 0x0000FCA4
		public global::System.Security.Cryptography.X509Certificates.X509Certificate ServerCertificate
		{
			get
			{
				if (this.context.HandshakeState == HandshakeState.Finished && this.context.ServerSettings.Certificates != null && this.context.ServerSettings.Certificates.Count > 0)
				{
					return new global::System.Security.Cryptography.X509Certificates.X509Certificate(this.context.ServerSettings.Certificates[0].RawData);
				}
				return null;
			}
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x06000339 RID: 825 RVA: 0x00011B0B File Offset: 0x0000FD0B
		internal Mono.Security.X509.X509CertificateCollection ServerCertificates
		{
			get
			{
				return this.context.ServerSettings.Certificates;
			}
		}

		// Token: 0x0600033A RID: 826 RVA: 0x00011B20 File Offset: 0x0000FD20
		private bool BeginNegotiateHandshake(SslStreamBase.InternalAsyncResult asyncResult)
		{
			bool flag2;
			try
			{
				object obj = this.negotiate;
				lock (obj)
				{
					if (this.context.HandshakeState == HandshakeState.None)
					{
						this.BeginNegotiateHandshake(new AsyncCallback(this.AsyncHandshakeCallback), asyncResult);
						flag2 = true;
					}
					else
					{
						flag2 = false;
					}
				}
			}
			catch (Exception ex)
			{
				this.negotiationComplete.Set();
				this.protocol.SendAlert(ref ex);
				throw new IOException("The authentication or decryption has failed.", ex);
			}
			return flag2;
		}

		// Token: 0x0600033B RID: 827 RVA: 0x00011BB4 File Offset: 0x0000FDB4
		private void EndNegotiateHandshake(SslStreamBase.InternalAsyncResult asyncResult)
		{
			if (!asyncResult.IsCompleted)
			{
				asyncResult.AsyncWaitHandle.WaitOne();
			}
			if (asyncResult.CompletedWithError)
			{
				throw asyncResult.AsyncException;
			}
		}

		// Token: 0x0600033C RID: 828 RVA: 0x00011BDC File Offset: 0x0000FDDC
		public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			this.checkDisposed();
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer is a null reference.");
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset is less than 0.");
			}
			if (offset > buffer.Length)
			{
				throw new ArgumentOutOfRangeException("offset is greater than the length of buffer.");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count is less than 0.");
			}
			if (count > buffer.Length - offset)
			{
				throw new ArgumentOutOfRangeException("count is less than the length of buffer minus the value of the offset parameter.");
			}
			SslStreamBase.InternalAsyncResult internalAsyncResult = new SslStreamBase.InternalAsyncResult(callback, state, buffer, offset, count, false, true);
			if (this.MightNeedHandshake)
			{
				if (!this.BeginNegotiateHandshake(internalAsyncResult))
				{
					this.negotiationComplete.WaitOne();
					this.InternalBeginRead(internalAsyncResult);
				}
			}
			else
			{
				this.InternalBeginRead(internalAsyncResult);
			}
			return internalAsyncResult;
		}

		// Token: 0x0600033D RID: 829 RVA: 0x00011C7C File Offset: 0x0000FE7C
		private void InternalBeginRead(SslStreamBase.InternalAsyncResult asyncResult)
		{
			try
			{
				int num = 0;
				object obj = this.read;
				lock (obj)
				{
					bool flag2 = this.inputBuffer.Position == this.inputBuffer.Length && this.inputBuffer.Length > 0L;
					bool flag3 = this.inputBuffer.Length > 0L && asyncResult.Count > 0;
					if (flag2)
					{
						this.resetBuffer();
					}
					else if (flag3)
					{
						num = this.inputBuffer.Read(asyncResult.Buffer, asyncResult.Offset, asyncResult.Count);
					}
				}
				if (0 < num)
				{
					asyncResult.SetComplete(num);
				}
				else if (this.recordStream.Position < this.recordStream.Length)
				{
					this.InternalReadCallback_inner(asyncResult, this.recbuf, new object[] { this.recbuf, asyncResult }, false, 0);
				}
				else if (!this.context.ReceivedConnectionEnd)
				{
					this.innerStream.BeginRead(this.recbuf, 0, this.recbuf.Length, new AsyncCallback(this.InternalReadCallback), new object[] { this.recbuf, asyncResult });
				}
				else
				{
					asyncResult.SetComplete(0);
				}
			}
			catch (Exception ex)
			{
				this.protocol.SendAlert(ref ex);
				throw new IOException("The authentication or decryption has failed.", ex);
			}
		}

		// Token: 0x0600033E RID: 830 RVA: 0x00011E08 File Offset: 0x00010008
		private void InternalReadCallback(IAsyncResult result)
		{
			object[] array = (object[])result.AsyncState;
			byte[] array2 = (byte[])array[0];
			SslStreamBase.InternalAsyncResult internalAsyncResult = (SslStreamBase.InternalAsyncResult)array[1];
			try
			{
				this.checkDisposed();
				int num = this.innerStream.EndRead(result);
				if (num > 0)
				{
					this.recordStream.Write(array2, 0, num);
					this.InternalReadCallback_inner(internalAsyncResult, array2, array, true, num);
				}
				else
				{
					internalAsyncResult.SetComplete(0);
				}
			}
			catch (Exception ex)
			{
				internalAsyncResult.SetComplete(ex);
			}
		}

		// Token: 0x0600033F RID: 831 RVA: 0x00011E8C File Offset: 0x0001008C
		private void InternalReadCallback_inner(SslStreamBase.InternalAsyncResult internalResult, byte[] recbuf, object[] state, bool didRead, int n)
		{
			if (this.disposed)
			{
				return;
			}
			try
			{
				bool flag = false;
				long num = this.recordStream.Position;
				this.recordStream.Position = 0L;
				byte[] array = null;
				if (this.recordStream.Length >= 5L)
				{
					array = this.protocol.ReceiveRecord(this.recordStream);
				}
				while (array != null)
				{
					long num2 = this.recordStream.Length - this.recordStream.Position;
					byte[] array2 = null;
					if (num2 > 0L)
					{
						array2 = new byte[num2];
						this.recordStream.Read(array2, 0, array2.Length);
					}
					object obj = this.read;
					lock (obj)
					{
						long position = this.inputBuffer.Position;
						if (array.Length != 0)
						{
							this.inputBuffer.Seek(0L, SeekOrigin.End);
							this.inputBuffer.Write(array, 0, array.Length);
							this.inputBuffer.Seek(position, SeekOrigin.Begin);
							flag = true;
						}
					}
					this.recordStream.SetLength(0L);
					array = null;
					if (num2 > 0L)
					{
						this.recordStream.Write(array2, 0, array2.Length);
						if (this.recordStream.Length >= 5L)
						{
							this.recordStream.Position = 0L;
							array = this.protocol.ReceiveRecord(this.recordStream);
							if (array == null)
							{
								num = this.recordStream.Length;
							}
						}
						else
						{
							num = num2;
						}
					}
					else
					{
						num = 0L;
					}
				}
				if (!flag && (!didRead || n > 0))
				{
					if (this.context.ReceivedConnectionEnd)
					{
						internalResult.SetComplete(0);
					}
					else
					{
						this.recordStream.Position = this.recordStream.Length;
						this.innerStream.BeginRead(recbuf, 0, recbuf.Length, new AsyncCallback(this.InternalReadCallback), state);
					}
				}
				else
				{
					this.recordStream.Position = num;
					int num3 = 0;
					object obj = this.read;
					lock (obj)
					{
						num3 = this.inputBuffer.Read(internalResult.Buffer, internalResult.Offset, internalResult.Count);
					}
					internalResult.SetComplete(num3);
				}
			}
			catch (Exception ex)
			{
				internalResult.SetComplete(ex);
			}
		}

		// Token: 0x06000340 RID: 832 RVA: 0x0001210C File Offset: 0x0001030C
		private void InternalBeginWrite(SslStreamBase.InternalAsyncResult asyncResult)
		{
			try
			{
				object obj = this.write;
				lock (obj)
				{
					byte[] array = this.protocol.EncodeRecord(ContentType.ApplicationData, asyncResult.Buffer, asyncResult.Offset, asyncResult.Count);
					this.innerStream.BeginWrite(array, 0, array.Length, new AsyncCallback(this.InternalWriteCallback), asyncResult);
				}
			}
			catch (Exception ex)
			{
				this.protocol.SendAlert(ref ex);
				this.Close();
				throw new IOException("The authentication or decryption has failed.", ex);
			}
		}

		// Token: 0x06000341 RID: 833 RVA: 0x000121B4 File Offset: 0x000103B4
		private void InternalWriteCallback(IAsyncResult ar)
		{
			SslStreamBase.InternalAsyncResult internalAsyncResult = (SslStreamBase.InternalAsyncResult)ar.AsyncState;
			try
			{
				this.checkDisposed();
				this.innerStream.EndWrite(ar);
				internalAsyncResult.SetComplete();
			}
			catch (Exception ex)
			{
				internalAsyncResult.SetComplete(ex);
			}
		}

		// Token: 0x06000342 RID: 834 RVA: 0x00012204 File Offset: 0x00010404
		public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			this.checkDisposed();
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer is a null reference.");
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset is less than 0.");
			}
			if (offset > buffer.Length)
			{
				throw new ArgumentOutOfRangeException("offset is greater than the length of buffer.");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count is less than 0.");
			}
			if (count > buffer.Length - offset)
			{
				throw new ArgumentOutOfRangeException("count is less than the length of buffer minus the value of the offset parameter.");
			}
			SslStreamBase.InternalAsyncResult internalAsyncResult = new SslStreamBase.InternalAsyncResult(callback, state, buffer, offset, count, true, true);
			if (this.MightNeedHandshake)
			{
				if (!this.BeginNegotiateHandshake(internalAsyncResult))
				{
					this.negotiationComplete.WaitOne();
					this.InternalBeginWrite(internalAsyncResult);
				}
			}
			else
			{
				this.InternalBeginWrite(internalAsyncResult);
			}
			return internalAsyncResult;
		}

		// Token: 0x06000343 RID: 835 RVA: 0x000122A4 File Offset: 0x000104A4
		public override int EndRead(IAsyncResult asyncResult)
		{
			this.checkDisposed();
			SslStreamBase.InternalAsyncResult internalAsyncResult = asyncResult as SslStreamBase.InternalAsyncResult;
			if (internalAsyncResult == null)
			{
				throw new ArgumentNullException("asyncResult is null or was not obtained by calling BeginRead.");
			}
			if (!asyncResult.IsCompleted && !asyncResult.AsyncWaitHandle.WaitOne())
			{
				throw new TlsException(AlertDescription.InternalError, "Couldn't complete EndRead");
			}
			if (internalAsyncResult.CompletedWithError)
			{
				throw internalAsyncResult.AsyncException;
			}
			return internalAsyncResult.BytesRead;
		}

		// Token: 0x06000344 RID: 836 RVA: 0x00012304 File Offset: 0x00010504
		public override void EndWrite(IAsyncResult asyncResult)
		{
			this.checkDisposed();
			SslStreamBase.InternalAsyncResult internalAsyncResult = asyncResult as SslStreamBase.InternalAsyncResult;
			if (internalAsyncResult == null)
			{
				throw new ArgumentNullException("asyncResult is null or was not obtained by calling BeginWrite.");
			}
			if (!asyncResult.IsCompleted && !internalAsyncResult.AsyncWaitHandle.WaitOne())
			{
				throw new TlsException(AlertDescription.InternalError, "Couldn't complete EndWrite");
			}
			if (internalAsyncResult.CompletedWithError)
			{
				throw internalAsyncResult.AsyncException;
			}
		}

		// Token: 0x06000345 RID: 837 RVA: 0x0001235D File Offset: 0x0001055D
		public override void Close()
		{
			base.Close();
		}

		// Token: 0x06000346 RID: 838 RVA: 0x00012365 File Offset: 0x00010565
		public override void Flush()
		{
			this.checkDisposed();
			this.innerStream.Flush();
		}

		// Token: 0x06000347 RID: 839 RVA: 0x00012378 File Offset: 0x00010578
		public int Read(byte[] buffer)
		{
			return this.Read(buffer, 0, buffer.Length);
		}

		// Token: 0x06000348 RID: 840 RVA: 0x00012388 File Offset: 0x00010588
		public override int Read(byte[] buffer, int offset, int count)
		{
			this.checkDisposed();
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset is less than 0.");
			}
			if (offset > buffer.Length)
			{
				throw new ArgumentOutOfRangeException("offset is greater than the length of buffer.");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count is less than 0.");
			}
			if (count > buffer.Length - offset)
			{
				throw new ArgumentOutOfRangeException("count is less than the length of buffer minus the value of the offset parameter.");
			}
			if (this.context.HandshakeState != HandshakeState.Finished)
			{
				this.NegotiateHandshake();
			}
			object obj = this.read;
			int num6;
			lock (obj)
			{
				try
				{
					SslStreamBase.record_processing.Reset();
					if (this.inputBuffer.Position > 0L)
					{
						if (this.inputBuffer.Position == this.inputBuffer.Length)
						{
							this.inputBuffer.SetLength(0L);
						}
						else
						{
							int num = this.inputBuffer.Read(buffer, offset, count);
							if (num > 0)
							{
								SslStreamBase.record_processing.Set();
								return num;
							}
						}
					}
					bool flag2 = false;
					for (;;)
					{
						if (this.recordStream.Position == 0L || flag2)
						{
							flag2 = false;
							byte[] array = new byte[16384];
							int num2 = 0;
							if (count == 1)
							{
								int num3 = this.innerStream.ReadByte();
								if (num3 >= 0)
								{
									array[0] = (byte)num3;
									num2 = 1;
								}
							}
							else
							{
								num2 = this.innerStream.Read(array, 0, array.Length);
							}
							if (num2 <= 0)
							{
								break;
							}
							if (this.recordStream.Length > 0L && this.recordStream.Position != this.recordStream.Length)
							{
								this.recordStream.Seek(0L, SeekOrigin.End);
							}
							this.recordStream.Write(array, 0, num2);
						}
						bool flag3 = false;
						this.recordStream.Position = 0L;
						byte[] array2 = null;
						if (this.recordStream.Length >= 5L)
						{
							array2 = this.protocol.ReceiveRecord(this.recordStream);
							flag2 = array2 == null;
						}
						while (array2 != null)
						{
							long num4 = this.recordStream.Length - this.recordStream.Position;
							byte[] array3 = null;
							if (num4 > 0L)
							{
								array3 = new byte[num4];
								this.recordStream.Read(array3, 0, array3.Length);
							}
							long position = this.inputBuffer.Position;
							if (array2.Length != 0)
							{
								this.inputBuffer.Seek(0L, SeekOrigin.End);
								this.inputBuffer.Write(array2, 0, array2.Length);
								this.inputBuffer.Seek(position, SeekOrigin.Begin);
								flag3 = true;
							}
							this.recordStream.SetLength(0L);
							array2 = null;
							if (num4 > 0L)
							{
								this.recordStream.Write(array3, 0, array3.Length);
								this.recordStream.Position = 0L;
							}
							if (flag3)
							{
								goto Block_24;
							}
						}
					}
					SslStreamBase.record_processing.Set();
					return 0;
					Block_24:
					int num5 = this.inputBuffer.Read(buffer, offset, count);
					SslStreamBase.record_processing.Set();
					num6 = num5;
				}
				catch (TlsException ex)
				{
					throw new IOException("The authentication or decryption has failed.", ex);
				}
				catch (Exception ex2)
				{
					throw new IOException("IO exception during read.", ex2);
				}
			}
			return num6;
		}

		// Token: 0x06000349 RID: 841 RVA: 0x000126D8 File Offset: 0x000108D8
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600034A RID: 842 RVA: 0x000126DF File Offset: 0x000108DF
		public override void SetLength(long value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600034B RID: 843 RVA: 0x000126E6 File Offset: 0x000108E6
		public void Write(byte[] buffer)
		{
			this.Write(buffer, 0, buffer.Length);
		}

		// Token: 0x0600034C RID: 844 RVA: 0x000126F4 File Offset: 0x000108F4
		public override void Write(byte[] buffer, int offset, int count)
		{
			this.checkDisposed();
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset is less than 0.");
			}
			if (offset > buffer.Length)
			{
				throw new ArgumentOutOfRangeException("offset is greater than the length of buffer.");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count is less than 0.");
			}
			if (count > buffer.Length - offset)
			{
				throw new ArgumentOutOfRangeException("count is less than the length of buffer minus the value of the offset parameter.");
			}
			if (this.context.HandshakeState != HandshakeState.Finished)
			{
				this.NegotiateHandshake();
			}
			object obj = this.write;
			lock (obj)
			{
				try
				{
					byte[] array = this.protocol.EncodeRecord(ContentType.ApplicationData, buffer, offset, count);
					this.innerStream.Write(array, 0, array.Length);
				}
				catch (Exception ex)
				{
					this.protocol.SendAlert(ref ex);
					this.Close();
					throw new IOException("The authentication or decryption has failed.", ex);
				}
			}
		}

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x0600034D RID: 845 RVA: 0x000127E8 File Offset: 0x000109E8
		public override bool CanRead
		{
			get
			{
				return this.innerStream.CanRead;
			}
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x0600034E RID: 846 RVA: 0x000127F5 File Offset: 0x000109F5
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x0600034F RID: 847 RVA: 0x000127F8 File Offset: 0x000109F8
		public override bool CanWrite
		{
			get
			{
				return this.innerStream.CanWrite;
			}
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x06000350 RID: 848 RVA: 0x00012805 File Offset: 0x00010A05
		public override long Length
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x06000351 RID: 849 RVA: 0x0001280C File Offset: 0x00010A0C
		// (set) Token: 0x06000352 RID: 850 RVA: 0x00012813 File Offset: 0x00010A13
		public override long Position
		{
			get
			{
				throw new NotSupportedException();
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x06000353 RID: 851 RVA: 0x0001281C File Offset: 0x00010A1C
		~SslStreamBase()
		{
			this.Dispose(false);
		}

		// Token: 0x06000354 RID: 852 RVA: 0x0001284C File Offset: 0x00010A4C
		protected override void Dispose(bool disposing)
		{
			if (!this.disposed)
			{
				if (disposing)
				{
					if (this.innerStream != null)
					{
						if (this.context.HandshakeState == HandshakeState.Finished && !this.context.SentConnectionEnd)
						{
							try
							{
								this.protocol.SendAlert(AlertDescription.CloseNotify);
							}
							catch
							{
							}
						}
						if (this.ownsStream)
						{
							this.innerStream.Close();
						}
					}
					this.ownsStream = false;
					this.innerStream = null;
				}
				this.disposed = true;
				base.Dispose(disposing);
			}
		}

		// Token: 0x06000355 RID: 853 RVA: 0x000128DC File Offset: 0x00010ADC
		private void resetBuffer()
		{
			this.inputBuffer.SetLength(0L);
			this.inputBuffer.Position = 0L;
		}

		// Token: 0x06000356 RID: 854 RVA: 0x000128F8 File Offset: 0x00010AF8
		internal void checkDisposed()
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException("The Stream is closed.");
			}
		}

		// Token: 0x040001A2 RID: 418
		private static ManualResetEvent record_processing = new ManualResetEvent(true);

		// Token: 0x040001A3 RID: 419
		internal Stream innerStream;

		// Token: 0x040001A4 RID: 420
		internal MemoryStream inputBuffer;

		// Token: 0x040001A5 RID: 421
		internal Context context;

		// Token: 0x040001A6 RID: 422
		internal RecordProtocol protocol;

		// Token: 0x040001A7 RID: 423
		internal bool ownsStream;

		// Token: 0x040001A8 RID: 424
		private volatile bool disposed;

		// Token: 0x040001A9 RID: 425
		private bool checkCertRevocationStatus;

		// Token: 0x040001AA RID: 426
		private object negotiate;

		// Token: 0x040001AB RID: 427
		private object read;

		// Token: 0x040001AC RID: 428
		private object write;

		// Token: 0x040001AD RID: 429
		private ManualResetEvent negotiationComplete;

		// Token: 0x040001AE RID: 430
		private byte[] recbuf = new byte[16384];

		// Token: 0x040001AF RID: 431
		private MemoryStream recordStream = new MemoryStream();

		// Token: 0x020000E2 RID: 226
		// (Invoke) Token: 0x060007A2 RID: 1954
		private delegate void AsyncHandshakeDelegate(SslStreamBase.InternalAsyncResult asyncResult, bool fromWrite);

		// Token: 0x020000E3 RID: 227
		private class InternalAsyncResult : IAsyncResult
		{
			// Token: 0x060007A5 RID: 1957 RVA: 0x00022164 File Offset: 0x00020364
			public InternalAsyncResult(AsyncCallback userCallback, object userState, byte[] buffer, int offset, int count, bool fromWrite, bool proceedAfterHandshake)
			{
				this._userCallback = userCallback;
				this._userState = userState;
				this._buffer = buffer;
				this._offset = offset;
				this._count = count;
				this._fromWrite = fromWrite;
				this._proceedAfterHandshake = proceedAfterHandshake;
			}

			// Token: 0x170001EF RID: 495
			// (get) Token: 0x060007A6 RID: 1958 RVA: 0x000221B7 File Offset: 0x000203B7
			public bool ProceedAfterHandshake
			{
				get
				{
					return this._proceedAfterHandshake;
				}
			}

			// Token: 0x170001F0 RID: 496
			// (get) Token: 0x060007A7 RID: 1959 RVA: 0x000221BF File Offset: 0x000203BF
			public bool FromWrite
			{
				get
				{
					return this._fromWrite;
				}
			}

			// Token: 0x170001F1 RID: 497
			// (get) Token: 0x060007A8 RID: 1960 RVA: 0x000221C7 File Offset: 0x000203C7
			public byte[] Buffer
			{
				get
				{
					return this._buffer;
				}
			}

			// Token: 0x170001F2 RID: 498
			// (get) Token: 0x060007A9 RID: 1961 RVA: 0x000221CF File Offset: 0x000203CF
			public int Offset
			{
				get
				{
					return this._offset;
				}
			}

			// Token: 0x170001F3 RID: 499
			// (get) Token: 0x060007AA RID: 1962 RVA: 0x000221D7 File Offset: 0x000203D7
			public int Count
			{
				get
				{
					return this._count;
				}
			}

			// Token: 0x170001F4 RID: 500
			// (get) Token: 0x060007AB RID: 1963 RVA: 0x000221DF File Offset: 0x000203DF
			public int BytesRead
			{
				get
				{
					return this._bytesRead;
				}
			}

			// Token: 0x170001F5 RID: 501
			// (get) Token: 0x060007AC RID: 1964 RVA: 0x000221E7 File Offset: 0x000203E7
			public object AsyncState
			{
				get
				{
					return this._userState;
				}
			}

			// Token: 0x170001F6 RID: 502
			// (get) Token: 0x060007AD RID: 1965 RVA: 0x000221EF File Offset: 0x000203EF
			public Exception AsyncException
			{
				get
				{
					return this._asyncException;
				}
			}

			// Token: 0x170001F7 RID: 503
			// (get) Token: 0x060007AE RID: 1966 RVA: 0x000221F7 File Offset: 0x000203F7
			public bool CompletedWithError
			{
				get
				{
					return this.IsCompleted && this._asyncException != null;
				}
			}

			// Token: 0x170001F8 RID: 504
			// (get) Token: 0x060007AF RID: 1967 RVA: 0x0002220C File Offset: 0x0002040C
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

			// Token: 0x170001F9 RID: 505
			// (get) Token: 0x060007B0 RID: 1968 RVA: 0x00022268 File Offset: 0x00020468
			public bool CompletedSynchronously
			{
				get
				{
					return false;
				}
			}

			// Token: 0x170001FA RID: 506
			// (get) Token: 0x060007B1 RID: 1969 RVA: 0x0002226C File Offset: 0x0002046C
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

			// Token: 0x060007B2 RID: 1970 RVA: 0x000222B0 File Offset: 0x000204B0
			private void SetComplete(Exception ex, int bytesRead)
			{
				object obj = this.locker;
				lock (obj)
				{
					if (this.completed)
					{
						return;
					}
					this.completed = true;
					this._asyncException = ex;
					this._bytesRead = bytesRead;
					if (this.handle != null)
					{
						this.handle.Set();
					}
				}
				if (this._userCallback != null)
				{
					this._userCallback.BeginInvoke(this, null, null);
				}
			}

			// Token: 0x060007B3 RID: 1971 RVA: 0x00022334 File Offset: 0x00020534
			public void SetComplete(Exception ex)
			{
				this.SetComplete(ex, 0);
			}

			// Token: 0x060007B4 RID: 1972 RVA: 0x0002233E File Offset: 0x0002053E
			public void SetComplete(int bytesRead)
			{
				this.SetComplete(null, bytesRead);
			}

			// Token: 0x060007B5 RID: 1973 RVA: 0x00022348 File Offset: 0x00020548
			public void SetComplete()
			{
				this.SetComplete(null, 0);
			}

			// Token: 0x04000509 RID: 1289
			private object locker = new object();

			// Token: 0x0400050A RID: 1290
			private AsyncCallback _userCallback;

			// Token: 0x0400050B RID: 1291
			private object _userState;

			// Token: 0x0400050C RID: 1292
			private Exception _asyncException;

			// Token: 0x0400050D RID: 1293
			private ManualResetEvent handle;

			// Token: 0x0400050E RID: 1294
			private bool completed;

			// Token: 0x0400050F RID: 1295
			private int _bytesRead;

			// Token: 0x04000510 RID: 1296
			private bool _fromWrite;

			// Token: 0x04000511 RID: 1297
			private bool _proceedAfterHandshake;

			// Token: 0x04000512 RID: 1298
			private byte[] _buffer;

			// Token: 0x04000513 RID: 1299
			private int _offset;

			// Token: 0x04000514 RID: 1300
			private int _count;
		}
	}
}
