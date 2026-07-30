using System;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x02000106 RID: 262
	[GenerateHLSL(PackingRules.Exact, true, false, false, 1, false, false)]
	internal enum StencilUsage
	{
		// Token: 0x040009E7 RID: 2535
		Clear,
		// Token: 0x040009E8 RID: 2536
		RequiresDeferredLighting = 2,
		// Token: 0x040009E9 RID: 2537
		SubsurfaceScattering = 4,
		// Token: 0x040009EA RID: 2538
		TraceReflectionRay = 8,
		// Token: 0x040009EB RID: 2539
		Decals = 16,
		// Token: 0x040009EC RID: 2540
		ObjectMotionVector = 32,
		// Token: 0x040009ED RID: 2541
		ExcludeFromTAA = 2,
		// Token: 0x040009EE RID: 2542
		DistortionVectors = 4,
		// Token: 0x040009EF RID: 2543
		SMAA = 4,
		// Token: 0x040009F0 RID: 2544
		AfterOpaqueReservedBits = 56,
		// Token: 0x040009F1 RID: 2545
		UserBit0 = 64,
		// Token: 0x040009F2 RID: 2546
		UserBit1 = 128,
		// Token: 0x040009F3 RID: 2547
		HDRPReservedBits = 63
	}
}
