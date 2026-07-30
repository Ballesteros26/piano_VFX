using System;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x0200006A RID: 106
	[GenerateHLSL(PackingRules.Exact, true, false, false, 1, false, false)]
	internal enum LightFeatureFlags
	{
		// Token: 0x04000357 RID: 855
		Punctual = 4096,
		// Token: 0x04000358 RID: 856
		Area = 8192,
		// Token: 0x04000359 RID: 857
		Directional = 16384,
		// Token: 0x0400035A RID: 858
		Env = 32768,
		// Token: 0x0400035B RID: 859
		Sky = 65536,
		// Token: 0x0400035C RID: 860
		SSRefraction = 131072,
		// Token: 0x0400035D RID: 861
		SSReflection = 262144
	}
}
