using System;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x020000FB RID: 251
	[Serializable]
	public sealed class GlobalLightingQualitySettings
	{
		// Token: 0x06000838 RID: 2104 RVA: 0x00041C54 File Offset: 0x0003FE54
		internal GlobalLightingQualitySettings()
		{
			this.AOStepCount[0] = 4;
			this.AOStepCount[1] = 6;
			this.AOStepCount[2] = 16;
			this.AOFullRes[0] = false;
			this.AOFullRes[1] = false;
			this.AOFullRes[2] = true;
			this.AOBilateralUpsample[0] = false;
			this.AOBilateralUpsample[1] = true;
			this.AOBilateralUpsample[2] = true;
			this.AODirectionCount[0] = 1;
			this.AODirectionCount[1] = 2;
			this.AODirectionCount[2] = 4;
			this.AOMaximumRadiusPixels[0] = 32;
			this.AOMaximumRadiusPixels[1] = 40;
			this.AOMaximumRadiusPixels[2] = 80;
			this.ContactShadowSampleCount[0] = 6;
			this.ContactShadowSampleCount[1] = 10;
			this.ContactShadowSampleCount[2] = 16;
			this.SSRMaxRaySteps[0] = 16;
			this.SSRMaxRaySteps[1] = 32;
			this.SSRMaxRaySteps[2] = 64;
		}

		// Token: 0x06000839 RID: 2105 RVA: 0x00041D9D File Offset: 0x0003FF9D
		internal static GlobalLightingQualitySettings NewDefault()
		{
			return new GlobalLightingQualitySettings();
		}

		// Token: 0x040008DE RID: 2270
		private static int s_QualitySettingCount = Enum.GetNames(typeof(ScalableSettingLevelParameter.Level)).Length;

		// Token: 0x040008DF RID: 2271
		public int[] AOStepCount = new int[GlobalLightingQualitySettings.s_QualitySettingCount];

		// Token: 0x040008E0 RID: 2272
		public bool[] AOFullRes = new bool[GlobalLightingQualitySettings.s_QualitySettingCount];

		// Token: 0x040008E1 RID: 2273
		public int[] AOMaximumRadiusPixels = new int[GlobalLightingQualitySettings.s_QualitySettingCount];

		// Token: 0x040008E2 RID: 2274
		public bool[] AOBilateralUpsample = new bool[GlobalLightingQualitySettings.s_QualitySettingCount];

		// Token: 0x040008E3 RID: 2275
		public int[] AODirectionCount = new int[GlobalLightingQualitySettings.s_QualitySettingCount];

		// Token: 0x040008E4 RID: 2276
		public int[] ContactShadowSampleCount = new int[GlobalLightingQualitySettings.s_QualitySettingCount];

		// Token: 0x040008E5 RID: 2277
		public int[] SSRMaxRaySteps = new int[GlobalLightingQualitySettings.s_QualitySettingCount];
	}
}
