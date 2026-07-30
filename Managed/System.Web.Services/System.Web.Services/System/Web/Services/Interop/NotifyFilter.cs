using System;

namespace System.Web.Services.Interop
{
	// Token: 0x02000099 RID: 153
	internal enum NotifyFilter
	{
		// Token: 0x04000319 RID: 793
		OnSyncCallOut = 1,
		// Token: 0x0400031A RID: 794
		OnSyncCallEnter,
		// Token: 0x0400031B RID: 795
		OnSyncCallExit = 4,
		// Token: 0x0400031C RID: 796
		OnSyncCallReturn = 8,
		// Token: 0x0400031D RID: 797
		AllSync = 15,
		// Token: 0x0400031E RID: 798
		All = -1,
		// Token: 0x0400031F RID: 799
		None
	}
}
