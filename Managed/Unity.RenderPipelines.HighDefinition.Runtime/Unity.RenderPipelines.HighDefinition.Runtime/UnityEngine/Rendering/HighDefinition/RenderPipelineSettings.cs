using System;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x0200013D RID: 317
	[Serializable]
	public struct RenderPipelineSettings
	{
		// Token: 0x0600094D RID: 2381 RVA: 0x0004BC54 File Offset: 0x00049E54
		internal static RenderPipelineSettings NewDefault()
		{
			return new RenderPipelineSettings
			{
				supportShadowMask = true,
				supportSSAO = true,
				supportSubsurfaceScattering = true,
				supportVolumetrics = true,
				supportDistortion = true,
				supportTransparentBackface = true,
				supportTransparentDepthPrepass = true,
				supportTransparentDepthPostpass = true,
				colorBufferFormat = RenderPipelineSettings.ColorBufferFormat.R11G11B10,
				supportCustomPass = true,
				customBufferFormat = RenderPipelineSettings.CustomBufferFormat.R8G8B8A8,
				supportedLitShaderMode = RenderPipelineSettings.SupportedLitShaderMode.DeferredOnly,
				supportDecals = true,
				msaaSampleCount = MSAASamples.None,
				supportMotionVectors = true,
				supportRuntimeDebugDisplay = true,
				supportDitheringCrossFade = true,
				supportTerrainHole = false,
				lightLoopSettings = GlobalLightLoopSettings.NewDefault(),
				hdShadowInitParams = HDShadowInitParameters.NewDefault(),
				decalSettings = GlobalDecalSettings.NewDefault(),
				postProcessSettings = GlobalPostProcessSettings.NewDefault(),
				dynamicResolutionSettings = GlobalDynamicResolutionSettings.NewDefault(),
				lowresTransparentSettings = GlobalLowResolutionTransparencySettings.NewDefault(),
				xrSettings = GlobalXRSettings.NewDefault(),
				postProcessQualitySettings = GlobalPostProcessingQualitySettings.NewDefault(),
				lightingQualitySettings = GlobalLightingQualitySettings.NewDefault(),
				supportRayTracing = false,
				lodBias = new FloatScalableSetting(new float[] { 1f, 1f, 1f }, ScalableSettingSchemaId.With3Levels),
				maximumLODLevel = new IntScalableSetting(new int[3], ScalableSettingSchemaId.With3Levels),
				lightLayerName0 = "Light Layer default",
				lightLayerName1 = "Light Layer 1",
				lightLayerName2 = "Light Layer 2",
				lightLayerName3 = "Light Layer 3",
				lightLayerName4 = "Light Layer 4",
				lightLayerName5 = "Light Layer 5",
				lightLayerName6 = "Light Layer 6",
				lightLayerName7 = "Light Layer 7"
			};
		}

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x0600094E RID: 2382 RVA: 0x0004BE09 File Offset: 0x0004A009
		public bool supportMSAA
		{
			get
			{
				return this.msaaSampleCount != MSAASamples.None;
			}
		}

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x0600094F RID: 2383 RVA: 0x0004BE17 File Offset: 0x0004A017
		internal bool supportsAlpha
		{
			get
			{
				return this.colorBufferFormat == RenderPipelineSettings.ColorBufferFormat.R16G16B16A16;
			}
		}

		// Token: 0x04000ECA RID: 3786
		public bool supportShadowMask;

		// Token: 0x04000ECB RID: 3787
		public bool supportSSR;

		// Token: 0x04000ECC RID: 3788
		public bool supportSSAO;

		// Token: 0x04000ECD RID: 3789
		public bool supportSubsurfaceScattering;

		// Token: 0x04000ECE RID: 3790
		public bool increaseSssSampleCount;

		// Token: 0x04000ECF RID: 3791
		public bool supportVolumetrics;

		// Token: 0x04000ED0 RID: 3792
		public bool increaseResolutionOfVolumetrics;

		// Token: 0x04000ED1 RID: 3793
		public bool supportLightLayers;

		// Token: 0x04000ED2 RID: 3794
		public string lightLayerName0;

		// Token: 0x04000ED3 RID: 3795
		public string lightLayerName1;

		// Token: 0x04000ED4 RID: 3796
		public string lightLayerName2;

		// Token: 0x04000ED5 RID: 3797
		public string lightLayerName3;

		// Token: 0x04000ED6 RID: 3798
		public string lightLayerName4;

		// Token: 0x04000ED7 RID: 3799
		public string lightLayerName5;

		// Token: 0x04000ED8 RID: 3800
		public string lightLayerName6;

		// Token: 0x04000ED9 RID: 3801
		public string lightLayerName7;

		// Token: 0x04000EDA RID: 3802
		public bool supportDistortion;

		// Token: 0x04000EDB RID: 3803
		public bool supportTransparentBackface;

		// Token: 0x04000EDC RID: 3804
		public bool supportTransparentDepthPrepass;

		// Token: 0x04000EDD RID: 3805
		public bool supportTransparentDepthPostpass;

		// Token: 0x04000EDE RID: 3806
		public RenderPipelineSettings.ColorBufferFormat colorBufferFormat;

		// Token: 0x04000EDF RID: 3807
		public bool supportCustomPass;

		// Token: 0x04000EE0 RID: 3808
		public RenderPipelineSettings.CustomBufferFormat customBufferFormat;

		// Token: 0x04000EE1 RID: 3809
		public RenderPipelineSettings.SupportedLitShaderMode supportedLitShaderMode;

		// Token: 0x04000EE2 RID: 3810
		public bool supportDecals;

		// Token: 0x04000EE3 RID: 3811
		public MSAASamples msaaSampleCount;

		// Token: 0x04000EE4 RID: 3812
		public bool supportMotionVectors;

		// Token: 0x04000EE5 RID: 3813
		public bool supportRuntimeDebugDisplay;

		// Token: 0x04000EE6 RID: 3814
		public bool supportDitheringCrossFade;

		// Token: 0x04000EE7 RID: 3815
		public bool supportTerrainHole;

		// Token: 0x04000EE8 RID: 3816
		public bool supportRayTracing;

		// Token: 0x04000EE9 RID: 3817
		public GlobalLightLoopSettings lightLoopSettings;

		// Token: 0x04000EEA RID: 3818
		public HDShadowInitParameters hdShadowInitParams;

		// Token: 0x04000EEB RID: 3819
		public GlobalDecalSettings decalSettings;

		// Token: 0x04000EEC RID: 3820
		public GlobalPostProcessSettings postProcessSettings;

		// Token: 0x04000EED RID: 3821
		public GlobalDynamicResolutionSettings dynamicResolutionSettings;

		// Token: 0x04000EEE RID: 3822
		public GlobalLowResolutionTransparencySettings lowresTransparentSettings;

		// Token: 0x04000EEF RID: 3823
		public GlobalXRSettings xrSettings;

		// Token: 0x04000EF0 RID: 3824
		public GlobalPostProcessingQualitySettings postProcessQualitySettings;

		// Token: 0x04000EF1 RID: 3825
		public RenderPipelineSettings.LightSettings lightSettings;

		// Token: 0x04000EF2 RID: 3826
		public IntScalableSetting maximumLODLevel;

		// Token: 0x04000EF3 RID: 3827
		public FloatScalableSetting lodBias;

		// Token: 0x04000EF4 RID: 3828
		public GlobalLightingQualitySettings lightingQualitySettings;

		// Token: 0x02000287 RID: 647
		public enum SupportedLitShaderMode
		{
			// Token: 0x040016CF RID: 5839
			ForwardOnly = 1,
			// Token: 0x040016D0 RID: 5840
			DeferredOnly,
			// Token: 0x040016D1 RID: 5841
			Both
		}

		// Token: 0x02000288 RID: 648
		public enum ColorBufferFormat
		{
			// Token: 0x040016D3 RID: 5843
			R11G11B10 = 74,
			// Token: 0x040016D4 RID: 5844
			R16G16B16A16 = 48
		}

		// Token: 0x02000289 RID: 649
		public enum CustomBufferFormat
		{
			// Token: 0x040016D6 RID: 5846
			R8G8B8A8 = 12,
			// Token: 0x040016D7 RID: 5847
			R16G16B16A16 = 48,
			// Token: 0x040016D8 RID: 5848
			R11G11B10 = 74
		}

		// Token: 0x0200028A RID: 650
		[Serializable]
		public struct LightSettings
		{
			// Token: 0x040016D9 RID: 5849
			public BoolScalableSetting useContactShadow;
		}
	}
}
