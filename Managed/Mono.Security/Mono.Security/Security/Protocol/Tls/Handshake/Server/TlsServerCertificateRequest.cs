using System;

namespace Mono.Security.Protocol.Tls.Handshake.Server
{
	// Token: 0x0200005C RID: 92
	internal class TlsServerCertificateRequest : HandshakeMessage
	{
		// Token: 0x060003C1 RID: 961 RVA: 0x00013E68 File Offset: 0x00012068
		public TlsServerCertificateRequest(Context context)
			: base(context, HandshakeType.CertificateRequest)
		{
		}

		// Token: 0x060003C2 RID: 962 RVA: 0x00013E73 File Offset: 0x00012073
		protected override void ProcessAsSsl3()
		{
			this.ProcessAsTls1();
		}

		// Token: 0x060003C3 RID: 963 RVA: 0x00013E7C File Offset: 0x0001207C
		protected override void ProcessAsTls1()
		{
			ServerContext serverContext = (ServerContext)base.Context;
			int num = serverContext.ServerSettings.CertificateTypes.Length;
			this.WriteByte(Convert.ToByte(num));
			for (int i = 0; i < num; i++)
			{
				this.WriteByte((byte)serverContext.ServerSettings.CertificateTypes[i]);
			}
			base.Write(0);
		}
	}
}
