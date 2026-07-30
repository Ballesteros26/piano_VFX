using System;
using UnityEngine.Experimental.Rendering;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x02000023 RID: 35
	internal class TextureCacheCubemap : TextureCache
	{
		// Token: 0x0600005B RID: 91 RVA: 0x00004284 File Offset: 0x00002484
		public TextureCacheCubemap(string cacheName = "", int sliceSize = 1)
			: base(cacheName, sliceSize)
		{
			RenderPipelineResources renderPipelineResources = HDRenderPipeline.defaultAsset.renderPipelineResources;
			this.m_BlitCubemapFaceMaterial = CoreUtils.CreateEngineMaterial(renderPipelineResources.shaders.blitCubeTextureFacePS);
			this.m_BlitCubemapFaceProperties = new MaterialPropertyBlock();
		}

		// Token: 0x0600005C RID: 92 RVA: 0x000042C5 File Offset: 0x000024C5
		public override bool IsCreated()
		{
			return this.m_Cache.IsCreated();
		}

		// Token: 0x0600005D RID: 93 RVA: 0x000042D4 File Offset: 0x000024D4
		protected override bool TransferToSlice(CommandBuffer cmd, int sliceIndex, Texture[] textureArray)
		{
			if (!TextureCache.supportsCubemapArrayTextures)
			{
				return this.TransferToPanoCache(cmd, sliceIndex, textureArray);
			}
			if (textureArray == null || textureArray.Length == 0)
			{
				return false;
			}
			for (int i = 1; i < textureArray.Length; i++)
			{
				if (textureArray[i].width != textureArray[0].width || textureArray[i].height != textureArray[0].height)
				{
					Debug.LogWarning("All the sub-textures should have the same dimensions to be handled by the texture cache.");
					return false;
				}
			}
			bool flag = this.m_Cache.width != textureArray[0].width || this.m_Cache.height != textureArray[0].height;
			if (textureArray[0] is Cubemap)
			{
				flag |= this.m_Cache.graphicsFormat != (textureArray[0] as Cubemap).graphicsFormat;
			}
			for (int j = 0; j < textureArray.Length; j++)
			{
				if (flag)
				{
					this.m_BlitCubemapFaceProperties.SetTexture(HDShaderIDs._InputTex, textureArray[j]);
					this.m_BlitCubemapFaceProperties.SetFloat(HDShaderIDs._LoD, 0f);
					for (int k = 0; k < 6; k++)
					{
						this.m_BlitCubemapFaceProperties.SetFloat(HDShaderIDs._FaceIndex, (float)k);
						CoreUtils.SetRenderTarget(cmd, this.m_Cache, ClearFlag.None, Color.black, 0, CubemapFace.Unknown, 6 * (this.m_SliceSize * sliceIndex + j) + k);
						CoreUtils.DrawFullScreen(cmd, this.m_BlitCubemapFaceMaterial, this.m_BlitCubemapFaceProperties, 0);
					}
				}
				else
				{
					for (int l = 0; l < 6; l++)
					{
						cmd.CopyTexture(textureArray[j], l, this.m_Cache, 6 * (this.m_SliceSize * sliceIndex + j) + l);
					}
				}
			}
			return true;
		}

		// Token: 0x0600005E RID: 94 RVA: 0x0000446B File Offset: 0x0000266B
		public override Texture GetTexCache()
		{
			if (TextureCache.supportsCubemapArrayTextures)
			{
				return this.m_Cache;
			}
			return this.m_CacheNoCubeArray;
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00004484 File Offset: 0x00002684
		public bool AllocTextureArray(int numCubeMaps, int width, GraphicsFormat format, bool isMipMapped, Material cubeBlitMaterial)
		{
			bool flag = base.AllocTextureArray(numCubeMaps);
			this.m_NumMipLevels = base.GetNumMips(width, width);
			if (!TextureCache.supportsCubemapArrayTextures)
			{
				this.m_CubeBlitMaterial = cubeBlitMaterial;
				int num = 4 * width;
				int num2 = 2 * width;
				this.m_CacheNoCubeArray = new Texture2DArray(num, num2, numCubeMaps, TextureFormat.RGBAHalf, isMipMapped)
				{
					hideFlags = HideFlags.HideAndDontSave,
					wrapMode = TextureWrapMode.Repeat,
					wrapModeV = TextureWrapMode.Clamp,
					filterMode = FilterMode.Trilinear,
					anisoLevel = 0,
					name = CoreUtils.GetTextureAutoName(num, num2, TextureFormat.RGBAHalf, TextureDimension.Tex2DArray, this.m_CacheName, false, numCubeMaps)
				};
				this.m_NumPanoMipLevels = (isMipMapped ? base.GetNumMips(num, num2) : 1);
				this.m_StagingRTs = new RenderTexture[this.m_NumPanoMipLevels];
				for (int i = 0; i < this.m_NumPanoMipLevels; i++)
				{
					this.m_StagingRTs[i] = new RenderTexture(Mathf.Max(1, num >> i), Mathf.Max(1, num2 >> i), 0, RenderTextureFormat.ARGBHalf)
					{
						hideFlags = HideFlags.HideAndDontSave
					};
					this.m_StagingRTs[i].name = CoreUtils.GetRenderTargetAutoName(Mathf.Max(1, num >> i), Mathf.Max(1, num2 >> i), 1, RenderTextureFormat.ARGBHalf, string.Format("PanaCache{0}", i), false, false, MSAASamples.None);
				}
				if (this.m_CubeBlitMaterial)
				{
					this.m_CubeMipLevelPropName = Shader.PropertyToID("_cubeMipLvl");
					this.m_cubeSrcTexPropName = Shader.PropertyToID("_srcCubeTexture");
				}
			}
			else
			{
				RenderTextureDescriptor renderTextureDescriptor = new RenderTextureDescriptor(width, width, format, 0)
				{
					dimension = TextureDimension.CubeArray,
					volumeDepth = numCubeMaps * 6,
					autoGenerateMips = false,
					useMipMap = isMipMapped,
					msaaSamples = 1
				};
				this.m_Cache = new RenderTexture(renderTextureDescriptor)
				{
					hideFlags = HideFlags.HideAndDontSave,
					wrapMode = TextureWrapMode.Clamp,
					filterMode = FilterMode.Trilinear,
					anisoLevel = 0,
					name = CoreUtils.GetTextureAutoName(width, width, format, renderTextureDescriptor.dimension, this.m_CacheName, isMipMapped, numCubeMaps)
				};
				this.ClearCache();
				this.m_Cache.Create();
			}
			return flag;
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00004690 File Offset: 0x00002890
		internal void ClearCache()
		{
			RenderTextureDescriptor descriptor = this.m_Cache.descriptor;
			int num = (descriptor.useMipMap ? base.GetNumMips(descriptor.width, descriptor.height) : 1);
			for (int i = 0; i < descriptor.volumeDepth; i++)
			{
				for (int j = 0; j < num; j++)
				{
					for (int k = 0; k < 6; k++)
					{
						Graphics.SetRenderTarget(this.m_Cache, j, (CubemapFace)k, i);
						GL.Clear(false, true, Color.clear);
					}
				}
			}
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00004714 File Offset: 0x00002914
		public void Release()
		{
			if (this.m_CacheNoCubeArray)
			{
				CoreUtils.Destroy(this.m_CacheNoCubeArray);
				for (int i = 0; i < this.m_NumPanoMipLevels; i++)
				{
					this.m_StagingRTs[i].Release();
				}
				this.m_StagingRTs = null;
				CoreUtils.Destroy(this.m_CubeBlitMaterial);
			}
			this.m_Cache.Release();
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00004774 File Offset: 0x00002974
		private bool TransferToPanoCache(CommandBuffer cmd, int sliceIndex, Texture[] textureArray)
		{
			for (int i = 0; i < textureArray.Length; i++)
			{
				this.m_CubeBlitMaterial.SetTexture(this.m_cubeSrcTexPropName, textureArray[i]);
				for (int j = 0; j < this.m_NumPanoMipLevels; j++)
				{
					this.m_CubeBlitMaterial.SetInt(this.m_CubeMipLevelPropName, Mathf.Min(this.m_NumMipLevels - 1, j));
					cmd.Blit(null, this.m_StagingRTs[j], this.m_CubeBlitMaterial, 0);
				}
				for (int k = 0; k < this.m_NumPanoMipLevels; k++)
				{
					cmd.CopyTexture(this.m_StagingRTs[k], 0, 0, this.m_CacheNoCubeArray, this.m_SliceSize * sliceIndex + i, k);
				}
			}
			return true;
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00004831 File Offset: 0x00002A31
		internal static long GetApproxCacheSizeInByte(int nbElement, int resolution, int sliceSize)
		{
			return (long)((float)((long)nbElement * (long)resolution * (long)resolution * 6L * 2L * 4L) * 1.33f * (float)sliceSize);
		}

		// Token: 0x06000064 RID: 100 RVA: 0x0000484F File Offset: 0x00002A4F
		internal static int GetMaxCacheSizeForWeightInByte(long weight, int resolution, int sliceSize)
		{
			return Mathf.Clamp(Mathf.FloorToInt((float)weight / ((float)((long)resolution * (long)resolution * 6L * 2L * 4L) * 1.33f * (float)sliceSize)), 1, 250);
		}

		// Token: 0x0400008E RID: 142
		private RenderTexture m_Cache;

		// Token: 0x0400008F RID: 143
		private const int k_NbFace = 6;

		// Token: 0x04000090 RID: 144
		private Texture2DArray m_CacheNoCubeArray;

		// Token: 0x04000091 RID: 145
		private RenderTexture[] m_StagingRTs;

		// Token: 0x04000092 RID: 146
		private int m_NumPanoMipLevels;

		// Token: 0x04000093 RID: 147
		private Material m_CubeBlitMaterial;

		// Token: 0x04000094 RID: 148
		private int m_CubeMipLevelPropName;

		// Token: 0x04000095 RID: 149
		private int m_cubeSrcTexPropName;

		// Token: 0x04000096 RID: 150
		private Material m_BlitCubemapFaceMaterial;

		// Token: 0x04000097 RID: 151
		private MaterialPropertyBlock m_BlitCubemapFaceProperties;
	}
}
