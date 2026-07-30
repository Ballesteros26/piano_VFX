using System;

namespace UnityEngine.Yoga
{
	// Token: 0x0200001F RID: 31
	internal static class YogaValueExtensions
	{
		// Token: 0x0600014F RID: 335 RVA: 0x00003B3C File Offset: 0x00001D3C
		public static YogaValue Percent(this float value)
		{
			return YogaValue.Percent(value);
		}

		// Token: 0x06000150 RID: 336 RVA: 0x00003B54 File Offset: 0x00001D54
		public static YogaValue Pt(this float value)
		{
			return YogaValue.Point(value);
		}

		// Token: 0x06000151 RID: 337 RVA: 0x00003B6C File Offset: 0x00001D6C
		public static YogaValue Percent(this int value)
		{
			return YogaValue.Percent((float)value);
		}

		// Token: 0x06000152 RID: 338 RVA: 0x00003B88 File Offset: 0x00001D88
		public static YogaValue Pt(this int value)
		{
			return YogaValue.Point((float)value);
		}
	}
}
