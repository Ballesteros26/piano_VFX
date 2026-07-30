using System;
using System.Security;
using System.Security.Permissions;
using System.Threading.Tasks;

namespace System.Runtime.CompilerServices
{
	/// <summary>Provides an awaitable object that enables configured awaits on a task.</summary>
	/// <typeparam name="TResult">The type of the result produced by this <see cref="T:System.Threading.Tasks.Task`1" />.</typeparam>
	// Token: 0x02000858 RID: 2136
	public struct ConfiguredTaskAwaitable<TResult>
	{
		// Token: 0x06005422 RID: 21538 RVA: 0x00127111 File Offset: 0x00125311
		internal ConfiguredTaskAwaitable(Task<TResult> task, bool continueOnCapturedContext)
		{
			this.m_configuredTaskAwaiter = new ConfiguredTaskAwaitable<TResult>.ConfiguredTaskAwaiter(task, continueOnCapturedContext);
		}

		/// <summary>Returns an awaiter for this awaitable object.</summary>
		/// <returns>The awaiter.</returns>
		// Token: 0x06005423 RID: 21539 RVA: 0x00127120 File Offset: 0x00125320
		public ConfiguredTaskAwaitable<TResult>.ConfiguredTaskAwaiter GetAwaiter()
		{
			return this.m_configuredTaskAwaiter;
		}

		// Token: 0x04002BAB RID: 11179
		private readonly ConfiguredTaskAwaitable<TResult>.ConfiguredTaskAwaiter m_configuredTaskAwaiter;

		/// <summary>Provides an awaiter for an awaitable object(<see cref="T:System.Runtime.CompilerServices.ConfiguredTaskAwaitable`1" />).</summary>
		// Token: 0x02000859 RID: 2137
		[HostProtection(SecurityAction.LinkDemand, Synchronization = true, ExternalThreading = true)]
		public struct ConfiguredTaskAwaiter : ICriticalNotifyCompletion, INotifyCompletion
		{
			// Token: 0x06005424 RID: 21540 RVA: 0x00127128 File Offset: 0x00125328
			internal ConfiguredTaskAwaiter(Task<TResult> task, bool continueOnCapturedContext)
			{
				this.m_task = task;
				this.m_continueOnCapturedContext = continueOnCapturedContext;
			}

			/// <summary>Gets a value that specifies whether the task being awaited has been completed.</summary>
			/// <returns>true if the task being awaited has been completed; otherwise, false.</returns>
			/// <exception cref="T:System.NullReferenceException">The awaiter was not properly initialized.</exception>
			// Token: 0x17000EB3 RID: 3763
			// (get) Token: 0x06005425 RID: 21541 RVA: 0x00127138 File Offset: 0x00125338
			public bool IsCompleted
			{
				get
				{
					return this.m_task.IsCompleted;
				}
			}

			/// <summary>Schedules the continuation action for the task associated with this awaiter.</summary>
			/// <param name="continuation">The action to invoke when the await operation completes.</param>
			/// <exception cref="T:System.ArgumentNullException">The <paramref name="continuation" /> argument is null.</exception>
			/// <exception cref="T:System.NullReferenceException">The awaiter was not properly initialized.</exception>
			// Token: 0x06005426 RID: 21542 RVA: 0x00127145 File Offset: 0x00125345
			[SecuritySafeCritical]
			public void OnCompleted(Action continuation)
			{
				TaskAwaiter.OnCompletedInternal(this.m_task, continuation, this.m_continueOnCapturedContext, true);
			}

			/// <summary>Schedules the continuation action for the task associated with this awaiter. </summary>
			/// <param name="continuation">The action to invoke when the await operation completes.</param>
			/// <exception cref="T:System.ArgumentNullException">The <paramref name="continuation" /> argument is null.</exception>
			/// <exception cref="T:System.NullReferenceException">The awaiter was not properly initialized.</exception>
			// Token: 0x06005427 RID: 21543 RVA: 0x0012715A File Offset: 0x0012535A
			[SecurityCritical]
			public void UnsafeOnCompleted(Action continuation)
			{
				TaskAwaiter.OnCompletedInternal(this.m_task, continuation, this.m_continueOnCapturedContext, false);
			}

			/// <summary>Ends the await on the completed task.</summary>
			/// <returns>The result of the completed task.</returns>
			/// <exception cref="T:System.NullReferenceException">The awaiter was not properly initialized.</exception>
			/// <exception cref="T:System.Threading.Tasks.TaskCanceledException">The task was canceled.</exception>
			/// <exception cref="T:System.Exception">The task completed in a faulted state.</exception>
			// Token: 0x06005428 RID: 21544 RVA: 0x0012716F File Offset: 0x0012536F
			public TResult GetResult()
			{
				TaskAwaiter.ValidateEnd(this.m_task);
				return this.m_task.ResultOnSuccess;
			}

			// Token: 0x04002BAC RID: 11180
			private readonly Task<TResult> m_task;

			// Token: 0x04002BAD RID: 11181
			private readonly bool m_continueOnCapturedContext;
		}
	}
}
