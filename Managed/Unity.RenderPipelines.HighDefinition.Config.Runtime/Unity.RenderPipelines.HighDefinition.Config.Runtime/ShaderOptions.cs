using System;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x02000003 RID: 3
	[GenerateHLSL(PackingRules.Exact, true, false, false, 1, false, false)]
	public enum ShaderOptions
	{
		// Token: 0x04000006 RID: 6
		CameraRelativeRendering = 1,
		// Token: 0x04000007 RID: 7
		PreExposition = 1,
		// Token: 0x04000008 RID: 8
		PrecomputedAtmosphericAttenuation = 0,
		// Token: 0x04000009 RID: 9
		Raytracing = 0,
		// Token: 0x0400000A RID: 10
		XrMaxViews = 2,
		// Token: 0x0400000B RID: 11
		AreaLights = 1,
		// Token: 0x0400000C RID: 12
		DeferredShadowFiltering = 1,
		// Token: 0x0400000D RID: 13
		BarnDoor = 0
	}
}
