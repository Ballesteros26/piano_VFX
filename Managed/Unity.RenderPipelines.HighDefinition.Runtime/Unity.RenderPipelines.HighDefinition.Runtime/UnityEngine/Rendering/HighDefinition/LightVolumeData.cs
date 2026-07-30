using System;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x0200006D RID: 109
	[GenerateHLSL(PackingRules.Exact, true, false, false, 1, false, false)]
	internal struct LightVolumeData
	{
		// Token: 0x04000379 RID: 889
		public Vector3 lightPos;

		// Token: 0x0400037A RID: 890
		public uint lightVolume;

		// Token: 0x0400037B RID: 891
		public Vector3 lightAxisX;

		// Token: 0x0400037C RID: 892
		public uint lightCategory;

		// Token: 0x0400037D RID: 893
		public Vector3 lightAxisY;

		// Token: 0x0400037E RID: 894
		public float radiusSq;

		// Token: 0x0400037F RID: 895
		public Vector3 lightAxisZ;

		// Token: 0x04000380 RID: 896
		public float cotan;

		// Token: 0x04000381 RID: 897
		public Vector3 boxInnerDist;

		// Token: 0x04000382 RID: 898
		public uint featureFlags;

		// Token: 0x04000383 RID: 899
		public Vector3 boxInvRange;

		// Token: 0x04000384 RID: 900
		public float unused2;
	}
}
