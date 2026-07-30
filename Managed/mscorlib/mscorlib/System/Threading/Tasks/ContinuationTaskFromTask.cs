using System;

namespace System.Threading.Tasks
{
	// Token: 0x0200050D RID: 1293
	internal sealed class ContinuationTaskFromTask : Task
	{
		// Token: 0x06003B33 RID: 15155 RVA: 0x000D678C File Offset: 0x000D498C
		public ContinuationTaskFromTask(Task antecedent, Delegate action, object state, TaskCreationOptions creationOptions, InternalTaskOptions internalOptions, ref StackCrawlMark stackMark)
			: base(action, state, Task.InternalCurrentIfAttached(creationOptions), default(CancellationToken), creationOptions, internalOptions, null)
		{
			this.m_antecedent = antecedent;
			base.PossiblyCaptureContext(ref stackMark);
		}

		// Token: 0x06003B34 RID: 15156 RVA: 0x000D67C8 File Offset: 0x000D49C8
		internal override void InnerInvoke()
		{
			Task antecedent = this.m_antecedent;
			this.m_antecedent = null;
			antecedent.NotifyDebuggerOfWaitCompletionIfNecessary();
			Action<Task> action = this.m_action as Action<Task>;
			if (action != null)
			{
				action(antecedent);
				return;
			}
			Action<Task, object> action2 = this.m_action as Action<Task, object>;
			if (action2 != null)
			{
				action2(antecedent, this.m_stateObject);
				return;
			}
		}

		// Token: 0x04001EE8 RID: 7912
		private Task m_antecedent;
	}
}
