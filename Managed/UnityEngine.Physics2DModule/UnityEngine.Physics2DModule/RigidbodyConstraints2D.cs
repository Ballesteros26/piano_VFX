using System;

namespace UnityEngine
{
	// Token: 0x02000007 RID: 7
	[Flags]
	public enum RigidbodyConstraints2D
	{
		// Token: 0x0400000E RID: 14
		None = 0,
		// Token: 0x0400000F RID: 15
		FreezePositionX = 1,
		// Token: 0x04000010 RID: 16
		FreezePositionY = 2,
		// Token: 0x04000011 RID: 17
		FreezeRotation = 4,
		// Token: 0x04000012 RID: 18
		FreezePosition = 3,
		// Token: 0x04000013 RID: 19
		FreezeAll = 7
	}
}
