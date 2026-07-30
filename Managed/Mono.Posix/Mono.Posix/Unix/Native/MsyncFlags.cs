using System;

namespace Mono.Unix.Native
{
	// Token: 0x02000046 RID: 70
	[Map]
	[Flags]
	[CLSCompliant(false)]
	public enum MsyncFlags
	{
		// Token: 0x04000358 RID: 856
		MS_ASYNC = 1,
		// Token: 0x04000359 RID: 857
		MS_SYNC = 4,
		// Token: 0x0400035A RID: 858
		MS_INVALIDATE = 2
	}
}
