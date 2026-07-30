using System;
using UnityEngine.Rendering.HighDefinition.Attributes;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x020000BD RID: 189
	internal class Hair : RenderPipelineMaterial
	{
		// Token: 0x060006EF RID: 1775 RVA: 0x00036E96 File Offset: 0x00035096
		public override void Build(HDRenderPipelineAsset hdAsset, RenderPipelineResources defaultResources)
		{
			PreIntegratedFGD.instance.Build(PreIntegratedFGD.FGDIndex.FGD_GGXAndDisneyDiffuse);
			LTCAreaLight.instance.Build();
		}

		// Token: 0x060006F0 RID: 1776 RVA: 0x00036EAD File Offset: 0x000350AD
		public override void Cleanup()
		{
			PreIntegratedFGD.instance.Cleanup(PreIntegratedFGD.FGDIndex.FGD_GGXAndDisneyDiffuse);
			LTCAreaLight.instance.Cleanup();
		}

		// Token: 0x060006F1 RID: 1777 RVA: 0x00036EC4 File Offset: 0x000350C4
		public override void RenderInit(CommandBuffer cmd)
		{
			PreIntegratedFGD.instance.RenderInit(PreIntegratedFGD.FGDIndex.FGD_GGXAndDisneyDiffuse, cmd);
		}

		// Token: 0x060006F2 RID: 1778 RVA: 0x00036ED2 File Offset: 0x000350D2
		public override void Bind(CommandBuffer cmd)
		{
			PreIntegratedFGD.instance.Bind(cmd, PreIntegratedFGD.FGDIndex.FGD_GGXAndDisneyDiffuse);
			LTCAreaLight.instance.Bind(cmd);
		}

		// Token: 0x0200023E RID: 574
		[GenerateHLSL(PackingRules.Exact, true, false, false, 1, false, false)]
		public enum MaterialFeatureFlags
		{
			// Token: 0x040014BC RID: 5308
			HairKajiyaKay = 1
		}

		// Token: 0x0200023F RID: 575
		[GenerateHLSL(PackingRules.Exact, false, false, true, 1400, false, false)]
		public struct SurfaceData
		{
			// Token: 0x040014BD RID: 5309
			[SurfaceDataAttributes("MaterialFeatures", false, false, FieldPrecision.Default)]
			public uint materialFeatures;

			// Token: 0x040014BE RID: 5310
			[MaterialSharedPropertyMapping(MaterialSharedProperty.AmbientOcclusion)]
			[SurfaceDataAttributes("Ambient Occlusion", false, false, FieldPrecision.Default)]
			public float ambientOcclusion;

			// Token: 0x040014BF RID: 5311
			[MaterialSharedPropertyMapping(MaterialSharedProperty.Albedo)]
			[SurfaceDataAttributes("Diffuse", false, true, FieldPrecision.Default)]
			public Vector3 diffuseColor;

			// Token: 0x040014C0 RID: 5312
			[SurfaceDataAttributes("Specular Occlusion", false, false, FieldPrecision.Default)]
			public float specularOcclusion;

			// Token: 0x040014C1 RID: 5313
			[MaterialSharedPropertyMapping(MaterialSharedProperty.Normal)]
			[SurfaceDataAttributes(new string[] { "Normal", "Normal View Space" }, true, false, FieldPrecision.Default)]
			public Vector3 normalWS;

			// Token: 0x040014C2 RID: 5314
			[SurfaceDataAttributes(new string[] { "Geometric Normal", "Geometric Normal View Space" }, true, false, FieldPrecision.Default)]
			public Vector3 geomNormalWS;

			// Token: 0x040014C3 RID: 5315
			[MaterialSharedPropertyMapping(MaterialSharedProperty.Smoothness)]
			[SurfaceDataAttributes("Smoothness", false, false, FieldPrecision.Default)]
			public float perceptualSmoothness;

			// Token: 0x040014C4 RID: 5316
			[SurfaceDataAttributes("Transmittance", false, false, FieldPrecision.Default)]
			public Vector3 transmittance;

			// Token: 0x040014C5 RID: 5317
			[SurfaceDataAttributes("RimTransmissionIntensity", false, false, FieldPrecision.Default)]
			public float rimTransmissionIntensity;

			// Token: 0x040014C6 RID: 5318
			[SurfaceDataAttributes("Hair Strand Direction", true, false, FieldPrecision.Default)]
			public Vector3 hairStrandDirectionWS;

			// Token: 0x040014C7 RID: 5319
			[SurfaceDataAttributes("Secondary Smoothness", false, false, FieldPrecision.Default)]
			public float secondaryPerceptualSmoothness;

			// Token: 0x040014C8 RID: 5320
			[MaterialSharedPropertyMapping(MaterialSharedProperty.Specular)]
			[SurfaceDataAttributes("Specular Tint", false, true, FieldPrecision.Default)]
			public Vector3 specularTint;

			// Token: 0x040014C9 RID: 5321
			[SurfaceDataAttributes("Secondary Specular Tint", false, true, FieldPrecision.Default)]
			public Vector3 secondarySpecularTint;

			// Token: 0x040014CA RID: 5322
			[SurfaceDataAttributes("Specular Shift", false, false, FieldPrecision.Default)]
			public float specularShift;

			// Token: 0x040014CB RID: 5323
			[SurfaceDataAttributes("Secondary Specular Shift", false, false, FieldPrecision.Default)]
			public float secondarySpecularShift;
		}

		// Token: 0x02000240 RID: 576
		[GenerateHLSL(PackingRules.Exact, false, false, true, 1450, false, false)]
		public struct BSDFData
		{
			// Token: 0x040014CC RID: 5324
			public uint materialFeatures;

			// Token: 0x040014CD RID: 5325
			public float ambientOcclusion;

			// Token: 0x040014CE RID: 5326
			public float specularOcclusion;

			// Token: 0x040014CF RID: 5327
			[SurfaceDataAttributes("", false, true, FieldPrecision.Default)]
			public Vector3 diffuseColor;

			// Token: 0x040014D0 RID: 5328
			public Vector3 fresnel0;

			// Token: 0x040014D1 RID: 5329
			public Vector3 specularTint;

			// Token: 0x040014D2 RID: 5330
			[SurfaceDataAttributes(new string[] { "Normal WS", "Normal View Space" }, true, false, FieldPrecision.Default)]
			public Vector3 normalWS;

			// Token: 0x040014D3 RID: 5331
			[SurfaceDataAttributes(new string[] { "Geometric Normal", "Geometric Normal View Space" }, true, false, FieldPrecision.Default)]
			public Vector3 geomNormalWS;

			// Token: 0x040014D4 RID: 5332
			public float perceptualRoughness;

			// Token: 0x040014D5 RID: 5333
			public Vector3 transmittance;

			// Token: 0x040014D6 RID: 5334
			public float rimTransmissionIntensity;

			// Token: 0x040014D7 RID: 5335
			[SurfaceDataAttributes("", true, false, FieldPrecision.Default)]
			public Vector3 hairStrandDirectionWS;

			// Token: 0x040014D8 RID: 5336
			public float anisotropy;

			// Token: 0x040014D9 RID: 5337
			public float secondaryPerceptualRoughness;

			// Token: 0x040014DA RID: 5338
			public Vector3 secondarySpecularTint;

			// Token: 0x040014DB RID: 5339
			public float specularExponent;

			// Token: 0x040014DC RID: 5340
			public float secondarySpecularExponent;

			// Token: 0x040014DD RID: 5341
			public float specularShift;

			// Token: 0x040014DE RID: 5342
			public float secondarySpecularShift;
		}
	}
}
