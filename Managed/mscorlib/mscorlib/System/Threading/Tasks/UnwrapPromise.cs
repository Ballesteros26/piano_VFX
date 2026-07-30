using System;
using System.Collections.ObjectModel;
using System.Runtime.ExceptionServices;
using System.Security;

namespace System.Threading.Tasks
{
	// Token: 0x02000509 RID: 1289
	internal sealed class UnwrapPromise<TResult> : Task<TResult>, ITaskCompletionAction
	{
		// Token: 0x06003B13 RID: 15123 RVA: 0x000D61E4 File Offset: 0x000D43E4
		public UnwrapPromise(Task outerTask, bool lookForOce)
			: base(null, outerTask.CreationOptions & TaskCreationOptions.AttachedToParent)
		{
			this._lookForOce = lookForOce;
			this._state = 0;
			if (AsyncCausalityTracer.LoggingOn)
			{
				AsyncCausalityTracer.TraceOperationCreation(CausalityTraceLevel.Required, base.Id, "Task.Unwrap", 0UL);
			}
			if (Task.s_asyncDebuggingEnabled)
			{
				Task.AddToActiveTasks(this);
			}
			if (outerTask.IsCompleted)
			{
				this.ProcessCompletedOuterTask(outerTask);
				return;
			}
			outerTask.AddCompletionAction(this);
		}

		// Token: 0x06003B14 RID: 15124 RVA: 0x000D6250 File Offset: 0x000D4450
		public void Invoke(Task completingTask)
		{
			StackGuard currentStackGuard = Task.CurrentStackGuard;
			if (currentStackGuard.TryBeginInliningScope())
			{
				try
				{
					this.InvokeCore(completingTask);
					return;
				}
				finally
				{
					currentStackGuard.EndInliningScope();
				}
			}
			this.InvokeCoreAsync(completingTask);
		}

		// Token: 0x06003B15 RID: 15125 RVA: 0x000D6294 File Offset: 0x000D4494
		private void InvokeCore(Task completingTask)
		{
			byte state = this._state;
			if (state == 0)
			{
				this.ProcessCompletedOuterTask(completingTask);
				return;
			}
			if (state != 1)
			{
				return;
			}
			this.TrySetFromTask(completingTask, false);
			this._state = 2;
		}

		// Token: 0x06003B16 RID: 15126 RVA: 0x000D62C8 File Offset: 0x000D44C8
		[SecuritySafeCritical]
		private void InvokeCoreAsync(Task completingTask)
		{
			ThreadPool.UnsafeQueueUserWorkItem(delegate(object state)
			{
				Tuple<UnwrapPromise<TResult>, Task> tuple = (Tuple<UnwrapPromise<TResult>, Task>)state;
				tuple.Item1.InvokeCore(tuple.Item2);
			}, Tuple.Create<UnwrapPromise<TResult>, Task>(this, completingTask));
		}

		// Token: 0x06003B17 RID: 15127 RVA: 0x000D62F8 File Offset: 0x000D44F8
		private void ProcessCompletedOuterTask(Task task)
		{
			this._state = 1;
			TaskStatus status = task.Status;
			if (status != TaskStatus.RanToCompletion)
			{
				if (status - TaskStatus.Canceled <= 1)
				{
					this.TrySetFromTask(task, this._lookForOce);
					return;
				}
			}
			else
			{
				Task<Task<TResult>> task2 = task as Task<Task<TResult>>;
				this.ProcessInnerTask((task2 != null) ? task2.Result : ((Task<Task>)task).Result);
			}
		}

		// Token: 0x06003B18 RID: 15128 RVA: 0x000D6350 File Offset: 0x000D4550
		private bool TrySetFromTask(Task task, bool lookForOce)
		{
			if (AsyncCausalityTracer.LoggingOn)
			{
				AsyncCausalityTracer.TraceOperationRelation(CausalityTraceLevel.Important, base.Id, CausalityRelation.Join);
			}
			bool flag = false;
			switch (task.Status)
			{
			case TaskStatus.RanToCompletion:
			{
				Task<TResult> task2 = task as Task<TResult>;
				if (AsyncCausalityTracer.LoggingOn)
				{
					AsyncCausalityTracer.TraceOperationCompletion(CausalityTraceLevel.Required, base.Id, AsyncCausalityStatus.Completed);
				}
				if (Task.s_asyncDebuggingEnabled)
				{
					Task.RemoveFromActiveTasks(base.Id);
				}
				flag = base.TrySetResult((task2 != null) ? task2.Result : default(TResult));
				break;
			}
			case TaskStatus.Canceled:
				flag = base.TrySetCanceled(task.CancellationToken, task.GetCancellationExceptionDispatchInfo());
				break;
			case TaskStatus.Faulted:
			{
				ReadOnlyCollection<ExceptionDispatchInfo> exceptionDispatchInfos = task.GetExceptionDispatchInfos();
				ExceptionDispatchInfo exceptionDispatchInfo;
				OperationCanceledException ex;
				if (lookForOce && exceptionDispatchInfos.Count > 0 && (exceptionDispatchInfo = exceptionDispatchInfos[0]) != null && (ex = exceptionDispatchInfo.SourceException as OperationCanceledException) != null)
				{
					flag = base.TrySetCanceled(ex.CancellationToken, exceptionDispatchInfo);
				}
				else
				{
					flag = base.TrySetException(exceptionDispatchInfos);
				}
				break;
			}
			}
			return flag;
		}

		// Token: 0x06003B19 RID: 15129 RVA: 0x000D6444 File Offset: 0x000D4644
		private void ProcessInnerTask(Task task)
		{
			if (task == null)
			{
				base.TrySetCanceled(default(CancellationToken));
				this._state = 2;
				return;
			}
			if (task.IsCompleted)
			{
				this.TrySetFromTask(task, false);
				this._state = 2;
				return;
			}
			task.AddCompletionAction(this);
		}

		// Token: 0x04001EDF RID: 7903
		private const byte STATE_WAITING_ON_OUTER_TASK = 0;

		// Token: 0x04001EE0 RID: 7904
		private const byte STATE_WAITING_ON_INNER_TASK = 1;

		// Token: 0x04001EE1 RID: 7905
		private const byte STATE_DONE = 2;

		// Token: 0x04001EE2 RID: 7906
		private byte _state;

		// Token: 0x04001EE3 RID: 7907
		private readonly bool _lookForOce;
	}
}
