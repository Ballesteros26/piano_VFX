using System;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using System.Security.Permissions;

namespace System.Threading.Tasks
{
	/// <summary>Represents the producer side of a <see cref="T:System.Threading.Tasks.Task`1" /> unbound to a delegate, providing access to the consumer side through the <see cref="P:System.Threading.Tasks.TaskCompletionSource`1.Task" /> property.</summary>
	/// <typeparam name="TResult">The type of the result value assocatied with this <see cref="T:System.Threading.Tasks.TaskCompletionSource`1" />.</typeparam>
	// Token: 0x0200050C RID: 1292
	[HostProtection(SecurityAction.LinkDemand, Synchronization = true, ExternalThreading = true)]
	public class TaskCompletionSource<TResult>
	{
		/// <summary>Creates a <see cref="T:System.Threading.Tasks.TaskCompletionSource`1" />.</summary>
		// Token: 0x06003B23 RID: 15139 RVA: 0x000D652E File Offset: 0x000D472E
		public TaskCompletionSource()
		{
			this.m_task = new Task<TResult>();
		}

		/// <summary>Creates a <see cref="T:System.Threading.Tasks.TaskCompletionSource`1" /> with the specified options.</summary>
		/// <param name="creationOptions">The options to use when creating the underlying <see cref="T:System.Threading.Tasks.Task`1" />.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="creationOptions" /> represent options invalid for use with a <see cref="T:System.Threading.Tasks.TaskCompletionSource`1" />.</exception>
		// Token: 0x06003B24 RID: 15140 RVA: 0x000D6541 File Offset: 0x000D4741
		public TaskCompletionSource(TaskCreationOptions creationOptions)
			: this(null, creationOptions)
		{
		}

		/// <summary>Creates a <see cref="T:System.Threading.Tasks.TaskCompletionSource`1" /> with the specified state.</summary>
		/// <param name="state">The state to use as the underlying <see cref="T:System.Threading.Tasks.Task`1" />'s AsyncState.</param>
		// Token: 0x06003B25 RID: 15141 RVA: 0x000D654B File Offset: 0x000D474B
		public TaskCompletionSource(object state)
			: this(state, TaskCreationOptions.None)
		{
		}

		/// <summary>Creates a <see cref="T:System.Threading.Tasks.TaskCompletionSource`1" /> with the specified state and options.</summary>
		/// <param name="state">The state to use as the underlying <see cref="T:System.Threading.Tasks.Task`1" />'s AsyncState.</param>
		/// <param name="creationOptions">The options to use when creating the underlying <see cref="T:System.Threading.Tasks.Task`1" />.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="creationOptions" /> represent options invalid for use with a <see cref="T:System.Threading.Tasks.TaskCompletionSource`1" />.</exception>
		// Token: 0x06003B26 RID: 15142 RVA: 0x000D6555 File Offset: 0x000D4755
		public TaskCompletionSource(object state, TaskCreationOptions creationOptions)
		{
			this.m_task = new Task<TResult>(state, creationOptions);
		}

		/// <summary>Gets the <see cref="T:System.Threading.Tasks.Task`1" /> created by this <see cref="T:System.Threading.Tasks.TaskCompletionSource`1" />.</summary>
		/// <returns>Returns the <see cref="T:System.Threading.Tasks.Task`1" /> created by this <see cref="T:System.Threading.Tasks.TaskCompletionSource`1" />.</returns>
		// Token: 0x170009BA RID: 2490
		// (get) Token: 0x06003B27 RID: 15143 RVA: 0x000D656A File Offset: 0x000D476A
		public Task<TResult> Task
		{
			get
			{
				return this.m_task;
			}
		}

		// Token: 0x06003B28 RID: 15144 RVA: 0x000D6574 File Offset: 0x000D4774
		private void SpinUntilCompleted()
		{
			SpinWait spinWait = default(SpinWait);
			while (!this.m_task.IsCompleted)
			{
				spinWait.SpinOnce();
			}
		}

		/// <summary>Attempts to transition the underlying <see cref="T:System.Threading.Tasks.Task`1" /> into the <see cref="F:System.Threading.Tasks.TaskStatus.Faulted" /> state.</summary>
		/// <returns>True if the operation was successful; otherwise, false.</returns>
		/// <param name="exception">The exception to bind to this <see cref="T:System.Threading.Tasks.Task`1" />.</param>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="P:System.Threading.Tasks.TaskCompletionSource`1.Task" /> was disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="exception" /> argument is null.</exception>
		// Token: 0x06003B29 RID: 15145 RVA: 0x000D659F File Offset: 0x000D479F
		public bool TrySetException(Exception exception)
		{
			if (exception == null)
			{
				throw new ArgumentNullException("exception");
			}
			bool flag = this.m_task.TrySetException(exception);
			if (!flag && !this.m_task.IsCompleted)
			{
				this.SpinUntilCompleted();
			}
			return flag;
		}

		/// <summary>Attempts to transition the underlying <see cref="T:System.Threading.Tasks.Task`1" /> into the <see cref="F:System.Threading.Tasks.TaskStatus.Faulted" /> state.</summary>
		/// <returns>True if the operation was successful; otherwise, false.</returns>
		/// <param name="exceptions">The collection of exceptions to bind to this <see cref="T:System.Threading.Tasks.Task`1" />.</param>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="P:System.Threading.Tasks.TaskCompletionSource`1.Task" /> was disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="exceptions" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentException">There are one or more null elements in <paramref name="exceptions" />.-or-The <paramref name="exceptions" /> collection is empty.</exception>
		// Token: 0x06003B2A RID: 15146 RVA: 0x000D65D4 File Offset: 0x000D47D4
		public bool TrySetException(IEnumerable<Exception> exceptions)
		{
			if (exceptions == null)
			{
				throw new ArgumentNullException("exceptions");
			}
			List<Exception> list = new List<Exception>();
			foreach (Exception ex in exceptions)
			{
				if (ex == null)
				{
					throw new ArgumentException(Environment.GetResourceString("The exceptions collection included at least one null element."), "exceptions");
				}
				list.Add(ex);
			}
			if (list.Count == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("The exceptions collection was empty."), "exceptions");
			}
			bool flag = this.m_task.TrySetException(list);
			if (!flag && !this.m_task.IsCompleted)
			{
				this.SpinUntilCompleted();
			}
			return flag;
		}

		// Token: 0x06003B2B RID: 15147 RVA: 0x000D6688 File Offset: 0x000D4888
		internal bool TrySetException(IEnumerable<ExceptionDispatchInfo> exceptions)
		{
			bool flag = this.m_task.TrySetException(exceptions);
			if (!flag && !this.m_task.IsCompleted)
			{
				this.SpinUntilCompleted();
			}
			return flag;
		}

		/// <summary>Transitions the underlying <see cref="T:System.Threading.Tasks.Task`1" /> into the <see cref="F:System.Threading.Tasks.TaskStatus.Faulted" /> state.</summary>
		/// <param name="exception">The exception to bind to this <see cref="T:System.Threading.Tasks.Task`1" />.</param>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="P:System.Threading.Tasks.TaskCompletionSource`1.Task" /> was disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="exception" /> argument is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The underlying <see cref="T:System.Threading.Tasks.Task`1" /> is already in one of the three final states: <see cref="F:System.Threading.Tasks.TaskStatus.RanToCompletion" />, <see cref="F:System.Threading.Tasks.TaskStatus.Faulted" />, or <see cref="F:System.Threading.Tasks.TaskStatus.Canceled" />.</exception>
		// Token: 0x06003B2C RID: 15148 RVA: 0x000D66AC File Offset: 0x000D48AC
		public void SetException(Exception exception)
		{
			if (exception == null)
			{
				throw new ArgumentNullException("exception");
			}
			if (!this.TrySetException(exception))
			{
				throw new InvalidOperationException(Environment.GetResourceString("An attempt was made to transition a task to a final state when it had already completed."));
			}
		}

		/// <summary>Transitions the underlying <see cref="T:System.Threading.Tasks.Task`1" /> into the <see cref="F:System.Threading.Tasks.TaskStatus.Faulted" /> state.</summary>
		/// <param name="exceptions">The collection of exceptions to bind to this <see cref="T:System.Threading.Tasks.Task`1" />.</param>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="P:System.Threading.Tasks.TaskCompletionSource`1.Task" /> was disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="exceptions" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentException">There are one or more null elements in <paramref name="exceptions" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The underlying <see cref="T:System.Threading.Tasks.Task`1" /> is already in one of the three final states: <see cref="F:System.Threading.Tasks.TaskStatus.RanToCompletion" />, <see cref="F:System.Threading.Tasks.TaskStatus.Faulted" />, or <see cref="F:System.Threading.Tasks.TaskStatus.Canceled" />.</exception>
		// Token: 0x06003B2D RID: 15149 RVA: 0x000D66D5 File Offset: 0x000D48D5
		public void SetException(IEnumerable<Exception> exceptions)
		{
			if (!this.TrySetException(exceptions))
			{
				throw new InvalidOperationException(Environment.GetResourceString("An attempt was made to transition a task to a final state when it had already completed."));
			}
		}

		/// <summary>Attempts to transition the underlying <see cref="T:System.Threading.Tasks.Task`1" /> into the <see cref="F:System.Threading.Tasks.TaskStatus.RanToCompletion" /> state.</summary>
		/// <returns>True if the operation was successful; otherwise, false. </returns>
		/// <param name="result">The result value to bind to this <see cref="T:System.Threading.Tasks.Task`1" />.</param>
		// Token: 0x06003B2E RID: 15150 RVA: 0x000D66F0 File Offset: 0x000D48F0
		public bool TrySetResult(TResult result)
		{
			bool flag = this.m_task.TrySetResult(result);
			if (!flag && !this.m_task.IsCompleted)
			{
				this.SpinUntilCompleted();
			}
			return flag;
		}

		/// <summary>Transitions the underlying <see cref="T:System.Threading.Tasks.Task`1" /> into the <see cref="F:System.Threading.Tasks.TaskStatus.RanToCompletion" /> state.</summary>
		/// <param name="result">The result value to bind to this <see cref="T:System.Threading.Tasks.Task`1" />.</param>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="P:System.Threading.Tasks.TaskCompletionSource`1.Task" /> was disposed.</exception>
		/// <exception cref="T:System.InvalidOperationException">The underlying <see cref="T:System.Threading.Tasks.Task`1" /> is already in one of the three final states: <see cref="F:System.Threading.Tasks.TaskStatus.RanToCompletion" />, <see cref="F:System.Threading.Tasks.TaskStatus.Faulted" />, or <see cref="F:System.Threading.Tasks.TaskStatus.Canceled" />.</exception>
		// Token: 0x06003B2F RID: 15151 RVA: 0x000D6714 File Offset: 0x000D4914
		public void SetResult(TResult result)
		{
			if (!this.TrySetResult(result))
			{
				throw new InvalidOperationException(Environment.GetResourceString("An attempt was made to transition a task to a final state when it had already completed."));
			}
		}

		/// <summary>Attempts to transition the underlying <see cref="T:System.Threading.Tasks.Task`1" /> into the <see cref="F:System.Threading.Tasks.TaskStatus.Canceled" /> state.</summary>
		/// <returns>True if the operation was successful; false if the operation was unsuccessful or the object has already been disposed.</returns>
		// Token: 0x06003B30 RID: 15152 RVA: 0x000D6730 File Offset: 0x000D4930
		public bool TrySetCanceled()
		{
			return this.TrySetCanceled(default(CancellationToken));
		}

		// Token: 0x06003B31 RID: 15153 RVA: 0x000D674C File Offset: 0x000D494C
		public bool TrySetCanceled(CancellationToken cancellationToken)
		{
			bool flag = this.m_task.TrySetCanceled(cancellationToken);
			if (!flag && !this.m_task.IsCompleted)
			{
				this.SpinUntilCompleted();
			}
			return flag;
		}

		/// <summary>Transitions the underlying <see cref="T:System.Threading.Tasks.Task`1" /> into the <see cref="F:System.Threading.Tasks.TaskStatus.Canceled" /> state.</summary>
		/// <exception cref="T:System.InvalidOperationException">The underlying <see cref="T:System.Threading.Tasks.Task`1" /> is already in one of the three final states: <see cref="F:System.Threading.Tasks.TaskStatus.RanToCompletion" />, <see cref="F:System.Threading.Tasks.TaskStatus.Faulted" />, or <see cref="F:System.Threading.Tasks.TaskStatus.Canceled" />, or if the underlying <see cref="T:System.Threading.Tasks.Task`1" /> has already been disposed.</exception>
		// Token: 0x06003B32 RID: 15154 RVA: 0x000D6770 File Offset: 0x000D4970
		public void SetCanceled()
		{
			if (!this.TrySetCanceled())
			{
				throw new InvalidOperationException(Environment.GetResourceString("An attempt was made to transition a task to a final state when it had already completed."));
			}
		}

		// Token: 0x04001EE7 RID: 7911
		private readonly Task<TResult> m_task;
	}
}
