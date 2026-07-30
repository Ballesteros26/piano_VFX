using System;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x02000031 RID: 49
	[GenerateHLSL(PackingRules.Exact, true, false, false, 1, false, false)]
	public enum ShadowMapDebugMode
	{
		// Token: 0x0400010D RID: 269
		None,
		// Token: 0x0400010E RID: 270
		VisualizePunctualLightAtlas,
		// Token: 0x0400010F RID: 271
		VisualizeDirectionalLightAtlas,
		// Token: 0x04000110 RID: 272
		VisualizeAreaLightAtlas,
		// Token: 0x04000111 RID: 273
		VisualizeShadowMap,
		// Token: 0x04000112 RID: 274
		SingleShadow
	}
}
