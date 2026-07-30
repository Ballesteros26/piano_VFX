using System;

namespace System.Threading.Tasks
{
	// Token: 0x02000504 RID: 1284
	[Flags]
	[Serializable]
	internal enum InternalTaskOptions
	{
		// Token: 0x04001EC4 RID: 7876
		None = 0,
		// Token: 0x04001EC5 RID: 7877
		InternalOptionsMask = 65280,
		// Token: 0x04001EC6 RID: 7878
		ChildReplica = 256,
		// Token: 0x04001EC7 RID: 7879
		ContinuationTask = 512,
		// Token: 0x04001EC8 RID: 7880
		PromiseTask = 1024,
		// Token: 0x04001EC9 RID: 7881
		SelfReplicating = 2048,
		// Token: 0x04001ECA RID: 7882
		LazyCancellation = 4096,
		// Token: 0x04001ECB RID: 7883
		QueuedByRuntime = 8192,
		// Token: 0x04001ECC RID: 7884
		DoNotDispose = 16384
	}
}
