using System;
using System.Diagnostics;
using System.Security;
using System.Security.Permissions;
using System.Threading;
using System.Threading.Tasks;

namespace System.Runtime.CompilerServices
{
	/// <summary>Represents a builder for asynchronous methods that returns a task and provides a parameter for the result.</summary>
	/// <typeparam name="TResult">The result to use to complete the task.</typeparam>
	// Token: 0x0200083E RID: 2110
	[HostProtection(SecurityAction.LinkDemand, Synchronization = true, ExternalThreading = true)]
	public struct AsyncTaskMethodBuilder<TResult>
	{
		/// <summary>Creates an instance of the <see cref="T:System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1" /> class.</summary>
		/// <returns>A new instance of the builder.</returns>
		// Token: 0x060053C9 RID: 21449 RVA: 0x00126408 File Offset: 0x00124608
		public static AsyncTaskMethodBuilder<TResult> Create()
		{
			return default(AsyncTaskMethodBuilder<TResult>);
		}

		/// <summary>Begins running the builder with the associated state machine.</summary>
		/// <param name="stateMachine">The state machine instance, passed by reference.</param>
		/// <typeparam name="TStateMachine">The type of the state machine.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="stateMachine" /> is null.</exception>
		// Token: 0x060053CA RID: 21450 RVA: 0x00126420 File Offset: 0x00124620
		[DebuggerStepThrough]
		[SecuritySafeCritical]
		public void Start<TStateMachine>(ref TStateMachine stateMachine) where TStateMachine : IAsyncStateMachine
		{
			if (stateMachine == null)
			{
				throw new ArgumentNullException("stateMachine");
			}
			ExecutionContextSwitcher executionContextSwitcher = default(ExecutionContextSwitcher);
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				ExecutionContext.EstablishCopyOnWriteScope(ref executionContextSwitcher);
				stateMachine.MoveNext();
			}
			finally
			{
				executionContextSwitcher.Undo();
			}
		}

		/// <summary>Associates the builder with the specified state machine.</summary>
		/// <param name="stateMachine">The state machine instance to associate with the builder.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="stateMachine" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The state machine was previously set.</exception>
		// Token: 0x060053CB RID: 21451 RVA: 0x00126480 File Offset: 0x00124680
		public void SetStateMachine(IAsyncStateMachine stateMachine)
		{
			this.m_coreState.SetStateMachine(stateMachine);
		}

		/// <summary>Schedules the state machine to proceed to the next action when the specified awaiter completes.</summary>
		/// <param name="awaiter">The awaiter.</param>
		/// <param name="stateMachine">The state machine.</param>
		/// <typeparam name="TAwaiter">The type of the awaiter.</typeparam>
		/// <typeparam name="TStateMachine">The type of the state machine.</typeparam>
		// Token: 0x060053CC RID: 21452 RVA: 0x00126490 File Offset: 0x00124690
		public void AwaitOnCompleted<TAwaiter, TStateMachine>(ref TAwaiter awaiter, ref TStateMachine stateMachine) where TAwaiter : INotifyCompletion where TStateMachine : IAsyncStateMachine
		{
			try
			{
				AsyncMethodBuilderCore.MoveNextRunner moveNextRunner = null;
				Action completionAction = this.m_coreState.GetCompletionAction(AsyncCausalityTracer.LoggingOn ? this.Task : null, ref moveNextRunner);
				if (this.m_coreState.m_stateMachine == null)
				{
					Task<TResult> task = this.Task;
					this.m_coreState.PostBoxInitialization(stateMachine, moveNextRunner, task);
				}
				awaiter.OnCompleted(completionAction);
			}
			catch (Exception ex)
			{
				AsyncMethodBuilderCore.ThrowAsync(ex, null);
			}
		}

		/// <summary>Schedules the state machine to proceed to the next action when the specified awaiter completes. This method can be called from partially trusted code.</summary>
		/// <param name="awaiter">The awaiter.</param>
		/// <param name="stateMachine">The state machine.</param>
		/// <typeparam name="TAwaiter">The type of the awaiter.</typeparam>
		/// <typeparam name="TStateMachine">The type of the state machine.</typeparam>
		// Token: 0x060053CD RID: 21453 RVA: 0x00126510 File Offset: 0x00124710
		[SecuritySafeCritical]
		public void AwaitUnsafeOnCompleted<TAwaiter, TStateMachine>(ref TAwaiter awaiter, ref TStateMachine stateMachine) where TAwaiter : ICriticalNotifyCompletion where TStateMachine : IAsyncStateMachine
		{
			try
			{
				AsyncMethodBuilderCore.MoveNextRunner moveNextRunner = null;
				Action completionAction = this.m_coreState.GetCompletionAction(AsyncCausalityTracer.LoggingOn ? this.Task : null, ref moveNextRunner);
				if (this.m_coreState.m_stateMachine == null)
				{
					Task<TResult> task = this.Task;
					this.m_coreState.PostBoxInitialization(stateMachine, moveNextRunner, task);
				}
				awaiter.UnsafeOnCompleted(completionAction);
			}
			catch (Exception ex)
			{
				AsyncMethodBuilderCore.ThrowAsync(ex, null);
			}
		}

		/// <summary>Gets the task for this builder.</summary>
		/// <returns>The task for this builder.</returns>
		// Token: 0x17000EA8 RID: 3752
		// (get) Token: 0x060053CE RID: 21454 RVA: 0x00126590 File Offset: 0x00124790
		public Task<TResult> Task
		{
			get
			{
				Task<TResult> task = this.m_task;
				if (task == null)
				{
					task = (this.m_task = new Task<TResult>());
				}
				return task;
			}
		}

		/// <summary>Marks the task as successfully completed.</summary>
		/// <param name="result">The result to use to complete the task.</param>
		/// <exception cref="T:System.InvalidOperationException">The task has already completed.</exception>
		// Token: 0x060053CF RID: 21455 RVA: 0x001265B8 File Offset: 0x001247B8
		public void SetResult(TResult result)
		{
			Task<TResult> task = this.m_task;
			if (task == null)
			{
				this.m_task = this.GetTaskForResult(result);
				return;
			}
			if (AsyncCausalityTracer.LoggingOn)
			{
				AsyncCausalityTracer.TraceOperationCompletion(CausalityTraceLevel.Required, task.Id, AsyncCausalityStatus.Completed);
			}
			if (global::System.Threading.Tasks.Task.s_asyncDebuggingEnabled)
			{
				global::System.Threading.Tasks.Task.RemoveFromActiveTasks(task.Id);
			}
			if (!task.TrySetResult(result))
			{
				throw new InvalidOperationException(Environment.GetResourceString("An attempt was made to transition a task to a final state when it had already completed."));
			}
		}

		// Token: 0x060053D0 RID: 21456 RVA: 0x0012661C File Offset: 0x0012481C
		internal void SetResult(Task<TResult> completedTask)
		{
			if (this.m_task == null)
			{
				this.m_task = completedTask;
				return;
			}
			this.SetResult(default(TResult));
		}

		/// <summary>Marks the task as failed and binds the specified exception to the task.</summary>
		/// <param name="exception">The exception to bind to the task.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="exception" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The task has already completed.</exception>
		// Token: 0x060053D1 RID: 21457 RVA: 0x00126648 File Offset: 0x00124848
		public void SetException(Exception exception)
		{
			if (exception == null)
			{
				throw new ArgumentNullException("exception");
			}
			Task<TResult> task = this.m_task;
			if (task == null)
			{
				task = this.Task;
			}
			OperationCanceledException ex = exception as OperationCanceledException;
			if (!((ex != null) ? task.TrySetCanceled(ex.CancellationToken, ex) : task.TrySetException(exception)))
			{
				throw new InvalidOperationException(Environment.GetResourceString("An attempt was made to transition a task to a final state when it had already completed."));
			}
		}

		// Token: 0x060053D2 RID: 21458 RVA: 0x001266A6 File Offset: 0x001248A6
		internal void SetNotificationForWaitCompletion(bool enabled)
		{
			this.Task.SetNotificationForWaitCompletion(enabled);
		}

		// Token: 0x17000EA9 RID: 3753
		// (get) Token: 0x060053D3 RID: 21459 RVA: 0x001266B4 File Offset: 0x001248B4
		private object ObjectIdForDebugger
		{
			get
			{
				return this.Task;
			}
		}

		// Token: 0x060053D4 RID: 21460 RVA: 0x001266BC File Offset: 0x001248BC
		[SecuritySafeCritical]
		private Task<TResult> GetTaskForResult(TResult result)
		{
			if (default(TResult) != null)
			{
				if (typeof(TResult) == typeof(bool))
				{
					return JitHelpers.UnsafeCast<Task<TResult>>(((bool)((object)result)) ? AsyncTaskCache.TrueTask : AsyncTaskCache.FalseTask);
				}
				if (typeof(TResult) == typeof(int))
				{
					int num = (int)((object)result);
					if (num < 9 && num >= -1)
					{
						return JitHelpers.UnsafeCast<Task<TResult>>(AsyncTaskCache.Int32Tasks[num - -1]);
					}
				}
				else if ((typeof(TResult) == typeof(uint) && (uint)((object)result) == 0U) || (typeof(TResult) == typeof(byte) && (byte)((object)result) == 0) || (typeof(TResult) == typeof(sbyte) && (sbyte)((object)result) == 0) || (typeof(TResult) == typeof(char) && (char)((object)result) == '\0') || (typeof(TResult) == typeof(decimal) && 0m == (decimal)((object)result)) || (typeof(TResult) == typeof(long) && (long)((object)result) == 0L) || (typeof(TResult) == typeof(ulong) && (ulong)((object)result) == 0UL) || (typeof(TResult) == typeof(short) && (short)((object)result) == 0) || (typeof(TResult) == typeof(ushort) && (ushort)((object)result) == 0) || (typeof(TResult) == typeof(IntPtr) && (IntPtr)0 == (IntPtr)((object)result)) || (typeof(TResult) == typeof(UIntPtr) && (UIntPtr)0 == (UIntPtr)((object)result)))
				{
					return AsyncTaskMethodBuilder<TResult>.s_defaultResultTask;
				}
			}
			else if (result == null)
			{
				return AsyncTaskMethodBuilder<TResult>.s_defaultResultTask;
			}
			return new Task<TResult>(result);
		}

		// Token: 0x04002B89 RID: 11145
		internal static readonly Task<TResult> s_defaultResultTask = AsyncTaskCache.CreateCacheableTask<TResult>(default(TResult));

		// Token: 0x04002B8A RID: 11146
		private AsyncMethodBuilderCore m_coreState;

		// Token: 0x04002B8B RID: 11147
		private Task<TResult> m_task;
	}
}
