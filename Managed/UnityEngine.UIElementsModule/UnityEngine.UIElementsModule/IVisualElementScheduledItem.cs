using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020000A1 RID: 161
	public interface IVisualElementScheduledItem
	{
		// Token: 0x17000121 RID: 289
		// (get) Token: 0x060004D1 RID: 1233
		VisualElement element { get; }

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x060004D2 RID: 1234
		bool isActive { get; }

		// Token: 0x060004D3 RID: 1235
		void Resume();

		// Token: 0x060004D4 RID: 1236
		void Pause();

		// Token: 0x060004D5 RID: 1237
		void ExecuteLater(long delayMs);

		// Token: 0x060004D6 RID: 1238
		IVisualElementScheduledItem StartingIn(long delayMs);

		// Token: 0x060004D7 RID: 1239
		IVisualElementScheduledItem Every(long intervalMs);

		// Token: 0x060004D8 RID: 1240
		IVisualElementScheduledItem Until(Func<bool> stopCondition);

		// Token: 0x060004D9 RID: 1241
		IVisualElementScheduledItem ForDuration(long durationMs);
	}
}
