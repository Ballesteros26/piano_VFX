using System;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x020000CE RID: 206
	[VolumeComponentMenu("Post-processing/Color Adjustments")]
	[Serializable]
	public sealed class ColorAdjustments : VolumeComponent, IPostProcessComponent
	{
		// Token: 0x06000745 RID: 1861 RVA: 0x00038058 File Offset: 0x00036258
		public bool IsActive()
		{
			return this.postExposure.value != 0f || this.contrast.value != 0f || this.colorFilter != Color.white || this.hueShift != 0f || this.saturation != 0f;
		}

		// Token: 0x04000772 RID: 1906
		[Tooltip("Sets the value that HDRP uses to adjust the overall exposure of the Scene, in EV.")]
		public FloatParameter postExposure = new FloatParameter(0f, false);

		// Token: 0x04000773 RID: 1907
		[Tooltip("Controls the overall range of the tonal values.")]
		public ClampedFloatParameter contrast = new ClampedFloatParameter(0f, -100f, 100f, false);

		// Token: 0x04000774 RID: 1908
		[Tooltip("Specifies the color that HDRP tints the render to.")]
		public ColorParameter colorFilter = new ColorParameter(Color.white, true, false, true, false);

		// Token: 0x04000775 RID: 1909
		[Tooltip("Controls the hue of all colors in the render.")]
		public ClampedFloatParameter hueShift = new ClampedFloatParameter(0f, -180f, 180f, false);

		// Token: 0x04000776 RID: 1910
		[Tooltip("Controls the intensity of all colors in the render.")]
		public ClampedFloatParameter saturation = new ClampedFloatParameter(0f, -100f, 100f, false);
	}
}
