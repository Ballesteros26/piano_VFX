using System;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000101 RID: 257
	[NativeHeader("Runtime/Camera/SharedLightData.h")]
	public struct LightBakingOutput
	{
		// Token: 0x040002B0 RID: 688
		public int probeOcclusionLightIndex;

		// Token: 0x040002B1 RID: 689
		public int occlusionMaskChannel;

		// Token: 0x040002B2 RID: 690
		[NativeName("lightmapBakeMode.lightmapBakeType")]
		public LightmapBakeType lightmapBakeType;

		// Token: 0x040002B3 RID: 691
		[NativeName("lightmapBakeMode.mixedLightingMode")]
		public MixedLightingMode mixedLightingMode;

		// Token: 0x040002B4 RID: 692
		public bool isBaked;
	}
}
