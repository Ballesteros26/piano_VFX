using System;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x02000037 RID: 55
	[GenerateHLSL(PackingRules.Exact, true, false, false, 1, false, false)]
	public enum RayCountValues
	{
		// Token: 0x0400016A RID: 362
		AmbientOcclusion,
		// Token: 0x0400016B RID: 363
		ShadowDirectional,
		// Token: 0x0400016C RID: 364
		ShadowPointSpot,
		// Token: 0x0400016D RID: 365
		ShadowAreaLight,
		// Token: 0x0400016E RID: 366
		DiffuseGI_Forward,
		// Token: 0x0400016F RID: 367
		DiffuseGI_Deferred,
		// Token: 0x04000170 RID: 368
		ReflectionForward,
		// Token: 0x04000171 RID: 369
		ReflectionDeferred,
		// Token: 0x04000172 RID: 370
		Recursive,
		// Token: 0x04000173 RID: 371
		Count,
		// Token: 0x04000174 RID: 372
		Total
	}
}
