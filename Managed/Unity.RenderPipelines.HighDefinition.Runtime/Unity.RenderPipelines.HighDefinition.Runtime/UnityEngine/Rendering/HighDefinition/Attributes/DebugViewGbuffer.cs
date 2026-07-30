using System;

namespace UnityEngine.Rendering.HighDefinition.Attributes
{
	// Token: 0x02000184 RID: 388
	[GenerateHLSL(PackingRules.Exact, true, false, false, 1, false, false)]
	public enum DebugViewGbuffer
	{
		// Token: 0x04001075 RID: 4213
		None,
		// Token: 0x04001076 RID: 4214
		Depth = 10,
		// Token: 0x04001077 RID: 4215
		BakeDiffuseLightingWithAlbedoPlusEmissive,
		// Token: 0x04001078 RID: 4216
		BakeShadowMask0,
		// Token: 0x04001079 RID: 4217
		BakeShadowMask1,
		// Token: 0x0400107A RID: 4218
		BakeShadowMask2,
		// Token: 0x0400107B RID: 4219
		BakeShadowMask3
	}
}
