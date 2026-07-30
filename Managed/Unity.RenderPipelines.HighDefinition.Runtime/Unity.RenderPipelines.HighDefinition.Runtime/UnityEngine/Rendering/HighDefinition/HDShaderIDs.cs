using System;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x02000109 RID: 265
	internal static class HDShaderIDs
	{
		// Token: 0x04000A32 RID: 2610
		public static readonly int _ZClip = Shader.PropertyToID("_ZClip");

		// Token: 0x04000A33 RID: 2611
		public static readonly int _HDShadowDatas = Shader.PropertyToID("_HDShadowDatas");

		// Token: 0x04000A34 RID: 2612
		public static readonly int _HDDirectionalShadowData = Shader.PropertyToID("_HDDirectionalShadowData");

		// Token: 0x04000A35 RID: 2613
		public static readonly int _ShadowmapAtlas = Shader.PropertyToID("_ShadowmapAtlas");

		// Token: 0x04000A36 RID: 2614
		public static readonly int _AreaLightShadowmapAtlas = Shader.PropertyToID("_AreaShadowmapAtlas");

		// Token: 0x04000A37 RID: 2615
		public static readonly int _AreaShadowmapMomentAtlas = Shader.PropertyToID("_AreaShadowmapMomentAtlas");

		// Token: 0x04000A38 RID: 2616
		public static readonly int _ShadowmapCascadeAtlas = Shader.PropertyToID("_ShadowmapCascadeAtlas");

		// Token: 0x04000A39 RID: 2617
		public static readonly int _AreaShadowAtlasSize = Shader.PropertyToID("_AreaShadowAtlasSize");

		// Token: 0x04000A3A RID: 2618
		public static readonly int _ShadowAtlasSize = Shader.PropertyToID("_ShadowAtlasSize");

		// Token: 0x04000A3B RID: 2619
		public static readonly int _CascadeShadowAtlasSize = Shader.PropertyToID("_CascadeShadowAtlasSize");

		// Token: 0x04000A3C RID: 2620
		public static readonly int _CascadeShadowCount = Shader.PropertyToID("_CascadeShadowCount");

		// Token: 0x04000A3D RID: 2621
		public static readonly int _MomentShadowAtlas = Shader.PropertyToID("_MomentShadowAtlas");

		// Token: 0x04000A3E RID: 2622
		public static readonly int _MomentShadowmapSlotST = Shader.PropertyToID("_MomentShadowmapSlotST");

		// Token: 0x04000A3F RID: 2623
		public static readonly int _MomentShadowmapSize = Shader.PropertyToID("_MomentShadowmapSize");

		// Token: 0x04000A40 RID: 2624
		public static readonly int _SummedAreaTableInputInt = Shader.PropertyToID("_SummedAreaTableInputInt");

		// Token: 0x04000A41 RID: 2625
		public static readonly int _SummedAreaTableOutputInt = Shader.PropertyToID("_SummedAreaTableOutputInt");

		// Token: 0x04000A42 RID: 2626
		public static readonly int _SummedAreaTableInputFloat = Shader.PropertyToID("_SummedAreaTableInputFloat");

		// Token: 0x04000A43 RID: 2627
		public static readonly int _IMSKernelSize = Shader.PropertyToID("_IMSKernelSize");

		// Token: 0x04000A44 RID: 2628
		public static readonly int _SrcRect = Shader.PropertyToID("_SrcRect");

		// Token: 0x04000A45 RID: 2629
		public static readonly int _DstRect = Shader.PropertyToID("_DstRect");

		// Token: 0x04000A46 RID: 2630
		public static readonly int _EVSMExponent = Shader.PropertyToID("_EVSMExponent");

		// Token: 0x04000A47 RID: 2631
		public static readonly int _BlurWeightsStorage = Shader.PropertyToID("_BlurWeightsStorage");

		// Token: 0x04000A48 RID: 2632
		public static readonly int g_LayeredSingleIdxBuffer = Shader.PropertyToID("g_LayeredSingleIdxBuffer");

		// Token: 0x04000A49 RID: 2633
		public static readonly int _EnvLightIndexShift = Shader.PropertyToID("_EnvLightIndexShift");

		// Token: 0x04000A4A RID: 2634
		public static readonly int _DensityVolumeIndexShift = Shader.PropertyToID("_DensityVolumeIndexShift");

		// Token: 0x04000A4B RID: 2635
		public static readonly int g_isOrthographic = Shader.PropertyToID("g_isOrthographic");

		// Token: 0x04000A4C RID: 2636
		public static readonly int g_iNrVisibLights = Shader.PropertyToID("g_iNrVisibLights");

		// Token: 0x04000A4D RID: 2637
		public static readonly int g_mScrProjectionArr = Shader.PropertyToID("g_mScrProjectionArr");

		// Token: 0x04000A4E RID: 2638
		public static readonly int g_mInvScrProjectionArr = Shader.PropertyToID("g_mInvScrProjectionArr");

		// Token: 0x04000A4F RID: 2639
		public static readonly int g_iLog2NumClusters = Shader.PropertyToID("g_iLog2NumClusters");

		// Token: 0x04000A50 RID: 2640
		public static readonly int g_screenSize = Shader.PropertyToID("g_screenSize");

		// Token: 0x04000A51 RID: 2641
		public static readonly int g_iNumSamplesMSAA = Shader.PropertyToID("g_iNumSamplesMSAA");

		// Token: 0x04000A52 RID: 2642
		public static readonly int g_fNearPlane = Shader.PropertyToID("g_fNearPlane");

		// Token: 0x04000A53 RID: 2643
		public static readonly int g_fFarPlane = Shader.PropertyToID("g_fFarPlane");

		// Token: 0x04000A54 RID: 2644
		public static readonly int g_fClustScale = Shader.PropertyToID("g_fClustScale");

		// Token: 0x04000A55 RID: 2645
		public static readonly int g_fClustBase = Shader.PropertyToID("g_fClustBase");

		// Token: 0x04000A56 RID: 2646
		public static readonly int g_depth_tex = Shader.PropertyToID("g_depth_tex");

		// Token: 0x04000A57 RID: 2647
		public static readonly int g_vLayeredLightList = Shader.PropertyToID("g_vLayeredLightList");

		// Token: 0x04000A58 RID: 2648
		public static readonly int g_LayeredOffset = Shader.PropertyToID("g_LayeredOffset");

		// Token: 0x04000A59 RID: 2649
		public static readonly int g_vBigTileLightList = Shader.PropertyToID("g_vBigTileLightList");

		// Token: 0x04000A5A RID: 2650
		public static readonly int g_vLightListGlobal = Shader.PropertyToID("g_vLightListGlobal");

		// Token: 0x04000A5B RID: 2651
		public static readonly int g_logBaseBuffer = Shader.PropertyToID("g_logBaseBuffer");

		// Token: 0x04000A5C RID: 2652
		public static readonly int g_vBoundsBuffer = Shader.PropertyToID("g_vBoundsBuffer");

		// Token: 0x04000A5D RID: 2653
		public static readonly int _LightVolumeData = Shader.PropertyToID("_LightVolumeData");

		// Token: 0x04000A5E RID: 2654
		public static readonly int g_data = Shader.PropertyToID("g_data");

		// Token: 0x04000A5F RID: 2655
		public static readonly int g_mProjectionArr = Shader.PropertyToID("g_mProjectionArr");

		// Token: 0x04000A60 RID: 2656
		public static readonly int g_mInvProjectionArr = Shader.PropertyToID("g_mInvProjectionArr");

		// Token: 0x04000A61 RID: 2657
		public static readonly int g_viDimensions = Shader.PropertyToID("g_viDimensions");

		// Token: 0x04000A62 RID: 2658
		public static readonly int g_vLightList = Shader.PropertyToID("g_vLightList");

		// Token: 0x04000A63 RID: 2659
		public static readonly int g_BaseFeatureFlags = Shader.PropertyToID("g_BaseFeatureFlags");

		// Token: 0x04000A64 RID: 2660
		public static readonly int g_TileFeatureFlags = Shader.PropertyToID("g_TileFeatureFlags");

		// Token: 0x04000A65 RID: 2661
		public static readonly int g_DispatchIndirectBuffer = Shader.PropertyToID("g_DispatchIndirectBuffer");

		// Token: 0x04000A66 RID: 2662
		public static readonly int g_TileList = Shader.PropertyToID("g_TileList");

		// Token: 0x04000A67 RID: 2663
		public static readonly int g_NumTiles = Shader.PropertyToID("g_NumTiles");

		// Token: 0x04000A68 RID: 2664
		public static readonly int g_NumTilesX = Shader.PropertyToID("g_NumTilesX");

		// Token: 0x04000A69 RID: 2665
		public static readonly int g_VertexPerTile = Shader.PropertyToID("g_VertexPerTile");

		// Token: 0x04000A6A RID: 2666
		public static readonly int _NumTiles = Shader.PropertyToID("_NumTiles");

		// Token: 0x04000A6B RID: 2667
		public static readonly int _CookieAtlas = Shader.PropertyToID("_CookieAtlas");

		// Token: 0x04000A6C RID: 2668
		public static readonly int _CookieAtlasSize = Shader.PropertyToID("_CookieAtlasSize");

		// Token: 0x04000A6D RID: 2669
		public static readonly int _CookieAtlasData = Shader.PropertyToID("_CookieAtlasData");

		// Token: 0x04000A6E RID: 2670
		public static readonly int _CookieCubeTextures = Shader.PropertyToID("_CookieCubeTextures");

		// Token: 0x04000A6F RID: 2671
		public static readonly int _PlanarAtlasData = Shader.PropertyToID("_PlanarAtlasData");

		// Token: 0x04000A70 RID: 2672
		public static readonly int _EnvCubemapTextures = Shader.PropertyToID("_EnvCubemapTextures");

		// Token: 0x04000A71 RID: 2673
		public static readonly int _EnvSliceSize = Shader.PropertyToID("_EnvSliceSize");

		// Token: 0x04000A72 RID: 2674
		public static readonly int _Env2DTextures = Shader.PropertyToID("_Env2DTextures");

		// Token: 0x04000A73 RID: 2675
		public static readonly int _Env2DCaptureVP = Shader.PropertyToID("_Env2DCaptureVP");

		// Token: 0x04000A74 RID: 2676
		public static readonly int _Env2DCaptureForward = Shader.PropertyToID("_Env2DCaptureForward");

		// Token: 0x04000A75 RID: 2677
		public static readonly int _Env2DAtlasScaleOffset = Shader.PropertyToID("_Env2DAtlasScaleOffset");

		// Token: 0x04000A76 RID: 2678
		public static readonly int _DirectionalLightDatas = Shader.PropertyToID("_DirectionalLightDatas");

		// Token: 0x04000A77 RID: 2679
		public static readonly int _DirectionalLightCount = Shader.PropertyToID("_DirectionalLightCount");

		// Token: 0x04000A78 RID: 2680
		public static readonly int _LightDatas = Shader.PropertyToID("_LightDatas");

		// Token: 0x04000A79 RID: 2681
		public static readonly int _PunctualLightCount = Shader.PropertyToID("_PunctualLightCount");

		// Token: 0x04000A7A RID: 2682
		public static readonly int _AreaLightCount = Shader.PropertyToID("_AreaLightCount");

		// Token: 0x04000A7B RID: 2683
		public static readonly int _EnvLightDatas = Shader.PropertyToID("_EnvLightDatas");

		// Token: 0x04000A7C RID: 2684
		public static readonly int _EnvLightCount = Shader.PropertyToID("_EnvLightCount");

		// Token: 0x04000A7D RID: 2685
		public static readonly int _EnvProxyCount = Shader.PropertyToID("_EnvProxyCount");

		// Token: 0x04000A7E RID: 2686
		public static readonly int _NumTileBigTileX = Shader.PropertyToID("_NumTileBigTileX");

		// Token: 0x04000A7F RID: 2687
		public static readonly int _NumTileBigTileY = Shader.PropertyToID("_NumTileBigTileY");

		// Token: 0x04000A80 RID: 2688
		public static readonly int _NumTileFtplX = Shader.PropertyToID("_NumTileFtplX");

		// Token: 0x04000A81 RID: 2689
		public static readonly int _NumTileFtplY = Shader.PropertyToID("_NumTileFtplY");

		// Token: 0x04000A82 RID: 2690
		public static readonly int _NumTileClusteredX = Shader.PropertyToID("_NumTileClusteredX");

		// Token: 0x04000A83 RID: 2691
		public static readonly int _NumTileClusteredY = Shader.PropertyToID("_NumTileClusteredY");

		// Token: 0x04000A84 RID: 2692
		public static readonly int _IndirectLightingMultiplier = Shader.PropertyToID("_IndirectLightingMultiplier");

		// Token: 0x04000A85 RID: 2693
		public static readonly int g_isLogBaseBufferEnabled = Shader.PropertyToID("g_isLogBaseBufferEnabled");

		// Token: 0x04000A86 RID: 2694
		public static readonly int g_vLayeredOffsetsBuffer = Shader.PropertyToID("g_vLayeredOffsetsBuffer");

		// Token: 0x04000A87 RID: 2695
		public static readonly int _LightListToClear = Shader.PropertyToID("_LightListToClear");

		// Token: 0x04000A88 RID: 2696
		public static readonly int _LightListEntries = Shader.PropertyToID("_LightListEntries");

		// Token: 0x04000A89 RID: 2697
		public static readonly int _ViewTilesFlags = Shader.PropertyToID("_ViewTilesFlags");

		// Token: 0x04000A8A RID: 2698
		public static readonly int _MousePixelCoord = Shader.PropertyToID("_MousePixelCoord");

		// Token: 0x04000A8B RID: 2699
		public static readonly int _MouseClickPixelCoord = Shader.PropertyToID("_MouseClickPixelCoord");

		// Token: 0x04000A8C RID: 2700
		public static readonly int _DebugFont = Shader.PropertyToID("_DebugFont");

		// Token: 0x04000A8D RID: 2701
		public static readonly int _DebugExposure = Shader.PropertyToID("_DebugExposure");

		// Token: 0x04000A8E RID: 2702
		public static readonly int _SliceIndex = Shader.PropertyToID("_SliceIndex");

		// Token: 0x04000A8F RID: 2703
		public static readonly int _DebugContactShadowLightIndex = Shader.PropertyToID("_DebugContactShadowLightIndex");

		// Token: 0x04000A90 RID: 2704
		public static readonly int _DebugViewMaterial = Shader.PropertyToID("_DebugViewMaterialArray");

		// Token: 0x04000A91 RID: 2705
		public static readonly int _DebugLightingMode = Shader.PropertyToID("_DebugLightingMode");

		// Token: 0x04000A92 RID: 2706
		public static readonly int _DebugShadowMapMode = Shader.PropertyToID("_DebugShadowMapMode");

		// Token: 0x04000A93 RID: 2707
		public static readonly int _DebugLightingAlbedo = Shader.PropertyToID("_DebugLightingAlbedo");

		// Token: 0x04000A94 RID: 2708
		public static readonly int _DebugLightingSmoothness = Shader.PropertyToID("_DebugLightingSmoothness");

		// Token: 0x04000A95 RID: 2709
		public static readonly int _DebugLightingNormal = Shader.PropertyToID("_DebugLightingNormal");

		// Token: 0x04000A96 RID: 2710
		public static readonly int _DebugLightingAmbientOcclusion = Shader.PropertyToID("_DebugLightingAmbientOcclusion");

		// Token: 0x04000A97 RID: 2711
		public static readonly int _DebugLightingSpecularColor = Shader.PropertyToID("_DebugLightingSpecularColor");

		// Token: 0x04000A98 RID: 2712
		public static readonly int _DebugLightingEmissiveColor = Shader.PropertyToID("_DebugLightingEmissiveColor");

		// Token: 0x04000A99 RID: 2713
		public static readonly int _AmbientOcclusionTexture = Shader.PropertyToID("_AmbientOcclusionTexture");

		// Token: 0x04000A9A RID: 2714
		public static readonly int _AmbientOcclusionTextureRW = Shader.PropertyToID("_AmbientOcclusionTextureRW");

		// Token: 0x04000A9B RID: 2715
		public static readonly int _MultiAmbientOcclusionTexture = Shader.PropertyToID("_MultiAmbientOcclusionTexture");

		// Token: 0x04000A9C RID: 2716
		public static readonly int _DebugMipMapMode = Shader.PropertyToID("_DebugMipMapMode");

		// Token: 0x04000A9D RID: 2717
		public static readonly int _DebugMipMapModeTerrainTexture = Shader.PropertyToID("_DebugMipMapModeTerrainTexture");

		// Token: 0x04000A9E RID: 2718
		public static readonly int _DebugSingleShadowIndex = Shader.PropertyToID("_DebugSingleShadowIndex");

		// Token: 0x04000A9F RID: 2719
		public static readonly int _DebugDepthPyramidMip = Shader.PropertyToID("_DebugDepthPyramidMip");

		// Token: 0x04000AA0 RID: 2720
		public static readonly int _DebugDepthPyramidOffsets = Shader.PropertyToID("_DebugDepthPyramidOffsets");

		// Token: 0x04000AA1 RID: 2721
		public static readonly int _DebugLightingMaterialValidateHighColor = Shader.PropertyToID("_DebugLightingMaterialValidateHighColor");

		// Token: 0x04000AA2 RID: 2722
		public static readonly int _DebugLightingMaterialValidateLowColor = Shader.PropertyToID("_DebugLightingMaterialValidateLowColor");

		// Token: 0x04000AA3 RID: 2723
		public static readonly int _DebugLightingMaterialValidatePureMetalColor = Shader.PropertyToID("_DebugLightingMaterialValidatePureMetalColor");

		// Token: 0x04000AA4 RID: 2724
		public static readonly int _DebugFullScreenMode = Shader.PropertyToID("_DebugFullScreenMode");

		// Token: 0x04000AA5 RID: 2725
		public static readonly int _DebugTransparencyOverdrawWeight = Shader.PropertyToID("_DebugTransparencyOverdrawWeight");

		// Token: 0x04000AA6 RID: 2726
		public static readonly int _UseTileLightList = Shader.PropertyToID("_UseTileLightList");

		// Token: 0x04000AA7 RID: 2727
		public static readonly int _FrameCount = Shader.PropertyToID("_FrameCount");

		// Token: 0x04000AA8 RID: 2728
		public static readonly int _Time = Shader.PropertyToID("_Time");

		// Token: 0x04000AA9 RID: 2729
		public static readonly int _SinTime = Shader.PropertyToID("_SinTime");

		// Token: 0x04000AAA RID: 2730
		public static readonly int _CosTime = Shader.PropertyToID("_CosTime");

		// Token: 0x04000AAB RID: 2731
		public static readonly int unity_DeltaTime = Shader.PropertyToID("unity_DeltaTime");

		// Token: 0x04000AAC RID: 2732
		public static readonly int _TimeParameters = Shader.PropertyToID("_TimeParameters");

		// Token: 0x04000AAD RID: 2733
		public static readonly int _LastTimeParameters = Shader.PropertyToID("_LastTimeParameters");

		// Token: 0x04000AAE RID: 2734
		public static readonly int _EnvLightSkyEnabled = Shader.PropertyToID("_EnvLightSkyEnabled");

		// Token: 0x04000AAF RID: 2735
		public static readonly int _AmbientOcclusionParam = Shader.PropertyToID("_AmbientOcclusionParam");

		// Token: 0x04000AB0 RID: 2736
		public static readonly int _SkyTexture = Shader.PropertyToID("_SkyTexture");

		// Token: 0x04000AB1 RID: 2737
		public static readonly int _SkyTextureMipCount = Shader.PropertyToID("_SkyTextureMipCount");

		// Token: 0x04000AB2 RID: 2738
		public static readonly int _EnableSubsurfaceScattering = Shader.PropertyToID("_EnableSubsurfaceScattering");

		// Token: 0x04000AB3 RID: 2739
		public static readonly int _TransmittanceMultiplier = Shader.PropertyToID("_TransmittanceMultiplier");

		// Token: 0x04000AB4 RID: 2740
		public static readonly int _TexturingModeFlags = Shader.PropertyToID("_TexturingModeFlags");

		// Token: 0x04000AB5 RID: 2741
		public static readonly int _TransmissionFlags = Shader.PropertyToID("_TransmissionFlags");

		// Token: 0x04000AB6 RID: 2742
		public static readonly int _ThicknessRemaps = Shader.PropertyToID("_ThicknessRemaps");

		// Token: 0x04000AB7 RID: 2743
		public static readonly int _ShapeParams = Shader.PropertyToID("_ShapeParams");

		// Token: 0x04000AB8 RID: 2744
		public static readonly int _TransmissionTintsAndFresnel0 = Shader.PropertyToID("_TransmissionTintsAndFresnel0");

		// Token: 0x04000AB9 RID: 2745
		public static readonly int specularLightingUAV = Shader.PropertyToID("specularLightingUAV");

		// Token: 0x04000ABA RID: 2746
		public static readonly int diffuseLightingUAV = Shader.PropertyToID("diffuseLightingUAV");

		// Token: 0x04000ABB RID: 2747
		public static readonly int _DiffusionProfileHashTable = Shader.PropertyToID("_DiffusionProfileHashTable");

		// Token: 0x04000ABC RID: 2748
		public static readonly int _DiffusionProfileCount = Shader.PropertyToID("_DiffusionProfileCount");

		// Token: 0x04000ABD RID: 2749
		public static readonly int _DiffusionProfileAsset = Shader.PropertyToID("_DiffusionProfileAsset");

		// Token: 0x04000ABE RID: 2750
		public static readonly int _MaterialID = Shader.PropertyToID("_MaterialID");

		// Token: 0x04000ABF RID: 2751
		public static readonly int g_TileListOffset = Shader.PropertyToID("g_TileListOffset");

		// Token: 0x04000AC0 RID: 2752
		public static readonly int _LtcData = Shader.PropertyToID("_LtcData");

		// Token: 0x04000AC1 RID: 2753
		public static readonly int _LtcGGXMatrix = Shader.PropertyToID("_LtcGGXMatrix");

		// Token: 0x04000AC2 RID: 2754
		public static readonly int _LtcDisneyDiffuseMatrix = Shader.PropertyToID("_LtcDisneyDiffuseMatrix");

		// Token: 0x04000AC3 RID: 2755
		public static readonly int _LtcMultiGGXFresnelDisneyDiffuse = Shader.PropertyToID("_LtcMultiGGXFresnelDisneyDiffuse");

		// Token: 0x04000AC4 RID: 2756
		public static readonly int _ScreenSpaceShadowsTexture = Shader.PropertyToID("_ScreenSpaceShadowsTexture");

		// Token: 0x04000AC5 RID: 2757
		public static readonly int _ContactShadowTexture = Shader.PropertyToID("_ContactShadowTexture");

		// Token: 0x04000AC6 RID: 2758
		public static readonly int _ContactShadowTextureUAV = Shader.PropertyToID("_ContactShadowTextureUAV");

		// Token: 0x04000AC7 RID: 2759
		public static readonly int _DirectionalShadowIndex = Shader.PropertyToID("_DirectionalShadowIndex");

		// Token: 0x04000AC8 RID: 2760
		public static readonly int _ContactShadowOpacity = Shader.PropertyToID("_ContactShadowOpacity");

		// Token: 0x04000AC9 RID: 2761
		public static readonly int _ContactShadowParamsParameters = Shader.PropertyToID("_ContactShadowParamsParameters");

		// Token: 0x04000ACA RID: 2762
		public static readonly int _ContactShadowParamsParameters2 = Shader.PropertyToID("_ContactShadowParamsParameters2");

		// Token: 0x04000ACB RID: 2763
		public static readonly int _DirectionalContactShadowSampleCount = Shader.PropertyToID("_SampleCount");

		// Token: 0x04000ACC RID: 2764
		public static readonly int _MicroShadowOpacity = Shader.PropertyToID("_MicroShadowOpacity");

		// Token: 0x04000ACD RID: 2765
		public static readonly int _DirectionalTransmissionMultiplier = Shader.PropertyToID("_DirectionalTransmissionMultiplier");

		// Token: 0x04000ACE RID: 2766
		public static readonly int _ShadowClipPlanes = Shader.PropertyToID("_ShadowClipPlanes");

		// Token: 0x04000ACF RID: 2767
		public static readonly int _StencilMask = Shader.PropertyToID("_StencilMask");

		// Token: 0x04000AD0 RID: 2768
		public static readonly int _StencilRef = Shader.PropertyToID("_StencilRef");

		// Token: 0x04000AD1 RID: 2769
		public static readonly int _StencilCmp = Shader.PropertyToID("_StencilCmp");

		// Token: 0x04000AD2 RID: 2770
		public static readonly int _InputDepth = Shader.PropertyToID("_InputDepthTexture");

		// Token: 0x04000AD3 RID: 2771
		public static readonly int _SrcBlend = Shader.PropertyToID("_SrcBlend");

		// Token: 0x04000AD4 RID: 2772
		public static readonly int _DstBlend = Shader.PropertyToID("_DstBlend");

		// Token: 0x04000AD5 RID: 2773
		public static readonly int _ColorMaskTransparentVel = Shader.PropertyToID("_ColorMaskTransparentVel");

		// Token: 0x04000AD6 RID: 2774
		public static readonly int _StencilTexture = Shader.PropertyToID("_StencilTexture");

		// Token: 0x04000AD7 RID: 2775
		public static readonly int _OutputStencilBuffer = Shader.PropertyToID("_OutputStencilBuffer");

		// Token: 0x04000AD8 RID: 2776
		public static readonly int _CoarseStencilBuffer = Shader.PropertyToID("_CoarseStencilBuffer");

		// Token: 0x04000AD9 RID: 2777
		public static readonly int _CoarseStencilBufferSize = Shader.PropertyToID("_CoarseStencilBufferSize");

		// Token: 0x04000ADA RID: 2778
		public static readonly int _NormalToWorldID = Shader.PropertyToID("_NormalToWorld");

		// Token: 0x04000ADB RID: 2779
		public static readonly int _DecalAtlas2DID = Shader.PropertyToID("_DecalAtlas2D");

		// Token: 0x04000ADC RID: 2780
		public static readonly int _DecalHTileTexture = Shader.PropertyToID("_DecalHTileTexture");

		// Token: 0x04000ADD RID: 2781
		public static readonly int _DecalIndexShift = Shader.PropertyToID("_DecalIndexShift");

		// Token: 0x04000ADE RID: 2782
		public static readonly int _DecalCount = Shader.PropertyToID("_DecalCount");

		// Token: 0x04000ADF RID: 2783
		public static readonly int _DecalDatas = Shader.PropertyToID("_DecalDatas");

		// Token: 0x04000AE0 RID: 2784
		public static readonly int _DecalNormalBufferStencilReadMask = Shader.PropertyToID("_DecalNormalBufferStencilReadMask");

		// Token: 0x04000AE1 RID: 2785
		public static readonly int _DecalNormalBufferStencilRef = Shader.PropertyToID("_DecalNormalBufferStencilRef");

		// Token: 0x04000AE2 RID: 2786
		public static readonly int _DecalPropertyMaskBuffer = Shader.PropertyToID("_DecalPropertyMaskBuffer");

		// Token: 0x04000AE3 RID: 2787
		public static readonly int _DecalPropertyMaskBufferSRV = Shader.PropertyToID("_DecalPropertyMaskBufferSRV");

		// Token: 0x04000AE4 RID: 2788
		public static readonly int _WorldSpaceCameraPos = Shader.PropertyToID("_WorldSpaceCameraPos");

		// Token: 0x04000AE5 RID: 2789
		public static readonly int _PrevCamPosRWS = Shader.PropertyToID("_PrevCamPosRWS");

		// Token: 0x04000AE6 RID: 2790
		public static readonly int _ViewMatrix = Shader.PropertyToID("_ViewMatrix");

		// Token: 0x04000AE7 RID: 2791
		public static readonly int _InvViewMatrix = Shader.PropertyToID("_InvViewMatrix");

		// Token: 0x04000AE8 RID: 2792
		public static readonly int _ProjMatrix = Shader.PropertyToID("_ProjMatrix");

		// Token: 0x04000AE9 RID: 2793
		public static readonly int _InvProjMatrix = Shader.PropertyToID("_InvProjMatrix");

		// Token: 0x04000AEA RID: 2794
		public static readonly int _NonJitteredViewProjMatrix = Shader.PropertyToID("_NonJitteredViewProjMatrix");

		// Token: 0x04000AEB RID: 2795
		public static readonly int _ViewProjMatrix = Shader.PropertyToID("_ViewProjMatrix");

		// Token: 0x04000AEC RID: 2796
		public static readonly int _CameraViewProjMatrix = Shader.PropertyToID("_CameraViewProjMatrix");

		// Token: 0x04000AED RID: 2797
		public static readonly int _InvViewProjMatrix = Shader.PropertyToID("_InvViewProjMatrix");

		// Token: 0x04000AEE RID: 2798
		public static readonly int _ZBufferParams = Shader.PropertyToID("_ZBufferParams");

		// Token: 0x04000AEF RID: 2799
		public static readonly int _ProjectionParams = Shader.PropertyToID("_ProjectionParams");

		// Token: 0x04000AF0 RID: 2800
		public static readonly int unity_OrthoParams = Shader.PropertyToID("unity_OrthoParams");

		// Token: 0x04000AF1 RID: 2801
		public static readonly int _InvProjParam = Shader.PropertyToID("_InvProjParam");

		// Token: 0x04000AF2 RID: 2802
		public static readonly int _ScreenSize = Shader.PropertyToID("_ScreenSize");

		// Token: 0x04000AF3 RID: 2803
		public static readonly int _ScreenParams = Shader.PropertyToID("_ScreenParams");

		// Token: 0x04000AF4 RID: 2804
		public static readonly int _RTHandleScale = Shader.PropertyToID("_RTHandleScale");

		// Token: 0x04000AF5 RID: 2805
		public static readonly int _RTHandleScaleHistory = Shader.PropertyToID("_RTHandleScaleHistory");

		// Token: 0x04000AF6 RID: 2806
		public static readonly int _PrevViewProjMatrix = Shader.PropertyToID("_PrevViewProjMatrix");

		// Token: 0x04000AF7 RID: 2807
		public static readonly int _PrevInvViewProjMatrix = Shader.PropertyToID("_PrevInvViewProjMatrix");

		// Token: 0x04000AF8 RID: 2808
		public static readonly int _FrustumPlanes = Shader.PropertyToID("_FrustumPlanes");

		// Token: 0x04000AF9 RID: 2809
		public static readonly int _TaaFrameInfo = Shader.PropertyToID("_TaaFrameInfo");

		// Token: 0x04000AFA RID: 2810
		public static readonly int _TaaJitterStrength = Shader.PropertyToID("_TaaJitterStrength");

		// Token: 0x04000AFB RID: 2811
		public static readonly int _WorldSpaceCameraPos1 = Shader.PropertyToID("_WorldSpaceCameraPos1");

		// Token: 0x04000AFC RID: 2812
		public static readonly int _ViewMatrix1 = Shader.PropertyToID("_ViewMatrix1");

		// Token: 0x04000AFD RID: 2813
		public static readonly int _XRViewCount = Shader.PropertyToID("_XRViewCount");

		// Token: 0x04000AFE RID: 2814
		public static readonly int _XRViewMatrix = Shader.PropertyToID("_XRViewMatrix");

		// Token: 0x04000AFF RID: 2815
		public static readonly int _XRInvViewMatrix = Shader.PropertyToID("_XRInvViewMatrix");

		// Token: 0x04000B00 RID: 2816
		public static readonly int _XRProjMatrix = Shader.PropertyToID("_XRProjMatrix");

		// Token: 0x04000B01 RID: 2817
		public static readonly int _XRInvProjMatrix = Shader.PropertyToID("_XRInvProjMatrix");

		// Token: 0x04000B02 RID: 2818
		public static readonly int _XRViewProjMatrix = Shader.PropertyToID("_XRViewProjMatrix");

		// Token: 0x04000B03 RID: 2819
		public static readonly int _XRInvViewProjMatrix = Shader.PropertyToID("_XRInvViewProjMatrix");

		// Token: 0x04000B04 RID: 2820
		public static readonly int _XRNonJitteredViewProjMatrix = Shader.PropertyToID("_XRNonJitteredViewProjMatrix");

		// Token: 0x04000B05 RID: 2821
		public static readonly int _XRPrevViewProjMatrix = Shader.PropertyToID("_XRPrevViewProjMatrix");

		// Token: 0x04000B06 RID: 2822
		public static readonly int _XRPrevInvViewProjMatrix = Shader.PropertyToID("_XRPrevInvViewProjMatrix");

		// Token: 0x04000B07 RID: 2823
		public static readonly int _XRPrevViewProjMatrixNoCameraTrans = Shader.PropertyToID("_XRPrevViewProjMatrixNoCameraTrans");

		// Token: 0x04000B08 RID: 2824
		public static readonly int _XRPixelCoordToViewDirWS = Shader.PropertyToID("_XRPixelCoordToViewDirWS");

		// Token: 0x04000B09 RID: 2825
		public static readonly int _XRWorldSpaceCameraPos = Shader.PropertyToID("_XRWorldSpaceCameraPos");

		// Token: 0x04000B0A RID: 2826
		public static readonly int _XRWorldSpaceCameraPosViewOffset = Shader.PropertyToID("_XRWorldSpaceCameraPosViewOffset");

		// Token: 0x04000B0B RID: 2827
		public static readonly int _XRPrevWorldSpaceCameraPos = Shader.PropertyToID("_XRPrevWorldSpaceCameraPos");

		// Token: 0x04000B0C RID: 2828
		public static readonly int _ColorTexture = Shader.PropertyToID("_ColorTexture");

		// Token: 0x04000B0D RID: 2829
		public static readonly int _DepthTexture = Shader.PropertyToID("_DepthTexture");

		// Token: 0x04000B0E RID: 2830
		public static readonly int _DepthValuesTexture = Shader.PropertyToID("_DepthValuesTexture");

		// Token: 0x04000B0F RID: 2831
		public static readonly int _CameraColorTexture = Shader.PropertyToID("_CameraColorTexture");

		// Token: 0x04000B10 RID: 2832
		public static readonly int _CameraColorTextureRW = Shader.PropertyToID("_CameraColorTextureRW");

		// Token: 0x04000B11 RID: 2833
		public static readonly int _CameraSssDiffuseLightingBuffer = Shader.PropertyToID("_CameraSssDiffuseLightingTexture");

		// Token: 0x04000B12 RID: 2834
		public static readonly int _CameraFilteringBuffer = Shader.PropertyToID("_CameraFilteringTexture");

		// Token: 0x04000B13 RID: 2835
		public static readonly int _IrradianceSource = Shader.PropertyToID("_IrradianceSource");

		// Token: 0x04000B14 RID: 2836
		public static readonly int _EnableDecals = Shader.PropertyToID("_EnableDecals");

		// Token: 0x04000B15 RID: 2837
		public static readonly int _DecalAtlasResolution = Shader.PropertyToID("_DecalAtlasResolution");

		// Token: 0x04000B16 RID: 2838
		public static readonly int _ColorTextureMS = Shader.PropertyToID("_ColorTextureMS");

		// Token: 0x04000B17 RID: 2839
		public static readonly int _DepthTextureMS = Shader.PropertyToID("_DepthTextureMS");

		// Token: 0x04000B18 RID: 2840
		public static readonly int _NormalTextureMS = Shader.PropertyToID("_NormalTextureMS");

		// Token: 0x04000B19 RID: 2841
		public static readonly int _MotionVectorTextureMS = Shader.PropertyToID("_MotionVectorTextureMS");

		// Token: 0x04000B1A RID: 2842
		public static readonly int _CameraDepthValuesTexture = Shader.PropertyToID("_CameraDepthValues");

		// Token: 0x04000B1B RID: 2843
		public static readonly int[] _GBufferTexture = new int[]
		{
			Shader.PropertyToID("_GBufferTexture0"),
			Shader.PropertyToID("_GBufferTexture1"),
			Shader.PropertyToID("_GBufferTexture2"),
			Shader.PropertyToID("_GBufferTexture3"),
			Shader.PropertyToID("_GBufferTexture4"),
			Shader.PropertyToID("_GBufferTexture5"),
			Shader.PropertyToID("_GBufferTexture6"),
			Shader.PropertyToID("_GBufferTexture7")
		};

		// Token: 0x04000B1C RID: 2844
		public static readonly int[] _GBufferTextureRW = new int[]
		{
			Shader.PropertyToID("_GBufferTexture0RW"),
			Shader.PropertyToID("_GBufferTexture1RW"),
			Shader.PropertyToID("_GBufferTexture2RW"),
			Shader.PropertyToID("_GBufferTexture3RW"),
			Shader.PropertyToID("_GBufferTexture4RW"),
			Shader.PropertyToID("_GBufferTexture5RW"),
			Shader.PropertyToID("_GBufferTexture6RW"),
			Shader.PropertyToID("_GBufferTexture7RW")
		};

		// Token: 0x04000B1D RID: 2845
		public static readonly int[] _DBufferTexture = new int[]
		{
			Shader.PropertyToID("_DBufferTexture0"),
			Shader.PropertyToID("_DBufferTexture1"),
			Shader.PropertyToID("_DBufferTexture2"),
			Shader.PropertyToID("_DBufferTexture3")
		};

		// Token: 0x04000B1E RID: 2846
		public static readonly int _SSSBufferTexture = Shader.PropertyToID("_SSSBufferTexture");

		// Token: 0x04000B1F RID: 2847
		public static readonly int _NormalBufferTexture = Shader.PropertyToID("_NormalBufferTexture");

		// Token: 0x04000B20 RID: 2848
		public static readonly int _EnableSSRefraction = Shader.PropertyToID("_EnableSSRefraction");

		// Token: 0x04000B21 RID: 2849
		public static readonly int _SSRefractionInvScreenWeightDistance = Shader.PropertyToID("_SSRefractionInvScreenWeightDistance");

		// Token: 0x04000B22 RID: 2850
		public static readonly int _SsrIterLimit = Shader.PropertyToID("_SsrIterLimit");

		// Token: 0x04000B23 RID: 2851
		public static readonly int _SsrThicknessScale = Shader.PropertyToID("_SsrThicknessScale");

		// Token: 0x04000B24 RID: 2852
		public static readonly int _SsrThicknessBias = Shader.PropertyToID("_SsrThicknessBias");

		// Token: 0x04000B25 RID: 2853
		public static readonly int _SsrRoughnessFadeEnd = Shader.PropertyToID("_SsrRoughnessFadeEnd");

		// Token: 0x04000B26 RID: 2854
		public static readonly int _SsrRoughnessFadeRcpLength = Shader.PropertyToID("_SsrRoughnessFadeRcpLength");

		// Token: 0x04000B27 RID: 2855
		public static readonly int _SsrRoughnessFadeEndTimesRcpLength = Shader.PropertyToID("_SsrRoughnessFadeEndTimesRcpLength");

		// Token: 0x04000B28 RID: 2856
		public static readonly int _SsrDepthPyramidMaxMip = Shader.PropertyToID("_SsrDepthPyramidMaxMip");

		// Token: 0x04000B29 RID: 2857
		public static readonly int _SsrColorPyramidMaxMip = Shader.PropertyToID("_SsrColorPyramidMaxMip");

		// Token: 0x04000B2A RID: 2858
		public static readonly int _SsrEdgeFadeRcpLength = Shader.PropertyToID("_SsrEdgeFadeRcpLength");

		// Token: 0x04000B2B RID: 2859
		public static readonly int _SsrLightingTexture = Shader.PropertyToID("_SsrLightingTexture");

		// Token: 0x04000B2C RID: 2860
		public static readonly int _SsrLightingTextureRW = Shader.PropertyToID("_SsrLightingTextureRW");

		// Token: 0x04000B2D RID: 2861
		public static readonly int _SsrHitPointTexture = Shader.PropertyToID("_SsrHitPointTexture");

		// Token: 0x04000B2E RID: 2862
		public static readonly int _SsrClearCoatMaskTexture = Shader.PropertyToID("_SsrClearCoatMaskTexture");

		// Token: 0x04000B2F RID: 2863
		public static readonly int _SsrStencilBit = Shader.PropertyToID("_SsrStencilBit");

		// Token: 0x04000B30 RID: 2864
		public static readonly int _SsrReflectsSky = Shader.PropertyToID("_SsrReflectsSky");

		// Token: 0x04000B31 RID: 2865
		public static readonly int _DepthPyramidMipLevelOffsets = Shader.PropertyToID("_DepthPyramidMipLevelOffsets");

		// Token: 0x04000B32 RID: 2866
		public static readonly int _ShadowMaskTexture = Shader.PropertyToID("_ShadowMaskTexture");

		// Token: 0x04000B33 RID: 2867
		public static readonly int _LightLayersTexture = Shader.PropertyToID("_LightLayersTexture");

		// Token: 0x04000B34 RID: 2868
		public static readonly int _DistortionTexture = Shader.PropertyToID("_DistortionTexture");

		// Token: 0x04000B35 RID: 2869
		public static readonly int _ColorPyramidTexture = Shader.PropertyToID("_ColorPyramidTexture");

		// Token: 0x04000B36 RID: 2870
		public static readonly int _ColorPyramidScale = Shader.PropertyToID("_ColorPyramidScale");

		// Token: 0x04000B37 RID: 2871
		public static readonly int _ColorPyramidUvScaleAndLimitPrevFrame = Shader.PropertyToID("_ColorPyramidUvScaleAndLimitPrevFrame");

		// Token: 0x04000B38 RID: 2872
		public static readonly int _DepthPyramidScale = Shader.PropertyToID("_DepthPyramidScale");

		// Token: 0x04000B39 RID: 2873
		public static readonly int _DebugColorPickerTexture = Shader.PropertyToID("_DebugColorPickerTexture");

		// Token: 0x04000B3A RID: 2874
		public static readonly int _ColorPickerMode = Shader.PropertyToID("_ColorPickerMode");

		// Token: 0x04000B3B RID: 2875
		public static readonly int _ApplyLinearToSRGB = Shader.PropertyToID("_ApplyLinearToSRGB");

		// Token: 0x04000B3C RID: 2876
		public static readonly int _ColorPickerFontColor = Shader.PropertyToID("_ColorPickerFontColor");

		// Token: 0x04000B3D RID: 2877
		public static readonly int _FalseColorEnabled = Shader.PropertyToID("_FalseColor");

		// Token: 0x04000B3E RID: 2878
		public static readonly int _FalseColorThresholds = Shader.PropertyToID("_FalseColorThresholds");

		// Token: 0x04000B3F RID: 2879
		public static readonly int _DebugMatCapTexture = Shader.PropertyToID("_DebugMatCapTexture");

		// Token: 0x04000B40 RID: 2880
		public static readonly int _MatcapViewScale = Shader.PropertyToID("_MatcapViewScale");

		// Token: 0x04000B41 RID: 2881
		public static readonly int _MatcapMixAlbedo = Shader.PropertyToID("_MatcapMixAlbedo");

		// Token: 0x04000B42 RID: 2882
		public static readonly int _DebugFullScreenTexture = Shader.PropertyToID("_DebugFullScreenTexture");

		// Token: 0x04000B43 RID: 2883
		public static readonly int _BlitTexture = Shader.PropertyToID("_BlitTexture");

		// Token: 0x04000B44 RID: 2884
		public static readonly int _BlitScaleBias = Shader.PropertyToID("_BlitScaleBias");

		// Token: 0x04000B45 RID: 2885
		public static readonly int _BlitMipLevel = Shader.PropertyToID("_BlitMipLevel");

		// Token: 0x04000B46 RID: 2886
		public static readonly int _BlitScaleBiasRt = Shader.PropertyToID("_BlitScaleBiasRt");

		// Token: 0x04000B47 RID: 2887
		public static readonly int _BlitTextureSize = Shader.PropertyToID("_BlitTextureSize");

		// Token: 0x04000B48 RID: 2888
		public static readonly int _BlitPaddingSize = Shader.PropertyToID("_BlitPaddingSize");

		// Token: 0x04000B49 RID: 2889
		public static readonly int _BlitTexArraySlice = Shader.PropertyToID("_BlitTexArraySlice");

		// Token: 0x04000B4A RID: 2890
		public static readonly int _WorldScales = Shader.PropertyToID("_WorldScales");

		// Token: 0x04000B4B RID: 2891
		public static readonly int _FilterKernels = Shader.PropertyToID("_FilterKernels");

		// Token: 0x04000B4C RID: 2892
		public static readonly int _FilterKernelsBasic = Shader.PropertyToID("_FilterKernelsBasic");

		// Token: 0x04000B4D RID: 2893
		public static readonly int _HalfRcpWeightedVariances = Shader.PropertyToID("_HalfRcpWeightedVariances");

		// Token: 0x04000B4E RID: 2894
		public static readonly int _CameraDepthTexture = Shader.PropertyToID("_CameraDepthTexture");

		// Token: 0x04000B4F RID: 2895
		public static readonly int _CameraMotionVectorsTexture = Shader.PropertyToID("_CameraMotionVectorsTexture");

		// Token: 0x04000B50 RID: 2896
		public static readonly int _CameraMotionVectorsSize = Shader.PropertyToID("_CameraMotionVectorsSize");

		// Token: 0x04000B51 RID: 2897
		public static readonly int _CameraMotionVectorsScale = Shader.PropertyToID("_CameraMotionVectorsScale");

		// Token: 0x04000B52 RID: 2898
		public static readonly int _FullScreenDebugMode = Shader.PropertyToID("_FullScreenDebugMode");

		// Token: 0x04000B53 RID: 2899
		public static readonly int _TransparencyOverdrawMaxPixelCost = Shader.PropertyToID("_TransparencyOverdrawMaxPixelCost");

		// Token: 0x04000B54 RID: 2900
		public static readonly int _CustomDepthTexture = Shader.PropertyToID("_CustomDepthTexture");

		// Token: 0x04000B55 RID: 2901
		public static readonly int _CustomColorTexture = Shader.PropertyToID("_CustomColorTexture");

		// Token: 0x04000B56 RID: 2902
		public static readonly int _CustomPassInjectionPoint = Shader.PropertyToID("_CustomPassInjectionPoint");

		// Token: 0x04000B57 RID: 2903
		public static readonly int _InputCubemap = Shader.PropertyToID("_InputCubemap");

		// Token: 0x04000B58 RID: 2904
		public static readonly int _Mipmap = Shader.PropertyToID("_Mipmap");

		// Token: 0x04000B59 RID: 2905
		public static readonly int _DiffusionProfileHash = Shader.PropertyToID("_DiffusionProfileHash");

		// Token: 0x04000B5A RID: 2906
		public static readonly int _MaxRadius = Shader.PropertyToID("_MaxRadius");

		// Token: 0x04000B5B RID: 2907
		public static readonly int _ShapeParam = Shader.PropertyToID("_ShapeParam");

		// Token: 0x04000B5C RID: 2908
		public static readonly int _StdDev1 = Shader.PropertyToID("_StdDev1");

		// Token: 0x04000B5D RID: 2909
		public static readonly int _StdDev2 = Shader.PropertyToID("_StdDev2");

		// Token: 0x04000B5E RID: 2910
		public static readonly int _LerpWeight = Shader.PropertyToID("_LerpWeight");

		// Token: 0x04000B5F RID: 2911
		public static readonly int _HalfRcpVarianceAndWeight1 = Shader.PropertyToID("_HalfRcpVarianceAndWeight1");

		// Token: 0x04000B60 RID: 2912
		public static readonly int _HalfRcpVarianceAndWeight2 = Shader.PropertyToID("_HalfRcpVarianceAndWeight2");

		// Token: 0x04000B61 RID: 2913
		public static readonly int _TransmissionTint = Shader.PropertyToID("_TransmissionTint");

		// Token: 0x04000B62 RID: 2914
		public static readonly int _ThicknessRemap = Shader.PropertyToID("_ThicknessRemap");

		// Token: 0x04000B63 RID: 2915
		public static readonly int _Cubemap = Shader.PropertyToID("_Cubemap");

		// Token: 0x04000B64 RID: 2916
		public static readonly int _InvOmegaP = Shader.PropertyToID("_InvOmegaP");

		// Token: 0x04000B65 RID: 2917
		public static readonly int _SkyParam = Shader.PropertyToID("_SkyParam");

		// Token: 0x04000B66 RID: 2918
		public static readonly int _BackplateParameters0 = Shader.PropertyToID("_BackplateParameters0");

		// Token: 0x04000B67 RID: 2919
		public static readonly int _BackplateParameters1 = Shader.PropertyToID("_BackplateParameters1");

		// Token: 0x04000B68 RID: 2920
		public static readonly int _BackplateParameters2 = Shader.PropertyToID("_BackplateParameters2");

		// Token: 0x04000B69 RID: 2921
		public static readonly int _BackplateShadowTint = Shader.PropertyToID("_BackplateShadowTint");

		// Token: 0x04000B6A RID: 2922
		public static readonly int _BackplateShadowFilter = Shader.PropertyToID("_BackplateShadowFilter");

		// Token: 0x04000B6B RID: 2923
		public static readonly int _SkyIntensity = Shader.PropertyToID("_SkyIntensity");

		// Token: 0x04000B6C RID: 2924
		public static readonly int _PixelCoordToViewDirWS = Shader.PropertyToID("_PixelCoordToViewDirWS");

		// Token: 0x04000B6D RID: 2925
		public static readonly int _Size = Shader.PropertyToID("_Size");

		// Token: 0x04000B6E RID: 2926
		public static readonly int _Source = Shader.PropertyToID("_Source");

		// Token: 0x04000B6F RID: 2927
		public static readonly int _Destination = Shader.PropertyToID("_Destination");

		// Token: 0x04000B70 RID: 2928
		public static readonly int _Mip0 = Shader.PropertyToID("_Mip0");

		// Token: 0x04000B71 RID: 2929
		public static readonly int _SourceMip = Shader.PropertyToID("_SourceMip");

		// Token: 0x04000B72 RID: 2930
		public static readonly int _SrcOffsetAndLimit = Shader.PropertyToID("_SrcOffsetAndLimit");

		// Token: 0x04000B73 RID: 2931
		public static readonly int _SrcScaleBias = Shader.PropertyToID("_SrcScaleBias");

		// Token: 0x04000B74 RID: 2932
		public static readonly int _SrcUvLimits = Shader.PropertyToID("_SrcUvLimits");

		// Token: 0x04000B75 RID: 2933
		public static readonly int _DstOffset = Shader.PropertyToID("_DstOffset");

		// Token: 0x04000B76 RID: 2934
		public static readonly int _DepthMipChain = Shader.PropertyToID("_DepthMipChain");

		// Token: 0x04000B77 RID: 2935
		public static readonly int _FogEnabled = Shader.PropertyToID("_FogEnabled");

		// Token: 0x04000B78 RID: 2936
		public static readonly int _PBRFogEnabled = Shader.PropertyToID("_PBRFogEnabled");

		// Token: 0x04000B79 RID: 2937
		public static readonly int _MaxFogDistance = Shader.PropertyToID("_MaxFogDistance");

		// Token: 0x04000B7A RID: 2938
		public static readonly int _AmbientProbeCoeffs = Shader.PropertyToID("_AmbientProbeCoeffs");

		// Token: 0x04000B7B RID: 2939
		public static readonly int _HeightFogBaseExtinction = Shader.PropertyToID("_HeightFogBaseExtinction");

		// Token: 0x04000B7C RID: 2940
		public static readonly int _HeightFogBaseScattering = Shader.PropertyToID("_HeightFogBaseScattering");

		// Token: 0x04000B7D RID: 2941
		public static readonly int _HeightFogBaseHeight = Shader.PropertyToID("_HeightFogBaseHeight");

		// Token: 0x04000B7E RID: 2942
		public static readonly int _HeightFogExponents = Shader.PropertyToID("_HeightFogExponents");

		// Token: 0x04000B7F RID: 2943
		public static readonly int _EnableVolumetricFog = Shader.PropertyToID("_EnableVolumetricFog");

		// Token: 0x04000B80 RID: 2944
		public static readonly int _GlobalFogAnisotropy = Shader.PropertyToID("_GlobalFogAnisotropy");

		// Token: 0x04000B81 RID: 2945
		public static readonly int _CornetteShanksConstant = Shader.PropertyToID("_CornetteShanksConstant");

		// Token: 0x04000B82 RID: 2946
		public static readonly int _VBufferViewportSize = Shader.PropertyToID("_VBufferViewportSize");

		// Token: 0x04000B83 RID: 2947
		public static readonly int _VBufferSliceCount = Shader.PropertyToID("_VBufferSliceCount");

		// Token: 0x04000B84 RID: 2948
		public static readonly int _VBufferRcpSliceCount = Shader.PropertyToID("_VBufferRcpSliceCount");

		// Token: 0x04000B85 RID: 2949
		public static readonly int _VBufferRcpInstancedViewCount = Shader.PropertyToID("_VBufferRcpInstancedViewCount");

		// Token: 0x04000B86 RID: 2950
		public static readonly int _VBufferSharedUvScaleAndLimit = Shader.PropertyToID("_VBufferSharedUvScaleAndLimit");

		// Token: 0x04000B87 RID: 2951
		public static readonly int _VBufferDistanceEncodingParams = Shader.PropertyToID("_VBufferDistanceEncodingParams");

		// Token: 0x04000B88 RID: 2952
		public static readonly int _VBufferDistanceDecodingParams = Shader.PropertyToID("_VBufferDistanceDecodingParams");

		// Token: 0x04000B89 RID: 2953
		public static readonly int _VBufferPrevViewportSize = Shader.PropertyToID("_VBufferPrevViewportSize");

		// Token: 0x04000B8A RID: 2954
		public static readonly int _VBufferHistoryPrevUvScaleAndLimit = Shader.PropertyToID("_VBufferHistoryPrevUvScaleAndLimit");

		// Token: 0x04000B8B RID: 2955
		public static readonly int _VBufferPrevDepthEncodingParams = Shader.PropertyToID("_VBufferPrevDepthEncodingParams");

		// Token: 0x04000B8C RID: 2956
		public static readonly int _VBufferPrevDepthDecodingParams = Shader.PropertyToID("_VBufferPrevDepthDecodingParams");

		// Token: 0x04000B8D RID: 2957
		public static readonly int _VBufferLastSliceDist = Shader.PropertyToID("_VBufferLastSliceDist");

		// Token: 0x04000B8E RID: 2958
		public static readonly int _VBufferCoordToViewDirWS = Shader.PropertyToID("_VBufferCoordToViewDirWS");

		// Token: 0x04000B8F RID: 2959
		public static readonly int _VBufferUnitDepthTexelSpacing = Shader.PropertyToID("_VBufferUnitDepthTexelSpacing");

		// Token: 0x04000B90 RID: 2960
		public static readonly int _VBufferDensity = Shader.PropertyToID("_VBufferDensity");

		// Token: 0x04000B91 RID: 2961
		public static readonly int _VBufferLighting = Shader.PropertyToID("_VBufferLighting");

		// Token: 0x04000B92 RID: 2962
		public static readonly int _VBufferLightingIntegral = Shader.PropertyToID("_VBufferLightingIntegral");

		// Token: 0x04000B93 RID: 2963
		public static readonly int _VBufferLightingHistory = Shader.PropertyToID("_VBufferLightingHistory");

		// Token: 0x04000B94 RID: 2964
		public static readonly int _VBufferLightingHistoryIsValid = Shader.PropertyToID("_VBufferLightingHistoryIsValid");

		// Token: 0x04000B95 RID: 2965
		public static readonly int _VBufferLightingFeedback = Shader.PropertyToID("_VBufferLightingFeedback");

		// Token: 0x04000B96 RID: 2966
		public static readonly int _VBufferSampleOffset = Shader.PropertyToID("_VBufferSampleOffset");

		// Token: 0x04000B97 RID: 2967
		public static readonly int _VolumeBounds = Shader.PropertyToID("_VolumeBounds");

		// Token: 0x04000B98 RID: 2968
		public static readonly int _VolumeData = Shader.PropertyToID("_VolumeData");

		// Token: 0x04000B99 RID: 2969
		public static readonly int _NumVisibleDensityVolumes = Shader.PropertyToID("_NumVisibleDensityVolumes");

		// Token: 0x04000B9A RID: 2970
		public static readonly int _VolumeMaskAtlas = Shader.PropertyToID("_VolumeMaskAtlas");

		// Token: 0x04000B9B RID: 2971
		public static readonly int _VolumeMaskDimensions = Shader.PropertyToID("_VolumeMaskDimensions");

		// Token: 0x04000B9C RID: 2972
		public static readonly int _EnableLightLayers = Shader.PropertyToID("_EnableLightLayers");

		// Token: 0x04000B9D RID: 2973
		public static readonly int _OffScreenRendering = Shader.PropertyToID("_OffScreenRendering");

		// Token: 0x04000B9E RID: 2974
		public static readonly int _OffScreenDownsampleFactor = Shader.PropertyToID("_OffScreenDownsampleFactor");

		// Token: 0x04000B9F RID: 2975
		public static readonly int _ReplaceDiffuseForIndirect = Shader.PropertyToID("_ReplaceDiffuseForIndirect");

		// Token: 0x04000BA0 RID: 2976
		public static readonly int _EnableSkyReflection = Shader.PropertyToID("_EnableSkyReflection");

		// Token: 0x04000BA1 RID: 2977
		public static readonly int _GroundIrradianceTexture = Shader.PropertyToID("_GroundIrradianceTexture");

		// Token: 0x04000BA2 RID: 2978
		public static readonly int _GroundIrradianceTable = Shader.PropertyToID("_GroundIrradianceTable");

		// Token: 0x04000BA3 RID: 2979
		public static readonly int _GroundIrradianceTableOrder = Shader.PropertyToID("_GroundIrradianceTableOrder");

		// Token: 0x04000BA4 RID: 2980
		public static readonly int _AirSingleScatteringTexture = Shader.PropertyToID("_AirSingleScatteringTexture");

		// Token: 0x04000BA5 RID: 2981
		public static readonly int _AirSingleScatteringTable = Shader.PropertyToID("_AirSingleScatteringTable");

		// Token: 0x04000BA6 RID: 2982
		public static readonly int _AerosolSingleScatteringTexture = Shader.PropertyToID("_AerosolSingleScatteringTexture");

		// Token: 0x04000BA7 RID: 2983
		public static readonly int _AerosolSingleScatteringTable = Shader.PropertyToID("_AerosolSingleScatteringTable");

		// Token: 0x04000BA8 RID: 2984
		public static readonly int _MultipleScatteringTexture = Shader.PropertyToID("_MultipleScatteringTexture");

		// Token: 0x04000BA9 RID: 2985
		public static readonly int _MultipleScatteringTable = Shader.PropertyToID("_MultipleScatteringTable");

		// Token: 0x04000BAA RID: 2986
		public static readonly int _MultipleScatteringTableOrder = Shader.PropertyToID("_MultipleScatteringTableOrder");

		// Token: 0x04000BAB RID: 2987
		public static readonly int _PlanetaryRadius = Shader.PropertyToID("_PlanetaryRadius");

		// Token: 0x04000BAC RID: 2988
		public static readonly int _RcpPlanetaryRadius = Shader.PropertyToID("_RcpPlanetaryRadius");

		// Token: 0x04000BAD RID: 2989
		public static readonly int _AtmosphericDepth = Shader.PropertyToID("_AtmosphericDepth");

		// Token: 0x04000BAE RID: 2990
		public static readonly int _RcpAtmosphericDepth = Shader.PropertyToID("_RcpAtmosphericDepth");

		// Token: 0x04000BAF RID: 2991
		public static readonly int _AtmosphericRadius = Shader.PropertyToID("_AtmosphericRadius");

		// Token: 0x04000BB0 RID: 2992
		public static readonly int _AerosolAnisotropy = Shader.PropertyToID("_AerosolAnisotropy");

		// Token: 0x04000BB1 RID: 2993
		public static readonly int _AerosolPhasePartConstant = Shader.PropertyToID("_AerosolPhasePartConstant");

		// Token: 0x04000BB2 RID: 2994
		public static readonly int _AirDensityFalloff = Shader.PropertyToID("_AirDensityFalloff");

		// Token: 0x04000BB3 RID: 2995
		public static readonly int _AirScaleHeight = Shader.PropertyToID("_AirScaleHeight");

		// Token: 0x04000BB4 RID: 2996
		public static readonly int _AerosolDensityFalloff = Shader.PropertyToID("_AerosolDensityFalloff");

		// Token: 0x04000BB5 RID: 2997
		public static readonly int _AerosolScaleHeight = Shader.PropertyToID("_AerosolScaleHeight");

		// Token: 0x04000BB6 RID: 2998
		public static readonly int _AirSeaLevelExtinction = Shader.PropertyToID("_AirSeaLevelExtinction");

		// Token: 0x04000BB7 RID: 2999
		public static readonly int _AerosolSeaLevelExtinction = Shader.PropertyToID("_AerosolSeaLevelExtinction");

		// Token: 0x04000BB8 RID: 3000
		public static readonly int _AirSeaLevelScattering = Shader.PropertyToID("_AirSeaLevelScattering");

		// Token: 0x04000BB9 RID: 3001
		public static readonly int _AerosolSeaLevelScattering = Shader.PropertyToID("_AerosolSeaLevelScattering");

		// Token: 0x04000BBA RID: 3002
		public static readonly int _GroundAlbedo = Shader.PropertyToID("_GroundAlbedo");

		// Token: 0x04000BBB RID: 3003
		public static readonly int _IntensityMultiplier = Shader.PropertyToID("_IntensityMultiplier");

		// Token: 0x04000BBC RID: 3004
		public static readonly int _PlanetCenterPosition = Shader.PropertyToID("_PlanetCenterPosition");

		// Token: 0x04000BBD RID: 3005
		public static readonly int _PlanetRotation = Shader.PropertyToID("_PlanetRotation");

		// Token: 0x04000BBE RID: 3006
		public static readonly int _SpaceRotation = Shader.PropertyToID("_SpaceRotation");

		// Token: 0x04000BBF RID: 3007
		public static readonly int _HasGroundAlbedoTexture = Shader.PropertyToID("_HasGroundAlbedoTexture");

		// Token: 0x04000BC0 RID: 3008
		public static readonly int _GroundAlbedoTexture = Shader.PropertyToID("_GroundAlbedoTexture");

		// Token: 0x04000BC1 RID: 3009
		public static readonly int _HasGroundEmissionTexture = Shader.PropertyToID("_HasGroundEmissionTexture");

		// Token: 0x04000BC2 RID: 3010
		public static readonly int _GroundEmissionTexture = Shader.PropertyToID("_GroundEmissionTexture");

		// Token: 0x04000BC3 RID: 3011
		public static readonly int _GroundEmissionMultiplier = Shader.PropertyToID("_GroundEmissionMultiplier");

		// Token: 0x04000BC4 RID: 3012
		public static readonly int _HasSpaceEmissionTexture = Shader.PropertyToID("_HasSpaceEmissionTexture");

		// Token: 0x04000BC5 RID: 3013
		public static readonly int _SpaceEmissionTexture = Shader.PropertyToID("_SpaceEmissionTexture");

		// Token: 0x04000BC6 RID: 3014
		public static readonly int _SpaceEmissionMultiplier = Shader.PropertyToID("_SpaceEmissionMultiplier");

		// Token: 0x04000BC7 RID: 3015
		public static readonly int _RenderSunDisk = Shader.PropertyToID("_RenderSunDisk");

		// Token: 0x04000BC8 RID: 3016
		public static readonly int _ColorSaturation = Shader.PropertyToID("_ColorSaturation");

		// Token: 0x04000BC9 RID: 3017
		public static readonly int _AlphaSaturation = Shader.PropertyToID("_AlphaSaturation");

		// Token: 0x04000BCA RID: 3018
		public static readonly int _AlphaMultiplier = Shader.PropertyToID("_AlphaMultiplier");

		// Token: 0x04000BCB RID: 3019
		public static readonly int _HorizonTint = Shader.PropertyToID("_HorizonTint");

		// Token: 0x04000BCC RID: 3020
		public static readonly int _ZenithTint = Shader.PropertyToID("_ZenithTint");

		// Token: 0x04000BCD RID: 3021
		public static readonly int _HorizonZenithShiftPower = Shader.PropertyToID("_HorizonZenithShiftPower");

		// Token: 0x04000BCE RID: 3022
		public static readonly int _HorizonZenithShiftScale = Shader.PropertyToID("_HorizonZenithShiftScale");

		// Token: 0x04000BCF RID: 3023
		public static readonly int _RaytracingRayBias = Shader.PropertyToID("_RaytracingRayBias");

		// Token: 0x04000BD0 RID: 3024
		public static readonly int _RayTracingLayerMask = Shader.PropertyToID("_RayTracingLayerMask");

		// Token: 0x04000BD1 RID: 3025
		public static readonly int _RaytracingNumSamples = Shader.PropertyToID("_RaytracingNumSamples");

		// Token: 0x04000BD2 RID: 3026
		public static readonly int _RaytracingSampleIndex = Shader.PropertyToID("_RaytracingSampleIndex");

		// Token: 0x04000BD3 RID: 3027
		public static readonly int _RaytracingRayMaxLength = Shader.PropertyToID("_RaytracingRayMaxLength");

		// Token: 0x04000BD4 RID: 3028
		public static readonly int _PixelSpreadAngleTangent = Shader.PropertyToID("_PixelSpreadAngleTangent");

		// Token: 0x04000BD5 RID: 3029
		public static readonly int _RaytracingFrameIndex = Shader.PropertyToID("_RaytracingFrameIndex");

		// Token: 0x04000BD6 RID: 3030
		public static readonly int _RaytracingPixelSpreadAngle = Shader.PropertyToID("_RaytracingPixelSpreadAngle");

		// Token: 0x04000BD7 RID: 3031
		public static readonly string _RaytracingAccelerationStructureName = "_RaytracingAccelerationStructure";

		// Token: 0x04000BD8 RID: 3032
		public static readonly int _MinClusterPos = Shader.PropertyToID("_MinClusterPos");

		// Token: 0x04000BD9 RID: 3033
		public static readonly int _MaxClusterPos = Shader.PropertyToID("_MaxClusterPos");

		// Token: 0x04000BDA RID: 3034
		public static readonly int _LightPerCellCount = Shader.PropertyToID("_LightPerCellCount");

		// Token: 0x04000BDB RID: 3035
		public static readonly int _LightDatasRT = Shader.PropertyToID("_LightDatasRT");

		// Token: 0x04000BDC RID: 3036
		public static readonly int _EnvLightDatasRT = Shader.PropertyToID("_EnvLightDatasRT");

		// Token: 0x04000BDD RID: 3037
		public static readonly int _PunctualLightCountRT = Shader.PropertyToID("_PunctualLightCountRT");

		// Token: 0x04000BDE RID: 3038
		public static readonly int _AreaLightCountRT = Shader.PropertyToID("_AreaLightCountRT");

		// Token: 0x04000BDF RID: 3039
		public static readonly int _EnvLightCountRT = Shader.PropertyToID("_EnvLightCountRT");

		// Token: 0x04000BE0 RID: 3040
		public static readonly int _RaytracingLightCluster = Shader.PropertyToID("_RaytracingLightCluster");

		// Token: 0x04000BE1 RID: 3041
		public static readonly int _HistoryBuffer = Shader.PropertyToID("_HistoryBuffer");

		// Token: 0x04000BE2 RID: 3042
		public static readonly int _ValidationBuffer = Shader.PropertyToID("_ValidationBuffer");

		// Token: 0x04000BE3 RID: 3043
		public static readonly int _ValidationBufferRW = Shader.PropertyToID("_ValidationBufferRW");

		// Token: 0x04000BE4 RID: 3044
		public static readonly int _HistoryDepthTexture = Shader.PropertyToID("_HistoryDepthTexture");

		// Token: 0x04000BE5 RID: 3045
		public static readonly int _HistoryNormalBufferTexture = Shader.PropertyToID("_HistoryNormalBufferTexture");

		// Token: 0x04000BE6 RID: 3046
		public static readonly int _RaytracingDenoiseRadius = Shader.PropertyToID("_RaytracingDenoiseRadius");

		// Token: 0x04000BE7 RID: 3047
		public static readonly int _DenoiserFilterRadius = Shader.PropertyToID("_DenoiserFilterRadius");

		// Token: 0x04000BE8 RID: 3048
		public static readonly int _NormalHistoryCriterion = Shader.PropertyToID("_NormalHistoryCriterion");

		// Token: 0x04000BE9 RID: 3049
		public static readonly int _DenoiseInputTexture = Shader.PropertyToID("_DenoiseInputTexture");

		// Token: 0x04000BEA RID: 3050
		public static readonly int _DenoiseOutputTextureRW = Shader.PropertyToID("_DenoiseOutputTextureRW");

		// Token: 0x04000BEB RID: 3051
		public static readonly int _HalfResolutionFilter = Shader.PropertyToID("_HalfResolutionFilter");

		// Token: 0x04000BEC RID: 3052
		public static readonly int _DenoisingHistorySlot = Shader.PropertyToID("_DenoisingHistorySlot");

		// Token: 0x04000BED RID: 3053
		public static readonly int _HistoryValidity = Shader.PropertyToID("_HistoryValidity");

		// Token: 0x04000BEE RID: 3054
		public static readonly int _ReflectionFilterMapping = Shader.PropertyToID("_ReflectionFilterMapping");

		// Token: 0x04000BEF RID: 3055
		public static readonly int _DenoisingHistorySlice = Shader.PropertyToID("_DenoisingHistorySlice");

		// Token: 0x04000BF0 RID: 3056
		public static readonly int _DenoisingHistoryMask = Shader.PropertyToID("_DenoisingHistoryMask");

		// Token: 0x04000BF1 RID: 3057
		public static readonly int _DenoisingHistoryMaskSn = Shader.PropertyToID("_DenoisingHistoryMaskSn");

		// Token: 0x04000BF2 RID: 3058
		public static readonly int _DenoisingHistoryMaskUn = Shader.PropertyToID("_DenoisingHistoryMaskUn");

		// Token: 0x04000BF3 RID: 3059
		public static readonly int _HistoryValidityBuffer = Shader.PropertyToID("_HistoryValidityBuffer");

		// Token: 0x04000BF4 RID: 3060
		public static readonly int _ValidityOutputTextureRW = Shader.PropertyToID("_ValidityOutputTextureRW");

		// Token: 0x04000BF5 RID: 3061
		public static readonly int _VelocityBuffer = Shader.PropertyToID("_VelocityBuffer");

		// Token: 0x04000BF6 RID: 3062
		public static readonly int _ReflectionHistorybufferRW = Shader.PropertyToID("_ReflectionHistorybufferRW");

		// Token: 0x04000BF7 RID: 3063
		public static readonly int _CurrentFrameTexture = Shader.PropertyToID("_CurrentFrameTexture");

		// Token: 0x04000BF8 RID: 3064
		public static readonly int _AccumulatedFrameTexture = Shader.PropertyToID("_AccumulatedFrameTexture");

		// Token: 0x04000BF9 RID: 3065
		public static readonly int _TemporalAccumuationWeight = Shader.PropertyToID("_TemporalAccumuationWeight");

		// Token: 0x04000BFA RID: 3066
		public static readonly int _SpatialFilterRadius = Shader.PropertyToID("_SpatialFilterRadius");

		// Token: 0x04000BFB RID: 3067
		public static readonly int _RaytracingReflectionMaxDistance = Shader.PropertyToID("_RaytracingReflectionMaxDistance");

		// Token: 0x04000BFC RID: 3068
		public static readonly int _RaytracingHitDistanceTexture = Shader.PropertyToID("_RaytracingHitDistanceTexture");

		// Token: 0x04000BFD RID: 3069
		public static readonly int _RaytracingIntensityClamp = Shader.PropertyToID("_RaytracingIntensityClamp");

		// Token: 0x04000BFE RID: 3070
		public static readonly int _RaytracingPreExposition = Shader.PropertyToID("_RaytracingPreExposition");

		// Token: 0x04000BFF RID: 3071
		public static readonly int _RaytracingReflectionMinSmoothness = Shader.PropertyToID("_RaytracingReflectionMinSmoothness");

		// Token: 0x04000C00 RID: 3072
		public static readonly int _RaytracingReflectionSmoothnessFadeStart = Shader.PropertyToID("_RaytracingReflectionSmoothnessFadeStart");

		// Token: 0x04000C01 RID: 3073
		public static readonly int _RaytracingVSNormalTexture = Shader.PropertyToID("_RaytracingVSNormalTexture");

		// Token: 0x04000C02 RID: 3074
		public static readonly int _RaytracingIncludeSky = Shader.PropertyToID("_RaytracingIncludeSky");

		// Token: 0x04000C03 RID: 3075
		public static readonly int _UseRayTracedReflections = Shader.PropertyToID("_UseRayTracedReflections");

		// Token: 0x04000C04 RID: 3076
		public static readonly int _RaytracingTargetAreaLight = Shader.PropertyToID("_RaytracingTargetAreaLight");

		// Token: 0x04000C05 RID: 3077
		public static readonly int _RaytracingShadowSlot = Shader.PropertyToID("_RaytracingShadowSlot");

		// Token: 0x04000C06 RID: 3078
		public static readonly int _RaytracingChannelMask = Shader.PropertyToID("_RaytracingChannelMask");

		// Token: 0x04000C07 RID: 3079
		public static readonly int _RaytracingAreaWorldToLocal = Shader.PropertyToID("_RaytracingAreaWorldToLocal");

		// Token: 0x04000C08 RID: 3080
		public static readonly int _RaytracedAreaShadowSample = Shader.PropertyToID("_RaytracedAreaShadowSample");

		// Token: 0x04000C09 RID: 3081
		public static readonly int _RaytracedAreaShadowIntegration = Shader.PropertyToID("_RaytracedAreaShadowIntegration");

		// Token: 0x04000C0A RID: 3082
		public static readonly int _RaytracingDirectionBuffer = Shader.PropertyToID("_RaytracingDirectionBuffer");

		// Token: 0x04000C0B RID: 3083
		public static readonly int _RaytracingDistanceBuffer = Shader.PropertyToID("_RaytracingDistanceBuffer");

		// Token: 0x04000C0C RID: 3084
		public static readonly int _AreaShadowTexture = Shader.PropertyToID("_AreaShadowTexture");

		// Token: 0x04000C0D RID: 3085
		public static readonly int _AreaShadowTextureRW = Shader.PropertyToID("_AreaShadowTextureRW");

		// Token: 0x04000C0E RID: 3086
		public static readonly int _ScreenSpaceShadowsTextureRW = Shader.PropertyToID("_ScreenSpaceShadowsTextureRW");

		// Token: 0x04000C0F RID: 3087
		public static readonly int _AreaShadowHistory = Shader.PropertyToID("_AreaShadowHistory");

		// Token: 0x04000C10 RID: 3088
		public static readonly int _AreaShadowHistoryRW = Shader.PropertyToID("_AreaShadowHistoryRW");

		// Token: 0x04000C11 RID: 3089
		public static readonly int _AnalyticProbBuffer = Shader.PropertyToID("_AnalyticProbBuffer");

		// Token: 0x04000C12 RID: 3090
		public static readonly int _AnalyticHistoryBuffer = Shader.PropertyToID("_AnalyticHistoryBuffer");

		// Token: 0x04000C13 RID: 3091
		public static readonly int _RaytracingLightRadius = Shader.PropertyToID("_RaytracingLightRadius");

		// Token: 0x04000C14 RID: 3092
		public static readonly int _RaytracingSpotAngle = Shader.PropertyToID("_RaytracingSpotAngle");

		// Token: 0x04000C15 RID: 3093
		public static readonly int _RaytracedShadowIntegration = Shader.PropertyToID("_RaytracedShadowIntegration");

		// Token: 0x04000C16 RID: 3094
		public static readonly int _RaytracedColorShadowIntegration = Shader.PropertyToID("_RaytracedColorShadowIntegration");

		// Token: 0x04000C17 RID: 3095
		public static readonly int _DirectionalLightAngle = Shader.PropertyToID("_DirectionalLightAngle");

		// Token: 0x04000C18 RID: 3096
		public static readonly int _RaytracingAOIntensity = Shader.PropertyToID("_RaytracingAOIntensity");

		// Token: 0x04000C19 RID: 3097
		public static readonly int _RayCountEnabled = Shader.PropertyToID("_RayCountEnabled");

		// Token: 0x04000C1A RID: 3098
		public static readonly int _RayCountTexture = Shader.PropertyToID("_RayCountTexture");

		// Token: 0x04000C1B RID: 3099
		public static readonly int _RayCountType = Shader.PropertyToID("_RayCountType");

		// Token: 0x04000C1C RID: 3100
		public static readonly int _InputRayCountTexture = Shader.PropertyToID("_InputRayCountTexture");

		// Token: 0x04000C1D RID: 3101
		public static readonly int _InputRayCountBuffer = Shader.PropertyToID("_InputRayCountBuffer");

		// Token: 0x04000C1E RID: 3102
		public static readonly int _OutputRayCountBuffer = Shader.PropertyToID("_OutputRayCountBuffer");

		// Token: 0x04000C1F RID: 3103
		public static readonly int _InputBufferDimension = Shader.PropertyToID("_InputBufferDimension");

		// Token: 0x04000C20 RID: 3104
		public static readonly int _OutputBufferDimension = Shader.PropertyToID("_OutputBufferDimension");

		// Token: 0x04000C21 RID: 3105
		public static readonly int _RaytracingFlagMask = Shader.PropertyToID("_RaytracingFlagMask");

		// Token: 0x04000C22 RID: 3106
		public static readonly int _RaytracingMinRecursion = Shader.PropertyToID("_RaytracingMinRecursion");

		// Token: 0x04000C23 RID: 3107
		public static readonly int _RaytracingMaxRecursion = Shader.PropertyToID("_RaytracingMaxRecursion");

		// Token: 0x04000C24 RID: 3108
		public static readonly int _RaytracingPrimaryDebug = Shader.PropertyToID("_RaytracingPrimaryDebug");

		// Token: 0x04000C25 RID: 3109
		public static readonly int _RaytracingCameraNearPlane = Shader.PropertyToID("_RaytracingCameraNearPlane");

		// Token: 0x04000C26 RID: 3110
		public static readonly int _RaytracedIndirectDiffuse = Shader.PropertyToID("_RaytracedIndirectDiffuse");

		// Token: 0x04000C27 RID: 3111
		public static readonly int _IndirectDiffuseTexture = Shader.PropertyToID("_IndirectDiffuseTexture");

		// Token: 0x04000C28 RID: 3112
		public static readonly int _IndirectDiffuseTextureRW = Shader.PropertyToID("_IndirectDiffuseTextureRW");

		// Token: 0x04000C29 RID: 3113
		public static readonly int _IndirectDiffuseHitPointTextureRW = Shader.PropertyToID("_IndirectDiffuseHitPointTextureRW");

		// Token: 0x04000C2A RID: 3114
		public static readonly int _UpscaledIndirectDiffuseTextureRW = Shader.PropertyToID("_UpscaledIndirectDiffuseTextureRW");

		// Token: 0x04000C2B RID: 3115
		public static readonly int _RaytracingLitBufferRW = Shader.PropertyToID("_RaytracingLitBufferRW");

		// Token: 0x04000C2C RID: 3116
		public static readonly int _RaytracingDiffuseRay = Shader.PropertyToID("_RaytracingDiffuseRay");

		// Token: 0x04000C2D RID: 3117
		public static readonly int _RayBinResult = Shader.PropertyToID("_RayBinResult");

		// Token: 0x04000C2E RID: 3118
		public static readonly int _RayBinSizeResult = Shader.PropertyToID("_RayBinSizeResult");

		// Token: 0x04000C2F RID: 3119
		public static readonly int _RayBinTileCountX = Shader.PropertyToID("_RayBinTileCountX");

		// Token: 0x04000C30 RID: 3120
		public static readonly int _ThroughputTextureRW = Shader.PropertyToID("_ThroughputTextureRW");

		// Token: 0x04000C31 RID: 3121
		public static readonly int _NormalTextureRW = Shader.PropertyToID("_NormalTextureRW");

		// Token: 0x04000C32 RID: 3122
		public static readonly int _PositionTextureRW = Shader.PropertyToID("_PositionTextureRW");

		// Token: 0x04000C33 RID: 3123
		public static readonly int _DiffuseLightingTextureRW = Shader.PropertyToID("_DiffuseLightingTextureRW");

		// Token: 0x04000C34 RID: 3124
		public static readonly int _PreIntegratedFGD_GGXDisneyDiffuse = Shader.PropertyToID("_PreIntegratedFGD_GGXDisneyDiffuse");

		// Token: 0x04000C35 RID: 3125
		public static readonly int _PreIntegratedFGD_CharlieAndFabric = Shader.PropertyToID("_PreIntegratedFGD_CharlieAndFabric");

		// Token: 0x04000C36 RID: 3126
		public static readonly int _ExposureTexture = Shader.PropertyToID("_ExposureTexture");

		// Token: 0x04000C37 RID: 3127
		public static readonly int _PrevExposureTexture = Shader.PropertyToID("_PrevExposureTexture");

		// Token: 0x04000C38 RID: 3128
		public static readonly int _PreviousExposureTexture = Shader.PropertyToID("_PreviousExposureTexture");

		// Token: 0x04000C39 RID: 3129
		public static readonly int _ExposureParams = Shader.PropertyToID("_ExposureParams");

		// Token: 0x04000C3A RID: 3130
		public static readonly int _AdaptationParams = Shader.PropertyToID("_AdaptationParams");

		// Token: 0x04000C3B RID: 3131
		public static readonly int _ExposureCurveTexture = Shader.PropertyToID("_ExposureCurveTexture");

		// Token: 0x04000C3C RID: 3132
		public static readonly int _ProbeExposureScale = Shader.PropertyToID("_ProbeExposureScale");

		// Token: 0x04000C3D RID: 3133
		public static readonly int _Variants = Shader.PropertyToID("_Variants");

		// Token: 0x04000C3E RID: 3134
		public static readonly int _InputTexture = Shader.PropertyToID("_InputTexture");

		// Token: 0x04000C3F RID: 3135
		public static readonly int _OutputTexture = Shader.PropertyToID("_OutputTexture");

		// Token: 0x04000C40 RID: 3136
		public static readonly int _SourceTexture = Shader.PropertyToID("_SourceTexture");

		// Token: 0x04000C41 RID: 3137
		public static readonly int _InputHistoryTexture = Shader.PropertyToID("_InputHistoryTexture");

		// Token: 0x04000C42 RID: 3138
		public static readonly int _OutputHistoryTexture = Shader.PropertyToID("_OutputHistoryTexture");

		// Token: 0x04000C43 RID: 3139
		public static readonly int _TargetScale = Shader.PropertyToID("_TargetScale");

		// Token: 0x04000C44 RID: 3140
		public static readonly int _Params = Shader.PropertyToID("_Params");

		// Token: 0x04000C45 RID: 3141
		public static readonly int _Params1 = Shader.PropertyToID("_Params1");

		// Token: 0x04000C46 RID: 3142
		public static readonly int _Params2 = Shader.PropertyToID("_Params2");

		// Token: 0x04000C47 RID: 3143
		public static readonly int _BokehKernel = Shader.PropertyToID("_BokehKernel");

		// Token: 0x04000C48 RID: 3144
		public static readonly int _InputCoCTexture = Shader.PropertyToID("_InputCoCTexture");

		// Token: 0x04000C49 RID: 3145
		public static readonly int _InputHistoryCoCTexture = Shader.PropertyToID("_InputHistoryCoCTexture");

		// Token: 0x04000C4A RID: 3146
		public static readonly int _OutputCoCTexture = Shader.PropertyToID("_OutputCoCTexture");

		// Token: 0x04000C4B RID: 3147
		public static readonly int _OutputNearCoCTexture = Shader.PropertyToID("_OutputNearCoCTexture");

		// Token: 0x04000C4C RID: 3148
		public static readonly int _OutputNearTexture = Shader.PropertyToID("_OutputNearTexture");

		// Token: 0x04000C4D RID: 3149
		public static readonly int _OutputFarCoCTexture = Shader.PropertyToID("_OutputFarCoCTexture");

		// Token: 0x04000C4E RID: 3150
		public static readonly int _OutputFarTexture = Shader.PropertyToID("_OutputFarTexture");

		// Token: 0x04000C4F RID: 3151
		public static readonly int _OutputMip1 = Shader.PropertyToID("_OutputMip1");

		// Token: 0x04000C50 RID: 3152
		public static readonly int _OutputMip2 = Shader.PropertyToID("_OutputMip2");

		// Token: 0x04000C51 RID: 3153
		public static readonly int _OutputMip3 = Shader.PropertyToID("_OutputMip3");

		// Token: 0x04000C52 RID: 3154
		public static readonly int _OutputMip4 = Shader.PropertyToID("_OutputMip4");

		// Token: 0x04000C53 RID: 3155
		public static readonly int _IndirectBuffer = Shader.PropertyToID("_IndirectBuffer");

		// Token: 0x04000C54 RID: 3156
		public static readonly int _InputNearCoCTexture = Shader.PropertyToID("_InputNearCoCTexture");

		// Token: 0x04000C55 RID: 3157
		public static readonly int _NearTileList = Shader.PropertyToID("_NearTileList");

		// Token: 0x04000C56 RID: 3158
		public static readonly int _InputFarTexture = Shader.PropertyToID("_InputFarTexture");

		// Token: 0x04000C57 RID: 3159
		public static readonly int _InputNearTexture = Shader.PropertyToID("_InputNearTexture");

		// Token: 0x04000C58 RID: 3160
		public static readonly int _InputFarCoCTexture = Shader.PropertyToID("_InputFarCoCTexture");

		// Token: 0x04000C59 RID: 3161
		public static readonly int _FarTileList = Shader.PropertyToID("_FarTileList");

		// Token: 0x04000C5A RID: 3162
		public static readonly int _TileList = Shader.PropertyToID("_TileList");

		// Token: 0x04000C5B RID: 3163
		public static readonly int _TexelSize = Shader.PropertyToID("_TexelSize");

		// Token: 0x04000C5C RID: 3164
		public static readonly int _InputDilatedCoCTexture = Shader.PropertyToID("_InputDilatedCoCTexture");

		// Token: 0x04000C5D RID: 3165
		public static readonly int _OutputAlphaTexture = Shader.PropertyToID("_OutputAlphaTexture");

		// Token: 0x04000C5E RID: 3166
		public static readonly int _InputNearAlphaTexture = Shader.PropertyToID("_InputNearAlphaTexture");

		// Token: 0x04000C5F RID: 3167
		public static readonly int _CoCTargetScale = Shader.PropertyToID("_CoCTargetScale");

		// Token: 0x04000C60 RID: 3168
		public static readonly int _BloomParams = Shader.PropertyToID("_BloomParams");

		// Token: 0x04000C61 RID: 3169
		public static readonly int _BloomTint = Shader.PropertyToID("_BloomTint");

		// Token: 0x04000C62 RID: 3170
		public static readonly int _BloomTexture = Shader.PropertyToID("_BloomTexture");

		// Token: 0x04000C63 RID: 3171
		public static readonly int _BloomDirtTexture = Shader.PropertyToID("_BloomDirtTexture");

		// Token: 0x04000C64 RID: 3172
		public static readonly int _BloomDirtScaleOffset = Shader.PropertyToID("_BloomDirtScaleOffset");

		// Token: 0x04000C65 RID: 3173
		public static readonly int _InputLowTexture = Shader.PropertyToID("_InputLowTexture");

		// Token: 0x04000C66 RID: 3174
		public static readonly int _InputHighTexture = Shader.PropertyToID("_InputHighTexture");

		// Token: 0x04000C67 RID: 3175
		public static readonly int _BloomBicubicParams = Shader.PropertyToID("_BloomBicubicParams");

		// Token: 0x04000C68 RID: 3176
		public static readonly int _BloomThreshold = Shader.PropertyToID("_BloomThreshold");

		// Token: 0x04000C69 RID: 3177
		public static readonly int _ChromaSpectralLut = Shader.PropertyToID("_ChromaSpectralLut");

		// Token: 0x04000C6A RID: 3178
		public static readonly int _ChromaParams = Shader.PropertyToID("_ChromaParams");

		// Token: 0x04000C6B RID: 3179
		public static readonly int _VignetteParams1 = Shader.PropertyToID("_VignetteParams1");

		// Token: 0x04000C6C RID: 3180
		public static readonly int _VignetteParams2 = Shader.PropertyToID("_VignetteParams2");

		// Token: 0x04000C6D RID: 3181
		public static readonly int _VignetteColor = Shader.PropertyToID("_VignetteColor");

		// Token: 0x04000C6E RID: 3182
		public static readonly int _VignetteMask = Shader.PropertyToID("_VignetteMask");

		// Token: 0x04000C6F RID: 3183
		public static readonly int _DistortionParams1 = Shader.PropertyToID("_DistortionParams1");

		// Token: 0x04000C70 RID: 3184
		public static readonly int _DistortionParams2 = Shader.PropertyToID("_DistortionParams2");

		// Token: 0x04000C71 RID: 3185
		public static readonly int _LogLut3D = Shader.PropertyToID("_LogLut3D");

		// Token: 0x04000C72 RID: 3186
		public static readonly int _LogLut3D_Params = Shader.PropertyToID("_LogLut3D_Params");

		// Token: 0x04000C73 RID: 3187
		public static readonly int _ColorBalance = Shader.PropertyToID("_ColorBalance");

		// Token: 0x04000C74 RID: 3188
		public static readonly int _ColorFilter = Shader.PropertyToID("_ColorFilter");

		// Token: 0x04000C75 RID: 3189
		public static readonly int _ChannelMixerRed = Shader.PropertyToID("_ChannelMixerRed");

		// Token: 0x04000C76 RID: 3190
		public static readonly int _ChannelMixerGreen = Shader.PropertyToID("_ChannelMixerGreen");

		// Token: 0x04000C77 RID: 3191
		public static readonly int _ChannelMixerBlue = Shader.PropertyToID("_ChannelMixerBlue");

		// Token: 0x04000C78 RID: 3192
		public static readonly int _HueSatCon = Shader.PropertyToID("_HueSatCon");

		// Token: 0x04000C79 RID: 3193
		public static readonly int _Lift = Shader.PropertyToID("_Lift");

		// Token: 0x04000C7A RID: 3194
		public static readonly int _Gamma = Shader.PropertyToID("_Gamma");

		// Token: 0x04000C7B RID: 3195
		public static readonly int _Gain = Shader.PropertyToID("_Gain");

		// Token: 0x04000C7C RID: 3196
		public static readonly int _Shadows = Shader.PropertyToID("_Shadows");

		// Token: 0x04000C7D RID: 3197
		public static readonly int _Midtones = Shader.PropertyToID("_Midtones");

		// Token: 0x04000C7E RID: 3198
		public static readonly int _Highlights = Shader.PropertyToID("_Highlights");

		// Token: 0x04000C7F RID: 3199
		public static readonly int _ShaHiLimits = Shader.PropertyToID("_ShaHiLimits");

		// Token: 0x04000C80 RID: 3200
		public static readonly int _SplitShadows = Shader.PropertyToID("_SplitShadows");

		// Token: 0x04000C81 RID: 3201
		public static readonly int _SplitHighlights = Shader.PropertyToID("_SplitHighlights");

		// Token: 0x04000C82 RID: 3202
		public static readonly int _CurveMaster = Shader.PropertyToID("_CurveMaster");

		// Token: 0x04000C83 RID: 3203
		public static readonly int _CurveRed = Shader.PropertyToID("_CurveRed");

		// Token: 0x04000C84 RID: 3204
		public static readonly int _CurveGreen = Shader.PropertyToID("_CurveGreen");

		// Token: 0x04000C85 RID: 3205
		public static readonly int _CurveBlue = Shader.PropertyToID("_CurveBlue");

		// Token: 0x04000C86 RID: 3206
		public static readonly int _CurveHueVsHue = Shader.PropertyToID("_CurveHueVsHue");

		// Token: 0x04000C87 RID: 3207
		public static readonly int _CurveHueVsSat = Shader.PropertyToID("_CurveHueVsSat");

		// Token: 0x04000C88 RID: 3208
		public static readonly int _CurveSatVsSat = Shader.PropertyToID("_CurveSatVsSat");

		// Token: 0x04000C89 RID: 3209
		public static readonly int _CurveLumVsSat = Shader.PropertyToID("_CurveLumVsSat");

		// Token: 0x04000C8A RID: 3210
		public static readonly int _CustomToneCurve = Shader.PropertyToID("_CustomToneCurve");

		// Token: 0x04000C8B RID: 3211
		public static readonly int _ToeSegmentA = Shader.PropertyToID("_ToeSegmentA");

		// Token: 0x04000C8C RID: 3212
		public static readonly int _ToeSegmentB = Shader.PropertyToID("_ToeSegmentB");

		// Token: 0x04000C8D RID: 3213
		public static readonly int _MidSegmentA = Shader.PropertyToID("_MidSegmentA");

		// Token: 0x04000C8E RID: 3214
		public static readonly int _MidSegmentB = Shader.PropertyToID("_MidSegmentB");

		// Token: 0x04000C8F RID: 3215
		public static readonly int _ShoSegmentA = Shader.PropertyToID("_ShoSegmentA");

		// Token: 0x04000C90 RID: 3216
		public static readonly int _ShoSegmentB = Shader.PropertyToID("_ShoSegmentB");

		// Token: 0x04000C91 RID: 3217
		public static readonly int _Depth = Shader.PropertyToID("_Depth");

		// Token: 0x04000C92 RID: 3218
		public static readonly int _LinearZ = Shader.PropertyToID("_LinearZ");

		// Token: 0x04000C93 RID: 3219
		public static readonly int _DS2x = Shader.PropertyToID("_DS2x");

		// Token: 0x04000C94 RID: 3220
		public static readonly int _DS4x = Shader.PropertyToID("_DS4x");

		// Token: 0x04000C95 RID: 3221
		public static readonly int _DS8x = Shader.PropertyToID("_DS8x");

		// Token: 0x04000C96 RID: 3222
		public static readonly int _DS16x = Shader.PropertyToID("_DS16x");

		// Token: 0x04000C97 RID: 3223
		public static readonly int _DS2xAtlas = Shader.PropertyToID("_DS2xAtlas");

		// Token: 0x04000C98 RID: 3224
		public static readonly int _DS4xAtlas = Shader.PropertyToID("_DS4xAtlas");

		// Token: 0x04000C99 RID: 3225
		public static readonly int _DS8xAtlas = Shader.PropertyToID("_DS8xAtlas");

		// Token: 0x04000C9A RID: 3226
		public static readonly int _DS16xAtlas = Shader.PropertyToID("_DS16xAtlas");

		// Token: 0x04000C9B RID: 3227
		public static readonly int _InvThicknessTable = Shader.PropertyToID("_InvThicknessTable");

		// Token: 0x04000C9C RID: 3228
		public static readonly int _SampleWeightTable = Shader.PropertyToID("_SampleWeightTable");

		// Token: 0x04000C9D RID: 3229
		public static readonly int _InvSliceDimension = Shader.PropertyToID("_InvSliceDimension");

		// Token: 0x04000C9E RID: 3230
		public static readonly int _AdditionalParams = Shader.PropertyToID("_AdditionalParams");

		// Token: 0x04000C9F RID: 3231
		public static readonly int _Occlusion = Shader.PropertyToID("_Occlusion");

		// Token: 0x04000CA0 RID: 3232
		public static readonly int _InvLowResolution = Shader.PropertyToID("_InvLowResolution");

		// Token: 0x04000CA1 RID: 3233
		public static readonly int _InvHighResolution = Shader.PropertyToID("_InvHighResolution");

		// Token: 0x04000CA2 RID: 3234
		public static readonly int _LoResDB = Shader.PropertyToID("_LoResDB");

		// Token: 0x04000CA3 RID: 3235
		public static readonly int _HiResDB = Shader.PropertyToID("_HiResDB");

		// Token: 0x04000CA4 RID: 3236
		public static readonly int _LoResAO1 = Shader.PropertyToID("_LoResAO1");

		// Token: 0x04000CA5 RID: 3237
		public static readonly int _HiResAO = Shader.PropertyToID("_HiResAO");

		// Token: 0x04000CA6 RID: 3238
		public static readonly int _AoResult = Shader.PropertyToID("_AoResult");

		// Token: 0x04000CA7 RID: 3239
		public static readonly int _GrainTexture = Shader.PropertyToID("_GrainTexture");

		// Token: 0x04000CA8 RID: 3240
		public static readonly int _GrainParams = Shader.PropertyToID("_GrainParams");

		// Token: 0x04000CA9 RID: 3241
		public static readonly int _GrainTextureParams = Shader.PropertyToID("_GrainTextureParams");

		// Token: 0x04000CAA RID: 3242
		public static readonly int _BlueNoiseTexture = Shader.PropertyToID("_BlueNoiseTexture");

		// Token: 0x04000CAB RID: 3243
		public static readonly int _AlphaTexture = Shader.PropertyToID("_AlphaTexture");

		// Token: 0x04000CAC RID: 3244
		public static readonly int _OwenScrambledRGTexture = Shader.PropertyToID("_OwenScrambledRGTexture");

		// Token: 0x04000CAD RID: 3245
		public static readonly int _OwenScrambledTexture = Shader.PropertyToID("_OwenScrambledTexture");

		// Token: 0x04000CAE RID: 3246
		public static readonly int _ScramblingTileXSPP = Shader.PropertyToID("_ScramblingTileXSPP");

		// Token: 0x04000CAF RID: 3247
		public static readonly int _RankingTileXSPP = Shader.PropertyToID("_RankingTileXSPP");

		// Token: 0x04000CB0 RID: 3248
		public static readonly int _ScramblingTexture = Shader.PropertyToID("_ScramblingTexture");

		// Token: 0x04000CB1 RID: 3249
		public static readonly int _AfterPostProcessTexture = Shader.PropertyToID("_AfterPostProcessTexture");

		// Token: 0x04000CB2 RID: 3250
		public static readonly int _DitherParams = Shader.PropertyToID("_DitherParams");

		// Token: 0x04000CB3 RID: 3251
		public static readonly int _KeepAlpha = Shader.PropertyToID("_KeepAlpha");

		// Token: 0x04000CB4 RID: 3252
		public static readonly int _UVTransform = Shader.PropertyToID("_UVTransform");

		// Token: 0x04000CB5 RID: 3253
		public static readonly int _MotionVecAndDepth = Shader.PropertyToID("_MotionVecAndDepth");

		// Token: 0x04000CB6 RID: 3254
		public static readonly int _TileMinMaxMotionVec = Shader.PropertyToID("_TileMinMaxMotionVec");

		// Token: 0x04000CB7 RID: 3255
		public static readonly int _TileMaxNeighbourhood = Shader.PropertyToID("_TileMaxNeighbourhood");

		// Token: 0x04000CB8 RID: 3256
		public static readonly int _TileToScatterMax = Shader.PropertyToID("_TileToScatterMax");

		// Token: 0x04000CB9 RID: 3257
		public static readonly int _TileToScatterMin = Shader.PropertyToID("_TileToScatterMin");

		// Token: 0x04000CBA RID: 3258
		public static readonly int _TileTargetSize = Shader.PropertyToID("_TileTargetSize");

		// Token: 0x04000CBB RID: 3259
		public static readonly int _MotionBlurParams = Shader.PropertyToID("_MotionBlurParams0");

		// Token: 0x04000CBC RID: 3260
		public static readonly int _MotionBlurParams1 = Shader.PropertyToID("_MotionBlurParams1");

		// Token: 0x04000CBD RID: 3261
		public static readonly int _MotionBlurParams2 = Shader.PropertyToID("_MotionBlurParams2");

		// Token: 0x04000CBE RID: 3262
		public static readonly int _PrevVPMatrixNoTranslation = Shader.PropertyToID("_PrevVPMatrixNoTranslation");

		// Token: 0x04000CBF RID: 3263
		public static readonly int _SMAAAreaTex = Shader.PropertyToID("_AreaTex");

		// Token: 0x04000CC0 RID: 3264
		public static readonly int _SMAASearchTex = Shader.PropertyToID("_SearchTex");

		// Token: 0x04000CC1 RID: 3265
		public static readonly int _SMAABlendTex = Shader.PropertyToID("_BlendTex");

		// Token: 0x04000CC2 RID: 3266
		public static readonly int _SMAARTMetrics = Shader.PropertyToID("_SMAARTMetrics");

		// Token: 0x04000CC3 RID: 3267
		public static readonly int _LowResDepthTexture = Shader.PropertyToID("_LowResDepthTexture");

		// Token: 0x04000CC4 RID: 3268
		public static readonly int _LowResTransparent = Shader.PropertyToID("_LowResTransparent");

		// Token: 0x04000CC5 RID: 3269
		public static readonly int _AOBufferSize = Shader.PropertyToID("_AOBufferSize");

		// Token: 0x04000CC6 RID: 3270
		public static readonly int _AOParams0 = Shader.PropertyToID("_AOParams0");

		// Token: 0x04000CC7 RID: 3271
		public static readonly int _AOParams1 = Shader.PropertyToID("_AOParams1");

		// Token: 0x04000CC8 RID: 3272
		public static readonly int _AOParams2 = Shader.PropertyToID("_AOParams2");

		// Token: 0x04000CC9 RID: 3273
		public static readonly int _AOParams3 = Shader.PropertyToID("_AOParams3");

		// Token: 0x04000CCA RID: 3274
		public static readonly int _AOParams4 = Shader.PropertyToID("_AOParams4");

		// Token: 0x04000CCB RID: 3275
		public static readonly int _FirstTwoDepthMipOffsets = Shader.PropertyToID("_FirstTwoDepthMipOffsets");

		// Token: 0x04000CCC RID: 3276
		public static readonly int _OcclusionTexture = Shader.PropertyToID("_OcclusionTexture");

		// Token: 0x04000CCD RID: 3277
		public static readonly int _BentNormalsTexture = Shader.PropertyToID("_BentNormalsTexture");

		// Token: 0x04000CCE RID: 3278
		public static readonly int _AOPackedData = Shader.PropertyToID("_AOPackedData");

		// Token: 0x04000CCF RID: 3279
		public static readonly int _AOPackedHistory = Shader.PropertyToID("_AOPackedHistory");

		// Token: 0x04000CD0 RID: 3280
		public static readonly int _AODepthToViewParams = Shader.PropertyToID("_AODepthToViewParams");

		// Token: 0x04000CD1 RID: 3281
		public static readonly int _AOPackedBlurred = Shader.PropertyToID("_AOPackedBlurred");

		// Token: 0x04000CD2 RID: 3282
		public static readonly int _AOOutputHistory = Shader.PropertyToID("_AOOutputHistory");

		// Token: 0x04000CD3 RID: 3283
		public static readonly int _Sharpness = Shader.PropertyToID("Sharpness");

		// Token: 0x04000CD4 RID: 3284
		public static readonly int _InputTextureDimensions = Shader.PropertyToID("InputTextureDimensions");

		// Token: 0x04000CD5 RID: 3285
		public static readonly int _OutputTextureDimensions = Shader.PropertyToID("OutputTextureDimensions");

		// Token: 0x04000CD6 RID: 3286
		public static readonly int _InputTex = Shader.PropertyToID("_InputTex");

		// Token: 0x04000CD7 RID: 3287
		public static readonly int _LoD = Shader.PropertyToID("_LoD");

		// Token: 0x04000CD8 RID: 3288
		public static readonly int _FaceIndex = Shader.PropertyToID("_FaceIndex");
	}
}
