using System;

namespace System.Web.UI.WebControls
{
	/// <summary>Specifies the type of an item in a list control.</summary>
	// Token: 0x020002DD RID: 733
	public enum ListItemType
	{
		/// <summary>A header for the list control. It is not data-bound.</summary>
		// Token: 0x04001704 RID: 5892
		Header,
		/// <summary>A footer for the list control. It is not data-bound.</summary>
		// Token: 0x04001705 RID: 5893
		Footer,
		/// <summary>An item in the list control. It is data-bound.</summary>
		// Token: 0x04001706 RID: 5894
		Item,
		/// <summary>An item in alternating (zero-based even-indexed) cells. It is data-bound.</summary>
		// Token: 0x04001707 RID: 5895
		AlternatingItem,
		/// <summary>A selected item in the list control. It is data-bound.</summary>
		// Token: 0x04001708 RID: 5896
		SelectedItem,
		/// <summary>An item in a list control currently in edit mode. It is data-bound.</summary>
		// Token: 0x04001709 RID: 5897
		EditItem,
		/// <summary>A separator between items in a list control. It is not data-bound.</summary>
		// Token: 0x0400170A RID: 5898
		Separator,
		/// <summary>A pager that displays the controls to navigate to different pages associated with the <see cref="T:System.Web.UI.WebControls.DataGrid" /> control. It is not data-bound.</summary>
		// Token: 0x0400170B RID: 5899
		Pager
	}
}
