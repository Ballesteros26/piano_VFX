using System;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Rendering.HighDefinition.Attributes;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x020000BF RID: 191
	internal class Lit : RenderPipelineMaterial
	{
		// Token: 0x060006FA RID: 1786 RVA: 0x00003AC0 File Offset: 0x00001CC0
		public override bool IsDefferedMaterial()
		{
			return true;
		}

		// Token: 0x060006FB RID: 1787 RVA: 0x00036F21 File Offset: 0x00035121
		protected void GetGBufferOptions(HDRenderPipelineAsset asset, out int gBufferCount, out bool supportShadowMask, out bool supportLightLayers)
		{
			supportShadowMask = asset.currentPlatformRenderPipelineSettings.supportShadowMask;
			supportLightLayers = asset.currentPlatformRenderPipelineSettings.supportLightLayers;
			gBufferCount = 4 + (supportShadowMask ? 1 : 0) + (supportLightLayers ? 1 : 0);
		}

		// Token: 0x060006FC RID: 1788 RVA: 0x00036F54 File Offset: 0x00035154
		public override int GetMaterialGBufferCount(HDRenderPipelineAsset asset)
		{
			int num;
			bool flag;
			bool flag2;
			this.GetGBufferOptions(asset, out num, out flag, out flag2);
			return num;
		}

		// Token: 0x060006FD RID: 1789 RVA: 0x00036F70 File Offset: 0x00035170
		public override void GetMaterialGBufferDescription(HDRenderPipelineAsset asset, out GraphicsFormat[] RTFormat, out GBufferUsage[] gBufferUsage, out bool[] enableWrite)
		{
			int num;
			bool flag;
			bool flag2;
			this.GetGBufferOptions(asset, out num, out flag, out flag2);
			RTFormat = new GraphicsFormat[num];
			gBufferUsage = new GBufferUsage[num];
			enableWrite = new bool[num];
			RTFormat[0] = GraphicsFormat.R8G8B8A8_SRGB;
			gBufferUsage[0] = GBufferUsage.SubsurfaceScattering;
			enableWrite[0] = true;
			RTFormat[1] = GraphicsFormat.R8G8B8A8_UNorm;
			gBufferUsage[1] = GBufferUsage.Normal;
			enableWrite[1] = true;
			RTFormat[2] = GraphicsFormat.R8G8B8A8_UNorm;
			gBufferUsage[2] = GBufferUsage.None;
			enableWrite[2] = true;
			RTFormat[3] = Builtin.GetLightingBufferFormat();
			gBufferUsage[3] = GBufferUsage.None;
			enableWrite[3] = true;
			int num2 = 4;
			if (flag2)
			{
				RTFormat[num2] = GraphicsFormat.R8G8B8A8_UNorm;
				gBufferUsage[num2] = GBufferUsage.LightLayers;
				num2++;
			}
			if (flag)
			{
				RTFormat[num2] = Builtin.GetShadowMaskBufferFormat();
				gBufferUsage[num2] = GBufferUsage.ShadowMask;
				num2++;
			}
		}

		// Token: 0x060006FF RID: 1791 RVA: 0x00036E96 File Offset: 0x00035096
		public override void Build(HDRenderPipelineAsset hdAsset, RenderPipelineResources defaultResources)
		{
			PreIntegratedFGD.instance.Build(PreIntegratedFGD.FGDIndex.FGD_GGXAndDisneyDiffuse);
			LTCAreaLight.instance.Build();
		}

		// Token: 0x06000700 RID: 1792 RVA: 0x00036EAD File Offset: 0x000350AD
		public override void Cleanup()
		{
			PreIntegratedFGD.instance.Cleanup(PreIntegratedFGD.FGDIndex.FGD_GGXAndDisneyDiffuse);
			LTCAreaLight.instance.Cleanup();
		}

		// Token: 0x06000701 RID: 1793 RVA: 0x00036EC4 File Offset: 0x000350C4
		public override void RenderInit(CommandBuffer cmd)
		{
			PreIntegratedFGD.instance.RenderInit(PreIntegratedFGD.FGDIndex.FGD_GGXAndDisneyDiffuse, cmd);
		}

		// Token: 0x06000702 RID: 1794 RVA: 0x00036ED2 File Offset: 0x000350D2
		public override void Bind(CommandBuffer cmd)
		{
			PreIntegratedFGD.instance.Bind(cmd, PreIntegratedFGD.FGDIndex.FGD_GGXAndDisneyDiffuse);
			LTCAreaLight.instance.Bind(cmd);
		}

		// Token: 0x02000241 RID: 577
		[GenerateHLSL(PackingRules.Exact, true, false, false, 1, false, false)]
		public enum MaterialFeatureFlags
		{
			// Token: 0x040014E0 RID: 5344
			LitStandard = 1,
			// Token: 0x040014E1 RID: 5345
			LitSpecularColor,
			// Token: 0x040014E2 RID: 5346
			LitSubsurfaceScattering = 4,
			// Token: 0x040014E3 RID: 5347
			LitTransmission = 8,
			// Token: 0x040014E4 RID: 5348
			LitAnisotropy = 16,
			// Token: 0x040014E5 RID: 5349
			LitIridescence = 32,
			// Token: 0x040014E6 RID: 5350
			LitClearCoat = 64
		}

		// Token: 0x02000242 RID: 578
		[GenerateHLSL(PackingRules.Exact, false, false, true, 1000, false, false)]
		public struct SurfaceData
		{
			// Token: 0x040014E7 RID: 5351
			[SurfaceDataAttributes("MaterialFeatures", false, false, FieldPrecision.Default)]
			public uint materialFeatures;

			// Token: 0x040014E8 RID: 5352
			[MaterialSharedPropertyMapping(MaterialSharedProperty.Albedo)]
			[SurfaceDataAttributes("Base Color", false, true, FieldPrecision.Real)]
			public Vector3 baseColor;

			// Token: 0x040014E9 RID: 5353
			[SurfaceDataAttributes("Specular Occlusion", false, false, FieldPrecision.Default, precision = FieldPrecision.Real)]
			public float specularOcclusion;

			// Token: 0x040014EA RID: 5354
			[MaterialSharedPropertyMapping(MaterialSharedProperty.Normal)]
			[SurfaceDataAttributes(new string[] { "Normal", "Normal View Space" }, true, false, FieldPrecision.Default)]
			public Vector3 normalWS;

			// Token: 0x040014EB RID: 5355
			[MaterialSharedPropertyMapping(MaterialSharedProperty.Smoothness)]
			[SurfaceDataAttributes("Smoothness", false, false, FieldPrecision.Default, precision = FieldPrecision.Real)]
			public float perceptualSmoothness;

			// Token: 0x040014EC RID: 5356
			[MaterialSharedPropertyMapping(MaterialSharedProperty.AmbientOcclusion)]
			[SurfaceDataAttributes("Ambient Occlusion", false, false, FieldPrecision.Default, precision = FieldPrecision.Real)]
			public float ambientOcclusion;

			// Token: 0x040014ED RID: 5357
			[MaterialSharedPropertyMapping(MaterialSharedProperty.Metal)]
			[SurfaceDataAttributes("Metallic", false, false, FieldPrecision.Default, precision = FieldPrecision.Real)]
			public float metallic;

			// Token: 0x040014EE RID: 5358
			[SurfaceDataAttributes("Coat mask", false, false, FieldPrecision.Default, precision = FieldPrecision.Real)]
			public float coatMask;

			// Token: 0x040014EF RID: 5359
			[MaterialSharedPropertyMapping(MaterialSharedProperty.Specular)]
			[SurfaceDataAttributes("Specular Color", false, true, FieldPrecision.Real)]
			public Vector3 specularColor;

			// Token: 0x040014F0 RID: 5360
			[SurfaceDataAttributes("Diffusion Profile Hash", false, false, FieldPrecision.Default)]
			public uint diffusionProfileHash;

			// Token: 0x040014F1 RID: 5361
			[SurfaceDataAttributes("Subsurface Mask", false, false, FieldPrecision.Default, precision = FieldPrecision.Real)]
			public float subsurfaceMask;

			// Token: 0x040014F2 RID: 5362
			[SurfaceDataAttributes("Thickness", false, false, FieldPrecision.Default, precision = FieldPrecision.Real)]
			public float thickness;

			// Token: 0x040014F3 RID: 5363
			[SurfaceDataAttributes("Tangent", true, false, FieldPrecision.Default)]
			public Vector3 tangentWS;

			// Token: 0x040014F4 RID: 5364
			[SurfaceDataAttributes("Anisotropy", false, false, FieldPrecision.Default, precision = FieldPrecision.Real)]
			public float anisotropy;

			// Token: 0x040014F5 RID: 5365
			[SurfaceDataAttributes("Iridescence Layer Thickness", false, false, FieldPrecision.Default, precision = FieldPrecision.Real)]
			public float iridescenceThickness;

			// Token: 0x040014F6 RID: 5366
			[SurfaceDataAttributes("Iridescence Mask", false, false, FieldPrecision.Default, precision = FieldPrecision.Real)]
			public float iridescenceMask;

			// Token: 0x040014F7 RID: 5367
			[SurfaceDataAttributes(new string[] { "Geometric Normal", "Geometric Normal View Space" }, true, false, FieldPrecision.Default)]
			public Vector3 geomNormalWS;

			// Token: 0x040014F8 RID: 5368
			[SurfaceDataAttributes("Index of refraction", false, false, FieldPrecision.Default, precision = FieldPrecision.Real)]
			public float ior;

			// Token: 0x040014F9 RID: 5369
			[SurfaceDataAttributes("Transmittance Color", false, false, FieldPrecision.Default, precision = FieldPrecision.Real)]
			public Vector3 transmittanceColor;

			// Token: 0x040014FA RID: 5370
			[SurfaceDataAttributes("Transmittance Absorption Distance", false, false, FieldPrecision.Default, precision = FieldPrecision.Real)]
			public float atDistance;

			// Token: 0x040014FB RID: 5371
			[SurfaceDataAttributes("Transmittance mask", false, false, FieldPrecision.Default, precision = FieldPrecision.Real)]
			public float transmittanceMask;
		}

		// Token: 0x02000243 RID: 579
		[GenerateHLSL(PackingRules.Exact, false, false, true, 1050, false, false)]
		public struct BSDFData
		{
			// Token: 0x040014FC RID: 5372
			public uint materialFeatures;

			// Token: 0x040014FD RID: 5373
			[SurfaceDataAttributes("", false, true, FieldPrecision.Real)]
			public Vector3 diffuseColor;

			// Token: 0x040014FE RID: 5374
			[SurfaceDataAttributes("", false, false, FieldPrecision.Default, precision = FieldPrecision.Real)]
			public Vector3 fresnel0;

			// Token: 0x040014FF RID: 5375
			[SurfaceDataAttributes("", false, false, FieldPrecision.Default, precision = FieldPrecision.Real)]
			public float ambientOcclusion;

			// Token: 0x04001500 RID: 5376
			[SurfaceDataAttributes("", false, false, FieldPrecision.Default, precision = FieldPrecision.Real)]
			public float specularOcclusion;

			// Token: 0x04001501 RID: 5377
			[SurfaceDataAttributes(new string[] { "Normal WS", "Normal View Space" }, true, false, FieldPrecision.Default)]
			public Vector3 normalWS;

			// Token: 0x04001502 RID: 5378
			[SurfaceDataAttributes("", false, false, FieldPrecision.Default, precision = FieldPrecision.Real)]
			public float perceptualRoughness;

			// Token: 0x04001503 RID: 5379
			[SurfaceDataAttributes("", false, false, FieldPrecision.Default, precision = FieldPrecision.Real)]
			public float coatMask;

			// Token: 0x04001504 RID: 5380
			public uint diffusionProfileIndex;

			// Token: 0x04001505 RID: 5381
			[SurfaceDataAttributes("", false, false, FieldPrecision.Default, precision = FieldPrecision.Real)]
			public float subsurfaceMask;

			// Token: 0x04001506 RID: 5382
			[SurfaceDataAttributes("", false, false, FieldPrecision.Default, precision = FieldPrecision.Real)]
			public float thickness;

			// Token: 0x04001507 RID: 5383
			public bool useThickObjectMode;

			// Token: 0x04001508 RID: 5384
			[SurfaceDataAttributes("", false, false, FieldPrecision.Default, precision = FieldPrecision.Real)]
			public Vector3 transmittance;

			// Token: 0x04001509 RID: 5385
			[SurfaceDataAttributes("", true, false, FieldPrecision.Default)]
			public Vector3 tangentWS;

			// Token: 0x0400150A RID: 5386
			[SurfaceDataAttributes("", true, false, FieldPrecision.Default)]
			public Vector3 bitangentWS;

			// Token: 0x0400150B RID: 5387
			[SurfaceDataAttributes("", false, false, FieldPrecision.Default, precision = FieldPrecision.Real)]
			public float roughnessT;

			// Token: 0x0400150C RID: 5388
			[SurfaceDataAttributes("", false, false, FieldPrecision.Default, precision = FieldPrecision.Real)]
			public float roughnessB;

			// Token: 0x0400150D RID: 5389
			[SurfaceDataAttributes("", false, false, FieldPrecision.Default, precision = FieldPrecision.Real)]
			public float anisotropy;

			// Token: 0x0400150E RID: 5390
			[SurfaceDataAttributes("", false, false, FieldPrecision.Default, precision = FieldPrecision.Real)]
			public float iridescenceThickness;

			// Token: 0x0400150F RID: 5391
			[SurfaceDataAttributes("", false, false, FieldPrecision.Default, precision = FieldPrecision.Real)]
			public float iridescenceMask;

			// Token: 0x04001510 RID: 5392
			[SurfaceDataAttributes("", false, false, FieldPrecision.Default, precision = FieldPrecision.Real)]
			public float coatRoughness;

			// Token: 0x04001511 RID: 5393
			[SurfaceDataAttributes(new string[] { "Geometric Normal", "Geometric Normal View Space" }, true, false, FieldPrecision.Default, precision = FieldPrecision.Real)]
			public Vector3 geomNormalWS;

			// Token: 0x04001512 RID: 5394
			[SurfaceDataAttributes("", false, false, FieldPrecision.Default, precision = FieldPrecision.Real)]
			public float ior;

			// Token: 0x04001513 RID: 5395
			[SurfaceDataAttributes("", false, false, FieldPrecision.Default, precision = FieldPrecision.Real)]
			public Vector3 absorptionCoefficient;

			// Token: 0x04001514 RID: 5396
			[SurfaceDataAttributes("", false, false, FieldPrecision.Default, precision = FieldPrecision.Real)]
			public float transmittanceMask;
		}
	}
}
