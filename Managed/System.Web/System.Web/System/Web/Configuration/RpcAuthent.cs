using System;

namespace System.Web.Configuration
{
	// Token: 0x02000575 RID: 1397
	internal enum RpcAuthent
	{
		// Token: 0x0400204D RID: 8269
		None,
		// Token: 0x0400204E RID: 8270
		DcePrivate,
		// Token: 0x0400204F RID: 8271
		DcePublic,
		// Token: 0x04002050 RID: 8272
		DecPublic = 4,
		// Token: 0x04002051 RID: 8273
		GssNegotiate = 9,
		// Token: 0x04002052 RID: 8274
		WinNT,
		// Token: 0x04002053 RID: 8275
		GssSchannel = 14,
		// Token: 0x04002054 RID: 8276
		GssKerberos = 16,
		// Token: 0x04002055 RID: 8277
		DPA,
		// Token: 0x04002056 RID: 8278
		MSN,
		// Token: 0x04002057 RID: 8279
		Digest = 21,
		// Token: 0x04002058 RID: 8280
		MQ = 100,
		// Token: 0x04002059 RID: 8281
		Default = -1
	}
}
