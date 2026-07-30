using System;

namespace System
{
	// Token: 0x02000102 RID: 258
	internal enum ParsingError
	{
		// Token: 0x04000CDA RID: 3290
		None,
		// Token: 0x04000CDB RID: 3291
		BadFormat,
		// Token: 0x04000CDC RID: 3292
		BadScheme,
		// Token: 0x04000CDD RID: 3293
		BadAuthority,
		// Token: 0x04000CDE RID: 3294
		EmptyUriString,
		// Token: 0x04000CDF RID: 3295
		LastRelativeUriOkErrIndex = 4,
		// Token: 0x04000CE0 RID: 3296
		SchemeLimit,
		// Token: 0x04000CE1 RID: 3297
		SizeLimit,
		// Token: 0x04000CE2 RID: 3298
		MustRootedPath,
		// Token: 0x04000CE3 RID: 3299
		BadHostName,
		// Token: 0x04000CE4 RID: 3300
		NonEmptyHost,
		// Token: 0x04000CE5 RID: 3301
		BadPort,
		// Token: 0x04000CE6 RID: 3302
		BadAuthorityTerminator,
		// Token: 0x04000CE7 RID: 3303
		CannotCreateRelative
	}
}
