using System;

namespace System.Windows.Forms
{
	/// <summary>Specifies the display and layout information for text strings.</summary>
	// Token: 0x02000322 RID: 802
	[Flags]
	public enum TextFormatFlags
	{
		/// <summary>Aligns the text on the left side of the clipping area.</summary>
		// Token: 0x04001951 RID: 6481
		Left = 0,
		/// <summary>Aligns the text on the top of the bounding rectangle.</summary>
		// Token: 0x04001952 RID: 6482
		Top = 0,
		/// <summary>Applies the default formatting, which is left-aligned.</summary>
		// Token: 0x04001953 RID: 6483
		Default = 0,
		/// <summary>Adds padding to the bounding rectangle to accommodate overhanging glyphs. </summary>
		// Token: 0x04001954 RID: 6484
		GlyphOverhangPadding = 0,
		/// <summary>Centers the text horizontally within the bounding rectangle.</summary>
		// Token: 0x04001955 RID: 6485
		HorizontalCenter = 1,
		/// <summary>Aligns the text on the right side of the clipping area.</summary>
		// Token: 0x04001956 RID: 6486
		Right = 2,
		/// <summary>Centers the text vertically, within the bounding rectangle.</summary>
		// Token: 0x04001957 RID: 6487
		VerticalCenter = 4,
		/// <summary>Aligns the text on the bottom of the bounding rectangle. Applied only when the text is a single line.</summary>
		// Token: 0x04001958 RID: 6488
		Bottom = 8,
		/// <summary>Breaks the text at the end of a word.</summary>
		// Token: 0x04001959 RID: 6489
		WordBreak = 16,
		/// <summary>Displays the text in a single line.</summary>
		// Token: 0x0400195A RID: 6490
		SingleLine = 32,
		/// <summary>Expands tab characters. The default number of characters per tab is eight. The <see cref="F:System.Windows.Forms.TextFormatFlags.WordEllipsis" />, <see cref="F:System.Windows.Forms.TextFormatFlags.PathEllipsis" />, and <see cref="F:System.Windows.Forms.TextFormatFlags.EndEllipsis" /> values cannot be used with <see cref="F:System.Windows.Forms.TextFormatFlags.ExpandTabs" />.</summary>
		// Token: 0x0400195B RID: 6491
		ExpandTabs = 64,
		/// <summary>Allows the overhanging parts of glyphs and unwrapped text reaching outside the formatting rectangle to show.</summary>
		// Token: 0x0400195C RID: 6492
		NoClipping = 256,
		/// <summary>Includes the font external leading in line height. Typically, external leading is not included in the height of a line of text.</summary>
		// Token: 0x0400195D RID: 6493
		ExternalLeading = 512,
		/// <summary>Turns off processing of prefix characters. Typically, the ampersand (&amp;) mnemonic-prefix character is interpreted as a directive to underscore the character that follows, and the double-ampersand (&amp;&amp;) mnemonic-prefix characters as a directive to print a single ampersand. By specifying <see cref="F:System.Windows.Forms.TextFormatFlags.NoPrefix" />, this processing is turned off. For example, an input string of "A&amp;bc&amp;&amp;d" with <see cref="F:System.Windows.Forms.TextFormatFlags.NoPrefix" /> applied would result in output of "A&amp;bc&amp;&amp;d".</summary>
		// Token: 0x0400195E RID: 6494
		NoPrefix = 2048,
		/// <summary>Uses the system font to calculate text metrics.</summary>
		// Token: 0x0400195F RID: 6495
		Internal = 4096,
		/// <summary>Specifies the text should be formatted for display on a <see cref="T:System.Windows.Forms.TextBox" /> control.</summary>
		// Token: 0x04001960 RID: 6496
		TextBoxControl = 8192,
		/// <summary>Removes the center of trimmed lines and replaces it with an ellipsis. </summary>
		// Token: 0x04001961 RID: 6497
		PathEllipsis = 16384,
		/// <summary>Removes the end of trimmed lines, and replaces them with an ellipsis.</summary>
		// Token: 0x04001962 RID: 6498
		EndEllipsis = 32768,
		/// <summary>Has no effect on the drawn text.</summary>
		// Token: 0x04001963 RID: 6499
		ModifyString = 65536,
		/// <summary>Displays the text from right to left.</summary>
		// Token: 0x04001964 RID: 6500
		RightToLeft = 131072,
		/// <summary>Trims the line to the nearest word and an ellipsis is placed at the end of a trimmed line.</summary>
		// Token: 0x04001965 RID: 6501
		WordEllipsis = 262144,
		/// <summary>Applies to Windows 98, Windows Me, Windows 2000, or Windows XP only:</summary>
		// Token: 0x04001966 RID: 6502
		NoFullWidthCharacterBreak = 524288,
		/// <summary>Applies to Windows 2000 and Windows XP only: </summary>
		// Token: 0x04001967 RID: 6503
		HidePrefix = 1048576,
		/// <summary>Applies to Windows 2000 or Windows XP only: </summary>
		// Token: 0x04001968 RID: 6504
		PrefixOnly = 2097152,
		/// <summary>Preserves the clipping specified by a <see cref="T:System.Drawing.Graphics" /> object. Applies only to methods receiving an <see cref="T:System.Drawing.IDeviceContext" /> that is a <see cref="T:System.Drawing.Graphics" />.</summary>
		// Token: 0x04001969 RID: 6505
		PreserveGraphicsClipping = 16777216,
		/// <summary>Preserves the transformation specified by a <see cref="T:System.Drawing.Graphics" />. Applies only to methods receiving an <see cref="T:System.Drawing.IDeviceContext" /> that is a <see cref="T:System.Drawing.Graphics" />.</summary>
		// Token: 0x0400196A RID: 6506
		PreserveGraphicsTranslateTransform = 33554432,
		/// <summary>Does not add padding to the bounding rectangle.</summary>
		// Token: 0x0400196B RID: 6507
		NoPadding = 268435456,
		/// <summary>Adds padding to both sides of the bounding rectangle.</summary>
		// Token: 0x0400196C RID: 6508
		LeftAndRightPadding = 536870912
	}
}
