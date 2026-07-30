using System;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x02000058 RID: 88
	[GenerateHLSL(PackingRules.Exact, true, false, false, 1, false, false)]
	internal enum GPULightType
	{
		// Token: 0x0400029F RID: 671
		Directional,
		// Token: 0x040002A0 RID: 672
		Point,
		// Token: 0x040002A1 RID: 673
		Spot,
		// Token: 0x040002A2 RID: 674
		ProjectorPyramid,
		// Token: 0x040002A3 RID: 675
		ProjectorBox,
		// Token: 0x040002A4 RID: 676
		Tube,
		// Token: 0x040002A5 RID: 677
		Rectangle,
		// Token: 0x040002A6 RID: 678
		Disc
	}
}
