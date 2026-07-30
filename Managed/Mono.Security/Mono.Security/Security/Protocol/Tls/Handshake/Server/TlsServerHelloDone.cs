using System;

namespace Mono.Security.Protocol.Tls.Handshake.Server
{
	// Token: 0x0200005F RID: 95
	internal class TlsServerHelloDone : HandshakeMessage
	{
		// Token: 0x060003CC RID: 972 RVA: 0x0001415E File Offset: 0x0001235E
		public TlsServerHelloDone(Context context)
			: base(context, HandshakeType.ServerHelloDone)
		{
		}

		// Token: 0x060003CD RID: 973 RVA: 0x00014169 File Offset: 0x00012369
		protected override void ProcessAsSsl3()
		{
		}

		// Token: 0x060003CE RID: 974 RVA: 0x0001416B File Offset: 0x0001236B
		protected override void ProcessAsTls1()
		{
		}
	}
}
