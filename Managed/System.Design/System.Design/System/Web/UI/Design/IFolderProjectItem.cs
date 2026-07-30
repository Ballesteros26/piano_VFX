using System;
using System.Collections;

namespace System.Web.UI.Design
{
	/// <summary>Provides an interface for a project folder in a design host, such as Visual Studio 2005.</summary>
	// Token: 0x0200008F RID: 143
	public interface IFolderProjectItem
	{
		/// <summary>Gets a collection of items in a project folder in a design host, such as Visual Studio 2005.</summary>
		/// <returns>An <see cref="T:System.Collections.ICollection" /> containing the items in the project folder of the design host.</returns>
		// Token: 0x1700010B RID: 267
		// (get) Token: 0x06000460 RID: 1120
		ICollection Children { get; }

		/// <summary>Adds a document to a project folder in a design host, such as Visual Studio 2005.</summary>
		/// <returns>An <see cref="T:System.Web.UI.Design.IDocumentProjectItem" /> representing the added document.</returns>
		/// <param name="name">The name of the document.</param>
		/// <param name="content">An array of type <see cref="T:System.Byte" /> containing the document contents.</param>
		// Token: 0x06000461 RID: 1121
		IDocumentProjectItem AddDocument(string name, byte[] content);

		/// <summary>Creates a new folder in a project folder of a design host, such as Visual Studio 2005.</summary>
		/// <returns>An <see cref="T:System.Web.UI.Design.IFolderProjectItem" /> representing the new folder.</returns>
		/// <param name="name">The name for the new folder.</param>
		// Token: 0x06000462 RID: 1122
		IFolderProjectItem AddFolder(string name);
	}
}
