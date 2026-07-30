using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace System.Linq.Parallel
{
	// Token: 0x0200011E RID: 286
	internal class OrderPreservingMergeHelper<TInputOutput, TKey> : IMergeHelper<TInputOutput>
	{
		// Token: 0x06000973 RID: 2419 RVA: 0x0001E40F File Offset: 0x0001C60F
		internal OrderPreservingMergeHelper(PartitionedStream<TInputOutput, TKey> partitions, TaskScheduler taskScheduler, CancellationState cancellationState, int queryId)
		{
			this._taskGroupState = new QueryTaskGroupState(cancellationState, queryId);
			this._partitions = partitions;
			this._results = new Shared<TInputOutput[]>(null);
			this._taskScheduler = taskScheduler;
		}

		// Token: 0x06000974 RID: 2420 RVA: 0x0001E43F File Offset: 0x0001C63F
		void IMergeHelper<TInputOutput>.Execute()
		{
			OrderPreservingSpoolingTask<TInputOutput, TKey>.Spool(this._taskGroupState, this._partitions, this._results, this._taskScheduler);
		}

		// Token: 0x06000975 RID: 2421 RVA: 0x0001E45E File Offset: 0x0001C65E
		IEnumerator<TInputOutput> IMergeHelper<TInputOutput>.GetEnumerator()
		{
			return this._results.Value.GetEnumerator();
		}

		// Token: 0x06000976 RID: 2422 RVA: 0x0001E470 File Offset: 0x0001C670
		public TInputOutput[] GetResultsAsArray()
		{
			return this._results.Value;
		}

		// Token: 0x04000577 RID: 1399
		private QueryTaskGroupState _taskGroupState;

		// Token: 0x04000578 RID: 1400
		private PartitionedStream<TInputOutput, TKey> _partitions;

		// Token: 0x04000579 RID: 1401
		private Shared<TInputOutput[]> _results;

		// Token: 0x0400057A RID: 1402
		private TaskScheduler _taskScheduler;
	}
}
