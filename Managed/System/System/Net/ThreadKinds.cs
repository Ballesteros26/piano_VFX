using System;

namespace System.Net
{
	// Token: 0x0200048F RID: 1167
	[Flags]
	internal enum ThreadKinds
	{
		// Token: 0x04001EFC RID: 7932
		Unknown = 0,
		// Token: 0x04001EFD RID: 7933
		User = 1,
		// Token: 0x04001EFE RID: 7934
		System = 2,
		// Token: 0x04001EFF RID: 7935
		Sync = 4,
		// Token: 0x04001F00 RID: 7936
		Async = 8,
		// Token: 0x04001F01 RID: 7937
		Timer = 16,
		// Token: 0x04001F02 RID: 7938
		CompletionPort = 32,
		// Token: 0x04001F03 RID: 7939
		Worker = 64,
		// Token: 0x04001F04 RID: 7940
		Finalization = 128,
		// Token: 0x04001F05 RID: 7941
		Other = 256,
		// Token: 0x04001F06 RID: 7942
		OwnerMask = 3,
		// Token: 0x04001F07 RID: 7943
		SyncMask = 12,
		// Token: 0x04001F08 RID: 7944
		SourceMask = 496,
		// Token: 0x04001F09 RID: 7945
		SafeSources = 352,
		// Token: 0x04001F0A RID: 7946
		ThreadPool = 96
	}
}
