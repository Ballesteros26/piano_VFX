using System;

namespace UnityEngine.TextCore
{
	// Token: 0x02000003 RID: 3
	internal static class ColorUtilities
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020A8 File Offset: 0x000002A8
		internal static bool CompareColors(Color32 a, Color32 b)
		{
			return a.r == b.r && a.g == b.g && a.b == b.b && a.a == b.a;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020F8 File Offset: 0x000002F8
		internal static bool CompareColorsRgb(Color32 a, Color32 b)
		{
			return a.r == b.r && a.g == b.g && a.b == b.b;
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002138 File Offset: 0x00000338
		internal static bool CompareColors(Color a, Color b)
		{
			return Mathf.Approximately(a.r, b.r) && Mathf.Approximately(a.g, b.g) && Mathf.Approximately(a.b, b.b) && Mathf.Approximately(a.a, b.a);
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002198 File Offset: 0x00000398
		internal static bool CompareColorsRgb(Color a, Color b)
		{
			return Mathf.Approximately(a.r, b.r) && Mathf.Approximately(a.g, b.g) && Mathf.Approximately(a.b, b.b);
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000021E4 File Offset: 0x000003E4
		internal static Color32 MultiplyColors(Color32 c1, Color32 c2)
		{
			byte b = (byte)((float)c1.r / 255f * ((float)c2.r / 255f) * 255f);
			byte b2 = (byte)((float)c1.g / 255f * ((float)c2.g / 255f) * 255f);
			byte b3 = (byte)((float)c1.b / 255f * ((float)c2.b / 255f) * 255f);
			byte b4 = (byte)((float)c1.a / 255f * ((float)c2.a / 255f) * 255f);
			return new Color32(b, b2, b3, b4);
		}
	}
}
