using System;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x02000129 RID: 297
	internal class OrderedHashRepartitionEnumerator<TInputOutput, THashKey, TOrderKey> : QueryOperatorEnumerator<Pair<TInputOutput, THashKey>, TOrderKey>
	{
		// Token: 0x06000993 RID: 2451 RVA: 0x0001EE8C File Offset: 0x0001D08C
		internal OrderedHashRepartitionEnumerator(QueryOperatorEnumerator<TInputOutput, TOrderKey> source, int partitionCount, int partitionIndex, Func<TInputOutput, THashKey> keySelector, OrderedHashRepartitionStream<TInputOutput, THashKey, TOrderKey> repartitionStream, CountdownEvent barrier, ListChunk<Pair<TInputOutput, THashKey>>[][] valueExchangeMatrix, ListChunk<TOrderKey>[][] keyExchangeMatrix, CancellationToken cancellationToken)
		{
			this._source = source;
			this._partitionCount = partitionCount;
			this._partitionIndex = partitionIndex;
			this._keySelector = keySelector;
			this._repartitionStream = repartitionStream;
			this._barrier = barrier;
			this._valueExchangeMatrix = valueExchangeMatrix;
			this._keyExchangeMatrix = keyExchangeMatrix;
			this._cancellationToken = cancellationToken;
		}

		// Token: 0x06000994 RID: 2452 RVA: 0x0001EEE4 File Offset: 0x0001D0E4
		internal override bool MoveNext(ref Pair<TInputOutput, THashKey> currentElement, ref TOrderKey currentKey)
		{
			if (this._partitionCount != 1)
			{
				OrderedHashRepartitionEnumerator<TInputOutput, THashKey, TOrderKey>.Mutables mutables = this._mutables;
				if (mutables == null)
				{
					mutables = (this._mutables = new OrderedHashRepartitionEnumerator<TInputOutput, THashKey, TOrderKey>.Mutables());
				}
				if (mutables._currentBufferIndex == -1)
				{
					this.EnumerateAndRedistributeElements();
				}
				while (mutables._currentBufferIndex < this._partitionCount)
				{
					if (mutables._currentBuffer != null)
					{
						OrderedHashRepartitionEnumerator<TInputOutput, THashKey, TOrderKey>.Mutables mutables2 = mutables;
						int num = mutables2._currentIndex + 1;
						mutables2._currentIndex = num;
						if (num < mutables._currentBuffer.Count)
						{
							currentElement = mutables._currentBuffer._chunk[mutables._currentIndex];
							currentKey = mutables._currentKeyBuffer._chunk[mutables._currentIndex];
							return true;
						}
						mutables._currentIndex = -1;
						mutables._currentBuffer = mutables._currentBuffer.Next;
						mutables._currentKeyBuffer = mutables._currentKeyBuffer.Next;
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
							mutables._currentKeyBuffer = this._keyExchangeMatrix[mutables._currentBufferIndex][this._partitionIndex];
						}
					}
				}
				return false;
			}
			TInputOutput tinputOutput = default(TInputOutput);
			if (this._source.MoveNext(ref tinputOutput, ref currentKey))
			{
				currentElement = new Pair<TInputOutput, THashKey>(tinputOutput, (this._keySelector == null) ? default(THashKey) : this._keySelector(tinputOutput));
				return true;
			}
			return false;
		}

		// Token: 0x06000995 RID: 2453 RVA: 0x0001F0B0 File Offset: 0x0001D2B0
		private void EnumerateAndRedistributeElements()
		{
			OrderedHashRepartitionEnumerator<TInputOutput, THashKey, TOrderKey>.Mutables mutables = this._mutables;
			ListChunk<Pair<TInputOutput, THashKey>>[] array = new ListChunk<Pair<TInputOutput, THashKey>>[this._partitionCount];
			ListChunk<TOrderKey>[] array2 = new ListChunk<TOrderKey>[this._partitionCount];
			TInputOutput tinputOutput = default(TInputOutput);
			TOrderKey torderKey = default(TOrderKey);
			int num = 0;
			while (this._source.MoveNext(ref tinputOutput, ref torderKey))
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
				ListChunk<TOrderKey> listChunk2 = array2[num2];
				if (listChunk == null)
				{
					listChunk = (array[num2] = new ListChunk<Pair<TInputOutput, THashKey>>(128));
					listChunk2 = (array2[num2] = new ListChunk<TOrderKey>(128));
				}
				listChunk.Add(new Pair<TInputOutput, THashKey>(tinputOutput, thashKey));
				listChunk2.Add(torderKey);
			}
			for (int i = 0; i < this._partitionCount; i++)
			{
				this._valueExchangeMatrix[this._partitionIndex][i] = array[i];
				this._keyExchangeMatrix[this._partitionIndex][i] = array2[i];
			}
			this._barrier.Signal();
			mutables._currentBufferIndex = this._partitionIndex;
			mutables._currentBuffer = array[this._partitionIndex];
			mutables._currentKeyBuffer = array2[this._partitionIndex];
			mutables._currentIndex = -1;
		}

		// Token: 0x06000996 RID: 2454 RVA: 0x0001F22C File Offset: 0x0001D42C
		protected override void Dispose(bool disposing)
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

		// Token: 0x040005A5 RID: 1445
		private const int ENUMERATION_NOT_STARTED = -1;

		// Token: 0x040005A6 RID: 1446
		private readonly int _partitionCount;

		// Token: 0x040005A7 RID: 1447
		private readonly int _partitionIndex;

		// Token: 0x040005A8 RID: 1448
		private readonly Func<TInputOutput, THashKey> _keySelector;

		// Token: 0x040005A9 RID: 1449
		private readonly HashRepartitionStream<TInputOutput, THashKey, TOrderKey> _repartitionStream;

		// Token: 0x040005AA RID: 1450
		private readonly ListChunk<Pair<TInputOutput, THashKey>>[][] _valueExchangeMatrix;

		// Token: 0x040005AB RID: 1451
		private readonly ListChunk<TOrderKey>[][] _keyExchangeMatrix;

		// Token: 0x040005AC RID: 1452
		private readonly QueryOperatorEnumerator<TInputOutput, TOrderKey> _source;

		// Token: 0x040005AD RID: 1453
		private CountdownEvent _barrier;

		// Token: 0x040005AE RID: 1454
		private readonly CancellationToken _cancellationToken;

		// Token: 0x040005AF RID: 1455
		private OrderedHashRepartitionEnumerator<TInputOutput, THashKey, TOrderKey>.Mutables _mutables;

		// Token: 0x0200012A RID: 298
		private class Mutables
		{
			// Token: 0x06000997 RID: 2455 RVA: 0x0001F26A File Offset: 0x0001D46A
			internal Mutables()
			{
				this._currentBufferIndex = -1;
			}

			// Token: 0x040005B0 RID: 1456
			internal int _currentBufferIndex;

			// Token: 0x040005B1 RID: 1457
			internal ListChunk<Pair<TInputOutput, THashKey>> _currentBuffer;

			// Token: 0x040005B2 RID: 1458
			internal ListChunk<TOrderKey> _currentKeyBuffer;

			// Token: 0x040005B3 RID: 1459
			internal int _currentIndex;
		}
	}
}
