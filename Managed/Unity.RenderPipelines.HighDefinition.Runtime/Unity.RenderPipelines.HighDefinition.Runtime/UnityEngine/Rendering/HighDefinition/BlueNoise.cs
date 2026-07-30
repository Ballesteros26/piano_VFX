using System;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x0200014B RID: 331
	public sealed class BlueNoise
	{
		// Token: 0x17000171 RID: 369
		// (get) Token: 0x06000973 RID: 2419 RVA: 0x0004C195 File Offset: 0x0004A395
		public Texture2D[] textures16L
		{
			get
			{
				return this.m_Textures16L;
			}
		}

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x06000974 RID: 2420 RVA: 0x0004C19D File Offset: 0x0004A39D
		public Texture2D[] textures16RGB
		{
			get
			{
				return this.m_Textures16RGB;
			}
		}

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x06000975 RID: 2421 RVA: 0x0004C1A5 File Offset: 0x0004A3A5
		public Texture2DArray textureArray16L
		{
			get
			{
				return this.m_TextureArray16L;
			}
		}

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x06000976 RID: 2422 RVA: 0x0004C1AD File Offset: 0x0004A3AD
		public Texture2DArray textureArray16RGB
		{
			get
			{
				return this.m_TextureArray16RGB;
			}
		}

		// Token: 0x06000977 RID: 2423 RVA: 0x0004C1B8 File Offset: 0x0004A3B8
		internal BlueNoise(RenderPipelineResources resources)
		{
			this.m_RenderPipelineResources = resources;
			BlueNoise.InitTextures(16, TextureFormat.Alpha8, resources.textures.blueNoise16LTex, out this.m_Textures16L, out this.m_TextureArray16L);
			BlueNoise.InitTextures(16, TextureFormat.RGB24, resources.textures.blueNoise16RGBTex, out this.m_Textures16RGB, out this.m_TextureArray16RGB);
		}

		// Token: 0x06000978 RID: 2424 RVA: 0x0004C210 File Offset: 0x0004A410
		public void Cleanup()
		{
			CoreUtils.Destroy(this.m_TextureArray16L);
			CoreUtils.Destroy(this.m_TextureArray16RGB);
			this.m_TextureArray16L = null;
			this.m_TextureArray16RGB = null;
		}

		// Token: 0x06000979 RID: 2425 RVA: 0x0004C236 File Offset: 0x0004A436
		public Texture2D GetRandom16L()
		{
			return this.textures16L[(int)(BlueNoise.m_Random.NextDouble() * (double)(this.textures16L.Length - 1))];
		}

		// Token: 0x0600097A RID: 2426 RVA: 0x0004C256 File Offset: 0x0004A456
		public Texture2D GetRandom16RGB()
		{
			return this.textures16RGB[(int)(BlueNoise.m_Random.NextDouble() * (double)(this.textures16RGB.Length - 1))];
		}

		// Token: 0x0600097B RID: 2427 RVA: 0x0004C278 File Offset: 0x0004A478
		private static void InitTextures(int size, TextureFormat format, Texture2D[] sourceTextures, out Texture2D[] destination, out Texture2DArray destinationArray)
		{
			int num = sourceTextures.Length;
			destination = new Texture2D[num];
			destinationArray = new Texture2DArray(size, size, num, format, false, true);
			destinationArray.hideFlags = HideFlags.HideAndDontSave;
			for (int i = 0; i < num; i++)
			{
				Texture2D texture2D = sourceTextures[i];
				if (texture2D == null)
				{
					destination[i] = Texture2D.whiteTexture;
				}
				else
				{
					destination[i] = texture2D;
					Graphics.CopyTexture(texture2D, 0, 0, destinationArray, i, 0);
				}
			}
		}

		// Token: 0x0600097C RID: 2428 RVA: 0x0004C2E0 File Offset: 0x0004A4E0
		internal void BindDitheredRNGData1SPP(CommandBuffer cmd)
		{
			cmd.SetGlobalTexture(HDShaderIDs._OwenScrambledTexture, this.m_RenderPipelineResources.textures.owenScrambled256Tex);
			cmd.SetGlobalTexture(HDShaderIDs._ScramblingTileXSPP, this.m_RenderPipelineResources.textures.scramblingTile1SPP);
			cmd.SetGlobalTexture(HDShaderIDs._RankingTileXSPP, this.m_RenderPipelineResources.textures.rankingTile1SPP);
			cmd.SetGlobalTexture(HDShaderIDs._ScramblingTexture, this.m_RenderPipelineResources.textures.scramblingTex);
		}

		// Token: 0x0600097D RID: 2429 RVA: 0x0004C370 File Offset: 0x0004A570
		internal void BindDitheredRNGData8SPP(CommandBuffer cmd)
		{
			cmd.SetGlobalTexture(HDShaderIDs._OwenScrambledTexture, this.m_RenderPipelineResources.textures.owenScrambled256Tex);
			cmd.SetGlobalTexture(HDShaderIDs._ScramblingTileXSPP, this.m_RenderPipelineResources.textures.scramblingTile8SPP);
			cmd.SetGlobalTexture(HDShaderIDs._RankingTileXSPP, this.m_RenderPipelineResources.textures.rankingTile8SPP);
			cmd.SetGlobalTexture(HDShaderIDs._ScramblingTexture, this.m_RenderPipelineResources.textures.scramblingTex);
		}

		// Token: 0x0600097E RID: 2430 RVA: 0x0004C400 File Offset: 0x0004A600
		internal void BindDitheredRNGData256SPP(CommandBuffer cmd)
		{
			cmd.SetGlobalTexture(HDShaderIDs._OwenScrambledTexture, this.m_RenderPipelineResources.textures.owenScrambled256Tex);
			cmd.SetGlobalTexture(HDShaderIDs._ScramblingTileXSPP, this.m_RenderPipelineResources.textures.scramblingTile256SPP);
			cmd.SetGlobalTexture(HDShaderIDs._RankingTileXSPP, this.m_RenderPipelineResources.textures.rankingTile256SPP);
			cmd.SetGlobalTexture(HDShaderIDs._ScramblingTexture, this.m_RenderPipelineResources.textures.scramblingTex);
		}

		// Token: 0x04000F17 RID: 3863
		private readonly Texture2D[] m_Textures16L;

		// Token: 0x04000F18 RID: 3864
		private readonly Texture2D[] m_Textures16RGB;

		// Token: 0x04000F19 RID: 3865
		private Texture2DArray m_TextureArray16L;

		// Token: 0x04000F1A RID: 3866
		private Texture2DArray m_TextureArray16RGB;

		// Token: 0x04000F1B RID: 3867
		private RenderPipelineResources m_RenderPipelineResources;

		// Token: 0x04000F1C RID: 3868
		private static readonly Random m_Random = new Random();
	}
}
