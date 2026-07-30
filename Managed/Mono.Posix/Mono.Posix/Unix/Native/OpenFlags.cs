using System;

namespace Mono.Unix.Native
{
	// Token: 0x02000031 RID: 49
	[Map]
	[Flags]
	[CLSCompliant(false)]
	public enum OpenFlags
	{
		// Token: 0x04000183 RID: 387
		O_RDONLY = 0,
		// Token: 0x04000184 RID: 388
		O_WRONLY = 1,
		// Token: 0x04000185 RID: 389
		O_RDWR = 2,
		// Token: 0x04000186 RID: 390
		O_CREAT = 64,
		// Token: 0x04000187 RID: 391
		O_EXCL = 128,
		// Token: 0x04000188 RID: 392
		O_NOCTTY = 256,
		// Token: 0x04000189 RID: 393
		O_TRUNC = 512,
		// Token: 0x0400018A RID: 394
		O_APPEND = 1024,
		// Token: 0x0400018B RID: 395
		O_NONBLOCK = 2048,
		// Token: 0x0400018C RID: 396
		O_SYNC = 4096,
		// Token: 0x0400018D RID: 397
		O_NOFOLLOW = 131072,
		// Token: 0x0400018E RID: 398
		O_DIRECTORY = 65536,
		// Token: 0x0400018F RID: 399
		O_DIRECT = 16384,
		// Token: 0x04000190 RID: 400
		O_ASYNC = 8192,
		// Token: 0x04000191 RID: 401
		O_LARGEFILE = 32768,
		// Token: 0x04000192 RID: 402
		O_CLOEXEC = 524288,
		// Token: 0x04000193 RID: 403
		O_PATH = 2097152
	}
}
