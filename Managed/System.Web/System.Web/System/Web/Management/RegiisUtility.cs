using System;
using System.Security.Permissions;
using Unity;

namespace System.Web.Management
{
	/// <summary>Provides authorization utilities to support specific Web-application configuration, assembly registration, and assembly-key container manipulation. This class cannot be inherited.</summary>
	// Token: 0x02000749 RID: 1865
	public sealed class RegiisUtility : IRegiisUtility
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Management.RegiisUtility" /> class. </summary>
		// Token: 0x06004CAE RID: 19630 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public RegiisUtility()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Allows specific manipulation of configuration sections and assembly-key containers based on the supplied parameters.</summary>
		/// <param name="options">The type of action to perform.</param>
		/// <param name="firstArgument">A configuration section or assembly-key container name.</param>
		/// <param name="secondArgument">The configuration file name or account name.</param>
		/// <param name="providerName">The provider name.</param>
		/// <param name="appPath">The application path.</param>
		/// <param name="site">The site reference.</param>
		/// <param name="cspOrLocation">The configuration location.</param>
		/// <param name="keySize">The size of the key.</param>
		/// <param name="exception">The exception to display.</param>
		// Token: 0x06004CAF RID: 19631 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public void ProtectedConfigAction(long options, string firstArgument, string secondArgument, string providerName, string appPath, string site, string cspOrLocation, int keySize, out IntPtr exception)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Allows the executing Microsoft Management Console (MMC) assembly to be registered or unregistered.</summary>
		/// <param name="doReg">0 indicates that the assembly should be unregistered; otherwise, the assembly should be registered.</param>
		/// <param name="typeName">The type of the assembly.</param>
		/// <param name="binaryDirectory">The path of the binary directory.</param>
		/// <param name="exception">The exception to display.</param>
		// Token: 0x06004CB0 RID: 19632 RVA: 0x0000B3E4 File Offset: 0x000095E4
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		public void RegisterAsnetMmcAssembly(int doReg, string typeName, string binaryDirectory, out IntPtr exception)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Allows the executing Web assembly to be registered or unregistered.</summary>
		/// <param name="doReg">0 indicates that the assembly should be unregistered; otherwise, the assembly should be registered.</param>
		/// <param name="exception">The exception to display.</param>
		// Token: 0x06004CB1 RID: 19633 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public void RegisterSystemWebAssembly(int doReg, out IntPtr exception)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Allows the browser-capabilities code generator to be uninstalled.</summary>
		/// <param name="exception">The exception to display.</param>
		// Token: 0x06004CB2 RID: 19634 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public void RemoveBrowserCaps(out IntPtr exception)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
}
