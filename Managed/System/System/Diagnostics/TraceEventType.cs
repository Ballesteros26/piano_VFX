using System;
using System.ComponentModel;

namespace System.Diagnostics
{
	/// <summary>Identifies the type of event that has caused the trace.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001C4 RID: 452
	public enum TraceEventType
	{
		/// <summary>Fatal error or application crash.</summary>
		// Token: 0x04001056 RID: 4182
		Critical = 1,
		/// <summary>Recoverable error.</summary>
		// Token: 0x04001057 RID: 4183
		Error,
		/// <summary>Noncritical problem.</summary>
		// Token: 0x04001058 RID: 4184
		Warning = 4,
		/// <summary>Informational message.</summary>
		// Token: 0x04001059 RID: 4185
		Information = 8,
		/// <summary>Debugging trace.</summary>
		// Token: 0x0400105A RID: 4186
		Verbose = 16,
		/// <summary>Starting of a logical operation.</summary>
		// Token: 0x0400105B RID: 4187
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		Start = 256,
		/// <summary>Stopping of a logical operation.</summary>
		// Token: 0x0400105C RID: 4188
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		Stop = 512,
		/// <summary>Suspension of a logical operation.</summary>
		// Token: 0x0400105D RID: 4189
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		Suspend = 1024,
		/// <summary>Resumption of a logical operation.</summary>
		// Token: 0x0400105E RID: 4190
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		Resume = 2048,
		/// <summary>Changing of correlation identity.</summary>
		// Token: 0x0400105F RID: 4191
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		Transfer = 4096
	}
}
