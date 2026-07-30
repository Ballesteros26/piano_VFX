using System;

namespace Mono.Security.Protocol.Tls.Handshake.Client
{
	// Token: 0x0200006A RID: 106
	internal class TlsServerHelloDone : HandshakeMessage
	{
		// Token: 0x06000408 RID: 1032 RVA: 0x000155EE File Offset: 0x000137EE
		public TlsServerHelloDone(Context context, byte[] buffer)
			: base(context, HandshakeType.ServerHelloDone, buffer)
		{
		}

		// Token: 0x06000409 RID: 1033 RVA: 0x000155FA File Offset: 0x000137FA
		protected override void ProcessAsSsl3()
		{
		}

		// Token: 0x0600040A RID: 1034 RVA: 0x000155FC File Offset: 0x000137FC
		protected override void ProcessAsTls1()
		{
		}
	}
}
