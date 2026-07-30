using System;

namespace UnityEngine.UIElements.UIR
{
	// Token: 0x02000242 RID: 578
	internal struct HeapStatistics
	{
		// Token: 0x0400080A RID: 2058
		public uint numAllocs;

		// Token: 0x0400080B RID: 2059
		public uint totalSize;

		// Token: 0x0400080C RID: 2060
		public uint allocatedSize;

		// Token: 0x0400080D RID: 2061
		public uint freeSize;

		// Token: 0x0400080E RID: 2062
		public uint largestAvailableBlock;

		// Token: 0x0400080F RID: 2063
		public uint availableBlocksCount;

		// Token: 0x04000810 RID: 2064
		public uint blockCount;

		// Token: 0x04000811 RID: 2065
		public uint highWatermark;

		// Token: 0x04000812 RID: 2066
		public float fragmentation;

		// Token: 0x04000813 RID: 2067
		public HeapStatistics[] subAllocators;
	}
}
