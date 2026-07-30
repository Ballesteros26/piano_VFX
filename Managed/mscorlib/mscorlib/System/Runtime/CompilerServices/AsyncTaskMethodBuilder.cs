using System;
using System.Diagnostics;
using System.Security;
using System.Security.Permissions;
using System.Threading;
using System.Threading.Tasks;

namespace System.Runtime.CompilerServices
{
	/// <summary>Represents a builder for asynchronous methods that return a task.</summary>
	// Token: 0x0200083D RID: 2109
	[HostProtection(SecurityAction.LinkDemand, Synchronization = true, ExternalThreading = true)]
	public struct AsyncTaskMethodBuilder
	{
		/// <summary>Creates an instance of the <see cref="T:System.Runtime.CompilerServices.AsyncTaskMethodBuilder" /> class.</summary>
		/// <returns>A new instance of the builder.</returns>
		// Token: 0x060053BE RID: 21438 RVA: 0x00126314 File Offset: 0x00124514
		public static AsyncTaskMethodBuilder Create()
		{
			return default(AsyncTaskMethodBuilder);
		}

		/// <summary>Begins running the builder with the associated state machine.</summary>
		/// <param name="stateMachine">The state machine instance, passed by reference.</param>
		/// <typeparam name="TStateMachine">The type of the state machine.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="stateMachine" /> is null.</exception>
		// Token: 0x060053BF RID: 21439 RVA: 0x0012632C File Offset: 0x0012452C
		[SecuritySafeCritical]
		[DebuggerStepThrough]
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
		// Token: 0x060053C0 RID: 21440 RVA: 0x0012638C File Offset: 0x0012458C
		public void SetStateMachine(IAsyncStateMachine stateMachine)
		{
			this.m_builder.SetStateMachine(stateMachine);
		}

		/// <summary>Schedules the state machine to proceed to the next action when the specified awaiter completes.</summary>
		/// <param name="awaiter">The awaiter.</param>
		/// <param name="stateMachine">The state machine.</param>
		/// <typeparam name="TAwaiter">The type of the awaiter.</typeparam>
		/// <typeparam name="TStateMachine">The type of the state machine.</typeparam>
		// Token: 0x060053C1 RID: 21441 RVA: 0x0012639A File Offset: 0x0012459A
		public void AwaitOnCompleted<TAwaiter, TStateMachine>(ref TAwaiter awaiter, ref TStateMachine stateMachine) where TAwaiter : INotifyCompletion where TStateMachine : IAsyncStateMachine
		{
			this.m_builder.AwaitOnCompleted<TAwaiter, TStateMachine>(ref awaiter, ref stateMachine);
		}

		/// <summary>Schedules the state machine to proceed to the next action when the specified awaiter completes. This method can be called from partially trusted code.</summary>
		/// <param name="awaiter">The awaiter.</param>
		/// <param name="stateMachine">The state machine.</param>
		/// <typeparam name="TAwaiter">The type of the awaiter.</typeparam>
		/// <typeparam name="TStateMachine">The type of the state machine.</typeparam>
		// Token: 0x060053C2 RID: 21442 RVA: 0x001263A9 File Offset: 0x001245A9
		public void AwaitUnsafeOnCompleted<TAwaiter, TStateMachine>(ref TAwaiter awaiter, ref TStateMachine stateMachine) where TAwaiter : ICriticalNotifyCompletion where TStateMachine : IAsyncStateMachine
		{
			this.m_builder.AwaitUnsafeOnCompleted<TAwaiter, TStateMachine>(ref awaiter, ref stateMachine);
		}

		/// <summary>Gets the task for this builder.</summary>
		/// <returns>The task for this builder.</returns>
		/// <exception cref="T:System.InvalidOperationException">The builder is not initialized.</exception>
		// Token: 0x17000EA6 RID: 3750
		// (get) Token: 0x060053C3 RID: 21443 RVA: 0x001263B8 File Offset: 0x001245B8
		public Task Task
		{
			get
			{
				return this.m_builder.Task;
			}
		}

		/// <summary>Marks the task as successfully completed.</summary>
		/// <exception cref="T:System.InvalidOperationException">The task has already completed.-or-The builder is not initialized.</exception>
		// Token: 0x060053C4 RID: 21444 RVA: 0x001263C5 File Offset: 0x001245C5
		public void SetResult()
		{
			this.m_builder.SetResult(AsyncTaskMethodBuilder.s_cachedCompleted);
		}

		/// <summary>Marks the task as failed and binds the specified exception to the task.</summary>
		/// <param name="exception">The exception to bind to the task.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="exception" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The task has already completed.-or-The builder is not initialized.</exception>
		// Token: 0x060053C5 RID: 21445 RVA: 0x001263D7 File Offset: 0x001245D7
		public void SetException(Exception exception)
		{
			this.m_builder.SetException(exception);
		}

		// Token: 0x060053C6 RID: 21446 RVA: 0x001263E5 File Offset: 0x001245E5
		internal void SetNotificationForWaitCompletion(bool enabled)
		{
			this.m_builder.SetNotificationForWaitCompletion(enabled);
		}

		// Token: 0x17000EA7 RID: 3751
		// (get) Token: 0x060053C7 RID: 21447 RVA: 0x001263F3 File Offset: 0x001245F3
		private object ObjectIdForDebugger
		{
			get
			{
				return this.Task;
			}
		}

		// Token: 0x04002B87 RID: 11143
		private static readonly Task<VoidTaskResult> s_cachedCompleted = AsyncTaskMethodBuilder<VoidTaskResult>.s_defaultResultTask;

		// Token: 0x04002B88 RID: 11144
		private AsyncTaskMethodBuilder<VoidTaskResult> m_builder;
	}
}
