using System;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x020000C5 RID: 197
	internal class StandardLit
	{
		// Token: 0x0200024B RID: 587
		[GenerateHLSL(PackingRules.Exact, true, false, false, 1, false, false)]
		public struct StandardBSDFData
		{
			// Token: 0x04001577 RID: 5495
			public Vector3 baseColor;

			// Token: 0x04001578 RID: 5496
			public float specularOcclusion;

			// Token: 0x04001579 RID: 5497
			public Vector3 normalWS;

			// Token: 0x0400157A RID: 5498
			public float perceptualRoughness;

			// Token: 0x0400157B RID: 5499
			public Vector3 fresnel0;

			// Token: 0x0400157C RID: 5500
			public float coatMask;

			// Token: 0x0400157D RID: 5501
			public Vector3 emissiveAndBaked;

			// Token: 0x0400157E RID: 5502
			public uint renderingLayers;

			// Token: 0x0400157F RID: 5503
			public Vector4 shadowMasks;

			// Token: 0x04001580 RID: 5504
			public uint isUnlit;
		}
	}
}
