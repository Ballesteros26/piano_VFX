using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x0200013E RID: 318
	internal sealed class ExceptQueryOperator<TInputOutput> : BinaryQueryOperator<TInputOutput, TInputOutput, TInputOutput>
	{
		// Token: 0x060009D1 RID: 2513 RVA: 0x000202D6 File Offset: 0x0001E4D6
		internal ExceptQueryOperator(ParallelQuery<TInputOutput> left, ParallelQuery<TInputOutput> right, IEqualityComparer<TInputOutput> comparer)
			: base(left, right)
		{
			this._comparer = comparer;
			this._outputOrdered = base.LeftChild.OutputOrdered;
			base.SetOrdinalIndex(OrdinalIndexState.Shuffled);
		}

		// Token: 0x060009D2 RID: 2514 RVA: 0x00020300 File Offset: 0x0001E500
		internal override QueryResults<TInputOutput> Open(QuerySettings settings, bool preferStriping)
		{
			QueryResults<TInputOutput> queryResults = base.LeftChild.Open(settings, false);
			QueryResults<TInputOutput> queryResults2 = base.RightChild.Open(settings, false);
			return new BinaryQueryOperator<TInputOutput, TInputOutput, TInputOutput>.BinaryQueryOperatorResults(queryResults, queryResults2, this, settings, false);
		}

		// Token: 0x060009D3 RID: 2515 RVA: 0x00020334 File Offset: 0x0001E534
		public override void WrapPartitionedStream<TLeftKey, TRightKey>(PartitionedStream<TInputOutput, TLeftKey> leftStream, PartitionedStream<TInputOutput, TRightKey> rightStream, IPartitionedStreamRecipient<TInputOutput> outputRecipient, bool preferStriping, QuerySettings settings)
		{
			if (base.OutputOrdered)
			{
				this.WrapPartitionedStreamHelper<TLeftKey, TRightKey>(ExchangeUtilities.HashRepartitionOrdered<TInputOutput, NoKeyMemoizationRequired, TLeftKey>(leftStream, null, null, this._comparer, settings.CancellationState.MergedCancellationToken), rightStream, outputRecipient, settings.CancellationState.MergedCancellationToken);
				return;
			}
			this.WrapPartitionedStreamHelper<int, TRightKey>(ExchangeUtilities.HashRepartition<TInputOutput, NoKeyMemoizationRequired, TLeftKey>(leftStream, null, null, this._comparer, settings.CancellationState.MergedCancellationToken), rightStream, outputRecipient, settings.CancellationState.MergedCancellationToken);
		}

		// Token: 0x060009D4 RID: 2516 RVA: 0x000203A8 File Offset: 0x0001E5A8
		private void WrapPartitionedStreamHelper<TLeftKey, TRightKey>(PartitionedStream<Pair<TInputOutput, NoKeyMemoizationRequired>, TLeftKey> leftHashStream, PartitionedStream<TInputOutput, TRightKey> rightPartitionedStream, IPartitionedStreamRecipient<TInputOutput> outputRecipient, CancellationToken cancellationToken)
		{
			int partitionCount = leftHashStream.PartitionCount;
			PartitionedStream<Pair<TInputOutput, NoKeyMemoizationRequired>, int> partitionedStream = ExchangeUtilities.HashRepartition<TInputOutput, NoKeyMemoizationRequired, TRightKey>(rightPartitionedStream, null, null, this._comparer, cancellationToken);
			PartitionedStream<TInputOutput, TLeftKey> partitionedStream2 = new PartitionedStream<TInputOutput, TLeftKey>(partitionCount, leftHashStream.KeyComparer, OrdinalIndexState.Shuffled);
			for (int i = 0; i < partitionCount; i++)
			{
				if (base.OutputOrdered)
				{
					partitionedStream2[i] = new ExceptQueryOperator<TInputOutput>.OrderedExceptQueryOperatorEnumerator<TLeftKey>(leftHashStream[i], partitionedStream[i], this._comparer, leftHashStream.KeyComparer, cancellationToken);
				}
				else
				{
					partitionedStream2[i] = (QueryOperatorEnumerator<TInputOutput, TLeftKey>)new ExceptQueryOperator<TInputOutput>.ExceptQueryOperatorEnumerator<TLeftKey>(leftHashStream[i], partitionedStream[i], this._comparer, cancellationToken);
				}
			}
			outputRecipient.Receive<TLeftKey>(partitionedStream2);
		}

		// Token: 0x060009D5 RID: 2517 RVA: 0x00020448 File Offset: 0x0001E648
		internal override IEnumerable<TInputOutput> AsSequentialQuery(CancellationToken token)
		{
			IEnumerable<TInputOutput> enumerable = CancellableEnumerable.Wrap<TInputOutput>(base.LeftChild.AsSequentialQuery(token), token);
			IEnumerable<TInputOutput> enumerable2 = CancellableEnumerable.Wrap<TInputOutput>(base.RightChild.AsSequentialQuery(token), token);
			return enumerable.Except(enumerable2, this._comparer);
		}

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x060009D6 RID: 2518 RVA: 0x00002285 File Offset: 0x00000485
		internal override bool LimitsParallelism
		{
			get
			{
				return false;
			}
		}

		// Token: 0x040005FA RID: 1530
		private readonly IEqualityComparer<TInputOutput> _comparer;

		// Token: 0x0200013F RID: 319
		private class ExceptQueryOperatorEnumerator<TLeftKey> : QueryOperatorEnumerator<TInputOutput, int>
		{
			// Token: 0x060009D7 RID: 2519 RVA: 0x00020486 File Offset: 0x0001E686
			internal ExceptQueryOperatorEnumerator(QueryOperatorEnumerator<Pair<TInputOutput, NoKeyMemoizationRequired>, TLeftKey> leftSource, QueryOperatorEnumerator<Pair<TInputOutput, NoKeyMemoizationRequired>, int> rightSource, IEqualityComparer<TInputOutput> comparer, CancellationToken cancellationToken)
			{
				this._leftSource = leftSource;
				this._rightSource = rightSource;
				this._comparer = comparer;
				this._cancellationToken = cancellationToken;
			}

			// Token: 0x060009D8 RID: 2520 RVA: 0x000204AC File Offset: 0x0001E6AC
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
					if (this._hashLookup.Add(pair2.First))
					{
						currentElement = pair2.First;
						return true;
					}
				}
				return false;
			}

			// Token: 0x060009D9 RID: 2521 RVA: 0x00020598 File Offset: 0x0001E798
			protected override void Dispose(bool disposing)
			{
				this._leftSource.Dispose();
				this._rightSource.Dispose();
			}

			// Token: 0x040005FB RID: 1531
			private QueryOperatorEnumerator<Pair<TInputOutput, NoKeyMemoizationRequired>, TLeftKey> _leftSource;

			// Token: 0x040005FC RID: 1532
			private QueryOperatorEnumerator<Pair<TInputOutput, NoKeyMemoizationRequired>, int> _rightSource;

			// Token: 0x040005FD RID: 1533
			private IEqualityComparer<TInputOutput> _comparer;

			// Token: 0x040005FE RID: 1534
			private Set<TInputOutput> _hashLookup;

			// Token: 0x040005FF RID: 1535
			private CancellationToken _cancellationToken;

			// Token: 0x04000600 RID: 1536
			private Shared<int> _outputLoopCount;
		}

		// Token: 0x02000140 RID: 320
		private class OrderedExceptQueryOperatorEnumerator<TLeftKey> : QueryOperatorEnumerator<TInputOutput, TLeftKey>
		{
			// Token: 0x060009DA RID: 2522 RVA: 0x000205B0 File Offset: 0x0001E7B0
			internal OrderedExceptQueryOperatorEnumerator(QueryOperatorEnumerator<Pair<TInputOutput, NoKeyMemoizationRequired>, TLeftKey> leftSource, QueryOperatorEnumerator<Pair<TInputOutput, NoKeyMemoizationRequired>, int> rightSource, IEqualityComparer<TInputOutput> comparer, IComparer<TLeftKey> leftKeyComparer, CancellationToken cancellationToken)
			{
				this._leftSource = leftSource;
				this._rightSource = rightSource;
				this._comparer = comparer;
				this._leftKeyComparer = leftKeyComparer;
				this._cancellationToken = cancellationToken;
			}

			// Token: 0x060009DB RID: 2523 RVA: 0x000205E0 File Offset: 0x0001E7E0
			internal override bool MoveNext(ref TInputOutput currentElement, ref TLeftKey currentKey)
			{
				if (this._outputEnumerator == null)
				{
					Set<TInputOutput> set = new Set<TInputOutput>(this._comparer);
					Pair<TInputOutput, NoKeyMemoizationRequired> pair = default(Pair<TInputOutput, NoKeyMemoizationRequired>);
					int num = 0;
					int num2 = 0;
					while (this._rightSource.MoveNext(ref pair, ref num))
					{
						if ((num2++ & 63) == 0)
						{
							CancellationState.ThrowIfCanceled(this._cancellationToken);
						}
						set.Add(pair.First);
					}
					Dictionary<Wrapper<TInputOutput>, Pair<TInputOutput, TLeftKey>> dictionary = new Dictionary<Wrapper<TInputOutput>, Pair<TInputOutput, TLeftKey>>(new WrapperEqualityComparer<TInputOutput>(this._comparer));
					Pair<TInputOutput, NoKeyMemoizationRequired> pair2 = default(Pair<TInputOutput, NoKeyMemoizationRequired>);
					TLeftKey tleftKey = default(TLeftKey);
					while (this._leftSource.MoveNext(ref pair2, ref tleftKey))
					{
						if ((num2++ & 63) == 0)
						{
							CancellationState.ThrowIfCanceled(this._cancellationToken);
						}
						if (!set.Contains(pair2.First))
						{
							Wrapper<TInputOutput> wrapper = new Wrapper<TInputOutput>(pair2.First);
							Pair<TInputOutput, TLeftKey> pair3;
							if (!dictionary.TryGetValue(wrapper, out pair3) || this._leftKeyComparer.Compare(tleftKey, pair3.Second) < 0)
							{
								dictionary[wrapper] = new Pair<TInputOutput, TLeftKey>(pair2.First, tleftKey);
							}
						}
					}
					this._outputEnumerator = dictionary.GetEnumerator();
				}
				if (this._outputEnumerator.MoveNext())
				{
					KeyValuePair<Wrapper<TInputOutput>, Pair<TInputOutput, TLeftKey>> keyValuePair = this._outputEnumerator.Current;
					Pair<TInputOutput, TLeftKey> value = keyValuePair.Value;
					currentElement = value.First;
					currentKey = value.Second;
					return true;
				}
				return false;
			}

			// Token: 0x060009DC RID: 2524 RVA: 0x0002073F File Offset: 0x0001E93F
			protected override void Dispose(bool disposing)
			{
				this._leftSource.Dispose();
				this._rightSource.Dispose();
			}

			// Token: 0x04000601 RID: 1537
			private QueryOperatorEnumerator<Pair<TInputOutput, NoKeyMemoizationRequired>, TLeftKey> _leftSource;

			// Token: 0x04000602 RID: 1538
			private QueryOperatorEnumerator<Pair<TInputOutput, NoKeyMemoizationRequired>, int> _rightSource;

			// Token: 0x04000603 RID: 1539
			private IEqualityComparer<TInputOutput> _comparer;

			// Token: 0x04000604 RID: 1540
			private IComparer<TLeftKey> _leftKeyComparer;

			// Token: 0x04000605 RID: 1541
			private IEnumerator<KeyValuePair<Wrapper<TInputOutput>, Pair<TInputOutput, TLeftKey>>> _outputEnumerator;

			// Token: 0x04000606 RID: 1542
			private CancellationToken _cancellationToken;
		}
	}
}
