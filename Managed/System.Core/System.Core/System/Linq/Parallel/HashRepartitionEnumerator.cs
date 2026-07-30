using System;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x02000125 RID: 293
	internal class HashRepartitionEnumerator<TInputOutput, THashKey, TIgnoreKey> : QueryOperatorEnumerator<Pair<TInputOutput, THashKey>, int>
	{
		// Token: 0x0600098A RID: 2442 RVA: 0x0001EA7C File Offset: 0x0001CC7C
		internal HashRepartitionEnumerator(QueryOperatorEnumerator<TInputOutput, TIgnoreKey> source, int partitionCount, int partitionIndex, Func<TInputOutput, THashKey> keySelector, HashRepartitionStream<TInputOutput, THashKey, int> repartitionStream, CountdownEvent barrier, ListChunk<Pair<TInputOutput, THashKey>>[][] valueExchangeMatrix, CancellationToken cancellationToken)
		{
			this._source = source;
			this._partitionCount = partitionCount;
			this._partitionIndex = partitionIndex;
			this._keySelector = keySelector;
			this._repartitionStream = repartitionStream;
			this._barrier = barrier;
			this._valueExchangeMatrix = valueExchangeMatrix;
			this._cancellationToken = cancellationToken;
		}

		// Token: 0x0600098B RID: 2443 RVA: 0x0001EACC File Offset: 0x0001CCCC
		internal override bool MoveNext(ref Pair<TInputOutput, THashKey> currentElement, ref int currentKey)
		{
			if (this._partitionCount != 1)
			{
				HashRepartitionEnumerator<TInputOutput, THashKey, TIgnoreKey>.Mutables mutables = this._mutables;
				if (mutables == null)
				{
					mutables = (this._mutables = new HashRepartitionEnumerator<TInputOutput, THashKey, TIgnoreKey>.Mutables());
				}
				if (mutables._currentBufferIndex == -1)
				{
					this.EnumerateAndRedistributeElements();
				}
				while (mutables._currentBufferIndex < this._partitionCount)
				{
					if (mutables._currentBuffer != null)
					{
						HashRepartitionEnumerator<TInputOutput, THashKey, TIgnoreKey>.Mutables mutables2 = mutables;
						int num = mutables2._currentIndex + 1;
						mutables2._currentIndex = num;
						if (num < mutables._currentBuffer.Count)
						{
							currentElement = mutables._currentBuffer._chunk[mutables._currentIndex];
							return true;
						}
						mutables._currentIndex = -1;
						mutables._currentBuffer = mutables._currentBuffer.Next;
					}
					else
					{
						if (mutables._currentBufferIndex == this._partitionIndex)
						{
							this._barrier.Wait(this._cancellationToken);
							mutables._currentBufferIndex = -1;
						}
						mutables._currentBufferIndex++;
						mutables._currentIndex = -1;
						if (mutables._currentBufferIndex == this._partitionIndex)
						{
							mutables._currentBufferIndex++;
						}
						if (mutables._currentBufferIndex < this._partitionCount)
						{
							mutables._currentBuffer = this._valueExchangeMatrix[mutables._currentBufferIndex][this._partitionIndex];
						}
					}
				}
				return false;
			}
			TIgnoreKey tignoreKey = default(TIgnoreKey);
			TInputOutput tinputOutput = default(TInputOutput);
			if (this._source.MoveNext(ref tinputOutput, ref tignoreKey))
			{
				currentElement = new Pair<TInputOutput, THashKey>(tinputOutput, (this._keySelector == null) ? default(THashKey) : this._keySelector(tinputOutput));
				return true;
			}
			return false;
		}

		// Token: 0x0600098C RID: 2444 RVA: 0x0001EC58 File Offset: 0x0001CE58
		private void EnumerateAndRedistributeElements()
		{
			HashRepartitionEnumerator<TInputOutput, THashKey, TIgnoreKey>.Mutables mutables = this._mutables;
			ListChunk<Pair<TInputOutput, THashKey>>[] array = new ListChunk<Pair<TInputOutput, THashKey>>[this._partitionCount];
			TInputOutput tinputOutput = default(TInputOutput);
			TIgnoreKey tignoreKey = default(TIgnoreKey);
			int num = 0;
			while (this._source.MoveNext(ref tinputOutput, ref tignoreKey))
			{
				if ((num++ & 63) == 0)
				{
					CancellationState.ThrowIfCanceled(this._cancellationToken);
				}
				THashKey thashKey = default(THashKey);
				int num2;
				if (this._keySelector != null)
				{
					thashKey = this._keySelector(tinputOutput);
					num2 = this._repartitionStream.GetHashCode(thashKey) % this._partitionCount;
				}
				else
				{
					num2 = this._repartitionStream.GetHashCode(tinputOutput) % this._partitionCount;
				}
				ListChunk<Pair<TInputOutput, THashKey>> listChunk = array[num2];
				if (listChunk == null)
				{
					listChunk = (array[num2] = new ListChunk<Pair<TInputOutput, THashKey>>(128));
				}
				listChunk.Add(new Pair<TInputOutput, THashKey>(tinputOutput, thashKey));
			}
			for (int i = 0; i < this._partitionCount; i++)
			{
				this._valueExchangeMatrix[this._partitionIndex][i] = array[i];
			}
			this._barrier.Signal();
			mutables._currentBufferIndex = this._partitionIndex;
			mutables._currentBuffer = array[this._partitionIndex];
			mutables._currentIndex = -1;
		}

		// Token: 0x0600098D RID: 2445 RVA: 0x0001ED86 File Offset: 0x0001CF86
		protected override void Dispose(bool disposed)
		{
			if (this._barrier != null)
			{
				if (this._mutables == null || this._mutables._currentBufferIndex == -1)
				{
					this._barrier.Signal();
					this._barrier = null;
				}
				this._source.Dispose();
			}
		}

		// Token: 0x04000593 RID: 1427
		private const int ENUMERATION_NOT_STARTED = -1;

		// Token: 0x04000594 RID: 1428
		private readonly int _partitionCount;

		// Token: 0x04000595 RID: 1429
		private readonly int _partitionIndex;

		// Token: 0x04000596 RID: 1430
		private readonly Func<TInputOutput, THashKey> _keySelector;

		// Token: 0x04000597 RID: 1431
		private readonly HashRepartitionStream<TInputOutput, THashKey, int> _repartitionStream;

		// Token: 0x04000598 RID: 1432
		private readonly ListChunk<Pair<TInputOutput, THashKey>>[][] _valueExchangeMatrix;

		// Token: 0x04000599 RID: 1433
		private readonly QueryOperatorEnumerator<TInputOutput, TIgnoreKey> _source;

		// Token: 0x0400059A RID: 1434
		private CountdownEvent _barrier;

		// Token: 0x0400059B RID: 1435
		private readonly CancellationToken _cancellationToken;

		// Token: 0x0400059C RID: 1436
		private HashRepartitionEnumerator<TInputOutput, THashKey, TIgnoreKey>.Mutables _mutables;

		// Token: 0x02000126 RID: 294
		private class Mutables
		{
			// Token: 0x0600098E RID: 2446 RVA: 0x0001EDC4 File Offset: 0x0001CFC4
			internal Mutables()
			{
				this._currentBufferIndex = -1;
			}

			// Token: 0x0400059D RID: 1437
			internal int _currentBufferIndex;

			// Token: 0x0400059E RID: 1438
			internal ListChunk<Pair<TInputOutput, THashKey>> _currentBuffer;

			// Token: 0x0400059F RID: 1439
			internal int _currentIndex;
		}
	}
}
