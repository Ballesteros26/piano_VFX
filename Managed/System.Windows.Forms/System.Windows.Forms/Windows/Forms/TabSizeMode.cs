using System;

namespace System.Windows.Forms
{
	/// <summary>Specifies how tabs in a tab control are sized.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000300 RID: 768
	public enum TabSizeMode
	{
		/// <summary>The width of each tab is sized to accommodate what is displayed on the tab, and the size of tabs in a row are not adjusted to fill the entire width of the container control.</summary>
		// Token: 0x0400185B RID: 6235
		Normal,
		/// <summary>The width of each tab is sized so that each row of tabs fills the entire width of the container control. This is only applicable to tab controls with more than one row.</summary>
		// Token: 0x0400185C RID: 6236
		FillToRight,
		/// <summary>All tabs in a control are the same width.</summary>
		// Token: 0x0400185D RID: 6237
		Fixed
	}
}
