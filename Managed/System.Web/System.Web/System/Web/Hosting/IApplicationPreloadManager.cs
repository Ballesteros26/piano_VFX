using System;
using System.Runtime.InteropServices;

namespace System.Web.Hosting
{
	/// <summary>Provides methods that ASP.NET uses to invoke the application-preload feature in IIS 7.0.</summary>
	// Token: 0x02000768 RID: 1896
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("AE54F424-71BC-4da5-AA2F-8C0CD53496FC")]
	public interface IApplicationPreloadManager
	{
		/// <summary>Called by IIS 7.0 to notify ASP.NET whether an application should be preloaded.</summary>
		/// <param name="context">The application context.</param>
		/// <param name="appId">The unique ID of the application. </param>
		/// <param name="enabled">true to indicate that the application should be preloaded; otherwise, false. </param>
		// Token: 0x06004D35 RID: 19765
		void SetApplicationPreloadState([MarshalAs(UnmanagedType.LPWStr)] [In] string context, [MarshalAs(UnmanagedType.LPWStr)] [In] string appId, [MarshalAs(UnmanagedType.Bool)] [In] bool enabled);

		/// <summary>Calls IIS 7.0 to get information that is required in order to preload an application. </summary>
		/// <param name="preloadUtil">The handle to an unmanaged interface in IIS 7.0 that ASP.NET calls to get information. </param>
		// Token: 0x06004D36 RID: 19766
		void SetApplicationPreloadUtil([MarshalAs(UnmanagedType.Interface)] [In] IApplicationPreloadUtil preloadUtil);
	}
}
