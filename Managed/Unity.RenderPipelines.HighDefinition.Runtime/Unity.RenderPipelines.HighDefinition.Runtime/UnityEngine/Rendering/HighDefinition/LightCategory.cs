using System;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x02000069 RID: 105
	[GenerateHLSL(PackingRules.Exact, true, false, false, 1, false, false)]
	internal enum LightCategory
	{
		// Token: 0x04000350 RID: 848
		Punctual,
		// Token: 0x04000351 RID: 849
		Area,
		// Token: 0x04000352 RID: 850
		Env,
		// Token: 0x04000353 RID: 851
		Decal,
		// Token: 0x04000354 RID: 852
		DensityVolume,
		// Token: 0x04000355 RID: 853
		Count
	}
}
