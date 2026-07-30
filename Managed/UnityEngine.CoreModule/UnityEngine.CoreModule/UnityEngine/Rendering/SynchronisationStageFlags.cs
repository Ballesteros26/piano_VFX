using System;

namespace UnityEngine.Rendering
{
	// Token: 0x0200034A RID: 842
	public enum SynchronisationStageFlags
	{
		// Token: 0x040009F1 RID: 2545
		VertexProcessing = 1,
		// Token: 0x040009F2 RID: 2546
		PixelProcessing,
		// Token: 0x040009F3 RID: 2547
		ComputeProcessing = 4,
		// Token: 0x040009F4 RID: 2548
		AllGPUOperations = 7
	}
}
