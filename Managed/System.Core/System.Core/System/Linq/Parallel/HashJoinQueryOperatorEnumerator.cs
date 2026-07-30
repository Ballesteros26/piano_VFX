using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x02000142 RID: 322
	internal class HashJoinQueryOperatorEnumerator<TLeftInput, TLeftKey, TRightInput, THashKey, TOutput> : QueryOperatorEnumerator<TOutput, TLeftKey>
	{
		// Token: 0x060009E3 RID: 2531 RVA: 0x00020928 File Offset: 0x0001EB28
		internal HashJoinQueryOperatorEnumerator(QueryOperatorEnumerator<Pair<TLeftInput, THashKey>, TLeftKey> leftSource, QueryOperatorEnumerator<Pair<TRightInput, THashKey>, int> rightSource, Func<TLeftInput, TRightInput, TOutput> singleResultSelector, Func<TLeftInput, IEnumerable<TRightInput>, TOutput> groupResultSelector, IEqualityComparer<THashKey> keyComparer, CancellationToken cancellationToken)
		{
			this._leftSource = leftSource;
			this._rightSource = rightSource;
			this._singleResultSelector = singleResultSelector;
			this._groupResultSelector = groupResultSelector;
			this._keyComparer = keyComparer;
			this._cancellationToken = cancellationToken;
		}

		// Token: 0x060009E4 RID: 2532 RVA: 0x00020960 File Offset: 0x0001EB60
		internal override bool MoveNext(ref TOutput currentElement, ref TLeftKey currentKey)
		{
			HashJoinQueryOperatorEnumerator<TLeftInput, TLeftKey, TRightInput, THashKey, TOutput>.Mutables mutables = this._mutables;
			if (mutables == null)
			{
				mutables = (this._mutables = new HashJoinQueryOperatorEnumerator<TLeftInput, TLeftKey, TRightInput, THashKey, TOutput>.Mutables());
				mutables._rightHashLookup = new HashLookup<THashKey, Pair<TRightInput, ListChunk<TRightInput>>>(this._keyComparer);
				Pair<TRightInput, THashKey> pair = default(Pair<TRightInput, THashKey>);
				int num = 0;
				int num2 = 0;
				while (this._rightSource.MoveNext(ref pair, ref num))
				{
					if ((num2++ & 63) == 0)
					{
						CancellationState.ThrowIfCanceled(this._cancellationToken);
					}
					TRightInput first = pair.First;
					THashKey second = pair.Second;
					if (second != null)
					{
						Pair<TRightInput, ListChunk<TRightInput>> pair2 = default(Pair<TRightInput, ListChunk<TRightInput>>);
						if (!mutables._rightHashLookup.TryGetValue(second, ref pair2))
						{
							pair2 = new Pair<TRightInput, ListChunk<TRightInput>>(first, null);
							if (this._groupResultSelector != null)
							{
								pair2.Second = new ListChunk<TRightInput>(2);
								pair2.Second.Add(first);
							}
							mutables._rightHashLookup.Add(second, pair2);
						}
						else
						{
							if (pair2.Second == null)
							{
								pair2.Second = new ListChunk<TRightInput>(2);
								mutables._rightHashLookup[second] = pair2;
							}
							pair2.Second.Add(first);
						}
					}
				}
			}
			ListChunk<TRightInput> currentRightMatches = mutables._currentRightMatches;
			if (currentRightMatches != null && mutables._currentRightMatchesIndex == currentRightMatches.Count)
			{
				ListChunk<TRightInput> listChunk = (mutables._currentRightMatches = currentRightMatches.Next);
				mutables._currentRightMatchesIndex = 0;
			}
			if (mutables._currentRightMatches == null)
			{
				Pair<TLeftInput, THashKey> pair3 = default(Pair<TLeftInput, THashKey>);
				TLeftKey tleftKey = default(TLeftKey);
				while (this._leftSource.MoveNext(ref pair3, ref tleftKey))
				{
					HashJoinQueryOperatorEnumerator<TLeftInput, TLeftKey, TRightInput, THashKey, TOutput>.Mutables mutables2 = mutables;
					int outputLoopCount = mutables2._outputLoopCount;
					mutables2._outputLoopCount = outputLoopCount + 1;
					if ((outputLoopCount & 63) == 0)
					{
						CancellationState.ThrowIfCanceled(this._cancellationToken);
					}
					Pair<TRightInput, ListChunk<TRightInput>> pair4 = default(Pair<TRightInput, ListChunk<TRightInput>>);
					TLeftInput first2 = pair3.First;
					THashKey second2 = pair3.Second;
					if (second2 != null && mutables._rightHashLookup.TryGetValue(second2, ref pair4) && this._singleResultSelector != null)
					{
						mutables._currentRightMatches = pair4.Second;
						mutables._currentRightMatchesIndex = 0;
						currentElement = this._singleResultSelector(first2, pair4.First);
						currentKey = tleftKey;
						if (pair4.Second != null)
						{
							mutables._currentLeft = first2;
							mutables._currentLeftKey = tleftKey;
						}
						return true;
					}
					if (this._groupResultSelector != null)
					{
						IEnumerable<TRightInput> enumerable = pair4.Second;
						if (enumerable == null)
						{
							enumerable = ParallelEnumerable.Empty<TRightInput>();
						}
						currentElement = this._groupResultSelector(first2, enumerable);
						currentKey = tleftKey;
						return true;
					}
				}
				return false;
			}
			currentElement = this._singleResultSelector(mutables._currentLeft, mutables._currentRightMatches._chunk[mutables._currentRightMatchesIndex]);
			currentKey = mutables._currentLeftKey;
			mutables._currentRightMatchesIndex++;
			return true;
		}

		// Token: 0x060009E5 RID: 2533 RVA: 0x00020C1D File Offset: 0x0001EE1D
		protected override void Dispose(bool disposing)
		{
			this._leftSource.Dispose();
			this._rightSource.Dispose();
		}

		// Token: 0x0400060B RID: 1547
		private readonly QueryOperatorEnumerator<Pair<TLeftInput, THashKey>, TLeftKey> _leftSource;

		// Token: 0x0400060C RID: 1548
		private readonly QueryOperatorEnumerator<Pair<TRightInput, THashKey>, int> _rightSource;

		// Token: 0x0400060D RID: 1549
		private readonly Func<TLeftInput, TRightInput, TOutput> _singleResultSelector;

		// Token: 0x0400060E RID: 1550
		private readonly Func<TLeftInput, IEnumerable<TRightInput>, TOutput> _groupResultSelector;

		// Token: 0x0400060F RID: 1551
		private readonly IEqualityComparer<THashKey> _keyComparer;

		// Token: 0x04000610 RID: 1552
		private readonly CancellationToken _cancellationToken;

		// Token: 0x04000611 RID: 1553
		private HashJoinQueryOperatorEnumerator<TLeftInput, TLeftKey, TRightInput, THashKey, TOutput>.Mutables _mutables;

		// Token: 0x02000143 RID: 323
		private class Mutables
		{
			// Token: 0x04000612 RID: 1554
			internal TLeftInput _currentLeft;

			// Token: 0x04000613 RID: 1555
			internal TLeftKey _currentLeftKey;

			// Token: 0x04000614 RID: 1556
			internal HashLookup<THashKey, Pair<TRightInput, ListChunk<TRightInput>>> _rightHashLookup;

			// Token: 0x04000615 RID: 1557
			internal ListChunk<TRightInput> _currentRightMatches;

			// Token: 0x04000616 RID: 1558
			internal int _currentRightMatchesIndex;

			// Token: 0x04000617 RID: 1559
			internal int _outputLoopCount;
		}
	}
}
