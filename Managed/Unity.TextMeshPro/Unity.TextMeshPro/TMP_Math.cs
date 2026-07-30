using System;
using UnityEngine;

namespace TMPro
{
	// Token: 0x02000062 RID: 98
	public static class TMP_Math
	{
		// Token: 0x060004D0 RID: 1232 RVA: 0x000237DA File Offset: 0x000219DA
		public static bool Approximately(float a, float b)
		{
			return b - 0.0001f < a && a < b + 0.0001f;
		}

		// Token: 0x0400045E RID: 1118
		public const float FLOAT_MAX = 32767f;

		// Token: 0x0400045F RID: 1119
		public const float FLOAT_MIN = -32767f;

		// Token: 0x04000460 RID: 1120
		public const int INT_MAX = 2147483647;

		// Token: 0x04000461 RID: 1121
		public const int INT_MIN = -2147483647;

		// Token: 0x04000462 RID: 1122
		public const float FLOAT_UNSET = -32767f;

		// Token: 0x04000463 RID: 1123
		public const int INT_UNSET = -32767;

		// Token: 0x04000464 RID: 1124
		public static Vector2 MAX_16BIT = new Vector2(32767f, 32767f);

		// Token: 0x04000465 RID: 1125
		public static Vector2 MIN_16BIT = new Vector2(-32767f, -32767f);
	}
}
