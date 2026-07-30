using System;

namespace Mono.Security.Interface
{
	// Token: 0x0200007F RID: 127
	public enum HashAlgorithmType
	{
		// Token: 0x04000359 RID: 857
		None,
		// Token: 0x0400035A RID: 858
		Md5,
		// Token: 0x0400035B RID: 859
		Sha1,
		// Token: 0x0400035C RID: 860
		Sha224,
		// Token: 0x0400035D RID: 861
		Sha256,
		// Token: 0x0400035E RID: 862
		Sha384,
		// Token: 0x0400035F RID: 863
		Sha512,
		// Token: 0x04000360 RID: 864
		Unknown = 255,
		// Token: 0x04000361 RID: 865
		Md5Sha1 = 254
	}
}
