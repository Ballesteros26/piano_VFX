using System;

namespace System.Threading.Tasks
{
	// Token: 0x0200050E RID: 1294
	internal sealed class ContinuationResultTaskFromTask<TResult> : Task<TResult>
	{
		// Token: 0x06003B35 RID: 15157 RVA: 0x000D6820 File Offset: 0x000D4A20
		public ContinuationResultTaskFromTask(Task antecedent, Delegate function, object state, TaskCreationOptions creationOptions, InternalTaskOptions internalOptions, ref StackCrawlMark stackMark)
			: base(function, state, Task.InternalCurrentIfAttached(creationOptions), default(CancellationToken), creationOptions, internalOptions, null)
		{
			this.m_antecedent = antecedent;
			base.PossiblyCaptureContext(ref stackMark);
		}

		// Token: 0x06003B36 RID: 15158 RVA: 0x000D685C File Offset: 0x000D4A5C
		internal override void InnerInvoke()
		{
			Task antecedent = this.m_antecedent;
			this.m_antecedent = null;
			antecedent.NotifyDebuggerOfWaitCompletionIfNecessary();
			Func<Task, TResult> func = this.m_action as Func<Task, TResult>;
			if (func != null)
			{
				this.m_result = func(antecedent);
				return;
			}
			Func<Task, object, TResult> func2 = this.m_action as Func<Task, object, TResult>;
			if (func2 != null)
			{
				this.m_result = func2(antecedent, this.m_stateObject);
				return;
			}
		}

		// Token: 0x04001EE9 RID: 7913
		private Task m_antecedent;
	}
}
