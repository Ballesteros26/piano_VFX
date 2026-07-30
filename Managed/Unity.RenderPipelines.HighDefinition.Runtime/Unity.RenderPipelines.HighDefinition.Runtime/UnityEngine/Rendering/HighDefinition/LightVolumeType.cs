using System;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x02000068 RID: 104
	[GenerateHLSL(PackingRules.Exact, true, false, false, 1, false, false)]
	internal enum LightVolumeType
	{
		// Token: 0x0400034B RID: 843
		Cone,
		// Token: 0x0400034C RID: 844
		Sphere,
		// Token: 0x0400034D RID: 845
		Box,
		// Token: 0x0400034E RID: 846
		Count
	}
}
