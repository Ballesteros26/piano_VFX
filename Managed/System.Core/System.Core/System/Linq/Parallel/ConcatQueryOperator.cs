using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x02000139 RID: 313
	internal sealed class ConcatQueryOperator<TSource> : BinaryQueryOperator<TSource, TSource, TSource>
	{
		// Token: 0x060009BC RID: 2492 RVA: 0x0001FEDC File Offset: 0x0001E0DC
		internal ConcatQueryOperator(ParallelQuery<TSource> firstChild, ParallelQuery<TSource> secondChild)
			: base(firstChild, secondChild)
		{
			this._outputOrdered = base.LeftChild.OutputOrdered || base.RightChild.OutputOrdered;
			this._prematureMergeLeft = base.LeftChild.OrdinalIndexState.IsWorseThan(OrdinalIndexState.Increasing);
			this._prematureMergeRight = base.RightChild.OrdinalIndexState.IsWorseThan(OrdinalIndexState.Increasing);
			if (base.LeftChild.OrdinalIndexState == OrdinalIndexState.Indexable && base.RightChild.OrdinalIndexState == OrdinalIndexState.Indexable)
			{
				base.SetOrdinalIndex(OrdinalIndexState.Indexable);
				return;
			}
			base.SetOrdinalIndex(OrdinalIndexState.Increasing.Worse(base.LeftChild.OrdinalIndexState.Worse(base.RightChild.OrdinalIndexState)));
		}

		// Token: 0x060009BD RID: 2493 RVA: 0x0001FF8C File Offset: 0x0001E18C
		internal override QueryResults<TSource> Open(QuerySettings settings, bool preferStriping)
		{
			QueryResults<TSource> queryResults = base.LeftChild.Open(settings, preferStriping);
			QueryResults<TSource> queryResults2 = base.RightChild.Open(settings, preferStriping);
			return ConcatQueryOperator<TSource>.ConcatQueryOperatorResults.NewResults(queryResults, queryResults2, this, settings, preferStriping);
		}

		// Token: 0x060009BE RID: 2494 RVA: 0x0001FFC0 File Offset: 0x0001E1C0
		public override void WrapPartitionedStream<TLeftKey, TRightKey>(PartitionedStream<TSource, TLeftKey> leftStream, PartitionedStream<TSource, TRightKey> rightStream, IPartitionedStreamRecipient<TSource> outputRecipient, bool preferStriping, QuerySettings settings)
		{
			if (this._prematureMergeLeft)
			{
				PartitionedStream<TSource, int> partitionedStream = QueryOperator<TSource>.ExecuteAndCollectResults<TLeftKey>(leftStream, leftStream.PartitionCount, base.LeftChild.OutputOrdered, preferStriping, settings).GetPartitionedStream();
				this.WrapHelper<int, TRightKey>(partitionedStream, rightStream, outputRecipient, settings, preferStriping);
				return;
			}
			this.WrapHelper<TLeftKey, TRightKey>(leftStream, rightStream, outputRecipient, settings, preferStriping);
		}

		// Token: 0x060009BF RID: 2495 RVA: 0x00020014 File Offset: 0x0001E214
		private void WrapHelper<TLeftKey, TRightKey>(PartitionedStream<TSource, TLeftKey> leftStreamInc, PartitionedStream<TSource, TRightKey> rightStream, IPartitionedStreamRecipient<TSource> outputRecipient, QuerySettings settings, bool preferStriping)
		{
			if (this._prematureMergeRight)
			{
				PartitionedStream<TSource, int> partitionedStream = QueryOperator<TSource>.ExecuteAndCollectResults<TRightKey>(rightStream, leftStreamInc.PartitionCount, base.LeftChild.OutputOrdered, preferStriping, settings).GetPartitionedStream();
				this.WrapHelper2<TLeftKey, int>(leftStreamInc, partitionedStream, outputRecipient);
				return;
			}
			this.WrapHelper2<TLeftKey, TRightKey>(leftStreamInc, rightStream, outputRecipient);
		}

		// Token: 0x060009C0 RID: 2496 RVA: 0x00020060 File Offset: 0x0001E260
		private void WrapHelper2<TLeftKey, TRightKey>(PartitionedStream<TSource, TLeftKey> leftStreamInc, PartitionedStream<TSource, TRightKey> rightStreamInc, IPartitionedStreamRecipient<TSource> outputRecipient)
		{
			int partitionCount = leftStreamInc.PartitionCount;
			IComparer<ConcatKey<TLeftKey, TRightKey>> comparer = ConcatKey<TLeftKey, TRightKey>.MakeComparer(leftStreamInc.KeyComparer, rightStreamInc.KeyComparer);
			PartitionedStream<TSource, ConcatKey<TLeftKey, TRightKey>> partitionedStream = new PartitionedStream<TSource, ConcatKey<TLeftKey, TRightKey>>(partitionCount, comparer, this.OrdinalIndexState);
			for (int i = 0; i < partitionCount; i++)
			{
				partitionedStream[i] = new ConcatQueryOperator<TSource>.ConcatQueryOperatorEnumerator<TLeftKey, TRightKey>(leftStreamInc[i], rightStreamInc[i]);
			}
			outputRecipient.Receive<ConcatKey<TLeftKey, TRightKey>>(partitionedStream);
		}

		// Token: 0x060009C1 RID: 2497 RVA: 0x000200C1 File Offset: 0x0001E2C1
		internal override IEnumerable<TSource> AsSequentialQuery(CancellationToken token)
		{
			return base.LeftChild.AsSequentialQuery(token).Concat(base.RightChild.AsSequentialQuery(token));
		}

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x060009C2 RID: 2498 RVA: 0x00002285 File Offset: 0x00000485
		internal override bool LimitsParallelism
		{
			get
			{
				return false;
			}
		}

		// Token: 0x040005EE RID: 1518
		private readonly bool _prematureMergeLeft;

		// Token: 0x040005EF RID: 1519
		private readonly bool _prematureMergeRight;

		// Token: 0x0200013A RID: 314
		private sealed class ConcatQueryOperatorEnumerator<TLeftKey, TRightKey> : QueryOperatorEnumerator<TSource, ConcatKey<TLeftKey, TRightKey>>
		{
			// Token: 0x060009C3 RID: 2499 RVA: 0x000200E0 File Offset: 0x0001E2E0
			internal ConcatQueryOperatorEnumerator(QueryOperatorEnumerator<TSource, TLeftKey> firstSource, QueryOperatorEnumerator<TSource, TRightKey> secondSource)
			{
				this._firstSource = firstSource;
				this._secondSource = secondSource;
			}

			// Token: 0x060009C4 RID: 2500 RVA: 0x000200F8 File Offset: 0x0001E2F8
			internal override bool MoveNext(ref TSource currentElement, ref ConcatKey<TLeftKey, TRightKey> currentKey)
			{
				if (!this._begunSecond)
				{
					TLeftKey tleftKey = default(TLeftKey);
					if (this._firstSource.MoveNext(ref currentElement, ref tleftKey))
					{
						currentKey = ConcatKey<TLeftKey, TRightKey>.MakeLeft(tleftKey);
						return true;
					}
					this._begunSecond = true;
				}
				TRightKey trightKey = default(TRightKey);
				if (this._secondSource.MoveNext(ref currentElement, ref trightKey))
				{
					currentKey = ConcatKey<TLeftKey, TRightKey>.MakeRight(trightKey);
					return true;
				}
				return false;
			}

			// Token: 0x060009C5 RID: 2501 RVA: 0x00020161 File Offset: 0x0001E361
			protected override void Dispose(bool disposing)
			{
				this._firstSource.Dispose();
				this._secondSource.Dispose();
			}

			// Token: 0x040005F0 RID: 1520
			private QueryOperatorEnumerator<TSource, TLeftKey> _firstSource;

			// Token: 0x040005F1 RID: 1521
			private QueryOperatorEnumerator<TSource, TRightKey> _secondSource;

			// Token: 0x040005F2 RID: 1522
			private bool _begunSecond;
		}

		// Token: 0x0200013B RID: 315
		private class ConcatQueryOperatorResults : BinaryQueryOperator<TSource, TSource, TSource>.BinaryQueryOperatorResults
		{
			// Token: 0x060009C6 RID: 2502 RVA: 0x00020179 File Offset: 0x0001E379
			public static QueryResults<TSource> NewResults(QueryResults<TSource> leftChildQueryResults, QueryResults<TSource> rightChildQueryResults, ConcatQueryOperator<TSource> op, QuerySettings settings, bool preferStriping)
			{
				if (leftChildQueryResults.IsIndexible && rightChildQueryResults.IsIndexible)
				{
					return new ConcatQueryOperator<TSource>.ConcatQueryOperatorResults(leftChildQueryResults, rightChildQueryResults, op, settings, preferStriping);
				}
				return new BinaryQueryOperator<TSource, TSource, TSource>.BinaryQueryOperatorResults(leftChildQueryResults, rightChildQueryResults, op, settings, preferStriping);
			}

			// Token: 0x060009C7 RID: 2503 RVA: 0x000201A2 File Offset: 0x0001E3A2
			private ConcatQueryOperatorResults(QueryResults<TSource> leftChildQueryResults, QueryResults<TSource> rightChildQueryResults, ConcatQueryOperator<TSource> concatOp, QuerySettings settings, bool preferStriping)
				: base(leftChildQueryResults, rightChildQueryResults, concatOp, settings, preferStriping)
			{
				this._leftChildCount = leftChildQueryResults.ElementsCount;
				this._rightChildCount = rightChildQueryResults.ElementsCount;
			}

			// Token: 0x1700013D RID: 317
			// (get) Token: 0x060009C8 RID: 2504 RVA: 0x0000AA13 File Offset: 0x00008C13
			internal override bool IsIndexible
			{
				get
				{
					return true;
				}
			}

			// Token: 0x1700013E RID: 318
			// (get) Token: 0x060009C9 RID: 2505 RVA: 0x000201C9 File Offset: 0x0001E3C9
			internal override int ElementsCount
			{
				get
				{
					return this._leftChildCount + this._rightChildCount;
				}
			}

			// Token: 0x060009CA RID: 2506 RVA: 0x000201D8 File Offset: 0x0001E3D8
			internal override TSource GetElement(int index)
			{
				if (index < this._leftChildCount)
				{
					return this._leftChildQueryResults.GetElement(index);
				}
				return this._rightChildQueryResults.GetElement(index - this._leftChildCount);
			}

			// Token: 0x040005F3 RID: 1523
			private int _leftChildCount;

			// Token: 0x040005F4 RID: 1524
			private int _rightChildCount;
		}
	}
}
