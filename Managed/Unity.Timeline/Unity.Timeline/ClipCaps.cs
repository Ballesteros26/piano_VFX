using System;

namespace UnityEngine.Timeline
{
	// Token: 0x02000016 RID: 22
	[Flags]
	public enum ClipCaps
	{
		// Token: 0x0400008F RID: 143
		None = 0,
		// Token: 0x04000090 RID: 144
		Looping = 1,
		// Token: 0x04000091 RID: 145
		Extrapolation = 2,
		// Token: 0x04000092 RID: 146
		ClipIn = 4,
		// Token: 0x04000093 RID: 147
		SpeedMultiplier = 8,
		// Token: 0x04000094 RID: 148
		Blending = 16,
		// Token: 0x04000095 RID: 149
		All = -1
	}
}
