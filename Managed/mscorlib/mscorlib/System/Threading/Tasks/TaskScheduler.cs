using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Security;
using System.Security.Permissions;

namespace System.Threading.Tasks
{
	/// <summary>Represents an object that handles the low-level work of queuing tasks onto threads.</summary>
	// Token: 0x0200051E RID: 1310
	[DebuggerDisplay("Id={Id}")]
	[DebuggerTypeProxy(typeof(TaskScheduler.SystemThreadingTasks_TaskSchedulerDebugView))]
	[PermissionSet(SecurityAction.InheritanceDemand, Unrestricted = true)]
	[HostProtection(SecurityAction.LinkDemand, Synchronization = true, ExternalThreading = true)]
	public abstract class TaskScheduler
	{
		/// <summary>Queues a <see cref="T:System.Threading.Tasks.Task" /> to the scheduler. </summary>
		/// <param name="task">The <see cref="T:System.Threading.Tasks.Task" /> to be queued.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="task" /> argument is null.</exception>
		// Token: 0x06003BD4 RID: 15316
		[SecurityCritical]
		protected internal abstract void QueueTask(Task task);

		/// <summary>Determines whether the provided <see cref="T:System.Threading.Tasks.Task" /> can be executed synchronously in this call, and if it can, executes it.</summary>
		/// <returns>A Boolean value indicating whether the task was executed inline.</returns>
		/// <param name="task">The <see cref="T:System.Threading.Tasks.Task" /> to be executed.</param>
		/// <param name="taskWasPreviouslyQueued">A Boolean denoting whether or not task has previously been queued. If this parameter is True, then the task may have been previously queued (scheduled); if False, then the task is known not to have been queued, and this call is being made in order to execute the task inline without queuing it.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="task" /> argument is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <paramref name="task" /> was already executed.</exception>
		// Token: 0x06003BD5 RID: 15317
		[SecurityCritical]
		protected abstract bool TryExecuteTaskInline(Task task, bool taskWasPreviouslyQueued);

		/// <summary>For debugger support only, generates an enumerable of <see cref="T:System.Threading.Tasks.Task" /> instances currently queued to the scheduler waiting to be executed.</summary>
		/// <returns>An enumerable that allows a debugger to traverse the tasks currently queued to this scheduler.</returns>
		/// <exception cref="T:System.NotSupportedException">This scheduler is unable to generate a list of queued tasks at this time.</exception>
		// Token: 0x06003BD6 RID: 15318
		[SecurityCritical]
		protected abstract IEnumerable<Task> GetScheduledTasks();

		/// <summary>Indicates the maximum concurrency level this <see cref="T:System.Threading.Tasks.TaskScheduler" /> is able to support.</summary>
		/// <returns>Returns an integer that represents the maximum concurrency level. The default scheduler returns <see cref="F:System.Int32.MaxValue" />.</returns>
		// Token: 0x170009C4 RID: 2500
		// (get) Token: 0x06003BD7 RID: 15319 RVA: 0x000D8656 File Offset: 0x000D6856
		public virtual int MaximumConcurrencyLevel
		{
			get
			{
				return int.MaxValue;
			}
		}

		// Token: 0x06003BD8 RID: 15320 RVA: 0x000D8660 File Offset: 0x000D6860
		[SecuritySafeCritical]
		internal bool TryRunInline(Task task, bool taskWasPreviouslyQueued)
		{
			TaskScheduler executingTaskScheduler = task.ExecutingTaskScheduler;
			if (executingTaskScheduler != this && executingTaskScheduler != null)
			{
				return executingTaskScheduler.TryRunInline(task, taskWasPreviouslyQueued);
			}
			StackGuard currentStackGuard;
			if (executingTaskScheduler == null || task.m_action == null || task.IsDelegateInvoked || task.IsCanceled || !(currentStackGuard = Task.CurrentStackGuard).TryBeginInliningScope())
			{
				return false;
			}
			bool flag = false;
			try
			{
				task.FireTaskScheduledIfNeeded(this);
				flag = this.TryExecuteTaskInline(task, taskWasPreviouslyQueued);
			}
			finally
			{
				currentStackGuard.EndInliningScope();
			}
			if (flag && !task.IsDelegateInvoked && !task.IsCanceled)
			{
				throw new InvalidOperationException(Environment.GetResourceString("The TryExecuteTaskInline call to the underlying scheduler succeeded, but the task body was not invoked."));
			}
			return flag;
		}

		/// <summary>Attempts to dequeue a <see cref="T:System.Threading.Tasks.Task" /> that was previously queued to this scheduler.</summary>
		/// <returns>A Boolean denoting whether the <paramref name="task" /> argument was successfully dequeued.</returns>
		/// <param name="task">The <see cref="T:System.Threading.Tasks.Task" /> to be dequeued.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="task" /> argument is null.</exception>
		// Token: 0x06003BD9 RID: 15321 RVA: 0x00015ED5 File Offset: 0x000140D5
		[SecurityCritical]
		protected internal virtual bool TryDequeue(Task task)
		{
			return false;
		}

		// Token: 0x06003BDA RID: 15322 RVA: 0x00002194 File Offset: 0x00000394
		internal virtual void NotifyWorkItemProgress()
		{
		}

		// Token: 0x170009C5 RID: 2501
		// (get) Token: 0x06003BDB RID: 15323 RVA: 0x00003B29 File Offset: 0x00001D29
		internal virtual bool RequiresAtomicStartTransition
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06003BDC RID: 15324 RVA: 0x000D8700 File Offset: 0x000D6900
		[SecurityCritical]
		internal void InternalQueueTask(Task task)
		{
			task.FireTaskScheduledIfNeeded(this);
			this.QueueTask(task);
		}

		/// <summary>Initializes the <see cref="T:System.Threading.Tasks.TaskScheduler" />.</summary>
		// Token: 0x06003BDD RID: 15325 RVA: 0x000D8711 File Offset: 0x000D6911
		protected TaskScheduler()
		{
			if (Debugger.IsAttached)
			{
				this.AddToActiveTaskSchedulers();
			}
		}

		// Token: 0x06003BDE RID: 15326 RVA: 0x000D8728 File Offset: 0x000D6928
		private void AddToActiveTaskSchedulers()
		{
			ConditionalWeakTable<TaskScheduler, object> conditionalWeakTable = TaskScheduler.s_activeTaskSchedulers;
			if (conditionalWeakTable == null)
			{
				Interlocked.CompareExchange<ConditionalWeakTable<TaskScheduler, object>>(ref TaskScheduler.s_activeTaskSchedulers, new ConditionalWeakTable<TaskScheduler, object>(), null);
				conditionalWeakTable = TaskScheduler.s_activeTaskSchedulers;
			}
			conditionalWeakTable.Add(this, null);
		}

		/// <summary>Gets the default <see cref="T:System.Threading.Tasks.TaskScheduler" /> instance that is provided by the .NET Framework.</summary>
		/// <returns>Returns the default <see cref="T:System.Threading.Tasks.TaskScheduler" /> instance.</returns>
		// Token: 0x170009C6 RID: 2502
		// (get) Token: 0x06003BDF RID: 15327 RVA: 0x000D875D File Offset: 0x000D695D
		public static TaskScheduler Default
		{
			get
			{
				return TaskScheduler.s_defaultTaskScheduler;
			}
		}

		/// <summary>Gets the <see cref="T:System.Threading.Tasks.TaskScheduler" /> associated with the currently executing task.</summary>
		/// <returns>Returns the <see cref="T:System.Threading.Tasks.TaskScheduler" /> associated with the currently executing task.</returns>
		// Token: 0x170009C7 RID: 2503
		// (get) Token: 0x06003BE0 RID: 15328 RVA: 0x000D8764 File Offset: 0x000D6964
		public static TaskScheduler Current
		{
			get
			{
				return TaskScheduler.InternalCurrent ?? TaskScheduler.Default;
			}
		}

		// Token: 0x170009C8 RID: 2504
		// (get) Token: 0x06003BE1 RID: 15329 RVA: 0x000D8774 File Offset: 0x000D6974
		internal static bool IsDefault
		{
			get
			{
				return TaskScheduler.Current == TaskScheduler.Default;
			}
		}

		// Token: 0x170009C9 RID: 2505
		// (get) Token: 0x06003BE2 RID: 15330 RVA: 0x000D8784 File Offset: 0x000D6984
		internal static TaskScheduler InternalCurrent
		{
			get
			{
				Task internalCurrent = Task.InternalCurrent;
				if (internalCurrent == null || (internalCurrent.CreationOptions & TaskCreationOptions.HideScheduler) != TaskCreationOptions.None)
				{
					return null;
				}
				return internalCurrent.ExecutingTaskScheduler;
			}
		}

		/// <summary>Creates a <see cref="T:System.Threading.Tasks.TaskScheduler" /> associated with the current <see cref="T:System.Threading.SynchronizationContext" />.</summary>
		/// <returns>A <see cref="T:System.Threading.Tasks.TaskScheduler" /> associated with the current <see cref="T:System.Threading.SynchronizationContext" />, as determined by <see cref="P:System.Threading.SynchronizationContext.Current" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">The current SynchronizationContext may not be used as a TaskScheduler.</exception>
		// Token: 0x06003BE3 RID: 15331 RVA: 0x000D87AD File Offset: 0x000D69AD
		public static TaskScheduler FromCurrentSynchronizationContext()
		{
			return new SynchronizationContextTaskScheduler();
		}

		/// <summary>Gets the unique ID for this <see cref="T:System.Threading.Tasks.TaskScheduler" />.</summary>
		/// <returns>Returns the unique ID for this <see cref="T:System.Threading.Tasks.TaskScheduler" />.</returns>
		// Token: 0x170009CA RID: 2506
		// (get) Token: 0x06003BE4 RID: 15332 RVA: 0x000D87B4 File Offset: 0x000D69B4
		public int Id
		{
			get
			{
				if (this.m_taskSchedulerId == 0)
				{
					int num;
					do
					{
						num = Interlocked.Increment(ref TaskScheduler.s_taskSchedulerIdCounter);
					}
					while (num == 0);
					Interlocked.CompareExchange(ref this.m_taskSchedulerId, num, 0);
				}
				return this.m_taskSchedulerId;
			}
		}

		/// <summary>Attempts to execute the provided <see cref="T:System.Threading.Tasks.Task" /> on this scheduler.</summary>
		/// <returns>A Boolean that is true if <paramref name="task" /> was successfully executed, false if it was not. A common reason for execution failure is that the task had previously been executed or is in the process of being executed by another thread.</returns>
		/// <param name="task">A <see cref="T:System.Threading.Tasks.Task" /> object to be executed.</param>
		/// <exception cref="T:System.InvalidOperationException">The <paramref name="task" /> is not associated with this scheduler.</exception>
		// Token: 0x06003BE5 RID: 15333 RVA: 0x000D87F1 File Offset: 0x000D69F1
		[SecurityCritical]
		protected bool TryExecuteTask(Task task)
		{
			if (task.ExecutingTaskScheduler != this)
			{
				throw new InvalidOperationException(Environment.GetResourceString("ExecuteTask may not be called for a task which was previously queued to a different TaskScheduler."));
			}
			return task.ExecuteEntry(true);
		}

		/// <summary>Occurs when a faulted task's unobserved exception is about to trigger exception escalation policy, which, by default, would terminate the process.</summary>
		// Token: 0x14000018 RID: 24
		// (add) Token: 0x06003BE6 RID: 15334 RVA: 0x000D8814 File Offset: 0x000D6A14
		// (remove) Token: 0x06003BE7 RID: 15335 RVA: 0x000D886C File Offset: 0x000D6A6C
		public static event EventHandler<UnobservedTaskExceptionEventArgs> UnobservedTaskException
		{
			[SecurityCritical]
			add
			{
				if (value != null)
				{
					RuntimeHelpers.PrepareContractedDelegate(value);
					object unobservedTaskExceptionLockObject = TaskScheduler._unobservedTaskExceptionLockObject;
					lock (unobservedTaskExceptionLockObject)
					{
						TaskScheduler._unobservedTaskException = (EventHandler<UnobservedTaskExceptionEventArgs>)Delegate.Combine(TaskScheduler._unobservedTaskException, value);
					}
				}
			}
			[SecurityCritical]
			remove
			{
				object unobservedTaskExceptionLockObject = TaskScheduler._unobservedTaskExceptionLockObject;
				lock (unobservedTaskExceptionLockObject)
				{
					TaskScheduler._unobservedTaskException = (EventHandler<UnobservedTaskExceptionEventArgs>)Delegate.Remove(TaskScheduler._unobservedTaskException, value);
				}
			}
		}

		// Token: 0x06003BE8 RID: 15336 RVA: 0x000D88BC File Offset: 0x000D6ABC
		internal static void PublishUnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs ueea)
		{
			object unobservedTaskExceptionLockObject = TaskScheduler._unobservedTaskExceptionLockObject;
			lock (unobservedTaskExceptionLockObject)
			{
				EventHandler<UnobservedTaskExceptionEventArgs> unobservedTaskException = TaskScheduler._unobservedTaskException;
				if (unobservedTaskException != null)
				{
					unobservedTaskException(sender, ueea);
				}
			}
		}

		// Token: 0x06003BE9 RID: 15337 RVA: 0x000D8908 File Offset: 0x000D6B08
		[SecurityCritical]
		internal Task[] GetScheduledTasksForDebugger()
		{
			IEnumerable<Task> scheduledTasks = this.GetScheduledTasks();
			if (scheduledTasks == null)
			{
				return null;
			}
			Task[] array = scheduledTasks as Task[];
			if (array == null)
			{
				array = new List<Task>(scheduledTasks).ToArray();
			}
			Task[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				int id = array2[i].Id;
			}
			return array;
		}

		// Token: 0x06003BEA RID: 15338 RVA: 0x000D8954 File Offset: 0x000D6B54
		[SecurityCritical]
		internal static TaskScheduler[] GetTaskSchedulersForDebugger()
		{
			if (TaskScheduler.s_activeTaskSchedulers == null)
			{
				return new TaskScheduler[] { TaskScheduler.s_defaultTaskScheduler };
			}
			ICollection<TaskScheduler> keys = TaskScheduler.s_activeTaskSchedulers.Keys;
			if (!keys.Contains(TaskScheduler.s_defaultTaskScheduler))
			{
				keys.Add(TaskScheduler.s_defaultTaskScheduler);
			}
			TaskScheduler[] array = new TaskScheduler[keys.Count];
			keys.CopyTo(array, 0);
			TaskScheduler[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				int id = array2[i].Id;
			}
			return array;
		}

		// Token: 0x04001F0C RID: 7948
		private static ConditionalWeakTable<TaskScheduler, object> s_activeTaskSchedulers;

		// Token: 0x04001F0D RID: 7949
		private static readonly TaskScheduler s_defaultTaskScheduler = new ThreadPoolTaskScheduler();

		// Token: 0x04001F0E RID: 7950
		internal static int s_taskSchedulerIdCounter;

		// Token: 0x04001F0F RID: 7951
		private volatile int m_taskSchedulerId;

		// Token: 0x04001F10 RID: 7952
		private static EventHandler<UnobservedTaskExceptionEventArgs> _unobservedTaskException;

		// Token: 0x04001F11 RID: 7953
		private static readonly object _unobservedTaskExceptionLockObject = new object();

		// Token: 0x0200051F RID: 1311
		internal sealed class SystemThreadingTasks_TaskSchedulerDebugView
		{
			// Token: 0x06003BEC RID: 15340 RVA: 0x000D89DE File Offset: 0x000D6BDE
			public SystemThreadingTasks_TaskSchedulerDebugView(TaskScheduler scheduler)
			{
				this.m_taskScheduler = scheduler;
			}

			// Token: 0x170009CB RID: 2507
			// (get) Token: 0x06003BED RID: 15341 RVA: 0x000D89ED File Offset: 0x000D6BED
			public int Id
			{
				get
				{
					return this.m_taskScheduler.Id;
				}
			}

			// Token: 0x170009CC RID: 2508
			// (get) Token: 0x06003BEE RID: 15342 RVA: 0x000D89FA File Offset: 0x000D6BFA
			public IEnumerable<Task> ScheduledTasks
			{
				[SecurityCritical]
				get
				{
					return this.m_taskScheduler.GetScheduledTasks();
				}
			}

			// Token: 0x04001F12 RID: 7954
			private readonly TaskScheduler m_taskScheduler;
		}
	}
}
