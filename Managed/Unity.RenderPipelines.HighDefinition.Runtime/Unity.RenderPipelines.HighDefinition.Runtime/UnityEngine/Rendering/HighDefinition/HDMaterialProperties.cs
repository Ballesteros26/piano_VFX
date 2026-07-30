using System;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x0200010A RID: 266
	internal static class HDMaterialProperties
	{
		// Token: 0x04000CD9 RID: 3289
		public const string kStencilRef = "_StencilRef";

		// Token: 0x04000CDA RID: 3290
		public const string kStencilWriteMask = "_StencilWriteMask";

		// Token: 0x04000CDB RID: 3291
		public const string kStencilRefDepth = "_StencilRefDepth";

		// Token: 0x04000CDC RID: 3292
		public const string kStencilWriteMaskDepth = "_StencilWriteMaskDepth";

		// Token: 0x04000CDD RID: 3293
		public const string kStencilRefGBuffer = "_StencilRefGBuffer";

		// Token: 0x04000CDE RID: 3294
		public const string kStencilWriteMaskGBuffer = "_StencilWriteMaskGBuffer";

		// Token: 0x04000CDF RID: 3295
		public const string kStencilRefMV = "_StencilRefMV";

		// Token: 0x04000CE0 RID: 3296
		public const string kStencilWriteMaskMV = "_StencilWriteMaskMV";

		// Token: 0x04000CE1 RID: 3297
		public const string kStencilRefDistortionVec = "_StencilRefDistortionVec";

		// Token: 0x04000CE2 RID: 3298
		public const string kStencilWriteMaskDistortionVec = "_StencilWriteMaskDistortionVec";

		// Token: 0x04000CE3 RID: 3299
		public const string kUseSplitLighting = "_RequireSplitLighting";

		// Token: 0x04000CE4 RID: 3300
		public const string kZWrite = "_ZWrite";

		// Token: 0x04000CE5 RID: 3301
		public const string kTransparentZWrite = "_TransparentZWrite";

		// Token: 0x04000CE6 RID: 3302
		public const string kTransparentCullMode = "_TransparentCullMode";

		// Token: 0x04000CE7 RID: 3303
		public const string kZTestTransparent = "_ZTestTransparent";

		// Token: 0x04000CE8 RID: 3304
		public const string kEmissiveColorMap = "_EmissiveColorMap";

		// Token: 0x04000CE9 RID: 3305
		public const string kSurfaceType = "_SurfaceType";

		// Token: 0x04000CEA RID: 3306
		public const string kMaterialID = "_MaterialID";

		// Token: 0x04000CEB RID: 3307
		public const string kTransmissionEnable = "_TransmissionEnable";

		// Token: 0x04000CEC RID: 3308
		public const string kEnableDecals = "_SupportDecals";

		// Token: 0x04000CED RID: 3309
		public const string kSupportDecals = "_SupportDecals";

		// Token: 0x04000CEE RID: 3310
		public const string kEnableSSR = "_ReceivesSSR";

		// Token: 0x04000CEF RID: 3311
		public const string kLayerCount = "_LayerCount";

		// Token: 0x04000CF0 RID: 3312
		public const string kAlphaCutoffEnabled = "_AlphaCutoffEnable";

		// Token: 0x04000CF1 RID: 3313
		public const string kZTestGBuffer = "_ZTestGBuffer";

		// Token: 0x04000CF2 RID: 3314
		public const string kZTestDepthEqualForOpaque = "_ZTestDepthEqualForOpaque";

		// Token: 0x04000CF3 RID: 3315
		public const string kBlendMode = "_BlendMode";

		// Token: 0x04000CF4 RID: 3316
		public const string kEnableFogOnTransparent = "_EnableFogOnTransparent";

		// Token: 0x04000CF5 RID: 3317
		public const string kDistortionDepthTest = "_DistortionDepthTest";

		// Token: 0x04000CF6 RID: 3318
		public const string kDistortionEnable = "_DistortionEnable";

		// Token: 0x04000CF7 RID: 3319
		public const string kZTestModeDistortion = "_ZTestModeDistortion";

		// Token: 0x04000CF8 RID: 3320
		public const string kDistortionBlendMode = "_DistortionBlendMode";

		// Token: 0x04000CF9 RID: 3321
		public const string kTransparentWritingMotionVec = "_TransparentWritingMotionVec";

		// Token: 0x04000CFA RID: 3322
		public const string kEnableBlendModePreserveSpecularLighting = "_EnableBlendModePreserveSpecularLighting";

		// Token: 0x04000CFB RID: 3323
		public const string kEmissionColor = "_EmissionColor";

		// Token: 0x04000CFC RID: 3324
		public const string kTransparentBackfaceEnable = "_TransparentBackfaceEnable";

		// Token: 0x04000CFD RID: 3325
		public const string kDoubleSidedEnable = "_DoubleSidedEnable";

		// Token: 0x04000CFE RID: 3326
		public const string kDoubleSidedNormalMode = "_DoubleSidedNormalMode";

		// Token: 0x04000CFF RID: 3327
		public const string kDistortionOnly = "_DistortionOnly";

		// Token: 0x04000D00 RID: 3328
		public const string kTransparentDepthPrepassEnable = "_TransparentDepthPrepassEnable";

		// Token: 0x04000D01 RID: 3329
		public const string kTransparentDepthPostpassEnable = "_TransparentDepthPostpassEnable";

		// Token: 0x04000D02 RID: 3330
		public const string kTransparentSortPriority = "_TransparentSortPriority";

		// Token: 0x04000D03 RID: 3331
		public const int kMaxLayerCount = 4;

		// Token: 0x04000D04 RID: 3332
		public const string kUVBase = "_UVBase";

		// Token: 0x04000D05 RID: 3333
		public const string kTexWorldScale = "_TexWorldScale";

		// Token: 0x04000D06 RID: 3334
		public const string kUVMappingMask = "_UVMappingMask";

		// Token: 0x04000D07 RID: 3335
		public const string kUVDetail = "_UVDetail";

		// Token: 0x04000D08 RID: 3336
		public const string kUVDetailsMappingMask = "_UVDetailsMappingMask";

		// Token: 0x04000D09 RID: 3337
		public const string kReceivesSSR = "_ReceivesSSR";

		// Token: 0x04000D0A RID: 3338
		public const string kAddPrecomputedVelocity = "_AddPrecomputedVelocity";

		// Token: 0x04000D0B RID: 3339
		public const string kShadowMatteFilter = "_ShadowMatteFilter";

		// Token: 0x04000D0C RID: 3340
		public static readonly Color[] kLayerColors = new Color[]
		{
			Color.white,
			Color.red,
			Color.green,
			Color.blue
		};
	}
}
