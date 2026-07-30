using System;
using UnityEngine.Serialization;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x020000E3 RID: 227
	[VolumeComponentMenu("Post-processing/Motion Blur")]
	[Serializable]
	public sealed class MotionBlur : VolumeComponentWithQuality, IPostProcessComponent
	{
		// Token: 0x17000114 RID: 276
		// (get) Token: 0x06000768 RID: 1896 RVA: 0x00038A80 File Offset: 0x00036C80
		// (set) Token: 0x06000769 RID: 1897 RVA: 0x00038ABE File Offset: 0x00036CBE
		public int sampleCount
		{
			get
			{
				if (!base.UsesQualitySettings())
				{
					return this.m_SampleCount.value;
				}
				int item = this.quality.levelAndOverride.Item1;
				return VolumeComponentWithQuality.GetPostProcessingQualitySettings().MotionBlurSampleCount[item];
			}
			set
			{
				this.m_SampleCount.value = value;
			}
		}

		// Token: 0x0600076A RID: 1898 RVA: 0x00038ACC File Offset: 0x00036CCC
		public bool IsActive()
		{
			return this.intensity.value > 0f;
		}

		// Token: 0x040007C6 RID: 1990
		[Tooltip("Sets the intensity of the motion blur effect. Acts as a multiplier for velocities.")]
		public MinFloatParameter intensity = new MinFloatParameter(0f, 0f, false);

		// Token: 0x040007C7 RID: 1991
		[Tooltip("Controls the maximum velocity, in pixels, that HDRP allows for all sources of motion blur except Camera rotation.")]
		public ClampedFloatParameter maximumVelocity = new ClampedFloatParameter(200f, 0f, 1500f, false);

		// Token: 0x040007C8 RID: 1992
		[Tooltip("Controls the minimum velocity, in pixels, that a GameObject must have to contribute to the motion blur effect.")]
		public ClampedFloatParameter minimumVelocity = new ClampedFloatParameter(2f, 0f, 64f, false);

		// Token: 0x040007C9 RID: 1993
		[Tooltip("Sets the maximum length, as a fraction of the screen's full resolution, that the velocity resulting from Camera rotation can have.")]
		public ClampedFloatParameter cameraRotationVelocityClamp = new ClampedFloatParameter(0.03f, 0f, 0.2f, false);

		// Token: 0x040007CA RID: 1994
		[Tooltip("Value used for the depth based weighting of samples. Tweak if unwanted leak of background onto foreground or viceversa is detected.")]
		public ClampedFloatParameter depthComparisonExtent = new ClampedFloatParameter(1f, 0f, 20f, false);

		// Token: 0x040007CB RID: 1995
		[Tooltip("If toggled off, the motion caused by the camera is not considered when doing motion blur.")]
		public BoolParameter cameraMotionBlur = new BoolParameter(true, false);

		// Token: 0x040007CC RID: 1996
		[Tooltip("Sets the maximum number of sample points that HDRP uses to compute motion blur.")]
		[SerializeField]
		[FormerlySerializedAs("sampleCount")]
		private MinIntParameter m_SampleCount = new MinIntParameter(8, 2, false);
	}
}
