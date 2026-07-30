using System;

namespace System.Runtime
{
	/// <summary>Indicates whether the next blocking garbage collection compacts the large object heap (LOH). </summary>
	// Token: 0x020006B5 RID: 1717
	public enum GCLargeObjectHeapCompactionMode
	{
		/// <summary>Blocking garbage collections do not compact the large object heap (LOH).</summary>
		// Token: 0x04002673 RID: 9843
		Default = 1,
		/// <summary>The large object heap (LOH) will be compacted during the next blocking garbage collection. </summary>
		// Token: 0x04002674 RID: 9844
		CompactOnce
	}
}
