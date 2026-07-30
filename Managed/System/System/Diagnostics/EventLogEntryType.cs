using System;

namespace System.Diagnostics
{
	/// <summary>Specifies the event type of an event log entry.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001F4 RID: 500
	public enum EventLogEntryType
	{
		/// <summary>An error event. This indicates a significant problem the user should know about; usually a loss of functionality or data.</summary>
		// Token: 0x04001143 RID: 4419
		Error = 1,
		/// <summary>A warning event. This indicates a problem that is not immediately significant, but that may signify conditions that could cause future problems.</summary>
		// Token: 0x04001144 RID: 4420
		Warning,
		/// <summary>An information event. This indicates a significant, successful operation.</summary>
		// Token: 0x04001145 RID: 4421
		Information = 4,
		/// <summary>A success audit event. This indicates a security event that occurs when an audited access attempt is successful; for example, logging on successfully.</summary>
		// Token: 0x04001146 RID: 4422
		SuccessAudit = 8,
		/// <summary>A failure audit event. This indicates a security event that occurs when an audited access attempt fails; for example, a failed attempt to open a file.</summary>
		// Token: 0x04001147 RID: 4423
		FailureAudit = 16
	}
}
