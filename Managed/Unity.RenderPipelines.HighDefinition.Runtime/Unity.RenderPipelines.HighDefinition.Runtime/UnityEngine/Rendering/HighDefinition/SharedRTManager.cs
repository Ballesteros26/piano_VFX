using System;
using UnityEngine.Experimental.Rendering;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x020000C2 RID: 194
	internal class SharedRTManager
	{
		// Token: 0x06000712 RID: 1810 RVA: 0x0003730C File Offset: 0x0003550C
		public void InitSharedBuffers(GBufferManager gbufferManager, RenderPipelineSettings settings, RenderPipelineResources resources)
		{
			this.m_MSAASupported = settings.supportMSAA;
			this.m_MSAASamples = (this.m_MSAASupported ? settings.msaaSampleCount : MSAASamples.None);
			this.m_MotionVectorsSupport = settings.supportMotionVectors;
			this.m_ReuseGBufferMemory = settings.supportedLitShaderMode != RenderPipelineSettings.SupportedLitShaderMode.ForwardOnly;
			this.m_CameraDepthStencilBuffer = RTHandles.Alloc(Vector2.one, TextureXR.slices, DepthBits.Depth32, GraphicsFormat.R8G8B8A8_SRGB, FilterMode.Point, TextureWrapMode.Repeat, TextureXR.dimension, false, false, true, false, 1, 0f, false, false, true, RenderTextureMemoryless.None, "CameraDepthStencil");
			this.m_CameraDepthBufferMipChainInfo = default(HDUtils.PackedMipChainInfo);
			this.m_CameraDepthBufferMipChainInfo.Allocate();
			this.m_CameraDepthBufferMipChain = RTHandles.Alloc(new ScaleFunc(this.ComputeDepthBufferMipChainSize), TextureXR.slices, DepthBits.None, GraphicsFormat.R32_SFloat, FilterMode.Point, TextureWrapMode.Repeat, TextureXR.dimension, true, false, true, false, 1, 0f, false, false, true, RenderTextureMemoryless.None, "CameraDepthBufferMipChain");
			if (settings.lowresTransparentSettings.enabled)
			{
				this.m_CameraHalfResDepthBuffer = RTHandles.Alloc(Vector2.one * 0.5f, TextureXR.slices, DepthBits.Depth32, GraphicsFormat.R8G8B8A8_SRGB, FilterMode.Point, TextureWrapMode.Repeat, TextureXR.dimension, false, false, true, false, 1, 0f, false, false, true, RenderTextureMemoryless.None, "LowResDepthBuffer");
			}
			if (this.m_MotionVectorsSupport)
			{
				this.m_MotionVectorsRT = RTHandles.Alloc(Vector2.one, TextureXR.slices, DepthBits.None, Builtin.GetMotionVectorFormat(), FilterMode.Point, TextureWrapMode.Repeat, TextureXR.dimension, false, false, true, false, 1, 0f, false, false, true, RenderTextureMemoryless.None, "MotionVectors");
				if (this.m_MSAASupported)
				{
					this.m_MotionVectorsMSAART = RTHandles.Alloc(Vector2.one, TextureXR.slices, DepthBits.None, Builtin.GetMotionVectorFormat(), FilterMode.Point, TextureWrapMode.Repeat, TextureXR.dimension, false, false, true, false, 1, 0f, true, true, true, RenderTextureMemoryless.None, "MotionVectorsMSAA");
				}
			}
			if (this.m_MSAASupported)
			{
				this.m_CameraDepthStencilMSAABuffer = RTHandles.Alloc(Vector2.one, TextureXR.slices, DepthBits.Depth24, GraphicsFormat.R8G8B8A8_SRGB, FilterMode.Point, TextureWrapMode.Repeat, TextureXR.dimension, false, false, true, false, 1, 0f, true, true, true, RenderTextureMemoryless.None, "CameraDepthStencilMSAA");
				this.m_CameraDepthValuesBuffer = RTHandles.Alloc(Vector2.one, TextureXR.slices, DepthBits.None, GraphicsFormat.R32G32B32A32_SFloat, FilterMode.Point, TextureWrapMode.Repeat, TextureXR.dimension, false, false, true, false, 1, 0f, false, false, true, RenderTextureMemoryless.None, "DepthValuesBuffer");
				this.m_DepthAsColorMSAART = RTHandles.Alloc(Vector2.one, TextureXR.slices, DepthBits.None, GraphicsFormat.R32_SFloat, FilterMode.Point, TextureWrapMode.Repeat, TextureXR.dimension, false, false, true, false, 1, 0f, true, true, true, RenderTextureMemoryless.None, "DepthAsColorMSAA");
				this.m_StencilBufferResolved = RTHandles.Alloc(Vector2.one, TextureXR.slices, DepthBits.None, GraphicsFormat.R8G8_UInt, FilterMode.Point, TextureWrapMode.Repeat, TextureXR.dimension, true, false, true, false, 1, 0f, false, false, true, RenderTextureMemoryless.None, "StencilBufferResolved");
				this.m_NormalMSAART = RTHandles.Alloc(Vector2.one, TextureXR.slices, DepthBits.None, GraphicsFormat.R8G8B8A8_UNorm, FilterMode.Point, TextureWrapMode.Repeat, TextureXR.dimension, false, false, true, false, 1, 0f, true, true, true, RenderTextureMemoryless.None, "NormalBufferMSAA");
				this.m_DepthResolveMaterial = CoreUtils.CreateEngineMaterial(resources.shaders.depthValuesPS);
				this.m_ColorResolveMaterial = CoreUtils.CreateEngineMaterial(resources.shaders.colorResolvePS);
			}
			this.AllocateCoarseStencilBuffer(RTHandles.maxWidth, RTHandles.maxHeight, TextureXR.slices);
			if (!this.m_ReuseGBufferMemory)
			{
				this.m_NormalRT = RTHandles.Alloc(Vector2.one, TextureXR.slices, DepthBits.None, GraphicsFormat.R8G8B8A8_UNorm, FilterMode.Point, TextureWrapMode.Repeat, TextureXR.dimension, true, false, true, false, 1, 0f, false, false, true, RenderTextureMemoryless.None, "NormalBuffer");
				return;
			}
			this.m_NormalRT = gbufferManager.GetNormalBuffer(0);
		}

		// Token: 0x06000713 RID: 1811 RVA: 0x0003762C File Offset: 0x0003582C
		public bool IsConsolePlatform()
		{
			return SystemInfo.graphicsDeviceType == GraphicsDeviceType.PlayStation4 || SystemInfo.graphicsDeviceType == GraphicsDeviceType.XboxOne || SystemInfo.graphicsDeviceType == GraphicsDeviceType.XboxOneD3D12;
		}

		// Token: 0x06000714 RID: 1812 RVA: 0x0003764C File Offset: 0x0003584C
		public RenderTargetIdentifier[] GetPrepassBuffersRTI(FrameSettings frameSettings)
		{
			if (frameSettings.IsEnabled(FrameSettingsField.MSAA))
			{
				this.m_RTIDs2[0] = this.m_NormalMSAART.nameID;
				this.m_RTIDs2[1] = this.m_DepthAsColorMSAART.nameID;
				return this.m_RTIDs2;
			}
			this.m_RTIDs1[0] = this.m_NormalRT.nameID;
			return this.m_RTIDs1;
		}

		// Token: 0x06000715 RID: 1813 RVA: 0x000376B8 File Offset: 0x000358B8
		public RenderTargetIdentifier[] GetMotionVectorsPassBuffersRTI(FrameSettings frameSettings)
		{
			if (frameSettings.IsEnabled(FrameSettingsField.MSAA))
			{
				this.m_RTIDs3[0] = this.m_MotionVectorsMSAART.nameID;
				this.m_RTIDs3[1] = this.m_NormalMSAART.nameID;
				this.m_RTIDs3[2] = this.m_DepthAsColorMSAART.nameID;
				return this.m_RTIDs3;
			}
			this.m_RTIDs2[0] = this.m_MotionVectorsRT.nameID;
			this.m_RTIDs2[1] = this.m_NormalRT.nameID;
			return this.m_RTIDs2;
		}

		// Token: 0x06000716 RID: 1814 RVA: 0x00037750 File Offset: 0x00035950
		public RTHandle GetNormalBuffer(bool isMSAA = false)
		{
			if (isMSAA)
			{
				return this.m_NormalMSAART;
			}
			return this.m_NormalRT;
		}

		// Token: 0x06000717 RID: 1815 RVA: 0x00037762 File Offset: 0x00035962
		public RTHandle GetMotionVectorsBuffer(bool isMSAA = false)
		{
			if (isMSAA)
			{
				return this.m_MotionVectorsMSAART;
			}
			return this.m_MotionVectorsRT;
		}

		// Token: 0x06000718 RID: 1816 RVA: 0x00037774 File Offset: 0x00035974
		public RTHandle GetDepthStencilBuffer(bool isMSAA = false)
		{
			if (isMSAA)
			{
				return this.m_CameraDepthStencilMSAABuffer;
			}
			return this.m_CameraDepthStencilBuffer;
		}

		// Token: 0x06000719 RID: 1817 RVA: 0x00037786 File Offset: 0x00035986
		public RTHandle GetStencilBuffer(bool isMSAA = false)
		{
			if (isMSAA)
			{
				return this.m_StencilBufferResolved;
			}
			return this.m_CameraDepthStencilBuffer;
		}

		// Token: 0x0600071A RID: 1818 RVA: 0x00037798 File Offset: 0x00035998
		public ComputeBuffer GetCoarseStencilBuffer()
		{
			return this.m_CoarseStencilBuffer;
		}

		// Token: 0x0600071B RID: 1819 RVA: 0x000377A0 File Offset: 0x000359A0
		public RTHandle GetLowResDepthBuffer()
		{
			return this.m_CameraHalfResDepthBuffer;
		}

		// Token: 0x0600071C RID: 1820 RVA: 0x000377A8 File Offset: 0x000359A8
		public RTHandle GetDepthTexture(bool isMSAA = false)
		{
			if (isMSAA)
			{
				return this.m_DepthAsColorMSAART;
			}
			return this.m_CameraDepthBufferMipChain;
		}

		// Token: 0x0600071D RID: 1821 RVA: 0x000377BA File Offset: 0x000359BA
		public RTHandle GetDepthValuesTexture()
		{
			return this.m_CameraDepthValuesBuffer;
		}

		// Token: 0x0600071E RID: 1822 RVA: 0x000377C2 File Offset: 0x000359C2
		public void SetNumMSAASamples(MSAASamples msaaSamples)
		{
			this.m_MSAASamples = msaaSamples;
		}

		// Token: 0x0600071F RID: 1823 RVA: 0x000377CB File Offset: 0x000359CB
		public Vector2Int ComputeDepthBufferMipChainSize(Vector2Int screenSize)
		{
			this.m_CameraDepthBufferMipChainInfo.ComputePackedMipChainInfo(screenSize);
			return this.m_CameraDepthBufferMipChainInfo.textureSize;
		}

		// Token: 0x06000720 RID: 1824 RVA: 0x000377E4 File Offset: 0x000359E4
		public HDUtils.PackedMipChainInfo GetDepthBufferMipChainInfo()
		{
			return this.m_CameraDepthBufferMipChainInfo;
		}

		// Token: 0x06000721 RID: 1825 RVA: 0x00002646 File Offset: 0x00000846
		public void Build(HDRenderPipelineAsset hdAsset)
		{
		}

		// Token: 0x06000722 RID: 1826 RVA: 0x000377EC File Offset: 0x000359EC
		public void AllocateCoarseStencilBuffer(int width, int height, int viewCount)
		{
			if (width > 8 && height > 8)
			{
				this.m_CoarseStencilBuffer = new ComputeBuffer(HDUtils.DivRoundUp(width, 8) * HDUtils.DivRoundUp(height, 8) * viewCount, 4);
			}
		}

		// Token: 0x06000723 RID: 1827 RVA: 0x00037813 File Offset: 0x00035A13
		public void DisposeCoarseStencilBuffer()
		{
			CoreUtils.SafeRelease(this.m_CoarseStencilBuffer);
		}

		// Token: 0x06000724 RID: 1828 RVA: 0x00037820 File Offset: 0x00035A20
		public void Cleanup()
		{
			if (!this.m_ReuseGBufferMemory)
			{
				RTHandles.Release(this.m_NormalRT);
			}
			if (this.m_MotionVectorsSupport)
			{
				RTHandles.Release(this.m_MotionVectorsRT);
				if (this.m_MSAASupported)
				{
					RTHandles.Release(this.m_MotionVectorsMSAART);
				}
			}
			RTHandles.Release(this.m_CameraDepthStencilBuffer);
			RTHandles.Release(this.m_CameraDepthBufferMipChain);
			RTHandles.Release(this.m_CameraHalfResDepthBuffer);
			this.DisposeCoarseStencilBuffer();
			if (this.m_MSAASupported)
			{
				RTHandles.Release(this.m_CameraDepthStencilMSAABuffer);
				RTHandles.Release(this.m_CameraDepthValuesBuffer);
				RTHandles.Release(this.m_StencilBufferResolved);
				RTHandles.Release(this.m_NormalMSAART);
				RTHandles.Release(this.m_DepthAsColorMSAART);
				CoreUtils.Destroy(this.m_DepthResolveMaterial);
				CoreUtils.Destroy(this.m_ColorResolveMaterial);
			}
		}

		// Token: 0x06000725 RID: 1829 RVA: 0x0001EE37 File Offset: 0x0001D037
		public static int SampleCountToPassIndex(MSAASamples samples)
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

		// Token: 0x06000726 RID: 1830 RVA: 0x000378E2 File Offset: 0x00035AE2
		public void BindNormalBuffer(CommandBuffer cmd, bool isMSAA = false)
		{
			cmd.SetGlobalTexture(HDShaderIDs._NormalBufferTexture, this.GetNormalBuffer(isMSAA));
		}

		// Token: 0x06000727 RID: 1831 RVA: 0x000378FC File Offset: 0x00035AFC
		public void ResolveSharedRT(CommandBuffer cmd, HDCamera hdCamera)
		{
			if (hdCamera.frameSettings.IsEnabled(FrameSettingsField.MSAA))
			{
				using (new ProfilingScope(cmd, ProfilingSampler.Get<HDProfileId>(HDProfileId.ResolveMSAADepth)))
				{
					this.m_RTIDs3[0] = this.m_CameraDepthValuesBuffer.nameID;
					this.m_RTIDs3[1] = this.m_NormalRT.nameID;
					this.m_RTIDs3[2] = this.m_MotionVectorsRT.nameID;
					CoreUtils.SetRenderTarget(cmd, this.m_RTIDs3, this.m_CameraDepthStencilBuffer);
					Shader.SetGlobalTexture(HDShaderIDs._NormalTextureMS, this.m_NormalMSAART);
					Shader.SetGlobalTexture(HDShaderIDs._DepthTextureMS, this.m_DepthAsColorMSAART);
					Shader.SetGlobalTexture(HDShaderIDs._MotionVectorTextureMS, this.m_MotionVectorsMSAART);
					cmd.DrawProcedural(Matrix4x4.identity, this.m_DepthResolveMaterial, SharedRTManager.SampleCountToPassIndex(this.m_MSAASamples), MeshTopology.Triangles, 3, 1);
				}
			}
		}

		// Token: 0x06000728 RID: 1832 RVA: 0x00037A04 File Offset: 0x00035C04
		public void ResolveMSAAColor(CommandBuffer cmd, HDCamera hdCamera, RTHandle msaaTarget, RTHandle simpleTarget)
		{
			if (hdCamera.frameSettings.IsEnabled(FrameSettingsField.MSAA))
			{
				using (new ProfilingScope(cmd, ProfilingSampler.Get<HDProfileId>(HDProfileId.ResolveMSAAColor)))
				{
					CoreUtils.SetRenderTarget(cmd, simpleTarget, ClearFlag.None, 0, CubemapFace.Unknown, -1);
					this.m_PropertyBlock.SetTexture(HDShaderIDs._ColorTextureMS, msaaTarget);
					cmd.DrawProcedural(Matrix4x4.identity, this.m_ColorResolveMaterial, SharedRTManager.SampleCountToPassIndex(this.m_MSAASamples), MeshTopology.Triangles, 3, 1, this.m_PropertyBlock);
				}
			}
		}

		// Token: 0x04000731 RID: 1841
		private RTHandle m_NormalRT;

		// Token: 0x04000732 RID: 1842
		private RTHandle m_MotionVectorsRT;

		// Token: 0x04000733 RID: 1843
		private RTHandle m_CameraDepthStencilBuffer;

		// Token: 0x04000734 RID: 1844
		private RTHandle m_StencilBufferResolved;

		// Token: 0x04000735 RID: 1845
		private RTHandle m_CameraDepthBufferMipChain;

		// Token: 0x04000736 RID: 1846
		private RTHandle m_CameraHalfResDepthBuffer;

		// Token: 0x04000737 RID: 1847
		private HDUtils.PackedMipChainInfo m_CameraDepthBufferMipChainInfo;

		// Token: 0x04000738 RID: 1848
		private RTHandle m_NormalMSAART;

		// Token: 0x04000739 RID: 1849
		private RTHandle m_MotionVectorsMSAART;

		// Token: 0x0400073A RID: 1850
		private RTHandle m_DepthAsColorMSAART;

		// Token: 0x0400073B RID: 1851
		private RTHandle m_CameraDepthStencilMSAABuffer;

		// Token: 0x0400073C RID: 1852
		private RTHandle m_CameraDepthValuesBuffer;

		// Token: 0x0400073D RID: 1853
		private ComputeBuffer m_CoarseStencilBuffer;

		// Token: 0x0400073E RID: 1854
		private Material m_DepthResolveMaterial;

		// Token: 0x0400073F RID: 1855
		private Material m_ColorResolveMaterial;

		// Token: 0x04000740 RID: 1856
		private bool m_ReuseGBufferMemory;

		// Token: 0x04000741 RID: 1857
		private bool m_MotionVectorsSupport;

		// Token: 0x04000742 RID: 1858
		private bool m_MSAASupported;

		// Token: 0x04000743 RID: 1859
		private MSAASamples m_MSAASamples = MSAASamples.None;

		// Token: 0x04000744 RID: 1860
		protected RenderTargetIdentifier[] m_RTIDs1 = new RenderTargetIdentifier[1];

		// Token: 0x04000745 RID: 1861
		protected RenderTargetIdentifier[] m_RTIDs2 = new RenderTargetIdentifier[2];

		// Token: 0x04000746 RID: 1862
		protected RenderTargetIdentifier[] m_RTIDs3 = new RenderTargetIdentifier[3];

		// Token: 0x04000747 RID: 1863
		private MaterialPropertyBlock m_PropertyBlock = new MaterialPropertyBlock();
	}
}
