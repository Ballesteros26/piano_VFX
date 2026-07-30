using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Security;
using System.Security.Permissions;

namespace System.Threading.Tasks
{
	/// <summary>Represents an asynchronous operation.</summary>
	// Token: 0x020004F6 RID: 1270
	[DebuggerTypeProxy(typeof(SystemThreadingTasks_TaskDebugView))]
	[DebuggerDisplay("Id = {Id}, Status = {Status}, Method = {DebuggerDisplayMethodDescription}")]
	[HostProtection(SecurityAction.LinkDemand, Synchronization = true, ExternalThreading = true)]
	public class Task : IThreadPoolWorkItem, IAsyncResult, IDisposable
	{
		// Token: 0x06003A0D RID: 14861 RVA: 0x000D264C File Offset: 0x000D084C
		[FriendAccessAllowed]
		internal static bool AddToActiveTasks(Task task)
		{
			object obj = Task.s_activeTasksLock;
			lock (obj)
			{
				Task.s_currentActiveTasks[task.Id] = task;
			}
			return true;
		}

		// Token: 0x06003A0E RID: 14862 RVA: 0x000D2698 File Offset: 0x000D0898
		[FriendAccessAllowed]
		internal static void RemoveFromActiveTasks(int taskId)
		{
			object obj = Task.s_activeTasksLock;
			lock (obj)
			{
				Task.s_currentActiveTasks.Remove(taskId);
			}
		}

		// Token: 0x06003A0F RID: 14863 RVA: 0x000D26E0 File Offset: 0x000D08E0
		internal Task(bool canceled, TaskCreationOptions creationOptions, CancellationToken ct)
		{
			if (canceled)
			{
				this.m_stateFlags = (int)((TaskCreationOptions)5242880 | creationOptions);
				Task.ContingentProperties contingentProperties = (this.m_contingentProperties = new Task.ContingentProperties());
				contingentProperties.m_cancellationToken = ct;
				contingentProperties.m_internalCancellationRequested = 1;
				return;
			}
			this.m_stateFlags = (int)((TaskCreationOptions)16777216 | creationOptions);
		}

		// Token: 0x06003A10 RID: 14864 RVA: 0x000D2736 File Offset: 0x000D0936
		internal Task()
		{
			this.m_stateFlags = 33555456;
		}

		// Token: 0x06003A11 RID: 14865 RVA: 0x000D274C File Offset: 0x000D094C
		internal Task(object state, TaskCreationOptions creationOptions, bool promiseStyle)
		{
			if ((creationOptions & ~(TaskCreationOptions.AttachedToParent | TaskCreationOptions.RunContinuationsAsynchronously)) != TaskCreationOptions.None)
			{
				throw new ArgumentOutOfRangeException("creationOptions");
			}
			if ((creationOptions & TaskCreationOptions.AttachedToParent) != TaskCreationOptions.None)
			{
				this.m_parent = Task.InternalCurrent;
			}
			this.TaskConstructorCore(null, state, default(CancellationToken), creationOptions, InternalTaskOptions.PromiseTask, null);
		}

		/// <summary>Initializes a new <see cref="T:System.Threading.Tasks.Task" /> with the specified action.</summary>
		/// <param name="action">The delegate that represents the code to execute in the task.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="action" /> argument is null.</exception>
		// Token: 0x06003A12 RID: 14866 RVA: 0x000D2798 File Offset: 0x000D0998
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task(Action action)
			: this(action, null, null, default(CancellationToken), TaskCreationOptions.None, InternalTaskOptions.None, null)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			this.PossiblyCaptureContext(ref stackCrawlMark);
		}

		/// <summary>Initializes a new <see cref="T:System.Threading.Tasks.Task" /> with the specified action and <see cref="T:System.Threading.CancellationToken" />.</summary>
		/// <param name="action">The delegate that represents the code to execute in the task.</param>
		/// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> that the new  task will observe.</param>
		/// <exception cref="T:System.ObjectDisposedException">The provided <see cref="T:System.Threading.CancellationToken" /> has already been disposed. </exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="action" /> argument is null.</exception>
		// Token: 0x06003A13 RID: 14867 RVA: 0x000D27C4 File Offset: 0x000D09C4
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task(Action action, CancellationToken cancellationToken)
			: this(action, null, null, cancellationToken, TaskCreationOptions.None, InternalTaskOptions.None, null)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			this.PossiblyCaptureContext(ref stackCrawlMark);
		}

		/// <summary>Initializes a new <see cref="T:System.Threading.Tasks.Task" /> with the specified action and creation options.</summary>
		/// <param name="action">The delegate that represents the code to execute in the task.</param>
		/// <param name="creationOptions">The <see cref="T:System.Threading.Tasks.TaskCreationOptions" /> used to customize the task's behavior. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="action" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="creationOptions" /> argument specifies an invalid value for <see cref="T:System.Threading.Tasks.TaskCreationOptions" />.</exception>
		// Token: 0x06003A14 RID: 14868 RVA: 0x000D27E8 File Offset: 0x000D09E8
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task(Action action, TaskCreationOptions creationOptions)
			: this(action, null, Task.InternalCurrentIfAttached(creationOptions), default(CancellationToken), creationOptions, InternalTaskOptions.None, null)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			this.PossiblyCaptureContext(ref stackCrawlMark);
		}

		/// <summary>Initializes a new <see cref="T:System.Threading.Tasks.Task" /> with the specified action and creation options.</summary>
		/// <param name="action">The delegate that represents the code to execute in the task.</param>
		/// <param name="cancellationToken">The <see cref="P:System.Threading.Tasks.TaskFactory.CancellationToken" /> that the new task will observe.</param>
		/// <param name="creationOptions">The <see cref="T:System.Threading.Tasks.TaskCreationOptions" /> used to customize the task's behavior.</param>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Threading.CancellationTokenSource" /> that created <paramref name="cancellationToken" /> has already been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="action" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="creationOptions" /> argument specifies an invalid value for <see cref="T:System.Threading.Tasks.TaskCreationOptions" />.</exception>
		// Token: 0x06003A15 RID: 14869 RVA: 0x000D281C File Offset: 0x000D0A1C
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task(Action action, CancellationToken cancellationToken, TaskCreationOptions creationOptions)
			: this(action, null, Task.InternalCurrentIfAttached(creationOptions), cancellationToken, creationOptions, InternalTaskOptions.None, null)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			this.PossiblyCaptureContext(ref stackCrawlMark);
		}

		/// <summary>Initializes a new <see cref="T:System.Threading.Tasks.Task" /> with the specified action and state.</summary>
		/// <param name="action">The delegate that represents the code to execute in the task.</param>
		/// <param name="state">An object representing data to be used by the action.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="action" /> argument is null.</exception>
		// Token: 0x06003A16 RID: 14870 RVA: 0x000D2848 File Offset: 0x000D0A48
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task(Action<object> action, object state)
			: this(action, state, null, default(CancellationToken), TaskCreationOptions.None, InternalTaskOptions.None, null)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			this.PossiblyCaptureContext(ref stackCrawlMark);
		}

		/// <summary>Initializes a new <see cref="T:System.Threading.Tasks.Task" /> with the specified action, state, and options.</summary>
		/// <param name="action">The delegate that represents the code to execute in the task.</param>
		/// <param name="state">An object representing data to be used by the action.</param>
		/// <param name="cancellationToken">The <see cref="P:System.Threading.Tasks.TaskFactory.CancellationToken" /> that that the new task will observe.</param>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Threading.CancellationTokenSource" /> that created <paramref name="cancellationToken" /> has already been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="action" /> argument is null.</exception>
		// Token: 0x06003A17 RID: 14871 RVA: 0x000D2874 File Offset: 0x000D0A74
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task(Action<object> action, object state, CancellationToken cancellationToken)
			: this(action, state, null, cancellationToken, TaskCreationOptions.None, InternalTaskOptions.None, null)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			this.PossiblyCaptureContext(ref stackCrawlMark);
		}

		/// <summary>Initializes a new <see cref="T:System.Threading.Tasks.Task" /> with the specified action, state, and options.</summary>
		/// <param name="action">The delegate that represents the code to execute in the task.</param>
		/// <param name="state">An object representing data to be used by the action.</param>
		/// <param name="creationOptions">The <see cref="T:System.Threading.Tasks.TaskCreationOptions" /> used to customize the task's behavior.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="action" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="creationOptions" /> argument specifies an invalid value for <see cref="T:System.Threading.Tasks.TaskCreationOptions" />.</exception>
		// Token: 0x06003A18 RID: 14872 RVA: 0x000D2898 File Offset: 0x000D0A98
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task(Action<object> action, object state, TaskCreationOptions creationOptions)
			: this(action, state, Task.InternalCurrentIfAttached(creationOptions), default(CancellationToken), creationOptions, InternalTaskOptions.None, null)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			this.PossiblyCaptureContext(ref stackCrawlMark);
		}

		/// <summary>Initializes a new <see cref="T:System.Threading.Tasks.Task" /> with the specified action, state, and options.</summary>
		/// <param name="action">The delegate that represents the code to execute in the task.</param>
		/// <param name="state">An object representing data to be used by the action.</param>
		/// <param name="cancellationToken">The <see cref="P:System.Threading.Tasks.TaskFactory.CancellationToken" /> that that the new task will observe..</param>
		/// <param name="creationOptions">The <see cref="T:System.Threading.Tasks.TaskCreationOptions" /> used to customize the task's behavior.</param>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Threading.CancellationTokenSource" /> that created <paramref name="cancellationToken" /> has already been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="action" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="creationOptions" /> argument specifies an invalid value for <see cref="T:System.Threading.Tasks.TaskCreationOptions" />.</exception>
		// Token: 0x06003A19 RID: 14873 RVA: 0x000D28CC File Offset: 0x000D0ACC
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task(Action<object> action, object state, CancellationToken cancellationToken, TaskCreationOptions creationOptions)
			: this(action, state, Task.InternalCurrentIfAttached(creationOptions), cancellationToken, creationOptions, InternalTaskOptions.None, null)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			this.PossiblyCaptureContext(ref stackCrawlMark);
		}

		// Token: 0x06003A1A RID: 14874 RVA: 0x000D28F7 File Offset: 0x000D0AF7
		internal Task(Action<object> action, object state, Task parent, CancellationToken cancellationToken, TaskCreationOptions creationOptions, InternalTaskOptions internalOptions, TaskScheduler scheduler, ref StackCrawlMark stackMark)
			: this(action, state, parent, cancellationToken, creationOptions, internalOptions, scheduler)
		{
			this.PossiblyCaptureContext(ref stackMark);
		}

		// Token: 0x06003A1B RID: 14875 RVA: 0x000D2912 File Offset: 0x000D0B12
		internal Task(Delegate action, object state, Task parent, CancellationToken cancellationToken, TaskCreationOptions creationOptions, InternalTaskOptions internalOptions, TaskScheduler scheduler)
		{
			if (action == null)
			{
				throw new ArgumentNullException("action");
			}
			if ((creationOptions & TaskCreationOptions.AttachedToParent) != TaskCreationOptions.None || (internalOptions & InternalTaskOptions.SelfReplicating) != InternalTaskOptions.None)
			{
				this.m_parent = parent;
			}
			this.TaskConstructorCore(action, state, cancellationToken, creationOptions, internalOptions, scheduler);
		}

		// Token: 0x06003A1C RID: 14876 RVA: 0x000D2950 File Offset: 0x000D0B50
		internal void TaskConstructorCore(object action, object state, CancellationToken cancellationToken, TaskCreationOptions creationOptions, InternalTaskOptions internalOptions, TaskScheduler scheduler)
		{
			this.m_action = action;
			this.m_stateObject = state;
			this.m_taskScheduler = scheduler;
			if ((creationOptions & ~(TaskCreationOptions.PreferFairness | TaskCreationOptions.LongRunning | TaskCreationOptions.AttachedToParent | TaskCreationOptions.DenyChildAttach | TaskCreationOptions.HideScheduler | TaskCreationOptions.RunContinuationsAsynchronously)) != TaskCreationOptions.None)
			{
				throw new ArgumentOutOfRangeException("creationOptions");
			}
			if ((creationOptions & TaskCreationOptions.LongRunning) != TaskCreationOptions.None && (internalOptions & InternalTaskOptions.SelfReplicating) != InternalTaskOptions.None)
			{
				throw new InvalidOperationException(Environment.GetResourceString("(Internal)An attempt was made to create a LongRunning SelfReplicating task."));
			}
			int num = (int)(creationOptions | (TaskCreationOptions)internalOptions);
			if (this.m_action == null || (internalOptions & InternalTaskOptions.ContinuationTask) != InternalTaskOptions.None)
			{
				num |= 33554432;
			}
			this.m_stateFlags = num;
			if (this.m_parent != null && (creationOptions & TaskCreationOptions.AttachedToParent) != TaskCreationOptions.None && (this.m_parent.CreationOptions & TaskCreationOptions.DenyChildAttach) == TaskCreationOptions.None)
			{
				this.m_parent.AddNewChild();
			}
			if (cancellationToken.CanBeCanceled)
			{
				this.AssignCancellationToken(cancellationToken, null, null);
			}
		}

		// Token: 0x06003A1D RID: 14877 RVA: 0x000D2A08 File Offset: 0x000D0C08
		private void AssignCancellationToken(CancellationToken cancellationToken, Task antecedent, TaskContinuation continuation)
		{
			Task.ContingentProperties contingentProperties = this.EnsureContingentPropertiesInitialized(false);
			contingentProperties.m_cancellationToken = cancellationToken;
			try
			{
				if (AppContextSwitches.ThrowExceptionIfDisposedCancellationTokenSource)
				{
					cancellationToken.ThrowIfSourceDisposed();
				}
				if ((this.Options & (TaskCreationOptions)13312) == TaskCreationOptions.None)
				{
					if (cancellationToken.IsCancellationRequested)
					{
						this.InternalCancel(false);
					}
					else
					{
						CancellationTokenRegistration cancellationTokenRegistration;
						if (antecedent == null)
						{
							cancellationTokenRegistration = cancellationToken.InternalRegisterWithoutEC(Task.s_taskCancelCallback, this);
						}
						else
						{
							cancellationTokenRegistration = cancellationToken.InternalRegisterWithoutEC(Task.s_taskCancelCallback, new Tuple<Task, Task, TaskContinuation>(this, antecedent, continuation));
						}
						contingentProperties.m_cancellationRegistration = new Shared<CancellationTokenRegistration>(cancellationTokenRegistration);
					}
				}
			}
			catch
			{
				if (this.m_parent != null && (this.Options & TaskCreationOptions.AttachedToParent) != TaskCreationOptions.None && (this.m_parent.Options & TaskCreationOptions.DenyChildAttach) == TaskCreationOptions.None)
				{
					this.m_parent.DisregardChild();
				}
				throw;
			}
		}

		// Token: 0x06003A1E RID: 14878 RVA: 0x000D2AC8 File Offset: 0x000D0CC8
		private static void TaskCancelCallback(object o)
		{
			Task task = o as Task;
			if (task == null)
			{
				Tuple<Task, Task, TaskContinuation> tuple = o as Tuple<Task, Task, TaskContinuation>;
				if (tuple != null)
				{
					task = tuple.Item1;
					Task item = tuple.Item2;
					TaskContinuation item2 = tuple.Item3;
					item.RemoveContinuation(item2);
				}
			}
			task.InternalCancel(false);
		}

		// Token: 0x17000989 RID: 2441
		// (get) Token: 0x06003A1F RID: 14879 RVA: 0x000D2B10 File Offset: 0x000D0D10
		private string DebuggerDisplayMethodDescription
		{
			get
			{
				Delegate @delegate = (Delegate)this.m_action;
				if (@delegate == null)
				{
					return "{null}";
				}
				return @delegate.Method.ToString();
			}
		}

		// Token: 0x06003A20 RID: 14880 RVA: 0x000D2B3D File Offset: 0x000D0D3D
		[SecuritySafeCritical]
		internal void PossiblyCaptureContext(ref StackCrawlMark stackMark)
		{
			this.CapturedContext = ExecutionContext.Capture(ref stackMark, ExecutionContext.CaptureOptions.IgnoreSyncCtx | ExecutionContext.CaptureOptions.OptimizeDefaultCase);
		}

		// Token: 0x1700098A RID: 2442
		// (get) Token: 0x06003A21 RID: 14881 RVA: 0x000D2B4C File Offset: 0x000D0D4C
		internal TaskCreationOptions Options
		{
			get
			{
				return Task.OptionsMethod(this.m_stateFlags);
			}
		}

		// Token: 0x06003A22 RID: 14882 RVA: 0x000D2B5B File Offset: 0x000D0D5B
		internal static TaskCreationOptions OptionsMethod(int flags)
		{
			return (TaskCreationOptions)(flags & 65535);
		}

		// Token: 0x06003A23 RID: 14883 RVA: 0x000D2B64 File Offset: 0x000D0D64
		internal bool AtomicStateUpdate(int newBits, int illegalBits)
		{
			SpinWait spinWait = default(SpinWait);
			for (;;)
			{
				int stateFlags = this.m_stateFlags;
				if ((stateFlags & illegalBits) != 0)
				{
					break;
				}
				if (Interlocked.CompareExchange(ref this.m_stateFlags, stateFlags | newBits, stateFlags) == stateFlags)
				{
					return true;
				}
				spinWait.SpinOnce();
			}
			return false;
		}

		// Token: 0x06003A24 RID: 14884 RVA: 0x000D2BA8 File Offset: 0x000D0DA8
		internal bool AtomicStateUpdate(int newBits, int illegalBits, ref int oldFlags)
		{
			SpinWait spinWait = default(SpinWait);
			for (;;)
			{
				oldFlags = this.m_stateFlags;
				if ((oldFlags & illegalBits) != 0)
				{
					break;
				}
				if (Interlocked.CompareExchange(ref this.m_stateFlags, oldFlags | newBits, oldFlags) == oldFlags)
				{
					return true;
				}
				spinWait.SpinOnce();
			}
			return false;
		}

		// Token: 0x06003A25 RID: 14885 RVA: 0x000D2BF0 File Offset: 0x000D0DF0
		internal void SetNotificationForWaitCompletion(bool enabled)
		{
			if (enabled)
			{
				this.AtomicStateUpdate(268435456, 90177536);
				return;
			}
			SpinWait spinWait = default(SpinWait);
			for (;;)
			{
				int stateFlags = this.m_stateFlags;
				int num = stateFlags & -268435457;
				if (Interlocked.CompareExchange(ref this.m_stateFlags, num, stateFlags) == stateFlags)
				{
					break;
				}
				spinWait.SpinOnce();
			}
		}

		// Token: 0x06003A26 RID: 14886 RVA: 0x000D2C44 File Offset: 0x000D0E44
		internal bool NotifyDebuggerOfWaitCompletionIfNecessary()
		{
			if (this.IsWaitNotificationEnabled && this.ShouldNotifyDebuggerOfWaitCompletion)
			{
				this.NotifyDebuggerOfWaitCompletion();
				return true;
			}
			return false;
		}

		// Token: 0x06003A27 RID: 14887 RVA: 0x000D2C60 File Offset: 0x000D0E60
		internal static bool AnyTaskRequiresNotifyDebuggerOfWaitCompletion(Task[] tasks)
		{
			foreach (Task task in tasks)
			{
				if (task != null && task.IsWaitNotificationEnabled && task.ShouldNotifyDebuggerOfWaitCompletion)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x1700098B RID: 2443
		// (get) Token: 0x06003A28 RID: 14888 RVA: 0x000D2C97 File Offset: 0x000D0E97
		internal bool IsWaitNotificationEnabledOrNotRanToCompletion
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return (this.m_stateFlags & 285212672) != 16777216;
			}
		}

		// Token: 0x1700098C RID: 2444
		// (get) Token: 0x06003A29 RID: 14889 RVA: 0x000D2CB1 File Offset: 0x000D0EB1
		internal virtual bool ShouldNotifyDebuggerOfWaitCompletion
		{
			get
			{
				return this.IsWaitNotificationEnabled;
			}
		}

		// Token: 0x1700098D RID: 2445
		// (get) Token: 0x06003A2A RID: 14890 RVA: 0x000D2CB9 File Offset: 0x000D0EB9
		internal bool IsWaitNotificationEnabled
		{
			get
			{
				return (this.m_stateFlags & 268435456) != 0;
			}
		}

		// Token: 0x06003A2B RID: 14891 RVA: 0x000D2CCC File Offset: 0x000D0ECC
		[MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
		private void NotifyDebuggerOfWaitCompletion()
		{
			this.SetNotificationForWaitCompletion(false);
		}

		// Token: 0x06003A2C RID: 14892 RVA: 0x000D2CD5 File Offset: 0x000D0ED5
		internal bool MarkStarted()
		{
			return this.AtomicStateUpdate(65536, 4259840);
		}

		// Token: 0x06003A2D RID: 14893 RVA: 0x00015ED5 File Offset: 0x000140D5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal bool FireTaskScheduledIfNeeded(TaskScheduler ts)
		{
			return false;
		}

		// Token: 0x06003A2E RID: 14894 RVA: 0x000D2CE8 File Offset: 0x000D0EE8
		internal void AddNewChild()
		{
			Task.ContingentProperties contingentProperties = this.EnsureContingentPropertiesInitialized(true);
			if (contingentProperties.m_completionCountdown == 1 && !this.IsSelfReplicatingRoot)
			{
				contingentProperties.m_completionCountdown++;
				return;
			}
			Interlocked.Increment(ref contingentProperties.m_completionCountdown);
		}

		// Token: 0x06003A2F RID: 14895 RVA: 0x000D2D2F File Offset: 0x000D0F2F
		internal void DisregardChild()
		{
			Interlocked.Decrement(ref this.EnsureContingentPropertiesInitialized(true).m_completionCountdown);
		}

		/// <summary>Starts the <see cref="T:System.Threading.Tasks.Task" />, scheduling it for execution to the current <see cref="T:System.Threading.Tasks.TaskScheduler" />.</summary>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Threading.Tasks.Task" /> instance has been disposed.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Threading.Tasks.Task" /> is not in a valid state to be started. It may have already been started, executed, or canceled, or it may have been created in a manner that doesn't support direct scheduling.</exception>
		// Token: 0x06003A30 RID: 14896 RVA: 0x000D2D43 File Offset: 0x000D0F43
		public void Start()
		{
			this.Start(TaskScheduler.Current);
		}

		/// <summary>Starts the <see cref="T:System.Threading.Tasks.Task" />, scheduling it for execution to the specified <see cref="T:System.Threading.Tasks.TaskScheduler" />.</summary>
		/// <param name="scheduler">The <see cref="T:System.Threading.Tasks.TaskScheduler" /> with which to associate and execute this task.</param>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Threading.Tasks.Task" /> instance has been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="scheduler" /> argument is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Threading.Tasks.Task" /> is not in a valid state to be started. It may have already been started, executed, or canceled, or it may have been created in a manner that doesn't support direct scheduling.</exception>
		// Token: 0x06003A31 RID: 14897 RVA: 0x000D2D50 File Offset: 0x000D0F50
		public void Start(TaskScheduler scheduler)
		{
			int stateFlags = this.m_stateFlags;
			if (Task.IsCompletedMethod(stateFlags))
			{
				throw new InvalidOperationException(Environment.GetResourceString("Start may not be called on a task that has completed."));
			}
			if (scheduler == null)
			{
				throw new ArgumentNullException("scheduler");
			}
			TaskCreationOptions taskCreationOptions = Task.OptionsMethod(stateFlags);
			if ((taskCreationOptions & (TaskCreationOptions)1024) != TaskCreationOptions.None)
			{
				throw new InvalidOperationException(Environment.GetResourceString("Start may not be called on a promise-style task."));
			}
			if ((taskCreationOptions & (TaskCreationOptions)512) != TaskCreationOptions.None)
			{
				throw new InvalidOperationException(Environment.GetResourceString("Start may not be called on a continuation task."));
			}
			if (Interlocked.CompareExchange<TaskScheduler>(ref this.m_taskScheduler, scheduler, null) != null)
			{
				throw new InvalidOperationException(Environment.GetResourceString("Start may not be called on a task that was already started."));
			}
			this.ScheduleAndStart(true);
		}

		/// <summary>Runs the <see cref="T:System.Threading.Tasks.Task" /> synchronously on the current <see cref="T:System.Threading.Tasks.TaskScheduler" />.</summary>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Threading.Tasks.Task" /> instance has been disposed.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Threading.Tasks.Task" /> is not in a valid state to be started. It may have already been started, executed, or canceled, or it may have been created in a manner that doesn't support direct scheduling.</exception>
		// Token: 0x06003A32 RID: 14898 RVA: 0x000D2DE7 File Offset: 0x000D0FE7
		public void RunSynchronously()
		{
			this.InternalRunSynchronously(TaskScheduler.Current, true);
		}

		/// <summary>Runs the <see cref="T:System.Threading.Tasks.Task" /> synchronously on the <see cref="T:System.Threading.Tasks.TaskScheduler" /> provided.</summary>
		/// <param name="scheduler">The scheduler on which to attempt to run this task inline.</param>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Threading.Tasks.Task" /> instance has been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="scheduler" /> parameter is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Threading.Tasks.Task" /> is not in a valid state to be started. It may have already been started, executed, or canceled, or it may have been created in a manner that doesn't support direct scheduling.</exception>
		// Token: 0x06003A33 RID: 14899 RVA: 0x000D2DF5 File Offset: 0x000D0FF5
		public void RunSynchronously(TaskScheduler scheduler)
		{
			if (scheduler == null)
			{
				throw new ArgumentNullException("scheduler");
			}
			this.InternalRunSynchronously(scheduler, true);
		}

		// Token: 0x06003A34 RID: 14900 RVA: 0x000D2E10 File Offset: 0x000D1010
		[SecuritySafeCritical]
		internal void InternalRunSynchronously(TaskScheduler scheduler, bool waitForCompletion)
		{
			int stateFlags = this.m_stateFlags;
			TaskCreationOptions taskCreationOptions = Task.OptionsMethod(stateFlags);
			if ((taskCreationOptions & (TaskCreationOptions)512) != TaskCreationOptions.None)
			{
				throw new InvalidOperationException(Environment.GetResourceString("RunSynchronously may not be called on a continuation task."));
			}
			if ((taskCreationOptions & (TaskCreationOptions)1024) != TaskCreationOptions.None)
			{
				throw new InvalidOperationException(Environment.GetResourceString("RunSynchronously may not be called on a task not bound to a delegate, such as the task returned from an asynchronous method."));
			}
			if (Task.IsCompletedMethod(stateFlags))
			{
				throw new InvalidOperationException(Environment.GetResourceString("RunSynchronously may not be called on a task that has already completed."));
			}
			if (Interlocked.CompareExchange<TaskScheduler>(ref this.m_taskScheduler, scheduler, null) != null)
			{
				throw new InvalidOperationException(Environment.GetResourceString("RunSynchronously may not be called on a task that was already started."));
			}
			if (this.MarkStarted())
			{
				bool flag = false;
				try
				{
					if (!scheduler.TryRunInline(this, false))
					{
						scheduler.InternalQueueTask(this);
						flag = true;
					}
					if (waitForCompletion && !this.IsCompleted)
					{
						this.SpinThenBlockingWait(-1, default(CancellationToken));
					}
					return;
				}
				catch (Exception ex)
				{
					if (!flag && !(ex is ThreadAbortException))
					{
						TaskSchedulerException ex2 = new TaskSchedulerException(ex);
						this.AddException(ex2);
						this.Finish(false);
						this.m_contingentProperties.m_exceptionsHolder.MarkAsHandled(false);
						throw ex2;
					}
					throw;
				}
			}
			throw new InvalidOperationException(Environment.GetResourceString("RunSynchronously may not be called on a task that has already completed."));
		}

		// Token: 0x06003A35 RID: 14901 RVA: 0x000D2F28 File Offset: 0x000D1128
		internal static Task InternalStartNew(Task creatingTask, Delegate action, object state, CancellationToken cancellationToken, TaskScheduler scheduler, TaskCreationOptions options, InternalTaskOptions internalOptions, ref StackCrawlMark stackMark)
		{
			if (scheduler == null)
			{
				throw new ArgumentNullException("scheduler");
			}
			Task task = new Task(action, state, creatingTask, cancellationToken, options, internalOptions | InternalTaskOptions.QueuedByRuntime, scheduler);
			task.PossiblyCaptureContext(ref stackMark);
			task.ScheduleAndStart(false);
			return task;
		}

		// Token: 0x06003A36 RID: 14902 RVA: 0x000D2F60 File Offset: 0x000D1160
		internal static int NewId()
		{
			int num;
			do
			{
				num = Interlocked.Increment(ref Task.s_taskIdCounter);
			}
			while (num == 0);
			return num;
		}

		/// <summary>Gets a unique ID for this <see cref="T:System.Threading.Tasks.Task" /> instance.</summary>
		/// <returns>An integer that was assigned by the system to this task instance.</returns>
		// Token: 0x1700098E RID: 2446
		// (get) Token: 0x06003A37 RID: 14903 RVA: 0x000D2F80 File Offset: 0x000D1180
		public int Id
		{
			get
			{
				if (this.m_taskId == 0)
				{
					int num = Task.NewId();
					Interlocked.CompareExchange(ref this.m_taskId, num, 0);
				}
				return this.m_taskId;
			}
		}

		/// <summary>Returns the unique ID of the currently executing <see cref="T:System.Threading.Tasks.Task" />.</summary>
		/// <returns>An integer that was assigned by the system to the currently-executing task.</returns>
		// Token: 0x1700098F RID: 2447
		// (get) Token: 0x06003A38 RID: 14904 RVA: 0x000D2FB4 File Offset: 0x000D11B4
		public static int? CurrentId
		{
			get
			{
				Task internalCurrent = Task.InternalCurrent;
				if (internalCurrent != null)
				{
					return new int?(internalCurrent.Id);
				}
				return null;
			}
		}

		// Token: 0x17000990 RID: 2448
		// (get) Token: 0x06003A39 RID: 14905 RVA: 0x000D2FDF File Offset: 0x000D11DF
		internal static Task InternalCurrent
		{
			get
			{
				return Task.t_currentTask;
			}
		}

		// Token: 0x06003A3A RID: 14906 RVA: 0x000D2FE6 File Offset: 0x000D11E6
		internal static Task InternalCurrentIfAttached(TaskCreationOptions creationOptions)
		{
			if ((creationOptions & TaskCreationOptions.AttachedToParent) == TaskCreationOptions.None)
			{
				return null;
			}
			return Task.InternalCurrent;
		}

		// Token: 0x17000991 RID: 2449
		// (get) Token: 0x06003A3B RID: 14907 RVA: 0x000D2FF4 File Offset: 0x000D11F4
		internal static StackGuard CurrentStackGuard
		{
			get
			{
				StackGuard stackGuard = Task.t_stackGuard;
				if (stackGuard == null)
				{
					stackGuard = (Task.t_stackGuard = new StackGuard());
				}
				return stackGuard;
			}
		}

		/// <summary>Gets the <see cref="T:System.AggregateException" /> that caused the <see cref="T:System.Threading.Tasks.Task" /> to end prematurely. If the <see cref="T:System.Threading.Tasks.Task" /> completed successfully or has not yet thrown any exceptions, this will return null.</summary>
		/// <returns>The <see cref="T:System.AggregateException" /> that caused the <see cref="T:System.Threading.Tasks.Task" /> to end prematurely.</returns>
		// Token: 0x17000992 RID: 2450
		// (get) Token: 0x06003A3C RID: 14908 RVA: 0x000D3018 File Offset: 0x000D1218
		public AggregateException Exception
		{
			get
			{
				AggregateException ex = null;
				if (this.IsFaulted)
				{
					ex = this.GetExceptions(false);
				}
				return ex;
			}
		}

		/// <summary>Gets the <see cref="T:System.Threading.Tasks.TaskStatus" /> of this task.</summary>
		/// <returns>The current <see cref="T:System.Threading.Tasks.TaskStatus" /> of this task instance.</returns>
		// Token: 0x17000993 RID: 2451
		// (get) Token: 0x06003A3D RID: 14909 RVA: 0x000D3038 File Offset: 0x000D1238
		public TaskStatus Status
		{
			get
			{
				int stateFlags = this.m_stateFlags;
				TaskStatus taskStatus;
				if ((stateFlags & 2097152) != 0)
				{
					taskStatus = TaskStatus.Faulted;
				}
				else if ((stateFlags & 4194304) != 0)
				{
					taskStatus = TaskStatus.Canceled;
				}
				else if ((stateFlags & 16777216) != 0)
				{
					taskStatus = TaskStatus.RanToCompletion;
				}
				else if ((stateFlags & 8388608) != 0)
				{
					taskStatus = TaskStatus.WaitingForChildrenToComplete;
				}
				else if ((stateFlags & 131072) != 0)
				{
					taskStatus = TaskStatus.Running;
				}
				else if ((stateFlags & 65536) != 0)
				{
					taskStatus = TaskStatus.WaitingToRun;
				}
				else if ((stateFlags & 33554432) != 0)
				{
					taskStatus = TaskStatus.WaitingForActivation;
				}
				else
				{
					taskStatus = TaskStatus.Created;
				}
				return taskStatus;
			}
		}

		/// <summary>Gets whether this <see cref="T:System.Threading.Tasks.Task" /> instance has completed execution due to being canceled.</summary>
		/// <returns>true if the task has completed due to being canceled; otherwise false.</returns>
		// Token: 0x17000994 RID: 2452
		// (get) Token: 0x06003A3E RID: 14910 RVA: 0x000D30AC File Offset: 0x000D12AC
		public bool IsCanceled
		{
			get
			{
				return (this.m_stateFlags & 6291456) == 4194304;
			}
		}

		// Token: 0x17000995 RID: 2453
		// (get) Token: 0x06003A3F RID: 14911 RVA: 0x000D30C4 File Offset: 0x000D12C4
		internal bool IsCancellationRequested
		{
			get
			{
				Task.ContingentProperties contingentProperties = this.m_contingentProperties;
				return contingentProperties != null && (contingentProperties.m_internalCancellationRequested == 1 || contingentProperties.m_cancellationToken.IsCancellationRequested);
			}
		}

		// Token: 0x06003A40 RID: 14912 RVA: 0x000D30F8 File Offset: 0x000D12F8
		internal Task.ContingentProperties EnsureContingentPropertiesInitialized(bool needsProtection)
		{
			Task.ContingentProperties contingentProperties = this.m_contingentProperties;
			if (contingentProperties == null)
			{
				return this.EnsureContingentPropertiesInitializedCore(needsProtection);
			}
			return contingentProperties;
		}

		// Token: 0x06003A41 RID: 14913 RVA: 0x000D311C File Offset: 0x000D131C
		private Task.ContingentProperties EnsureContingentPropertiesInitializedCore(bool needsProtection)
		{
			if (needsProtection)
			{
				return LazyInitializer.EnsureInitialized<Task.ContingentProperties>(ref this.m_contingentProperties, Task.s_createContingentProperties);
			}
			return this.m_contingentProperties = new Task.ContingentProperties();
		}

		// Token: 0x17000996 RID: 2454
		// (get) Token: 0x06003A42 RID: 14914 RVA: 0x000D3150 File Offset: 0x000D1350
		internal CancellationToken CancellationToken
		{
			get
			{
				Task.ContingentProperties contingentProperties = this.m_contingentProperties;
				if (contingentProperties != null)
				{
					return contingentProperties.m_cancellationToken;
				}
				return default(CancellationToken);
			}
		}

		// Token: 0x17000997 RID: 2455
		// (get) Token: 0x06003A43 RID: 14915 RVA: 0x000D3179 File Offset: 0x000D1379
		internal bool IsCancellationAcknowledged
		{
			get
			{
				return (this.m_stateFlags & 1048576) != 0;
			}
		}

		/// <summary>Gets whether this <see cref="T:System.Threading.Tasks.Task" /> has completed.</summary>
		/// <returns>true if the task has completed; otherwise false.</returns>
		// Token: 0x17000998 RID: 2456
		// (get) Token: 0x06003A44 RID: 14916 RVA: 0x000D318C File Offset: 0x000D138C
		public bool IsCompleted
		{
			get
			{
				return Task.IsCompletedMethod(this.m_stateFlags);
			}
		}

		// Token: 0x06003A45 RID: 14917 RVA: 0x000D319B File Offset: 0x000D139B
		private static bool IsCompletedMethod(int flags)
		{
			return (flags & 23068672) != 0;
		}

		// Token: 0x17000999 RID: 2457
		// (get) Token: 0x06003A46 RID: 14918 RVA: 0x000D31A7 File Offset: 0x000D13A7
		public bool IsCompletedSuccessfully
		{
			get
			{
				return (this.m_stateFlags & 23068672) == 16777216;
			}
		}

		// Token: 0x1700099A RID: 2458
		// (get) Token: 0x06003A47 RID: 14919 RVA: 0x000D31A7 File Offset: 0x000D13A7
		internal bool IsRanToCompletion
		{
			get
			{
				return (this.m_stateFlags & 23068672) == 16777216;
			}
		}

		/// <summary>Gets the <see cref="T:System.Threading.Tasks.TaskCreationOptions" /> used to create this task.</summary>
		/// <returns>The <see cref="T:System.Threading.Tasks.TaskCreationOptions" /> used to create this task.</returns>
		// Token: 0x1700099B RID: 2459
		// (get) Token: 0x06003A48 RID: 14920 RVA: 0x000D31BE File Offset: 0x000D13BE
		public TaskCreationOptions CreationOptions
		{
			get
			{
				return this.Options & (TaskCreationOptions)(-65281);
			}
		}

		/// <summary>Gets a <see cref="T:System.Threading.WaitHandle" /> that can be used to wait for the task to complete.</summary>
		/// <returns>A <see cref="T:System.Threading.WaitHandle" /> that can be used to wait for the task to complete.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Threading.Tasks.Task" /> has been disposed.</exception>
		// Token: 0x1700099C RID: 2460
		// (get) Token: 0x06003A49 RID: 14921 RVA: 0x000D31CC File Offset: 0x000D13CC
		WaitHandle IAsyncResult.AsyncWaitHandle
		{
			get
			{
				if ((this.m_stateFlags & 262144) != 0)
				{
					throw new ObjectDisposedException(null, Environment.GetResourceString("The task has been disposed."));
				}
				return this.CompletedEvent.WaitHandle;
			}
		}

		/// <summary>Gets the state object supplied when the <see cref="T:System.Threading.Tasks.Task" /> was created, or null if none was supplied.</summary>
		/// <returns>An <see cref="T:System.Object" /> that represents the state data that was passed in to the task when it was created.</returns>
		// Token: 0x1700099D RID: 2461
		// (get) Token: 0x06003A4A RID: 14922 RVA: 0x000D31FD File Offset: 0x000D13FD
		public object AsyncState
		{
			get
			{
				return this.m_stateObject;
			}
		}

		/// <summary>Gets an indication of whether the operation completed synchronously.</summary>
		/// <returns>true if the operation completed synchronously; otherwise, false.</returns>
		// Token: 0x1700099E RID: 2462
		// (get) Token: 0x06003A4B RID: 14923 RVA: 0x00015ED5 File Offset: 0x000140D5
		bool IAsyncResult.CompletedSynchronously
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700099F RID: 2463
		// (get) Token: 0x06003A4C RID: 14924 RVA: 0x000D3205 File Offset: 0x000D1405
		internal TaskScheduler ExecutingTaskScheduler
		{
			get
			{
				return this.m_taskScheduler;
			}
		}

		/// <summary>Provides access to factory methods for creating <see cref="T:System.Threading.Tasks.Task" /> and <see cref="T:System.Threading.Tasks.Task`1" /> instances.</summary>
		/// <returns>The default <see cref="T:System.Threading.Tasks.TaskFactory" /> for the current task.</returns>
		// Token: 0x170009A0 RID: 2464
		// (get) Token: 0x06003A4D RID: 14925 RVA: 0x000D320D File Offset: 0x000D140D
		public static TaskFactory Factory
		{
			get
			{
				return Task.s_factory;
			}
		}

		// Token: 0x170009A1 RID: 2465
		// (get) Token: 0x06003A4E RID: 14926 RVA: 0x000D3214 File Offset: 0x000D1414
		public static Task CompletedTask
		{
			get
			{
				Task task = Task.s_completedTask;
				if (task == null)
				{
					task = (Task.s_completedTask = new Task(false, (TaskCreationOptions)16384, default(CancellationToken)));
				}
				return task;
			}
		}

		// Token: 0x170009A2 RID: 2466
		// (get) Token: 0x06003A4F RID: 14927 RVA: 0x000D3248 File Offset: 0x000D1448
		internal ManualResetEventSlim CompletedEvent
		{
			get
			{
				Task.ContingentProperties contingentProperties = this.EnsureContingentPropertiesInitialized(true);
				if (contingentProperties.m_completionEvent == null)
				{
					bool isCompleted = this.IsCompleted;
					ManualResetEventSlim manualResetEventSlim = new ManualResetEventSlim(isCompleted);
					if (Interlocked.CompareExchange<ManualResetEventSlim>(ref contingentProperties.m_completionEvent, manualResetEventSlim, null) != null)
					{
						manualResetEventSlim.Dispose();
					}
					else if (!isCompleted && this.IsCompleted)
					{
						manualResetEventSlim.Set();
					}
				}
				return contingentProperties.m_completionEvent;
			}
		}

		// Token: 0x170009A3 RID: 2467
		// (get) Token: 0x06003A50 RID: 14928 RVA: 0x000D32A5 File Offset: 0x000D14A5
		internal bool IsSelfReplicatingRoot
		{
			get
			{
				return (this.Options & (TaskCreationOptions)2304) == (TaskCreationOptions)2048;
			}
		}

		// Token: 0x170009A4 RID: 2468
		// (get) Token: 0x06003A51 RID: 14929 RVA: 0x000D32BA File Offset: 0x000D14BA
		internal bool IsChildReplica
		{
			get
			{
				return (this.Options & (TaskCreationOptions)256) > TaskCreationOptions.None;
			}
		}

		// Token: 0x170009A5 RID: 2469
		// (get) Token: 0x06003A52 RID: 14930 RVA: 0x000D32CC File Offset: 0x000D14CC
		internal int ActiveChildCount
		{
			get
			{
				Task.ContingentProperties contingentProperties = this.m_contingentProperties;
				if (contingentProperties == null)
				{
					return 0;
				}
				return contingentProperties.m_completionCountdown - 1;
			}
		}

		// Token: 0x170009A6 RID: 2470
		// (get) Token: 0x06003A53 RID: 14931 RVA: 0x000D32F4 File Offset: 0x000D14F4
		internal bool ExceptionRecorded
		{
			get
			{
				Task.ContingentProperties contingentProperties = this.m_contingentProperties;
				return contingentProperties != null && contingentProperties.m_exceptionsHolder != null && contingentProperties.m_exceptionsHolder.ContainsFaultList;
			}
		}

		/// <summary>Gets whether the <see cref="T:System.Threading.Tasks.Task" /> completed due to an unhandled exception.</summary>
		/// <returns>true if the task has thrown an unhandled exception; otherwise false.</returns>
		// Token: 0x170009A7 RID: 2471
		// (get) Token: 0x06003A54 RID: 14932 RVA: 0x000D3326 File Offset: 0x000D1526
		public bool IsFaulted
		{
			get
			{
				return (this.m_stateFlags & 2097152) != 0;
			}
		}

		// Token: 0x170009A8 RID: 2472
		// (get) Token: 0x06003A55 RID: 14933 RVA: 0x000D333C File Offset: 0x000D153C
		// (set) Token: 0x06003A56 RID: 14934 RVA: 0x000D3380 File Offset: 0x000D1580
		internal ExecutionContext CapturedContext
		{
			get
			{
				if ((this.m_stateFlags & 536870912) == 536870912)
				{
					return null;
				}
				Task.ContingentProperties contingentProperties = this.m_contingentProperties;
				if (contingentProperties != null && contingentProperties.m_capturedContext != null)
				{
					return contingentProperties.m_capturedContext;
				}
				return ExecutionContext.PreAllocatedDefault;
			}
			set
			{
				if (value == null)
				{
					this.m_stateFlags |= 536870912;
					return;
				}
				if (!value.IsPreAllocatedDefault)
				{
					this.EnsureContingentPropertiesInitialized(false).m_capturedContext = value;
				}
			}
		}

		// Token: 0x06003A57 RID: 14935 RVA: 0x000D33B1 File Offset: 0x000D15B1
		private static ExecutionContext CopyExecutionContext(ExecutionContext capturedContext)
		{
			if (capturedContext == null)
			{
				return null;
			}
			if (capturedContext.IsPreAllocatedDefault)
			{
				return ExecutionContext.PreAllocatedDefault;
			}
			return capturedContext.CreateCopy();
		}

		/// <summary>Releases all resources used by the current instance of the <see cref="T:System.Threading.Tasks.Task" /> class.</summary>
		/// <exception cref="T:System.InvalidOperationException">The exception that is thrown if the <see cref="T:System.Threading.Tasks.Task" /> is not in one of the final states: <see cref="F:System.Threading.Tasks.TaskStatus.RanToCompletion" />, <see cref="F:System.Threading.Tasks.TaskStatus.Faulted" />, or <see cref="F:System.Threading.Tasks.TaskStatus.Canceled" />.</exception>
		// Token: 0x06003A58 RID: 14936 RVA: 0x000D33CC File Offset: 0x000D15CC
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>Disposes the <see cref="T:System.Threading.Tasks.Task" />, releasing all of its unmanaged resources.</summary>
		/// <param name="disposing">A Boolean value that indicates whether this method is being called due to a call to <see cref="M:System.Threading.Tasks.Task.Dispose" />.</param>
		/// <exception cref="T:System.InvalidOperationException">The exception that is thrown if the <see cref="T:System.Threading.Tasks.Task" /> is not in one of the final states: <see cref="F:System.Threading.Tasks.TaskStatus.RanToCompletion" />, <see cref="F:System.Threading.Tasks.TaskStatus.Faulted" />, or <see cref="F:System.Threading.Tasks.TaskStatus.Canceled" />.</exception>
		// Token: 0x06003A59 RID: 14937 RVA: 0x000D33DC File Offset: 0x000D15DC
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				if ((this.Options & (TaskCreationOptions)16384) != TaskCreationOptions.None)
				{
					return;
				}
				if (!this.IsCompleted)
				{
					throw new InvalidOperationException(Environment.GetResourceString("A task may only be disposed if it is in a completion state (RanToCompletion, Faulted or Canceled)."));
				}
				Task.ContingentProperties contingentProperties = this.m_contingentProperties;
				if (contingentProperties != null)
				{
					ManualResetEventSlim completionEvent = contingentProperties.m_completionEvent;
					if (completionEvent != null)
					{
						contingentProperties.m_completionEvent = null;
						if (!completionEvent.IsSet)
						{
							completionEvent.Set();
						}
						completionEvent.Dispose();
					}
				}
			}
			this.m_stateFlags |= 262144;
		}

		// Token: 0x06003A5A RID: 14938 RVA: 0x000D3460 File Offset: 0x000D1660
		[SecuritySafeCritical]
		internal void ScheduleAndStart(bool needsProtection)
		{
			if (needsProtection)
			{
				if (!this.MarkStarted())
				{
					return;
				}
			}
			else
			{
				this.m_stateFlags |= 65536;
			}
			if (Task.s_asyncDebuggingEnabled)
			{
				Task.AddToActiveTasks(this);
			}
			if (AsyncCausalityTracer.LoggingOn && (this.Options & (TaskCreationOptions)512) == TaskCreationOptions.None)
			{
				AsyncCausalityTracer.TraceOperationCreation(CausalityTraceLevel.Required, this.Id, "Task: " + ((Delegate)this.m_action).Method.Name, 0UL);
			}
			try
			{
				this.m_taskScheduler.InternalQueueTask(this);
			}
			catch (ThreadAbortException ex)
			{
				this.AddException(ex);
				this.FinishThreadAbortedTask(true, false);
			}
			catch (Exception ex2)
			{
				TaskSchedulerException ex3 = new TaskSchedulerException(ex2);
				this.AddException(ex3);
				this.Finish(false);
				if ((this.Options & (TaskCreationOptions)512) == TaskCreationOptions.None)
				{
					this.m_contingentProperties.m_exceptionsHolder.MarkAsHandled(false);
				}
				throw ex3;
			}
		}

		// Token: 0x06003A5B RID: 14939 RVA: 0x000D3558 File Offset: 0x000D1758
		internal void AddException(object exceptionObject)
		{
			this.AddException(exceptionObject, false);
		}

		// Token: 0x06003A5C RID: 14940 RVA: 0x000D3564 File Offset: 0x000D1764
		internal void AddException(object exceptionObject, bool representsCancellation)
		{
			Task.ContingentProperties contingentProperties = this.EnsureContingentPropertiesInitialized(true);
			if (contingentProperties.m_exceptionsHolder == null)
			{
				TaskExceptionHolder taskExceptionHolder = new TaskExceptionHolder(this);
				if (Interlocked.CompareExchange<TaskExceptionHolder>(ref contingentProperties.m_exceptionsHolder, taskExceptionHolder, null) != null)
				{
					taskExceptionHolder.MarkAsHandled(false);
				}
			}
			Task.ContingentProperties contingentProperties2 = contingentProperties;
			lock (contingentProperties2)
			{
				contingentProperties.m_exceptionsHolder.Add(exceptionObject, representsCancellation);
			}
		}

		// Token: 0x06003A5D RID: 14941 RVA: 0x000D35D8 File Offset: 0x000D17D8
		private AggregateException GetExceptions(bool includeTaskCanceledExceptions)
		{
			Exception ex = null;
			if (includeTaskCanceledExceptions && this.IsCanceled)
			{
				ex = new TaskCanceledException(this);
			}
			if (this.ExceptionRecorded)
			{
				return this.m_contingentProperties.m_exceptionsHolder.CreateExceptionObject(false, ex);
			}
			if (ex != null)
			{
				return new AggregateException(new Exception[] { ex });
			}
			return null;
		}

		// Token: 0x06003A5E RID: 14942 RVA: 0x000D362C File Offset: 0x000D182C
		internal ReadOnlyCollection<ExceptionDispatchInfo> GetExceptionDispatchInfos()
		{
			if (!this.IsFaulted || !this.ExceptionRecorded)
			{
				return new ReadOnlyCollection<ExceptionDispatchInfo>(new ExceptionDispatchInfo[0]);
			}
			return this.m_contingentProperties.m_exceptionsHolder.GetExceptionDispatchInfos();
		}

		// Token: 0x06003A5F RID: 14943 RVA: 0x000D3664 File Offset: 0x000D1864
		internal ExceptionDispatchInfo GetCancellationExceptionDispatchInfo()
		{
			Task.ContingentProperties contingentProperties = this.m_contingentProperties;
			if (contingentProperties == null)
			{
				return null;
			}
			TaskExceptionHolder exceptionsHolder = contingentProperties.m_exceptionsHolder;
			if (exceptionsHolder == null)
			{
				return null;
			}
			return exceptionsHolder.GetCancellationExceptionDispatchInfo();
		}

		// Token: 0x06003A60 RID: 14944 RVA: 0x000D3694 File Offset: 0x000D1894
		internal void ThrowIfExceptional(bool includeTaskCanceledExceptions)
		{
			Exception exceptions = this.GetExceptions(includeTaskCanceledExceptions);
			if (exceptions != null)
			{
				this.UpdateExceptionObservedStatus();
				throw exceptions;
			}
		}

		// Token: 0x06003A61 RID: 14945 RVA: 0x000D36B4 File Offset: 0x000D18B4
		internal void UpdateExceptionObservedStatus()
		{
			if (this.m_parent != null && (this.Options & TaskCreationOptions.AttachedToParent) != TaskCreationOptions.None && (this.m_parent.CreationOptions & TaskCreationOptions.DenyChildAttach) == TaskCreationOptions.None && Task.InternalCurrent == this.m_parent)
			{
				this.m_stateFlags |= 524288;
			}
		}

		// Token: 0x170009A9 RID: 2473
		// (get) Token: 0x06003A62 RID: 14946 RVA: 0x000D3705 File Offset: 0x000D1905
		internal bool IsExceptionObservedByParent
		{
			get
			{
				return (this.m_stateFlags & 524288) != 0;
			}
		}

		// Token: 0x170009AA RID: 2474
		// (get) Token: 0x06003A63 RID: 14947 RVA: 0x000D3718 File Offset: 0x000D1918
		internal bool IsDelegateInvoked
		{
			get
			{
				return (this.m_stateFlags & 131072) != 0;
			}
		}

		// Token: 0x06003A64 RID: 14948 RVA: 0x000D372C File Offset: 0x000D192C
		internal void Finish(bool bUserDelegateExecuted)
		{
			if (!bUserDelegateExecuted)
			{
				this.FinishStageTwo();
				return;
			}
			Task.ContingentProperties contingentProperties = this.m_contingentProperties;
			if (contingentProperties == null || (contingentProperties.m_completionCountdown == 1 && !this.IsSelfReplicatingRoot) || Interlocked.Decrement(ref contingentProperties.m_completionCountdown) == 0)
			{
				this.FinishStageTwo();
			}
			else
			{
				this.AtomicStateUpdate(8388608, 23068672);
			}
			List<Task> list = ((contingentProperties != null) ? contingentProperties.m_exceptionalChildren : null);
			if (list != null)
			{
				List<Task> list2 = list;
				lock (list2)
				{
					list.RemoveAll(Task.s_IsExceptionObservedByParentPredicate);
				}
			}
		}

		// Token: 0x06003A65 RID: 14949 RVA: 0x000D37D0 File Offset: 0x000D19D0
		internal void FinishStageTwo()
		{
			this.AddExceptionsFromChildren();
			int num;
			if (this.ExceptionRecorded)
			{
				num = 2097152;
				if (AsyncCausalityTracer.LoggingOn)
				{
					AsyncCausalityTracer.TraceOperationCompletion(CausalityTraceLevel.Required, this.Id, AsyncCausalityStatus.Error);
				}
				if (Task.s_asyncDebuggingEnabled)
				{
					Task.RemoveFromActiveTasks(this.Id);
				}
			}
			else if (this.IsCancellationRequested && this.IsCancellationAcknowledged)
			{
				num = 4194304;
				if (AsyncCausalityTracer.LoggingOn)
				{
					AsyncCausalityTracer.TraceOperationCompletion(CausalityTraceLevel.Required, this.Id, AsyncCausalityStatus.Canceled);
				}
				if (Task.s_asyncDebuggingEnabled)
				{
					Task.RemoveFromActiveTasks(this.Id);
				}
			}
			else
			{
				num = 16777216;
				if (AsyncCausalityTracer.LoggingOn)
				{
					AsyncCausalityTracer.TraceOperationCompletion(CausalityTraceLevel.Required, this.Id, AsyncCausalityStatus.Completed);
				}
				if (Task.s_asyncDebuggingEnabled)
				{
					Task.RemoveFromActiveTasks(this.Id);
				}
			}
			Interlocked.Exchange(ref this.m_stateFlags, this.m_stateFlags | num);
			Task.ContingentProperties contingentProperties = this.m_contingentProperties;
			if (contingentProperties != null)
			{
				contingentProperties.SetCompleted();
				contingentProperties.DeregisterCancellationCallback();
			}
			this.FinishStageThree();
		}

		// Token: 0x06003A66 RID: 14950 RVA: 0x000D38B8 File Offset: 0x000D1AB8
		internal void FinishStageThree()
		{
			this.m_action = null;
			if (this.m_parent != null && (this.m_parent.CreationOptions & TaskCreationOptions.DenyChildAttach) == TaskCreationOptions.None && (this.m_stateFlags & 65535 & 4) != 0)
			{
				this.m_parent.ProcessChildCompletion(this);
			}
			this.FinishContinuations();
		}

		// Token: 0x06003A67 RID: 14951 RVA: 0x000D3908 File Offset: 0x000D1B08
		internal void ProcessChildCompletion(Task childTask)
		{
			Task.ContingentProperties contingentProperties = this.m_contingentProperties;
			if (childTask.IsFaulted && !childTask.IsExceptionObservedByParent)
			{
				if (contingentProperties.m_exceptionalChildren == null)
				{
					Interlocked.CompareExchange<List<Task>>(ref contingentProperties.m_exceptionalChildren, new List<Task>(), null);
				}
				List<Task> exceptionalChildren = contingentProperties.m_exceptionalChildren;
				if (exceptionalChildren != null)
				{
					List<Task> list = exceptionalChildren;
					lock (list)
					{
						exceptionalChildren.Add(childTask);
					}
				}
			}
			if (Interlocked.Decrement(ref contingentProperties.m_completionCountdown) == 0)
			{
				this.FinishStageTwo();
			}
		}

		// Token: 0x06003A68 RID: 14952 RVA: 0x000D3998 File Offset: 0x000D1B98
		internal void AddExceptionsFromChildren()
		{
			Task.ContingentProperties contingentProperties = this.m_contingentProperties;
			List<Task> list = ((contingentProperties != null) ? contingentProperties.m_exceptionalChildren : null);
			if (list != null)
			{
				List<Task> list2 = list;
				lock (list2)
				{
					foreach (Task task in list)
					{
						if (task.IsFaulted && !task.IsExceptionObservedByParent)
						{
							TaskExceptionHolder exceptionsHolder = task.m_contingentProperties.m_exceptionsHolder;
							this.AddException(exceptionsHolder.CreateExceptionObject(false, null));
						}
					}
				}
				contingentProperties.m_exceptionalChildren = null;
			}
		}

		// Token: 0x06003A69 RID: 14953 RVA: 0x000D3A5C File Offset: 0x000D1C5C
		internal void FinishThreadAbortedTask(bool bTAEAddedToExceptionHolder, bool delegateRan)
		{
			if (bTAEAddedToExceptionHolder)
			{
				this.m_contingentProperties.m_exceptionsHolder.MarkAsHandled(false);
			}
			if (!this.AtomicStateUpdate(134217728, 157286400))
			{
				return;
			}
			this.Finish(delegateRan);
		}

		// Token: 0x06003A6A RID: 14954 RVA: 0x000D3A90 File Offset: 0x000D1C90
		private void Execute()
		{
			if (this.IsSelfReplicatingRoot)
			{
				Task.ExecuteSelfReplicating(this);
				return;
			}
			try
			{
				this.InnerInvoke();
			}
			catch (ThreadAbortException ex)
			{
				if (!this.IsChildReplica)
				{
					this.HandleException(ex);
					this.FinishThreadAbortedTask(true, true);
				}
			}
			catch (Exception ex2)
			{
				this.HandleException(ex2);
			}
		}

		// Token: 0x06003A6B RID: 14955 RVA: 0x00003B29 File Offset: 0x00001D29
		internal virtual bool ShouldReplicate()
		{
			return true;
		}

		// Token: 0x06003A6C RID: 14956 RVA: 0x000D3AF8 File Offset: 0x000D1CF8
		internal virtual Task CreateReplicaTask(Action<object> taskReplicaDelegate, object stateObject, Task parentTask, TaskScheduler taskScheduler, TaskCreationOptions creationOptionsForReplica, InternalTaskOptions internalOptionsForReplica)
		{
			return new Task(taskReplicaDelegate, stateObject, parentTask, default(CancellationToken), creationOptionsForReplica, internalOptionsForReplica, parentTask.ExecutingTaskScheduler);
		}

		// Token: 0x170009AB RID: 2475
		// (get) Token: 0x06003A6D RID: 14957 RVA: 0x0000A42E File Offset: 0x0000862E
		// (set) Token: 0x06003A6E RID: 14958 RVA: 0x00002194 File Offset: 0x00000394
		internal virtual object SavedStateForNextReplica
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		// Token: 0x170009AC RID: 2476
		// (get) Token: 0x06003A6F RID: 14959 RVA: 0x0000A42E File Offset: 0x0000862E
		// (set) Token: 0x06003A70 RID: 14960 RVA: 0x00002194 File Offset: 0x00000394
		internal virtual object SavedStateFromPreviousReplica
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		// Token: 0x170009AD RID: 2477
		// (get) Token: 0x06003A71 RID: 14961 RVA: 0x0000A42E File Offset: 0x0000862E
		// (set) Token: 0x06003A72 RID: 14962 RVA: 0x00002194 File Offset: 0x00000394
		internal virtual Task HandedOverChildReplica
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		// Token: 0x06003A73 RID: 14963 RVA: 0x000D3B20 File Offset: 0x000D1D20
		private static void ExecuteSelfReplicating(Task root)
		{
			TaskCreationOptions creationOptionsForReplicas = root.CreationOptions | TaskCreationOptions.AttachedToParent;
			InternalTaskOptions internalOptionsForReplicas = InternalTaskOptions.ChildReplica | InternalTaskOptions.SelfReplicating | InternalTaskOptions.QueuedByRuntime;
			bool replicasAreQuitting = false;
			Action<object> taskReplicaDelegate = null;
			taskReplicaDelegate = delegate
			{
				Task internalCurrent = Task.InternalCurrent;
				Task task = internalCurrent.HandedOverChildReplica;
				if (task == null)
				{
					if (!root.ShouldReplicate())
					{
						return;
					}
					if (Volatile.Read(ref replicasAreQuitting))
					{
						return;
					}
					ExecutionContext capturedContext = root.CapturedContext;
					task = root.CreateReplicaTask(taskReplicaDelegate, root.m_stateObject, root, root.ExecutingTaskScheduler, creationOptionsForReplicas, internalOptionsForReplicas);
					task.CapturedContext = Task.CopyExecutionContext(capturedContext);
					task.ScheduleAndStart(false);
				}
				try
				{
					root.InnerInvokeWithArg(internalCurrent);
				}
				catch (Exception ex)
				{
					root.HandleException(ex);
					if (ex is ThreadAbortException)
					{
						internalCurrent.FinishThreadAbortedTask(false, true);
					}
				}
				object savedStateForNextReplica = internalCurrent.SavedStateForNextReplica;
				if (savedStateForNextReplica != null)
				{
					Task task2 = root.CreateReplicaTask(taskReplicaDelegate, root.m_stateObject, root, root.ExecutingTaskScheduler, creationOptionsForReplicas, internalOptionsForReplicas);
					ExecutionContext capturedContext2 = root.CapturedContext;
					task2.CapturedContext = Task.CopyExecutionContext(capturedContext2);
					task2.HandedOverChildReplica = task;
					task2.SavedStateFromPreviousReplica = savedStateForNextReplica;
					task2.ScheduleAndStart(false);
					return;
				}
				replicasAreQuitting = true;
				try
				{
					task.InternalCancel(true);
				}
				catch (Exception ex2)
				{
					root.HandleException(ex2);
				}
			};
			taskReplicaDelegate(null);
		}

		// Token: 0x06003A74 RID: 14964 RVA: 0x000D3B82 File Offset: 0x000D1D82
		[SecurityCritical]
		void IThreadPoolWorkItem.ExecuteWorkItem()
		{
			this.ExecuteEntry(false);
		}

		// Token: 0x06003A75 RID: 14965 RVA: 0x000D3B8C File Offset: 0x000D1D8C
		[SecurityCritical]
		void IThreadPoolWorkItem.MarkAborted(ThreadAbortException tae)
		{
			if (!this.IsCompleted)
			{
				this.HandleException(tae);
				this.FinishThreadAbortedTask(true, false);
			}
		}

		// Token: 0x06003A76 RID: 14966 RVA: 0x000D3BA8 File Offset: 0x000D1DA8
		[SecuritySafeCritical]
		internal bool ExecuteEntry(bool bPreventDoubleExecution)
		{
			if (bPreventDoubleExecution || (this.Options & (TaskCreationOptions)2048) != TaskCreationOptions.None)
			{
				int num = 0;
				if (!this.AtomicStateUpdate(131072, 23199744, ref num) && (num & 4194304) == 0)
				{
					return false;
				}
			}
			else
			{
				this.m_stateFlags |= 131072;
			}
			if (!this.IsCancellationRequested && !this.IsCanceled)
			{
				this.ExecuteWithThreadLocal(ref Task.t_currentTask);
			}
			else if (!this.IsCanceled && (Interlocked.Exchange(ref this.m_stateFlags, this.m_stateFlags | 4194304) & 4194304) == 0)
			{
				this.CancellationCleanupLogic();
			}
			return true;
		}

		// Token: 0x06003A77 RID: 14967 RVA: 0x000D3C4C File Offset: 0x000D1E4C
		[SecurityCritical]
		private void ExecuteWithThreadLocal(ref Task currentTaskSlot)
		{
			Task task = currentTaskSlot;
			try
			{
				currentTaskSlot = this;
				ExecutionContext capturedContext = this.CapturedContext;
				if (capturedContext == null)
				{
					this.Execute();
				}
				else
				{
					if (this.IsSelfReplicatingRoot || this.IsChildReplica)
					{
						this.CapturedContext = Task.CopyExecutionContext(capturedContext);
					}
					ContextCallback contextCallback = Task.s_ecCallback;
					if (contextCallback == null)
					{
						contextCallback = (Task.s_ecCallback = new ContextCallback(Task.ExecutionContextCallback));
					}
					ExecutionContext.Run(capturedContext, contextCallback, this, true);
				}
				if (AsyncCausalityTracer.LoggingOn)
				{
					AsyncCausalityTracer.TraceSynchronousWorkCompletion(CausalityTraceLevel.Required, CausalitySynchronousWork.Execution);
				}
				this.Finish(true);
			}
			finally
			{
				currentTaskSlot = task;
			}
		}

		// Token: 0x06003A78 RID: 14968 RVA: 0x000D3CE0 File Offset: 0x000D1EE0
		[SecurityCritical]
		private static void ExecutionContextCallback(object obj)
		{
			(obj as Task).Execute();
		}

		// Token: 0x06003A79 RID: 14969 RVA: 0x000D3CF0 File Offset: 0x000D1EF0
		internal virtual void InnerInvoke()
		{
			Action action = this.m_action as Action;
			if (action != null)
			{
				action();
				return;
			}
			Action<object> action2 = this.m_action as Action<object>;
			if (action2 != null)
			{
				action2(this.m_stateObject);
				return;
			}
		}

		// Token: 0x06003A7A RID: 14970 RVA: 0x000D3D2F File Offset: 0x000D1F2F
		[MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
		internal void InnerInvokeWithArg(Task childTask)
		{
			this.InnerInvoke();
		}

		// Token: 0x06003A7B RID: 14971 RVA: 0x000D3D38 File Offset: 0x000D1F38
		private void HandleException(Exception unhandledException)
		{
			OperationCanceledException ex = unhandledException as OperationCanceledException;
			if (ex != null && this.IsCancellationRequested && this.m_contingentProperties.m_cancellationToken == ex.CancellationToken)
			{
				this.SetCancellationAcknowledged();
				this.AddException(ex, true);
				return;
			}
			this.AddException(unhandledException);
		}

		/// <summary>Gets an awaiter used to await this <see cref="T:System.Threading.Tasks.Task" />.</summary>
		/// <returns>An awaiter instance.</returns>
		// Token: 0x06003A7C RID: 14972 RVA: 0x000D3D87 File Offset: 0x000D1F87
		public TaskAwaiter GetAwaiter()
		{
			return new TaskAwaiter(this);
		}

		/// <summary>Configures an awaiter used to await this <see cref="T:System.Threading.Tasks.Task" />.</summary>
		/// <returns>An object used to await this task.</returns>
		/// <param name="continueOnCapturedContext">true to attempt to marshal the continuation back to the original context captured; otherwise, false.</param>
		// Token: 0x06003A7D RID: 14973 RVA: 0x000D3D8F File Offset: 0x000D1F8F
		public ConfiguredTaskAwaitable ConfigureAwait(bool continueOnCapturedContext)
		{
			return new ConfiguredTaskAwaitable(this, continueOnCapturedContext);
		}

		// Token: 0x06003A7E RID: 14974 RVA: 0x000D3D98 File Offset: 0x000D1F98
		[SecurityCritical]
		internal void SetContinuationForAwait(Action continuationAction, bool continueOnCapturedContext, bool flowExecutionContext, ref StackCrawlMark stackMark)
		{
			TaskContinuation taskContinuation = null;
			if (continueOnCapturedContext)
			{
				SynchronizationContext currentNoFlow = SynchronizationContext.CurrentNoFlow;
				if (currentNoFlow != null && currentNoFlow.GetType() != typeof(SynchronizationContext))
				{
					taskContinuation = new SynchronizationContextAwaitTaskContinuation(currentNoFlow, continuationAction, flowExecutionContext, ref stackMark);
				}
				else
				{
					TaskScheduler internalCurrent = TaskScheduler.InternalCurrent;
					if (internalCurrent != null && internalCurrent != TaskScheduler.Default)
					{
						taskContinuation = new TaskSchedulerAwaitTaskContinuation(internalCurrent, continuationAction, flowExecutionContext, ref stackMark);
					}
				}
			}
			if (taskContinuation == null && flowExecutionContext)
			{
				taskContinuation = new AwaitTaskContinuation(continuationAction, true, ref stackMark);
			}
			if (taskContinuation != null)
			{
				if (!this.AddTaskContinuation(taskContinuation, false))
				{
					taskContinuation.Run(this, false);
					return;
				}
			}
			else if (!this.AddTaskContinuation(continuationAction, false))
			{
				AwaitTaskContinuation.UnsafeScheduleAction(continuationAction, this);
			}
		}

		/// <summary>Creates an awaitable task that asynchronously yields back to the current context when awaited.</summary>
		/// <returns>A context that, when awaited, will asynchronously transition back into the current context at the time of the await. If the current <see cref="T:System.Threading.SynchronizationContext" /> is non-null, it is treated as the current context. Otherwise, the task scheduler that is associated with the currently executing task is treated as the current context. </returns>
		// Token: 0x06003A7F RID: 14975 RVA: 0x000D3E2C File Offset: 0x000D202C
		public static YieldAwaitable Yield()
		{
			return default(YieldAwaitable);
		}

		/// <summary>Waits for the <see cref="T:System.Threading.Tasks.Task" /> to complete execution.</summary>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Threading.Tasks.Task" /> has been disposed.</exception>
		/// <exception cref="T:System.AggregateException">The <see cref="T:System.Threading.Tasks.Task" /> was canceled -or- an exception was thrown during the execution of the <see cref="T:System.Threading.Tasks.Task" />. If the task was canceled, the <see cref="T:System.AggregateException" /> contains an <see cref="T:System.OperationCanceledException" /> in its <see cref="P:System.AggregateException.InnerExceptions" /> collection.</exception>
		// Token: 0x06003A80 RID: 14976 RVA: 0x000D3E44 File Offset: 0x000D2044
		public void Wait()
		{
			this.Wait(-1, default(CancellationToken));
		}

		/// <summary>Waits for the <see cref="T:System.Threading.Tasks.Task" /> to complete execution within a specified time interval.</summary>
		/// <returns>true if the <see cref="T:System.Threading.Tasks.Task" /> completed execution within the allotted time; otherwise, false.</returns>
		/// <param name="timeout">A <see cref="T:System.TimeSpan" /> that represents the number of milliseconds to wait, or a <see cref="T:System.TimeSpan" /> that represents -1 milliseconds to wait indefinitely.</param>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Threading.Tasks.Task" /> has been disposed.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="timeout" /> is a negative number other than -1 milliseconds, which represents an infinite time-out -or- timeout is greater than <see cref="F:System.Int32.MaxValue" />.</exception>
		/// <exception cref="T:System.AggregateException">The <see cref="T:System.Threading.Tasks.Task" /> was canceled -or- an exception was thrown during the execution of the <see cref="T:System.Threading.Tasks.Task" />.</exception>
		// Token: 0x06003A81 RID: 14977 RVA: 0x000D3E64 File Offset: 0x000D2064
		public bool Wait(TimeSpan timeout)
		{
			long num = (long)timeout.TotalMilliseconds;
			if (num < -1L || num > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("timeout");
			}
			return this.Wait((int)num, default(CancellationToken));
		}

		/// <summary>Waits for the cancellable <see cref="T:System.Threading.Tasks.Task" /> to complete execution.</summary>
		/// <param name="cancellationToken">A <see cref="P:System.Threading.Tasks.TaskFactory.CancellationToken" /> to observe while waiting for the task to complete.</param>
		/// <exception cref="T:System.OperationCanceledException">The <paramref name="cancellationToken" /> was canceled.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Threading.Tasks.Task" /> has been disposed.</exception>
		/// <exception cref="T:System.AggregateException">The <see cref="T:System.Threading.Tasks.Task" /> was canceled -or- an exception was thrown during the execution of the <see cref="T:System.Threading.Tasks.Task" />. If the task was canceled, the <see cref="T:System.AggregateException" /> contains an <see cref="T:System.OperationCanceledException" /> in its <see cref="P:System.AggregateException.InnerExceptions" /> collection.</exception>
		// Token: 0x06003A82 RID: 14978 RVA: 0x000D3EA4 File Offset: 0x000D20A4
		public void Wait(CancellationToken cancellationToken)
		{
			this.Wait(-1, cancellationToken);
		}

		/// <summary>Waits for the <see cref="T:System.Threading.Tasks.Task" /> to complete execution within a specified number of milliseconds.</summary>
		/// <returns>true if the <see cref="T:System.Threading.Tasks.Task" /> completed execution within the allotted time; otherwise, false.</returns>
		/// <param name="millisecondsTimeout">The number of milliseconds to wait, or <see cref="F:System.Threading.Timeout.Infinite" /> (-1) to wait indefinitely.</param>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Threading.Tasks.Task" /> has been disposed.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="millisecondsTimeout" /> is a negative number other than -1, which represents an infinite time-out.</exception>
		/// <exception cref="T:System.AggregateException">The <see cref="T:System.Threading.Tasks.Task" /> was canceled -or- an exception was thrown during the execution of the <see cref="T:System.Threading.Tasks.Task" />. If the task was canceled, the <see cref="T:System.AggregateException" /> contains an <see cref="T:System.OperationCanceledException" /> in its <see cref="P:System.AggregateException.InnerExceptions" /> collection.</exception>
		// Token: 0x06003A83 RID: 14979 RVA: 0x000D3EB0 File Offset: 0x000D20B0
		public bool Wait(int millisecondsTimeout)
		{
			return this.Wait(millisecondsTimeout, default(CancellationToken));
		}

		/// <summary>Waits for the cancellable <see cref="T:System.Threading.Tasks.Task" /> to complete execution.</summary>
		/// <returns>true if the <see cref="T:System.Threading.Tasks.Task" /> completed execution within the allotted time; otherwise, false.</returns>
		/// <param name="millisecondsTimeout">The number of milliseconds to wait, or <see cref="F:System.Threading.Timeout.Infinite" /> (-1) to wait indefinitely.</param>
		/// <param name="cancellationToken">A <see cref="P:System.Threading.Tasks.TaskFactory.CancellationToken" /> to observe while waiting for the task to complete.</param>
		/// <exception cref="T:System.OperationCanceledException">The <paramref name="cancellationToken" /> was canceled.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Threading.Tasks.Task" /> has been disposed.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="millisecondsTimeout" /> is a negative number other than -1, which represents an infinite time-out.</exception>
		/// <exception cref="T:System.AggregateException">The <see cref="T:System.Threading.Tasks.Task" /> was canceled -or- an exception was thrown during the execution of the <see cref="T:System.Threading.Tasks.Task" />. If the task was canceled, the <see cref="T:System.AggregateException" /> contains an <see cref="T:System.OperationCanceledException" /> in its <see cref="P:System.AggregateException.InnerExceptions" /> collection.</exception>
		// Token: 0x06003A84 RID: 14980 RVA: 0x000D3ED0 File Offset: 0x000D20D0
		public bool Wait(int millisecondsTimeout, CancellationToken cancellationToken)
		{
			if (millisecondsTimeout < -1)
			{
				throw new ArgumentOutOfRangeException("millisecondsTimeout");
			}
			if (!this.IsWaitNotificationEnabledOrNotRanToCompletion)
			{
				return true;
			}
			if (!this.InternalWait(millisecondsTimeout, cancellationToken))
			{
				return false;
			}
			if (this.IsWaitNotificationEnabledOrNotRanToCompletion)
			{
				this.NotifyDebuggerOfWaitCompletionIfNecessary();
				if (this.IsCanceled)
				{
					cancellationToken.ThrowIfCancellationRequested();
				}
				this.ThrowIfExceptional(true);
			}
			return true;
		}

		// Token: 0x06003A85 RID: 14981 RVA: 0x000D3F28 File Offset: 0x000D2128
		private bool WrappedTryRunInline()
		{
			if (this.m_taskScheduler == null)
			{
				return false;
			}
			bool flag;
			try
			{
				flag = this.m_taskScheduler.TryRunInline(this, true);
			}
			catch (Exception ex)
			{
				if (!(ex is ThreadAbortException))
				{
					throw new TaskSchedulerException(ex);
				}
				throw;
			}
			return flag;
		}

		// Token: 0x06003A86 RID: 14982 RVA: 0x000D3F74 File Offset: 0x000D2174
		[MethodImpl(MethodImplOptions.NoOptimization)]
		internal bool InternalWait(int millisecondsTimeout, CancellationToken cancellationToken)
		{
			bool flag = this.IsCompleted;
			if (!flag)
			{
				Debugger.NotifyOfCrossThreadDependency();
				flag = (millisecondsTimeout == -1 && !cancellationToken.CanBeCanceled && this.WrappedTryRunInline() && this.IsCompleted) || this.SpinThenBlockingWait(millisecondsTimeout, cancellationToken);
			}
			return flag;
		}

		// Token: 0x06003A87 RID: 14983 RVA: 0x000D3FBC File Offset: 0x000D21BC
		private bool SpinThenBlockingWait(int millisecondsTimeout, CancellationToken cancellationToken)
		{
			bool flag = millisecondsTimeout == -1;
			uint num = (uint)(flag ? 0 : Environment.TickCount);
			bool flag2 = this.SpinWait(millisecondsTimeout);
			if (!flag2)
			{
				Task.SetOnInvokeMres setOnInvokeMres = new Task.SetOnInvokeMres();
				try
				{
					this.AddCompletionAction(setOnInvokeMres, true);
					if (flag)
					{
						flag2 = setOnInvokeMres.Wait(-1, cancellationToken);
					}
					else
					{
						uint num2 = (uint)(Environment.TickCount - (int)num);
						if ((ulong)num2 < (ulong)((long)millisecondsTimeout))
						{
							flag2 = setOnInvokeMres.Wait((int)((long)millisecondsTimeout - (long)((ulong)num2)), cancellationToken);
						}
					}
				}
				finally
				{
					if (!this.IsCompleted)
					{
						this.RemoveContinuation(setOnInvokeMres);
					}
				}
			}
			return flag2;
		}

		// Token: 0x06003A88 RID: 14984 RVA: 0x000D4044 File Offset: 0x000D2244
		private bool SpinWait(int millisecondsTimeout)
		{
			if (this.IsCompleted)
			{
				return true;
			}
			if (millisecondsTimeout == 0)
			{
				return false;
			}
			int num = (PlatformHelper.IsSingleProcessor ? 1 : 10);
			for (int i = 0; i < num; i++)
			{
				if (this.IsCompleted)
				{
					return true;
				}
				if (i == num / 2)
				{
					Thread.Yield();
				}
				else
				{
					Thread.SpinWait(PlatformHelper.ProcessorCount * (4 << i));
				}
			}
			return this.IsCompleted;
		}

		// Token: 0x06003A89 RID: 14985 RVA: 0x000D40A8 File Offset: 0x000D22A8
		[SecuritySafeCritical]
		internal bool InternalCancel(bool bCancelNonExecutingOnly)
		{
			bool flag = false;
			bool flag2 = false;
			TaskSchedulerException ex = null;
			if ((this.m_stateFlags & 65536) != 0)
			{
				TaskScheduler taskScheduler = this.m_taskScheduler;
				try
				{
					flag = taskScheduler != null && taskScheduler.TryDequeue(this);
				}
				catch (Exception ex2)
				{
					if (!(ex2 is ThreadAbortException))
					{
						ex = new TaskSchedulerException(ex2);
					}
				}
				bool flag3 = (taskScheduler != null && taskScheduler.RequiresAtomicStartTransition) || (this.Options & (TaskCreationOptions)2048) > TaskCreationOptions.None;
				if (!flag && bCancelNonExecutingOnly && flag3)
				{
					flag2 = this.AtomicStateUpdate(4194304, 4325376);
				}
			}
			if (!bCancelNonExecutingOnly || flag || flag2)
			{
				this.RecordInternalCancellationRequest();
				if (flag)
				{
					flag2 = this.AtomicStateUpdate(4194304, 4325376);
				}
				else if (!flag2 && (this.m_stateFlags & 65536) == 0)
				{
					flag2 = this.AtomicStateUpdate(4194304, 23265280);
				}
				if (flag2)
				{
					this.CancellationCleanupLogic();
				}
			}
			if (ex != null)
			{
				throw ex;
			}
			return flag2;
		}

		// Token: 0x06003A8A RID: 14986 RVA: 0x000D419C File Offset: 0x000D239C
		internal void RecordInternalCancellationRequest()
		{
			this.EnsureContingentPropertiesInitialized(true).m_internalCancellationRequested = 1;
		}

		// Token: 0x06003A8B RID: 14987 RVA: 0x000D41B0 File Offset: 0x000D23B0
		internal void RecordInternalCancellationRequest(CancellationToken tokenToRecord)
		{
			this.RecordInternalCancellationRequest();
			if (tokenToRecord != default(CancellationToken))
			{
				this.m_contingentProperties.m_cancellationToken = tokenToRecord;
			}
		}

		// Token: 0x06003A8C RID: 14988 RVA: 0x000D41E2 File Offset: 0x000D23E2
		internal void RecordInternalCancellationRequest(CancellationToken tokenToRecord, object cancellationException)
		{
			this.RecordInternalCancellationRequest(tokenToRecord);
			if (cancellationException != null)
			{
				this.AddException(cancellationException, true);
			}
		}

		// Token: 0x06003A8D RID: 14989 RVA: 0x000D41F8 File Offset: 0x000D23F8
		internal void CancellationCleanupLogic()
		{
			Interlocked.Exchange(ref this.m_stateFlags, this.m_stateFlags | 4194304);
			Task.ContingentProperties contingentProperties = this.m_contingentProperties;
			if (contingentProperties != null)
			{
				contingentProperties.SetCompleted();
				contingentProperties.DeregisterCancellationCallback();
			}
			if (AsyncCausalityTracer.LoggingOn)
			{
				AsyncCausalityTracer.TraceOperationCompletion(CausalityTraceLevel.Required, this.Id, AsyncCausalityStatus.Canceled);
			}
			if (Task.s_asyncDebuggingEnabled)
			{
				Task.RemoveFromActiveTasks(this.Id);
			}
			this.FinishStageThree();
		}

		// Token: 0x06003A8E RID: 14990 RVA: 0x000D4263 File Offset: 0x000D2463
		private void SetCancellationAcknowledged()
		{
			this.m_stateFlags |= 1048576;
		}

		// Token: 0x06003A8F RID: 14991 RVA: 0x000D427C File Offset: 0x000D247C
		[SecuritySafeCritical]
		internal void FinishContinuations()
		{
			object obj = Interlocked.Exchange(ref this.m_continuationObject, Task.s_taskCompletionSentinel);
			if (obj != null)
			{
				if (AsyncCausalityTracer.LoggingOn)
				{
					AsyncCausalityTracer.TraceSynchronousWorkStart(CausalityTraceLevel.Required, this.Id, CausalitySynchronousWork.CompletionNotification);
				}
				bool flag = (this.m_stateFlags & 134217728) == 0 && Thread.CurrentThread.ThreadState != ThreadState.AbortRequested && (this.m_stateFlags & 64) == 0;
				Action action = obj as Action;
				if (action != null)
				{
					AwaitTaskContinuation.RunOrScheduleAction(action, flag, ref Task.t_currentTask);
					this.LogFinishCompletionNotification();
					return;
				}
				ITaskCompletionAction taskCompletionAction = obj as ITaskCompletionAction;
				if (taskCompletionAction != null)
				{
					if (flag)
					{
						taskCompletionAction.Invoke(this);
					}
					else
					{
						ThreadPool.UnsafeQueueCustomWorkItem(new CompletionActionInvoker(taskCompletionAction, this), false);
					}
					this.LogFinishCompletionNotification();
					return;
				}
				TaskContinuation taskContinuation = obj as TaskContinuation;
				if (taskContinuation != null)
				{
					taskContinuation.Run(this, flag);
					this.LogFinishCompletionNotification();
					return;
				}
				List<object> list = obj as List<object>;
				if (list == null)
				{
					this.LogFinishCompletionNotification();
					return;
				}
				List<object> list2 = list;
				lock (list2)
				{
				}
				int count = list.Count;
				for (int i = 0; i < count; i++)
				{
					StandardTaskContinuation standardTaskContinuation = list[i] as StandardTaskContinuation;
					if (standardTaskContinuation != null && (standardTaskContinuation.m_options & TaskContinuationOptions.ExecuteSynchronously) == TaskContinuationOptions.None)
					{
						list[i] = null;
						standardTaskContinuation.Run(this, flag);
					}
				}
				for (int j = 0; j < count; j++)
				{
					object obj2 = list[j];
					if (obj2 != null)
					{
						list[j] = null;
						Action action2 = obj2 as Action;
						if (action2 != null)
						{
							AwaitTaskContinuation.RunOrScheduleAction(action2, flag, ref Task.t_currentTask);
						}
						else
						{
							TaskContinuation taskContinuation2 = obj2 as TaskContinuation;
							if (taskContinuation2 != null)
							{
								taskContinuation2.Run(this, flag);
							}
							else
							{
								ITaskCompletionAction taskCompletionAction2 = (ITaskCompletionAction)obj2;
								if (flag)
								{
									taskCompletionAction2.Invoke(this);
								}
								else
								{
									ThreadPool.UnsafeQueueCustomWorkItem(new CompletionActionInvoker(taskCompletionAction2, this), false);
								}
							}
						}
					}
				}
				this.LogFinishCompletionNotification();
			}
		}

		// Token: 0x06003A90 RID: 14992 RVA: 0x000D4464 File Offset: 0x000D2664
		private void LogFinishCompletionNotification()
		{
			if (AsyncCausalityTracer.LoggingOn)
			{
				AsyncCausalityTracer.TraceSynchronousWorkCompletion(CausalityTraceLevel.Required, CausalitySynchronousWork.CompletionNotification);
			}
		}

		/// <summary>Creates a continuation that executes asynchronously when the target <see cref="T:System.Threading.Tasks.Task" /> completes.</summary>
		/// <returns>A new continuation <see cref="T:System.Threading.Tasks.Task" />.</returns>
		/// <param name="continuationAction">An action to run when the <see cref="T:System.Threading.Tasks.Task" /> completes. When run, the delegate will be passed the completed task as an argument.</param>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Threading.CancellationTokenSource" /> that created <paramref name="cancellationToken" /> has already been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="continuationAction" /> argument is null.</exception>
		// Token: 0x06003A91 RID: 14993 RVA: 0x000D4474 File Offset: 0x000D2674
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task ContinueWith(Action<Task> continuationAction)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return this.ContinueWith(continuationAction, TaskScheduler.Current, default(CancellationToken), TaskContinuationOptions.None, ref stackCrawlMark);
		}

		/// <summary>Creates a continuation that executes asynchronously when the target <see cref="T:System.Threading.Tasks.Task" /> completes.</summary>
		/// <returns>A new continuation <see cref="T:System.Threading.Tasks.Task" />.</returns>
		/// <param name="continuationAction">An action to run when the <see cref="T:System.Threading.Tasks.Task" /> completes. When run, the delegate will be passed the completed task as an argument.</param>
		/// <param name="cancellationToken">The <see cref="P:System.Threading.Tasks.TaskFactory.CancellationToken" /> that will be assigned to the new continuation task.</param>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Threading.Tasks.Task" /> has been disposed.-or-The <see cref="T:System.Threading.CancellationTokenSource" /> that created the token has already been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="continuationAction" /> argument is null.</exception>
		// Token: 0x06003A92 RID: 14994 RVA: 0x000D449C File Offset: 0x000D269C
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task ContinueWith(Action<Task> continuationAction, CancellationToken cancellationToken)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return this.ContinueWith(continuationAction, TaskScheduler.Current, cancellationToken, TaskContinuationOptions.None, ref stackCrawlMark);
		}

		/// <summary>Creates a continuation that executes asynchronously when the target <see cref="T:System.Threading.Tasks.Task" /> completes.</summary>
		/// <returns>A new continuation <see cref="T:System.Threading.Tasks.Task" />.</returns>
		/// <param name="continuationAction">An action to run when the <see cref="T:System.Threading.Tasks.Task" /> completes. When run, the delegate will be passed the completed task as an argument.</param>
		/// <param name="scheduler">The <see cref="T:System.Threading.Tasks.TaskScheduler" /> to associate with the continuation task and to use for its execution.</param>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Threading.Tasks.Task" /> has been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="continuationAction" /> argument is null.-or-The <paramref name="scheduler" /> argument is null.</exception>
		// Token: 0x06003A93 RID: 14995 RVA: 0x000D44BC File Offset: 0x000D26BC
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task ContinueWith(Action<Task> continuationAction, TaskScheduler scheduler)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return this.ContinueWith(continuationAction, scheduler, default(CancellationToken), TaskContinuationOptions.None, ref stackCrawlMark);
		}

		/// <summary>Creates a continuation that executes according to the specified <see cref="T:System.Threading.Tasks.TaskContinuationOptions" />.</summary>
		/// <returns>A new continuation <see cref="T:System.Threading.Tasks.Task" />.</returns>
		/// <param name="continuationAction">An action to run according to the specified <paramref name="continuationOptions" />. When run, the delegate will be passed the completed task as an argument.</param>
		/// <param name="continuationOptions">Options for when the continuation is scheduled and how it behaves. This includes criteria, such as <see cref="F:System.Threading.Tasks.TaskContinuationOptions.OnlyOnCanceled" />, as well as execution options, such as <see cref="F:System.Threading.Tasks.TaskContinuationOptions.ExecuteSynchronously" />.</param>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Threading.Tasks.Task" /> has been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="continuationAction" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="continuationOptions" /> argument specifies an invalid value for <see cref="T:System.Threading.Tasks.TaskContinuationOptions" />.</exception>
		// Token: 0x06003A94 RID: 14996 RVA: 0x000D44E0 File Offset: 0x000D26E0
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task ContinueWith(Action<Task> continuationAction, TaskContinuationOptions continuationOptions)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return this.ContinueWith(continuationAction, TaskScheduler.Current, default(CancellationToken), continuationOptions, ref stackCrawlMark);
		}

		/// <summary>Creates a continuation that executes according to the specified <see cref="T:System.Threading.Tasks.TaskContinuationOptions" />.</summary>
		/// <returns>A new continuation <see cref="T:System.Threading.Tasks.Task" />.</returns>
		/// <param name="continuationAction">An action to run according to the specified <paramref name="continuationOptions" />. When run, the delegate will be passed the completed task as an argument.</param>
		/// <param name="cancellationToken">The <see cref="P:System.Threading.Tasks.TaskFactory.CancellationToken" /> that will be assigned to the new continuation task.</param>
		/// <param name="continuationOptions">Options for when the continuation is scheduled and how it behaves. This includes criteria, such as <see cref="F:System.Threading.Tasks.TaskContinuationOptions.OnlyOnCanceled" />, as well as execution options, such as <see cref="F:System.Threading.Tasks.TaskContinuationOptions.ExecuteSynchronously" />.</param>
		/// <param name="scheduler">The <see cref="T:System.Threading.Tasks.TaskScheduler" /> to associate with the continuation task and to use for its execution.</param>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Threading.Tasks.Task" /> has been disposed.-or-The <see cref="T:System.Threading.CancellationTokenSource" /> that created the token has already been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="continuationAction" /> argument is null.-or-The <paramref name="scheduler" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="continuationOptions" /> argument specifies an invalid value for <see cref="T:System.Threading.Tasks.TaskContinuationOptions" />.</exception>
		// Token: 0x06003A95 RID: 14997 RVA: 0x000D4508 File Offset: 0x000D2708
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task ContinueWith(Action<Task> continuationAction, CancellationToken cancellationToken, TaskContinuationOptions continuationOptions, TaskScheduler scheduler)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return this.ContinueWith(continuationAction, scheduler, cancellationToken, continuationOptions, ref stackCrawlMark);
		}

		// Token: 0x06003A96 RID: 14998 RVA: 0x000D4524 File Offset: 0x000D2724
		private Task ContinueWith(Action<Task> continuationAction, TaskScheduler scheduler, CancellationToken cancellationToken, TaskContinuationOptions continuationOptions, ref StackCrawlMark stackMark)
		{
			if (continuationAction == null)
			{
				throw new ArgumentNullException("continuationAction");
			}
			if (scheduler == null)
			{
				throw new ArgumentNullException("scheduler");
			}
			TaskCreationOptions taskCreationOptions;
			InternalTaskOptions internalTaskOptions;
			Task.CreationOptionsFromContinuationOptions(continuationOptions, out taskCreationOptions, out internalTaskOptions);
			Task task = new ContinuationTaskFromTask(this, continuationAction, null, taskCreationOptions, internalTaskOptions, ref stackMark);
			this.ContinueWithCore(task, scheduler, cancellationToken, continuationOptions);
			return task;
		}

		/// <summary>Creates a continuation that executes when the target <see cref="T:System.Threading.Tasks.Task" /> completes.</summary>
		/// <returns>A new continuation <see cref="T:System.Threading.Tasks.Task" />.</returns>
		/// <param name="continuationAction">An action to run when the <see cref="T:System.Threading.Tasks.Task" /> completes. When run, the delegate will be passed the completed task as and the caller-supplied state object as arguments.</param>
		/// <param name="state">An object representing data to be used by the continuation action.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="continuationAction" /> argument is null.</exception>
		// Token: 0x06003A97 RID: 14999 RVA: 0x000D4574 File Offset: 0x000D2774
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task ContinueWith(Action<Task, object> continuationAction, object state)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return this.ContinueWith(continuationAction, state, TaskScheduler.Current, default(CancellationToken), TaskContinuationOptions.None, ref stackCrawlMark);
		}

		/// <summary>Creates a continuation that executes when the target <see cref="T:System.Threading.Tasks.Task" /> completes.</summary>
		/// <returns>A new continuation <see cref="T:System.Threading.Tasks.Task" />.</returns>
		/// <param name="continuationAction">An action to run when the <see cref="T:System.Threading.Tasks.Task" /> completes. When run, the delegate will be passed the completed task and the caller-supplied state object as arguments.</param>
		/// <param name="state">An object representing data to be used by the continuation action.</param>
		/// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> that will be assigned to the new continuation task.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="continuationAction" /> argument is null.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The provided <see cref="T:System.Threading.CancellationToken" /> has already been disposed.</exception>
		// Token: 0x06003A98 RID: 15000 RVA: 0x000D459C File Offset: 0x000D279C
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task ContinueWith(Action<Task, object> continuationAction, object state, CancellationToken cancellationToken)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return this.ContinueWith(continuationAction, state, TaskScheduler.Current, cancellationToken, TaskContinuationOptions.None, ref stackCrawlMark);
		}

		/// <summary>Creates a continuation that executes when the target <see cref="T:System.Threading.Tasks.Task" /> completes.</summary>
		/// <returns>A new continuation <see cref="T:System.Threading.Tasks.Task" />.</returns>
		/// <param name="continuationAction">An action to run when the <see cref="T:System.Threading.Tasks.Task" /> completes.  When run, the delegate will be  passed the completed task and the caller-supplied state object as arguments.</param>
		/// <param name="state">An object representing data to be used by the continuation action.</param>
		/// <param name="scheduler">The <see cref="T:System.Threading.Tasks.TaskScheduler" /> to associate with the continuation task and to use for its execution.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="continuationAction" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="scheduler" /> argument is null.</exception>
		// Token: 0x06003A99 RID: 15001 RVA: 0x000D45BC File Offset: 0x000D27BC
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task ContinueWith(Action<Task, object> continuationAction, object state, TaskScheduler scheduler)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return this.ContinueWith(continuationAction, state, scheduler, default(CancellationToken), TaskContinuationOptions.None, ref stackCrawlMark);
		}

		/// <summary>Creates a continuation that executes when the target <see cref="T:System.Threading.Tasks.Task" /> completes.</summary>
		/// <returns>A new continuation <see cref="T:System.Threading.Tasks.Task" />.</returns>
		/// <param name="continuationAction">An action to run when the <see cref="T:System.Threading.Tasks.Task" /> completes. When run, the delegate will be  passed the completed task and the caller-supplied state object as arguments.</param>
		/// <param name="state">An object representing data to be used by the continuation action.</param>
		/// <param name="continuationOptions">Options for when the continuation is scheduled and how it behaves. This includes criteria, such as <see cref="F:System.Threading.Tasks.TaskContinuationOptions.OnlyOnCanceled" />, as well as execution options, such as <see cref="F:System.Threading.Tasks.TaskContinuationOptions.ExecuteSynchronously" />.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="continuationAction" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="continuationOptions" /> argument specifies an invalid value for <see cref="T:System.Threading.Tasks.TaskContinuationOptions" />.</exception>
		// Token: 0x06003A9A RID: 15002 RVA: 0x000D45E0 File Offset: 0x000D27E0
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task ContinueWith(Action<Task, object> continuationAction, object state, TaskContinuationOptions continuationOptions)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return this.ContinueWith(continuationAction, state, TaskScheduler.Current, default(CancellationToken), continuationOptions, ref stackCrawlMark);
		}

		/// <summary>Creates a continuation that executes when the target <see cref="T:System.Threading.Tasks.Task" /> completes.</summary>
		/// <returns>A new continuation <see cref="T:System.Threading.Tasks.Task" />.</returns>
		/// <param name="continuationAction">An action to run when the <see cref="T:System.Threading.Tasks.Task" /> completes. When run, the delegate will be  passed the completed task and the caller-supplied state object as arguments.</param>
		/// <param name="state">An object representing data to be used by the continuation action.</param>
		/// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> that will be assigned to the new continuation task.</param>
		/// <param name="continuationOptions">Options for when the continuation is scheduled and how it behaves. This includes criteria, such as <see cref="F:System.Threading.Tasks.TaskContinuationOptions.OnlyOnCanceled" />, as well as execution options, such as <see cref="F:System.Threading.Tasks.TaskContinuationOptions.ExecuteSynchronously" />.</param>
		/// <param name="scheduler">The <see cref="T:System.Threading.Tasks.TaskScheduler" /> to associate with the continuation task and to use for its  execution.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="continuationAction" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="continuationOptions" /> argument specifies an invalid value for <see cref="T:System.Threading.Tasks.TaskContinuationOptions" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="scheduler" /> argument is null.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The provided <see cref="T:System.Threading.CancellationToken" /> has already been disposed.</exception>
		// Token: 0x06003A9B RID: 15003 RVA: 0x000D4608 File Offset: 0x000D2808
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task ContinueWith(Action<Task, object> continuationAction, object state, CancellationToken cancellationToken, TaskContinuationOptions continuationOptions, TaskScheduler scheduler)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return this.ContinueWith(continuationAction, state, scheduler, cancellationToken, continuationOptions, ref stackCrawlMark);
		}

		// Token: 0x06003A9C RID: 15004 RVA: 0x000D4628 File Offset: 0x000D2828
		private Task ContinueWith(Action<Task, object> continuationAction, object state, TaskScheduler scheduler, CancellationToken cancellationToken, TaskContinuationOptions continuationOptions, ref StackCrawlMark stackMark)
		{
			if (continuationAction == null)
			{
				throw new ArgumentNullException("continuationAction");
			}
			if (scheduler == null)
			{
				throw new ArgumentNullException("scheduler");
			}
			TaskCreationOptions taskCreationOptions;
			InternalTaskOptions internalTaskOptions;
			Task.CreationOptionsFromContinuationOptions(continuationOptions, out taskCreationOptions, out internalTaskOptions);
			Task task = new ContinuationTaskFromTask(this, continuationAction, state, taskCreationOptions, internalTaskOptions, ref stackMark);
			this.ContinueWithCore(task, scheduler, cancellationToken, continuationOptions);
			return task;
		}

		/// <summary>Creates a continuation that executes asynchronously when the target <see cref="T:System.Threading.Tasks.Task" /> completes.</summary>
		/// <returns>A new continuation <see cref="T:System.Threading.Tasks.Task`1" />.</returns>
		/// <param name="continuationFunction">A function to run when the <see cref="T:System.Threading.Tasks.Task" /> completes. When run, the delegate will be passed the completed task as an argument.</param>
		/// <typeparam name="TResult"> The type of the result produced by the continuation.</typeparam>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Threading.Tasks.Task" /> has been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="continuationFunction" /> argument is null.</exception>
		// Token: 0x06003A9D RID: 15005 RVA: 0x000D4678 File Offset: 0x000D2878
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task<TResult> ContinueWith<TResult>(Func<Task, TResult> continuationFunction)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return this.ContinueWith<TResult>(continuationFunction, TaskScheduler.Current, default(CancellationToken), TaskContinuationOptions.None, ref stackCrawlMark);
		}

		/// <summary>Creates a continuation that executes asynchronously when the target <see cref="T:System.Threading.Tasks.Task" /> completes.</summary>
		/// <returns>A new continuation <see cref="T:System.Threading.Tasks.Task`1" />.</returns>
		/// <param name="continuationFunction">A function to run when the <see cref="T:System.Threading.Tasks.Task" /> completes. When run, the delegate will be passed the completed task as an argument.</param>
		/// <param name="cancellationToken">The <see cref="P:System.Threading.Tasks.TaskFactory.CancellationToken" /> that will be assigned to the new continuation task.</param>
		/// <typeparam name="TResult"> The type of the result produced by the continuation.</typeparam>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Threading.Tasks.Task" /> has been disposed.-or-The <see cref="T:System.Threading.CancellationTokenSource" /> that created the token has already been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="continuationFunction" /> argument is null.</exception>
		// Token: 0x06003A9E RID: 15006 RVA: 0x000D46A0 File Offset: 0x000D28A0
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task<TResult> ContinueWith<TResult>(Func<Task, TResult> continuationFunction, CancellationToken cancellationToken)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return this.ContinueWith<TResult>(continuationFunction, TaskScheduler.Current, cancellationToken, TaskContinuationOptions.None, ref stackCrawlMark);
		}

		/// <summary>Creates a continuation that executes asynchronously when the target <see cref="T:System.Threading.Tasks.Task" /> completes.</summary>
		/// <returns>A new continuation <see cref="T:System.Threading.Tasks.Task`1" />.</returns>
		/// <param name="continuationFunction">A function to run when the <see cref="T:System.Threading.Tasks.Task" /> completes. When run, the delegate will be passed the completed task as an argument.</param>
		/// <param name="scheduler">The <see cref="T:System.Threading.Tasks.TaskScheduler" /> to associate with the continuation task and to use for its execution.</param>
		/// <typeparam name="TResult"> The type of the result produced by the continuation.</typeparam>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Threading.Tasks.Task" /> has been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="continuationFunction" /> argument is null.-or-The <paramref name="scheduler" /> argument is null.</exception>
		// Token: 0x06003A9F RID: 15007 RVA: 0x000D46C0 File Offset: 0x000D28C0
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task<TResult> ContinueWith<TResult>(Func<Task, TResult> continuationFunction, TaskScheduler scheduler)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return this.ContinueWith<TResult>(continuationFunction, scheduler, default(CancellationToken), TaskContinuationOptions.None, ref stackCrawlMark);
		}

		/// <summary>Creates a continuation that executes according to the condition specified in <paramref name="continuationOptions" />.</summary>
		/// <returns>A new continuation <see cref="T:System.Threading.Tasks.Task`1" />.</returns>
		/// <param name="continuationFunction">A function to run according to the condition specified in <paramref name="continuationOptions" />. When run, the delegate will be passed the completed task as an argument.</param>
		/// <param name="continuationOptions">Options for when the continuation is scheduled and how it behaves. This includes criteria, such as <see cref="F:System.Threading.Tasks.TaskContinuationOptions.OnlyOnCanceled" />, as well as execution options, such as <see cref="F:System.Threading.Tasks.TaskContinuationOptions.ExecuteSynchronously" />.</param>
		/// <typeparam name="TResult"> The type of the result produced by the continuation.</typeparam>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Threading.Tasks.Task" /> has been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="continuationFunction" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="continuationOptions" /> argument specifies an invalid value for <see cref="T:System.Threading.Tasks.TaskContinuationOptions" />.</exception>
		// Token: 0x06003AA0 RID: 15008 RVA: 0x000D46E4 File Offset: 0x000D28E4
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task<TResult> ContinueWith<TResult>(Func<Task, TResult> continuationFunction, TaskContinuationOptions continuationOptions)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return this.ContinueWith<TResult>(continuationFunction, TaskScheduler.Current, default(CancellationToken), continuationOptions, ref stackCrawlMark);
		}

		/// <summary>Creates a continuation that executes according to the condition specified in <paramref name="continuationOptions" />.</summary>
		/// <returns>A new continuation <see cref="T:System.Threading.Tasks.Task`1" />.</returns>
		/// <param name="continuationFunction">A function to run according to the specified <paramref name="continuationOptions." /> When run, the delegate will be passed the completed task as an argument.</param>
		/// <param name="cancellationToken">The <see cref="P:System.Threading.Tasks.TaskFactory.CancellationToken" /> that will be assigned to the new continuation task.</param>
		/// <param name="continuationOptions">Options for when the continuation is scheduled and how it behaves. This includes criteria, such as <see cref="F:System.Threading.Tasks.TaskContinuationOptions.OnlyOnCanceled" />, as well as execution options, such as <see cref="F:System.Threading.Tasks.TaskContinuationOptions.ExecuteSynchronously" />.</param>
		/// <param name="scheduler">The <see cref="T:System.Threading.Tasks.TaskScheduler" /> to associate with the continuation task and to use for its execution.</param>
		/// <typeparam name="TResult"> The type of the result produced by the continuation.</typeparam>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Threading.Tasks.Task" /> has been disposed.-or-The <see cref="T:System.Threading.CancellationTokenSource" /> that created the token has already been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="continuationFunction" /> argument is null.-or-The <paramref name="scheduler" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="continuationOptions" /> argument specifies an invalid value for <see cref="T:System.Threading.Tasks.TaskContinuationOptions" />.</exception>
		// Token: 0x06003AA1 RID: 15009 RVA: 0x000D470C File Offset: 0x000D290C
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task<TResult> ContinueWith<TResult>(Func<Task, TResult> continuationFunction, CancellationToken cancellationToken, TaskContinuationOptions continuationOptions, TaskScheduler scheduler)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return this.ContinueWith<TResult>(continuationFunction, scheduler, cancellationToken, continuationOptions, ref stackCrawlMark);
		}

		// Token: 0x06003AA2 RID: 15010 RVA: 0x000D4728 File Offset: 0x000D2928
		private Task<TResult> ContinueWith<TResult>(Func<Task, TResult> continuationFunction, TaskScheduler scheduler, CancellationToken cancellationToken, TaskContinuationOptions continuationOptions, ref StackCrawlMark stackMark)
		{
			if (continuationFunction == null)
			{
				throw new ArgumentNullException("continuationFunction");
			}
			if (scheduler == null)
			{
				throw new ArgumentNullException("scheduler");
			}
			TaskCreationOptions taskCreationOptions;
			InternalTaskOptions internalTaskOptions;
			Task.CreationOptionsFromContinuationOptions(continuationOptions, out taskCreationOptions, out internalTaskOptions);
			Task<TResult> task = new ContinuationResultTaskFromTask<TResult>(this, continuationFunction, null, taskCreationOptions, internalTaskOptions, ref stackMark);
			this.ContinueWithCore(task, scheduler, cancellationToken, continuationOptions);
			return task;
		}

		/// <summary>Creates a continuation that executes when the target <see cref="T:System.Threading.Tasks.Task" /> completes.</summary>
		/// <returns>A new continuation <see cref="T:System.Threading.Tasks.Task`1" />.</returns>
		/// <param name="continuationFunction">A function to run when the <see cref="T:System.Threading.Tasks.Task" /> completes. When run, the delegate will be  passed the completed task and the caller-supplied state object as arguments.</param>
		/// <param name="state">An object representing data to be used by the continuation function.</param>
		/// <typeparam name="TResult">The type of the result produced by the continuation.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="continuationFunction" /> argument is null.</exception>
		// Token: 0x06003AA3 RID: 15011 RVA: 0x000D4778 File Offset: 0x000D2978
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task<TResult> ContinueWith<TResult>(Func<Task, object, TResult> continuationFunction, object state)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return this.ContinueWith<TResult>(continuationFunction, state, TaskScheduler.Current, default(CancellationToken), TaskContinuationOptions.None, ref stackCrawlMark);
		}

		/// <summary>Creates a continuation that executes when the target <see cref="T:System.Threading.Tasks.Task" /> completes.</summary>
		/// <returns>A new continuation <see cref="T:System.Threading.Tasks.Task`1" />.</returns>
		/// <param name="continuationFunction">A function to run when the <see cref="T:System.Threading.Tasks.Task" /> completes. When run, the delegate will be  passed the completed task and the caller-supplied state object as arguments.</param>
		/// <param name="state">An object representing data to be used by the continuation function.</param>
		/// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> that will be assigned to the new continuation task.</param>
		/// <typeparam name="TResult">The type of the result produced by the continuation.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="continuationFunction" /> argument is null.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The provided <see cref="T:System.Threading.CancellationToken" /> has already been disposed.</exception>
		// Token: 0x06003AA4 RID: 15012 RVA: 0x000D47A0 File Offset: 0x000D29A0
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task<TResult> ContinueWith<TResult>(Func<Task, object, TResult> continuationFunction, object state, CancellationToken cancellationToken)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return this.ContinueWith<TResult>(continuationFunction, state, TaskScheduler.Current, cancellationToken, TaskContinuationOptions.None, ref stackCrawlMark);
		}

		/// <summary>Creates a continuation that executes when the target <see cref="T:System.Threading.Tasks.Task" /> completes.</summary>
		/// <returns>A new continuation <see cref="T:System.Threading.Tasks.Task`1" />.</returns>
		/// <param name="continuationFunction">A function to run when the <see cref="T:System.Threading.Tasks.Task" /> completes.  When run, the delegate will be  passed the completed task and the caller-supplied state object as arguments.</param>
		/// <param name="state">An object representing data to be used by the continuation function.</param>
		/// <param name="scheduler">The <see cref="T:System.Threading.Tasks.TaskScheduler" /> to associate with the continuation task and to use for its execution.</param>
		/// <typeparam name="TResult">The type of the result produced by the continuation.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="continuationFunction" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="scheduler" /> argument is null.</exception>
		// Token: 0x06003AA5 RID: 15013 RVA: 0x000D47C0 File Offset: 0x000D29C0
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task<TResult> ContinueWith<TResult>(Func<Task, object, TResult> continuationFunction, object state, TaskScheduler scheduler)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return this.ContinueWith<TResult>(continuationFunction, state, scheduler, default(CancellationToken), TaskContinuationOptions.None, ref stackCrawlMark);
		}

		/// <summary>Creates a continuation that executes when the target <see cref="T:System.Threading.Tasks.Task" /> completes.</summary>
		/// <returns>A new continuation <see cref="T:System.Threading.Tasks.Task`1" />.</returns>
		/// <param name="continuationFunction">A function to run when the <see cref="T:System.Threading.Tasks.Task" /> completes. When run, the delegate will be  passed the completed task and the caller-supplied state object as arguments.</param>
		/// <param name="state">An object representing data to be used by the continuation function.</param>
		/// <param name="continuationOptions">Options for when the continuation is scheduled and how it behaves. This includes criteria, such as <see cref="F:System.Threading.Tasks.TaskContinuationOptions.OnlyOnCanceled" />, as well as execution options, such as <see cref="F:System.Threading.Tasks.TaskContinuationOptions.ExecuteSynchronously" />.</param>
		/// <typeparam name="TResult">The type of the result produced by the continuation.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="continuationFunction" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="continuationOptions" /> argument specifies an invalid value for <see cref="T:System.Threading.Tasks.TaskContinuationOptions" />.</exception>
		// Token: 0x06003AA6 RID: 15014 RVA: 0x000D47E4 File Offset: 0x000D29E4
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task<TResult> ContinueWith<TResult>(Func<Task, object, TResult> continuationFunction, object state, TaskContinuationOptions continuationOptions)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return this.ContinueWith<TResult>(continuationFunction, state, TaskScheduler.Current, default(CancellationToken), continuationOptions, ref stackCrawlMark);
		}

		/// <summary>Creates a continuation that executes when the target <see cref="T:System.Threading.Tasks.Task" /> completes.</summary>
		/// <returns>A new continuation <see cref="T:System.Threading.Tasks.Task`1" />.</returns>
		/// <param name="continuationFunction">A function to run when the <see cref="T:System.Threading.Tasks.Task" /> completes. When run, the delegate will be  passed the completed task and the caller-supplied state object as arguments.</param>
		/// <param name="state">An object representing data to be used by the continuation function.</param>
		/// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> that will be assigned to the new continuation task.</param>
		/// <param name="continuationOptions">Options for when the continuation is scheduled and how it behaves. This includes criteria, such as <see cref="F:System.Threading.Tasks.TaskContinuationOptions.OnlyOnCanceled" />, as well as execution options, such as <see cref="F:System.Threading.Tasks.TaskContinuationOptions.ExecuteSynchronously" />.</param>
		/// <param name="scheduler">The <see cref="T:System.Threading.Tasks.TaskScheduler" /> to associate with the continuation task and to use for its  execution.</param>
		/// <typeparam name="TResult">The type of the result produced by the continuation.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="continuationFunction" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="continuationOptions" /> argument specifies an invalid value for <see cref="T:System.Threading.Tasks.TaskContinuationOptions" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="scheduler" /> argument is null.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The provided <see cref="T:System.Threading.CancellationToken" /> has already been disposed.</exception>
		// Token: 0x06003AA7 RID: 15015 RVA: 0x000D480C File Offset: 0x000D2A0C
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task<TResult> ContinueWith<TResult>(Func<Task, object, TResult> continuationFunction, object state, CancellationToken cancellationToken, TaskContinuationOptions continuationOptions, TaskScheduler scheduler)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return this.ContinueWith<TResult>(continuationFunction, state, scheduler, cancellationToken, continuationOptions, ref stackCrawlMark);
		}

		// Token: 0x06003AA8 RID: 15016 RVA: 0x000D482C File Offset: 0x000D2A2C
		private Task<TResult> ContinueWith<TResult>(Func<Task, object, TResult> continuationFunction, object state, TaskScheduler scheduler, CancellationToken cancellationToken, TaskContinuationOptions continuationOptions, ref StackCrawlMark stackMark)
		{
			if (continuationFunction == null)
			{
				throw new ArgumentNullException("continuationFunction");
			}
			if (scheduler == null)
			{
				throw new ArgumentNullException("scheduler");
			}
			TaskCreationOptions taskCreationOptions;
			InternalTaskOptions internalTaskOptions;
			Task.CreationOptionsFromContinuationOptions(continuationOptions, out taskCreationOptions, out internalTaskOptions);
			Task<TResult> task = new ContinuationResultTaskFromTask<TResult>(this, continuationFunction, state, taskCreationOptions, internalTaskOptions, ref stackMark);
			this.ContinueWithCore(task, scheduler, cancellationToken, continuationOptions);
			return task;
		}

		// Token: 0x06003AA9 RID: 15017 RVA: 0x000D487C File Offset: 0x000D2A7C
		internal static void CreationOptionsFromContinuationOptions(TaskContinuationOptions continuationOptions, out TaskCreationOptions creationOptions, out InternalTaskOptions internalOptions)
		{
			TaskContinuationOptions taskContinuationOptions = TaskContinuationOptions.NotOnRanToCompletion | TaskContinuationOptions.NotOnFaulted | TaskContinuationOptions.NotOnCanceled;
			TaskContinuationOptions taskContinuationOptions2 = TaskContinuationOptions.PreferFairness | TaskContinuationOptions.LongRunning | TaskContinuationOptions.AttachedToParent | TaskContinuationOptions.DenyChildAttach | TaskContinuationOptions.HideScheduler | TaskContinuationOptions.RunContinuationsAsynchronously;
			TaskContinuationOptions taskContinuationOptions3 = TaskContinuationOptions.LongRunning | TaskContinuationOptions.ExecuteSynchronously;
			if ((continuationOptions & taskContinuationOptions3) == taskContinuationOptions3)
			{
				throw new ArgumentOutOfRangeException("continuationOptions", Environment.GetResourceString("The specified TaskContinuationOptions combined LongRunning and ExecuteSynchronously.  Synchronous continuations should not be long running."));
			}
			if ((continuationOptions & ~((taskContinuationOptions2 | taskContinuationOptions | TaskContinuationOptions.LazyCancellation | TaskContinuationOptions.ExecuteSynchronously) != TaskContinuationOptions.None)) != TaskContinuationOptions.None)
			{
				throw new ArgumentOutOfRangeException("continuationOptions");
			}
			if ((continuationOptions & taskContinuationOptions) == taskContinuationOptions)
			{
				throw new ArgumentOutOfRangeException("continuationOptions", Environment.GetResourceString("The specified TaskContinuationOptions excluded all continuation kinds."));
			}
			creationOptions = (TaskCreationOptions)(continuationOptions & taskContinuationOptions2);
			internalOptions = InternalTaskOptions.ContinuationTask;
			if ((continuationOptions & TaskContinuationOptions.LazyCancellation) != TaskContinuationOptions.None)
			{
				internalOptions |= InternalTaskOptions.LazyCancellation;
			}
		}

		// Token: 0x06003AAA RID: 15018 RVA: 0x000D4908 File Offset: 0x000D2B08
		internal void ContinueWithCore(Task continuationTask, TaskScheduler scheduler, CancellationToken cancellationToken, TaskContinuationOptions options)
		{
			TaskContinuation taskContinuation = new StandardTaskContinuation(continuationTask, options, scheduler);
			if (cancellationToken.CanBeCanceled)
			{
				if (this.IsCompleted || cancellationToken.IsCancellationRequested)
				{
					continuationTask.AssignCancellationToken(cancellationToken, null, null);
				}
				else
				{
					continuationTask.AssignCancellationToken(cancellationToken, this, taskContinuation);
				}
			}
			if (!continuationTask.IsCompleted && !this.AddTaskContinuation(taskContinuation, false))
			{
				taskContinuation.Run(this, true);
			}
		}

		// Token: 0x06003AAB RID: 15019 RVA: 0x000D4967 File Offset: 0x000D2B67
		internal void AddCompletionAction(ITaskCompletionAction action)
		{
			this.AddCompletionAction(action, false);
		}

		// Token: 0x06003AAC RID: 15020 RVA: 0x000D4971 File Offset: 0x000D2B71
		private void AddCompletionAction(ITaskCompletionAction action, bool addBeforeOthers)
		{
			if (!this.AddTaskContinuation(action, addBeforeOthers))
			{
				action.Invoke(this);
			}
		}

		// Token: 0x06003AAD RID: 15021 RVA: 0x000D4984 File Offset: 0x000D2B84
		private bool AddTaskContinuationComplex(object tc, bool addBeforeOthers)
		{
			object continuationObject = this.m_continuationObject;
			if (continuationObject != Task.s_taskCompletionSentinel && !(continuationObject is List<object>))
			{
				Interlocked.CompareExchange(ref this.m_continuationObject, new List<object> { continuationObject }, continuationObject);
			}
			List<object> list = this.m_continuationObject as List<object>;
			if (list != null)
			{
				List<object> list2 = list;
				lock (list2)
				{
					if (this.m_continuationObject != Task.s_taskCompletionSentinel)
					{
						if (list.Count == list.Capacity)
						{
							list.RemoveAll(Task.s_IsTaskContinuationNullPredicate);
						}
						if (addBeforeOthers)
						{
							list.Insert(0, tc);
						}
						else
						{
							list.Add(tc);
						}
						return true;
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x06003AAE RID: 15022 RVA: 0x000D4A48 File Offset: 0x000D2C48
		private bool AddTaskContinuation(object tc, bool addBeforeOthers)
		{
			return !this.IsCompleted && ((this.m_continuationObject == null && Interlocked.CompareExchange(ref this.m_continuationObject, tc, null) == null) || this.AddTaskContinuationComplex(tc, addBeforeOthers));
		}

		// Token: 0x06003AAF RID: 15023 RVA: 0x000D4A78 File Offset: 0x000D2C78
		internal void RemoveContinuation(object continuationObject)
		{
			object continuationObject2 = this.m_continuationObject;
			if (continuationObject2 == Task.s_taskCompletionSentinel)
			{
				return;
			}
			List<object> list = continuationObject2 as List<object>;
			if (list == null)
			{
				if (Interlocked.CompareExchange(ref this.m_continuationObject, new List<object>(), continuationObject) == continuationObject)
				{
					return;
				}
				list = this.m_continuationObject as List<object>;
			}
			if (list != null)
			{
				List<object> list2 = list;
				lock (list2)
				{
					if (this.m_continuationObject != Task.s_taskCompletionSentinel)
					{
						int num = list.IndexOf(continuationObject);
						if (num != -1)
						{
							list[num] = null;
						}
					}
				}
			}
		}

		/// <summary>Waits for all of the provided <see cref="T:System.Threading.Tasks.Task" /> objects to complete execution.</summary>
		/// <param name="tasks">An array of <see cref="T:System.Threading.Tasks.Task" /> instances on which to wait.</param>
		/// <exception cref="T:System.ObjectDisposedException">One or more of the <see cref="T:System.Threading.Tasks.Task" /> objects in <paramref name="tasks" /> has been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="tasks" /> argument is null.-or-The <paramref name="tasks" /> argument contains a null element.</exception>
		/// <exception cref="T:System.AggregateException">At least one of the <see cref="T:System.Threading.Tasks.Task" /> instances was canceled -or- an exception was thrown during the execution of at least one of the <see cref="T:System.Threading.Tasks.Task" /> instances. If a task was canceled, the <see cref="T:System.AggregateException" /> contains an <see cref="T:System.OperationCanceledException" /> in its <see cref="P:System.AggregateException.InnerExceptions" /> collection.</exception>
		// Token: 0x06003AB0 RID: 15024 RVA: 0x000D4B1C File Offset: 0x000D2D1C
		[MethodImpl(MethodImplOptions.NoOptimization)]
		public static void WaitAll(params Task[] tasks)
		{
			Task.WaitAll(tasks, -1);
		}

		/// <summary>Waits for all of the provided cancellable <see cref="T:System.Threading.Tasks.Task" /> objects to complete execution within a specified time interval.</summary>
		/// <returns>true if all of the <see cref="T:System.Threading.Tasks.Task" /> instances completed execution within the allotted time; otherwise, false.</returns>
		/// <param name="tasks">An array of <see cref="T:System.Threading.Tasks.Task" /> instances on which to wait.</param>
		/// <param name="timeout">A <see cref="T:System.TimeSpan" /> that represents the number of milliseconds to wait, or a <see cref="T:System.TimeSpan" /> that represents -1 milliseconds to wait indefinitely.</param>
		/// <exception cref="T:System.ObjectDisposedException">One or more of the <see cref="T:System.Threading.Tasks.Task" /> objects in <paramref name="tasks" /> has been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="tasks" /> argument is null.</exception>
		/// <exception cref="T:System.AggregateException">At least one of the <see cref="T:System.Threading.Tasks.Task" /> instances was canceled -or- an exception was thrown during the execution of at least one of the <see cref="T:System.Threading.Tasks.Task" /> instances. If a task was canceled, the <see cref="T:System.AggregateException" /> contains an <see cref="T:System.OperationCanceledException" /> in its <see cref="P:System.AggregateException.InnerExceptions" /> collection.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="timeout" /> is a negative number other than -1 milliseconds, which represents an infinite time-out -or- timeout is greater than <see cref="F:System.Int32.MaxValue" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="tasks" /> argument contains a null element.</exception>
		// Token: 0x06003AB1 RID: 15025 RVA: 0x000D4B28 File Offset: 0x000D2D28
		[MethodImpl(MethodImplOptions.NoOptimization)]
		public static bool WaitAll(Task[] tasks, TimeSpan timeout)
		{
			long num = (long)timeout.TotalMilliseconds;
			if (num < -1L || num > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("timeout");
			}
			return Task.WaitAll(tasks, (int)num);
		}

		/// <summary>Waits for all of the provided <see cref="T:System.Threading.Tasks.Task" /> objects to complete execution within a specified number of milliseconds.</summary>
		/// <returns>true if all of the <see cref="T:System.Threading.Tasks.Task" /> instances completed execution within the allotted time; otherwise, false.</returns>
		/// <param name="tasks">An array of <see cref="T:System.Threading.Tasks.Task" /> instances on which to wait.</param>
		/// <param name="millisecondsTimeout">The number of milliseconds to wait, or <see cref="F:System.Threading.Timeout.Infinite" /> (-1) to wait indefinitely.</param>
		/// <exception cref="T:System.ObjectDisposedException">One or more of the <see cref="T:System.Threading.Tasks.Task" /> objects in <paramref name="tasks" /> has been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="tasks" /> argument is null.</exception>
		/// <exception cref="T:System.AggregateException">At least one of the <see cref="T:System.Threading.Tasks.Task" /> instances was canceled -or- an exception was thrown during the execution of at least one of the <see cref="T:System.Threading.Tasks.Task" /> instances. If a task was canceled, the <see cref="T:System.AggregateException" /> contains an <see cref="T:System.OperationCanceledException" /> in its <see cref="P:System.AggregateException.InnerExceptions" /> collection.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="millisecondsTimeout" /> is a negative number other than -1, which represents an infinite time-out.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="tasks" /> argument contains a null element.</exception>
		// Token: 0x06003AB2 RID: 15026 RVA: 0x000D4B60 File Offset: 0x000D2D60
		[MethodImpl(MethodImplOptions.NoOptimization)]
		public static bool WaitAll(Task[] tasks, int millisecondsTimeout)
		{
			return Task.WaitAll(tasks, millisecondsTimeout, default(CancellationToken));
		}

		/// <summary>Waits for all of the provided cancellable <see cref="T:System.Threading.Tasks.Task" /> objects to complete execution.</summary>
		/// <param name="tasks">An array of <see cref="T:System.Threading.Tasks.Task" /> instances on which to wait.</param>
		/// <param name="cancellationToken">A <see cref="P:System.Threading.Tasks.TaskFactory.CancellationToken" /> to observe while waiting for the tasks to complete.</param>
		/// <exception cref="T:System.OperationCanceledException">The <paramref name="cancellationToken" /> was canceled.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="tasks" /> argument is null.</exception>
		/// <exception cref="T:System.AggregateException">At least one of the <see cref="T:System.Threading.Tasks.Task" /> instances was canceled -or- an exception was thrown during the execution of at least one of the <see cref="T:System.Threading.Tasks.Task" /> instances. If a task was canceled, the <see cref="T:System.AggregateException" /> contains an <see cref="T:System.OperationCanceledException" /> in its <see cref="P:System.AggregateException.InnerExceptions" /> collection.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="tasks" /> argument contains a null element.</exception>
		/// <exception cref="T:System.ObjectDisposedException">One or more of the <see cref="T:System.Threading.Tasks.Task" /> objects in <paramref name="tasks" /> has been disposed.</exception>
		// Token: 0x06003AB3 RID: 15027 RVA: 0x000D4B7D File Offset: 0x000D2D7D
		[MethodImpl(MethodImplOptions.NoOptimization)]
		public static void WaitAll(Task[] tasks, CancellationToken cancellationToken)
		{
			Task.WaitAll(tasks, -1, cancellationToken);
		}

		/// <summary>Waits for all of the provided cancellable <see cref="T:System.Threading.Tasks.Task" /> objects to complete execution within a specified number of milliseconds.</summary>
		/// <returns>true if all of the <see cref="T:System.Threading.Tasks.Task" /> instances completed execution within the allotted time; otherwise, false.</returns>
		/// <param name="tasks">An array of <see cref="T:System.Threading.Tasks.Task" /> instances on which to wait.</param>
		/// <param name="millisecondsTimeout">The number of milliseconds to wait, or <see cref="F:System.Threading.Timeout.Infinite" /> (-1) to wait indefinitely.</param>
		/// <param name="cancellationToken">A <see cref="P:System.Threading.Tasks.TaskFactory.CancellationToken" /> to observe while waiting for the tasks to complete.</param>
		/// <exception cref="T:System.ObjectDisposedException">One or more of the <see cref="T:System.Threading.Tasks.Task" /> objects in <paramref name="tasks" /> has been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="tasks" /> argument is null.</exception>
		/// <exception cref="T:System.AggregateException">At least one of the <see cref="T:System.Threading.Tasks.Task" /> instances was canceled -or- an exception was thrown during the execution of at least one of the <see cref="T:System.Threading.Tasks.Task" /> instances. If a task was canceled, the <see cref="T:System.AggregateException" /> contains an <see cref="T:System.OperationCanceledException" /> in its <see cref="P:System.AggregateException.InnerExceptions" /> collection.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="millisecondsTimeout" /> is a negative number other than -1, which represents an infinite time-out.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="tasks" /> argument contains a null element.</exception>
		/// <exception cref="T:System.OperationCanceledException">The <paramref name="cancellationToken" /> was canceled.</exception>
		// Token: 0x06003AB4 RID: 15028 RVA: 0x000D4B88 File Offset: 0x000D2D88
		[MethodImpl(MethodImplOptions.NoOptimization)]
		public static bool WaitAll(Task[] tasks, int millisecondsTimeout, CancellationToken cancellationToken)
		{
			if (tasks == null)
			{
				throw new ArgumentNullException("tasks");
			}
			if (millisecondsTimeout < -1)
			{
				throw new ArgumentOutOfRangeException("millisecondsTimeout");
			}
			cancellationToken.ThrowIfCancellationRequested();
			List<Exception> list = null;
			List<Task> list2 = null;
			List<Task> list3 = null;
			bool flag = false;
			bool flag2 = false;
			bool flag3 = true;
			for (int i = tasks.Length - 1; i >= 0; i--)
			{
				Task task = tasks[i];
				if (task == null)
				{
					throw new ArgumentException(Environment.GetResourceString("The tasks array included at least one null element."), "tasks");
				}
				bool flag4 = task.IsCompleted;
				if (!flag4)
				{
					if (millisecondsTimeout != -1 || cancellationToken.CanBeCanceled)
					{
						Task.AddToList<Task>(task, ref list2, tasks.Length);
					}
					else
					{
						flag4 = task.WrappedTryRunInline() && task.IsCompleted;
						if (!flag4)
						{
							Task.AddToList<Task>(task, ref list2, tasks.Length);
						}
					}
				}
				if (flag4)
				{
					if (task.IsFaulted)
					{
						flag = true;
					}
					else if (task.IsCanceled)
					{
						flag2 = true;
					}
					if (task.IsWaitNotificationEnabled)
					{
						Task.AddToList<Task>(task, ref list3, 1);
					}
				}
			}
			if (list2 != null)
			{
				flag3 = Task.WaitAllBlockingCore(list2, millisecondsTimeout, cancellationToken);
				if (flag3)
				{
					foreach (Task task2 in list2)
					{
						if (task2.IsFaulted)
						{
							flag = true;
						}
						else if (task2.IsCanceled)
						{
							flag2 = true;
						}
						if (task2.IsWaitNotificationEnabled)
						{
							Task.AddToList<Task>(task2, ref list3, 1);
						}
					}
				}
				GC.KeepAlive(tasks);
			}
			if (flag3 && list3 != null)
			{
				using (List<Task>.Enumerator enumerator = list3.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (enumerator.Current.NotifyDebuggerOfWaitCompletionIfNecessary())
						{
							break;
						}
					}
				}
			}
			if (flag3 && (flag || flag2))
			{
				if (!flag)
				{
					cancellationToken.ThrowIfCancellationRequested();
				}
				foreach (Task task3 in tasks)
				{
					Task.AddExceptionsForCompletedTask(ref list, task3);
				}
				throw new AggregateException(list);
			}
			return flag3;
		}

		// Token: 0x06003AB5 RID: 15029 RVA: 0x000D4D88 File Offset: 0x000D2F88
		private static void AddToList<T>(T item, ref List<T> list, int initSize)
		{
			if (list == null)
			{
				list = new List<T>(initSize);
			}
			list.Add(item);
		}

		// Token: 0x06003AB6 RID: 15030 RVA: 0x000D4DA0 File Offset: 0x000D2FA0
		private static bool WaitAllBlockingCore(List<Task> tasks, int millisecondsTimeout, CancellationToken cancellationToken)
		{
			bool flag = false;
			Task.SetOnCountdownMres setOnCountdownMres = new Task.SetOnCountdownMres(tasks.Count);
			try
			{
				foreach (Task task in tasks)
				{
					task.AddCompletionAction(setOnCountdownMres, true);
				}
				flag = setOnCountdownMres.Wait(millisecondsTimeout, cancellationToken);
			}
			finally
			{
				if (!flag)
				{
					foreach (Task task2 in tasks)
					{
						if (!task2.IsCompleted)
						{
							task2.RemoveContinuation(setOnCountdownMres);
						}
					}
				}
			}
			return flag;
		}

		// Token: 0x06003AB7 RID: 15031 RVA: 0x000D4E5C File Offset: 0x000D305C
		internal static void FastWaitAll(Task[] tasks)
		{
			List<Exception> list = null;
			for (int i = tasks.Length - 1; i >= 0; i--)
			{
				if (!tasks[i].IsCompleted)
				{
					tasks[i].WrappedTryRunInline();
				}
			}
			for (int j = tasks.Length - 1; j >= 0; j--)
			{
				Task task = tasks[j];
				task.SpinThenBlockingWait(-1, default(CancellationToken));
				Task.AddExceptionsForCompletedTask(ref list, task);
			}
			if (list != null)
			{
				throw new AggregateException(list);
			}
		}

		// Token: 0x06003AB8 RID: 15032 RVA: 0x000D4EC8 File Offset: 0x000D30C8
		internal static void AddExceptionsForCompletedTask(ref List<Exception> exceptions, Task t)
		{
			AggregateException exceptions2 = t.GetExceptions(true);
			if (exceptions2 != null)
			{
				t.UpdateExceptionObservedStatus();
				if (exceptions == null)
				{
					exceptions = new List<Exception>(exceptions2.InnerExceptions.Count);
				}
				exceptions.AddRange(exceptions2.InnerExceptions);
			}
		}

		/// <summary>Waits for any of the provided <see cref="T:System.Threading.Tasks.Task" /> objects to complete execution.</summary>
		/// <returns>The index of the completed task in the <paramref name="tasks" /> array argument.</returns>
		/// <param name="tasks">An array of <see cref="T:System.Threading.Tasks.Task" /> instances on which to wait.</param>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Threading.Tasks.Task" /> has been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="tasks" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="tasks" /> argument contains a null element.</exception>
		// Token: 0x06003AB9 RID: 15033 RVA: 0x000D4F09 File Offset: 0x000D3109
		[MethodImpl(MethodImplOptions.NoOptimization)]
		public static int WaitAny(params Task[] tasks)
		{
			return Task.WaitAny(tasks, -1);
		}

		/// <summary>Waits for any of the provided <see cref="T:System.Threading.Tasks.Task" /> objects to complete execution within a specified time interval.</summary>
		/// <returns>The index of the completed task in the <paramref name="tasks" /> array argument, or -1 if the timeout occurred.</returns>
		/// <param name="tasks">An array of <see cref="T:System.Threading.Tasks.Task" /> instances on which to wait.</param>
		/// <param name="timeout">A <see cref="T:System.TimeSpan" /> that represents the number of milliseconds to wait, or a <see cref="T:System.TimeSpan" /> that represents -1 milliseconds to wait indefinitely.</param>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Threading.Tasks.Task" /> has been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="tasks" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="timeout" /> is a negative number other than -1 milliseconds, which represents an infinite time-out -or- timeout is greater than <see cref="F:System.Int32.MaxValue" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="tasks" /> argument contains a null element.</exception>
		// Token: 0x06003ABA RID: 15034 RVA: 0x000D4F14 File Offset: 0x000D3114
		[MethodImpl(MethodImplOptions.NoOptimization)]
		public static int WaitAny(Task[] tasks, TimeSpan timeout)
		{
			long num = (long)timeout.TotalMilliseconds;
			if (num < -1L || num > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("timeout");
			}
			return Task.WaitAny(tasks, (int)num);
		}

		/// <summary>Waits for any of the provided cancellable <see cref="T:System.Threading.Tasks.Task" /> objects to complete execution.</summary>
		/// <returns>The index of the completed task in the <paramref name="tasks" /> array argument.</returns>
		/// <param name="tasks">An array of <see cref="T:System.Threading.Tasks.Task" /> instances on which to wait.</param>
		/// <param name="cancellationToken">A <see cref="P:System.Threading.Tasks.TaskFactory.CancellationToken" /> to observe while waiting for a task to complete.</param>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Threading.Tasks.Task" /> has been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="tasks" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="tasks" /> argument contains a null element.</exception>
		/// <exception cref="T:System.OperationCanceledException">The <paramref name="cancellationToken" /> was canceled.</exception>
		// Token: 0x06003ABB RID: 15035 RVA: 0x000D4F4B File Offset: 0x000D314B
		[MethodImpl(MethodImplOptions.NoOptimization)]
		public static int WaitAny(Task[] tasks, CancellationToken cancellationToken)
		{
			return Task.WaitAny(tasks, -1, cancellationToken);
		}

		/// <summary>Waits for any of the provided <see cref="T:System.Threading.Tasks.Task" /> objects to complete execution within a specified number of milliseconds.</summary>
		/// <returns>The index of the completed task in the <paramref name="tasks" /> array argument, or -1 if the timeout occurred.</returns>
		/// <param name="tasks">An array of <see cref="T:System.Threading.Tasks.Task" /> instances on which to wait.</param>
		/// <param name="millisecondsTimeout">The number of milliseconds to wait, or <see cref="F:System.Threading.Timeout.Infinite" /> (-1) to wait indefinitely.</param>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Threading.Tasks.Task" /> has been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="tasks" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="millisecondsTimeout" /> is a negative number other than -1, which represents an infinite time-out.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="tasks" /> argument contains a null element.</exception>
		// Token: 0x06003ABC RID: 15036 RVA: 0x000D4F58 File Offset: 0x000D3158
		[MethodImpl(MethodImplOptions.NoOptimization)]
		public static int WaitAny(Task[] tasks, int millisecondsTimeout)
		{
			return Task.WaitAny(tasks, millisecondsTimeout, default(CancellationToken));
		}

		/// <summary>Waits for any of the provided cancellable <see cref="T:System.Threading.Tasks.Task" /> objects to complete execution within a specified number of milliseconds.</summary>
		/// <returns>The index of the completed task in the <paramref name="tasks" /> array argument, or -1 if the timeout occurred.</returns>
		/// <param name="tasks">An array of <see cref="T:System.Threading.Tasks.Task" /> instances on which to wait.</param>
		/// <param name="millisecondsTimeout">The number of milliseconds to wait, or <see cref="F:System.Threading.Timeout.Infinite" /> (-1) to wait indefinitely.</param>
		/// <param name="cancellationToken">A <see cref="P:System.Threading.Tasks.TaskFactory.CancellationToken" /> to observe while waiting for a task to complete.</param>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Threading.Tasks.Task" /> has been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="tasks" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="millisecondsTimeout" /> is a negative number other than -1, which represents an infinite time-out.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="tasks" /> argument contains a null element.</exception>
		/// <exception cref="T:System.OperationCanceledException">The <paramref name="cancellationToken" /> was canceled.</exception>
		// Token: 0x06003ABD RID: 15037 RVA: 0x000D4F78 File Offset: 0x000D3178
		[MethodImpl(MethodImplOptions.NoOptimization)]
		public static int WaitAny(Task[] tasks, int millisecondsTimeout, CancellationToken cancellationToken)
		{
			if (tasks == null)
			{
				throw new ArgumentNullException("tasks");
			}
			if (millisecondsTimeout < -1)
			{
				throw new ArgumentOutOfRangeException("millisecondsTimeout");
			}
			cancellationToken.ThrowIfCancellationRequested();
			int num = -1;
			for (int i = 0; i < tasks.Length; i++)
			{
				Task task = tasks[i];
				if (task == null)
				{
					throw new ArgumentException(Environment.GetResourceString("The tasks array included at least one null element."), "tasks");
				}
				if (num == -1 && task.IsCompleted)
				{
					num = i;
				}
			}
			if (num == -1 && tasks.Length != 0)
			{
				Task<Task> task2 = TaskFactory.CommonCWAnyLogic(tasks);
				if (task2.Wait(millisecondsTimeout, cancellationToken))
				{
					num = Array.IndexOf<Task>(tasks, task2.Result);
				}
			}
			GC.KeepAlive(tasks);
			return num;
		}

		/// <summary>Creates a <see cref="T:System.Threading.Tasks.Task`1" /> that's completed successfully with the specified result.</summary>
		/// <returns>The successfully completed task.</returns>
		/// <param name="result">The result to store into the completed task.</param>
		/// <typeparam name="TResult">The type of the result returned by the task.</typeparam>
		// Token: 0x06003ABE RID: 15038 RVA: 0x000D5010 File Offset: 0x000D3210
		public static Task<TResult> FromResult<TResult>(TResult result)
		{
			return new Task<TResult>(result);
		}

		// Token: 0x06003ABF RID: 15039 RVA: 0x000D5018 File Offset: 0x000D3218
		public static Task FromException(Exception exception)
		{
			return Task.FromException<VoidTaskResult>(exception);
		}

		// Token: 0x06003AC0 RID: 15040 RVA: 0x000D5020 File Offset: 0x000D3220
		public static Task<TResult> FromException<TResult>(Exception exception)
		{
			if (exception == null)
			{
				throw new ArgumentNullException("exception");
			}
			Task<TResult> task = new Task<TResult>();
			task.TrySetException(exception);
			return task;
		}

		// Token: 0x06003AC1 RID: 15041 RVA: 0x000D503D File Offset: 0x000D323D
		[FriendAccessAllowed]
		internal static Task FromCancellation(CancellationToken cancellationToken)
		{
			if (!cancellationToken.IsCancellationRequested)
			{
				throw new ArgumentOutOfRangeException("cancellationToken");
			}
			return new Task(true, TaskCreationOptions.None, cancellationToken);
		}

		// Token: 0x06003AC2 RID: 15042 RVA: 0x000D505B File Offset: 0x000D325B
		public static Task FromCanceled(CancellationToken cancellationToken)
		{
			return Task.FromCancellation(cancellationToken);
		}

		// Token: 0x06003AC3 RID: 15043 RVA: 0x000D5064 File Offset: 0x000D3264
		[FriendAccessAllowed]
		internal static Task<TResult> FromCancellation<TResult>(CancellationToken cancellationToken)
		{
			if (!cancellationToken.IsCancellationRequested)
			{
				throw new ArgumentOutOfRangeException("cancellationToken");
			}
			return new Task<TResult>(true, default(TResult), TaskCreationOptions.None, cancellationToken);
		}

		// Token: 0x06003AC4 RID: 15044 RVA: 0x000D5096 File Offset: 0x000D3296
		public static Task<TResult> FromCanceled<TResult>(CancellationToken cancellationToken)
		{
			return Task.FromCancellation<TResult>(cancellationToken);
		}

		// Token: 0x06003AC5 RID: 15045 RVA: 0x000D509E File Offset: 0x000D329E
		internal static Task<TResult> FromCancellation<TResult>(OperationCanceledException exception)
		{
			if (exception == null)
			{
				throw new ArgumentNullException("exception");
			}
			Task<TResult> task = new Task<TResult>();
			task.TrySetCanceled(exception.CancellationToken, exception);
			return task;
		}

		/// <summary>Queues the specified work to run on the ThreadPool and returns a task handle for that work.</summary>
		/// <returns>A task that represents the work queued to execute in the ThreadPool.</returns>
		/// <param name="action">The work to execute asynchronously</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="action" /> parameter was null.</exception>
		// Token: 0x06003AC6 RID: 15046 RVA: 0x000D50C4 File Offset: 0x000D32C4
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static Task Run(Action action)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return Task.InternalStartNew(null, action, null, default(CancellationToken), TaskScheduler.Default, TaskCreationOptions.DenyChildAttach, InternalTaskOptions.None, ref stackCrawlMark);
		}

		/// <summary>Queues the specified work to run on the ThreadPool and returns a task handle for that work.</summary>
		/// <returns>A task that represents the work queued to execute in the ThreadPool.</returns>
		/// <param name="action">The work to execute asynchronously</param>
		/// <param name="cancellationToken">A cancellation token that should be used to cancel the work</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="action" /> parameter was null.</exception>
		/// <exception cref="T:System.Threading.Tasks.TaskCanceledException">The task has been canceled.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Threading.CancellationTokenSource" /> associated with <paramref name="cancellationToken" /> was disposed.</exception>
		// Token: 0x06003AC7 RID: 15047 RVA: 0x000D50F0 File Offset: 0x000D32F0
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static Task Run(Action action, CancellationToken cancellationToken)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return Task.InternalStartNew(null, action, null, cancellationToken, TaskScheduler.Default, TaskCreationOptions.DenyChildAttach, InternalTaskOptions.None, ref stackCrawlMark);
		}

		/// <summary>Queues the specified work to run on the ThreadPool and returns a Task(TResult) handle for that work.</summary>
		/// <returns>A Task(TResult) that represents the work queued to execute in the ThreadPool.</returns>
		/// <param name="function">The work to execute asynchronously</param>
		/// <typeparam name="TResult">The result type of the task.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="function" /> parameter was null.</exception>
		// Token: 0x06003AC8 RID: 15048 RVA: 0x000D5114 File Offset: 0x000D3314
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static Task<TResult> Run<TResult>(Func<TResult> function)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return Task<TResult>.StartNew(null, function, default(CancellationToken), TaskCreationOptions.DenyChildAttach, InternalTaskOptions.None, TaskScheduler.Default, ref stackCrawlMark);
		}

		/// <summary>Queues the specified work to run on the ThreadPool and returns a Task(TResult) handle for that work.</summary>
		/// <returns>A Task(TResult) that represents the work queued to execute in the ThreadPool.</returns>
		/// <param name="function">The work to execute asynchronously</param>
		/// <param name="cancellationToken">A cancellation token that should be used to cancel the work</param>
		/// <typeparam name="TResult">The result type of the task.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="function" /> parameter was null.</exception>
		/// <exception cref="T:System.Threading.Tasks.TaskCanceledException">The task has been canceled.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Threading.CancellationTokenSource" /> associated with <paramref name="cancellationToken" /> was disposed.</exception>
		// Token: 0x06003AC9 RID: 15049 RVA: 0x000D513C File Offset: 0x000D333C
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static Task<TResult> Run<TResult>(Func<TResult> function, CancellationToken cancellationToken)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return Task<TResult>.StartNew(null, function, cancellationToken, TaskCreationOptions.DenyChildAttach, InternalTaskOptions.None, TaskScheduler.Default, ref stackCrawlMark);
		}

		/// <summary>Queues the specified work to run on the ThreadPool and returns a proxy for the  task returned by <paramref name="function" />.</summary>
		/// <returns>A task that represents a proxy for the task returned by <paramref name="function" />.</returns>
		/// <param name="function">The work to execute asynchronously</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="function" /> parameter was null.</exception>
		// Token: 0x06003ACA RID: 15050 RVA: 0x000D515C File Offset: 0x000D335C
		public static Task Run(Func<Task> function)
		{
			return Task.Run(function, default(CancellationToken));
		}

		/// <summary>Queues the specified work to run on the ThreadPool and returns a proxy for the  task returned by <paramref name="function" />.</summary>
		/// <returns>A task that represents a proxy for the task returned by <paramref name="function" />.</returns>
		/// <param name="function">The work to execute asynchronously</param>
		/// <param name="cancellationToken">A cancellation token that should be used to cancel the work</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="function" /> parameter was null.</exception>
		/// <exception cref="T:System.Threading.Tasks.TaskCanceledException">The task has been canceled.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Threading.CancellationTokenSource" /> associated with <paramref name="cancellationToken" /> was disposed.</exception>
		// Token: 0x06003ACB RID: 15051 RVA: 0x000D5178 File Offset: 0x000D3378
		public static Task Run(Func<Task> function, CancellationToken cancellationToken)
		{
			if (function == null)
			{
				throw new ArgumentNullException("function");
			}
			if (AppContextSwitches.ThrowExceptionIfDisposedCancellationTokenSource)
			{
				cancellationToken.ThrowIfSourceDisposed();
			}
			if (cancellationToken.IsCancellationRequested)
			{
				return Task.FromCancellation(cancellationToken);
			}
			return new UnwrapPromise<VoidTaskResult>(Task<Task>.Factory.StartNew(function, cancellationToken, TaskCreationOptions.DenyChildAttach, TaskScheduler.Default), true);
		}

		/// <summary>Queues the specified work to run on the ThreadPool and returns a proxy for the  Task(TResult) returned by <paramref name="function" />.</summary>
		/// <returns>A Task(TResult) that represents a proxy for the Task(TResult) returned by <paramref name="function" />.</returns>
		/// <param name="function">The work to execute asynchronously</param>
		/// <typeparam name="TResult">The type of the result returned by the proxy task.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="function" /> parameter was null.</exception>
		// Token: 0x06003ACC RID: 15052 RVA: 0x000D51CC File Offset: 0x000D33CC
		public static Task<TResult> Run<TResult>(Func<Task<TResult>> function)
		{
			return Task.Run<TResult>(function, default(CancellationToken));
		}

		/// <summary>Queues the specified work to run on the ThreadPool and returns a proxy for the  Task(TResult) returned by <paramref name="function" />.</summary>
		/// <returns>A Task(TResult) that represents a proxy for the Task(TResult) returned by <paramref name="function" />.</returns>
		/// <param name="function">The work to execute asynchronously</param>
		/// <param name="cancellationToken">A cancellation token that should be used to cancel the work</param>
		/// <typeparam name="TResult">The type of the result returned by the proxy task.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="function" /> parameter was null.</exception>
		/// <exception cref="T:System.Threading.Tasks.TaskCanceledException">The task has been canceled.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Threading.CancellationTokenSource" /> associated with <paramref name="cancellationToken" /> was disposed.</exception>
		// Token: 0x06003ACD RID: 15053 RVA: 0x000D51E8 File Offset: 0x000D33E8
		public static Task<TResult> Run<TResult>(Func<Task<TResult>> function, CancellationToken cancellationToken)
		{
			if (function == null)
			{
				throw new ArgumentNullException("function");
			}
			if (AppContextSwitches.ThrowExceptionIfDisposedCancellationTokenSource)
			{
				cancellationToken.ThrowIfSourceDisposed();
			}
			if (cancellationToken.IsCancellationRequested)
			{
				return Task.FromCancellation<TResult>(cancellationToken);
			}
			return new UnwrapPromise<TResult>(Task<Task<TResult>>.Factory.StartNew(function, cancellationToken, TaskCreationOptions.DenyChildAttach, TaskScheduler.Default), true);
		}

		/// <summary>Creates a task that will complete after a time delay.</summary>
		/// <returns>A task that represents the time delay</returns>
		/// <param name="delay">The time span to wait before completing the returned task</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="delay" /> represents a negative time interval or is greater than <see cref="F:System.TimeSpan.MaxValue" />.</exception>
		// Token: 0x06003ACE RID: 15054 RVA: 0x000D523C File Offset: 0x000D343C
		public static Task Delay(TimeSpan delay)
		{
			return Task.Delay(delay, default(CancellationToken));
		}

		/// <summary>Creates a task that will complete after a time delay.</summary>
		/// <returns>A task that represents the time delay</returns>
		/// <param name="delay">The time span to wait before completing the returned task</param>
		/// <param name="cancellationToken">The cancellation token that will be checked prior to completing the returned task</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="delay" /> represents a negative time interval or is greater than <see cref="F:System.TimeSpan.MaxValue" />.</exception>
		/// <exception cref="T:System.Threading.Tasks.TaskCanceledException">The task has been canceled.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The provided <paramref name="cancellationToken" /> has already been disposed.</exception>
		// Token: 0x06003ACF RID: 15055 RVA: 0x000D5258 File Offset: 0x000D3458
		public static Task Delay(TimeSpan delay, CancellationToken cancellationToken)
		{
			long num = (long)delay.TotalMilliseconds;
			if (num < -1L || num > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("delay", Environment.GetResourceString("The value needs to translate in milliseconds to -1 (signifying an infinite timeout), 0 or a positive integer less than or equal to Int32.MaxValue."));
			}
			return Task.Delay((int)num, cancellationToken);
		}

		/// <summary>Creates a task that will complete after a time delay.</summary>
		/// <returns>A task that represents the time delay</returns>
		/// <param name="millisecondsDelay">The number of milliseconds to wait before completing the returned task</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="millisecondsDelay" /> is less than -1.</exception>
		// Token: 0x06003AD0 RID: 15056 RVA: 0x000D529C File Offset: 0x000D349C
		public static Task Delay(int millisecondsDelay)
		{
			return Task.Delay(millisecondsDelay, default(CancellationToken));
		}

		/// <summary>Creates a task that will complete after a time delay.</summary>
		/// <returns>A task that represents the time delay</returns>
		/// <param name="millisecondsDelay">The number of milliseconds to wait before completing the returned task</param>
		/// <param name="cancellationToken">The cancellation token that will be checked prior to completing the returned task</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="millisecondsDelay" /> is less than -1.</exception>
		/// <exception cref="T:System.Threading.Tasks.TaskCanceledException">The task has been canceled.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The provided <paramref name="cancellationToken" /> has already been disposed.</exception>
		// Token: 0x06003AD1 RID: 15057 RVA: 0x000D52B8 File Offset: 0x000D34B8
		public static Task Delay(int millisecondsDelay, CancellationToken cancellationToken)
		{
			if (millisecondsDelay < -1)
			{
				throw new ArgumentOutOfRangeException("millisecondsDelay", Environment.GetResourceString("The value needs to be either -1 (signifying an infinite timeout), 0 or a positive integer."));
			}
			if (cancellationToken.IsCancellationRequested)
			{
				return Task.FromCancellation(cancellationToken);
			}
			if (millisecondsDelay == 0)
			{
				return Task.CompletedTask;
			}
			Task.DelayPromise delayPromise = new Task.DelayPromise(cancellationToken);
			if (cancellationToken.CanBeCanceled)
			{
				delayPromise.Registration = cancellationToken.InternalRegisterWithoutEC(delegate(object state)
				{
					((Task.DelayPromise)state).Complete();
				}, delayPromise);
			}
			if (millisecondsDelay != -1)
			{
				delayPromise.Timer = new Timer(delegate(object state)
				{
					((Task.DelayPromise)state).Complete();
				}, delayPromise, millisecondsDelay, -1);
				delayPromise.Timer.KeepRootedWhileScheduled();
			}
			return delayPromise;
		}

		/// <summary>Creates a task that will complete when all of the supplied tasks have completed.</summary>
		/// <returns>A task that represents the completion of all of the supplied tasks.</returns>
		/// <param name="tasks">The tasks to wait on for completion.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="tasks" /> argument was null.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="tasks" /> collection contained a null task.</exception>
		// Token: 0x06003AD2 RID: 15058 RVA: 0x000D5374 File Offset: 0x000D3574
		public static Task WhenAll(IEnumerable<Task> tasks)
		{
			Task[] array = tasks as Task[];
			if (array != null)
			{
				return Task.WhenAll(array);
			}
			ICollection<Task> collection = tasks as ICollection<Task>;
			if (collection != null)
			{
				int num = 0;
				array = new Task[collection.Count];
				foreach (Task task in tasks)
				{
					if (task == null)
					{
						throw new ArgumentException(Environment.GetResourceString("The tasks argument included a null value."), "tasks");
					}
					array[num++] = task;
				}
				return Task.InternalWhenAll(array);
			}
			if (tasks == null)
			{
				throw new ArgumentNullException("tasks");
			}
			List<Task> list = new List<Task>();
			foreach (Task task2 in tasks)
			{
				if (task2 == null)
				{
					throw new ArgumentException(Environment.GetResourceString("The tasks argument included a null value."), "tasks");
				}
				list.Add(task2);
			}
			return Task.InternalWhenAll(list.ToArray());
		}

		/// <summary>Creates a task that will complete when all of the supplied tasks have completed.</summary>
		/// <returns>A task that represents the completion of all of the supplied tasks.</returns>
		/// <param name="tasks">The tasks to wait on for completion.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="tasks" /> argument was null.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="tasks" /> array contained a null task.</exception>
		// Token: 0x06003AD3 RID: 15059 RVA: 0x000D5484 File Offset: 0x000D3684
		public static Task WhenAll(params Task[] tasks)
		{
			if (tasks == null)
			{
				throw new ArgumentNullException("tasks");
			}
			int num = tasks.Length;
			if (num == 0)
			{
				return Task.InternalWhenAll(tasks);
			}
			Task[] array = new Task[num];
			for (int i = 0; i < num; i++)
			{
				Task task = tasks[i];
				if (task == null)
				{
					throw new ArgumentException(Environment.GetResourceString("The tasks argument included a null value."), "tasks");
				}
				array[i] = task;
			}
			return Task.InternalWhenAll(array);
		}

		// Token: 0x06003AD4 RID: 15060 RVA: 0x000D54E6 File Offset: 0x000D36E6
		private static Task InternalWhenAll(Task[] tasks)
		{
			if (tasks.Length != 0)
			{
				return new Task.WhenAllPromise(tasks);
			}
			return Task.CompletedTask;
		}

		/// <summary>Creates a task that will complete when all of the supplied tasks have completed.</summary>
		/// <returns>A task that represents the completion of all of the supplied tasks.</returns>
		/// <param name="tasks">The tasks to wait on for completion.</param>
		/// <typeparam name="TResult">The type of the completed task.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="tasks" /> argument was null.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="tasks" /> collection contained a null task.</exception>
		// Token: 0x06003AD5 RID: 15061 RVA: 0x000D54F8 File Offset: 0x000D36F8
		public static Task<TResult[]> WhenAll<TResult>(IEnumerable<Task<TResult>> tasks)
		{
			Task<TResult>[] array = tasks as Task<TResult>[];
			if (array != null)
			{
				return Task.WhenAll<TResult>(array);
			}
			ICollection<Task<TResult>> collection = tasks as ICollection<Task<TResult>>;
			if (collection != null)
			{
				int num = 0;
				array = new Task<TResult>[collection.Count];
				foreach (Task<TResult> task in tasks)
				{
					if (task == null)
					{
						throw new ArgumentException(Environment.GetResourceString("The tasks argument included a null value."), "tasks");
					}
					array[num++] = task;
				}
				return Task.InternalWhenAll<TResult>(array);
			}
			if (tasks == null)
			{
				throw new ArgumentNullException("tasks");
			}
			List<Task<TResult>> list = new List<Task<TResult>>();
			foreach (Task<TResult> task2 in tasks)
			{
				if (task2 == null)
				{
					throw new ArgumentException(Environment.GetResourceString("The tasks argument included a null value."), "tasks");
				}
				list.Add(task2);
			}
			return Task.InternalWhenAll<TResult>(list.ToArray());
		}

		/// <summary>Creates a task that will complete when all of the supplied tasks have completed.</summary>
		/// <returns>A task that represents the completion of all of the supplied tasks.</returns>
		/// <param name="tasks">The tasks to wait on for completion.</param>
		/// <typeparam name="TResult">The type of the completed task.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="tasks" /> argument was null.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="tasks" /> array contained a null task.</exception>
		// Token: 0x06003AD6 RID: 15062 RVA: 0x000D5608 File Offset: 0x000D3808
		public static Task<TResult[]> WhenAll<TResult>(params Task<TResult>[] tasks)
		{
			if (tasks == null)
			{
				throw new ArgumentNullException("tasks");
			}
			int num = tasks.Length;
			if (num == 0)
			{
				return Task.InternalWhenAll<TResult>(tasks);
			}
			Task<TResult>[] array = new Task<TResult>[num];
			for (int i = 0; i < num; i++)
			{
				Task<TResult> task = tasks[i];
				if (task == null)
				{
					throw new ArgumentException(Environment.GetResourceString("The tasks argument included a null value."), "tasks");
				}
				array[i] = task;
			}
			return Task.InternalWhenAll<TResult>(array);
		}

		// Token: 0x06003AD7 RID: 15063 RVA: 0x000D566C File Offset: 0x000D386C
		private static Task<TResult[]> InternalWhenAll<TResult>(Task<TResult>[] tasks)
		{
			if (tasks.Length != 0)
			{
				return new Task.WhenAllPromise<TResult>(tasks);
			}
			return new Task<TResult[]>(false, new TResult[0], TaskCreationOptions.None, default(CancellationToken));
		}

		/// <summary>Creates a task that will complete when any of the supplied tasks have completed.</summary>
		/// <returns>A task that represents the completion of one of the supplied tasks.  The return task's Result is the task that completed.</returns>
		/// <param name="tasks">The tasks to wait on for completion.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="tasks" /> argument was null.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="tasks" /> array contained a null task, or was empty.</exception>
		// Token: 0x06003AD8 RID: 15064 RVA: 0x000D569C File Offset: 0x000D389C
		public static Task<Task> WhenAny(params Task[] tasks)
		{
			if (tasks == null)
			{
				throw new ArgumentNullException("tasks");
			}
			if (tasks.Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("The tasks argument contains no tasks."), "tasks");
			}
			int num = tasks.Length;
			Task[] array = new Task[num];
			for (int i = 0; i < num; i++)
			{
				Task task = tasks[i];
				if (task == null)
				{
					throw new ArgumentException(Environment.GetResourceString("The tasks argument included a null value."), "tasks");
				}
				array[i] = task;
			}
			return TaskFactory.CommonCWAnyLogic(array);
		}

		/// <summary>Creates a task that will complete when any of the supplied tasks have completed.</summary>
		/// <returns>A task that represents the completion of one of the supplied tasks.  The return task's Result is the task that completed.</returns>
		/// <param name="tasks">The tasks to wait on for completion.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="tasks" /> argument was null.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="tasks" /> array contained a null task, or was empty.</exception>
		// Token: 0x06003AD9 RID: 15065 RVA: 0x000D5710 File Offset: 0x000D3910
		public static Task<Task> WhenAny(IEnumerable<Task> tasks)
		{
			if (tasks == null)
			{
				throw new ArgumentNullException("tasks");
			}
			List<Task> list = new List<Task>();
			foreach (Task task in tasks)
			{
				if (task == null)
				{
					throw new ArgumentException(Environment.GetResourceString("The tasks argument included a null value."), "tasks");
				}
				list.Add(task);
			}
			if (list.Count == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("The tasks argument contains no tasks."), "tasks");
			}
			return TaskFactory.CommonCWAnyLogic(list);
		}

		/// <summary>Creates a task that will complete when any of the supplied tasks have completed.</summary>
		/// <returns>A task that represents the completion of one of the supplied tasks.  The return task's Result is the task that completed.</returns>
		/// <param name="tasks">The tasks to wait on for completion.</param>
		/// <typeparam name="TResult">The type of the completed task.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="tasks" /> argument was null.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="tasks" /> array contained a null task, or was empty.</exception>
		// Token: 0x06003ADA RID: 15066 RVA: 0x000D57A8 File Offset: 0x000D39A8
		public static Task<Task<TResult>> WhenAny<TResult>(params Task<TResult>[] tasks)
		{
			return Task.WhenAny(tasks).ContinueWith<Task<TResult>>(Task<TResult>.TaskWhenAnyCast, default(CancellationToken), TaskContinuationOptions.DenyChildAttach | TaskContinuationOptions.ExecuteSynchronously, TaskScheduler.Default);
		}

		/// <summary>Creates a task that will complete when any of the supplied tasks have completed.</summary>
		/// <returns>A task that represents the completion of one of the supplied tasks.  The return task's Result is the task that completed.</returns>
		/// <param name="tasks">The tasks to wait on for completion.</param>
		/// <typeparam name="TResult">The type of the completed task.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="tasks" /> argument was null.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="tasks" /> array contained a null task, or was empty.</exception>
		// Token: 0x06003ADB RID: 15067 RVA: 0x000D57D8 File Offset: 0x000D39D8
		public static Task<Task<TResult>> WhenAny<TResult>(IEnumerable<Task<TResult>> tasks)
		{
			return Task.WhenAny(tasks).ContinueWith<Task<TResult>>(Task<TResult>.TaskWhenAnyCast, default(CancellationToken), TaskContinuationOptions.DenyChildAttach | TaskContinuationOptions.ExecuteSynchronously, TaskScheduler.Default);
		}

		// Token: 0x06003ADC RID: 15068 RVA: 0x000D5808 File Offset: 0x000D3A08
		[FriendAccessAllowed]
		internal static Task<TResult> CreateUnwrapPromise<TResult>(Task outerTask, bool lookForOce)
		{
			return new UnwrapPromise<TResult>(outerTask, lookForOce);
		}

		// Token: 0x06003ADD RID: 15069 RVA: 0x000D5811 File Offset: 0x000D3A11
		internal virtual Delegate[] GetDelegateContinuationsForDebugger()
		{
			if (this.m_continuationObject != this)
			{
				return Task.GetDelegatesFromContinuationObject(this.m_continuationObject);
			}
			return null;
		}

		// Token: 0x06003ADE RID: 15070 RVA: 0x000D5830 File Offset: 0x000D3A30
		internal static Delegate[] GetDelegatesFromContinuationObject(object continuationObject)
		{
			if (continuationObject != null)
			{
				Action action = continuationObject as Action;
				if (action != null)
				{
					return new Delegate[] { AsyncMethodBuilderCore.TryGetStateMachineForDebugger(action) };
				}
				TaskContinuation taskContinuation = continuationObject as TaskContinuation;
				if (taskContinuation != null)
				{
					return taskContinuation.GetDelegateContinuationsForDebugger();
				}
				Task task = continuationObject as Task;
				if (task != null)
				{
					Delegate[] delegateContinuationsForDebugger = task.GetDelegateContinuationsForDebugger();
					if (delegateContinuationsForDebugger != null)
					{
						return delegateContinuationsForDebugger;
					}
				}
				ITaskCompletionAction taskCompletionAction = continuationObject as ITaskCompletionAction;
				if (taskCompletionAction != null)
				{
					return new Delegate[]
					{
						new Action<Task>(taskCompletionAction.Invoke)
					};
				}
				List<object> list = continuationObject as List<object>;
				if (list != null)
				{
					List<Delegate> list2 = new List<Delegate>();
					foreach (object obj in list)
					{
						Delegate[] delegatesFromContinuationObject = Task.GetDelegatesFromContinuationObject(obj);
						if (delegatesFromContinuationObject != null)
						{
							foreach (Delegate @delegate in delegatesFromContinuationObject)
							{
								if (@delegate != null)
								{
									list2.Add(@delegate);
								}
							}
						}
					}
					return list2.ToArray();
				}
			}
			return null;
		}

		// Token: 0x06003ADF RID: 15071 RVA: 0x000D5938 File Offset: 0x000D3B38
		private static Task GetActiveTaskFromId(int taskId)
		{
			Task task = null;
			Task.s_currentActiveTasks.TryGetValue(taskId, out task);
			return task;
		}

		// Token: 0x06003AE0 RID: 15072 RVA: 0x000D5956 File Offset: 0x000D3B56
		private static Task[] GetActiveTasks()
		{
			return new List<Task>(Task.s_currentActiveTasks.Values).ToArray();
		}

		// Token: 0x04001E74 RID: 7796
		[ThreadStatic]
		internal static Task t_currentTask;

		// Token: 0x04001E75 RID: 7797
		[ThreadStatic]
		private static StackGuard t_stackGuard;

		// Token: 0x04001E76 RID: 7798
		internal static int s_taskIdCounter;

		// Token: 0x04001E77 RID: 7799
		private static readonly TaskFactory s_factory = new TaskFactory();

		// Token: 0x04001E78 RID: 7800
		private volatile int m_taskId;

		// Token: 0x04001E79 RID: 7801
		internal object m_action;

		// Token: 0x04001E7A RID: 7802
		internal object m_stateObject;

		// Token: 0x04001E7B RID: 7803
		internal TaskScheduler m_taskScheduler;

		// Token: 0x04001E7C RID: 7804
		internal readonly Task m_parent;

		// Token: 0x04001E7D RID: 7805
		internal volatile int m_stateFlags;

		// Token: 0x04001E7E RID: 7806
		private const int OptionsMask = 65535;

		// Token: 0x04001E7F RID: 7807
		internal const int TASK_STATE_STARTED = 65536;

		// Token: 0x04001E80 RID: 7808
		internal const int TASK_STATE_DELEGATE_INVOKED = 131072;

		// Token: 0x04001E81 RID: 7809
		internal const int TASK_STATE_DISPOSED = 262144;

		// Token: 0x04001E82 RID: 7810
		internal const int TASK_STATE_EXCEPTIONOBSERVEDBYPARENT = 524288;

		// Token: 0x04001E83 RID: 7811
		internal const int TASK_STATE_CANCELLATIONACKNOWLEDGED = 1048576;

		// Token: 0x04001E84 RID: 7812
		internal const int TASK_STATE_FAULTED = 2097152;

		// Token: 0x04001E85 RID: 7813
		internal const int TASK_STATE_CANCELED = 4194304;

		// Token: 0x04001E86 RID: 7814
		internal const int TASK_STATE_WAITING_ON_CHILDREN = 8388608;

		// Token: 0x04001E87 RID: 7815
		internal const int TASK_STATE_RAN_TO_COMPLETION = 16777216;

		// Token: 0x04001E88 RID: 7816
		internal const int TASK_STATE_WAITINGFORACTIVATION = 33554432;

		// Token: 0x04001E89 RID: 7817
		internal const int TASK_STATE_COMPLETION_RESERVED = 67108864;

		// Token: 0x04001E8A RID: 7818
		internal const int TASK_STATE_THREAD_WAS_ABORTED = 134217728;

		// Token: 0x04001E8B RID: 7819
		internal const int TASK_STATE_WAIT_COMPLETION_NOTIFICATION = 268435456;

		// Token: 0x04001E8C RID: 7820
		internal const int TASK_STATE_EXECUTIONCONTEXT_IS_NULL = 536870912;

		// Token: 0x04001E8D RID: 7821
		internal const int TASK_STATE_TASKSCHEDULED_WAS_FIRED = 1073741824;

		// Token: 0x04001E8E RID: 7822
		private const int TASK_STATE_COMPLETED_MASK = 23068672;

		// Token: 0x04001E8F RID: 7823
		private const int CANCELLATION_REQUESTED = 1;

		// Token: 0x04001E90 RID: 7824
		private volatile object m_continuationObject;

		// Token: 0x04001E91 RID: 7825
		private static readonly object s_taskCompletionSentinel = new object();

		// Token: 0x04001E92 RID: 7826
		[FriendAccessAllowed]
		internal static bool s_asyncDebuggingEnabled;

		// Token: 0x04001E93 RID: 7827
		private static readonly Dictionary<int, Task> s_currentActiveTasks = new Dictionary<int, Task>();

		// Token: 0x04001E94 RID: 7828
		private static readonly object s_activeTasksLock = new object();

		// Token: 0x04001E95 RID: 7829
		internal volatile Task.ContingentProperties m_contingentProperties;

		// Token: 0x04001E96 RID: 7830
		private static readonly Action<object> s_taskCancelCallback = new Action<object>(Task.TaskCancelCallback);

		// Token: 0x04001E97 RID: 7831
		private static readonly Func<Task.ContingentProperties> s_createContingentProperties = () => new Task.ContingentProperties();

		// Token: 0x04001E98 RID: 7832
		private static Task s_completedTask;

		// Token: 0x04001E99 RID: 7833
		private static readonly Predicate<Task> s_IsExceptionObservedByParentPredicate = (Task t) => t.IsExceptionObservedByParent;

		// Token: 0x04001E9A RID: 7834
		[SecurityCritical]
		private static ContextCallback s_ecCallback;

		// Token: 0x04001E9B RID: 7835
		private static readonly Predicate<object> s_IsTaskContinuationNullPredicate = (object tc) => tc == null;

		// Token: 0x020004F7 RID: 1271
		internal class ContingentProperties
		{
			// Token: 0x06003AE2 RID: 15074 RVA: 0x000D59F4 File Offset: 0x000D3BF4
			internal void SetCompleted()
			{
				ManualResetEventSlim completionEvent = this.m_completionEvent;
				if (completionEvent != null)
				{
					completionEvent.Set();
				}
			}

			// Token: 0x06003AE3 RID: 15075 RVA: 0x000D5A14 File Offset: 0x000D3C14
			internal void DeregisterCancellationCallback()
			{
				if (this.m_cancellationRegistration != null)
				{
					try
					{
						this.m_cancellationRegistration.Value.Dispose();
					}
					catch (ObjectDisposedException)
					{
					}
					this.m_cancellationRegistration = null;
				}
			}

			// Token: 0x04001E9C RID: 7836
			internal ExecutionContext m_capturedContext;

			// Token: 0x04001E9D RID: 7837
			internal volatile ManualResetEventSlim m_completionEvent;

			// Token: 0x04001E9E RID: 7838
			internal volatile TaskExceptionHolder m_exceptionsHolder;

			// Token: 0x04001E9F RID: 7839
			internal CancellationToken m_cancellationToken;

			// Token: 0x04001EA0 RID: 7840
			internal Shared<CancellationTokenRegistration> m_cancellationRegistration;

			// Token: 0x04001EA1 RID: 7841
			internal volatile int m_internalCancellationRequested;

			// Token: 0x04001EA2 RID: 7842
			internal volatile int m_completionCountdown = 1;

			// Token: 0x04001EA3 RID: 7843
			internal volatile List<Task> m_exceptionalChildren;
		}

		// Token: 0x020004F8 RID: 1272
		private sealed class SetOnInvokeMres : ManualResetEventSlim, ITaskCompletionAction
		{
			// Token: 0x06003AE5 RID: 15077 RVA: 0x000D5A69 File Offset: 0x000D3C69
			internal SetOnInvokeMres()
				: base(false, 0)
			{
			}

			// Token: 0x06003AE6 RID: 15078 RVA: 0x000D5A73 File Offset: 0x000D3C73
			public void Invoke(Task completingTask)
			{
				base.Set();
			}
		}

		// Token: 0x020004F9 RID: 1273
		private sealed class SetOnCountdownMres : ManualResetEventSlim, ITaskCompletionAction
		{
			// Token: 0x06003AE7 RID: 15079 RVA: 0x000D5A7B File Offset: 0x000D3C7B
			internal SetOnCountdownMres(int count)
			{
				this._count = count;
			}

			// Token: 0x06003AE8 RID: 15080 RVA: 0x000D5A8A File Offset: 0x000D3C8A
			public void Invoke(Task completingTask)
			{
				if (Interlocked.Decrement(ref this._count) == 0)
				{
					base.Set();
				}
			}

			// Token: 0x04001EA4 RID: 7844
			private int _count;
		}

		// Token: 0x020004FA RID: 1274
		private sealed class DelayPromise : Task<VoidTaskResult>
		{
			// Token: 0x06003AE9 RID: 15081 RVA: 0x000D5A9F File Offset: 0x000D3C9F
			internal DelayPromise(CancellationToken token)
			{
				this.Token = token;
				if (AsyncCausalityTracer.LoggingOn)
				{
					AsyncCausalityTracer.TraceOperationCreation(CausalityTraceLevel.Required, base.Id, "Task.Delay", 0UL);
				}
				if (Task.s_asyncDebuggingEnabled)
				{
					Task.AddToActiveTasks(this);
				}
			}

			// Token: 0x06003AEA RID: 15082 RVA: 0x000D5AD8 File Offset: 0x000D3CD8
			internal void Complete()
			{
				bool flag;
				if (this.Token.IsCancellationRequested)
				{
					flag = base.TrySetCanceled(this.Token);
				}
				else
				{
					if (AsyncCausalityTracer.LoggingOn)
					{
						AsyncCausalityTracer.TraceOperationCompletion(CausalityTraceLevel.Required, base.Id, AsyncCausalityStatus.Completed);
					}
					if (Task.s_asyncDebuggingEnabled)
					{
						Task.RemoveFromActiveTasks(base.Id);
					}
					flag = base.TrySetResult(default(VoidTaskResult));
				}
				if (flag)
				{
					if (this.Timer != null)
					{
						this.Timer.Dispose();
					}
					this.Registration.Dispose();
				}
			}

			// Token: 0x04001EA5 RID: 7845
			internal readonly CancellationToken Token;

			// Token: 0x04001EA6 RID: 7846
			internal CancellationTokenRegistration Registration;

			// Token: 0x04001EA7 RID: 7847
			internal Timer Timer;
		}

		// Token: 0x020004FB RID: 1275
		private sealed class WhenAllPromise : Task<VoidTaskResult>, ITaskCompletionAction
		{
			// Token: 0x06003AEB RID: 15083 RVA: 0x000D5B5C File Offset: 0x000D3D5C
			internal WhenAllPromise(Task[] tasks)
			{
				if (AsyncCausalityTracer.LoggingOn)
				{
					AsyncCausalityTracer.TraceOperationCreation(CausalityTraceLevel.Required, base.Id, "Task.WhenAll", 0UL);
				}
				if (Task.s_asyncDebuggingEnabled)
				{
					Task.AddToActiveTasks(this);
				}
				this.m_tasks = tasks;
				this.m_count = tasks.Length;
				foreach (Task task in tasks)
				{
					if (task.IsCompleted)
					{
						this.Invoke(task);
					}
					else
					{
						task.AddCompletionAction(this);
					}
				}
			}

			// Token: 0x06003AEC RID: 15084 RVA: 0x000D5BD4 File Offset: 0x000D3DD4
			public void Invoke(Task completedTask)
			{
				if (AsyncCausalityTracer.LoggingOn)
				{
					AsyncCausalityTracer.TraceOperationRelation(CausalityTraceLevel.Important, base.Id, CausalityRelation.Join);
				}
				if (Interlocked.Decrement(ref this.m_count) == 0)
				{
					List<ExceptionDispatchInfo> list = null;
					Task task = null;
					for (int i = 0; i < this.m_tasks.Length; i++)
					{
						Task task2 = this.m_tasks[i];
						if (task2.IsFaulted)
						{
							if (list == null)
							{
								list = new List<ExceptionDispatchInfo>();
							}
							list.AddRange(task2.GetExceptionDispatchInfos());
						}
						else if (task2.IsCanceled && task == null)
						{
							task = task2;
						}
						if (task2.IsWaitNotificationEnabled)
						{
							base.SetNotificationForWaitCompletion(true);
						}
						else
						{
							this.m_tasks[i] = null;
						}
					}
					if (list != null)
					{
						base.TrySetException(list);
						return;
					}
					if (task != null)
					{
						base.TrySetCanceled(task.CancellationToken, task.GetCancellationExceptionDispatchInfo());
						return;
					}
					if (AsyncCausalityTracer.LoggingOn)
					{
						AsyncCausalityTracer.TraceOperationCompletion(CausalityTraceLevel.Required, base.Id, AsyncCausalityStatus.Completed);
					}
					if (Task.s_asyncDebuggingEnabled)
					{
						Task.RemoveFromActiveTasks(base.Id);
					}
					base.TrySetResult(default(VoidTaskResult));
				}
			}

			// Token: 0x170009AE RID: 2478
			// (get) Token: 0x06003AED RID: 15085 RVA: 0x000D5CC5 File Offset: 0x000D3EC5
			internal override bool ShouldNotifyDebuggerOfWaitCompletion
			{
				get
				{
					return base.ShouldNotifyDebuggerOfWaitCompletion && Task.AnyTaskRequiresNotifyDebuggerOfWaitCompletion(this.m_tasks);
				}
			}

			// Token: 0x04001EA8 RID: 7848
			private readonly Task[] m_tasks;

			// Token: 0x04001EA9 RID: 7849
			private int m_count;
		}

		// Token: 0x020004FC RID: 1276
		private sealed class WhenAllPromise<T> : Task<T[]>, ITaskCompletionAction
		{
			// Token: 0x06003AEE RID: 15086 RVA: 0x000D5CDC File Offset: 0x000D3EDC
			internal WhenAllPromise(Task<T>[] tasks)
			{
				this.m_tasks = tasks;
				this.m_count = tasks.Length;
				if (AsyncCausalityTracer.LoggingOn)
				{
					AsyncCausalityTracer.TraceOperationCreation(CausalityTraceLevel.Required, base.Id, "Task.WhenAll", 0UL);
				}
				if (Task.s_asyncDebuggingEnabled)
				{
					Task.AddToActiveTasks(this);
				}
				foreach (Task<T> task in tasks)
				{
					if (task.IsCompleted)
					{
						this.Invoke(task);
					}
					else
					{
						task.AddCompletionAction(this);
					}
				}
			}

			// Token: 0x06003AEF RID: 15087 RVA: 0x000D5D54 File Offset: 0x000D3F54
			public void Invoke(Task ignored)
			{
				if (AsyncCausalityTracer.LoggingOn)
				{
					AsyncCausalityTracer.TraceOperationRelation(CausalityTraceLevel.Important, base.Id, CausalityRelation.Join);
				}
				if (Interlocked.Decrement(ref this.m_count) == 0)
				{
					T[] array = new T[this.m_tasks.Length];
					List<ExceptionDispatchInfo> list = null;
					Task task = null;
					for (int i = 0; i < this.m_tasks.Length; i++)
					{
						Task<T> task2 = this.m_tasks[i];
						if (task2.IsFaulted)
						{
							if (list == null)
							{
								list = new List<ExceptionDispatchInfo>();
							}
							list.AddRange(task2.GetExceptionDispatchInfos());
						}
						else if (task2.IsCanceled)
						{
							if (task == null)
							{
								task = task2;
							}
						}
						else
						{
							array[i] = task2.GetResultCore(false);
						}
						if (task2.IsWaitNotificationEnabled)
						{
							base.SetNotificationForWaitCompletion(true);
						}
						else
						{
							this.m_tasks[i] = null;
						}
					}
					if (list != null)
					{
						base.TrySetException(list);
						return;
					}
					if (task != null)
					{
						base.TrySetCanceled(task.CancellationToken, task.GetCancellationExceptionDispatchInfo());
						return;
					}
					if (AsyncCausalityTracer.LoggingOn)
					{
						AsyncCausalityTracer.TraceOperationCompletion(CausalityTraceLevel.Required, base.Id, AsyncCausalityStatus.Completed);
					}
					if (Task.s_asyncDebuggingEnabled)
					{
						Task.RemoveFromActiveTasks(base.Id);
					}
					base.TrySetResult(array);
				}
			}

			// Token: 0x170009AF RID: 2479
			// (get) Token: 0x06003AF0 RID: 15088 RVA: 0x000D5E61 File Offset: 0x000D4061
			internal override bool ShouldNotifyDebuggerOfWaitCompletion
			{
				get
				{
					return base.ShouldNotifyDebuggerOfWaitCompletion && Task.AnyTaskRequiresNotifyDebuggerOfWaitCompletion(this.m_tasks);
				}
			}

			// Token: 0x04001EAA RID: 7850
			private readonly Task<T>[] m_tasks;

			// Token: 0x04001EAB RID: 7851
			private int m_count;
		}
	}
}
