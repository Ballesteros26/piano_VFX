using System;

namespace System.Diagnostics.Eventing.Reader
{
	/// <summary>Determines the behavior for the event log service handles an event log when the log reaches its maximum allowed size (when the event log is full).</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000390 RID: 912
	public enum EventLogMode
	{
		/// <summary>Archive the log when full, do not overwrite events. The log is automatically archived when necessary. No events are overwritten. </summary>
		// Token: 0x04000C11 RID: 3089
		AutoBackup = 1,
		/// <summary>New events continue to be stored when the log file is full. Each new incoming event replaces the oldest event in the log.</summary>
		// Token: 0x04000C12 RID: 3090
		Circular = 0,
		/// <summary>Do not overwrite events. Clear the log manually rather than automatically.</summary>
		// Token: 0x04000C13 RID: 3091
		Retain = 2
	}
}
