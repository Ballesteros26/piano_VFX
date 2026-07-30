using System;

namespace UnityEngine.UIElements.UIR
{
	// Token: 0x0200021C RID: 540
	[Flags]
	internal enum RenderDataDirtyTypes
	{
		// Token: 0x0400070E RID: 1806
		None = 0,
		// Token: 0x0400070F RID: 1807
		Transform = 1,
		// Token: 0x04000710 RID: 1808
		ClipRectSize = 2,
		// Token: 0x04000711 RID: 1809
		Clipping = 4,
		// Token: 0x04000712 RID: 1810
		ClippingHierarchy = 8,
		// Token: 0x04000713 RID: 1811
		Visuals = 16,
		// Token: 0x04000714 RID: 1812
		VisualsHierarchy = 32,
		// Token: 0x04000715 RID: 1813
		Opacity = 64
	}
}
