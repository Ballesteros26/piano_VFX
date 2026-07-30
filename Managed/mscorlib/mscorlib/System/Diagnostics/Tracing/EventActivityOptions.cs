using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000AF7 RID: 2807
	[Flags]
	public enum EventActivityOptions
	{
		// Token: 0x0400322B RID: 12843
		None = 0,
		// Token: 0x0400322C RID: 12844
		Disable = 2,
		// Token: 0x0400322D RID: 12845
		Recursive = 4,
		// Token: 0x0400322E RID: 12846
		Detachable = 8
	}
}
