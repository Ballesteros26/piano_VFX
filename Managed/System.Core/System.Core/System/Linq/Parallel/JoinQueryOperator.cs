using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x02000147 RID: 327
	internal sealed class JoinQueryOperator<TLeftInput, TRightInput, TKey, TOutput> : BinaryQueryOperator<TLeftInput, TRightInput, TOutput>
	{
		// Token: 0x060009F3 RID: 2547 RVA: 0x0002109C File Offset: 0x0001F29C
		internal JoinQueryOperator(ParallelQuery<TLeftInput> left, ParallelQuery<TRightInput> right, Func<TLeftInput, TKey> leftKeySelector, Func<TRightInput, TKey> rightKeySelector, Func<TLeftInput, TRightInput, TOutput> resultSelector, IEqualityComparer<TKey> keyComparer)
			: base(left, right)
		{
			this._leftKeySelector = leftKeySelector;
			this._rightKeySelector = rightKeySelector;
			this._resultSelector = resultSelector;
			this._keyComparer = keyComparer;
			this._outputOrdered = base.LeftChild.OutputOrdered;
			base.SetOrdinalIndex(OrdinalIndexState.Shuffled);
		}

		// Token: 0x060009F4 RID: 2548 RVA: 0x000210E8 File Offset: 0x0001F2E8
		public override void WrapPartitionedStream<TLeftKey, TRightKey>(PartitionedStream<TLeftInput, TLeftKey> leftStream, PartitionedStream<TRightInput, TRightKey> rightStream, IPartitionedStreamRecipient<TOutput> outputRecipient, bool preferStriping, QuerySettings settings)
		{
			if (base.LeftChild.OutputOrdered)
			{
				this.WrapPartitionedStreamHelper<TLeftKey, TRightKey>(ExchangeUtilities.HashRepartitionOrdered<TLeftInput, TKey, TLeftKey>(leftStream, this._leftKeySelector, this._keyComparer, null, settings.CancellationState.MergedCancellationToken), rightStream, outputRecipient, settings.CancellationState.MergedCancellationToken);
				return;
			}
			this.WrapPartitionedStreamHelper<int, TRightKey>(ExchangeUtilities.HashRepartition<TLeftInput, TKey, TLeftKey>(leftStream, this._leftKeySelector, this._keyComparer, null, settings.CancellationState.MergedCancellationToken), rightStream, outputRecipient, settings.CancellationState.MergedCancellationToken);
		}

		// Token: 0x060009F5 RID: 2549 RVA: 0x0002116C File Offset: 0x0001F36C
		private void WrapPartitionedStreamHelper<TLeftKey, TRightKey>(PartitionedStream<Pair<TLeftInput, TKey>, TLeftKey> leftHashStream, PartitionedStream<TRightInput, TRightKey> rightPartitionedStream, IPartitionedStreamRecipient<TOutput> outputRecipient, CancellationToken cancellationToken)
		{
			int partitionCount = leftHashStream.PartitionCount;
			PartitionedStream<Pair<TRightInput, TKey>, int> partitionedStream = ExchangeUtilities.HashRepartition<TRightInput, TKey, TRightKey>(rightPartitionedStream, this._rightKeySelector, this._keyComparer, null, cancellationToken);
			PartitionedStream<TOutput, TLeftKey> partitionedStream2 = new PartitionedStream<TOutput, TLeftKey>(partitionCount, leftHashStream.KeyComparer, this.OrdinalIndexState);
			for (int i = 0; i < partitionCount; i++)
			{
				partitionedStream2[i] = new HashJoinQueryOperatorEnumerator<TLeftInput, TLeftKey, TRightInput, TKey, TOutput>(leftHashStream[i], partitionedStream[i], this._resultSelector, null, this._keyComparer, cancellationToken);
			}
			outputRecipient.Receive<TLeftKey>(partitionedStream2);
		}

		// Token: 0x060009F6 RID: 2550 RVA: 0x000211E8 File Offset: 0x0001F3E8
		internal override QueryResults<TOutput> Open(QuerySettings settings, bool preferStriping)
		{
			QueryResults<TLeftInput> queryResults = base.LeftChild.Open(settings, false);
			QueryResults<TRightInput> queryResults2 = base.RightChild.Open(settings, false);
			return new BinaryQueryOperator<TLeftInput, TRightInput, TOutput>.BinaryQueryOperatorResults(queryResults, queryResults2, this, settings, false);
		}

		// Token: 0x060009F7 RID: 2551 RVA: 0x0002121C File Offset: 0x0001F41C
		internal override IEnumerable<TOutput> AsSequentialQuery(CancellationToken token)
		{
			IEnumerable<TLeftInput> enumerable = CancellableEnumerable.Wrap<TLeftInput>(base.LeftChild.AsSequentialQuery(token), token);
			IEnumerable<TRightInput> enumerable2 = CancellableEnumerable.Wrap<TRightInput>(base.RightChild.AsSequentialQuery(token), token);
			return enumerable.Join(enumerable2, this._leftKeySelector, this._rightKeySelector, this._resultSelector, this._keyComparer);
		}

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x060009F8 RID: 2552 RVA: 0x00002285 File Offset: 0x00000485
		internal override bool LimitsParallelism
		{
			get
			{
				return false;
			}
		}

		// Token: 0x04000625 RID: 1573
		private readonly Func<TLeftInput, TKey> _leftKeySelector;

		// Token: 0x04000626 RID: 1574
		private readonly Func<TRightInput, TKey> _rightKeySelector;

		// Token: 0x04000627 RID: 1575
		private readonly Func<TLeftInput, TRightInput, TOutput> _resultSelector;

		// Token: 0x04000628 RID: 1576
		private readonly IEqualityComparer<TKey> _keyComparer;
	}
}
