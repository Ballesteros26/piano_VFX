using System;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security;

namespace System.Threading.Tasks
{
	// Token: 0x02000517 RID: 1303
	internal class AwaitTaskContinuation : TaskContinuation, IThreadPoolWorkItem
	{
		// Token: 0x06003B4F RID: 15183 RVA: 0x000D6DA4 File Offset: 0x000D4FA4
		[SecurityCritical]
		internal AwaitTaskContinuation(Action action, bool flowExecutionContext, ref StackCrawlMark stackMark)
		{
			this.m_action = action;
			if (flowExecutionContext)
			{
				this.m_capturedContext = ExecutionContext.Capture(ref stackMark, ExecutionContext.CaptureOptions.IgnoreSyncCtx | ExecutionContext.CaptureOptions.OptimizeDefaultCase);
			}
		}

		// Token: 0x06003B50 RID: 15184 RVA: 0x000D6DC3 File Offset: 0x000D4FC3
		[SecurityCritical]
		internal AwaitTaskContinuation(Action action, bool flowExecutionContext)
		{
			this.m_action = action;
			if (flowExecutionContext)
			{
				this.m_capturedContext = ExecutionContext.FastCapture();
			}
		}

		// Token: 0x06003B51 RID: 15185 RVA: 0x000D6DE0 File Offset: 0x000D4FE0
		protected Task CreateTask(Action<object> action, object state, TaskScheduler scheduler)
		{
			return new Task(action, state, null, default(CancellationToken), TaskCreationOptions.None, InternalTaskOptions.QueuedByRuntime, scheduler)
			{
				CapturedContext = this.m_capturedContext
			};
		}

		// Token: 0x06003B52 RID: 15186 RVA: 0x000D6E11 File Offset: 0x000D5011
		[SecuritySafeCritical]
		internal override void Run(Task task, bool canInlineContinuationTask)
		{
			if (canInlineContinuationTask && AwaitTaskContinuation.IsValidLocationForInlining)
			{
				this.RunCallback(AwaitTaskContinuation.GetInvokeActionCallback(), this.m_action, ref Task.t_currentTask);
				return;
			}
			ThreadPool.UnsafeQueueCustomWorkItem(this, false);
		}

		// Token: 0x170009BB RID: 2491
		// (get) Token: 0x06003B53 RID: 15187 RVA: 0x000D6E3C File Offset: 0x000D503C
		internal static bool IsValidLocationForInlining
		{
			get
			{
				SynchronizationContext currentNoFlow = SynchronizationContext.CurrentNoFlow;
				if (currentNoFlow != null && currentNoFlow.GetType() != typeof(SynchronizationContext))
				{
					return false;
				}
				TaskScheduler internalCurrent = TaskScheduler.InternalCurrent;
				return internalCurrent == null || internalCurrent == TaskScheduler.Default;
			}
		}

		// Token: 0x06003B54 RID: 15188 RVA: 0x000D6E80 File Offset: 0x000D5080
		[SecurityCritical]
		private void ExecuteWorkItemHelper()
		{
			if (this.m_capturedContext == null)
			{
				this.m_action();
				return;
			}
			try
			{
				ExecutionContext.Run(this.m_capturedContext, AwaitTaskContinuation.GetInvokeActionCallback(), this.m_action, true);
			}
			finally
			{
				this.m_capturedContext.Dispose();
			}
		}

		// Token: 0x06003B55 RID: 15189 RVA: 0x000D6ED8 File Offset: 0x000D50D8
		[SecurityCritical]
		void IThreadPoolWorkItem.ExecuteWorkItem()
		{
			if (this.m_capturedContext == null)
			{
				this.m_action();
				return;
			}
			this.ExecuteWorkItemHelper();
		}

		// Token: 0x06003B56 RID: 15190 RVA: 0x00002194 File Offset: 0x00000394
		[SecurityCritical]
		void IThreadPoolWorkItem.MarkAborted(ThreadAbortException tae)
		{
		}

		// Token: 0x06003B57 RID: 15191 RVA: 0x000D6EF4 File Offset: 0x000D50F4
		[SecurityCritical]
		private static void InvokeAction(object state)
		{
			((Action)state)();
		}

		// Token: 0x06003B58 RID: 15192 RVA: 0x000D6F04 File Offset: 0x000D5104
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		protected static ContextCallback GetInvokeActionCallback()
		{
			ContextCallback contextCallback = AwaitTaskContinuation.s_invokeActionCallback;
			if (contextCallback == null)
			{
				contextCallback = (AwaitTaskContinuation.s_invokeActionCallback = new ContextCallback(AwaitTaskContinuation.InvokeAction));
			}
			return contextCallback;
		}

		// Token: 0x06003B59 RID: 15193 RVA: 0x000D6F30 File Offset: 0x000D5130
		[SecurityCritical]
		protected void RunCallback(ContextCallback callback, object state, ref Task currentTask)
		{
			Task task = currentTask;
			try
			{
				if (task != null)
				{
					currentTask = null;
				}
				if (this.m_capturedContext == null)
				{
					callback(state);
				}
				else
				{
					ExecutionContext.Run(this.m_capturedContext, callback, state, true);
				}
			}
			catch (Exception ex)
			{
				AwaitTaskContinuation.ThrowAsyncIfNecessary(ex);
			}
			finally
			{
				if (task != null)
				{
					currentTask = task;
				}
				if (this.m_capturedContext != null)
				{
					this.m_capturedContext.Dispose();
				}
			}
		}

		// Token: 0x06003B5A RID: 15194 RVA: 0x000D6FA4 File Offset: 0x000D51A4
		[SecurityCritical]
		internal static void RunOrScheduleAction(Action action, bool allowInlining, ref Task currentTask)
		{
			if (!allowInlining || !AwaitTaskContinuation.IsValidLocationForInlining)
			{
				AwaitTaskContinuation.UnsafeScheduleAction(action, currentTask);
				return;
			}
			Task task = currentTask;
			try
			{
				if (task != null)
				{
					currentTask = null;
				}
				action();
			}
			catch (Exception ex)
			{
				AwaitTaskContinuation.ThrowAsyncIfNecessary(ex);
			}
			finally
			{
				if (task != null)
				{
					currentTask = task;
				}
			}
		}

		// Token: 0x06003B5B RID: 15195 RVA: 0x000D7000 File Offset: 0x000D5200
		[SecurityCritical]
		internal static void UnsafeScheduleAction(Action action, Task task)
		{
			ThreadPool.UnsafeQueueCustomWorkItem(new AwaitTaskContinuation(action, false), false);
		}

		// Token: 0x06003B5C RID: 15196 RVA: 0x000D7010 File Offset: 0x000D5210
		protected static void ThrowAsyncIfNecessary(Exception exc)
		{
			if (!(exc is ThreadAbortException) && !(exc is AppDomainUnloadedException) && !WindowsRuntimeMarshal.ReportUnhandledError(exc))
			{
				ExceptionDispatchInfo exceptionDispatchInfo = ExceptionDispatchInfo.Capture(exc);
				ThreadPool.QueueUserWorkItem(delegate(object s)
				{
					((ExceptionDispatchInfo)s).Throw();
				}, exceptionDispatchInfo);
			}
		}

		// Token: 0x06003B5D RID: 15197 RVA: 0x000D7062 File Offset: 0x000D5262
		internal override Delegate[] GetDelegateContinuationsForDebugger()
		{
			return new Delegate[] { AsyncMethodBuilderCore.TryGetStateMachineForDebugger(this.m_action) };
		}

		// Token: 0x04001EF6 RID: 7926
		private readonly ExecutionContext m_capturedContext;

		// Token: 0x04001EF7 RID: 7927
		protected readonly Action m_action;

		// Token: 0x04001EF8 RID: 7928
		[SecurityCritical]
		private static ContextCallback s_invokeActionCallback;
	}
}
