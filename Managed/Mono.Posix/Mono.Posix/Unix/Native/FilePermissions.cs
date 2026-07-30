using System;

namespace Mono.Unix.Native
{
	// Token: 0x02000033 RID: 51
	[Flags]
	[Map]
	[CLSCompliant(false)]
	public enum FilePermissions : uint
	{
		// Token: 0x0400019B RID: 411
		S_ISUID = 2048U,
		// Token: 0x0400019C RID: 412
		S_ISGID = 1024U,
		// Token: 0x0400019D RID: 413
		S_ISVTX = 512U,
		// Token: 0x0400019E RID: 414
		S_IRUSR = 256U,
		// Token: 0x0400019F RID: 415
		S_IWUSR = 128U,
		// Token: 0x040001A0 RID: 416
		S_IXUSR = 64U,
		// Token: 0x040001A1 RID: 417
		S_IRGRP = 32U,
		// Token: 0x040001A2 RID: 418
		S_IWGRP = 16U,
		// Token: 0x040001A3 RID: 419
		S_IXGRP = 8U,
		// Token: 0x040001A4 RID: 420
		S_IROTH = 4U,
		// Token: 0x040001A5 RID: 421
		S_IWOTH = 2U,
		// Token: 0x040001A6 RID: 422
		S_IXOTH = 1U,
		// Token: 0x040001A7 RID: 423
		S_IRWXG = 56U,
		// Token: 0x040001A8 RID: 424
		S_IRWXU = 448U,
		// Token: 0x040001A9 RID: 425
		S_IRWXO = 7U,
		// Token: 0x040001AA RID: 426
		ACCESSPERMS = 511U,
		// Token: 0x040001AB RID: 427
		ALLPERMS = 4095U,
		// Token: 0x040001AC RID: 428
		DEFFILEMODE = 438U,
		// Token: 0x040001AD RID: 429
		S_IFMT = 61440U,
		// Token: 0x040001AE RID: 430
		[Map(SuppressFlags = "S_IFMT")]
		S_IFDIR = 16384U,
		// Token: 0x040001AF RID: 431
		[Map(SuppressFlags = "S_IFMT")]
		S_IFCHR = 8192U,
		// Token: 0x040001B0 RID: 432
		[Map(SuppressFlags = "S_IFMT")]
		S_IFBLK = 24576U,
		// Token: 0x040001B1 RID: 433
		[Map(SuppressFlags = "S_IFMT")]
		S_IFREG = 32768U,
		// Token: 0x040001B2 RID: 434
		[Map(SuppressFlags = "S_IFMT")]
		S_IFIFO = 4096U,
		// Token: 0x040001B3 RID: 435
		[Map(SuppressFlags = "S_IFMT")]
		S_IFLNK = 40960U,
		// Token: 0x040001B4 RID: 436
		[Map(SuppressFlags = "S_IFMT")]
		S_IFSOCK = 49152U
	}
}
