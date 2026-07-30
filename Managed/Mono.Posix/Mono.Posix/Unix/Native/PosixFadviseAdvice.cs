using System;

namespace Mono.Unix.Native
{
	// Token: 0x02000038 RID: 56
	[Map]
	[CLSCompliant(false)]
	public enum PosixFadviseAdvice
	{
		// Token: 0x040001DA RID: 474
		POSIX_FADV_NORMAL,
		// Token: 0x040001DB RID: 475
		POSIX_FADV_RANDOM,
		// Token: 0x040001DC RID: 476
		POSIX_FADV_SEQUENTIAL,
		// Token: 0x040001DD RID: 477
		POSIX_FADV_WILLNEED,
		// Token: 0x040001DE RID: 478
		POSIX_FADV_DONTNEED,
		// Token: 0x040001DF RID: 479
		POSIX_FADV_NOREUSE
	}
}
