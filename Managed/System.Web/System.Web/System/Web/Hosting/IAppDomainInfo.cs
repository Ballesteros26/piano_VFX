using System;
using System.Runtime.InteropServices;

namespace System.Web.Hosting
{
	/// <summary>Provides information about the application domain.</summary>
	// Token: 0x0200075E RID: 1886
	[Guid("5BC9C234-6CD7-49bf-A07A-6FDB7F22DFFF")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface IAppDomainInfo
	{
		/// <summary>Gets the application domain ID.</summary>
		/// <returns>The unique application domain ID.</returns>
		// Token: 0x06004D0D RID: 19725
		[return: MarshalAs(UnmanagedType.BStr)]
		string GetId();

		/// <summary>Gets the physical path of the application domain.</summary>
		/// <returns>The physical path of the application domain.</returns>
		// Token: 0x06004D0E RID: 19726
		[return: MarshalAs(UnmanagedType.BStr)]
		string GetPhysicalPath();

		/// <summary>Gets the site ID of the application domain.</summary>
		/// <returns>The site ID of the application domain.</returns>
		// Token: 0x06004D0F RID: 19727
		[return: MarshalAs(UnmanagedType.I4)]
		int GetSiteId();

		/// <summary>Gets the root virtual path of the application domain.</summary>
		/// <returns>The root virtual path of the application domain.</returns>
		// Token: 0x06004D10 RID: 19728
		[return: MarshalAs(UnmanagedType.BStr)]
		string GetVirtualPath();

		/// <summary>Gets the state of the application domain.</summary>
		/// <returns>true if the application domain is idle; otherwise, false.</returns>
		// Token: 0x06004D11 RID: 19729
		[return: MarshalAs(UnmanagedType.Bool)]
		bool IsIdle();
	}
}
