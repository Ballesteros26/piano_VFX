using System;
using System.Collections.Generic;
using UnityEngine.Experimental.Rendering;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x0200014F RID: 335
	internal class Texture2DAtlas
	{
		// Token: 0x1700017B RID: 379
		// (get) Token: 0x060009C7 RID: 2503 RVA: 0x0004D91F File Offset: 0x0004BB1F
		public RTHandle AtlasTexture
		{
			get
			{
				return this.m_AtlasTexture;
			}
		}

		// Token: 0x060009C8 RID: 2504 RVA: 0x0004D928 File Offset: 0x0004BB28
		public Texture2DAtlas(int width, int height, GraphicsFormat format, FilterMode filterMode = FilterMode.Point, bool powerOfTwoPadding = false, string name = "", bool useMipMap = true)
		{
			this.m_Width = width;
			this.m_Height = height;
			this.m_Format = format;
			this.m_UseMipMaps = useMipMap;
			this.m_AtlasTexture = RTHandles.Alloc(this.m_Width, this.m_Height, 1, DepthBits.None, this.m_Format, filterMode, TextureWrapMode.Clamp, TextureDimension.Tex2D, false, useMipMap, false, false, 1, 0f, MSAASamples.None, false, false, RenderTextureMemoryless.None, name);
			int num = (useMipMap ? this.GetTextureMipmapCount(this.m_Width, this.m_Height) : 1);
			for (int i = 0; i < num; i++)
			{
				Graphics.SetRenderTarget(this.m_AtlasTexture, i);
				GL.Clear(false, true, Color.clear);
			}
			this.m_AtlasAllocator = new AtlasAllocator(width, height, powerOfTwoPadding);
		}

		// Token: 0x060009C9 RID: 2505 RVA: 0x0004D9F6 File Offset: 0x0004BBF6
		public void Release()
		{
			this.ResetAllocator();
			RTHandles.Release(this.m_AtlasTexture);
		}

		// Token: 0x060009CA RID: 2506 RVA: 0x0004DA09 File Offset: 0x0004BC09
		public void ResetAllocator()
		{
			this.m_AtlasAllocator.Reset();
			this.m_AllocationCache.Clear();
			this.m_IsGPUTextureUpToDate.Clear();
		}

		// Token: 0x060009CB RID: 2507 RVA: 0x0004DA2C File Offset: 0x0004BC2C
		public void ClearTarget(CommandBuffer cmd)
		{
			int num = (this.m_UseMipMaps ? this.GetTextureMipmapCount(this.m_Width, this.m_Height) : 1);
			for (int i = 0; i < num; i++)
			{
				cmd.SetRenderTarget(this.m_AtlasTexture, i);
				HDUtils.BlitQuad(cmd, Texture2D.blackTexture, Texture2DAtlas.fullScaleOffset, Texture2DAtlas.fullScaleOffset, i, true);
			}
			this.m_IsGPUTextureUpToDate.Clear();
		}

		// Token: 0x060009CC RID: 2508 RVA: 0x0004DA97 File Offset: 0x0004BC97
		protected int GetTextureMipmapCount(int width, int height)
		{
			if (!this.m_UseMipMaps)
			{
				return 1;
			}
			return Mathf.FloorToInt(Mathf.Log((float)Mathf.Max(width, height), 2f)) + 1;
		}

		// Token: 0x060009CD RID: 2509 RVA: 0x0004DABC File Offset: 0x0004BCBC
		protected bool Is2D(Texture texture)
		{
			RenderTexture renderTexture = texture as RenderTexture;
			return texture is Texture2D || (renderTexture != null && renderTexture.dimension == TextureDimension.Tex2D);
		}

		// Token: 0x060009CE RID: 2510 RVA: 0x0004DAE8 File Offset: 0x0004BCE8
		protected void Blit2DTexture(CommandBuffer cmd, Vector4 scaleOffset, Texture texture, Vector4 sourceScaleOffset, bool blitMips = true)
		{
			int num = this.GetTextureMipmapCount(texture.width, texture.height);
			if (!blitMips)
			{
				num = 1;
			}
			for (int i = 0; i < num; i++)
			{
				cmd.SetRenderTarget(this.m_AtlasTexture, i);
				HDUtils.BlitQuad(cmd, texture, sourceScaleOffset, scaleOffset, i, true);
			}
		}

		// Token: 0x060009CF RID: 2511 RVA: 0x0004DB38 File Offset: 0x0004BD38
		protected void MarkGPUTextureValid(int instanceId, bool mipAreValid = false)
		{
			this.m_IsGPUTextureUpToDate[instanceId] = (mipAreValid ? 2U : 1U);
		}

		// Token: 0x060009D0 RID: 2512 RVA: 0x0004DB4D File Offset: 0x0004BD4D
		protected void MarkGPUTextureInvalid(int instanceId)
		{
			this.m_IsGPUTextureUpToDate[instanceId] = 0U;
		}

		// Token: 0x060009D1 RID: 2513 RVA: 0x0004DB5C File Offset: 0x0004BD5C
		public virtual void BlitTexture(CommandBuffer cmd, Vector4 scaleOffset, Texture texture, Vector4 sourceScaleOffset, bool blitMips = true, int overrideInstanceID = -1)
		{
			if (this.Is2D(texture))
			{
				this.Blit2DTexture(cmd, scaleOffset, texture, sourceScaleOffset, blitMips);
			}
		}

		// Token: 0x060009D2 RID: 2514 RVA: 0x0004DB74 File Offset: 0x0004BD74
		public virtual bool AllocateTexture(CommandBuffer cmd, ref Vector4 scaleOffset, Texture texture, int width, int height, int overrideInstanceID = -1)
		{
			bool flag = this.AllocateTextureWithoutBlit(texture, width, height, ref scaleOffset);
			if (flag)
			{
				this.BlitTexture(cmd, scaleOffset, texture, Texture2DAtlas.fullScaleOffset, true, -1);
				this.MarkGPUTextureValid((overrideInstanceID != -1) ? overrideInstanceID : texture.GetInstanceID(), true);
			}
			return flag;
		}

		// Token: 0x060009D3 RID: 2515 RVA: 0x0004DBB0 File Offset: 0x0004BDB0
		public bool AllocateTextureWithoutBlit(Texture texture, int width, int height, ref Vector4 scaleOffset)
		{
			return this.AllocateTextureWithoutBlit(texture.GetInstanceID(), width, height, ref scaleOffset);
		}

		// Token: 0x060009D4 RID: 2516 RVA: 0x0004DBC4 File Offset: 0x0004BDC4
		public virtual bool AllocateTextureWithoutBlit(int instanceId, int width, int height, ref Vector4 scaleOffset)
		{
			scaleOffset = Vector4.zero;
			if (this.m_AtlasAllocator.Allocate(ref scaleOffset, width, height))
			{
				scaleOffset.Scale(new Vector4(1f / (float)this.m_Width, 1f / (float)this.m_Height, 1f / (float)this.m_Width, 1f / (float)this.m_Height));
				this.m_AllocationCache.Add(instanceId, scaleOffset);
				this.MarkGPUTextureInvalid(instanceId);
				return true;
			}
			return false;
		}

		// Token: 0x060009D5 RID: 2517 RVA: 0x0004DC4B File Offset: 0x0004BE4B
		public bool IsCached(out Vector4 scaleOffset, Texture texture)
		{
			return this.m_AllocationCache.TryGetValue(texture.GetInstanceID(), out scaleOffset);
		}

		// Token: 0x060009D6 RID: 2518 RVA: 0x0004DC60 File Offset: 0x0004BE60
		public virtual bool NeedsUpdate(Texture texture, bool needMips = false)
		{
			RenderTexture renderTexture = texture as RenderTexture;
			int instanceID = texture.GetInstanceID();
			uint num2;
			if (renderTexture != null)
			{
				uint num;
				if (this.m_IsGPUTextureUpToDate.TryGetValue(instanceID, out num))
				{
					this.m_IsGPUTextureUpToDate[instanceID] = renderTexture.updateCount;
					if (renderTexture.updateCount != num)
					{
						return true;
					}
				}
				else
				{
					this.m_IsGPUTextureUpToDate[instanceID] = renderTexture.updateCount;
				}
			}
			else if (this.m_IsGPUTextureUpToDate.TryGetValue(instanceID, out num2))
			{
				return num2 == 0U || (needMips && num2 == 1U);
			}
			return false;
		}

		// Token: 0x060009D7 RID: 2519 RVA: 0x0004DCE5 File Offset: 0x0004BEE5
		public virtual bool AddTexture(CommandBuffer cmd, ref Vector4 scaleOffset, Texture texture)
		{
			return this.IsCached(out scaleOffset, texture) || (this.Is2D(texture) && this.AllocateTexture(cmd, ref scaleOffset, texture, texture.width, texture.height, -1));
		}

		// Token: 0x060009D8 RID: 2520 RVA: 0x0004DD14 File Offset: 0x0004BF14
		public virtual bool UpdateTexture(CommandBuffer cmd, Texture oldTexture, Texture newTexture, ref Vector4 scaleOffset, Vector4 sourceScaleOffset, bool updateIfNeeded = true, bool blitMips = true)
		{
			if (this.IsCached(out scaleOffset, oldTexture))
			{
				if (updateIfNeeded && this.NeedsUpdate(newTexture, false))
				{
					this.BlitTexture(cmd, scaleOffset, newTexture, sourceScaleOffset, blitMips, -1);
					this.MarkGPUTextureValid(newTexture.GetInstanceID(), blitMips);
				}
				return true;
			}
			return this.AllocateTexture(cmd, ref scaleOffset, newTexture, newTexture.width, newTexture.height, -1);
		}

		// Token: 0x060009D9 RID: 2521 RVA: 0x0004DD75 File Offset: 0x0004BF75
		public virtual bool UpdateTexture(CommandBuffer cmd, Texture texture, ref Vector4 scaleOffset, bool updateIfNeeded = true, bool blitMips = true)
		{
			return this.UpdateTexture(cmd, texture, texture, ref scaleOffset, Texture2DAtlas.fullScaleOffset, updateIfNeeded, blitMips);
		}

		// Token: 0x04000F2B RID: 3883
		protected RTHandle m_AtlasTexture;

		// Token: 0x04000F2C RID: 3884
		protected int m_Width;

		// Token: 0x04000F2D RID: 3885
		protected int m_Height;

		// Token: 0x04000F2E RID: 3886
		protected bool m_UseMipMaps;

		// Token: 0x04000F2F RID: 3887
		protected GraphicsFormat m_Format;

		// Token: 0x04000F30 RID: 3888
		private AtlasAllocator m_AtlasAllocator;

		// Token: 0x04000F31 RID: 3889
		private Dictionary<int, Vector4> m_AllocationCache = new Dictionary<int, Vector4>();

		// Token: 0x04000F32 RID: 3890
		private Dictionary<int, uint> m_IsGPUTextureUpToDate = new Dictionary<int, uint>();

		// Token: 0x04000F33 RID: 3891
		private static readonly Vector4 fullScaleOffset = new Vector4(1f, 1f, 0f, 0f);

		// Token: 0x04000F34 RID: 3892
		public static readonly int maxMipLevelPadding = 10;
	}
}
