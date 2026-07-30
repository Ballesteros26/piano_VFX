using System;

namespace System.Windows.Forms
{
	/// <summary>Specifies how an accessible object is selected or receives focus.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200003A RID: 58
	[Flags]
	public enum AccessibleSelection
	{
		/// <summary>The selection or focus of an object is unchanged.</summary>
		// Token: 0x04000576 RID: 1398
		None = 0,
		/// <summary>Assigns focus to an object and makes it the anchor, which is the starting point for the selection. Can be combined with TakeSelection, ExtendSelection, AddSelection, or RemoveSelection.</summary>
		// Token: 0x04000577 RID: 1399
		TakeFocus = 1,
		/// <summary>Selects the object and deselects all other objects in the container.</summary>
		// Token: 0x04000578 RID: 1400
		TakeSelection = 2,
		/// <summary>Selects all objects between the anchor and the selected object.</summary>
		// Token: 0x04000579 RID: 1401
		ExtendSelection = 4,
		/// <summary>Adds the object to the selection.</summary>
		// Token: 0x0400057A RID: 1402
		AddSelection = 8,
		/// <summary>Removes the object from the selection.</summary>
		// Token: 0x0400057B RID: 1403
		RemoveSelection = 16
	}
}
