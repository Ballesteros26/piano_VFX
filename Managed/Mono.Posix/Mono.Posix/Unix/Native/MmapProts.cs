using System;

namespace Mono.Unix.Native
{
	// Token: 0x02000045 RID: 69
	[Map]
	[Flags]
	[CLSCompliant(false)]
	public enum MmapProts
	{
		// Token: 0x04000351 RID: 849
		PROT_READ = 1,
		// Token: 0x04000352 RID: 850
		PROT_WRITE = 2,
		// Token: 0x04000353 RID: 851
		PROT_EXEC = 4,
		// Token: 0x04000354 RID: 852
		PROT_NONE = 0,
		// Token: 0x04000355 RID: 853
		PROT_GROWSDOWN = 16777216,
		// Token: 0x04000356 RID: 854
		PROT_GROWSUP = 33554432
	}
}
