using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x0200012C RID: 300
	internal class PartitionedDataSource<T> : PartitionedStream<T, int>
	{
		// Token: 0x06000999 RID: 2457 RVA: 0x0001F312 File Offset: 0x0001D512
		internal PartitionedDataSource(IEnumerable<T> source, int partitionCount, bool useStriping)
			: base(partitionCount, Util.GetDefaultComparer<int>(), (source is IList<T>) ? OrdinalIndexState.Indexable : OrdinalIndexState.Correct)
		{
			this.InitializePartitions(source, partitionCount, useStriping);
		}

		// Token: 0x0600099A RID: 2458 RVA: 0x0001F338 File Offset: 0x0001D538
		private void InitializePartitions(IEnumerable<T> source, int partitionCount, bool useStriping)
		{
			ParallelEnumerableWrapper<T> parallelEnumerableWrapper = source as ParallelEnumerableWrapper<T>;
			if (parallelEnumerableWrapper != null)
			{
				source = parallelEnumerableWrapper.WrappedEnumerable;
			}
			IList<T> list = source as IList<T>;
			if (list != null)
			{
				QueryOperatorEnumerator<T, int>[] array = new QueryOperatorEnumerator<T, int>[partitionCount];
				T[] array2 = source as T[];
				int num = -1;
				if (useStriping)
				{
					num = Scheduling.GetDefaultChunkSize<T>();
					if (num < 1)
					{
						num = 1;
					}
				}
				for (int i = 0; i < partitionCount; i++)
				{
					if (array2 != null)
					{
						if (useStriping)
						{
							array[i] = new PartitionedDataSource<T>.ArrayIndexRangeEnumerator(array2, partitionCount, i, num);
						}
						else
						{
							array[i] = new PartitionedDataSource<T>.ArrayContiguousIndexRangeEnumerator(array2, partitionCount, i);
						}
					}
					else if (useStriping)
					{
						array[i] = new PartitionedDataSource<T>.ListIndexRangeEnumerator(list, partitionCount, i, num);
					}
					else
					{
						array[i] = new PartitionedDataSource<T>.ListContiguousIndexRangeEnumerator(list, partitionCount, i);
					}
				}
				this._partitions = array;
				return;
			}
			this._partitions = PartitionedDataSource<T>.MakePartitions(source.GetEnumerator(), partitionCount);
		}

		// Token: 0x0600099B RID: 2459 RVA: 0x0001F3F8 File Offset: 0x0001D5F8
		private static QueryOperatorEnumerator<T, int>[] MakePartitions(IEnumerator<T> source, int partitionCount)
		{
			QueryOperatorEnumerator<T, int>[] array = new QueryOperatorEnumerator<T, int>[partitionCount];
			object obj = new object();
			Shared<int> shared = new Shared<int>(0);
			Shared<int> shared2 = new Shared<int>(partitionCount);
			Shared<bool> shared3 = new Shared<bool>(false);
			for (int i = 0; i < partitionCount; i++)
			{
				array[i] = new PartitionedDataSource<T>.ContiguousChunkLazyEnumerator(source, shared3, obj, shared, shared2);
			}
			return array;
		}

		// Token: 0x0200012D RID: 301
		internal sealed class ArrayIndexRangeEnumerator : QueryOperatorEnumerator<T, int>
		{
			// Token: 0x0600099C RID: 2460 RVA: 0x0001F448 File Offset: 0x0001D648
			internal ArrayIndexRangeEnumerator(T[] data, int partitionCount, int partitionIndex, int maxChunkSize)
			{
				this._data = data;
				this._elementCount = data.Length;
				this._partitionCount = partitionCount;
				this._partitionIndex = partitionIndex;
				this._maxChunkSize = maxChunkSize;
				int num = maxChunkSize * partitionCount;
				this._sectionCount = this._elementCount / num + ((this._elementCount % num == 0) ? 0 : 1);
			}

			// Token: 0x0600099D RID: 2461 RVA: 0x0001F4A4 File Offset: 0x0001D6A4
			internal override bool MoveNext(ref T currentElement, ref int currentKey)
			{
				PartitionedDataSource<T>.ArrayIndexRangeEnumerator.Mutables mutables = this._mutables;
				if (mutables == null)
				{
					mutables = (this._mutables = new PartitionedDataSource<T>.ArrayIndexRangeEnumerator.Mutables());
				}
				PartitionedDataSource<T>.ArrayIndexRangeEnumerator.Mutables mutables2 = mutables;
				int num = mutables2._currentPositionInChunk + 1;
				mutables2._currentPositionInChunk = num;
				if (num < mutables._currentChunkSize || this.MoveNextSlowPath())
				{
					currentKey = mutables._currentChunkOffset + mutables._currentPositionInChunk;
					currentElement = this._data[currentKey];
					return true;
				}
				return false;
			}

			// Token: 0x0600099E RID: 2462 RVA: 0x0001F510 File Offset: 0x0001D710
			private bool MoveNextSlowPath()
			{
				PartitionedDataSource<T>.ArrayIndexRangeEnumerator.Mutables mutables = this._mutables;
				PartitionedDataSource<T>.ArrayIndexRangeEnumerator.Mutables mutables2 = mutables;
				int num = mutables2._currentSection + 1;
				mutables2._currentSection = num;
				int num2 = num;
				int num3 = this._sectionCount - num2;
				if (num3 <= 0)
				{
					return false;
				}
				int num4 = num2 * this._partitionCount * this._maxChunkSize;
				mutables._currentPositionInChunk = 0;
				if (num3 > 1)
				{
					mutables._currentChunkSize = this._maxChunkSize;
					mutables._currentChunkOffset = num4 + this._partitionIndex * this._maxChunkSize;
				}
				else
				{
					int num5 = this._elementCount - num4;
					int num6 = num5 / this._partitionCount;
					int num7 = num5 % this._partitionCount;
					mutables._currentChunkSize = num6;
					if (this._partitionIndex < num7)
					{
						mutables._currentChunkSize++;
					}
					if (mutables._currentChunkSize == 0)
					{
						return false;
					}
					mutables._currentChunkOffset = num4 + this._partitionIndex * num6 + ((this._partitionIndex < num7) ? this._partitionIndex : num7);
				}
				return true;
			}

			// Token: 0x040005B4 RID: 1460
			private readonly T[] _data;

			// Token: 0x040005B5 RID: 1461
			private readonly int _elementCount;

			// Token: 0x040005B6 RID: 1462
			private readonly int _partitionCount;

			// Token: 0x040005B7 RID: 1463
			private readonly int _partitionIndex;

			// Token: 0x040005B8 RID: 1464
			private readonly int _maxChunkSize;

			// Token: 0x040005B9 RID: 1465
			private readonly int _sectionCount;

			// Token: 0x040005BA RID: 1466
			private PartitionedDataSource<T>.ArrayIndexRangeEnumerator.Mutables _mutables;

			// Token: 0x0200012E RID: 302
			private class Mutables
			{
				// Token: 0x0600099F RID: 2463 RVA: 0x0001F5F2 File Offset: 0x0001D7F2
				internal Mutables()
				{
					this._currentSection = -1;
				}

				// Token: 0x040005BB RID: 1467
				internal int _currentSection;

				// Token: 0x040005BC RID: 1468
				internal int _currentChunkSize;

				// Token: 0x040005BD RID: 1469
				internal int _currentPositionInChunk;

				// Token: 0x040005BE RID: 1470
				internal int _currentChunkOffset;
			}
		}

		// Token: 0x0200012F RID: 303
		internal sealed class ArrayContiguousIndexRangeEnumerator : QueryOperatorEnumerator<T, int>
		{
			// Token: 0x060009A0 RID: 2464 RVA: 0x0001F604 File Offset: 0x0001D804
			internal ArrayContiguousIndexRangeEnumerator(T[] data, int partitionCount, int partitionIndex)
			{
				this._data = data;
				int num = data.Length / partitionCount;
				int num2 = data.Length % partitionCount;
				int num3 = partitionIndex * num + ((partitionIndex < num2) ? partitionIndex : num2);
				this._startIndex = num3 - 1;
				this._maximumIndex = num3 + num + ((partitionIndex < num2) ? 1 : 0);
			}

			// Token: 0x060009A1 RID: 2465 RVA: 0x0001F654 File Offset: 0x0001D854
			internal override bool MoveNext(ref T currentElement, ref int currentKey)
			{
				if (this._currentIndex == null)
				{
					this._currentIndex = new Shared<int>(this._startIndex);
				}
				Shared<int> currentIndex = this._currentIndex;
				int num = currentIndex.Value + 1;
				currentIndex.Value = num;
				int num2 = num;
				if (num2 < this._maximumIndex)
				{
					currentKey = num2;
					currentElement = this._data[num2];
					return true;
				}
				return false;
			}

			// Token: 0x040005BF RID: 1471
			private readonly T[] _data;

			// Token: 0x040005C0 RID: 1472
			private readonly int _startIndex;

			// Token: 0x040005C1 RID: 1473
			private readonly int _maximumIndex;

			// Token: 0x040005C2 RID: 1474
			private Shared<int> _currentIndex;
		}

		// Token: 0x02000130 RID: 304
		internal sealed class ListIndexRangeEnumerator : QueryOperatorEnumerator<T, int>
		{
			// Token: 0x060009A2 RID: 2466 RVA: 0x0001F6B4 File Offset: 0x0001D8B4
			internal ListIndexRangeEnumerator(IList<T> data, int partitionCount, int partitionIndex, int maxChunkSize)
			{
				this._data = data;
				this._elementCount = data.Count;
				this._partitionCount = partitionCount;
				this._partitionIndex = partitionIndex;
				this._maxChunkSize = maxChunkSize;
				int num = maxChunkSize * partitionCount;
				this._sectionCount = this._elementCount / num + ((this._elementCount % num == 0) ? 0 : 1);
			}

			// Token: 0x060009A3 RID: 2467 RVA: 0x0001F714 File Offset: 0x0001D914
			internal override bool MoveNext(ref T currentElement, ref int currentKey)
			{
				PartitionedDataSource<T>.ListIndexRangeEnumerator.Mutables mutables = this._mutables;
				if (mutables == null)
				{
					mutables = (this._mutables = new PartitionedDataSource<T>.ListIndexRangeEnumerator.Mutables());
				}
				PartitionedDataSource<T>.ListIndexRangeEnumerator.Mutables mutables2 = mutables;
				int num = mutables2._currentPositionInChunk + 1;
				mutables2._currentPositionInChunk = num;
				if (num < mutables._currentChunkSize || this.MoveNextSlowPath())
				{
					currentKey = mutables._currentChunkOffset + mutables._currentPositionInChunk;
					currentElement = this._data[currentKey];
					return true;
				}
				return false;
			}

			// Token: 0x060009A4 RID: 2468 RVA: 0x0001F780 File Offset: 0x0001D980
			private bool MoveNextSlowPath()
			{
				PartitionedDataSource<T>.ListIndexRangeEnumerator.Mutables mutables = this._mutables;
				PartitionedDataSource<T>.ListIndexRangeEnumerator.Mutables mutables2 = mutables;
				int num = mutables2._currentSection + 1;
				mutables2._currentSection = num;
				int num2 = num;
				int num3 = this._sectionCount - num2;
				if (num3 <= 0)
				{
					return false;
				}
				int num4 = num2 * this._partitionCount * this._maxChunkSize;
				mutables._currentPositionInChunk = 0;
				if (num3 > 1)
				{
					mutables._currentChunkSize = this._maxChunkSize;
					mutables._currentChunkOffset = num4 + this._partitionIndex * this._maxChunkSize;
				}
				else
				{
					int num5 = this._elementCount - num4;
					int num6 = num5 / this._partitionCount;
					int num7 = num5 % this._partitionCount;
					mutables._currentChunkSize = num6;
					if (this._partitionIndex < num7)
					{
						mutables._currentChunkSize++;
					}
					if (mutables._currentChunkSize == 0)
					{
						return false;
					}
					mutables._currentChunkOffset = num4 + this._partitionIndex * num6 + ((this._partitionIndex < num7) ? this._partitionIndex : num7);
				}
				return true;
			}

			// Token: 0x040005C3 RID: 1475
			private readonly IList<T> _data;

			// Token: 0x040005C4 RID: 1476
			private readonly int _elementCount;

			// Token: 0x040005C5 RID: 1477
			private readonly int _partitionCount;

			// Token: 0x040005C6 RID: 1478
			private readonly int _partitionIndex;

			// Token: 0x040005C7 RID: 1479
			private readonly int _maxChunkSize;

			// Token: 0x040005C8 RID: 1480
			private readonly int _sectionCount;

			// Token: 0x040005C9 RID: 1481
			private PartitionedDataSource<T>.ListIndexRangeEnumerator.Mutables _mutables;

			// Token: 0x02000131 RID: 305
			private class Mutables
			{
				// Token: 0x060009A5 RID: 2469 RVA: 0x0001F862 File Offset: 0x0001DA62
				internal Mutables()
				{
					this._currentSection = -1;
				}

				// Token: 0x040005CA RID: 1482
				internal int _currentSection;

				// Token: 0x040005CB RID: 1483
				internal int _currentChunkSize;

				// Token: 0x040005CC RID: 1484
				internal int _currentPositionInChunk;

				// Token: 0x040005CD RID: 1485
				internal int _currentChunkOffset;
			}
		}

		// Token: 0x02000132 RID: 306
		internal sealed class ListContiguousIndexRangeEnumerator : QueryOperatorEnumerator<T, int>
		{
			// Token: 0x060009A6 RID: 2470 RVA: 0x0001F874 File Offset: 0x0001DA74
			internal ListContiguousIndexRangeEnumerator(IList<T> data, int partitionCount, int partitionIndex)
			{
				this._data = data;
				int num = data.Count / partitionCount;
				int num2 = data.Count % partitionCount;
				int num3 = partitionIndex * num + ((partitionIndex < num2) ? partitionIndex : num2);
				this._startIndex = num3 - 1;
				this._maximumIndex = num3 + num + ((partitionIndex < num2) ? 1 : 0);
			}

			// Token: 0x060009A7 RID: 2471 RVA: 0x0001F8C8 File Offset: 0x0001DAC8
			internal override bool MoveNext(ref T currentElement, ref int currentKey)
			{
				if (this._currentIndex == null)
				{
					this._currentIndex = new Shared<int>(this._startIndex);
				}
				Shared<int> currentIndex = this._currentIndex;
				int num = currentIndex.Value + 1;
				currentIndex.Value = num;
				int num2 = num;
				if (num2 < this._maximumIndex)
				{
					currentKey = num2;
					currentElement = this._data[num2];
					return true;
				}
				return false;
			}

			// Token: 0x040005CE RID: 1486
			private readonly IList<T> _data;

			// Token: 0x040005CF RID: 1487
			private readonly int _startIndex;

			// Token: 0x040005D0 RID: 1488
			private readonly int _maximumIndex;

			// Token: 0x040005D1 RID: 1489
			private Shared<int> _currentIndex;
		}

		// Token: 0x02000133 RID: 307
		private class ContiguousChunkLazyEnumerator : QueryOperatorEnumerator<T, int>
		{
			// Token: 0x060009A8 RID: 2472 RVA: 0x0001F926 File Offset: 0x0001DB26
			internal ContiguousChunkLazyEnumerator(IEnumerator<T> source, Shared<bool> exceptionTracker, object sourceSyncLock, Shared<int> currentIndex, Shared<int> degreeOfParallelism)
			{
				this._source = source;
				this._sourceSyncLock = sourceSyncLock;
				this._currentIndex = currentIndex;
				this._activeEnumeratorsCount = degreeOfParallelism;
				this._exceptionTracker = exceptionTracker;
			}

			// Token: 0x060009A9 RID: 2473 RVA: 0x0001F954 File Offset: 0x0001DB54
			internal override bool MoveNext(ref T currentElement, ref int currentKey)
			{
				PartitionedDataSource<T>.ContiguousChunkLazyEnumerator.Mutables mutables = this._mutables;
				if (mutables == null)
				{
					mutables = (this._mutables = new PartitionedDataSource<T>.ContiguousChunkLazyEnumerator.Mutables());
				}
				T[] chunkBuffer;
				int num2;
				for (;;)
				{
					chunkBuffer = mutables._chunkBuffer;
					PartitionedDataSource<T>.ContiguousChunkLazyEnumerator.Mutables mutables2 = mutables;
					int num = mutables2._currentChunkIndex + 1;
					mutables2._currentChunkIndex = num;
					num2 = num;
					if (num2 < mutables._currentChunkSize)
					{
						break;
					}
					object sourceSyncLock = this._sourceSyncLock;
					lock (sourceSyncLock)
					{
						int num3 = 0;
						if (this._exceptionTracker.Value)
						{
							return false;
						}
						try
						{
							while (num3 < mutables._nextChunkMaxSize && this._source.MoveNext())
							{
								chunkBuffer[num3] = this._source.Current;
								num3++;
							}
						}
						catch
						{
							this._exceptionTracker.Value = true;
							throw;
						}
						mutables._currentChunkSize = num3;
						if (num3 == 0)
						{
							return false;
						}
						mutables._chunkBaseIndex = this._currentIndex.Value;
						checked
						{
							this._currentIndex.Value += num3;
						}
					}
					if (mutables._nextChunkMaxSize < chunkBuffer.Length)
					{
						PartitionedDataSource<T>.ContiguousChunkLazyEnumerator.Mutables mutables3 = mutables;
						num = mutables3._chunkCounter;
						mutables3._chunkCounter = num + 1;
						if ((num & 7) == 7)
						{
							mutables._nextChunkMaxSize *= 2;
							if (mutables._nextChunkMaxSize > chunkBuffer.Length)
							{
								mutables._nextChunkMaxSize = chunkBuffer.Length;
							}
						}
					}
					mutables._currentChunkIndex = -1;
				}
				currentElement = chunkBuffer[num2];
				currentKey = mutables._chunkBaseIndex + num2;
				return true;
			}

			// Token: 0x060009AA RID: 2474 RVA: 0x0001FAE0 File Offset: 0x0001DCE0
			protected override void Dispose(bool disposing)
			{
				if (Interlocked.Decrement(ref this._activeEnumeratorsCount.Value) == 0)
				{
					this._source.Dispose();
				}
			}

			// Token: 0x040005D2 RID: 1490
			private const int chunksPerChunkSize = 7;

			// Token: 0x040005D3 RID: 1491
			private readonly IEnumerator<T> _source;

			// Token: 0x040005D4 RID: 1492
			private readonly object _sourceSyncLock;

			// Token: 0x040005D5 RID: 1493
			private readonly Shared<int> _currentIndex;

			// Token: 0x040005D6 RID: 1494
			private readonly Shared<int> _activeEnumeratorsCount;

			// Token: 0x040005D7 RID: 1495
			private readonly Shared<bool> _exceptionTracker;

			// Token: 0x040005D8 RID: 1496
			private PartitionedDataSource<T>.ContiguousChunkLazyEnumerator.Mutables _mutables;

			// Token: 0x02000134 RID: 308
			private class Mutables
			{
				// Token: 0x060009AB RID: 2475 RVA: 0x0001FAFF File Offset: 0x0001DCFF
				internal Mutables()
				{
					this._nextChunkMaxSize = 1;
					this._chunkBuffer = new T[Scheduling.GetDefaultChunkSize<T>()];
					this._currentChunkSize = 0;
					this._currentChunkIndex = -1;
					this._chunkBaseIndex = 0;
					this._chunkCounter = 0;
				}

				// Token: 0x040005D9 RID: 1497
				internal readonly T[] _chunkBuffer;

				// Token: 0x040005DA RID: 1498
				internal int _nextChunkMaxSize;

				// Token: 0x040005DB RID: 1499
				internal int _currentChunkSize;

				// Token: 0x040005DC RID: 1500
				internal int _currentChunkIndex;

				// Token: 0x040005DD RID: 1501
				internal int _chunkBaseIndex;

				// Token: 0x040005DE RID: 1502
				internal int _chunkCounter;
			}
		}
	}
}
