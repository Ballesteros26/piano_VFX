using System;
using System.Collections.ObjectModel;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	/// <summary>Provides data for the <see cref="E:System.Runtime.InteropServices.WindowsRuntime.WindowsRuntimeMetadata.DesignerNamespaceResolve" /> event. </summary>
	// Token: 0x0200096D RID: 2413
	[ComVisible(false)]
	public class DesignerNamespaceResolveEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.WindowsRuntime.DesignerNamespaceResolveEventArgs" /> class. </summary>
		/// <param name="namespaceName">The name of the namespace to resolve. </param>
		// Token: 0x06005984 RID: 22916 RVA: 0x0012BF5C File Offset: 0x0012A15C
		public DesignerNamespaceResolveEventArgs(string namespaceName)
		{
			this.NamespaceName = namespaceName;
			this.ResolvedAssemblyFiles = new Collection<string>();
		}

		/// <summary>Gets the name of the namespace to resolve. </summary>
		/// <returns>The name of the namespace to resolve. </returns>
		// Token: 0x17000FB6 RID: 4022
		// (get) Token: 0x06005985 RID: 22917 RVA: 0x0012BF76 File Offset: 0x0012A176
		// (set) Token: 0x06005986 RID: 22918 RVA: 0x0012BF7E File Offset: 0x0012A17E
		public string NamespaceName { get; private set; }

		/// <summary>Gets a collection of assembly file paths; when the event handler for the <see cref="E:System.Runtime.InteropServices.WindowsRuntime.WindowsRuntimeMetadata.DesignerNamespaceResolve" /> event is invoked, the collection is empty, and the event handler is responsible for adding the necessary assembly files. </summary>
		/// <returns>A collection of assembly files that define the requested namespace. </returns>
		// Token: 0x17000FB7 RID: 4023
		// (get) Token: 0x06005987 RID: 22919 RVA: 0x0012BF87 File Offset: 0x0012A187
		// (set) Token: 0x06005988 RID: 22920 RVA: 0x0012BF8F File Offset: 0x0012A18F
		public Collection<string> ResolvedAssemblyFiles { get; private set; }
	}
}
