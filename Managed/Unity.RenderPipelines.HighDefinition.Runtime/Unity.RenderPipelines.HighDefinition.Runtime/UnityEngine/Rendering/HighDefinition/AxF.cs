using System;
using UnityEngine.Rendering.HighDefinition.Attributes;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x020000A9 RID: 169
	internal class AxF : RenderPipelineMaterial
	{
		// Token: 0x0600064A RID: 1610 RVA: 0x00034140 File Offset: 0x00032340
		public override void Build(HDRenderPipelineAsset hdAsset, RenderPipelineResources defaultResources)
		{
			this.m_preIntegratedFGDMaterial_Ward = CoreUtils.CreateEngineMaterial(defaultResources.shaders.preIntegratedFGD_WardPS);
			if (this.m_preIntegratedFGDMaterial_Ward == null)
			{
				throw new Exception("Failed to create material for Ward BRDF pre-integration!");
			}
			this.m_preIntegratedFGDMaterial_CookTorrance = CoreUtils.CreateEngineMaterial(defaultResources.shaders.preIntegratedFGD_CookTorrancePS);
			if (this.m_preIntegratedFGDMaterial_CookTorrance == null)
			{
				throw new Exception("Failed to create material for Cook-Torrance BRDF pre-integration!");
			}
			this.m_preIntegratedFGD_Ward = new RenderTexture(128, 128, 0, RenderTextureFormat.ARGB2101010, RenderTextureReadWrite.Linear);
			this.m_preIntegratedFGD_Ward.hideFlags = HideFlags.HideAndDontSave;
			this.m_preIntegratedFGD_Ward.filterMode = FilterMode.Bilinear;
			this.m_preIntegratedFGD_Ward.wrapMode = TextureWrapMode.Clamp;
			this.m_preIntegratedFGD_Ward.hideFlags = HideFlags.DontSave;
			this.m_preIntegratedFGD_Ward.name = CoreUtils.GetRenderTargetAutoName(128, 128, 1, RenderTextureFormat.ARGB2101010, "PreIntegratedFGD_Ward", false, false, MSAASamples.None);
			this.m_preIntegratedFGD_Ward.Create();
			this.m_preIntegratedFGD_CookTorrance = new RenderTexture(128, 128, 0, RenderTextureFormat.ARGB2101010, RenderTextureReadWrite.Linear);
			this.m_preIntegratedFGD_CookTorrance.hideFlags = HideFlags.HideAndDontSave;
			this.m_preIntegratedFGD_CookTorrance.filterMode = FilterMode.Bilinear;
			this.m_preIntegratedFGD_CookTorrance.wrapMode = TextureWrapMode.Clamp;
			this.m_preIntegratedFGD_CookTorrance.hideFlags = HideFlags.DontSave;
			this.m_preIntegratedFGD_CookTorrance.name = CoreUtils.GetRenderTargetAutoName(128, 128, 1, RenderTextureFormat.ARGB2101010, "PreIntegratedFGD_CookTorrance", false, false, MSAASamples.None);
			this.m_preIntegratedFGD_CookTorrance.Create();
			this.m_LtcData = new Texture2DArray(64, 64, 3, TextureFormat.RGBAHalf, false, true)
			{
				hideFlags = HideFlags.HideAndDontSave,
				wrapMode = TextureWrapMode.Clamp,
				filterMode = FilterMode.Bilinear,
				name = CoreUtils.GetTextureAutoName(64, 64, TextureFormat.RGBAHalf, TextureDimension.Tex2DArray, "LTC_LUT", false, 2)
			};
			LTCAreaLight.LoadLUT(this.m_LtcData, 0, TextureFormat.RGBAHalf, LTCAreaLight.s_LtcMatrixData_GGX);
			this.m_LtcData.Apply();
			LTCAreaLight.instance.Build();
		}

		// Token: 0x0600064B RID: 1611 RVA: 0x0003430C File Offset: 0x0003250C
		public override void Cleanup()
		{
			CoreUtils.Destroy(this.m_preIntegratedFGD_CookTorrance);
			CoreUtils.Destroy(this.m_preIntegratedFGD_Ward);
			CoreUtils.Destroy(this.m_preIntegratedFGDMaterial_CookTorrance);
			CoreUtils.Destroy(this.m_preIntegratedFGDMaterial_Ward);
			this.m_preIntegratedFGD_CookTorrance = null;
			this.m_preIntegratedFGD_Ward = null;
			this.m_preIntegratedFGDMaterial_Ward = null;
			this.m_preIntegratedFGDMaterial_CookTorrance = null;
			this.m_precomputedFGDTablesAreInit = false;
			CoreUtils.Destroy(this.m_LtcData);
			LTCAreaLight.instance.Cleanup();
		}

		// Token: 0x0600064C RID: 1612 RVA: 0x00034380 File Offset: 0x00032580
		public override void RenderInit(CommandBuffer cmd)
		{
			if (this.m_precomputedFGDTablesAreInit || this.m_preIntegratedFGDMaterial_Ward == null || this.m_preIntegratedFGDMaterial_CookTorrance == null)
			{
				return;
			}
			using (new ProfilingScope(cmd, ProfilingSampler.Get<HDProfileId>(HDProfileId.PreIntegradeWardCookTorrance)))
			{
				CoreUtils.DrawFullScreen(cmd, this.m_preIntegratedFGDMaterial_Ward, new RenderTargetIdentifier(this.m_preIntegratedFGD_Ward), null, 0);
				CoreUtils.DrawFullScreen(cmd, this.m_preIntegratedFGDMaterial_CookTorrance, new RenderTargetIdentifier(this.m_preIntegratedFGD_CookTorrance), null, 0);
			}
			this.m_precomputedFGDTablesAreInit = true;
		}

		// Token: 0x0600064D RID: 1613 RVA: 0x0003441C File Offset: 0x0003261C
		public override void Bind(CommandBuffer cmd)
		{
			if (this.m_preIntegratedFGD_Ward == null || this.m_preIntegratedFGD_CookTorrance == null)
			{
				throw new Exception("Ward & Cook-Torrance BRDF pre-integration table not available!");
			}
			cmd.SetGlobalTexture(AxF._PreIntegratedFGD_Ward, this.m_preIntegratedFGD_Ward);
			cmd.SetGlobalTexture(AxF._PreIntegratedFGD_CookTorrance, this.m_preIntegratedFGD_CookTorrance);
			cmd.SetGlobalTexture(AxF._AxFLtcData, this.m_LtcData);
			LTCAreaLight.instance.Bind(cmd);
		}

		// Token: 0x040006A2 RID: 1698
		private Texture2DArray m_LtcData;

		// Token: 0x040006A3 RID: 1699
		private Material m_preIntegratedFGDMaterial_Ward;

		// Token: 0x040006A4 RID: 1700
		private Material m_preIntegratedFGDMaterial_CookTorrance;

		// Token: 0x040006A5 RID: 1701
		private RenderTexture m_preIntegratedFGD_Ward;

		// Token: 0x040006A6 RID: 1702
		private RenderTexture m_preIntegratedFGD_CookTorrance;

		// Token: 0x040006A7 RID: 1703
		private bool m_precomputedFGDTablesAreInit;

		// Token: 0x040006A8 RID: 1704
		public static readonly int _PreIntegratedFGD_Ward = Shader.PropertyToID("_PreIntegratedFGD_Ward");

		// Token: 0x040006A9 RID: 1705
		public static readonly int _PreIntegratedFGD_CookTorrance = Shader.PropertyToID("_PreIntegratedFGD_CookTorrance");

		// Token: 0x040006AA RID: 1706
		public static readonly int _AxFLtcData = Shader.PropertyToID("_AxFLtcData");

		// Token: 0x02000222 RID: 546
		[GenerateHLSL(PackingRules.Exact, true, false, false, 1, false, false)]
		public enum FeatureFlags
		{
			// Token: 0x040013EE RID: 5102
			AxfAnisotropy = 1,
			// Token: 0x040013EF RID: 5103
			AxfClearCoat,
			// Token: 0x040013F0 RID: 5104
			AxfClearCoatRefraction = 4,
			// Token: 0x040013F1 RID: 5105
			AxfUseHeightMap = 8,
			// Token: 0x040013F2 RID: 5106
			AxfBRDFColorDiagonalClamp = 16,
			// Token: 0x040013F3 RID: 5107
			AxfHonorMinRoughness = 256,
			// Token: 0x040013F4 RID: 5108
			AxfHonorMinRoughnessCoat = 512
		}

		// Token: 0x02000223 RID: 547
		[GenerateHLSL(PackingRules.Exact, false, false, true, 1200, false, false)]
		public struct SurfaceData
		{
			// Token: 0x040013F5 RID: 5109
			[MaterialSharedPropertyMapping(MaterialSharedProperty.Normal)]
			[SurfaceDataAttributes(new string[] { "Normal", "Normal View Space" }, true, false, FieldPrecision.Default)]
			public Vector3 normalWS;

			// Token: 0x040013F6 RID: 5110
			[SurfaceDataAttributes("Tangent", true, false, FieldPrecision.Default)]
			public Vector3 tangentWS;

			// Token: 0x040013F7 RID: 5111
			[MaterialSharedPropertyMapping(MaterialSharedProperty.Albedo)]
			[SurfaceDataAttributes("Diffuse Color", false, true, FieldPrecision.Default)]
			public Vector3 diffuseColor;

			// Token: 0x040013F8 RID: 5112
			[MaterialSharedPropertyMapping(MaterialSharedProperty.Specular)]
			[SurfaceDataAttributes("Specular Color", false, true, FieldPrecision.Default)]
			public Vector3 specularColor;

			// Token: 0x040013F9 RID: 5113
			[SurfaceDataAttributes("Fresnel F0", false, false, FieldPrecision.Default)]
			public Vector3 fresnelF0;

			// Token: 0x040013FA RID: 5114
			[SurfaceDataAttributes("Specular Lobe", false, false, FieldPrecision.Default)]
			public Vector2 specularLobe;

			// Token: 0x040013FB RID: 5115
			[SurfaceDataAttributes("Height", false, false, FieldPrecision.Default)]
			public float height_mm;

			// Token: 0x040013FC RID: 5116
			[SurfaceDataAttributes("Anisotropic Angle", false, false, FieldPrecision.Default)]
			public float anisotropyAngle;

			// Token: 0x040013FD RID: 5117
			[SurfaceDataAttributes("Flakes UV", false, false, FieldPrecision.Default)]
			public Vector2 flakesUV;

			// Token: 0x040013FE RID: 5118
			[SurfaceDataAttributes("Flakes Mip", false, false, FieldPrecision.Default)]
			public float flakesMipLevel;

			// Token: 0x040013FF RID: 5119
			[SurfaceDataAttributes("Clearcoat Color", false, false, FieldPrecision.Default)]
			public Vector3 clearcoatColor;

			// Token: 0x04001400 RID: 5120
			[SurfaceDataAttributes("Clearcoat Normal", true, false, FieldPrecision.Default)]
			public Vector3 clearcoatNormalWS;

			// Token: 0x04001401 RID: 5121
			[SurfaceDataAttributes("Clearcoat IOR", false, false, FieldPrecision.Default)]
			public float clearcoatIOR;

			// Token: 0x04001402 RID: 5122
			[SurfaceDataAttributes(new string[] { "Geometric Normal", "Geometric Normal View Space" }, true, false, FieldPrecision.Default)]
			public Vector3 geomNormalWS;
		}

		// Token: 0x02000224 RID: 548
		[GenerateHLSL(PackingRules.Exact, false, false, true, 1250, false, false)]
		public struct BSDFData
		{
			// Token: 0x04001403 RID: 5123
			[SurfaceDataAttributes(new string[] { "Normal WS", "Normal View Space" }, true, false, FieldPrecision.Default)]
			public Vector3 normalWS;

			// Token: 0x04001404 RID: 5124
			[SurfaceDataAttributes("", true, false, FieldPrecision.Default)]
			public Vector3 tangentWS;

			// Token: 0x04001405 RID: 5125
			[SurfaceDataAttributes("", true, false, FieldPrecision.Default)]
			public Vector3 biTangentWS;

			// Token: 0x04001406 RID: 5126
			public Vector3 diffuseColor;

			// Token: 0x04001407 RID: 5127
			public Vector3 specularColor;

			// Token: 0x04001408 RID: 5128
			public Vector3 fresnelF0;

			// Token: 0x04001409 RID: 5129
			public Vector2 roughness;

			// Token: 0x0400140A RID: 5130
			public float height_mm;

			// Token: 0x0400140B RID: 5131
			[SurfaceDataAttributes("", false, false, FieldPrecision.Default)]
			public Vector2 flakesUV;

			// Token: 0x0400140C RID: 5132
			[SurfaceDataAttributes("Flakes Mip", false, false, FieldPrecision.Default)]
			public float flakesMipLevel;

			// Token: 0x0400140D RID: 5133
			public Vector3 clearcoatColor;

			// Token: 0x0400140E RID: 5134
			[SurfaceDataAttributes("", true, false, FieldPrecision.Default)]
			public Vector3 clearcoatNormalWS;

			// Token: 0x0400140F RID: 5135
			public float clearcoatIOR;

			// Token: 0x04001410 RID: 5136
			[SurfaceDataAttributes(new string[] { "Geometric Normal", "Geometric Normal View Space" }, true, false, FieldPrecision.Default)]
			public Vector3 geomNormalWS;
		}
	}
}
