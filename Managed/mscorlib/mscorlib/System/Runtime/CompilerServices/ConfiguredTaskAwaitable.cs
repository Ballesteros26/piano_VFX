using System;
using System.Security;
using System.Security.Permissions;
using System.Threading.Tasks;

namespace System.Runtime.CompilerServices
{
	/// <summary>Provides an awaitable object that enables configured awaits on a task.</summary>
	// Token: 0x02000856 RID: 2134
	public struct ConfiguredTaskAwaitable
	{
		// Token: 0x0600541B RID: 21531 RVA: 0x001270A6 File Offset: 0x001252A6
		internal ConfiguredTaskAwaitable(Task task, bool continueOnCapturedContext)
		{
			this.m_configuredTaskAwaiter = new ConfiguredTaskAwaitable.ConfiguredTaskAwaiter(task, continueOnCapturedContext);
		}

		/// <summary>Returns an awaiter for this awaitable object.</summary>
		/// <returns>The awaiter.</returns>
		// Token: 0x0600541C RID: 21532 RVA: 0x001270B5 File Offset: 0x001252B5
		public ConfiguredTaskAwaitable.ConfiguredTaskAwaiter GetAwaiter()
		{
			return this.m_configuredTaskAwaiter;
		}

		// Token: 0x04002BA8 RID: 11176
		private readonly ConfiguredTaskAwaitable.ConfiguredTaskAwaiter m_configuredTaskAwaiter;

		/// <summary>Provides an awaiter for an awaitable (<see cref="T:System.Runtime.CompilerServices.ConfiguredTaskAwaitable" />) object.</summary>
		// Token: 0x02000857 RID: 2135
		[HostProtection(SecurityAction.LinkDemand, Synchronization = true, ExternalThreading = true)]
		public struct ConfiguredTaskAwaiter : ICriticalNotifyCompletion, INotifyCompletion
		{
			// Token: 0x0600541D RID: 21533 RVA: 0x001270BD File Offset: 0x001252BD
			internal ConfiguredTaskAwaiter(Task task, bool continueOnCapturedContext)
			{
				this.m_task = task;
				this.m_continueOnCapturedContext = continueOnCapturedContext;
			}

			/// <summary>Gets a value that specifies whether the task being awaited is completed.</summary>
			/// <returns>true if the task being awaited is completed; otherwise, false.</returns>
			/// <exception cref="T:System.NullReferenceException">The awaiter was not properly initialized.</exception>
			// Token: 0x17000EB2 RID: 3762
			// (get) Token: 0x0600541E RID: 21534 RVA: 0x001270CD File Offset: 0x001252CD
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
			// Token: 0x0600541F RID: 21535 RVA: 0x001270DA File Offset: 0x001252DA
			[SecuritySafeCritical]
			public void OnCompleted(Action continuation)
			{
				TaskAwaiter.OnCompletedInternal(this.m_task, continuation, this.m_continueOnCapturedContext, true);
			}

			/// <summary>Schedules the continuation action for the task associated with this awaiter. </summary>
			/// <param name="continuation">The action to invoke when the await operation completes.</param>
			/// <exception cref="T:System.ArgumentNullException">The <paramref name="continuation" /> argument is null.</exception>
			/// <exception cref="T:System.NullReferenceException">The awaiter was not properly initialized.</exception>
			// Token: 0x06005420 RID: 21536 RVA: 0x001270EF File Offset: 0x001252EF
			[SecurityCritical]
			public void UnsafeOnCompleted(Action continuation)
			{
				TaskAwaiter.OnCompletedInternal(this.m_task, continuation, this.m_continueOnCapturedContext, false);
			}

			/// <summary>Ends the await on the completed task.</summary>
			/// <exception cref="T:System.NullReferenceException">The awaiter was not properly initialized.</exception>
			/// <exception cref="T:System.Threading.Tasks.TaskCanceledException">The task was canceled.</exception>
			/// <exception cref="T:System.Exception">The task completed in a faulted state.</exception>
			// Token: 0x06005421 RID: 21537 RVA: 0x00127104 File Offset: 0x00125304
			public void GetResult()
			{
				TaskAwaiter.ValidateEnd(this.m_task);
			}

			// Token: 0x04002BA9 RID: 11177
			private readonly Task m_task;

			// Token: 0x04002BAA RID: 11178
			private readonly bool m_continueOnCapturedContext;
		}
	}
}
