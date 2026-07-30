using System;
using System.ComponentModel;

namespace UnityEngine
{
	// Token: 0x02000005 RID: 5
	public enum JointProjectionMode
	{
		// Token: 0x04000017 RID: 23
		None,
		// Token: 0x04000018 RID: 24
		PositionAndRotation,
		// Token: 0x04000019 RID: 25
		[EditorBrowsable(1)]
		[Obsolete("JointProjectionMode.PositionOnly is no longer supported", true)]
		PositionOnly
	}
}
