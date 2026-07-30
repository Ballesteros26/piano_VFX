using System;
using System.Runtime.InteropServices;

namespace System.Diagnostics.SymbolStore
{
	/// <summary>Represents a symbol reader for managed code.</summary>
	// Token: 0x02000A75 RID: 2677
	[ComVisible(true)]
	public interface ISymbolReader
	{
		/// <summary>Gets the metadata token for the method that was specified as the user entry point for the module, if any.</summary>
		/// <returns>The metadata token for the method that is the user entry point for the module.</returns>
		// Token: 0x1700118A RID: 4490
		// (get) Token: 0x060061DC RID: 25052
		SymbolToken UserEntryPoint { get; }

		/// <summary>Gets a document specified by the language, vendor, and type.</summary>
		/// <returns>The specified document.</returns>
		/// <param name="url">The URL that identifies the document. </param>
		/// <param name="language">The document language. You can specify this parameter as <see cref="F:System.Guid.Empty" />. </param>
		/// <param name="languageVendor">The identity of the vendor for the document language. You can specify this parameter as <see cref="F:System.Guid.Empty" />.</param>
		/// <param name="documentType">The type of the document. You can specify this parameter as <see cref="F:System.Guid.Empty" />.</param>
		// Token: 0x060061DD RID: 25053
		ISymbolDocument GetDocument(string url, Guid language, Guid languageVendor, Guid documentType);

		/// <summary>Gets an array of all documents defined in the symbol store.</summary>
		/// <returns>An array of all documents defined in the symbol store.</returns>
		// Token: 0x060061DE RID: 25054
		ISymbolDocument[] GetDocuments();

		/// <summary>Gets all global variables in the module.</summary>
		/// <returns>An array of all variables in the module.</returns>
		// Token: 0x060061DF RID: 25055
		ISymbolVariable[] GetGlobalVariables();

		/// <summary>Gets a symbol reader method object when given the identifier of a method.</summary>
		/// <returns>The symbol reader method object for the specified method identifier.</returns>
		/// <param name="method">The metadata token of the method. </param>
		// Token: 0x060061E0 RID: 25056
		ISymbolMethod GetMethod(SymbolToken method);

		/// <summary>Gets a symbol reader method object when given the identifier of a method and its edit and continue version.</summary>
		/// <returns>The symbol reader method object for the specified method identifier.</returns>
		/// <param name="method">The metadata token of the method. </param>
		/// <param name="version">The edit and continue version of the method. </param>
		// Token: 0x060061E1 RID: 25057
		ISymbolMethod GetMethod(SymbolToken method, int version);

		/// <summary>Gets a symbol reader method object that contains a specified position in a document.</summary>
		/// <returns>The reader method object for the specified position in the document.</returns>
		/// <param name="document">The document in which the method is located. </param>
		/// <param name="line">The position of the line within the document. The lines are numbered, beginning with 1. </param>
		/// <param name="column">The position of column within the document. The columns are numbered, beginning with 1. </param>
		// Token: 0x060061E2 RID: 25058
		ISymbolMethod GetMethodFromDocumentPosition(ISymbolDocument document, int line, int column);

		/// <summary>Gets the namespaces that are defined in the global scope within the current symbol store.</summary>
		/// <returns>The namespaces defined in the global scope within the current symbol store.</returns>
		// Token: 0x060061E3 RID: 25059
		ISymbolNamespace[] GetNamespaces();

		/// <summary>Gets an attribute value when given the attribute name.</summary>
		/// <returns>The value of the attribute.</returns>
		/// <param name="parent">The metadata token for the object for which the attribute is requested. </param>
		/// <param name="name">The attribute name. </param>
		// Token: 0x060061E4 RID: 25060
		byte[] GetSymAttribute(SymbolToken parent, string name);

		/// <summary>Gets the variables that are not local when given the parent.</summary>
		/// <returns>An array of variables for the parent.</returns>
		/// <param name="parent">The metadata token for the type for which the variables are requested. </param>
		// Token: 0x060061E5 RID: 25061
		ISymbolVariable[] GetVariables(SymbolToken parent);
	}
}
