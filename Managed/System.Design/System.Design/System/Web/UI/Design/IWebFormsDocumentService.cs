using System;

namespace System.Web.UI.Design
{
	/// <summary>Provides methods to access services for tracking the loading state of a Web Forms document, handling events at load time, accessing a document's location, managing a document's undo service, and setting a new selection within the document.</summary>
	// Token: 0x02000099 RID: 153
	[Obsolete("Use new WebFormsReferenceManager feature")]
	public interface IWebFormsDocumentService
	{
		/// <summary>Occurs when the service has finished loading.</summary>
		// Token: 0x1400000E RID: 14
		// (add) Token: 0x06000497 RID: 1175
		// (remove) Token: 0x06000498 RID: 1176
		event EventHandler LoadComplete;

		/// <summary>Creates a discardable undo unit.</summary>
		/// <returns>The new discardable undo unit.</returns>
		// Token: 0x06000499 RID: 1177
		object CreateDiscardableUndoUnit();

		/// <summary>Discards the specified undo unit.</summary>
		/// <param name="discardableUndoUnit">The undo unit to discard. </param>
		// Token: 0x0600049A RID: 1178
		void DiscardUndoUnit(object discardableUndoUnit);

		/// <summary>Enables the ability to undo actions that occur within undoable action units or transactions.</summary>
		/// <param name="enable">true if actions should be undoable; otherwise, false. </param>
		// Token: 0x0600049B RID: 1179
		void EnableUndo(bool enable);

		/// <summary>When implemented in a derived class, updates the current selection.</summary>
		// Token: 0x0600049C RID: 1180
		void UpdateSelection();

		/// <summary>Gets the URL at which the document is located.</summary>
		/// <returns>The URL at which the document is located, or null if the document has no associated URL.</returns>
		// Token: 0x1700011D RID: 285
		// (get) Token: 0x0600049D RID: 1181
		string DocumentUrl { get; }

		/// <summary>Gets a value indicating whether the document service is currently loading.</summary>
		/// <returns>true if the document service is loading; otherwise, false.</returns>
		// Token: 0x1700011E RID: 286
		// (get) Token: 0x0600049E RID: 1182
		bool IsLoading { get; }
	}
}
