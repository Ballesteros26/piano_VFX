using System;

namespace Mono.Security.Protocol.Tls.Handshake
{
	// Token: 0x02000055 RID: 85
	[Serializable]
	internal enum HandshakeType : byte
	{
		// Token: 0x040001D0 RID: 464
		HelloRequest,
		// Token: 0x040001D1 RID: 465
		ClientHello,
		// Token: 0x040001D2 RID: 466
		ServerHello,
		// Token: 0x040001D3 RID: 467
		Certificate = 11,
		// Token: 0x040001D4 RID: 468
		ServerKeyExchange,
		// Token: 0x040001D5 RID: 469
		CertificateRequest,
		// Token: 0x040001D6 RID: 470
		ServerHelloDone,
		// Token: 0x040001D7 RID: 471
		CertificateVerify,
		// Token: 0x040001D8 RID: 472
		ClientKeyExchange,
		// Token: 0x040001D9 RID: 473
		Finished = 20,
		// Token: 0x040001DA RID: 474
		None = 255
	}
}
