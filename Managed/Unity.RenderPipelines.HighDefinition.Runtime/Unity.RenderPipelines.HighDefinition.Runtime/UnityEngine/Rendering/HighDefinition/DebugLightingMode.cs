using System;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x0200002E RID: 46
	[GenerateHLSL(PackingRules.Exact, true, false, false, 1, false, false)]
	public enum DebugLightingMode
	{
		// Token: 0x040000F7 RID: 247
		None,
		// Token: 0x040000F8 RID: 248
		DiffuseLighting,
		// Token: 0x040000F9 RID: 249
		SpecularLighting,
		// Token: 0x040000FA RID: 250
		LuxMeter,
		// Token: 0x040000FB RID: 251
		LuminanceMeter,
		// Token: 0x040000FC RID: 252
		MatcapView,
		// Token: 0x040000FD RID: 253
		VisualizeCascade,
		// Token: 0x040000FE RID: 254
		VisualizeShadowMasks,
		// Token: 0x040000FF RID: 255
		IndirectDiffuseOcclusion,
		// Token: 0x04000100 RID: 256
		IndirectSpecularOcclusion
	}
}
