using System;

namespace UnityEngine.Experimental.GlobalIllumination
{
	// Token: 0x020003B9 RID: 953
	public struct DirectionalLight
	{
		// Token: 0x04000BE4 RID: 3044
		public int instanceID;

		// Token: 0x04000BE5 RID: 3045
		public bool shadow;

		// Token: 0x04000BE6 RID: 3046
		public LightMode mode;

		// Token: 0x04000BE7 RID: 3047
		public Vector3 position;

		// Token: 0x04000BE8 RID: 3048
		public Quaternion orientation;

		// Token: 0x04000BE9 RID: 3049
		public LinearColor color;

		// Token: 0x04000BEA RID: 3050
		public LinearColor indirectColor;

		// Token: 0x04000BEB RID: 3051
		public float penumbraWidthRadian;

		// Token: 0x04000BEC RID: 3052
		[Obsolete("Directional lights support cookies now. In order to position the cookie projection in the world, a position and full orientation are necessary. Use the position and orientation members instead of the direction parameter.", true)]
		public Vector3 direction;
	}
}
