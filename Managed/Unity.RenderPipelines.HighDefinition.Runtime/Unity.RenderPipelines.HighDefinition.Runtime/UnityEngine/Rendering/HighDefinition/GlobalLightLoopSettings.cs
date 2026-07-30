using System;
using UnityEngine.Serialization;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x02000066 RID: 102
	[Serializable]
	public struct GlobalLightLoopSettings
	{
		// Token: 0x060002BB RID: 699 RVA: 0x0000F788 File Offset: 0x0000D988
		internal static GlobalLightLoopSettings NewDefault()
		{
			return new GlobalLightLoopSettings
			{
				cookieAtlasSize = CookieAtlasResolution.CookieResolution2048,
				cookieFormat = CookieAtlasGraphicsFormat.R11G11B10,
				pointCookieSize = CubeCookieResolution.CubeCookieResolution128,
				cubeCookieTexArraySize = 16,
				cookieAtlasLastValidMip = 0,
				cookieTexArraySize = 1,
				planarReflectionAtlasSize = PlanarReflectionAtlasResolution.PlanarReflectionResolution1024,
				reflectionProbeCacheSize = 64,
				reflectionCubemapSize = CubeReflectionResolution.CubeReflectionResolution256,
				skyReflectionSize = SkyResolution.SkyResolution256,
				skyLightingOverrideLayerMask = 0,
				maxDirectionalLightsOnScreen = 16,
				maxPunctualLightsOnScreen = 512,
				maxAreaLightsOnScreen = 64,
				maxEnvLightsOnScreen = 64,
				maxDecalsOnScreen = 512,
				maxPlanarReflectionOnScreen = 16
			};
		}

		// Token: 0x04000335 RID: 821
		internal static readonly GlobalLightLoopSettings @default;

		// Token: 0x04000336 RID: 822
		[FormerlySerializedAs("cookieSize")]
		public CookieAtlasResolution cookieAtlasSize;

		// Token: 0x04000337 RID: 823
		public CookieAtlasGraphicsFormat cookieFormat;

		// Token: 0x04000338 RID: 824
		public CubeCookieResolution pointCookieSize;

		// Token: 0x04000339 RID: 825
		public int cubeCookieTexArraySize;

		// Token: 0x0400033A RID: 826
		public int cookieAtlasLastValidMip;

		// Token: 0x0400033B RID: 827
		[SerializeField]
		[Obsolete("There is no more texture array for cookies, use cookie atlases properties instead.")]
		internal int cookieTexArraySize;

		// Token: 0x0400033C RID: 828
		[FormerlySerializedAs("planarReflectionTextureSize")]
		public PlanarReflectionAtlasResolution planarReflectionAtlasSize;

		// Token: 0x0400033D RID: 829
		public int reflectionProbeCacheSize;

		// Token: 0x0400033E RID: 830
		public CubeReflectionResolution reflectionCubemapSize;

		// Token: 0x0400033F RID: 831
		public bool reflectionCacheCompressed;

		// Token: 0x04000340 RID: 832
		public bool planarReflectionCacheCompressed;

		// Token: 0x04000341 RID: 833
		public SkyResolution skyReflectionSize;

		// Token: 0x04000342 RID: 834
		public LayerMask skyLightingOverrideLayerMask;

		// Token: 0x04000343 RID: 835
		public bool supportFabricConvolution;

		// Token: 0x04000344 RID: 836
		public int maxDirectionalLightsOnScreen;

		// Token: 0x04000345 RID: 837
		public int maxPunctualLightsOnScreen;

		// Token: 0x04000346 RID: 838
		public int maxAreaLightsOnScreen;

		// Token: 0x04000347 RID: 839
		public int maxEnvLightsOnScreen;

		// Token: 0x04000348 RID: 840
		public int maxDecalsOnScreen;

		// Token: 0x04000349 RID: 841
		public int maxPlanarReflectionOnScreen;
	}
}
