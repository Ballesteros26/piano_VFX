using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Permissions;

namespace System.Threading.Tasks
{
	/// <summary>Provides support for creating and scheduling <see cref="T:System.Threading.Tasks.Task" /> objects.</summary>
	// Token: 0x0200051A RID: 1306
	[HostProtection(SecurityAction.LinkDemand, Synchronization = true, ExternalThreading = true)]
	public class TaskFactory
	{
		// Token: 0x170009BD RID: 2493
		// (get) Token: 0x06003B71 RID: 15217 RVA: 0x000D74C0 File Offset: 0x000D56C0
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

		// Token: 0x06003B72 RID: 15218 RVA: 0x000D74D6 File Offset: 0x000D56D6
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

		/// <summary>Initializes a <see cref="T:System.Threading.Tasks.TaskFactory" /> instance with the default configuration.</summary>
		// Token: 0x06003B73 RID: 15219 RVA: 0x000D7504 File Offset: 0x000D5704
		public TaskFactory()
			: this(default(CancellationToken), TaskCreationOptions.None, TaskContinuationOptions.None, null)
		{
		}

		/// <summary>Initializes a <see cref="T:System.Threading.Tasks.TaskFactory" /> instance with the specified configuration.</summary>
		/// <param name="cancellationToken">The <see cref="P:System.Threading.Tasks.TaskFactory.CancellationToken" /> that will be assigned to tasks created by this <see cref="T:System.Threading.Tasks.TaskFactory" /> unless another CancellationToken is explicitly specified while calling the factory methods.</param>
		// Token: 0x06003B74 RID: 15220 RVA: 0x000D7523 File Offset: 0x000D5723
		public TaskFactory(CancellationToken cancellationToken)
			: this(cancellationToken, TaskCreationOptions.None, TaskContinuationOptions.None, null)
		{
		}

		/// <summary>Initializes a <see cref="T:System.Threading.Tasks.TaskFactory" /> instance with the specified configuration.</summary>
		/// <param name="scheduler">The <see cref="T:System.Threading.Tasks.TaskScheduler" /> to use to schedule any tasks created with this TaskFactory. A null value indicates that the current TaskScheduler should be used.</param>
		// Token: 0x06003B75 RID: 15221 RVA: 0x000D7530 File Offset: 0x000D5730
		public TaskFactory(TaskScheduler scheduler)
			: this(default(CancellationToken), TaskCreationOptions.None, TaskContinuationOptions.None, scheduler)
		{
		}

		/// <summary>Initializes a <see cref="T:System.Threading.Tasks.TaskFactory" /> instance with the specified configuration.</summary>
		/// <param name="creationOptions">The default <see cref="T:System.Threading.Tasks.TaskCreationOptions" /> to use when creating tasks with this TaskFactory.</param>
		/// <param name="continuationOptions">The default <see cref="T:System.Threading.Tasks.TaskContinuationOptions" /> to use when creating continuation tasks with this TaskFactory.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The exception that is thrown when the <paramref name="creationOptions" /> argument or the <paramref name="continuationOptions" /> argument specifies an invalid value. The exception that is thrown when the <paramref name="creationOptions" /> argument specifies an invalid TaskCreationOptions value. For more information, see the Remarks for <see cref="M:System.Threading.Tasks.TaskFactory.FromAsync(System.Func{System.AsyncCallback,System.Object,System.IAsyncResult},System.Action{System.IAsyncResult},System.Object,System.Threading.Tasks.TaskCreationOptions)" /></exception>
		// Token: 0x06003B76 RID: 15222 RVA: 0x000D7550 File Offset: 0x000D5750
		public TaskFactory(TaskCreationOptions creationOptions, TaskContinuationOptions continuationOptions)
			: this(default(CancellationToken), creationOptions, continuationOptions, null)
		{
		}

		/// <summary>Initializes a <see cref="T:System.Threading.Tasks.TaskFactory" /> instance with the specified configuration.</summary>
		/// <param name="cancellationToken">The default <see cref="P:System.Threading.Tasks.TaskFactory.CancellationToken" /> that will be assigned to tasks created by this <see cref="T:System.Threading.Tasks.TaskFactory" /> unless another CancellationToken is explicitly specified while calling the factory methods.</param>
		/// <param name="creationOptions">The default <see cref="T:System.Threading.Tasks.TaskCreationOptions" /> to use when creating tasks with this TaskFactory.</param>
		/// <param name="continuationOptions">The default <see cref="T:System.Threading.Tasks.TaskContinuationOptions" /> to use when creating continuation tasks with this TaskFactory.</param>
		/// <param name="scheduler">The default <see cref="T:System.Threading.Tasks.TaskScheduler" /> to use to schedule any Tasks created with this TaskFactory. A null value indicates that TaskScheduler.Current should be used.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The exception that is thrown when the <paramref name="creationOptions" /> argument or the <paramref name="continuationOptions" /> argument specifies an invalid value. The exception that is thrown when the <paramref name="creationOptions" /> argument specifies an invalid TaskCreationOptions value. For more information, see the Remarks for <see cref="M:System.Threading.Tasks.TaskFactory.FromAsync(System.Func{System.AsyncCallback,System.Object,System.IAsyncResult},System.Action{System.IAsyncResult},System.Object,System.Threading.Tasks.TaskCreationOptions)" /></exception>
		// Token: 0x06003B77 RID: 15223 RVA: 0x000D756F File Offset: 0x000D576F
		public TaskFactory(CancellationToken cancellationToken, TaskCreationOptions creationOptions, TaskContinuationOptions continuationOptions, TaskScheduler scheduler)
		{
			TaskFactory.CheckMultiTaskContinuationOptions(continuationOptions);
			TaskFactory.CheckCreationOptions(creationOptions);
			this.m_defaultCancellationToken = cancellationToken;
			this.m_defaultScheduler = scheduler;
			this.m_defaultCreationOptions = creationOptions;
			this.m_defaultContinuationOptions = continuationOptions;
		}

		// Token: 0x06003B78 RID: 15224 RVA: 0x000D75A0 File Offset: 0x000D57A0
		internal static void CheckCreationOptions(TaskCreationOptions creationOptions)
		{
			if ((creationOptions & ~(TaskCreationOptions.PreferFairness | TaskCreationOptions.LongRunning | TaskCreationOptions.AttachedToParent | TaskCreationOptions.DenyChildAttach | TaskCreationOptions.HideScheduler | TaskCreationOptions.RunContinuationsAsynchronously)) != TaskCreationOptions.None)
			{
				throw new ArgumentOutOfRangeException("creationOptions");
			}
		}

		/// <summary>Gets the default cancellation token for this task factory.</summary>
		/// <returns>The default task cancellation token for this task factory.</returns>
		// Token: 0x170009BE RID: 2494
		// (get) Token: 0x06003B79 RID: 15225 RVA: 0x000D75B3 File Offset: 0x000D57B3
		public CancellationToken CancellationToken
		{
			get
			{
				return this.m_defaultCancellationToken;
			}
		}

		/// <summary>Gets the default task scheduler for this task factory.</summary>
		/// <returns>The default task scheduler for this task factory.</returns>
		// Token: 0x170009BF RID: 2495
		// (get) Token: 0x06003B7A RID: 15226 RVA: 0x000D75BB File Offset: 0x000D57BB
		public TaskScheduler Scheduler
		{
			get
			{
				return this.m_defaultScheduler;
			}
		}

		/// <summary>Gets the default task creation options for this task factory.</summary>
		/// <returns>The default task creation options for this task factory.</returns>
		// Token: 0x170009C0 RID: 2496
		// (get) Token: 0x06003B7B RID: 15227 RVA: 0x000D75C3 File Offset: 0x000D57C3
		public TaskCreationOptions CreationOptions
		{
			get
			{
				return this.m_defaultCreationOptions;
			}
		}

		/// <summary>Gets the default task continuation options for this task factory.</summary>
		/// <returns>The default task continuation options for this task factory.</returns>
		// Token: 0x170009C1 RID: 2497
		// (get) Token: 0x06003B7C RID: 15228 RVA: 0x000D75CB File Offset: 0x000D57CB
		public TaskContinuationOptions ContinuationOptions
		{
			get
			{
				return this.m_defaultContinuationOptions;
			}
		}

		/// <summary>Creates and starts a task.</summary>
		/// <returns>The started task.</returns>
		/// <param name="action">The action delegate to execute asynchronously.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="action" /> argument is null.</exception>
		// Token: 0x06003B7D RID: 15229 RVA: 0x000D75D4 File Offset: 0x000D57D4
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task StartNew(Action action)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			Task internalCurrent = Task.InternalCurrent;
			return Task.InternalStartNew(internalCurrent, action, null, this.m_defaultCancellationToken, this.GetDefaultScheduler(internalCurrent), this.m_defaultCreationOptions, InternalTaskOptions.None, ref stackCrawlMark);
		}

		/// <summary>Creates and starts a <see cref="T:System.Threading.Tasks.Task" />.</summary>
		/// <returns>The started <see cref="T:System.Threading.Tasks.Task" />.</returns>
		/// <param name="action">The action delegate to execute asynchronously.</param>
		/// <param name="cancellationToken">The <see cref="P:System.Threading.Tasks.TaskFactory.CancellationToken" /> that will be assigned to the new task.</param>
		/// <exception cref="T:System.ObjectDisposedException">The provided <see cref="T:System.Threading.CancellationToken" /> has already been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The exception that is thrown when the <paramref name="action" /> argument is null.</exception>
		// Token: 0x06003B7E RID: 15230 RVA: 0x000D7608 File Offset: 0x000D5808
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task StartNew(Action action, CancellationToken cancellationToken)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			Task internalCurrent = Task.InternalCurrent;
			return Task.InternalStartNew(internalCurrent, action, null, cancellationToken, this.GetDefaultScheduler(internalCurrent), this.m_defaultCreationOptions, InternalTaskOptions.None, ref stackCrawlMark);
		}

		/// <summary>Creates and starts a <see cref="T:System.Threading.Tasks.Task" />.</summary>
		/// <returns>The started <see cref="T:System.Threading.Tasks.Task" />.</returns>
		/// <param name="action">The action delegate to execute asynchronously.</param>
		/// <param name="creationOptions">A TaskCreationOptions value that controls the behavior of the created <see cref="T:System.Threading.Tasks.Task" /></param>
		/// <exception cref="T:System.ArgumentNullException">The exception that is thrown when the <paramref name="action" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The exception that is thrown when the <paramref name="creationOptions" /> argument specifies an invalid TaskCreationOptions value.</exception>
		// Token: 0x06003B7F RID: 15231 RVA: 0x000D7638 File Offset: 0x000D5838
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task StartNew(Action action, TaskCreationOptions creationOptions)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			Task internalCurrent = Task.InternalCurrent;
			return Task.InternalStartNew(internalCurrent, action, null, this.m_defaultCancellationToken, this.GetDefaultScheduler(internalCurrent), creationOptions, InternalTaskOptions.None, ref stackCrawlMark);
		}

		/// <summary>Creates and starts a <see cref="T:System.Threading.Tasks.Task" />.</summary>
		/// <returns>The started <see cref="T:System.Threading.Tasks.Task" />.</returns>
		/// <param name="action">The action delegate to execute asynchronously.</param>
		/// <param name="cancellationToken">The <see cref="P:System.Threading.Tasks.TaskFactory.CancellationToken" /> that will be assigned to the new <see cref="T:System.Threading.Tasks.Task" /></param>
		/// <param name="creationOptions">A TaskCreationOptions value that controls the behavior of the created <see cref="T:System.Threading.Tasks.Task" /></param>
		/// <param name="scheduler">The <see cref="T:System.Threading.Tasks.TaskScheduler" /> that is used to schedule the created <see cref="T:System.Threading.Tasks.Task" />.</param>
		/// <exception cref="T:System.ObjectDisposedException">The provided <see cref="T:System.Threading.CancellationToken" /> has already been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The exception that is thrown when the <paramref name="action" /> argument is null.-or-The exception that is thrown when the <paramref name="scheduler" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The exception that is thrown when the <paramref name="creationOptions" /> argument specifies an invalid TaskCreationOptions value. The exception that is thrown when the <paramref name="creationOptions" /> argument specifies an invalid TaskCreationOptions value. For more information, see the Remarks for <see cref="M:System.Threading.Tasks.TaskFactory.FromAsync(System.Func{System.AsyncCallback,System.Object,System.IAsyncResult},System.Action{System.IAsyncResult},System.Object,System.Threading.Tasks.TaskCreationOptions)" /></exception>
		// Token: 0x06003B80 RID: 15232 RVA: 0x000D7668 File Offset: 0x000D5868
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task StartNew(Action action, CancellationToken cancellationToken, TaskCreationOptions creationOptions, TaskScheduler scheduler)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return Task.InternalStartNew(Task.InternalCurrentIfAttached(creationOptions), action, null, cancellationToken, scheduler, creationOptions, InternalTaskOptions.None, ref stackCrawlMark);
		}

		// Token: 0x06003B81 RID: 15233 RVA: 0x000D768C File Offset: 0x000D588C
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal Task StartNew(Action action, CancellationToken cancellationToken, TaskCreationOptions creationOptions, InternalTaskOptions internalOptions, TaskScheduler scheduler)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return Task.InternalStartNew(Task.InternalCurrentIfAttached(creationOptions), action, null, cancellationToken, scheduler, creationOptions, internalOptions, ref stackCrawlMark);
		}

		/// <summary>Creates and starts a <see cref="T:System.Threading.Tasks.Task" />.</summary>
		/// <returns>The started <see cref="T:System.Threading.Tasks.Task" />.</returns>
		/// <param name="action">The action delegate to execute asynchronously.</param>
		/// <param name="state">An object containing data to be used by the <paramref name="action" /> delegate.</param>
		/// <exception cref="T:System.ArgumentNullException">The exception that is thrown when the <paramref name="action" /> argument is null.</exception>
		// Token: 0x06003B82 RID: 15234 RVA: 0x000D76B0 File Offset: 0x000D58B0
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task StartNew(Action<object> action, object state)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			Task internalCurrent = Task.InternalCurrent;
			return Task.InternalStartNew(internalCurrent, action, state, this.m_defaultCancellationToken, this.GetDefaultScheduler(internalCurrent), this.m_defaultCreationOptions, InternalTaskOptions.None, ref stackCrawlMark);
		}

		/// <summary>Creates and starts a <see cref="T:System.Threading.Tasks.Task" />.</summary>
		/// <returns>The started <see cref="T:System.Threading.Tasks.Task" />.</returns>
		/// <param name="action">The action delegate to execute asynchronously.</param>
		/// <param name="state">An object containing data to be used by the <paramref name="action" /> delegate.</param>
		/// <param name="cancellationToken">The <see cref="P:System.Threading.Tasks.TaskFactory.CancellationToken" /> that will be assigned to the new <see cref="T:System.Threading.Tasks.Task" /></param>
		/// <exception cref="T:System.ObjectDisposedException">The provided <see cref="T:System.Threading.CancellationToken" /> has already been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The exception that is thrown when the <paramref name="action" /> argument is null.</exception>
		// Token: 0x06003B83 RID: 15235 RVA: 0x000D76E4 File Offset: 0x000D58E4
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task StartNew(Action<object> action, object state, CancellationToken cancellationToken)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			Task internalCurrent = Task.InternalCurrent;
			return Task.InternalStartNew(internalCurrent, action, state, cancellationToken, this.GetDefaultScheduler(internalCurrent), this.m_defaultCreationOptions, InternalTaskOptions.None, ref stackCrawlMark);
		}

		/// <summary>Creates and starts a <see cref="T:System.Threading.Tasks.Task" />.</summary>
		/// <returns>The started <see cref="T:System.Threading.Tasks.Task" />.</returns>
		/// <param name="action">The action delegate to execute asynchronously.</param>
		/// <param name="state">An object containing data to be used by the <paramref name="action" /> delegate.</param>
		/// <param name="creationOptions">A TaskCreationOptions value that controls the behavior of the created <see cref="T:System.Threading.Tasks.Task" /></param>
		/// <exception cref="T:System.ArgumentNullException">The exception that is thrown when the <paramref name="action" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The exception that is thrown when the <paramref name="creationOptions" /> argument specifies an invalid TaskCreationOptions value.</exception>
		// Token: 0x06003B84 RID: 15236 RVA: 0x000D7714 File Offset: 0x000D5914
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task StartNew(Action<object> action, object state, TaskCreationOptions creationOptions)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			Task internalCurrent = Task.InternalCurrent;
			return Task.InternalStartNew(internalCurrent, action, state, this.m_defaultCancellationToken, this.GetDefaultScheduler(internalCurrent), creationOptions, InternalTaskOptions.None, ref stackCrawlMark);
		}

		/// <summary>Creates and starts a <see cref="T:System.Threading.Tasks.Task" />.</summary>
		/// <returns>The started <see cref="T:System.Threading.Tasks.Task" />.</returns>
		/// <param name="action">The action delegate to execute asynchronously.</param>
		/// <param name="state">An object containing data to be used by the <paramref name="action" /> delegate.</param>
		/// <param name="cancellationToken">The <see cref="P:System.Threading.Tasks.TaskFactory.CancellationToken" /> that will be assigned to the new task.</param>
		/// <param name="creationOptions">A TaskCreationOptions value that controls the behavior of the created <see cref="T:System.Threading.Tasks.Task" /></param>
		/// <param name="scheduler">The <see cref="T:System.Threading.Tasks.TaskScheduler" /> that is used to schedule the created <see cref="T:System.Threading.Tasks.Task" />.</param>
		/// <exception cref="T:System.ObjectDisposedException">The provided <see cref="T:System.Threading.CancellationToken" /> has already been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The exception that is thrown when the <paramref name="action" /> argument is null.-or-The exception that is thrown when the <paramref name="scheduler" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The exception that is thrown when the <paramref name="creationOptions" /> argument specifies an invalid TaskCreationOptions value. The exception that is thrown when the <paramref name="creationOptions" /> argument specifies an invalid TaskCreationOptions value. For more information, see the Remarks for <see cref="M:System.Threading.Tasks.TaskFactory.FromAsync(System.Func{System.AsyncCallback,System.Object,System.IAsyncResult},System.Action{System.IAsyncResult},System.Object,System.Threading.Tasks.TaskCreationOptions)" /></exception>
		// Token: 0x06003B85 RID: 15237 RVA: 0x000D7744 File Offset: 0x000D5944
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task StartNew(Action<object> action, object state, CancellationToken cancellationToken, TaskCreationOptions creationOptions, TaskScheduler scheduler)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return Task.InternalStartNew(Task.InternalCurrentIfAttached(creationOptions), action, state, cancellationToken, scheduler, creationOptions, InternalTaskOptions.None, ref stackCrawlMark);
		}

		/// <summary>Creates and starts a <see cref="T:System.Threading.Tasks.Task`1" />.</summary>
		/// <returns>The started <see cref="T:System.Threading.Tasks.Task`1" />.</returns>
		/// <param name="function">A function delegate that returns the future result to be available through the <see cref="T:System.Threading.Tasks.Task`1" />.</param>
		/// <typeparam name="TResult">The type of the result available through the <see cref="T:System.Threading.Tasks.Task`1" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">The exception that is thrown when the <paramref name="function" /> argument is null.</exception>
		// Token: 0x06003B86 RID: 15238 RVA: 0x000D776C File Offset: 0x000D596C
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task<TResult> StartNew<TResult>(Func<TResult> function)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			Task internalCurrent = Task.InternalCurrent;
			return Task<TResult>.StartNew(internalCurrent, function, this.m_defaultCancellationToken, this.m_defaultCreationOptions, InternalTaskOptions.None, this.GetDefaultScheduler(internalCurrent), ref stackCrawlMark);
		}

		/// <summary>Creates and starts a <see cref="T:System.Threading.Tasks.Task`1" />.</summary>
		/// <returns>The started <see cref="T:System.Threading.Tasks.Task`1" />.</returns>
		/// <param name="function">A function delegate that returns the future result to be available through the <see cref="T:System.Threading.Tasks.Task`1" />.</param>
		/// <param name="cancellationToken">The <see cref="P:System.Threading.Tasks.TaskFactory.CancellationToken" /> that will be assigned to the new <see cref="T:System.Threading.Tasks.Task" /></param>
		/// <typeparam name="TResult">The type of the result available through the <see cref="T:System.Threading.Tasks.Task`1" />.</typeparam>
		/// <exception cref="T:System.ObjectDisposedException">The provided <see cref="T:System.Threading.CancellationToken" /> has already been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The exception that is thrown when the <paramref name="function" /> argument is null.</exception>
		// Token: 0x06003B87 RID: 15239 RVA: 0x000D77A0 File Offset: 0x000D59A0
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task<TResult> StartNew<TResult>(Func<TResult> function, CancellationToken cancellationToken)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			Task internalCurrent = Task.InternalCurrent;
			return Task<TResult>.StartNew(internalCurrent, function, cancellationToken, this.m_defaultCreationOptions, InternalTaskOptions.None, this.GetDefaultScheduler(internalCurrent), ref stackCrawlMark);
		}

		/// <summary>Creates and starts a <see cref="T:System.Threading.Tasks.Task`1" />.</summary>
		/// <returns>The started <see cref="T:System.Threading.Tasks.Task`1" />.</returns>
		/// <param name="function">A function delegate that returns the future result to be available through the <see cref="T:System.Threading.Tasks.Task`1" />.</param>
		/// <param name="creationOptions">A TaskCreationOptions value that controls the behavior of the created <see cref="T:System.Threading.Tasks.Task`1" />.</param>
		/// <typeparam name="TResult">The type of the result available through the <see cref="T:System.Threading.Tasks.Task`1" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">The exception that is thrown when the <paramref name="function" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The exception that is thrown when the <paramref name="creationOptions" /> argument specifies an invalid TaskCreationOptions value. The exception that is thrown when the <paramref name="creationOptions" /> argument specifies an invalid TaskCreationOptions value. For more information, see the Remarks for <see cref="M:System.Threading.Tasks.TaskFactory.FromAsync(System.Func{System.AsyncCallback,System.Object,System.IAsyncResult},System.Action{System.IAsyncResult},System.Object,System.Threading.Tasks.TaskCreationOptions)" /></exception>
		// Token: 0x06003B88 RID: 15240 RVA: 0x000D77D0 File Offset: 0x000D59D0
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task<TResult> StartNew<TResult>(Func<TResult> function, TaskCreationOptions creationOptions)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			Task internalCurrent = Task.InternalCurrent;
			return Task<TResult>.StartNew(internalCurrent, function, this.m_defaultCancellationToken, creationOptions, InternalTaskOptions.None, this.GetDefaultScheduler(internalCurrent), ref stackCrawlMark);
		}

		/// <summary>Creates and starts a <see cref="T:System.Threading.Tasks.Task`1" />.</summary>
		/// <returns>The started <see cref="T:System.Threading.Tasks.Task`1" />.</returns>
		/// <param name="function">A function delegate that returns the future result to be available through the <see cref="T:System.Threading.Tasks.Task`1" />.</param>
		/// <param name="cancellationToken">The <see cref="P:System.Threading.Tasks.TaskFactory.CancellationToken" /> that will be assigned to the new task.</param>
		/// <param name="creationOptions">A TaskCreationOptions value that controls the behavior of the created <see cref="T:System.Threading.Tasks.Task`1" />.</param>
		/// <param name="scheduler">The <see cref="T:System.Threading.Tasks.TaskScheduler" /> that is used to schedule the created <see cref="T:System.Threading.Tasks.Task`1" />.</param>
		/// <typeparam name="TResult">The type of the result available through the <see cref="T:System.Threading.Tasks.Task`1" />.</typeparam>
		/// <exception cref="T:System.ObjectDisposedException">The provided <see cref="T:System.Threading.CancellationToken" /> has already been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The exception that is thrown when the <paramref name="function" /> argument is null.-or-The exception that is thrown when the <paramref name="scheduler" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The exception that is thrown when the <paramref name="creationOptions" /> argument specifies an invalid TaskCreationOptions value. The exception that is thrown when the <paramref name="creationOptions" /> argument specifies an invalid TaskCreationOptions value. For more information, see the Remarks for <see cref="M:System.Threading.Tasks.TaskFactory.FromAsync(System.Func{System.AsyncCallback,System.Object,System.IAsyncResult},System.Action{System.IAsyncResult},System.Object,System.Threading.Tasks.TaskCreationOptions)" /></exception>
		// Token: 0x06003B89 RID: 15241 RVA: 0x000D7800 File Offset: 0x000D5A00
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task<TResult> StartNew<TResult>(Func<TResult> function, CancellationToken cancellationToken, TaskCreationOptions creationOptions, TaskScheduler scheduler)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return Task<TResult>.StartNew(Task.InternalCurrentIfAttached(creationOptions), function, cancellationToken, creationOptions, InternalTaskOptions.None, scheduler, ref stackCrawlMark);
		}

		/// <summary>Creates and starts a <see cref="T:System.Threading.Tasks.Task`1" />.</summary>
		/// <returns>The started <see cref="T:System.Threading.Tasks.Task`1" />.</returns>
		/// <param name="function">A function delegate that returns the future result to be available through the <see cref="T:System.Threading.Tasks.Task`1" />.</param>
		/// <param name="state">An object containing data to be used by the <paramref name="function" /> delegate.</param>
		/// <typeparam name="TResult">The type of the result available through the <see cref="T:System.Threading.Tasks.Task`1" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">The exception that is thrown when the <paramref name="function" /> argument is null.</exception>
		// Token: 0x06003B8A RID: 15242 RVA: 0x000D7824 File Offset: 0x000D5A24
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task<TResult> StartNew<TResult>(Func<object, TResult> function, object state)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			Task internalCurrent = Task.InternalCurrent;
			return Task<TResult>.StartNew(internalCurrent, function, state, this.m_defaultCancellationToken, this.m_defaultCreationOptions, InternalTaskOptions.None, this.GetDefaultScheduler(internalCurrent), ref stackCrawlMark);
		}

		/// <summary>Creates and starts a <see cref="T:System.Threading.Tasks.Task`1" />.</summary>
		/// <returns>The started <see cref="T:System.Threading.Tasks.Task`1" />.</returns>
		/// <param name="function">A function delegate that returns the future result to be available through the <see cref="T:System.Threading.Tasks.Task`1" />.</param>
		/// <param name="state">An object containing data to be used by the <paramref name="function" /> delegate.</param>
		/// <param name="cancellationToken">The <see cref="P:System.Threading.Tasks.TaskFactory.CancellationToken" /> that will be assigned to the new <see cref="T:System.Threading.Tasks.Task" /></param>
		/// <typeparam name="TResult">The type of the result available through the <see cref="T:System.Threading.Tasks.Task`1" />.</typeparam>
		/// <exception cref="T:System.ObjectDisposedException">The provided <see cref="T:System.Threading.CancellationToken" /> has already been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The exception that is thrown when the <paramref name="function" /> argument is null.</exception>
		// Token: 0x06003B8B RID: 15243 RVA: 0x000D7858 File Offset: 0x000D5A58
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task<TResult> StartNew<TResult>(Func<object, TResult> function, object state, CancellationToken cancellationToken)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			Task internalCurrent = Task.InternalCurrent;
			return Task<TResult>.StartNew(internalCurrent, function, state, cancellationToken, this.m_defaultCreationOptions, InternalTaskOptions.None, this.GetDefaultScheduler(internalCurrent), ref stackCrawlMark);
		}

		/// <summary>Creates and starts a <see cref="T:System.Threading.Tasks.Task`1" />.</summary>
		/// <returns>The started <see cref="T:System.Threading.Tasks.Task`1" />.</returns>
		/// <param name="function">A function delegate that returns the future result to be available through the <see cref="T:System.Threading.Tasks.Task`1" />.</param>
		/// <param name="state">An object containing data to be used by the <paramref name="function" /> delegate.</param>
		/// <param name="creationOptions">A TaskCreationOptions value that controls the behavior of the created <see cref="T:System.Threading.Tasks.Task`1" />.</param>
		/// <typeparam name="TResult">The type of the result available through the <see cref="T:System.Threading.Tasks.Task`1" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">The exception that is thrown when the <paramref name="function" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The exception that is thrown when the <paramref name="creationOptions" /> argument specifies an invalid TaskCreationOptions value. The exception that is thrown when the <paramref name="creationOptions" /> argument specifies an invalid TaskCreationOptions value. For more information, see the Remarks for <see cref="M:System.Threading.Tasks.TaskFactory.FromAsync(System.Func{System.AsyncCallback,System.Object,System.IAsyncResult},System.Action{System.IAsyncResult},System.Object,System.Threading.Tasks.TaskCreationOptions)" /></exception>
		// Token: 0x06003B8C RID: 15244 RVA: 0x000D7888 File Offset: 0x000D5A88
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task<TResult> StartNew<TResult>(Func<object, TResult> function, object state, TaskCreationOptions creationOptions)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			Task internalCurrent = Task.InternalCurrent;
			return Task<TResult>.StartNew(internalCurrent, function, state, this.m_defaultCancellationToken, creationOptions, InternalTaskOptions.None, this.GetDefaultScheduler(internalCurrent), ref stackCrawlMark);
		}

		/// <summary>Creates and starts a <see cref="T:System.Threading.Tasks.Task`1" />.</summary>
		/// <returns>The started <see cref="T:System.Threading.Tasks.Task`1" />.</returns>
		/// <param name="function">A function delegate that returns the future result to be available through the <see cref="T:System.Threading.Tasks.Task`1" />.</param>
		/// <param name="state">An object containing data to be used by the <paramref name="function" /> delegate.</param>
		/// <param name="cancellationToken">The <see cref="P:System.Threading.Tasks.TaskFactory.CancellationToken" /> that will be assigned to the new task.</param>
		/// <param name="creationOptions">A TaskCreationOptions value that controls the behavior of the created <see cref="T:System.Threading.Tasks.Task`1" />.</param>
		/// <param name="scheduler">The <see cref="T:System.Threading.Tasks.TaskScheduler" /> that is used to schedule the created <see cref="T:System.Threading.Tasks.Task`1" />.</param>
		/// <typeparam name="TResult">The type of the result available through the <see cref="T:System.Threading.Tasks.Task`1" />.</typeparam>
		/// <exception cref="T:System.ObjectDisposedException">The provided <see cref="T:System.Threading.CancellationToken" /> has already been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The exception that is thrown when the <paramref name="function" /> argument is null.-or-The exception that is thrown when the <paramref name="scheduler" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The exception that is thrown when the <paramref name="creationOptions" /> argument specifies an invalid TaskCreationOptions value. The exception that is thrown when the <paramref name="creationOptions" /> argument specifies an invalid TaskCreationOptions value. For more information, see the Remarks for <see cref="M:System.Threading.Tasks.TaskFactory.FromAsync(System.Func{System.AsyncCallback,System.Object,System.IAsyncResult},System.Action{System.IAsyncResult},System.Object,System.Threading.Tasks.TaskCreationOptions)" /></exception>
		// Token: 0x06003B8D RID: 15245 RVA: 0x000D78B8 File Offset: 0x000D5AB8
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task<TResult> StartNew<TResult>(Func<object, TResult> function, object state, CancellationToken cancellationToken, TaskCreationOptions creationOptions, TaskScheduler scheduler)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return Task<TResult>.StartNew(Task.InternalCurrentIfAttached(creationOptions), function, state, cancellationToken, creationOptions, InternalTaskOptions.None, scheduler, ref stackCrawlMark);
		}

		/// <summary>Creates a <see cref="T:System.Threading.Tasks.Task" /> that executes an end method action when a specified <see cref="T:System.IAsyncResult" /> completes.</summary>
		/// <returns>A <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation.</returns>
		/// <param name="asyncResult">The IAsyncResult whose completion should trigger the processing of the <paramref name="endMethod" />.</param>
		/// <param name="endMethod">The action delegate that processes the completed <paramref name="asyncResult" />.</param>
		/// <exception cref="T:System.ArgumentNullException">The exception that is thrown when the <paramref name="asyncResult" /> argument is null.-or-The exception that is thrown when the <paramref name="endMethod" /> argument is null.</exception>
		// Token: 0x06003B8E RID: 15246 RVA: 0x000D78E0 File Offset: 0x000D5AE0
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task FromAsync(IAsyncResult asyncResult, Action<IAsyncResult> endMethod)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return this.FromAsync(asyncResult, endMethod, this.m_defaultCreationOptions, this.DefaultScheduler, ref stackCrawlMark);
		}

		/// <summary>Creates a <see cref="T:System.Threading.Tasks.Task" /> that executes an end method action when a specified <see cref="T:System.IAsyncResult" /> completes.</summary>
		/// <returns>A <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation.</returns>
		/// <param name="asyncResult">The IAsyncResult whose completion should trigger the processing of the <paramref name="endMethod" />.</param>
		/// <param name="endMethod">The action delegate that processes the completed <paramref name="asyncResult" />.</param>
		/// <param name="creationOptions">The TaskCreationOptions value that controls the behavior of the created <see cref="T:System.Threading.Tasks.Task" />.</param>
		/// <exception cref="T:System.ArgumentNullException">The exception that is thrown when the <paramref name="asyncResult" /> argument is null.-or-The exception that is thrown when the <paramref name="endMethod" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The exception that is thrown when the <paramref name="creationOptions" /> argument specifies an invalid TaskCreationOptions value. For more information, see the Remarks for <see cref="M:System.Threading.Tasks.TaskFactory.FromAsync(System.Func{System.AsyncCallback,System.Object,System.IAsyncResult},System.Action{System.IAsyncResult},System.Object,System.Threading.Tasks.TaskCreationOptions)" /></exception>
		// Token: 0x06003B8F RID: 15247 RVA: 0x000D7908 File Offset: 0x000D5B08
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task FromAsync(IAsyncResult asyncResult, Action<IAsyncResult> endMethod, TaskCreationOptions creationOptions)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return this.FromAsync(asyncResult, endMethod, creationOptions, this.DefaultScheduler, ref stackCrawlMark);
		}

		/// <summary>Creates a <see cref="T:System.Threading.Tasks.Task" /> that executes an end method action when a specified <see cref="T:System.IAsyncResult" /> completes.</summary>
		/// <returns>The created <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation.</returns>
		/// <param name="asyncResult">The IAsyncResult whose completion should trigger the processing of the <paramref name="endMethod" />.</param>
		/// <param name="endMethod">The action delegate that processes the completed <paramref name="asyncResult" />.</param>
		/// <param name="creationOptions">The TaskCreationOptions value that controls the behavior of the created <see cref="T:System.Threading.Tasks.Task" />.</param>
		/// <param name="scheduler">The <see cref="T:System.Threading.Tasks.TaskScheduler" /> that is used to schedule the task that executes the end method.</param>
		/// <exception cref="T:System.ArgumentNullException">The exception that is thrown when the <paramref name="asyncResult" /> argument is null.-or-The exception that is thrown when the <paramref name="endMethod" /> argument is null.-or-The exception that is thrown when the <paramref name="scheduler" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The exception that is thrown when the <paramref name="creationOptions" /> argument specifies an invalid TaskCreationOptions value. The exception that is thrown when the <paramref name="creationOptions" /> argument specifies an invalid TaskCreationOptions value. For more information, see the Remarks for <see cref="M:System.Threading.Tasks.TaskFactory.FromAsync(System.Func{System.AsyncCallback,System.Object,System.IAsyncResult},System.Action{System.IAsyncResult},System.Object,System.Threading.Tasks.TaskCreationOptions)" /></exception>
		// Token: 0x06003B90 RID: 15248 RVA: 0x000D7928 File Offset: 0x000D5B28
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task FromAsync(IAsyncResult asyncResult, Action<IAsyncResult> endMethod, TaskCreationOptions creationOptions, TaskScheduler scheduler)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return this.FromAsync(asyncResult, endMethod, creationOptions, scheduler, ref stackCrawlMark);
		}

		// Token: 0x06003B91 RID: 15249 RVA: 0x000D7944 File Offset: 0x000D5B44
		private Task FromAsync(IAsyncResult asyncResult, Action<IAsyncResult> endMethod, TaskCreationOptions creationOptions, TaskScheduler scheduler, ref StackCrawlMark stackMark)
		{
			return TaskFactory<VoidTaskResult>.FromAsyncImpl(asyncResult, null, endMethod, creationOptions, scheduler, ref stackMark);
		}

		/// <summary>Creates a <see cref="T:System.Threading.Tasks.Task" /> that represents a pair of begin and end methods that conform to the Asynchronous Programming Model pattern.</summary>
		/// <returns>The created <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation.</returns>
		/// <param name="beginMethod">The delegate that begins the asynchronous operation.</param>
		/// <param name="endMethod">The delegate that ends the asynchronous operation.</param>
		/// <param name="state">An object containing data to be used by the <paramref name="beginMethod" /> delegate.</param>
		/// <exception cref="T:System.ArgumentNullException">The exception that is thrown when the <paramref name="beginMethod" /> argument is null.-or-The exception that is thrown when the <paramref name="endMethod" /> argument is null.</exception>
		// Token: 0x06003B92 RID: 15250 RVA: 0x000D7953 File Offset: 0x000D5B53
		public Task FromAsync(Func<AsyncCallback, object, IAsyncResult> beginMethod, Action<IAsyncResult> endMethod, object state)
		{
			return this.FromAsync(beginMethod, endMethod, state, this.m_defaultCreationOptions);
		}

		/// <summary>Creates a <see cref="T:System.Threading.Tasks.Task" /> that represents a pair of begin and end methods that conform to the Asynchronous Programming Model pattern.</summary>
		/// <returns>The created <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation.</returns>
		/// <param name="beginMethod">The delegate that begins the asynchronous operation.</param>
		/// <param name="endMethod">The delegate that ends the asynchronous operation.</param>
		/// <param name="state">An object containing data to be used by the <paramref name="beginMethod" /> delegate.</param>
		/// <param name="creationOptions">The TaskCreationOptions value that controls the behavior of the created <see cref="T:System.Threading.Tasks.Task" />.</param>
		/// <exception cref="T:System.ArgumentNullException">The exception that is thrown when the <paramref name="beginMethod" /> argument is null.-or-The exception that is thrown when the <paramref name="endMethod" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The exception that is thrown when the <paramref name="creationOptions" /> argument specifies an invalid TaskCreationOptions value.</exception>
		// Token: 0x06003B93 RID: 15251 RVA: 0x000D7964 File Offset: 0x000D5B64
		public Task FromAsync(Func<AsyncCallback, object, IAsyncResult> beginMethod, Action<IAsyncResult> endMethod, object state, TaskCreationOptions creationOptions)
		{
			return TaskFactory<VoidTaskResult>.FromAsyncImpl(beginMethod, null, endMethod, state, creationOptions);
		}

		/// <summary>Creates a <see cref="T:System.Threading.Tasks.Task" /> that represents a pair of begin and end methods that conform to the Asynchronous Programming Model pattern.</summary>
		/// <returns>The created <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation.</returns>
		/// <param name="beginMethod">The delegate that begins the asynchronous operation.</param>
		/// <param name="endMethod">The delegate that ends the asynchronous operation.</param>
		/// <param name="arg1">The first argument passed to the <paramref name="beginMethod" /> delegate.</param>
		/// <param name="state">An object containing data to be used by the <paramref name="beginMethod" /> delegate.</param>
		/// <typeparam name="TArg1">The type of the first argument passed to the <paramref name="beginMethod" /> delegate.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">The exception that is thrown when the <paramref name="beginMethod" /> argument is null.-or-The exception that is thrown when the <paramref name="endMethod" /> argument is null.</exception>
		// Token: 0x06003B94 RID: 15252 RVA: 0x000D7971 File Offset: 0x000D5B71
		public Task FromAsync<TArg1>(Func<TArg1, AsyncCallback, object, IAsyncResult> beginMethod, Action<IAsyncResult> endMethod, TArg1 arg1, object state)
		{
			return this.FromAsync<TArg1>(beginMethod, endMethod, arg1, state, this.m_defaultCreationOptions);
		}

		/// <summary>Creates a <see cref="T:System.Threading.Tasks.Task" /> that represents a pair of begin and end methods that conform to the Asynchronous Programming Model pattern.</summary>
		/// <returns>The created <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation.</returns>
		/// <param name="beginMethod">The delegate that begins the asynchronous operation.</param>
		/// <param name="endMethod">The delegate that ends the asynchronous operation.</param>
		/// <param name="arg1">The first argument passed to the <paramref name="beginMethod" /> delegate.</param>
		/// <param name="state">An object containing data to be used by the <paramref name="beginMethod" /> delegate.</param>
		/// <param name="creationOptions">The TaskCreationOptions value that controls the behavior of the created <see cref="T:System.Threading.Tasks.Task" />.</param>
		/// <typeparam name="TArg1">The type of the first argument passed to the <paramref name="beginMethod" /> delegate.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">The exception that is thrown when the <paramref name="beginMethod" /> argument is null.-or-The exception that is thrown when the <paramref name="endMethod" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The exception that is thrown when the <paramref name="creationOptions" /> argument specifies an invalid TaskCreationOptions value. The exception that is thrown when the <paramref name="creationOptions" /> argument specifies an invalid TaskCreationOptions value. For more information, see the Remarks for <see cref="M:System.Threading.Tasks.TaskFactory.FromAsync(System.Func{System.AsyncCallback,System.Object,System.IAsyncResult},System.Action{System.IAsyncResult},System.Object,System.Threading.Tasks.TaskCreationOptions)" /></exception>
		// Token: 0x06003B95 RID: 15253 RVA: 0x000D7984 File Offset: 0x000D5B84
		public Task FromAsync<TArg1>(Func<TArg1, AsyncCallback, object, IAsyncResult> beginMethod, Action<IAsyncResult> endMethod, TArg1 arg1, object state, TaskCreationOptions creationOptions)
		{
			return TaskFactory<VoidTaskResult>.FromAsyncImpl<TArg1>(beginMethod, null, endMethod, arg1, state, creationOptions);
		}

		/// <summary>Creates a <see cref="T:System.Threading.Tasks.Task" /> that represents a pair of begin and end methods that conform to the Asynchronous Programming Model pattern.</summary>
		/// <returns>The created <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation.</returns>
		/// <param name="beginMethod">The delegate that begins the asynchronous operation.</param>
		/// <param name="endMethod">The delegate that ends the asynchronous operation.</param>
		/// <param name="arg1">The first argument passed to the <paramref name="beginMethod" /> delegate.</param>
		/// <param name="arg2">The second argument passed to the <paramref name="beginMethod" /> delegate.</param>
		/// <param name="state">An object containing data to be used by the <paramref name="beginMethod" /> delegate.</param>
		/// <typeparam name="TArg1">The type of the second argument passed to <paramref name="beginMethod" /> delegate.</typeparam>
		/// <typeparam name="TArg2">The type of the first argument passed to the <paramref name="beginMethod" /> delegate.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">The exception that is thrown when the <paramref name="beginMethod" /> argument is null.-or-The exception that is thrown when the <paramref name="endMethod" /> argument is null.</exception>
		// Token: 0x06003B96 RID: 15254 RVA: 0x000D7993 File Offset: 0x000D5B93
		public Task FromAsync<TArg1, TArg2>(Func<TArg1, TArg2, AsyncCallback, object, IAsyncResult> beginMethod, Action<IAsyncResult> endMethod, TArg1 arg1, TArg2 arg2, object state)
		{
			return this.FromAsync<TArg1, TArg2>(beginMethod, endMethod, arg1, arg2, state, this.m_defaultCreationOptions);
		}

		/// <summary>Creates a <see cref="T:System.Threading.Tasks.Task" /> that represents a pair of begin and end methods that conform to the Asynchronous Programming Model pattern.</summary>
		/// <returns>The created <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation.</returns>
		/// <param name="beginMethod">The delegate that begins the asynchronous operation.</param>
		/// <param name="endMethod">The delegate that ends the asynchronous operation.</param>
		/// <param name="arg1">The first argument passed to the <paramref name="beginMethod" /> delegate.</param>
		/// <param name="arg2">The second argument passed to the <paramref name="beginMethod" /> delegate.</param>
		/// <param name="state">An object containing data to be used by the <paramref name="beginMethod" /> delegate.</param>
		/// <param name="creationOptions">The TaskCreationOptions value that controls the behavior of the created <see cref="T:System.Threading.Tasks.Task" />.</param>
		/// <typeparam name="TArg1">The type of the second argument passed to <paramref name="beginMethod" /> delegate.</typeparam>
		/// <typeparam name="TArg2">The type of the first argument passed to the <paramref name="beginMethod" /> delegate.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">The exception that is thrown when the <paramref name="beginMethod" /> argument is null.-or-The exception that is thrown when the <paramref name="endMethod" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The exception that is thrown when the <paramref name="creationOptions" /> argument specifies an invalid TaskCreationOptions value. The exception that is thrown when the <paramref name="creationOptions" /> argument specifies an invalid TaskCreationOptions value. For more information, see the Remarks for <see cref="M:System.Threading.Tasks.TaskFactory.FromAsync(System.Func{System.AsyncCallback,System.Object,System.IAsyncResult},System.Action{System.IAsyncResult},System.Object,System.Threading.Tasks.TaskCreationOptions)" /></exception>
		// Token: 0x06003B97 RID: 15255 RVA: 0x000D79A8 File Offset: 0x000D5BA8
		public Task FromAsync<TArg1, TArg2>(Func<TArg1, TArg2, AsyncCallback, object, IAsyncResult> beginMethod, Action<IAsyncResult> endMethod, TArg1 arg1, TArg2 arg2, object state, TaskCreationOptions creationOptions)
		{
			return TaskFactory<VoidTaskResult>.FromAsyncImpl<TArg1, TArg2>(beginMethod, null, endMethod, arg1, arg2, state, creationOptions);
		}

		/// <summary>Creates a <see cref="T:System.Threading.Tasks.Task" /> that represents a pair of begin and end methods that conform to the Asynchronous Programming Model pattern.</summary>
		/// <returns>The created <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation.</returns>
		/// <param name="beginMethod">The delegate that begins the asynchronous operation.</param>
		/// <param name="endMethod">The delegate that ends the asynchronous operation.</param>
		/// <param name="arg1">The first argument passed to the <paramref name="beginMethod" /> delegate.</param>
		/// <param name="arg2">The second argument passed to the <paramref name="beginMethod" /> delegate.</param>
		/// <param name="arg3">The third argument passed to the <paramref name="beginMethod" /> delegate.</param>
		/// <param name="state">An object containing data to be used by the <paramref name="beginMethod" /> delegate.</param>
		/// <typeparam name="TArg1">The type of the second argument passed to <paramref name="beginMethod" /> delegate.</typeparam>
		/// <typeparam name="TArg2">The type of the third argument passed to <paramref name="beginMethod" /> delegate.</typeparam>
		/// <typeparam name="TArg3">The type of the first argument passed to the <paramref name="beginMethod" /> delegate.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">The exception that is thrown when the <paramref name="beginMethod" /> argument is null.-or-The exception that is thrown when the <paramref name="endMethod" /> argument is null.</exception>
		// Token: 0x06003B98 RID: 15256 RVA: 0x000D79B9 File Offset: 0x000D5BB9
		public Task FromAsync<TArg1, TArg2, TArg3>(Func<TArg1, TArg2, TArg3, AsyncCallback, object, IAsyncResult> beginMethod, Action<IAsyncResult> endMethod, TArg1 arg1, TArg2 arg2, TArg3 arg3, object state)
		{
			return this.FromAsync<TArg1, TArg2, TArg3>(beginMethod, endMethod, arg1, arg2, arg3, state, this.m_defaultCreationOptions);
		}

		/// <summary>Creates a <see cref="T:System.Threading.Tasks.Task" /> that represents a pair of begin and end methods that conform to the Asynchronous Programming Model pattern.</summary>
		/// <returns>The created <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation.</returns>
		/// <param name="beginMethod">The delegate that begins the asynchronous operation.</param>
		/// <param name="endMethod">The delegate that ends the asynchronous operation.</param>
		/// <param name="arg1">The first argument passed to the <paramref name="beginMethod" /> delegate.</param>
		/// <param name="arg2">The second argument passed to the <paramref name="beginMethod" /> delegate.</param>
		/// <param name="arg3">The third argument passed to the <paramref name="beginMethod" /> delegate.</param>
		/// <param name="state">An object containing data to be used by the <paramref name="beginMethod" /> delegate.</param>
		/// <param name="creationOptions">The TaskCreationOptions value that controls the behavior of the created <see cref="T:System.Threading.Tasks.Task" />.</param>
		/// <typeparam name="TArg1">The type of the second argument passed to <paramref name="beginMethod" /> delegate.</typeparam>
		/// <typeparam name="TArg2">The type of the third argument passed to <paramref name="beginMethod" /> delegate.</typeparam>
		/// <typeparam name="TArg3">The type of the first argument passed to the <paramref name="beginMethod" /> delegate.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">The exception that is thrown when the <paramref name="beginMethod" /> argument is null.-or-The exception that is thrown when the <paramref name="endMethod" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The exception that is thrown when the <paramref name="creationOptions" /> argument specifies an invalid TaskCreationOptions value. The exception that is thrown when the <paramref name="creationOptions" /> argument specifies an invalid TaskCreationOptions value. For more information, see the Remarks for <see cref="M:System.Threading.Tasks.TaskFactory.FromAsync(System.Func{System.AsyncCallback,System.Object,System.IAsyncResult},System.Action{System.IAsyncResult},System.Object,System.Threading.Tasks.TaskCreationOptions)" /></exception>
		// Token: 0x06003B99 RID: 15257 RVA: 0x000D79D0 File Offset: 0x000D5BD0
		public Task FromAsync<TArg1, TArg2, TArg3>(Func<TArg1, TArg2, TArg3, AsyncCallback, object, IAsyncResult> beginMethod, Action<IAsyncResult> endMethod, TArg1 arg1, TArg2 arg2, TArg3 arg3, object state, TaskCreationOptions creationOptions)
		{
			return TaskFactory<VoidTaskResult>.FromAsyncImpl<TArg1, TArg2, TArg3>(beginMethod, null, endMethod, arg1, arg2, arg3, state, creationOptions);
		}

		/// <summary>Creates a <see cref="T:System.Threading.Tasks.Task`1" /> that executes an end method function when a specified <see cref="T:System.IAsyncResult" /> completes.</summary>
		/// <returns>A <see cref="T:System.Threading.Tasks.Task`1" /> that represents the asynchronous operation.</returns>
		/// <param name="asyncResult">The IAsyncResult whose completion should trigger the processing of the <paramref name="endMethod" />.</param>
		/// <param name="endMethod">The function delegate that processes the completed <paramref name="asyncResult" />.</param>
		/// <typeparam name="TResult">The type of the result available through the <see cref="T:System.Threading.Tasks.Task`1" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">The exception that is thrown when the <paramref name="asyncResult" /> argument is null.-or-The exception that is thrown when the <paramref name="endMethod" /> argument is null.</exception>
		// Token: 0x06003B9A RID: 15258 RVA: 0x000D79E4 File Offset: 0x000D5BE4
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task<TResult> FromAsync<TResult>(IAsyncResult asyncResult, Func<IAsyncResult, TResult> endMethod)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return TaskFactory<TResult>.FromAsyncImpl(asyncResult, endMethod, null, this.m_defaultCreationOptions, this.DefaultScheduler, ref stackCrawlMark);
		}

		/// <summary>Creates a <see cref="T:System.Threading.Tasks.Task`1" /> that executes an end method function when a specified <see cref="T:System.IAsyncResult" /> completes.</summary>
		/// <returns>A <see cref="T:System.Threading.Tasks.Task`1" /> that represents the asynchronous operation.</returns>
		/// <param name="asyncResult">The IAsyncResult whose completion should trigger the processing of the <paramref name="endMethod" />.</param>
		/// <param name="endMethod">The function delegate that processes the completed <paramref name="asyncResult" />.</param>
		/// <param name="creationOptions">The TaskCreationOptions value that controls the behavior of the created <see cref="T:System.Threading.Tasks.Task`1" />.</param>
		/// <typeparam name="TResult">The type of the result available through the <see cref="T:System.Threading.Tasks.Task`1" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">The exception that is thrown when the <paramref name="asyncResult" /> argument is null.-or-The exception that is thrown when the <paramref name="endMethod" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The exception that is thrown when the <paramref name="creationOptions" /> argument specifies an invalid TaskCreationOptions value. The exception that is thrown when the <paramref name="creationOptions" /> argument specifies an invalid TaskCreationOptions value. For more information, see the Remarks for <see cref="M:System.Threading.Tasks.TaskFactory.FromAsync(System.Func{System.AsyncCallback,System.Object,System.IAsyncResult},System.Action{System.IAsyncResult},System.Object,System.Threading.Tasks.TaskCreationOptions)" /></exception>
		// Token: 0x06003B9B RID: 15259 RVA: 0x000D7A0C File Offset: 0x000D5C0C
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task<TResult> FromAsync<TResult>(IAsyncResult asyncResult, Func<IAsyncResult, TResult> endMethod, TaskCreationOptions creationOptions)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return TaskFactory<TResult>.FromAsyncImpl(asyncResult, endMethod, null, creationOptions, this.DefaultScheduler, ref stackCrawlMark);
		}

		/// <summary>Creates a <see cref="T:System.Threading.Tasks.Task`1" /> that executes an end method function when a specified <see cref="T:System.IAsyncResult" /> completes.</summary>
		/// <returns>A <see cref="T:System.Threading.Tasks.Task`1" /> that represents the asynchronous operation.</returns>
		/// <param name="asyncResult">The IAsyncResult whose completion should trigger the processing of the <paramref name="endMethod" />.</param>
		/// <param name="endMethod">The function delegate that processes the completed <paramref name="asyncResult" />.</param>
		/// <param name="creationOptions">The TaskCreationOptions value that controls the behavior of the created <see cref="T:System.Threading.Tasks.Task`1" />.</param>
		/// <param name="scheduler">The <see cref="T:System.Threading.Tasks.TaskScheduler" /> that is used to schedule the task that executes the end method.</param>
		/// <typeparam name="TResult">The type of the result available through the <see cref="T:System.Threading.Tasks.Task`1" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">The exception that is thrown when the <paramref name="asyncResult" /> argument is null.-or-The exception that is thrown when the <paramref name="endMethod" /> argument is null.-or-The exception that is thrown when the <paramref name="scheduler" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The exception that is thrown when the <paramref name="creationOptions" /> argument specifies an invalid TaskCreationOptions value. The exception that is thrown when the <paramref name="creationOptions" /> argument specifies an invalid TaskCreationOptions value. For more information, see the Remarks for <see cref="M:System.Threading.Tasks.TaskFactory.FromAsync(System.Func{System.AsyncCallback,System.Object,System.IAsyncResult},System.Action{System.IAsyncResult},System.Object,System.Threading.Tasks.TaskCreationOptions)" /></exception>
		// Token: 0x06003B9C RID: 15260 RVA: 0x000D7A2C File Offset: 0x000D5C2C
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task<TResult> FromAsync<TResult>(IAsyncResult asyncResult, Func<IAsyncResult, TResult> endMethod, TaskCreationOptions creationOptions, TaskScheduler scheduler)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return TaskFactory<TResult>.FromAsyncImpl(asyncResult, endMethod, null, creationOptions, scheduler, ref stackCrawlMark);
		}

		/// <summary>Creates a <see cref="T:System.Threading.Tasks.Task`1" /> that represents a pair of begin and end methods that conform to the Asynchronous Programming Model pattern.</summary>
		/// <returns>The created <see cref="T:System.Threading.Tasks.Task`1" /> that represents the asynchronous operation.</returns>
		/// <param name="beginMethod">The delegate that begins the asynchronous operation.</param>
		/// <param name="endMethod">The delegate that ends the asynchronous operation.</param>
		/// <param name="state">An object containing data to be used by the <paramref name="beginMethod" /> delegate.</param>
		/// <typeparam name="TResult">The type of the result available through the <see cref="T:System.Threading.Tasks.Task`1" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">The exception that is thrown when the <paramref name="beginMethod" /> argument is null.-or-The exception that is thrown when the <paramref name="endMethod" /> argument is null.</exception>
		// Token: 0x06003B9D RID: 15261 RVA: 0x000D7A48 File Offset: 0x000D5C48
		public Task<TResult> FromAsync<TResult>(Func<AsyncCallback, object, IAsyncResult> beginMethod, Func<IAsyncResult, TResult> endMethod, object state)
		{
			return TaskFactory<TResult>.FromAsyncImpl(beginMethod, endMethod, null, state, this.m_defaultCreationOptions);
		}

		/// <summary>Creates a <see cref="T:System.Threading.Tasks.Task`1" /> that represents a pair of begin and end methods that conform to the Asynchronous Programming Model pattern.</summary>
		/// <returns>The created <see cref="T:System.Threading.Tasks.Task`1" /> that represents the asynchronous operation.</returns>
		/// <param name="beginMethod">The delegate that begins the asynchronous operation.</param>
		/// <param name="endMethod">The delegate that ends the asynchronous operation.</param>
		/// <param name="state">An object containing data to be used by the <paramref name="beginMethod" /> delegate.</param>
		/// <param name="creationOptions">The TaskCreationOptions value that controls the behavior of the created <see cref="T:System.Threading.Tasks.Task`1" />.</param>
		/// <typeparam name="TResult">The type of the result available through the <see cref="T:System.Threading.Tasks.Task`1" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">The exception that is thrown when the <paramref name="beginMethod" /> argument is null.-or-The exception that is thrown when the <paramref name="endMethod" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The exception that is thrown when the <paramref name="creationOptions" /> argument specifies an invalid TaskCreationOptions value. The exception that is thrown when the <paramref name="creationOptions" /> argument specifies an invalid TaskCreationOptions value. For more information, see the Remarks for <see cref="M:System.Threading.Tasks.TaskFactory.FromAsync(System.Func{System.AsyncCallback,System.Object,System.IAsyncResult},System.Action{System.IAsyncResult},System.Object,System.Threading.Tasks.TaskCreationOptions)" /></exception>
		// Token: 0x06003B9E RID: 15262 RVA: 0x000D7A59 File Offset: 0x000D5C59
		public Task<TResult> FromAsync<TResult>(Func<AsyncCallback, object, IAsyncResult> beginMethod, Func<IAsyncResult, TResult> endMethod, object state, TaskCreationOptions creationOptions)
		{
			return TaskFactory<TResult>.FromAsyncImpl(beginMethod, endMethod, null, state, creationOptions);
		}

		/// <summary>Creates a <see cref="T:System.Threading.Tasks.Task`1" /> that represents a pair of begin and end methods that conform to the Asynchronous Programming Model pattern.</summary>
		/// <returns>The created <see cref="T:System.Threading.Tasks.Task`1" /> that represents the asynchronous operation.</returns>
		/// <param name="beginMethod">The delegate that begins the asynchronous operation.</param>
		/// <param name="endMethod">The delegate that ends the asynchronous operation.</param>
		/// <param name="arg1">The first argument passed to the <paramref name="beginMethod" /> delegate.</param>
		/// <param name="state">An object containing data to be used by the <paramref name="beginMethod" /> delegate.</param>
		/// <typeparam name="TArg1">The type of the first argument passed to the <paramref name="beginMethod" /> delegate.</typeparam>
		/// <typeparam name="TResult">The type of the result available through the <see cref="T:System.Threading.Tasks.Task`1" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">The exception that is thrown when the <paramref name="beginMethod" /> argument is null.-or-The exception that is thrown when the <paramref name="endMethod" /> argument is null.</exception>
		// Token: 0x06003B9F RID: 15263 RVA: 0x000D7A66 File Offset: 0x000D5C66
		public Task<TResult> FromAsync<TArg1, TResult>(Func<TArg1, AsyncCallback, object, IAsyncResult> beginMethod, Func<IAsyncResult, TResult> endMethod, TArg1 arg1, object state)
		{
			return TaskFactory<TResult>.FromAsyncImpl<TArg1>(beginMethod, endMethod, null, arg1, state, this.m_defaultCreationOptions);
		}

		/// <summary>Creates a <see cref="T:System.Threading.Tasks.Task`1" /> that represents a pair of begin and end methods that conform to the Asynchronous Programming Model pattern.</summary>
		/// <returns>The created <see cref="T:System.Threading.Tasks.Task`1" /> that represents the asynchronous operation.</returns>
		/// <param name="beginMethod">The delegate that begins the asynchronous operation.</param>
		/// <param name="endMethod">The delegate that ends the asynchronous operation.</param>
		/// <param name="arg1">The first argument passed to the <paramref name="beginMethod" /> delegate.</param>
		/// <param name="state">An object containing data to be used by the <paramref name="beginMethod" /> delegate.</param>
		/// <param name="creationOptions">The TaskCreationOptions value that controls the behavior of the created <see cref="T:System.Threading.Tasks.Task`1" />.</param>
		/// <typeparam name="TArg1">The type of the first argument passed to the <paramref name="beginMethod" /> delegate.</typeparam>
		/// <typeparam name="TResult">The type of the result available through the <see cref="T:System.Threading.Tasks.Task`1" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">The exception that is thrown when the <paramref name="beginMethod" /> argument is null.-or-The exception that is thrown when the <paramref name="endMethod" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The exception that is thrown when the <paramref name="creationOptions" /> argument specifies an invalid TaskCreationOptions value. The exception that is thrown when the <paramref name="creationOptions" /> argument specifies an invalid TaskCreationOptions value. For more information, see the Remarks for <see cref="M:System.Threading.Tasks.TaskFactory.FromAsync(System.Func{System.AsyncCallback,System.Object,System.IAsyncResult},System.Action{System.IAsyncResult},System.Object,System.Threading.Tasks.TaskCreationOptions)" /></exception>
		// Token: 0x06003BA0 RID: 15264 RVA: 0x000D7A79 File Offset: 0x000D5C79
		public Task<TResult> FromAsync<TArg1, TResult>(Func<TArg1, AsyncCallback, object, IAsyncResult> beginMethod, Func<IAsyncResult, TResult> endMethod, TArg1 arg1, object state, TaskCreationOptions creationOptions)
		{
			return TaskFactory<TResult>.FromAsyncImpl<TArg1>(beginMethod, endMethod, null, arg1, state, creationOptions);
		}

		/// <summary>Creates a <see cref="T:System.Threading.Tasks.Task`1" /> that represents a pair of begin and end methods that conform to the Asynchronous Programming Model pattern.</summary>
		/// <returns>The created <see cref="T:System.Threading.Tasks.Task`1" /> that represents the asynchronous operation.</returns>
		/// <param name="beginMethod">The delegate that begins the asynchronous operation.</param>
		/// <param name="endMethod">The delegate that ends the asynchronous operation.</param>
		/// <param name="arg1">The first argument passed to the <paramref name="beginMethod" /> delegate.</param>
		/// <param name="arg2">The second argument passed to the <paramref name="beginMethod" /> delegate.</param>
		/// <param name="state">An object containing data to be used by the <paramref name="beginMethod" /> delegate.</param>
		/// <typeparam name="TArg1">The type of the second argument passed to <paramref name="beginMethod" /> delegate.</typeparam>
		/// <typeparam name="TArg2">The type of the first argument passed to the <paramref name="beginMethod" /> delegate.</typeparam>
		/// <typeparam name="TResult">The type of the result available through the <see cref="T:System.Threading.Tasks.Task`1" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">The exception that is thrown when the <paramref name="beginMethod" /> argument is null.-or-The exception that is thrown when the <paramref name="endMethod" /> argument is null.</exception>
		// Token: 0x06003BA1 RID: 15265 RVA: 0x000D7A88 File Offset: 0x000D5C88
		public Task<TResult> FromAsync<TArg1, TArg2, TResult>(Func<TArg1, TArg2, AsyncCallback, object, IAsyncResult> beginMethod, Func<IAsyncResult, TResult> endMethod, TArg1 arg1, TArg2 arg2, object state)
		{
			return TaskFactory<TResult>.FromAsyncImpl<TArg1, TArg2>(beginMethod, endMethod, null, arg1, arg2, state, this.m_defaultCreationOptions);
		}

		/// <summary>Creates a <see cref="T:System.Threading.Tasks.Task`1" /> that represents a pair of begin and end methods that conform to the Asynchronous Programming Model pattern.</summary>
		/// <returns>The created <see cref="T:System.Threading.Tasks.Task`1" /> that represents the asynchronous operation.</returns>
		/// <param name="beginMethod">The delegate that begins the asynchronous operation.</param>
		/// <param name="endMethod">The delegate that ends the asynchronous operation.</param>
		/// <param name="arg1">The first argument passed to the <paramref name="beginMethod" /> delegate.</param>
		/// <param name="arg2">The second argument passed to the <paramref name="beginMethod" /> delegate.</param>
		/// <param name="state">An object containing data to be used by the <paramref name="beginMethod" /> delegate.</param>
		/// <param name="creationOptions">The TaskCreationOptions value that controls the behavior of the created <see cref="T:System.Threading.Tasks.Task`1" />.</param>
		/// <typeparam name="TArg1">The type of the second argument passed to <paramref name="beginMethod" /> delegate.</typeparam>
		/// <typeparam name="TArg2">The type of the first argument passed to the <paramref name="beginMethod" /> delegate.</typeparam>
		/// <typeparam name="TResult">The type of the result available through the <see cref="T:System.Threading.Tasks.Task`1" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">The exception that is thrown when the <paramref name="beginMethod" /> argument is null.-or-The exception that is thrown when the <paramref name="endMethod" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The exception that is thrown when the <paramref name="creationOptions" /> argument specifies an invalid TaskCreationOptions value. The exception that is thrown when the <paramref name="creationOptions" /> argument specifies an invalid TaskCreationOptions value. For more information, see the Remarks for <see cref="M:System.Threading.Tasks.TaskFactory.FromAsync(System.Func{System.AsyncCallback,System.Object,System.IAsyncResult},System.Action{System.IAsyncResult},System.Object,System.Threading.Tasks.TaskCreationOptions)" /></exception>
		// Token: 0x06003BA2 RID: 15266 RVA: 0x000D7A9D File Offset: 0x000D5C9D
		public Task<TResult> FromAsync<TArg1, TArg2, TResult>(Func<TArg1, TArg2, AsyncCallback, object, IAsyncResult> beginMethod, Func<IAsyncResult, TResult> endMethod, TArg1 arg1, TArg2 arg2, object state, TaskCreationOptions creationOptions)
		{
			return TaskFactory<TResult>.FromAsyncImpl<TArg1, TArg2>(beginMethod, endMethod, null, arg1, arg2, state, creationOptions);
		}

		/// <summary>Creates a <see cref="T:System.Threading.Tasks.Task`1" /> that represents a pair of begin and end methods that conform to the Asynchronous Programming Model pattern.</summary>
		/// <returns>The created <see cref="T:System.Threading.Tasks.Task`1" /> that represents the asynchronous operation.</returns>
		/// <param name="beginMethod">The delegate that begins the asynchronous operation.</param>
		/// <param name="endMethod">The delegate that ends the asynchronous operation.</param>
		/// <param name="arg1">The first argument passed to the <paramref name="beginMethod" /> delegate.</param>
		/// <param name="arg2">The second argument passed to the <paramref name="beginMethod" /> delegate.</param>
		/// <param name="arg3">The third argument passed to the <paramref name="beginMethod" /> delegate.</param>
		/// <param name="state">An object containing data to be used by the <paramref name="beginMethod" /> delegate.</param>
		/// <typeparam name="TArg1">The type of the second argument passed to <paramref name="beginMethod" /> delegate.</typeparam>
		/// <typeparam name="TArg2">The type of the third argument passed to <paramref name="beginMethod" /> delegate.</typeparam>
		/// <typeparam name="TArg3">The type of the first argument passed to the <paramref name="beginMethod" /> delegate.</typeparam>
		/// <typeparam name="TResult">The type of the result available through the <see cref="T:System.Threading.Tasks.Task`1" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">The exception that is thrown when the <paramref name="beginMethod" /> argument is null.-or-The exception that is thrown when the <paramref name="endMethod" /> argument is null.</exception>
		// Token: 0x06003BA3 RID: 15267 RVA: 0x000D7AAE File Offset: 0x000D5CAE
		public Task<TResult> FromAsync<TArg1, TArg2, TArg3, TResult>(Func<TArg1, TArg2, TArg3, AsyncCallback, object, IAsyncResult> beginMethod, Func<IAsyncResult, TResult> endMethod, TArg1 arg1, TArg2 arg2, TArg3 arg3, object state)
		{
			return TaskFactory<TResult>.FromAsyncImpl<TArg1, TArg2, TArg3>(beginMethod, endMethod, null, arg1, arg2, arg3, state, this.m_defaultCreationOptions);
		}

		/// <summary>Creates a <see cref="T:System.Threading.Tasks.Task`1" /> that represents a pair of begin and end methods that conform to the Asynchronous Programming Model pattern.</summary>
		/// <returns>The created <see cref="T:System.Threading.Tasks.Task`1" /> that represents the asynchronous operation.</returns>
		/// <param name="beginMethod">The delegate that begins the asynchronous operation.</param>
		/// <param name="endMethod">The delegate that ends the asynchronous operation.</param>
		/// <param name="arg1">The first argument passed to the <paramref name="beginMethod" /> delegate.</param>
		/// <param name="arg2">The second argument passed to the <paramref name="beginMethod" /> delegate.</param>
		/// <param name="arg3">The third argument passed to the <paramref name="beginMethod" /> delegate.</param>
		/// <param name="state">An object containing data to be used by the <paramref name="beginMethod" /> delegate.</param>
		/// <param name="creationOptions">The TaskCreationOptions value that controls the behavior of the created <see cref="T:System.Threading.Tasks.Task`1" />.</param>
		/// <typeparam name="TArg1">The type of the second argument passed to <paramref name="beginMethod" /> delegate.</typeparam>
		/// <typeparam name="TArg2">The type of the third argument passed to <paramref name="beginMethod" /> delegate.</typeparam>
		/// <typeparam name="TArg3">The type of the first argument passed to the <paramref name="beginMethod" /> delegate.</typeparam>
		/// <typeparam name="TResult">The type of the result available through the <see cref="T:System.Threading.Tasks.Task`1" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">The exception that is thrown when the <paramref name="beginMethod" /> argument is null.-or-The exception that is thrown when the <paramref name="endMethod" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The exception that is thrown when the <paramref name="creationOptions" /> argument specifies an invalid TaskCreationOptions value. The exception that is thrown when the <paramref name="creationOptions" /> argument specifies an invalid TaskCreationOptions value. For more information, see the Remarks for <see cref="M:System.Threading.Tasks.TaskFactory.FromAsync(System.Func{System.AsyncCallback,System.Object,System.IAsyncResult},System.Action{System.IAsyncResult},System.Object,System.Threading.Tasks.TaskCreationOptions)" /></exception>
		// Token: 0x06003BA4 RID: 15268 RVA: 0x000D7AC5 File Offset: 0x000D5CC5
		public Task<TResult> FromAsync<TArg1, TArg2, TArg3, TResult>(Func<TArg1, TArg2, TArg3, AsyncCallback, object, IAsyncResult> beginMethod, Func<IAsyncResult, TResult> endMethod, TArg1 arg1, TArg2 arg2, TArg3 arg3, object state, TaskCreationOptions creationOptions)
		{
			return TaskFactory<TResult>.FromAsyncImpl<TArg1, TArg2, TArg3>(beginMethod, endMethod, null, arg1, arg2, arg3, state, creationOptions);
		}

		// Token: 0x06003BA5 RID: 15269 RVA: 0x000D7AD8 File Offset: 0x000D5CD8
		internal static void CheckFromAsyncOptions(TaskCreationOptions creationOptions, bool hasBeginMethod)
		{
			if (hasBeginMethod)
			{
				if ((creationOptions & TaskCreationOptions.LongRunning) != TaskCreationOptions.None)
				{
					throw new ArgumentOutOfRangeException("creationOptions", Environment.GetResourceString("It is invalid to specify TaskCreationOptions.LongRunning in calls to FromAsync."));
				}
				if ((creationOptions & TaskCreationOptions.PreferFairness) != TaskCreationOptions.None)
				{
					throw new ArgumentOutOfRangeException("creationOptions", Environment.GetResourceString("It is invalid to specify TaskCreationOptions.PreferFairness in calls to FromAsync."));
				}
			}
			if ((creationOptions & ~(TaskCreationOptions.PreferFairness | TaskCreationOptions.LongRunning | TaskCreationOptions.AttachedToParent | TaskCreationOptions.DenyChildAttach | TaskCreationOptions.HideScheduler)) != TaskCreationOptions.None)
			{
				throw new ArgumentOutOfRangeException("creationOptions");
			}
		}

		// Token: 0x06003BA6 RID: 15270 RVA: 0x000D7B30 File Offset: 0x000D5D30
		internal static Task<Task[]> CommonCWAllLogic(Task[] tasksCopy)
		{
			TaskFactory.CompleteOnCountdownPromise completeOnCountdownPromise = new TaskFactory.CompleteOnCountdownPromise(tasksCopy);
			for (int i = 0; i < tasksCopy.Length; i++)
			{
				if (tasksCopy[i].IsCompleted)
				{
					completeOnCountdownPromise.Invoke(tasksCopy[i]);
				}
				else
				{
					tasksCopy[i].AddCompletionAction(completeOnCountdownPromise);
				}
			}
			return completeOnCountdownPromise;
		}

		// Token: 0x06003BA7 RID: 15271 RVA: 0x000D7B74 File Offset: 0x000D5D74
		internal static Task<Task<T>[]> CommonCWAllLogic<T>(Task<T>[] tasksCopy)
		{
			TaskFactory.CompleteOnCountdownPromise<T> completeOnCountdownPromise = new TaskFactory.CompleteOnCountdownPromise<T>(tasksCopy);
			for (int i = 0; i < tasksCopy.Length; i++)
			{
				if (tasksCopy[i].IsCompleted)
				{
					completeOnCountdownPromise.Invoke(tasksCopy[i]);
				}
				else
				{
					tasksCopy[i].AddCompletionAction(completeOnCountdownPromise);
				}
			}
			return completeOnCountdownPromise;
		}

		/// <summary>Creates a continuation <see cref="T:System.Threading.Tasks.Task" /> that will be started upon the completion of a set of provided Tasks.</summary>
		/// <returns>The new continuation <see cref="T:System.Threading.Tasks.Task" />.</returns>
		/// <param name="tasks">The array of tasks from which to continue.</param>
		/// <param name="continuationAction">The action delegate to execute when all tasks in the <paramref name="tasks" /> array have completed.</param>
		/// <exception cref="T:System.ObjectDisposedException">The exception that is thrown when one of the elements in the <paramref name="tasks" /> array has been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The exception that is thrown when the <paramref name="tasks" /> array is null.-or-The exception that is thrown when the <paramref name="continuationAction" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentException">The exception that is thrown when the <paramref name="tasks" /> array contains a null value.-or-The exception that is thrown when the <paramref name="tasks" /> array is empty.</exception>
		// Token: 0x06003BA8 RID: 15272 RVA: 0x000D7BB8 File Offset: 0x000D5DB8
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task ContinueWhenAll(Task[] tasks, Action<Task[]> continuationAction)
		{
			if (continuationAction == null)
			{
				throw new ArgumentNullException("continuationAction");
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return TaskFactory<VoidTaskResult>.ContinueWhenAllImpl(tasks, null, continuationAction, this.m_defaultContinuationOptions, this.m_defaultCancellationToken, this.DefaultScheduler, ref stackCrawlMark);
		}

		/// <summary>Creates a continuation <see cref="T:System.Threading.Tasks.Task" /> that will be started upon the completion of a set of provided Tasks.</summary>
		/// <returns>The new continuation <see cref="T:System.Threading.Tasks.Task" />.</returns>
		/// <param name="tasks">The array of tasks from which to continue.</param>
		/// <param name="continuationAction">The action delegate to execute when all tasks in the <paramref name="tasks" /> array have completed.</param>
		/// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> that will be assigned to the new continuation task.</param>
		/// <exception cref="T:System.ObjectDisposedException">The exception that is thrown when one of the elements in the <paramref name="tasks" /> array has been disposed.-or-The <see cref="T:System.Threading.CancellationTokenSource" /> that created<paramref name=" cancellationToken" /> has already been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The exception that is thrown when the <paramref name="tasks" /> array is null.-or-The exception that is thrown when the <paramref name="continuationAction" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentException">The exception that is thrown when the <paramref name="tasks" /> array contains a null value.-or-The exception that is thrown when the <paramref name="tasks" /> array is empty.</exception>
		// Token: 0x06003BA9 RID: 15273 RVA: 0x000D7BF4 File Offset: 0x000D5DF4
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task ContinueWhenAll(Task[] tasks, Action<Task[]> continuationAction, CancellationToken cancellationToken)
		{
			if (continuationAction == null)
			{
				throw new ArgumentNullException("continuationAction");
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return TaskFactory<VoidTaskResult>.ContinueWhenAllImpl(tasks, null, continuationAction, this.m_defaultContinuationOptions, cancellationToken, this.DefaultScheduler, ref stackCrawlMark);
		}

		/// <summary>Creates a continuation <see cref="T:System.Threading.Tasks.Task" /> that will be started upon the completion of a set of provided Tasks.</summary>
		/// <returns>The new continuation <see cref="T:System.Threading.Tasks.Task" />.</returns>
		/// <param name="tasks">The array of tasks from which to continue.</param>
		/// <param name="continuationAction">The action delegate to execute when all tasks in the <paramref name="tasks" /> array have completed.</param>
		/// <param name="continuationOptions">The <see cref="T:System.Threading.Tasks.TaskContinuationOptions" /> value that controls the behavior of the created continuation <see cref="T:System.Threading.Tasks.Task" />.</param>
		/// <exception cref="T:System.ObjectDisposedException">The exception that is thrown when one of the elements in the <paramref name="tasks" /> array has been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The exception that is thrown when the <paramref name="tasks" /> array is null.-or-The exception that is thrown when the <paramref name="continuationAction" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The exception that is thrown when the <paramref name="continuationOptions" /> argument specifies an invalid TaskContinuationOptions value. The exception that is thrown when the <paramref name="creationOptions" /> argument specifies an invalid TaskCreationOptions value. For more information, see the Remarks for <see cref="M:System.Threading.Tasks.TaskFactory.FromAsync(System.Func{System.AsyncCallback,System.Object,System.IAsyncResult},System.Action{System.IAsyncResult},System.Object,System.Threading.Tasks.TaskCreationOptions)" /></exception>
		/// <exception cref="T:System.ArgumentException">The exception that is thrown when the <paramref name="tasks" /> array contains a null value.-or-The exception that is thrown when the <paramref name="tasks" /> array is empty.</exception>
		// Token: 0x06003BAA RID: 15274 RVA: 0x000D7C28 File Offset: 0x000D5E28
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task ContinueWhenAll(Task[] tasks, Action<Task[]> continuationAction, TaskContinuationOptions continuationOptions)
		{
			if (continuationAction == null)
			{
				throw new ArgumentNullException("continuationAction");
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return TaskFactory<VoidTaskResult>.ContinueWhenAllImpl(tasks, null, continuationAction, continuationOptions, this.m_defaultCancellationToken, this.DefaultScheduler, ref stackCrawlMark);
		}

		/// <summary>Creates a continuation <see cref="T:System.Threading.Tasks.Task" /> that will be started upon the completion of a set of provided Tasks.</summary>
		/// <returns>The new continuation <see cref="T:System.Threading.Tasks.Task" />.</returns>
		/// <param name="tasks">The array of tasks from which to continue.</param>
		/// <param name="continuationAction">The action delegate to execute when all tasks in the <paramref name="tasks" /> array have completed.</param>
		/// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> that will be assigned to the new continuation task.</param>
		/// <param name="continuationOptions">The <see cref="T:System.Threading.Tasks.TaskContinuationOptions" /> value that controls the behavior of the created continuation <see cref="T:System.Threading.Tasks.Task" />.</param>
		/// <param name="scheduler">The <see cref="T:System.Threading.Tasks.TaskScheduler" /> that is used to schedule the created continuation <see cref="T:System.Threading.Tasks.Task" />.</param>
		/// <exception cref="T:System.ArgumentNullException">The exception that is thrown when the <paramref name="tasks" /> array is null.-or-The exception that is thrown when the <paramref name="continuationAction" /> argument is null.-or-The exception that is thrown when the <paramref name="scheduler" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentException">The exception that is thrown when the <paramref name="tasks" /> array contains a null value.-or-The exception that is thrown when the <paramref name="tasks" /> array is empty.</exception>
		// Token: 0x06003BAB RID: 15275 RVA: 0x000D7C5C File Offset: 0x000D5E5C
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task ContinueWhenAll(Task[] tasks, Action<Task[]> continuationAction, CancellationToken cancellationToken, TaskContinuationOptions continuationOptions, TaskScheduler scheduler)
		{
			if (continuationAction == null)
			{
				throw new ArgumentNullException("continuationAction");
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return TaskFactory<VoidTaskResult>.ContinueWhenAllImpl(tasks, null, continuationAction, continuationOptions, cancellationToken, scheduler, ref stackCrawlMark);
		}

		/// <summary>Creates a continuation <see cref="T:System.Threading.Tasks.Task" /> that will be started upon the completion of a set of provided Tasks.</summary>
		/// <returns>The new continuation <see cref="T:System.Threading.Tasks.Task" />.</returns>
		/// <param name="tasks">The array of tasks from which to continue.</param>
		/// <param name="continuationAction">The action delegate to execute when all tasks in the <paramref name="tasks" /> array have completed.</param>
		/// <typeparam name="TAntecedentResult">The type of the result of the antecedent <paramref name="tasks" />.</typeparam>
		/// <exception cref="T:System.ObjectDisposedException">The exception that is thrown when one of the elements in the <paramref name="tasks" /> array has been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The exception that is thrown when the <paramref name="tasks" /> array is null.-or-The exception that is thrown when the <paramref name="continuationAction" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentException">The exception that is thrown when the <paramref name="tasks" /> array contains a null value.-or-The exception that is thrown when the <paramref name="tasks" /> array is empty.</exception>
		// Token: 0x06003BAC RID: 15276 RVA: 0x000D7C88 File Offset: 0x000D5E88
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task ContinueWhenAll<TAntecedentResult>(Task<TAntecedentResult>[] tasks, Action<Task<TAntecedentResult>[]> continuationAction)
		{
			if (continuationAction == null)
			{
				throw new ArgumentNullException("continuationAction");
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return TaskFactory<VoidTaskResult>.ContinueWhenAllImpl<TAntecedentResult>(tasks, null, continuationAction, this.m_defaultContinuationOptions, this.m_defaultCancellationToken, this.DefaultScheduler, ref stackCrawlMark);
		}

		/// <summary>Creates a continuation <see cref="T:System.Threading.Tasks.Task" /> that will be started upon the completion of a set of provided Tasks.</summary>
		/// <returns>The new continuation <see cref="T:System.Threading.Tasks.Task" />.</returns>
		/// <param name="tasks">The array of tasks from which to continue.</param>
		/// <param name="continuationAction">The action delegate to execute when all tasks in the <paramref name="tasks" /> array have completed.</param>
		/// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> that will be assigned to the new continuation task.</param>
		/// <typeparam name="TAntecedentResult">The type of the result of the antecedent <paramref name="tasks" />.</typeparam>
		/// <exception cref="T:System.ObjectDisposedException">The exception that is thrown when one of the elements in the <paramref name="tasks" /> array has been disposed.-or-The <see cref="T:System.Threading.CancellationTokenSource" /> that created<paramref name=" cancellationToken" /> has already been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The exception that is thrown when the <paramref name="tasks" /> array is null.-or-The exception that is thrown when the <paramref name="continuationAction" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentException">The exception that is thrown when the <paramref name="tasks" /> array contains a null value.-or-The exception that is thrown when the <paramref name="tasks" /> array is empty.</exception>
		// Token: 0x06003BAD RID: 15277 RVA: 0x000D7CC4 File Offset: 0x000D5EC4
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task ContinueWhenAll<TAntecedentResult>(Task<TAntecedentResult>[] tasks, Action<Task<TAntecedentResult>[]> continuationAction, CancellationToken cancellationToken)
		{
			if (continuationAction == null)
			{
				throw new ArgumentNullException("continuationAction");
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return TaskFactory<VoidTaskResult>.ContinueWhenAllImpl<TAntecedentResult>(tasks, null, continuationAction, this.m_defaultContinuationOptions, cancellationToken, this.DefaultScheduler, ref stackCrawlMark);
		}

		/// <summary>Creates a continuation <see cref="T:System.Threading.Tasks.Task" /> that will be started upon the completion of a set of provided Tasks.</summary>
		/// <returns>The new continuation <see cref="T:System.Threading.Tasks.Task" />.</returns>
		/// <param name="tasks">The array of tasks from which to continue.</param>
		/// <param name="continuationAction">The action delegate to execute when all tasks in the <paramref name="tasks" /> array have completed.</param>
		/// <param name="continuationOptions">The <see cref="T:System.Threading.Tasks.TaskContinuationOptions" /> value that controls the behavior of the created continuation <see cref="T:System.Threading.Tasks.Task" />.</param>
		/// <typeparam name="TAntecedentResult">The type of the result of the antecedent <paramref name="tasks" />.</typeparam>
		/// <exception cref="T:System.ObjectDisposedException">The exception that is thrown when one of the elements in the <paramref name="tasks" /> array has been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The exception that is thrown when the <paramref name="tasks" /> array is null.-or-The exception that is thrown when the <paramref name="continuationAction" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The exception that is thrown when the <paramref name="continuationOptions" /> argument specifies an invalid TaskContinuationOptions value. The exception that is thrown when the <paramref name="creationOptions" /> argument specifies an invalid TaskCreationOptions value. For more information, see the Remarks for <see cref="M:System.Threading.Tasks.TaskFactory.FromAsync(System.Func{System.AsyncCallback,System.Object,System.IAsyncResult},System.Action{System.IAsyncResult},System.Object,System.Threading.Tasks.TaskCreationOptions)" /></exception>
		/// <exception cref="T:System.ArgumentException">The exception that is thrown when the <paramref name="tasks" /> array contains a null value.-or-The exception that is thrown when the <paramref name="tasks" /> array is empty.</exception>
		// Token: 0x06003BAE RID: 15278 RVA: 0x000D7CF8 File Offset: 0x000D5EF8
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task ContinueWhenAll<TAntecedentResult>(Task<TAntecedentResult>[] tasks, Action<Task<TAntecedentResult>[]> continuationAction, TaskContinuationOptions continuationOptions)
		{
			if (continuationAction == null)
			{
				throw new ArgumentNullException("continuationAction");
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return TaskFactory<VoidTaskResult>.ContinueWhenAllImpl<TAntecedentResult>(tasks, null, continuationAction, continuationOptions, this.m_defaultCancellationToken, this.DefaultScheduler, ref stackCrawlMark);
		}

		/// <summary>Creates a continuation <see cref="T:System.Threading.Tasks.Task" /> that will be started upon the completion of a set of provided Tasks.</summary>
		/// <returns>The new continuation <see cref="T:System.Threading.Tasks.Task" />.</returns>
		/// <param name="tasks">The array of tasks from which to continue.</param>
		/// <param name="continuationAction">The action delegate to execute when all tasks in the <paramref name="tasks" /> array have completed.</param>
		/// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> that will be assigned to the new continuation task.</param>
		/// <param name="continuationOptions">The <see cref="T:System.Threading.Tasks.TaskContinuationOptions" /> value that controls the behavior of the created continuation <see cref="T:System.Threading.Tasks.Task" />.</param>
		/// <param name="scheduler">The <see cref="T:System.Threading.Tasks.TaskScheduler" /> that is used to schedule the created continuation <see cref="T:System.Threading.Tasks.Task" />.</param>
		/// <typeparam name="TAntecedentResult">The type of the result of the antecedent <paramref name="tasks" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">The exception that is thrown when the <paramref name="tasks" /> array is null.-or-The exception that is thrown when the <paramref name="continuationAction" /> argument is null.-or-The exception that is thrown when the <paramref name="scheduler" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentException">The exception that is thrown when the <paramref name="tasks" /> array contains a null value.-or-The exception that is thrown when the <paramref name="tasks" /> array is empty.</exception>
		// Token: 0x06003BAF RID: 15279 RVA: 0x000D7D2C File Offset: 0x000D5F2C
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task ContinueWhenAll<TAntecedentResult>(Task<TAntecedentResult>[] tasks, Action<Task<TAntecedentResult>[]> continuationAction, CancellationToken cancellationToken, TaskContinuationOptions continuationOptions, TaskScheduler scheduler)
		{
			if (continuationAction == null)
			{
				throw new ArgumentNullException("continuationAction");
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return TaskFactory<VoidTaskResult>.ContinueWhenAllImpl<TAntecedentResult>(tasks, null, continuationAction, continuationOptions, cancellationToken, scheduler, ref stackCrawlMark);
		}

		/// <summary>Creates a continuation <see cref="T:System.Threading.Tasks.Task" /> that will be started upon the completion of a set of provided Tasks.</summary>
		/// <returns>The new continuation <see cref="T:System.Threading.Tasks.Task`1" />.</returns>
		/// <param name="tasks">The array of tasks from which to continue.</param>
		/// <param name="continuationFunction">The function delegate to execute asynchronously when all tasks in the <paramref name="tasks" /> array have completed.</param>
		/// <typeparam name="TResult">The type of the result that is returned by the <paramref name="continuationFunction" /> delegate and associated with the created <see cref="T:System.Threading.Tasks.Task`1" />.</typeparam>
		/// <exception cref="T:System.ObjectDisposedException">The exception that is thrown when one of the elements in the <paramref name="tasks" /> array has been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The exception that is thrown when the <paramref name="tasks" /> array is null.-or-The exception that is thrown when the <paramref name="continuationFunction" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentException">The exception that is thrown when the <paramref name="tasks" /> array contains a null value.-or-The exception that is thrown when the <paramref name="tasks" /> array is empty.</exception>
		// Token: 0x06003BB0 RID: 15280 RVA: 0x000D7D58 File Offset: 0x000D5F58
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task<TResult> ContinueWhenAll<TResult>(Task[] tasks, Func<Task[], TResult> continuationFunction)
		{
			if (continuationFunction == null)
			{
				throw new ArgumentNullException("continuationFunction");
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return TaskFactory<TResult>.ContinueWhenAllImpl(tasks, continuationFunction, null, this.m_defaultContinuationOptions, this.m_defaultCancellationToken, this.DefaultScheduler, ref stackCrawlMark);
		}

		/// <summary>Creates a continuation <see cref="T:System.Threading.Tasks.Task" /> that will be started upon the completion of a set of provided Tasks.</summary>
		/// <returns>The new continuation <see cref="T:System.Threading.Tasks.Task`1" />.</returns>
		/// <param name="tasks">The array of tasks from which to continue.</param>
		/// <param name="continuationFunction">The function delegate to execute asynchronously when all tasks in the <paramref name="tasks" /> array have completed.</param>
		/// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> that will be assigned to the new continuation task.</param>
		/// <typeparam name="TResult">The type of the result that is returned by the <paramref name="continuationFunction" /> delegate and associated with the created <see cref="T:System.Threading.Tasks.Task`1" />.</typeparam>
		/// <exception cref="T:System.ObjectDisposedException">The exception that is thrown when one of the elements in the <paramref name="tasks" /> array has been disposed.-or-The <see cref="T:System.Threading.CancellationTokenSource" /> that created<paramref name=" cancellationToken" /> has already been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The exception that is thrown when the <paramref name="tasks" /> array is null.-or-The exception that is thrown when the <paramref name="continuationFunction" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentException">The exception that is thrown when the <paramref name="tasks" /> array contains a null value.-or-The exception that is thrown when the <paramref name="tasks" /> array is empty.</exception>
		// Token: 0x06003BB1 RID: 15281 RVA: 0x000D7D94 File Offset: 0x000D5F94
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task<TResult> ContinueWhenAll<TResult>(Task[] tasks, Func<Task[], TResult> continuationFunction, CancellationToken cancellationToken)
		{
			if (continuationFunction == null)
			{
				throw new ArgumentNullException("continuationFunction");
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return TaskFactory<TResult>.ContinueWhenAllImpl(tasks, continuationFunction, null, this.m_defaultContinuationOptions, cancellationToken, this.DefaultScheduler, ref stackCrawlMark);
		}

		/// <summary>Creates a continuation <see cref="T:System.Threading.Tasks.Task`1" /> that will be started upon the completion of a set of provided Tasks.</summary>
		/// <returns>The new continuation <see cref="T:System.Threading.Tasks.Task`1" />.</returns>
		/// <param name="tasks">The array of tasks from which to continue.</param>
		/// <param name="continuationFunction">The function delegate to execute asynchronously when all tasks in the <paramref name="tasks" /> array have completed.</param>
		/// <param name="continuationOptions">The <see cref="T:System.Threading.Tasks.TaskContinuationOptions" /> value that controls the behavior of the created continuation <see cref="T:System.Threading.Tasks.Task`1" />.</param>
		/// <typeparam name="TResult">The type of the result that is returned by the <paramref name="continuationFunction" /> delegate and associated with the created <see cref="T:System.Threading.Tasks.Task`1" />.</typeparam>
		/// <exception cref="T:System.ObjectDisposedException">The exception that is thrown when one of the elements in the <paramref name="tasks" /> array has been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The exception that is thrown when the <paramref name="tasks" /> array is null.-or-The exception that is thrown when the <paramref name="continuationFunction" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The exception that is thrown when the <paramref name="continuationOptions" /> argument specifies an invalid TaskContinuationOptions value. The exception that is thrown when the <paramref name="creationOptions" /> argument specifies an invalid TaskCreationOptions value. For more information, see the Remarks for <see cref="M:System.Threading.Tasks.TaskFactory.FromAsync(System.Func{System.AsyncCallback,System.Object,System.IAsyncResult},System.Action{System.IAsyncResult},System.Object,System.Threading.Tasks.TaskCreationOptions)" /></exception>
		/// <exception cref="T:System.ArgumentException">The exception that is thrown when the <paramref name="tasks" /> array contains a null value.-or-The exception that is thrown when the <paramref name="tasks" /> array is empty.</exception>
		// Token: 0x06003BB2 RID: 15282 RVA: 0x000D7DC8 File Offset: 0x000D5FC8
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task<TResult> ContinueWhenAll<TResult>(Task[] tasks, Func<Task[], TResult> continuationFunction, TaskContinuationOptions continuationOptions)
		{
			if (continuationFunction == null)
			{
				throw new ArgumentNullException("continuationFunction");
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return TaskFactory<TResult>.ContinueWhenAllImpl(tasks, continuationFunction, null, continuationOptions, this.m_defaultCancellationToken, this.DefaultScheduler, ref stackCrawlMark);
		}

		/// <summary>Creates a continuation <see cref="T:System.Threading.Tasks.Task`1" /> that will be started upon the completion of a set of provided Tasks.</summary>
		/// <returns>The new continuation <see cref="T:System.Threading.Tasks.Task`1" />.</returns>
		/// <param name="tasks">The array of tasks from which to continue.</param>
		/// <param name="continuationFunction">The function delegate to execute asynchronously when all tasks in the <paramref name="tasks" /> array have completed.</param>
		/// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> that will be assigned to the new continuation task.</param>
		/// <param name="continuationOptions">The <see cref="T:System.Threading.Tasks.TaskContinuationOptions" /> value that controls the behavior of the created continuation <see cref="T:System.Threading.Tasks.Task`1" />.</param>
		/// <param name="scheduler">The <see cref="T:System.Threading.Tasks.TaskScheduler" /> that is used to schedule the created continuation <see cref="T:System.Threading.Tasks.Task`1" />.</param>
		/// <typeparam name="TResult">The type of the result that is returned by the <paramref name="continuationFunction" /> delegate and associated with the created <see cref="T:System.Threading.Tasks.Task`1" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">The exception that is thrown when the <paramref name="tasks" /> array is null.-or-The exception that is thrown when the <paramref name="continuationFunction" /> argument is null.-or-The exception that is thrown when the <paramref name="scheduler" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentException">The exception that is thrown when the <paramref name="tasks" /> array contains a null value.-or-The exception that is thrown when the <paramref name="tasks" /> array is empty.</exception>
		// Token: 0x06003BB3 RID: 15283 RVA: 0x000D7DFC File Offset: 0x000D5FFC
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task<TResult> ContinueWhenAll<TResult>(Task[] tasks, Func<Task[], TResult> continuationFunction, CancellationToken cancellationToken, TaskContinuationOptions continuationOptions, TaskScheduler scheduler)
		{
			if (continuationFunction == null)
			{
				throw new ArgumentNullException("continuationFunction");
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return TaskFactory<TResult>.ContinueWhenAllImpl(tasks, continuationFunction, null, continuationOptions, cancellationToken, scheduler, ref stackCrawlMark);
		}

		/// <summary>Creates a continuation <see cref="T:System.Threading.Tasks.Task`1" /> that will be started upon the completion of a set of provided Tasks.</summary>
		/// <returns>The new continuation <see cref="T:System.Threading.Tasks.Task`1" />.</returns>
		/// <param name="tasks">The array of tasks from which to continue.</param>
		/// <param name="continuationFunction">The function delegate to execute asynchronously when all tasks in the <paramref name="tasks" /> array have completed.</param>
		/// <typeparam name="TAntecedentResult">The type of the result of the antecedent <paramref name="tasks" />.</typeparam>
		/// <typeparam name="TResult">The type of the result that is returned by the <paramref name="continuationFunction" /> delegate and associated with the created <see cref="T:System.Threading.Tasks.Task`1" />.</typeparam>
		/// <exception cref="T:System.ObjectDisposedException">The exception that is thrown when one of the elements in the <paramref name="tasks" /> array has been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The exception that is thrown when the <paramref name="tasks" /> array is null.-or-The exception that is thrown when the <paramref name="continuationFunction" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentException">The exception that is thrown when the <paramref name="tasks" /> array contains a null value.-or-The exception that is thrown when the <paramref name="tasks" /> array is empty.</exception>
		// Token: 0x06003BB4 RID: 15284 RVA: 0x000D7E28 File Offset: 0x000D6028
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task<TResult> ContinueWhenAll<TAntecedentResult, TResult>(Task<TAntecedentResult>[] tasks, Func<Task<TAntecedentResult>[], TResult> continuationFunction)
		{
			if (continuationFunction == null)
			{
				throw new ArgumentNullException("continuationFunction");
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return TaskFactory<TResult>.ContinueWhenAllImpl<TAntecedentResult>(tasks, continuationFunction, null, this.m_defaultContinuationOptions, this.m_defaultCancellationToken, this.DefaultScheduler, ref stackCrawlMark);
		}

		/// <summary>Creates a continuation <see cref="T:System.Threading.Tasks.Task`1" /> that will be started upon the completion of a set of provided Tasks.</summary>
		/// <returns>The new continuation <see cref="T:System.Threading.Tasks.Task`1" />.</returns>
		/// <param name="tasks">The array of tasks from which to continue.</param>
		/// <param name="continuationFunction">The function delegate to execute asynchronously when all tasks in the <paramref name="tasks" /> array have completed.</param>
		/// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> that will be assigned to the new continuation task.</param>
		/// <typeparam name="TAntecedentResult">The type of the result of the antecedent <paramref name="tasks" />.</typeparam>
		/// <typeparam name="TResult">The type of the result that is returned by the <paramref name="continuationFunction" /> delegate and associated with the created <see cref="T:System.Threading.Tasks.Task`1" />.</typeparam>
		/// <exception cref="T:System.ObjectDisposedException">The exception that is thrown when one of the elements in the <paramref name="tasks" /> array has been disposed.-or-The <see cref="T:System.Threading.CancellationTokenSource" /> that created<paramref name=" cancellationToken" /> has already been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The exception that is thrown when the <paramref name="tasks" /> array is null.-or-The exception that is thrown when the <paramref name="continuationFunction" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentException">The exception that is thrown when the <paramref name="tasks" /> array contains a null value.-or-The exception that is thrown when the <paramref name="tasks" /> array is empty.</exception>
		// Token: 0x06003BB5 RID: 15285 RVA: 0x000D7E64 File Offset: 0x000D6064
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task<TResult> ContinueWhenAll<TAntecedentResult, TResult>(Task<TAntecedentResult>[] tasks, Func<Task<TAntecedentResult>[], TResult> continuationFunction, CancellationToken cancellationToken)
		{
			if (continuationFunction == null)
			{
				throw new ArgumentNullException("continuationFunction");
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return TaskFactory<TResult>.ContinueWhenAllImpl<TAntecedentResult>(tasks, continuationFunction, null, this.m_defaultContinuationOptions, cancellationToken, this.DefaultScheduler, ref stackCrawlMark);
		}

		/// <summary>Creates a continuation <see cref="T:System.Threading.Tasks.Task`1" /> that will be started upon the completion of a set of provided Tasks.</summary>
		/// <returns>The new continuation <see cref="T:System.Threading.Tasks.Task`1" />.</returns>
		/// <param name="tasks">The array of tasks from which to continue.</param>
		/// <param name="continuationFunction">The function delegate to execute asynchronously when all tasks in the <paramref name="tasks" /> array have completed.</param>
		/// <param name="continuationOptions">The <see cref="T:System.Threading.Tasks.TaskContinuationOptions" /> value that controls the behavior of the created continuation <see cref="T:System.Threading.Tasks.Task`1" />.</param>
		/// <typeparam name="TAntecedentResult">The type of the result of the antecedent <paramref name="tasks" />.</typeparam>
		/// <typeparam name="TResult">The type of the result that is returned by the <paramref name="continuationFunction" /> delegate and associated with the created <see cref="T:System.Threading.Tasks.Task`1" />.</typeparam>
		/// <exception cref="T:System.ObjectDisposedException">The exception that is thrown when one of the elements in the <paramref name="tasks" /> array has been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The exception that is thrown when the <paramref name="tasks" /> array is null.-or-The exception that is thrown when the <paramref name="continuationFunction" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The exception that is thrown when the <paramref name="continuationOptions" /> argument specifies an invalid TaskContinuationOptions value.</exception>
		/// <exception cref="T:System.ArgumentException">The exception that is thrown when the <paramref name="tasks" /> array contains a null value.-or-The exception that is thrown when the <paramref name="tasks" /> array is empty.</exception>
		// Token: 0x06003BB6 RID: 15286 RVA: 0x000D7E98 File Offset: 0x000D6098
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task<TResult> ContinueWhenAll<TAntecedentResult, TResult>(Task<TAntecedentResult>[] tasks, Func<Task<TAntecedentResult>[], TResult> continuationFunction, TaskContinuationOptions continuationOptions)
		{
			if (continuationFunction == null)
			{
				throw new ArgumentNullException("continuationFunction");
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return TaskFactory<TResult>.ContinueWhenAllImpl<TAntecedentResult>(tasks, continuationFunction, null, continuationOptions, this.m_defaultCancellationToken, this.DefaultScheduler, ref stackCrawlMark);
		}

		/// <summary>Creates a continuation <see cref="T:System.Threading.Tasks.Task`1" /> that will be started upon the completion of a set of provided Tasks.</summary>
		/// <returns>The new continuation <see cref="T:System.Threading.Tasks.Task`1" />.</returns>
		/// <param name="tasks">The array of tasks from which to continue.</param>
		/// <param name="continuationFunction">The function delegate to execute asynchronously when all tasks in the <paramref name="tasks" /> array have completed.</param>
		/// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> that will be assigned to the new continuation task.</param>
		/// <param name="continuationOptions">The <see cref="T:System.Threading.Tasks.TaskContinuationOptions" /> value that controls the behavior of the created continuation <see cref="T:System.Threading.Tasks.Task`1" />.</param>
		/// <param name="scheduler">The <see cref="T:System.Threading.Tasks.TaskScheduler" /> that is used to schedule the created continuation <see cref="T:System.Threading.Tasks.Task`1" />.</param>
		/// <typeparam name="TAntecedentResult">The type of the result of the antecedent <paramref name="tasks" />.</typeparam>
		/// <typeparam name="TResult">The type of the result that is returned by the <paramref name="continuationFunction" /> delegate and associated with the created <see cref="T:System.Threading.Tasks.Task`1" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">The exception that is thrown when the <paramref name="tasks" /> array is null.-or-The exception that is thrown when the <paramref name="continuationFunction" /> argument is null.-or-The exception that is thrown when the <paramref name="scheduler" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentException">The exception that is thrown when the <paramref name="tasks" /> array contains a null value.-or-The exception that is thrown when the <paramref name="tasks" /> array is empty.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The exception that is thrown when the <paramref name="continuationOptions" /> argument specifies an invalid TaskContinuationOptions value. The exception that is thrown when the <paramref name="creationOptions" /> argument specifies an invalid TaskCreationOptions value. For more information, see the Remarks for <see cref="M:System.Threading.Tasks.TaskFactory.FromAsync(System.Func{System.AsyncCallback,System.Object,System.IAsyncResult},System.Action{System.IAsyncResult},System.Object,System.Threading.Tasks.TaskCreationOptions)" /></exception>
		/// <exception cref="T:System.ObjectDisposedException">The exception that is thrown when one of the elements in the <paramref name="tasks" /> array has been disposed.-or-The <see cref="T:System.Threading.CancellationTokenSource" /> that created<paramref name=" cancellationToken" /> has already been disposed.</exception>
		// Token: 0x06003BB7 RID: 15287 RVA: 0x000D7ECC File Offset: 0x000D60CC
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task<TResult> ContinueWhenAll<TAntecedentResult, TResult>(Task<TAntecedentResult>[] tasks, Func<Task<TAntecedentResult>[], TResult> continuationFunction, CancellationToken cancellationToken, TaskContinuationOptions continuationOptions, TaskScheduler scheduler)
		{
			if (continuationFunction == null)
			{
				throw new ArgumentNullException("continuationFunction");
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return TaskFactory<TResult>.ContinueWhenAllImpl<TAntecedentResult>(tasks, continuationFunction, null, continuationOptions, cancellationToken, scheduler, ref stackCrawlMark);
		}

		// Token: 0x06003BB8 RID: 15288 RVA: 0x000D7EF8 File Offset: 0x000D60F8
		internal static Task<Task> CommonCWAnyLogic(IList<Task> tasks)
		{
			TaskFactory.CompleteOnInvokePromise completeOnInvokePromise = new TaskFactory.CompleteOnInvokePromise(tasks);
			bool flag = false;
			int count = tasks.Count;
			for (int i = 0; i < count; i++)
			{
				Task task = tasks[i];
				if (task == null)
				{
					throw new ArgumentException(Environment.GetResourceString("The tasks argument included a null value."), "tasks");
				}
				if (!flag)
				{
					if (completeOnInvokePromise.IsCompleted)
					{
						flag = true;
					}
					else if (task.IsCompleted)
					{
						completeOnInvokePromise.Invoke(task);
						flag = true;
					}
					else
					{
						task.AddCompletionAction(completeOnInvokePromise);
						if (completeOnInvokePromise.IsCompleted)
						{
							task.RemoveContinuation(completeOnInvokePromise);
						}
					}
				}
			}
			return completeOnInvokePromise;
		}

		/// <summary>Creates a continuation <see cref="T:System.Threading.Tasks.Task" /> that will be started upon the completion of any Task in the provided set.</summary>
		/// <returns>The new continuation <see cref="T:System.Threading.Tasks.Task" />.</returns>
		/// <param name="tasks">The array of tasks from which to continue when one task completes.</param>
		/// <param name="continuationAction">The action delegate to execute when one task in the <paramref name="tasks" /> array completes.</param>
		/// <exception cref="T:System.ObjectDisposedException">The exception that is thrown when one of the elements in the <paramref name="tasks" /> array has been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The exception that is thrown when the <paramref name="tasks" /> array is null.-or-The exception that is thrown when the <paramref name="continuationAction" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentException">The exception that is thrown when the <paramref name="tasks" /> array contains a null value.-or-The exception that is thrown when the <paramref name="tasks" /> array is empty.</exception>
		// Token: 0x06003BB9 RID: 15289 RVA: 0x000D7F80 File Offset: 0x000D6180
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task ContinueWhenAny(Task[] tasks, Action<Task> continuationAction)
		{
			if (continuationAction == null)
			{
				throw new ArgumentNullException("continuationAction");
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return TaskFactory<VoidTaskResult>.ContinueWhenAnyImpl(tasks, null, continuationAction, this.m_defaultContinuationOptions, this.m_defaultCancellationToken, this.DefaultScheduler, ref stackCrawlMark);
		}

		/// <summary>Creates a continuation <see cref="T:System.Threading.Tasks.Task" /> that will be started upon the completion of any Task in the provided set.</summary>
		/// <returns>The new continuation <see cref="T:System.Threading.Tasks.Task" />.</returns>
		/// <param name="tasks">The array of tasks from which to continue when one task completes.</param>
		/// <param name="continuationAction">The action delegate to execute when one task in the <paramref name="tasks" /> array completes.</param>
		/// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> that will be assigned to the new continuation task.</param>
		/// <exception cref="T:System.ObjectDisposedException">The exception that is thrown when one of the elements in the <paramref name="tasks" /> array has been disposed.-or-The provided <see cref="T:System.Threading.CancellationToken" /> has already been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The exception that is thrown when the <paramref name="tasks" /> array is null.-or-The exception that is thrown when the <paramref name="continuationAction" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentException">The exception that is thrown when the <paramref name="tasks" /> array contains a null value.-or-The exception that is thrown when the <paramref name="tasks" /> array is empty.</exception>
		// Token: 0x06003BBA RID: 15290 RVA: 0x000D7FBC File Offset: 0x000D61BC
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task ContinueWhenAny(Task[] tasks, Action<Task> continuationAction, CancellationToken cancellationToken)
		{
			if (continuationAction == null)
			{
				throw new ArgumentNullException("continuationAction");
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return TaskFactory<VoidTaskResult>.ContinueWhenAnyImpl(tasks, null, continuationAction, this.m_defaultContinuationOptions, cancellationToken, this.DefaultScheduler, ref stackCrawlMark);
		}

		/// <summary>Creates a continuation <see cref="T:System.Threading.Tasks.Task" /> that will be started upon the completion of any Task in the provided set.</summary>
		/// <returns>The new continuation <see cref="T:System.Threading.Tasks.Task" />.</returns>
		/// <param name="tasks">The array of tasks from which to continue when one task completes.</param>
		/// <param name="continuationAction">The action delegate to execute when one task in the <paramref name="tasks" /> array completes.</param>
		/// <param name="continuationOptions">The <see cref="T:System.Threading.Tasks.TaskContinuationOptions" /> value that controls the behavior of the created continuation <see cref="T:System.Threading.Tasks.Task" />.</param>
		/// <exception cref="T:System.ObjectDisposedException">The exception that is thrown when one of the elements in the <paramref name="tasks" /> array has been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The exception that is thrown when the <paramref name="tasks" /> array is null.-or-The exception that is thrown when the <paramref name="continuationAction" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The exception that is thrown when the <paramref name="continuationOptions" /> argument specifies an invalid TaskContinuationOptions value.</exception>
		/// <exception cref="T:System.ArgumentException">The exception that is thrown when the <paramref name="tasks" /> array contains a null value.-or-The exception that is thrown when the <paramref name="tasks" /> array is empty.</exception>
		// Token: 0x06003BBB RID: 15291 RVA: 0x000D7FF0 File Offset: 0x000D61F0
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task ContinueWhenAny(Task[] tasks, Action<Task> continuationAction, TaskContinuationOptions continuationOptions)
		{
			if (continuationAction == null)
			{
				throw new ArgumentNullException("continuationAction");
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return TaskFactory<VoidTaskResult>.ContinueWhenAnyImpl(tasks, null, continuationAction, continuationOptions, this.m_defaultCancellationToken, this.DefaultScheduler, ref stackCrawlMark);
		}

		/// <summary>Creates a continuation <see cref="T:System.Threading.Tasks.Task" /> that will be started upon the completion of any Task in the provided set.</summary>
		/// <returns>The new continuation <see cref="T:System.Threading.Tasks.Task" />.</returns>
		/// <param name="tasks">The array of tasks from which to continue when one task completes.</param>
		/// <param name="continuationAction">The action delegate to execute when one task in the <paramref name="tasks" /> array completes.</param>
		/// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> that will be assigned to the new continuation task.</param>
		/// <param name="continuationOptions">The <see cref="T:System.Threading.Tasks.TaskContinuationOptions" /> value that controls the behavior of the created continuation <see cref="T:System.Threading.Tasks.Task" />.</param>
		/// <param name="scheduler">The <see cref="T:System.Threading.Tasks.TaskScheduler" /> that is used to schedule the created continuation <see cref="T:System.Threading.Tasks.Task" />.</param>
		/// <exception cref="T:System.ArgumentNullException">The exception that is thrown when the <paramref name="tasks" /> array is null.-or-The exception that is thrown when the <paramref name="continuationAction" /> argument is null.-or-The exception that is thrown when the <paramref name="scheduler" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentException">The exception that is thrown when the <paramref name="tasks" /> array contains a null value.-or-The exception that is thrown when the <paramref name="tasks" /> array is empty.</exception>
		// Token: 0x06003BBC RID: 15292 RVA: 0x000D8024 File Offset: 0x000D6224
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task ContinueWhenAny(Task[] tasks, Action<Task> continuationAction, CancellationToken cancellationToken, TaskContinuationOptions continuationOptions, TaskScheduler scheduler)
		{
			if (continuationAction == null)
			{
				throw new ArgumentNullException("continuationAction");
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return TaskFactory<VoidTaskResult>.ContinueWhenAnyImpl(tasks, null, continuationAction, continuationOptions, cancellationToken, scheduler, ref stackCrawlMark);
		}

		/// <summary>Creates a continuation <see cref="T:System.Threading.Tasks.Task`1" /> that will be started upon the completion of any Task in the provided set.</summary>
		/// <returns>The new continuation <see cref="T:System.Threading.Tasks.Task`1" />.</returns>
		/// <param name="tasks">The array of tasks from which to continue when one task completes.</param>
		/// <param name="continuationFunction">The function delegate to execute asynchronously when one task in the <paramref name="tasks" /> array completes.</param>
		/// <typeparam name="TResult">The type of the result that is returned by the <paramref name="continuationFunction" /> delegate and associated with the created <see cref="T:System.Threading.Tasks.Task`1" />.</typeparam>
		/// <exception cref="T:System.ObjectDisposedException">The exception that is thrown when one of the elements in the <paramref name="tasks" /> array has been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The exception that is thrown when the <paramref name="tasks" /> array is null.-or-The exception that is thrown when the <paramref name="continuationFunction" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentException">The exception that is thrown when the <paramref name="tasks" /> array contains a null value.-or-The exception that is thrown when the <paramref name="tasks" /> array is empty.</exception>
		// Token: 0x06003BBD RID: 15293 RVA: 0x000D8050 File Offset: 0x000D6250
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task<TResult> ContinueWhenAny<TResult>(Task[] tasks, Func<Task, TResult> continuationFunction)
		{
			if (continuationFunction == null)
			{
				throw new ArgumentNullException("continuationFunction");
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return TaskFactory<TResult>.ContinueWhenAnyImpl(tasks, continuationFunction, null, this.m_defaultContinuationOptions, this.m_defaultCancellationToken, this.DefaultScheduler, ref stackCrawlMark);
		}

		/// <summary>Creates a continuation <see cref="T:System.Threading.Tasks.Task`1" /> that will be started upon the completion of any Task in the provided set.</summary>
		/// <returns>The new continuation <see cref="T:System.Threading.Tasks.Task`1" />.</returns>
		/// <param name="tasks">The array of tasks from which to continue when one task completes.</param>
		/// <param name="continuationFunction">The function delegate to execute asynchronously when one task in the <paramref name="tasks" /> array completes.</param>
		/// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> that will be assigned to the new continuation task.</param>
		/// <typeparam name="TResult">The type of the result that is returned by the <paramref name="continuationFunction" /> delegate and associated with the created <see cref="T:System.Threading.Tasks.Task`1" />.</typeparam>
		/// <exception cref="T:System.ObjectDisposedException">The exception that is thrown when one of the elements in the <paramref name="tasks" /> array has been disposed.-or-The provided <see cref="T:System.Threading.CancellationToken" /> has already been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The exception that is thrown when the <paramref name="tasks" /> array is null.-or-The exception that is thrown when the <paramref name="continuationFunction" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentException">The exception that is thrown when the <paramref name="tasks" /> array contains a null value.-or-The exception that is thrown when the <paramref name="tasks" /> array is empty.</exception>
		// Token: 0x06003BBE RID: 15294 RVA: 0x000D808C File Offset: 0x000D628C
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task<TResult> ContinueWhenAny<TResult>(Task[] tasks, Func<Task, TResult> continuationFunction, CancellationToken cancellationToken)
		{
			if (continuationFunction == null)
			{
				throw new ArgumentNullException("continuationFunction");
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return TaskFactory<TResult>.ContinueWhenAnyImpl(tasks, continuationFunction, null, this.m_defaultContinuationOptions, cancellationToken, this.DefaultScheduler, ref stackCrawlMark);
		}

		/// <summary>Creates a continuation <see cref="T:System.Threading.Tasks.Task`1" /> that will be started upon the completion of any Task in the provided set.</summary>
		/// <returns>The new continuation <see cref="T:System.Threading.Tasks.Task`1" />.</returns>
		/// <param name="tasks">The array of tasks from which to continue when one task completes.</param>
		/// <param name="continuationFunction">The function delegate to execute asynchronously when one task in the <paramref name="tasks" /> array completes.</param>
		/// <param name="continuationOptions">The <see cref="T:System.Threading.Tasks.TaskContinuationOptions" /> value that controls the behavior of the created continuation <see cref="T:System.Threading.Tasks.Task`1" />.</param>
		/// <typeparam name="TResult">The type of the result that is returned by the <paramref name="continuationFunction" /> delegate and associated with the created <see cref="T:System.Threading.Tasks.Task`1" />.</typeparam>
		/// <exception cref="T:System.ObjectDisposedException">The exception that is thrown when one of the elements in the <paramref name="tasks" /> array has been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The exception that is thrown when the <paramref name="tasks" /> array is null.-or-The exception that is thrown when the <paramref name="continuationFunction" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The exception that is thrown when the <paramref name="continuationOptions" /> argument specifies an invalid TaskContinuationOptions value.</exception>
		/// <exception cref="T:System.ArgumentException">The exception that is thrown when the <paramref name="tasks" /> array contains a null value.-or-The exception that is thrown when the <paramref name="tasks" /> array is empty.</exception>
		// Token: 0x06003BBF RID: 15295 RVA: 0x000D80C0 File Offset: 0x000D62C0
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task<TResult> ContinueWhenAny<TResult>(Task[] tasks, Func<Task, TResult> continuationFunction, TaskContinuationOptions continuationOptions)
		{
			if (continuationFunction == null)
			{
				throw new ArgumentNullException("continuationFunction");
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return TaskFactory<TResult>.ContinueWhenAnyImpl(tasks, continuationFunction, null, continuationOptions, this.m_defaultCancellationToken, this.DefaultScheduler, ref stackCrawlMark);
		}

		/// <summary>Creates a continuation <see cref="T:System.Threading.Tasks.Task`1" /> that will be started upon the completion of any Task in the provided set.</summary>
		/// <returns>The new continuation <see cref="T:System.Threading.Tasks.Task`1" />.</returns>
		/// <param name="tasks">The array of tasks from which to continue when one task completes.</param>
		/// <param name="continuationFunction">The function delegate to execute asynchronously when one task in the <paramref name="tasks" /> array completes.</param>
		/// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> that will be assigned to the new continuation task.</param>
		/// <param name="continuationOptions">The <see cref="T:System.Threading.Tasks.TaskContinuationOptions" /> value that controls the behavior of the created continuation <see cref="T:System.Threading.Tasks.Task`1" />.</param>
		/// <param name="scheduler">The <see cref="T:System.Threading.Tasks.TaskScheduler" /> that is used to schedule the created continuation <see cref="T:System.Threading.Tasks.Task`1" />.</param>
		/// <typeparam name="TResult">The type of the result that is returned by the <paramref name="continuationFunction" /> delegate and associated with the created <see cref="T:System.Threading.Tasks.Task`1" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">The exception that is thrown when the <paramref name="tasks" /> array is null.-or-The exception that is thrown when the <paramref name="continuationFunction" /> argument is null.-or-The exception that is thrown when the <paramref name="scheduler" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentException">The exception that is thrown when the <paramref name="tasks" /> array contains a null value.-or-The exception that is thrown when the <paramref name="tasks" /> array is empty.</exception>
		// Token: 0x06003BC0 RID: 15296 RVA: 0x000D80F4 File Offset: 0x000D62F4
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task<TResult> ContinueWhenAny<TResult>(Task[] tasks, Func<Task, TResult> continuationFunction, CancellationToken cancellationToken, TaskContinuationOptions continuationOptions, TaskScheduler scheduler)
		{
			if (continuationFunction == null)
			{
				throw new ArgumentNullException("continuationFunction");
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return TaskFactory<TResult>.ContinueWhenAnyImpl(tasks, continuationFunction, null, continuationOptions, cancellationToken, scheduler, ref stackCrawlMark);
		}

		/// <summary>Creates a continuation <see cref="T:System.Threading.Tasks.Task`1" /> that will be started upon the completion of any Task in the provided set.</summary>
		/// <returns>The new continuation <see cref="T:System.Threading.Tasks.Task`1" />.</returns>
		/// <param name="tasks">The array of tasks from which to continue when one task completes.</param>
		/// <param name="continuationFunction">The function delegate to execute asynchronously when one task in the <paramref name="tasks" /> array completes.</param>
		/// <typeparam name="TAntecedentResult">The type of the result of the antecedent <paramref name="tasks" />.</typeparam>
		/// <typeparam name="TResult">The type of the result that is returned by the <paramref name="continuationFunction" /> delegate and associated with the created <see cref="T:System.Threading.Tasks.Task`1" />.</typeparam>
		/// <exception cref="T:System.ObjectDisposedException">The exception that is thrown when one of the elements in the <paramref name="tasks" /> array has been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The exception that is thrown when the <paramref name="tasks" /> array is null.-or-The exception that is thrown when the <paramref name="continuationFunction" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentException">The exception that is thrown when the <paramref name="tasks" /> array contains a null value.-or-The exception that is thrown when the <paramref name="tasks" /> array is empty.</exception>
		// Token: 0x06003BC1 RID: 15297 RVA: 0x000D8120 File Offset: 0x000D6320
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task<TResult> ContinueWhenAny<TAntecedentResult, TResult>(Task<TAntecedentResult>[] tasks, Func<Task<TAntecedentResult>, TResult> continuationFunction)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			if (continuationFunction == null)
			{
				throw new ArgumentNullException("continuationFunction");
			}
			return TaskFactory<TResult>.ContinueWhenAnyImpl<TAntecedentResult>(tasks, continuationFunction, null, this.m_defaultContinuationOptions, this.m_defaultCancellationToken, this.DefaultScheduler, ref stackCrawlMark);
		}

		/// <summary>Creates a continuation <see cref="T:System.Threading.Tasks.Task`1" /> that will be started upon the completion of any Task in the provided set.</summary>
		/// <returns>The new continuation <see cref="T:System.Threading.Tasks.Task`1" />.</returns>
		/// <param name="tasks">The array of tasks from which to continue when one task completes.</param>
		/// <param name="continuationFunction">The function delegate to execute asynchronously when one task in the <paramref name="tasks" /> array completes.</param>
		/// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> that will be assigned to the new continuation task.</param>
		/// <typeparam name="TAntecedentResult">The type of the result of the antecedent <paramref name="tasks" />.</typeparam>
		/// <typeparam name="TResult">The type of the result that is returned by the <paramref name="continuationFunction" /> delegate and associated with the created <see cref="T:System.Threading.Tasks.Task`1" />.</typeparam>
		/// <exception cref="T:System.ObjectDisposedException">The exception that is thrown when one of the elements in the <paramref name="tasks" /> array has been disposed.-or-The provided <see cref="T:System.Threading.CancellationToken" /> has already been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The exception that is thrown when the <paramref name="tasks" /> array is null.-or-The exception that is thrown when the <paramref name="continuationFunction" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentException">The exception that is thrown when the <paramref name="tasks" /> array contains a null value.-or-The exception that is thrown when the <paramref name="tasks" /> array is empty.</exception>
		// Token: 0x06003BC2 RID: 15298 RVA: 0x000D815C File Offset: 0x000D635C
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task<TResult> ContinueWhenAny<TAntecedentResult, TResult>(Task<TAntecedentResult>[] tasks, Func<Task<TAntecedentResult>, TResult> continuationFunction, CancellationToken cancellationToken)
		{
			if (continuationFunction == null)
			{
				throw new ArgumentNullException("continuationFunction");
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return TaskFactory<TResult>.ContinueWhenAnyImpl<TAntecedentResult>(tasks, continuationFunction, null, this.m_defaultContinuationOptions, cancellationToken, this.DefaultScheduler, ref stackCrawlMark);
		}

		/// <summary>Creates a continuation <see cref="T:System.Threading.Tasks.Task`1" /> that will be started upon the completion of any Task in the provided set.</summary>
		/// <returns>The new continuation <see cref="T:System.Threading.Tasks.Task`1" />.</returns>
		/// <param name="tasks">The array of tasks from which to continue when one task completes.</param>
		/// <param name="continuationFunction">The function delegate to execute asynchronously when one task in the <paramref name="tasks" /> array completes.</param>
		/// <param name="continuationOptions">The <see cref="T:System.Threading.Tasks.TaskContinuationOptions" /> value that controls the behavior of the created continuation <see cref="T:System.Threading.Tasks.Task`1" />.</param>
		/// <typeparam name="TAntecedentResult">The type of the result of the antecedent <paramref name="tasks" />.</typeparam>
		/// <typeparam name="TResult">The type of the result that is returned by the <paramref name="continuationFunction" /> delegate and associated with the created <see cref="T:System.Threading.Tasks.Task`1" />.</typeparam>
		/// <exception cref="T:System.ObjectDisposedException">The exception that is thrown when one of the elements in the <paramref name="tasks" /> array has been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The exception that is thrown when the <paramref name="tasks" /> array is null.-or-The exception that is thrown when the <paramref name="continuationFunction" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The exception that is thrown when the <paramref name="continuationOptions" /> argument specifies an invalid TaskContinuationOptions value.</exception>
		/// <exception cref="T:System.ArgumentException">The exception that is thrown when the <paramref name="tasks" /> array contains a null value.-or-The exception that is thrown when the <paramref name="tasks" /> array is empty.</exception>
		// Token: 0x06003BC3 RID: 15299 RVA: 0x000D8190 File Offset: 0x000D6390
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task<TResult> ContinueWhenAny<TAntecedentResult, TResult>(Task<TAntecedentResult>[] tasks, Func<Task<TAntecedentResult>, TResult> continuationFunction, TaskContinuationOptions continuationOptions)
		{
			if (continuationFunction == null)
			{
				throw new ArgumentNullException("continuationFunction");
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return TaskFactory<TResult>.ContinueWhenAnyImpl<TAntecedentResult>(tasks, continuationFunction, null, continuationOptions, this.m_defaultCancellationToken, this.DefaultScheduler, ref stackCrawlMark);
		}

		/// <summary>Creates a continuation <see cref="T:System.Threading.Tasks.Task`1" /> that will be started upon the completion of any Task in the provided set.</summary>
		/// <returns>The new continuation <see cref="T:System.Threading.Tasks.Task`1" />.</returns>
		/// <param name="tasks">The array of tasks from which to continue when one task completes.</param>
		/// <param name="continuationFunction">The function delegate to execute asynchronously when one task in the <paramref name="tasks" /> array completes.</param>
		/// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> that will be assigned to the new continuation task.</param>
		/// <param name="continuationOptions">The <see cref="T:System.Threading.Tasks.TaskContinuationOptions" /> value that controls the behavior of the created continuation <see cref="T:System.Threading.Tasks.Task`1" />.</param>
		/// <param name="scheduler">The <see cref="T:System.Threading.Tasks.TaskScheduler" /> that is used to schedule the created continuation <see cref="T:System.Threading.Tasks.Task`1" />.</param>
		/// <typeparam name="TAntecedentResult">The type of the result of the antecedent <paramref name="tasks" />.</typeparam>
		/// <typeparam name="TResult">The type of the result that is returned by the <paramref name="continuationFunction" /> delegate and associated with the created <see cref="T:System.Threading.Tasks.Task`1" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">The exception that is thrown when the <paramref name="tasks" /> array is null.-or-The exception that is thrown when the <paramref name="continuationFunction" /> argument is null.-or-The exception that is thrown when the <paramref name="scheduler" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentException">The exception that is thrown when the <paramref name="tasks" /> array contains a null value.-or-The exception that is thrown when the <paramref name="tasks" /> array is empty.</exception>
		// Token: 0x06003BC4 RID: 15300 RVA: 0x000D81C4 File Offset: 0x000D63C4
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task<TResult> ContinueWhenAny<TAntecedentResult, TResult>(Task<TAntecedentResult>[] tasks, Func<Task<TAntecedentResult>, TResult> continuationFunction, CancellationToken cancellationToken, TaskContinuationOptions continuationOptions, TaskScheduler scheduler)
		{
			if (continuationFunction == null)
			{
				throw new ArgumentNullException("continuationFunction");
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return TaskFactory<TResult>.ContinueWhenAnyImpl<TAntecedentResult>(tasks, continuationFunction, null, continuationOptions, cancellationToken, scheduler, ref stackCrawlMark);
		}

		/// <summary>Creates a continuation <see cref="T:System.Threading.Tasks.Task" /> that will be started upon the completion of any Task in the provided set.</summary>
		/// <returns>The new continuation <see cref="T:System.Threading.Tasks.Task" />.</returns>
		/// <param name="tasks">The array of tasks from which to continue when one task completes.</param>
		/// <param name="continuationAction">The action delegate to execute when one task in the <paramref name="tasks" /> array completes.</param>
		/// <typeparam name="TAntecedentResult">The type of the result of the antecedent <paramref name="tasks" />.</typeparam>
		/// <exception cref="T:System.ObjectDisposedException">The exception that is thrown when one of the elements in the <paramref name="tasks" /> array has been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The exception that is thrown when the <paramref name="tasks" /> array is null.-or-The exception that is thrown when the <paramref name="continuationAction" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentException">The exception that is thrown when the <paramref name="tasks" /> array contains a null value.-or-The exception that is thrown when the <paramref name="tasks" /> array is empty.</exception>
		// Token: 0x06003BC5 RID: 15301 RVA: 0x000D81F0 File Offset: 0x000D63F0
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task ContinueWhenAny<TAntecedentResult>(Task<TAntecedentResult>[] tasks, Action<Task<TAntecedentResult>> continuationAction)
		{
			if (continuationAction == null)
			{
				throw new ArgumentNullException("continuationAction");
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return TaskFactory<VoidTaskResult>.ContinueWhenAnyImpl<TAntecedentResult>(tasks, null, continuationAction, this.m_defaultContinuationOptions, this.m_defaultCancellationToken, this.DefaultScheduler, ref stackCrawlMark);
		}

		/// <summary>Creates a continuation <see cref="T:System.Threading.Tasks.Task" /> that will be started upon the completion of any Task in the provided set.</summary>
		/// <returns>The new continuation <see cref="T:System.Threading.Tasks.Task" />.</returns>
		/// <param name="tasks">The array of tasks from which to continue when one task completes.</param>
		/// <param name="continuationAction">The action delegate to execute when one task in the <paramref name="tasks" /> array completes.</param>
		/// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> that will be assigned to the new continuation task.</param>
		/// <typeparam name="TAntecedentResult">The type of the result of the antecedent <paramref name="tasks" />.</typeparam>
		/// <exception cref="T:System.ObjectDisposedException">The exception that is thrown when one of the elements in the <paramref name="tasks" /> array has been disposed.-or-The provided <see cref="T:System.Threading.CancellationToken" /> has already been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The exception that is thrown when the <paramref name="tasks" /> array is null.-or-The exception that is thrown when the <paramref name="continuationAction" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentException">The exception that is thrown when the <paramref name="tasks" /> array contains a null value.-or-The exception that is thrown when the <paramref name="tasks" /> array is empty.</exception>
		// Token: 0x06003BC6 RID: 15302 RVA: 0x000D822C File Offset: 0x000D642C
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task ContinueWhenAny<TAntecedentResult>(Task<TAntecedentResult>[] tasks, Action<Task<TAntecedentResult>> continuationAction, CancellationToken cancellationToken)
		{
			if (continuationAction == null)
			{
				throw new ArgumentNullException("continuationAction");
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return TaskFactory<VoidTaskResult>.ContinueWhenAnyImpl<TAntecedentResult>(tasks, null, continuationAction, this.m_defaultContinuationOptions, cancellationToken, this.DefaultScheduler, ref stackCrawlMark);
		}

		/// <summary>Creates a continuation <see cref="T:System.Threading.Tasks.Task" /> that will be started upon the completion of any Task in the provided set.</summary>
		/// <returns>The new continuation <see cref="T:System.Threading.Tasks.Task" />.</returns>
		/// <param name="tasks">The array of tasks from which to continue when one task completes.</param>
		/// <param name="continuationAction">The action delegate to execute when one task in the <paramref name="tasks" /> array completes.</param>
		/// <param name="continuationOptions">The <see cref="T:System.Threading.Tasks.TaskContinuationOptions" /> value that controls the behavior of the created continuation <see cref="T:System.Threading.Tasks.Task" />.</param>
		/// <typeparam name="TAntecedentResult">The type of the result of the antecedent <paramref name="tasks" />.</typeparam>
		/// <exception cref="T:System.ObjectDisposedException">The exception that is thrown when one of the elements in the <paramref name="tasks" /> array has been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The exception that is thrown when the <paramref name="tasks" /> array is null.-or-The exception that is thrown when the <paramref name="continuationAction" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The exception that is thrown when the <paramref name="continuationOptions" /> argument specifies an invalid TaskContinuationOptions value.</exception>
		/// <exception cref="T:System.ArgumentException">The exception that is thrown when the <paramref name="tasks" /> array contains a null value.-or-The exception that is thrown when the <paramref name="tasks" /> array is empty.</exception>
		// Token: 0x06003BC7 RID: 15303 RVA: 0x000D8260 File Offset: 0x000D6460
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task ContinueWhenAny<TAntecedentResult>(Task<TAntecedentResult>[] tasks, Action<Task<TAntecedentResult>> continuationAction, TaskContinuationOptions continuationOptions)
		{
			if (continuationAction == null)
			{
				throw new ArgumentNullException("continuationAction");
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return TaskFactory<VoidTaskResult>.ContinueWhenAnyImpl<TAntecedentResult>(tasks, null, continuationAction, continuationOptions, this.m_defaultCancellationToken, this.DefaultScheduler, ref stackCrawlMark);
		}

		/// <summary>Creates a continuation <see cref="T:System.Threading.Tasks.Task" /> that will be started upon the completion of any Task in the provided set.</summary>
		/// <returns>The new continuation <see cref="T:System.Threading.Tasks.Task" />.</returns>
		/// <param name="tasks">The array of tasks from which to continue when one task completes.</param>
		/// <param name="continuationAction">The action delegate to execute when one task in the <paramref name="tasks" /> array completes.</param>
		/// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> that will be assigned to the new continuation task.</param>
		/// <param name="continuationOptions">The <see cref="T:System.Threading.Tasks.TaskContinuationOptions" /> value that controls the behavior of the created continuation <see cref="T:System.Threading.Tasks.Task" />.</param>
		/// <param name="scheduler">The <see cref="T:System.Threading.Tasks.TaskScheduler" /> that is used to schedule the created continuation <see cref="T:System.Threading.Tasks.Task`1" />.</param>
		/// <typeparam name="TAntecedentResult">The type of the result of the antecedent <paramref name="tasks" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">The exception that is thrown when the <paramref name="tasks" /> array is null.-or-The exception that is thrown when the <paramref name="continuationAction" /> argument is null.-or-The exception that is thrown when the <paramref name="scheduler" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentException">The exception that is thrown when the <paramref name="tasks" /> array contains a null value.-or-The exception that is thrown when the <paramref name="tasks" /> array is empty.</exception>
		// Token: 0x06003BC8 RID: 15304 RVA: 0x000D8294 File Offset: 0x000D6494
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task ContinueWhenAny<TAntecedentResult>(Task<TAntecedentResult>[] tasks, Action<Task<TAntecedentResult>> continuationAction, CancellationToken cancellationToken, TaskContinuationOptions continuationOptions, TaskScheduler scheduler)
		{
			if (continuationAction == null)
			{
				throw new ArgumentNullException("continuationAction");
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return TaskFactory<VoidTaskResult>.ContinueWhenAnyImpl<TAntecedentResult>(tasks, null, continuationAction, continuationOptions, cancellationToken, scheduler, ref stackCrawlMark);
		}

		// Token: 0x06003BC9 RID: 15305 RVA: 0x000D82C0 File Offset: 0x000D64C0
		internal static Task[] CheckMultiContinuationTasksAndCopy(Task[] tasks)
		{
			if (tasks == null)
			{
				throw new ArgumentNullException("tasks");
			}
			if (tasks.Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("The tasks argument contains no tasks."), "tasks");
			}
			Task[] array = new Task[tasks.Length];
			for (int i = 0; i < tasks.Length; i++)
			{
				array[i] = tasks[i];
				if (array[i] == null)
				{
					throw new ArgumentException(Environment.GetResourceString("The tasks argument included a null value."), "tasks");
				}
			}
			return array;
		}

		// Token: 0x06003BCA RID: 15306 RVA: 0x000D832C File Offset: 0x000D652C
		internal static Task<TResult>[] CheckMultiContinuationTasksAndCopy<TResult>(Task<TResult>[] tasks)
		{
			if (tasks == null)
			{
				throw new ArgumentNullException("tasks");
			}
			if (tasks.Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("The tasks argument contains no tasks."), "tasks");
			}
			Task<TResult>[] array = new Task<TResult>[tasks.Length];
			for (int i = 0; i < tasks.Length; i++)
			{
				array[i] = tasks[i];
				if (array[i] == null)
				{
					throw new ArgumentException(Environment.GetResourceString("The tasks argument included a null value."), "tasks");
				}
			}
			return array;
		}

		// Token: 0x06003BCB RID: 15307 RVA: 0x000D8398 File Offset: 0x000D6598
		internal static void CheckMultiTaskContinuationOptions(TaskContinuationOptions continuationOptions)
		{
			if ((continuationOptions & (TaskContinuationOptions.LongRunning | TaskContinuationOptions.ExecuteSynchronously)) == (TaskContinuationOptions.LongRunning | TaskContinuationOptions.ExecuteSynchronously))
			{
				throw new ArgumentOutOfRangeException("continuationOptions", Environment.GetResourceString("The specified TaskContinuationOptions combined LongRunning and ExecuteSynchronously.  Synchronous continuations should not be long running."));
			}
			if ((continuationOptions & ~(TaskContinuationOptions.PreferFairness | TaskContinuationOptions.LongRunning | TaskContinuationOptions.AttachedToParent | TaskContinuationOptions.DenyChildAttach | TaskContinuationOptions.HideScheduler | TaskContinuationOptions.LazyCancellation | TaskContinuationOptions.NotOnRanToCompletion | TaskContinuationOptions.NotOnFaulted | TaskContinuationOptions.NotOnCanceled | TaskContinuationOptions.ExecuteSynchronously)) != TaskContinuationOptions.None)
			{
				throw new ArgumentOutOfRangeException("continuationOptions");
			}
			if ((continuationOptions & (TaskContinuationOptions.NotOnRanToCompletion | TaskContinuationOptions.NotOnFaulted | TaskContinuationOptions.NotOnCanceled)) != TaskContinuationOptions.None)
			{
				throw new ArgumentOutOfRangeException("continuationOptions", Environment.GetResourceString("It is invalid to exclude specific continuation kinds for continuations off of multiple tasks."));
			}
		}

		// Token: 0x04001F02 RID: 7938
		private CancellationToken m_defaultCancellationToken;

		// Token: 0x04001F03 RID: 7939
		private TaskScheduler m_defaultScheduler;

		// Token: 0x04001F04 RID: 7940
		private TaskCreationOptions m_defaultCreationOptions;

		// Token: 0x04001F05 RID: 7941
		private TaskContinuationOptions m_defaultContinuationOptions;

		// Token: 0x0200051B RID: 1307
		private sealed class CompleteOnCountdownPromise : Task<Task[]>, ITaskCompletionAction
		{
			// Token: 0x06003BCC RID: 15308 RVA: 0x000D83FA File Offset: 0x000D65FA
			internal CompleteOnCountdownPromise(Task[] tasksCopy)
			{
				this._tasks = tasksCopy;
				this._count = tasksCopy.Length;
				if (AsyncCausalityTracer.LoggingOn)
				{
					AsyncCausalityTracer.TraceOperationCreation(CausalityTraceLevel.Required, base.Id, "TaskFactory.ContinueWhenAll", 0UL);
				}
				if (Task.s_asyncDebuggingEnabled)
				{
					Task.AddToActiveTasks(this);
				}
			}

			// Token: 0x06003BCD RID: 15309 RVA: 0x000D843C File Offset: 0x000D663C
			public void Invoke(Task completingTask)
			{
				if (AsyncCausalityTracer.LoggingOn)
				{
					AsyncCausalityTracer.TraceOperationRelation(CausalityTraceLevel.Important, base.Id, CausalityRelation.Join);
				}
				if (completingTask.IsWaitNotificationEnabled)
				{
					base.SetNotificationForWaitCompletion(true);
				}
				if (Interlocked.Decrement(ref this._count) == 0)
				{
					if (AsyncCausalityTracer.LoggingOn)
					{
						AsyncCausalityTracer.TraceOperationCompletion(CausalityTraceLevel.Required, base.Id, AsyncCausalityStatus.Completed);
					}
					if (Task.s_asyncDebuggingEnabled)
					{
						Task.RemoveFromActiveTasks(base.Id);
					}
					base.TrySetResult(this._tasks);
				}
			}

			// Token: 0x170009C2 RID: 2498
			// (get) Token: 0x06003BCE RID: 15310 RVA: 0x000D84AC File Offset: 0x000D66AC
			internal override bool ShouldNotifyDebuggerOfWaitCompletion
			{
				get
				{
					return base.ShouldNotifyDebuggerOfWaitCompletion && Task.AnyTaskRequiresNotifyDebuggerOfWaitCompletion(this._tasks);
				}
			}

			// Token: 0x04001F06 RID: 7942
			private readonly Task[] _tasks;

			// Token: 0x04001F07 RID: 7943
			private int _count;
		}

		// Token: 0x0200051C RID: 1308
		private sealed class CompleteOnCountdownPromise<T> : Task<Task<T>[]>, ITaskCompletionAction
		{
			// Token: 0x06003BCF RID: 15311 RVA: 0x000D84C3 File Offset: 0x000D66C3
			internal CompleteOnCountdownPromise(Task<T>[] tasksCopy)
			{
				this._tasks = tasksCopy;
				this._count = tasksCopy.Length;
				if (AsyncCausalityTracer.LoggingOn)
				{
					AsyncCausalityTracer.TraceOperationCreation(CausalityTraceLevel.Required, base.Id, "TaskFactory.ContinueWhenAll<>", 0UL);
				}
				if (Task.s_asyncDebuggingEnabled)
				{
					Task.AddToActiveTasks(this);
				}
			}

			// Token: 0x06003BD0 RID: 15312 RVA: 0x000D8504 File Offset: 0x000D6704
			public void Invoke(Task completingTask)
			{
				if (AsyncCausalityTracer.LoggingOn)
				{
					AsyncCausalityTracer.TraceOperationRelation(CausalityTraceLevel.Important, base.Id, CausalityRelation.Join);
				}
				if (completingTask.IsWaitNotificationEnabled)
				{
					base.SetNotificationForWaitCompletion(true);
				}
				if (Interlocked.Decrement(ref this._count) == 0)
				{
					if (AsyncCausalityTracer.LoggingOn)
					{
						AsyncCausalityTracer.TraceOperationCompletion(CausalityTraceLevel.Required, base.Id, AsyncCausalityStatus.Completed);
					}
					if (Task.s_asyncDebuggingEnabled)
					{
						Task.RemoveFromActiveTasks(base.Id);
					}
					base.TrySetResult(this._tasks);
				}
			}

			// Token: 0x170009C3 RID: 2499
			// (get) Token: 0x06003BD1 RID: 15313 RVA: 0x000D8574 File Offset: 0x000D6774
			internal override bool ShouldNotifyDebuggerOfWaitCompletion
			{
				get
				{
					return base.ShouldNotifyDebuggerOfWaitCompletion && Task.AnyTaskRequiresNotifyDebuggerOfWaitCompletion(this._tasks);
				}
			}

			// Token: 0x04001F08 RID: 7944
			private readonly Task<T>[] _tasks;

			// Token: 0x04001F09 RID: 7945
			private int _count;
		}

		// Token: 0x0200051D RID: 1309
		internal sealed class CompleteOnInvokePromise : Task<Task>, ITaskCompletionAction
		{
			// Token: 0x06003BD2 RID: 15314 RVA: 0x000D858B File Offset: 0x000D678B
			public CompleteOnInvokePromise(IList<Task> tasks)
			{
				this._tasks = tasks;
				if (AsyncCausalityTracer.LoggingOn)
				{
					AsyncCausalityTracer.TraceOperationCreation(CausalityTraceLevel.Required, base.Id, "TaskFactory.ContinueWhenAny", 0UL);
				}
				if (Task.s_asyncDebuggingEnabled)
				{
					Task.AddToActiveTasks(this);
				}
			}

			// Token: 0x06003BD3 RID: 15315 RVA: 0x000D85C4 File Offset: 0x000D67C4
			public void Invoke(Task completingTask)
			{
				if (Interlocked.CompareExchange(ref this.m_firstTaskAlreadyCompleted, 1, 0) == 0)
				{
					if (AsyncCausalityTracer.LoggingOn)
					{
						AsyncCausalityTracer.TraceOperationRelation(CausalityTraceLevel.Important, base.Id, CausalityRelation.Choice);
						AsyncCausalityTracer.TraceOperationCompletion(CausalityTraceLevel.Required, base.Id, AsyncCausalityStatus.Completed);
					}
					if (Task.s_asyncDebuggingEnabled)
					{
						Task.RemoveFromActiveTasks(base.Id);
					}
					base.TrySetResult(completingTask);
					IList<Task> tasks = this._tasks;
					int count = tasks.Count;
					for (int i = 0; i < count; i++)
					{
						Task task = tasks[i];
						if (task != null && !task.IsCompleted)
						{
							task.RemoveContinuation(this);
						}
					}
					this._tasks = null;
				}
			}

			// Token: 0x04001F0A RID: 7946
			private IList<Task> _tasks;

			// Token: 0x04001F0B RID: 7947
			private int m_firstTaskAlreadyCompleted;
		}
	}
}
