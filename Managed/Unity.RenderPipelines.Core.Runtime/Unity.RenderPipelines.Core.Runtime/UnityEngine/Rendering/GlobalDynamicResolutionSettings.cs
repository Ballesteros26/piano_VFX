using System;

namespace UnityEngine.Rendering
{
	// Token: 0x02000024 RID: 36
	[Serializable]
	public struct GlobalDynamicResolutionSettings
	{
		// Token: 0x060000D2 RID: 210 RVA: 0x000052B8 File Offset: 0x000034B8
		public static GlobalDynamicResolutionSettings NewDefault()
		{
			return new GlobalDynamicResolutionSettings
			{
				maxPercentage = 100f,
				minPercentage = 100f,
				dynResType = DynamicResolutionType.Hardware,
				upsampleFilter = DynamicResUpscaleFilter.CatmullRom,
				forcedPercentage = 100f
			};
		}

		// Token: 0x040000AD RID: 173
		public bool enabled;

		// Token: 0x040000AE RID: 174
		public float maxPercentage;

		// Token: 0x040000AF RID: 175
		public float minPercentage;

		// Token: 0x040000B0 RID: 176
		public DynamicResolutionType dynResType;

		// Token: 0x040000B1 RID: 177
		public DynamicResUpscaleFilter upsampleFilter;

		// Token: 0x040000B2 RID: 178
		public bool forceResolution;

		// Token: 0x040000B3 RID: 179
		public float forcedPercentage;
	}
}
