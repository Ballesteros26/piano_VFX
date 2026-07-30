using System;

namespace UnityEngine
{
	// Token: 0x02000130 RID: 304
	[Flags]
	public enum MaterialGlobalIlluminationFlags
	{
		// Token: 0x040003E7 RID: 999
		None = 0,
		// Token: 0x040003E8 RID: 1000
		RealtimeEmissive = 1,
		// Token: 0x040003E9 RID: 1001
		BakedEmissive = 2,
		// Token: 0x040003EA RID: 1002
		EmissiveIsBlack = 4,
		// Token: 0x040003EB RID: 1003
		AnyEmissive = 3
	}
}
