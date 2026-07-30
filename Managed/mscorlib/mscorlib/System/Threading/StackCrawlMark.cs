using System;

namespace System.Threading
{
	// Token: 0x02000485 RID: 1157
	[Serializable]
	internal enum StackCrawlMark
	{
		// Token: 0x04001CE2 RID: 7394
		LookForMe,
		// Token: 0x04001CE3 RID: 7395
		LookForMyCaller,
		// Token: 0x04001CE4 RID: 7396
		LookForMyCallersCaller,
		// Token: 0x04001CE5 RID: 7397
		LookForThread
	}
}
