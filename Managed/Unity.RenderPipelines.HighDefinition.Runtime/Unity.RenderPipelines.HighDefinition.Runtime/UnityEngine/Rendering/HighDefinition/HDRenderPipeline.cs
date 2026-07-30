using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Experimental.Rendering.HighDefinition;
using UnityEngine.Experimental.Rendering.RenderGraphModule;
using UnityEngine.Rendering.HighDefinition.Attributes;
using UnityEngine.Rendering.LookDev;
using UnityEngine.VFX;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x02000073 RID: 115
	public class HDRenderPipeline : RenderPipeline, IDataProvider
	{
		// Token: 0x060002C3 RID: 707 RVA: 0x0000F99F File Offset: 0x0000DB9F
		private void UpdateSortKeysArray(int count)
		{
			if (this.m_SortKeys == null || count > this.m_SortKeys.Length)
			{
				this.m_SortKeys = new uint[count];
			}
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x0000F9C0 File Offset: 0x0000DBC0
		private Matrix4x4 GetWorldToViewMatrix(HDCamera hdCamera, int viewIndex)
		{
			Matrix4x4 matrix4x = (hdCamera.xr.enabled ? hdCamera.xr.GetViewMatrix(viewIndex) : hdCamera.camera.worldToCameraMatrix);
			return HDRenderPipeline.s_FlipMatrixLHSRHS * matrix4x;
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x060002C5 RID: 709 RVA: 0x0000F9FF File Offset: 0x0000DBFF
		private ComputeShader buildScreenAABBShader
		{
			get
			{
				return this.defaultResources.shaders.buildScreenAABBCS;
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x060002C6 RID: 710 RVA: 0x0000FA11 File Offset: 0x0000DC11
		private ComputeShader buildPerTileLightListShader
		{
			get
			{
				return this.defaultResources.shaders.buildPerTileLightListCS;
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x060002C7 RID: 711 RVA: 0x0000FA23 File Offset: 0x0000DC23
		private ComputeShader buildPerBigTileLightListShader
		{
			get
			{
				return this.defaultResources.shaders.buildPerBigTileLightListCS;
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x060002C8 RID: 712 RVA: 0x0000FA35 File Offset: 0x0000DC35
		private ComputeShader buildPerVoxelLightListShader
		{
			get
			{
				return this.defaultResources.shaders.buildPerVoxelLightListCS;
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x060002C9 RID: 713 RVA: 0x0000FA47 File Offset: 0x0000DC47
		private ComputeShader buildMaterialFlagsShader
		{
			get
			{
				return this.defaultResources.shaders.buildMaterialFlagsCS;
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x060002CA RID: 714 RVA: 0x0000FA59 File Offset: 0x0000DC59
		private ComputeShader buildDispatchIndirectShader
		{
			get
			{
				return this.defaultResources.shaders.buildDispatchIndirectCS;
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x060002CB RID: 715 RVA: 0x0000FA6B File Offset: 0x0000DC6B
		private ComputeShader clearDispatchIndirectShader
		{
			get
			{
				return this.defaultResources.shaders.clearDispatchIndirectCS;
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x060002CC RID: 716 RVA: 0x0000FA7D File Offset: 0x0000DC7D
		private ComputeShader deferredComputeShader
		{
			get
			{
				return this.defaultResources.shaders.deferredCS;
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x060002CD RID: 717 RVA: 0x0000FA8F File Offset: 0x0000DC8F
		private ComputeShader contactShadowComputeShader
		{
			get
			{
				return this.defaultResources.shaders.contactShadowCS;
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x060002CE RID: 718 RVA: 0x0000FAA1 File Offset: 0x0000DCA1
		private Shader screenSpaceShadowsShader
		{
			get
			{
				return this.defaultResources.shaders.screenSpaceShadowPS;
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x060002CF RID: 719 RVA: 0x0000FAB3 File Offset: 0x0000DCB3
		private Shader deferredTilePixelShader
		{
			get
			{
				return this.defaultResources.shaders.deferredTilePS;
			}
		}

		// Token: 0x060002D0 RID: 720 RVA: 0x0000FAC5 File Offset: 0x0000DCC5
		private Light GetCurrentSunLight()
		{
			return this.m_CurrentSunLight;
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x0000FACD File Offset: 0x0000DCCD
		private bool HasLightToCull()
		{
			return this.m_TotalLightCount > 0;
		}

		// Token: 0x060002D2 RID: 722 RVA: 0x0000FAD8 File Offset: 0x0000DCD8
		private static int GetNumTileBigTileX(HDCamera hdCamera)
		{
			return HDUtils.DivRoundUp((int)hdCamera.screenSize.x, LightDefinitions.s_TileSizeBigTile);
		}

		// Token: 0x060002D3 RID: 723 RVA: 0x0000FAF0 File Offset: 0x0000DCF0
		private static int GetNumTileBigTileY(HDCamera hdCamera)
		{
			return HDUtils.DivRoundUp((int)hdCamera.screenSize.y, LightDefinitions.s_TileSizeBigTile);
		}

		// Token: 0x060002D4 RID: 724 RVA: 0x0000FB08 File Offset: 0x0000DD08
		private static int GetNumTileFtplX(HDCamera hdCamera)
		{
			return HDUtils.DivRoundUp((int)hdCamera.screenSize.x, LightDefinitions.s_TileSizeFptl);
		}

		// Token: 0x060002D5 RID: 725 RVA: 0x0000FB20 File Offset: 0x0000DD20
		private static int GetNumTileFtplY(HDCamera hdCamera)
		{
			return HDUtils.DivRoundUp((int)hdCamera.screenSize.y, LightDefinitions.s_TileSizeFptl);
		}

		// Token: 0x060002D6 RID: 726 RVA: 0x0000FB38 File Offset: 0x0000DD38
		private static int GetNumTileClusteredX(HDCamera hdCamera)
		{
			return HDUtils.DivRoundUp((int)hdCamera.screenSize.x, LightDefinitions.s_TileSizeClustered);
		}

		// Token: 0x060002D7 RID: 727 RVA: 0x0000FB50 File Offset: 0x0000DD50
		private static int GetNumTileClusteredY(HDCamera hdCamera)
		{
			return HDUtils.DivRoundUp((int)hdCamera.screenSize.y, LightDefinitions.s_TileSizeClustered);
		}

		// Token: 0x060002D8 RID: 728 RVA: 0x0000FB68 File Offset: 0x0000DD68
		private void InitShadowSystem(HDRenderPipelineAsset hdAsset, RenderPipelineResources defaultResources)
		{
			this.m_ShadowInitParameters = hdAsset.currentPlatformRenderPipelineSettings.hdShadowInitParams;
			this.m_ShadowManager = HDShadowManager.instance;
			this.m_ShadowManager.InitShadowManager(defaultResources, this.m_ShadowInitParameters.directionalShadowsDepthBits, this.m_ShadowInitParameters.punctualLightShadowAtlas, this.m_ShadowInitParameters.areaLightShadowAtlas, this.m_ShadowInitParameters.maxShadowRequests, defaultResources.shaders.shadowClearPS);
		}

		// Token: 0x060002D9 RID: 729 RVA: 0x0000FBD4 File Offset: 0x0000DDD4
		private void DeinitShadowSystem()
		{
			if (this.m_ShadowManager != null)
			{
				this.m_ShadowManager.Dispose();
				this.m_ShadowManager = null;
			}
		}

		// Token: 0x060002DA RID: 730 RVA: 0x0000FBF0 File Offset: 0x0000DDF0
		private static bool GetFeatureVariantsEnabled(FrameSettings frameSettings)
		{
			return frameSettings.litShaderMode == LitShaderMode.Deferred && frameSettings.IsEnabled(FrameSettingsField.DeferredTile) && (frameSettings.IsEnabled(FrameSettingsField.ComputeLightVariants) || frameSettings.IsEnabled(FrameSettingsField.ComputeMaterialVariants));
		}

		// Token: 0x060002DB RID: 731 RVA: 0x0000FC1F File Offset: 0x0000DE1F
		private int GetDeferredLightingMaterialIndex(int outputSplitLighting, int shadowMask, int debugDisplay)
		{
			return outputSplitLighting | (shadowMask << 1) | (debugDisplay << 2);
		}

		// Token: 0x060002DC RID: 732 RVA: 0x0000FC2C File Offset: 0x0000DE2C
		private Material GetDeferredLightingMaterial(bool outputSplitLighting, bool shadowMask, bool debugDisplayEnabled)
		{
			int deferredLightingMaterialIndex = this.GetDeferredLightingMaterialIndex(outputSplitLighting ? 1 : 0, shadowMask ? 1 : 0, debugDisplayEnabled ? 1 : 0);
			return this.m_deferredLightingMaterial[deferredLightingMaterialIndex];
		}

		// Token: 0x060002DD RID: 733 RVA: 0x0000FC60 File Offset: 0x0000DE60
		private void InitializeLightLoop(IBLFilterBSDF[] iBLFilterBSDFArray)
		{
			GlobalLightLoopSettings lightLoopSettings = this.asset.currentPlatformRenderPipelineSettings.lightLoopSettings;
			this.m_lightList = new HDRenderPipeline.LightList();
			this.m_lightList.Allocate();
			this.m_DebugViewTilesMaterial = CoreUtils.CreateEngineMaterial(this.defaultResources.shaders.debugViewTilesPS);
			this.m_DebugHDShadowMapMaterial = CoreUtils.CreateEngineMaterial(this.defaultResources.shaders.debugHDShadowMapPS);
			this.m_DebugBlitMaterial = CoreUtils.CreateEngineMaterial(this.defaultResources.shaders.debugBlitQuad);
			this.m_MaxDirectionalLightsOnScreen = lightLoopSettings.maxDirectionalLightsOnScreen;
			this.m_MaxPunctualLightsOnScreen = lightLoopSettings.maxPunctualLightsOnScreen;
			this.m_MaxAreaLightsOnScreen = lightLoopSettings.maxAreaLightsOnScreen;
			this.m_MaxDecalsOnScreen = lightLoopSettings.maxDecalsOnScreen;
			this.m_MaxEnvLightsOnScreen = lightLoopSettings.maxEnvLightsOnScreen;
			this.m_MaxLightsOnScreen = this.m_MaxDirectionalLightsOnScreen + this.m_MaxPunctualLightsOnScreen + this.m_MaxAreaLightsOnScreen + this.m_MaxEnvLightsOnScreen;
			this.m_MaxPlanarReflectionOnScreen = lightLoopSettings.maxPlanarReflectionOnScreen;
			HDRenderPipeline.s_GenAABBKernel = this.buildScreenAABBShader.FindKernel("ScreenBoundsAABB");
			HDRenderPipeline.s_GenAABBKernel_Oblique = this.buildScreenAABBShader.FindKernel("ScreenBoundsAABB_Oblique");
			HDRenderPipeline.s_ClearVoxelAtomicKernel = this.buildPerVoxelLightListShader.FindKernel("ClearAtomic");
			HDRenderPipeline.s_GenListPerBigTileKernel = this.buildPerBigTileLightListShader.FindKernel("BigTileLightListGen");
			HDRenderPipeline.s_BuildDispatchIndirectKernel = this.buildDispatchIndirectShader.FindKernel("BuildDispatchIndirect");
			HDRenderPipeline.s_ClearDispatchIndirectKernel = this.clearDispatchIndirectShader.FindKernel("ClearDispatchIndirect");
			HDRenderPipeline.s_BuildDrawProceduralIndirectKernel = this.buildDispatchIndirectShader.FindKernel("BuildDrawProceduralIndirect");
			HDRenderPipeline.s_ClearDrawProceduralIndirectKernel = this.clearDispatchIndirectShader.FindKernel("ClearDrawProceduralIndirect");
			HDRenderPipeline.s_BuildMaterialFlagsOrKernel = this.buildMaterialFlagsShader.FindKernel("MaterialFlagsGen_Or");
			HDRenderPipeline.s_BuildMaterialFlagsWriteKernel = this.buildMaterialFlagsShader.FindKernel("MaterialFlagsGen_Write");
			HDRenderPipeline.s_shadeOpaqueDirectFptlKernel = this.deferredComputeShader.FindKernel("Deferred_Direct_Fptl");
			HDRenderPipeline.s_shadeOpaqueDirectFptlDebugDisplayKernel = this.deferredComputeShader.FindKernel("Deferred_Direct_Fptl_DebugDisplay");
			HDRenderPipeline.s_shadeOpaqueDirectShadowMaskFptlKernel = this.deferredComputeShader.FindKernel("Deferred_Direct_ShadowMask_Fptl");
			HDRenderPipeline.s_shadeOpaqueDirectShadowMaskFptlDebugDisplayKernel = this.deferredComputeShader.FindKernel("Deferred_Direct_ShadowMask_Fptl_DebugDisplay");
			HDRenderPipeline.s_deferredContactShadowKernel = this.contactShadowComputeShader.FindKernel("DeferredContactShadow");
			HDRenderPipeline.s_deferredContactShadowKernelMSAA = this.contactShadowComputeShader.FindKernel("DeferredContactShadowMSAA");
			for (int i = 0; i < LightDefinitions.s_NumFeatureVariants; i++)
			{
				HDRenderPipeline.s_shadeOpaqueIndirectFptlKernels[i] = this.deferredComputeShader.FindKernel("Deferred_Indirect_Fptl_Variant" + i);
				HDRenderPipeline.s_shadeOpaqueIndirectShadowMaskFptlKernels[i] = this.deferredComputeShader.FindKernel("Deferred_Indirect_ShadowMask_Fptl_Variant" + i);
			}
			this.m_TextureCaches.Initialize(this.asset, this.defaultResources, iBLFilterBSDFArray);
			this.m_LightLoopLightData.Initialize(this.m_MaxDirectionalLightsOnScreen, this.m_MaxPunctualLightsOnScreen, this.m_MaxAreaLightsOnScreen, this.m_MaxEnvLightsOnScreen, this.m_MaxDecalsOnScreen);
			this.m_TileAndClusterData.Initialize();
			this.m_deferredLightingMaterial = new Material[8];
			int num = 6;
			for (int j = 0; j < 2; j++)
			{
				for (int k = 0; k < 2; k++)
				{
					for (int l = 0; l < 2; l++)
					{
						int deferredLightingMaterialIndex = this.GetDeferredLightingMaterialIndex(j, k, l);
						this.m_deferredLightingMaterial[deferredLightingMaterialIndex] = CoreUtils.CreateEngineMaterial(this.defaultResources.shaders.deferredPS);
						this.m_deferredLightingMaterial[deferredLightingMaterialIndex].name = string.Format("{0}_{1}", this.defaultResources.shaders.deferredPS.name, deferredLightingMaterialIndex);
						CoreUtils.SetKeyword(this.m_deferredLightingMaterial[deferredLightingMaterialIndex], "OUTPUT_SPLIT_LIGHTING", j == 1);
						CoreUtils.SetKeyword(this.m_deferredLightingMaterial[deferredLightingMaterialIndex], "SHADOWS_SHADOWMASK", k == 1);
						CoreUtils.SetKeyword(this.m_deferredLightingMaterial[deferredLightingMaterialIndex], "DEBUG_DISPLAY", l == 1);
						int num2 = 2;
						if (j == 1)
						{
							num2 |= 4;
						}
						this.m_deferredLightingMaterial[deferredLightingMaterialIndex].SetInt(HDShaderIDs._StencilMask, num);
						this.m_deferredLightingMaterial[deferredLightingMaterialIndex].SetInt(HDShaderIDs._StencilRef, num2);
						this.m_deferredLightingMaterial[deferredLightingMaterialIndex].SetInt(HDShaderIDs._StencilCmp, 3);
					}
				}
			}
			HDRenderPipeline.s_DeferredTileRegularLightingMat = CoreUtils.CreateEngineMaterial(this.deferredTilePixelShader);
			HDRenderPipeline.s_DeferredTileRegularLightingMat.SetInt(HDShaderIDs._StencilMask, 6);
			HDRenderPipeline.s_DeferredTileRegularLightingMat.SetInt(HDShaderIDs._StencilRef, 2);
			HDRenderPipeline.s_DeferredTileRegularLightingMat.SetInt(HDShaderIDs._StencilCmp, 3);
			HDRenderPipeline.s_DeferredTileSplitLightingMat = CoreUtils.CreateEngineMaterial(this.deferredTilePixelShader);
			HDRenderPipeline.s_DeferredTileSplitLightingMat.SetInt(HDShaderIDs._StencilMask, 4);
			HDRenderPipeline.s_DeferredTileSplitLightingMat.SetInt(HDShaderIDs._StencilRef, 4);
			HDRenderPipeline.s_DeferredTileSplitLightingMat.SetInt(HDShaderIDs._StencilCmp, 3);
			HDRenderPipeline.s_DeferredTileMat = CoreUtils.CreateEngineMaterial(this.deferredTilePixelShader);
			HDRenderPipeline.s_DeferredTileMat.SetInt(HDShaderIDs._StencilMask, 2);
			HDRenderPipeline.s_DeferredTileMat.SetInt(HDShaderIDs._StencilRef, 0);
			HDRenderPipeline.s_DeferredTileMat.SetInt(HDShaderIDs._StencilCmp, 6);
			for (int m = 0; m < LightDefinitions.s_NumFeatureVariants; m++)
			{
				HDRenderPipeline.s_variantNames[m] = "VARIANT" + m;
			}
			this.m_DefaultTexture2DArray = new Texture2DArray(1, 1, 1, TextureFormat.ARGB32, false);
			this.m_DefaultTexture2DArray.hideFlags = HideFlags.HideAndDontSave;
			this.m_DefaultTexture2DArray.name = CoreUtils.GetTextureAutoName(1, 1, TextureFormat.ARGB32, TextureDimension.Tex2DArray, "LightLoopDefault", false, 1);
			this.m_DefaultTexture2DArray.SetPixels32(new Color32[]
			{
				new Color32(128, 128, 128, 128)
			}, 0);
			this.m_DefaultTexture2DArray.Apply();
			this.m_DefaultTextureCube = new Cubemap(16, TextureFormat.ARGB32, false);
			this.m_DefaultTextureCube.Apply();
			HDShadowInitParameters hdShadowInitParams = this.asset.currentPlatformRenderPipelineSettings.hdShadowInitParams;
			string[] array = new string[] { "SHADOW_LOW", "SHADOW_MEDIUM", "SHADOW_HIGH" };
			string[] array2 = array;
			for (int n = 0; n < array2.Length; n++)
			{
				Shader.DisableKeyword(array2[n]);
			}
			Shader.EnableKeyword(array[(int)hdShadowInitParams.shadowFilteringQuality]);
			this.InitShadowSystem(this.asset, this.defaultResources);
			HDRenderPipeline.s_lightVolumes = new DebugLightVolumes();
			HDRenderPipeline.s_lightVolumes.InitData(this.defaultResources);
			int num3 = Math.Max(this.m_Asset.currentPlatformRenderPipelineSettings.hdShadowInitParams.maxScreenSpaceShadowSlots, 1);
			this.m_CurrentScreenSpaceShadowData = new HDRenderPipeline.ScreenSpaceShadowData[num3];
		}

		// Token: 0x060002DE RID: 734 RVA: 0x000102B8 File Offset: 0x0000E4B8
		private void CleanupLightLoop()
		{
			HDRenderPipeline.s_lightVolumes.ReleaseData();
			this.DeinitShadowSystem();
			CoreUtils.Destroy(this.m_DefaultTexture2DArray);
			CoreUtils.Destroy(this.m_DefaultTextureCube);
			this.m_TextureCaches.Cleanup();
			this.m_LightLoopLightData.Cleanup();
			this.m_TileAndClusterData.Cleanup();
			this.LightLoopReleaseResolutionDependentBuffers();
			for (int i = 0; i < 2; i++)
			{
				for (int j = 0; j < 2; j++)
				{
					for (int k = 0; k < 2; k++)
					{
						int deferredLightingMaterialIndex = this.GetDeferredLightingMaterialIndex(i, j, k);
						CoreUtils.Destroy(this.m_deferredLightingMaterial[deferredLightingMaterialIndex]);
					}
				}
			}
			CoreUtils.Destroy(HDRenderPipeline.s_DeferredTileRegularLightingMat);
			CoreUtils.Destroy(HDRenderPipeline.s_DeferredTileSplitLightingMat);
			CoreUtils.Destroy(HDRenderPipeline.s_DeferredTileMat);
			CoreUtils.Destroy(this.m_DebugViewTilesMaterial);
			CoreUtils.Destroy(this.m_DebugHDShadowMapMaterial);
			CoreUtils.Destroy(this.m_DebugBlitMaterial);
		}

		// Token: 0x060002DF RID: 735 RVA: 0x0001038C File Offset: 0x0000E58C
		private void LightLoopNewRender()
		{
			this.m_ScreenSpaceShadowsUnion.Clear();
		}

		// Token: 0x060002E0 RID: 736 RVA: 0x0001039C File Offset: 0x0000E59C
		private void LightLoopNewFrame(HDCamera hdCamera)
		{
			FrameSettings frameSettings = hdCamera.frameSettings;
			this.m_ContactShadows = hdCamera.volumeStack.GetComponent<ContactShadows>();
			this.m_EnableContactShadow = frameSettings.IsEnabled(FrameSettingsField.ContactShadows) && this.m_ContactShadows.enable.value && this.m_ContactShadows.length.value > 0f;
			this.m_indirectLightingController = hdCamera.volumeStack.GetComponent<IndirectLightingController>();
			this.m_ContactShadowIndex = 0;
			HDRenderPipeline.ClusterPrepassSource clusterPrepassSource = (frameSettings.IsEnabled(FrameSettingsField.BigTilePrepass) ? HDRenderPipeline.ClusterPrepassSource.BigTile : HDRenderPipeline.ClusterPrepassSource.None);
			HDRenderPipeline.ClusterDepthSource clusterDepthSource;
			if (frameSettings.IsEnabled(FrameSettingsField.MSAA))
			{
				clusterDepthSource = HDRenderPipeline.ClusterDepthSource.MSAA_Depth;
			}
			else
			{
				clusterDepthSource = HDRenderPipeline.ClusterDepthSource.Depth;
			}
			string text = HDRenderPipeline.s_ClusterKernelNames[(int)clusterPrepassSource, (int)clusterDepthSource];
			string text2 = HDRenderPipeline.s_ClusterObliqueKernelNames[(int)clusterPrepassSource, (int)clusterDepthSource];
			HDRenderPipeline.s_GenListPerVoxelKernel = this.buildPerVoxelLightListShader.FindKernel(text);
			HDRenderPipeline.s_GenListPerVoxelKernelOblique = this.buildPerVoxelLightListShader.FindKernel(text2);
			if (HDRenderPipeline.GetFeatureVariantsEnabled(frameSettings))
			{
				HDRenderPipeline.s_GenListPerTileKernel = this.buildPerTileLightListShader.FindKernel(frameSettings.IsEnabled(FrameSettingsField.BigTilePrepass) ? "TileLightListGen_SrcBigTile_FeatureFlags" : "TileLightListGen_FeatureFlags");
				HDRenderPipeline.s_GenListPerTileKernel_Oblique = this.buildPerTileLightListShader.FindKernel(frameSettings.IsEnabled(FrameSettingsField.BigTilePrepass) ? "TileLightListGen_SrcBigTile_FeatureFlags_Oblique" : "TileLightListGen_FeatureFlags_Oblique");
			}
			else
			{
				HDRenderPipeline.s_GenListPerTileKernel = this.buildPerTileLightListShader.FindKernel(frameSettings.IsEnabled(FrameSettingsField.BigTilePrepass) ? "TileLightListGen_SrcBigTile" : "TileLightListGen");
				HDRenderPipeline.s_GenListPerTileKernel_Oblique = this.buildPerTileLightListShader.FindKernel(frameSettings.IsEnabled(FrameSettingsField.BigTilePrepass) ? "TileLightListGen_SrcBigTile_Oblique" : "TileLightListGen_Oblique");
			}
			this.m_TextureCaches.NewFrame();
		}

		// Token: 0x060002E1 RID: 737 RVA: 0x00010528 File Offset: 0x0000E728
		private bool LightLoopNeedResize(HDCamera hdCamera, HDRenderPipeline.TileAndClusterData tileAndClusterData)
		{
			return tileAndClusterData.lightList == null || tileAndClusterData.tileList == null || tileAndClusterData.tileFeatureFlags == null || tileAndClusterData.AABBBoundsBuffer == null || tileAndClusterData.convexBoundsBuffer == null || tileAndClusterData.lightVolumeDataBuffer == null || (tileAndClusterData.bigTileLightList == null && hdCamera.frameSettings.IsEnabled(FrameSettingsField.BigTilePrepass)) || (tileAndClusterData.dispatchIndirectBuffer == null && hdCamera.frameSettings.IsEnabled(FrameSettingsField.DeferredTile)) || tileAndClusterData.perVoxelLightLists == null || hdCamera.viewCount > this.m_MaxViewCount;
		}

		// Token: 0x060002E2 RID: 738 RVA: 0x000105B1 File Offset: 0x0000E7B1
		private void LightLoopReleaseResolutionDependentBuffers()
		{
			this.m_MaxViewCount = 1;
			this.m_TileAndClusterData.ReleaseResolutionDependentBuffers();
		}

		// Token: 0x060002E3 RID: 739 RVA: 0x000105C5 File Offset: 0x0000E7C5
		private static int NumLightIndicesPerClusteredTile()
		{
			return 2048;
		}

		// Token: 0x060002E4 RID: 740 RVA: 0x000105CC File Offset: 0x0000E7CC
		private void LightLoopAllocResolutionDependentBuffers(HDCamera hdCamera, int width, int height)
		{
			this.m_MaxViewCount = Math.Max(hdCamera.viewCount, this.m_MaxViewCount);
			this.m_TileAndClusterData.AllocateResolutionDependentBuffers(hdCamera, width, height, this.m_MaxViewCount, this.m_MaxLightsOnScreen);
		}

		// Token: 0x060002E5 RID: 741 RVA: 0x000105FF File Offset: 0x0000E7FF
		internal static Matrix4x4 WorldToCamera(Camera camera)
		{
			return HDRenderPipeline.s_FlipMatrixLHSRHS * camera.worldToCameraMatrix;
		}

		// Token: 0x060002E6 RID: 742 RVA: 0x00010611 File Offset: 0x0000E811
		private static Matrix4x4 CameraProjectionNonObliqueLHS(HDCamera camera)
		{
			return camera.nonObliqueProjMatrix * HDRenderPipeline.s_FlipMatrixLHSRHS;
		}

		// Token: 0x060002E7 RID: 743 RVA: 0x00010623 File Offset: 0x0000E823
		private Vector3 GetLightColor(VisibleLight light)
		{
			return new Vector3(light.finalColor.r, light.finalColor.g, light.finalColor.b);
		}

		// Token: 0x060002E8 RID: 744 RVA: 0x0001064E File Offset: 0x0000E84E
		private static float Saturate(float x)
		{
			return Mathf.Max(0f, Mathf.Min(x, 1f));
		}

		// Token: 0x060002E9 RID: 745 RVA: 0x00010665 File Offset: 0x0000E865
		private static float Rcp(float x)
		{
			return 1f / x;
		}

		// Token: 0x060002EA RID: 746 RVA: 0x0001066E File Offset: 0x0000E86E
		private static float Rsqrt(float x)
		{
			return HDRenderPipeline.Rcp(Mathf.Sqrt(x));
		}

		// Token: 0x060002EB RID: 747 RVA: 0x0001067C File Offset: 0x0000E87C
		private static float ComputeCosineOfHorizonAngle(float r, float R)
		{
			float num = R * HDRenderPipeline.Rcp(r);
			return -Mathf.Sqrt(HDRenderPipeline.Saturate(1f - num * num));
		}

		// Token: 0x060002EC RID: 748 RVA: 0x000106A8 File Offset: 0x0000E8A8
		private static float ChapmanUpperApprox(float z, float cosTheta)
		{
			float num = 0.761643f * (1f + 2f * z - cosTheta * cosTheta * z);
			float num2 = cosTheta * z + Mathf.Sqrt(z * (1.47721f + 0.273828f * (cosTheta * cosTheta * z)));
			return 0.5f * cosTheta + num * HDRenderPipeline.Rcp(num2);
		}

		// Token: 0x060002ED RID: 749 RVA: 0x00010700 File Offset: 0x0000E900
		private static float ChapmanHorizontal(float z)
		{
			float num = HDRenderPipeline.Rsqrt(z);
			float num2 = z * num;
			return 0.626657f * (num + 2f * num2);
		}

		// Token: 0x060002EE RID: 750 RVA: 0x00010728 File Offset: 0x0000E928
		private static Vector3 ComputeAtmosphericOpticalDepth(PhysicallyBasedSky skySettings, float r, float cosTheta, bool alwaysAboveHorizon = false)
		{
			float planetaryRadius = skySettings.GetPlanetaryRadius();
			Vector2 vector = new Vector2(skySettings.GetAirScaleHeight(), skySettings.GetAerosolScaleHeight());
			Vector2 vector2 = new Vector2(HDRenderPipeline.Rcp(vector.x), HDRenderPipeline.Rcp(vector.y));
			Vector2 vector3 = r * vector2;
			Vector2 vector4 = planetaryRadius * vector2;
			float num = HDRenderPipeline.ComputeCosineOfHorizonAngle(r, planetaryRadius);
			float num2 = Mathf.Sqrt(HDRenderPipeline.Saturate(1f - cosTheta * cosTheta));
			Vector2 vector5;
			vector5.x = HDRenderPipeline.ChapmanUpperApprox(vector3.x, Mathf.Abs(cosTheta)) * Mathf.Exp(vector4.x - vector3.x);
			vector5.y = HDRenderPipeline.ChapmanUpperApprox(vector3.y, Mathf.Abs(cosTheta)) * Mathf.Exp(vector4.y - vector3.y);
			if (!alwaysAboveHorizon && cosTheta < num)
			{
				float num3 = r / planetaryRadius * num2;
				float num4 = Mathf.Sqrt(HDRenderPipeline.Saturate(1f - num3 * num3));
				Vector2 vector6;
				vector6.x = HDRenderPipeline.ChapmanUpperApprox(vector4.x, num4);
				vector6.y = HDRenderPipeline.ChapmanUpperApprox(vector4.y, num4);
				vector5 = vector6 - vector5;
			}
			else if (cosTheta < 0f)
			{
				Vector2 vector7 = vector3 * num2;
				Vector2 vector8 = new Vector2(Mathf.Exp(vector4.x - vector7.x), Mathf.Exp(vector4.x - vector7.x));
				Vector2 vector9;
				vector9.x = 2f * HDRenderPipeline.ChapmanHorizontal(vector7.x);
				vector9.y = 2f * HDRenderPipeline.ChapmanHorizontal(vector7.y);
				vector5 = vector9 * vector8 - vector5;
			}
			Vector2 vector10 = vector5 * vector;
			Vector3 airExtinctionCoefficient = skySettings.GetAirExtinctionCoefficient();
			float aerosolExtinctionCoefficient = skySettings.GetAerosolExtinctionCoefficient();
			return new Vector3(vector10.x * airExtinctionCoefficient.x + vector10.y * aerosolExtinctionCoefficient, vector10.x * airExtinctionCoefficient.y + vector10.y * aerosolExtinctionCoefficient, vector10.x * airExtinctionCoefficient.z + vector10.y * aerosolExtinctionCoefficient);
		}

		// Token: 0x060002EF RID: 751 RVA: 0x00010948 File Offset: 0x0000EB48
		private static Vector3 EvaluateAtmosphericAttenuation(PhysicallyBasedSky skySettings, Vector3 L, Vector3 X)
		{
			Vector3 planetCenterPosition = skySettings.GetPlanetCenterPosition(X);
			float num = Vector3.Distance(X, planetCenterPosition);
			float planetaryRadius = skySettings.GetPlanetaryRadius();
			float num2 = HDRenderPipeline.ComputeCosineOfHorizonAngle(num, planetaryRadius);
			float num3 = Vector3.Dot(X - planetCenterPosition, L) * HDRenderPipeline.Rcp(num);
			if (num3 > num2)
			{
				Vector3 vector = HDRenderPipeline.ComputeAtmosphericOpticalDepth(skySettings, num, num3, true);
				Vector3 vector2;
				vector2.x = Mathf.Exp(-vector.x);
				vector2.y = Mathf.Exp(-vector.y);
				vector2.z = Mathf.Exp(-vector.z);
				return vector2;
			}
			return Vector3.zero;
		}

		// Token: 0x060002F0 RID: 752 RVA: 0x000109E0 File Offset: 0x0000EBE0
		internal unsafe void GetDirectionalLightData(CommandBuffer cmd, HDCamera hdCamera, VisibleLight light, Light lightComponent, int lightIndex, int shadowIndex, DebugDisplaySettings debugDisplaySettings, int sortedIndex, bool isPhysicallyBasedSkyActive, ref int screenSpaceShadowIndex, ref int screenSpaceShadowslot)
		{
			ProcessedLightData processedLightData = *this.m_ProcessedLightData[lightIndex];
			HDAdditionalLightData additionalLightData = processedLightData.additionalLightData;
			DirectionalLightData directionalLightData = default(DirectionalLightData);
			directionalLightData.lightLayers = additionalLightData.GetLightLayers();
			directionalLightData.forward = light.GetForward();
			directionalLightData.right = light.GetRight() * 2f / Mathf.Max(additionalLightData.shapeWidth, 0.001f);
			directionalLightData.up = light.GetUp() * 2f / Mathf.Max(additionalLightData.shapeHeight, 0.001f);
			directionalLightData.positionRWS = light.GetPosition();
			directionalLightData.color = this.GetLightColor(light);
			directionalLightData.color *= ((HDUtils.s_DefaultHDAdditionalLightData == additionalLightData) ? 3.1415927f : 1f);
			directionalLightData.lightDimmer = additionalLightData.lightDimmer;
			directionalLightData.diffuseDimmer = (additionalLightData.affectDiffuse ? additionalLightData.lightDimmer : 0f);
			directionalLightData.specularDimmer = (additionalLightData.affectSpecular ? (additionalLightData.lightDimmer * hdCamera.frameSettings.specularGlobalDimmer) : 0f);
			directionalLightData.volumetricLightDimmer = additionalLightData.volumetricDimmer;
			directionalLightData.shadowIndex = -1;
			directionalLightData.screenSpaceShadowIndex = (int)LightDefinitions.s_InvalidScreenSpaceShadow;
			directionalLightData.isRayTracedContactShadow = 0f;
			if (lightComponent != null && lightComponent.cookie != null)
			{
				directionalLightData.cookieMode = ((lightComponent.cookie.wrapMode == TextureWrapMode.Repeat) ? CookieMode.Repeat : CookieMode.Clamp);
				directionalLightData.cookieScaleOffset = this.m_TextureCaches.lightCookieManager.Fetch2DCookie(cmd, lightComponent.cookie);
			}
			else
			{
				directionalLightData.cookieMode = CookieMode.None;
			}
			if (additionalLightData.surfaceTexture == null)
			{
				directionalLightData.surfaceTextureScaleOffset = Vector4.zero;
			}
			else
			{
				directionalLightData.surfaceTextureScaleOffset = this.m_TextureCaches.lightCookieManager.Fetch2DCookie(cmd, additionalLightData.surfaceTexture);
			}
			directionalLightData.shadowDimmer = additionalLightData.shadowDimmer;
			directionalLightData.volumetricShadowDimmer = additionalLightData.volumetricShadowDimmer;
			this.GetContactShadowMask(additionalLightData, HDAdditionalLightData.ScalableSettings.UseContactShadow(this.m_Asset), hdCamera, ref directionalLightData.contactShadowMask, ref directionalLightData.isRayTracedContactShadow);
			bool flag = additionalLightData.penumbraTint && (additionalLightData.shadowTint.r != additionalLightData.shadowTint.g || additionalLightData.shadowTint.g != additionalLightData.shadowTint.b);
			directionalLightData.penumbraTint = (flag ? 1f : 0f);
			if (flag)
			{
				directionalLightData.shadowTint = new Vector3(additionalLightData.shadowTint.r * additionalLightData.shadowTint.r, additionalLightData.shadowTint.g * additionalLightData.shadowTint.g, additionalLightData.shadowTint.b * additionalLightData.shadowTint.b);
			}
			else
			{
				directionalLightData.shadowTint = new Vector3(additionalLightData.shadowTint.r, additionalLightData.shadowTint.g, additionalLightData.shadowTint.b);
			}
			directionalLightData.shadowIndex = shadowIndex;
			if (shadowIndex != -1)
			{
				if (additionalLightData.WillRenderScreenSpaceShadow())
				{
					directionalLightData.screenSpaceShadowIndex = screenSpaceShadowslot;
					if (additionalLightData.colorShadow && additionalLightData.WillRenderRayTracedShadow())
					{
						screenSpaceShadowslot += 3;
						directionalLightData.screenSpaceShadowIndex |= (int)LightDefinitions.s_ScreenSpaceColorShadowFlag;
					}
					else
					{
						screenSpaceShadowslot++;
					}
					screenSpaceShadowIndex++;
					this.m_ScreenSpaceShadowsUnion.Add(additionalLightData);
				}
				this.m_CurrentSunLight = lightComponent;
				this.m_CurrentSunLightAdditionalLightData = additionalLightData;
				this.m_CurrentSunLightDirectionalLightData = directionalLightData;
				this.m_CurrentShadowSortedSunLightIndex = sortedIndex;
			}
			float num = Mathf.Clamp01(1.35f / (1f + Mathf.Pow(1.15f * (0.0315f * additionalLightData.angularDiameter + 0.4f), 2f)) - 0.11f);
			directionalLightData.minRoughness = (1f - num) * (1f - num);
			directionalLightData.shadowMaskSelector = Vector4.zero;
			if (processedLightData.isBakedShadowMask)
			{
				directionalLightData.shadowMaskSelector[lightComponent.bakingOutput.occlusionMaskChannel] = 1f;
				directionalLightData.nonLightMappedOnly = ((lightComponent.lightShadowCasterMode == LightShadowCasterMode.NonLightmappedOnly) ? 1 : 0);
			}
			else
			{
				directionalLightData.shadowMaskSelector.x = -1f;
				directionalLightData.nonLightMappedOnly = 0;
			}
			bool flag2 = isPhysicallyBasedSkyActive && additionalLightData.interactsWithSky;
			directionalLightData.distanceFromCamera = -1f;
			if (flag2)
			{
				directionalLightData.distanceFromCamera = additionalLightData.distance;
				if (ShaderConfig.s_PrecomputedAtmosphericAttenuation != 0)
				{
					Vector3 vector = HDRenderPipeline.EvaluateAtmosphericAttenuation(hdCamera.volumeStack.GetComponent<PhysicallyBasedSky>(), -directionalLightData.forward, hdCamera.camera.transform.position);
					directionalLightData.color.x = directionalLightData.color.x * vector.x;
					directionalLightData.color.y = directionalLightData.color.y * vector.y;
					directionalLightData.color.z = directionalLightData.color.z * vector.z;
				}
			}
			directionalLightData.angularDiameter = additionalLightData.angularDiameter * 0.017453292f;
			directionalLightData.flareSize = Mathf.Max(additionalLightData.flareSize * 0.017453292f, 5.9604645E-08f);
			directionalLightData.flareFalloff = additionalLightData.flareFalloff;
			directionalLightData.flareTint = additionalLightData.flareTint;
			directionalLightData.surfaceTint = additionalLightData.surfaceTint;
			this.m_CurrentSunLight = ((this.m_CurrentSunLight == null) ? lightComponent : this.m_CurrentSunLight);
			this.m_lightList.directionalLights.Add(directionalLightData);
		}

		// Token: 0x060002F1 RID: 753 RVA: 0x00010F6B File Offset: 0x0000F16B
		private bool EnoughScreenSpaceShadowSlots(GPULightType gpuLightType, int screenSpaceChannelSlot)
		{
			if (gpuLightType == GPULightType.Rectangle)
			{
				return screenSpaceChannelSlot + 1 < this.m_Asset.currentPlatformRenderPipelineSettings.hdShadowInitParams.maxScreenSpaceShadowSlots;
			}
			return screenSpaceChannelSlot < this.m_Asset.currentPlatformRenderPipelineSettings.hdShadowInitParams.maxScreenSpaceShadowSlots;
		}

		// Token: 0x060002F2 RID: 754 RVA: 0x00010FA4 File Offset: 0x0000F1A4
		internal unsafe void GetLightData(CommandBuffer cmd, HDCamera hdCamera, HDShadowSettings shadowSettings, VisibleLight light, Light lightComponent, int lightIndex, int shadowIndex, ref Vector3 lightDimensions, DebugDisplaySettings debugDisplaySettings, ref int screenSpaceShadowIndex, ref int screenSpaceChannelSlot)
		{
			ProcessedLightData processedLightData = *this.m_ProcessedLightData[lightIndex];
			HDAdditionalLightData additionalLightData = processedLightData.additionalLightData;
			GPULightType gpuLightType = processedLightData.gpuLightType;
			HDLightType lightType = processedLightData.lightType;
			LightData lightData = default(LightData);
			lightData.lightLayers = additionalLightData.GetLightLayers();
			lightData.lightType = gpuLightType;
			lightData.positionRWS = light.GetPosition();
			bool flag = additionalLightData.applyRangeAttenuation && gpuLightType != GPULightType.ProjectorBox;
			lightData.range = light.range;
			if (flag)
			{
				lightData.rangeAttenuationScale = 1f / (light.range * light.range);
				lightData.rangeAttenuationBias = 1f;
				if (lightData.lightType == GPULightType.Rectangle)
				{
					lightData.rangeAttenuationScale = 1f;
				}
			}
			else
			{
				lightData.rangeAttenuationScale = 4096f / (light.range * light.range);
				lightData.rangeAttenuationBias = 16777216f;
				if (lightData.lightType == GPULightType.Rectangle)
				{
					lightData.rangeAttenuationScale = 4096f;
				}
			}
			lightData.color = this.GetLightColor(light);
			lightData.forward = light.GetForward();
			lightData.up = light.GetUp();
			lightData.right = light.GetRight();
			lightDimensions.x = additionalLightData.shapeWidth;
			lightDimensions.y = additionalLightData.shapeHeight;
			lightDimensions.z = light.range;
			lightData.boxLightSafeExtent = 1f;
			if (lightData.lightType == GPULightType.ProjectorBox)
			{
				lightData.right *= 2f / Mathf.Max(additionalLightData.shapeWidth, 0.001f);
				lightData.up *= 2f / Mathf.Max(additionalLightData.shapeHeight, 0.001f);
				if (shadowIndex >= 0)
				{
					float num = (float)additionalLightData.shadowResolution.Value(this.m_ShadowInitParameters.shadowResolutionPunctual);
					num = Mathf.Clamp(num, 128f, 2048f);
					float num2 = Mathf.Lerp(0.05f, 0.01f, Mathf.Max(num / 2048f, 0f));
					lightData.boxLightSafeExtent = 1f - num2;
				}
			}
			else if (lightData.lightType == GPULightType.ProjectorPyramid)
			{
				float spotAngle = light.spotAngle;
				float num3;
				float num4;
				if (additionalLightData.aspectRatio >= 1f)
				{
					num3 = 2f * Mathf.Tan(spotAngle * 0.5f * 0.017453292f);
					num4 = num3 * additionalLightData.aspectRatio;
				}
				else
				{
					num4 = 2f * Mathf.Tan(spotAngle * 0.5f * 0.017453292f);
					num3 = num4 / additionalLightData.aspectRatio;
				}
				lightDimensions.x = num4;
				lightDimensions.y = num3;
				lightData.right *= 2f / num4;
				lightData.up *= 2f / num3;
			}
			if (lightData.lightType == GPULightType.Spot)
			{
				float spotAngle2 = light.spotAngle;
				float innerSpotPercent = additionalLightData.innerSpotPercent01;
				float num5 = Mathf.Clamp(Mathf.Cos(spotAngle2 * 0.5f * 0.017453292f), 0f, 1f);
				float num6 = Mathf.Sqrt(1f - num5 * num5);
				float num7 = Mathf.Clamp(Mathf.Cos(spotAngle2 * 0.5f * innerSpotPercent * 0.017453292f), 0f, 1f);
				float num8 = Mathf.Max(0.0001f, num7 - num5);
				lightData.angleScale = 1f / num8;
				lightData.angleOffset = -num5 * lightData.angleScale;
				float num9 = num5 / num6;
				lightData.up *= num9;
				lightData.right *= num9;
			}
			else
			{
				lightData.angleScale = 0f;
				lightData.angleOffset = 1f;
			}
			if (lightData.lightType != GPULightType.Directional && lightData.lightType != GPULightType.ProjectorBox)
			{
				lightData.size = new Vector4(additionalLightData.shapeRadius * additionalLightData.shapeRadius, 0f, 0f, 0f);
			}
			if (lightData.lightType == GPULightType.Rectangle || lightData.lightType == GPULightType.Tube)
			{
				lightData.size = new Vector4(additionalLightData.shapeWidth, additionalLightData.shapeHeight, Mathf.Cos(additionalLightData.barnDoorAngle * 3.1415927f / 180f), additionalLightData.barnDoorLength);
			}
			lightData.lightDimmer = processedLightData.lightDistanceFade * additionalLightData.lightDimmer;
			lightData.diffuseDimmer = processedLightData.lightDistanceFade * (additionalLightData.affectDiffuse ? additionalLightData.lightDimmer : 0f);
			lightData.specularDimmer = processedLightData.lightDistanceFade * (additionalLightData.affectSpecular ? (additionalLightData.lightDimmer * hdCamera.frameSettings.specularGlobalDimmer) : 0f);
			lightData.volumetricLightDimmer = processedLightData.lightDistanceFade * additionalLightData.volumetricDimmer;
			lightData.cookieMode = CookieMode.None;
			lightData.cookieIndex = -1;
			lightData.shadowIndex = -1;
			lightData.screenSpaceShadowIndex = (int)LightDefinitions.s_InvalidScreenSpaceShadow;
			lightData.isRayTracedContactShadow = 0f;
			if (lightComponent != null && lightComponent.cookie != null)
			{
				if (lightType != HDLightType.Spot)
				{
					if (lightType == HDLightType.Point)
					{
						lightData.cookieMode = CookieMode.Clamp;
						lightData.cookieIndex = this.m_TextureCaches.lightCookieManager.FetchCubeCookie(cmd, lightComponent.cookie);
					}
				}
				else
				{
					lightData.cookieMode = ((lightComponent.cookie.wrapMode == TextureWrapMode.Repeat) ? CookieMode.Repeat : CookieMode.Clamp);
					lightData.cookieScaleOffset = this.m_TextureCaches.lightCookieManager.Fetch2DCookie(cmd, lightComponent.cookie);
				}
			}
			else if (lightType == HDLightType.Spot && additionalLightData.spotLightShape != SpotLightShape.Cone)
			{
				lightData.cookieMode = CookieMode.Clamp;
				lightData.cookieScaleOffset = this.m_TextureCaches.lightCookieManager.Fetch2DCookie(cmd, Texture2D.whiteTexture);
			}
			else if (lightData.lightType == GPULightType.Rectangle && additionalLightData.areaLightCookie != null)
			{
				lightData.cookieMode = CookieMode.Clamp;
				lightData.cookieScaleOffset = this.m_TextureCaches.lightCookieManager.FetchAreaCookie(cmd, additionalLightData.areaLightCookie);
			}
			float num10 = HDUtils.ComputeLinearDistanceFade(processedLightData.distanceToCamera, Mathf.Min(shadowSettings.maxShadowDistance.value, additionalLightData.shadowFadeDistance));
			lightData.shadowDimmer = num10 * additionalLightData.shadowDimmer;
			lightData.volumetricShadowDimmer = num10 * additionalLightData.volumetricShadowDimmer;
			this.GetContactShadowMask(additionalLightData, HDAdditionalLightData.ScalableSettings.UseContactShadow(this.m_Asset), hdCamera, ref lightData.contactShadowMask, ref lightData.isRayTracedContactShadow);
			bool flag2 = additionalLightData.penumbraTint && (additionalLightData.shadowTint.r != additionalLightData.shadowTint.g || additionalLightData.shadowTint.g != additionalLightData.shadowTint.b);
			lightData.penumbraTint = (flag2 ? 1f : 0f);
			if (flag2)
			{
				lightData.shadowTint = new Vector3(Mathf.Pow(additionalLightData.shadowTint.r, 2.2f), Mathf.Pow(additionalLightData.shadowTint.g, 2.2f), Mathf.Pow(additionalLightData.shadowTint.b, 2.2f));
			}
			else
			{
				lightData.shadowTint = new Vector3(additionalLightData.shadowTint.r, additionalLightData.shadowTint.g, additionalLightData.shadowTint.b);
			}
			if (hdCamera.frameSettings.IsEnabled(FrameSettingsField.RayTracing) && this.EnoughScreenSpaceShadowSlots(lightData.lightType, screenSpaceChannelSlot) && additionalLightData.WillRenderScreenSpaceShadow())
			{
				if (lightData.lightType == GPULightType.Rectangle && screenSpaceChannelSlot % 4 == 3)
				{
					screenSpaceChannelSlot++;
				}
				lightData.screenSpaceShadowIndex = screenSpaceChannelSlot;
				this.m_CurrentScreenSpaceShadowData[screenSpaceShadowIndex].additionalLightData = additionalLightData;
				this.m_CurrentScreenSpaceShadowData[screenSpaceShadowIndex].lightDataIndex = this.m_lightList.lights.Count;
				this.m_CurrentScreenSpaceShadowData[screenSpaceShadowIndex].valid = true;
				this.m_ScreenSpaceShadowsUnion.Add(additionalLightData);
				screenSpaceShadowIndex++;
				if (lightData.lightType == GPULightType.Rectangle)
				{
					screenSpaceChannelSlot += 2;
				}
				else
				{
					screenSpaceChannelSlot++;
				}
			}
			lightData.shadowIndex = shadowIndex;
			additionalLightData.shadowIndex = shadowIndex;
			float num11 = Mathf.Clamp01(1.1725f / (1.01f + Mathf.Pow(1f * (additionalLightData.shapeRadius + 0.1f), 2f)) - 0.15f);
			lightData.minRoughness = (1f - num11) * (1f - num11);
			lightData.shadowMaskSelector = Vector4.zero;
			if (processedLightData.isBakedShadowMask)
			{
				lightData.shadowMaskSelector[lightComponent.bakingOutput.occlusionMaskChannel] = 1f;
				lightData.nonLightMappedOnly = ((lightComponent.lightShadowCasterMode == LightShadowCasterMode.NonLightmappedOnly) ? 1 : 0);
			}
			else
			{
				lightData.shadowMaskSelector.x = -1f;
				lightData.nonLightMappedOnly = 0;
			}
			this.m_lightList.lights.Add(lightData);
		}

		// Token: 0x060002F3 RID: 755 RVA: 0x000118B4 File Offset: 0x0000FAB4
		private void GetLightVolumeDataAndBound(LightCategory lightCategory, GPULightType gpuLightType, LightVolumeType lightVolumeType, VisibleLight light, LightData lightData, Vector3 lightDimensions, Matrix4x4 worldToView, int viewIndex)
		{
			float z = lightDimensions.z;
			Matrix4x4 localToWorldMatrix = light.localToWorldMatrix;
			Vector3 positionRWS = lightData.positionRWS;
			Vector3 vector = worldToView.MultiplyPoint(positionRWS);
			Matrix4x4 matrix4x = worldToView * localToWorldMatrix;
			Vector3 vector2 = matrix4x.GetColumn(0);
			Vector3 vector3 = matrix4x.GetColumn(1);
			Vector3 vector4 = matrix4x.GetColumn(2);
			SFiniteLightBound sfiniteLightBound = default(SFiniteLightBound);
			LightVolumeData lightVolumeData = default(LightVolumeData);
			lightVolumeData.lightCategory = (uint)lightCategory;
			lightVolumeData.lightVolume = (uint)lightVolumeType;
			if (gpuLightType == GPULightType.Spot || gpuLightType == GPULightType.ProjectorPyramid)
			{
				Vector3 vector5 = localToWorldMatrix.GetColumn(2);
				Vector3 vector6 = vector2;
				Vector3 vector7 = vector3;
				Vector3 vector8 = vector4;
				float spotAngle = light.spotAngle;
				float num = Mathf.Cos(0.5f * spotAngle * 0.017453292f);
				float num2 = Mathf.Sin(0.5f * spotAngle * 0.017453292f);
				if (gpuLightType == GPULightType.ProjectorPyramid)
				{
					Vector3 vector9 = 0.5f * lightDimensions.x * vector6 + 0.5f * lightDimensions.y * vector7 + 1f * vector8;
					num = Vector3.Dot(vector8, Vector3.Normalize(vector9));
					num2 = Mathf.Sqrt(1f - num * num);
				}
				float num3 = ((num > 0f) ? (num2 / num) : float.MaxValue);
				float num4 = ((num2 > 0f) ? (num / num2) : float.MaxValue);
				bool flag = true;
				float num5 = (flag ? num3 : num2);
				sfiniteLightBound.center = worldToView.MultiplyPoint(positionRWS + 0.5f * z * vector5);
				sfiniteLightBound.boxAxisX = num5 * z * vector6;
				sfiniteLightBound.boxAxisY = num5 * z * vector7;
				sfiniteLightBound.boxAxisZ = 0.5f * z * vector8;
				float num6 = num2;
				float num7 = num - 0.5f;
				num6 *= z;
				float num8 = num7 * z;
				float num9 = Mathf.Sqrt(num8 * num8 + 1f * num6 * num6);
				sfiniteLightBound.radius = ((num9 > 0.5f * z) ? num9 : (0.5f * z));
				sfiniteLightBound.scaleXY = (flag ? new Vector2(0.01f, 0.01f) : new Vector2(1f, 1f));
				lightVolumeData.lightAxisX = vector6;
				lightVolumeData.lightAxisY = vector7;
				lightVolumeData.lightAxisZ = vector8;
				lightVolumeData.lightPos = vector;
				lightVolumeData.radiusSq = z * z;
				lightVolumeData.cotan = num4;
				lightVolumeData.featureFlags = 4096U;
			}
			else if (gpuLightType == GPULightType.Point)
			{
				Vector3 vector10 = vector2;
				Vector3 vector11 = vector3;
				Vector3 vector12 = vector4;
				sfiniteLightBound.center = vector;
				sfiniteLightBound.boxAxisX = vector10 * z;
				sfiniteLightBound.boxAxisY = vector11 * z;
				sfiniteLightBound.boxAxisZ = vector12 * z;
				sfiniteLightBound.scaleXY.Set(1f, 1f);
				sfiniteLightBound.radius = z;
				lightVolumeData.lightAxisX = vector10;
				lightVolumeData.lightAxisY = vector11;
				lightVolumeData.lightAxisZ = vector12;
				lightVolumeData.lightPos = sfiniteLightBound.center;
				lightVolumeData.radiusSq = z * z;
				lightVolumeData.featureFlags = 4096U;
			}
			else if (gpuLightType == GPULightType.Tube)
			{
				Vector3 vector13 = new Vector3(lightDimensions.x + 2f * z, 2f * z, 2f * z);
				Vector3 vector14 = 0.5f * vector13;
				sfiniteLightBound.center = vector;
				sfiniteLightBound.boxAxisX = vector14.x * vector2;
				sfiniteLightBound.boxAxisY = vector14.y * vector3;
				sfiniteLightBound.boxAxisZ = vector14.z * vector4;
				sfiniteLightBound.scaleXY.Set(1f, 1f);
				sfiniteLightBound.radius = vector14.magnitude;
				lightVolumeData.lightPos = vector;
				lightVolumeData.lightAxisX = vector2;
				lightVolumeData.lightAxisY = vector3;
				lightVolumeData.lightAxisZ = vector4;
				lightVolumeData.boxInnerDist = new Vector3(lightDimensions.x, 0f, 0f);
				lightVolumeData.boxInvRange.Set(1f / z, 1f / z, 1f / z);
				lightVolumeData.featureFlags = 8192U;
			}
			else if (gpuLightType == GPULightType.Rectangle)
			{
				Vector3 vector15 = new Vector3(lightDimensions.x + 2f * z, lightDimensions.y + 2f * z, z);
				Vector3 vector16 = 0.5f * vector15;
				Vector3 vector17 = vector + vector16.z * vector4;
				sfiniteLightBound.center = vector17;
				sfiniteLightBound.boxAxisX = vector16.x * vector2;
				sfiniteLightBound.boxAxisY = vector16.y * vector3;
				sfiniteLightBound.boxAxisZ = vector16.z * vector4;
				sfiniteLightBound.scaleXY.Set(1f, 1f);
				sfiniteLightBound.radius = vector16.magnitude;
				lightVolumeData.lightPos = vector17;
				lightVolumeData.lightAxisX = vector2;
				lightVolumeData.lightAxisY = vector3;
				lightVolumeData.lightAxisZ = vector4;
				lightVolumeData.boxInnerDist = vector16;
				lightVolumeData.boxInvRange.Set(float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity);
				lightVolumeData.featureFlags = 8192U;
			}
			else if (gpuLightType == GPULightType.ProjectorBox)
			{
				Vector3 vector18 = new Vector3(lightDimensions.x, lightDimensions.y, z);
				Vector3 vector19 = 0.5f * vector18;
				Vector3 vector20 = vector + vector19.z * vector4;
				sfiniteLightBound.center = vector20;
				sfiniteLightBound.boxAxisX = vector19.x * vector2;
				sfiniteLightBound.boxAxisY = vector19.y * vector3;
				sfiniteLightBound.boxAxisZ = vector19.z * vector4;
				sfiniteLightBound.radius = vector19.magnitude;
				sfiniteLightBound.scaleXY.Set(1f, 1f);
				lightVolumeData.lightPos = vector20;
				lightVolumeData.lightAxisX = vector2;
				lightVolumeData.lightAxisY = vector3;
				lightVolumeData.lightAxisZ = vector4;
				lightVolumeData.boxInnerDist = vector19;
				lightVolumeData.boxInvRange.Set(float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity);
				lightVolumeData.featureFlags = 4096U;
			}
			this.m_lightList.lightsPerView[viewIndex].bounds.Add(sfiniteLightBound);
			this.m_lightList.lightsPerView[viewIndex].lightVolumes.Add(lightVolumeData);
		}

		// Token: 0x060002F4 RID: 756 RVA: 0x00011F64 File Offset: 0x00010164
		internal bool GetEnvLightData(CommandBuffer cmd, HDCamera hdCamera, in ProcessedProbeData processedProbe, DebugDisplaySettings debugDisplaySettings, ref EnvLightData envLightData)
		{
			Camera camera = hdCamera.camera;
			HDProbe hdProbe = processedProbe.hdProbe;
			if (!hdProbe.HasValidRenderedData())
			{
				return false;
			}
			Vector3 vector = Vector3.zero;
			Matrix4x4 influenceToWorld = hdProbe.influenceToWorld;
			Vector4 zero = Vector4.zero;
			int num = int.MinValue;
			HDProbe hdprobe = hdProbe;
			if (hdprobe != null)
			{
				PlanarReflectionProbe planarReflectionProbe;
				if ((planarReflectionProbe = hdprobe as PlanarReflectionProbe) == null)
				{
					if (hdprobe is HDAdditionalReflectionData)
					{
						num = this.m_TextureCaches.reflectionProbeCache.FetchSlice(cmd, hdProbe.texture);
						num = ((num == -1) ? int.MinValue : (num + 1));
						ProbeCapturePositionSettings probeCapturePositionSettings = ProbeCapturePositionSettings.ComputeFrom(hdProbe, camera.transform);
						CameraSettings cameraSettings;
						CameraPositionSettings cameraPositionSettings;
						HDRenderUtilities.ComputeCameraSettingsFromProbeSettings(hdProbe.settings, probeCapturePositionSettings, out cameraSettings, out cameraPositionSettings, 0UL, 90f, 1f);
						vector = cameraPositionSettings.position;
					}
				}
				else
				{
					PlanarReflectionProbe planarReflectionProbe2 = planarReflectionProbe;
					if (hdProbe.mode != ProbeSettings.Mode.Realtime || hdCamera.frameSettings.IsEnabled(FrameSettingsField.PlanarProbe))
					{
						int num2;
						Vector4 vector2 = this.m_TextureCaches.reflectionPlanarProbeCache.FetchSlice(cmd, hdProbe.texture, out num2);
						num = ((vector2 == Vector4.zero) ? int.MinValue : (-(num2 + 1)));
						if (num2 >= this.m_MaxPlanarReflectionOnScreen)
						{
							Debug.LogWarning("Maximum planar reflection probe on screen reached. To fix this error, increase the maximum number of planar reflections on screen in the HDRP asset.");
						}
						else
						{
							HDProbe.RenderData renderData = planarReflectionProbe2.renderData;
							Matrix4x4 worldToCameraRHS = renderData.worldToCameraRHS;
							Matrix4x4 projectionMatrix = renderData.projectionMatrix;
							vector = Vector3.zero;
							Matrix4x4 gpuprojectionMatrix = GL.GetGPUProjectionMatrix(projectionMatrix, true);
							Matrix4x4 matrix4x = worldToCameraRHS;
							Matrix4x4 matrix4x2 = gpuprojectionMatrix * matrix4x;
							this.m_TextureCaches.env2DAtlasScaleOffset[num2] = vector2;
							this.m_TextureCaches.env2DCaptureVP[num2] = matrix4x2;
							Vector3 vector3 = renderData.captureRotation * Vector3.forward;
							this.m_TextureCaches.env2DCaptureForward[num2 * 3] = vector3.x;
							this.m_TextureCaches.env2DCaptureForward[num2 * 3 + 1] = vector3.y;
							this.m_TextureCaches.env2DCaptureForward[num2 * 3 + 2] = vector3.z;
						}
					}
				}
			}
			if (num == -2147483648)
			{
				return false;
			}
			InfluenceVolume influenceVolume = hdProbe.influenceVolume;
			envLightData.lightLayers = hdProbe.lightLayersAsUInt;
			envLightData.influenceShapeType = influenceVolume.envShape;
			envLightData.weight = processedProbe.weight;
			envLightData.multiplier = hdProbe.multiplier * this.m_indirectLightingController.indirectSpecularIntensity.value;
			envLightData.rangeCompressionFactorCompensation = Mathf.Max(hdProbe.rangeCompressionFactor, 1E-06f);
			envLightData.influenceExtents = influenceVolume.extents;
			EnvShapeType envShape = influenceVolume.envShape;
			if (envShape != EnvShapeType.Box)
			{
				if (envShape != EnvShapeType.Sphere)
				{
					throw new ArgumentOutOfRangeException("Unknown EnvShapeType");
				}
				envLightData.blendNormalDistancePositive.x = influenceVolume.sphereBlendNormalDistance;
				envLightData.blendDistancePositive.x = influenceVolume.sphereBlendDistance;
			}
			else
			{
				envLightData.blendNormalDistancePositive = influenceVolume.boxBlendNormalDistancePositive;
				envLightData.blendNormalDistanceNegative = influenceVolume.boxBlendNormalDistanceNegative;
				envLightData.blendDistancePositive = influenceVolume.boxBlendDistancePositive;
				envLightData.blendDistanceNegative = influenceVolume.boxBlendDistanceNegative;
				envLightData.boxSideFadePositive = influenceVolume.boxSideFadePositive;
				envLightData.boxSideFadeNegative = influenceVolume.boxSideFadeNegative;
			}
			envLightData.influenceRight = influenceToWorld.GetColumn(0).normalized;
			envLightData.influenceUp = influenceToWorld.GetColumn(1).normalized;
			envLightData.influenceForward = influenceToWorld.GetColumn(2).normalized;
			envLightData.capturePositionRWS = vector;
			envLightData.influencePositionRWS = influenceToWorld.GetColumn(3);
			envLightData.envIndex = num;
			Matrix4x4 proxyToWorld = hdProbe.proxyToWorld;
			envLightData.proxyExtents = hdProbe.proxyExtents;
			envLightData.minProjectionDistance = (hdProbe.isProjectionInfinite ? 65504f : 0f);
			envLightData.proxyRight = proxyToWorld.GetColumn(0).normalized;
			envLightData.proxyUp = proxyToWorld.GetColumn(1).normalized;
			envLightData.proxyForward = proxyToWorld.GetColumn(2).normalized;
			envLightData.proxyPositionRWS = proxyToWorld.GetColumn(3);
			return true;
		}

		// Token: 0x060002F5 RID: 757 RVA: 0x00012394 File Offset: 0x00010594
		private void GetEnvLightVolumeDataAndBound(HDProbe probe, LightVolumeType lightVolumeType, Matrix4x4 worldToView, int viewIndex)
		{
			SFiniteLightBound sfiniteLightBound = default(SFiniteLightBound);
			LightVolumeData lightVolumeData = default(LightVolumeData);
			Vector3 influenceExtents = probe.influenceExtents;
			Matrix4x4 influenceToWorld = probe.influenceToWorld;
			Vector3 vector = worldToView.MultiplyVector(influenceToWorld.GetColumn(0).normalized);
			Vector3 vector2 = worldToView.MultiplyVector(influenceToWorld.GetColumn(1).normalized);
			Vector3 vector3 = worldToView.MultiplyVector(influenceToWorld.GetColumn(2).normalized);
			Vector3 vector4 = worldToView.MultiplyPoint(influenceToWorld.GetColumn(3));
			lightVolumeData.lightCategory = 2U;
			lightVolumeData.lightVolume = (uint)lightVolumeType;
			lightVolumeData.featureFlags = 32768U;
			if (lightVolumeType != LightVolumeType.Sphere)
			{
				if (lightVolumeType == LightVolumeType.Box)
				{
					sfiniteLightBound.center = vector4;
					sfiniteLightBound.boxAxisX = influenceExtents.x * vector;
					sfiniteLightBound.boxAxisY = influenceExtents.y * vector2;
					sfiniteLightBound.boxAxisZ = influenceExtents.z * vector3;
					sfiniteLightBound.scaleXY.Set(1f, 1f);
					sfiniteLightBound.radius = influenceExtents.magnitude;
					lightVolumeData.lightPos = vector4;
					lightVolumeData.lightAxisX = vector;
					lightVolumeData.lightAxisY = vector2;
					lightVolumeData.lightAxisZ = vector3;
					lightVolumeData.boxInnerDist = influenceExtents - HDRenderPipeline.k_BoxCullingExtentThreshold;
					lightVolumeData.boxInvRange.Set(1f / HDRenderPipeline.k_BoxCullingExtentThreshold.x, 1f / HDRenderPipeline.k_BoxCullingExtentThreshold.y, 1f / HDRenderPipeline.k_BoxCullingExtentThreshold.z);
				}
			}
			else
			{
				lightVolumeData.lightPos = vector4;
				lightVolumeData.radiusSq = influenceExtents.x * influenceExtents.x;
				lightVolumeData.lightAxisX = vector;
				lightVolumeData.lightAxisY = vector2;
				lightVolumeData.lightAxisZ = vector3;
				sfiniteLightBound.center = vector4;
				sfiniteLightBound.boxAxisX = vector * influenceExtents.x;
				sfiniteLightBound.boxAxisY = vector2 * influenceExtents.x;
				sfiniteLightBound.boxAxisZ = vector3 * influenceExtents.x;
				sfiniteLightBound.scaleXY.Set(1f, 1f);
				sfiniteLightBound.radius = influenceExtents.x;
			}
			this.m_lightList.lightsPerView[viewIndex].bounds.Add(sfiniteLightBound);
			this.m_lightList.lightsPerView[viewIndex].lightVolumes.Add(lightVolumeData);
		}

		// Token: 0x060002F6 RID: 758 RVA: 0x00012618 File Offset: 0x00010818
		private void AddBoxVolumeDataAndBound(OrientedBBox obb, LightCategory category, LightFeatureFlags featureFlags, Matrix4x4 worldToView, int viewIndex)
		{
			SFiniteLightBound sfiniteLightBound = default(SFiniteLightBound);
			LightVolumeData lightVolumeData = default(LightVolumeData);
			Vector3 vector = worldToView.MultiplyPoint(obb.center);
			Vector3 vector2 = worldToView.MultiplyVector(obb.right);
			Vector3 vector3 = worldToView.MultiplyVector(obb.up);
			Vector3 vector4 = Vector3.Cross(vector3, vector2);
			Vector3 vector5 = new Vector3(obb.extentX, obb.extentY, obb.extentZ);
			lightVolumeData.lightVolume = 2U;
			lightVolumeData.lightCategory = (uint)category;
			lightVolumeData.featureFlags = (uint)featureFlags;
			sfiniteLightBound.center = vector;
			sfiniteLightBound.boxAxisX = obb.extentX * vector2;
			sfiniteLightBound.boxAxisY = obb.extentY * vector3;
			sfiniteLightBound.boxAxisZ = obb.extentZ * vector4;
			sfiniteLightBound.radius = vector5.magnitude;
			sfiniteLightBound.scaleXY.Set(1f, 1f);
			lightVolumeData.lightPos = vector;
			lightVolumeData.lightAxisX = vector2;
			lightVolumeData.lightAxisY = vector3;
			lightVolumeData.lightAxisZ = vector4;
			lightVolumeData.boxInnerDist = vector5 - HDRenderPipeline.k_BoxCullingExtentThreshold;
			lightVolumeData.boxInvRange.Set(1f / HDRenderPipeline.k_BoxCullingExtentThreshold.x, 1f / HDRenderPipeline.k_BoxCullingExtentThreshold.y, 1f / HDRenderPipeline.k_BoxCullingExtentThreshold.z);
			this.m_lightList.lightsPerView[viewIndex].bounds.Add(sfiniteLightBound);
			this.m_lightList.lightsPerView[viewIndex].lightVolumes.Add(lightVolumeData);
		}

		// Token: 0x060002F7 RID: 759 RVA: 0x000127AD File Offset: 0x000109AD
		internal int GetCurrentShadowCount()
		{
			return this.m_ShadowManager.GetShadowRequestCount();
		}

		// Token: 0x060002F8 RID: 760 RVA: 0x000127BC File Offset: 0x000109BC
		private void LightLoopUpdateCullingParameters(ref ScriptableCullingParameters cullingParams, HDCamera hdCamera)
		{
			float value = hdCamera.volumeStack.GetComponent<HDShadowSettings>().maxShadowDistance.value;
			this.m_ShadowManager.UpdateCullingParameters(ref cullingParams, value);
			cullingParams.cullingOptions |= CullingOptions.DisablePerObjectCulling;
		}

		// Token: 0x060002F9 RID: 761 RVA: 0x000127FB File Offset: 0x000109FB
		private bool IsBakedShadowMaskLight(Light light)
		{
			return !(light == null) && (light.bakingOutput.lightmapBakeType == LightmapBakeType.Mixed && light.bakingOutput.mixedLightingMode == MixedLightingMode.Shadowmask) && light.bakingOutput.occlusionMaskChannel != -1;
		}

		// Token: 0x060002FA RID: 762 RVA: 0x00012838 File Offset: 0x00010A38
		internal static void EvaluateGPULightType(HDLightType lightType, SpotLightShape spotLightShape, AreaLightShape areaLightShape, ref LightCategory lightCategory, ref GPULightType gpuLightType, ref LightVolumeType lightVolumeType)
		{
			lightCategory = LightCategory.Count;
			gpuLightType = GPULightType.Point;
			lightVolumeType = LightVolumeType.Count;
			switch (lightType)
			{
			case HDLightType.Spot:
				lightCategory = LightCategory.Punctual;
				switch (spotLightShape)
				{
				case SpotLightShape.Cone:
					gpuLightType = GPULightType.Spot;
					lightVolumeType = LightVolumeType.Cone;
					return;
				case SpotLightShape.Pyramid:
					gpuLightType = GPULightType.ProjectorPyramid;
					lightVolumeType = LightVolumeType.Cone;
					return;
				case SpotLightShape.Box:
					gpuLightType = GPULightType.ProjectorBox;
					lightVolumeType = LightVolumeType.Box;
					return;
				default:
					return;
				}
				break;
			case HDLightType.Directional:
				lightCategory = LightCategory.Punctual;
				gpuLightType = GPULightType.Directional;
				lightVolumeType = LightVolumeType.Count;
				return;
			case HDLightType.Point:
				lightCategory = LightCategory.Punctual;
				gpuLightType = GPULightType.Point;
				lightVolumeType = LightVolumeType.Sphere;
				return;
			case HDLightType.Area:
				lightCategory = LightCategory.Area;
				switch (areaLightShape)
				{
				case AreaLightShape.Rectangle:
					gpuLightType = GPULightType.Rectangle;
					lightVolumeType = LightVolumeType.Box;
					return;
				case AreaLightShape.Tube:
					gpuLightType = GPULightType.Tube;
					lightVolumeType = LightVolumeType.Box;
					return;
				case AreaLightShape.Disc:
					gpuLightType = GPULightType.Disc;
					lightVolumeType = LightVolumeType.Sphere;
					return;
				default:
					return;
				}
				break;
			default:
				return;
			}
		}

		// Token: 0x060002FB RID: 763 RVA: 0x000128E0 File Offset: 0x00010AE0
		private bool TrivialRejectLight(VisibleLight light, HDCamera hdCamera, in AOVRequestData aovRequest)
		{
			if (light.screenRect.height * (float)hdCamera.actualHeight * (light.screenRect.width * (float)hdCamera.actualWidth) < 1f)
			{
				return true;
			}
			if (light.light != null)
			{
				AOVRequestData aovrequestData = aovRequest;
				if (!aovrequestData.IsLightEnabled(light.light.gameObject))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060002FC RID: 764 RVA: 0x00012954 File Offset: 0x00010B54
		private void PreprocessLightData(ref ProcessedLightData processedData, VisibleLight light, HDCamera hdCamera)
		{
			Light light2 = light.light;
			HDAdditionalLightData hdadditionalLightData = this.GetHDAdditionalLightData(light2);
			processedData.additionalLightData = hdadditionalLightData;
			processedData.lightType = hdadditionalLightData.ComputeLightType(light2);
			processedData.distanceToCamera = (light.GetPosition() - hdCamera.camera.transform.position).magnitude;
			processedData.lightCategory = LightCategory.Count;
			processedData.gpuLightType = GPULightType.Point;
			processedData.lightVolumeType = LightVolumeType.Count;
			HDRenderPipeline.EvaluateGPULightType(processedData.lightType, processedData.additionalLightData.spotLightShape, processedData.additionalLightData.areaLightShape, ref processedData.lightCategory, ref processedData.gpuLightType, ref processedData.lightVolumeType);
			processedData.lightDistanceFade = ((processedData.gpuLightType == GPULightType.Directional) ? 1f : HDUtils.ComputeLinearDistanceFade(processedData.distanceToCamera, hdadditionalLightData.fadeDistance));
			processedData.isBakedShadowMask = this.IsBakedShadowMaskLight(light2);
		}

		// Token: 0x060002FD RID: 765 RVA: 0x00012A2C File Offset: 0x00010C2C
		private int PreprocessVisibleLights(HDCamera hdCamera, CullingResults cullResults, DebugDisplaySettings debugDisplaySettings, in AOVRequestData aovRequest)
		{
			HDShadowSettings component = hdCamera.volumeStack.GetComponent<HDShadowSettings>();
			DebugLightFilterMode debugLightFilterMode = debugDisplaySettings.GetDebugLightFilterMode();
			bool flag = debugLightFilterMode > DebugLightFilterMode.None;
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			this.m_ProcessedLightData.Resize(cullResults.visibleLights.Length, false);
			int num4 = Math.Min(cullResults.visibleLights.Length, this.m_MaxLightsOnScreen);
			this.UpdateSortKeysArray(num4);
			int num5 = 0;
			int num6 = 0;
			int length = cullResults.visibleLights.Length;
			while (num6 < length && num5 < num4)
			{
				VisibleLight visibleLight = cullResults.visibleLights[num6];
				if (!this.TrivialRejectLight(visibleLight, hdCamera, in aovRequest))
				{
					ref ProcessedLightData ptr = ref this.m_ProcessedLightData[num6];
					this.PreprocessLightData(ref ptr, visibleLight, hdCamera);
					HDAdditionalLightData additionalLightData = ptr.additionalLightData;
					HDLightType lightType = ptr.lightType;
					if ((ShaderConfig.s_AreaLights != 0 || lightType != HDLightType.Area || (additionalLightData.areaLightShape != AreaLightShape.Rectangle && additionalLightData.areaLightShape != AreaLightShape.Tube)) && (((additionalLightData.lightDimmer > 0f && (additionalLightData.affectDiffuse || additionalLightData.affectSpecular)) || additionalLightData.volumetricDimmer > 0f) && ptr.lightDistanceFade > 0f))
					{
						LightCategory lightCategory = ptr.lightCategory;
						if (lightCategory != LightCategory.Punctual)
						{
							if (lightCategory == LightCategory.Area)
							{
								if (!debugDisplaySettings.data.lightingDebugSettings.showAreaLight || num3 >= this.m_MaxAreaLightsOnScreen)
								{
									goto IL_026B;
								}
								num3++;
							}
						}
						else if (ptr.gpuLightType == GPULightType.Directional)
						{
							if (!debugDisplaySettings.data.lightingDebugSettings.showDirectionalLight || num >= this.m_MaxDirectionalLightsOnScreen)
							{
								goto IL_026B;
							}
							num++;
						}
						else
						{
							if (!debugDisplaySettings.data.lightingDebugSettings.showPunctualLight || num2 >= this.m_MaxPunctualLightsOnScreen)
							{
								goto IL_026B;
							}
							num2++;
						}
						additionalLightData.EvaluateShadowState(hdCamera, in ptr, cullResults, hdCamera.frameSettings, num6);
						if (additionalLightData.WillRenderShadowMap())
						{
							additionalLightData.ReserveShadowMap(hdCamera.camera, this.m_ShadowManager, component, this.m_ShadowInitParameters, visibleLight.screenRect);
						}
						this.ReserveCookieAtlasTexture(additionalLightData, visibleLight.light);
						if (!flag || debugLightFilterMode.IsEnabledFor(ptr.gpuLightType, additionalLightData.spotLightShape))
						{
							this.m_SortKeys[num5++] = (uint)(((int)ptr.lightCategory << 27) | (LightCategory)((int)ptr.gpuLightType << 22) | (LightCategory)((int)ptr.lightVolumeType << 17) | (LightCategory)num6);
						}
					}
				}
				IL_026B:
				num6++;
			}
			CoreUnsafeUtils.QuickSort(this.m_SortKeys, 0, num5 - 1);
			return num5;
		}

		// Token: 0x060002FE RID: 766 RVA: 0x00012CCC File Offset: 0x00010ECC
		private unsafe void PrepareGPULightdata(CommandBuffer cmd, HDCamera hdCamera, CullingResults cullResults, int processedLightCount)
		{
			Vector3 worldSpaceCameraPos = hdCamera.mainViewConstants.worldSpaceCameraPos;
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			this.m_ShadowManager.LayoutShadowMaps(this.debugDisplaySettings.data.lightingDebugSettings);
			this.m_TextureCaches.lightCookieManager.LayoutIfNeeded();
			bool flag = hdCamera.volumeStack.GetComponent<VisualEnvironment>().skyType.value == 4;
			HDShadowSettings component = hdCamera.volumeStack.GetComponent<HDShadowSettings>();
			for (int i = 0; i < processedLightCount; i++)
			{
				uint num4 = this.m_SortKeys[i];
				LightCategory lightCategory = (LightCategory)((num4 >> 27) & 31U);
				GPULightType gpulightType = (GPULightType)((num4 >> 22) & 31U);
				LightVolumeType lightVolumeType = (LightVolumeType)((num4 >> 17) & 31U);
				int num5 = (int)(num4 & 65535U);
				VisibleLight visibleLight = cullResults.visibleLights[num5];
				Light light = visibleLight.light;
				ProcessedLightData processedLightData = *this.m_ProcessedLightData[num5];
				this.m_enableBakeShadowMask = this.m_enableBakeShadowMask || processedLightData.isBakedShadowMask;
				HDAdditionalLightData additionalLightData = processedLightData.additionalLightData;
				int num6 = -1;
				if (additionalLightData.WillRenderShadowMap())
				{
					int num7;
					num6 = additionalLightData.UpdateShadowRequest(hdCamera, this.m_ShadowManager, component, visibleLight, cullResults, num5, this.debugDisplaySettings.data.lightingDebugSettings, out num7);
				}
				if (gpulightType == GPULightType.Directional)
				{
					this.GetDirectionalLightData(cmd, hdCamera, visibleLight, light, num5, num6, this.debugDisplaySettings, num, flag, ref this.m_ScreenSpaceShadowIndex, ref this.m_ScreenSpaceShadowChannelSlot);
					num++;
					if (ShaderConfig.s_CameraRelativeRendering != 0)
					{
						int num8 = this.m_lightList.directionalLights.Count - 1;
						DirectionalLightData directionalLightData = this.m_lightList.directionalLights[num8];
						directionalLightData.positionRWS -= worldSpaceCameraPos;
						this.m_lightList.directionalLights[num8] = directionalLightData;
					}
				}
				else
				{
					Vector3 vector = default(Vector3);
					this.GetLightData(cmd, hdCamera, component, visibleLight, light, num5, num6, ref vector, this.debugDisplaySettings, ref this.m_ScreenSpaceShadowIndex, ref this.m_ScreenSpaceShadowChannelSlot);
					if (lightCategory != LightCategory.Punctual)
					{
						if (lightCategory == LightCategory.Area)
						{
							num3++;
						}
					}
					else
					{
						num2++;
					}
					for (int j = 0; j < hdCamera.viewCount; j++)
					{
						Matrix4x4 worldToViewMatrix = this.GetWorldToViewMatrix(hdCamera, j);
						this.GetLightVolumeDataAndBound(lightCategory, gpulightType, lightVolumeType, visibleLight, this.m_lightList.lights[this.m_lightList.lights.Count - 1], vector, worldToViewMatrix, j);
					}
					if (ShaderConfig.s_CameraRelativeRendering != 0)
					{
						int num9 = this.m_lightList.lights.Count - 1;
						LightData lightData = this.m_lightList.lights[num9];
						lightData.positionRWS -= worldSpaceCameraPos;
						this.m_lightList.lights[num9] = lightData;
					}
				}
			}
			this.m_lightList.punctualLightCount = num2;
			this.m_lightList.areaLightCount = num3;
		}

		// Token: 0x060002FF RID: 767 RVA: 0x00012FA8 File Offset: 0x000111A8
		private bool TrivialRejectProbe(in ProcessedProbeData processedProbe, HDCamera hdCamera)
		{
			return (processedProbe.hdProbe.mode == ProbeSettings.Mode.Realtime && hdCamera.camera.cameraType == CameraType.Reflection) || !this.debugDisplaySettings.data.lightingDebugSettings.showReflectionProbe || processedProbe.weight <= 0f || (hdCamera.probeLayerMask.value & (1 << processedProbe.hdProbe.gameObject.layer)) == 0 || processedProbe.hdProbe.texture == null;
		}

		// Token: 0x06000300 RID: 768 RVA: 0x0001303C File Offset: 0x0001123C
		internal static void PreprocessReflectionProbeData(ref ProcessedProbeData processedData, VisibleReflectionProbe probe, HDCamera hdCamera)
		{
			HDAdditionalReflectionData hdadditionalReflectionData = probe.reflectionProbe.GetComponent<HDAdditionalReflectionData>();
			if (hdadditionalReflectionData == null)
			{
				hdadditionalReflectionData = HDUtils.s_DefaultHDAdditionalReflectionData;
				Vector3 vector = Vector3.one * probe.blendDistance;
				hdadditionalReflectionData.influenceVolume.boxBlendDistancePositive = vector;
				hdadditionalReflectionData.influenceVolume.boxBlendDistanceNegative = vector;
				hdadditionalReflectionData.influenceVolume.shape = InfluenceShape.Box;
			}
			HDRenderPipeline.PreprocessProbeData(ref processedData, hdadditionalReflectionData, hdCamera);
		}

		// Token: 0x06000301 RID: 769 RVA: 0x000130A4 File Offset: 0x000112A4
		internal static void PreprocessProbeData(ref ProcessedProbeData processedData, HDProbe probe, HDCamera hdCamera)
		{
			processedData.hdProbe = probe;
			processedData.weight = HDUtils.ComputeWeightedLinearFadeDistance(processedData.hdProbe.transform.position, hdCamera.camera.transform.position, processedData.hdProbe.weight, processedData.hdProbe.fadeDistance);
		}

		// Token: 0x06000302 RID: 770 RVA: 0x000130FC File Offset: 0x000112FC
		private int PreprocessVisibleProbes(HDCamera hdCamera, CullingResults cullResults, HDProbeCullingResults hdProbeCullingResults, in AOVRequestData aovRequest)
		{
			DebugLightFilterMode debugLightFilterMode = this.debugDisplaySettings.GetDebugLightFilterMode();
			bool flag = debugLightFilterMode > DebugLightFilterMode.None;
			int num = 0;
			int num2 = cullResults.visibleReflectionProbes.Length + hdProbeCullingResults.visibleProbes.Count;
			this.m_ProcessedReflectionProbeData.Resize(cullResults.visibleReflectionProbes.Length, false);
			this.m_ProcessedPlanarProbeData.Resize(hdProbeCullingResults.visibleProbes.Count, false);
			int num3 = Math.Min(num2, this.m_MaxEnvLightsOnScreen);
			this.UpdateSortKeysArray(num3);
			bool flag2 = hdCamera.frameSettings.IsEnabled(FrameSettingsField.ReflectionProbe) && (!flag || debugLightFilterMode.IsEnabledFor(ProbeSettings.ProbeType.ReflectionProbe));
			bool flag3 = hdCamera.frameSettings.IsEnabled(FrameSettingsField.PlanarProbe) && (!flag || debugLightFilterMode.IsEnabledFor(ProbeSettings.ProbeType.PlanarProbe));
			if (flag2)
			{
				for (int i = 0; i < cullResults.visibleReflectionProbes.Length; i++)
				{
					VisibleReflectionProbe visibleReflectionProbe = cullResults.visibleReflectionProbes[i];
					ref ProcessedProbeData ptr = ref this.m_ProcessedReflectionProbeData[i];
					HDRenderPipeline.PreprocessReflectionProbeData(ref ptr, visibleReflectionProbe, hdCamera);
					if (!this.TrivialRejectProbe(in ptr, hdCamera) && !(visibleReflectionProbe.reflectionProbe == null) && !visibleReflectionProbe.reflectionProbe.Equals(null) && visibleReflectionProbe.reflectionProbe.isActiveAndEnabled)
					{
						AOVRequestData aovrequestData = aovRequest;
						if (aovrequestData.IsLightEnabled(visibleReflectionProbe.reflectionProbe.gameObject))
						{
							if (visibleReflectionProbe.localToWorldMatrix.determinant == 0f)
							{
								Debug.LogError("Reflection probe " + visibleReflectionProbe.reflectionProbe.name + " has an invalid local frame and needs to be fixed.");
							}
							else if (num < num3)
							{
								LightVolumeType lightVolumeType = LightVolumeType.Box;
								if (ptr.hdProbe != null && ptr.hdProbe.influenceVolume.shape == InfluenceShape.Sphere)
								{
									lightVolumeType = LightVolumeType.Sphere;
								}
								float num4 = HDRenderPipeline.CalculateProbeLogVolume(visibleReflectionProbe.bounds);
								this.m_SortKeys[num++] = HDRenderPipeline.PackProbeKey(num4, lightVolumeType, 0U, i);
							}
						}
					}
				}
			}
			if (flag3)
			{
				for (int j = 0; j < hdProbeCullingResults.visibleProbes.Count; j++)
				{
					HDProbe hdprobe = hdProbeCullingResults.visibleProbes[j];
					HDRenderPipeline.PreprocessProbeData(this.m_ProcessedPlanarProbeData[j], hdprobe, hdCamera);
					AOVRequestData aovrequestData = aovRequest;
					if (aovrequestData.IsLightEnabled(hdprobe.gameObject) && num < num3)
					{
						LightVolumeType lightVolumeType2 = LightVolumeType.Box;
						if (hdprobe.influenceVolume.shape == InfluenceShape.Sphere)
						{
							lightVolumeType2 = LightVolumeType.Sphere;
						}
						float num5 = HDRenderPipeline.CalculateProbeLogVolume(hdprobe.bounds);
						this.m_SortKeys[num++] = HDRenderPipeline.PackProbeKey(num5, lightVolumeType2, 1U, j);
					}
				}
			}
			CoreUnsafeUtils.QuickSort(this.m_SortKeys, 0, num - 1);
			return num;
		}

		// Token: 0x06000303 RID: 771 RVA: 0x000133C4 File Offset: 0x000115C4
		private unsafe void PrepareGPUProbeData(CommandBuffer cmd, HDCamera hdCamera, CullingResults cullResults, HDProbeCullingResults hdProbeCullingResults, int processedLightCount)
		{
			Vector3 worldSpaceCameraPos = hdCamera.mainViewConstants.worldSpaceCameraPos;
			for (int i = 0; i < processedLightCount; i++)
			{
				LightVolumeType lightVolumeType;
				int num;
				int num2;
				HDRenderPipeline.UnpackProbeSortKey(this.m_SortKeys[i], out lightVolumeType, out num, out num2);
				ProcessedProbeData processedProbeData = ((num2 == 0) ? (*this.m_ProcessedReflectionProbeData[num]) : (*this.m_ProcessedPlanarProbeData[num]));
				EnvLightData envLightData = default(EnvLightData);
				if (this.GetEnvLightData(cmd, hdCamera, in processedProbeData, this.debugDisplaySettings, ref envLightData))
				{
					this.m_lightList.envLights.Add(envLightData);
					for (int j = 0; j < hdCamera.viewCount; j++)
					{
						Matrix4x4 worldToViewMatrix = this.GetWorldToViewMatrix(hdCamera, j);
						this.GetEnvLightVolumeDataAndBound(processedProbeData.hdProbe, lightVolumeType, worldToViewMatrix, j);
					}
					this.UpdateEnvLighCameraRelativetData(ref envLightData, worldSpaceCameraPos);
					int num3 = this.m_lightList.envLights.Count - 1;
					this.m_lightList.envLights[num3] = envLightData;
				}
			}
		}

		// Token: 0x06000304 RID: 772 RVA: 0x000134C0 File Offset: 0x000116C0
		private bool PrepareLightsForGPU(CommandBuffer cmd, HDCamera hdCamera, CullingResults cullResults, HDProbeCullingResults hdProbeCullingResults, DensityVolumeList densityVolumes, DebugDisplaySettings debugDisplaySettings, AOVRequestData aovRequest)
		{
			debugDisplaySettings.GetDebugLightFilterMode();
			using (new ProfilingScope(cmd, ProfilingSampler.Get<HDProfileId>(HDProfileId.PrepareLightsForGPU)))
			{
				Camera camera = hdCamera.camera;
				this.m_enableBakeShadowMask = false;
				this.m_lightList.Clear();
				this.m_CurrentSunLight = null;
				this.m_CurrentSunLightAdditionalLightData = null;
				this.m_CurrentShadowSortedSunLightIndex = -1;
				this.m_DebugSelectedLightShadowIndex = -1;
				this.m_DebugSelectedLightShadowCount = 0;
				int num = Math.Min(DecalSystem.m_DecalDatasCount, this.m_MaxDecalsOnScreen);
				this.m_ShadowManager.Clear();
				this.m_TextureCaches.reflectionPlanarProbeCache.ClearAtlasAllocator();
				this.m_ScreenSpaceShadowIndex = 0;
				this.m_ScreenSpaceShadowChannelSlot = 0;
				for (int i = 0; i < this.m_Asset.currentPlatformRenderPipelineSettings.hdShadowInitParams.maxScreenSpaceShadowSlots; i++)
				{
					this.m_CurrentScreenSpaceShadowData[i].additionalLightData = null;
					this.m_CurrentScreenSpaceShadowData[i].lightDataIndex = -1;
					this.m_CurrentScreenSpaceShadowData[i].valid = false;
				}
				if (cullResults.visibleLights.Length != 0)
				{
					int num2 = this.PreprocessVisibleLights(hdCamera, cullResults, debugDisplaySettings, in aovRequest);
					this.PrepareGPULightdata(cmd, hdCamera, cullResults, num2);
					this.m_ShadowManager.PrepareGPUShadowDatas(cullResults, hdCamera);
				}
				if (cullResults.visibleReflectionProbes.Length != 0 || hdProbeCullingResults.visibleProbes.Count != 0)
				{
					int num3 = this.PreprocessVisibleProbes(hdCamera, cullResults, hdProbeCullingResults, in aovRequest);
					this.PrepareGPUProbeData(cmd, hdCamera, cullResults, hdProbeCullingResults, num3);
				}
				HDShadowManager.instance.CheckForCulledCachedShadows();
				if (num > 0)
				{
					for (int j = 0; j < num; j++)
					{
						for (int k = 0; k < hdCamera.viewCount; k++)
						{
							this.m_lightList.lightsPerView[k].bounds.Add(DecalSystem.m_Bounds[j]);
							this.m_lightList.lightsPerView[k].lightVolumes.Add(DecalSystem.m_LightVolumes[j]);
						}
					}
				}
				this.m_densityVolumeCount = ((densityVolumes.bounds != null) ? densityVolumes.bounds.Count : 0);
				for (int l = 0; l < hdCamera.viewCount; l++)
				{
					Matrix4x4 worldToViewMatrix = this.GetWorldToViewMatrix(hdCamera, l);
					if (ShaderConfig.s_CameraRelativeRendering != 0)
					{
						worldToViewMatrix.SetColumn(3, new Vector4(0f, 0f, 0f, 1f));
					}
					int m = 0;
					int densityVolumeCount = this.m_densityVolumeCount;
					while (m < densityVolumeCount)
					{
						LightFeatureFlags lightFeatureFlags = (LightFeatureFlags)0;
						this.AddBoxVolumeDataAndBound(densityVolumes.bounds[m], LightCategory.DensityVolume, lightFeatureFlags, worldToViewMatrix, l);
						m++;
					}
				}
				this.m_TotalLightCount = this.m_lightList.lights.Count + this.m_lightList.envLights.Count + num + this.m_densityVolumeCount;
				for (int n = 1; n < hdCamera.viewCount; n++)
				{
					this.m_lightList.lightsPerView[0].bounds.AddRange(this.m_lightList.lightsPerView[n].bounds);
					this.m_lightList.lightsPerView[0].lightVolumes.AddRange(this.m_lightList.lightsPerView[n].lightVolumes);
				}
				this.UpdateDataBuffers();
				cmd.SetGlobalInt(HDShaderIDs._EnvLightIndexShift, this.m_lightList.lights.Count);
				cmd.SetGlobalInt(HDShaderIDs._DecalIndexShift, this.m_lightList.lights.Count + this.m_lightList.envLights.Count);
				cmd.SetGlobalInt(HDShaderIDs._DensityVolumeIndexShift, this.m_lightList.lights.Count + this.m_lightList.envLights.Count + num);
			}
			this.m_enableBakeShadowMask = this.m_enableBakeShadowMask && hdCamera.frameSettings.IsEnabled(FrameSettingsField.Shadowmask);
			if (debugDisplaySettings.data.lightingDebugSettings.shadowDebugMode == ShadowMapDebugMode.SingleShadow)
			{
				int num4 = (int)debugDisplaySettings.data.lightingDebugSettings.shadowMapIndex;
				if (debugDisplaySettings.data.lightingDebugSettings.shadowDebugUseSelection)
				{
					num4 = this.m_DebugSelectedLightShadowIndex;
				}
				cmd.SetGlobalInt(HDShaderIDs._DebugSingleShadowIndex, num4);
			}
			return this.m_enableBakeShadowMask;
		}

		// Token: 0x06000305 RID: 773 RVA: 0x0001390C File Offset: 0x00011B0C
		internal void ReserveCookieAtlasTexture(HDAdditionalLightData hdLightData, Light light)
		{
			switch (hdLightData.ComputeLightType(light))
			{
			case HDLightType.Spot:
				this.m_TextureCaches.lightCookieManager.ReserveSpace(((light != null) ? light.cookie : null) ?? Texture2D.whiteTexture);
				return;
			case HDLightType.Directional:
				this.m_TextureCaches.lightCookieManager.ReserveSpace(hdLightData.surfaceTexture);
				this.m_TextureCaches.lightCookieManager.ReserveSpace((light != null) ? light.cookie : null);
				return;
			case HDLightType.Point:
				break;
			case HDLightType.Area:
				if (hdLightData.areaLightShape == AreaLightShape.Rectangle)
				{
					this.m_TextureCaches.lightCookieManager.ReserveSpace(hdLightData.areaLightCookie);
				}
				break;
			default:
				return;
			}
		}

		// Token: 0x06000306 RID: 774 RVA: 0x000139B0 File Offset: 0x00011BB0
		internal void UpdateEnvLighCameraRelativetData(ref EnvLightData envLightData, Vector3 camPosWS)
		{
			if (ShaderConfig.s_CameraRelativeRendering != 0)
			{
				envLightData.capturePositionRWS -= camPosWS;
				envLightData.influencePositionRWS -= camPosWS;
				envLightData.proxyPositionRWS -= camPosWS;
			}
		}

		// Token: 0x06000307 RID: 775 RVA: 0x00013A0C File Offset: 0x00011C0C
		private static float CalculateProbeLogVolume(Bounds bounds)
		{
			float num = 8f * bounds.extents.x * bounds.extents.y * bounds.extents.z;
			return Mathf.Clamp(Mathf.Log(1f + num, 1.05f) * 1000f, 0f, 1048575f);
		}

		// Token: 0x06000308 RID: 776 RVA: 0x00013A6C File Offset: 0x00011C6C
		private static void UnpackProbeSortKey(uint sortKey, out LightVolumeType lightVolumeType, out int probeIndex, out int listType)
		{
			lightVolumeType = (LightVolumeType)((sortKey >> 9) & 3U);
			probeIndex = (int)(sortKey & 255U);
			listType = (int)((sortKey >> 8) & 1U);
		}

		// Token: 0x06000309 RID: 777 RVA: 0x00013A86 File Offset: 0x00011C86
		private static uint PackProbeKey(float logVolume, LightVolumeType lightVolumeType, uint listType, int probeIndex)
		{
			return ((uint)logVolume << 12) | (uint)((uint)lightVolumeType << 9) | (listType << 8) | (uint)(probeIndex & 255);
		}

		// Token: 0x0600030A RID: 778 RVA: 0x00013AA0 File Offset: 0x00011CA0
		private HDRenderPipeline.BuildGPULightListResources PrepareBuildGPULightListResources(HDRenderPipeline.TileAndClusterData tileAndClusterData, RTHandle depthBuffer, RTHandle stencilTexture)
		{
			return new HDRenderPipeline.BuildGPULightListResources
			{
				tileAndClusterData = tileAndClusterData,
				depthBuffer = depthBuffer,
				stencilTexture = stencilTexture,
				gBuffer = this.m_GbufferManager.GetBuffers()
			};
		}

		// Token: 0x0600030B RID: 779 RVA: 0x00013AE0 File Offset: 0x00011CE0
		private static void GenerateLightsScreenSpaceAABBs(in HDRenderPipeline.BuildGPULightListParameters parameters, in HDRenderPipeline.BuildGPULightListResources resources, CommandBuffer cmd)
		{
			if (parameters.totalLightCount != 0)
			{
				HDRenderPipeline.TileAndClusterData tileAndClusterData = resources.tileAndClusterData;
				cmd.SetComputeIntParam(parameters.screenSpaceAABBShader, HDShaderIDs.g_isOrthographic, parameters.isOrthographic ? 1 : 0);
				cmd.SetComputeIntParam(parameters.screenSpaceAABBShader, HDShaderIDs.g_iNrVisibLights, parameters.totalLightCount);
				cmd.SetComputeBufferParam(parameters.screenSpaceAABBShader, parameters.screenSpaceAABBKernel, HDShaderIDs.g_data, tileAndClusterData.convexBoundsBuffer);
				cmd.SetComputeBufferParam(parameters.screenSpaceAABBShader, parameters.screenSpaceAABBKernel, HDShaderIDs.g_vBoundsBuffer, tileAndClusterData.AABBBoundsBuffer);
				cmd.SetComputeMatrixArrayParam(parameters.screenSpaceAABBShader, HDShaderIDs.g_mProjectionArr, parameters.lightListProjHMatrices);
				cmd.SetComputeMatrixArrayParam(parameters.screenSpaceAABBShader, HDShaderIDs.g_mInvProjectionArr, parameters.lightListInvProjHMatrices);
				cmd.DispatchCompute(parameters.screenSpaceAABBShader, parameters.screenSpaceAABBKernel, (parameters.totalLightCount + 7) / 8, parameters.viewCount, 1);
			}
		}

		// Token: 0x0600030C RID: 780 RVA: 0x00013BC0 File Offset: 0x00011DC0
		private static void BigTilePrepass(in HDRenderPipeline.BuildGPULightListParameters parameters, in HDRenderPipeline.BuildGPULightListResources resources, CommandBuffer cmd)
		{
			if (parameters.runLightList && parameters.runBigTilePrepass)
			{
				HDRenderPipeline.TileAndClusterData tileAndClusterData = resources.tileAndClusterData;
				cmd.SetComputeIntParam(parameters.bigTilePrepassShader, HDShaderIDs.g_iNrVisibLights, parameters.totalLightCount);
				cmd.SetComputeIntParam(parameters.bigTilePrepassShader, HDShaderIDs.g_isOrthographic, parameters.isOrthographic ? 1 : 0);
				cmd.SetComputeIntParams(parameters.bigTilePrepassShader, HDShaderIDs.g_viDimensions, HDRenderPipeline.s_TempScreenDimArray);
				cmd.SetComputeIntParam(parameters.bigTilePrepassShader, HDShaderIDs._EnvLightIndexShift, parameters.lightList.lights.Count);
				cmd.SetComputeIntParam(parameters.bigTilePrepassShader, HDShaderIDs._DecalIndexShift, parameters.lightList.lights.Count + parameters.lightList.envLights.Count);
				cmd.SetComputeMatrixArrayParam(parameters.bigTilePrepassShader, HDShaderIDs.g_mScrProjectionArr, parameters.lightListProjscrMatrices);
				cmd.SetComputeMatrixArrayParam(parameters.bigTilePrepassShader, HDShaderIDs.g_mInvScrProjectionArr, parameters.lightListInvProjscrMatrices);
				cmd.SetComputeFloatParam(parameters.bigTilePrepassShader, HDShaderIDs.g_fNearPlane, parameters.nearClipPlane);
				cmd.SetComputeFloatParam(parameters.bigTilePrepassShader, HDShaderIDs.g_fFarPlane, parameters.farClipPlane);
				cmd.SetComputeBufferParam(parameters.bigTilePrepassShader, parameters.bigTilePrepassKernel, HDShaderIDs.g_vLightList, tileAndClusterData.bigTileLightList);
				cmd.SetComputeBufferParam(parameters.bigTilePrepassShader, parameters.bigTilePrepassKernel, HDShaderIDs.g_vBoundsBuffer, tileAndClusterData.AABBBoundsBuffer);
				cmd.SetComputeBufferParam(parameters.bigTilePrepassShader, parameters.bigTilePrepassKernel, HDShaderIDs._LightVolumeData, tileAndClusterData.lightVolumeDataBuffer);
				cmd.SetComputeBufferParam(parameters.bigTilePrepassShader, parameters.bigTilePrepassKernel, HDShaderIDs.g_data, tileAndClusterData.convexBoundsBuffer);
				cmd.DispatchCompute(parameters.bigTilePrepassShader, parameters.bigTilePrepassKernel, parameters.numBigTilesX, parameters.numBigTilesY, parameters.viewCount);
			}
		}

		// Token: 0x0600030D RID: 781 RVA: 0x00013D7C File Offset: 0x00011F7C
		private static void BuildPerTileLightList(in HDRenderPipeline.BuildGPULightListParameters parameters, in HDRenderPipeline.BuildGPULightListResources resources, ref bool tileFlagsWritten, CommandBuffer cmd)
		{
			if (parameters.runLightList && parameters.runFPTL)
			{
				HDRenderPipeline.TileAndClusterData tileAndClusterData = resources.tileAndClusterData;
				cmd.SetComputeIntParam(parameters.buildPerTileLightListShader, HDShaderIDs.g_isOrthographic, parameters.isOrthographic ? 1 : 0);
				cmd.SetComputeIntParams(parameters.buildPerTileLightListShader, HDShaderIDs.g_viDimensions, HDRenderPipeline.s_TempScreenDimArray);
				cmd.SetComputeIntParam(parameters.buildPerTileLightListShader, HDShaderIDs._EnvLightIndexShift, parameters.lightList.lights.Count);
				cmd.SetComputeIntParam(parameters.buildPerTileLightListShader, HDShaderIDs._DecalIndexShift, parameters.lightList.lights.Count + parameters.lightList.envLights.Count);
				cmd.SetComputeIntParam(parameters.buildPerTileLightListShader, HDShaderIDs.g_iNrVisibLights, parameters.totalLightCount);
				cmd.SetComputeBufferParam(parameters.buildPerTileLightListShader, parameters.buildPerTileLightListKernel, HDShaderIDs.g_vBoundsBuffer, tileAndClusterData.AABBBoundsBuffer);
				cmd.SetComputeBufferParam(parameters.buildPerTileLightListShader, parameters.buildPerTileLightListKernel, HDShaderIDs._LightVolumeData, tileAndClusterData.lightVolumeDataBuffer);
				cmd.SetComputeBufferParam(parameters.buildPerTileLightListShader, parameters.buildPerTileLightListKernel, HDShaderIDs.g_data, tileAndClusterData.convexBoundsBuffer);
				cmd.SetComputeMatrixArrayParam(parameters.buildPerTileLightListShader, HDShaderIDs.g_mScrProjectionArr, parameters.lightListProjscrMatrices);
				cmd.SetComputeMatrixArrayParam(parameters.buildPerTileLightListShader, HDShaderIDs.g_mInvScrProjectionArr, parameters.lightListInvProjscrMatrices);
				cmd.SetComputeTextureParam(parameters.buildPerTileLightListShader, parameters.buildPerTileLightListKernel, HDShaderIDs.g_depth_tex, resources.depthBuffer);
				cmd.SetComputeBufferParam(parameters.buildPerTileLightListShader, parameters.buildPerTileLightListKernel, HDShaderIDs.g_vLightList, tileAndClusterData.lightList);
				if (parameters.runBigTilePrepass)
				{
					cmd.SetComputeBufferParam(parameters.buildPerTileLightListShader, parameters.buildPerTileLightListKernel, HDShaderIDs.g_vBigTileLightList, tileAndClusterData.bigTileLightList);
				}
				if (parameters.enableFeatureVariants)
				{
					uint num = 0U;
					if (parameters.lightList.directionalLights.Count > 0)
					{
						num |= 16384U;
					}
					if (parameters.skyEnabled)
					{
						num |= 65536U;
					}
					if (!parameters.computeMaterialVariants)
					{
						num |= LightDefinitions.s_MaterialFeatureMaskFlags;
					}
					cmd.SetComputeIntParam(parameters.buildPerTileLightListShader, HDShaderIDs.g_BaseFeatureFlags, (int)num);
					cmd.SetComputeBufferParam(parameters.buildPerTileLightListShader, parameters.buildPerTileLightListKernel, HDShaderIDs.g_TileFeatureFlags, tileAndClusterData.tileFeatureFlags);
					tileFlagsWritten = true;
				}
				cmd.DispatchCompute(parameters.buildPerTileLightListShader, parameters.buildPerTileLightListKernel, parameters.numTilesFPTLX, parameters.numTilesFPTLY, parameters.viewCount);
			}
		}

		// Token: 0x0600030E RID: 782 RVA: 0x00013FC8 File Offset: 0x000121C8
		private static void VoxelLightListGeneration(in HDRenderPipeline.BuildGPULightListParameters parameters, in HDRenderPipeline.BuildGPULightListResources resources, CommandBuffer cmd)
		{
			if (parameters.runLightList)
			{
				HDRenderPipeline.TileAndClusterData tileAndClusterData = resources.tileAndClusterData;
				cmd.SetComputeBufferParam(parameters.buildPerVoxelLightListShader, HDRenderPipeline.s_ClearVoxelAtomicKernel, HDShaderIDs.g_LayeredSingleIdxBuffer, tileAndClusterData.globalLightListAtomic);
				cmd.DispatchCompute(parameters.buildPerVoxelLightListShader, HDRenderPipeline.s_ClearVoxelAtomicKernel, 1, 1, 1);
				cmd.SetComputeIntParam(parameters.buildPerVoxelLightListShader, HDShaderIDs.g_isOrthographic, parameters.isOrthographic ? 1 : 0);
				cmd.SetComputeIntParam(parameters.buildPerVoxelLightListShader, HDShaderIDs.g_iNrVisibLights, parameters.totalLightCount);
				cmd.SetComputeMatrixArrayParam(parameters.buildPerVoxelLightListShader, HDShaderIDs.g_mScrProjectionArr, parameters.lightListProjscrMatrices);
				cmd.SetComputeMatrixArrayParam(parameters.buildPerVoxelLightListShader, HDShaderIDs.g_mInvScrProjectionArr, parameters.lightListInvProjscrMatrices);
				cmd.SetComputeIntParam(parameters.buildPerVoxelLightListShader, HDShaderIDs.g_iLog2NumClusters, 6);
				cmd.SetComputeVectorParam(parameters.buildPerVoxelLightListShader, HDShaderIDs.g_screenSize, parameters.screenSize);
				cmd.SetComputeIntParam(parameters.buildPerVoxelLightListShader, HDShaderIDs.g_iNumSamplesMSAA, parameters.msaaSamples);
				cmd.SetComputeFloatParam(parameters.buildPerVoxelLightListShader, HDShaderIDs.g_fNearPlane, parameters.nearClipPlane);
				cmd.SetComputeFloatParam(parameters.buildPerVoxelLightListShader, HDShaderIDs.g_fFarPlane, parameters.farClipPlane);
				cmd.SetComputeFloatParam(parameters.buildPerVoxelLightListShader, HDShaderIDs.g_fClustScale, parameters.clusterScale);
				cmd.SetComputeFloatParam(parameters.buildPerVoxelLightListShader, HDShaderIDs.g_fClustBase, 1.02f);
				cmd.SetComputeTextureParam(parameters.buildPerVoxelLightListShader, parameters.buildPerVoxelLightListKernel, HDShaderIDs.g_depth_tex, resources.depthBuffer);
				cmd.SetComputeBufferParam(parameters.buildPerVoxelLightListShader, parameters.buildPerVoxelLightListKernel, HDShaderIDs.g_vLayeredLightList, tileAndClusterData.perVoxelLightLists);
				cmd.SetComputeBufferParam(parameters.buildPerVoxelLightListShader, parameters.buildPerVoxelLightListKernel, HDShaderIDs.g_LayeredOffset, tileAndClusterData.perVoxelOffset);
				cmd.SetComputeBufferParam(parameters.buildPerVoxelLightListShader, parameters.buildPerVoxelLightListKernel, HDShaderIDs.g_LayeredSingleIdxBuffer, tileAndClusterData.globalLightListAtomic);
				if (parameters.runBigTilePrepass)
				{
					cmd.SetComputeBufferParam(parameters.buildPerVoxelLightListShader, parameters.buildPerVoxelLightListKernel, HDShaderIDs.g_vBigTileLightList, tileAndClusterData.bigTileLightList);
				}
				cmd.SetComputeBufferParam(parameters.buildPerVoxelLightListShader, parameters.buildPerVoxelLightListKernel, HDShaderIDs.g_logBaseBuffer, tileAndClusterData.perTileLogBaseTweak);
				cmd.SetComputeBufferParam(parameters.buildPerVoxelLightListShader, parameters.buildPerVoxelLightListKernel, HDShaderIDs.g_vBoundsBuffer, tileAndClusterData.AABBBoundsBuffer);
				cmd.SetComputeBufferParam(parameters.buildPerVoxelLightListShader, parameters.buildPerVoxelLightListKernel, HDShaderIDs._LightVolumeData, tileAndClusterData.lightVolumeDataBuffer);
				cmd.SetComputeBufferParam(parameters.buildPerVoxelLightListShader, parameters.buildPerVoxelLightListKernel, HDShaderIDs.g_data, tileAndClusterData.convexBoundsBuffer);
				cmd.DispatchCompute(parameters.buildPerVoxelLightListShader, parameters.buildPerVoxelLightListKernel, parameters.numTilesClusterX, parameters.numTilesClusterY, parameters.viewCount);
			}
		}

		// Token: 0x0600030F RID: 783 RVA: 0x0001424C File Offset: 0x0001244C
		private static void BuildDispatchIndirectArguments(in HDRenderPipeline.BuildGPULightListParameters parameters, in HDRenderPipeline.BuildGPULightListResources resources, bool tileFlagsWritten, CommandBuffer cmd)
		{
			if (parameters.enableFeatureVariants)
			{
				HDRenderPipeline.TileAndClusterData tileAndClusterData = resources.tileAndClusterData;
				if (!tileFlagsWritten || parameters.computeMaterialVariants)
				{
					int num = ((!tileFlagsWritten || !parameters.computeLightVariants) ? HDRenderPipeline.s_BuildMaterialFlagsWriteKernel : HDRenderPipeline.s_BuildMaterialFlagsOrKernel);
					uint num2 = 0U;
					if (!parameters.computeLightVariants)
					{
						num2 |= LightDefinitions.s_LightFeatureMaskFlags;
					}
					if (!tileFlagsWritten)
					{
						if (parameters.lightList.directionalLights.Count > 0)
						{
							num2 |= 16384U;
						}
						if (parameters.skyEnabled)
						{
							num2 |= 65536U;
						}
						if (!parameters.computeMaterialVariants)
						{
							num2 |= LightDefinitions.s_MaterialFeatureMaskFlags;
						}
					}
					cmd.SetComputeIntParam(parameters.buildMaterialFlagsShader, HDShaderIDs.g_BaseFeatureFlags, (int)num2);
					cmd.SetComputeIntParams(parameters.buildMaterialFlagsShader, HDShaderIDs.g_viDimensions, HDRenderPipeline.s_TempScreenDimArray);
					cmd.SetComputeBufferParam(parameters.buildMaterialFlagsShader, num, HDShaderIDs.g_TileFeatureFlags, tileAndClusterData.tileFeatureFlags);
					for (int i = 0; i < resources.gBuffer.Length; i++)
					{
						cmd.SetComputeTextureParam(parameters.buildMaterialFlagsShader, num, HDShaderIDs._GBufferTexture[i], resources.gBuffer[i]);
					}
					if (resources.stencilTexture.rt.stencilFormat == GraphicsFormat.None)
					{
						cmd.SetComputeTextureParam(parameters.buildMaterialFlagsShader, num, HDShaderIDs._StencilTexture, resources.stencilTexture);
					}
					else
					{
						cmd.SetComputeTextureParam(parameters.buildMaterialFlagsShader, num, HDShaderIDs._StencilTexture, resources.stencilTexture, 0, RenderTextureSubElement.Stencil);
					}
					cmd.DispatchCompute(parameters.buildMaterialFlagsShader, num, parameters.numTilesFPTLX, parameters.numTilesFPTLY, parameters.viewCount);
				}
				if (parameters.useComputeAsPixel)
				{
					cmd.SetComputeBufferParam(parameters.clearDispatchIndirectShader, HDRenderPipeline.s_ClearDrawProceduralIndirectKernel, HDShaderIDs.g_DispatchIndirectBuffer, tileAndClusterData.dispatchIndirectBuffer);
					cmd.SetComputeIntParam(parameters.clearDispatchIndirectShader, HDShaderIDs.g_NumTiles, parameters.numTilesFPTL);
					cmd.SetComputeIntParam(parameters.clearDispatchIndirectShader, HDShaderIDs.g_VertexPerTile, 6);
					cmd.DispatchCompute(parameters.clearDispatchIndirectShader, HDRenderPipeline.s_ClearDrawProceduralIndirectKernel, 1, 1, 1);
					cmd.SetComputeBufferParam(parameters.buildDispatchIndirectShader, HDRenderPipeline.s_BuildDrawProceduralIndirectKernel, HDShaderIDs.g_DispatchIndirectBuffer, tileAndClusterData.dispatchIndirectBuffer);
					cmd.SetComputeBufferParam(parameters.buildDispatchIndirectShader, HDRenderPipeline.s_BuildDrawProceduralIndirectKernel, HDShaderIDs.g_TileList, tileAndClusterData.tileList);
					cmd.SetComputeBufferParam(parameters.buildDispatchIndirectShader, HDRenderPipeline.s_BuildDrawProceduralIndirectKernel, HDShaderIDs.g_TileFeatureFlags, tileAndClusterData.tileFeatureFlags);
					cmd.SetComputeIntParam(parameters.buildDispatchIndirectShader, HDShaderIDs.g_NumTiles, parameters.numTilesFPTL);
					cmd.SetComputeIntParam(parameters.buildDispatchIndirectShader, HDShaderIDs.g_NumTilesX, parameters.numTilesFPTLX);
					cmd.DispatchCompute(parameters.buildDispatchIndirectShader, HDRenderPipeline.s_BuildDrawProceduralIndirectKernel, (parameters.numTilesFPTL + 64 - 1) / 64, 1, parameters.viewCount);
					return;
				}
				cmd.SetComputeBufferParam(parameters.clearDispatchIndirectShader, HDRenderPipeline.s_ClearDispatchIndirectKernel, HDShaderIDs.g_DispatchIndirectBuffer, tileAndClusterData.dispatchIndirectBuffer);
				cmd.DispatchCompute(parameters.clearDispatchIndirectShader, HDRenderPipeline.s_ClearDispatchIndirectKernel, 1, 1, 1);
				cmd.SetComputeBufferParam(parameters.buildDispatchIndirectShader, HDRenderPipeline.s_BuildDispatchIndirectKernel, HDShaderIDs.g_DispatchIndirectBuffer, tileAndClusterData.dispatchIndirectBuffer);
				cmd.SetComputeBufferParam(parameters.buildDispatchIndirectShader, HDRenderPipeline.s_BuildDispatchIndirectKernel, HDShaderIDs.g_TileList, tileAndClusterData.tileList);
				cmd.SetComputeBufferParam(parameters.buildDispatchIndirectShader, HDRenderPipeline.s_BuildDispatchIndirectKernel, HDShaderIDs.g_TileFeatureFlags, tileAndClusterData.tileFeatureFlags);
				cmd.SetComputeIntParam(parameters.buildDispatchIndirectShader, HDShaderIDs.g_NumTiles, parameters.numTilesFPTL);
				cmd.SetComputeIntParam(parameters.buildDispatchIndirectShader, HDShaderIDs.g_NumTilesX, parameters.numTilesFPTLX);
				cmd.DispatchCompute(parameters.buildDispatchIndirectShader, HDRenderPipeline.s_BuildDispatchIndirectKernel, (parameters.numTilesFPTL + 64 - 1) / 64, 1, parameters.viewCount);
			}
		}

		// Token: 0x06000310 RID: 784 RVA: 0x000145AC File Offset: 0x000127AC
		private static bool DeferredUseComputeAsPixel(FrameSettings frameSettings)
		{
			return frameSettings.IsEnabled(FrameSettingsField.DeferredTile) && (!frameSettings.IsEnabled(FrameSettingsField.ComputeLightEvaluation) || HDRenderPipeline.k_PreferFragment);
		}

		// Token: 0x06000311 RID: 785 RVA: 0x000145D0 File Offset: 0x000127D0
		private HDRenderPipeline.BuildGPULightListParameters PrepareBuildGPULightListParameters(HDCamera hdCamera)
		{
			HDRenderPipeline.BuildGPULightListParameters buildGPULightListParameters = default(HDRenderPipeline.BuildGPULightListParameters);
			Camera camera = hdCamera.camera;
			int num = (int)hdCamera.screenSize.x;
			int num2 = (int)hdCamera.screenSize.y;
			HDRenderPipeline.s_TempScreenDimArray[0] = num;
			HDRenderPipeline.s_TempScreenDimArray[1] = num2;
			buildGPULightListParameters.runLightList = this.m_TotalLightCount > 0;
			buildGPULightListParameters.clearLightLists = false;
			if (hdCamera.xr.enabled)
			{
				buildGPULightListParameters.runLightList = true;
			}
			else if (!buildGPULightListParameters.runLightList && !this.m_TileAndClusterData.listsAreClear)
			{
				buildGPULightListParameters.clearLightLists = true;
			}
			Matrix4x4 matrix4x = default(Matrix4x4);
			matrix4x.SetRow(0, new Vector4(0.5f * (float)num, 0f, 0f, 0.5f * (float)num));
			matrix4x.SetRow(1, new Vector4(0f, 0.5f * (float)num2, 0f, 0.5f * (float)num2));
			matrix4x.SetRow(2, new Vector4(0f, 0f, 0.5f, 0.5f));
			matrix4x.SetRow(3, new Vector4(0f, 0f, 0f, 1f));
			buildGPULightListParameters.lightListProjscrMatrices = this.m_LightListProjscrMatrices;
			buildGPULightListParameters.lightListInvProjscrMatrices = this.m_LightListInvProjscrMatrices;
			buildGPULightListParameters.lightListProjHMatrices = this.m_LightListProjHMatrices;
			buildGPULightListParameters.lightListInvProjHMatrices = this.m_LightListInvProjHMatrices;
			for (int i = 0; i < hdCamera.viewCount; i++)
			{
				Matrix4x4 matrix4x2 = (hdCamera.xr.enabled ? hdCamera.xr.GetProjMatrix(i) : camera.projectionMatrix);
				this.m_LightListProjMatrices[i] = matrix4x2 * HDRenderPipeline.s_FlipMatrixLHSRHS;
				buildGPULightListParameters.lightListProjscrMatrices[i] = matrix4x * this.m_LightListProjMatrices[i];
				buildGPULightListParameters.lightListInvProjscrMatrices[i] = buildGPULightListParameters.lightListProjscrMatrices[i].inverse;
			}
			buildGPULightListParameters.totalLightCount = this.m_TotalLightCount;
			buildGPULightListParameters.isOrthographic = camera.orthographic;
			buildGPULightListParameters.viewCount = hdCamera.viewCount;
			buildGPULightListParameters.enableFeatureVariants = HDRenderPipeline.GetFeatureVariantsEnabled(hdCamera.frameSettings);
			buildGPULightListParameters.computeMaterialVariants = hdCamera.frameSettings.IsEnabled(FrameSettingsField.ComputeMaterialVariants);
			buildGPULightListParameters.computeLightVariants = hdCamera.frameSettings.IsEnabled(FrameSettingsField.ComputeLightVariants);
			buildGPULightListParameters.nearClipPlane = camera.nearClipPlane;
			buildGPULightListParameters.farClipPlane = camera.farClipPlane;
			buildGPULightListParameters.lightList = this.m_lightList;
			buildGPULightListParameters.skyEnabled = this.m_SkyManager.IsLightingSkyValid(hdCamera);
			buildGPULightListParameters.screenSize = hdCamera.screenSize;
			buildGPULightListParameters.msaaSamples = (int)hdCamera.msaaSamples;
			buildGPULightListParameters.useComputeAsPixel = HDRenderPipeline.DeferredUseComputeAsPixel(hdCamera.frameSettings);
			bool flag = GeometryUtils.IsProjectionMatrixOblique(this.m_LightListProjMatrices[0]);
			buildGPULightListParameters.screenSpaceAABBShader = this.buildScreenAABBShader;
			buildGPULightListParameters.screenSpaceAABBKernel = (flag ? HDRenderPipeline.s_GenAABBKernel_Oblique : HDRenderPipeline.s_GenAABBKernel);
			for (int j = 0; j < hdCamera.viewCount; j++)
			{
				matrix4x.SetRow(0, new Vector4(1f, 0f, 0f, 0f));
				matrix4x.SetRow(1, new Vector4(0f, 1f, 0f, 0f));
				matrix4x.SetRow(2, new Vector4(0f, 0f, 0.5f, 0.5f));
				matrix4x.SetRow(3, new Vector4(0f, 0f, 0f, 1f));
				buildGPULightListParameters.lightListProjHMatrices[j] = matrix4x * this.m_LightListProjMatrices[j];
				buildGPULightListParameters.lightListInvProjHMatrices[j] = buildGPULightListParameters.lightListProjHMatrices[j].inverse;
			}
			buildGPULightListParameters.runBigTilePrepass = hdCamera.frameSettings.IsEnabled(FrameSettingsField.BigTilePrepass);
			buildGPULightListParameters.bigTilePrepassShader = this.buildPerBigTileLightListShader;
			buildGPULightListParameters.bigTilePrepassKernel = HDRenderPipeline.s_GenListPerBigTileKernel;
			buildGPULightListParameters.numBigTilesX = (num + 63) / 64;
			buildGPULightListParameters.numBigTilesY = (num2 + 63) / 64;
			buildGPULightListParameters.runFPTL = hdCamera.frameSettings.fptl;
			buildGPULightListParameters.buildPerTileLightListShader = this.buildPerTileLightListShader;
			buildGPULightListParameters.buildPerTileLightListKernel = (flag ? HDRenderPipeline.s_GenListPerTileKernel_Oblique : HDRenderPipeline.s_GenListPerTileKernel);
			buildGPULightListParameters.numTilesFPTLX = HDRenderPipeline.GetNumTileFtplX(hdCamera);
			buildGPULightListParameters.numTilesFPTLY = HDRenderPipeline.GetNumTileFtplY(hdCamera);
			buildGPULightListParameters.numTilesFPTL = buildGPULightListParameters.numTilesFPTLX * buildGPULightListParameters.numTilesFPTLY;
			buildGPULightListParameters.buildPerVoxelLightListShader = this.buildPerVoxelLightListShader;
			buildGPULightListParameters.buildPerVoxelLightListKernel = (flag ? HDRenderPipeline.s_GenListPerVoxelKernelOblique : HDRenderPipeline.s_GenListPerVoxelKernel);
			buildGPULightListParameters.numTilesClusterX = HDRenderPipeline.GetNumTileClusteredX(hdCamera);
			buildGPULightListParameters.numTilesClusterY = HDRenderPipeline.GetNumTileClusteredY(hdCamera);
			double num3 = (1.0 - (double)Mathf.Pow(1.02f, 64f)) / -0.019999980926513672;
			this.m_ClusterScale = (float)(num3 / (double)(buildGPULightListParameters.farClipPlane - buildGPULightListParameters.nearClipPlane));
			buildGPULightListParameters.clusterScale = this.m_ClusterScale;
			buildGPULightListParameters.buildMaterialFlagsShader = this.buildMaterialFlagsShader;
			buildGPULightListParameters.clearDispatchIndirectShader = this.clearDispatchIndirectShader;
			buildGPULightListParameters.buildDispatchIndirectShader = this.buildDispatchIndirectShader;
			return buildGPULightListParameters;
		}

		// Token: 0x06000312 RID: 786 RVA: 0x00014B14 File Offset: 0x00012D14
		private void ClearLightList(HDCamera camera, CommandBuffer cmd, ComputeBuffer bufferToClear)
		{
			ComputeShader clearLightListsCS = this.defaultResources.shaders.clearLightListsCS;
			int num = clearLightListsCS.FindKernel("ClearList");
			cmd.SetComputeBufferParam(clearLightListsCS, num, HDShaderIDs._LightListToClear, bufferToClear);
			cmd.SetComputeIntParam(clearLightListsCS, HDShaderIDs._LightListEntries, bufferToClear.count);
			int num2 = 64;
			cmd.DispatchCompute(clearLightListsCS, num, (bufferToClear.count + num2 - 1) / num2, 1, 1);
		}

		// Token: 0x06000313 RID: 787 RVA: 0x00014B78 File Offset: 0x00012D78
		private void BuildGPULightListsCommon(HDCamera hdCamera, CommandBuffer cmd)
		{
			using (new ProfilingScope(cmd, ProfilingSampler.Get<HDProfileId>(HDProfileId.BuildLightList)))
			{
				HDRenderPipeline.BuildGPULightListParameters buildGPULightListParameters = this.PrepareBuildGPULightListParameters(hdCamera);
				HDRenderPipeline.BuildGPULightListResources buildGPULightListResources = this.PrepareBuildGPULightListResources(this.m_TileAndClusterData, this.m_SharedRTManager.GetDepthStencilBuffer(hdCamera.frameSettings.IsEnabled(FrameSettingsField.MSAA)), this.m_SharedRTManager.GetStencilBuffer(hdCamera.frameSettings.IsEnabled(FrameSettingsField.MSAA)));
				bool flag = false;
				if (buildGPULightListParameters.clearLightLists && !buildGPULightListParameters.runLightList)
				{
					this.ClearLightList(hdCamera, cmd, buildGPULightListResources.tileAndClusterData.bigTileLightList);
					this.ClearLightList(hdCamera, cmd, buildGPULightListResources.tileAndClusterData.lightList);
					this.ClearLightList(hdCamera, cmd, buildGPULightListResources.tileAndClusterData.perVoxelOffset);
					this.m_TileAndClusterData.listsAreClear = true;
				}
				else if (buildGPULightListParameters.runLightList)
				{
					this.m_TileAndClusterData.listsAreClear = false;
				}
				HDRenderPipeline.GenerateLightsScreenSpaceAABBs(in buildGPULightListParameters, in buildGPULightListResources, cmd);
				HDRenderPipeline.BigTilePrepass(in buildGPULightListParameters, in buildGPULightListResources, cmd);
				HDRenderPipeline.BuildPerTileLightList(in buildGPULightListParameters, in buildGPULightListResources, ref flag, cmd);
				HDRenderPipeline.VoxelLightListGeneration(in buildGPULightListParameters, in buildGPULightListResources, cmd);
				HDRenderPipeline.BuildDispatchIndirectArguments(in buildGPULightListParameters, in buildGPULightListResources, flag, cmd);
			}
		}

		// Token: 0x06000314 RID: 788 RVA: 0x00014CA4 File Offset: 0x00012EA4
		private void BuildGPULightLists(HDCamera hdCamera, CommandBuffer cmd)
		{
			cmd.SetRenderTarget(BuiltinRenderTextureType.None);
			this.BuildGPULightListsCommon(hdCamera, cmd);
			HDRenderPipeline.LightLoopGlobalParameters lightLoopGlobalParameters = this.PrepareLightLoopGlobalParameters(hdCamera);
			HDRenderPipeline.PushLightLoopGlobalParams(in lightLoopGlobalParameters, cmd);
		}

		// Token: 0x06000315 RID: 789 RVA: 0x00014CD8 File Offset: 0x00012ED8
		private void BindLightDataParameters(HDCamera hdCamera, CommandBuffer cmd)
		{
			HDRenderPipeline.LightDataGlobalParameters lightDataGlobalParameters = this.PrepareLightDataGlobalParameters(hdCamera);
			HDRenderPipeline.PushLightDataGlobalParams(in lightDataGlobalParameters, cmd);
		}

		// Token: 0x06000316 RID: 790 RVA: 0x00014CF8 File Offset: 0x00012EF8
		private void UpdateDataBuffers()
		{
			this.m_LightLoopLightData.directionalLightData.SetData<DirectionalLightData>(this.m_lightList.directionalLights);
			this.m_LightLoopLightData.lightData.SetData<LightData>(this.m_lightList.lights);
			this.m_LightLoopLightData.envLightData.SetData<EnvLightData>(this.m_lightList.envLights);
			this.m_LightLoopLightData.decalData.SetData(DecalSystem.m_DecalDatas, 0, 0, Math.Min(DecalSystem.m_DecalDatasCount, this.m_MaxDecalsOnScreen));
			this.m_TileAndClusterData.convexBoundsBuffer.SetData<SFiniteLightBound>(this.m_lightList.lightsPerView[0].bounds);
			this.m_TileAndClusterData.lightVolumeDataBuffer.SetData<LightVolumeData>(this.m_lightList.lightsPerView[0].lightVolumes);
		}

		// Token: 0x06000317 RID: 791 RVA: 0x00014DCC File Offset: 0x00012FCC
		private HDAdditionalLightData GetHDAdditionalLightData(Light light)
		{
			HDAdditionalLightData hdadditionalLightData = null;
			if (light != null)
			{
				light.TryGetComponent<HDAdditionalLightData>(out hdadditionalLightData);
			}
			if (hdadditionalLightData == null)
			{
				hdadditionalLightData = HDUtils.s_DefaultHDAdditionalLightData;
			}
			return hdadditionalLightData;
		}

		// Token: 0x06000318 RID: 792 RVA: 0x00014E00 File Offset: 0x00013000
		private HDRenderPipeline.LightDataGlobalParameters PrepareLightDataGlobalParameters(HDCamera hdCamera)
		{
			return new HDRenderPipeline.LightDataGlobalParameters
			{
				hdCamera = hdCamera,
				lightList = this.m_lightList,
				textureCaches = this.m_TextureCaches,
				lightData = this.m_LightLoopLightData
			};
		}

		// Token: 0x06000319 RID: 793 RVA: 0x00014E48 File Offset: 0x00013048
		private HDRenderPipeline.ShadowGlobalParameters PrepareShadowGlobalParameters(HDCamera hdCamera)
		{
			return new HDRenderPipeline.ShadowGlobalParameters
			{
				hdCamera = hdCamera,
				shadowManager = this.m_ShadowManager,
				sunLightIndex = ((this.GetHDAdditionalLightData(this.m_CurrentSunLight) != null && this.m_CurrentShadowSortedSunLightIndex >= 0) ? this.m_CurrentShadowSortedSunLightIndex : (-1))
			};
		}

		// Token: 0x0600031A RID: 794 RVA: 0x00014EAC File Offset: 0x000130AC
		private HDRenderPipeline.LightLoopGlobalParameters PrepareLightLoopGlobalParameters(HDCamera hdCamera)
		{
			return new HDRenderPipeline.LightLoopGlobalParameters
			{
				hdCamera = hdCamera,
				tileAndClusterData = this.m_TileAndClusterData,
				clusterScale = this.m_ClusterScale
			};
		}

		// Token: 0x0600031B RID: 795 RVA: 0x00014EE4 File Offset: 0x000130E4
		private static void PushLightDataGlobalParams(in HDRenderPipeline.LightDataGlobalParameters param, CommandBuffer cmd)
		{
			using (new ProfilingScope(cmd, ProfilingSampler.Get<HDProfileId>(HDProfileId.PushLightDataGlobalParameters)))
			{
				Camera camera = param.hdCamera.camera;
				cmd.SetGlobalTexture(HDShaderIDs._CookieAtlas, param.textureCaches.lightCookieManager.atlasTexture);
				cmd.SetGlobalVector(HDShaderIDs._CookieAtlasSize, param.textureCaches.lightCookieManager.GetCookieAtlasSize());
				cmd.SetGlobalVector(HDShaderIDs._CookieAtlasData, param.textureCaches.lightCookieManager.GetCookieAtlasDatas());
				cmd.SetGlobalTexture(HDShaderIDs._CookieCubeTextures, param.textureCaches.lightCookieManager.cubeCache);
				cmd.SetGlobalVector(HDShaderIDs._PlanarAtlasData, param.textureCaches.reflectionPlanarProbeCache.GetAtlasDatas());
				cmd.SetGlobalTexture(HDShaderIDs._EnvCubemapTextures, param.textureCaches.reflectionProbeCache.GetTexCache());
				cmd.SetGlobalInt(HDShaderIDs._EnvSliceSize, param.textureCaches.reflectionProbeCache.GetEnvSliceSize());
				cmd.SetGlobalTexture(HDShaderIDs._Env2DTextures, param.textureCaches.reflectionPlanarProbeCache.GetTexCache());
				cmd.SetGlobalMatrixArray(HDShaderIDs._Env2DCaptureVP, param.textureCaches.env2DCaptureVP);
				cmd.SetGlobalFloatArray(HDShaderIDs._Env2DCaptureForward, param.textureCaches.env2DCaptureForward);
				cmd.SetGlobalVectorArray(HDShaderIDs._Env2DAtlasScaleOffset, param.textureCaches.env2DAtlasScaleOffset);
				cmd.SetGlobalBuffer(HDShaderIDs._LightDatas, param.lightData.lightData);
				cmd.SetGlobalInt(HDShaderIDs._PunctualLightCount, param.lightList.punctualLightCount);
				cmd.SetGlobalInt(HDShaderIDs._AreaLightCount, param.lightList.areaLightCount);
				cmd.SetGlobalBuffer(HDShaderIDs._EnvLightDatas, param.lightData.envLightData);
				cmd.SetGlobalInt(HDShaderIDs._EnvLightCount, param.lightList.envLights.Count);
				cmd.SetGlobalBuffer(HDShaderIDs._DecalDatas, param.lightData.decalData);
				cmd.SetGlobalInt(HDShaderIDs._DecalCount, DecalSystem.m_DecalDatasCount);
				cmd.SetGlobalInt(HDShaderIDs._EnableSSRefraction, param.hdCamera.frameSettings.IsEnabled(FrameSettingsField.Refraction) ? 1 : 0);
				cmd.SetGlobalBuffer(HDShaderIDs._DirectionalLightDatas, param.lightData.directionalLightData);
				cmd.SetGlobalInt(HDShaderIDs._DirectionalLightCount, param.lightList.directionalLights.Count);
			}
		}

		// Token: 0x0600031C RID: 796 RVA: 0x00015158 File Offset: 0x00013358
		private static void PushShadowGlobalParams(in HDRenderPipeline.ShadowGlobalParameters param, CommandBuffer cmd)
		{
			using (new ProfilingScope(cmd, ProfilingSampler.Get<HDProfileId>(HDProfileId.PushShadowGlobalParameters)))
			{
				Camera camera = param.hdCamera.camera;
				param.shadowManager.SyncData();
				param.shadowManager.BindResources(cmd);
				cmd.SetGlobalInt(HDShaderIDs._DirectionalShadowIndex, param.sunLightIndex);
			}
		}

		// Token: 0x0600031D RID: 797 RVA: 0x000151C8 File Offset: 0x000133C8
		private static void PushLightLoopGlobalParams(in HDRenderPipeline.LightLoopGlobalParameters param, CommandBuffer cmd)
		{
			using (new ProfilingScope(cmd, ProfilingSampler.Get<HDProfileId>(HDProfileId.PushGlobalParameters)))
			{
				Camera camera = param.hdCamera.camera;
				cmd.SetGlobalInt(HDShaderIDs._NumTileBigTileX, HDRenderPipeline.GetNumTileBigTileX(param.hdCamera));
				cmd.SetGlobalInt(HDShaderIDs._NumTileBigTileY, HDRenderPipeline.GetNumTileBigTileY(param.hdCamera));
				cmd.SetGlobalInt(HDShaderIDs._NumTileFtplX, HDRenderPipeline.GetNumTileFtplX(param.hdCamera));
				cmd.SetGlobalInt(HDShaderIDs._NumTileFtplY, HDRenderPipeline.GetNumTileFtplY(param.hdCamera));
				cmd.SetGlobalInt(HDShaderIDs._NumTileClusteredX, HDRenderPipeline.GetNumTileClusteredX(param.hdCamera));
				cmd.SetGlobalInt(HDShaderIDs._NumTileClusteredY, HDRenderPipeline.GetNumTileClusteredY(param.hdCamera));
				if (param.hdCamera.frameSettings.IsEnabled(FrameSettingsField.BigTilePrepass))
				{
					cmd.SetGlobalBuffer(HDShaderIDs.g_vBigTileLightList, param.tileAndClusterData.bigTileLightList);
				}
				cmd.SetGlobalFloat(HDShaderIDs.g_fClustScale, param.clusterScale);
				cmd.SetGlobalFloat(HDShaderIDs.g_fClustBase, 1.02f);
				cmd.SetGlobalFloat(HDShaderIDs.g_fNearPlane, camera.nearClipPlane);
				cmd.SetGlobalFloat(HDShaderIDs.g_fFarPlane, camera.farClipPlane);
				cmd.SetGlobalInt(HDShaderIDs.g_iLog2NumClusters, 6);
				cmd.SetGlobalInt(HDShaderIDs.g_isLogBaseBufferEnabled, 1);
				cmd.SetGlobalBuffer(HDShaderIDs.g_vLayeredOffsetsBuffer, param.tileAndClusterData.perVoxelOffset);
				cmd.SetGlobalBuffer(HDShaderIDs.g_logBaseBuffer, param.tileAndClusterData.perTileLogBaseTweak);
				cmd.SetGlobalBuffer(HDShaderIDs.g_vLightListGlobal, param.tileAndClusterData.perVoxelLightLists);
			}
		}

		// Token: 0x0600031E RID: 798 RVA: 0x0001536C File Offset: 0x0001356C
		private void RenderShadowMaps(ScriptableRenderContext renderContext, CommandBuffer cmd, CullingResults cullResults, HDCamera hdCamera)
		{
			this.m_ShadowManager.RenderShadows(renderContext, cmd, cullResults, hdCamera);
			HDRenderPipeline.ShadowGlobalParameters shadowGlobalParameters = this.PrepareShadowGlobalParameters(hdCamera);
			HDRenderPipeline.PushShadowGlobalParams(in shadowGlobalParameters, cmd);
		}

		// Token: 0x0600031F RID: 799 RVA: 0x0001539A File Offset: 0x0001359A
		private bool WillRenderContactShadow()
		{
			return this.m_EnableContactShadow && this.m_ContactShadowIndex != 0;
		}

		// Token: 0x06000320 RID: 800 RVA: 0x000153AF File Offset: 0x000135AF
		private void SetContactShadowsTexture(HDCamera hdCamera, RTHandle contactShadowsRT, CommandBuffer cmd)
		{
			if (!this.WillRenderContactShadow())
			{
				cmd.SetGlobalTexture(HDShaderIDs._ContactShadowTexture, TextureXR.GetBlackUIntTexture());
				return;
			}
			cmd.SetGlobalTexture(HDShaderIDs._ContactShadowTexture, contactShadowsRT);
		}

		// Token: 0x06000321 RID: 801 RVA: 0x000153E0 File Offset: 0x000135E0
		private void GetContactShadowMask(HDAdditionalLightData hdAdditionalLightData, BoolScalableSetting contactShadowEnabled, HDCamera hdCamera, ref int contactShadowMask, ref float rayTracingShadowFlag)
		{
			contactShadowMask = 0;
			rayTracingShadowFlag = 0f;
			if (!hdAdditionalLightData.useContactShadow.Value(contactShadowEnabled) || this.m_ContactShadowIndex >= LightDefinitions.s_LightListMaxPrunedEntries)
			{
				return;
			}
			int num = 1;
			int contactShadowIndex = this.m_ContactShadowIndex;
			this.m_ContactShadowIndex = contactShadowIndex + 1;
			contactShadowMask = num << contactShadowIndex;
			if (hdCamera.frameSettings.IsEnabled(FrameSettingsField.RayTracing) && hdAdditionalLightData.rayTraceContactShadow)
			{
				rayTracingShadowFlag = 1f;
			}
		}

		// Token: 0x06000322 RID: 802 RVA: 0x00015450 File Offset: 0x00013650
		private HDRenderPipeline.ContactShadowsParameters PrepareContactShadowsParameters(HDCamera hdCamera, float firstMipOffsetY)
		{
			HDRenderPipeline.ContactShadowsParameters contactShadowsParameters = default(HDRenderPipeline.ContactShadowsParameters);
			contactShadowsParameters.contactShadowsCS = this.contactShadowComputeShader;
			contactShadowsParameters.rayTracingEnabled = hdCamera.frameSettings.IsEnabled(FrameSettingsField.RayTracing);
			if (hdCamera.frameSettings.IsEnabled(FrameSettingsField.RayTracing))
			{
				RayTracingSettings component = hdCamera.volumeStack.GetComponent<RayTracingSettings>();
				contactShadowsParameters.contactShadowsRTS = this.m_Asset.renderPipelineRayTracingResources.contactShadowRayTracingRT;
				contactShadowsParameters.rayTracingBias = component.rayBias.value;
				contactShadowsParameters.accelerationStructure = this.RequestAccelerationStructure();
				contactShadowsParameters.actualWidth = hdCamera.actualWidth;
				contactShadowsParameters.actualHeight = hdCamera.actualHeight;
			}
			contactShadowsParameters.kernel = (hdCamera.frameSettings.IsEnabled(FrameSettingsField.MSAA) ? HDRenderPipeline.s_deferredContactShadowKernelMSAA : HDRenderPipeline.s_deferredContactShadowKernel);
			float num = Mathf.Clamp(this.m_ContactShadows.fadeDistance.value, 0f, this.m_ContactShadows.maxDistance.value);
			float value = this.m_ContactShadows.maxDistance.value;
			float num2 = 1f / Math.Max(1E-06f, num);
			contactShadowsParameters.params1 = new Vector4(this.m_ContactShadows.length.value, this.m_ContactShadows.distanceScaleFactor.value, value, num2);
			contactShadowsParameters.params2 = new Vector4(firstMipOffsetY, 0f, 0f, 0f);
			contactShadowsParameters.sampleCount = this.m_ContactShadows.sampleCount;
			int num3 = 16;
			contactShadowsParameters.numTilesX = (hdCamera.actualWidth + (num3 - 1)) / num3;
			contactShadowsParameters.numTilesY = (hdCamera.actualHeight + (num3 - 1)) / num3;
			contactShadowsParameters.viewCount = hdCamera.viewCount;
			contactShadowsParameters.depthTextureParameterName = (hdCamera.frameSettings.IsEnabled(FrameSettingsField.MSAA) ? HDShaderIDs._CameraDepthValuesTexture : HDShaderIDs._CameraDepthTexture);
			return contactShadowsParameters;
		}

		// Token: 0x06000323 RID: 803 RVA: 0x00015630 File Offset: 0x00013830
		private static void RenderContactShadows(in HDRenderPipeline.ContactShadowsParameters parameters, RTHandle contactShadowRT, RTHandle depthTexture, HDRenderPipeline.LightLoopLightData lightLoopLightData, HDRenderPipeline.TileAndClusterData tileAndClusterData, CommandBuffer cmd)
		{
			cmd.SetComputeVectorParam(parameters.contactShadowsCS, HDShaderIDs._ContactShadowParamsParameters, parameters.params1);
			cmd.SetComputeVectorParam(parameters.contactShadowsCS, HDShaderIDs._ContactShadowParamsParameters2, parameters.params2);
			cmd.SetComputeIntParam(parameters.contactShadowsCS, HDShaderIDs._DirectionalContactShadowSampleCount, parameters.sampleCount);
			cmd.SetComputeBufferParam(parameters.contactShadowsCS, parameters.kernel, HDShaderIDs._DirectionalLightDatas, lightLoopLightData.directionalLightData);
			cmd.SetComputeBufferParam(parameters.contactShadowsCS, parameters.kernel, HDShaderIDs._LightDatas, lightLoopLightData.lightData);
			cmd.SetComputeBufferParam(parameters.contactShadowsCS, parameters.kernel, HDShaderIDs.g_vLightListGlobal, tileAndClusterData.lightList);
			cmd.SetComputeTextureParam(parameters.contactShadowsCS, parameters.kernel, parameters.depthTextureParameterName, depthTexture);
			cmd.SetComputeTextureParam(parameters.contactShadowsCS, parameters.kernel, HDShaderIDs._ContactShadowTextureUAV, contactShadowRT);
			cmd.DispatchCompute(parameters.contactShadowsCS, parameters.kernel, parameters.numTilesX, parameters.numTilesY, parameters.viewCount);
			if (parameters.rayTracingEnabled)
			{
				cmd.SetRayTracingShaderPass(parameters.contactShadowsRTS, "VisibilityDXR");
				cmd.SetRayTracingFloatParam(parameters.contactShadowsRTS, HDShaderIDs._RaytracingRayBias, parameters.rayTracingBias);
				cmd.SetRayTracingAccelerationStructure(parameters.contactShadowsRTS, HDShaderIDs._RaytracingAccelerationStructureName, parameters.accelerationStructure);
				cmd.SetRayTracingVectorParam(parameters.contactShadowsRTS, HDShaderIDs._ContactShadowParamsParameters, parameters.params1);
				cmd.SetRayTracingVectorParam(parameters.contactShadowsRTS, HDShaderIDs._ContactShadowParamsParameters2, parameters.params2);
				cmd.SetRayTracingBufferParam(parameters.contactShadowsRTS, HDShaderIDs._DirectionalLightDatas, lightLoopLightData.directionalLightData);
				cmd.SetRayTracingBufferParam(parameters.contactShadowsRTS, HDShaderIDs._LightDatas, lightLoopLightData.lightData);
				cmd.SetRayTracingBufferParam(parameters.contactShadowsRTS, HDShaderIDs.g_vLightListGlobal, tileAndClusterData.lightList);
				cmd.SetRayTracingTextureParam(parameters.contactShadowsRTS, HDShaderIDs._DepthTexture, depthTexture);
				cmd.SetRayTracingTextureParam(parameters.contactShadowsRTS, HDShaderIDs._ContactShadowTextureUAV, contactShadowRT);
				cmd.DispatchRays(parameters.contactShadowsRTS, "RayGenContactShadows", (uint)parameters.actualWidth, (uint)parameters.actualHeight, (uint)parameters.viewCount, null);
			}
		}

		// Token: 0x06000324 RID: 804 RVA: 0x00015860 File Offset: 0x00013A60
		private void RenderContactShadows(HDCamera hdCamera, CommandBuffer cmd)
		{
			if (!this.WillRenderContactShadow())
			{
				return;
			}
			using (new ProfilingScope(cmd, ProfilingSampler.Get<HDProfileId>(HDProfileId.ContactShadows)))
			{
				this.m_ShadowManager.BindResources(cmd);
				RTHandle rthandle = (hdCamera.frameSettings.IsEnabled(FrameSettingsField.MSAA) ? this.m_SharedRTManager.GetDepthValuesTexture() : this.m_SharedRTManager.GetDepthTexture(false));
				int y = this.m_SharedRTManager.GetDepthBufferMipChainInfo().mipLevelOffsets[1].y;
				HDRenderPipeline.ContactShadowsParameters contactShadowsParameters = this.PrepareContactShadowsParameters(hdCamera, (float)y);
				HDRenderPipeline.RenderContactShadows(in contactShadowsParameters, this.m_ContactShadowBuffer, rthandle, this.m_LightLoopLightData, this.m_TileAndClusterData, cmd);
			}
		}

		// Token: 0x06000325 RID: 805 RVA: 0x00015920 File Offset: 0x00013B20
		private HDRenderPipeline.DeferredLightingParameters PrepareDeferredLightingParameters(HDCamera hdCamera, DebugDisplaySettings debugDisplaySettings)
		{
			HDRenderPipeline.DeferredLightingParameters deferredLightingParameters = default(HDRenderPipeline.DeferredLightingParameters);
			bool flag = CoreUtils.IsSceneLightingDisabled(hdCamera.camera) || debugDisplaySettings.IsDebugDisplayEnabled();
			int actualWidth = hdCamera.actualWidth;
			int actualHeight = hdCamera.actualHeight;
			deferredLightingParameters.numTilesX = (actualWidth + 15) / 16;
			deferredLightingParameters.numTilesY = (actualHeight + 15) / 16;
			deferredLightingParameters.numTiles = deferredLightingParameters.numTilesX * deferredLightingParameters.numTilesY;
			deferredLightingParameters.enableTile = hdCamera.frameSettings.IsEnabled(FrameSettingsField.DeferredTile);
			deferredLightingParameters.outputSplitLighting = hdCamera.frameSettings.IsEnabled(FrameSettingsField.SubsurfaceScattering);
			deferredLightingParameters.useComputeLightingEvaluation = hdCamera.frameSettings.IsEnabled(FrameSettingsField.ComputeLightEvaluation);
			deferredLightingParameters.enableFeatureVariants = HDRenderPipeline.GetFeatureVariantsEnabled(hdCamera.frameSettings) && !flag;
			deferredLightingParameters.enableShadowMasks = this.m_enableBakeShadowMask;
			deferredLightingParameters.numVariants = LightDefinitions.s_NumFeatureVariants;
			deferredLightingParameters.debugDisplaySettings = debugDisplaySettings;
			deferredLightingParameters.deferredComputeShader = this.deferredComputeShader;
			deferredLightingParameters.viewCount = hdCamera.viewCount;
			deferredLightingParameters.splitLightingMat = this.GetDeferredLightingMaterial(true, deferredLightingParameters.enableShadowMasks, flag);
			deferredLightingParameters.regularLightingMat = this.GetDeferredLightingMaterial(false, deferredLightingParameters.enableShadowMasks, flag);
			return deferredLightingParameters;
		}

		// Token: 0x06000326 RID: 806 RVA: 0x00015A54 File Offset: 0x00013C54
		private HDRenderPipeline.DeferredLightingResources PrepareDeferredLightingResources()
		{
			HDRenderPipeline.DeferredLightingResources deferredLightingResources = new HDRenderPipeline.DeferredLightingResources
			{
				colorBuffers = this.m_MRTCache2
			};
			deferredLightingResources.colorBuffers[0] = this.m_CameraColorBuffer;
			deferredLightingResources.colorBuffers[1] = this.m_CameraSssDiffuseLightingBuffer;
			deferredLightingResources.depthStencilBuffer = this.m_SharedRTManager.GetDepthStencilBuffer(false);
			deferredLightingResources.depthTexture = this.m_SharedRTManager.GetDepthTexture(false);
			deferredLightingResources.lightListBuffer = this.m_TileAndClusterData.lightList;
			deferredLightingResources.tileFeatureFlagsBuffer = this.m_TileAndClusterData.tileFeatureFlags;
			deferredLightingResources.tileListBuffer = this.m_TileAndClusterData.tileList;
			deferredLightingResources.dispatchIndirectBuffer = this.m_TileAndClusterData.dispatchIndirectBuffer;
			return deferredLightingResources;
		}

		// Token: 0x06000327 RID: 807 RVA: 0x00015B14 File Offset: 0x00013D14
		private void RenderDeferredLighting(HDCamera hdCamera, CommandBuffer cmd)
		{
			if (hdCamera.frameSettings.litShaderMode != LitShaderMode.Deferred)
			{
				return;
			}
			HDRenderPipeline.DeferredLightingParameters deferredLightingParameters = this.PrepareDeferredLightingParameters(hdCamera, this.debugDisplaySettings);
			HDRenderPipeline.DeferredLightingResources deferredLightingResources = this.PrepareDeferredLightingResources();
			if (!deferredLightingParameters.enableTile)
			{
				HDRenderPipeline.RenderPixelDeferredLighting(in deferredLightingParameters, in deferredLightingResources, cmd);
				return;
			}
			if (deferredLightingParameters.useComputeLightingEvaluation && !HDRenderPipeline.k_PreferFragment)
			{
				HDRenderPipeline.RenderComputeDeferredLighting(in deferredLightingParameters, in deferredLightingResources, cmd);
				return;
			}
			HDRenderPipeline.RenderComputeAsPixelDeferredLighting(in deferredLightingParameters, in deferredLightingResources, cmd);
		}

		// Token: 0x06000328 RID: 808 RVA: 0x00015B88 File Offset: 0x00013D88
		private static void RenderComputeDeferredLighting(in HDRenderPipeline.DeferredLightingParameters parameters, in HDRenderPipeline.DeferredLightingResources resources, CommandBuffer cmd)
		{
			using (new ProfilingScope(cmd, ProfilingSampler.Get<HDProfileId>(HDProfileId.RenderDeferredLightingCompute)))
			{
				cmd.SetGlobalBuffer(HDShaderIDs.g_vLightListGlobal, resources.lightListBuffer);
				for (int i = 0; i < parameters.numVariants; i++)
				{
					int num;
					if (parameters.enableFeatureVariants)
					{
						num = (parameters.enableShadowMasks ? HDRenderPipeline.s_shadeOpaqueIndirectShadowMaskFptlKernels[i] : HDRenderPipeline.s_shadeOpaqueIndirectFptlKernels[i]);
					}
					else if (parameters.enableShadowMasks)
					{
						num = (parameters.debugDisplaySettings.IsDebugDisplayEnabled() ? HDRenderPipeline.s_shadeOpaqueDirectShadowMaskFptlDebugDisplayKernel : HDRenderPipeline.s_shadeOpaqueDirectShadowMaskFptlKernel);
					}
					else
					{
						num = (parameters.debugDisplaySettings.IsDebugDisplayEnabled() ? HDRenderPipeline.s_shadeOpaqueDirectFptlDebugDisplayKernel : HDRenderPipeline.s_shadeOpaqueDirectFptlKernel);
					}
					cmd.SetComputeTextureParam(parameters.deferredComputeShader, num, HDShaderIDs._CameraDepthTexture, resources.depthTexture);
					cmd.SetComputeTextureParam(parameters.deferredComputeShader, num, HDShaderIDs.specularLightingUAV, resources.colorBuffers[0]);
					cmd.SetComputeTextureParam(parameters.deferredComputeShader, num, HDShaderIDs.diffuseLightingUAV, resources.colorBuffers[1]);
					cmd.SetComputeTextureParam(parameters.deferredComputeShader, num, HDShaderIDs._StencilTexture, resources.depthStencilBuffer, 0, RenderTextureSubElement.Stencil);
					if (parameters.enableFeatureVariants)
					{
						cmd.SetComputeBufferParam(parameters.deferredComputeShader, num, HDShaderIDs.g_TileFeatureFlags, resources.tileFeatureFlagsBuffer);
						cmd.SetComputeIntParam(parameters.deferredComputeShader, HDShaderIDs.g_TileListOffset, i * parameters.numTiles * parameters.viewCount);
						cmd.SetComputeBufferParam(parameters.deferredComputeShader, num, HDShaderIDs.g_TileList, resources.tileListBuffer);
						cmd.DispatchCompute(parameters.deferredComputeShader, num, resources.dispatchIndirectBuffer, (uint)(i * 3 * 4));
					}
					else
					{
						cmd.DispatchCompute(parameters.deferredComputeShader, num, parameters.numTilesX * 2, parameters.numTilesY * 2, parameters.viewCount);
					}
				}
			}
		}

		// Token: 0x06000329 RID: 809 RVA: 0x00015D68 File Offset: 0x00013F68
		private static void RenderComputeAsPixelDeferredLighting(in HDRenderPipeline.DeferredLightingParameters parameters, in HDRenderPipeline.DeferredLightingResources resources, Material deferredMat, bool outputSplitLighting, CommandBuffer cmd)
		{
			CoreUtils.SetKeyword(cmd, "OUTPUT_SPLIT_LIGHTING", outputSplitLighting);
			CoreUtils.SetKeyword(cmd, "SHADOWS_SHADOWMASK", parameters.enableShadowMasks);
			if (parameters.enableFeatureVariants)
			{
				if (outputSplitLighting)
				{
					CoreUtils.SetRenderTarget(cmd, resources.colorBuffers, resources.depthStencilBuffer);
				}
				else
				{
					CoreUtils.SetRenderTarget(cmd, resources.colorBuffers[0], resources.depthStencilBuffer, 0, CubemapFace.Unknown, -1);
				}
				for (int i = 0; i < parameters.numVariants; i++)
				{
					cmd.SetGlobalInt(HDShaderIDs.g_TileListOffset, i * parameters.numTiles);
					cmd.EnableShaderKeyword(HDRenderPipeline.s_variantNames[i]);
					MeshTopology meshTopology = MeshTopology.Triangles;
					cmd.DrawProceduralIndirect(Matrix4x4.identity, deferredMat, 0, meshTopology, resources.dispatchIndirectBuffer, i * 4 * 4, null);
					cmd.DisableShaderKeyword(HDRenderPipeline.s_variantNames[i]);
				}
				return;
			}
			CoreUtils.SetKeyword(cmd, "DEBUG_DISPLAY", parameters.debugDisplaySettings.IsDebugDisplayEnabled());
			if (outputSplitLighting)
			{
				CoreUtils.DrawFullScreen(cmd, deferredMat, resources.colorBuffers, resources.depthStencilBuffer, null, 1);
				return;
			}
			CoreUtils.DrawFullScreen(cmd, deferredMat, resources.colorBuffers[0], resources.depthStencilBuffer, null, 1);
		}

		// Token: 0x0600032A RID: 810 RVA: 0x00015E90 File Offset: 0x00014090
		private static void RenderComputeAsPixelDeferredLighting(in HDRenderPipeline.DeferredLightingParameters parameters, in HDRenderPipeline.DeferredLightingResources resources, CommandBuffer cmd)
		{
			using (new ProfilingScope(cmd, ProfilingSampler.Get<HDProfileId>(HDProfileId.RenderDeferredLightingComputeAsPixel)))
			{
				cmd.SetGlobalBuffer(HDShaderIDs.g_vLightListGlobal, resources.lightListBuffer);
				cmd.SetGlobalTexture(HDShaderIDs._CameraDepthTexture, resources.depthTexture);
				cmd.SetGlobalBuffer(HDShaderIDs.g_TileFeatureFlags, resources.tileFeatureFlagsBuffer);
				cmd.SetGlobalBuffer(HDShaderIDs.g_TileList, resources.tileListBuffer);
				if (parameters.outputSplitLighting)
				{
					HDRenderPipeline.RenderComputeAsPixelDeferredLighting(in parameters, in resources, HDRenderPipeline.s_DeferredTileSplitLightingMat, true, cmd);
					HDRenderPipeline.RenderComputeAsPixelDeferredLighting(in parameters, in resources, HDRenderPipeline.s_DeferredTileRegularLightingMat, false, cmd);
				}
				else
				{
					HDRenderPipeline.RenderComputeAsPixelDeferredLighting(in parameters, in resources, HDRenderPipeline.s_DeferredTileMat, false, cmd);
				}
			}
		}

		// Token: 0x0600032B RID: 811 RVA: 0x00015F4C File Offset: 0x0001414C
		private static void RenderPixelDeferredLighting(in HDRenderPipeline.DeferredLightingParameters parameters, in HDRenderPipeline.DeferredLightingResources resources, CommandBuffer cmd)
		{
			cmd.SetGlobalBuffer(HDShaderIDs.g_vLightListGlobal, resources.lightListBuffer);
			if (parameters.outputSplitLighting)
			{
				using (new ProfilingScope(cmd, ProfilingSampler.Get<HDProfileId>(HDProfileId.RenderDeferredLightingSinglePassMRT)))
				{
					CoreUtils.DrawFullScreen(cmd, parameters.splitLightingMat, resources.colorBuffers, resources.depthStencilBuffer, null, 0);
				}
			}
			using (new ProfilingScope(cmd, ProfilingSampler.Get<HDProfileId>(HDProfileId.RenderDeferredLightingSinglePass)))
			{
				Material regularLightingMat = parameters.regularLightingMat;
				if (!parameters.outputSplitLighting)
				{
					regularLightingMat.SetInt(HDShaderIDs._StencilRef, 0);
					regularLightingMat.SetInt(HDShaderIDs._StencilMask, 6);
					regularLightingMat.SetInt(HDShaderIDs._StencilCmp, 6);
				}
				else
				{
					regularLightingMat.SetInt(HDShaderIDs._StencilRef, 2);
					regularLightingMat.SetInt(HDShaderIDs._StencilMask, 2);
					regularLightingMat.SetInt(HDShaderIDs._StencilCmp, 3);
				}
				CoreUtils.DrawFullScreen(cmd, regularLightingMat, resources.colorBuffers[0], resources.depthStencilBuffer, null, 0);
			}
		}

		// Token: 0x0600032C RID: 812 RVA: 0x00016064 File Offset: 0x00014264
		private HDRenderPipeline.LightLoopDebugOverlayParameters PrepareLightLoopDebugOverlayParameters()
		{
			return new HDRenderPipeline.LightLoopDebugOverlayParameters
			{
				debugViewTilesMaterial = this.m_DebugViewTilesMaterial,
				tileAndClusterData = this.m_TileAndClusterData,
				shadowManager = this.m_ShadowManager,
				debugSelectedLightShadowIndex = this.m_DebugSelectedLightShadowIndex,
				debugSelectedLightShadowCount = this.m_DebugSelectedLightShadowCount,
				debugShadowMapMaterial = this.m_DebugHDShadowMapMaterial,
				debugBlitMaterial = this.m_DebugBlitMaterial,
				cookieManager = this.m_TextureCaches.lightCookieManager,
				planarProbeCache = this.m_TextureCaches.reflectionPlanarProbeCache
			};
		}

		// Token: 0x0600032D RID: 813 RVA: 0x000160FC File Offset: 0x000142FC
		private static void RenderLightLoopDebugOverlay(in HDRenderPipeline.DebugParameters debugParameters, CommandBuffer cmd, ref float x, ref float y, float overlaySize, RTHandle depthTexture)
		{
			HDCamera hdCamera = debugParameters.hdCamera;
			HDRenderPipeline.LightLoopDebugOverlayParameters lightingOverlayParameters = debugParameters.lightingOverlayParameters;
			LightingDebugSettings lightingDebugSettings = debugParameters.debugDisplaySettings.data.lightingDebugSettings;
			if (lightingDebugSettings.tileClusterDebug != TileClusterDebug.None)
			{
				using (new ProfilingScope(cmd, ProfilingSampler.Get<HDProfileId>(HDProfileId.TileClusterLightingDebug)))
				{
					int actualWidth = hdCamera.actualWidth;
					int actualHeight = hdCamera.actualHeight;
					int num = (actualWidth + 15) / 16;
					int num2 = (actualHeight + 15) / 16;
					int num3 = num * num2;
					if (lightingDebugSettings.tileClusterDebug == TileClusterDebug.MaterialFeatureVariants)
					{
						if (HDRenderPipeline.GetFeatureVariantsEnabled(hdCamera.frameSettings))
						{
							lightingOverlayParameters.debugViewTilesMaterial.SetInt(HDShaderIDs._NumTiles, num3);
							lightingOverlayParameters.debugViewTilesMaterial.SetInt(HDShaderIDs._ViewTilesFlags, (int)lightingDebugSettings.tileClusterDebugByCategory);
							lightingOverlayParameters.debugViewTilesMaterial.SetVector(HDShaderIDs._MousePixelCoord, HDUtils.GetMouseCoordinates(hdCamera));
							lightingOverlayParameters.debugViewTilesMaterial.SetVector(HDShaderIDs._MouseClickPixelCoord, HDUtils.GetMouseClickCoordinates(hdCamera));
							lightingOverlayParameters.debugViewTilesMaterial.SetBuffer(HDShaderIDs.g_TileList, lightingOverlayParameters.tileAndClusterData.tileList);
							lightingOverlayParameters.debugViewTilesMaterial.SetBuffer(HDShaderIDs.g_DispatchIndirectBuffer, lightingOverlayParameters.tileAndClusterData.dispatchIndirectBuffer);
							lightingOverlayParameters.debugViewTilesMaterial.EnableKeyword("USE_FPTL_LIGHTLIST");
							lightingOverlayParameters.debugViewTilesMaterial.DisableKeyword("USE_CLUSTERED_LIGHTLIST");
							lightingOverlayParameters.debugViewTilesMaterial.DisableKeyword("SHOW_LIGHT_CATEGORIES");
							lightingOverlayParameters.debugViewTilesMaterial.EnableKeyword("SHOW_FEATURE_VARIANTS");
							if (HDRenderPipeline.DeferredUseComputeAsPixel(hdCamera.frameSettings))
							{
								lightingOverlayParameters.debugViewTilesMaterial.EnableKeyword("IS_DRAWPROCEDURALINDIRECT");
							}
							else
							{
								lightingOverlayParameters.debugViewTilesMaterial.DisableKeyword("IS_DRAWPROCEDURALINDIRECT");
							}
							cmd.DrawProcedural(Matrix4x4.identity, lightingOverlayParameters.debugViewTilesMaterial, 0, MeshTopology.Triangles, num3 * 6);
						}
					}
					else
					{
						bool flag = lightingDebugSettings.tileClusterDebug == TileClusterDebug.Cluster;
						lightingOverlayParameters.debugViewTilesMaterial.SetInt(HDShaderIDs._ViewTilesFlags, (int)lightingDebugSettings.tileClusterDebugByCategory);
						lightingOverlayParameters.debugViewTilesMaterial.SetVector(HDShaderIDs._MousePixelCoord, HDUtils.GetMouseCoordinates(hdCamera));
						lightingOverlayParameters.debugViewTilesMaterial.SetVector(HDShaderIDs._MouseClickPixelCoord, HDUtils.GetMouseClickCoordinates(hdCamera));
						lightingOverlayParameters.debugViewTilesMaterial.SetBuffer(HDShaderIDs.g_vLightListGlobal, flag ? lightingOverlayParameters.tileAndClusterData.perVoxelLightLists : lightingOverlayParameters.tileAndClusterData.lightList);
						lightingOverlayParameters.debugViewTilesMaterial.SetTexture(HDShaderIDs._CameraDepthTexture, depthTexture);
						lightingOverlayParameters.debugViewTilesMaterial.EnableKeyword(flag ? "USE_CLUSTERED_LIGHTLIST" : "USE_FPTL_LIGHTLIST");
						lightingOverlayParameters.debugViewTilesMaterial.DisableKeyword((!flag) ? "USE_CLUSTERED_LIGHTLIST" : "USE_FPTL_LIGHTLIST");
						lightingOverlayParameters.debugViewTilesMaterial.EnableKeyword("SHOW_LIGHT_CATEGORIES");
						lightingOverlayParameters.debugViewTilesMaterial.DisableKeyword("SHOW_FEATURE_VARIANTS");
						CoreUtils.DrawFullScreen(cmd, lightingOverlayParameters.debugViewTilesMaterial, 0, null, 0);
					}
				}
			}
			if (lightingDebugSettings.clearCookieAtlas)
			{
				lightingOverlayParameters.cookieManager.ResetAllocator();
				lightingOverlayParameters.cookieManager.ClearAtlasTexture(cmd);
				lightingDebugSettings.clearCookieAtlas = false;
			}
			if (lightingDebugSettings.displayCookieAtlas)
			{
				using (new ProfilingScope(cmd, ProfilingSampler.Get<HDProfileId>(HDProfileId.DisplayCookieAtlas)))
				{
					HDRenderPipeline.m_LightLoopDebugMaterialProperties.SetFloat(HDShaderIDs._DebugExposure, lightingDebugSettings.debugExposure);
					HDRenderPipeline.m_LightLoopDebugMaterialProperties.SetFloat(HDShaderIDs._Mipmap, lightingDebugSettings.cookieAtlasMipLevel);
					HDRenderPipeline.m_LightLoopDebugMaterialProperties.SetTexture(HDShaderIDs._InputTexture, lightingOverlayParameters.cookieManager.atlasTexture);
					cmd.SetViewport(new Rect(x, y, overlaySize, overlaySize));
					cmd.DrawProcedural(Matrix4x4.identity, lightingOverlayParameters.debugBlitMaterial, 0, MeshTopology.Triangles, 3, 1, HDRenderPipeline.m_LightLoopDebugMaterialProperties);
					HDUtils.NextOverlayCoord(ref x, ref y, overlaySize, overlaySize, hdCamera);
				}
			}
			if (lightingDebugSettings.displayCookieCubeArray)
			{
				using (new ProfilingScope(cmd, ProfilingSampler.Get<HDProfileId>(HDProfileId.DisplayPointLightCookieArray)))
				{
					HDRenderPipeline.m_LightLoopDebugMaterialProperties.SetFloat(HDShaderIDs._DebugExposure, lightingDebugSettings.debugExposure);
					HDRenderPipeline.m_LightLoopDebugMaterialProperties.SetTexture(HDShaderIDs._InputCubemap, lightingOverlayParameters.cookieManager.cubeCache);
					HDRenderPipeline.m_LightLoopDebugMaterialProperties.SetFloat(HDShaderIDs._Mipmap, 0f);
					HDRenderPipeline.m_LightLoopDebugMaterialProperties.SetFloat(HDShaderIDs._SliceIndex, lightingDebugSettings.cookieCubeArraySliceIndex);
					cmd.SetViewport(new Rect(x, y, overlaySize, overlaySize));
					cmd.DrawProcedural(Matrix4x4.identity, debugParameters.debugLatlongMaterial, 0, MeshTopology.Triangles, 3, 1, HDRenderPipeline.m_LightLoopDebugMaterialProperties);
					HDUtils.NextOverlayCoord(ref x, ref y, overlaySize, overlaySize, hdCamera);
				}
			}
			if (lightingDebugSettings.clearPlanarReflectionProbeAtlas)
			{
				lightingOverlayParameters.planarProbeCache.Clear(cmd);
				lightingDebugSettings.clearPlanarReflectionProbeAtlas = false;
			}
			if (lightingDebugSettings.displayPlanarReflectionProbeAtlas)
			{
				using (new ProfilingScope(cmd, ProfilingSampler.Get<HDProfileId>(HDProfileId.DisplayPlanarReflectionProbeAtlas)))
				{
					HDRenderPipeline.m_LightLoopDebugMaterialProperties.SetFloat(HDShaderIDs._DebugExposure, lightingDebugSettings.debugExposure);
					HDRenderPipeline.m_LightLoopDebugMaterialProperties.SetFloat(HDShaderIDs._Mipmap, lightingDebugSettings.planarReflectionProbeMipLevel);
					HDRenderPipeline.m_LightLoopDebugMaterialProperties.SetTexture(HDShaderIDs._InputTexture, lightingOverlayParameters.planarProbeCache.GetTexCache());
					cmd.SetViewport(new Rect(x, y, overlaySize, overlaySize));
					cmd.DrawProcedural(Matrix4x4.identity, lightingOverlayParameters.debugBlitMaterial, 0, MeshTopology.Triangles, 3, 1, HDRenderPipeline.m_LightLoopDebugMaterialProperties);
					HDUtils.NextOverlayCoord(ref x, ref y, overlaySize, overlaySize, hdCamera);
				}
			}
		}

		// Token: 0x0600032E RID: 814 RVA: 0x0001665C File Offset: 0x0001485C
		private static void RenderShadowsDebugOverlay(in HDRenderPipeline.DebugParameters debugParameters, in HDShadowManager.ShadowDebugAtlasTextures atlasTextures, CommandBuffer cmd, ref float x, ref float y, float overlaySize, MaterialPropertyBlock mpb)
		{
			LightingDebugSettings lightingDebugSettings = debugParameters.debugDisplaySettings.data.lightingDebugSettings;
			if (lightingDebugSettings.shadowDebugMode != ShadowMapDebugMode.None)
			{
				using (new ProfilingScope(cmd, ProfilingSampler.Get<HDProfileId>(HDProfileId.DisplayShadows)))
				{
					HDCamera hdCamera = debugParameters.hdCamera;
					HDRenderPipeline.LightLoopDebugOverlayParameters lightingOverlayParameters = debugParameters.lightingOverlayParameters;
					switch (lightingDebugSettings.shadowDebugMode)
					{
					case ShadowMapDebugMode.VisualizePunctualLightAtlas:
						lightingOverlayParameters.shadowManager.DisplayShadowAtlas(atlasTextures.punctualShadowAtlas, cmd, lightingOverlayParameters.debugShadowMapMaterial, x, y, overlaySize, overlaySize, lightingDebugSettings.shadowMinValue, lightingDebugSettings.shadowMaxValue, mpb);
						HDUtils.NextOverlayCoord(ref x, ref y, overlaySize, overlaySize, hdCamera);
						break;
					case ShadowMapDebugMode.VisualizeDirectionalLightAtlas:
						lightingOverlayParameters.shadowManager.DisplayShadowCascadeAtlas(atlasTextures.cascadeShadowAtlas, cmd, lightingOverlayParameters.debugShadowMapMaterial, x, y, overlaySize, overlaySize, lightingDebugSettings.shadowMinValue, lightingDebugSettings.shadowMaxValue, mpb);
						HDUtils.NextOverlayCoord(ref x, ref y, overlaySize, overlaySize, hdCamera);
						break;
					case ShadowMapDebugMode.VisualizeAreaLightAtlas:
						lightingOverlayParameters.shadowManager.DisplayAreaLightShadowAtlas(atlasTextures.areaShadowAtlas, cmd, lightingOverlayParameters.debugShadowMapMaterial, x, y, overlaySize, overlaySize, lightingDebugSettings.shadowMinValue, lightingDebugSettings.shadowMaxValue, mpb);
						HDUtils.NextOverlayCoord(ref x, ref y, overlaySize, overlaySize, hdCamera);
						break;
					case ShadowMapDebugMode.VisualizeShadowMap:
					{
						int shadowMapIndex = (int)lightingDebugSettings.shadowMapIndex;
						int num = 1;
						for (int i = shadowMapIndex; i < shadowMapIndex + num; i++)
						{
							lightingOverlayParameters.shadowManager.DisplayShadowMap(in atlasTextures, i, cmd, lightingOverlayParameters.debugShadowMapMaterial, x, y, overlaySize, overlaySize, lightingDebugSettings.shadowMinValue, lightingDebugSettings.shadowMaxValue, mpb);
							HDUtils.NextOverlayCoord(ref x, ref y, overlaySize, overlaySize, hdCamera);
						}
						break;
					}
					}
				}
			}
		}

		// Token: 0x0600032F RID: 815 RVA: 0x00016810 File Offset: 0x00014A10
		private static RTHandle ShadowHistoryBufferAllocatorFunction(string viewName, int frameIndex, RTHandleSystem rtHandleSystem)
		{
			GraphicsFormat screenSpaceShadowBufferFormat = (GraphicsFormat)(GraphicsSettings.renderPipelineAsset as HDRenderPipelineAsset).currentPlatformRenderPipelineSettings.hdShadowInitParams.screenSpaceShadowBufferFormat;
			int num = Math.Max((int)Math.Ceiling((double)((float)(RenderPipelineManager.currentPipeline as HDRenderPipeline).m_Asset.currentPlatformRenderPipelineSettings.hdShadowInitParams.maxScreenSpaceShadowSlots / 4f)), 1);
			return rtHandleSystem.Alloc(Vector2.one, num * TextureXR.slices, DepthBits.None, screenSpaceShadowBufferFormat, FilterMode.Point, TextureWrapMode.Repeat, TextureDimension.Tex2DArray, true, false, true, false, 1, 0f, false, false, true, RenderTextureMemoryless.None, string.Format("ScreenSpaceShadowHistoryBuffer{0}", frameIndex));
		}

		// Token: 0x06000330 RID: 816 RVA: 0x000168A0 File Offset: 0x00014AA0
		private static RTHandle ShadowHistoryValidityBufferAllocatorFunction(string viewName, int frameIndex, RTHandleSystem rtHandleSystem)
		{
			HDRenderPipelineAsset hdrenderPipelineAsset = GraphicsSettings.renderPipelineAsset as HDRenderPipelineAsset;
			HDRenderPipeline hdrenderPipeline = RenderPipelineManager.currentPipeline as HDRenderPipeline;
			GraphicsFormat screenSpaceShadowBufferFormat = (GraphicsFormat)hdrenderPipelineAsset.currentPlatformRenderPipelineSettings.hdShadowInitParams.screenSpaceShadowBufferFormat;
			int num = Math.Max((int)Math.Ceiling((double)((float)hdrenderPipeline.m_Asset.currentPlatformRenderPipelineSettings.hdShadowInitParams.maxScreenSpaceShadowSlots / 4f)), 1);
			return rtHandleSystem.Alloc(Vector2.one, num * TextureXR.slices, DepthBits.None, screenSpaceShadowBufferFormat, FilterMode.Point, TextureWrapMode.Repeat, TextureDimension.Tex2DArray, true, false, true, false, 1, 0f, false, false, true, RenderTextureMemoryless.None, string.Format("ShadowHistoryValidityBuffer{0}", frameIndex));
		}

		// Token: 0x06000331 RID: 817 RVA: 0x00016934 File Offset: 0x00014B34
		private static void GetShadowChannelMask(int shadowSlot, HDRenderPipeline.ScreenSpaceShadowType shadowType, ref Vector4 outputMask)
		{
			int num = shadowSlot % 4;
			if (shadowType == HDRenderPipeline.ScreenSpaceShadowType.GrayScale)
			{
				switch (num)
				{
				case 0:
					outputMask.Set(1f, 0f, 0f, 0f);
					return;
				case 1:
					outputMask.Set(0f, 1f, 0f, 0f);
					return;
				case 2:
					outputMask.Set(0f, 0f, 1f, 0f);
					return;
				case 3:
					outputMask.Set(0f, 0f, 0f, 1f);
					return;
				default:
					return;
				}
			}
			else
			{
				if (shadowType != HDRenderPipeline.ScreenSpaceShadowType.Area)
				{
					if (shadowType == HDRenderPipeline.ScreenSpaceShadowType.Color && num == 0)
					{
						outputMask.Set(1f, 1f, 1f, 0f);
					}
					return;
				}
				switch (num)
				{
				case 0:
					outputMask.Set(1f, 1f, 0f, 0f);
					return;
				case 1:
					outputMask.Set(0f, 1f, 1f, 0f);
					return;
				case 2:
					outputMask.Set(0f, 0f, 1f, 1f);
					return;
				default:
					return;
				}
			}
		}

		// Token: 0x06000332 RID: 818 RVA: 0x00016A58 File Offset: 0x00014C58
		private void InitializeScreenSpaceShadows()
		{
			if (!this.m_Asset.currentPlatformRenderPipelineSettings.hdShadowInitParams.supportScreenSpaceShadows)
			{
				return;
			}
			if (this.m_RayTracingSupported)
			{
				this.m_ScreenSpaceShadowsCS = this.m_Asset.renderPipelineRayTracingResources.shadowRaytracingCS;
				this.m_ScreenSpaceShadowsFilterCS = this.m_Asset.renderPipelineRayTracingResources.shadowFilterCS;
				this.m_ScreenSpaceShadowsRT = this.m_Asset.renderPipelineRayTracingResources.shadowRaytracingRT;
				this.m_ClearShadowTexture = this.m_ScreenSpaceShadowsCS.FindKernel("ClearShadowTexture");
				this.m_OutputShadowTextureKernel = this.m_ScreenSpaceShadowsCS.FindKernel("OutputShadowTexture");
				this.m_OutputColorShadowTextureKernel = this.m_ScreenSpaceShadowsCS.FindKernel("OutputColorShadowTexture");
				this.m_RaytracingDirectionalShadowSample = this.m_ScreenSpaceShadowsCS.FindKernel("RaytracingDirectionalShadowSample");
				this.m_RaytracingPointShadowSample = this.m_ScreenSpaceShadowsCS.FindKernel("RaytracingPointShadowSample");
				this.m_RaytracingSpotShadowSample = this.m_ScreenSpaceShadowsCS.FindKernel("RaytracingSpotShadowSample");
				this.m_AreaRaytracingAreaShadowPrepassKernel = this.m_ScreenSpaceShadowsCS.FindKernel("RaytracingAreaShadowPrepass");
				this.m_AreaRaytracingAreaShadowNewSampleKernel = this.m_ScreenSpaceShadowsCS.FindKernel("RaytracingAreaShadowNewSample");
				this.m_AreaShadowApplyTAAKernel = this.m_ScreenSpaceShadowsFilterCS.FindKernel("AreaShadowApplyTAA");
				this.m_AreaUpdateAnalyticHistoryKernel = this.m_ScreenSpaceShadowsFilterCS.FindKernel("AreaAnalyticHistoryCopy");
				this.m_AreaUpdateShadowHistoryKernel = this.m_ScreenSpaceShadowsFilterCS.FindKernel("AreaShadowHistoryCopy");
				this.m_AreaEstimateNoiseKernel = this.m_ScreenSpaceShadowsFilterCS.FindKernel("AreaShadowEstimateNoise");
				this.m_AreaFirstDenoiseKernel = this.m_ScreenSpaceShadowsFilterCS.FindKernel("AreaShadowDenoiseFirstPass");
				this.m_AreaSecondDenoiseKernel = this.m_ScreenSpaceShadowsFilterCS.FindKernel("AreaShadowDenoiseSecondPass");
				this.m_AreaShadowNoDenoiseKernel = this.m_ScreenSpaceShadowsFilterCS.FindKernel("AreaShadowNoDenoise");
			}
			HDRenderPipeline.s_ScreenSpaceShadowsMat = CoreUtils.CreateEngineMaterial(this.screenSpaceShadowsShader);
			int num = Math.Max((int)Math.Ceiling((double)((float)this.m_Asset.currentPlatformRenderPipelineSettings.hdShadowInitParams.maxScreenSpaceShadowSlots / 4f)), 1);
			GraphicsFormat screenSpaceShadowBufferFormat = (GraphicsFormat)this.m_Asset.currentPlatformRenderPipelineSettings.hdShadowInitParams.screenSpaceShadowBufferFormat;
			this.m_ScreenSpaceShadowTextureArray = RTHandles.Alloc(Vector2.one, num * TextureXR.slices, DepthBits.None, screenSpaceShadowBufferFormat, FilterMode.Point, TextureWrapMode.Repeat, TextureDimension.Tex2DArray, true, false, true, false, 1, 0f, false, false, true, RenderTextureMemoryless.None, "AreaShadowArrayBuffer");
		}

		// Token: 0x06000333 RID: 819 RVA: 0x00016C93 File Offset: 0x00014E93
		private void ReleaseScreenSpaceShadows()
		{
			CoreUtils.Destroy(HDRenderPipeline.s_ScreenSpaceShadowsMat);
			RTHandles.Release(this.m_ScreenSpaceShadowTextureArray);
		}

		// Token: 0x06000334 RID: 820 RVA: 0x00016CAA File Offset: 0x00014EAA
		private void BindBlackShadowTexture(CommandBuffer cmd)
		{
			cmd.SetGlobalTexture(HDShaderIDs._ScreenSpaceShadowsTexture, TextureXR.GetBlackTextureArray());
		}

		// Token: 0x06000335 RID: 821 RVA: 0x00016CC4 File Offset: 0x00014EC4
		private void RenderScreenSpaceShadows(HDCamera hdCamera, CommandBuffer cmd)
		{
			if (!hdCamera.frameSettings.IsEnabled(FrameSettingsField.ScreenSpaceShadows))
			{
				this.BindBlackShadowTexture(cmd);
				return;
			}
			if (hdCamera.frameSettings.IsEnabled(FrameSettingsField.RayTracing))
			{
				using (new ProfilingScope(cmd, ProfilingSampler.Get<HDProfileId>(HDProfileId.ScreenSpaceShadows)))
				{
					this.RenderDirectionalLightScreenSpaceShadow(cmd, hdCamera);
					this.RenderLightScreenSpaceShadows(hdCamera, cmd);
					this.EvaluateShadowDebugView(cmd, hdCamera);
					cmd.SetGlobalTexture(HDShaderIDs._ScreenSpaceShadowsTexture, this.m_ScreenSpaceShadowTextureArray);
					return;
				}
			}
			this.BindBlackShadowTexture(cmd);
		}

		// Token: 0x06000336 RID: 822 RVA: 0x00016D64 File Offset: 0x00014F64
		private void WriteToScreenSpaceShadowBuffer(CommandBuffer cmd, HDCamera hdCamera, RTHandle source, int shadowSlot, HDRenderPipeline.ScreenSpaceShadowType shadowType)
		{
			int actualWidth = hdCamera.actualWidth;
			int actualHeight = hdCamera.actualHeight;
			int num = 8;
			int num2 = (actualWidth + (num - 1)) / num;
			int num3 = (actualHeight + (num - 1)) / num;
			int num4 = ((shadowType == HDRenderPipeline.ScreenSpaceShadowType.Color) ? this.m_OutputColorShadowTextureKernel : this.m_OutputShadowTextureKernel);
			HDRenderPipeline.GetShadowChannelMask(shadowSlot, shadowType, ref this.m_ShadowChannelMask0);
			cmd.SetComputeIntParam(this.m_ScreenSpaceShadowsCS, HDShaderIDs._RaytracingShadowSlot, shadowSlot / 4);
			cmd.SetComputeVectorParam(this.m_ScreenSpaceShadowsCS, HDShaderIDs._RaytracingChannelMask, this.m_ShadowChannelMask0);
			cmd.SetComputeTextureParam(this.m_ScreenSpaceShadowsCS, num4, HDShaderIDs._RaytracedShadowIntegration, source);
			cmd.SetComputeTextureParam(this.m_ScreenSpaceShadowsCS, num4, HDShaderIDs._ScreenSpaceShadowsTextureRW, this.m_ScreenSpaceShadowTextureArray);
			cmd.DispatchCompute(this.m_ScreenSpaceShadowsCS, num4, num2, num3, hdCamera.viewCount);
		}

		// Token: 0x06000337 RID: 823 RVA: 0x00016E2C File Offset: 0x0001502C
		private void RenderDirectionalLightScreenSpaceShadow(CommandBuffer cmd, HDCamera hdCamera)
		{
			if (this.m_CurrentSunLightAdditionalLightData != null && this.m_CurrentSunLightAdditionalLightData.WillRenderScreenSpaceShadow())
			{
				if (this.m_CurrentSunLightAdditionalLightData.WillRenderRayTracedShadow())
				{
					using (new ProfilingScope(cmd, ProfilingSampler.Get<HDProfileId>(HDProfileId.RaytracingDirectionalLightShadow)))
					{
						RTHandle rayTracingBuffer = this.GetRayTracingBuffer(InternalRayTracingBuffers.RGBA0);
						RTHandle rayTracingBuffer2 = this.GetRayTracingBuffer(InternalRayTracingBuffers.RGBA1);
						RTHandle rayTracingBuffer3 = this.GetRayTracingBuffer(InternalRayTracingBuffers.Direction);
						this.GetRayTracingBuffer(InternalRayTracingBuffers.Distance);
						RTHandle rayTracingBuffer4 = this.GetRayTracingBuffer(InternalRayTracingBuffers.R1);
						int actualWidth = hdCamera.actualWidth;
						int actualHeight = hdCamera.actualHeight;
						int num = 8;
						int num2 = (actualWidth + (num - 1)) / num;
						int num3 = (actualHeight + (num - 1)) / num;
						cmd.SetComputeTextureParam(this.m_ScreenSpaceShadowsCS, this.m_ClearShadowTexture, HDShaderIDs._RaytracedShadowIntegration, rayTracingBuffer);
						cmd.DispatchCompute(this.m_ScreenSpaceShadowsCS, this.m_ClearShadowTexture, num2, num3, hdCamera.viewCount);
						cmd.SetComputeTextureParam(this.m_ScreenSpaceShadowsCS, this.m_ClearShadowTexture, HDShaderIDs._RaytracedShadowIntegration, rayTracingBuffer4);
						cmd.DispatchCompute(this.m_ScreenSpaceShadowsCS, this.m_ClearShadowTexture, num2, num3, hdCamera.viewCount);
						RayTracingAccelerationStructure rayTracingAccelerationStructure = this.RequestAccelerationStructure();
						cmd.SetRayTracingAccelerationStructure(this.m_ScreenSpaceShadowsRT, HDShaderIDs._RaytracingAccelerationStructureName, rayTracingAccelerationStructure);
						this.m_BlueNoise.BindDitheredRNGData8SPP(cmd);
						int num4 = this.RayTracingFrameIndex(hdCamera);
						cmd.SetComputeIntParam(this.m_ScreenSpaceShadowsCS, HDShaderIDs._RaytracingFrameIndex, num4);
						RayTracingSettings component = hdCamera.volumeStack.GetComponent<RayTracingSettings>();
						cmd.SetRayTracingFloatParam(this.m_ScreenSpaceShadowsRT, HDShaderIDs._RaytracingRayBias, component.rayBias.value);
						CoreUtils.SetKeyword(cmd, "TRANSPARENT_COLOR_SHADOW", this.m_CurrentSunLightAdditionalLightData.colorShadow);
						string text = (this.m_CurrentSunLightAdditionalLightData.colorShadow ? "RayGenDirectionalColorShadowSingle" : "RayGenDirectionalShadowSingle");
						for (int i = 0; i < this.m_CurrentSunLightAdditionalLightData.numRayTracingSamples; i++)
						{
							cmd.SetComputeBufferParam(this.m_ScreenSpaceShadowsCS, this.m_RaytracingDirectionalShadowSample, HDShaderIDs._DirectionalLightDatas, this.m_LightLoopLightData.directionalLightData);
							cmd.SetComputeIntParam(this.m_ScreenSpaceShadowsCS, HDShaderIDs._DirectionalShadowIndex, this.m_CurrentShadowSortedSunLightIndex);
							cmd.SetComputeIntParam(this.m_ScreenSpaceShadowsCS, HDShaderIDs._RaytracingSampleIndex, i);
							cmd.SetComputeIntParam(this.m_ScreenSpaceShadowsCS, HDShaderIDs._RaytracingNumSamples, this.m_CurrentSunLightAdditionalLightData.numRayTracingSamples);
							cmd.SetComputeTextureParam(this.m_ScreenSpaceShadowsCS, this.m_RaytracingDirectionalShadowSample, HDShaderIDs._DepthTexture, this.m_SharedRTManager.GetDepthStencilBuffer(false));
							cmd.SetComputeTextureParam(this.m_ScreenSpaceShadowsCS, this.m_RaytracingDirectionalShadowSample, HDShaderIDs._NormalBufferTexture, this.m_SharedRTManager.GetNormalBuffer(false));
							cmd.SetComputeTextureParam(this.m_ScreenSpaceShadowsCS, this.m_RaytracingDirectionalShadowSample, HDShaderIDs._RaytracingDirectionBuffer, rayTracingBuffer3);
							cmd.DispatchCompute(this.m_ScreenSpaceShadowsCS, this.m_RaytracingDirectionalShadowSample, num2, num3, hdCamera.viewCount);
							cmd.SetRayTracingShaderPass(this.m_ScreenSpaceShadowsRT, "VisibilityDXR");
							RayCountManager rayCountManager = this.GetRayCountManager();
							cmd.SetRayTracingIntParam(this.m_ScreenSpaceShadowsRT, HDShaderIDs._RayCountEnabled, rayCountManager.RayCountIsEnabled());
							cmd.SetRayTracingTextureParam(this.m_ScreenSpaceShadowsRT, HDShaderIDs._RayCountTexture, rayCountManager.GetRayCountTexture());
							cmd.SetRayTracingTextureParam(this.m_ScreenSpaceShadowsRT, HDShaderIDs._DepthTexture, this.m_SharedRTManager.GetDepthStencilBuffer(false));
							cmd.SetRayTracingTextureParam(this.m_ScreenSpaceShadowsRT, HDShaderIDs._NormalBufferTexture, this.m_SharedRTManager.GetNormalBuffer(false));
							cmd.SetRayTracingTextureParam(this.m_ScreenSpaceShadowsRT, HDShaderIDs._RaytracingDirectionBuffer, rayTracingBuffer3);
							cmd.SetRayTracingIntParam(this.m_ScreenSpaceShadowsRT, HDShaderIDs._RaytracingNumSamples, this.m_CurrentSunLightAdditionalLightData.numRayTracingSamples);
							cmd.SetRayTracingTextureParam(this.m_ScreenSpaceShadowsRT, this.m_CurrentSunLightAdditionalLightData.colorShadow ? HDShaderIDs._RaytracedColorShadowIntegration : HDShaderIDs._RaytracedShadowIntegration, rayTracingBuffer);
							cmd.SetRayTracingTextureParam(this.m_ScreenSpaceShadowsRT, HDShaderIDs._VelocityBuffer, rayTracingBuffer4);
							cmd.DispatchRays(this.m_ScreenSpaceShadowsRT, text, (uint)hdCamera.actualWidth, (uint)hdCamera.actualHeight, (uint)hdCamera.viewCount, null);
						}
						CoreUtils.SetKeyword(cmd, "TRANSPARENT_COLOR_SHADOW", false);
						RTHandle rthandle = hdCamera.GetCurrentFrameRT(9) ?? hdCamera.AllocHistoryFrameRT(9, new Func<string, int, RTHandleSystem, RTHandle>(HDRenderPipeline.ShadowHistoryBufferAllocatorFunction), 1);
						RTHandle rthandle2 = hdCamera.GetCurrentFrameRT(10) ?? hdCamera.AllocHistoryFrameRT(10, new Func<string, int, RTHandleSystem, RTHandle>(HDRenderPipeline.ShadowHistoryValidityBufferAllocatorFunction), 1);
						int num5 = this.m_CurrentSunLightDirectionalLightData.screenSpaceShadowIndex & (int)LightDefinitions.s_ScreenSpaceShadowIndexMask;
						HDRenderPipeline.GetShadowChannelMask(num5, this.m_CurrentSunLightAdditionalLightData.colorShadow ? HDRenderPipeline.ScreenSpaceShadowType.Color : HDRenderPipeline.ScreenSpaceShadowType.GrayScale, ref this.m_ShadowChannelMask0);
						if (this.m_CurrentSunLightAdditionalLightData.filterTracedShadow)
						{
							float num6 = 1f;
							if (this.m_CurrentSunLightAdditionalLightData.previousTransform.rotation != this.m_CurrentSunLightAdditionalLightData.transform.localToWorldMatrix.rotation || !hdCamera.ValidShadowHistory(this.m_CurrentSunLightAdditionalLightData, num5, GPULightType.Directional))
							{
								num6 = 0f;
							}
							num6 *= (this.ValidRayTracingHistory(hdCamera) ? 1f : 0f);
							this.GetTemporalFilter().DenoiseBuffer(cmd, hdCamera, rayTracingBuffer, rthandle, rthandle2, rayTracingBuffer4, rayTracingBuffer2, num5 / 4, this.m_ShadowChannelMask0, !this.m_CurrentSunLightAdditionalLightData.colorShadow, num6);
							this.GetSimpleDenoiser().DenoiseBufferNoHistory(cmd, hdCamera, rayTracingBuffer2, rayTracingBuffer, this.m_CurrentSunLightAdditionalLightData.filterSizeTraced, !this.m_CurrentSunLightAdditionalLightData.colorShadow);
							hdCamera.PropagateShadowHistory(this.m_CurrentSunLightAdditionalLightData, num5, GPULightType.Directional);
						}
						this.WriteToScreenSpaceShadowBuffer(cmd, hdCamera, rayTracingBuffer, num5, this.m_CurrentSunLightAdditionalLightData.colorShadow ? HDRenderPipeline.ScreenSpaceShadowType.Color : HDRenderPipeline.ScreenSpaceShadowType.GrayScale);
						return;
					}
				}
				CoreUtils.SetRenderTarget(cmd, this.m_ScreenSpaceShadowTextureArray, ClearFlag.None, 0, CubemapFace.Unknown, this.m_CurrentSunLightDirectionalLightData.screenSpaceShadowIndex);
				HDUtils.DrawFullScreen(cmd, HDRenderPipeline.s_ScreenSpaceShadowsMat, this.m_ScreenSpaceShadowTextureArray, null, 0);
			}
		}

		// Token: 0x06000338 RID: 824 RVA: 0x000173EC File Offset: 0x000155EC
		private bool RenderLightScreenSpaceShadows(HDCamera hdCamera, CommandBuffer cmd)
		{
			bool flag;
			using (new ProfilingScope(cmd, ProfilingSampler.Get<HDProfileId>(HDProfileId.RaytracingLightShadow)))
			{
				RTHandle rthandle = hdCamera.GetCurrentFrameRT(9) ?? hdCamera.AllocHistoryFrameRT(9, new Func<string, int, RTHandleSystem, RTHandle>(HDRenderPipeline.ShadowHistoryBufferAllocatorFunction), 1);
				RTHandle rthandle2 = hdCamera.GetCurrentFrameRT(10) ?? hdCamera.AllocHistoryFrameRT(10, new Func<string, int, RTHandleSystem, RTHandle>(HDRenderPipeline.ShadowHistoryValidityBufferAllocatorFunction), 1);
				RayTracingAccelerationStructure rayTracingAccelerationStructure = this.RequestAccelerationStructure();
				cmd.SetRayTracingAccelerationStructure(this.m_ScreenSpaceShadowsRT, HDShaderIDs._RaytracingAccelerationStructureName, rayTracingAccelerationStructure);
				cmd.SetRayTracingShaderPass(this.m_ScreenSpaceShadowsRT, "VisibilityDXR");
				this.m_BlueNoise.BindDitheredRNGData8SPP(cmd);
				using (new ProfilingScope(cmd, ProfilingSampler.Get<HDProfileId>(HDProfileId.RaytracingLightShadow)))
				{
					for (int i = 0; i < this.m_ScreenSpaceShadowIndex; i++)
					{
						if (this.m_CurrentScreenSpaceShadowData[i].valid)
						{
							LightData lightData = this.m_lightList.lights[this.m_CurrentScreenSpaceShadowData[i].lightDataIndex];
							HDAdditionalLightData additionalLightData = this.m_CurrentScreenSpaceShadowData[i].additionalLightData;
							GPULightType lightType = lightData.lightType;
							if (lightType - GPULightType.Point > 1)
							{
								if (lightType == GPULightType.Rectangle)
								{
									this.RenderAreaScreenSpaceShadow(cmd, hdCamera, in lightData, additionalLightData, this.m_CurrentScreenSpaceShadowData[i].lightDataIndex, rthandle, rthandle2);
								}
							}
							else
							{
								this.RenderPunctualScreenSpaceShadow(cmd, hdCamera, in lightData, additionalLightData, this.m_CurrentScreenSpaceShadowData[i].lightDataIndex, rthandle, rthandle2);
							}
						}
					}
				}
				flag = true;
			}
			return flag;
		}

		// Token: 0x06000339 RID: 825 RVA: 0x000175AC File Offset: 0x000157AC
		private void RenderAreaScreenSpaceShadow(CommandBuffer cmd, HDCamera hdCamera, in LightData lightData, HDAdditionalLightData additionalLightData, int lightIndex, RTHandle shadowHistoryArray, RTHandle shadowHistoryValidityArray)
		{
			if (hdCamera.frameSettings.litShaderMode != LitShaderMode.Deferred)
			{
				return;
			}
			int actualWidth = hdCamera.actualWidth;
			int actualHeight = hdCamera.actualHeight;
			int num = 8;
			int num2 = (actualWidth + (num - 1)) / num;
			int num3 = (actualHeight + (num - 1)) / num;
			RTHandle rayTracingBuffer = this.GetRayTracingBuffer(InternalRayTracingBuffers.RG0);
			RTHandle rayTracingBuffer2 = this.GetRayTracingBuffer(InternalRayTracingBuffers.RGBA0);
			RTHandle rayTracingBuffer3 = this.GetRayTracingBuffer(InternalRayTracingBuffers.RGBA1);
			RTHandle rayTracingBuffer4 = this.GetRayTracingBuffer(InternalRayTracingBuffers.Direction);
			RTHandle rayTracingBuffer5 = this.GetRayTracingBuffer(InternalRayTracingBuffers.Distance);
			this.m_WorldToLocalArea.SetColumn(0, lightData.right);
			this.m_WorldToLocalArea.SetColumn(1, lightData.up);
			this.m_WorldToLocalArea.SetColumn(2, lightData.forward);
			Vector3 vector = lightData.positionRWS;
			if (ShaderConfig.s_CameraRelativeRendering != 0)
			{
				vector += hdCamera.camera.transform.position;
			}
			this.m_WorldToLocalArea.SetColumn(3, vector);
			this.m_WorldToLocalArea.m33 = 1f;
			this.m_WorldToLocalArea = this.m_WorldToLocalArea.inverse;
			cmd.SetComputeBufferParam(this.m_ScreenSpaceShadowsCS, this.m_AreaRaytracingAreaShadowPrepassKernel, HDShaderIDs._LightDatas, this.m_LightLoopLightData.lightData);
			cmd.SetComputeMatrixParam(this.m_ScreenSpaceShadowsCS, HDShaderIDs._RaytracingAreaWorldToLocal, this.m_WorldToLocalArea);
			cmd.SetComputeIntParam(this.m_ScreenSpaceShadowsCS, HDShaderIDs._RaytracingTargetAreaLight, lightIndex);
			cmd.SetComputeIntParam(this.m_ScreenSpaceShadowsCS, HDShaderIDs._RaytracingNumSamples, additionalLightData.numRayTracingSamples);
			int num4 = this.RayTracingFrameIndex(hdCamera);
			cmd.SetComputeIntParam(this.m_ScreenSpaceShadowsCS, HDShaderIDs._RaytracingFrameIndex, num4);
			cmd.SetComputeTextureParam(this.m_ScreenSpaceShadowsCS, this.m_AreaRaytracingAreaShadowPrepassKernel, HDShaderIDs._DepthTexture, this.m_SharedRTManager.GetDepthStencilBuffer(false));
			cmd.SetComputeTextureParam(this.m_ScreenSpaceShadowsCS, this.m_AreaRaytracingAreaShadowPrepassKernel, HDShaderIDs._NormalBufferTexture, this.m_SharedRTManager.GetNormalBuffer(false));
			cmd.SetComputeTextureParam(this.m_ScreenSpaceShadowsCS, this.m_AreaRaytracingAreaShadowPrepassKernel, HDShaderIDs._GBufferTexture[0], this.m_GbufferManager.GetBuffer(0));
			cmd.SetComputeTextureParam(this.m_ScreenSpaceShadowsCS, this.m_AreaRaytracingAreaShadowPrepassKernel, HDShaderIDs._GBufferTexture[1], this.m_GbufferManager.GetBuffer(1));
			cmd.SetComputeTextureParam(this.m_ScreenSpaceShadowsCS, this.m_AreaRaytracingAreaShadowPrepassKernel, HDShaderIDs._GBufferTexture[2], this.m_GbufferManager.GetBuffer(2));
			cmd.SetComputeTextureParam(this.m_ScreenSpaceShadowsCS, this.m_AreaRaytracingAreaShadowPrepassKernel, HDShaderIDs._GBufferTexture[3], this.m_GbufferManager.GetBuffer(3));
			cmd.SetComputeTextureParam(this.m_ScreenSpaceShadowsCS, this.m_AreaRaytracingAreaShadowPrepassKernel, HDShaderIDs._CookieAtlas, this.m_TextureCaches.lightCookieManager.atlasTexture);
			cmd.SetComputeTextureParam(this.m_ScreenSpaceShadowsCS, this.m_AreaRaytracingAreaShadowPrepassKernel, HDShaderIDs._RaytracedAreaShadowIntegration, rayTracingBuffer2);
			cmd.SetComputeTextureParam(this.m_ScreenSpaceShadowsCS, this.m_AreaRaytracingAreaShadowPrepassKernel, HDShaderIDs._RaytracedAreaShadowSample, rayTracingBuffer3);
			cmd.SetComputeTextureParam(this.m_ScreenSpaceShadowsCS, this.m_AreaRaytracingAreaShadowPrepassKernel, HDShaderIDs._RaytracingDirectionBuffer, rayTracingBuffer4);
			cmd.SetComputeTextureParam(this.m_ScreenSpaceShadowsCS, this.m_AreaRaytracingAreaShadowPrepassKernel, HDShaderIDs._RaytracingDistanceBuffer, rayTracingBuffer5);
			cmd.SetComputeTextureParam(this.m_ScreenSpaceShadowsCS, this.m_AreaRaytracingAreaShadowPrepassKernel, HDShaderIDs._AnalyticProbBuffer, rayTracingBuffer);
			cmd.DispatchCompute(this.m_ScreenSpaceShadowsCS, this.m_AreaRaytracingAreaShadowPrepassKernel, num2, num3, hdCamera.viewCount);
			RayCountManager rayCountManager = this.GetRayCountManager();
			cmd.SetRayTracingIntParam(this.m_ScreenSpaceShadowsRT, HDShaderIDs._RayCountEnabled, rayCountManager.RayCountIsEnabled());
			cmd.SetRayTracingTextureParam(this.m_ScreenSpaceShadowsRT, HDShaderIDs._RayCountTexture, rayCountManager.GetRayCountTexture());
			cmd.SetRayTracingBufferParam(this.m_ScreenSpaceShadowsRT, HDShaderIDs._LightDatas, this.m_LightLoopLightData.lightData);
			cmd.SetRayTracingTextureParam(this.m_ScreenSpaceShadowsRT, HDShaderIDs._DepthTexture, this.m_SharedRTManager.GetDepthStencilBuffer(false));
			cmd.SetRayTracingTextureParam(this.m_ScreenSpaceShadowsRT, HDShaderIDs._NormalBufferTexture, this.m_SharedRTManager.GetNormalBuffer(false));
			cmd.SetRayTracingTextureParam(this.m_ScreenSpaceShadowsRT, HDShaderIDs._AnalyticProbBuffer, rayTracingBuffer);
			cmd.SetRayTracingTextureParam(this.m_ScreenSpaceShadowsRT, HDShaderIDs._RaytracedAreaShadowSample, rayTracingBuffer3);
			cmd.SetRayTracingTextureParam(this.m_ScreenSpaceShadowsRT, HDShaderIDs._RaytracingDirectionBuffer, rayTracingBuffer4);
			cmd.SetRayTracingTextureParam(this.m_ScreenSpaceShadowsRT, HDShaderIDs._RaytracingDistanceBuffer, rayTracingBuffer5);
			cmd.SetRayTracingIntParam(this.m_ScreenSpaceShadowsRT, HDShaderIDs._RaytracingTargetAreaLight, lightIndex);
			RayTracingSettings component = hdCamera.volumeStack.GetComponent<RayTracingSettings>();
			cmd.SetRayTracingFloatParam(this.m_ScreenSpaceShadowsRT, HDShaderIDs._RaytracingRayBias, component.rayBias.value);
			cmd.SetRayTracingTextureParam(this.m_ScreenSpaceShadowsRT, HDShaderIDs._RaytracedAreaShadowIntegration, rayTracingBuffer2);
			cmd.DispatchRays(this.m_ScreenSpaceShadowsRT, "RayGenAreaShadowSingle", (uint)hdCamera.actualWidth, (uint)hdCamera.actualHeight, (uint)hdCamera.viewCount, null);
			for (int i = 1; i < additionalLightData.numRayTracingSamples; i++)
			{
				cmd.SetComputeBufferParam(this.m_ScreenSpaceShadowsCS, this.m_AreaRaytracingAreaShadowNewSampleKernel, HDShaderIDs._LightDatas, this.m_LightLoopLightData.lightData);
				cmd.SetComputeIntParam(this.m_ScreenSpaceShadowsCS, HDShaderIDs._RaytracingTargetAreaLight, lightIndex);
				cmd.SetComputeIntParam(this.m_ScreenSpaceShadowsCS, HDShaderIDs._RaytracingSampleIndex, i);
				cmd.SetComputeMatrixParam(this.m_ScreenSpaceShadowsCS, HDShaderIDs._RaytracingAreaWorldToLocal, this.m_WorldToLocalArea);
				cmd.SetComputeIntParam(this.m_ScreenSpaceShadowsCS, HDShaderIDs._RaytracingNumSamples, additionalLightData.numRayTracingSamples);
				cmd.SetComputeTextureParam(this.m_ScreenSpaceShadowsCS, this.m_AreaRaytracingAreaShadowNewSampleKernel, HDShaderIDs._DepthTexture, this.m_SharedRTManager.GetDepthStencilBuffer(false));
				cmd.SetComputeTextureParam(this.m_ScreenSpaceShadowsCS, this.m_AreaRaytracingAreaShadowNewSampleKernel, HDShaderIDs._NormalBufferTexture, this.m_SharedRTManager.GetNormalBuffer(false));
				cmd.SetComputeTextureParam(this.m_ScreenSpaceShadowsCS, this.m_AreaRaytracingAreaShadowNewSampleKernel, HDShaderIDs._GBufferTexture[0], this.m_GbufferManager.GetBuffer(0));
				cmd.SetComputeTextureParam(this.m_ScreenSpaceShadowsCS, this.m_AreaRaytracingAreaShadowNewSampleKernel, HDShaderIDs._GBufferTexture[1], this.m_GbufferManager.GetBuffer(1));
				cmd.SetComputeTextureParam(this.m_ScreenSpaceShadowsCS, this.m_AreaRaytracingAreaShadowNewSampleKernel, HDShaderIDs._GBufferTexture[2], this.m_GbufferManager.GetBuffer(2));
				cmd.SetComputeTextureParam(this.m_ScreenSpaceShadowsCS, this.m_AreaRaytracingAreaShadowNewSampleKernel, HDShaderIDs._GBufferTexture[3], this.m_GbufferManager.GetBuffer(3));
				cmd.SetComputeTextureParam(this.m_ScreenSpaceShadowsCS, this.m_AreaRaytracingAreaShadowNewSampleKernel, HDShaderIDs._CookieAtlas, this.m_TextureCaches.lightCookieManager.atlasTexture);
				cmd.SetComputeTextureParam(this.m_ScreenSpaceShadowsCS, this.m_AreaRaytracingAreaShadowNewSampleKernel, HDShaderIDs._RaytracedAreaShadowSample, rayTracingBuffer3);
				cmd.SetComputeTextureParam(this.m_ScreenSpaceShadowsCS, this.m_AreaRaytracingAreaShadowNewSampleKernel, HDShaderIDs._RaytracingDirectionBuffer, rayTracingBuffer4);
				cmd.SetComputeTextureParam(this.m_ScreenSpaceShadowsCS, this.m_AreaRaytracingAreaShadowNewSampleKernel, HDShaderIDs._RaytracingDistanceBuffer, rayTracingBuffer5);
				cmd.SetComputeTextureParam(this.m_ScreenSpaceShadowsCS, this.m_AreaRaytracingAreaShadowNewSampleKernel, HDShaderIDs._AnalyticProbBuffer, rayTracingBuffer);
				cmd.DispatchCompute(this.m_ScreenSpaceShadowsCS, this.m_AreaRaytracingAreaShadowNewSampleKernel, num2, num3, hdCamera.viewCount);
				cmd.SetRayTracingBufferParam(this.m_ScreenSpaceShadowsRT, HDShaderIDs._LightDatas, this.m_LightLoopLightData.lightData);
				cmd.SetRayTracingTextureParam(this.m_ScreenSpaceShadowsRT, HDShaderIDs._DepthTexture, this.m_SharedRTManager.GetDepthStencilBuffer(false));
				cmd.SetRayTracingTextureParam(this.m_ScreenSpaceShadowsRT, HDShaderIDs._NormalBufferTexture, this.m_SharedRTManager.GetNormalBuffer(false));
				cmd.SetRayTracingTextureParam(this.m_ScreenSpaceShadowsRT, HDShaderIDs._RaytracedAreaShadowSample, rayTracingBuffer3);
				cmd.SetRayTracingTextureParam(this.m_ScreenSpaceShadowsRT, HDShaderIDs._RaytracingDirectionBuffer, rayTracingBuffer4);
				cmd.SetRayTracingTextureParam(this.m_ScreenSpaceShadowsRT, HDShaderIDs._RaytracingDistanceBuffer, rayTracingBuffer5);
				cmd.SetRayTracingTextureParam(this.m_ScreenSpaceShadowsRT, HDShaderIDs._AnalyticProbBuffer, rayTracingBuffer);
				cmd.SetRayTracingIntParam(this.m_ScreenSpaceShadowsRT, HDShaderIDs._RaytracingTargetAreaLight, lightIndex);
				cmd.SetRayTracingTextureParam(this.m_ScreenSpaceShadowsRT, HDShaderIDs._RaytracedAreaShadowIntegration, rayTracingBuffer2);
				cmd.DispatchRays(this.m_ScreenSpaceShadowsRT, "RayGenAreaShadowSingle", (uint)hdCamera.actualWidth, (uint)hdCamera.actualHeight, (uint)hdCamera.viewCount, null);
			}
			if (additionalLightData.filterTracedShadow)
			{
				int screenSpaceShadowIndex = this.m_lightList.lights[lightIndex].screenSpaceShadowIndex;
				HDRenderPipeline.GetShadowChannelMask(screenSpaceShadowIndex, HDRenderPipeline.ScreenSpaceShadowType.Area, ref this.m_ShadowChannelMask0);
				HDRenderPipeline.GetShadowChannelMask(screenSpaceShadowIndex, HDRenderPipeline.ScreenSpaceShadowType.GrayScale, ref this.m_ShadowChannelMask1);
				HDRenderPipeline.GetShadowChannelMask(screenSpaceShadowIndex + 1, HDRenderPipeline.ScreenSpaceShadowType.GrayScale, ref this.m_ShadowChannelMask2);
				cmd.SetComputeIntParam(this.m_ScreenSpaceShadowsFilterCS, HDShaderIDs._RaytracingDenoiseRadius, additionalLightData.filterSizeTraced);
				cmd.SetComputeIntParam(this.m_ScreenSpaceShadowsFilterCS, HDShaderIDs._DenoisingHistorySlice, screenSpaceShadowIndex / 4);
				cmd.SetComputeVectorParam(this.m_ScreenSpaceShadowsFilterCS, HDShaderIDs._DenoisingHistoryMask, this.m_ShadowChannelMask0);
				cmd.SetComputeVectorParam(this.m_ScreenSpaceShadowsFilterCS, HDShaderIDs._DenoisingHistoryMaskSn, this.m_ShadowChannelMask1);
				cmd.SetComputeVectorParam(this.m_ScreenSpaceShadowsFilterCS, HDShaderIDs._DenoisingHistoryMaskUn, this.m_ShadowChannelMask2);
				Vector2 vector2 = new Vector2((float)hdCamera.actualWidth / (float)shadowHistoryArray.rt.width, (float)hdCamera.actualHeight / (float)shadowHistoryArray.rt.height);
				cmd.SetComputeVectorParam(this.m_ScreenSpaceShadowsFilterCS, HDShaderIDs._RTHandleScaleHistory, vector2);
				float num5 = (this.ValidRayTracingHistory(hdCamera) ? 1f : 0f);
				cmd.SetComputeTextureParam(this.m_ScreenSpaceShadowsFilterCS, this.m_AreaShadowApplyTAAKernel, HDShaderIDs._AnalyticProbBuffer, rayTracingBuffer);
				cmd.SetComputeTextureParam(this.m_ScreenSpaceShadowsFilterCS, this.m_AreaShadowApplyTAAKernel, HDShaderIDs._DepthTexture, this.m_SharedRTManager.GetDepthStencilBuffer(false));
				cmd.SetComputeTextureParam(this.m_ScreenSpaceShadowsFilterCS, this.m_AreaShadowApplyTAAKernel, HDShaderIDs._AreaShadowHistory, shadowHistoryArray);
				cmd.SetComputeTextureParam(this.m_ScreenSpaceShadowsFilterCS, this.m_AreaShadowApplyTAAKernel, HDShaderIDs._AnalyticHistoryBuffer, shadowHistoryValidityArray);
				cmd.SetComputeTextureParam(this.m_ScreenSpaceShadowsFilterCS, this.m_AreaShadowApplyTAAKernel, HDShaderIDs._DenoiseInputTexture, rayTracingBuffer2);
				cmd.SetComputeTextureParam(this.m_ScreenSpaceShadowsFilterCS, this.m_AreaShadowApplyTAAKernel, HDShaderIDs._DenoiseOutputTextureRW, rayTracingBuffer3);
				cmd.SetComputeFloatParam(this.m_ScreenSpaceShadowsFilterCS, HDShaderIDs._HistoryValidity, num5);
				cmd.DispatchCompute(this.m_ScreenSpaceShadowsFilterCS, this.m_AreaShadowApplyTAAKernel, num2, num3, hdCamera.viewCount);
				cmd.SetComputeTextureParam(this.m_ScreenSpaceShadowsFilterCS, this.m_AreaUpdateAnalyticHistoryKernel, HDShaderIDs._AnalyticProbBuffer, rayTracingBuffer);
				cmd.SetComputeTextureParam(this.m_ScreenSpaceShadowsFilterCS, this.m_AreaUpdateAnalyticHistoryKernel, HDShaderIDs._AnalyticHistoryBuffer, shadowHistoryValidityArray);
				cmd.DispatchCompute(this.m_ScreenSpaceShadowsFilterCS, this.m_AreaUpdateAnalyticHistoryKernel, num2, num3, hdCamera.viewCount);
				cmd.SetComputeTextureParam(this.m_ScreenSpaceShadowsFilterCS, this.m_AreaUpdateShadowHistoryKernel, HDShaderIDs._DenoiseInputTexture, rayTracingBuffer3);
				cmd.SetComputeTextureParam(this.m_ScreenSpaceShadowsFilterCS, this.m_AreaUpdateShadowHistoryKernel, HDShaderIDs._AreaShadowHistoryRW, shadowHistoryArray);
				cmd.DispatchCompute(this.m_ScreenSpaceShadowsFilterCS, this.m_AreaUpdateShadowHistoryKernel, num2, num3, hdCamera.viewCount);
				cmd.SetComputeTextureParam(this.m_ScreenSpaceShadowsFilterCS, this.m_AreaEstimateNoiseKernel, HDShaderIDs._DepthTexture, this.m_SharedRTManager.GetDepthStencilBuffer(false));
				cmd.SetComputeTextureParam(this.m_ScreenSpaceShadowsFilterCS, this.m_AreaEstimateNoiseKernel, HDShaderIDs._NormalBufferTexture, this.m_SharedRTManager.GetNormalBuffer(false));
				cmd.SetComputeTextureParam(this.m_ScreenSpaceShadowsFilterCS, this.m_AreaEstimateNoiseKernel, HDShaderIDs._ScramblingTexture, this.m_Asset.renderPipelineResources.textures.scramblingTex);
				cmd.SetComputeTextureParam(this.m_ScreenSpaceShadowsFilterCS, this.m_AreaEstimateNoiseKernel, HDShaderIDs._DenoiseInputTexture, rayTracingBuffer3);
				cmd.SetComputeTextureParam(this.m_ScreenSpaceShadowsFilterCS, this.m_AreaEstimateNoiseKernel, HDShaderIDs._DenoiseOutputTextureRW, rayTracingBuffer2);
				cmd.DispatchCompute(this.m_ScreenSpaceShadowsFilterCS, this.m_AreaEstimateNoiseKernel, num2, num3, hdCamera.viewCount);
				cmd.SetComputeTextureParam(this.m_ScreenSpaceShadowsFilterCS, this.m_AreaFirstDenoiseKernel, HDShaderIDs._DepthTexture, this.m_SharedRTManager.GetDepthStencilBuffer(false));
				cmd.SetComputeTextureParam(this.m_ScreenSpaceShadowsFilterCS, this.m_AreaFirstDenoiseKernel, HDShaderIDs._NormalBufferTexture, this.m_SharedRTManager.GetNormalBuffer(false));
				cmd.SetComputeTextureParam(this.m_ScreenSpaceShadowsFilterCS, this.m_AreaFirstDenoiseKernel, HDShaderIDs._ScreenSpaceShadowsTextureRW, this.m_ScreenSpaceShadowTextureArray);
				cmd.SetComputeTextureParam(this.m_ScreenSpaceShadowsFilterCS, this.m_AreaFirstDenoiseKernel, HDShaderIDs._DenoiseInputTexture, rayTracingBuffer2);
				cmd.SetComputeTextureParam(this.m_ScreenSpaceShadowsFilterCS, this.m_AreaFirstDenoiseKernel, HDShaderIDs._DenoiseOutputTextureRW, rayTracingBuffer3);
				cmd.DispatchCompute(this.m_ScreenSpaceShadowsFilterCS, this.m_AreaFirstDenoiseKernel, num2, num3, hdCamera.viewCount);
				cmd.SetComputeTextureParam(this.m_ScreenSpaceShadowsFilterCS, this.m_AreaSecondDenoiseKernel, HDShaderIDs._DepthTexture, this.m_SharedRTManager.GetDepthStencilBuffer(false));
				cmd.SetComputeTextureParam(this.m_ScreenSpaceShadowsFilterCS, this.m_AreaSecondDenoiseKernel, HDShaderIDs._NormalBufferTexture, this.m_SharedRTManager.GetNormalBuffer(false));
				cmd.SetComputeTextureParam(this.m_ScreenSpaceShadowsFilterCS, this.m_AreaSecondDenoiseKernel, HDShaderIDs._DenoiseInputTexture, rayTracingBuffer3);
				cmd.SetComputeTextureParam(this.m_ScreenSpaceShadowsFilterCS, this.m_AreaSecondDenoiseKernel, HDShaderIDs._DenoiseOutputTextureRW, rayTracingBuffer2);
				cmd.DispatchCompute(this.m_ScreenSpaceShadowsFilterCS, this.m_AreaSecondDenoiseKernel, num2, num3, hdCamera.viewCount);
				this.WriteToScreenSpaceShadowBuffer(cmd, hdCamera, rayTracingBuffer2, screenSpaceShadowIndex, HDRenderPipeline.ScreenSpaceShadowType.Area);
				hdCamera.PropagateShadowHistory(additionalLightData, screenSpaceShadowIndex, GPULightType.Rectangle);
				return;
			}
			int screenSpaceShadowIndex2 = lightData.screenSpaceShadowIndex;
			int num6 = screenSpaceShadowIndex2 / 4;
			HDRenderPipeline.GetShadowChannelMask(screenSpaceShadowIndex2, HDRenderPipeline.ScreenSpaceShadowType.Area, ref this.m_ShadowChannelMask0);
			cmd.SetComputeVectorParam(this.m_ScreenSpaceShadowsFilterCS, HDShaderIDs._DenoisingHistoryMask, this.m_ShadowChannelMask0);
			cmd.SetComputeIntParam(this.m_ScreenSpaceShadowsFilterCS, HDShaderIDs._DenoisingHistorySlice, num6);
			cmd.SetComputeTextureParam(this.m_ScreenSpaceShadowsFilterCS, this.m_AreaShadowNoDenoiseKernel, HDShaderIDs._DenoiseInputTexture, rayTracingBuffer2);
			cmd.SetComputeTextureParam(this.m_ScreenSpaceShadowsFilterCS, this.m_AreaShadowNoDenoiseKernel, HDShaderIDs._ScreenSpaceShadowsTextureRW, this.m_ScreenSpaceShadowTextureArray);
			cmd.DispatchCompute(this.m_ScreenSpaceShadowsFilterCS, this.m_AreaShadowNoDenoiseKernel, num2, num3, hdCamera.viewCount);
		}

		// Token: 0x0600033A RID: 826 RVA: 0x00018394 File Offset: 0x00016594
		private void RenderPunctualScreenSpaceShadow(CommandBuffer cmd, HDCamera hdCamera, in LightData lightData, HDAdditionalLightData additionalLightData, int lightIndex, RTHandle shadowHistoryArray, RTHandle shadowHistoryValidityArray)
		{
			int actualWidth = hdCamera.actualWidth;
			int actualHeight = hdCamera.actualHeight;
			int num = 8;
			int num2 = (actualWidth + (num - 1)) / num;
			int num3 = (actualHeight + (num - 1)) / num;
			RTHandle rayTracingBuffer = this.GetRayTracingBuffer(InternalRayTracingBuffers.RGBA0);
			RTHandle rayTracingBuffer2 = this.GetRayTracingBuffer(InternalRayTracingBuffers.RGBA1);
			RTHandle rayTracingBuffer3 = this.GetRayTracingBuffer(InternalRayTracingBuffers.Direction);
			RTHandle rayTracingBuffer4 = this.GetRayTracingBuffer(InternalRayTracingBuffers.Distance);
			RTHandle rayTracingBuffer5 = this.GetRayTracingBuffer(InternalRayTracingBuffers.R1);
			cmd.SetComputeTextureParam(this.m_ScreenSpaceShadowsCS, this.m_ClearShadowTexture, HDShaderIDs._RaytracedShadowIntegration, rayTracingBuffer);
			cmd.DispatchCompute(this.m_ScreenSpaceShadowsCS, this.m_ClearShadowTexture, num2, num3, hdCamera.viewCount);
			cmd.SetComputeTextureParam(this.m_ScreenSpaceShadowsCS, this.m_ClearShadowTexture, HDShaderIDs._RaytracedShadowIntegration, rayTracingBuffer5);
			cmd.DispatchCompute(this.m_ScreenSpaceShadowsCS, this.m_ClearShadowTexture, num2, num3, hdCamera.viewCount);
			RayTracingSettings component = hdCamera.volumeStack.GetComponent<RayTracingSettings>();
			cmd.SetRayTracingFloatParam(this.m_ScreenSpaceShadowsRT, HDShaderIDs._RaytracingRayBias, component.rayBias.value);
			for (int i = 0; i < additionalLightData.numRayTracingSamples; i++)
			{
				int num4 = ((lightData.lightType == GPULightType.Point) ? this.m_RaytracingPointShadowSample : this.m_RaytracingSpotShadowSample);
				cmd.SetComputeBufferParam(this.m_ScreenSpaceShadowsCS, num4, HDShaderIDs._LightDatas, this.m_LightLoopLightData.lightData);
				cmd.SetComputeIntParam(this.m_ScreenSpaceShadowsCS, HDShaderIDs._RaytracingTargetAreaLight, lightIndex);
				cmd.SetComputeIntParam(this.m_ScreenSpaceShadowsCS, HDShaderIDs._RaytracingSampleIndex, i);
				cmd.SetComputeIntParam(this.m_ScreenSpaceShadowsCS, HDShaderIDs._RaytracingNumSamples, additionalLightData.numRayTracingSamples);
				cmd.SetComputeFloatParam(this.m_ScreenSpaceShadowsCS, HDShaderIDs._RaytracingLightRadius, additionalLightData.shapeRadius);
				int num5 = this.RayTracingFrameIndex(hdCamera);
				cmd.SetComputeIntParam(this.m_ScreenSpaceShadowsCS, HDShaderIDs._RaytracingFrameIndex, num5);
				if (lightData.lightType == GPULightType.Spot)
				{
					float num6 = additionalLightData.legacyLight.spotAngle * 3.1415927f / 180f;
					cmd.SetComputeFloatParam(this.m_ScreenSpaceShadowsCS, HDShaderIDs._RaytracingSpotAngle, num6);
				}
				cmd.SetComputeTextureParam(this.m_ScreenSpaceShadowsCS, num4, HDShaderIDs._DepthTexture, this.m_SharedRTManager.GetDepthStencilBuffer(false));
				cmd.SetComputeTextureParam(this.m_ScreenSpaceShadowsCS, num4, HDShaderIDs._NormalBufferTexture, this.m_SharedRTManager.GetNormalBuffer(false));
				cmd.SetComputeTextureParam(this.m_ScreenSpaceShadowsCS, num4, HDShaderIDs._RaytracingDirectionBuffer, rayTracingBuffer3);
				cmd.SetComputeTextureParam(this.m_ScreenSpaceShadowsCS, num4, HDShaderIDs._RaytracingDistanceBuffer, rayTracingBuffer4);
				cmd.DispatchCompute(this.m_ScreenSpaceShadowsCS, num4, num2, num3, hdCamera.viewCount);
				cmd.SetRayTracingShaderPass(this.m_ScreenSpaceShadowsRT, "VisibilityDXR");
				RayCountManager rayCountManager = this.GetRayCountManager();
				cmd.SetRayTracingIntParam(this.m_ScreenSpaceShadowsRT, HDShaderIDs._RayCountEnabled, rayCountManager.RayCountIsEnabled());
				cmd.SetRayTracingTextureParam(this.m_ScreenSpaceShadowsRT, HDShaderIDs._RayCountTexture, rayCountManager.GetRayCountTexture());
				cmd.SetRayTracingTextureParam(this.m_ScreenSpaceShadowsRT, HDShaderIDs._DepthTexture, this.m_SharedRTManager.GetDepthStencilBuffer(false));
				cmd.SetRayTracingTextureParam(this.m_ScreenSpaceShadowsRT, HDShaderIDs._NormalBufferTexture, this.m_SharedRTManager.GetNormalBuffer(false));
				cmd.SetRayTracingTextureParam(this.m_ScreenSpaceShadowsRT, HDShaderIDs._RaytracingDirectionBuffer, rayTracingBuffer3);
				cmd.SetRayTracingTextureParam(this.m_ScreenSpaceShadowsRT, HDShaderIDs._RaytracingDistanceBuffer, rayTracingBuffer4);
				cmd.SetRayTracingIntParam(this.m_ScreenSpaceShadowsRT, HDShaderIDs._RaytracingNumSamples, additionalLightData.numRayTracingSamples);
				cmd.SetRayTracingTextureParam(this.m_ScreenSpaceShadowsRT, HDShaderIDs._RaytracedShadowIntegration, rayTracingBuffer);
				cmd.SetRayTracingTextureParam(this.m_ScreenSpaceShadowsRT, HDShaderIDs._VelocityBuffer, rayTracingBuffer5);
				CoreUtils.SetKeyword(cmd, "TRANSPARENT_COLOR_SHADOW", additionalLightData.semiTransparentShadow);
				cmd.DispatchRays(this.m_ScreenSpaceShadowsRT, additionalLightData.semiTransparentShadow ? "RayGenSemiTransparentShadowSegmentSingle" : "RayGenShadowSegmentSingle", (uint)hdCamera.actualWidth, (uint)hdCamera.actualHeight, (uint)hdCamera.viewCount, null);
				CoreUtils.SetKeyword(cmd, "TRANSPARENT_COLOR_SHADOW", false);
			}
			HDRenderPipeline.GetShadowChannelMask(lightData.screenSpaceShadowIndex, HDRenderPipeline.ScreenSpaceShadowType.GrayScale, ref this.m_ShadowChannelMask0);
			if (additionalLightData.filterTracedShadow)
			{
				float num7 = 1f;
				if (additionalLightData.previousTransform != additionalLightData.transform.localToWorldMatrix || !hdCamera.ValidShadowHistory(additionalLightData, lightData.screenSpaceShadowIndex, lightData.lightType))
				{
					num7 = 0f;
				}
				num7 *= (this.ValidRayTracingHistory(hdCamera) ? 1f : 0f);
				this.GetTemporalFilter().DenoiseBuffer(cmd, hdCamera, rayTracingBuffer, shadowHistoryArray, shadowHistoryValidityArray, rayTracingBuffer5, rayTracingBuffer2, lightData.screenSpaceShadowIndex / 4, this.m_ShadowChannelMask0, true, num7);
				this.GetSimpleDenoiser().DenoiseBufferNoHistory(cmd, hdCamera, rayTracingBuffer2, rayTracingBuffer, additionalLightData.filterSizeTraced, true);
				hdCamera.PropagateShadowHistory(additionalLightData, lightData.screenSpaceShadowIndex, lightData.lightType);
			}
			this.WriteToScreenSpaceShadowBuffer(cmd, hdCamera, rayTracingBuffer, lightData.screenSpaceShadowIndex, HDRenderPipeline.ScreenSpaceShadowType.GrayScale);
		}

		// Token: 0x0600033B RID: 827 RVA: 0x0001884C File Offset: 0x00016A4C
		private void EvaluateShadowDebugView(CommandBuffer cmd, HDCamera hdCamera)
		{
			ComputeShader shadowFilterCS = this.m_Asset.renderPipelineRayTracingResources.shadowFilterCS;
			HDRenderPipeline hdrenderPipeline = RenderPipelineManager.currentPipeline as HDRenderPipeline;
			if (FullScreenDebugMode.ScreenSpaceShadows == hdrenderPipeline.m_CurrentDebugDisplaySettings.data.fullScreenDebugMode)
			{
				int actualWidth = hdCamera.actualWidth;
				int actualHeight = hdCamera.actualHeight;
				int num = 8;
				int num2 = (actualWidth + (num - 1)) / num;
				int num3 = (actualHeight + (num - 1)) / num;
				RTHandle rayTracingBuffer = this.GetRayTracingBuffer(InternalRayTracingBuffers.RGBA0);
				CoreUtils.SetRenderTarget(cmd, rayTracingBuffer, ClearFlag.Color, 0, CubemapFace.Unknown, -1);
				if ((long)this.m_ScreenSpaceShadowChannelSlot > (long)((ulong)hdrenderPipeline.m_CurrentDebugDisplaySettings.data.screenSpaceShadowIndex))
				{
					int num4 = shadowFilterCS.FindKernel("WriteShadowTextureDebug");
					cmd.SetComputeIntParam(shadowFilterCS, HDShaderIDs._DenoisingHistorySlot, (int)hdrenderPipeline.m_CurrentDebugDisplaySettings.data.screenSpaceShadowIndex);
					cmd.SetComputeTextureParam(shadowFilterCS, num4, HDShaderIDs._ScreenSpaceShadowsTextureRW, this.m_ScreenSpaceShadowTextureArray);
					cmd.SetComputeTextureParam(shadowFilterCS, num4, HDShaderIDs._DenoiseOutputTextureRW, rayTracingBuffer);
					cmd.DispatchCompute(shadowFilterCS, num4, num2, num3, hdCamera.viewCount);
				}
				hdrenderPipeline.PushFullScreenDebugTexture(hdCamera, cmd, rayTracingBuffer, FullScreenDebugMode.ScreenSpaceShadows);
			}
		}

		// Token: 0x0600033C RID: 828 RVA: 0x00018950 File Offset: 0x00016B50
		private void InitializeVolumetricLighting()
		{
			this.m_SupportVolumetrics = this.asset.currentPlatformRenderPipelineSettings.supportVolumetrics;
			if (!this.m_SupportVolumetrics)
			{
				return;
			}
			this.volumetricLightingPreset = (this.asset.currentPlatformRenderPipelineSettings.increaseResolutionOfVolumetrics ? VolumetricLightingPreset.High : VolumetricLightingPreset.Medium);
			this.m_VolumeVoxelizationCS = this.defaultResources.shaders.volumeVoxelizationCS;
			this.m_VolumetricLightingCS = this.defaultResources.shaders.volumetricLightingCS;
			this.m_PackedCoeffs = new Vector4[7];
			this.m_PhaseZH = default(ZonalHarmonicsL2);
			this.m_PhaseZH.coeffs = new float[3];
			this.m_xySeq = new Vector2[7];
			this.m_PixelCoordToViewDirWS = new Matrix4x4[ShaderConfig.s_XrMaxViews];
			this.CreateVolumetricLightingBuffers();
		}

		// Token: 0x0600033D RID: 829 RVA: 0x00018A10 File Offset: 0x00016C10
		private Vector2Int ComputeVBufferResolutionXY(Vector2Int screenSize)
		{
			Vector3Int vector3Int = HDRenderPipeline.ComputeVBufferResolution(this.volumetricLightingPreset, screenSize.x, screenSize.y);
			return new Vector2Int(vector3Int.x, vector3Int.y);
		}

		// Token: 0x0600033E RID: 830 RVA: 0x00018A4C File Offset: 0x00016C4C
		private void CreateVolumetricLightingBuffers()
		{
			this.m_VisibleVolumeBounds = new List<OrientedBBox>();
			this.m_VisibleVolumeData = new List<DensityVolumeEngineData>();
			this.m_VisibleVolumeBoundsBuffer = new ComputeBuffer(512, Marshal.SizeOf(typeof(OrientedBBox)));
			this.m_VisibleVolumeDataBuffer = new ComputeBuffer(512, Marshal.SizeOf(typeof(DensityVolumeEngineData)));
			int num = HDRenderPipeline.ComputeVBufferSliceCount(this.volumetricLightingPreset);
			this.m_DensityBufferHandle = RTHandles.Alloc(new ScaleFunc(this.ComputeVBufferResolutionXY), num, DepthBits.None, GraphicsFormat.R16G16B16A16_SFloat, FilterMode.Point, TextureWrapMode.Repeat, TextureDimension.Tex3D, true, false, true, false, 1, 0f, false, false, false, RenderTextureMemoryless.None, "VBufferDensity");
			this.m_LightingBufferHandle = RTHandles.Alloc(new ScaleFunc(this.ComputeVBufferResolutionXY), num, DepthBits.None, GraphicsFormat.R16G16B16A16_SFloat, FilterMode.Point, TextureWrapMode.Repeat, TextureDimension.Tex3D, true, false, true, false, 1, 0f, false, false, false, RenderTextureMemoryless.None, "VBufferIntegral");
		}

		// Token: 0x0600033F RID: 831 RVA: 0x00018B1C File Offset: 0x00016D1C
		private VBufferParameters ComputeVBufferParameters(HDCamera hdCamera)
		{
			Vector3Int vector3Int = HDRenderPipeline.ComputeVBufferResolution(this.volumetricLightingPreset, hdCamera.actualWidth, hdCamera.actualHeight);
			Fog component = hdCamera.volumeStack.GetComponent<Fog>();
			return new VBufferParameters(vector3Int, component.depthExtent.value, hdCamera.camera.nearClipPlane, hdCamera.camera.farClipPlane, hdCamera.camera.fieldOfView, component.sliceDistributionUniformity.value);
		}

		// Token: 0x06000340 RID: 832 RVA: 0x00018B88 File Offset: 0x00016D88
		internal void ReinitializeVolumetricBufferParams(HDCamera hdCamera)
		{
			bool flag = Fog.IsVolumetricFogEnabled(hdCamera);
			bool flag2 = hdCamera.vBufferParams != null;
			if (flag ^ flag2)
			{
				if (flag2)
				{
					hdCamera.vBufferParams = null;
					return;
				}
				VBufferParameters vbufferParameters = this.ComputeVBufferParameters(hdCamera);
				hdCamera.vBufferParams = new VBufferParameters[2];
				hdCamera.vBufferParams[0] = vbufferParameters;
				hdCamera.vBufferParams[1] = vbufferParameters;
			}
		}

		// Token: 0x06000341 RID: 833 RVA: 0x00018BE4 File Offset: 0x00016DE4
		internal void UpdateVolumetricBufferParams(HDCamera hdCamera)
		{
			if (!Fog.IsVolumetricFogEnabled(hdCamera))
			{
				return;
			}
			VBufferParameters vbufferParameters = this.ComputeVBufferParameters(hdCamera);
			if ((float)hdCamera.vBufferParams[0].viewportSize.x == 0f && (float)hdCamera.vBufferParams[0].viewportSize.y == 0f)
			{
				hdCamera.vBufferParams[1] = vbufferParameters;
			}
			else
			{
				hdCamera.vBufferParams[1] = hdCamera.vBufferParams[0];
			}
			hdCamera.vBufferParams[0] = vbufferParameters;
		}

		// Token: 0x06000342 RID: 834 RVA: 0x00018C72 File Offset: 0x00016E72
		internal void AllocateVolumetricHistoryBuffers(HDCamera hdCamera, int bufferCount)
		{
			hdCamera.AllocHistoryFrameRT(1, new Func<string, int, RTHandleSystem, RTHandle>(this.<AllocateVolumetricHistoryBuffers>g__HistoryBufferAllocatorFunction|294_0), bufferCount);
		}

		// Token: 0x06000343 RID: 835 RVA: 0x00018C8C File Offset: 0x00016E8C
		private void DestroyVolumetricLightingBuffers()
		{
			if (this.m_DensityBufferHandle != null)
			{
				RTHandles.Release(this.m_DensityBufferHandle);
			}
			if (this.m_LightingBufferHandle != null)
			{
				RTHandles.Release(this.m_LightingBufferHandle);
			}
			CoreUtils.SafeRelease(this.m_VisibleVolumeBoundsBuffer);
			CoreUtils.SafeRelease(this.m_VisibleVolumeDataBuffer);
			this.m_VisibleVolumeBounds = null;
			this.m_VisibleVolumeData = null;
		}

		// Token: 0x06000344 RID: 836 RVA: 0x00018CE3 File Offset: 0x00016EE3
		private void CleanupVolumetricLighting()
		{
			this.DestroyVolumetricLightingBuffers();
			this.m_VolumeVoxelizationCS = null;
			this.m_VolumetricLightingCS = null;
		}

		// Token: 0x06000345 RID: 837 RVA: 0x00018CF9 File Offset: 0x00016EF9
		private static int ComputeVBufferTileSize(VolumetricLightingPreset preset)
		{
			switch (preset)
			{
			case VolumetricLightingPreset.Off:
				return 0;
			case VolumetricLightingPreset.Medium:
				return 8;
			case VolumetricLightingPreset.High:
				return 4;
			default:
				return 0;
			}
		}

		// Token: 0x06000346 RID: 838 RVA: 0x00018D18 File Offset: 0x00016F18
		private static int ComputeVBufferSliceCount(VolumetricLightingPreset preset)
		{
			int num;
			switch (preset)
			{
			case VolumetricLightingPreset.Off:
				num = 0;
				break;
			case VolumetricLightingPreset.Medium:
				num = 64;
				break;
			case VolumetricLightingPreset.High:
				num = 128;
				break;
			default:
				num = 0;
				break;
			}
			return num;
		}

		// Token: 0x06000347 RID: 839 RVA: 0x00018D50 File Offset: 0x00016F50
		private static Vector3Int ComputeVBufferResolution(VolumetricLightingPreset preset, int screenWidth, int screenHeight)
		{
			int num = HDRenderPipeline.ComputeVBufferTileSize(preset);
			int num2 = HDUtils.DivRoundUp(screenWidth, num);
			int num3 = HDUtils.DivRoundUp(screenHeight, num);
			int num4 = HDRenderPipeline.ComputeVBufferSliceCount(preset);
			return new Vector3Int(num2, num3, num4);
		}

		// Token: 0x06000348 RID: 840 RVA: 0x00018D84 File Offset: 0x00016F84
		private void SetPreconvolvedAmbientLightProbe(HDCamera hdCamera, CommandBuffer cmd, float dimmer, float anisotropy)
		{
			SphericalHarmonicsL2 sphericalHarmonicsL = SphericalHarmonicMath.RescaleCoefficients(SphericalHarmonicMath.UndoCosineRescaling(this.m_SkyManager.GetAmbientProbe(hdCamera)), dimmer);
			ZonalHarmonicsL2.GetCornetteShanksPhaseFunction(this.m_PhaseZH, anisotropy);
			SphericalHarmonicsL2 sphericalHarmonicsL2 = SphericalHarmonicMath.PremultiplyCoefficients(SphericalHarmonicMath.Convolve(sphericalHarmonicsL, this.m_PhaseZH));
			SphericalHarmonicMath.PackCoefficients(this.m_PackedCoeffs, sphericalHarmonicsL2);
			cmd.SetGlobalVectorArray(HDShaderIDs._AmbientProbeCoeffs, this.m_PackedCoeffs);
		}

		// Token: 0x06000349 RID: 841 RVA: 0x00018DE4 File Offset: 0x00016FE4
		private static float CornetteShanksPhasePartConstant(float anisotropy)
		{
			return 0.119366206f * (1f - anisotropy * anisotropy) / (2f + anisotropy * anisotropy);
		}

		// Token: 0x0600034A RID: 842 RVA: 0x00018E0C File Offset: 0x0001700C
		private void PushVolumetricLightingGlobalParams(HDCamera hdCamera, CommandBuffer cmd, int frameIndex)
		{
			if (!Fog.IsVolumetricFogEnabled(hdCamera))
			{
				cmd.SetGlobalTexture(HDShaderIDs._VBufferLighting, HDUtils.clearTexture3D);
				return;
			}
			Fog component = hdCamera.volumeStack.GetComponent<Fog>();
			this.SetPreconvolvedAmbientLightProbe(hdCamera, cmd, component.globalLightProbeDimmer.value, component.anisotropy.value);
			VBufferParameters vbufferParameters = hdCamera.vBufferParams[0];
			VBufferParameters vbufferParameters2 = hdCamera.vBufferParams[1];
			Vector2Int vector2Int = new Vector2Int(this.m_LightingBufferHandle.rt.width, this.m_LightingBufferHandle.rt.height);
			Vector2Int vector2Int2 = Vector2Int.zero;
			if (hdCamera.IsVolumetricReprojectionEnabled())
			{
				RTHandle previousFrameRT = hdCamera.GetPreviousFrameRT(1);
				vector2Int2 = new Vector2Int(previousFrameRT.rt.width, previousFrameRT.rt.height);
				if ((float)vector2Int2.x == 0f && (float)vector2Int2.y == 0f)
				{
					vector2Int2 = vector2Int;
				}
			}
			Vector3Int viewportSize = vbufferParameters.viewportSize;
			Vector3Int viewportSize2 = vbufferParameters2.viewportSize;
			int num = viewportSize.z / hdCamera.viewCount;
			cmd.SetGlobalVector(HDShaderIDs._VBufferViewportSize, new Vector4((float)viewportSize.x, (float)viewportSize.y, 1f / (float)viewportSize.x, 1f / (float)viewportSize.y));
			cmd.SetGlobalInt(HDShaderIDs._VBufferSliceCount, num);
			cmd.SetGlobalFloat(HDShaderIDs._VBufferRcpSliceCount, 1f / (float)num);
			cmd.SetGlobalVector(HDShaderIDs._VBufferSharedUvScaleAndLimit, vbufferParameters.ComputeUvScaleAndLimit(vector2Int));
			cmd.SetGlobalVector(HDShaderIDs._VBufferDistanceEncodingParams, vbufferParameters.depthEncodingParams);
			cmd.SetGlobalVector(HDShaderIDs._VBufferDistanceDecodingParams, vbufferParameters.depthDecodingParams);
			cmd.SetGlobalFloat(HDShaderIDs._VBufferLastSliceDist, vbufferParameters.ComputeLastSliceDistance(num));
			cmd.SetGlobalFloat(HDShaderIDs._VBufferRcpInstancedViewCount, 1f / (float)hdCamera.viewCount);
			cmd.SetGlobalVector(HDShaderIDs._VBufferPrevViewportSize, new Vector4((float)viewportSize2.x, (float)viewportSize2.y, 1f / (float)viewportSize2.x, 1f / (float)viewportSize2.y));
			cmd.SetGlobalVector(HDShaderIDs._VBufferHistoryPrevUvScaleAndLimit, vbufferParameters2.ComputeUvScaleAndLimit(vector2Int2));
			cmd.SetGlobalVector(HDShaderIDs._VBufferPrevDepthEncodingParams, vbufferParameters2.depthEncodingParams);
			cmd.SetGlobalVector(HDShaderIDs._VBufferPrevDepthDecodingParams, vbufferParameters2.depthDecodingParams);
			cmd.SetGlobalTexture(HDShaderIDs._VBufferLighting, this.m_LightingBufferHandle);
		}

		// Token: 0x0600034B RID: 843 RVA: 0x00019060 File Offset: 0x00017260
		private DensityVolumeList PrepareVisibleDensityVolumeList(HDCamera hdCamera, CommandBuffer cmd, float time)
		{
			DensityVolumeList densityVolumeList = default(DensityVolumeList);
			if (!Fog.IsVolumetricFogEnabled(hdCamera))
			{
				return densityVolumeList;
			}
			DensityVolumeList densityVolumeList2;
			using (new ProfilingScope(cmd, ProfilingSampler.Get<HDProfileId>(HDProfileId.PrepareVisibleDensityVolumeList)))
			{
				Vector3 position = hdCamera.camera.transform.position;
				Vector3 vector = Vector3.zero;
				if (ShaderConfig.s_CameraRelativeRendering != 0)
				{
					vector = position;
				}
				this.m_VisibleVolumeBounds.Clear();
				this.m_VisibleVolumeData.Clear();
				List<DensityVolume> list = DensityVolumeManager.manager.PrepareDensityVolumeData(cmd, hdCamera, time);
				for (int i = 0; i < Math.Min(list.Count, 512); i++)
				{
					DensityVolume densityVolume = list[i];
					OrientedBBox orientedBBox = new OrientedBBox(Matrix4x4.TRS(densityVolume.transform.position, densityVolume.transform.rotation, densityVolume.parameters.size));
					orientedBBox.center -= vector;
					if (GeometryUtils.Overlap(orientedBBox, hdCamera.frustum, 6, 8))
					{
						DensityVolumeEngineData densityVolumeEngineData = densityVolume.parameters.ConvertToEngineData();
						this.m_VisibleVolumeBounds.Add(orientedBBox);
						this.m_VisibleVolumeData.Add(densityVolumeEngineData);
					}
				}
				this.m_VisibleVolumeBoundsBuffer.SetData<OrientedBBox>(this.m_VisibleVolumeBounds);
				this.m_VisibleVolumeDataBuffer.SetData<DensityVolumeEngineData>(this.m_VisibleVolumeData);
				densityVolumeList.bounds = this.m_VisibleVolumeBounds;
				densityVolumeList.density = this.m_VisibleVolumeData;
				densityVolumeList2 = densityVolumeList;
			}
			return densityVolumeList2;
		}

		// Token: 0x0600034C RID: 844 RVA: 0x000191F4 File Offset: 0x000173F4
		private HDRenderPipeline.VolumeVoxelizationParameters PrepareVolumeVoxelizationParameters(HDCamera hdCamera)
		{
			HDRenderPipeline.VolumeVoxelizationParameters volumeVoxelizationParameters = default(HDRenderPipeline.VolumeVoxelizationParameters);
			volumeVoxelizationParameters.viewCount = hdCamera.viewCount;
			volumeVoxelizationParameters.numBigTileX = HDRenderPipeline.GetNumTileBigTileX(hdCamera);
			volumeVoxelizationParameters.numBigTileY = HDRenderPipeline.GetNumTileBigTileY(hdCamera);
			volumeVoxelizationParameters.tiledLighting = this.HasLightToCull() && hdCamera.frameSettings.IsEnabled(FrameSettingsField.BigTilePrepass);
			bool flag = this.volumetricLightingPreset == VolumetricLightingPreset.High;
			volumeVoxelizationParameters.voxelizationCS = this.m_VolumeVoxelizationCS;
			volumeVoxelizationParameters.voxelizationKernel = (volumeVoxelizationParameters.tiledLighting ? 1 : 0) | (flag ? 2 : 0);
			Vector3Int viewportSize = hdCamera.vBufferParams[0].viewportSize;
			volumeVoxelizationParameters.resolution = new Vector4((float)viewportSize.x, (float)viewportSize.y, 1f / (float)viewportSize.x, 1f / (float)viewportSize.y);
			float num = hdCamera.camera.GetGateFittedFieldOfView() * 0.017453292f;
			float num2 = HDUtils.ProjectionMatrixAspect(in hdCamera.mainViewConstants.projMatrix);
			hdCamera.GetPixelCoordToViewDirWS(volumeVoxelizationParameters.resolution, num2, ref this.m_PixelCoordToViewDirWS);
			volumeVoxelizationParameters.pixelCoordToViewDirWS = this.m_PixelCoordToViewDirWS;
			volumeVoxelizationParameters.unitDepthTexelSpacing = HDUtils.ComputZPlaneTexelSpacing(1f, num, volumeVoxelizationParameters.resolution.y);
			volumeVoxelizationParameters.numVisibleVolumes = this.m_VisibleVolumeBounds.Count;
			volumeVoxelizationParameters.volumeAtlas = DensityVolumeManager.manager.volumeAtlas.GetAtlas();
			volumeVoxelizationParameters.volumeAtlasDimensions = new Vector4(0f, 0f, 0f, 0f);
			if (volumeVoxelizationParameters.volumeAtlas != null)
			{
				volumeVoxelizationParameters.volumeAtlasDimensions.x = (float)volumeVoxelizationParameters.volumeAtlas.width / (float)volumeVoxelizationParameters.volumeAtlas.depth;
				volumeVoxelizationParameters.volumeAtlasDimensions.y = (float)volumeVoxelizationParameters.volumeAtlas.width;
				volumeVoxelizationParameters.volumeAtlasDimensions.z = (float)volumeVoxelizationParameters.volumeAtlas.depth;
				volumeVoxelizationParameters.volumeAtlasDimensions.w = Mathf.Log((float)volumeVoxelizationParameters.volumeAtlas.width, 2f);
			}
			else
			{
				volumeVoxelizationParameters.volumeAtlas = CoreUtils.blackVolumeTexture;
			}
			return volumeVoxelizationParameters;
		}

		// Token: 0x0600034D RID: 845 RVA: 0x00019410 File Offset: 0x00017610
		private static void VolumeVoxelizationPass(in HDRenderPipeline.VolumeVoxelizationParameters parameters, RTHandle densityBuffer, ComputeBuffer visibleVolumeBoundsBuffer, ComputeBuffer visibleVolumeDataBuffer, ComputeBuffer bigTileLightList, CommandBuffer cmd)
		{
			cmd.SetComputeIntParam(parameters.voxelizationCS, HDShaderIDs._NumTileBigTileX, parameters.numBigTileX);
			cmd.SetComputeIntParam(parameters.voxelizationCS, HDShaderIDs._NumTileBigTileY, parameters.numBigTileY);
			if (parameters.tiledLighting)
			{
				cmd.SetComputeBufferParam(parameters.voxelizationCS, parameters.voxelizationKernel, HDShaderIDs.g_vBigTileLightList, bigTileLightList);
			}
			cmd.SetComputeTextureParam(parameters.voxelizationCS, parameters.voxelizationKernel, HDShaderIDs._VBufferDensity, densityBuffer);
			cmd.SetComputeBufferParam(parameters.voxelizationCS, parameters.voxelizationKernel, HDShaderIDs._VolumeBounds, visibleVolumeBoundsBuffer);
			cmd.SetComputeBufferParam(parameters.voxelizationCS, parameters.voxelizationKernel, HDShaderIDs._VolumeData, visibleVolumeDataBuffer);
			cmd.SetComputeTextureParam(parameters.voxelizationCS, parameters.voxelizationKernel, HDShaderIDs._VolumeMaskAtlas, parameters.volumeAtlas);
			cmd.SetComputeMatrixArrayParam(parameters.voxelizationCS, HDShaderIDs._VBufferCoordToViewDirWS, parameters.pixelCoordToViewDirWS);
			cmd.SetComputeFloatParam(parameters.voxelizationCS, HDShaderIDs._VBufferUnitDepthTexelSpacing, parameters.unitDepthTexelSpacing);
			cmd.SetComputeIntParam(parameters.voxelizationCS, HDShaderIDs._NumVisibleDensityVolumes, parameters.numVisibleVolumes);
			cmd.SetComputeVectorParam(parameters.voxelizationCS, HDShaderIDs._VolumeMaskDimensions, parameters.volumeAtlasDimensions);
			cmd.DispatchCompute(parameters.voxelizationCS, parameters.voxelizationKernel, ((int)parameters.resolution.x + 7) / 8, ((int)parameters.resolution.y + 7) / 8, parameters.viewCount);
		}

		// Token: 0x0600034E RID: 846 RVA: 0x0001957C File Offset: 0x0001777C
		private void VolumeVoxelizationPass(HDCamera hdCamera, CommandBuffer cmd)
		{
			if (!Fog.IsVolumetricFogEnabled(hdCamera))
			{
				return;
			}
			using (new ProfilingScope(cmd, ProfilingSampler.Get<HDProfileId>(HDProfileId.VolumeVoxelization)))
			{
				HDRenderPipeline.VolumeVoxelizationParameters volumeVoxelizationParameters = this.PrepareVolumeVoxelizationParameters(hdCamera);
				HDRenderPipeline.VolumeVoxelizationPass(in volumeVoxelizationParameters, this.m_DensityBufferHandle, this.m_VisibleVolumeBoundsBuffer, this.m_VisibleVolumeDataBuffer, this.m_TileAndClusterData.bigTileLightList, cmd);
			}
		}

		// Token: 0x0600034F RID: 847 RVA: 0x000195F0 File Offset: 0x000177F0
		private static void GetHexagonalClosePackedSpheres7(Vector2[] coords)
		{
			float num = 0.17054069f;
			float num2 = 2f * num;
			float num3 = num * Mathf.Sqrt(3f);
			coords[0] = new Vector2(0f, 0f);
			coords[1] = new Vector2(-num2, 0f);
			coords[2] = new Vector2(num2, 0f);
			coords[3] = new Vector2(-num, -num3);
			coords[4] = new Vector2(num, num3);
			coords[5] = new Vector2(num, -num3);
			coords[6] = new Vector2(-num, num3);
			for (int i = 0; i < 7; i++)
			{
				Vector2 vector = coords[i];
				coords[i].x = vector.x * 0.9659258f - vector.y * 0.25881904f;
				coords[i].y = vector.x * 0.25881904f + vector.y * 0.9659258f;
			}
		}

		// Token: 0x06000350 RID: 848 RVA: 0x000196F4 File Offset: 0x000178F4
		private HDRenderPipeline.VolumetricLightingParameters PrepareVolumetricLightingParameters(HDCamera hdCamera, int frameIndex)
		{
			HDRenderPipeline.VolumetricLightingParameters volumetricLightingParameters = default(HDRenderPipeline.VolumetricLightingParameters);
			Fog component = hdCamera.volumeStack.GetComponent<Fog>();
			volumetricLightingParameters.tiledLighting = hdCamera.frameSettings.IsEnabled(FrameSettingsField.BigTilePrepass);
			volumetricLightingParameters.enableReprojection = hdCamera.IsVolumetricReprojectionEnabled();
			bool flag = component.anisotropy.value != 0f;
			bool flag2 = this.volumetricLightingPreset == VolumetricLightingPreset.High;
			volumetricLightingParameters.volumetricLightingCS = this.m_VolumetricLightingCS;
			volumetricLightingParameters.volumetricLightingKernel = (volumetricLightingParameters.tiledLighting ? 1 : 0) | (volumetricLightingParameters.enableReprojection ? 2 : 0) | (flag ? 4 : 0) | (flag2 ? 8 : 0);
			volumetricLightingParameters.volumetricFilteringKernelX = 16;
			volumetricLightingParameters.volumetricFilteringKernelY = 17;
			Vector3Int viewportSize = hdCamera.vBufferParams[0].viewportSize;
			volumetricLightingParameters.resolution = new Vector4((float)viewportSize.x, (float)viewportSize.y, 1f / (float)viewportSize.x, 1f / (float)viewportSize.y);
			float num = hdCamera.camera.GetGateFittedFieldOfView() * 0.017453292f;
			float num2 = HDUtils.ProjectionMatrixAspect(in hdCamera.mainViewConstants.projMatrix);
			hdCamera.GetPixelCoordToViewDirWS(volumetricLightingParameters.resolution, num2, ref this.m_PixelCoordToViewDirWS);
			volumetricLightingParameters.pixelCoordToViewDirWS = this.m_PixelCoordToViewDirWS;
			volumetricLightingParameters.unitDepthTexelSpacing = HDUtils.ComputZPlaneTexelSpacing(1f, num, volumetricLightingParameters.resolution.y);
			volumetricLightingParameters.anisotropy = component.anisotropy.value;
			volumetricLightingParameters.historyIsValid = hdCamera.volumetricHistoryIsValid;
			volumetricLightingParameters.viewCount = hdCamera.viewCount;
			volumetricLightingParameters.numBigTileX = HDRenderPipeline.GetNumTileBigTileX(hdCamera);
			volumetricLightingParameters.numBigTileY = HDRenderPipeline.GetNumTileBigTileY(hdCamera);
			volumetricLightingParameters.filterVolume = component.filter.value;
			HDRenderPipeline.GetHexagonalClosePackedSpheres7(this.m_xySeq);
			int num3 = frameIndex % 7;
			volumetricLightingParameters.xySeqOffset.Set(this.m_xySeq[num3].x, this.m_xySeq[num3].y, this.m_zSeq[num3], (float)frameIndex);
			return volumetricLightingParameters;
		}

		// Token: 0x06000351 RID: 849 RVA: 0x000198FC File Offset: 0x00017AFC
		private static void VolumetricLightingPass(in HDRenderPipeline.VolumetricLightingParameters parameters, RTHandle densityBuffer, RTHandle lightingBuffer, RTHandle historyRT, RTHandle feedbackRT, ComputeBuffer bigTileLightList, CommandBuffer cmd)
		{
			cmd.SetComputeIntParam(parameters.volumetricLightingCS, HDShaderIDs._NumTileBigTileX, parameters.numBigTileX);
			cmd.SetComputeIntParam(parameters.volumetricLightingCS, HDShaderIDs._NumTileBigTileY, parameters.numBigTileY);
			if (parameters.tiledLighting)
			{
				cmd.SetComputeBufferParam(parameters.volumetricLightingCS, parameters.volumetricLightingKernel, HDShaderIDs.g_vBigTileLightList, bigTileLightList);
			}
			cmd.SetComputeMatrixArrayParam(parameters.volumetricLightingCS, HDShaderIDs._VBufferCoordToViewDirWS, parameters.pixelCoordToViewDirWS);
			cmd.SetComputeFloatParam(parameters.volumetricLightingCS, HDShaderIDs._VBufferUnitDepthTexelSpacing, parameters.unitDepthTexelSpacing);
			cmd.SetComputeFloatParam(parameters.volumetricLightingCS, HDShaderIDs._CornetteShanksConstant, HDRenderPipeline.CornetteShanksPhasePartConstant(parameters.anisotropy));
			cmd.SetComputeVectorParam(parameters.volumetricLightingCS, HDShaderIDs._VBufferSampleOffset, parameters.xySeqOffset);
			cmd.SetComputeTextureParam(parameters.volumetricLightingCS, parameters.volumetricLightingKernel, HDShaderIDs._VBufferDensity, densityBuffer);
			cmd.SetComputeTextureParam(parameters.volumetricLightingCS, parameters.volumetricLightingKernel, HDShaderIDs._VBufferLightingIntegral, lightingBuffer);
			cmd.SetComputeIntParam(parameters.volumetricLightingCS, HDShaderIDs._VBufferLightingHistoryIsValid, parameters.historyIsValid ? 1 : 0);
			if (parameters.enableReprojection)
			{
				cmd.SetComputeTextureParam(parameters.volumetricLightingCS, parameters.volumetricLightingKernel, HDShaderIDs._VBufferLightingHistory, historyRT);
				cmd.SetComputeTextureParam(parameters.volumetricLightingCS, parameters.volumetricLightingKernel, HDShaderIDs._VBufferLightingFeedback, feedbackRT);
			}
			cmd.DispatchCompute(parameters.volumetricLightingCS, parameters.volumetricLightingKernel, ((int)parameters.resolution.x + 7) / 8, ((int)parameters.resolution.y + 7) / 8, parameters.viewCount);
		}

		// Token: 0x06000352 RID: 850 RVA: 0x00019A98 File Offset: 0x00017C98
		private static void FilterVolumetricLighting(in HDRenderPipeline.VolumetricLightingParameters parameters, RTHandle outputBuffer, RTHandle inputBuffer, CommandBuffer cmd)
		{
			using (new ProfilingScope(cmd, ProfilingSampler.Get<HDProfileId>(HDProfileId.VolumetricLightingFiltering)))
			{
				cmd.SetComputeTextureParam(parameters.volumetricLightingCS, parameters.volumetricFilteringKernelX, HDShaderIDs._VBufferLightingFeedback, inputBuffer);
				cmd.SetComputeTextureParam(parameters.volumetricLightingCS, parameters.volumetricFilteringKernelX, HDShaderIDs._VBufferLightingIntegral, outputBuffer);
				cmd.DispatchCompute(parameters.volumetricLightingCS, parameters.volumetricFilteringKernelX, ((int)parameters.resolution.x + 7) / 8, ((int)parameters.resolution.y + 7) / 8, parameters.viewCount);
				cmd.SetComputeTextureParam(parameters.volumetricLightingCS, parameters.volumetricFilteringKernelY, HDShaderIDs._VBufferLightingFeedback, outputBuffer);
				cmd.SetComputeTextureParam(parameters.volumetricLightingCS, parameters.volumetricFilteringKernelY, HDShaderIDs._VBufferLightingIntegral, inputBuffer);
				cmd.DispatchCompute(parameters.volumetricLightingCS, parameters.volumetricFilteringKernelY, ((int)parameters.resolution.x + 7) / 8, ((int)parameters.resolution.y + 7) / 8, parameters.viewCount);
			}
		}

		// Token: 0x06000353 RID: 851 RVA: 0x00019BB8 File Offset: 0x00017DB8
		private void VolumetricLightingPass(HDCamera hdCamera, CommandBuffer cmd, int frameIndex)
		{
			if (!Fog.IsVolumetricFogEnabled(hdCamera))
			{
				return;
			}
			HDRenderPipeline.VolumetricLightingParameters volumetricLightingParameters = this.PrepareVolumetricLightingParameters(hdCamera, frameIndex);
			using (new ProfilingScope(cmd, ProfilingSampler.Get<HDProfileId>(HDProfileId.VolumetricLighting)))
			{
				RTHandle previousFrameRT = hdCamera.GetPreviousFrameRT(1);
				RTHandle currentFrameRT = hdCamera.GetCurrentFrameRT(1);
				HDRenderPipeline.VolumetricLightingPass(in volumetricLightingParameters, this.m_DensityBufferHandle, this.m_LightingBufferHandle, previousFrameRT, currentFrameRT, this.m_TileAndClusterData.bigTileLightList, cmd);
				if (volumetricLightingParameters.enableReprojection)
				{
					hdCamera.volumetricHistoryIsValid = true;
				}
			}
			if (volumetricLightingParameters.filterVolume)
			{
				HDRenderPipeline.FilterVolumetricLighting(in volumetricLightingParameters, this.m_DensityBufferHandle, this.m_LightingBufferHandle, cmd);
			}
		}

		// Token: 0x06000354 RID: 852 RVA: 0x00019C64 File Offset: 0x00017E64
		private void InitSSSBuffers()
		{
			RenderPipelineSettings currentPlatformRenderPipelineSettings = this.asset.currentPlatformRenderPipelineSettings;
			if (currentPlatformRenderPipelineSettings.supportedLitShaderMode == RenderPipelineSettings.SupportedLitShaderMode.ForwardOnly)
			{
				this.m_SSSColor = RTHandles.Alloc(Vector2.one, TextureXR.slices, DepthBits.None, GraphicsFormat.R8G8B8A8_SRGB, FilterMode.Point, TextureWrapMode.Repeat, TextureXR.dimension, false, false, true, false, 1, 0f, false, false, true, RenderTextureMemoryless.None, "SSSBuffer");
				this.m_SSSReuseGBufferMemory = false;
			}
			if (currentPlatformRenderPipelineSettings.supportMSAA)
			{
				this.m_SSSColorMSAA = RTHandles.Alloc(Vector2.one, TextureXR.slices, DepthBits.None, GraphicsFormat.R8G8B8A8_SRGB, FilterMode.Point, TextureWrapMode.Repeat, TextureXR.dimension, false, false, true, false, 1, 0f, true, true, true, RenderTextureMemoryless.None, "SSSBufferMSAA");
			}
			if ((currentPlatformRenderPipelineSettings.supportedLitShaderMode & RenderPipelineSettings.SupportedLitShaderMode.DeferredOnly) != (RenderPipelineSettings.SupportedLitShaderMode)0)
			{
				this.m_SSSColor = this.m_GbufferManager.GetSubsurfaceScatteringBuffer(0);
				this.m_SSSReuseGBufferMemory = true;
			}
			if (HDRenderPipeline.NeedTemporarySubsurfaceBuffer() || currentPlatformRenderPipelineSettings.supportMSAA)
			{
				this.m_SSSCameraFilteringBuffer = RTHandles.Alloc(Vector2.one, TextureXR.slices, DepthBits.None, GraphicsFormat.B10G11R11_UFloatPack32, FilterMode.Point, TextureWrapMode.Repeat, TextureXR.dimension, true, false, true, false, 1, 0f, false, false, true, RenderTextureMemoryless.None, "SSSCameraFiltering");
			}
			this.m_SSSThicknessRemaps = new Vector4[16];
			this.m_SSSShapeParams = new Vector4[16];
			this.m_SSSTransmissionTintsAndFresnel0 = new Vector4[16];
			this.m_SSSDisabledTransmissionTintsAndFresnel0 = new Vector4[16];
			this.m_SSSWorldScales = new Vector4[16];
			this.m_SSSFilterKernels = new Vector4[880];
			this.m_SSSDiffusionProfileHashes = new float[16];
			this.m_SSSDiffusionProfileUpdate = new int[16];
			this.m_SSSSetDiffusionProfiles = new DiffusionProfileSettings[16];
		}

		// Token: 0x06000355 RID: 853 RVA: 0x00019DD5 File Offset: 0x00017FD5
		private RTHandle GetSSSBuffer()
		{
			return this.m_SSSColor;
		}

		// Token: 0x06000356 RID: 854 RVA: 0x00019DDD File Offset: 0x00017FDD
		private RTHandle GetSSSBufferMSAA()
		{
			return this.m_SSSColorMSAA;
		}

		// Token: 0x06000357 RID: 855 RVA: 0x00019DE8 File Offset: 0x00017FE8
		private void InitializeSubsurfaceScattering()
		{
			string text = (this.asset.currentPlatformRenderPipelineSettings.increaseSssSampleCount ? "SubsurfaceScatteringHQ" : "SubsurfaceScatteringMQ");
			string text2 = (this.asset.currentPlatformRenderPipelineSettings.increaseSssSampleCount ? "SubsurfaceScatteringHQ_MSAA" : "SubsurfaceScatteringMQ_MSAA");
			this.m_SubsurfaceScatteringCS = this.defaultResources.shaders.subsurfaceScatteringCS;
			this.m_SubsurfaceScatteringKernel = this.m_SubsurfaceScatteringCS.FindKernel(text);
			this.m_SubsurfaceScatteringKernelMSAA = this.m_SubsurfaceScatteringCS.FindKernel(text2);
			this.m_CombineLightingPass = CoreUtils.CreateEngineMaterial(this.defaultResources.shaders.combineLightingPS);
			this.m_CombineLightingPass.SetInt(HDShaderIDs._StencilRef, 4);
			this.m_CombineLightingPass.SetInt(HDShaderIDs._StencilMask, 4);
			this.m_SSSCopyStencilForSplitLighting = CoreUtils.CreateEngineMaterial(this.defaultResources.shaders.copyStencilBufferPS);
			this.m_SSSCopyStencilForSplitLighting.SetInt(HDShaderIDs._StencilRef, 4);
			this.m_SSSCopyStencilForSplitLighting.SetInt(HDShaderIDs._StencilMask, 4);
			this.m_SSSDefaultDiffusionProfile = this.defaultResources.assets.defaultDiffusionProfile;
		}

		// Token: 0x06000358 RID: 856 RVA: 0x00019F00 File Offset: 0x00018100
		private void CleanupSubsurfaceScattering()
		{
			CoreUtils.Destroy(this.m_CombineLightingPass);
			CoreUtils.Destroy(this.m_SSSCopyStencilForSplitLighting);
			if (!this.m_SSSReuseGBufferMemory)
			{
				RTHandles.Release(this.m_SSSColor);
			}
			RTHandles.Release(this.m_SSSColorMSAA);
			RTHandles.Release(this.m_SSSCameraFilteringBuffer);
		}

		// Token: 0x06000359 RID: 857 RVA: 0x00019F4C File Offset: 0x0001814C
		private void UpdateCurrentDiffusionProfileSettings(HDCamera hdCamera)
		{
			DiffusionProfileSettings[] array = this.asset.diffusionProfileSettingsList;
			DiffusionProfileOverride component = hdCamera.volumeStack.GetComponent<DiffusionProfileOverride>();
			if (component.active && component.diffusionProfiles.value != null)
			{
				array = component.diffusionProfiles.value;
			}
			this.SetDiffusionProfileAtIndex(this.m_SSSDefaultDiffusionProfile, 0);
			this.m_SSSDiffusionProfileHashes[0] = 0f;
			int num = 1;
			foreach (DiffusionProfileSettings diffusionProfileSettings in array)
			{
				if (!(diffusionProfileSettings == null))
				{
					this.SetDiffusionProfileAtIndex(diffusionProfileSettings, num++);
				}
			}
			this.m_SSSActiveDiffusionProfileCount = num;
		}

		// Token: 0x0600035A RID: 858 RVA: 0x00019FE8 File Offset: 0x000181E8
		private void SetDiffusionProfileAtIndex(DiffusionProfileSettings settings, int index)
		{
			if (this.m_SSSSetDiffusionProfiles[index] == settings && this.m_SSSDiffusionProfileUpdate[index] == settings.updateCount)
			{
				return;
			}
			if (settings.profile.filterKernelNearField == null)
			{
				return;
			}
			this.m_SSSThicknessRemaps[index] = settings.thicknessRemaps;
			this.m_SSSShapeParams[index] = settings.shapeParams;
			this.m_SSSTransmissionTintsAndFresnel0[index] = settings.transmissionTintsAndFresnel0;
			this.m_SSSDisabledTransmissionTintsAndFresnel0[index] = settings.disabledTransmissionTintsAndFresnel0;
			this.m_SSSWorldScales[index] = settings.worldScales;
			int i = 0;
			int num = 55;
			while (i < num)
			{
				this.m_SSSFilterKernels[num * index + i].x = settings.profile.filterKernelNearField[i].x;
				this.m_SSSFilterKernels[num * index + i].y = settings.profile.filterKernelNearField[i].y;
				if (i < 21)
				{
					this.m_SSSFilterKernels[num * index + i].z = settings.profile.filterKernelFarField[i].x;
					this.m_SSSFilterKernels[num * index + i].w = settings.profile.filterKernelFarField[i].y;
				}
				i++;
			}
			this.m_SSSDiffusionProfileHashes[index] = HDShadowUtils.Asfloat(settings.profile.hash);
			uint num2 = 1U << index;
			this.m_SSSTexturingModeFlags &= ~num2;
			this.m_SSSTransmissionFlags &= ~num2;
			this.m_SSSTexturingModeFlags |= (uint)((uint)settings.profile.texturingMode << (index & 31));
			this.m_SSSTransmissionFlags |= (uint)((uint)settings.profile.transmissionMode << (index & 31));
			this.m_SSSSetDiffusionProfiles[index] = settings;
			this.m_SSSDiffusionProfileUpdate[index] = settings.updateCount;
		}

		// Token: 0x0600035B RID: 859 RVA: 0x0001A1D4 File Offset: 0x000183D4
		private unsafe void PushSubsurfaceScatteringGlobalParams(HDCamera hdCamera, CommandBuffer cmd)
		{
			this.UpdateCurrentDiffusionProfileSettings(hdCamera);
			cmd.SetGlobalInt(HDShaderIDs._DiffusionProfileCount, this.m_SSSActiveDiffusionProfileCount);
			if (this.m_SSSActiveDiffusionProfileCount == 0)
			{
				return;
			}
			cmd.SetGlobalInt(HDShaderIDs._EnableSubsurfaceScattering, hdCamera.frameSettings.IsEnabled(FrameSettingsField.SubsurfaceScattering) ? 1 : 0);
			uint ssstexturingModeFlags = this.m_SSSTexturingModeFlags;
			uint ssstransmissionFlags = this.m_SSSTransmissionFlags;
			cmd.SetGlobalFloat(HDShaderIDs._TexturingModeFlags, *(float*)(&ssstexturingModeFlags));
			cmd.SetGlobalFloat(HDShaderIDs._TransmissionFlags, *(float*)(&ssstransmissionFlags));
			cmd.SetGlobalVectorArray(HDShaderIDs._ThicknessRemaps, this.m_SSSThicknessRemaps);
			cmd.SetGlobalVectorArray(HDShaderIDs._ShapeParams, this.m_SSSShapeParams);
			cmd.SetGlobalVectorArray(HDShaderIDs._TransmissionTintsAndFresnel0, hdCamera.frameSettings.IsEnabled(FrameSettingsField.Transmission) ? this.m_SSSTransmissionTintsAndFresnel0 : this.m_SSSDisabledTransmissionTintsAndFresnel0);
			cmd.SetGlobalVectorArray(HDShaderIDs._WorldScales, this.m_SSSWorldScales);
			cmd.SetGlobalFloatArray(HDShaderIDs._DiffusionProfileHashTable, this.m_SSSDiffusionProfileHashes);
		}

		// Token: 0x0600035C RID: 860 RVA: 0x0001A2BE File Offset: 0x000184BE
		private static bool NeedTemporarySubsurfaceBuffer()
		{
			return SystemInfo.graphicsDeviceType != GraphicsDeviceType.PlayStation4 && SystemInfo.graphicsDeviceType != GraphicsDeviceType.XboxOne && SystemInfo.graphicsDeviceType != GraphicsDeviceType.XboxOneD3D12;
		}

		// Token: 0x0600035D RID: 861 RVA: 0x0001A2E0 File Offset: 0x000184E0
		private HDRenderPipeline.SubsurfaceScatteringParameters PrepareSubsurfaceScatteringParameters(HDCamera hdCamera)
		{
			return new HDRenderPipeline.SubsurfaceScatteringParameters
			{
				subsurfaceScatteringCS = this.m_SubsurfaceScatteringCS,
				subsurfaceScatteringCSKernel = (hdCamera.frameSettings.IsEnabled(FrameSettingsField.MSAA) ? this.m_SubsurfaceScatteringKernelMSAA : this.m_SubsurfaceScatteringKernel),
				needTemporaryBuffer = (HDRenderPipeline.NeedTemporarySubsurfaceBuffer() || hdCamera.frameSettings.IsEnabled(FrameSettingsField.MSAA)),
				copyStencilForSplitLighting = this.m_SSSCopyStencilForSplitLighting,
				combineLighting = this.m_CombineLightingPass,
				texturingModeFlags = this.m_SSSTexturingModeFlags,
				numTilesX = ((int)hdCamera.screenSize.x + 15) / 16,
				numTilesY = ((int)hdCamera.screenSize.y + 15) / 16,
				numTilesZ = hdCamera.viewCount,
				worldScales = this.m_SSSWorldScales,
				filterKernels = this.m_SSSFilterKernels,
				shapeParams = this.m_SSSShapeParams,
				diffusionProfileHashes = this.m_SSSDiffusionProfileHashes
			};
		}

		// Token: 0x0600035E RID: 862 RVA: 0x0001A3E8 File Offset: 0x000185E8
		private static RTHandle SubSurfaceHistoryBufferAllocatorFunction(string viewName, int frameIndex, RTHandleSystem rtHandleSystem)
		{
			return rtHandleSystem.Alloc(Vector2.one, TextureXR.slices, DepthBits.None, GraphicsFormat.R16G16B16A16_SFloat, FilterMode.Point, TextureWrapMode.Repeat, TextureXR.dimension, true, false, false, false, 1, 0f, false, false, false, RenderTextureMemoryless.None, string.Format("SubSurfaceHistoryBuffer{0}", frameIndex));
		}

		// Token: 0x0600035F RID: 863 RVA: 0x0001A430 File Offset: 0x00018630
		private void RenderSubsurfaceScattering(HDCamera hdCamera, CommandBuffer cmd, RTHandle colorBufferRT, RTHandle diffuseBufferRT, RTHandle depthStencilBufferRT, RTHandle depthTextureRT)
		{
			if (!hdCamera.frameSettings.IsEnabled(FrameSettingsField.SubsurfaceScattering))
			{
				return;
			}
			SubSurfaceScattering component = hdCamera.volumeStack.GetComponent<SubSurfaceScattering>();
			if (hdCamera.frameSettings.IsEnabled(FrameSettingsField.RayTracing) && component.rayTracing.value && this.GetRayTracingState())
			{
				using (new ProfilingScope(cmd, ProfilingSampler.Get<HDProfileId>(HDProfileId.SubsurfaceScattering)))
				{
					int num = 8;
					int num2 = (hdCamera.actualWidth + (num - 1)) / num;
					int num3 = (hdCamera.actualHeight + (num - 1)) / num;
					cmd.SetComputeTextureParam(this.m_ScreenSpaceShadowsCS, this.m_ClearShadowTexture, HDShaderIDs._RaytracedShadowIntegration, diffuseBufferRT);
					cmd.DispatchCompute(this.m_ScreenSpaceShadowsCS, this.m_ClearShadowTexture, num2, num3, hdCamera.viewCount);
					RayTracingShader subSurfaceRayTracing = this.m_Asset.renderPipelineRayTracingResources.subSurfaceRayTracing;
					RayTracingSettings component2 = hdCamera.volumeStack.GetComponent<RayTracingSettings>();
					ComputeShader deferredRaytracingCS = this.m_Asset.renderPipelineRayTracingResources.deferredRaytracingCS;
					RTHandle rayTracingBuffer = this.GetRayTracingBuffer(InternalRayTracingBuffers.RGBA0);
					RTHandle rayTracingBuffer2 = this.GetRayTracingBuffer(InternalRayTracingBuffers.RGBA1);
					RTHandle rayTracingBuffer3 = this.GetRayTracingBuffer(InternalRayTracingBuffers.RGBA2);
					RTHandle rayTracingBuffer4 = this.GetRayTracingBuffer(InternalRayTracingBuffers.RGBA3);
					RayTracingAccelerationStructure rayTracingAccelerationStructure = this.RequestAccelerationStructure();
					cmd.SetRayTracingShaderPass(subSurfaceRayTracing, "SubSurfaceDXR");
					cmd.SetRayTracingAccelerationStructure(subSurfaceRayTracing, HDShaderIDs._RaytracingAccelerationStructureName, rayTracingAccelerationStructure);
					this.GetBlueNoiseManager().BindDitheredRNGData8SPP(cmd);
					for (int i = 0; i < component.sampleCount.value; i++)
					{
						cmd.SetRayTracingFloatParams(subSurfaceRayTracing, HDShaderIDs._RaytracingRayBias, new float[] { component2.rayBias.value });
						cmd.SetRayTracingIntParams(subSurfaceRayTracing, HDShaderIDs._RaytracingNumSamples, new int[] { component.sampleCount.value });
						cmd.SetRayTracingIntParams(subSurfaceRayTracing, HDShaderIDs._RaytracingSampleIndex, new int[] { i });
						int num4 = this.RayTracingFrameIndex(hdCamera);
						cmd.SetRayTracingIntParam(subSurfaceRayTracing, HDShaderIDs._RaytracingFrameIndex, num4);
						cmd.SetRayTracingTextureParam(subSurfaceRayTracing, HDShaderIDs._DepthTexture, this.sharedRTManager.GetDepthStencilBuffer(false));
						cmd.SetRayTracingTextureParam(subSurfaceRayTracing, HDShaderIDs._NormalBufferTexture, this.sharedRTManager.GetNormalBuffer(false));
						cmd.SetRayTracingTextureParam(subSurfaceRayTracing, HDShaderIDs._GBufferTexture[0], this.m_GbufferManager.GetBuffer(0));
						cmd.SetRayTracingTextureParam(subSurfaceRayTracing, HDShaderIDs._GBufferTexture[1], this.m_GbufferManager.GetBuffer(1));
						cmd.SetRayTracingTextureParam(subSurfaceRayTracing, HDShaderIDs._GBufferTexture[2], this.m_GbufferManager.GetBuffer(2));
						cmd.SetRayTracingTextureParam(subSurfaceRayTracing, HDShaderIDs._GBufferTexture[3], this.m_GbufferManager.GetBuffer(3));
						cmd.SetRayTracingTextureParam(subSurfaceRayTracing, HDShaderIDs._SSSBufferTexture, this.m_SSSColor);
						cmd.SetGlobalTexture(HDShaderIDs._StencilTexture, this.sharedRTManager.GetDepthStencilBuffer(false), RenderTextureSubElement.Stencil);
						cmd.SetRayTracingTextureParam(subSurfaceRayTracing, HDShaderIDs._ThroughputTextureRW, rayTracingBuffer);
						cmd.SetRayTracingTextureParam(subSurfaceRayTracing, HDShaderIDs._NormalTextureRW, rayTracingBuffer2);
						cmd.SetRayTracingTextureParam(subSurfaceRayTracing, HDShaderIDs._PositionTextureRW, rayTracingBuffer3);
						cmd.SetRayTracingTextureParam(subSurfaceRayTracing, HDShaderIDs._DiffuseLightingTextureRW, rayTracingBuffer4);
						cmd.DispatchRays(subSurfaceRayTracing, "RayGenSubSurface", (uint)hdCamera.actualWidth, (uint)hdCamera.actualHeight, (uint)hdCamera.viewCount, null);
						int num5 = deferredRaytracingCS.FindKernel("RaytracingDiffuseDeferred");
						this.RequestLightCluster().BindLightClusterData(cmd);
						cmd.SetComputeTextureParam(deferredRaytracingCS, num5, HDShaderIDs._DepthTexture, this.sharedRTManager.GetDepthStencilBuffer(false));
						cmd.SetComputeTextureParam(deferredRaytracingCS, num5, HDShaderIDs._ThroughputTextureRW, rayTracingBuffer);
						cmd.SetComputeTextureParam(deferredRaytracingCS, num5, HDShaderIDs._NormalTextureRW, rayTracingBuffer2);
						cmd.SetComputeTextureParam(deferredRaytracingCS, num5, HDShaderIDs._PositionTextureRW, rayTracingBuffer3);
						cmd.SetComputeTextureParam(deferredRaytracingCS, num5, HDShaderIDs._DiffuseLightingTextureRW, rayTracingBuffer4);
						cmd.SetComputeTextureParam(deferredRaytracingCS, num5, HDShaderIDs._RaytracingLitBufferRW, diffuseBufferRT);
						cmd.DispatchCompute(deferredRaytracingCS, num5, num2, num3, hdCamera.viewCount);
					}
					RTHandle rthandle = hdCamera.GetCurrentFrameRT(14) ?? hdCamera.AllocHistoryFrameRT(14, new Func<string, int, RTHandleSystem, RTHandle>(HDRenderPipeline.SubSurfaceHistoryBufferAllocatorFunction), 1);
					float num6 = 1f;
					num6 *= (this.ValidRayTracingHistory(hdCamera) ? 1f : 0f);
					this.GetTemporalFilter().DenoiseBuffer(cmd, hdCamera, diffuseBufferRT, rthandle, rayTracingBuffer, false, num6);
					this.PushFullScreenDebugTexture(hdCamera, cmd, rayTracingBuffer, FullScreenDebugMode.RayTracedSubSurface);
					this.m_CombineLightingPass.SetTexture(HDShaderIDs._IrradianceSource, rayTracingBuffer);
					HDUtils.DrawFullScreen(cmd, this.m_CombineLightingPass, colorBufferRT, depthStencilBufferRT, null, 1);
					return;
				}
			}
			using (new ProfilingScope(cmd, ProfilingSampler.Get<HDProfileId>(HDProfileId.SubsurfaceScattering)))
			{
				HDRenderPipeline.SubsurfaceScatteringParameters subsurfaceScatteringParameters = this.PrepareSubsurfaceScatteringParameters(hdCamera);
				HDRenderPipeline.SubsurfaceScatteringResources subsurfaceScatteringResources = default(HDRenderPipeline.SubsurfaceScatteringResources);
				subsurfaceScatteringResources.colorBuffer = colorBufferRT;
				subsurfaceScatteringResources.diffuseBuffer = diffuseBufferRT;
				subsurfaceScatteringResources.depthStencilBuffer = depthStencilBufferRT;
				subsurfaceScatteringResources.depthTexture = depthTextureRT;
				subsurfaceScatteringResources.cameraFilteringBuffer = this.m_SSSCameraFilteringBuffer;
				subsurfaceScatteringResources.coarseStencilBuffer = this.m_SharedRTManager.GetCoarseStencilBuffer();
				subsurfaceScatteringResources.sssBuffer = this.m_SSSColor;
				if (subsurfaceScatteringParameters.needTemporaryBuffer)
				{
					using (new ProfilingScope(cmd, ProfilingSampler.Get<HDProfileId>(HDProfileId.ClearSSSFilteringTarget)))
					{
						CoreUtils.SetRenderTarget(cmd, this.m_SSSCameraFilteringBuffer, ClearFlag.Color, Color.clear, 0, CubemapFace.Unknown, -1);
					}
				}
				HDRenderPipeline.RenderSubsurfaceScattering(in subsurfaceScatteringParameters, in subsurfaceScatteringResources, cmd);
			}
		}

		// Token: 0x06000360 RID: 864 RVA: 0x0001A9DC File Offset: 0x00018BDC
		private unsafe static void RenderSubsurfaceScattering(in HDRenderPipeline.SubsurfaceScatteringParameters parameters, in HDRenderPipeline.SubsurfaceScatteringResources resources, CommandBuffer cmd)
		{
			uint texturingModeFlags = parameters.texturingModeFlags;
			cmd.SetComputeFloatParam(parameters.subsurfaceScatteringCS, HDShaderIDs._TexturingModeFlags, *(float*)(&texturingModeFlags));
			cmd.SetComputeVectorArrayParam(parameters.subsurfaceScatteringCS, HDShaderIDs._WorldScales, parameters.worldScales);
			cmd.SetComputeVectorArrayParam(parameters.subsurfaceScatteringCS, HDShaderIDs._FilterKernels, parameters.filterKernels);
			cmd.SetComputeVectorArrayParam(parameters.subsurfaceScatteringCS, HDShaderIDs._ShapeParams, parameters.shapeParams);
			cmd.SetComputeFloatParams(parameters.subsurfaceScatteringCS, HDShaderIDs._DiffusionProfileHashTable, parameters.diffusionProfileHashes);
			cmd.SetComputeTextureParam(parameters.subsurfaceScatteringCS, parameters.subsurfaceScatteringCSKernel, HDShaderIDs._DepthTexture, resources.depthTexture);
			cmd.SetComputeTextureParam(parameters.subsurfaceScatteringCS, parameters.subsurfaceScatteringCSKernel, HDShaderIDs._IrradianceSource, resources.diffuseBuffer);
			cmd.SetComputeTextureParam(parameters.subsurfaceScatteringCS, parameters.subsurfaceScatteringCSKernel, HDShaderIDs._SSSBufferTexture, resources.sssBuffer);
			cmd.SetComputeBufferParam(parameters.subsurfaceScatteringCS, parameters.subsurfaceScatteringCSKernel, HDShaderIDs._CoarseStencilBuffer, resources.coarseStencilBuffer);
			if (parameters.needTemporaryBuffer)
			{
				cmd.SetComputeTextureParam(parameters.subsurfaceScatteringCS, parameters.subsurfaceScatteringCSKernel, HDShaderIDs._CameraFilteringBuffer, resources.cameraFilteringBuffer);
				cmd.DispatchCompute(parameters.subsurfaceScatteringCS, parameters.subsurfaceScatteringCSKernel, parameters.numTilesX, parameters.numTilesY, parameters.numTilesZ);
				parameters.combineLighting.SetTexture(HDShaderIDs._IrradianceSource, resources.cameraFilteringBuffer);
				HDUtils.DrawFullScreen(cmd, parameters.combineLighting, resources.colorBuffer, resources.depthStencilBuffer, null, 0);
				return;
			}
			cmd.SetComputeTextureParam(parameters.subsurfaceScatteringCS, parameters.subsurfaceScatteringCSKernel, HDShaderIDs._CameraColorTexture, resources.colorBuffer);
			cmd.DispatchCompute(parameters.subsurfaceScatteringCS, parameters.subsurfaceScatteringCSKernel, parameters.numTilesX, parameters.numTilesY, parameters.numTilesZ);
		}

		// Token: 0x06000361 RID: 865 RVA: 0x0001ABB0 File Offset: 0x00018DB0
		private RenderGraphMutableResource ResolveFullScreenDebug(RenderGraph renderGraph, in HDRenderPipeline.DebugParameters debugParameters, RenderGraphResource inputFullScreenDebug, RenderGraphResource depthPyramid)
		{
			HDRenderPipeline.ResolveFullScreenDebugPassData resolveFullScreenDebugPassData;
			RenderGraphMutableResource renderGraphMutableResource;
			using (RenderGraphBuilder renderGraphBuilder = renderGraph.AddRenderPass<HDRenderPipeline.ResolveFullScreenDebugPassData>("ResolveFullScreenDebug", out resolveFullScreenDebugPassData, null))
			{
				resolveFullScreenDebugPassData.debugParameters = debugParameters;
				resolveFullScreenDebugPassData.input = renderGraphBuilder.ReadTexture(in inputFullScreenDebug);
				resolveFullScreenDebugPassData.depthPyramid = renderGraphBuilder.ReadTexture(in depthPyramid);
				HDRenderPipeline.ResolveFullScreenDebugPassData resolveFullScreenDebugPassData2 = resolveFullScreenDebugPassData;
				renderGraphMutableResource = renderGraph.CreateTexture(new TextureDesc(Vector2.one, true, true)
				{
					colorFormat = GraphicsFormat.R16G16B16A16_SFloat,
					name = "ResolveFullScreenDebug"
				}, 0);
				resolveFullScreenDebugPassData2.output = renderGraphBuilder.WriteTexture(in renderGraphMutableResource);
				renderGraphBuilder.SetRenderFunc<HDRenderPipeline.ResolveFullScreenDebugPassData>(delegate(HDRenderPipeline.ResolveFullScreenDebugPassData data, RenderGraphContext ctx)
				{
					MaterialPropertyBlock tempMaterialPropertyBlock = ctx.renderGraphPool.GetTempMaterialPropertyBlock();
					RTHandle texture = ctx.resources.GetTexture(in data.input);
					RTHandle texture2 = ctx.resources.GetTexture(in data.depthPyramid);
					RenderGraphResourceRegistry resources = ctx.resources;
					RenderGraphResource renderGraphResource = data.output;
					HDRenderPipeline.ResolveFullScreenDebug(in data.debugParameters, tempMaterialPropertyBlock, texture, texture2, resources.GetTexture(in renderGraphResource), ctx.cmd);
				});
				renderGraphMutableResource = resolveFullScreenDebugPassData.output;
			}
			return renderGraphMutableResource;
		}

		// Token: 0x06000362 RID: 866 RVA: 0x0001AC80 File Offset: 0x00018E80
		private RenderGraphMutableResource ResolveColorPickerDebug(RenderGraph renderGraph, in HDRenderPipeline.DebugParameters debugParameters, RenderGraphResource inputColorPickerDebug)
		{
			HDRenderPipeline.ResolveColorPickerDebugPassData resolveColorPickerDebugPassData;
			RenderGraphMutableResource renderGraphMutableResource;
			using (RenderGraphBuilder renderGraphBuilder = renderGraph.AddRenderPass<HDRenderPipeline.ResolveColorPickerDebugPassData>("ResolveColorPickerDebug", out resolveColorPickerDebugPassData, null))
			{
				resolveColorPickerDebugPassData.debugParameters = debugParameters;
				resolveColorPickerDebugPassData.input = renderGraphBuilder.ReadTexture(in inputColorPickerDebug);
				HDRenderPipeline.ResolveColorPickerDebugPassData resolveColorPickerDebugPassData2 = resolveColorPickerDebugPassData;
				renderGraphMutableResource = renderGraph.CreateTexture(new TextureDesc(Vector2.one, true, true)
				{
					colorFormat = GraphicsFormat.R16G16B16A16_SFloat,
					name = "ResolveColorPickerDebug"
				}, 0);
				resolveColorPickerDebugPassData2.output = renderGraphBuilder.WriteTexture(in renderGraphMutableResource);
				renderGraphBuilder.SetRenderFunc<HDRenderPipeline.ResolveColorPickerDebugPassData>(delegate(HDRenderPipeline.ResolveColorPickerDebugPassData data, RenderGraphContext ctx)
				{
					RTHandle texture = ctx.resources.GetTexture(in data.input);
					RenderGraphResourceRegistry resources = ctx.resources;
					RenderGraphResource renderGraphResource = data.output;
					HDRenderPipeline.ResolveColorPickerDebug(in data.debugParameters, texture, resources.GetTexture(in renderGraphResource), ctx.cmd);
				});
				renderGraphMutableResource = resolveColorPickerDebugPassData.output;
			}
			return renderGraphMutableResource;
		}

		// Token: 0x06000363 RID: 867 RVA: 0x0001AD40 File Offset: 0x00018F40
		private void RenderDebugOverlays(RenderGraph renderGraph, in HDRenderPipeline.DebugParameters debugParameters, RenderGraphMutableResource colorBuffer, RenderGraphMutableResource depthBuffer, RenderGraphResource depthPyramidTexture, in ShadowResult shadowResult)
		{
			HDRenderPipeline.RenderDebugOverlayPassData renderDebugOverlayPassData;
			using (RenderGraphBuilder renderGraphBuilder = renderGraph.AddRenderPass<HDRenderPipeline.RenderDebugOverlayPassData>("DebugOverlay", out renderDebugOverlayPassData, null))
			{
				renderDebugOverlayPassData.debugParameters = debugParameters;
				renderDebugOverlayPassData.colorBuffer = renderGraphBuilder.UseColorBuffer(in colorBuffer, 0);
				renderDebugOverlayPassData.depthBuffer = renderGraphBuilder.UseDepthBuffer(in depthBuffer, DepthAccess.ReadWrite);
				renderDebugOverlayPassData.depthPyramidTexture = renderGraphBuilder.ReadTexture(in depthPyramidTexture);
				renderDebugOverlayPassData.shadowTextures = HDShadowManager.ReadShadowResult(shadowResult, renderGraphBuilder);
				renderGraphBuilder.SetRenderFunc<HDRenderPipeline.RenderDebugOverlayPassData>(delegate(HDRenderPipeline.RenderDebugOverlayPassData data, RenderGraphContext ctx)
				{
					HDRenderPipeline.DebugParameters debugParameters2 = data.debugParameters;
					HDUtils.ResetOverlay();
					float num = 0f;
					float debugOverlayRatio = debugParameters2.debugDisplaySettings.data.debugOverlayRatio;
					float num2 = (float)Math.Min(debugParameters2.hdCamera.actualHeight, debugParameters2.hdCamera.actualWidth) * debugOverlayRatio;
					float num3 = (float)debugParameters2.hdCamera.actualHeight - num2;
					HDShadowManager.ShadowDebugAtlasTextures shadowDebugAtlasTextures = default(HDShadowManager.ShadowDebugAtlasTextures);
					shadowDebugAtlasTextures.punctualShadowAtlas = (data.shadowTextures.punctualShadowResult.IsValid() ? ctx.resources.GetTexture(in data.shadowTextures.punctualShadowResult) : null);
					shadowDebugAtlasTextures.cascadeShadowAtlas = (data.shadowTextures.directionalShadowResult.IsValid() ? ctx.resources.GetTexture(in data.shadowTextures.directionalShadowResult) : null);
					shadowDebugAtlasTextures.areaShadowAtlas = (data.shadowTextures.areaShadowResult.IsValid() ? ctx.resources.GetTexture(in data.shadowTextures.areaShadowResult) : null);
					HDRenderPipeline.RenderSkyReflectionOverlay(in debugParameters2, ctx.cmd, ctx.renderGraphPool.GetTempMaterialPropertyBlock(), ref num, ref num3, num2);
					HDRenderPipeline.RenderRayCountOverlay(in debugParameters2, ctx.cmd, ref num, ref num3, num2);
					HDRenderPipeline.RenderLightLoopDebugOverlay(in debugParameters2, ctx.cmd, ref num, ref num3, num2, ctx.resources.GetTexture(in data.depthPyramidTexture));
					HDRenderPipeline.RenderShadowsDebugOverlay(in debugParameters2, in shadowDebugAtlasTextures, ctx.cmd, ref num, ref num3, num2, ctx.renderGraphPool.GetTempMaterialPropertyBlock());
					DecalSystem.instance.RenderDebugOverlay(debugParameters2.hdCamera, ctx.cmd, debugParameters2.debugDisplaySettings, ref num, ref num3, num2, (float)debugParameters2.hdCamera.actualWidth);
				});
			}
		}

		// Token: 0x06000364 RID: 868 RVA: 0x0001ADF0 File Offset: 0x00018FF0
		private static void RenderLightVolumes(RenderGraph renderGraph, in HDRenderPipeline.DebugParameters debugParameters, RenderGraphMutableResource destination, RenderGraphMutableResource depthBuffer, CullingResults cullResults)
		{
			HDRenderPipeline.RenderLightVolumesPassData renderLightVolumesPassData;
			using (RenderGraphBuilder renderGraphBuilder = renderGraph.AddRenderPass<HDRenderPipeline.RenderLightVolumesPassData>("LightVolumes", out renderLightVolumesPassData, null))
			{
				renderLightVolumesPassData.parameters = HDRenderPipeline.s_lightVolumes.PrepareLightVolumeParameters(debugParameters.hdCamera, debugParameters.debugDisplaySettings.data.lightingDebugSettings, cullResults);
				HDRenderPipeline.RenderLightVolumesPassData renderLightVolumesPassData2 = renderLightVolumesPassData;
				RenderGraphMutableResource renderGraphMutableResource = renderGraph.CreateTexture(new TextureDesc(Vector2.one, true, true)
				{
					colorFormat = GraphicsFormat.R32_SFloat,
					clearBuffer = true,
					clearColor = Color.black,
					name = "LightVolumeCount"
				}, 0);
				renderLightVolumesPassData2.lightCountBuffer = renderGraphBuilder.WriteTexture(in renderGraphMutableResource);
				HDRenderPipeline.RenderLightVolumesPassData renderLightVolumesPassData3 = renderLightVolumesPassData;
				renderGraphMutableResource = renderGraph.CreateTexture(new TextureDesc(Vector2.one, true, true)
				{
					colorFormat = GraphicsFormat.R16G16B16A16_SFloat,
					clearBuffer = true,
					clearColor = Color.black,
					name = "LightVolumeColorAccumulation"
				}, 0);
				renderLightVolumesPassData3.colorAccumulationBuffer = renderGraphBuilder.WriteTexture(in renderGraphMutableResource);
				HDRenderPipeline.RenderLightVolumesPassData renderLightVolumesPassData4 = renderLightVolumesPassData;
				renderGraphMutableResource = renderGraph.CreateTexture(new TextureDesc(Vector2.one, true, true)
				{
					colorFormat = GraphicsFormat.R16G16B16A16_SFloat,
					clearBuffer = true,
					clearColor = Color.black,
					enableRandomWrite = true,
					name = "LightVolumeDebugLightVolumesTexture"
				}, 0);
				renderLightVolumesPassData4.debugLightVolumesTexture = renderGraphBuilder.WriteTexture(in renderGraphMutableResource);
				renderLightVolumesPassData.depthBuffer = renderGraphBuilder.UseDepthBuffer(in depthBuffer, DepthAccess.ReadWrite);
				renderLightVolumesPassData.destination = renderGraphBuilder.WriteTexture(in destination);
				renderGraphBuilder.SetRenderFunc<HDRenderPipeline.RenderLightVolumesPassData>(delegate(HDRenderPipeline.RenderLightVolumesPassData data, RenderGraphContext ctx)
				{
					RenderTargetIdentifier[] tempArray = ctx.renderGraphPool.GetTempArray<RenderTargetIdentifier>(2);
					RenderGraphResourceRegistry resources = ctx.resources;
					RenderGraphResource renderGraphResource = data.lightCountBuffer;
					RTHandle texture = resources.GetTexture(in renderGraphResource);
					RenderGraphResourceRegistry resources2 = ctx.resources;
					renderGraphResource = data.colorAccumulationBuffer;
					RTHandle texture2 = resources2.GetTexture(in renderGraphResource);
					tempArray[0] = texture;
					tempArray[1] = texture2;
					CommandBuffer cmd = ctx.cmd;
					RenderTargetIdentifier[] array = tempArray;
					RTHandle rthandle = texture;
					RTHandle rthandle2 = texture2;
					RenderGraphResourceRegistry resources3 = ctx.resources;
					renderGraphResource = data.debugLightVolumesTexture;
					RTHandle texture3 = resources3.GetTexture(in renderGraphResource);
					RenderGraphResourceRegistry resources4 = ctx.resources;
					RenderGraphResource renderGraphResource2 = data.depthBuffer;
					RTHandle texture4 = resources4.GetTexture(in renderGraphResource2);
					RenderGraphResourceRegistry resources5 = ctx.resources;
					RenderGraphResource renderGraphResource3 = data.destination;
					DebugLightVolumes.RenderLightVolumes(cmd, in data.parameters, array, rthandle, rthandle2, texture3, texture4, resources5.GetTexture(in renderGraphResource3), ctx.renderGraphPool.GetTempMaterialPropertyBlock());
				});
			}
		}

		// Token: 0x06000365 RID: 869 RVA: 0x0001AF98 File Offset: 0x00019198
		private RenderGraphMutableResource RenderDebug(RenderGraph renderGraph, HDCamera hdCamera, RenderGraphMutableResource colorBuffer, RenderGraphMutableResource depthBuffer, RenderGraphResource depthPyramidTexture, RenderGraphResource fullScreenDebugTexture, RenderGraphResource colorPickerDebugTexture, in ShadowResult shadowResult, CullingResults cullResults)
		{
			if (hdCamera.camera.cameraType == CameraType.Reflection || hdCamera.camera.cameraType == CameraType.Preview)
			{
				return colorBuffer;
			}
			RenderGraphMutableResource renderGraphMutableResource = colorBuffer;
			HDRenderPipeline.DebugParameters debugParameters = this.PrepareDebugParameters(hdCamera, this.GetDepthBufferMipChainInfo());
			if (debugParameters.resolveFullScreenDebug)
			{
				renderGraphMutableResource = this.ResolveFullScreenDebug(renderGraph, in debugParameters, fullScreenDebugTexture, depthPyramidTexture);
				if (debugParameters.colorPickerEnabled)
				{
					colorPickerDebugTexture = this.PushColorPickerDebugTexture(renderGraph, renderGraphMutableResource);
				}
				this.m_FullScreenDebugPushed = false;
			}
			if (debugParameters.colorPickerEnabled)
			{
				renderGraphMutableResource = this.ResolveColorPickerDebug(renderGraph, in debugParameters, colorPickerDebugTexture);
			}
			if (debugParameters.debugDisplaySettings.data.lightingDebugSettings.displayLightVolumes)
			{
				HDRenderPipeline.RenderLightVolumes(renderGraph, in debugParameters, renderGraphMutableResource, depthBuffer, cullResults);
			}
			this.RenderDebugOverlays(renderGraph, in debugParameters, renderGraphMutableResource, depthBuffer, depthPyramidTexture, in shadowResult);
			return renderGraphMutableResource;
		}

		// Token: 0x06000366 RID: 870 RVA: 0x0001B054 File Offset: 0x00019254
		private void RenderDebugViewMaterial(RenderGraph renderGraph, CullingResults cull, HDCamera hdCamera, RenderGraphMutableResource output)
		{
			if (this.m_CurrentDebugDisplaySettings.data.materialDebugSettings.IsDebugGBufferEnabled() && hdCamera.frameSettings.litShaderMode == LitShaderMode.Deferred)
			{
				HDRenderPipeline.DebugViewMaterialData debugViewMaterialData;
				using (RenderGraphBuilder renderGraphBuilder = renderGraph.AddRenderPass<HDRenderPipeline.DebugViewMaterialData>("DebugViewMaterialGBuffer", out debugViewMaterialData, ProfilingSampler.Get<HDProfileId>(HDProfileId.DebugViewMaterialGBuffer)))
				{
					debugViewMaterialData.debugGBufferMaterial = this.m_currentDebugViewMaterialGBuffer;
					debugViewMaterialData.outputColor = renderGraphBuilder.WriteTexture(in output);
					renderGraphBuilder.SetRenderFunc<HDRenderPipeline.DebugViewMaterialData>(delegate(HDRenderPipeline.DebugViewMaterialData data, RenderGraphContext context)
					{
						RenderGraphResourceRegistry resources = context.resources;
						CommandBuffer cmd = context.cmd;
						Material debugGBufferMaterial = data.debugGBufferMaterial;
						RenderGraphResourceRegistry renderGraphResourceRegistry = resources;
						RenderGraphResource renderGraphResource2 = data.outputColor;
						HDUtils.DrawFullScreen(cmd, debugGBufferMaterial, renderGraphResourceRegistry.GetTexture(in renderGraphResource2), null, 0);
					});
					return;
				}
			}
			HDRenderPipeline.DebugViewMaterialData debugViewMaterialData2;
			using (RenderGraphBuilder renderGraphBuilder2 = renderGraph.AddRenderPass<HDRenderPipeline.DebugViewMaterialData>("DisplayDebug ViewMaterial", out debugViewMaterialData2, ProfilingSampler.Get<HDProfileId>(HDProfileId.DisplayDebugViewMaterial)))
			{
				debugViewMaterialData2.frameSettings = hdCamera.frameSettings;
				debugViewMaterialData2.outputColor = renderGraphBuilder2.UseColorBuffer(in output, 0);
				HDRenderPipeline.DebugViewMaterialData debugViewMaterialData3 = debugViewMaterialData2;
				RenderGraphMutableResource renderGraphMutableResource = this.CreateDepthBuffer(renderGraph, hdCamera.frameSettings.IsEnabled(FrameSettingsField.MSAA));
				debugViewMaterialData3.outputDepth = renderGraphBuilder2.UseDepthBuffer(in renderGraphMutableResource, DepthAccess.ReadWrite);
				HDRenderPipeline.DebugViewMaterialData debugViewMaterialData4 = debugViewMaterialData2;
				RendererListDesc rendererListDesc = HDRenderPipeline.CreateOpaqueRendererListDesc(cull, hdCamera.camera, this.m_AllForwardOpaquePassNames, this.m_CurrentRendererConfigurationBakedLighting, null, new RenderStateBlock?(this.m_DepthStateOpaque), null, false);
				RenderGraphResource renderGraphResource = renderGraph.CreateRendererList(in rendererListDesc);
				debugViewMaterialData4.opaqueRendererList = renderGraphBuilder2.UseRendererList(in renderGraphResource);
				HDRenderPipeline.DebugViewMaterialData debugViewMaterialData5 = debugViewMaterialData2;
				rendererListDesc = HDRenderPipeline.CreateTransparentRendererListDesc(cull, hdCamera.camera, this.m_AllTransparentPassNames, this.m_CurrentRendererConfigurationBakedLighting, null, new RenderStateBlock?(this.m_DepthStateOpaque), null, false);
				renderGraphResource = renderGraph.CreateRendererList(in rendererListDesc);
				debugViewMaterialData5.transparentRendererList = renderGraphBuilder2.UseRendererList(in renderGraphResource);
				renderGraphBuilder2.SetRenderFunc<HDRenderPipeline.DebugViewMaterialData>(delegate(HDRenderPipeline.DebugViewMaterialData data, RenderGraphContext context)
				{
					RenderGraphResourceRegistry resources2 = context.resources;
					RendererList rendererList = resources2.GetRendererList(in data.opaqueRendererList);
					HDRenderPipeline.DrawOpaqueRendererList(in context, in data.frameSettings, in rendererList);
					HDRenderPipeline.DrawTransparentRendererList(in context, in data.frameSettings, resources2.GetRendererList(in data.transparentRendererList));
				});
			}
		}

		// Token: 0x06000367 RID: 871 RVA: 0x0001B250 File Offset: 0x00019450
		private void PushFullScreenLightingDebugTexture(RenderGraph renderGraph, RenderGraphResource input)
		{
			if (this.NeedsFullScreenDebugMode() && !this.m_FullScreenDebugPushed)
			{
				this.PushFullScreenDebugTexture(renderGraph, input, Vector4.one, -1);
			}
		}

		// Token: 0x06000368 RID: 872 RVA: 0x0001B270 File Offset: 0x00019470
		private void PushFullScreenDebugTexture(RenderGraph renderGraph, RenderGraphResource input, FullScreenDebugMode debugMode)
		{
			if (debugMode == this.m_CurrentDebugDisplaySettings.data.fullScreenDebugMode)
			{
				this.PushFullScreenDebugTexture(renderGraph, input, Vector4.one, -1);
			}
		}

		// Token: 0x06000369 RID: 873 RVA: 0x0001B294 File Offset: 0x00019494
		private void PushFullScreenDebugTextureMip(RenderGraph renderGraph, RenderGraphResource input, int lodCount, Vector4 scaleBias, FullScreenDebugMode debugMode)
		{
			if (debugMode == this.m_CurrentDebugDisplaySettings.data.fullScreenDebugMode)
			{
				int num = Mathf.FloorToInt(this.m_CurrentDebugDisplaySettings.data.fullscreenDebugMip * (float)lodCount);
				this.PushFullScreenDebugTexture(renderGraph, input, scaleBias, num);
			}
		}

		// Token: 0x0600036A RID: 874 RVA: 0x0001B2DC File Offset: 0x000194DC
		private void PushFullScreenDebugTexture(RenderGraph renderGraph, RenderGraphResource input, Vector4 scaleBias, int mipIndex = -1)
		{
			HDRenderPipeline.PushFullScreenDebugPassData passData;
			using (RenderGraphBuilder renderGraphBuilder = renderGraph.AddRenderPass<HDRenderPipeline.PushFullScreenDebugPassData>("Push Full Screen Debug", out passData, null))
			{
				passData.scaleBias = ((mipIndex != -1) ? scaleBias : new Vector4(1f, 1f, 0f, 0f));
				passData.mipIndex = mipIndex;
				passData.input = renderGraphBuilder.ReadTexture(in input);
				HDRenderPipeline.PushFullScreenDebugPassData passData2 = passData;
				RenderGraphMutableResource renderGraphMutableResource = renderGraph.CreateTexture(new TextureDesc(Vector2.one, true, true)
				{
					colorFormat = GraphicsFormat.R16G16B16A16_SFloat,
					name = "DebugFullScreen"
				}, 0);
				passData2.output = renderGraphBuilder.UseColorBuffer(in renderGraphMutableResource, 0);
				renderGraphBuilder.SetRenderFunc<HDRenderPipeline.PushFullScreenDebugPassData>(delegate(HDRenderPipeline.PushFullScreenDebugPassData data, RenderGraphContext ctx)
				{
					RenderGraphResource renderGraphResource;
					if (data.mipIndex != -1)
					{
						CommandBuffer cmd = ctx.cmd;
						RTHandle texture = ctx.resources.GetTexture(in passData.input);
						RenderGraphResourceRegistry resources = ctx.resources;
						renderGraphResource = passData.output;
						HDUtils.BlitCameraTexture(cmd, texture, resources.GetTexture(in renderGraphResource), data.scaleBias, (float)data.mipIndex, false);
						return;
					}
					CommandBuffer cmd2 = ctx.cmd;
					RTHandle texture2 = ctx.resources.GetTexture(in passData.input);
					RenderGraphResourceRegistry resources2 = ctx.resources;
					renderGraphResource = passData.output;
					HDUtils.BlitCameraTexture(cmd2, texture2, resources2.GetTexture(in renderGraphResource), 0f, false);
				});
				this.m_DebugFullScreenTexture = passData.output;
			}
			this.m_FullScreenDebugPushed = true;
		}

		// Token: 0x0600036B RID: 875 RVA: 0x0001B3DC File Offset: 0x000195DC
		private RenderGraphResource PushColorPickerDebugTexture(RenderGraph renderGraph, RenderGraphResource input)
		{
			HDRenderPipeline.PushFullScreenDebugPassData passData;
			RenderGraphResource renderGraphResource;
			using (RenderGraphBuilder renderGraphBuilder = renderGraph.AddRenderPass<HDRenderPipeline.PushFullScreenDebugPassData>("Push To Color Picker", out passData, null))
			{
				passData.input = renderGraphBuilder.ReadTexture(in input);
				HDRenderPipeline.PushFullScreenDebugPassData passData2 = passData;
				RenderGraphMutableResource renderGraphMutableResource = renderGraph.CreateTexture(new TextureDesc(Vector2.one, true, true)
				{
					colorFormat = GraphicsFormat.R16G16B16A16_SFloat,
					name = "DebugColorPicker"
				}, 0);
				passData2.output = renderGraphBuilder.UseColorBuffer(in renderGraphMutableResource, 0);
				renderGraphBuilder.SetRenderFunc<HDRenderPipeline.PushFullScreenDebugPassData>(delegate(HDRenderPipeline.PushFullScreenDebugPassData data, RenderGraphContext ctx)
				{
					CommandBuffer cmd = ctx.cmd;
					RTHandle texture = ctx.resources.GetTexture(in passData.input);
					RenderGraphResourceRegistry resources = ctx.resources;
					RenderGraphResource renderGraphResource2 = passData.output;
					HDUtils.BlitCameraTexture(cmd, texture, resources.GetTexture(in renderGraphResource2), 0f, false);
				});
				renderGraphResource = passData.output;
			}
			return renderGraphResource;
		}

		// Token: 0x0600036C RID: 876 RVA: 0x0001B4A0 File Offset: 0x000196A0
		private static void ReadLightingBuffers(HDRenderPipeline.LightingBuffers buffers, RenderGraphBuilder builder)
		{
			builder.ReadTexture(in buffers.ambientOcclusionBuffer);
			builder.ReadTexture(in buffers.ssrLightingBuffer);
			builder.ReadTexture(in buffers.contactShadowsBuffer);
		}

		// Token: 0x0600036D RID: 877 RVA: 0x0001B4D0 File Offset: 0x000196D0
		private void BuildGPULightList(RenderGraph renderGraph, HDCamera hdCamera, RenderGraphResource depthStencilBuffer, RenderGraphResource stencilBufferCopy, HDRenderPipeline.GBufferOutput gBuffer)
		{
			HDRenderPipeline.BuildGPULightListPassData passData;
			using (RenderGraphBuilder renderGraphBuilder = renderGraph.AddRenderPass<HDRenderPipeline.BuildGPULightListPassData>("Build Light List", out passData, ProfilingSampler.Get<HDProfileId>(HDProfileId.BuildLightList)))
			{
				renderGraphBuilder.EnableAsyncCompute(hdCamera.frameSettings.BuildLightListRunsAsync());
				passData.lightDataGlobalParameters = this.PrepareLightDataGlobalParameters(hdCamera);
				passData.shadowGlobalParameters = this.PrepareShadowGlobalParameters(hdCamera);
				passData.lightLoopGlobalParameters = this.PrepareLightLoopGlobalParameters(hdCamera);
				passData.buildGPULightListParameters = this.PrepareBuildGPULightListParameters(hdCamera);
				passData.buildGPULightListResources = this.PrepareBuildGPULightListResources(this.m_TileAndClusterData, null, null);
				passData.depthBuffer = renderGraphBuilder.ReadTexture(in depthStencilBuffer);
				passData.stencilTexture = renderGraphBuilder.ReadTexture(in stencilBufferCopy);
				if (passData.buildGPULightListParameters.computeMaterialVariants && passData.buildGPULightListParameters.enableFeatureVariants)
				{
					for (int i = 0; i < gBuffer.gBufferCount; i++)
					{
						passData.gBuffer[i] = renderGraphBuilder.ReadTexture(in gBuffer.mrt[i]);
					}
					passData.gBufferCount = gBuffer.gBufferCount;
				}
				renderGraphBuilder.SetRenderFunc<HDRenderPipeline.BuildGPULightListPassData>(delegate(HDRenderPipeline.BuildGPULightListPassData data, RenderGraphContext context)
				{
					bool flag = false;
					data.buildGPULightListResources.depthBuffer = context.resources.GetTexture(in data.depthBuffer);
					data.buildGPULightListResources.stencilTexture = context.resources.GetTexture(in data.stencilTexture);
					if (passData.buildGPULightListParameters.computeMaterialVariants && passData.buildGPULightListParameters.enableFeatureVariants)
					{
						data.buildGPULightListResources.gBuffer = context.renderGraphPool.GetTempArray<RTHandle>(data.gBufferCount);
						for (int j = 0; j < data.gBufferCount; j++)
						{
							data.buildGPULightListResources.gBuffer[j] = context.resources.GetTexture(in data.gBuffer[j]);
						}
					}
					HDRenderPipeline.GenerateLightsScreenSpaceAABBs(in data.buildGPULightListParameters, in data.buildGPULightListResources, context.cmd);
					HDRenderPipeline.BigTilePrepass(in data.buildGPULightListParameters, in data.buildGPULightListResources, context.cmd);
					HDRenderPipeline.BuildPerTileLightList(in data.buildGPULightListParameters, in data.buildGPULightListResources, ref flag, context.cmd);
					HDRenderPipeline.VoxelLightListGeneration(in data.buildGPULightListParameters, in data.buildGPULightListResources, context.cmd);
					HDRenderPipeline.BuildDispatchIndirectArguments(in data.buildGPULightListParameters, in data.buildGPULightListResources, flag, context.cmd);
					HDRenderPipeline.PushLightDataGlobalParams(in data.lightDataGlobalParameters, context.cmd);
					HDRenderPipeline.PushShadowGlobalParams(in data.shadowGlobalParameters, context.cmd);
					HDRenderPipeline.PushLightLoopGlobalParams(in data.lightLoopGlobalParameters, context.cmd);
				});
			}
		}

		// Token: 0x0600036E RID: 878 RVA: 0x0001B648 File Offset: 0x00019848
		private void PushGlobalCameraParams(RenderGraph renderGraph, HDCamera hdCamera)
		{
			HDRenderPipeline.PushGlobalCameraParamPassData pushGlobalCameraParamPassData;
			using (RenderGraphBuilder renderGraphBuilder = renderGraph.AddRenderPass<HDRenderPipeline.PushGlobalCameraParamPassData>("Push Global Camera Parameters", out pushGlobalCameraParamPassData, null))
			{
				pushGlobalCameraParamPassData.hdCamera = hdCamera;
				pushGlobalCameraParamPassData.frameCount = this.m_FrameCount;
				renderGraphBuilder.SetRenderFunc<HDRenderPipeline.PushGlobalCameraParamPassData>(delegate(HDRenderPipeline.PushGlobalCameraParamPassData data, RenderGraphContext context)
				{
					data.hdCamera.SetupGlobalParams(context.cmd, data.frameCount);
				});
			}
		}

		// Token: 0x0600036F RID: 879 RVA: 0x0001B6C0 File Offset: 0x000198C0
		internal ShadowResult RenderShadows(RenderGraph renderGraph, HDCamera hdCamera, CullingResults cullResults)
		{
			ShadowResult shadowResult = this.m_ShadowManager.RenderShadows(this.m_RenderGraph, hdCamera, cullResults);
			this.PushGlobalCameraParams(renderGraph, hdCamera);
			return shadowResult;
		}

		// Token: 0x06000370 RID: 880 RVA: 0x0001B6E0 File Offset: 0x000198E0
		private RenderGraphMutableResource CreateDiffuseLightingBuffer(RenderGraph renderGraph, bool msaa)
		{
			return renderGraph.CreateTexture(new TextureDesc(Vector2.one, true, true)
			{
				colorFormat = GraphicsFormat.B10G11R11_UFloatPack32,
				enableRandomWrite = !msaa,
				bindTextureMS = msaa,
				enableMSAA = msaa,
				clearBuffer = true,
				clearColor = Color.clear,
				name = string.Format("CameraSSSDiffuseLighting{0}", msaa ? "MSAA" : "")
			}, 0);
		}

		// Token: 0x06000371 RID: 881 RVA: 0x0001B75C File Offset: 0x0001995C
		private HDRenderPipeline.LightingOutput RenderDeferredLighting(RenderGraph renderGraph, HDCamera hdCamera, RenderGraphMutableResource colorBuffer, RenderGraphResource depthStencilBuffer, RenderGraphResource depthPyramidTexture, in HDRenderPipeline.LightingBuffers lightingBuffers, in HDRenderPipeline.GBufferOutput gbuffer, in ShadowResult shadowResult)
		{
			HDRenderPipeline.LightingOutput lightingOutput;
			if (hdCamera.frameSettings.litShaderMode != LitShaderMode.Deferred)
			{
				lightingOutput = default(HDRenderPipeline.LightingOutput);
				return lightingOutput;
			}
			HDRenderPipeline.DeferredLightingPassData deferredLightingPassData;
			using (RenderGraphBuilder renderGraphBuilder = renderGraph.AddRenderPass<HDRenderPipeline.DeferredLightingPassData>("Deferred Lighting", out deferredLightingPassData, null))
			{
				deferredLightingPassData.parameters = this.PrepareDeferredLightingParameters(hdCamera, this.debugDisplaySettings);
				deferredLightingPassData.resources = default(HDRenderPipeline.DeferredLightingResources);
				deferredLightingPassData.resources.lightListBuffer = this.m_TileAndClusterData.lightList;
				deferredLightingPassData.resources.tileFeatureFlagsBuffer = this.m_TileAndClusterData.tileFeatureFlags;
				deferredLightingPassData.resources.tileListBuffer = this.m_TileAndClusterData.tileList;
				deferredLightingPassData.resources.dispatchIndirectBuffer = this.m_TileAndClusterData.dispatchIndirectBuffer;
				deferredLightingPassData.colorBuffer = renderGraphBuilder.WriteTexture(in colorBuffer);
				if (deferredLightingPassData.parameters.outputSplitLighting)
				{
					deferredLightingPassData.sssDiffuseLightingBuffer = renderGraphBuilder.WriteTexture(in lightingBuffers.diffuseLightingBuffer);
				}
				deferredLightingPassData.depthBuffer = renderGraphBuilder.ReadTexture(in depthStencilBuffer);
				deferredLightingPassData.depthTexture = renderGraphBuilder.ReadTexture(in depthPyramidTexture);
				HDRenderPipeline.ReadLightingBuffers(lightingBuffers, renderGraphBuilder);
				deferredLightingPassData.gbufferCount = gbuffer.gBufferCount;
				for (int i = 0; i < gbuffer.gBufferCount; i++)
				{
					deferredLightingPassData.gbuffer[i] = renderGraphBuilder.ReadTexture(in gbuffer.mrt[i]);
				}
				HDShadowManager.ReadShadowResult(shadowResult, renderGraphBuilder);
				HDRenderPipeline.LightingOutput lightingOutput2 = default(HDRenderPipeline.LightingOutput);
				lightingOutput2.colorBuffer = deferredLightingPassData.colorBuffer;
				renderGraphBuilder.SetRenderFunc<HDRenderPipeline.DeferredLightingPassData>(delegate(HDRenderPipeline.DeferredLightingPassData data, RenderGraphContext context)
				{
					data.resources.colorBuffers = context.renderGraphPool.GetTempArray<RenderTargetIdentifier>(2);
					RenderTargetIdentifier[] colorBuffers = data.resources.colorBuffers;
					int num = 0;
					RenderGraphResourceRegistry resources = context.resources;
					RenderGraphResource renderGraphResource = data.colorBuffer;
					colorBuffers[num] = resources.GetTexture(in renderGraphResource);
					if (data.parameters.outputSplitLighting)
					{
						RenderTargetIdentifier[] colorBuffers2 = data.resources.colorBuffers;
						int num2 = 1;
						RenderGraphResourceRegistry resources2 = context.resources;
						renderGraphResource = data.sssDiffuseLightingBuffer;
						colorBuffers2[num2] = resources2.GetTexture(in renderGraphResource);
					}
					data.resources.depthStencilBuffer = context.resources.GetTexture(in data.depthBuffer);
					data.resources.depthTexture = context.resources.GetTexture(in data.depthTexture);
					for (int j = 0; j < data.gbufferCount; j++)
					{
						context.cmd.SetGlobalTexture(HDShaderIDs._GBufferTexture[j], context.resources.GetTexture(in data.gbuffer[j]));
					}
					if (!data.parameters.enableTile)
					{
						HDRenderPipeline.RenderPixelDeferredLighting(in data.parameters, in data.resources, context.cmd);
						return;
					}
					if (data.parameters.useComputeLightingEvaluation && !HDRenderPipeline.k_PreferFragment)
					{
						HDRenderPipeline.RenderComputeDeferredLighting(in data.parameters, in data.resources, context.cmd);
						return;
					}
					HDRenderPipeline.RenderComputeAsPixelDeferredLighting(in data.parameters, in data.resources, context.cmd);
				});
				lightingOutput = lightingOutput2;
			}
			return lightingOutput;
		}

		// Token: 0x06000372 RID: 882 RVA: 0x0001B924 File Offset: 0x00019B24
		private RenderGraphResource RenderSSR(RenderGraph renderGraph, HDCamera hdCamera, RenderGraphResource normalBuffer, RenderGraphResource motionVectorsBuffer, RenderGraphResource depthPyramid, RenderGraphResource stencilBuffer, RenderGraphResource clearCoatMask)
		{
			RenderGraphMutableResource renderGraphMutableResource = renderGraph.ImportTexture(TextureXR.GetBlackTexture(), HDShaderIDs._SsrLightingTexture);
			if (!hdCamera.frameSettings.IsEnabled(FrameSettingsField.SSR))
			{
				return renderGraphMutableResource;
			}
			HDRenderPipeline.RenderSSRPassData renderSSRPassData;
			RenderGraphResource renderGraphResource2;
			using (RenderGraphBuilder renderGraphBuilder = renderGraph.AddRenderPass<HDRenderPipeline.RenderSSRPassData>("Render SSR", out renderSSRPassData, null))
			{
				renderGraphBuilder.EnableAsyncCompute(hdCamera.frameSettings.SSRRunsAsync());
				RenderGraphMutableResource renderGraphMutableResource2 = renderGraph.ImportTexture(hdCamera.GetPreviousFrameRT(0), 0);
				renderSSRPassData.parameters = this.PrepareSSRParameters(hdCamera);
				renderSSRPassData.depthPyramid = renderGraphBuilder.ReadTexture(in depthPyramid);
				HDRenderPipeline.RenderSSRPassData renderSSRPassData2 = renderSSRPassData;
				RenderGraphResource renderGraphResource = renderGraphMutableResource2;
				renderSSRPassData2.colorPyramid = renderGraphBuilder.ReadTexture(in renderGraphResource);
				renderSSRPassData.stencilBuffer = renderGraphBuilder.ReadTexture(in stencilBuffer);
				renderSSRPassData.clearCoatMask = renderGraphBuilder.ReadTexture(in clearCoatMask);
				renderGraphBuilder.ReadTexture(in normalBuffer);
				renderGraphBuilder.ReadTexture(in motionVectorsBuffer);
				HDRenderPipeline.RenderSSRPassData renderSSRPassData3 = renderSSRPassData;
				RenderGraphMutableResource renderGraphMutableResource3 = renderGraph.CreateTexture(new TextureDesc(Vector2.one, true, true)
				{
					colorFormat = GraphicsFormat.R16G16_UNorm,
					clearBuffer = true,
					clearColor = Color.clear,
					enableRandomWrite = true,
					name = "SSR_Hit_Point_Texture"
				}, 0);
				renderSSRPassData3.hitPointsTexture = renderGraphBuilder.WriteTexture(in renderGraphMutableResource3);
				HDRenderPipeline.RenderSSRPassData renderSSRPassData4 = renderSSRPassData;
				renderGraphMutableResource3 = renderGraph.CreateTexture(new TextureDesc(Vector2.one, true, true)
				{
					colorFormat = GraphicsFormat.R16G16B16A16_SFloat,
					clearBuffer = true,
					clearColor = Color.clear,
					enableRandomWrite = true,
					name = "SSR_Lighting_Texture"
				}, HDShaderIDs._SsrLightingTexture);
				renderSSRPassData4.lightingTexture = renderGraphBuilder.WriteTexture(in renderGraphMutableResource3);
				renderGraphBuilder.SetRenderFunc<HDRenderPipeline.RenderSSRPassData>(delegate(HDRenderPipeline.RenderSSRPassData data, RenderGraphContext context)
				{
					RenderGraphResourceRegistry resources = context.resources;
					RTHandle texture = resources.GetTexture(in data.depthPyramid);
					RenderGraphResourceRegistry renderGraphResourceRegistry = resources;
					RenderGraphResource renderGraphResource3 = data.hitPointsTexture;
					RTHandle texture2 = renderGraphResourceRegistry.GetTexture(in renderGraphResource3);
					RTHandle texture3 = resources.GetTexture(in data.stencilBuffer);
					RTHandle texture4 = resources.GetTexture(in data.clearCoatMask);
					RTHandle texture5 = resources.GetTexture(in data.colorPyramid);
					RenderGraphResourceRegistry renderGraphResourceRegistry2 = resources;
					RenderGraphResource renderGraphResource4 = data.lightingTexture;
					HDRenderPipeline.RenderSSR(in data.parameters, texture, texture2, texture3, texture4, texture5, renderGraphResourceRegistry2.GetTexture(in renderGraphResource4), context.cmd, context.renderContext);
				});
				renderGraphResource2 = renderSSRPassData.lightingTexture;
			}
			if (!hdCamera.colorPyramidHistoryIsValid)
			{
				hdCamera.colorPyramidHistoryIsValid = true;
				renderGraphResource2 = renderGraphMutableResource;
			}
			this.PushFullScreenDebugTexture(renderGraph, renderGraphResource2, FullScreenDebugMode.ScreenSpaceReflections);
			return renderGraphResource2;
		}

		// Token: 0x06000373 RID: 883 RVA: 0x0001BB34 File Offset: 0x00019D34
		private RenderGraphResource RenderContactShadows(RenderGraph renderGraph, HDCamera hdCamera, RenderGraphResource depthTexture, int firstMipOffsetY)
		{
			if (!this.WillRenderContactShadow())
			{
				return renderGraph.ImportTexture(TextureXR.GetClearTexture(), HDShaderIDs._ContactShadowTexture);
			}
			HDRenderPipeline.RenderContactShadowPassData renderContactShadowPassData;
			RenderGraphResource renderGraphResource;
			using (RenderGraphBuilder renderGraphBuilder = renderGraph.AddRenderPass<HDRenderPipeline.RenderContactShadowPassData>("Contact Shadows", out renderContactShadowPassData, null))
			{
				renderGraphBuilder.EnableAsyncCompute(hdCamera.frameSettings.ContactShadowsRunAsync());
				bool flag = this.m_CurrentDebugDisplaySettings.data.fullScreenDebugMode == FullScreenDebugMode.ContactShadows;
				renderContactShadowPassData.parameters = this.PrepareContactShadowsParameters(hdCamera, (float)firstMipOffsetY);
				renderContactShadowPassData.lightLoopLightData = this.m_LightLoopLightData;
				renderContactShadowPassData.tileAndClusterData = this.m_TileAndClusterData;
				renderContactShadowPassData.depthTexture = renderGraphBuilder.ReadTexture(in depthTexture);
				renderContactShadowPassData.shadowManager = this.m_ShadowManager;
				HDRenderPipeline.RenderContactShadowPassData renderContactShadowPassData2 = renderContactShadowPassData;
				RenderGraphMutableResource renderGraphMutableResource = renderGraph.CreateTexture(new TextureDesc(Vector2.one, true, true)
				{
					colorFormat = GraphicsFormat.R32_UInt,
					enableRandomWrite = true,
					clearBuffer = flag,
					clearColor = Color.clear,
					name = "ContactShadowsBuffer"
				}, HDShaderIDs._ContactShadowTexture);
				renderContactShadowPassData2.contactShadowsTexture = renderGraphBuilder.WriteTexture(in renderGraphMutableResource);
				renderGraphResource = renderContactShadowPassData.contactShadowsTexture;
				renderGraphBuilder.SetRenderFunc<HDRenderPipeline.RenderContactShadowPassData>(delegate(HDRenderPipeline.RenderContactShadowPassData data, RenderGraphContext context)
				{
					RenderGraphResourceRegistry resources = context.resources;
					data.shadowManager.PushGlobalParameters(context.cmd);
					RenderGraphResourceRegistry renderGraphResourceRegistry = resources;
					RenderGraphResource renderGraphResource2 = data.contactShadowsTexture;
					HDRenderPipeline.RenderContactShadows(in data.parameters, renderGraphResourceRegistry.GetTexture(in renderGraphResource2), resources.GetTexture(in data.depthTexture), data.lightLoopLightData, data.tileAndClusterData, context.cmd);
				});
			}
			this.PushFullScreenDebugTexture(renderGraph, renderGraphResource, FullScreenDebugMode.ContactShadows);
			return renderGraphResource;
		}

		// Token: 0x06000374 RID: 884 RVA: 0x0001BC94 File Offset: 0x00019E94
		private RenderGraphResource VolumeVoxelizationPass(RenderGraph renderGraph, HDCamera hdCamera, ComputeBuffer visibleVolumeBoundsBuffer, ComputeBuffer visibleVolumeDataBuffer, ComputeBuffer bigTileLightListBuffer)
		{
			if (Fog.IsVolumetricFogEnabled(hdCamera))
			{
				HDRenderPipeline.VolumeVoxelizationPassData volumeVoxelizationPassData;
				using (RenderGraphBuilder renderGraphBuilder = renderGraph.AddRenderPass<HDRenderPipeline.VolumeVoxelizationPassData>("Volume Voxelization", out volumeVoxelizationPassData, null))
				{
					renderGraphBuilder.EnableAsyncCompute(hdCamera.frameSettings.VolumeVoxelizationRunsAsync());
					volumeVoxelizationPassData.parameters = this.PrepareVolumeVoxelizationParameters(hdCamera);
					volumeVoxelizationPassData.visibleVolumeBoundsBuffer = visibleVolumeBoundsBuffer;
					volumeVoxelizationPassData.visibleVolumeDataBuffer = visibleVolumeDataBuffer;
					volumeVoxelizationPassData.bigTileLightListBuffer = bigTileLightListBuffer;
					HDRenderPipeline.VolumeVoxelizationPassData volumeVoxelizationPassData2 = volumeVoxelizationPassData;
					RenderGraphMutableResource renderGraphMutableResource = renderGraph.CreateTexture(new TextureDesc(new ScaleFunc(this.ComputeVBufferResolutionXY), false, false)
					{
						dimension = TextureDimension.Tex3D,
						colorFormat = GraphicsFormat.R16G16B16A16_SFloat,
						enableRandomWrite = true,
						slices = HDRenderPipeline.ComputeVBufferSliceCount(this.volumetricLightingPreset),
						name = "VBufferDensity"
					}, 0);
					volumeVoxelizationPassData2.densityBuffer = renderGraphBuilder.WriteTexture(in renderGraphMutableResource);
					renderGraphBuilder.SetRenderFunc<HDRenderPipeline.VolumeVoxelizationPassData>(delegate(HDRenderPipeline.VolumeVoxelizationPassData data, RenderGraphContext ctx)
					{
						RenderGraphResourceRegistry resources = ctx.resources;
						RenderGraphResource renderGraphResource = data.densityBuffer;
						HDRenderPipeline.VolumeVoxelizationPass(in data.parameters, resources.GetTexture(in renderGraphResource), data.visibleVolumeBoundsBuffer, data.visibleVolumeDataBuffer, data.bigTileLightListBuffer, ctx.cmd);
					});
					return volumeVoxelizationPassData.densityBuffer;
				}
			}
			return default(RenderGraphResource);
		}

		// Token: 0x06000375 RID: 885 RVA: 0x0001BDB8 File Offset: 0x00019FB8
		private RenderGraphResource VolumetricLightingPass(RenderGraph renderGraph, HDCamera hdCamera, RenderGraphResource densityBuffer, ComputeBuffer bigTileLightListBuffer, ShadowResult shadowResult, int frameIndex)
		{
			if (Fog.IsVolumetricFogEnabled(hdCamera))
			{
				HDRenderPipeline.VolumetricLightingParameters volumetricLightingParameters = this.PrepareVolumetricLightingParameters(hdCamera, frameIndex);
				HDRenderPipeline.VolumetricLightingPassData volumetricLightingPassData;
				using (RenderGraphBuilder renderGraphBuilder = renderGraph.AddRenderPass<HDRenderPipeline.VolumetricLightingPassData>("Volumetric Lighting", out volumetricLightingPassData, null))
				{
					volumetricLightingPassData.parameters = volumetricLightingParameters;
					volumetricLightingPassData.bigTileLightListBuffer = bigTileLightListBuffer;
					volumetricLightingPassData.densityBuffer = renderGraphBuilder.ReadTexture(in densityBuffer);
					HDRenderPipeline.VolumetricLightingPassData volumetricLightingPassData2 = volumetricLightingPassData;
					RenderGraphMutableResource renderGraphMutableResource = renderGraph.CreateTexture(new TextureDesc(new ScaleFunc(this.ComputeVBufferResolutionXY), false, false)
					{
						dimension = TextureDimension.Tex3D,
						colorFormat = GraphicsFormat.R16G16B16A16_SFloat,
						enableRandomWrite = true,
						slices = HDRenderPipeline.ComputeVBufferSliceCount(this.volumetricLightingPreset),
						name = "VBufferIntegral"
					}, HDShaderIDs._VBufferLighting);
					volumetricLightingPassData2.lightingBuffer = renderGraphBuilder.WriteTexture(in renderGraphMutableResource);
					if (volumetricLightingPassData.parameters.enableReprojection)
					{
						HDRenderPipeline.VolumetricLightingPassData volumetricLightingPassData3 = volumetricLightingPassData;
						RenderGraphResource renderGraphResource = renderGraph.ImportTexture(hdCamera.GetPreviousFrameRT(1), 0);
						volumetricLightingPassData3.historyBuffer = renderGraphBuilder.ReadTexture(in renderGraphResource);
						HDRenderPipeline.VolumetricLightingPassData volumetricLightingPassData4 = volumetricLightingPassData;
						renderGraphMutableResource = renderGraph.ImportTexture(hdCamera.GetCurrentFrameRT(1), 0);
						volumetricLightingPassData4.feedbackBuffer = renderGraphBuilder.WriteTexture(in renderGraphMutableResource);
					}
					HDShadowManager.ReadShadowResult(shadowResult, renderGraphBuilder);
					renderGraphBuilder.SetRenderFunc<HDRenderPipeline.VolumetricLightingPassData>(delegate(HDRenderPipeline.VolumetricLightingPassData data, RenderGraphContext ctx)
					{
						RTHandle texture = ctx.resources.GetTexture(in data.densityBuffer);
						RenderGraphResourceRegistry resources = ctx.resources;
						RenderGraphResource renderGraphResource2 = data.lightingBuffer;
						RTHandle texture2 = resources.GetTexture(in renderGraphResource2);
						RTHandle rthandle = texture;
						RTHandle rthandle2 = texture2;
						RTHandle rthandle3 = (data.parameters.enableReprojection ? ctx.resources.GetTexture(in data.historyBuffer) : null);
						RTHandle rthandle4;
						if (!data.parameters.enableReprojection)
						{
							rthandle4 = null;
						}
						else
						{
							RenderGraphResourceRegistry resources2 = ctx.resources;
							renderGraphResource2 = data.feedbackBuffer;
							rthandle4 = resources2.GetTexture(in renderGraphResource2);
						}
						HDRenderPipeline.VolumetricLightingPass(in data.parameters, rthandle, rthandle2, rthandle3, rthandle4, data.bigTileLightListBuffer, ctx.cmd);
						if (data.parameters.filterVolume)
						{
							HDRenderPipeline.FilterVolumetricLighting(in data.parameters, texture, texture2, ctx.cmd);
						}
					});
					if (volumetricLightingParameters.enableReprojection)
					{
						hdCamera.volumetricHistoryIsValid = true;
					}
					return volumetricLightingPassData.lightingBuffer;
				}
			}
			return renderGraph.ImportTexture(HDUtils.clearTexture3DRTH, 0);
		}

		// Token: 0x06000376 RID: 886 RVA: 0x0001BF4C File Offset: 0x0001A14C
		void IDataProvider.FirstInitScene(StageRuntimeInterface SRI)
		{
			Camera camera = SRI.camera;
			camera.allowHDR = true;
			HDAdditionalCameraData hdadditionalCameraData = camera.gameObject.AddComponent<HDAdditionalCameraData>();
			hdadditionalCameraData.clearColorMode = HDAdditionalCameraData.ClearColorMode.Color;
			hdadditionalCameraData.clearDepth = true;
			hdadditionalCameraData.backgroundColorHDR = camera.backgroundColor;
			hdadditionalCameraData.volumeAnchorOverride = camera.transform;
			hdadditionalCameraData.volumeLayerMask = int.MinValue;
			hdadditionalCameraData.customRenderingSettings = true;
			hdadditionalCameraData.renderingPathCustomFrameSettings.SetEnabled(FrameSettingsField.SSR, false);
			hdadditionalCameraData.hasPersistentHistory = true;
			HDAdditionalLightData hdadditionalLightData = SRI.sunLight.gameObject.AddComponent<HDAdditionalLightData>();
			hdadditionalLightData.intensity = 0f;
			hdadditionalLightData.SetShadowResolution(2048);
			GameObject gameObject = SRI.AddGameObject(true);
			gameObject.name = "StageVolume";
			Volume volume = gameObject.AddComponent<Volume>();
			volume.isGlobal = true;
			volume.priority = float.MaxValue;
			volume.enabled = false;
			SRI.SRPData = new HDRenderPipeline.LookDevDataForHDRP
			{
				additionalCameraData = null,
				additionalLightData = null,
				visualEnvironment = null,
				sky = null,
				volume = null
			};
		}

		// Token: 0x06000377 RID: 887 RVA: 0x0001C054 File Offset: 0x0001A254
		void IDataProvider.UpdateSky(Camera camera, Sky sky, StageRuntimeInterface SRI)
		{
			HDRenderPipeline.LookDevDataForHDRP lookDevDataForHDRP = (HDRenderPipeline.LookDevDataForHDRP)SRI.SRPData;
			if (sky.cubemap == null)
			{
				lookDevDataForHDRP.visualEnvironment.skyType.Override(0);
				lookDevDataForHDRP.additionalCameraData.clearColorMode = HDAdditionalCameraData.ClearColorMode.Color;
				return;
			}
			lookDevDataForHDRP.visualEnvironment.skyType.Override(1);
			lookDevDataForHDRP.sky.hdriSky.Override(sky.cubemap);
			lookDevDataForHDRP.sky.rotation.Override(sky.longitudeOffset);
			lookDevDataForHDRP.additionalCameraData.clearColorMode = HDAdditionalCameraData.ClearColorMode.Sky;
		}

		// Token: 0x06000378 RID: 888 RVA: 0x0001C0E2 File Offset: 0x0001A2E2
		void IDataProvider.OnBeginRendering(StageRuntimeInterface SRI)
		{
			((HDRenderPipeline.LookDevDataForHDRP)SRI.SRPData).volume.enabled = true;
		}

		// Token: 0x06000379 RID: 889 RVA: 0x0001C0FA File Offset: 0x0001A2FA
		void IDataProvider.OnEndRendering(StageRuntimeInterface SRI)
		{
			((HDRenderPipeline.LookDevDataForHDRP)SRI.SRPData).volume.enabled = false;
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x0600037A RID: 890 RVA: 0x0001C112 File Offset: 0x0001A312
		IEnumerable<string> IDataProvider.supportedDebugModes
		{
			get
			{
				return new string[] { "Albedo", "Normal", "Smoothness", "AmbientOcclusion", "Metal", "Specular", "Alpha" };
			}
		}

		// Token: 0x0600037B RID: 891 RVA: 0x0001C152 File Offset: 0x0001A352
		void IDataProvider.UpdateDebugMode(int debugIndex)
		{
			this.debugDisplaySettings.SetDebugViewCommonMaterialProperty(debugIndex + MaterialSharedProperty.Albedo);
		}

		// Token: 0x0600037C RID: 892 RVA: 0x0001C164 File Offset: 0x0001A364
		void IDataProvider.GetShadowMask(ref RenderTexture output, StageRuntimeInterface SRI)
		{
			HDRenderPipeline.LookDevDataForHDRP lookDevDataForHDRP = (HDRenderPipeline.LookDevDataForHDRP)SRI.SRPData;
			Color backgroundColorHDR = lookDevDataForHDRP.additionalCameraData.backgroundColorHDR;
			HDAdditionalCameraData.ClearColorMode clearColorMode = lookDevDataForHDRP.additionalCameraData.clearColorMode;
			lookDevDataForHDRP.additionalCameraData.backgroundColorHDR = Color.white;
			lookDevDataForHDRP.additionalCameraData.clearColorMode = HDAdditionalCameraData.ClearColorMode.Color;
			lookDevDataForHDRP.additionalLightData.intensity = 1f;
			this.debugDisplaySettings.SetShadowDebugMode(ShadowMapDebugMode.SingleShadow);
			SRI.camera.targetTexture = output;
			SRI.camera.Render();
			this.debugDisplaySettings.SetShadowDebugMode(ShadowMapDebugMode.None);
			lookDevDataForHDRP.additionalLightData.intensity = 0f;
			lookDevDataForHDRP.additionalCameraData.backgroundColorHDR = backgroundColorHDR;
			lookDevDataForHDRP.additionalCameraData.clearColorMode = clearColorMode;
		}

		// Token: 0x0600037D RID: 893 RVA: 0x0001C217 File Offset: 0x0001A417
		private Vector2Int ComputeDepthBufferMipChainSize(Vector2Int screenSize)
		{
			this.m_DepthBufferMipChainInfo.ComputePackedMipChainInfo(screenSize);
			return this.m_DepthBufferMipChainInfo.textureSize;
		}

		// Token: 0x0600037E RID: 894 RVA: 0x0001C230 File Offset: 0x0001A430
		private void InitializePrepass(HDRenderPipelineAsset hdAsset)
		{
			this.m_DepthResolveMaterial = CoreUtils.CreateEngineMaterial(this.asset.renderPipelineResources.shaders.depthValuesPS);
			this.m_GBufferOutput = default(HDRenderPipeline.GBufferOutput);
			this.m_GBufferOutput.mrt = new RenderGraphResource[RenderGraph.kMaxMRTCount];
			this.m_DBufferOutput = default(HDRenderPipeline.DBufferOutput);
			this.m_DBufferOutput.mrt = new RenderGraphResource[4];
			this.m_DepthBufferMipChainInfo = default(HDUtils.PackedMipChainInfo);
			this.m_DepthBufferMipChainInfo.Allocate();
		}

		// Token: 0x0600037F RID: 895 RVA: 0x0001C2B2 File Offset: 0x0001A4B2
		private void CleanupPrepass()
		{
			CoreUtils.Destroy(this.m_DepthResolveMaterial);
		}

		// Token: 0x06000380 RID: 896 RVA: 0x0001C2BF File Offset: 0x0001A4BF
		private bool NeedClearGBuffer()
		{
			return this.m_CurrentDebugDisplaySettings.IsDebugDisplayEnabled();
		}

		// Token: 0x06000381 RID: 897 RVA: 0x0001C2CC File Offset: 0x0001A4CC
		private HDUtils.PackedMipChainInfo GetDepthBufferMipChainInfo()
		{
			return this.m_DepthBufferMipChainInfo;
		}

		// Token: 0x06000382 RID: 898 RVA: 0x0001C2D4 File Offset: 0x0001A4D4
		private RenderGraphMutableResource CreateDepthBuffer(RenderGraph renderGraph, bool msaa)
		{
			TextureDesc textureDesc = new TextureDesc(Vector2.one, true, true)
			{
				depthBufferBits = DepthBits.Depth32,
				bindTextureMS = msaa,
				enableMSAA = msaa,
				clearBuffer = true,
				name = (msaa ? "CameraDepthStencilMSAA" : "CameraDepthStencil")
			};
			return renderGraph.CreateTexture(textureDesc, 0);
		}

		// Token: 0x06000383 RID: 899 RVA: 0x0001C330 File Offset: 0x0001A530
		private RenderGraphMutableResource CreateNormalBuffer(RenderGraph renderGraph, bool msaa)
		{
			TextureDesc textureDesc = new TextureDesc(Vector2.one, true, true)
			{
				colorFormat = GraphicsFormat.R8G8B8A8_UNorm,
				clearBuffer = this.NeedClearGBuffer(),
				clearColor = Color.black,
				bindTextureMS = msaa,
				enableMSAA = msaa,
				enableRandomWrite = !msaa,
				name = (msaa ? "NormalBufferMSAA" : "NormalBuffer")
			};
			return renderGraph.CreateTexture(textureDesc, msaa ? HDShaderIDs._NormalTextureMS : HDShaderIDs._NormalBufferTexture);
		}

		// Token: 0x06000384 RID: 900 RVA: 0x0001C3B8 File Offset: 0x0001A5B8
		private RenderGraphMutableResource CreateMotionVectorBuffer(RenderGraph renderGraph, bool msaa, bool clear)
		{
			TextureDesc textureDesc = new TextureDesc(Vector2.one, true, true)
			{
				colorFormat = Builtin.GetMotionVectorFormat(),
				bindTextureMS = msaa,
				enableMSAA = msaa,
				clearBuffer = clear,
				clearColor = Color.clear,
				name = (msaa ? "Motion Vectors MSAA" : "Motion Vectors")
			};
			return renderGraph.CreateTexture(textureDesc, HDShaderIDs._CameraMotionVectorsTexture);
		}

		// Token: 0x06000385 RID: 901 RVA: 0x0001C428 File Offset: 0x0001A628
		private HDRenderPipeline.PrepassOutput RenderPrepass(RenderGraph renderGraph, RenderGraphMutableResource sssBuffer, CullingResults cullingResults, HDCamera hdCamera)
		{
			this.m_IsDepthBufferCopyValid = false;
			HDRenderPipeline.PrepassOutput prepassOutput = default(HDRenderPipeline.PrepassOutput);
			prepassOutput.gbuffer = this.m_GBufferOutput;
			prepassOutput.dbuffer = this.m_DBufferOutput;
			bool flag = hdCamera.frameSettings.IsEnabled(FrameSettingsField.MSAA);
			bool flag2 = hdCamera.camera.cameraType == CameraType.SceneView && !hdCamera.animateMaterials;
			prepassOutput.motionVectorsBuffer = this.CreateMotionVectorBuffer(renderGraph, flag, flag2);
			prepassOutput.depthBuffer = this.CreateDepthBuffer(renderGraph, flag);
			this.RenderXROcclusionMeshes(renderGraph, hdCamera, prepassOutput.depthBuffer);
			using (new XRSinglePassScope(renderGraph, hdCamera))
			{
				bool flag3 = this.RenderDepthPrepass(renderGraph, cullingResults, hdCamera, ref prepassOutput);
				if (!flag3)
				{
					this.RenderObjectsMotionVectors(renderGraph, cullingResults, hdCamera, in prepassOutput);
				}
				this.ResolvePrepassBuffers(renderGraph, hdCamera, ref prepassOutput);
				this.RenderDBuffer(renderGraph, hdCamera, ref prepassOutput, cullingResults);
				this.RenderGBuffer(renderGraph, sssBuffer, ref prepassOutput, cullingResults, hdCamera);
				this.DecalNormalPatch(renderGraph, hdCamera, ref prepassOutput);
				this.GenerateDepthPyramid(renderGraph, hdCamera, ref prepassOutput);
				if (flag3)
				{
					this.RenderObjectsMotionVectors(renderGraph, cullingResults, hdCamera, in prepassOutput);
				}
				this.RenderCameraMotionVectors(renderGraph, hdCamera, prepassOutput.depthPyramidTexture, prepassOutput.resolvedMotionVectorsBuffer);
				this.ResolveStencilBufferIfNeeded(renderGraph, hdCamera, ref prepassOutput);
			}
			return prepassOutput;
		}

		// Token: 0x06000386 RID: 902 RVA: 0x0001C570 File Offset: 0x0001A770
		private bool RenderDepthPrepass(RenderGraph renderGraph, CullingResults cull, HDCamera hdCamera, ref HDRenderPipeline.PrepassOutput output)
		{
			HDRenderPipeline.DepthPrepassParameters depthPrepassParameters = this.PrepareDepthPrepass(cull, hdCamera);
			bool flag = hdCamera.frameSettings.IsEnabled(FrameSettingsField.MSAA);
			HDRenderPipeline.DepthPrepassData passData;
			using (RenderGraphBuilder renderGraphBuilder = renderGraph.AddRenderPass<HDRenderPipeline.DepthPrepassData>(depthPrepassParameters.passName, out passData, ProfilingSampler.Get<HDProfileId>(depthPrepassParameters.profilingId)))
			{
				passData.frameSettings = hdCamera.frameSettings;
				passData.msaaEnabled = flag;
				passData.hasDepthOnlyPrepass = depthPrepassParameters.hasDepthOnlyPass;
				passData.renderRayTracingPrepass = depthPrepassParameters.renderRayTracingPrepass;
				passData.depthBuffer = renderGraphBuilder.UseDepthBuffer(in output.depthBuffer, DepthAccess.ReadWrite);
				HDRenderPipeline.DepthPrepassData passData7 = passData;
				RenderGraphMutableResource renderGraphMutableResource = this.CreateNormalBuffer(renderGraph, flag);
				passData7.normalBuffer = renderGraphBuilder.WriteTexture(in renderGraphMutableResource);
				if (flag)
				{
					HDRenderPipeline.DepthPrepassData passData2 = passData;
					renderGraphMutableResource = renderGraph.CreateTexture(new TextureDesc(Vector2.one, true, true)
					{
						colorFormat = GraphicsFormat.R32_SFloat,
						clearBuffer = true,
						clearColor = Color.black,
						bindTextureMS = true,
						enableMSAA = true,
						name = "DepthAsColorMSAA"
					}, HDShaderIDs._DepthTextureMS);
					passData2.depthAsColorBuffer = renderGraphBuilder.WriteTexture(in renderGraphMutableResource);
				}
				RenderGraphResource renderGraphResource;
				if (passData.hasDepthOnlyPrepass)
				{
					HDRenderPipeline.DepthPrepassData passData3 = passData;
					renderGraphResource = renderGraph.CreateRendererList(in depthPrepassParameters.depthOnlyRendererListDesc);
					passData3.rendererListDepthOnly = renderGraphBuilder.UseRendererList(in renderGraphResource);
				}
				HDRenderPipeline.DepthPrepassData passData4 = passData;
				renderGraphResource = renderGraph.CreateRendererList(in depthPrepassParameters.mrtRendererListDesc);
				passData4.rendererListMRT = renderGraphBuilder.UseRendererList(in renderGraphResource);
				if (hdCamera.frameSettings.IsEnabled(FrameSettingsField.RayTracing))
				{
					HDRenderPipeline.DepthPrepassData passData5 = passData;
					renderGraphResource = renderGraph.CreateRendererList(in depthPrepassParameters.rayTracingOpaqueRLDesc);
					passData5.renderListRayTracingOpaque = renderGraphBuilder.UseRendererList(in renderGraphResource);
					HDRenderPipeline.DepthPrepassData passData6 = passData;
					renderGraphResource = renderGraph.CreateRendererList(in depthPrepassParameters.rayTracingTransparentRLDesc);
					passData6.renderListRayTracingTransparent = renderGraphBuilder.UseRendererList(in renderGraphResource);
				}
				output.depthBuffer = passData.depthBuffer;
				output.depthAsColor = passData.depthAsColorBuffer;
				output.normalBuffer = passData.normalBuffer;
				renderGraphBuilder.SetRenderFunc<HDRenderPipeline.DepthPrepassData>(delegate(HDRenderPipeline.DepthPrepassData data, RenderGraphContext context)
				{
					RenderTargetIdentifier[] tempArray = context.renderGraphPool.GetTempArray<RenderTargetIdentifier>(data.msaaEnabled ? 2 : 1);
					RenderTargetIdentifier[] array = tempArray;
					int num = 0;
					RenderGraphResourceRegistry resources = context.resources;
					RenderGraphResource renderGraphResource2 = data.normalBuffer;
					array[num] = resources.GetTexture(in renderGraphResource2);
					if (data.msaaEnabled)
					{
						RenderTargetIdentifier[] array2 = tempArray;
						int num2 = 1;
						RenderGraphResourceRegistry resources2 = context.resources;
						renderGraphResource2 = data.depthAsColorBuffer;
						array2[num2] = resources2.GetTexture(in renderGraphResource2);
					}
					bool flag2 = passData.frameSettings.IsEnabled(FrameSettingsField.RayTracing);
					ScriptableRenderContext renderContext = context.renderContext;
					CommandBuffer cmd = context.cmd;
					FrameSettings frameSettings = data.frameSettings;
					RenderTargetIdentifier[] array3 = tempArray;
					RenderGraphResourceRegistry resources3 = context.resources;
					renderGraphResource2 = data.depthBuffer;
					RTHandle texture = resources3.GetTexture(in renderGraphResource2);
					RendererList rendererList = (data.hasDepthOnlyPrepass ? context.resources.GetRendererList(in data.rendererListDepthOnly) : RendererList.nullRendererList);
					RendererList rendererList2 = context.resources.GetRendererList(in data.rendererListMRT);
					bool hasDepthOnlyPrepass = data.hasDepthOnlyPrepass;
					RendererList rendererList3;
					RendererList rendererList4;
					if (!flag2)
					{
						rendererList3 = default(RendererList);
						rendererList4 = rendererList3;
					}
					else
					{
						rendererList4 = context.resources.GetRendererList(in data.renderListRayTracingOpaque);
					}
					rendererList3 = rendererList4;
					RendererList rendererList5;
					RendererList rendererList6;
					if (!flag2)
					{
						rendererList5 = default(RendererList);
						rendererList6 = rendererList5;
					}
					else
					{
						rendererList6 = context.resources.GetRendererList(in data.renderListRayTracingTransparent);
					}
					rendererList5 = rendererList6;
					HDRenderPipeline.RenderDepthPrepass(renderContext, cmd, frameSettings, array3, texture, in rendererList, in rendererList2, hasDepthOnlyPrepass, in rendererList3, in rendererList5, data.renderRayTracingPrepass);
				});
			}
			return depthPrepassParameters.shouldRenderMotionVectorAfterGBuffer;
		}

		// Token: 0x06000387 RID: 903 RVA: 0x0001C7C8 File Offset: 0x0001A9C8
		private void RenderObjectsMotionVectors(RenderGraph renderGraph, CullingResults cull, HDCamera hdCamera, in HDRenderPipeline.PrepassOutput output)
		{
			if (!hdCamera.frameSettings.IsEnabled(FrameSettingsField.ObjectMotionVectors))
			{
				return;
			}
			HDRenderPipeline.ObjectMotionVectorsPassData objectMotionVectorsPassData;
			using (RenderGraphBuilder renderGraphBuilder = renderGraph.AddRenderPass<HDRenderPipeline.ObjectMotionVectorsPassData>("Objects Motion Vectors Rendering", out objectMotionVectorsPassData, ProfilingSampler.Get<HDProfileId>(HDProfileId.ObjectsMotionVector)))
			{
				hdCamera.camera.depthTextureMode |= DepthTextureMode.Depth | DepthTextureMode.MotionVectors;
				bool flag = hdCamera.frameSettings.IsEnabled(FrameSettingsField.MSAA);
				objectMotionVectorsPassData.frameSettings = hdCamera.frameSettings;
				objectMotionVectorsPassData.depthBuffer = renderGraphBuilder.UseDepthBuffer(in output.depthBuffer, DepthAccess.ReadWrite);
				objectMotionVectorsPassData.motionVectorsBuffer = renderGraphBuilder.UseColorBuffer(in output.motionVectorsBuffer, 0);
				objectMotionVectorsPassData.normalBuffer = renderGraphBuilder.UseColorBuffer(in output.normalBuffer, 1);
				if (flag)
				{
					objectMotionVectorsPassData.depthAsColorMSAABuffer = renderGraphBuilder.UseColorBuffer(in output.depthAsColor, 2);
				}
				HDRenderPipeline.ObjectMotionVectorsPassData objectMotionVectorsPassData2 = objectMotionVectorsPassData;
				RendererListDesc rendererListDesc = HDRenderPipeline.CreateOpaqueRendererListDesc(cull, hdCamera.camera, HDShaderPassNames.s_MotionVectorsName, PerObjectData.MotionVectors, null, null, null, false);
				RenderGraphResource renderGraphResource = renderGraph.CreateRendererList(in rendererListDesc);
				objectMotionVectorsPassData2.rendererList = renderGraphBuilder.UseRendererList(in renderGraphResource);
				renderGraphBuilder.SetRenderFunc<HDRenderPipeline.ObjectMotionVectorsPassData>(delegate(HDRenderPipeline.ObjectMotionVectorsPassData data, RenderGraphContext context)
				{
					RendererList rendererList = context.resources.GetRendererList(in data.rendererList);
					HDRenderPipeline.DrawOpaqueRendererList(in context, in data.frameSettings, in rendererList);
				});
			}
		}

		// Token: 0x06000388 RID: 904 RVA: 0x0001C90C File Offset: 0x0001AB0C
		private void SetupGBufferTargets(RenderGraph renderGraph, HDCamera hdCamera, HDRenderPipeline.GBufferPassData passData, RenderGraphMutableResource sssBuffer, ref HDRenderPipeline.PrepassOutput prepassOutput, FrameSettings frameSettings, RenderGraphBuilder builder)
		{
			bool flag = this.NeedClearGBuffer();
			bool flag2 = frameSettings.IsEnabled(FrameSettingsField.LightLayers);
			bool flag3 = frameSettings.IsEnabled(FrameSettingsField.Shadowmask);
			passData.depthBuffer = builder.UseDepthBuffer(in prepassOutput.depthBuffer, DepthAccess.ReadWrite);
			passData.gbufferRT[0] = builder.UseColorBuffer(in sssBuffer, 0);
			passData.gbufferRT[1] = builder.UseColorBuffer(in prepassOutput.normalBuffer, 1);
			bool flag4 = flag || hdCamera.frameSettings.IsEnabled(FrameSettingsField.SSR);
			RenderGraphMutableResource[] gbufferRT = passData.gbufferRT;
			int num = 2;
			TextureDesc textureDesc = new TextureDesc(Vector2.one, true, true)
			{
				colorFormat = GraphicsFormat.R8G8B8A8_UNorm,
				clearBuffer = flag4,
				clearColor = Color.clear,
				name = "GBuffer2"
			};
			RenderGraphMutableResource renderGraphMutableResource = renderGraph.CreateTexture(textureDesc, HDShaderIDs._GBufferTexture[2]);
			gbufferRT[num] = builder.UseColorBuffer(in renderGraphMutableResource, 2);
			RenderGraphMutableResource[] gbufferRT2 = passData.gbufferRT;
			int num2 = 3;
			textureDesc = new TextureDesc(Vector2.one, true, true)
			{
				colorFormat = Builtin.GetLightingBufferFormat(),
				clearBuffer = flag,
				clearColor = Color.clear,
				name = "GBuffer3"
			};
			renderGraphMutableResource = renderGraph.CreateTexture(textureDesc, HDShaderIDs._GBufferTexture[3]);
			gbufferRT2[num2] = builder.UseColorBuffer(in renderGraphMutableResource, 3);
			int num3 = 4;
			if (flag2)
			{
				RenderGraphMutableResource[] gbufferRT3 = passData.gbufferRT;
				int num4 = num3;
				textureDesc = new TextureDesc(Vector2.one, true, true)
				{
					colorFormat = GraphicsFormat.R8G8B8A8_UNorm,
					clearBuffer = flag,
					clearColor = Color.clear,
					name = "LightLayers"
				};
				renderGraphMutableResource = renderGraph.CreateTexture(textureDesc, HDShaderIDs._LightLayersTexture);
				gbufferRT3[num4] = builder.UseColorBuffer(in renderGraphMutableResource, num3);
				num3++;
			}
			if (flag3)
			{
				RenderGraphMutableResource[] gbufferRT4 = passData.gbufferRT;
				int num5 = num3;
				textureDesc = new TextureDesc(Vector2.one, true, true)
				{
					colorFormat = Builtin.GetShadowMaskBufferFormat(),
					clearBuffer = flag,
					clearColor = Color.clear,
					name = "ShadowMasks"
				};
				renderGraphMutableResource = renderGraph.CreateTexture(textureDesc, HDShaderIDs._ShadowMaskTexture);
				gbufferRT4[num5] = builder.UseColorBuffer(in renderGraphMutableResource, num3);
				num3++;
			}
			prepassOutput.gbuffer.gBufferCount = num3;
			for (int i = 0; i < num3; i++)
			{
				prepassOutput.gbuffer.mrt[i] = passData.gbufferRT[i];
			}
		}

		// Token: 0x06000389 RID: 905 RVA: 0x0001CB5C File Offset: 0x0001AD5C
		private void RenderGBuffer(RenderGraph renderGraph, RenderGraphMutableResource sssBuffer, ref HDRenderPipeline.PrepassOutput prepassOutput, CullingResults cull, HDCamera hdCamera)
		{
			if (hdCamera.frameSettings.litShaderMode != LitShaderMode.Deferred)
			{
				prepassOutput.gbuffer.gBufferCount = 0;
				return;
			}
			HDRenderPipeline.GBufferPassData gbufferPassData;
			using (RenderGraphBuilder renderGraphBuilder = renderGraph.AddRenderPass<HDRenderPipeline.GBufferPassData>("GBuffer", out gbufferPassData, ProfilingSampler.Get<HDProfileId>(HDProfileId.GBuffer)))
			{
				FrameSettings frameSettings = hdCamera.frameSettings;
				gbufferPassData.frameSettings = frameSettings;
				this.SetupGBufferTargets(renderGraph, hdCamera, gbufferPassData, sssBuffer, ref prepassOutput, frameSettings, renderGraphBuilder);
				HDRenderPipeline.GBufferPassData gbufferPassData2 = gbufferPassData;
				RendererListDesc rendererListDesc = HDRenderPipeline.CreateOpaqueRendererListDesc(cull, hdCamera.camera, HDShaderPassNames.s_GBufferName, this.m_CurrentRendererConfigurationBakedLighting, null, null, null, false);
				RenderGraphResource renderGraphResource = renderGraph.CreateRendererList(in rendererListDesc);
				gbufferPassData2.rendererList = renderGraphBuilder.UseRendererList(in renderGraphResource);
				HDRenderPipeline.ReadDBuffer(prepassOutput.dbuffer, renderGraphBuilder);
				renderGraphBuilder.SetRenderFunc<HDRenderPipeline.GBufferPassData>(delegate(HDRenderPipeline.GBufferPassData data, RenderGraphContext context)
				{
					RendererList rendererList = context.resources.GetRendererList(in data.rendererList);
					HDRenderPipeline.DrawOpaqueRendererList(in context, in data.frameSettings, in rendererList);
				});
			}
		}

		// Token: 0x0600038A RID: 906 RVA: 0x0001CC58 File Offset: 0x0001AE58
		private void ResolvePrepassBuffers(RenderGraph renderGraph, HDCamera hdCamera, ref HDRenderPipeline.PrepassOutput output)
		{
			if (!hdCamera.frameSettings.IsEnabled(FrameSettingsField.MSAA))
			{
				output.resolvedNormalBuffer = output.normalBuffer;
				output.resolvedDepthBuffer = output.depthBuffer;
				output.resolvedMotionVectorsBuffer = output.motionVectorsBuffer;
				return;
			}
			HDRenderPipeline.ResolvePrepassData resolvePrepassData;
			using (RenderGraphBuilder renderGraphBuilder = renderGraph.AddRenderPass<HDRenderPipeline.ResolvePrepassData>("Resolve Prepass MSAA", out resolvePrepassData, null))
			{
				RenderGraphMutableResource renderGraphMutableResource = renderGraph.CreateTexture(new TextureDesc(Vector2.one, true, true)
				{
					colorFormat = GraphicsFormat.R32G32B32A32_SFloat,
					name = "DepthValuesBuffer"
				}, 0);
				resolvePrepassData.depthResolveMaterial = this.m_DepthResolveMaterial;
				resolvePrepassData.depthResolvePassIndex = HDRenderPipeline.SampleCountToPassIndex(this.m_MSAASamples);
				HDRenderPipeline.ResolvePrepassData resolvePrepassData2 = resolvePrepassData;
				RenderGraphMutableResource renderGraphMutableResource2 = this.CreateDepthBuffer(renderGraph, false);
				resolvePrepassData2.depthBuffer = renderGraphBuilder.UseDepthBuffer(in renderGraphMutableResource2, DepthAccess.Write);
				resolvePrepassData.depthValuesBuffer = renderGraphBuilder.UseColorBuffer(in renderGraphMutableResource, 0);
				HDRenderPipeline.ResolvePrepassData resolvePrepassData3 = resolvePrepassData;
				renderGraphMutableResource2 = this.CreateNormalBuffer(renderGraph, false);
				resolvePrepassData3.normalBuffer = renderGraphBuilder.UseColorBuffer(in renderGraphMutableResource2, 1);
				HDRenderPipeline.ResolvePrepassData resolvePrepassData4 = resolvePrepassData;
				renderGraphMutableResource2 = this.CreateMotionVectorBuffer(renderGraph, false, false);
				resolvePrepassData4.motionVectorsBuffer = renderGraphBuilder.UseColorBuffer(in renderGraphMutableResource2, 2);
				HDRenderPipeline.ResolvePrepassData resolvePrepassData5 = resolvePrepassData;
				RenderGraphResource renderGraphResource = output.normalBuffer;
				resolvePrepassData5.normalBufferMSAA = renderGraphBuilder.ReadTexture(in renderGraphResource);
				HDRenderPipeline.ResolvePrepassData resolvePrepassData6 = resolvePrepassData;
				renderGraphResource = output.depthAsColor;
				resolvePrepassData6.depthAsColorBufferMSAA = renderGraphBuilder.ReadTexture(in renderGraphResource);
				HDRenderPipeline.ResolvePrepassData resolvePrepassData7 = resolvePrepassData;
				renderGraphResource = output.motionVectorsBuffer;
				resolvePrepassData7.motionVectorBufferMSAA = renderGraphBuilder.ReadTexture(in renderGraphResource);
				output.resolvedNormalBuffer = resolvePrepassData.normalBuffer;
				output.resolvedDepthBuffer = resolvePrepassData.depthBuffer;
				output.resolvedMotionVectorsBuffer = resolvePrepassData.motionVectorsBuffer;
				output.depthValuesMSAA = resolvePrepassData.depthValuesBuffer;
				renderGraphBuilder.SetRenderFunc<HDRenderPipeline.ResolvePrepassData>(delegate(HDRenderPipeline.ResolvePrepassData data, RenderGraphContext context)
				{
					context.cmd.DrawProcedural(Matrix4x4.identity, data.depthResolveMaterial, data.depthResolvePassIndex, MeshTopology.Triangles, 3, 1);
				});
			}
		}

		// Token: 0x0600038B RID: 907 RVA: 0x0001CE2C File Offset: 0x0001B02C
		private void CopyDepthBufferIfNeeded(RenderGraph renderGraph, HDCamera hdCamera, ref HDRenderPipeline.PrepassOutput output)
		{
			if (!this.m_IsDepthBufferCopyValid)
			{
				HDRenderPipeline.CopyDepthPassData copyDepthPassData;
				using (RenderGraphBuilder renderGraphBuilder = renderGraph.AddRenderPass<HDRenderPipeline.CopyDepthPassData>("Copy depth buffer", out copyDepthPassData, ProfilingSampler.Get<HDProfileId>(HDProfileId.CopyDepthBuffer)))
				{
					HDRenderPipeline.CopyDepthPassData copyDepthPassData2 = copyDepthPassData;
					RenderGraphResource renderGraphResource = output.resolvedDepthBuffer;
					copyDepthPassData2.inputDepth = renderGraphBuilder.ReadTexture(in renderGraphResource);
					HDRenderPipeline.CopyDepthPassData copyDepthPassData3 = copyDepthPassData;
					RenderGraphMutableResource renderGraphMutableResource = renderGraph.CreateTexture(new TextureDesc(new ScaleFunc(this.ComputeDepthBufferMipChainSize), true, true)
					{
						colorFormat = GraphicsFormat.R32_SFloat,
						enableRandomWrite = true,
						name = "CameraDepthBufferMipChain"
					}, HDShaderIDs._CameraDepthTexture);
					copyDepthPassData3.outputDepth = renderGraphBuilder.WriteTexture(in renderGraphMutableResource);
					copyDepthPassData.GPUCopy = this.m_GPUCopy;
					copyDepthPassData.width = hdCamera.actualWidth;
					copyDepthPassData.height = hdCamera.actualHeight;
					output.depthPyramidTexture = copyDepthPassData.outputDepth;
					renderGraphBuilder.SetRenderFunc<HDRenderPipeline.CopyDepthPassData>(delegate(HDRenderPipeline.CopyDepthPassData data, RenderGraphContext context)
					{
						RenderGraphResourceRegistry resources = context.resources;
						GPUCopy gpucopy = data.GPUCopy;
						CommandBuffer cmd = context.cmd;
						RTHandle texture = resources.GetTexture(in data.inputDepth);
						RenderGraphResourceRegistry renderGraphResourceRegistry = resources;
						RenderGraphResource renderGraphResource2 = data.outputDepth;
						gpucopy.SampleCopyChannel_xyzw2x(cmd, texture, renderGraphResourceRegistry.GetTexture(in renderGraphResource2), new RectInt(0, 0, data.width, data.height));
					});
				}
				this.m_IsDepthBufferCopyValid = true;
			}
		}

		// Token: 0x0600038C RID: 908 RVA: 0x0001CF40 File Offset: 0x0001B140
		private void ResolveStencilBufferIfNeeded(RenderGraph renderGraph, HDCamera hdCamera, ref HDRenderPipeline.PrepassOutput output)
		{
			HDRenderPipeline.ResolveStencilPassData resolveStencilPassData;
			using (RenderGraphBuilder renderGraphBuilder = renderGraph.AddRenderPass<HDRenderPipeline.ResolveStencilPassData>("Resolve Stencil", out resolveStencilPassData, ProfilingSampler.Get<HDProfileId>(HDProfileId.ResolveStencilBuffer)))
			{
				resolveStencilPassData.inputDepth = output.depthBuffer;
				resolveStencilPassData.coarseStencilBuffer = this.m_SharedRTManager.GetCoarseStencilBuffer();
				HDRenderPipeline.ResolveStencilPassData resolveStencilPassData2 = resolveStencilPassData;
				RenderGraphMutableResource renderGraphMutableResource = renderGraph.CreateTexture(new TextureDesc(Vector2.one, true, true)
				{
					colorFormat = GraphicsFormat.R8G8_UInt,
					enableRandomWrite = true,
					name = "StencilBufferResolved"
				}, 0);
				resolveStencilPassData2.resolvedStencil = renderGraphBuilder.WriteTexture(in renderGraphMutableResource);
				renderGraphBuilder.SetRenderFunc<HDRenderPipeline.ResolveStencilPassData>(delegate(HDRenderPipeline.ResolveStencilPassData data, RenderGraphContext context)
				{
					RenderGraphResourceRegistry resources = context.resources;
					HDRenderPipeline <>4__this = this;
					HDCamera hdCamera2 = hdCamera;
					RTHandle texture = resources.GetTexture(in data.inputDepth);
					RenderGraphResourceRegistry renderGraphResourceRegistry = resources;
					RenderGraphResource renderGraphResource = data.resolvedStencil;
					<>4__this.BuildCoarseStencilAndResolveIfNeeded(hdCamera2, texture, renderGraphResourceRegistry.GetTexture(in renderGraphResource), data.coarseStencilBuffer, context.cmd);
				});
				if (hdCamera.frameSettings.IsEnabled(FrameSettingsField.MSAA))
				{
					output.stencilBuffer = resolveStencilPassData.resolvedStencil;
				}
				else
				{
					output.stencilBuffer = output.depthBuffer;
				}
			}
		}

		// Token: 0x0600038D RID: 909 RVA: 0x0001D04C File Offset: 0x0001B24C
		private void SetupDBufferTargets(RenderGraph renderGraph, HDRenderPipeline.RenderDBufferPassData passData, bool use4RTs, ref HDRenderPipeline.PrepassOutput output, RenderGraphBuilder builder)
		{
			GraphicsFormat[] array;
			Decal.GetMaterialDBufferDescription(out array);
			passData.dBufferCount = (use4RTs ? 4 : 3);
			for (int i = 0; i < passData.dBufferCount; i++)
			{
				RenderGraphMutableResource[] mrt = passData.mrt;
				int num = i;
				RenderGraphMutableResource renderGraphMutableResource = renderGraph.CreateTexture(new TextureDesc(Vector2.one, true, true)
				{
					colorFormat = array[i],
					name = string.Format("DBuffer{0}", i)
				}, HDShaderIDs._DBufferTexture[i]);
				mrt[num] = builder.UseColorBuffer(in renderGraphMutableResource, i);
			}
			passData.depthStencilBuffer = builder.UseDepthBuffer(in output.resolvedDepthBuffer, DepthAccess.Write);
			output.dbuffer.dBufferCount = passData.dBufferCount;
			for (int j = 0; j < passData.dBufferCount; j++)
			{
				output.dbuffer.mrt[j] = passData.mrt[j];
			}
		}

		// Token: 0x0600038E RID: 910 RVA: 0x0001D134 File Offset: 0x0001B334
		private static void ReadDBuffer(HDRenderPipeline.DBufferOutput dBufferOutput, RenderGraphBuilder builder)
		{
			for (int i = 0; i < dBufferOutput.dBufferCount; i++)
			{
				builder.ReadTexture(in dBufferOutput.mrt[i]);
			}
		}

		// Token: 0x0600038F RID: 911 RVA: 0x0001D168 File Offset: 0x0001B368
		private void RenderDBuffer(RenderGraph renderGraph, HDCamera hdCamera, ref HDRenderPipeline.PrepassOutput output, CullingResults cullingResults)
		{
			bool perChannelMask = this.m_Asset.currentPlatformRenderPipelineSettings.decalSettings.perChannelMask;
			if (!hdCamera.frameSettings.IsEnabled(FrameSettingsField.Decals))
			{
				RenderGraphMutableResource renderGraphMutableResource = renderGraph.ImportTexture(TextureXR.GetBlackTexture(), 0);
				output.dbuffer.dBufferCount = (perChannelMask ? 4 : 3);
				for (int i = 0; i < output.dbuffer.dBufferCount; i++)
				{
					output.dbuffer.mrt[i] = renderGraphMutableResource;
				}
				return;
			}
			this.CopyDepthBufferIfNeeded(renderGraph, hdCamera, ref output);
			HDRenderPipeline.RenderDBufferPassData renderDBufferPassData;
			using (RenderGraphBuilder renderGraphBuilder = renderGraph.AddRenderPass<HDRenderPipeline.RenderDBufferPassData>("DBufferRender", out renderDBufferPassData, ProfilingSampler.Get<HDProfileId>(HDProfileId.DBufferRender)))
			{
				HDRenderPipeline.RenderDBufferPassData renderDBufferPassData2 = renderDBufferPassData;
				RendererListDesc rendererListDesc = this.PrepareMeshDecalsRendererList(cullingResults, hdCamera, perChannelMask);
				RenderGraphResource renderGraphResource = renderGraph.CreateRendererList(in rendererListDesc);
				renderDBufferPassData2.meshDecalsRendererList = renderGraphBuilder.UseRendererList(in renderGraphResource);
				this.SetupDBufferTargets(renderGraph, renderDBufferPassData, perChannelMask, ref output, renderGraphBuilder);
				renderGraphBuilder.SetRenderFunc<HDRenderPipeline.RenderDBufferPassData>(delegate(HDRenderPipeline.RenderDBufferPassData data, RenderGraphContext context)
				{
					RenderGraphResourceRegistry resources = context.resources;
					RenderTargetIdentifier[] tempArray = context.renderGraphPool.GetTempArray<RenderTargetIdentifier>(data.dBufferCount);
					RTHandle[] tempArray2 = context.renderGraphPool.GetTempArray<RTHandle>(data.dBufferCount);
					RenderGraphResource renderGraphResource2;
					for (int j = 0; j < data.dBufferCount; j++)
					{
						RTHandle[] array = tempArray2;
						int num = j;
						RenderGraphResourceRegistry renderGraphResourceRegistry = resources;
						renderGraphResource2 = data.mrt[j];
						array[num] = renderGraphResourceRegistry.GetTexture(in renderGraphResource2);
						tempArray[j] = tempArray2[j];
					}
					bool flag = data.dBufferCount == 4;
					RenderTargetIdentifier[] array2 = tempArray;
					RTHandle[] array3 = tempArray2;
					RenderGraphResourceRegistry renderGraphResourceRegistry2 = resources;
					renderGraphResource2 = data.depthStencilBuffer;
					HDRenderPipeline.RenderDBuffer(flag, array2, array3, renderGraphResourceRegistry2.GetTexture(in renderGraphResource2), this.m_DbufferManager.propertyMaskBuffer, this.m_DbufferManager.clearPropertyMaskBufferShader, this.m_DbufferManager.clearPropertyMaskBufferKernel, this.m_DbufferManager.propertyMaskBufferSize, resources.GetRendererList(in data.meshDecalsRendererList), context.renderContext, context.cmd);
				});
			}
		}

		// Token: 0x06000390 RID: 912 RVA: 0x0001D270 File Offset: 0x0001B470
		private void DecalNormalPatch(RenderGraph renderGraph, HDCamera hdCamera, ref HDRenderPipeline.PrepassOutput output)
		{
			if (hdCamera.frameSettings.IsEnabled(FrameSettingsField.Decals) && !hdCamera.frameSettings.IsEnabled(FrameSettingsField.MSAA))
			{
				HDRenderPipeline.DBufferNormalPatchData dbufferNormalPatchData;
				using (RenderGraphBuilder renderGraphBuilder = renderGraph.AddRenderPass<HDRenderPipeline.DBufferNormalPatchData>("DBuffer Normal (forward)", out dbufferNormalPatchData, ProfilingSampler.Get<HDProfileId>(HDProfileId.DBufferNormal)))
				{
					dbufferNormalPatchData.parameters = this.PrepareDBufferNormalPatchParameters(hdCamera);
					HDRenderPipeline.ReadDBuffer(output.dbuffer, renderGraphBuilder);
					dbufferNormalPatchData.normalBuffer = renderGraphBuilder.WriteTexture(in output.resolvedNormalBuffer);
					HDRenderPipeline.DBufferNormalPatchData dbufferNormalPatchData2 = dbufferNormalPatchData;
					RenderGraphResource renderGraphResource = output.resolvedDepthBuffer;
					dbufferNormalPatchData2.depthStencilBuffer = renderGraphBuilder.ReadTexture(in renderGraphResource);
					renderGraphBuilder.SetRenderFunc<HDRenderPipeline.DBufferNormalPatchData>(delegate(HDRenderPipeline.DBufferNormalPatchData data, RenderGraphContext context)
					{
						this.DecalNormalPatch(hdCamera, context.cmd, context.renderContext);
					});
				}
			}
		}

		// Token: 0x06000391 RID: 913 RVA: 0x0001D35C File Offset: 0x0001B55C
		private void GenerateDepthPyramid(RenderGraph renderGraph, HDCamera hdCamera, ref HDRenderPipeline.PrepassOutput output)
		{
			this.CopyDepthBufferIfNeeded(renderGraph, hdCamera, ref output);
			HDRenderPipeline.GenerateDepthPyramidPassData generateDepthPyramidPassData;
			using (RenderGraphBuilder renderGraphBuilder = renderGraph.AddRenderPass<HDRenderPipeline.GenerateDepthPyramidPassData>("Generate Depth Buffer MIP Chain", out generateDepthPyramidPassData, ProfilingSampler.Get<HDProfileId>(HDProfileId.DepthPyramid)))
			{
				generateDepthPyramidPassData.depthTexture = renderGraphBuilder.WriteTexture(in output.depthPyramidTexture);
				generateDepthPyramidPassData.mipInfo = this.GetDepthBufferMipChainInfo();
				generateDepthPyramidPassData.mipGenerator = this.m_MipGenerator;
				renderGraphBuilder.SetRenderFunc<HDRenderPipeline.GenerateDepthPyramidPassData>(delegate(HDRenderPipeline.GenerateDepthPyramidPassData data, RenderGraphContext context)
				{
					MipGenerator mipGenerator = data.mipGenerator;
					CommandBuffer cmd = context.cmd;
					RenderGraphResourceRegistry resources = context.resources;
					RenderGraphResource renderGraphResource = data.depthTexture;
					mipGenerator.RenderMinDepthPyramid(cmd, resources.GetTexture(in renderGraphResource), data.mipInfo);
				});
				output.depthPyramidTexture = generateDepthPyramidPassData.depthTexture;
			}
		}

		// Token: 0x06000392 RID: 914 RVA: 0x0001D404 File Offset: 0x0001B604
		private void RenderCameraMotionVectors(RenderGraph renderGraph, HDCamera hdCamera, RenderGraphResource depthTexture, RenderGraphMutableResource motionVectorsBuffer)
		{
			if (!hdCamera.frameSettings.IsEnabled(FrameSettingsField.MotionVectors))
			{
				return;
			}
			HDRenderPipeline.CameraMotionVectorsPassData cameraMotionVectorsPassData;
			using (RenderGraphBuilder renderGraphBuilder = renderGraph.AddRenderPass<HDRenderPipeline.CameraMotionVectorsPassData>("Camera Motion Vectors Rendering", out cameraMotionVectorsPassData, ProfilingSampler.Get<HDProfileId>(HDProfileId.CameraMotionVectors)))
			{
				hdCamera.camera.depthTextureMode |= DepthTextureMode.Depth | DepthTextureMode.MotionVectors;
				cameraMotionVectorsPassData.cameraMotionVectorsMaterial = this.m_CameraMotionVectorsMaterial;
				cameraMotionVectorsPassData.depthTexture = renderGraphBuilder.ReadTexture(in depthTexture);
				cameraMotionVectorsPassData.motionVectorsBuffer = renderGraphBuilder.WriteTexture(in motionVectorsBuffer);
				renderGraphBuilder.SetRenderFunc<HDRenderPipeline.CameraMotionVectorsPassData>(delegate(HDRenderPipeline.CameraMotionVectorsPassData data, RenderGraphContext context)
				{
					RenderGraphResourceRegistry resources = context.resources;
					CommandBuffer cmd = context.cmd;
					Material cameraMotionVectorsMaterial = data.cameraMotionVectorsMaterial;
					RenderGraphResourceRegistry renderGraphResourceRegistry = resources;
					RenderGraphResource renderGraphResource = data.motionVectorsBuffer;
					HDUtils.DrawFullScreen(cmd, cameraMotionVectorsMaterial, renderGraphResourceRegistry.GetTexture(in renderGraphResource), null, 0);
				});
			}
		}

		// Token: 0x06000393 RID: 915 RVA: 0x0001D4BC File Offset: 0x0001B6BC
		private void ExecuteWithRenderGraph(HDRenderPipeline.RenderRequest renderRequest, AOVRequestData aovRequest, List<RTHandle> aovBuffers, ScriptableRenderContext renderContext, CommandBuffer commandBuffer)
		{
			HDCamera hdCamera = renderRequest.hdCamera;
			Camera camera = hdCamera.camera;
			CullingResults cullingResults = renderRequest.cullingResults.cullingResults;
			bool flag = hdCamera.frameSettings.IsEnabled(FrameSettingsField.MSAA);
			HDRenderPipeline.RenderRequest.Target target = renderRequest.target;
			RenderGraphMutableResource renderGraphMutableResource = this.CreateColorBuffer(this.m_RenderGraph, hdCamera, flag);
			RenderGraphMutableResource renderGraphMutableResource2 = this.m_RenderGraph.ImportTexture(hdCamera.GetCurrentFrameRT(0), HDShaderIDs._ColorPyramidTexture);
			HDRenderPipeline.LightingBuffers lightingBuffers = default(HDRenderPipeline.LightingBuffers);
			lightingBuffers.diffuseLightingBuffer = this.CreateDiffuseLightingBuffer(this.m_RenderGraph, flag);
			lightingBuffers.sssBuffer = this.CreateSSSBuffer(this.m_RenderGraph, flag);
			HDRenderPipeline.PrepassOutput prepassOutput = this.RenderPrepass(this.m_RenderGraph, lightingBuffers.sssBuffer, cullingResults, hdCamera);
			ShadowResult shadowResult = default(ShadowResult);
			if (this.m_CurrentDebugDisplaySettings.IsDebugMaterialDisplayEnabled() || this.m_CurrentDebugDisplaySettings.IsMaterialValidationEnabled() || CoreUtils.IsSceneLightingDisabled(hdCamera.camera))
			{
				using (new XRSinglePassScope(this.m_RenderGraph, hdCamera))
				{
					this.RenderDebugViewMaterial(this.m_RenderGraph, cullingResults, hdCamera, renderGraphMutableResource);
					renderGraphMutableResource = this.ResolveMSAAColor(this.m_RenderGraph, hdCamera, renderGraphMutableResource);
					goto IL_050A;
				}
			}
			if (!hdCamera.frameSettings.IsEnabled(FrameSettingsField.RayTracing) || !hdCamera.volumeStack.GetComponent<PathTracing>().enable.value)
			{
				this.BuildGPULightList(this.m_RenderGraph, hdCamera, prepassOutput.depthBuffer, prepassOutput.stencilBuffer, prepassOutput.gbuffer);
				lightingBuffers.ambientOcclusionBuffer = this.m_AmbientOcclusionSystem.Render(this.m_RenderGraph, hdCamera, prepassOutput.depthPyramidTexture, prepassOutput.motionVectorsBuffer, this.m_FrameCount);
				this.PushFullScreenDebugTexture(this.m_RenderGraph, lightingBuffers.ambientOcclusionBuffer, FullScreenDebugMode.SSAO);
				RenderGraphResource renderGraphResource = ((hdCamera.frameSettings.litShaderMode == LitShaderMode.Deferred) ? prepassOutput.gbuffer.mrt[2] : this.m_RenderGraph.ImportTexture(TextureXR.GetBlackTexture(), 0));
				lightingBuffers.ssrLightingBuffer = this.RenderSSR(this.m_RenderGraph, hdCamera, prepassOutput.resolvedNormalBuffer, prepassOutput.resolvedMotionVectorsBuffer, prepassOutput.depthPyramidTexture, prepassOutput.stencilBuffer, renderGraphResource);
				lightingBuffers.contactShadowsBuffer = this.RenderContactShadows(this.m_RenderGraph, hdCamera, flag ? prepassOutput.depthValuesMSAA : prepassOutput.depthPyramidTexture, this.GetDepthBufferMipChainInfo().mipLevelOffsets[1].y);
				RenderGraphResource renderGraphResource2 = this.VolumeVoxelizationPass(this.m_RenderGraph, hdCamera, this.m_VisibleVolumeBoundsBuffer, this.m_VisibleVolumeDataBuffer, this.m_TileAndClusterData.bigTileLightList);
				shadowResult = this.RenderShadows(this.m_RenderGraph, hdCamera, cullingResults);
				RenderGraphResource renderGraphResource3 = this.VolumetricLightingPass(this.m_RenderGraph, hdCamera, renderGraphResource2, this.m_TileAndClusterData.bigTileLightList, shadowResult, this.m_FrameCount);
				HDRenderPipeline.StartXRSinglePass(this.m_RenderGraph, hdCamera);
				this.RenderDeferredLighting(this.m_RenderGraph, hdCamera, renderGraphMutableResource, prepassOutput.depthBuffer, prepassOutput.depthPyramidTexture, in lightingBuffers, in prepassOutput.gbuffer, in shadowResult);
				this.RenderForwardOpaque(this.m_RenderGraph, hdCamera, renderGraphMutableResource, in lightingBuffers, prepassOutput.depthBuffer, shadowResult, prepassOutput.dbuffer, cullingResults);
				aovRequest.PushCameraTexture(this.m_RenderGraph, AOVBuffers.Normals, hdCamera, prepassOutput.resolvedNormalBuffer, aovBuffers);
				lightingBuffers.diffuseLightingBuffer = this.ResolveMSAAColor(this.m_RenderGraph, hdCamera, lightingBuffers.diffuseLightingBuffer);
				lightingBuffers.sssBuffer = this.ResolveMSAAColor(this.m_RenderGraph, hdCamera, lightingBuffers.sssBuffer);
				this.RenderSubsurfaceScattering(this.m_RenderGraph, hdCamera, renderGraphMutableResource, in lightingBuffers, prepassOutput.depthBuffer, prepassOutput.depthPyramidTexture);
				this.RenderForwardEmissive(this.m_RenderGraph, hdCamera, renderGraphMutableResource, prepassOutput.depthBuffer, cullingResults);
				this.RenderSky(this.m_RenderGraph, hdCamera, renderGraphMutableResource, renderGraphResource3, prepassOutput.depthBuffer, prepassOutput.depthPyramidTexture);
				renderGraphMutableResource = this.RenderTransparency(this.m_RenderGraph, hdCamera, renderGraphMutableResource, prepassOutput.depthBuffer, prepassOutput.motionVectorsBuffer, renderGraphMutableResource2, prepassOutput.depthPyramidTexture, shadowResult, cullingResults);
				aovRequest.PushCameraTexture(this.m_RenderGraph, AOVBuffers.DepthStencil, hdCamera, prepassOutput.resolvedDepthBuffer, aovBuffers);
				if (this.m_Asset.currentPlatformRenderPipelineSettings.supportMotionVectors)
				{
					aovRequest.PushCameraTexture(this.m_RenderGraph, AOVBuffers.MotionVectors, hdCamera, prepassOutput.resolvedMotionVectorsBuffer, aovBuffers);
				}
				if (hdCamera.frameSettings.IsEnabled(FrameSettingsField.Distortion) || hdCamera.frameSettings.IsEnabled(FrameSettingsField.SSR))
				{
					this.GenerateColorPyramid(this.m_RenderGraph, hdCamera, renderGraphMutableResource, renderGraphMutableResource2, false);
				}
				RenderGraphResource renderGraphResource4 = this.AccumulateDistortion(this.m_RenderGraph, hdCamera, prepassOutput.resolvedDepthBuffer, cullingResults);
				this.RenderDistortion(this.m_RenderGraph, hdCamera, renderGraphMutableResource, prepassOutput.resolvedDepthBuffer, renderGraphMutableResource2, renderGraphResource4);
				this.PushFullScreenDebugTexture(this.m_RenderGraph, renderGraphMutableResource, FullScreenDebugMode.NanTracker);
				this.PushFullScreenLightingDebugTexture(this.m_RenderGraph, renderGraphMutableResource);
				this.RenderGizmos(this.m_RenderGraph, hdCamera, renderGraphMutableResource, GizmoSubset.PreImageEffects);
			}
			IL_050A:
			RenderGraphResource renderGraphResource5 = this.PushColorPickerDebugTexture(this.m_RenderGraph, renderGraphMutableResource);
			aovRequest.PushCameraTexture(this.m_RenderGraph, AOVBuffers.Color, hdCamera, renderGraphMutableResource, aovBuffers);
			hdCamera.ExecuteCaptureActions(this.m_RenderGraph, renderGraphMutableResource);
			renderGraphMutableResource = this.RenderDebug(this.m_RenderGraph, hdCamera, renderGraphMutableResource, prepassOutput.depthBuffer, prepassOutput.depthPyramidTexture, this.m_DebugFullScreenTexture, renderGraphResource5, in shadowResult, cullingResults);
			this.BlitFinalCameraTexture(this.m_RenderGraph, hdCamera, renderGraphMutableResource, target.id, prepassOutput.resolvedMotionVectorsBuffer, prepassOutput.resolvedNormalBuffer);
			aovRequest.PushCameraTexture(this.m_RenderGraph, AOVBuffers.Output, hdCamera, renderGraphMutableResource, aovBuffers);
			this.EndCameraXR(this.m_RenderGraph, hdCamera);
			this.SetFinalTarget(this.m_RenderGraph, hdCamera, prepassOutput.resolvedDepthBuffer, target.id);
			this.RenderGizmos(this.m_RenderGraph, hdCamera, renderGraphMutableResource, GizmoSubset.PostImageEffects);
			HDRenderPipeline.ExecuteRenderGraph(this.m_RenderGraph, hdCamera, this.m_MSAASamples, renderContext, commandBuffer);
			aovRequest.Execute(commandBuffer, aovBuffers, RenderOutputProperties.From(hdCamera));
		}

		// Token: 0x06000394 RID: 916 RVA: 0x0001DB04 File Offset: 0x0001BD04
		private static void ExecuteRenderGraph(RenderGraph renderGraph, HDCamera hdCamera, MSAASamples msaaSample, ScriptableRenderContext renderContext, CommandBuffer cmd)
		{
			RenderGraphExecuteParams renderGraphExecuteParams = new RenderGraphExecuteParams
			{
				renderingWidth = hdCamera.actualWidth,
				renderingHeight = hdCamera.actualHeight,
				msaaSamples = msaaSample
			};
			renderGraph.Execute(renderContext, cmd, in renderGraphExecuteParams);
		}

		// Token: 0x06000395 RID: 917 RVA: 0x0001DB48 File Offset: 0x0001BD48
		private void BlitFinalCameraTexture(RenderGraph renderGraph, HDCamera hdCamera, RenderGraphResource source, RenderTargetIdentifier destination, RenderGraphResource motionVectors, RenderGraphResource normalBuffer)
		{
			HDRenderPipeline.FinalBlitPassData finalBlitPassData;
			using (RenderGraphBuilder renderGraphBuilder = renderGraph.AddRenderPass<HDRenderPipeline.FinalBlitPassData>("Final Blit (Dev Build Only)", out finalBlitPassData, null))
			{
				finalBlitPassData.parameters = this.PrepareFinalBlitParameters(hdCamera, 0);
				finalBlitPassData.source = renderGraphBuilder.ReadTexture(in source);
				finalBlitPassData.destination = destination;
				hdCamera.frameSettings.IsEnabled(FrameSettingsField.MSAA);
				if (hdCamera.frameSettings.IsEnabled(FrameSettingsField.MotionVectors))
				{
					renderGraphBuilder.ReadTexture(in motionVectors);
				}
				renderGraphBuilder.ReadTexture(in normalBuffer);
				renderGraphBuilder.SetRenderFunc<HDRenderPipeline.FinalBlitPassData>(delegate(HDRenderPipeline.FinalBlitPassData data, RenderGraphContext context)
				{
					RTHandle texture = context.resources.GetTexture(in data.source);
					HDRenderPipeline.BlitFinalCameraTexture(data.parameters, context.renderGraphPool.GetTempMaterialPropertyBlock(), texture, data.destination, context.cmd);
				});
			}
		}

		// Token: 0x06000396 RID: 918 RVA: 0x0001DC08 File Offset: 0x0001BE08
		private void SetFinalTarget(RenderGraph renderGraph, HDCamera hdCamera, RenderGraphResource depthBuffer, RenderTargetIdentifier finalTarget)
		{
			HDRenderPipeline.SetFinalTargetPassData setFinalTargetPassData;
			using (RenderGraphBuilder renderGraphBuilder = renderGraph.AddRenderPass<HDRenderPipeline.SetFinalTargetPassData>("Set Final Target", out setFinalTargetPassData, null))
			{
				setFinalTargetPassData.copyDepth = hdCamera.camera.targetTexture != null && hdCamera.camera.targetTexture.depth != 0;
				setFinalTargetPassData.copyDepthMaterial = this.m_CopyDepth;
				setFinalTargetPassData.finalTarget = finalTarget;
				setFinalTargetPassData.finalViewport = hdCamera.finalViewport;
				setFinalTargetPassData.depthBuffer = renderGraphBuilder.ReadTexture(in depthBuffer);
				setFinalTargetPassData.flipY = hdCamera.isMainGameView;
				renderGraphBuilder.SetRenderFunc<HDRenderPipeline.SetFinalTargetPassData>(delegate(HDRenderPipeline.SetFinalTargetPassData data, RenderGraphContext ctx)
				{
					ctx.cmd.SetRenderTarget(data.finalTarget);
					ctx.cmd.SetViewport(data.finalViewport);
					if (data.copyDepth)
					{
						using (new ProfilingScope(ctx.cmd, ProfilingSampler.Get<HDProfileId>(HDProfileId.CopyDepthInTargetTexture)))
						{
							MaterialPropertyBlock tempMaterialPropertyBlock = ctx.renderGraphPool.GetTempMaterialPropertyBlock();
							tempMaterialPropertyBlock.SetTexture(HDShaderIDs._InputDepth, ctx.resources.GetTexture(in data.depthBuffer));
							tempMaterialPropertyBlock.SetInt("_FlipY", data.flipY ? 1 : 0);
							tempMaterialPropertyBlock.SetVector(HDShaderIDs._BlitScaleBias, new Vector4(1f, 1f, 0f, 0f));
							CoreUtils.DrawFullScreen(ctx.cmd, data.copyDepthMaterial, tempMaterialPropertyBlock, 0);
						}
					}
				});
			}
		}

		// Token: 0x06000397 RID: 919 RVA: 0x0001DCD4 File Offset: 0x0001BED4
		private void PrepareForwardPassData(RenderGraph renderGraph, RenderGraphBuilder builder, HDRenderPipeline.ForwardPassData data, bool opaque, FrameSettings frameSettings, RendererListDesc rendererListDesc, RenderGraphMutableResource depthBuffer, ShadowResult shadowResult, HDRenderPipeline.DBufferOutput? dbuffer = null)
		{
			bool flag = frameSettings.IsEnabled(FrameSettingsField.FPTLForForwardOpaque) && opaque;
			data.frameSettings = frameSettings;
			data.lightListBuffer = (flag ? this.m_TileAndClusterData.lightList : this.m_TileAndClusterData.perVoxelLightLists);
			data.depthBuffer = builder.UseDepthBuffer(in depthBuffer, DepthAccess.ReadWrite);
			RenderGraphResource renderGraphResource = renderGraph.CreateRendererList(in rendererListDesc);
			data.rendererList = builder.UseRendererList(in renderGraphResource);
			data.decalsEnabled = frameSettings.IsEnabled(FrameSettingsField.Decals) && DecalSystem.m_DecalDatasCount > 0;
			data.renderMotionVecForTransparent = HDRenderPipeline.NeedMotionVectorForTransparent(frameSettings);
			HDShadowManager.ReadShadowResult(shadowResult, builder);
			if (dbuffer != null)
			{
				HDRenderPipeline.ReadDBuffer(dbuffer.Value, builder);
			}
		}

		// Token: 0x06000398 RID: 920 RVA: 0x0001DD88 File Offset: 0x0001BF88
		private void RenderForwardOpaque(RenderGraph renderGraph, HDCamera hdCamera, RenderGraphMutableResource colorBuffer, in HDRenderPipeline.LightingBuffers lightingBuffers, RenderGraphMutableResource depthBuffer, ShadowResult shadowResult, HDRenderPipeline.DBufferOutput dbuffer, CullingResults cullResults)
		{
			bool flag = this.m_CurrentDebugDisplaySettings.IsDebugDisplayEnabled();
			HDRenderPipeline.ForwardPassData forwardPassData;
			using (RenderGraphBuilder renderGraphBuilder = renderGraph.AddRenderPass<HDRenderPipeline.ForwardPassData>(flag ? "Forward Opaque Debug" : "Forward Opaque", out forwardPassData, flag ? ProfilingSampler.Get<HDProfileId>(HDProfileId.ForwardOpaqueDebug) : ProfilingSampler.Get<HDProfileId>(HDProfileId.ForwardOpaque)))
			{
				this.PrepareForwardPassData(renderGraph, renderGraphBuilder, forwardPassData, true, hdCamera.frameSettings, this.PrepareForwardOpaqueRendererList(cullResults, hdCamera), depthBuffer, shadowResult, new HDRenderPipeline.DBufferOutput?(dbuffer));
				if (hdCamera.frameSettings.IsEnabled(FrameSettingsField.SubsurfaceScattering))
				{
					forwardPassData.renderTarget[0] = renderGraphBuilder.WriteTexture(in colorBuffer);
					forwardPassData.renderTarget[1] = renderGraphBuilder.WriteTexture(in lightingBuffers.diffuseLightingBuffer);
					forwardPassData.renderTarget[2] = renderGraphBuilder.WriteTexture(in lightingBuffers.sssBuffer);
					forwardPassData.renderTargetCount = 3;
				}
				else
				{
					forwardPassData.renderTarget[0] = renderGraphBuilder.WriteTexture(in colorBuffer);
					forwardPassData.renderTargetCount = 1;
				}
				HDRenderPipeline.ReadLightingBuffers(lightingBuffers, renderGraphBuilder);
				renderGraphBuilder.SetRenderFunc<HDRenderPipeline.ForwardPassData>(delegate(HDRenderPipeline.ForwardPassData data, RenderGraphContext context)
				{
					RenderTargetIdentifier[] tempArray = context.renderGraphPool.GetTempArray<RenderTargetIdentifier>(data.renderTargetCount);
					RenderGraphResource renderGraphResource;
					for (int i = 0; i < data.renderTargetCount; i++)
					{
						RenderTargetIdentifier[] array = tempArray;
						int num = i;
						RenderGraphResourceRegistry resources = context.resources;
						renderGraphResource = data.renderTarget[i];
						array[num] = resources.GetTexture(in renderGraphResource);
					}
					FrameSettings frameSettings = data.frameSettings;
					RendererList rendererList = context.resources.GetRendererList(in data.rendererList);
					RenderTargetIdentifier[] array2 = tempArray;
					RenderGraphResourceRegistry resources2 = context.resources;
					renderGraphResource = data.depthBuffer;
					HDRenderPipeline.RenderForwardRendererList(frameSettings, rendererList, array2, resources2.GetTexture(in renderGraphResource), data.lightListBuffer, true, context.renderContext, context.cmd);
				});
			}
		}

		// Token: 0x06000399 RID: 921 RVA: 0x0001DEC4 File Offset: 0x0001C0C4
		private void RenderForwardTransparent(RenderGraph renderGraph, HDCamera hdCamera, RenderGraphMutableResource colorBuffer, RenderGraphMutableResource motionVectorBuffer, RenderGraphMutableResource depthBuffer, RenderGraphResource? colorPyramid, ShadowResult shadowResult, CullingResults cullResults, bool preRefractionPass)
		{
			if (!hdCamera.frameSettings.IsEnabled(FrameSettingsField.Refraction) && preRefractionPass)
			{
				return;
			}
			string text;
			HDProfileId hdprofileId;
			if (this.m_CurrentDebugDisplaySettings.IsDebugDisplayEnabled())
			{
				text = (preRefractionPass ? "Forward PreRefraction Debug" : "Forward Transparent Debug");
				hdprofileId = (preRefractionPass ? HDProfileId.ForwardPreRefractionDebug : HDProfileId.ForwardTransparentDebug);
			}
			else
			{
				text = (preRefractionPass ? "Forward PreRefraction" : "Forward Transparent");
				hdprofileId = (preRefractionPass ? HDProfileId.ForwardPreRefraction : HDProfileId.ForwardTransparent);
			}
			HDRenderPipeline.ForwardPassData forwardPassData;
			using (RenderGraphBuilder renderGraphBuilder = renderGraph.AddRenderPass<HDRenderPipeline.ForwardPassData>(text, out forwardPassData, ProfilingSampler.Get<HDProfileId>(hdprofileId)))
			{
				this.PrepareForwardPassData(renderGraph, renderGraphBuilder, forwardPassData, false, hdCamera.frameSettings, this.PrepareForwardTransparentRendererList(cullResults, hdCamera, preRefractionPass), depthBuffer, shadowResult, null);
				RenderGraphMutableResource renderGraphMutableResource;
				if (HDRenderPipeline.NeedMotionVectorForTransparent(hdCamera.frameSettings))
				{
					renderGraphMutableResource = motionVectorBuffer;
					RenderGraphResource renderGraphResource = motionVectorBuffer;
					renderGraphBuilder.ReadTexture(in renderGraphResource);
				}
				else
				{
					renderGraphMutableResource = renderGraph.CreateTexture(new TextureDesc(Vector2.one, true, true)
					{
						colorFormat = GraphicsFormat.R8G8B8A8_SRGB,
						name = "Transparency Velocity Dummy"
					}, 0);
				}
				forwardPassData.renderTargetCount = 2;
				forwardPassData.renderTarget[0] = renderGraphBuilder.WriteTexture(in colorBuffer);
				forwardPassData.renderTarget[1] = renderGraphBuilder.WriteTexture(in renderGraphMutableResource);
				if (colorPyramid != null && hdCamera.frameSettings.IsEnabled(FrameSettingsField.Refraction) && !preRefractionPass)
				{
					RenderGraphResource renderGraphResource = colorPyramid.Value;
					renderGraphBuilder.ReadTexture(in renderGraphResource);
				}
				renderGraphBuilder.SetRenderFunc<HDRenderPipeline.ForwardPassData>(delegate(HDRenderPipeline.ForwardPassData data, RenderGraphContext context)
				{
					RenderTargetIdentifier[] tempArray = context.renderGraphPool.GetTempArray<RenderTargetIdentifier>(data.renderTargetCount);
					RenderGraphResource renderGraphResource2;
					for (int i = 0; i < data.renderTargetCount; i++)
					{
						RenderTargetIdentifier[] array = tempArray;
						int num = i;
						RenderGraphResourceRegistry resources = context.resources;
						renderGraphResource2 = data.renderTarget[i];
						array[num] = resources.GetTexture(in renderGraphResource2);
					}
					context.cmd.SetGlobalInt(HDShaderIDs._ColorMaskTransparentVel, data.renderMotionVecForTransparent ? 15 : 0);
					if (data.decalsEnabled)
					{
						DecalSystem.instance.SetAtlas(context.cmd);
					}
					FrameSettings frameSettings = data.frameSettings;
					RendererList rendererList = context.resources.GetRendererList(in data.rendererList);
					RenderTargetIdentifier[] array2 = tempArray;
					RenderGraphResourceRegistry resources2 = context.resources;
					renderGraphResource2 = data.depthBuffer;
					HDRenderPipeline.RenderForwardRendererList(frameSettings, rendererList, array2, resources2.GetTexture(in renderGraphResource2), data.lightListBuffer, false, context.renderContext, context.cmd);
				});
			}
		}

		// Token: 0x0600039A RID: 922 RVA: 0x0001E078 File Offset: 0x0001C278
		private void RenderTransparentDepthPrepass(RenderGraph renderGraph, HDCamera hdCamera, RenderGraphMutableResource depthStencilBuffer, CullingResults cull)
		{
			if (!hdCamera.frameSettings.IsEnabled(FrameSettingsField.TransparentPrepass))
			{
				return;
			}
			HDRenderPipeline.ForwardPassData forwardPassData;
			using (RenderGraphBuilder renderGraphBuilder = renderGraph.AddRenderPass<HDRenderPipeline.ForwardPassData>("Transparent Depth Prepass", out forwardPassData, ProfilingSampler.Get<HDProfileId>(HDProfileId.TransparentDepthPrepass)))
			{
				forwardPassData.frameSettings = hdCamera.frameSettings;
				forwardPassData.depthBuffer = renderGraphBuilder.UseDepthBuffer(in depthStencilBuffer, DepthAccess.ReadWrite);
				forwardPassData.renderTargetCount = 0;
				HDRenderPipeline.ForwardPassData forwardPassData2 = forwardPassData;
				RendererListDesc rendererListDesc = HDRenderPipeline.CreateTransparentRendererListDesc(cull, hdCamera.camera, this.m_TransparentDepthPrepassNames, PerObjectData.None, null, null, null, false);
				RenderGraphResource renderGraphResource = renderGraph.CreateRendererList(in rendererListDesc);
				forwardPassData2.rendererList = renderGraphBuilder.UseRendererList(in renderGraphResource);
				renderGraphBuilder.SetRenderFunc<HDRenderPipeline.ForwardPassData>(delegate(HDRenderPipeline.ForwardPassData data, RenderGraphContext context)
				{
					HDRenderPipeline.DrawTransparentRendererList(in context.renderContext, context.cmd, in data.frameSettings, context.resources.GetRendererList(in data.rendererList));
				});
			}
		}

		// Token: 0x0600039B RID: 923 RVA: 0x0001E15C File Offset: 0x0001C35C
		private void RenderTransparentDepthPostpass(RenderGraph renderGraph, HDCamera hdCamera, RenderGraphMutableResource depthStencilBuffer, CullingResults cull)
		{
			if (!hdCamera.frameSettings.IsEnabled(FrameSettingsField.TransparentPostpass))
			{
				return;
			}
			HDRenderPipeline.ForwardPassData forwardPassData;
			using (RenderGraphBuilder renderGraphBuilder = renderGraph.AddRenderPass<HDRenderPipeline.ForwardPassData>("Transparent Depth Postpass", out forwardPassData, ProfilingSampler.Get<HDProfileId>(HDProfileId.TransparentDepthPostpass)))
			{
				forwardPassData.frameSettings = hdCamera.frameSettings;
				forwardPassData.depthBuffer = renderGraphBuilder.UseDepthBuffer(in depthStencilBuffer, DepthAccess.ReadWrite);
				forwardPassData.renderTargetCount = 0;
				HDRenderPipeline.ForwardPassData forwardPassData2 = forwardPassData;
				RendererListDesc rendererListDesc = HDRenderPipeline.CreateTransparentRendererListDesc(cull, hdCamera.camera, this.m_TransparentDepthPostpassNames, PerObjectData.None, null, null, null, false);
				RenderGraphResource renderGraphResource = renderGraph.CreateRendererList(in rendererListDesc);
				forwardPassData2.rendererList = renderGraphBuilder.UseRendererList(in renderGraphResource);
				renderGraphBuilder.SetRenderFunc<HDRenderPipeline.ForwardPassData>(delegate(HDRenderPipeline.ForwardPassData data, RenderGraphContext context)
				{
					HDRenderPipeline.DrawTransparentRendererList(in context.renderContext, context.cmd, in data.frameSettings, context.resources.GetRendererList(in data.rendererList));
				});
			}
		}

		// Token: 0x0600039C RID: 924 RVA: 0x0001E240 File Offset: 0x0001C440
		private RenderGraphMutableResource DownsampleDepthForLowResTransparency(RenderGraph renderGraph, HDCamera hdCamera, RenderGraphResource depthTexture)
		{
			HDRenderPipeline.DownsampleDepthForLowResPassData downsampleDepthForLowResPassData;
			RenderGraphMutableResource renderGraphMutableResource;
			using (RenderGraphBuilder renderGraphBuilder = renderGraph.AddRenderPass<HDRenderPipeline.DownsampleDepthForLowResPassData>("Downsample Depth Buffer for Low Res Transparency", out downsampleDepthForLowResPassData, ProfilingSampler.Get<HDProfileId>(HDProfileId.DownsampleDepth)))
			{
				if (this.m_Asset.currentPlatformRenderPipelineSettings.lowresTransparentSettings.checkerboardDepthBuffer)
				{
					this.m_DownsampleDepthMaterial.EnableKeyword("CHECKERBOARD_DOWNSAMPLE");
				}
				downsampleDepthForLowResPassData.downsampleDepthMaterial = this.m_DownsampleDepthMaterial;
				downsampleDepthForLowResPassData.depthTexture = renderGraphBuilder.ReadTexture(in depthTexture);
				HDRenderPipeline.DownsampleDepthForLowResPassData downsampleDepthForLowResPassData2 = downsampleDepthForLowResPassData;
				renderGraphMutableResource = renderGraph.CreateTexture(new TextureDesc(Vector2.one * 0.5f, true, true)
				{
					depthBufferBits = DepthBits.Depth32,
					name = "LowResDepthBuffer"
				}, 0);
				downsampleDepthForLowResPassData2.downsampledDepthBuffer = renderGraphBuilder.UseDepthBuffer(in renderGraphMutableResource, DepthAccess.Write);
				renderGraphBuilder.SetRenderFunc<HDRenderPipeline.DownsampleDepthForLowResPassData>(delegate(HDRenderPipeline.DownsampleDepthForLowResPassData data, RenderGraphContext context)
				{
					context.cmd.DrawProcedural(Matrix4x4.identity, data.downsampleDepthMaterial, 0, MeshTopology.Triangles, 3, 1, null);
				});
				renderGraphMutableResource = downsampleDepthForLowResPassData.downsampledDepthBuffer;
			}
			return renderGraphMutableResource;
		}

		// Token: 0x0600039D RID: 925 RVA: 0x0001E33C File Offset: 0x0001C53C
		private RenderGraphResource RenderLowResTransparent(RenderGraph renderGraph, HDCamera hdCamera, RenderGraphMutableResource downsampledDepth, CullingResults cullingResults)
		{
			HDRenderPipeline.RenderLowResTransparentPassData renderLowResTransparentPassData;
			RenderGraphResource renderGraphResource;
			using (RenderGraphBuilder renderGraphBuilder = renderGraph.AddRenderPass<HDRenderPipeline.RenderLowResTransparentPassData>("Low Res Transparent", out renderLowResTransparentPassData, ProfilingSampler.Get<HDProfileId>(HDProfileId.LowResTransparent)))
			{
				ShaderTagId[] array = (this.m_Asset.currentPlatformRenderPipelineSettings.supportTransparentBackface ? this.m_AllTransparentPassNames : this.m_TransparentNoBackfaceNames);
				renderLowResTransparentPassData.frameSettings = hdCamera.frameSettings;
				HDRenderPipeline.RenderLowResTransparentPassData renderLowResTransparentPassData2 = renderLowResTransparentPassData;
				RendererListDesc rendererListDesc = HDRenderPipeline.CreateTransparentRendererListDesc(cullingResults, hdCamera.camera, array, this.m_CurrentRendererConfigurationBakedLighting, new RenderQueueRange?(HDRenderQueue.k_RenderQueue_LowTransparent), null, null, false);
				renderGraphResource = renderGraph.CreateRendererList(in rendererListDesc);
				renderLowResTransparentPassData2.rendererList = renderGraphBuilder.UseRendererList(in renderGraphResource);
				renderLowResTransparentPassData.downsampledDepthBuffer = renderGraphBuilder.UseDepthBuffer(in downsampledDepth, DepthAccess.ReadWrite);
				HDRenderPipeline.RenderLowResTransparentPassData renderLowResTransparentPassData3 = renderLowResTransparentPassData;
				RenderGraphMutableResource renderGraphMutableResource = renderGraph.CreateTexture(new TextureDesc(Vector2.one * 0.5f, true, true)
				{
					colorFormat = GraphicsFormat.R16G16B16A16_SFloat,
					enableRandomWrite = true,
					clearBuffer = true,
					clearColor = Color.black,
					name = "Low res transparent"
				}, 0);
				renderLowResTransparentPassData3.lowResBuffer = renderGraphBuilder.UseColorBuffer(in renderGraphMutableResource, 0);
				renderGraphBuilder.SetRenderFunc<HDRenderPipeline.RenderLowResTransparentPassData>(delegate(HDRenderPipeline.RenderLowResTransparentPassData data, RenderGraphContext context)
				{
					context.cmd.SetGlobalInt(HDShaderIDs._OffScreenRendering, 1);
					context.cmd.SetGlobalInt(HDShaderIDs._OffScreenDownsampleFactor, 2);
					HDRenderPipeline.DrawTransparentRendererList(in context.renderContext, context.cmd, in data.frameSettings, context.resources.GetRendererList(in data.rendererList));
					context.cmd.SetGlobalInt(HDShaderIDs._OffScreenRendering, 0);
					context.cmd.SetGlobalInt(HDShaderIDs._OffScreenDownsampleFactor, 1);
				});
				renderGraphResource = renderLowResTransparentPassData.lowResBuffer;
			}
			return renderGraphResource;
		}

		// Token: 0x0600039E RID: 926 RVA: 0x0001E4A8 File Offset: 0x0001C6A8
		private void UpsampleTransparent(RenderGraph renderGraph, HDCamera hdCamera, RenderGraphMutableResource colorBuffer, RenderGraphResource lowResTransparentBuffer, RenderGraphResource downsampledDepthBuffer)
		{
			HDRenderPipeline.UpsampleTransparentPassData upsampleTransparentPassData;
			using (RenderGraphBuilder renderGraphBuilder = renderGraph.AddRenderPass<HDRenderPipeline.UpsampleTransparentPassData>("Upsample Low Res Transparency", out upsampleTransparentPassData, ProfilingSampler.Get<HDProfileId>(HDProfileId.UpsampleLowResTransparent)))
			{
				GlobalLowResolutionTransparencySettings lowresTransparentSettings = this.m_Asset.currentPlatformRenderPipelineSettings.lowresTransparentSettings;
				if (lowresTransparentSettings.upsampleType == LowResTransparentUpsample.Bilinear)
				{
					this.m_UpsampleTransparency.EnableKeyword("BILINEAR");
				}
				else if (lowresTransparentSettings.upsampleType == LowResTransparentUpsample.NearestDepth)
				{
					this.m_UpsampleTransparency.EnableKeyword("NEAREST_DEPTH");
				}
				upsampleTransparentPassData.upsampleMaterial = this.m_UpsampleTransparency;
				upsampleTransparentPassData.colorBuffer = renderGraphBuilder.UseColorBuffer(in colorBuffer, 0);
				upsampleTransparentPassData.lowResTransparentBuffer = renderGraphBuilder.ReadTexture(in lowResTransparentBuffer);
				upsampleTransparentPassData.downsampledDepthBuffer = renderGraphBuilder.ReadTexture(in downsampledDepthBuffer);
				renderGraphBuilder.SetRenderFunc<HDRenderPipeline.UpsampleTransparentPassData>(delegate(HDRenderPipeline.UpsampleTransparentPassData data, RenderGraphContext context)
				{
					RenderGraphResourceRegistry resources = context.resources;
					data.upsampleMaterial.SetTexture(HDShaderIDs._LowResTransparent, resources.GetTexture(in data.lowResTransparentBuffer));
					data.upsampleMaterial.SetTexture(HDShaderIDs._LowResDepthTexture, resources.GetTexture(in data.downsampledDepthBuffer));
					context.cmd.DrawProcedural(Matrix4x4.identity, data.upsampleMaterial, 0, MeshTopology.Triangles, 3, 1, null);
				});
			}
		}

		// Token: 0x0600039F RID: 927 RVA: 0x0001E594 File Offset: 0x0001C794
		private RenderGraphMutableResource RenderTransparency(RenderGraph renderGraph, HDCamera hdCamera, RenderGraphMutableResource colorBuffer, RenderGraphMutableResource depthStencilBuffer, RenderGraphMutableResource motionVectorsBuffer, RenderGraphMutableResource currentColorPyramid, RenderGraphResource depthPyramid, ShadowResult shadowResult, CullingResults cullingResults)
		{
			this.RenderTransparentDepthPrepass(renderGraph, hdCamera, depthStencilBuffer, cullingResults);
			this.RenderForwardTransparent(renderGraph, hdCamera, colorBuffer, motionVectorsBuffer, depthStencilBuffer, null, shadowResult, cullingResults, true);
			if (hdCamera.frameSettings.IsEnabled(FrameSettingsField.Refraction))
			{
				RenderGraphMutableResource renderGraphMutableResource = this.ResolveMSAAColor(renderGraph, hdCamera, colorBuffer);
				this.GenerateColorPyramid(renderGraph, hdCamera, renderGraphMutableResource, currentColorPyramid, true);
			}
			this.RenderForwardTransparent(renderGraph, hdCamera, colorBuffer, motionVectorsBuffer, depthStencilBuffer, new RenderGraphResource?(currentColorPyramid), shadowResult, cullingResults, false);
			if (this.m_Asset.currentPlatformRenderPipelineSettings.supportMotionVectors)
			{
				this.PushFullScreenDebugTexture(this.m_RenderGraph, motionVectorsBuffer, FullScreenDebugMode.MotionVectors);
			}
			colorBuffer = this.ResolveMSAAColor(renderGraph, hdCamera, colorBuffer);
			if (hdCamera.frameSettings.IsEnabled(FrameSettingsField.LowResTransparent))
			{
				RenderGraphMutableResource renderGraphMutableResource2 = this.DownsampleDepthForLowResTransparency(renderGraph, hdCamera, depthPyramid);
				RenderGraphResource renderGraphResource = this.RenderLowResTransparent(renderGraph, hdCamera, renderGraphMutableResource2, cullingResults);
				this.UpsampleTransparent(renderGraph, hdCamera, colorBuffer, renderGraphResource, renderGraphMutableResource2);
			}
			this.RenderTransparentDepthPostpass(renderGraph, hdCamera, depthStencilBuffer, cullingResults);
			return colorBuffer;
		}

		// Token: 0x060003A0 RID: 928 RVA: 0x0001E690 File Offset: 0x0001C890
		private void RenderForwardEmissive(RenderGraph renderGraph, HDCamera hdCamera, RenderGraphMutableResource colorBuffer, RenderGraphMutableResource depthStencilBuffer, CullingResults cullingResults)
		{
			if (!hdCamera.frameSettings.IsEnabled(FrameSettingsField.Decals))
			{
				return;
			}
			HDRenderPipeline.RenderForwardEmissivePassData renderForwardEmissivePassData;
			using (RenderGraphBuilder renderGraphBuilder = renderGraph.AddRenderPass<HDRenderPipeline.RenderForwardEmissivePassData>("ForwardEmissive", out renderForwardEmissivePassData, ProfilingSampler.Get<HDProfileId>(HDProfileId.ForwardEmissive)))
			{
				renderGraphBuilder.UseColorBuffer(in colorBuffer, 0);
				renderGraphBuilder.UseDepthBuffer(in depthStencilBuffer, DepthAccess.ReadWrite);
				HDRenderPipeline.RenderForwardEmissivePassData renderForwardEmissivePassData2 = renderForwardEmissivePassData;
				RendererListDesc rendererListDesc = this.PrepareForwardEmissiveRendererList(cullingResults, hdCamera);
				RenderGraphResource renderGraphResource = renderGraph.CreateRendererList(in rendererListDesc);
				renderForwardEmissivePassData2.rendererList = renderGraphBuilder.UseRendererList(in renderGraphResource);
				renderGraphBuilder.SetRenderFunc<HDRenderPipeline.RenderForwardEmissivePassData>(delegate(HDRenderPipeline.RenderForwardEmissivePassData data, RenderGraphContext context)
				{
					HDUtils.DrawRendererList(context.renderContext, context.cmd, context.resources.GetRendererList(in data.rendererList));
					DecalSystem.instance.RenderForwardEmissive(context.cmd);
				});
			}
		}

		// Token: 0x060003A1 RID: 929 RVA: 0x0001E744 File Offset: 0x0001C944
		[Conditional("DEVELOPMENT_BUILD")]
		[Conditional("UNITY_EDITOR")]
		private void RenderForwardError(RenderGraph renderGraph, HDCamera hdCamera, RenderGraphMutableResource colorBuffer, RenderGraphMutableResource depthStencilBuffer, CullingResults cullResults)
		{
			HDRenderPipeline.ForwardPassData forwardPassData;
			using (RenderGraphBuilder renderGraphBuilder = renderGraph.AddRenderPass<HDRenderPipeline.ForwardPassData>("Forward Error", out forwardPassData, ProfilingSampler.Get<HDProfileId>(HDProfileId.RenderForwardError)))
			{
				renderGraphBuilder.UseColorBuffer(in colorBuffer, 0);
				renderGraphBuilder.UseDepthBuffer(in depthStencilBuffer, DepthAccess.ReadWrite);
				HDRenderPipeline.ForwardPassData forwardPassData2 = forwardPassData;
				RendererListDesc rendererListDesc = HDRenderPipeline.CreateOpaqueRendererListDesc(cullResults, hdCamera.camera, this.m_ForwardErrorPassNames, PerObjectData.None, new RenderQueueRange?(RenderQueueRange.all), null, this.m_ErrorMaterial, false);
				RenderGraphResource renderGraphResource = renderGraph.CreateRendererList(in rendererListDesc);
				forwardPassData2.rendererList = renderGraphBuilder.UseRendererList(in renderGraphResource);
				renderGraphBuilder.SetRenderFunc<HDRenderPipeline.ForwardPassData>(delegate(HDRenderPipeline.ForwardPassData data, RenderGraphContext context)
				{
					HDUtils.DrawRendererList(context.renderContext, context.cmd, context.resources.GetRendererList(in data.rendererList));
				});
			}
		}

		// Token: 0x060003A2 RID: 930 RVA: 0x0001E80C File Offset: 0x0001CA0C
		private void RenderSky(RenderGraph renderGraph, HDCamera hdCamera, RenderGraphMutableResource colorBuffer, RenderGraphResource volumetricLighting, RenderGraphMutableResource depthStencilBuffer, RenderGraphResource depthTexture)
		{
			if (this.m_CurrentDebugDisplaySettings.IsMatcapViewEnabled(hdCamera))
			{
				return;
			}
			HDRenderPipeline.RenderSkyPassData renderSkyPassData;
			using (RenderGraphBuilder renderGraphBuilder = renderGraph.AddRenderPass<HDRenderPipeline.RenderSkyPassData>("Render Sky And Fog", out renderSkyPassData, null))
			{
				renderSkyPassData.visualEnvironment = hdCamera.volumeStack.GetComponent<VisualEnvironment>();
				renderSkyPassData.sunLight = this.GetCurrentSunLight();
				renderSkyPassData.hdCamera = hdCamera;
				renderSkyPassData.volumetricLighting = renderGraphBuilder.ReadTexture(in volumetricLighting);
				renderSkyPassData.colorBuffer = renderGraphBuilder.WriteTexture(in colorBuffer);
				renderSkyPassData.depthStencilBuffer = renderGraphBuilder.WriteTexture(in depthStencilBuffer);
				HDRenderPipeline.RenderSkyPassData renderSkyPassData2 = renderSkyPassData;
				RenderGraphResource renderGraphResource = colorBuffer;
				RenderGraphMutableResource renderGraphMutableResource = renderGraph.CreateTexture(in renderGraphResource, 0);
				renderSkyPassData2.intermediateBuffer = renderGraphBuilder.WriteTexture(in renderGraphMutableResource);
				renderSkyPassData.debugDisplaySettings = this.m_CurrentDebugDisplaySettings;
				renderSkyPassData.skyManager = this.m_SkyManager;
				renderSkyPassData.frameCount = this.m_FrameCount;
				renderGraphBuilder.ReadTexture(in depthTexture);
				renderGraphBuilder.SetRenderFunc<HDRenderPipeline.RenderSkyPassData>(delegate(HDRenderPipeline.RenderSkyPassData data, RenderGraphContext context)
				{
					RenderGraphResourceRegistry resources = context.resources;
					RenderGraphResource renderGraphResource2 = data.depthStencilBuffer;
					RTHandle texture = resources.GetTexture(in renderGraphResource2);
					RenderGraphResourceRegistry resources2 = context.resources;
					renderGraphResource2 = data.colorBuffer;
					RTHandle texture2 = resources2.GetTexture(in renderGraphResource2);
					RenderGraphResourceRegistry resources3 = context.resources;
					renderGraphResource2 = data.intermediateBuffer;
					RTHandle texture3 = resources3.GetTexture(in renderGraphResource2);
					RTHandle texture4 = context.resources.GetTexture(in data.volumetricLighting);
					data.skyManager.RenderSky(data.hdCamera, data.sunLight, texture2, texture, data.debugDisplaySettings, data.frameCount, context.cmd);
					if (Fog.IsFogEnabled(data.hdCamera) || Fog.IsPBRFogEnabled(data.hdCamera))
					{
						Matrix4x4 pixelCoordToViewDirWS = data.hdCamera.mainViewConstants.pixelCoordToViewDirWS;
						data.skyManager.RenderOpaqueAtmosphericScattering(context.cmd, data.hdCamera, texture2, texture4, texture3, texture, pixelCoordToViewDirWS, data.hdCamera.frameSettings.IsEnabled(FrameSettingsField.MSAA));
					}
				});
			}
		}

		// Token: 0x060003A3 RID: 931 RVA: 0x0001E91C File Offset: 0x0001CB1C
		private void GenerateColorPyramid(RenderGraph renderGraph, HDCamera hdCamera, RenderGraphResource inputColor, RenderGraphMutableResource output, bool isPreRefraction)
		{
			if (isPreRefraction)
			{
				if (!hdCamera.frameSettings.IsEnabled(FrameSettingsField.Refraction))
				{
					return;
				}
			}
			else if (!hdCamera.frameSettings.IsEnabled(FrameSettingsField.Distortion) && !hdCamera.frameSettings.IsEnabled(FrameSettingsField.SSR))
			{
				return;
			}
			HDRenderPipeline.GenerateColorPyramidData generateColorPyramidData;
			using (RenderGraphBuilder renderGraphBuilder = renderGraph.AddRenderPass<HDRenderPipeline.GenerateColorPyramidData>("Color Gaussian MIP Chain", out generateColorPyramidData, ProfilingSampler.Get<HDProfileId>(HDProfileId.ColorPyramid)))
			{
				generateColorPyramidData.colorPyramid = renderGraphBuilder.WriteTexture(in output);
				generateColorPyramidData.inputColor = renderGraphBuilder.ReadTexture(in inputColor);
				generateColorPyramidData.hdCamera = hdCamera;
				generateColorPyramidData.mipGenerator = this.m_MipGenerator;
				renderGraphBuilder.SetRenderFunc<HDRenderPipeline.GenerateColorPyramidData>(delegate(HDRenderPipeline.GenerateColorPyramidData data, RenderGraphContext context)
				{
					Vector2Int vector2Int = new Vector2Int(data.hdCamera.actualWidth, data.hdCamera.actualHeight);
					RenderGraphResourceRegistry resources = context.resources;
					RenderGraphResource renderGraphResource = data.colorPyramid;
					RTHandle texture = resources.GetTexture(in renderGraphResource);
					RTHandle texture2 = context.resources.GetTexture(in data.inputColor);
					data.hdCamera.colorPyramidHistoryMipCount = data.mipGenerator.RenderColorGaussianPyramid(context.cmd, vector2Int, texture2, texture);
					float num = (float)data.hdCamera.actualWidth / (float)texture.rt.width;
					float num2 = (float)data.hdCamera.actualHeight / (float)texture.rt.height;
					Vector4 vector2 = new Vector4(num, num2, (float)data.hdCamera.colorPyramidHistoryMipCount, 0f);
					context.cmd.SetGlobalVector(HDShaderIDs._ColorPyramidScale, vector2);
				});
			}
			Vector4 vector = new Vector4(renderGraph.rtHandleProperties.rtHandleScale.x, renderGraph.rtHandleProperties.rtHandleScale.y, 0f, 0f);
			this.PushFullScreenDebugTextureMip(renderGraph, output, hdCamera.colorPyramidHistoryMipCount, vector, isPreRefraction ? FullScreenDebugMode.PreRefractionColorPyramid : FullScreenDebugMode.FinalColorPyramid);
		}

		// Token: 0x060003A4 RID: 932 RVA: 0x0001EA40 File Offset: 0x0001CC40
		private RenderGraphResource AccumulateDistortion(RenderGraph renderGraph, HDCamera hdCamera, RenderGraphMutableResource depthStencilBuffer, CullingResults cullResults)
		{
			HDRenderPipeline.AccumulateDistortionPassData accumulateDistortionPassData;
			RenderGraphResource renderGraphResource;
			using (RenderGraphBuilder renderGraphBuilder = renderGraph.AddRenderPass<HDRenderPipeline.AccumulateDistortionPassData>("Accumulate Distortion", out accumulateDistortionPassData, ProfilingSampler.Get<HDProfileId>(HDProfileId.Distortion)))
			{
				accumulateDistortionPassData.frameSettings = hdCamera.frameSettings;
				HDRenderPipeline.AccumulateDistortionPassData accumulateDistortionPassData2 = accumulateDistortionPassData;
				RenderGraphMutableResource renderGraphMutableResource = renderGraph.CreateTexture(new TextureDesc(Vector2.one, true, true)
				{
					colorFormat = Builtin.GetDistortionBufferFormat(),
					clearBuffer = true,
					clearColor = Color.clear,
					name = "Distortion"
				}, 0);
				accumulateDistortionPassData2.distortionBuffer = renderGraphBuilder.UseColorBuffer(in renderGraphMutableResource, 0);
				accumulateDistortionPassData.depthStencilBuffer = renderGraphBuilder.UseDepthBuffer(in depthStencilBuffer, DepthAccess.Write);
				HDRenderPipeline.AccumulateDistortionPassData accumulateDistortionPassData3 = accumulateDistortionPassData;
				RendererListDesc rendererListDesc = HDRenderPipeline.CreateTransparentRendererListDesc(cullResults, hdCamera.camera, HDShaderPassNames.s_DistortionVectorsName, PerObjectData.None, null, null, null, false);
				renderGraphResource = renderGraph.CreateRendererList(in rendererListDesc);
				accumulateDistortionPassData3.distortionRendererList = renderGraphBuilder.UseRendererList(in renderGraphResource);
				renderGraphBuilder.SetRenderFunc<HDRenderPipeline.AccumulateDistortionPassData>(delegate(HDRenderPipeline.AccumulateDistortionPassData data, RenderGraphContext context)
				{
					HDRenderPipeline.DrawTransparentRendererList(in context.renderContext, context.cmd, in data.frameSettings, context.resources.GetRendererList(in data.distortionRendererList));
				});
				renderGraphResource = accumulateDistortionPassData.distortionBuffer;
			}
			return renderGraphResource;
		}

		// Token: 0x060003A5 RID: 933 RVA: 0x0001EB6C File Offset: 0x0001CD6C
		private void RenderDistortion(RenderGraph renderGraph, HDCamera hdCamera, RenderGraphMutableResource colorBuffer, RenderGraphMutableResource depthStencilBuffer, RenderGraphResource colorPyramidBuffer, RenderGraphResource distortionBuffer)
		{
			if (!hdCamera.frameSettings.IsEnabled(FrameSettingsField.Distortion))
			{
				return;
			}
			HDRenderPipeline.RenderDistortionPassData renderDistortionPassData;
			using (RenderGraphBuilder renderGraphBuilder = renderGraph.AddRenderPass<HDRenderPipeline.RenderDistortionPassData>("Apply Distortion", out renderDistortionPassData, ProfilingSampler.Get<HDProfileId>(HDProfileId.ApplyDistortion)))
			{
				renderDistortionPassData.applyDistortionMaterial = this.m_ApplyDistortionMaterial;
				renderDistortionPassData.colorPyramidBuffer = renderGraphBuilder.ReadTexture(in colorPyramidBuffer);
				renderDistortionPassData.distortionBuffer = renderGraphBuilder.ReadTexture(in distortionBuffer);
				renderDistortionPassData.colorBuffer = renderGraphBuilder.UseColorBuffer(in colorBuffer, 0);
				renderDistortionPassData.depthStencilBuffer = renderGraphBuilder.UseDepthBuffer(in depthStencilBuffer, DepthAccess.Read);
				renderDistortionPassData.size = new Vector4((float)hdCamera.actualWidth, (float)hdCamera.actualHeight, 1f / (float)hdCamera.actualWidth, 1f / (float)hdCamera.actualHeight);
				renderGraphBuilder.SetRenderFunc<HDRenderPipeline.RenderDistortionPassData>(delegate(HDRenderPipeline.RenderDistortionPassData data, RenderGraphContext context)
				{
					RenderGraphResourceRegistry resources = context.resources;
					data.applyDistortionMaterial.SetTexture(HDShaderIDs._DistortionTexture, resources.GetTexture(in data.distortionBuffer));
					data.applyDistortionMaterial.SetTexture(HDShaderIDs._ColorPyramidTexture, resources.GetTexture(in data.colorPyramidBuffer));
					data.applyDistortionMaterial.SetVector(HDShaderIDs._Size, data.size);
					CommandBuffer cmd = context.cmd;
					Material applyDistortionMaterial = data.applyDistortionMaterial;
					RenderGraphResourceRegistry renderGraphResourceRegistry = resources;
					RenderGraphResource renderGraphResource = data.colorBuffer;
					HDUtils.DrawFullScreen(cmd, applyDistortionMaterial, renderGraphResourceRegistry.GetTexture(in renderGraphResource), resources.GetTexture(in data.depthStencilBuffer), null, 0);
				});
			}
		}

		// Token: 0x060003A6 RID: 934 RVA: 0x0001EC6C File Offset: 0x0001CE6C
		private RenderGraphMutableResource CreateColorBuffer(RenderGraph renderGraph, HDCamera hdCamera, bool msaa)
		{
			return renderGraph.CreateTexture(new TextureDesc(Vector2.one, true, true)
			{
				colorFormat = this.GetColorBufferFormat(),
				enableRandomWrite = !msaa,
				bindTextureMS = msaa,
				enableMSAA = msaa,
				clearBuffer = this.NeedClearColorBuffer(hdCamera),
				clearColor = this.GetColorBufferClearColor(hdCamera),
				name = string.Format("CameraColor{0}", msaa ? "MSAA" : "")
			}, 0);
		}

		// Token: 0x060003A7 RID: 935 RVA: 0x0001ECF4 File Offset: 0x0001CEF4
		private RenderGraphMutableResource ResolveMSAAColor(RenderGraph renderGraph, HDCamera hdCamera, RenderGraphMutableResource input)
		{
			if (hdCamera.frameSettings.IsEnabled(FrameSettingsField.MSAA))
			{
				HDRenderPipeline.ResolveColorData resolveColorData;
				using (RenderGraphBuilder renderGraphBuilder = renderGraph.AddRenderPass<HDRenderPipeline.ResolveColorData>("ResolveColor", out resolveColorData, null))
				{
					RenderGraphResource renderGraphResource = input;
					TextureDesc textureDesc = renderGraph.GetTextureDesc(in renderGraphResource);
					textureDesc.enableMSAA = false;
					textureDesc.enableRandomWrite = true;
					textureDesc.bindTextureMS = false;
					textureDesc.name = string.Format("{0}Resolved", textureDesc.name);
					HDRenderPipeline.ResolveColorData resolveColorData2 = resolveColorData;
					renderGraphResource = input;
					resolveColorData2.input = renderGraphBuilder.ReadTexture(in renderGraphResource);
					HDRenderPipeline.ResolveColorData resolveColorData3 = resolveColorData;
					RenderGraphMutableResource renderGraphMutableResource = renderGraph.CreateTexture(textureDesc, 0);
					resolveColorData3.output = renderGraphBuilder.UseColorBuffer(in renderGraphMutableResource, 0);
					resolveColorData.resolveMaterial = this.m_ColorResolveMaterial;
					resolveColorData.passIndex = HDRenderPipeline.SampleCountToPassIndex(this.m_MSAASamples);
					renderGraphBuilder.SetRenderFunc<HDRenderPipeline.ResolveColorData>(delegate(HDRenderPipeline.ResolveColorData data, RenderGraphContext context)
					{
						RenderGraphResourceRegistry resources = context.resources;
						MaterialPropertyBlock tempMaterialPropertyBlock = context.renderGraphPool.GetTempMaterialPropertyBlock();
						tempMaterialPropertyBlock.SetTexture(HDShaderIDs._ColorTextureMS, resources.GetTexture(in data.input));
						context.cmd.DrawProcedural(Matrix4x4.identity, data.resolveMaterial, data.passIndex, MeshTopology.Triangles, 3, 1, tempMaterialPropertyBlock);
					});
					return resolveColorData.output;
				}
				return input;
			}
			return input;
		}

		// Token: 0x060003A8 RID: 936 RVA: 0x00002646 File Offset: 0x00000846
		private void RenderGizmos(RenderGraph renderGraph, HDCamera hdCamera, RenderGraphMutableResource colorBuffer, GizmoSubset gizmoSubset)
		{
		}

		// Token: 0x060003A9 RID: 937 RVA: 0x0001EE08 File Offset: 0x0001D008
		private static void DrawOpaqueRendererList(in RenderGraphContext context, in FrameSettings frameSettings, in RendererList rendererList)
		{
			HDRenderPipeline.DrawOpaqueRendererList(in context.renderContext, context.cmd, in frameSettings, rendererList);
		}

		// Token: 0x060003AA RID: 938 RVA: 0x0001EE22 File Offset: 0x0001D022
		private static void DrawTransparentRendererList(in RenderGraphContext context, in FrameSettings frameSettings, RendererList rendererList)
		{
			HDRenderPipeline.DrawTransparentRendererList(in context.renderContext, context.cmd, in frameSettings, rendererList);
		}

		// Token: 0x060003AB RID: 939 RVA: 0x0001EE37 File Offset: 0x0001D037
		private static int SampleCountToPassIndex(MSAASamples samples)
		{
			switch (samples)
			{
			case MSAASamples.None:
				return 0;
			case MSAASamples.MSAA2x:
				return 1;
			case (MSAASamples)3:
				break;
			case MSAASamples.MSAA4x:
				return 2;
			default:
				if (samples == MSAASamples.MSAA8x)
				{
					return 3;
				}
				break;
			}
			return 0;
		}

		// Token: 0x060003AC RID: 940 RVA: 0x0001EE60 File Offset: 0x0001D060
		private bool NeedClearColorBuffer(HDCamera hdCamera)
		{
			return hdCamera.clearColorMode == HDAdditionalCameraData.ClearColorMode.Color || this.m_CurrentDebugDisplaySettings.data.lightingDebugSettings.debugLightingMode == DebugLightingMode.LuxMeter || this.m_CurrentDebugDisplaySettings.IsMatcapViewEnabled(hdCamera) || (hdCamera.clearColorMode == HDAdditionalCameraData.ClearColorMode.Sky && !this.m_SkyManager.IsVisualSkyValid(hdCamera)) || HDUtils.IsRegularPreviewCamera(hdCamera.camera);
		}

		// Token: 0x060003AD RID: 941 RVA: 0x0001EEC4 File Offset: 0x0001D0C4
		private Color GetColorBufferClearColor(HDCamera hdCamera)
		{
			Color color = hdCamera.backgroundColorHDR;
			if (this.m_CurrentDebugDisplaySettings.data.lightingDebugSettings.debugLightingMode == DebugLightingMode.LuxMeter || this.m_CurrentDebugDisplaySettings.IsMatcapViewEnabled(hdCamera))
			{
				color = Color.black;
			}
			return color;
		}

		// Token: 0x060003AE RID: 942 RVA: 0x0001EF08 File Offset: 0x0001D108
		internal static void StartXRSinglePass(RenderGraph renderGraph, HDCamera hdCamera)
		{
			if (hdCamera.xr.enabled)
			{
				HDRenderPipeline.XRRenderingPassData xrrenderingPassData;
				using (RenderGraphBuilder renderGraphBuilder = renderGraph.AddRenderPass<HDRenderPipeline.XRRenderingPassData>("Start XR single-pass", out xrrenderingPassData, null))
				{
					xrrenderingPassData.camera = hdCamera.camera;
					xrrenderingPassData.xr = hdCamera.xr;
					renderGraphBuilder.SetRenderFunc<HDRenderPipeline.XRRenderingPassData>(delegate(HDRenderPipeline.XRRenderingPassData data, RenderGraphContext context)
					{
						data.xr.StartSinglePass(context.cmd, data.camera, context.renderContext);
					});
				}
			}
		}

		// Token: 0x060003AF RID: 943 RVA: 0x0001EF90 File Offset: 0x0001D190
		internal static void StopXRSinglePass(RenderGraph renderGraph, HDCamera hdCamera)
		{
			if (hdCamera.xr.enabled)
			{
				HDRenderPipeline.XRRenderingPassData xrrenderingPassData;
				using (RenderGraphBuilder renderGraphBuilder = renderGraph.AddRenderPass<HDRenderPipeline.XRRenderingPassData>("Stop XR single-pass", out xrrenderingPassData, null))
				{
					xrrenderingPassData.camera = hdCamera.camera;
					xrrenderingPassData.xr = hdCamera.xr;
					renderGraphBuilder.SetRenderFunc<HDRenderPipeline.XRRenderingPassData>(delegate(HDRenderPipeline.XRRenderingPassData data, RenderGraphContext context)
					{
						data.xr.StopSinglePass(context.cmd, data.camera, context.renderContext);
					});
				}
			}
		}

		// Token: 0x060003B0 RID: 944 RVA: 0x0001F018 File Offset: 0x0001D218
		private void EndCameraXR(RenderGraph renderGraph, HDCamera hdCamera)
		{
			if (hdCamera.xr.enabled)
			{
				HDRenderPipeline.EndCameraXRPassData endCameraXRPassData;
				using (RenderGraphBuilder renderGraphBuilder = renderGraph.AddRenderPass<HDRenderPipeline.EndCameraXRPassData>("End Camera", out endCameraXRPassData, null))
				{
					endCameraXRPassData.hdCamera = hdCamera;
					renderGraphBuilder.SetRenderFunc<HDRenderPipeline.EndCameraXRPassData>(delegate(HDRenderPipeline.EndCameraXRPassData data, RenderGraphContext ctx)
					{
						data.hdCamera.xr.EndCamera(ctx.cmd, data.hdCamera, ctx.renderContext);
					});
				}
			}
		}

		// Token: 0x060003B1 RID: 945 RVA: 0x0001F090 File Offset: 0x0001D290
		private void RenderXROcclusionMeshes(RenderGraph renderGraph, HDCamera hdCamera, RenderGraphMutableResource depthBuffer)
		{
			if (hdCamera.xr.enabled && this.m_Asset.currentPlatformRenderPipelineSettings.xrSettings.occlusionMesh)
			{
				HDRenderPipeline.RenderOcclusionMeshesPassData renderOcclusionMeshesPassData;
				using (RenderGraphBuilder renderGraphBuilder = renderGraph.AddRenderPass<HDRenderPipeline.RenderOcclusionMeshesPassData>("XR Occlusion Meshes", out renderOcclusionMeshesPassData, null))
				{
					renderOcclusionMeshesPassData.hdCamera = hdCamera;
					renderOcclusionMeshesPassData.depthBuffer = renderGraphBuilder.UseDepthBuffer(in depthBuffer, DepthAccess.Write);
					renderGraphBuilder.SetRenderFunc<HDRenderPipeline.RenderOcclusionMeshesPassData>(delegate(HDRenderPipeline.RenderOcclusionMeshesPassData data, RenderGraphContext ctx)
					{
						XRPass xr = data.hdCamera.xr;
						CommandBuffer cmd = ctx.cmd;
						RenderGraphResourceRegistry resources = ctx.resources;
						RenderGraphResource renderGraphResource = data.depthBuffer;
						xr.RenderOcclusionMeshes(cmd, resources.GetTexture(in renderGraphResource));
					});
				}
			}
		}

		// Token: 0x060003B2 RID: 946 RVA: 0x0001F130 File Offset: 0x0001D330
		private RenderGraphMutableResource CreateSSSBuffer(RenderGraph renderGraph, bool msaa)
		{
			return renderGraph.CreateTexture(new TextureDesc(Vector2.one, true, true)
			{
				colorFormat = GraphicsFormat.R8G8B8A8_SRGB,
				enableRandomWrite = !msaa,
				bindTextureMS = msaa,
				enableMSAA = msaa,
				clearBuffer = this.NeedClearGBuffer(),
				clearColor = Color.clear,
				name = string.Format("SSSBuffer{0}", msaa ? "MSAA" : "")
			}, 0);
		}

		// Token: 0x060003B3 RID: 947 RVA: 0x0001F1B0 File Offset: 0x0001D3B0
		private void RenderSubsurfaceScattering(RenderGraph renderGraph, HDCamera hdCamera, RenderGraphMutableResource colorBuffer, in HDRenderPipeline.LightingBuffers lightingBuffers, RenderGraphResource depthStencilBuffer, RenderGraphResource depthTexture)
		{
			if (!hdCamera.frameSettings.IsEnabled(FrameSettingsField.SubsurfaceScattering))
			{
				return;
			}
			HDRenderPipeline.SubsurfaceScaterringPassData subsurfaceScaterringPassData;
			using (RenderGraphBuilder renderGraphBuilder = renderGraph.AddRenderPass<HDRenderPipeline.SubsurfaceScaterringPassData>("Subsurface Scattering", out subsurfaceScaterringPassData, ProfilingSampler.Get<HDProfileId>(HDProfileId.SubsurfaceScattering)))
			{
				subsurfaceScaterringPassData.parameters = this.PrepareSubsurfaceScatteringParameters(hdCamera);
				subsurfaceScaterringPassData.colorBuffer = renderGraphBuilder.WriteTexture(in colorBuffer);
				HDRenderPipeline.SubsurfaceScaterringPassData subsurfaceScaterringPassData2 = subsurfaceScaterringPassData;
				RenderGraphResource renderGraphResource = lightingBuffers.diffuseLightingBuffer;
				subsurfaceScaterringPassData2.diffuseBuffer = renderGraphBuilder.ReadTexture(in renderGraphResource);
				subsurfaceScaterringPassData.depthStencilBuffer = renderGraphBuilder.ReadTexture(in depthStencilBuffer);
				subsurfaceScaterringPassData.depthTexture = renderGraphBuilder.ReadTexture(in depthTexture);
				HDRenderPipeline.SubsurfaceScaterringPassData subsurfaceScaterringPassData3 = subsurfaceScaterringPassData;
				renderGraphResource = lightingBuffers.sssBuffer;
				subsurfaceScaterringPassData3.sssBuffer = renderGraphBuilder.ReadTexture(in renderGraphResource);
				if (subsurfaceScaterringPassData.parameters.needTemporaryBuffer)
				{
					HDRenderPipeline.SubsurfaceScaterringPassData subsurfaceScaterringPassData4 = subsurfaceScaterringPassData;
					RenderGraphMutableResource renderGraphMutableResource = renderGraph.CreateTexture(new TextureDesc(Vector2.one, true, true)
					{
						colorFormat = GraphicsFormat.B10G11R11_UFloatPack32,
						enableRandomWrite = true,
						clearBuffer = true,
						clearColor = Color.clear,
						name = "SSSCameraFiltering"
					}, 0);
					subsurfaceScaterringPassData4.cameraFilteringBuffer = renderGraphBuilder.WriteTexture(in renderGraphMutableResource);
				}
				renderGraphBuilder.SetRenderFunc<HDRenderPipeline.SubsurfaceScaterringPassData>(delegate(HDRenderPipeline.SubsurfaceScaterringPassData data, RenderGraphContext context)
				{
					HDRenderPipeline.SubsurfaceScatteringResources subsurfaceScatteringResources = default(HDRenderPipeline.SubsurfaceScatteringResources);
					subsurfaceScatteringResources.colorBuffer = context.resources.GetTexture(in data.colorBuffer);
					subsurfaceScatteringResources.diffuseBuffer = context.resources.GetTexture(in data.diffuseBuffer);
					subsurfaceScatteringResources.depthStencilBuffer = context.resources.GetTexture(in data.depthStencilBuffer);
					subsurfaceScatteringResources.depthTexture = context.resources.GetTexture(in data.depthTexture);
					RenderGraphResourceRegistry resources = context.resources;
					RenderGraphResource renderGraphResource2 = data.cameraFilteringBuffer;
					subsurfaceScatteringResources.cameraFilteringBuffer = resources.GetTexture(in renderGraphResource2);
					subsurfaceScatteringResources.sssBuffer = context.resources.GetTexture(in data.sssBuffer);
					HDRenderPipeline.RenderSubsurfaceScattering(in data.parameters, in subsurfaceScatteringResources, context.cmd);
				});
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x060003B4 RID: 948 RVA: 0x0001F314 File Offset: 0x0001D514
		internal static HDRenderPipelineAsset defaultAsset
		{
			get
			{
				HDRenderPipelineAsset hdrenderPipelineAsset;
				if ((hdrenderPipelineAsset = GraphicsSettings.renderPipelineAsset as HDRenderPipelineAsset) == null)
				{
					return null;
				}
				return hdrenderPipelineAsset;
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x060003B5 RID: 949 RVA: 0x0001F334 File Offset: 0x0001D534
		internal static HDRenderPipelineAsset currentAsset
		{
			get
			{
				HDRenderPipelineAsset hdrenderPipelineAsset;
				if ((hdrenderPipelineAsset = GraphicsSettings.currentRenderPipeline as HDRenderPipelineAsset) == null)
				{
					return null;
				}
				return hdrenderPipelineAsset;
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x060003B6 RID: 950 RVA: 0x0001F354 File Offset: 0x0001D554
		internal static HDRenderPipeline currentPipeline
		{
			get
			{
				HDRenderPipeline hdrenderPipeline;
				if ((hdrenderPipeline = RenderPipelineManager.currentPipeline as HDRenderPipeline) == null)
				{
					return null;
				}
				return hdrenderPipeline;
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x060003B7 RID: 951 RVA: 0x0001F372 File Offset: 0x0001D572
		internal static bool pipelineSupportsRayTracing
		{
			get
			{
				return HDRenderPipeline.currentPipeline != null && HDRenderPipeline.currentPipeline.rayTracingSupported;
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x060003B8 RID: 952 RVA: 0x0001F387 File Offset: 0x0001D587
		private static VolumeProfile defaultVolumeProfile
		{
			get
			{
				HDRenderPipelineAsset defaultAsset = HDRenderPipeline.defaultAsset;
				if (defaultAsset == null)
				{
					return null;
				}
				return defaultAsset.defaultVolumeProfile;
			}
		}

		// Token: 0x060003B9 RID: 953 RVA: 0x0001F39C File Offset: 0x0001D59C
		static HDRenderPipeline()
		{
			string[,] array = new string[2, 3];
			array[0, 0] = "TileLightListGen_NoDepthRT";
			array[0, 1] = "TileLightListGen_DepthRT";
			array[0, 2] = "TileLightListGen_DepthRT_MSAA";
			array[1, 0] = "TileLightListGen_NoDepthRT_SrcBigTile";
			array[1, 1] = "TileLightListGen_DepthRT_SrcBigTile";
			array[1, 2] = "TileLightListGen_DepthRT_MSAA_SrcBigTile";
			HDRenderPipeline.s_ClusterKernelNames = array;
			string[,] array2 = new string[2, 3];
			array2[0, 0] = "TileLightListGen_NoDepthRT";
			array2[0, 1] = "TileLightListGen_DepthRT_Oblique";
			array2[0, 2] = "TileLightListGen_DepthRT_MSAA_Oblique";
			array2[1, 0] = "TileLightListGen_NoDepthRT_SrcBigTile";
			array2[1, 1] = "TileLightListGen_DepthRT_SrcBigTile_Oblique";
			array2[1, 2] = "TileLightListGen_DepthRT_MSAA_SrcBigTile_Oblique";
			HDRenderPipeline.s_ClusterObliqueKernelNames = array2;
			HDRenderPipeline.s_TempScreenDimArray = new int[2];
			HDRenderPipeline.m_LightLoopDebugMaterialProperties = new MaterialPropertyBlock();
			HDRenderPipeline.s_DefaultVolume = null;
			HDRenderPipeline.s_NeutralDebugDisplaySettings = new DebugDisplaySettings();
			HDRenderPipeline.m_Dbuffer3RtIds = new RenderTargetIdentifier[3];
		}

		// Token: 0x060003BA RID: 954 RVA: 0x0001F500 File Offset: 0x0001D700
		private static Volume GetOrCreateDefaultVolume()
		{
			if (HDRenderPipeline.s_DefaultVolume == null || HDRenderPipeline.s_DefaultVolume.Equals(null))
			{
				HDRenderPipeline.s_DefaultVolume = new GameObject("Default Volume")
				{
					hideFlags = HideFlags.HideAndDontSave
				}.AddComponent<Volume>();
				HDRenderPipeline.s_DefaultVolume.isGlobal = true;
				HDRenderPipeline.s_DefaultVolume.priority = float.MinValue;
				HDRenderPipeline.s_DefaultVolume.sharedProfile = HDRenderPipeline.defaultVolumeProfile;
			}
			if (HDRenderPipeline.s_DefaultVolume.sharedProfile == null || HDRenderPipeline.s_DefaultVolume.sharedProfile.Equals(null))
			{
				HDRenderPipeline.s_DefaultVolume.sharedProfile = HDRenderPipeline.defaultVolumeProfile;
			}
			return HDRenderPipeline.s_DefaultVolume;
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x060003BB RID: 955 RVA: 0x0001F5A4 File Offset: 0x0001D7A4
		internal HDRenderPipelineAsset asset
		{
			get
			{
				return this.m_Asset;
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x060003BC RID: 956 RVA: 0x0001F5AC File Offset: 0x0001D7AC
		internal RenderPipelineResources defaultResources
		{
			get
			{
				return this.m_DefaultAsset.renderPipelineResources;
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x060003BD RID: 957 RVA: 0x0001F5B9 File Offset: 0x0001D7B9
		internal RenderPipelineSettings currentPlatformRenderPipelineSettings
		{
			get
			{
				return this.m_Asset.currentPlatformRenderPipelineSettings;
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x060003BE RID: 958 RVA: 0x0001F5C6 File Offset: 0x0001D7C6
		internal SharedRTManager sharedRTManager
		{
			get
			{
				return this.m_SharedRTManager;
			}
		}

		// Token: 0x060003BF RID: 959 RVA: 0x0001F5CE File Offset: 0x0001D7CE
		public uint GetRaysPerFrame(RayCountValues rayValues)
		{
			return this.m_RayCountManager.GetRaysPerFrame(rayValues);
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x060003C0 RID: 960 RVA: 0x0001F5DC File Offset: 0x0001D7DC
		private ComputeShader m_ScreenSpaceReflectionsCS
		{
			get
			{
				return this.defaultResources.shaders.screenSpaceReflectionsCS;
			}
		}

		// Token: 0x060003C1 RID: 961 RVA: 0x0001F5EE File Offset: 0x0001D7EE
		internal int GetFrameCount()
		{
			return this.m_FrameCount;
		}

		// Token: 0x060003C2 RID: 962 RVA: 0x0001F5F6 File Offset: 0x0001D7F6
		internal float GetLastTime()
		{
			return this.m_LastTime;
		}

		// Token: 0x060003C3 RID: 963 RVA: 0x0001F5FE File Offset: 0x0001D7FE
		internal float GetTime()
		{
			return this.m_Time;
		}

		// Token: 0x060003C4 RID: 964 RVA: 0x0001F606 File Offset: 0x0001D806
		private GraphicsFormat GetColorBufferFormat()
		{
			return (GraphicsFormat)this.m_Asset.currentPlatformRenderPipelineSettings.colorBufferFormat;
		}

		// Token: 0x060003C5 RID: 965 RVA: 0x0001F618 File Offset: 0x0001D818
		private GraphicsFormat GetCustomBufferFormat()
		{
			return (GraphicsFormat)this.m_Asset.currentPlatformRenderPipelineSettings.customBufferFormat;
		}

		// Token: 0x060003C6 RID: 966 RVA: 0x0001F62A File Offset: 0x0001D82A
		internal int GetDecalAtlasMipCount()
		{
			return (int)Math.Log((double)Math.Max(this.currentPlatformRenderPipelineSettings.decalSettings.atlasWidth, this.currentPlatformRenderPipelineSettings.decalSettings.atlasHeight), 2.0);
		}

		// Token: 0x060003C7 RID: 967 RVA: 0x0001F661 File Offset: 0x0001D861
		internal int GetCookieAtlasMipCount()
		{
			return (int)Mathf.Log((float)this.currentPlatformRenderPipelineSettings.lightLoopSettings.cookieAtlasSize, 2f);
		}

		// Token: 0x060003C8 RID: 968 RVA: 0x0001F67F File Offset: 0x0001D87F
		internal int GetCookieCubeArraySize()
		{
			return this.currentPlatformRenderPipelineSettings.lightLoopSettings.cubeCookieTexArraySize;
		}

		// Token: 0x060003C9 RID: 969 RVA: 0x0001F691 File Offset: 0x0001D891
		internal int GetPlanarReflectionProbeMipCount()
		{
			return (int)Mathf.Log((float)this.currentPlatformRenderPipelineSettings.lightLoopSettings.planarReflectionAtlasSize, 2f);
		}

		// Token: 0x060003CA RID: 970 RVA: 0x0001F6AF File Offset: 0x0001D8AF
		internal int GetMaxScreenSpaceShadows()
		{
			if (!this.currentPlatformRenderPipelineSettings.hdShadowInitParams.supportScreenSpaceShadows)
			{
				return 0;
			}
			return this.currentPlatformRenderPipelineSettings.hdShadowInitParams.maxScreenSpaceShadowSlots;
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x060003CB RID: 971 RVA: 0x0001F6D5 File Offset: 0x0001D8D5
		public DebugDisplaySettings debugDisplaySettings
		{
			get
			{
				return this.m_DebugDisplaySettings;
			}
		}

		// Token: 0x060003CC RID: 972 RVA: 0x0001F6DD File Offset: 0x0001D8DD
		internal Material GetBlitMaterial(bool useTexArray, bool singleSlice)
		{
			if (!useTexArray)
			{
				return this.m_Blit;
			}
			if (!singleSlice)
			{
				return this.m_BlitTexArray;
			}
			return this.m_BlitTexArraySingleSlice;
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x060003CD RID: 973 RVA: 0x0001F6F9 File Offset: 0x0001D8F9
		// (set) Token: 0x060003CE RID: 974 RVA: 0x0001F709 File Offset: 0x0001D909
		internal bool showCascade
		{
			get
			{
				return this.m_CurrentDebugDisplaySettings.GetDebugLightingMode() == DebugLightingMode.VisualizeCascade;
			}
			set
			{
				if (value)
				{
					this.m_CurrentDebugDisplaySettings.SetDebugLightingMode(DebugLightingMode.VisualizeCascade);
					return;
				}
				this.m_CurrentDebugDisplaySettings.SetDebugLightingMode(DebugLightingMode.None);
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x060003CF RID: 975 RVA: 0x0001F727 File Offset: 0x0001D927
		public bool rayTracingSupported
		{
			get
			{
				return this.m_RayTracingSupported;
			}
		}

		// Token: 0x060003D0 RID: 976 RVA: 0x0001F730 File Offset: 0x0001D930
		public HDRenderPipeline(HDRenderPipelineAsset asset, HDRenderPipelineAsset defaultAsset)
		{
			HDRenderPipeline <>4__this = this;
			this.m_Asset = asset;
			this.m_DefaultAsset = defaultAsset;
			HDProbeSystem.Parameters = asset.reflectionSystemParameters;
			DebugManager.instance.RefreshEditor();
			this.m_ValidAPI = true;
			if (!this.SetRenderingFeatures())
			{
				this.m_ValidAPI = false;
				return;
			}
			this.m_RayTracingSupported = HDRenderPipeline.GatherRayTracingSupport(this.m_Asset.currentPlatformRenderPipelineSettings);
			RTHandles.Initialize(1, 1, this.m_Asset.currentPlatformRenderPipelineSettings.supportMSAA, this.m_Asset.currentPlatformRenderPipelineSettings.msaaSampleCount);
			this.m_XRSystem = new XRSystem(asset.renderPipelineResources.shaders);
			this.m_GPUCopy = new GPUCopy(this.defaultResources.shaders.copyChannelCS);
			this.m_MipGenerator = new MipGenerator(this.defaultResources);
			this.m_BlueNoise = new BlueNoise(this.defaultResources);
			EncodeBC6H.DefaultInstance = EncodeBC6H.DefaultInstance ?? new EncodeBC6H(this.defaultResources.shaders.encodeBC6HCS);
			this.m_MaterialList = HDUtils.GetRenderPipelineMaterialList();
			this.m_DeferredMaterial = null;
			foreach (RenderPipelineMaterial renderPipelineMaterial in this.m_MaterialList)
			{
				if (renderPipelineMaterial.IsDefferedMaterial())
				{
					this.m_DeferredMaterial = renderPipelineMaterial;
				}
			}
			this.m_GbufferManager = new GBufferManager(asset, this.m_DeferredMaterial);
			this.m_DbufferManager = new DBufferManager();
			this.m_DbufferManager.InitializeHDRPResouces(asset);
			this.m_SharedRTManager.Build(asset);
			this.m_PostProcessSystem = new PostProcessSystem(asset, this.defaultResources);
			this.m_AmbientOcclusionSystem = new AmbientOcclusionSystem(asset, this.defaultResources);
			this.m_SsrTracingKernel = this.m_ScreenSpaceReflectionsCS.FindKernel("ScreenSpaceReflectionsTracing");
			this.m_SsrReprojectionKernel = this.m_ScreenSpaceReflectionsCS.FindKernel("ScreenSpaceReflectionsReprojection");
			this.m_CameraMotionVectorsMaterial = CoreUtils.CreateEngineMaterial(this.defaultResources.shaders.cameraMotionVectorsPS);
			this.m_DecalNormalBufferMaterial = CoreUtils.CreateEngineMaterial(this.defaultResources.shaders.decalNormalBufferPS);
			this.m_CopyDepth = CoreUtils.CreateEngineMaterial(this.defaultResources.shaders.copyDepthBufferPS);
			this.m_DownsampleDepthMaterial = CoreUtils.CreateEngineMaterial(this.defaultResources.shaders.downsampleDepthPS);
			this.m_UpsampleTransparency = CoreUtils.CreateEngineMaterial(this.defaultResources.shaders.upsampleTransparentPS);
			this.m_ApplyDistortionMaterial = CoreUtils.CreateEngineMaterial(this.defaultResources.shaders.applyDistortionPS);
			this.m_ClearStencilBufferMaterial = CoreUtils.CreateEngineMaterial(this.defaultResources.shaders.clearStencilBufferPS);
			this.InitializeDebugMaterials();
			this.m_MaterialList.ForEach(delegate(RenderPipelineMaterial material)
			{
				material.Build(asset, <>4__this.defaultResources);
			});
			if (this.m_Asset.currentPlatformRenderPipelineSettings.lightLoopSettings.supportFabricConvolution)
			{
				this.m_IBLFilterArray = new IBLFilterBSDF[2];
				this.m_IBLFilterArray[0] = new IBLFilterGGX(this.defaultResources, this.m_MipGenerator);
				this.m_IBLFilterArray[1] = new IBLFilterCharlie(this.defaultResources, this.m_MipGenerator);
			}
			else
			{
				this.m_IBLFilterArray = new IBLFilterBSDF[1];
				this.m_IBLFilterArray[0] = new IBLFilterGGX(this.defaultResources, this.m_MipGenerator);
			}
			this.InitializeLightLoop(this.m_IBLFilterArray);
			this.m_SkyManager.Build(asset, this.defaultResources, this.m_IBLFilterArray);
			this.InitializeVolumetricLighting();
			this.InitializeSubsurfaceScattering();
			this.m_DebugDisplaySettings.RegisterDebug();
			this.m_DepthPyramidMipLevelOffsetsBuffer = new ComputeBuffer(15, 8);
			this.InitializeRenderTextures();
			MousePositionDebug.instance.Build();
			this.InitializeRenderStateBlocks();
			this.m_MSAASamples = (this.m_Asset ? this.m_Asset.currentPlatformRenderPipelineSettings.msaaSampleCount : MSAASamples.None);
			this.m_DebugDisplaySettings.data.msaaSamples = this.m_MSAASamples;
			this.m_MRTTransparentMotionVec = new RenderTargetIdentifier[2];
			if (this.m_RayTracingSupported)
			{
				this.InitRayTracingManager();
				this.InitRayTracedReflections();
				this.InitRayTracedIndirectDiffuse();
				this.InitRaytracingDeferred();
				this.InitRecursiveRenderer();
				this.InitPathTracing();
				this.m_AmbientOcclusionSystem.InitRaytracing(this);
			}
			this.InitializeScreenSpaceShadows();
			CameraCaptureBridge.enabled = true;
			this.m_RenderGraph = new RenderGraph(this.m_Asset.currentPlatformRenderPipelineSettings.supportMSAA, this.m_MSAASamples);
			this.m_RenderGraph.RegisterDebug();
			this.InitializePrepass(this.m_Asset);
			this.m_ColorResolveMaterial = CoreUtils.CreateEngineMaterial(asset.renderPipelineResources.shaders.colorResolvePS);
		}

		// Token: 0x060003D1 RID: 977 RVA: 0x000200F8 File Offset: 0x0001E2F8
		private void InitializeRenderTextures()
		{
			RenderPipelineSettings currentPlatformRenderPipelineSettings = this.m_Asset.currentPlatformRenderPipelineSettings;
			if (currentPlatformRenderPipelineSettings.supportedLitShaderMode != RenderPipelineSettings.SupportedLitShaderMode.ForwardOnly)
			{
				this.m_GbufferManager.CreateBuffers();
			}
			if (currentPlatformRenderPipelineSettings.supportDecals)
			{
				this.m_DbufferManager.CreateBuffers();
			}
			this.InitSSSBuffers();
			this.m_SharedRTManager.InitSharedBuffers(this.m_GbufferManager, this.m_Asset.currentPlatformRenderPipelineSettings, this.defaultResources);
			Vector2 one = Vector2.one;
			int slices = TextureXR.slices;
			DepthBits depthBits = DepthBits.None;
			TextureDimension textureDimension = TextureXR.dimension;
			this.m_CameraColorBuffer = RTHandles.Alloc(one, slices, depthBits, this.GetColorBufferFormat(), FilterMode.Point, TextureWrapMode.Repeat, textureDimension, true, false, true, false, 1, 0f, false, false, true, RenderTextureMemoryless.None, "CameraColor");
			Vector2 one2 = Vector2.one;
			int slices2 = TextureXR.slices;
			DepthBits depthBits2 = DepthBits.None;
			textureDimension = TextureXR.dimension;
			this.m_OpaqueAtmosphericScatteringBuffer = RTHandles.Alloc(one2, slices2, depthBits2, this.GetColorBufferFormat(), FilterMode.Point, TextureWrapMode.Repeat, textureDimension, true, false, true, false, 1, 0f, false, false, true, RenderTextureMemoryless.None, "OpaqueAtmosphericScattering");
			this.m_CameraSssDiffuseLightingBuffer = RTHandles.Alloc(Vector2.one, TextureXR.slices, DepthBits.None, GraphicsFormat.B10G11R11_UFloatPack32, FilterMode.Point, TextureWrapMode.Repeat, TextureXR.dimension, true, false, true, false, 1, 0f, false, false, true, RenderTextureMemoryless.None, "CameraSSSDiffuseLighting");
			this.m_CustomPassColorBuffer = new Lazy<RTHandle>(delegate
			{
				Vector2 one7 = Vector2.one;
				int slices7 = TextureXR.slices;
				DepthBits depthBits7 = DepthBits.None;
				TextureDimension dimension = TextureXR.dimension;
				return RTHandles.Alloc(one7, slices7, depthBits7, this.GetCustomBufferFormat(), FilterMode.Point, TextureWrapMode.Repeat, dimension, true, false, true, false, 1, 0f, false, false, true, RenderTextureMemoryless.None, "CustomPassColorBuffer");
			});
			this.m_CustomPassDepthBuffer = new Lazy<RTHandle>(() => RTHandles.Alloc(Vector2.one, TextureXR.slices, DepthBits.Depth32, GraphicsFormat.R32_UInt, FilterMode.Point, TextureWrapMode.Repeat, TextureXR.dimension, true, false, true, true, 1, 0f, false, false, true, RenderTextureMemoryless.None, "CustomPassDepthBuffer"));
			Vector2 one3 = Vector2.one;
			int slices3 = TextureXR.slices;
			DepthBits depthBits3 = DepthBits.None;
			textureDimension = TextureXR.dimension;
			this.m_DistortionBuffer = RTHandles.Alloc(one3, slices3, depthBits3, Builtin.GetDistortionBufferFormat(), FilterMode.Point, TextureWrapMode.Repeat, textureDimension, false, false, true, false, 1, 0f, false, false, true, RenderTextureMemoryless.None, "Distortion");
			this.m_ContactShadowBuffer = RTHandles.Alloc(Vector2.one, TextureXR.slices, DepthBits.None, GraphicsFormat.R32_UInt, FilterMode.Point, TextureWrapMode.Repeat, TextureXR.dimension, true, false, true, false, 1, 0f, false, false, true, RenderTextureMemoryless.None, "ContactShadowsBuffer");
			if (this.m_Asset.currentPlatformRenderPipelineSettings.lowresTransparentSettings.enabled)
			{
				this.m_LowResTransparentBuffer = RTHandles.Alloc(Vector2.one * 0.5f, TextureXR.slices, DepthBits.None, GraphicsFormat.R16G16B16A16_SFloat, FilterMode.Point, TextureWrapMode.Repeat, TextureXR.dimension, true, false, true, false, 1, 0f, false, false, true, RenderTextureMemoryless.None, "Low res transparent");
			}
			if (currentPlatformRenderPipelineSettings.supportSSR)
			{
				this.m_SsrHitPointTexture = RTHandles.Alloc(Vector2.one, TextureXR.slices, DepthBits.None, GraphicsFormat.R16G16_UNorm, FilterMode.Point, TextureWrapMode.Repeat, TextureXR.dimension, true, false, true, false, 1, 0f, false, false, true, RenderTextureMemoryless.None, "SSR_Hit_Point_Texture");
				this.m_SsrLightingTexture = RTHandles.Alloc(Vector2.one, TextureXR.slices, DepthBits.None, GraphicsFormat.R16G16B16A16_SFloat, FilterMode.Point, TextureWrapMode.Repeat, TextureXR.dimension, true, false, true, false, 1, 0f, false, false, true, RenderTextureMemoryless.None, "SSR_Lighting_Texture");
			}
			if (this.m_Asset.currentPlatformRenderPipelineSettings.supportMSAA)
			{
				Vector2 one4 = Vector2.one;
				int slices4 = TextureXR.slices;
				DepthBits depthBits4 = DepthBits.None;
				textureDimension = TextureXR.dimension;
				this.m_CameraColorMSAABuffer = RTHandles.Alloc(one4, slices4, depthBits4, this.GetColorBufferFormat(), FilterMode.Point, TextureWrapMode.Repeat, textureDimension, false, false, true, false, 1, 0f, true, true, true, RenderTextureMemoryless.None, "CameraColorMSAA");
				Vector2 one5 = Vector2.one;
				int slices5 = TextureXR.slices;
				DepthBits depthBits5 = DepthBits.None;
				textureDimension = TextureXR.dimension;
				this.m_OpaqueAtmosphericScatteringMSAABuffer = RTHandles.Alloc(one5, slices5, depthBits5, this.GetColorBufferFormat(), FilterMode.Point, TextureWrapMode.Repeat, textureDimension, false, false, true, false, 1, 0f, true, true, true, RenderTextureMemoryless.None, "OpaqueAtmosphericScatteringMSAA");
				Vector2 one6 = Vector2.one;
				int slices6 = TextureXR.slices;
				DepthBits depthBits6 = DepthBits.None;
				textureDimension = TextureXR.dimension;
				this.m_CameraSssDiffuseLightingMSAABuffer = RTHandles.Alloc(one6, slices6, depthBits6, this.GetColorBufferFormat(), FilterMode.Point, TextureWrapMode.Repeat, textureDimension, false, false, true, false, 1, 0f, true, true, true, RenderTextureMemoryless.None, "CameraSSSDiffuseLightingMSAA");
			}
		}

		// Token: 0x060003D2 RID: 978 RVA: 0x0002042C File Offset: 0x0001E62C
		private void GetOrCreateDebugTextures()
		{
			if (Debug.isDebugBuild && this.m_DebugColorPickerBuffer == null && this.m_DebugFullScreenTempBuffer == null)
			{
				this.m_DebugColorPickerBuffer = RTHandles.Alloc(Vector2.one, 1, DepthBits.None, GraphicsFormat.R16G16B16A16_SFloat, FilterMode.Point, TextureWrapMode.Repeat, TextureDimension.Tex2D, false, false, true, false, 1, 0f, false, false, true, RenderTextureMemoryless.None, "DebugColorPicker");
				this.m_DebugFullScreenTempBuffer = RTHandles.Alloc(Vector2.one, TextureXR.slices, DepthBits.None, GraphicsFormat.R16G16B16A16_SFloat, FilterMode.Point, TextureWrapMode.Repeat, TextureXR.dimension, false, false, true, false, 1, 0f, false, false, true, RenderTextureMemoryless.None, "DebugFullScreen");
			}
			if (this.m_IntermediateAfterPostProcessBuffer == null)
			{
				Vector2 one = Vector2.one;
				int slices = TextureXR.slices;
				DepthBits depthBits = DepthBits.None;
				TextureDimension dimension = TextureXR.dimension;
				this.m_IntermediateAfterPostProcessBuffer = RTHandles.Alloc(one, slices, depthBits, this.GetColorBufferFormat(), FilterMode.Point, TextureWrapMode.Repeat, dimension, false, false, true, false, 1, 0f, false, false, true, RenderTextureMemoryless.None, "AfterPostProcess");
			}
		}

		// Token: 0x060003D3 RID: 979 RVA: 0x000204EC File Offset: 0x0001E6EC
		private void DestroyRenderTextures()
		{
			this.m_GbufferManager.DestroyBuffers();
			this.m_DbufferManager.DestroyBuffers();
			this.m_MipGenerator.Release();
			RTHandles.Release(this.m_CameraColorBuffer);
			if (this.m_CustomPassColorBuffer.IsValueCreated)
			{
				RTHandles.Release(this.m_CustomPassColorBuffer.Value);
			}
			if (this.m_CustomPassDepthBuffer.IsValueCreated)
			{
				RTHandles.Release(this.m_CustomPassDepthBuffer.Value);
			}
			RTHandles.Release(this.m_OpaqueAtmosphericScatteringBuffer);
			RTHandles.Release(this.m_CameraSssDiffuseLightingBuffer);
			RTHandles.Release(this.m_DistortionBuffer);
			RTHandles.Release(this.m_ContactShadowBuffer);
			RTHandles.Release(this.m_LowResTransparentBuffer);
			RTHandles.Release(this.m_SsrHitPointTexture);
			RTHandles.Release(this.m_SsrLightingTexture);
			RTHandles.Release(this.m_DebugColorPickerBuffer);
			RTHandles.Release(this.m_DebugFullScreenTempBuffer);
			RTHandles.Release(this.m_IntermediateAfterPostProcessBuffer);
			RTHandles.Release(this.m_CameraColorMSAABuffer);
			RTHandles.Release(this.m_OpaqueAtmosphericScatteringMSAABuffer);
			RTHandles.Release(this.m_CameraSssDiffuseLightingMSAABuffer);
		}

		// Token: 0x060003D4 RID: 980 RVA: 0x000205F0 File Offset: 0x0001E7F0
		private bool SetRenderingFeatures()
		{
			Shader.globalRenderPipeline = "HDRenderPipeline";
			GraphicsSettings.lightsUseLinearIntensity = true;
			GraphicsSettings.lightsUseColorTemperature = true;
			GraphicsSettings.useScriptableRenderPipelineBatching = this.m_Asset.enableSRPBatcher;
			SupportedRenderingFeatures.active = new SupportedRenderingFeatures
			{
				reflectionProbeModes = SupportedRenderingFeatures.ReflectionProbeModes.Rotation,
				defaultMixedLightingModes = SupportedRenderingFeatures.LightmapMixedBakeModes.IndirectOnly,
				mixedLightingModes = (SupportedRenderingFeatures.LightmapMixedBakeModes.IndirectOnly | SupportedRenderingFeatures.LightmapMixedBakeModes.Shadowmask),
				lightmapBakeTypes = (LightmapBakeType.Realtime | LightmapBakeType.Baked | LightmapBakeType.Mixed),
				lightmapsModes = LightmapsMode.CombinedDirectional,
				lightProbeProxyVolumes = true,
				motionVectors = true,
				receiveShadows = false,
				reflectionProbes = true,
				rendererPriority = true,
				overridesFog = true,
				overridesOtherLightingSettings = true,
				editableMaterialRenderQueue = false,
				enlighten = false,
				overridesLODBias = true,
				overridesMaximumLODLevel = true,
				terrainDetailUnsupported = true,
				rendererProbes = false
			};
			Lightmapping.SetDelegate(GlobalIlluminationUtils.hdLightsDelegate);
			GraphicsDeviceType graphicsDeviceType;
			if (!this.IsSupportedPlatform(out graphicsDeviceType))
			{
				HDUtils.DisplayUnsupportedAPIMessage(graphicsDeviceType.ToString());
				if (SystemInfo.graphicsDeviceType.ToString().StartsWith("OpenGL"))
				{
					if (SystemInfo.operatingSystem.StartsWith("Mac"))
					{
						HDUtils.DisplayUnsupportedMessage("Use Metal API instead.");
					}
					else if (SystemInfo.operatingSystem.StartsWith("Windows"))
					{
						HDUtils.DisplayUnsupportedMessage("Use Vulkan API instead.");
					}
				}
				return false;
			}
			return true;
		}

		// Token: 0x060003D5 RID: 981 RVA: 0x0002072C File Offset: 0x0001E92C
		private bool IsSupportedPlatform(out GraphicsDeviceType unsupportedGraphicDevice)
		{
			unsupportedGraphicDevice = SystemInfo.graphicsDeviceType;
			if (!SystemInfo.supportsComputeShaders)
			{
				return false;
			}
			RenderPipelineResources defaultResources = this.defaultResources;
			bool? flag;
			if (defaultResources == null)
			{
				flag = null;
			}
			else
			{
				Shader defaultPS = defaultResources.shaders.defaultPS;
				flag = ((defaultPS != null) ? new bool?(defaultPS.isSupported) : null);
			}
			return (flag ?? true) && HDUtils.IsSupportedGraphicDevice(SystemInfo.graphicsDeviceType) && HDUtils.IsOperatingSystemSupported(SystemInfo.operatingSystem);
		}

		// Token: 0x060003D6 RID: 982 RVA: 0x000207B6 File Offset: 0x0001E9B6
		private void UnsetRenderingFeatures()
		{
			Shader.globalRenderPipeline = "";
			SupportedRenderingFeatures.active = new SupportedRenderingFeatures();
			GraphicsSettings.useScriptableRenderPipelineBatching = false;
			Lightmapping.ResetDelegate();
		}

		// Token: 0x060003D7 RID: 983 RVA: 0x000207D8 File Offset: 0x0001E9D8
		private void InitializeDebugMaterials()
		{
			this.m_DebugViewMaterialGBuffer = CoreUtils.CreateEngineMaterial(this.defaultResources.shaders.debugViewMaterialGBufferPS);
			this.m_DebugViewMaterialGBufferShadowMask = CoreUtils.CreateEngineMaterial(this.defaultResources.shaders.debugViewMaterialGBufferPS);
			this.m_DebugViewMaterialGBufferShadowMask.EnableKeyword("SHADOWS_SHADOWMASK");
			this.m_DebugDisplayLatlong = CoreUtils.CreateEngineMaterial(this.defaultResources.shaders.debugDisplayLatlongPS);
			this.m_DebugFullScreen = CoreUtils.CreateEngineMaterial(this.defaultResources.shaders.debugFullScreenPS);
			this.m_DebugColorPicker = CoreUtils.CreateEngineMaterial(this.defaultResources.shaders.debugColorPickerPS);
			this.m_Blit = CoreUtils.CreateEngineMaterial(this.defaultResources.shaders.blitPS);
			this.m_ErrorMaterial = CoreUtils.CreateEngineMaterial("Hidden/InternalErrorShader");
			if (TextureXR.useTexArray)
			{
				this.m_Blit.EnableKeyword("DISABLE_TEXTURE2D_X_ARRAY");
				this.m_BlitTexArray = CoreUtils.CreateEngineMaterial(this.defaultResources.shaders.blitPS);
				this.m_BlitTexArraySingleSlice = CoreUtils.CreateEngineMaterial(this.defaultResources.shaders.blitPS);
				this.m_BlitTexArraySingleSlice.EnableKeyword("BLIT_SINGLE_SLICE");
			}
		}

		// Token: 0x060003D8 RID: 984 RVA: 0x00020904 File Offset: 0x0001EB04
		private void InitializeRenderStateBlocks()
		{
			this.m_DepthStateOpaque = new RenderStateBlock
			{
				depthState = new DepthState(true, CompareFunction.LessEqual),
				mask = RenderStateMask.Depth
			};
		}

		// Token: 0x060003D9 RID: 985 RVA: 0x00020938 File Offset: 0x0001EB38
		protected override void Dispose(bool disposing)
		{
			this.<Dispose>g__DisposeProbeCameraPool|636_1();
			this.UnsetRenderingFeatures();
			if (!this.m_ValidAPI)
			{
				return;
			}
			base.Dispose(disposing);
			this.ReleaseScreenSpaceShadows();
			if (this.m_RayTracingSupported)
			{
				this.ReleaseRecursiveRenderer();
				this.ReleaseRayTracingDeferred();
				this.ReleaseRayTracedIndirectDiffuse();
				this.ReleaseRayTracedReflections();
				this.ReleasePathTracing();
				this.ReleaseRayTracingManager();
			}
			this.m_DebugDisplaySettings.UnregisterDebug();
			this.CleanupLightLoop();
			MousePositionDebug.instance.Cleanup();
			DecalSystem.instance.Cleanup();
			this.m_MaterialList.ForEach(delegate(RenderPipelineMaterial material)
			{
				material.Cleanup();
			});
			CoreUtils.Destroy(this.m_CameraMotionVectorsMaterial);
			CoreUtils.Destroy(this.m_DecalNormalBufferMaterial);
			CoreUtils.Destroy(this.m_DebugViewMaterialGBuffer);
			CoreUtils.Destroy(this.m_DebugViewMaterialGBufferShadowMask);
			CoreUtils.Destroy(this.m_DebugDisplayLatlong);
			CoreUtils.Destroy(this.m_DebugFullScreen);
			CoreUtils.Destroy(this.m_DebugColorPicker);
			CoreUtils.Destroy(this.m_Blit);
			CoreUtils.Destroy(this.m_BlitTexArray);
			CoreUtils.Destroy(this.m_BlitTexArraySingleSlice);
			CoreUtils.Destroy(this.m_CopyDepth);
			CoreUtils.Destroy(this.m_ErrorMaterial);
			CoreUtils.Destroy(this.m_DownsampleDepthMaterial);
			CoreUtils.Destroy(this.m_UpsampleTransparency);
			CoreUtils.Destroy(this.m_ApplyDistortionMaterial);
			CoreUtils.Destroy(this.m_ClearStencilBufferMaterial);
			this.CleanupSubsurfaceScattering();
			this.m_SharedRTManager.Cleanup();
			this.m_XRSystem.Cleanup();
			this.m_SkyManager.Cleanup();
			this.CleanupVolumetricLighting();
			for (int i = 0; i < this.m_IBLFilterArray.Length; i++)
			{
				this.m_IBLFilterArray[i].Cleanup();
			}
			this.m_PostProcessSystem.Cleanup();
			this.m_AmbientOcclusionSystem.Cleanup();
			this.m_BlueNoise.Cleanup();
			HDCamera.ClearAll();
			this.DestroyRenderTextures();
			CullingGroupManager.instance.Cleanup();
			CoreUtils.SafeRelease(this.m_DepthPyramidMipLevelOffsetsBuffer);
			CustomPassVolume.Cleanup();
			this.m_RenderGraph.Cleanup();
			this.m_RenderGraph.UnRegisterDebug();
			this.CleanupPrepass();
			CoreUtils.Destroy(this.m_ColorResolveMaterial);
			CameraCaptureBridge.enabled = false;
		}

		// Token: 0x060003DA RID: 986 RVA: 0x00020B54 File Offset: 0x0001ED54
		private void Resize(HDCamera hdCamera)
		{
			if (hdCamera.actualWidth > this.m_MaxCameraWidth || hdCamera.actualHeight > this.m_MaxCameraHeight || this.LightLoopNeedResize(hdCamera, this.m_TileAndClusterData))
			{
				this.m_MaxCameraWidth = Mathf.Max(this.m_MaxCameraWidth, hdCamera.actualWidth);
				this.m_MaxCameraHeight = Mathf.Max(this.m_MaxCameraHeight, hdCamera.actualHeight);
				if (this.m_MaxCameraWidth > 0 && this.m_MaxCameraHeight > 0)
				{
					this.LightLoopReleaseResolutionDependentBuffers();
					this.m_DbufferManager.ReleaseResolutionDependentBuffers();
					this.m_SharedRTManager.DisposeCoarseStencilBuffer();
				}
				this.LightLoopAllocResolutionDependentBuffers(hdCamera, this.m_MaxCameraWidth, this.m_MaxCameraHeight);
				this.m_DbufferManager.AllocResolutionDependentBuffers(hdCamera, this.m_MaxCameraWidth, this.m_MaxCameraHeight);
				this.m_SharedRTManager.AllocateCoarseStencilBuffer(this.m_MaxCameraWidth, this.m_MaxCameraHeight, hdCamera.viewCount);
			}
		}

		// Token: 0x060003DB RID: 987 RVA: 0x00020C38 File Offset: 0x0001EE38
		private void PushGlobalParams(HDCamera hdCamera, CommandBuffer cmd)
		{
			using (new ProfilingScope(cmd, ProfilingSampler.Get<HDProfileId>(HDProfileId.PushGlobalParameters)))
			{
				this.PushSubsurfaceScatteringGlobalParams(hdCamera, cmd);
				HDRenderPipeline.PushDecalsGlobalParams(hdCamera, cmd);
				Fog.PushFogShaderParameters(hdCamera, cmd);
				this.PushVolumetricLightingGlobalParams(hdCamera, cmd, this.m_FrameCount);
				this.SetMicroShadowingSettings(hdCamera, cmd);
				HDShadowSettings component = hdCamera.volumeStack.GetComponent<HDShadowSettings>();
				cmd.SetGlobalFloat(HDShaderIDs._DirectionalTransmissionMultiplier, component.directionalTransmissionMultiplier.value);
				this.m_AmbientOcclusionSystem.PushGlobalParameters(hdCamera, cmd);
				(hdCamera.volumeStack.GetComponent<ScreenSpaceRefraction>() ?? ScreenSpaceRefraction.defaultInstance).PushShaderParameters(cmd);
				hdCamera.SetupGlobalParams(cmd, this.m_FrameCount);
				cmd.SetGlobalVector(HDShaderIDs._IndirectLightingMultiplier, new Vector4(hdCamera.volumeStack.GetComponent<IndirectLightingController>().indirectDiffuseIntensity.value, 0f, 0f, 0f));
				cmd.SetGlobalInt(HDShaderIDs._ColorMaskTransparentVel, 15);
				if (hdCamera.frameSettings.IsEnabled(FrameSettingsField.MotionVectors))
				{
					RTHandle motionVectorsBuffer = this.m_SharedRTManager.GetMotionVectorsBuffer(false);
					cmd.SetGlobalTexture(HDShaderIDs._CameraMotionVectorsTexture, motionVectorsBuffer);
					cmd.SetGlobalVector(HDShaderIDs._CameraMotionVectorsSize, new Vector4((float)motionVectorsBuffer.referenceSize.x, (float)motionVectorsBuffer.referenceSize.y, 1f / (float)motionVectorsBuffer.referenceSize.x, 1f / (float)motionVectorsBuffer.referenceSize.y));
					cmd.SetGlobalVector(HDShaderIDs._CameraMotionVectorsScale, new Vector4((float)motionVectorsBuffer.referenceSize.x / (float)motionVectorsBuffer.rt.width, (float)motionVectorsBuffer.referenceSize.y / (float)motionVectorsBuffer.rt.height));
				}
				else
				{
					cmd.SetGlobalTexture(HDShaderIDs._CameraMotionVectorsTexture, TextureXR.GetBlackTexture());
				}
				if (hdCamera.frameSettings.IsEnabled(FrameSettingsField.SSR))
				{
					cmd.SetGlobalTexture(HDShaderIDs._SsrLightingTexture, this.m_SsrLightingTexture);
				}
				else
				{
					cmd.SetGlobalTexture(HDShaderIDs._SsrLightingTexture, TextureXR.GetClearTexture());
				}
				cmd.SetGlobalInt(HDShaderIDs._OffScreenRendering, 0);
				cmd.SetGlobalFloat(HDShaderIDs._ReplaceDiffuseForIndirect, hdCamera.frameSettings.IsEnabled(FrameSettingsField.ReplaceDiffuseForIndirect) ? 1f : 0f);
				cmd.SetGlobalInt(HDShaderIDs._EnableSkyReflection, hdCamera.frameSettings.IsEnabled(FrameSettingsField.SkyReflection) ? 1 : 0);
				this.m_SkyManager.SetGlobalSkyData(cmd, hdCamera);
				if (hdCamera.frameSettings.IsEnabled(FrameSettingsField.RayTracing))
				{
					bool flag = this.ValidIndirectDiffuseState(hdCamera);
					cmd.SetGlobalInt(HDShaderIDs._RaytracedIndirectDiffuse, flag ? 1 : 0);
					cmd.SetGlobalInt(HDShaderIDs._RaytracingFrameIndex, this.RayTracingFrameIndex(hdCamera));
				}
				cmd.SetGlobalFloat(HDShaderIDs._ContactShadowOpacity, this.m_ContactShadows.opacity.value);
			}
		}

		// Token: 0x060003DC RID: 988 RVA: 0x00020F30 File Offset: 0x0001F130
		private void CopyDepthBufferIfNeeded(HDCamera hdCamera, CommandBuffer cmd)
		{
			if (!this.m_IsDepthBufferCopyValid)
			{
				using (new ProfilingScope(cmd, ProfilingSampler.Get<HDProfileId>(HDProfileId.CopyDepthBuffer)))
				{
					this.m_GPUCopy.SampleCopyChannel_xyzw2x(cmd, this.m_SharedRTManager.GetDepthStencilBuffer(false), this.m_SharedRTManager.GetDepthTexture(false), new RectInt(0, 0, hdCamera.actualWidth, hdCamera.actualHeight));
					cmd.SetGlobalTexture(HDShaderIDs._CameraDepthTexture, this.m_SharedRTManager.GetDepthTexture(false));
				}
				this.m_IsDepthBufferCopyValid = true;
			}
		}

		// Token: 0x060003DD RID: 989 RVA: 0x00020FD0 File Offset: 0x0001F1D0
		private void BuildCoarseStencilAndResolveIfNeeded(HDCamera hdCamera, RTHandle depthStencilBuffer, RTHandle resolvedStencilBuffer, ComputeBuffer coarseStencilBuffer, CommandBuffer cmd)
		{
			using (new ProfilingScope(cmd, ProfilingSampler.Get<HDProfileId>(HDProfileId.CoarseStencilGeneration)))
			{
				bool flag = hdCamera.frameSettings.IsEnabled(FrameSettingsField.MSAA);
				bool flag2 = (HDRenderPipeline.GetFeatureVariantsEnabled(hdCamera.frameSettings) || hdCamera.frameSettings.IsEnabled(FrameSettingsField.SSR)) && flag;
				ComputeShader resolveStencilCS = this.defaultResources.shaders.resolveStencilCS;
				int num = HDRenderPipeline.SampleCountToPassIndex(flag ? hdCamera.msaaSamples : MSAASamples.None);
				num = (flag2 ? (num + 3) : num);
				int num2 = HDUtils.DivRoundUp(hdCamera.actualWidth, 8);
				int num3 = HDUtils.DivRoundUp(hdCamera.actualHeight, 8);
				cmd.SetGlobalVector(HDShaderIDs._CoarseStencilBufferSize, new Vector4((float)num2, (float)num3, 1f / (float)num2, 1f / (float)num3));
				cmd.SetComputeBufferParam(resolveStencilCS, num, HDShaderIDs._CoarseStencilBuffer, coarseStencilBuffer);
				cmd.SetComputeTextureParam(resolveStencilCS, num, HDShaderIDs._StencilTexture, depthStencilBuffer, 0, RenderTextureSubElement.Stencil);
				if (flag2)
				{
					cmd.SetComputeTextureParam(resolveStencilCS, num, HDShaderIDs._OutputStencilBuffer, resolvedStencilBuffer);
				}
				cmd.DispatchCompute(resolveStencilCS, num, num2, num3, hdCamera.viewCount);
			}
		}

		// Token: 0x060003DE RID: 990 RVA: 0x00021104 File Offset: 0x0001F304
		private void SetMicroShadowingSettings(HDCamera hdCamera, CommandBuffer cmd)
		{
			MicroShadowing component = hdCamera.volumeStack.GetComponent<MicroShadowing>();
			cmd.SetGlobalFloat(HDShaderIDs._MicroShadowOpacity, component.enable.value ? component.opacity.value : 0f);
		}

		// Token: 0x060003DF RID: 991 RVA: 0x00021148 File Offset: 0x0001F348
		private void ConfigureKeywords(bool enableBakeShadowMask, HDCamera hdCamera, CommandBuffer cmd)
		{
			CoreUtils.SetKeyword(cmd, "SHADOWS_SHADOWMASK", enableBakeShadowMask);
			this.m_CurrentRendererConfigurationBakedLighting = (enableBakeShadowMask ? (PerObjectData.LightProbe | PerObjectData.LightProbeProxyVolume | PerObjectData.Lightmaps | PerObjectData.OcclusionProbe | PerObjectData.OcclusionProbeProxyVolume | PerObjectData.ShadowMask) : (PerObjectData.LightProbe | PerObjectData.LightProbeProxyVolume | PerObjectData.Lightmaps));
			this.m_currentDebugViewMaterialGBuffer = (enableBakeShadowMask ? this.m_DebugViewMaterialGBufferShadowMask : this.m_DebugViewMaterialGBuffer);
			CoreUtils.SetKeyword(cmd, "LIGHT_LAYERS", hdCamera.frameSettings.IsEnabled(FrameSettingsField.LightLayers));
			cmd.SetGlobalInt(HDShaderIDs._EnableLightLayers, hdCamera.frameSettings.IsEnabled(FrameSettingsField.LightLayers) ? 1 : 0);
			if (this.m_Asset.currentPlatformRenderPipelineSettings.supportDecals)
			{
				CoreUtils.SetKeyword(cmd, "DECALS_OFF", false);
				CoreUtils.SetKeyword(cmd, "DECALS_3RT", !this.m_Asset.currentPlatformRenderPipelineSettings.decalSettings.perChannelMask);
				CoreUtils.SetKeyword(cmd, "DECALS_4RT", this.m_Asset.currentPlatformRenderPipelineSettings.decalSettings.perChannelMask);
			}
			else
			{
				CoreUtils.SetKeyword(cmd, "DECALS_OFF", true);
				CoreUtils.SetKeyword(cmd, "DECALS_3RT", false);
				CoreUtils.SetKeyword(cmd, "DECALS_4RT", false);
			}
			CoreUtils.SetKeyword(cmd, "WRITE_NORMAL_BUFFER", hdCamera.frameSettings.litShaderMode == LitShaderMode.Forward);
			CoreUtils.SetKeyword(cmd, "WRITE_MSAA_DEPTH", hdCamera.frameSettings.IsEnabled(FrameSettingsField.MSAA));
		}

		// Token: 0x060003E0 RID: 992 RVA: 0x00021284 File Offset: 0x0001F484
		protected override void Render(ScriptableRenderContext renderContext, Camera[] cameras)
		{
			HDRenderPipeline.<>c__DisplayClass645_0 CS$<>8__locals1;
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.renderContext = renderContext;
			if (!this.m_ValidAPI || cameras.Length == 0)
			{
				return;
			}
			HDRenderPipeline.GetOrCreateDefaultVolume();
			this.GetOrCreateDebugTextures();
			this.LightLoopNewRender();
			RenderPipeline.BeginFrameRendering(CS$<>8__locals1.renderContext, cameras);
			this.m_FrameSettingsHistoryEnabled = FrameSettingsHistory.enabled;
			int frameCount = Time.frameCount;
			bool flag = frameCount != this.m_FrameCount;
			this.m_FrameCount = frameCount;
			if (flag)
			{
				this.m_LastTime = this.m_Time;
				this.m_Time = Time.time;
				this.m_LastTime = Mathf.Min(this.m_Time, this.m_LastTime);
				this.m_ProbeCameraCache.ClearCamerasUnusedFor(2, this.m_FrameCount);
				HDCamera.CleanUnused();
			}
			DynamicResolutionHandler instance = DynamicResolutionHandler.instance;
			instance.Update(this.m_Asset.currentPlatformRenderPipelineSettings.dynamicResolutionSettings, delegate
			{
				HDRenderPipeline hdrenderPipeline = RenderPipelineManager.currentPipeline as HDRenderPipeline;
				RenderTexture rt = hdrenderPipeline.m_SharedRTManager.GetDepthStencilBuffer(false).rt;
				Vector2Int vector2Int = new Vector2Int(rt.width, rt.height);
				hdrenderPipeline.m_SharedRTManager.ComputeDepthBufferMipChainSize(DynamicResolutionHandler.instance.GetScaledSize(vector2Int));
			});
			HDRenderPipeline.<>c__DisplayClass645_1 CS$<>8__locals2;
			using (ListPool<HDRenderPipeline.RenderRequest>.Get(out CS$<>8__locals2.renderRequests))
			{
				List<int> list;
				using (ListPool<int>.Get(out list))
				{
					HashSet<int> hashSet;
					using (HashSetPool<int>.Get(out hashSet))
					{
						HDRenderPipeline.<>c__DisplayClass645_2 CS$<>8__locals3;
						using (DictionaryPool<HDProbe, List<ValueTuple<int, float>>>.Get(out CS$<>8__locals3.renderRequestIndicesWhereTheProbeIsVisible))
						{
							HDRenderPipeline.<>c__DisplayClass645_3 CS$<>8__locals4;
							using (ListPool<CameraSettings>.Get(out CS$<>8__locals4.cameraSettings))
							{
								HDRenderPipeline.<>c__DisplayClass645_4 CS$<>8__locals5;
								using (ListPool<CameraPositionSettings>.Get(out CS$<>8__locals5.cameraPositionSettings))
								{
									foreach (ValueTuple<Camera, XRPass> valueTuple in this.m_XRSystem.SetupFrame(cameras, this.m_Asset.currentPlatformRenderPipelineSettings.xrSettings.singlePass, this.m_DebugDisplaySettings.data.xrSinglePassTestMode))
									{
										Camera item = valueTuple.Item1;
										XRPass item2 = valueTuple.Item2;
										if (!(item == null))
										{
											bool flag2 = false;
											HDAdditionalCameraData hdadditionalCameraData;
											if (item.TryGetComponent<HDAdditionalCameraData>(out hdadditionalCameraData))
											{
												flag2 = hdadditionalCameraData.allowDynamicResolution;
												if (SystemInfo.graphicsDeviceType == GraphicsDeviceType.Metal || (instance.RequestsHardwareDynamicResolution() && flag2 && !item.allowDynamicResolution))
												{
													instance.ForceSoftwareFallback();
												}
											}
											instance.SetCurrentCameraRequest(flag2);
											RTHandles.SetHardwareDynamicResolutionState(instance.HardwareDynamicResIsEnabled());
											VFXManager.PrepareCamera(item);
											CS$<>8__locals4.cameraSettings.Clear();
											CS$<>8__locals5.cameraPositionSettings.Clear();
											hashSet.Clear();
											HDRenderPipeline.HDCullingResults hdcullingResults = UnsafeGenericPool<HDRenderPipeline.HDCullingResults>.Get();
											hdcullingResults.Reset();
											HDAdditionalCameraData hdadditionalCameraData2;
											HDCamera hdcamera;
											ScriptableCullingParameters scriptableCullingParameters;
											bool flag3 = !this.TryCalculateFrameParameters(item, item2, out hdadditionalCameraData2, out hdcamera, out scriptableCullingParameters);
											if (!flag3)
											{
												bool flag4 = true;
												if (item2.multipassId > 0)
												{
													foreach (HDRenderPipeline.RenderRequest renderRequest in CS$<>8__locals2.renderRequests)
													{
														if (renderRequest.hdCamera.xr.cullingPassId == item2.cullingPassId)
														{
															UnsafeGenericPool<HDRenderPipeline.HDCullingResults>.Release(hdcullingResults);
															hdcullingResults = renderRequest.cullingResults;
															hashSet.Add(renderRequest.index);
															flag4 = false;
														}
													}
												}
												if (flag4)
												{
													flag3 = !HDRenderPipeline.TryCull(item, hdcamera, CS$<>8__locals1.renderContext, this.m_SkyManager, scriptableCullingParameters, this.m_Asset, ref hdcullingResults);
												}
											}
											if (hdadditionalCameraData2 != null && hdadditionalCameraData2.hasCustomRender)
											{
												flag3 = true;
												hdadditionalCameraData2.ExecuteCustomRender(CS$<>8__locals1.renderContext, hdcamera);
											}
											if (flag3)
											{
												CS$<>8__locals1.renderContext.Submit();
												UnsafeGenericPool<HDRenderPipeline.HDCullingResults>.Release(hdcullingResults);
												RenderPipeline.EndCameraRendering(CS$<>8__locals1.renderContext, item);
											}
											else
											{
												RenderTexture targetTexture = item.targetTexture;
												RenderTargetIdentifier renderTargetIdentifier = ((targetTexture != null) ? targetTexture : new RenderTargetIdentifier(BuiltinRenderTextureType.CameraTarget));
												if (item.targetTexture != null)
												{
													item.targetTexture.IncrementUpdateCount();
												}
												if (hdcamera.xr.enabled && hdcamera.xr.renderTargetValid)
												{
													renderTargetIdentifier = hdcamera.xr.renderTarget;
												}
												HDRenderPipeline.RenderRequest renderRequest2 = new HDRenderPipeline.RenderRequest
												{
													hdCamera = hdcamera,
													cullingResults = hdcullingResults,
													target = new HDRenderPipeline.RenderRequest.Target
													{
														id = renderTargetIdentifier,
														face = CubemapFace.Unknown
													},
													dependsOnRenderRequestIndices = ListPool<int>.Get(),
													index = CS$<>8__locals2.renderRequests.Count,
													cameraSettings = CameraSettings.From(hdcamera)
												};
												CS$<>8__locals2.renderRequests.Add(renderRequest2);
												list.Add(renderRequest2.index);
												for (int i = 0; i < hdcullingResults.cullingResults.visibleReflectionProbes.Length; i++)
												{
													VisibleReflectionProbe visibleReflectionProbe = hdcullingResults.cullingResults.visibleReflectionProbes[i];
													if (!visibleReflectionProbe.Equals(null) && !(visibleReflectionProbe.reflectionProbe == null) && !visibleReflectionProbe.reflectionProbe.Equals(null))
													{
														HDAdditionalReflectionData hdadditionalReflectionData;
														if (!visibleReflectionProbe.reflectionProbe.TryGetComponent<HDAdditionalReflectionData>(out hdadditionalReflectionData))
														{
															hdadditionalReflectionData = visibleReflectionProbe.reflectionProbe.gameObject.AddComponent<HDAdditionalReflectionData>();
														}
														this.<Render>g__AddVisibleProbeVisibleIndexIfUpdateIsRequired|645_2(hdadditionalReflectionData, renderRequest2.index, ref CS$<>8__locals1, ref CS$<>8__locals2, ref CS$<>8__locals3);
													}
												}
												for (int j = 0; j < hdcullingResults.hdProbeCullingResults.visibleProbes.Count; j++)
												{
													this.<Render>g__AddVisibleProbeVisibleIndexIfUpdateIsRequired|645_2(hdcullingResults.hdProbeCullingResults.visibleProbes[j], renderRequest2.index, ref CS$<>8__locals1, ref CS$<>8__locals2, ref CS$<>8__locals3);
												}
											}
										}
									}
									foreach (KeyValuePair<HDProbe, List<ValueTuple<int, float>>> keyValuePair in CS$<>8__locals3.renderRequestIndicesWhereTheProbeIsVisible)
									{
										HDProbe key = keyValuePair.Key;
										List<ValueTuple<int, float>> value = keyValuePair.Value;
										if (key.type == ProbeSettings.ProbeType.PlanarProbe)
										{
											for (int k = 0; k < value.Count; k++)
											{
												ValueTuple<int, float> valueTuple2 = value[k];
												if (valueTuple2.Item2 > 0f)
												{
													int item3 = valueTuple2.Item1;
													HDRenderPipeline.RenderRequest renderRequest3 = CS$<>8__locals2.renderRequests[item3];
													Transform transform = renderRequest3.hdCamera.camera.transform;
													Camera camera = renderRequest3.hdCamera.camera;
													this.<Render>g__AddHDProbeRenderRequests|645_1(key, transform, new List<ValueTuple<int, float>> { valueTuple2 }, HDUtils.GetSceneCullingMaskFromCamera(renderRequest3.hdCamera.camera), camera, renderRequest3.hdCamera.camera.fieldOfView, renderRequest3.hdCamera.camera.aspect, ref CS$<>8__locals1, ref CS$<>8__locals2, ref CS$<>8__locals4, ref CS$<>8__locals5);
												}
											}
										}
										else
										{
											Camera camera = null;
											bool flag5 = false;
											int num = 0;
											while (num < value.Count && !flag5)
											{
												if (value[num].Item2 > 0f)
												{
													flag5 = true;
												}
												num++;
											}
											if (flag5)
											{
												this.<Render>g__AddHDProbeRenderRequests|645_1(key, null, value, 0UL, camera, 90f, 1f, ref CS$<>8__locals1, ref CS$<>8__locals2, ref CS$<>8__locals4, ref CS$<>8__locals5);
											}
										}
									}
									foreach (KeyValuePair<HDProbe, List<ValueTuple<int, float>>> keyValuePair2 in CS$<>8__locals3.renderRequestIndicesWhereTheProbeIsVisible)
									{
										ListPool<ValueTuple<int, float>>.Release(keyValuePair2.Value);
									}
									CS$<>8__locals3.renderRequestIndicesWhereTheProbeIsVisible.Clear();
									Vector2Int zero = Vector2Int.zero;
									for (int l = 0; l < CS$<>8__locals2.renderRequests.Count; l++)
									{
										HDRenderPipeline.RenderRequest renderRequest4 = CS$<>8__locals2.renderRequests[l];
										if (renderRequest4.target.face != CubemapFace.Unknown)
										{
											int actualWidth = renderRequest4.hdCamera.actualWidth;
											int actualHeight = renderRequest4.hdCamera.actualHeight;
											zero.x = Mathf.Max(actualWidth, zero.x);
											zero.y = Mathf.Max(actualHeight, zero.y);
										}
									}
									if (zero != Vector2.zero)
									{
										if (this.m_TemporaryTargetForCubemaps != null && (this.m_TemporaryTargetForCubemaps.width != zero.x || this.m_TemporaryTargetForCubemaps.height != zero.y))
										{
											this.m_TemporaryTargetForCubemaps.Release();
											this.m_TemporaryTargetForCubemaps = null;
										}
										if (this.m_TemporaryTargetForCubemaps == null)
										{
											this.m_TemporaryTargetForCubemaps = new RenderTexture(zero.x, zero.y, 1, GraphicsFormat.R16G16B16A16_SFloat)
											{
												autoGenerateMips = false,
												useMipMap = false,
												name = "Temporary Target For Cubemap Face",
												volumeDepth = 1,
												useDynamicScale = false
											};
										}
									}
									List<int> list2;
									using (ListPool<int>.Get(out list2))
									{
										Stack<int> stack;
										using (GenericPool<Stack<int>>.Get(out stack))
										{
											stack.Clear();
											for (int m = list.Count - 1; m >= 0; m--)
											{
												stack.Push(list[m]);
												while (stack.Count > 0)
												{
													int num2 = stack.Pop();
													if (!list2.Contains(num2))
													{
														list2.Add(num2);
													}
													HDRenderPipeline.RenderRequest renderRequest5 = CS$<>8__locals2.renderRequests[num2];
													for (int n = 0; n < renderRequest5.dependsOnRenderRequestIndices.Count; n++)
													{
														stack.Push(renderRequest5.dependsOnRenderRequestIndices[n]);
													}
												}
											}
										}
										using (new ProfilingScope(null, ProfilingSampler.Get<HDProfileId>(HDProfileId.HDRenderPipelineAllRenderRequest)))
										{
											for (int num3 = list2.Count - 1; num3 >= 0; num3--)
											{
												int num4 = list2[num3];
												HDRenderPipeline.RenderRequest renderRequest6 = CS$<>8__locals2.renderRequests[num4];
												CommandBuffer commandBuffer = CommandBufferPool.Get("");
												if (renderRequest6.target.face != CubemapFace.Unknown)
												{
													if (!this.m_TemporaryTargetForCubemaps.IsCreated())
													{
														this.m_TemporaryTargetForCubemaps.Create();
													}
													renderRequest6.target.id = this.m_TemporaryTargetForCubemaps;
												}
												foreach (AOVRequestData aovrequestData in renderRequest6.hdCamera.aovRequests)
												{
													using (new ProfilingScope(commandBuffer, ProfilingSampler.Get<HDProfileId>(HDProfileId.HDRenderPipelineRenderAOV)))
													{
														commandBuffer.SetInvertCulling(renderRequest6.cameraSettings.invertFaceCulling);
														this.ExecuteRenderRequest(renderRequest6, CS$<>8__locals1.renderContext, commandBuffer, aovrequestData);
														commandBuffer.SetInvertCulling(false);
													}
													CS$<>8__locals1.renderContext.ExecuteCommandBuffer(commandBuffer);
													CommandBufferPool.Release(commandBuffer);
													CS$<>8__locals1.renderContext.Submit();
													commandBuffer = CommandBufferPool.Get();
												}
												using (new ProfilingScope(commandBuffer, renderRequest6.hdCamera.profilingSampler))
												{
													commandBuffer.SetInvertCulling(renderRequest6.cameraSettings.invertFaceCulling);
													this.ExecuteRenderRequest(renderRequest6, CS$<>8__locals1.renderContext, commandBuffer, AOVRequestData.defaultAOVRequestDataNonAlloc);
													commandBuffer.SetInvertCulling(false);
													RenderPipeline.EndCameraRendering(CS$<>8__locals1.renderContext, renderRequest6.hdCamera.camera);
												}
												HDRenderPipeline.RenderRequest.Target target = renderRequest6.target;
												if (target.copyToTarget != null)
												{
													commandBuffer.CopyTexture(target.id, 0, 0, 0, 0, renderRequest6.hdCamera.actualWidth, renderRequest6.hdCamera.actualHeight, target.copyToTarget, (int)target.face, 0, 0, 0);
												}
												if (renderRequest6.clearCameraSettings)
												{
													renderRequest6.hdCamera.camera.targetTexture = null;
												}
												ListPool<int>.Release(renderRequest6.dependsOnRenderRequestIndices);
												if (!hashSet.Contains(renderRequest6.index))
												{
													DecalSystem.CullResult decalCullResults = renderRequest6.cullingResults.decalCullResults;
													if (decalCullResults != null)
													{
														decalCullResults.Clear();
													}
													UnsafeGenericPool<HDRenderPipeline.HDCullingResults>.Release(renderRequest6.cullingResults);
												}
												if (num3 == 0 && renderRequest6.hdCamera.camera.cameraType == CameraType.Game)
												{
													this.m_XRSystem.RenderMirrorView(commandBuffer);
												}
												this.PropagateScreenSpaceShadowData();
												CS$<>8__locals1.renderContext.ExecuteCommandBuffer(commandBuffer);
												CommandBufferPool.Release(commandBuffer);
												CS$<>8__locals1.renderContext.Submit();
											}
										}
									}
								}
							}
						}
					}
				}
			}
			this.m_XRSystem.ReleaseFrame();
			RenderPipeline.EndFrameRendering(CS$<>8__locals1.renderContext, cameras);
		}

		// Token: 0x060003E1 RID: 993 RVA: 0x00022014 File Offset: 0x00020214
		private void PropagateScreenSpaceShadowData()
		{
			foreach (HDAdditionalLightData hdadditionalLightData in this.m_ScreenSpaceShadowsUnion)
			{
				hdadditionalLightData.previousTransform = hdadditionalLightData.transform.localToWorldMatrix;
			}
		}

		// Token: 0x060003E2 RID: 994 RVA: 0x00022070 File Offset: 0x00020270
		private void ExecuteRenderRequest(HDRenderPipeline.RenderRequest renderRequest, ScriptableRenderContext renderContext, CommandBuffer cmd, AOVRequestData aovRequest)
		{
			this.InitializeGlobalResources(renderContext);
			HDCamera hdCamera = renderRequest.hdCamera;
			Camera camera = hdCamera.camera;
			CullingResults cullingResults = renderRequest.cullingResults.cullingResults;
			CullingResults cullingResults2 = renderRequest.cullingResults.customPassCullingResults ?? cullingResults;
			HDProbeCullingResults hdProbeCullingResults = renderRequest.cullingResults.hdProbeCullingResults;
			DecalSystem.CullResult decalCullResults = renderRequest.cullingResults.decalCullResults;
			HDRenderPipeline.RenderRequest.Target target = renderRequest.target;
			hdCamera.BeginRender(cmd);
			if (this.m_RayTracingSupported)
			{
				this.BuildRayTracingAccelerationStructure(hdCamera);
			}
			List<RTHandle> list;
			using (ListPool<RTHandle>.Get(out list))
			{
				aovRequest.AllocateTargetTexturesIfRequired(ref list);
				if (camera.cameraType == CameraType.Reflection || camera.cameraType == CameraType.Preview)
				{
					this.m_CurrentDebugDisplaySettings = HDRenderPipeline.s_NeutralDebugDisplaySettings;
				}
				else
				{
					this.m_MSAASamples = this.m_DebugDisplaySettings.data.msaaSamples;
					this.m_SharedRTManager.SetNumMSAASamples(this.m_MSAASamples);
					this.m_DebugDisplaySettings.UpdateCameraFreezeOptions();
					this.m_CurrentDebugDisplaySettings = this.m_DebugDisplaySettings;
				}
				aovRequest.SetupDebugData(ref this.m_CurrentDebugDisplaySettings);
				if (hdCamera.frameSettings.IsEnabled(FrameSettingsField.RayTracing))
				{
					this.m_RayCountManager.ClearRayCount(cmd, hdCamera, this.m_CurrentDebugDisplaySettings.data.countRays);
				}
				if (hdCamera.frameSettings.IsEnabled(FrameSettingsField.Decals))
				{
					using (new ProfilingScope(cmd, ProfilingSampler.Get<HDProfileId>(HDProfileId.DBufferPrepareDrawData)))
					{
						DecalSystem.instance.CurrentCamera = hdCamera.camera;
						DecalSystem.instance.LoadCullResults(decalCullResults);
						DecalSystem.instance.UpdateCachedMaterialData();
						DecalSystem.instance.CreateDrawData();
						DecalSystem.instance.UpdateTextureAtlas(cmd);
					}
				}
				using (new ProfilingScope(null, ProfilingSampler.Get<HDProfileId>(HDProfileId.CustomPassVolumeUpdate)))
				{
					if (hdCamera.frameSettings.IsEnabled(FrameSettingsField.CustomPass))
					{
						CustomPassVolume.Update(hdCamera);
					}
				}
				this.LightLoopNewFrame(hdCamera);
				cmd.DisableScissorRect();
				this.Resize(hdCamera);
				this.m_PostProcessSystem.BeginFrame(cmd, hdCamera, this);
				this.ApplyDebugDisplaySettings(hdCamera, cmd);
				this.m_SkyManager.UpdateCurrentSkySettings(hdCamera);
				this.SetupCameraProperties(hdCamera, renderContext, cmd);
				foreach (RenderPipelineMaterial renderPipelineMaterial in this.m_MaterialList)
				{
					renderPipelineMaterial.Bind(cmd);
				}
				DensityVolumeList densityVolumeList = this.PrepareVisibleDensityVolumeList(hdCamera, cmd, hdCamera.time);
				bool flag = this.PrepareLightsForGPU(cmd, hdCamera, cullingResults, hdProbeCullingResults, densityVolumeList, this.m_CurrentDebugDisplaySettings, aovRequest);
				this.BindLightDataParameters(hdCamera, cmd);
				this.ConfigureKeywords(flag, hdCamera, cmd);
				if (!this.m_CurrentDebugDisplaySettings.IsMatcapViewEnabled(hdCamera))
				{
					this.UpdateSkyEnvironment(hdCamera, renderContext, this.m_FrameCount, cmd);
				}
				else
				{
					cmd.SetGlobalTexture(HDShaderIDs._SkyTexture, CoreUtils.magentaCubeTextureArray);
				}
				this.PushGlobalParams(hdCamera, cmd);
				VFXManager.ProcessCameraCommand(camera, cmd);
				if (GL.wireframe)
				{
					this.RenderWireFrame(cullingResults, hdCamera, target.id, renderContext, cmd);
					return;
				}
				if (this.m_RenderGraph.enabled)
				{
					this.ExecuteWithRenderGraph(renderRequest, aovRequest, list, renderContext, cmd);
					return;
				}
				hdCamera.xr.StartSinglePass(cmd, camera, renderContext);
				this.ClearBuffers(hdCamera, cmd);
				if (hdCamera.xr.enabled && this.m_Asset.currentPlatformRenderPipelineSettings.xrSettings.occlusionMesh)
				{
					hdCamera.xr.StopSinglePass(cmd, camera, renderContext);
					hdCamera.xr.RenderOcclusionMeshes(cmd, this.m_SharedRTManager.GetDepthStencilBuffer(hdCamera.frameSettings.IsEnabled(FrameSettingsField.MSAA)));
					hdCamera.xr.StartSinglePass(cmd, camera, renderContext);
				}
				if (hdCamera.frameSettings.IsEnabled(FrameSettingsField.CustomPass))
				{
					if (this.m_CustomPassColorBuffer.IsValueCreated)
					{
						cmd.SetGlobalTexture(HDShaderIDs._CustomColorTexture, this.m_CustomPassColorBuffer.Value);
					}
					if (this.m_CustomPassDepthBuffer.IsValueCreated)
					{
						cmd.SetGlobalTexture(HDShaderIDs._CustomDepthTexture, this.m_CustomPassDepthBuffer.Value);
					}
				}
				this.RenderCustomPass(renderContext, cmd, hdCamera, cullingResults2, CustomPassInjectionPoint.BeforeRendering);
				bool flag2 = this.RenderDepthPrepass(cullingResults, hdCamera, renderContext, cmd);
				if (!flag2)
				{
					this.RenderObjectsMotionVectors(cullingResults, hdCamera, renderContext, cmd);
				}
				bool flag3 = hdCamera.frameSettings.IsEnabled(FrameSettingsField.MSAA);
				if (flag3)
				{
					this.RenderCameraMotionVectors(cullingResults, hdCamera, renderContext, cmd);
				}
				this.PreRenderSky(hdCamera, cmd);
				this.m_SharedRTManager.ResolveSharedRT(cmd, hdCamera);
				this.RenderDBuffer(hdCamera, cmd, renderContext, cullingResults);
				this.RenderGBuffer(cullingResults, hdCamera, renderContext, cmd);
				this.DecalNormalPatch(hdCamera, cmd, renderContext);
				this.m_SharedRTManager.BindNormalBuffer(cmd, false);
				this.RenderCustomPass(renderContext, cmd, hdCamera, cullingResults2, CustomPassInjectionPoint.AfterOpaqueDepthAndNormal);
				this.GenerateDepthPyramid(hdCamera, cmd, FullScreenDebugMode.DepthPyramid);
				cmd.SetGlobalTexture(HDShaderIDs._CameraDepthTexture, this.m_SharedRTManager.GetDepthTexture(false));
				if (flag2)
				{
					this.RenderObjectsMotionVectors(cullingResults, hdCamera, renderContext, cmd);
				}
				if (!flag3)
				{
					this.RenderCameraMotionVectors(cullingResults, hdCamera, renderContext, cmd);
				}
				this.RenderTransparencyOverdraw(cullingResults, hdCamera, renderContext, cmd);
				if (this.m_CurrentDebugDisplaySettings.IsDebugMaterialDisplayEnabled() || this.m_CurrentDebugDisplaySettings.IsMaterialValidationEnabled() || CoreUtils.IsSceneLightingDisabled(hdCamera.camera))
				{
					this.RenderDebugViewMaterial(cullingResults, hdCamera, renderContext, cmd);
				}
				else if (hdCamera.frameSettings.IsEnabled(FrameSettingsField.RayTracing) && hdCamera.volumeStack.GetComponent<PathTracing>().enable.value)
				{
					this.BuildRayTracingLightCluster(cmd, hdCamera);
					if (FullScreenDebugMode.LightCluster == this.m_CurrentDebugDisplaySettings.data.fullScreenDebugMode && this.GetRayTracingClusterState())
					{
						this.RequestLightCluster().EvaluateClusterDebugView(cmd, hdCamera);
					}
					this.RenderPathTracing(hdCamera, cmd, this.m_CameraColorBuffer, renderContext, this.m_FrameCount);
				}
				else
				{
					if (hdCamera.frameSettings.IsEnabled(FrameSettingsField.ContactShadows) && this.m_CurrentDebugDisplaySettings.data.fullScreenDebugMode == FullScreenDebugMode.ContactShadows)
					{
						CoreUtils.SetRenderTarget(cmd, this.m_ContactShadowBuffer, ClearFlag.Color, Color.clear, 0, CubemapFace.Unknown, -1);
					}
					bool flag4 = hdCamera.frameSettings.IsEnabled(FrameSettingsField.MSAA);
					this.BuildCoarseStencilAndResolveIfNeeded(hdCamera, this.m_SharedRTManager.GetDepthStencilBuffer(flag4), flag4 ? this.m_SharedRTManager.GetStencilBuffer(flag4) : null, this.m_SharedRTManager.GetCoarseStencilBuffer(), cmd);
					hdCamera.xr.StopSinglePass(cmd, camera, renderContext);
					HDGPUAsyncTask hdgpuasyncTask = new HDGPUAsyncTask("Build light list", ComputeQueueType.Background);
					HDGPUAsyncTask hdgpuasyncTask2 = new HDGPUAsyncTask("Volumetric voxelization", ComputeQueueType.Background);
					HDGPUAsyncTask hdgpuasyncTask3 = new HDGPUAsyncTask("Screen Space Reflection", ComputeQueueType.Background);
					HDGPUAsyncTask hdgpuasyncTask4 = new HDGPUAsyncTask("SSAO", ComputeQueueType.Background);
					HDGPUAsyncTaskParams hdgpuasyncTaskParams = new HDGPUAsyncTaskParams
					{
						renderContext = renderContext,
						hdCamera = hdCamera,
						frameCount = this.m_FrameCount
					};
					bool flag5 = false;
					if (hdCamera.frameSettings.BuildLightListRunsAsync())
					{
						hdgpuasyncTask.Start(cmd, in hdgpuasyncTaskParams, new Action<CommandBuffer, HDGPUAsyncTaskParams>(this.<ExecuteRenderRequest>g__Callback|647_0), !flag5);
						flag5 = true;
					}
					if (hdCamera.frameSettings.VolumeVoxelizationRunsAsync())
					{
						hdgpuasyncTask2.Start(cmd, in hdgpuasyncTaskParams, new Action<CommandBuffer, HDGPUAsyncTaskParams>(this.<ExecuteRenderRequest>g__Callback|647_1), !flag5);
						flag5 = true;
					}
					if (hdCamera.frameSettings.SSRRunsAsync())
					{
						hdgpuasyncTask3.Start(cmd, in hdgpuasyncTaskParams, new Action<CommandBuffer, HDGPUAsyncTaskParams>(this.<ExecuteRenderRequest>g__Callback|647_2), !flag5);
						flag5 = true;
					}
					if (hdCamera.frameSettings.SSAORunsAsync())
					{
						hdgpuasyncTask4.Start(cmd, in hdgpuasyncTaskParams, new Action<CommandBuffer, HDGPUAsyncTaskParams>(this.<ExecuteRenderRequest>g__AsyncSSAODispatch|647_3), !flag5);
					}
					using (new ProfilingScope(cmd, ProfilingSampler.Get<HDProfileId>(HDProfileId.RenderShadowMaps)))
					{
						this.RenderShadowMaps(renderContext, cmd, cullingResults, hdCamera);
						hdCamera.SetupGlobalParams(cmd, this.m_FrameCount);
					}
					if (hdCamera.frameSettings.IsEnabled(FrameSettingsField.RayTracing))
					{
						this.BuildRayTracingLightCluster(cmd, hdCamera);
						if (FullScreenDebugMode.LightCluster == this.m_CurrentDebugDisplaySettings.data.fullScreenDebugMode && this.GetRayTracingClusterState())
						{
							this.RequestLightCluster().EvaluateClusterDebugView(cmd, hdCamera);
						}
						if (this.ValidIndirectDiffuseState(hdCamera))
						{
							this.RenderIndirectDiffuse(hdCamera, cmd, renderContext, this.m_FrameCount);
						}
					}
					if (!hdCamera.frameSettings.SSRRunsAsync())
					{
						this.RenderSSR(hdCamera, cmd, renderContext);
					}
					if (hdCamera.frameSettings.BuildLightListRunsAsync())
					{
						hdgpuasyncTask.EndWithPostWork(cmd, hdCamera, new Action<CommandBuffer, HDCamera>(HDRenderPipeline.<>c.<>9.<ExecuteRenderRequest>g__Callback|647_4));
					}
					else
					{
						this.BuildGPULightLists(hdCamera, cmd);
					}
					if (!hdCamera.frameSettings.SSAORunsAsync())
					{
						this.m_AmbientOcclusionSystem.Render(cmd, hdCamera, renderContext, this.m_FrameCount);
					}
					HDUtils.CheckRTCreated(this.m_ContactShadowBuffer);
					this.RenderContactShadows(hdCamera, cmd);
					this.PushFullScreenDebugTexture(hdCamera, cmd, this.m_ContactShadowBuffer, FullScreenDebugMode.ContactShadows);
					hdCamera.xr.StartSinglePass(cmd, camera, renderContext);
					this.RenderScreenSpaceShadows(hdCamera, cmd);
					hdCamera.xr.StopSinglePass(cmd, camera, renderContext);
					if (hdCamera.frameSettings.VolumeVoxelizationRunsAsync())
					{
						hdgpuasyncTask2.End(cmd, hdCamera);
					}
					else
					{
						this.VolumeVoxelizationPass(hdCamera, cmd);
					}
					this.VolumetricLightingPass(hdCamera, cmd, this.m_FrameCount);
					if (hdCamera.frameSettings.SSAORunsAsync())
					{
						hdgpuasyncTask4.EndWithPostWork(cmd, hdCamera, new Action<CommandBuffer, HDCamera>(HDRenderPipeline.<>c.<>9.<ExecuteRenderRequest>g__Callback|647_5));
					}
					this.SetContactShadowsTexture(hdCamera, this.m_ContactShadowBuffer, cmd);
					if (hdCamera.frameSettings.SSRRunsAsync())
					{
						hdgpuasyncTask3.End(cmd, hdCamera);
					}
					hdCamera.xr.StartSinglePass(cmd, camera, renderContext);
					this.RenderDeferredLighting(hdCamera, cmd);
					this.RenderForwardOpaque(cullingResults, hdCamera, renderContext, cmd);
					this.m_SharedRTManager.ResolveMSAAColor(cmd, hdCamera, this.m_CameraSssDiffuseLightingMSAABuffer, this.m_CameraSssDiffuseLightingBuffer);
					this.m_SharedRTManager.ResolveMSAAColor(cmd, hdCamera, this.GetSSSBufferMSAA(), this.GetSSSBuffer());
					if (hdCamera.frameSettings.IsEnabled(FrameSettingsField.SubsurfaceScattering))
					{
						this.BuildCoarseStencilAndResolveIfNeeded(hdCamera, this.m_SharedRTManager.GetDepthStencilBuffer(flag4), flag4 ? this.m_SharedRTManager.GetStencilBuffer(flag4) : null, this.m_SharedRTManager.GetCoarseStencilBuffer(), cmd);
					}
					this.RenderSubsurfaceScattering(hdCamera, cmd, hdCamera.frameSettings.IsEnabled(FrameSettingsField.MSAA) ? this.m_CameraColorMSAABuffer : this.m_CameraColorBuffer, this.m_CameraSssDiffuseLightingBuffer, this.m_SharedRTManager.GetDepthStencilBuffer(hdCamera.frameSettings.IsEnabled(FrameSettingsField.MSAA)), this.m_SharedRTManager.GetDepthTexture(false));
					this.RenderForwardEmissive(cullingResults, hdCamera, renderContext, cmd);
					this.RenderSky(hdCamera, cmd);
					this.SendGeometryGraphicsBuffers(cmd, hdCamera);
					this.m_PostProcessSystem.DoUserAfterOpaqueAndSky(cmd, hdCamera, this.m_CameraColorBuffer);
					this.ClearStencilBuffer(hdCamera, cmd);
					this.RenderTransparentDepthPrepass(cullingResults, hdCamera, renderContext, cmd);
					if (hdCamera.frameSettings.IsEnabled(FrameSettingsField.RayTracing))
					{
						this.RaytracingRecursiveRender(hdCamera, cmd, renderContext, cullingResults);
					}
					cmd.SetGlobalTexture(HDShaderIDs._ColorPyramidTexture, this.m_CameraColorBuffer);
					this.RenderCustomPass(renderContext, cmd, hdCamera, cullingResults2, CustomPassInjectionPoint.BeforePreRefraction);
					this.RenderForwardTransparent(cullingResults, hdCamera, true, renderContext, cmd);
					if (hdCamera.frameSettings.IsEnabled(FrameSettingsField.Refraction))
					{
						this.m_SharedRTManager.ResolveMSAAColor(cmd, hdCamera, this.m_CameraColorMSAABuffer, this.m_CameraColorBuffer);
						this.RenderColorPyramid(hdCamera, cmd, true);
						cmd.SetGlobalTexture(HDShaderIDs._ColorPyramidTexture, hdCamera.GetCurrentFrameRT(0));
					}
					else
					{
						cmd.SetGlobalTexture(HDShaderIDs._ColorPyramidTexture, TextureXR.GetBlackTexture());
					}
					this.RenderCustomPass(renderContext, cmd, hdCamera, cullingResults2, CustomPassInjectionPoint.BeforeTransparent);
					this.RenderForwardTransparent(cullingResults, hdCamera, false, renderContext, cmd);
					if (this.m_Asset.currentPlatformRenderPipelineSettings.supportMotionVectors)
					{
						this.PushFullScreenDebugTexture(hdCamera, cmd, this.m_SharedRTManager.GetMotionVectorsBuffer(false), FullScreenDebugMode.MotionVectors);
					}
					this.m_SharedRTManager.ResolveMSAAColor(cmd, hdCamera, this.m_CameraColorMSAABuffer, this.m_CameraColorBuffer);
					this.DownsampleDepthForLowResTransparency(hdCamera, cmd);
					this.RenderLowResTransparent(cullingResults, hdCamera, renderContext, cmd);
					this.UpsampleTransparent(hdCamera, cmd);
					this.RenderTransparentDepthPostpass(cullingResults, hdCamera, renderContext, cmd);
					this.RenderColorPyramid(hdCamera, cmd, false);
					this.AccumulateDistortion(cullingResults, hdCamera, renderContext, cmd);
					this.RenderDistortion(hdCamera, cmd);
					this.PushFullScreenDebugTexture(hdCamera, cmd, this.m_CameraColorBuffer, FullScreenDebugMode.NanTracker);
					this.PushFullScreenLightingDebugTexture(hdCamera, cmd, this.m_CameraColorBuffer);
				}
				this.PushColorPickerDebugTexture(cmd, hdCamera, this.m_CameraColorBuffer);
				this.RenderCustomPass(renderContext, cmd, hdCamera, cullingResults2, CustomPassInjectionPoint.BeforePostProcess);
				bool flag6 = this.WillCustomPassBeExecuted(hdCamera, CustomPassInjectionPoint.AfterPostProcess);
				aovRequest.PushCameraTexture(cmd, AOVBuffers.Color, hdCamera, this.m_CameraColorBuffer, list);
				this.RenderPostProcess(cullingResults, hdCamera, target.id, renderContext, cmd, !flag6);
				this.RenderCustomPass(renderContext, cmd, hdCamera, cullingResults2, CustomPassInjectionPoint.AfterPostProcess);
				if (hdCamera.xr.enabled && hdCamera.xr.copyDepth)
				{
					using (new ProfilingScope(cmd, ProfilingSampler.Get<HDProfileId>(HDProfileId.XRDepthCopy)))
					{
						RTHandle depthStencilBuffer = this.m_SharedRTManager.GetDepthStencilBuffer(false);
						Vector4 vector = depthStencilBuffer.rtHandleProperties.rtHandleScale / DynamicResolutionHandler.instance.GetCurrentScale();
						this.m_CopyDepthPropertyBlock.SetTexture(HDShaderIDs._InputDepth, depthStencilBuffer);
						this.m_CopyDepthPropertyBlock.SetVector(HDShaderIDs._BlitScaleBias, vector);
						this.m_CopyDepthPropertyBlock.SetInt("_FlipY", 1);
						cmd.SetRenderTarget(target.id, 0, CubemapFace.Unknown, -1);
						cmd.SetViewport(hdCamera.finalViewport);
						CoreUtils.DrawFullScreen(cmd, this.m_CopyDepth, this.m_CopyDepthPropertyBlock, 0);
					}
				}
				if (!HDUtils.PostProcessIsFinalPass() || aovRequest.isValid || flag6)
				{
					hdCamera.ExecuteCaptureActions(this.m_IntermediateAfterPostProcessBuffer, cmd);
					this.RenderDebug(hdCamera, cmd, cullingResults);
					hdCamera.xr.StopSinglePass(cmd, hdCamera.camera, renderContext);
					using (new ProfilingScope(cmd, ProfilingSampler.Get<HDProfileId>(HDProfileId.BlitToFinalRTDevBuildOnly)))
					{
						for (int i = 0; i < hdCamera.viewCount; i++)
						{
							HDRenderPipeline.BlitFinalCameraTexture(this.PrepareFinalBlitParameters(hdCamera, i), this.m_BlitPropertyBlock, this.m_IntermediateAfterPostProcessBuffer, target.id, cmd);
						}
					}
					aovRequest.PushCameraTexture(cmd, AOVBuffers.Output, hdCamera, this.m_IntermediateAfterPostProcessBuffer, list);
				}
				hdCamera.xr.EndCamera(cmd, hdCamera, renderContext);
				this.SendColorGraphicsBuffer(cmd, hdCamera);
				if (hdCamera.camera.targetTexture != null && hdCamera.camera.targetTexture.depth != 0 && !hdCamera.xr.enabled)
				{
					using (new ProfilingScope(cmd, ProfilingSampler.Get<HDProfileId>(HDProfileId.CopyDepthInTargetTexture)))
					{
						cmd.SetRenderTarget(target.id);
						cmd.SetViewport(hdCamera.finalViewport);
						this.m_CopyDepthPropertyBlock.SetTexture(HDShaderIDs._InputDepth, this.m_SharedRTManager.GetDepthStencilBuffer(false));
						this.m_CopyDepthPropertyBlock.SetInt("_FlipY", hdCamera.isMainGameView ? 1 : 0);
						this.m_CopyDepthPropertyBlock.SetVector(HDShaderIDs._BlitScaleBias, new Vector4(1f, 1f, 0f, 0f));
						CoreUtils.DrawFullScreen(cmd, this.m_CopyDepth, this.m_CopyDepthPropertyBlock, 0);
					}
				}
				aovRequest.PushCameraTexture(cmd, AOVBuffers.DepthStencil, hdCamera, this.m_SharedRTManager.GetDepthStencilBuffer(false), list);
				aovRequest.PushCameraTexture(cmd, AOVBuffers.Normals, hdCamera, this.m_SharedRTManager.GetNormalBuffer(false), list);
				if (this.m_Asset.currentPlatformRenderPipelineSettings.supportMotionVectors)
				{
					aovRequest.PushCameraTexture(cmd, AOVBuffers.MotionVectors, hdCamera, this.m_SharedRTManager.GetMotionVectorsBuffer(false), list);
				}
				aovRequest.Execute(cmd, list, RenderOutputProperties.From(hdCamera));
			}
			renderContext.ExecuteCommandBuffer(cmd);
			cmd.Clear();
		}

		// Token: 0x060003E3 RID: 995 RVA: 0x00023010 File Offset: 0x00021210
		internal RTHandle GetExposureTexture(HDCamera hdCamera)
		{
			return this.m_PostProcessSystem.GetExposureTexture(hdCamera);
		}

		// Token: 0x060003E4 RID: 996 RVA: 0x00023020 File Offset: 0x00021220
		private HDRenderPipeline.BlitFinalCameraTextureParameters PrepareFinalBlitParameters(HDCamera hdCamera, int viewIndex)
		{
			HDRenderPipeline.BlitFinalCameraTextureParameters blitFinalCameraTextureParameters = default(HDRenderPipeline.BlitFinalCameraTextureParameters);
			if (hdCamera.xr.enabled)
			{
				blitFinalCameraTextureParameters.viewport = hdCamera.xr.GetViewport(viewIndex);
				blitFinalCameraTextureParameters.srcTexArraySlice = viewIndex;
				blitFinalCameraTextureParameters.dstTexArraySlice = hdCamera.xr.GetTextureArraySlice(viewIndex);
			}
			else
			{
				blitFinalCameraTextureParameters.viewport = hdCamera.finalViewport;
				blitFinalCameraTextureParameters.srcTexArraySlice = -1;
				blitFinalCameraTextureParameters.dstTexArraySlice = -1;
			}
			blitFinalCameraTextureParameters.flip = hdCamera.flipYMode == HDAdditionalCameraData.FlipYMode.ForceFlipY || hdCamera.isMainGameView;
			blitFinalCameraTextureParameters.blitMaterial = HDUtils.GetBlitMaterial(TextureXR.useTexArray ? TextureDimension.Tex2DArray : TextureDimension.Tex2D, blitFinalCameraTextureParameters.srcTexArraySlice >= 0);
			return blitFinalCameraTextureParameters;
		}

		// Token: 0x060003E5 RID: 997 RVA: 0x000230CC File Offset: 0x000212CC
		private static void BlitFinalCameraTexture(HDRenderPipeline.BlitFinalCameraTextureParameters parameters, MaterialPropertyBlock propertyBlock, RTHandle source, RenderTargetIdentifier destination, CommandBuffer cmd)
		{
			Vector4 vector = new Vector4(parameters.viewport.width / (float)source.rt.width, parameters.viewport.height / (float)source.rt.height, 0f, 0f);
			if (parameters.flip)
			{
				vector.w = vector.y;
				vector.y *= -1f;
			}
			propertyBlock.SetTexture(HDShaderIDs._BlitTexture, source);
			propertyBlock.SetVector(HDShaderIDs._BlitScaleBias, vector);
			propertyBlock.SetFloat(HDShaderIDs._BlitMipLevel, 0f);
			propertyBlock.SetInt(HDShaderIDs._BlitTexArraySlice, parameters.srcTexArraySlice);
			HDUtils.DrawFullScreen(cmd, parameters.viewport, parameters.blitMaterial, destination, propertyBlock, 0, parameters.dstTexArraySlice);
		}

		// Token: 0x060003E6 RID: 998 RVA: 0x000231A0 File Offset: 0x000213A0
		private void SetupCameraProperties(HDCamera hdCamera, ScriptableRenderContext renderContext, CommandBuffer cmd)
		{
			renderContext.ExecuteCommandBuffer(cmd);
			cmd.Clear();
			if (hdCamera.xr.legacyMultipassEnabled)
			{
				renderContext.SetupCameraProperties(hdCamera.camera, hdCamera.xr.enabled, hdCamera.xr.legacyMultipassEye);
				return;
			}
			renderContext.SetupCameraProperties(hdCamera.camera, hdCamera.xr.enabled);
		}

		// Token: 0x060003E7 RID: 999 RVA: 0x00023204 File Offset: 0x00021404
		private void InitializeGlobalResources(ScriptableRenderContext renderContext)
		{
			CommandBuffer commandBuffer = CommandBufferPool.Get("");
			for (int i = 0; i < this.m_IBLFilterArray.Length; i++)
			{
				if (!this.m_IBLFilterArray[i].IsInitialized())
				{
					this.m_IBLFilterArray[i].Initialize(commandBuffer);
				}
			}
			foreach (RenderPipelineMaterial renderPipelineMaterial in this.m_MaterialList)
			{
				renderPipelineMaterial.RenderInit(commandBuffer);
			}
			TextureXR.Initialize(commandBuffer, this.defaultResources.shaders.clearUIntTextureCS);
			renderContext.ExecuteCommandBuffer(commandBuffer);
			CommandBufferPool.Release(commandBuffer);
		}

		// Token: 0x060003E8 RID: 1000 RVA: 0x000232B4 File Offset: 0x000214B4
		private bool TryCalculateFrameParameters(Camera camera, XRPass xrPass, out HDAdditionalCameraData additionalCameraData, out HDCamera hdCamera, out ScriptableCullingParameters cullingParams)
		{
			additionalCameraData = HDUtils.TryGetAdditionalCameraDataOrDefault(camera);
			hdCamera = null;
			cullingParams = default(ScriptableCullingParameters);
			FrameSettings frameSettings = default(FrameSettings);
			if (this.m_FrameSettingsHistoryEnabled && camera.cameraType != CameraType.Preview && camera.cameraType != CameraType.Reflection)
			{
				FrameSettingsHistory.AggregateFrameSettings(ref frameSettings, camera, additionalCameraData, this.m_Asset, this.m_DefaultAsset);
			}
			else
			{
				FrameSettings.AggregateFrameSettings(ref frameSettings, camera, additionalCameraData, this.m_Asset, this.m_DefaultAsset);
			}
			if (additionalCameraData.fullscreenPassthrough)
			{
				return false;
			}
			DebugDisplaySettings debugDisplaySettings = ((camera.cameraType == CameraType.Reflection || camera.cameraType == CameraType.Preview) ? HDRenderPipeline.s_NeutralDebugDisplaySettings : this.m_DebugDisplaySettings);
			if (debugDisplaySettings.IsDebugDisplayEnabled())
			{
				if (debugDisplaySettings.IsDebugDisplayRemovePostprocess())
				{
					frameSettings.SetEnabled(FrameSettingsField.Postprocess, false);
					frameSettings.SetEnabled(FrameSettingsField.CustomPass, false);
				}
				if (!debugDisplaySettings.DebugNeedsExposure())
				{
					frameSettings.SetEnabled(FrameSettingsField.ExposureControl, false);
				}
				if (debugDisplaySettings.data.lightingDebugSettings.debugLightingMode == DebugLightingMode.LuxMeter)
				{
					frameSettings.SetEnabled(FrameSettingsField.SubsurfaceScattering, false);
				}
			}
			if (CoreUtils.IsSceneLightingDisabled(camera))
			{
				frameSettings.SetEnabled(FrameSettingsField.ExposureControl, false);
			}
			if (camera.cameraType != CameraType.Game)
			{
				frameSettings.SetEnabled(FrameSettingsField.ObjectMotionVectors, false);
			}
			hdCamera = HDCamera.GetOrCreate(camera, xrPass.multipassId);
			hdCamera.Update(frameSettings, this, this.m_MSAASamples, xrPass);
			if (additionalCameraData != null && additionalCameraData.hasCustomRender)
			{
				return false;
			}
			if (hdCamera.xr.enabled)
			{
				cullingParams = hdCamera.xr.cullingParams;
			}
			else if (!camera.TryGetCullingParameters(camera.stereoEnabled, out cullingParams))
			{
				return false;
			}
			if (this.m_DebugDisplaySettings.IsCameraFreezeEnabled())
			{
				if (this.m_DebugDisplaySettings.IsCameraFrozen(camera))
				{
					if (!this.frozenCullingParamAvailable)
					{
						this.frozenCullingParams = cullingParams;
						this.frozenCullingParamAvailable = true;
					}
					cullingParams = this.frozenCullingParams;
				}
			}
			else
			{
				this.frozenCullingParamAvailable = false;
			}
			this.LightLoopUpdateCullingParameters(ref cullingParams, hdCamera);
			if (hdCamera.frameSettings.IsEnabled(FrameSettingsField.ReflectionProbe))
			{
				cullingParams.cullingOptions |= CullingOptions.NeedsReflectionProbes;
			}
			else
			{
				cullingParams.cullingOptions &= ~CullingOptions.NeedsReflectionProbes;
			}
			return true;
		}

		// Token: 0x060003E9 RID: 1001 RVA: 0x000234C4 File Offset: 0x000216C4
		private static bool TryCull(Camera camera, HDCamera hdCamera, ScriptableRenderContext renderContext, SkyManager skyManager, ScriptableCullingParameters cullingParams, HDRenderPipelineAsset hdrp, ref HDRenderPipeline.HDCullingResults cullingResults)
		{
			float lodBias = QualitySettings.lodBias;
			int maximumLODLevel = QualitySettings.maximumLODLevel;
			bool flag;
			try
			{
				QualitySettings.lodBias = hdCamera.frameSettings.GetResolvedLODBias(hdrp);
				QualitySettings.maximumLODLevel = hdCamera.frameSettings.GetResolvedMaximumLODLevel(hdrp);
				RenderPipeline.BeginCameraRendering(renderContext, camera);
				DecalSystem.CullRequest cullRequest = null;
				if (hdCamera.frameSettings.IsEnabled(FrameSettingsField.Decals))
				{
					cullRequest = GenericPool<DecalSystem.CullRequest>.Get();
					DecalSystem.instance.CurrentCamera = camera;
					DecalSystem.instance.BeginCull(cullRequest);
				}
				HDProbeCullState hdprobeCullState = default(HDProbeCullState);
				if (hdCamera.frameSettings.IsEnabled(FrameSettingsField.PlanarProbe))
				{
					hdprobeCullState = HDProbeSystem.PrepareCull(camera);
				}
				skyManager.SetupAmbientProbe(hdCamera);
				using (new ProfilingScope(null, ProfilingSampler.Get<HDProfileId>(HDProfileId.CullResultsCull)))
				{
					cullingResults.cullingResults = renderContext.Cull(ref cullingParams);
				}
				if (hdCamera.frameSettings.IsEnabled(FrameSettingsField.CustomPass))
				{
					using (new ProfilingScope(null, ProfilingSampler.Get<HDProfileId>(HDProfileId.CustomPassCullResultsCull)))
					{
						cullingResults.customPassCullingResults = CustomPassVolume.Cull(renderContext, hdCamera);
					}
				}
				if (hdCamera.frameSettings.IsEnabled(FrameSettingsField.PlanarProbe))
				{
					HDProbeSystem.QueryCullResults(hdprobeCullState, ref cullingResults.hdProbeCullingResults);
				}
				else
				{
					cullingResults.hdProbeCullingResults = default(HDProbeCullingResults);
				}
				if (hdCamera.frameSettings.IsEnabled(FrameSettingsField.Decals))
				{
					using (new ProfilingScope(null, ProfilingSampler.Get<HDProfileId>(HDProfileId.DBufferPrepareDrawData)))
					{
						DecalSystem.instance.EndCull(cullRequest, cullingResults.decalCullResults);
					}
				}
				if (cullRequest != null)
				{
					cullRequest.Clear();
					GenericPool<DecalSystem.CullRequest>.Release(cullRequest);
				}
				flag = true;
			}
			finally
			{
				QualitySettings.lodBias = lodBias;
				QualitySettings.maximumLODLevel = maximumLODLevel;
			}
			return flag;
		}

		// Token: 0x060003EA RID: 1002 RVA: 0x00002646 File Offset: 0x00000846
		private void RenderGizmos(CommandBuffer cmd, Camera camera, ScriptableRenderContext renderContext, GizmoSubset gizmoSubset)
		{
		}

		// Token: 0x060003EB RID: 1003 RVA: 0x000236D4 File Offset: 0x000218D4
		private static RendererListDesc CreateOpaqueRendererListDesc(CullingResults cull, Camera camera, ShaderTagId passName, PerObjectData rendererConfiguration = PerObjectData.None, RenderQueueRange? renderQueueRange = null, RenderStateBlock? stateBlock = null, Material overrideMaterial = null, bool excludeObjectMotionVectors = false)
		{
			return new RendererListDesc(passName, cull, camera)
			{
				rendererConfiguration = rendererConfiguration,
				renderQueueRange = ((renderQueueRange != null) ? renderQueueRange.Value : HDRenderQueue.k_RenderQueue_AllOpaque),
				sortingCriteria = SortingCriteria.CommonOpaque,
				stateBlock = stateBlock,
				overrideMaterial = overrideMaterial,
				excludeObjectMotionVectors = excludeObjectMotionVectors
			};
		}

		// Token: 0x060003EC RID: 1004 RVA: 0x00023738 File Offset: 0x00021938
		private static RendererListDesc CreateOpaqueRendererListDesc(CullingResults cull, Camera camera, ShaderTagId[] passNames, PerObjectData rendererConfiguration = PerObjectData.None, RenderQueueRange? renderQueueRange = null, RenderStateBlock? stateBlock = null, Material overrideMaterial = null, bool excludeObjectMotionVectors = false)
		{
			return new RendererListDesc(passNames, cull, camera)
			{
				rendererConfiguration = rendererConfiguration,
				renderQueueRange = ((renderQueueRange != null) ? renderQueueRange.Value : HDRenderQueue.k_RenderQueue_AllOpaque),
				sortingCriteria = SortingCriteria.CommonOpaque,
				stateBlock = stateBlock,
				overrideMaterial = overrideMaterial,
				excludeObjectMotionVectors = excludeObjectMotionVectors
			};
		}

		// Token: 0x060003ED RID: 1005 RVA: 0x0002379C File Offset: 0x0002199C
		private static RendererListDesc CreateTransparentRendererListDesc(CullingResults cull, Camera camera, ShaderTagId passName, PerObjectData rendererConfiguration = PerObjectData.None, RenderQueueRange? renderQueueRange = null, RenderStateBlock? stateBlock = null, Material overrideMaterial = null, bool excludeObjectMotionVectors = false)
		{
			return new RendererListDesc(passName, cull, camera)
			{
				rendererConfiguration = rendererConfiguration,
				renderQueueRange = ((renderQueueRange != null) ? renderQueueRange.Value : HDRenderQueue.k_RenderQueue_AllTransparent),
				sortingCriteria = (SortingCriteria.SortingLayer | SortingCriteria.RenderQueue | SortingCriteria.BackToFront | SortingCriteria.OptimizeStateChanges | SortingCriteria.RendererPriority),
				stateBlock = stateBlock,
				overrideMaterial = overrideMaterial,
				excludeObjectMotionVectors = excludeObjectMotionVectors
			};
		}

		// Token: 0x060003EE RID: 1006 RVA: 0x00023800 File Offset: 0x00021A00
		private static RendererListDesc CreateTransparentRendererListDesc(CullingResults cull, Camera camera, ShaderTagId[] passNames, PerObjectData rendererConfiguration = PerObjectData.None, RenderQueueRange? renderQueueRange = null, RenderStateBlock? stateBlock = null, Material overrideMaterial = null, bool excludeObjectMotionVectors = false)
		{
			return new RendererListDesc(passNames, cull, camera)
			{
				rendererConfiguration = rendererConfiguration,
				renderQueueRange = ((renderQueueRange != null) ? renderQueueRange.Value : HDRenderQueue.k_RenderQueue_AllTransparent),
				sortingCriteria = (SortingCriteria.SortingLayer | SortingCriteria.RenderQueue | SortingCriteria.BackToFront | SortingCriteria.OptimizeStateChanges | SortingCriteria.RendererPriority),
				stateBlock = stateBlock,
				overrideMaterial = overrideMaterial,
				excludeObjectMotionVectors = excludeObjectMotionVectors
			};
		}

		// Token: 0x060003EF RID: 1007 RVA: 0x00023864 File Offset: 0x00021A64
		private static void DrawOpaqueRendererList(in ScriptableRenderContext renderContext, CommandBuffer cmd, in FrameSettings frameSettings, RendererList rendererList)
		{
			FrameSettings frameSettings2 = frameSettings;
			if (!frameSettings2.IsEnabled(FrameSettingsField.OpaqueObjects))
			{
				return;
			}
			HDUtils.DrawRendererList(renderContext, cmd, rendererList);
		}

		// Token: 0x060003F0 RID: 1008 RVA: 0x00023890 File Offset: 0x00021A90
		private static void DrawTransparentRendererList(in ScriptableRenderContext renderContext, CommandBuffer cmd, in FrameSettings frameSettings, RendererList rendererList)
		{
			FrameSettings frameSettings2 = frameSettings;
			if (!frameSettings2.IsEnabled(FrameSettingsField.TransparentObjects))
			{
				return;
			}
			HDUtils.DrawRendererList(renderContext, cmd, rendererList);
		}

		// Token: 0x060003F1 RID: 1009 RVA: 0x000238BC File Offset: 0x00021ABC
		private void AccumulateDistortion(CullingResults cullResults, HDCamera hdCamera, ScriptableRenderContext renderContext, CommandBuffer cmd)
		{
			FrameSettings frameSettings = hdCamera.frameSettings;
			if (!frameSettings.IsEnabled(FrameSettingsField.Distortion))
			{
				return;
			}
			using (new ProfilingScope(cmd, ProfilingSampler.Get<HDProfileId>(HDProfileId.Distortion)))
			{
				CoreUtils.SetRenderTarget(cmd, this.m_DistortionBuffer, this.m_SharedRTManager.GetDepthStencilBuffer(false), ClearFlag.Color, Color.clear, 0, CubemapFace.Unknown, -1);
				RendererListDesc rendererListDesc = HDRenderPipeline.CreateTransparentRendererListDesc(cullResults, hdCamera.camera, HDShaderPassNames.s_DistortionVectorsName, PerObjectData.None, null, null, null, false);
				RendererList rendererList = RendererList.Create(in rendererListDesc);
				frameSettings = hdCamera.frameSettings;
				HDRenderPipeline.DrawTransparentRendererList(in renderContext, cmd, in frameSettings, rendererList);
			}
		}

		// Token: 0x060003F2 RID: 1010 RVA: 0x00023974 File Offset: 0x00021B74
		private void RenderDistortion(HDCamera hdCamera, CommandBuffer cmd)
		{
			if (!hdCamera.frameSettings.IsEnabled(FrameSettingsField.Distortion))
			{
				return;
			}
			using (new ProfilingScope(cmd, ProfilingSampler.Get<HDProfileId>(HDProfileId.ApplyDistortion)))
			{
				RTHandle currentFrameRT = hdCamera.GetCurrentFrameRT(0);
				CoreUtils.SetRenderTarget(cmd, this.m_CameraColorBuffer, ClearFlag.None, 0, CubemapFace.Unknown, -1);
				this.m_ApplyDistortionMaterial.SetTexture(HDShaderIDs._DistortionTexture, this.m_DistortionBuffer);
				this.m_ApplyDistortionMaterial.SetTexture(HDShaderIDs._ColorPyramidTexture, currentFrameRT);
				Vector4 vector = new Vector4((float)hdCamera.actualWidth, (float)hdCamera.actualHeight, 1f / (float)hdCamera.actualWidth, 1f / (float)hdCamera.actualHeight);
				this.m_ApplyDistortionMaterial.SetVector(HDShaderIDs._Size, vector);
				this.m_ApplyDistortionMaterial.SetInt(HDShaderIDs._StencilMask, 4);
				this.m_ApplyDistortionMaterial.SetInt(HDShaderIDs._StencilRef, 4);
				HDUtils.DrawFullScreen(cmd, this.m_ApplyDistortionMaterial, this.m_CameraColorBuffer, this.m_SharedRTManager.GetDepthStencilBuffer(false), null, 0);
			}
		}

		// Token: 0x060003F3 RID: 1011 RVA: 0x00023A90 File Offset: 0x00021C90
		private HDRenderPipeline.DepthPrepassParameters PrepareDepthPrepass(CullingResults cull, HDCamera hdCamera)
		{
			HDRenderPipeline.DepthPrepassParameters depthPrepassParameters = default(HDRenderPipeline.DepthPrepassParameters);
			bool flag = hdCamera.frameSettings.IsEnabled(FrameSettingsField.Decals);
			bool flag2 = hdCamera.frameSettings.IsEnabled(FrameSettingsField.DepthPrepassWithDeferredRendering) || flag;
			bool flag3 = hdCamera.frameSettings.IsEnabled(FrameSettingsField.ObjectMotionVectors);
			depthPrepassParameters.shouldRenderMotionVectorAfterGBuffer = hdCamera.frameSettings.litShaderMode == LitShaderMode.Deferred && !flag2;
			depthPrepassParameters.hasDepthOnlyPass = false;
			LitShaderMode litShaderMode = hdCamera.frameSettings.litShaderMode;
			if (litShaderMode != LitShaderMode.Forward)
			{
				if (litShaderMode != LitShaderMode.Deferred)
				{
					throw new ArgumentOutOfRangeException("Unknown ShaderLitMode");
				}
				depthPrepassParameters.passName = (flag2 ? (flag ? "Depth Prepass (deferred) forced by Decals" : "Depth Prepass (deferred)") : "Depth Prepass (deferred incomplete)");
				depthPrepassParameters.profilingId = (flag2 ? (flag ? HDProfileId.DepthPrepassDeferredForDecals : HDProfileId.DepthPrepassDeferred) : HDProfileId.DepthPrepassDeferredIncomplete);
				bool flag4 = flag2 && flag3;
				RenderQueueRange renderQueueRange = new RenderQueueRange
				{
					lowerBound = 2450,
					upperBound = 2499
				};
				depthPrepassParameters.hasDepthOnlyPass = true;
				depthPrepassParameters.depthOnlyRendererListDesc = HDRenderPipeline.CreateOpaqueRendererListDesc(cull, hdCamera.camera, this.m_DepthOnlyPassNames, PerObjectData.None, new RenderQueueRange?(flag2 ? HDRenderQueue.k_RenderQueue_AllOpaque : renderQueueRange), null, null, flag4);
				depthPrepassParameters.mrtRendererListDesc = HDRenderPipeline.CreateOpaqueRendererListDesc(cull, hdCamera.camera, this.m_DepthForwardOnlyPassNames, PerObjectData.None, null, null, null, flag4);
			}
			else
			{
				depthPrepassParameters.passName = "Depth Prepass (forward)";
				depthPrepassParameters.profilingId = HDProfileId.DepthPrepassForward;
				depthPrepassParameters.mrtRendererListDesc = HDRenderPipeline.CreateOpaqueRendererListDesc(cull, hdCamera.camera, this.m_DepthOnlyAndDepthForwardOnlyPassNames, PerObjectData.None, null, null, null, flag3);
			}
			depthPrepassParameters.renderRayTracingPrepass = false;
			if (hdCamera.frameSettings.IsEnabled(FrameSettingsField.RayTracing) && hdCamera.volumeStack.GetComponent<RecursiveRendering>().enable.value)
			{
				depthPrepassParameters.renderRayTracingPrepass = true;
				depthPrepassParameters.rayTracingOpaqueRLDesc = HDRenderPipeline.CreateOpaqueRendererListDesc(cull, hdCamera.camera, this.m_DepthOnlyAndDepthForwardOnlyPassNames, PerObjectData.None, new RenderQueueRange?(HDRenderQueue.k_RenderQueue_AllOpaqueRaytracing), null, null, false);
				depthPrepassParameters.rayTracingTransparentRLDesc = HDRenderPipeline.CreateOpaqueRendererListDesc(cull, hdCamera.camera, this.m_DepthOnlyAndDepthForwardOnlyPassNames, PerObjectData.None, new RenderQueueRange?(HDRenderQueue.k_RenderQueue_AllTransparentRaytracing), null, null, false);
			}
			return depthPrepassParameters;
		}

		// Token: 0x060003F4 RID: 1012 RVA: 0x00023CEC File Offset: 0x00021EEC
		private static void RenderDepthPrepass(ScriptableRenderContext renderContext, CommandBuffer cmd, FrameSettings frameSettings, RenderTargetIdentifier[] mrt, RTHandle depthBuffer, in RendererList depthOnlyRendererList, in RendererList mrtRendererList, bool hasDepthOnlyPass, in RendererList rayTracingOpaqueRL, in RendererList rayTracingTransparentRL, bool renderRayTracingPrepass)
		{
			CoreUtils.SetRenderTarget(cmd, depthBuffer, ClearFlag.None, 0, CubemapFace.Unknown, -1);
			if (hasDepthOnlyPass)
			{
				HDRenderPipeline.DrawOpaqueRendererList(in renderContext, cmd, in frameSettings, depthOnlyRendererList);
			}
			CoreUtils.SetRenderTarget(cmd, mrt, depthBuffer);
			HDRenderPipeline.DrawOpaqueRendererList(in renderContext, cmd, in frameSettings, mrtRendererList);
			if (renderRayTracingPrepass)
			{
				HDUtils.DrawRendererList(renderContext, cmd, rayTracingOpaqueRL);
				HDUtils.DrawRendererList(renderContext, cmd, rayTracingTransparentRL);
			}
		}

		// Token: 0x060003F5 RID: 1013 RVA: 0x00023D54 File Offset: 0x00021F54
		private bool RenderDepthPrepass(CullingResults cull, HDCamera hdCamera, ScriptableRenderContext renderContext, CommandBuffer cmd)
		{
			HDRenderPipeline.DepthPrepassParameters depthPrepassParameters = this.PrepareDepthPrepass(cull, hdCamera);
			RendererList rendererList = RendererList.Create(in depthPrepassParameters.depthOnlyRendererListDesc);
			RendererList rendererList2 = RendererList.Create(in depthPrepassParameters.mrtRendererListDesc);
			RendererList rendererList3 = RendererList.Create(in depthPrepassParameters.rayTracingOpaqueRLDesc);
			RendererList rendererList4 = RendererList.Create(in depthPrepassParameters.rayTracingTransparentRLDesc);
			using (new ProfilingScope(cmd, ProfilingSampler.Get<HDProfileId>(depthPrepassParameters.profilingId)))
			{
				HDRenderPipeline.RenderDepthPrepass(renderContext, cmd, hdCamera.frameSettings, this.m_SharedRTManager.GetPrepassBuffersRTI(hdCamera.frameSettings), this.m_SharedRTManager.GetDepthStencilBuffer(hdCamera.frameSettings.IsEnabled(FrameSettingsField.MSAA)), in rendererList, in rendererList2, depthPrepassParameters.hasDepthOnlyPass, in rendererList3, in rendererList4, depthPrepassParameters.renderRayTracingPrepass);
			}
			return depthPrepassParameters.shouldRenderMotionVectorAfterGBuffer;
		}

		// Token: 0x060003F6 RID: 1014 RVA: 0x00023E28 File Offset: 0x00022028
		private void RenderGBuffer(CullingResults cull, HDCamera hdCamera, ScriptableRenderContext renderContext, CommandBuffer cmd)
		{
			FrameSettings frameSettings = hdCamera.frameSettings;
			if (frameSettings.litShaderMode != LitShaderMode.Deferred)
			{
				return;
			}
			using (new ProfilingScope(cmd, this.m_CurrentDebugDisplaySettings.IsDebugDisplayEnabled() ? ProfilingSampler.Get<HDProfileId>(HDProfileId.GBufferDebug) : ProfilingSampler.Get<HDProfileId>(HDProfileId.GBuffer)))
			{
				CoreUtils.SetRenderTarget(cmd, this.m_GbufferManager.GetBuffersRTI(hdCamera.frameSettings), this.m_SharedRTManager.GetDepthStencilBuffer(false));
				RendererListDesc rendererListDesc = HDRenderPipeline.CreateOpaqueRendererListDesc(cull, hdCamera.camera, HDShaderPassNames.s_GBufferName, this.m_CurrentRendererConfigurationBakedLighting, null, null, null, false);
				RendererList rendererList = RendererList.Create(in rendererListDesc);
				frameSettings = hdCamera.frameSettings;
				HDRenderPipeline.DrawOpaqueRendererList(in renderContext, cmd, in frameSettings, rendererList);
				this.m_GbufferManager.BindBufferAsTextures(cmd);
			}
		}

		// Token: 0x060003F7 RID: 1015 RVA: 0x00023F08 File Offset: 0x00022108
		private void RenderDBuffer(HDCamera hdCamera, CommandBuffer cmd, ScriptableRenderContext renderContext, CullingResults cullingResults)
		{
			if (!hdCamera.frameSettings.IsEnabled(FrameSettingsField.Decals))
			{
				this.m_DbufferManager.BindBlackTextures(cmd);
				return;
			}
			this.CopyDepthBufferIfNeeded(hdCamera, cmd);
			using (new ProfilingScope(cmd, ProfilingSampler.Get<HDProfileId>(HDProfileId.DBufferRender)))
			{
				bool perChannelMask = this.m_Asset.currentPlatformRenderPipelineSettings.decalSettings.perChannelMask;
				bool flag = perChannelMask;
				RenderTargetIdentifier[] buffersRTI = this.m_DbufferManager.GetBuffersRTI();
				RTHandle[] rthandles = this.m_DbufferManager.GetRTHandles();
				RTHandle depthStencilBuffer = this.m_SharedRTManager.GetDepthStencilBuffer(false);
				ComputeBuffer propertyMaskBuffer = this.m_DbufferManager.propertyMaskBuffer;
				ComputeShader clearPropertyMaskBufferShader = this.m_DbufferManager.clearPropertyMaskBufferShader;
				int clearPropertyMaskBufferKernel = this.m_DbufferManager.clearPropertyMaskBufferKernel;
				int propertyMaskBufferSize = this.m_DbufferManager.propertyMaskBufferSize;
				RendererListDesc rendererListDesc = this.PrepareMeshDecalsRendererList(cullingResults, hdCamera, perChannelMask);
				HDRenderPipeline.RenderDBuffer(flag, buffersRTI, rthandles, depthStencilBuffer, propertyMaskBuffer, clearPropertyMaskBufferShader, clearPropertyMaskBufferKernel, propertyMaskBufferSize, RendererList.Create(in rendererListDesc), renderContext, cmd);
				cmd.SetGlobalBuffer(HDShaderIDs._DecalPropertyMaskBufferSRV, this.m_DbufferManager.propertyMaskBuffer);
				this.m_DbufferManager.BindBufferAsTextures(cmd);
			}
		}

		// Token: 0x060003F8 RID: 1016 RVA: 0x0002400C File Offset: 0x0002220C
		private void DecalNormalPatch(HDCamera hdCamera, CommandBuffer cmd, ScriptableRenderContext renderContext)
		{
			if (hdCamera.frameSettings.IsEnabled(FrameSettingsField.Decals) && !hdCamera.frameSettings.IsEnabled(FrameSettingsField.MSAA))
			{
				using (new ProfilingScope(cmd, ProfilingSampler.Get<HDProfileId>(HDProfileId.DBufferNormal)))
				{
					HDRenderPipeline.DBufferNormalPatchParameters dbufferNormalPatchParameters = this.PrepareDBufferNormalPatchParameters(hdCamera);
					dbufferNormalPatchParameters.decalNormalBufferMaterial.SetInt(HDShaderIDs._DecalNormalBufferStencilReadMask, dbufferNormalPatchParameters.stencilMask);
					dbufferNormalPatchParameters.decalNormalBufferMaterial.SetInt(HDShaderIDs._DecalNormalBufferStencilRef, dbufferNormalPatchParameters.stencilRef);
					CoreUtils.SetRenderTarget(cmd, this.m_SharedRTManager.GetDepthStencilBuffer(false), ClearFlag.None, 0, CubemapFace.Unknown, -1);
					cmd.SetRandomWriteTarget(1, this.m_SharedRTManager.GetNormalBuffer(false));
					cmd.DrawProcedural(Matrix4x4.identity, dbufferNormalPatchParameters.decalNormalBufferMaterial, 0, MeshTopology.Triangles, 3, 1);
					cmd.ClearRandomWriteTargets();
				}
			}
		}

		// Token: 0x060003F9 RID: 1017 RVA: 0x000240F0 File Offset: 0x000222F0
		private RendererListDesc PrepareMeshDecalsRendererList(CullingResults cullingResults, HDCamera hdCamera, bool use4RTs)
		{
			return new RendererListDesc(use4RTs ? this.m_Decals4RTPassNames : this.m_Decals3RTPassNames, cullingResults, hdCamera.camera)
			{
				sortingCriteria = SortingCriteria.CommonOpaque,
				rendererConfiguration = PerObjectData.None,
				renderQueueRange = HDRenderQueue.k_RenderQueue_AllOpaque
			};
		}

		// Token: 0x060003FA RID: 1018 RVA: 0x0002413C File Offset: 0x0002233C
		private static void PushDecalsGlobalParams(HDCamera hdCamera, CommandBuffer cmd)
		{
			if (hdCamera.frameSettings.IsEnabled(FrameSettingsField.Decals))
			{
				cmd.SetGlobalInt(HDShaderIDs._EnableDecals, 1);
				cmd.SetGlobalVector(HDShaderIDs._DecalAtlasResolution, new Vector2((float)HDUtils.hdrpSettings.decalSettings.atlasWidth, (float)HDUtils.hdrpSettings.decalSettings.atlasHeight));
				return;
			}
			cmd.SetGlobalInt(HDShaderIDs._EnableDecals, 0);
		}

		// Token: 0x060003FB RID: 1019 RVA: 0x000241AC File Offset: 0x000223AC
		private static void RenderDBuffer(bool use4RTs, RenderTargetIdentifier[] mrt, RTHandle[] rtHandles, RTHandle depthStencilBuffer, ComputeBuffer propertyMaskBuffer, ComputeShader propertyMaskClearShader, int propertyMaskClearShaderKernel, int propertyMaskBufferSize, RendererList meshDecalsRendererList, ScriptableRenderContext renderContext, CommandBuffer cmd)
		{
			Color color = new Color(0f, 0f, 0f, 1f);
			Color color2 = new Color(0.5f, 0.5f, 0.5f, 1f);
			Color color3 = new Color(1f, 1f, 1f, 1f);
			CoreUtils.SetRenderTarget(cmd, rtHandles[0], ClearFlag.Color, color, 0, CubemapFace.Unknown, -1);
			CoreUtils.SetRenderTarget(cmd, rtHandles[1], ClearFlag.Color, color2, 0, CubemapFace.Unknown, -1);
			CoreUtils.SetRenderTarget(cmd, rtHandles[2], ClearFlag.Color, color, 0, CubemapFace.Unknown, -1);
			if (use4RTs)
			{
				CoreUtils.SetRenderTarget(cmd, rtHandles[3], ClearFlag.Color, color3, 0, CubemapFace.Unknown, -1);
				CoreUtils.SetRenderTarget(cmd, mrt, depthStencilBuffer);
			}
			else
			{
				for (int i = 0; i < 3; i++)
				{
					HDRenderPipeline.m_Dbuffer3RtIds[i] = mrt[i];
				}
				CoreUtils.SetRenderTarget(cmd, HDRenderPipeline.m_Dbuffer3RtIds, depthStencilBuffer);
			}
			cmd.SetComputeBufferParam(propertyMaskClearShader, propertyMaskClearShaderKernel, HDShaderIDs._DecalPropertyMaskBuffer, propertyMaskBuffer);
			cmd.DispatchCompute(propertyMaskClearShader, propertyMaskClearShaderKernel, propertyMaskBufferSize / 64, 1, 1);
			cmd.SetRandomWriteTarget(use4RTs ? 4 : 3, propertyMaskBuffer);
			HDUtils.DrawRendererList(renderContext, cmd, meshDecalsRendererList);
			DecalSystem.instance.RenderIntoDBuffer(cmd);
			cmd.ClearRandomWriteTargets();
		}

		// Token: 0x060003FC RID: 1020 RVA: 0x000242D4 File Offset: 0x000224D4
		private HDRenderPipeline.DBufferNormalPatchParameters PrepareDBufferNormalPatchParameters(HDCamera hdCamera)
		{
			HDRenderPipeline.DBufferNormalPatchParameters dbufferNormalPatchParameters = default(HDRenderPipeline.DBufferNormalPatchParameters);
			dbufferNormalPatchParameters.decalNormalBufferMaterial = this.m_DecalNormalBufferMaterial;
			LitShaderMode litShaderMode = hdCamera.frameSettings.litShaderMode;
			if (litShaderMode != LitShaderMode.Forward)
			{
				if (litShaderMode != LitShaderMode.Deferred)
				{
					throw new ArgumentOutOfRangeException("Unknown ShaderLitMode");
				}
				dbufferNormalPatchParameters.stencilMask = 18;
				dbufferNormalPatchParameters.stencilRef = 16;
			}
			else
			{
				dbufferNormalPatchParameters.stencilMask = 16;
				dbufferNormalPatchParameters.stencilRef = 16;
			}
			return dbufferNormalPatchParameters;
		}

		// Token: 0x060003FD RID: 1021 RVA: 0x00024344 File Offset: 0x00022544
		private RendererListDesc PrepareForwardEmissiveRendererList(CullingResults cullResults, HDCamera hdCamera)
		{
			return new RendererListDesc(this.m_DecalsEmissivePassNames, cullResults, hdCamera.camera)
			{
				renderQueueRange = HDRenderQueue.k_RenderQueue_AllOpaque,
				sortingCriteria = SortingCriteria.CommonOpaque,
				rendererConfiguration = PerObjectData.None
			};
		}

		// Token: 0x060003FE RID: 1022 RVA: 0x00024384 File Offset: 0x00022584
		private void RenderForwardEmissive(CullingResults cullResults, HDCamera hdCamera, ScriptableRenderContext renderContext, CommandBuffer cmd)
		{
			using (new ProfilingScope(cmd, ProfilingSampler.Get<HDProfileId>(HDProfileId.ForwardEmissive)))
			{
				bool flag = hdCamera.frameSettings.IsEnabled(FrameSettingsField.MSAA);
				CoreUtils.SetRenderTarget(cmd, flag ? this.m_CameraColorMSAABuffer : this.m_CameraColorBuffer, this.m_SharedRTManager.GetDepthStencilBuffer(flag), 0, CubemapFace.Unknown, -1);
				RendererListDesc rendererListDesc = this.PrepareForwardEmissiveRendererList(cullResults, hdCamera);
				HDUtils.DrawRendererList(renderContext, cmd, RendererList.Create(in rendererListDesc));
				if (hdCamera.frameSettings.IsEnabled(FrameSettingsField.Decals))
				{
					DecalSystem.instance.RenderForwardEmissive(cmd);
				}
			}
		}

		// Token: 0x060003FF RID: 1023 RVA: 0x00024430 File Offset: 0x00022630
		private void RenderWireFrame(CullingResults cull, HDCamera hdCamera, RenderTargetIdentifier backbuffer, ScriptableRenderContext renderContext, CommandBuffer cmd)
		{
			using (new ProfilingScope(cmd, ProfilingSampler.Get<HDProfileId>(HDProfileId.RenderWireFrame)))
			{
				CoreUtils.SetRenderTarget(cmd, backbuffer, ClearFlag.Color, this.GetColorBufferClearColor(hdCamera), 0, CubemapFace.Unknown, -1);
				RendererListDesc rendererListDesc = HDRenderPipeline.CreateOpaqueRendererListDesc(cull, hdCamera.camera, this.m_AllForwardOpaquePassNames, PerObjectData.None, null, null, null, false);
				RendererList rendererList = RendererList.Create(in rendererListDesc);
				FrameSettings frameSettings = hdCamera.frameSettings;
				HDRenderPipeline.DrawOpaqueRendererList(in renderContext, cmd, in frameSettings, rendererList);
				rendererListDesc = HDRenderPipeline.CreateTransparentRendererListDesc(cull, hdCamera.camera, this.m_AllTransparentPassNames, PerObjectData.None, null, null, null, false);
				RendererList rendererList2 = RendererList.Create(in rendererListDesc);
				frameSettings = hdCamera.frameSettings;
				HDRenderPipeline.DrawTransparentRendererList(in renderContext, cmd, in frameSettings, rendererList2);
			}
		}

		// Token: 0x06000400 RID: 1024 RVA: 0x0002450C File Offset: 0x0002270C
		private void RenderDebugViewMaterial(CullingResults cull, HDCamera hdCamera, ScriptableRenderContext renderContext, CommandBuffer cmd)
		{
			using (new ProfilingScope(cmd, ProfilingSampler.Get<HDProfileId>(HDProfileId.DisplayDebugViewMaterial)))
			{
				FrameSettings frameSettings;
				if (this.m_CurrentDebugDisplaySettings.data.materialDebugSettings.IsDebugGBufferEnabled())
				{
					frameSettings = hdCamera.frameSettings;
					if (frameSettings.litShaderMode == LitShaderMode.Deferred)
					{
						using (new ProfilingScope(cmd, ProfilingSampler.Get<HDProfileId>(HDProfileId.DebugViewMaterialGBuffer)))
						{
							HDUtils.DrawFullScreen(cmd, this.m_currentDebugViewMaterialGBuffer, this.m_CameraColorBuffer, null, 0);
							return;
						}
					}
				}
				CoreUtils.SetRenderTarget(cmd, this.m_CameraColorBuffer, this.m_SharedRTManager.GetDepthStencilBuffer(false), ClearFlag.All, Color.clear, 0, CubemapFace.Unknown, -1);
				RendererListDesc rendererListDesc = HDRenderPipeline.CreateOpaqueRendererListDesc(cull, hdCamera.camera, this.m_AllForwardOpaquePassNames, this.m_CurrentRendererConfigurationBakedLighting, null, new RenderStateBlock?(this.m_DepthStateOpaque), null, false);
				RendererList rendererList = RendererList.Create(in rendererListDesc);
				frameSettings = hdCamera.frameSettings;
				HDRenderPipeline.DrawOpaqueRendererList(in renderContext, cmd, in frameSettings, rendererList);
				rendererListDesc = HDRenderPipeline.CreateTransparentRendererListDesc(cull, hdCamera.camera, this.m_AllTransparentPassNames, this.m_CurrentRendererConfigurationBakedLighting, null, new RenderStateBlock?(this.m_DepthStateOpaque), null, false);
				RendererList rendererList2 = RendererList.Create(in rendererListDesc);
				frameSettings = hdCamera.frameSettings;
				HDRenderPipeline.DrawTransparentRendererList(in renderContext, cmd, in frameSettings, rendererList2);
			}
		}

		// Token: 0x06000401 RID: 1025 RVA: 0x00024688 File Offset: 0x00022888
		private void RenderTransparencyOverdraw(CullingResults cull, HDCamera hdCamera, ScriptableRenderContext renderContext, CommandBuffer cmd)
		{
			if (this.m_CurrentDebugDisplaySettings.IsDebugDisplayEnabled() && this.m_CurrentDebugDisplaySettings.data.fullScreenDebugMode == FullScreenDebugMode.TransparencyOverdraw)
			{
				CoreUtils.SetRenderTarget(cmd, this.m_CameraColorBuffer, this.m_SharedRTManager.GetDepthStencilBuffer(false), ClearFlag.Color, Color.black, 0, CubemapFace.Unknown, -1);
				RenderStateBlock renderStateBlock = new RenderStateBlock
				{
					mask = RenderStateMask.Blend,
					blendState = new BlendState
					{
						blendState0 = new RenderTargetBlendState
						{
							destinationColorBlendMode = BlendMode.One,
							sourceColorBlendMode = BlendMode.One,
							destinationAlphaBlendMode = BlendMode.One,
							sourceAlphaBlendMode = BlendMode.One,
							colorBlendOperation = BlendOp.Add,
							alphaBlendOperation = BlendOp.Add,
							writeMask = ColorWriteMask.All
						}
					}
				};
				cmd.SetGlobalFloat(HDShaderIDs._DebugTransparencyOverdrawWeight, 1f);
				ShaderTagId[] array = (this.m_Asset.currentPlatformRenderPipelineSettings.supportTransparentBackface ? this.m_AllTransparentPassNames : this.m_TransparentNoBackfaceNames);
				this.m_DebugFullScreenPropertyBlock.SetFloat(HDShaderIDs._TransparencyOverdrawMaxPixelCost, this.m_DebugDisplaySettings.data.transparencyDebugSettings.maxPixelCost);
				RendererListDesc rendererListDesc = HDRenderPipeline.CreateTransparentRendererListDesc(cull, hdCamera.camera, array, PerObjectData.None, null, new RenderStateBlock?(renderStateBlock), null, false);
				RendererList rendererList = RendererList.Create(in rendererListDesc);
				FrameSettings frameSettings = hdCamera.frameSettings;
				HDRenderPipeline.DrawTransparentRendererList(in renderContext, cmd, in frameSettings, rendererList);
				rendererListDesc = HDRenderPipeline.CreateTransparentRendererListDesc(cull, hdCamera.camera, array, PerObjectData.None, new RenderQueueRange?(HDRenderQueue.k_RenderQueue_AfterPostProcessTransparent), new RenderStateBlock?(renderStateBlock), null, false);
				rendererList = RendererList.Create(in rendererListDesc);
				frameSettings = hdCamera.frameSettings;
				HDRenderPipeline.DrawTransparentRendererList(in renderContext, cmd, in frameSettings, rendererList);
				cmd.SetGlobalFloat(HDShaderIDs._DebugTransparencyOverdrawWeight, 0.25f);
				rendererListDesc = HDRenderPipeline.CreateTransparentRendererListDesc(cull, hdCamera.camera, array, PerObjectData.None, new RenderQueueRange?(HDRenderQueue.k_RenderQueue_LowTransparent), new RenderStateBlock?(renderStateBlock), null, false);
				rendererList = RendererList.Create(in rendererListDesc);
				frameSettings = hdCamera.frameSettings;
				HDRenderPipeline.DrawTransparentRendererList(in renderContext, cmd, in frameSettings, rendererList);
				this.PushFullScreenDebugTexture(hdCamera, cmd, this.m_CameraColorBuffer, FullScreenDebugMode.TransparencyOverdraw);
			}
		}

		// Token: 0x06000402 RID: 1026 RVA: 0x0002487F File Offset: 0x00022A7F
		private void UpdateSkyEnvironment(HDCamera hdCamera, ScriptableRenderContext renderContext, int frameIndex, CommandBuffer cmd)
		{
			this.m_SkyManager.UpdateEnvironment(hdCamera, renderContext, this.GetCurrentSunLight(), frameIndex, cmd);
		}

		// Token: 0x06000403 RID: 1027 RVA: 0x00024897 File Offset: 0x00022A97
		public void RequestSkyEnvironmentUpdate()
		{
			this.m_SkyManager.RequestEnvironmentUpdate();
		}

		// Token: 0x06000404 RID: 1028 RVA: 0x000248A4 File Offset: 0x00022AA4
		private void PreRenderSky(HDCamera hdCamera, CommandBuffer cmd)
		{
			if (this.m_CurrentDebugDisplaySettings.IsMatcapViewEnabled(hdCamera))
			{
				return;
			}
			bool flag = hdCamera.frameSettings.IsEnabled(FrameSettingsField.MSAA);
			RTHandle rthandle = (flag ? this.m_CameraColorMSAABuffer : this.m_CameraColorBuffer);
			RTHandle depthStencilBuffer = this.m_SharedRTManager.GetDepthStencilBuffer(flag);
			RTHandle normalBuffer = this.m_SharedRTManager.GetNormalBuffer(flag);
			hdCamera.volumeStack.GetComponent<VisualEnvironment>();
			this.m_SkyManager.PreRenderSky(hdCamera, this.GetCurrentSunLight(), rthandle, normalBuffer, depthStencilBuffer, this.m_CurrentDebugDisplaySettings, this.m_FrameCount, cmd);
		}

		// Token: 0x06000405 RID: 1029 RVA: 0x0002492C File Offset: 0x00022B2C
		private void RenderSky(HDCamera hdCamera, CommandBuffer cmd)
		{
			if (this.m_CurrentDebugDisplaySettings.IsMatcapViewEnabled(hdCamera))
			{
				return;
			}
			bool flag = hdCamera.frameSettings.IsEnabled(FrameSettingsField.MSAA);
			RTHandle rthandle = (flag ? this.m_CameraColorMSAABuffer : this.m_CameraColorBuffer);
			RTHandle rthandle2 = (flag ? this.m_OpaqueAtmosphericScatteringMSAABuffer : this.m_OpaqueAtmosphericScatteringBuffer);
			RTHandle depthStencilBuffer = this.m_SharedRTManager.GetDepthStencilBuffer(flag);
			hdCamera.volumeStack.GetComponent<VisualEnvironment>();
			this.m_SkyManager.RenderSky(hdCamera, this.GetCurrentSunLight(), rthandle, depthStencilBuffer, this.m_CurrentDebugDisplaySettings, this.m_FrameCount, cmd);
			if (Fog.IsFogEnabled(hdCamera) || Fog.IsPBRFogEnabled(hdCamera))
			{
				Matrix4x4 pixelCoordToViewDirWS = hdCamera.mainViewConstants.pixelCoordToViewDirWS;
				this.m_SkyManager.RenderOpaqueAtmosphericScattering(cmd, hdCamera, rthandle, this.m_LightingBufferHandle, rthandle2, depthStencilBuffer, pixelCoordToViewDirWS, hdCamera.frameSettings.IsEnabled(FrameSettingsField.MSAA));
			}
		}

		// Token: 0x06000406 RID: 1030 RVA: 0x000249FE File Offset: 0x00022BFE
		public Texture2D ExportSkyToTexture(Camera camera)
		{
			return this.m_SkyManager.ExportSkyToTexture(camera);
		}

		// Token: 0x06000407 RID: 1031 RVA: 0x00024A0C File Offset: 0x00022C0C
		private RendererListDesc PrepareForwardOpaqueRendererList(CullingResults cullResults, HDCamera hdCamera)
		{
			ShaderTagId[] array = ((hdCamera.frameSettings.litShaderMode == LitShaderMode.Forward) ? this.m_ForwardAndForwardOnlyPassNames : this.m_ForwardOnlyPassNames);
			return HDRenderPipeline.CreateOpaqueRendererListDesc(cullResults, hdCamera.camera, array, this.m_CurrentRendererConfigurationBakedLighting, null, null, null, false);
		}

		// Token: 0x06000408 RID: 1032 RVA: 0x00024A60 File Offset: 0x00022C60
		private void RenderForwardOpaque(CullingResults cullResults, HDCamera hdCamera, ScriptableRenderContext renderContext, CommandBuffer cmd)
		{
			using (new ProfilingScope(cmd, this.m_CurrentDebugDisplaySettings.IsDebugDisplayEnabled() ? ProfilingSampler.Get<HDProfileId>(HDProfileId.ForwardOpaqueDebug) : ProfilingSampler.Get<HDProfileId>(HDProfileId.ForwardOpaque)))
			{
				bool flag = hdCamera.frameSettings.IsEnabled(FrameSettingsField.FPTLForForwardOpaque);
				bool flag2 = hdCamera.frameSettings.IsEnabled(FrameSettingsField.MSAA);
				RenderTargetIdentifier[] mrtwithSSS;
				if (hdCamera.frameSettings.IsEnabled(FrameSettingsField.SubsurfaceScattering))
				{
					mrtwithSSS = this.m_MRTWithSSS;
					mrtwithSSS[0] = (flag2 ? this.m_CameraColorMSAABuffer : this.m_CameraColorBuffer);
					mrtwithSSS[1] = (flag2 ? this.m_CameraSssDiffuseLightingMSAABuffer : this.m_CameraSssDiffuseLightingBuffer);
					mrtwithSSS[2] = (flag2 ? this.GetSSSBufferMSAA() : this.GetSSSBuffer());
				}
				else
				{
					mrtwithSSS = this.mMRTSingle;
					mrtwithSSS[0] = (flag2 ? this.m_CameraColorMSAABuffer : this.m_CameraColorBuffer);
				}
				FrameSettings frameSettings = hdCamera.frameSettings;
				RendererListDesc rendererListDesc = this.PrepareForwardOpaqueRendererList(cullResults, hdCamera);
				HDRenderPipeline.RenderForwardRendererList(frameSettings, RendererList.Create(in rendererListDesc), mrtwithSSS, this.m_SharedRTManager.GetDepthStencilBuffer(flag2), flag ? this.m_TileAndClusterData.lightList : this.m_TileAndClusterData.perVoxelLightLists, true, renderContext, cmd);
			}
		}

		// Token: 0x06000409 RID: 1033 RVA: 0x00024BD0 File Offset: 0x00022DD0
		private static bool NeedMotionVectorForTransparent(FrameSettings frameSettings)
		{
			return frameSettings.IsEnabled(FrameSettingsField.MotionVectors) && frameSettings.IsEnabled(FrameSettingsField.TransparentsWriteMotionVector);
		}

		// Token: 0x0600040A RID: 1034 RVA: 0x00024BE8 File Offset: 0x00022DE8
		private RendererListDesc PrepareForwardTransparentRendererList(CullingResults cullResults, HDCamera hdCamera, bool preRefraction)
		{
			RenderQueueRange renderQueueRange;
			if (preRefraction)
			{
				renderQueueRange = HDRenderQueue.k_RenderQueue_PreRefraction;
			}
			else if (hdCamera.frameSettings.IsEnabled(FrameSettingsField.LowResTransparent))
			{
				renderQueueRange = HDRenderQueue.k_RenderQueue_Transparent;
			}
			else
			{
				renderQueueRange = HDRenderQueue.k_RenderQueue_TransparentWithLowRes;
			}
			if (!hdCamera.frameSettings.IsEnabled(FrameSettingsField.Refraction))
			{
				if (hdCamera.frameSettings.IsEnabled(FrameSettingsField.LowResTransparent))
				{
					renderQueueRange = HDRenderQueue.k_RenderQueue_AllTransparent;
				}
				else
				{
					renderQueueRange = HDRenderQueue.k_RenderQueue_AllTransparentWithLowRes;
				}
			}
			if (HDRenderPipeline.NeedMotionVectorForTransparent(hdCamera.frameSettings))
			{
				this.m_CurrentRendererConfigurationBakedLighting |= PerObjectData.MotionVectors;
			}
			ShaderTagId[] array = (this.m_Asset.currentPlatformRenderPipelineSettings.supportTransparentBackface ? this.m_AllTransparentPassNames : this.m_TransparentNoBackfaceNames);
			return HDRenderPipeline.CreateTransparentRendererListDesc(cullResults, hdCamera.camera, array, this.m_CurrentRendererConfigurationBakedLighting, new RenderQueueRange?(renderQueueRange), null, null, false);
		}

		// Token: 0x0600040B RID: 1035 RVA: 0x00024CB4 File Offset: 0x00022EB4
		private void RenderForwardTransparent(CullingResults cullResults, HDCamera hdCamera, bool preRefraction, ScriptableRenderContext renderContext, CommandBuffer cmd)
		{
			if (!hdCamera.frameSettings.IsEnabled(FrameSettingsField.Refraction) && preRefraction)
			{
				return;
			}
			HDProfileId hdprofileId;
			if (this.m_CurrentDebugDisplaySettings.IsDebugDisplayEnabled())
			{
				hdprofileId = (preRefraction ? HDProfileId.ForwardPreRefractionDebug : HDProfileId.ForwardTransparentDebug);
			}
			else
			{
				hdprofileId = (preRefraction ? HDProfileId.ForwardPreRefraction : HDProfileId.ForwardTransparent);
			}
			using (new ProfilingScope(cmd, ProfilingSampler.Get<HDProfileId>(hdprofileId)))
			{
				bool flag = hdCamera.frameSettings.IsEnabled(FrameSettingsField.MSAA);
				bool flag2 = HDRenderPipeline.NeedMotionVectorForTransparent(hdCamera.frameSettings);
				cmd.SetGlobalInt(HDShaderIDs._ColorMaskTransparentVel, flag2 ? 15 : 0);
				this.m_MRTTransparentMotionVec[0] = (flag ? this.m_CameraColorMSAABuffer : this.m_CameraColorBuffer);
				this.m_MRTTransparentMotionVec[1] = (flag2 ? this.m_SharedRTManager.GetMotionVectorsBuffer(hdCamera.frameSettings.IsEnabled(FrameSettingsField.MSAA)) : this.m_SharedRTManager.GetNormalBuffer(flag));
				if (hdCamera.frameSettings.IsEnabled(FrameSettingsField.Decals) && DecalSystem.m_DecalDatasCount > 0)
				{
					DecalSystem.instance.SetAtlas(cmd);
				}
				FrameSettings frameSettings = hdCamera.frameSettings;
				RendererListDesc rendererListDesc = this.PrepareForwardTransparentRendererList(cullResults, hdCamera, preRefraction);
				HDRenderPipeline.RenderForwardRendererList(frameSettings, RendererList.Create(in rendererListDesc), this.m_MRTTransparentMotionVec, this.m_SharedRTManager.GetDepthStencilBuffer(hdCamera.frameSettings.IsEnabled(FrameSettingsField.MSAA)), this.m_TileAndClusterData.perVoxelLightLists, false, renderContext, cmd);
			}
		}

		// Token: 0x0600040C RID: 1036 RVA: 0x00024E40 File Offset: 0x00023040
		private static void RenderForwardRendererList(FrameSettings frameSettings, RendererList rendererList, RenderTargetIdentifier[] renderTarget, RTHandle depthBuffer, ComputeBuffer lightListBuffer, bool opaque, ScriptableRenderContext renderContext, CommandBuffer cmd)
		{
			bool flag = opaque && frameSettings.IsEnabled(FrameSettingsField.FPTLForForwardOpaque);
			CoreUtils.SetKeyword(cmd, "USE_FPTL_LIGHTLIST", flag);
			CoreUtils.SetKeyword(cmd, "USE_CLUSTERED_LIGHTLIST", !flag);
			cmd.SetGlobalBuffer(HDShaderIDs.g_vLightListGlobal, lightListBuffer);
			CoreUtils.SetRenderTarget(cmd, renderTarget, depthBuffer);
			if (opaque)
			{
				HDRenderPipeline.DrawOpaqueRendererList(in renderContext, cmd, in frameSettings, rendererList);
				return;
			}
			HDRenderPipeline.DrawTransparentRendererList(in renderContext, cmd, in frameSettings, rendererList);
		}

		// Token: 0x0600040D RID: 1037 RVA: 0x00024EB0 File Offset: 0x000230B0
		[Conditional("DEVELOPMENT_BUILD")]
		[Conditional("UNITY_EDITOR")]
		private void RenderForwardError(CullingResults cullResults, HDCamera hdCamera, ScriptableRenderContext renderContext, CommandBuffer cmd)
		{
			using (new ProfilingScope(cmd, ProfilingSampler.Get<HDProfileId>(HDProfileId.RenderForwardError)))
			{
				CoreUtils.SetRenderTarget(cmd, this.m_CameraColorBuffer, this.m_SharedRTManager.GetDepthStencilBuffer(false), 0, CubemapFace.Unknown, -1);
				RendererListDesc rendererListDesc = HDRenderPipeline.CreateOpaqueRendererListDesc(cullResults, hdCamera.camera, this.m_ForwardErrorPassNames, PerObjectData.None, new RenderQueueRange?(RenderQueueRange.all), null, this.m_ErrorMaterial, false);
				RendererList rendererList = RendererList.Create(in rendererListDesc);
				HDUtils.DrawRendererList(renderContext, cmd, rendererList);
			}
		}

		// Token: 0x0600040E RID: 1038 RVA: 0x00024F48 File Offset: 0x00023148
		private bool RenderCustomPass(ScriptableRenderContext context, CommandBuffer cmd, HDCamera hdCamera, CullingResults cullingResults, CustomPassInjectionPoint injectionPoint)
		{
			if (!hdCamera.frameSettings.IsEnabled(FrameSettingsField.CustomPass))
			{
				return false;
			}
			CustomPassVolume activePassVolume = CustomPassVolume.GetActivePassVolume(injectionPoint);
			if (activePassVolume == null)
			{
				return false;
			}
			CustomPass.RenderTargets renderTargets = new CustomPass.RenderTargets
			{
				cameraColorMSAABuffer = this.m_CameraColorMSAABuffer,
				cameraColorBuffer = ((injectionPoint == CustomPassInjectionPoint.AfterPostProcess) ? this.m_IntermediateAfterPostProcessBuffer : this.m_CameraColorBuffer),
				customColorBuffer = this.m_CustomPassColorBuffer,
				customDepthBuffer = this.m_CustomPassDepthBuffer
			};
			return activePassVolume.Execute(context, cmd, hdCamera, cullingResults, this.m_SharedRTManager, renderTargets);
		}

		// Token: 0x0600040F RID: 1039 RVA: 0x00024FD8 File Offset: 0x000231D8
		private bool WillCustomPassBeExecuted(HDCamera hdCamera, CustomPassInjectionPoint injectionPoint)
		{
			if (!hdCamera.frameSettings.IsEnabled(FrameSettingsField.CustomPass))
			{
				return false;
			}
			CustomPassVolume activePassVolume = CustomPassVolume.GetActivePassVolume(injectionPoint);
			return !(activePassVolume == null) && activePassVolume.WillExecuteInjectionPoint(hdCamera);
		}

		// Token: 0x06000410 RID: 1040 RVA: 0x00025014 File Offset: 0x00023214
		private void RenderTransparentDepthPrepass(CullingResults cull, HDCamera hdCamera, ScriptableRenderContext renderContext, CommandBuffer cmd)
		{
			FrameSettings frameSettings = hdCamera.frameSettings;
			if (frameSettings.IsEnabled(FrameSettingsField.TransparentPrepass))
			{
				using (new ProfilingScope(cmd, ProfilingSampler.Get<HDProfileId>(HDProfileId.TransparentDepthPrepass)))
				{
					CoreUtils.SetRenderTarget(cmd, this.m_SharedRTManager.GetDepthStencilBuffer(false), ClearFlag.None, 0, CubemapFace.Unknown, -1);
					RendererListDesc rendererListDesc = HDRenderPipeline.CreateTransparentRendererListDesc(cull, hdCamera.camera, this.m_TransparentDepthPrepassNames, PerObjectData.None, null, null, null, false);
					RendererList rendererList = RendererList.Create(in rendererListDesc);
					frameSettings = hdCamera.frameSettings;
					HDRenderPipeline.DrawTransparentRendererList(in renderContext, cmd, in frameSettings, rendererList);
				}
			}
		}

		// Token: 0x06000411 RID: 1041 RVA: 0x000250C0 File Offset: 0x000232C0
		private void RenderTransparentDepthPostpass(CullingResults cullResults, HDCamera hdCamera, ScriptableRenderContext renderContext, CommandBuffer cmd)
		{
			FrameSettings frameSettings = hdCamera.frameSettings;
			if (!frameSettings.IsEnabled(FrameSettingsField.TransparentPostpass))
			{
				return;
			}
			using (new ProfilingScope(cmd, ProfilingSampler.Get<HDProfileId>(HDProfileId.TransparentDepthPostpass)))
			{
				CoreUtils.SetRenderTarget(cmd, this.m_SharedRTManager.GetDepthStencilBuffer(false), ClearFlag.None, 0, CubemapFace.Unknown, -1);
				RendererListDesc rendererListDesc = HDRenderPipeline.CreateTransparentRendererListDesc(cullResults, hdCamera.camera, this.m_TransparentDepthPostpassNames, PerObjectData.None, null, null, null, false);
				RendererList rendererList = RendererList.Create(in rendererListDesc);
				frameSettings = hdCamera.frameSettings;
				HDRenderPipeline.DrawTransparentRendererList(in renderContext, cmd, in frameSettings, rendererList);
				frameSettings = hdCamera.frameSettings;
				if (frameSettings.IsEnabled(FrameSettingsField.RayTracing) && hdCamera.volumeStack.GetComponent<RecursiveRendering>().enable.value)
				{
					rendererListDesc = HDRenderPipeline.CreateTransparentRendererListDesc(cullResults, hdCamera.camera, this.m_TransparentDepthPostpassNames, PerObjectData.None, new RenderQueueRange?(HDRenderQueue.k_RenderQueue_AllTransparentRaytracing), null, null, false);
					RendererList rendererList2 = RendererList.Create(in rendererListDesc);
					frameSettings = hdCamera.frameSettings;
					HDRenderPipeline.DrawTransparentRendererList(in renderContext, cmd, in frameSettings, rendererList2);
				}
			}
		}

		// Token: 0x06000412 RID: 1042 RVA: 0x000251E0 File Offset: 0x000233E0
		private void RenderLowResTransparent(CullingResults cullResults, HDCamera hdCamera, ScriptableRenderContext renderContext, CommandBuffer cmd)
		{
			FrameSettings frameSettings = hdCamera.frameSettings;
			if (!frameSettings.IsEnabled(FrameSettingsField.LowResTransparent))
			{
				return;
			}
			using (new ProfilingScope(cmd, ProfilingSampler.Get<HDProfileId>(HDProfileId.LowResTransparent)))
			{
				cmd.SetGlobalInt(HDShaderIDs._OffScreenRendering, 1);
				cmd.SetGlobalInt(HDShaderIDs._OffScreenDownsampleFactor, 2);
				CoreUtils.SetRenderTarget(cmd, this.m_LowResTransparentBuffer, this.m_SharedRTManager.GetLowResDepthBuffer(), ClearFlag.Color, Color.black, 0, CubemapFace.Unknown, -1);
				RenderQueueRange k_RenderQueue_LowTransparent = HDRenderQueue.k_RenderQueue_LowTransparent;
				ShaderTagId[] array = (this.m_Asset.currentPlatformRenderPipelineSettings.supportTransparentBackface ? this.m_AllTransparentPassNames : this.m_TransparentNoBackfaceNames);
				RendererListDesc rendererListDesc = HDRenderPipeline.CreateTransparentRendererListDesc(cullResults, hdCamera.camera, array, this.m_CurrentRendererConfigurationBakedLighting, new RenderQueueRange?(HDRenderQueue.k_RenderQueue_LowTransparent), null, null, false);
				RendererList rendererList = RendererList.Create(in rendererListDesc);
				frameSettings = hdCamera.frameSettings;
				HDRenderPipeline.DrawTransparentRendererList(in renderContext, cmd, in frameSettings, rendererList);
				cmd.SetGlobalInt(HDShaderIDs._OffScreenRendering, 0);
				cmd.SetGlobalInt(HDShaderIDs._OffScreenDownsampleFactor, 1);
			}
		}

		// Token: 0x06000413 RID: 1043 RVA: 0x000252F8 File Offset: 0x000234F8
		private void RenderObjectsMotionVectors(CullingResults cullResults, HDCamera hdCamera, ScriptableRenderContext renderContext, CommandBuffer cmd)
		{
			FrameSettings frameSettings = hdCamera.frameSettings;
			if (!frameSettings.IsEnabled(FrameSettingsField.ObjectMotionVectors))
			{
				return;
			}
			using (new ProfilingScope(cmd, ProfilingSampler.Get<HDProfileId>(HDProfileId.ObjectsMotionVector)))
			{
				hdCamera.camera.depthTextureMode |= DepthTextureMode.Depth | DepthTextureMode.MotionVectors;
				RenderTargetIdentifier[] motionVectorsPassBuffersRTI = this.m_SharedRTManager.GetMotionVectorsPassBuffersRTI(hdCamera.frameSettings);
				SharedRTManager sharedRTManager = this.m_SharedRTManager;
				frameSettings = hdCamera.frameSettings;
				CoreUtils.SetRenderTarget(cmd, motionVectorsPassBuffersRTI, sharedRTManager.GetDepthStencilBuffer(frameSettings.IsEnabled(FrameSettingsField.MSAA)));
				RendererListDesc rendererListDesc = HDRenderPipeline.CreateOpaqueRendererListDesc(cullResults, hdCamera.camera, HDShaderPassNames.s_MotionVectorsName, PerObjectData.MotionVectors, null, null, null, false);
				RendererList rendererList = RendererList.Create(in rendererListDesc);
				frameSettings = hdCamera.frameSettings;
				HDRenderPipeline.DrawOpaqueRendererList(in renderContext, cmd, in frameSettings, rendererList);
			}
		}

		// Token: 0x06000414 RID: 1044 RVA: 0x000253D4 File Offset: 0x000235D4
		private void RenderCameraMotionVectors(CullingResults cullResults, HDCamera hdCamera, ScriptableRenderContext renderContext, CommandBuffer cmd)
		{
			if (!hdCamera.frameSettings.IsEnabled(FrameSettingsField.MotionVectors))
			{
				return;
			}
			using (new ProfilingScope(cmd, ProfilingSampler.Get<HDProfileId>(HDProfileId.CameraMotionVectors)))
			{
				bool flag = hdCamera.frameSettings.IsEnabled(FrameSettingsField.MSAA);
				hdCamera.camera.depthTextureMode |= DepthTextureMode.Depth | DepthTextureMode.MotionVectors;
				this.m_CameraMotionVectorsMaterial.SetInt(HDShaderIDs._StencilMask, 32);
				this.m_CameraMotionVectorsMaterial.SetInt(HDShaderIDs._StencilRef, 32);
				HDUtils.DrawFullScreen(cmd, this.m_CameraMotionVectorsMaterial, this.m_SharedRTManager.GetMotionVectorsBuffer(flag), this.m_SharedRTManager.GetDepthStencilBuffer(flag), null, 0);
			}
		}

		// Token: 0x06000415 RID: 1045 RVA: 0x00025494 File Offset: 0x00023694
		private HDRenderPipeline.RenderSSRParameters PrepareSSRParameters(HDCamera hdCamera)
		{
			ScreenSpaceReflection component = hdCamera.volumeStack.GetComponent<ScreenSpaceReflection>();
			HDRenderPipeline.RenderSSRParameters renderSSRParameters = default(HDRenderPipeline.RenderSSRParameters);
			renderSSRParameters.ssrCS = this.m_ScreenSpaceReflectionsCS;
			renderSSRParameters.tracingKernel = this.m_SsrTracingKernel;
			renderSSRParameters.reprojectionKernel = this.m_SsrReprojectionKernel;
			renderSSRParameters.width = hdCamera.actualWidth;
			renderSSRParameters.height = hdCamera.actualHeight;
			renderSSRParameters.viewCount = hdCamera.viewCount;
			float nearClipPlane = hdCamera.camera.nearClipPlane;
			float farClipPlane = hdCamera.camera.farClipPlane;
			renderSSRParameters.maxIteration = component.rayMaxIterations;
			renderSSRParameters.reflectSky = component.reflectSky.value;
			float value = component.depthBufferThickness.value;
			renderSSRParameters.thicknessScale = 1f / (1f + value);
			renderSSRParameters.thicknessBias = -nearClipPlane / (farClipPlane - nearClipPlane) * (value * renderSSRParameters.thicknessScale);
			HDUtils.PackedMipChainInfo depthBufferMipChainInfo = this.m_SharedRTManager.GetDepthBufferMipChainInfo();
			renderSSRParameters.depthPyramidMipCount = depthBufferMipChainInfo.mipLevelCount;
			renderSSRParameters.offsetBufferData = depthBufferMipChainInfo.GetOffsetBufferData(this.m_DepthPyramidMipLevelOffsetsBuffer);
			renderSSRParameters.coarseStencilBuffer = this.m_SharedRTManager.GetCoarseStencilBuffer();
			float num = 1f - component.smoothnessFadeStart.value;
			renderSSRParameters.roughnessFadeEnd = 1f - component.minSmoothness.value;
			float num2 = renderSSRParameters.roughnessFadeEnd - num;
			renderSSRParameters.roughnessFadeEndTimesRcpLength = ((num2 != 0f) ? (renderSSRParameters.roughnessFadeEnd * (1f / num2)) : 1f);
			renderSSRParameters.roughnessFadeRcpLength = ((num2 != 0f) ? (1f / num2) : 0f);
			renderSSRParameters.edgeFadeRcpLength = Mathf.Min(1f / component.screenFadeDistance.value, float.MaxValue);
			renderSSRParameters.colorPyramidUVScaleAndLimit = HDUtils.ComputeUvScaleAndLimit(hdCamera.historyRTHandleProperties.previousViewportSize, hdCamera.historyRTHandleProperties.previousRenderTargetSize);
			renderSSRParameters.colorPyramidMipCount = hdCamera.colorPyramidHistoryMipCount;
			return renderSSRParameters;
		}

		// Token: 0x06000416 RID: 1046 RVA: 0x00025684 File Offset: 0x00023884
		private static void RenderSSR(in HDRenderPipeline.RenderSSRParameters parameters, RTHandle depthPyramid, RTHandle SsrHitPointTexture, RTHandle stencilBuffer, RTHandle clearCoatMask, RTHandle previousColorPyramid, RTHandle ssrLightingTexture, CommandBuffer cmd, ScriptableRenderContext renderContext)
		{
			ComputeShader ssrCS = parameters.ssrCS;
			using (new ProfilingScope(cmd, ProfilingSampler.Get<HDProfileId>(HDProfileId.SsrTracing)))
			{
				cmd.SetComputeIntParam(ssrCS, HDShaderIDs._SsrIterLimit, parameters.maxIteration);
				cmd.SetComputeFloatParam(ssrCS, HDShaderIDs._SsrThicknessScale, parameters.thicknessScale);
				cmd.SetComputeFloatParam(ssrCS, HDShaderIDs._SsrThicknessBias, parameters.thicknessBias);
				cmd.SetComputeFloatParam(ssrCS, HDShaderIDs._SsrRoughnessFadeEnd, parameters.roughnessFadeEnd);
				cmd.SetComputeFloatParam(ssrCS, HDShaderIDs._SsrRoughnessFadeRcpLength, parameters.roughnessFadeRcpLength);
				cmd.SetComputeFloatParam(ssrCS, HDShaderIDs._SsrRoughnessFadeEndTimesRcpLength, parameters.roughnessFadeEndTimesRcpLength);
				cmd.SetComputeIntParam(ssrCS, HDShaderIDs._SsrDepthPyramidMaxMip, parameters.depthPyramidMipCount - 1);
				cmd.SetComputeFloatParam(ssrCS, HDShaderIDs._SsrEdgeFadeRcpLength, parameters.edgeFadeRcpLength);
				cmd.SetComputeIntParam(ssrCS, HDShaderIDs._SsrReflectsSky, parameters.reflectSky ? 1 : 0);
				cmd.SetComputeIntParam(ssrCS, HDShaderIDs._SsrStencilBit, 8);
				cmd.SetComputeTextureParam(ssrCS, parameters.tracingKernel, HDShaderIDs._CameraDepthTexture, depthPyramid);
				cmd.SetComputeTextureParam(ssrCS, parameters.tracingKernel, HDShaderIDs._SsrClearCoatMaskTexture, clearCoatMask);
				cmd.SetComputeTextureParam(ssrCS, parameters.tracingKernel, HDShaderIDs._SsrHitPointTexture, SsrHitPointTexture);
				if (stencilBuffer.rt.stencilFormat == GraphicsFormat.None)
				{
					cmd.SetComputeTextureParam(ssrCS, parameters.tracingKernel, HDShaderIDs._StencilTexture, stencilBuffer);
				}
				else
				{
					cmd.SetComputeTextureParam(ssrCS, parameters.tracingKernel, HDShaderIDs._StencilTexture, stencilBuffer, 0, RenderTextureSubElement.Stencil);
				}
				cmd.SetComputeBufferParam(ssrCS, parameters.tracingKernel, HDShaderIDs._CoarseStencilBuffer, parameters.coarseStencilBuffer);
				cmd.SetComputeBufferParam(ssrCS, parameters.tracingKernel, HDShaderIDs._DepthPyramidMipLevelOffsets, parameters.offsetBufferData);
				cmd.DispatchCompute(ssrCS, parameters.tracingKernel, HDUtils.DivRoundUp(parameters.width, 8), HDUtils.DivRoundUp(parameters.height, 8), parameters.viewCount);
			}
			using (new ProfilingScope(cmd, ProfilingSampler.Get<HDProfileId>(HDProfileId.SsrReprojection)))
			{
				cmd.SetComputeTextureParam(ssrCS, parameters.reprojectionKernel, HDShaderIDs._SsrHitPointTexture, SsrHitPointTexture);
				cmd.SetComputeTextureParam(ssrCS, parameters.reprojectionKernel, HDShaderIDs._SsrLightingTextureRW, ssrLightingTexture);
				cmd.SetComputeTextureParam(ssrCS, parameters.reprojectionKernel, HDShaderIDs._ColorPyramidTexture, previousColorPyramid);
				cmd.SetComputeTextureParam(ssrCS, parameters.reprojectionKernel, HDShaderIDs._SsrClearCoatMaskTexture, clearCoatMask);
				cmd.SetComputeVectorParam(ssrCS, HDShaderIDs._ColorPyramidUvScaleAndLimitPrevFrame, parameters.colorPyramidUVScaleAndLimit);
				cmd.SetComputeIntParam(ssrCS, HDShaderIDs._SsrColorPyramidMaxMip, parameters.colorPyramidMipCount - 1);
				cmd.DispatchCompute(ssrCS, parameters.reprojectionKernel, HDUtils.DivRoundUp(parameters.width, 8), HDUtils.DivRoundUp(parameters.height, 8), parameters.viewCount);
			}
		}

		// Token: 0x06000417 RID: 1047 RVA: 0x00025978 File Offset: 0x00023B78
		private void RenderSSR(HDCamera hdCamera, CommandBuffer cmd, ScriptableRenderContext renderContext)
		{
			if (!hdCamera.frameSettings.IsEnabled(FrameSettingsField.SSR))
			{
				return;
			}
			ScreenSpaceReflection component = hdCamera.volumeStack.GetComponent<ScreenSpaceReflection>();
			bool flag = hdCamera.frameSettings.IsEnabled(FrameSettingsField.RayTracing) && component.rayTracing.value;
			if (flag)
			{
				hdCamera.xr.StartSinglePass(cmd, hdCamera.camera, renderContext);
				this.RenderRayTracedReflections(hdCamera, cmd, this.m_SsrLightingTexture, renderContext, this.m_FrameCount);
				hdCamera.xr.StopSinglePass(cmd, hdCamera.camera, renderContext);
			}
			else
			{
				RTHandle previousFrameRT = hdCamera.GetPreviousFrameRT(0);
				RTHandle rthandle = ((hdCamera.frameSettings.litShaderMode == LitShaderMode.Deferred) ? this.m_GbufferManager.GetBuffer(2) : TextureXR.GetBlackTexture());
				HDRenderPipeline.RenderSSRParameters renderSSRParameters = this.PrepareSSRParameters(hdCamera);
				HDRenderPipeline.RenderSSR(in renderSSRParameters, this.m_SharedRTManager.GetDepthTexture(false), this.m_SsrHitPointTexture, this.m_SharedRTManager.GetStencilBuffer(hdCamera.frameSettings.IsEnabled(FrameSettingsField.MSAA)), rthandle, previousFrameRT, this.m_SsrLightingTexture, cmd, renderContext);
				if (!hdCamera.colorPyramidHistoryIsValid)
				{
					cmd.SetGlobalTexture(HDShaderIDs._SsrLightingTexture, TextureXR.GetClearTexture());
					hdCamera.colorPyramidHistoryIsValid = true;
				}
			}
			cmd.SetGlobalInt(HDShaderIDs._UseRayTracedReflections, flag ? 1 : 0);
			this.PushFullScreenDebugTexture(hdCamera, cmd, this.m_SsrLightingTexture, FullScreenDebugMode.ScreenSpaceReflections);
		}

		// Token: 0x06000418 RID: 1048 RVA: 0x00025AC4 File Offset: 0x00023CC4
		private void RenderColorPyramid(HDCamera hdCamera, CommandBuffer cmd, bool isPreRefraction)
		{
			if (isPreRefraction)
			{
				if (!hdCamera.frameSettings.IsEnabled(FrameSettingsField.Refraction))
				{
					return;
				}
			}
			else if (!hdCamera.frameSettings.IsEnabled(FrameSettingsField.Distortion) && !hdCamera.frameSettings.IsEnabled(FrameSettingsField.SSR))
			{
				return;
			}
			RTHandle currentFrameRT = hdCamera.GetCurrentFrameRT(0);
			int num;
			using (new ProfilingScope(cmd, ProfilingSampler.Get<HDProfileId>(HDProfileId.ColorPyramid)))
			{
				Vector2Int vector2Int = new Vector2Int(hdCamera.actualWidth, hdCamera.actualHeight);
				num = this.m_MipGenerator.RenderColorGaussianPyramid(cmd, vector2Int, this.m_CameraColorBuffer, currentFrameRT);
				hdCamera.colorPyramidHistoryMipCount = num;
			}
			float num2 = (float)hdCamera.actualWidth / (float)currentFrameRT.rt.width;
			float num3 = (float)hdCamera.actualHeight / (float)currentFrameRT.rt.height;
			Vector4 vector = new Vector4(num2, num3, (float)num, 0f);
			Vector4 vector2 = new Vector4(num2, num3, 0f, 0f);
			cmd.SetGlobalTexture(HDShaderIDs._ColorPyramidTexture, currentFrameRT);
			cmd.SetGlobalVector(HDShaderIDs._ColorPyramidScale, vector);
			this.PushFullScreenDebugTextureMip(hdCamera, cmd, currentFrameRT, num, vector2, isPreRefraction ? FullScreenDebugMode.PreRefractionColorPyramid : FullScreenDebugMode.FinalColorPyramid);
		}

		// Token: 0x06000419 RID: 1049 RVA: 0x00025C00 File Offset: 0x00023E00
		private void GenerateDepthPyramid(HDCamera hdCamera, CommandBuffer cmd, FullScreenDebugMode debugMode)
		{
			this.CopyDepthBufferIfNeeded(hdCamera, cmd);
			int mipLevelCount = this.m_SharedRTManager.GetDepthBufferMipChainInfo().mipLevelCount;
			using (new ProfilingScope(cmd, ProfilingSampler.Get<HDProfileId>(HDProfileId.DepthPyramid)))
			{
				this.m_MipGenerator.RenderMinDepthPyramid(cmd, this.m_SharedRTManager.GetDepthTexture(false), this.m_SharedRTManager.GetDepthBufferMipChainInfo());
			}
			float num = (float)hdCamera.actualWidth / (float)this.m_SharedRTManager.GetDepthTexture(false).rt.width;
			float num2 = (float)hdCamera.actualHeight / (float)this.m_SharedRTManager.GetDepthTexture(false).rt.height;
			Vector4 vector = new Vector4(num, num2, (float)mipLevelCount, 0f);
			Vector4 vector2 = new Vector4(num, num2, 0f, 0f);
			cmd.SetGlobalTexture(HDShaderIDs._CameraDepthTexture, this.m_SharedRTManager.GetDepthTexture(false));
			cmd.SetGlobalVector(HDShaderIDs._DepthPyramidScale, vector);
			this.PushFullScreenDebugTextureMip(hdCamera, cmd, this.m_SharedRTManager.GetDepthTexture(false), mipLevelCount, vector2, debugMode);
		}

		// Token: 0x0600041A RID: 1050 RVA: 0x00025D20 File Offset: 0x00023F20
		private void DownsampleDepthForLowResTransparency(HDCamera hdCamera, CommandBuffer cmd)
		{
			GlobalLowResolutionTransparencySettings lowresTransparentSettings = this.m_Asset.currentPlatformRenderPipelineSettings.lowresTransparentSettings;
			if (!hdCamera.frameSettings.IsEnabled(FrameSettingsField.LowResTransparent))
			{
				return;
			}
			using (new ProfilingScope(cmd, ProfilingSampler.Get<HDProfileId>(HDProfileId.DownsampleDepth)))
			{
				CoreUtils.SetRenderTarget(cmd, this.m_SharedRTManager.GetLowResDepthBuffer(), ClearFlag.None, 0, CubemapFace.Unknown, -1);
				cmd.SetViewport(new Rect(0f, 0f, (float)hdCamera.actualWidth * 0.5f, (float)hdCamera.actualHeight * 0.5f));
				if (lowresTransparentSettings.checkerboardDepthBuffer)
				{
					this.m_DownsampleDepthMaterial.EnableKeyword("CHECKERBOARD_DOWNSAMPLE");
				}
				cmd.DrawProcedural(Matrix4x4.identity, this.m_DownsampleDepthMaterial, 0, MeshTopology.Triangles, 3, 1, null);
			}
		}

		// Token: 0x0600041B RID: 1051 RVA: 0x00025DF8 File Offset: 0x00023FF8
		private void UpsampleTransparent(HDCamera hdCamera, CommandBuffer cmd)
		{
			GlobalLowResolutionTransparencySettings lowresTransparentSettings = this.m_Asset.currentPlatformRenderPipelineSettings.lowresTransparentSettings;
			if (!hdCamera.frameSettings.IsEnabled(FrameSettingsField.LowResTransparent))
			{
				return;
			}
			using (new ProfilingScope(cmd, ProfilingSampler.Get<HDProfileId>(HDProfileId.UpsampleLowResTransparent)))
			{
				CoreUtils.SetRenderTarget(cmd, this.m_CameraColorBuffer, ClearFlag.None, 0, CubemapFace.Unknown, -1);
				if (lowresTransparentSettings.upsampleType == LowResTransparentUpsample.Bilinear)
				{
					this.m_UpsampleTransparency.EnableKeyword("BILINEAR");
				}
				else if (lowresTransparentSettings.upsampleType == LowResTransparentUpsample.NearestDepth)
				{
					this.m_UpsampleTransparency.EnableKeyword("NEAREST_DEPTH");
				}
				this.m_UpsampleTransparency.SetTexture(HDShaderIDs._LowResTransparent, this.m_LowResTransparentBuffer);
				this.m_UpsampleTransparency.SetTexture(HDShaderIDs._LowResDepthTexture, this.m_SharedRTManager.GetLowResDepthBuffer());
				cmd.DrawProcedural(Matrix4x4.identity, this.m_UpsampleTransparency, 0, MeshTopology.Triangles, 3, 1, null);
			}
		}

		// Token: 0x0600041C RID: 1052 RVA: 0x00025EF0 File Offset: 0x000240F0
		private void ApplyDebugDisplaySettings(HDCamera hdCamera, CommandBuffer cmd)
		{
			bool flag = this.m_CurrentDebugDisplaySettings.IsDebugDisplayEnabled() || CoreUtils.IsSceneLightingDisabled(hdCamera.camera);
			CoreUtils.SetKeyword(cmd, "DEBUG_DISPLAY", flag);
			cmd.SetGlobalTexture(HDShaderIDs._DebugMatCapTexture, this.defaultResources.textures.matcapTex);
			if (flag || this.m_CurrentDebugDisplaySettings.data.colorPickerDebugSettings.colorPickerMode != ColorPickerDebugMode.None)
			{
				this.m_CurrentDebugDisplaySettings.UpdateMaterials();
				LightingDebugSettings lightingDebugSettings = this.m_CurrentDebugDisplaySettings.data.lightingDebugSettings;
				MaterialDebugSettings materialDebugSettings = this.m_CurrentDebugDisplaySettings.data.materialDebugSettings;
				Vector4 vector = new Vector4(lightingDebugSettings.overrideAlbedo ? 1f : 0f, lightingDebugSettings.overrideAlbedoValue.r, lightingDebugSettings.overrideAlbedoValue.g, lightingDebugSettings.overrideAlbedoValue.b);
				Vector4 vector2 = new Vector4(lightingDebugSettings.overrideSmoothness ? 1f : 0f, lightingDebugSettings.overrideSmoothnessValue, 0f, 0f);
				Vector4 vector3 = new Vector4(lightingDebugSettings.overrideNormal ? 1f : 0f, 0f, 0f, 0f);
				Vector4 vector4 = new Vector4(lightingDebugSettings.overrideAmbientOcclusion ? 1f : 0f, lightingDebugSettings.overrideAmbientOcclusionValue, 0f, 0f);
				Vector4 vector5 = new Vector4(lightingDebugSettings.overrideSpecularColor ? 1f : 0f, lightingDebugSettings.overrideSpecularColorValue.r, lightingDebugSettings.overrideSpecularColorValue.g, lightingDebugSettings.overrideSpecularColorValue.b);
				Vector4 vector6 = new Vector4(lightingDebugSettings.overrideEmissiveColor ? 1f : 0f, lightingDebugSettings.overrideEmissiveColorValue.r, lightingDebugSettings.overrideEmissiveColorValue.g, lightingDebugSettings.overrideEmissiveColorValue.b);
				Vector4 vector7 = new Vector4(materialDebugSettings.materialValidateTrueMetal ? 1f : 0f, materialDebugSettings.materialValidateTrueMetalColor.r, materialDebugSettings.materialValidateTrueMetalColor.g, materialDebugSettings.materialValidateTrueMetalColor.b);
				DebugLightingMode debugLightingMode = this.m_CurrentDebugDisplaySettings.GetDebugLightingMode();
				if (CoreUtils.IsSceneLightingDisabled(hdCamera.camera))
				{
					debugLightingMode = DebugLightingMode.MatcapView;
				}
				cmd.SetGlobalFloatArray(HDShaderIDs._DebugViewMaterial, this.m_CurrentDebugDisplaySettings.GetDebugMaterialIndexes());
				cmd.SetGlobalInt(HDShaderIDs._DebugLightingMode, (int)debugLightingMode);
				cmd.SetGlobalInt(HDShaderIDs._DebugShadowMapMode, (int)this.m_CurrentDebugDisplaySettings.GetDebugShadowMapMode());
				cmd.SetGlobalInt(HDShaderIDs._DebugMipMapMode, (int)this.m_CurrentDebugDisplaySettings.GetDebugMipMapMode());
				cmd.SetGlobalInt(HDShaderIDs._DebugMipMapModeTerrainTexture, (int)this.m_CurrentDebugDisplaySettings.GetDebugMipMapModeTerrainTexture());
				cmd.SetGlobalInt(HDShaderIDs._ColorPickerMode, (int)this.m_CurrentDebugDisplaySettings.GetDebugColorPickerMode());
				cmd.SetGlobalInt(HDShaderIDs._DebugFullScreenMode, (int)this.m_CurrentDebugDisplaySettings.data.fullScreenDebugMode);
				cmd.SetGlobalInt(HDShaderIDs._MatcapMixAlbedo, 0);
				cmd.SetGlobalFloat(HDShaderIDs._MatcapViewScale, 1f);
				cmd.SetGlobalVector(HDShaderIDs._DebugLightingAlbedo, vector);
				cmd.SetGlobalVector(HDShaderIDs._DebugLightingSmoothness, vector2);
				cmd.SetGlobalVector(HDShaderIDs._DebugLightingNormal, vector3);
				cmd.SetGlobalVector(HDShaderIDs._DebugLightingAmbientOcclusion, vector4);
				cmd.SetGlobalVector(HDShaderIDs._DebugLightingSpecularColor, vector5);
				cmd.SetGlobalVector(HDShaderIDs._DebugLightingEmissiveColor, vector6);
				cmd.SetGlobalColor(HDShaderIDs._DebugLightingMaterialValidateHighColor, materialDebugSettings.materialValidateHighColor);
				cmd.SetGlobalColor(HDShaderIDs._DebugLightingMaterialValidateLowColor, materialDebugSettings.materialValidateLowColor);
				cmd.SetGlobalColor(HDShaderIDs._DebugLightingMaterialValidatePureMetalColor, vector7);
				cmd.SetGlobalVector(HDShaderIDs._MousePixelCoord, HDUtils.GetMouseCoordinates(hdCamera));
				cmd.SetGlobalVector(HDShaderIDs._MouseClickPixelCoord, HDUtils.GetMouseClickCoordinates(hdCamera));
				cmd.SetGlobalTexture(HDShaderIDs._DebugFont, this.defaultResources.textures.debugFontTex);
				cmd.SetGlobalFloat(HDShaderIDs._DebugExposure, this.m_CurrentDebugDisplaySettings.DebugNeedsExposure() ? lightingDebugSettings.debugExposure : 0f);
			}
		}

		// Token: 0x0600041D RID: 1053 RVA: 0x000262B6 File Offset: 0x000244B6
		private static bool NeedColorPickerDebug(DebugDisplaySettings debugSettings)
		{
			return debugSettings.data.colorPickerDebugSettings.colorPickerMode != ColorPickerDebugMode.None || debugSettings.data.falseColorDebugSettings.falseColor || debugSettings.data.lightingDebugSettings.debugLightingMode == DebugLightingMode.LuminanceMeter;
		}

		// Token: 0x0600041E RID: 1054 RVA: 0x000262F4 File Offset: 0x000244F4
		private void PushColorPickerDebugTexture(CommandBuffer cmd, HDCamera hdCamera, RTHandle textureID)
		{
			if (HDRenderPipeline.NeedColorPickerDebug(this.m_CurrentDebugDisplaySettings))
			{
				using (new ProfilingScope(cmd, ProfilingSampler.Get<HDProfileId>(HDProfileId.PushToColorPicker)))
				{
					HDUtils.BlitCameraTexture(cmd, textureID, this.m_DebugColorPickerBuffer, 0f, false);
				}
			}
		}

		// Token: 0x0600041F RID: 1055 RVA: 0x00026350 File Offset: 0x00024550
		private bool NeedsFullScreenDebugMode()
		{
			bool flag = this.m_CurrentDebugDisplaySettings.data.fullScreenDebugMode > FullScreenDebugMode.None;
			bool flag2 = this.m_CurrentDebugDisplaySettings.data.lightingDebugSettings.shadowDebugMode == ShadowMapDebugMode.SingleShadow;
			return flag || flag2;
		}

		// Token: 0x06000420 RID: 1056 RVA: 0x0002638B File Offset: 0x0002458B
		private void PushFullScreenLightingDebugTexture(HDCamera hdCamera, CommandBuffer cmd, RTHandle textureID)
		{
			if (this.NeedsFullScreenDebugMode() && !this.m_FullScreenDebugPushed)
			{
				this.m_FullScreenDebugPushed = true;
				HDUtils.BlitCameraTexture(cmd, textureID, this.m_DebugFullScreenTempBuffer, 0f, false);
			}
		}

		// Token: 0x06000421 RID: 1057 RVA: 0x000263B7 File Offset: 0x000245B7
		internal void PushFullScreenDebugTexture(HDCamera hdCamera, CommandBuffer cmd, RTHandle textureID, FullScreenDebugMode debugMode)
		{
			if (debugMode == this.m_CurrentDebugDisplaySettings.data.fullScreenDebugMode)
			{
				this.m_FullScreenDebugPushed = true;
				HDUtils.BlitCameraTexture(cmd, textureID, this.m_DebugFullScreenTempBuffer, 0f, false);
			}
		}

		// Token: 0x06000422 RID: 1058 RVA: 0x000263E8 File Offset: 0x000245E8
		private void PushFullScreenDebugTextureMip(HDCamera hdCamera, CommandBuffer cmd, RTHandle texture, int lodCount, Vector4 scaleBias, FullScreenDebugMode debugMode)
		{
			if (debugMode == this.m_CurrentDebugDisplaySettings.data.fullScreenDebugMode)
			{
				int num = Mathf.FloorToInt(this.m_CurrentDebugDisplaySettings.data.fullscreenDebugMip * (float)lodCount);
				this.m_FullScreenDebugPushed = true;
				HDUtils.BlitCameraTexture(cmd, texture, this.m_DebugFullScreenTempBuffer, scaleBias, (float)num, false);
			}
		}

		// Token: 0x06000423 RID: 1059 RVA: 0x0002643C File Offset: 0x0002463C
		private HDRenderPipeline.DebugParameters PrepareDebugParameters(HDCamera hdCamera, HDUtils.PackedMipChainInfo depthMipInfo)
		{
			HDRenderPipeline.DebugParameters debugParameters = default(HDRenderPipeline.DebugParameters);
			debugParameters.debugDisplaySettings = this.m_CurrentDebugDisplaySettings;
			debugParameters.hdCamera = hdCamera;
			debugParameters.resolveFullScreenDebug = this.NeedsFullScreenDebugMode() && this.m_FullScreenDebugPushed;
			debugParameters.debugFullScreenMaterial = this.m_DebugFullScreen;
			debugParameters.depthPyramidMip = (int)(debugParameters.debugDisplaySettings.data.fullscreenDebugMip * (float)depthMipInfo.mipLevelCount);
			debugParameters.depthPyramidOffsets = depthMipInfo.GetOffsetBufferData(this.m_DepthPyramidMipLevelOffsetsBuffer);
			debugParameters.skyReflectionTexture = this.m_SkyManager.GetSkyReflection(hdCamera);
			debugParameters.debugLatlongMaterial = this.m_DebugDisplayLatlong;
			debugParameters.lightingOverlayParameters = this.PrepareLightLoopDebugOverlayParameters();
			debugParameters.rayTracingSupported = hdCamera.frameSettings.IsEnabled(FrameSettingsField.RayTracing);
			debugParameters.rayCountManager = this.m_RayCountManager;
			debugParameters.colorPickerEnabled = HDRenderPipeline.NeedColorPickerDebug(debugParameters.debugDisplaySettings);
			debugParameters.colorPickerMaterial = this.m_DebugColorPicker;
			return debugParameters;
		}

		// Token: 0x06000424 RID: 1060 RVA: 0x00026530 File Offset: 0x00024730
		private static void ResolveFullScreenDebug(in HDRenderPipeline.DebugParameters parameters, MaterialPropertyBlock mpb, RTHandle inputFullScreenDebug, RTHandle inputDepthPyramid, RTHandle output, CommandBuffer cmd)
		{
			mpb.SetTexture(HDShaderIDs._DebugFullScreenTexture, inputFullScreenDebug);
			mpb.SetTexture(HDShaderIDs._CameraDepthTexture, inputDepthPyramid);
			mpb.SetFloat(HDShaderIDs._FullScreenDebugMode, (float)parameters.debugDisplaySettings.data.fullScreenDebugMode);
			mpb.SetInt(HDShaderIDs._DebugDepthPyramidMip, parameters.depthPyramidMip);
			mpb.SetBuffer(HDShaderIDs._DebugDepthPyramidOffsets, parameters.depthPyramidOffsets);
			mpb.SetInt(HDShaderIDs._DebugContactShadowLightIndex, parameters.debugDisplaySettings.data.fullScreenContactShadowLightIndex);
			HDUtils.DrawFullScreen(cmd, parameters.debugFullScreenMaterial, output, mpb, 0);
		}

		// Token: 0x06000425 RID: 1061 RVA: 0x000265CC File Offset: 0x000247CC
		private static void ResolveColorPickerDebug(in HDRenderPipeline.DebugParameters parameters, RTHandle debugColorPickerBuffer, RTHandle output, CommandBuffer cmd)
		{
			ColorPickerDebugSettings colorPickerDebugSettings = parameters.debugDisplaySettings.data.colorPickerDebugSettings;
			FalseColorDebugSettings falseColorDebugSettings = parameters.debugDisplaySettings.data.falseColorDebugSettings;
			Vector4 vector = new Vector4(falseColorDebugSettings.colorThreshold0, falseColorDebugSettings.colorThreshold1, falseColorDebugSettings.colorThreshold2, falseColorDebugSettings.colorThreshold3);
			parameters.colorPickerMaterial.SetTexture(HDShaderIDs._DebugColorPickerTexture, debugColorPickerBuffer);
			parameters.colorPickerMaterial.SetColor(HDShaderIDs._ColorPickerFontColor, colorPickerDebugSettings.fontColor);
			parameters.colorPickerMaterial.SetInt(HDShaderIDs._FalseColorEnabled, falseColorDebugSettings.falseColor ? 1 : 0);
			parameters.colorPickerMaterial.SetVector(HDShaderIDs._FalseColorThresholds, vector);
			parameters.colorPickerMaterial.SetFloat(HDShaderIDs._ApplyLinearToSRGB, parameters.debugDisplaySettings.IsDebugMaterialDisplayEnabled() ? 1f : 0f);
			HDUtils.DrawFullScreen(cmd, parameters.colorPickerMaterial, output, null, 0);
		}

		// Token: 0x06000426 RID: 1062 RVA: 0x000266AC File Offset: 0x000248AC
		private static void RenderSkyReflectionOverlay(in HDRenderPipeline.DebugParameters debugParameters, CommandBuffer cmd, MaterialPropertyBlock mpb, ref float x, ref float y, float overlaySize)
		{
			LightingDebugSettings lightingDebugSettings = debugParameters.debugDisplaySettings.data.lightingDebugSettings;
			if (lightingDebugSettings.displaySkyReflection)
			{
				mpb.SetTexture(HDShaderIDs._InputCubemap, debugParameters.skyReflectionTexture);
				mpb.SetFloat(HDShaderIDs._Mipmap, lightingDebugSettings.skyReflectionMipmap);
				mpb.SetFloat(HDShaderIDs._DebugExposure, lightingDebugSettings.debugExposure);
				mpb.SetFloat(HDShaderIDs._SliceIndex, lightingDebugSettings.cookieCubeArraySliceIndex);
				cmd.SetViewport(new Rect(x, y, overlaySize, overlaySize));
				cmd.DrawProcedural(Matrix4x4.identity, debugParameters.debugLatlongMaterial, 0, MeshTopology.Triangles, 3, 1, mpb);
				HDUtils.NextOverlayCoord(ref x, ref y, overlaySize, overlaySize, debugParameters.hdCamera);
			}
		}

		// Token: 0x06000427 RID: 1063 RVA: 0x00026757 File Offset: 0x00024957
		private static void RenderRayCountOverlay(in HDRenderPipeline.DebugParameters debugParameters, CommandBuffer cmd, ref float x, ref float y, float overlaySize)
		{
			if (debugParameters.rayTracingSupported)
			{
				debugParameters.rayCountManager.EvaluateRayCount(cmd, debugParameters.hdCamera);
			}
		}

		// Token: 0x06000428 RID: 1064 RVA: 0x00026774 File Offset: 0x00024974
		private void RenderDebug(HDCamera hdCamera, CommandBuffer cmd, CullingResults cullResults)
		{
			if (hdCamera.camera.cameraType == CameraType.Reflection || hdCamera.camera.cameraType == CameraType.Preview)
			{
				return;
			}
			CoreUtils.SetRenderTarget(cmd, this.m_IntermediateAfterPostProcessBuffer, this.m_SharedRTManager.GetDepthStencilBuffer(false), 0, CubemapFace.Unknown, -1);
			HDRenderPipeline.DebugParameters debugParameters = this.PrepareDebugParameters(hdCamera, this.m_SharedRTManager.GetDepthBufferMipChainInfo());
			using (new ProfilingScope(cmd, ProfilingSampler.Get<HDProfileId>(HDProfileId.RenderDebug)))
			{
				if (debugParameters.resolveFullScreenDebug)
				{
					this.m_FullScreenDebugPushed = false;
					HDRenderPipeline.ResolveFullScreenDebug(in debugParameters, this.m_DebugFullScreenPropertyBlock, this.m_DebugFullScreenTempBuffer, this.m_SharedRTManager.GetDepthTexture(false), this.m_IntermediateAfterPostProcessBuffer, cmd);
					this.PushColorPickerDebugTexture(cmd, hdCamera, this.m_IntermediateAfterPostProcessBuffer);
				}
				if (debugParameters.colorPickerEnabled)
				{
					HDRenderPipeline.ResolveColorPickerDebug(in debugParameters, this.m_DebugColorPickerBuffer, this.m_IntermediateAfterPostProcessBuffer, cmd);
				}
				LightingDebugSettings lightingDebugSettings = debugParameters.debugDisplaySettings.data.lightingDebugSettings;
				if (lightingDebugSettings.displayLightVolumes)
				{
					HDRenderPipeline.s_lightVolumes.RenderLightVolumes(cmd, hdCamera, cullResults, lightingDebugSettings, this.m_IntermediateAfterPostProcessBuffer);
				}
				HDUtils.ResetOverlay();
				float runtimeDebugPanelWidth = HDUtils.GetRuntimeDebugPanelWidth(debugParameters.hdCamera);
				float num = 0f;
				float debugOverlayRatio = debugParameters.debugDisplaySettings.data.debugOverlayRatio;
				float num2 = Math.Min((float)debugParameters.hdCamera.actualHeight, (float)debugParameters.hdCamera.actualWidth - runtimeDebugPanelWidth) * debugOverlayRatio;
				float num3 = (float)debugParameters.hdCamera.actualHeight - num2;
				num += runtimeDebugPanelWidth;
				HDRenderPipeline.RenderSkyReflectionOverlay(in debugParameters, cmd, this.m_SharedPropertyBlock, ref num, ref num3, num2);
				HDRenderPipeline.RenderRayCountOverlay(in debugParameters, cmd, ref num, ref num3, num2);
				HDRenderPipeline.RenderLightLoopDebugOverlay(in debugParameters, cmd, ref num, ref num3, num2, this.m_SharedRTManager.GetDepthTexture(false));
				HDShadowManager.ShadowDebugAtlasTextures debugAtlasTextures = debugParameters.lightingOverlayParameters.shadowManager.GetDebugAtlasTextures();
				HDRenderPipeline.RenderShadowsDebugOverlay(in debugParameters, in debugAtlasTextures, cmd, ref num, ref num3, num2, this.m_SharedPropertyBlock);
				DecalSystem.instance.RenderDebugOverlay(debugParameters.hdCamera, cmd, debugParameters.debugDisplaySettings, ref num, ref num3, num2, (float)debugParameters.hdCamera.actualWidth);
			}
		}

		// Token: 0x06000429 RID: 1065 RVA: 0x00026988 File Offset: 0x00024B88
		private void ClearStencilBuffer(HDCamera hdCamera, CommandBuffer cmd)
		{
			using (new ProfilingScope(cmd, ProfilingSampler.Get<HDProfileId>(HDProfileId.ClearStencil)))
			{
				this.m_ClearStencilBufferMaterial.SetInt(HDShaderIDs._StencilMask, 63);
				HDUtils.DrawFullScreen(cmd, this.m_ClearStencilBufferMaterial, this.m_CameraColorBuffer, this.m_SharedRTManager.GetDepthStencilBuffer(false), null, 0);
			}
		}

		// Token: 0x0600042A RID: 1066 RVA: 0x000269F8 File Offset: 0x00024BF8
		private void ClearBuffers(HDCamera hdCamera, CommandBuffer cmd)
		{
			bool flag = hdCamera.frameSettings.IsEnabled(FrameSettingsField.MSAA);
			using (new ProfilingScope(cmd, ProfilingSampler.Get<HDProfileId>(HDProfileId.ClearBuffers)))
			{
				using (new ProfilingScope(cmd, ProfilingSampler.Get<HDProfileId>(HDProfileId.ClearDepthStencil)))
				{
					if (hdCamera.clearDepth)
					{
						CoreUtils.SetRenderTarget(cmd, flag ? this.m_CameraColorMSAABuffer : this.m_CameraColorBuffer, this.m_SharedRTManager.GetDepthStencilBuffer(flag), ClearFlag.Depth, 0, CubemapFace.Unknown, -1);
						if (hdCamera.frameSettings.IsEnabled(FrameSettingsField.MSAA))
						{
							CoreUtils.SetRenderTarget(cmd, this.m_SharedRTManager.GetDepthTexture(true), this.m_SharedRTManager.GetDepthStencilBuffer(true), ClearFlag.Color, Color.black, 0, CubemapFace.Unknown, -1);
						}
					}
					this.m_IsDepthBufferCopyValid = false;
				}
				using (new ProfilingScope(cmd, ProfilingSampler.Get<HDProfileId>(HDProfileId.ClearHDRTarget)))
				{
					if (hdCamera.clearColorMode == HDAdditionalCameraData.ClearColorMode.Color || this.m_CurrentDebugDisplaySettings.data.lightingDebugSettings.debugLightingMode == DebugLightingMode.LuxMeter || this.m_CurrentDebugDisplaySettings.IsMatcapViewEnabled(hdCamera) || (hdCamera.clearColorMode == HDAdditionalCameraData.ClearColorMode.Sky && !this.m_SkyManager.IsVisualSkyValid(hdCamera)) || HDUtils.IsRegularPreviewCamera(hdCamera.camera))
					{
						CoreUtils.SetRenderTarget(cmd, flag ? this.m_CameraColorMSAABuffer : this.m_CameraColorBuffer, this.m_SharedRTManager.GetDepthStencilBuffer(flag), ClearFlag.Color, this.GetColorBufferClearColor(hdCamera), 0, CubemapFace.Unknown, -1);
					}
				}
				if (hdCamera.frameSettings.IsEnabled(FrameSettingsField.SubsurfaceScattering))
				{
					using (new ProfilingScope(cmd, ProfilingSampler.Get<HDProfileId>(HDProfileId.ClearSssLightingBuffer)))
					{
						CoreUtils.SetRenderTarget(cmd, flag ? this.m_CameraSssDiffuseLightingMSAABuffer : this.m_CameraSssDiffuseLightingBuffer, ClearFlag.Color, Color.clear, 0, CubemapFace.Unknown, -1);
					}
				}
				if (hdCamera.frameSettings.IsEnabled(FrameSettingsField.SSR))
				{
					using (new ProfilingScope(cmd, ProfilingSampler.Get<HDProfileId>(HDProfileId.ClearSsrBuffers)))
					{
						CoreUtils.SetRenderTarget(cmd, this.m_SsrHitPointTexture, ClearFlag.Color, Color.clear, 0, CubemapFace.Unknown, -1);
						CoreUtils.SetRenderTarget(cmd, this.m_SsrLightingTexture, ClearFlag.Color, Color.clear, 0, CubemapFace.Unknown, -1);
					}
				}
				if (hdCamera.frameSettings.litShaderMode == LitShaderMode.Deferred)
				{
					using (new ProfilingScope(cmd, ProfilingSampler.Get<HDProfileId>(HDProfileId.ClearGBuffer)))
					{
						if (this.m_CurrentDebugDisplaySettings.IsDebugDisplayEnabled() || hdCamera.frameSettings.IsEnabled(FrameSettingsField.ClearGBuffers))
						{
							if (Application.platform == RuntimePlatform.PS4)
							{
								foreach (RenderTargetIdentifier renderTargetIdentifier in this.m_GbufferManager.GetBuffersRTI())
								{
									CoreUtils.SetRenderTarget(cmd, renderTargetIdentifier, this.m_SharedRTManager.GetDepthStencilBuffer(false), ClearFlag.Color, Color.clear, 0, CubemapFace.Unknown, -1);
								}
							}
							else
							{
								CoreUtils.SetRenderTarget(cmd, this.m_GbufferManager.GetBuffersRTI(), this.m_SharedRTManager.GetDepthStencilBuffer(false), ClearFlag.Color, Color.clear);
							}
						}
						if (hdCamera.frameSettings.IsEnabled(FrameSettingsField.SSR))
						{
							CoreUtils.SetRenderTarget(cmd, this.m_GbufferManager.GetBuffer(2), this.m_SharedRTManager.GetDepthStencilBuffer(false), ClearFlag.Color, Color.clear, 0, CubemapFace.Unknown, -1);
						}
					}
				}
			}
		}

		// Token: 0x0600042B RID: 1067 RVA: 0x00026DA4 File Offset: 0x00024FA4
		private void RenderPostProcess(CullingResults cullResults, HDCamera hdCamera, RenderTargetIdentifier finalRT, ScriptableRenderContext renderContext, CommandBuffer cmd, bool isFinalPass)
		{
			bool flag = HDUtils.PostProcessIsFinalPass() && isFinalPass && (hdCamera.flipYMode == HDAdditionalCameraData.FlipYMode.ForceFlipY || hdCamera.isMainGameView);
			RenderTargetIdentifier renderTargetIdentifier = ((HDUtils.PostProcessIsFinalPass() && isFinalPass) ? finalRT : this.m_IntermediateAfterPostProcessBuffer);
			this.RenderAfterPostProcess(cullResults, hdCamera, renderContext, cmd);
			cmd.SetGlobalTexture(HDShaderIDs._CameraDepthTexture, this.m_SharedRTManager.GetDepthStencilBuffer(false));
			this.m_PostProcessSystem.Render(cmd, hdCamera, this.m_BlueNoise, this.m_CameraColorBuffer, this.GetAfterPostProcessOffScreenBuffer(), null, renderTargetIdentifier, this.m_SharedRTManager.GetDepthStencilBuffer(false), flag);
		}

		// Token: 0x0600042C RID: 1068 RVA: 0x00026E42 File Offset: 0x00025042
		private RTHandle GetAfterPostProcessOffScreenBuffer()
		{
			if (this.currentPlatformRenderPipelineSettings.supportedLitShaderMode == RenderPipelineSettings.SupportedLitShaderMode.ForwardOnly)
			{
				return this.GetSSSBuffer();
			}
			return this.m_GbufferManager.GetBuffer(0);
		}

		// Token: 0x0600042D RID: 1069 RVA: 0x00026E68 File Offset: 0x00025068
		private void RenderAfterPostProcess(CullingResults cullResults, HDCamera hdCamera, ScriptableRenderContext renderContext, CommandBuffer cmd)
		{
			FrameSettings frameSettings = hdCamera.frameSettings;
			if (!frameSettings.IsEnabled(FrameSettingsField.AfterPostprocess))
			{
				return;
			}
			using (new ProfilingScope(cmd, ProfilingSampler.Get<HDProfileId>(HDProfileId.AfterPostProcessing)))
			{
				bool flag = hdCamera.IsTAAEnabled();
				hdCamera.UpdateAllViewConstants(false);
				hdCamera.SetupGlobalParams(cmd, this.m_FrameCount);
				if (!flag)
				{
					frameSettings = hdCamera.frameSettings;
					if (frameSettings.IsEnabled(FrameSettingsField.ZTestAfterPostProcessTAA))
					{
						CoreUtils.SetRenderTarget(cmd, this.GetAfterPostProcessOffScreenBuffer(), this.m_SharedRTManager.GetDepthStencilBuffer(false), ClearFlag.Color, Color.black, 0, CubemapFace.Unknown, -1);
						goto IL_008C;
					}
				}
				CoreUtils.SetRenderTarget(cmd, this.GetAfterPostProcessOffScreenBuffer(), ClearFlag.Color, Color.black, 0, CubemapFace.Unknown, -1);
				IL_008C:
				cmd.SetGlobalInt(HDShaderIDs._OffScreenRendering, 1);
				RendererListDesc rendererListDesc = HDRenderPipeline.CreateOpaqueRendererListDesc(cullResults, hdCamera.camera, HDShaderPassNames.s_ForwardOnlyName, PerObjectData.None, new RenderQueueRange?(HDRenderQueue.k_RenderQueue_AfterPostProcessOpaque), null, null, false);
				RendererList rendererList = RendererList.Create(in rendererListDesc);
				frameSettings = hdCamera.frameSettings;
				HDRenderPipeline.DrawOpaqueRendererList(in renderContext, cmd, in frameSettings, rendererList);
				rendererListDesc = HDRenderPipeline.CreateTransparentRendererListDesc(cullResults, hdCamera.camera, HDShaderPassNames.s_ForwardOnlyName, PerObjectData.None, new RenderQueueRange?(HDRenderQueue.k_RenderQueue_AfterPostProcessTransparent), null, null, false);
				RendererList rendererList2 = RendererList.Create(in rendererListDesc);
				frameSettings = hdCamera.frameSettings;
				HDRenderPipeline.DrawTransparentRendererList(in renderContext, cmd, in frameSettings, rendererList2);
				cmd.SetGlobalInt(HDShaderIDs._OffScreenRendering, 0);
			}
		}

		// Token: 0x0600042E RID: 1070 RVA: 0x00026FD4 File Offset: 0x000251D4
		private void SendGeometryGraphicsBuffers(CommandBuffer cmd, HDCamera hdCamera)
		{
			bool flag = false;
			Texture texture = null;
			bool flag2 = false;
			Texture texture2 = null;
			HDAdditionalCameraData hdadditionalCameraData = null;
			hdCamera.camera.TryGetComponent<HDAdditionalCameraData>(out hdadditionalCameraData);
			HDAdditionalCameraData.BufferAccessType bufferAccessType = (HDAdditionalCameraData.BufferAccessType)0;
			if (hdadditionalCameraData != null)
			{
				bufferAccessType = hdadditionalCameraData.GetBufferAccess();
			}
			VFXCameraBufferTypes vfxcameraBufferTypes = VFXManager.IsCameraBufferNeeded(hdCamera.camera);
			flag |= (vfxcameraBufferTypes & VFXCameraBufferTypes.Normal) != VFXCameraBufferTypes.None || (bufferAccessType & HDAdditionalCameraData.BufferAccessType.Normal) > (HDAdditionalCameraData.BufferAccessType)0;
			flag2 |= (vfxcameraBufferTypes & VFXCameraBufferTypes.Depth) != VFXCameraBufferTypes.None || (bufferAccessType & HDAdditionalCameraData.BufferAccessType.Depth) > (HDAdditionalCameraData.BufferAccessType)0;
			if (hdCamera.frameSettings.IsEnabled(FrameSettingsField.RayTracing) && this.GetRayTracingState())
			{
				flag = true;
				flag2 = true;
			}
			if (flag)
			{
				HDRenderPipeline.<>c__DisplayClass729_0 CS$<>8__locals1 = new HDRenderPipeline.<>c__DisplayClass729_0();
				CS$<>8__locals1.mainNormalBuffer = this.m_SharedRTManager.GetNormalBuffer(false);
				texture = hdCamera.GetCurrentFrameRT(5) ?? hdCamera.AllocHistoryFrameRT(5, new Func<string, int, RTHandleSystem, RTHandle>(CS$<>8__locals1.<SendGeometryGraphicsBuffers>g__Allocator|0), 1);
				for (int i = 0; i < hdCamera.viewCount; i++)
				{
					cmd.CopyTexture(CS$<>8__locals1.mainNormalBuffer, i, 0, 0, 0, hdCamera.actualWidth, hdCamera.actualHeight, texture, i, 0, 0, 0);
				}
			}
			if (flag2)
			{
				HDRenderPipeline.<>c__DisplayClass729_1 CS$<>8__locals2 = new HDRenderPipeline.<>c__DisplayClass729_1();
				CS$<>8__locals2.mainDepthBuffer = this.m_SharedRTManager.GetDepthTexture(false);
				texture2 = hdCamera.GetCurrentFrameRT(6) ?? hdCamera.AllocHistoryFrameRT(6, new Func<string, int, RTHandleSystem, RTHandle>(CS$<>8__locals2.<SendGeometryGraphicsBuffers>g__Allocator|1), 1);
				for (int j = 0; j < hdCamera.viewCount; j++)
				{
					cmd.CopyTexture(CS$<>8__locals2.mainDepthBuffer, j, 0, 0, 0, hdCamera.actualWidth, hdCamera.actualHeight, texture2, j, 0, 0, 0);
				}
			}
			if ((vfxcameraBufferTypes & VFXCameraBufferTypes.Depth) != VFXCameraBufferTypes.None)
			{
				VFXManager.SetCameraBuffer(hdCamera.camera, VFXCameraBufferTypes.Depth, texture2, 0, 0, hdCamera.actualWidth, hdCamera.actualHeight);
			}
			if ((vfxcameraBufferTypes & VFXCameraBufferTypes.Normal) != VFXCameraBufferTypes.None)
			{
				VFXManager.SetCameraBuffer(hdCamera.camera, VFXCameraBufferTypes.Normal, texture, 0, 0, hdCamera.actualWidth, hdCamera.actualHeight);
			}
		}

		// Token: 0x0600042F RID: 1071 RVA: 0x000271B8 File Offset: 0x000253B8
		private void SendColorGraphicsBuffer(CommandBuffer cmd, HDCamera hdCamera)
		{
			if ((VFXManager.IsCameraBufferNeeded(hdCamera.camera) & VFXCameraBufferTypes.Color) != VFXCameraBufferTypes.None)
			{
				RTHandle currentFrameRT = hdCamera.GetCurrentFrameRT(0);
				VFXManager.SetCameraBuffer(hdCamera.camera, VFXCameraBufferTypes.Color, currentFrameRT, 0, 0, hdCamera.actualWidth, hdCamera.actualHeight);
			}
		}

		// Token: 0x06000430 RID: 1072 RVA: 0x00002646 File Offset: 0x00000846
		private void InitPathTracing()
		{
		}

		// Token: 0x06000431 RID: 1073 RVA: 0x00002646 File Offset: 0x00000846
		private void ReleasePathTracing()
		{
		}

		// Token: 0x06000432 RID: 1074 RVA: 0x000271FC File Offset: 0x000253FC
		private void CheckCameraChange(HDCamera hdCamera)
		{
			if (hdCamera.mainViewConstants.nonJitteredViewProjMatrix != hdCamera.mainViewConstants.prevViewProjMatrix)
			{
				this.currentIteration = 0U;
			}
		}

		// Token: 0x06000433 RID: 1075 RVA: 0x00027224 File Offset: 0x00025424
		private static RTHandle PathTracingHistoryBufferAllocatorFunction(string viewName, int frameIndex, RTHandleSystem rtHandleSystem)
		{
			return rtHandleSystem.Alloc(Vector2.one, TextureXR.slices, DepthBits.None, GraphicsFormat.R32G32B32A32_SFloat, FilterMode.Point, TextureWrapMode.Repeat, TextureXR.dimension, true, false, false, false, 1, 0f, false, false, false, RenderTextureMemoryless.None, string.Format("PathTracingHistoryBuffer{0}", frameIndex));
		}

		// Token: 0x06000434 RID: 1076 RVA: 0x0002726C File Offset: 0x0002546C
		private void RenderPathTracing(HDCamera hdCamera, CommandBuffer cmd, RTHandle outputTexture, ScriptableRenderContext renderContext, int frameCount)
		{
			RayTracingShader pathTracing = this.m_Asset.renderPipelineRayTracingResources.pathTracing;
			PathTracing component = hdCamera.volumeStack.GetComponent<PathTracing>();
			if (!pathTracing || !component.enable.value)
			{
				return;
			}
			this.CheckCameraChange(hdCamera);
			this.GetBlueNoiseManager().BindDitheredRNGData256SPP(cmd);
			RTHandle rthandle = hdCamera.GetCurrentFrameRT(15) ?? hdCamera.AllocHistoryFrameRT(15, new Func<string, int, RTHandleSystem, RTHandle>(HDRenderPipeline.PathTracingHistoryBufferAllocatorFunction), 1);
			RayTracingAccelerationStructure rayTracingAccelerationStructure = this.RequestAccelerationStructure();
			HDRaytracingLightCluster hdraytracingLightCluster = this.RequestLightCluster();
			LightCluster component2 = hdCamera.volumeStack.GetComponent<LightCluster>();
			RayTracingSettings component3 = hdCamera.volumeStack.GetComponent<RayTracingSettings>();
			cmd.SetRayTracingShaderPass(pathTracing, "PathTracingDXR");
			cmd.SetRayTracingAccelerationStructure(pathTracing, HDShaderIDs._RaytracingAccelerationStructureName, rayTracingAccelerationStructure);
			cmd.SetGlobalTexture(HDShaderIDs._OwenScrambledTexture, this.m_Asset.renderPipelineResources.textures.owenScrambled256Tex);
			cmd.SetGlobalTexture(HDShaderIDs._ScramblingTexture, this.m_Asset.renderPipelineResources.textures.scramblingTex);
			cmd.SetGlobalFloat(HDShaderIDs._RaytracingRayBias, component3.rayBias.value);
			cmd.SetGlobalFloat(HDShaderIDs._RaytracingNumSamples, (float)component.maximumSamples.value);
			cmd.SetGlobalFloat(HDShaderIDs._RaytracingMinRecursion, (float)component.minimumDepth.value);
			cmd.SetGlobalFloat(HDShaderIDs._RaytracingMaxRecursion, (float)component.maximumDepth.value);
			cmd.SetGlobalFloat(HDShaderIDs._RaytracingIntensityClamp, component.maximumIntensity.value);
			cmd.SetGlobalFloat(HDShaderIDs._RaytracingCameraNearPlane, hdCamera.camera.nearClipPlane);
			cmd.SetRayTracingTextureParam(pathTracing, HDShaderIDs._CameraColorTextureRW, outputTexture);
			int raytracingFrameIndex = HDShaderIDs._RaytracingFrameIndex;
			uint num = this.currentIteration;
			this.currentIteration = num + 1U;
			cmd.SetGlobalInt(raytracingFrameIndex, (int)num);
			cmd.SetRayTracingFloatParam(pathTracing, HDShaderIDs._RaytracingPixelSpreadAngle, HDRenderPipeline.GetPixelSpreadAngle(hdCamera.camera.fieldOfView, hdCamera.actualWidth, hdCamera.actualHeight));
			cmd.SetGlobalBuffer(HDShaderIDs._RaytracingLightCluster, hdraytracingLightCluster.GetCluster());
			cmd.SetGlobalBuffer(HDShaderIDs._LightDatasRT, hdraytracingLightCluster.GetLightDatas());
			cmd.SetGlobalVector(HDShaderIDs._MinClusterPos, hdraytracingLightCluster.GetMinClusterPos());
			cmd.SetGlobalVector(HDShaderIDs._MaxClusterPos, hdraytracingLightCluster.GetMaxClusterPos());
			cmd.SetGlobalInt(HDShaderIDs._LightPerCellCount, component2.maxNumLightsPercell.value);
			cmd.SetGlobalInt(HDShaderIDs._PunctualLightCountRT, hdraytracingLightCluster.GetPunctualLightCount());
			cmd.SetGlobalInt(HDShaderIDs._AreaLightCountRT, hdraytracingLightCluster.GetAreaLightCount());
			cmd.SetRayTracingTextureParam(pathTracing, HDShaderIDs._SkyTexture, this.m_SkyManager.GetSkyReflection(hdCamera));
			cmd.SetRayTracingTextureParam(pathTracing, HDShaderIDs._AccumulatedFrameTexture, rthandle);
			cmd.SetRayTracingMatrixParam(pathTracing, HDShaderIDs._PixelCoordToViewDirWS, hdCamera.mainViewConstants.pixelCoordToViewDirWS);
			cmd.DispatchRays(pathTracing, "RayGen", (uint)hdCamera.actualWidth, (uint)hdCamera.actualHeight, 1U, null);
		}

		// Token: 0x06000435 RID: 1077 RVA: 0x0002753B File Offset: 0x0002573B
		private void InitRaytracingDeferred()
		{
			this.m_RayBinResult = new ComputeBuffer(1, 4);
			this.m_RayBinSizeResult = new ComputeBuffer(1, 4);
			this.m_RaytracingGBufferManager = new GBufferManager(this.asset, this.m_DeferredMaterial);
			this.m_RaytracingGBufferManager.CreateBuffers();
		}

		// Token: 0x06000436 RID: 1078 RVA: 0x00027579 File Offset: 0x00025779
		private void ReleaseRayTracingDeferred()
		{
			CoreUtils.SafeRelease(this.m_RayBinResult);
			CoreUtils.SafeRelease(this.m_RayBinSizeResult);
			this.m_RaytracingGBufferManager.DestroyBuffers();
		}

		// Token: 0x06000437 RID: 1079 RVA: 0x0002759C File Offset: 0x0002579C
		private HDRenderPipeline.DeferredLightingRTResources PrepareDeferredLightingRTResources(HDCamera hdCamera, RTHandle directionBuffer, RTHandle ouputBuffer)
		{
			return new HDRenderPipeline.DeferredLightingRTResources
			{
				directionBuffer = directionBuffer,
				depthStencilBuffer = this.m_SharedRTManager.GetDepthStencilBuffer(false),
				normalBuffer = this.m_SharedRTManager.GetNormalBuffer(false),
				skyTexture = this.m_SkyManager.GetSkyReflection(hdCamera),
				gbuffer0 = this.m_RaytracingGBufferManager.GetBuffer(0),
				gbuffer1 = this.m_RaytracingGBufferManager.GetBuffer(1),
				gbuffer2 = this.m_RaytracingGBufferManager.GetBuffer(2),
				gbuffer3 = this.m_RaytracingGBufferManager.GetBuffer(3),
				distanceBuffer = this.GetRayTracingBuffer(InternalRayTracingBuffers.Distance),
				rayCountTexture = this.m_RayCountManager.GetRayCountTexture(),
				litBuffer = ouputBuffer
			};
		}

		// Token: 0x06000438 RID: 1080 RVA: 0x00027668 File Offset: 0x00025868
		private void CheckBinningBuffersSize(HDCamera hdCamera)
		{
			int num = (hdCamera.actualWidth + 15) / 16;
			int num2 = (hdCamera.actualHeight + 15) / 16;
			int num3 = num * 16;
			int num4 = num2 * 16;
			if (num3 * num4 > this.m_RayBinResult.count)
			{
				if (this.m_RayBinResult != null)
				{
					CoreUtils.SafeRelease(this.m_RayBinResult);
					CoreUtils.SafeRelease(this.m_RayBinSizeResult);
					this.m_RayBinResult = null;
					this.m_RayBinSizeResult = null;
				}
				if (num3 * num4 > 0)
				{
					this.m_RayBinResult = new ComputeBuffer(num3 * num4, 4);
					this.m_RayBinSizeResult = new ComputeBuffer(num * num2, 4);
				}
			}
		}

		// Token: 0x06000439 RID: 1081 RVA: 0x000276FC File Offset: 0x000258FC
		private static void BinRays(CommandBuffer cmd, in HDRenderPipeline.DeferredLightingRTParameters config, RTHandle directionBuffer, int texWidth, int texHeight)
		{
			int num = config.rayBinningCS.FindKernel(config.halfResolution ? "RayBinningHalf" : "RayBinning");
			int num2 = (texWidth + 15) / 16;
			int num3 = (texHeight + 15) / 16;
			cmd.SetComputeTextureParam(config.rayBinningCS, num, HDShaderIDs._RaytracingDirectionBuffer, directionBuffer);
			cmd.SetComputeBufferParam(config.rayBinningCS, num, HDShaderIDs._RayBinResult, config.rayBinResult);
			cmd.SetComputeBufferParam(config.rayBinningCS, num, HDShaderIDs._RayBinSizeResult, config.rayBinSizeResult);
			cmd.SetComputeIntParam(config.rayBinningCS, HDShaderIDs._RayBinTileCountX, num2);
			cmd.DispatchCompute(config.rayBinningCS, num, num2, num3, config.viewCount);
		}

		// Token: 0x0600043A RID: 1082 RVA: 0x000277AC File Offset: 0x000259AC
		private static void RenderRaytracingDeferredLighting(CommandBuffer cmd, in HDRenderPipeline.DeferredLightingRTParameters parameters, in HDRenderPipeline.DeferredLightingRTResources buffers)
		{
			int num = parameters.width;
			int num2 = parameters.height;
			if (parameters.halfResolution)
			{
				num /= 2;
				num2 /= 2;
			}
			if (parameters.rayBinning)
			{
				HDRenderPipeline.BinRays(cmd, in parameters, buffers.directionBuffer, num, num2);
			}
			cmd.SetRayTracingShaderPass(parameters.gBufferRaytracingRT, "GBufferDXR");
			if (parameters.rayBinning)
			{
				int num3 = (num + 15) / 16;
				cmd.SetGlobalBuffer(HDShaderIDs._RayBinResult, parameters.rayBinResult);
				cmd.SetGlobalBuffer(HDShaderIDs._RayBinSizeResult, parameters.rayBinSizeResult);
				cmd.SetRayTracingIntParam(parameters.gBufferRaytracingRT, HDShaderIDs._RayBinTileCountX, num3);
			}
			cmd.SetRayTracingAccelerationStructure(parameters.gBufferRaytracingRT, HDShaderIDs._RaytracingAccelerationStructureName, parameters.accelerationStructure);
			cmd.SetRayTracingIntParam(parameters.gBufferRaytracingRT, HDShaderIDs._RayCountEnabled, parameters.rayCountFlag);
			cmd.SetRayTracingIntParam(parameters.gBufferRaytracingRT, HDShaderIDs._RayCountType, parameters.rayCountType);
			cmd.SetRayTracingTextureParam(parameters.gBufferRaytracingRT, HDShaderIDs._RayCountTexture, buffers.rayCountTexture);
			cmd.SetRayTracingFloatParams(parameters.gBufferRaytracingRT, HDShaderIDs._RaytracingRayBias, new float[] { parameters.rayBias });
			cmd.SetRayTracingIntParams(parameters.gBufferRaytracingRT, HDShaderIDs._RayTracingLayerMask, new int[] { parameters.layerMask });
			cmd.SetRayTracingFloatParams(parameters.gBufferRaytracingRT, HDShaderIDs._RaytracingRayMaxLength, new float[] { parameters.maxRayLength });
			cmd.SetRayTracingTextureParam(parameters.gBufferRaytracingRT, HDShaderIDs._DepthTexture, buffers.depthStencilBuffer);
			cmd.SetRayTracingTextureParam(parameters.gBufferRaytracingRT, HDShaderIDs._NormalBufferTexture, buffers.normalBuffer);
			cmd.SetRayTracingTextureParam(parameters.gBufferRaytracingRT, HDShaderIDs._RaytracingDirectionBuffer, buffers.directionBuffer);
			cmd.SetRayTracingFloatParams(parameters.gBufferRaytracingRT, HDShaderIDs._RaytracingPixelSpreadAngle, new float[] { HDRenderPipeline.GetPixelSpreadAngle(parameters.fov, parameters.width, parameters.height) });
			cmd.SetRayTracingTextureParam(parameters.gBufferRaytracingRT, HDShaderIDs._GBufferTextureRW[0], buffers.gbuffer0);
			cmd.SetRayTracingTextureParam(parameters.gBufferRaytracingRT, HDShaderIDs._GBufferTextureRW[1], buffers.gbuffer1);
			cmd.SetRayTracingTextureParam(parameters.gBufferRaytracingRT, HDShaderIDs._GBufferTextureRW[2], buffers.gbuffer2);
			cmd.SetRayTracingTextureParam(parameters.gBufferRaytracingRT, HDShaderIDs._GBufferTextureRW[3], buffers.gbuffer3);
			cmd.SetRayTracingTextureParam(parameters.gBufferRaytracingRT, HDShaderIDs._RaytracingDistanceBuffer, buffers.distanceBuffer);
			uint width = (uint)parameters.width;
			uint height = (uint)parameters.height;
			cmd.SetRayTracingIntParam(parameters.gBufferRaytracingRT, HDShaderIDs._RaytracingIncludeSky, parameters.includeSky ? 1 : 0);
			cmd.SetRayTracingTextureParam(parameters.gBufferRaytracingRT, HDShaderIDs._SkyTexture, buffers.skyTexture);
			CoreUtils.SetKeyword(cmd, "DIFFUSE_LIGHTING_ONLY", parameters.diffuseLightingOnly);
			CoreUtils.SetKeyword(cmd, "MULTI_BOUNCE_INDIRECT", false);
			cmd.SetRayTracingIntParams(parameters.gBufferRaytracingRT, HDShaderIDs._RaytracingDiffuseRay, new int[] { parameters.diffuseLightingOnly ? 1 : 0 });
			if (parameters.rayBinning)
			{
				int num4 = (num + 15) / 16;
				int num5 = (num2 + 15) / 16;
				int num6 = num4 * 16;
				int num7 = num5 * 16;
				cmd.DispatchRays(parameters.gBufferRaytracingRT, "RayGenGBufferBinned", (uint)num6, (uint)num7, 1U, null);
			}
			else
			{
				cmd.SetRayTracingIntParams(parameters.gBufferRaytracingRT, "_RaytracingHalfResolution", new int[] { parameters.halfResolution ? 1 : 0 });
				cmd.DispatchRays(parameters.gBufferRaytracingRT, "RayGenGBuffer", width, height, (uint)parameters.viewCount, null);
			}
			CoreUtils.SetKeyword(cmd, "DIFFUSE_LIGHTING_ONLY", false);
			int num8 = parameters.deferredRaytracingCS.FindKernel(parameters.halfResolution ? "RaytracingDeferredHalf" : "RaytracingDeferred");
			parameters.lightCluster.BindLightClusterData(cmd);
			cmd.SetComputeTextureParam(parameters.deferredRaytracingCS, num8, HDShaderIDs._DepthTexture, buffers.depthStencilBuffer);
			cmd.SetComputeTextureParam(parameters.deferredRaytracingCS, num8, HDShaderIDs._RaytracingDirectionBuffer, buffers.directionBuffer);
			cmd.SetComputeTextureParam(parameters.deferredRaytracingCS, num8, HDShaderIDs._RaytracingDistanceBuffer, buffers.distanceBuffer);
			cmd.SetComputeTextureParam(parameters.deferredRaytracingCS, num8, HDShaderIDs._GBufferTexture[0], buffers.gbuffer0);
			cmd.SetComputeTextureParam(parameters.deferredRaytracingCS, num8, HDShaderIDs._GBufferTexture[1], buffers.gbuffer1);
			cmd.SetComputeTextureParam(parameters.deferredRaytracingCS, num8, HDShaderIDs._GBufferTexture[2], buffers.gbuffer2);
			cmd.SetComputeTextureParam(parameters.deferredRaytracingCS, num8, HDShaderIDs._GBufferTexture[3], buffers.gbuffer3);
			cmd.SetComputeTextureParam(parameters.deferredRaytracingCS, num8, HDShaderIDs._LightLayersTexture, TextureXR.GetWhiteTexture());
			cmd.SetComputeFloatParam(parameters.deferredRaytracingCS, HDShaderIDs._RaytracingIntensityClamp, parameters.clampValue);
			cmd.SetComputeIntParam(parameters.deferredRaytracingCS, HDShaderIDs._RaytracingPreExposition, parameters.preExpose ? 1 : 0);
			cmd.SetComputeTextureParam(parameters.deferredRaytracingCS, num8, HDShaderIDs._RaytracingLitBufferRW, buffers.litBuffer);
			int num9 = 8;
			int num10 = (num + (num9 - 1)) / num9;
			int num11 = (num2 + (num9 - 1)) / num9;
			cmd.DispatchCompute(parameters.deferredRaytracingCS, num8, num10, num11, parameters.viewCount);
		}

		// Token: 0x0600043B RID: 1083 RVA: 0x00027CE0 File Offset: 0x00025EE0
		private void InitRayTracedIndirectDiffuse()
		{
			this.m_IndirectDiffuseBuffer = RTHandles.Alloc(Vector2.one, TextureXR.slices, DepthBits.None, GraphicsFormat.R16G16B16A16_SFloat, FilterMode.Point, TextureWrapMode.Repeat, TextureXR.dimension, true, false, false, false, 1, 0f, false, false, true, RenderTextureMemoryless.None, "IndirectDiffuseBuffer");
		}

		// Token: 0x0600043C RID: 1084 RVA: 0x00027D1F File Offset: 0x00025F1F
		private void ReleaseRayTracedIndirectDiffuse()
		{
			RTHandles.Release(this.m_IndirectDiffuseBuffer);
		}

		// Token: 0x0600043D RID: 1085 RVA: 0x00027D2C File Offset: 0x00025F2C
		private void BindIndirectDiffuseTexture(CommandBuffer cmd)
		{
			cmd.SetGlobalTexture(HDShaderIDs._IndirectDiffuseTexture, this.m_IndirectDiffuseBuffer);
		}

		// Token: 0x0600043E RID: 1086 RVA: 0x00027D44 File Offset: 0x00025F44
		private RTHandle IndirectDiffuseHistoryBufferAllocatorFunction(string viewName, int frameIndex, RTHandleSystem rtHandleSystem)
		{
			return rtHandleSystem.Alloc(Vector2.one, TextureXR.slices, DepthBits.None, GraphicsFormat.R16G16B16A16_SFloat, FilterMode.Point, TextureWrapMode.Repeat, TextureXR.dimension, true, false, false, false, 1, 0f, false, false, false, RenderTextureMemoryless.None, string.Format("IndirectDiffuseHistoryBuffer{0}", frameIndex));
		}

		// Token: 0x0600043F RID: 1087 RVA: 0x00027D89 File Offset: 0x00025F89
		private RTHandle GetIndirectDiffuseTexture()
		{
			return this.m_IndirectDiffuseBuffer;
		}

		// Token: 0x06000440 RID: 1088 RVA: 0x00027D94 File Offset: 0x00025F94
		private bool ValidIndirectDiffuseState(HDCamera hdCamera)
		{
			GlobalIllumination component = hdCamera.volumeStack.GetComponent<GlobalIllumination>();
			return hdCamera.frameSettings.IsEnabled(FrameSettingsField.RayTracing) && component.rayTracing.value;
		}

		// Token: 0x06000441 RID: 1089 RVA: 0x00027DCC File Offset: 0x00025FCC
		private void RenderIndirectDiffuse(HDCamera hdCamera, CommandBuffer cmd, ScriptableRenderContext renderContext, int frameCount)
		{
			if (!this.ValidIndirectDiffuseState(hdCamera))
			{
				return;
			}
			RayTracingMode value = hdCamera.volumeStack.GetComponent<GlobalIllumination>().mode.value;
			if (value != RayTracingMode.Performance)
			{
				if (value == RayTracingMode.Quality)
				{
					this.RenderIndirectDiffuseQuality(hdCamera, cmd, renderContext, frameCount);
				}
			}
			else
			{
				this.RenderIndirectDiffusePerformance(hdCamera, cmd, renderContext, frameCount);
			}
			this.BindIndirectDiffuseTexture(cmd);
			ComputeShader indirectDiffuseRaytracingCS = this.m_Asset.renderPipelineRayTracingResources.indirectDiffuseRaytracingCS;
			if (hdCamera.frameSettings.litShaderMode == LitShaderMode.Deferred)
			{
				int num = indirectDiffuseRaytracingCS.FindKernel("IndirectDiffuseAccumulation");
				cmd.SetComputeTextureParam(indirectDiffuseRaytracingCS, num, HDShaderIDs._IndirectDiffuseTexture, this.m_IndirectDiffuseBuffer);
				cmd.SetComputeTextureParam(indirectDiffuseRaytracingCS, num, HDShaderIDs._GBufferTexture[0], this.m_GbufferManager.GetBuffer(0));
				cmd.SetComputeTextureParam(indirectDiffuseRaytracingCS, num, HDShaderIDs._GBufferTexture[3], this.m_GbufferManager.GetBuffer(3));
				cmd.SetComputeVectorParam(indirectDiffuseRaytracingCS, HDShaderIDs._IndirectLightingMultiplier, new Vector4(hdCamera.volumeStack.GetComponent<IndirectLightingController>().indirectDiffuseIntensity.value, 0f, 0f, 0f));
				int num2 = 8;
				int num3 = (hdCamera.actualWidth + (num2 - 1)) / num2;
				int num4 = (hdCamera.actualHeight + (num2 - 1)) / num2;
				cmd.DispatchCompute(indirectDiffuseRaytracingCS, num, num3, num4, hdCamera.viewCount);
			}
			(RenderPipelineManager.currentPipeline as HDRenderPipeline).PushFullScreenDebugTexture(hdCamera, cmd, this.m_IndirectDiffuseBuffer, FullScreenDebugMode.RayTracedGlobalIllumination);
		}

		// Token: 0x06000442 RID: 1090 RVA: 0x00027F30 File Offset: 0x00026130
		private HDRenderPipeline.DeferredLightingRTParameters PrepareIndirectDiffuseDeferredLightingRTParameters(HDCamera hdCamera)
		{
			HDRenderPipeline.DeferredLightingRTParameters deferredLightingRTParameters = default(HDRenderPipeline.DeferredLightingRTParameters);
			GlobalIllumination component = hdCamera.volumeStack.GetComponent<GlobalIllumination>();
			RayTracingSettings component2 = hdCamera.volumeStack.GetComponent<RayTracingSettings>();
			this.CheckBinningBuffersSize(hdCamera);
			deferredLightingRTParameters.rayBinning = true;
			deferredLightingRTParameters.layerMask.value = 32;
			deferredLightingRTParameters.rayBias = component2.rayBias.value;
			deferredLightingRTParameters.maxRayLength = component.rayLength.value;
			deferredLightingRTParameters.clampValue = component.clampValue.value;
			deferredLightingRTParameters.includeSky = true;
			deferredLightingRTParameters.diffuseLightingOnly = true;
			deferredLightingRTParameters.halfResolution = false;
			deferredLightingRTParameters.rayCountFlag = this.m_RayCountManager.RayCountIsEnabled();
			deferredLightingRTParameters.rayCountType = 5;
			deferredLightingRTParameters.preExpose = true;
			deferredLightingRTParameters.width = hdCamera.actualWidth;
			deferredLightingRTParameters.height = hdCamera.actualHeight;
			deferredLightingRTParameters.viewCount = hdCamera.viewCount;
			deferredLightingRTParameters.fov = hdCamera.camera.fieldOfView;
			deferredLightingRTParameters.rayBinResult = this.m_RayBinResult;
			deferredLightingRTParameters.rayBinSizeResult = this.m_RayBinSizeResult;
			deferredLightingRTParameters.accelerationStructure = this.RequestAccelerationStructure();
			deferredLightingRTParameters.lightCluster = this.RequestLightCluster();
			deferredLightingRTParameters.gBufferRaytracingRT = this.m_Asset.renderPipelineRayTracingResources.gBufferRaytracingRT;
			deferredLightingRTParameters.deferredRaytracingCS = this.m_Asset.renderPipelineRayTracingResources.deferredRaytracingCS;
			deferredLightingRTParameters.rayBinningCS = this.m_Asset.renderPipelineRayTracingResources.rayBinningCS;
			if (deferredLightingRTParameters.viewCount > 1 && deferredLightingRTParameters.rayBinning)
			{
				deferredLightingRTParameters.rayBinning = false;
				Debug.LogWarning("Ray binning is not supported with XR single-pass rendering!");
			}
			return deferredLightingRTParameters;
		}

		// Token: 0x06000443 RID: 1091 RVA: 0x000280C0 File Offset: 0x000262C0
		private void RenderIndirectDiffusePerformance(HDCamera hdCamera, CommandBuffer cmd, ScriptableRenderContext renderContext, int frameCount)
		{
			GlobalIllumination component = hdCamera.volumeStack.GetComponent<GlobalIllumination>();
			BlueNoise blueNoiseManager = this.GetBlueNoiseManager();
			hdCamera.volumeStack.GetComponent<LightCluster>();
			hdCamera.volumeStack.GetComponent<RayTracingSettings>();
			ComputeShader indirectDiffuseRaytracingCS = this.m_Asset.renderPipelineRayTracingResources.indirectDiffuseRaytracingCS;
			RTHandle rayTracingBuffer = this.GetRayTracingBuffer(InternalRayTracingBuffers.Direction);
			RTHandle rayTracingBuffer2 = this.GetRayTracingBuffer(InternalRayTracingBuffers.RGBA1);
			using (new ProfilingScope(cmd, ProfilingSampler.Get<HDProfileId>(HDProfileId.RaytracingIntegrateIndirectDiffuse)))
			{
				int num = indirectDiffuseRaytracingCS.FindKernel(component.fullResolution.value ? "RaytracingIndirectDiffuseFullRes" : "RaytracingIndirectDiffuseHalfRes");
				blueNoiseManager.BindDitheredRNGData8SPP(cmd);
				cmd.SetComputeTextureParam(indirectDiffuseRaytracingCS, num, HDShaderIDs._DepthTexture, this.m_SharedRTManager.GetDepthStencilBuffer(false));
				cmd.SetComputeTextureParam(indirectDiffuseRaytracingCS, num, HDShaderIDs._NormalBufferTexture, this.m_SharedRTManager.GetNormalBuffer(false));
				cmd.SetComputeFloatParam(indirectDiffuseRaytracingCS, HDShaderIDs._RaytracingIntensityClamp, component.clampValue.value);
				int num2 = this.RayTracingFrameIndex(hdCamera);
				cmd.SetComputeIntParam(indirectDiffuseRaytracingCS, HDShaderIDs._RaytracingFrameIndex, num2);
				cmd.SetComputeTextureParam(indirectDiffuseRaytracingCS, num, HDShaderIDs._RaytracingDirectionBuffer, rayTracingBuffer);
				int actualWidth = hdCamera.actualWidth;
				int actualHeight = hdCamera.actualHeight;
				int num3 = 8;
				int num4 = (actualWidth + (num3 - 1)) / num3;
				int num5 = (actualHeight + (num3 - 1)) / num3;
				cmd.DispatchCompute(indirectDiffuseRaytracingCS, num, num4, num5, hdCamera.viewCount);
				HDRenderPipeline.DeferredLightingRTParameters deferredLightingRTParameters = this.PrepareIndirectDiffuseDeferredLightingRTParameters(hdCamera);
				HDRenderPipeline.DeferredLightingRTResources deferredLightingRTResources = this.PrepareDeferredLightingRTResources(hdCamera, rayTracingBuffer, this.m_IndirectDiffuseBuffer);
				HDRenderPipeline.RenderRaytracingDeferredLighting(cmd, in deferredLightingRTParameters, in deferredLightingRTResources);
			}
			using (new ProfilingScope(cmd, ProfilingSampler.Get<HDProfileId>(HDProfileId.RaytracingFilterIndirectDiffuse)))
			{
				int num6 = indirectDiffuseRaytracingCS.FindKernel(component.fullResolution.value ? "IndirectDiffuseIntegrationUpscaleFullRes" : "IndirectDiffuseIntegrationUpscaleHalfRes");
				cmd.SetComputeTextureParam(indirectDiffuseRaytracingCS, num6, HDShaderIDs._DepthTexture, this.m_SharedRTManager.GetDepthStencilBuffer(false));
				cmd.SetComputeTextureParam(indirectDiffuseRaytracingCS, num6, HDShaderIDs._NormalBufferTexture, this.m_SharedRTManager.GetNormalBuffer(false));
				cmd.SetComputeTextureParam(indirectDiffuseRaytracingCS, num6, HDShaderIDs._IndirectDiffuseTexture, this.m_IndirectDiffuseBuffer);
				cmd.SetComputeTextureParam(indirectDiffuseRaytracingCS, num6, HDShaderIDs._RaytracingDirectionBuffer, rayTracingBuffer);
				cmd.SetComputeTextureParam(indirectDiffuseRaytracingCS, num6, HDShaderIDs._BlueNoiseTexture, blueNoiseManager.textureArray16RGB);
				cmd.SetComputeTextureParam(indirectDiffuseRaytracingCS, num6, HDShaderIDs._UpscaledIndirectDiffuseTextureRW, rayTracingBuffer2);
				cmd.SetComputeTextureParam(indirectDiffuseRaytracingCS, num6, HDShaderIDs._ScramblingTexture, this.m_Asset.renderPipelineResources.textures.scramblingTex);
				cmd.SetComputeIntParam(indirectDiffuseRaytracingCS, HDShaderIDs._SpatialFilterRadius, component.upscaleRadius.value);
				int actualWidth2 = hdCamera.actualWidth;
				int actualHeight2 = hdCamera.actualHeight;
				int num7 = 8;
				int num8 = (actualWidth2 + (num7 - 1)) / num7;
				int num9 = (actualHeight2 + (num7 - 1)) / num7;
				cmd.DispatchCompute(indirectDiffuseRaytracingCS, num6, num8, num9, hdCamera.viewCount);
				HDUtils.BlitCameraTexture(cmd, rayTracingBuffer2, this.m_IndirectDiffuseBuffer, 0f, false);
				if (component.denoise.value)
				{
					this.DenoiseIndirectDiffuseBuffer(hdCamera, cmd, component);
				}
			}
		}

		// Token: 0x06000444 RID: 1092 RVA: 0x000283F0 File Offset: 0x000265F0
		private void BindRayTracedIndirectDiffuseData(CommandBuffer cmd, HDCamera hdCamera, RayTracingShader indirectDiffuseShader, GlobalIllumination settings, LightCluster lightClusterSettings, RayTracingSettings rtSettings, RTHandle outputLightingBuffer, RTHandle outputHitPointBuffer)
		{
			RayTracingAccelerationStructure rayTracingAccelerationStructure = this.RequestAccelerationStructure();
			HDRaytracingLightCluster hdraytracingLightCluster = this.RequestLightCluster();
			BlueNoise blueNoiseManager = this.GetBlueNoiseManager();
			cmd.SetRayTracingShaderPass(indirectDiffuseShader, "IndirectDXR");
			cmd.SetRayTracingAccelerationStructure(indirectDiffuseShader, HDShaderIDs._RaytracingAccelerationStructureName, rayTracingAccelerationStructure);
			blueNoiseManager.BindDitheredRNGData8SPP(cmd);
			cmd.SetGlobalFloat(HDShaderIDs._RaytracingRayBias, rtSettings.rayBias.value);
			cmd.SetGlobalFloat(HDShaderIDs._RaytracingRayMaxLength, settings.rayLength.value);
			cmd.SetRayTracingIntParams(indirectDiffuseShader, HDShaderIDs._RaytracingNumSamples, new int[] { settings.sampleCount.value });
			int num = this.RayTracingFrameIndex(hdCamera);
			cmd.SetGlobalInt(HDShaderIDs._RaytracingFrameIndex, num);
			cmd.SetRayTracingTextureParam(indirectDiffuseShader, HDShaderIDs._IndirectDiffuseTextureRW, outputLightingBuffer);
			cmd.SetRayTracingTextureParam(indirectDiffuseShader, HDShaderIDs._IndirectDiffuseHitPointTextureRW, outputHitPointBuffer);
			cmd.SetRayTracingTextureParam(indirectDiffuseShader, HDShaderIDs._DepthTexture, this.m_SharedRTManager.GetDepthStencilBuffer(false));
			cmd.SetRayTracingTextureParam(indirectDiffuseShader, HDShaderIDs._NormalBufferTexture, this.m_SharedRTManager.GetNormalBuffer(false));
			cmd.SetRayTracingFloatParams(indirectDiffuseShader, HDShaderIDs._RaytracingIntensityClamp, new float[] { settings.clampValue.value });
			RayCountManager rayCountManager = this.GetRayCountManager();
			cmd.SetRayTracingIntParam(indirectDiffuseShader, HDShaderIDs._RayCountEnabled, rayCountManager.RayCountIsEnabled());
			cmd.SetRayTracingTextureParam(indirectDiffuseShader, HDShaderIDs._RayCountTexture, rayCountManager.GetRayCountTexture());
			cmd.SetRayTracingFloatParam(indirectDiffuseShader, HDShaderIDs._RaytracingPixelSpreadAngle, HDRenderPipeline.GetPixelSpreadAngle(hdCamera.camera.fieldOfView, hdCamera.actualWidth, hdCamera.actualHeight));
			hdraytracingLightCluster.BindLightClusterData(cmd);
			cmd.SetRayTracingTextureParam(indirectDiffuseShader, HDShaderIDs._SkyTexture, this.m_SkyManager.GetSkyReflection(hdCamera));
			cmd.SetGlobalInt(HDShaderIDs._RaytracingMaxRecursion, settings.bounceCount.value);
		}

		// Token: 0x06000445 RID: 1093 RVA: 0x000285A8 File Offset: 0x000267A8
		private void RenderIndirectDiffuseQuality(HDCamera hdCamera, CommandBuffer cmd, ScriptableRenderContext renderContext, int frameCount)
		{
			GlobalIllumination component = hdCamera.volumeStack.GetComponent<GlobalIllumination>();
			LightCluster component2 = hdCamera.volumeStack.GetComponent<LightCluster>();
			RayTracingSettings component3 = hdCamera.volumeStack.GetComponent<RayTracingSettings>();
			RayTracingShader indirectDiffuseRaytracingRT = this.m_Asset.renderPipelineRayTracingResources.indirectDiffuseRaytracingRT;
			RTHandle rayTracingBuffer = this.GetRayTracingBuffer(InternalRayTracingBuffers.RGBA1);
			this.BindRayTracedIndirectDiffuseData(cmd, hdCamera, indirectDiffuseRaytracingRT, component, component2, component3, this.m_IndirectDiffuseBuffer, rayTracingBuffer);
			int actualWidth = hdCamera.actualWidth;
			int actualHeight = hdCamera.actualHeight;
			CoreUtils.SetKeyword(cmd, "MULTI_BOUNCE_INDIRECT", component.bounceCount.value > 1);
			CoreUtils.SetKeyword(cmd, "DIFFUSE_LIGHTING_ONLY", true);
			cmd.DispatchRays(indirectDiffuseRaytracingRT, "RayGenIntegration", (uint)actualWidth, (uint)actualHeight, (uint)hdCamera.viewCount, null);
			CoreUtils.SetKeyword(cmd, "DIFFUSE_LIGHTING_ONLY", false);
			CoreUtils.SetKeyword(cmd, "MULTI_BOUNCE_INDIRECT", false);
			using (new ProfilingScope(cmd, ProfilingSampler.Get<HDProfileId>(HDProfileId.RaytracingFilterIndirectDiffuse)))
			{
				if (component.denoise.value)
				{
					this.DenoiseIndirectDiffuseBuffer(hdCamera, cmd, component);
				}
			}
		}

		// Token: 0x06000446 RID: 1094 RVA: 0x000286B0 File Offset: 0x000268B0
		private void DenoiseIndirectDiffuseBuffer(HDCamera hdCamera, CommandBuffer cmd, GlobalIllumination settings)
		{
			RTHandle rthandle = hdCamera.GetCurrentFrameRT(12) ?? hdCamera.AllocHistoryFrameRT(12, new Func<string, int, RTHandleSystem, RTHandle>(this.IndirectDiffuseHistoryBufferAllocatorFunction), 1);
			RTHandle rayTracingBuffer = this.GetRayTracingBuffer(InternalRayTracingBuffers.RGBA1);
			float num = 1f;
			num *= (this.ValidRayTracingHistory(hdCamera) ? 1f : 0f);
			HDTemporalFilter temporalFilter = this.GetTemporalFilter();
			temporalFilter.DenoiseBuffer(cmd, hdCamera, this.m_IndirectDiffuseBuffer, rthandle, rayTracingBuffer, false, num);
			HDDiffuseDenoiser diffuseDenoiser = this.GetDiffuseDenoiser();
			diffuseDenoiser.DenoiseBuffer(cmd, hdCamera, rayTracingBuffer, this.m_IndirectDiffuseBuffer, settings.denoiserRadius.value, false, settings.halfResolutionDenoiser.value);
			if (settings.secondDenoiserPass.value)
			{
				RTHandle rthandle2 = hdCamera.GetCurrentFrameRT(13) ?? hdCamera.AllocHistoryFrameRT(13, new Func<string, int, RTHandleSystem, RTHandle>(this.IndirectDiffuseHistoryBufferAllocatorFunction), 1);
				temporalFilter.DenoiseBuffer(cmd, hdCamera, this.m_IndirectDiffuseBuffer, rthandle2, rayTracingBuffer, false, num);
				diffuseDenoiser.DenoiseBuffer(cmd, hdCamera, rayTracingBuffer, this.m_IndirectDiffuseBuffer, settings.secondDenoiserRadius.value, false, settings.halfResolutionDenoiser.value);
			}
		}

		// Token: 0x06000447 RID: 1095 RVA: 0x000287B8 File Offset: 0x000269B8
		internal void InitRayTracingManager()
		{
			this.m_TemporalFilter.Init(this.m_Asset.renderPipelineRayTracingResources, this.m_SharedRTManager, this);
			this.m_SimpleDenoiser.Init(this.m_Asset.renderPipelineRayTracingResources, this.m_SharedRTManager, this);
			this.m_DiffuseDenoiser.Init(this.m_Asset.renderPipelineResources, this.m_Asset.renderPipelineRayTracingResources, this.m_SharedRTManager, this);
			this.m_ReflectionDenoiser.Init(this.m_Asset.renderPipelineRayTracingResources, this.m_SharedRTManager, this);
			this.m_RayCountManager.Init(this.m_Asset.renderPipelineRayTracingResources);
			this.m_RayTracingLightCluster.Initialize(this);
			this.m_RayTracingDirectionBuffer = RTHandles.Alloc(Vector2.one, TextureXR.slices, DepthBits.None, GraphicsFormat.R16G16B16A16_SFloat, FilterMode.Point, TextureWrapMode.Repeat, TextureXR.dimension, true, false, true, false, 1, 0f, false, false, true, RenderTextureMemoryless.None, "RaytracingDirectionBuffer");
			this.m_RayTracingDistanceBuffer = RTHandles.Alloc(Vector2.one, TextureXR.slices, DepthBits.None, GraphicsFormat.R32_SFloat, FilterMode.Point, TextureWrapMode.Repeat, TextureXR.dimension, true, false, true, false, 1, 0f, false, false, true, RenderTextureMemoryless.None, "RaytracingDistanceBuffer");
			this.m_RayTracingIntermediateBufferR0 = RTHandles.Alloc(Vector2.one, TextureXR.slices, DepthBits.None, GraphicsFormat.R8_SNorm, FilterMode.Point, TextureWrapMode.Repeat, TextureXR.dimension, true, false, true, false, 1, 0f, false, false, true, RenderTextureMemoryless.None, "RayTracingIntermediateBufferR0");
			this.m_RayTracingIntermediateBufferR1 = RTHandles.Alloc(Vector2.one, TextureXR.slices, DepthBits.None, GraphicsFormat.R8_SNorm, FilterMode.Point, TextureWrapMode.Repeat, TextureXR.dimension, true, false, true, false, 1, 0f, false, false, true, RenderTextureMemoryless.None, "RayTracingIntermediateBufferR1");
			this.m_RayTracingIntermediateBufferRG0 = RTHandles.Alloc(Vector2.one, TextureXR.slices, DepthBits.None, GraphicsFormat.R16G16_SFloat, FilterMode.Point, TextureWrapMode.Repeat, TextureXR.dimension, true, false, true, false, 1, 0f, false, false, true, RenderTextureMemoryless.None, "RayTracingIntermediateBufferRG0");
			this.m_RayTracingIntermediateBufferRGBA0 = RTHandles.Alloc(Vector2.one, TextureXR.slices, DepthBits.None, GraphicsFormat.R16G16B16A16_SFloat, FilterMode.Point, TextureWrapMode.Repeat, TextureXR.dimension, true, false, true, false, 1, 0f, false, false, true, RenderTextureMemoryless.None, "RayTracingIntermediateBufferRGBA0");
			this.m_RayTracingIntermediateBufferRGBA1 = RTHandles.Alloc(Vector2.one, TextureXR.slices, DepthBits.None, GraphicsFormat.R16G16B16A16_SFloat, FilterMode.Point, TextureWrapMode.Repeat, TextureXR.dimension, true, false, true, false, 1, 0f, false, false, true, RenderTextureMemoryless.None, "RayTracingIntermediateBufferRGBA1");
			this.m_RayTracingIntermediateBufferRGBA2 = RTHandles.Alloc(Vector2.one, TextureXR.slices, DepthBits.None, GraphicsFormat.R16G16B16A16_SFloat, FilterMode.Point, TextureWrapMode.Repeat, TextureXR.dimension, true, false, true, false, 1, 0f, false, false, true, RenderTextureMemoryless.None, "RayTracingIntermediateBufferRGBA2");
			this.m_RayTracingIntermediateBufferRGBA3 = RTHandles.Alloc(Vector2.one, TextureXR.slices, DepthBits.None, GraphicsFormat.R16G16B16A16_SFloat, FilterMode.Point, TextureWrapMode.Repeat, TextureXR.dimension, true, false, true, false, 1, 0f, false, false, true, RenderTextureMemoryless.None, "RayTracingIntermediateBufferRGBA3");
		}

		// Token: 0x06000448 RID: 1096 RVA: 0x00028A28 File Offset: 0x00026C28
		internal void ReleaseRayTracingManager()
		{
			RTHandles.Release(this.m_RayTracingDistanceBuffer);
			RTHandles.Release(this.m_RayTracingDirectionBuffer);
			RTHandles.Release(this.m_RayTracingIntermediateBufferR0);
			RTHandles.Release(this.m_RayTracingIntermediateBufferR1);
			RTHandles.Release(this.m_RayTracingIntermediateBufferRG0);
			RTHandles.Release(this.m_RayTracingIntermediateBufferRGBA0);
			RTHandles.Release(this.m_RayTracingIntermediateBufferRGBA1);
			RTHandles.Release(this.m_RayTracingIntermediateBufferRGBA2);
			RTHandles.Release(this.m_RayTracingIntermediateBufferRGBA3);
			this.m_RayTracingLightCluster.ReleaseResources();
			this.m_ReflectionDenoiser.Release();
			this.m_TemporalFilter.Release();
			this.m_SimpleDenoiser.Release();
			this.m_DiffuseDenoiser.Release();
			this.m_RayCountManager.Release();
		}

		// Token: 0x06000449 RID: 1097 RVA: 0x00028ADC File Offset: 0x00026CDC
		private AccelerationStructureStatus AddInstanceToRAS(Renderer currentRenderer, bool rayTracedShadow, bool aoEnabled, int aoLayerValue, bool reflEnabled, int reflLayerValue, bool giEnabled, int giLayerValue, bool recursiveEnabled, int rrLayerValue, bool pathTracingEnabled, int ptLayerValue)
		{
			currentRenderer.GetSharedMaterials(this.materialArray);
			if (this.materialArray == null)
			{
				return AccelerationStructureStatus.NullMaterial;
			}
			int num;
			if (!(currentRenderer.GetType() == typeof(SkinnedMeshRenderer)))
			{
				MeshFilter meshFilter;
				currentRenderer.TryGetComponent<MeshFilter>(out meshFilter);
				if (meshFilter == null || meshFilter.sharedMesh == null)
				{
					return AccelerationStructureStatus.MissingMesh;
				}
				num = meshFilter.sharedMesh.subMeshCount;
			}
			else
			{
				SkinnedMeshRenderer skinnedMeshRenderer = (SkinnedMeshRenderer)currentRenderer;
				if (skinnedMeshRenderer.sharedMesh == null)
				{
					return AccelerationStructureStatus.MissingMesh;
				}
				num = skinnedMeshRenderer.sharedMesh.subMeshCount;
			}
			int num2 = 1 << currentRenderer.gameObject.layer;
			uint num3 = 0U;
			bool flag = false;
			bool flag2 = true;
			bool flag3 = false;
			for (int i = 0; i < num; i++)
			{
				bool flag4 = false;
				if (this.materialArray.Count > i)
				{
					Material material = this.materialArray[i];
					if (material != null)
					{
						flag4 = true;
						this.subMeshFlagArray[i] = true;
						this.subMeshTransparentArray[i] = material.IsKeywordEnabled("_SURFACE_TYPE_TRANSPARENT") || (HDRenderQueue.k_RenderQueue_Transparent.lowerBound <= material.renderQueue && HDRenderQueue.k_RenderQueue_Transparent.upperBound >= material.renderQueue) || (HDRenderQueue.k_RenderQueue_AllTransparentRaytracing.lowerBound <= material.renderQueue && HDRenderQueue.k_RenderQueue_AllTransparentRaytracing.upperBound >= material.renderQueue);
						flag2 &= this.subMeshTransparentArray[i];
						flag3 |= this.subMeshTransparentArray[i];
						this.subMeshCutoffArray[i] = material.IsKeywordEnabled("_ALPHATEST_ON") || (HDRenderQueue.k_RenderQueue_OpaqueAlphaTest.lowerBound <= material.renderQueue && HDRenderQueue.k_RenderQueue_OpaqueAlphaTest.upperBound >= material.renderQueue);
						bool flag5 = material.doubleSidedGI || material.IsKeywordEnabled("_DOUBLESIDED_ON");
						flag |= !flag5;
					}
				}
				if (!flag4)
				{
					this.subMeshFlagArray[i] = false;
					this.subMeshCutoffArray[i] = false;
					flag = true;
				}
			}
			if (!flag2 && flag3)
			{
				for (int j = 0; j < num; j++)
				{
					this.subMeshCutoffArray[j] = this.subMeshTransparentArray[j] || this.subMeshCutoffArray[j];
				}
			}
			if (!flag3)
			{
				num3 |= 1U;
			}
			if (rayTracedShadow || pathTracingEnabled)
			{
				if (flag3)
				{
					num3 |= ((currentRenderer.shadowCastingMode != ShadowCastingMode.Off) ? 2U : 0U);
				}
				else
				{
					num3 |= ((currentRenderer.shadowCastingMode != ShadowCastingMode.Off) ? 4U : 0U);
				}
			}
			bool flag6 = currentRenderer.shadowCastingMode != ShadowCastingMode.ShadowsOnly;
			if (aoEnabled && !flag2 && flag6)
			{
				num3 |= (((aoLayerValue & num2) != 0) ? 8U : 0U);
			}
			if (reflEnabled && !flag2 && flag6)
			{
				num3 |= (((reflLayerValue & num2) != 0) ? 16U : 0U);
			}
			if (giEnabled && !flag2 && flag6)
			{
				num3 |= (((giLayerValue & num2) != 0) ? 32U : 0U);
			}
			if (recursiveEnabled && flag6)
			{
				num3 |= (((rrLayerValue & num2) != 0) ? 64U : 0U);
			}
			if (pathTracingEnabled && flag6)
			{
				num3 |= (((ptLayerValue & num2) != 0) ? 128U : 0U);
			}
			if (num3 == 0U)
			{
				return AccelerationStructureStatus.Added;
			}
			this.m_CurrentRAS.AddInstance(currentRenderer, this.subMeshFlagArray, this.subMeshCutoffArray, flag, false, num3);
			if (flag2 || !flag3)
			{
				return AccelerationStructureStatus.Added;
			}
			return AccelerationStructureStatus.TransparencyIssue;
		}

		// Token: 0x0600044A RID: 1098 RVA: 0x00028E34 File Offset: 0x00027034
		internal void BuildRayTracingAccelerationStructure(HDCamera hdCamera)
		{
			this.m_RayTracingRendererReference.Clear();
			this.m_RayTracingLights.hdDirectionalLightArray.Clear();
			this.m_RayTracingLights.hdPointLightArray.Clear();
			this.m_RayTracingLights.hdLineLightArray.Clear();
			this.m_RayTracingLights.hdRectLightArray.Clear();
			this.m_RayTracingLights.hdLightArray.Clear();
			this.m_RayTracingLights.reflectionProbeArray.Clear();
			this.m_RayTracingLights.lightCount = 0;
			this.m_CurrentRAS.Dispose();
			this.m_CurrentRAS = new RayTracingAccelerationStructure();
			this.m_ValidRayTracingState = false;
			this.m_ValidRayTracingCluster = false;
			bool flag = false;
			foreach (HDAdditionalLightData hdadditionalLightData in Object.FindObjectsOfType<HDAdditionalLightData>())
			{
				if (hdadditionalLightData.enabled)
				{
					flag |= hdadditionalLightData.useRayTracedShadows || (hdadditionalLightData.useContactShadow.@override && hdadditionalLightData.rayTraceContactShadow);
					switch (hdadditionalLightData.type)
					{
					case HDLightType.Spot:
					case HDLightType.Point:
						this.m_RayTracingLights.hdPointLightArray.Add(hdadditionalLightData);
						break;
					case HDLightType.Directional:
						this.m_RayTracingLights.hdDirectionalLightArray.Add(hdadditionalLightData);
						break;
					case HDLightType.Area:
					{
						AreaLightShape areaLightShape = hdadditionalLightData.areaLightShape;
						if (areaLightShape != AreaLightShape.Rectangle)
						{
							if (areaLightShape == AreaLightShape.Tube)
							{
								this.m_RayTracingLights.hdLineLightArray.Add(hdadditionalLightData);
							}
						}
						else
						{
							this.m_RayTracingLights.hdRectLightArray.Add(hdadditionalLightData);
						}
						break;
					}
					}
				}
			}
			this.m_RayTracingLights.hdLightArray.AddRange(this.m_RayTracingLights.hdPointLightArray);
			this.m_RayTracingLights.hdLightArray.AddRange(this.m_RayTracingLights.hdLineLightArray);
			this.m_RayTracingLights.hdLightArray.AddRange(this.m_RayTracingLights.hdRectLightArray);
			foreach (HDAdditionalReflectionData hdadditionalReflectionData in Object.FindObjectsOfType<HDAdditionalReflectionData>())
			{
				if (hdadditionalReflectionData.enabled)
				{
					this.m_RayTracingLights.reflectionProbeArray.Add(hdadditionalReflectionData);
				}
			}
			this.m_RayTracingLights.lightCount = this.m_RayTracingLights.hdPointLightArray.Count + this.m_RayTracingLights.hdLineLightArray.Count + this.m_RayTracingLights.hdRectLightArray.Count + this.m_RayTracingLights.reflectionProbeArray.Count;
			AmbientOcclusion component = hdCamera.volumeStack.GetComponent<AmbientOcclusion>();
			ScreenSpaceReflection component2 = hdCamera.volumeStack.GetComponent<ScreenSpaceReflection>();
			GlobalIllumination component3 = hdCamera.volumeStack.GetComponent<GlobalIllumination>();
			RecursiveRendering component4 = hdCamera.volumeStack.GetComponent<RecursiveRendering>();
			PathTracing component5 = hdCamera.volumeStack.GetComponent<PathTracing>();
			LODGroup[] array3 = Object.FindObjectsOfType<LODGroup>();
			for (int k = 0; k < array3.Length; k++)
			{
				LOD[] lods = array3[k].GetLODs();
				for (int l = 0; l < lods.Length; l++)
				{
					LOD lod = lods[l];
					if (l == 0)
					{
						for (int m = 0; m < lod.renderers.Length; m++)
						{
							Renderer renderer = lod.renderers[m];
							this.AddInstanceToRAS(renderer, flag, component.rayTracing.value, component.layerMask.value, component2.rayTracing.value, component2.layerMask.value, component3.rayTracing.value, component3.layerMask.value, component4.enable.value, component4.layerMask.value, component5.enable.value, component5.layerMask.value);
						}
					}
					for (int n = 0; n < lod.renderers.Length; n++)
					{
						Renderer renderer2 = lod.renderers[n];
						this.m_RayTracingRendererReference.Add(renderer2.GetInstanceID(), 1);
					}
				}
			}
			foreach (Renderer renderer3 in Object.FindObjectsOfType<Renderer>())
			{
				if (renderer3.enabled)
				{
					GameObject gameObject = renderer3.gameObject;
					if (!this.m_RayTracingRendererReference.ContainsKey(renderer3.GetInstanceID()) && !gameObject.TryGetComponent<ReflectionProbe>(out this.reflectionProbe))
					{
						this.AddInstanceToRAS(renderer3, flag, component.rayTracing.value, component.layerMask.value, component2.rayTracing.value, component2.layerMask.value, component3.rayTracing.value, component3.layerMask.value, component4.enable.value, component4.layerMask.value, component5.enable.value, component5.layerMask.value);
					}
				}
			}
			this.m_CurrentRAS.Build();
			this.m_ValidRayTracingState = true;
		}

		// Token: 0x0600044B RID: 1099 RVA: 0x00029334 File Offset: 0x00027534
		internal bool ValidRayTracingHistory(HDCamera hdCamera)
		{
			RTHandleProperties rthandleProperties = hdCamera.historyRTHandleProperties;
			if (rthandleProperties.previousViewportSize.x == hdCamera.actualWidth)
			{
				rthandleProperties = hdCamera.historyRTHandleProperties;
				return rthandleProperties.previousViewportSize.y == hdCamera.actualHeight;
			}
			return false;
		}

		// Token: 0x0600044C RID: 1100 RVA: 0x00029379 File Offset: 0x00027579
		internal int RayTracingFrameIndex(HDCamera hdCamera)
		{
			if (!hdCamera.IsTAAEnabled())
			{
				return this.m_FrameCount % 8;
			}
			return hdCamera.taaFrameIndex;
		}

		// Token: 0x0600044D RID: 1101 RVA: 0x00029394 File Offset: 0x00027594
		internal void BuildRayTracingLightCluster(CommandBuffer cmd, HDCamera hdCamera)
		{
			ScreenSpaceReflection component = hdCamera.volumeStack.GetComponent<ScreenSpaceReflection>();
			GlobalIllumination component2 = hdCamera.volumeStack.GetComponent<GlobalIllumination>();
			RecursiveRendering component3 = hdCamera.volumeStack.GetComponent<RecursiveRendering>();
			PathTracing component4 = hdCamera.volumeStack.GetComponent<PathTracing>();
			SubSurfaceScattering component5 = hdCamera.volumeStack.GetComponent<SubSurfaceScattering>();
			if (this.m_ValidRayTracingState && (component.rayTracing.value || component2.rayTracing.value || component3.enable.value || component4.enable.value || component5.rayTracing.value))
			{
				this.m_RayTracingLightCluster.EvaluateLightClusters(cmd, hdCamera, this.m_RayTracingLights);
				this.m_ValidRayTracingCluster = true;
			}
		}

		// Token: 0x0600044E RID: 1102 RVA: 0x00029442 File Offset: 0x00027642
		internal RayTracingAccelerationStructure RequestAccelerationStructure()
		{
			if (this.m_ValidRayTracingState)
			{
				return this.m_CurrentRAS;
			}
			return null;
		}

		// Token: 0x0600044F RID: 1103 RVA: 0x00029454 File Offset: 0x00027654
		internal HDRaytracingLightCluster RequestLightCluster()
		{
			if (this.m_ValidRayTracingCluster)
			{
				return this.m_RayTracingLightCluster;
			}
			return null;
		}

		// Token: 0x06000450 RID: 1104 RVA: 0x00029466 File Offset: 0x00027666
		internal static bool GatherRayTracingSupport(RenderPipelineSettings rpSetting)
		{
			return rpSetting.supportRayTracing && HDRenderPipeline.rayTracingSupportedBySystem;
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x06000451 RID: 1105 RVA: 0x00029477 File Offset: 0x00027677
		internal static bool rayTracingSupportedBySystem
		{
			get
			{
				return SystemInfo.supportsRayTracing;
			}
		}

		// Token: 0x06000452 RID: 1106 RVA: 0x0002947E File Offset: 0x0002767E
		internal BlueNoise GetBlueNoiseManager()
		{
			return this.m_BlueNoise;
		}

		// Token: 0x06000453 RID: 1107 RVA: 0x00029486 File Offset: 0x00027686
		internal RayCountManager GetRayCountManager()
		{
			return this.m_RayCountManager;
		}

		// Token: 0x06000454 RID: 1108 RVA: 0x0002948E File Offset: 0x0002768E
		internal HDTemporalFilter GetTemporalFilter()
		{
			return this.m_TemporalFilter;
		}

		// Token: 0x06000455 RID: 1109 RVA: 0x00029496 File Offset: 0x00027696
		internal HDSimpleDenoiser GetSimpleDenoiser()
		{
			return this.m_SimpleDenoiser;
		}

		// Token: 0x06000456 RID: 1110 RVA: 0x0002949E File Offset: 0x0002769E
		internal HDDiffuseDenoiser GetDiffuseDenoiser()
		{
			return this.m_DiffuseDenoiser;
		}

		// Token: 0x06000457 RID: 1111 RVA: 0x000294A6 File Offset: 0x000276A6
		internal HDReflectionDenoiser GetReflectionDenoiser()
		{
			return this.m_ReflectionDenoiser;
		}

		// Token: 0x06000458 RID: 1112 RVA: 0x000294AE File Offset: 0x000276AE
		internal bool GetRayTracingState()
		{
			return this.m_ValidRayTracingState;
		}

		// Token: 0x06000459 RID: 1113 RVA: 0x000294B6 File Offset: 0x000276B6
		internal bool GetRayTracingClusterState()
		{
			return this.m_ValidRayTracingCluster;
		}

		// Token: 0x0600045A RID: 1114 RVA: 0x000294C0 File Offset: 0x000276C0
		internal RTHandle GetRayTracingBuffer(InternalRayTracingBuffers bufferID)
		{
			switch (bufferID)
			{
			case InternalRayTracingBuffers.Distance:
				return this.m_RayTracingDistanceBuffer;
			case InternalRayTracingBuffers.Direction:
				return this.m_RayTracingDirectionBuffer;
			case InternalRayTracingBuffers.R0:
				return this.m_RayTracingIntermediateBufferR0;
			case InternalRayTracingBuffers.R1:
				return this.m_RayTracingIntermediateBufferR1;
			case InternalRayTracingBuffers.RG0:
				return this.m_RayTracingIntermediateBufferRG0;
			case InternalRayTracingBuffers.RGBA0:
				return this.m_RayTracingIntermediateBufferRGBA0;
			case InternalRayTracingBuffers.RGBA1:
				return this.m_RayTracingIntermediateBufferRGBA1;
			case InternalRayTracingBuffers.RGBA2:
				return this.m_RayTracingIntermediateBufferRGBA2;
			case InternalRayTracingBuffers.RGBA3:
				return this.m_RayTracingIntermediateBufferRGBA3;
			default:
				return null;
			}
		}

		// Token: 0x0600045B RID: 1115 RVA: 0x00029539 File Offset: 0x00027739
		internal static float GetPixelSpreadTangent(float fov, int width, int height)
		{
			return Mathf.Tan(fov * 0.017453292f * 0.5f) * 2f / (float)Mathf.Min(width, height);
		}

		// Token: 0x0600045C RID: 1116 RVA: 0x0002955C File Offset: 0x0002775C
		internal static float GetPixelSpreadAngle(float fov, int width, int height)
		{
			return Mathf.Atan(HDRenderPipeline.GetPixelSpreadTangent(fov, width, height));
		}

		// Token: 0x0600045D RID: 1117 RVA: 0x0002956C File Offset: 0x0002776C
		private void InitRecursiveRenderer()
		{
			this.m_RaytracingFlagStateBlock = new RenderStateBlock
			{
				depthState = new DepthState(false, CompareFunction.LessEqual),
				mask = RenderStateMask.Depth
			};
		}

		// Token: 0x0600045E RID: 1118 RVA: 0x0002959E File Offset: 0x0002779E
		private void ReleaseRecursiveRenderer()
		{
			if (this.m_RaytracingFlagMaterial != null)
			{
				CoreUtils.Destroy(this.m_RaytracingFlagMaterial);
			}
		}

		// Token: 0x0600045F RID: 1119 RVA: 0x000295BC File Offset: 0x000277BC
		private void EvaluateRaytracingMask(CullingResults cull, HDCamera hdCamera, CommandBuffer cmd, ScriptableRenderContext renderContext, RTHandle flagBuffer)
		{
			CoreUtils.SetRenderTarget(cmd, flagBuffer, ClearFlag.Color, Color.black, 0, CubemapFace.Unknown, -1);
			CoreUtils.SetRenderTarget(cmd, flagBuffer, this.m_SharedRTManager.GetDepthStencilBuffer(false), 0, CubemapFace.Unknown, -1);
			renderContext.ExecuteCommandBuffer(cmd);
			cmd.Clear();
			SortingSettings sortingSettings = new SortingSettings(hdCamera.camera)
			{
				criteria = SortingCriteria.None
			};
			FilteringSettings filteringSettings = new FilteringSettings(new RenderQueueRange?(HDRenderQueue.k_RenderQueue_AllOpaqueRaytracing), -1, uint.MaxValue, 0)
			{
				excludeMotionVectorObjects = false
			};
			DrawingSettings drawingSettings = new DrawingSettings(HDShaderPassNames.s_EmptyName, sortingSettings)
			{
				perObjectData = PerObjectData.None
			};
			this.m_RaytracingFlagMaterial.renderQueue = 2520;
			drawingSettings.SetShaderPassName(0, this.raytracingPassID);
			drawingSettings.overrideMaterial = this.m_RaytracingFlagMaterial;
			drawingSettings.overrideMaterialPassIndex = 0;
			renderContext.DrawRenderers(cull, ref drawingSettings, ref filteringSettings);
			filteringSettings.renderQueueRange = HDRenderQueue.k_RenderQueue_AllTransparentRaytracing;
			this.m_RaytracingFlagMaterial.renderQueue = 3900;
			drawingSettings.SetShaderPassName(0, this.raytracingPassID);
			drawingSettings.overrideMaterial = this.m_RaytracingFlagMaterial;
			drawingSettings.overrideMaterialPassIndex = 0;
			renderContext.DrawRenderers(cull, ref drawingSettings, ref filteringSettings);
		}

		// Token: 0x06000460 RID: 1120 RVA: 0x000296D8 File Offset: 0x000278D8
		private void RaytracingRecursiveRender(HDCamera hdCamera, CommandBuffer cmd, ScriptableRenderContext renderContext, CullingResults cull)
		{
			RecursiveRendering component = hdCamera.volumeStack.GetComponent<RecursiveRendering>();
			if (!hdCamera.frameSettings.IsEnabled(FrameSettingsField.RayTracing) || !component.enable.value)
			{
				return;
			}
			RayTracingShader forwardRaytracing = this.m_Asset.renderPipelineRayTracingResources.forwardRaytracing;
			Shader raytracingFlagMask = this.m_Asset.renderPipelineRayTracingResources.raytracingFlagMask;
			LightCluster component2 = hdCamera.volumeStack.GetComponent<LightCluster>();
			RayTracingSettings component3 = hdCamera.volumeStack.GetComponent<RayTracingSettings>();
			RayTracingAccelerationStructure rayTracingAccelerationStructure = this.RequestAccelerationStructure();
			HDRaytracingLightCluster hdraytracingLightCluster = this.RequestLightCluster();
			RTHandle rayTracingBuffer = this.GetRayTracingBuffer(InternalRayTracingBuffers.R0);
			if (this.m_RaytracingFlagMaterial == null)
			{
				this.m_RaytracingFlagMaterial = CoreUtils.CreateEngineMaterial(raytracingFlagMask);
			}
			this.EvaluateRaytracingMask(cull, hdCamera, cmd, renderContext, rayTracingBuffer);
			cmd.SetRayTracingShaderPass(forwardRaytracing, "ForwardDXR");
			cmd.SetRayTracingAccelerationStructure(forwardRaytracing, HDShaderIDs._RaytracingAccelerationStructureName, rayTracingAccelerationStructure);
			cmd.SetRayTracingTextureParam(forwardRaytracing, HDShaderIDs._OwenScrambledTexture, this.m_Asset.renderPipelineResources.textures.owenScrambledRGBATex);
			cmd.SetRayTracingTextureParam(forwardRaytracing, HDShaderIDs._ScramblingTexture, this.m_Asset.renderPipelineResources.textures.scramblingTex);
			cmd.SetGlobalFloat(HDShaderIDs._RaytracingRayBias, component3.rayBias.value);
			cmd.SetGlobalFloat(HDShaderIDs._RaytracingRayMaxLength, component.rayLength.value);
			cmd.SetGlobalFloat(HDShaderIDs._RaytracingMaxRecursion, (float)component.maxDepth.value);
			cmd.SetGlobalFloat(HDShaderIDs._RaytracingCameraNearPlane, hdCamera.camera.nearClipPlane);
			cmd.SetRayTracingTextureParam(forwardRaytracing, HDShaderIDs._RaytracingFlagMask, rayTracingBuffer);
			cmd.SetRayTracingTextureParam(forwardRaytracing, HDShaderIDs._DepthTexture, this.m_SharedRTManager.GetDepthStencilBuffer(false));
			cmd.SetRayTracingTextureParam(forwardRaytracing, HDShaderIDs._CameraColorTextureRW, this.m_CameraColorBuffer);
			RayCountManager rayCountManager = this.GetRayCountManager();
			cmd.SetRayTracingIntParam(forwardRaytracing, HDShaderIDs._RayCountEnabled, rayCountManager.RayCountIsEnabled());
			cmd.SetRayTracingTextureParam(forwardRaytracing, HDShaderIDs._RayCountTexture, rayCountManager.GetRayCountTexture());
			cmd.SetGlobalFloat(HDShaderIDs._RaytracingPixelSpreadAngle, HDRenderPipeline.GetPixelSpreadAngle(hdCamera.camera.fieldOfView, hdCamera.actualWidth, hdCamera.actualHeight));
			cmd.SetGlobalBuffer(HDShaderIDs._RaytracingLightCluster, hdraytracingLightCluster.GetCluster());
			cmd.SetGlobalBuffer(HDShaderIDs._LightDatasRT, hdraytracingLightCluster.GetLightDatas());
			cmd.SetGlobalVector(HDShaderIDs._MinClusterPos, hdraytracingLightCluster.GetMinClusterPos());
			cmd.SetGlobalVector(HDShaderIDs._MaxClusterPos, hdraytracingLightCluster.GetMaxClusterPos());
			cmd.SetGlobalInt(HDShaderIDs._LightPerCellCount, component2.maxNumLightsPercell.value);
			cmd.SetGlobalInt(HDShaderIDs._PunctualLightCountRT, hdraytracingLightCluster.GetPunctualLightCount());
			cmd.SetGlobalInt(HDShaderIDs._AreaLightCountRT, hdraytracingLightCluster.GetAreaLightCount());
			cmd.SetGlobalBuffer(HDShaderIDs._DirectionalLightDatas, this.m_LightLoopLightData.directionalLightData);
			cmd.SetGlobalInt(HDShaderIDs._DirectionalLightCount, this.m_lightList.directionalLights.Count);
			cmd.SetRayTracingTextureParam(forwardRaytracing, HDShaderIDs._SkyTexture, this.m_SkyManager.GetSkyReflection(hdCamera));
			RTHandle rayTracingBuffer2 = this.GetRayTracingBuffer(InternalRayTracingBuffers.RGBA0);
			cmd.SetRayTracingTextureParam(forwardRaytracing, HDShaderIDs._RaytracingPrimaryDebug, rayTracingBuffer2);
			cmd.DispatchRays(forwardRaytracing, "RayGenRenderer", (uint)hdCamera.actualWidth, (uint)hdCamera.actualHeight, (uint)hdCamera.viewCount, null);
			(RenderPipelineManager.currentPipeline as HDRenderPipeline).PushFullScreenDebugTexture(hdCamera, cmd, rayTracingBuffer2, FullScreenDebugMode.RecursiveRayTracing);
		}

		// Token: 0x06000461 RID: 1121 RVA: 0x00002646 File Offset: 0x00000846
		private void InitRayTracedReflections()
		{
		}

		// Token: 0x06000462 RID: 1122 RVA: 0x00029A20 File Offset: 0x00027C20
		private static RTHandle ReflectionHistoryBufferAllocatorFunction(string viewName, int frameIndex, RTHandleSystem rtHandleSystem)
		{
			return rtHandleSystem.Alloc(Vector2.one, TextureXR.slices, DepthBits.None, GraphicsFormat.R16G16B16A16_SFloat, FilterMode.Point, TextureWrapMode.Repeat, TextureXR.dimension, true, false, false, false, 1, 0f, false, false, false, RenderTextureMemoryless.None, string.Format("ReflectionHistoryBuffer{0}", frameIndex));
		}

		// Token: 0x06000463 RID: 1123 RVA: 0x00002646 File Offset: 0x00000846
		private void ReleaseRayTracedReflections()
		{
		}

		// Token: 0x06000464 RID: 1124 RVA: 0x00029A68 File Offset: 0x00027C68
		private void RenderRayTracedReflections(HDCamera hdCamera, CommandBuffer cmd, RTHandle outputTexture, ScriptableRenderContext renderContext, int frameCount)
		{
			RayTracingMode value = hdCamera.volumeStack.GetComponent<ScreenSpaceReflection>().mode.value;
			if (value == RayTracingMode.Performance)
			{
				this.RenderReflectionsPerformance(hdCamera, cmd, outputTexture, renderContext, frameCount);
				return;
			}
			if (value != RayTracingMode.Quality)
			{
				return;
			}
			this.RenderReflectionsQuality(hdCamera, cmd, outputTexture, renderContext, frameCount);
		}

		// Token: 0x06000465 RID: 1125 RVA: 0x00029AB0 File Offset: 0x00027CB0
		private void BindRayTracedReflectionData(CommandBuffer cmd, HDCamera hdCamera, RayTracingShader reflectionShader, ScreenSpaceReflection settings, LightCluster lightClusterSettings, RayTracingSettings rtSettings, RTHandle outputLightingBuffer, RTHandle outputHitPointBuffer)
		{
			RayTracingAccelerationStructure rayTracingAccelerationStructure = this.RequestAccelerationStructure();
			HDRaytracingLightCluster hdraytracingLightCluster = this.RequestLightCluster();
			BlueNoise blueNoiseManager = this.GetBlueNoiseManager();
			cmd.SetRayTracingShaderPass(reflectionShader, "IndirectDXR");
			cmd.SetRayTracingAccelerationStructure(reflectionShader, HDShaderIDs._RaytracingAccelerationStructureName, rayTracingAccelerationStructure);
			cmd.SetRayTracingFloatParams(reflectionShader, HDShaderIDs._RaytracingIntensityClamp, new float[] { settings.clampValue.value });
			cmd.SetRayTracingFloatParams(reflectionShader, HDShaderIDs._RaytracingReflectionMinSmoothness, new float[] { settings.minSmoothness.value });
			cmd.SetRayTracingFloatParams(reflectionShader, HDShaderIDs._RaytracingReflectionSmoothnessFadeStart, new float[] { settings.smoothnessFadeStart.value });
			cmd.SetRayTracingIntParams(reflectionShader, HDShaderIDs._RaytracingIncludeSky, new int[] { settings.reflectSky.value ? 1 : 0 });
			cmd.SetGlobalFloat(HDShaderIDs._RaytracingRayBias, rtSettings.rayBias.value);
			cmd.SetGlobalFloat(HDShaderIDs._RaytracingRayMaxLength, settings.rayLength.value);
			cmd.SetRayTracingIntParams(reflectionShader, HDShaderIDs._RaytracingNumSamples, new int[] { settings.sampleCount.value });
			int num = this.RayTracingFrameIndex(hdCamera);
			cmd.SetRayTracingIntParam(reflectionShader, HDShaderIDs._RaytracingFrameIndex, num);
			blueNoiseManager.BindDitheredRNGData8SPP(cmd);
			cmd.SetRayTracingTextureParam(reflectionShader, HDShaderIDs._SsrLightingTextureRW, outputLightingBuffer);
			cmd.SetRayTracingTextureParam(reflectionShader, HDShaderIDs._SsrHitPointTexture, outputHitPointBuffer);
			cmd.SetRayTracingTextureParam(reflectionShader, HDShaderIDs._DepthTexture, this.m_SharedRTManager.GetDepthStencilBuffer(false));
			cmd.SetRayTracingTextureParam(reflectionShader, HDShaderIDs._NormalBufferTexture, this.m_SharedRTManager.GetNormalBuffer(false));
			cmd.SetGlobalTexture(HDShaderIDs._StencilTexture, this.sharedRTManager.GetDepthStencilBuffer(false), RenderTextureSubElement.Stencil);
			cmd.SetRayTracingIntParams(reflectionShader, HDShaderIDs._SsrStencilBit, new int[] { 8 });
			RayCountManager rayCountManager = this.GetRayCountManager();
			cmd.SetRayTracingIntParam(reflectionShader, HDShaderIDs._RayCountEnabled, rayCountManager.RayCountIsEnabled());
			cmd.SetRayTracingTextureParam(reflectionShader, HDShaderIDs._RayCountTexture, rayCountManager.GetRayCountTexture());
			cmd.SetGlobalFloat(HDShaderIDs._RaytracingPixelSpreadAngle, HDRenderPipeline.GetPixelSpreadAngle(hdCamera.camera.fieldOfView, hdCamera.actualWidth, hdCamera.actualHeight));
			hdraytracingLightCluster.BindLightClusterData(cmd);
			cmd.SetGlobalBuffer(HDShaderIDs._DirectionalLightDatas, this.m_LightLoopLightData.directionalLightData);
			cmd.SetGlobalInt(HDShaderIDs._DirectionalLightCount, this.m_lightList.directionalLights.Count);
			RenderTargetIdentifier renderTargetIdentifier = ((hdCamera.frameSettings.litShaderMode == LitShaderMode.Deferred) ? this.m_GbufferManager.GetBuffersRTI()[2] : TextureXR.GetBlackTexture());
			cmd.SetRayTracingTextureParam(reflectionShader, HDShaderIDs._SsrClearCoatMaskTexture, renderTargetIdentifier);
			cmd.SetGlobalInt(HDShaderIDs._RaytracingMaxRecursion, settings.bounceCount.value);
			cmd.SetRayTracingTextureParam(reflectionShader, HDShaderIDs._SkyTexture, this.m_SkyManager.GetSkyReflection(hdCamera));
		}

		// Token: 0x06000466 RID: 1126 RVA: 0x00029D78 File Offset: 0x00027F78
		private HDRenderPipeline.DeferredLightingRTParameters PrepareReflectionDeferredLightingRTParameters(HDCamera hdCamera)
		{
			HDRenderPipeline.DeferredLightingRTParameters deferredLightingRTParameters = default(HDRenderPipeline.DeferredLightingRTParameters);
			ScreenSpaceReflection component = hdCamera.volumeStack.GetComponent<ScreenSpaceReflection>();
			RayTracingSettings component2 = hdCamera.volumeStack.GetComponent<RayTracingSettings>();
			this.CheckBinningBuffersSize(hdCamera);
			deferredLightingRTParameters.rayBinning = true;
			deferredLightingRTParameters.layerMask.value = 16;
			deferredLightingRTParameters.rayBias = component2.rayBias.value;
			deferredLightingRTParameters.maxRayLength = component.rayLength.value;
			deferredLightingRTParameters.clampValue = component.clampValue.value;
			deferredLightingRTParameters.includeSky = component.reflectSky.value;
			deferredLightingRTParameters.diffuseLightingOnly = false;
			deferredLightingRTParameters.halfResolution = !component.fullResolution.value;
			deferredLightingRTParameters.rayCountFlag = this.m_RayCountManager.RayCountIsEnabled();
			deferredLightingRTParameters.rayCountType = 7;
			deferredLightingRTParameters.preExpose = false;
			deferredLightingRTParameters.width = hdCamera.actualWidth;
			deferredLightingRTParameters.height = hdCamera.actualHeight;
			deferredLightingRTParameters.viewCount = hdCamera.viewCount;
			deferredLightingRTParameters.fov = hdCamera.camera.fieldOfView;
			deferredLightingRTParameters.rayBinResult = this.m_RayBinResult;
			deferredLightingRTParameters.rayBinSizeResult = this.m_RayBinSizeResult;
			deferredLightingRTParameters.accelerationStructure = this.RequestAccelerationStructure();
			deferredLightingRTParameters.lightCluster = this.RequestLightCluster();
			deferredLightingRTParameters.gBufferRaytracingRT = this.m_Asset.renderPipelineRayTracingResources.gBufferRaytracingRT;
			deferredLightingRTParameters.deferredRaytracingCS = this.m_Asset.renderPipelineRayTracingResources.deferredRaytracingCS;
			deferredLightingRTParameters.rayBinningCS = this.m_Asset.renderPipelineRayTracingResources.rayBinningCS;
			if (deferredLightingRTParameters.viewCount > 1 && deferredLightingRTParameters.rayBinning)
			{
				deferredLightingRTParameters.rayBinning = false;
				Debug.LogWarning("Ray binning is not supported with XR single-pass rendering!");
			}
			return deferredLightingRTParameters;
		}

		// Token: 0x06000467 RID: 1127 RVA: 0x00029F20 File Offset: 0x00028120
		private void RenderReflectionsPerformance(HDCamera hdCamera, CommandBuffer cmd, RTHandle outputTexture, ScriptableRenderContext renderContext, int frameCount)
		{
			BlueNoise blueNoiseManager = this.GetBlueNoiseManager();
			RayTracingShader reflectionRaytracingRT = this.m_Asset.renderPipelineRayTracingResources.reflectionRaytracingRT;
			ComputeShader reflectionRaytracingCS = this.m_Asset.renderPipelineRayTracingResources.reflectionRaytracingCS;
			ComputeShader reflectionBilateralFilterCS = this.m_Asset.renderPipelineRayTracingResources.reflectionBilateralFilterCS;
			RTHandle rayTracingBuffer = this.GetRayTracingBuffer(InternalRayTracingBuffers.RGBA0);
			RTHandle rayTracingBuffer2 = this.GetRayTracingBuffer(InternalRayTracingBuffers.RGBA1);
			ScreenSpaceReflection component = hdCamera.volumeStack.GetComponent<ScreenSpaceReflection>();
			hdCamera.volumeStack.GetComponent<LightCluster>();
			hdCamera.volumeStack.GetComponent<RayTracingSettings>();
			int actualWidth = hdCamera.actualWidth;
			int actualHeight = hdCamera.actualHeight;
			int num = 8;
			using (new ProfilingScope(cmd, ProfilingSampler.Get<HDProfileId>(HDProfileId.RaytracingIntegrateReflection)))
			{
				int num2 = reflectionRaytracingCS.FindKernel(component.fullResolution.value ? "RaytracingReflectionsFullRes" : "RaytracingReflectionsHalfRes");
				blueNoiseManager.BindDitheredRNGData8SPP(cmd);
				cmd.SetComputeTextureParam(reflectionRaytracingCS, num2, HDShaderIDs._DepthTexture, this.m_SharedRTManager.GetDepthStencilBuffer(false));
				cmd.SetComputeTextureParam(reflectionRaytracingCS, num2, HDShaderIDs._NormalBufferTexture, this.m_SharedRTManager.GetNormalBuffer(false));
				RenderTargetIdentifier renderTargetIdentifier = ((hdCamera.frameSettings.litShaderMode == LitShaderMode.Deferred) ? this.m_GbufferManager.GetBuffersRTI()[2] : TextureXR.GetBlackTexture());
				cmd.SetComputeTextureParam(reflectionRaytracingCS, num2, HDShaderIDs._SsrClearCoatMaskTexture, renderTargetIdentifier);
				cmd.SetComputeIntParam(reflectionRaytracingCS, HDShaderIDs._SsrStencilBit, 8);
				cmd.SetComputeTextureParam(reflectionRaytracingCS, num2, HDShaderIDs._StencilTexture, this.m_SharedRTManager.GetDepthStencilBuffer(false), 0, RenderTextureSubElement.Stencil);
				cmd.SetComputeFloatParam(reflectionRaytracingCS, HDShaderIDs._RaytracingIntensityClamp, component.clampValue.value);
				cmd.SetComputeFloatParam(reflectionRaytracingCS, HDShaderIDs._RaytracingReflectionMinSmoothness, component.minSmoothness.value);
				cmd.SetComputeIntParam(reflectionRaytracingCS, HDShaderIDs._RaytracingIncludeSky, component.reflectSky.value ? 1 : 0);
				int num3 = this.RayTracingFrameIndex(hdCamera);
				cmd.SetComputeIntParam(reflectionRaytracingCS, HDShaderIDs._RaytracingFrameIndex, num3);
				cmd.SetComputeTextureParam(reflectionRaytracingCS, num2, HDShaderIDs._RaytracingDirectionBuffer, rayTracingBuffer2);
				int num4;
				int num5;
				if (component.fullResolution.value)
				{
					num4 = (actualWidth + (num - 1)) / num;
					num5 = (actualHeight + (num - 1)) / num;
				}
				else
				{
					num4 = (actualWidth / 2 + (num - 1)) / num;
					num5 = (actualHeight / 2 + (num - 1)) / num;
				}
				cmd.DispatchCompute(reflectionRaytracingCS, num2, num4, num5, hdCamera.viewCount);
				HDRenderPipeline.DeferredLightingRTParameters deferredLightingRTParameters = this.PrepareReflectionDeferredLightingRTParameters(hdCamera);
				HDRenderPipeline.DeferredLightingRTResources deferredLightingRTResources = this.PrepareDeferredLightingRTResources(hdCamera, rayTracingBuffer2, rayTracingBuffer);
				HDRenderPipeline.RenderRaytracingDeferredLighting(cmd, in deferredLightingRTParameters, in deferredLightingRTResources);
				if (component.fullResolution.value)
				{
					num2 = reflectionBilateralFilterCS.FindKernel("ReflectionIntegrationUpscaleFullRes");
				}
				else
				{
					num2 = reflectionBilateralFilterCS.FindKernel("ReflectionIntegrationUpscaleHalfRes");
				}
				cmd.SetComputeTextureParam(reflectionBilateralFilterCS, num2, HDShaderIDs._SsrLightingTextureRW, rayTracingBuffer);
				cmd.SetComputeTextureParam(reflectionBilateralFilterCS, num2, HDShaderIDs._SsrHitPointTexture, rayTracingBuffer2);
				cmd.SetComputeTextureParam(reflectionBilateralFilterCS, num2, HDShaderIDs._DepthTexture, this.m_SharedRTManager.GetDepthStencilBuffer(false));
				cmd.SetComputeTextureParam(reflectionBilateralFilterCS, num2, HDShaderIDs._NormalBufferTexture, this.m_SharedRTManager.GetNormalBuffer(false));
				cmd.SetComputeTextureParam(reflectionBilateralFilterCS, num2, HDShaderIDs._BlueNoiseTexture, blueNoiseManager.textureArray16RGB);
				cmd.SetComputeTextureParam(reflectionBilateralFilterCS, num2, "_RaytracingReflectionTexture", outputTexture);
				cmd.SetComputeTextureParam(reflectionBilateralFilterCS, num2, HDShaderIDs._ScramblingTexture, this.m_Asset.renderPipelineResources.textures.scramblingTex);
				cmd.SetComputeIntParam(reflectionBilateralFilterCS, HDShaderIDs._SpatialFilterRadius, component.upscaleRadius.value);
				cmd.SetComputeIntParam(reflectionBilateralFilterCS, HDShaderIDs._RaytracingDenoiseRadius, component.denoise.value ? component.denoiserRadius.value : 0);
				cmd.SetComputeFloatParam(reflectionBilateralFilterCS, HDShaderIDs._RaytracingReflectionMinSmoothness, component.minSmoothness.value);
				cmd.SetComputeFloatParam(reflectionBilateralFilterCS, HDShaderIDs._RaytracingReflectionSmoothnessFadeStart, component.smoothnessFadeStart.value);
				num4 = (actualWidth + (num - 1)) / num;
				num5 = (actualHeight + (num - 1)) / num;
				renderTargetIdentifier = ((hdCamera.frameSettings.litShaderMode == LitShaderMode.Deferred) ? this.m_GbufferManager.GetBuffersRTI()[2] : TextureXR.GetBlackTexture());
				cmd.SetComputeTextureParam(reflectionBilateralFilterCS, num2, HDShaderIDs._SsrClearCoatMaskTexture, renderTargetIdentifier);
				cmd.DispatchCompute(reflectionBilateralFilterCS, num2, num4, num5, hdCamera.viewCount);
			}
			using (new ProfilingScope(cmd, ProfilingSampler.Get<HDProfileId>(HDProfileId.RaytracingFilterReflection)))
			{
				if (component.denoise.value)
				{
					RTHandle rthandle = hdCamera.GetCurrentFrameRT(11) ?? hdCamera.AllocHistoryFrameRT(11, new Func<string, int, RTHandleSystem, RTHandle>(HDRenderPipeline.ReflectionHistoryBufferAllocatorFunction), 1);
					float num6 = 1f;
					num6 *= (this.ValidRayTracingHistory(hdCamera) ? 1f : 0f);
					this.GetReflectionDenoiser().DenoiseBuffer(cmd, hdCamera, component.denoiserRadius.value, outputTexture, rthandle, rayTracingBuffer, num6);
					HDUtils.BlitCameraTexture(cmd, rayTracingBuffer, outputTexture, 0f, false);
				}
			}
		}

		// Token: 0x06000468 RID: 1128 RVA: 0x0002A434 File Offset: 0x00028634
		private void RenderReflectionsQuality(HDCamera hdCamera, CommandBuffer cmd, RTHandle outputTexture, ScriptableRenderContext renderContext, int frameCount)
		{
			ComputeShader reflectionBilateralFilterCS = this.m_Asset.renderPipelineRayTracingResources.reflectionBilateralFilterCS;
			RayTracingShader reflectionRaytracingRT = this.m_Asset.renderPipelineRayTracingResources.reflectionRaytracingRT;
			RTHandle rayTracingBuffer = this.GetRayTracingBuffer(InternalRayTracingBuffers.RGBA0);
			RTHandle rayTracingBuffer2 = this.GetRayTracingBuffer(InternalRayTracingBuffers.RGBA1);
			ScreenSpaceReflection component = hdCamera.volumeStack.GetComponent<ScreenSpaceReflection>();
			LightCluster component2 = hdCamera.volumeStack.GetComponent<LightCluster>();
			RayTracingSettings component3 = hdCamera.volumeStack.GetComponent<RayTracingSettings>();
			using (new ProfilingScope(cmd, ProfilingSampler.Get<HDProfileId>(HDProfileId.RaytracingIntegrateReflection)))
			{
				this.BindRayTracedReflectionData(cmd, hdCamera, reflectionRaytracingRT, component, component2, component3, rayTracingBuffer, rayTracingBuffer2);
				CoreUtils.SetKeyword(cmd, "MULTI_BOUNCE_INDIRECT", component.bounceCount.value > 1);
				CoreUtils.SetKeyword(cmd, "DIFFUSE_LIGHTING_ONLY", false);
				cmd.DispatchRays(reflectionRaytracingRT, "RayGenIntegration", (uint)hdCamera.actualWidth, (uint)hdCamera.actualHeight, (uint)hdCamera.viewCount, null);
				CoreUtils.SetKeyword(cmd, "MULTI_BOUNCE_INDIRECT", false);
			}
			using (new ProfilingScope(cmd, ProfilingSampler.Get<HDProfileId>(HDProfileId.RaytracingFilterReflection)))
			{
				if (component.denoise.value)
				{
					RTHandle rthandle = hdCamera.GetCurrentFrameRT(11) ?? hdCamera.AllocHistoryFrameRT(11, new Func<string, int, RTHandleSystem, RTHandle>(HDRenderPipeline.ReflectionHistoryBufferAllocatorFunction), 1);
					float num = 1f;
					num *= (this.ValidRayTracingHistory(hdCamera) ? 1f : 0f);
					this.GetReflectionDenoiser().DenoiseBuffer(cmd, hdCamera, component.denoiserRadius.value, rayTracingBuffer, rthandle, outputTexture, num);
				}
				else
				{
					HDUtils.BlitCameraTexture(cmd, rayTracingBuffer, outputTexture, 0f, false);
				}
			}
		}

		// Token: 0x06000469 RID: 1129 RVA: 0x0002A5D4 File Offset: 0x000287D4
		[CompilerGenerated]
		private RTHandle <AllocateVolumetricHistoryBuffers>g__HistoryBufferAllocatorFunction|294_0(string viewName, int frameIndex, RTHandleSystem rtHandleSystem)
		{
			frameIndex &= 1;
			int num = HDRenderPipeline.ComputeVBufferSliceCount(this.volumetricLightingPreset);
			return rtHandleSystem.Alloc(new ScaleFunc(this.ComputeVBufferResolutionXY), num, DepthBits.None, GraphicsFormat.R16G16B16A16_SFloat, FilterMode.Point, TextureWrapMode.Repeat, TextureDimension.Tex3D, true, false, true, false, 1, 0f, false, false, false, RenderTextureMemoryless.None, string.Format("{0}_VBufferHistory{1}", viewName, frameIndex));
		}

		// Token: 0x0600046C RID: 1132 RVA: 0x0002A74B File Offset: 0x0002894B
		[CompilerGenerated]
		private void <Dispose>g__DisposeProbeCameraPool|636_1()
		{
			this.m_ProbeCameraCache.Dispose();
			this.m_ProbeCameraCache = null;
		}

		// Token: 0x0600046D RID: 1133 RVA: 0x0002A760 File Offset: 0x00028960
		[CompilerGenerated]
		private void <Render>g__AddVisibleProbeVisibleIndexIfUpdateIsRequired|645_2(HDProbe probe, int visibleInIndex, ref HDRenderPipeline.<>c__DisplayClass645_0 A_3, ref HDRenderPipeline.<>c__DisplayClass645_1 A_4, ref HDRenderPipeline.<>c__DisplayClass645_2 A_5)
		{
			if (!probe.requiresRealtimeUpdate)
			{
				return;
			}
			probe.SetIsRendered(this.m_FrameCount);
			float num = HDRenderPipeline.<Render>g__ComputeVisibility|645_3(visibleInIndex, probe, ref A_4);
			List<ValueTuple<int, float>> list;
			if (!A_5.renderRequestIndicesWhereTheProbeIsVisible.TryGetValue(probe, out list))
			{
				list = ListPool<ValueTuple<int, float>>.Get();
				A_5.renderRequestIndicesWhereTheProbeIsVisible.Add(probe, list);
			}
			if (!list.Contains(new ValueTuple<int, float>(visibleInIndex, num)))
			{
				list.Add(new ValueTuple<int, float>(visibleInIndex, num));
			}
		}

		// Token: 0x0600046E RID: 1134 RVA: 0x0002A7D0 File Offset: 0x000289D0
		[CompilerGenerated]
		internal static float <Render>g__ComputeVisibility|645_3(int visibleInIndex, HDProbe visibleProbe, ref HDRenderPipeline.<>c__DisplayClass645_1 A_2)
		{
			Transform transform = A_2.renderRequests[visibleInIndex].hdCamera.camera.transform;
			return HDUtils.ComputeWeightedLinearFadeDistance(visibleProbe.transform.position, transform.position, visibleProbe.weight, visibleProbe.fadeDistance);
		}

		// Token: 0x0600046F RID: 1135 RVA: 0x0002A81C File Offset: 0x00028A1C
		[CompilerGenerated]
		private void <Render>g__AddHDProbeRenderRequests|645_1(HDProbe visibleProbe, Transform viewerTransform, [TupleElementNames(new string[] { "index", "weight" })] List<ValueTuple<int, float>> visibilities, ulong overrideSceneCullingMask, Camera parentCamera, float referenceFieldOfView, float referenceAspect, ref HDRenderPipeline.<>c__DisplayClass645_0 A_8, ref HDRenderPipeline.<>c__DisplayClass645_1 A_9, ref HDRenderPipeline.<>c__DisplayClass645_3 A_10, ref HDRenderPipeline.<>c__DisplayClass645_4 A_11)
		{
			ProbeCapturePositionSettings probeCapturePositionSettings = ProbeCapturePositionSettings.ComputeFrom(visibleProbe, viewerTransform);
			A_10.cameraSettings.Clear();
			A_11.cameraPositionSettings.Clear();
			HDRenderUtilities.GenerateRenderingSettingsFor(visibleProbe.settings, probeCapturePositionSettings, A_10.cameraSettings, A_11.cameraPositionSettings, overrideSceneCullingMask, false, referenceFieldOfView, referenceAspect);
			ProbeSettings.ProbeType type = visibleProbe.type;
			if (type != ProbeSettings.ProbeType.ReflectionProbe)
			{
				if (type == ProbeSettings.ProbeType.PlanarProbe)
				{
					int resolution = (int)visibleProbe.resolution;
					if (visibleProbe.realtimeTexture == null || visibleProbe.realtimeTexture.width != resolution)
					{
						visibleProbe.SetTexture(ProbeSettings.Mode.Realtime, HDRenderUtilities.CreatePlanarProbeRenderTarget(resolution));
					}
					for (int i = 0; i < A_10.cameraSettings.Count; i++)
					{
						CameraSettings cameraSettings = A_10.cameraSettings[i];
						if (cameraSettings.volumes.anchorOverride == null)
						{
							cameraSettings.volumes.anchorOverride = viewerTransform;
							A_10.cameraSettings[i] = cameraSettings;
						}
					}
				}
			}
			else
			{
				int reflectionCubemapSize = (int)((HDRenderPipeline)RenderPipelineManager.currentPipeline).currentPlatformRenderPipelineSettings.lightLoopSettings.reflectionCubemapSize;
				if (visibleProbe.realtimeTexture == null || visibleProbe.realtimeTexture.width != reflectionCubemapSize)
				{
					visibleProbe.SetTexture(ProbeSettings.Mode.Realtime, HDRenderUtilities.CreateReflectionProbeRenderTarget(reflectionCubemapSize));
				}
			}
			for (int j = 0; j < A_10.cameraSettings.Count; j++)
			{
				Camera orCreate = this.m_ProbeCameraCache.GetOrCreate(new ValueTuple<Transform, HDProbe, int>(viewerTransform, visibleProbe, j), this.m_FrameCount);
				HDAdditionalCameraData hdadditionalCameraData = orCreate.GetComponent<HDAdditionalCameraData>();
				if (hdadditionalCameraData == null)
				{
					hdadditionalCameraData = orCreate.gameObject.AddComponent<HDAdditionalCameraData>();
				}
				hdadditionalCameraData.hasPersistentHistory = true;
				orCreate.targetTexture = visibleProbe.realtimeTexture;
				orCreate.gameObject.hideFlags = HideFlags.HideAndDontSave;
				orCreate.gameObject.SetActive(false);
				orCreate.name = visibleProbe.probeName[j];
				orCreate.ApplySettings(A_10.cameraSettings[j]);
				orCreate.ApplySettings(A_11.cameraPositionSettings[j]);
				orCreate.cameraType = CameraType.Reflection;
				orCreate.pixelRect = new Rect(0f, 0f, (float)visibleProbe.realtimeTexture.width, (float)visibleProbe.realtimeTexture.height);
				HDRenderPipeline.HDCullingResults hdcullingResults = UnsafeGenericPool<HDRenderPipeline.HDCullingResults>.Get();
				hdcullingResults.Reset();
				HDAdditionalCameraData hdadditionalCameraData2;
				HDCamera hdcamera;
				ScriptableCullingParameters scriptableCullingParameters;
				if (!this.TryCalculateFrameParameters(orCreate, this.m_XRSystem.emptyPass, out hdadditionalCameraData2, out hdcamera, out scriptableCullingParameters) || !HDRenderPipeline.TryCull(orCreate, hdcamera, A_8.renderContext, this.m_SkyManager, scriptableCullingParameters, this.m_Asset, ref hdcullingResults))
				{
					UnsafeGenericPool<HDRenderPipeline.HDCullingResults>.Release(hdcullingResults);
				}
				else
				{
					hdcamera.parentCamera = parentCamera;
					HDAdditionalCameraData hdadditionalCameraData3;
					orCreate.TryGetComponent<HDAdditionalCameraData>(out hdadditionalCameraData3);
					hdadditionalCameraData3.flipYMode = ((visibleProbe.type == ProbeSettings.ProbeType.ReflectionProbe) ? HDAdditionalCameraData.FlipYMode.ForceFlipY : HDAdditionalCameraData.FlipYMode.Automatic);
					if (!visibleProbe.realtimeTexture.IsCreated())
					{
						visibleProbe.realtimeTexture.Create();
					}
					visibleProbe.SetRenderData(ProbeSettings.Mode.Realtime, new HDProbe.RenderData(orCreate.worldToCameraMatrix, orCreate.projectionMatrix, orCreate.transform.position, orCreate.transform.rotation, A_10.cameraSettings[j].frustum.fieldOfView, A_10.cameraSettings[j].frustum.aspect));
					HDRenderPipeline.RenderRequest renderRequest = new HDRenderPipeline.RenderRequest
					{
						hdCamera = hdcamera,
						cullingResults = hdcullingResults,
						clearCameraSettings = true,
						dependsOnRenderRequestIndices = ListPool<int>.Get(),
						index = A_9.renderRequests.Count,
						cameraSettings = A_10.cameraSettings[j]
					};
					visibleProbe.realtimeTexture.IncrementUpdateCount();
					if (A_10.cameraSettings.Count > 1)
					{
						CubemapFace cubemapFace = (CubemapFace)j;
						renderRequest.target = new HDRenderPipeline.RenderRequest.Target
						{
							copyToTarget = visibleProbe.realtimeTexture,
							face = cubemapFace
						};
					}
					else
					{
						renderRequest.target = new HDRenderPipeline.RenderRequest.Target
						{
							id = visibleProbe.realtimeTexture,
							face = CubemapFace.Unknown
						};
					}
					A_9.renderRequests.Add(renderRequest);
					foreach (ValueTuple<int, float> valueTuple in visibilities)
					{
						A_9.renderRequests[valueTuple.Item1].dependsOnRenderRequestIndices.Add(renderRequest.index);
					}
				}
			}
		}

		// Token: 0x06000470 RID: 1136 RVA: 0x0002AC88 File Offset: 0x00028E88
		[CompilerGenerated]
		private void <ExecuteRenderRequest>g__Callback|647_0(CommandBuffer c, HDGPUAsyncTaskParams a)
		{
			this.BuildGPULightListsCommon(a.hdCamera, c);
		}

		// Token: 0x06000471 RID: 1137 RVA: 0x0002AC97 File Offset: 0x00028E97
		[CompilerGenerated]
		private void <ExecuteRenderRequest>g__Callback|647_1(CommandBuffer c, HDGPUAsyncTaskParams a)
		{
			this.VolumeVoxelizationPass(a.hdCamera, c);
		}

		// Token: 0x06000472 RID: 1138 RVA: 0x0002ACA6 File Offset: 0x00028EA6
		[CompilerGenerated]
		private void <ExecuteRenderRequest>g__Callback|647_2(CommandBuffer c, HDGPUAsyncTaskParams a)
		{
			this.RenderSSR(a.hdCamera, c, a.renderContext);
		}

		// Token: 0x06000473 RID: 1139 RVA: 0x0002ACBB File Offset: 0x00028EBB
		[CompilerGenerated]
		private void <ExecuteRenderRequest>g__AsyncSSAODispatch|647_3(CommandBuffer c, HDGPUAsyncTaskParams a)
		{
			this.m_AmbientOcclusionSystem.Dispatch(c, a.hdCamera, a.frameCount);
		}

		// Token: 0x040003A1 RID: 929
		internal const int k_MaxCacheSize = 2000000000;

		// Token: 0x040003A2 RID: 930
		internal const int k_MaxDirectionalLightsOnScreen = 16;

		// Token: 0x040003A3 RID: 931
		internal const int k_MaxPunctualLightsOnScreen = 512;

		// Token: 0x040003A4 RID: 932
		internal const int k_MaxAreaLightsOnScreen = 128;

		// Token: 0x040003A5 RID: 933
		internal const int k_MaxDecalsOnScreen = 512;

		// Token: 0x040003A6 RID: 934
		internal const int k_MaxLightsOnScreen = 784;

		// Token: 0x040003A7 RID: 935
		internal const int k_MaxEnvLightsOnScreen = 128;

		// Token: 0x040003A8 RID: 936
		internal static readonly Vector3 k_BoxCullingExtentThreshold = Vector3.one * 0.01f;

		// Token: 0x040003A9 RID: 937
		private static bool k_PreferFragment = false;

		// Token: 0x040003AA RID: 938
		private const bool k_HasNativeQuadSupport = false;

		// Token: 0x040003AB RID: 939
		private const int k_ThreadGroupOptimalSize = 64;

		// Token: 0x040003AC RID: 940
		private int m_MaxDirectionalLightsOnScreen;

		// Token: 0x040003AD RID: 941
		private int m_MaxPunctualLightsOnScreen;

		// Token: 0x040003AE RID: 942
		private int m_MaxAreaLightsOnScreen;

		// Token: 0x040003AF RID: 943
		private int m_MaxDecalsOnScreen;

		// Token: 0x040003B0 RID: 944
		private int m_MaxLightsOnScreen;

		// Token: 0x040003B1 RID: 945
		private int m_MaxEnvLightsOnScreen;

		// Token: 0x040003B2 RID: 946
		private int m_MaxPlanarReflectionOnScreen;

		// Token: 0x040003B3 RID: 947
		private Texture2DArray m_DefaultTexture2DArray;

		// Token: 0x040003B4 RID: 948
		private Cubemap m_DefaultTextureCube;

		// Token: 0x040003B5 RID: 949
		internal HDRenderPipeline.LightLoopTextureCaches m_TextureCaches = new HDRenderPipeline.LightLoopTextureCaches();

		// Token: 0x040003B6 RID: 950
		internal HDRenderPipeline.LightLoopLightData m_LightLoopLightData = new HDRenderPipeline.LightLoopLightData();

		// Token: 0x040003B7 RID: 951
		private HDRenderPipeline.TileAndClusterData m_TileAndClusterData = new HDRenderPipeline.TileAndClusterData();

		// Token: 0x040003B8 RID: 952
		internal static readonly bool s_UseCascadeBorders = true;

		// Token: 0x040003B9 RID: 953
		private uint[] m_SortKeys;

		// Token: 0x040003BA RID: 954
		private DynamicArray<ProcessedLightData> m_ProcessedLightData = new DynamicArray<ProcessedLightData>();

		// Token: 0x040003BB RID: 955
		private DynamicArray<ProcessedProbeData> m_ProcessedReflectionProbeData = new DynamicArray<ProcessedProbeData>();

		// Token: 0x040003BC RID: 956
		private DynamicArray<ProcessedProbeData> m_ProcessedPlanarProbeData = new DynamicArray<ProcessedProbeData>();

		// Token: 0x040003BD RID: 957
		private static readonly Matrix4x4 s_FlipMatrixLHSRHS = Matrix4x4.Scale(new Vector3(1f, 1f, -1f));

		// Token: 0x040003BE RID: 958
		private int m_MaxViewCount = 1;

		// Token: 0x040003BF RID: 959
		private Matrix4x4[] m_LightListProjMatrices = new Matrix4x4[ShaderConfig.s_XrMaxViews];

		// Token: 0x040003C0 RID: 960
		private Matrix4x4[] m_LightListProjscrMatrices = new Matrix4x4[ShaderConfig.s_XrMaxViews];

		// Token: 0x040003C1 RID: 961
		private Matrix4x4[] m_LightListInvProjscrMatrices = new Matrix4x4[ShaderConfig.s_XrMaxViews];

		// Token: 0x040003C2 RID: 962
		private Matrix4x4[] m_LightListProjHMatrices = new Matrix4x4[ShaderConfig.s_XrMaxViews];

		// Token: 0x040003C3 RID: 963
		private Matrix4x4[] m_LightListInvProjHMatrices = new Matrix4x4[ShaderConfig.s_XrMaxViews];

		// Token: 0x040003C4 RID: 964
		internal HDRenderPipeline.LightList m_lightList;

		// Token: 0x040003C5 RID: 965
		private int m_TotalLightCount;

		// Token: 0x040003C6 RID: 966
		private int m_densityVolumeCount;

		// Token: 0x040003C7 RID: 967
		private bool m_enableBakeShadowMask;

		// Token: 0x040003C8 RID: 968
		private static int s_GenAABBKernel;

		// Token: 0x040003C9 RID: 969
		private static int s_GenAABBKernel_Oblique;

		// Token: 0x040003CA RID: 970
		private static int s_GenListPerTileKernel;

		// Token: 0x040003CB RID: 971
		private static int s_GenListPerTileKernel_Oblique;

		// Token: 0x040003CC RID: 972
		private static int s_GenListPerVoxelKernel;

		// Token: 0x040003CD RID: 973
		private static int s_GenListPerVoxelKernelOblique;

		// Token: 0x040003CE RID: 974
		private static int s_ClearVoxelAtomicKernel;

		// Token: 0x040003CF RID: 975
		private static int s_ClearDispatchIndirectKernel;

		// Token: 0x040003D0 RID: 976
		private static int s_BuildDispatchIndirectKernel;

		// Token: 0x040003D1 RID: 977
		private static int s_ClearDrawProceduralIndirectKernel;

		// Token: 0x040003D2 RID: 978
		private static int s_BuildDrawProceduralIndirectKernel;

		// Token: 0x040003D3 RID: 979
		private static int s_BuildMaterialFlagsWriteKernel;

		// Token: 0x040003D4 RID: 980
		private static int s_BuildMaterialFlagsOrKernel;

		// Token: 0x040003D5 RID: 981
		private static int s_shadeOpaqueDirectFptlKernel;

		// Token: 0x040003D6 RID: 982
		private static int s_shadeOpaqueDirectFptlDebugDisplayKernel;

		// Token: 0x040003D7 RID: 983
		private static int s_shadeOpaqueDirectShadowMaskFptlKernel;

		// Token: 0x040003D8 RID: 984
		private static int s_shadeOpaqueDirectShadowMaskFptlDebugDisplayKernel;

		// Token: 0x040003D9 RID: 985
		private static int[] s_shadeOpaqueIndirectFptlKernels = new int[LightDefinitions.s_NumFeatureVariants];

		// Token: 0x040003DA RID: 986
		private static int[] s_shadeOpaqueIndirectShadowMaskFptlKernels = new int[LightDefinitions.s_NumFeatureVariants];

		// Token: 0x040003DB RID: 987
		private static int s_deferredContactShadowKernel;

		// Token: 0x040003DC RID: 988
		private static int s_deferredContactShadowKernelMSAA;

		// Token: 0x040003DD RID: 989
		private static int s_GenListPerBigTileKernel;

		// Token: 0x040003DE RID: 990
		private const bool k_UseDepthBuffer = true;

		// Token: 0x040003DF RID: 991
		private const int k_Log2NumClusters = 6;

		// Token: 0x040003E0 RID: 992
		private const float k_ClustLogBase = 1.02f;

		// Token: 0x040003E1 RID: 993
		private float m_ClusterScale;

		// Token: 0x040003E2 RID: 994
		private static DebugLightVolumes s_lightVolumes = null;

		// Token: 0x040003E3 RID: 995
		private static Material s_DeferredTileRegularLightingMat;

		// Token: 0x040003E4 RID: 996
		private static Material s_DeferredTileSplitLightingMat;

		// Token: 0x040003E5 RID: 997
		private static Material s_DeferredTileMat;

		// Token: 0x040003E6 RID: 998
		private static string[] s_variantNames = new string[LightDefinitions.s_NumFeatureVariants];

		// Token: 0x040003E7 RID: 999
		private static string[,] s_ClusterKernelNames;

		// Token: 0x040003E8 RID: 1000
		private static string[,] s_ClusterObliqueKernelNames;

		// Token: 0x040003E9 RID: 1001
		private static int[] s_TempScreenDimArray;

		// Token: 0x040003EA RID: 1002
		private ContactShadows m_ContactShadows;

		// Token: 0x040003EB RID: 1003
		private bool m_EnableContactShadow;

		// Token: 0x040003EC RID: 1004
		private IndirectLightingController m_indirectLightingController;

		// Token: 0x040003ED RID: 1005
		private Material[] m_deferredLightingMaterial;

		// Token: 0x040003EE RID: 1006
		private Material m_DebugViewTilesMaterial;

		// Token: 0x040003EF RID: 1007
		private Material m_DebugHDShadowMapMaterial;

		// Token: 0x040003F0 RID: 1008
		private Material m_DebugBlitMaterial;

		// Token: 0x040003F1 RID: 1009
		private HashSet<HDAdditionalLightData> m_ScreenSpaceShadowsUnion = new HashSet<HDAdditionalLightData>();

		// Token: 0x040003F2 RID: 1010
		private Light m_CurrentSunLight;

		// Token: 0x040003F3 RID: 1011
		private int m_CurrentShadowSortedSunLightIndex = -1;

		// Token: 0x040003F4 RID: 1012
		private HDAdditionalLightData m_CurrentSunLightAdditionalLightData;

		// Token: 0x040003F5 RID: 1013
		private DirectionalLightData m_CurrentSunLightDirectionalLightData;

		// Token: 0x040003F6 RID: 1014
		private int m_ScreenSpaceShadowIndex;

		// Token: 0x040003F7 RID: 1015
		private int m_ScreenSpaceShadowChannelSlot;

		// Token: 0x040003F8 RID: 1016
		private HDRenderPipeline.ScreenSpaceShadowData[] m_CurrentScreenSpaceShadowData;

		// Token: 0x040003F9 RID: 1017
		private int m_ContactShadowIndex;

		// Token: 0x040003FA RID: 1018
		private HDShadowManager m_ShadowManager;

		// Token: 0x040003FB RID: 1019
		private HDShadowInitParameters m_ShadowInitParameters;

		// Token: 0x040003FC RID: 1020
		private int m_DebugSelectedLightShadowIndex;

		// Token: 0x040003FD RID: 1021
		private int m_DebugSelectedLightShadowCount;

		// Token: 0x040003FE RID: 1022
		private static MaterialPropertyBlock m_LightLoopDebugMaterialProperties;

		// Token: 0x040003FF RID: 1023
		private const string m_RayGenAreaShadowSingleName = "RayGenAreaShadowSingle";

		// Token: 0x04000400 RID: 1024
		private const string m_RayGenDirectionalShadowSingleName = "RayGenDirectionalShadowSingle";

		// Token: 0x04000401 RID: 1025
		private const string m_RayGenDirectionalColorShadowSingleName = "RayGenDirectionalColorShadowSingle";

		// Token: 0x04000402 RID: 1026
		private const string m_RayGenShadowSegmentSingleName = "RayGenShadowSegmentSingle";

		// Token: 0x04000403 RID: 1027
		private const string m_RayGenSemiTransparentShadowSegmentSingleName = "RayGenSemiTransparentShadowSegmentSingle";

		// Token: 0x04000404 RID: 1028
		private RTHandle m_ScreenSpaceShadowTextureArray;

		// Token: 0x04000405 RID: 1029
		private RayTracingShader m_ScreenSpaceShadowsRT;

		// Token: 0x04000406 RID: 1030
		private ComputeShader m_ScreenSpaceShadowsCS;

		// Token: 0x04000407 RID: 1031
		private ComputeShader m_ScreenSpaceShadowsFilterCS;

		// Token: 0x04000408 RID: 1032
		private int m_ClearShadowTexture;

		// Token: 0x04000409 RID: 1033
		private int m_OutputShadowTextureKernel;

		// Token: 0x0400040A RID: 1034
		private int m_OutputColorShadowTextureKernel;

		// Token: 0x0400040B RID: 1035
		private int m_RaytracingDirectionalShadowSample;

		// Token: 0x0400040C RID: 1036
		private int m_RaytracingPointShadowSample;

		// Token: 0x0400040D RID: 1037
		private int m_RaytracingSpotShadowSample;

		// Token: 0x0400040E RID: 1038
		private int m_AreaRaytracingAreaShadowPrepassKernel;

		// Token: 0x0400040F RID: 1039
		private int m_AreaRaytracingAreaShadowNewSampleKernel;

		// Token: 0x04000410 RID: 1040
		private int m_AreaShadowApplyTAAKernel;

		// Token: 0x04000411 RID: 1041
		private int m_AreaUpdateAnalyticHistoryKernel;

		// Token: 0x04000412 RID: 1042
		private int m_AreaUpdateShadowHistoryKernel;

		// Token: 0x04000413 RID: 1043
		private int m_AreaEstimateNoiseKernel;

		// Token: 0x04000414 RID: 1044
		private int m_AreaFirstDenoiseKernel;

		// Token: 0x04000415 RID: 1045
		private int m_AreaSecondDenoiseKernel;

		// Token: 0x04000416 RID: 1046
		private int m_AreaShadowNoDenoiseKernel;

		// Token: 0x04000417 RID: 1047
		private Matrix4x4 m_WorldToLocalArea;

		// Token: 0x04000418 RID: 1048
		private Vector4 m_ShadowChannelMask0 = new Vector4(1f, 1f, 1f, 1f);

		// Token: 0x04000419 RID: 1049
		private Vector4 m_ShadowChannelMask1 = new Vector4(1f, 1f, 1f, 1f);

		// Token: 0x0400041A RID: 1050
		private Vector4 m_ShadowChannelMask2 = new Vector4(1f, 1f, 1f, 1f);

		// Token: 0x0400041B RID: 1051
		private static Material s_ScreenSpaceShadowsMat;

		// Token: 0x0400041C RID: 1052
		private VolumetricLightingPreset volumetricLightingPreset;

		// Token: 0x0400041D RID: 1053
		private ComputeShader m_VolumeVoxelizationCS;

		// Token: 0x0400041E RID: 1054
		private ComputeShader m_VolumetricLightingCS;

		// Token: 0x0400041F RID: 1055
		private List<OrientedBBox> m_VisibleVolumeBounds;

		// Token: 0x04000420 RID: 1056
		private List<DensityVolumeEngineData> m_VisibleVolumeData;

		// Token: 0x04000421 RID: 1057
		private const int k_MaxVisibleVolumeCount = 512;

		// Token: 0x04000422 RID: 1058
		private ComputeBuffer m_VisibleVolumeBoundsBuffer;

		// Token: 0x04000423 RID: 1059
		private ComputeBuffer m_VisibleVolumeDataBuffer;

		// Token: 0x04000424 RID: 1060
		private RTHandle m_DensityBufferHandle;

		// Token: 0x04000425 RID: 1061
		private RTHandle m_LightingBufferHandle;

		// Token: 0x04000426 RID: 1062
		private bool m_SupportVolumetrics;

		// Token: 0x04000427 RID: 1063
		private Vector4[] m_PackedCoeffs;

		// Token: 0x04000428 RID: 1064
		private ZonalHarmonicsL2 m_PhaseZH;

		// Token: 0x04000429 RID: 1065
		private Vector2[] m_xySeq;

		// Token: 0x0400042A RID: 1066
		private float[] m_zSeq = new float[] { 0.5f, 0.21428572f, 0.78571427f, 0.35714287f, 0.64285713f, 0.071428575f, 0.9285714f };

		// Token: 0x0400042B RID: 1067
		private Matrix4x4[] m_PixelCoordToViewDirWS;

		// Token: 0x0400042C RID: 1068
		private RTHandle m_SSSColor;

		// Token: 0x0400042D RID: 1069
		private RTHandle m_SSSColorMSAA;

		// Token: 0x0400042E RID: 1070
		private bool m_SSSReuseGBufferMemory;

		// Token: 0x0400042F RID: 1071
		private ComputeShader m_SubsurfaceScatteringCS;

		// Token: 0x04000430 RID: 1072
		private int m_SubsurfaceScatteringKernel;

		// Token: 0x04000431 RID: 1073
		private int m_SubsurfaceScatteringKernelMSAA;

		// Token: 0x04000432 RID: 1074
		private Material m_CombineLightingPass;

		// Token: 0x04000433 RID: 1075
		private RTHandle m_SSSCameraFilteringBuffer;

		// Token: 0x04000434 RID: 1076
		private Material m_SSSCopyStencilForSplitLighting;

		// Token: 0x04000435 RID: 1077
		private Vector4[] m_SSSThicknessRemaps;

		// Token: 0x04000436 RID: 1078
		private Vector4[] m_SSSShapeParams;

		// Token: 0x04000437 RID: 1079
		private Vector4[] m_SSSTransmissionTintsAndFresnel0;

		// Token: 0x04000438 RID: 1080
		private Vector4[] m_SSSDisabledTransmissionTintsAndFresnel0;

		// Token: 0x04000439 RID: 1081
		private Vector4[] m_SSSWorldScales;

		// Token: 0x0400043A RID: 1082
		private Vector4[] m_SSSFilterKernels;

		// Token: 0x0400043B RID: 1083
		private float[] m_SSSDiffusionProfileHashes;

		// Token: 0x0400043C RID: 1084
		private int[] m_SSSDiffusionProfileUpdate;

		// Token: 0x0400043D RID: 1085
		private DiffusionProfileSettings[] m_SSSSetDiffusionProfiles;

		// Token: 0x0400043E RID: 1086
		private DiffusionProfileSettings m_SSSDefaultDiffusionProfile;

		// Token: 0x0400043F RID: 1087
		private int m_SSSActiveDiffusionProfileCount;

		// Token: 0x04000440 RID: 1088
		private uint m_SSSTexturingModeFlags;

		// Token: 0x04000441 RID: 1089
		private uint m_SSSTransmissionFlags;

		// Token: 0x04000442 RID: 1090
		private const string m_RayGenSubSurfaceShaderName = "RayGenSubSurface";

		// Token: 0x04000443 RID: 1091
		private RenderGraphMutableResource m_DebugFullScreenTexture;

		// Token: 0x04000444 RID: 1092
		private Material m_DepthResolveMaterial;

		// Token: 0x04000445 RID: 1093
		private HDRenderPipeline.GBufferOutput m_GBufferOutput;

		// Token: 0x04000446 RID: 1094
		private HDRenderPipeline.DBufferOutput m_DBufferOutput;

		// Token: 0x04000447 RID: 1095
		private HDUtils.PackedMipChainInfo m_DepthBufferMipChainInfo;

		// Token: 0x04000448 RID: 1096
		private static Volume s_DefaultVolume;

		// Token: 0x04000449 RID: 1097
		public const string k_ShaderTagName = "HDRenderPipeline";

		// Token: 0x0400044A RID: 1098
		private readonly HDRenderPipelineAsset m_Asset;

		// Token: 0x0400044B RID: 1099
		private readonly HDRenderPipelineAsset m_DefaultAsset;

		// Token: 0x0400044C RID: 1100
		private readonly RenderPipelineMaterial m_DeferredMaterial;

		// Token: 0x0400044D RID: 1101
		private readonly List<RenderPipelineMaterial> m_MaterialList = new List<RenderPipelineMaterial>();

		// Token: 0x0400044E RID: 1102
		private readonly GBufferManager m_GbufferManager;

		// Token: 0x0400044F RID: 1103
		private readonly DBufferManager m_DbufferManager;

		// Token: 0x04000450 RID: 1104
		private readonly SharedRTManager m_SharedRTManager = new SharedRTManager();

		// Token: 0x04000451 RID: 1105
		private readonly PostProcessSystem m_PostProcessSystem;

		// Token: 0x04000452 RID: 1106
		private readonly XRSystem m_XRSystem;

		// Token: 0x04000453 RID: 1107
		private bool m_FrameSettingsHistoryEnabled;

		// Token: 0x04000454 RID: 1108
		private PerObjectData m_CurrentRendererConfigurationBakedLighting = PerObjectData.LightProbe | PerObjectData.LightProbeProxyVolume | PerObjectData.Lightmaps;

		// Token: 0x04000455 RID: 1109
		private MaterialPropertyBlock m_CopyDepthPropertyBlock = new MaterialPropertyBlock();

		// Token: 0x04000456 RID: 1110
		private Material m_CopyDepth;

		// Token: 0x04000457 RID: 1111
		private Material m_DownsampleDepthMaterial;

		// Token: 0x04000458 RID: 1112
		private Material m_UpsampleTransparency;

		// Token: 0x04000459 RID: 1113
		private GPUCopy m_GPUCopy;

		// Token: 0x0400045A RID: 1114
		private MipGenerator m_MipGenerator;

		// Token: 0x0400045B RID: 1115
		private BlueNoise m_BlueNoise;

		// Token: 0x0400045C RID: 1116
		private IBLFilterBSDF[] m_IBLFilterArray;

		// Token: 0x0400045D RID: 1117
		private int m_SsrTracingKernel = -1;

		// Token: 0x0400045E RID: 1118
		private int m_SsrReprojectionKernel = -1;

		// Token: 0x0400045F RID: 1119
		private Material m_ApplyDistortionMaterial;

		// Token: 0x04000460 RID: 1120
		private Material m_CameraMotionVectorsMaterial;

		// Token: 0x04000461 RID: 1121
		private Material m_DecalNormalBufferMaterial;

		// Token: 0x04000462 RID: 1122
		private Material m_ClearStencilBufferMaterial;

		// Token: 0x04000463 RID: 1123
		private Material m_DebugViewMaterialGBuffer;

		// Token: 0x04000464 RID: 1124
		private Material m_DebugViewMaterialGBufferShadowMask;

		// Token: 0x04000465 RID: 1125
		private Material m_currentDebugViewMaterialGBuffer;

		// Token: 0x04000466 RID: 1126
		private Material m_DebugDisplayLatlong;

		// Token: 0x04000467 RID: 1127
		private Material m_DebugFullScreen;

		// Token: 0x04000468 RID: 1128
		private MaterialPropertyBlock m_DebugFullScreenPropertyBlock = new MaterialPropertyBlock();

		// Token: 0x04000469 RID: 1129
		private Material m_DebugColorPicker;

		// Token: 0x0400046A RID: 1130
		private Material m_ErrorMaterial;

		// Token: 0x0400046B RID: 1131
		private Material m_Blit;

		// Token: 0x0400046C RID: 1132
		private Material m_BlitTexArray;

		// Token: 0x0400046D RID: 1133
		private Material m_BlitTexArraySingleSlice;

		// Token: 0x0400046E RID: 1134
		private MaterialPropertyBlock m_BlitPropertyBlock = new MaterialPropertyBlock();

		// Token: 0x0400046F RID: 1135
		private RenderTargetIdentifier[] m_MRTCache2 = new RenderTargetIdentifier[2];

		// Token: 0x04000470 RID: 1136
		private RTHandle m_CameraColorBuffer;

		// Token: 0x04000471 RID: 1137
		private RTHandle m_OpaqueAtmosphericScatteringBuffer;

		// Token: 0x04000472 RID: 1138
		private RTHandle m_CameraSssDiffuseLightingBuffer;

		// Token: 0x04000473 RID: 1139
		private RTHandle m_ContactShadowBuffer;

		// Token: 0x04000474 RID: 1140
		private RTHandle m_ScreenSpaceShadowsBuffer;

		// Token: 0x04000475 RID: 1141
		private RTHandle m_DistortionBuffer;

		// Token: 0x04000476 RID: 1142
		private RTHandle m_LowResTransparentBuffer;

		// Token: 0x04000477 RID: 1143
		private RTHandle m_SsrHitPointTexture;

		// Token: 0x04000478 RID: 1144
		private RTHandle m_SsrLightingTexture;

		// Token: 0x04000479 RID: 1145
		private RTHandle m_CameraColorMSAABuffer;

		// Token: 0x0400047A RID: 1146
		private RTHandle m_OpaqueAtmosphericScatteringMSAABuffer;

		// Token: 0x0400047B RID: 1147
		private RTHandle m_CameraSssDiffuseLightingMSAABuffer;

		// Token: 0x0400047C RID: 1148
		private Lazy<RTHandle> m_CustomPassColorBuffer;

		// Token: 0x0400047D RID: 1149
		private Lazy<RTHandle> m_CustomPassDepthBuffer;

		// Token: 0x0400047E RID: 1150
		private MSAASamples m_MSAASamples;

		// Token: 0x0400047F RID: 1151
		private ShaderTagId[] m_ForwardAndForwardOnlyPassNames = new ShaderTagId[]
		{
			HDShaderPassNames.s_ForwardOnlyName,
			HDShaderPassNames.s_ForwardName,
			HDShaderPassNames.s_SRPDefaultUnlitName
		};

		// Token: 0x04000480 RID: 1152
		private ShaderTagId[] m_ForwardOnlyPassNames = new ShaderTagId[]
		{
			HDShaderPassNames.s_ForwardOnlyName,
			HDShaderPassNames.s_SRPDefaultUnlitName
		};

		// Token: 0x04000481 RID: 1153
		private ShaderTagId[] m_AllTransparentPassNames = new ShaderTagId[]
		{
			HDShaderPassNames.s_TransparentBackfaceName,
			HDShaderPassNames.s_ForwardOnlyName,
			HDShaderPassNames.s_ForwardName,
			HDShaderPassNames.s_SRPDefaultUnlitName
		};

		// Token: 0x04000482 RID: 1154
		private ShaderTagId[] m_TransparentNoBackfaceNames = new ShaderTagId[]
		{
			HDShaderPassNames.s_ForwardOnlyName,
			HDShaderPassNames.s_ForwardName,
			HDShaderPassNames.s_SRPDefaultUnlitName
		};

		// Token: 0x04000483 RID: 1155
		private ShaderTagId[] m_AllForwardOpaquePassNames = new ShaderTagId[]
		{
			HDShaderPassNames.s_ForwardOnlyName,
			HDShaderPassNames.s_ForwardName,
			HDShaderPassNames.s_SRPDefaultUnlitName
		};

		// Token: 0x04000484 RID: 1156
		private ShaderTagId[] m_DepthOnlyAndDepthForwardOnlyPassNames = new ShaderTagId[]
		{
			HDShaderPassNames.s_DepthForwardOnlyName,
			HDShaderPassNames.s_DepthOnlyName
		};

		// Token: 0x04000485 RID: 1157
		private ShaderTagId[] m_DepthForwardOnlyPassNames = new ShaderTagId[] { HDShaderPassNames.s_DepthForwardOnlyName };

		// Token: 0x04000486 RID: 1158
		private ShaderTagId[] m_DepthOnlyPassNames = new ShaderTagId[] { HDShaderPassNames.s_DepthOnlyName };

		// Token: 0x04000487 RID: 1159
		private ShaderTagId[] m_TransparentDepthPrepassNames = new ShaderTagId[] { HDShaderPassNames.s_TransparentDepthPrepassName };

		// Token: 0x04000488 RID: 1160
		private ShaderTagId[] m_TransparentDepthPostpassNames = new ShaderTagId[] { HDShaderPassNames.s_TransparentDepthPostpassName };

		// Token: 0x04000489 RID: 1161
		private ShaderTagId[] m_ForwardErrorPassNames = new ShaderTagId[]
		{
			HDShaderPassNames.s_AlwaysName,
			HDShaderPassNames.s_ForwardBaseName,
			HDShaderPassNames.s_DeferredName,
			HDShaderPassNames.s_PrepassBaseName,
			HDShaderPassNames.s_VertexName,
			HDShaderPassNames.s_VertexLMRGBMName,
			HDShaderPassNames.s_VertexLMName
		};

		// Token: 0x0400048A RID: 1162
		private ShaderTagId[] m_DecalsEmissivePassNames = new ShaderTagId[]
		{
			HDShaderPassNames.s_MeshDecalsForwardEmissiveName,
			HDShaderPassNames.s_ShaderGraphMeshDecalsForwardEmissiveName
		};

		// Token: 0x0400048B RID: 1163
		private ShaderTagId[] m_SinglePassName = new ShaderTagId[1];

		// Token: 0x0400048C RID: 1164
		private ShaderTagId[] m_Decals4RTPassNames = new ShaderTagId[]
		{
			HDShaderPassNames.s_MeshDecalsMName,
			HDShaderPassNames.s_MeshDecalsAOName,
			HDShaderPassNames.s_MeshDecalsMAOName,
			HDShaderPassNames.s_MeshDecalsSName,
			HDShaderPassNames.s_MeshDecalsMSName,
			HDShaderPassNames.s_MeshDecalsAOSName,
			HDShaderPassNames.s_MeshDecalsMAOSName,
			HDShaderPassNames.s_ShaderGraphMeshDecalsName4RT
		};

		// Token: 0x0400048D RID: 1165
		private ShaderTagId[] m_Decals3RTPassNames = new ShaderTagId[]
		{
			HDShaderPassNames.s_MeshDecals3RTName,
			HDShaderPassNames.s_ShaderGraphMeshDecalsName3RT
		};

		// Token: 0x0400048E RID: 1166
		private RenderStateBlock m_DepthStateOpaque;

		// Token: 0x0400048F RID: 1167
		private int m_MaxCameraWidth;

		// Token: 0x04000490 RID: 1168
		private int m_MaxCameraHeight;

		// Token: 0x04000491 RID: 1169
		private int m_FrameCount;

		// Token: 0x04000492 RID: 1170
		private float m_LastTime;

		// Token: 0x04000493 RID: 1171
		private float m_Time;

		// Token: 0x04000494 RID: 1172
		private readonly SkyManager m_SkyManager = new SkyManager();

		// Token: 0x04000495 RID: 1173
		private readonly AmbientOcclusionSystem m_AmbientOcclusionSystem;

		// Token: 0x04000496 RID: 1174
		private MaterialPropertyBlock m_SharedPropertyBlock = new MaterialPropertyBlock();

		// Token: 0x04000497 RID: 1175
		private DebugDisplaySettings m_DebugDisplaySettings = new DebugDisplaySettings();

		// Token: 0x04000498 RID: 1176
		private static DebugDisplaySettings s_NeutralDebugDisplaySettings;

		// Token: 0x04000499 RID: 1177
		internal DebugDisplaySettings m_CurrentDebugDisplaySettings;

		// Token: 0x0400049A RID: 1178
		private RTHandle m_DebugColorPickerBuffer;

		// Token: 0x0400049B RID: 1179
		private RTHandle m_DebugFullScreenTempBuffer;

		// Token: 0x0400049C RID: 1180
		private RTHandle m_IntermediateAfterPostProcessBuffer;

		// Token: 0x0400049D RID: 1181
		private bool m_FullScreenDebugPushed;

		// Token: 0x0400049E RID: 1182
		private bool m_ValidAPI;

		// Token: 0x0400049F RID: 1183
		private bool m_IsDepthBufferCopyValid;

		// Token: 0x040004A0 RID: 1184
		private RenderTexture m_TemporaryTargetForCubemaps;

		// Token: 0x040004A1 RID: 1185
		[TupleElementNames(new string[] { "viewer", "probe", "face" })]
		private CameraCache<ValueTuple<Transform, HDProbe, int>> m_ProbeCameraCache = new CameraCache<ValueTuple<Transform, HDProbe, int>>();

		// Token: 0x040004A2 RID: 1186
		private RenderTargetIdentifier[] m_MRTTransparentMotionVec;

		// Token: 0x040004A3 RID: 1187
		private RenderTargetIdentifier[] m_MRTWithSSS = new RenderTargetIdentifier[3];

		// Token: 0x040004A4 RID: 1188
		private RenderTargetIdentifier[] mMRTSingle = new RenderTargetIdentifier[1];

		// Token: 0x040004A5 RID: 1189
		private string m_ForwardPassProfileName;

		// Token: 0x040004A6 RID: 1190
		private ComputeBuffer m_DepthPyramidMipLevelOffsetsBuffer;

		// Token: 0x040004A7 RID: 1191
		private ScriptableCullingParameters frozenCullingParams;

		// Token: 0x040004A8 RID: 1192
		private bool frozenCullingParamAvailable;

		// Token: 0x040004A9 RID: 1193
		private RenderGraph m_RenderGraph;

		// Token: 0x040004AA RID: 1194
		private Material m_ColorResolveMaterial;

		// Token: 0x040004AB RID: 1195
		private bool m_RayTracingSupported;

		// Token: 0x040004AC RID: 1196
		private static RenderTargetIdentifier[] m_Dbuffer3RtIds;

		// Token: 0x040004AD RID: 1197
		private const string m_PathTracingRayGenShaderName = "RayGen";

		// Token: 0x040004AE RID: 1198
		private uint currentIteration;

		// Token: 0x040004AF RID: 1199
		private ComputeBuffer m_RayBinResult;

		// Token: 0x040004B0 RID: 1200
		private ComputeBuffer m_RayBinSizeResult;

		// Token: 0x040004B1 RID: 1201
		private GBufferManager m_RaytracingGBufferManager;

		// Token: 0x040004B2 RID: 1202
		private const string m_RayGenGBuffer = "RayGenGBuffer";

		// Token: 0x040004B3 RID: 1203
		private const string m_RayGenGBufferHalfRes = "RayGenGBufferHalfRes";

		// Token: 0x040004B4 RID: 1204
		private const string m_RayGenGBufferBinned = "RayGenGBufferBinned";

		// Token: 0x040004B5 RID: 1205
		private const string m_RayGenGBufferHalfResBinned = "RayGenGBufferHalfResBinned";

		// Token: 0x040004B6 RID: 1206
		private const string m_MissShaderNameGBuffer = "MissShaderGBuffer";

		// Token: 0x040004B7 RID: 1207
		private const int binningTileSize = 16;

		// Token: 0x040004B8 RID: 1208
		private RTHandle m_IndirectDiffuseBuffer;

		// Token: 0x040004B9 RID: 1209
		private const string m_RayGenIndirectDiffuseIntegrationName = "RayGenIntegration";

		// Token: 0x040004BA RID: 1210
		private const string m_RayGenIndirectDiffuseFullResName = "RayGenFullRes";

		// Token: 0x040004BB RID: 1211
		private const string m_MissIndirectDiffuseName = "MissShaderIndirectDiffuse";

		// Token: 0x040004BC RID: 1212
		private const string m_ClosestHitIndirectDiffuseName = "ClosestHitMain";

		// Token: 0x040004BD RID: 1213
		private RayTracingAccelerationStructure m_CurrentRAS = new RayTracingAccelerationStructure();

		// Token: 0x040004BE RID: 1214
		private HDRaytracingLightCluster m_RayTracingLightCluster = new HDRaytracingLightCluster();

		// Token: 0x040004BF RID: 1215
		private HDRayTracingLights m_RayTracingLights = new HDRayTracingLights();

		// Token: 0x040004C0 RID: 1216
		private bool m_ValidRayTracingState;

		// Token: 0x040004C1 RID: 1217
		private bool m_ValidRayTracingCluster;

		// Token: 0x040004C2 RID: 1218
		private HDTemporalFilter m_TemporalFilter = new HDTemporalFilter();

		// Token: 0x040004C3 RID: 1219
		private HDSimpleDenoiser m_SimpleDenoiser = new HDSimpleDenoiser();

		// Token: 0x040004C4 RID: 1220
		private HDDiffuseDenoiser m_DiffuseDenoiser = new HDDiffuseDenoiser();

		// Token: 0x040004C5 RID: 1221
		private HDReflectionDenoiser m_ReflectionDenoiser = new HDReflectionDenoiser();

		// Token: 0x040004C6 RID: 1222
		private RayCountManager m_RayCountManager = new RayCountManager();

		// Token: 0x040004C7 RID: 1223
		private const int maxNumSubMeshes = 32;

		// Token: 0x040004C8 RID: 1224
		private Dictionary<int, int> m_RayTracingRendererReference = new Dictionary<int, int>();

		// Token: 0x040004C9 RID: 1225
		private bool[] subMeshFlagArray = new bool[32];

		// Token: 0x040004CA RID: 1226
		private bool[] subMeshCutoffArray = new bool[32];

		// Token: 0x040004CB RID: 1227
		private bool[] subMeshTransparentArray = new bool[32];

		// Token: 0x040004CC RID: 1228
		private ReflectionProbe reflectionProbe = new ReflectionProbe();

		// Token: 0x040004CD RID: 1229
		private List<Material> materialArray = new List<Material>(32);

		// Token: 0x040004CE RID: 1230
		private RTHandle m_RayTracingDirectionBuffer;

		// Token: 0x040004CF RID: 1231
		private RTHandle m_RayTracingDistanceBuffer;

		// Token: 0x040004D0 RID: 1232
		private RTHandle m_RayTracingIntermediateBufferR0;

		// Token: 0x040004D1 RID: 1233
		private RTHandle m_RayTracingIntermediateBufferR1;

		// Token: 0x040004D2 RID: 1234
		private RTHandle m_RayTracingIntermediateBufferRG0;

		// Token: 0x040004D3 RID: 1235
		private RTHandle m_RayTracingIntermediateBufferRGBA0;

		// Token: 0x040004D4 RID: 1236
		private RTHandle m_RayTracingIntermediateBufferRGBA1;

		// Token: 0x040004D5 RID: 1237
		private RTHandle m_RayTracingIntermediateBufferRGBA2;

		// Token: 0x040004D6 RID: 1238
		private RTHandle m_RayTracingIntermediateBufferRGBA3;

		// Token: 0x040004D7 RID: 1239
		private Material m_RaytracingFlagMaterial;

		// Token: 0x040004D8 RID: 1240
		private const string m_RayGenShaderName = "RayGenRenderer";

		// Token: 0x040004D9 RID: 1241
		private ShaderTagId raytracingPassID = new ShaderTagId("Forward");

		// Token: 0x040004DA RID: 1242
		private RenderStateBlock m_RaytracingFlagStateBlock;

		// Token: 0x040004DB RID: 1243
		private const string m_RayGenReflectionHalfResName = "RayGenReflectionHalfRes";

		// Token: 0x040004DC RID: 1244
		private const string m_RayGenReflectionFullResName = "RayGenReflectionFullRes";

		// Token: 0x040004DD RID: 1245
		private const string m_RayGenIntegrationName = "RayGenIntegration";

		// Token: 0x020001A0 RID: 416
		internal class LightLoopTextureCaches
		{
			// Token: 0x17000197 RID: 407
			// (get) Token: 0x06000B4E RID: 2894 RVA: 0x00054EA0 File Offset: 0x000530A0
			// (set) Token: 0x06000B4F RID: 2895 RVA: 0x00054EA8 File Offset: 0x000530A8
			public LightCookieManager lightCookieManager { get; private set; }

			// Token: 0x17000198 RID: 408
			// (get) Token: 0x06000B50 RID: 2896 RVA: 0x00054EB1 File Offset: 0x000530B1
			// (set) Token: 0x06000B51 RID: 2897 RVA: 0x00054EB9 File Offset: 0x000530B9
			public ReflectionProbeCache reflectionProbeCache { get; private set; }

			// Token: 0x17000199 RID: 409
			// (get) Token: 0x06000B52 RID: 2898 RVA: 0x00054EC2 File Offset: 0x000530C2
			// (set) Token: 0x06000B53 RID: 2899 RVA: 0x00054ECA File Offset: 0x000530CA
			public PlanarReflectionProbeCache reflectionPlanarProbeCache { get; private set; }

			// Token: 0x1700019A RID: 410
			// (get) Token: 0x06000B54 RID: 2900 RVA: 0x00054ED3 File Offset: 0x000530D3
			// (set) Token: 0x06000B55 RID: 2901 RVA: 0x00054EDB File Offset: 0x000530DB
			public List<Matrix4x4> env2DCaptureVP { get; private set; }

			// Token: 0x1700019B RID: 411
			// (get) Token: 0x06000B56 RID: 2902 RVA: 0x00054EE4 File Offset: 0x000530E4
			// (set) Token: 0x06000B57 RID: 2903 RVA: 0x00054EEC File Offset: 0x000530EC
			public List<float> env2DCaptureForward { get; private set; }

			// Token: 0x1700019C RID: 412
			// (get) Token: 0x06000B58 RID: 2904 RVA: 0x00054EF5 File Offset: 0x000530F5
			// (set) Token: 0x06000B59 RID: 2905 RVA: 0x00054EFD File Offset: 0x000530FD
			public List<Vector4> env2DAtlasScaleOffset { get; private set; } = new List<Vector4>();

			// Token: 0x06000B5A RID: 2906 RVA: 0x00054F08 File Offset: 0x00053108
			public void Initialize(HDRenderPipelineAsset hdrpAsset, RenderPipelineResources defaultResources, IBLFilterBSDF[] iBLFilterBSDFArray)
			{
				GlobalLightLoopSettings lightLoopSettings = hdrpAsset.currentPlatformRenderPipelineSettings.lightLoopSettings;
				this.m_CubeToPanoMaterial = CoreUtils.CreateEngineMaterial(defaultResources.shaders.cubeToPanoPS);
				this.lightCookieManager = new LightCookieManager(hdrpAsset, 2000000000);
				this.env2DCaptureVP = new List<Matrix4x4>();
				this.env2DCaptureForward = new List<float>();
				int i = 0;
				int num = Mathf.Max(1, lightLoopSettings.maxPlanarReflectionOnScreen);
				while (i < num)
				{
					this.env2DCaptureVP.Add(Matrix4x4.identity);
					this.env2DCaptureForward.Add(0f);
					this.env2DCaptureForward.Add(0f);
					this.env2DCaptureForward.Add(0f);
					this.env2DAtlasScaleOffset.Add(Vector4.zero);
					i++;
				}
				GraphicsFormat graphicsFormat = (lightLoopSettings.reflectionCacheCompressed ? GraphicsFormat.RGB_BC6H_SFloat : GraphicsFormat.R16G16B16A16_SFloat);
				int num2 = lightLoopSettings.reflectionProbeCacheSize;
				int reflectionCubemapSize = (int)lightLoopSettings.reflectionCubemapSize;
				if (ReflectionProbeCache.GetApproxCacheSizeInByte(num2, reflectionCubemapSize, iBLFilterBSDFArray.Length) > 2000000000L)
				{
					num2 = ReflectionProbeCache.GetMaxCacheSizeForWeightInByte(2000000000, reflectionCubemapSize, iBLFilterBSDFArray.Length);
				}
				this.reflectionProbeCache = new ReflectionProbeCache(defaultResources, iBLFilterBSDFArray, num2, reflectionCubemapSize, graphicsFormat, true);
				GraphicsFormat graphicsFormat2 = (lightLoopSettings.planarReflectionCacheCompressed ? GraphicsFormat.RGB_BC6H_SFloat : GraphicsFormat.R16G16B16A16_SFloat);
				int planarReflectionAtlasSize = (int)lightLoopSettings.planarReflectionAtlasSize;
				this.reflectionPlanarProbeCache = new PlanarReflectionProbeCache(defaultResources, (IBLFilterGGX)iBLFilterBSDFArray[0], planarReflectionAtlasSize, graphicsFormat2, true);
			}

			// Token: 0x06000B5B RID: 2907 RVA: 0x0005504D File Offset: 0x0005324D
			public void Cleanup()
			{
				this.reflectionProbeCache.Release();
				this.reflectionPlanarProbeCache.Release();
				this.lightCookieManager.Release();
				CoreUtils.Destroy(this.m_CubeToPanoMaterial);
			}

			// Token: 0x06000B5C RID: 2908 RVA: 0x0005507B File Offset: 0x0005327B
			public void NewFrame()
			{
				this.lightCookieManager.NewFrame();
				this.reflectionProbeCache.NewFrame();
				this.reflectionPlanarProbeCache.NewFrame();
			}

			// Token: 0x0400112E RID: 4398
			private Material m_CubeToPanoMaterial;
		}

		// Token: 0x020001A1 RID: 417
		internal class LightLoopLightData
		{
			// Token: 0x1700019D RID: 413
			// (get) Token: 0x06000B5E RID: 2910 RVA: 0x000550B1 File Offset: 0x000532B1
			// (set) Token: 0x06000B5F RID: 2911 RVA: 0x000550B9 File Offset: 0x000532B9
			public ComputeBuffer directionalLightData { get; private set; }

			// Token: 0x1700019E RID: 414
			// (get) Token: 0x06000B60 RID: 2912 RVA: 0x000550C2 File Offset: 0x000532C2
			// (set) Token: 0x06000B61 RID: 2913 RVA: 0x000550CA File Offset: 0x000532CA
			public ComputeBuffer lightData { get; private set; }

			// Token: 0x1700019F RID: 415
			// (get) Token: 0x06000B62 RID: 2914 RVA: 0x000550D3 File Offset: 0x000532D3
			// (set) Token: 0x06000B63 RID: 2915 RVA: 0x000550DB File Offset: 0x000532DB
			public ComputeBuffer envLightData { get; private set; }

			// Token: 0x170001A0 RID: 416
			// (get) Token: 0x06000B64 RID: 2916 RVA: 0x000550E4 File Offset: 0x000532E4
			// (set) Token: 0x06000B65 RID: 2917 RVA: 0x000550EC File Offset: 0x000532EC
			public ComputeBuffer decalData { get; private set; }

			// Token: 0x06000B66 RID: 2918 RVA: 0x000550F8 File Offset: 0x000532F8
			public void Initialize(int directionalCount, int punctualCount, int areaLightCount, int envLightCount, int decalCount)
			{
				this.directionalLightData = new ComputeBuffer(directionalCount, Marshal.SizeOf(typeof(DirectionalLightData)));
				this.lightData = new ComputeBuffer(punctualCount + areaLightCount, Marshal.SizeOf(typeof(LightData)));
				this.envLightData = new ComputeBuffer(envLightCount, Marshal.SizeOf(typeof(EnvLightData)));
				this.decalData = new ComputeBuffer(decalCount, Marshal.SizeOf(typeof(DecalData)));
			}

			// Token: 0x06000B67 RID: 2919 RVA: 0x00055175 File Offset: 0x00053375
			public void Cleanup()
			{
				CoreUtils.SafeRelease(this.directionalLightData);
				CoreUtils.SafeRelease(this.lightData);
				CoreUtils.SafeRelease(this.envLightData);
				CoreUtils.SafeRelease(this.decalData);
			}
		}

		// Token: 0x020001A2 RID: 418
		private class TileAndClusterData
		{
			// Token: 0x06000B69 RID: 2921 RVA: 0x000551A3 File Offset: 0x000533A3
			public void Initialize()
			{
				this.globalLightListAtomic = new ComputeBuffer(1, 4);
			}

			// Token: 0x06000B6A RID: 2922 RVA: 0x000551B4 File Offset: 0x000533B4
			public void AllocateResolutionDependentBuffers(HDCamera hdCamera, int width, int height, int viewCount, int maxLightOnScreen)
			{
				int num = (width + LightDefinitions.s_TileSizeFptl - 1) / LightDefinitions.s_TileSizeFptl;
				int num2 = (height + LightDefinitions.s_TileSizeFptl - 1) / LightDefinitions.s_TileSizeFptl;
				int num3 = num * num2 * viewCount;
				this.lightList = new ComputeBuffer(80 * num3, 4);
				this.tileList = new ComputeBuffer(LightDefinitions.s_NumFeatureVariants * num3, 4);
				this.tileFeatureFlags = new ComputeBuffer(num3, 4);
				int num4 = (width + LightDefinitions.s_TileSizeClustered - 1) / LightDefinitions.s_TileSizeClustered;
				int num5 = (height + LightDefinitions.s_TileSizeClustered - 1) / LightDefinitions.s_TileSizeClustered;
				int num6 = num4 * num5 * viewCount;
				this.perVoxelOffset = new ComputeBuffer(320 * num6, 4);
				this.perVoxelLightLists = new ComputeBuffer(HDRenderPipeline.NumLightIndicesPerClusteredTile() * num6, 4);
				this.perTileLogBaseTweak = new ComputeBuffer(num6, 4);
				if (hdCamera.frameSettings.IsEnabled(FrameSettingsField.BigTilePrepass))
				{
					int num7 = (width + 63) / 64;
					int num8 = (height + 63) / 64;
					int num9 = num7 * num8 * viewCount;
					this.bigTileLightList = new ComputeBuffer(LightDefinitions.s_MaxNrBigTileLightsPlusOne * num9, 4);
				}
				this.AABBBoundsBuffer = new ComputeBuffer(viewCount * 2 * maxLightOnScreen, 16);
				this.convexBoundsBuffer = new ComputeBuffer(viewCount * maxLightOnScreen, Marshal.SizeOf(typeof(SFiniteLightBound)));
				this.lightVolumeDataBuffer = new ComputeBuffer(viewCount * maxLightOnScreen, Marshal.SizeOf(typeof(LightVolumeData)));
				this.dispatchIndirectBuffer = new ComputeBuffer(viewCount * LightDefinitions.s_NumFeatureVariants * 4, 4, ComputeBufferType.DrawIndirect);
			}

			// Token: 0x06000B6B RID: 2923 RVA: 0x0005531C File Offset: 0x0005351C
			public void ReleaseResolutionDependentBuffers()
			{
				CoreUtils.SafeRelease(this.lightList);
				CoreUtils.SafeRelease(this.tileList);
				CoreUtils.SafeRelease(this.tileFeatureFlags);
				CoreUtils.SafeRelease(this.perVoxelLightLists);
				CoreUtils.SafeRelease(this.perVoxelOffset);
				CoreUtils.SafeRelease(this.perTileLogBaseTweak);
				CoreUtils.SafeRelease(this.bigTileLightList);
				CoreUtils.SafeRelease(this.AABBBoundsBuffer);
				CoreUtils.SafeRelease(this.convexBoundsBuffer);
				CoreUtils.SafeRelease(this.lightVolumeDataBuffer);
				CoreUtils.SafeRelease(this.dispatchIndirectBuffer);
			}

			// Token: 0x06000B6C RID: 2924 RVA: 0x000553A2 File Offset: 0x000535A2
			public void Cleanup()
			{
				CoreUtils.SafeRelease(this.globalLightListAtomic);
				this.ReleaseResolutionDependentBuffers();
			}

			// Token: 0x04001133 RID: 4403
			public ComputeBuffer lightVolumeDataBuffer;

			// Token: 0x04001134 RID: 4404
			public ComputeBuffer convexBoundsBuffer;

			// Token: 0x04001135 RID: 4405
			public ComputeBuffer AABBBoundsBuffer;

			// Token: 0x04001136 RID: 4406
			public ComputeBuffer lightList;

			// Token: 0x04001137 RID: 4407
			public ComputeBuffer tileList;

			// Token: 0x04001138 RID: 4408
			public ComputeBuffer tileFeatureFlags;

			// Token: 0x04001139 RID: 4409
			public ComputeBuffer dispatchIndirectBuffer;

			// Token: 0x0400113A RID: 4410
			public ComputeBuffer bigTileLightList;

			// Token: 0x0400113B RID: 4411
			public ComputeBuffer perVoxelLightLists;

			// Token: 0x0400113C RID: 4412
			public ComputeBuffer perVoxelOffset;

			// Token: 0x0400113D RID: 4413
			public ComputeBuffer perTileLogBaseTweak;

			// Token: 0x0400113E RID: 4414
			public ComputeBuffer globalLightListAtomic;

			// Token: 0x0400113F RID: 4415
			public bool listsAreClear;
		}

		// Token: 0x020001A3 RID: 419
		internal class LightList
		{
			// Token: 0x06000B6E RID: 2926 RVA: 0x000553B8 File Offset: 0x000535B8
			public void Clear()
			{
				this.directionalLights.Clear();
				this.lights.Clear();
				this.envLights.Clear();
				this.punctualLightCount = 0;
				this.areaLightCount = 0;
				for (int i = 0; i < this.lightsPerView.Count; i++)
				{
					this.lightsPerView[i].bounds.Clear();
					this.lightsPerView[i].lightVolumes.Clear();
				}
			}

			// Token: 0x06000B6F RID: 2927 RVA: 0x00055438 File Offset: 0x00053638
			public void Allocate()
			{
				this.directionalLights = new List<DirectionalLightData>();
				this.lights = new List<LightData>();
				this.envLights = new List<EnvLightData>();
				this.lightsPerView = new List<HDRenderPipeline.LightList.LightsPerView>();
				for (int i = 0; i < TextureXR.slices; i++)
				{
					this.lightsPerView.Add(new HDRenderPipeline.LightList.LightsPerView
					{
						bounds = new List<SFiniteLightBound>(),
						lightVolumes = new List<LightVolumeData>()
					});
				}
			}

			// Token: 0x04001140 RID: 4416
			public List<DirectionalLightData> directionalLights;

			// Token: 0x04001141 RID: 4417
			public List<LightData> lights;

			// Token: 0x04001142 RID: 4418
			public List<EnvLightData> envLights;

			// Token: 0x04001143 RID: 4419
			public int punctualLightCount;

			// Token: 0x04001144 RID: 4420
			public int areaLightCount;

			// Token: 0x04001145 RID: 4421
			public List<HDRenderPipeline.LightList.LightsPerView> lightsPerView;

			// Token: 0x020002AC RID: 684
			public struct LightsPerView
			{
				// Token: 0x0400172E RID: 5934
				public List<SFiniteLightBound> bounds;

				// Token: 0x0400172F RID: 5935
				public List<LightVolumeData> lightVolumes;
			}
		}

		// Token: 0x020001A4 RID: 420
		private enum ClusterPrepassSource
		{
			// Token: 0x04001147 RID: 4423
			None,
			// Token: 0x04001148 RID: 4424
			BigTile,
			// Token: 0x04001149 RID: 4425
			Count
		}

		// Token: 0x020001A5 RID: 421
		private enum ClusterDepthSource
		{
			// Token: 0x0400114B RID: 4427
			NoDepth,
			// Token: 0x0400114C RID: 4428
			Depth,
			// Token: 0x0400114D RID: 4429
			MSAA_Depth,
			// Token: 0x0400114E RID: 4430
			Count
		}

		// Token: 0x020001A6 RID: 422
		private struct ScreenSpaceShadowData
		{
			// Token: 0x0400114F RID: 4431
			public HDAdditionalLightData additionalLightData;

			// Token: 0x04001150 RID: 4432
			public int lightDataIndex;

			// Token: 0x04001151 RID: 4433
			public bool valid;
		}

		// Token: 0x020001A7 RID: 423
		private struct BuildGPULightListParameters
		{
			// Token: 0x04001152 RID: 4434
			public int totalLightCount;

			// Token: 0x04001153 RID: 4435
			public bool isOrthographic;

			// Token: 0x04001154 RID: 4436
			public int viewCount;

			// Token: 0x04001155 RID: 4437
			public bool runLightList;

			// Token: 0x04001156 RID: 4438
			public bool clearLightLists;

			// Token: 0x04001157 RID: 4439
			public bool enableFeatureVariants;

			// Token: 0x04001158 RID: 4440
			public bool computeMaterialVariants;

			// Token: 0x04001159 RID: 4441
			public bool computeLightVariants;

			// Token: 0x0400115A RID: 4442
			public bool skyEnabled;

			// Token: 0x0400115B RID: 4443
			public HDRenderPipeline.LightList lightList;

			// Token: 0x0400115C RID: 4444
			public Matrix4x4[] lightListProjscrMatrices;

			// Token: 0x0400115D RID: 4445
			public Matrix4x4[] lightListInvProjscrMatrices;

			// Token: 0x0400115E RID: 4446
			public float nearClipPlane;

			// Token: 0x0400115F RID: 4447
			public float farClipPlane;

			// Token: 0x04001160 RID: 4448
			public Vector4 screenSize;

			// Token: 0x04001161 RID: 4449
			public int msaaSamples;

			// Token: 0x04001162 RID: 4450
			public ComputeShader screenSpaceAABBShader;

			// Token: 0x04001163 RID: 4451
			public int screenSpaceAABBKernel;

			// Token: 0x04001164 RID: 4452
			public Matrix4x4[] lightListProjHMatrices;

			// Token: 0x04001165 RID: 4453
			public Matrix4x4[] lightListInvProjHMatrices;

			// Token: 0x04001166 RID: 4454
			public ComputeShader bigTilePrepassShader;

			// Token: 0x04001167 RID: 4455
			public int bigTilePrepassKernel;

			// Token: 0x04001168 RID: 4456
			public bool runBigTilePrepass;

			// Token: 0x04001169 RID: 4457
			public int numBigTilesX;

			// Token: 0x0400116A RID: 4458
			public int numBigTilesY;

			// Token: 0x0400116B RID: 4459
			public ComputeShader buildPerTileLightListShader;

			// Token: 0x0400116C RID: 4460
			public int buildPerTileLightListKernel;

			// Token: 0x0400116D RID: 4461
			public bool runFPTL;

			// Token: 0x0400116E RID: 4462
			public int numTilesFPTLX;

			// Token: 0x0400116F RID: 4463
			public int numTilesFPTLY;

			// Token: 0x04001170 RID: 4464
			public int numTilesFPTL;

			// Token: 0x04001171 RID: 4465
			public ComputeShader buildPerVoxelLightListShader;

			// Token: 0x04001172 RID: 4466
			public int buildPerVoxelLightListKernel;

			// Token: 0x04001173 RID: 4467
			public int numTilesClusterX;

			// Token: 0x04001174 RID: 4468
			public int numTilesClusterY;

			// Token: 0x04001175 RID: 4469
			public float clusterScale;

			// Token: 0x04001176 RID: 4470
			public ComputeShader buildMaterialFlagsShader;

			// Token: 0x04001177 RID: 4471
			public ComputeShader clearDispatchIndirectShader;

			// Token: 0x04001178 RID: 4472
			public ComputeShader buildDispatchIndirectShader;

			// Token: 0x04001179 RID: 4473
			public bool useComputeAsPixel;
		}

		// Token: 0x020001A8 RID: 424
		private struct BuildGPULightListResources
		{
			// Token: 0x0400117A RID: 4474
			public HDRenderPipeline.TileAndClusterData tileAndClusterData;

			// Token: 0x0400117B RID: 4475
			public RTHandle depthBuffer;

			// Token: 0x0400117C RID: 4476
			public RTHandle stencilTexture;

			// Token: 0x0400117D RID: 4477
			public RTHandle[] gBuffer;
		}

		// Token: 0x020001A9 RID: 425
		private struct LightDataGlobalParameters
		{
			// Token: 0x0400117E RID: 4478
			public HDCamera hdCamera;

			// Token: 0x0400117F RID: 4479
			public HDRenderPipeline.LightList lightList;

			// Token: 0x04001180 RID: 4480
			public HDRenderPipeline.LightLoopTextureCaches textureCaches;

			// Token: 0x04001181 RID: 4481
			public HDRenderPipeline.LightLoopLightData lightData;
		}

		// Token: 0x020001AA RID: 426
		private struct ShadowGlobalParameters
		{
			// Token: 0x04001182 RID: 4482
			public HDCamera hdCamera;

			// Token: 0x04001183 RID: 4483
			public HDShadowManager shadowManager;

			// Token: 0x04001184 RID: 4484
			public int sunLightIndex;
		}

		// Token: 0x020001AB RID: 427
		private struct LightLoopGlobalParameters
		{
			// Token: 0x04001185 RID: 4485
			public HDCamera hdCamera;

			// Token: 0x04001186 RID: 4486
			public HDRenderPipeline.TileAndClusterData tileAndClusterData;

			// Token: 0x04001187 RID: 4487
			public float clusterScale;
		}

		// Token: 0x020001AC RID: 428
		private struct ContactShadowsParameters
		{
			// Token: 0x04001188 RID: 4488
			public ComputeShader contactShadowsCS;

			// Token: 0x04001189 RID: 4489
			public int kernel;

			// Token: 0x0400118A RID: 4490
			public Vector4 params1;

			// Token: 0x0400118B RID: 4491
			public Vector4 params2;

			// Token: 0x0400118C RID: 4492
			public int sampleCount;

			// Token: 0x0400118D RID: 4493
			public int numTilesX;

			// Token: 0x0400118E RID: 4494
			public int numTilesY;

			// Token: 0x0400118F RID: 4495
			public int viewCount;

			// Token: 0x04001190 RID: 4496
			public bool rayTracingEnabled;

			// Token: 0x04001191 RID: 4497
			public RayTracingShader contactShadowsRTS;

			// Token: 0x04001192 RID: 4498
			public RayTracingAccelerationStructure accelerationStructure;

			// Token: 0x04001193 RID: 4499
			public float rayTracingBias;

			// Token: 0x04001194 RID: 4500
			public int actualWidth;

			// Token: 0x04001195 RID: 4501
			public int actualHeight;

			// Token: 0x04001196 RID: 4502
			public int depthTextureParameterName;
		}

		// Token: 0x020001AD RID: 429
		private struct DeferredLightingParameters
		{
			// Token: 0x04001197 RID: 4503
			public int numTilesX;

			// Token: 0x04001198 RID: 4504
			public int numTilesY;

			// Token: 0x04001199 RID: 4505
			public int numTiles;

			// Token: 0x0400119A RID: 4506
			public bool enableTile;

			// Token: 0x0400119B RID: 4507
			public bool outputSplitLighting;

			// Token: 0x0400119C RID: 4508
			public bool useComputeLightingEvaluation;

			// Token: 0x0400119D RID: 4509
			public bool enableFeatureVariants;

			// Token: 0x0400119E RID: 4510
			public bool enableShadowMasks;

			// Token: 0x0400119F RID: 4511
			public int numVariants;

			// Token: 0x040011A0 RID: 4512
			public DebugDisplaySettings debugDisplaySettings;

			// Token: 0x040011A1 RID: 4513
			public ComputeShader deferredComputeShader;

			// Token: 0x040011A2 RID: 4514
			public int viewCount;

			// Token: 0x040011A3 RID: 4515
			public Material splitLightingMat;

			// Token: 0x040011A4 RID: 4516
			public Material regularLightingMat;
		}

		// Token: 0x020001AE RID: 430
		private struct DeferredLightingResources
		{
			// Token: 0x040011A5 RID: 4517
			public RenderTargetIdentifier[] colorBuffers;

			// Token: 0x040011A6 RID: 4518
			public RTHandle depthStencilBuffer;

			// Token: 0x040011A7 RID: 4519
			public RTHandle depthTexture;

			// Token: 0x040011A8 RID: 4520
			public ComputeBuffer lightListBuffer;

			// Token: 0x040011A9 RID: 4521
			public ComputeBuffer tileFeatureFlagsBuffer;

			// Token: 0x040011AA RID: 4522
			public ComputeBuffer tileListBuffer;

			// Token: 0x040011AB RID: 4523
			public ComputeBuffer dispatchIndirectBuffer;
		}

		// Token: 0x020001AF RID: 431
		private struct LightLoopDebugOverlayParameters
		{
			// Token: 0x040011AC RID: 4524
			public Material debugViewTilesMaterial;

			// Token: 0x040011AD RID: 4525
			public HDRenderPipeline.TileAndClusterData tileAndClusterData;

			// Token: 0x040011AE RID: 4526
			public HDShadowManager shadowManager;

			// Token: 0x040011AF RID: 4527
			public int debugSelectedLightShadowIndex;

			// Token: 0x040011B0 RID: 4528
			public int debugSelectedLightShadowCount;

			// Token: 0x040011B1 RID: 4529
			public Material debugShadowMapMaterial;

			// Token: 0x040011B2 RID: 4530
			public Material debugBlitMaterial;

			// Token: 0x040011B3 RID: 4531
			public LightCookieManager cookieManager;

			// Token: 0x040011B4 RID: 4532
			public PlanarReflectionProbeCache planarProbeCache;
		}

		// Token: 0x020001B0 RID: 432
		private enum ScreenSpaceShadowType
		{
			// Token: 0x040011B6 RID: 4534
			GrayScale,
			// Token: 0x040011B7 RID: 4535
			Area,
			// Token: 0x040011B8 RID: 4536
			Color
		}

		// Token: 0x020001B1 RID: 433
		private struct VolumeVoxelizationParameters
		{
			// Token: 0x040011B9 RID: 4537
			public ComputeShader voxelizationCS;

			// Token: 0x040011BA RID: 4538
			public int voxelizationKernel;

			// Token: 0x040011BB RID: 4539
			public Vector4 resolution;

			// Token: 0x040011BC RID: 4540
			public int numBigTileX;

			// Token: 0x040011BD RID: 4541
			public int numBigTileY;

			// Token: 0x040011BE RID: 4542
			public int viewCount;

			// Token: 0x040011BF RID: 4543
			public bool tiledLighting;

			// Token: 0x040011C0 RID: 4544
			public float unitDepthTexelSpacing;

			// Token: 0x040011C1 RID: 4545
			public int numVisibleVolumes;

			// Token: 0x040011C2 RID: 4546
			public Texture3D volumeAtlas;

			// Token: 0x040011C3 RID: 4547
			public Vector4 volumeAtlasDimensions;

			// Token: 0x040011C4 RID: 4548
			public Matrix4x4[] pixelCoordToViewDirWS;
		}

		// Token: 0x020001B2 RID: 434
		private struct VolumetricLightingParameters
		{
			// Token: 0x040011C5 RID: 4549
			public ComputeShader volumetricLightingCS;

			// Token: 0x040011C6 RID: 4550
			public int volumetricLightingKernel;

			// Token: 0x040011C7 RID: 4551
			public int volumetricFilteringKernelX;

			// Token: 0x040011C8 RID: 4552
			public int volumetricFilteringKernelY;

			// Token: 0x040011C9 RID: 4553
			public bool tiledLighting;

			// Token: 0x040011CA RID: 4554
			public Vector4 resolution;

			// Token: 0x040011CB RID: 4555
			public int numBigTileX;

			// Token: 0x040011CC RID: 4556
			public int numBigTileY;

			// Token: 0x040011CD RID: 4557
			public float unitDepthTexelSpacing;

			// Token: 0x040011CE RID: 4558
			public float anisotropy;

			// Token: 0x040011CF RID: 4559
			public Vector4 xySeqOffset;

			// Token: 0x040011D0 RID: 4560
			public bool enableReprojection;

			// Token: 0x040011D1 RID: 4561
			public bool historyIsValid;

			// Token: 0x040011D2 RID: 4562
			public int viewCount;

			// Token: 0x040011D3 RID: 4563
			public bool filterVolume;

			// Token: 0x040011D4 RID: 4564
			public Matrix4x4[] pixelCoordToViewDirWS;
		}

		// Token: 0x020001B3 RID: 435
		private struct SubsurfaceScatteringParameters
		{
			// Token: 0x040011D5 RID: 4565
			public ComputeShader subsurfaceScatteringCS;

			// Token: 0x040011D6 RID: 4566
			public int subsurfaceScatteringCSKernel;

			// Token: 0x040011D7 RID: 4567
			public bool needTemporaryBuffer;

			// Token: 0x040011D8 RID: 4568
			public Material copyStencilForSplitLighting;

			// Token: 0x040011D9 RID: 4569
			public Material combineLighting;

			// Token: 0x040011DA RID: 4570
			public uint texturingModeFlags;

			// Token: 0x040011DB RID: 4571
			public int numTilesX;

			// Token: 0x040011DC RID: 4572
			public int numTilesY;

			// Token: 0x040011DD RID: 4573
			public int numTilesZ;

			// Token: 0x040011DE RID: 4574
			public Vector4[] worldScales;

			// Token: 0x040011DF RID: 4575
			public Vector4[] filterKernels;

			// Token: 0x040011E0 RID: 4576
			public Vector4[] shapeParams;

			// Token: 0x040011E1 RID: 4577
			public float[] diffusionProfileHashes;
		}

		// Token: 0x020001B4 RID: 436
		private struct SubsurfaceScatteringResources
		{
			// Token: 0x040011E2 RID: 4578
			public RTHandle colorBuffer;

			// Token: 0x040011E3 RID: 4579
			public RTHandle diffuseBuffer;

			// Token: 0x040011E4 RID: 4580
			public RTHandle depthStencilBuffer;

			// Token: 0x040011E5 RID: 4581
			public RTHandle depthTexture;

			// Token: 0x040011E6 RID: 4582
			public RTHandle cameraFilteringBuffer;

			// Token: 0x040011E7 RID: 4583
			public ComputeBuffer coarseStencilBuffer;

			// Token: 0x040011E8 RID: 4584
			public RTHandle sssBuffer;
		}

		// Token: 0x020001B5 RID: 437
		private class ResolveFullScreenDebugPassData
		{
			// Token: 0x040011E9 RID: 4585
			public HDRenderPipeline.DebugParameters debugParameters;

			// Token: 0x040011EA RID: 4586
			public RenderGraphMutableResource output;

			// Token: 0x040011EB RID: 4587
			public RenderGraphResource input;

			// Token: 0x040011EC RID: 4588
			public RenderGraphResource depthPyramid;
		}

		// Token: 0x020001B6 RID: 438
		private class ResolveColorPickerDebugPassData
		{
			// Token: 0x040011ED RID: 4589
			public HDRenderPipeline.DebugParameters debugParameters;

			// Token: 0x040011EE RID: 4590
			public RenderGraphMutableResource output;

			// Token: 0x040011EF RID: 4591
			public RenderGraphResource input;
		}

		// Token: 0x020001B7 RID: 439
		private class RenderDebugOverlayPassData
		{
			// Token: 0x040011F0 RID: 4592
			public HDRenderPipeline.DebugParameters debugParameters;

			// Token: 0x040011F1 RID: 4593
			public RenderGraphMutableResource colorBuffer;

			// Token: 0x040011F2 RID: 4594
			public RenderGraphMutableResource depthBuffer;

			// Token: 0x040011F3 RID: 4595
			public RenderGraphResource depthPyramidTexture;

			// Token: 0x040011F4 RID: 4596
			public ShadowResult shadowTextures;
		}

		// Token: 0x020001B8 RID: 440
		private class RenderLightVolumesPassData
		{
			// Token: 0x040011F5 RID: 4597
			public DebugLightVolumes.RenderLightVolumesParameters parameters;

			// Token: 0x040011F6 RID: 4598
			public RenderGraphMutableResource lightCountBuffer;

			// Token: 0x040011F7 RID: 4599
			public RenderGraphMutableResource colorAccumulationBuffer;

			// Token: 0x040011F8 RID: 4600
			public RenderGraphMutableResource debugLightVolumesTexture;

			// Token: 0x040011F9 RID: 4601
			public RenderGraphMutableResource depthBuffer;

			// Token: 0x040011FA RID: 4602
			public RenderGraphMutableResource destination;
		}

		// Token: 0x020001B9 RID: 441
		private class DebugViewMaterialData
		{
			// Token: 0x040011FB RID: 4603
			public RenderGraphMutableResource outputColor;

			// Token: 0x040011FC RID: 4604
			public RenderGraphMutableResource outputDepth;

			// Token: 0x040011FD RID: 4605
			public RenderGraphResource opaqueRendererList;

			// Token: 0x040011FE RID: 4606
			public RenderGraphResource transparentRendererList;

			// Token: 0x040011FF RID: 4607
			public Material debugGBufferMaterial;

			// Token: 0x04001200 RID: 4608
			public FrameSettings frameSettings;
		}

		// Token: 0x020001BA RID: 442
		private class PushFullScreenDebugPassData
		{
			// Token: 0x04001201 RID: 4609
			public RenderGraphResource input;

			// Token: 0x04001202 RID: 4610
			public RenderGraphMutableResource output;

			// Token: 0x04001203 RID: 4611
			public Vector4 scaleBias;

			// Token: 0x04001204 RID: 4612
			public int mipIndex;
		}

		// Token: 0x020001BB RID: 443
		private struct LightingBuffers
		{
			// Token: 0x04001205 RID: 4613
			public RenderGraphMutableResource sssBuffer;

			// Token: 0x04001206 RID: 4614
			public RenderGraphMutableResource diffuseLightingBuffer;

			// Token: 0x04001207 RID: 4615
			public RenderGraphResource ambientOcclusionBuffer;

			// Token: 0x04001208 RID: 4616
			public RenderGraphResource ssrLightingBuffer;

			// Token: 0x04001209 RID: 4617
			public RenderGraphResource contactShadowsBuffer;
		}

		// Token: 0x020001BC RID: 444
		private class BuildGPULightListPassData
		{
			// Token: 0x0400120A RID: 4618
			public HDRenderPipeline.LightDataGlobalParameters lightDataGlobalParameters;

			// Token: 0x0400120B RID: 4619
			public HDRenderPipeline.ShadowGlobalParameters shadowGlobalParameters;

			// Token: 0x0400120C RID: 4620
			public HDRenderPipeline.LightLoopGlobalParameters lightLoopGlobalParameters;

			// Token: 0x0400120D RID: 4621
			public HDRenderPipeline.BuildGPULightListParameters buildGPULightListParameters;

			// Token: 0x0400120E RID: 4622
			public HDRenderPipeline.BuildGPULightListResources buildGPULightListResources;

			// Token: 0x0400120F RID: 4623
			public RenderGraphResource depthBuffer;

			// Token: 0x04001210 RID: 4624
			public RenderGraphResource stencilTexture;

			// Token: 0x04001211 RID: 4625
			public RenderGraphResource[] gBuffer = new RenderGraphResource[RenderGraph.kMaxMRTCount];

			// Token: 0x04001212 RID: 4626
			public int gBufferCount;
		}

		// Token: 0x020001BD RID: 445
		private class PushGlobalCameraParamPassData
		{
			// Token: 0x04001213 RID: 4627
			public HDCamera hdCamera;

			// Token: 0x04001214 RID: 4628
			public int frameCount;
		}

		// Token: 0x020001BE RID: 446
		private class DeferredLightingPassData
		{
			// Token: 0x04001215 RID: 4629
			public HDRenderPipeline.DeferredLightingParameters parameters;

			// Token: 0x04001216 RID: 4630
			public HDRenderPipeline.DeferredLightingResources resources;

			// Token: 0x04001217 RID: 4631
			public RenderGraphMutableResource colorBuffer;

			// Token: 0x04001218 RID: 4632
			public RenderGraphMutableResource sssDiffuseLightingBuffer;

			// Token: 0x04001219 RID: 4633
			public RenderGraphResource depthBuffer;

			// Token: 0x0400121A RID: 4634
			public RenderGraphResource depthTexture;

			// Token: 0x0400121B RID: 4635
			public int gbufferCount;

			// Token: 0x0400121C RID: 4636
			public RenderGraphResource[] gbuffer = new RenderGraphResource[8];
		}

		// Token: 0x020001BF RID: 447
		private struct LightingOutput
		{
			// Token: 0x0400121D RID: 4637
			public RenderGraphMutableResource colorBuffer;
		}

		// Token: 0x020001C0 RID: 448
		private class RenderSSRPassData
		{
			// Token: 0x0400121E RID: 4638
			public HDRenderPipeline.RenderSSRParameters parameters;

			// Token: 0x0400121F RID: 4639
			public RenderGraphResource depthPyramid;

			// Token: 0x04001220 RID: 4640
			public RenderGraphResource colorPyramid;

			// Token: 0x04001221 RID: 4641
			public RenderGraphResource stencilBuffer;

			// Token: 0x04001222 RID: 4642
			public RenderGraphMutableResource hitPointsTexture;

			// Token: 0x04001223 RID: 4643
			public RenderGraphMutableResource lightingTexture;

			// Token: 0x04001224 RID: 4644
			public RenderGraphResource clearCoatMask;
		}

		// Token: 0x020001C1 RID: 449
		private class RenderContactShadowPassData
		{
			// Token: 0x04001225 RID: 4645
			public HDRenderPipeline.ContactShadowsParameters parameters;

			// Token: 0x04001226 RID: 4646
			public HDRenderPipeline.LightLoopLightData lightLoopLightData;

			// Token: 0x04001227 RID: 4647
			public HDRenderPipeline.TileAndClusterData tileAndClusterData;

			// Token: 0x04001228 RID: 4648
			public RenderGraphResource depthTexture;

			// Token: 0x04001229 RID: 4649
			public RenderGraphMutableResource contactShadowsTexture;

			// Token: 0x0400122A RID: 4650
			public HDShadowManager shadowManager;
		}

		// Token: 0x020001C2 RID: 450
		private class VolumeVoxelizationPassData
		{
			// Token: 0x0400122B RID: 4651
			public HDRenderPipeline.VolumeVoxelizationParameters parameters;

			// Token: 0x0400122C RID: 4652
			public RenderGraphMutableResource densityBuffer;

			// Token: 0x0400122D RID: 4653
			public ComputeBuffer visibleVolumeBoundsBuffer;

			// Token: 0x0400122E RID: 4654
			public ComputeBuffer visibleVolumeDataBuffer;

			// Token: 0x0400122F RID: 4655
			public ComputeBuffer bigTileLightListBuffer;
		}

		// Token: 0x020001C3 RID: 451
		private class VolumetricLightingPassData
		{
			// Token: 0x04001230 RID: 4656
			public HDRenderPipeline.VolumetricLightingParameters parameters;

			// Token: 0x04001231 RID: 4657
			public RenderGraphResource densityBuffer;

			// Token: 0x04001232 RID: 4658
			public RenderGraphMutableResource lightingBuffer;

			// Token: 0x04001233 RID: 4659
			public RenderGraphResource historyBuffer;

			// Token: 0x04001234 RID: 4660
			public RenderGraphMutableResource feedbackBuffer;

			// Token: 0x04001235 RID: 4661
			public ComputeBuffer bigTileLightListBuffer;
		}

		// Token: 0x020001C4 RID: 452
		private struct LookDevDataForHDRP
		{
			// Token: 0x04001236 RID: 4662
			public HDAdditionalCameraData additionalCameraData;

			// Token: 0x04001237 RID: 4663
			public HDAdditionalLightData additionalLightData;

			// Token: 0x04001238 RID: 4664
			public VisualEnvironment visualEnvironment;

			// Token: 0x04001239 RID: 4665
			public HDRISky sky;

			// Token: 0x0400123A RID: 4666
			public Volume volume;
		}

		// Token: 0x020001C5 RID: 453
		private struct PrepassOutput
		{
			// Token: 0x0400123B RID: 4667
			public RenderGraphMutableResource depthBuffer;

			// Token: 0x0400123C RID: 4668
			public RenderGraphMutableResource depthAsColor;

			// Token: 0x0400123D RID: 4669
			public RenderGraphMutableResource normalBuffer;

			// Token: 0x0400123E RID: 4670
			public RenderGraphMutableResource motionVectorsBuffer;

			// Token: 0x0400123F RID: 4671
			public HDRenderPipeline.GBufferOutput gbuffer;

			// Token: 0x04001240 RID: 4672
			public HDRenderPipeline.DBufferOutput dbuffer;

			// Token: 0x04001241 RID: 4673
			public RenderGraphMutableResource depthValuesMSAA;

			// Token: 0x04001242 RID: 4674
			public RenderGraphMutableResource resolvedDepthBuffer;

			// Token: 0x04001243 RID: 4675
			public RenderGraphMutableResource resolvedNormalBuffer;

			// Token: 0x04001244 RID: 4676
			public RenderGraphMutableResource resolvedMotionVectorsBuffer;

			// Token: 0x04001245 RID: 4677
			public RenderGraphMutableResource depthPyramidTexture;

			// Token: 0x04001246 RID: 4678
			public RenderGraphResource stencilBuffer;
		}

		// Token: 0x020001C6 RID: 454
		private class DepthPrepassData
		{
			// Token: 0x04001247 RID: 4679
			public FrameSettings frameSettings;

			// Token: 0x04001248 RID: 4680
			public bool msaaEnabled;

			// Token: 0x04001249 RID: 4681
			public bool hasDepthOnlyPrepass;

			// Token: 0x0400124A RID: 4682
			public bool renderRayTracingPrepass;

			// Token: 0x0400124B RID: 4683
			public RenderGraphMutableResource depthBuffer;

			// Token: 0x0400124C RID: 4684
			public RenderGraphMutableResource depthAsColorBuffer;

			// Token: 0x0400124D RID: 4685
			public RenderGraphMutableResource normalBuffer;

			// Token: 0x0400124E RID: 4686
			public RenderGraphResource rendererListMRT;

			// Token: 0x0400124F RID: 4687
			public RenderGraphResource rendererListDepthOnly;

			// Token: 0x04001250 RID: 4688
			public RenderGraphResource renderListRayTracingOpaque;

			// Token: 0x04001251 RID: 4689
			public RenderGraphResource renderListRayTracingTransparent;
		}

		// Token: 0x020001C7 RID: 455
		private class ObjectMotionVectorsPassData
		{
			// Token: 0x04001252 RID: 4690
			public FrameSettings frameSettings;

			// Token: 0x04001253 RID: 4691
			public RenderGraphMutableResource depthBuffer;

			// Token: 0x04001254 RID: 4692
			public RenderGraphMutableResource motionVectorsBuffer;

			// Token: 0x04001255 RID: 4693
			public RenderGraphMutableResource normalBuffer;

			// Token: 0x04001256 RID: 4694
			public RenderGraphMutableResource depthAsColorMSAABuffer;

			// Token: 0x04001257 RID: 4695
			public RenderGraphResource rendererList;
		}

		// Token: 0x020001C8 RID: 456
		private class GBufferPassData
		{
			// Token: 0x04001258 RID: 4696
			public FrameSettings frameSettings;

			// Token: 0x04001259 RID: 4697
			public RenderGraphResource rendererList;

			// Token: 0x0400125A RID: 4698
			public RenderGraphMutableResource[] gbufferRT = new RenderGraphMutableResource[RenderGraph.kMaxMRTCount];

			// Token: 0x0400125B RID: 4699
			public RenderGraphMutableResource depthBuffer;
		}

		// Token: 0x020001C9 RID: 457
		private struct GBufferOutput
		{
			// Token: 0x0400125C RID: 4700
			public RenderGraphResource[] mrt;

			// Token: 0x0400125D RID: 4701
			public int gBufferCount;
		}

		// Token: 0x020001CA RID: 458
		private class ResolvePrepassData
		{
			// Token: 0x0400125E RID: 4702
			public RenderGraphMutableResource depthBuffer;

			// Token: 0x0400125F RID: 4703
			public RenderGraphMutableResource depthValuesBuffer;

			// Token: 0x04001260 RID: 4704
			public RenderGraphMutableResource normalBuffer;

			// Token: 0x04001261 RID: 4705
			public RenderGraphMutableResource motionVectorsBuffer;

			// Token: 0x04001262 RID: 4706
			public RenderGraphResource depthAsColorBufferMSAA;

			// Token: 0x04001263 RID: 4707
			public RenderGraphResource normalBufferMSAA;

			// Token: 0x04001264 RID: 4708
			public RenderGraphResource motionVectorBufferMSAA;

			// Token: 0x04001265 RID: 4709
			public Material depthResolveMaterial;

			// Token: 0x04001266 RID: 4710
			public int depthResolvePassIndex;
		}

		// Token: 0x020001CB RID: 459
		private class CopyDepthPassData
		{
			// Token: 0x04001267 RID: 4711
			public RenderGraphResource inputDepth;

			// Token: 0x04001268 RID: 4712
			public RenderGraphMutableResource outputDepth;

			// Token: 0x04001269 RID: 4713
			public GPUCopy GPUCopy;

			// Token: 0x0400126A RID: 4714
			public int width;

			// Token: 0x0400126B RID: 4715
			public int height;
		}

		// Token: 0x020001CC RID: 460
		private class ResolveStencilPassData
		{
			// Token: 0x0400126C RID: 4716
			public RenderGraphResource inputDepth;

			// Token: 0x0400126D RID: 4717
			public RenderGraphMutableResource resolvedStencil;

			// Token: 0x0400126E RID: 4718
			public ComputeBuffer coarseStencilBuffer;
		}

		// Token: 0x020001CD RID: 461
		private class RenderDBufferPassData
		{
			// Token: 0x0400126F RID: 4719
			public RenderGraphMutableResource[] mrt = new RenderGraphMutableResource[Decal.GetMaterialDBufferCount()];

			// Token: 0x04001270 RID: 4720
			public int dBufferCount;

			// Token: 0x04001271 RID: 4721
			public RenderGraphResource meshDecalsRendererList;

			// Token: 0x04001272 RID: 4722
			public RenderGraphMutableResource depthStencilBuffer;
		}

		// Token: 0x020001CE RID: 462
		private struct DBufferOutput
		{
			// Token: 0x04001273 RID: 4723
			public RenderGraphResource[] mrt;

			// Token: 0x04001274 RID: 4724
			public int dBufferCount;
		}

		// Token: 0x020001CF RID: 463
		private class DBufferNormalPatchData
		{
			// Token: 0x04001275 RID: 4725
			public HDRenderPipeline.DBufferNormalPatchParameters parameters;

			// Token: 0x04001276 RID: 4726
			public RenderGraphResource depthStencilBuffer;

			// Token: 0x04001277 RID: 4727
			public RenderGraphMutableResource normalBuffer;
		}

		// Token: 0x020001D0 RID: 464
		private class GenerateDepthPyramidPassData
		{
			// Token: 0x04001278 RID: 4728
			public RenderGraphMutableResource depthTexture;

			// Token: 0x04001279 RID: 4729
			public HDUtils.PackedMipChainInfo mipInfo;

			// Token: 0x0400127A RID: 4730
			public MipGenerator mipGenerator;
		}

		// Token: 0x020001D1 RID: 465
		private class CameraMotionVectorsPassData
		{
			// Token: 0x0400127B RID: 4731
			public Material cameraMotionVectorsMaterial;

			// Token: 0x0400127C RID: 4732
			public RenderGraphMutableResource motionVectorsBuffer;

			// Token: 0x0400127D RID: 4733
			public RenderGraphResource depthTexture;
		}

		// Token: 0x020001D2 RID: 466
		private class FinalBlitPassData
		{
			// Token: 0x0400127E RID: 4734
			public HDRenderPipeline.BlitFinalCameraTextureParameters parameters;

			// Token: 0x0400127F RID: 4735
			public RenderGraphResource source;

			// Token: 0x04001280 RID: 4736
			public RenderTargetIdentifier destination;
		}

		// Token: 0x020001D3 RID: 467
		private class SetFinalTargetPassData
		{
			// Token: 0x04001281 RID: 4737
			public bool copyDepth;

			// Token: 0x04001282 RID: 4738
			public Material copyDepthMaterial;

			// Token: 0x04001283 RID: 4739
			public RenderTargetIdentifier finalTarget;

			// Token: 0x04001284 RID: 4740
			public Rect finalViewport;

			// Token: 0x04001285 RID: 4741
			public RenderGraphResource depthBuffer;

			// Token: 0x04001286 RID: 4742
			public bool flipY;
		}

		// Token: 0x020001D4 RID: 468
		private class ForwardPassData
		{
			// Token: 0x04001287 RID: 4743
			public RenderGraphResource rendererList;

			// Token: 0x04001288 RID: 4744
			public RenderGraphMutableResource[] renderTarget = new RenderGraphMutableResource[3];

			// Token: 0x04001289 RID: 4745
			public int renderTargetCount;

			// Token: 0x0400128A RID: 4746
			public RenderGraphMutableResource depthBuffer;

			// Token: 0x0400128B RID: 4747
			public ComputeBuffer lightListBuffer;

			// Token: 0x0400128C RID: 4748
			public FrameSettings frameSettings;

			// Token: 0x0400128D RID: 4749
			public bool decalsEnabled;

			// Token: 0x0400128E RID: 4750
			public bool renderMotionVecForTransparent;
		}

		// Token: 0x020001D5 RID: 469
		private class DownsampleDepthForLowResPassData
		{
			// Token: 0x0400128F RID: 4751
			public Material downsampleDepthMaterial;

			// Token: 0x04001290 RID: 4752
			public RenderGraphResource depthTexture;

			// Token: 0x04001291 RID: 4753
			public RenderGraphMutableResource downsampledDepthBuffer;
		}

		// Token: 0x020001D6 RID: 470
		private class RenderLowResTransparentPassData
		{
			// Token: 0x04001292 RID: 4754
			public FrameSettings frameSettings;

			// Token: 0x04001293 RID: 4755
			public RenderGraphResource rendererList;

			// Token: 0x04001294 RID: 4756
			public RenderGraphMutableResource lowResBuffer;

			// Token: 0x04001295 RID: 4757
			public RenderGraphMutableResource downsampledDepthBuffer;
		}

		// Token: 0x020001D7 RID: 471
		private class UpsampleTransparentPassData
		{
			// Token: 0x04001296 RID: 4758
			public Material upsampleMaterial;

			// Token: 0x04001297 RID: 4759
			public RenderGraphMutableResource colorBuffer;

			// Token: 0x04001298 RID: 4760
			public RenderGraphResource lowResTransparentBuffer;

			// Token: 0x04001299 RID: 4761
			public RenderGraphResource downsampledDepthBuffer;
		}

		// Token: 0x020001D8 RID: 472
		private class RenderForwardEmissivePassData
		{
			// Token: 0x0400129A RID: 4762
			public RenderGraphResource rendererList;
		}

		// Token: 0x020001D9 RID: 473
		private class RenderSkyPassData
		{
			// Token: 0x0400129B RID: 4763
			public VisualEnvironment visualEnvironment;

			// Token: 0x0400129C RID: 4764
			public Light sunLight;

			// Token: 0x0400129D RID: 4765
			public HDCamera hdCamera;

			// Token: 0x0400129E RID: 4766
			public RenderGraphResource volumetricLighting;

			// Token: 0x0400129F RID: 4767
			public RenderGraphMutableResource colorBuffer;

			// Token: 0x040012A0 RID: 4768
			public RenderGraphMutableResource depthStencilBuffer;

			// Token: 0x040012A1 RID: 4769
			public RenderGraphMutableResource intermediateBuffer;

			// Token: 0x040012A2 RID: 4770
			public DebugDisplaySettings debugDisplaySettings;

			// Token: 0x040012A3 RID: 4771
			public SkyManager skyManager;

			// Token: 0x040012A4 RID: 4772
			public int frameCount;
		}

		// Token: 0x020001DA RID: 474
		private class GenerateColorPyramidData
		{
			// Token: 0x040012A5 RID: 4773
			public RenderGraphMutableResource colorPyramid;

			// Token: 0x040012A6 RID: 4774
			public RenderGraphResource inputColor;

			// Token: 0x040012A7 RID: 4775
			public MipGenerator mipGenerator;

			// Token: 0x040012A8 RID: 4776
			public HDCamera hdCamera;
		}

		// Token: 0x020001DB RID: 475
		private class AccumulateDistortionPassData
		{
			// Token: 0x040012A9 RID: 4777
			public RenderGraphMutableResource distortionBuffer;

			// Token: 0x040012AA RID: 4778
			public RenderGraphMutableResource depthStencilBuffer;

			// Token: 0x040012AB RID: 4779
			public RenderGraphResource distortionRendererList;

			// Token: 0x040012AC RID: 4780
			public FrameSettings frameSettings;
		}

		// Token: 0x020001DC RID: 476
		private class RenderDistortionPassData
		{
			// Token: 0x040012AD RID: 4781
			public Material applyDistortionMaterial;

			// Token: 0x040012AE RID: 4782
			public RenderGraphResource colorPyramidBuffer;

			// Token: 0x040012AF RID: 4783
			public RenderGraphResource distortionBuffer;

			// Token: 0x040012B0 RID: 4784
			public RenderGraphMutableResource colorBuffer;

			// Token: 0x040012B1 RID: 4785
			public RenderGraphResource depthStencilBuffer;

			// Token: 0x040012B2 RID: 4786
			public Vector4 size;
		}

		// Token: 0x020001DD RID: 477
		private class ResolveColorData
		{
			// Token: 0x040012B3 RID: 4787
			public RenderGraphResource input;

			// Token: 0x040012B4 RID: 4788
			public RenderGraphMutableResource output;

			// Token: 0x040012B5 RID: 4789
			public Material resolveMaterial;

			// Token: 0x040012B6 RID: 4790
			public int passIndex;
		}

		// Token: 0x020001DE RID: 478
		private class XRRenderingPassData
		{
			// Token: 0x040012B7 RID: 4791
			public Camera camera;

			// Token: 0x040012B8 RID: 4792
			public XRPass xr;
		}

		// Token: 0x020001DF RID: 479
		private class EndCameraXRPassData
		{
			// Token: 0x040012B9 RID: 4793
			public HDCamera hdCamera;
		}

		// Token: 0x020001E0 RID: 480
		private class RenderOcclusionMeshesPassData
		{
			// Token: 0x040012BA RID: 4794
			public HDCamera hdCamera;

			// Token: 0x040012BB RID: 4795
			public RenderGraphMutableResource depthBuffer;
		}

		// Token: 0x020001E1 RID: 481
		private class SubsurfaceScaterringPassData
		{
			// Token: 0x040012BC RID: 4796
			public HDRenderPipeline.SubsurfaceScatteringParameters parameters;

			// Token: 0x040012BD RID: 4797
			public RenderGraphResource colorBuffer;

			// Token: 0x040012BE RID: 4798
			public RenderGraphResource diffuseBuffer;

			// Token: 0x040012BF RID: 4799
			public RenderGraphResource depthStencilBuffer;

			// Token: 0x040012C0 RID: 4800
			public RenderGraphResource depthTexture;

			// Token: 0x040012C1 RID: 4801
			public RenderGraphMutableResource cameraFilteringBuffer;

			// Token: 0x040012C2 RID: 4802
			public RenderGraphResource sssBuffer;
		}

		// Token: 0x020001E2 RID: 482
		private struct RenderRequest
		{
			// Token: 0x040012C3 RID: 4803
			public HDCamera hdCamera;

			// Token: 0x040012C4 RID: 4804
			public bool clearCameraSettings;

			// Token: 0x040012C5 RID: 4805
			public HDRenderPipeline.RenderRequest.Target target;

			// Token: 0x040012C6 RID: 4806
			public HDRenderPipeline.HDCullingResults cullingResults;

			// Token: 0x040012C7 RID: 4807
			public int index;

			// Token: 0x040012C8 RID: 4808
			public List<int> dependsOnRenderRequestIndices;

			// Token: 0x040012C9 RID: 4809
			public CameraSettings cameraSettings;

			// Token: 0x020002AD RID: 685
			public struct Target
			{
				// Token: 0x04001730 RID: 5936
				public RenderTargetIdentifier id;

				// Token: 0x04001731 RID: 5937
				public CubemapFace face;

				// Token: 0x04001732 RID: 5938
				public RenderTexture copyToTarget;
			}
		}

		// Token: 0x020001E3 RID: 483
		private struct HDCullingResults
		{
			// Token: 0x06000B98 RID: 2968 RVA: 0x0005551D File Offset: 0x0005371D
			internal void Reset()
			{
				this.hdProbeCullingResults.Reset();
				if (this.decalCullResults != null)
				{
					this.decalCullResults.Clear();
					return;
				}
				this.decalCullResults = GenericPool<DecalSystem.CullResult>.Get();
			}

			// Token: 0x040012CA RID: 4810
			public CullingResults cullingResults;

			// Token: 0x040012CB RID: 4811
			public CullingResults? customPassCullingResults;

			// Token: 0x040012CC RID: 4812
			public HDProbeCullingResults hdProbeCullingResults;

			// Token: 0x040012CD RID: 4813
			public DecalSystem.CullResult decalCullResults;
		}

		// Token: 0x020001E4 RID: 484
		private struct BlitFinalCameraTextureParameters
		{
			// Token: 0x040012CE RID: 4814
			public bool flip;

			// Token: 0x040012CF RID: 4815
			public int srcTexArraySlice;

			// Token: 0x040012D0 RID: 4816
			public int dstTexArraySlice;

			// Token: 0x040012D1 RID: 4817
			public Rect viewport;

			// Token: 0x040012D2 RID: 4818
			public Material blitMaterial;
		}

		// Token: 0x020001E5 RID: 485
		private struct DepthPrepassParameters
		{
			// Token: 0x040012D3 RID: 4819
			public string passName;

			// Token: 0x040012D4 RID: 4820
			public HDProfileId profilingId;

			// Token: 0x040012D5 RID: 4821
			public RendererListDesc depthOnlyRendererListDesc;

			// Token: 0x040012D6 RID: 4822
			public RendererListDesc mrtRendererListDesc;

			// Token: 0x040012D7 RID: 4823
			public bool hasDepthOnlyPass;

			// Token: 0x040012D8 RID: 4824
			public bool shouldRenderMotionVectorAfterGBuffer;

			// Token: 0x040012D9 RID: 4825
			public RendererListDesc rayTracingOpaqueRLDesc;

			// Token: 0x040012DA RID: 4826
			public RendererListDesc rayTracingTransparentRLDesc;

			// Token: 0x040012DB RID: 4827
			public bool renderRayTracingPrepass;
		}

		// Token: 0x020001E6 RID: 486
		private struct DBufferNormalPatchParameters
		{
			// Token: 0x040012DC RID: 4828
			public Material decalNormalBufferMaterial;

			// Token: 0x040012DD RID: 4829
			public int stencilRef;

			// Token: 0x040012DE RID: 4830
			public int stencilMask;
		}

		// Token: 0x020001E7 RID: 487
		private struct RenderSSRParameters
		{
			// Token: 0x040012DF RID: 4831
			public ComputeShader ssrCS;

			// Token: 0x040012E0 RID: 4832
			public int tracingKernel;

			// Token: 0x040012E1 RID: 4833
			public int reprojectionKernel;

			// Token: 0x040012E2 RID: 4834
			public int width;

			// Token: 0x040012E3 RID: 4835
			public int height;

			// Token: 0x040012E4 RID: 4836
			public int viewCount;

			// Token: 0x040012E5 RID: 4837
			public int maxIteration;

			// Token: 0x040012E6 RID: 4838
			public bool reflectSky;

			// Token: 0x040012E7 RID: 4839
			public float thicknessScale;

			// Token: 0x040012E8 RID: 4840
			public float thicknessBias;

			// Token: 0x040012E9 RID: 4841
			public float roughnessFadeEnd;

			// Token: 0x040012EA RID: 4842
			public float roughnessFadeEndTimesRcpLength;

			// Token: 0x040012EB RID: 4843
			public float roughnessFadeRcpLength;

			// Token: 0x040012EC RID: 4844
			public float edgeFadeRcpLength;

			// Token: 0x040012ED RID: 4845
			public int depthPyramidMipCount;

			// Token: 0x040012EE RID: 4846
			public ComputeBuffer offsetBufferData;

			// Token: 0x040012EF RID: 4847
			public ComputeBuffer coarseStencilBuffer;

			// Token: 0x040012F0 RID: 4848
			public Vector4 colorPyramidUVScaleAndLimit;

			// Token: 0x040012F1 RID: 4849
			public int colorPyramidMipCount;
		}

		// Token: 0x020001E8 RID: 488
		private struct DebugParameters
		{
			// Token: 0x040012F2 RID: 4850
			public DebugDisplaySettings debugDisplaySettings;

			// Token: 0x040012F3 RID: 4851
			public HDCamera hdCamera;

			// Token: 0x040012F4 RID: 4852
			public bool resolveFullScreenDebug;

			// Token: 0x040012F5 RID: 4853
			public Material debugFullScreenMaterial;

			// Token: 0x040012F6 RID: 4854
			public int depthPyramidMip;

			// Token: 0x040012F7 RID: 4855
			public ComputeBuffer depthPyramidOffsets;

			// Token: 0x040012F8 RID: 4856
			public Texture skyReflectionTexture;

			// Token: 0x040012F9 RID: 4857
			public Material debugLatlongMaterial;

			// Token: 0x040012FA RID: 4858
			public bool rayTracingSupported;

			// Token: 0x040012FB RID: 4859
			public RayCountManager rayCountManager;

			// Token: 0x040012FC RID: 4860
			public HDRenderPipeline.LightLoopDebugOverlayParameters lightingOverlayParameters;

			// Token: 0x040012FD RID: 4861
			public bool colorPickerEnabled;

			// Token: 0x040012FE RID: 4862
			public Material colorPickerMaterial;
		}

		// Token: 0x020001E9 RID: 489
		private struct DeferredLightingRTParameters
		{
			// Token: 0x040012FF RID: 4863
			public bool rayBinning;

			// Token: 0x04001300 RID: 4864
			public LayerMask layerMask;

			// Token: 0x04001301 RID: 4865
			public float rayBias;

			// Token: 0x04001302 RID: 4866
			public float maxRayLength;

			// Token: 0x04001303 RID: 4867
			public float clampValue;

			// Token: 0x04001304 RID: 4868
			public bool includeSky;

			// Token: 0x04001305 RID: 4869
			public bool diffuseLightingOnly;

			// Token: 0x04001306 RID: 4870
			public bool halfResolution;

			// Token: 0x04001307 RID: 4871
			public int rayCountFlag;

			// Token: 0x04001308 RID: 4872
			public int rayCountType;

			// Token: 0x04001309 RID: 4873
			public bool preExpose;

			// Token: 0x0400130A RID: 4874
			public int width;

			// Token: 0x0400130B RID: 4875
			public int height;

			// Token: 0x0400130C RID: 4876
			public int viewCount;

			// Token: 0x0400130D RID: 4877
			public float fov;

			// Token: 0x0400130E RID: 4878
			public ComputeBuffer rayBinResult;

			// Token: 0x0400130F RID: 4879
			public ComputeBuffer rayBinSizeResult;

			// Token: 0x04001310 RID: 4880
			public RayTracingAccelerationStructure accelerationStructure;

			// Token: 0x04001311 RID: 4881
			public HDRaytracingLightCluster lightCluster;

			// Token: 0x04001312 RID: 4882
			public RayTracingShader gBufferRaytracingRT;

			// Token: 0x04001313 RID: 4883
			public ComputeShader deferredRaytracingCS;

			// Token: 0x04001314 RID: 4884
			public ComputeShader rayBinningCS;
		}

		// Token: 0x020001EA RID: 490
		private struct DeferredLightingRTResources
		{
			// Token: 0x04001315 RID: 4885
			public RTHandle directionBuffer;

			// Token: 0x04001316 RID: 4886
			public RTHandle depthStencilBuffer;

			// Token: 0x04001317 RID: 4887
			public RTHandle normalBuffer;

			// Token: 0x04001318 RID: 4888
			public Texture skyTexture;

			// Token: 0x04001319 RID: 4889
			public RTHandle gbuffer0;

			// Token: 0x0400131A RID: 4890
			public RTHandle gbuffer1;

			// Token: 0x0400131B RID: 4891
			public RTHandle gbuffer2;

			// Token: 0x0400131C RID: 4892
			public RTHandle gbuffer3;

			// Token: 0x0400131D RID: 4893
			public RTHandle distanceBuffer;

			// Token: 0x0400131E RID: 4894
			public RTHandle rayCountTexture;

			// Token: 0x0400131F RID: 4895
			public RTHandle litBuffer;
		}
	}
}
