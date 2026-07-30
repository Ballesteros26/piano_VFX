using System;
using System.Threading;
using System.Threading.Tasks;

namespace System.Linq.Parallel
{
	// Token: 0x020001F0 RID: 496
	internal abstract class QueryTask
	{
		// Token: 0x06000C86 RID: 3206 RVA: 0x00029F5E File Offset: 0x0002815E
		protected QueryTask(int taskIndex, QueryTaskGroupState groupState)
		{
			this._taskIndex = taskIndex;
			this._groupState = groupState;
		}

		// Token: 0x06000C87 RID: 3207 RVA: 0x00029F74 File Offset: 0x00028174
		private static void RunTaskSynchronously(object o)
		{
			((QueryTask)o).BaseWork(null);
		}

		// Token: 0x06000C88 RID: 3208 RVA: 0x00029F82 File Offset: 0x00028182
		internal Task RunSynchronously(TaskScheduler taskScheduler)
		{
			Task task = new Task(QueryTask.s_runTaskSynchronouslyDelegate, this, TaskCreationOptions.AttachedToParent);
			task.RunSynchronously(taskScheduler);
			return task;
		}

		// Token: 0x06000C89 RID: 3209 RVA: 0x00029F98 File Offset: 0x00028198
		internal Task RunAsynchronously(TaskScheduler taskScheduler)
		{
			return Task.Factory.StartNew(QueryTask.s_baseWorkDelegate, this, default(CancellationToken), TaskCreationOptions.PreferFairness | TaskCreationOptions.AttachedToParent, taskScheduler);
		}

		// Token: 0x06000C8A RID: 3210 RVA: 0x00029FC0 File Offset: 0x000281C0
		private void BaseWork(object unused)
		{
			PlinqEtwProvider.Log.ParallelQueryFork(this._groupState.QueryId);
			try
			{
				this.Work();
			}
			finally
			{
				PlinqEtwProvider.Log.ParallelQueryJoin(this._groupState.QueryId);
			}
		}

		// Token: 0x06000C8B RID: 3211
		protected abstract void Work();

		// Token: 0x040007C5 RID: 1989
		protected int _taskIndex;

		// Token: 0x040007C6 RID: 1990
		protected QueryTaskGroupState _groupState;

		// Token: 0x040007C7 RID: 1991
		private static Action<object> s_runTaskSynchronouslyDelegate = new Action<object>(QueryTask.RunTaskSynchronously);

		// Token: 0x040007C8 RID: 1992
		private static Action<object> s_baseWorkDelegate = delegate(object o)
		{
			((QueryTask)o).BaseWork(null);
		};
	}
}
