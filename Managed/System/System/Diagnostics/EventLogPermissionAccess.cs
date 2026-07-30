using System;

namespace System.Diagnostics
{
	/// <summary>Defines access levels used by <see cref="T:System.Diagnostics.EventLog" /> permission classes.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001F8 RID: 504
	[Flags]
	public enum EventLogPermissionAccess
	{
		/// <summary>The <see cref="T:System.Diagnostics.EventLog" /> has no permissions.</summary>
		// Token: 0x0400114B RID: 4427
		None = 0,
		/// <summary>The <see cref="T:System.Diagnostics.EventLog" /> can read existing logs. Note This member is now obsolete, use <see cref="F:System.Diagnostics.EventLogPermissionAccess.Administer" /> instead.</summary>
		// Token: 0x0400114C RID: 4428
		[Obsolete]
		Browse = 2,
		/// <summary>The <see cref="T:System.Diagnostics.EventLog" /> can read or write to existing logs, and create event sources and logs. Note This member is now obsolete, use <see cref="F:System.Diagnostics.EventLogPermissionAccess.Write" /> instead.</summary>
		// Token: 0x0400114D RID: 4429
		[Obsolete]
		Instrument = 6,
		/// <summary>The <see cref="T:System.Diagnostics.EventLog" /> can read existing logs, delete event sources or logs, respond to entries, clear an event log, listen to events, and access a collection of all event logs. Note This member is now obsolete, use <see cref="F:System.Diagnostics.EventLogPermissionAccess.Administer" /> instead.</summary>
		// Token: 0x0400114E RID: 4430
		[Obsolete]
		Audit = 10,
		/// <summary>The <see cref="T:System.Diagnostics.EventLog" /> can write to existing logs, and create event sources and logs.</summary>
		// Token: 0x0400114F RID: 4431
		Write = 16,
		/// <summary>The <see cref="T:System.Diagnostics.EventLog" /> can create an event source, read existing logs, delete event sources or logs, respond to entries, clear an event log, listen to events, and access a collection of all event logs.</summary>
		// Token: 0x04001150 RID: 4432
		Administer = 48
	}
}
