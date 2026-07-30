using System;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x020000A3 RID: 163
	[GenerateHLSL(PackingRules.Exact, true, false, false, 1, false, false)]
	internal struct DensityVolumeEngineData
	{
		// Token: 0x0600063C RID: 1596 RVA: 0x00033E3C File Offset: 0x0003203C
		public static DensityVolumeEngineData GetNeutralValues()
		{
			DensityVolumeEngineData densityVolumeEngineData;
			densityVolumeEngineData.scattering = Vector3.zero;
			densityVolumeEngineData.extinction = 0f;
			densityVolumeEngineData.textureIndex = -1;
			densityVolumeEngineData.textureTiling = Vector3.one;
			densityVolumeEngineData.textureScroll = Vector3.zero;
			densityVolumeEngineData.rcpPosFaceFade = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);
			densityVolumeEngineData.rcpNegFaceFade = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);
			densityVolumeEngineData.invertFade = 0;
			densityVolumeEngineData.rcpDistFadeLen = 0f;
			densityVolumeEngineData.endTimesRcpDistFadeLen = 1f;
			return densityVolumeEngineData;
		}

		// Token: 0x0400068C RID: 1676
		public Vector3 scattering;

		// Token: 0x0400068D RID: 1677
		public float extinction;

		// Token: 0x0400068E RID: 1678
		public Vector3 textureTiling;

		// Token: 0x0400068F RID: 1679
		public int textureIndex;

		// Token: 0x04000690 RID: 1680
		public Vector3 textureScroll;

		// Token: 0x04000691 RID: 1681
		public int invertFade;

		// Token: 0x04000692 RID: 1682
		public Vector3 rcpPosFaceFade;

		// Token: 0x04000693 RID: 1683
		public float rcpDistFadeLen;

		// Token: 0x04000694 RID: 1684
		public Vector3 rcpNegFaceFade;

		// Token: 0x04000695 RID: 1685
		public float endTimesRcpDistFadeLen;
	}
}
