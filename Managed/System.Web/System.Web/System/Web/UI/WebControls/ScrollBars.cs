using System;

namespace System.Web.UI.WebControls
{
	/// <summary>Specifies the visibility and position of scroll bars in a <see cref="T:System.Web.UI.WebControls.Panel" /> control.</summary>
	// Token: 0x02000304 RID: 772
	[Flags]
	public enum ScrollBars
	{
		/// <summary>Displays no scroll bars.</summary>
		// Token: 0x04001754 RID: 5972
		None = 0,
		/// <summary>Displays only a horizontal scroll bar.</summary>
		// Token: 0x04001755 RID: 5973
		Horizontal = 1,
		/// <summary>Displays only a vertical scroll bar.</summary>
		// Token: 0x04001756 RID: 5974
		Vertical = 2,
		/// <summary>Displays both a horizontal and a vertical scroll bar.</summary>
		// Token: 0x04001757 RID: 5975
		Both = 3,
		/// <summary>Displays, horizontal, vertical, or both scroll bars as necessary. Otherwise, no scroll bars are shown.</summary>
		// Token: 0x04001758 RID: 5976
		Auto = 4
	}
}
