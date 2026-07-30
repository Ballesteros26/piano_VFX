using System;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x02000071 RID: 113
	internal struct ProcessedLightData
	{
		// Token: 0x04000397 RID: 919
		public HDAdditionalLightData additionalLightData;

		// Token: 0x04000398 RID: 920
		public HDLightType lightType;

		// Token: 0x04000399 RID: 921
		public LightCategory lightCategory;

		// Token: 0x0400039A RID: 922
		public GPULightType gpuLightType;

		// Token: 0x0400039B RID: 923
		public LightVolumeType lightVolumeType;

		// Token: 0x0400039C RID: 924
		public float distanceToCamera;

		// Token: 0x0400039D RID: 925
		public float lightDistanceFade;

		// Token: 0x0400039E RID: 926
		public bool isBakedShadowMask;
	}
}
