using System;

namespace UnityEngine.Rendering
{
	// Token: 0x0200036C RID: 876
	[Flags]
	public enum PerObjectData
	{
		// Token: 0x04000AB3 RID: 2739
		None = 0,
		// Token: 0x04000AB4 RID: 2740
		LightProbe = 1,
		// Token: 0x04000AB5 RID: 2741
		ReflectionProbes = 2,
		// Token: 0x04000AB6 RID: 2742
		LightProbeProxyVolume = 4,
		// Token: 0x04000AB7 RID: 2743
		Lightmaps = 8,
		// Token: 0x04000AB8 RID: 2744
		LightData = 16,
		// Token: 0x04000AB9 RID: 2745
		MotionVectors = 32,
		// Token: 0x04000ABA RID: 2746
		LightIndices = 64,
		// Token: 0x04000ABB RID: 2747
		ReflectionProbeData = 128,
		// Token: 0x04000ABC RID: 2748
		OcclusionProbe = 256,
		// Token: 0x04000ABD RID: 2749
		OcclusionProbeProxyVolume = 512,
		// Token: 0x04000ABE RID: 2750
		ShadowMask = 1024
	}
}
