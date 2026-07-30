using System;
using System.Security;

namespace System.Threading.Tasks
{
	// Token: 0x02000515 RID: 1301
	internal sealed class TaskSchedulerAwaitTaskContinuation : AwaitTaskContinuation
	{
		// Token: 0x06003B4A RID: 15178 RVA: 0x000D6CB2 File Offset: 0x000D4EB2
		[SecurityCritical]
		internal TaskSchedulerAwaitTaskContinuation(TaskScheduler scheduler, Action action, bool flowExecutionContext, ref StackCrawlMark stackMark)
			: base(action, flowExecutionContext, ref stackMark)
		{
			this.m_scheduler = scheduler;
		}

		// Token: 0x06003B4B RID: 15179 RVA: 0x000D6CC8 File Offset: 0x000D4EC8
		internal sealed override void Run(Task ignored, bool canInlineContinuationTask)
		{
			if (this.m_scheduler == TaskScheduler.Default)
			{
				base.Run(ignored, canInlineContinuationTask);
				return;
			}
			bool flag = canInlineContinuationTask && (TaskScheduler.InternalCurrent == this.m_scheduler || Thread.CurrentThread.IsThreadPoolThread);
			Task task = base.CreateTask(delegate(object state)
			{
				try
				{
					((Action)state)();
				}
				catch (Exception ex)
				{
					AwaitTaskContinuation.ThrowAsyncIfNecessary(ex);
				}
			}, this.m_action, this.m_scheduler);
			if (flag)
			{
				TaskContinuation.InlineIfPossibleOrElseQueue(task, false);
				return;
			}
			try
			{
				task.ScheduleAndStart(false);
			}
			catch (TaskSchedulerException)
			{
			}
		}

		// Token: 0x04001EF3 RID: 7923
		private readonly TaskScheduler m_scheduler;
	}
}
