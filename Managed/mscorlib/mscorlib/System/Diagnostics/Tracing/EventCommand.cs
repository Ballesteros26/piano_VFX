using System;

namespace System.Diagnostics.Tracing
{
	/// <summary>Describes the command (<see cref="P:System.Diagnostics.Tracing.EventCommandEventArgs.Command" /> property) that is passed to the <see cref="M:System.Diagnostics.Tracing.EventSource.OnEventCommand(System.Diagnostics.Tracing.EventCommandEventArgs)" /> callback.</summary>
	// Token: 0x02000B08 RID: 2824
	public enum EventCommand
	{
		/// <summary>Update the event.</summary>
		// Token: 0x0400328F RID: 12943
		Update,
		/// <summary>Send the manifest.</summary>
		// Token: 0x04003290 RID: 12944
		SendManifest = -1,
		/// <summary>Enable the event.</summary>
		// Token: 0x04003291 RID: 12945
		Enable = -2,
		/// <summary>Disable the event.</summary>
		// Token: 0x04003292 RID: 12946
		Disable = -3
	}
}
