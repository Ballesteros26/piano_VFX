using System;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x020000FF RID: 255
	[Serializable]
	public sealed class GlobalPostProcessingQualitySettings
	{
		// Token: 0x0600083F RID: 2111 RVA: 0x00041E44 File Offset: 0x00040044
		internal GlobalPostProcessingQualitySettings()
		{
			this.NearBlurSampleCount[0] = 3;
			this.NearBlurSampleCount[1] = 5;
			this.NearBlurSampleCount[2] = 8;
			this.NearBlurMaxRadius[0] = 2f;
			this.NearBlurMaxRadius[1] = 4f;
			this.NearBlurMaxRadius[2] = 7f;
			this.FarBlurSampleCount[0] = 4;
			this.FarBlurSampleCount[1] = 7;
			this.FarBlurSampleCount[2] = 14;
			this.FarBlurMaxRadius[0] = 5f;
			this.FarBlurMaxRadius[1] = 8f;
			this.FarBlurMaxRadius[2] = 13f;
			this.DoFResolution[0] = DepthOfFieldResolution.Quarter;
			this.DoFResolution[1] = DepthOfFieldResolution.Half;
			this.DoFResolution[2] = DepthOfFieldResolution.Full;
			this.DoFHighQualityFiltering[0] = false;
			this.DoFHighQualityFiltering[1] = true;
			this.DoFHighQualityFiltering[2] = true;
			this.MotionBlurSampleCount[0] = 4;
			this.MotionBlurSampleCount[1] = 8;
			this.MotionBlurSampleCount[2] = 12;
			this.BloomRes[0] = BloomResolution.Quarter;
			this.BloomRes[1] = BloomResolution.Half;
			this.BloomRes[2] = BloomResolution.Half;
			this.BloomHighQualityFiltering[0] = false;
			this.BloomHighQualityFiltering[1] = true;
			this.BloomHighQualityFiltering[2] = true;
			this.ChromaticAberrationMaxSamples[0] = 3;
			this.ChromaticAberrationMaxSamples[1] = 6;
			this.ChromaticAberrationMaxSamples[2] = 12;
		}

		// Token: 0x06000840 RID: 2112 RVA: 0x00042020 File Offset: 0x00040220
		internal static GlobalPostProcessingQualitySettings NewDefault()
		{
			return new GlobalPostProcessingQualitySettings();
		}

		// Token: 0x040008ED RID: 2285
		private static int s_QualitySettingCount = 3;

		// Token: 0x040008EE RID: 2286
		public int[] NearBlurSampleCount = new int[GlobalPostProcessingQualitySettings.s_QualitySettingCount];

		// Token: 0x040008EF RID: 2287
		public float[] NearBlurMaxRadius = new float[GlobalPostProcessingQualitySettings.s_QualitySettingCount];

		// Token: 0x040008F0 RID: 2288
		public int[] FarBlurSampleCount = new int[GlobalPostProcessingQualitySettings.s_QualitySettingCount];

		// Token: 0x040008F1 RID: 2289
		public float[] FarBlurMaxRadius = new float[GlobalPostProcessingQualitySettings.s_QualitySettingCount];

		// Token: 0x040008F2 RID: 2290
		public DepthOfFieldResolution[] DoFResolution = new DepthOfFieldResolution[GlobalPostProcessingQualitySettings.s_QualitySettingCount];

		// Token: 0x040008F3 RID: 2291
		public bool[] DoFHighQualityFiltering = new bool[GlobalPostProcessingQualitySettings.s_QualitySettingCount];

		// Token: 0x040008F4 RID: 2292
		public int[] MotionBlurSampleCount = new int[GlobalPostProcessingQualitySettings.s_QualitySettingCount];

		// Token: 0x040008F5 RID: 2293
		public BloomResolution[] BloomRes = new BloomResolution[GlobalPostProcessingQualitySettings.s_QualitySettingCount];

		// Token: 0x040008F6 RID: 2294
		public bool[] BloomHighQualityFiltering = new bool[GlobalPostProcessingQualitySettings.s_QualitySettingCount];

		// Token: 0x040008F7 RID: 2295
		public int[] ChromaticAberrationMaxSamples = new int[GlobalPostProcessingQualitySettings.s_QualitySettingCount];
	}
}
