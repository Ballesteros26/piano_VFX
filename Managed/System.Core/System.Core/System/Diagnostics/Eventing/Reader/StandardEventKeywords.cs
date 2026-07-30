using System;

namespace System.Diagnostics.Eventing.Reader
{
	/// <summary>Defines the standard keywords that are attached to events by the event provider. For more information about keywords, see <see cref="T:System.Diagnostics.Eventing.Reader.EventKeyword" />.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020003A5 RID: 933
	[Flags]
	public enum StandardEventKeywords : long
	{
		/// <summary>Attached to all failed security audit events. This keyword should only be used for events in the Security log.</summary>
		// Token: 0x04000C1A RID: 3098
		AuditFailure = 4503599627370496L,
		/// <summary>Attached to all successful security audit events. This keyword should only be used for events in the Security log.</summary>
		// Token: 0x04000C1B RID: 3099
		AuditSuccess = 9007199254740992L,
		/// <summary>Attached to transfer events where the related Activity ID (Correlation ID) is a computed value and is not guaranteed to be unique (not a real GUID).</summary>
		// Token: 0x04000C1C RID: 3100
		[Obsolete("Incorrect value: use CorrelationHint2 instead", false)]
		CorrelationHint = 4503599627370496L,
		/// <summary>Attached to transfer events where the related Activity ID (Correlation ID) is a computed value and is not guaranteed to be unique (not a real GUID).</summary>
		// Token: 0x04000C1D RID: 3101
		CorrelationHint2 = 18014398509481984L,
		/// <summary>Attached to events which are raised using the RaiseEvent function.</summary>
		// Token: 0x04000C1E RID: 3102
		EventLogClassic = 36028797018963968L,
		/// <summary>This value indicates that no filtering on keyword is performed when the event is published.</summary>
		// Token: 0x04000C1F RID: 3103
		None = 0L,
		/// <summary>Attached to all response time events. </summary>
		// Token: 0x04000C20 RID: 3104
		ResponseTime = 281474976710656L,
		/// <summary>Attached to all Service Quality Mechanism (SQM) events.</summary>
		// Token: 0x04000C21 RID: 3105
		Sqm = 2251799813685248L,
		/// <summary>Attached to all Windows Diagnostic Infrastructure (WDI) context events.</summary>
		// Token: 0x04000C22 RID: 3106
		WdiContext = 562949953421312L,
		/// <summary>Attached to all Windows Diagnostic Infrastructure (WDI) diagnostic events.</summary>
		// Token: 0x04000C23 RID: 3107
		WdiDiagnostic = 1125899906842624L
	}
}
