using System;

namespace UnityEngine.Profiling.Memory.Experimental
{
	// Token: 0x02000217 RID: 535
	[Flags]
	public enum CaptureFlags : uint
	{
		// Token: 0x04000754 RID: 1876
		ManagedObjects = 1U,
		// Token: 0x04000755 RID: 1877
		NativeObjects = 2U,
		// Token: 0x04000756 RID: 1878
		NativeAllocations = 4U,
		// Token: 0x04000757 RID: 1879
		NativeAllocationSites = 8U,
		// Token: 0x04000758 RID: 1880
		NativeStackTraces = 16U
	}
}
