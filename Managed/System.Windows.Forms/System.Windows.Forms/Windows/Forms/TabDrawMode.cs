using System;

namespace System.Windows.Forms
{
	/// <summary>Specifies whether the tabs in a tab control are owner-drawn (drawn by the parent window), or drawn by the operating system.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020002FC RID: 764
	public enum TabDrawMode
	{
		/// <summary>The tabs are drawn by the operating system, and are of the same size.</summary>
		// Token: 0x04001852 RID: 6226
		Normal,
		/// <summary>The tabs are drawn by the parent window, and are of the same size.</summary>
		// Token: 0x04001853 RID: 6227
		OwnerDrawFixed
	}
}
