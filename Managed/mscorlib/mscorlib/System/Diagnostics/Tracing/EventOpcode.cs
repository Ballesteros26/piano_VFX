using System;
using System.Runtime.CompilerServices;

namespace System.Diagnostics.Tracing
{
	/// <summary>Defines the standard operation codes that the event source attaches to events.</summary>
	// Token: 0x02000B1B RID: 2843
	[FriendAccessAllowed]
	public enum EventOpcode
	{
		/// <summary>An informational event.</summary>
		// Token: 0x040032E2 RID: 13026
		Info,
		/// <summary>An event that is published when an application starts a new transaction or activity. This operation code can be embedded within another transaction or activity when multiple events that have the <see cref="F:System.Diagnostics.Tracing.EventOpcode.Start" /> code follow each other without an intervening event that has a <see cref="F:System.Diagnostics.Tracing.EventOpcode.Stop" /> code.</summary>
		// Token: 0x040032E3 RID: 13027
		Start,
		/// <summary>An event that is published when an activity or a transaction in an application ends. The event corresponds to the last unpaired event that has a <see cref="F:System.Diagnostics.Tracing.EventOpcode.Start" /> operation code.</summary>
		// Token: 0x040032E4 RID: 13028
		Stop,
		/// <summary>A trace collection start event.</summary>
		// Token: 0x040032E5 RID: 13029
		DataCollectionStart,
		/// <summary>A trace collection stop event.</summary>
		// Token: 0x040032E6 RID: 13030
		DataCollectionStop,
		/// <summary>An extension event.</summary>
		// Token: 0x040032E7 RID: 13031
		Extension,
		/// <summary>An event that is published after an activity in an application replies to an event.</summary>
		// Token: 0x040032E8 RID: 13032
		Reply,
		/// <summary>An event that is published after an activity in an application resumes from a suspended state. The event should follow an event that has the <see cref="F:System.Diagnostics.Tracing.EventOpcode.Suspend" /> operation code.</summary>
		// Token: 0x040032E9 RID: 13033
		Resume,
		/// <summary>An event that is published when an activity in an application is suspended.</summary>
		// Token: 0x040032EA RID: 13034
		Suspend,
		/// <summary>An event that is published when one activity in an application transfers data or system resources to another activity.</summary>
		// Token: 0x040032EB RID: 13035
		Send,
		/// <summary>An event that is published when one activity in an application receives data.</summary>
		// Token: 0x040032EC RID: 13036
		Receive = 240
	}
}
