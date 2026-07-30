using System;

namespace UnityEngine.UI
{
	// Token: 0x0200000B RID: 11
	public interface IClippable
	{
		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000046 RID: 70
		GameObject gameObject { get; }

		// Token: 0x06000047 RID: 71
		void RecalculateClipping();

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000048 RID: 72
		RectTransform rectTransform { get; }

		// Token: 0x06000049 RID: 73
		void Cull(Rect clipRect, bool validRect);

		// Token: 0x0600004A RID: 74
		void SetClipRect(Rect value, bool validRect);

		// Token: 0x0600004B RID: 75
		void SetClipSoftness(Vector2 clipSoftness);
	}
}
