using System;
using System.Runtime.InteropServices;

namespace System.Diagnostics.SymbolStore
{
	/// <summary>Represents a lexical scope within <see cref="T:System.Diagnostics.SymbolStore.ISymbolMethod" />, providing access to the start and end offsets of the scope, as well as its child and parent scopes.</summary>
	// Token: 0x02000A76 RID: 2678
	[ComVisible(true)]
	public interface ISymbolScope
	{
		/// <summary>Gets the end offset of the current lexical scope.</summary>
		/// <returns>The end offset of the current lexical scope.</returns>
		// Token: 0x1700118B RID: 4491
		// (get) Token: 0x060061E6 RID: 25062
		int EndOffset { get; }

		/// <summary>Gets the method that contains the current lexical scope.</summary>
		/// <returns>The method that contains the current lexical scope.</returns>
		// Token: 0x1700118C RID: 4492
		// (get) Token: 0x060061E7 RID: 25063
		ISymbolMethod Method { get; }

		/// <summary>Gets the parent lexical scope of the current scope.</summary>
		/// <returns>The parent lexical scope of the current scope.</returns>
		// Token: 0x1700118D RID: 4493
		// (get) Token: 0x060061E8 RID: 25064
		ISymbolScope Parent { get; }

		/// <summary>Gets the start offset of the current lexical scope.</summary>
		/// <returns>The start offset of the current lexical scope.</returns>
		// Token: 0x1700118E RID: 4494
		// (get) Token: 0x060061E9 RID: 25065
		int StartOffset { get; }

		/// <summary>Gets the child lexical scopes of the current lexical scope.</summary>
		/// <returns>The child lexical scopes that of the current lexical scope.</returns>
		// Token: 0x060061EA RID: 25066
		ISymbolScope[] GetChildren();

		/// <summary>Gets the local variables within the current lexical scope.</summary>
		/// <returns>The local variables within the current lexical scope.</returns>
		// Token: 0x060061EB RID: 25067
		ISymbolVariable[] GetLocals();

		/// <summary>Gets the namespaces that are used within the current scope.</summary>
		/// <returns>The namespaces that are used within the current scope.</returns>
		// Token: 0x060061EC RID: 25068
		ISymbolNamespace[] GetNamespaces();
	}
}
