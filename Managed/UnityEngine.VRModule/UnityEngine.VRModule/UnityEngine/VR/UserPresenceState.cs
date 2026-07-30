using System;
using System.ComponentModel;

namespace UnityEngine.VR
{
	// Token: 0x02000023 RID: 35
	[Obsolete("UserPresenceState has been moved.  Use UnityEngine.XR.UserPresenceState instead (UnityUpgradable) -> UnityEngine.XR.UserPresenceState", true)]
	[EditorBrowsable(1)]
	public enum UserPresenceState
	{
		// Token: 0x0400005D RID: 93
		Unsupported = -1,
		// Token: 0x0400005E RID: 94
		NotPresent,
		// Token: 0x0400005F RID: 95
		Present,
		// Token: 0x04000060 RID: 96
		Unknown
	}
}
