using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000168 RID: 360
	[NativeHeader("Runtime/Export/Math/ColorUtility.bindings.h")]
	public class ColorUtility
	{
		// Token: 0x0600106A RID: 4202
		[FreeFunction]
		[MethodImpl(4096)]
		internal static extern bool DoTryParseHtmlColor(string htmlString, out Color32 color);

		// Token: 0x0600106B RID: 4203 RVA: 0x00017C10 File Offset: 0x00015E10
		public static bool TryParseHtmlString(string htmlString, out Color color)
		{
			Color32 color2;
			bool flag = ColorUtility.DoTryParseHtmlColor(htmlString, out color2);
			color = color2;
			return flag;
		}

		// Token: 0x0600106C RID: 4204 RVA: 0x00017C38 File Offset: 0x00015E38
		public static string ToHtmlStringRGB(Color color)
		{
			Color32 color2 = new Color32((byte)Mathf.Clamp(Mathf.RoundToInt(color.r * 255f), 0, 255), (byte)Mathf.Clamp(Mathf.RoundToInt(color.g * 255f), 0, 255), (byte)Mathf.Clamp(Mathf.RoundToInt(color.b * 255f), 0, 255), 1);
			return UnityString.Format("{0:X2}{1:X2}{2:X2}", new object[] { color2.r, color2.g, color2.b });
		}

		// Token: 0x0600106D RID: 4205 RVA: 0x00017CE4 File Offset: 0x00015EE4
		public static string ToHtmlStringRGBA(Color color)
		{
			Color32 color2 = new Color32((byte)Mathf.Clamp(Mathf.RoundToInt(color.r * 255f), 0, 255), (byte)Mathf.Clamp(Mathf.RoundToInt(color.g * 255f), 0, 255), (byte)Mathf.Clamp(Mathf.RoundToInt(color.b * 255f), 0, 255), (byte)Mathf.Clamp(Mathf.RoundToInt(color.a * 255f), 0, 255));
			return UnityString.Format("{0:X2}{1:X2}{2:X2}{3:X2}", new object[] { color2.r, color2.g, color2.b, color2.a });
		}
	}
}
