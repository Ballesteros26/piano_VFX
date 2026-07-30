using System;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	/// <summary>Specifies the user interface (UI) state of a element within a <see cref="T:System.Windows.Forms.DataGridView" /> control.</summary>
	// Token: 0x02000113 RID: 275
	[ComVisible(true)]
	[Flags]
	public enum DataGridViewElementStates
	{
		/// <summary>Indicates that an element is in its default state.</summary>
		// Token: 0x04000BAF RID: 2991
		None = 0,
		/// <summary>Indicates the an element is currently displayed onscreen.</summary>
		// Token: 0x04000BB0 RID: 2992
		Displayed = 1,
		/// <summary>Indicates that an element cannot be scrolled through the UI.</summary>
		// Token: 0x04000BB1 RID: 2993
		Frozen = 2,
		/// <summary>Indicates that an element will not accept user input to change its value.</summary>
		// Token: 0x04000BB2 RID: 2994
		ReadOnly = 4,
		/// <summary>Indicates that an element can be resized through the UI. This value is ignored except when combined with the <see cref="F:System.Windows.Forms.DataGridViewElementStates.ResizableSet" /> value.</summary>
		// Token: 0x04000BB3 RID: 2995
		Resizable = 8,
		/// <summary>Indicates that an element does not inherit the resizable state of its parent.</summary>
		// Token: 0x04000BB4 RID: 2996
		ResizableSet = 16,
		/// <summary>Indicates that an element is in a selected (highlighted) UI state.</summary>
		// Token: 0x04000BB5 RID: 2997
		Selected = 32,
		/// <summary>Indicates that an element is visible (displayable).</summary>
		// Token: 0x04000BB6 RID: 2998
		Visible = 64
	}
}
