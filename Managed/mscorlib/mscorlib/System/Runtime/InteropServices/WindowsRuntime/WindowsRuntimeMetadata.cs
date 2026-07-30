using System;
using System.Collections.Generic;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	/// <summary>Provides an event for resolving reflection-only type requests for types that are provided by Windows Metadata files, and methods for performing the resolution. </summary>
	// Token: 0x02000970 RID: 2416
	[MonoTODO]
	public static class WindowsRuntimeMetadata
	{
		/// <summary>Locates the Windows Metadata files for the specified namespace, given the specified locations to search. </summary>
		/// <returns>An enumerable list of strings that represent the Windows Metadata files that define <paramref name="namespaceName" />. </returns>
		/// <param name="namespaceName">The namespace to resolve. </param>
		/// <param name="packageGraphFilePaths">The application paths to search for Windows Metadata files, or null to search only for Windows Metadata files from the operating system installation. </param>
		/// <exception cref="T:System.PlatformNotSupportedException">The operating system version does not support the Windows Runtime. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="namespaceName" /> is null.</exception>
		// Token: 0x06005996 RID: 22934 RVA: 0x0002126B File Offset: 0x0001F46B
		public static IEnumerable<string> ResolveNamespace(string namespaceName, IEnumerable<string> packageGraphFilePaths)
		{
			throw new NotImplementedException();
		}

		/// <summary>Locates the Windows Metadata files for the specified namespace, given the specified locations to search. </summary>
		/// <returns>An enumerable list of strings that represent the Windows Metadata files that define <paramref name="namespaceName" />. </returns>
		/// <param name="namespaceName">The namespace to resolve. </param>
		/// <param name="windowsSdkFilePath">The path to search for Windows Metadata files provided by the SDK, or null to search for Windows Metadata files from the operating system installation. </param>
		/// <param name="packageGraphFilePaths">The application paths to search for Windows Metadata files. </param>
		/// <exception cref="T:System.PlatformNotSupportedException">The operating system version does not support the Windows Runtime. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="namespaceName" /> is null.</exception>
		// Token: 0x06005997 RID: 22935 RVA: 0x0002126B File Offset: 0x0001F46B
		public static IEnumerable<string> ResolveNamespace(string namespaceName, string windowsSdkFilePath, IEnumerable<string> packageGraphFilePaths)
		{
			throw new NotImplementedException();
		}

		/// <summary>Occurs when the resolution of a Windows Metadata file fails in the design environment. </summary>
		// Token: 0x1400001C RID: 28
		// (add) Token: 0x06005998 RID: 22936 RVA: 0x0012BFEC File Offset: 0x0012A1EC
		// (remove) Token: 0x06005999 RID: 22937 RVA: 0x0012C020 File Offset: 0x0012A220
		public static event EventHandler<DesignerNamespaceResolveEventArgs> DesignerNamespaceResolve;

		/// <summary>Occurs when the resolution of a Windows Metadata file fails in the reflection-only context. </summary>
		// Token: 0x1400001D RID: 29
		// (add) Token: 0x0600599A RID: 22938 RVA: 0x0012C054 File Offset: 0x0012A254
		// (remove) Token: 0x0600599B RID: 22939 RVA: 0x0012C088 File Offset: 0x0012A288
		public static event EventHandler<NamespaceResolveEventArgs> ReflectionOnlyNamespaceResolve;
	}
}
