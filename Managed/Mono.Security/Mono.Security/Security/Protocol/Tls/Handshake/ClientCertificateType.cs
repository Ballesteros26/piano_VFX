using System;

namespace Mono.Security.Protocol.Tls.Handshake
{
	// Token: 0x02000053 RID: 83
	[Serializable]
	internal enum ClientCertificateType
	{
		// Token: 0x040001C6 RID: 454
		RSA = 1,
		// Token: 0x040001C7 RID: 455
		DSS,
		// Token: 0x040001C8 RID: 456
		RSAFixed,
		// Token: 0x040001C9 RID: 457
		DSSFixed,
		// Token: 0x040001CA RID: 458
		Unknown = 255
	}
}
