using System;
using System.Collections.Generic;
using System.Security;

namespace System.Threading.Tasks
{
	// Token: 0x02000526 RID: 1318
	internal sealed class ThreadPoolTaskScheduler : TaskScheduler
	{
		// Token: 0x06003C0A RID: 15370 RVA: 0x000D8C4A File Offset: 0x000D6E4A
		internal ThreadPoolTaskScheduler()
		{
			int id = base.Id;
		}

		// Token: 0x06003C0B RID: 15371 RVA: 0x000D8C59 File Offset: 0x000D6E59
		private static void LongRunningThreadWork(object obj)
		{
			(obj as Task).ExecuteEntry(false);
		}

		// Token: 0x06003C0C RID: 15372 RVA: 0x000D8C68 File Offset: 0x000D6E68
		[SecurityCritical]
		protected internal override void QueueTask(Task task)
		{
			if ((task.Options & TaskCreationOptions.LongRunning) != TaskCreationOptions.None)
			{
				new Thread(ThreadPoolTaskScheduler.s_longRunningThreadWork)
				{
					IsBackground = true
				}.Start(task);
				return;
			}
			bool flag = (task.Options & TaskCreationOptions.PreferFairness) > TaskCreationOptions.None;
			ThreadPool.UnsafeQueueCustomWorkItem(task, flag);
		}

		// Token: 0x06003C0D RID: 15373 RVA: 0x000D8CAC File Offset: 0x000D6EAC
		[SecurityCritical]
		protected override bool TryExecuteTaskInline(Task task, bool taskWasPreviouslyQueued)
		{
			if (taskWasPreviouslyQueued && !ThreadPool.TryPopCustomWorkItem(task))
			{
				return false;
			}
			bool flag = false;
			try
			{
				flag = task.ExecuteEntry(false);
			}
			finally
			{
				if (taskWasPreviouslyQueued)
				{
					this.NotifyWorkItemProgress();
				}
			}
			return flag;
		}

		// Token: 0x06003C0E RID: 15374 RVA: 0x000D8CF0 File Offset: 0x000D6EF0
		[SecurityCritical]
		protected internal override bool TryDequeue(Task task)
		{
			return ThreadPool.TryPopCustomWorkItem(task);
		}

		// Token: 0x06003C0F RID: 15375 RVA: 0x000D8CF8 File Offset: 0x000D6EF8
		[SecurityCritical]
		protected override IEnumerable<Task> GetScheduledTasks()
		{
			return this.FilterTasksFromWorkItems(ThreadPool.GetQueuedWorkItems());
		}

		// Token: 0x06003C10 RID: 15376 RVA: 0x000D8D05 File Offset: 0x000D6F05
		private IEnumerable<Task> FilterTasksFromWorkItems(IEnumerable<IThreadPoolWorkItem> tpwItems)
		{
			foreach (IThreadPoolWorkItem threadPoolWorkItem in tpwItems)
			{
				if (threadPoolWorkItem is Task)
				{
					yield return (Task)threadPoolWorkItem;
				}
			}
			IEnumerator<IThreadPoolWorkItem> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06003C11 RID: 15377 RVA: 0x000D8D15 File Offset: 0x000D6F15
		internal override void NotifyWorkItemProgress()
		{
			ThreadPool.NotifyWorkItemProgress();
		}

		// Token: 0x170009D4 RID: 2516
		// (get) Token: 0x06003C12 RID: 15378 RVA: 0x00015ED5 File Offset: 0x000140D5
		internal override bool RequiresAtomicStartTransition
		{
			get
			{
				return false;
			}
		}

		// Token: 0x04001F1C RID: 7964
		private static readonly ParameterizedThreadStart s_longRunningThreadWork = new ParameterizedThreadStart(ThreadPoolTaskScheduler.LongRunningThreadWork);
	}
}
