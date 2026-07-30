using System;

namespace System.Diagnostics
{
	/// <summary>Specifies trace data options to be written to the trace output.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001CA RID: 458
	[Flags]
	public enum TraceOptions
	{
		/// <summary>Do not write any elements.</summary>
		// Token: 0x0400107A RID: 4218
		None = 0,
		/// <summary>Write the logical operation stack, which is represented by the return value of the <see cref="P:System.Diagnostics.CorrelationManager.LogicalOperationStack" /> property.</summary>
		// Token: 0x0400107B RID: 4219
		LogicalOperationStack = 1,
		/// <summary>Write the date and time. </summary>
		// Token: 0x0400107C RID: 4220
		DateTime = 2,
		/// <summary>Write the timestamp, which is represented by the return value of the <see cref="M:System.Diagnostics.Stopwatch.GetTimestamp" /> method.</summary>
		// Token: 0x0400107D RID: 4221
		Timestamp = 4,
		/// <summary>Write the process identity, which is represented by the return value of the <see cref="P:System.Diagnostics.Process.Id" /> property.</summary>
		// Token: 0x0400107E RID: 4222
		ProcessId = 8,
		/// <summary>Write the thread identity, which is represented by the return value of the <see cref="P:System.Threading.Thread.ManagedThreadId" /> property for the current thread.</summary>
		// Token: 0x0400107F RID: 4223
		ThreadId = 16,
		/// <summary>Write the call stack, which is represented by the return value of the <see cref="P:System.Environment.StackTrace" /> property.</summary>
		// Token: 0x04001080 RID: 4224
		Callstack = 32
	}
}
