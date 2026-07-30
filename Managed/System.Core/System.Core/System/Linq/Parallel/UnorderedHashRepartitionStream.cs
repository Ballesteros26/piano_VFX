using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x02000136 RID: 310
	internal class UnorderedHashRepartitionStream<TInputOutput, THashKey, TIgnoreKey> : HashRepartitionStream<TInputOutput, THashKey, int>
	{
		// Token: 0x060009B2 RID: 2482 RVA: 0x0001FB8C File Offset: 0x0001DD8C
		internal UnorderedHashRepartitionStream(PartitionedStream<TInputOutput, TIgnoreKey> inputStream, Func<TInputOutput, THashKey> keySelector, IEqualityComparer<THashKey> keyComparer, IEqualityComparer<TInputOutput> elementComparer, CancellationToken cancellationToken)
			: base(inputStream.PartitionCount, Util.GetDefaultComparer<int>(), keyComparer, elementComparer)
		{
			this._partitions = new HashRepartitionEnumerator<TInputOutput, THashKey, TIgnoreKey>[inputStream.PartitionCount];
			CountdownEvent countdownEvent = new CountdownEvent(inputStream.PartitionCount);
			ListChunk<Pair<TInputOutput, THashKey>>[][] array = JaggedArray<ListChunk<Pair<TInputOutput, THashKey>>>.Allocate(inputStream.PartitionCount, inputStream.PartitionCount);
			for (int i = 0; i < inputStream.PartitionCount; i++)
			{
				this._partitions[i] = new HashRepartitionEnumerator<TInputOutput, THashKey, TIgnoreKey>(inputStream[i], inputStream.PartitionCount, i, keySelector, this, countdownEvent, array, cancellationToken);
			}
		}
	}
}
