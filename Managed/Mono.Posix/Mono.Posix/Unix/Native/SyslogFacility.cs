using System;

namespace Mono.Unix.Native
{
	// Token: 0x0200002F RID: 47
	[Map]
	[CLSCompliant(false)]
	public enum SyslogFacility
	{
		// Token: 0x04000165 RID: 357
		LOG_KERN,
		// Token: 0x04000166 RID: 358
		LOG_USER = 8,
		// Token: 0x04000167 RID: 359
		LOG_MAIL = 16,
		// Token: 0x04000168 RID: 360
		LOG_DAEMON = 24,
		// Token: 0x04000169 RID: 361
		LOG_AUTH = 32,
		// Token: 0x0400016A RID: 362
		LOG_SYSLOG = 40,
		// Token: 0x0400016B RID: 363
		LOG_LPR = 48,
		// Token: 0x0400016C RID: 364
		LOG_NEWS = 56,
		// Token: 0x0400016D RID: 365
		LOG_UUCP = 64,
		// Token: 0x0400016E RID: 366
		LOG_CRON = 72,
		// Token: 0x0400016F RID: 367
		LOG_AUTHPRIV = 80,
		// Token: 0x04000170 RID: 368
		LOG_FTP = 88,
		// Token: 0x04000171 RID: 369
		LOG_LOCAL0 = 128,
		// Token: 0x04000172 RID: 370
		LOG_LOCAL1 = 136,
		// Token: 0x04000173 RID: 371
		LOG_LOCAL2 = 144,
		// Token: 0x04000174 RID: 372
		LOG_LOCAL3 = 152,
		// Token: 0x04000175 RID: 373
		LOG_LOCAL4 = 160,
		// Token: 0x04000176 RID: 374
		LOG_LOCAL5 = 168,
		// Token: 0x04000177 RID: 375
		LOG_LOCAL6 = 176,
		// Token: 0x04000178 RID: 376
		LOG_LOCAL7 = 184
	}
}
