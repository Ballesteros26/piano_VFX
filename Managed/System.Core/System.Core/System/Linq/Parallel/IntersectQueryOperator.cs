using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x02000144 RID: 324
	internal sealed class IntersectQueryOperator<TInputOutput> : BinaryQueryOperator<TInputOutput, TInputOutput, TInputOutput>
	{
		// Token: 0x060009E7 RID: 2535 RVA: 0x00020C35 File Offset: 0x0001EE35
		internal IntersectQueryOperator(ParallelQuery<TInputOutput> left, ParallelQuery<TInputOutput> right, IEqualityComparer<TInputOutput> comparer)
			: base(left, right)
		{
			this._comparer = comparer;
			this._outputOrdered = base.LeftChild.OutputOrdered;
			base.SetOrdinalIndex(OrdinalIndexState.Shuffled);
		}

		// Token: 0x060009E8 RID: 2536 RVA: 0x00020C60 File Offset: 0x0001EE60
		internal override QueryResults<TInputOutput> Open(QuerySettings settings, bool preferStriping)
		{
			QueryResults<TInputOutput> queryResults = base.LeftChild.Open(settings, false);
			QueryResults<TInputOutput> queryResults2 = base.RightChild.Open(settings, false);
			return new BinaryQueryOperator<TInputOutput, TInputOutput, TInputOutput>.BinaryQueryOperatorResults(queryResults, queryResults2, this, settings, false);
		}

		// Token: 0x060009E9 RID: 2537 RVA: 0x00020C94 File Offset: 0x0001EE94
		public override void WrapPartitionedStream<TLeftKey, TRightKey>(PartitionedStream<TInputOutput, TLeftKey> leftPartitionedStream, PartitionedStream<TInputOutput, TRightKey> rightPartitionedStream, IPartitionedStreamRecipient<TInputOutput> outputRecipient, bool preferStriping, QuerySettings settings)
		{
			if (base.OutputOrdered)
			{
				this.WrapPartitionedStreamHelper<TLeftKey, TRightKey>(ExchangeUtilities.HashRepartitionOrdered<TInputOutput, NoKeyMemoizationRequired, TLeftKey>(leftPartitionedStream, null, null, this._comparer, settings.CancellationState.MergedCancellationToken), rightPartitionedStream, outputRecipient, settings.CancellationState.MergedCancellationToken);
				return;
			}
			this.WrapPartitionedStreamHelper<int, TRightKey>(ExchangeUtilities.HashRepartition<TInputOutput, NoKeyMemoizationRequired, TLeftKey>(leftPartitionedStream, null, null, this._comparer, settings.CancellationState.MergedCancellationToken), rightPartitionedStream, outputRecipient, settings.CancellationState.MergedCancellationToken);
		}

		// Token: 0x060009EA RID: 2538 RVA: 0x00020D08 File Offset: 0x0001EF08
		private void WrapPartitionedStreamHelper<TLeftKey, TRightKey>(PartitionedStream<Pair<TInputOutput, NoKeyMemoizationRequired>, TLeftKey> leftHashStream, PartitionedStream<TInputOutput, TRightKey> rightPartitionedStream, IPartitionedStreamRecipient<TInputOutput> outputRecipient, CancellationToken cancellationToken)
		{
			int partitionCount = leftHashStream.PartitionCount;
			PartitionedStream<Pair<TInputOutput, NoKeyMemoizationRequired>, int> partitionedStream = ExchangeUtilities.HashRepartition<TInputOutput, NoKeyMemoizationRequired, TRightKey>(rightPartitionedStream, null, null, this._comparer, cancellationToken);
			PartitionedStream<TInputOutput, TLeftKey> partitionedStream2 = new PartitionedStream<TInputOutput, TLeftKey>(partitionCount, leftHashStream.KeyComparer, OrdinalIndexState.Shuffled);
			for (int i = 0; i < partitionCount; i++)
			{
				if (base.OutputOrdered)
				{
					partitionedStream2[i] = new IntersectQueryOperator<TInputOutput>.OrderedIntersectQueryOperatorEnumerator<TLeftKey>(leftHashStream[i], partitionedStream[i], this._comparer, leftHashStream.KeyComparer, cancellationToken);
				}
				else
				{
					partitionedStream2[i] = (QueryOperatorEnumerator<TInputOutput, TLeftKey>)new IntersectQueryOperator<TInputOutput>.IntersectQueryOperatorEnumerator<TLeftKey>(leftHashStream[i], partitionedStream[i], this._comparer, cancellationToken);
				}
			}
			outputRecipient.Receive<TLeftKey>(partitionedStream2);
		}

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x060009EB RID: 2539 RVA: 0x00002285 File Offset: 0x00000485
		internal override bool LimitsParallelism
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060009EC RID: 2540 RVA: 0x00020DA8 File Offset: 0x0001EFA8
		internal override IEnumerable<TInputOutput> AsSequentialQuery(CancellationToken token)
		{
			IEnumerable<TInputOutput> enumerable = CancellableEnumerable.Wrap<TInputOutput>(base.LeftChild.AsSequentialQuery(token), token);
			IEnumerable<TInputOutput> enumerable2 = CancellableEnumerable.Wrap<TInputOutput>(base.RightChild.AsSequentialQuery(token), token);
			return enumerable.Intersect(enumerable2, this._comparer);
		}

		// Token: 0x04000618 RID: 1560
		private readonly IEqualityComparer<TInputOutput> _comparer;

		// Token: 0x02000145 RID: 325
		private class IntersectQueryOperatorEnumerator<TLeftKey> : QueryOperatorEnumerator<TInputOutput, int>
		{
			// Token: 0x060009ED RID: 2541 RVA: 0x00020DE6 File Offset: 0x0001EFE6
			internal IntersectQueryOperatorEnumerator(QueryOperatorEnumerator<Pair<TInputOutput, NoKeyMemoizationRequired>, TLeftKey> leftSource, QueryOperatorEnumerator<Pair<TInputOutput, NoKeyMemoizationRequired>, int> rightSource, IEqualityComparer<TInputOutput> comparer, CancellationToken cancellationToken)
			{
				this._leftSource = leftSource;
				this._rightSource = rightSource;
				this._comparer = comparer;
				this._cancellationToken = cancellationToken;
			}

			// Token: 0x060009EE RID: 2542 RVA: 0x00020E0C File Offset: 0x0001F00C
			internal override bool MoveNext(ref TInputOutput currentElement, ref int currentKey)
			{
				if (this._hashLookup == null)
				{
					this._outputLoopCount = new Shared<int>(0);
					this._hashLookup = new Set<TInputOutput>(this._comparer);
					Pair<TInputOutput, NoKeyMemoizationRequired> pair = default(Pair<TInputOutput, NoKeyMemoizationRequired>);
					int num = 0;
					int num2 = 0;
					while (this._rightSource.MoveNext(ref pair, ref num))
					{
						if ((num2++ & 63) == 0)
						{
							CancellationState.ThrowIfCanceled(this._cancellationToken);
						}
						this._hashLookup.Add(pair.First);
					}
				}
				Pair<TInputOutput, NoKeyMemoizationRequired> pair2 = default(Pair<TInputOutput, NoKeyMemoizationRequired>);
				TLeftKey tleftKey = default(TLeftKey);
				while (this._leftSource.MoveNext(ref pair2, ref tleftKey))
				{
					Shared<int> outputLoopCount = this._outputLoopCount;
					int value = outputLoopCount.Value;
					outputLoopCount.Value = value + 1;
					if ((value & 63) == 0)
					{
						CancellationState.ThrowIfCanceled(this._cancellationToken);
					}
					if (this._hashLookup.Remove(pair2.First))
					{
						currentElement = pair2.First;
						return true;
					}
				}
				return false;
			}

			// Token: 0x060009EF RID: 2543 RVA: 0x00020EF8 File Offset: 0x0001F0F8
			protected override void Dispose(bool disposing)
			{
				this._leftSource.Dispose();
				this._rightSource.Dispose();
			}

			// Token: 0x04000619 RID: 1561
			private QueryOperatorEnumerator<Pair<TInputOutput, NoKeyMemoizationRequired>, TLeftKey> _leftSource;

			// Token: 0x0400061A RID: 1562
			private QueryOperatorEnumerator<Pair<TInputOutput, NoKeyMemoizationRequired>, int> _rightSource;

			// Token: 0x0400061B RID: 1563
			private IEqualityComparer<TInputOutput> _comparer;

			// Token: 0x0400061C RID: 1564
			private Set<TInputOutput> _hashLookup;

			// Token: 0x0400061D RID: 1565
			private CancellationToken _cancellationToken;

			// Token: 0x0400061E RID: 1566
			private Shared<int> _outputLoopCount;
		}

		// Token: 0x02000146 RID: 326
		private class OrderedIntersectQueryOperatorEnumerator<TLeftKey> : QueryOperatorEnumerator<TInputOutput, TLeftKey>
		{
			// Token: 0x060009F0 RID: 2544 RVA: 0x00020F10 File Offset: 0x0001F110
			internal OrderedIntersectQueryOperatorEnumerator(QueryOperatorEnumerator<Pair<TInputOutput, NoKeyMemoizationRequired>, TLeftKey> leftSource, QueryOperatorEnumerator<Pair<TInputOutput, NoKeyMemoizationRequired>, int> rightSource, IEqualityComparer<TInputOutput> comparer, IComparer<TLeftKey> leftKeyComparer, CancellationToken cancellationToken)
			{
				this._leftSource = leftSource;
				this._rightSource = rightSource;
				this._comparer = new WrapperEqualityComparer<TInputOutput>(comparer);
				this._leftKeyComparer = leftKeyComparer;
				this._cancellationToken = cancellationToken;
			}

			// Token: 0x060009F1 RID: 2545 RVA: 0x00020F48 File Offset: 0x0001F148
			internal override bool MoveNext(ref TInputOutput currentElement, ref TLeftKey currentKey)
			{
				int num = 0;
				if (this._hashLookup == null)
				{
					this._hashLookup = new Dictionary<Wrapper<TInputOutput>, Pair<TInputOutput, TLeftKey>>(this._comparer);
					Pair<TInputOutput, NoKeyMemoizationRequired> pair = default(Pair<TInputOutput, NoKeyMemoizationRequired>);
					TLeftKey tleftKey = default(TLeftKey);
					while (this._leftSource.MoveNext(ref pair, ref tleftKey))
					{
						if ((num++ & 63) == 0)
						{
							CancellationState.ThrowIfCanceled(this._cancellationToken);
						}
						Wrapper<TInputOutput> wrapper = new Wrapper<TInputOutput>(pair.First);
						Pair<TInputOutput, TLeftKey> pair2;
						if (!this._hashLookup.TryGetValue(wrapper, out pair2) || this._leftKeyComparer.Compare(tleftKey, pair2.Second) < 0)
						{
							this._hashLookup[wrapper] = new Pair<TInputOutput, TLeftKey>(pair.First, tleftKey);
						}
					}
				}
				Pair<TInputOutput, NoKeyMemoizationRequired> pair3 = default(Pair<TInputOutput, NoKeyMemoizationRequired>);
				int num2 = 0;
				while (this._rightSource.MoveNext(ref pair3, ref num2))
				{
					if ((num++ & 63) == 0)
					{
						CancellationState.ThrowIfCanceled(this._cancellationToken);
					}
					Wrapper<TInputOutput> wrapper2 = new Wrapper<TInputOutput>(pair3.First);
					Pair<TInputOutput, TLeftKey> pair4;
					if (this._hashLookup.TryGetValue(wrapper2, out pair4))
					{
						currentElement = pair4.First;
						currentKey = pair4.Second;
						this._hashLookup.Remove(new Wrapper<TInputOutput>(pair4.First));
						return true;
					}
				}
				return false;
			}

			// Token: 0x060009F2 RID: 2546 RVA: 0x00021082 File Offset: 0x0001F282
			protected override void Dispose(bool disposing)
			{
				this._leftSource.Dispose();
				this._rightSource.Dispose();
			}

			// Token: 0x0400061F RID: 1567
			private QueryOperatorEnumerator<Pair<TInputOutput, NoKeyMemoizationRequired>, TLeftKey> _leftSource;

			// Token: 0x04000620 RID: 1568
			private QueryOperatorEnumerator<Pair<TInputOutput, NoKeyMemoizationRequired>, int> _rightSource;

			// Token: 0x04000621 RID: 1569
			private IEqualityComparer<Wrapper<TInputOutput>> _comparer;

			// Token: 0x04000622 RID: 1570
			private IComparer<TLeftKey> _leftKeyComparer;

			// Token: 0x04000623 RID: 1571
			private Dictionary<Wrapper<TInputOutput>, Pair<TInputOutput, TLeftKey>> _hashLookup;

			// Token: 0x04000624 RID: 1572
			private CancellationToken _cancellationToken;
		}
	}
}
