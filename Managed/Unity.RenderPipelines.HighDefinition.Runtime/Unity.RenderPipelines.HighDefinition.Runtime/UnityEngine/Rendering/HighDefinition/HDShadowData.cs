using System;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x02000091 RID: 145
	[GenerateHLSL(PackingRules.Exact, true, false, false, 1, false, false, needAccessors = false)]
	internal struct HDShadowData
	{
		// Token: 0x040005F3 RID: 1523
		public Vector3 rot0;

		// Token: 0x040005F4 RID: 1524
		public Vector3 rot1;

		// Token: 0x040005F5 RID: 1525
		public Vector3 rot2;

		// Token: 0x040005F6 RID: 1526
		public Vector3 pos;

		// Token: 0x040005F7 RID: 1527
		public Vector4 proj;

		// Token: 0x040005F8 RID: 1528
		public Vector2 atlasOffset;

		// Token: 0x040005F9 RID: 1529
		public float worldTexelSize;

		// Token: 0x040005FA RID: 1530
		public float normalBias;

		// Token: 0x040005FB RID: 1531
		[SurfaceDataAttributes("", false, false, FieldPrecision.Default, precision = FieldPrecision.Real)]
		public Vector4 zBufferParam;

		// Token: 0x040005FC RID: 1532
		public Vector4 shadowMapSize;

		// Token: 0x040005FD RID: 1533
		[SurfaceDataAttributes("", false, false, FieldPrecision.Default, precision = FieldPrecision.Real)]
		public Vector4 shadowFilterParams0;

		// Token: 0x040005FE RID: 1534
		public Vector3 cacheTranslationDelta;

		// Token: 0x040005FF RID: 1535
		public float _pad0;

		// Token: 0x04000600 RID: 1536
		public Matrix4x4 shadowToWorld;
	}
}
