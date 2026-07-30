using System;
using System.Security;

namespace System.Threading.Tasks
{
	// Token: 0x020004FF RID: 1279
	internal sealed class CompletionActionInvoker : IThreadPoolWorkItem
	{
		// Token: 0x06003AFA RID: 15098 RVA: 0x000D601A File Offset: 0x000D421A
		internal CompletionActionInvoker(ITaskCompletionAction action, Task completingTask)
		{
			this.m_action = action;
			this.m_completingTask = completingTask;
		}

		// Token: 0x06003AFB RID: 15099 RVA: 0x000D6030 File Offset: 0x000D4230
		[SecurityCritical]
		public void ExecuteWorkItem()
		{
			this.m_action.Invoke(this.m_completingTask);
		}

		// Token: 0x06003AFC RID: 15100 RVA: 0x00002194 File Offset: 0x00000394
		[SecurityCritical]
		public void MarkAborted(ThreadAbortException tae)
		{
		}

		// Token: 0x04001EB4 RID: 7860
		private readonly ITaskCompletionAction m_action;

		// Token: 0x04001EB5 RID: 7861
		private readonly Task m_completingTask;
	}
}
