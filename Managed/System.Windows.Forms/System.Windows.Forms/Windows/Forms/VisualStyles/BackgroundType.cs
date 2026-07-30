using System;

namespace System.Windows.Forms.VisualStyles
{
	/// <summary>Specifies the source of a visual style element's background.</summary>
	// Token: 0x020004D1 RID: 1233
	public enum BackgroundType
	{
		/// <summary>The background of the element is a bitmap. If this value is set, then the property corresponding to the <see cref="F:System.Windows.Forms.VisualStyles.FilenameProperty.ImageFile" /> value will contain the name of a valid image file.</summary>
		// Token: 0x04002A04 RID: 10756
		ImageFile,
		/// <summary>The background of the element is a rectangle filled with a color or pattern. </summary>
		// Token: 0x04002A05 RID: 10757
		BorderFill,
		/// <summary>The element has no background.</summary>
		// Token: 0x04002A06 RID: 10758
		None
	}
}
