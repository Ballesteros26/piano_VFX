using System;

namespace Mono.Posix
{
	// Token: 0x02000095 RID: 149
	[Flags]
	[CLSCompliant(false)]
	[Obsolete("Use Mono.Unix.Native.OpenFlags")]
	public enum OpenFlags
	{
		// Token: 0x040004E0 RID: 1248
		O_RDONLY = 0,
		// Token: 0x040004E1 RID: 1249
		O_WRONLY = 1,
		// Token: 0x040004E2 RID: 1250
		O_RDWR = 2,
		// Token: 0x040004E3 RID: 1251
		O_CREAT = 4,
		// Token: 0x040004E4 RID: 1252
		O_EXCL = 8,
		// Token: 0x040004E5 RID: 1253
		O_NOCTTY = 16,
		// Token: 0x040004E6 RID: 1254
		O_TRUNC = 32,
		// Token: 0x040004E7 RID: 1255
		O_APPEND = 64,
		// Token: 0x040004E8 RID: 1256
		O_NONBLOCK = 128,
		// Token: 0x040004E9 RID: 1257
		O_SYNC = 256
	}
}
