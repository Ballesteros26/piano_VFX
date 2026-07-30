using System;

namespace Mono.Unix
{
	// Token: 0x0200000B RID: 11
	public enum FileTypes
	{
		// Token: 0x04000048 RID: 72
		Directory = 16384,
		// Token: 0x04000049 RID: 73
		CharacterDevice = 8192,
		// Token: 0x0400004A RID: 74
		BlockDevice = 24576,
		// Token: 0x0400004B RID: 75
		RegularFile = 32768,
		// Token: 0x0400004C RID: 76
		Fifo = 4096,
		// Token: 0x0400004D RID: 77
		SymbolicLink = 40960,
		// Token: 0x0400004E RID: 78
		Socket = 49152
	}
}
