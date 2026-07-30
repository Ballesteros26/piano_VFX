using System;

namespace UnityEngine.Experimental.Rendering.RenderGraphModule
{
	// Token: 0x0200000D RID: 13
	public struct RenderGraphBuilder : IDisposable
	{
		// Token: 0x06000043 RID: 67 RVA: 0x00002E7C File Offset: 0x0000107C
		public RenderGraphMutableResource UseColorBuffer(in RenderGraphMutableResource input, int index)
		{
			RenderGraphMutableResource renderGraphMutableResource = input;
			if (renderGraphMutableResource.type != RenderGraphResourceType.Texture)
			{
				throw new ArgumentException("Trying to write to a resource that is not a texture or is invalid.");
			}
			this.m_RenderPass.SetColorBuffer(in input, index);
			this.m_Resources.UpdateTextureFirstWrite(input, this.m_RenderPass.index);
			return input;
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002EDC File Offset: 0x000010DC
		public RenderGraphMutableResource UseDepthBuffer(in RenderGraphMutableResource input, DepthAccess flags)
		{
			RenderGraphMutableResource renderGraphMutableResource = input;
			if (renderGraphMutableResource.type != RenderGraphResourceType.Texture)
			{
				throw new ArgumentException("Trying to write to a resource that is not a texture or is invalid.");
			}
			this.m_RenderPass.SetDepthBuffer(in input, flags);
			if ((flags | DepthAccess.Read) != (DepthAccess)0)
			{
				this.m_Resources.UpdateTextureLastRead(input, this.m_RenderPass.index);
			}
			if ((flags | DepthAccess.Write) != (DepthAccess)0)
			{
				this.m_Resources.UpdateTextureFirstWrite(input, this.m_RenderPass.index);
			}
			return input;
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00002F64 File Offset: 0x00001164
		public RenderGraphResource ReadTexture(in RenderGraphResource input)
		{
			RenderGraphResource renderGraphResource = input;
			if (renderGraphResource.type != RenderGraphResourceType.Texture)
			{
				throw new ArgumentException("Trying to read a resource that is not a texture or is invalid.");
			}
			this.m_RenderPass.resourceReadList.Add(input);
			this.m_Resources.UpdateTextureLastRead(input, this.m_RenderPass.index);
			return input;
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002FC8 File Offset: 0x000011C8
		public RenderGraphMutableResource WriteTexture(in RenderGraphMutableResource input)
		{
			RenderGraphMutableResource renderGraphMutableResource = input;
			if (renderGraphMutableResource.type != RenderGraphResourceType.Texture)
			{
				throw new ArgumentException("Trying to write to a resource that is not a texture or is invalid.");
			}
			this.m_RenderPass.resourceWriteList.Add(input);
			this.m_Resources.UpdateTextureFirstWrite(input, this.m_RenderPass.index);
			return input;
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00003030 File Offset: 0x00001230
		public RenderGraphResource UseRendererList(in RenderGraphResource input)
		{
			RenderGraphResource renderGraphResource = input;
			if (renderGraphResource.type != RenderGraphResourceType.RendererList)
			{
				throw new ArgumentException("Trying use a resource that is not a renderer list.");
			}
			this.m_RenderPass.usedRendererListList.Add(input);
			return input;
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00003075 File Offset: 0x00001275
		public void SetRenderFunc<PassData>(RenderFunc<PassData> renderFunc) where PassData : class, new()
		{
			((RenderGraph.RenderPass<PassData>)this.m_RenderPass).renderFunc = renderFunc;
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00003088 File Offset: 0x00001288
		public void EnableAsyncCompute(bool value)
		{
			this.m_RenderPass.enableAsyncCompute = value;
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00003096 File Offset: 0x00001296
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x0600004B RID: 75 RVA: 0x0000309F File Offset: 0x0000129F
		internal RenderGraphBuilder(RenderGraph.RenderPass renderPass, RenderGraphResourceRegistry resources)
		{
			this.m_RenderPass = renderPass;
			this.m_Resources = resources;
			this.m_Disposed = false;
		}

		// Token: 0x0600004C RID: 76 RVA: 0x000030B6 File Offset: 0x000012B6
		private void Dispose(bool disposing)
		{
			if (this.m_Disposed)
			{
				return;
			}
			this.m_Disposed = true;
		}

		// Token: 0x04000035 RID: 53
		private RenderGraph.RenderPass m_RenderPass;

		// Token: 0x04000036 RID: 54
		private RenderGraphResourceRegistry m_Resources;

		// Token: 0x04000037 RID: 55
		private bool m_Disposed;
	}
}
