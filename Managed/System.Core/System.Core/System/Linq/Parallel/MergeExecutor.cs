using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace System.Linq.Parallel
{
	// Token: 0x0200011D RID: 285
	internal class MergeExecutor<TInputOutput> : IEnumerable<TInputOutput>, IEnumerable
	{
		// Token: 0x0600096B RID: 2411 RVA: 0x00002320 File Offset: 0x00000520
		private MergeExecutor()
		{
		}

		// Token: 0x0600096C RID: 2412 RVA: 0x0001E2E4 File Offset: 0x0001C4E4
		internal static MergeExecutor<TInputOutput> Execute<TKey>(PartitionedStream<TInputOutput, TKey> partitions, bool ignoreOutput, ParallelMergeOptions options, TaskScheduler taskScheduler, bool isOrdered, CancellationState cancellationState, int queryId)
		{
			MergeExecutor<TInputOutput> mergeExecutor = new MergeExecutor<TInputOutput>();
			if (isOrdered && !ignoreOutput)
			{
				if (options != ParallelMergeOptions.FullyBuffered && !partitions.OrdinalIndexState.IsWorseThan(OrdinalIndexState.Increasing))
				{
					bool flag = options == ParallelMergeOptions.AutoBuffered;
					if (partitions.PartitionCount > 1)
					{
						mergeExecutor._mergeHelper = new OrderPreservingPipeliningMergeHelper<TInputOutput, TKey>(partitions, taskScheduler, cancellationState, flag, queryId, partitions.KeyComparer);
					}
					else
					{
						mergeExecutor._mergeHelper = new DefaultMergeHelper<TInputOutput, TKey>(partitions, false, options, taskScheduler, cancellationState, queryId);
					}
				}
				else
				{
					mergeExecutor._mergeHelper = new OrderPreservingMergeHelper<TInputOutput, TKey>(partitions, taskScheduler, cancellationState, queryId);
				}
			}
			else
			{
				mergeExecutor._mergeHelper = new DefaultMergeHelper<TInputOutput, TKey>(partitions, ignoreOutput, options, taskScheduler, cancellationState, queryId);
			}
			mergeExecutor.Execute();
			return mergeExecutor;
		}

		// Token: 0x0600096D RID: 2413 RVA: 0x0001E37A File Offset: 0x0001C57A
		private void Execute()
		{
			this._mergeHelper.Execute();
		}

		// Token: 0x0600096E RID: 2414 RVA: 0x0001E387 File Offset: 0x0001C587
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x0600096F RID: 2415 RVA: 0x0001E38F File Offset: 0x0001C58F
		public IEnumerator<TInputOutput> GetEnumerator()
		{
			return this._mergeHelper.GetEnumerator();
		}

		// Token: 0x06000970 RID: 2416 RVA: 0x0001E39C File Offset: 0x0001C59C
		internal TInputOutput[] GetResultsAsArray()
		{
			return this._mergeHelper.GetResultsAsArray();
		}

		// Token: 0x06000971 RID: 2417 RVA: 0x0001E3AC File Offset: 0x0001C5AC
		internal static AsynchronousChannel<TInputOutput>[] MakeAsynchronousChannels(int partitionCount, ParallelMergeOptions options, IntValueEvent consumerEvent, CancellationToken cancellationToken)
		{
			AsynchronousChannel<TInputOutput>[] array = new AsynchronousChannel<TInputOutput>[partitionCount];
			int num = 0;
			if (options == ParallelMergeOptions.NotBuffered)
			{
				num = 1;
			}
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = new AsynchronousChannel<TInputOutput>(i, num, cancellationToken, consumerEvent);
			}
			return array;
		}

		// Token: 0x06000972 RID: 2418 RVA: 0x0001E3E4 File Offset: 0x0001C5E4
		internal static SynchronousChannel<TInputOutput>[] MakeSynchronousChannels(int partitionCount)
		{
			SynchronousChannel<TInputOutput>[] array = new SynchronousChannel<TInputOutput>[partitionCount];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = new SynchronousChannel<TInputOutput>();
			}
			return array;
		}

		// Token: 0x04000576 RID: 1398
		private IMergeHelper<TInputOutput> _mergeHelper;
	}
}
