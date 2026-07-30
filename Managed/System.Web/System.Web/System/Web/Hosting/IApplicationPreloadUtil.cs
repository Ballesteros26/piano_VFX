using System;
using System.Runtime.InteropServices;

namespace System.Web.Hosting
{
	/// <summary>Provides methods that ASP.NET uses to communicate with IIS 7.0 while the server preloads an application.</summary>
	// Token: 0x02000769 RID: 1897
	[Guid("940D8ADD-9E40-4475-9A67-2CDCDF57995C")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface IApplicationPreloadUtil
	{
		/// <summary>Gets initialization data that is required in order to preload an application. This method is called by ASP.NET.</summary>
		/// <param name="context">The application context. </param>
		/// <param name="enabled">When this method returns, contains true to indicate that the application has to be preloaded; otherwise, false. This parameter is passed uninitialized.</param>
		/// <param name="startupObjType">When this method returns, contains a string that identifies the managed type that is part of the preload process. This parameter is passed uninitialized.</param>
		/// <param name="parametersForStartupObj">When this method returns, contains the data that will be passed to the preloaded type. This parameter is passed uninitialized.</param>
		// Token: 0x06004D37 RID: 19767
		void GetApplicationPreloadInfo([MarshalAs(UnmanagedType.LPWStr)] [In] string context, [MarshalAs(UnmanagedType.Bool)] out bool enabled, [MarshalAs(UnmanagedType.BStr)] out string startupObjType, [MarshalAs(UnmanagedType.SafeArray)] out string[] parametersForStartupObj);

		/// <summary>Reports that an error occurred in IIS 7.0 while the server was preloading the ASP.NET application.</summary>
		/// <param name="context">The application context.</param>
		/// <param name="errorCode">The numeric error code.</param>
		/// <param name="errorMessage">The error text.</param>
		// Token: 0x06004D38 RID: 19768
		void ReportApplicationPreloadFailure([MarshalAs(UnmanagedType.LPWStr)] [In] string context, [MarshalAs(UnmanagedType.U4)] [In] int errorCode, [MarshalAs(UnmanagedType.LPWStr)] [In] string errorMessage);
	}
}
