using System;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x020000E4 RID: 228
	[VolumeComponentMenu("Post-processing/Panini Projection")]
	[Serializable]
	public sealed class PaniniProjection : VolumeComponent, IPostProcessComponent
	{
		// Token: 0x0600076C RID: 1900 RVA: 0x00038B90 File Offset: 0x00036D90
		public bool IsActive()
		{
			return this.distance.value > 0f;
		}

		// Token: 0x040007CD RID: 1997
		[Tooltip("Controls the panini projection distance. This controls the strength of the distorion.")]
		public ClampedFloatParameter distance = new ClampedFloatParameter(0f, 0f, 1f, false);

		// Token: 0x040007CE RID: 1998
		[Tooltip("Controls how much cropping HDRP applies to the screen with the panini projection effect. A value of 1 crops the distortion to the edge of the screen.")]
		public ClampedFloatParameter cropToFit = new ClampedFloatParameter(1f, 0f, 1f, false);
	}
}
