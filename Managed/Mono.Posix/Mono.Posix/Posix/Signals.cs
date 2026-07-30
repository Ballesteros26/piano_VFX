using System;

namespace Mono.Posix
{
	// Token: 0x02000099 RID: 153
	[CLSCompliant(false)]
	[Obsolete("Use Mono.Unix.Native.Signum")]
	public enum Signals
	{
		// Token: 0x04000500 RID: 1280
		SIGHUP,
		// Token: 0x04000501 RID: 1281
		SIGINT,
		// Token: 0x04000502 RID: 1282
		SIGQUIT,
		// Token: 0x04000503 RID: 1283
		SIGILL,
		// Token: 0x04000504 RID: 1284
		SIGTRAP,
		// Token: 0x04000505 RID: 1285
		SIGABRT,
		// Token: 0x04000506 RID: 1286
		SIGBUS,
		// Token: 0x04000507 RID: 1287
		SIGFPE,
		// Token: 0x04000508 RID: 1288
		SIGKILL,
		// Token: 0x04000509 RID: 1289
		SIGUSR1,
		// Token: 0x0400050A RID: 1290
		SIGSEGV,
		// Token: 0x0400050B RID: 1291
		SIGUSR2,
		// Token: 0x0400050C RID: 1292
		SIGPIPE,
		// Token: 0x0400050D RID: 1293
		SIGALRM,
		// Token: 0x0400050E RID: 1294
		SIGTERM,
		// Token: 0x0400050F RID: 1295
		SIGCHLD,
		// Token: 0x04000510 RID: 1296
		SIGCONT,
		// Token: 0x04000511 RID: 1297
		SIGSTOP,
		// Token: 0x04000512 RID: 1298
		SIGTSTP,
		// Token: 0x04000513 RID: 1299
		SIGTTIN,
		// Token: 0x04000514 RID: 1300
		SIGTTOU,
		// Token: 0x04000515 RID: 1301
		SIGURG,
		// Token: 0x04000516 RID: 1302
		SIGXCPU,
		// Token: 0x04000517 RID: 1303
		SIGXFSZ,
		// Token: 0x04000518 RID: 1304
		SIGVTALRM,
		// Token: 0x04000519 RID: 1305
		SIGPROF,
		// Token: 0x0400051A RID: 1306
		SIGWINCH,
		// Token: 0x0400051B RID: 1307
		SIGIO,
		// Token: 0x0400051C RID: 1308
		SIGSYS
	}
}
