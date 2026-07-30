using System;

namespace Mono.Unix.Native
{
	// Token: 0x02000044 RID: 68
	[Map]
	[Flags]
	[CLSCompliant(false)]
	public enum MmapFlags
	{
		// Token: 0x04000342 RID: 834
		MAP_SHARED = 1,
		// Token: 0x04000343 RID: 835
		MAP_PRIVATE = 2,
		// Token: 0x04000344 RID: 836
		MAP_TYPE = 15,
		// Token: 0x04000345 RID: 837
		MAP_FIXED = 16,
		// Token: 0x04000346 RID: 838
		MAP_FILE = 0,
		// Token: 0x04000347 RID: 839
		MAP_ANONYMOUS = 32,
		// Token: 0x04000348 RID: 840
		MAP_ANON = 32,
		// Token: 0x04000349 RID: 841
		MAP_GROWSDOWN = 256,
		// Token: 0x0400034A RID: 842
		MAP_DENYWRITE = 2048,
		// Token: 0x0400034B RID: 843
		MAP_EXECUTABLE = 4096,
		// Token: 0x0400034C RID: 844
		MAP_LOCKED = 8192,
		// Token: 0x0400034D RID: 845
		MAP_NORESERVE = 16384,
		// Token: 0x0400034E RID: 846
		MAP_POPULATE = 32768,
		// Token: 0x0400034F RID: 847
		MAP_NONBLOCK = 65536
	}
}
