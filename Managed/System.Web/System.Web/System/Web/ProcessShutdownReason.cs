using System;

namespace System.Web
{
	/// <summary>Provides enumerated values that indicate why a process has shut down.</summary>
	// Token: 0x020000CE RID: 206
	public enum ProcessShutdownReason
	{
		/// <summary>Indicates that the process has not shut down.</summary>
		// Token: 0x0400107D RID: 4221
		None,
		/// <summary>Indicates that the process shut down unexpectedly.</summary>
		// Token: 0x0400107E RID: 4222
		Unexpected,
		/// <summary>Indicates that requests executed by the process exceeded the allowable limit.</summary>
		// Token: 0x0400107F RID: 4223
		RequestsLimit,
		/// <summary>Indicates that requests assigned to the process exceeded the allowable number in the queue.</summary>
		// Token: 0x04001080 RID: 4224
		RequestQueueLimit,
		/// <summary>Indicates that the process restarted because it was active longer than allowed.</summary>
		// Token: 0x04001081 RID: 4225
		Timeout,
		/// <summary>Indicates that the process exceeded the allowable idle time.</summary>
		// Token: 0x04001082 RID: 4226
		IdleTimeout,
		/// <summary>Indicates that the process exceeded the per-process memory limit.</summary>
		// Token: 0x04001083 RID: 4227
		MemoryLimitExceeded,
		/// <summary>Indicates that the worker process did not respond to a ping from the Internet Information Services (IIS) process.</summary>
		// Token: 0x04001084 RID: 4228
		PingFailed,
		/// <summary>Indicates that a deadlock was suspected because the response time limit was exceeded with requests in the queue.</summary>
		// Token: 0x04001085 RID: 4229
		DeadlockSuspected
	}
}
