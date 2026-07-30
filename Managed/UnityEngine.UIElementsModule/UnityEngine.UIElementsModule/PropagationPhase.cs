using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000150 RID: 336
	public enum PropagationPhase
	{
		// Token: 0x04000423 RID: 1059
		None,
		// Token: 0x04000424 RID: 1060
		TrickleDown,
		// Token: 0x04000425 RID: 1061
		AtTarget,
		// Token: 0x04000426 RID: 1062
		DefaultActionAtTarget = 5,
		// Token: 0x04000427 RID: 1063
		BubbleUp = 3,
		// Token: 0x04000428 RID: 1064
		DefaultAction
	}
}
