using System;
using System.Security;

namespace System.Threading.Tasks
{
	// Token: 0x02000511 RID: 1297
	internal abstract class TaskContinuation
	{
		// Token: 0x06003B3B RID: 15163
		internal abstract void Run(Task completedTask, bool bCanInlineContinuationTask);

		// Token: 0x06003B3C RID: 15164 RVA: 0x000D69F4 File Offset: 0x000D4BF4
		[SecuritySafeCritical]
		protected static void InlineIfPossibleOrElseQueue(Task task, bool needsProtection)
		{
			if (needsProtection)
			{
				if (!task.MarkStarted())
				{
					return;
				}
			}
			else
			{
				task.m_stateFlags |= 65536;
			}
			try
			{
				if (!task.m_taskScheduler.TryRunInline(task, false))
				{
					task.m_taskScheduler.InternalQueueTask(task);
				}
			}
			catch (Exception ex)
			{
				if (!(ex is ThreadAbortException) || (task.m_stateFlags & 134217728) == 0)
				{
					TaskSchedulerException ex2 = new TaskSchedulerException(ex);
					task.AddException(ex2);
					task.Finish(false);
				}
			}
		}

		// Token: 0x06003B3D RID: 15165
		internal abstract Delegate[] GetDelegateContinuationsForDebugger();
	}
}
