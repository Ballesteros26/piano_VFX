using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace System.Linq.Parallel
{
	// Token: 0x0200011A RID: 282
	internal class DefaultMergeHelper<TInputOutput, TIgnoreKey> : IMergeHelper<TInputOutput>
	{
		// Token: 0x0600095E RID: 2398 RVA: 0x0001E090 File Offset: 0x0001C290
		internal DefaultMergeHelper(PartitionedStream<TInputOutput, TIgnoreKey> partitions, bool ignoreOutput, ParallelMergeOptions options, TaskScheduler taskScheduler, CancellationState cancellationState, int queryId)
		{
			this._taskGroupState = new QueryTaskGroupState(cancellationState, queryId);
			this._partitions = partitions;
			this._taskScheduler = taskScheduler;
			this._ignoreOutput = ignoreOutput;
			IntValueEvent intValueEvent = new IntValueEvent();
			if (!ignoreOutput)
			{
				if (options != ParallelMergeOptions.FullyBuffered)
				{
					if (partitions.PartitionCount > 1)
					{
						this._asyncChannels = MergeExecutor<TInputOutput>.MakeAsynchronousChannels(partitions.PartitionCount, options, intValueEvent, cancellationState.MergedCancellationToken);
						this._channelEnumerator = new AsynchronousChannelMergeEnumerator<TInputOutput>(this._taskGroupState, this._asyncChannels, intValueEvent);
						return;
					}
					this._channelEnumerator = ExceptionAggregator.WrapQueryEnumerator<TInputOutput, TIgnoreKey>(partitions[0], this._taskGroupState.CancellationState).GetEnumerator();
					return;
				}
				else
				{
					this._syncChannels = MergeExecutor<TInputOutput>.MakeSynchronousChannels(partitions.PartitionCount);
					this._channelEnumerator = new SynchronousChannelMergeEnumerator<TInputOutput>(this._taskGroupState, this._syncChannels);
				}
			}
		}

		// Token: 0x0600095F RID: 2399 RVA: 0x0001E160 File Offset: 0x0001C360
		void IMergeHelper<TInputOutput>.Execute()
		{
			if (this._asyncChannels != null)
			{
				SpoolingTask.SpoolPipeline<TInputOutput, TIgnoreKey>(this._taskGroupState, this._partitions, this._asyncChannels, this._taskScheduler);
				return;
			}
			if (this._syncChannels != null)
			{
				SpoolingTask.SpoolStopAndGo<TInputOutput, TIgnoreKey>(this._taskGroupState, this._partitions, this._syncChannels, this._taskScheduler);
				return;
			}
			if (this._ignoreOutput)
			{
				SpoolingTask.SpoolForAll<TInputOutput, TIgnoreKey>(this._taskGroupState, this._partitions, this._taskScheduler);
			}
		}

		// Token: 0x06000960 RID: 2400 RVA: 0x0001E1D8 File Offset: 0x0001C3D8
		IEnumerator<TInputOutput> IMergeHelper<TInputOutput>.GetEnumerator()
		{
			return this._channelEnumerator;
		}

		// Token: 0x06000961 RID: 2401 RVA: 0x0001E1E0 File Offset: 0x0001C3E0
		public TInputOutput[] GetResultsAsArray()
		{
			if (this._syncChannels != null)
			{
				int num = 0;
				for (int i = 0; i < this._syncChannels.Length; i++)
				{
					num += this._syncChannels[i].Count;
				}
				TInputOutput[] array = new TInputOutput[num];
				int num2 = 0;
				for (int j = 0; j < this._syncChannels.Length; j++)
				{
					this._syncChannels[j].CopyTo(array, num2);
					num2 += this._syncChannels[j].Count;
				}
				return array;
			}
			List<TInputOutput> list = new List<TInputOutput>();
			foreach (TInputOutput tinputOutput in ((IMergeHelper<TInputOutput>)this))
			{
				list.Add(tinputOutput);
			}
			return list.ToArray();
		}

		// Token: 0x0400056E RID: 1390
		private QueryTaskGroupState _taskGroupState;

		// Token: 0x0400056F RID: 1391
		private PartitionedStream<TInputOutput, TIgnoreKey> _partitions;

		// Token: 0x04000570 RID: 1392
		private AsynchronousChannel<TInputOutput>[] _asyncChannels;

		// Token: 0x04000571 RID: 1393
		private SynchronousChannel<TInputOutput>[] _syncChannels;

		// Token: 0x04000572 RID: 1394
		private IEnumerator<TInputOutput> _channelEnumerator;

		// Token: 0x04000573 RID: 1395
		private TaskScheduler _taskScheduler;

		// Token: 0x04000574 RID: 1396
		private bool _ignoreOutput;
	}
}
