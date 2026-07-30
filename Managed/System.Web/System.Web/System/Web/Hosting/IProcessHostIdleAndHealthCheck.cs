using System;
using System.Runtime.InteropServices;

namespace System.Web.Hosting
{
	/// <summary>Provides ways to check on the state of a process.</summary>
	// Token: 0x0200076D RID: 1901
	[Guid("9d98b251-453e-44f6-9cec-8b5aed970129")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface IProcessHostIdleAndHealthCheck
	{
		/// <summary>Gets the state of the application domain.</summary>
		/// <returns>true if the application domain is idle; otherwise, false.</returns>
		// Token: 0x06004D41 RID: 19777
		[return: MarshalAs(UnmanagedType.Bool)]
		bool IsIdle();

		/// <summary>Pings a process.</summary>
		/// <param name="callback">The callback to handle the ping response.</param>
		// Token: 0x06004D42 RID: 19778
		void Ping(IProcessPingCallback callback);
	}
}
