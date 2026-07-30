using System;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x020000AE RID: 174
	[GenerateHLSL(PackingRules.Exact, false, false, false, 1, false, false)]
	internal struct DecalData
	{
		// Token: 0x040006B8 RID: 1720
		public Matrix4x4 worldToDecal;

		// Token: 0x040006B9 RID: 1721
		public Matrix4x4 normalToWorld;

		// Token: 0x040006BA RID: 1722
		public Vector4 diffuseScaleBias;

		// Token: 0x040006BB RID: 1723
		public Vector4 normalScaleBias;

		// Token: 0x040006BC RID: 1724
		public Vector4 maskScaleBias;

		// Token: 0x040006BD RID: 1725
		public Vector4 baseColor;

		// Token: 0x040006BE RID: 1726
		public Vector4 remappingAOS;

		// Token: 0x040006BF RID: 1727
		public Vector4 scalingMAB;

		// Token: 0x040006C0 RID: 1728
		public Vector3 blendParams;
	}
}
