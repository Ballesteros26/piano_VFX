using System;

namespace UnityEngine.Experimental.GlobalIllumination
{
	// Token: 0x020003BD RID: 957
	public struct DiscLight
	{
		// Token: 0x04000C0E RID: 3086
		public int instanceID;

		// Token: 0x04000C0F RID: 3087
		public bool shadow;

		// Token: 0x04000C10 RID: 3088
		public LightMode mode;

		// Token: 0x04000C11 RID: 3089
		public Vector3 position;

		// Token: 0x04000C12 RID: 3090
		public Quaternion orientation;

		// Token: 0x04000C13 RID: 3091
		public LinearColor color;

		// Token: 0x04000C14 RID: 3092
		public LinearColor indirectColor;

		// Token: 0x04000C15 RID: 3093
		public float range;

		// Token: 0x04000C16 RID: 3094
		public float radius;

		// Token: 0x04000C17 RID: 3095
		public FalloffType falloff;
	}
}
