using System;
using System.Collections.ObjectModel;
using System.Reflection;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	/// <summary>Provides data for the <see cref="E:System.Runtime.InteropServices.WindowsRuntime.WindowsRuntimeMetadata.ReflectionOnlyNamespaceResolve" /> event.</summary>
	// Token: 0x0200096E RID: 2414
	[ComVisible(false)]
	public class NamespaceResolveEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.WindowsRuntime.NamespaceResolveEventArgs" /> class, specifying the namespace to resolve and the assembly whose dependency is being resolved. </summary>
		/// <param name="namespaceName">The namespace to resolve. </param>
		/// <param name="requestingAssembly">The assembly whose dependency is being resolved. </param>
		// Token: 0x06005989 RID: 22921 RVA: 0x0012BF98 File Offset: 0x0012A198
		public NamespaceResolveEventArgs(string namespaceName, Assembly requestingAssembly)
		{
			this.NamespaceName = namespaceName;
			this.RequestingAssembly = requestingAssembly;
			this.ResolvedAssemblies = new Collection<Assembly>();
		}

		/// <summary>Gets the name of the namespace to resolve. </summary>
		/// <returns>The name of the namespace to resolve. </returns>
		// Token: 0x17000FB8 RID: 4024
		// (get) Token: 0x0600598A RID: 22922 RVA: 0x0012BFB9 File Offset: 0x0012A1B9
		// (set) Token: 0x0600598B RID: 22923 RVA: 0x0012BFC1 File Offset: 0x0012A1C1
		public string NamespaceName { get; private set; }

		/// <summary>Gets the name of the assembly whose dependency is being resolved. </summary>
		/// <returns>The name of the assembly whose dependency is being resolved. </returns>
		// Token: 0x17000FB9 RID: 4025
		// (get) Token: 0x0600598C RID: 22924 RVA: 0x0012BFCA File Offset: 0x0012A1CA
		// (set) Token: 0x0600598D RID: 22925 RVA: 0x0012BFD2 File Offset: 0x0012A1D2
		public Assembly RequestingAssembly { get; private set; }

		/// <summary>Gets a collection of assemblies; when the event handler for the <see cref="E:System.Runtime.InteropServices.WindowsRuntime.WindowsRuntimeMetadata.ReflectionOnlyNamespaceResolve" /> event is invoked, the collection is empty, and the event handler is responsible for adding the necessary assemblies. </summary>
		/// <returns>A collection of assemblies that define the requested namespace. </returns>
		// Token: 0x17000FBA RID: 4026
		// (get) Token: 0x0600598E RID: 22926 RVA: 0x0012BFDB File Offset: 0x0012A1DB
		// (set) Token: 0x0600598F RID: 22927 RVA: 0x0012BFE3 File Offset: 0x0012A1E3
		public Collection<Assembly> ResolvedAssemblies { get; private set; }
	}
}
