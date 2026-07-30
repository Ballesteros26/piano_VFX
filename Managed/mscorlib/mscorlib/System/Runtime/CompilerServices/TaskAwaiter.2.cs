using System;
using System.Security;
using System.Security.Permissions;
using System.Threading.Tasks;

namespace System.Runtime.CompilerServices
{
	/// <summary>Represents an object that waits for the completion of an asynchronous task and provides a parameter for the result.</summary>
	/// <typeparam name="TResult">The result for the task.</typeparam>
	// Token: 0x02000855 RID: 2133
	[HostProtection(SecurityAction.LinkDemand, Synchronization = true, ExternalThreading = true)]
	public struct TaskAwaiter<TResult> : ICriticalNotifyCompletion, INotifyCompletion
	{
		// Token: 0x06005416 RID: 21526 RVA: 0x00127058 File Offset: 0x00125258
		internal TaskAwaiter(Task<TResult> task)
		{
			this.m_task = task;
		}

		/// <summary>Gets a value that indicates whether the asynchronous task has completed.</summary>
		/// <returns>true if the task has completed; otherwise, false.</returns>
		/// <exception cref="T:System.NullReferenceException">The <see cref="T:System.Runtime.CompilerServices.TaskAwaiter`1" /> object was not properly initialized.</exception>
		// Token: 0x17000EB1 RID: 3761
		// (get) Token: 0x06005417 RID: 21527 RVA: 0x00127061 File Offset: 0x00125261
		public bool IsCompleted
		{
			get
			{
				return this.m_task.IsCompleted;
			}
		}

		/// <summary>Sets the action to perform when the <see cref="T:System.Runtime.CompilerServices.TaskAwaiter`1" /> object stops waiting for the asynchronous task to complete.</summary>
		/// <param name="continuation">The action to perform when the wait operation completes.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="continuation" /> is null.</exception>
		/// <exception cref="T:System.NullReferenceException">The <see cref="T:System.Runtime.CompilerServices.TaskAwaiter`1" /> object was not properly initialized.</exception>
		// Token: 0x06005418 RID: 21528 RVA: 0x0012706E File Offset: 0x0012526E
		[SecuritySafeCritical]
		public void OnCompleted(Action continuation)
		{
			TaskAwaiter.OnCompletedInternal(this.m_task, continuation, true, true);
		}

		/// <summary>Schedules the continuation action for the asynchronous task associated with this awaiter.</summary>
		/// <param name="continuation">The action to invoke when the await operation completes.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="continuation" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The awaiter was not properly initialized.</exception>
		// Token: 0x06005419 RID: 21529 RVA: 0x0012707E File Offset: 0x0012527E
		[SecurityCritical]
		public void UnsafeOnCompleted(Action continuation)
		{
			TaskAwaiter.OnCompletedInternal(this.m_task, continuation, true, false);
		}

		/// <summary>Ends the wait for the completion of the asynchronous task.</summary>
		/// <returns>The result of the completed task.</returns>
		/// <exception cref="T:System.NullReferenceException">The <see cref="T:System.Runtime.CompilerServices.TaskAwaiter`1" /> object was not properly initialized.</exception>
		/// <exception cref="T:System.Threading.Tasks.TaskCanceledException">The task was canceled.</exception>
		/// <exception cref="T:System.Exception">The task completed in a <see cref="F:System.Threading.Tasks.TaskStatus.Faulted" /> state.</exception>
		// Token: 0x0600541A RID: 21530 RVA: 0x0012708E File Offset: 0x0012528E
		public TResult GetResult()
		{
			TaskAwaiter.ValidateEnd(this.m_task);
			return this.m_task.ResultOnSuccess;
		}

		// Token: 0x04002BA7 RID: 11175
		private readonly Task<TResult> m_task;
	}
}
