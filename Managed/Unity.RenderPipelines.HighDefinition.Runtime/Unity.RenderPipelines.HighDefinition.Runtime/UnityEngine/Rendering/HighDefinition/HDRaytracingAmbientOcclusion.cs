using System;
using UnityEngine.Experimental.Rendering;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x0200010D RID: 269
	internal class HDRaytracingAmbientOcclusion
	{
		// Token: 0x06000879 RID: 2169 RVA: 0x00045930 File Offset: 0x00043B30
		public void Init(HDRenderPipeline renderPipeline)
		{
			this.m_PipelineSettings = renderPipeline.currentPlatformRenderPipelineSettings;
			this.m_PipelineResources = renderPipeline.asset.renderPipelineResources;
			this.m_PipelineRayTracingResources = renderPipeline.asset.renderPipelineRayTracingResources;
			this.m_RenderPipeline = renderPipeline;
			this.m_AOIntermediateBuffer0 = RTHandles.Alloc(Vector2.one, TextureXR.slices, DepthBits.None, GraphicsFormat.R16G16B16A16_SFloat, FilterMode.Point, TextureWrapMode.Repeat, TextureXR.dimension, true, false, false, false, 1, 0f, false, false, true, RenderTextureMemoryless.None, "AOIntermediateBuffer0");
			this.m_AOIntermediateBuffer1 = RTHandles.Alloc(Vector2.one, TextureXR.slices, DepthBits.None, GraphicsFormat.R16G16B16A16_SFloat, FilterMode.Point, TextureWrapMode.Repeat, TextureXR.dimension, true, false, false, false, 1, 0f, false, false, true, RenderTextureMemoryless.None, "AOIntermediateBuffer1");
		}

		// Token: 0x0600087A RID: 2170 RVA: 0x000459D6 File Offset: 0x00043BD6
		public void Release()
		{
			RTHandles.Release(this.m_AOIntermediateBuffer1);
			RTHandles.Release(this.m_AOIntermediateBuffer0);
		}

		// Token: 0x0600087B RID: 2171 RVA: 0x000459F0 File Offset: 0x00043BF0
		private static RTHandle AmbientOcclusionHistoryBufferAllocatorFunction(string viewName, int frameIndex, RTHandleSystem rtHandleSystem)
		{
			return rtHandleSystem.Alloc(Vector2.one, TextureXR.slices, DepthBits.None, GraphicsFormat.R16G16_SFloat, FilterMode.Point, TextureWrapMode.Repeat, TextureXR.dimension, true, false, false, false, 1, 0f, false, false, false, RenderTextureMemoryless.None, string.Format("AmbientOcclusionHistoryBuffer{0}", frameIndex));
		}

		// Token: 0x0600087C RID: 2172 RVA: 0x00045A35 File Offset: 0x00043C35
		public void SetDefaultAmbientOcclusionTexture(CommandBuffer cmd)
		{
			cmd.SetGlobalTexture(HDShaderIDs._AmbientOcclusionTexture, TextureXR.GetBlackTexture());
			cmd.SetGlobalVector(HDShaderIDs._AmbientOcclusionParam, Vector4.zero);
		}

		// Token: 0x0600087D RID: 2173 RVA: 0x00045A5C File Offset: 0x00043C5C
		public void RenderAO(HDCamera hdCamera, CommandBuffer cmd, RTHandle outputTexture, ScriptableRenderContext renderContext, int frameCount)
		{
			if (!this.m_RenderPipeline.GetRayTracingState())
			{
				this.SetDefaultAmbientOcclusionTexture(cmd);
				return;
			}
			RayTracingShader aoRaytracing = this.m_PipelineRayTracingResources.aoRaytracing;
			AmbientOcclusion component = hdCamera.volumeStack.GetComponent<AmbientOcclusion>();
			RayTracingSettings component2 = hdCamera.volumeStack.GetComponent<RayTracingSettings>();
			using (new ProfilingScope(cmd, ProfilingSampler.Get<HDProfileId>(HDProfileId.RaytracingAmbientOcclusion)))
			{
				RayTracingAccelerationStructure rayTracingAccelerationStructure = this.m_RenderPipeline.RequestAccelerationStructure();
				cmd.SetRayTracingShaderPass(aoRaytracing, "VisibilityDXR");
				cmd.SetRayTracingAccelerationStructure(aoRaytracing, HDShaderIDs._RaytracingAccelerationStructureName, rayTracingAccelerationStructure);
				cmd.SetRayTracingFloatParams(aoRaytracing, HDShaderIDs._RaytracingRayBias, new float[] { component2.rayBias.value });
				cmd.SetRayTracingFloatParams(aoRaytracing, HDShaderIDs._RaytracingRayMaxLength, new float[] { component.rayLength.value });
				cmd.SetRayTracingIntParams(aoRaytracing, HDShaderIDs._RaytracingNumSamples, new int[] { component.sampleCount.value });
				cmd.SetRayTracingTextureParam(aoRaytracing, HDShaderIDs._DepthTexture, this.m_RenderPipeline.sharedRTManager.GetDepthStencilBuffer(false));
				cmd.SetRayTracingTextureParam(aoRaytracing, HDShaderIDs._NormalBufferTexture, this.m_RenderPipeline.sharedRTManager.GetNormalBuffer(false));
				int num = this.m_RenderPipeline.RayTracingFrameIndex(hdCamera);
				cmd.SetRayTracingIntParam(aoRaytracing, HDShaderIDs._RaytracingFrameIndex, num);
				this.m_RenderPipeline.GetBlueNoiseManager().BindDitheredRNGData8SPP(cmd);
				cmd.SetRayTracingFloatParam(aoRaytracing, HDShaderIDs._RaytracingAOIntensity, component.intensity.value);
				RayCountManager rayCountManager = this.m_RenderPipeline.GetRayCountManager();
				cmd.SetRayTracingIntParam(aoRaytracing, HDShaderIDs._RayCountEnabled, rayCountManager.RayCountIsEnabled());
				cmd.SetRayTracingTextureParam(aoRaytracing, HDShaderIDs._RayCountTexture, rayCountManager.GetRayCountTexture());
				cmd.SetRayTracingTextureParam(aoRaytracing, HDShaderIDs._AmbientOcclusionTextureRW, this.m_AOIntermediateBuffer0);
				cmd.DispatchRays(aoRaytracing, "RayGenAmbientOcclusion", (uint)hdCamera.actualWidth, (uint)hdCamera.actualHeight, (uint)hdCamera.viewCount, null);
			}
			using (new ProfilingScope(cmd, ProfilingSampler.Get<HDProfileId>(HDProfileId.RaytracingFilterAmbientOcclusion)))
			{
				if (component.denoise.value)
				{
					RTHandle rthandle = hdCamera.GetCurrentFrameRT(8) ?? hdCamera.AllocHistoryFrameRT(8, new Func<string, int, RTHandleSystem, RTHandle>(HDRaytracingAmbientOcclusion.AmbientOcclusionHistoryBufferAllocatorFunction), 1);
					float num2 = (this.m_RenderPipeline.ValidRayTracingHistory(hdCamera) ? 1f : 0f);
					this.m_RenderPipeline.GetTemporalFilter().DenoiseBuffer(cmd, hdCamera, this.m_AOIntermediateBuffer0, rthandle, this.m_AOIntermediateBuffer1, true, num2);
					this.m_RenderPipeline.GetDiffuseDenoiser().DenoiseBuffer(cmd, hdCamera, this.m_AOIntermediateBuffer1, outputTexture, component.denoiserRadius.value, true, false);
				}
				else
				{
					HDUtils.BlitCameraTexture(cmd, this.m_AOIntermediateBuffer0, outputTexture, 0f, false);
				}
			}
			cmd.SetGlobalTexture(HDShaderIDs._AmbientOcclusionTexture, outputTexture);
			cmd.SetGlobalVector(HDShaderIDs._AmbientOcclusionParam, new Vector4(0f, 0f, 0f, hdCamera.volumeStack.GetComponent<AmbientOcclusion>().directLightingStrength.value));
			(RenderPipelineManager.currentPipeline as HDRenderPipeline).PushFullScreenDebugTexture(hdCamera, cmd, outputTexture, FullScreenDebugMode.SSAO);
		}

		// Token: 0x04000D17 RID: 3351
		private RenderPipelineResources m_PipelineResources;

		// Token: 0x04000D18 RID: 3352
		private HDRenderPipelineRayTracingResources m_PipelineRayTracingResources;

		// Token: 0x04000D19 RID: 3353
		private RenderPipelineSettings m_PipelineSettings;

		// Token: 0x04000D1A RID: 3354
		private HDRenderPipeline m_RenderPipeline;

		// Token: 0x04000D1B RID: 3355
		private static int m_KernelFilter;

		// Token: 0x04000D1C RID: 3356
		private RTHandle m_AOIntermediateBuffer0;

		// Token: 0x04000D1D RID: 3357
		private RTHandle m_AOIntermediateBuffer1;

		// Token: 0x04000D1E RID: 3358
		private const string m_RayGenShaderName = "RayGenAmbientOcclusion";

		// Token: 0x04000D1F RID: 3359
		private const string m_MissShaderName = "MissShaderAmbientOcclusion";

		// Token: 0x04000D20 RID: 3360
		private const string m_ClosestHitShaderName = "ClosestHitMain";
	}
}
