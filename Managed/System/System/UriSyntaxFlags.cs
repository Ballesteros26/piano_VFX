using System;

namespace System
{
	// Token: 0x02000115 RID: 277
	[Flags]
	internal enum UriSyntaxFlags
	{
		// Token: 0x04000D3C RID: 3388
		None = 0,
		// Token: 0x04000D3D RID: 3389
		MustHaveAuthority = 1,
		// Token: 0x04000D3E RID: 3390
		OptionalAuthority = 2,
		// Token: 0x04000D3F RID: 3391
		MayHaveUserInfo = 4,
		// Token: 0x04000D40 RID: 3392
		MayHavePort = 8,
		// Token: 0x04000D41 RID: 3393
		MayHavePath = 16,
		// Token: 0x04000D42 RID: 3394
		MayHaveQuery = 32,
		// Token: 0x04000D43 RID: 3395
		MayHaveFragment = 64,
		// Token: 0x04000D44 RID: 3396
		AllowEmptyHost = 128,
		// Token: 0x04000D45 RID: 3397
		AllowUncHost = 256,
		// Token: 0x04000D46 RID: 3398
		AllowDnsHost = 512,
		// Token: 0x04000D47 RID: 3399
		AllowIPv4Host = 1024,
		// Token: 0x04000D48 RID: 3400
		AllowIPv6Host = 2048,
		// Token: 0x04000D49 RID: 3401
		AllowAnInternetHost = 3584,
		// Token: 0x04000D4A RID: 3402
		AllowAnyOtherHost = 4096,
		// Token: 0x04000D4B RID: 3403
		FileLikeUri = 8192,
		// Token: 0x04000D4C RID: 3404
		MailToLikeUri = 16384,
		// Token: 0x04000D4D RID: 3405
		V1_UnknownUri = 65536,
		// Token: 0x04000D4E RID: 3406
		SimpleUserSyntax = 131072,
		// Token: 0x04000D4F RID: 3407
		BuiltInSyntax = 262144,
		// Token: 0x04000D50 RID: 3408
		ParserSchemeOnly = 524288,
		// Token: 0x04000D51 RID: 3409
		AllowDOSPath = 1048576,
		// Token: 0x04000D52 RID: 3410
		PathIsRooted = 2097152,
		// Token: 0x04000D53 RID: 3411
		ConvertPathSlashes = 4194304,
		// Token: 0x04000D54 RID: 3412
		CompressPath = 8388608,
		// Token: 0x04000D55 RID: 3413
		CanonicalizeAsFilePath = 16777216,
		// Token: 0x04000D56 RID: 3414
		UnEscapeDotsAndSlashes = 33554432,
		// Token: 0x04000D57 RID: 3415
		AllowIdn = 67108864,
		// Token: 0x04000D58 RID: 3416
		AllowIriParsing = 268435456
	}
}
