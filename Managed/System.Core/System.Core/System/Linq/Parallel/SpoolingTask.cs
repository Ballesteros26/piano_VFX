using System;
using System.Threading.Tasks;

namespace System.Linq.Parallel
{
	// Token: 0x020001F4 RID: 500
	internal static class SpoolingTask
	{
		// Token: 0x06000C99 RID: 3225 RVA: 0x0002A204 File Offset: 0x00028404
		internal static void SpoolStopAndGo<TInputOutput, TIgnoreKey>(QueryTaskGroupState groupState, PartitionedStream<TInputOutput, TIgnoreKey> partitions, SynchronousChannel<TInputOutput>[] channels, TaskScheduler taskScheduler)
		{
			Task task = new Task(delegate
			{
				int num = partitions.PartitionCount - 1;
				for (int i = 0; i < num; i++)
				{
					new StopAndGoSpoolingTask<TInputOutput, TIgnoreKey>(i, groupState, partitions[i], channels[i]).RunAsynchronously(taskScheduler);
				}
				new StopAndGoSpoolingTask<TInputOutput, TIgnoreKey>(num, groupState, partitions[num], channels[num]).RunSynchronously(taskScheduler);
			});
			groupState.QueryBegin(task);
			task.RunSynchronously(taskScheduler);
			groupState.QueryEnd(false);
		}

		// Token: 0x06000C9A RID: 3226 RVA: 0x0002A26C File Offset: 0x0002846C
		internal static void SpoolPipeline<TInputOutput, TIgnoreKey>(QueryTaskGroupState groupState, PartitionedStream<TInputOutput, TIgnoreKey> partitions, AsynchronousChannel<TInputOutput>[] channels, TaskScheduler taskScheduler)
		{
			Task task = new Task(delegate
			{
				for (int i = 0; i < partitions.PartitionCount; i++)
				{
					new PipelineSpoolingTask<TInputOutput, TIgnoreKey>(i, groupState, partitions[i], channels[i]).RunAsynchronously(taskScheduler);
				}
			});
			groupState.QueryBegin(task);
			task.Start(taskScheduler);
		}

		// Token: 0x06000C9B RID: 3227 RVA: 0x0002A2C8 File Offset: 0x000284C8
		internal static void SpoolForAll<TInputOutput, TIgnoreKey>(QueryTaskGroupState groupState, PartitionedStream<TInputOutput, TIgnoreKey> partitions, TaskScheduler taskScheduler)
		{
			Task task = new Task(delegate
			{
				int num = partitions.PartitionCount - 1;
				for (int i = 0; i < num; i++)
				{
					new ForAllSpoolingTask<TInputOutput, TIgnoreKey>(i, groupState, partitions[i]).RunAsynchronously(taskScheduler);
				}
				new ForAllSpoolingTask<TInputOutput, TIgnoreKey>(num, groupState, partitions[num]).RunSynchronously(taskScheduler);
			});
			groupState.QueryBegin(task);
			task.RunSynchronously(taskScheduler);
			groupState.QueryEnd(false);
		}
	}
}
