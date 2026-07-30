using System;
using System.ComponentModel;

namespace System.Diagnostics
{
	/// <summary>Specifies the levels of trace messages filtered by the source switch and event type filter.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001B9 RID: 441
	[Flags]
	public enum SourceLevels
	{
		/// <summary>Does not allow any events through.</summary>
		// Token: 0x0400102A RID: 4138
		Off = 0,
		/// <summary>Allows only <see cref="F:System.Diagnostics.TraceEventType.Critical" /> events through.</summary>
		// Token: 0x0400102B RID: 4139
		Critical = 1,
		/// <summary>Allows <see cref="F:System.Diagnostics.TraceEventType.Critical" /> and <see cref="F:System.Diagnostics.TraceEventType.Error" /> events through.</summary>
		// Token: 0x0400102C RID: 4140
		Error = 3,
		/// <summary>Allows <see cref="F:System.Diagnostics.TraceEventType.Critical" />, <see cref="F:System.Diagnostics.TraceEventType.Error" />, and <see cref="F:System.Diagnostics.TraceEventType.Warning" /> events through.</summary>
		// Token: 0x0400102D RID: 4141
		Warning = 7,
		/// <summary>Allows <see cref="F:System.Diagnostics.TraceEventType.Critical" />, <see cref="F:System.Diagnostics.TraceEventType.Error" />, <see cref="F:System.Diagnostics.TraceEventType.Warning" />, and <see cref="F:System.Diagnostics.TraceEventType.Information" /> events through.</summary>
		// Token: 0x0400102E RID: 4142
		Information = 15,
		/// <summary>Allows <see cref="F:System.Diagnostics.TraceEventType.Critical" />, <see cref="F:System.Diagnostics.TraceEventType.Error" />, <see cref="F:System.Diagnostics.TraceEventType.Warning" />, <see cref="F:System.Diagnostics.TraceEventType.Information" />, and <see cref="F:System.Diagnostics.TraceEventType.Verbose" /> events through.</summary>
		// Token: 0x0400102F RID: 4143
		Verbose = 31,
		/// <summary>Allows the <see cref="F:System.Diagnostics.TraceEventType.Stop" />, <see cref="F:System.Diagnostics.TraceEventType.Start" />, <see cref="F:System.Diagnostics.TraceEventType.Suspend" />, <see cref="F:System.Diagnostics.TraceEventType.Transfer" />, and <see cref="F:System.Diagnostics.TraceEventType.Resume" /> events through.</summary>
		// Token: 0x04001030 RID: 4144
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		ActivityTracing = 65280,
		/// <summary>Allows all events through.</summary>
		// Token: 0x04001031 RID: 4145
		All = -1
	}
}
