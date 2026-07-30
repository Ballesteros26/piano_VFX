using System;

namespace UnityEngine.UI
{
	// Token: 0x0200001F RID: 31
	public interface ILayoutElement
	{
		// Token: 0x06000251 RID: 593
		void CalculateLayoutInputHorizontal();

		// Token: 0x06000252 RID: 594
		void CalculateLayoutInputVertical();

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x06000253 RID: 595
		float minWidth { get; }

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x06000254 RID: 596
		float preferredWidth { get; }

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x06000255 RID: 597
		float flexibleWidth { get; }

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x06000256 RID: 598
		float minHeight { get; }

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x06000257 RID: 599
		float preferredHeight { get; }

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x06000258 RID: 600
		float flexibleHeight { get; }

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x06000259 RID: 601
		int layoutPriority { get; }
	}
}
