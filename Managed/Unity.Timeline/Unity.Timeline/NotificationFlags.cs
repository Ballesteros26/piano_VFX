using System;

namespace UnityEngine.Timeline
{
	// Token: 0x02000034 RID: 52
	[Flags]
	[Serializable]
	public enum NotificationFlags : short
	{
		// Token: 0x040000D5 RID: 213
		TriggerInEditMode = 1,
		// Token: 0x040000D6 RID: 214
		Retroactive = 2,
		// Token: 0x040000D7 RID: 215
		TriggerOnce = 4
	}
}
