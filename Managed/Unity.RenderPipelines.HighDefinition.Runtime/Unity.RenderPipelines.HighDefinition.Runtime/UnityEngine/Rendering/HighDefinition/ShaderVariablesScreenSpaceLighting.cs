using System;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x0200008B RID: 139
	[GenerateHLSL(PackingRules.Exact, true, false, false, 1, false, false, needAccessors = false, omitStructDeclaration = true)]
	internal struct ShaderVariablesScreenSpaceLighting
	{
		// Token: 0x040005AF RID: 1455
		public Vector4 _CameraMotionVectorsSize;

		// Token: 0x040005B0 RID: 1456
		public Vector4 _ColorPyramidScale;

		// Token: 0x040005B1 RID: 1457
		public Vector4 _DepthPyramidScale;

		// Token: 0x040005B2 RID: 1458
		public Vector4 _CameraMotionVectorsScale;

		// Token: 0x040005B3 RID: 1459
		public Vector4 _AmbientOcclusionParam;

		// Token: 0x040005B4 RID: 1460
		public Vector4 _IndirectLightingMultiplier;

		// Token: 0x040005B5 RID: 1461
		public float _SSRefractionInvScreenWeightDistance;
	}
}
