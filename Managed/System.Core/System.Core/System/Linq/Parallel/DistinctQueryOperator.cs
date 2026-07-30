using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x020001AD RID: 429
	internal sealed class DistinctQueryOperator<TInputOutput> : UnaryQueryOperator<TInputOutput, TInputOutput>
	{
		// Token: 0x06000B8F RID: 2959 RVA: 0x00026450 File Offset: 0x00024650
		internal DistinctQueryOperator(IEnumerable<TInputOutput> source, IEqualityComparer<TInputOutput> comparer)
			: base(source)
		{
			this._comparer = comparer;
			base.SetOrdinalIndexState(OrdinalIndexState.Shuffled);
		}

		// Token: 0x06000B90 RID: 2960 RVA: 0x00026467 File Offset: 0x00024667
		internal override QueryResults<TInputOutput> Open(QuerySettings settings, bool preferStriping)
		{
			return new UnaryQueryOperator<TInputOutput, TInputOutput>.UnaryQueryOperatorResults(base.Child.Open(settings, false), this, settings, false);
		}

		// Token: 0x06000B91 RID: 2961 RVA: 0x00026480 File Offset: 0x00024680
		internal override void WrapPartitionedStream<TKey>(PartitionedStream<TInputOutput, TKey> inputStream, IPartitionedStreamRecipient<TInputOutput> recipient, bool preferStriping, QuerySettings settings)
		{
			if (base.OutputOrdered)
			{
				this.WrapPartitionedStreamHelper<TKey>(ExchangeUtilities.HashRepartitionOrdered<TInputOutput, NoKeyMemoizationRequired, TKey>(inputStream, null, null, this._comparer, settings.CancellationState.MergedCancellationToken), recipient, settings.CancellationState.MergedCancellationToken);
				return;
			}
			this.WrapPartitionedStreamHelper<int>(ExchangeUtilities.HashRepartition<TInputOutput, NoKeyMemoizationRequired, TKey>(inputStream, null, null, this._comparer, settings.CancellationState.MergedCancellationToken), recipient, settings.CancellationState.MergedCancellationToken);
		}

		// Token: 0x06000B92 RID: 2962 RVA: 0x000264F0 File Offset: 0x000246F0
		private void WrapPartitionedStreamHelper<TKey>(PartitionedStream<Pair<TInputOutput, NoKeyMemoizationRequired>, TKey> hashStream, IPartitionedStreamRecipient<TInputOutput> recipient, CancellationToken cancellationToken)
		{
			int partitionCount = hashStream.PartitionCount;
			PartitionedStream<TInputOutput, TKey> partitionedStream = new PartitionedStream<TInputOutput, TKey>(partitionCount, hashStream.KeyComparer, OrdinalIndexState.Shuffled);
			for (int i = 0; i < partitionCount; i++)
			{
				if (base.OutputOrdered)
				{
					partitionedStream[i] = new DistinctQueryOperator<TInputOutput>.OrderedDistinctQueryOperatorEnumerator<TKey>(hashStream[i], this._comparer, hashStream.KeyComparer, cancellationToken);
				}
				else
				{
					partitionedStream[i] = (QueryOperatorEnumerator<TInputOutput, TKey>)new DistinctQueryOperator<TInputOutput>.DistinctQueryOperatorEnumerator<TKey>(hashStream[i], this._comparer, cancellationToken);
				}
			}
			recipient.Receive<TKey>(partitionedStream);
		}

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x06000B93 RID: 2963 RVA: 0x00002285 File Offset: 0x00000485
		internal override bool LimitsParallelism
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000B94 RID: 2964 RVA: 0x0002656E File Offset: 0x0002476E
		internal override IEnumerable<TInputOutput> AsSequentialQuery(CancellationToken token)
		{
			return CancellableEnumerable.Wrap<TInputOutput>(base.Child.AsSequentialQuery(token), token).Distinct(this._comparer);
		}

		// Token: 0x040006E3 RID: 1763
		private readonly IEqualityComparer<TInputOutput> _comparer;

		// Token: 0x020001AE RID: 430
		private class DistinctQueryOperatorEnumerator<TKey> : QueryOperatorEnumerator<TInputOutput, int>
		{
			// Token: 0x06000B95 RID: 2965 RVA: 0x0002658D File Offset: 0x0002478D
			internal DistinctQueryOperatorEnumerator(QueryOperatorEnumerator<Pair<TInputOutput, NoKeyMemoizationRequired>, TKey> source, IEqualityComparer<TInputOutput> comparer, CancellationToken cancellationToken)
			{
				this._source = source;
				this._hashLookup = new Set<TInputOutput>(comparer);
				this._cancellationToken = cancellationToken;
			}

			// Token: 0x06000B96 RID: 2966 RVA: 0x000265B0 File Offset: 0x000247B0
			internal override bool MoveNext(ref TInputOutput currentElement, ref int currentKey)
			{
				TKey tkey = default(TKey);
				Pair<TInputOutput, NoKeyMemoizationRequired> pair = default(Pair<TInputOutput, NoKeyMemoizationRequired>);
				if (this._outputLoopCount == null)
				{
					this._outputLoopCount = new Shared<int>(0);
				}
				while (this._source.MoveNext(ref pair, ref tkey))
				{
					Shared<int> outputLoopCount = this._outputLoopCount;
					int value = outputLoopCount.Value;
					outputLoopCount.Value = value + 1;
					if ((value & 63) == 0)
					{
						CancellationState.ThrowIfCanceled(this._cancellationToken);
					}
					if (this._hashLookup.Add(pair.First))
					{
						currentElement = pair.First;
						return true;
					}
				}
				return false;
			}

			// Token: 0x06000B97 RID: 2967 RVA: 0x0002663E File Offset: 0x0002483E
			protected override void Dispose(bool disposing)
			{
				this._source.Dispose();
			}

			// Token: 0x040006E4 RID: 1764
			private QueryOperatorEnumerator<Pair<TInputOutput, NoKeyMemoizationRequired>, TKey> _source;

			// Token: 0x040006E5 RID: 1765
			private Set<TInputOutput> _hashLookup;

			// Token: 0x040006E6 RID: 1766
			private CancellationToken _cancellationToken;

			// Token: 0x040006E7 RID: 1767
			private Shared<int> _outputLoopCount;
		}

		// Token: 0x020001AF RID: 431
		private class OrderedDistinctQueryOperatorEnumerator<TKey> : QueryOperatorEnumerator<TInputOutput, TKey>
		{
			// Token: 0x06000B98 RID: 2968 RVA: 0x0002664B File Offset: 0x0002484B
			internal OrderedDistinctQueryOperatorEnumerator(QueryOperatorEnumerator<Pair<TInputOutput, NoKeyMemoizationRequired>, TKey> source, IEqualityComparer<TInputOutput> comparer, IComparer<TKey> keyComparer, CancellationToken cancellationToken)
			{
				this._source = source;
				this._keyComparer = keyComparer;
				this._hashLookup = new Dictionary<Wrapper<TInputOutput>, TKey>(new WrapperEqualityComparer<TInputOutput>(comparer));
				this._cancellationToken = cancellationToken;
			}

			// Token: 0x06000B99 RID: 2969 RVA: 0x00026680 File Offset: 0x00024880
			internal override bool MoveNext(ref TInputOutput currentElement, ref TKey currentKey)
			{
				if (this._hashLookupEnumerator == null)
				{
					Pair<TInputOutput, NoKeyMemoizationRequired> pair = default(Pair<TInputOutput, NoKeyMemoizationRequired>);
					TKey tkey = default(TKey);
					int num = 0;
					while (this._source.MoveNext(ref pair, ref tkey))
					{
						if ((num++ & 63) == 0)
						{
							CancellationState.ThrowIfCanceled(this._cancellationToken);
						}
						Wrapper<TInputOutput> wrapper = new Wrapper<TInputOutput>(pair.First);
						TKey tkey2;
						if (!this._hashLookup.TryGetValue(wrapper, out tkey2) || this._keyComparer.Compare(tkey, tkey2) < 0)
						{
							this._hashLookup[wrapper] = tkey;
						}
					}
					this._hashLookupEnumerator = this._hashLookup.GetEnumerator();
				}
				if (this._hashLookupEnumerator.MoveNext())
				{
					KeyValuePair<Wrapper<TInputOutput>, TKey> keyValuePair = this._hashLookupEnumerator.Current;
					currentElement = keyValuePair.Key.Value;
					currentKey = keyValuePair.Value;
					return true;
				}
				return false;
			}

			// Token: 0x06000B9A RID: 2970 RVA: 0x00026761 File Offset: 0x00024961
			protected override void Dispose(bool disposing)
			{
				this._source.Dispose();
				if (this._hashLookupEnumerator != null)
				{
					this._hashLookupEnumerator.Dispose();
				}
			}

			// Token: 0x040006E8 RID: 1768
			private QueryOperatorEnumerator<Pair<TInputOutput, NoKeyMemoizationRequired>, TKey> _source;

			// Token: 0x040006E9 RID: 1769
			private Dictionary<Wrapper<TInputOutput>, TKey> _hashLookup;

			// Token: 0x040006EA RID: 1770
			private IComparer<TKey> _keyComparer;

			// Token: 0x040006EB RID: 1771
			private IEnumerator<KeyValuePair<Wrapper<TInputOutput>, TKey>> _hashLookupEnumerator;

			// Token: 0x040006EC RID: 1772
			private CancellationToken _cancellationToken;
		}
	}
}
