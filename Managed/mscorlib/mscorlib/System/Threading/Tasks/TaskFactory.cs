using System;
using System.Runtime.CompilerServices;
using System.Runtime.Versioning;
using System.Security.Permissions;

namespace System.Threading.Tasks
{
	/// <summary>Provides support for creating and scheduling <see cref="T:System.Threading.Tasks.Task`1" /> objects.</summary>
	/// <typeparam name="TResult">The type of the results that are available through the <see cref="T:System.Threading.Tasks.Task`1" /> objects that are associated with the methods in this class.</typeparam>
	// Token: 0x020004C7 RID: 1223
	[HostProtection(SecurityAction.LinkDemand, Synchronization = true, ExternalThreading = true)]
	public class TaskFactory<TResult>
	{
		// Token: 0x17000963 RID: 2403
		// (get) Token: 0x06003907 RID: 14599 RVA: 0x000CDC69 File Offset: 0x000CBE69
		private TaskScheduler DefaultScheduler
		{
			get
			{
				if (this.m_defaultScheduler == null)
				{
					return TaskScheduler.Current;
				}
				return this.m_defaultScheduler;
			}
		}

		// Token: 0x06003908 RID: 14600 RVA: 0x000CDC7F File Offset: 0x000CBE7F
		private TaskScheduler GetDefaultScheduler(Task currTask)
		{
			if (this.m_defaultScheduler != null)
			{
				return this.m_defaultScheduler;
			}
			if (currTask != null && (currTask.CreationOptions & TaskCreationOptions.HideScheduler) == TaskCreationOptions.None)
			{
				return currTask.ExecutingTaskScheduler;
			}
			return TaskScheduler.Default;
		}

		/// <summary>Initializes a <see cref="T:System.Threading.Tasks.TaskFactory`1" /> instance with the default configuration.</summary>
		// Token: 0x06003909 RID: 14601 RVA: 0x000CDCAC File Offset: 0x000CBEAC
		public TaskFactory()
			: this(default(CancellationToken), TaskCreationOptions.None, TaskContinuationOptions.None, null)
		{
		}

		/// <summary>Initializes a <see cref="T:System.Threading.Tasks.TaskFactory`1" /> instance with the default configuration.</summary>
		/// <param name="cancellationToken">The default cancellation token that will be assigned to tasks created by this <see cref="T:System.Threading.Tasks.TaskFactory" /> unless another cancellation token is explicitly specified when calling the factory methods.</param>
		// Token: 0x0600390A RID: 14602 RVA: 0x000CDCCB File Offset: 0x000CBECB
		public TaskFactory(CancellationToken cancellationToken)
			: this(cancellationToken, TaskCreationOptions.None, TaskContinuationOptions.None, null)
		{
		}

		/// <summary>Initializes a <see cref="T:System.Threading.Tasks.TaskFactory`1" /> instance with the specified configuration.</summary>
		/// <param name="scheduler">The scheduler to use to schedule any tasks created with this <see cref="T:System.Threading.Tasks.TaskFactory`1" />. A null value indicates that the current <see cref="T:System.Threading.Tasks.TaskScheduler" /> should be used.</param>
		// Token: 0x0600390B RID: 14603 RVA: 0x000CDCD8 File Offset: 0x000CBED8
		public TaskFactory(TaskScheduler scheduler)
			: this(default(CancellationToken), TaskCreationOptions.None, TaskContinuationOptions.None, scheduler)
		{
		}

		/// <summary>Initializes a <see cref="T:System.Threading.Tasks.TaskFactory`1" /> instance with the specified configuration.</summary>
		/// <param name="creationOptions">The default options to use when creating tasks with this <see cref="T:System.Threading.Tasks.TaskFactory`1" />.</param>
		/// <param name="continuationOptions">The default options to use when creating continuation tasks with this <see cref="T:System.Threading.Tasks.TaskFactory`1" />.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="creationOptions" /> or <paramref name="continuationOptions" /> specifies an invalid value.</exception>
		// Token: 0x0600390C RID: 14604 RVA: 0x000CDCF8 File Offset: 0x000CBEF8
		public TaskFactory(TaskCreationOptions creationOptions, TaskContinuationOptions continuationOptions)
			: this(default(CancellationToken), creationOptions, continuationOptions, null)
		{
		}

		/// <summary>Initializes a <see cref="T:System.Threading.Tasks.TaskFactory`1" /> instance with the specified configuration.</summary>
		/// <param name="cancellationToken">The default cancellation token that will be assigned to tasks created by this <see cref="T:System.Threading.Tasks.TaskFactory" /> unless another cancellation token is explicitly specified when calling the factory methods.</param>
		/// <param name="creationOptions">The default options to use when creating tasks with this <see cref="T:System.Threading.Tasks.TaskFactory`1" />.</param>
		/// <param name="continuationOptions">The default options to use when creating continuation tasks with this <see cref="T:System.Threading.Tasks.TaskFactory`1" />.</param>
		/// <param name="scheduler">The default scheduler to use to schedule any tasks created with this <see cref="T:System.Threading.Tasks.TaskFactory`1" />. A null value indicates that <see cref="P:System.Threading.Tasks.TaskScheduler.Current" /> should be used.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="creationOptions" /> or <paramref name="continuationOptions" /> specifies an invalid value.</exception>
		// Token: 0x0600390D RID: 14605 RVA: 0x000CDD17 File Offset: 0x000CBF17
		public TaskFactory(CancellationToken cancellationToken, TaskCreationOptions creationOptions, TaskContinuationOptions continuationOptions, TaskScheduler scheduler)
		{
			TaskFactory.CheckMultiTaskContinuationOptions(continuationOptions);
			TaskFactory.CheckCreationOptions(creationOptions);
			this.m_defaultCancellationToken = cancellationToken;
			this.m_defaultScheduler = scheduler;
			this.m_defaultCreationOptions = creationOptions;
			this.m_defaultContinuationOptions = continuationOptions;
		}

		/// <summary>Gets the default cancellation token for this task factory.</summary>
		/// <returns>The default cancellation token for this task factory.</returns>
		// Token: 0x17000964 RID: 2404
		// (get) Token: 0x0600390E RID: 14606 RVA: 0x000CDD48 File Offset: 0x000CBF48
		public CancellationToken CancellationToken
		{
			get
			{
				return this.m_defaultCancellationToken;
			}
		}

		/// <summary>Gets the task scheduler for this task factory.</summary>
		/// <returns>The task scheduler for this task factory.</returns>
		// Token: 0x17000965 RID: 2405
		// (get) Token: 0x0600390F RID: 14607 RVA: 0x000CDD50 File Offset: 0x000CBF50
		public TaskScheduler Scheduler
		{
			get
			{
				return this.m_defaultScheduler;
			}
		}

		/// <summary>Gets the <see cref="T:System.Threading.Tasks.TaskCreationOptions" /> enumeration value for this task factory.</summary>
		/// <returns>One of the enumeration values that specifies the default creation options for this task factory.</returns>
		// Token: 0x17000966 RID: 2406
		// (get) Token: 0x06003910 RID: 14608 RVA: 0x000CDD58 File Offset: 0x000CBF58
		public TaskCreationOptions CreationOptions
		{
			get
			{
				return this.m_defaultCreationOptions;
			}
		}

		/// <summary>Gets the <see cref="T:System.Threading.Tasks.TaskContinuationOptions" /> enumeration value for this task factory.</summary>
		/// <returns>One of the enumeration values that specifies the default continuation options for this task factory.</returns>
		// Token: 0x17000967 RID: 2407
		// (get) Token: 0x06003911 RID: 14609 RVA: 0x000CDD60 File Offset: 0x000CBF60
		public TaskContinuationOptions ContinuationOptions
		{
			get
			{
				return this.m_defaultContinuationOptions;
			}
		}

		/// <summary>Creates and starts a task.</summary>
		/// <returns>The started task.</returns>
		/// <param name="function">A function delegate that returns the future result to be available through the task.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="function" /> argument is null.</exception>
		// Token: 0x06003912 RID: 14610 RVA: 0x000CDD68 File Offset: 0x000CBF68
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task<TResult> StartNew(Func<TResult> function)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			Task internalCurrent = Task.InternalCurrent;
			return Task<TResult>.StartNew(internalCurrent, function, this.m_defaultCancellationToken, this.m_defaultCreationOptions, InternalTaskOptions.None, this.GetDefaultScheduler(internalCurrent), ref stackCrawlMark);
		}

		/// <summary>Creates and starts a task.</summary>
		/// <returns>The started task.</returns>
		/// <param name="function">A function delegate that returns the future result to be available through the task.</param>
		/// <param name="cancellationToken">The cancellation token that will be assigned to the new task.</param>
		/// <exception cref="T:System.ObjectDisposedException">The cancellation token source that created<paramref name="cancellationToken" /> has already been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="function" /> argument is null.</exception>
		// Token: 0x06003913 RID: 14611 RVA: 0x000CDD9C File Offset: 0x000CBF9C
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task<TResult> StartNew(Func<TResult> function, CancellationToken cancellationToken)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			Task internalCurrent = Task.InternalCurrent;
			return Task<TResult>.StartNew(internalCurrent, function, cancellationToken, this.m_defaultCreationOptions, InternalTaskOptions.None, this.GetDefaultScheduler(internalCurrent), ref stackCrawlMark);
		}

		/// <summary>Creates and starts a task.</summary>
		/// <returns>The started <see cref="T:System.Threading.Tasks.Task`1" />.</returns>
		/// <param name="function">A function delegate that returns the future result to be available through the task.</param>
		/// <param name="creationOptions">One of the enumeration values that controls the behavior of the created task.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="function" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="creationOptions" /> parameter specifies an invalid value.</exception>
		// Token: 0x06003914 RID: 14612 RVA: 0x000CDDCC File Offset: 0x000CBFCC
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task<TResult> StartNew(Func<TResult> function, TaskCreationOptions creationOptions)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			Task internalCurrent = Task.InternalCurrent;
			return Task<TResult>.StartNew(internalCurrent, function, this.m_defaultCancellationToken, creationOptions, InternalTaskOptions.None, this.GetDefaultScheduler(internalCurrent), ref stackCrawlMark);
		}

		/// <summary>Creates and starts a task.</summary>
		/// <returns>The started task.</returns>
		/// <param name="function">A function delegate that returns the future result to be available through the task.</param>
		/// <param name="cancellationToken">The cancellation token that will be assigned to the new task.</param>
		/// <param name="creationOptions">One of the enumeration values that controls the behavior of the created task.</param>
		/// <param name="scheduler">The task scheduler that is used to schedule the created task.</param>
		/// <exception cref="T:System.ObjectDisposedException">The cancellation token source that created<paramref name="cancellationToken" /> has already been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="function" /> argument is null.-or-The <paramref name="scheduler" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="creationOptions" /> parameter specifies an invalid value.</exception>
		// Token: 0x06003915 RID: 14613 RVA: 0x000CDDFC File Offset: 0x000CBFFC
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task<TResult> StartNew(Func<TResult> function, CancellationToken cancellationToken, TaskCreationOptions creationOptions, TaskScheduler scheduler)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return Task<TResult>.StartNew(Task.InternalCurrentIfAttached(creationOptions), function, cancellationToken, creationOptions, InternalTaskOptions.None, scheduler, ref stackCrawlMark);
		}

		/// <summary>Creates and starts a task.</summary>
		/// <returns>The started task.</returns>
		/// <param name="function">A function delegate that returns the future result to be available through the task.</param>
		/// <param name="state">An object that contains data to be used by the <paramref name="function" /> delegate.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="function" /> argument is null.</exception>
		// Token: 0x06003916 RID: 14614 RVA: 0x000CDE20 File Offset: 0x000CC020
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task<TResult> StartNew(Func<object, TResult> function, object state)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			Task internalCurrent = Task.InternalCurrent;
			return Task<TResult>.StartNew(internalCurrent, function, state, this.m_defaultCancellationToken, this.m_defaultCreationOptions, InternalTaskOptions.None, this.GetDefaultScheduler(internalCurrent), ref stackCrawlMark);
		}

		/// <summary>Creates and starts a task.</summary>
		/// <returns>The started task.</returns>
		/// <param name="function">A function delegate that returns the future result to be available through the task.</param>
		/// <param name="state">An object that contains data to be used by the <paramref name="function" /> delegate.</param>
		/// <param name="cancellationToken">The cancellation token that will be assigned to the new task.</param>
		/// <exception cref="T:System.ObjectDisposedException">The cancellation token source that created<paramref name="cancellationToken" /> has already been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="function" /> argument is null.</exception>
		// Token: 0x06003917 RID: 14615 RVA: 0x000CDE54 File Offset: 0x000CC054
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task<TResult> StartNew(Func<object, TResult> function, object state, CancellationToken cancellationToken)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			Task internalCurrent = Task.InternalCurrent;
			return Task<TResult>.StartNew(internalCurrent, function, state, cancellationToken, this.m_defaultCreationOptions, InternalTaskOptions.None, this.GetDefaultScheduler(internalCurrent), ref stackCrawlMark);
		}

		/// <summary>Creates and starts a task.</summary>
		/// <returns>The started task.</returns>
		/// <param name="function">A function delegate that returns the future result to be available through the task.</param>
		/// <param name="state">An object that contains data to be used by the <paramref name="function" /> delegate.</param>
		/// <param name="creationOptions">One of the enumeration values that controls the behavior of the created task.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="function" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="creationOptions" /> parameter specifies an invalid value.</exception>
		// Token: 0x06003918 RID: 14616 RVA: 0x000CDE84 File Offset: 0x000CC084
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task<TResult> StartNew(Func<object, TResult> function, object state, TaskCreationOptions creationOptions)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			Task internalCurrent = Task.InternalCurrent;
			return Task<TResult>.StartNew(internalCurrent, function, state, this.m_defaultCancellationToken, creationOptions, InternalTaskOptions.None, this.GetDefaultScheduler(internalCurrent), ref stackCrawlMark);
		}

		/// <summary>Creates and starts a task.</summary>
		/// <returns>The started task.</returns>
		/// <param name="function">A function delegate that returns the future result to be available through the task.</param>
		/// <param name="state">An object that contains data to be used by the <paramref name="function" /> delegate.</param>
		/// <param name="cancellationToken">The cancellation token that will be assigned to the new task.</param>
		/// <param name="creationOptions">One of the enumeration values that controls the behavior of the created task.</param>
		/// <param name="scheduler">The task scheduler that is used to schedule the created task.</param>
		/// <exception cref="T:System.ObjectDisposedException">The cancellation token source that created<paramref name="cancellationToken" /> has already been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="function" /> argument is null.-or-The <paramref name="scheduler" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="creationOptions" /> parameter specifies an invalid value.</exception>
		// Token: 0x06003919 RID: 14617 RVA: 0x000CDEB4 File Offset: 0x000CC0B4
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task<TResult> StartNew(Func<object, TResult> function, object state, CancellationToken cancellationToken, TaskCreationOptions creationOptions, TaskScheduler scheduler)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return Task<TResult>.StartNew(Task.InternalCurrentIfAttached(creationOptions), function, state, cancellationToken, creationOptions, InternalTaskOptions.None, scheduler, ref stackCrawlMark);
		}

		// Token: 0x0600391A RID: 14618 RVA: 0x000CDEDC File Offset: 0x000CC0DC
		private static void FromAsyncCoreLogic(IAsyncResult iar, Func<IAsyncResult, TResult> endFunction, Action<IAsyncResult> endAction, Task<TResult> promise, bool requiresSynchronization)
		{
			Exception ex = null;
			OperationCanceledException ex2 = null;
			TResult tresult = default(TResult);
			try
			{
				if (endFunction != null)
				{
					tresult = endFunction(iar);
				}
				else
				{
					endAction(iar);
				}
			}
			catch (OperationCanceledException ex2)
			{
			}
			catch (Exception ex)
			{
			}
			finally
			{
				if (ex2 != null)
				{
					promise.TrySetCanceled(ex2.CancellationToken, ex2);
				}
				else if (ex != null)
				{
					if (promise.TrySetException(ex) && ex is ThreadAbortException)
					{
						promise.m_contingentProperties.m_exceptionsHolder.MarkAsHandled(false);
					}
				}
				else
				{
					if (AsyncCausalityTracer.LoggingOn)
					{
						AsyncCausalityTracer.TraceOperationCompletion(CausalityTraceLevel.Required, promise.Id, AsyncCausalityStatus.Completed);
					}
					if (Task.s_asyncDebuggingEnabled)
					{
						Task.RemoveFromActiveTasks(promise.Id);
					}
					if (requiresSynchronization)
					{
						promise.TrySetResult(tresult);
					}
					else
					{
						promise.DangerousSetResult(tresult);
					}
				}
			}
		}

		/// <summary>Creates a task that executes an end method function when a specified <see cref="T:System.IAsyncResult" /> completes.</summary>
		/// <returns>A <see cref="T:System.Threading.Tasks.Task`1" /> that represents the asynchronous operation.</returns>
		/// <param name="asyncResult">The <see cref="T:System.IAsyncResult" /> whose completion should trigger the processing of the <paramref name="endMethod" />.</param>
		/// <param name="endMethod">The function delegate that processes the completed <paramref name="asyncResult" />.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="asyncResult" /> argument is null.-or-The <paramref name="endMethod" /> argument is null.</exception>
		// Token: 0x0600391B RID: 14619 RVA: 0x000CDFB8 File Offset: 0x000CC1B8
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task<TResult> FromAsync(IAsyncResult asyncResult, Func<IAsyncResult, TResult> endMethod)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return TaskFactory<TResult>.FromAsyncImpl(asyncResult, endMethod, null, this.m_defaultCreationOptions, this.DefaultScheduler, ref stackCrawlMark);
		}

		/// <summary>Creates a task that executes an end method function when a specified <see cref="T:System.IAsyncResult" /> completes.</summary>
		/// <returns>A task that represents the asynchronous operation.</returns>
		/// <param name="asyncResult">The <see cref="T:System.IAsyncResult" /> whose completion should trigger the processing of the <paramref name="endMethod" />.</param>
		/// <param name="endMethod">The function delegate that processes the completed <paramref name="asyncResult" />.</param>
		/// <param name="creationOptions">One of the enumeration values that controls the behavior of the created task.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="asyncResult" /> argument is null.-or-The <paramref name="endMethod" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="creationOptions" /> argument specifies an invalid value.</exception>
		// Token: 0x0600391C RID: 14620 RVA: 0x000CDFE0 File Offset: 0x000CC1E0
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task<TResult> FromAsync(IAsyncResult asyncResult, Func<IAsyncResult, TResult> endMethod, TaskCreationOptions creationOptions)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return TaskFactory<TResult>.FromAsyncImpl(asyncResult, endMethod, null, creationOptions, this.DefaultScheduler, ref stackCrawlMark);
		}

		/// <summary>Creates a task that executes an end method function when a specified <see cref="T:System.IAsyncResult" /> completes.</summary>
		/// <returns>The created task that represents the asynchronous operation.</returns>
		/// <param name="asyncResult">The <see cref="T:System.IAsyncResult" /> whose completion should trigger the processing of the <paramref name="endMethod" />.</param>
		/// <param name="endMethod">The function delegate that processes the completed <paramref name="asyncResult" />.</param>
		/// <param name="creationOptions">One of the enumeration values that controls the behavior of the created task.</param>
		/// <param name="scheduler">The task scheduler that is used to schedule the task that executes the end method.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="asyncResult" /> argument is null.-or-The <paramref name="endMethod" /> argument is null.-or-The <paramref name="scheduler" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="creationOptions" /> parameter specifies an invalid value.</exception>
		// Token: 0x0600391D RID: 14621 RVA: 0x000CE000 File Offset: 0x000CC200
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task<TResult> FromAsync(IAsyncResult asyncResult, Func<IAsyncResult, TResult> endMethod, TaskCreationOptions creationOptions, TaskScheduler scheduler)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return TaskFactory<TResult>.FromAsyncImpl(asyncResult, endMethod, null, creationOptions, scheduler, ref stackCrawlMark);
		}

		// Token: 0x0600391E RID: 14622 RVA: 0x000CE01C File Offset: 0x000CC21C
		internal static Task<TResult> FromAsyncImpl(IAsyncResult asyncResult, Func<IAsyncResult, TResult> endFunction, Action<IAsyncResult> endAction, TaskCreationOptions creationOptions, TaskScheduler scheduler, ref StackCrawlMark stackMark)
		{
			if (asyncResult == null)
			{
				throw new ArgumentNullException("asyncResult");
			}
			if (endFunction == null && endAction == null)
			{
				throw new ArgumentNullException("endMethod");
			}
			if (scheduler == null)
			{
				throw new ArgumentNullException("scheduler");
			}
			TaskFactory.CheckFromAsyncOptions(creationOptions, false);
			Task<TResult> promise = new Task<TResult>(null, creationOptions);
			if (AsyncCausalityTracer.LoggingOn)
			{
				AsyncCausalityTracer.TraceOperationCreation(CausalityTraceLevel.Required, promise.Id, "TaskFactory.FromAsync", 0UL);
			}
			if (Task.s_asyncDebuggingEnabled)
			{
				Task.AddToActiveTasks(promise);
			}
			Task t = new Task(delegate
			{
				TaskFactory<TResult>.FromAsyncCoreLogic(asyncResult, endFunction, endAction, promise, true);
			}, null, null, default(CancellationToken), TaskCreationOptions.None, InternalTaskOptions.None, null, ref stackMark);
			if (AsyncCausalityTracer.LoggingOn)
			{
				AsyncCausalityTracer.TraceOperationCreation(CausalityTraceLevel.Verbose, t.Id, "TaskFactory.FromAsync Callback", 0UL);
			}
			if (Task.s_asyncDebuggingEnabled)
			{
				Task.AddToActiveTasks(t);
			}
			if (asyncResult.IsCompleted)
			{
				try
				{
					t.InternalRunSynchronously(scheduler, false);
					goto IL_0154;
				}
				catch (Exception ex)
				{
					promise.TrySetException(ex);
					goto IL_0154;
				}
			}
			ThreadPool.RegisterWaitForSingleObject(asyncResult.AsyncWaitHandle, delegate
			{
				try
				{
					t.InternalRunSynchronously(scheduler, false);
				}
				catch (Exception ex2)
				{
					promise.TrySetException(ex2);
				}
			}, null, -1, true);
			IL_0154:
			return promise;
		}

		/// <summary>Creates a task that represents a pair of begin and end methods that conform to the Asynchronous Programming Model pattern.</summary>
		/// <returns>The created task that represents the asynchronous operation.</returns>
		/// <param name="beginMethod">The delegate that begins the asynchronous operation.</param>
		/// <param name="endMethod">The delegate that ends the asynchronous operation.</param>
		/// <param name="state">An object containing data to be used by the <paramref name="beginMethod" /> delegate.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="beginMethod" /> argument is null.-or-The <paramref name="endMethod" /> argument is null.</exception>
		// Token: 0x0600391F RID: 14623 RVA: 0x000CE194 File Offset: 0x000CC394
		public Task<TResult> FromAsync(Func<AsyncCallback, object, IAsyncResult> beginMethod, Func<IAsyncResult, TResult> endMethod, object state)
		{
			return TaskFactory<TResult>.FromAsyncImpl(beginMethod, endMethod, null, state, this.m_defaultCreationOptions);
		}

		/// <summary>Creates a task that represents a pair of begin and end methods that conform to the Asynchronous Programming Model pattern.</summary>
		/// <returns>The created <see cref="T:System.Threading.Tasks.Task`1" /> that represents the asynchronous operation.</returns>
		/// <param name="beginMethod">The delegate that begins the asynchronous operation.</param>
		/// <param name="endMethod">The delegate that ends the asynchronous operation.</param>
		/// <param name="state">An object containing data to be used by the <paramref name="beginMethod" /> delegate.</param>
		/// <param name="creationOptions">One of the enumeration values that controls the behavior of the created task.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="beginMethod" /> argument is null.-or-The <paramref name="endMethod" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="creationOptions" /> argument specifies an invalid value.</exception>
		// Token: 0x06003920 RID: 14624 RVA: 0x000CE1A5 File Offset: 0x000CC3A5
		public Task<TResult> FromAsync(Func<AsyncCallback, object, IAsyncResult> beginMethod, Func<IAsyncResult, TResult> endMethod, object state, TaskCreationOptions creationOptions)
		{
			return TaskFactory<TResult>.FromAsyncImpl(beginMethod, endMethod, null, state, creationOptions);
		}

		// Token: 0x06003921 RID: 14625 RVA: 0x000CE1B4 File Offset: 0x000CC3B4
		internal static Task<TResult> FromAsyncImpl(Func<AsyncCallback, object, IAsyncResult> beginMethod, Func<IAsyncResult, TResult> endFunction, Action<IAsyncResult> endAction, object state, TaskCreationOptions creationOptions)
		{
			if (beginMethod == null)
			{
				throw new ArgumentNullException("beginMethod");
			}
			if (endFunction == null && endAction == null)
			{
				throw new ArgumentNullException("endMethod");
			}
			TaskFactory.CheckFromAsyncOptions(creationOptions, true);
			Task<TResult> promise = new Task<TResult>(state, creationOptions);
			if (AsyncCausalityTracer.LoggingOn)
			{
				AsyncCausalityTracer.TraceOperationCreation(CausalityTraceLevel.Required, promise.Id, "TaskFactory.FromAsync: " + beginMethod.Method.Name, 0UL);
			}
			if (Task.s_asyncDebuggingEnabled)
			{
				Task.AddToActiveTasks(promise);
			}
			try
			{
				if (BinaryCompatibility.TargetsAtLeast_Desktop_V4_5)
				{
					AtomicBoolean invoked = new AtomicBoolean();
					IAsyncResult asyncResult = beginMethod(delegate(IAsyncResult iar)
					{
						if (invoked.TryRelaxedSet())
						{
							TaskFactory<TResult>.FromAsyncCoreLogic(iar, endFunction, endAction, promise, true);
						}
					}, state);
					if (asyncResult != null && asyncResult.CompletedSynchronously && invoked.TryRelaxedSet())
					{
						TaskFactory<TResult>.FromAsyncCoreLogic(asyncResult, endFunction, endAction, promise, false);
					}
				}
				else
				{
					beginMethod(delegate(IAsyncResult iar)
					{
						TaskFactory<TResult>.FromAsyncCoreLogic(iar, endFunction, endAction, promise, true);
					}, state);
				}
			}
			catch
			{
				if (AsyncCausalityTracer.LoggingOn)
				{
					AsyncCausalityTracer.TraceOperationCompletion(CausalityTraceLevel.Required, promise.Id, AsyncCausalityStatus.Error);
				}
				if (Task.s_asyncDebuggingEnabled)
				{
					Task.RemoveFromActiveTasks(promise.Id);
				}
				promise.TrySetResult(default(TResult));
				throw;
			}
			return promise;
		}

		/// <summary>Creates a task that represents a pair of begin and end methods that conform to the Asynchronous Programming Model pattern.</summary>
		/// <returns>The created task that represents the asynchronous operation.</returns>
		/// <param name="beginMethod">The delegate that begins the asynchronous operation.</param>
		/// <param name="endMethod">The delegate that ends the asynchronous operation.</param>
		/// <param name="arg1">The first argument passed to the <paramref name="beginMethod" /> delegate.</param>
		/// <param name="state">An object containing data to be used by the <paramref name="beginMethod" /> delegate.</param>
		/// <typeparam name="TArg1">The type of the first argument passed to the <paramref name="beginMethod" /> delegate.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="beginMethod" /> argument is null.-or-The <paramref name="endMethod" /> argument is null.</exception>
		// Token: 0x06003922 RID: 14626 RVA: 0x000CE340 File Offset: 0x000CC540
		public Task<TResult> FromAsync<TArg1>(Func<TArg1, AsyncCallback, object, IAsyncResult> beginMethod, Func<IAsyncResult, TResult> endMethod, TArg1 arg1, object state)
		{
			return TaskFactory<TResult>.FromAsyncImpl<TArg1>(beginMethod, endMethod, null, arg1, state, this.m_defaultCreationOptions);
		}

		/// <summary>Creates a task that represents a pair of begin and end methods that conform to the Asynchronous Programming Model pattern.</summary>
		/// <returns>The created task that represents the asynchronous operation.</returns>
		/// <param name="beginMethod">The delegate that begins the asynchronous operation.</param>
		/// <param name="endMethod">The delegate that ends the asynchronous operation.</param>
		/// <param name="arg1">The first argument passed to the <paramref name="beginMethod" /> delegate.</param>
		/// <param name="state">An object containing data to be used by the <paramref name="beginMethod" /> delegate.</param>
		/// <param name="creationOptions">One of the enumeration values that controls the behavior of the created task.</param>
		/// <typeparam name="TArg1">The type of the first argument passed to the <paramref name="beginMethod" /> delegate.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="beginMethod" /> argument is null.-or-The <paramref name="endMethod" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="creationOptions" /> parameter specifies an invalid value.</exception>
		// Token: 0x06003923 RID: 14627 RVA: 0x000CE353 File Offset: 0x000CC553
		public Task<TResult> FromAsync<TArg1>(Func<TArg1, AsyncCallback, object, IAsyncResult> beginMethod, Func<IAsyncResult, TResult> endMethod, TArg1 arg1, object state, TaskCreationOptions creationOptions)
		{
			return TaskFactory<TResult>.FromAsyncImpl<TArg1>(beginMethod, endMethod, null, arg1, state, creationOptions);
		}

		// Token: 0x06003924 RID: 14628 RVA: 0x000CE364 File Offset: 0x000CC564
		internal static Task<TResult> FromAsyncImpl<TArg1>(Func<TArg1, AsyncCallback, object, IAsyncResult> beginMethod, Func<IAsyncResult, TResult> endFunction, Action<IAsyncResult> endAction, TArg1 arg1, object state, TaskCreationOptions creationOptions)
		{
			if (beginMethod == null)
			{
				throw new ArgumentNullException("beginMethod");
			}
			if (endFunction == null && endAction == null)
			{
				throw new ArgumentNullException("endFunction");
			}
			TaskFactory.CheckFromAsyncOptions(creationOptions, true);
			Task<TResult> promise = new Task<TResult>(state, creationOptions);
			if (AsyncCausalityTracer.LoggingOn)
			{
				AsyncCausalityTracer.TraceOperationCreation(CausalityTraceLevel.Required, promise.Id, "TaskFactory.FromAsync: " + beginMethod.Method.Name, 0UL);
			}
			if (Task.s_asyncDebuggingEnabled)
			{
				Task.AddToActiveTasks(promise);
			}
			try
			{
				if (BinaryCompatibility.TargetsAtLeast_Desktop_V4_5)
				{
					AtomicBoolean invoked = new AtomicBoolean();
					IAsyncResult asyncResult = beginMethod(arg1, delegate(IAsyncResult iar)
					{
						if (invoked.TryRelaxedSet())
						{
							TaskFactory<TResult>.FromAsyncCoreLogic(iar, endFunction, endAction, promise, true);
						}
					}, state);
					if (asyncResult != null && asyncResult.CompletedSynchronously && invoked.TryRelaxedSet())
					{
						TaskFactory<TResult>.FromAsyncCoreLogic(asyncResult, endFunction, endAction, promise, false);
					}
				}
				else
				{
					beginMethod(arg1, delegate(IAsyncResult iar)
					{
						TaskFactory<TResult>.FromAsyncCoreLogic(iar, endFunction, endAction, promise, true);
					}, state);
				}
			}
			catch
			{
				if (AsyncCausalityTracer.LoggingOn)
				{
					AsyncCausalityTracer.TraceOperationCompletion(CausalityTraceLevel.Required, promise.Id, AsyncCausalityStatus.Error);
				}
				if (Task.s_asyncDebuggingEnabled)
				{
					Task.RemoveFromActiveTasks(promise.Id);
				}
				promise.TrySetResult(default(TResult));
				throw;
			}
			return promise;
		}

		/// <summary>Creates a task that represents a pair of begin and end methods that conform to the Asynchronous Programming Model pattern.</summary>
		/// <returns>The created task that represents the asynchronous operation.</returns>
		/// <param name="beginMethod">The delegate that begins the asynchronous operation.</param>
		/// <param name="endMethod">The delegate that ends the asynchronous operation.</param>
		/// <param name="arg1">The first argument passed to the <paramref name="beginMethod" /> delegate.</param>
		/// <param name="arg2">The second argument passed to the <paramref name="beginMethod" /> delegate.</param>
		/// <param name="state">An object containing data to be used by the <paramref name="beginMethod" /> delegate.</param>
		/// <typeparam name="TArg1">The type of the second argument passed to <paramref name="beginMethod" /> delegate.</typeparam>
		/// <typeparam name="TArg2">The type of the first argument passed to the <paramref name="beginMethod" /> delegate.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="beginMethod" /> argument is null.-or-The <paramref name="endMethod" /> argument is null.</exception>
		// Token: 0x06003925 RID: 14629 RVA: 0x000CE4F8 File Offset: 0x000CC6F8
		public Task<TResult> FromAsync<TArg1, TArg2>(Func<TArg1, TArg2, AsyncCallback, object, IAsyncResult> beginMethod, Func<IAsyncResult, TResult> endMethod, TArg1 arg1, TArg2 arg2, object state)
		{
			return TaskFactory<TResult>.FromAsyncImpl<TArg1, TArg2>(beginMethod, endMethod, null, arg1, arg2, state, this.m_defaultCreationOptions);
		}

		/// <summary>Creates a task that represents a pair of begin and end methods that conform to the Asynchronous Programming Model pattern.</summary>
		/// <returns>The created task that represents the asynchronous operation.</returns>
		/// <param name="beginMethod">The delegate that begins the asynchronous operation.</param>
		/// <param name="endMethod">The delegate that ends the asynchronous operation.</param>
		/// <param name="arg1">The first argument passed to the <paramref name="beginMethod" /> delegate.</param>
		/// <param name="arg2">The second argument passed to the <paramref name="beginMethod" /> delegate.</param>
		/// <param name="state">An object containing data to be used by the <paramref name="beginMethod" /> delegate.</param>
		/// <param name="creationOptions">An object that controls the behavior of the created <see cref="T:System.Threading.Tasks.Task`1" />.</param>
		/// <typeparam name="TArg1">The type of the second argument passed to <paramref name="beginMethod" /> delegate.</typeparam>
		/// <typeparam name="TArg2">The type of the first argument passed to the <paramref name="beginMethod" /> delegate.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="beginMethod" /> argument is null.-or-The <paramref name="endMethod" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="creationOptions" /> parameter specifies an invalid value.</exception>
		// Token: 0x06003926 RID: 14630 RVA: 0x000CE50D File Offset: 0x000CC70D
		public Task<TResult> FromAsync<TArg1, TArg2>(Func<TArg1, TArg2, AsyncCallback, object, IAsyncResult> beginMethod, Func<IAsyncResult, TResult> endMethod, TArg1 arg1, TArg2 arg2, object state, TaskCreationOptions creationOptions)
		{
			return TaskFactory<TResult>.FromAsyncImpl<TArg1, TArg2>(beginMethod, endMethod, null, arg1, arg2, state, creationOptions);
		}

		// Token: 0x06003927 RID: 14631 RVA: 0x000CE520 File Offset: 0x000CC720
		internal static Task<TResult> FromAsyncImpl<TArg1, TArg2>(Func<TArg1, TArg2, AsyncCallback, object, IAsyncResult> beginMethod, Func<IAsyncResult, TResult> endFunction, Action<IAsyncResult> endAction, TArg1 arg1, TArg2 arg2, object state, TaskCreationOptions creationOptions)
		{
			if (beginMethod == null)
			{
				throw new ArgumentNullException("beginMethod");
			}
			if (endFunction == null && endAction == null)
			{
				throw new ArgumentNullException("endMethod");
			}
			TaskFactory.CheckFromAsyncOptions(creationOptions, true);
			Task<TResult> promise = new Task<TResult>(state, creationOptions);
			if (AsyncCausalityTracer.LoggingOn)
			{
				AsyncCausalityTracer.TraceOperationCreation(CausalityTraceLevel.Required, promise.Id, "TaskFactory.FromAsync: " + beginMethod.Method.Name, 0UL);
			}
			if (Task.s_asyncDebuggingEnabled)
			{
				Task.AddToActiveTasks(promise);
			}
			try
			{
				if (BinaryCompatibility.TargetsAtLeast_Desktop_V4_5)
				{
					AtomicBoolean invoked = new AtomicBoolean();
					IAsyncResult asyncResult = beginMethod(arg1, arg2, delegate(IAsyncResult iar)
					{
						if (invoked.TryRelaxedSet())
						{
							TaskFactory<TResult>.FromAsyncCoreLogic(iar, endFunction, endAction, promise, true);
						}
					}, state);
					if (asyncResult != null && asyncResult.CompletedSynchronously && invoked.TryRelaxedSet())
					{
						TaskFactory<TResult>.FromAsyncCoreLogic(asyncResult, endFunction, endAction, promise, false);
					}
				}
				else
				{
					beginMethod(arg1, arg2, delegate(IAsyncResult iar)
					{
						TaskFactory<TResult>.FromAsyncCoreLogic(iar, endFunction, endAction, promise, true);
					}, state);
				}
			}
			catch
			{
				if (AsyncCausalityTracer.LoggingOn)
				{
					AsyncCausalityTracer.TraceOperationCompletion(CausalityTraceLevel.Required, promise.Id, AsyncCausalityStatus.Error);
				}
				if (Task.s_asyncDebuggingEnabled)
				{
					Task.RemoveFromActiveTasks(promise.Id);
				}
				promise.TrySetResult(default(TResult));
				throw;
			}
			return promise;
		}

		/// <summary>Creates a task that represents a pair of begin and end methods that conform to the Asynchronous Programming Model pattern.</summary>
		/// <returns>The created task that represents the asynchronous operation.</returns>
		/// <param name="beginMethod">The delegate that begins the asynchronous operation.</param>
		/// <param name="endMethod">The delegate that ends the asynchronous operation.</param>
		/// <param name="arg1">The first argument passed to the <paramref name="beginMethod" /> delegate.</param>
		/// <param name="arg2">The second argument passed to the <paramref name="beginMethod" /> delegate.</param>
		/// <param name="arg3">The third argument passed to the <paramref name="beginMethod" /> delegate.</param>
		/// <param name="state">An object containing data to be used by the <paramref name="beginMethod" /> delegate.</param>
		/// <typeparam name="TArg1">The type of the second argument passed to <paramref name="beginMethod" /> delegate.</typeparam>
		/// <typeparam name="TArg2">The type of the third argument passed to <paramref name="beginMethod" /> delegate.</typeparam>
		/// <typeparam name="TArg3">The type of the first argument passed to the <paramref name="beginMethod" /> delegate.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="beginMethod" /> argument is null.-or-The <paramref name="endMethod" /> argument is null.</exception>
		// Token: 0x06003928 RID: 14632 RVA: 0x000CE6B8 File Offset: 0x000CC8B8
		public Task<TResult> FromAsync<TArg1, TArg2, TArg3>(Func<TArg1, TArg2, TArg3, AsyncCallback, object, IAsyncResult> beginMethod, Func<IAsyncResult, TResult> endMethod, TArg1 arg1, TArg2 arg2, TArg3 arg3, object state)
		{
			return TaskFactory<TResult>.FromAsyncImpl<TArg1, TArg2, TArg3>(beginMethod, endMethod, null, arg1, arg2, arg3, state, this.m_defaultCreationOptions);
		}

		/// <summary>Creates a task that represents a pair of begin and end methods that conform to the Asynchronous Programming Model pattern.</summary>
		/// <returns>The created task that represents the asynchronous operation.</returns>
		/// <param name="beginMethod">The delegate that begins the asynchronous operation.</param>
		/// <param name="endMethod">The delegate that ends the asynchronous operation.</param>
		/// <param name="arg1">The first argument passed to the <paramref name="beginMethod" /> delegate.</param>
		/// <param name="arg2">The second argument passed to the <paramref name="beginMethod" /> delegate.</param>
		/// <param name="arg3">The third argument passed to the <paramref name="beginMethod" /> delegate.</param>
		/// <param name="state">An object containing data to be used by the <paramref name="beginMethod" /> delegate.</param>
		/// <param name="creationOptions">An object that controls the behavior of the created task.</param>
		/// <typeparam name="TArg1">The type of the second argument passed to <paramref name="beginMethod" /> delegate.</typeparam>
		/// <typeparam name="TArg2">The type of the third argument passed to <paramref name="beginMethod" /> delegate.</typeparam>
		/// <typeparam name="TArg3">The type of the first argument passed to the <paramref name="beginMethod" /> delegate.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="beginMethod" /> argument is null.-or-The <paramref name="endMethod" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="creationOptions" /> parameter specifies an invalid value.</exception>
		// Token: 0x06003929 RID: 14633 RVA: 0x000CE6CF File Offset: 0x000CC8CF
		public Task<TResult> FromAsync<TArg1, TArg2, TArg3>(Func<TArg1, TArg2, TArg3, AsyncCallback, object, IAsyncResult> beginMethod, Func<IAsyncResult, TResult> endMethod, TArg1 arg1, TArg2 arg2, TArg3 arg3, object state, TaskCreationOptions creationOptions)
		{
			return TaskFactory<TResult>.FromAsyncImpl<TArg1, TArg2, TArg3>(beginMethod, endMethod, null, arg1, arg2, arg3, state, creationOptions);
		}

		// Token: 0x0600392A RID: 14634 RVA: 0x000CE6E4 File Offset: 0x000CC8E4
		internal static Task<TResult> FromAsyncImpl<TArg1, TArg2, TArg3>(Func<TArg1, TArg2, TArg3, AsyncCallback, object, IAsyncResult> beginMethod, Func<IAsyncResult, TResult> endFunction, Action<IAsyncResult> endAction, TArg1 arg1, TArg2 arg2, TArg3 arg3, object state, TaskCreationOptions creationOptions)
		{
			if (beginMethod == null)
			{
				throw new ArgumentNullException("beginMethod");
			}
			if (endFunction == null && endAction == null)
			{
				throw new ArgumentNullException("endMethod");
			}
			TaskFactory.CheckFromAsyncOptions(creationOptions, true);
			Task<TResult> promise = new Task<TResult>(state, creationOptions);
			if (AsyncCausalityTracer.LoggingOn)
			{
				AsyncCausalityTracer.TraceOperationCreation(CausalityTraceLevel.Required, promise.Id, "TaskFactory.FromAsync: " + beginMethod.Method.Name, 0UL);
			}
			if (Task.s_asyncDebuggingEnabled)
			{
				Task.AddToActiveTasks(promise);
			}
			try
			{
				if (BinaryCompatibility.TargetsAtLeast_Desktop_V4_5)
				{
					AtomicBoolean invoked = new AtomicBoolean();
					IAsyncResult asyncResult = beginMethod(arg1, arg2, arg3, delegate(IAsyncResult iar)
					{
						if (invoked.TryRelaxedSet())
						{
							TaskFactory<TResult>.FromAsyncCoreLogic(iar, endFunction, endAction, promise, true);
						}
					}, state);
					if (asyncResult != null && asyncResult.CompletedSynchronously && invoked.TryRelaxedSet())
					{
						TaskFactory<TResult>.FromAsyncCoreLogic(asyncResult, endFunction, endAction, promise, false);
					}
				}
				else
				{
					beginMethod(arg1, arg2, arg3, delegate(IAsyncResult iar)
					{
						TaskFactory<TResult>.FromAsyncCoreLogic(iar, endFunction, endAction, promise, true);
					}, state);
				}
			}
			catch
			{
				if (AsyncCausalityTracer.LoggingOn)
				{
					AsyncCausalityTracer.TraceOperationCompletion(CausalityTraceLevel.Required, promise.Id, AsyncCausalityStatus.Error);
				}
				if (Task.s_asyncDebuggingEnabled)
				{
					Task.RemoveFromActiveTasks(promise.Id);
				}
				promise.TrySetResult(default(TResult));
				throw;
			}
			return promise;
		}

		// Token: 0x0600392B RID: 14635 RVA: 0x000CE880 File Offset: 0x000CCA80
		internal static Task<TResult> FromAsyncTrim<TInstance, TArgs>(TInstance thisRef, TArgs args, Func<TInstance, TArgs, AsyncCallback, object, IAsyncResult> beginMethod, Func<TInstance, IAsyncResult, TResult> endMethod) where TInstance : class
		{
			TaskFactory<TResult>.FromAsyncTrimPromise<TInstance> fromAsyncTrimPromise = new TaskFactory<TResult>.FromAsyncTrimPromise<TInstance>(thisRef, endMethod);
			IAsyncResult asyncResult = beginMethod(thisRef, args, TaskFactory<TResult>.FromAsyncTrimPromise<TInstance>.s_completeFromAsyncResult, fromAsyncTrimPromise);
			if (asyncResult != null && asyncResult.CompletedSynchronously)
			{
				fromAsyncTrimPromise.Complete(thisRef, endMethod, asyncResult, false);
			}
			return fromAsyncTrimPromise;
		}

		// Token: 0x0600392C RID: 14636 RVA: 0x000CE8BC File Offset: 0x000CCABC
		private static Task<TResult> CreateCanceledTask(TaskContinuationOptions continuationOptions, CancellationToken ct)
		{
			TaskCreationOptions taskCreationOptions;
			InternalTaskOptions internalTaskOptions;
			Task.CreationOptionsFromContinuationOptions(continuationOptions, out taskCreationOptions, out internalTaskOptions);
			return new Task<TResult>(true, default(TResult), taskCreationOptions, ct);
		}

		/// <summary>Creates a continuation task that will be started upon the completion of a set of provided tasks.</summary>
		/// <returns>The new continuation task.</returns>
		/// <param name="tasks">The array of tasks from which to continue.</param>
		/// <param name="continuationFunction">The function delegate to execute asynchronously when all tasks in the <paramref name="tasks" /> array have completed.</param>
		/// <exception cref="T:System.ObjectDisposedException">One of the elements in the <paramref name="tasks" /> array has been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="tasks" /> array is null.-or-The <paramref name="continuationFunction" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="tasks" /> array contains a null value or is empty.</exception>
		// Token: 0x0600392D RID: 14637 RVA: 0x000CE8E4 File Offset: 0x000CCAE4
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task<TResult> ContinueWhenAll(Task[] tasks, Func<Task[], TResult> continuationFunction)
		{
			if (continuationFunction == null)
			{
				throw new ArgumentNullException("continuationFunction");
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return TaskFactory<TResult>.ContinueWhenAllImpl(tasks, continuationFunction, null, this.m_defaultContinuationOptions, this.m_defaultCancellationToken, this.DefaultScheduler, ref stackCrawlMark);
		}

		/// <summary>Creates a continuation task that will be started upon the completion of a set of provided tasks.</summary>
		/// <returns>The new continuation task.</returns>
		/// <param name="tasks">The array of tasks from which to continue.</param>
		/// <param name="continuationFunction">The function delegate to execute asynchronously when all tasks in the <paramref name="tasks" /> array have completed.</param>
		/// <param name="cancellationToken">The cancellation token that will be assigned to the new continuation task.</param>
		/// <exception cref="T:System.ObjectDisposedException">One of the elements in the <paramref name="tasks" /> array has been disposed.-or-The <see cref="T:System.Threading.CancellationTokenSource" /> that created<paramref name=" cancellationToken" /> has already been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="tasks" /> array is null.-or-<paramref name="continuationFunction" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="tasks" /> array contains a null value or is empty.</exception>
		// Token: 0x0600392E RID: 14638 RVA: 0x000CE920 File Offset: 0x000CCB20
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task<TResult> ContinueWhenAll(Task[] tasks, Func<Task[], TResult> continuationFunction, CancellationToken cancellationToken)
		{
			if (continuationFunction == null)
			{
				throw new ArgumentNullException("continuationFunction");
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return TaskFactory<TResult>.ContinueWhenAllImpl(tasks, continuationFunction, null, this.m_defaultContinuationOptions, cancellationToken, this.DefaultScheduler, ref stackCrawlMark);
		}

		/// <summary>Creates a continuation task that will be started upon the completion of a set of provided Tasks.</summary>
		/// <returns>The new continuation task.</returns>
		/// <param name="tasks">The array of tasks from which to continue.</param>
		/// <param name="continuationFunction">The function delegate to execute asynchronously when all tasks in the <paramref name="tasks" /> array have completed.</param>
		/// <param name="continuationOptions">One of the enumeration values that controls the behavior of the created continuation task. The NotOn* or OnlyOn* values are not valid.</param>
		/// <exception cref="T:System.ObjectDisposedException">One of the elements in the <paramref name="tasks" /> array has been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="tasks" /> array is null.-or-The <paramref name="continuationFunction" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="continuationOptions" /> argument specifies an invalid value.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="tasks" /> array contains a null value or is empty.</exception>
		// Token: 0x0600392F RID: 14639 RVA: 0x000CE954 File Offset: 0x000CCB54
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task<TResult> ContinueWhenAll(Task[] tasks, Func<Task[], TResult> continuationFunction, TaskContinuationOptions continuationOptions)
		{
			if (continuationFunction == null)
			{
				throw new ArgumentNullException("continuationFunction");
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return TaskFactory<TResult>.ContinueWhenAllImpl(tasks, continuationFunction, null, continuationOptions, this.m_defaultCancellationToken, this.DefaultScheduler, ref stackCrawlMark);
		}

		/// <summary>Creates a continuation task that will be started upon the completion of a set of provided Tasks.</summary>
		/// <returns>The new continuation task.</returns>
		/// <param name="tasks">The array of tasks from which to continue.</param>
		/// <param name="continuationFunction">The function delegate to execute asynchronously when all tasks in the <paramref name="tasks" /> array have completed.</param>
		/// <param name="cancellationToken">The cancellation token that will be assigned to the new continuation task.</param>
		/// <param name="continuationOptions">One of the enumeration values that controls the behavior of the created continuation task. The NotOn* or OnlyOn* values are not valid.</param>
		/// <param name="scheduler">The scheduler that is used to schedule the created continuation task.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="tasks" /> array is null.-or-The <paramref name="continuationFunction" /> argument is null.-or-The <paramref name="scheduler" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="tasks" /> array contains a null value or is empty.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="continuationOptions" /> specifies an invalid value.</exception>
		/// <exception cref="T:System.ObjectDisposedException">One of the elements in the <paramref name="tasks" /> array has been disposed.-or-The <see cref="T:System.Threading.CancellationTokenSource" /> that created<paramref name=" cancellationToken" /> has already been disposed.</exception>
		// Token: 0x06003930 RID: 14640 RVA: 0x000CE988 File Offset: 0x000CCB88
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task<TResult> ContinueWhenAll(Task[] tasks, Func<Task[], TResult> continuationFunction, CancellationToken cancellationToken, TaskContinuationOptions continuationOptions, TaskScheduler scheduler)
		{
			if (continuationFunction == null)
			{
				throw new ArgumentNullException("continuationFunction");
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return TaskFactory<TResult>.ContinueWhenAllImpl(tasks, continuationFunction, null, continuationOptions, cancellationToken, scheduler, ref stackCrawlMark);
		}

		/// <summary>Creates a continuation task that will be started upon the completion of a set of provided tasks.</summary>
		/// <returns>The new continuation task.</returns>
		/// <param name="tasks">The array of tasks from which to continue.</param>
		/// <param name="continuationFunction">The function delegate to execute asynchronously when all tasks in the <paramref name="tasks" /> array have completed.</param>
		/// <typeparam name="TAntecedentResult">The type of the result of the antecedent <paramref name="tasks" />.</typeparam>
		/// <exception cref="T:System.ObjectDisposedException">One of the elements in the <paramref name="tasks" /> array has been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="tasks" /> array is null.-or-The <paramref name="continuationFunction" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="tasks" /> array contains a null value or is empty.</exception>
		// Token: 0x06003931 RID: 14641 RVA: 0x000CE9B4 File Offset: 0x000CCBB4
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task<TResult> ContinueWhenAll<TAntecedentResult>(Task<TAntecedentResult>[] tasks, Func<Task<TAntecedentResult>[], TResult> continuationFunction)
		{
			if (continuationFunction == null)
			{
				throw new ArgumentNullException("continuationFunction");
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return TaskFactory<TResult>.ContinueWhenAllImpl<TAntecedentResult>(tasks, continuationFunction, null, this.m_defaultContinuationOptions, this.m_defaultCancellationToken, this.DefaultScheduler, ref stackCrawlMark);
		}

		/// <summary>Creates a continuation task that will be started upon the completion of a set of provided tasks.</summary>
		/// <returns>The new continuation task.</returns>
		/// <param name="tasks">The array of tasks from which to continue.</param>
		/// <param name="continuationFunction">The function delegate to execute asynchronously when all tasks in the <paramref name="tasks" /> array have completed.</param>
		/// <param name="cancellationToken">The cancellation token that will be assigned to the new continuation task.</param>
		/// <typeparam name="TAntecedentResult">The type of the result of the antecedent <paramref name="tasks" />.</typeparam>
		/// <exception cref="T:System.ObjectDisposedException">One of the elements in the <paramref name="tasks" /> array has been disposed.-or-The <see cref="T:System.Threading.CancellationTokenSource" /> that created<paramref name=" cancellationToken" /> has already been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="tasks" /> array is null.-or-The <paramref name="continuationFunction" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="tasks" /> array contains a null value or is empty.</exception>
		// Token: 0x06003932 RID: 14642 RVA: 0x000CE9F0 File Offset: 0x000CCBF0
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task<TResult> ContinueWhenAll<TAntecedentResult>(Task<TAntecedentResult>[] tasks, Func<Task<TAntecedentResult>[], TResult> continuationFunction, CancellationToken cancellationToken)
		{
			if (continuationFunction == null)
			{
				throw new ArgumentNullException("continuationFunction");
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return TaskFactory<TResult>.ContinueWhenAllImpl<TAntecedentResult>(tasks, continuationFunction, null, this.m_defaultContinuationOptions, cancellationToken, this.DefaultScheduler, ref stackCrawlMark);
		}

		/// <summary>Creates a continuation task that will be started upon the completion of a set of provided tasks.</summary>
		/// <returns>The new continuation task.</returns>
		/// <param name="tasks">The array of tasks from which to continue.</param>
		/// <param name="continuationFunction">The function delegate to execute asynchronously when all tasks in the <paramref name="tasks" /> array have completed.</param>
		/// <param name="continuationOptions">One of the enumeration values that controls the behavior of the created continuation task. The NotOn* or OnlyOn* values are not valid.</param>
		/// <typeparam name="TAntecedentResult">The type of the result of the antecedent <paramref name="tasks" />.</typeparam>
		/// <exception cref="T:System.ObjectDisposedException">One of the elements in the <paramref name="tasks" /> array has been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="tasks" /> array is null.-or-The <paramref name="continuationFunction" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="continuationOptions" /> argument specifies an invalid value.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="tasks" /> array contains a null value or is empty.</exception>
		// Token: 0x06003933 RID: 14643 RVA: 0x000CEA24 File Offset: 0x000CCC24
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task<TResult> ContinueWhenAll<TAntecedentResult>(Task<TAntecedentResult>[] tasks, Func<Task<TAntecedentResult>[], TResult> continuationFunction, TaskContinuationOptions continuationOptions)
		{
			if (continuationFunction == null)
			{
				throw new ArgumentNullException("continuationFunction");
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return TaskFactory<TResult>.ContinueWhenAllImpl<TAntecedentResult>(tasks, continuationFunction, null, continuationOptions, this.m_defaultCancellationToken, this.DefaultScheduler, ref stackCrawlMark);
		}

		/// <summary>Creates a continuation task that will be started upon the completion of a set of provided tasks.</summary>
		/// <returns>The new continuation task.</returns>
		/// <param name="tasks">The array of tasks from which to continue.</param>
		/// <param name="continuationFunction">The function delegate to execute asynchronously when all tasks in the <paramref name="tasks" /> array have completed.</param>
		/// <param name="cancellationToken">The cancellation token that will be assigned to the new continuation task.</param>
		/// <param name="continuationOptions">One of the enumeration values that controls the behavior of the created continuation task. The NotOn* or OnlyOn* values are not valid.</param>
		/// <param name="scheduler">The scheduler that is used to schedule the created continuation task.</param>
		/// <typeparam name="TAntecedentResult">The type of the result of the antecedent <paramref name="tasks" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="tasks" /> array is null.-or-The <paramref name="continuationFunction" /> argument is null.-or-The <paramref name="scheduler" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="tasks" /> array contains a null value or is empty.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="continuationOptions" /> argument specifies an invalid value.</exception>
		/// <exception cref="T:System.ObjectDisposedException">One of the elements in the <paramref name="tasks" /> array has been disposed.-or-The <see cref="T:System.Threading.CancellationTokenSource" /> that created<paramref name=" cancellationToken" /> has already been disposed.</exception>
		// Token: 0x06003934 RID: 14644 RVA: 0x000CEA58 File Offset: 0x000CCC58
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task<TResult> ContinueWhenAll<TAntecedentResult>(Task<TAntecedentResult>[] tasks, Func<Task<TAntecedentResult>[], TResult> continuationFunction, CancellationToken cancellationToken, TaskContinuationOptions continuationOptions, TaskScheduler scheduler)
		{
			if (continuationFunction == null)
			{
				throw new ArgumentNullException("continuationFunction");
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return TaskFactory<TResult>.ContinueWhenAllImpl<TAntecedentResult>(tasks, continuationFunction, null, continuationOptions, cancellationToken, scheduler, ref stackCrawlMark);
		}

		// Token: 0x06003935 RID: 14645 RVA: 0x000CEA84 File Offset: 0x000CCC84
		internal static Task<TResult> ContinueWhenAllImpl<TAntecedentResult>(Task<TAntecedentResult>[] tasks, Func<Task<TAntecedentResult>[], TResult> continuationFunction, Action<Task<TAntecedentResult>[]> continuationAction, TaskContinuationOptions continuationOptions, CancellationToken cancellationToken, TaskScheduler scheduler, ref StackCrawlMark stackMark)
		{
			TaskFactory.CheckMultiTaskContinuationOptions(continuationOptions);
			if (tasks == null)
			{
				throw new ArgumentNullException("tasks");
			}
			if (scheduler == null)
			{
				throw new ArgumentNullException("scheduler");
			}
			Task<TAntecedentResult>[] array = TaskFactory.CheckMultiContinuationTasksAndCopy<TAntecedentResult>(tasks);
			if (cancellationToken.IsCancellationRequested && (continuationOptions & TaskContinuationOptions.LazyCancellation) == TaskContinuationOptions.None)
			{
				return TaskFactory<TResult>.CreateCanceledTask(continuationOptions, cancellationToken);
			}
			Task<Task<TAntecedentResult>[]> task = TaskFactory.CommonCWAllLogic<TAntecedentResult>(array);
			if (continuationFunction != null)
			{
				return task.ContinueWith<TResult>(GenericDelegateCache<TAntecedentResult, TResult>.CWAllFuncDelegate, continuationFunction, scheduler, cancellationToken, continuationOptions, ref stackMark);
			}
			return task.ContinueWith<TResult>(GenericDelegateCache<TAntecedentResult, TResult>.CWAllActionDelegate, continuationAction, scheduler, cancellationToken, continuationOptions, ref stackMark);
		}

		// Token: 0x06003936 RID: 14646 RVA: 0x000CEB04 File Offset: 0x000CCD04
		internal static Task<TResult> ContinueWhenAllImpl(Task[] tasks, Func<Task[], TResult> continuationFunction, Action<Task[]> continuationAction, TaskContinuationOptions continuationOptions, CancellationToken cancellationToken, TaskScheduler scheduler, ref StackCrawlMark stackMark)
		{
			TaskFactory.CheckMultiTaskContinuationOptions(continuationOptions);
			if (tasks == null)
			{
				throw new ArgumentNullException("tasks");
			}
			if (scheduler == null)
			{
				throw new ArgumentNullException("scheduler");
			}
			Task[] array = TaskFactory.CheckMultiContinuationTasksAndCopy(tasks);
			if (cancellationToken.IsCancellationRequested && (continuationOptions & TaskContinuationOptions.LazyCancellation) == TaskContinuationOptions.None)
			{
				return TaskFactory<TResult>.CreateCanceledTask(continuationOptions, cancellationToken);
			}
			Task<Task[]> task = TaskFactory.CommonCWAllLogic(array);
			if (continuationFunction != null)
			{
				return task.ContinueWith<TResult>(delegate(Task<Task[]> completedTasks, object state)
				{
					completedTasks.NotifyDebuggerOfWaitCompletionIfNecessary();
					return ((Func<Task[], TResult>)state)(completedTasks.Result);
				}, continuationFunction, scheduler, cancellationToken, continuationOptions, ref stackMark);
			}
			return task.ContinueWith<TResult>(delegate(Task<Task[]> completedTasks, object state)
			{
				completedTasks.NotifyDebuggerOfWaitCompletionIfNecessary();
				((Action<Task[]>)state)(completedTasks.Result);
				return default(TResult);
			}, continuationAction, scheduler, cancellationToken, continuationOptions, ref stackMark);
		}

		/// <summary>Creates a continuation task that will be started upon the completion of any task in the provided set. </summary>
		/// <returns>The new continuation task.</returns>
		/// <param name="tasks">The array of tasks from which to continue when one task completes.</param>
		/// <param name="continuationFunction">The function delegate to execute asynchronously when one task in the <paramref name="tasks" /> array completes.</param>
		/// <exception cref="T:System.ObjectDisposedException">One of the elements in the <paramref name="tasks" /> array has been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="tasks" /> array is null.-or-The <paramref name="continuationFunction" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="tasks" /> array contains a null value or is empty.</exception>
		// Token: 0x06003937 RID: 14647 RVA: 0x000CEBB8 File Offset: 0x000CCDB8
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task<TResult> ContinueWhenAny(Task[] tasks, Func<Task, TResult> continuationFunction)
		{
			if (continuationFunction == null)
			{
				throw new ArgumentNullException("continuationFunction");
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return TaskFactory<TResult>.ContinueWhenAnyImpl(tasks, continuationFunction, null, this.m_defaultContinuationOptions, this.m_defaultCancellationToken, this.DefaultScheduler, ref stackCrawlMark);
		}

		/// <summary>Creates a continuation task that will be started upon the completion of any task in the provided set.</summary>
		/// <returns>The new continuation task.</returns>
		/// <param name="tasks">The array of tasks from which to continue when one task completes.</param>
		/// <param name="continuationFunction">The function delegate to execute asynchronously when one task in the <paramref name="tasks" /> array completes.</param>
		/// <param name="cancellationToken">The cancellation token that will be assigned to the new continuation task.</param>
		/// <exception cref="T:System.ObjectDisposedException">One of the elements in the <paramref name="tasks" /> array has been disposed.-or-The <see cref="T:System.Threading.CancellationTokenSource" /> that created<paramref name=" cancellationToken" /> has already been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="tasks" /> array is null.-or-The <paramref name="continuationFunction" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="tasks" /> array contains a null value.-or-The <paramref name="tasks" /> array is empty.</exception>
		// Token: 0x06003938 RID: 14648 RVA: 0x000CEBF4 File Offset: 0x000CCDF4
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task<TResult> ContinueWhenAny(Task[] tasks, Func<Task, TResult> continuationFunction, CancellationToken cancellationToken)
		{
			if (continuationFunction == null)
			{
				throw new ArgumentNullException("continuationFunction");
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return TaskFactory<TResult>.ContinueWhenAnyImpl(tasks, continuationFunction, null, this.m_defaultContinuationOptions, cancellationToken, this.DefaultScheduler, ref stackCrawlMark);
		}

		/// <summary>Creates a continuation task that will be started upon the completion of any task in the provided set.</summary>
		/// <returns>The new continuation task.</returns>
		/// <param name="tasks">The array of tasks from which to continue when one task completes.</param>
		/// <param name="continuationFunction">The function delegate to execute asynchronously when one task in the <paramref name="tasks" /> array completes.</param>
		/// <param name="continuationOptions">One of the enumeration values that controls the behavior of the created continuation task. The NotOn* or OnlyOn* values are not valid.</param>
		/// <exception cref="T:System.ObjectDisposedException">One of the elements in the <paramref name="tasks" /> array has been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="tasks" /> array is null.-or-The <paramref name="continuationFunction" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="continuationOptions" /> argument specifies an invalid enumeration value.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="tasks" /> array contains a null value.-or-The <paramref name="tasks" /> array is empty.</exception>
		// Token: 0x06003939 RID: 14649 RVA: 0x000CEC28 File Offset: 0x000CCE28
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task<TResult> ContinueWhenAny(Task[] tasks, Func<Task, TResult> continuationFunction, TaskContinuationOptions continuationOptions)
		{
			if (continuationFunction == null)
			{
				throw new ArgumentNullException("continuationFunction");
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return TaskFactory<TResult>.ContinueWhenAnyImpl(tasks, continuationFunction, null, continuationOptions, this.m_defaultCancellationToken, this.DefaultScheduler, ref stackCrawlMark);
		}

		/// <summary>Creates a continuation task that will be started upon the completion of any task in the provided set.</summary>
		/// <returns>The new continuation task.</returns>
		/// <param name="tasks">The array of tasks from which to continue when one task completes.</param>
		/// <param name="continuationFunction">The function delegate to execute asynchronously when one task in the <paramref name="tasks" /> array completes.</param>
		/// <param name="cancellationToken">The cancellation token that will be assigned to the new continuation task.</param>
		/// <param name="continuationOptions">One of the enumeration values that controls the behavior of the created continuation task. The NotOn* or OnlyOn* values are not valid.</param>
		/// <param name="scheduler">The task scheduler that is used to schedule the created continuation task.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="tasks" /> array is null.-or-The <paramref name="continuationFunction" /> argument is null.-or-The <paramref name="scheduler" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="tasks" /> array contains a null value.-or-The <paramref name="tasks" /> array is empty.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="continuationOptions" /> argument specifies an invalid <see cref="T:System.Threading.Tasks.TaskContinuationOptions" /> value.</exception>
		/// <exception cref="T:System.ObjectDisposedException">One of the elements in the <paramref name="tasks" /> array has been disposed.-or-The <see cref="T:System.Threading.CancellationTokenSource" /> that created<paramref name=" cancellationToken" /> has already been disposed. </exception>
		// Token: 0x0600393A RID: 14650 RVA: 0x000CEC5C File Offset: 0x000CCE5C
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task<TResult> ContinueWhenAny(Task[] tasks, Func<Task, TResult> continuationFunction, CancellationToken cancellationToken, TaskContinuationOptions continuationOptions, TaskScheduler scheduler)
		{
			if (continuationFunction == null)
			{
				throw new ArgumentNullException("continuationFunction");
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return TaskFactory<TResult>.ContinueWhenAnyImpl(tasks, continuationFunction, null, continuationOptions, cancellationToken, scheduler, ref stackCrawlMark);
		}

		/// <summary>Creates a continuation task that will be started upon the completion of any task in the provided set.</summary>
		/// <returns>The new continuation <see cref="T:System.Threading.Tasks.Task`1" />.</returns>
		/// <param name="tasks">The array of tasks from which to continue when one task completes.</param>
		/// <param name="continuationFunction">The function delegate to execute asynchronously when one task in the <paramref name="tasks" /> array completes.</param>
		/// <typeparam name="TAntecedentResult">The type of the result of the antecedent <paramref name="tasks" />.</typeparam>
		/// <exception cref="T:System.ObjectDisposedException">One of the elements in the <paramref name="tasks" /> array has been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="tasks" /> array is null.-or-The <paramref name="continuationFunction" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="tasks" /> array contains a null value.-or-The <paramref name="tasks" /> array is empty.</exception>
		// Token: 0x0600393B RID: 14651 RVA: 0x000CEC88 File Offset: 0x000CCE88
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task<TResult> ContinueWhenAny<TAntecedentResult>(Task<TAntecedentResult>[] tasks, Func<Task<TAntecedentResult>, TResult> continuationFunction)
		{
			if (continuationFunction == null)
			{
				throw new ArgumentNullException("continuationFunction");
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return TaskFactory<TResult>.ContinueWhenAnyImpl<TAntecedentResult>(tasks, continuationFunction, null, this.m_defaultContinuationOptions, this.m_defaultCancellationToken, this.DefaultScheduler, ref stackCrawlMark);
		}

		/// <summary>Creates a continuation task that will be started upon the completion of any task in the provided set.</summary>
		/// <returns>The new continuation task.</returns>
		/// <param name="tasks">The array of tasks from which to continue when one task completes.</param>
		/// <param name="continuationFunction">The function delegate to execute asynchronously when one task in the <paramref name="tasks" /> array completes.</param>
		/// <param name="cancellationToken">The cancellation token that will be assigned to the new continuation task.</param>
		/// <typeparam name="TAntecedentResult">The type of the result of the antecedent <paramref name="tasks" />.</typeparam>
		/// <exception cref="T:System.ObjectDisposedException">One of the elements in the <paramref name="tasks" /> array has been disposed.-or-The <see cref="T:System.Threading.CancellationTokenSource" /> that created<paramref name=" cancellationToken" /> has already been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="tasks" /> array is null.-or-The <paramref name="continuationFunction" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="tasks" /> array contains a null value.-or-The <paramref name="tasks" /> array is empty.</exception>
		// Token: 0x0600393C RID: 14652 RVA: 0x000CECC4 File Offset: 0x000CCEC4
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task<TResult> ContinueWhenAny<TAntecedentResult>(Task<TAntecedentResult>[] tasks, Func<Task<TAntecedentResult>, TResult> continuationFunction, CancellationToken cancellationToken)
		{
			if (continuationFunction == null)
			{
				throw new ArgumentNullException("continuationFunction");
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return TaskFactory<TResult>.ContinueWhenAnyImpl<TAntecedentResult>(tasks, continuationFunction, null, this.m_defaultContinuationOptions, cancellationToken, this.DefaultScheduler, ref stackCrawlMark);
		}

		/// <summary>Creates a continuation task that will be started upon the completion of any task in the provided set.</summary>
		/// <returns>The new continuation <see cref="T:System.Threading.Tasks.Task`1" />.</returns>
		/// <param name="tasks">The array of tasks from which to continue when one task completes.</param>
		/// <param name="continuationFunction">The function delegate to execute asynchronously when one task in the <paramref name="tasks" /> array completes.</param>
		/// <param name="continuationOptions">One of the enumeration values that controls the behavior of the created continuation task. The NotOn* or OnlyOn* values are not valid.</param>
		/// <typeparam name="TAntecedentResult">The type of the result of the antecedent <paramref name="tasks" />.</typeparam>
		/// <exception cref="T:System.ObjectDisposedException">One of the elements in the <paramref name="tasks" /> array has been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="tasks" /> array is null.-or-The <paramref name="continuationFunction" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="continuationOptions" /> argument specifies an invalid enumeration value.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="tasks" /> array contains a null value.-or-The <paramref name="tasks" /> array is empty.</exception>
		// Token: 0x0600393D RID: 14653 RVA: 0x000CECF8 File Offset: 0x000CCEF8
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task<TResult> ContinueWhenAny<TAntecedentResult>(Task<TAntecedentResult>[] tasks, Func<Task<TAntecedentResult>, TResult> continuationFunction, TaskContinuationOptions continuationOptions)
		{
			if (continuationFunction == null)
			{
				throw new ArgumentNullException("continuationFunction");
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return TaskFactory<TResult>.ContinueWhenAnyImpl<TAntecedentResult>(tasks, continuationFunction, null, continuationOptions, this.m_defaultCancellationToken, this.DefaultScheduler, ref stackCrawlMark);
		}

		/// <summary>Creates a continuation task that will be started upon the completion of any task in the provided set.</summary>
		/// <returns>The new continuation <see cref="T:System.Threading.Tasks.Task`1" />.</returns>
		/// <param name="tasks">The array of tasks from which to continue when one task completes.</param>
		/// <param name="continuationFunction">The function delegate to execute asynchronously when one task in the <paramref name="tasks" /> array completes.</param>
		/// <param name="cancellationToken">The cancellation token that will be assigned to the new continuation task.</param>
		/// <param name="continuationOptions">One of the enumeration values that controls the behavior of the created continuation task. The NotOn* or OnlyOn* values are not valid.</param>
		/// <param name="scheduler">The <see cref="T:System.Threading.Tasks.TaskScheduler" /> that is used to schedule the created continuation <see cref="T:System.Threading.Tasks.Task`1" />.</param>
		/// <typeparam name="TAntecedentResult">The type of the result of the antecedent <paramref name="tasks" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="tasks" /> array is null.-or-The <paramref name="continuationFunction" /> argument is null.-or-The <paramref name="scheduler" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="tasks" /> array contains a null value.-or-The <paramref name="tasks" /> array is empty.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="continuationOptions" /> argument specifies an invalid TaskContinuationOptions value.</exception>
		/// <exception cref="T:System.ObjectDisposedException">One of the elements in the <paramref name="tasks" /> array has been disposed.-or-The <see cref="T:System.Threading.CancellationTokenSource" /> that created<paramref name=" cancellationToken" /> has already been disposed. </exception>
		// Token: 0x0600393E RID: 14654 RVA: 0x000CED2C File Offset: 0x000CCF2C
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task<TResult> ContinueWhenAny<TAntecedentResult>(Task<TAntecedentResult>[] tasks, Func<Task<TAntecedentResult>, TResult> continuationFunction, CancellationToken cancellationToken, TaskContinuationOptions continuationOptions, TaskScheduler scheduler)
		{
			if (continuationFunction == null)
			{
				throw new ArgumentNullException("continuationFunction");
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return TaskFactory<TResult>.ContinueWhenAnyImpl<TAntecedentResult>(tasks, continuationFunction, null, continuationOptions, cancellationToken, scheduler, ref stackCrawlMark);
		}

		// Token: 0x0600393F RID: 14655 RVA: 0x000CED58 File Offset: 0x000CCF58
		internal static Task<TResult> ContinueWhenAnyImpl(Task[] tasks, Func<Task, TResult> continuationFunction, Action<Task> continuationAction, TaskContinuationOptions continuationOptions, CancellationToken cancellationToken, TaskScheduler scheduler, ref StackCrawlMark stackMark)
		{
			TaskFactory.CheckMultiTaskContinuationOptions(continuationOptions);
			if (tasks == null)
			{
				throw new ArgumentNullException("tasks");
			}
			if (tasks.Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("The tasks argument contains no tasks."), "tasks");
			}
			if (scheduler == null)
			{
				throw new ArgumentNullException("scheduler");
			}
			Task<Task> task = TaskFactory.CommonCWAnyLogic(tasks);
			if (cancellationToken.IsCancellationRequested && (continuationOptions & TaskContinuationOptions.LazyCancellation) == TaskContinuationOptions.None)
			{
				return TaskFactory<TResult>.CreateCanceledTask(continuationOptions, cancellationToken);
			}
			if (continuationFunction != null)
			{
				return task.ContinueWith<TResult>((Task<Task> completedTask, object state) => ((Func<Task, TResult>)state)(completedTask.Result), continuationFunction, scheduler, cancellationToken, continuationOptions, ref stackMark);
			}
			return task.ContinueWith<TResult>(delegate(Task<Task> completedTask, object state)
			{
				((Action<Task>)state)(completedTask.Result);
				return default(TResult);
			}, continuationAction, scheduler, cancellationToken, continuationOptions, ref stackMark);
		}

		// Token: 0x06003940 RID: 14656 RVA: 0x000CEE20 File Offset: 0x000CD020
		internal static Task<TResult> ContinueWhenAnyImpl<TAntecedentResult>(Task<TAntecedentResult>[] tasks, Func<Task<TAntecedentResult>, TResult> continuationFunction, Action<Task<TAntecedentResult>> continuationAction, TaskContinuationOptions continuationOptions, CancellationToken cancellationToken, TaskScheduler scheduler, ref StackCrawlMark stackMark)
		{
			TaskFactory.CheckMultiTaskContinuationOptions(continuationOptions);
			if (tasks == null)
			{
				throw new ArgumentNullException("tasks");
			}
			if (tasks.Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("The tasks argument contains no tasks."), "tasks");
			}
			if (scheduler == null)
			{
				throw new ArgumentNullException("scheduler");
			}
			Task<Task> task = TaskFactory.CommonCWAnyLogic(tasks);
			if (cancellationToken.IsCancellationRequested && (continuationOptions & TaskContinuationOptions.LazyCancellation) == TaskContinuationOptions.None)
			{
				return TaskFactory<TResult>.CreateCanceledTask(continuationOptions, cancellationToken);
			}
			if (continuationFunction != null)
			{
				return task.ContinueWith<TResult>(GenericDelegateCache<TAntecedentResult, TResult>.CWAnyFuncDelegate, continuationFunction, scheduler, cancellationToken, continuationOptions, ref stackMark);
			}
			return task.ContinueWith<TResult>(GenericDelegateCache<TAntecedentResult, TResult>.CWAnyActionDelegate, continuationAction, scheduler, cancellationToken, continuationOptions, ref stackMark);
		}

		// Token: 0x04001DCD RID: 7629
		private CancellationToken m_defaultCancellationToken;

		// Token: 0x04001DCE RID: 7630
		private TaskScheduler m_defaultScheduler;

		// Token: 0x04001DCF RID: 7631
		private TaskCreationOptions m_defaultCreationOptions;

		// Token: 0x04001DD0 RID: 7632
		private TaskContinuationOptions m_defaultContinuationOptions;

		// Token: 0x020004C8 RID: 1224
		private sealed class FromAsyncTrimPromise<TInstance> : Task<TResult> where TInstance : class
		{
			// Token: 0x06003941 RID: 14657 RVA: 0x000CEEB2 File Offset: 0x000CD0B2
			internal FromAsyncTrimPromise(TInstance thisRef, Func<TInstance, IAsyncResult, TResult> endMethod)
			{
				this.m_thisRef = thisRef;
				this.m_endMethod = endMethod;
			}

			// Token: 0x06003942 RID: 14658 RVA: 0x000CEEC8 File Offset: 0x000CD0C8
			internal static void CompleteFromAsyncResult(IAsyncResult asyncResult)
			{
				if (asyncResult == null)
				{
					throw new ArgumentNullException("asyncResult");
				}
				TaskFactory<TResult>.FromAsyncTrimPromise<TInstance> fromAsyncTrimPromise = asyncResult.AsyncState as TaskFactory<TResult>.FromAsyncTrimPromise<TInstance>;
				if (fromAsyncTrimPromise == null)
				{
					throw new ArgumentException(Environment.GetResourceString("Either the IAsyncResult object did not come from the corresponding async method on this type, or the End method was called multiple times with the same IAsyncResult."), "asyncResult");
				}
				TInstance thisRef = fromAsyncTrimPromise.m_thisRef;
				Func<TInstance, IAsyncResult, TResult> endMethod = fromAsyncTrimPromise.m_endMethod;
				fromAsyncTrimPromise.m_thisRef = default(TInstance);
				fromAsyncTrimPromise.m_endMethod = null;
				if (endMethod == null)
				{
					throw new ArgumentException(Environment.GetResourceString("Either the IAsyncResult object did not come from the corresponding async method on this type, or the End method was called multiple times with the same IAsyncResult."), "asyncResult");
				}
				if (!asyncResult.CompletedSynchronously)
				{
					fromAsyncTrimPromise.Complete(thisRef, endMethod, asyncResult, true);
				}
			}

			// Token: 0x06003943 RID: 14659 RVA: 0x000CEF54 File Offset: 0x000CD154
			internal void Complete(TInstance thisRef, Func<TInstance, IAsyncResult, TResult> endMethod, IAsyncResult asyncResult, bool requiresSynchronization)
			{
				try
				{
					TResult tresult = endMethod(thisRef, asyncResult);
					if (requiresSynchronization)
					{
						base.TrySetResult(tresult);
					}
					else
					{
						base.DangerousSetResult(tresult);
					}
				}
				catch (OperationCanceledException ex)
				{
					base.TrySetCanceled(ex.CancellationToken, ex);
				}
				catch (Exception ex2)
				{
					base.TrySetException(ex2);
				}
			}

			// Token: 0x04001DD1 RID: 7633
			internal static readonly AsyncCallback s_completeFromAsyncResult = new AsyncCallback(TaskFactory<TResult>.FromAsyncTrimPromise<TInstance>.CompleteFromAsyncResult);

			// Token: 0x04001DD2 RID: 7634
			private TInstance m_thisRef;

			// Token: 0x04001DD3 RID: 7635
			private Func<TInstance, IAsyncResult, TResult> m_endMethod;
		}
	}
}
