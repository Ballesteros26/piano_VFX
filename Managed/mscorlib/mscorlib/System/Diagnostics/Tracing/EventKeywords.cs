using System;

namespace System.Diagnostics.Tracing
{
	/// <summary>Defines the standard keywords that apply to events.</summary>
	// Token: 0x02000B1D RID: 2845
	[Flags]
	public enum EventKeywords : long
	{
		/// <summary>No filtering on keywords is performed when the event is published.</summary>
		// Token: 0x040032F4 RID: 13044
		None = 0L,
		// Token: 0x040032F5 RID: 13045
		All = -1L,
		// Token: 0x040032F6 RID: 13046
		MicrosoftTelemetry = 562949953421312L,
		/// <summary>Attached to all Windows Diagnostics Infrastructure (WDI) context events.</summary>
		// Token: 0x040032F7 RID: 13047
		WdiContext = 562949953421312L,
		/// <summary>Attached to all Windows Diagnostics Infrastructure (WDI) diagnostic events.</summary>
		// Token: 0x040032F8 RID: 13048
		WdiDiagnostic = 1125899906842624L,
		/// <summary>Attached to all Service Quality Mechanism (SQM) events.</summary>
		// Token: 0x040032F9 RID: 13049
		Sqm = 2251799813685248L,
		/// <summary>Attached to all failed security audit events. Use this keyword only  for events in the security log.</summary>
		// Token: 0x040032FA RID: 13050
		AuditFailure = 4503599627370496L,
		/// <summary>Attached to all successful security audit events. Use this keyword only for events in the security log.</summary>
		// Token: 0x040032FB RID: 13051
		AuditSuccess = 9007199254740992L,
		/// <summary>Attached to transfer events where the related activity ID (correlation ID) is a computed value and is not guaranteed to be unique (that is, it is not a real GUID).</summary>
		// Token: 0x040032FC RID: 13052
		CorrelationHint = 4503599627370496L,
		/// <summary>Attached to events that are raised by using the RaiseEvent function.</summary>
		// Token: 0x040032FD RID: 13053
		EventLogClassic = 36028797018963968L
	}
}
