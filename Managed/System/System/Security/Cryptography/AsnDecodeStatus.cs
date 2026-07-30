using System;

namespace System.Security.Cryptography
{
	// Token: 0x02000392 RID: 914
	internal enum AsnDecodeStatus
	{
		// Token: 0x040018F0 RID: 6384
		NotDecoded = -1,
		// Token: 0x040018F1 RID: 6385
		Ok,
		// Token: 0x040018F2 RID: 6386
		BadAsn,
		// Token: 0x040018F3 RID: 6387
		BadTag,
		// Token: 0x040018F4 RID: 6388
		BadLength,
		// Token: 0x040018F5 RID: 6389
		InformationNotAvailable
	}
}
