using System;

namespace System.Linq.Parallel
{
	// Token: 0x02000128 RID: 296
	internal interface IPartitionedStreamRecipient<TElement>
	{
		// Token: 0x06000992 RID: 2450
		void Receive<TKey>(PartitionedStream<TElement, TKey> partitionedStream);
	}
}
