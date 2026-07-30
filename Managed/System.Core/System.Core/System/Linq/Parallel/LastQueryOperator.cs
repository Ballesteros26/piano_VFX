using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x020001CA RID: 458
	internal sealed class LastQueryOperator<TSource> : UnaryQueryOperator<TSource, TSource>
	{
		// Token: 0x06000BFA RID: 3066 RVA: 0x00027B5D File Offset: 0x00025D5D
		internal LastQueryOperator(IEnumerable<TSource> child, Func<TSource, bool> predicate)
			: base(child)
		{
			this._predicate = predicate;
			this._prematureMergeNeeded = base.Child.OrdinalIndexState.IsWorseThan(OrdinalIndexState.Increasing);
		}

		// Token: 0x06000BFB RID: 3067 RVA: 0x000267C8 File Offset: 0x000249C8
		internal override QueryResults<TSource> Open(QuerySettings settings, bool preferStriping)
		{
			return new UnaryQueryOperator<TSource, TSource>.UnaryQueryOperatorResults(base.Child.Open(settings, false), this, settings, preferStriping);
		}

		// Token: 0x06000BFC RID: 3068 RVA: 0x00027B84 File Offset: 0x00025D84
		internal override void WrapPartitionedStream<TKey>(PartitionedStream<TSource, TKey> inputStream, IPartitionedStreamRecipient<TSource> recipient, bool preferStriping, QuerySettings settings)
		{
			if (this._prematureMergeNeeded)
			{
				PartitionedStream<TSource, int> partitionedStream = QueryOperator<TSource>.ExecuteAndCollectResults<TKey>(inputStream, inputStream.PartitionCount, base.Child.OutputOrdered, preferStriping, settings).GetPartitionedStream();
				this.WrapHelper<int>(partitionedStream, recipient, settings);
				return;
			}
			this.WrapHelper<TKey>(inputStream, recipient, settings);
		}

		// Token: 0x06000BFD RID: 3069 RVA: 0x00027BD0 File Offset: 0x00025DD0
		private void WrapHelper<TKey>(PartitionedStream<TSource, TKey> inputStream, IPartitionedStreamRecipient<TSource> recipient, QuerySettings settings)
		{
			int partitionCount = inputStream.PartitionCount;
			LastQueryOperator<TSource>.LastQueryOperatorState<TKey> lastQueryOperatorState = new LastQueryOperator<TSource>.LastQueryOperatorState<TKey>();
			CountdownEvent countdownEvent = new CountdownEvent(partitionCount);
			PartitionedStream<TSource, int> partitionedStream = new PartitionedStream<TSource, int>(partitionCount, Util.GetDefaultComparer<int>(), OrdinalIndexState.Shuffled);
			for (int i = 0; i < partitionCount; i++)
			{
				partitionedStream[i] = new LastQueryOperator<TSource>.LastQueryOperatorEnumerator<TKey>(inputStream[i], this._predicate, lastQueryOperatorState, countdownEvent, settings.CancellationState.MergedCancellationToken, inputStream.KeyComparer, i);
			}
			recipient.Receive<int>(partitionedStream);
		}

		// Token: 0x06000BFE RID: 3070 RVA: 0x00003CCF File Offset: 0x00001ECF
		[ExcludeFromCodeCoverage]
		internal override IEnumerable<TSource> AsSequentialQuery(CancellationToken token)
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x06000BFF RID: 3071 RVA: 0x00002285 File Offset: 0x00000485
		internal override bool LimitsParallelism
		{
			get
			{
				return false;
			}
		}

		// Token: 0x04000734 RID: 1844
		private readonly Func<TSource, bool> _predicate;

		// Token: 0x04000735 RID: 1845
		private readonly bool _prematureMergeNeeded;

		// Token: 0x020001CB RID: 459
		private class LastQueryOperatorEnumerator<TKey> : QueryOperatorEnumerator<TSource, int>
		{
			// Token: 0x06000C00 RID: 3072 RVA: 0x00027C46 File Offset: 0x00025E46
			internal LastQueryOperatorEnumerator(QueryOperatorEnumerator<TSource, TKey> source, Func<TSource, bool> predicate, LastQueryOperator<TSource>.LastQueryOperatorState<TKey> operatorState, CountdownEvent sharedBarrier, CancellationToken cancelToken, IComparer<TKey> keyComparer, int partitionId)
			{
				this._source = source;
				this._predicate = predicate;
				this._operatorState = operatorState;
				this._sharedBarrier = sharedBarrier;
				this._cancellationToken = cancelToken;
				this._keyComparer = keyComparer;
				this._partitionId = partitionId;
			}

			// Token: 0x06000C01 RID: 3073 RVA: 0x00027C84 File Offset: 0x00025E84
			internal override bool MoveNext(ref TSource currentElement, ref int currentKey)
			{
				if (this._alreadySearched)
				{
					return false;
				}
				TSource tsource = default(TSource);
				TKey tkey = default(TKey);
				bool flag = false;
				try
				{
					int num = 0;
					TSource tsource2 = default(TSource);
					TKey tkey2 = default(TKey);
					while (this._source.MoveNext(ref tsource2, ref tkey2))
					{
						if ((num & 63) == 0)
						{
							CancellationState.ThrowIfCanceled(this._cancellationToken);
						}
						if (this._predicate == null || this._predicate(tsource2))
						{
							tsource = tsource2;
							tkey = tkey2;
							flag = true;
						}
						num++;
					}
					if (flag)
					{
						LastQueryOperator<TSource>.LastQueryOperatorState<TKey> operatorState = this._operatorState;
						lock (operatorState)
						{
							if (this._operatorState._partitionId == -1 || this._keyComparer.Compare(tkey, this._operatorState._key) > 0)
							{
								this._operatorState._partitionId = this._partitionId;
								this._operatorState._key = tkey;
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
					if (this._operatorState._partitionId == this._partitionId)
					{
						currentElement = tsource;
						currentKey = 0;
						return true;
					}
				}
				return false;
			}

			// Token: 0x06000C02 RID: 3074 RVA: 0x00027DE8 File Offset: 0x00025FE8
			protected override void Dispose(bool disposing)
			{
				this._source.Dispose();
			}

			// Token: 0x04000736 RID: 1846
			private QueryOperatorEnumerator<TSource, TKey> _source;

			// Token: 0x04000737 RID: 1847
			private Func<TSource, bool> _predicate;

			// Token: 0x04000738 RID: 1848
			private bool _alreadySearched;

			// Token: 0x04000739 RID: 1849
			private int _partitionId;

			// Token: 0x0400073A RID: 1850
			private LastQueryOperator<TSource>.LastQueryOperatorState<TKey> _operatorState;

			// Token: 0x0400073B RID: 1851
			private CountdownEvent _sharedBarrier;

			// Token: 0x0400073C RID: 1852
			private CancellationToken _cancellationToken;

			// Token: 0x0400073D RID: 1853
			private IComparer<TKey> _keyComparer;
		}

		// Token: 0x020001CC RID: 460
		private class LastQueryOperatorState<TKey>
		{
			// Token: 0x0400073E RID: 1854
			internal TKey _key;

			// Token: 0x0400073F RID: 1855
			internal int _partitionId = -1;
		}
	}
}
