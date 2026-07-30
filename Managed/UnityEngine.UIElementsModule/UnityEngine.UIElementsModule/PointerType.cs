using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000176 RID: 374
	public static class PointerType
	{
		// Token: 0x06000A41 RID: 2625 RVA: 0x00027340 File Offset: 0x00025540
		internal static string GetPointerType(int pointerId)
		{
			bool flag = pointerId == PointerId.mousePointerId;
			string text;
			if (flag)
			{
				text = PointerType.mouse;
			}
			else
			{
				text = PointerType.touch;
			}
			return text;
		}

		// Token: 0x06000A42 RID: 2626 RVA: 0x0002736C File Offset: 0x0002556C
		internal static bool IsDirectManipulationDevice(string pointerType)
		{
			return pointerType == PointerType.touch || pointerType == PointerType.pen;
		}

		// Token: 0x04000448 RID: 1096
		public static readonly string mouse = "mouse";

		// Token: 0x04000449 RID: 1097
		public static readonly string touch = "touch";

		// Token: 0x0400044A RID: 1098
		public static readonly string pen = "pen";

		// Token: 0x0400044B RID: 1099
		public static readonly string unknown = "";
	}
}
