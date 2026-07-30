using System;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x02000029 RID: 41
	[GenerateHLSL(PackingRules.Exact, true, false, false, 1, false, false)]
	public enum FullScreenDebugMode
	{
		// Token: 0x040000A9 RID: 169
		None,
		// Token: 0x040000AA RID: 170
		MinLightingFullScreenDebug,
		// Token: 0x040000AB RID: 171
		SSAO,
		// Token: 0x040000AC RID: 172
		ScreenSpaceReflections,
		// Token: 0x040000AD RID: 173
		ContactShadows,
		// Token: 0x040000AE RID: 174
		ContactShadowsFade,
		// Token: 0x040000AF RID: 175
		ScreenSpaceShadows,
		// Token: 0x040000B0 RID: 176
		PreRefractionColorPyramid,
		// Token: 0x040000B1 RID: 177
		DepthPyramid,
		// Token: 0x040000B2 RID: 178
		FinalColorPyramid,
		// Token: 0x040000B3 RID: 179
		LightCluster,
		// Token: 0x040000B4 RID: 180
		RayTracedGlobalIllumination,
		// Token: 0x040000B5 RID: 181
		RecursiveRayTracing,
		// Token: 0x040000B6 RID: 182
		RayTracedSubSurface,
		// Token: 0x040000B7 RID: 183
		MaxLightingFullScreenDebug,
		// Token: 0x040000B8 RID: 184
		MinRenderingFullScreenDebug,
		// Token: 0x040000B9 RID: 185
		MotionVectors,
		// Token: 0x040000BA RID: 186
		NanTracker,
		// Token: 0x040000BB RID: 187
		ColorLog,
		// Token: 0x040000BC RID: 188
		DepthOfFieldCoc,
		// Token: 0x040000BD RID: 189
		TransparencyOverdraw,
		// Token: 0x040000BE RID: 190
		MaxRenderingFullScreenDebug,
		// Token: 0x040000BF RID: 191
		MinMaterialFullScreenDebug,
		// Token: 0x040000C0 RID: 192
		ValidateDiffuseColor,
		// Token: 0x040000C1 RID: 193
		ValidateSpecularColor,
		// Token: 0x040000C2 RID: 194
		MaxMaterialFullScreenDebug
	}
}
