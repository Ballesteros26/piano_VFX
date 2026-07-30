using System;

namespace System.Diagnostics.Eventing.Reader
{
	/// <summary>Defines the standard opcodes that are attached to events by the event provider. For more information about opcodes, see <see cref="T:System.Diagnostics.Eventing.Reader.EventOpcode" />.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020003A7 RID: 935
	public enum StandardEventOpcode
	{
		/// <summary>An event with this opcode is a trace collection start event.</summary>
		// Token: 0x04000C2C RID: 3116
		DataCollectionStart = 3,
		/// <summary>An event with this opcode is a trace collection stop event.</summary>
		// Token: 0x04000C2D RID: 3117
		DataCollectionStop,
		/// <summary>An event with this opcode is an extension event.</summary>
		// Token: 0x04000C2E RID: 3118
		Extension,
		/// <summary>An event with this opcode is an informational event.</summary>
		// Token: 0x04000C2F RID: 3119
		Info = 0,
		/// <summary>An event with this opcode is published when one activity in an application receives data.</summary>
		// Token: 0x04000C30 RID: 3120
		Receive = 240,
		/// <summary>An event with this opcode is published after an activity in an application replies to an event.</summary>
		// Token: 0x04000C31 RID: 3121
		Reply = 6,
		/// <summary>An event with this opcode is published after an activity in an application resumes from a suspended state. The event should follow an event with the Suspend opcode.</summary>
		// Token: 0x04000C32 RID: 3122
		Resume,
		/// <summary>An event with this opcode is published when one activity in an application transfers data or system resources to another activity. </summary>
		// Token: 0x04000C33 RID: 3123
		Send = 9,
		/// <summary>An event with this opcode is published when an application starts a new transaction or activity. This can be embedded into another transaction or activity when multiple events with the Start opcode follow each other without an event with a Stop opcode.</summary>
		// Token: 0x04000C34 RID: 3124
		Start = 1,
		/// <summary>An event with this opcode is published when an activity or a transaction in an application ends. The event corresponds to the last unpaired event with a Start opcode.</summary>
		// Token: 0x04000C35 RID: 3125
		Stop,
		/// <summary>An event with this opcode is published when an activity in an application is suspended. </summary>
		// Token: 0x04000C36 RID: 3126
		Suspend = 8
	}
}
