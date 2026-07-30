using System;
using System.IO;

namespace System.Web.UI.Design
{
	/// <summary>Provides an interface for accessing a document item retrieved from a design host at design time.</summary>
	// Token: 0x0200008E RID: 142
	public interface IDocumentProjectItem
	{
		/// <summary>Provides access to the contents of a document item that is retrieved from the design host.</summary>
		/// <returns>A <see cref="T:System.IO.Stream" /> object.</returns>
		// Token: 0x0600045E RID: 1118
		Stream GetContents();

		/// <summary>Opens a document item that is retrieved from the design host.</summary>
		// Token: 0x0600045F RID: 1119
		void Open();
	}
}
