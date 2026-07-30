using System;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x020000E6 RID: 230
	[VolumeComponentMenu("Post-processing/Split Toning")]
	[Serializable]
	public sealed class SplitToning : VolumeComponent, IPostProcessComponent
	{
		// Token: 0x06000770 RID: 1904 RVA: 0x00038D1D File Offset: 0x00036F1D
		public bool IsActive()
		{
			return this.shadows != Color.grey || this.highlights != Color.grey;
		}

		// Token: 0x040007D6 RID: 2006
		[Tooltip("Specifies the color to use for shadows.")]
		public ColorParameter shadows = new ColorParameter(Color.grey, false, false, true, false);

		// Token: 0x040007D7 RID: 2007
		[Tooltip("Specifies the color to use for highlights.")]
		public ColorParameter highlights = new ColorParameter(Color.grey, false, false, true, false);

		// Token: 0x040007D8 RID: 2008
		[Tooltip("Controls the balance between the colors in the highlights and shadows.")]
		public ClampedFloatParameter balance = new ClampedFloatParameter(0f, -100f, 100f, false);
	}
}
