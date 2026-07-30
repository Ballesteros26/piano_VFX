using System;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x0200015F RID: 351
	public class BuiltinSkyParameters
	{
		// Token: 0x04000FB4 RID: 4020
		public HDCamera hdCamera;

		// Token: 0x04000FB5 RID: 4021
		public Matrix4x4 pixelCoordToViewDirMatrix;

		// Token: 0x04000FB6 RID: 4022
		public Vector3 worldSpaceCameraPos;

		// Token: 0x04000FB7 RID: 4023
		public Matrix4x4 viewMatrix;

		// Token: 0x04000FB8 RID: 4024
		public Vector4 screenSize;

		// Token: 0x04000FB9 RID: 4025
		public CommandBuffer commandBuffer;

		// Token: 0x04000FBA RID: 4026
		public Light sunLight;

		// Token: 0x04000FBB RID: 4027
		public RTHandle colorBuffer;

		// Token: 0x04000FBC RID: 4028
		public RTHandle depthBuffer;

		// Token: 0x04000FBD RID: 4029
		public int frameIndex;

		// Token: 0x04000FBE RID: 4030
		public SkySettings skySettings;

		// Token: 0x04000FBF RID: 4031
		public DebugDisplaySettings debugSettings;

		// Token: 0x04000FC0 RID: 4032
		public static RenderTargetIdentifier nullRT = -1;
	}
}
