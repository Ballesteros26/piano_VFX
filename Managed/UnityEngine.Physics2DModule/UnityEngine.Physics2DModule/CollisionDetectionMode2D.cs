using System;
using System.ComponentModel;

namespace UnityEngine
{
	// Token: 0x0200000A RID: 10
	public enum CollisionDetectionMode2D
	{
		// Token: 0x0400001D RID: 29
		[EditorBrowsable(1)]
		[Obsolete("Enum member CollisionDetectionMode2D.None has been deprecated. Use CollisionDetectionMode2D.Discrete instead (UnityUpgradable) -> Discrete", true)]
		None,
		// Token: 0x0400001E RID: 30
		Discrete = 0,
		// Token: 0x0400001F RID: 31
		Continuous
	}
}
