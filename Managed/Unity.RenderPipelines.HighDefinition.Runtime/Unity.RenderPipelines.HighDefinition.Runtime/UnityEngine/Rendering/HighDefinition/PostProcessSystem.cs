using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine.Experimental.Rendering;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x020000F4 RID: 244
	internal sealed class PostProcessSystem
	{
		// Token: 0x06000789 RID: 1929 RVA: 0x00039200 File Offset: 0x00037400
		public PostProcessSystem(HDRenderPipelineAsset hdAsset, RenderPipelineResources defaultResources)
		{
			this.m_Resources = defaultResources;
			this.m_FinalPassMaterial = CoreUtils.CreateEngineMaterial(this.m_Resources.shaders.finalPassPS);
			this.m_ClearBlackMaterial = CoreUtils.CreateEngineMaterial(this.m_Resources.shaders.clearBlackPS);
			this.m_SMAAMaterial = CoreUtils.CreateEngineMaterial(this.m_Resources.shaders.SMAAPS);
			this.m_TemporalAAMaterial = CoreUtils.CreateEngineMaterial(this.m_Resources.shaders.temporalAntialiasingPS);
			this.m_UseSafePath = SystemInfo.graphicsDeviceVendor.ToLowerInvariant().Contains("intel");
			GlobalPostProcessSettings postProcessSettings = hdAsset.currentPlatformRenderPipelineSettings.postProcessSettings;
			this.m_LutSize = postProcessSettings.lutSize;
			GraphicsFormat lutFormat = (GraphicsFormat)postProcessSettings.lutFormat;
			this.PushUberFeature(UberPostFeatureFlags.None);
			this.PushUberFeature(UberPostFeatureFlags.ChromaticAberration);
			this.PushUberFeature(UberPostFeatureFlags.Vignette);
			this.PushUberFeature(UberPostFeatureFlags.LensDistortion);
			this.PushUberFeature(UberPostFeatureFlags.ChromaticAberration | UberPostFeatureFlags.Vignette);
			this.PushUberFeature(UberPostFeatureFlags.ChromaticAberration | UberPostFeatureFlags.LensDistortion);
			this.PushUberFeature(UberPostFeatureFlags.Vignette | UberPostFeatureFlags.LensDistortion);
			this.PushUberFeature(UberPostFeatureFlags.ChromaticAberration | UberPostFeatureFlags.Vignette | UberPostFeatureFlags.LensDistortion);
			this.PushUberFeature(UberPostFeatureFlags.EnableAlpha);
			this.PushUberFeature(UberPostFeatureFlags.ChromaticAberration | UberPostFeatureFlags.EnableAlpha);
			this.PushUberFeature(UberPostFeatureFlags.Vignette | UberPostFeatureFlags.EnableAlpha);
			this.PushUberFeature(UberPostFeatureFlags.LensDistortion | UberPostFeatureFlags.EnableAlpha);
			this.PushUberFeature(UberPostFeatureFlags.ChromaticAberration | UberPostFeatureFlags.Vignette | UberPostFeatureFlags.EnableAlpha);
			this.PushUberFeature(UberPostFeatureFlags.ChromaticAberration | UberPostFeatureFlags.LensDistortion | UberPostFeatureFlags.EnableAlpha);
			this.PushUberFeature(UberPostFeatureFlags.Vignette | UberPostFeatureFlags.LensDistortion | UberPostFeatureFlags.EnableAlpha);
			this.PushUberFeature(UberPostFeatureFlags.ChromaticAberration | UberPostFeatureFlags.Vignette | UberPostFeatureFlags.LensDistortion | UberPostFeatureFlags.EnableAlpha);
			this.m_HableCurve = new HableCurve();
			this.m_InternalLogLut = RTHandles.Alloc(this.m_LutSize, this.m_LutSize, this.m_LutSize, DepthBits.None, lutFormat, FilterMode.Bilinear, TextureWrapMode.Clamp, TextureDimension.Tex3D, true, false, true, false, 0, 0f, MSAASamples.None, false, false, RenderTextureMemoryless.None, "Color Grading Log Lut");
			this.m_EmptyExposureTexture = RTHandles.Alloc(1, 1, 1, DepthBits.None, GraphicsFormat.R32G32_SFloat, FilterMode.Point, TextureWrapMode.Repeat, TextureDimension.Tex2D, true, false, true, false, 1, 0f, MSAASamples.None, false, false, RenderTextureMemoryless.None, "Empty EV100 Exposure");
			this.m_MotionBlurSupportsScattering = SystemInfo.IsFormatSupported(GraphicsFormat.R32_UInt, FormatUsage.LoadStore) && SystemInfo.IsFormatSupported(GraphicsFormat.R16_UInt, FormatUsage.LoadStore);
			this.m_MotionBlurSupportsScattering = this.m_MotionBlurSupportsScattering && SystemInfo.graphicsDeviceType != GraphicsDeviceType.Vulkan;
			this.m_MotionBlurSupportsScattering = this.m_MotionBlurSupportsScattering && SystemInfo.graphicsDeviceType != GraphicsDeviceType.Metal;
			Texture2D texture2D = new Texture2D(1, 1, TextureFormat.RGHalf, false, true);
			texture2D.SetPixel(0, 0, new Color(1f, ColorUtils.ConvertExposureToEV100(1f), 0f, 0f));
			texture2D.Apply();
			Graphics.Blit(texture2D, this.m_EmptyExposureTexture);
			CoreUtils.Destroy(texture2D);
			this.m_Pool = new PostProcessSystem.TargetPool();
			this.m_Random = new Random();
			this.m_TempTexture1024 = RTHandles.Alloc(1024, 1024, 1, DepthBits.None, GraphicsFormat.R16G16_SFloat, FilterMode.Point, TextureWrapMode.Repeat, TextureDimension.Tex2D, true, false, true, false, 1, 0f, MSAASamples.None, false, false, RenderTextureMemoryless.None, "Average Luminance Temp 1024");
			this.m_TempTexture32 = RTHandles.Alloc(32, 32, 1, DepthBits.None, GraphicsFormat.R16G16_SFloat, FilterMode.Point, TextureWrapMode.Repeat, TextureDimension.Tex2D, true, false, true, false, 1, 0f, MSAASamples.None, false, false, RenderTextureMemoryless.None, "Average Luminance Temp 32");
			this.m_ColorFormat = (GraphicsFormat)hdAsset.currentPlatformRenderPipelineSettings.postProcessSettings.bufferFormat;
			this.m_KeepAlpha = false;
			RenderPipelineSettings renderPipelineSettings = hdAsset.currentPlatformRenderPipelineSettings;
			bool flag;
			if (renderPipelineSettings.supportsAlpha)
			{
				renderPipelineSettings = hdAsset.currentPlatformRenderPipelineSettings;
				flag = renderPipelineSettings.postProcessSettings.supportsAlpha;
			}
			else
			{
				flag = false;
			}
			this.m_EnableAlpha = flag;
			if (!this.m_EnableAlpha)
			{
				renderPipelineSettings = hdAsset.currentPlatformRenderPipelineSettings;
				this.m_KeepAlpha = renderPipelineSettings.supportsAlpha;
			}
			if (this.m_KeepAlpha)
			{
				this.m_AlphaTexture = RTHandles.Alloc(Vector2.one, TextureXR.slices, DepthBits.None, GraphicsFormat.R16_SFloat, FilterMode.Point, TextureWrapMode.Repeat, TextureXR.dimension, true, false, true, false, 1, 0f, false, false, false, RenderTextureMemoryless.None, "Alpha Channel Copy");
			}
		}

		// Token: 0x0600078A RID: 1930 RVA: 0x000395B0 File Offset: 0x000377B0
		public void Cleanup()
		{
			this.m_Pool.Cleanup();
			RTHandles.Release(this.m_EmptyExposureTexture);
			RTHandles.Release(this.m_TempTexture1024);
			RTHandles.Release(this.m_TempTexture32);
			RTHandles.Release(this.m_AlphaTexture);
			CoreUtils.Destroy(this.m_ExposureCurveTexture);
			CoreUtils.Destroy(this.m_InternalSpectralLut);
			RTHandles.Release(this.m_InternalLogLut);
			CoreUtils.Destroy(this.m_FinalPassMaterial);
			CoreUtils.Destroy(this.m_ClearBlackMaterial);
			CoreUtils.SafeRelease(this.m_BokehNearKernel);
			CoreUtils.SafeRelease(this.m_BokehFarKernel);
			CoreUtils.SafeRelease(this.m_BokehIndirectCmd);
			CoreUtils.SafeRelease(this.m_NearBokehTileList);
			CoreUtils.SafeRelease(this.m_FarBokehTileList);
			CoreUtils.SafeRelease(this.m_ContrastAdaptiveSharpen);
			this.m_EmptyExposureTexture = null;
			this.m_TempTexture1024 = null;
			this.m_TempTexture32 = null;
			this.m_AlphaTexture = null;
			this.m_ExposureCurveTexture = null;
			this.m_InternalSpectralLut = null;
			this.m_InternalLogLut = null;
			this.m_FinalPassMaterial = null;
			this.m_ClearBlackMaterial = null;
			this.m_BokehNearKernel = null;
			this.m_BokehFarKernel = null;
			this.m_BokehIndirectCmd = null;
			this.m_NearBokehTileList = null;
			this.m_FarBokehTileList = null;
		}

		// Token: 0x0600078B RID: 1931 RVA: 0x000396D0 File Offset: 0x000378D0
		public void BeginFrame(CommandBuffer cmd, HDCamera camera, HDRenderPipeline hdInstance)
		{
			this.m_HDInstance = hdInstance;
			this.m_PostProcessEnabled = camera.frameSettings.IsEnabled(FrameSettingsField.Postprocess) && CoreUtils.ArePostProcessesEnabled(camera.camera);
			this.m_AnimatedMaterialsEnabled = camera.animateMaterials;
			this.m_PhysicalCamera = camera.physicalParameters ?? PostProcessSystem.m_DefaultPhysicalCamera;
			VolumeStack volumeStack = camera.volumeStack;
			this.m_Exposure = volumeStack.GetComponent<Exposure>();
			this.m_DepthOfField = volumeStack.GetComponent<DepthOfField>();
			this.m_MotionBlur = volumeStack.GetComponent<MotionBlur>();
			this.m_PaniniProjection = volumeStack.GetComponent<PaniniProjection>();
			this.m_Bloom = volumeStack.GetComponent<Bloom>();
			this.m_ChromaticAberration = volumeStack.GetComponent<ChromaticAberration>();
			this.m_LensDistortion = volumeStack.GetComponent<LensDistortion>();
			this.m_Vignette = volumeStack.GetComponent<Vignette>();
			this.m_Tonemapping = volumeStack.GetComponent<Tonemapping>();
			this.m_WhiteBalance = volumeStack.GetComponent<WhiteBalance>();
			this.m_ColorAdjustments = volumeStack.GetComponent<ColorAdjustments>();
			this.m_ChannelMixer = volumeStack.GetComponent<ChannelMixer>();
			this.m_SplitToning = volumeStack.GetComponent<SplitToning>();
			this.m_LiftGammaGain = volumeStack.GetComponent<LiftGammaGain>();
			this.m_ShadowsMidtonesHighlights = volumeStack.GetComponent<ShadowsMidtonesHighlights>();
			this.m_Curves = volumeStack.GetComponent<ColorCurves>();
			this.m_FilmGrain = volumeStack.GetComponent<FilmGrain>();
			FrameSettings frameSettings = camera.frameSettings;
			this.m_ExposureControlFS = frameSettings.IsEnabled(FrameSettingsField.ExposureControl);
			this.m_StopNaNFS = frameSettings.IsEnabled(FrameSettingsField.StopNaN);
			this.m_DepthOfFieldFS = frameSettings.IsEnabled(FrameSettingsField.DepthOfField);
			this.m_MotionBlurFS = frameSettings.IsEnabled(FrameSettingsField.MotionBlur);
			this.m_PaniniProjectionFS = frameSettings.IsEnabled(FrameSettingsField.PaniniProjection);
			this.m_BloomFS = frameSettings.IsEnabled(FrameSettingsField.Bloom);
			this.m_ChromaticAberrationFS = frameSettings.IsEnabled(FrameSettingsField.ChromaticAberration);
			this.m_LensDistortionFS = frameSettings.IsEnabled(FrameSettingsField.LensDistortion);
			this.m_VignetteFS = frameSettings.IsEnabled(FrameSettingsField.Vignette);
			this.m_ColorGradingFS = frameSettings.IsEnabled(FrameSettingsField.ColorGrading);
			this.m_TonemappingFS = frameSettings.IsEnabled(FrameSettingsField.Tonemapping);
			this.m_FilmGrainFS = frameSettings.IsEnabled(FrameSettingsField.FilmGrain);
			this.m_DitheringFS = frameSettings.IsEnabled(FrameSettingsField.Dithering);
			this.m_AntialiasingFS = frameSettings.IsEnabled(FrameSettingsField.Antialiasing);
			if (!this.m_ExposureControlFS)
			{
				cmd.SetGlobalTexture(HDShaderIDs._ExposureTexture, this.m_EmptyExposureTexture);
				cmd.SetGlobalTexture(HDShaderIDs._PrevExposureTexture, this.m_EmptyExposureTexture);
				return;
			}
			if (this.IsExposureFixed())
			{
				using (new ProfilingScope(cmd, ProfilingSampler.Get<HDProfileId>(HDProfileId.FixedExposure)))
				{
					this.DoFixedExposure(cmd, camera);
				}
			}
			cmd.SetGlobalTexture(HDShaderIDs._ExposureTexture, this.GetExposureTexture(camera));
			cmd.SetGlobalTexture(HDShaderIDs._PrevExposureTexture, this.GetPreviousExposureTexture(camera));
		}

		// Token: 0x0600078C RID: 1932 RVA: 0x0003997C File Offset: 0x00037B7C
		private void PoolSourceGuard(ref RTHandle src, RTHandle dst, RTHandle colorBuffer)
		{
			if (src != colorBuffer)
			{
				this.m_Pool.Recycle(src);
			}
			src = dst;
		}

		// Token: 0x0600078D RID: 1933 RVA: 0x00039994 File Offset: 0x00037B94
		public void Render(CommandBuffer cmd, HDCamera camera, BlueNoise blueNoise, RTHandle colorBuffer, RTHandle afterPostProcessTexture, RTHandle lightingBuffer, RenderTargetIdentifier finalRT, RTHandle depthBuffer, bool flipY)
		{
			PostProcessSystem.<>c__DisplayClass81_0 CS$<>8__locals1;
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.colorBuffer = colorBuffer;
			DynamicResolutionHandler instance = DynamicResolutionHandler.instance;
			this.m_Pool.SetHWDynamicResolutionState(camera);
			bool flag = camera.camera.cameraType == CameraType.SceneView;
			using (new ProfilingScope(cmd, ProfilingSampler.Get<HDProfileId>(HDProfileId.PostProcessing)))
			{
				if (this.m_KeepAlpha)
				{
					using (new ProfilingScope(cmd, ProfilingSampler.Get<HDProfileId>(HDProfileId.AlphaCopy)))
					{
						this.DoCopyAlpha(cmd, camera, CS$<>8__locals1.colorBuffer);
					}
				}
				RTHandle colorBuffer2 = CS$<>8__locals1.colorBuffer;
				if (this.m_PostProcessEnabled)
				{
					int actualWidth = camera.actualWidth;
					int actualHeight = camera.actualHeight;
					cmd.SetRenderTarget(colorBuffer2, 0, CubemapFace.Unknown, -1);
					if (actualWidth < colorBuffer2.rt.width || actualHeight < colorBuffer2.rt.height)
					{
						cmd.SetViewport(new Rect((float)actualWidth, 0f, 4f, (float)actualHeight));
						cmd.DrawProcedural(Matrix4x4.identity, this.m_ClearBlackMaterial, 0, MeshTopology.Triangles, 3, 1);
						cmd.SetViewport(new Rect(0f, (float)actualHeight, (float)(actualWidth + 4), 4f));
						cmd.DrawProcedural(Matrix4x4.identity, this.m_ClearBlackMaterial, 0, MeshTopology.Triangles, 3, 1);
					}
					if (camera.stopNaNs && this.m_StopNaNFS)
					{
						using (new ProfilingScope(cmd, ProfilingSampler.Get<HDProfileId>(HDProfileId.StopNaNs)))
						{
							PostProcessSystem.TargetPool pool = this.m_Pool;
							Vector2 vector = Vector2.one;
							RTHandle rthandle = pool.Get(in vector, this.m_ColorFormat, false);
							this.DoStopNaNs(cmd, camera, colorBuffer2, rthandle);
							this.<Render>g__PoolSource|81_0(ref colorBuffer2, rthandle, ref CS$<>8__locals1);
						}
					}
				}
				if (!this.IsExposureFixed() && this.m_ExposureControlFS)
				{
					using (new ProfilingScope(cmd, ProfilingSampler.Get<HDProfileId>(HDProfileId.DynamicExposure)))
					{
						this.DoDynamicExposure(cmd, camera, colorBuffer2, lightingBuffer);
						if (camera.resetPostProcessingHistory)
						{
							PostProcessSystem.TargetPool pool2 = this.m_Pool;
							Vector2 vector = Vector2.one;
							RTHandle rthandle2 = pool2.Get(in vector, this.m_ColorFormat, false);
							ComputeShader applyExposureCS = this.m_Resources.shaders.applyExposureCS;
							int num = applyExposureCS.FindKernel("KMain");
							cmd.SetComputeTextureParam(applyExposureCS, num, HDShaderIDs._ExposureTexture, this.GetPreviousExposureTexture(camera));
							cmd.SetComputeTextureParam(applyExposureCS, num, HDShaderIDs._InputTexture, colorBuffer2);
							cmd.SetComputeTextureParam(applyExposureCS, num, HDShaderIDs._OutputTexture, rthandle2);
							cmd.DispatchCompute(applyExposureCS, num, (camera.actualWidth + 7) / 8, (camera.actualHeight + 7) / 8, camera.viewCount);
							this.<Render>g__PoolSource|81_0(ref colorBuffer2, rthandle2, ref CS$<>8__locals1);
						}
					}
				}
				if (this.m_PostProcessEnabled)
				{
					bool flag2 = false;
					if (this.m_AntialiasingFS)
					{
						flag2 = camera.antialiasing == HDAdditionalCameraData.AntialiasingMode.TemporalAntialiasing;
						if (flag2)
						{
							using (new ProfilingScope(cmd, ProfilingSampler.Get<HDProfileId>(HDProfileId.TemporalAntialiasing)))
							{
								PostProcessSystem.TargetPool pool3 = this.m_Pool;
								Vector2 vector = Vector2.one;
								RTHandle rthandle3 = pool3.Get(in vector, this.m_ColorFormat, false);
								this.DoTemporalAntialiasing(cmd, camera, colorBuffer2, rthandle3, depthBuffer);
								this.<Render>g__PoolSource|81_0(ref colorBuffer2, rthandle3, ref CS$<>8__locals1);
								goto IL_037E;
							}
						}
						if (camera.antialiasing == HDAdditionalCameraData.AntialiasingMode.SubpixelMorphologicalAntiAliasing)
						{
							using (new ProfilingScope(cmd, ProfilingSampler.Get<HDProfileId>(HDProfileId.SMAA)))
							{
								PostProcessSystem.TargetPool pool4 = this.m_Pool;
								Vector2 vector = Vector2.one;
								RTHandle rthandle4 = pool4.Get(in vector, this.m_ColorFormat, false);
								this.DoSMAA(cmd, camera, colorBuffer2, rthandle4, depthBuffer);
								this.<Render>g__PoolSource|81_0(ref colorBuffer2, rthandle4, ref CS$<>8__locals1);
							}
						}
					}
					IL_037E:
					if (camera.frameSettings.IsEnabled(FrameSettingsField.CustomPostProcess))
					{
						using (new ProfilingScope(cmd, ProfilingSampler.Get<HDProfileId>(HDProfileId.CustomPostProcessBeforePP)))
						{
							foreach (string text in HDRenderPipeline.defaultAsset.beforePostProcessCustomPostProcesses)
							{
								this.RenderCustomPostProcess(cmd, camera, ref colorBuffer2, CS$<>8__locals1.colorBuffer, Type.GetType(text));
							}
						}
					}
					if (this.m_DepthOfField.IsActive() && !flag && this.m_DepthOfFieldFS)
					{
						using (new ProfilingScope(cmd, ProfilingSampler.Get<HDProfileId>(HDProfileId.DepthOfField)))
						{
							PostProcessSystem.TargetPool pool5 = this.m_Pool;
							Vector2 vector = Vector2.one;
							RTHandle rthandle5 = pool5.Get(in vector, this.m_ColorFormat, false);
							this.DoDepthOfField(cmd, camera, colorBuffer2, rthandle5, flag2);
							this.<Render>g__PoolSource|81_0(ref colorBuffer2, rthandle5, ref CS$<>8__locals1);
						}
					}
					if (this.m_MotionBlur.IsActive() && this.m_AnimatedMaterialsEnabled && !camera.resetPostProcessingHistory && this.m_MotionBlurFS)
					{
						using (new ProfilingScope(cmd, ProfilingSampler.Get<HDProfileId>(HDProfileId.MotionBlur)))
						{
							PostProcessSystem.TargetPool pool6 = this.m_Pool;
							Vector2 vector = Vector2.one;
							RTHandle rthandle6 = pool6.Get(in vector, this.m_ColorFormat, false);
							this.DoMotionBlur(cmd, camera, colorBuffer2, rthandle6);
							this.<Render>g__PoolSource|81_0(ref colorBuffer2, rthandle6, ref CS$<>8__locals1);
						}
					}
					if (this.m_PaniniProjection.IsActive() && !flag && this.m_PaniniProjectionFS)
					{
						using (new ProfilingScope(cmd, ProfilingSampler.Get<HDProfileId>(HDProfileId.PaniniProjection)))
						{
							PostProcessSystem.TargetPool pool7 = this.m_Pool;
							Vector2 vector = Vector2.one;
							RTHandle rthandle7 = pool7.Get(in vector, this.m_ColorFormat, false);
							this.DoPaniniProjection(cmd, camera, colorBuffer2, rthandle7);
							this.<Render>g__PoolSource|81_0(ref colorBuffer2, rthandle7, ref CS$<>8__locals1);
						}
					}
					using (new ProfilingScope(cmd, ProfilingSampler.Get<HDProfileId>(HDProfileId.UberPost)))
					{
						ComputeShader uberPostCS = this.m_Resources.shaders.uberPostCS;
						UberPostFeatureFlags uberFeatureFlags = this.GetUberFeatureFlags(flag);
						int uberKernel = this.GetUberKernel(uberPostCS, uberFeatureFlags);
						bool flag3 = this.m_Bloom.IsActive() && this.m_BloomFS;
						if (flag3)
						{
							using (new ProfilingScope(cmd, ProfilingSampler.Get<HDProfileId>(HDProfileId.Bloom)))
							{
								this.DoBloom(cmd, camera, colorBuffer2, uberPostCS, uberKernel);
								goto IL_0623;
							}
						}
						cmd.SetComputeTextureParam(uberPostCS, uberKernel, HDShaderIDs._BloomTexture, TextureXR.GetBlackTexture());
						cmd.SetComputeTextureParam(uberPostCS, uberKernel, HDShaderIDs._BloomDirtTexture, Texture2D.blackTexture);
						cmd.SetComputeVectorParam(uberPostCS, HDShaderIDs._BloomParams, Vector4.zero);
						IL_0623:
						using (new ProfilingScope(cmd, ProfilingSampler.Get<HDProfileId>(HDProfileId.ColorGradingLUTBuilder)))
						{
							this.DoColorGrading(cmd, uberPostCS, uberKernel);
						}
						this.DoLensDistortion(cmd, uberPostCS, uberKernel, uberFeatureFlags);
						this.DoChromaticAberration(cmd, uberPostCS, uberKernel, uberFeatureFlags);
						this.DoVignette(cmd, uberPostCS, uberKernel, uberFeatureFlags);
						PostProcessSystem.TargetPool pool8 = this.m_Pool;
						Vector2 vector = Vector2.one;
						RTHandle rthandle8 = pool8.Get(in vector, this.m_ColorFormat, false);
						bool flag4 = this.m_HDInstance.m_CurrentDebugDisplaySettings.data.fullScreenDebugMode == FullScreenDebugMode.ColorLog;
						cmd.SetComputeVectorParam(uberPostCS, "_DebugFlags", new Vector4((float)(flag4 ? 1 : 0), 0f, 0f, 0f));
						cmd.SetComputeTextureParam(uberPostCS, uberKernel, HDShaderIDs._InputTexture, colorBuffer2);
						cmd.SetComputeTextureParam(uberPostCS, uberKernel, HDShaderIDs._OutputTexture, rthandle8);
						cmd.DispatchCompute(uberPostCS, uberKernel, (camera.actualWidth + 7) / 8, (camera.actualHeight + 7) / 8, camera.viewCount);
						this.m_HDInstance.PushFullScreenDebugTexture(camera, cmd, rthandle8, FullScreenDebugMode.ColorLog);
						if (flag3)
						{
							this.m_Pool.Recycle(this.m_BloomTexture);
						}
						this.m_BloomTexture = null;
						this.<Render>g__PoolSource|81_0(ref colorBuffer2, rthandle8, ref CS$<>8__locals1);
					}
					if (camera.frameSettings.IsEnabled(FrameSettingsField.CustomPostProcess))
					{
						using (new ProfilingScope(cmd, ProfilingSampler.Get<HDProfileId>(HDProfileId.CustomPostProcessAfterPP)))
						{
							foreach (string text2 in HDRenderPipeline.defaultAsset.afterPostProcessCustomPostProcesses)
							{
								this.RenderCustomPostProcess(cmd, camera, ref colorBuffer2, CS$<>8__locals1.colorBuffer, Type.GetType(text2));
							}
						}
					}
				}
				if (instance.DynamicResolutionEnabled() && camera.antialiasing == HDAdditionalCameraData.AntialiasingMode.FastApproximateAntialiasing && this.m_AntialiasingFS)
				{
					using (new ProfilingScope(cmd, ProfilingSampler.Get<HDProfileId>(HDProfileId.FXAA)))
					{
						PostProcessSystem.TargetPool pool9 = this.m_Pool;
						Vector2 vector = Vector2.one;
						RTHandle rthandle9 = pool9.Get(in vector, this.m_ColorFormat, false);
						this.DoFXAA(cmd, camera, colorBuffer2, rthandle9);
						this.<Render>g__PoolSource|81_0(ref colorBuffer2, rthandle9, ref CS$<>8__locals1);
					}
				}
				if (instance.DynamicResolutionEnabled() && instance.filter == DynamicResUpscaleFilter.ContrastAdaptiveSharpen)
				{
					using (new ProfilingScope(cmd, ProfilingSampler.Get<HDProfileId>(HDProfileId.ContrastAdaptiveSharpen)))
					{
						PostProcessSystem.TargetPool pool10 = this.m_Pool;
						Vector2 vector = Vector2.one;
						RTHandle rthandle10 = pool10.Get(in vector, this.m_ColorFormat, false);
						ComputeShader contrastAdaptiveSharpenCS = this.m_Resources.shaders.contrastAdaptiveSharpenCS;
						int num2 = contrastAdaptiveSharpenCS.FindKernel("KInitialize");
						int num3 = contrastAdaptiveSharpenCS.FindKernel("KMain");
						if (num2 >= 0 && num3 >= 0)
						{
							cmd.SetComputeFloatParam(contrastAdaptiveSharpenCS, HDShaderIDs._Sharpness, 1f);
							cmd.SetComputeTextureParam(contrastAdaptiveSharpenCS, num3, HDShaderIDs._InputTexture, colorBuffer2);
							cmd.SetComputeVectorParam(contrastAdaptiveSharpenCS, HDShaderIDs._InputTextureDimensions, new Vector4((float)colorBuffer2.rt.width, (float)colorBuffer2.rt.height));
							cmd.SetComputeTextureParam(contrastAdaptiveSharpenCS, num3, HDShaderIDs._OutputTexture, rthandle10);
							cmd.SetComputeVectorParam(contrastAdaptiveSharpenCS, HDShaderIDs._OutputTextureDimensions, new Vector4((float)rthandle10.rt.width, (float)rthandle10.rt.height));
							PostProcessSystem.ValidateComputeBuffer(ref this.m_ContrastAdaptiveSharpen, 2, 16, ComputeBufferType.Default);
							cmd.SetComputeBufferParam(contrastAdaptiveSharpenCS, num2, "CasParameters", this.m_ContrastAdaptiveSharpen);
							cmd.SetComputeBufferParam(contrastAdaptiveSharpenCS, num3, "CasParameters", this.m_ContrastAdaptiveSharpen);
							cmd.DispatchCompute(contrastAdaptiveSharpenCS, num2, 1, 1, 1);
							int num4 = (int)Math.Ceiling((double)((float)rthandle10.rt.width / 16f));
							int num5 = (int)Math.Ceiling((double)((float)rthandle10.rt.height / 16f));
							cmd.DispatchCompute(contrastAdaptiveSharpenCS, num3, num4, num5, camera.viewCount);
						}
						this.<Render>g__PoolSource|81_0(ref colorBuffer2, rthandle10, ref CS$<>8__locals1);
					}
				}
				using (new ProfilingScope(cmd, ProfilingSampler.Get<HDProfileId>(HDProfileId.FinalPost)))
				{
					this.DoFinalPass(cmd, camera, blueNoise, colorBuffer2, afterPostProcessTexture, finalRT, flipY);
					this.<Render>g__PoolSource|81_0(ref colorBuffer2, null, ref CS$<>8__locals1);
				}
			}
			camera.resetPostProcessingHistory = false;
		}

		// Token: 0x0600078E RID: 1934 RVA: 0x0003A5EC File Offset: 0x000387EC
		private void PushUberFeature(UberPostFeatureFlags flags)
		{
			this.m_UberPostFeatureMap.Add((int)flags, "KMain_Variant" + (int)flags);
		}

		// Token: 0x0600078F RID: 1935 RVA: 0x0003A618 File Offset: 0x00038818
		private int GetUberKernel(ComputeShader cs, UberPostFeatureFlags flags)
		{
			string text;
			this.m_UberPostFeatureMap.TryGetValue((int)flags, out text);
			return cs.FindKernel(text);
		}

		// Token: 0x06000790 RID: 1936 RVA: 0x0003A63C File Offset: 0x0003883C
		private UberPostFeatureFlags GetUberFeatureFlags(bool isSceneView)
		{
			UberPostFeatureFlags uberPostFeatureFlags = UberPostFeatureFlags.None;
			if (this.m_ChromaticAberration.IsActive() && this.m_ChromaticAberrationFS)
			{
				uberPostFeatureFlags |= UberPostFeatureFlags.ChromaticAberration;
			}
			if (this.m_Vignette.IsActive() && this.m_VignetteFS)
			{
				uberPostFeatureFlags |= UberPostFeatureFlags.Vignette;
			}
			if (this.m_LensDistortion.IsActive() && !isSceneView && this.m_LensDistortionFS)
			{
				uberPostFeatureFlags |= UberPostFeatureFlags.LensDistortion;
			}
			if (this.m_EnableAlpha)
			{
				uberPostFeatureFlags |= UberPostFeatureFlags.EnableAlpha;
			}
			return uberPostFeatureFlags;
		}

		// Token: 0x06000791 RID: 1937 RVA: 0x0003A6A6 File Offset: 0x000388A6
		private static void ValidateComputeBuffer(ref ComputeBuffer cb, int size, int stride, ComputeBufferType type = ComputeBufferType.Default)
		{
			if (cb == null || cb.count < size)
			{
				CoreUtils.SafeRelease(cb);
				cb = new ComputeBuffer(size, stride, type);
			}
		}

		// Token: 0x06000792 RID: 1938 RVA: 0x0003A6C8 File Offset: 0x000388C8
		private void DoStopNaNs(CommandBuffer cmd, HDCamera camera, RTHandle source, RTHandle destination)
		{
			ComputeShader nanKillerCS = this.m_Resources.shaders.nanKillerCS;
			int num = nanKillerCS.FindKernel("KMain");
			cmd.SetComputeTextureParam(nanKillerCS, num, HDShaderIDs._InputTexture, source);
			cmd.SetComputeTextureParam(nanKillerCS, num, HDShaderIDs._OutputTexture, destination);
			cmd.DispatchCompute(nanKillerCS, num, (camera.actualWidth + 7) / 8, (camera.actualHeight + 7) / 8, camera.viewCount);
		}

		// Token: 0x06000793 RID: 1939 RVA: 0x0003A73C File Offset: 0x0003893C
		private void DoCopyAlpha(CommandBuffer cmd, HDCamera camera, RTHandle source)
		{
			ComputeShader copyAlphaCS = this.m_Resources.shaders.copyAlphaCS;
			int num = copyAlphaCS.FindKernel("KMain");
			cmd.SetComputeTextureParam(copyAlphaCS, num, HDShaderIDs._InputTexture, source);
			cmd.SetComputeTextureParam(copyAlphaCS, num, HDShaderIDs._OutputTexture, this.m_AlphaTexture);
			cmd.DispatchCompute(copyAlphaCS, num, (camera.actualWidth + 7) / 8, (camera.actualHeight + 7) / 8, camera.viewCount);
		}

		// Token: 0x06000794 RID: 1940 RVA: 0x0003A7B3 File Offset: 0x000389B3
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private bool IsExposureFixed()
		{
			return this.m_Exposure.mode.value == ExposureMode.Fixed || this.m_Exposure.mode.value == ExposureMode.UsePhysicalCamera;
		}

		// Token: 0x06000795 RID: 1941 RVA: 0x0003A7DC File Offset: 0x000389DC
		public RTHandle GetExposureTexture(HDCamera camera)
		{
			return camera.GetPreviousFrameRT(2) ?? this.m_EmptyExposureTexture;
		}

		// Token: 0x06000796 RID: 1942 RVA: 0x0003A7EF File Offset: 0x000389EF
		public RTHandle GetPreviousExposureTexture(HDCamera camera)
		{
			return camera.GetCurrentFrameRT(2) ?? this.m_EmptyExposureTexture;
		}

		// Token: 0x06000797 RID: 1943 RVA: 0x0003A804 File Offset: 0x00038A04
		private void DoFixedExposure(CommandBuffer cmd, HDCamera camera)
		{
			ComputeShader exposureCS = this.m_Resources.shaders.exposureCS;
			RTHandle rthandle;
			RTHandle rthandle2;
			PostProcessSystem.GrabExposureHistoryTextures(camera, out rthandle, out rthandle2);
			int num = 0;
			if (this.m_Exposure.mode.value == ExposureMode.Fixed)
			{
				num = exposureCS.FindKernel("KFixedExposure");
				cmd.SetComputeVectorParam(exposureCS, HDShaderIDs._ExposureParams, new Vector4(this.m_Exposure.fixedExposure.value, 0f, 0f, 0f));
			}
			else if (this.m_Exposure.mode == ExposureMode.UsePhysicalCamera)
			{
				num = exposureCS.FindKernel("KManualCameraExposure");
				cmd.SetComputeVectorParam(exposureCS, HDShaderIDs._ExposureParams, new Vector4(this.m_Exposure.compensation.value, this.m_PhysicalCamera.aperture, this.m_PhysicalCamera.shutterSpeed, (float)this.m_PhysicalCamera.iso));
			}
			cmd.SetComputeTextureParam(exposureCS, num, HDShaderIDs._OutputTexture, rthandle);
			cmd.DispatchCompute(exposureCS, num, 1, 1, 1);
		}

		// Token: 0x06000798 RID: 1944 RVA: 0x0003A8FE File Offset: 0x00038AFE
		private static void GrabExposureHistoryTextures(HDCamera camera, out RTHandle previous, out RTHandle next)
		{
			next = camera.GetCurrentFrameRT(2) ?? camera.AllocHistoryFrameRT(2, new Func<string, int, RTHandleSystem, RTHandle>(PostProcessSystem.<>c.<>9.<GrabExposureHistoryTextures>g__Allocator|92_0), 2);
			previous = camera.GetPreviousFrameRT(2);
		}

		// Token: 0x06000799 RID: 1945 RVA: 0x0003A930 File Offset: 0x00038B30
		private void PrepareExposureCurveData(AnimationCurve curve, out float min, out float max)
		{
			if (this.m_ExposureCurveTexture == null)
			{
				this.m_ExposureCurveTexture = new Texture2D(128, 1, TextureFormat.RHalf, false, true)
				{
					name = "Exposure Curve",
					filterMode = FilterMode.Bilinear,
					wrapMode = TextureWrapMode.Clamp
				};
			}
			Color[] exposureCurveColorArray = this.m_ExposureCurveColorArray;
			if (curve == null || curve.length == 0)
			{
				min = 0f;
				max = 0f;
				for (int i = 0; i < 128; i++)
				{
					exposureCurveColorArray[i] = Color.clear;
				}
			}
			else
			{
				min = curve[0].time;
				max = curve[curve.length - 1].time;
				float num = (max - min) / 127f;
				for (int j = 0; j < 128; j++)
				{
					exposureCurveColorArray[j] = new Color(curve.Evaluate(min + num * (float)j), 0f, 0f, 0f);
				}
			}
			this.m_ExposureCurveTexture.SetPixels(exposureCurveColorArray);
			this.m_ExposureCurveTexture.Apply();
		}

		// Token: 0x0600079A RID: 1946 RVA: 0x0003AA44 File Offset: 0x00038C44
		private void DoDynamicExposure(CommandBuffer cmd, HDCamera camera, RTHandle colorBuffer, RTHandle lightingBuffer)
		{
			ComputeShader exposureCS = this.m_Resources.shaders.exposureCS;
			RTHandle rthandle;
			RTHandle rthandle2;
			PostProcessSystem.GrabExposureHistoryTextures(camera, out rthandle, out rthandle2);
			AdaptationMode adaptationMode = this.m_Exposure.adaptationMode.value;
			if (!Application.isPlaying || camera.resetPostProcessingHistory)
			{
				adaptationMode = AdaptationMode.Fixed;
			}
			int num;
			if (camera.resetPostProcessingHistory)
			{
				num = exposureCS.FindKernel("KReset");
				cmd.SetComputeTextureParam(exposureCS, num, HDShaderIDs._OutputTexture, rthandle);
				cmd.DispatchCompute(exposureCS, num, 1, 1, 1);
			}
			this.m_ExposureVariants[0] = 1;
			this.m_ExposureVariants[1] = (int)this.m_Exposure.meteringMode.value;
			this.m_ExposureVariants[2] = (int)adaptationMode;
			this.m_ExposureVariants[3] = 0;
			num = exposureCS.FindKernel("KPrePass");
			cmd.SetComputeIntParams(exposureCS, HDShaderIDs._Variants, this.m_ExposureVariants);
			cmd.SetComputeTextureParam(exposureCS, num, HDShaderIDs._PreviousExposureTexture, rthandle);
			cmd.SetComputeTextureParam(exposureCS, num, HDShaderIDs._SourceTexture, colorBuffer);
			cmd.SetComputeTextureParam(exposureCS, num, HDShaderIDs._OutputTexture, this.m_TempTexture1024);
			cmd.DispatchCompute(exposureCS, num, 128, 128, 1);
			num = exposureCS.FindKernel("KReduction");
			cmd.SetComputeTextureParam(exposureCS, num, HDShaderIDs._PreviousExposureTexture, rthandle);
			cmd.SetComputeTextureParam(exposureCS, num, HDShaderIDs._ExposureCurveTexture, Texture2D.blackTexture);
			cmd.SetComputeTextureParam(exposureCS, num, HDShaderIDs._InputTexture, this.m_TempTexture1024);
			cmd.SetComputeTextureParam(exposureCS, num, HDShaderIDs._OutputTexture, this.m_TempTexture32);
			cmd.DispatchCompute(exposureCS, num, 32, 32, 1);
			if (this.m_Exposure.mode.value == ExposureMode.Automatic)
			{
				cmd.SetComputeVectorParam(exposureCS, HDShaderIDs._ExposureParams, new Vector4(this.m_Exposure.compensation.value, this.m_Exposure.limitMin.value, this.m_Exposure.limitMax.value, 0f));
				this.m_ExposureVariants[3] = 1;
			}
			else if (this.m_Exposure.mode.value == ExposureMode.CurveMapping)
			{
				float num2;
				float num3;
				this.PrepareExposureCurveData(this.m_Exposure.curveMap.value, out num2, out num3);
				cmd.SetComputeTextureParam(exposureCS, num, HDShaderIDs._ExposureCurveTexture, this.m_ExposureCurveTexture);
				cmd.SetComputeVectorParam(exposureCS, HDShaderIDs._ExposureParams, new Vector4(this.m_Exposure.compensation.value, num2, num3, 0f));
				this.m_ExposureVariants[3] = 2;
			}
			cmd.SetComputeVectorParam(exposureCS, HDShaderIDs._AdaptationParams, new Vector4(this.m_Exposure.adaptationSpeedLightToDark.value, this.m_Exposure.adaptationSpeedDarkToLight.value, 0f, 0f));
			cmd.SetComputeIntParams(exposureCS, HDShaderIDs._Variants, this.m_ExposureVariants);
			cmd.SetComputeTextureParam(exposureCS, num, HDShaderIDs._PreviousExposureTexture, rthandle);
			cmd.SetComputeTextureParam(exposureCS, num, HDShaderIDs._InputTexture, this.m_TempTexture32);
			cmd.SetComputeTextureParam(exposureCS, num, HDShaderIDs._OutputTexture, rthandle2);
			cmd.DispatchCompute(exposureCS, num, 1, 1, 1);
		}

		// Token: 0x0600079B RID: 1947 RVA: 0x0003AD50 File Offset: 0x00038F50
		private void DoTemporalAntialiasing(CommandBuffer cmd, HDCamera camera, RTHandle source, RTHandle destination, RTHandle depthBuffer)
		{
			RTHandle rthandle;
			RTHandle rthandle2;
			this.GrabTemporalAntialiasingHistoryTextures(camera, out rthandle, out rthandle2);
			if (this.m_EnableAlpha)
			{
				this.m_TemporalAAMaterial.EnableKeyword("ENABLE_ALPHA");
			}
			if (camera.resetPostProcessingHistory)
			{
				this.m_TAAHistoryBlitPropertyBlock.SetTexture(HDShaderIDs._BlitTexture, source);
				Vector4 rtHandleScale = source.rtHandleProperties.rtHandleScale;
				this.m_TAAHistoryBlitPropertyBlock.SetVector(HDShaderIDs._BlitScaleBias, new Vector4(rtHandleScale.x, rtHandleScale.y, 0f, 0f));
				this.m_TAAHistoryBlitPropertyBlock.SetFloat(HDShaderIDs._BlitMipLevel, 0f);
				HDUtils.DrawFullScreen(cmd, HDUtils.GetBlitMaterial(source.rt.dimension, false), rthandle, this.m_TAAHistoryBlitPropertyBlock, 0);
				HDUtils.DrawFullScreen(cmd, HDUtils.GetBlitMaterial(source.rt.dimension, false), rthandle2, this.m_TAAHistoryBlitPropertyBlock, 0);
			}
			this.m_TAAPropertyBlock.SetInt(HDShaderIDs._StencilMask, 2);
			this.m_TAAPropertyBlock.SetInt(HDShaderIDs._StencilRef, 2);
			this.m_TAAPropertyBlock.SetVector(HDShaderIDs._RTHandleScaleHistory, camera.historyRTHandleProperties.rtHandleScale);
			this.m_TAAPropertyBlock.SetTexture(HDShaderIDs._InputTexture, source);
			this.m_TAAPropertyBlock.SetTexture(HDShaderIDs._InputHistoryTexture, rthandle);
			CoreUtils.SetRenderTarget(cmd, destination, depthBuffer, 0, CubemapFace.Unknown, -1);
			cmd.SetRandomWriteTarget(1, rthandle2);
			cmd.SetGlobalVector(HDShaderIDs._RTHandleScale, destination.rtHandleProperties.rtHandleScale);
			cmd.DrawProcedural(Matrix4x4.identity, this.m_TemporalAAMaterial, 0, MeshTopology.Triangles, 3, 1, this.m_TAAPropertyBlock);
			cmd.DrawProcedural(Matrix4x4.identity, this.m_TemporalAAMaterial, 1, MeshTopology.Triangles, 3, 1, this.m_TAAPropertyBlock);
			cmd.ClearRandomWriteTargets();
		}

		// Token: 0x0600079C RID: 1948 RVA: 0x0003AF01 File Offset: 0x00039101
		private void GrabTemporalAntialiasingHistoryTextures(HDCamera camera, out RTHandle previous, out RTHandle next)
		{
			next = camera.GetCurrentFrameRT(3) ?? camera.AllocHistoryFrameRT(3, new Func<string, int, RTHandleSystem, RTHandle>(this.<GrabTemporalAntialiasingHistoryTextures>g__Allocator|96_0), 2);
			previous = camera.GetPreviousFrameRT(3);
		}

		// Token: 0x0600079D RID: 1949 RVA: 0x0003AF30 File Offset: 0x00039130
		private void DoDepthOfField(CommandBuffer cmd, HDCamera camera, RTHandle source, RTHandle destination, bool taaEnabled)
		{
			bool flag = this.m_DepthOfField.IsNearLayerActive();
			bool flag2 = this.m_DepthOfField.IsFarLayerActive();
			bool flag3 = flag && flag2;
			bool flag4 = !camera.xr.singlePassEnabled;
			bool highQualityFiltering = this.m_DepthOfField.highQualityFiltering;
			int bladeCount = this.m_PhysicalCamera.bladeCount;
			float num = (this.m_PhysicalCamera.aperture - 1f) / 31f;
			num *= 360f / (float)bladeCount * 0.017453292f;
			float num2 = 1f;
			if (this.m_PhysicalCamera.curvature.y - this.m_PhysicalCamera.curvature.x > 0f)
			{
				num2 = (this.m_PhysicalCamera.aperture - this.m_PhysicalCamera.curvature.x) / (this.m_PhysicalCamera.curvature.y - this.m_PhysicalCamera.curvature.x);
			}
			num2 = Mathf.Clamp01(num2);
			num2 = Mathf.Lerp(num2, 0f, Mathf.Abs(this.m_PhysicalCamera.anamorphism));
			float num3 = this.m_PhysicalCamera.anamorphism / 4f;
			float num4 = this.m_PhysicalCamera.barrelClipping / 3f;
			float num5 = 1f / (float)this.m_DepthOfField.resolution;
			Vector2 vector = new Vector2(num5, num5);
			int num6 = Mathf.RoundToInt((float)camera.actualWidth * num5);
			int num7 = Mathf.RoundToInt((float)camera.actualHeight * num5);
			int num8 = (num6 + 7) / 8;
			int num9 = (num7 + 7) / 8;
			cmd.SetGlobalVector(HDShaderIDs._TargetScale, new Vector4((float)this.m_DepthOfField.resolution, num5, 0f, 0f));
			float num10 = (float)camera.actualHeight / 1080f * (num5 * 2f);
			int num11 = Mathf.CeilToInt((float)this.m_DepthOfField.farSampleCount * num10);
			int num12 = Mathf.CeilToInt((float)this.m_DepthOfField.nearSampleCount * num10);
			num11 = Mathf.Max(3, num11);
			num12 = Mathf.Max(3, num12);
			float num13 = this.m_DepthOfField.farMaxBlur * num10;
			float num14 = this.m_DepthOfField.nearMaxBlur * num10;
			Vector4 vector2 = RTHandles.rtHandleProperties.rtHandleScale;
			RTHandle rthandle = null;
			RTHandle rthandle2 = null;
			RTHandle rthandle3 = null;
			RTHandle rthandle4 = null;
			RTHandle rthandle5 = null;
			RTHandle rthandle6 = null;
			RTHandle rthandle7 = null;
			RTHandle rthandle8 = null;
			if (flag)
			{
				rthandle = this.m_Pool.Get(in vector, this.m_ColorFormat, false);
				rthandle2 = this.m_Pool.Get(in vector, this.m_ColorFormat, false);
				rthandle3 = this.m_Pool.Get(in vector, GraphicsFormat.R16_SFloat, false);
				rthandle4 = this.m_Pool.Get(in vector, GraphicsFormat.R16_SFloat, false);
				rthandle5 = this.m_Pool.Get(in vector, GraphicsFormat.R16_SFloat, false);
			}
			if (flag2)
			{
				rthandle6 = this.m_Pool.Get(in vector, this.m_ColorFormat, true);
				rthandle7 = this.m_Pool.Get(in vector, this.m_ColorFormat, false);
				rthandle8 = this.m_Pool.Get(in vector, GraphicsFormat.R16_SFloat, true);
			}
			PostProcessSystem.TargetPool pool = this.m_Pool;
			Vector2 vector3 = Vector2.one;
			RTHandle rthandle9 = pool.Get(in vector3, GraphicsFormat.R16_SFloat, false);
			using (new ProfilingScope(cmd, ProfilingSampler.Get<HDProfileId>(HDProfileId.DepthOfFieldKernel)))
			{
				ComputeShader computeShader = this.m_Resources.shaders.depthOfFieldKernelCS;
				int num15 = computeShader.FindKernel("KParametricBlurKernel");
				if (flag)
				{
					PostProcessSystem.ValidateComputeBuffer(ref this.m_BokehNearKernel, num12 * num12, 4, ComputeBufferType.Default);
					cmd.SetComputeVectorParam(computeShader, HDShaderIDs._Params1, new Vector4((float)num12, num2, (float)bladeCount, num));
					cmd.SetComputeVectorParam(computeShader, HDShaderIDs._Params2, new Vector4(num3, 0f, 0f, 0f));
					cmd.SetComputeBufferParam(computeShader, num15, HDShaderIDs._BokehKernel, this.m_BokehNearKernel);
					cmd.DispatchCompute(computeShader, num15, Mathf.CeilToInt((float)(num12 * num12) / 64f), 1, 1);
				}
				if (flag2)
				{
					PostProcessSystem.ValidateComputeBuffer(ref this.m_BokehFarKernel, num11 * num11, 4, ComputeBufferType.Default);
					cmd.SetComputeVectorParam(computeShader, HDShaderIDs._Params1, new Vector4((float)num11, num2, (float)bladeCount, num));
					cmd.SetComputeVectorParam(computeShader, HDShaderIDs._Params2, new Vector4(num3, 0f, 0f, 0f));
					cmd.SetComputeBufferParam(computeShader, num15, HDShaderIDs._BokehKernel, this.m_BokehFarKernel);
					cmd.DispatchCompute(computeShader, num15, Mathf.CeilToInt((float)(num11 * num11) / 64f), 1, 1);
				}
			}
			using (new ProfilingScope(cmd, ProfilingSampler.Get<HDProfileId>(HDProfileId.DepthOfFieldCoC)))
			{
				ComputeShader computeShader = this.m_Resources.shaders.depthOfFieldCoCCS;
				int num15;
				if (this.m_DepthOfField.focusMode.value == DepthOfFieldMode.UsePhysicalCamera)
				{
					float num16 = camera.camera.focalLength / 1000f;
					float num17 = camera.camera.focalLength / this.m_PhysicalCamera.aperture;
					float value = this.m_DepthOfField.focusDistance.value;
					float num18 = num17 * num16 / Mathf.Max(value - num16, 1E-06f);
					num15 = computeShader.FindKernel("KMainPhysical");
					cmd.SetComputeVectorParam(computeShader, HDShaderIDs._Params, new Vector4(value, num18, 0f, 0f));
				}
				else
				{
					float value2 = this.m_DepthOfField.nearFocusEnd.value;
					float num19 = Mathf.Min(this.m_DepthOfField.nearFocusStart.value, value2 - 1E-05f);
					float num20 = Mathf.Max(this.m_DepthOfField.farFocusStart.value, value2);
					float num21 = Mathf.Max(this.m_DepthOfField.farFocusEnd.value, num20 + 1E-05f);
					num15 = computeShader.FindKernel("KMainManual");
					cmd.SetComputeVectorParam(computeShader, HDShaderIDs._Params, new Vector4(num19, value2, num20, num21));
				}
				cmd.SetComputeTextureParam(computeShader, num15, HDShaderIDs._OutputCoCTexture, rthandle9);
				cmd.DispatchCompute(computeShader, num15, (camera.actualWidth + 7) / 8, (camera.actualHeight + 7) / 8, camera.viewCount);
				if (taaEnabled)
				{
					RTHandle rthandle10;
					RTHandle rthandle11;
					PostProcessSystem.GrabCoCHistory(camera, out rthandle10, out rthandle11);
					vector2 = new Vector2(camera.historyRTHandleProperties.rtHandleScale.z, camera.historyRTHandleProperties.rtHandleScale.w);
					computeShader = this.m_Resources.shaders.depthOfFieldCoCReprojectCS;
					num15 = computeShader.FindKernel("KMain");
					cmd.SetComputeVectorParam(computeShader, HDShaderIDs._Params, new Vector4(camera.resetPostProcessingHistory ? 0f : 0.91f, vector2.x, vector2.y, 0f));
					cmd.SetComputeTextureParam(computeShader, num15, HDShaderIDs._InputCoCTexture, rthandle9);
					cmd.SetComputeTextureParam(computeShader, num15, HDShaderIDs._InputHistoryCoCTexture, rthandle10);
					cmd.SetComputeTextureParam(computeShader, num15, HDShaderIDs._OutputCoCTexture, rthandle11);
					cmd.DispatchCompute(computeShader, num15, (camera.actualWidth + 7) / 8, (camera.actualHeight + 7) / 8, camera.viewCount);
					this.m_Pool.Recycle(rthandle9);
					rthandle9 = rthandle11;
				}
				this.m_HDInstance.PushFullScreenDebugTexture(camera, cmd, rthandle9, FullScreenDebugMode.DepthOfFieldCoc);
			}
			using (new ProfilingScope(cmd, ProfilingSampler.Get<HDProfileId>(HDProfileId.DepthOfFieldPrefilter)))
			{
				ComputeShader computeShader = this.m_Resources.shaders.depthOfFieldPrefilterCS;
				int num15;
				if (this.m_EnableAlpha)
				{
					num15 = computeShader.FindKernel((this.m_DepthOfField.resolution == DepthOfFieldResolution.Full) ? (flag3 ? "KMainNearFarFullResAlpha" : (flag ? "KMainNearFullResAlpha" : "KMainFarFullResAlpha")) : (flag3 ? "KMainNearFarAlpha" : (flag ? "KMainNearAlpha" : "KMainFarAlpha")));
				}
				else
				{
					num15 = computeShader.FindKernel((this.m_DepthOfField.resolution == DepthOfFieldResolution.Full) ? (flag3 ? "KMainNearFarFullRes" : (flag ? "KMainNearFullRes" : "KMainFarFullRes")) : (flag3 ? "KMainNearFar" : (flag ? "KMainNear" : "KMainFar")));
				}
				cmd.SetComputeTextureParam(computeShader, num15, HDShaderIDs._InputTexture, source);
				cmd.SetComputeTextureParam(computeShader, num15, HDShaderIDs._InputCoCTexture, rthandle9);
				cmd.SetComputeVectorParam(computeShader, HDShaderIDs._CoCTargetScale, vector2);
				if (flag)
				{
					cmd.SetComputeTextureParam(computeShader, num15, HDShaderIDs._OutputNearCoCTexture, rthandle3);
					cmd.SetComputeTextureParam(computeShader, num15, HDShaderIDs._OutputNearTexture, rthandle);
				}
				if (flag2)
				{
					cmd.SetComputeTextureParam(computeShader, num15, HDShaderIDs._OutputFarCoCTexture, rthandle8);
					cmd.SetComputeTextureParam(computeShader, num15, HDShaderIDs._OutputFarTexture, rthandle6);
				}
				cmd.DispatchCompute(computeShader, num15, num8, num9, camera.viewCount);
			}
			if (flag2)
			{
				using (new ProfilingScope(cmd, ProfilingSampler.Get<HDProfileId>(HDProfileId.DepthOfFieldPyramid)))
				{
					int num22 = ((num6 >> 1) + 7) / 8;
					int num23 = ((num7 >> 1) + 7) / 8;
					ComputeShader computeShader;
					int num15;
					if (this.m_UseSafePath)
					{
						computeShader = this.m_Resources.shaders.depthOfFieldMipSafeCS;
						num15 = computeShader.FindKernel(this.m_EnableAlpha ? "KMainAlpha" : "KMain");
						float num24 = num5;
						for (int i = 0; i < 4; i++)
						{
							num24 *= 0.5f;
							Vector2Int vector2Int = new Vector2Int(Mathf.RoundToInt((float)camera.actualWidth * num24), Mathf.RoundToInt((float)camera.actualHeight * num24));
							PostProcessSystem.TargetPool pool2 = this.m_Pool;
							vector3 = new Vector2(num24, num24);
							RTHandle rthandle12 = pool2.Get(in vector3, this.m_ColorFormat, false);
							cmd.SetComputeVectorParam(computeShader, HDShaderIDs._TexelSize, new Vector4((float)vector2Int.x, (float)vector2Int.y, 1f / (float)vector2Int.x, 1f / (float)vector2Int.y));
							int num25 = (vector2Int.x + 7) / 8;
							int num26 = (vector2Int.y + 7) / 8;
							cmd.SetComputeTextureParam(computeShader, num15, HDShaderIDs._InputTexture, rthandle6);
							cmd.SetComputeTextureParam(computeShader, num15, HDShaderIDs._OutputTexture, rthandle12);
							cmd.DispatchCompute(computeShader, num15, num25, num26, camera.viewCount);
							cmd.SetComputeTextureParam(computeShader, num15, HDShaderIDs._InputTexture, rthandle12);
							cmd.SetComputeTextureParam(computeShader, num15, HDShaderIDs._OutputTexture, rthandle6, i + 1);
							cmd.DispatchCompute(computeShader, num15, num25, num26, camera.viewCount);
							this.m_Pool.Recycle(rthandle12);
						}
					}
					else
					{
						computeShader = this.m_Resources.shaders.depthOfFieldMipCS;
						num15 = computeShader.FindKernel(this.m_EnableAlpha ? "KMainColorAlpha" : "KMainColor");
						cmd.SetComputeTextureParam(computeShader, num15, HDShaderIDs._InputTexture, rthandle6, 0);
						cmd.SetComputeTextureParam(computeShader, num15, HDShaderIDs._OutputMip1, rthandle6, 1);
						cmd.SetComputeTextureParam(computeShader, num15, HDShaderIDs._OutputMip2, rthandle6, 2);
						cmd.SetComputeTextureParam(computeShader, num15, HDShaderIDs._OutputMip3, rthandle6, 3);
						cmd.SetComputeTextureParam(computeShader, num15, HDShaderIDs._OutputMip4, rthandle6, 4);
						cmd.DispatchCompute(computeShader, num15, num22, num23, camera.viewCount);
					}
					computeShader = this.m_Resources.shaders.depthOfFieldMipCS;
					num15 = computeShader.FindKernel("KMainCoC");
					cmd.SetComputeTextureParam(computeShader, num15, HDShaderIDs._InputTexture, rthandle8, 0);
					cmd.SetComputeTextureParam(computeShader, num15, HDShaderIDs._OutputMip1, rthandle8, 1);
					cmd.SetComputeTextureParam(computeShader, num15, HDShaderIDs._OutputMip2, rthandle8, 2);
					cmd.SetComputeTextureParam(computeShader, num15, HDShaderIDs._OutputMip3, rthandle8, 3);
					cmd.SetComputeTextureParam(computeShader, num15, HDShaderIDs._OutputMip4, rthandle8, 4);
					cmd.DispatchCompute(computeShader, num15, num22, num23, camera.viewCount);
				}
			}
			if (flag)
			{
				using (new ProfilingScope(cmd, ProfilingSampler.Get<HDProfileId>(HDProfileId.DepthOfFieldDilate)))
				{
					ComputeShader computeShader = this.m_Resources.shaders.depthOfFieldDilateCS;
					int num15 = computeShader.FindKernel("KMain");
					cmd.SetComputeVectorParam(computeShader, HDShaderIDs._Params, new Vector4((float)(num6 - 1), (float)(num7 - 1), 0f, 0f));
					int num27 = Mathf.CeilToInt((num14 + 2f) / 4f);
					cmd.SetComputeTextureParam(computeShader, num15, HDShaderIDs._InputCoCTexture, rthandle3);
					cmd.SetComputeTextureParam(computeShader, num15, HDShaderIDs._OutputCoCTexture, rthandle5);
					cmd.DispatchCompute(computeShader, num15, num8, num9, camera.viewCount);
					if (num27 > 1)
					{
						RTHandle rthandle13 = rthandle5;
						RTHandle rthandle14 = this.m_Pool.Get(in vector, GraphicsFormat.R16_SFloat, false);
						for (int j = 1; j < num27; j++)
						{
							cmd.SetComputeTextureParam(computeShader, num15, HDShaderIDs._InputCoCTexture, rthandle13);
							cmd.SetComputeTextureParam(computeShader, num15, HDShaderIDs._OutputCoCTexture, rthandle14);
							cmd.DispatchCompute(computeShader, num15, num8, num9, camera.viewCount);
							CoreUtils.Swap<RTHandle>(ref rthandle13, ref rthandle14);
						}
						rthandle5 = rthandle13;
						this.m_Pool.Recycle(rthandle14);
					}
				}
			}
			if (flag4)
			{
				using (new ProfilingScope(cmd, ProfilingSampler.Get<HDProfileId>(HDProfileId.DepthOfFieldTileMax)))
				{
					PostProcessSystem.ValidateComputeBuffer(ref this.m_BokehIndirectCmd, 6, 4, ComputeBufferType.DrawIndirect);
					PostProcessSystem.ValidateComputeBuffer(ref this.m_NearBokehTileList, num8 * num9, 4, ComputeBufferType.Append);
					PostProcessSystem.ValidateComputeBuffer(ref this.m_FarBokehTileList, num8 * num9, 4, ComputeBufferType.Append);
					this.m_NearBokehTileList.SetCounterValue(0U);
					this.m_FarBokehTileList.SetCounterValue(0U);
					ComputeShader computeShader = this.m_Resources.shaders.depthOfFieldTileMaxCS;
					int num15 = computeShader.FindKernel("KClear");
					cmd.SetComputeBufferParam(computeShader, num15, HDShaderIDs._IndirectBuffer, this.m_BokehIndirectCmd);
					cmd.DispatchCompute(computeShader, num15, 1, 1, 1);
					num15 = computeShader.FindKernel(flag3 ? "KMainNearFar" : (flag ? "KMainNear" : "KMainFar"));
					cmd.SetComputeVectorParam(computeShader, HDShaderIDs._Params, new Vector4((float)(num6 - 1), (float)(num7 - 1), 0f, 0f));
					cmd.SetComputeBufferParam(computeShader, num15, HDShaderIDs._IndirectBuffer, this.m_BokehIndirectCmd);
					if (flag)
					{
						cmd.SetComputeTextureParam(computeShader, num15, HDShaderIDs._InputNearCoCTexture, rthandle5);
						cmd.SetComputeBufferParam(computeShader, num15, HDShaderIDs._NearTileList, this.m_NearBokehTileList);
					}
					if (flag2)
					{
						cmd.SetComputeTextureParam(computeShader, num15, HDShaderIDs._InputFarCoCTexture, rthandle8);
						cmd.SetComputeBufferParam(computeShader, num15, HDShaderIDs._FarTileList, this.m_FarBokehTileList);
					}
					cmd.DispatchCompute(computeShader, num15, num8, num9, 1);
				}
			}
			if (flag2)
			{
				using (new ProfilingScope(cmd, ProfilingSampler.Get<HDProfileId>(HDProfileId.DepthOfFieldGatherFar)))
				{
					if (flag4)
					{
						cmd.SetRenderTarget(rthandle7);
						cmd.ClearRenderTarget(false, true, Color.clear);
					}
					ComputeShader computeShader = this.m_Resources.shaders.depthOfFieldGatherCS;
					int num15 = (this.m_EnableAlpha ? computeShader.FindKernel(flag4 ? "KMainFarTilesAlpha" : "KMainFarAlpha") : computeShader.FindKernel(flag4 ? "KMainFarTiles" : "KMainFar"));
					cmd.SetComputeVectorParam(computeShader, HDShaderIDs._Params, new Vector4((float)num11, (float)(num11 * num11), num4, num13));
					cmd.SetComputeVectorParam(computeShader, HDShaderIDs._TexelSize, new Vector4((float)num6, (float)num7, 1f / (float)num6, 1f / (float)num7));
					cmd.SetComputeTextureParam(computeShader, num15, HDShaderIDs._InputTexture, rthandle6);
					cmd.SetComputeTextureParam(computeShader, num15, HDShaderIDs._InputCoCTexture, rthandle8);
					cmd.SetComputeTextureParam(computeShader, num15, HDShaderIDs._OutputTexture, rthandle7);
					cmd.SetComputeBufferParam(computeShader, num15, HDShaderIDs._BokehKernel, this.m_BokehFarKernel);
					if (flag4)
					{
						cmd.SetComputeBufferParam(computeShader, num15, HDShaderIDs._TileList, this.m_FarBokehTileList);
						cmd.DispatchCompute(computeShader, num15, this.m_BokehIndirectCmd, 12U);
					}
					else
					{
						cmd.DispatchCompute(computeShader, num15, num8, num9, camera.viewCount);
					}
				}
			}
			if (flag)
			{
				using (new ProfilingScope(cmd, ProfilingSampler.Get<HDProfileId>(HDProfileId.DepthOfFieldPreCombine)))
				{
					if (flag2)
					{
						ComputeShader computeShader = this.m_Resources.shaders.depthOfFieldCombineCS;
						int num15 = computeShader.FindKernel(this.m_EnableAlpha ? "KMainPreCombineFarAlpha" : "KMainPreCombineFar");
						cmd.SetComputeTextureParam(computeShader, num15, HDShaderIDs._InputTexture, rthandle);
						cmd.SetComputeTextureParam(computeShader, num15, HDShaderIDs._InputFarTexture, rthandle7);
						cmd.SetComputeTextureParam(computeShader, num15, HDShaderIDs._InputCoCTexture, rthandle8);
						cmd.SetComputeTextureParam(computeShader, num15, HDShaderIDs._OutputTexture, rthandle2);
						cmd.DispatchCompute(computeShader, num15, num8, num9, camera.viewCount);
						CoreUtils.Swap<RTHandle>(ref rthandle, ref rthandle2);
					}
				}
				using (new ProfilingScope(cmd, ProfilingSampler.Get<HDProfileId>(HDProfileId.DepthOfFieldGatherNear)))
				{
					if (flag4)
					{
						if (!flag2)
						{
							cmd.SetRenderTarget(rthandle2);
							cmd.ClearRenderTarget(false, true, Color.clear);
						}
						cmd.SetRenderTarget(rthandle4);
						cmd.ClearRenderTarget(false, true, Color.clear);
					}
					ComputeShader computeShader = this.m_Resources.shaders.depthOfFieldGatherCS;
					int num15 = (this.m_EnableAlpha ? computeShader.FindKernel(flag4 ? "KMainNearTilesAlpha" : "KMainNearAlpha") : computeShader.FindKernel(flag4 ? "KMainNearTiles" : "KMainNear"));
					cmd.SetComputeVectorParam(computeShader, HDShaderIDs._Params, new Vector4((float)num12, (float)(num12 * num12), num4, num14));
					cmd.SetComputeVectorParam(computeShader, HDShaderIDs._TexelSize, new Vector4((float)num6, (float)num7, 1f / (float)num6, 1f / (float)num7));
					cmd.SetComputeTextureParam(computeShader, num15, HDShaderIDs._InputTexture, rthandle);
					cmd.SetComputeTextureParam(computeShader, num15, HDShaderIDs._InputCoCTexture, rthandle3);
					cmd.SetComputeTextureParam(computeShader, num15, HDShaderIDs._InputDilatedCoCTexture, rthandle5);
					cmd.SetComputeTextureParam(computeShader, num15, HDShaderIDs._OutputTexture, rthandle2);
					cmd.SetComputeTextureParam(computeShader, num15, HDShaderIDs._OutputAlphaTexture, rthandle4);
					cmd.SetComputeBufferParam(computeShader, num15, HDShaderIDs._BokehKernel, this.m_BokehNearKernel);
					if (flag4)
					{
						cmd.SetComputeBufferParam(computeShader, num15, HDShaderIDs._TileList, this.m_NearBokehTileList);
						cmd.DispatchCompute(computeShader, num15, this.m_BokehIndirectCmd, 0U);
					}
					else
					{
						cmd.DispatchCompute(computeShader, num15, num8, num9, camera.viewCount);
					}
				}
			}
			using (new ProfilingScope(cmd, ProfilingSampler.Get<HDProfileId>(HDProfileId.DepthOfFieldCombine)))
			{
				ComputeShader computeShader = this.m_Resources.shaders.depthOfFieldCombineCS;
				int num15;
				if (this.m_EnableAlpha)
				{
					if (this.m_DepthOfField.resolution == DepthOfFieldResolution.Full)
					{
						num15 = computeShader.FindKernel(flag3 ? "KMainNearFarFullResAlpha" : (flag ? "KMainNearFullResAlpha" : "KMainFarFullResAlpha"));
					}
					else if (highQualityFiltering)
					{
						num15 = computeShader.FindKernel(flag3 ? "KMainNearFarHighQAlpha" : (flag ? "KMainNearHighQAlpha" : "KMainFarHighQAlpha"));
					}
					else
					{
						num15 = computeShader.FindKernel(flag3 ? "KMainNearFarLowQAlpha" : (flag ? "KMainNearLowQAlpha" : "KMainFarLowQAlpha"));
					}
				}
				else if (this.m_DepthOfField.resolution == DepthOfFieldResolution.Full)
				{
					num15 = computeShader.FindKernel(flag3 ? "KMainNearFarFullRes" : (flag ? "KMainNearFullRes" : "KMainFarFullRes"));
				}
				else if (highQualityFiltering)
				{
					num15 = computeShader.FindKernel(flag3 ? "KMainNearFarHighQ" : (flag ? "KMainNearHighQ" : "KMainFarHighQ"));
				}
				else
				{
					num15 = computeShader.FindKernel(flag3 ? "KMainNearFarLowQ" : (flag ? "KMainNearLowQ" : "KMainFarLowQ"));
				}
				if (flag)
				{
					cmd.SetComputeTextureParam(computeShader, num15, HDShaderIDs._InputNearTexture, rthandle2);
					cmd.SetComputeTextureParam(computeShader, num15, HDShaderIDs._InputNearAlphaTexture, rthandle4);
				}
				if (flag2)
				{
					cmd.SetComputeTextureParam(computeShader, num15, HDShaderIDs._InputFarTexture, rthandle7);
					cmd.SetComputeTextureParam(computeShader, num15, HDShaderIDs._InputCoCTexture, rthandle9);
				}
				cmd.SetComputeTextureParam(computeShader, num15, HDShaderIDs._InputTexture, source);
				cmd.SetComputeTextureParam(computeShader, num15, HDShaderIDs._OutputTexture, destination);
				cmd.DispatchCompute(computeShader, num15, (camera.actualWidth + 7) / 8, (camera.actualHeight + 7) / 8, camera.viewCount);
			}
			if (flag2)
			{
				this.m_Pool.Recycle(rthandle6);
				this.m_Pool.Recycle(rthandle7);
				this.m_Pool.Recycle(rthandle8);
			}
			if (flag)
			{
				this.m_Pool.Recycle(rthandle);
				this.m_Pool.Recycle(rthandle2);
				this.m_Pool.Recycle(rthandle3);
				this.m_Pool.Recycle(rthandle4);
				this.m_Pool.Recycle(rthandle5);
			}
			if (!taaEnabled)
			{
				this.m_Pool.Recycle(rthandle9);
			}
		}

		// Token: 0x0600079E RID: 1950 RVA: 0x0003C514 File Offset: 0x0003A714
		private static void GrabCoCHistory(HDCamera camera, out RTHandle previous, out RTHandle next)
		{
			next = camera.GetCurrentFrameRT(4) ?? camera.AllocHistoryFrameRT(4, new Func<string, int, RTHandleSystem, RTHandle>(PostProcessSystem.<>c.<>9.<GrabCoCHistory>g__Allocator|98_0), 2);
			previous = camera.GetPreviousFrameRT(4);
		}

		// Token: 0x0600079F RID: 1951 RVA: 0x0003C544 File Offset: 0x0003A744
		private void DoMotionBlur(CommandBuffer cmd, HDCamera camera, RTHandle source, RTHandle destination)
		{
			int num = 32;
			if (this.m_MotionBlurSupportsScattering)
			{
				num = 16;
			}
			int num2 = Mathf.CeilToInt((float)(camera.actualWidth / num));
			int num3 = Mathf.CeilToInt((float)(camera.actualHeight / num));
			Vector2 vector = new Vector2((float)num2 / (float)camera.actualWidth, (float)num3 / (float)camera.actualHeight);
			Vector4 vector2 = new Vector4((float)num2, (float)num3, 1f / (float)num2, 1f / (float)num3);
			PostProcessSystem.TargetPool pool = this.m_Pool;
			Vector2 vector3 = Vector2.one;
			RTHandle rthandle = pool.Get(in vector3, GraphicsFormat.B10G11R11_UFloatPack32, false);
			RTHandle rthandle2 = this.m_Pool.Get(in vector, GraphicsFormat.B10G11R11_UFloatPack32, false);
			RTHandle rthandle3 = this.m_Pool.Get(in vector, GraphicsFormat.B10G11R11_UFloatPack32, false);
			RTHandle rthandle4 = null;
			RTHandle rthandle5 = null;
			if (this.m_MotionBlurSupportsScattering)
			{
				rthandle4 = this.m_Pool.Get(in vector, GraphicsFormat.R32_UInt, false);
				rthandle5 = this.m_Pool.Get(in vector, GraphicsFormat.R16_SFloat, false);
			}
			vector3 = new Vector2((float)camera.actualWidth, (float)camera.actualHeight);
			float magnitude = vector3.magnitude;
			Vector4 vector4 = new Vector4(magnitude, magnitude * magnitude, this.m_MotionBlur.minimumVelocity.value, this.m_MotionBlur.minimumVelocity.value * this.m_MotionBlur.minimumVelocity.value);
			Vector4 vector5 = new Vector4(this.m_MotionBlur.intensity.value, this.m_MotionBlur.maximumVelocity.value / magnitude, 0.25f, this.m_MotionBlur.cameraRotationVelocityClamp.value);
			uint sampleCount = (uint)this.m_MotionBlur.sampleCount;
			Vector4 vector6 = new Vector4(this.m_MotionBlurSupportsScattering ? (sampleCount + (sampleCount & 1U)) : sampleCount, (float)num, this.m_MotionBlur.depthComparisonExtent.value, this.m_MotionBlur.cameraMotionBlur.value ? 0f : 1f);
			int num4 = 8;
			int num5 = 8;
			ComputeShader computeShader;
			int num7;
			int num8;
			using (new ProfilingScope(cmd, ProfilingSampler.Get<HDProfileId>(HDProfileId.MotionBlurMotionVecPrep)))
			{
				computeShader = this.m_Resources.shaders.motionBlurMotionVecPrepCS;
				int num6 = computeShader.FindKernel("MotionVecPreppingCS");
				cmd.SetComputeTextureParam(computeShader, num6, HDShaderIDs._MotionVecAndDepth, rthandle);
				cmd.SetComputeVectorParam(computeShader, HDShaderIDs._MotionBlurParams, vector4);
				cmd.SetComputeVectorParam(computeShader, HDShaderIDs._MotionBlurParams1, vector5);
				cmd.SetComputeVectorParam(computeShader, HDShaderIDs._MotionBlurParams2, vector6);
				cmd.SetComputeMatrixParam(computeShader, HDShaderIDs._PrevVPMatrixNoTranslation, camera.mainViewConstants.prevViewProjMatrixNoCameraTrans);
				num7 = (camera.actualWidth + (num4 - 1)) / num4;
				num8 = (camera.actualHeight + (num5 - 1)) / num5;
				cmd.DispatchCompute(computeShader, num6, num7, num8, camera.viewCount);
			}
			using (new ProfilingScope(cmd, ProfilingSampler.Get<HDProfileId>(HDProfileId.MotionBlurTileMinMax)))
			{
				computeShader = this.m_Resources.shaders.motionBlurTileGenCS;
				int num6;
				if (this.m_MotionBlurSupportsScattering)
				{
					num6 = computeShader.FindKernel("TileGenPass_Scattering");
				}
				else
				{
					num6 = computeShader.FindKernel("TileGenPass");
				}
				cmd.SetComputeTextureParam(computeShader, num6, HDShaderIDs._TileMinMaxMotionVec, rthandle2);
				cmd.SetComputeTextureParam(computeShader, num6, HDShaderIDs._MotionVecAndDepth, rthandle);
				cmd.SetComputeVectorParam(computeShader, HDShaderIDs._MotionBlurParams, vector4);
				cmd.SetComputeVectorParam(computeShader, HDShaderIDs._MotionBlurParams1, vector5);
				if (this.m_MotionBlurSupportsScattering)
				{
					cmd.SetComputeTextureParam(computeShader, num6, HDShaderIDs._TileToScatterMax, rthandle4);
					cmd.SetComputeTextureParam(computeShader, num6, HDShaderIDs._TileToScatterMin, rthandle5);
				}
				num7 = (camera.actualWidth + (num - 1)) / num;
				num8 = (camera.actualHeight + (num - 1)) / num;
				cmd.DispatchCompute(computeShader, num6, num7, num8, camera.viewCount);
			}
			using (new ProfilingScope(cmd, this.m_MotionBlurSupportsScattering ? ProfilingSampler.Get<HDProfileId>(HDProfileId.MotionBlurTileScattering) : ProfilingSampler.Get<HDProfileId>(HDProfileId.MotionBlurTileNeighbourhood)))
			{
				computeShader = this.m_Resources.shaders.motionBlurTileGenCS;
				int num6;
				if (this.m_MotionBlurSupportsScattering)
				{
					num6 = computeShader.FindKernel("TileNeighbourhood_Scattering");
				}
				else
				{
					num6 = computeShader.FindKernel("TileNeighbourhood");
				}
				cmd.SetComputeVectorParam(computeShader, HDShaderIDs._TileTargetSize, vector2);
				cmd.SetComputeTextureParam(computeShader, num6, HDShaderIDs._TileMinMaxMotionVec, rthandle2);
				cmd.SetComputeTextureParam(computeShader, num6, HDShaderIDs._TileMaxNeighbourhood, rthandle3);
				if (this.m_MotionBlurSupportsScattering)
				{
					cmd.SetComputeTextureParam(computeShader, num6, HDShaderIDs._TileToScatterMax, rthandle4);
					cmd.SetComputeTextureParam(computeShader, num6, HDShaderIDs._TileToScatterMin, rthandle5);
				}
				num4 = 8;
				num5 = 8;
				num7 = (num2 + (num4 - 1)) / num4;
				num8 = (num3 + (num5 - 1)) / num5;
				cmd.DispatchCompute(computeShader, num6, num7, num8, camera.viewCount);
			}
			if (this.m_MotionBlurSupportsScattering)
			{
				int num6 = computeShader.FindKernel("TileMinMaxMerge");
				cmd.SetComputeVectorParam(computeShader, HDShaderIDs._TileTargetSize, vector2);
				cmd.SetComputeTextureParam(computeShader, num6, HDShaderIDs._TileToScatterMax, rthandle4);
				cmd.SetComputeTextureParam(computeShader, num6, HDShaderIDs._TileToScatterMin, rthandle5);
				cmd.SetComputeTextureParam(computeShader, num6, HDShaderIDs._TileMaxNeighbourhood, rthandle3);
				cmd.DispatchCompute(computeShader, num6, num7, num8, camera.viewCount);
			}
			using (new ProfilingScope(cmd, ProfilingSampler.Get<HDProfileId>(HDProfileId.MotionBlurKernel)))
			{
				computeShader = this.m_Resources.shaders.motionBlurCS;
				int num6 = computeShader.FindKernel("MotionBlurCS");
				cmd.SetComputeVectorParam(computeShader, HDShaderIDs._TileTargetSize, vector2);
				cmd.SetComputeTextureParam(computeShader, num6, HDShaderIDs._MotionVecAndDepth, rthandle);
				cmd.SetComputeTextureParam(computeShader, num6, HDShaderIDs._OutputTexture, destination);
				cmd.SetComputeTextureParam(computeShader, num6, HDShaderIDs._TileMaxNeighbourhood, rthandle3);
				cmd.SetComputeTextureParam(computeShader, num6, HDShaderIDs._InputTexture, source);
				cmd.SetComputeVectorParam(computeShader, HDShaderIDs._MotionBlurParams, vector4);
				cmd.SetComputeVectorParam(computeShader, HDShaderIDs._MotionBlurParams1, vector5);
				cmd.SetComputeVectorParam(computeShader, HDShaderIDs._MotionBlurParams2, vector6);
				num4 = 16;
				num5 = 16;
				num7 = (camera.actualWidth + (num4 - 1)) / num4;
				num8 = (camera.actualHeight + (num5 - 1)) / num5;
				cmd.DispatchCompute(computeShader, num6, num7, num8, camera.viewCount);
			}
			this.m_Pool.Recycle(rthandle2);
			this.m_Pool.Recycle(rthandle3);
			this.m_Pool.Recycle(rthandle);
			if (this.m_MotionBlurSupportsScattering)
			{
				this.m_Pool.Recycle(rthandle4);
				this.m_Pool.Recycle(rthandle5);
			}
		}

		// Token: 0x060007A0 RID: 1952 RVA: 0x0003CC0C File Offset: 0x0003AE0C
		private void DoPaniniProjection(CommandBuffer cmd, HDCamera camera, RTHandle source, RTHandle destination)
		{
			float value = this.m_PaniniProjection.distance.value;
			Vector2 vector = this.CalcViewExtents(camera);
			Vector2 vector2 = this.CalcCropExtents(camera, value);
			float num = vector2.x / vector.x;
			float num2 = vector2.y / vector.y;
			float num3 = Mathf.Min(num, num2);
			float num4 = value;
			float num5 = Mathf.Lerp(1f, Mathf.Clamp01(num3), this.m_PaniniProjection.cropToFit.value);
			ComputeShader paniniProjectionCS = this.m_Resources.shaders.paniniProjectionCS;
			int num6 = ((1f - Mathf.Abs(num4) > float.Epsilon) ? paniniProjectionCS.FindKernel("KMainGeneric") : paniniProjectionCS.FindKernel("KMainUnitDistance"));
			cmd.SetComputeVectorParam(paniniProjectionCS, HDShaderIDs._Params, new Vector4(vector.x, vector.y, num4, num5));
			cmd.SetComputeTextureParam(paniniProjectionCS, num6, HDShaderIDs._InputTexture, source);
			cmd.SetComputeTextureParam(paniniProjectionCS, num6, HDShaderIDs._OutputTexture, destination);
			cmd.DispatchCompute(paniniProjectionCS, num6, (camera.actualWidth + 7) / 8, (camera.actualHeight + 7) / 8, camera.viewCount);
		}

		// Token: 0x060007A1 RID: 1953 RVA: 0x0003CD38 File Offset: 0x0003AF38
		private Vector2 CalcViewExtents(HDCamera camera)
		{
			float num = camera.camera.fieldOfView * 0.017453292f;
			float num2 = (float)camera.actualWidth / (float)camera.actualHeight;
			float num3 = Mathf.Tan(0.5f * num);
			return new Vector2(num2 * num3, num3);
		}

		// Token: 0x060007A2 RID: 1954 RVA: 0x0003CD7C File Offset: 0x0003AF7C
		private Vector2 CalcCropExtents(HDCamera camera, float d)
		{
			float num = 1f + d;
			Vector2 vector = this.CalcViewExtents(camera);
			float num2 = Mathf.Sqrt(vector.x * vector.x + 1f);
			float num3 = 1f / num2;
			float num4 = num3 + d;
			return vector * num3 * (num / num4);
		}

		// Token: 0x060007A3 RID: 1955 RVA: 0x0003CDD0 File Offset: 0x0003AFD0
		private unsafe void DoBloom(CommandBuffer cmd, HDCamera camera, RTHandle source, ComputeShader uberCS, int uberKernel)
		{
			PostProcessSystem.<>c__DisplayClass103_0 CS$<>8__locals1;
			CS$<>8__locals1.source = source;
			CS$<>8__locals1.cmd = cmd;
			CS$<>8__locals1.camera = camera;
			BloomResolution resolution = this.m_Bloom.resolution;
			bool flag = this.m_Bloom.highQualityFiltering;
			float num = 1f / ((float)resolution / 2f);
			float num2 = 1f / ((float)resolution / 2f);
			if (CS$<>8__locals1.camera.actualWidth < 800 || CS$<>8__locals1.camera.actualHeight < 450)
			{
				num = 1f;
				num2 = 1f;
				flag = false;
			}
			if (this.m_Bloom.anamorphic.value)
			{
				float num3 = this.m_PhysicalCamera.anamorphism * 0.5f;
				num *= ((num3 < 0f) ? (1f + num3) : 1f);
				num2 *= ((num3 > 0f) ? (1f - num3) : 1f);
			}
			int num4 = Mathf.Clamp(Mathf.FloorToInt(Mathf.Log((float)Mathf.Max(CS$<>8__locals1.camera.actualWidth, CS$<>8__locals1.camera.actualHeight), 2f) - 2f - (float)((resolution == BloomResolution.Half) ? 0 : 1)), 1, 16);
			Vector2Int* ptr;
			float num5;
			checked
			{
				ptr = stackalloc Vector2Int[unchecked((UIntPtr)num4) * (UIntPtr)sizeof(Vector2Int)];
				num5 = Mathf.GammaToLinearSpace(this.m_Bloom.threshold.value);
			}
			float num6 = num5 * 0.5f + 1E-05f;
			Vector4 vector = new Vector4(num5, num5 - num6, num6 * 2f, 0.25f / num6);
			for (int i = 0; i < num4; i++)
			{
				float num7 = 1f / Mathf.Pow(2f, (float)i + 1f);
				float num8 = num * num7;
				float num9 = num2 * num7;
				int num10;
				int num11;
				if (DynamicResolutionHandler.instance.HardwareDynamicResIsEnabled())
				{
					num10 = Mathf.Max(1, Mathf.CeilToInt(num8 * (float)CS$<>8__locals1.camera.actualWidth));
					num11 = Mathf.Max(1, Mathf.CeilToInt(num9 * (float)CS$<>8__locals1.camera.actualHeight));
				}
				else
				{
					num10 = Mathf.Max(1, Mathf.RoundToInt(num8 * (float)CS$<>8__locals1.camera.actualWidth));
					num11 = Mathf.Max(1, Mathf.RoundToInt(num9 * (float)CS$<>8__locals1.camera.actualHeight));
				}
				Vector2 vector2 = new Vector2(num8, num9);
				Vector2Int vector2Int = new Vector2Int(num10, num11);
				ptr[i] = vector2Int;
				this.m_BloomMipsDown[i] = this.m_Pool.Get(in vector2, this.m_ColorFormat, false);
				this.m_BloomMipsUp[i] = this.m_Pool.Get(in vector2, this.m_ColorFormat, false);
			}
			Vector2Int vector2Int2 = *ptr;
			ComputeShader computeShader = this.m_Resources.shaders.bloomPrefilterCS;
			int num12 = computeShader.FindKernel("KMain");
			CS$<>8__locals1.cmd.SetComputeTextureParam(computeShader, num12, HDShaderIDs._InputTexture, CS$<>8__locals1.source);
			CS$<>8__locals1.cmd.SetComputeTextureParam(computeShader, num12, HDShaderIDs._OutputTexture, this.m_BloomMipsUp[0]);
			CS$<>8__locals1.cmd.SetComputeVectorParam(computeShader, HDShaderIDs._TexelSize, new Vector4((float)vector2Int2.x, (float)vector2Int2.y, 1f / (float)vector2Int2.x, 1f / (float)vector2Int2.y));
			CS$<>8__locals1.cmd.SetComputeVectorParam(computeShader, HDShaderIDs._BloomThreshold, vector);
			PostProcessSystem.<DoBloom>g__DispatchWithGuardBands|103_0(computeShader, num12, in vector2Int2, ref CS$<>8__locals1);
			computeShader = this.m_Resources.shaders.bloomBlurCS;
			num12 = computeShader.FindKernel("KMain");
			CS$<>8__locals1.cmd.SetComputeTextureParam(computeShader, num12, HDShaderIDs._InputTexture, this.m_BloomMipsUp[0]);
			CS$<>8__locals1.cmd.SetComputeTextureParam(computeShader, num12, HDShaderIDs._OutputTexture, this.m_BloomMipsDown[0]);
			CS$<>8__locals1.cmd.SetComputeVectorParam(computeShader, HDShaderIDs._TexelSize, new Vector4((float)vector2Int2.x, (float)vector2Int2.y, 1f / (float)vector2Int2.x, 1f / (float)vector2Int2.y));
			PostProcessSystem.<DoBloom>g__DispatchWithGuardBands|103_0(computeShader, num12, in vector2Int2, ref CS$<>8__locals1);
			num12 = computeShader.FindKernel("KMainDownsample");
			for (int j = 0; j < num4 - 1; j++)
			{
				RTHandle rthandle = this.m_BloomMipsDown[j];
				RTHandle rthandle2 = this.m_BloomMipsDown[j + 1];
				Vector2Int vector2Int3 = ptr[j + 1];
				CS$<>8__locals1.cmd.SetComputeTextureParam(computeShader, num12, HDShaderIDs._InputTexture, rthandle);
				CS$<>8__locals1.cmd.SetComputeTextureParam(computeShader, num12, HDShaderIDs._OutputTexture, rthandle2);
				CS$<>8__locals1.cmd.SetComputeVectorParam(computeShader, HDShaderIDs._TexelSize, new Vector4((float)vector2Int3.x, (float)vector2Int3.y, 1f / (float)vector2Int3.x, 1f / (float)vector2Int3.y));
				PostProcessSystem.<DoBloom>g__DispatchWithGuardBands|103_0(computeShader, num12, in vector2Int3, ref CS$<>8__locals1);
			}
			computeShader = this.m_Resources.shaders.bloomUpsampleCS;
			num12 = computeShader.FindKernel(flag ? "KMainHighQ" : "KMainLowQ");
			float num13 = Mathf.Lerp(0.05f, 0.95f, this.m_Bloom.scatter.value);
			for (int k = num4 - 2; k >= 0; k--)
			{
				RTHandle rthandle3 = ((k == num4 - 2) ? this.m_BloomMipsDown : this.m_BloomMipsUp)[k + 1];
				RTHandle rthandle4 = this.m_BloomMipsDown[k];
				RTHandle rthandle5 = this.m_BloomMipsUp[k];
				Vector2Int vector2Int4 = ptr[k];
				Vector2Int vector2Int5 = ptr[k + 1];
				CS$<>8__locals1.cmd.SetComputeTextureParam(computeShader, num12, HDShaderIDs._InputLowTexture, rthandle3);
				CS$<>8__locals1.cmd.SetComputeTextureParam(computeShader, num12, HDShaderIDs._InputHighTexture, rthandle4);
				CS$<>8__locals1.cmd.SetComputeTextureParam(computeShader, num12, HDShaderIDs._OutputTexture, rthandle5);
				CS$<>8__locals1.cmd.SetComputeVectorParam(computeShader, HDShaderIDs._Params, new Vector4(num13, 0f, 0f, 0f));
				CS$<>8__locals1.cmd.SetComputeVectorParam(computeShader, HDShaderIDs._BloomBicubicParams, new Vector4((float)vector2Int5.x, (float)vector2Int5.y, 1f / (float)vector2Int5.x, 1f / (float)vector2Int5.y));
				CS$<>8__locals1.cmd.SetComputeVectorParam(computeShader, HDShaderIDs._TexelSize, new Vector4((float)vector2Int4.x, (float)vector2Int4.y, 1f / (float)vector2Int4.x, 1f / (float)vector2Int4.y));
				PostProcessSystem.<DoBloom>g__DispatchWithGuardBands|103_0(computeShader, num12, in vector2Int4, ref CS$<>8__locals1);
			}
			for (int l = 0; l < num4; l++)
			{
				this.m_Pool.Recycle(this.m_BloomMipsDown[l]);
				if (l > 0)
				{
					this.m_Pool.Recycle(this.m_BloomMipsUp[l]);
				}
			}
			Vector2Int vector2Int6 = *ptr;
			this.m_BloomTexture = this.m_BloomMipsUp[0];
			float num14 = Mathf.Pow(2f, this.m_Bloom.intensity.value) - 1f;
			Color color = this.m_Bloom.tint.value.linear;
			float num15 = ColorUtils.Luminance(in color);
			color = ((num15 > 0f) ? (color * (1f / num15)) : Color.white);
			Texture texture = ((this.m_Bloom.dirtTexture.value == null) ? Texture2D.blackTexture : this.m_Bloom.dirtTexture.value);
			int num16 = ((this.m_Bloom.dirtTexture.value != null && this.m_Bloom.dirtIntensity.value > 0f) ? 1 : 0);
			float num17 = (float)texture.width / (float)texture.height;
			float num18 = (float)CS$<>8__locals1.camera.actualWidth / (float)CS$<>8__locals1.camera.actualHeight;
			Vector4 vector3 = new Vector4(1f, 1f, 0f, 0f);
			float num19 = this.m_Bloom.dirtIntensity.value * num14;
			if (num17 > num18)
			{
				vector3.x = num18 / num17;
				vector3.z = (1f - vector3.x) * 0.5f;
			}
			else if (num18 > num17)
			{
				vector3.y = num17 / num18;
				vector3.w = (1f - vector3.y) * 0.5f;
			}
			CS$<>8__locals1.cmd.SetComputeTextureParam(uberCS, uberKernel, HDShaderIDs._BloomTexture, this.m_BloomTexture);
			CS$<>8__locals1.cmd.SetComputeTextureParam(uberCS, uberKernel, HDShaderIDs._BloomDirtTexture, texture);
			CS$<>8__locals1.cmd.SetComputeVectorParam(uberCS, HDShaderIDs._BloomParams, new Vector4(num14, num19, 1f, (float)num16));
			CS$<>8__locals1.cmd.SetComputeVectorParam(uberCS, HDShaderIDs._BloomTint, color);
			CS$<>8__locals1.cmd.SetComputeVectorParam(uberCS, HDShaderIDs._BloomBicubicParams, new Vector4((float)vector2Int6.x, (float)vector2Int6.y, 1f / (float)vector2Int6.x, 1f / (float)vector2Int6.y));
			CS$<>8__locals1.cmd.SetComputeVectorParam(uberCS, HDShaderIDs._BloomDirtScaleOffset, vector3);
			CS$<>8__locals1.cmd.SetComputeVectorParam(uberCS, HDShaderIDs._BloomThreshold, vector);
		}

		// Token: 0x060007A4 RID: 1956 RVA: 0x0003D758 File Offset: 0x0003B958
		private void DoLensDistortion(CommandBuffer cmd, ComputeShader cs, int kernel, UberPostFeatureFlags flags)
		{
			if ((flags & UberPostFeatureFlags.LensDistortion) != UberPostFeatureFlags.LensDistortion)
			{
				return;
			}
			float num = 1.6f * Mathf.Max(Mathf.Abs(this.m_LensDistortion.intensity.value * 100f), 1f);
			float num2 = 0.017453292f * Mathf.Min(160f, num);
			float num3 = 2f * Mathf.Tan(num2 * 0.5f);
			Vector2 vector = this.m_LensDistortion.center.value * 2f - Vector2.one;
			Vector4 vector2 = new Vector4(vector.x, vector.y, Mathf.Max(this.m_LensDistortion.xMultiplier.value, 0.0001f), Mathf.Max(this.m_LensDistortion.yMultiplier.value, 0.0001f));
			Vector4 vector3 = new Vector4((this.m_LensDistortion.intensity.value >= 0f) ? num2 : (1f / num2), num3, 1f / this.m_LensDistortion.scale.value, this.m_LensDistortion.intensity.value * 100f);
			cmd.SetComputeVectorParam(cs, HDShaderIDs._DistortionParams1, vector2);
			cmd.SetComputeVectorParam(cs, HDShaderIDs._DistortionParams2, vector3);
		}

		// Token: 0x060007A5 RID: 1957 RVA: 0x0003D89C File Offset: 0x0003BA9C
		private void DoChromaticAberration(CommandBuffer cmd, ComputeShader cs, int kernel, UberPostFeatureFlags flags)
		{
			if ((flags & UberPostFeatureFlags.ChromaticAberration) != UberPostFeatureFlags.ChromaticAberration)
			{
				return;
			}
			Texture texture = this.m_ChromaticAberration.spectralLut.value;
			if (texture == null)
			{
				if (this.m_InternalSpectralLut == null)
				{
					this.m_InternalSpectralLut = new Texture2D(3, 1, TextureFormat.RGB24, false)
					{
						name = "Chromatic Aberration Spectral LUT",
						filterMode = FilterMode.Bilinear,
						wrapMode = TextureWrapMode.Clamp,
						anisoLevel = 0,
						hideFlags = HideFlags.DontSave
					};
					this.m_InternalSpectralLut.SetPixels(new Color[]
					{
						new Color(1f, 0f, 0f),
						new Color(0f, 1f, 0f),
						new Color(0f, 0f, 1f)
					});
					this.m_InternalSpectralLut.Apply();
				}
				texture = this.m_InternalSpectralLut;
			}
			Vector4 vector = new Vector4(this.m_ChromaticAberration.intensity.value * 0.05f, (float)this.m_ChromaticAberration.maxSamples, 0f, 0f);
			cmd.SetComputeTextureParam(cs, kernel, HDShaderIDs._ChromaSpectralLut, texture);
			cmd.SetComputeVectorParam(cs, HDShaderIDs._ChromaParams, vector);
		}

		// Token: 0x060007A6 RID: 1958 RVA: 0x0003D9E0 File Offset: 0x0003BBE0
		private void DoVignette(CommandBuffer cmd, ComputeShader cs, int kernel, UberPostFeatureFlags flags)
		{
			if ((flags & UberPostFeatureFlags.Vignette) != UberPostFeatureFlags.Vignette)
			{
				return;
			}
			if (this.m_Vignette.mode.value == VignetteMode.Procedural)
			{
				float num = (1f - this.m_Vignette.roundness.value) * 6f + this.m_Vignette.roundness.value;
				cmd.SetComputeVectorParam(cs, HDShaderIDs._VignetteParams1, new Vector4(this.m_Vignette.center.value.x, this.m_Vignette.center.value.y, 0f, 0f));
				cmd.SetComputeVectorParam(cs, HDShaderIDs._VignetteParams2, new Vector4(this.m_Vignette.intensity.value * 3f, this.m_Vignette.smoothness.value * 5f, num, this.m_Vignette.rounded.value ? 1f : 0f));
				cmd.SetComputeVectorParam(cs, HDShaderIDs._VignetteColor, this.m_Vignette.color.value);
				cmd.SetComputeTextureParam(cs, kernel, HDShaderIDs._VignetteMask, Texture2D.blackTexture);
				return;
			}
			Color value = this.m_Vignette.color.value;
			value.a = Mathf.Clamp01(this.m_Vignette.opacity.value);
			cmd.SetComputeVectorParam(cs, HDShaderIDs._VignetteParams1, new Vector4(0f, 0f, 1f, 0f));
			cmd.SetComputeVectorParam(cs, HDShaderIDs._VignetteColor, value);
			cmd.SetComputeTextureParam(cs, kernel, HDShaderIDs._VignetteMask, this.m_Vignette.mask.value);
		}

		// Token: 0x060007A7 RID: 1959 RVA: 0x0003DB98 File Offset: 0x0003BD98
		private void DoColorGrading(CommandBuffer cmd, ComputeShader cs, int kernel)
		{
			Vector3 colorBalanceCoeffs = PostProcessSystem.GetColorBalanceCoeffs(this.m_WhiteBalance.temperature.value, this.m_WhiteBalance.tint.value);
			Vector4 vector = new Vector4(this.m_ColorAdjustments.hueShift.value / 360f, this.m_ColorAdjustments.saturation.value / 100f + 1f, this.m_ColorAdjustments.contrast.value / 100f + 1f, 0f);
			Vector4 vector2 = new Vector4(this.m_ChannelMixer.redOutRedIn.value / 100f, this.m_ChannelMixer.redOutGreenIn.value / 100f, this.m_ChannelMixer.redOutBlueIn.value / 100f, 0f);
			Vector4 vector3 = new Vector4(this.m_ChannelMixer.greenOutRedIn.value / 100f, this.m_ChannelMixer.greenOutGreenIn.value / 100f, this.m_ChannelMixer.greenOutBlueIn.value / 100f, 0f);
			Vector4 vector4 = new Vector4(this.m_ChannelMixer.blueOutRedIn.value / 100f, this.m_ChannelMixer.blueOutGreenIn.value / 100f, this.m_ChannelMixer.blueOutBlueIn.value / 100f, 0f);
			Vector4 vector5;
			Vector4 vector6;
			Vector4 vector7;
			Vector4 vector8;
			this.ComputeShadowsMidtonesHighlights(out vector5, out vector6, out vector7, out vector8);
			Vector4 vector9;
			Vector4 vector10;
			Vector4 vector11;
			this.ComputeLiftGammaGain(out vector9, out vector10, out vector11);
			Vector4 vector12;
			Vector4 vector13;
			this.ComputeSplitToning(out vector12, out vector13);
			TonemappingMode tonemappingMode = (this.m_TonemappingFS ? this.m_Tonemapping.mode.value : TonemappingMode.None);
			ComputeShader lutBuilder3DCS = this.m_Resources.shaders.lutBuilder3DCS;
			string text = "KBuild_NoTonemap";
			if (this.m_Tonemapping.IsActive())
			{
				switch (tonemappingMode)
				{
				case TonemappingMode.Neutral:
					text = "KBuild_NeutralTonemap";
					break;
				case TonemappingMode.ACES:
					text = "KBuild_AcesTonemap";
					break;
				case TonemappingMode.Custom:
					text = "KBuild_CustomTonemap";
					break;
				case TonemappingMode.External:
					text = "KBuild_ExternalTonemap";
					break;
				}
			}
			int num = lutBuilder3DCS.FindKernel(text);
			cmd.SetComputeTextureParam(lutBuilder3DCS, num, HDShaderIDs._OutputTexture, this.m_InternalLogLut);
			cmd.SetComputeVectorParam(lutBuilder3DCS, HDShaderIDs._Size, new Vector4((float)this.m_LutSize, 1f / ((float)this.m_LutSize - 1f), 0f, 0f));
			cmd.SetComputeVectorParam(lutBuilder3DCS, HDShaderIDs._ColorBalance, colorBalanceCoeffs);
			cmd.SetComputeVectorParam(lutBuilder3DCS, HDShaderIDs._ColorFilter, this.m_ColorAdjustments.colorFilter.value.linear);
			cmd.SetComputeVectorParam(lutBuilder3DCS, HDShaderIDs._ChannelMixerRed, vector2);
			cmd.SetComputeVectorParam(lutBuilder3DCS, HDShaderIDs._ChannelMixerGreen, vector3);
			cmd.SetComputeVectorParam(lutBuilder3DCS, HDShaderIDs._ChannelMixerBlue, vector4);
			cmd.SetComputeVectorParam(lutBuilder3DCS, HDShaderIDs._HueSatCon, vector);
			cmd.SetComputeVectorParam(lutBuilder3DCS, HDShaderIDs._Lift, vector9);
			cmd.SetComputeVectorParam(lutBuilder3DCS, HDShaderIDs._Gamma, vector10);
			cmd.SetComputeVectorParam(lutBuilder3DCS, HDShaderIDs._Gain, vector11);
			cmd.SetComputeVectorParam(lutBuilder3DCS, HDShaderIDs._Shadows, vector5);
			cmd.SetComputeVectorParam(lutBuilder3DCS, HDShaderIDs._Midtones, vector6);
			cmd.SetComputeVectorParam(lutBuilder3DCS, HDShaderIDs._Highlights, vector7);
			cmd.SetComputeVectorParam(lutBuilder3DCS, HDShaderIDs._ShaHiLimits, vector8);
			cmd.SetComputeVectorParam(lutBuilder3DCS, HDShaderIDs._SplitShadows, vector12);
			cmd.SetComputeVectorParam(lutBuilder3DCS, HDShaderIDs._SplitHighlights, vector13);
			cmd.SetComputeTextureParam(lutBuilder3DCS, num, HDShaderIDs._CurveMaster, this.m_Curves.master.value.GetTexture());
			cmd.SetComputeTextureParam(lutBuilder3DCS, num, HDShaderIDs._CurveRed, this.m_Curves.red.value.GetTexture());
			cmd.SetComputeTextureParam(lutBuilder3DCS, num, HDShaderIDs._CurveGreen, this.m_Curves.green.value.GetTexture());
			cmd.SetComputeTextureParam(lutBuilder3DCS, num, HDShaderIDs._CurveBlue, this.m_Curves.blue.value.GetTexture());
			cmd.SetComputeTextureParam(lutBuilder3DCS, num, HDShaderIDs._CurveHueVsHue, this.m_Curves.hueVsHue.value.GetTexture());
			cmd.SetComputeTextureParam(lutBuilder3DCS, num, HDShaderIDs._CurveHueVsSat, this.m_Curves.hueVsSat.value.GetTexture());
			cmd.SetComputeTextureParam(lutBuilder3DCS, num, HDShaderIDs._CurveLumVsSat, this.m_Curves.lumVsSat.value.GetTexture());
			cmd.SetComputeTextureParam(lutBuilder3DCS, num, HDShaderIDs._CurveSatVsSat, this.m_Curves.satVsSat.value.GetTexture());
			if (tonemappingMode == TonemappingMode.Custom)
			{
				this.m_HableCurve.Init(this.m_Tonemapping.toeStrength.value, this.m_Tonemapping.toeLength.value, this.m_Tonemapping.shoulderStrength.value, this.m_Tonemapping.shoulderLength.value, this.m_Tonemapping.shoulderAngle.value, this.m_Tonemapping.gamma.value);
				cmd.SetComputeVectorParam(lutBuilder3DCS, HDShaderIDs._CustomToneCurve, this.m_HableCurve.uniforms.curve);
				cmd.SetComputeVectorParam(lutBuilder3DCS, HDShaderIDs._ToeSegmentA, this.m_HableCurve.uniforms.toeSegmentA);
				cmd.SetComputeVectorParam(lutBuilder3DCS, HDShaderIDs._ToeSegmentB, this.m_HableCurve.uniforms.toeSegmentB);
				cmd.SetComputeVectorParam(lutBuilder3DCS, HDShaderIDs._MidSegmentA, this.m_HableCurve.uniforms.midSegmentA);
				cmd.SetComputeVectorParam(lutBuilder3DCS, HDShaderIDs._MidSegmentB, this.m_HableCurve.uniforms.midSegmentB);
				cmd.SetComputeVectorParam(lutBuilder3DCS, HDShaderIDs._ShoSegmentA, this.m_HableCurve.uniforms.shoSegmentA);
				cmd.SetComputeVectorParam(lutBuilder3DCS, HDShaderIDs._ShoSegmentB, this.m_HableCurve.uniforms.shoSegmentB);
			}
			else if (tonemappingMode == TonemappingMode.External)
			{
				cmd.SetComputeTextureParam(lutBuilder3DCS, num, HDShaderIDs._LogLut3D, this.m_Tonemapping.lutTexture.value);
				cmd.SetComputeVectorParam(lutBuilder3DCS, HDShaderIDs._LogLut3D_Params, new Vector4(1f / (float)this.m_LutSize, (float)this.m_LutSize - 1f, this.m_Tonemapping.lutContribution.value, 0f));
			}
			cmd.SetComputeVectorParam(lutBuilder3DCS, HDShaderIDs._Params, new Vector4(this.m_ColorGradingFS ? 1f : 0f, 0f, 0f, 0f));
			uint num2;
			uint num3;
			uint num4;
			lutBuilder3DCS.GetKernelThreadGroupSizes(num, out num2, out num3, out num4);
			cmd.DispatchCompute(lutBuilder3DCS, num, (int)(((long)this.m_LutSize + (long)((ulong)num2) - 1L) / (long)((ulong)num2)), (int)(((long)this.m_LutSize + (long)((ulong)num3) - 1L) / (long)((ulong)num3)), (int)(((long)this.m_LutSize + (long)((ulong)num4) - 1L) / (long)((ulong)num4)));
			float num5 = Mathf.Pow(2f, this.m_ColorAdjustments.postExposure.value);
			Vector4 vector14 = new Vector4(1f / (float)this.m_LutSize, (float)this.m_LutSize - 1f, num5, 0f);
			cmd.SetComputeTextureParam(cs, kernel, HDShaderIDs._LogLut3D, this.m_InternalLogLut);
			cmd.SetComputeVectorParam(cs, HDShaderIDs._LogLut3D_Params, vector14);
		}

		// Token: 0x060007A8 RID: 1960 RVA: 0x0003E2F0 File Offset: 0x0003C4F0
		public static Vector3 GetColorBalanceCoeffs(float temperature, float tint)
		{
			float num = temperature / 65f;
			float num2 = tint / 65f;
			float num3 = 0.31271f - num * ((num < 0f) ? 0.1f : 0.05f);
			float num4 = ColorUtils.StandardIlluminantY(num3) + num2 * 0.05f;
			Vector3 vector = new Vector3(0.949237f, 1.03542f, 1.08728f);
			Vector3 vector2 = ColorUtils.CIExyToLMS(num3, num4);
			return new Vector3(vector.x / vector2.x, vector.y / vector2.y, vector.z / vector2.z);
		}

		// Token: 0x060007A9 RID: 1961 RVA: 0x0003E388 File Offset: 0x0003C588
		private void ComputeShadowsMidtonesHighlights(out Vector4 shadows, out Vector4 midtones, out Vector4 highlights, out Vector4 limits)
		{
			shadows = this.m_ShadowsMidtonesHighlights.shadows.value;
			shadows.x = Mathf.GammaToLinearSpace(shadows.x);
			shadows.y = Mathf.GammaToLinearSpace(shadows.y);
			shadows.z = Mathf.GammaToLinearSpace(shadows.z);
			float num = shadows.w * ((Mathf.Sign(shadows.w) < 0f) ? 1f : 4f);
			shadows.x = Mathf.Max(shadows.x + num, 0f);
			shadows.y = Mathf.Max(shadows.y + num, 0f);
			shadows.z = Mathf.Max(shadows.z + num, 0f);
			shadows.w = 0f;
			midtones = this.m_ShadowsMidtonesHighlights.midtones.value;
			midtones.x = Mathf.GammaToLinearSpace(midtones.x);
			midtones.y = Mathf.GammaToLinearSpace(midtones.y);
			midtones.z = Mathf.GammaToLinearSpace(midtones.z);
			num = midtones.w * ((Mathf.Sign(midtones.w) < 0f) ? 1f : 4f);
			midtones.x = Mathf.Max(midtones.x + num, 0f);
			midtones.y = Mathf.Max(midtones.y + num, 0f);
			midtones.z = Mathf.Max(midtones.z + num, 0f);
			midtones.w = 0f;
			highlights = this.m_ShadowsMidtonesHighlights.highlights.value;
			highlights.x = Mathf.GammaToLinearSpace(highlights.x);
			highlights.y = Mathf.GammaToLinearSpace(highlights.y);
			highlights.z = Mathf.GammaToLinearSpace(highlights.z);
			num = highlights.w * ((Mathf.Sign(highlights.w) < 0f) ? 1f : 4f);
			highlights.x = Mathf.Max(highlights.x + num, 0f);
			highlights.y = Mathf.Max(highlights.y + num, 0f);
			highlights.z = Mathf.Max(highlights.z + num, 0f);
			highlights.w = 0f;
			limits = new Vector4(this.m_ShadowsMidtonesHighlights.shadowsStart.value, this.m_ShadowsMidtonesHighlights.shadowsEnd.value, this.m_ShadowsMidtonesHighlights.highlightsStart.value, this.m_ShadowsMidtonesHighlights.highlightsEnd.value);
		}

		// Token: 0x060007AA RID: 1962 RVA: 0x0003E628 File Offset: 0x0003C828
		private void ComputeLiftGammaGain(out Vector4 lift, out Vector4 gamma, out Vector4 gain)
		{
			lift = this.m_LiftGammaGain.lift.value;
			lift.x = Mathf.GammaToLinearSpace(lift.x) * 0.15f;
			lift.y = Mathf.GammaToLinearSpace(lift.y) * 0.15f;
			lift.z = Mathf.GammaToLinearSpace(lift.z) * 0.15f;
			Color color = lift;
			float num = ColorUtils.Luminance(in color);
			lift.x = lift.x - num + lift.w;
			lift.y = lift.y - num + lift.w;
			lift.z = lift.z - num + lift.w;
			lift.w = 0f;
			gamma = this.m_LiftGammaGain.gamma.value;
			gamma.x = Mathf.GammaToLinearSpace(gamma.x) * 0.8f;
			gamma.y = Mathf.GammaToLinearSpace(gamma.y) * 0.8f;
			gamma.z = Mathf.GammaToLinearSpace(gamma.z) * 0.8f;
			color = gamma;
			float num2 = ColorUtils.Luminance(in color);
			gamma.w += 1f;
			gamma.x = 1f / Mathf.Max(gamma.x - num2 + gamma.w, 0.001f);
			gamma.y = 1f / Mathf.Max(gamma.y - num2 + gamma.w, 0.001f);
			gamma.z = 1f / Mathf.Max(gamma.z - num2 + gamma.w, 0.001f);
			gamma.w = 0f;
			gain = this.m_LiftGammaGain.gain.value;
			gain.x = Mathf.GammaToLinearSpace(gain.x) * 0.8f;
			gain.y = Mathf.GammaToLinearSpace(gain.y) * 0.8f;
			gain.z = Mathf.GammaToLinearSpace(gain.z) * 0.8f;
			color = gain;
			float num3 = ColorUtils.Luminance(in color);
			gain.w += 1f;
			gain.x = gain.x - num3 + gain.w;
			gain.y = gain.y - num3 + gain.w;
			gain.z = gain.z - num3 + gain.w;
			gain.w = 0f;
		}

		// Token: 0x060007AB RID: 1963 RVA: 0x0003E8B0 File Offset: 0x0003CAB0
		private void ComputeSplitToning(out Vector4 shadows, out Vector4 highlights)
		{
			shadows = this.m_SplitToning.shadows.value;
			highlights = this.m_SplitToning.highlights.value;
			shadows.w = this.m_SplitToning.balance.value / 100f;
			highlights.w = 0f;
		}

		// Token: 0x060007AC RID: 1964 RVA: 0x0003E91C File Offset: 0x0003CB1C
		private void DoFXAA(CommandBuffer cmd, HDCamera camera, RTHandle source, RTHandle destination)
		{
			ComputeShader fxaacs = this.m_Resources.shaders.FXAACS;
			int num = fxaacs.FindKernel("FXAA");
			cmd.SetComputeTextureParam(fxaacs, num, HDShaderIDs._InputTexture, source);
			cmd.SetComputeTextureParam(fxaacs, num, HDShaderIDs._OutputTexture, destination);
			cmd.DispatchCompute(fxaacs, num, (camera.actualWidth + 7) / 8, (camera.actualHeight + 7) / 8, camera.viewCount);
		}

		// Token: 0x060007AD RID: 1965 RVA: 0x0003E990 File Offset: 0x0003CB90
		private void DoSMAA(CommandBuffer cmd, HDCamera camera, RTHandle source, RTHandle destination, RTHandle depthBuffer)
		{
			PostProcessSystem.TargetPool pool = this.m_Pool;
			Vector2 vector = Vector2.one;
			RTHandle rthandle = pool.Get(in vector, GraphicsFormat.R8G8B8A8_UNorm, false);
			PostProcessSystem.TargetPool pool2 = this.m_Pool;
			vector = Vector2.one;
			RTHandle rthandle2 = pool2.Get(in vector, GraphicsFormat.R8G8B8A8_UNorm, false);
			this.m_SMAAMaterial.SetVector(HDShaderIDs._SMAARTMetrics, new Vector4(1f / (float)camera.actualWidth, 1f / (float)camera.actualHeight, (float)camera.actualWidth, (float)camera.actualHeight));
			this.m_SMAAMaterial.SetTexture(HDShaderIDs._SMAAAreaTex, this.m_Resources.textures.SMAAAreaTex);
			this.m_SMAAMaterial.SetTexture(HDShaderIDs._SMAASearchTex, this.m_Resources.textures.SMAASearchTex);
			this.m_SMAAMaterial.SetInt(HDShaderIDs._StencilRef, 4);
			this.m_SMAAMaterial.SetInt(HDShaderIDs._StencilMask, 4);
			switch (camera.SMAAQuality)
			{
			case HDAdditionalCameraData.SMAAQualityLevel.Low:
				this.m_SMAAMaterial.EnableKeyword("SMAA_PRESET_LOW");
				break;
			case HDAdditionalCameraData.SMAAQualityLevel.Medium:
				this.m_SMAAMaterial.EnableKeyword("SMAA_PRESET_MEDIUM");
				break;
			case HDAdditionalCameraData.SMAAQualityLevel.High:
				this.m_SMAAMaterial.EnableKeyword("SMAA_PRESET_HIGH");
				break;
			default:
				this.m_SMAAMaterial.EnableKeyword("SMAA_PRESET_HIGH");
				break;
			}
			CoreUtils.SetRenderTarget(cmd, rthandle, ClearFlag.Color, 0, CubemapFace.Unknown, -1);
			CoreUtils.SetRenderTarget(cmd, rthandle2, ClearFlag.Color, 0, CubemapFace.Unknown, -1);
			cmd.SetGlobalTexture(HDShaderIDs._InputTexture, source);
			HDUtils.DrawFullScreen(cmd, this.m_SMAAMaterial, rthandle, depthBuffer, null, 0);
			cmd.SetGlobalTexture(HDShaderIDs._InputTexture, rthandle);
			HDUtils.DrawFullScreen(cmd, this.m_SMAAMaterial, rthandle2, depthBuffer, null, 1);
			cmd.SetGlobalTexture(HDShaderIDs._InputTexture, source);
			this.m_SMAAMaterial.SetTexture(HDShaderIDs._SMAABlendTex, rthandle2);
			HDUtils.DrawFullScreen(cmd, this.m_SMAAMaterial, destination, null, 2);
			this.m_Pool.Recycle(rthandle);
			this.m_Pool.Recycle(rthandle2);
		}

		// Token: 0x060007AE RID: 1966 RVA: 0x0003EB74 File Offset: 0x0003CD74
		private void DoFinalPass(CommandBuffer cmd, HDCamera camera, BlueNoise blueNoise, RTHandle source, RTHandle afterPostProcessTexture, RenderTargetIdentifier destination, bool flipY)
		{
			this.m_FinalPassMaterial.shaderKeywords = null;
			this.m_FinalPassMaterial.SetTexture(HDShaderIDs._InputTexture, source);
			DynamicResolutionHandler instance = DynamicResolutionHandler.instance;
			bool flag = camera.isMainGameView && instance.DynamicResolutionEnabled();
			if (flag)
			{
				switch (instance.filter)
				{
				case DynamicResUpscaleFilter.Bilinear:
					this.m_FinalPassMaterial.EnableKeyword("BILINEAR");
					break;
				case DynamicResUpscaleFilter.CatmullRom:
					this.m_FinalPassMaterial.EnableKeyword("CATMULL_ROM_4");
					break;
				case DynamicResUpscaleFilter.Lanczos:
					this.m_FinalPassMaterial.EnableKeyword("LANCZOS");
					break;
				case DynamicResUpscaleFilter.ContrastAdaptiveSharpen:
					this.m_FinalPassMaterial.EnableKeyword("CONTRASTADAPTIVESHARPEN");
					break;
				}
			}
			if (this.m_PostProcessEnabled)
			{
				if (camera.antialiasing == HDAdditionalCameraData.AntialiasingMode.FastApproximateAntialiasing && !flag && this.m_AntialiasingFS)
				{
					this.m_FinalPassMaterial.EnableKeyword("FXAA");
				}
				if (this.m_FilmGrain.IsActive() && this.m_FilmGrainFS)
				{
					Texture texture = this.m_FilmGrain.texture.value;
					if (this.m_FilmGrain.type.value != FilmGrainLookup.Custom)
					{
						texture = this.m_Resources.textures.filmGrainTex[(int)this.m_FilmGrain.type.value];
					}
					if (texture != null)
					{
						int num = (int)(this.m_Random.NextDouble() * (double)texture.width);
						int num2 = (int)(this.m_Random.NextDouble() * (double)texture.height);
						this.m_FinalPassMaterial.EnableKeyword("GRAIN");
						this.m_FinalPassMaterial.SetTexture(HDShaderIDs._GrainTexture, texture);
						this.m_FinalPassMaterial.SetVector(HDShaderIDs._GrainParams, new Vector2(this.m_FilmGrain.intensity.value * 4f, this.m_FilmGrain.response.value));
						this.m_FinalPassMaterial.SetVector(HDShaderIDs._GrainTextureParams, new Vector4((float)texture.width, (float)texture.height, (float)num, (float)num2));
					}
				}
				if (camera.dithering && this.m_DitheringFS)
				{
					Texture2DArray textureArray16L = blueNoise.textureArray16L;
					int num3 = Time.frameCount % textureArray16L.depth;
					this.m_FinalPassMaterial.EnableKeyword("DITHER");
					this.m_FinalPassMaterial.SetTexture(HDShaderIDs._BlueNoiseTexture, textureArray16L);
					this.m_FinalPassMaterial.SetVector(HDShaderIDs._DitherParams, new Vector3((float)textureArray16L.width, (float)textureArray16L.height, (float)num3));
				}
			}
			if (this.m_KeepAlpha)
			{
				this.m_FinalPassMaterial.SetTexture(HDShaderIDs._AlphaTexture, this.m_AlphaTexture);
				this.m_FinalPassMaterial.SetFloat(HDShaderIDs._KeepAlpha, 1f);
			}
			else
			{
				this.m_FinalPassMaterial.SetTexture(HDShaderIDs._AlphaTexture, TextureXR.GetWhiteTexture());
				this.m_FinalPassMaterial.SetFloat(HDShaderIDs._KeepAlpha, 0f);
			}
			if (this.m_EnableAlpha)
			{
				this.m_FinalPassMaterial.EnableKeyword("ENABLE_ALPHA");
			}
			this.m_FinalPassMaterial.SetVector(HDShaderIDs._UVTransform, flipY ? new Vector4(1f, -1f, 0f, 1f) : new Vector4(1f, 1f, 0f, 0f));
			Rect finalViewport = camera.finalViewport;
			if (!HDUtils.PostProcessIsFinalPass())
			{
				if (instance.HardwareDynamicResIsEnabled())
				{
					Vector2Int lastScaledSize = instance.GetLastScaledSize();
					finalViewport.width = (float)lastScaledSize.x;
					finalViewport.height = (float)lastScaledSize.y;
				}
				finalViewport.x = (finalViewport.y = 0f);
			}
			if (camera.frameSettings.IsEnabled(FrameSettingsField.AfterPostprocess))
			{
				this.m_FinalPassMaterial.EnableKeyword("APPLY_AFTER_POST");
				this.m_FinalPassMaterial.SetTexture(HDShaderIDs._AfterPostProcessTexture, afterPostProcessTexture);
			}
			else
			{
				this.m_FinalPassMaterial.SetTexture(HDShaderIDs._AfterPostProcessTexture, TextureXR.GetBlackTexture());
			}
			HDUtils.DrawFullScreen(cmd, finalViewport, this.m_FinalPassMaterial, destination, null, 0, -1);
		}

		// Token: 0x060007AF RID: 1967 RVA: 0x0003EF7C File Offset: 0x0003D17C
		internal void DoUserAfterOpaqueAndSky(CommandBuffer cmd, HDCamera camera, RTHandle colorBuffer)
		{
			if (!camera.frameSettings.IsEnabled(FrameSettingsField.CustomPostProcess))
			{
				return;
			}
			RTHandle rthandle = colorBuffer;
			using (new ProfilingScope(cmd, ProfilingSampler.Get<HDProfileId>(HDProfileId.CustomPostProcessAfterOpaqueAndSky)))
			{
				bool flag = false;
				foreach (string text in HDRenderPipeline.defaultAsset.beforeTransparentCustomPostProcesses)
				{
					flag |= this.RenderCustomPostProcess(cmd, camera, ref rthandle, colorBuffer, Type.GetType(text));
				}
				if (flag)
				{
					Rect finalViewport = camera.finalViewport;
					HDUtils.BlitCameraTexture(cmd, rthandle, colorBuffer, 0f, false);
				}
			}
			this.PoolSourceGuard(ref rthandle, null, colorBuffer);
		}

		// Token: 0x060007B0 RID: 1968 RVA: 0x0003F04C File Offset: 0x0003D24C
		private bool RenderCustomPostProcess(CommandBuffer cmd, HDCamera camera, ref RTHandle source, RTHandle colorBuffer, Type customPostProcessComponentType)
		{
			if (customPostProcessComponentType == null)
			{
				return false;
			}
			CustomPostProcessVolumeComponent customPostProcessVolumeComponent;
			if ((customPostProcessVolumeComponent = camera.volumeStack.GetComponent(customPostProcessComponentType) as CustomPostProcessVolumeComponent) != null)
			{
				customPostProcessVolumeComponent.SetupIfNeeded();
				IPostProcessComponent postProcessComponent;
				if ((postProcessComponent = customPostProcessVolumeComponent as IPostProcessComponent) != null && postProcessComponent.IsActive() && (camera.camera.cameraType != CameraType.SceneView || customPostProcessVolumeComponent.visibleInSceneView))
				{
					PostProcessSystem.TargetPool pool = this.m_Pool;
					Vector2 one = Vector2.one;
					RTHandle rthandle = pool.Get(in one, this.m_ColorFormat, false);
					CoreUtils.SetRenderTarget(cmd, rthandle, ClearFlag.None, 0, CubemapFace.Unknown, -1);
					cmd.BeginSample(customPostProcessVolumeComponent.name);
					customPostProcessVolumeComponent.Render(cmd, camera, source, rthandle);
					cmd.EndSample(customPostProcessVolumeComponent.name);
					this.PoolSourceGuard(ref source, rthandle, colorBuffer);
					return true;
				}
			}
			return false;
		}

		// Token: 0x060007B2 RID: 1970 RVA: 0x0003F10E File Offset: 0x0003D30E
		[CompilerGenerated]
		private void <Render>g__PoolSource|81_0(ref RTHandle src, RTHandle dst, ref PostProcessSystem.<>c__DisplayClass81_0 A_3)
		{
			this.PoolSourceGuard(ref src, dst, A_3.colorBuffer);
		}

		// Token: 0x060007B3 RID: 1971 RVA: 0x0003F120 File Offset: 0x0003D320
		[CompilerGenerated]
		private RTHandle <GrabTemporalAntialiasingHistoryTextures>g__Allocator|96_0(string id, int frameIndex, RTHandleSystem rtHandleSystem)
		{
			Vector2 one = Vector2.one;
			int slices = TextureXR.slices;
			DepthBits depthBits = DepthBits.None;
			TextureDimension dimension = TextureXR.dimension;
			return rtHandleSystem.Alloc(one, slices, depthBits, this.m_ColorFormat, FilterMode.Bilinear, TextureWrapMode.Repeat, dimension, true, false, true, false, 1, 0f, false, false, true, RenderTextureMemoryless.None, "TAA History");
		}

		// Token: 0x060007B4 RID: 1972 RVA: 0x0003F160 File Offset: 0x0003D360
		[CompilerGenerated]
		internal static void <DoBloom>g__DispatchWithGuardBands|103_0(ComputeShader shader, int kernelId, in Vector2Int size, ref PostProcessSystem.<>c__DisplayClass103_0 A_3)
		{
			Vector2Int vector2Int = size;
			int num = vector2Int.x;
			vector2Int = size;
			int num2 = vector2Int.y;
			if (num < A_3.source.rt.width && num % 8 < 4)
			{
				num += 4;
			}
			if (num2 < A_3.source.rt.height && num2 % 8 < 4)
			{
				num2 += 4;
			}
			A_3.cmd.DispatchCompute(shader, kernelId, (num + 7) / 8, (num2 + 7) / 8, A_3.camera.viewCount);
		}

		// Token: 0x04000808 RID: 2056
		private GraphicsFormat m_ColorFormat = GraphicsFormat.B10G11R11_UFloatPack32;

		// Token: 0x04000809 RID: 2057
		private const GraphicsFormat k_CoCFormat = GraphicsFormat.R16_SFloat;

		// Token: 0x0400080A RID: 2058
		private const GraphicsFormat k_ExposureFormat = GraphicsFormat.R32G32_SFloat;

		// Token: 0x0400080B RID: 2059
		private readonly RenderPipelineResources m_Resources;

		// Token: 0x0400080C RID: 2060
		private Material m_FinalPassMaterial;

		// Token: 0x0400080D RID: 2061
		private Material m_ClearBlackMaterial;

		// Token: 0x0400080E RID: 2062
		private Material m_SMAAMaterial;

		// Token: 0x0400080F RID: 2063
		private Material m_TemporalAAMaterial;

		// Token: 0x04000810 RID: 2064
		private MaterialPropertyBlock m_TAAHistoryBlitPropertyBlock = new MaterialPropertyBlock();

		// Token: 0x04000811 RID: 2065
		private MaterialPropertyBlock m_TAAPropertyBlock = new MaterialPropertyBlock();

		// Token: 0x04000812 RID: 2066
		private const int k_ExposureCurvePrecision = 128;

		// Token: 0x04000813 RID: 2067
		private readonly Color[] m_ExposureCurveColorArray = new Color[128];

		// Token: 0x04000814 RID: 2068
		private readonly int[] m_ExposureVariants = new int[4];

		// Token: 0x04000815 RID: 2069
		private Texture2D m_ExposureCurveTexture;

		// Token: 0x04000816 RID: 2070
		private RTHandle m_EmptyExposureTexture;

		// Token: 0x04000817 RID: 2071
		private ComputeBuffer m_BokehNearKernel;

		// Token: 0x04000818 RID: 2072
		private ComputeBuffer m_BokehFarKernel;

		// Token: 0x04000819 RID: 2073
		private ComputeBuffer m_BokehIndirectCmd;

		// Token: 0x0400081A RID: 2074
		private ComputeBuffer m_NearBokehTileList;

		// Token: 0x0400081B RID: 2075
		private ComputeBuffer m_FarBokehTileList;

		// Token: 0x0400081C RID: 2076
		private ComputeBuffer m_ContrastAdaptiveSharpen;

		// Token: 0x0400081D RID: 2077
		private const int k_MaxBloomMipCount = 16;

		// Token: 0x0400081E RID: 2078
		private readonly RTHandle[] m_BloomMipsDown = new RTHandle[17];

		// Token: 0x0400081F RID: 2079
		private readonly RTHandle[] m_BloomMipsUp = new RTHandle[17];

		// Token: 0x04000820 RID: 2080
		private RTHandle m_BloomTexture;

		// Token: 0x04000821 RID: 2081
		private Texture2D m_InternalSpectralLut;

		// Token: 0x04000822 RID: 2082
		private readonly int m_LutSize;

		// Token: 0x04000823 RID: 2083
		private RTHandle m_InternalLogLut;

		// Token: 0x04000824 RID: 2084
		private readonly HableCurve m_HableCurve;

		// Token: 0x04000825 RID: 2085
		private Exposure m_Exposure;

		// Token: 0x04000826 RID: 2086
		private DepthOfField m_DepthOfField;

		// Token: 0x04000827 RID: 2087
		private MotionBlur m_MotionBlur;

		// Token: 0x04000828 RID: 2088
		private PaniniProjection m_PaniniProjection;

		// Token: 0x04000829 RID: 2089
		private Bloom m_Bloom;

		// Token: 0x0400082A RID: 2090
		private ChromaticAberration m_ChromaticAberration;

		// Token: 0x0400082B RID: 2091
		private LensDistortion m_LensDistortion;

		// Token: 0x0400082C RID: 2092
		private Vignette m_Vignette;

		// Token: 0x0400082D RID: 2093
		private Tonemapping m_Tonemapping;

		// Token: 0x0400082E RID: 2094
		private WhiteBalance m_WhiteBalance;

		// Token: 0x0400082F RID: 2095
		private ColorAdjustments m_ColorAdjustments;

		// Token: 0x04000830 RID: 2096
		private ChannelMixer m_ChannelMixer;

		// Token: 0x04000831 RID: 2097
		private SplitToning m_SplitToning;

		// Token: 0x04000832 RID: 2098
		private LiftGammaGain m_LiftGammaGain;

		// Token: 0x04000833 RID: 2099
		private ShadowsMidtonesHighlights m_ShadowsMidtonesHighlights;

		// Token: 0x04000834 RID: 2100
		private ColorCurves m_Curves;

		// Token: 0x04000835 RID: 2101
		private FilmGrain m_FilmGrain;

		// Token: 0x04000836 RID: 2102
		private bool m_ExposureControlFS;

		// Token: 0x04000837 RID: 2103
		private bool m_StopNaNFS;

		// Token: 0x04000838 RID: 2104
		private bool m_DepthOfFieldFS;

		// Token: 0x04000839 RID: 2105
		private bool m_MotionBlurFS;

		// Token: 0x0400083A RID: 2106
		private bool m_PaniniProjectionFS;

		// Token: 0x0400083B RID: 2107
		private bool m_BloomFS;

		// Token: 0x0400083C RID: 2108
		private bool m_ChromaticAberrationFS;

		// Token: 0x0400083D RID: 2109
		private bool m_LensDistortionFS;

		// Token: 0x0400083E RID: 2110
		private bool m_VignetteFS;

		// Token: 0x0400083F RID: 2111
		private bool m_ColorGradingFS;

		// Token: 0x04000840 RID: 2112
		private bool m_TonemappingFS;

		// Token: 0x04000841 RID: 2113
		private bool m_FilmGrainFS;

		// Token: 0x04000842 RID: 2114
		private bool m_DitheringFS;

		// Token: 0x04000843 RID: 2115
		private bool m_AntialiasingFS;

		// Token: 0x04000844 RID: 2116
		private HDPhysicalCamera m_PhysicalCamera;

		// Token: 0x04000845 RID: 2117
		private static readonly HDPhysicalCamera m_DefaultPhysicalCamera = new HDPhysicalCamera();

		// Token: 0x04000846 RID: 2118
		private RTHandle m_TempTexture1024;

		// Token: 0x04000847 RID: 2119
		private RTHandle m_TempTexture32;

		// Token: 0x04000848 RID: 2120
		private readonly bool m_EnableAlpha;

		// Token: 0x04000849 RID: 2121
		private readonly bool m_KeepAlpha;

		// Token: 0x0400084A RID: 2122
		private RTHandle m_AlphaTexture;

		// Token: 0x0400084B RID: 2123
		private readonly PostProcessSystem.TargetPool m_Pool;

		// Token: 0x0400084C RID: 2124
		private readonly bool m_UseSafePath;

		// Token: 0x0400084D RID: 2125
		private bool m_PostProcessEnabled;

		// Token: 0x0400084E RID: 2126
		private bool m_AnimatedMaterialsEnabled;

		// Token: 0x0400084F RID: 2127
		private bool m_MotionBlurSupportsScattering;

		// Token: 0x04000850 RID: 2128
		private const int k_RTGuardBandSize = 4;

		// Token: 0x04000851 RID: 2129
		private readonly Dictionary<int, string> m_UberPostFeatureMap = new Dictionary<int, string>();

		// Token: 0x04000852 RID: 2130
		private readonly Random m_Random;

		// Token: 0x04000853 RID: 2131
		private HDRenderPipeline m_HDInstance;

		// Token: 0x02000253 RID: 595
		private enum SMAAStage
		{
			// Token: 0x04001589 RID: 5513
			EdgeDetection,
			// Token: 0x0400158A RID: 5514
			BlendWeights,
			// Token: 0x0400158B RID: 5515
			NeighborhoodBlending
		}

		// Token: 0x02000254 RID: 596
		private class TargetPool
		{
			// Token: 0x06000C42 RID: 3138 RVA: 0x00058F71 File Offset: 0x00057171
			public TargetPool()
			{
				this.m_Targets = new Dictionary<int, Stack<RTHandle>>();
				this.m_Tracker = 0;
				this.m_HasHWDynamicResolution = false;
			}

			// Token: 0x06000C43 RID: 3139 RVA: 0x00058F94 File Offset: 0x00057194
			public void Cleanup()
			{
				foreach (KeyValuePair<int, Stack<RTHandle>> keyValuePair in this.m_Targets)
				{
					Stack<RTHandle> value = keyValuePair.Value;
					if (value != null)
					{
						while (value.Count > 0)
						{
							RTHandles.Release(value.Pop());
						}
					}
				}
				this.m_Targets.Clear();
			}

			// Token: 0x06000C44 RID: 3140 RVA: 0x0005900C File Offset: 0x0005720C
			public void SetHWDynamicResolutionState(HDCamera camera)
			{
				bool flag = DynamicResolutionHandler.instance.HardwareDynamicResIsEnabled();
				if (this.m_Targets.Count > 0 && flag != this.m_HasHWDynamicResolution)
				{
					bool flag2 = false;
					foreach (KeyValuePair<int, Stack<RTHandle>> keyValuePair in this.m_Targets)
					{
						Stack<RTHandle> value = keyValuePair.Value;
						if (value != null && value.Count > 0 && value.Peek().rt.useDynamicScale != flag)
						{
							flag2 = true;
							break;
						}
					}
					if (flag2)
					{
						this.Cleanup();
					}
					this.m_HasHWDynamicResolution = flag;
				}
			}

			// Token: 0x06000C45 RID: 3141 RVA: 0x000590BC File Offset: 0x000572BC
			public RTHandle Get(in Vector2 scaleFactor, GraphicsFormat format, bool mipmap = false)
			{
				int num = this.ComputeHashCode(scaleFactor.x, scaleFactor.y, (int)format, mipmap);
				Stack<RTHandle> stack;
				if (this.m_Targets.TryGetValue(num, out stack) && stack.Count > 0)
				{
					return stack.Pop();
				}
				RTHandle rthandle = RTHandles.Alloc(scaleFactor, TextureXR.slices, DepthBits.None, format, FilterMode.Point, TextureWrapMode.Repeat, TextureXR.dimension, true, mipmap, true, false, 1, 0f, false, false, true, RenderTextureMemoryless.None, "Post-processing Target Pool " + this.m_Tracker);
				this.m_Tracker++;
				return rthandle;
			}

			// Token: 0x06000C46 RID: 3142 RVA: 0x00059148 File Offset: 0x00057348
			public void Recycle(RTHandle rt)
			{
				int num = this.ComputeHashCode(rt.scaleFactor.x, rt.scaleFactor.y, (int)rt.rt.graphicsFormat, rt.rt.useMipMap);
				Stack<RTHandle> stack;
				if (!this.m_Targets.TryGetValue(num, out stack))
				{
					stack = new Stack<RTHandle>();
					this.m_Targets.Add(num, stack);
				}
				stack.Push(rt);
			}

			// Token: 0x06000C47 RID: 3143 RVA: 0x000591B2 File Offset: 0x000573B2
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			private unsafe int ComputeHashCode(float scaleX, float scaleY, int format, bool mipmap)
			{
				return (((17 * 23 + *(int*)(&scaleX)) * 23 + *(int*)(&scaleY)) * 23 + format) * 23 + (mipmap ? 1 : 0);
			}

			// Token: 0x0400158C RID: 5516
			private readonly Dictionary<int, Stack<RTHandle>> m_Targets;

			// Token: 0x0400158D RID: 5517
			private int m_Tracker;

			// Token: 0x0400158E RID: 5518
			private bool m_HasHWDynamicResolution;
		}
	}
}
