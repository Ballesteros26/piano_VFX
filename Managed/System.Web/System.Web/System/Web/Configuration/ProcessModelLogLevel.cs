using System;

namespace System.Web.Configuration
{
	/// <summary>Specifies the event types to be logged to the event log.</summary>
	// Token: 0x02000571 RID: 1393
	public enum ProcessModelLogLevel
	{
		/// <summary>Specifies that no events are logged. This field is constant.</summary>
		// Token: 0x04002044 RID: 8260
		None,
		/// <summary>Specifies that all process events are logged. This field is constant.</summary>
		// Token: 0x04002045 RID: 8261
		All,
		/// <summary>Specifies that only unexpected shutdowns, memory-limit shutdowns, and deadlock shutdowns are logged. This field is constant.</summary>
		// Token: 0x04002046 RID: 8262
		Errors
	}
}
