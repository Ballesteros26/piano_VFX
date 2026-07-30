using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000179 RID: 377
	public interface IPointerEvent
	{
		// Token: 0x1700026D RID: 621
		// (get) Token: 0x06000A4E RID: 2638
		int pointerId { get; }

		// Token: 0x1700026E RID: 622
		// (get) Token: 0x06000A4F RID: 2639
		string pointerType { get; }

		// Token: 0x1700026F RID: 623
		// (get) Token: 0x06000A50 RID: 2640
		bool isPrimary { get; }

		// Token: 0x17000270 RID: 624
		// (get) Token: 0x06000A51 RID: 2641
		int button { get; }

		// Token: 0x17000271 RID: 625
		// (get) Token: 0x06000A52 RID: 2642
		int pressedButtons { get; }

		// Token: 0x17000272 RID: 626
		// (get) Token: 0x06000A53 RID: 2643
		Vector3 position { get; }

		// Token: 0x17000273 RID: 627
		// (get) Token: 0x06000A54 RID: 2644
		Vector3 localPosition { get; }

		// Token: 0x17000274 RID: 628
		// (get) Token: 0x06000A55 RID: 2645
		Vector3 deltaPosition { get; }

		// Token: 0x17000275 RID: 629
		// (get) Token: 0x06000A56 RID: 2646
		float deltaTime { get; }

		// Token: 0x17000276 RID: 630
		// (get) Token: 0x06000A57 RID: 2647
		int clickCount { get; }

		// Token: 0x17000277 RID: 631
		// (get) Token: 0x06000A58 RID: 2648
		float pressure { get; }

		// Token: 0x17000278 RID: 632
		// (get) Token: 0x06000A59 RID: 2649
		float tangentialPressure { get; }

		// Token: 0x17000279 RID: 633
		// (get) Token: 0x06000A5A RID: 2650
		float altitudeAngle { get; }

		// Token: 0x1700027A RID: 634
		// (get) Token: 0x06000A5B RID: 2651
		float azimuthAngle { get; }

		// Token: 0x1700027B RID: 635
		// (get) Token: 0x06000A5C RID: 2652
		float twist { get; }

		// Token: 0x1700027C RID: 636
		// (get) Token: 0x06000A5D RID: 2653
		Vector2 radius { get; }

		// Token: 0x1700027D RID: 637
		// (get) Token: 0x06000A5E RID: 2654
		Vector2 radiusVariance { get; }

		// Token: 0x1700027E RID: 638
		// (get) Token: 0x06000A5F RID: 2655
		EventModifiers modifiers { get; }

		// Token: 0x1700027F RID: 639
		// (get) Token: 0x06000A60 RID: 2656
		bool shiftKey { get; }

		// Token: 0x17000280 RID: 640
		// (get) Token: 0x06000A61 RID: 2657
		bool ctrlKey { get; }

		// Token: 0x17000281 RID: 641
		// (get) Token: 0x06000A62 RID: 2658
		bool commandKey { get; }

		// Token: 0x17000282 RID: 642
		// (get) Token: 0x06000A63 RID: 2659
		bool altKey { get; }

		// Token: 0x17000283 RID: 643
		// (get) Token: 0x06000A64 RID: 2660
		bool actionKey { get; }
	}
}
