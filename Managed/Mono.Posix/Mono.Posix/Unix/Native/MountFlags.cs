using System;

namespace Mono.Unix.Native
{
	// Token: 0x02000043 RID: 67
	[Map]
	[Flags]
	[CLSCompliant(false)]
	public enum MountFlags : ulong
	{
		// Token: 0x04000334 RID: 820
		ST_RDONLY = 1UL,
		// Token: 0x04000335 RID: 821
		ST_NOSUID = 2UL,
		// Token: 0x04000336 RID: 822
		ST_NODEV = 4UL,
		// Token: 0x04000337 RID: 823
		ST_NOEXEC = 8UL,
		// Token: 0x04000338 RID: 824
		ST_SYNCHRONOUS = 16UL,
		// Token: 0x04000339 RID: 825
		ST_REMOUNT = 32UL,
		// Token: 0x0400033A RID: 826
		ST_MANDLOCK = 64UL,
		// Token: 0x0400033B RID: 827
		ST_WRITE = 128UL,
		// Token: 0x0400033C RID: 828
		ST_APPEND = 256UL,
		// Token: 0x0400033D RID: 829
		ST_IMMUTABLE = 512UL,
		// Token: 0x0400033E RID: 830
		ST_NOATIME = 1024UL,
		// Token: 0x0400033F RID: 831
		ST_NODIRATIME = 2048UL,
		// Token: 0x04000340 RID: 832
		ST_BIND = 4096UL
	}
}
