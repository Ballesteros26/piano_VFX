using System;

namespace Mono.Posix
{
	// Token: 0x02000096 RID: 150
	[Flags]
	[CLSCompliant(false)]
	[Obsolete("Use Mono.Unix.Native.FilePermissions")]
	public enum FileMode
	{
		// Token: 0x040004EB RID: 1259
		S_ISUID = 2048,
		// Token: 0x040004EC RID: 1260
		S_ISGID = 1024,
		// Token: 0x040004ED RID: 1261
		S_ISVTX = 512,
		// Token: 0x040004EE RID: 1262
		S_IRUSR = 256,
		// Token: 0x040004EF RID: 1263
		S_IWUSR = 128,
		// Token: 0x040004F0 RID: 1264
		S_IXUSR = 64,
		// Token: 0x040004F1 RID: 1265
		S_IRGRP = 32,
		// Token: 0x040004F2 RID: 1266
		S_IWGRP = 16,
		// Token: 0x040004F3 RID: 1267
		S_IXGRP = 8,
		// Token: 0x040004F4 RID: 1268
		S_IROTH = 4,
		// Token: 0x040004F5 RID: 1269
		S_IWOTH = 2,
		// Token: 0x040004F6 RID: 1270
		S_IXOTH = 1
	}
}
