using System;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x02000110 RID: 272
	[GenerateHLSL(PackingRules.Exact, true, false, false, 1, false, false)]
	internal enum RayTracingRendererFlag
	{
		// Token: 0x04000D4A RID: 3402
		Opaque = 1,
		// Token: 0x04000D4B RID: 3403
		CastShadowTransparent,
		// Token: 0x04000D4C RID: 3404
		CastShadowOpaque = 4,
		// Token: 0x04000D4D RID: 3405
		CastShadow = 6,
		// Token: 0x04000D4E RID: 3406
		AmbientOcclusion = 8,
		// Token: 0x04000D4F RID: 3407
		Reflection = 16,
		// Token: 0x04000D50 RID: 3408
		GlobalIllumination = 32,
		// Token: 0x04000D51 RID: 3409
		RecursiveRendering = 64,
		// Token: 0x04000D52 RID: 3410
		PathTracing = 128
	}
}
