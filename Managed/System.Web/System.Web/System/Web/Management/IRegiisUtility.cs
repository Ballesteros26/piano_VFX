using System;
using System.Runtime.InteropServices;

namespace System.Web.Management
{
	/// <summary>Provides authorization utilities to support specific Web-application configuration, assembly registration, and assembly-key container manipulation.</summary>
	// Token: 0x0200052A RID: 1322
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("C84F668A-CC3F-11D7-B79E-505054503030")]
	[ComImport]
	public interface IRegiisUtility
	{
		/// <summary>Allows specific manipulation of configuration sections and assembly-key containers based on the supplied parameters.</summary>
		/// <param name="actionToPerform">The type of action to perform.</param>
		/// <param name="firstArgument">A configuration section or assembly-key container name.</param>
		/// <param name="secondArgument">The configuration file name or account name.</param>
		/// <param name="providerName">The provider name.</param>
		/// <param name="appPath">The application path.</param>
		/// <param name="site">The site reference.</param>
		/// <param name="cspOrLocation">The configuration location.</param>
		/// <param name="keySize">The size of the key.</param>
		/// <param name="exception">The exception to display.</param>
		// Token: 0x06003A2D RID: 14893
		void ProtectedConfigAction(long actionToPerform, [MarshalAs(UnmanagedType.LPWStr)] [In] string firstArgument, [MarshalAs(UnmanagedType.LPWStr)] [In] string secondArgument, [MarshalAs(UnmanagedType.LPWStr)] [In] string providerName, [MarshalAs(UnmanagedType.LPWStr)] [In] string appPath, [MarshalAs(UnmanagedType.LPWStr)] [In] string site, [MarshalAs(UnmanagedType.LPWStr)] [In] string cspOrLocation, int keySize, out IntPtr exception);

		/// <summary>Allows the executing Microsoft Management Console (MMC) assembly to be registered or unregistered.</summary>
		/// <param name="doReg">A value of 0 indicates that the assembly should be unregistered. A value other than 0 indicates that the assembly should be registered.</param>
		/// <param name="assemblyName">The type of the assembly.</param>
		/// <param name="binaryDirectory">The path of the binary directory.</param>
		/// <param name="exception">The exception to display.</param>
		// Token: 0x06003A2E RID: 14894
		void RegisterAsnetMmcAssembly(int doReg, [MarshalAs(UnmanagedType.LPWStr)] [In] string assemblyName, [MarshalAs(UnmanagedType.LPWStr)] [In] string binaryDirectory, out IntPtr exception);

		/// <summary>Allows the executing Web assembly to be registered or unregistered.</summary>
		/// <param name="doReg">A value of 0 indicates that the assembly should be unregistered. A value other than 0 indicates that the assembly should be registered.</param>
		/// <param name="exception">An <see cref="T:System.IntPtr" /> that points to the exception thrown by the method.  If no exception is thrown, the value is <see cref="F:System.IntPtr.Zero" />.</param>
		// Token: 0x06003A2F RID: 14895
		void RegisterSystemWebAssembly(int doReg, out IntPtr exception);

		/// <summary>Allows the browser-capabilities code generator to be uninstalled.</summary>
		/// <param name="exception">An <see cref="T:System.IntPtr" /> that points to the exception thrown by the method.  If no exception is thrown, the value is <see cref="F:System.IntPtr.Zero" />.</param>
		/// <exception cref="T:System.Exception">The attempt to uninstall the browser-capabilities code generator fails.</exception>
		// Token: 0x06003A30 RID: 14896
		void RemoveBrowserCaps(out IntPtr exception);
	}
}
