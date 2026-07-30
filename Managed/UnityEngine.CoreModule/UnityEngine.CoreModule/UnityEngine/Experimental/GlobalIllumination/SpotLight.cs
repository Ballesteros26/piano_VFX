using System;

namespace UnityEngine.Experimental.GlobalIllumination
{
	// Token: 0x020003BB RID: 955
	public struct SpotLight
	{
		// Token: 0x04000BF6 RID: 3062
		public int instanceID;

		// Token: 0x04000BF7 RID: 3063
		public bool shadow;

		// Token: 0x04000BF8 RID: 3064
		public LightMode mode;

		// Token: 0x04000BF9 RID: 3065
		public Vector3 position;

		// Token: 0x04000BFA RID: 3066
		public Quaternion orientation;

		// Token: 0x04000BFB RID: 3067
		public LinearColor color;

		// Token: 0x04000BFC RID: 3068
		public LinearColor indirectColor;

		// Token: 0x04000BFD RID: 3069
		public float range;

		// Token: 0x04000BFE RID: 3070
		public float sphereRadius;

		// Token: 0x04000BFF RID: 3071
		public float coneAngle;

		// Token: 0x04000C00 RID: 3072
		public float innerConeAngle;

		// Token: 0x04000C01 RID: 3073
		public FalloffType falloff;

		// Token: 0x04000C02 RID: 3074
		public AngularFalloffType angularFalloff;
	}
}
