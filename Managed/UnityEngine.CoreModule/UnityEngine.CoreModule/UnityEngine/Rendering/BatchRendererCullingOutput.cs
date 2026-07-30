using System;
using Unity.Jobs;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Rendering
{
	// Token: 0x02000355 RID: 853
	[NativeHeader("Runtime/Camera/BatchRendererGroup.h")]
	[UsedByNativeCode]
	internal struct BatchRendererCullingOutput
	{
		// Token: 0x04000A22 RID: 2594
		public JobHandle cullingJobsFence;

		// Token: 0x04000A23 RID: 2595
		public unsafe Plane* cullingPlanes;

		// Token: 0x04000A24 RID: 2596
		public unsafe BatchVisibility* batchVisibility;

		// Token: 0x04000A25 RID: 2597
		public unsafe int* visibleIndices;

		// Token: 0x04000A26 RID: 2598
		public int cullingPlanesCount;

		// Token: 0x04000A27 RID: 2599
		public int batchVisibilityCount;

		// Token: 0x04000A28 RID: 2600
		public int visibleIndicesCount;
	}
}
