using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x02000141 RID: 321
	internal sealed class GroupJoinQueryOperator<TLeftInput, TRightInput, TKey, TOutput> : BinaryQueryOperator<TLeftInput, TRightInput, TOutput>
	{
		// Token: 0x060009DD RID: 2525 RVA: 0x00020758 File Offset: 0x0001E958
		internal GroupJoinQueryOperator(ParallelQuery<TLeftInput> left, ParallelQuery<TRightInput> right, Func<TLeftInput, TKey> leftKeySelector, Func<TRightInput, TKey> rightKeySelector, Func<TLeftInput, IEnumerable<TRightInput>, TOutput> resultSelector, IEqualityComparer<TKey> keyComparer)
			: base(left, right)
		{
			this._leftKeySelector = leftKeySelector;
			this._rightKeySelector = rightKeySelector;
			this._resultSelector = resultSelector;
			this._keyComparer = keyComparer;
			this._outputOrdered = base.LeftChild.OutputOrdered;
			base.SetOrdinalIndex(OrdinalIndexState.Shuffled);
		}

		// Token: 0x060009DE RID: 2526 RVA: 0x000207A4 File Offset: 0x0001E9A4
		internal override QueryResults<TOutput> Open(QuerySettings settings, bool preferStriping)
		{
			QueryResults<TLeftInput> queryResults = base.LeftChild.Open(settings, false);
			QueryResults<TRightInput> queryResults2 = base.RightChild.Open(settings, false);
			return new BinaryQueryOperator<TLeftInput, TRightInput, TOutput>.BinaryQueryOperatorResults(queryResults, queryResults2, this, settings, false);
		}

		// Token: 0x060009DF RID: 2527 RVA: 0x000207D8 File Offset: 0x0001E9D8
		public override void WrapPartitionedStream<TLeftKey, TRightKey>(PartitionedStream<TLeftInput, TLeftKey> leftStream, PartitionedStream<TRightInput, TRightKey> rightStream, IPartitionedStreamRecipient<TOutput> outputRecipient, bool preferStriping, QuerySettings settings)
		{
			int partitionCount = leftStream.PartitionCount;
			if (base.LeftChild.OutputOrdered)
			{
				this.WrapPartitionedStreamHelper<TLeftKey, TRightKey>(ExchangeUtilities.HashRepartitionOrdered<TLeftInput, TKey, TLeftKey>(leftStream, this._leftKeySelector, this._keyComparer, null, settings.CancellationState.MergedCancellationToken), rightStream, outputRecipient, partitionCount, settings.CancellationState.MergedCancellationToken);
				return;
			}
			this.WrapPartitionedStreamHelper<int, TRightKey>(ExchangeUtilities.HashRepartition<TLeftInput, TKey, TLeftKey>(leftStream, this._leftKeySelector, this._keyComparer, null, settings.CancellationState.MergedCancellationToken), rightStream, outputRecipient, partitionCount, settings.CancellationState.MergedCancellationToken);
		}

		// Token: 0x060009E0 RID: 2528 RVA: 0x00020864 File Offset: 0x0001EA64
		private void WrapPartitionedStreamHelper<TLeftKey, TRightKey>(PartitionedStream<Pair<TLeftInput, TKey>, TLeftKey> leftHashStream, PartitionedStream<TRightInput, TRightKey> rightPartitionedStream, IPartitionedStreamRecipient<TOutput> outputRecipient, int partitionCount, CancellationToken cancellationToken)
		{
			PartitionedStream<Pair<TRightInput, TKey>, int> partitionedStream = ExchangeUtilities.HashRepartition<TRightInput, TKey, TRightKey>(rightPartitionedStream, this._rightKeySelector, this._keyComparer, null, cancellationToken);
			PartitionedStream<TOutput, TLeftKey> partitionedStream2 = new PartitionedStream<TOutput, TLeftKey>(partitionCount, leftHashStream.KeyComparer, this.OrdinalIndexState);
			for (int i = 0; i < partitionCount; i++)
			{
				partitionedStream2[i] = new HashJoinQueryOperatorEnumerator<TLeftInput, TLeftKey, TRightInput, TKey, TOutput>(leftHashStream[i], partitionedStream[i], null, this._resultSelector, this._keyComparer, cancellationToken);
			}
			outputRecipient.Receive<TLeftKey>(partitionedStream2);
		}

		// Token: 0x060009E1 RID: 2529 RVA: 0x000208D8 File Offset: 0x0001EAD8
		internal override IEnumerable<TOutput> AsSequentialQuery(CancellationToken token)
		{
			IEnumerable<TLeftInput> enumerable = CancellableEnumerable.Wrap<TLeftInput>(base.LeftChild.AsSequentialQuery(token), token);
			IEnumerable<TRightInput> enumerable2 = CancellableEnumerable.Wrap<TRightInput>(base.RightChild.AsSequentialQuery(token), token);
			return enumerable.GroupJoin(enumerable2, this._leftKeySelector, this._rightKeySelector, this._resultSelector, this._keyComparer);
		}

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x060009E2 RID: 2530 RVA: 0x00002285 File Offset: 0x00000485
		internal override bool LimitsParallelism
		{
			get
			{
				return false;
			}
		}

		// Token: 0x04000607 RID: 1543
		private readonly Func<TLeftInput, TKey> _leftKeySelector;

		// Token: 0x04000608 RID: 1544
		private readonly Func<TRightInput, TKey> _rightKeySelector;

		// Token: 0x04000609 RID: 1545
		private readonly Func<TLeftInput, IEnumerable<TRightInput>, TOutput> _resultSelector;

		// Token: 0x0400060A RID: 1546
		private readonly IEqualityComparer<TKey> _keyComparer;
	}
}
