using System;
using System.Collections.Generic;
using System.Security;

namespace System.Threading.Tasks
{
	// Token: 0x02000520 RID: 1312
	internal sealed class SynchronizationContextTaskScheduler : TaskScheduler
	{
		// Token: 0x06003BEF RID: 15343 RVA: 0x000D8A08 File Offset: 0x000D6C08
		internal SynchronizationContextTaskScheduler()
		{
			SynchronizationContext synchronizationContext = SynchronizationContext.Current;
			if (synchronizationContext == null)
			{
				throw new InvalidOperationException(Environment.GetResourceString("The current SynchronizationContext may not be used as a TaskScheduler."));
			}
			this.m_synchronizationContext = synchronizationContext;
		}

		// Token: 0x06003BF0 RID: 15344 RVA: 0x000D8A3B File Offset: 0x000D6C3B
		[SecurityCritical]
		protected internal override void QueueTask(Task task)
		{
			this.m_synchronizationContext.Post(SynchronizationContextTaskScheduler.s_postCallback, task);
		}

		// Token: 0x06003BF1 RID: 15345 RVA: 0x000D8A4E File Offset: 0x000D6C4E
		[SecurityCritical]
		protected override bool TryExecuteTaskInline(Task task, bool taskWasPreviouslyQueued)
		{
			return SynchronizationContext.Current == this.m_synchronizationContext && base.TryExecuteTask(task);
		}

		// Token: 0x06003BF2 RID: 15346 RVA: 0x0000A42E File Offset: 0x0000862E
		[SecurityCritical]
		protected override IEnumerable<Task> GetScheduledTasks()
		{
			return null;
		}

		// Token: 0x170009CD RID: 2509
		// (get) Token: 0x06003BF3 RID: 15347 RVA: 0x00003B29 File Offset: 0x00001D29
		public override int MaximumConcurrencyLevel
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x06003BF4 RID: 15348 RVA: 0x000D8A66 File Offset: 0x000D6C66
		private static void PostCallback(object obj)
		{
			((Task)obj).ExecuteEntry(true);
		}

		// Token: 0x04001F13 RID: 7955
		private SynchronizationContext m_synchronizationContext;

		// Token: 0x04001F14 RID: 7956
		private static SendOrPostCallback s_postCallback = new SendOrPostCallback(SynchronizationContextTaskScheduler.PostCallback);
	}
}
