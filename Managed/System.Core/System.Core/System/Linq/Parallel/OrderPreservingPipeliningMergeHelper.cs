using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

namespace System.Linq.Parallel
{
	// Token: 0x0200011F RID: 287
	internal class OrderPreservingPipeliningMergeHelper<TOutput, TKey> : IMergeHelper<TOutput>
	{
		// Token: 0x06000977 RID: 2423 RVA: 0x0001E480 File Offset: 0x0001C680
		internal OrderPreservingPipeliningMergeHelper(PartitionedStream<TOutput, TKey> partitions, TaskScheduler taskScheduler, CancellationState cancellationState, bool autoBuffered, int queryId, IComparer<TKey> keyComparer)
		{
			this._taskGroupState = new QueryTaskGroupState(cancellationState, queryId);
			this._partitions = partitions;
			this._taskScheduler = taskScheduler;
			this._autoBuffered = autoBuffered;
			int partitionCount = this._partitions.PartitionCount;
			this._buffers = new Queue<Pair<TKey, TOutput>>[partitionCount];
			this._producerDone = new bool[partitionCount];
			this._consumerWaiting = new bool[partitionCount];
			this._producerWaiting = new bool[partitionCount];
			this._bufferLocks = new object[partitionCount];
			if (keyComparer == Util.GetDefaultComparer<int>())
			{
				this._producerComparer = (IComparer<Producer<TKey>>)new ProducerComparerInt();
				return;
			}
			this._producerComparer = new OrderPreservingPipeliningMergeHelper<TOutput, TKey>.ProducerComparer(keyComparer);
		}

		// Token: 0x06000978 RID: 2424 RVA: 0x0001E528 File Offset: 0x0001C728
		void IMergeHelper<TOutput>.Execute()
		{
			OrderPreservingPipeliningSpoolingTask<TOutput, TKey>.Spool(this._taskGroupState, this._partitions, this._consumerWaiting, this._producerWaiting, this._producerDone, this._buffers, this._bufferLocks, this._taskScheduler, this._autoBuffered);
		}

		// Token: 0x06000979 RID: 2425 RVA: 0x0001E570 File Offset: 0x0001C770
		IEnumerator<TOutput> IMergeHelper<TOutput>.GetEnumerator()
		{
			return new OrderPreservingPipeliningMergeHelper<TOutput, TKey>.OrderedPipeliningMergeEnumerator(this, this._producerComparer);
		}

		// Token: 0x0600097A RID: 2426 RVA: 0x0001E57E File Offset: 0x0001C77E
		[ExcludeFromCodeCoverage]
		public TOutput[] GetResultsAsArray()
		{
			throw new InvalidOperationException();
		}

		// Token: 0x0400057B RID: 1403
		private readonly QueryTaskGroupState _taskGroupState;

		// Token: 0x0400057C RID: 1404
		private readonly PartitionedStream<TOutput, TKey> _partitions;

		// Token: 0x0400057D RID: 1405
		private readonly TaskScheduler _taskScheduler;

		// Token: 0x0400057E RID: 1406
		private readonly bool _autoBuffered;

		// Token: 0x0400057F RID: 1407
		private readonly Queue<Pair<TKey, TOutput>>[] _buffers;

		// Token: 0x04000580 RID: 1408
		private readonly bool[] _producerDone;

		// Token: 0x04000581 RID: 1409
		private readonly bool[] _producerWaiting;

		// Token: 0x04000582 RID: 1410
		private readonly bool[] _consumerWaiting;

		// Token: 0x04000583 RID: 1411
		private readonly object[] _bufferLocks;

		// Token: 0x04000584 RID: 1412
		private IComparer<Producer<TKey>> _producerComparer;

		// Token: 0x04000585 RID: 1413
		internal const int INITIAL_BUFFER_SIZE = 128;

		// Token: 0x04000586 RID: 1414
		internal const int STEAL_BUFFER_SIZE = 1024;

		// Token: 0x04000587 RID: 1415
		internal const int MAX_BUFFER_SIZE = 8192;

		// Token: 0x02000120 RID: 288
		private class ProducerComparer : IComparer<Producer<TKey>>
		{
			// Token: 0x0600097B RID: 2427 RVA: 0x0001E585 File Offset: 0x0001C785
			internal ProducerComparer(IComparer<TKey> keyComparer)
			{
				this._keyComparer = keyComparer;
			}

			// Token: 0x0600097C RID: 2428 RVA: 0x0001E594 File Offset: 0x0001C794
			public int Compare(Producer<TKey> x, Producer<TKey> y)
			{
				return this._keyComparer.Compare(y.MaxKey, x.MaxKey);
			}

			// Token: 0x04000588 RID: 1416
			private IComparer<TKey> _keyComparer;
		}

		// Token: 0x02000121 RID: 289
		private class OrderedPipeliningMergeEnumerator : MergeEnumerator<TOutput>
		{
			// Token: 0x0600097D RID: 2429 RVA: 0x0001E5B0 File Offset: 0x0001C7B0
			internal OrderedPipeliningMergeEnumerator(OrderPreservingPipeliningMergeHelper<TOutput, TKey> mergeHelper, IComparer<Producer<TKey>> producerComparer)
				: base(mergeHelper._taskGroupState)
			{
				int partitionCount = mergeHelper._partitions.PartitionCount;
				this._mergeHelper = mergeHelper;
				this._producerHeap = new FixedMaxHeap<Producer<TKey>>(partitionCount, producerComparer);
				this._privateBuffer = new Queue<Pair<TKey, TOutput>>[partitionCount];
				this._producerNextElement = new TOutput[partitionCount];
			}

			// Token: 0x17000135 RID: 309
			// (get) Token: 0x0600097E RID: 2430 RVA: 0x0001E604 File Offset: 0x0001C804
			public override TOutput Current
			{
				get
				{
					int producerIndex = this._producerHeap.MaxValue.ProducerIndex;
					return this._producerNextElement[producerIndex];
				}
			}

			// Token: 0x0600097F RID: 2431 RVA: 0x0001E630 File Offset: 0x0001C830
			public override bool MoveNext()
			{
				if (!this._initialized)
				{
					this._initialized = true;
					for (int i = 0; i < this._mergeHelper._partitions.PartitionCount; i++)
					{
						Pair<TKey, TOutput> pair = default(Pair<TKey, TOutput>);
						if (this.TryWaitForElement(i, ref pair))
						{
							this._producerHeap.Insert(new Producer<TKey>(pair.First, i));
							this._producerNextElement[i] = pair.Second;
						}
						else
						{
							this.ThrowIfInTearDown();
						}
					}
				}
				else
				{
					if (this._producerHeap.Count == 0)
					{
						return false;
					}
					int producerIndex = this._producerHeap.MaxValue.ProducerIndex;
					Pair<TKey, TOutput> pair2 = default(Pair<TKey, TOutput>);
					if (this.TryGetPrivateElement(producerIndex, ref pair2) || this.TryWaitForElement(producerIndex, ref pair2))
					{
						this._producerHeap.ReplaceMax(new Producer<TKey>(pair2.First, producerIndex));
						this._producerNextElement[producerIndex] = pair2.Second;
					}
					else
					{
						this.ThrowIfInTearDown();
						this._producerHeap.RemoveMax();
					}
				}
				return this._producerHeap.Count > 0;
			}

			// Token: 0x06000980 RID: 2432 RVA: 0x0001E73C File Offset: 0x0001C93C
			private void ThrowIfInTearDown()
			{
				if (this._mergeHelper._taskGroupState.CancellationState.MergedCancellationToken.IsCancellationRequested)
				{
					try
					{
						object[] bufferLocks = this._mergeHelper._bufferLocks;
						for (int i = 0; i < bufferLocks.Length; i++)
						{
							object obj = bufferLocks[i];
							lock (obj)
							{
								Monitor.Pulse(bufferLocks[i]);
							}
						}
						this._taskGroupState.QueryEnd(false);
					}
					finally
					{
						this._producerHeap.Clear();
					}
				}
			}

			// Token: 0x06000981 RID: 2433 RVA: 0x0001E7DC File Offset: 0x0001C9DC
			private bool TryWaitForElement(int producer, ref Pair<TKey, TOutput> element)
			{
				Queue<Pair<TKey, TOutput>> queue = this._mergeHelper._buffers[producer];
				object obj = this._mergeHelper._bufferLocks[producer];
				object obj2 = obj;
				lock (obj2)
				{
					if (queue.Count == 0)
					{
						if (this._mergeHelper._producerDone[producer])
						{
							element = default(Pair<TKey, TOutput>);
							return false;
						}
						this._mergeHelper._consumerWaiting[producer] = true;
						Monitor.Wait(obj);
						if (queue.Count == 0)
						{
							element = default(Pair<TKey, TOutput>);
							return false;
						}
					}
					if (this._mergeHelper._producerWaiting[producer])
					{
						Monitor.Pulse(obj);
						this._mergeHelper._producerWaiting[producer] = false;
					}
					if (queue.Count < 1024)
					{
						element = queue.Dequeue();
						return true;
					}
					this._privateBuffer[producer] = this._mergeHelper._buffers[producer];
					this._mergeHelper._buffers[producer] = new Queue<Pair<TKey, TOutput>>(128);
				}
				this.TryGetPrivateElement(producer, ref element);
				return true;
			}

			// Token: 0x06000982 RID: 2434 RVA: 0x0001E8FC File Offset: 0x0001CAFC
			private bool TryGetPrivateElement(int producer, ref Pair<TKey, TOutput> element)
			{
				Queue<Pair<TKey, TOutput>> queue = this._privateBuffer[producer];
				if (queue != null)
				{
					if (queue.Count > 0)
					{
						element = queue.Dequeue();
						return true;
					}
					this._privateBuffer[producer] = null;
				}
				return false;
			}

			// Token: 0x06000983 RID: 2435 RVA: 0x0001E938 File Offset: 0x0001CB38
			public override void Dispose()
			{
				int num = this._mergeHelper._buffers.Length;
				for (int i = 0; i < num; i++)
				{
					object obj = this._mergeHelper._bufferLocks[i];
					object obj2 = obj;
					lock (obj2)
					{
						if (this._mergeHelper._producerWaiting[i])
						{
							Monitor.Pulse(obj);
						}
					}
				}
				base.Dispose();
			}

			// Token: 0x04000589 RID: 1417
			private OrderPreservingPipeliningMergeHelper<TOutput, TKey> _mergeHelper;

			// Token: 0x0400058A RID: 1418
			private readonly FixedMaxHeap<Producer<TKey>> _producerHeap;

			// Token: 0x0400058B RID: 1419
			private readonly TOutput[] _producerNextElement;

			// Token: 0x0400058C RID: 1420
			private readonly Queue<Pair<TKey, TOutput>>[] _privateBuffer;

			// Token: 0x0400058D RID: 1421
			private bool _initialized;
		}
	}
}
