using System;

namespace Mono.Unix.Native
{
	// Token: 0x0200002E RID: 46
	[Flags]
	[Map]
	[CLSCompliant(false)]
	public enum SyslogOptions
	{
		// Token: 0x0400015E RID: 350
		LOG_PID = 1,
		// Token: 0x0400015F RID: 351
		LOG_CONS = 2,
		// Token: 0x04000160 RID: 352
		LOG_ODELAY = 4,
		// Token: 0x04000161 RID: 353
		LOG_NDELAY = 8,
		// Token: 0x04000162 RID: 354
		LOG_NOWAIT = 16,
		// Token: 0x04000163 RID: 355
		LOG_PERROR = 32
	}
}
