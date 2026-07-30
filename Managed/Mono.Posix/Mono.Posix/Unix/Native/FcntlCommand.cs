using System;

namespace Mono.Unix.Native
{
	// Token: 0x02000034 RID: 52
	[Map]
	[CLSCompliant(false)]
	public enum FcntlCommand
	{
		// Token: 0x040001B6 RID: 438
		F_DUPFD,
		// Token: 0x040001B7 RID: 439
		F_GETFD,
		// Token: 0x040001B8 RID: 440
		F_SETFD,
		// Token: 0x040001B9 RID: 441
		F_GETFL,
		// Token: 0x040001BA RID: 442
		F_SETFL,
		// Token: 0x040001BB RID: 443
		F_GETLK = 12,
		// Token: 0x040001BC RID: 444
		F_SETLK,
		// Token: 0x040001BD RID: 445
		F_SETLKW,
		// Token: 0x040001BE RID: 446
		F_SETOWN = 8,
		// Token: 0x040001BF RID: 447
		F_GETOWN,
		// Token: 0x040001C0 RID: 448
		F_SETSIG,
		// Token: 0x040001C1 RID: 449
		F_GETSIG,
		// Token: 0x040001C2 RID: 450
		F_NOCACHE = 48,
		// Token: 0x040001C3 RID: 451
		F_SETLEASE = 1024,
		// Token: 0x040001C4 RID: 452
		F_GETLEASE,
		// Token: 0x040001C5 RID: 453
		F_NOTIFY
	}
}
