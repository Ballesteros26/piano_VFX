using System;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x02000059 RID: 89
	internal static class GPULightTypeExtension
	{
		// Token: 0x060002B9 RID: 697 RVA: 0x0000F76B File Offset: 0x0000D96B
		public static bool IsAreaLight(this GPULightType lightType)
		{
			return lightType == GPULightType.Rectangle || lightType == GPULightType.Tube;
		}

		// Token: 0x060002BA RID: 698 RVA: 0x0000F777 File Offset: 0x0000D977
		public static bool IsSpot(this GPULightType lightType)
		{
			return lightType == GPULightType.Spot || lightType == GPULightType.ProjectorBox || lightType == GPULightType.ProjectorPyramid;
		}
	}
}
