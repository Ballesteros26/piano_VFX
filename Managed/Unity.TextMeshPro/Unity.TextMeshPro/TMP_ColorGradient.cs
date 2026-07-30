using System;
using UnityEngine;

namespace TMPro
{
	// Token: 0x02000010 RID: 16
	[Serializable]
	public class TMP_ColorGradient : ScriptableObject
	{
		// Token: 0x0600004E RID: 78 RVA: 0x000029B4 File Offset: 0x00000BB4
		public TMP_ColorGradient()
		{
			this.colorMode = ColorMode.FourCornersGradient;
			this.topLeft = TMP_ColorGradient.k_DefaultColor;
			this.topRight = TMP_ColorGradient.k_DefaultColor;
			this.bottomLeft = TMP_ColorGradient.k_DefaultColor;
			this.bottomRight = TMP_ColorGradient.k_DefaultColor;
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00002A01 File Offset: 0x00000C01
		public TMP_ColorGradient(Color color)
		{
			this.colorMode = ColorMode.FourCornersGradient;
			this.topLeft = color;
			this.topRight = color;
			this.bottomLeft = color;
			this.bottomRight = color;
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00002A33 File Offset: 0x00000C33
		public TMP_ColorGradient(Color color0, Color color1, Color color2, Color color3)
		{
			this.colorMode = ColorMode.FourCornersGradient;
			this.topLeft = color0;
			this.topRight = color1;
			this.bottomLeft = color2;
			this.bottomRight = color3;
		}

		// Token: 0x04000056 RID: 86
		public ColorMode colorMode = ColorMode.FourCornersGradient;

		// Token: 0x04000057 RID: 87
		public Color topLeft;

		// Token: 0x04000058 RID: 88
		public Color topRight;

		// Token: 0x04000059 RID: 89
		public Color bottomLeft;

		// Token: 0x0400005A RID: 90
		public Color bottomRight;

		// Token: 0x0400005B RID: 91
		private const ColorMode k_DefaultColorMode = ColorMode.FourCornersGradient;

		// Token: 0x0400005C RID: 92
		private static readonly Color k_DefaultColor = Color.white;
	}
}
