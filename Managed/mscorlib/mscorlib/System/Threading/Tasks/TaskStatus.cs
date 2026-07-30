using System;

namespace System.Threading.Tasks
{
	/// <summary>Represents the current stage in the lifecycle of a <see cref="T:System.Threading.Tasks.Task" />.</summary>
	// Token: 0x020004F5 RID: 1269
	public enum TaskStatus
	{
		/// <summary>The task has been initialized but has not yet been scheduled.</summary>
		// Token: 0x04001E6C RID: 7788
		Created,
		/// <summary>The task is waiting to be activated and scheduled internally by the .NET Framework infrastructure.</summary>
		// Token: 0x04001E6D RID: 7789
		WaitingForActivation,
		/// <summary>The task has been scheduled for execution but has not yet begun executing.</summary>
		// Token: 0x04001E6E RID: 7790
		WaitingToRun,
		/// <summary>The task is running but has not yet completed.</summary>
		// Token: 0x04001E6F RID: 7791
		Running,
		/// <summary>The task has finished executing and is implicitly waiting for attached child tasks to complete.</summary>
		// Token: 0x04001E70 RID: 7792
		WaitingForChildrenToComplete,
		/// <summary>The task completed execution successfully.</summary>
		// Token: 0x04001E71 RID: 7793
		RanToCompletion,
		/// <summary>The task acknowledged cancellation by throwing an OperationCanceledException with its own CancellationToken while the token was in signaled state, or the task's CancellationToken was already signaled before the task started executing. For more information, see Task Cancellation.</summary>
		// Token: 0x04001E72 RID: 7794
		Canceled,
		/// <summary>The task completed due to an unhandled exception.</summary>
		// Token: 0x04001E73 RID: 7795
		Faulted
	}
}
