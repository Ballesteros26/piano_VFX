using System;
using Unity.Collections;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Rendering
{
	// Token: 0x02000354 RID: 852
	[NativeHeader("Runtime/Camera/BatchRendererGroup.h")]
	[UsedByNativeCode]
	public struct BatchCullingContext
	{
		// Token: 0x06001D0A RID: 7434 RVA: 0x000304C0 File Offset: 0x0002E6C0
		public BatchCullingContext(NativeArray<Plane> inCullingPlanes, NativeArray<BatchVisibility> inOutBatchVisibility, NativeArray<int> outVisibleIndices, LODParameters inLodParameters)
		{
			this.cullingPlanes = inCullingPlanes;
			this.batchVisibility = inOutBatchVisibility;
			this.visibleIndices = outVisibleIndices;
			this.lodParameters = inLodParameters;
		}

		// Token: 0x04000A1E RID: 2590
		public readonly NativeArray<Plane> cullingPlanes;

		// Token: 0x04000A1F RID: 2591
		public NativeArray<BatchVisibility> batchVisibility;

		// Token: 0x04000A20 RID: 2592
		public NativeArray<int> visibleIndices;

		// Token: 0x04000A21 RID: 2593
		public readonly LODParameters lodParameters;
	}
}
