using System;
using System.Security.Permissions;

namespace System.Diagnostics
{
	/// <summary>Directs tracing or debugging output to either the standard output or the standard error stream.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001AA RID: 426
	[HostProtection(SecurityAction.LinkDemand, Synchronization = true)]
	public class ConsoleTraceListener : TextWriterTraceListener
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.ConsoleTraceListener" /> class with trace output written to the standard output stream.</summary>
		// Token: 0x06000C71 RID: 3185 RVA: 0x0003D3D8 File Offset: 0x0003B5D8
		public ConsoleTraceListener()
			: base(Console.Out)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.ConsoleTraceListener" /> class with an option to write trace output to the standard output stream or the standard error stream.</summary>
		/// <param name="useErrorStream">true to write tracing and debugging output to the standard error stream; false to write tracing and debugging output to the standard output stream.</param>
		// Token: 0x06000C72 RID: 3186 RVA: 0x0003D3E5 File Offset: 0x0003B5E5
		public ConsoleTraceListener(bool useErrorStream)
			: base(useErrorStream ? Console.Error : Console.Out)
		{
		}

		/// <summary>Closes the output to the stream specified for this trace listener.</summary>
		// Token: 0x06000C73 RID: 3187 RVA: 0x000027E8 File Offset: 0x000009E8
		public override void Close()
		{
		}
	}
}
