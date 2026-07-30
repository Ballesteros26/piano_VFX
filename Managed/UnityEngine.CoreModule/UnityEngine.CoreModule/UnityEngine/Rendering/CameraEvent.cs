using System;

namespace UnityEngine.Rendering
{
	// Token: 0x02000325 RID: 805
	public enum CameraEvent
	{
		// Token: 0x040008D5 RID: 2261
		BeforeDepthTexture,
		// Token: 0x040008D6 RID: 2262
		AfterDepthTexture,
		// Token: 0x040008D7 RID: 2263
		BeforeDepthNormalsTexture,
		// Token: 0x040008D8 RID: 2264
		AfterDepthNormalsTexture,
		// Token: 0x040008D9 RID: 2265
		BeforeGBuffer,
		// Token: 0x040008DA RID: 2266
		AfterGBuffer,
		// Token: 0x040008DB RID: 2267
		BeforeLighting,
		// Token: 0x040008DC RID: 2268
		AfterLighting,
		// Token: 0x040008DD RID: 2269
		BeforeFinalPass,
		// Token: 0x040008DE RID: 2270
		AfterFinalPass,
		// Token: 0x040008DF RID: 2271
		BeforeForwardOpaque,
		// Token: 0x040008E0 RID: 2272
		AfterForwardOpaque,
		// Token: 0x040008E1 RID: 2273
		BeforeImageEffectsOpaque,
		// Token: 0x040008E2 RID: 2274
		AfterImageEffectsOpaque,
		// Token: 0x040008E3 RID: 2275
		BeforeSkybox,
		// Token: 0x040008E4 RID: 2276
		AfterSkybox,
		// Token: 0x040008E5 RID: 2277
		BeforeForwardAlpha,
		// Token: 0x040008E6 RID: 2278
		AfterForwardAlpha,
		// Token: 0x040008E7 RID: 2279
		BeforeImageEffects,
		// Token: 0x040008E8 RID: 2280
		AfterImageEffects,
		// Token: 0x040008E9 RID: 2281
		AfterEverything,
		// Token: 0x040008EA RID: 2282
		BeforeReflections,
		// Token: 0x040008EB RID: 2283
		AfterReflections,
		// Token: 0x040008EC RID: 2284
		BeforeHaloAndLensFlares,
		// Token: 0x040008ED RID: 2285
		AfterHaloAndLensFlares
	}
}
