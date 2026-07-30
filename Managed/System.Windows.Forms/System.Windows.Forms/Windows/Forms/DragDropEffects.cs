using System;

namespace System.Windows.Forms
{
	/// <summary>Specifies the possible effects of a drag-and-drop operation.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000153 RID: 339
	[Flags]
	public enum DragDropEffects
	{
		/// <summary>The drop target does not accept the data.</summary>
		// Token: 0x04000CBB RID: 3259
		None = 0,
		/// <summary>The data from the drag source is copied to the drop target.</summary>
		// Token: 0x04000CBC RID: 3260
		Copy = 1,
		/// <summary>The data from the drag source is moved to the drop target.</summary>
		// Token: 0x04000CBD RID: 3261
		Move = 2,
		/// <summary>The data from the drag source is linked to the drop target.</summary>
		// Token: 0x04000CBE RID: 3262
		Link = 4,
		/// <summary>The target can be scrolled while dragging to locate a drop position that is not currently visible in the target.</summary>
		// Token: 0x04000CBF RID: 3263
		Scroll = -2147483648,
		/// <summary>The combination of the <see cref="F:System.Windows.DragDropEffects.Copy" />, <see cref="F:System.Windows.Forms.DragDropEffects.Move" />, and <see cref="F:System.Windows.Forms.DragDropEffects.Scroll" /> effects.</summary>
		// Token: 0x04000CC0 RID: 3264
		All = -2147483645
	}
}
