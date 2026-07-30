using System;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x020000B4 RID: 180
	[GenerateHLSL(PackingRules.Exact, true, false, false, 1, false, false)]
	internal class DiffusionProfileConstants
	{
		// Token: 0x040006FE RID: 1790
		public const int DIFFUSION_PROFILE_COUNT = 16;

		// Token: 0x040006FF RID: 1791
		public const int DIFFUSION_PROFILE_NEUTRAL_ID = 0;

		// Token: 0x04000700 RID: 1792
		public const int SSS_N_SAMPLES_NEAR_FIELD = 55;

		// Token: 0x04000701 RID: 1793
		public const int SSS_N_SAMPLES_FAR_FIELD = 21;

		// Token: 0x04000702 RID: 1794
		public const int SSS_LOD_THRESHOLD = 4;
	}
}
