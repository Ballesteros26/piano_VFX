using System;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

namespace UnityEngine.Experimental.Rendering.HighDefinition
{
	// Token: 0x02000012 RID: 18
	internal class HDTemporalFilter
	{
		// Token: 0x06000018 RID: 24 RVA: 0x00002811 File Offset: 0x00000A11
		public void Init(HDRenderPipelineRayTracingResources rpRTResources, SharedRTManager sharedRTManager, HDRenderPipeline renderPipeline)
		{
			this.m_TemporalFilterCS = rpRTResources.temporalFilterCS;
			this.m_SharedRTManager = sharedRTManager;
			this.m_RenderPipeline = renderPipeline;
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002646 File Offset: 0x00000846
		public void Release()
		{
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002830 File Offset: 0x00000A30
		public void DenoiseBuffer(CommandBuffer cmd, HDCamera hdCamera, RTHandle noisySignal, RTHandle historySignal, RTHandle outputSignal, bool singleChannel = true, float historyValidity = 1f)
		{
			RTHandle currentFrameRT = hdCamera.GetCurrentFrameRT(6);
			RTHandle currentFrameRT2 = hdCamera.GetCurrentFrameRT(5);
			if (currentFrameRT == null || currentFrameRT2 == null)
			{
				HDUtils.BlitCameraTexture(cmd, noisySignal, historySignal, 0f, false);
				HDUtils.BlitCameraTexture(cmd, noisySignal, outputSignal, 0f, false);
				return;
			}
			int actualWidth = hdCamera.actualWidth;
			int actualHeight = hdCamera.actualHeight;
			int num = 8;
			int num2 = (actualWidth + (num - 1)) / num;
			int num3 = (actualHeight + (num - 1)) / num;
			RTHandle rayTracingBuffer = this.m_RenderPipeline.GetRayTracingBuffer(InternalRayTracingBuffers.R0);
			int num4 = this.m_TemporalFilterCS.FindKernel("ValidateHistory");
			Vector2 vector = new Vector2((float)hdCamera.actualWidth / (float)historySignal.rt.width, (float)hdCamera.actualHeight / (float)historySignal.rt.height);
			cmd.SetComputeVectorParam(this.m_TemporalFilterCS, HDShaderIDs._RTHandleScaleHistory, vector);
			cmd.SetComputeTextureParam(this.m_TemporalFilterCS, num4, HDShaderIDs._DepthTexture, this.m_SharedRTManager.GetDepthStencilBuffer(false));
			cmd.SetComputeTextureParam(this.m_TemporalFilterCS, num4, HDShaderIDs._HistoryDepthTexture, currentFrameRT);
			cmd.SetComputeTextureParam(this.m_TemporalFilterCS, num4, HDShaderIDs._NormalBufferTexture, this.m_SharedRTManager.GetNormalBuffer(false));
			cmd.SetComputeTextureParam(this.m_TemporalFilterCS, num4, HDShaderIDs._HistoryNormalBufferTexture, currentFrameRT2);
			cmd.SetComputeTextureParam(this.m_TemporalFilterCS, num4, HDShaderIDs._ValidationBufferRW, rayTracingBuffer);
			cmd.SetComputeTextureParam(this.m_TemporalFilterCS, num4, HDShaderIDs._VelocityBuffer, TextureXR.GetBlackTexture());
			cmd.SetComputeFloatParam(this.m_TemporalFilterCS, HDShaderIDs._HistoryValidity, historyValidity);
			cmd.SetComputeFloatParam(this.m_TemporalFilterCS, HDShaderIDs._PixelSpreadAngleTangent, HDRenderPipeline.GetPixelSpreadTangent(hdCamera.camera.fieldOfView, hdCamera.actualWidth, hdCamera.actualHeight));
			cmd.DispatchCompute(this.m_TemporalFilterCS, num4, num2, num3, hdCamera.viewCount);
			num4 = this.m_TemporalFilterCS.FindKernel(singleChannel ? "TemporalAccumulationSingle" : "TemporalAccumulationColor");
			cmd.SetComputeTextureParam(this.m_TemporalFilterCS, num4, HDShaderIDs._DenoiseInputTexture, noisySignal);
			cmd.SetComputeTextureParam(this.m_TemporalFilterCS, num4, HDShaderIDs._HistoryBuffer, historySignal);
			cmd.SetComputeTextureParam(this.m_TemporalFilterCS, num4, HDShaderIDs._DepthTexture, this.m_SharedRTManager.GetDepthStencilBuffer(false));
			cmd.SetComputeTextureParam(this.m_TemporalFilterCS, num4, HDShaderIDs._DenoiseOutputTextureRW, outputSignal);
			cmd.SetComputeTextureParam(this.m_TemporalFilterCS, num4, HDShaderIDs._ValidationBuffer, rayTracingBuffer);
			cmd.SetComputeTextureParam(this.m_TemporalFilterCS, num4, HDShaderIDs._VelocityBuffer, TextureXR.GetBlackTexture());
			cmd.DispatchCompute(this.m_TemporalFilterCS, num4, num2, num3, hdCamera.viewCount);
			num4 = this.m_TemporalFilterCS.FindKernel(singleChannel ? "CopyHistorySingle" : "CopyHistoryColor");
			cmd.SetComputeTextureParam(this.m_TemporalFilterCS, num4, HDShaderIDs._DenoiseInputTexture, outputSignal);
			cmd.SetComputeTextureParam(this.m_TemporalFilterCS, num4, HDShaderIDs._DenoiseOutputTextureRW, historySignal);
			cmd.DispatchCompute(this.m_TemporalFilterCS, num4, num2, num3, hdCamera.viewCount);
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002B4C File Offset: 0x00000D4C
		public void DenoiseBuffer(CommandBuffer cmd, HDCamera hdCamera, RTHandle noisySignal, RTHandle historySignal, RTHandle validationHistory, RTHandle velocityBuffer, RTHandle outputSignal, int sliceIndex, Vector4 channelMask, bool singleChannel = true, float historyValidity = 1f)
		{
			RTHandle currentFrameRT = hdCamera.GetCurrentFrameRT(6);
			RTHandle currentFrameRT2 = hdCamera.GetCurrentFrameRT(5);
			if (currentFrameRT == null || currentFrameRT2 == null)
			{
				HDUtils.BlitCameraTexture(cmd, noisySignal, historySignal, 0f, false);
				HDUtils.BlitCameraTexture(cmd, noisySignal, outputSignal, 0f, false);
				return;
			}
			int actualWidth = hdCamera.actualWidth;
			int actualHeight = hdCamera.actualHeight;
			int num = 8;
			int num2 = (actualWidth + (num - 1)) / num;
			int num3 = (actualHeight + (num - 1)) / num;
			RTHandle rayTracingBuffer = this.m_RenderPipeline.GetRayTracingBuffer(InternalRayTracingBuffers.R0);
			int num4 = this.m_TemporalFilterCS.FindKernel("ValidateHistory");
			Vector2 vector = new Vector2((float)hdCamera.actualWidth / (float)historySignal.rt.width, (float)hdCamera.actualHeight / (float)historySignal.rt.height);
			cmd.SetComputeVectorParam(this.m_TemporalFilterCS, HDShaderIDs._RTHandleScaleHistory, vector);
			cmd.SetComputeTextureParam(this.m_TemporalFilterCS, num4, HDShaderIDs._DepthTexture, this.m_SharedRTManager.GetDepthStencilBuffer(false));
			cmd.SetComputeTextureParam(this.m_TemporalFilterCS, num4, HDShaderIDs._HistoryDepthTexture, currentFrameRT);
			cmd.SetComputeTextureParam(this.m_TemporalFilterCS, num4, HDShaderIDs._NormalBufferTexture, this.m_SharedRTManager.GetNormalBuffer(false));
			cmd.SetComputeTextureParam(this.m_TemporalFilterCS, num4, HDShaderIDs._HistoryNormalBufferTexture, currentFrameRT2);
			cmd.SetComputeTextureParam(this.m_TemporalFilterCS, num4, HDShaderIDs._ValidationBufferRW, rayTracingBuffer);
			cmd.SetComputeTextureParam(this.m_TemporalFilterCS, num4, HDShaderIDs._VelocityBuffer, velocityBuffer);
			cmd.SetComputeFloatParam(this.m_TemporalFilterCS, HDShaderIDs._HistoryValidity, historyValidity);
			cmd.SetComputeFloatParam(this.m_TemporalFilterCS, HDShaderIDs._PixelSpreadAngleTangent, HDRenderPipeline.GetPixelSpreadTangent(hdCamera.camera.fieldOfView, hdCamera.actualWidth, hdCamera.actualHeight));
			cmd.DispatchCompute(this.m_TemporalFilterCS, num4, num2, num3, hdCamera.viewCount);
			num4 = this.m_TemporalFilterCS.FindKernel(singleChannel ? "TemporalAccumulationSingleArray" : "TemporalAccumulationColorArray");
			cmd.SetComputeTextureParam(this.m_TemporalFilterCS, num4, HDShaderIDs._DenoiseInputTexture, noisySignal);
			cmd.SetComputeTextureParam(this.m_TemporalFilterCS, num4, HDShaderIDs._HistoryBuffer, historySignal);
			cmd.SetComputeTextureParam(this.m_TemporalFilterCS, num4, HDShaderIDs._HistoryValidityBuffer, validationHistory);
			cmd.SetComputeTextureParam(this.m_TemporalFilterCS, num4, HDShaderIDs._DepthTexture, this.m_SharedRTManager.GetDepthStencilBuffer(false));
			cmd.SetComputeTextureParam(this.m_TemporalFilterCS, num4, HDShaderIDs._DenoiseOutputTextureRW, outputSignal);
			cmd.SetComputeTextureParam(this.m_TemporalFilterCS, num4, HDShaderIDs._ValidationBuffer, rayTracingBuffer);
			cmd.SetComputeTextureParam(this.m_TemporalFilterCS, num4, HDShaderIDs._VelocityBuffer, velocityBuffer);
			cmd.SetComputeIntParam(this.m_TemporalFilterCS, HDShaderIDs._DenoisingHistorySlice, sliceIndex);
			cmd.SetComputeVectorParam(this.m_TemporalFilterCS, HDShaderIDs._DenoisingHistoryMask, channelMask);
			cmd.DispatchCompute(this.m_TemporalFilterCS, num4, num2, num3, hdCamera.viewCount);
			num4 = this.m_TemporalFilterCS.FindKernel(singleChannel ? "CopyHistorySingleArray" : "CopyHistoryColorArray");
			cmd.SetComputeTextureParam(this.m_TemporalFilterCS, num4, HDShaderIDs._DenoiseInputTexture, outputSignal);
			cmd.SetComputeTextureParam(this.m_TemporalFilterCS, num4, HDShaderIDs._DenoiseOutputTextureRW, historySignal);
			cmd.SetComputeTextureParam(this.m_TemporalFilterCS, num4, HDShaderIDs._ValidityOutputTextureRW, validationHistory);
			cmd.SetComputeIntParam(this.m_TemporalFilterCS, HDShaderIDs._DenoisingHistorySlice, sliceIndex);
			cmd.SetComputeVectorParam(this.m_TemporalFilterCS, HDShaderIDs._DenoisingHistoryMask, channelMask);
			cmd.DispatchCompute(this.m_TemporalFilterCS, num4, num2, num3, hdCamera.viewCount);
		}

		// Token: 0x04000049 RID: 73
		private ComputeShader m_TemporalFilterCS;

		// Token: 0x0400004A RID: 74
		private SharedRTManager m_SharedRTManager;

		// Token: 0x0400004B RID: 75
		private HDRenderPipeline m_RenderPipeline;
	}
}
