using System;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x02000157 RID: 343
	[VolumeComponentMenu("Sky/Gradient Sky")]
	[SkyUniqueID(3)]
	public class GradientSky : SkySettings
	{
		// Token: 0x06000A19 RID: 2585 RVA: 0x0004EE1C File Offset: 0x0004D01C
		public override int GetHashCode()
		{
			return (((base.GetHashCode() * 23 + this.bottom.GetHashCode()) * 23 + this.top.GetHashCode()) * 23 + this.middle.GetHashCode()) * 23 + this.gradientDiffusion.GetHashCode();
		}

		// Token: 0x06000A1A RID: 2586 RVA: 0x0004EE6B File Offset: 0x0004D06B
		public override Type GetSkyRendererType()
		{
			return typeof(GradientSkyRenderer);
		}

		// Token: 0x04000F5B RID: 3931
		[Tooltip("Specifies the color of the upper hemisphere of the sky.")]
		public ColorParameter top = new ColorParameter(Color.blue, true, false, true, false);

		// Token: 0x04000F5C RID: 3932
		[Tooltip("Specifies the color at the horizon.")]
		public ColorParameter middle = new ColorParameter(new Color(0.3f, 0.7f, 1f), true, false, true, false);

		// Token: 0x04000F5D RID: 3933
		[Tooltip("Specifies the color of the lower hemisphere of the sky. This is below the horizon.")]
		public ColorParameter bottom = new ColorParameter(Color.white, true, false, true, false);

		// Token: 0x04000F5E RID: 3934
		[Tooltip("Sets the size of the horizon (Middle color).")]
		public FloatParameter gradientDiffusion = new FloatParameter(1f, false);
	}
}
