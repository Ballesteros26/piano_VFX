using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x020001B2 RID: 434
	internal sealed class FirstQueryOperator<TSource> : UnaryQueryOperator<TSource, TSource>
	{
		// Token: 0x06000BA4 RID: 2980 RVA: 0x00026A20 File Offset: 0x00024C20
		internal FirstQueryOperator(IEnumerable<TSource> child, Func<TSource, bool> predicate)
			: base(child)
		{
			this._predicate = predicate;
			this._prematureMergeNeeded = base.Child.OrdinalIndexState.IsWorseThan(OrdinalIndexState.Increasing);
		}

		// Token: 0x06000BA5 RID: 2981 RVA: 0x000267C8 File Offset: 0x000249C8
		internal override QueryResults<TSource> Open(QuerySettings settings, bool preferStriping)
		{
			return new UnaryQueryOperator<TSource, TSource>.UnaryQueryOperatorResults(base.Child.Open(settings, false), this, settings, preferStriping);
		}

		// Token: 0x06000BA6 RID: 2982 RVA: 0x00026A48 File Offset: 0x00024C48
		internal override void WrapPartitionedStream<TKey>(PartitionedStream<TSource, TKey> inputStream, IPartitionedStreamRecipient<TSource> recipient, bool preferStriping, QuerySettings settings)
		{
			if (this._prematureMergeNeeded)
			{
				ListQueryResults<TSource> listQueryResults = QueryOperator<TSource>.ExecuteAndCollectResults<TKey>(inputStream, inputStream.PartitionCount, base.Child.OutputOrdered, preferStriping, settings);
				this.WrapHelper<int>(listQueryResults.GetPartitionedStream(), recipient, settings);
				return;
			}
			this.WrapHelper<TKey>(inputStream, recipient, settings);
		}

		// Token: 0x06000BA7 RID: 2983 RVA: 0x00026A94 File Offset: 0x00024C94
		private void WrapHelper<TKey>(PartitionedStream<TSource, TKey> inputStream, IPartitionedStreamRecipient<TSource> recipient, QuerySettings settings)
		{
			int partitionCount = inputStream.PartitionCount;
			FirstQueryOperator<TSource>.FirstQueryOperatorState<TKey> firstQueryOperatorState = new FirstQueryOperator<TSource>.FirstQueryOperatorState<TKey>();
			CountdownEvent countdownEvent = new CountdownEvent(partitionCount);
			PartitionedStream<TSource, int> partitionedStream = new PartitionedStream<TSource, int>(partitionCount, Util.GetDefaultComparer<int>(), OrdinalIndexState.Shuffled);
			for (int i = 0; i < partitionCount; i++)
			{
				partitionedStream[i] = new FirstQueryOperator<TSource>.FirstQueryOperatorEnumerator<TKey>(inputStream[i], this._predicate, firstQueryOperatorState, countdownEvent, settings.CancellationState.MergedCancellationToken, inputStream.KeyComparer, i);
			}
			recipient.Receive<int>(partitionedStream);
		}

		// Token: 0x06000BA8 RID: 2984 RVA: 0x00003CCF File Offset: 0x00001ECF
		[ExcludeFromCodeCoverage]
		internal override IEnumerable<TSource> AsSequentialQuery(CancellationToken token)
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x06000BA9 RID: 2985 RVA: 0x00002285 File Offset: 0x00000485
		internal override bool LimitsParallelism
		{
			get
			{
				return false;
			}
		}

		// Token: 0x040006F4 RID: 1780
		private readonly Func<TSource, bool> _predicate;

		// Token: 0x040006F5 RID: 1781
		private readonly bool _prematureMergeNeeded;

		// Token: 0x020001B3 RID: 435
		private class FirstQueryOperatorEnumerator<TKey> : QueryOperatorEnumerator<TSource, int>
		{
			// Token: 0x06000BAA RID: 2986 RVA: 0x00026B0A File Offset: 0x00024D0A
			internal FirstQueryOperatorEnumerator(QueryOperatorEnumerator<TSource, TKey> source, Func<TSource, bool> predicate, FirstQueryOperator<TSource>.FirstQueryOperatorState<TKey> operatorState, CountdownEvent sharedBarrier, CancellationToken cancellationToken, IComparer<TKey> keyComparer, int partitionId)
			{
				this._source = source;
				this._predicate = predicate;
				this._operatorState = operatorState;
				this._sharedBarrier = sharedBarrier;
				this._cancellationToken = cancellationToken;
				this._keyComparer = keyComparer;
				this._partitionId = partitionId;
			}

			// Token: 0x06000BAB RID: 2987 RVA: 0x00026B48 File Offset: 0x00024D48
			internal override bool MoveNext(ref TSource currentElement, ref int currentKey)
			{
				if (this._alreadySearched)
				{
					return false;
				}
				TSource tsource = default(TSource);
				TKey tkey = default(TKey);
				try
				{
					TSource tsource2 = default(TSource);
					TKey tkey2 = default(TKey);
					int num = 0;
					while (this._source.MoveNext(ref tsource2, ref tkey2))
					{
						if ((num++ & 63) == 0)
						{
							CancellationState.ThrowIfCanceled(this._cancellationToken);
						}
						if (this._predicate == null || this._predicate(tsource2))
						{
							tsource = tsource2;
							tkey = tkey2;
							FirstQueryOperator<TSource>.FirstQueryOperatorState<TKey> operatorState = this._operatorState;
							lock (operatorState)
							{
								if (this._operatorState._partitionId == -1 || this._keyComparer.Compare(tkey, this._operatorState._key) < 0)
								{
									this._operatorState._key = tkey;
									this._operatorState._partitionId = this._partitionId;
								}
								break;
							}
						}
					}
				}
				finally
				{
					this._sharedBarrier.Signal();
				}
				this._alreadySearched = true;
				if (this._partitionId == this._operatorState._partitionId)
				{
					this._sharedBarrier.Wait(this._cancellationToken);
					if (this._partitionId == this._operatorState._partitionId)
					{
						currentElement = tsource;
						currentKey = 0;
						return true;
					}
				}
				return false;
			}

			// Token: 0x06000BAC RID: 2988 RVA: 0x00026CAC File Offset: 0x00024EAC
			protected override void Dispose(bool disposing)
			{
				this._source.Dispose();
			}

			// Token: 0x040006F6 RID: 1782
			private QueryOperatorEnumerator<TSource, TKey> _source;

			// Token: 0x040006F7 RID: 1783
			private Func<TSource, bool> _predicate;

			// Token: 0x040006F8 RID: 1784
			private bool _alreadySearched;

			// Token: 0x040006F9 RID: 1785
			private int _partitionId;

			// Token: 0x040006FA RID: 1786
			private FirstQueryOperator<TSource>.FirstQueryOperatorState<TKey> _operatorState;

			// Token: 0x040006FB RID: 1787
			private CountdownEvent _sharedBarrier;

			// Token: 0x040006FC RID: 1788
			private CancellationToken _cancellationToken;

			// Token: 0x040006FD RID: 1789
			private IComparer<TKey> _keyComparer;
		}

		// Token: 0x020001B4 RID: 436
		private class FirstQueryOperatorState<TKey>
		{
			// Token: 0x040006FE RID: 1790
			internal TKey _key;

			// Token: 0x040006FF RID: 1791
			internal int _partitionId = -1;
		}
	}
}
