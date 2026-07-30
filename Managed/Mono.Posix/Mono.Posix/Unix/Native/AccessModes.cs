using System;

namespace Mono.Unix.Native
{
	// Token: 0x0200003C RID: 60
	[Flags]
	[Map]
	[CLSCompliant(false)]
	public enum AccessModes
	{
		// Token: 0x0400020E RID: 526
		R_OK = 1,
		// Token: 0x0400020F RID: 527
		W_OK = 2,
		// Token: 0x04000210 RID: 528
		X_OK = 4,
		// Token: 0x04000211 RID: 529
		F_OK = 8
	}
}
