using System;
using System.Runtime.InteropServices;

namespace System.Web.Hosting
{
	/// <summary>Defines the set of process-wide functionality that every host of the application manager must implement.</summary>
	// Token: 0x0200076B RID: 1899
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("0ccd465e-3114-4ca3-ad50-cea561307e93")]
	public interface IProcessHost
	{
		/// <summary>Gets an <see cref="T:System.Web.Hosting.AppDomainInfoEnum" /> interface.</summary>
		/// <param name="appDomainInfoEnum">The <see cref="T:System.Web.Hosting.AppDomainInfoEnum" /> interface.</param>
		// Token: 0x06004D3C RID: 19772
		void EnumerateAppDomains([MarshalAs(UnmanagedType.Interface)] out IAppDomainInfoEnum appDomainInfoEnum);

		/// <summary>Sends a request to terminate all applications in an application domain.</summary>
		// Token: 0x06004D3D RID: 19773
		void Shutdown();

		/// <summary>Terminates the specified application.</summary>
		/// <param name="appId">The application to terminate.</param>
		// Token: 0x06004D3E RID: 19774
		void ShutdownApplication([MarshalAs(UnmanagedType.LPWStr)] [In] string appId);

		/// <summary>Starts the specified application</summary>
		/// <param name="appId">The unique application ID.</param>
		/// <param name="appPath">The virtual path to the application.</param>
		/// <param name="runtimeInterface">A runtime manager interface.</param>
		// Token: 0x06004D3F RID: 19775
		void StartApplication([MarshalAs(UnmanagedType.LPWStr)] [In] string appId, [MarshalAs(UnmanagedType.LPWStr)] [In] string appPath, [MarshalAs(UnmanagedType.Interface)] out object runtimeInterface);
	}
}
