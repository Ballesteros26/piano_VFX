using System;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Mono.Security.Interface;
using Mono.Security.Protocol.Tls;
using Mono.Security.X509;

namespace Mono.Net.Security.Private
{
	// Token: 0x02000089 RID: 137
	[MonoTODO("Non-X509Certificate2 certificate is not supported")]
	internal class LegacySslStream : AuthenticatedStream, IMonoSslStream, IDisposable
	{
		// Token: 0x060002E9 RID: 745 RVA: 0x000096F5 File Offset: 0x000078F5
		public LegacySslStream(Stream innerStream, bool leaveInnerStreamOpen, SslStream owner, MonoTlsProvider provider, MonoTlsSettings settings)
			: base(innerStream, leaveInnerStreamOpen)
		{
			this.SslStream = owner;
			this.Provider = provider;
			this.certificateValidator = ChainValidationHelper.GetInternalValidator(provider, settings);
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x060002EA RID: 746 RVA: 0x0000971D File Offset: 0x0000791D
		public override bool CanRead
		{
			get
			{
				return base.InnerStream.CanRead;
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x060002EB RID: 747 RVA: 0x0000972A File Offset: 0x0000792A
		public override bool CanSeek
		{
			get
			{
				return base.InnerStream.CanSeek;
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x060002EC RID: 748 RVA: 0x00007510 File Offset: 0x00005710
		public override bool CanTimeout
		{
			get
			{
				return base.InnerStream.CanTimeout;
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x060002ED RID: 749 RVA: 0x00009737 File Offset: 0x00007937
		public override bool CanWrite
		{
			get
			{
				return base.InnerStream.CanWrite;
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x060002EE RID: 750 RVA: 0x0000753E File Offset: 0x0000573E
		public override long Length
		{
			get
			{
				return base.InnerStream.Length;
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x060002EF RID: 751 RVA: 0x0000754B File Offset: 0x0000574B
		// (set) Token: 0x060002F0 RID: 752 RVA: 0x00009744 File Offset: 0x00007944
		public override long Position
		{
			get
			{
				return base.InnerStream.Position;
			}
			set
			{
				throw new NotSupportedException("This stream does not support seek operations");
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x060002F1 RID: 753 RVA: 0x00009750 File Offset: 0x00007950
		public override bool IsAuthenticated
		{
			get
			{
				return this.ssl_stream != null;
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x060002F2 RID: 754 RVA: 0x00007558 File Offset: 0x00005758
		public override bool IsEncrypted
		{
			get
			{
				return this.IsAuthenticated;
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x060002F3 RID: 755 RVA: 0x0000975B File Offset: 0x0000795B
		public override bool IsMutuallyAuthenticated
		{
			get
			{
				if (!this.IsAuthenticated)
				{
					return false;
				}
				if (!this.IsServer)
				{
					return this.LocalCertificate != null;
				}
				return this.RemoteCertificate != null;
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x060002F4 RID: 756 RVA: 0x00009782 File Offset: 0x00007982
		public override bool IsServer
		{
			get
			{
				return this.ssl_stream is SslServerStream;
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x060002F5 RID: 757 RVA: 0x00007558 File Offset: 0x00005758
		public override bool IsSigned
		{
			get
			{
				return this.IsAuthenticated;
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x060002F6 RID: 758 RVA: 0x00007560 File Offset: 0x00005760
		// (set) Token: 0x060002F7 RID: 759 RVA: 0x0000756D File Offset: 0x0000576D
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

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x060002F8 RID: 760 RVA: 0x0000757B File Offset: 0x0000577B
		// (set) Token: 0x060002F9 RID: 761 RVA: 0x00007588 File Offset: 0x00005788
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

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x060002FA RID: 762 RVA: 0x00009792 File Offset: 0x00007992
		public virtual bool CheckCertRevocationStatus
		{
			get
			{
				return this.IsAuthenticated && this.ssl_stream.CheckCertRevocationStatus;
			}
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x060002FB RID: 763 RVA: 0x000097AC File Offset: 0x000079AC
		public virtual global::System.Security.Authentication.CipherAlgorithmType CipherAlgorithm
		{
			get
			{
				this.CheckConnectionAuthenticated();
				switch (this.ssl_stream.CipherAlgorithm)
				{
				case Mono.Security.Protocol.Tls.CipherAlgorithmType.Des:
					return global::System.Security.Authentication.CipherAlgorithmType.Des;
				case Mono.Security.Protocol.Tls.CipherAlgorithmType.None:
					return global::System.Security.Authentication.CipherAlgorithmType.None;
				case Mono.Security.Protocol.Tls.CipherAlgorithmType.Rc2:
					return global::System.Security.Authentication.CipherAlgorithmType.Rc2;
				case Mono.Security.Protocol.Tls.CipherAlgorithmType.Rc4:
					return global::System.Security.Authentication.CipherAlgorithmType.Rc4;
				case Mono.Security.Protocol.Tls.CipherAlgorithmType.Rijndael:
				{
					int cipherStrength = this.ssl_stream.CipherStrength;
					if (cipherStrength == 128)
					{
						return global::System.Security.Authentication.CipherAlgorithmType.Aes128;
					}
					if (cipherStrength == 192)
					{
						return global::System.Security.Authentication.CipherAlgorithmType.Aes192;
					}
					if (cipherStrength == 256)
					{
						return global::System.Security.Authentication.CipherAlgorithmType.Aes256;
					}
					break;
				}
				case Mono.Security.Protocol.Tls.CipherAlgorithmType.TripleDes:
					return global::System.Security.Authentication.CipherAlgorithmType.TripleDes;
				}
				throw new InvalidOperationException("Not supported cipher algorithm is in use. It is likely a bug in SslStream.");
			}
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x060002FC RID: 764 RVA: 0x0000984B File Offset: 0x00007A4B
		public virtual int CipherStrength
		{
			get
			{
				this.CheckConnectionAuthenticated();
				return this.ssl_stream.CipherStrength;
			}
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x060002FD RID: 765 RVA: 0x00009860 File Offset: 0x00007A60
		public virtual global::System.Security.Authentication.HashAlgorithmType HashAlgorithm
		{
			get
			{
				this.CheckConnectionAuthenticated();
				switch (this.ssl_stream.HashAlgorithm)
				{
				case Mono.Security.Protocol.Tls.HashAlgorithmType.Md5:
					return global::System.Security.Authentication.HashAlgorithmType.Md5;
				case Mono.Security.Protocol.Tls.HashAlgorithmType.None:
					return global::System.Security.Authentication.HashAlgorithmType.None;
				case Mono.Security.Protocol.Tls.HashAlgorithmType.Sha1:
					return global::System.Security.Authentication.HashAlgorithmType.Sha1;
				default:
					throw new InvalidOperationException("Not supported hash algorithm is in use. It is likely a bug in SslStream.");
				}
			}
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x060002FE RID: 766 RVA: 0x000098AB File Offset: 0x00007AAB
		public virtual int HashStrength
		{
			get
			{
				this.CheckConnectionAuthenticated();
				return this.ssl_stream.HashStrength;
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x060002FF RID: 767 RVA: 0x000098C0 File Offset: 0x00007AC0
		public virtual global::System.Security.Authentication.ExchangeAlgorithmType KeyExchangeAlgorithm
		{
			get
			{
				this.CheckConnectionAuthenticated();
				switch (this.ssl_stream.KeyExchangeAlgorithm)
				{
				case Mono.Security.Protocol.Tls.ExchangeAlgorithmType.DiffieHellman:
					return global::System.Security.Authentication.ExchangeAlgorithmType.DiffieHellman;
				case Mono.Security.Protocol.Tls.ExchangeAlgorithmType.None:
					return global::System.Security.Authentication.ExchangeAlgorithmType.None;
				case Mono.Security.Protocol.Tls.ExchangeAlgorithmType.RsaKeyX:
					return global::System.Security.Authentication.ExchangeAlgorithmType.RsaKeyX;
				case Mono.Security.Protocol.Tls.ExchangeAlgorithmType.RsaSign:
					return global::System.Security.Authentication.ExchangeAlgorithmType.RsaSign;
				}
				throw new InvalidOperationException("Not supported exchange algorithm is in use. It is likely a bug in SslStream.");
			}
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x06000300 RID: 768 RVA: 0x00009919 File Offset: 0x00007B19
		public virtual int KeyExchangeStrength
		{
			get
			{
				this.CheckConnectionAuthenticated();
				return this.ssl_stream.KeyExchangeStrength;
			}
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x06000301 RID: 769 RVA: 0x0000992C File Offset: 0x00007B2C
		global::System.Security.Cryptography.X509Certificates.X509Certificate IMonoSslStream.InternalLocalCertificate
		{
			get
			{
				if (!this.IsServer)
				{
					return ((SslClientStream)this.ssl_stream).SelectedClientCertificate;
				}
				return this.ssl_stream.ServerCertificate;
			}
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x06000302 RID: 770 RVA: 0x00009952 File Offset: 0x00007B52
		public virtual global::System.Security.Cryptography.X509Certificates.X509Certificate LocalCertificate
		{
			get
			{
				this.CheckConnectionAuthenticated();
				if (!this.IsServer)
				{
					return ((SslClientStream)this.ssl_stream).SelectedClientCertificate;
				}
				return this.ssl_stream.ServerCertificate;
			}
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x06000303 RID: 771 RVA: 0x0000997E File Offset: 0x00007B7E
		public virtual global::System.Security.Cryptography.X509Certificates.X509Certificate RemoteCertificate
		{
			get
			{
				this.CheckConnectionAuthenticated();
				if (this.IsServer)
				{
					return ((SslServerStream)this.ssl_stream).ClientCertificate;
				}
				return this.ssl_stream.ServerCertificate;
			}
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x06000304 RID: 772 RVA: 0x000099AC File Offset: 0x00007BAC
		public virtual SslProtocols SslProtocol
		{
			get
			{
				this.CheckConnectionAuthenticated();
				Mono.Security.Protocol.Tls.SecurityProtocolType securityProtocol = this.ssl_stream.SecurityProtocol;
				if (securityProtocol <= Mono.Security.Protocol.Tls.SecurityProtocolType.Ssl2)
				{
					if (securityProtocol == Mono.Security.Protocol.Tls.SecurityProtocolType.Default)
					{
						return SslProtocols.Default;
					}
					if (securityProtocol == Mono.Security.Protocol.Tls.SecurityProtocolType.Ssl2)
					{
						return SslProtocols.Ssl2;
					}
				}
				else
				{
					if (securityProtocol == Mono.Security.Protocol.Tls.SecurityProtocolType.Ssl3)
					{
						return SslProtocols.Ssl3;
					}
					if (securityProtocol == Mono.Security.Protocol.Tls.SecurityProtocolType.Tls)
					{
						return SslProtocols.Tls;
					}
				}
				throw new InvalidOperationException("Not supported SSL/TLS protocol is in use. It is likely a bug in SslStream.");
			}
		}

		// Token: 0x06000305 RID: 773 RVA: 0x00009A0C File Offset: 0x00007C0C
		private global::System.Security.Cryptography.X509Certificates.X509Certificate OnCertificateSelection(global::System.Security.Cryptography.X509Certificates.X509CertificateCollection clientCerts, global::System.Security.Cryptography.X509Certificates.X509Certificate serverCert, string targetHost, global::System.Security.Cryptography.X509Certificates.X509CertificateCollection serverRequestedCerts)
		{
			string[] array = new string[(serverRequestedCerts != null) ? serverRequestedCerts.Count : 0];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = serverRequestedCerts[i].GetIssuerName();
			}
			global::System.Security.Cryptography.X509Certificates.X509Certificate x509Certificate;
			this.certificateValidator.SelectClientCertificate(targetHost, clientCerts, serverCert, array, out x509Certificate);
			return x509Certificate;
		}

		// Token: 0x06000306 RID: 774 RVA: 0x00009A5E File Offset: 0x00007C5E
		public virtual IAsyncResult BeginAuthenticateAsClient(string targetHost, AsyncCallback asyncCallback, object asyncState)
		{
			return this.BeginAuthenticateAsClient(targetHost, new global::System.Security.Cryptography.X509Certificates.X509CertificateCollection(), SslProtocols.Tls, false, asyncCallback, asyncState);
		}

		// Token: 0x06000307 RID: 775 RVA: 0x00009A74 File Offset: 0x00007C74
		public virtual IAsyncResult BeginAuthenticateAsClient(string targetHost, global::System.Security.Cryptography.X509Certificates.X509CertificateCollection clientCertificates, SslProtocols enabledSslProtocols, bool checkCertificateRevocation, AsyncCallback asyncCallback, object asyncState)
		{
			if (this.IsAuthenticated)
			{
				throw new InvalidOperationException("This SslStream is already authenticated");
			}
			SslClientStream sslClientStream = new SslClientStream(base.InnerStream, targetHost, !base.LeaveInnerStreamOpen, this.GetMonoSslProtocol(enabledSslProtocols), clientCertificates);
			sslClientStream.CheckCertRevocationStatus = checkCertificateRevocation;
			sslClientStream.PrivateKeyCertSelectionDelegate = delegate(global::System.Security.Cryptography.X509Certificates.X509Certificate cert, string host)
			{
				string certHashString = cert.GetCertHashString();
				foreach (global::System.Security.Cryptography.X509Certificates.X509Certificate x509Certificate in clientCertificates)
				{
					if (!(x509Certificate.GetCertHashString() != certHashString))
					{
						return ((x509Certificate as X509Certificate2) ?? new X509Certificate2(x509Certificate)).PrivateKey;
					}
				}
				return null;
			};
			sslClientStream.ServerCertValidation2 += delegate(Mono.Security.X509.X509CertificateCollection mcerts)
			{
				global::System.Security.Cryptography.X509Certificates.X509CertificateCollection x509CertificateCollection = null;
				if (mcerts != null)
				{
					x509CertificateCollection = new global::System.Security.Cryptography.X509Certificates.X509CertificateCollection();
					for (int i = 0; i < mcerts.Count; i++)
					{
						x509CertificateCollection.Add(new X509Certificate2(mcerts[i].RawData));
					}
				}
				return ((ChainValidationHelper)this.certificateValidator).ValidateCertificate(targetHost, false, x509CertificateCollection);
			};
			sslClientStream.ClientCertSelectionDelegate = new CertificateSelectionCallback(this.OnCertificateSelection);
			this.ssl_stream = sslClientStream;
			return this.BeginWrite(new byte[0], 0, 0, asyncCallback, asyncState);
		}

		// Token: 0x06000308 RID: 776 RVA: 0x00009B2E File Offset: 0x00007D2E
		public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback asyncCallback, object asyncState)
		{
			this.CheckConnectionAuthenticated();
			return this.ssl_stream.BeginRead(buffer, offset, count, asyncCallback, asyncState);
		}

		// Token: 0x06000309 RID: 777 RVA: 0x00009B48 File Offset: 0x00007D48
		public virtual IAsyncResult BeginAuthenticateAsServer(global::System.Security.Cryptography.X509Certificates.X509Certificate serverCertificate, AsyncCallback asyncCallback, object asyncState)
		{
			return this.BeginAuthenticateAsServer(serverCertificate, false, SslProtocols.Tls, false, asyncCallback, asyncState);
		}

		// Token: 0x0600030A RID: 778 RVA: 0x00009B5C File Offset: 0x00007D5C
		public virtual IAsyncResult BeginAuthenticateAsServer(global::System.Security.Cryptography.X509Certificates.X509Certificate serverCertificate, bool clientCertificateRequired, SslProtocols enabledSslProtocols, bool checkCertificateRevocation, AsyncCallback asyncCallback, object asyncState)
		{
			if (this.IsAuthenticated)
			{
				throw new InvalidOperationException("This SslStream is already authenticated");
			}
			this.ssl_stream = new SslServerStream(base.InnerStream, serverCertificate, false, clientCertificateRequired, !base.LeaveInnerStreamOpen, this.GetMonoSslProtocol(enabledSslProtocols))
			{
				CheckCertRevocationStatus = checkCertificateRevocation,
				PrivateKeyCertSelectionDelegate = delegate(global::System.Security.Cryptography.X509Certificates.X509Certificate cert, string targetHost)
				{
					X509Certificate2 x509Certificate = (serverCertificate as X509Certificate2) ?? new X509Certificate2(serverCertificate);
					if (x509Certificate == null)
					{
						return null;
					}
					return x509Certificate.PrivateKey;
				},
				ClientCertValidationDelegate = delegate(global::System.Security.Cryptography.X509Certificates.X509Certificate cert, int[] certErrors)
				{
					MonoSslPolicyErrors monoSslPolicyErrors = ((certErrors.Length != 0) ? MonoSslPolicyErrors.RemoteCertificateChainErrors : MonoSslPolicyErrors.None);
					return ((ChainValidationHelper)this.certificateValidator).ValidateClientCertificate(cert, monoSslPolicyErrors);
				}
			};
			return this.BeginWrite(new byte[0], 0, 0, asyncCallback, asyncState);
		}

		// Token: 0x0600030B RID: 779 RVA: 0x00009BF9 File Offset: 0x00007DF9
		private Mono.Security.Protocol.Tls.SecurityProtocolType GetMonoSslProtocol(SslProtocols ms)
		{
			if (ms == SslProtocols.Ssl2)
			{
				return Mono.Security.Protocol.Tls.SecurityProtocolType.Ssl2;
			}
			if (ms == SslProtocols.Ssl3)
			{
				return Mono.Security.Protocol.Tls.SecurityProtocolType.Ssl3;
			}
			if (ms != SslProtocols.Tls)
			{
				return Mono.Security.Protocol.Tls.SecurityProtocolType.Default;
			}
			return Mono.Security.Protocol.Tls.SecurityProtocolType.Tls;
		}

		// Token: 0x0600030C RID: 780 RVA: 0x00009C20 File Offset: 0x00007E20
		public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback asyncCallback, object asyncState)
		{
			this.CheckConnectionAuthenticated();
			return this.ssl_stream.BeginWrite(buffer, offset, count, asyncCallback, asyncState);
		}

		// Token: 0x0600030D RID: 781 RVA: 0x00009C3A File Offset: 0x00007E3A
		public virtual void AuthenticateAsClient(string targetHost)
		{
			this.AuthenticateAsClient(targetHost, new global::System.Security.Cryptography.X509Certificates.X509CertificateCollection(), SslProtocols.Tls, false);
		}

		// Token: 0x0600030E RID: 782 RVA: 0x00009C4E File Offset: 0x00007E4E
		public virtual void AuthenticateAsClient(string targetHost, global::System.Security.Cryptography.X509Certificates.X509CertificateCollection clientCertificates, SslProtocols enabledSslProtocols, bool checkCertificateRevocation)
		{
			this.EndAuthenticateAsClient(this.BeginAuthenticateAsClient(targetHost, clientCertificates, enabledSslProtocols, checkCertificateRevocation, null, null));
		}

		// Token: 0x0600030F RID: 783 RVA: 0x00009C63 File Offset: 0x00007E63
		public virtual void AuthenticateAsServer(global::System.Security.Cryptography.X509Certificates.X509Certificate serverCertificate)
		{
			this.AuthenticateAsServer(serverCertificate, false, SslProtocols.Tls, false);
		}

		// Token: 0x06000310 RID: 784 RVA: 0x00009C73 File Offset: 0x00007E73
		public virtual void AuthenticateAsServer(global::System.Security.Cryptography.X509Certificates.X509Certificate serverCertificate, bool clientCertificateRequired, SslProtocols enabledSslProtocols, bool checkCertificateRevocation)
		{
			this.EndAuthenticateAsServer(this.BeginAuthenticateAsServer(serverCertificate, clientCertificateRequired, enabledSslProtocols, checkCertificateRevocation, null, null));
		}

		// Token: 0x06000311 RID: 785 RVA: 0x00009C88 File Offset: 0x00007E88
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this.ssl_stream != null)
				{
					this.ssl_stream.Dispose();
				}
				this.ssl_stream = null;
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000312 RID: 786 RVA: 0x00009CAE File Offset: 0x00007EAE
		public virtual void EndAuthenticateAsClient(IAsyncResult asyncResult)
		{
			this.CheckConnectionAuthenticated();
			if (this.CanRead)
			{
				this.ssl_stream.EndRead(asyncResult);
				return;
			}
			this.ssl_stream.EndWrite(asyncResult);
		}

		// Token: 0x06000313 RID: 787 RVA: 0x00009CAE File Offset: 0x00007EAE
		public virtual void EndAuthenticateAsServer(IAsyncResult asyncResult)
		{
			this.CheckConnectionAuthenticated();
			if (this.CanRead)
			{
				this.ssl_stream.EndRead(asyncResult);
				return;
			}
			this.ssl_stream.EndWrite(asyncResult);
		}

		// Token: 0x06000314 RID: 788 RVA: 0x00009CD8 File Offset: 0x00007ED8
		public override int EndRead(IAsyncResult asyncResult)
		{
			this.CheckConnectionAuthenticated();
			return this.ssl_stream.EndRead(asyncResult);
		}

		// Token: 0x06000315 RID: 789 RVA: 0x00009CEC File Offset: 0x00007EEC
		public override void EndWrite(IAsyncResult asyncResult)
		{
			this.CheckConnectionAuthenticated();
			this.ssl_stream.EndWrite(asyncResult);
		}

		// Token: 0x06000316 RID: 790 RVA: 0x00009D00 File Offset: 0x00007F00
		public override void Flush()
		{
			this.CheckConnectionAuthenticated();
			base.InnerStream.Flush();
		}

		// Token: 0x06000317 RID: 791 RVA: 0x00009D13 File Offset: 0x00007F13
		public override int Read(byte[] buffer, int offset, int count)
		{
			return this.EndRead(this.BeginRead(buffer, offset, count, null, null));
		}

		// Token: 0x06000318 RID: 792 RVA: 0x00009744 File Offset: 0x00007944
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException("This stream does not support seek operations");
		}

		// Token: 0x06000319 RID: 793 RVA: 0x000074EB File Offset: 0x000056EB
		public override void SetLength(long value)
		{
			base.InnerStream.SetLength(value);
		}

		// Token: 0x0600031A RID: 794 RVA: 0x00009D26 File Offset: 0x00007F26
		public override void Write(byte[] buffer, int offset, int count)
		{
			this.EndWrite(this.BeginWrite(buffer, offset, count, null, null));
		}

		// Token: 0x0600031B RID: 795 RVA: 0x00006C8A File Offset: 0x00004E8A
		public void Write(byte[] buffer)
		{
			this.Write(buffer, 0, buffer.Length);
		}

		// Token: 0x0600031C RID: 796 RVA: 0x00009D39 File Offset: 0x00007F39
		private void CheckConnectionAuthenticated()
		{
			if (!this.IsAuthenticated)
			{
				throw new InvalidOperationException("This operation is invalid until it is successfully authenticated");
			}
		}

		// Token: 0x0600031D RID: 797 RVA: 0x00009D4E File Offset: 0x00007F4E
		public virtual Task AuthenticateAsClientAsync(string targetHost)
		{
			return Task.Factory.FromAsync<string>(new Func<string, AsyncCallback, object, IAsyncResult>(this.BeginAuthenticateAsClient), new Action<IAsyncResult>(this.EndAuthenticateAsClient), targetHost, null);
		}

		// Token: 0x0600031E RID: 798 RVA: 0x00009D78 File Offset: 0x00007F78
		public virtual Task AuthenticateAsClientAsync(string targetHost, global::System.Security.Cryptography.X509Certificates.X509CertificateCollection clientCertificates, SslProtocols enabledSslProtocols, bool checkCertificateRevocation)
		{
			Tuple<string, global::System.Security.Cryptography.X509Certificates.X509CertificateCollection, SslProtocols, bool, LegacySslStream> tuple = Tuple.Create<string, global::System.Security.Cryptography.X509Certificates.X509CertificateCollection, SslProtocols, bool, LegacySslStream>(targetHost, clientCertificates, enabledSslProtocols, checkCertificateRevocation, this);
			return Task.Factory.FromAsync(delegate(AsyncCallback callback, object state)
			{
				Tuple<string, global::System.Security.Cryptography.X509Certificates.X509CertificateCollection, SslProtocols, bool, LegacySslStream> tuple2 = (Tuple<string, global::System.Security.Cryptography.X509Certificates.X509CertificateCollection, SslProtocols, bool, LegacySslStream>)state;
				return tuple2.Item5.BeginAuthenticateAsClient(tuple2.Item1, tuple2.Item2, tuple2.Item3, tuple2.Item4, callback, null);
			}, new Action<IAsyncResult>(this.EndAuthenticateAsClient), tuple);
		}

		// Token: 0x0600031F RID: 799 RVA: 0x00009DC8 File Offset: 0x00007FC8
		public virtual Task AuthenticateAsServerAsync(global::System.Security.Cryptography.X509Certificates.X509Certificate serverCertificate)
		{
			return Task.Factory.FromAsync<global::System.Security.Cryptography.X509Certificates.X509Certificate>(new Func<global::System.Security.Cryptography.X509Certificates.X509Certificate, AsyncCallback, object, IAsyncResult>(this.BeginAuthenticateAsServer), new Action<IAsyncResult>(this.EndAuthenticateAsServer), serverCertificate, null);
		}

		// Token: 0x06000320 RID: 800 RVA: 0x00009DF0 File Offset: 0x00007FF0
		public virtual Task AuthenticateAsServerAsync(global::System.Security.Cryptography.X509Certificates.X509Certificate serverCertificate, bool clientCertificateRequired, SslProtocols enabledSslProtocols, bool checkCertificateRevocation)
		{
			Tuple<global::System.Security.Cryptography.X509Certificates.X509Certificate, bool, SslProtocols, bool, LegacySslStream> tuple = Tuple.Create<global::System.Security.Cryptography.X509Certificates.X509Certificate, bool, SslProtocols, bool, LegacySslStream>(serverCertificate, clientCertificateRequired, enabledSslProtocols, checkCertificateRevocation, this);
			return Task.Factory.FromAsync(delegate(AsyncCallback callback, object state)
			{
				Tuple<global::System.Security.Cryptography.X509Certificates.X509Certificate, bool, SslProtocols, bool, LegacySslStream> tuple2 = (Tuple<global::System.Security.Cryptography.X509Certificates.X509Certificate, bool, SslProtocols, bool, LegacySslStream>)state;
				return tuple2.Item5.BeginAuthenticateAsServer(tuple2.Item1, tuple2.Item2, tuple2.Item3, tuple2.Item4, callback, null);
			}, new Action<IAsyncResult>(this.EndAuthenticateAsServer), tuple);
		}

		// Token: 0x06000321 RID: 801 RVA: 0x00009E40 File Offset: 0x00008040
		Task IMonoSslStream.ShutdownAsync()
		{
			return Task.CompletedTask;
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x06000322 RID: 802 RVA: 0x00002068 File Offset: 0x00000268
		AuthenticatedStream IMonoSslStream.AuthenticatedStream
		{
			get
			{
				return this;
			}
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x06000323 RID: 803 RVA: 0x000074E4 File Offset: 0x000056E4
		TransportContext IMonoSslStream.TransportContext
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x06000324 RID: 804 RVA: 0x00009E47 File Offset: 0x00008047
		public SslStream SslStream { get; }

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x06000325 RID: 805 RVA: 0x00009E4F File Offset: 0x0000804F
		public MonoTlsProvider Provider { get; }

		// Token: 0x06000326 RID: 806 RVA: 0x00009E57 File Offset: 0x00008057
		public MonoTlsConnectionInfo GetConnectionInfo()
		{
			return null;
		}

		// Token: 0x04000814 RID: 2068
		private SslStreamBase ssl_stream;

		// Token: 0x04000815 RID: 2069
		private ICertificateValidator certificateValidator;
	}
}
