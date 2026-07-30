using System;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x02000130 RID: 304
	[Flags]
	[Obsolete("For data migration")]
	internal enum ObsoleteLightLoopSettingsOverrides
	{
		// Token: 0x04000E0A RID: 3594
		FptlForForwardOpaque = 1,
		// Token: 0x04000E0B RID: 3595
		BigTilePrepass = 2,
		// Token: 0x04000E0C RID: 3596
		ComputeLightEvaluation = 4,
		// Token: 0x04000E0D RID: 3597
		ComputeLightVariants = 8,
		// Token: 0x04000E0E RID: 3598
		ComputeMaterialVariants = 16,
		// Token: 0x04000E0F RID: 3599
		TileAndCluster = 32
	}
}
