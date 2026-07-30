using System;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine.Experimental.Rendering;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x02000038 RID: 56
	internal class RayCountManager
	{
		// Token: 0x0600018C RID: 396 RVA: 0x0000A824 File Offset: 0x00008A24
		public void Init(HDRenderPipelineRayTracingResources rayTracingResources)
		{
			this.rayCountCS = rayTracingResources.countTracedRays;
			this.m_RayCountTexture = RTHandles.Alloc(Vector2.one, TextureXR.slices * 9, DepthBits.None, GraphicsFormat.R16_UInt, FilterMode.Point, TextureWrapMode.Repeat, TextureDimension.Tex2DArray, true, false, true, false, 1, 0f, false, false, false, RenderTextureMemoryless.None, "RayCountTextureDebug");
			this.m_ReducedRayCountBuffer0 = new ComputeBuffer(589824, 4);
			this.m_ReducedRayCountBuffer1 = new ComputeBuffer(9216, 4);
			this.m_ReducedRayCountBuffer2 = new ComputeBuffer(10, 4);
			for (int i = 0; i < 9; i++)
			{
				this.m_ReducedRayCountValues[i] = 0U;
			}
			this.m_IsActive = false;
			this.m_RayTracingSupported = true;
		}

		// Token: 0x0600018D RID: 397 RVA: 0x0000A8C2 File Offset: 0x00008AC2
		public void Release()
		{
			RTHandles.Release(this.m_RayCountTexture);
			CoreUtils.SafeRelease(this.m_ReducedRayCountBuffer0);
			CoreUtils.SafeRelease(this.m_ReducedRayCountBuffer1);
			CoreUtils.SafeRelease(this.m_ReducedRayCountBuffer2);
		}

		// Token: 0x0600018E RID: 398 RVA: 0x0000A8F0 File Offset: 0x00008AF0
		public void ClearRayCount(CommandBuffer cmd, HDCamera camera, bool isActive)
		{
			this.m_IsActive = isActive;
			if (this.m_IsActive)
			{
				int num = this.rayCountCS.FindKernel("ClearBuffer");
				cmd.SetComputeBufferParam(this.rayCountCS, num, HDShaderIDs._OutputRayCountBuffer, this.m_ReducedRayCountBuffer0);
				cmd.SetComputeIntParam(this.rayCountCS, HDShaderIDs._OutputBufferDimension, 2304);
				int num2 = 8;
				cmd.DispatchCompute(this.rayCountCS, num, num2, num2, 1);
				CoreUtils.SetRenderTarget(cmd, this.m_RayCountTexture, ClearFlag.Color, 0, CubemapFace.Unknown, -1);
			}
		}

		// Token: 0x0600018F RID: 399 RVA: 0x0000A96D File Offset: 0x00008B6D
		public int RayCountIsEnabled()
		{
			if (!this.m_IsActive)
			{
				return 0;
			}
			return 1;
		}

		// Token: 0x06000190 RID: 400 RVA: 0x0000A97A File Offset: 0x00008B7A
		public RTHandle GetRayCountTexture()
		{
			return this.m_RayCountTexture;
		}

		// Token: 0x06000191 RID: 401 RVA: 0x0000A984 File Offset: 0x00008B84
		public void EvaluateRayCount(CommandBuffer cmd, HDCamera camera)
		{
			if (this.m_IsActive)
			{
				using (new ProfilingScope(cmd, ProfilingSampler.Get<HDProfileId>(HDProfileId.RaytracingDebugOverlay)))
				{
					int num = camera.actualWidth;
					int num2 = camera.actualHeight;
					int num3 = this.rayCountCS.FindKernel("TextureReduction");
					int num4 = 32;
					int num5 = Mathf.Max(1, (num + (num4 - 1)) / num4);
					int num6 = Mathf.Max(1, (num2 + (num4 - 1)) / num4);
					if (num6 > 32 || num5 > 32)
					{
						cmd.SetComputeTextureParam(this.rayCountCS, num3, HDShaderIDs._InputRayCountTexture, this.m_RayCountTexture);
						cmd.SetComputeBufferParam(this.rayCountCS, num3, HDShaderIDs._OutputRayCountBuffer, this.m_ReducedRayCountBuffer0);
						cmd.SetComputeIntParam(this.rayCountCS, HDShaderIDs._OutputBufferDimension, 2304);
						cmd.DispatchCompute(this.rayCountCS, num3, num5, num6, 1);
						num /= 32;
						num2 /= 32;
						num3 = this.rayCountCS.FindKernel("BufferReduction");
						num5 = Mathf.Max(1, (num + (num4 - 1)) / num4);
						num6 = Mathf.Max(1, (num2 + (num4 - 1)) / num4);
						cmd.SetComputeBufferParam(this.rayCountCS, num3, HDShaderIDs._InputRayCountBuffer, this.m_ReducedRayCountBuffer0);
						cmd.SetComputeBufferParam(this.rayCountCS, num3, HDShaderIDs._OutputRayCountBuffer, this.m_ReducedRayCountBuffer1);
						cmd.SetComputeIntParam(this.rayCountCS, HDShaderIDs._InputBufferDimension, 2304);
						cmd.SetComputeIntParam(this.rayCountCS, HDShaderIDs._OutputBufferDimension, 288);
						cmd.DispatchCompute(this.rayCountCS, num3, num5, num6, 1);
						num /= 32;
						num2 /= 32;
						num5 = Mathf.Max(1, (num + (num4 - 1)) / num4);
						num6 = Mathf.Max(1, (num2 + (num4 - 1)) / num4);
						cmd.SetComputeBufferParam(this.rayCountCS, num3, HDShaderIDs._InputRayCountBuffer, this.m_ReducedRayCountBuffer1);
						cmd.SetComputeBufferParam(this.rayCountCS, num3, HDShaderIDs._OutputRayCountBuffer, this.m_ReducedRayCountBuffer2);
						cmd.SetComputeIntParam(this.rayCountCS, HDShaderIDs._InputBufferDimension, 288);
						cmd.SetComputeIntParam(this.rayCountCS, HDShaderIDs._OutputBufferDimension, 9);
						cmd.DispatchCompute(this.rayCountCS, num3, num5, num6, 1);
					}
					else
					{
						cmd.SetComputeTextureParam(this.rayCountCS, num3, HDShaderIDs._InputRayCountTexture, this.m_RayCountTexture);
						cmd.SetComputeBufferParam(this.rayCountCS, num3, HDShaderIDs._OutputRayCountBuffer, this.m_ReducedRayCountBuffer1);
						cmd.SetComputeIntParam(this.rayCountCS, HDShaderIDs._OutputBufferDimension, 288);
						cmd.DispatchCompute(this.rayCountCS, num3, num5, num6, 1);
						num /= 32;
						num2 /= 32;
						num3 = this.rayCountCS.FindKernel("BufferReduction");
						num5 = Mathf.Max(1, (num + (num4 - 1)) / num4);
						num6 = Mathf.Max(1, (num2 + (num4 - 1)) / num4);
						cmd.SetComputeBufferParam(this.rayCountCS, num3, HDShaderIDs._InputRayCountBuffer, this.m_ReducedRayCountBuffer1);
						cmd.SetComputeBufferParam(this.rayCountCS, num3, HDShaderIDs._OutputRayCountBuffer, this.m_ReducedRayCountBuffer2);
						cmd.SetComputeIntParam(this.rayCountCS, HDShaderIDs._InputBufferDimension, 288);
						cmd.SetComputeIntParam(this.rayCountCS, HDShaderIDs._OutputBufferDimension, 9);
						cmd.DispatchCompute(this.rayCountCS, num3, num5, num6, 1);
					}
					AsyncGPUReadbackRequest asyncGPUReadbackRequest = AsyncGPUReadback.Request(this.m_ReducedRayCountBuffer2, 36, 0, null);
					this.rayCountReadbacks.Enqueue(asyncGPUReadbackRequest);
				}
			}
		}

		// Token: 0x06000192 RID: 402 RVA: 0x0000ACF4 File Offset: 0x00008EF4
		public uint GetRaysPerFrame(RayCountValues rayCountValue)
		{
			if (!this.m_RayTracingSupported || !this.m_IsActive)
			{
				return 0U;
			}
			while (this.rayCountReadbacks.Peek().done || this.rayCountReadbacks.Peek().hasError)
			{
				if (!this.rayCountReadbacks.Peek().hasError)
				{
					NativeArray<uint> data = this.rayCountReadbacks.Peek().GetData<uint>(0);
					for (int i = 0; i < 9; i++)
					{
						this.m_ReducedRayCountValues[i] = data[i];
					}
				}
				this.rayCountReadbacks.Dequeue();
			}
			if (rayCountValue != RayCountValues.Total)
			{
				return this.m_ReducedRayCountValues[(int)rayCountValue];
			}
			uint num = 0U;
			for (int j = 0; j < 9; j++)
			{
				num += this.m_ReducedRayCountValues[j];
			}
			return num;
		}

		// Token: 0x04000175 RID: 373
		private RTHandle m_RayCountTexture;

		// Token: 0x04000176 RID: 374
		private ComputeBuffer m_ReducedRayCountBuffer0;

		// Token: 0x04000177 RID: 375
		private ComputeBuffer m_ReducedRayCountBuffer1;

		// Token: 0x04000178 RID: 376
		private ComputeBuffer m_ReducedRayCountBuffer2;

		// Token: 0x04000179 RID: 377
		private uint[] m_ReducedRayCountValues = new uint[9];

		// Token: 0x0400017A RID: 378
		private ComputeShader rayCountCS;

		// Token: 0x0400017B RID: 379
		private bool m_IsActive;

		// Token: 0x0400017C RID: 380
		private bool m_RayTracingSupported;

		// Token: 0x0400017D RID: 381
		private Queue<AsyncGPUReadbackRequest> rayCountReadbacks = new Queue<AsyncGPUReadbackRequest>();
	}
}
