using System;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x02000152 RID: 338
	internal struct XRViewCreateInfo
	{
		// Token: 0x04000F3C RID: 3900
		public Matrix4x4 projMatrix;

		// Token: 0x04000F3D RID: 3901
		public Matrix4x4 viewMatrix;

		// Token: 0x04000F3E RID: 3902
		public Rect viewport;

		// Token: 0x04000F3F RID: 3903
		public int textureArraySlice;
	}
}
