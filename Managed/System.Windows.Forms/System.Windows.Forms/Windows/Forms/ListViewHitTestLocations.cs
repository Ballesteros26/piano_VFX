using System;

namespace System.Windows.Forms
{
	/// <summary>Defines constants that represent areas in a <see cref="T:System.Windows.Forms.ListView" /> or <see cref="T:System.Windows.Forms.ListViewItem" />. </summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200022E RID: 558
	[Flags]
	public enum ListViewHitTestLocations
	{
		/// <summary>A position outside the bounds of a <see cref="T:System.Windows.Forms.ListViewItem" /></summary>
		// Token: 0x0400129F RID: 4767
		None = 1,
		/// <summary>A position within the bounds of an image contained in a <see cref="T:System.Windows.Forms.ListView" /> or <see cref="T:System.Windows.Forms.ListViewItem" />.</summary>
		// Token: 0x040012A0 RID: 4768
		Image = 2,
		/// <summary>A position within the bounds of a text area contained in a <see cref="T:System.Windows.Forms.ListView" /> or <see cref="T:System.Windows.Forms.ListViewItem" />.</summary>
		// Token: 0x040012A1 RID: 4769
		Label = 4,
		/// <summary>A position below the client portion of a <see cref="T:System.Windows.Forms.ListView" /> control.</summary>
		// Token: 0x040012A2 RID: 4770
		BelowClientArea = 16,
		/// <summary>A position to the right of the client portion of a <see cref="T:System.Windows.Forms.ListView" /> control.</summary>
		// Token: 0x040012A3 RID: 4771
		RightOfClientArea = 32,
		/// <summary>A position to the left of the client portion of a <see cref="T:System.Windows.Forms.ListView" /> control.</summary>
		// Token: 0x040012A4 RID: 4772
		LeftOfClientArea = 64,
		/// <summary>A position above the client portion of a <see cref="T:System.Windows.Forms.ListView" /> control.</summary>
		// Token: 0x040012A5 RID: 4773
		AboveClientArea = 256,
		/// <summary>A position within the bounds of an image associated with a <see cref="T:System.Windows.Forms.ListViewItem" /> that indicates the state of the item.</summary>
		// Token: 0x040012A6 RID: 4774
		StateImage = 512
	}
}
