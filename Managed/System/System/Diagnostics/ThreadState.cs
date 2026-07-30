using System;

namespace System.Diagnostics
{
	/// <summary>Specifies the current execution state of the thread.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000219 RID: 537
	public enum ThreadState
	{
		/// <summary>A state that indicates the thread has been initialized, but has not yet started.</summary>
		// Token: 0x040011EA RID: 4586
		Initialized,
		/// <summary>A state that indicates the thread is waiting to use a processor because no processor is free. The thread is prepared to run on the next available processor.</summary>
		// Token: 0x040011EB RID: 4587
		Ready,
		/// <summary>A state that indicates the thread is currently using a processor.</summary>
		// Token: 0x040011EC RID: 4588
		Running,
		/// <summary>A state that indicates the thread is about to use a processor. Only one thread can be in this state at a time.</summary>
		// Token: 0x040011ED RID: 4589
		Standby,
		/// <summary>A state that indicates the thread has finished executing and has exited.</summary>
		// Token: 0x040011EE RID: 4590
		Terminated,
		/// <summary>A state that indicates the thread is waiting for a resource, other than the processor, before it can execute. For example, it might be waiting for its execution stack to be paged in from disk.</summary>
		// Token: 0x040011EF RID: 4591
		Transition = 6,
		/// <summary>The state of the thread is unknown.</summary>
		// Token: 0x040011F0 RID: 4592
		Unknown,
		/// <summary>A state that indicates the thread is not ready to use the processor because it is waiting for a peripheral operation to complete or a resource to become free. When the thread is ready, it will be rescheduled.</summary>
		// Token: 0x040011F1 RID: 4593
		Wait = 5
	}
}
