using System;

namespace Mono.Posix
{
	// Token: 0x0200009C RID: 156
	[Flags]
	[Obsolete("Use Mono.Unix.Native.FilePermissions")]
	public enum StatMode
	{
		// Token: 0x04000523 RID: 1315
		[Obsolete("Use Mono.Unix.Native.FilePermissions.S_IFSOCK")]
		Socket = 49152,
		// Token: 0x04000524 RID: 1316
		[Obsolete("Use Mono.Unix.Native.FilePermissions.S_IFLNK")]
		SymLink = 40960,
		// Token: 0x04000525 RID: 1317
		[Obsolete("Use Mono.Unix.Native.FilePermissions.S_IFREG")]
		Regular = 32768,
		// Token: 0x04000526 RID: 1318
		[Obsolete("Use Mono.Unix.Native.FilePermissions.S_IFBLK")]
		BlockDevice = 24576,
		// Token: 0x04000527 RID: 1319
		[Obsolete("Use Mono.Unix.Native.FilePermissions.S_IFDIR")]
		Directory = 16384,
		// Token: 0x04000528 RID: 1320
		[Obsolete("Use Mono.Unix.Native.FilePermissions.S_IFCHR")]
		CharDevice = 8192,
		// Token: 0x04000529 RID: 1321
		[Obsolete("Use Mono.Unix.Native.FilePermissions.S_IFIFO")]
		FIFO = 4096,
		// Token: 0x0400052A RID: 1322
		[Obsolete("Use Mono.Unix.Native.FilePermissions.S_ISUID")]
		SUid = 2048,
		// Token: 0x0400052B RID: 1323
		[Obsolete("Use Mono.Unix.Native.FilePermissions.S_ISGID")]
		SGid = 1024,
		// Token: 0x0400052C RID: 1324
		[Obsolete("Use Mono.Unix.Native.FilePermissions.S_ISVTX")]
		Sticky = 512,
		// Token: 0x0400052D RID: 1325
		[Obsolete("Use Mono.Unix.Native.FilePermissions.S_IRUSR")]
		OwnerRead = 256,
		// Token: 0x0400052E RID: 1326
		[Obsolete("Use Mono.Unix.Native.FilePermissions.S_IWUSR")]
		OwnerWrite = 128,
		// Token: 0x0400052F RID: 1327
		[Obsolete("Use Mono.Unix.Native.FilePermissions.S_IXUSR")]
		OwnerExecute = 64,
		// Token: 0x04000530 RID: 1328
		[Obsolete("Use Mono.Unix.Native.FilePermissions.S_IRGRP")]
		GroupRead = 32,
		// Token: 0x04000531 RID: 1329
		[Obsolete("Use Mono.Unix.Native.FilePermissions.S_IWGRP")]
		GroupWrite = 16,
		// Token: 0x04000532 RID: 1330
		[Obsolete("Use Mono.Unix.Native.FilePermissions.S_IXGRP")]
		GroupExecute = 8,
		// Token: 0x04000533 RID: 1331
		[Obsolete("Use Mono.Unix.Native.FilePermissions.S_IROTH")]
		OthersRead = 4,
		// Token: 0x04000534 RID: 1332
		[Obsolete("Use Mono.Unix.Native.FilePermissions.S_IWOTH")]
		OthersWrite = 2,
		// Token: 0x04000535 RID: 1333
		[Obsolete("Use Mono.Unix.Native.FilePermissions.S_IXOTH")]
		OthersExecute = 1
	}
}
