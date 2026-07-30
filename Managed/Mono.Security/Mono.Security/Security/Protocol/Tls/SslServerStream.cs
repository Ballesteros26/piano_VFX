using System;
using System.IO;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using Mono.Security.Interface;
using Mono.Security.Protocol.Tls.Handshake;
using Mono.Security.X509;

namespace Mono.Security.Protocol.Tls
{
	// Token: 0x0200004C RID: 76
	public class SslServerStream : SslStreamBase
	{
		// Token: 0x14000005 RID: 5
		// (add) Token: 0x06000305 RID: 773 RVA: 0x0001131C File Offset: 0x0000F51C
		// (remove) Token: 0x06000306 RID: 774 RVA: 0x00011354 File Offset: 0x0000F554
		internal event CertificateValidationCallback ClientCertValidation;

		// Token: 0x14000006 RID: 6
		// (add) Token: 0x06000307 RID: 775 RVA: 0x0001138C File Offset: 0x0000F58C
		// (remove) Token: 0x06000308 RID: 776 RVA: 0x000113C4 File Offset: 0x0000F5C4
		internal event PrivateKeySelectionCallback PrivateKeySelection;

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x06000309 RID: 777 RVA: 0x000113F9 File Offset: 0x0000F5F9
		public global::System.Security.Cryptography.X509Certificates.X509Certificate ClientCertificate
		{
			get
			{
				if (this.context.HandshakeState == HandshakeState.Finished)
				{
					return this.context.ClientSettings.ClientCertificate;
				}
				return null;
			}
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x0600030A RID: 778 RVA: 0x0001141B File Offset: 0x0000F61B
		// (set) Token: 0x0600030B RID: 779 RVA: 0x00011423 File Offset: 0x0000F623
		public CertificateValidationCallback ClientCertValidationDelegate
		{
			get
			{
				return this.ClientCertValidation;
			}
			set
			{
				this.ClientCertValidation = value;
			}
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x0600030C RID: 780 RVA: 0x0001142C File Offset: 0x0000F62C
		// (set) Token: 0x0600030D RID: 781 RVA: 0x00011434 File Offset: 0x0000F634
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

		// Token: 0x14000007 RID: 7
		// (add) Token: 0x0600030E RID: 782 RVA: 0x00011440 File Offset: 0x0000F640
		// (remove) Token: 0x0600030F RID: 783 RVA: 0x00011478 File Offset: 0x0000F678
		public event CertificateValidationCallback2 ClientCertValidation2;

		// Token: 0x06000310 RID: 784 RVA: 0x000114AD File Offset: 0x0000F6AD
		public SslServerStream(Stream stream, global::System.Security.Cryptography.X509Certificates.X509Certificate serverCertificate)
			: this(stream, serverCertificate, false, false, SecurityProtocolType.Default)
		{
		}

		// Token: 0x06000311 RID: 785 RVA: 0x000114BE File Offset: 0x0000F6BE
		public SslServerStream(Stream stream, global::System.Security.Cryptography.X509Certificates.X509Certificate serverCertificate, bool clientCertificateRequired, bool ownsStream)
			: this(stream, serverCertificate, clientCertificateRequired, ownsStream, SecurityProtocolType.Default)
		{
		}

		// Token: 0x06000312 RID: 786 RVA: 0x000114D0 File Offset: 0x0000F6D0
		public SslServerStream(Stream stream, global::System.Security.Cryptography.X509Certificates.X509Certificate serverCertificate, bool clientCertificateRequired, bool requestClientCertificate, bool ownsStream)
			: this(stream, serverCertificate, clientCertificateRequired, requestClientCertificate, ownsStream, SecurityProtocolType.Default)
		{
		}

		// Token: 0x06000313 RID: 787 RVA: 0x000114E4 File Offset: 0x0000F6E4
		public SslServerStream(Stream stream, global::System.Security.Cryptography.X509Certificates.X509Certificate serverCertificate, bool clientCertificateRequired, bool ownsStream, SecurityProtocolType securityProtocolType)
			: this(stream, serverCertificate, clientCertificateRequired, false, ownsStream, securityProtocolType)
		{
		}

		// Token: 0x06000314 RID: 788 RVA: 0x000114F4 File Offset: 0x0000F6F4
		public SslServerStream(Stream stream, global::System.Security.Cryptography.X509Certificates.X509Certificate serverCertificate, bool clientCertificateRequired, bool requestClientCertificate, bool ownsStream, SecurityProtocolType securityProtocolType)
			: base(stream, ownsStream)
		{
			this.context = new ServerContext(this, securityProtocolType, serverCertificate, clientCertificateRequired, requestClientCertificate);
			this.protocol = new ServerRecordProtocol(this.innerStream, (ServerContext)this.context);
		}

		// Token: 0x06000315 RID: 789 RVA: 0x00011530 File Offset: 0x0000F730
		~SslServerStream()
		{
			this.Dispose(false);
		}

		// Token: 0x06000316 RID: 790 RVA: 0x00011560 File Offset: 0x0000F760
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
			if (disposing)
			{
				this.ClientCertValidation = null;
				this.PrivateKeySelection = null;
			}
		}

		// Token: 0x06000317 RID: 791 RVA: 0x0001157C File Offset: 0x0000F77C
		internal override IAsyncResult BeginNegotiateHandshake(AsyncCallback callback, object state)
		{
			if (this.context.HandshakeState != HandshakeState.None)
			{
				this.context.Clear();
			}
			this.context.SupportedCiphers = CipherSuiteFactory.GetSupportedCiphers(true, this.context.SecurityProtocol);
			this.context.HandshakeState = HandshakeState.Started;
			return this.protocol.BeginReceiveRecord(this.innerStream, callback, state);
		}

		// Token: 0x06000318 RID: 792 RVA: 0x000115DC File Offset: 0x0000F7DC
		internal override void EndNegotiateHandshake(IAsyncResult asyncResult)
		{
			this.protocol.EndReceiveRecord(asyncResult);
			if (this.context.LastHandshakeMsg != HandshakeType.ClientHello)
			{
				this.protocol.SendAlert(AlertDescription.UnexpectedMessage);
			}
			this.protocol.SendRecord(HandshakeType.ServerHello);
			this.protocol.SendRecord(HandshakeType.Certificate);
			if (((ServerContext)this.context).ClientCertificateRequired || ((ServerContext)this.context).RequestClientCertificate)
			{
				this.protocol.SendRecord(HandshakeType.CertificateRequest);
			}
			this.protocol.SendRecord(HandshakeType.ServerHelloDone);
			while (this.context.LastHandshakeMsg != HandshakeType.Finished)
			{
				byte[] array = this.protocol.ReceiveRecord(this.innerStream);
				if (array == null || array.Length == 0)
				{
					throw new TlsException(AlertDescription.HandshakeFailiure, "The client stopped the handshake.");
				}
			}
			this.protocol.SendChangeCipherSpec();
			this.protocol.SendRecord(HandshakeType.Finished);
			this.context.HandshakeState = HandshakeState.Finished;
			this.context.HandshakeMessages.Reset();
			this.context.ClearKeyInfo();
		}

		// Token: 0x06000319 RID: 793 RVA: 0x000116DE File Offset: 0x0000F8DE
		internal override global::System.Security.Cryptography.X509Certificates.X509Certificate OnLocalCertificateSelection(global::System.Security.Cryptography.X509Certificates.X509CertificateCollection clientCertificates, global::System.Security.Cryptography.X509Certificates.X509Certificate serverCertificate, string targetHost, global::System.Security.Cryptography.X509Certificates.X509CertificateCollection serverRequestedCertificates)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600031A RID: 794 RVA: 0x000116E5 File Offset: 0x0000F8E5
		internal override bool OnRemoteCertificateValidation(global::System.Security.Cryptography.X509Certificates.X509Certificate certificate, int[] errors)
		{
			if (this.ClientCertValidation != null)
			{
				return this.ClientCertValidation(certificate, errors);
			}
			return errors != null && errors.Length == 0;
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x0600031B RID: 795 RVA: 0x00011707 File Offset: 0x0000F907
		internal override bool HaveRemoteValidation2Callback
		{
			get
			{
				return this.ClientCertValidation2 != null;
			}
		}

		// Token: 0x0600031C RID: 796 RVA: 0x00011714 File Offset: 0x0000F914
		internal override ValidationResult OnRemoteCertificateValidation2(Mono.Security.X509.X509CertificateCollection collection)
		{
			CertificateValidationCallback2 clientCertValidation = this.ClientCertValidation2;
			if (clientCertValidation != null)
			{
				return clientCertValidation(collection);
			}
			return null;
		}

		// Token: 0x0600031D RID: 797 RVA: 0x00011734 File Offset: 0x0000F934
		internal bool RaiseClientCertificateValidation(global::System.Security.Cryptography.X509Certificates.X509Certificate certificate, int[] certificateErrors)
		{
			return base.RaiseRemoteCertificateValidation(certificate, certificateErrors);
		}

		// Token: 0x0600031E RID: 798 RVA: 0x0001173E File Offset: 0x0000F93E
		internal override AsymmetricAlgorithm OnLocalPrivateKeySelection(global::System.Security.Cryptography.X509Certificates.X509Certificate certificate, string targetHost)
		{
			if (this.PrivateKeySelection != null)
			{
				return this.PrivateKeySelection(certificate, targetHost);
			}
			return null;
		}

		// Token: 0x0600031F RID: 799 RVA: 0x00011757 File Offset: 0x0000F957
		internal AsymmetricAlgorithm RaisePrivateKeySelection(global::System.Security.Cryptography.X509Certificates.X509Certificate certificate, string targetHost)
		{
			return base.RaiseLocalPrivateKeySelection(certificate, targetHost);
		}
	}
}
