using System;

namespace System.Windows.Forms.VisualStyles
{
	/// <summary>Specifies when the visual style selects a different multiple-image file to draw an element.</summary>
	// Token: 0x0200051D RID: 1309
	public enum ImageSelectType
	{
		/// <summary>The image file does not change.</summary>
		// Token: 0x04002B70 RID: 11120
		None,
		/// <summary>Image file changes are based on size settings.</summary>
		// Token: 0x04002B71 RID: 11121
		Size,
		/// <summary>Image file changes are based on dots per inch (DPI) settings.</summary>
		// Token: 0x04002B72 RID: 11122
		Dpi
	}
}
