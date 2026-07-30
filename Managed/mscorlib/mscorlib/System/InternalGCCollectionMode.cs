using System;

namespace System
{
	// Token: 0x02000165 RID: 357
	[Serializable]
	internal enum InternalGCCollectionMode
	{
		// Token: 0x0400091C RID: 2332
		NonBlocking = 1,
		// Token: 0x0400091D RID: 2333
		Blocking,
		// Token: 0x0400091E RID: 2334
		Optimized = 4,
		// Token: 0x0400091F RID: 2335
		Compacting = 8
	}
}
