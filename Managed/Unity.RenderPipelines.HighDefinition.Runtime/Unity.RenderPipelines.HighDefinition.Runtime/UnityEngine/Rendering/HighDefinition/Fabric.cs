using System;
using UnityEngine.Rendering.HighDefinition.Attributes;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x020000B7 RID: 183
	internal class Fabric : RenderPipelineMaterial
	{
		// Token: 0x060006D5 RID: 1749 RVA: 0x000363B7 File Offset: 0x000345B7
		public override void Build(HDRenderPipelineAsset hdAsset, RenderPipelineResources defaultResources)
		{
			PreIntegratedFGD.instance.Build(PreIntegratedFGD.FGDIndex.FGD_CharlieAndFabricLambert);
		}

		// Token: 0x060006D6 RID: 1750 RVA: 0x000363C4 File Offset: 0x000345C4
		public override void Cleanup()
		{
			PreIntegratedFGD.instance.Cleanup(PreIntegratedFGD.FGDIndex.FGD_CharlieAndFabricLambert);
		}

		// Token: 0x060006D7 RID: 1751 RVA: 0x000363D1 File Offset: 0x000345D1
		public override void RenderInit(CommandBuffer cmd)
		{
			PreIntegratedFGD.instance.RenderInit(PreIntegratedFGD.FGDIndex.FGD_CharlieAndFabricLambert, cmd);
		}

		// Token: 0x060006D8 RID: 1752 RVA: 0x000363DF File Offset: 0x000345DF
		public override void Bind(CommandBuffer cmd)
		{
			PreIntegratedFGD.instance.Bind(cmd, PreIntegratedFGD.FGDIndex.FGD_CharlieAndFabricLambert);
		}

		// Token: 0x0200023B RID: 571
		[GenerateHLSL(PackingRules.Exact, true, false, false, 1, false, false)]
		public enum MaterialFeatureFlags
		{
			// Token: 0x04001499 RID: 5273
			FabricCottonWool = 1,
			// Token: 0x0400149A RID: 5274
			FabricSubsurfaceScattering,
			// Token: 0x0400149B RID: 5275
			FabricTransmission = 4
		}

		// Token: 0x0200023C RID: 572
		[GenerateHLSL(PackingRules.Exact, false, false, true, 1300, false, false)]
		public struct SurfaceData
		{
			// Token: 0x0400149C RID: 5276
			[SurfaceDataAttributes("MaterialFeatures", false, false, FieldPrecision.Default)]
			public uint materialFeatures;

			// Token: 0x0400149D RID: 5277
			[MaterialSharedPropertyMapping(MaterialSharedProperty.Albedo)]
			[SurfaceDataAttributes("Base Color", false, true, FieldPrecision.Default)]
			public Vector3 baseColor;

			// Token: 0x0400149E RID: 5278
			[SurfaceDataAttributes("Specular Occlusion", false, false, FieldPrecision.Default)]
			public float specularOcclusion;

			// Token: 0x0400149F RID: 5279
			[MaterialSharedPropertyMapping(MaterialSharedProperty.Normal)]
			[SurfaceDataAttributes(new string[] { "Normal", "Normal View Space" }, true, false, FieldPrecision.Default)]
			public Vector3 normalWS;

			// Token: 0x040014A0 RID: 5280
			[SurfaceDataAttributes(new string[] { "Geometric Normal", "Geometric Normal View Space" }, true, false, FieldPrecision.Default)]
			public Vector3 geomNormalWS;

			// Token: 0x040014A1 RID: 5281
			[MaterialSharedPropertyMapping(MaterialSharedProperty.Smoothness)]
			[SurfaceDataAttributes("Smoothness", false, false, FieldPrecision.Default)]
			public float perceptualSmoothness;

			// Token: 0x040014A2 RID: 5282
			[MaterialSharedPropertyMapping(MaterialSharedProperty.AmbientOcclusion)]
			[SurfaceDataAttributes("Ambient Occlusion", false, false, FieldPrecision.Default)]
			public float ambientOcclusion;

			// Token: 0x040014A3 RID: 5283
			[MaterialSharedPropertyMapping(MaterialSharedProperty.Specular)]
			[SurfaceDataAttributes("Specular Tint", false, true, FieldPrecision.Default)]
			public Vector3 specularColor;

			// Token: 0x040014A4 RID: 5284
			[SurfaceDataAttributes("Diffusion Profile Hash", false, false, FieldPrecision.Default)]
			public uint diffusionProfileHash;

			// Token: 0x040014A5 RID: 5285
			[SurfaceDataAttributes("Subsurface Mask", false, false, FieldPrecision.Default)]
			public float subsurfaceMask;

			// Token: 0x040014A6 RID: 5286
			[SurfaceDataAttributes("Thickness", false, false, FieldPrecision.Default)]
			public float thickness;

			// Token: 0x040014A7 RID: 5287
			[SurfaceDataAttributes("Tangent", true, false, FieldPrecision.Default)]
			public Vector3 tangentWS;

			// Token: 0x040014A8 RID: 5288
			[SurfaceDataAttributes("Anisotropy", false, false, FieldPrecision.Default)]
			public float anisotropy;
		}

		// Token: 0x0200023D RID: 573
		[GenerateHLSL(PackingRules.Exact, false, false, true, 1350, false, false)]
		public struct BSDFData
		{
			// Token: 0x040014A9 RID: 5289
			public uint materialFeatures;

			// Token: 0x040014AA RID: 5290
			[SurfaceDataAttributes("", false, true, FieldPrecision.Default)]
			public Vector3 diffuseColor;

			// Token: 0x040014AB RID: 5291
			public Vector3 fresnel0;

			// Token: 0x040014AC RID: 5292
			public float ambientOcclusion;

			// Token: 0x040014AD RID: 5293
			public float specularOcclusion;

			// Token: 0x040014AE RID: 5294
			[SurfaceDataAttributes(new string[] { "Normal WS", "Normal View Space" }, true, false, FieldPrecision.Default)]
			public Vector3 normalWS;

			// Token: 0x040014AF RID: 5295
			[SurfaceDataAttributes(new string[] { "Geometric Normal", "Geometric Normal View Space" }, true, false, FieldPrecision.Default)]
			public Vector3 geomNormalWS;

			// Token: 0x040014B0 RID: 5296
			public float perceptualRoughness;

			// Token: 0x040014B1 RID: 5297
			public uint diffusionProfileIndex;

			// Token: 0x040014B2 RID: 5298
			public float subsurfaceMask;

			// Token: 0x040014B3 RID: 5299
			public float thickness;

			// Token: 0x040014B4 RID: 5300
			public bool useThickObjectMode;

			// Token: 0x040014B5 RID: 5301
			public Vector3 transmittance;

			// Token: 0x040014B6 RID: 5302
			[SurfaceDataAttributes("", true, false, FieldPrecision.Default)]
			public Vector3 tangentWS;

			// Token: 0x040014B7 RID: 5303
			[SurfaceDataAttributes("", true, false, FieldPrecision.Default)]
			public Vector3 bitangentWS;

			// Token: 0x040014B8 RID: 5304
			public float roughnessT;

			// Token: 0x040014B9 RID: 5305
			public float roughnessB;

			// Token: 0x040014BA RID: 5306
			public float anisotropy;
		}
	}
}
