using System;

namespace Mono.Unix.Native
{
	// Token: 0x02000039 RID: 57
	[Map]
	[CLSCompliant(false)]
	public enum PosixMadviseAdvice
	{
		// Token: 0x040001E1 RID: 481
		POSIX_MADV_NORMAL,
		// Token: 0x040001E2 RID: 482
		POSIX_MADV_RANDOM,
		// Token: 0x040001E3 RID: 483
		POSIX_MADV_SEQUENTIAL,
		// Token: 0x040001E4 RID: 484
		POSIX_MADV_WILLNEED,
		// Token: 0x040001E5 RID: 485
		POSIX_MADV_DONTNEED
	}
}
