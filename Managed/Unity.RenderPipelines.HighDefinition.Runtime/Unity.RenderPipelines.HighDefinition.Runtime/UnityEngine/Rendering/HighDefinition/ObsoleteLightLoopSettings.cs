using System;
using UnityEngine.Serialization;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x02000132 RID: 306
	[Obsolete("For data migration")]
	[Serializable]
	internal class ObsoleteLightLoopSettings
	{
		// Token: 0x04000E30 RID: 3632
		public ObsoleteLightLoopSettingsOverrides overrides;

		// Token: 0x04000E31 RID: 3633
		[FormerlySerializedAs("enableTileAndCluster")]
		public bool enableDeferredTileAndCluster;

		// Token: 0x04000E32 RID: 3634
		public bool enableComputeLightEvaluation;

		// Token: 0x04000E33 RID: 3635
		public bool enableComputeLightVariants;

		// Token: 0x04000E34 RID: 3636
		public bool enableComputeMaterialVariants;

		// Token: 0x04000E35 RID: 3637
		public bool enableFptlForForwardOpaque;

		// Token: 0x04000E36 RID: 3638
		public bool enableBigTilePrepass;

		// Token: 0x04000E37 RID: 3639
		public bool isFptlEnabled;
	}
}
