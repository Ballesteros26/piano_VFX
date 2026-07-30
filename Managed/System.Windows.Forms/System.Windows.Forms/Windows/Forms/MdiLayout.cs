using System;

namespace System.Windows.Forms
{
	/// <summary>Specifies the layout of multiple document interface (MDI) child windows in an MDI parent window.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000246 RID: 582
	public enum MdiLayout
	{
		/// <summary>All MDI child windows are cascaded within the client region of the MDI parent form.</summary>
		// Token: 0x0400131B RID: 4891
		Cascade,
		/// <summary>All MDI child windows are tiled horizontally within the client region of the MDI parent form.</summary>
		// Token: 0x0400131C RID: 4892
		TileHorizontal,
		/// <summary>All MDI child windows are tiled vertically within the client region of the MDI parent form.</summary>
		// Token: 0x0400131D RID: 4893
		TileVertical,
		/// <summary>All MDI child icons are arranged within the client region of the MDI parent form.</summary>
		// Token: 0x0400131E RID: 4894
		ArrangeIcons
	}
}
