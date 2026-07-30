using System;

namespace System.Threading.Tasks
{
	/// <summary>Specifies the behavior for a task that is created by using the <see cref="M:System.Threading.Tasks.Task.ContinueWith(System.Action{System.Threading.Tasks.Task},System.Threading.CancellationToken,System.Threading.Tasks.TaskContinuationOptions,System.Threading.Tasks.TaskScheduler)" /> or <see cref="M:System.Threading.Tasks.Task`1.ContinueWith(System.Action{System.Threading.Tasks.Task{`0}},System.Threading.Tasks.TaskContinuationOptions)" /> method.</summary>
	// Token: 0x02000505 RID: 1285
	[Flags]
	[Serializable]
	public enum TaskContinuationOptions
	{
		/// <summary>Default = "Continue on any, no task options, run asynchronously" Specifies that the default behavior should be used. Continuations, by default, will be scheduled when the antecedent task completes, regardless of the task's final <see cref="T:System.Threading.Tasks.TaskStatus" />.</summary>
		// Token: 0x04001ECE RID: 7886
		None = 0,
		/// <summary>A hint to a <see cref="T:System.Threading.Tasks.TaskScheduler" /> to schedule a task in as fair a manner as possible, meaning that tasks scheduled sooner will be more likely to be run sooner, and tasks scheduled later will be more likely to be run later.</summary>
		// Token: 0x04001ECF RID: 7887
		PreferFairness = 1,
		/// <summary>Specifies that a task will be a long-running, course-grained operation. It provides a hint to the <see cref="T:System.Threading.Tasks.TaskScheduler" /> that oversubscription may be warranted.</summary>
		// Token: 0x04001ED0 RID: 7888
		LongRunning = 2,
		/// <summary>Specifies that a task is attached to a parent in the task hierarchy.</summary>
		// Token: 0x04001ED1 RID: 7889
		AttachedToParent = 4,
		/// <summary>Specifies that an <see cref="T:System.InvalidOperationException" /> will be thrown if an attempt is made to attach a child task to the created task.</summary>
		// Token: 0x04001ED2 RID: 7890
		DenyChildAttach = 8,
		/// <summary>Prevents the ambient scheduler from being seen as the current scheduler in the created task. This means that operations like StartNew or ContinueWith that are performed in the created task will see <see cref="P:System.Threading.Tasks.TaskScheduler.Default" /> as the current scheduler.</summary>
		// Token: 0x04001ED3 RID: 7891
		HideScheduler = 16,
		/// <summary>In the case of continuation cancellation, prevents completion of the continuation until the antecedent has completed.</summary>
		// Token: 0x04001ED4 RID: 7892
		LazyCancellation = 32,
		// Token: 0x04001ED5 RID: 7893
		RunContinuationsAsynchronously = 64,
		/// <summary>Specifies that the continuation task should not be scheduled if its antecedent ran to completion. This option is not valid for multi-task continuations.</summary>
		// Token: 0x04001ED6 RID: 7894
		NotOnRanToCompletion = 65536,
		/// <summary>Specifies that the continuation task should not be scheduled if its antecedent threw an unhandled exception. This option is not valid for multi-task continuations.</summary>
		// Token: 0x04001ED7 RID: 7895
		NotOnFaulted = 131072,
		/// <summary>Specifies that the continuation task should not be scheduled if its antecedent was canceled. This option is not valid for multi-task continuations.</summary>
		// Token: 0x04001ED8 RID: 7896
		NotOnCanceled = 262144,
		/// <summary>Specifies that the continuation task should be scheduled only if its antecedent ran to completion. This option is not valid for multi-task continuations.</summary>
		// Token: 0x04001ED9 RID: 7897
		OnlyOnRanToCompletion = 393216,
		/// <summary>Specifies that the continuation task should be scheduled only if its antecedent threw an unhandled exception. This option is not valid for multi-task continuations.</summary>
		// Token: 0x04001EDA RID: 7898
		OnlyOnFaulted = 327680,
		/// <summary>Specifies that the continuation task should be scheduled only if its antecedent was canceled.  This option is not valid for multi-task continuations.</summary>
		// Token: 0x04001EDB RID: 7899
		OnlyOnCanceled = 196608,
		/// <summary>Specifies that the continuation task should be executed synchronously. With this option specified, the continuation will be run on the same thread that causes the antecedent task to transition into its final state. If the antecedent is already complete when the continuation is created, the continuation will run on the thread creating the continuation. Only very short-running continuations should be executed synchronously.</summary>
		// Token: 0x04001EDC RID: 7900
		ExecuteSynchronously = 524288
	}
}
