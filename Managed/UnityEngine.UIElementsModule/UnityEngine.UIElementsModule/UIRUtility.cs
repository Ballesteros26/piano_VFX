using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020001A4 RID: 420
	internal static class UIRUtility
	{
		// Token: 0x06000BB9 RID: 3001 RVA: 0x0002C498 File Offset: 0x0002A698
		public static Vector4 ToVector4(Rect rc)
		{
			return new Vector4(rc.xMin, rc.yMin, rc.xMax, rc.yMax);
		}

		// Token: 0x06000BBA RID: 3002 RVA: 0x0002C4CC File Offset: 0x0002A6CC
		public static bool IsRoundRect(VisualElement ve)
		{
			IResolvedStyle resolvedStyle = ve.resolvedStyle;
			return resolvedStyle.borderTopLeftRadius >= Mathf.Epsilon || resolvedStyle.borderTopRightRadius >= Mathf.Epsilon || resolvedStyle.borderBottomLeftRadius >= Mathf.Epsilon || resolvedStyle.borderBottomRightRadius >= Mathf.Epsilon;
		}

		// Token: 0x06000BBB RID: 3003 RVA: 0x0002C520 File Offset: 0x0002A720
		public static bool IsVectorImageBackground(VisualElement ve)
		{
			return ve.computedStyle.backgroundImage.value.vectorImage != null;
		}

		// Token: 0x06000BBC RID: 3004 RVA: 0x0002C554 File Offset: 0x0002A754
		public static void Destroy(Object obj)
		{
			bool flag = obj == null;
			if (!flag)
			{
				bool isPlaying = Application.isPlaying;
				if (isPlaying)
				{
					Object.Destroy(obj);
				}
				else
				{
					Object.DestroyImmediate(obj);
				}
			}
		}

		// Token: 0x04000515 RID: 1301
		public static readonly string k_DefaultShaderName = "Hidden/Internal-UIRDefault";

		// Token: 0x04000516 RID: 1302
		public static readonly string k_DefaultWorldSpaceShaderName = "Hidden/Internal-UIRDefaultWorld";

		// Token: 0x04000517 RID: 1303
		public const float k_ClearZ = 0.99f;

		// Token: 0x04000518 RID: 1304
		public const float k_MeshPosZ = 0f;

		// Token: 0x04000519 RID: 1305
		public const float k_MaskPosZ = 1f;
	}
}
