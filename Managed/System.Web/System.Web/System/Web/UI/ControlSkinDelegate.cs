using System;
using System.ComponentModel;

namespace System.Web.UI
{
	/// <summary>Represents the method that applies the correct control skin to the specified control.</summary>
	/// <returns>The <see cref="T:System.Web.UI.Control" /> that was passed to the method, with a control skin applied.</returns>
	/// <param name="control">The <see cref="T:System.Web.UI.Control" /> to which to apply the theme skin.</param>
	// Token: 0x020001BB RID: 443
	// (Invoke) Token: 0x060011FD RID: 4605
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public delegate Control ControlSkinDelegate(Control control);
}
