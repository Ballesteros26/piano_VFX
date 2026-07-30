using System;
using System.Runtime.InteropServices;

namespace System.Diagnostics.SymbolStore
{
	/// <summary>Represents a namespace within a symbol store.</summary>
	// Token: 0x02000A74 RID: 2676
	[ComVisible(true)]
	public interface ISymbolNamespace
	{
		/// <summary>Gets the current namespace.</summary>
		/// <returns>The current namespace.</returns>
		// Token: 0x17001189 RID: 4489
		// (get) Token: 0x060061D9 RID: 25049
		string Name { get; }

		/// <summary>Gets the child members of the current namespace.</summary>
		/// <returns>The child members of the current namespace.</returns>
		// Token: 0x060061DA RID: 25050
		ISymbolNamespace[] GetNamespaces();

		/// <summary>Gets all the variables defined at global scope within the current namespace.</summary>
		/// <returns>The variables defined at global scope within the current namespace.</returns>
		// Token: 0x060061DB RID: 25051
		ISymbolVariable[] GetVariables();
	}
}
