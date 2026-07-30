using System;
using System.Security.Cryptography.X509Certificates;

namespace Mono.Security.Protocol.Tls.Handshake.Client
{
	// Token: 0x02000061 RID: 97
	internal class TlsClientCertificate : HandshakeMessage
	{
		// Token: 0x060003D4 RID: 980 RVA: 0x0001428D File Offset: 0x0001248D
		public TlsClientCertificate(Context context)
			: base(context, HandshakeType.Certificate)
		{
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x060003D5 RID: 981 RVA: 0x00014298 File Offset: 0x00012498
		public X509Certificate ClientCertificate
		{
			get
			{
				if (!this.clientCertSelected)
				{
					this.GetClientCertificate();
					this.clientCertSelected = true;
				}
				return this.clientCert;
			}
		}

		// Token: 0x060003D6 RID: 982 RVA: 0x000142B5 File Offset: 0x000124B5
		public override void Update()
		{
			base.Update();
			base.Reset();
		}

		// Token: 0x060003D7 RID: 983 RVA: 0x000142C4 File Offset: 0x000124C4
		private void GetClientCertificate()
		{
			ClientContext clientContext = (ClientContext)base.Context;
			if (clientContext.ClientSettings.Certificates != null && clientContext.ClientSettings.Certificates.Count > 0)
			{
				this.clientCert = clientContext.SslStream.RaiseClientCertificateSelection(base.Context.ClientSettings.Certificates, new X509Certificate(base.Context.ServerSettings.Certificates[0].RawData), base.Context.ClientSettings.TargetHost, null);
			}
			clientContext.ClientSettings.ClientCertificate = this.clientCert;
		}

		// Token: 0x060003D8 RID: 984 RVA: 0x00014360 File Offset: 0x00012560
		private void SendCertificates()
		{
			TlsStream tlsStream = new TlsStream();
			for (X509Certificate x509Certificate = this.ClientCertificate; x509Certificate != null; x509Certificate = this.FindParentCertificate(x509Certificate))
			{
				byte[] rawCertData = x509Certificate.GetRawCertData();
				tlsStream.WriteInt24(rawCertData.Length);
				tlsStream.Write(rawCertData);
			}
			base.WriteInt24((int)tlsStream.Length);
			base.Write(tlsStream.ToArray());
		}

		// Token: 0x060003D9 RID: 985 RVA: 0x000143B7 File Offset: 0x000125B7
		protected override void ProcessAsSsl3()
		{
			if (this.ClientCertificate != null)
			{
				this.SendCertificates();
			}
		}

		// Token: 0x060003DA RID: 986 RVA: 0x000143C7 File Offset: 0x000125C7
		protected override void ProcessAsTls1()
		{
			if (this.ClientCertificate != null)
			{
				this.SendCertificates();
				return;
			}
			base.WriteInt24(0);
		}

		// Token: 0x060003DB RID: 987 RVA: 0x000143E0 File Offset: 0x000125E0
		private X509Certificate FindParentCertificate(X509Certificate cert)
		{
			if (cert.GetName() == cert.GetIssuerName())
			{
				return null;
			}
			foreach (X509Certificate x509Certificate in base.Context.ClientSettings.Certificates)
			{
				if (x509Certificate.GetName() == cert.GetIssuerName())
				{
					return x509Certificate;
				}
			}
			return null;
		}

		// Token: 0x040001E3 RID: 483
		private bool clientCertSelected;

		// Token: 0x040001E4 RID: 484
		private X509Certificate clientCert;
	}
}
