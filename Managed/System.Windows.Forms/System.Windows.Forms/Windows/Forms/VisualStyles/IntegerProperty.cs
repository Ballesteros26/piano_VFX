using System;

namespace System.Windows.Forms.VisualStyles
{
	/// <summary>Identifies the integer properties of a visual style element.</summary>
	// Token: 0x0200051E RID: 1310
	public enum IntegerProperty
	{
		/// <summary>The number of state images in multiple-image file.</summary>
		// Token: 0x04002B74 RID: 11124
		ImageCount = 2401,
		/// <summary>The alpha value for an icon, between 0 and 255.</summary>
		// Token: 0x04002B75 RID: 11125
		AlphaLevel,
		/// <summary>The size of the border line for elements with a filled-border background.</summary>
		// Token: 0x04002B76 RID: 11126
		BorderSize,
		/// <summary>A percentage value that represents the width of a rounded corner, from 0 to 100.</summary>
		// Token: 0x04002B77 RID: 11127
		RoundCornerWidth,
		/// <summary>A percentage value that represents the height of a rounded corner, from 0 to 100.</summary>
		// Token: 0x04002B78 RID: 11128
		RoundCornerHeight,
		/// <summary>The amount of <see cref="F:System.Windows.Forms.VisualStyles.ColorProperty.GradientColor1" />  to use in a color gradient. The sum of the five GradientRatio properties must equal 255.</summary>
		// Token: 0x04002B79 RID: 11129
		GradientRatio1,
		/// <summary>The amount of <see cref="F:System.Windows.Forms.VisualStyles.ColorProperty.GradientColor2" />  to use in a color gradient. The sum of the five GradientRatio properties must equal 255.</summary>
		// Token: 0x04002B7A RID: 11130
		GradientRatio2,
		/// <summary>The amount of <see cref="F:System.Windows.Forms.VisualStyles.ColorProperty.GradientColor3" />  to use in a color gradient. The sum of the five GradientRatio properties must equal 255.</summary>
		// Token: 0x04002B7B RID: 11131
		GradientRatio3,
		/// <summary>The amount of <see cref="F:System.Windows.Forms.VisualStyles.ColorProperty.GradientColor4" />  to use in a color gradient. The sum of the five GradientRatio properties must equal 255.</summary>
		// Token: 0x04002B7C RID: 11132
		GradientRatio4,
		/// <summary>The amount of <see cref="F:System.Windows.Forms.VisualStyles.ColorProperty.GradientColor5" />  to use in a color gradient. The sum of the five GradientRatio properties must equal 255.</summary>
		// Token: 0x04002B7D RID: 11133
		GradientRatio5,
		/// <summary>The size of progress bar elements.</summary>
		// Token: 0x04002B7E RID: 11134
		ProgressChunkSize,
		/// <summary>The size of spaces between progress bar elements.</summary>
		// Token: 0x04002B7F RID: 11135
		ProgressSpaceSize,
		/// <summary>The amount of saturation for an image, between 0 and 255.</summary>
		// Token: 0x04002B80 RID: 11136
		Saturation,
		/// <summary>The size of the border around text characters.</summary>
		// Token: 0x04002B81 RID: 11137
		TextBorderSize,
		/// <summary>The minimum alpha value of a solid pixel, between 0 and 255.</summary>
		// Token: 0x04002B82 RID: 11138
		AlphaThreshold,
		/// <summary>The width of an element.</summary>
		// Token: 0x04002B83 RID: 11139
		Width,
		/// <summary>The height of an element. </summary>
		// Token: 0x04002B84 RID: 11140
		Height,
		/// <summary>The index into the font for font-based glyphs.</summary>
		// Token: 0x04002B85 RID: 11141
		GlyphIndex,
		/// <summary>A percentage value indicating how far a fixed-size element will stretch when the target exceeds the source. </summary>
		// Token: 0x04002B86 RID: 11142
		TrueSizeStretchMark,
		/// <summary>The minimum dots per inch (DPI) that <see cref="F:System.Windows.Forms.VisualStyles.FilenameProperty.ImageFile1" /> was designed for.</summary>
		// Token: 0x04002B87 RID: 11143
		MinDpi1,
		/// <summary>The minimum DPI that <see cref="F:System.Windows.Forms.VisualStyles.FilenameProperty.ImageFile2" /> was designed for.</summary>
		// Token: 0x04002B88 RID: 11144
		MinDpi2,
		/// <summary>The minimum DPI that <see cref="F:System.Windows.Forms.VisualStyles.FilenameProperty.ImageFile3" /> was designed for.</summary>
		// Token: 0x04002B89 RID: 11145
		MinDpi3,
		/// <summary>The minimum DPI that <see cref="F:System.Windows.Forms.VisualStyles.FilenameProperty.ImageFile4" /> was designed for.</summary>
		// Token: 0x04002B8A RID: 11146
		MinDpi4,
		/// <summary>The minimum DPI that <see cref="F:System.Windows.Forms.VisualStyles.FilenameProperty.ImageFile5" /> was designed for.</summary>
		// Token: 0x04002B8B RID: 11147
		MinDpi5
	}
}
