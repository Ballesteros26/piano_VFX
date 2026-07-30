using System;

namespace System.Diagnostics.Eventing.Reader
{
	/// <summary>Defines the type of events that are logged in an event log. Each log can only contain one type of event.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000391 RID: 913
	public enum EventLogType
	{
		/// <summary>These events are primarily for end users, administrators, and support. The events that are found in the Administrative type logs indicate a problem and a well-defined solution that an administrator can act on. An example of an administrative event is an event that occurs when an application fails to connect to a printer. </summary>
		// Token: 0x04000C15 RID: 3093
		Administrative,
		/// <summary>Events in an analytic event log are published in high volume. They describe program operation and indicate problems that cannot be handled by user intervention.</summary>
		// Token: 0x04000C16 RID: 3094
		Analytical = 2,
		/// <summary>Events in a debug type event log are used solely by developers to diagnose a problem for debugging.</summary>
		// Token: 0x04000C17 RID: 3095
		Debug,
		/// <summary>Events in an operational type event log are used for analyzing and diagnosing a problem or occurrence. They can be used to trigger tools or tasks based on the problem or occurrence. An example of an operational event is an event that occurs when a printer is added or removed from a system.</summary>
		// Token: 0x04000C18 RID: 3096
		Operational = 1
	}
}
