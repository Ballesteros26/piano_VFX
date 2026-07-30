using System;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x020000D5 RID: 213
	[VolumeComponentMenu("Exposure")]
	[Serializable]
	public sealed class Exposure : VolumeComponent, IPostProcessComponent
	{
		// Token: 0x0600075B RID: 1883 RVA: 0x00003AC0 File Offset: 0x00001CC0
		public bool IsActive()
		{
			return true;
		}

		// Token: 0x04000794 RID: 1940
		[Tooltip("Specifies the method that HDRP uses to process exposure.")]
		public ExposureModeParameter mode = new ExposureModeParameter(ExposureMode.Fixed, false);

		// Token: 0x04000795 RID: 1941
		[Tooltip("Specifies the metering method that HDRP uses the filter the luminance source.")]
		public MeteringModeParameter meteringMode = new MeteringModeParameter(MeteringMode.CenterWeighted, false);

		// Token: 0x04000796 RID: 1942
		[Tooltip("Specifies the luminance source that HDRP uses to calculate the current Scene exposure.")]
		public LuminanceSourceParameter luminanceSource = new LuminanceSourceParameter(LuminanceSource.ColorBuffer, false);

		// Token: 0x04000797 RID: 1943
		[Tooltip("Sets a static exposure value for Cameras in this Volume.")]
		public FloatParameter fixedExposure = new FloatParameter(0f, false);

		// Token: 0x04000798 RID: 1944
		[Tooltip("Sets the compensation that the Camera applies to the calculated exposure value.")]
		public FloatParameter compensation = new FloatParameter(0f, false);

		// Token: 0x04000799 RID: 1945
		[Tooltip("Sets the minimum value that the Scene exposure can be set to.")]
		public FloatParameter limitMin = new FloatParameter(-10f, false);

		// Token: 0x0400079A RID: 1946
		[Tooltip("Sets the maximum value that the Scene exposure can be set to.")]
		public FloatParameter limitMax = new FloatParameter(20f, false);

		// Token: 0x0400079B RID: 1947
		[Tooltip("Specifies a curve that remaps the Scene exposure on the x-axis to the exposure you want on the y-axis.")]
		public AnimationCurveParameter curveMap = new AnimationCurveParameter(AnimationCurve.Linear(-10f, -10f, 20f, 20f), false);

		// Token: 0x0400079C RID: 1948
		[Tooltip("Specifies the method that HDRP uses to change the exposure when the Camera moves from dark to light and vice versa.")]
		public AdaptationModeParameter adaptationMode = new AdaptationModeParameter(AdaptationMode.Progressive, false);

		// Token: 0x0400079D RID: 1949
		[Tooltip("Sets the speed at which the exposure changes when the Camera moves from a dark area to a bright area.")]
		public MinFloatParameter adaptationSpeedDarkToLight = new MinFloatParameter(3f, 0.001f, false);

		// Token: 0x0400079E RID: 1950
		[Tooltip("Sets the speed at which the exposure changes when the Camera moves from a bright area to a dark area.")]
		public MinFloatParameter adaptationSpeedLightToDark = new MinFloatParameter(1f, 0.001f, false);
	}
}
