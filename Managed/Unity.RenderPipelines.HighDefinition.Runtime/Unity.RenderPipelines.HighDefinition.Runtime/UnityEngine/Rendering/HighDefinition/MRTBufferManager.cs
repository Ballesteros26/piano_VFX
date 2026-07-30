using System;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x0200010B RID: 267
	internal abstract class MRTBufferManager
	{
		// Token: 0x17000153 RID: 339
		// (get) Token: 0x0600086F RID: 2159 RVA: 0x000457B6 File Offset: 0x000439B6
		public int bufferCount
		{
			get
			{
				return this.m_BufferCount;
			}
		}

		// Token: 0x06000870 RID: 2160 RVA: 0x000457BE File Offset: 0x000439BE
		public MRTBufferManager(int maxBufferCount)
		{
			this.m_BufferCount = maxBufferCount;
			this.m_RTIDs = new RenderTargetIdentifier[maxBufferCount];
			this.m_RTs = new RTHandle[maxBufferCount];
			this.m_TextureShaderIDs = new int[maxBufferCount];
		}

		// Token: 0x06000871 RID: 2161 RVA: 0x000457F4 File Offset: 0x000439F4
		public RenderTargetIdentifier[] GetBuffersRTI()
		{
			for (int i = 0; i < this.m_BufferCount; i++)
			{
				this.m_RTIDs[i] = this.m_RTs[i].nameID;
			}
			return this.m_RTIDs;
		}

		// Token: 0x06000872 RID: 2162 RVA: 0x00034721 File Offset: 0x00032921
		public RTHandle[] GetBuffers()
		{
			return this.m_RTs;
		}

		// Token: 0x06000873 RID: 2163 RVA: 0x00045831 File Offset: 0x00043A31
		public RTHandle GetBuffer(int index)
		{
			return this.m_RTs[index];
		}

		// Token: 0x06000874 RID: 2164
		public abstract void CreateBuffers();

		// Token: 0x06000875 RID: 2165 RVA: 0x0004583C File Offset: 0x00043A3C
		public virtual void BindBufferAsTextures(CommandBuffer cmd)
		{
			for (int i = 0; i < this.m_BufferCount; i++)
			{
				cmd.SetGlobalTexture(this.m_TextureShaderIDs[i], this.m_RTs[i]);
			}
		}

		// Token: 0x06000876 RID: 2166 RVA: 0x00045878 File Offset: 0x00043A78
		public virtual void DestroyBuffers()
		{
			for (int i = 0; i < this.m_BufferCount; i++)
			{
				RTHandles.Release(this.m_RTs[i]);
				this.m_RTs[i] = null;
			}
		}

		// Token: 0x04000D0D RID: 3341
		protected int m_BufferCount;

		// Token: 0x04000D0E RID: 3342
		protected RenderTargetIdentifier[] m_RTIDs;

		// Token: 0x04000D0F RID: 3343
		protected RTHandle[] m_RTs;

		// Token: 0x04000D10 RID: 3344
		protected int[] m_TextureShaderIDs;
	}
}
