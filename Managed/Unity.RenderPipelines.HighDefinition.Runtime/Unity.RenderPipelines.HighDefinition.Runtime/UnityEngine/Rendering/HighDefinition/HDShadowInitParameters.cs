using System;
using UnityEngine.Serialization;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x02000096 RID: 150
	[Serializable]
	public struct HDShadowInitParameters
	{
		// Token: 0x060005E9 RID: 1513 RVA: 0x00031F34 File Offset: 0x00030134
		internal static HDShadowInitParameters NewDefault()
		{
			return new HDShadowInitParameters
			{
				maxShadowRequests = 128,
				directionalShadowsDepthBits = DepthBits.Depth32,
				punctualLightShadowAtlas = HDShadowInitParameters.HDShadowAtlasInitParams.GetDefault(),
				areaLightShadowAtlas = HDShadowInitParameters.HDShadowAtlasInitParams.GetDefault(),
				shadowResolutionDirectional = new IntScalableSetting(new int[] { 256, 512, 1024, 2048 }, ScalableSettingSchemaId.With4Levels),
				shadowResolutionArea = new IntScalableSetting(new int[] { 256, 512, 1024, 2048 }, ScalableSettingSchemaId.With4Levels),
				shadowResolutionPunctual = new IntScalableSetting(new int[] { 256, 512, 1024, 2048 }, ScalableSettingSchemaId.With4Levels),
				shadowFilteringQuality = ShaderConfig.s_DeferredShadowFiltering,
				supportScreenSpaceShadows = false,
				maxScreenSpaceShadowSlots = 4,
				screenSpaceShadowBufferFormat = ScreenSpaceShadowFormat.R16G16B16A16,
				maxDirectionalShadowMapResolution = 2048,
				maxAreaShadowMapResolution = 2048,
				maxPunctualShadowMapResolution = 2048
			};
		}

		// Token: 0x04000626 RID: 1574
		internal const int k_DefaultShadowAtlasResolution = 4096;

		// Token: 0x04000627 RID: 1575
		internal const int k_DefaultMaxShadowRequests = 128;

		// Token: 0x04000628 RID: 1576
		internal const DepthBits k_DefaultShadowMapDepthBits = DepthBits.Depth32;

		// Token: 0x04000629 RID: 1577
		public int maxShadowRequests;

		// Token: 0x0400062A RID: 1578
		public DepthBits directionalShadowsDepthBits;

		// Token: 0x0400062B RID: 1579
		[FormerlySerializedAs("shadowQuality")]
		public HDShadowFilteringQuality shadowFilteringQuality;

		// Token: 0x0400062C RID: 1580
		public HDShadowInitParameters.HDShadowAtlasInitParams punctualLightShadowAtlas;

		// Token: 0x0400062D RID: 1581
		public HDShadowInitParameters.HDShadowAtlasInitParams areaLightShadowAtlas;

		// Token: 0x0400062E RID: 1582
		public IntScalableSetting shadowResolutionDirectional;

		// Token: 0x0400062F RID: 1583
		public IntScalableSetting shadowResolutionPunctual;

		// Token: 0x04000630 RID: 1584
		public IntScalableSetting shadowResolutionArea;

		// Token: 0x04000631 RID: 1585
		public int maxDirectionalShadowMapResolution;

		// Token: 0x04000632 RID: 1586
		public int maxPunctualShadowMapResolution;

		// Token: 0x04000633 RID: 1587
		public int maxAreaShadowMapResolution;

		// Token: 0x04000634 RID: 1588
		public bool supportScreenSpaceShadows;

		// Token: 0x04000635 RID: 1589
		public int maxScreenSpaceShadowSlots;

		// Token: 0x04000636 RID: 1590
		public ScreenSpaceShadowFormat screenSpaceShadowBufferFormat;

		// Token: 0x0200021E RID: 542
		[Serializable]
		public struct HDShadowAtlasInitParams
		{
			// Token: 0x06000C0C RID: 3084 RVA: 0x000576A8 File Offset: 0x000558A8
			internal static HDShadowInitParameters.HDShadowAtlasInitParams GetDefault()
			{
				return new HDShadowInitParameters.HDShadowAtlasInitParams
				{
					shadowAtlasResolution = 4096,
					shadowAtlasDepthBits = DepthBits.Depth32,
					useDynamicViewportRescale = true
				};
			}

			// Token: 0x040013E5 RID: 5093
			public int shadowAtlasResolution;

			// Token: 0x040013E6 RID: 5094
			public DepthBits shadowAtlasDepthBits;

			// Token: 0x040013E7 RID: 5095
			public bool useDynamicViewportRescale;
		}
	}
}
