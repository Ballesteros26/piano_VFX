using System;
using UnityEngine.Rendering.HighDefinition.Attributes;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x020000C4 RID: 196
	internal class StackLit : RenderPipelineMaterial
	{
		// Token: 0x06000731 RID: 1841 RVA: 0x00037BF5 File Offset: 0x00035DF5
		public override void Build(HDRenderPipelineAsset hdAsset, RenderPipelineResources defaultResources)
		{
			PreIntegratedFGD.instance.Build(PreIntegratedFGD.FGDIndex.FGD_GGXAndDisneyDiffuse);
			LTCAreaLight.instance.Build();
			SPTDistribution.instance.Build();
		}

		// Token: 0x06000732 RID: 1842 RVA: 0x00037C16 File Offset: 0x00035E16
		public override void Cleanup()
		{
			PreIntegratedFGD.instance.Cleanup(PreIntegratedFGD.FGDIndex.FGD_GGXAndDisneyDiffuse);
			LTCAreaLight.instance.Cleanup();
			SPTDistribution.instance.Cleanup();
		}

		// Token: 0x06000733 RID: 1843 RVA: 0x00036EC4 File Offset: 0x000350C4
		public override void RenderInit(CommandBuffer cmd)
		{
			PreIntegratedFGD.instance.RenderInit(PreIntegratedFGD.FGDIndex.FGD_GGXAndDisneyDiffuse, cmd);
		}

		// Token: 0x06000734 RID: 1844 RVA: 0x00037C37 File Offset: 0x00035E37
		public override void Bind(CommandBuffer cmd)
		{
			PreIntegratedFGD.instance.Bind(cmd, PreIntegratedFGD.FGDIndex.FGD_GGXAndDisneyDiffuse);
			LTCAreaLight.instance.Bind(cmd);
			SPTDistribution.instance.Bind(cmd);
		}

		// Token: 0x02000246 RID: 582
		[GenerateHLSL(PackingRules.Exact, true, false, false, 1, false, false)]
		public enum MaterialFeatureFlags
		{
			// Token: 0x0400151C RID: 5404
			StackLitStandard = 1,
			// Token: 0x0400151D RID: 5405
			StackLitDualSpecularLobe,
			// Token: 0x0400151E RID: 5406
			StackLitAnisotropy = 4,
			// Token: 0x0400151F RID: 5407
			StackLitCoat = 8,
			// Token: 0x04001520 RID: 5408
			StackLitIridescence = 16,
			// Token: 0x04001521 RID: 5409
			StackLitSubsurfaceScattering = 32,
			// Token: 0x04001522 RID: 5410
			StackLitTransmission = 64,
			// Token: 0x04001523 RID: 5411
			StackLitCoatNormalMap = 128,
			// Token: 0x04001524 RID: 5412
			StackLitSpecularColor = 256,
			// Token: 0x04001525 RID: 5413
			StackLitHazyGloss = 512
		}

		// Token: 0x02000247 RID: 583
		public enum BaseParametrization
		{
			// Token: 0x04001527 RID: 5415
			BaseMetallic,
			// Token: 0x04001528 RID: 5416
			SpecularColor
		}

		// Token: 0x02000248 RID: 584
		public enum DualSpecularLobeParametrization
		{
			// Token: 0x0400152A RID: 5418
			Direct,
			// Token: 0x0400152B RID: 5419
			HazyGloss
		}

		// Token: 0x02000249 RID: 585
		[GenerateHLSL(PackingRules.Exact, false, false, true, 1100, false, false)]
		public struct SurfaceData
		{
			// Token: 0x0400152C RID: 5420
			[SurfaceDataAttributes("Material Features", false, false, FieldPrecision.Default)]
			public uint materialFeatures;

			// Token: 0x0400152D RID: 5421
			[MaterialSharedPropertyMapping(MaterialSharedProperty.Albedo)]
			[SurfaceDataAttributes("Base Color", false, true, FieldPrecision.Default)]
			public Vector3 baseColor;

			// Token: 0x0400152E RID: 5422
			[MaterialSharedPropertyMapping(MaterialSharedProperty.AmbientOcclusion)]
			[SurfaceDataAttributes("Ambient Occlusion", false, false, FieldPrecision.Default)]
			public float ambientOcclusion;

			// Token: 0x0400152F RID: 5423
			[MaterialSharedPropertyMapping(MaterialSharedProperty.Metal)]
			[SurfaceDataAttributes("Metallic", false, false, FieldPrecision.Default)]
			public float metallic;

			// Token: 0x04001530 RID: 5424
			[SurfaceDataAttributes("Dielectric IOR", false, false, FieldPrecision.Default)]
			public float dielectricIor;

			// Token: 0x04001531 RID: 5425
			[MaterialSharedPropertyMapping(MaterialSharedProperty.Specular)]
			[SurfaceDataAttributes("Specular Color", false, true, FieldPrecision.Default)]
			public Vector3 specularColor;

			// Token: 0x04001532 RID: 5426
			[MaterialSharedPropertyMapping(MaterialSharedProperty.Normal)]
			[SurfaceDataAttributes(new string[] { "Normal", "Normal View Space" }, true, false, FieldPrecision.Default)]
			public Vector3 normalWS;

			// Token: 0x04001533 RID: 5427
			[SurfaceDataAttributes(new string[] { "Geometric Normal", "Geometric Normal View Space" }, true, false, FieldPrecision.Default)]
			public Vector3 geomNormalWS;

			// Token: 0x04001534 RID: 5428
			[SurfaceDataAttributes(new string[] { "Coat Normal", "Coat Normal View Space" }, true, false, FieldPrecision.Default)]
			public Vector3 coatNormalWS;

			// Token: 0x04001535 RID: 5429
			[SurfaceDataAttributes(new string[] { "Bent Normal", "Bent Normal View Space" }, true, false, FieldPrecision.Default)]
			public Vector3 bentNormalWS;

			// Token: 0x04001536 RID: 5430
			[MaterialSharedPropertyMapping(MaterialSharedProperty.Smoothness)]
			[SurfaceDataAttributes("Smoothness A", false, false, FieldPrecision.Default)]
			public float perceptualSmoothnessA;

			// Token: 0x04001537 RID: 5431
			[SurfaceDataAttributes("Smoothness B", false, false, FieldPrecision.Default)]
			public float perceptualSmoothnessB;

			// Token: 0x04001538 RID: 5432
			[SurfaceDataAttributes("Lobe Mixing", false, false, FieldPrecision.Default)]
			public float lobeMix;

			// Token: 0x04001539 RID: 5433
			[SurfaceDataAttributes("Haziness", false, false, FieldPrecision.Default)]
			public float haziness;

			// Token: 0x0400153A RID: 5434
			[SurfaceDataAttributes("Haze Extent", false, false, FieldPrecision.Default)]
			public float hazeExtent;

			// Token: 0x0400153B RID: 5435
			[SurfaceDataAttributes("Hazy Gloss Max Dielectric f0 When Using Metallic Input", false, false, FieldPrecision.Default)]
			public float hazyGlossMaxDielectricF0;

			// Token: 0x0400153C RID: 5436
			[SurfaceDataAttributes("Tangent", true, false, FieldPrecision.Default)]
			public Vector3 tangentWS;

			// Token: 0x0400153D RID: 5437
			[SurfaceDataAttributes("AnisotropyA", false, false, FieldPrecision.Default)]
			public float anisotropyA;

			// Token: 0x0400153E RID: 5438
			[SurfaceDataAttributes("AnisotropyB", false, false, FieldPrecision.Default)]
			public float anisotropyB;

			// Token: 0x0400153F RID: 5439
			[SurfaceDataAttributes("Iridescence Ior", false, false, FieldPrecision.Default)]
			public float iridescenceIor;

			// Token: 0x04001540 RID: 5440
			[SurfaceDataAttributes("Iridescence Layer Thickness", false, false, FieldPrecision.Default)]
			public float iridescenceThickness;

			// Token: 0x04001541 RID: 5441
			[SurfaceDataAttributes("Iridescence Mask", false, false, FieldPrecision.Default)]
			public float iridescenceMask;

			// Token: 0x04001542 RID: 5442
			[SurfaceDataAttributes("Iridescence Coat Fixup TIR", false, false, FieldPrecision.Default)]
			public float iridescenceCoatFixupTIR;

			// Token: 0x04001543 RID: 5443
			[SurfaceDataAttributes("Iridescence Coat Fixup TIR Clamp", false, false, FieldPrecision.Default)]
			public float iridescenceCoatFixupTIRClamp;

			// Token: 0x04001544 RID: 5444
			[SurfaceDataAttributes("Coat Smoothness", false, false, FieldPrecision.Default)]
			public float coatPerceptualSmoothness;

			// Token: 0x04001545 RID: 5445
			[SurfaceDataAttributes("Coat mask", false, false, FieldPrecision.Default)]
			public float coatMask;

			// Token: 0x04001546 RID: 5446
			[SurfaceDataAttributes("Coat IOR", false, false, FieldPrecision.Default)]
			public float coatIor;

			// Token: 0x04001547 RID: 5447
			[SurfaceDataAttributes("Coat Thickness", false, false, FieldPrecision.Default)]
			public float coatThickness;

			// Token: 0x04001548 RID: 5448
			[SurfaceDataAttributes("Coat Extinction Coefficient", false, false, FieldPrecision.Default)]
			public Vector3 coatExtinction;

			// Token: 0x04001549 RID: 5449
			[SurfaceDataAttributes("Diffusion Profile Hash", false, false, FieldPrecision.Default)]
			public uint diffusionProfileHash;

			// Token: 0x0400154A RID: 5450
			[SurfaceDataAttributes("Subsurface Mask", false, false, FieldPrecision.Default)]
			public float subsurfaceMask;

			// Token: 0x0400154B RID: 5451
			[SurfaceDataAttributes("Thickness", false, false, FieldPrecision.Default)]
			public float thickness;

			// Token: 0x0400154C RID: 5452
			[SurfaceDataAttributes("Specular Occlusion From Custom Input", false, false, FieldPrecision.Default)]
			public float specularOcclusionCustomInput;

			// Token: 0x0400154D RID: 5453
			[SurfaceDataAttributes("Specular Occlusion Fixup Visibility Ratio Threshold", false, false, FieldPrecision.Default)]
			public float soFixupVisibilityRatioThreshold;

			// Token: 0x0400154E RID: 5454
			[SurfaceDataAttributes("Specular Occlusion Fixup Strength", false, false, FieldPrecision.Default)]
			public float soFixupStrengthFactor;

			// Token: 0x0400154F RID: 5455
			[SurfaceDataAttributes("Specular Occlusion Fixup Max Added Roughness", false, false, FieldPrecision.Default)]
			public float soFixupMaxAddedRoughness;
		}

		// Token: 0x0200024A RID: 586
		[GenerateHLSL(PackingRules.Exact, false, false, true, 1150, false, false)]
		public struct BSDFData
		{
			// Token: 0x04001550 RID: 5456
			public uint materialFeatures;

			// Token: 0x04001551 RID: 5457
			[SurfaceDataAttributes("", false, true, FieldPrecision.Default)]
			public Vector3 diffuseColor;

			// Token: 0x04001552 RID: 5458
			public Vector3 fresnel0;

			// Token: 0x04001553 RID: 5459
			public float ambientOcclusion;

			// Token: 0x04001554 RID: 5460
			[SurfaceDataAttributes(new string[] { "Normal WS", "Normal View Space" }, true, false, FieldPrecision.Default)]
			public Vector3 normalWS;

			// Token: 0x04001555 RID: 5461
			[SurfaceDataAttributes(new string[] { "Geometric Normal", "Geometric Normal View Space" }, true, false, FieldPrecision.Default)]
			public Vector3 geomNormalWS;

			// Token: 0x04001556 RID: 5462
			[SurfaceDataAttributes(new string[] { "Coat Normal", "Coat Normal View Space" }, true, false, FieldPrecision.Default)]
			public Vector3 coatNormalWS;

			// Token: 0x04001557 RID: 5463
			[SurfaceDataAttributes(new string[] { "Bent Normal", "Bent Normal View Space" }, true, false, FieldPrecision.Default)]
			public Vector3 bentNormalWS;

			// Token: 0x04001558 RID: 5464
			public float perceptualRoughnessA;

			// Token: 0x04001559 RID: 5465
			public float perceptualRoughnessB;

			// Token: 0x0400155A RID: 5466
			public float lobeMix;

			// Token: 0x0400155B RID: 5467
			[SurfaceDataAttributes("", true, false, FieldPrecision.Default)]
			public Vector3 tangentWS;

			// Token: 0x0400155C RID: 5468
			[SurfaceDataAttributes("", true, false, FieldPrecision.Default)]
			public Vector3 bitangentWS;

			// Token: 0x0400155D RID: 5469
			public float roughnessAT;

			// Token: 0x0400155E RID: 5470
			public float roughnessAB;

			// Token: 0x0400155F RID: 5471
			public float roughnessBT;

			// Token: 0x04001560 RID: 5472
			public float roughnessBB;

			// Token: 0x04001561 RID: 5473
			public float anisotropyA;

			// Token: 0x04001562 RID: 5474
			public float anisotropyB;

			// Token: 0x04001563 RID: 5475
			public float coatRoughness;

			// Token: 0x04001564 RID: 5476
			public float coatPerceptualRoughness;

			// Token: 0x04001565 RID: 5477
			public float coatMask;

			// Token: 0x04001566 RID: 5478
			public float coatIor;

			// Token: 0x04001567 RID: 5479
			public float coatThickness;

			// Token: 0x04001568 RID: 5480
			public Vector3 coatExtinction;

			// Token: 0x04001569 RID: 5481
			public float iridescenceIor;

			// Token: 0x0400156A RID: 5482
			public float iridescenceThickness;

			// Token: 0x0400156B RID: 5483
			public float iridescenceMask;

			// Token: 0x0400156C RID: 5484
			public float iridescenceCoatFixupTIR;

			// Token: 0x0400156D RID: 5485
			public float iridescenceCoatFixupTIRClamp;

			// Token: 0x0400156E RID: 5486
			public uint diffusionProfileIndex;

			// Token: 0x0400156F RID: 5487
			public float subsurfaceMask;

			// Token: 0x04001570 RID: 5488
			public float thickness;

			// Token: 0x04001571 RID: 5489
			public bool useThickObjectMode;

			// Token: 0x04001572 RID: 5490
			public Vector3 transmittance;

			// Token: 0x04001573 RID: 5491
			public float specularOcclusionCustomInput;

			// Token: 0x04001574 RID: 5492
			public float soFixupVisibilityRatioThreshold;

			// Token: 0x04001575 RID: 5493
			public float soFixupStrengthFactor;

			// Token: 0x04001576 RID: 5494
			public float soFixupMaxAddedRoughness;
		}
	}
}
