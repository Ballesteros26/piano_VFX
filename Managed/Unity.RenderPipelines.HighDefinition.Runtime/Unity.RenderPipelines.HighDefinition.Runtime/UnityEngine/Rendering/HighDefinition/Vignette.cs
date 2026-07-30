using System;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x020000EB RID: 235
	[VolumeComponentMenu("Post-processing/Vignette")]
	[Serializable]
	public sealed class Vignette : VolumeComponent, IPostProcessComponent
	{
		// Token: 0x06000776 RID: 1910 RVA: 0x00038FA8 File Offset: 0x000371A8
		public bool IsActive()
		{
			return (this.mode.value == VignetteMode.Procedural && this.intensity.value > 0f) || (this.mode.value == VignetteMode.Masked && this.opacity.value > 0f && this.mask.value != null);
		}

		// Token: 0x040007EB RID: 2027
		[Tooltip("Specifies the mode HDRP uses to display the vignette effect.")]
		public VignetteModeParameter mode = new VignetteModeParameter(VignetteMode.Procedural, false);

		// Token: 0x040007EC RID: 2028
		[Tooltip("Specifies the color of the vignette.")]
		public ColorParameter color = new ColorParameter(Color.black, false, false, true, false);

		// Token: 0x040007ED RID: 2029
		[Tooltip("Sets the center point for the vignette.")]
		public Vector2Parameter center = new Vector2Parameter(new Vector2(0.5f, 0.5f), false);

		// Token: 0x040007EE RID: 2030
		[Tooltip("Controls the strength of the vignette effect.")]
		public ClampedFloatParameter intensity = new ClampedFloatParameter(0f, 0f, 1f, false);

		// Token: 0x040007EF RID: 2031
		[Tooltip("Controls the smoothness of the vignette borders.")]
		public ClampedFloatParameter smoothness = new ClampedFloatParameter(0.2f, 0.01f, 1f, false);

		// Token: 0x040007F0 RID: 2032
		[Tooltip("Controls how round the vignette is, lower values result in a more square vignette.")]
		public ClampedFloatParameter roundness = new ClampedFloatParameter(1f, 0f, 1f, false);

		// Token: 0x040007F1 RID: 2033
		[Tooltip("When enabled, the vignette is perfectly round. When disabled, the vignette matches shape with the current aspect ratio.")]
		public BoolParameter rounded = new BoolParameter(false, false);

		// Token: 0x040007F2 RID: 2034
		[Tooltip("Specifies a black and white mask Texture to use as a vignette.")]
		public TextureParameter mask = new TextureParameter(null, false);

		// Token: 0x040007F3 RID: 2035
		[Range(0f, 1f)]
		[Tooltip("Controls the opacity of the mask vignette. Lower values result in a more transparent vignette.")]
		public ClampedFloatParameter opacity = new ClampedFloatParameter(1f, 0f, 1f, false);
	}
}
