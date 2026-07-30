using System;

namespace Mono.Security.X509.Extensions
{
	// Token: 0x0200007B RID: 123
	[Flags]
	internal enum KeyUsages
	{
		// Token: 0x0400053F RID: 1343
		digitalSignature = 128,
		// Token: 0x04000540 RID: 1344
		nonRepudiation = 64,
		// Token: 0x04000541 RID: 1345
		keyEncipherment = 32,
		// Token: 0x04000542 RID: 1346
		dataEncipherment = 16,
		// Token: 0x04000543 RID: 1347
		keyAgreement = 8,
		// Token: 0x04000544 RID: 1348
		keyCertSign = 4,
		// Token: 0x04000545 RID: 1349
		cRLSign = 2,
		// Token: 0x04000546 RID: 1350
		encipherOnly = 1,
		// Token: 0x04000547 RID: 1351
		decipherOnly = 2048,
		// Token: 0x04000548 RID: 1352
		none = 0
	}
}
