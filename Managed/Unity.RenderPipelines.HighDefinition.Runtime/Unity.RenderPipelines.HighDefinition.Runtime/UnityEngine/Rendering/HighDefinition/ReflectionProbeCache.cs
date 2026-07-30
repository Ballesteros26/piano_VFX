using System;
using UnityEngine.Experimental.Rendering;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x02000080 RID: 128
	internal class ReflectionProbeCache
	{
		// Token: 0x06000526 RID: 1318 RVA: 0x0002C9AC File Offset: 0x0002ABAC
		public ReflectionProbeCache(RenderPipelineResources defaultResources, IBLFilterBSDF[] iblFilterBSDFArray, int cacheSize, int probeSize, GraphicsFormat probeFormat, bool isMipmaped)
		{
			this.m_ConvertTextureMaterial = CoreUtils.CreateEngineMaterial(defaultResources.shaders.blitCubeTextureFacePS);
			this.m_ConvertTextureMPB = new MaterialPropertyBlock();
			this.m_CubeToPano = CoreUtils.CreateEngineMaterial(defaultResources.shaders.cubeToPanoPS);
			probeFormat = GraphicsFormat.R16G16B16A16_SFloat;
			this.m_ProbeSize = probeSize;
			this.m_CacheSize = cacheSize;
			this.m_TextureCache = new TextureCacheCubemap("ReflectionProbe", iblFilterBSDFArray.Length);
			this.m_TextureCache.AllocTextureArray(cacheSize, probeSize, probeFormat, isMipmaped, this.m_CubeToPano);
			this.m_IBLFilterBSDF = iblFilterBSDFArray;
			this.m_PerformBC6HCompression = probeFormat == GraphicsFormat.RGB_BC6H_SFloat;
			this.InitializeProbeBakingStates();
		}

		// Token: 0x06000527 RID: 1319 RVA: 0x0002CA50 File Offset: 0x0002AC50
		private void Initialize()
		{
			if (this.m_TempRenderTexture == null)
			{
				this.m_TempRenderTexture = new RenderTexture(this.m_ProbeSize, this.m_ProbeSize, 1, RenderTextureFormat.ARGBHalf);
				this.m_TempRenderTexture.hideFlags = HideFlags.HideAndDontSave;
				this.m_TempRenderTexture.dimension = TextureDimension.Cube;
				this.m_TempRenderTexture.useMipMap = true;
				this.m_TempRenderTexture.autoGenerateMips = false;
				this.m_TempRenderTexture.name = CoreUtils.GetRenderTargetAutoName(this.m_ProbeSize, this.m_ProbeSize, 1, RenderTextureFormat.ARGBHalf, "ReflectionProbeTemp", true, false, MSAASamples.None);
				this.m_TempRenderTexture.Create();
				this.m_ConvolutionTargetTextureArray = new RenderTexture[this.m_IBLFilterBSDF.Length];
				for (int i = 0; i < this.m_IBLFilterBSDF.Length; i++)
				{
					this.m_ConvolutionTargetTextureArray[i] = new RenderTexture(this.m_ProbeSize, this.m_ProbeSize, 1, RenderTextureFormat.ARGBHalf);
					this.m_ConvolutionTargetTextureArray[i].hideFlags = HideFlags.HideAndDontSave;
					this.m_ConvolutionTargetTextureArray[i].dimension = TextureDimension.Cube;
					this.m_ConvolutionTargetTextureArray[i].useMipMap = true;
					this.m_ConvolutionTargetTextureArray[i].autoGenerateMips = false;
					this.m_ConvolutionTargetTextureArray[i].name = CoreUtils.GetRenderTargetAutoName(this.m_ProbeSize, this.m_ProbeSize, 1, RenderTextureFormat.ARGBHalf, "ReflectionProbeConvolution_" + i.ToString(), true, false, MSAASamples.None);
					this.m_ConvolutionTargetTextureArray[i].Create();
				}
			}
			this.InitializeProbeBakingStates();
		}

		// Token: 0x06000528 RID: 1320 RVA: 0x0002CBB4 File Offset: 0x0002ADB4
		private void InitializeProbeBakingStates()
		{
			if (this.m_ProbeBakingState == null || this.m_ProbeBakingState.Length != this.m_CacheSize)
			{
				Array.Resize<ReflectionProbeCache.ProbeFilteringState>(ref this.m_ProbeBakingState, this.m_CacheSize);
				for (int i = 0; i < this.m_CacheSize; i++)
				{
					this.m_ProbeBakingState[i] = ReflectionProbeCache.ProbeFilteringState.Convolving;
				}
			}
		}

		// Token: 0x06000529 RID: 1321 RVA: 0x0002CC04 File Offset: 0x0002AE04
		public void Release()
		{
			this.m_TextureCache.Release();
			CoreUtils.Destroy(this.m_TempRenderTexture);
			if (this.m_ConvolutionTargetTextureArray != null)
			{
				for (int i = 0; i < this.m_IBLFilterBSDF.Length; i++)
				{
					if (this.m_ConvolutionTargetTextureArray[i] != null)
					{
						CoreUtils.Destroy(this.m_ConvolutionTargetTextureArray[i]);
						this.m_ConvolutionTargetTextureArray[i] = null;
					}
				}
			}
			this.m_ProbeBakingState = null;
			CoreUtils.Destroy(this.m_ConvertTextureMaterial);
			CoreUtils.Destroy(this.m_CubeToPano);
		}

		// Token: 0x0600052A RID: 1322 RVA: 0x0002CC85 File Offset: 0x0002AE85
		public void NewFrame()
		{
			this.Initialize();
			this.m_TextureCache.NewFrame();
		}

		// Token: 0x0600052B RID: 1323 RVA: 0x0002CC98 File Offset: 0x0002AE98
		private void ConvertTexture(CommandBuffer cmd, Texture input, RenderTexture target)
		{
			this.m_ConvertTextureMPB.SetTexture(HDShaderIDs._InputTex, input);
			this.m_ConvertTextureMPB.SetFloat(HDShaderIDs._LoD, 0f);
			for (int i = 0; i < 6; i++)
			{
				this.m_ConvertTextureMPB.SetFloat(HDShaderIDs._FaceIndex, (float)i);
				CoreUtils.SetRenderTarget(cmd, target, ClearFlag.None, Color.black, 0, (CubemapFace)i, -1);
				CoreUtils.DrawFullScreen(cmd, this.m_ConvertTextureMaterial, this.m_ConvertTextureMPB, 0);
			}
		}

		// Token: 0x0600052C RID: 1324 RVA: 0x0002CD14 File Offset: 0x0002AF14
		private Texture[] ConvolveProbeTexture(CommandBuffer cmd, Texture texture)
		{
			Cubemap cubemap = texture as Cubemap;
			RenderTexture renderTexture = texture as RenderTexture;
			RenderTexture renderTexture2;
			if (cubemap != null)
			{
				bool flag = cubemap.width != this.m_ProbeSize || cubemap.height != this.m_ProbeSize;
				if (cubemap.format != TextureFormat.RGBAHalf || flag)
				{
					if (!flag)
					{
						TextureFormat format = cubemap.format;
					}
					this.ConvertTexture(cmd, cubemap, this.m_TempRenderTexture);
				}
				else
				{
					for (int i = 0; i < 6; i++)
					{
						cmd.CopyTexture(cubemap, i, 0, this.m_TempRenderTexture, i, 0);
					}
				}
				cmd.GenerateMips(this.m_TempRenderTexture);
				renderTexture2 = this.m_TempRenderTexture;
			}
			else
			{
				if (renderTexture.dimension != TextureDimension.Cube)
				{
					Debug.LogError("Realtime reflection probe should always be a Cube RenderTexture.");
					return null;
				}
				if (renderTexture.width != this.m_ProbeSize || renderTexture.height != this.m_ProbeSize)
				{
					this.ConvertTexture(cmd, renderTexture, this.m_TempRenderTexture);
					renderTexture2 = this.m_TempRenderTexture;
				}
				else
				{
					renderTexture2 = renderTexture;
				}
				cmd.GenerateMips(renderTexture2);
			}
			for (int j = 0; j < this.m_IBLFilterBSDF.Length; j++)
			{
				this.m_IBLFilterBSDF[j].FilterCubemap(cmd, renderTexture2, this.m_ConvolutionTargetTextureArray[j]);
			}
			return this.m_ConvolutionTargetTextureArray;
		}

		// Token: 0x0600052D RID: 1325 RVA: 0x0002CE64 File Offset: 0x0002B064
		public int FetchSlice(CommandBuffer cmd, Texture texture)
		{
			bool flag;
			int num = this.m_TextureCache.ReserveSlice(texture, out flag);
			if (num != -1 && (flag || this.m_ProbeBakingState[num] != ReflectionProbeCache.ProbeFilteringState.Ready))
			{
				using (new ProfilingScope(cmd, ProfilingSampler.Get<HDProfileId>(HDProfileId.ConvolveReflectionProbe)))
				{
					this.m_ProbeBakingState[num] = ReflectionProbeCache.ProbeFilteringState.Convolving;
					Texture[] array = this.ConvolveProbeTexture(cmd, texture);
					if (array == null)
					{
						return -1;
					}
					if (this.m_PerformBC6HCompression)
					{
						cmd.BC6HEncodeFastCubemap(array[0], this.m_ProbeSize, this.m_TextureCache.GetTexCache(), 0, int.MaxValue, num);
						this.m_TextureCache.SetSliceHash(num, this.m_TextureCache.GetTextureHash(texture));
					}
					else
					{
						this.m_TextureCache.UpdateSlice(cmd, num, array, this.m_TextureCache.GetTextureHash(texture));
					}
					this.m_ProbeBakingState[num] = ReflectionProbeCache.ProbeFilteringState.Ready;
				}
				return num;
			}
			return num;
		}

		// Token: 0x0600052E RID: 1326 RVA: 0x0002CF5C File Offset: 0x0002B15C
		public Texture GetTexCache()
		{
			return this.m_TextureCache.GetTexCache();
		}

		// Token: 0x0600052F RID: 1327 RVA: 0x0002CF69 File Offset: 0x0002B169
		internal static long GetApproxCacheSizeInByte(int nbElement, int resolution, int sliceSize)
		{
			return TextureCacheCubemap.GetApproxCacheSizeInByte(nbElement, resolution, sliceSize);
		}

		// Token: 0x06000530 RID: 1328 RVA: 0x0002CF73 File Offset: 0x0002B173
		internal static int GetMaxCacheSizeForWeightInByte(int weight, int resolution, int sliceSize)
		{
			return TextureCacheCubemap.GetMaxCacheSizeForWeightInByte((long)weight, resolution, sliceSize);
		}

		// Token: 0x06000531 RID: 1329 RVA: 0x0002CF7E File Offset: 0x0002B17E
		public int GetEnvSliceSize()
		{
			return this.m_IBLFilterBSDF.Length;
		}

		// Token: 0x04000548 RID: 1352
		private int m_ProbeSize;

		// Token: 0x04000549 RID: 1353
		private int m_CacheSize;

		// Token: 0x0400054A RID: 1354
		private IBLFilterBSDF[] m_IBLFilterBSDF;

		// Token: 0x0400054B RID: 1355
		private TextureCacheCubemap m_TextureCache;

		// Token: 0x0400054C RID: 1356
		private RenderTexture m_TempRenderTexture;

		// Token: 0x0400054D RID: 1357
		private RenderTexture[] m_ConvolutionTargetTextureArray;

		// Token: 0x0400054E RID: 1358
		private ReflectionProbeCache.ProbeFilteringState[] m_ProbeBakingState;

		// Token: 0x0400054F RID: 1359
		private Material m_ConvertTextureMaterial;

		// Token: 0x04000550 RID: 1360
		private Material m_CubeToPano;

		// Token: 0x04000551 RID: 1361
		private MaterialPropertyBlock m_ConvertTextureMPB;

		// Token: 0x04000552 RID: 1362
		private bool m_PerformBC6HCompression;

		// Token: 0x02000208 RID: 520
		private enum ProbeFilteringState
		{
			// Token: 0x0400138A RID: 5002
			Convolving,
			// Token: 0x0400138B RID: 5003
			Ready
		}
	}
}
