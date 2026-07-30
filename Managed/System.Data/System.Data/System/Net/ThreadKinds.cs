using System;

namespace System.Net
{
	// Token: 0x02000040 RID: 64
	[Flags]
	internal enum ThreadKinds
	{
		// Token: 0x04000469 RID: 1129
		Unknown = 0,
		// Token: 0x0400046A RID: 1130
		User = 1,
		// Token: 0x0400046B RID: 1131
		System = 2,
		// Token: 0x0400046C RID: 1132
		Sync = 4,
		// Token: 0x0400046D RID: 1133
		Async = 8,
		// Token: 0x0400046E RID: 1134
		Timer = 16,
		// Token: 0x0400046F RID: 1135
		CompletionPort = 32,
		// Token: 0x04000470 RID: 1136
		Worker = 64,
		// Token: 0x04000471 RID: 1137
		Finalization = 128,
		// Token: 0x04000472 RID: 1138
		Other = 256,
		// Token: 0x04000473 RID: 1139
		OwnerMask = 3,
		// Token: 0x04000474 RID: 1140
		SyncMask = 12,
		// Token: 0x04000475 RID: 1141
		SourceMask = 496,
		// Token: 0x04000476 RID: 1142
		SafeSources = 352,
		// Token: 0x04000477 RID: 1143
		ThreadPool = 96
	}
}
