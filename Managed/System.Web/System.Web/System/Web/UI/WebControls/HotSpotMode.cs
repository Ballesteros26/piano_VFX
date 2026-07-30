using System;

namespace System.Web.UI.WebControls
{
	/// <summary>Specifies the behaviors of a <see cref="T:System.Web.UI.WebControls.HotSpot" /> object in an <see cref="T:System.Web.UI.WebControls.ImageMap" /> control when the <see cref="T:System.Web.UI.WebControls.HotSpot" /> is clicked.</summary>
	// Token: 0x020002CE RID: 718
	public enum HotSpotMode
	{
		/// <summary>The <see cref="T:System.Web.UI.WebControls.HotSpot" /> uses the behavior set by the <see cref="T:System.Web.UI.WebControls.ImageMap" /> control's <see cref="P:System.Web.UI.WebControls.ImageMap.HotSpotMode" /> property. If the <see cref="T:System.Web.UI.WebControls.ImageMap" /> control does not define the behavior, the <see cref="T:System.Web.UI.WebControls.HotSpot" /> object navigates to a URL.</summary>
		// Token: 0x040016F3 RID: 5875
		NotSet,
		/// <summary>The <see cref="T:System.Web.UI.WebControls.HotSpot" /> navigates to a URL.</summary>
		// Token: 0x040016F4 RID: 5876
		Navigate,
		/// <summary>The <see cref="T:System.Web.UI.WebControls.HotSpot" /> generates a postback to the server.</summary>
		// Token: 0x040016F5 RID: 5877
		PostBack,
		/// <summary>The <see cref="T:System.Web.UI.WebControls.HotSpot" /> does not have any behavior.</summary>
		// Token: 0x040016F6 RID: 5878
		Inactive
	}
}
