using System;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x0200010E RID: 270
	[GenerateHLSL(PackingRules.Exact, false, false, false, 1, false, false)]
	internal struct LightVolume
	{
		// Token: 0x04000D21 RID: 3361
		public int active;

		// Token: 0x04000D22 RID: 3362
		public int shape;

		// Token: 0x04000D23 RID: 3363
		public Vector3 position;

		// Token: 0x04000D24 RID: 3364
		public Vector3 range;

		// Token: 0x04000D25 RID: 3365
		public uint lightType;

		// Token: 0x04000D26 RID: 3366
		public uint lightIndex;
	}
}
