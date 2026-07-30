using System;

namespace System.Windows.Forms
{
	/// <summary>The site for a <see cref="T:System.Windows.Forms.Design.ComponentEditorPage" />.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001C5 RID: 453
	public interface IComponentEditorPageSite
	{
		/// <summary>Returns the parent control for the page window.</summary>
		/// <returns>The parent control for the page window.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001DCE RID: 7630
		Control GetControl();

		/// <summary>Notifies the site that the editor is in a modified state.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001DCF RID: 7631
		void SetDirty();
	}
}
