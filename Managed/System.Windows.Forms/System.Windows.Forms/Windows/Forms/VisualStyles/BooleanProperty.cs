using System;

namespace System.Windows.Forms.VisualStyles
{
	/// <summary>Identifies the Boolean properties of a visual style element.</summary>
	// Token: 0x020004D2 RID: 1234
	public enum BooleanProperty
	{
		/// <summary>The image has transparent areas.</summary>
		// Token: 0x04002A08 RID: 10760
		Transparent = 2201,
		/// <summary>The width of nonclient captions varies with the extent of the text.</summary>
		// Token: 0x04002A09 RID: 10761
		AutoSize,
		/// <summary>Only the border of an image is drawn.</summary>
		// Token: 0x04002A0A RID: 10762
		BorderOnly,
		/// <summary>The control will handle composite drawing.</summary>
		// Token: 0x04002A0B RID: 10763
		Composited,
		/// <summary>The background of a fixed-size element is a filled rectangle.</summary>
		// Token: 0x04002A0C RID: 10764
		BackgroundFill,
		/// <summary>The glyph has transparent areas.</summary>
		// Token: 0x04002A0D RID: 10765
		GlyphTransparent,
		/// <summary>Only the glyph should be drawn, not the background.</summary>
		// Token: 0x04002A0E RID: 10766
		GlyphOnly,
		/// <summary>The sizing handle will always be displayed.</summary>
		// Token: 0x04002A0F RID: 10767
		AlwaysShowSizingBar,
		/// <summary>The image is mirrored in right-to-left display modes.</summary>
		// Token: 0x04002A10 RID: 10768
		MirrorImage,
		/// <summary>The height and width must be sized equally.</summary>
		// Token: 0x04002A11 RID: 10769
		UniformSizing,
		/// <summary>The scaling factor must be an integer for fixed-size elements.</summary>
		// Token: 0x04002A12 RID: 10770
		IntegralSizing,
		/// <summary>The source image will scale larger when needed.</summary>
		// Token: 0x04002A13 RID: 10771
		SourceGrow,
		/// <summary>The source image will scale smaller when needed.</summary>
		// Token: 0x04002A14 RID: 10772
		SourceShrink
	}
}
