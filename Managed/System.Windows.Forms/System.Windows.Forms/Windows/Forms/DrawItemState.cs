using System;

namespace System.Windows.Forms
{
	/// <summary>Specifies the state of an item that is being drawn.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000156 RID: 342
	[Flags]
	public enum DrawItemState
	{
		/// <summary>The item currently has no state.</summary>
		// Token: 0x04000CCF RID: 3279
		None = 0,
		/// <summary>The item is selected.</summary>
		// Token: 0x04000CD0 RID: 3280
		Selected = 1,
		/// <summary>The item is grayed. Only menu controls use this value.</summary>
		// Token: 0x04000CD1 RID: 3281
		Grayed = 2,
		/// <summary>The item is unavailable.</summary>
		// Token: 0x04000CD2 RID: 3282
		Disabled = 4,
		/// <summary>The item is checked. Only menu controls use this value.</summary>
		// Token: 0x04000CD3 RID: 3283
		Checked = 8,
		/// <summary>The item has focus.</summary>
		// Token: 0x04000CD4 RID: 3284
		Focus = 16,
		/// <summary>The item is in its default visual state.</summary>
		// Token: 0x04000CD5 RID: 3285
		Default = 32,
		/// <summary>The item is being hot-tracked, that is, the item is highlighted as the mouse pointer passes over it.</summary>
		// Token: 0x04000CD6 RID: 3286
		HotLight = 64,
		/// <summary>The item is inactive.</summary>
		// Token: 0x04000CD7 RID: 3287
		Inactive = 128,
		/// <summary>The item displays without a keyboard accelerator.</summary>
		// Token: 0x04000CD8 RID: 3288
		NoAccelerator = 256,
		/// <summary>The item displays without the visual cue that indicates it has focus.</summary>
		// Token: 0x04000CD9 RID: 3289
		NoFocusRect = 512,
		/// <summary>The item is the editing portion of a <see cref="T:System.Windows.Forms.ComboBox" />.</summary>
		// Token: 0x04000CDA RID: 3290
		ComboBoxEdit = 4096
	}
}
