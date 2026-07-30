using System;

namespace System.Diagnostics
{
	/// <summary>Specifies how to handle entries in an event log that has reached its maximum file size.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000206 RID: 518
	public enum OverflowAction
	{
		/// <summary>Indicates that existing entries are retained when the event log is full and new entries are discarded.</summary>
		// Token: 0x04001183 RID: 4483
		DoNotOverwrite = -1,
		/// <summary>Indicates that each new entry overwrites the oldest entry when the event log is full.</summary>
		// Token: 0x04001184 RID: 4484
		OverwriteAsNeeded,
		/// <summary>Indicates that new events overwrite events older than specified by the <see cref="P:System.Diagnostics.EventLog.MinimumRetentionDays" /> property value when the event log is full. New events are discarded if the event log is full and there are no events older than specified by the <see cref="P:System.Diagnostics.EventLog.MinimumRetentionDays" /> property value.</summary>
		// Token: 0x04001185 RID: 4485
		OverwriteOlder
	}
}
