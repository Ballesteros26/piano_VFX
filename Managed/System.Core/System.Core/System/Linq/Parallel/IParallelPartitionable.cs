using System;

namespace System.Linq.Parallel
{
	// Token: 0x0200010C RID: 268
	internal interface IParallelPartitionable<T>
	{
		// Token: 0x06000937 RID: 2359
		QueryOperatorEnumerator<T, int>[] GetPartitions(int partitionCount);
	}
}
