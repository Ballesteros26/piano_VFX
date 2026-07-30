using System;

namespace Mono.Posix
{
	// Token: 0x0200009B RID: 155
	[Obsolete("Use Mono.Unix.Native.FilePermissions")]
	public enum StatModeMasks
	{
		// Token: 0x0400051E RID: 1310
		[Obsolete("Use Mono.Unix.Native.FilePermissions.S_IFMT")]
		TypeMask = 61440,
		// Token: 0x0400051F RID: 1311
		[Obsolete("Use Mono.Unix.Native.FilePermissions.S_RWXU")]
		OwnerMask = 448,
		// Token: 0x04000520 RID: 1312
		[Obsolete("Use Mono.Unix.Native.FilePermissions.S_RWXG")]
		GroupMask = 56,
		// Token: 0x04000521 RID: 1313
		[Obsolete("Use Mono.Unix.Native.FilePermissions.S_RWXO")]
		OthersMask = 7
	}
}
