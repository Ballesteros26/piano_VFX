using System;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x02000115 RID: 277
	internal class HDSimpleDenoiser
	{
		// Token: 0x060008A0 RID: 2208 RVA: 0x00047D58 File Offset: 0x00045F58
		public void Init(HDRenderPipelineRayTracingResources rpRTResources, SharedRTManager sharedRTManager, HDRenderPipeline renderPipeline)
		{
			this.m_SimpleDenoiserCS = rpRTResources.simpleDenoiserCS;
			this.m_SharedRTManager = sharedRTManager;
			this.m_RenderPipeline = renderPipeline;
		}

		// Token: 0x060008A1 RID: 2209 RVA: 0x00002646 File Offset: 0x00000846
		public void Release()
		{
		}

		// Token: 0x060008A2 RID: 2210 RVA: 0x00047D74 File Offset: 0x00045F74
		public void DenoiseBuffer(CommandBuffer cmd, HDCamera hdCamera, RTHandle noisySignal, RTHandle historySignal, RTHandle outputSignal, int kernelSize, bool singleChannel = true, int slotIndex = -1)
		{
			int actualWidth = hdCamera.actualWidth;
			int actualHeight = hdCamera.actualHeight;
			int num = 8;
			int num2 = (actualWidth + (num - 1)) / num;
			int num3 = (actualHeight + (num - 1)) / num;
			int num4;
			if (singleChannel)
			{
				if (slotIndex < 0)
				{
					num4 = this.m_SimpleDenoiserCS.FindKernel("TemporalAccumulationSingle");
				}
				else
				{
					num4 = this.m_SimpleDenoiserCS.FindKernel("TemporalAccumulationSingleArray");
				}
			}
			else
			{
				num4 = this.m_SimpleDenoiserCS.FindKernel("TemporalAccumulationColor");
			}
			RTHandle rayTracingBuffer = this.m_RenderPipeline.GetRayTracingBuffer(InternalRayTracingBuffers.RGBA0);
			RTHandle rayTracingBuffer2 = this.m_RenderPipeline.GetRayTracingBuffer(InternalRayTracingBuffers.RGBA1);
			Vector2 vector = new Vector2((float)hdCamera.actualWidth / (float)historySignal.rt.width, (float)hdCamera.actualHeight / (float)historySignal.rt.height);
			cmd.SetComputeVectorParam(this.m_SimpleDenoiserCS, HDShaderIDs._RTHandleScaleHistory, vector);
			cmd.SetComputeTextureParam(this.m_SimpleDenoiserCS, num4, HDShaderIDs._DenoiseInputTexture, noisySignal);
			cmd.SetComputeTextureParam(this.m_SimpleDenoiserCS, num4, HDShaderIDs._HistoryBuffer, historySignal);
			cmd.SetComputeTextureParam(this.m_SimpleDenoiserCS, num4, HDShaderIDs._DepthTexture, this.m_SharedRTManager.GetDepthStencilBuffer(false));
			cmd.SetComputeTextureParam(this.m_SimpleDenoiserCS, num4, HDShaderIDs._DenoiseOutputTextureRW, rayTracingBuffer);
			cmd.SetComputeIntParam(this.m_SimpleDenoiserCS, HDShaderIDs._DenoisingHistorySlot, slotIndex);
			cmd.DispatchCompute(this.m_SimpleDenoiserCS, num4, num2, num3, hdCamera.viewCount);
			if (slotIndex < 0)
			{
				num4 = this.m_SimpleDenoiserCS.FindKernel(singleChannel ? "CopyHistorySingle" : "CopyHistoryColor");
			}
			else
			{
				num4 = this.m_SimpleDenoiserCS.FindKernel(singleChannel ? "CopyHistorySingleArray" : "CopyHistoryColorArray");
			}
			cmd.SetComputeTextureParam(this.m_SimpleDenoiserCS, num4, HDShaderIDs._DenoiseInputTexture, rayTracingBuffer);
			cmd.SetComputeTextureParam(this.m_SimpleDenoiserCS, num4, HDShaderIDs._DenoiseOutputTextureRW, historySignal);
			cmd.SetComputeIntParam(this.m_SimpleDenoiserCS, HDShaderIDs._DenoisingHistorySlot, slotIndex);
			cmd.DispatchCompute(this.m_SimpleDenoiserCS, num4, num2, num3, hdCamera.viewCount);
			num4 = this.m_SimpleDenoiserCS.FindKernel(singleChannel ? "BilateralFilterHSingle" : "BilateralFilterHColor");
			cmd.SetComputeIntParam(this.m_SimpleDenoiserCS, HDShaderIDs._DenoiserFilterRadius, kernelSize);
			cmd.SetComputeTextureParam(this.m_SimpleDenoiserCS, num4, HDShaderIDs._DenoiseInputTexture, rayTracingBuffer);
			cmd.SetComputeTextureParam(this.m_SimpleDenoiserCS, num4, HDShaderIDs._DepthTexture, this.m_SharedRTManager.GetDepthStencilBuffer(false));
			cmd.SetComputeTextureParam(this.m_SimpleDenoiserCS, num4, HDShaderIDs._NormalBufferTexture, this.m_SharedRTManager.GetNormalBuffer(false));
			cmd.SetComputeTextureParam(this.m_SimpleDenoiserCS, num4, HDShaderIDs._DenoiseOutputTextureRW, rayTracingBuffer2);
			cmd.DispatchCompute(this.m_SimpleDenoiserCS, num4, num2, num3, hdCamera.viewCount);
			num4 = this.m_SimpleDenoiserCS.FindKernel(singleChannel ? "BilateralFilterVSingle" : "BilateralFilterVColor");
			cmd.SetComputeIntParam(this.m_SimpleDenoiserCS, HDShaderIDs._DenoiserFilterRadius, kernelSize);
			cmd.SetComputeTextureParam(this.m_SimpleDenoiserCS, num4, HDShaderIDs._DenoiseInputTexture, rayTracingBuffer2);
			cmd.SetComputeTextureParam(this.m_SimpleDenoiserCS, num4, HDShaderIDs._DepthTexture, this.m_SharedRTManager.GetDepthStencilBuffer(false));
			cmd.SetComputeTextureParam(this.m_SimpleDenoiserCS, num4, HDShaderIDs._NormalBufferTexture, this.m_SharedRTManager.GetNormalBuffer(false));
			cmd.SetComputeTextureParam(this.m_SimpleDenoiserCS, num4, HDShaderIDs._DenoiseOutputTextureRW, outputSignal);
			cmd.DispatchCompute(this.m_SimpleDenoiserCS, num4, num2, num3, hdCamera.viewCount);
		}

		// Token: 0x060008A3 RID: 2211 RVA: 0x00048108 File Offset: 0x00046308
		public void DenoiseBufferNoHistory(CommandBuffer cmd, HDCamera hdCamera, RTHandle noisySignal, RTHandle outputSignal, int kernelSize, bool singleChannel = true)
		{
			int actualWidth = hdCamera.actualWidth;
			int actualHeight = hdCamera.actualHeight;
			int num = 8;
			int num2 = (actualWidth + (num - 1)) / num;
			int num3 = (actualHeight + (num - 1)) / num;
			RTHandle rayTracingBuffer = this.m_RenderPipeline.GetRayTracingBuffer(InternalRayTracingBuffers.RGBA0);
			int num4 = this.m_SimpleDenoiserCS.FindKernel(singleChannel ? "BilateralFilterHSingle" : "BilateralFilterHColor");
			cmd.SetComputeIntParam(this.m_SimpleDenoiserCS, HDShaderIDs._DenoiserFilterRadius, kernelSize);
			cmd.SetComputeTextureParam(this.m_SimpleDenoiserCS, num4, HDShaderIDs._DenoiseInputTexture, noisySignal);
			cmd.SetComputeTextureParam(this.m_SimpleDenoiserCS, num4, HDShaderIDs._DepthTexture, this.m_SharedRTManager.GetDepthStencilBuffer(false));
			cmd.SetComputeTextureParam(this.m_SimpleDenoiserCS, num4, HDShaderIDs._NormalBufferTexture, this.m_SharedRTManager.GetNormalBuffer(false));
			cmd.SetComputeTextureParam(this.m_SimpleDenoiserCS, num4, HDShaderIDs._DenoiseOutputTextureRW, rayTracingBuffer);
			cmd.DispatchCompute(this.m_SimpleDenoiserCS, num4, num2, num3, hdCamera.viewCount);
			num4 = this.m_SimpleDenoiserCS.FindKernel(singleChannel ? "BilateralFilterVSingle" : "BilateralFilterVColor");
			cmd.SetComputeIntParam(this.m_SimpleDenoiserCS, HDShaderIDs._DenoiserFilterRadius, kernelSize);
			cmd.SetComputeTextureParam(this.m_SimpleDenoiserCS, num4, HDShaderIDs._DenoiseInputTexture, rayTracingBuffer);
			cmd.SetComputeTextureParam(this.m_SimpleDenoiserCS, num4, HDShaderIDs._DepthTexture, this.m_SharedRTManager.GetDepthStencilBuffer(false));
			cmd.SetComputeTextureParam(this.m_SimpleDenoiserCS, num4, HDShaderIDs._NormalBufferTexture, this.m_SharedRTManager.GetNormalBuffer(false));
			cmd.SetComputeTextureParam(this.m_SimpleDenoiserCS, num4, HDShaderIDs._DenoiseOutputTextureRW, outputSignal);
			cmd.DispatchCompute(this.m_SimpleDenoiserCS, num4, num2, num3, hdCamera.viewCount);
		}

		// Token: 0x04000D73 RID: 3443
		private ComputeShader m_SimpleDenoiserCS;

		// Token: 0x04000D74 RID: 3444
		private SharedRTManager m_SharedRTManager;

		// Token: 0x04000D75 RID: 3445
		private HDRenderPipeline m_RenderPipeline;
	}
}
