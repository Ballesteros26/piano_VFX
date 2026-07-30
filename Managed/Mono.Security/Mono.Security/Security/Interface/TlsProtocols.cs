using System;

namespace Mono.Security.Interface
{
	// Token: 0x0200008B RID: 139
	[Flags]
	public enum TlsProtocols
	{
		// Token: 0x04000389 RID: 905
		Zero = 0,
		// Token: 0x0400038A RID: 906
		Tls10Client = 128,
		// Token: 0x0400038B RID: 907
		Tls10Server = 64,
		// Token: 0x0400038C RID: 908
		Tls10 = 192,
		// Token: 0x0400038D RID: 909
		Tls11Client = 512,
		// Token: 0x0400038E RID: 910
		Tls11Server = 256,
		// Token: 0x0400038F RID: 911
		Tls11 = 768,
		// Token: 0x04000390 RID: 912
		Tls12Client = 2048,
		// Token: 0x04000391 RID: 913
		Tls12Server = 1024,
		// Token: 0x04000392 RID: 914
		Tls12 = 3072,
		// Token: 0x04000393 RID: 915
		ClientMask = 2688,
		// Token: 0x04000394 RID: 916
		ServerMask = 1344
	}
}
