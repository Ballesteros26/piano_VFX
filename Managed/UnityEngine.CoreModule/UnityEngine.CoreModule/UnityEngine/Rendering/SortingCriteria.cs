using System;

namespace UnityEngine.Rendering
{
	// Token: 0x0200037D RID: 893
	[Flags]
	public enum SortingCriteria
	{
		// Token: 0x04000B01 RID: 2817
		None = 0,
		// Token: 0x04000B02 RID: 2818
		SortingLayer = 1,
		// Token: 0x04000B03 RID: 2819
		RenderQueue = 2,
		// Token: 0x04000B04 RID: 2820
		BackToFront = 4,
		// Token: 0x04000B05 RID: 2821
		QuantizedFrontToBack = 8,
		// Token: 0x04000B06 RID: 2822
		OptimizeStateChanges = 16,
		// Token: 0x04000B07 RID: 2823
		CanvasOrder = 32,
		// Token: 0x04000B08 RID: 2824
		RendererPriority = 64,
		// Token: 0x04000B09 RID: 2825
		CommonOpaque = 59,
		// Token: 0x04000B0A RID: 2826
		CommonTransparent = 23
	}
}
