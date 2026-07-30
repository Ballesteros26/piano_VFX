using System;

namespace UnityEngine.Rendering
{
	// Token: 0x0200035F RID: 863
	[Flags]
	public enum CullingOptions
	{
		// Token: 0x04000A68 RID: 2664
		None = 0,
		// Token: 0x04000A69 RID: 2665
		ForceEvenIfCameraIsNotActive = 1,
		// Token: 0x04000A6A RID: 2666
		OcclusionCull = 2,
		// Token: 0x04000A6B RID: 2667
		NeedsLighting = 4,
		// Token: 0x04000A6C RID: 2668
		NeedsReflectionProbes = 8,
		// Token: 0x04000A6D RID: 2669
		Stereo = 16,
		// Token: 0x04000A6E RID: 2670
		DisablePerObjectCulling = 32,
		// Token: 0x04000A6F RID: 2671
		ShadowCasters = 64
	}
}
