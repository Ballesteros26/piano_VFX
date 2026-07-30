using System;
using System.Runtime.InteropServices;

namespace System.Web.Hosting
{
	/// <summary>Defines a single method to create a new <see cref="T:System.AppDomain" /> instance for a Web application. This interface was used by .NET Framework versions earlier than 2.0; version 2.0 uses the <see cref="T:System.Web.Hosting.IAppManagerAppDomainFactory" /> interface instead.</summary>
	// Token: 0x02000552 RID: 1362
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("e6e21054-a7dc-4378-877d-b7f4a2d7e8ba")]
	[ComImport]
	public interface IAppDomainFactory
	{
		/// <summary>Creates a new application domain for the specified Web application. This interface was used by .NET Framework versions earlier than 2.0; version 2.0 uses the <see cref="T:System.Web.Hosting.IAppManagerAppDomainFactory" /> interface instead.</summary>
		/// <returns>A new application domain for the specified Web application.</returns>
		/// <param name="module">The module containing the Web application.</param>
		/// <param name="typeName">The type of the Web application.</param>
		/// <param name="appId">The unique identifier for the Web application.</param>
		/// <param name="appPath">The path to the Web application's files.</param>
		/// <param name="strUrlOfAppOrigin">The URL of origin for the Web application.</param>
		/// <param name="iZone">The zone of origin for the Web application.</param>
		// Token: 0x06003AFE RID: 15102
		[return: MarshalAs(UnmanagedType.Interface)]
		object Create([MarshalAs(UnmanagedType.BStr)] [In] string module, [MarshalAs(UnmanagedType.BStr)] [In] string typeName, [MarshalAs(UnmanagedType.BStr)] [In] string appId, [MarshalAs(UnmanagedType.BStr)] [In] string appPath, [MarshalAs(UnmanagedType.BStr)] [In] string strUrlOfAppOrigin, [MarshalAs(UnmanagedType.I4)] [In] int iZone);
	}
}
