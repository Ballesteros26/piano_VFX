using System;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x0200002F RID: 47
	[GenerateHLSL(PackingRules.Exact, true, false, false, 1, false, false)]
	[Flags]
	public enum DebugLightFilterMode
	{
		// Token: 0x04000102 RID: 258
		None = 0,
		// Token: 0x04000103 RID: 259
		DirectDirectional = 1,
		// Token: 0x04000104 RID: 260
		DirectPunctual = 2,
		// Token: 0x04000105 RID: 261
		DirectRectangle = 4,
		// Token: 0x04000106 RID: 262
		DirectTube = 8,
		// Token: 0x04000107 RID: 263
		DirectSpotCone = 16,
		// Token: 0x04000108 RID: 264
		DirectSpotPyramid = 32,
		// Token: 0x04000109 RID: 265
		DirectSpotBox = 64,
		// Token: 0x0400010A RID: 266
		IndirectReflectionProbe = 128,
		// Token: 0x0400010B RID: 267
		IndirectPlanarProbe = 256
	}
}
