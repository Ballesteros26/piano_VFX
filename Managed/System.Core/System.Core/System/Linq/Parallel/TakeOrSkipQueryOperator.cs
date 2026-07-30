using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x020001DF RID: 479
	internal sealed class TakeOrSkipQueryOperator<TResult> : UnaryQueryOperator<TResult, TResult>
	{
		// Token: 0x06000C46 RID: 3142 RVA: 0x00028B48 File Offset: 0x00026D48
		internal TakeOrSkipQueryOperator(IEnumerable<TResult> child, int count, bool take)
			: base(child)
		{
			this._count = count;
			this._take = take;
			base.SetOrdinalIndexState(this.OutputOrdinalIndexState());
		}

		// Token: 0x06000C47 RID: 3143 RVA: 0x00028B6C File Offset: 0x00026D6C
		private OrdinalIndexState OutputOrdinalIndexState()
		{
			OrdinalIndexState ordinalIndexState = base.Child.OrdinalIndexState;
			if (ordinalIndexState == OrdinalIndexState.Indexable)
			{
				return OrdinalIndexState.Indexable;
			}
			if (ordinalIndexState.IsWorseThan(OrdinalIndexState.Increasing))
			{
				this._prematureMerge = true;
				ordinalIndexState = OrdinalIndexState.Correct;
			}
			if (!this._take && ordinalIndexState == OrdinalIndexState.Correct)
			{
				ordinalIndexState = OrdinalIndexState.Increasing;
			}
			return ordinalIndexState;
		}

		// Token: 0x06000C48 RID: 3144 RVA: 0x00028BAC File Offset: 0x00026DAC
		internal override void WrapPartitionedStream<TKey>(PartitionedStream<TResult, TKey> inputStream, IPartitionedStreamRecipient<TResult> recipient, bool preferStriping, QuerySettings settings)
		{
			if (this._prematureMerge)
			{
				PartitionedStream<TResult, int> partitionedStream = QueryOperator<TResult>.ExecuteAndCollectResults<TKey>(inputStream, inputStream.PartitionCount, base.Child.OutputOrdered, preferStriping, settings).GetPartitionedStream();
				this.WrapHelper<int>(partitionedStream, recipient, settings);
				return;
			}
			this.WrapHelper<TKey>(inputStream, recipient, settings);
		}

		// Token: 0x06000C49 RID: 3145 RVA: 0x00028BF8 File Offset: 0x00026DF8
		private void WrapHelper<TKey>(PartitionedStream<TResult, TKey> inputStream, IPartitionedStreamRecipient<TResult> recipient, QuerySettings settings)
		{
			int partitionCount = inputStream.PartitionCount;
			FixedMaxHeap<TKey> fixedMaxHeap = new FixedMaxHeap<TKey>(this._count, inputStream.KeyComparer);
			CountdownEvent countdownEvent = new CountdownEvent(partitionCount);
			PartitionedStream<TResult, TKey> partitionedStream = new PartitionedStream<TResult, TKey>(partitionCount, inputStream.KeyComparer, this.OrdinalIndexState);
			for (int i = 0; i < partitionCount; i++)
			{
				partitionedStream[i] = new TakeOrSkipQueryOperator<TResult>.TakeOrSkipQueryOperatorEnumerator<TKey>(inputStream[i], this._take, fixedMaxHeap, countdownEvent, settings.CancellationState.MergedCancellationToken, inputStream.KeyComparer);
			}
			recipient.Receive<TKey>(partitionedStream);
		}

		// Token: 0x06000C4A RID: 3146 RVA: 0x00028C7E File Offset: 0x00026E7E
		internal override QueryResults<TResult> Open(QuerySettings settings, bool preferStriping)
		{
			return TakeOrSkipQueryOperator<TResult>.TakeOrSkipQueryOperatorResults.NewResults(base.Child.Open(settings, true), this, settings, preferStriping);
		}

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x06000C4B RID: 3147 RVA: 0x00002285 File Offset: 0x00000485
		internal override bool LimitsParallelism
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000C4C RID: 3148 RVA: 0x00028C95 File Offset: 0x00026E95
		internal override IEnumerable<TResult> AsSequentialQuery(CancellationToken token)
		{
			if (this._take)
			{
				return base.Child.AsSequentialQuery(token).Take(this._count);
			}
			return CancellableEnumerable.Wrap<TResult>(base.Child.AsSequentialQuery(token), token).Skip(this._count);
		}

		// Token: 0x04000775 RID: 1909
		private readonly int _count;

		// Token: 0x04000776 RID: 1910
		private readonly bool _take;

		// Token: 0x04000777 RID: 1911
		private bool _prematureMerge;

		// Token: 0x020001E0 RID: 480
		private class TakeOrSkipQueryOperatorEnumerator<TKey> : QueryOperatorEnumerator<TResult, TKey>
		{
			// Token: 0x06000C4D RID: 3149 RVA: 0x00028CD4 File Offset: 0x00026ED4
			internal TakeOrSkipQueryOperatorEnumerator(QueryOperatorEnumerator<TResult, TKey> source, bool take, FixedMaxHeap<TKey> sharedIndices, CountdownEvent sharedBarrier, CancellationToken cancellationToken, IComparer<TKey> keyComparer)
			{
				this._source = source;
				this._count = sharedIndices.Size;
				this._take = take;
				this._sharedIndices = sharedIndices;
				this._sharedBarrier = sharedBarrier;
				this._cancellationToken = cancellationToken;
				this._keyComparer = keyComparer;
			}

			// Token: 0x06000C4E RID: 3150 RVA: 0x00028D20 File Offset: 0x00026F20
			internal override bool MoveNext(ref TResult currentElement, ref TKey currentKey)
			{
				if (this._buffer == null && this._count > 0)
				{
					List<Pair<TResult, TKey>> list = new List<Pair<TResult, TKey>>();
					TResult tresult = default(TResult);
					TKey tkey = default(TKey);
					int num = 0;
					while (list.Count < this._count && this._source.MoveNext(ref tresult, ref tkey))
					{
						if ((num++ & 63) == 0)
						{
							CancellationState.ThrowIfCanceled(this._cancellationToken);
						}
						list.Add(new Pair<TResult, TKey>(tresult, tkey));
						FixedMaxHeap<TKey> sharedIndices = this._sharedIndices;
						lock (sharedIndices)
						{
							if (!this._sharedIndices.Insert(tkey))
							{
								break;
							}
						}
					}
					this._sharedBarrier.Signal();
					this._sharedBarrier.Wait(this._cancellationToken);
					this._buffer = list;
					this._bufferIndex = new Shared<int>(-1);
				}
				if (!this._take)
				{
					TKey tkey2 = default(TKey);
					if (this._count > 0)
					{
						if (this._sharedIndices.Count < this._count)
						{
							return false;
						}
						tkey2 = this._sharedIndices.MaxValue;
						if (this._bufferIndex.Value < this._buffer.Count - 1)
						{
							this._bufferIndex.Value++;
							while (this._bufferIndex.Value < this._buffer.Count)
							{
								if (this._keyComparer.Compare(this._buffer[this._bufferIndex.Value].Second, tkey2) > 0)
								{
									currentElement = this._buffer[this._bufferIndex.Value].First;
									currentKey = this._buffer[this._bufferIndex.Value].Second;
									return true;
								}
								this._bufferIndex.Value++;
							}
						}
					}
					return this._source.MoveNext(ref currentElement, ref currentKey);
				}
				if (this._count == 0 || this._bufferIndex.Value >= this._buffer.Count - 1)
				{
					return false;
				}
				this._bufferIndex.Value++;
				currentElement = this._buffer[this._bufferIndex.Value].First;
				currentKey = this._buffer[this._bufferIndex.Value].Second;
				return this._sharedIndices.Count == 0 || this._keyComparer.Compare(this._buffer[this._bufferIndex.Value].Second, this._sharedIndices.MaxValue) <= 0;
			}

			// Token: 0x06000C4F RID: 3151 RVA: 0x00029010 File Offset: 0x00027210
			protected override void Dispose(bool disposing)
			{
				this._source.Dispose();
			}

			// Token: 0x04000778 RID: 1912
			private readonly QueryOperatorEnumerator<TResult, TKey> _source;

			// Token: 0x04000779 RID: 1913
			private readonly int _count;

			// Token: 0x0400077A RID: 1914
			private readonly bool _take;

			// Token: 0x0400077B RID: 1915
			private readonly IComparer<TKey> _keyComparer;

			// Token: 0x0400077C RID: 1916
			private readonly FixedMaxHeap<TKey> _sharedIndices;

			// Token: 0x0400077D RID: 1917
			private readonly CountdownEvent _sharedBarrier;

			// Token: 0x0400077E RID: 1918
			private readonly CancellationToken _cancellationToken;

			// Token: 0x0400077F RID: 1919
			private List<Pair<TResult, TKey>> _buffer;

			// Token: 0x04000780 RID: 1920
			private Shared<int> _bufferIndex;
		}

		// Token: 0x020001E1 RID: 481
		private class TakeOrSkipQueryOperatorResults : UnaryQueryOperator<TResult, TResult>.UnaryQueryOperatorResults
		{
			// Token: 0x06000C50 RID: 3152 RVA: 0x0002901D File Offset: 0x0002721D
			public static QueryResults<TResult> NewResults(QueryResults<TResult> childQueryResults, TakeOrSkipQueryOperator<TResult> op, QuerySettings settings, bool preferStriping)
			{
				if (childQueryResults.IsIndexible)
				{
					return new TakeOrSkipQueryOperator<TResult>.TakeOrSkipQueryOperatorResults(childQueryResults, op, settings, preferStriping);
				}
				return new UnaryQueryOperator<TResult, TResult>.UnaryQueryOperatorResults(childQueryResults, op, settings, preferStriping);
			}

			// Token: 0x06000C51 RID: 3153 RVA: 0x0002903A File Offset: 0x0002723A
			private TakeOrSkipQueryOperatorResults(QueryResults<TResult> childQueryResults, TakeOrSkipQueryOperator<TResult> takeOrSkipOp, QuerySettings settings, bool preferStriping)
				: base(childQueryResults, takeOrSkipOp, settings, preferStriping)
			{
				this._takeOrSkipOp = takeOrSkipOp;
				this._childCount = this._childQueryResults.ElementsCount;
			}

			// Token: 0x1700018E RID: 398
			// (get) Token: 0x06000C52 RID: 3154 RVA: 0x0002905F File Offset: 0x0002725F
			internal override bool IsIndexible
			{
				get
				{
					return this._childCount >= 0;
				}
			}

			// Token: 0x1700018F RID: 399
			// (get) Token: 0x06000C53 RID: 3155 RVA: 0x0002906D File Offset: 0x0002726D
			internal override int ElementsCount
			{
				get
				{
					if (this._takeOrSkipOp._take)
					{
						return Math.Min(this._childCount, this._takeOrSkipOp._count);
					}
					return Math.Max(this._childCount - this._takeOrSkipOp._count, 0);
				}
			}

			// Token: 0x06000C54 RID: 3156 RVA: 0x000290AB File Offset: 0x000272AB
			internal override TResult GetElement(int index)
			{
				if (this._takeOrSkipOp._take)
				{
					return this._childQueryResults.GetElement(index);
				}
				return this._childQueryResults.GetElement(this._takeOrSkipOp._count + index);
			}

			// Token: 0x04000781 RID: 1921
			private TakeOrSkipQueryOperator<TResult> _takeOrSkipOp;

			// Token: 0x04000782 RID: 1922
			private int _childCount;
		}
	}
}
