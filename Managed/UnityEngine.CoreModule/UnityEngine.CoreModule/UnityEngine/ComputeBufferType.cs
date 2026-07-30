using System;

namespace UnityEngine
{
	// Token: 0x0200010B RID: 267
	[Flags]
	public enum ComputeBufferType
	{
		// Token: 0x040002D1 RID: 721
		Default = 0,
		// Token: 0x040002D2 RID: 722
		Raw = 1,
		// Token: 0x040002D3 RID: 723
		Append = 2,
		// Token: 0x040002D4 RID: 724
		Counter = 4,
		// Token: 0x040002D5 RID: 725
		Constant = 8,
		// Token: 0x040002D6 RID: 726
		Structured = 16,
		// Token: 0x040002D7 RID: 727
		[Obsolete("Enum member DrawIndirect has been deprecated. Use IndirectArguments instead (UnityUpgradable) -> IndirectArguments", false)]
		DrawIndirect = 256,
		// Token: 0x040002D8 RID: 728
		IndirectArguments = 256,
		// Token: 0x040002D9 RID: 729
		[Obsolete("Enum member GPUMemory has been deprecated. All compute buffers now follow the behavior previously defined by this member.", false)]
		GPUMemory = 512
	}
}
