using System;

namespace System.Drawing
{
	// Token: 0x0200000F RID: 15
	internal static class ColorUtil
	{
		// Token: 0x06000025 RID: 37 RVA: 0x000024F2 File Offset: 0x000006F2
		public static Color FromKnownColor(KnownColor color)
		{
			return Color.FromKnownColor(color);
		}

		// Token: 0x06000026 RID: 38 RVA: 0x000024FA File Offset: 0x000006FA
		public static bool IsSystemColor(this Color color)
		{
			return color.IsSystemColor;
		}
	}
}
