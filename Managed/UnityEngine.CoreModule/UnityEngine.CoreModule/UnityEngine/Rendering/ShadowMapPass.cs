using System;

namespace UnityEngine.Rendering
{
	// Token: 0x02000328 RID: 808
	[Flags]
	public enum ShadowMapPass
	{
		// Token: 0x040008F8 RID: 2296
		PointlightPositiveX = 1,
		// Token: 0x040008F9 RID: 2297
		PointlightNegativeX = 2,
		// Token: 0x040008FA RID: 2298
		PointlightPositiveY = 4,
		// Token: 0x040008FB RID: 2299
		PointlightNegativeY = 8,
		// Token: 0x040008FC RID: 2300
		PointlightPositiveZ = 16,
		// Token: 0x040008FD RID: 2301
		PointlightNegativeZ = 32,
		// Token: 0x040008FE RID: 2302
		DirectionalCascade0 = 64,
		// Token: 0x040008FF RID: 2303
		DirectionalCascade1 = 128,
		// Token: 0x04000900 RID: 2304
		DirectionalCascade2 = 256,
		// Token: 0x04000901 RID: 2305
		DirectionalCascade3 = 512,
		// Token: 0x04000902 RID: 2306
		Spotlight = 1024,
		// Token: 0x04000903 RID: 2307
		Pointlight = 63,
		// Token: 0x04000904 RID: 2308
		Directional = 960,
		// Token: 0x04000905 RID: 2309
		All = 2047
	}
}
