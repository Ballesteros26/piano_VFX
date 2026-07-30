using System;

namespace Mono.Unix.Native
{
	// Token: 0x02000030 RID: 48
	[Map]
	[CLSCompliant(false)]
	public enum SyslogLevel
	{
		// Token: 0x0400017A RID: 378
		LOG_EMERG,
		// Token: 0x0400017B RID: 379
		LOG_ALERT,
		// Token: 0x0400017C RID: 380
		LOG_CRIT,
		// Token: 0x0400017D RID: 381
		LOG_ERR,
		// Token: 0x0400017E RID: 382
		LOG_WARNING,
		// Token: 0x0400017F RID: 383
		LOG_NOTICE,
		// Token: 0x04000180 RID: 384
		LOG_INFO,
		// Token: 0x04000181 RID: 385
		LOG_DEBUG
	}
}
