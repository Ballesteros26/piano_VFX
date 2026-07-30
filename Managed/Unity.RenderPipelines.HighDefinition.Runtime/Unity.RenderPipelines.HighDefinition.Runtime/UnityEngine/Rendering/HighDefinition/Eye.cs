using System;
using UnityEngine.Rendering.HighDefinition.Attributes;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x020000B6 RID: 182
	internal class Eye : RenderPipelineMaterial
	{
		// Token: 0x02000238 RID: 568
		[GenerateHLSL(PackingRules.Exact, true, false, false, 1, false, false)]
		public enum MaterialFeatureFlags
		{
			// Token: 0x0400147C RID: 5244
			EyeCinematic = 1,
			// Token: 0x0400147D RID: 5245
			EyeSubsurfaceScattering
		}

		// Token: 0x02000239 RID: 569
		[GenerateHLSL(PackingRules.Exact, false, false, true, 1500, false, false)]
		public struct SurfaceData
		{
			// Token: 0x0400147E RID: 5246
			[SurfaceDataAttributes("MaterialFeatures", false, false, FieldPrecision.Default)]
			public uint materialFeatures;

			// Token: 0x0400147F RID: 5247
			[MaterialSharedPropertyMapping(MaterialSharedProperty.Albedo)]
			[SurfaceDataAttributes("Base Color", false, true, FieldPrecision.Default)]
			public Vector3 baseColor;

			// Token: 0x04001480 RID: 5248
			[MaterialSharedPropertyMapping(MaterialSharedProperty.Normal)]
			[SurfaceDataAttributes(new string[] { "Normal", "Normal View Space" }, true, false, FieldPrecision.Default)]
			public Vector3 normalWS;

			// Token: 0x04001481 RID: 5249
			[SurfaceDataAttributes(new string[] { "Iris Normal", "Iris Normal View Space" }, true, false, FieldPrecision.Default)]
			public Vector3 irisNormalWS;

			// Token: 0x04001482 RID: 5250
			[SurfaceDataAttributes(new string[] { "Geometric Normal", "Geometric Normal View Space" }, true, false, FieldPrecision.Default)]
			public Vector3 geomNormalWS;

			// Token: 0x04001483 RID: 5251
			[MaterialSharedPropertyMapping(MaterialSharedProperty.Smoothness)]
			[SurfaceDataAttributes("Smoothness", false, false, FieldPrecision.Default)]
			public float perceptualSmoothness;

			// Token: 0x04001484 RID: 5252
			[MaterialSharedPropertyMapping(MaterialSharedProperty.AmbientOcclusion)]
			[SurfaceDataAttributes("Ambient Occlusion", false, false, FieldPrecision.Default)]
			public float ambientOcclusion;

			// Token: 0x04001485 RID: 5253
			[SurfaceDataAttributes("Specular Occlusion", false, false, FieldPrecision.Default)]
			public float specularOcclusion;

			// Token: 0x04001486 RID: 5254
			[MaterialSharedPropertyMapping(MaterialSharedProperty.Specular)]
			[SurfaceDataAttributes("IOR", false, true, FieldPrecision.Default)]
			public float IOR;

			// Token: 0x04001487 RID: 5255
			[SurfaceDataAttributes("Mask", false, true, FieldPrecision.Default)]
			public Vector2 mask;

			// Token: 0x04001488 RID: 5256
			[SurfaceDataAttributes("Diffusion Profile Hash", false, false, FieldPrecision.Default)]
			public uint diffusionProfileHash;

			// Token: 0x04001489 RID: 5257
			[SurfaceDataAttributes("Subsurface Mask", false, false, FieldPrecision.Default)]
			public float subsurfaceMask;
		}

		// Token: 0x0200023A RID: 570
		[GenerateHLSL(PackingRules.Exact, false, false, true, 1550, false, false)]
		public struct BSDFData
		{
			// Token: 0x0400148A RID: 5258
			public uint materialFeatures;

			// Token: 0x0400148B RID: 5259
			[SurfaceDataAttributes("", false, true, FieldPrecision.Default)]
			public Vector3 diffuseColor;

			// Token: 0x0400148C RID: 5260
			public Vector3 fresnel0;

			// Token: 0x0400148D RID: 5261
			public float IOR;

			// Token: 0x0400148E RID: 5262
			public float ambientOcclusion;

			// Token: 0x0400148F RID: 5263
			public float specularOcclusion;

			// Token: 0x04001490 RID: 5264
			[SurfaceDataAttributes(new string[] { "Normal WS", "Normal View Space" }, true, false, FieldPrecision.Default)]
			public Vector3 normalWS;

			// Token: 0x04001491 RID: 5265
			[SurfaceDataAttributes(new string[] { "Diffuse Normal WS", "Diffuse Normal View Space" }, true, false, FieldPrecision.Default)]
			public Vector3 diffuseNormalWS;

			// Token: 0x04001492 RID: 5266
			[SurfaceDataAttributes(new string[] { "Geometric Normal", "Geometric Normal View Space" }, true, false, FieldPrecision.Default)]
			public Vector3 geomNormalWS;

			// Token: 0x04001493 RID: 5267
			public float perceptualRoughness;

			// Token: 0x04001494 RID: 5268
			public Vector2 mask;

			// Token: 0x04001495 RID: 5269
			public uint diffusionProfileIndex;

			// Token: 0x04001496 RID: 5270
			public float subsurfaceMask;

			// Token: 0x04001497 RID: 5271
			public float roughness;
		}
	}
}
