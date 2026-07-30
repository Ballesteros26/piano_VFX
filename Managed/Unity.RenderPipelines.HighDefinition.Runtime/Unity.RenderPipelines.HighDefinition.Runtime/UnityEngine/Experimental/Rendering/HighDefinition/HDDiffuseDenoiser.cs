using System;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

namespace UnityEngine.Experimental.Rendering.HighDefinition
{
	// Token: 0x02000011 RID: 17
	internal class HDDiffuseDenoiser
	{
		// Token: 0x06000014 RID: 20 RVA: 0x00002618 File Offset: 0x00000818
		public void Init(RenderPipelineResources rpResources, HDRenderPipelineRayTracingResources rpRTResources, SharedRTManager sharedRTManager, HDRenderPipeline renderPipeline)
		{
			this.m_SimpleDenoiserCS = rpRTResources.diffuseDenoiserCS;
			this.m_OwenScrambleRGBA = rpResources.textures.owenScrambledRGBATex;
			this.m_SharedRTManager = sharedRTManager;
			this.m_RenderPipeline = renderPipeline;
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002646 File Offset: 0x00000846
		public void Release()
		{
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002648 File Offset: 0x00000848
		public void DenoiseBuffer(CommandBuffer cmd, HDCamera hdCamera, RTHandle noisySignal, RTHandle outputSignal, float kernelSize, bool singleChannel = true, bool halfResolutionFilter = false)
		{
			int actualWidth = hdCamera.actualWidth;
			int actualHeight = hdCamera.actualHeight;
			int num = 8;
			int num2 = (actualWidth + (num - 1)) / num;
			int num3 = (actualHeight + (num - 1)) / num;
			RTHandle rayTracingBuffer = this.m_RenderPipeline.GetRayTracingBuffer(InternalRayTracingBuffers.RGBA0);
			int num4 = this.m_SimpleDenoiserCS.FindKernel(singleChannel ? "BilateralFilterSingle" : "BilateralFilterColor");
			cmd.SetGlobalTexture(HDShaderIDs._OwenScrambledRGTexture, this.m_OwenScrambleRGBA);
			cmd.SetComputeFloatParam(this.m_SimpleDenoiserCS, HDShaderIDs._DenoiserFilterRadius, kernelSize);
			cmd.SetComputeTextureParam(this.m_SimpleDenoiserCS, num4, HDShaderIDs._DenoiseInputTexture, noisySignal);
			cmd.SetComputeTextureParam(this.m_SimpleDenoiserCS, num4, HDShaderIDs._DepthTexture, this.m_SharedRTManager.GetDepthStencilBuffer(false));
			cmd.SetComputeTextureParam(this.m_SimpleDenoiserCS, num4, HDShaderIDs._NormalBufferTexture, this.m_SharedRTManager.GetNormalBuffer(false));
			cmd.SetComputeTextureParam(this.m_SimpleDenoiserCS, num4, HDShaderIDs._DenoiseOutputTextureRW, halfResolutionFilter ? rayTracingBuffer : outputSignal);
			cmd.SetComputeIntParam(this.m_SimpleDenoiserCS, HDShaderIDs._HalfResolutionFilter, halfResolutionFilter ? 1 : 0);
			cmd.SetComputeFloatParam(this.m_SimpleDenoiserCS, HDShaderIDs._PixelSpreadAngleTangent, HDRenderPipeline.GetPixelSpreadTangent(hdCamera.camera.fieldOfView, hdCamera.actualWidth, hdCamera.actualHeight));
			cmd.DispatchCompute(this.m_SimpleDenoiserCS, num4, num2, num3, hdCamera.viewCount);
			if (halfResolutionFilter)
			{
				num4 = this.m_SimpleDenoiserCS.FindKernel(singleChannel ? "GatherSingle" : "GatherColor");
				cmd.SetComputeTextureParam(this.m_SimpleDenoiserCS, num4, HDShaderIDs._DenoiseInputTexture, rayTracingBuffer);
				cmd.SetComputeTextureParam(this.m_SimpleDenoiserCS, num4, HDShaderIDs._DenoiseOutputTextureRW, outputSignal);
				cmd.DispatchCompute(this.m_SimpleDenoiserCS, num4, num2, num3, hdCamera.viewCount);
			}
		}

		// Token: 0x04000045 RID: 69
		private ComputeShader m_SimpleDenoiserCS;

		// Token: 0x04000046 RID: 70
		private Texture m_OwenScrambleRGBA;

		// Token: 0x04000047 RID: 71
		private SharedRTManager m_SharedRTManager;

		// Token: 0x04000048 RID: 72
		private HDRenderPipeline m_RenderPipeline;
	}
}
