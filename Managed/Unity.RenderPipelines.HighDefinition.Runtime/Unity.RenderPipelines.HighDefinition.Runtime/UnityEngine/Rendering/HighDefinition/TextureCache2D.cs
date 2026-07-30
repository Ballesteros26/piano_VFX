using System;
using UnityEngine.Experimental.Rendering;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x02000022 RID: 34
	internal class TextureCache2D : TextureCache
	{
		// Token: 0x06000051 RID: 81 RVA: 0x00003F8D File Offset: 0x0000218D
		public TextureCache2D(string cacheName = "")
			: base(cacheName, 1)
		{
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00003F97 File Offset: 0x00002197
		private bool TextureHasMipmaps(Texture texture)
		{
			if (texture is Texture2D)
			{
				return ((Texture2D)texture).mipmapCount > 1;
			}
			return ((RenderTexture)texture).useMipMap;
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00003FBB File Offset: 0x000021BB
		public override bool IsCreated()
		{
			return this.m_Cache.IsCreated();
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00003FC8 File Offset: 0x000021C8
		protected override bool TransferToSlice(CommandBuffer cmd, int sliceIndex, Texture[] textureArray)
		{
			if (textureArray == null || (textureArray.Length == 0 && !(textureArray[0] is RenderTexture) && !(textureArray[0] is Texture2D)))
			{
				return false;
			}
			for (int i = 1; i < textureArray.Length; i++)
			{
				if (textureArray[i].width != textureArray[0].width || textureArray[i].height != textureArray[0].height || (!(textureArray[0] is RenderTexture) && !(textureArray[0] is Texture2D)))
				{
					Debug.LogWarning("All the sub-textures should have the same dimensions to be handled by the texture cache.");
					return false;
				}
			}
			bool flag = this.m_Cache.width != textureArray[0].width || this.m_Cache.height != textureArray[0].height;
			if (textureArray[0] is Texture2D)
			{
				flag |= this.m_Cache.graphicsFormat != (textureArray[0] as Texture2D).graphicsFormat;
			}
			for (int j = 0; j < textureArray.Length; j++)
			{
				if (!this.TextureHasMipmaps(textureArray[j]))
				{
					Debug.LogWarning("The texture '" + textureArray[j] + "' should have mipmaps to be handled by the cookie texture array");
				}
				if (flag)
				{
					cmd.Blit(textureArray[j], this.m_Cache, 0, this.m_SliceSize * sliceIndex + j);
				}
				else
				{
					cmd.CopyTexture(textureArray[j], 0, this.m_Cache, this.m_SliceSize * sliceIndex + j);
				}
			}
			return true;
		}

		// Token: 0x06000055 RID: 85 RVA: 0x0000411D File Offset: 0x0000231D
		public override Texture GetTexCache()
		{
			return this.m_Cache;
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00004128 File Offset: 0x00002328
		public bool AllocTextureArray(int numTextures, int width, int height, GraphicsFormat format, bool isMipMapped)
		{
			bool flag = base.AllocTextureArray(numTextures);
			this.m_NumMipLevels = base.GetNumMips(width, height);
			RenderTextureDescriptor renderTextureDescriptor = new RenderTextureDescriptor(width, width, format, 0)
			{
				dimension = TextureDimension.Tex2DArray,
				volumeDepth = numTextures,
				useMipMap = isMipMapped,
				msaaSamples = 1
			};
			this.m_Cache = new RenderTexture(renderTextureDescriptor)
			{
				hideFlags = HideFlags.HideAndDontSave,
				wrapMode = TextureWrapMode.Clamp,
				name = CoreUtils.GetTextureAutoName(width, height, format, TextureDimension.Tex2DArray, this.m_CacheName, false, numTextures)
			};
			this.ClearCache();
			this.m_Cache.Create();
			return flag;
		}

		// Token: 0x06000057 RID: 87 RVA: 0x000041C0 File Offset: 0x000023C0
		internal void ClearCache()
		{
			RenderTextureDescriptor descriptor = this.m_Cache.descriptor;
			int num = (descriptor.useMipMap ? base.GetNumMips(descriptor.width, descriptor.height) : 1);
			for (int i = 0; i < descriptor.volumeDepth; i++)
			{
				for (int j = 0; j < num; j++)
				{
					Graphics.SetRenderTarget(this.m_Cache, j, CubemapFace.Unknown, i);
					GL.Clear(false, true, Color.clear);
				}
			}
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00004232 File Offset: 0x00002432
		public void Release()
		{
			CoreUtils.Destroy(this.m_Cache);
		}

		// Token: 0x06000059 RID: 89 RVA: 0x0000423F File Offset: 0x0000243F
		internal static long GetApproxCacheSizeInByte(int nbElement, int resolution, int sliceSize)
		{
			return (long)((float)((long)nbElement * (long)resolution * (long)resolution * 2L * 4L) * 1.33f * (float)sliceSize);
		}

		// Token: 0x0600005A RID: 90 RVA: 0x0000425A File Offset: 0x0000245A
		internal static int GetMaxCacheSizeForWeightInByte(int weight, int resolution, int sliceSize)
		{
			return Mathf.Clamp(Mathf.FloorToInt((float)weight / ((float)((long)resolution * (long)resolution * 2L * 4L) * 1.33f * (float)sliceSize)), 1, 250);
		}

		// Token: 0x0400008D RID: 141
		private RenderTexture m_Cache;
	}
}
