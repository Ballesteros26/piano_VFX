using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	// Token: 0x02000177 RID: 375
	public static class PointerId
	{
		// Token: 0x1700026A RID: 618
		// (get) Token: 0x06000A44 RID: 2628 RVA: 0x000273BC File Offset: 0x000255BC
		internal static IEnumerable<int> hoveringPointers
		{
			get
			{
				yield return PointerId.mousePointerId;
				yield break;
			}
		}

		// Token: 0x0400044C RID: 1100
		public static readonly int maxPointers = 32;

		// Token: 0x0400044D RID: 1101
		public static readonly int invalidPointerId = -1;

		// Token: 0x0400044E RID: 1102
		public static readonly int mousePointerId = 0;

		// Token: 0x0400044F RID: 1103
		public static readonly int touchPointerIdBase = 1;

		// Token: 0x04000450 RID: 1104
		public static readonly int touchPointerCount = 20;

		// Token: 0x04000451 RID: 1105
		public static readonly int penPointerIdBase = PointerId.touchPointerIdBase + PointerId.touchPointerCount;

		// Token: 0x04000452 RID: 1106
		public static readonly int penPointerCount = 2;
	}
}
