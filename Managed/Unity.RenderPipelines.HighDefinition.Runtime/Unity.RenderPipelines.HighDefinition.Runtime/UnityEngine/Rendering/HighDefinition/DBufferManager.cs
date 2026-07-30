using System;
using UnityEngine.Experimental.Rendering;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x020000AC RID: 172
	internal class DBufferManager : MRTBufferManager
	{
		// Token: 0x0600065C RID: 1628 RVA: 0x00034714 File Offset: 0x00032914
		public DBufferManager()
			: base(Decal.GetMaterialDBufferCount())
		{
		}

		// Token: 0x0600065D RID: 1629 RVA: 0x00034721 File Offset: 0x00032921
		public RTHandle[] GetRTHandles()
		{
			return this.m_RTs;
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x0600065E RID: 1630 RVA: 0x00034729 File Offset: 0x00032929
		public ComputeBuffer propertyMaskBuffer
		{
			get
			{
				return this.m_PropertyMaskBuffer;
			}
		}

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x0600065F RID: 1631 RVA: 0x00034731 File Offset: 0x00032931
		public int clearPropertyMaskBufferKernel
		{
			get
			{
				return this.m_ClearPropertyMaskBufferKernel;
			}
		}

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x06000660 RID: 1632 RVA: 0x00034739 File Offset: 0x00032939
		public ComputeShader clearPropertyMaskBufferShader
		{
			get
			{
				return this.m_ClearPropertyMaskBufferShader;
			}
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x06000661 RID: 1633 RVA: 0x00034741 File Offset: 0x00032941
		public int propertyMaskBufferSize
		{
			get
			{
				return this.m_PropertyMaskBufferSize;
			}
		}

		// Token: 0x06000662 RID: 1634 RVA: 0x0003474C File Offset: 0x0003294C
		public override void CreateBuffers()
		{
			GraphicsFormat[] array;
			Decal.GetMaterialDBufferDescription(out array);
			for (int i = 0; i < this.m_BufferCount; i++)
			{
				this.m_RTs[i] = RTHandles.Alloc(Vector2.one, TextureXR.slices, DepthBits.None, array[i], FilterMode.Point, TextureWrapMode.Repeat, TextureXR.dimension, false, false, true, false, 1, 0f, false, false, true, RenderTextureMemoryless.None, string.Format("DBuffer{0}", i));
				this.m_RTIDs[i] = this.m_RTs[i].nameID;
				this.m_TextureShaderIDs[i] = HDShaderIDs._DBufferTexture[i];
			}
		}

		// Token: 0x06000663 RID: 1635 RVA: 0x000347D9 File Offset: 0x000329D9
		public void InitializeHDRPResouces(HDRenderPipelineAsset asset)
		{
			this.m_ClearPropertyMaskBufferShader = asset.renderPipelineResources.shaders.decalClearPropertyMaskBufferCS;
			this.m_ClearPropertyMaskBufferKernel = this.m_ClearPropertyMaskBufferShader.FindKernel("CSMain");
		}

		// Token: 0x06000664 RID: 1636 RVA: 0x00034807 File Offset: 0x00032A07
		public void ReleaseResolutionDependentBuffers()
		{
			if (this.m_PropertyMaskBuffer != null)
			{
				this.m_PropertyMaskBuffer.Dispose();
				this.m_PropertyMaskBuffer = null;
			}
		}

		// Token: 0x06000665 RID: 1637 RVA: 0x00034823 File Offset: 0x00032A23
		public void AllocResolutionDependentBuffers(HDCamera hdCamera, int width, int height)
		{
			this.m_PropertyMaskBufferSize = (width + 7) / 8 * ((height + 7) / 8);
			this.m_PropertyMaskBufferSize = (this.m_PropertyMaskBufferSize + 63) / 64 * 64;
			this.m_PropertyMaskBuffer = new ComputeBuffer(this.m_PropertyMaskBufferSize, 4);
		}

		// Token: 0x06000666 RID: 1638 RVA: 0x0003485D File Offset: 0x00032A5D
		public override void DestroyBuffers()
		{
			base.DestroyBuffers();
			this.ReleaseResolutionDependentBuffers();
		}

		// Token: 0x06000667 RID: 1639 RVA: 0x0003486C File Offset: 0x00032A6C
		public void BindBlackTextures(CommandBuffer cmd)
		{
			for (int i = 0; i < this.m_BufferCount; i++)
			{
				cmd.SetGlobalTexture(this.m_TextureShaderIDs[i], TextureXR.GetBlackTexture());
			}
		}

		// Token: 0x040006B3 RID: 1715
		private ComputeBuffer m_PropertyMaskBuffer;

		// Token: 0x040006B4 RID: 1716
		private int m_PropertyMaskBufferSize;

		// Token: 0x040006B5 RID: 1717
		private ComputeShader m_ClearPropertyMaskBufferShader;

		// Token: 0x040006B6 RID: 1718
		private int m_ClearPropertyMaskBufferKernel;
	}
}
