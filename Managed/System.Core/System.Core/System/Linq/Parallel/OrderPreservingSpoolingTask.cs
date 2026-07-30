using System;
using System.Threading.Tasks;

namespace System.Linq.Parallel
{
	// Token: 0x020001ED RID: 493
	internal class OrderPreservingSpoolingTask<TInputOutput, TKey> : SpoolingTaskBase
	{
		// Token: 0x06000C7F RID: 3199 RVA: 0x00029DCC File Offset: 0x00027FCC
		private OrderPreservingSpoolingTask(int taskIndex, QueryTaskGroupState groupState, Shared<TInputOutput[]> results, SortHelper<TInputOutput> sortHelper)
			: base(taskIndex, groupState)
		{
			this._results = results;
			this._sortHelper = sortHelper;
		}

		// Token: 0x06000C80 RID: 3200 RVA: 0x00029DE8 File Offset: 0x00027FE8
		internal static void Spool(QueryTaskGroupState groupState, PartitionedStream<TInputOutput, TKey> partitions, Shared<TInputOutput[]> results, TaskScheduler taskScheduler)
		{
			int maxToRunInParallel = partitions.PartitionCount - 1;
			SortHelper<TInputOutput, TKey>[] sortHelpers = SortHelper<TInputOutput, TKey>.GenerateSortHelpers(partitions, groupState);
			Task task = new Task(delegate
			{
				for (int j = 0; j < maxToRunInParallel; j++)
				{
					new OrderPreservingSpoolingTask<TInputOutput, TKey>(j, groupState, results, sortHelpers[j]).RunAsynchronously(taskScheduler);
				}
				new OrderPreservingSpoolingTask<TInputOutput, TKey>(maxToRunInParallel, groupState, results, sortHelpers[maxToRunInParallel]).RunSynchronously(taskScheduler);
			});
			groupState.QueryBegin(task);
			task.RunSynchronously(taskScheduler);
			for (int i = 0; i < sortHelpers.Length; i++)
			{
				sortHelpers[i].Dispose();
			}
			groupState.QueryEnd(false);
		}

		// Token: 0x06000C81 RID: 3201 RVA: 0x00029E88 File Offset: 0x00028088
		protected override void SpoolingWork()
		{
			TInputOutput[] array = this._sortHelper.Sort();
			if (!this._groupState.CancellationState.MergedCancellationToken.IsCancellationRequested && this._taskIndex == 0)
			{
				this._results.Value = array;
			}
		}

		// Token: 0x040007BE RID: 1982
		private Shared<TInputOutput[]> _results;

		// Token: 0x040007BF RID: 1983
		private SortHelper<TInputOutput> _sortHelper;
	}
}
