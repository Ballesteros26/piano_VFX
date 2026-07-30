using System;

namespace System.Threading.Tasks
{
	// Token: 0x0200050F RID: 1295
	internal sealed class ContinuationTaskFromResultTask<TAntecedentResult> : Task
	{
		// Token: 0x06003B37 RID: 15159 RVA: 0x000D68C0 File Offset: 0x000D4AC0
		public ContinuationTaskFromResultTask(Task<TAntecedentResult> antecedent, Delegate action, object state, TaskCreationOptions creationOptions, InternalTaskOptions internalOptions, ref StackCrawlMark stackMark)
			: base(action, state, Task.InternalCurrentIfAttached(creationOptions), default(CancellationToken), creationOptions, internalOptions, null)
		{
			this.m_antecedent = antecedent;
			base.PossiblyCaptureContext(ref stackMark);
		}

		// Token: 0x06003B38 RID: 15160 RVA: 0x000D68FC File Offset: 0x000D4AFC
		internal override void InnerInvoke()
		{
			Task<TAntecedentResult> antecedent = this.m_antecedent;
			this.m_antecedent = null;
			antecedent.NotifyDebuggerOfWaitCompletionIfNecessary();
			Action<Task<TAntecedentResult>> action = this.m_action as Action<Task<TAntecedentResult>>;
			if (action != null)
			{
				action(antecedent);
				return;
			}
			Action<Task<TAntecedentResult>, object> action2 = this.m_action as Action<Task<TAntecedentResult>, object>;
			if (action2 != null)
			{
				action2(antecedent, this.m_stateObject);
				return;
			}
		}

		// Token: 0x04001EEA RID: 7914
		private Task<TAntecedentResult> m_antecedent;
	}
}
