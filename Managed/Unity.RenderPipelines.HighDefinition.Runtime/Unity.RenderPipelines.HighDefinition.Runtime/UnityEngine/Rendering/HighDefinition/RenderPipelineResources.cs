using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine.Serialization;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x0200012B RID: 299
	[HelpURL("https://docs.unity3d.com/Packages/com.unity.render-pipelines.high-definition@8.0/manual/HDRP-Asset.html")]
	internal class RenderPipelineResources : ScriptableObject, IVersionable<RenderPipelineResources.Version>
	{
		// Token: 0x1700015F RID: 351
		// (get) Token: 0x0600090F RID: 2319 RVA: 0x00049F2C File Offset: 0x0004812C
		// (set) Token: 0x06000910 RID: 2320 RVA: 0x00049F34 File Offset: 0x00048134
		RenderPipelineResources.Version IVersionable<RenderPipelineResources.Version>.version
		{
			get
			{
				return this.m_Version;
			}
			set
			{
				this.m_Version = value;
			}
		}

		// Token: 0x04000DDE RID: 3550
		[HideInInspector]
		[SerializeField]
		[FormerlySerializedAs("version")]
		private RenderPipelineResources.Version m_Version = MigrationDescription.LastVersion<RenderPipelineResources.Version>();

		// Token: 0x04000DDF RID: 3551
		public RenderPipelineResources.ShaderResources shaders;

		// Token: 0x04000DE0 RID: 3552
		public RenderPipelineResources.MaterialResources materials;

		// Token: 0x04000DE1 RID: 3553
		public RenderPipelineResources.TextureResources textures;

		// Token: 0x04000DE2 RID: 3554
		public RenderPipelineResources.ShaderGraphResources shaderGraphs;

		// Token: 0x04000DE3 RID: 3555
		public RenderPipelineResources.AssetResources assets;

		// Token: 0x0200027A RID: 634
		private enum Version
		{
			// Token: 0x0400163F RID: 5695
			None,
			// Token: 0x04001640 RID: 5696
			First,
			// Token: 0x04001641 RID: 5697
			RemovedEditorOnlyResources = 4
		}

		// Token: 0x0200027B RID: 635
		[ReloadGroup]
		[Serializable]
		public sealed class ShaderResources
		{
			// Token: 0x06000C92 RID: 3218 RVA: 0x000598EF File Offset: 0x00057AEF
			public IEnumerable<ComputeShader> GetAllComputeShaders()
			{
				FieldInfo[] fields = typeof(RenderPipelineResources.ShaderResources).GetFields(BindingFlags.Instance | BindingFlags.Public);
				FieldInfo[] array = fields;
				for (int i = 0; i < array.Length; i++)
				{
					ComputeShader computeShader;
					if ((computeShader = array[i].GetValue(this) as ComputeShader) != null)
					{
						yield return computeShader;
					}
				}
				array = null;
				yield break;
			}

			// Token: 0x04001642 RID: 5698
			[Reload("Runtime/Material/Lit/Lit.shader", ReloadAttribute.Package.Root)]
			public Shader defaultPS;

			// Token: 0x04001643 RID: 5699
			[Reload("Runtime/Debug/DebugDisplayLatlong.Shader", ReloadAttribute.Package.Root)]
			public Shader debugDisplayLatlongPS;

			// Token: 0x04001644 RID: 5700
			[Reload("Runtime/Debug/DebugViewMaterialGBuffer.Shader", ReloadAttribute.Package.Root)]
			public Shader debugViewMaterialGBufferPS;

			// Token: 0x04001645 RID: 5701
			[Reload("Runtime/Debug/DebugViewTiles.Shader", ReloadAttribute.Package.Root)]
			public Shader debugViewTilesPS;

			// Token: 0x04001646 RID: 5702
			[Reload("Runtime/Debug/DebugFullScreen.Shader", ReloadAttribute.Package.Root)]
			public Shader debugFullScreenPS;

			// Token: 0x04001647 RID: 5703
			[Reload("Runtime/Debug/DebugColorPicker.Shader", ReloadAttribute.Package.Root)]
			public Shader debugColorPickerPS;

			// Token: 0x04001648 RID: 5704
			[Reload("Runtime/Debug/DebugLightVolumes.Shader", ReloadAttribute.Package.Root)]
			public Shader debugLightVolumePS;

			// Token: 0x04001649 RID: 5705
			[Reload("Runtime/Debug/DebugLightVolumes.compute", ReloadAttribute.Package.Root)]
			public ComputeShader debugLightVolumeCS;

			// Token: 0x0400164A RID: 5706
			[Reload("Runtime/Debug/DebugBlitQuad.Shader", ReloadAttribute.Package.Root)]
			public Shader debugBlitQuad;

			// Token: 0x0400164B RID: 5707
			[Reload("Runtime/Lighting/Deferred.Shader", ReloadAttribute.Package.Root)]
			public Shader deferredPS;

			// Token: 0x0400164C RID: 5708
			[Reload("Runtime/RenderPipeline/RenderPass/ColorPyramidPS.Shader", ReloadAttribute.Package.Root)]
			public Shader colorPyramidPS;

			// Token: 0x0400164D RID: 5709
			[Reload("Runtime/RenderPipeline/RenderPass/DepthPyramid.compute", ReloadAttribute.Package.Root)]
			public ComputeShader depthPyramidCS;

			// Token: 0x0400164E RID: 5710
			[Reload("Runtime/Core/CoreResources/GPUCopy.compute", ReloadAttribute.Package.Root)]
			public ComputeShader copyChannelCS;

			// Token: 0x0400164F RID: 5711
			[Reload("Runtime/Lighting/ScreenSpaceLighting/ScreenSpaceReflections.compute", ReloadAttribute.Package.Root)]
			public ComputeShader screenSpaceReflectionsCS;

			// Token: 0x04001650 RID: 5712
			[Reload("Runtime/RenderPipeline/RenderPass/Distortion/ApplyDistortion.shader", ReloadAttribute.Package.Root)]
			public Shader applyDistortionPS;

			// Token: 0x04001651 RID: 5713
			[Reload("Runtime/Lighting/LightLoop/cleardispatchindirect.compute", ReloadAttribute.Package.Root)]
			public ComputeShader clearDispatchIndirectCS;

			// Token: 0x04001652 RID: 5714
			[Reload("Runtime/Lighting/LightLoop/ClearLightLists.compute", ReloadAttribute.Package.Root)]
			public ComputeShader clearLightListsCS;

			// Token: 0x04001653 RID: 5715
			[Reload("Runtime/Lighting/LightLoop/builddispatchindirect.compute", ReloadAttribute.Package.Root)]
			public ComputeShader buildDispatchIndirectCS;

			// Token: 0x04001654 RID: 5716
			[Reload("Runtime/Lighting/LightLoop/scrbound.compute", ReloadAttribute.Package.Root)]
			public ComputeShader buildScreenAABBCS;

			// Token: 0x04001655 RID: 5717
			[Reload("Runtime/Lighting/LightLoop/lightlistbuild.compute", ReloadAttribute.Package.Root)]
			public ComputeShader buildPerTileLightListCS;

			// Token: 0x04001656 RID: 5718
			[Reload("Runtime/Lighting/LightLoop/lightlistbuild-bigtile.compute", ReloadAttribute.Package.Root)]
			public ComputeShader buildPerBigTileLightListCS;

			// Token: 0x04001657 RID: 5719
			[Reload("Runtime/Lighting/LightLoop/lightlistbuild-clustered.compute", ReloadAttribute.Package.Root)]
			public ComputeShader buildPerVoxelLightListCS;

			// Token: 0x04001658 RID: 5720
			[Reload("Runtime/Lighting/LightLoop/materialflags.compute", ReloadAttribute.Package.Root)]
			public ComputeShader buildMaterialFlagsCS;

			// Token: 0x04001659 RID: 5721
			[Reload("Runtime/Lighting/LightLoop/Deferred.compute", ReloadAttribute.Package.Root)]
			public ComputeShader deferredCS;

			// Token: 0x0400165A RID: 5722
			[Reload("Runtime/Lighting/Shadow/ContactShadows.compute", ReloadAttribute.Package.Root)]
			public ComputeShader contactShadowCS;

			// Token: 0x0400165B RID: 5723
			[Reload("Runtime/Lighting/VolumetricLighting/VolumeVoxelization.compute", ReloadAttribute.Package.Root)]
			public ComputeShader volumeVoxelizationCS;

			// Token: 0x0400165C RID: 5724
			[Reload("Runtime/Lighting/VolumetricLighting/VolumetricLighting.compute", ReloadAttribute.Package.Root)]
			public ComputeShader volumetricLightingCS;

			// Token: 0x0400165D RID: 5725
			[Reload("Runtime/Lighting/LightLoop/DeferredTile.shader", ReloadAttribute.Package.Root)]
			public Shader deferredTilePS;

			// Token: 0x0400165E RID: 5726
			[Reload("Runtime/Lighting/Shadow/ScreenSpaceShadows.shader", ReloadAttribute.Package.Root)]
			public Shader screenSpaceShadowPS;

			// Token: 0x0400165F RID: 5727
			[Reload("Runtime/Material/SubsurfaceScattering/SubsurfaceScattering.compute", ReloadAttribute.Package.Root)]
			public ComputeShader subsurfaceScatteringCS;

			// Token: 0x04001660 RID: 5728
			[Reload("Runtime/Material/SubsurfaceScattering/CombineLighting.shader", ReloadAttribute.Package.Root)]
			public Shader combineLightingPS;

			// Token: 0x04001661 RID: 5729
			[Reload("Runtime/RenderPipeline/RenderPass/MotionVectors/CameraMotionVectors.shader", ReloadAttribute.Package.Root)]
			public Shader cameraMotionVectorsPS;

			// Token: 0x04001662 RID: 5730
			[Reload("Runtime/ShaderLibrary/ClearStencilBuffer.shader", ReloadAttribute.Package.Root)]
			public Shader clearStencilBufferPS;

			// Token: 0x04001663 RID: 5731
			[Reload("Runtime/ShaderLibrary/CopyStencilBuffer.shader", ReloadAttribute.Package.Root)]
			public Shader copyStencilBufferPS;

			// Token: 0x04001664 RID: 5732
			[Reload("Runtime/ShaderLibrary/CopyDepthBuffer.shader", ReloadAttribute.Package.Root)]
			public Shader copyDepthBufferPS;

			// Token: 0x04001665 RID: 5733
			[Reload("Runtime/ShaderLibrary/Blit.shader", ReloadAttribute.Package.Root)]
			public Shader blitPS;

			// Token: 0x04001666 RID: 5734
			[Reload("Runtime/ShaderLibrary/DownsampleDepth.shader", ReloadAttribute.Package.Root)]
			public Shader downsampleDepthPS;

			// Token: 0x04001667 RID: 5735
			[Reload("Runtime/ShaderLibrary/UpsampleTransparent.shader", ReloadAttribute.Package.Root)]
			public Shader upsampleTransparentPS;

			// Token: 0x04001668 RID: 5736
			[Reload("Runtime/ShaderLibrary/ResolveStencilBuffer.compute", ReloadAttribute.Package.Root)]
			public ComputeShader resolveStencilCS;

			// Token: 0x04001669 RID: 5737
			[Reload("Runtime/Sky/BlitCubemap.shader", ReloadAttribute.Package.Root)]
			public Shader blitCubemapPS;

			// Token: 0x0400166A RID: 5738
			[Reload("Runtime/Material/GGXConvolution/BuildProbabilityTables.compute", ReloadAttribute.Package.Root)]
			public ComputeShader buildProbabilityTablesCS;

			// Token: 0x0400166B RID: 5739
			[Reload("Runtime/Material/GGXConvolution/ComputeGgxIblSampleData.compute", ReloadAttribute.Package.Root)]
			public ComputeShader computeGgxIblSampleDataCS;

			// Token: 0x0400166C RID: 5740
			[Reload("Runtime/Material/GGXConvolution/GGXConvolve.shader", ReloadAttribute.Package.Root)]
			public Shader GGXConvolvePS;

			// Token: 0x0400166D RID: 5741
			[Reload("Runtime/Material/Fabric/CharlieConvolve.shader", ReloadAttribute.Package.Root)]
			public Shader charlieConvolvePS;

			// Token: 0x0400166E RID: 5742
			[Reload("Runtime/Lighting/AtmosphericScattering/OpaqueAtmosphericScattering.shader", ReloadAttribute.Package.Root)]
			public Shader opaqueAtmosphericScatteringPS;

			// Token: 0x0400166F RID: 5743
			[Reload("Runtime/Sky/HDRISky/HDRISky.shader", ReloadAttribute.Package.Root)]
			public Shader hdriSkyPS;

			// Token: 0x04001670 RID: 5744
			[Reload("Runtime/Sky/HDRISky/IntegrateHDRISky.shader", ReloadAttribute.Package.Root)]
			public Shader integrateHdriSkyPS;

			// Token: 0x04001671 RID: 5745
			[Reload("Skybox/Cubemap", ReloadAttribute.Package.Builtin)]
			public Shader skyboxCubemapPS;

			// Token: 0x04001672 RID: 5746
			[Reload("Runtime/Sky/GradientSky/GradientSky.shader", ReloadAttribute.Package.Root)]
			public Shader gradientSkyPS;

			// Token: 0x04001673 RID: 5747
			[Reload("Runtime/Sky/AmbientProbeConvolution.compute", ReloadAttribute.Package.Root)]
			public ComputeShader ambientProbeConvolutionCS;

			// Token: 0x04001674 RID: 5748
			[Reload("Runtime/Sky/PhysicallyBasedSky/GroundIrradiancePrecomputation.compute", ReloadAttribute.Package.Root)]
			public ComputeShader groundIrradiancePrecomputationCS;

			// Token: 0x04001675 RID: 5749
			[Reload("Runtime/Sky/PhysicallyBasedSky/InScatteredRadiancePrecomputation.compute", ReloadAttribute.Package.Root)]
			public ComputeShader inScatteredRadiancePrecomputationCS;

			// Token: 0x04001676 RID: 5750
			[Reload("Runtime/Sky/PhysicallyBasedSky/PhysicallyBasedSky.shader", ReloadAttribute.Package.Root)]
			public Shader physicallyBasedSkyPS;

			// Token: 0x04001677 RID: 5751
			[Reload("Runtime/Material/PreIntegratedFGD/PreIntegratedFGD_GGXDisneyDiffuse.shader", ReloadAttribute.Package.Root)]
			public Shader preIntegratedFGD_GGXDisneyDiffusePS;

			// Token: 0x04001678 RID: 5752
			[Reload("Runtime/Material/PreIntegratedFGD/PreIntegratedFGD_CharlieFabricLambert.shader", ReloadAttribute.Package.Root)]
			public Shader preIntegratedFGD_CharlieFabricLambertPS;

			// Token: 0x04001679 RID: 5753
			[Reload("Runtime/Material/AxF/PreIntegratedFGD_Ward.shader", ReloadAttribute.Package.Root)]
			public Shader preIntegratedFGD_WardPS;

			// Token: 0x0400167A RID: 5754
			[Reload("Runtime/Material/AxF/PreIntegratedFGD_CookTorrance.shader", ReloadAttribute.Package.Root)]
			public Shader preIntegratedFGD_CookTorrancePS;

			// Token: 0x0400167B RID: 5755
			[Reload("Runtime/Core/CoreResources/EncodeBC6H.compute", ReloadAttribute.Package.Root)]
			public ComputeShader encodeBC6HCS;

			// Token: 0x0400167C RID: 5756
			[Reload("Runtime/Core/CoreResources/CubeToPano.shader", ReloadAttribute.Package.Root)]
			public Shader cubeToPanoPS;

			// Token: 0x0400167D RID: 5757
			[Reload("Runtime/Core/CoreResources/BlitCubeTextureFace.shader", ReloadAttribute.Package.Root)]
			public Shader blitCubeTextureFacePS;

			// Token: 0x0400167E RID: 5758
			[Reload("Runtime/Material/LTCAreaLight/FilterAreaLightCookies.shader", ReloadAttribute.Package.Root)]
			public Shader filterAreaLightCookiesPS;

			// Token: 0x0400167F RID: 5759
			[Reload("Runtime/Core/CoreResources/ClearUIntTextureArray.compute", ReloadAttribute.Package.Root)]
			public ComputeShader clearUIntTextureCS;

			// Token: 0x04001680 RID: 5760
			[Reload("Runtime/ShaderLibrary/XRMirrorView.shader", ReloadAttribute.Package.Root)]
			public Shader xrMirrorViewPS;

			// Token: 0x04001681 RID: 5761
			[Reload("Runtime/ShaderLibrary/XROcclusionMesh.shader", ReloadAttribute.Package.Root)]
			public Shader xrOcclusionMeshPS;

			// Token: 0x04001682 RID: 5762
			[Reload("Runtime/Lighting/Shadow/ShadowClear.shader", ReloadAttribute.Package.Root)]
			public Shader shadowClearPS;

			// Token: 0x04001683 RID: 5763
			[Reload("Runtime/Lighting/Shadow/EVSMBlur.compute", ReloadAttribute.Package.Root)]
			public ComputeShader evsmBlurCS;

			// Token: 0x04001684 RID: 5764
			[Reload("Runtime/Lighting/Shadow/DebugDisplayHDShadowMap.shader", ReloadAttribute.Package.Root)]
			public Shader debugHDShadowMapPS;

			// Token: 0x04001685 RID: 5765
			[Reload("Runtime/Lighting/Shadow/MomentShadows.compute", ReloadAttribute.Package.Root)]
			public ComputeShader momentShadowsCS;

			// Token: 0x04001686 RID: 5766
			[Reload("Runtime/Material/Decal/DecalNormalBuffer.shader", ReloadAttribute.Package.Root)]
			public Shader decalNormalBufferPS;

			// Token: 0x04001687 RID: 5767
			[Reload("Runtime/Material/Decal/ClearPropertyMaskBuffer.compute", ReloadAttribute.Package.Root)]
			public ComputeShader decalClearPropertyMaskBufferCS;

			// Token: 0x04001688 RID: 5768
			[Reload("Runtime/Lighting/ScreenSpaceLighting/GTAO.compute", ReloadAttribute.Package.Root)]
			public ComputeShader GTAOCS;

			// Token: 0x04001689 RID: 5769
			[Reload("Runtime/Lighting/ScreenSpaceLighting/GTAODenoise.compute", ReloadAttribute.Package.Root)]
			public ComputeShader GTAODenoiseCS;

			// Token: 0x0400168A RID: 5770
			[Reload("Runtime/Lighting/ScreenSpaceLighting/GTAOBlurAndUpsample.compute", ReloadAttribute.Package.Root)]
			public ComputeShader GTAOBlurAndUpsample;

			// Token: 0x0400168B RID: 5771
			[Reload("Runtime/RenderPipeline/RenderPass/MSAA/DepthValues.shader", ReloadAttribute.Package.Root)]
			public Shader depthValuesPS;

			// Token: 0x0400168C RID: 5772
			[Reload("Runtime/RenderPipeline/RenderPass/MSAA/ColorResolve.shader", ReloadAttribute.Package.Root)]
			public Shader colorResolvePS;

			// Token: 0x0400168D RID: 5773
			[Reload("Runtime/PostProcessing/Shaders/AlphaCopy.compute", ReloadAttribute.Package.Root)]
			public ComputeShader copyAlphaCS;

			// Token: 0x0400168E RID: 5774
			[Reload("Runtime/PostProcessing/Shaders/NaNKiller.compute", ReloadAttribute.Package.Root)]
			public ComputeShader nanKillerCS;

			// Token: 0x0400168F RID: 5775
			[Reload("Runtime/PostProcessing/Shaders/Exposure.compute", ReloadAttribute.Package.Root)]
			public ComputeShader exposureCS;

			// Token: 0x04001690 RID: 5776
			[Reload("Runtime/PostProcessing/Shaders/ApplyExposure.compute", ReloadAttribute.Package.Root)]
			public ComputeShader applyExposureCS;

			// Token: 0x04001691 RID: 5777
			[Reload("Runtime/PostProcessing/Shaders/UberPost.compute", ReloadAttribute.Package.Root)]
			public ComputeShader uberPostCS;

			// Token: 0x04001692 RID: 5778
			[Reload("Runtime/PostProcessing/Shaders/LutBuilder3D.compute", ReloadAttribute.Package.Root)]
			public ComputeShader lutBuilder3DCS;

			// Token: 0x04001693 RID: 5779
			[Reload("Runtime/PostProcessing/Shaders/DepthOfFieldKernel.compute", ReloadAttribute.Package.Root)]
			public ComputeShader depthOfFieldKernelCS;

			// Token: 0x04001694 RID: 5780
			[Reload("Runtime/PostProcessing/Shaders/DepthOfFieldCoC.compute", ReloadAttribute.Package.Root)]
			public ComputeShader depthOfFieldCoCCS;

			// Token: 0x04001695 RID: 5781
			[Reload("Runtime/PostProcessing/Shaders/DepthOfFieldCoCReproject.compute", ReloadAttribute.Package.Root)]
			public ComputeShader depthOfFieldCoCReprojectCS;

			// Token: 0x04001696 RID: 5782
			[Reload("Runtime/PostProcessing/Shaders/DepthOfFieldCoCDilate.compute", ReloadAttribute.Package.Root)]
			public ComputeShader depthOfFieldDilateCS;

			// Token: 0x04001697 RID: 5783
			[Reload("Runtime/PostProcessing/Shaders/DepthOfFieldMip.compute", ReloadAttribute.Package.Root)]
			public ComputeShader depthOfFieldMipCS;

			// Token: 0x04001698 RID: 5784
			[Reload("Runtime/PostProcessing/Shaders/DepthOfFieldMipSafe.compute", ReloadAttribute.Package.Root)]
			public ComputeShader depthOfFieldMipSafeCS;

			// Token: 0x04001699 RID: 5785
			[Reload("Runtime/PostProcessing/Shaders/DepthOfFieldPrefilter.compute", ReloadAttribute.Package.Root)]
			public ComputeShader depthOfFieldPrefilterCS;

			// Token: 0x0400169A RID: 5786
			[Reload("Runtime/PostProcessing/Shaders/DepthOfFieldTileMax.compute", ReloadAttribute.Package.Root)]
			public ComputeShader depthOfFieldTileMaxCS;

			// Token: 0x0400169B RID: 5787
			[Reload("Runtime/PostProcessing/Shaders/DepthOfFieldGather.compute", ReloadAttribute.Package.Root)]
			public ComputeShader depthOfFieldGatherCS;

			// Token: 0x0400169C RID: 5788
			[Reload("Runtime/PostProcessing/Shaders/DepthOfFieldCombine.compute", ReloadAttribute.Package.Root)]
			public ComputeShader depthOfFieldCombineCS;

			// Token: 0x0400169D RID: 5789
			[Reload("Runtime/PostProcessing/Shaders/PaniniProjection.compute", ReloadAttribute.Package.Root)]
			public ComputeShader paniniProjectionCS;

			// Token: 0x0400169E RID: 5790
			[Reload("Runtime/PostProcessing/Shaders/MotionBlurMotionVecPrep.compute", ReloadAttribute.Package.Root)]
			public ComputeShader motionBlurMotionVecPrepCS;

			// Token: 0x0400169F RID: 5791
			[Reload("Runtime/PostProcessing/Shaders/MotionBlurTilePass.compute", ReloadAttribute.Package.Root)]
			public ComputeShader motionBlurTileGenCS;

			// Token: 0x040016A0 RID: 5792
			[Reload("Runtime/PostProcessing/Shaders/MotionBlur.compute", ReloadAttribute.Package.Root)]
			public ComputeShader motionBlurCS;

			// Token: 0x040016A1 RID: 5793
			[Reload("Runtime/PostProcessing/Shaders/BloomPrefilter.compute", ReloadAttribute.Package.Root)]
			public ComputeShader bloomPrefilterCS;

			// Token: 0x040016A2 RID: 5794
			[Reload("Runtime/PostProcessing/Shaders/BloomBlur.compute", ReloadAttribute.Package.Root)]
			public ComputeShader bloomBlurCS;

			// Token: 0x040016A3 RID: 5795
			[Reload("Runtime/PostProcessing/Shaders/BloomUpsample.compute", ReloadAttribute.Package.Root)]
			public ComputeShader bloomUpsampleCS;

			// Token: 0x040016A4 RID: 5796
			[Reload("Runtime/PostProcessing/Shaders/FXAA.compute", ReloadAttribute.Package.Root)]
			public ComputeShader FXAACS;

			// Token: 0x040016A5 RID: 5797
			[Reload("Runtime/PostProcessing/Shaders/FinalPass.shader", ReloadAttribute.Package.Root)]
			public Shader finalPassPS;

			// Token: 0x040016A6 RID: 5798
			[Reload("Runtime/PostProcessing/Shaders/ClearBlack.shader", ReloadAttribute.Package.Root)]
			public Shader clearBlackPS;

			// Token: 0x040016A7 RID: 5799
			[Reload("Runtime/PostProcessing/Shaders/SubpixelMorphologicalAntialiasing.shader", ReloadAttribute.Package.Root)]
			public Shader SMAAPS;

			// Token: 0x040016A8 RID: 5800
			[Reload("Runtime/PostProcessing/Shaders/TemporalAntialiasing.shader", ReloadAttribute.Package.Root)]
			public Shader temporalAntialiasingPS;

			// Token: 0x040016A9 RID: 5801
			[Reload("Runtime/PostProcessing/Shaders/ContrastAdaptiveSharpen.compute", ReloadAttribute.Package.Root)]
			public ComputeShader contrastAdaptiveSharpenCS;
		}

		// Token: 0x0200027C RID: 636
		[ReloadGroup]
		[Serializable]
		public sealed class MaterialResources
		{
		}

		// Token: 0x0200027D RID: 637
		[ReloadGroup]
		[Serializable]
		public sealed class TextureResources
		{
			// Token: 0x040016AA RID: 5802
			[Reload("Runtime/RenderPipelineResources/Texture/DebugFont.tga", ReloadAttribute.Package.Root)]
			public Texture2D debugFontTex;

			// Token: 0x040016AB RID: 5803
			[Reload("Runtime/Debug/ColorGradient.png", ReloadAttribute.Package.Root)]
			public Texture2D colorGradient;

			// Token: 0x040016AC RID: 5804
			[Reload("Runtime/RenderPipelineResources/Texture/Matcap/DefaultMatcap.png", ReloadAttribute.Package.Root)]
			public Texture2D matcapTex;

			// Token: 0x040016AD RID: 5805
			[Reload("Runtime/RenderPipelineResources/Texture/BlueNoise16/L/LDR_LLL1_{0}.png", 0, 32, ReloadAttribute.Package.Root)]
			public Texture2D[] blueNoise16LTex;

			// Token: 0x040016AE RID: 5806
			[Reload("Runtime/RenderPipelineResources/Texture/BlueNoise16/RGB/LDR_RGB1_{0}.png", 0, 32, ReloadAttribute.Package.Root)]
			public Texture2D[] blueNoise16RGBTex;

			// Token: 0x040016AF RID: 5807
			[Reload("Runtime/RenderPipelineResources/Texture/CoherentNoise/OwenScrambledNoise4.png", ReloadAttribute.Package.Root)]
			public Texture2D owenScrambledRGBATex;

			// Token: 0x040016B0 RID: 5808
			[Reload("Runtime/RenderPipelineResources/Texture/CoherentNoise/OwenScrambledNoise256.png", ReloadAttribute.Package.Root)]
			public Texture2D owenScrambled256Tex;

			// Token: 0x040016B1 RID: 5809
			[Reload("Runtime/RenderPipelineResources/Texture/CoherentNoise/ScrambleNoise.png", ReloadAttribute.Package.Root)]
			public Texture2D scramblingTex;

			// Token: 0x040016B2 RID: 5810
			[Reload("Runtime/RenderPipelineResources/Texture/CoherentNoise/RankingTile1SPP.png", ReloadAttribute.Package.Root)]
			public Texture2D rankingTile1SPP;

			// Token: 0x040016B3 RID: 5811
			[Reload("Runtime/RenderPipelineResources/Texture/CoherentNoise/ScramblingTile1SPP.png", ReloadAttribute.Package.Root)]
			public Texture2D scramblingTile1SPP;

			// Token: 0x040016B4 RID: 5812
			[Reload("Runtime/RenderPipelineResources/Texture/CoherentNoise/RankingTile8SPP.png", ReloadAttribute.Package.Root)]
			public Texture2D rankingTile8SPP;

			// Token: 0x040016B5 RID: 5813
			[Reload("Runtime/RenderPipelineResources/Texture/CoherentNoise/ScramblingTile8SPP.png", ReloadAttribute.Package.Root)]
			public Texture2D scramblingTile8SPP;

			// Token: 0x040016B6 RID: 5814
			[Reload("Runtime/RenderPipelineResources/Texture/CoherentNoise/RankingTile256SPP.png", ReloadAttribute.Package.Root)]
			public Texture2D rankingTile256SPP;

			// Token: 0x040016B7 RID: 5815
			[Reload("Runtime/RenderPipelineResources/Texture/CoherentNoise/ScramblingTile256SPP.png", ReloadAttribute.Package.Root)]
			public Texture2D scramblingTile256SPP;

			// Token: 0x040016B8 RID: 5816
			[Reload(new string[] { "Runtime/RenderPipelineResources/Texture/FilmGrain/Thin01.png", "Runtime/RenderPipelineResources/Texture/FilmGrain/Thin02.png", "Runtime/RenderPipelineResources/Texture/FilmGrain/Medium01.png", "Runtime/RenderPipelineResources/Texture/FilmGrain/Medium02.png", "Runtime/RenderPipelineResources/Texture/FilmGrain/Medium03.png", "Runtime/RenderPipelineResources/Texture/FilmGrain/Medium04.png", "Runtime/RenderPipelineResources/Texture/FilmGrain/Medium05.png", "Runtime/RenderPipelineResources/Texture/FilmGrain/Medium06.png", "Runtime/RenderPipelineResources/Texture/FilmGrain/Large01.png", "Runtime/RenderPipelineResources/Texture/FilmGrain/Large02.png" }, ReloadAttribute.Package.Root)]
			public Texture2D[] filmGrainTex;

			// Token: 0x040016B9 RID: 5817
			[Reload("Runtime/RenderPipelineResources/Texture/SMAA/SearchTex.tga", ReloadAttribute.Package.Root)]
			public Texture2D SMAASearchTex;

			// Token: 0x040016BA RID: 5818
			[Reload("Runtime/RenderPipelineResources/Texture/SMAA/AreaTex.tga", ReloadAttribute.Package.Root)]
			public Texture2D SMAAAreaTex;

			// Token: 0x040016BB RID: 5819
			[Reload("Runtime/RenderPipelineResources/Texture/DefaultHDRISky.exr", ReloadAttribute.Package.Root)]
			public Cubemap defaultHDRISky;
		}

		// Token: 0x0200027E RID: 638
		[ReloadGroup]
		[Serializable]
		public sealed class ShaderGraphResources
		{
		}

		// Token: 0x0200027F RID: 639
		[ReloadGroup]
		[Serializable]
		public sealed class AssetResources
		{
			// Token: 0x040016BC RID: 5820
			[Reload("Runtime/RenderPipelineResources/defaultDiffusionProfile.asset", ReloadAttribute.Package.Root)]
			public DiffusionProfileSettings defaultDiffusionProfile;
		}
	}
}
