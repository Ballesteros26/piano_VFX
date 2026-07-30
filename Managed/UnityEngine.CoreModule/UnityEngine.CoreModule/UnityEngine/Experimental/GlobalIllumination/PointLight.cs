using System;

namespace UnityEngine.Experimental.GlobalIllumination
{
	// Token: 0x020003BA RID: 954
	public struct PointLight
	{
		// Token: 0x04000BED RID: 3053
		public int instanceID;

		// Token: 0x04000BEE RID: 3054
		public bool shadow;

		// Token: 0x04000BEF RID: 3055
		public LightMode mode;

		// Token: 0x04000BF0 RID: 3056
		public Vector3 position;

		// Token: 0x04000BF1 RID: 3057
		public LinearColor color;

		// Token: 0x04000BF2 RID: 3058
		public LinearColor indirectColor;

		// Token: 0x04000BF3 RID: 3059
		public float range;

		// Token: 0x04000BF4 RID: 3060
		public float sphereRadius;

		// Token: 0x04000BF5 RID: 3061
		public FalloffType falloff;
	}
}
