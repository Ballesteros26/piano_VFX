using System;
using System.Security.Cryptography.X509Certificates;

namespace Mono.Security.Protocol.Tls
{
	// Token: 0x02000032 RID: 50
	internal class ClientContext : Context
	{
		// Token: 0x1700009A RID: 154
		// (get) Token: 0x06000220 RID: 544 RVA: 0x0000E107 File Offset: 0x0000C307
		public SslClientStream SslStream
		{
			get
			{
				return this.sslStream;
			}
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x06000221 RID: 545 RVA: 0x0000E10F File Offset: 0x0000C30F
		// (set) Token: 0x06000222 RID: 546 RVA: 0x0000E117 File Offset: 0x0000C317
		public short ClientHelloProtocol
		{
			get
			{
				return this.clientHelloProtocol;
			}
			set
			{
				this.clientHelloProtocol = value;
			}
		}

		// Token: 0x06000223 RID: 547 RVA: 0x0000E120 File Offset: 0x0000C320
		public ClientContext(SslClientStream stream, SecurityProtocolType securityProtocolType, string targetHost, X509CertificateCollection clientCertificates)
			: base(securityProtocolType)
		{
			this.sslStream = stream;
			base.ClientSettings.Certificates = clientCertificates;
			base.ClientSettings.TargetHost = targetHost;
		}

		// Token: 0x06000224 RID: 548 RVA: 0x0000E149 File Offset: 0x0000C349
		public override void Clear()
		{
			this.clientHelloProtocol = 0;
			base.Clear();
		}

		// Token: 0x04000133 RID: 307
		private SslClientStream sslStream;

		// Token: 0x04000134 RID: 308
		private short clientHelloProtocol;
	}
}
