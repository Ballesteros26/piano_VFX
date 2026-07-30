using System;

namespace Mono.Security.Protocol.Tls
{
	// Token: 0x0200002E RID: 46
	[Serializable]
	public enum CipherAlgorithmType
	{
		// Token: 0x04000116 RID: 278
		Des,
		// Token: 0x04000117 RID: 279
		None,
		// Token: 0x04000118 RID: 280
		Rc2,
		// Token: 0x04000119 RID: 281
		Rc4,
		// Token: 0x0400011A RID: 282
		Rijndael,
		// Token: 0x0400011B RID: 283
		SkipJack,
		// Token: 0x0400011C RID: 284
		TripleDes
	}
}
