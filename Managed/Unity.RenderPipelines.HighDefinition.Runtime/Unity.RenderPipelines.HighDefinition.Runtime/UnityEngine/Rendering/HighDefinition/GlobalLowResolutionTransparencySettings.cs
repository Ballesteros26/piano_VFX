using System;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x020000FD RID: 253
	[Serializable]
	public struct GlobalLowResolutionTransparencySettings
	{
		// Token: 0x0600083B RID: 2107 RVA: 0x00041DBC File Offset: 0x0003FFBC
		internal static GlobalLowResolutionTransparencySettings NewDefault()
		{
			return new GlobalLowResolutionTransparencySettings
			{
				enabled = true,
				checkerboardDepthBuffer = true,
				upsampleType = LowResTransparentUpsample.NearestDepth
			};
		}

		// Token: 0x040008E9 RID: 2281
		public bool enabled;

		// Token: 0x040008EA RID: 2282
		public bool checkerboardDepthBuffer;

		// Token: 0x040008EB RID: 2283
		public LowResTransparentUpsample upsampleType;
	}
}
