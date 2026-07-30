using System;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x0200006B RID: 107
	[GenerateHLSL(PackingRules.Exact, true, false, false, 1, false, false)]
	internal class LightDefinitions
	{
		// Token: 0x0400035E RID: 862
		public static int s_MaxNrBigTileLightsPlusOne = 512;

		// Token: 0x0400035F RID: 863
		public static float s_ViewportScaleZ = 1f;

		// Token: 0x04000360 RID: 864
		public static int s_UseLeftHandCameraSpace = 1;

		// Token: 0x04000361 RID: 865
		public static int s_TileSizeFptl = 16;

		// Token: 0x04000362 RID: 866
		public static int s_TileSizeClustered = 32;

		// Token: 0x04000363 RID: 867
		public static int s_TileSizeBigTile = 64;

		// Token: 0x04000364 RID: 868
		public static int s_TileIndexMask = 32767;

		// Token: 0x04000365 RID: 869
		public static int s_TileIndexShiftX = 0;

		// Token: 0x04000366 RID: 870
		public static int s_TileIndexShiftY = 15;

		// Token: 0x04000367 RID: 871
		public static int s_TileIndexShiftEye = 30;

		// Token: 0x04000368 RID: 872
		public static int s_NumFeatureVariants = 29;

		// Token: 0x04000369 RID: 873
		public static int s_LightListMaxCoarseEntries = 64;

		// Token: 0x0400036A RID: 874
		public static int s_LightListMaxPrunedEntries = 24;

		// Token: 0x0400036B RID: 875
		public static int s_LightClusterMaxCoarseEntries = 128;

		// Token: 0x0400036C RID: 876
		public static uint s_LightFeatureMaskFlags = 16773120U;

		// Token: 0x0400036D RID: 877
		public static uint s_LightFeatureMaskFlagsOpaque = 16642048U;

		// Token: 0x0400036E RID: 878
		public static uint s_LightFeatureMaskFlagsTransparent = 16510976U;

		// Token: 0x0400036F RID: 879
		public static uint s_MaterialFeatureMaskFlags = 4095U;

		// Token: 0x04000370 RID: 880
		public static uint s_ScreenSpaceColorShadowFlag = 256U;

		// Token: 0x04000371 RID: 881
		public static uint s_InvalidScreenSpaceShadow = 255U;

		// Token: 0x04000372 RID: 882
		public static uint s_ScreenSpaceShadowIndexMask = 255U;
	}
}
