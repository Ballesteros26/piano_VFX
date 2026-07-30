using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x020001E2 RID: 482
	internal sealed class TakeOrSkipWhileQueryOperator<TResult> : UnaryQueryOperator<TResult, TResult>
	{
		// Token: 0x06000C55 RID: 3157 RVA: 0x000290DF File Offset: 0x000272DF
		internal TakeOrSkipWhileQueryOperator(IEnumerable<TResult> child, Func<TResult, bool> predicate, Func<TResult, int, bool> indexedPredicate, bool take)
			: base(child)
		{
			this._predicate = predicate;
			this._indexedPredicate = indexedPredicate;
			this._take = take;
			this.InitOrderIndexState();
		}

		// Token: 0x06000C56 RID: 3158 RVA: 0x00029104 File Offset: 0x00027304
		private void InitOrderIndexState()
		{
			OrdinalIndexState ordinalIndexState = OrdinalIndexState.Increasing;
			OrdinalIndexState ordinalIndexState2 = base.Child.OrdinalIndexState;
			if (this._indexedPredicate != null)
			{
				ordinalIndexState = OrdinalIndexState.Correct;
				this._limitsParallelism = ordinalIndexState2 == OrdinalIndexState.Increasing;
			}
			OrdinalIndexState ordinalIndexState3 = ordinalIndexState2.Worse(OrdinalIndexState.Correct);
			if (ordinalIndexState3.IsWorseThan(ordinalIndexState))
			{
				this._prematureMerge = true;
			}
			if (!this._take)
			{
				ordinalIndexState3 = ordinalIndexState3.Worse(OrdinalIndexState.Increasing);
			}
			base.SetOrdinalIndexState(ordinalIndexState3);
		}

		// Token: 0x06000C57 RID: 3159 RVA: 0x00029164 File Offset: 0x00027364
		internal override void WrapPartitionedStream<TKey>(PartitionedStream<TResult, TKey> inputStream, IPartitionedStreamRecipient<TResult> recipient, bool preferStriping, QuerySettings settings)
		{
			if (this._prematureMerge)
			{
				PartitionedStream<TResult, int> partitionedStream = QueryOperator<TResult>.ExecuteAndCollectResults<TKey>(inputStream, inputStream.PartitionCount, base.Child.OutputOrdered, preferStriping, settings).GetPartitionedStream();
				this.WrapHelper<int>(partitionedStream, recipient, settings);
				return;
			}
			this.WrapHelper<TKey>(inputStream, recipient, settings);
		}

		// Token: 0x06000C58 RID: 3160 RVA: 0x000291B0 File Offset: 0x000273B0
		private void WrapHelper<TKey>(PartitionedStream<TResult, TKey> inputStream, IPartitionedStreamRecipient<TResult> recipient, QuerySettings settings)
		{
			int partitionCount = inputStream.PartitionCount;
			TakeOrSkipWhileQueryOperator<TResult>.OperatorState<TKey> operatorState = new TakeOrSkipWhileQueryOperator<TResult>.OperatorState<TKey>();
			CountdownEvent countdownEvent = new CountdownEvent(partitionCount);
			Func<TResult, TKey, bool> func = (Func<TResult, TKey, bool>)this._indexedPredicate;
			PartitionedStream<TResult, TKey> partitionedStream = new PartitionedStream<TResult, TKey>(partitionCount, inputStream.KeyComparer, this.OrdinalIndexState);
			for (int i = 0; i < partitionCount; i++)
			{
				partitionedStream[i] = new TakeOrSkipWhileQueryOperator<TResult>.TakeOrSkipWhileQueryOperatorEnumerator<TKey>(inputStream[i], this._predicate, func, this._take, operatorState, countdownEvent, settings.CancellationState.MergedCancellationToken, inputStream.KeyComparer);
			}
			recipient.Receive<TKey>(partitionedStream);
		}

		// Token: 0x06000C59 RID: 3161 RVA: 0x00029240 File Offset: 0x00027440
		internal override QueryResults<TResult> Open(QuerySettings settings, bool preferStriping)
		{
			return new UnaryQueryOperator<TResult, TResult>.UnaryQueryOperatorResults(base.Child.Open(settings, true), this, settings, preferStriping);
		}

		// Token: 0x06000C5A RID: 3162 RVA: 0x00029258 File Offset: 0x00027458
		internal override IEnumerable<TResult> AsSequentialQuery(CancellationToken token)
		{
			if (this._take)
			{
				if (this._indexedPredicate != null)
				{
					return base.Child.AsSequentialQuery(token).TakeWhile(this._indexedPredicate);
				}
				return base.Child.AsSequentialQuery(token).TakeWhile(this._predicate);
			}
			else
			{
				if (this._indexedPredicate != null)
				{
					return CancellableEnumerable.Wrap<TResult>(base.Child.AsSequentialQuery(token), token).SkipWhile(this._indexedPredicate);
				}
				return CancellableEnumerable.Wrap<TResult>(base.Child.AsSequentialQuery(token), token).SkipWhile(this._predicate);
			}
		}

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x06000C5B RID: 3163 RVA: 0x000292E8 File Offset: 0x000274E8
		internal override bool LimitsParallelism
		{
			get
			{
				return this._limitsParallelism;
			}
		}

		// Token: 0x04000783 RID: 1923
		private Func<TResult, bool> _predicate;

		// Token: 0x04000784 RID: 1924
		private Func<TResult, int, bool> _indexedPredicate;

		// Token: 0x04000785 RID: 1925
		private readonly bool _take;

		// Token: 0x04000786 RID: 1926
		private bool _prematureMerge;

		// Token: 0x04000787 RID: 1927
		private bool _limitsParallelism;

		// Token: 0x020001E3 RID: 483
		private class TakeOrSkipWhileQueryOperatorEnumerator<TKey> : QueryOperatorEnumerator<TResult, TKey>
		{
			// Token: 0x06000C5C RID: 3164 RVA: 0x000292F0 File Offset: 0x000274F0
			internal TakeOrSkipWhileQueryOperatorEnumerator(QueryOperatorEnumerator<TResult, TKey> source, Func<TResult, bool> predicate, Func<TResult, TKey, bool> indexedPredicate, bool take, TakeOrSkipWhileQueryOperator<TResult>.OperatorState<TKey> operatorState, CountdownEvent sharedBarrier, CancellationToken cancelToken, IComparer<TKey> keyComparer)
			{
				this._source = source;
				this._predicate = predicate;
				this._indexedPredicate = indexedPredicate;
				this._take = take;
				this._operatorState = operatorState;
				this._sharedBarrier = sharedBarrier;
				this._cancellationToken = cancelToken;
				this._keyComparer = keyComparer;
			}

			// Token: 0x06000C5D RID: 3165 RVA: 0x00029340 File Offset: 0x00027540
			internal override bool MoveNext(ref TResult currentElement, ref TKey currentKey)
			{
				if (this._buffer == null)
				{
					List<Pair<TResult, TKey>> list = new List<Pair<TResult, TKey>>();
					try
					{
						TResult tresult = default(TResult);
						TKey tkey = default(TKey);
						int num = 0;
						while (this._source.MoveNext(ref tresult, ref tkey))
						{
							if ((num++ & 63) == 0)
							{
								CancellationState.ThrowIfCanceled(this._cancellationToken);
							}
							list.Add(new Pair<TResult, TKey>(tresult, tkey));
							if (this._updatesSeen != this._operatorState._updatesDone)
							{
								TakeOrSkipWhileQueryOperator<TResult>.OperatorState<TKey> operatorState = this._operatorState;
								lock (operatorState)
								{
									this._currentLowKey = this._operatorState._currentLowKey;
									this._updatesSeen = this._operatorState._updatesDone;
								}
							}
							if (this._updatesSeen > 0 && this._keyComparer.Compare(tkey, this._currentLowKey) > 0)
							{
								break;
							}
							bool flag2;
							if (this._predicate != null)
							{
								flag2 = this._predicate(tresult);
							}
							else
							{
								flag2 = this._indexedPredicate(tresult, tkey);
							}
							if (!flag2)
							{
								TakeOrSkipWhileQueryOperator<TResult>.OperatorState<TKey> operatorState = this._operatorState;
								lock (operatorState)
								{
									if (this._operatorState._updatesDone == 0 || this._keyComparer.Compare(this._operatorState._currentLowKey, tkey) > 0)
									{
										this._currentLowKey = (this._operatorState._currentLowKey = tkey);
										TakeOrSkipWhileQueryOperator<TResult>.OperatorState<TKey> operatorState2 = this._operatorState;
										int num2 = operatorState2._updatesDone + 1;
										operatorState2._updatesDone = num2;
										this._updatesSeen = num2;
									}
									break;
								}
							}
						}
					}
					finally
					{
						this._sharedBarrier.Signal();
					}
					this._sharedBarrier.Wait(this._cancellationToken);
					this._buffer = list;
					this._bufferIndex = new Shared<int>(-1);
				}
				if (this._take)
				{
					if (this._bufferIndex.Value >= this._buffer.Count - 1)
					{
						return false;
					}
					this._bufferIndex.Value++;
					currentElement = this._buffer[this._bufferIndex.Value].First;
					currentKey = this._buffer[this._bufferIndex.Value].Second;
					return this._operatorState._updatesDone == 0 || this._keyComparer.Compare(this._operatorState._currentLowKey, currentKey) > 0;
				}
				else
				{
					if (this._operatorState._updatesDone == 0)
					{
						return false;
					}
					if (this._bufferIndex.Value < this._buffer.Count - 1)
					{
						this._bufferIndex.Value++;
						while (this._bufferIndex.Value < this._buffer.Count)
						{
							if (this._keyComparer.Compare(this._buffer[this._bufferIndex.Value].Second, this._operatorState._currentLowKey) >= 0)
							{
								currentElement = this._buffer[this._bufferIndex.Value].First;
								currentKey = this._buffer[this._bufferIndex.Value].Second;
								return true;
							}
							this._bufferIndex.Value++;
						}
					}
					return this._source.MoveNext(ref currentElement, ref currentKey);
				}
			}

			// Token: 0x06000C5E RID: 3166 RVA: 0x0002971C File Offset: 0x0002791C
			protected override void Dispose(bool disposing)
			{
				this._source.Dispose();
			}

			// Token: 0x04000788 RID: 1928
			private readonly QueryOperatorEnumerator<TResult, TKey> _source;

			// Token: 0x04000789 RID: 1929
			private readonly Func<TResult, bool> _predicate;

			// Token: 0x0400078A RID: 1930
			private readonly Func<TResult, TKey, bool> _indexedPredicate;

			// Token: 0x0400078B RID: 1931
			private readonly bool _take;

			// Token: 0x0400078C RID: 1932
			private readonly IComparer<TKey> _keyComparer;

			// Token: 0x0400078D RID: 1933
			private readonly TakeOrSkipWhileQueryOperator<TResult>.OperatorState<TKey> _operatorState;

			// Token: 0x0400078E RID: 1934
			private readonly CountdownEvent _sharedBarrier;

			// Token: 0x0400078F RID: 1935
			private readonly CancellationToken _cancellationToken;

			// Token: 0x04000790 RID: 1936
			private List<Pair<TResult, TKey>> _buffer;

			// Token: 0x04000791 RID: 1937
			private Shared<int> _bufferIndex;

			// Token: 0x04000792 RID: 1938
			private int _updatesSeen;

			// Token: 0x04000793 RID: 1939
			private TKey _currentLowKey;
		}

		// Token: 0x020001E4 RID: 484
		private class OperatorState<TKey>
		{
			// Token: 0x04000794 RID: 1940
			internal volatile int _updatesDone;

			// Token: 0x04000795 RID: 1941
			internal TKey _currentLowKey;
		}
	}
}
