using System;

namespace System.Windows.Forms.Design
{
	/// <summary>A value that indicates whether the scrollbars of a window and its children will be themed when displayed in the Visual Studio designer.</summary>
	// Token: 0x02000178 RID: 376
	public enum ThemedScrollbarMode
	{
		/// <summary>The window and all of its children will have themed scrollbars.</summary>
		// Token: 0x0400029C RID: 668
		All = 1,
		/// <summary>The window and all of its children will not be themed.</summary>
		// Token: 0x0400029D RID: 669
		None,
		/// <summary>The window will have themed scrollbars but all of its children will not be themed.</summary>
		// Token: 0x0400029E RID: 670
		OnlyTopLevel
	}
}
