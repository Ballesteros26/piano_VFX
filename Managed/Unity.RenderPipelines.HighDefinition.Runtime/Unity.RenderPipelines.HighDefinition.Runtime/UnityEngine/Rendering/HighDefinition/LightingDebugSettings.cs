using System;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x02000032 RID: 50
	[Serializable]
	public class LightingDebugSettings
	{
		// Token: 0x06000170 RID: 368 RVA: 0x00009AA4 File Offset: 0x00007CA4
		public bool IsDebugDisplayEnabled()
		{
			return this.debugLightingMode != DebugLightingMode.None || this.debugLightFilterMode != DebugLightFilterMode.None || this.overrideSmoothness || this.overrideAlbedo || this.overrideNormal || this.overrideAmbientOcclusion || this.overrideSpecularColor || this.overrideEmissiveColor || this.shadowDebugMode == ShadowMapDebugMode.SingleShadow;
		}

		// Token: 0x06000171 RID: 369 RVA: 0x00009AFC File Offset: 0x00007CFC
		internal bool IsDebugDisplayRemovePostprocess()
		{
			return this.debugLightingMode != DebugLightingMode.None && this.debugLightingMode != DebugLightingMode.MatcapView;
		}

		// Token: 0x04000113 RID: 275
		public DebugLightFilterMode debugLightFilterMode;

		// Token: 0x04000114 RID: 276
		public DebugLightingMode debugLightingMode;

		// Token: 0x04000115 RID: 277
		public ShadowMapDebugMode shadowDebugMode;

		// Token: 0x04000116 RID: 278
		public bool shadowDebugUseSelection;

		// Token: 0x04000117 RID: 279
		public uint shadowMapIndex;

		// Token: 0x04000118 RID: 280
		public float shadowMinValue;

		// Token: 0x04000119 RID: 281
		public float shadowMaxValue = 1f;

		// Token: 0x0400011A RID: 282
		public float shadowResolutionScaleFactor = 1f;

		// Token: 0x0400011B RID: 283
		public bool clearShadowAtlas;

		// Token: 0x0400011C RID: 284
		public bool overrideSmoothness;

		// Token: 0x0400011D RID: 285
		public float overrideSmoothnessValue = 0.5f;

		// Token: 0x0400011E RID: 286
		public bool overrideAlbedo;

		// Token: 0x0400011F RID: 287
		public Color overrideAlbedoValue = new Color(0.5f, 0.5f, 0.5f);

		// Token: 0x04000120 RID: 288
		public bool overrideNormal;

		// Token: 0x04000121 RID: 289
		public bool overrideAmbientOcclusion;

		// Token: 0x04000122 RID: 290
		public float overrideAmbientOcclusionValue = 1f;

		// Token: 0x04000123 RID: 291
		public bool overrideSpecularColor;

		// Token: 0x04000124 RID: 292
		public Color overrideSpecularColorValue = new Color(1f, 1f, 1f);

		// Token: 0x04000125 RID: 293
		public bool overrideEmissiveColor;

		// Token: 0x04000126 RID: 294
		public Color overrideEmissiveColorValue = new Color(1f, 1f, 1f);

		// Token: 0x04000127 RID: 295
		public bool displaySkyReflection;

		// Token: 0x04000128 RID: 296
		public float skyReflectionMipmap;

		// Token: 0x04000129 RID: 297
		public bool displayLightVolumes;

		// Token: 0x0400012A RID: 298
		public LightVolumeDebug lightVolumeDebugByCategory;

		// Token: 0x0400012B RID: 299
		public uint maxDebugLightCount = 24U;

		// Token: 0x0400012C RID: 300
		public float debugExposure;

		// Token: 0x0400012D RID: 301
		public bool displayCookieAtlas;

		// Token: 0x0400012E RID: 302
		public bool displayCookieCubeArray;

		// Token: 0x0400012F RID: 303
		public uint cookieCubeArraySliceIndex;

		// Token: 0x04000130 RID: 304
		public uint cookieAtlasMipLevel;

		// Token: 0x04000131 RID: 305
		public bool clearCookieAtlas;

		// Token: 0x04000132 RID: 306
		public bool displayPlanarReflectionProbeAtlas;

		// Token: 0x04000133 RID: 307
		public uint planarReflectionProbeMipLevel;

		// Token: 0x04000134 RID: 308
		public bool clearPlanarReflectionProbeAtlas;

		// Token: 0x04000135 RID: 309
		public bool showPunctualLight = true;

		// Token: 0x04000136 RID: 310
		public bool showDirectionalLight = true;

		// Token: 0x04000137 RID: 311
		public bool showAreaLight = true;

		// Token: 0x04000138 RID: 312
		public bool showReflectionProbe = true;

		// Token: 0x04000139 RID: 313
		public TileClusterDebug tileClusterDebug;

		// Token: 0x0400013A RID: 314
		public TileClusterCategoryDebug tileClusterDebugByCategory = TileClusterCategoryDebug.Punctual;
	}
}
