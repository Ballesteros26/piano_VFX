using System;

namespace UnityEngine.Analytics
{
	// Token: 0x02000011 RID: 17
	[Flags]
	public enum AnalyticsEventPriority
	{
		// Token: 0x0400002A RID: 42
		FlushQueueFlag = 1,
		// Token: 0x0400002B RID: 43
		CacheImmediatelyFlag = 2,
		// Token: 0x0400002C RID: 44
		AllowInStopModeFlag = 4,
		// Token: 0x0400002D RID: 45
		SendImmediateFlag = 8,
		// Token: 0x0400002E RID: 46
		NoCachingFlag = 16,
		// Token: 0x0400002F RID: 47
		NoRetryFlag = 32,
		// Token: 0x04000030 RID: 48
		NormalPriorityEvent = 0,
		// Token: 0x04000031 RID: 49
		NormalPriorityEvent_WithCaching = 2,
		// Token: 0x04000032 RID: 50
		NormalPriorityEvent_NoRetryNoCaching = 48,
		// Token: 0x04000033 RID: 51
		HighPriorityEvent = 1,
		// Token: 0x04000034 RID: 52
		HighPriorityEvent_InStopMode = 5,
		// Token: 0x04000035 RID: 53
		HighestPriorityEvent = 9,
		// Token: 0x04000036 RID: 54
		HighestPriorityEvent_NoRetryNoCaching = 49
	}
}
