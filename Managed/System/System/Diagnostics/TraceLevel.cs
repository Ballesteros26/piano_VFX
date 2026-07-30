using System;

namespace System.Diagnostics
{
	/// <summary>Specifies what messages to output for the <see cref="T:System.Diagnostics.Debug" />, <see cref="T:System.Diagnostics.Trace" /> and <see cref="T:System.Diagnostics.TraceSwitch" /> classes.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001C7 RID: 455
	public enum TraceLevel
	{
		/// <summary>Output no tracing and debugging messages.</summary>
		// Token: 0x0400106B RID: 4203
		Off,
		/// <summary>Output error-handling messages.</summary>
		// Token: 0x0400106C RID: 4204
		Error,
		/// <summary>Output warnings and error-handling messages.</summary>
		// Token: 0x0400106D RID: 4205
		Warning,
		/// <summary>Output informational messages, warnings, and error-handling messages.</summary>
		// Token: 0x0400106E RID: 4206
		Info,
		/// <summary>Output all debugging and tracing messages.</summary>
		// Token: 0x0400106F RID: 4207
		Verbose
	}
}
