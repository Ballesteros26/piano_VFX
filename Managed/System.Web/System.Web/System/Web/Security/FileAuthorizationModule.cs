using System;
using System.Security.Permissions;

namespace System.Web.Security
{
	/// <summary>Verifies that the user has permission to access the file requested. This class cannot be inherited.</summary>
	// Token: 0x020004BD RID: 1213
	[global::System.MonoTODO("that's only a stub")]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public sealed class FileAuthorizationModule : IHttpModule
	{
		/// <summary>Creates an instance of the <see cref="T:System.Web.Security.FileAuthorizationModule" /> class.</summary>
		// Token: 0x06003685 RID: 13957 RVA: 0x00002050 File Offset: 0x00000250
		[SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
		public FileAuthorizationModule()
		{
		}

		/// <summary>Releases all resources, other than memory, used by the <see cref="T:System.Web.Security.FileAuthorizationModule" />.</summary>
		// Token: 0x06003686 RID: 13958 RVA: 0x0000393A File Offset: 0x00001B3A
		public void Dispose()
		{
		}

		/// <summary>Initializes the <see cref="T:System.Web.Security.FileAuthorizationModule" /> object.</summary>
		/// <param name="app">The current <see cref="T:System.Web.HttpApplication" /> instance. </param>
		// Token: 0x06003687 RID: 13959 RVA: 0x00003A1F File Offset: 0x00001C1F
		[global::System.MonoTODO("Not implemented")]
		public void Init(HttpApplication app)
		{
			throw new NotImplementedException();
		}

		/// <summary>Determines whether the user has access to the requested file.</summary>
		/// <returns>true if the current Windows user represented by <paramref name="token" /> has access to the file using the specified HTTP verb or if the <see cref="T:System.Web.Security.FileAuthorizationModule" /> module is not defined in the application's configuration file; otherwise, false.</returns>
		/// <param name="virtualPath">The virtual path to the file.</param>
		/// <param name="token">A Windows access token representing the user.</param>
		/// <param name="verb">The HTTP verb used to make the request.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="virtualPath" /> is null.-or-<paramref name="token" /> is <see cref="F:System.IntPtr.Zero" />.-or-<paramref name="verb" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="virtualPath" /> is not in the application directory structure of the Web application.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The file specified by <paramref name="virtualPath" /> does not exist.</exception>
		// Token: 0x06003688 RID: 13960 RVA: 0x00003A1F File Offset: 0x00001C1F
		[global::System.MonoTODO("Not implemented")]
		public static bool CheckFileAccessForUser(string virtualPath, IntPtr token, string verb)
		{
			throw new NotImplementedException();
		}
	}
}
