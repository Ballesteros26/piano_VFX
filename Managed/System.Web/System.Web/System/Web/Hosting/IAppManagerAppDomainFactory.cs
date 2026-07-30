using System;
using System.Runtime.InteropServices;

namespace System.Web.Hosting
{
	/// <summary>Defines a method used to create an <see cref="T:System.AppDomain" /> instance for a Web-application manager and a method used to stop all <see cref="T:System.AppDomain" /> instances for a Web-application manager.</summary>
	// Token: 0x02000553 RID: 1363
	[Guid("02998279-7175-4D59-AA5A-FB8E44D4CA9D")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface IAppManagerAppDomainFactory
	{
		/// <summary>Creates a new application domain for the specified Web application.</summary>
		/// <returns>A new application domain for the specified Web application.</returns>
		/// <param name="appId">The unique identifier for the new application.</param>
		/// <param name="appPath">The path to the new application's files.</param>
		// Token: 0x06003AFF RID: 15103
		[return: MarshalAs(UnmanagedType.Interface)]
		object Create([MarshalAs(UnmanagedType.BStr)] [In] string appId, [MarshalAs(UnmanagedType.BStr)] [In] string appPath);

		/// <summary>Stops all application domains associated with this application manager. </summary>
		// Token: 0x06003B00 RID: 15104
		void Stop();
	}
}
