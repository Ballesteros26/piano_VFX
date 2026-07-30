using System;

namespace System.Threading.Tasks
{
	// Token: 0x02000510 RID: 1296
	internal sealed class ContinuationResultTaskFromResultTask<TAntecedentResult, TResult> : Task<TResult>
	{
		// Token: 0x06003B39 RID: 15161 RVA: 0x000D6954 File Offset: 0x000D4B54
		public ContinuationResultTaskFromResultTask(Task<TAntecedentResult> antecedent, Delegate function, object state, TaskCreationOptions creationOptions, InternalTaskOptions internalOptions, ref StackCrawlMark stackMark)
			: base(function, state, Task.InternalCurrentIfAttached(creationOptions), default(CancellationToken), creationOptions, internalOptions, null)
		{
			this.m_antecedent = antecedent;
			base.PossiblyCaptureContext(ref stackMark);
		}

		// Token: 0x06003B3A RID: 15162 RVA: 0x000D6990 File Offset: 0x000D4B90
		internal override void InnerInvoke()
		{
			Task<TAntecedentResult> antecedent = this.m_antecedent;
			this.m_antecedent = null;
			antecedent.NotifyDebuggerOfWaitCompletionIfNecessary();
			Func<Task<TAntecedentResult>, TResult> func = this.m_action as Func<Task<TAntecedentResult>, TResult>;
			if (func != null)
			{
				this.m_result = func(antecedent);
				return;
			}
			Func<Task<TAntecedentResult>, object, TResult> func2 = this.m_action as Func<Task<TAntecedentResult>, object, TResult>;
			if (func2 != null)
			{
				this.m_result = func2(antecedent, this.m_stateObject);
				return;
			}
		}

		// Token: 0x04001EEB RID: 7915
		private Task<TAntecedentResult> m_antecedent;
	}
}
