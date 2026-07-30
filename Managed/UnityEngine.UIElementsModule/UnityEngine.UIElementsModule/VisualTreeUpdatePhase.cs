using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020000B1 RID: 177
	internal enum VisualTreeUpdatePhase
	{
		// Token: 0x04000234 RID: 564
		ViewData,
		// Token: 0x04000235 RID: 565
		Bindings,
		// Token: 0x04000236 RID: 566
		Animation,
		// Token: 0x04000237 RID: 567
		Styles,
		// Token: 0x04000238 RID: 568
		Layout,
		// Token: 0x04000239 RID: 569
		TransformClip,
		// Token: 0x0400023A RID: 570
		Repaint,
		// Token: 0x0400023B RID: 571
		Count
	}
}
