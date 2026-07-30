using System;
using UnityEngine.Experimental.Rendering;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x02000057 RID: 87
	internal class LightCookieManager
	{
		// Token: 0x060002A7 RID: 679 RVA: 0x0000EED0 File Offset: 0x0000D0D0
		public LightCookieManager(HDRenderPipelineAsset hdAsset, int maxCacheSize)
		{
			this.m_RenderPipelineAsset = hdAsset;
			RenderPipelineResources renderPipelineResources = HDRenderPipeline.defaultAsset.renderPipelineResources;
			GlobalLightLoopSettings lightLoopSettings = hdAsset.currentPlatformRenderPipelineSettings.lightLoopSettings;
			this.m_MaterialFilterAreaLights = CoreUtils.CreateEngineMaterial(renderPipelineResources.shaders.filterAreaLightCookiesPS);
			int num = lightLoopSettings.cubeCookieTexArraySize;
			int num2 = (int)lightLoopSettings.cookieAtlasSize;
			this.cookieFormat = (GraphicsFormat)lightLoopSettings.cookieFormat;
			this.cookieAtlasLastValidMip = lightLoopSettings.cookieAtlasLastValidMip;
			if (PowerOfTwoTextureAtlas.GetApproxCacheSizeInByte(1, num2, true, this.cookieFormat) > 2000000000L)
			{
				num2 = PowerOfTwoTextureAtlas.GetMaxCacheSizeForWeightInByte(2000000000, true, this.cookieFormat);
			}
			this.m_CookieAtlas = new PowerOfTwoTextureAtlas(num2, lightLoopSettings.cookieAtlasLastValidMip, this.cookieFormat, FilterMode.Point, "Cookie Atlas (Punctual Lights)", true);
			this.m_CubeToPanoMaterial = CoreUtils.CreateEngineMaterial(renderPipelineResources.shaders.cubeToPanoPS);
			this.m_CubeCookieTexArray = new TextureCacheCubemap("Cookie", 1);
			int pointCookieSize = (int)lightLoopSettings.pointCookieSize;
			if (TextureCacheCubemap.GetApproxCacheSizeInByte(num, pointCookieSize, 1) > 2000000000L)
			{
				num = TextureCacheCubemap.GetMaxCacheSizeForWeightInByte(2000000000L, pointCookieSize, 1);
			}
			this.m_CubeCookieTexArray.AllocTextureArray(num, pointCookieSize, this.cookieFormat, true, this.m_CubeToPanoMaterial);
		}

		// Token: 0x060002A8 RID: 680 RVA: 0x0000EFEF File Offset: 0x0000D1EF
		public void NewFrame()
		{
			this.m_CubeCookieTexArray.NewFrame();
			this.m_CookieAtlas.ResetRequestedTexture();
			this.m_2DCookieAtlasNeedsLayouting = false;
			this.m_NoMoreSpace = false;
		}

		// Token: 0x060002A9 RID: 681 RVA: 0x0000F018 File Offset: 0x0000D218
		public void Release()
		{
			CoreUtils.Destroy(this.m_MaterialFilterAreaLights);
			CoreUtils.Destroy(this.m_CubeToPanoMaterial);
			if (this.m_TempRenderTexture0 != null)
			{
				this.m_TempRenderTexture0.Release();
				this.m_TempRenderTexture0 = null;
			}
			if (this.m_TempRenderTexture1 != null)
			{
				this.m_TempRenderTexture1.Release();
				this.m_TempRenderTexture1 = null;
			}
			if (this.m_CookieAtlas != null)
			{
				this.m_CookieAtlas.Release();
				this.m_CookieAtlas = null;
			}
			if (this.m_CubeCookieTexArray != null)
			{
				this.m_CubeCookieTexArray.Release();
				this.m_CubeCookieTexArray = null;
			}
		}

		// Token: 0x060002AA RID: 682 RVA: 0x0000F0B0 File Offset: 0x0000D2B0
		private Texture FilterAreaLightTexture(CommandBuffer cmd, Texture source)
		{
			if (this.m_MaterialFilterAreaLights == null)
			{
				Debug.LogError("FilterAreaLightTexture has an invalid shader. Can't filter area light cookie.");
				return null;
			}
			int num = this.m_CookieAtlas.AtlasTexture.rt.width;
			int num2 = this.m_CookieAtlas.AtlasTexture.rt.height;
			int num3 = source.width;
			int num4 = source.height;
			int num5 = 1 + Mathf.FloorToInt(Mathf.Log((float)Mathf.Max(source.width, source.height), 2f));
			if (this.m_TempRenderTexture0 == null)
			{
				string name = this.m_CookieAtlas.AtlasTexture.name;
				this.m_TempRenderTexture0 = new RenderTexture(num, num2, 1, this.cookieFormat)
				{
					hideFlags = HideFlags.HideAndDontSave,
					useMipMap = true,
					autoGenerateMips = false,
					name = name + "TempAreaLightRT0"
				};
				this.m_TempRenderTexture1 = new RenderTexture(num >> 1, num2, 1, this.cookieFormat)
				{
					hideFlags = HideFlags.HideAndDontSave,
					useMipMap = true,
					autoGenerateMips = false,
					name = name + "TempAreaLightRT1"
				};
			}
			using (new ProfilingScope(cmd, ProfilingSampler.Get<HDProfileId>(HDProfileId.AreaLightCookieConvolution)))
			{
				int num6 = num;
				int num7 = num2;
				cmd.SetGlobalTexture(LightCookieManager.s_texSource, source);
				cmd.SetGlobalInt(LightCookieManager.s_sourceMipLevel, 0);
				cmd.SetRenderTarget(this.m_TempRenderTexture0, 0);
				cmd.SetViewport(new Rect(0f, 0f, (float)num3, (float)num4));
				cmd.DrawProcedural(Matrix4x4.identity, this.m_MaterialFilterAreaLights, 0, MeshTopology.Triangles, 3, 1);
				Vector4 zero = Vector4.zero;
				for (int i = 1; i < num5; i++)
				{
					zero.Set((float)num3 / (float)num * 1f, (float)num4 / (float)num2, 1f / (float)num, 1f / (float)num2);
					Vector4 vector = new Vector4(0f, 0f, (float)num3 / (float)num, (float)num4 / (float)num2);
					num3 = Mathf.Max(1, num3 >> 1);
					num6 = Mathf.Max(1, num6 >> 1);
					cmd.SetRenderTarget(this.m_TempRenderTexture1, i - 1);
					cmd.SetViewport(new Rect(0f, 0f, (float)num3, (float)num4));
					cmd.SetGlobalTexture(LightCookieManager.s_texSource, this.m_TempRenderTexture0);
					cmd.SetGlobalInt(LightCookieManager.s_sourceMipLevel, i - 1);
					cmd.SetGlobalVector(LightCookieManager.s_sourceSize, zero);
					cmd.SetGlobalVector(LightCookieManager.s_uvLimits, vector);
					cmd.DrawProcedural(Matrix4x4.identity, this.m_MaterialFilterAreaLights, 1, MeshTopology.Triangles, 3, 1);
					num = num6;
					zero.Set((float)num3 / (float)num, (float)num4 / (float)num2 * 1f, 1f / (float)num, 1f / (float)num2);
					Vector4 vector2 = new Vector4(0f, 0f, (float)num3 / (float)num, (float)num4 / (float)num2);
					num4 = Mathf.Max(1, num4 >> 1);
					num7 = Mathf.Max(1, num7 >> 1);
					cmd.SetRenderTarget(this.m_TempRenderTexture0, i);
					cmd.SetViewport(new Rect(0f, 0f, (float)num3, (float)num4));
					cmd.SetGlobalTexture(LightCookieManager.s_texSource, this.m_TempRenderTexture1);
					cmd.SetGlobalInt(LightCookieManager.s_sourceMipLevel, i - 1);
					cmd.SetGlobalVector(LightCookieManager.s_sourceSize, zero);
					cmd.SetGlobalVector(LightCookieManager.s_uvLimits, vector2);
					cmd.DrawProcedural(Matrix4x4.identity, this.m_MaterialFilterAreaLights, 2, MeshTopology.Triangles, 3, 1);
					num2 = num7;
				}
			}
			return this.m_TempRenderTexture0;
		}

		// Token: 0x060002AB RID: 683 RVA: 0x0000F450 File Offset: 0x0000D650
		public void LayoutIfNeeded()
		{
			if (!this.m_2DCookieAtlasNeedsLayouting)
			{
				return;
			}
			if (!this.m_CookieAtlas.RelayoutEntries())
			{
				Debug.LogError("No more space in the 2D Cookie Texture Atlas. To solve this issue, increase the resolution of the cookie atlas in the HDRP settings.");
				this.m_NoMoreSpace = true;
			}
		}

		// Token: 0x060002AC RID: 684 RVA: 0x0000F47C File Offset: 0x0000D67C
		public Vector4 Fetch2DCookie(CommandBuffer cmd, Texture cookie)
		{
			if (cookie.width < 2 || cookie.height < 2)
			{
				return Vector4.zero;
			}
			Vector4 vector;
			if (!this.m_CookieAtlas.IsCached(out vector, cookie) && !this.m_NoMoreSpace)
			{
				Debug.LogError(string.Format("2D Light cookie texture {0} can't be fetched without having reserved. You can try to increase the cookie atlas resolution in the HDRP settings.", cookie));
			}
			if (this.m_CookieAtlas.NeedsUpdate(cookie, false))
			{
				this.m_CookieAtlas.BlitTexture(cmd, vector, cookie, new Vector4(1f, 1f, 0f, 0f), false, -1);
			}
			return vector;
		}

		// Token: 0x060002AD RID: 685 RVA: 0x0000F504 File Offset: 0x0000D704
		public Vector4 FetchAreaCookie(CommandBuffer cmd, Texture cookie)
		{
			if (cookie.width < 2 || cookie.height < 2)
			{
				return Vector4.zero;
			}
			Vector4 vector;
			if (!this.m_CookieAtlas.IsCached(out vector, cookie) && !this.m_NoMoreSpace)
			{
				Debug.LogError(string.Format("Area Light cookie texture {0} can't be fetched without having reserved. You can try to increase the cookie atlas resolution in the HDRP settings.", cookie));
			}
			if (this.m_CookieAtlas.NeedsUpdate(cookie, true))
			{
				Texture texture = this.FilterAreaLightTexture(cmd, cookie);
				Vector4 vector2 = new Vector4((float)cookie.width / (float)this.atlasTexture.rt.width, (float)cookie.height / (float)this.atlasTexture.rt.height, 0f, 0f);
				this.m_CookieAtlas.BlitTexture(cmd, vector, texture, vector2, true, cookie.GetInstanceID());
			}
			return vector;
		}

		// Token: 0x060002AE RID: 686 RVA: 0x0000F5C3 File Offset: 0x0000D7C3
		public void ReserveSpace(Texture cookie)
		{
			if (cookie == null)
			{
				return;
			}
			if (cookie.width < 2 || cookie.height < 2)
			{
				return;
			}
			if (!this.m_CookieAtlas.ReserveSpace(cookie))
			{
				this.m_2DCookieAtlasNeedsLayouting = true;
			}
		}

		// Token: 0x060002AF RID: 687 RVA: 0x0000F5F7 File Offset: 0x0000D7F7
		public int FetchCubeCookie(CommandBuffer cmd, Texture cookie)
		{
			return this.m_CubeCookieTexArray.FetchSlice(cmd, cookie, false);
		}

		// Token: 0x060002B0 RID: 688 RVA: 0x0000F607 File Offset: 0x0000D807
		public void ResetAllocator()
		{
			this.m_CookieAtlas.ResetAllocator();
		}

		// Token: 0x060002B1 RID: 689 RVA: 0x0000F614 File Offset: 0x0000D814
		public void ClearAtlasTexture(CommandBuffer cmd)
		{
			this.m_CookieAtlas.ClearTarget(cmd);
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x060002B2 RID: 690 RVA: 0x0000F622 File Offset: 0x0000D822
		public RTHandle atlasTexture
		{
			get
			{
				return this.m_CookieAtlas.AtlasTexture;
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x060002B3 RID: 691 RVA: 0x0000F62F File Offset: 0x0000D82F
		public Texture cubeCache
		{
			get
			{
				return this.m_CubeCookieTexArray.GetTexCache();
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x060002B4 RID: 692 RVA: 0x0000F63C File Offset: 0x0000D83C
		public PowerOfTwoTextureAtlas atlas
		{
			get
			{
				return this.m_CookieAtlas;
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x060002B5 RID: 693 RVA: 0x0000F644 File Offset: 0x0000D844
		public TextureCacheCubemap cubeCookieTexArray
		{
			get
			{
				return this.m_CubeCookieTexArray;
			}
		}

		// Token: 0x060002B6 RID: 694 RVA: 0x0000F64C File Offset: 0x0000D84C
		public Vector4 GetCookieAtlasSize()
		{
			return new Vector4((float)this.m_CookieAtlas.AtlasTexture.rt.width, (float)this.m_CookieAtlas.AtlasTexture.rt.height, 1f / (float)this.m_CookieAtlas.AtlasTexture.rt.width, 1f / (float)this.m_CookieAtlas.AtlasTexture.rt.height);
		}

		// Token: 0x060002B7 RID: 695 RVA: 0x0000F6C4 File Offset: 0x0000D8C4
		public Vector4 GetCookieAtlasDatas()
		{
			float num = Mathf.Pow(2f, (float)this.m_CookieAtlas.mipPadding) * 2f;
			return new Vector4((float)this.m_CookieAtlas.AtlasTexture.rt.width, num / (float)this.m_CookieAtlas.AtlasTexture.rt.width, (float)this.cookieAtlasLastValidMip, 0f);
		}

		// Token: 0x0400028D RID: 653
		private HDRenderPipelineAsset m_RenderPipelineAsset;

		// Token: 0x0400028E RID: 654
		internal static readonly int s_texSource = Shader.PropertyToID("_SourceTexture");

		// Token: 0x0400028F RID: 655
		internal static readonly int s_sourceMipLevel = Shader.PropertyToID("_SourceMipLevel");

		// Token: 0x04000290 RID: 656
		internal static readonly int s_sourceSize = Shader.PropertyToID("_SourceSize");

		// Token: 0x04000291 RID: 657
		internal static readonly int s_uvLimits = Shader.PropertyToID("_UVLimits");

		// Token: 0x04000292 RID: 658
		internal const int k_MinCookieSize = 2;

		// Token: 0x04000293 RID: 659
		private readonly Material m_MaterialFilterAreaLights;

		// Token: 0x04000294 RID: 660
		private MaterialPropertyBlock m_MPBFilterAreaLights;

		// Token: 0x04000295 RID: 661
		private readonly Material m_CubeToPanoMaterial;

		// Token: 0x04000296 RID: 662
		private RenderTexture m_TempRenderTexture0;

		// Token: 0x04000297 RID: 663
		private RenderTexture m_TempRenderTexture1;

		// Token: 0x04000298 RID: 664
		private PowerOfTwoTextureAtlas m_CookieAtlas;

		// Token: 0x04000299 RID: 665
		private TextureCacheCubemap m_CubeCookieTexArray;

		// Token: 0x0400029A RID: 666
		private bool m_2DCookieAtlasNeedsLayouting;

		// Token: 0x0400029B RID: 667
		private bool m_NoMoreSpace;

		// Token: 0x0400029C RID: 668
		private readonly int cookieAtlasLastValidMip;

		// Token: 0x0400029D RID: 669
		private readonly GraphicsFormat cookieFormat;
	}
}
