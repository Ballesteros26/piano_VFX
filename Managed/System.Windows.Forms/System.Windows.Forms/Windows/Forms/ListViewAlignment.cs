using System;

namespace System.Windows.Forms
{
	/// <summary>Specifies how items align in the <see cref="T:System.Windows.Forms.ListView" />.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000229 RID: 553
	public enum ListViewAlignment
	{
		/// <summary>When the user moves an item, it remains where it is dropped.</summary>
		// Token: 0x04001286 RID: 4742
		Default,
		/// <summary>Items are aligned to the left of the <see cref="T:System.Windows.Forms.ListView" /> control.</summary>
		// Token: 0x04001287 RID: 4743
		Left,
		/// <summary>Items are aligned to the top of the <see cref="T:System.Windows.Forms.ListView" /> control.</summary>
		// Token: 0x04001288 RID: 4744
		Top,
		/// <summary>Items are aligned to an invisible grid in the control. When the user moves an item, it moves to the closest juncture in the grid.</summary>
		// Token: 0x04001289 RID: 4745
		SnapToGrid = 5
	}
}
