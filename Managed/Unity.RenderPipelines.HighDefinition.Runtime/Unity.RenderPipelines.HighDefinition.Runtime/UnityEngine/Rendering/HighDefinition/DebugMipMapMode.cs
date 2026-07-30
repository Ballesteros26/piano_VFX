using System;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x02000034 RID: 52
	[GenerateHLSL(PackingRules.Exact, true, false, false, 1, false, false)]
	public enum DebugMipMapMode
	{
		// Token: 0x04000157 RID: 343
		None,
		// Token: 0x04000158 RID: 344
		MipRatio,
		// Token: 0x04000159 RID: 345
		MipCount,
		// Token: 0x0400015A RID: 346
		MipCountReduction,
		// Token: 0x0400015B RID: 347
		StreamingMipBudget,
		// Token: 0x0400015C RID: 348
		StreamingMip
	}
}
