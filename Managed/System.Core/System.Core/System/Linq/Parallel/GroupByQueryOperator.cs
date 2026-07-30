using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x020001B7 RID: 439
	internal sealed class GroupByQueryOperator<TSource, TGroupKey, TElement> : UnaryQueryOperator<TSource, IGrouping<TGroupKey, TElement>>
	{
		// Token: 0x06000BB7 RID: 2999 RVA: 0x00026E15 File Offset: 0x00025015
		internal GroupByQueryOperator(IEnumerable<TSource> child, Func<TSource, TGroupKey> keySelector, Func<TSource, TElement> elementSelector, IEqualityComparer<TGroupKey> keyComparer)
			: base(child)
		{
			this._keySelector = keySelector;
			this._elementSelector = elementSelector;
			this._keyComparer = keyComparer;
			base.SetOrdinalIndexState(OrdinalIndexState.Shuffled);
		}

		// Token: 0x06000BB8 RID: 3000 RVA: 0x00026E3C File Offset: 0x0002503C
		internal override void WrapPartitionedStream<TKey>(PartitionedStream<TSource, TKey> inputStream, IPartitionedStreamRecipient<IGrouping<TGroupKey, TElement>> recipient, bool preferStriping, QuerySettings settings)
		{
			if (base.Child.OutputOrdered)
			{
				this.WrapPartitionedStreamHelperOrdered<TKey>(ExchangeUtilities.HashRepartitionOrdered<TSource, TGroupKey, TKey>(inputStream, this._keySelector, this._keyComparer, null, settings.CancellationState.MergedCancellationToken), recipient, settings.CancellationState.MergedCancellationToken);
				return;
			}
			this.WrapPartitionedStreamHelper<TKey, int>(ExchangeUtilities.HashRepartition<TSource, TGroupKey, TKey>(inputStream, this._keySelector, this._keyComparer, null, settings.CancellationState.MergedCancellationToken), recipient, settings.CancellationState.MergedCancellationToken);
		}

		// Token: 0x06000BB9 RID: 3001 RVA: 0x00026EBC File Offset: 0x000250BC
		private void WrapPartitionedStreamHelper<TIgnoreKey, TKey>(PartitionedStream<Pair<TSource, TGroupKey>, TKey> hashStream, IPartitionedStreamRecipient<IGrouping<TGroupKey, TElement>> recipient, CancellationToken cancellationToken)
		{
			int partitionCount = hashStream.PartitionCount;
			PartitionedStream<IGrouping<TGroupKey, TElement>, TKey> partitionedStream = new PartitionedStream<IGrouping<TGroupKey, TElement>, TKey>(partitionCount, hashStream.KeyComparer, OrdinalIndexState.Shuffled);
			for (int i = 0; i < partitionCount; i++)
			{
				if (this._elementSelector == null)
				{
					GroupByIdentityQueryOperatorEnumerator<TSource, TGroupKey, TKey> groupByIdentityQueryOperatorEnumerator = new GroupByIdentityQueryOperatorEnumerator<TSource, TGroupKey, TKey>(hashStream[i], this._keyComparer, cancellationToken);
					partitionedStream[i] = (QueryOperatorEnumerator<IGrouping<TGroupKey, TElement>, TKey>)groupByIdentityQueryOperatorEnumerator;
				}
				else
				{
					partitionedStream[i] = new GroupByElementSelectorQueryOperatorEnumerator<TSource, TGroupKey, TElement, TKey>(hashStream[i], this._keyComparer, this._elementSelector, cancellationToken);
				}
			}
			recipient.Receive<TKey>(partitionedStream);
		}

		// Token: 0x06000BBA RID: 3002 RVA: 0x00026F3C File Offset: 0x0002513C
		private void WrapPartitionedStreamHelperOrdered<TKey>(PartitionedStream<Pair<TSource, TGroupKey>, TKey> hashStream, IPartitionedStreamRecipient<IGrouping<TGroupKey, TElement>> recipient, CancellationToken cancellationToken)
		{
			int partitionCount = hashStream.PartitionCount;
			PartitionedStream<IGrouping<TGroupKey, TElement>, TKey> partitionedStream = new PartitionedStream<IGrouping<TGroupKey, TElement>, TKey>(partitionCount, hashStream.KeyComparer, OrdinalIndexState.Shuffled);
			IComparer<TKey> keyComparer = hashStream.KeyComparer;
			for (int i = 0; i < partitionCount; i++)
			{
				if (this._elementSelector == null)
				{
					OrderedGroupByIdentityQueryOperatorEnumerator<TSource, TGroupKey, TKey> orderedGroupByIdentityQueryOperatorEnumerator = new OrderedGroupByIdentityQueryOperatorEnumerator<TSource, TGroupKey, TKey>(hashStream[i], this._keySelector, this._keyComparer, keyComparer, cancellationToken);
					partitionedStream[i] = (QueryOperatorEnumerator<IGrouping<TGroupKey, TElement>, TKey>)orderedGroupByIdentityQueryOperatorEnumerator;
				}
				else
				{
					partitionedStream[i] = new OrderedGroupByElementSelectorQueryOperatorEnumerator<TSource, TGroupKey, TElement, TKey>(hashStream[i], this._keySelector, this._elementSelector, this._keyComparer, keyComparer, cancellationToken);
				}
			}
			recipient.Receive<TKey>(partitionedStream);
		}

		// Token: 0x06000BBB RID: 3003 RVA: 0x00026FD3 File Offset: 0x000251D3
		internal override QueryResults<IGrouping<TGroupKey, TElement>> Open(QuerySettings settings, bool preferStriping)
		{
			return new UnaryQueryOperator<TSource, IGrouping<TGroupKey, TElement>>.UnaryQueryOperatorResults(base.Child.Open(settings, false), this, settings, false);
		}

		// Token: 0x06000BBC RID: 3004 RVA: 0x00026FEC File Offset: 0x000251EC
		internal override IEnumerable<IGrouping<TGroupKey, TElement>> AsSequentialQuery(CancellationToken token)
		{
			IEnumerable<TSource> enumerable = CancellableEnumerable.Wrap<TSource>(base.Child.AsSequentialQuery(token), token);
			if (this._elementSelector == null)
			{
				return (IEnumerable<IGrouping<TGroupKey, TElement>>)enumerable.GroupBy(this._keySelector, this._keyComparer);
			}
			return enumerable.GroupBy(this._keySelector, this._elementSelector, this._keyComparer);
		}

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x06000BBD RID: 3005 RVA: 0x00002285 File Offset: 0x00000485
		internal override bool LimitsParallelism
		{
			get
			{
				return false;
			}
		}

		// Token: 0x04000704 RID: 1796
		private readonly Func<TSource, TGroupKey> _keySelector;

		// Token: 0x04000705 RID: 1797
		private readonly Func<TSource, TElement> _elementSelector;

		// Token: 0x04000706 RID: 1798
		private readonly IEqualityComparer<TGroupKey> _keyComparer;
	}
}
