using System;

namespace System.Diagnostics.Tracing
{
	/// <summary>Identifies the level of an event.</summary>
	// Token: 0x02000B19 RID: 2841
	public enum EventLevel
	{
		/// <summary>No level filtering is done on the event.</summary>
		// Token: 0x040032D9 RID: 13017
		LogAlways,
		/// <summary>This level corresponds to a critical error, which is a serious error that has caused a major failure.</summary>
		// Token: 0x040032DA RID: 13018
		Critical,
		/// <summary>This level adds standard errors that signify a problem.</summary>
		// Token: 0x040032DB RID: 13019
		Error,
		/// <summary>This level adds warning events (for example, events that are published because a disk is nearing full capacity).</summary>
		// Token: 0x040032DC RID: 13020
		Warning,
		/// <summary>This level adds informational events or messages that are not errors. These events can help trace the progress or state of an application.</summary>
		// Token: 0x040032DD RID: 13021
		Informational,
		/// <summary>This level adds lengthy events or messages. It causes all events to be logged.</summary>
		// Token: 0x040032DE RID: 13022
		Verbose
	}
}
