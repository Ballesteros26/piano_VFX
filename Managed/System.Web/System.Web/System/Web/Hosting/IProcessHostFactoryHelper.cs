using System;
using System.Runtime.InteropServices;

namespace System.Web.Hosting
{
	/// <summary>Provides a method to retrieve an <see cref="T:System.Web.Hosting.IProcessHost" /> interface.</summary>
	// Token: 0x0200076C RID: 1900
	[Guid("02fd465d-5c5d-4b7e-95b6-82faa031b74a")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface IProcessHostFactoryHelper
	{
		/// <summary>Gets the process host.</summary>
		/// <returns>A process host object.</returns>
		/// <param name="functions">Functions that are declared by the <see cref="T:System.Web.Hosting.IProcessHostSupportFunctions" /> interface.</param>
		// Token: 0x06004D40 RID: 19776
		[return: MarshalAs(UnmanagedType.Interface)]
		object GetProcessHost(IProcessHostSupportFunctions functions);
	}
}
