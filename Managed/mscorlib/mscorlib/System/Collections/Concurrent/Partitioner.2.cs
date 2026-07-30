using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Collections.Concurrent
{
	/// <summary>Provides common partitioning strategies for arrays, lists, and enumerables.</summary>
	// Token: 0x02000A0C RID: 2572
	public static class Partitioner
	{
		/// <summary>Creates an orderable partitioner from an <see cref="T:System.Collections.Generic.IList`1" /> instance.</summary>
		/// <returns>An orderable partitioner based on the input list.</returns>
		/// <param name="list">The list to be partitioned.</param>
		/// <param name="loadBalance">A Boolean value that indicates whether the created partitioner should dynamically load balance between partitions rather than statically partition.</param>
		/// <typeparam name="TSource">Type of the elements in source list.</typeparam>
		// Token: 0x06005F6C RID: 24428 RVA: 0x0013A623 File Offset: 0x00138823
		public static OrderablePartitioner<TSource> Create<TSource>(IList<TSource> list, bool loadBalance)
		{
			if (list == null)
			{
				throw new ArgumentNullException("list");
			}
			if (loadBalance)
			{
				return new Partitioner.DynamicPartitionerForIList<TSource>(list);
			}
			return new Partitioner.StaticIndexRangePartitionerForIList<TSource>(list);
		}

		/// <summary>Creates an orderable partitioner from a <see cref="T:System.Array" /> instance.</summary>
		/// <returns>An orderable partitioner based on the input array.</returns>
		/// <param name="array">The array to be partitioned.</param>
		/// <param name="loadBalance">A Boolean value that indicates whether the created partitioner should dynamically load balance between partitions rather than statically partition.</param>
		/// <typeparam name="TSource">Type of the elements in source array.</typeparam>
		// Token: 0x06005F6D RID: 24429 RVA: 0x0013A643 File Offset: 0x00138843
		public static OrderablePartitioner<TSource> Create<TSource>(TSource[] array, bool loadBalance)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (loadBalance)
			{
				return new Partitioner.DynamicPartitionerForArray<TSource>(array);
			}
			return new Partitioner.StaticIndexRangePartitionerForArray<TSource>(array);
		}

		/// <summary>Creates an orderable partitioner from a <see cref="T:System.Collections.Generic.IEnumerable`1" /> instance.</summary>
		/// <returns>An orderable partitioner based on the input array.</returns>
		/// <param name="source">The enumerable to be partitioned.</param>
		/// <typeparam name="TSource">Type of the elements in source enumerable.</typeparam>
		// Token: 0x06005F6E RID: 24430 RVA: 0x0013A663 File Offset: 0x00138863
		public static OrderablePartitioner<TSource> Create<TSource>(IEnumerable<TSource> source)
		{
			return Partitioner.Create<TSource>(source, EnumerablePartitionerOptions.None);
		}

		/// <summary>Creates an orderable partitioner from a <see cref="T:System.Collections.Generic.IEnumerable`1" /> instance.</summary>
		/// <returns>An orderable partitioner based on the input array.</returns>
		/// <param name="source">The enumerable to be partitioned.</param>
		/// <param name="partitionerOptions">Options to control the buffering behavior of the partitioner.</param>
		/// <typeparam name="TSource">Type of the elements in source enumerable.</typeparam>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="partitionerOptions" /> argument specifies an invalid value for <see cref="T:System.Collections.Concurrent.EnumerablePartitionerOptions" />.</exception>
		// Token: 0x06005F6F RID: 24431 RVA: 0x0013A66C File Offset: 0x0013886C
		public static OrderablePartitioner<TSource> Create<TSource>(IEnumerable<TSource> source, EnumerablePartitionerOptions partitionerOptions)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if ((partitionerOptions & ~EnumerablePartitionerOptions.NoBuffering) != EnumerablePartitionerOptions.None)
			{
				throw new ArgumentOutOfRangeException("partitionerOptions");
			}
			return new Partitioner.DynamicPartitionerForIEnumerable<TSource>(source, partitionerOptions);
		}

		/// <summary>Creates a partitioner that chunks the user-specified range.</summary>
		/// <returns>A partitioner.</returns>
		/// <param name="fromInclusive">The lower, inclusive bound of the range.</param>
		/// <param name="toExclusive">The upper, exclusive bound of the range.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="toExclusive" /> argument is less than or equal to the <paramref name="fromInclusive" /> argument.</exception>
		// Token: 0x06005F70 RID: 24432 RVA: 0x0013A694 File Offset: 0x00138894
		public static OrderablePartitioner<Tuple<long, long>> Create(long fromInclusive, long toExclusive)
		{
			int num = 3;
			if (toExclusive <= fromInclusive)
			{
				throw new ArgumentOutOfRangeException("toExclusive");
			}
			long num2 = (toExclusive - fromInclusive) / (long)(PlatformHelper.ProcessorCount * num);
			if (num2 == 0L)
			{
				num2 = 1L;
			}
			return Partitioner.Create<Tuple<long, long>>(Partitioner.CreateRanges(fromInclusive, toExclusive, num2), EnumerablePartitionerOptions.NoBuffering);
		}

		/// <summary>Creates a partitioner that chunks the user-specified range.</summary>
		/// <returns>A partitioner.</returns>
		/// <param name="fromInclusive">The lower, inclusive bound of the range.</param>
		/// <param name="toExclusive">The upper, exclusive bound of the range.</param>
		/// <param name="rangeSize">The size of each subrange.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="toExclusive" /> argument is less than or equal to the <paramref name="fromInclusive" /> argument.-or-The <paramref name="rangeSize" /> argument is less than or equal to 0.</exception>
		// Token: 0x06005F71 RID: 24433 RVA: 0x0013A6D3 File Offset: 0x001388D3
		public static OrderablePartitioner<Tuple<long, long>> Create(long fromInclusive, long toExclusive, long rangeSize)
		{
			if (toExclusive <= fromInclusive)
			{
				throw new ArgumentOutOfRangeException("toExclusive");
			}
			if (rangeSize <= 0L)
			{
				throw new ArgumentOutOfRangeException("rangeSize");
			}
			return Partitioner.Create<Tuple<long, long>>(Partitioner.CreateRanges(fromInclusive, toExclusive, rangeSize), EnumerablePartitionerOptions.NoBuffering);
		}

		// Token: 0x06005F72 RID: 24434 RVA: 0x0013A702 File Offset: 0x00138902
		private static IEnumerable<Tuple<long, long>> CreateRanges(long fromInclusive, long toExclusive, long rangeSize)
		{
			bool shouldQuit = false;
			long i = fromInclusive;
			while (i < toExclusive && !shouldQuit)
			{
				long num = i;
				long num2;
				try
				{
					num2 = checked(i + rangeSize);
				}
				catch (OverflowException)
				{
					num2 = toExclusive;
					shouldQuit = true;
				}
				if (num2 > toExclusive)
				{
					num2 = toExclusive;
				}
				yield return new Tuple<long, long>(num, num2);
				i += rangeSize;
			}
			yield break;
		}

		/// <summary>Creates a partitioner that chunks the user-specified range.</summary>
		/// <returns>A partitioner.</returns>
		/// <param name="fromInclusive">The lower, inclusive bound of the range.</param>
		/// <param name="toExclusive">The upper, exclusive bound of the range.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="toExclusive" /> argument is less than or equal to the <paramref name="fromInclusive" /> argument.</exception>
		// Token: 0x06005F73 RID: 24435 RVA: 0x0013A720 File Offset: 0x00138920
		public static OrderablePartitioner<Tuple<int, int>> Create(int fromInclusive, int toExclusive)
		{
			int num = 3;
			if (toExclusive <= fromInclusive)
			{
				throw new ArgumentOutOfRangeException("toExclusive");
			}
			int num2 = (toExclusive - fromInclusive) / (PlatformHelper.ProcessorCount * num);
			if (num2 == 0)
			{
				num2 = 1;
			}
			return Partitioner.Create<Tuple<int, int>>(Partitioner.CreateRanges(fromInclusive, toExclusive, num2), EnumerablePartitionerOptions.NoBuffering);
		}

		/// <summary>Creates a partitioner that chunks the user-specified range.</summary>
		/// <returns>A partitioner.</returns>
		/// <param name="fromInclusive">The lower, inclusive bound of the range.</param>
		/// <param name="toExclusive">The upper, exclusive bound of the range.</param>
		/// <param name="rangeSize">The size of each subrange.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="toExclusive" /> argument is less than or equal to the <paramref name="fromInclusive" /> argument.-or-The <paramref name="rangeSize" /> argument is less than or equal to 0.</exception>
		// Token: 0x06005F74 RID: 24436 RVA: 0x0013A75D File Offset: 0x0013895D
		public static OrderablePartitioner<Tuple<int, int>> Create(int fromInclusive, int toExclusive, int rangeSize)
		{
			if (toExclusive <= fromInclusive)
			{
				throw new ArgumentOutOfRangeException("toExclusive");
			}
			if (rangeSize <= 0)
			{
				throw new ArgumentOutOfRangeException("rangeSize");
			}
			return Partitioner.Create<Tuple<int, int>>(Partitioner.CreateRanges(fromInclusive, toExclusive, rangeSize), EnumerablePartitionerOptions.NoBuffering);
		}

		// Token: 0x06005F75 RID: 24437 RVA: 0x0013A78B File Offset: 0x0013898B
		private static IEnumerable<Tuple<int, int>> CreateRanges(int fromInclusive, int toExclusive, int rangeSize)
		{
			bool shouldQuit = false;
			int i = fromInclusive;
			while (i < toExclusive && !shouldQuit)
			{
				int num = i;
				int num2;
				try
				{
					num2 = checked(i + rangeSize);
				}
				catch (OverflowException)
				{
					num2 = toExclusive;
					shouldQuit = true;
				}
				if (num2 > toExclusive)
				{
					num2 = toExclusive;
				}
				yield return new Tuple<int, int>(num, num2);
				i += rangeSize;
			}
			yield break;
		}

		// Token: 0x06005F76 RID: 24438 RVA: 0x0013A7AC File Offset: 0x001389AC
		private static int GetDefaultChunkSize<TSource>()
		{
			int num;
			if (default(TSource) != null || Nullable.GetUnderlyingType(typeof(TSource)) != null)
			{
				num = 128;
			}
			else
			{
				num = 512 / IntPtr.Size;
			}
			return num;
		}

		// Token: 0x04003013 RID: 12307
		private const int DEFAULT_BYTES_PER_UNIT = 128;

		// Token: 0x04003014 RID: 12308
		private const int DEFAULT_BYTES_PER_CHUNK = 512;

		// Token: 0x02000A0D RID: 2573
		private abstract class DynamicPartitionEnumerator_Abstract<TSource, TSourceReader> : IEnumerator<KeyValuePair<long, TSource>>, IDisposable, IEnumerator
		{
			// Token: 0x06005F77 RID: 24439 RVA: 0x0013A7F5 File Offset: 0x001389F5
			protected DynamicPartitionEnumerator_Abstract(TSourceReader sharedReader, Partitioner.SharedLong sharedIndex)
				: this(sharedReader, sharedIndex, false)
			{
			}

			// Token: 0x06005F78 RID: 24440 RVA: 0x0013A800 File Offset: 0x00138A00
			protected DynamicPartitionEnumerator_Abstract(TSourceReader sharedReader, Partitioner.SharedLong sharedIndex, bool useSingleChunking)
			{
				this._sharedReader = sharedReader;
				this._sharedIndex = sharedIndex;
				this._maxChunkSize = (useSingleChunking ? 1 : Partitioner.DynamicPartitionEnumerator_Abstract<TSource, TSourceReader>.s_defaultMaxChunkSize);
			}

			// Token: 0x06005F79 RID: 24441
			protected abstract bool GrabNextChunk(int requestedChunkSize);

			// Token: 0x17001108 RID: 4360
			// (get) Token: 0x06005F7A RID: 24442
			// (set) Token: 0x06005F7B RID: 24443
			protected abstract bool HasNoElementsLeft { get; set; }

			// Token: 0x17001109 RID: 4361
			// (get) Token: 0x06005F7C RID: 24444
			public abstract KeyValuePair<long, TSource> Current { get; }

			// Token: 0x06005F7D RID: 24445
			public abstract void Dispose();

			// Token: 0x06005F7E RID: 24446 RVA: 0x00014B5A File Offset: 0x00012D5A
			public void Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x1700110A RID: 4362
			// (get) Token: 0x06005F7F RID: 24447 RVA: 0x0013A827 File Offset: 0x00138A27
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x06005F80 RID: 24448 RVA: 0x0013A834 File Offset: 0x00138A34
			public bool MoveNext()
			{
				if (this._localOffset == null)
				{
					this._localOffset = new Partitioner.SharedInt(-1);
					this._currentChunkSize = new Partitioner.SharedInt(0);
					this._doublingCountdown = 3;
				}
				if (this._localOffset.Value < this._currentChunkSize.Value - 1)
				{
					this._localOffset.Value++;
					return true;
				}
				int num;
				if (this._currentChunkSize.Value == 0)
				{
					num = 1;
				}
				else if (this._doublingCountdown > 0)
				{
					num = this._currentChunkSize.Value;
				}
				else
				{
					num = Math.Min(this._currentChunkSize.Value * 2, this._maxChunkSize);
					this._doublingCountdown = 3;
				}
				this._doublingCountdown--;
				if (this.GrabNextChunk(num))
				{
					this._localOffset.Value = 0;
					return true;
				}
				return false;
			}

			// Token: 0x04003015 RID: 12309
			protected readonly TSourceReader _sharedReader;

			// Token: 0x04003016 RID: 12310
			protected static int s_defaultMaxChunkSize = Partitioner.GetDefaultChunkSize<TSource>();

			// Token: 0x04003017 RID: 12311
			protected Partitioner.SharedInt _currentChunkSize;

			// Token: 0x04003018 RID: 12312
			protected Partitioner.SharedInt _localOffset;

			// Token: 0x04003019 RID: 12313
			private const int CHUNK_DOUBLING_RATE = 3;

			// Token: 0x0400301A RID: 12314
			private int _doublingCountdown;

			// Token: 0x0400301B RID: 12315
			protected readonly int _maxChunkSize;

			// Token: 0x0400301C RID: 12316
			protected readonly Partitioner.SharedLong _sharedIndex;
		}

		// Token: 0x02000A0E RID: 2574
		private class DynamicPartitionerForIEnumerable<TSource> : OrderablePartitioner<TSource>
		{
			// Token: 0x06005F82 RID: 24450 RVA: 0x0013A921 File Offset: 0x00138B21
			internal DynamicPartitionerForIEnumerable(IEnumerable<TSource> source, EnumerablePartitionerOptions partitionerOptions)
				: base(true, false, true)
			{
				this._source = source;
				this._useSingleChunking = (partitionerOptions & EnumerablePartitionerOptions.NoBuffering) > EnumerablePartitionerOptions.None;
			}

			// Token: 0x06005F83 RID: 24451 RVA: 0x0013A940 File Offset: 0x00138B40
			public override IList<IEnumerator<KeyValuePair<long, TSource>>> GetOrderablePartitions(int partitionCount)
			{
				if (partitionCount <= 0)
				{
					throw new ArgumentOutOfRangeException("partitionCount");
				}
				IEnumerator<KeyValuePair<long, TSource>>[] array = new IEnumerator<KeyValuePair<long, TSource>>[partitionCount];
				IEnumerable<KeyValuePair<long, TSource>> enumerable = new Partitioner.DynamicPartitionerForIEnumerable<TSource>.InternalPartitionEnumerable(this._source.GetEnumerator(), this._useSingleChunking, true);
				for (int i = 0; i < partitionCount; i++)
				{
					array[i] = enumerable.GetEnumerator();
				}
				return array;
			}

			// Token: 0x06005F84 RID: 24452 RVA: 0x0013A991 File Offset: 0x00138B91
			public override IEnumerable<KeyValuePair<long, TSource>> GetOrderableDynamicPartitions()
			{
				return new Partitioner.DynamicPartitionerForIEnumerable<TSource>.InternalPartitionEnumerable(this._source.GetEnumerator(), this._useSingleChunking, false);
			}

			// Token: 0x1700110B RID: 4363
			// (get) Token: 0x06005F85 RID: 24453 RVA: 0x00003B29 File Offset: 0x00001D29
			public override bool SupportsDynamicPartitions
			{
				get
				{
					return true;
				}
			}

			// Token: 0x0400301D RID: 12317
			private IEnumerable<TSource> _source;

			// Token: 0x0400301E RID: 12318
			private readonly bool _useSingleChunking;

			// Token: 0x02000A0F RID: 2575
			private class InternalPartitionEnumerable : IEnumerable<KeyValuePair<long, TSource>>, IEnumerable, IDisposable
			{
				// Token: 0x06005F86 RID: 24454 RVA: 0x0013A9AC File Offset: 0x00138BAC
				internal InternalPartitionEnumerable(IEnumerator<TSource> sharedReader, bool useSingleChunking, bool isStaticPartitioning)
				{
					this._sharedReader = sharedReader;
					this._sharedIndex = new Partitioner.SharedLong(-1L);
					this._hasNoElementsLeft = new Partitioner.SharedBool(false);
					this._sourceDepleted = new Partitioner.SharedBool(false);
					this._sharedLock = new object();
					this._useSingleChunking = useSingleChunking;
					if (!this._useSingleChunking)
					{
						int num = ((PlatformHelper.ProcessorCount > 4) ? 4 : 1);
						this._fillBuffer = new KeyValuePair<long, TSource>[num * Partitioner.GetDefaultChunkSize<TSource>()];
					}
					if (isStaticPartitioning)
					{
						this._activePartitionCount = new Partitioner.SharedInt(0);
						return;
					}
					this._activePartitionCount = null;
				}

				// Token: 0x06005F87 RID: 24455 RVA: 0x0013AA3D File Offset: 0x00138C3D
				public IEnumerator<KeyValuePair<long, TSource>> GetEnumerator()
				{
					if (this._disposed)
					{
						throw new ObjectDisposedException("Can not call GetEnumerator on partitions after the source enumerable is disposed");
					}
					return new Partitioner.DynamicPartitionerForIEnumerable<TSource>.InternalPartitionEnumerator(this._sharedReader, this._sharedIndex, this._hasNoElementsLeft, this._activePartitionCount, this, this._useSingleChunking);
				}

				// Token: 0x06005F88 RID: 24456 RVA: 0x0013AA76 File Offset: 0x00138C76
				IEnumerator IEnumerable.GetEnumerator()
				{
					return this.GetEnumerator();
				}

				// Token: 0x06005F89 RID: 24457 RVA: 0x0013AA80 File Offset: 0x00138C80
				private void TryCopyFromFillBuffer(KeyValuePair<long, TSource>[] destArray, int requestedChunkSize, ref int actualNumElementsGrabbed)
				{
					actualNumElementsGrabbed = 0;
					KeyValuePair<long, TSource>[] fillBuffer = this._fillBuffer;
					if (fillBuffer == null)
					{
						return;
					}
					if (this._fillBufferCurrentPosition >= this._fillBufferSize)
					{
						return;
					}
					Interlocked.Increment(ref this._activeCopiers);
					int num = Interlocked.Add(ref this._fillBufferCurrentPosition, requestedChunkSize);
					int num2 = num - requestedChunkSize;
					if (num2 < this._fillBufferSize)
					{
						actualNumElementsGrabbed = ((num < this._fillBufferSize) ? num : (this._fillBufferSize - num2));
						Array.Copy(fillBuffer, num2, destArray, 0, actualNumElementsGrabbed);
					}
					Interlocked.Decrement(ref this._activeCopiers);
				}

				// Token: 0x06005F8A RID: 24458 RVA: 0x0013AB09 File Offset: 0x00138D09
				internal bool GrabChunk(KeyValuePair<long, TSource>[] destArray, int requestedChunkSize, ref int actualNumElementsGrabbed)
				{
					actualNumElementsGrabbed = 0;
					if (this._hasNoElementsLeft.Value)
					{
						return false;
					}
					if (this._useSingleChunking)
					{
						return this.GrabChunk_Single(destArray, requestedChunkSize, ref actualNumElementsGrabbed);
					}
					return this.GrabChunk_Buffered(destArray, requestedChunkSize, ref actualNumElementsGrabbed);
				}

				// Token: 0x06005F8B RID: 24459 RVA: 0x0013AB3C File Offset: 0x00138D3C
				internal bool GrabChunk_Single(KeyValuePair<long, TSource>[] destArray, int requestedChunkSize, ref int actualNumElementsGrabbed)
				{
					object sharedLock = this._sharedLock;
					bool flag2;
					lock (sharedLock)
					{
						if (this._hasNoElementsLeft.Value)
						{
							flag2 = false;
						}
						else
						{
							try
							{
								if (this._sharedReader.MoveNext())
								{
									this._sharedIndex.Value = checked(this._sharedIndex.Value + 1L);
									destArray[0] = new KeyValuePair<long, TSource>(this._sharedIndex.Value, this._sharedReader.Current);
									actualNumElementsGrabbed = 1;
									flag2 = true;
								}
								else
								{
									this._sourceDepleted.Value = true;
									this._hasNoElementsLeft.Value = true;
									flag2 = false;
								}
							}
							catch
							{
								this._sourceDepleted.Value = true;
								this._hasNoElementsLeft.Value = true;
								throw;
							}
						}
					}
					return flag2;
				}

				// Token: 0x06005F8C RID: 24460 RVA: 0x0013AC28 File Offset: 0x00138E28
				internal bool GrabChunk_Buffered(KeyValuePair<long, TSource>[] destArray, int requestedChunkSize, ref int actualNumElementsGrabbed)
				{
					this.TryCopyFromFillBuffer(destArray, requestedChunkSize, ref actualNumElementsGrabbed);
					if (actualNumElementsGrabbed == requestedChunkSize)
					{
						return true;
					}
					if (this._sourceDepleted.Value)
					{
						this._hasNoElementsLeft.Value = true;
						this._fillBuffer = null;
						return actualNumElementsGrabbed > 0;
					}
					object sharedLock = this._sharedLock;
					lock (sharedLock)
					{
						if (this._sourceDepleted.Value)
						{
							return actualNumElementsGrabbed > 0;
						}
						try
						{
							if (this._activeCopiers > 0)
							{
								SpinWait spinWait = default(SpinWait);
								while (this._activeCopiers > 0)
								{
									spinWait.SpinOnce();
								}
							}
							while (actualNumElementsGrabbed < requestedChunkSize)
							{
								if (!this._sharedReader.MoveNext())
								{
									this._sourceDepleted.Value = true;
									break;
								}
								this._sharedIndex.Value = checked(this._sharedIndex.Value + 1L);
								destArray[actualNumElementsGrabbed] = new KeyValuePair<long, TSource>(this._sharedIndex.Value, this._sharedReader.Current);
								actualNumElementsGrabbed++;
							}
							KeyValuePair<long, TSource>[] fillBuffer = this._fillBuffer;
							if (!this._sourceDepleted.Value && fillBuffer != null && this._fillBufferCurrentPosition >= fillBuffer.Length)
							{
								for (int i = 0; i < fillBuffer.Length; i++)
								{
									if (!this._sharedReader.MoveNext())
									{
										this._sourceDepleted.Value = true;
										this._fillBufferSize = i;
										break;
									}
									this._sharedIndex.Value = checked(this._sharedIndex.Value + 1L);
									fillBuffer[i] = new KeyValuePair<long, TSource>(this._sharedIndex.Value, this._sharedReader.Current);
								}
								this._fillBufferCurrentPosition = 0;
							}
						}
						catch
						{
							this._sourceDepleted.Value = true;
							this._hasNoElementsLeft.Value = true;
							throw;
						}
					}
					return actualNumElementsGrabbed > 0;
				}

				// Token: 0x06005F8D RID: 24461 RVA: 0x0013AE44 File Offset: 0x00139044
				public void Dispose()
				{
					if (!this._disposed)
					{
						this._disposed = true;
						this._sharedReader.Dispose();
					}
				}

				// Token: 0x0400301F RID: 12319
				private readonly IEnumerator<TSource> _sharedReader;

				// Token: 0x04003020 RID: 12320
				private Partitioner.SharedLong _sharedIndex;

				// Token: 0x04003021 RID: 12321
				private volatile KeyValuePair<long, TSource>[] _fillBuffer;

				// Token: 0x04003022 RID: 12322
				private volatile int _fillBufferSize;

				// Token: 0x04003023 RID: 12323
				private volatile int _fillBufferCurrentPosition;

				// Token: 0x04003024 RID: 12324
				private volatile int _activeCopiers;

				// Token: 0x04003025 RID: 12325
				private Partitioner.SharedBool _hasNoElementsLeft;

				// Token: 0x04003026 RID: 12326
				private Partitioner.SharedBool _sourceDepleted;

				// Token: 0x04003027 RID: 12327
				private object _sharedLock;

				// Token: 0x04003028 RID: 12328
				private bool _disposed;

				// Token: 0x04003029 RID: 12329
				private Partitioner.SharedInt _activePartitionCount;

				// Token: 0x0400302A RID: 12330
				private readonly bool _useSingleChunking;
			}

			// Token: 0x02000A10 RID: 2576
			private class InternalPartitionEnumerator : Partitioner.DynamicPartitionEnumerator_Abstract<TSource, IEnumerator<TSource>>
			{
				// Token: 0x06005F8E RID: 24462 RVA: 0x0013AE60 File Offset: 0x00139060
				internal InternalPartitionEnumerator(IEnumerator<TSource> sharedReader, Partitioner.SharedLong sharedIndex, Partitioner.SharedBool hasNoElementsLeft, Partitioner.SharedInt activePartitionCount, Partitioner.DynamicPartitionerForIEnumerable<TSource>.InternalPartitionEnumerable enumerable, bool useSingleChunking)
					: base(sharedReader, sharedIndex, useSingleChunking)
				{
					this._hasNoElementsLeft = hasNoElementsLeft;
					this._enumerable = enumerable;
					this._activePartitionCount = activePartitionCount;
					if (this._activePartitionCount != null)
					{
						Interlocked.Increment(ref this._activePartitionCount.Value);
					}
				}

				// Token: 0x06005F8F RID: 24463 RVA: 0x0013AE9C File Offset: 0x0013909C
				protected override bool GrabNextChunk(int requestedChunkSize)
				{
					if (this.HasNoElementsLeft)
					{
						return false;
					}
					if (this._localList == null)
					{
						this._localList = new KeyValuePair<long, TSource>[this._maxChunkSize];
					}
					return this._enumerable.GrabChunk(this._localList, requestedChunkSize, ref this._currentChunkSize.Value);
				}

				// Token: 0x1700110C RID: 4364
				// (get) Token: 0x06005F90 RID: 24464 RVA: 0x0013AEE9 File Offset: 0x001390E9
				// (set) Token: 0x06005F91 RID: 24465 RVA: 0x0013AEF8 File Offset: 0x001390F8
				protected override bool HasNoElementsLeft
				{
					get
					{
						return this._hasNoElementsLeft.Value;
					}
					set
					{
						this._hasNoElementsLeft.Value = true;
					}
				}

				// Token: 0x1700110D RID: 4365
				// (get) Token: 0x06005F92 RID: 24466 RVA: 0x0013AF08 File Offset: 0x00139108
				public override KeyValuePair<long, TSource> Current
				{
					get
					{
						if (this._currentChunkSize == null)
						{
							throw new InvalidOperationException("MoveNext must be called at least once before calling Current.");
						}
						return this._localList[this._localOffset.Value];
					}
				}

				// Token: 0x06005F93 RID: 24467 RVA: 0x0013AF35 File Offset: 0x00139135
				public override void Dispose()
				{
					if (this._activePartitionCount != null && Interlocked.Decrement(ref this._activePartitionCount.Value) == 0)
					{
						this._enumerable.Dispose();
					}
				}

				// Token: 0x0400302B RID: 12331
				private KeyValuePair<long, TSource>[] _localList;

				// Token: 0x0400302C RID: 12332
				private readonly Partitioner.SharedBool _hasNoElementsLeft;

				// Token: 0x0400302D RID: 12333
				private readonly Partitioner.SharedInt _activePartitionCount;

				// Token: 0x0400302E RID: 12334
				private Partitioner.DynamicPartitionerForIEnumerable<TSource>.InternalPartitionEnumerable _enumerable;
			}
		}

		// Token: 0x02000A11 RID: 2577
		private abstract class DynamicPartitionerForIndexRange_Abstract<TSource, TCollection> : OrderablePartitioner<TSource>
		{
			// Token: 0x06005F94 RID: 24468 RVA: 0x0013AF5C File Offset: 0x0013915C
			protected DynamicPartitionerForIndexRange_Abstract(TCollection data)
				: base(true, false, true)
			{
				this._data = data;
			}

			// Token: 0x06005F95 RID: 24469
			protected abstract IEnumerable<KeyValuePair<long, TSource>> GetOrderableDynamicPartitions_Factory(TCollection data);

			// Token: 0x06005F96 RID: 24470 RVA: 0x0013AF70 File Offset: 0x00139170
			public override IList<IEnumerator<KeyValuePair<long, TSource>>> GetOrderablePartitions(int partitionCount)
			{
				if (partitionCount <= 0)
				{
					throw new ArgumentOutOfRangeException("partitionCount");
				}
				IEnumerator<KeyValuePair<long, TSource>>[] array = new IEnumerator<KeyValuePair<long, TSource>>[partitionCount];
				IEnumerable<KeyValuePair<long, TSource>> orderableDynamicPartitions_Factory = this.GetOrderableDynamicPartitions_Factory(this._data);
				for (int i = 0; i < partitionCount; i++)
				{
					array[i] = orderableDynamicPartitions_Factory.GetEnumerator();
				}
				return array;
			}

			// Token: 0x06005F97 RID: 24471 RVA: 0x0013AFB6 File Offset: 0x001391B6
			public override IEnumerable<KeyValuePair<long, TSource>> GetOrderableDynamicPartitions()
			{
				return this.GetOrderableDynamicPartitions_Factory(this._data);
			}

			// Token: 0x1700110E RID: 4366
			// (get) Token: 0x06005F98 RID: 24472 RVA: 0x00003B29 File Offset: 0x00001D29
			public override bool SupportsDynamicPartitions
			{
				get
				{
					return true;
				}
			}

			// Token: 0x0400302F RID: 12335
			private TCollection _data;
		}

		// Token: 0x02000A12 RID: 2578
		private abstract class DynamicPartitionEnumeratorForIndexRange_Abstract<TSource, TSourceReader> : Partitioner.DynamicPartitionEnumerator_Abstract<TSource, TSourceReader>
		{
			// Token: 0x06005F99 RID: 24473 RVA: 0x0013AFC4 File Offset: 0x001391C4
			protected DynamicPartitionEnumeratorForIndexRange_Abstract(TSourceReader sharedReader, Partitioner.SharedLong sharedIndex)
				: base(sharedReader, sharedIndex)
			{
			}

			// Token: 0x1700110F RID: 4367
			// (get) Token: 0x06005F9A RID: 24474
			protected abstract int SourceCount { get; }

			// Token: 0x06005F9B RID: 24475 RVA: 0x0013AFD0 File Offset: 0x001391D0
			protected override bool GrabNextChunk(int requestedChunkSize)
			{
				while (!this.HasNoElementsLeft)
				{
					long num = Volatile.Read(ref this._sharedIndex.Value);
					if (this.HasNoElementsLeft)
					{
						return false;
					}
					long num2 = Math.Min((long)(this.SourceCount - 1), num + (long)requestedChunkSize);
					if (Interlocked.CompareExchange(ref this._sharedIndex.Value, num2, num) == num)
					{
						this._currentChunkSize.Value = (int)(num2 - num);
						this._localOffset.Value = -1;
						this._startIndex = (int)(num + 1L);
						return true;
					}
				}
				return false;
			}

			// Token: 0x17001110 RID: 4368
			// (get) Token: 0x06005F9C RID: 24476 RVA: 0x0013B057 File Offset: 0x00139257
			// (set) Token: 0x06005F9D RID: 24477 RVA: 0x00002194 File Offset: 0x00000394
			protected override bool HasNoElementsLeft
			{
				get
				{
					return Volatile.Read(ref this._sharedIndex.Value) >= (long)(this.SourceCount - 1);
				}
				set
				{
				}
			}

			// Token: 0x06005F9E RID: 24478 RVA: 0x00002194 File Offset: 0x00000394
			public override void Dispose()
			{
			}

			// Token: 0x04003030 RID: 12336
			protected int _startIndex;
		}

		// Token: 0x02000A13 RID: 2579
		private class DynamicPartitionerForIList<TSource> : Partitioner.DynamicPartitionerForIndexRange_Abstract<TSource, IList<TSource>>
		{
			// Token: 0x06005F9F RID: 24479 RVA: 0x0013B077 File Offset: 0x00139277
			internal DynamicPartitionerForIList(IList<TSource> source)
				: base(source)
			{
			}

			// Token: 0x06005FA0 RID: 24480 RVA: 0x0013B080 File Offset: 0x00139280
			protected override IEnumerable<KeyValuePair<long, TSource>> GetOrderableDynamicPartitions_Factory(IList<TSource> _data)
			{
				return new Partitioner.DynamicPartitionerForIList<TSource>.InternalPartitionEnumerable(_data);
			}

			// Token: 0x02000A14 RID: 2580
			private class InternalPartitionEnumerable : IEnumerable<KeyValuePair<long, TSource>>, IEnumerable
			{
				// Token: 0x06005FA1 RID: 24481 RVA: 0x0013B088 File Offset: 0x00139288
				internal InternalPartitionEnumerable(IList<TSource> sharedReader)
				{
					this._sharedReader = sharedReader;
					this._sharedIndex = new Partitioner.SharedLong(-1L);
				}

				// Token: 0x06005FA2 RID: 24482 RVA: 0x0013B0A4 File Offset: 0x001392A4
				public IEnumerator<KeyValuePair<long, TSource>> GetEnumerator()
				{
					return new Partitioner.DynamicPartitionerForIList<TSource>.InternalPartitionEnumerator(this._sharedReader, this._sharedIndex);
				}

				// Token: 0x06005FA3 RID: 24483 RVA: 0x0013B0B7 File Offset: 0x001392B7
				IEnumerator IEnumerable.GetEnumerator()
				{
					return this.GetEnumerator();
				}

				// Token: 0x04003031 RID: 12337
				private readonly IList<TSource> _sharedReader;

				// Token: 0x04003032 RID: 12338
				private Partitioner.SharedLong _sharedIndex;
			}

			// Token: 0x02000A15 RID: 2581
			private class InternalPartitionEnumerator : Partitioner.DynamicPartitionEnumeratorForIndexRange_Abstract<TSource, IList<TSource>>
			{
				// Token: 0x06005FA4 RID: 24484 RVA: 0x0013B0BF File Offset: 0x001392BF
				internal InternalPartitionEnumerator(IList<TSource> sharedReader, Partitioner.SharedLong sharedIndex)
					: base(sharedReader, sharedIndex)
				{
				}

				// Token: 0x17001111 RID: 4369
				// (get) Token: 0x06005FA5 RID: 24485 RVA: 0x0013B0C9 File Offset: 0x001392C9
				protected override int SourceCount
				{
					get
					{
						return this._sharedReader.Count;
					}
				}

				// Token: 0x17001112 RID: 4370
				// (get) Token: 0x06005FA6 RID: 24486 RVA: 0x0013B0D8 File Offset: 0x001392D8
				public override KeyValuePair<long, TSource> Current
				{
					get
					{
						if (this._currentChunkSize == null)
						{
							throw new InvalidOperationException("MoveNext must be called at least once before calling Current.");
						}
						return new KeyValuePair<long, TSource>((long)(this._startIndex + this._localOffset.Value), this._sharedReader[this._startIndex + this._localOffset.Value]);
					}
				}
			}
		}

		// Token: 0x02000A16 RID: 2582
		private class DynamicPartitionerForArray<TSource> : Partitioner.DynamicPartitionerForIndexRange_Abstract<TSource, TSource[]>
		{
			// Token: 0x06005FA7 RID: 24487 RVA: 0x0013B131 File Offset: 0x00139331
			internal DynamicPartitionerForArray(TSource[] source)
				: base(source)
			{
			}

			// Token: 0x06005FA8 RID: 24488 RVA: 0x0013B13A File Offset: 0x0013933A
			protected override IEnumerable<KeyValuePair<long, TSource>> GetOrderableDynamicPartitions_Factory(TSource[] _data)
			{
				return new Partitioner.DynamicPartitionerForArray<TSource>.InternalPartitionEnumerable(_data);
			}

			// Token: 0x02000A17 RID: 2583
			private class InternalPartitionEnumerable : IEnumerable<KeyValuePair<long, TSource>>, IEnumerable
			{
				// Token: 0x06005FA9 RID: 24489 RVA: 0x0013B142 File Offset: 0x00139342
				internal InternalPartitionEnumerable(TSource[] sharedReader)
				{
					this._sharedReader = sharedReader;
					this._sharedIndex = new Partitioner.SharedLong(-1L);
				}

				// Token: 0x06005FAA RID: 24490 RVA: 0x0013B15E File Offset: 0x0013935E
				IEnumerator IEnumerable.GetEnumerator()
				{
					return this.GetEnumerator();
				}

				// Token: 0x06005FAB RID: 24491 RVA: 0x0013B166 File Offset: 0x00139366
				public IEnumerator<KeyValuePair<long, TSource>> GetEnumerator()
				{
					return new Partitioner.DynamicPartitionerForArray<TSource>.InternalPartitionEnumerator(this._sharedReader, this._sharedIndex);
				}

				// Token: 0x04003033 RID: 12339
				private readonly TSource[] _sharedReader;

				// Token: 0x04003034 RID: 12340
				private Partitioner.SharedLong _sharedIndex;
			}

			// Token: 0x02000A18 RID: 2584
			private class InternalPartitionEnumerator : Partitioner.DynamicPartitionEnumeratorForIndexRange_Abstract<TSource, TSource[]>
			{
				// Token: 0x06005FAC RID: 24492 RVA: 0x0013B179 File Offset: 0x00139379
				internal InternalPartitionEnumerator(TSource[] sharedReader, Partitioner.SharedLong sharedIndex)
					: base(sharedReader, sharedIndex)
				{
				}

				// Token: 0x17001113 RID: 4371
				// (get) Token: 0x06005FAD RID: 24493 RVA: 0x0013B183 File Offset: 0x00139383
				protected override int SourceCount
				{
					get
					{
						return this._sharedReader.Length;
					}
				}

				// Token: 0x17001114 RID: 4372
				// (get) Token: 0x06005FAE RID: 24494 RVA: 0x0013B190 File Offset: 0x00139390
				public override KeyValuePair<long, TSource> Current
				{
					get
					{
						if (this._currentChunkSize == null)
						{
							throw new InvalidOperationException("MoveNext must be called at least once before calling Current.");
						}
						return new KeyValuePair<long, TSource>((long)(this._startIndex + this._localOffset.Value), this._sharedReader[this._startIndex + this._localOffset.Value]);
					}
				}
			}
		}

		// Token: 0x02000A19 RID: 2585
		private abstract class StaticIndexRangePartitioner<TSource, TCollection> : OrderablePartitioner<TSource>
		{
			// Token: 0x06005FAF RID: 24495 RVA: 0x0013B1E9 File Offset: 0x001393E9
			protected StaticIndexRangePartitioner()
				: base(true, true, true)
			{
			}

			// Token: 0x17001115 RID: 4373
			// (get) Token: 0x06005FB0 RID: 24496
			protected abstract int SourceCount { get; }

			// Token: 0x06005FB1 RID: 24497
			protected abstract IEnumerator<KeyValuePair<long, TSource>> CreatePartition(int startIndex, int endIndex);

			// Token: 0x06005FB2 RID: 24498 RVA: 0x0013B1F4 File Offset: 0x001393F4
			public override IList<IEnumerator<KeyValuePair<long, TSource>>> GetOrderablePartitions(int partitionCount)
			{
				if (partitionCount <= 0)
				{
					throw new ArgumentOutOfRangeException("partitionCount");
				}
				int num = this.SourceCount / partitionCount;
				int num2 = this.SourceCount % partitionCount;
				IEnumerator<KeyValuePair<long, TSource>>[] array = new IEnumerator<KeyValuePair<long, TSource>>[partitionCount];
				int num3 = -1;
				for (int i = 0; i < partitionCount; i++)
				{
					int num4 = num3 + 1;
					if (i < num2)
					{
						num3 = num4 + num;
					}
					else
					{
						num3 = num4 + num - 1;
					}
					array[i] = this.CreatePartition(num4, num3);
				}
				return array;
			}
		}

		// Token: 0x02000A1A RID: 2586
		private abstract class StaticIndexRangePartition<TSource> : IEnumerator<KeyValuePair<long, TSource>>, IDisposable, IEnumerator
		{
			// Token: 0x06005FB3 RID: 24499 RVA: 0x0013B261 File Offset: 0x00139461
			protected StaticIndexRangePartition(int startIndex, int endIndex)
			{
				this._startIndex = startIndex;
				this._endIndex = endIndex;
				this._offset = startIndex - 1;
			}

			// Token: 0x17001116 RID: 4374
			// (get) Token: 0x06005FB4 RID: 24500
			public abstract KeyValuePair<long, TSource> Current { get; }

			// Token: 0x06005FB5 RID: 24501 RVA: 0x00002194 File Offset: 0x00000394
			public void Dispose()
			{
			}

			// Token: 0x06005FB6 RID: 24502 RVA: 0x00014B5A File Offset: 0x00012D5A
			public void Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x06005FB7 RID: 24503 RVA: 0x0013B282 File Offset: 0x00139482
			public bool MoveNext()
			{
				if (this._offset < this._endIndex)
				{
					this._offset++;
					return true;
				}
				this._offset = this._endIndex + 1;
				return false;
			}

			// Token: 0x17001117 RID: 4375
			// (get) Token: 0x06005FB8 RID: 24504 RVA: 0x0013B2B9 File Offset: 0x001394B9
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x04003035 RID: 12341
			protected readonly int _startIndex;

			// Token: 0x04003036 RID: 12342
			protected readonly int _endIndex;

			// Token: 0x04003037 RID: 12343
			protected volatile int _offset;
		}

		// Token: 0x02000A1B RID: 2587
		private class StaticIndexRangePartitionerForIList<TSource> : Partitioner.StaticIndexRangePartitioner<TSource, IList<TSource>>
		{
			// Token: 0x06005FB9 RID: 24505 RVA: 0x0013B2C6 File Offset: 0x001394C6
			internal StaticIndexRangePartitionerForIList(IList<TSource> list)
			{
				this._list = list;
			}

			// Token: 0x17001118 RID: 4376
			// (get) Token: 0x06005FBA RID: 24506 RVA: 0x0013B2D5 File Offset: 0x001394D5
			protected override int SourceCount
			{
				get
				{
					return this._list.Count;
				}
			}

			// Token: 0x06005FBB RID: 24507 RVA: 0x0013B2E2 File Offset: 0x001394E2
			protected override IEnumerator<KeyValuePair<long, TSource>> CreatePartition(int startIndex, int endIndex)
			{
				return new Partitioner.StaticIndexRangePartitionForIList<TSource>(this._list, startIndex, endIndex);
			}

			// Token: 0x04003038 RID: 12344
			private IList<TSource> _list;
		}

		// Token: 0x02000A1C RID: 2588
		private class StaticIndexRangePartitionForIList<TSource> : Partitioner.StaticIndexRangePartition<TSource>
		{
			// Token: 0x06005FBC RID: 24508 RVA: 0x0013B2F1 File Offset: 0x001394F1
			internal StaticIndexRangePartitionForIList(IList<TSource> list, int startIndex, int endIndex)
				: base(startIndex, endIndex)
			{
				this._list = list;
			}

			// Token: 0x17001119 RID: 4377
			// (get) Token: 0x06005FBD RID: 24509 RVA: 0x0013B304 File Offset: 0x00139504
			public override KeyValuePair<long, TSource> Current
			{
				get
				{
					if (this._offset < this._startIndex)
					{
						throw new InvalidOperationException("MoveNext must be called at least once before calling Current.");
					}
					return new KeyValuePair<long, TSource>((long)this._offset, this._list[this._offset]);
				}
			}

			// Token: 0x04003039 RID: 12345
			private volatile IList<TSource> _list;
		}

		// Token: 0x02000A1D RID: 2589
		private class StaticIndexRangePartitionerForArray<TSource> : Partitioner.StaticIndexRangePartitioner<TSource, TSource[]>
		{
			// Token: 0x06005FBE RID: 24510 RVA: 0x0013B344 File Offset: 0x00139544
			internal StaticIndexRangePartitionerForArray(TSource[] array)
			{
				this._array = array;
			}

			// Token: 0x1700111A RID: 4378
			// (get) Token: 0x06005FBF RID: 24511 RVA: 0x0013B353 File Offset: 0x00139553
			protected override int SourceCount
			{
				get
				{
					return this._array.Length;
				}
			}

			// Token: 0x06005FC0 RID: 24512 RVA: 0x0013B35D File Offset: 0x0013955D
			protected override IEnumerator<KeyValuePair<long, TSource>> CreatePartition(int startIndex, int endIndex)
			{
				return new Partitioner.StaticIndexRangePartitionForArray<TSource>(this._array, startIndex, endIndex);
			}

			// Token: 0x0400303A RID: 12346
			private TSource[] _array;
		}

		// Token: 0x02000A1E RID: 2590
		private class StaticIndexRangePartitionForArray<TSource> : Partitioner.StaticIndexRangePartition<TSource>
		{
			// Token: 0x06005FC1 RID: 24513 RVA: 0x0013B36C File Offset: 0x0013956C
			internal StaticIndexRangePartitionForArray(TSource[] array, int startIndex, int endIndex)
				: base(startIndex, endIndex)
			{
				this._array = array;
			}

			// Token: 0x1700111B RID: 4379
			// (get) Token: 0x06005FC2 RID: 24514 RVA: 0x0013B37F File Offset: 0x0013957F
			public override KeyValuePair<long, TSource> Current
			{
				get
				{
					if (this._offset < this._startIndex)
					{
						throw new InvalidOperationException("MoveNext must be called at least once before calling Current.");
					}
					return new KeyValuePair<long, TSource>((long)this._offset, this._array[this._offset]);
				}
			}

			// Token: 0x0400303B RID: 12347
			private volatile TSource[] _array;
		}

		// Token: 0x02000A1F RID: 2591
		private class SharedInt
		{
			// Token: 0x06005FC3 RID: 24515 RVA: 0x0013B3BF File Offset: 0x001395BF
			internal SharedInt(int value)
			{
				this.Value = value;
			}

			// Token: 0x0400303C RID: 12348
			internal volatile int Value;
		}

		// Token: 0x02000A20 RID: 2592
		private class SharedBool
		{
			// Token: 0x06005FC4 RID: 24516 RVA: 0x0013B3D0 File Offset: 0x001395D0
			internal SharedBool(bool value)
			{
				this.Value = value;
			}

			// Token: 0x0400303D RID: 12349
			internal volatile bool Value;
		}

		// Token: 0x02000A21 RID: 2593
		private class SharedLong
		{
			// Token: 0x06005FC5 RID: 24517 RVA: 0x0013B3E1 File Offset: 0x001395E1
			internal SharedLong(long value)
			{
				this.Value = value;
			}

			// Token: 0x0400303E RID: 12350
			internal long Value;
		}
	}
}
