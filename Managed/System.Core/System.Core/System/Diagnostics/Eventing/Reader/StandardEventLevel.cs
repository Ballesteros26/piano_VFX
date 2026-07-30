using System;

namespace System.Diagnostics.Eventing.Reader
{
	/// <summary>Defines the standard event levels that are used in the Event Log service. The level defines the severity of the event. Custom event levels can be defined beyond these standard levels. For more information about levels, see <see cref="T:System.Diagnostics.Eventing.Reader.EventLevel" />.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020003A6 RID: 934
	public enum StandardEventLevel
	{
		/// <summary>This level corresponds to critical errors, which is a serious error that has caused a major failure. </summary>
		// Token: 0x04000C25 RID: 3109
		Critical = 1,
		/// <summary>This level corresponds to normal errors that signify a problem. </summary>
		// Token: 0x04000C26 RID: 3110
		Error,
		/// <summary>This level corresponds to informational events or messages that are not errors. These events can help trace the progress or state of an application.</summary>
		// Token: 0x04000C27 RID: 3111
		Informational = 4,
		/// <summary>This value indicates that not filtering on the level is done during the event publishing.</summary>
		// Token: 0x04000C28 RID: 3112
		LogAlways = 0,
		/// <summary>This level corresponds to lengthy events or messages. </summary>
		// Token: 0x04000C29 RID: 3113
		Verbose = 5,
		/// <summary>This level corresponds to warning events. For example, an event that gets published because a disk is nearing full capacity is a warning event.</summary>
		// Token: 0x04000C2A RID: 3114
		Warning = 3
	}
}
