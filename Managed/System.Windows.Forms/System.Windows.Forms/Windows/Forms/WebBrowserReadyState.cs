using System;

namespace System.Windows.Forms
{
	/// <summary>Specifies constants that define the state of the <see cref="T:System.Windows.Forms.WebBrowser" /> control.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020003B2 RID: 946
	public enum WebBrowserReadyState
	{
		/// <summary>No document is currently loaded.</summary>
		// Token: 0x04001CC5 RID: 7365
		Uninitialized,
		/// <summary>The control is loading a new document.</summary>
		// Token: 0x04001CC6 RID: 7366
		Loading,
		/// <summary>The control has loaded and initialized the new document, but has not yet received all the document data.</summary>
		// Token: 0x04001CC7 RID: 7367
		Loaded,
		/// <summary>The control has loaded enough of the document to allow limited user interaction, such as clicking hyperlinks that have been displayed.</summary>
		// Token: 0x04001CC8 RID: 7368
		Interactive,
		/// <summary>The control has finished loading the new document and all its contents.</summary>
		// Token: 0x04001CC9 RID: 7369
		Complete
	}
}
