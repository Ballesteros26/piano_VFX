using System;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x0200012A RID: 298
	internal class MipGenerator
	{
		// Token: 0x0600090A RID: 2314 RVA: 0x00049848 File Offset: 0x00047A48
		public MipGenerator(RenderPipelineResources defaultResources)
		{
			this.m_TempColorTargets = new RTHandle[this.tmpTargetCount];
			this.m_TempDownsamplePyramid = new RTHandle[this.tmpTargetCount];
			this.m_DepthPyramidCS = defaultResources.shaders.depthPyramidCS;
			this.m_DepthDownsampleKernel = this.m_DepthPyramidCS.FindKernel("KDepthDownsample8DualUav");
			this.m_SrcOffset = new int[4];
			this.m_DstOffset = new int[4];
			this.m_ColorPyramidPS = defaultResources.shaders.colorPyramidPS;
			this.m_ColorPyramidPSMat = CoreUtils.CreateEngineMaterial(this.m_ColorPyramidPS);
			this.m_PropertyBlock = new MaterialPropertyBlock();
		}

		// Token: 0x0600090B RID: 2315 RVA: 0x000498EC File Offset: 0x00047AEC
		public void Release()
		{
			for (int i = 0; i < this.tmpTargetCount; i++)
			{
				RTHandles.Release(this.m_TempColorTargets[i]);
				this.m_TempColorTargets[i] = null;
				RTHandles.Release(this.m_TempDownsamplePyramid[i]);
				this.m_TempDownsamplePyramid[i] = null;
			}
		}

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x0600090C RID: 2316 RVA: 0x00049936 File Offset: 0x00047B36
		private int tmpTargetCount
		{
			get
			{
				if (TextureXR.useTexArray)
				{
					return 2;
				}
				return 1;
			}
		}

		// Token: 0x0600090D RID: 2317 RVA: 0x00049944 File Offset: 0x00047B44
		public void RenderMinDepthPyramid(CommandBuffer cmd, RenderTexture texture, HDUtils.PackedMipChainInfo info)
		{
			HDUtils.CheckRTCreated(texture);
			ComputeShader depthPyramidCS = this.m_DepthPyramidCS;
			int depthDownsampleKernel = this.m_DepthDownsampleKernel;
			for (int i = 1; i < info.mipLevelCount; i++)
			{
				Vector2Int vector2Int = info.mipLevelSizes[i];
				Vector2Int vector2Int2 = info.mipLevelOffsets[i];
				Vector2Int vector2Int3 = info.mipLevelSizes[i - 1];
				Vector2Int vector2Int4 = info.mipLevelOffsets[i - 1];
				Vector2Int vector2Int5 = vector2Int4 + vector2Int3 - Vector2Int.one;
				this.m_SrcOffset[0] = vector2Int4.x;
				this.m_SrcOffset[1] = vector2Int4.y;
				this.m_SrcOffset[2] = vector2Int5.x;
				this.m_SrcOffset[3] = vector2Int5.y;
				this.m_DstOffset[0] = vector2Int2.x;
				this.m_DstOffset[1] = vector2Int2.y;
				this.m_DstOffset[2] = 0;
				this.m_DstOffset[3] = 0;
				cmd.SetComputeIntParams(depthPyramidCS, HDShaderIDs._SrcOffsetAndLimit, this.m_SrcOffset);
				cmd.SetComputeIntParams(depthPyramidCS, HDShaderIDs._DstOffset, this.m_DstOffset);
				cmd.SetComputeTextureParam(depthPyramidCS, depthDownsampleKernel, HDShaderIDs._DepthMipChain, texture);
				cmd.DispatchCompute(depthPyramidCS, depthDownsampleKernel, HDUtils.DivRoundUp(vector2Int.x, 8), HDUtils.DivRoundUp(vector2Int.y, 8), texture.volumeDepth);
			}
		}

		// Token: 0x0600090E RID: 2318 RVA: 0x00049A98 File Offset: 0x00047C98
		public int RenderColorGaussianPyramid(CommandBuffer cmd, Vector2Int size, Texture source, RenderTexture destination)
		{
			bool flag = source.dimension == TextureDimension.Tex2DArray;
			int num = (flag ? 1 : 0);
			if (this.m_TempColorTargets[num] == null)
			{
				RTHandle[] tempColorTargets = this.m_TempColorTargets;
				int num2 = num;
				Vector2 vector = Vector2.one * 0.5f;
				int num3 = (flag ? TextureXR.slices : 1);
				DepthBits depthBits = DepthBits.None;
				TextureDimension textureDimension = source.dimension;
				tempColorTargets[num2] = RTHandles.Alloc(vector, num3, depthBits, destination.graphicsFormat, FilterMode.Bilinear, TextureWrapMode.Repeat, textureDimension, true, false, true, false, 1, 0f, false, false, true, RenderTextureMemoryless.None, "Temp Gaussian Pyramid Target");
			}
			int num4 = 0;
			int num5 = size.x;
			int num6 = size.y;
			int volumeDepth = destination.volumeDepth;
			if (this.m_TempDownsamplePyramid[num] == null)
			{
				RTHandle[] tempDownsamplePyramid = this.m_TempDownsamplePyramid;
				int num7 = num;
				Vector2 vector2 = Vector2.one * 0.5f;
				int num8 = (flag ? TextureXR.slices : 1);
				DepthBits depthBits2 = DepthBits.None;
				TextureDimension textureDimension = source.dimension;
				tempDownsamplePyramid[num7] = RTHandles.Alloc(vector2, num8, depthBits2, destination.graphicsFormat, FilterMode.Bilinear, TextureWrapMode.Repeat, textureDimension, false, false, true, false, 1, 0f, false, false, true, RenderTextureMemoryless.None, "Temporary Downsampled Pyramid");
			}
			float num9 = (float)size.x / (float)source.width;
			float num10 = (float)size.y / (float)source.height;
			this.m_PropertyBlock.SetTexture(HDShaderIDs._BlitTexture, source);
			this.m_PropertyBlock.SetVector(HDShaderIDs._BlitScaleBias, new Vector4(num9, num10, 0f, 0f));
			this.m_PropertyBlock.SetFloat(HDShaderIDs._BlitMipLevel, 0f);
			cmd.SetRenderTarget(destination, 0, CubemapFace.Unknown, -1);
			cmd.SetViewport(new Rect(0f, 0f, (float)num5, (float)num6));
			cmd.DrawProcedural(Matrix4x4.identity, HDUtils.GetBlitMaterial(source.dimension, false), 0, MeshTopology.Triangles, 3, 1, this.m_PropertyBlock);
			int num11 = destination.width;
			int num12 = destination.height;
			while (num5 >= 8 || num6 >= 8)
			{
				int num13 = Mathf.Max(1, num5 >> 1);
				int num14 = Mathf.Max(1, num6 >> 1);
				float num15 = (float)num5 / (float)num11;
				float num16 = (float)num6 / (float)num12;
				this.m_PropertyBlock.SetTexture(HDShaderIDs._BlitTexture, destination);
				this.m_PropertyBlock.SetVector(HDShaderIDs._BlitScaleBias, new Vector4(num15, num16, 0f, 0f));
				this.m_PropertyBlock.SetFloat(HDShaderIDs._BlitMipLevel, (float)num4);
				cmd.SetRenderTarget(this.m_TempDownsamplePyramid[num], 0, CubemapFace.Unknown, -1);
				cmd.SetViewport(new Rect(0f, 0f, (float)num13, (float)num14));
				cmd.DrawProcedural(Matrix4x4.identity, HDUtils.GetBlitMaterial(source.dimension, false), 1, MeshTopology.Triangles, 3, 1, this.m_PropertyBlock);
				float num17 = (float)this.m_TempDownsamplePyramid[num].rt.width;
				float num18 = (float)this.m_TempDownsamplePyramid[num].rt.height;
				num15 = (float)num13 / num17;
				num16 = (float)num14 / num18;
				this.m_PropertyBlock.SetTexture(HDShaderIDs._Source, this.m_TempDownsamplePyramid[num]);
				this.m_PropertyBlock.SetVector(HDShaderIDs._SrcScaleBias, new Vector4(num15, num16, 0f, 0f));
				this.m_PropertyBlock.SetVector(HDShaderIDs._SrcUvLimits, new Vector4(((float)num13 - 0.5f) / num17, ((float)num14 - 0.5f) / num18, 1f / num17, 0f));
				this.m_PropertyBlock.SetFloat(HDShaderIDs._SourceMip, 0f);
				cmd.SetRenderTarget(this.m_TempColorTargets[num], 0, CubemapFace.Unknown, -1);
				cmd.SetViewport(new Rect(0f, 0f, (float)num13, (float)num14));
				cmd.DrawProcedural(Matrix4x4.identity, this.m_ColorPyramidPSMat, num, MeshTopology.Triangles, 3, 1, this.m_PropertyBlock);
				this.m_PropertyBlock.SetTexture(HDShaderIDs._Source, this.m_TempColorTargets[num]);
				this.m_PropertyBlock.SetVector(HDShaderIDs._SrcScaleBias, new Vector4(num15, num16, 0f, 0f));
				this.m_PropertyBlock.SetVector(HDShaderIDs._SrcUvLimits, new Vector4(((float)num13 - 0.5f) / num17, ((float)num14 - 0.5f) / num18, 0f, 1f / num18));
				this.m_PropertyBlock.SetFloat(HDShaderIDs._SourceMip, 0f);
				cmd.SetRenderTarget(destination, num4 + 1, CubemapFace.Unknown, -1);
				cmd.SetViewport(new Rect(0f, 0f, (float)num13, (float)num14));
				cmd.DrawProcedural(Matrix4x4.identity, this.m_ColorPyramidPSMat, num, MeshTopology.Triangles, 3, 1, this.m_PropertyBlock);
				num4++;
				num5 >>= 1;
				num6 >>= 1;
				num11 >>= 1;
				num12 >>= 1;
			}
			return num4 + 1;
		}

		// Token: 0x04000DD5 RID: 3541
		private RTHandle[] m_TempColorTargets;

		// Token: 0x04000DD6 RID: 3542
		private RTHandle[] m_TempDownsamplePyramid;

		// Token: 0x04000DD7 RID: 3543
		private ComputeShader m_DepthPyramidCS;

		// Token: 0x04000DD8 RID: 3544
		private Shader m_ColorPyramidPS;

		// Token: 0x04000DD9 RID: 3545
		private Material m_ColorPyramidPSMat;

		// Token: 0x04000DDA RID: 3546
		private MaterialPropertyBlock m_PropertyBlock;

		// Token: 0x04000DDB RID: 3547
		private int m_DepthDownsampleKernel;

		// Token: 0x04000DDC RID: 3548
		private int[] m_SrcOffset;

		// Token: 0x04000DDD RID: 3549
		private int[] m_DstOffset;
	}
}
