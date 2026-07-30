using System;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x0200006C RID: 108
	[GenerateHLSL(PackingRules.Exact, true, false, false, 1, false, false)]
	internal struct SFiniteLightBound
	{
		// Token: 0x04000373 RID: 883
		public Vector3 boxAxisX;

		// Token: 0x04000374 RID: 884
		public Vector3 boxAxisY;

		// Token: 0x04000375 RID: 885
		public Vector3 boxAxisZ;

		// Token: 0x04000376 RID: 886
		public Vector3 center;

		// Token: 0x04000377 RID: 887
		public Vector2 scaleXY;

		// Token: 0x04000378 RID: 888
		public float radius;
	}
}
