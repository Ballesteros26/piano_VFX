using System;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x020000B1 RID: 177
	[Serializable]
	public struct GlobalDecalSettings
	{
		// Token: 0x060006B2 RID: 1714 RVA: 0x00035B08 File Offset: 0x00033D08
		internal static GlobalDecalSettings NewDefault()
		{
			return new GlobalDecalSettings
			{
				drawDistance = 1000,
				atlasWidth = 4096,
				atlasHeight = 4096
			};
		}

		// Token: 0x040006EC RID: 1772
		public int drawDistance;

		// Token: 0x040006ED RID: 1773
		public int atlasWidth;

		// Token: 0x040006EE RID: 1774
		public int atlasHeight;

		// Token: 0x040006EF RID: 1775
		public bool perChannelMask;
	}
}
