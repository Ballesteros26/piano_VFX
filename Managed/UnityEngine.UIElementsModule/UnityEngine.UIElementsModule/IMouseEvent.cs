using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200015E RID: 350
	public interface IMouseEvent
	{
		// Token: 0x17000246 RID: 582
		// (get) Token: 0x060009B9 RID: 2489
		EventModifiers modifiers { get; }

		// Token: 0x17000247 RID: 583
		// (get) Token: 0x060009BA RID: 2490
		Vector2 mousePosition { get; }

		// Token: 0x17000248 RID: 584
		// (get) Token: 0x060009BB RID: 2491
		Vector2 localMousePosition { get; }

		// Token: 0x17000249 RID: 585
		// (get) Token: 0x060009BC RID: 2492
		Vector2 mouseDelta { get; }

		// Token: 0x1700024A RID: 586
		// (get) Token: 0x060009BD RID: 2493
		int clickCount { get; }

		// Token: 0x1700024B RID: 587
		// (get) Token: 0x060009BE RID: 2494
		int button { get; }

		// Token: 0x1700024C RID: 588
		// (get) Token: 0x060009BF RID: 2495
		int pressedButtons { get; }

		// Token: 0x1700024D RID: 589
		// (get) Token: 0x060009C0 RID: 2496
		bool shiftKey { get; }

		// Token: 0x1700024E RID: 590
		// (get) Token: 0x060009C1 RID: 2497
		bool ctrlKey { get; }

		// Token: 0x1700024F RID: 591
		// (get) Token: 0x060009C2 RID: 2498
		bool commandKey { get; }

		// Token: 0x17000250 RID: 592
		// (get) Token: 0x060009C3 RID: 2499
		bool altKey { get; }

		// Token: 0x17000251 RID: 593
		// (get) Token: 0x060009C4 RID: 2500
		bool actionKey { get; }
	}
}
