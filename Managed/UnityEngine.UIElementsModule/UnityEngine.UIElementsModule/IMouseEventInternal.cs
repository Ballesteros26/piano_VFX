using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200015F RID: 351
	internal interface IMouseEventInternal
	{
		// Token: 0x17000252 RID: 594
		// (get) Token: 0x060009C5 RID: 2501
		// (set) Token: 0x060009C6 RID: 2502
		bool triggeredByOS { get; set; }

		// Token: 0x17000253 RID: 595
		// (get) Token: 0x060009C7 RID: 2503
		// (set) Token: 0x060009C8 RID: 2504
		bool recomputeTopElementUnderMouse { get; set; }

		// Token: 0x17000254 RID: 596
		// (get) Token: 0x060009C9 RID: 2505
		// (set) Token: 0x060009CA RID: 2506
		IPointerEvent sourcePointerEvent { get; set; }
	}
}
