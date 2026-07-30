using System;

namespace Mono.Unix.Native
{
	// Token: 0x02000032 RID: 50
	[Map]
	[Flags]
	[CLSCompliant(false)]
	public enum AtFlags
	{
		// Token: 0x04000195 RID: 405
		AT_SYMLINK_NOFOLLOW = 256,
		// Token: 0x04000196 RID: 406
		AT_REMOVEDIR = 512,
		// Token: 0x04000197 RID: 407
		AT_SYMLINK_FOLLOW = 1024,
		// Token: 0x04000198 RID: 408
		AT_NO_AUTOMOUNT = 2048,
		// Token: 0x04000199 RID: 409
		AT_EMPTY_PATH = 4096
	}
}
