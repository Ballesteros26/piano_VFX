using System;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x02000114 RID: 276
	internal class HDReflectionDenoiser
	{
		// Token: 0x0600089C RID: 2204 RVA: 0x00047954 File Offset: 0x00045B54
		public void Init(HDRenderPipelineRayTracingResources rpRTResources, SharedRTManager sharedRTManager, HDRenderPipeline renderPipeline)
		{
			this.m_ReflectionDenoiserCS = rpRTResources.reflectionDenoiserCS;
			this.m_ReflectionFilterMapping = rpRTResources.reflectionFilterMapping;
			this.m_SharedRTManager = sharedRTManager;
			this.m_RenderPipeline = renderPipeline;
			HDReflectionDenoiser.s_TemporalAccumulationKernel = this.m_ReflectionDenoiserCS.FindKernel("TemporalAccumulation");
			HDReflectionDenoiser.s_CopyHistoryKernel = this.m_ReflectionDenoiserCS.FindKernel("CopyHistory");
			HDReflectionDenoiser.s_BilateralFilterHKernel = this.m_ReflectionDenoiserCS.FindKernel("BilateralFilterH");
			HDReflectionDenoiser.s_BilateralFilterVKernel = this.m_ReflectionDenoiserCS.FindKernel("BilateralFilterV");
		}

		// Token: 0x0600089D RID: 2205 RVA: 0x00002646 File Offset: 0x00000846
		public void Release()
		{
		}

		// Token: 0x0600089E RID: 2206 RVA: 0x000479DC File Offset: 0x00045BDC
		public void DenoiseBuffer(CommandBuffer cmd, HDCamera hdCamera, int maxKernelSize, RTHandle noisySignal, RTHandle historySignal, RTHandle outputSignal, float historyValidity = 1f)
		{
			int actualWidth = hdCamera.actualWidth;
			int actualHeight = hdCamera.actualHeight;
			int num = 8;
			int num2 = (actualWidth + (num - 1)) / num;
			int num3 = (actualHeight + (num - 1)) / num;
			ScreenSpaceReflection component = hdCamera.volumeStack.GetComponent<ScreenSpaceReflection>();
			RTHandle rayTracingBuffer = this.m_RenderPipeline.GetRayTracingBuffer(InternalRayTracingBuffers.RGBA0);
			RTHandle rayTracingBuffer2 = this.m_RenderPipeline.GetRayTracingBuffer(InternalRayTracingBuffers.RGBA1);
			Vector2 vector = new Vector2((float)hdCamera.actualWidth / (float)historySignal.rt.width, (float)hdCamera.actualHeight / (float)historySignal.rt.height);
			cmd.SetComputeVectorParam(this.m_ReflectionDenoiserCS, HDShaderIDs._RTHandleScaleHistory, vector);
			cmd.SetComputeTextureParam(this.m_ReflectionDenoiserCS, HDReflectionDenoiser.s_TemporalAccumulationKernel, HDShaderIDs._DenoiseInputTexture, noisySignal);
			cmd.SetComputeTextureParam(this.m_ReflectionDenoiserCS, HDReflectionDenoiser.s_TemporalAccumulationKernel, HDShaderIDs._HistoryBuffer, historySignal);
			cmd.SetComputeTextureParam(this.m_ReflectionDenoiserCS, HDReflectionDenoiser.s_TemporalAccumulationKernel, HDShaderIDs._DepthTexture, this.m_SharedRTManager.GetDepthStencilBuffer(false));
			cmd.SetComputeTextureParam(this.m_ReflectionDenoiserCS, HDReflectionDenoiser.s_TemporalAccumulationKernel, HDShaderIDs._DenoiseOutputTextureRW, rayTracingBuffer);
			cmd.SetComputeFloatParam(this.m_ReflectionDenoiserCS, HDShaderIDs._HistoryValidity, historyValidity);
			cmd.DispatchCompute(this.m_ReflectionDenoiserCS, HDReflectionDenoiser.s_TemporalAccumulationKernel, num2, num3, hdCamera.viewCount);
			cmd.SetComputeTextureParam(this.m_ReflectionDenoiserCS, HDReflectionDenoiser.s_CopyHistoryKernel, HDShaderIDs._DenoiseInputTexture, rayTracingBuffer);
			cmd.SetComputeTextureParam(this.m_ReflectionDenoiserCS, HDReflectionDenoiser.s_CopyHistoryKernel, HDShaderIDs._DenoiseOutputTextureRW, historySignal);
			cmd.DispatchCompute(this.m_ReflectionDenoiserCS, HDReflectionDenoiser.s_CopyHistoryKernel, num2, num3, hdCamera.viewCount);
			cmd.SetComputeIntParam(this.m_ReflectionDenoiserCS, HDShaderIDs._DenoiserFilterRadius, maxKernelSize);
			cmd.SetComputeTextureParam(this.m_ReflectionDenoiserCS, HDReflectionDenoiser.s_BilateralFilterHKernel, HDShaderIDs._DenoiseInputTexture, rayTracingBuffer);
			cmd.SetComputeTextureParam(this.m_ReflectionDenoiserCS, HDReflectionDenoiser.s_BilateralFilterHKernel, HDShaderIDs._DepthTexture, this.m_SharedRTManager.GetDepthStencilBuffer(false));
			cmd.SetComputeTextureParam(this.m_ReflectionDenoiserCS, HDReflectionDenoiser.s_BilateralFilterHKernel, HDShaderIDs._NormalBufferTexture, this.m_SharedRTManager.GetNormalBuffer(false));
			cmd.SetComputeTextureParam(this.m_ReflectionDenoiserCS, HDReflectionDenoiser.s_BilateralFilterHKernel, HDShaderIDs._DenoiseOutputTextureRW, rayTracingBuffer2);
			cmd.SetComputeTextureParam(this.m_ReflectionDenoiserCS, HDReflectionDenoiser.s_BilateralFilterHKernel, HDShaderIDs._ReflectionFilterMapping, this.m_ReflectionFilterMapping);
			cmd.SetComputeFloatParam(this.m_ReflectionDenoiserCS, HDShaderIDs._RaytracingReflectionMinSmoothness, component.minSmoothness.value);
			cmd.DispatchCompute(this.m_ReflectionDenoiserCS, HDReflectionDenoiser.s_BilateralFilterHKernel, num2, num3, hdCamera.viewCount);
			cmd.SetComputeIntParam(this.m_ReflectionDenoiserCS, HDShaderIDs._DenoiserFilterRadius, maxKernelSize);
			cmd.SetComputeTextureParam(this.m_ReflectionDenoiserCS, HDReflectionDenoiser.s_BilateralFilterVKernel, HDShaderIDs._DenoiseInputTexture, rayTracingBuffer2);
			cmd.SetComputeTextureParam(this.m_ReflectionDenoiserCS, HDReflectionDenoiser.s_BilateralFilterVKernel, HDShaderIDs._DepthTexture, this.m_SharedRTManager.GetDepthStencilBuffer(false));
			cmd.SetComputeTextureParam(this.m_ReflectionDenoiserCS, HDReflectionDenoiser.s_BilateralFilterVKernel, HDShaderIDs._NormalBufferTexture, this.m_SharedRTManager.GetNormalBuffer(false));
			cmd.SetComputeTextureParam(this.m_ReflectionDenoiserCS, HDReflectionDenoiser.s_BilateralFilterVKernel, HDShaderIDs._DenoiseOutputTextureRW, outputSignal);
			cmd.SetComputeTextureParam(this.m_ReflectionDenoiserCS, HDReflectionDenoiser.s_BilateralFilterVKernel, HDShaderIDs._ReflectionFilterMapping, this.m_ReflectionFilterMapping);
			cmd.SetComputeFloatParam(this.m_ReflectionDenoiserCS, HDShaderIDs._RaytracingReflectionMinSmoothness, component.minSmoothness.value);
			cmd.DispatchCompute(this.m_ReflectionDenoiserCS, HDReflectionDenoiser.s_BilateralFilterVKernel, num2, num3, hdCamera.viewCount);
		}

		// Token: 0x04000D6B RID: 3435
		private ComputeShader m_ReflectionDenoiserCS;

		// Token: 0x04000D6C RID: 3436
		private Texture2D m_ReflectionFilterMapping;

		// Token: 0x04000D6D RID: 3437
		private SharedRTManager m_SharedRTManager;

		// Token: 0x04000D6E RID: 3438
		private HDRenderPipeline m_RenderPipeline;

		// Token: 0x04000D6F RID: 3439
		private static int s_TemporalAccumulationKernel;

		// Token: 0x04000D70 RID: 3440
		private static int s_CopyHistoryKernel;

		// Token: 0x04000D71 RID: 3441
		private static int s_BilateralFilterHKernel;

		// Token: 0x04000D72 RID: 3442
		private static int s_BilateralFilterVKernel;
	}
}
