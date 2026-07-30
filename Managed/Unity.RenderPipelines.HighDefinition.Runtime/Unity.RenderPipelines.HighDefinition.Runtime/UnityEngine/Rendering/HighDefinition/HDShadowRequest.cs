using System;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x02000093 RID: 147
	internal class HDShadowRequest
	{
		// Token: 0x04000604 RID: 1540
		public Matrix4x4 view;

		// Token: 0x04000605 RID: 1541
		public Matrix4x4 deviceProjectionYFlip;

		// Token: 0x04000606 RID: 1542
		public Matrix4x4 deviceProjection;

		// Token: 0x04000607 RID: 1543
		public Matrix4x4 shadowToWorld;

		// Token: 0x04000608 RID: 1544
		public Vector3 position;

		// Token: 0x04000609 RID: 1545
		public Vector4 zBufferParam;

		// Token: 0x0400060A RID: 1546
		public Rect atlasViewport;

		// Token: 0x0400060B RID: 1547
		public bool zClip;

		// Token: 0x0400060C RID: 1548
		public Vector4[] frustumPlanes;

		// Token: 0x0400060D RID: 1549
		public int shadowIndex;

		// Token: 0x0400060E RID: 1550
		public ShadowMapType shadowMapType = ShadowMapType.PunctualAtlas;

		// Token: 0x0400060F RID: 1551
		public int lightIndex;

		// Token: 0x04000610 RID: 1552
		public ShadowSplitData splitData;

		// Token: 0x04000611 RID: 1553
		public float normalBias;

		// Token: 0x04000612 RID: 1554
		public float worldTexelSize;

		// Token: 0x04000613 RID: 1555
		public float slopeBias;

		// Token: 0x04000614 RID: 1556
		public float shadowSoftness;

		// Token: 0x04000615 RID: 1557
		public int blockerSampleCount;

		// Token: 0x04000616 RID: 1558
		public int filterSampleCount;

		// Token: 0x04000617 RID: 1559
		public float minFilterSize;

		// Token: 0x04000618 RID: 1560
		public float kernelSize;

		// Token: 0x04000619 RID: 1561
		public float lightAngle;

		// Token: 0x0400061A RID: 1562
		public float maxDepthBias;

		// Token: 0x0400061B RID: 1563
		public Vector4 evsmParams;

		// Token: 0x0400061C RID: 1564
		public bool shouldUseCachedShadow;

		// Token: 0x0400061D RID: 1565
		public HDShadowData cachedShadowData;
	}
}
