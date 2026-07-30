using System;
using System.Collections.Generic;
using UnityEngine.Experimental.Rendering;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x0200007F RID: 127
	internal class PlanarReflectionProbeCache
	{
		// Token: 0x06000515 RID: 1301 RVA: 0x0002C498 File Offset: 0x0002A698
		public PlanarReflectionProbeCache(RenderPipelineResources defaultResources, IBLFilterGGX iblFilter, int atlasResolution, GraphicsFormat probeFormat, bool isMipmaped)
		{
			this.m_ConvertTextureMaterial = CoreUtils.CreateEngineMaterial(defaultResources.shaders.blitCubeTextureFacePS);
			this.m_ConvertTextureMPB = new MaterialPropertyBlock();
			probeFormat = GraphicsFormat.R16G16B16A16_SFloat;
			this.m_ProbeSize = atlasResolution;
			this.m_TextureAtlas = new PowerOfTwoTextureAtlas(atlasResolution, 0, probeFormat, FilterMode.Point, "PlanarReflectionProbe Atlas", isMipmaped);
			this.m_IBLFilterGGX = iblFilter;
			this.m_PerformBC6HCompression = probeFormat == GraphicsFormat.RGB_BC6H_SFloat;
		}

		// Token: 0x06000516 RID: 1302 RVA: 0x0002C518 File Offset: 0x0002A718
		private void Initialize()
		{
			if (this.m_ConvolutionTargetTexture == null)
			{
				this.m_ConvolutionTargetTexture = new RenderTexture(this.m_ProbeSize, this.m_ProbeSize, 0, RenderTextureFormat.ARGBHalf);
				this.m_ConvolutionTargetTexture.hideFlags = HideFlags.HideAndDontSave;
				this.m_ConvolutionTargetTexture.dimension = TextureDimension.Tex2D;
				this.m_ConvolutionTargetTexture.useMipMap = true;
				this.m_ConvolutionTargetTexture.autoGenerateMips = false;
				this.m_ConvolutionTargetTexture.filterMode = FilterMode.Point;
				this.m_ConvolutionTargetTexture.name = CoreUtils.GetRenderTargetAutoName(this.m_ProbeSize, this.m_ProbeSize, 0, RenderTextureFormat.ARGBHalf, "PlanarReflectionConvolution", true, false, MSAASamples.None);
				this.m_ConvolutionTargetTexture.enableRandomWrite = true;
				this.m_ConvolutionTargetTexture.Create();
			}
			this.m_FrameProbeIndex = 0;
		}

		// Token: 0x06000517 RID: 1303 RVA: 0x0002C5D1 File Offset: 0x0002A7D1
		public void Release()
		{
			this.m_TextureAtlas.Release();
			CoreUtils.Destroy(this.m_TempRenderTexture);
			CoreUtils.Destroy(this.m_ConvolutionTargetTexture);
			this.m_ProbeBakingState = null;
			CoreUtils.Destroy(this.m_ConvertTextureMaterial);
		}

		// Token: 0x06000518 RID: 1304 RVA: 0x0002C606 File Offset: 0x0002A806
		public void NewFrame()
		{
			this.Initialize();
		}

		// Token: 0x06000519 RID: 1305 RVA: 0x0002C610 File Offset: 0x0002A810
		private void ConvertTexture(CommandBuffer cmd, Texture input, RenderTexture target)
		{
			this.m_ConvertTextureMPB.SetTexture(PlanarReflectionProbeCache.s_InputTexID, input);
			this.m_ConvertTextureMPB.SetFloat(PlanarReflectionProbeCache.s_LoDID, 0f);
			CoreUtils.SetRenderTarget(cmd, target, ClearFlag.None, Color.black, 0, CubemapFace.PositiveX, -1);
			CoreUtils.DrawFullScreen(cmd, this.m_ConvertTextureMaterial, this.m_ConvertTextureMPB, 0);
		}

		// Token: 0x0600051A RID: 1306 RVA: 0x0002C66C File Offset: 0x0002A86C
		private Texture ConvolveProbeTexture(CommandBuffer cmd, Texture texture, out Vector4 sourceScaleOffset)
		{
			Texture2D texture2D = texture as Texture2D;
			RenderTexture renderTexture = texture as RenderTexture;
			texture2D != null;
			if (renderTexture.dimension != TextureDimension.Tex2D)
			{
				Debug.LogError("Planar Realtime reflection probe should always be a 2D RenderTexture.");
				sourceScaleOffset = Vector4.zero;
				return null;
			}
			RenderTexture renderTexture2 = renderTexture;
			float num = (float)texture.width / (float)this.m_ConvolutionTargetTexture.width;
			float num2 = (float)texture.height / (float)this.m_ConvolutionTargetTexture.height;
			sourceScaleOffset = new Vector4(num, num2, 0f, 0f);
			this.m_IBLFilterGGX.FilterPlanarTexture(cmd, renderTexture2, this.m_ConvolutionTargetTexture);
			return this.m_ConvolutionTargetTexture;
		}

		// Token: 0x0600051B RID: 1307 RVA: 0x0002C710 File Offset: 0x0002A910
		public Vector4 FetchSlice(CommandBuffer cmd, Texture texture, out int fetchIndex)
		{
			Vector4 zero = Vector4.zero;
			int frameProbeIndex = this.m_FrameProbeIndex;
			this.m_FrameProbeIndex = frameProbeIndex + 1;
			fetchIndex = frameProbeIndex;
			if (this.m_TextureAtlas.IsCached(out zero, texture))
			{
				if ((this.NeedsUpdate(texture) || this.m_ProbeBakingState[zero] != PlanarReflectionProbeCache.ProbeFilteringState.Ready) && !this.UpdatePlanarTexture(cmd, texture, ref zero))
				{
					Debug.LogError("Can't convolve or update the planar reflection render target");
				}
			}
			else if (!this.UpdatePlanarTexture(cmd, texture, ref zero))
			{
				Debug.LogError("No more space in the planar reflection probe atlas. To solve this issue, increase the size of the Planar Reflection Probe Atlas in the HDRP settings.");
			}
			return zero;
		}

		// Token: 0x0600051C RID: 1308 RVA: 0x0002C790 File Offset: 0x0002A990
		private bool UpdatePlanarTexture(CommandBuffer cmd, Texture texture, ref Vector4 scaleOffset)
		{
			bool flag = false;
			using (new ProfilingScope(cmd, ProfilingSampler.Get<HDProfileId>(HDProfileId.ConvolvePlanarReflectionProbe)))
			{
				this.m_ProbeBakingState[scaleOffset] = PlanarReflectionProbeCache.ProbeFilteringState.Convolving;
				Vector4 vector;
				Texture texture2 = this.ConvolveProbeTexture(cmd, texture, out vector);
				if (texture2 == null)
				{
					return false;
				}
				if (this.m_PerformBC6HCompression)
				{
					throw new NotImplementedException("BC6H Support not implemented for PlanarReflectionProbeCache");
				}
				if (this.m_TextureAtlas.IsCached(out scaleOffset, texture))
				{
					flag = this.m_TextureAtlas.UpdateTexture(cmd, texture, texture2, ref scaleOffset, vector, true, true);
				}
				else
				{
					if (!this.m_TextureAtlas.AllocateTextureWithoutBlit(texture, texture.width, texture.height, ref scaleOffset))
					{
						return false;
					}
					this.m_TextureAtlas.BlitTexture(cmd, scaleOffset, texture2, vector, true, -1);
					flag = true;
				}
				this.m_ProbeBakingState[scaleOffset] = PlanarReflectionProbeCache.ProbeFilteringState.Ready;
			}
			return flag;
		}

		// Token: 0x0600051D RID: 1309 RVA: 0x00003B6D File Offset: 0x00001D6D
		public uint GetTextureHash(Texture texture)
		{
			return texture.updateCount;
		}

		// Token: 0x0600051E RID: 1310 RVA: 0x0002C884 File Offset: 0x0002AA84
		private bool NeedsUpdate(Texture texture)
		{
			uint textureHash = this.GetTextureHash(texture);
			int instanceID = texture.GetInstanceID();
			bool flag = false;
			uint num;
			if (!this.m_TextureHashes.TryGetValue(instanceID, out num) || num != textureHash)
			{
				this.m_TextureHashes[instanceID] = textureHash;
				flag = true;
			}
			return flag;
		}

		// Token: 0x0600051F RID: 1311 RVA: 0x0002C8C6 File Offset: 0x0002AAC6
		public Texture GetTexCache()
		{
			return this.m_TextureAtlas.AtlasTexture;
		}

		// Token: 0x06000520 RID: 1312 RVA: 0x0002C8D8 File Offset: 0x0002AAD8
		public void Clear(CommandBuffer cmd)
		{
			this.m_TextureAtlas.ResetAllocator();
			this.m_TextureAtlas.ClearTarget(cmd);
		}

		// Token: 0x06000521 RID: 1313 RVA: 0x0002C8F1 File Offset: 0x0002AAF1
		public void ClearAtlasAllocator()
		{
			this.m_TextureAtlas.ResetAllocator();
		}

		// Token: 0x06000522 RID: 1314 RVA: 0x0002C8FE File Offset: 0x0002AAFE
		internal static long GetApproxCacheSizeInByte(int nbElement, int atlasResolution, GraphicsFormat format)
		{
			return PowerOfTwoTextureAtlas.GetApproxCacheSizeInByte(nbElement, atlasResolution, true, format);
		}

		// Token: 0x06000523 RID: 1315 RVA: 0x0002C909 File Offset: 0x0002AB09
		internal static int GetMaxCacheSizeForWeightInByte(int weight, GraphicsFormat format)
		{
			return PowerOfTwoTextureAtlas.GetMaxCacheSizeForWeightInByte(weight, true, format);
		}

		// Token: 0x06000524 RID: 1316 RVA: 0x0002C914 File Offset: 0x0002AB14
		internal Vector4 GetAtlasDatas()
		{
			float num = Mathf.Pow(2f, (float)this.m_TextureAtlas.mipPadding) * 2f;
			return new Vector4((float)this.m_TextureAtlas.AtlasTexture.rt.width, num / (float)this.m_TextureAtlas.AtlasTexture.rt.width, 0f, 0f);
		}

		// Token: 0x0400053A RID: 1338
		internal static readonly int s_InputTexID = Shader.PropertyToID("_InputTex");

		// Token: 0x0400053B RID: 1339
		internal static readonly int s_LoDID = Shader.PropertyToID("_LoD");

		// Token: 0x0400053C RID: 1340
		internal static readonly int s_FaceIndexID = Shader.PropertyToID("_FaceIndex");

		// Token: 0x0400053D RID: 1341
		private int m_ProbeSize;

		// Token: 0x0400053E RID: 1342
		private IBLFilterGGX m_IBLFilterGGX;

		// Token: 0x0400053F RID: 1343
		private PowerOfTwoTextureAtlas m_TextureAtlas;

		// Token: 0x04000540 RID: 1344
		private RenderTexture m_TempRenderTexture;

		// Token: 0x04000541 RID: 1345
		private RenderTexture m_ConvolutionTargetTexture;

		// Token: 0x04000542 RID: 1346
		private Dictionary<Vector4, PlanarReflectionProbeCache.ProbeFilteringState> m_ProbeBakingState = new Dictionary<Vector4, PlanarReflectionProbeCache.ProbeFilteringState>();

		// Token: 0x04000543 RID: 1347
		private Material m_ConvertTextureMaterial;

		// Token: 0x04000544 RID: 1348
		private MaterialPropertyBlock m_ConvertTextureMPB;

		// Token: 0x04000545 RID: 1349
		private bool m_PerformBC6HCompression;

		// Token: 0x04000546 RID: 1350
		private Dictionary<int, uint> m_TextureHashes = new Dictionary<int, uint>();

		// Token: 0x04000547 RID: 1351
		private int m_FrameProbeIndex;

		// Token: 0x02000207 RID: 519
		private enum ProbeFilteringState
		{
			// Token: 0x04001387 RID: 4999
			Convolving,
			// Token: 0x04001388 RID: 5000
			Ready
		}
	}
}
