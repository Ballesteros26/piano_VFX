using System;

namespace Mono.Security.Protocol.Tls
{
	// Token: 0x02000036 RID: 54
	[Serializable]
	internal enum ContentType : byte
	{
		// Token: 0x0400013F RID: 319
		ChangeCipherSpec = 20,
		// Token: 0x04000140 RID: 320
		Alert,
		// Token: 0x04000141 RID: 321
		Handshake,
		// Token: 0x04000142 RID: 322
		ApplicationData
	}
}
