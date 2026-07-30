using System;

namespace UnityEngine.Rendering
{
	// Token: 0x02000374 RID: 884
	[Flags]
	public enum RenderStateMask
	{
		// Token: 0x04000AE2 RID: 2786
		Nothing = 0,
		// Token: 0x04000AE3 RID: 2787
		Blend = 1,
		// Token: 0x04000AE4 RID: 2788
		Raster = 2,
		// Token: 0x04000AE5 RID: 2789
		Depth = 4,
		// Token: 0x04000AE6 RID: 2790
		Stencil = 8,
		// Token: 0x04000AE7 RID: 2791
		Everything = 15
	}
}
