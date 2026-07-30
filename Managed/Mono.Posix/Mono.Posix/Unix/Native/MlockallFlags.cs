using System;

namespace Mono.Unix.Native
{
	// Token: 0x02000047 RID: 71
	[Map]
	[Flags]
	[CLSCompliant(false)]
	public enum MlockallFlags
	{
		// Token: 0x0400035C RID: 860
		MCL_CURRENT = 1,
		// Token: 0x0400035D RID: 861
		MCL_FUTURE = 2
	}
}
