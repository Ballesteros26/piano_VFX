using System;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x02000060 RID: 96
	[GenerateHLSL(PackingRules.Exact, false, false, false, 1, false, false)]
	internal struct EnvLightData
	{
		// Token: 0x040002F6 RID: 758
		public uint lightLayers;

		// Token: 0x040002F7 RID: 759
		public Vector3 capturePositionRWS;

		// Token: 0x040002F8 RID: 760
		public EnvShapeType influenceShapeType;

		// Token: 0x040002F9 RID: 761
		public Vector3 proxyExtents;

		// Token: 0x040002FA RID: 762
		[SurfaceDataAttributes("", false, false, FieldPrecision.Default, precision = FieldPrecision.Real)]
		public float minProjectionDistance;

		// Token: 0x040002FB RID: 763
		public Vector3 proxyPositionRWS;

		// Token: 0x040002FC RID: 764
		public Vector3 proxyForward;

		// Token: 0x040002FD RID: 765
		public Vector3 proxyUp;

		// Token: 0x040002FE RID: 766
		public Vector3 proxyRight;

		// Token: 0x040002FF RID: 767
		public Vector3 influencePositionRWS;

		// Token: 0x04000300 RID: 768
		public Vector3 influenceForward;

		// Token: 0x04000301 RID: 769
		public Vector3 influenceUp;

		// Token: 0x04000302 RID: 770
		public Vector3 influenceRight;

		// Token: 0x04000303 RID: 771
		public Vector3 influenceExtents;

		// Token: 0x04000304 RID: 772
		public float unused00;

		// Token: 0x04000305 RID: 773
		public Vector3 blendDistancePositive;

		// Token: 0x04000306 RID: 774
		public Vector3 blendDistanceNegative;

		// Token: 0x04000307 RID: 775
		public Vector3 blendNormalDistancePositive;

		// Token: 0x04000308 RID: 776
		public Vector3 blendNormalDistanceNegative;

		// Token: 0x04000309 RID: 777
		[SurfaceDataAttributes("", false, false, FieldPrecision.Default, precision = FieldPrecision.Real)]
		public Vector3 boxSideFadePositive;

		// Token: 0x0400030A RID: 778
		[SurfaceDataAttributes("", false, false, FieldPrecision.Default, precision = FieldPrecision.Real)]
		public Vector3 boxSideFadeNegative;

		// Token: 0x0400030B RID: 779
		public float weight;

		// Token: 0x0400030C RID: 780
		public float multiplier;

		// Token: 0x0400030D RID: 781
		public float rangeCompressionFactorCompensation;

		// Token: 0x0400030E RID: 782
		public int envIndex;
	}
}
