using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200017A RID: 378
	internal interface IPointerEventInternal
	{
		// Token: 0x17000284 RID: 644
		// (get) Token: 0x06000A65 RID: 2661
		// (set) Token: 0x06000A66 RID: 2662
		bool triggeredByOS { get; set; }

		// Token: 0x17000285 RID: 645
		// (get) Token: 0x06000A67 RID: 2663
		// (set) Token: 0x06000A68 RID: 2664
		bool recomputeTopElementUnderPointer { get; set; }
	}
}
