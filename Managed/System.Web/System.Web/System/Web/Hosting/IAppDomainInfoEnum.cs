using System;
using System.Runtime.InteropServices;

namespace System.Web.Hosting
{
	/// <summary>Provides access to an application domain.</summary>
	// Token: 0x02000760 RID: 1888
	[Guid("F79648FB-558B-4a09-88F1-1E3BCB30E34F")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface IAppDomainInfoEnum
	{
		/// <summary>Retrieves the number of application domains.</summary>
		/// <returns>The number of application domains.</returns>
		// Token: 0x06004D17 RID: 19735
		[return: MarshalAs(UnmanagedType.I4)]
		int Count();

		/// <summary>Gets an <see cref="T:System.Web.Hosting.IAppDomainInfo" /> interface.</summary>
		/// <returns>An <see cref="T:System.Web.Hosting.IAppDomainInfo" /> interface.</returns>
		// Token: 0x06004D18 RID: 19736
		[return: MarshalAs(UnmanagedType.Interface)]
		IAppDomainInfo GetData();

		/// <summary>Moves to the next <see cref="T:System.Web.Hosting.IAppDomainInfo" /> interface.</summary>
		/// <returns>true if a new interface is available; otherwise, false.</returns>
		// Token: 0x06004D19 RID: 19737
		[return: MarshalAs(UnmanagedType.Bool)]
		bool MoveNext();

		/// <summary>Initializes the <see cref="T:System.Web.Hosting.IAppDomainInfo" /> interface.</summary>
		// Token: 0x06004D1A RID: 19738
		void Reset();
	}
}
