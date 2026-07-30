using System;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x02000126 RID: 294
	[GenerateHLSL(PackingRules.Exact, true, false, false, 1, false, false)]
	public enum CustomPassInjectionPoint
	{
		// Token: 0x04000DAF RID: 3503
		BeforeRendering,
		// Token: 0x04000DB0 RID: 3504
		AfterOpaqueDepthAndNormal = 5,
		// Token: 0x04000DB1 RID: 3505
		BeforePreRefraction = 4,
		// Token: 0x04000DB2 RID: 3506
		BeforeTransparent = 1,
		// Token: 0x04000DB3 RID: 3507
		BeforePostProcess,
		// Token: 0x04000DB4 RID: 3508
		AfterPostProcess
	}
}
