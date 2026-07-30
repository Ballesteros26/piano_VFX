using System;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x02000097 RID: 151
	internal class HDShadowResolutionRequest
	{
		// Token: 0x060005EA RID: 1514 RVA: 0x00032026 File Offset: 0x00030226
		public HDShadowResolutionRequest ShallowCopy()
		{
			return (HDShadowResolutionRequest)base.MemberwiseClone();
		}

		// Token: 0x04000637 RID: 1591
		public Rect atlasViewport;

		// Token: 0x04000638 RID: 1592
		public Vector2 resolution;

		// Token: 0x04000639 RID: 1593
		public ShadowMapType shadowMapType;

		// Token: 0x0400063A RID: 1594
		public int lightID;

		// Token: 0x0400063B RID: 1595
		public int indexInLight;

		// Token: 0x0400063C RID: 1596
		public int lastFrameActive;

		// Token: 0x0400063D RID: 1597
		public bool emptyRequest;

		// Token: 0x0400063E RID: 1598
		public bool hasBeenStoredInCachedList;
	}
}
