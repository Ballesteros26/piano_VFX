using System;

namespace UnityEngine.Playables
{
	// Token: 0x020003A7 RID: 935
	public enum PlayState
	{
		// Token: 0x04000BA8 RID: 2984
		Paused,
		// Token: 0x04000BA9 RID: 2985
		Playing,
		// Token: 0x04000BAA RID: 2986
		[Obsolete("Delayed is obsolete; use a custom ScriptPlayable to implement this feature", false)]
		Delayed
	}
}
