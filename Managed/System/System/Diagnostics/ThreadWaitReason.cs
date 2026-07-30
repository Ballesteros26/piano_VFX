using System;

namespace System.Diagnostics
{
	/// <summary>Specifies the reason a thread is waiting.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200021A RID: 538
	public enum ThreadWaitReason
	{
		/// <summary>The thread is waiting for event pair high.</summary>
		// Token: 0x040011F3 RID: 4595
		EventPairHigh = 7,
		/// <summary>The thread is waiting for event pair low.</summary>
		// Token: 0x040011F4 RID: 4596
		EventPairLow,
		/// <summary>Thread execution is delayed.</summary>
		// Token: 0x040011F5 RID: 4597
		ExecutionDelay = 4,
		/// <summary>The thread is waiting for the scheduler.</summary>
		// Token: 0x040011F6 RID: 4598
		Executive = 0,
		/// <summary>The thread is waiting for a free virtual memory page.</summary>
		// Token: 0x040011F7 RID: 4599
		FreePage,
		/// <summary>The thread is waiting for a local procedure call to arrive.</summary>
		// Token: 0x040011F8 RID: 4600
		LpcReceive = 9,
		/// <summary>The thread is waiting for reply to a local procedure call to arrive.</summary>
		// Token: 0x040011F9 RID: 4601
		LpcReply,
		/// <summary>The thread is waiting for a virtual memory page to arrive in memory.</summary>
		// Token: 0x040011FA RID: 4602
		PageIn = 2,
		/// <summary>The thread is waiting for a virtual memory page to be written to disk.</summary>
		// Token: 0x040011FB RID: 4603
		PageOut = 12,
		/// <summary>Thread execution is suspended.</summary>
		// Token: 0x040011FC RID: 4604
		Suspended = 5,
		/// <summary>The thread is waiting for system allocation.</summary>
		// Token: 0x040011FD RID: 4605
		SystemAllocation = 3,
		/// <summary>The thread is waiting for an unknown reason.</summary>
		// Token: 0x040011FE RID: 4606
		Unknown = 13,
		/// <summary>The thread is waiting for a user request.</summary>
		// Token: 0x040011FF RID: 4607
		UserRequest = 6,
		/// <summary>The thread is waiting for the system to allocate virtual memory.</summary>
		// Token: 0x04001200 RID: 4608
		VirtualMemory = 11
	}
}
