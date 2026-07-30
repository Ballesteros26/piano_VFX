using System;

namespace Mono.Unix.Native
{
	// Token: 0x02000036 RID: 54
	[Map]
	[CLSCompliant(false)]
	public enum SeekFlags : short
	{
		// Token: 0x040001CB RID: 459
		SEEK_SET,
		// Token: 0x040001CC RID: 460
		SEEK_CUR,
		// Token: 0x040001CD RID: 461
		SEEK_END,
		// Token: 0x040001CE RID: 462
		L_SET = 0,
		// Token: 0x040001CF RID: 463
		L_INCR,
		// Token: 0x040001D0 RID: 464
		L_XTND
	}
}
