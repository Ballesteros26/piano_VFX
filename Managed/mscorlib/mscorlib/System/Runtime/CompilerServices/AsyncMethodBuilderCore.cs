using System;
using System.Diagnostics;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security;
using System.Threading;
using System.Threading.Tasks;

namespace System.Runtime.CompilerServices
{
	// Token: 0x02000840 RID: 2112
	internal struct AsyncMethodBuilderCore
	{
		// Token: 0x060053D9 RID: 21465 RVA: 0x001269F4 File Offset: 0x00124BF4
		public void SetStateMachine(IAsyncStateMachine stateMachine)
		{
			if (stateMachine == null)
			{
				throw new ArgumentNullException("stateMachine");
			}
			if (this.m_stateMachine != null)
			{
				throw new InvalidOperationException(Environment.GetResourceString("The builder was not properly initialized."));
			}
			this.m_stateMachine = stateMachine;
		}

		// Token: 0x060053DA RID: 21466 RVA: 0x00126A24 File Offset: 0x00124C24
		[SecuritySafeCritical]
		internal Action GetCompletionAction(Task taskForTracing, ref AsyncMethodBuilderCore.MoveNextRunner runnerToInitialize)
		{
			Debugger.NotifyOfCrossThreadDependency();
			ExecutionContext executionContext = ExecutionContext.FastCapture();
			Action action;
			AsyncMethodBuilderCore.MoveNextRunner moveNextRunner;
			if (executionContext != null && executionContext.IsPreAllocatedDefault)
			{
				action = this.m_defaultContextAction;
				if (action != null)
				{
					return action;
				}
				moveNextRunner = new AsyncMethodBuilderCore.MoveNextRunner(executionContext, this.m_stateMachine);
				action = new Action(moveNextRunner.Run);
				if (taskForTracing != null)
				{
					action = (this.m_defaultContextAction = this.OutputAsyncCausalityEvents(taskForTracing, action));
				}
				else
				{
					this.m_defaultContextAction = action;
				}
			}
			else
			{
				moveNextRunner = new AsyncMethodBuilderCore.MoveNextRunner(executionContext, this.m_stateMachine);
				action = new Action(moveNextRunner.Run);
				if (taskForTracing != null)
				{
					action = this.OutputAsyncCausalityEvents(taskForTracing, action);
				}
			}
			if (this.m_stateMachine == null)
			{
				runnerToInitialize = moveNextRunner;
			}
			return action;
		}

		// Token: 0x060053DB RID: 21467 RVA: 0x00126AC0 File Offset: 0x00124CC0
		private Action OutputAsyncCausalityEvents(Task innerTask, Action continuation)
		{
			return AsyncMethodBuilderCore.CreateContinuationWrapper(continuation, delegate
			{
				AsyncCausalityTracer.TraceSynchronousWorkStart(CausalityTraceLevel.Required, innerTask.Id, CausalitySynchronousWork.Execution);
				continuation();
				AsyncCausalityTracer.TraceSynchronousWorkCompletion(CausalityTraceLevel.Required, CausalitySynchronousWork.Execution);
			}, innerTask);
		}

		// Token: 0x060053DC RID: 21468 RVA: 0x00126B00 File Offset: 0x00124D00
		internal void PostBoxInitialization(IAsyncStateMachine stateMachine, AsyncMethodBuilderCore.MoveNextRunner runner, Task builtTask)
		{
			if (builtTask != null)
			{
				if (AsyncCausalityTracer.LoggingOn)
				{
					AsyncCausalityTracer.TraceOperationCreation(CausalityTraceLevel.Required, builtTask.Id, "Async: " + stateMachine.GetType().Name, 0UL);
				}
				if (Task.s_asyncDebuggingEnabled)
				{
					Task.AddToActiveTasks(builtTask);
				}
			}
			this.m_stateMachine = stateMachine;
			this.m_stateMachine.SetStateMachine(this.m_stateMachine);
			runner.m_stateMachine = this.m_stateMachine;
		}

		// Token: 0x060053DD RID: 21469 RVA: 0x00126B6C File Offset: 0x00124D6C
		internal static void ThrowAsync(Exception exception, SynchronizationContext targetContext)
		{
			ExceptionDispatchInfo exceptionDispatchInfo = ExceptionDispatchInfo.Capture(exception);
			if (targetContext != null)
			{
				try
				{
					targetContext.Post(delegate(object state)
					{
						((ExceptionDispatchInfo)state).Throw();
					}, exceptionDispatchInfo);
					return;
				}
				catch (Exception ex)
				{
					exceptionDispatchInfo = ExceptionDispatchInfo.Capture(new AggregateException(new Exception[] { exception, ex }));
				}
			}
			if (!WindowsRuntimeMarshal.ReportUnhandledError(exceptionDispatchInfo.SourceException))
			{
				ThreadPool.QueueUserWorkItem(delegate(object state)
				{
					((ExceptionDispatchInfo)state).Throw();
				}, exceptionDispatchInfo);
			}
		}

		// Token: 0x060053DE RID: 21470 RVA: 0x00126C0C File Offset: 0x00124E0C
		internal static Action CreateContinuationWrapper(Action continuation, Action invokeAction, Task innerTask = null)
		{
			return new Action(new AsyncMethodBuilderCore.ContinuationWrapper(continuation, invokeAction, innerTask).Invoke);
		}

		// Token: 0x060053DF RID: 21471 RVA: 0x00126C24 File Offset: 0x00124E24
		internal static Action TryGetStateMachineForDebugger(Action action)
		{
			object target = action.Target;
			AsyncMethodBuilderCore.MoveNextRunner moveNextRunner = target as AsyncMethodBuilderCore.MoveNextRunner;
			if (moveNextRunner != null)
			{
				return new Action(moveNextRunner.m_stateMachine.MoveNext);
			}
			AsyncMethodBuilderCore.ContinuationWrapper continuationWrapper = target as AsyncMethodBuilderCore.ContinuationWrapper;
			if (continuationWrapper != null)
			{
				return AsyncMethodBuilderCore.TryGetStateMachineForDebugger(continuationWrapper.m_continuation);
			}
			return action;
		}

		// Token: 0x060053E0 RID: 21472 RVA: 0x00126C6C File Offset: 0x00124E6C
		internal static Task TryGetContinuationTask(Action action)
		{
			if (action != null)
			{
				AsyncMethodBuilderCore.ContinuationWrapper continuationWrapper = action.Target as AsyncMethodBuilderCore.ContinuationWrapper;
				if (continuationWrapper != null)
				{
					return continuationWrapper.m_innerTask;
				}
			}
			return null;
		}

		// Token: 0x04002B91 RID: 11153
		internal IAsyncStateMachine m_stateMachine;

		// Token: 0x04002B92 RID: 11154
		internal Action m_defaultContextAction;

		// Token: 0x02000841 RID: 2113
		internal sealed class MoveNextRunner
		{
			// Token: 0x060053E1 RID: 21473 RVA: 0x00126C93 File Offset: 0x00124E93
			[SecurityCritical]
			internal MoveNextRunner(ExecutionContext context, IAsyncStateMachine stateMachine)
			{
				this.m_context = context;
				this.m_stateMachine = stateMachine;
			}

			// Token: 0x060053E2 RID: 21474 RVA: 0x00126CAC File Offset: 0x00124EAC
			[SecuritySafeCritical]
			internal void Run()
			{
				if (this.m_context != null)
				{
					try
					{
						ContextCallback contextCallback = AsyncMethodBuilderCore.MoveNextRunner.s_invokeMoveNext;
						if (contextCallback == null)
						{
							contextCallback = (AsyncMethodBuilderCore.MoveNextRunner.s_invokeMoveNext = new ContextCallback(AsyncMethodBuilderCore.MoveNextRunner.InvokeMoveNext));
						}
						ExecutionContext.Run(this.m_context, contextCallback, this.m_stateMachine, true);
						return;
					}
					finally
					{
						this.m_context.Dispose();
					}
				}
				this.m_stateMachine.MoveNext();
			}

			// Token: 0x060053E3 RID: 21475 RVA: 0x00126D1C File Offset: 0x00124F1C
			[SecurityCritical]
			private static void InvokeMoveNext(object stateMachine)
			{
				((IAsyncStateMachine)stateMachine).MoveNext();
			}

			// Token: 0x04002B93 RID: 11155
			private readonly ExecutionContext m_context;

			// Token: 0x04002B94 RID: 11156
			internal IAsyncStateMachine m_stateMachine;

			// Token: 0x04002B95 RID: 11157
			[SecurityCritical]
			private static ContextCallback s_invokeMoveNext;
		}

		// Token: 0x02000842 RID: 2114
		private class ContinuationWrapper
		{
			// Token: 0x060053E4 RID: 21476 RVA: 0x00126D29 File Offset: 0x00124F29
			internal ContinuationWrapper(Action continuation, Action invokeAction, Task innerTask)
			{
				if (innerTask == null)
				{
					innerTask = AsyncMethodBuilderCore.TryGetContinuationTask(continuation);
				}
				this.m_continuation = continuation;
				this.m_innerTask = innerTask;
				this.m_invokeAction = invokeAction;
			}

			// Token: 0x060053E5 RID: 21477 RVA: 0x00126D51 File Offset: 0x00124F51
			internal void Invoke()
			{
				this.m_invokeAction();
			}

			// Token: 0x04002B96 RID: 11158
			internal readonly Action m_continuation;

			// Token: 0x04002B97 RID: 11159
			private readonly Action m_invokeAction;

			// Token: 0x04002B98 RID: 11160
			internal readonly Task m_innerTask;
		}
	}
}
