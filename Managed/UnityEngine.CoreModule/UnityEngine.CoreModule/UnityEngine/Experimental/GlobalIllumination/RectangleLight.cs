using System;

namespace UnityEngine.Experimental.GlobalIllumination
{
	// Token: 0x020003BC RID: 956
	public struct RectangleLight
	{
		// Token: 0x04000C03 RID: 3075
		public int instanceID;

		// Token: 0x04000C04 RID: 3076
		public bool shadow;

		// Token: 0x04000C05 RID: 3077
		public LightMode mode;

		// Token: 0x04000C06 RID: 3078
		public Vector3 position;

		// Token: 0x04000C07 RID: 3079
		public Quaternion orientation;

		// Token: 0x04000C08 RID: 3080
		public LinearColor color;

		// Token: 0x04000C09 RID: 3081
		public LinearColor indirectColor;

		// Token: 0x04000C0A RID: 3082
		public float range;

		// Token: 0x04000C0B RID: 3083
		public float width;

		// Token: 0x04000C0C RID: 3084
		public float height;

		// Token: 0x04000C0D RID: 3085
		public FalloffType falloff;
	}
}
