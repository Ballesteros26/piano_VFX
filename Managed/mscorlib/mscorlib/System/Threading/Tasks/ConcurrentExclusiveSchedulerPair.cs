using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security;
using System.Security.Permissions;

namespace System.Threading.Tasks
{
	/// <summary>Provides task schedulers that coordinate to execute tasks while ensuring that concurrent tasks may run concurrently and exclusive tasks never do.</summary>
	// Token: 0x020004BD RID: 1213
	[DebuggerDisplay("Concurrent={ConcurrentTaskCountForDebugger}, Exclusive={ExclusiveTaskCountForDebugger}, Mode={ModeForDebugger}")]
	[DebuggerTypeProxy(typeof(ConcurrentExclusiveSchedulerPair.DebugView))]
	[HostProtection(SecurityAction.LinkDemand, Synchronization = true, ExternalThreading = true)]
	public class ConcurrentExclusiveSchedulerPair
	{
		// Token: 0x17000943 RID: 2371
		// (get) Token: 0x0600388F RID: 14479 RVA: 0x000CC890 File Offset: 0x000CAA90
		private static int DefaultMaxConcurrencyLevel
		{
			get
			{
				return Environment.ProcessorCount;
			}
		}

		// Token: 0x17000944 RID: 2372
		// (get) Token: 0x06003890 RID: 14480 RVA: 0x000CC897 File Offset: 0x000CAA97
		private object ValueLock
		{
			get
			{
				return this.m_threadProcessingMapping;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.Tasks.ConcurrentExclusiveSchedulerPair" /> class.</summary>
		// Token: 0x06003891 RID: 14481 RVA: 0x000CC89F File Offset: 0x000CAA9F
		public ConcurrentExclusiveSchedulerPair()
			: this(TaskScheduler.Default, ConcurrentExclusiveSchedulerPair.DefaultMaxConcurrencyLevel, -1)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.Tasks.ConcurrentExclusiveSchedulerPair" /> class that targets the specified scheduler.</summary>
		/// <param name="taskScheduler">The target scheduler on which this pair should execute.</param>
		// Token: 0x06003892 RID: 14482 RVA: 0x000CC8B2 File Offset: 0x000CAAB2
		public ConcurrentExclusiveSchedulerPair(TaskScheduler taskScheduler)
			: this(taskScheduler, ConcurrentExclusiveSchedulerPair.DefaultMaxConcurrencyLevel, -1)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.Tasks.ConcurrentExclusiveSchedulerPair" /> class that targets the specified scheduler with a maximum concurrency level.</summary>
		/// <param name="taskScheduler">The target scheduler on which this pair should execute.</param>
		/// <param name="maxConcurrencyLevel">The maximum number of tasks to run concurrently.</param>
		// Token: 0x06003893 RID: 14483 RVA: 0x000CC8C1 File Offset: 0x000CAAC1
		public ConcurrentExclusiveSchedulerPair(TaskScheduler taskScheduler, int maxConcurrencyLevel)
			: this(taskScheduler, maxConcurrencyLevel, -1)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.Tasks.ConcurrentExclusiveSchedulerPair" /> class that targets the specified scheduler with a maximum concurrency level and a maximum number of scheduled tasks that may be processed as a unit.</summary>
		/// <param name="taskScheduler">The target scheduler on which this pair should execute.</param>
		/// <param name="maxConcurrencyLevel">The maximum number of tasks to run concurrently.</param>
		/// <param name="maxItemsPerTask">The maximum number of tasks to process for each underlying scheduled task used by the pair.</param>
		// Token: 0x06003894 RID: 14484 RVA: 0x000CC8CC File Offset: 0x000CAACC
		public ConcurrentExclusiveSchedulerPair(TaskScheduler taskScheduler, int maxConcurrencyLevel, int maxItemsPerTask)
		{
			if (taskScheduler == null)
			{
				throw new ArgumentNullException("taskScheduler");
			}
			if (maxConcurrencyLevel == 0 || maxConcurrencyLevel < -1)
			{
				throw new ArgumentOutOfRangeException("maxConcurrencyLevel");
			}
			if (maxItemsPerTask == 0 || maxItemsPerTask < -1)
			{
				throw new ArgumentOutOfRangeException("maxItemsPerTask");
			}
			this.m_underlyingTaskScheduler = taskScheduler;
			this.m_maxConcurrencyLevel = maxConcurrencyLevel;
			this.m_maxItemsPerTask = maxItemsPerTask;
			int maximumConcurrencyLevel = taskScheduler.MaximumConcurrencyLevel;
			if (maximumConcurrencyLevel > 0 && maximumConcurrencyLevel < this.m_maxConcurrencyLevel)
			{
				this.m_maxConcurrencyLevel = maximumConcurrencyLevel;
			}
			if (this.m_maxConcurrencyLevel == -1)
			{
				this.m_maxConcurrencyLevel = int.MaxValue;
			}
			if (this.m_maxItemsPerTask == -1)
			{
				this.m_maxItemsPerTask = int.MaxValue;
			}
			this.m_exclusiveTaskScheduler = new ConcurrentExclusiveSchedulerPair.ConcurrentExclusiveTaskScheduler(this, 1, ConcurrentExclusiveSchedulerPair.ProcessingMode.ProcessingExclusiveTask);
			this.m_concurrentTaskScheduler = new ConcurrentExclusiveSchedulerPair.ConcurrentExclusiveTaskScheduler(this, this.m_maxConcurrencyLevel, ConcurrentExclusiveSchedulerPair.ProcessingMode.ProcessingConcurrentTasks);
		}

		/// <summary>Informs the scheduler pair that it should not accept any more tasks.</summary>
		// Token: 0x06003895 RID: 14485 RVA: 0x000CC998 File Offset: 0x000CAB98
		public void Complete()
		{
			object valueLock = this.ValueLock;
			lock (valueLock)
			{
				if (!this.CompletionRequested)
				{
					this.RequestCompletion();
					this.CleanupStateIfCompletingAndQuiesced();
				}
			}
		}

		/// <summary>Gets a <see cref="T:System.Threading.Tasks.Task" /> that will complete when the scheduler has completed processing.</summary>
		/// <returns>The asynchronous operation that will complete when the scheduler finishes processing.</returns>
		// Token: 0x17000945 RID: 2373
		// (get) Token: 0x06003896 RID: 14486 RVA: 0x000CC9E8 File Offset: 0x000CABE8
		public Task Completion
		{
			get
			{
				return this.EnsureCompletionStateInitialized().Task;
			}
		}

		// Token: 0x06003897 RID: 14487 RVA: 0x000CC9F5 File Offset: 0x000CABF5
		private ConcurrentExclusiveSchedulerPair.CompletionState EnsureCompletionStateInitialized()
		{
			return LazyInitializer.EnsureInitialized<ConcurrentExclusiveSchedulerPair.CompletionState>(ref this.m_completionState, () => new ConcurrentExclusiveSchedulerPair.CompletionState());
		}

		// Token: 0x17000946 RID: 2374
		// (get) Token: 0x06003898 RID: 14488 RVA: 0x000CCA21 File Offset: 0x000CAC21
		private bool CompletionRequested
		{
			get
			{
				return this.m_completionState != null && Volatile.Read(ref this.m_completionState.m_completionRequested);
			}
		}

		// Token: 0x06003899 RID: 14489 RVA: 0x000CCA3D File Offset: 0x000CAC3D
		private void RequestCompletion()
		{
			this.EnsureCompletionStateInitialized().m_completionRequested = true;
		}

		// Token: 0x0600389A RID: 14490 RVA: 0x000CCA4B File Offset: 0x000CAC4B
		private void CleanupStateIfCompletingAndQuiesced()
		{
			if (this.ReadyToComplete)
			{
				this.CompleteTaskAsync();
			}
		}

		// Token: 0x17000947 RID: 2375
		// (get) Token: 0x0600389B RID: 14491 RVA: 0x000CCA5C File Offset: 0x000CAC5C
		private bool ReadyToComplete
		{
			get
			{
				if (!this.CompletionRequested || this.m_processingCount != 0)
				{
					return false;
				}
				ConcurrentExclusiveSchedulerPair.CompletionState completionState = this.EnsureCompletionStateInitialized();
				return (completionState.m_exceptions != null && completionState.m_exceptions.Count > 0) || (this.m_concurrentTaskScheduler.m_tasks.IsEmpty && this.m_exclusiveTaskScheduler.m_tasks.IsEmpty);
			}
		}

		// Token: 0x0600389C RID: 14492 RVA: 0x000CCAC0 File Offset: 0x000CACC0
		private void CompleteTaskAsync()
		{
			ConcurrentExclusiveSchedulerPair.CompletionState completionState = this.EnsureCompletionStateInitialized();
			if (!completionState.m_completionQueued)
			{
				completionState.m_completionQueued = true;
				ThreadPool.QueueUserWorkItem(delegate(object state)
				{
					ConcurrentExclusiveSchedulerPair.CompletionState completionState2 = (ConcurrentExclusiveSchedulerPair.CompletionState)state;
					List<Exception> exceptions = completionState2.m_exceptions;
					if (exceptions == null || exceptions.Count <= 0)
					{
						completionState2.TrySetResult(default(VoidTaskResult));
						return;
					}
					completionState2.TrySetException(exceptions);
				}, completionState);
			}
		}

		// Token: 0x0600389D RID: 14493 RVA: 0x000CCB0C File Offset: 0x000CAD0C
		private void FaultWithTask(Task faultedTask)
		{
			ConcurrentExclusiveSchedulerPair.CompletionState completionState = this.EnsureCompletionStateInitialized();
			if (completionState.m_exceptions == null)
			{
				completionState.m_exceptions = new List<Exception>();
			}
			completionState.m_exceptions.AddRange(faultedTask.Exception.InnerExceptions);
			this.RequestCompletion();
		}

		/// <summary>Gets a <see cref="T:System.Threading.Tasks.TaskScheduler" /> that can be used to schedule tasks to this pair that may run concurrently with other tasks on this pair.</summary>
		/// <returns>An object that can be used to schedule tasks concurrently.</returns>
		// Token: 0x17000948 RID: 2376
		// (get) Token: 0x0600389E RID: 14494 RVA: 0x000CCB4F File Offset: 0x000CAD4F
		public TaskScheduler ConcurrentScheduler
		{
			get
			{
				return this.m_concurrentTaskScheduler;
			}
		}

		/// <summary>Gets a <see cref="T:System.Threading.Tasks.TaskScheduler" /> that can be used to schedule tasks to this pair that must run exclusively with regards to other tasks on this pair.</summary>
		/// <returns>An object that can be used to schedule tasks that do not run concurrently with other tasks.</returns>
		// Token: 0x17000949 RID: 2377
		// (get) Token: 0x0600389F RID: 14495 RVA: 0x000CCB57 File Offset: 0x000CAD57
		public TaskScheduler ExclusiveScheduler
		{
			get
			{
				return this.m_exclusiveTaskScheduler;
			}
		}

		// Token: 0x1700094A RID: 2378
		// (get) Token: 0x060038A0 RID: 14496 RVA: 0x000CCB5F File Offset: 0x000CAD5F
		private int ConcurrentTaskCountForDebugger
		{
			get
			{
				return this.m_concurrentTaskScheduler.m_tasks.Count;
			}
		}

		// Token: 0x1700094B RID: 2379
		// (get) Token: 0x060038A1 RID: 14497 RVA: 0x000CCB71 File Offset: 0x000CAD71
		private int ExclusiveTaskCountForDebugger
		{
			get
			{
				return this.m_exclusiveTaskScheduler.m_tasks.Count;
			}
		}

		// Token: 0x060038A2 RID: 14498 RVA: 0x000CCB84 File Offset: 0x000CAD84
		private void ProcessAsyncIfNecessary(bool fairly = false)
		{
			if (this.m_processingCount >= 0)
			{
				bool flag = !this.m_exclusiveTaskScheduler.m_tasks.IsEmpty;
				Task task = null;
				if (this.m_processingCount == 0 && flag)
				{
					this.m_processingCount = -1;
					try
					{
						task = new Task(delegate(object thisPair)
						{
							((ConcurrentExclusiveSchedulerPair)thisPair).ProcessExclusiveTasks();
						}, this, default(CancellationToken), ConcurrentExclusiveSchedulerPair.GetCreationOptionsForTask(fairly));
						task.Start(this.m_underlyingTaskScheduler);
						goto IL_0149;
					}
					catch
					{
						this.m_processingCount = 0;
						this.FaultWithTask(task);
						goto IL_0149;
					}
				}
				int count = this.m_concurrentTaskScheduler.m_tasks.Count;
				if (count > 0 && !flag && this.m_processingCount < this.m_maxConcurrencyLevel)
				{
					int num = 0;
					while (num < count && this.m_processingCount < this.m_maxConcurrencyLevel)
					{
						this.m_processingCount++;
						try
						{
							task = new Task(delegate(object thisPair)
							{
								((ConcurrentExclusiveSchedulerPair)thisPair).ProcessConcurrentTasks();
							}, this, default(CancellationToken), ConcurrentExclusiveSchedulerPair.GetCreationOptionsForTask(fairly));
							task.Start(this.m_underlyingTaskScheduler);
						}
						catch
						{
							this.m_processingCount--;
							this.FaultWithTask(task);
						}
						num++;
					}
				}
				IL_0149:
				this.CleanupStateIfCompletingAndQuiesced();
			}
		}

		// Token: 0x060038A3 RID: 14499 RVA: 0x000CCCFC File Offset: 0x000CAEFC
		private void ProcessExclusiveTasks()
		{
			try
			{
				this.m_threadProcessingMapping[Thread.CurrentThread.ManagedThreadId] = ConcurrentExclusiveSchedulerPair.ProcessingMode.ProcessingExclusiveTask;
				for (int i = 0; i < this.m_maxItemsPerTask; i++)
				{
					Task task;
					if (!this.m_exclusiveTaskScheduler.m_tasks.TryDequeue(out task))
					{
						break;
					}
					if (!task.IsFaulted)
					{
						this.m_exclusiveTaskScheduler.ExecuteTask(task);
					}
				}
			}
			finally
			{
				ConcurrentExclusiveSchedulerPair.ProcessingMode processingMode;
				this.m_threadProcessingMapping.TryRemove(Thread.CurrentThread.ManagedThreadId, out processingMode);
				object valueLock = this.ValueLock;
				lock (valueLock)
				{
					this.m_processingCount = 0;
					this.ProcessAsyncIfNecessary(true);
				}
			}
		}

		// Token: 0x060038A4 RID: 14500 RVA: 0x000CCDC0 File Offset: 0x000CAFC0
		private void ProcessConcurrentTasks()
		{
			try
			{
				this.m_threadProcessingMapping[Thread.CurrentThread.ManagedThreadId] = ConcurrentExclusiveSchedulerPair.ProcessingMode.ProcessingConcurrentTasks;
				for (int i = 0; i < this.m_maxItemsPerTask; i++)
				{
					Task task;
					if (!this.m_concurrentTaskScheduler.m_tasks.TryDequeue(out task))
					{
						break;
					}
					if (!task.IsFaulted)
					{
						this.m_concurrentTaskScheduler.ExecuteTask(task);
					}
					if (!this.m_exclusiveTaskScheduler.m_tasks.IsEmpty)
					{
						break;
					}
				}
			}
			finally
			{
				ConcurrentExclusiveSchedulerPair.ProcessingMode processingMode;
				this.m_threadProcessingMapping.TryRemove(Thread.CurrentThread.ManagedThreadId, out processingMode);
				object valueLock = this.ValueLock;
				lock (valueLock)
				{
					if (this.m_processingCount > 0)
					{
						this.m_processingCount--;
					}
					this.ProcessAsyncIfNecessary(true);
				}
			}
		}

		// Token: 0x1700094C RID: 2380
		// (get) Token: 0x060038A5 RID: 14501 RVA: 0x000CCEAC File Offset: 0x000CB0AC
		private ConcurrentExclusiveSchedulerPair.ProcessingMode ModeForDebugger
		{
			get
			{
				if (this.m_completionState != null && this.m_completionState.Task.IsCompleted)
				{
					return ConcurrentExclusiveSchedulerPair.ProcessingMode.Completed;
				}
				ConcurrentExclusiveSchedulerPair.ProcessingMode processingMode = ConcurrentExclusiveSchedulerPair.ProcessingMode.NotCurrentlyProcessing;
				if (this.m_processingCount == -1)
				{
					processingMode |= ConcurrentExclusiveSchedulerPair.ProcessingMode.ProcessingExclusiveTask;
				}
				if (this.m_processingCount >= 1)
				{
					processingMode |= ConcurrentExclusiveSchedulerPair.ProcessingMode.ProcessingConcurrentTasks;
				}
				if (this.CompletionRequested)
				{
					processingMode |= ConcurrentExclusiveSchedulerPair.ProcessingMode.Completing;
				}
				return processingMode;
			}
		}

		// Token: 0x060038A6 RID: 14502 RVA: 0x00002194 File Offset: 0x00000394
		[Conditional("DEBUG")]
		internal static void ContractAssertMonitorStatus(object syncObj, bool held)
		{
		}

		// Token: 0x060038A7 RID: 14503 RVA: 0x000CCF00 File Offset: 0x000CB100
		internal static TaskCreationOptions GetCreationOptionsForTask(bool isReplacementReplica = false)
		{
			TaskCreationOptions taskCreationOptions = TaskCreationOptions.DenyChildAttach;
			if (isReplacementReplica)
			{
				taskCreationOptions |= TaskCreationOptions.PreferFairness;
			}
			return taskCreationOptions;
		}

		// Token: 0x04001DA8 RID: 7592
		private readonly ConcurrentDictionary<int, ConcurrentExclusiveSchedulerPair.ProcessingMode> m_threadProcessingMapping = new ConcurrentDictionary<int, ConcurrentExclusiveSchedulerPair.ProcessingMode>();

		// Token: 0x04001DA9 RID: 7593
		private readonly ConcurrentExclusiveSchedulerPair.ConcurrentExclusiveTaskScheduler m_concurrentTaskScheduler;

		// Token: 0x04001DAA RID: 7594
		private readonly ConcurrentExclusiveSchedulerPair.ConcurrentExclusiveTaskScheduler m_exclusiveTaskScheduler;

		// Token: 0x04001DAB RID: 7595
		private readonly TaskScheduler m_underlyingTaskScheduler;

		// Token: 0x04001DAC RID: 7596
		private readonly int m_maxConcurrencyLevel;

		// Token: 0x04001DAD RID: 7597
		private readonly int m_maxItemsPerTask;

		// Token: 0x04001DAE RID: 7598
		private int m_processingCount;

		// Token: 0x04001DAF RID: 7599
		private ConcurrentExclusiveSchedulerPair.CompletionState m_completionState;

		// Token: 0x04001DB0 RID: 7600
		private const int UNLIMITED_PROCESSING = -1;

		// Token: 0x04001DB1 RID: 7601
		private const int EXCLUSIVE_PROCESSING_SENTINEL = -1;

		// Token: 0x04001DB2 RID: 7602
		private const int DEFAULT_MAXITEMSPERTASK = -1;

		// Token: 0x020004BE RID: 1214
		private sealed class CompletionState : TaskCompletionSource<VoidTaskResult>
		{
			// Token: 0x04001DB3 RID: 7603
			internal bool m_completionRequested;

			// Token: 0x04001DB4 RID: 7604
			internal bool m_completionQueued;

			// Token: 0x04001DB5 RID: 7605
			internal List<Exception> m_exceptions;
		}

		// Token: 0x020004BF RID: 1215
		[DebuggerTypeProxy(typeof(ConcurrentExclusiveSchedulerPair.ConcurrentExclusiveTaskScheduler.DebugView))]
		[DebuggerDisplay("Count={CountForDebugger}, MaxConcurrencyLevel={m_maxConcurrencyLevel}, Id={Id}")]
		private sealed class ConcurrentExclusiveTaskScheduler : TaskScheduler
		{
			// Token: 0x060038A9 RID: 14505 RVA: 0x000CCF20 File Offset: 0x000CB120
			internal ConcurrentExclusiveTaskScheduler(ConcurrentExclusiveSchedulerPair pair, int maxConcurrencyLevel, ConcurrentExclusiveSchedulerPair.ProcessingMode processingMode)
			{
				this.m_pair = pair;
				this.m_maxConcurrencyLevel = maxConcurrencyLevel;
				this.m_processingMode = processingMode;
				IProducerConsumerQueue<Task> producerConsumerQueue2;
				if (processingMode != ConcurrentExclusiveSchedulerPair.ProcessingMode.ProcessingExclusiveTask)
				{
					IProducerConsumerQueue<Task> producerConsumerQueue = new MultiProducerMultiConsumerQueue<Task>();
					producerConsumerQueue2 = producerConsumerQueue;
				}
				else
				{
					IProducerConsumerQueue<Task> producerConsumerQueue = new SingleProducerSingleConsumerQueue<Task>();
					producerConsumerQueue2 = producerConsumerQueue;
				}
				this.m_tasks = producerConsumerQueue2;
			}

			// Token: 0x1700094D RID: 2381
			// (get) Token: 0x060038AA RID: 14506 RVA: 0x000CCF62 File Offset: 0x000CB162
			public override int MaximumConcurrencyLevel
			{
				get
				{
					return this.m_maxConcurrencyLevel;
				}
			}

			// Token: 0x060038AB RID: 14507 RVA: 0x000CCF6C File Offset: 0x000CB16C
			[SecurityCritical]
			protected internal override void QueueTask(Task task)
			{
				object valueLock = this.m_pair.ValueLock;
				lock (valueLock)
				{
					if (this.m_pair.CompletionRequested)
					{
						throw new InvalidOperationException(base.GetType().Name);
					}
					this.m_tasks.Enqueue(task);
					this.m_pair.ProcessAsyncIfNecessary(false);
				}
			}

			// Token: 0x060038AC RID: 14508 RVA: 0x000CCFE4 File Offset: 0x000CB1E4
			[SecuritySafeCritical]
			internal void ExecuteTask(Task task)
			{
				base.TryExecuteTask(task);
			}

			// Token: 0x060038AD RID: 14509 RVA: 0x000CCFF0 File Offset: 0x000CB1F0
			[SecurityCritical]
			protected override bool TryExecuteTaskInline(Task task, bool taskWasPreviouslyQueued)
			{
				if (!taskWasPreviouslyQueued && this.m_pair.CompletionRequested)
				{
					return false;
				}
				bool flag = this.m_pair.m_underlyingTaskScheduler == TaskScheduler.Default;
				if (flag && taskWasPreviouslyQueued && !Thread.CurrentThread.IsThreadPoolThread)
				{
					return false;
				}
				ConcurrentExclusiveSchedulerPair.ProcessingMode processingMode;
				if (!this.m_pair.m_threadProcessingMapping.TryGetValue(Thread.CurrentThread.ManagedThreadId, out processingMode) || processingMode != this.m_processingMode)
				{
					return false;
				}
				if (!flag || taskWasPreviouslyQueued)
				{
					return this.TryExecuteTaskInlineOnTargetScheduler(task);
				}
				return base.TryExecuteTask(task);
			}

			// Token: 0x060038AE RID: 14510 RVA: 0x000CD074 File Offset: 0x000CB274
			private bool TryExecuteTaskInlineOnTargetScheduler(Task task)
			{
				Task<bool> task2 = new Task<bool>(ConcurrentExclusiveSchedulerPair.ConcurrentExclusiveTaskScheduler.s_tryExecuteTaskShim, Tuple.Create<ConcurrentExclusiveSchedulerPair.ConcurrentExclusiveTaskScheduler, Task>(this, task));
				bool result;
				try
				{
					task2.RunSynchronously(this.m_pair.m_underlyingTaskScheduler);
					result = task2.Result;
				}
				catch
				{
					AggregateException exception = task2.Exception;
					throw;
				}
				finally
				{
					task2.Dispose();
				}
				return result;
			}

			// Token: 0x060038AF RID: 14511 RVA: 0x000CD0DC File Offset: 0x000CB2DC
			[SecuritySafeCritical]
			private static bool TryExecuteTaskShim(object state)
			{
				Tuple<ConcurrentExclusiveSchedulerPair.ConcurrentExclusiveTaskScheduler, Task> tuple = (Tuple<ConcurrentExclusiveSchedulerPair.ConcurrentExclusiveTaskScheduler, Task>)state;
				return tuple.Item1.TryExecuteTask(tuple.Item2);
			}

			// Token: 0x060038B0 RID: 14512 RVA: 0x000CD101 File Offset: 0x000CB301
			[SecurityCritical]
			protected override IEnumerable<Task> GetScheduledTasks()
			{
				return this.m_tasks;
			}

			// Token: 0x1700094E RID: 2382
			// (get) Token: 0x060038B1 RID: 14513 RVA: 0x000CD109 File Offset: 0x000CB309
			private int CountForDebugger
			{
				get
				{
					return this.m_tasks.Count;
				}
			}

			// Token: 0x04001DB6 RID: 7606
			private static readonly Func<object, bool> s_tryExecuteTaskShim = new Func<object, bool>(ConcurrentExclusiveSchedulerPair.ConcurrentExclusiveTaskScheduler.TryExecuteTaskShim);

			// Token: 0x04001DB7 RID: 7607
			private readonly ConcurrentExclusiveSchedulerPair m_pair;

			// Token: 0x04001DB8 RID: 7608
			private readonly int m_maxConcurrencyLevel;

			// Token: 0x04001DB9 RID: 7609
			private readonly ConcurrentExclusiveSchedulerPair.ProcessingMode m_processingMode;

			// Token: 0x04001DBA RID: 7610
			internal readonly IProducerConsumerQueue<Task> m_tasks;

			// Token: 0x020004C0 RID: 1216
			private sealed class DebugView
			{
				// Token: 0x060038B3 RID: 14515 RVA: 0x000CD129 File Offset: 0x000CB329
				public DebugView(ConcurrentExclusiveSchedulerPair.ConcurrentExclusiveTaskScheduler scheduler)
				{
					this.m_taskScheduler = scheduler;
				}

				// Token: 0x1700094F RID: 2383
				// (get) Token: 0x060038B4 RID: 14516 RVA: 0x000CD138 File Offset: 0x000CB338
				public int MaximumConcurrencyLevel
				{
					get
					{
						return this.m_taskScheduler.m_maxConcurrencyLevel;
					}
				}

				// Token: 0x17000950 RID: 2384
				// (get) Token: 0x060038B5 RID: 14517 RVA: 0x000CD145 File Offset: 0x000CB345
				public IEnumerable<Task> ScheduledTasks
				{
					get
					{
						return this.m_taskScheduler.m_tasks;
					}
				}

				// Token: 0x17000951 RID: 2385
				// (get) Token: 0x060038B6 RID: 14518 RVA: 0x000CD152 File Offset: 0x000CB352
				public ConcurrentExclusiveSchedulerPair SchedulerPair
				{
					get
					{
						return this.m_taskScheduler.m_pair;
					}
				}

				// Token: 0x04001DBB RID: 7611
				private readonly ConcurrentExclusiveSchedulerPair.ConcurrentExclusiveTaskScheduler m_taskScheduler;
			}
		}

		// Token: 0x020004C1 RID: 1217
		private sealed class DebugView
		{
			// Token: 0x060038B7 RID: 14519 RVA: 0x000CD15F File Offset: 0x000CB35F
			public DebugView(ConcurrentExclusiveSchedulerPair pair)
			{
				this.m_pair = pair;
			}

			// Token: 0x17000952 RID: 2386
			// (get) Token: 0x060038B8 RID: 14520 RVA: 0x000CD16E File Offset: 0x000CB36E
			public ConcurrentExclusiveSchedulerPair.ProcessingMode Mode
			{
				get
				{
					return this.m_pair.ModeForDebugger;
				}
			}

			// Token: 0x17000953 RID: 2387
			// (get) Token: 0x060038B9 RID: 14521 RVA: 0x000CD17B File Offset: 0x000CB37B
			public IEnumerable<Task> ScheduledExclusive
			{
				get
				{
					return this.m_pair.m_exclusiveTaskScheduler.m_tasks;
				}
			}

			// Token: 0x17000954 RID: 2388
			// (get) Token: 0x060038BA RID: 14522 RVA: 0x000CD18D File Offset: 0x000CB38D
			public IEnumerable<Task> ScheduledConcurrent
			{
				get
				{
					return this.m_pair.m_concurrentTaskScheduler.m_tasks;
				}
			}

			// Token: 0x17000955 RID: 2389
			// (get) Token: 0x060038BB RID: 14523 RVA: 0x000CD19F File Offset: 0x000CB39F
			public int CurrentlyExecutingTaskCount
			{
				get
				{
					if (this.m_pair.m_processingCount != -1)
					{
						return this.m_pair.m_processingCount;
					}
					return 1;
				}
			}

			// Token: 0x17000956 RID: 2390
			// (get) Token: 0x060038BC RID: 14524 RVA: 0x000CD1BC File Offset: 0x000CB3BC
			public TaskScheduler TargetScheduler
			{
				get
				{
					return this.m_pair.m_underlyingTaskScheduler;
				}
			}

			// Token: 0x04001DBC RID: 7612
			private readonly ConcurrentExclusiveSchedulerPair m_pair;
		}

		// Token: 0x020004C2 RID: 1218
		[Flags]
		private enum ProcessingMode : byte
		{
			// Token: 0x04001DBE RID: 7614
			NotCurrentlyProcessing = 0,
			// Token: 0x04001DBF RID: 7615
			ProcessingExclusiveTask = 1,
			// Token: 0x04001DC0 RID: 7616
			ProcessingConcurrentTasks = 2,
			// Token: 0x04001DC1 RID: 7617
			Completing = 4,
			// Token: 0x04001DC2 RID: 7618
			Completed = 8
		}
	}
}
