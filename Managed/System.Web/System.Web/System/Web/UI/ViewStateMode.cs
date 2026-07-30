using System;

namespace System.Web.UI
{
	/// <summary>Specifies whether view state will be enabled for a control.</summary>
	// Token: 0x0200019A RID: 410
	public enum ViewStateMode
	{
		/// <summary>Inherit the value of <see cref="T:System.Web.UI.ViewStateMode" /> from the parent <see cref="T:System.Web.UI.Control" />.</summary>
		// Token: 0x04001344 RID: 4932
		Inherit,
		/// <summary>Enable view state for this control even if the parent control has view state disabled.</summary>
		// Token: 0x04001345 RID: 4933
		Enabled,
		/// <summary>Disable view state for this control even if the parent control has view state enabled.</summary>
		// Token: 0x04001346 RID: 4934
		Disabled
	}
}
