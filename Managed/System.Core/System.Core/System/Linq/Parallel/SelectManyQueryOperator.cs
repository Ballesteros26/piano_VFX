using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x020001D0 RID: 464
	internal sealed class SelectManyQueryOperator<TLeftInput, TRightInput, TOutput> : UnaryQueryOperator<TLeftInput, TOutput>
	{
		// Token: 0x06000C11 RID: 3089 RVA: 0x00028024 File Offset: 0x00026224
		internal SelectManyQueryOperator(IEnumerable<TLeftInput> leftChild, Func<TLeftInput, IEnumerable<TRightInput>> rightChildSelector, Func<TLeftInput, int, IEnumerable<TRightInput>> indexedRightChildSelector, Func<TLeftInput, TRightInput, TOutput> resultSelector)
			: base(leftChild)
		{
			this._rightChildSelector = rightChildSelector;
			this._indexedRightChildSelector = indexedRightChildSelector;
			this._resultSelector = resultSelector;
			this._outputOrdered = base.Child.OutputOrdered || indexedRightChildSelector != null;
			this.InitOrderIndex();
		}

		// Token: 0x06000C12 RID: 3090 RVA: 0x00028064 File Offset: 0x00026264
		private void InitOrderIndex()
		{
			OrdinalIndexState ordinalIndexState = base.Child.OrdinalIndexState;
			if (this._indexedRightChildSelector != null)
			{
				this._prematureMerge = ordinalIndexState.IsWorseThan(OrdinalIndexState.Correct);
				this._limitsParallelism = this._prematureMerge && ordinalIndexState != OrdinalIndexState.Shuffled;
			}
			else if (base.OutputOrdered)
			{
				this._prematureMerge = ordinalIndexState.IsWorseThan(OrdinalIndexState.Increasing);
			}
			base.SetOrdinalIndexState(OrdinalIndexState.Increasing);
		}

		// Token: 0x06000C13 RID: 3091 RVA: 0x000280C8 File Offset: 0x000262C8
		internal override void WrapPartitionedStream<TLeftKey>(PartitionedStream<TLeftInput, TLeftKey> inputStream, IPartitionedStreamRecipient<TOutput> recipient, bool preferStriping, QuerySettings settings)
		{
			int partitionCount = inputStream.PartitionCount;
			if (this._indexedRightChildSelector != null)
			{
				PartitionedStream<TLeftInput, int> partitionedStream;
				if (this._prematureMerge)
				{
					partitionedStream = QueryOperator<TLeftInput>.ExecuteAndCollectResults<TLeftKey>(inputStream, partitionCount, base.OutputOrdered, preferStriping, settings).GetPartitionedStream();
				}
				else
				{
					partitionedStream = (PartitionedStream<TLeftInput, int>)inputStream;
				}
				this.WrapPartitionedStreamIndexed(partitionedStream, recipient, settings);
				return;
			}
			if (this._prematureMerge)
			{
				PartitionedStream<TLeftInput, int> partitionedStream2 = QueryOperator<TLeftInput>.ExecuteAndCollectResults<TLeftKey>(inputStream, partitionCount, base.OutputOrdered, preferStriping, settings).GetPartitionedStream();
				this.WrapPartitionedStreamNotIndexed<int>(partitionedStream2, recipient, settings);
				return;
			}
			this.WrapPartitionedStreamNotIndexed<TLeftKey>(inputStream, recipient, settings);
		}

		// Token: 0x06000C14 RID: 3092 RVA: 0x0002814C File Offset: 0x0002634C
		private void WrapPartitionedStreamNotIndexed<TLeftKey>(PartitionedStream<TLeftInput, TLeftKey> inputStream, IPartitionedStreamRecipient<TOutput> recipient, QuerySettings settings)
		{
			int partitionCount = inputStream.PartitionCount;
			PairComparer<TLeftKey, int> pairComparer = new PairComparer<TLeftKey, int>(inputStream.KeyComparer, Util.GetDefaultComparer<int>());
			PartitionedStream<TOutput, Pair<TLeftKey, int>> partitionedStream = new PartitionedStream<TOutput, Pair<TLeftKey, int>>(partitionCount, pairComparer, this.OrdinalIndexState);
			for (int i = 0; i < partitionCount; i++)
			{
				partitionedStream[i] = new SelectManyQueryOperator<TLeftInput, TRightInput, TOutput>.SelectManyQueryOperatorEnumerator<TLeftKey>(inputStream[i], this, settings.CancellationState.MergedCancellationToken);
			}
			recipient.Receive<Pair<TLeftKey, int>>(partitionedStream);
		}

		// Token: 0x06000C15 RID: 3093 RVA: 0x000281B4 File Offset: 0x000263B4
		private void WrapPartitionedStreamIndexed(PartitionedStream<TLeftInput, int> inputStream, IPartitionedStreamRecipient<TOutput> recipient, QuerySettings settings)
		{
			PairComparer<int, int> pairComparer = new PairComparer<int, int>(inputStream.KeyComparer, Util.GetDefaultComparer<int>());
			PartitionedStream<TOutput, Pair<int, int>> partitionedStream = new PartitionedStream<TOutput, Pair<int, int>>(inputStream.PartitionCount, pairComparer, this.OrdinalIndexState);
			for (int i = 0; i < inputStream.PartitionCount; i++)
			{
				partitionedStream[i] = new SelectManyQueryOperator<TLeftInput, TRightInput, TOutput>.IndexedSelectManyQueryOperatorEnumerator(inputStream[i], this, settings.CancellationState.MergedCancellationToken);
			}
			recipient.Receive<Pair<int, int>>(partitionedStream);
		}

		// Token: 0x06000C16 RID: 3094 RVA: 0x0002821D File Offset: 0x0002641D
		internal override QueryResults<TOutput> Open(QuerySettings settings, bool preferStriping)
		{
			return new UnaryQueryOperator<TLeftInput, TOutput>.UnaryQueryOperatorResults(base.Child.Open(settings, preferStriping), this, settings, preferStriping);
		}

		// Token: 0x06000C17 RID: 3095 RVA: 0x00028234 File Offset: 0x00026434
		internal override IEnumerable<TOutput> AsSequentialQuery(CancellationToken token)
		{
			if (this._rightChildSelector != null)
			{
				if (this._resultSelector != null)
				{
					return CancellableEnumerable.Wrap<TLeftInput>(base.Child.AsSequentialQuery(token), token).SelectMany(this._rightChildSelector, this._resultSelector);
				}
				return (IEnumerable<TOutput>)CancellableEnumerable.Wrap<TLeftInput>(base.Child.AsSequentialQuery(token), token).SelectMany(this._rightChildSelector);
			}
			else
			{
				if (this._resultSelector != null)
				{
					return CancellableEnumerable.Wrap<TLeftInput>(base.Child.AsSequentialQuery(token), token).SelectMany(this._indexedRightChildSelector, this._resultSelector);
				}
				return (IEnumerable<TOutput>)CancellableEnumerable.Wrap<TLeftInput>(base.Child.AsSequentialQuery(token), token).SelectMany(this._indexedRightChildSelector);
			}
		}

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x06000C18 RID: 3096 RVA: 0x000282E6 File Offset: 0x000264E6
		internal override bool LimitsParallelism
		{
			get
			{
				return this._limitsParallelism;
			}
		}

		// Token: 0x04000745 RID: 1861
		private readonly Func<TLeftInput, IEnumerable<TRightInput>> _rightChildSelector;

		// Token: 0x04000746 RID: 1862
		private readonly Func<TLeftInput, int, IEnumerable<TRightInput>> _indexedRightChildSelector;

		// Token: 0x04000747 RID: 1863
		private readonly Func<TLeftInput, TRightInput, TOutput> _resultSelector;

		// Token: 0x04000748 RID: 1864
		private bool _prematureMerge;

		// Token: 0x04000749 RID: 1865
		private bool _limitsParallelism;

		// Token: 0x020001D1 RID: 465
		private class IndexedSelectManyQueryOperatorEnumerator : QueryOperatorEnumerator<TOutput, Pair<int, int>>
		{
			// Token: 0x06000C19 RID: 3097 RVA: 0x000282EE File Offset: 0x000264EE
			internal IndexedSelectManyQueryOperatorEnumerator(QueryOperatorEnumerator<TLeftInput, int> leftSource, SelectManyQueryOperator<TLeftInput, TRightInput, TOutput> selectManyOperator, CancellationToken cancellationToken)
			{
				this._leftSource = leftSource;
				this._selectManyOperator = selectManyOperator;
				this._cancellationToken = cancellationToken;
			}

			// Token: 0x06000C1A RID: 3098 RVA: 0x0002830C File Offset: 0x0002650C
			internal override bool MoveNext(ref TOutput currentElement, ref Pair<int, int> currentKey)
			{
				for (;;)
				{
					if (this._currentRightSource == null)
					{
						this._mutables = new SelectManyQueryOperator<TLeftInput, TRightInput, TOutput>.IndexedSelectManyQueryOperatorEnumerator.Mutables();
						SelectManyQueryOperator<TLeftInput, TRightInput, TOutput>.IndexedSelectManyQueryOperatorEnumerator.Mutables mutables = this._mutables;
						int lhsCount = mutables._lhsCount;
						mutables._lhsCount = lhsCount + 1;
						if ((lhsCount & 63) == 0)
						{
							CancellationState.ThrowIfCanceled(this._cancellationToken);
						}
						if (!this._leftSource.MoveNext(ref this._mutables._currentLeftElement, ref this._mutables._currentLeftSourceIndex))
						{
							break;
						}
						IEnumerable<TRightInput> enumerable = this._selectManyOperator._indexedRightChildSelector(this._mutables._currentLeftElement, this._mutables._currentLeftSourceIndex);
						this._currentRightSource = enumerable.GetEnumerator();
						if (this._selectManyOperator._resultSelector == null)
						{
							this._currentRightSourceAsOutput = (IEnumerator<TOutput>)this._currentRightSource;
						}
					}
					if (this._currentRightSource.MoveNext())
					{
						goto Block_4;
					}
					this._currentRightSource.Dispose();
					this._currentRightSource = null;
					this._currentRightSourceAsOutput = null;
				}
				return false;
				Block_4:
				this._mutables._currentRightSourceIndex++;
				if (this._selectManyOperator._resultSelector != null)
				{
					currentElement = this._selectManyOperator._resultSelector(this._mutables._currentLeftElement, this._currentRightSource.Current);
				}
				else
				{
					currentElement = this._currentRightSourceAsOutput.Current;
				}
				currentKey = new Pair<int, int>(this._mutables._currentLeftSourceIndex, this._mutables._currentRightSourceIndex);
				return true;
			}

			// Token: 0x06000C1B RID: 3099 RVA: 0x0002847A File Offset: 0x0002667A
			protected override void Dispose(bool disposing)
			{
				this._leftSource.Dispose();
				if (this._currentRightSource != null)
				{
					this._currentRightSource.Dispose();
				}
			}

			// Token: 0x0400074A RID: 1866
			private readonly QueryOperatorEnumerator<TLeftInput, int> _leftSource;

			// Token: 0x0400074B RID: 1867
			private readonly SelectManyQueryOperator<TLeftInput, TRightInput, TOutput> _selectManyOperator;

			// Token: 0x0400074C RID: 1868
			private IEnumerator<TRightInput> _currentRightSource;

			// Token: 0x0400074D RID: 1869
			private IEnumerator<TOutput> _currentRightSourceAsOutput;

			// Token: 0x0400074E RID: 1870
			private SelectManyQueryOperator<TLeftInput, TRightInput, TOutput>.IndexedSelectManyQueryOperatorEnumerator.Mutables _mutables;

			// Token: 0x0400074F RID: 1871
			private readonly CancellationToken _cancellationToken;

			// Token: 0x020001D2 RID: 466
			private class Mutables
			{
				// Token: 0x04000750 RID: 1872
				internal int _currentRightSourceIndex = -1;

				// Token: 0x04000751 RID: 1873
				internal TLeftInput _currentLeftElement;

				// Token: 0x04000752 RID: 1874
				internal int _currentLeftSourceIndex;

				// Token: 0x04000753 RID: 1875
				internal int _lhsCount;
			}
		}

		// Token: 0x020001D3 RID: 467
		private class SelectManyQueryOperatorEnumerator<TLeftKey> : QueryOperatorEnumerator<TOutput, Pair<TLeftKey, int>>
		{
			// Token: 0x06000C1D RID: 3101 RVA: 0x000284A9 File Offset: 0x000266A9
			internal SelectManyQueryOperatorEnumerator(QueryOperatorEnumerator<TLeftInput, TLeftKey> leftSource, SelectManyQueryOperator<TLeftInput, TRightInput, TOutput> selectManyOperator, CancellationToken cancellationToken)
			{
				this._leftSource = leftSource;
				this._selectManyOperator = selectManyOperator;
				this._cancellationToken = cancellationToken;
			}

			// Token: 0x06000C1E RID: 3102 RVA: 0x000284C8 File Offset: 0x000266C8
			internal override bool MoveNext(ref TOutput currentElement, ref Pair<TLeftKey, int> currentKey)
			{
				for (;;)
				{
					if (this._currentRightSource == null)
					{
						this._mutables = new SelectManyQueryOperator<TLeftInput, TRightInput, TOutput>.SelectManyQueryOperatorEnumerator<TLeftKey>.Mutables();
						SelectManyQueryOperator<TLeftInput, TRightInput, TOutput>.SelectManyQueryOperatorEnumerator<TLeftKey>.Mutables mutables = this._mutables;
						int lhsCount = mutables._lhsCount;
						mutables._lhsCount = lhsCount + 1;
						if ((lhsCount & 63) == 0)
						{
							CancellationState.ThrowIfCanceled(this._cancellationToken);
						}
						if (!this._leftSource.MoveNext(ref this._mutables._currentLeftElement, ref this._mutables._currentLeftKey))
						{
							break;
						}
						IEnumerable<TRightInput> enumerable = this._selectManyOperator._rightChildSelector(this._mutables._currentLeftElement);
						this._currentRightSource = enumerable.GetEnumerator();
						if (this._selectManyOperator._resultSelector == null)
						{
							this._currentRightSourceAsOutput = (IEnumerator<TOutput>)this._currentRightSource;
						}
					}
					if (this._currentRightSource.MoveNext())
					{
						goto Block_4;
					}
					this._currentRightSource.Dispose();
					this._currentRightSource = null;
					this._currentRightSourceAsOutput = null;
				}
				return false;
				Block_4:
				this._mutables._currentRightSourceIndex++;
				if (this._selectManyOperator._resultSelector != null)
				{
					currentElement = this._selectManyOperator._resultSelector(this._mutables._currentLeftElement, this._currentRightSource.Current);
				}
				else
				{
					currentElement = this._currentRightSourceAsOutput.Current;
				}
				currentKey = new Pair<TLeftKey, int>(this._mutables._currentLeftKey, this._mutables._currentRightSourceIndex);
				return true;
			}

			// Token: 0x06000C1F RID: 3103 RVA: 0x0002862B File Offset: 0x0002682B
			protected override void Dispose(bool disposing)
			{
				this._leftSource.Dispose();
				if (this._currentRightSource != null)
				{
					this._currentRightSource.Dispose();
				}
			}

			// Token: 0x04000754 RID: 1876
			private readonly QueryOperatorEnumerator<TLeftInput, TLeftKey> _leftSource;

			// Token: 0x04000755 RID: 1877
			private readonly SelectManyQueryOperator<TLeftInput, TRightInput, TOutput> _selectManyOperator;

			// Token: 0x04000756 RID: 1878
			private IEnumerator<TRightInput> _currentRightSource;

			// Token: 0x04000757 RID: 1879
			private IEnumerator<TOutput> _currentRightSourceAsOutput;

			// Token: 0x04000758 RID: 1880
			private SelectManyQueryOperator<TLeftInput, TRightInput, TOutput>.SelectManyQueryOperatorEnumerator<TLeftKey>.Mutables _mutables;

			// Token: 0x04000759 RID: 1881
			private readonly CancellationToken _cancellationToken;

			// Token: 0x020001D4 RID: 468
			private class Mutables
			{
				// Token: 0x0400075A RID: 1882
				internal int _currentRightSourceIndex = -1;

				// Token: 0x0400075B RID: 1883
				internal TLeftInput _currentLeftElement;

				// Token: 0x0400075C RID: 1884
				internal TLeftKey _currentLeftKey;

				// Token: 0x0400075D RID: 1885
				internal int _lhsCount;
			}
		}
	}
}
