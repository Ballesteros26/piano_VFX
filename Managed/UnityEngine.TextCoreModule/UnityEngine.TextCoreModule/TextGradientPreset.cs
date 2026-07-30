using System;

namespace UnityEngine.TextCore
{
	// Token: 0x02000036 RID: 54
	[Serializable]
	internal class TextGradientPreset : ScriptableObject
	{
		// Token: 0x06000151 RID: 337 RVA: 0x0001897F File Offset: 0x00016B7F
		public TextGradientPreset()
		{
			this.colorMode = ColorMode.FourCornersGradient;
			this.topLeft = Color.white;
			this.topRight = Color.white;
			this.bottomLeft = Color.white;
			this.bottomRight = Color.white;
		}

		// Token: 0x06000152 RID: 338 RVA: 0x000189BC File Offset: 0x00016BBC
		public TextGradientPreset(Color color)
		{
			this.colorMode = ColorMode.FourCornersGradient;
			this.topLeft = color;
			this.topRight = color;
			this.bottomLeft = color;
			this.bottomRight = color;
		}

		// Token: 0x06000153 RID: 339 RVA: 0x000189E9 File Offset: 0x00016BE9
		public TextGradientPreset(Color color0, Color color1, Color color2, Color color3)
		{
			this.colorMode = ColorMode.FourCornersGradient;
			this.topLeft = color0;
			this.topRight = color1;
			this.bottomLeft = color2;
			this.bottomRight = color3;
		}

		// Token: 0x0400030B RID: 779
		public ColorMode colorMode;

		// Token: 0x0400030C RID: 780
		public Color topLeft;

		// Token: 0x0400030D RID: 781
		public Color topRight;

		// Token: 0x0400030E RID: 782
		public Color bottomLeft;

		// Token: 0x0400030F RID: 783
		public Color bottomRight;
	}
}
