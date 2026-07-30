using System;
using System.Collections.Generic;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x02000113 RID: 275
	internal class HDRayTracingLights
	{
		// Token: 0x04000D64 RID: 3428
		public List<HDAdditionalLightData> hdPointLightArray = new List<HDAdditionalLightData>();

		// Token: 0x04000D65 RID: 3429
		public List<HDAdditionalLightData> hdLineLightArray = new List<HDAdditionalLightData>();

		// Token: 0x04000D66 RID: 3430
		public List<HDAdditionalLightData> hdRectLightArray = new List<HDAdditionalLightData>();

		// Token: 0x04000D67 RID: 3431
		public List<HDAdditionalLightData> hdLightArray = new List<HDAdditionalLightData>();

		// Token: 0x04000D68 RID: 3432
		public List<HDAdditionalLightData> hdDirectionalLightArray = new List<HDAdditionalLightData>();

		// Token: 0x04000D69 RID: 3433
		public List<HDProbe> reflectionProbeArray = new List<HDProbe>();

		// Token: 0x04000D6A RID: 3434
		public int lightCount;
	}
}
