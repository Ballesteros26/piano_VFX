using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x0200014B RID: 331
	internal sealed class ZipQueryOperator<TLeftInput, TRightInput, TOutput> : QueryOperator<TOutput>
	{
		// Token: 0x06000A06 RID: 2566 RVA: 0x0002187A File Offset: 0x0001FA7A
		internal ZipQueryOperator(ParallelQuery<TLeftInput> leftChildSource, ParallelQuery<TRightInput> rightChildSource, Func<TLeftInput, TRightInput, TOutput> resultSelector)
			: this(QueryOperator<TLeftInput>.AsQueryOperator(leftChildSource), QueryOperator<TRightInput>.AsQueryOperator(rightChildSource), resultSelector)
		{
		}

		// Token: 0x06000A07 RID: 2567 RVA: 0x00021890 File Offset: 0x0001FA90
		private ZipQueryOperator(QueryOperator<TLeftInput> left, QueryOperator<TRightInput> right, Func<TLeftInput, TRightInput, TOutput> resultSelector)
			: base(left.SpecifiedQuerySettings.Merge(right.SpecifiedQuerySettings))
		{
			this._leftChild = left;
			this._rightChild = right;
			this._resultSelector = resultSelector;
			this._outputOrdered = this._leftChild.OutputOrdered || this._rightChild.OutputOrdered;
			OrdinalIndexState ordinalIndexState = this._leftChild.OrdinalIndexState;
			OrdinalIndexState ordinalIndexState2 = this._rightChild.OrdinalIndexState;
			this._prematureMergeLeft = ordinalIndexState > OrdinalIndexState.Indexable;
			this._prematureMergeRight = ordinalIndexState2 > OrdinalIndexState.Indexable;
			this._limitsParallelism = (this._prematureMergeLeft && ordinalIndexState != OrdinalIndexState.Shuffled) || (this._prematureMergeRight && ordinalIndexState2 != OrdinalIndexState.Shuffled);
		}

		// Token: 0x06000A08 RID: 2568 RVA: 0x00021940 File Offset: 0x0001FB40
		internal override QueryResults<TOutput> Open(QuerySettings settings, bool preferStriping)
		{
			QueryResults<TLeftInput> queryResults = this._leftChild.Open(settings, preferStriping);
			QueryResults<TRightInput> queryResults2 = this._rightChild.Open(settings, preferStriping);
			int value = settings.DegreeOfParallelism.Value;
			if (this._prematureMergeLeft)
			{
				PartitionedStreamMerger<TLeftInput> partitionedStreamMerger = new PartitionedStreamMerger<TLeftInput>(false, ParallelMergeOptions.FullyBuffered, settings.TaskScheduler, this._leftChild.OutputOrdered, settings.CancellationState, settings.QueryId);
				queryResults.GivePartitionedStream(partitionedStreamMerger);
				queryResults = new ListQueryResults<TLeftInput>(partitionedStreamMerger.MergeExecutor.GetResultsAsArray(), value, preferStriping);
			}
			if (this._prematureMergeRight)
			{
				PartitionedStreamMerger<TRightInput> partitionedStreamMerger2 = new PartitionedStreamMerger<TRightInput>(false, ParallelMergeOptions.FullyBuffered, settings.TaskScheduler, this._rightChild.OutputOrdered, settings.CancellationState, settings.QueryId);
				queryResults2.GivePartitionedStream(partitionedStreamMerger2);
				queryResults2 = new ListQueryResults<TRightInput>(partitionedStreamMerger2.MergeExecutor.GetResultsAsArray(), value, preferStriping);
			}
			return new ZipQueryOperator<TLeftInput, TRightInput, TOutput>.ZipQueryOperatorResults(queryResults, queryResults2, this._resultSelector, value, preferStriping);
		}

		// Token: 0x06000A09 RID: 2569 RVA: 0x00021A22 File Offset: 0x0001FC22
		internal override IEnumerable<TOutput> AsSequentialQuery(CancellationToken token)
		{
			using (IEnumerator<TLeftInput> leftEnumerator = this._leftChild.AsSequentialQuery(token).GetEnumerator())
			{
				using (IEnumerator<TRightInput> rightEnumerator = this._rightChild.AsSequentialQuery(token).GetEnumerator())
				{
					while (leftEnumerator.MoveNext() && rightEnumerator.MoveNext())
					{
						yield return this._resultSelector(leftEnumerator.Current, rightEnumerator.Current);
					}
				}
				IEnumerator<TRightInput> rightEnumerator = null;
			}
			IEnumerator<TLeftInput> leftEnumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x06000A0A RID: 2570 RVA: 0x00002285 File Offset: 0x00000485
		internal override OrdinalIndexState OrdinalIndexState
		{
			get
			{
				return OrdinalIndexState.Indexable;
			}
		}

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x06000A0B RID: 2571 RVA: 0x00021A39 File Offset: 0x0001FC39
		internal override bool LimitsParallelism
		{
			get
			{
				return this._limitsParallelism;
			}
		}

		// Token: 0x04000638 RID: 1592
		private readonly Func<TLeftInput, TRightInput, TOutput> _resultSelector;

		// Token: 0x04000639 RID: 1593
		private readonly QueryOperator<TLeftInput> _leftChild;

		// Token: 0x0400063A RID: 1594
		private readonly QueryOperator<TRightInput> _rightChild;

		// Token: 0x0400063B RID: 1595
		private readonly bool _prematureMergeLeft;

		// Token: 0x0400063C RID: 1596
		private readonly bool _prematureMergeRight;

		// Token: 0x0400063D RID: 1597
		private readonly bool _limitsParallelism;

		// Token: 0x0200014C RID: 332
		internal class ZipQueryOperatorResults : QueryResults<TOutput>
		{
			// Token: 0x06000A0C RID: 2572 RVA: 0x00021A44 File Offset: 0x0001FC44
			internal ZipQueryOperatorResults(QueryResults<TLeftInput> leftChildResults, QueryResults<TRightInput> rightChildResults, Func<TLeftInput, TRightInput, TOutput> resultSelector, int partitionCount, bool preferStriping)
			{
				this._leftChildResults = leftChildResults;
				this._rightChildResults = rightChildResults;
				this._resultSelector = resultSelector;
				this._partitionCount = partitionCount;
				this._preferStriping = preferStriping;
				this._count = Math.Min(this._leftChildResults.Count, this._rightChildResults.Count);
			}

			// Token: 0x17000146 RID: 326
			// (get) Token: 0x06000A0D RID: 2573 RVA: 0x00021A9D File Offset: 0x0001FC9D
			internal override int ElementsCount
			{
				get
				{
					return this._count;
				}
			}

			// Token: 0x17000147 RID: 327
			// (get) Token: 0x06000A0E RID: 2574 RVA: 0x0000AA13 File Offset: 0x00008C13
			internal override bool IsIndexible
			{
				get
				{
					return true;
				}
			}

			// Token: 0x06000A0F RID: 2575 RVA: 0x00021AA5 File Offset: 0x0001FCA5
			internal override TOutput GetElement(int index)
			{
				return this._resultSelector(this._leftChildResults.GetElement(index), this._rightChildResults.GetElement(index));
			}

			// Token: 0x06000A10 RID: 2576 RVA: 0x00021ACC File Offset: 0x0001FCCC
			internal override void GivePartitionedStream(IPartitionedStreamRecipient<TOutput> recipient)
			{
				PartitionedStream<TOutput, int> partitionedStream = ExchangeUtilities.PartitionDataSource<TOutput>(this, this._partitionCount, this._preferStriping);
				recipient.Receive<int>(partitionedStream);
			}

			// Token: 0x0400063E RID: 1598
			private readonly QueryResults<TLeftInput> _leftChildResults;

			// Token: 0x0400063F RID: 1599
			private readonly QueryResults<TRightInput> _rightChildResults;

			// Token: 0x04000640 RID: 1600
			private readonly Func<TLeftInput, TRightInput, TOutput> _resultSelector;

			// Token: 0x04000641 RID: 1601
			private readonly int _count;

			// Token: 0x04000642 RID: 1602
			private readonly int _partitionCount;

			// Token: 0x04000643 RID: 1603
			private readonly bool _preferStriping;
		}
	}
}
