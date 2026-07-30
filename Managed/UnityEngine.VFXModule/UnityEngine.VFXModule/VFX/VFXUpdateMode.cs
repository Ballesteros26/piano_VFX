using System;

namespace UnityEngine.VFX
{
	// Token: 0x02000009 RID: 9
	[Flags]
	internal enum VFXUpdateMode
	{
		// Token: 0x040000BF RID: 191
		FixedDeltaTime = 0,
		// Token: 0x040000C0 RID: 192
		DeltaTime = 1,
		// Token: 0x040000C1 RID: 193
		IgnoreTimeScale = 2,
		// Token: 0x040000C2 RID: 194
		ExactFixedTimeStep = 4,
		// Token: 0x040000C3 RID: 195
		DeltaTimeAndIgnoreTimeScale = 3,
		// Token: 0x040000C4 RID: 196
		FixedDeltaAndExactTime = 4,
		// Token: 0x040000C5 RID: 197
		FixedDeltaAndExactTimeAndIgnoreTimeScale = 6
	}
}
