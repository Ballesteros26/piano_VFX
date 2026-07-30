using System;

namespace UnityEngine.Rendering
{
	// Token: 0x02000364 RID: 868
	internal struct CullingAllocationInfo
	{
		// Token: 0x04000A8E RID: 2702
		public unsafe VisibleLight* visibleLightsPtr;

		// Token: 0x04000A8F RID: 2703
		public unsafe VisibleLight* visibleOffscreenVertexLightsPtr;

		// Token: 0x04000A90 RID: 2704
		public unsafe VisibleReflectionProbe* visibleReflectionProbesPtr;

		// Token: 0x04000A91 RID: 2705
		public int visibleLightCount;

		// Token: 0x04000A92 RID: 2706
		public int visibleOffscreenVertexLightCount;

		// Token: 0x04000A93 RID: 2707
		public int visibleReflectionProbeCount;
	}
}
