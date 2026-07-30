using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace System.Linq.Parallel
{
	// Token: 0x020001EB RID: 491
	internal class OrderPreservingPipeliningSpoolingTask<TOutput, TKey> : SpoolingTaskBase
	{
		// Token: 0x06000C79 RID: 3193 RVA: 0x00029A70 File Offset: 0x00027C70
		internal OrderPreservingPipeliningSpoolingTask(QueryOperatorEnumerator<TOutput, TKey> partition, QueryTaskGroupState taskGroupState, bool[] consumerWaiting, bool[] producerWaiting, bool[] producerDone, int partitionIndex, Queue<Pair<TKey, TOutput>>[] buffers, object bufferLock, bool autoBuffered)
			: base(partitionIndex, taskGroupState)
		{
			this._partition = partition;
			this._taskGroupState = taskGroupState;
			this._producerDone = producerDone;
			this._consumerWaiting = consumerWaiting;
			this._producerWaiting = producerWaiting;
			this._partitionIndex = partitionIndex;
			this._buffers = buffers;
			this._bufferLock = bufferLock;
			this._autoBuffered = autoBuffered;
		}

		// Token: 0x06000C7A RID: 3194 RVA: 0x00029ACC File Offset: 0x00027CCC
		protected override void SpoolingWork()
		{
			TOutput toutput = default(TOutput);
			TKey tkey = default(TKey);
			int num = (this._autoBuffered ? 16 : 1);
			Pair<TKey, TOutput>[] array = new Pair<TKey, TOutput>[num];
			QueryOperatorEnumerator<TOutput, TKey> partition = this._partition;
			CancellationToken mergedCancellationToken = this._taskGroupState.CancellationState.MergedCancellationToken;
			int num2;
			do
			{
				num2 = 0;
				while (num2 < num && partition.MoveNext(ref toutput, ref tkey))
				{
					array[num2] = new Pair<TKey, TOutput>(tkey, toutput);
					num2++;
				}
				if (num2 == 0)
				{
					break;
				}
				object bufferLock = this._bufferLock;
				lock (bufferLock)
				{
					if (mergedCancellationToken.IsCancellationRequested)
					{
						break;
					}
					for (int i = 0; i < num2; i++)
					{
						this._buffers[this._partitionIndex].Enqueue(array[i]);
					}
					if (this._consumerWaiting[this._partitionIndex])
					{
						Monitor.Pulse(this._bufferLock);
						this._consumerWaiting[this._partitionIndex] = false;
					}
					if (this._buffers[this._partitionIndex].Count >= 8192)
					{
						this._producerWaiting[this._partitionIndex] = true;
						Monitor.Wait(this._bufferLock);
					}
				}
			}
			while (num2 == num);
		}

		// Token: 0x06000C7B RID: 3195 RVA: 0x00029C1C File Offset: 0x00027E1C
		public static void Spool(QueryTaskGroupState groupState, PartitionedStream<TOutput, TKey> partitions, bool[] consumerWaiting, bool[] producerWaiting, bool[] producerDone, Queue<Pair<TKey, TOutput>>[] buffers, object[] bufferLocks, TaskScheduler taskScheduler, bool autoBuffered)
		{
			int degreeOfParallelism = partitions.PartitionCount;
			for (int i = 0; i < degreeOfParallelism; i++)
			{
				buffers[i] = new Queue<Pair<TKey, TOutput>>(128);
				bufferLocks[i] = new object();
			}
			Task task = new Task(delegate
			{
				for (int j = 0; j < degreeOfParallelism; j++)
				{
					new OrderPreservingPipeliningSpoolingTask<TOutput, TKey>(partitions[j], groupState, consumerWaiting, producerWaiting, producerDone, j, buffers, bufferLocks[j], autoBuffered).RunAsynchronously(taskScheduler);
				}
			});
			groupState.QueryBegin(task);
			task.Start(taskScheduler);
		}

		// Token: 0x06000C7C RID: 3196 RVA: 0x00029CE0 File Offset: 0x00027EE0
		protected override void SpoolingFinally()
		{
			object bufferLock = this._bufferLock;
			lock (bufferLock)
			{
				this._producerDone[this._partitionIndex] = true;
				if (this._consumerWaiting[this._partitionIndex])
				{
					Monitor.Pulse(this._bufferLock);
					this._consumerWaiting[this._partitionIndex] = false;
				}
			}
			base.SpoolingFinally();
			this._partition.Dispose();
		}

		// Token: 0x040007AA RID: 1962
		private readonly QueryTaskGroupState _taskGroupState;

		// Token: 0x040007AB RID: 1963
		private readonly QueryOperatorEnumerator<TOutput, TKey> _partition;

		// Token: 0x040007AC RID: 1964
		private readonly bool[] _consumerWaiting;

		// Token: 0x040007AD RID: 1965
		private readonly bool[] _producerWaiting;

		// Token: 0x040007AE RID: 1966
		private readonly bool[] _producerDone;

		// Token: 0x040007AF RID: 1967
		private readonly int _partitionIndex;

		// Token: 0x040007B0 RID: 1968
		private readonly Queue<Pair<TKey, TOutput>>[] _buffers;

		// Token: 0x040007B1 RID: 1969
		private readonly object _bufferLock;

		// Token: 0x040007B2 RID: 1970
		private readonly bool _autoBuffered;

		// Token: 0x040007B3 RID: 1971
		private const int PRODUCER_BUFFER_AUTO_SIZE = 16;
	}
}
