using System;

namespace System.Windows.Forms.Design.Behavior
{
	/// <summary>Describes the designer selection state of a <see cref="T:System.Windows.Forms.Design.Behavior.Glyph" />.</summary>
	// Token: 0x0200004C RID: 76
	public enum GlyphSelectionType
	{
		/// <summary>The <see cref="T:System.Windows.Forms.Design.Behavior.Glyph" /> is not selected.</summary>
		// Token: 0x04000102 RID: 258
		NotSelected,
		/// <summary>The <see cref="T:System.Windows.Forms.Design.Behavior.Glyph" /> is selected.</summary>
		// Token: 0x04000103 RID: 259
		Selected,
		/// <summary>The <see cref="T:System.Windows.Forms.Design.Behavior.Glyph" /> is the primary selection. </summary>
		// Token: 0x04000104 RID: 260
		SelectedPrimary
	}
}
