using System;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x020000F5 RID: 245
	[GenerateHLSL(PackingRules.Exact, true, false, false, 1, false, false)]
	[Flags]
	internal enum UberPostFeatureFlags
	{
		// Token: 0x04000855 RID: 2133
		None = 0,
		// Token: 0x04000856 RID: 2134
		ChromaticAberration = 1,
		// Token: 0x04000857 RID: 2135
		Vignette = 2,
		// Token: 0x04000858 RID: 2136
		LensDistortion = 4,
		// Token: 0x04000859 RID: 2137
		EnableAlpha = 8
	}
}
