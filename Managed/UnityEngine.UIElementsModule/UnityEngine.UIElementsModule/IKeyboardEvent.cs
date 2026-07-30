using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000156 RID: 342
	public interface IKeyboardEvent
	{
		// Token: 0x17000233 RID: 563
		// (get) Token: 0x06000989 RID: 2441
		EventModifiers modifiers { get; }

		// Token: 0x17000234 RID: 564
		// (get) Token: 0x0600098A RID: 2442
		char character { get; }

		// Token: 0x17000235 RID: 565
		// (get) Token: 0x0600098B RID: 2443
		KeyCode keyCode { get; }

		// Token: 0x17000236 RID: 566
		// (get) Token: 0x0600098C RID: 2444
		bool shiftKey { get; }

		// Token: 0x17000237 RID: 567
		// (get) Token: 0x0600098D RID: 2445
		bool ctrlKey { get; }

		// Token: 0x17000238 RID: 568
		// (get) Token: 0x0600098E RID: 2446
		bool commandKey { get; }

		// Token: 0x17000239 RID: 569
		// (get) Token: 0x0600098F RID: 2447
		bool altKey { get; }

		// Token: 0x1700023A RID: 570
		// (get) Token: 0x06000990 RID: 2448
		bool actionKey { get; }
	}
}
