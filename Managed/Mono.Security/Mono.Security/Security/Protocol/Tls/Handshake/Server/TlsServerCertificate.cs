using System;
using Mono.Security.X509;

namespace Mono.Security.Protocol.Tls.Handshake.Server
{
	// Token: 0x0200005B RID: 91
	internal class TlsServerCertificate : HandshakeMessage
	{
		// Token: 0x060003BE RID: 958 RVA: 0x00013DB9 File Offset: 0x00011FB9
		public TlsServerCertificate(Context context)
			: base(context, HandshakeType.Certificate)
		{
		}

		// Token: 0x060003BF RID: 959 RVA: 0x00013DC4 File Offset: 0x00011FC4
		protected override void ProcessAsSsl3()
		{
			this.ProcessAsTls1();
		}

		// Token: 0x060003C0 RID: 960 RVA: 0x00013DCC File Offset: 0x00011FCC
		protected override void ProcessAsTls1()
		{
			TlsStream tlsStream = new TlsStream();
			foreach (X509Certificate x509Certificate in base.Context.ServerSettings.Certificates)
			{
				tlsStream.WriteInt24(x509Certificate.RawData.Length);
				tlsStream.Write(x509Certificate.RawData);
			}
			base.WriteInt24(Convert.ToInt32(tlsStream.Length));
			base.Write(tlsStream.ToArray());
			tlsStream.Close();
		}
	}
}
