using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x020001AB RID: 427
	internal sealed class DefaultIfEmptyQueryOperator<TSource> : UnaryQueryOperator<TSource, TSource>
	{
		// Token: 0x06000B87 RID: 2951 RVA: 0x00026290 File Offset: 0x00024490
		internal DefaultIfEmptyQueryOperator(IEnumerable<TSource> child, TSource defaultValue)
			: base(child)
		{
			this._defaultValue = defaultValue;
			base.SetOrdinalIndexState(base.Child.OrdinalIndexState.Worse(OrdinalIndexState.Correct));
		}

		// Token: 0x06000B88 RID: 2952 RVA: 0x000262B7 File Offset: 0x000244B7
		internal override QueryResults<TSource> Open(QuerySettings settings, bool preferStriping)
		{
			return new UnaryQueryOperator<TSource, TSource>.UnaryQueryOperatorResults(base.Child.Open(settings, preferStriping), this, settings, preferStriping);
		}

		// Token: 0x06000B89 RID: 2953 RVA: 0x000262D0 File Offset: 0x000244D0
		internal override void WrapPartitionedStream<TKey>(PartitionedStream<TSource, TKey> inputStream, IPartitionedStreamRecipient<TSource> recipient, bool preferStriping, QuerySettings settings)
		{
			int partitionCount = inputStream.PartitionCount;
			Shared<int> shared = new Shared<int>(0);
			CountdownEvent countdownEvent = new CountdownEvent(partitionCount - 1);
			PartitionedStream<TSource, TKey> partitionedStream = new PartitionedStream<TSource, TKey>(partitionCount, inputStream.KeyComparer, this.OrdinalIndexState);
			for (int i = 0; i < partitionCount; i++)
			{
				partitionedStream[i] = new DefaultIfEmptyQueryOperator<TSource>.DefaultIfEmptyQueryOperatorEnumerator<TKey>(inputStream[i], this._defaultValue, i, partitionCount, shared, countdownEvent, settings.CancellationState.MergedCancellationToken);
			}
			recipient.Receive<TKey>(partitionedStream);
		}

		// Token: 0x06000B8A RID: 2954 RVA: 0x0002634A File Offset: 0x0002454A
		internal override IEnumerable<TSource> AsSequentialQuery(CancellationToken token)
		{
			return base.Child.AsSequentialQuery(token).DefaultIfEmpty(this._defaultValue);
		}

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x06000B8B RID: 2955 RVA: 0x00002285 File Offset: 0x00000485
		internal override bool LimitsParallelism
		{
			get
			{
				return false;
			}
		}

		// Token: 0x040006DA RID: 1754
		private readonly TSource _defaultValue;

		// Token: 0x020001AC RID: 428
		private class DefaultIfEmptyQueryOperatorEnumerator<TKey> : QueryOperatorEnumerator<TSource, TKey>
		{
			// Token: 0x06000B8C RID: 2956 RVA: 0x00026363 File Offset: 0x00024563
			internal DefaultIfEmptyQueryOperatorEnumerator(QueryOperatorEnumerator<TSource, TKey> source, TSource defaultValue, int partitionIndex, int partitionCount, Shared<int> sharedEmptyCount, CountdownEvent sharedLatch, CancellationToken cancelToken)
			{
				this._source = source;
				this._defaultValue = defaultValue;
				this._partitionIndex = partitionIndex;
				this._partitionCount = partitionCount;
				this._sharedEmptyCount = sharedEmptyCount;
				this._sharedLatch = sharedLatch;
				this._cancelToken = cancelToken;
			}

			// Token: 0x06000B8D RID: 2957 RVA: 0x000263A0 File Offset: 0x000245A0
			internal override bool MoveNext(ref TSource currentElement, ref TKey currentKey)
			{
				bool flag = this._source.MoveNext(ref currentElement, ref currentKey);
				if (!this._lookedForEmpty)
				{
					this._lookedForEmpty = true;
					if (!flag)
					{
						if (this._partitionIndex == 0)
						{
							this._sharedLatch.Wait(this._cancelToken);
							this._sharedLatch.Dispose();
							if (this._sharedEmptyCount.Value == this._partitionCount - 1)
							{
								currentElement = this._defaultValue;
								currentKey = default(TKey);
								return true;
							}
							return false;
						}
						else
						{
							Interlocked.Increment(ref this._sharedEmptyCount.Value);
						}
					}
					if (this._partitionIndex != 0)
					{
						this._sharedLatch.Signal();
					}
				}
				return flag;
			}

			// Token: 0x06000B8E RID: 2958 RVA: 0x00026443 File Offset: 0x00024643
			protected override void Dispose(bool disposing)
			{
				this._source.Dispose();
			}

			// Token: 0x040006DB RID: 1755
			private QueryOperatorEnumerator<TSource, TKey> _source;

			// Token: 0x040006DC RID: 1756
			private bool _lookedForEmpty;

			// Token: 0x040006DD RID: 1757
			private int _partitionIndex;

			// Token: 0x040006DE RID: 1758
			private int _partitionCount;

			// Token: 0x040006DF RID: 1759
			private TSource _defaultValue;

			// Token: 0x040006E0 RID: 1760
			private Shared<int> _sharedEmptyCount;

			// Token: 0x040006E1 RID: 1761
			private CountdownEvent _sharedLatch;

			// Token: 0x040006E2 RID: 1762
			private CancellationToken _cancelToken;
		}
	}
}
