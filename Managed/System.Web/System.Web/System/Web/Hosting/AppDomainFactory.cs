using System;
using System.Security.Permissions;

namespace System.Web.Hosting
{
	/// <summary>Creates a new <see cref="T:System.AppDomain" /> instance for the Web application. This class cannot be inherited. This class was used by earlier versions of the .NET Framework than version 2.0, which uses the <see cref="T:System.Web.Hosting.AppManagerAppDomainFactory" /> class instead.</summary>
	// Token: 0x02000546 RID: 1350
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public sealed class AppDomainFactory : IAppDomainFactory
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Hosting.AppDomainFactory" /> class. This class was used by earlier versions of the .NET Framework than version 2.0, which uses the <see cref="T:System.Web.Hosting.AppManagerAppDomainFactory" /> class instead.</summary>
		// Token: 0x06003A8A RID: 14986 RVA: 0x00002050 File Offset: 0x00000250
		[AspNetHostingPermission(SecurityAction.Demand, Level = AspNetHostingPermissionLevel.Minimal)]
		[SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
		public AppDomainFactory()
		{
		}

		/// <summary>Returns a new application domain for the specified Web application. This class was used by earlier versions of the .NET Framework than version 2.0, which uses the <see cref="T:System.Web.Hosting.AppManagerAppDomainFactory" /> class instead.</summary>
		/// <returns>A new application domain.</returns>
		/// <param name="module">The module containing the Web application.</param>
		/// <param name="typeName">The type of the Web application.</param>
		/// <param name="appId">The unique identifier for the Web application.</param>
		/// <param name="appPath">The path to the Web application's files.</param>
		/// <param name="strUrlOfAppOrigin">The URL of origin for the Web application.</param>
		/// <param name="iZone">The zone of origin for the Web application.</param>
		// Token: 0x06003A8B RID: 14987 RVA: 0x00003A1F File Offset: 0x00001C1F
		[global::System.MonoTODO("Not implemented")]
		public object Create(string module, string typeName, string appId, string appPath, string strUrlOfAppOrigin, int iZone)
		{
			throw new NotImplementedException();
		}
	}
}
