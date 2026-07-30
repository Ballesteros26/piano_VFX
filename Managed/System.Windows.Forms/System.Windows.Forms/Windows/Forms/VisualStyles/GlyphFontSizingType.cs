using System;

namespace System.Windows.Forms.VisualStyles
{
	/// <summary>Specifies when the visual style selects a different glyph font size.</summary>
	// Token: 0x020004DF RID: 1247
	public enum GlyphFontSizingType
	{
		/// <summary>Glyph font sizes do not change.</summary>
		// Token: 0x04002A7A RID: 10874
		None,
		/// <summary>Glyph font size changes are based on font size settings.</summary>
		// Token: 0x04002A7B RID: 10875
		Size,
		/// <summary>Glyph font size changes are based on dots per inch (DPI) settings.</summary>
		// Token: 0x04002A7C RID: 10876
		Dpi
	}
}
