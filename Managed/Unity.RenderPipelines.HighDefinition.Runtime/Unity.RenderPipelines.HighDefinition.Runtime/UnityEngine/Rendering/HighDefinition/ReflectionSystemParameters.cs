using System;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x02000081 RID: 129
	internal struct ReflectionSystemParameters
	{
		// Token: 0x04000553 RID: 1363
		public static ReflectionSystemParameters Default = new ReflectionSystemParameters
		{
			maxPlanarReflectionProbePerCamera = 128,
			maxActivePlanarReflectionProbe = 512,
			planarReflectionProbeSize = 128,
			maxActiveReflectionProbe = 512,
			reflectionProbeSize = 128
		};

		// Token: 0x04000554 RID: 1364
		public int maxPlanarReflectionProbePerCamera;

		// Token: 0x04000555 RID: 1365
		public int maxActivePlanarReflectionProbe;

		// Token: 0x04000556 RID: 1366
		public int planarReflectionProbeSize;

		// Token: 0x04000557 RID: 1367
		public int maxActiveReflectionProbe;

		// Token: 0x04000558 RID: 1368
		public int reflectionProbeSize;
	}
}
