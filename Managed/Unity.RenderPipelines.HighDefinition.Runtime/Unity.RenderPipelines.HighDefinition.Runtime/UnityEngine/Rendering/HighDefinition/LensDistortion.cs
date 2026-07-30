using System;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x020000E1 RID: 225
	[VolumeComponentMenu("Post-processing/Lens Distortion")]
	[Serializable]
	public sealed class LensDistortion : VolumeComponent, IPostProcessComponent
	{
		// Token: 0x06000764 RID: 1892 RVA: 0x000388B4 File Offset: 0x00036AB4
		public bool IsActive()
		{
			return !Mathf.Approximately(this.intensity.value, 0f) && (this.xMultiplier.value > 0f || this.yMultiplier.value > 0f);
		}

		// Token: 0x040007BE RID: 1982
		[Tooltip("Controls the overall strength of the distortion effect.")]
		public ClampedFloatParameter intensity = new ClampedFloatParameter(0f, -1f, 1f, false);

		// Token: 0x040007BF RID: 1983
		[Tooltip("Controls the distortion intensity on the x-axis. Acts as a multiplier.")]
		public ClampedFloatParameter xMultiplier = new ClampedFloatParameter(1f, 0f, 1f, false);

		// Token: 0x040007C0 RID: 1984
		[Tooltip("Controls the distortion intensity on the x-axis. Acts as a multiplier.")]
		public ClampedFloatParameter yMultiplier = new ClampedFloatParameter(1f, 0f, 1f, false);

		// Token: 0x040007C1 RID: 1985
		[Tooltip("Sets the center point for the distortion.")]
		public Vector2Parameter center = new Vector2Parameter(new Vector2(0.5f, 0.5f), false);

		// Token: 0x040007C2 RID: 1986
		[Tooltip("Controls global screen scaling for the distortion effect. Use this to hide the screen borders when using a high \"Intensity\".")]
		public ClampedFloatParameter scale = new ClampedFloatParameter(1f, 0.01f, 5f, false);
	}
}
